#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string[]] $Components,
    [string[]] $Rids,
    [switch] $ManagedOnly
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-ZipEntryNames {
    param(
        [Parameter(Mandatory)]
        [string] $Path
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $zip = [System.IO.Compression.ZipFile]::OpenRead($Path)
    try {
        return @($zip.Entries | Where-Object { $_.FullName -and -not $_.FullName.EndsWith('/', [System.StringComparison]::Ordinal) } | ForEach-Object {
            $_.FullName.Replace('\', '/')
        })
    }
    finally {
        $zip.Dispose()
    }
}

function New-EntrySet {
    param(
        [Parameter(Mandatory)]
        [string[]] $EntryNames
    )

    $set = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    foreach ($entry in $EntryNames) {
        [void] $set.Add($entry)
    }

    return $set
}

function Add-ContentError {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:errors.Add($Message)
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

foreach ($componentId in $Components) {
    Get-ReleaseComponent -Manifest $manifest -Component $componentId | Out-Null
}
foreach ($rid in $Rids) {
    Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
}

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]
$packages = Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision
if ($ManagedOnly) {
    $packages = @($packages | Where-Object { $_.Kind -eq 'managed' })
}

function Get-ZipEntryText {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $EntryName
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $zip = [System.IO.Compression.ZipFile]::OpenRead($Path)
    try {
        $entry = $zip.GetEntry($EntryName)
        if (-not $entry) {
            return $null
        }

        $stream = $entry.Open()
        try {
            $reader = [System.IO.StreamReader]::new($stream, [System.Text.Encoding]::UTF8, $true)
            try {
                return $reader.ReadToEnd()
            }
            finally {
                $reader.Dispose()
            }
        }
        finally {
            $stream.Dispose()
        }
    }
    finally {
        $zip.Dispose()
    }
}

foreach ($package in $packages) {
    $component = $null
    $packageRids = @()
    if ($package.Kind -eq 'native') {
        $component = Get-ReleaseComponent -Manifest $manifest -Component $package.VersionComponent
        if ($Components -notcontains $component.id) {
            continue
        }

        $packageRids = @($package.Rids | Where-Object { $Rids -contains $_ })
        if ($packageRids.Count -eq 0) {
            continue
        }
    }

    $packageFileName = Get-ReleaseNuGetPackageFileName -Package $package
    $packagePath = Join-Path $PackageDir $packageFileName
    if (-not (Test-Path -LiteralPath $packagePath -PathType Leaf)) {
        Add-ContentError "Expected NuGet package is missing: $packagePath"
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'package'
            Expected = $packageFileName
            Count = 0
            Status = 'missing'
        })
        continue
    }

    $entryNames = Get-ZipEntryNames -Path $packagePath
    $entrySet = New-EntrySet -EntryNames $entryNames
    $rows.Add([pscustomobject]@{
        PackageId = $package.Id
        Scope = 'package'
        Expected = $packageFileName
        Count = $entryNames.Count
        Status = 'present'
    })

    if ($package.Kind -ne 'native') {
        $managedExpectedEntries = @(
            'SDL3-CS.nuspec',
            'CODE_OF_CONDUCT.md',
            'LICENSE',
            'README-nuget.md',
            'README.md',
            'SDL3-CS.xml',
            'logo.png',
            'lib/net7.0/SDL3-CS.dll',
            'lib/net7.0/SDL3-CS.xml',
            'lib/net8.0/SDL3-CS.dll',
            'lib/net8.0/SDL3-CS.xml',
            'lib/net9.0/SDL3-CS.dll',
            'lib/net9.0/SDL3-CS.xml',
            'lib/net10.0/SDL3-CS.dll',
            'lib/net10.0/SDL3-CS.xml'
        )
        foreach ($expectedEntry in $managedExpectedEntries) {
            $status = if ($entrySet.Contains($expectedEntry)) { 'present' } else { 'missing' }
            if ($status -eq 'missing') {
                Add-ContentError "$($package.Id) package is missing managed entry: $expectedEntry"
            }
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = 'managed'
                Expected = $expectedEntry
                Count = if ($status -eq 'present') { 1 } else { 0 }
                Status = $status
            })
        }

        $runtimeEntries = @($entryNames | Where-Object { $_.StartsWith('runtimes/', [System.StringComparison]::Ordinal) })
        foreach ($runtimeEntry in $runtimeEntries) {
            Add-ContentError "$($package.Id) managed package must not contain runtime entry: $runtimeEntry"
        }
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'managed-runtime'
            Expected = 'no runtimes/* entries'
            Count = $runtimeEntries.Count
            Status = if ($runtimeEntries.Count -eq 0) { 'absent' } else { 'unexpected' }
        })

        try {
            [xml] $nuspec = Get-ZipEntryText -Path $packagePath -EntryName 'SDL3-CS.nuspec'
            $actualId = [string] $nuspec.package.metadata.id
            $actualVersion = [string] $nuspec.package.metadata.version
            $expectedVersion = Get-ReleaseNormalizedNuGetVersion -PackageVersion $package.PackageVersion
            if ($actualId -ne $package.Id) {
                Add-ContentError "$($package.Id) nuspec id '$actualId' does not match expected '$($package.Id)'."
            }
            if ($actualVersion -ne $expectedVersion) {
                Add-ContentError "$($package.Id) nuspec version '$actualVersion' does not match expected '$expectedVersion'."
            }
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = 'managed-metadata'
                Expected = "$($package.Id) $expectedVersion"
                Count = 1
                Status = if ($actualId -eq $package.Id -and $actualVersion -eq $expectedVersion) { 'valid' } else { 'mismatch' }
            })
        }
        catch {
            Add-ContentError "$($package.Id) nuspec metadata could not be validated: $($_.Exception.Message)"
        }

        $readmeText = Get-ZipEntryText -Path $packagePath -EntryName 'README-nuget.md'
        $releaseMarker = "SDL3-CS $($package.PackageVersion)"
        $readmeIsCurrent = $readmeText -and $readmeText.Contains($releaseMarker, [System.StringComparison]::Ordinal)
        if (-not $readmeIsCurrent) {
            Add-ContentError "$($package.Id) package README does not identify managed release $($package.PackageVersion)."
        }
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'managed-readme'
            Expected = $releaseMarker
            Count = if ($readmeIsCurrent) { 1 } else { 0 }
            Status = if ($readmeIsCurrent) { 'valid' } else { 'mismatch' }
        })
        continue
    }

    if ($package.VersionComponent -eq 'SDL_shadercross' -and $package.NativePackagePlatform -in @('Windows', 'Linux', 'MacOS')) {
        foreach ($licenseEntry in @(
            'licenses/DirectXShaderCompiler/LICENSE.TXT',
            'licenses/DirectXShaderCompiler/LICENSE-LLVM.txt',
            'licenses/DirectXShaderCompiler/LICENSE-MS.txt',
            'licenses/DirectXShaderCompiler/ThirdPartyNotices.txt'
        )) {
            if (-not $entrySet.Contains($licenseEntry)) {
                Add-ContentError "$($package.Id) package is missing $licenseEntry."
                $rows.Add([pscustomobject]@{
                    PackageId = $package.Id
                    Scope = 'license'
                    Expected = $licenseEntry
                    Count = 0
                    Status = 'missing'
                })
            }
            else {
                $rows.Add([pscustomobject]@{
                    PackageId = $package.Id
                    Scope = 'license'
                    Expected = $licenseEntry
                    Count = 1
                    Status = 'present'
                })
            }
        }
    }

    $nativeArtifactProject = if ($package.PSObject.Properties.Name.Contains('NativeArtifactProject') -and $package.NativeArtifactProject) {
        $package.NativeArtifactProject
    }
    else {
        $package.Project
    }
    $packageProject = Resolve-ReleasePath $nativeArtifactProject
    $packageRoot = Split-Path -Parent $packageProject

    $targetsEntry = "buildTransitive/$($package.Id).targets"
    if (-not $entrySet.Contains($targetsEntry)) {
        Add-ContentError "$($package.Id) package is missing $targetsEntry."
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'buildTransitive'
            Expected = $targetsEntry
            Count = 0
            Status = 'missing'
        })
    }
    else {
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'buildTransitive'
            Expected = $targetsEntry
            Count = 1
            Status = 'present'
        })
    }

    foreach ($rid in $packageRids) {
        $ridRoot = Join-Path $packageRoot "lib\$rid"
        if (-not (Test-Path -LiteralPath $ridRoot -PathType Container)) {
            Add-ContentError "$($package.Id) source RID folder is missing before package content validation: $ridRoot"
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = $rid
                Expected = "runtimes/$rid/native"
                Count = 0
                Status = 'missing-source'
            })
            continue
        }

        $sourceFiles = @(Get-ChildItem -LiteralPath $ridRoot -File -Recurse)
        if ($sourceFiles.Count -eq 0) {
            Add-ContentError "$($package.Id) source RID folder has no files before package content validation: $ridRoot"
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = $rid
                Expected = "runtimes/$rid/native"
                Count = 0
                Status = 'empty-source'
            })
            continue
        }

        $expectedEntries = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
        foreach ($sourceFile in $sourceFiles) {
            $relative = [System.IO.Path]::GetRelativePath($ridRoot, $sourceFile.FullName).Replace('\', '/')
            $expectedEntry = "runtimes/$rid/native/$relative"
            [void] $expectedEntries.Add($expectedEntry)
            if (-not $entrySet.Contains($expectedEntry)) {
                Add-ContentError "$($package.Id) package is missing runtime entry: $expectedEntry"
            }
        }

        $runtimePrefix = "runtimes/$rid/native/"
        $actualEntries = @($entryNames | Where-Object { $_.StartsWith($runtimePrefix, [System.StringComparison]::Ordinal) })
        foreach ($actualEntry in $actualEntries) {
            if (-not $expectedEntries.Contains($actualEntry)) {
                Add-ContentError "$($package.Id) package has runtime entry not present in source lib folder $ridRoot`: $actualEntry"
            }
        }

        $status = if ($actualEntries.Count -eq $sourceFiles.Count) { 'present' } else { 'mismatch' }
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = $rid
            Expected = "runtimes/$rid/native"
            Count = $actualEntries.Count
            Status = $status
        })
    }
}

$rows | Sort-Object PackageId, Scope, Expected | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "NuGet package content validation failed with $($errors.Count) error(s)."
}

Write-Host "NuGet package contents are valid."
