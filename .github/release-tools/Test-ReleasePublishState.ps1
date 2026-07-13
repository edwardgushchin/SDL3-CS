#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string] $Repository = 'edwardgushchin/SDL3-CS',
    [switch] $GitHubRelease,
    [switch] $NuGetPush,
    [switch] $SkipExternalStateCheck,
    [switch] $AllowExistingGitHubRelease,
    [switch] $AllowExistingNuGetPackages,
    [switch] $ManagedOnly
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Add-PublishStateError {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:errors.Add($Message)
}

function Test-GitHubReleaseExists {
    param(
        [Parameter(Mandatory)]
        [string] $Tag,

        [Parameter(Mandatory)]
        [string] $Repository
    )

    $output = & gh release view $Tag --repo $Repository 2>&1
    $exitCode = $LASTEXITCODE
    $text = (($output | Out-String).Trim())

    if ($exitCode -eq 0) {
        return [pscustomobject]@{
            Exists = $true
            Available = $false
            Error = $null
            ExitCode = $exitCode
            Output = $text
        }
    }

    if ($text -match '(?i)(not found|no release found|HTTP 404)') {
        return [pscustomobject]@{
            Exists = $false
            Available = $true
            Error = $null
            ExitCode = $exitCode
            Output = $text
        }
    }

    return [pscustomobject]@{
        Exists = $false
        Available = $false
        Error = if ($text) { $text } else { "gh release view failed with exit code $exitCode." }
        ExitCode = $exitCode
        Output = $text
    }
}

function Test-GitHubTagExists {
    param(
        [Parameter(Mandatory)]
        [string] $Tag,

        [Parameter(Mandatory)]
        [string] $Repository
    )

    $repositoryUrl = "https://github.com/$Repository.git"
    $output = & git ls-remote --exit-code --tags $repositoryUrl "refs/tags/$Tag" 2>&1
    $exitCode = $LASTEXITCODE
    $text = (($output | Out-String).Trim())

    if ($exitCode -eq 0) {
        return [pscustomobject]@{
            Exists = $true
            Available = $false
            Error = $null
            ExitCode = $exitCode
            Output = $text
        }
    }

    if ($exitCode -eq 2 -or [string]::IsNullOrWhiteSpace($text)) {
        return [pscustomobject]@{
            Exists = $false
            Available = $true
            Error = $null
            ExitCode = $exitCode
            Output = $text
        }
    }

    return [pscustomobject]@{
        Exists = $false
        Available = $false
        Error = if ($text) { $text } else { "git ls-remote failed with exit code $exitCode." }
        ExitCode = $exitCode
        Output = $text
    }
}

