#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $NativePackageRevision = 1,
    [string] $Rid = 'win-x64',
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $Destination = 'SDL3-CS.Tests/bin/Release/net8.0',
    [string] $PackageCacheDir = 'artifacts/release/managed-test-runtime',
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-ArchiveEntrySha256 {
    param([Parameter(Mandatory)][object] $Entry)

    $sha256 = [System.Security.Cryptography.SHA256]::Create()
    try {
        $stream = $Entry.Open()
        try {
            return [System.Convert]::ToHexString($sha256.ComputeHash($stream))
        }
        finally {
            $stream.Dispose()
        }
    }
    finally {
        $sha256.Dispose()
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
Get-ReleaseRid -Manifest $manifest -Rid $Rid | Out-Null
$packages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $NativePackageRevision |
    Where-Object { $_.Kind -eq 'native' -and @($_.Rids) -contains $Rid } |
    Sort-Object Id)

$expectedComponentCount = @($manifest.components).Count
if ($packages.Count -ne $expectedComponentCount) {
    throw "Expected $expectedComponentCount native package(s) for $Rid, got $($packages.Count)."
}

$destinationPath = Resolve-ReleasePath $Destination
$cachePath = Resolve-ReleasePath $PackageCacheDir
$rows = @($packages | ForEach-Object {
    $lowerId = $_.Id.ToLowerInvariant()
    $lowerVersion = (Get-ReleaseNormalizedNuGetVersion -PackageVersion $_.PackageVersion).ToLowerInvariant()
    [pscustomobject]@{
        Package = $_
        Uri = "https://api.nuget.org/v3-flatcontainer/$lowerId/$lowerVersion/$lowerId.$lowerVersion.nupkg"
        PackagePath = Join-Path $cachePath (Get-ReleaseNuGetPackageFileName -Package $_)
    }
})

$rows | ForEach-Object {
    [pscustomobject]@{
        Package = $_.Package.Id
        Version = $_.Package.PackageVersion
        Rid = $Rid
        Uri = $_.Uri
    }
} | Format-Table -AutoSize

if ($DryRun) {
    Write-Host "[dry-run] Would restore $($packages.Count) published native package(s) into $destinationPath."
    return
}

New-Item -ItemType Directory -Force -Path $destinationPath | Out-Null
New-Item -ItemType Directory -Force -Path $cachePath | Out-Null
$destinationRoot = [System.IO.Path]::GetFullPath($destinationPath).TrimEnd([System.IO.Path]::DirectorySeparatorChar) + [System.IO.Path]::DirectorySeparatorChar
$writtenEntries = [System.Collections.Generic.Dictionary[string, string]]::new([System.StringComparer]::OrdinalIgnoreCase)
$restoredCount = 0

Add-Type -AssemblyName System.IO.Compression.FileSystem
foreach ($row in $rows) {
    Invoke-WebRequest -Uri $row.Uri -OutFile $row.PackagePath
    $archive = [System.IO.Compression.ZipFile]::OpenRead($row.PackagePath)
    try {
        $runtimePrefix = "runtimes/$Rid/native/"
        $runtimeEntries = @($archive.Entries | Where-Object {
            $_.FullName.StartsWith($runtimePrefix, [System.StringComparison]::Ordinal) -and
            -not $_.FullName.EndsWith('/', [System.StringComparison]::Ordinal)
        })
        if ($runtimeEntries.Count -eq 0) {
            throw "$($row.Package.Id) $($row.Package.PackageVersion) contains no $runtimePrefix entries."
        }

        foreach ($entry in $runtimeEntries) {
            $relativePath = $entry.FullName.Substring($runtimePrefix.Length).Replace('/', [System.IO.Path]::DirectorySeparatorChar)
            if ([System.IO.Path]::IsPathRooted($relativePath) -or @($relativePath -split '[\\/]') -contains '..') {
                throw "Unsafe runtime package entry: $($entry.FullName)"
            }

            $targetPath = [System.IO.Path]::GetFullPath((Join-Path $destinationPath $relativePath))
            if (-not $targetPath.StartsWith($destinationRoot, [System.StringComparison]::OrdinalIgnoreCase)) {
                throw "Runtime package entry escapes destination: $($entry.FullName)"
            }

            $entryHash = Get-ArchiveEntrySha256 -Entry $entry
            if ($writtenEntries.ContainsKey($targetPath)) {
                if ($writtenEntries[$targetPath] -ne $entryHash) {
                    throw "Published native packages contain conflicting runtime entry: $relativePath"
                }
                continue
            }

            New-Item -ItemType Directory -Force -Path (Split-Path -Parent $targetPath) | Out-Null
            $source = $entry.Open()
            try {
                $target = [System.IO.File]::Open($targetPath, [System.IO.FileMode]::Create, [System.IO.FileAccess]::Write, [System.IO.FileShare]::None)
                try {
                    $source.CopyTo($target)
                }
                finally {
                    $target.Dispose()
                }
            }
            finally {
                $source.Dispose()
            }

            $writtenEntries[$targetPath] = $entryHash
            $restoredCount++
        }
    }
    finally {
        $archive.Dispose()
    }
}

if ($Rid.StartsWith('win-', [System.StringComparison]::Ordinal) -and -not (Test-Path -LiteralPath (Join-Path $destinationPath 'SDL3.dll') -PathType Leaf)) {
    throw "Restored Windows test runtime is missing SDL3.dll in $destinationPath."
}

Write-Host "Restored $restoredCount native runtime file(s) from $($packages.Count) published package(s) for $Rid."
