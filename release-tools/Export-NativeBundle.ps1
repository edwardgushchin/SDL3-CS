#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $OutputPath,
    [switch] $AllowDirtySources,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-ReleaseSelectionSlug {
    param(
        [Parameter(Mandatory)]
        [string[]] $Values,

        [Parameter(Mandatory)]
        [string[]] $AllValues,

        [Parameter(Mandatory)]
        [string] $AllSlug,

        [Parameter(Mandatory)]
        [string] $SelectedSlug
    )

    if ($Values.Count -eq $AllValues.Count) {
        $difference = @($Values | Where-Object { -not $AllValues.Contains($_) })
        if ($difference.Count -eq 0) {
            return $AllSlug
        }
    }

    if ($Values.Count -eq 1) {
        return $Values[0]
    }

    return $SelectedSlug
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$allComponents = @($manifest.components | ForEach-Object { $_.id })
$allRids = @($manifest.rids | ForEach-Object { $_.rid })

if (-not $Components -or $Components.Count -eq 0) {
    $Components = $allComponents
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = $allRids
}

foreach ($componentId in $Components) {
    Get-ReleaseComponent -Manifest $manifest -Component $componentId | Out-Null
}
foreach ($rid in $Rids) {
    Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
}

if (-not $OutputPath) {
    $componentSlug = Get-ReleaseSelectionSlug -Values $Components -AllValues $allComponents -AllSlug 'all-components' -SelectedSlug 'selected-components'
    $ridSlug = Get-ReleaseSelectionSlug -Values $Rids -AllValues $allRids -AllSlug 'all-rids' -SelectedSlug 'selected-rids'
    $OutputPath = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) "bundles/native-$componentSlug-$ridSlug.zip"
}
$OutputPath = Resolve-ReleasePath $OutputPath

if (-not $DryRun) {
    & (Join-Path $PSScriptRoot 'Test-NativeBuildReceipts.ps1') `
        -ManifestPath $ManifestPath `
        -Components $Components `
        -Rids $Rids `
        -AllowDirtySources:$AllowDirtySources
}

$repoRoot = Get-ReleaseRepoRoot
$bundleFiles = New-Object System.Collections.Generic.List[object]
$rows = New-Object System.Collections.Generic.List[object]

foreach ($componentId in $Components) {
    $componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $componentId
    $packageProject = Resolve-ReleasePath $componentInfo.packageProject
    $packageRoot = Split-Path -Parent $packageProject

    foreach ($rid in $Rids) {
        $receiptPath = Get-ReleaseNativeReceiptPath -Manifest $manifest -Component $componentId -Rid $rid
        if (-not (Test-Path -LiteralPath $receiptPath -PathType Leaf)) {
            throw "Native build receipt is required before export: $receiptPath"
        }

        $receipt = Get-Content -LiteralPath $receiptPath -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 32
        foreach ($artifact in @($receipt.Artifacts)) {
            $artifactPath = Join-Path $packageRoot $artifact.RelativePath
            if (-not (Test-Path -LiteralPath $artifactPath -PathType Leaf)) {
                throw "Receipt artifact is missing before export: $artifactPath"
            }

            $fileInfo = Get-Item -LiteralPath $artifactPath
            $sha256 = (Get-FileHash -LiteralPath $artifactPath -Algorithm SHA256).Hash.ToLowerInvariant()
            $bundleFiles.Add([pscustomobject]@{
                Component = $componentId
                Rid = $rid
                Kind = 'artifact'
                RelativePath = Get-ReleaseRepoRelativePath -Path $artifactPath
                Length = $fileInfo.Length
                Sha256 = $sha256
                SourcePath = $artifactPath
            })
        }

        $receiptInfo = Get-Item -LiteralPath $receiptPath
        $bundleFiles.Add([pscustomobject]@{
            Component = $componentId
            Rid = $rid
            Kind = 'receipt'
            RelativePath = Get-ReleaseRepoRelativePath -Path $receiptPath
            Length = $receiptInfo.Length
            Sha256 = (Get-FileHash -LiteralPath $receiptPath -Algorithm SHA256).Hash.ToLowerInvariant()
            SourcePath = $receiptPath
        })
    }
}

foreach ($file in $bundleFiles) {
    $rows.Add([pscustomobject]@{
        Component = $file.Component
        Rid = $file.Rid
        Kind = $file.Kind
        Length = $file.Length
        RelativePath = $file.RelativePath
    })
}

$rows | Sort-Object Component, Rid, Kind, RelativePath | Format-Table -AutoSize

if ($DryRun) {
    Write-Host "[dry-run] export $($bundleFiles.Count) file(s) to $OutputPath"
    return
}

$bundleRoot = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Force -Path $bundleRoot | Out-Null

$stagingRoot = Join-Path $bundleRoot "_staging/$([guid]::NewGuid().ToString('N'))"
$payloadRoot = Join-Path $stagingRoot 'payload'

try {
    New-Item -ItemType Directory -Force -Path $payloadRoot | Out-Null

    foreach ($file in $bundleFiles) {
        $target = Resolve-ReleaseSafeRelativePath -Root $payloadRoot -RelativePath $file.RelativePath
        $targetParent = Split-Path -Parent $target
        New-Item -ItemType Directory -Force -Path $targetParent | Out-Null
        Copy-Item -LiteralPath $file.SourcePath -Destination $target -Force
    }

    $bundleManifest = [pscustomobject]@{
        SchemaVersion = 1
        CreatedAtUtc = [DateTime]::UtcNow.ToString('o')
        RepositoryRoot = $repoRoot
        Components = $Components
        Rids = $Rids
        Files = @($bundleFiles | ForEach-Object {
            [pscustomobject]@{
                Component = $_.Component
                Rid = $_.Rid
                Kind = $_.Kind
                RelativePath = $_.RelativePath
                Length = $_.Length
                Sha256 = $_.Sha256
            }
        })
    }
    $bundleManifest | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath (Join-Path $stagingRoot 'bundle-manifest.json') -Encoding UTF8

    Compress-Archive -Path (Join-Path $stagingRoot '*') -DestinationPath $OutputPath -Force
    Write-Host "Exported native bundle: $OutputPath"
}
finally {
    if (Test-Path -LiteralPath $stagingRoot) {
        Remove-Item -LiteralPath $stagingRoot -Recurse -Force
    }
}
