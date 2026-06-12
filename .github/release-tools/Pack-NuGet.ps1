#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string[]] $Rids,
    [string] $Configuration = 'Release',
    [string] $OutputDir,
    [switch] $NoBuild,
    [switch] $SkipNativeArtifactValidation,
    [switch] $SkipNativeBuildReceiptValidation,
    [switch] $SkipNuGetPackageContentValidation,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}
if (-not $OutputDir) {
    $OutputDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$OutputDir = Resolve-ReleasePath $OutputDir

Write-Host "Native RID validation scope: $($Rids -join ', ')"

& (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath

if (-not $DryRun) {
    New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null
}

if (-not $DryRun -and -not $SkipNativeArtifactValidation) {
    & (Join-Path $PSScriptRoot 'Test-NativePackageArtifacts.ps1') -ManifestPath $ManifestPath -Rids $Rids
}

if (-not $DryRun -and -not $SkipNativeBuildReceiptValidation) {
    & (Join-Path $PSScriptRoot 'Test-NativeBuildReceipts.ps1') -ManifestPath $ManifestPath -Rids $Rids
}

$packages = Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision
foreach ($package in $packages) {
    $projectPath = Resolve-ReleasePath $package.Project
    $expectedPackagePath = Join-Path $OutputDir "$($package.Id).$($package.PackageVersion).nupkg"
    $args = @(
        'pack', $projectPath,
        '-c', $Configuration,
        '-o', $OutputDir,
        "-p:PackageVersion=$($package.PackageVersion)",
        "-p:PackageId=$($package.Id)"
    )
    if ($package.Kind -eq 'native' -and $package.NativePackagePlatform) {
        $args += "-p:NativePackagePlatform=$($package.NativePackagePlatform)"
    }

    if ($NoBuild) {
        $args += '--no-build'
    }

    Write-Host "Packing $($package.Id) $($package.PackageVersion)"
    if (-not $DryRun -and (Test-Path -LiteralPath $expectedPackagePath -PathType Leaf)) {
        Remove-Item -LiteralPath $expectedPackagePath -Force
    }
    Invoke-ReleaseCommand -FilePath 'dotnet' -Arguments $args -DryRun:$DryRun
}

if (-not $DryRun -and -not $SkipNuGetPackageContentValidation) {
    & (Join-Path $PSScriptRoot 'Test-NuGetPackageContents.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -PackageDir $OutputDir `
        -Rids $Rids
}
