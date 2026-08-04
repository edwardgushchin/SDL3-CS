#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $NativePackageRevision = 1,
    [string] $Rid = 'win-x64',
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $Destination = 'SDL3-CS.Tests/bin/Release/net8.0',
    [string] $PackageCacheDir = 'artifacts/release/managed-test-runtime',
    [switch] $UseTrackedPayload,
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
    $sourceDescription = if ($UseTrackedPayload) { 'tracked' } else { 'published' }
    Write-Host "[dry-run] Would restore $($packages.Count) $sourceDescription native package payload(s) into $destinationPath."
    return
}

New-Item -ItemType Directory -Force -Path $destinationPath | Out-Null
$destinationRoot = [System.IO.Path]::GetFullPath($destinationPath).TrimEnd([System.IO.Path]::DirectorySeparatorChar) + [System.IO.Path]::DirectorySeparatorChar
$writtenEntries = [System.Collections.Generic.Dictionary[string, string]]::new([System.StringComparer]::OrdinalIgnoreCase)
$restoredCount = 0

if ($UseTrackedPayload) {
    $repoRoot = (Resolve-Path -LiteralPath (Get-ReleaseRepoRoot)).Path
    foreach ($row in $rows) {
        $projectRelativePath = Assert-ReleaseSafeRelativePath -RelativePath ([string]$row.Package.Project)
        $projectDirectory = Split-Path -Parent $projectRelativePath
        $runtimeRelativePath = Assert-ReleaseSafeRelativePath -RelativePath ((Join-Path $projectDirectory "lib/$Rid").Replace('\', '/'))
        $runtimePath = Resolve-ReleaseSafeRelativePath -Root $repoRoot -RelativePath $runtimeRelativePath
        if (-not (Test-Path -LiteralPath $runtimePath -PathType Container)) {
            throw "Tracked package $($row.Package.Id) contains no lib/$Rid payload: $runtimePath"
        }

        $runtimeRootItem = Get-Item -LiteralPath $runtimePath -Force
        if (($runtimeRootItem.Attributes -band [System.IO.FileAttributes]::ReparsePoint) -ne 0) {
            throw "Tracked package $($row.Package.Id) runtime root must not be a reparse point: $runtimePath"
        }

        $runtimeEntries = @(Get-ChildItem -LiteralPath $runtimePath -Recurse -Force)
        $reparseEntry = @($runtimeEntries | Where-Object {
            ($_.Attributes -band [System.IO.FileAttributes]::ReparsePoint) -ne 0
        } | Select-Object -First 1)
        if ($reparseEntry.Count -ne 0) {
            throw "Tracked package $($row.Package.Id) contains a reparse point: $($reparseEntry[0].FullName)"
        }

        $trackedFileRows = @(& git -C $repoRoot ls-files --stage -- $runtimeRelativePath 2>&1)
        if ($LASTEXITCODE -ne 0) {
            throw "Unable to enumerate tracked payload for $($row.Package.Id): $($trackedFileRows -join [Environment]::NewLine)"
        }

        $trackedPaths = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
        $runtimePrefix = $runtimeRelativePath.TrimEnd('/') + '/'
        foreach ($trackedFileRow in $trackedFileRows) {
            $match = [regex]::Match([string]$trackedFileRow, '^(?<mode>[0-9]{6}) [0-9a-fA-F]{40,64} (?<stage>[0-3])\t(?<path>.+)$')
            if (-not $match.Success -or $match.Groups['stage'].Value -ne '0') {
                throw "Unable to parse tracked payload index entry for $($row.Package.Id): $trackedFileRow"
            }
            if ($match.Groups['mode'].Value -notin @('100644', '100755')) {
                throw "Tracked package $($row.Package.Id) contains a non-regular Git entry (mode $($match.Groups['mode'].Value)): $($match.Groups['path'].Value)"
            }

            $trackedFile = $match.Groups['path'].Value
            $safeTrackedFile = Assert-ReleaseSafeRelativePath -RelativePath ([string]$trackedFile)
            if (-not $safeTrackedFile.StartsWith($runtimePrefix, [System.StringComparison]::Ordinal)) {
                throw "Tracked payload path escapes lib/$Rid for $($row.Package.Id): $safeTrackedFile"
            }

            $trackedPath = Resolve-ReleaseSafeRelativePath -Root $repoRoot -RelativePath $safeTrackedFile
            if (-not (Test-Path -LiteralPath $trackedPath -PathType Leaf)) {
                throw "Tracked payload file is missing for $($row.Package.Id): $safeTrackedFile"
            }
            $trackedItem = Get-Item -LiteralPath $trackedPath -Force
            if (($trackedItem.Attributes -band [System.IO.FileAttributes]::ReparsePoint) -ne 0) {
                throw "Tracked package $($row.Package.Id) contains a reparse point: $safeTrackedFile"
            }
            [void]$trackedPaths.Add($trackedItem.FullName)
        }

        $runtimeFiles = @($runtimeEntries | Where-Object { -not $_.PSIsContainer } | Sort-Object FullName)
        if ($runtimeFiles.Count -eq 0) {
            throw "Tracked package $($row.Package.Id) contains no files under lib/$Rid."
        }
        foreach ($runtimeFile in $runtimeFiles) {
            if (-not $trackedPaths.Contains($runtimeFile.FullName)) {
                throw "Tracked package $($row.Package.Id) contains an untracked or stale runtime file: $($runtimeFile.FullName)"
            }
        }
        if ($trackedPaths.Count -ne $runtimeFiles.Count) {
            throw "Tracked package $($row.Package.Id) inventory mismatch under lib/$Rid."
        }

        foreach ($runtimeFile in $runtimeFiles) {
            $relativePath = [System.IO.Path]::GetRelativePath($runtimePath, $runtimeFile.FullName)
            if ([System.IO.Path]::IsPathRooted($relativePath) -or @($relativePath -split '[\\/]') -contains '..') {
                throw "Unsafe tracked runtime path: $($runtimeFile.FullName)"
            }

            $targetPath = [System.IO.Path]::GetFullPath((Join-Path $destinationPath $relativePath))
            if (-not $targetPath.StartsWith($destinationRoot, [System.StringComparison]::OrdinalIgnoreCase)) {
                throw "Tracked runtime path escapes destination: $($runtimeFile.FullName)"
            }

            $entryHash = (Get-FileHash -LiteralPath $runtimeFile.FullName -Algorithm SHA256).Hash
            if ($writtenEntries.ContainsKey($targetPath)) {
                if ($writtenEntries[$targetPath] -ne $entryHash) {
                    throw "Tracked native packages contain conflicting runtime entry: $relativePath"
                }
                continue
            }

            New-Item -ItemType Directory -Force -Path (Split-Path -Parent $targetPath) | Out-Null
            [System.IO.File]::Copy($runtimeFile.FullName, $targetPath, $true)
            $writtenEntries[$targetPath] = $entryHash
            $restoredCount++
        }
    }

    if ($Rid.StartsWith('win-', [System.StringComparison]::Ordinal) -and -not (Test-Path -LiteralPath (Join-Path $destinationPath 'SDL3.dll') -PathType Leaf)) {
        throw "Tracked Windows test runtime is missing SDL3.dll in $destinationPath."
    }

    Write-Host "Restored $restoredCount native runtime file(s) from $($packages.Count) tracked package payload(s) for $Rid."
    return
}

New-Item -ItemType Directory -Force -Path $cachePath | Out-Null

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