function Test-NuGetPackageExists {
    param(
        [Parameter(Mandatory)]
        [string] $PackageId,

        [Parameter(Mandatory)]
        [string] $PackageVersion
    )

    $lowerId = $PackageId.ToLowerInvariant()
    $lowerVersion = (Get-ReleaseNormalizedNuGetVersion -PackageVersion $PackageVersion).ToLowerInvariant()
    $uri = "https://api.nuget.org/v3-flatcontainer/$lowerId/$lowerVersion/$lowerId.$lowerVersion.nupkg"

    try {
        $response = Invoke-WebRequest -Uri $uri -Method Head -TimeoutSec 30 -SkipHttpErrorCheck
        return [pscustomobject]@{
            Exists = ([int] $response.StatusCode -eq 200)
            StatusCode = [int] $response.StatusCode
            Uri = $uri
        }
    }
    catch {
        return [pscustomobject]@{
            Exists = $false
            StatusCode = 0
            Uri = $uri
            Error = $_.Exception.Message
        }
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir

& (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]
$packages = Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision
if ($ManagedOnly) {
    $packages = @($packages | Where-Object { $_.Kind -eq 'managed' })
}
$wrapper = @($packages | Where-Object { $_.Id -eq 'SDL3-CS' })[0]
if (-not $wrapper) {
    throw "Managed wrapper package SDL3-CS was not found in release manifest."
}

$tag = "v$($wrapper.PackageVersion)"

foreach ($package in $packages) {
    $packagePath = Get-ReleaseNuGetPackagePath -PackageDir $PackageDir -Package $package
    $status = if (Test-Path -LiteralPath $packagePath -PathType Leaf) { 'present' } else { 'missing' }
    if ($status -eq 'missing') {
        Add-PublishStateError "Expected package is missing before publish: $packagePath"
    }

    $rows.Add([pscustomobject]@{
        Target = 'local-package'
        Id = $package.Id
        Version = $package.PackageVersion
        Status = $status
        Detail = $packagePath
    })
}

if ($GitHubRelease) {
    if ($SkipExternalStateCheck) {
        foreach ($target in @('github-release', 'github-tag')) {
            $rows.Add([pscustomobject]@{
                Target = $target
                Id = $Repository
                Version = $tag
                Status = 'skipped'
                Detail = 'external state check skipped'
            })
        }
    }
    else {
        $gh = Get-Command gh -ErrorAction SilentlyContinue
        if (-not $gh) {
            Add-PublishStateError "GitHub CLI 'gh' is required to check GitHub release state."
            $rows.Add([pscustomobject]@{
                Target = 'github-release'
                Id = $Repository
                Version = $tag
                Status = 'missing-tool'
                Detail = 'gh'
            })
        }
        else {
            $releaseState = Test-GitHubReleaseExists -Tag $tag -Repository $Repository
            if ($releaseState.Exists -and -not $AllowExistingGitHubRelease) {
                Add-PublishStateError "GitHub release '$tag' already exists in $Repository."
            }
            elseif (-not $releaseState.Available -and -not $releaseState.Exists) {
                Add-PublishStateError "Could not prove GitHub release '$tag' is available in $Repository`: $($releaseState.Error)"
            }

            $rows.Add([pscustomobject]@{
                Target = 'github-release'
                Id = $Repository
                Version = $tag
                Status = if ($releaseState.Exists) { 'exists' } elseif ($releaseState.Available) { 'available' } else { 'unknown' }
                Detail = if ($releaseState.Error) { $releaseState.Error } elseif ($releaseState.Output) { $releaseState.Output } else { "exit $($releaseState.ExitCode)" }
            })
        }

        $tagState = Test-GitHubTagExists -Tag $tag -Repository $Repository
        if ($tagState.Exists -and -not $AllowExistingGitHubRelease) {
            Add-PublishStateError "GitHub tag '$tag' already exists in $Repository."
        }
        elseif (-not $tagState.Available -and -not $tagState.Exists) {
            Add-PublishStateError "Could not prove GitHub tag '$tag' is available in $Repository`: $($tagState.Error)"
        }

        $rows.Add([pscustomobject]@{
            Target = 'github-tag'
            Id = $Repository
            Version = $tag
            Status = if ($tagState.Exists) { 'exists' } elseif ($tagState.Available) { 'available' } else { 'unknown' }
            Detail = if ($tagState.Error) { $tagState.Error } elseif ($tagState.Output) { $tagState.Output } else { "exit $($tagState.ExitCode)" }
        })
    }
}

if ($NuGetPush) {
    foreach ($package in $packages) {
        if ($SkipExternalStateCheck) {
            $rows.Add([pscustomobject]@{
                Target = 'nuget-package'
                Id = $package.Id
                Version = $package.PackageVersion
                Status = 'skipped'
                Detail = 'external state check skipped'
            })
            continue
        }

        $nugetState = Test-NuGetPackageExists -PackageId $package.Id -PackageVersion $package.PackageVersion
        $nugetError = if ($nugetState.PSObject.Properties.Name.Contains('Error')) { $nugetState.Error } else { $null }
        if ($nugetError) {
            Add-PublishStateError "Could not check NuGet package state for $($package.Id) $($package.PackageVersion): $nugetError"
        }
        elseif ($nugetState.Exists -and -not $AllowExistingNuGetPackages) {
            Add-PublishStateError "NuGet package '$($package.Id)' version '$($package.PackageVersion)' already exists."
        }
        elseif ($nugetState.StatusCode -ne 200 -and $nugetState.StatusCode -ne 404) {
            Add-PublishStateError "Unexpected NuGet package state for $($package.Id) $($package.PackageVersion): HTTP $($nugetState.StatusCode)."
        }

        $rows.Add([pscustomobject]@{
            Target = 'nuget-package'
            Id = $package.Id
            Version = $package.PackageVersion
            Status = if ($nugetState.Exists) { 'exists' } else { 'available' }
            Detail = if ($nugetError) { $nugetError } else { "HTTP $($nugetState.StatusCode)" }
        })
    }
}

$rows | Sort-Object Target, Id, Version | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Release publish state validation failed with $($errors.Count) error(s)."
}

Write-Host "Release publish state is valid."
