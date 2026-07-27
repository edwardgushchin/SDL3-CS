#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][int] $PackageRevision,
    [Parameter(Mandatory)][int] $PreviousPackageRevision,
    [Parameter(Mandatory)][string[]] $PackageIds,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string] $PreviousPackageDir,
    [string[]] $RequiredEntries = @('licenses/libwebp/COPYING'),
    [switch] $SkipTargetAvailabilityCheck
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-PackageUri {
    param([Parameter(Mandatory)][object] $Package)

    $id = $Package.Id.ToLowerInvariant()
    $version = (Get-ReleaseNormalizedNuGetVersion -PackageVersion $Package.PackageVersion).ToLowerInvariant()
    return "https://api.nuget.org/v3-flatcontainer/$id/$version/$id.$version.nupkg"
}

function Get-ZipPayloadMap {
    param([Parameter(Mandatory)][string] $Path)

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $map = [System.Collections.Generic.Dictionary[string, string]]::new([System.StringComparer]::Ordinal)
    $zip = [System.IO.Compression.ZipFile]::OpenRead($Path)
    try {
        foreach ($entry in $zip.Entries) {
            $name = $entry.FullName.Replace('\', '/')
            if (-not $name -or $name.EndsWith('/', [System.StringComparison]::Ordinal)) {
                continue
            }
            if (-not ($name.StartsWith('runtimes/', [System.StringComparison]::Ordinal) -or
                $name.StartsWith('buildTransitive/', [System.StringComparison]::Ordinal))) {
                continue
            }

            $sha256 = [System.Security.Cryptography.SHA256]::Create()
            try {
                $stream = $entry.Open()
                try {
                    $hash = $sha256.ComputeHash($stream)
                    $map[$name] = ([System.BitConverter]::ToString($hash) -replace '-', '').ToLowerInvariant()
                }
                finally {
                    $stream.Dispose()
                }
            }
            finally {
                $sha256.Dispose()
            }
        }
    }
    finally {
        $zip.Dispose()
    }

    return $map
}

function Test-ZipEntryExists {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $EntryName
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $zip = [System.IO.Compression.ZipFile]::OpenRead($Path)
    try {
        return $null -ne $zip.GetEntry($EntryName)
    }
    finally {
        $zip.Dispose()
    }
}

if ($PackageRevision -lt 0 -or $PreviousPackageRevision -lt 0) {
    throw 'Package revisions must be zero or greater.'
}
if (-not $PackageIds -or $PackageIds.Count -eq 0) {
    throw 'PackageIds must select at least one native package.'
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir
if ($PreviousPackageDir) {
    $PreviousPackageDir = Resolve-ReleasePath $PreviousPackageDir
}

$targetRows = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
$previousRows = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PreviousPackageRevision)
$selectedIds = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
$selected = @()
foreach ($packageId in $PackageIds) {
    if ([string]::IsNullOrWhiteSpace($packageId)) {
        throw 'PackageIds must not contain empty values.'
    }
    if (-not $selectedIds.Add($packageId)) {
        throw "PackageIds contains duplicate package id: $packageId"
    }

    $target = @($targetRows | Where-Object { $_.Id -eq $packageId })
    $previous = @($previousRows | Where-Object { $_.Id -eq $packageId })
    if ($target.Count -ne 1 -or $previous.Count -ne 1) {
        throw "PackageIds contains unknown package id: $packageId"
    }
    if ($target[0].Kind -ne 'native' -or $previous[0].Kind -ne 'native') {
        throw "PackageIds can select only native packages: $packageId"
    }
    if ([version]$target[0].PackageVersion -le [version]$previous[0].PackageVersion) {
        throw "Target package version must be newer than previous package version for $packageId`: $($target[0].PackageVersion) <= $($previous[0].PackageVersion)"
    }

    $selected += [pscustomobject]@{ Target = $target[0]; Previous = $previous[0] }
}

$tempRoot = $null
$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]
try {
    if (-not $PreviousPackageDir) {
        $tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
        $tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-native-repack-source-$([guid]::NewGuid().ToString('N'))"))
        if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
            throw "Unsafe temporary source package path: $tempRoot"
        }
        New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null
        $PreviousPackageDir = $tempRoot
    }

    foreach ($selection in $selected) {
        $target = $selection.Target
        $previous = $selection.Previous
        $targetPath = Get-ReleaseNuGetPackagePath -PackageDir $PackageDir -Package $target
        $previousPath = Get-ReleaseNuGetPackagePath -PackageDir $PreviousPackageDir -Package $previous

        if (-not (Test-Path -LiteralPath $targetPath -PathType Leaf)) {
            $errors.Add("Target repack package is missing: $targetPath")
            continue
        }

        if (-not $SkipTargetAvailabilityCheck) {
            $response = Invoke-WebRequest -Uri (Get-PackageUri -Package $target) -Method Head -TimeoutSec 30 -SkipHttpErrorCheck
            if ([int]$response.StatusCode -ne 404) {
                $errors.Add("Target NuGet package version is not available: $($target.Id) $($target.PackageVersion) (HTTP $([int]$response.StatusCode))")
            }
        }

        if (-not (Test-Path -LiteralPath $previousPath -PathType Leaf)) {
            $uri = Get-PackageUri -Package $previous
            try {
                Invoke-WebRequest -Uri $uri -OutFile $previousPath -TimeoutSec 120
            }
            catch {
                $errors.Add("Could not download previous NuGet package $($previous.Id) $($previous.PackageVersion): $($_.Exception.Message)")
                continue
            }
        }

        foreach ($requiredEntry in $RequiredEntries) {
            if (-not (Test-ZipEntryExists -Path $targetPath -EntryName $requiredEntry)) {
                $errors.Add("Target repack package $($target.Id) is missing required entry: $requiredEntry")
            }
        }

        $previousPayload = Get-ZipPayloadMap -Path $previousPath
        $targetPayload = Get-ZipPayloadMap -Path $targetPath
        if ($previousPayload.Count -eq 0) {
            $errors.Add("Previous package has no runtimes/buildTransitive payload: $($previous.Id) $($previous.PackageVersion)")
        }
        foreach ($entryName in $previousPayload.Keys) {
            if (-not $targetPayload.ContainsKey($entryName)) {
                $errors.Add("Target package $($target.Id) is missing unchanged payload entry: $entryName")
            }
            elseif ($targetPayload[$entryName] -ne $previousPayload[$entryName]) {
                $errors.Add("Target package $($target.Id) changed payload entry: $entryName")
            }
        }
        foreach ($entryName in $targetPayload.Keys) {
            if (-not $previousPayload.ContainsKey($entryName)) {
                $errors.Add("Target package $($target.Id) added unexpected payload entry: $entryName")
            }
        }

        $status = if (@($errors | Where-Object { $_ -like "*$($target.Id)*" }).Count -eq 0) { 'valid' } else { 'failed' }
        $rows.Add([pscustomobject]@{
            Package = $target.Id
            PreviousVersion = $previous.PackageVersion
            TargetVersion = $target.PackageVersion
            PayloadEntries = $targetPayload.Count
            Status = $status
        })
    }
}
finally {
    if ($tempRoot -and (Test-Path -LiteralPath $tempRoot)) {
        $tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-native-repack-source-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary source package path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

$rows | Format-Table -AutoSize
if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native package repack validation failed with $($errors.Count) error(s)."
}

Write-Host "Native package repack is valid for $($selected.Count) package(s)."
