#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $StatePath,
    [string[]] $Components,
    [string[]] $Rids
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Convert-StatePath {
    param(
        [AllowNull()]
        [string] $Path
    )

    if ([string]::IsNullOrWhiteSpace($Path)) {
        return $Path
    }

    $normalized = $Path.Replace('\', '/').TrimStart('/')
    while ($normalized.StartsWith('./', [System.StringComparison]::Ordinal)) {
        $normalized = $normalized.Substring(2)
    }

    return $normalized
}

function Test-SafeStatePath {
    param(
        [Parameter(Mandatory)]
        [string] $Path
    )

    if ([System.IO.Path]::IsPathRooted($Path)) {
        return $false
    }

    $parts = @($Path -split '/')
    return -not ($parts | Where-Object { $_ -eq '..' -or $_ -eq '' })
}

function Get-EntrySha256 {
    param(
        [Parameter(Mandatory)]
        [object] $Entry
    )

    if ($Entry.Kind -eq 'directory') {
        return (Get-FileHash -LiteralPath $Entry.PhysicalPath -Algorithm SHA256).Hash.ToLowerInvariant()
    }

    $sha256 = [System.Security.Cryptography.SHA256]::Create()
    try {
        $stream = $Entry.ZipEntry.Open()
        try {
            $hash = $sha256.ComputeHash($stream)
            return ([System.BitConverter]::ToString($hash) -replace '-', '').ToLowerInvariant()
        }
        finally {
            $stream.Dispose()
        }
    }
    finally {
        $sha256.Dispose()
    }
}

function Get-EntryText {
    param(
        [Parameter(Mandatory)]
        [object] $Entry
    )

    if ($Entry.Kind -eq 'directory') {
        return Get-Content -LiteralPath $Entry.PhysicalPath -Raw -Encoding UTF8
    }

    $stream = $Entry.ZipEntry.Open()
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

function Add-StateError {
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
if (-not $StatePath) {
    $StatePath = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'release-assembly-state.zip'
}
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

$stateFile = Resolve-ReleasePath $StatePath
$stateIsZip = Test-Path -LiteralPath $stateFile -PathType Leaf
$stateIsDirectory = Test-Path -LiteralPath $stateFile -PathType Container
if (-not $stateIsZip -and -not $stateIsDirectory) {
    throw "Release assembly state path was not found: $stateFile"
}

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]
$entries = [System.Collections.Generic.Dictionary[string, object]]::new([System.StringComparer]::Ordinal)
$zip = $null

try {
    if ($stateIsZip) {
        Add-Type -AssemblyName System.IO.Compression.FileSystem
        $zip = [System.IO.Compression.ZipFile]::OpenRead($stateFile)
        foreach ($zipEntry in $zip.Entries) {
            if ([string]::IsNullOrWhiteSpace($zipEntry.FullName) -or $zipEntry.FullName.EndsWith('/', [System.StringComparison]::Ordinal)) {
                continue
            }

            $relativePath = Convert-StatePath $zipEntry.FullName
            if (-not (Test-SafeStatePath -Path $relativePath)) {
                Add-StateError "Release assembly state zip contains unsafe path: $($zipEntry.FullName)"
                continue
            }
            if ($entries.ContainsKey($relativePath)) {
                Add-StateError "Release assembly state zip contains duplicate path: $relativePath"
                continue
            }

            $entries[$relativePath] = [pscustomobject]@{
                Kind = 'zip'
                RelativePath = $relativePath
                Length = [int64] $zipEntry.Length
                ZipEntry = $zipEntry
            }
        }
    }
    else {
        foreach ($file in Get-ChildItem -LiteralPath $stateFile -File -Recurse) {
            $relativePath = Convert-StatePath ([System.IO.Path]::GetRelativePath($stateFile, $file.FullName))
            if (-not (Test-SafeStatePath -Path $relativePath)) {
                Add-StateError "Release assembly state directory contains unsafe path: $relativePath"
                continue
            }
            if ($entries.ContainsKey($relativePath)) {
                Add-StateError "Release assembly state directory contains duplicate path: $relativePath"
                continue
            }

            $entries[$relativePath] = [pscustomobject]@{
                Kind = 'directory'
                RelativePath = $relativePath
                Length = [int64] $file.Length
                PhysicalPath = $file.FullName
            }
        }
    }

    $packages = Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision
    $expectedPackageEntries = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    foreach ($package in $packages) {
        [void] $expectedPackageEntries.Add("artifacts/release/nuget/$(Get-ReleaseNuGetPackageFileName -Package $package)")
    }

    $actualPackageEntries = @($entries.Keys | Where-Object {
        $_.StartsWith('artifacts/release/nuget/', [System.StringComparison]::Ordinal) -and
        $_.EndsWith('.nupkg', [System.StringComparison]::Ordinal) -and
        (($_ -split '/').Count -eq 4)
    })

    foreach ($expectedPackageEntry in @($expectedPackageEntries | Sort-Object)) {
        $status = if ($entries.ContainsKey($expectedPackageEntry)) { 'present' } else { 'missing' }
        if ($status -eq 'missing') {
            Add-StateError "Release assembly state is missing expected NuGet package: $expectedPackageEntry"
        }

        $rows.Add([pscustomobject]@{
            Scope = 'package'
            Component = ''
            Rid = ''
            Path = $expectedPackageEntry
            Count = if ($status -eq 'present') { 1 } else { 0 }
            Status = $status
        })
    }

    foreach ($actualPackageEntry in $actualPackageEntries) {
        if (-not $expectedPackageEntries.Contains($actualPackageEntry)) {
            Add-StateError "Release assembly state contains unexpected NuGet package: $actualPackageEntry"
        }
    }

    $expectedReceiptEntries = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    foreach ($componentId in $Components) {
        $component = Get-ReleaseComponent -Manifest $manifest -Component $componentId

        foreach ($rid in $Rids) {
            $packageProject = Convert-StatePath (Get-ReleaseNativePackageProjectForRid -Manifest $manifest -Component $component -Rid $rid)
            $packageRoot = Convert-StatePath (Split-Path -Parent $packageProject)
            $expectedPackageId = Get-ReleaseNativePackageIdForRid -Manifest $manifest -Component $component -Rid $rid
            $receiptEntry = "artifacts/release/receipts/$componentId/$rid.json"
            [void] $expectedReceiptEntries.Add($receiptEntry)
            if (-not $entries.ContainsKey($receiptEntry)) {
                Add-StateError "Release assembly state is missing receipt: $receiptEntry"
                $rows.Add([pscustomobject]@{
                    Scope = 'receipt'
                    Component = $componentId
                    Rid = $rid
                    Path = $receiptEntry
                    Count = 0
                    Status = 'missing'
                })
                continue
            }

            $status = 'passed'
            $receipt = $null
            try {
                $receipt = Get-EntryText -Entry $entries[$receiptEntry] | ConvertFrom-Json -Depth 32
            }
            catch {
                Add-StateError "Release assembly state receipt is not valid JSON: $receiptEntry"
                $status = 'failed'
            }

            $receiptArtifactEntries = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
            if ($receipt) {
                if (-not $receipt.PSObject.Properties.Name.Contains('SchemaVersion') -or [int] $receipt.SchemaVersion -lt 2) {
                    Add-StateError "Release assembly state receipt has unsupported schema for $componentId/$rid`: $receiptEntry"
                    $status = 'failed'
                }
                if ($receipt.Component -ne $componentId) {
                    Add-StateError "Release assembly state receipt component mismatch for $componentId/$rid`: $($receipt.Component)"
                    $status = 'failed'
                }
                if ($receipt.Rid -ne $rid) {
                    Add-StateError "Release assembly state receipt RID mismatch for $componentId/$rid`: $($receipt.Rid)"
                    $status = 'failed'
                }
                if ($receipt.PackageId -ne $expectedPackageId) {
                    Add-StateError "Release assembly state receipt package id mismatch for $componentId/$rid`: $($receipt.PackageId)"
                    $status = 'failed'
                }

                $artifacts = @($receipt.Artifacts)
                if ($artifacts.Count -eq 0) {
                    Add-StateError "Release assembly state receipt has no artifacts for $componentId/$rid`: $receiptEntry"
                    $status = 'failed'
                }

                foreach ($artifact in $artifacts) {
                    $artifactRelativePath = Convert-StatePath $artifact.RelativePath
                    if ([string]::IsNullOrWhiteSpace($artifactRelativePath) -or -not (Test-SafeStatePath -Path $artifactRelativePath)) {
                        Add-StateError "Release assembly state receipt has unsafe artifact path for $componentId/$rid`: $($artifact.RelativePath)"
                        $status = 'failed'
                        continue
                    }
                    if (-not $artifactRelativePath.StartsWith("lib/$rid/", [System.StringComparison]::Ordinal)) {
                        Add-StateError "Release assembly state receipt artifact is outside selected RID folder for $componentId/$rid`: $artifactRelativePath"
                        $status = 'failed'
                    }

                    $stateArtifactEntry = "$packageRoot/$artifactRelativePath"
                    [void] $receiptArtifactEntries.Add($stateArtifactEntry)
                    if (-not $entries.ContainsKey($stateArtifactEntry)) {
                        Add-StateError "Release assembly state is missing artifact referenced by receipt $componentId/$rid`: $stateArtifactEntry"
                        $status = 'failed'
                        continue
                    }

                    $stateEntry = $entries[$stateArtifactEntry]
                    if ($stateEntry.Length -ne [int64] $artifact.Length) {
                        Add-StateError "Release assembly state artifact length mismatch for $componentId/$rid`: $stateArtifactEntry"
                        $status = 'failed'
                    }
                    if ([string]::IsNullOrWhiteSpace($artifact.Sha256)) {
                        Add-StateError "Release assembly state receipt artifact has no SHA256 for $componentId/$rid`: $stateArtifactEntry"
                        $status = 'failed'
                    }
                    else {
                        $actualSha256 = Get-EntrySha256 -Entry $stateEntry
                        if ($actualSha256 -ne $artifact.Sha256) {
                            Add-StateError "Release assembly state artifact SHA256 mismatch for $componentId/$rid`: $stateArtifactEntry"
                            $status = 'failed'
                        }
                    }
                }
            }

            $ridPrefix = "$packageRoot/lib/$rid/"
            $actualRidEntries = @($entries.Keys | Where-Object { $_.StartsWith($ridPrefix, [System.StringComparison]::Ordinal) })
            foreach ($actualRidEntry in $actualRidEntries) {
                if (-not $receiptArtifactEntries.Contains($actualRidEntry)) {
                    Add-StateError "Release assembly state contains artifact not backed by receipt $componentId/$rid`: $actualRidEntry"
                    $status = 'failed'
                }
            }

            $rows.Add([pscustomobject]@{
                Scope = 'component/rid'
                Component = $componentId
                Rid = $rid
                Path = $receiptEntry
                Count = $actualRidEntries.Count
                Status = $status
            })
        }
    }

    $actualReceiptEntries = @($entries.Keys | Where-Object {
        $_.StartsWith('artifacts/release/receipts/', [System.StringComparison]::Ordinal) -and
        $_.EndsWith('.json', [System.StringComparison]::Ordinal)
    })
    foreach ($actualReceiptEntry in $actualReceiptEntries) {
        if (-not $expectedReceiptEntries.Contains($actualReceiptEntry)) {
            Add-StateError "Release assembly state contains unexpected receipt: $actualReceiptEntry"
        }
    }

    $rows | Sort-Object Scope, Component, Rid, Path | Format-Table -AutoSize

    if ($errors.Count -gt 0) {
        $errors | ForEach-Object { Write-Error $_ }
        throw "Release assembly state validation failed with $($errors.Count) error(s)."
    }

    Write-Host "Release assembly state is valid: $stateFile"
}
finally {
    if ($zip) {
        $zip.Dispose()
    }
}
