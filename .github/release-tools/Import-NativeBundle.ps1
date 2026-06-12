#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $BundlePath,

    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $AllowDirtySources,
    [switch] $AllowEmptySelection,
    [switch] $SkipPostImportValidation,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Assert-ReleaseBundleFileTarget {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [object] $File
    )

    Get-ReleaseRid -Manifest $Manifest -Rid $File.Rid | Out-Null
    $componentInfo = Get-ReleaseComponent -Manifest $Manifest -Component $File.Component
    $repoRoot = Get-ReleaseRepoRoot

    $targetPath = Resolve-ReleaseSafeRelativePath -Root $repoRoot -RelativePath $File.RelativePath

    if ($File.Kind -eq 'artifact') {
        $packageProject = Resolve-ReleasePath (Get-ReleaseNativePackageProjectForRid -Manifest $Manifest -Component $componentInfo -Rid $File.Rid)
        $packageRoot = Split-Path -Parent $packageProject
        $ridRoot = [System.IO.Path]::GetFullPath((Join-Path $packageRoot "lib/$($File.Rid)"))
        $ridRootPrefix = $ridRoot.TrimEnd([System.IO.Path]::DirectorySeparatorChar, [System.IO.Path]::AltDirectorySeparatorChar) + [System.IO.Path]::DirectorySeparatorChar

        if (-not $targetPath.StartsWith($ridRootPrefix, [System.StringComparison]::OrdinalIgnoreCase)) {
            throw "Bundle artifact target is outside expected RID folder for $($File.Component)/$($File.Rid): $($File.RelativePath)"
        }
    }
    elseif ($File.Kind -eq 'receipt') {
        $expectedReceipt = Get-ReleaseNativeReceiptPath -Manifest $Manifest -Component $File.Component -Rid $File.Rid
        if ($targetPath -ne [System.IO.Path]::GetFullPath($expectedReceipt)) {
            throw "Bundle receipt target does not match expected receipt path for $($File.Component)/$($File.Rid): $($File.RelativePath)"
        }
    }
    else {
        throw "Unsupported bundle file kind: $($File.Kind)"
    }

    return $targetPath
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$BundlePath = Resolve-ReleasePath $BundlePath
if (-not (Test-Path -LiteralPath $BundlePath -PathType Leaf)) {
    throw "Native bundle was not found: $BundlePath"
}

$artifactsRoot = Resolve-ReleasePath $manifest.artifactsRoot
$importRoot = Join-Path $artifactsRoot "bundles/_import/$([guid]::NewGuid().ToString('N'))"
$payloadRoot = Join-Path $importRoot 'payload'

