#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-RequiredPropertyValue {
    param(
        [Parameter(Mandatory)][object] $InputObject,
        [Parameter(Mandatory)][string] $Name,
        [Parameter(Mandatory)][string] $Description
    )

    $property = $InputObject.PSObject.Properties[$Name]
    if (-not $property -or $null -eq $property.Value -or
        ($property.Value -is [string] -and [string]::IsNullOrWhiteSpace([string]$property.Value))) {
        throw "$Description must declare $Name."
    }

    return $property.Value
}

function Assert-FullCommitSha {
    param(
        [Parameter(Mandatory)][string] $Value,
        [Parameter(Mandatory)][string] $Description
    )

    if ($Value -notmatch '^[0-9a-f]{40}$') {
        throw "$Description must be a full 40-character lowercase commit SHA: $Value"
    }
}

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

    $escapedName = [regex]::Escape($FunctionName)
    $match = [regex]::Match(
        $Source,
        "(?ms)static\s+bool\s+$escapedName\s*\([^)]*\)\s*\{(?<body>.*?)^\s*\}")
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

$sdl = $sdlComponents[0]
$sourceRef = [string](Get-RequiredPropertyValue -InputObject $sdl -Name 'sourceRef' -Description 'SDL component')
Assert-FullCommitSha -Value $sourceRef -Description 'SDL sourceRef'

$provenance = Get-RequiredPropertyValue -InputObject $sdl -Name 'sourceProvenance' -Description 'SDL component'
$kind = [string](Get-RequiredPropertyValue -InputObject $provenance -Name 'kind' -Description 'SDL sourceProvenance')
if ($kind -ne 'downstream-patch') {
    throw "SDL sourceProvenance.kind must be 'downstream-patch', not '$kind'."
}

$upstream = Get-RequiredPropertyValue -InputObject $provenance -Name 'upstream' -Description 'SDL sourceProvenance'
$upstreamRepository = [string](Get-RequiredPropertyValue -InputObject $upstream -Name 'repository' -Description 'SDL sourceProvenance.upstream')
$upstreamTag = [string](Get-RequiredPropertyValue -InputObject $upstream -Name 'tag' -Description 'SDL sourceProvenance.upstream')
$upstreamSourceRef = [string](Get-RequiredPropertyValue -InputObject $upstream -Name 'sourceRef' -Description 'SDL sourceProvenance.upstream')
$parentSourceRef = [string](Get-RequiredPropertyValue -InputObject $provenance -Name 'parentSourceRef' -Description 'SDL sourceProvenance')
$immutableTag = [string](Get-RequiredPropertyValue -InputObject $provenance -Name 'immutableTag' -Description 'SDL sourceProvenance')
$issue = [string](Get-RequiredPropertyValue -InputObject $provenance -Name 'issue' -Description 'SDL sourceProvenance')

Assert-FullCommitSha -Value $upstreamSourceRef -Description 'SDL sourceProvenance.upstream.sourceRef'
Assert-FullCommitSha -Value $parentSourceRef -Description 'SDL sourceProvenance.parentSourceRef'

if ($sdl.PSObject.Properties['upstreamRepository'] -and
    [string]$sdl.upstreamRepository -ne $upstreamRepository) {
    throw 'SDL sourceProvenance.upstream.repository must match SDL upstreamRepository.'
}
if ($sourceRef -eq $upstreamSourceRef) {
    throw 'SDL downstream-patch sourceRef must differ from its upstream baseline.'
}
if ($parentSourceRef -ne $upstreamSourceRef) {
    throw 'SDL sourceProvenance.parentSourceRef must match its upstream baseline sourceRef for this single-commit patch.'
}
if ($issue -notmatch '^https://github\.com/[^/]+/[^/]+/issues/[1-9][0-9]*$') {
    throw "SDL sourceProvenance.issue must be an absolute GitHub issue URL: $issue"
}

if (-not $SourceRoot) {
    $sourceRootPath = Get-RequiredPropertyValue -InputObject $manifest -Name 'sourceRoot' -Description 'Release manifest'
    $sourceFolder = [string](Get-RequiredPropertyValue -InputObject $sdl -Name 'sourceFolder' -Description 'SDL component')
    $SourceRoot = Resolve-ReleasePath (Join-Path ([string]$sourceRootPath) $sourceFolder)
}

if (-not (Test-Path -LiteralPath $SourceRoot -PathType Container)) {
    throw "SDL source repository was not found: $SourceRoot"
}
$script:resolvedSourceRoot = (Resolve-Path -LiteralPath $SourceRoot).Path

