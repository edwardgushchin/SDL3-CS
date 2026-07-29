#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path,
    [string] $AssemblyPath,
    [string] $XmlDocPath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$generator = Join-Path $PSScriptRoot 'Generate-ApiWiki.ps1'
$validator = Join-Path $PSScriptRoot 'Test-ApiWiki.ps1'
$publisher = Join-Path $PSScriptRoot 'Publish-ApiWiki.ps1'
$remoteValidator = Join-Path $PSScriptRoot 'Test-PublishedApiWiki.ps1'
foreach ($dependency in @($generator, $validator, $publisher, $remoteValidator)) {
    if (-not (Test-Path -LiteralPath $dependency -PathType Leaf)) {
        throw "Wiki tooling dependency was not found: $dependency"
    }
}

if (-not $AssemblyPath) {
    $AssemblyPath = Join-Path $ProjectRoot 'SDL3-CS\bin\Release\net8.0\SDL3-CS.dll'
}
if (-not $XmlDocPath) {
    $XmlDocPath = Join-Path $ProjectRoot 'SDL3-CS\bin\Release\SDL3-CS.xml'
}
foreach ($buildOutput in @($AssemblyPath, $XmlDocPath)) {
    if (-not (Test-Path -LiteralPath $buildOutput -PathType Leaf)) {
        throw "Wiki tooling tests require Release build output: $buildOutput"
    }
}

function Invoke-JsonScript {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][hashtable] $Parameters
    )

    $output = @(& $Path @Parameters)
    $json = @($output | Where-Object { $_ -is [string] -and $_.TrimStart().StartsWith('{') } | Select-Object -Last 1)
    if ($json.Count -ne 1) {
        throw "Script did not return one-line JSON: $Path`n$($output | Out-String)"
    }

    return $json[0] | ConvertFrom-Json
}

function Assert-ActionFails {
    param(
        [Parameter(Mandatory)][string] $Description,
        [Parameter(Mandatory)][scriptblock] $Action
    )

    $failed = $false
    try {
        & $Action
    }
    catch {
        $failed = $true
    }

    if (-not $failed) {
        throw "Expected Wiki tooling action to fail: $Description"
    }
}

function Invoke-TestGit {
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

$sourceCommit = (Invoke-TestGit -RepositoryPath $ProjectRoot -Arguments @('rev-parse', 'HEAD')).Trim()
$managedVersion = [System.Reflection.AssemblyName]::GetAssemblyName([System.IO.Path]::GetFullPath($AssemblyPath)).Version.ToString()
$generatedAtUtc = '2026-07-29T13:05:28Z'
$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-wiki-tools-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe Wiki test path: $tempRoot"
}

$candidateA = Join-Path $tempRoot 'candidate-a'
$candidateB = Join-Path $tempRoot 'candidate-b'
$tamperedCandidate = Join-Path $tempRoot 'candidate-tampered'
$bareRemote = Join-Path $tempRoot 'wiki-remote.git'
$publishRoot = Join-Path $tempRoot 'publish-work'

try {
    New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null

    $generationParameters = @{
        ProjectRoot = $tempRoot
        AssemblyPath = [System.IO.Path]::GetFullPath($AssemblyPath)
        XmlDocPath = [System.IO.Path]::GetFullPath($XmlDocPath)
        SourceCommit = $sourceCommit
        GeneratedAtUtc = $generatedAtUtc
    }
    & $generator @generationParameters -OutputPath $candidateA | Out-Null
    & $generator @generationParameters -OutputPath $candidateB | Out-Null

    $validationParameters = @{
        ExpectedManagedVersion = $managedVersion
        ExpectedSourceCommit = $sourceCommit
        ExpectedGeneratedAtUtc = $generatedAtUtc
        RequireExactPages = $true
    }
    $first = Invoke-JsonScript -Path $validator -Parameters ($validationParameters + @{ WikiPath = $candidateA })
    $second = Invoke-JsonScript -Path $validator -Parameters ($validationParameters + @{ WikiPath = $candidateB })
    if ($first.ContentHash -ne $second.ContentHash) {
        throw "Repeated Wiki generation is not deterministic: $($first.ContentHash) != $($second.ContentHash)"
    }

    Assert-ActionFails -Description 'stale source commit metadata' -Action {
        $staleParameters = $validationParameters.Clone()
        $staleParameters.ExpectedSourceCommit = ('f' * 40)
        & $validator @staleParameters -WikiPath $candidateA *> $null
    }

    Copy-Item -LiteralPath $candidateA -Destination $tamperedCandidate -Recurse
    Add-Content -LiteralPath (Join-Path $tamperedCandidate 'API-SDL.md') -Value "`nTampered content" -Encoding utf8NoBOM
    Assert-ActionFails -Description 'unexpected content hash' -Action {
        & $validator @validationParameters -WikiPath $tamperedCandidate -ExpectedContentHash $first.ContentHash *> $null
    }

    & git init --bare --initial-branch=master $bareRemote *> $null
    if ($LASTEXITCODE -ne 0) {
        throw 'Failed to initialize local Wiki publication remote.'
    }

    $publishParameters = @{
        CandidatePath = $candidateA
        WikiRepositoryUrl = $bareRemote
        WorkingRoot = $publishRoot
        ExpectedManagedVersion = $managedVersion
        ExpectedSourceCommit = $sourceCommit
        ExpectedGeneratedAtUtc = $generatedAtUtc
        ExpectedContentHash = $first.ContentHash
        Push = $true
    }
    $published = Invoke-JsonScript -Path $publisher -Parameters $publishParameters
    if ($published.Status -ne 'Published') {
        throw "Initial Wiki publication returned unexpected status: $($published.Status)"
    }
    $commitCount = [int]((& git --git-dir=$bareRemote rev-list --count master).Trim())

    $noChange = Invoke-JsonScript -Path $publisher -Parameters $publishParameters
    if ($noChange.Status -ne 'NoChange') {
        throw "Repeated Wiki publication returned unexpected status: $($noChange.Status)"
    }
    $repeatedCommitCount = [int]((& git --git-dir=$bareRemote rev-list --count master).Trim())
    if ($repeatedCommitCount -ne $commitCount) {
        throw 'Idempotent Wiki publication created an extra commit.'
    }

    $remoteValidation = Invoke-JsonScript -Path $remoteValidator -Parameters @{
        WikiRepositoryUrl = $bareRemote
        WorkingRoot = (Join-Path $tempRoot 'remote-verification')
        ExpectedManagedVersion = $managedVersion
        ExpectedSourceCommit = $sourceCommit
        ExpectedGeneratedAtUtc = $generatedAtUtc
        ExpectedContentHash = $first.ContentHash
    }
    if ($remoteValidation.ContentHash -ne $first.ContentHash) {
        throw 'Published Wiki verification returned an unexpected content hash.'
    }
    Assert-ActionFails -Description 'published Wiki managed version mismatch' -Action {
        & $remoteValidator `
            -WikiRepositoryUrl $bareRemote `
            -WorkingRoot (Join-Path $tempRoot 'remote-verification-failure') `
            -ExpectedManagedVersion '9.9.9.9' `
            -ExpectedSourceCommit $sourceCommit *> $null
    }

    Assert-ActionFails -Description 'unavailable Wiki remote' -Action {
        $failedPublishParameters = $publishParameters.Clone()
        $failedPublishParameters.WikiRepositoryUrl = Join-Path $tempRoot 'missing-remote.git'
        & $publisher @failedPublishParameters *> $null
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-wiki-tools-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe Wiki test path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Wiki tooling tests passed.'
