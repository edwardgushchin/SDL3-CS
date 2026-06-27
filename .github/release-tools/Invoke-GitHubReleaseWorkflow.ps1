#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $WorkflowPath = (Join-Path (Join-Path $PSScriptRoot '..') 'workflows\release-native-packages.yml'),
    [string] $Branch,
    [int] $BuildParallelLevel = 2,
    [string] $Repository,
    [switch] $RequireUpstreamCurrent,
    [switch] $PublishGitHub,
    [switch] $PublishNuGet,
    [switch] $SkipLocalValidation,
    [switch] $SkipRemoteStateCheck,
    [switch] $Run
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Invoke-RepoGit {
    param(
        [Parameter(Mandatory)]
        [string[]] $Arguments,

        [switch] $AllowFailure
    )

    $repoRoot = Get-ReleaseRepoRoot
    $output = & git -C $repoRoot @Arguments 2>&1
    if ($LASTEXITCODE -ne 0 -and -not $AllowFailure) {
        throw "git $($Arguments -join ' ') failed with exit code $LASTEXITCODE`: $($output | Out-String)"
    }

    return ($output | Out-String).Trim()
}

function Add-RemoteStateProblem {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:remoteStateProblems.Add($Message)
}

function Test-TrackedPath {
    param(
        [Parameter(Mandatory)]
        [string] $RelativePath
    )

    $repoRoot = Get-ReleaseRepoRoot
    $output = & git -C $repoRoot ls-files -- $RelativePath
    if ($LASTEXITCODE -ne 0) {
        return $false
    }

    return -not [string]::IsNullOrWhiteSpace(($output | Out-String))
}

