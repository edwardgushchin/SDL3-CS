#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot,
    [string] $SourceManifestPath = (Join-Path $PSScriptRoot '../../SDL3-CS.NativePackages/ThirdPartySources/SOURCE_MANIFEST.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $SourceRoot) {
    $SourceRoot = Resolve-ReleasePath $manifest.sourceRoot
}
$sourceManifest = Get-Content -LiteralPath $SourceManifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
if ($sourceManifest.schemaVersion -ne 1 -or @($sourceManifest.sources).Count -ne 3) {
    throw 'Expected three versioned LGPL source records.'
}

foreach ($source in $sourceManifest.sources) {
    $component = Get-ReleaseComponent -Manifest $manifest -Component $source.component
    if ($source.componentSourceRef -ne $component.sourceRef) {
        throw "$($source.id) source component revision differs from release manifest."
    }

    $archive = Join-Path (Split-Path -Parent $SourceManifestPath) $source.archive
    if (-not (Test-Path -LiteralPath $archive -PathType Leaf)) {
        throw "$($source.id) source archive is missing: $archive"
    }
    $hash = (Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant()
    if ($hash -ne $source.sha256) {
        throw "$($source.id) source archive SHA-256 mismatch."
    }

    $repository = Join-Path $SourceRoot $component.sourceFolder
    if (-not (Test-Path -LiteralPath $repository -PathType Container)) {
        throw "$($source.id) pinned component source checkout is missing: $repository"
    }
    $head = Invoke-ReleaseGitValue -RepositoryPath $repository -Arguments @('rev-parse', 'HEAD')
    if ($head -ne $source.componentSourceRef) {
        throw "$($source.id) component checkout is at $head instead of $($source.componentSourceRef)."
    }

    if ($source.id -in @('game-music-emu', 'mpg123')) {
        $submodule = if ($source.id -eq 'game-music-emu') { 'external/libgme' } else { 'external/mpg123' }
        $tree = Invoke-ReleaseGitValue -RepositoryPath $repository -Arguments @('ls-tree', 'HEAD', $submodule)
        if ($tree -notmatch '^160000 commit ([0-9a-f]{40})\s') {
            throw "$($source.id) is not a pinned source submodule."
        }
        if ($Matches[1] -ne $source.revision) {
            throw "$($source.id) source archive revision differs from the SDL_mixer gitlink."
        }
    }
    elseif ($source.id -eq 'vkd3d') {
        $cmake = Get-Content -LiteralPath (Join-Path $repository 'CMakeLists.txt') -Raw -Encoding UTF8
        if (-not $cmake.Contains([string] $source.upstream, [System.StringComparison]::Ordinal) -or
            -not $cmake.Contains("SHA256=$($source.sha256)", [System.StringComparison]::Ordinal)) {
            throw 'Vkd3d source URL or digest differs from the pinned SDL_shadercross build.'
        }
    }
    else {
        throw "Unknown LGPL source: $($source.id)"
    }
}

Write-Host 'Three LGPL source archives match pinned component source and SHA-256.'
