#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = 2,
    [int] $NativePackageRevision = 1,
    [string] $BaseReleaseRef = 'v3.4.12.1',
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $WorkflowPath = (Join-Path (Join-Path $PSScriptRoot '..') 'workflows\release-native-packages.yml'),
    [string] $Branch,
    [string] $Repository,
    [switch] $PublishGitHub,
    [switch] $PublishNuGet,
    [switch] $SkipLocalValidation,
    [switch] $SkipRemoteStateCheck,
    [switch] $Run
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$managedPackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision | Where-Object { $_.Kind -eq 'managed' })
if ($managedPackages.Count -ne 1 -or $managedPackages[0].Id -ne 'SDL3-CS') {
    throw "Managed workflow dispatch requires exactly one SDL3-CS package, got $($managedPackages.Count)."
}
$releaseNotesRelativePath = ".github/release-tools/release-notes/v$($managedPackages[0].PackageVersion).md"

function Invoke-ManagedReleaseGit {
    param(
        [Parameter(Mandatory)][string[]] $Arguments,
        [switch] $AllowFailure
    )

    $repoRoot = Get-ReleaseRepoRoot
    $output = & git -C $repoRoot @Arguments 2>&1
    if ($LASTEXITCODE -ne 0 -and -not $AllowFailure) {
        throw "git $($Arguments -join ' ') failed with exit code $LASTEXITCODE`: $($output | Out-String)"
    }

    return (($output | Out-String).Trim())
}

if (-not $SkipLocalValidation) {
    & (Join-Path $PSScriptRoot 'Test-ManagedReleaseWorkflow.ps1') -WorkflowPath $WorkflowPath
    & (Join-Path $PSScriptRoot 'Test-ManagedReleaseScope.Tests.ps1') -ManifestPath $ManifestPath
    & (Join-Path $PSScriptRoot 'Test-ManagedReleaseScope.ps1') `
        -PackageRevision $PackageRevision `
        -NativePackageRevision $NativePackageRevision `
        -BaseRef $BaseReleaseRef `
        -ManifestPath $ManifestPath `
        -CheckNuGet
    & (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath
    & (Join-Path $PSScriptRoot 'Test-ReleasePublishPlan.ps1')
    & (Join-Path $PSScriptRoot 'Test-ReleaseNotes.ps1') -ReleaseNotesPath $releaseNotesRelativePath
}

$workflowFile = Resolve-ReleasePath $WorkflowPath
if (-not (Test-Path -LiteralPath $workflowFile -PathType Leaf)) {
    throw "Managed release workflow was not found: $workflowFile"
}
$workflowRelativePath = Get-ReleaseRepoRelativePath -Path $workflowFile
$workflowId = Split-Path -Leaf $workflowRelativePath
$currentBranch = Invoke-ManagedReleaseGit -Arguments @('rev-parse', '--abbrev-ref', 'HEAD')
if (-not $Branch) {
    $Branch = $currentBranch
}

$remoteProblems = New-Object System.Collections.Generic.List[string]
if (-not $SkipRemoteStateCheck) {
    if ($Branch -ne $currentBranch) {
        $remoteProblems.Add("Requested branch '$Branch' is not the checked-out branch '$currentBranch'.")
    }

    $requiredPaths = @(
        $workflowRelativePath,
        'SDL3-CS',
        'SDL3-CS.Tests',
        'README.md',
        'README-nuget.md',
        '.github/release-tools/Invoke-ManagedReleaseWorkflow.ps1',
        '.github/release-tools/Pack-NuGet.ps1',
        '.github/release-tools/Publish-Release.ps1',
        '.github/release-tools/Restore-ManagedReleaseTestRuntime.ps1',
        '.github/release-tools/Test-ManagedReleaseScope.ps1',
        '.github/release-tools/Test-ManagedReleaseScope.Tests.ps1',
        '.github/release-tools/Test-ManagedReleaseWorkflow.ps1',
        '.github/release-tools/Test-NuGetPackageContents.ps1',
        '.github/release-tools/Test-ReleasePublishState.ps1',
        $releaseNotesRelativePath
    ) | Sort-Object -Unique

    foreach ($path in $requiredPaths) {
        $tracked = Invoke-ManagedReleaseGit -Arguments @('ls-files', '--', $path) -AllowFailure
        if (-not $tracked) {
            $remoteProblems.Add("Required managed release path is not tracked: $path")
        }
    }

    $status = Invoke-ManagedReleaseGit -Arguments (@('status', '--porcelain', '--') + $requiredPaths) -AllowFailure
    if ($status) {
        $remoteProblems.Add("Required managed release paths have uncommitted changes:`n$status")
    }

    $upstream = Invoke-ManagedReleaseGit -Arguments @('rev-parse', '--abbrev-ref', '--symbolic-full-name', '@{u}') -AllowFailure
    if (-not $upstream) {
        $remoteProblems.Add("Current branch '$currentBranch' has no upstream branch.")
    }
    else {
        $aheadBehind = Invoke-ManagedReleaseGit -Arguments @('rev-list', '--left-right', '--count', "$upstream...HEAD") -AllowFailure
        $parts = @($aheadBehind -split '\s+')
        if ($parts.Count -ge 2 -and [int] $parts[1] -gt 0) {
            $remoteProblems.Add("Current branch '$currentBranch' is $($parts[1]) commit(s) ahead of '$upstream'; push before dispatch.")
        }
    }
}

$args = @(
    'workflow', 'run', $workflowId,
    '--ref', $Branch,
    '-f', "package_revision=$PackageRevision",
    '-f', 'managed_only=true',
    '-f', "native_package_revision=$NativePackageRevision",
    '-f', "base_release_ref=$BaseReleaseRef",
    '-f', "publish_github=$($PublishGitHub.IsPresent.ToString().ToLowerInvariant())",
    '-f', "publish_nuget=$($PublishNuGet.IsPresent.ToString().ToLowerInvariant())"
)
if ($Repository) {
    $args += @('--repo', $Repository)
}

[pscustomobject]@{
    Workflow = $workflowId
    Branch = $Branch
    PackageRevision = $PackageRevision
    NativePackageRevision = $NativePackageRevision
    BaseReleaseRef = $BaseReleaseRef
    PublishGitHub = $PublishGitHub.IsPresent
    PublishNuGet = $PublishNuGet.IsPresent
    Run = $Run.IsPresent
} | Format-List

if ($remoteProblems.Count -gt 0) {
    $remoteProblems | ForEach-Object { Write-Warning $_ }
    if ($Run) {
        throw "Managed release dispatch preflight failed with $($remoteProblems.Count) remote state problem(s)."
    }
}

if (-not $Run) {
    Write-Host "[dry-run] gh $($args -join ' ')"
    Write-Host 'Pass -Run only after commit, push and mainline parity are complete.'
    return
}

if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    throw "GitHub CLI was not found. Install 'gh' before dispatching the workflow."
}

& gh auth status
if ($LASTEXITCODE -ne 0) {
    throw "GitHub CLI is not authenticated. Run 'gh auth login' before dispatching the workflow."
}

Invoke-ReleaseCommand -FilePath 'gh' -Arguments $args
Write-Host "Managed release workflow dispatch requested for $Branch."
