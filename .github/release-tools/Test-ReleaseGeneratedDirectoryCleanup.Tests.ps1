#requires -Version 7.0
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$cleanupCommand = Get-Command Remove-ReleaseGeneratedDirectory -CommandType Function -ErrorAction SilentlyContinue
if (-not $cleanupCommand) {
    throw 'Release tooling must provide Remove-ReleaseGeneratedDirectory for fail-closed generated-directory cleanup.'
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-generated-cleanup-test-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe generated-directory cleanup test path: $tempRoot"
}

$reparsePaths = [System.Collections.Generic.List[string]]::new()
try {
    $sourceRoot = Join-Path $tempRoot 'source'
    $buildRoot = Join-Path $tempRoot 'build'
    $dxcRoot = Join-Path $sourceRoot 'external\DirectXShaderCompiler-binaries'
    $downloadRoot = Join-Path $buildRoot '_downloads\DirectXShaderCompiler'
    New-Item -ItemType Directory -Force -Path $sourceRoot, $buildRoot | Out-Null

    foreach ($iteration in 1..2) {
        New-Item -ItemType Directory -Force -Path $dxcRoot, $downloadRoot | Out-Null
        Set-Content -LiteralPath (Join-Path $dxcRoot "stale-payload-$iteration.txt") -Value 'stale payload' -Encoding UTF8
        Set-Content -LiteralPath (Join-Path $downloadRoot "stale-fetch-state-$iteration.txt") -Value 'stale fetch state' -Encoding UTF8

        Remove-ReleaseGeneratedDirectory -RootPath $sourceRoot -TargetPath $dxcRoot -ExpectedRelativePath 'external\DirectXShaderCompiler-binaries' -Description 'DXC payload test directory'
        Remove-ReleaseGeneratedDirectory -RootPath $buildRoot -TargetPath $downloadRoot -ExpectedRelativePath '_downloads\DirectXShaderCompiler' -Description 'DXC FetchContent test directory'

        if ((Test-Path -LiteralPath $dxcRoot) -or (Test-Path -LiteralPath $downloadRoot)) {
            throw "Repeated cleanup iteration $iteration left stale DXC payload or FetchContent state behind."
        }
    }

    $unexpectedRoot = Join-Path $sourceRoot 'external\unexpected'
    New-Item -ItemType Directory -Force -Path $unexpectedRoot | Out-Null
    $rejectedUnexpectedPath = $false
    try {
        Remove-ReleaseGeneratedDirectory -RootPath $sourceRoot -TargetPath $unexpectedRoot -ExpectedRelativePath 'external\DirectXShaderCompiler-binaries' -Description 'unexpected test directory'
    }
    catch {
        $rejectedUnexpectedPath = $true
    }
    if (-not $rejectedUnexpectedPath -or -not (Test-Path -LiteralPath $unexpectedRoot -PathType Container)) {
        throw 'Generated-directory cleanup must reject a target that does not equal the exact expected path.'
    }

    $outsideTraversalRoot = Join-Path $tempRoot 'outside-traversal'
    New-Item -ItemType Directory -Force -Path $outsideTraversalRoot | Out-Null
    $rejectedTraversal = $false
    try {
        Remove-ReleaseGeneratedDirectory -RootPath $sourceRoot -TargetPath $outsideTraversalRoot -ExpectedRelativePath '..\outside-traversal' -Description 'traversal test directory'
    }
    catch {
        $rejectedTraversal = $true
    }
    if (-not $rejectedTraversal -or -not (Test-Path -LiteralPath $outsideTraversalRoot -PathType Container)) {
        throw 'Generated-directory cleanup must reject a canonical target outside its allowed root.'
    }

    $linkType = if ([OperatingSystem]::IsWindows()) { 'Junction' } else { 'SymbolicLink' }
    $targetLinkDestination = Join-Path $tempRoot 'target-link-destination'
    $targetLink = Join-Path $sourceRoot 'external\DirectXShaderCompiler-binaries'
    New-Item -ItemType Directory -Force -Path (Split-Path -Parent $targetLink), $targetLinkDestination | Out-Null
    Set-Content -LiteralPath (Join-Path $targetLinkDestination 'sentinel.txt') -Value 'must remain' -Encoding UTF8
    New-Item -ItemType $linkType -Path $targetLink -Target $targetLinkDestination | Out-Null
    $reparsePaths.Add($targetLink)
    $rejectedTargetReparsePoint = $false
    try {
        Remove-ReleaseGeneratedDirectory -RootPath $sourceRoot -TargetPath $targetLink -ExpectedRelativePath 'external\DirectXShaderCompiler-binaries' -Description 'target reparse-point test directory'
    }
    catch {
        $rejectedTargetReparsePoint = $true
    }
    if (-not $rejectedTargetReparsePoint -or -not (Test-Path -LiteralPath (Join-Path $targetLinkDestination 'sentinel.txt') -PathType Leaf)) {
        throw 'Generated-directory cleanup must fail closed for a target reparse point without touching its destination.'
    }
    [System.IO.Directory]::Delete($targetLink)
    $reparsePaths.Remove($targetLink) | Out-Null

    $ancestorSourceRoot = Join-Path $tempRoot 'ancestor-source'
    $ancestorLinkDestination = Join-Path $tempRoot 'ancestor-link-destination'
    $ancestorLink = Join-Path $ancestorSourceRoot 'external'
    $ancestorDxcRoot = Join-Path $ancestorLink 'DirectXShaderCompiler-binaries'
    New-Item -ItemType Directory -Force -Path $ancestorSourceRoot, (Join-Path $ancestorLinkDestination 'DirectXShaderCompiler-binaries') | Out-Null
    Set-Content -LiteralPath (Join-Path $ancestorLinkDestination 'DirectXShaderCompiler-binaries\sentinel.txt') -Value 'must remain' -Encoding UTF8
    New-Item -ItemType $linkType -Path $ancestorLink -Target $ancestorLinkDestination | Out-Null
    $reparsePaths.Add($ancestorLink)
    $rejectedAncestorReparsePoint = $false
    try {
        Remove-ReleaseGeneratedDirectory -RootPath $ancestorSourceRoot -TargetPath $ancestorDxcRoot -ExpectedRelativePath 'external\DirectXShaderCompiler-binaries' -Description 'ancestor reparse-point test directory'
    }
    catch {
        $rejectedAncestorReparsePoint = $true
    }
    if (-not $rejectedAncestorReparsePoint -or -not (Test-Path -LiteralPath (Join-Path $ancestorLinkDestination 'DirectXShaderCompiler-binaries\sentinel.txt') -PathType Leaf)) {
        throw 'Generated-directory cleanup must fail closed when an ancestor is a reparse point.'
    }
    [System.IO.Directory]::Delete($ancestorLink)
    $reparsePaths.Remove($ancestorLink) | Out-Null
}
finally {
    foreach ($reparsePath in $reparsePaths) {
        $reparseItem = Get-Item -LiteralPath $reparsePath -Force -ErrorAction SilentlyContinue
        if ($reparseItem -and (($reparseItem.Attributes -band [System.IO.FileAttributes]::ReparsePoint) -ne 0)) {
            [System.IO.Directory]::Delete($reparseItem.FullName)
        }
    }

    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        $leaf = [System.IO.Path]::GetFileName($resolvedTempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not $leaf.StartsWith('sdl3-cs-generated-cleanup-test-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe generated-directory cleanup test path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Generated-directory cleanup tests passed.'
