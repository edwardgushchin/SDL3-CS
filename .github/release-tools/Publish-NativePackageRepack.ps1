#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][int] $PackageRevision,
    [Parameter(Mandatory)][int] $PreviousPackageRevision,
    [Parameter(Mandatory)][string[]] $PackageIds,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [switch] $NuGetPush,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir

& (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') `
    -PackageRevision $PackageRevision `
    -ManifestPath $ManifestPath
& (Join-Path $PSScriptRoot 'Test-NuGetPackageContents.ps1') `
    -PackageRevision $PackageRevision `
    -ManifestPath $ManifestPath `
    -PackageDir $PackageDir `
    -PackageIds $PackageIds
& (Join-Path $PSScriptRoot 'Test-NativePackageRepack.ps1') `
    -PackageRevision $PackageRevision `
    -PreviousPackageRevision $PreviousPackageRevision `
    -PackageIds $PackageIds `
    -ManifestPath $ManifestPath `
    -PackageDir $PackageDir

$allPackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
$packages = @()
foreach ($packageId in $PackageIds) {
    $matches = @($allPackages | Where-Object { $_.Id -eq $packageId -and $_.Kind -eq 'native' })
    if ($matches.Count -ne 1) {
        throw "Cannot publish unknown or non-native package id: $packageId"
    }
    $packages += $matches[0]
}

if (-not $NuGetPush) {
    Write-Host 'Selective native packages are validated. Pass -NuGetPush to publish them.'
    return
}
if (-not $DryRun -and [string]::IsNullOrWhiteSpace($env:NUGET_API_KEY)) {
    throw 'NUGET_API_KEY is required for -NuGetPush. In GitHub Actions it is provided by NuGet/login trusted publishing.'
}

foreach ($package in $packages) {
    $packagePath = Get-ReleaseNuGetPackagePath -PackageDir $PackageDir -Package $package
    if (-not (Test-Path -LiteralPath $packagePath -PathType Leaf)) {
        throw "Expected selective repack package is missing: $packagePath"
    }

    $args = @(
        'nuget', 'push', $packagePath,
        '--api-key', $env:NUGET_API_KEY,
        '--source', 'https://api.nuget.org/v3/index.json',
        '--timeout', '600'
    )
    Write-Host "Publishing $($package.Id) $($package.PackageVersion)"
    Invoke-ReleaseCommand -FilePath 'dotnet' -Arguments $args -DryRun:$DryRun
}

Write-Host "Selective native package publication completed for $($packages.Count) package(s)."
