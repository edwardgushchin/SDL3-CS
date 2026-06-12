#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string[]] $Components,
    [string[]] $Rids
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

foreach ($package in $packages) {
    $packagePath = Join-Path $PackageDir "$($package.Id).$($package.PackageVersion).nupkg"
    if (-not (Test-Path -LiteralPath $packagePath -PathType Leaf)) {
        Add-ContentError "Expected NuGet package is missing: $packagePath"
        $rows.Add([pscustomobject]@{
            PackageId = $package.Id
            Scope = 'package'
            Expected = "$($package.Id).$($package.PackageVersion).nupkg"
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
        Expected = "$($package.Id).$($package.PackageVersion).nupkg"
        Count = $entryNames.Count
        Status = 'present'
    })

    if ($package.Kind -ne 'native') {
        continue
    }

    $component = Get-ReleaseComponent -Manifest $manifest -Component $package.VersionComponent
    if ($Components -notcontains $component.id) {
        continue
    }

    $packageProject = Resolve-ReleasePath $component.packageProject
    $packageRoot = Split-Path -Parent $packageProject
    $targetsEntry = "buildTransitive/$($component.packageId).targets"
    if (-not $entrySet.Contains($targetsEntry)) {
        Add-ContentError "$($component.packageId) package is missing $targetsEntry."
        $rows.Add([pscustomobject]@{
            PackageId = $component.packageId
            Scope = 'buildTransitive'
            Expected = $targetsEntry
            Count = 0
            Status = 'missing'
        })
    }
    else {
        $rows.Add([pscustomobject]@{
            PackageId = $component.packageId
            Scope = 'buildTransitive'
            Expected = $targetsEntry
            Count = 1
            Status = 'present'
        })
    }

    foreach ($rid in $Rids) {
        $ridRoot = Join-Path $packageRoot "lib\$rid"
        if (-not (Test-Path -LiteralPath $ridRoot -PathType Container)) {
            Add-ContentError "$($component.packageId) source RID folder is missing before package content validation: $ridRoot"
            $rows.Add([pscustomobject]@{
                PackageId = $component.packageId
                Scope = $rid
                Expected = "runtimes/$rid/native"
                Count = 0
                Status = 'missing-source'
            })
            continue
        }

        $sourceFiles = @(Get-ChildItem -LiteralPath $ridRoot -File -Recurse)
        if ($sourceFiles.Count -eq 0) {
            Add-ContentError "$($component.packageId) source RID folder has no files before package content validation: $ridRoot"
            $rows.Add([pscustomobject]@{
                PackageId = $component.packageId
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
                Add-ContentError "$($component.packageId) package is missing runtime entry: $expectedEntry"
            }
        }

        $runtimePrefix = "runtimes/$rid/native/"
        $actualEntries = @($entryNames | Where-Object { $_.StartsWith($runtimePrefix, [System.StringComparison]::Ordinal) })
        foreach ($actualEntry in $actualEntries) {
            if (-not $expectedEntries.Contains($actualEntry)) {
                Add-ContentError "$($component.packageId) package has runtime entry not present in source lib folder $ridRoot`: $actualEntry"
            }
        }

        $status = if ($actualEntries.Count -eq $sourceFiles.Count) { 'present' } else { 'mismatch' }
        $rows.Add([pscustomobject]@{
            PackageId = $component.packageId
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