if (-not $SkipLocalValidation) {
    & (Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1') -ManifestPath $ManifestPath -SkipSourceCheckoutValidation
    & (Join-Path $PSScriptRoot 'Test-ReleaseWorkflow.ps1') -ManifestPath $ManifestPath -WorkflowPath $WorkflowPath
    & (Join-Path $PSScriptRoot 'Test-ReleasePublishPlan.ps1')
    & (Join-Path $PSScriptRoot 'Test-ReleaseNotes.ps1')
    & (Join-Path $PSScriptRoot 'Test-NativeForkInitializationPlan.ps1') -ManifestPath $ManifestPath
    & (Join-Path $PSScriptRoot 'Test-NativeBuildPlan.ps1') -ManifestPath $ManifestPath
    & (Join-Path $PSScriptRoot 'Test-NativePackageRidEntries.ps1') -ManifestPath $ManifestPath
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}

if (-not $SkipLocalValidation) {
    & (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath
}

$workflowFile = Resolve-ReleasePath $WorkflowPath
if (-not (Test-Path -LiteralPath $workflowFile -PathType Leaf)) {
    throw "Release workflow was not found: $workflowFile"
}

$workflowRelativePath = Get-ReleaseRepoRelativePath -Path $workflowFile
$workflowId = Split-Path -Leaf $workflowRelativePath
$currentBranch = Invoke-RepoGit -Arguments @('rev-parse', '--abbrev-ref', 'HEAD')
if (-not $Branch) {
    $Branch = $currentBranch
}

$remoteStateProblems = New-Object System.Collections.Generic.List[string]
if (-not $SkipRemoteStateCheck) {
    if ($Branch -ne $currentBranch) {
        Add-RemoteStateProblem "Requested branch '$Branch' is not the checked-out branch '$currentBranch'; local file checks cannot prove that the remote branch contains this workflow."
    }

    $releaseToolPaths = @(Get-ChildItem -LiteralPath (Join-Path (Get-ReleaseRepoRoot) '.github/release-tools') -File | ForEach-Object {
        Get-ReleaseRepoRelativePath -Path $_.FullName
    })

    $nativePackagePaths = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision |
        Where-Object { $_.Kind -eq 'native' } |
        ForEach-Object {
            $packageRoot = (Split-Path -Parent $_.Project).Replace('\', '/')
            $_.Project
            "$packageRoot/README.md"
            "$packageRoot/buildTransitive/$($_.Id).targets"
            if ($_.Id -eq 'SDL3-CS.Android') {
                "$packageRoot/SDL3Bridge.jar"
                "$packageRoot/Transforms/Metadata.xml"
            }
        })

    $requiredRemotePaths = @($workflowRelativePath) + $nativePackagePaths + $releaseToolPaths
    $requiredRemotePaths = @($requiredRemotePaths | Sort-Object -Unique)

    foreach ($relativePath in $requiredRemotePaths) {
        if (-not (Test-TrackedPath -RelativePath $relativePath)) {
            Add-RemoteStateProblem "Required workflow input path is not tracked by Git: $relativePath"
        }
    }

    $statusArgs = @('status', '--porcelain', '--') + $requiredRemotePaths
    $pathStatus = Invoke-RepoGit -Arguments $statusArgs -AllowFailure
    if ($pathStatus) {
        Add-RemoteStateProblem "Required workflow input paths have uncommitted changes:`n$pathStatus"
    }

    $upstream = Invoke-RepoGit -Arguments @('rev-parse', '--abbrev-ref', '--symbolic-full-name', '@{u}') -AllowFailure
    if (-not $upstream) {
        Add-RemoteStateProblem "Current branch '$currentBranch' has no upstream branch; GitHub Actions can only run committed files from a remote ref."
    }
    else {
        $aheadBehind = Invoke-RepoGit -Arguments @('rev-list', '--left-right', '--count', "$upstream...HEAD") -AllowFailure
        if ($aheadBehind) {
            $parts = @($aheadBehind -split '\s+')
            if ($parts.Count -ge 2) {
                $ahead = [int] $parts[1]
                if ($ahead -gt 0) {
                    Add-RemoteStateProblem "Current branch '$currentBranch' is $ahead commit(s) ahead of '$upstream'; push before running the workflow."
                }
            }
        }
    }
}

$args = @(
    'workflow', 'run', $workflowId,
    '--ref', $Branch,
    '-f', "package_revision=$PackageRevision",
    '-f', "build_parallel_level=$BuildParallelLevel",
    '-f', "require_upstream_current=$($RequireUpstreamCurrent.IsPresent.ToString().ToLowerInvariant())",
    '-f', "publish_github=$($PublishGitHub.IsPresent.ToString().ToLowerInvariant())",
    '-f', "publish_nuget=$($PublishNuGet.IsPresent.ToString().ToLowerInvariant())"
)
if ($Repository) {
    $args += @('--repo', $Repository)
}

Write-Host "GitHub Actions release workflow plan:"
[pscustomobject]@{
    Workflow = $workflowId
    Branch = $Branch
    PackageRevision = $PackageRevision
    BuildParallelLevel = $BuildParallelLevel
    RequireUpstreamCurrent = $RequireUpstreamCurrent.IsPresent
    PublishGitHub = $PublishGitHub.IsPresent
    PublishNuGet = $PublishNuGet.IsPresent
    Run = $Run.IsPresent
} | Format-List

if ($PublishNuGet) {
    Write-Warning "publish_nuget=true requires an active NuGet Trusted Publishing policy for owner 'edwardgushchin', repository 'edwardgushchin/SDL3-CS', workflow '$workflowId', environment 'production'."
}

if ($remoteStateProblems.Count -gt 0) {
    foreach ($problem in $remoteStateProblems) {
        Write-Warning $problem
    }

    if ($Run) {
        throw "GitHub Actions workflow run preflight failed with $($remoteStateProblems.Count) remote state problem(s). Commit and push the required release files before running."
    }
}

if (-not $Run) {
    Write-Host "[dry-run] gh $($args -join ' ')"
    Write-Host "Pass -Run to dispatch the workflow after the remote state preflight is clean."
    return
}

$gh = Get-Command gh -ErrorAction SilentlyContinue
if (-not $gh) {
    throw "GitHub CLI 'gh' is required to dispatch the release workflow."
}

& gh auth status
if ($LASTEXITCODE -ne 0) {
    throw "GitHub CLI is not authenticated. Run 'gh auth login' before dispatching the release workflow."
}

Invoke-ReleaseCommand -FilePath 'gh' -Arguments $args
Write-Host "Workflow dispatch requested. Use 'gh run list --workflow $workflowId --branch $Branch' to monitor it."
