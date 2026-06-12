#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $BundleOutputPath,
    [int] $BuildParallelLevel,
    [switch] $SkipNativeBuild,
    [switch] $SkipBundleExport,
    [switch] $AllowCrossCompile,
    [switch] $AllowDirtySources,
    [switch] $Clean,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

& (Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1') -ManifestPath $ManifestPath

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$allComponents = @($manifest.components | ForEach-Object { $_.id })

if (-not $Components -or $Components.Count -eq 0) {
    $Components = $allComponents
}

foreach ($componentId in $Components) {
    Get-ReleaseComponent -Manifest $manifest -Component $componentId | Out-Null
}

if (-not $Rids -or $Rids.Count -eq 0) {
    $hostOs = Get-ReleaseHostOs
    $hostArch = Get-ReleaseHostArch
    if ($AllowCrossCompile) {
        $Rids = @($manifest.rids | Where-Object { $_.os -eq $hostOs } | ForEach-Object { $_.rid })
    }
    else {
        $Rids = @($manifest.rids | Where-Object { $_.os -eq $hostOs -and $_.arch -eq $hostArch } | ForEach-Object { $_.rid })
    }
}

if (-not $Rids -or $Rids.Count -eq 0) {
    throw "No release RID matches current host. Pass -Rids explicitly or use a supported host."
}

foreach ($rid in $Rids) {
    Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
}

$planRows = New-Object System.Collections.Generic.List[object]
foreach ($rid in $Rids) {
    $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid
    foreach ($componentId in $Components) {
        $planRows.Add([pscustomobject]@{
            Component = $componentId
            Rid = $rid
            HostOs = Get-ReleaseHostOs
            HostArch = Get-ReleaseHostArch
            TargetOs = $ridInfo.os
            TargetArch = $ridInfo.arch
        })
    }
}

Write-Host "Native host build plan:"
$planRows | Format-Table -AutoSize

foreach ($rid in $Rids) {
    if ($DryRun) {
        Write-Host "[dry-run] preflight release tools for $rid"
    }
    else {
        & (Join-Path $PSScriptRoot 'Test-ReleaseTools.ps1') -Rid $rid -ManifestPath $ManifestPath -AllowCrossCompile:$AllowCrossCompile
    }
}

if (-not $SkipNativeBuild) {
    foreach ($rid in $Rids) {
        foreach ($componentId in $Components) {
            & (Join-Path $PSScriptRoot 'Build-Native.ps1') `
                -Component $componentId `
                -Rid $rid `
                -ManifestPath $ManifestPath `
                -BuildParallelLevel $BuildParallelLevel `
                -AllowCrossCompile:$AllowCrossCompile `
                -Clean:$Clean `
                -DryRun:$DryRun
        }
    }
}

if (-not $SkipBundleExport) {
    if ($DryRun) {
        $exportArgs = @(
            '-ManifestPath', $ManifestPath,
            '-Components', "[$($Components -join ', ')]",
            '-Rids', "[$($Rids -join ', ')]"
        )
        if ($BundleOutputPath) {
            $exportArgs += @('-OutputPath', $BundleOutputPath)
        }
        if ($AllowDirtySources) {
            $exportArgs += '-AllowDirtySources'
        }
        Write-Host "[dry-run] Export-NativeBundle.ps1 $($exportArgs -join ' ')"
    }
    else {
        $exportParameters = @{
            ManifestPath = $ManifestPath
            Components = $Components
            Rids = $Rids
            AllowDirtySources = $AllowDirtySources
        }
        if ($BundleOutputPath) {
            $exportParameters.OutputPath = $BundleOutputPath
        }

        & (Join-Path $PSScriptRoot 'Export-NativeBundle.ps1') @exportParameters
    }
}
