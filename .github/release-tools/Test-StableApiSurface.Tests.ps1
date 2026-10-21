#requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$validator = Join-Path $PSScriptRoot 'Test-StableApiSurface.ps1'
$fixture = Join-Path $PSScriptRoot "../../.agents/stable-api-tests/$([guid]::NewGuid().ToString('N'))"
$source = Join-Path $fixture 'source'
$manifest = Join-Path $fixture 'manifest.json'

function Assert-Rejected {
    param([string] $Expected)
    try { & $validator -SourceRoot $source -ManifestPath $manifest *> $null }
    catch {
        if ($_.Exception.Message.Contains($Expected, [StringComparison]::Ordinal)) { return }
        throw
    }
    throw "Expected rejection: $Expected"
}

try {
    New-Item -ItemType Directory -Path $source -Force | Out-Null
    $components = @('SDL', 'SDL_image', 'SDL_mixer', 'SDL_ttf', 'SDL_shadercross')
    @{ components = @($components | ForEach-Object { @{ id = $_; nativeVersion = '3.4.18' } }) } |
        ConvertTo-Json -Depth 5 | Set-Content -LiteralPath $manifest
    $file = Join-Path $source 'PInvoke.cs'
    foreach ($component in $components) {
        "/// <since>This function is available since $component 3.4.18.</since>" | Set-Content -LiteralPath $file
        & $validator -SourceRoot $source -ManifestPath $manifest
        "/// <since>`n/// This function is available since $component 3.6.0.`n/// </since>" | Set-Content -LiteralPath $file
        Assert-Rejected 'Stable API boundary violation'
    }
    '/// <since>This function is available since SDL_unknown 3.0.0.</since>' | Set-Content -LiteralPath $file
    Assert-Rejected 'Unknown since component'
    '/// <since>This function is available since SDL 3.2.0.</since>' | Set-Content -LiteralPath $file
    New-Item -ItemType Directory -Path (Join-Path $source 'obj') | Out-Null
    '/// <since>This function is available since SDL 9.0.0.</since>' | Set-Content -LiteralPath (Join-Path $source 'obj/Generated.cs')
    & $validator -SourceRoot $source -ManifestPath $manifest
    Write-Host 'Stable API boundary regression tests passed (all five components).'
}
finally {
    if (Test-Path -LiteralPath $fixture) { Remove-Item -LiteralPath $fixture -Recurse -Force }
}
