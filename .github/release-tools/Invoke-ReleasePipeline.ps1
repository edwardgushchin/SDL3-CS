#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $SkipNativeBuild,
    [switch] $SkipPack,
    [switch] $AllowCrossCompile,
    [switch] $Clean,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

& (Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1') -ManifestPath $ManifestPath

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

Write-Host "Release package versions:"
Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision | Sort-Object Kind, Id | Format-Table -AutoSize

if (-not $SkipNativeBuild) {
    foreach ($rid in $Rids) {
        foreach ($component in $Components) {
            & (Join-Path $PSScriptRoot 'Build-Native.ps1') -Component $component -Rid $rid -ManifestPath $ManifestPath -AllowCrossCompile:$AllowCrossCompile -Clean:$Clean -DryRun:$DryRun
        }
    }
}

if (-not $SkipPack) {
    & (Join-Path $PSScriptRoot 'Pack-NuGet.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath -Rids $Rids -DryRun:$DryRun
}
