#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string[]] $BundlePath,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string[]] $Rids,
    [string] $PackageDir,
    [string] $Repository = 'edwardgushchin/SDL3-CS',
    [switch] $SkipBundleImport,
    [switch] $SkipReadinessValidation,
    [switch] $SkipPack,
    [switch] $NoBuild,
    [switch] $GitHubRelease,
    [switch] $NuGetPush,
    [switch] $PublishDryRun,
    [switch] $AllowDirtySources,
    [switch] $RequireForksUpToDate,
    [switch] $CheckUpstream,
    [switch] $RequireUpstreamCurrent,
    [switch] $ValidateBundleSetInDryRun,
    [switch] $ValidateReadinessInDryRun,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

& (Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1') -ManifestPath $ManifestPath

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir

$allRids = @($manifest.rids | ForEach-Object { $_.rid })
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = $allRids
}
$Rids = @($Rids)
$uniqueRids = @($Rids | Select-Object -Unique)
if ($Rids.Count -ne $uniqueRids.Count) {
    throw "Rids must be unique: $($Rids -join ', ')"
}
foreach ($rid in $Rids) {
    Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
}
$selectedRidSet = @($Rids | Sort-Object)
$allRidSet = @($allRids | Sort-Object)
$ridScopeDiff = @(Compare-Object -ReferenceObject $allRidSet -DifferenceObject $selectedRidSet)
$usesFullRidScope = $selectedRidSet.Count -eq $allRidSet.Count -and $ridScopeDiff.Count -eq 0

if ($RequireUpstreamCurrent) {
    $CheckUpstream = $true
}
if (($PublishDryRun -or $GitHubRelease -or $NuGetPush) -and -not $usesFullRidScope) {
    throw "Publish and publish dry-run require the full manifest RID scope. Current scope: $($Rids -join ', ')."
}

Write-Host "Release assembly package versions:"
Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision | Sort-Object Kind, Id | Format-Table -AutoSize
Write-Host "Release assembly RID scope: $($Rids -join ', ')"

if (-not $SkipBundleImport) {
    if (-not $BundlePath -or $BundlePath.Count -eq 0) {
        throw "BundlePath is required unless -SkipBundleImport is passed."
    }

    if (-not $DryRun -or $ValidateBundleSetInDryRun) {
        & (Join-Path $PSScriptRoot 'Test-NativeBundleSet.ps1') `
            -BundlePath $BundlePath `
            -ManifestPath $ManifestPath `
            -Components @($manifest.components | ForEach-Object { $_.id }) `
            -Rids $Rids
    }
    else {
        Write-Host "[dry-run] native bundle set validation skipped. Pass -ValidateBundleSetInDryRun to inspect bundle payloads during dry-run."
    }

    foreach ($path in $BundlePath) {
        & (Join-Path $PSScriptRoot 'Import-NativeBundle.ps1') `
            -BundlePath $path `
            -ManifestPath $ManifestPath `
            -Components @($manifest.components | ForEach-Object { $_.id }) `
            -Rids $Rids `
            -AllowEmptySelection `
            -AllowDirtySources:$AllowDirtySources `
            -DryRun:$DryRun
    }
}

if (-not $SkipReadinessValidation -and (-not $DryRun -or $ValidateReadinessInDryRun)) {
    & (Join-Path $PSScriptRoot 'Test-ReleaseReadiness.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -Rids $Rids `
        -FetchForks:$RequireForksUpToDate `
        -RequireForksUpToDate:$RequireForksUpToDate `
        -CheckUpstream:$CheckUpstream `
        -RequireUpstreamCurrent:$RequireUpstreamCurrent `
        -SkipToolchainValidation `
        -FailOnError
}
elseif ($DryRun -and -not $ValidateReadinessInDryRun) {
    Write-Host "[dry-run] release readiness validation skipped. Pass -ValidateReadinessInDryRun to run it during dry-run."
}
else {
    Write-Host "Release readiness validation skipped."
}

if (-not $SkipPack) {
    & (Join-Path $PSScriptRoot 'Pack-NuGet.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -Rids $Rids `
        -OutputDir $PackageDir `
        -NoBuild:$NoBuild `
        -DryRun:$DryRun
}

if ($PublishDryRun -or $GitHubRelease -or $NuGetPush) {
    $publishIsDryRun = $DryRun -or $PublishDryRun
    $publishGitHub = $GitHubRelease -or $PublishDryRun
    $publishNuGet = $NuGetPush -or $PublishDryRun

    & (Join-Path $PSScriptRoot 'Publish-Release.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -PackageDir $PackageDir `
        -Repository $Repository `
        -GitHubRelease:$publishGitHub `
        -NuGetPush:$publishNuGet `
        -RequireUpstreamCurrent:$RequireUpstreamCurrent `
        -DryRun:$publishIsDryRun
}
