#requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$validator = Join-Path $PSScriptRoot 'Test-SDLMetalFenceSemantics.ps1'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$tempParent = Join-Path $repositoryRoot '.agents/sdl-metal-fence-tests'
$tempRoot = Join-Path $tempParent ([guid]::NewGuid().ToString('N'))
$fixtureRepository = Join-Path $tempRoot 'SDL'

function Invoke-FixtureGit {
    param([Parameter(Mandatory)][string[]] $Arguments)

    $output = @(& git -C $fixtureRepository @Arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Arguments -join ' ') failed in fixture repository: $($output -join [Environment]::NewLine)"
    }
    return ($output -join "`n").Trim()
}

function Get-MetalSource {
    param([Parameter(Mandatory)][bool] $Corrected)

    $queryReturn = if ($Corrected) { '    return !METAL_INTERNAL_IsFenceBusy(metalFence);' } else { '    return METAL_INTERNAL_IsFenceBusy(metalFence);' }
    return @"
static bool METAL_INTERNAL_IsFenceBusy(MetalFence *fence)
{
    MTLCommandBufferStatus status = fence->commandBuffer.status;
    return status == MTLCommandBufferStatusCommitted || status == MTLCommandBufferStatusScheduled;
}

static bool METAL_QueryFence(SDL_GPURenderer *driverData, SDL_GPUFence *fence)
{
    MetalFence *metalFence = (MetalFence *)fence;
$queryReturn
}
"@
}

function Write-FixtureManifest {
    param([Parameter(Mandatory)][string] $Path, [Parameter(Mandatory)][string] $SourceRef)

    [ordered]@{
        schemaVersion = 1
        sourceRoot = 'native-forks'
        components = @([ordered]@{
            id = 'SDL'
            sourceRef = $SourceRef
            sourceFolder = 'SDL'
        })
    } | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Assert-ValidationFails {
    param([Parameter(Mandatory)][string] $ExpectedMessage, [Parameter(Mandatory)][scriptblock] $Action)

    try {
        & $Action
    }
    catch {
        if ($_.Exception.Message.Contains($ExpectedMessage, [System.StringComparison]::Ordinal)) {
            return
        }
        throw "Validation failed for an unexpected reason: $($_.Exception.Message)"
    }
    throw "Expected SDL Metal fence validation to fail with: $ExpectedMessage"
}

try {
    New-Item -ItemType Directory -Force -Path (Join-Path $fixtureRepository 'src/gpu/metal') | Out-Null
    Invoke-FixtureGit @('init', '--quiet') | Out-Null
    Invoke-FixtureGit @('config', 'user.name', 'SDL3-CS Test') | Out-Null
    Invoke-FixtureGit @('config', 'user.email', 'sdl3-cs-test@example.invalid') | Out-Null

    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $false)
    Invoke-FixtureGit @('add', 'src/gpu/metal/SDL_gpu_metal.m') | Out-Null
    Invoke-FixtureGit @('commit', '--quiet', '-m', 'regressed upstream source') | Out-Null
    $badSourceRef = Invoke-FixtureGit @('rev-parse', 'HEAD')

    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $true)
    Invoke-FixtureGit @('add', 'src/gpu/metal/SDL_gpu_metal.m') | Out-Null
    Invoke-FixtureGit @('commit', '--quiet', '-m', 'upstream fence fix') | Out-Null
    $goodSourceRef = Invoke-FixtureGit @('rev-parse', 'HEAD')

    $goodManifest = Join-Path $tempRoot 'good-manifest.json'
    Write-FixtureManifest -Path $goodManifest -SourceRef $goodSourceRef
    & $validator -ManifestPath $goodManifest -SourceRoot $fixtureRepository

    Set-Content -LiteralPath (Join-Path $fixtureRepository 'src/gpu/metal/SDL_gpu_metal.m') -Encoding utf8NoBOM -Value (Get-MetalSource -Corrected $false)
    & $validator -ManifestPath $goodManifest -SourceRoot $fixtureRepository

    $badManifest = Join-Path $tempRoot 'bad-manifest.json'
    Write-FixtureManifest -Path $badManifest -SourceRef $badSourceRef
    Assert-ValidationFails -ExpectedMessage 'completion-semantics regression' -Action {
        & $validator -ManifestPath $badManifest -SourceRoot $fixtureRepository *> $null
    }

    Write-Host 'SDL Metal fence semantics tests passed.'
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $relative = [System.IO.Path]::GetRelativePath([System.IO.Path]::GetFullPath($tempParent), [System.IO.Path]::GetFullPath($tempRoot))
        if ([System.IO.Path]::IsPathRooted($relative) -or $relative.StartsWith('..')) {
            throw "Unsafe SDL Metal fence test cleanup path: $tempRoot"
        }
        Remove-Item -LiteralPath $tempRoot -Recurse -Force
    }
}
