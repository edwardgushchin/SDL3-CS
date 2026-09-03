#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Invoke-SourceGit {
    param(
        [Parameter(Mandatory)][string[]] $Arguments,
        [Parameter(Mandatory)][string] $Description
    )

    $output = @(& git -C $script:resolvedSourceRoot @Arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        throw "$Description failed: $($output -join [Environment]::NewLine)"
    }

    return ($output -join "`n").Trim()
}

function Get-FunctionBody {
    param(
        [Parameter(Mandatory)][string] $Source,
        [Parameter(Mandatory)][string] $FunctionName
    )

    $match = [regex]::Match(
        $Source,
        "(?ms)static\s+bool\s+$([regex]::Escape($FunctionName))\s*\([^)]*\)\s*\{(?<body>.*?)^\s*\}")
    if (-not $match.Success) {
        throw "Exact SDL sourceRef does not contain the expected $FunctionName function shape."
    }

    return $match.Groups['body'].Value
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$sdlComponents = @($manifest.components | Where-Object id -EQ 'SDL')
if ($sdlComponents.Count -ne 1) {
    throw 'Release manifest must declare component SDL exactly once.'
}

$sourceRef = [string]$sdlComponents[0].sourceRef
if ($sourceRef -notmatch '^[0-9a-f]{40}$') {
    throw "SDL sourceRef must be a full 40-character lowercase commit SHA: $sourceRef"
}

if (-not $SourceRoot) {
    $SourceRoot = Resolve-ReleasePath (Join-Path ([string]$manifest.sourceRoot) ([string]$sdlComponents[0].sourceFolder))
}
if (-not (Test-Path -LiteralPath $SourceRoot -PathType Container)) {
    throw "SDL source repository was not found: $SourceRoot"
}

$script:resolvedSourceRoot = (Resolve-Path -LiteralPath $SourceRoot).Path
$null = Invoke-SourceGit -Arguments @('cat-file', '-e', "$sourceRef`^{commit}") -Description 'SDL sourceRef lookup'
$metalSource = Invoke-SourceGit -Arguments @('show', '--no-textconv', "${sourceRef}:src/gpu/metal/SDL_gpu_metal.m") -Description 'Exact SDL Metal GPU source read'
$busyBody = Get-FunctionBody -Source $metalSource -FunctionName 'METAL_INTERNAL_IsFenceBusy'
$queryBody = Get-FunctionBody -Source $metalSource -FunctionName 'METAL_QueryFence'

if ($busyBody -notmatch '(?s)return\s+status\s*==\s*MTLCommandBufferStatusCommitted\s*\|\|\s*status\s*==\s*MTLCommandBufferStatusScheduled\s*;') {
    throw 'Exact SDL sourceRef no longer defines METAL_INTERNAL_IsFenceBusy as committed-or-scheduled.'
}
if ($queryBody -notmatch '(?s)return\s+!\s*METAL_INTERNAL_IsFenceBusy\s*\(\s*metalFence\s*\)\s*;') {
    throw 'Exact SDL sourceRef has the METAL_QueryFence completion-semantics regression: it must negate METAL_INTERNAL_IsFenceBusy.'
}

Write-Host "SDL Metal fence completion semantics are valid at exact sourceRef $sourceRef."