try {
    New-Item -ItemType Directory -Force -Path $importRoot | Out-Null
    Expand-Archive -LiteralPath $BundlePath -DestinationPath $importRoot -Force

    $bundleManifestPath = Join-Path $importRoot 'bundle-manifest.json'
    if (-not (Test-Path -LiteralPath $bundleManifestPath -PathType Leaf)) {
        throw "Bundle manifest was not found in $BundlePath."
    }

    $bundleManifest = Get-Content -LiteralPath $bundleManifestPath -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 32
    if ($bundleManifest.SchemaVersion -ne 1) {
        throw "Unsupported native bundle schema version: $($bundleManifest.SchemaVersion)"
    }

    $selectedFiles = @($bundleManifest.Files | Where-Object {
        (-not $Components -or $Components.Contains($_.Component)) -and
        (-not $Rids -or $Rids.Contains($_.Rid))
    })

    if ($selectedFiles.Count -eq 0) {
        if ($AllowEmptySelection) {
            Write-Host "Native bundle has no files for the requested component/RID selection; skipped: $BundlePath"
            return
        }

        throw "No bundle files matched the requested component/RID selection."
    }

    foreach ($componentId in @($selectedFiles | Select-Object -ExpandProperty Component -Unique)) {
        Get-ReleaseComponent -Manifest $manifest -Component $componentId | Out-Null
    }
    foreach ($rid in @($selectedFiles | Select-Object -ExpandProperty Rid -Unique)) {
        Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
    }

    $copyPlan = New-Object System.Collections.Generic.List[object]
    foreach ($file in $selectedFiles) {
        $safeRelativePath = Assert-ReleaseSafeRelativePath -RelativePath $file.RelativePath
        $sourcePath = Resolve-ReleaseSafeRelativePath -Root $payloadRoot -RelativePath $safeRelativePath
        if (-not (Test-Path -LiteralPath $sourcePath -PathType Leaf)) {
            throw "Bundle payload file is missing: $safeRelativePath"
        }

        $sourceInfo = Get-Item -LiteralPath $sourcePath
        if ($sourceInfo.Length -ne [int64]$file.Length) {
            throw "Bundle payload length mismatch: $safeRelativePath"
        }

        $sha256 = (Get-FileHash -LiteralPath $sourcePath -Algorithm SHA256).Hash.ToLowerInvariant()
        if ($sha256 -ne $file.Sha256) {
            throw "Bundle payload hash mismatch: $safeRelativePath"
        }

        $targetPath = Assert-ReleaseBundleFileTarget -Manifest $manifest -File $file
        $copyPlan.Add([pscustomobject]@{
            Component = $file.Component
            Rid = $file.Rid
            Kind = $file.Kind
            SourcePath = $sourcePath
            TargetPath = $targetPath
            RelativePath = $safeRelativePath
            Length = $file.Length
        })
    }

    $copyPlan | Sort-Object Component, Rid, Kind, RelativePath |
        Select-Object Component, Rid, Kind, Length, RelativePath |
        Format-Table -AutoSize

    $artifactGroups = @($copyPlan | Where-Object { $_.Kind -eq 'artifact' } | Group-Object Component, Rid)
    foreach ($group in $artifactGroups) {
        $first = $group.Group[0]
        $componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $first.Component
        $packageProject = Resolve-ReleasePath (Get-ReleaseNativePackageProjectForRid -Manifest $manifest -Component $componentInfo -Rid $first.Rid)
        $packageRoot = Split-Path -Parent $packageProject
        $ridRoot = Join-Path $packageRoot "lib/$($first.Rid)"

        if ($DryRun) {
            Write-Host "[dry-run] clean $ridRoot"
            continue
        }

        New-Item -ItemType Directory -Force -Path $ridRoot | Out-Null
        Get-ChildItem -LiteralPath $ridRoot -Force | Remove-Item -Recurse -Force
    }

    foreach ($item in $copyPlan) {
        if ($DryRun) {
            Write-Host "[dry-run] copy $($item.SourcePath) -> $($item.TargetPath)"
            continue
        }

        New-Item -ItemType Directory -Force -Path (Split-Path -Parent $item.TargetPath) | Out-Null
        Copy-Item -LiteralPath $item.SourcePath -Destination $item.TargetPath -Force
    }

    if (-not $DryRun -and -not $SkipPostImportValidation) {
        $importedComponents = @($copyPlan | Select-Object -ExpandProperty Component -Unique)
        $importedRids = @($copyPlan | Select-Object -ExpandProperty Rid -Unique)
        & (Join-Path $PSScriptRoot 'Test-NativeBuildReceipts.ps1') `
            -ManifestPath $ManifestPath `
            -Components $importedComponents `
            -Rids $importedRids `
            -AllowDirtySources:$AllowDirtySources
    }

    if ($DryRun) {
        Write-Host "[dry-run] import native bundle $BundlePath"
    }
    else {
        Write-Host "Imported native bundle: $BundlePath"
    }
}
finally {
    if (Test-Path -LiteralPath $importRoot) {
        Remove-Item -LiteralPath $importRoot -Recurse -Force
    }
}
