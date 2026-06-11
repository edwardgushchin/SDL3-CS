#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $ImageNamePrefix = 'sdl3-cs-native-linux',
    [int] $BuildParallelLevel = 2,
    [switch] $SkipImageBuild,
    [switch] $SkipNativeBuild,
    [switch] $SkipBundleExport,
    [switch] $AllowDirtySources,
    [switch] $Clean,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-RidBuildHost {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    if ($RidInfo.PSObject.Properties.Name.Contains('buildHost') -and $RidInfo.buildHost) {
        return $RidInfo.buildHost
    }

    return $RidInfo.os
}

& (Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1') -ManifestPath $ManifestPath

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | Where-Object { (Get-RidBuildHost -RidInfo $_) -eq 'linux-container' } | ForEach-Object { $_.rid })
}

foreach ($componentId in $Components) {
    Get-ReleaseComponent -Manifest $manifest -Component $componentId | Out-Null
}

foreach ($rid in $Rids) {
    $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid
    if ((Get-RidBuildHost -RidInfo $ridInfo) -ne 'linux-container') {
        throw "RID '$rid' is not configured for Linux container builds."
    }
}

$docker = Get-ReleaseToolPath -Name 'docker'
if (-not $docker) {
    if ($DryRun) {
        $docker = 'docker'
    }
    else {
        throw "Docker CLI was not found. Install Docker before running Linux container native builds."
    }
}

$repoRoot = Get-ReleaseRepoRoot

foreach ($rid in $Rids) {
    $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid
    $dockerfile = Resolve-ReleasePath $ridInfo.dockerfile
    $dockerPlatform = $ridInfo.dockerPlatform
    $imageName = "$ImageNamePrefix-$($ridInfo.arch)"

    if (-not (Test-Path -LiteralPath $dockerfile -PathType Leaf)) {
        throw "Dockerfile for RID '$rid' was not found: $dockerfile"
    }

    if (-not $SkipImageBuild) {
        $buildArgs = @('build')
        if ($dockerPlatform) {
            $buildArgs += @('--platform', $dockerPlatform)
        }
        $buildArgs += @('-f', $dockerfile, '-t', $imageName, $repoRoot)
        Invoke-ReleaseCommand -FilePath $docker -Arguments $buildArgs -DryRun:$DryRun
    }

    $containerArgs = @(
        'run',
        '--rm'
    )
    if ($dockerPlatform) {
        $containerArgs += @('--platform', $dockerPlatform)
    }

    $containerArgs += @(
        '-v', "${repoRoot}:/work",
        '-w', '/work',
        '-e', "SDL3CS_NATIVE_BUILD_PARALLEL_LEVEL=$BuildParallelLevel",
        $imageName,
        'pwsh',
        '-NoProfile',
        '-File', '/work/eng/release/Invoke-NativeHostBuild.ps1',
        '-ManifestPath', '/work/eng/release/release-manifest.json',
        '-Rids', $rid,
        '-BuildParallelLevel', [string]$BuildParallelLevel
    )

    if ($Components -and $Components.Count -gt 0) {
        $containerArgs += '-Components'
        $containerArgs += $Components
    }
    if ($SkipNativeBuild) {
        $containerArgs += '-SkipNativeBuild'
    }
    if ($SkipBundleExport) {
        $containerArgs += '-SkipBundleExport'
    }
    if ($AllowDirtySources) {
        $containerArgs += '-AllowDirtySources'
    }
    if ($Clean) {
        $containerArgs += '-Clean'
    }

    Invoke-ReleaseCommand -FilePath $docker -Arguments $containerArgs -DryRun:$DryRun
}
