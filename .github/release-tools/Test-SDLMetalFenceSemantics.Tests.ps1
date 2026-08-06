#requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$validator = Join-Path $PSScriptRoot 'Test-SDLMetalFenceSemantics.ps1'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$ciPath = Join-Path $repositoryRoot '.github/workflows/ci.yml'
$readinessPath = Join-Path $PSScriptRoot 'Test-ReleaseReadiness.ps1'
$manifestValidatorPath = Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1'
$releaseManifestPath = Join-Path $PSScriptRoot 'release-manifest.json'

foreach ($requiredPath in @($validator, $ciPath, $readinessPath, $manifestValidatorPath, $releaseManifestPath)) {
    if (-not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) {
        throw "SDL Metal fence semantics test dependency was not found: $requiredPath"
    }
}

function Invoke-FixtureGit {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string[]] $Arguments
    )

    $output = @(& git -C $Repository @Arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Arguments -join ' ') failed in fixture repository: $($output -join [Environment]::NewLine)"
    }

    return ($output -join "`n").Trim()
}

function Assert-ValidationFails {
    param(
        [Parameter(Mandatory)][string] $Description,
        [Parameter(Mandatory)][string] $ExpectedMessage,
        [Parameter(Mandatory)][scriptblock] $Action
    )

    $failed = $false
    $failureMessage = ''
    try {
        & $Action
    }
    catch {
        $failed = $true
        $failureMessage = $_.Exception.Message
    }

    if (-not $failed) {
        throw "Expected SDL Metal fence semantics validation to fail: $Description"
    }
    if (-not $failureMessage.Contains($ExpectedMessage, [System.StringComparison]::Ordinal)) {
        throw "SDL Metal fence semantics validation failed for an unexpected reason ($Description): $failureMessage"
    }
}

function Assert-Contains {
    param(
        [Parameter(Mandatory)][string] $Text,
        [Parameter(Mandatory)][string] $Expected,
        [Parameter(Mandatory)][string] $Description
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        throw "$Description is missing expected text: $Expected"
    }
}

function Get-MetalSource {
    param([Parameter(Mandatory)][bool] $Corrected)

    $queryReturn = if ($Corrected) {
        '    return !METAL_INTERNAL_IsFenceBusy(metalFence);'
    }
    else {
        '    return METAL_INTERNAL_IsFenceBusy(metalFence);'
    }

    return @"
static bool METAL_INTERNAL_IsFenceBusy(
    MetalFence *fence
)
{
    MTLCommandBufferStatus status = fence->commandBuffer.status;
    return status == MTLCommandBufferStatusCommitted || status == MTLCommandBufferStatusScheduled;
}

static bool METAL_QueryFence(
    SDL_GPURenderer *driverData,
    SDL_GPUFence *fence)
{
    MetalFence *metalFence = (MetalFence *)fence;
$queryReturn
}
"@
}

function Write-FixtureManifest {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $SourceRef,
        [Parameter(Mandatory)][string] $ParentSourceRef,
        [Parameter(Mandatory)][string] $ImmutableTag,
        [switch] $OmitProvenance
    )

    $component = [ordered]@{
        id = 'SDL'
        repository = 'https://github.com/example/SDL'
        upstreamRepository = 'https://github.com/libsdl-org/SDL'
        sourceRef = $SourceRef
        sourceFolder = 'SDL'
        nativeVersion = '3.4.14'
    }

    if (-not $OmitProvenance) {
        $component.sourceProvenance = [ordered]@{
            kind = 'downstream-patch'
            upstream = [ordered]@{
                repository = 'https://github.com/libsdl-org/SDL'
                tag = 'release-3.4.14'
                sourceRef = $script:baselineSourceRef
            }
            parentSourceRef = $ParentSourceRef
            immutableTag = $ImmutableTag
            issue = 'https://github.com/edwardgushchin/SDL3-CS/issues/257'
        }
    }

    [ordered]@{
        schemaVersion = 1
        sourceRoot = 'native-forks'
        components = @($component)
    } | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

$tempParent = Join-Path $repositoryRoot '.agents/sdl-metal-fence-tests'
$tempRoot = Join-Path $tempParent ([guid]::NewGuid().ToString('N'))
$fixtureRepository = Join-Path $tempRoot 'SDL'

