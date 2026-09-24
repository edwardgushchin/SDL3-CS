#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string] $ReceiptRoot,
    [string[]] $PackageIds,
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
if (-not $ReceiptRoot) {
    $ReceiptRoot = Resolve-ReleasePath (Join-Path $manifest.artifactsRoot 'receipts')
}
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
$packages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
if ($ManagedOnly) {
    $packages = @($packages | Where-Object { $_.Kind -eq 'managed' })
}
elseif ($PackageIds) {
    $selectedIds = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    $selectedPackages = @()
    foreach ($packageId in $PackageIds) {
        if ([string]::IsNullOrWhiteSpace($packageId)) {
            throw 'PackageIds must not contain empty values.'
        }
        if (-not $selectedIds.Add($packageId)) {
            throw "PackageIds contains duplicate package id: $packageId"
        }

        $matches = @($packages | Where-Object { $_.Id -eq $packageId })
        if ($matches.Count -ne 1) {
            throw "PackageIds contains unknown package id: $packageId"
        }
        if ($matches[0].Kind -ne 'native') {
            throw "PackageIds can select only native packages: $packageId"
        }

        $selectedPackages += $matches[0]
    }
    $packages = $selectedPackages
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

function Get-ZipEntryHash {
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
            return [Convert]::ToHexString([System.Security.Cryptography.SHA256]::HashData($stream))
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

    $entryNames = @(Get-ZipEntryNames -Path $packagePath)
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

    if ($package.NativePackagePlatform -eq 'Android') {
        $androidNativeEntries = @($entryNames | Where-Object {
            $_ -match '^runtimes/android-[^/]+/native/.+\.so(?:\..+)?$'
        })
        try {
            if ($androidNativeEntries.Count -eq 0) {
                throw "Android package '$($package.Id)' contains no native shared libraries to validate."
            }

            & (Join-Path $PSScriptRoot 'Test-AndroidPageSizeCompatibility.ps1') -Path $packagePath | Out-Host
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = 'android-page-size'
                Expected = 'all packaged .so files are valid ELF; every ELF64 PT_LOAD alignment is >= 0x4000'
                Count = $androidNativeEntries.Count
                Status = 'valid'
            })
        }
        catch {
            Add-ContentError "$($package.Id) Android page-size compatibility validation failed: $($_.Exception.Message)"
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = 'android-page-size'
                Expected = 'all packaged .so files are valid ELF; every ELF64 PT_LOAD alignment is >= 0x4000'
                Count = 0
                Status = 'failed'
            })
        }
    }

    $licenseRoot = Join-Path $PSScriptRoot '..\..\SDL3-CS.NativePackages\ThirdPartyLicenses'
    $licenseFolders = @(switch ($package.VersionComponent) {
        'SDL' { @('SDL') }
        'SDL_image' { @('Image', 'libwebp') }
        'SDL_mixer' { @('Mixer') }
        'SDL_ttf' { @('TTF') }
        'SDL_shadercross' { @('Shadercross') }
    })
    $licenseFileCounts = @{
        SDL = 1
        Image = 10
        libwebp = 1
        Mixer = 11
        TTF = 6
        Shadercross = 2
        DirectXShaderCompiler = 4
        ShadercrossVkd3d = 2
    }
    if ($package.VersionComponent -eq 'SDL_shadercross' -and $package.NativePackagePlatform -in @('Windows', 'Linux', 'MacOS')) {
        $licenseFolders += 'DirectXShaderCompiler'
    }
    if ($package.VersionComponent -eq 'SDL_shadercross' -and $package.NativePackagePlatform -in @('Linux', 'MacOS')) {
        $licenseFolders += 'ShadercrossVkd3d'
    }

    $licenseFiles = @((Join-Path $licenseRoot 'README.md'))
    foreach ($folder in $licenseFolders) {
        $folderPath = Join-Path $licenseRoot $folder
        $sourceFiles = @(Get-ChildItem -LiteralPath $folderPath -File)
        if ($sourceFiles.Count -ne $licenseFileCounts[$folder]) {
            Add-ContentError "$($package.Id) license source folder $folder has $($sourceFiles.Count) files; expected $($licenseFileCounts[$folder])."
        }
        $licenseFiles += @($sourceFiles | ForEach-Object { $_.FullName })
    }
    foreach ($sourceFile in $licenseFiles) {
        $relative = [System.IO.Path]::GetRelativePath($licenseRoot, $sourceFile).Replace('\', '/')
        $licenseEntry = "licenses/$relative"
        $expectedHash = (Get-FileHash -LiteralPath $sourceFile -Algorithm SHA256).Hash
        $actualHash = Get-ZipEntryHash -Path $packagePath -EntryName $licenseEntry
        $status = if (-not $actualHash) { 'missing' } elseif ($actualHash -ne $expectedHash) { 'mismatch' } else { 'valid' }
        if ($status -ne 'valid') {
            Add-ContentError "$($package.Id) license entry $licenseEntry is $status."
        }
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'license'
            Expected = $licenseEntry
            Count = if ($actualHash) { 1 } else { 0 }
            Status = $status
        })
    }

    $nativeArtifactProject = if ($package.PSObject.Properties.Name.Contains('NativeArtifactProject') -and $package.NativeArtifactProject) {
        $package.NativeArtifactProject
    }
    else {
        $package.Project
    }
    $packageProject = Resolve-ReleasePath $nativeArtifactProject
    $packageRoot = Split-Path -Parent $packageProject

    $needsLgplSources = $package.VersionComponent -eq 'SDL_mixer' -or
        ($package.VersionComponent -eq 'SDL_shadercross' -and $package.NativePackagePlatform -in @('Linux', 'MacOS'))
    if ($needsLgplSources) {
        $sourceRoot = Join-Path $PSScriptRoot '..\..\SDL3-CS.NativePackages\ThirdPartySources'
        $sourceManifestPath = Join-Path $sourceRoot 'SOURCE_MANIFEST.json'
        $sourceManifest = Get-Content -LiteralPath $sourceManifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
        $componentSources = @($sourceManifest.sources | Where-Object { $_.component -eq $package.VersionComponent })
        $expectedSourceIds = if ($package.VersionComponent -eq 'SDL_mixer') { @('game-music-emu', 'mpg123') } else { @('vkd3d') }
        if (@(Compare-Object -ReferenceObject $expectedSourceIds -DifferenceObject @($componentSources | ForEach-Object { $_.id } | Sort-Object)).Count -ne 0) {
            Add-ContentError "$($package.Id) source manifest does not identify its complete LGPL dependency set."
        }
        foreach ($source in $componentSources) {
            $archivePath = Join-Path $sourceRoot $source.archive
            if ($source.componentSourceRef -ne $component.sourceRef -or
                (Get-FileHash -LiteralPath $archivePath -Algorithm SHA256).Hash.ToLowerInvariant() -ne $source.sha256) {
                Add-ContentError "$($package.Id) $($source.id) archive or source revision differs from its source manifest."
            }
        }
        $sourceFiles = @(
            (Join-Path $sourceRoot 'README.md'),
            $sourceManifestPath
        ) + @($componentSources | ForEach-Object { Join-Path $sourceRoot $_.archive })
        foreach ($sourceFile in $sourceFiles) {
            $sourceEntry = "licenses/sources/$([System.IO.Path]::GetFileName($sourceFile))"
            $expectedHash = (Get-FileHash -LiteralPath $sourceFile -Algorithm SHA256).Hash
            $actualHash = Get-ZipEntryHash -Path $packagePath -EntryName $sourceEntry
            if ($actualHash -ne $expectedHash) {
                Add-ContentError "$($package.Id) source entry $sourceEntry is missing or differs from its tracked source."
            }
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = 'LGPL-source'
                Expected = $sourceEntry
                Count = if ($actualHash) { 1 } else { 0 }
                Status = if ($actualHash -eq $expectedHash) { 'valid' } else { 'mismatch' }
            })
        }

        foreach ($rid in $packageRids) {
            $receiptPath = Join-Path $ReceiptRoot "$($package.VersionComponent)/$rid.json"
            if (-not (Test-Path -LiteralPath $receiptPath -PathType Leaf)) {
                Add-ContentError "$($package.Id) LGPL build receipt is missing for $rid."
                continue
            }
            $receiptEntry = "licenses/provenance/$rid.json"
            $expectedHash = (Get-FileHash -LiteralPath $receiptPath -Algorithm SHA256).Hash
            $actualHash = Get-ZipEntryHash -Path $packagePath -EntryName $receiptEntry
            if ($actualHash -ne $expectedHash) {
                Add-ContentError "$($package.Id) receipt entry $receiptEntry is missing or differs from the validated build receipt."
                continue
            }
            $receipt = Get-Content -LiteralPath $receiptPath -Raw -Encoding UTF8 | ConvertFrom-Json
            $componentRef = @($receipt.SourceReferences | Where-Object { $_.Component -eq $package.VersionComponent })
            if ($componentRef.Count -ne 1 -or $componentRef[0].Head -ne $component.sourceRef) {
                Add-ContentError "$($package.Id) receipt $rid does not identify the pinned component source."
            }
            $binaryEntries = @($entryNames | Where-Object {
                $_.StartsWith("runtimes/$rid/native/", [System.StringComparison]::Ordinal) -and
                (($_ -match '(^|/)(lib)?gme(\.|$|[-])') -or
                 ($_ -match '(^|/)(lib)?mpg123(\.|$|[-])') -or
                 ($_ -match '(^|/)libvkd3d'))
            })
            if ($binaryEntries.Count -eq 0) {
                Add-ContentError "$($package.Id) $rid has no expected standalone LGPL library."
            }
            foreach ($binaryEntry in $binaryEntries) {
                $fileName = [System.IO.Path]::GetFileName($binaryEntry)
                $artifact = @($receipt.Artifacts | Where-Object {
                    $_.Name -eq $fileName -and $_.RelativePath.Replace('\', '/') -eq "lib/$rid/$fileName"
                })
                $binaryHash = Get-ZipEntryHash -Path $packagePath -EntryName $binaryEntry
                if ($artifact.Count -ne 1 -or -not $binaryHash -or $artifact[0].Sha256 -ne $binaryHash.ToLowerInvariant()) {
                    Add-ContentError "$($package.Id) $binaryEntry does not match its source-linked build receipt."
                }
            }
            $rows.Add([pscustomobject]@{
                PackageId = $package.Id
                Scope = 'LGPL-provenance'
                Expected = $rid
                Count = $binaryEntries.Count
                Status = if ($binaryEntries.Count -gt 0) { 'checked' } else { 'missing' }
            })
        }
    }

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
