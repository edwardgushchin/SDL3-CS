#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $WikiRepositoryUrl = 'https://github.com/edwardgushchin/SDL3-CS.wiki.git',
    [string] $WorkingRoot = (Join-Path (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path '.agents\wiki-verification'),
    [Parameter(Mandatory)][string] $ExpectedManagedVersion,
    [Parameter(Mandatory)]
    [ValidatePattern('^[0-9a-fA-F]{40}$')]
    [string] $ExpectedSourceCommit,
    [string] $ExpectedGeneratedAtUtc,
    [ValidatePattern('^[0-9a-fA-F]{64}$')]
    [string] $ExpectedContentHash,
    [switch] $KeepWorkingCopy
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if ($WikiRepositoryUrl.StartsWith('-', [System.StringComparison]::Ordinal)) {
    throw 'WikiRepositoryUrl must not start with a command-line option prefix.'
}
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    throw 'Git CLI is required to verify the published Wiki.'
}

$validator = Join-Path $PSScriptRoot 'Test-ApiWiki.ps1'
if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Wiki validator was not found: $validator"
}

$WorkingRoot = [System.IO.Path]::GetFullPath($WorkingRoot)
New-Item -ItemType Directory -Force -Path $WorkingRoot | Out-Null
$runName = "run-$([guid]::NewGuid().ToString('N'))"
$clonePath = [System.IO.Path]::GetFullPath((Join-Path $WorkingRoot $runName))
$relativeClonePath = [System.IO.Path]::GetRelativePath($WorkingRoot, $clonePath)
if ($relativeClonePath.StartsWith('..') -or [System.IO.Path]::IsPathRooted($relativeClonePath)) {
    throw "Unsafe published Wiki verification path: $clonePath"
}

$result = $null
try {
    $cloneOutput = & git clone --no-tags --depth 1 --branch master $WikiRepositoryUrl $clonePath 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to clone published Wiki '$WikiRepositoryUrl': $($cloneOutput | Out-String)"
    }

    $validationParameters = @{
        WikiPath = $clonePath
        ExpectedManagedVersion = $ExpectedManagedVersion
        ExpectedSourceCommit = $ExpectedSourceCommit
    }
    if ($ExpectedGeneratedAtUtc) { $validationParameters.ExpectedGeneratedAtUtc = $ExpectedGeneratedAtUtc }
    if ($ExpectedContentHash) { $validationParameters.ExpectedContentHash = $ExpectedContentHash }
    $validation = (& $validator @validationParameters | ConvertFrom-Json)

    $wikiCommit = ((& git -C $clonePath rev-parse HEAD 2>&1) | Out-String).Trim()
    if ($LASTEXITCODE -ne 0 -or $wikiCommit -notmatch '^[0-9a-fA-F]{40}$') {
        throw 'Failed to resolve the published Wiki commit.'
    }

    $result = [pscustomobject]@{
        Status = 'Current'
        ManagedVersion = $validation.ManagedVersion
        SourceCommit = $validation.SourceCommit
        GeneratedAtUtc = $validation.GeneratedAtUtc
        ContentHash = $validation.ContentHash
        WikiCommit = $wikiCommit.ToLowerInvariant()
        WikiRepositoryUrl = $WikiRepositoryUrl
    }
}
finally {
    if (-not $KeepWorkingCopy -and (Test-Path -LiteralPath $clonePath)) {
        $resolvedClonePath = [System.IO.Path]::GetFullPath($clonePath)
        $relative = [System.IO.Path]::GetRelativePath($WorkingRoot, $resolvedClonePath)
        if ($relative.StartsWith('..') -or [System.IO.Path]::IsPathRooted($relative) -or
            -not ([System.IO.Path]::GetFileName($resolvedClonePath)).StartsWith('run-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe published Wiki verification path: $resolvedClonePath"
        }

        Remove-Item -LiteralPath $resolvedClonePath -Recurse -Force
    }
}

$result | ConvertTo-Json -Depth 3 -Compress
