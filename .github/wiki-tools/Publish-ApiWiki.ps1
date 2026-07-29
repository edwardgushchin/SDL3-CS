#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $CandidatePath,
    [string] $WikiRepositoryUrl = 'https://github.com/edwardgushchin/SDL3-CS.wiki.git',
    [string] $WorkingRoot = (Join-Path (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path '.agents\wiki-publication'),
    [string] $ExpectedManagedVersion,
    [ValidatePattern('^[0-9a-fA-F]{40}$')]
    [string] $ExpectedSourceCommit,
    [string] $ExpectedGeneratedAtUtc,
    [ValidatePattern('^[0-9a-fA-F]{64}$')]
    [string] $ExpectedContentHash,
    [string] $CommitUserName = 'SDL3-CS Release Automation',
    [string] $CommitUserEmail = '41898282+github-actions[bot]@users.noreply.github.com',
    [switch] $Push,
    [switch] $KeepWorkingCopy
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$CandidatePath = [System.IO.Path]::GetFullPath($CandidatePath)
$WorkingRoot = [System.IO.Path]::GetFullPath($WorkingRoot)
$validator = Join-Path $PSScriptRoot 'Test-ApiWiki.ps1'
if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Wiki validator was not found: $validator"
}
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    throw "Git CLI is required to publish the Wiki."
}

$validationParameters = @{
    WikiPath = $CandidatePath
    RequireExactPages = $true
}
if ($ExpectedManagedVersion) { $validationParameters.ExpectedManagedVersion = $ExpectedManagedVersion }
if ($ExpectedSourceCommit) { $validationParameters.ExpectedSourceCommit = $ExpectedSourceCommit }
if ($ExpectedGeneratedAtUtc) { $validationParameters.ExpectedGeneratedAtUtc = $ExpectedGeneratedAtUtc }
if ($ExpectedContentHash) { $validationParameters.ExpectedContentHash = $ExpectedContentHash }
$candidateValidation = (& $validator @validationParameters | ConvertFrom-Json)

$managedPages = @(
    ('SDL3' + [char]0x2010 + 'CS-Wiki.md'),
    '_Sidebar.md',
    'API-Reference.md',
    'API-SDL.md',
    'API-Image.md',
    'API-Mixer.md',
    'API-TTF.md',
    'API-ShaderCross.md'
)

function Invoke-WikiGit {
    param(
        [Parameter(Mandatory)][string] $RepositoryPath,
        [Parameter(Mandatory)][string[]] $Arguments
    )

    $output = & git -C $RepositoryPath @Arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Arguments -join ' ') failed: $($output | Out-String)"
    }

    return (($output | Out-String).Trim())
}

New-Item -ItemType Directory -Force -Path $WorkingRoot | Out-Null
$runName = "run-$([guid]::NewGuid().ToString('N'))"
$clonePath = [System.IO.Path]::GetFullPath((Join-Path $WorkingRoot $runName))
$relativeClonePath = [System.IO.Path]::GetRelativePath($WorkingRoot, $clonePath)
if ($relativeClonePath.StartsWith('..') -or [System.IO.Path]::IsPathRooted($relativeClonePath) -or -not $runName.StartsWith('run-', [System.StringComparison]::Ordinal)) {
    throw "Unsafe Wiki publication working path: $clonePath"
}

$result = $null
try {
    $cloneOutput = & git clone --no-tags -- $WikiRepositoryUrl $clonePath 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to clone Wiki repository '$WikiRepositoryUrl': $($cloneOutput | Out-String)"
    }

    $initialStatus = Invoke-WikiGit -RepositoryPath $clonePath -Arguments @('status', '--porcelain', '--untracked-files=all')
    if ($initialStatus) {
        throw "Fresh Wiki clone is unexpectedly dirty: $initialStatus"
    }

    foreach ($page in $managedPages) {
        [System.IO.File]::Copy((Join-Path $CandidatePath $page), (Join-Path $clonePath $page), $true)
    }

    Invoke-WikiGit -RepositoryPath $clonePath -Arguments (@('add', '--') + $managedPages) | Out-Null
    & git -C $clonePath diff --cached --quiet -- @managedPages
    $diffExitCode = $LASTEXITCODE
    if ($diffExitCode -notin @(0, 1)) {
        throw "Failed to compare staged Wiki pages (git exit code $diffExitCode)."
    }
    $hasChanges = $diffExitCode -eq 1
    $changes = if ($hasChanges) {
        Invoke-WikiGit -RepositoryPath $clonePath -Arguments (@('diff', '--cached', '--name-status', '--') + $managedPages)
    } else {
        ''
    }
    $status = if (-not $hasChanges) { 'NoChange' } elseif ($Push) { 'Published' } else { 'Planned' }
    $wikiCommit = $null
    if (-not $hasChanges) {
        $wikiCommit = Invoke-WikiGit -RepositoryPath $clonePath -Arguments @('rev-parse', '--verify', 'HEAD')
    }

    if ($hasChanges -and $Push) {
        $commitMessage = "docs: update wiki for SDL3-CS $($candidateValidation.ManagedVersion)"
        Invoke-WikiGit -RepositoryPath $clonePath -Arguments @('-c', "user.name=$CommitUserName", '-c', "user.email=$CommitUserEmail", 'commit', '-m', $commitMessage) | Out-Null
        $wikiCommit = Invoke-WikiGit -RepositoryPath $clonePath -Arguments @('rev-parse', 'HEAD')
        Invoke-WikiGit -RepositoryPath $clonePath -Arguments @('push', 'origin', 'HEAD:master') | Out-Null

        $remoteLine = Invoke-WikiGit -RepositoryPath $clonePath -Arguments @('ls-remote', '--heads', 'origin', 'refs/heads/master')
        $remoteCommit = @($remoteLine -split '\s+')[0]
        if ($remoteCommit -ne $wikiCommit) {
            throw "Wiki push verification failed: local $wikiCommit, remote $remoteCommit"
        }

        $postPushParameters = $validationParameters.Clone()
        $postPushParameters.WikiPath = $clonePath
        $postPushParameters.ExpectedContentHash = $candidateValidation.ContentHash
        [void] $postPushParameters.Remove('RequireExactPages')
        & $validator @postPushParameters | Out-Null
    }

    $result = [pscustomobject]@{
        Status = $status
        ManagedVersion = $candidateValidation.ManagedVersion
        SourceCommit = $candidateValidation.SourceCommit
        GeneratedAtUtc = $candidateValidation.GeneratedAtUtc
        ContentHash = $candidateValidation.ContentHash
        WikiCommit = $wikiCommit
        WikiRepositoryUrl = $WikiRepositoryUrl
        ChangedPages = @($changes -split "`r?`n" | Where-Object { $_ })
        Push = $Push.IsPresent
    }
}
finally {
    if (-not $KeepWorkingCopy -and (Test-Path -LiteralPath $clonePath)) {
        $resolvedClonePath = [System.IO.Path]::GetFullPath($clonePath)
        $relative = [System.IO.Path]::GetRelativePath($WorkingRoot, $resolvedClonePath)
        if ($relative.StartsWith('..') -or [System.IO.Path]::IsPathRooted($relative) -or
            -not ([System.IO.Path]::GetFileName($resolvedClonePath)).StartsWith('run-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe Wiki publication path: $resolvedClonePath"
        }

        Remove-Item -LiteralPath $resolvedClonePath -Recurse -Force
    }
}

$result | ConvertTo-Json -Depth 4 -Compress
