#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string] $Repository = 'edwardgushchin/SDL3-CS',
    [switch] $GitHubRelease,
    [switch] $NuGetPush,
    [switch] $ValidateReadinessInDryRun,
    [switch] $SkipReadinessValidation,
    [switch] $SkipPublishStateValidation,
    [switch] $SkipExternalPublishStateCheck,
    [switch] $AllowExistingGitHubRelease,
    [switch] $AllowExistingNuGetPackages,
    [switch] $KeepGitHubReleaseDraft,
    [switch] $RequireUpstreamCurrent,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir

& (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath

$packages = Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision
$wrapper = @($packages | Where-Object { $_.Id -eq 'SDL3-CS' })[0]
$tag = "v$($wrapper.PackageVersion)"

$expectedPackagePaths = @($packages | ForEach-Object {
    Join-Path $PackageDir "$($_.Id).$($_.PackageVersion).nupkg"
})
$missingPackagePaths = @($expectedPackagePaths | Where-Object { -not (Test-Path -LiteralPath $_ -PathType Leaf) })

if ($missingPackagePaths.Count -gt 0) {
    if (-not $DryRun) {
        $missingPackagePaths | ForEach-Object { Write-Error "Expected package is missing: $_" }
        throw "Publish requires $($expectedPackagePaths.Count) expected package(s), but $($missingPackagePaths.Count) are missing. Run Pack-NuGet.ps1 first."
    }

    $missingPackagePaths | ForEach-Object { Write-Host "[dry-run] expected package is missing: $_" }
}
elseif (-not $DryRun -or $ValidateReadinessInDryRun) {
    & (Join-Path $PSScriptRoot 'Test-NuGetPackageContents.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -PackageDir $PackageDir
}
else {
    Write-Host "[dry-run] NuGet package content validation skipped. Pass -ValidateReadinessInDryRun to validate package contents during dry-run."
}

$packagePaths = $expectedPackagePaths

if (-not $GitHubRelease -and -not $NuGetPush) {
    Write-Host "No publish target selected. Pass -GitHubRelease and/or -NuGetPush. Use -DryRun to inspect commands."
    return
}

if (-not $SkipPublishStateValidation -and $missingPackagePaths.Count -eq 0) {
    $skipExternalStateCheck = $SkipExternalPublishStateCheck -or ($DryRun -and -not $ValidateReadinessInDryRun)
    & (Join-Path $PSScriptRoot 'Test-ReleasePublishState.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -PackageDir $PackageDir `
        -Repository $Repository `
        -GitHubRelease:$GitHubRelease `
        -NuGetPush:$NuGetPush `
        -SkipExternalStateCheck:$skipExternalStateCheck `
        -AllowExistingGitHubRelease:$AllowExistingGitHubRelease `
        -AllowExistingNuGetPackages:$AllowExistingNuGetPackages
}
elseif ($SkipPublishStateValidation) {
    Write-Host "Release publish state validation skipped."
}

if (-not $SkipReadinessValidation -and (-not $DryRun -or $ValidateReadinessInDryRun)) {
    & (Join-Path $PSScriptRoot 'Test-ReleaseReadiness.ps1') `
        -PackageRevision $PackageRevision `
        -ManifestPath $ManifestPath `
        -FetchForks `
        -RequireForksUpToDate `
        -CheckUpstream `
        -RequireUpstreamCurrent:$RequireUpstreamCurrent `
        -SkipToolchainValidation `
        -FailOnError
}
elseif ($DryRun -and -not $ValidateReadinessInDryRun) {
    Write-Host "[dry-run] readiness validation skipped. Pass -ValidateReadinessInDryRun to run strict publish readiness during dry-run."
}

if ($GitHubRelease) {
    $gh = Get-Command gh -ErrorAction SilentlyContinue
    if (-not $gh -and -not $DryRun) {
        throw "GitHub CLI 'gh' is required for -GitHubRelease."
    }

    $args = @('release', 'create', $tag, '--repo', $Repository, '--title', "SDL3-CS $($wrapper.PackageVersion)", '--notes', "SDL3-CS release $($wrapper.PackageVersion)", '--draft')
    foreach ($pkg in $packagePaths) {
        $args += $pkg
    }

    Invoke-ReleaseCommand -FilePath 'gh' -Arguments $args -DryRun:$DryRun
}

if ($NuGetPush) {
    if (-not $env:NUGET_API_KEY -and -not $DryRun) {
        throw "NUGET_API_KEY is required for -NuGetPush. In GitHub Actions it is provided by NuGet/login trusted publishing."
    }

    $apiKey = if ($DryRun -and -not $env:NUGET_API_KEY) { '<NUGET_API_KEY>' } else { $env:NUGET_API_KEY }

    foreach ($pkg in $packagePaths) {
        $args = @(
            'nuget', 'push', $pkg,
            '--api-key', $apiKey,
            '--source', 'https://api.nuget.org/v3/index.json',
            '--skip-duplicate'
        )
        Invoke-ReleaseCommand -FilePath 'dotnet' -Arguments $args -DryRun:$DryRun
    }
}

if ($GitHubRelease) {
    if ($KeepGitHubReleaseDraft) {
        Write-Host "GitHub release draft was created and left unpublished: $tag"
    }
    else {
        $args = @('release', 'edit', $tag, '--repo', $Repository, '--draft=false')
        Invoke-ReleaseCommand -FilePath 'gh' -Arguments $args -DryRun:$DryRun
    }
}