try {
    New-Item -ItemType Directory -Force -Path (Join-Path $fixtureRepository 'src/gpu/metal') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('init', '--quiet') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('config', 'user.name', 'SDL3-CS Test') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('config', 'user.email', 'sdl3-cs-test@example.invalid') | Out-Null

    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $false)
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('add', 'src/gpu/metal/SDL_gpu_metal.m') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('commit', '--quiet', '-m', 'upstream baseline') | Out-Null
    $script:baselineSourceRef = Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('rev-parse', 'HEAD')
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('tag', 'release-3.4.14', $script:baselineSourceRef) | Out-Null

    Set-Content -LiteralPath (Join-Path $fixtureRepository 'bad-patch.txt') -Encoding utf8NoBOM -Value 'The regression remains present.'
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('add', 'bad-patch.txt') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('commit', '--quiet', '-m', 'ineffective downstream patch') | Out-Null
    $badSourceRef = Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('rev-parse', 'HEAD')
    $badTag = 'sdl3-cs-test-bad'
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('tag', $badTag, $badSourceRef) | Out-Null

    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('switch', '--quiet', '--detach', $script:baselineSourceRef) | Out-Null
    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $true)
    Set-Content -LiteralPath (Join-Path $fixtureRepository 'unrelated-change.txt') -Encoding utf8NoBOM -Value 'This change must make the source patch fail closed.'
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('add', 'src/gpu/metal/SDL_gpu_metal.m', 'unrelated-change.txt') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('commit', '--quiet', '-m', 'fix fence and change unrelated source') | Out-Null
    $extraChangeSourceRef = Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('rev-parse', 'HEAD')
    $extraChangeTag = 'sdl3-cs-test-extra-change'
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('tag', $extraChangeTag, $extraChangeSourceRef) | Out-Null

    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('switch', '--quiet', '--detach', $script:baselineSourceRef) | Out-Null
    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $true)
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('add', 'src/gpu/metal/SDL_gpu_metal.m') | Out-Null
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('commit', '--quiet', '-m', 'fix Metal fence completion semantics') | Out-Null
    $goodSourceRef = Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('rev-parse', 'HEAD')
    $goodTag = 'sdl3-cs-test-good'
    Invoke-FixtureGit -Repository $fixtureRepository -Arguments @('tag', $goodTag, $goodSourceRef) | Out-Null

    $goodManifest = Join-Path $tempRoot 'good-manifest.json'
    Write-FixtureManifest -Path $goodManifest -SourceRef $goodSourceRef -ParentSourceRef $script:baselineSourceRef -ImmutableTag $goodTag
    & $validator -ManifestPath $goodManifest -SourceRoot $fixtureRepository

    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $false)
    & $validator -ManifestPath $goodManifest -SourceRoot $fixtureRepository

    $badManifest = Join-Path $tempRoot 'bad-manifest.json'
    Write-FixtureManifest -Path $badManifest -SourceRef $badSourceRef -ParentSourceRef $script:baselineSourceRef -ImmutableTag $badTag
    Assert-ValidationFails -Description 'unnegated METAL_QueryFence regression at the exact sourceRef' -ExpectedMessage 'completion-semantics regression' -Action {
        & $validator -ManifestPath $badManifest -SourceRoot $fixtureRepository *> $null
    }

    $extraChangeManifest = Join-Path $tempRoot 'extra-change-manifest.json'
    Write-FixtureManifest -Path $extraChangeManifest -SourceRef $extraChangeSourceRef -ParentSourceRef $script:baselineSourceRef -ImmutableTag $extraChangeTag
    Assert-ValidationFails -Description 'unrelated file beside the corrected Metal fence source' -ExpectedMessage 'must change only src/gpu/metal/SDL_gpu_metal.m' -Action {
        & $validator -ManifestPath $extraChangeManifest -SourceRoot $fixtureRepository *> $null
    }

    $missingProvenanceManifest = Join-Path $tempRoot 'missing-provenance-manifest.json'
    Write-FixtureManifest -Path $missingProvenanceManifest -SourceRef $goodSourceRef -ParentSourceRef $script:baselineSourceRef -ImmutableTag $goodTag -OmitProvenance
    Assert-ValidationFails -Description 'missing downstream source provenance' -ExpectedMessage 'SDL component must declare sourceProvenance.' -Action {
        & $validator -ManifestPath $missingProvenanceManifest -SourceRoot $fixtureRepository *> $null
    }

    $wrongParentManifest = Join-Path $tempRoot 'wrong-parent-manifest.json'
    Write-FixtureManifest -Path $wrongParentManifest -SourceRef $goodSourceRef -ParentSourceRef $badSourceRef -ImmutableTag $goodTag
    Assert-ValidationFails -Description 'sourceRef parent that does not match provenance' -ExpectedMessage 'parentSourceRef must match its upstream baseline sourceRef' -Action {
        & $validator -ManifestPath $wrongParentManifest -SourceRoot $fixtureRepository *> $null
    }

    $ciText = Get-Content -LiteralPath $ciPath -Raw -Encoding UTF8
    $readinessText = Get-Content -LiteralPath $readinessPath -Raw -Encoding UTF8
    $manifestValidatorText = Get-Content -LiteralPath $manifestValidatorPath -Raw -Encoding UTF8
    & $manifestValidatorPath -ManifestPath $releaseManifestPath -SkipSourceCheckoutValidation
    Assert-Contains -Text $ciText -Expected './.github/release-tools/Test-SDLMetalFenceSemantics.Tests.ps1' -Description 'CI source regression test gate'
    Assert-Contains -Text $readinessText -Expected 'Test-SDLMetalFenceSemantics.ps1' -Description 'release readiness exact-source gate'
    Assert-Contains -Text $manifestValidatorText -Expected 'sourceProvenance' -Description 'release manifest provenance schema gate'

    Write-Host 'SDL Metal fence semantics tests passed.'
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedParent = [System.IO.Path]::GetFullPath($tempParent)
        $resolvedTemp = [System.IO.Path]::GetFullPath($tempRoot)
        $relative = [System.IO.Path]::GetRelativePath($resolvedParent, $resolvedTemp)
        if ([System.IO.Path]::IsPathRooted($relative) -or $relative.StartsWith('..')) {
            throw "Unsafe SDL Metal fence test cleanup path: $resolvedTemp"
        }

        Remove-Item -LiteralPath $resolvedTemp -Recurse -Force
    }
}