$null = Invoke-SourceGit -Arguments @('cat-file', '-e', "$sourceRef`^{commit}") -Description 'SDL sourceRef lookup'
$commitLine = Invoke-SourceGit -Arguments @('rev-list', '--parents', '-n', '1', $sourceRef) -Description 'SDL sourceRef parent lookup'
$commitParts = @($commitLine -split '\s+' | Where-Object { $_ })
if ($commitParts.Count -ne 2) {
    throw "SDL patched sourceRef must have exactly one parent; found $($commitParts.Count - 1)."
}
if ($commitParts[1] -ne $parentSourceRef) {
    throw "SDL patched sourceRef parent is $($commitParts[1]), not manifest parentSourceRef $parentSourceRef."
}

$resolvedImmutableTag = Invoke-SourceGit -Arguments @('rev-parse', "$immutableTag`^{commit}") -Description 'SDL immutable downstream tag lookup'
if ($resolvedImmutableTag -ne $sourceRef) {
    throw "SDL immutable tag '$immutableTag' resolves to $resolvedImmutableTag, not sourceRef $sourceRef."
}

$resolvedUpstreamTag = Invoke-SourceGit -Arguments @('rev-parse', "$upstreamTag`^{commit}") -Description 'SDL upstream baseline tag lookup'
if ($resolvedUpstreamTag -ne $upstreamSourceRef) {
    throw "SDL upstream tag '$upstreamTag' resolves to $resolvedUpstreamTag, not upstream sourceRef $upstreamSourceRef."
}

$sourceObject = "${sourceRef}:src/gpu/metal/SDL_gpu_metal.m"
$metalSource = Invoke-SourceGit -Arguments @('show', '--no-textconv', $sourceObject) -Description 'Exact SDL Metal GPU source read'
$busyBody = Get-FunctionBody -Source $metalSource -FunctionName 'METAL_INTERNAL_IsFenceBusy'
$queryBody = Get-FunctionBody -Source $metalSource -FunctionName 'METAL_QueryFence'

if ($busyBody -notmatch '(?s)return\s+status\s*==\s*MTLCommandBufferStatusCommitted\s*\|\|\s*status\s*==\s*MTLCommandBufferStatusScheduled\s*;') {
    throw 'Exact SDL sourceRef no longer defines METAL_INTERNAL_IsFenceBusy as committed-or-scheduled.'
}
if ($queryBody -notmatch '(?s)return\s+!\s*METAL_INTERNAL_IsFenceBusy\s*\(\s*metalFence\s*\)\s*;') {
    throw 'Exact SDL sourceRef has the METAL_QueryFence completion-semantics regression: it must negate METAL_INTERNAL_IsFenceBusy.'
}

$expectedSourcePath = 'src/gpu/metal/SDL_gpu_metal.m'
$changedPathText = Invoke-SourceGit -Arguments @(
    'diff-tree', '--no-commit-id', '--name-only', '--no-renames', '-r', $sourceRef
) -Description 'SDL downstream patch path lookup'
$changedPaths = @($changedPathText -split '\r?\n' | Where-Object { $_ })
if ($changedPaths.Count -ne 1 -or $changedPaths[0] -ne $expectedSourcePath) {
    throw "SDL downstream patch must change only $expectedSourcePath; found: $($changedPaths -join ', ')"
}

$patchText = Invoke-SourceGit -Arguments @(
    'diff', '--unified=0', '--no-ext-diff', '--no-renames',
    $parentSourceRef, $sourceRef, '--', $expectedSourcePath
) -Description 'SDL downstream patch diff lookup'
$removedLines = @($patchText -split '\r?\n' | Where-Object {
    $_.StartsWith('-', [System.StringComparison]::Ordinal) -and
    -not $_.StartsWith('---', [System.StringComparison]::Ordinal)
})
$addedLines = @($patchText -split '\r?\n' | Where-Object {
    $_.StartsWith('+', [System.StringComparison]::Ordinal) -and
    -not $_.StartsWith('+++', [System.StringComparison]::Ordinal)
})
$expectedRemovedLine = '-    return METAL_INTERNAL_IsFenceBusy(metalFence);'
$expectedAddedLine = '+    return !METAL_INTERNAL_IsFenceBusy(metalFence);'
if ($removedLines.Count -ne 1 -or $removedLines[0] -ne $expectedRemovedLine -or
    $addedLines.Count -ne 1 -or $addedLines[0] -ne $expectedAddedLine) {
    throw 'SDL downstream patch must contain only the exact one-line METAL_QueryFence negation fix.'
}

Write-Host "SDL Metal fence completion semantics are valid at exact sourceRef $sourceRef ($immutableTag)."
