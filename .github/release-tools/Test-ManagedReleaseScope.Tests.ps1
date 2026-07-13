#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

$scopeScript = Join-Path $PSScriptRoot 'Test-ManagedReleaseScope.ps1'
if (-not (Test-Path -LiteralPath $scopeScript -PathType Leaf)) {
    throw "Managed release scope helper was not found: $scopeScript"
}

function Invoke-TestGit {
    param(
        [Parameter(Mandatory)][string] $RepositoryRoot,
        [Parameter(Mandatory)][string[]] $Arguments
    )

    $output = & git -C $RepositoryRoot @Arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Arguments -join ' ') failed: $($output | Out-String)"
    }
}

function Assert-ScopeFails {
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
        throw "Expected managed release scope validation to fail: $Description"
    }
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = Join-Path $tempBase "sdl3-cs-managed-release-scope-$([guid]::NewGuid().ToString('N'))"
$tempRoot = [System.IO.Path]::GetFullPath($tempRoot)
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary repository path: $tempRoot"
}

New-Item -ItemType Directory -Force -Path (Join-Path $tempRoot 'SDL3-CS') | Out-Null
New-Item -ItemType Directory -Force -Path (Join-Path $tempRoot 'SDL3-CS.NativePackages/Test') | Out-Null
New-Item -ItemType Directory -Force -Path (Join-Path $tempRoot '.github/release-tools') | Out-Null

try {
    Set-Content -LiteralPath (Join-Path $tempRoot 'SDL3-CS/Wrapper.cs') -Value 'managed-v1' -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $tempRoot 'SDL3-CS.NativePackages/Test/payload.txt') -Value 'native-v1' -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $tempRoot '.github/release-tools/release-manifest.json') -Value '{"native":"v1"}' -Encoding UTF8

    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('init', '--initial-branch=main')
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('config', 'user.email', 'managed-release-tests@example.invalid')
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('config', 'user.name', 'Managed Release Tests')
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('add', '.')
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('commit', '-m', 'base')
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('tag', 'base-release')

    Set-Content -LiteralPath (Join-Path $tempRoot 'SDL3-CS/Wrapper.cs') -Value 'managed-v2' -Encoding UTF8
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('add', 'SDL3-CS/Wrapper.cs')
    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('commit', '-m', 'managed change')

    & $scopeScript -RepositoryRoot $tempRoot -BaseRef 'base-release' -ManifestPath $ManifestPath -PackageRevision 2 -NativePackageRevision 1

    Assert-ScopeFails -Description 'missing base ref' -Action {
        & $scopeScript -RepositoryRoot $tempRoot -BaseRef 'missing-release' -ManifestPath $ManifestPath -PackageRevision 2 -NativePackageRevision 1
    }

    Set-Content -LiteralPath (Join-Path $tempRoot '.github/release-tools/release-manifest.json') -Value '{"native":"dirty"}' -Encoding UTF8
    Assert-ScopeFails -Description 'unstaged native release manifest change' -Action {
        & $scopeScript -RepositoryRoot $tempRoot -BaseRef 'base-release' -ManifestPath $ManifestPath -PackageRevision 2 -NativePackageRevision 1
    }
    Set-Content -LiteralPath (Join-Path $tempRoot '.github/release-tools/release-manifest.json') -Value '{"native":"v1"}' -Encoding UTF8

    Set-Content -LiteralPath (Join-Path $tempRoot 'SDL3-CS.NativePackages/Test/payload.txt') -Value 'native-dirty' -Encoding UTF8
    Assert-ScopeFails -Description 'unstaged native package change' -Action {
        & $scopeScript -RepositoryRoot $tempRoot -BaseRef 'base-release' -ManifestPath $ManifestPath -PackageRevision 2 -NativePackageRevision 1
    }

    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('add', 'SDL3-CS.NativePackages/Test/payload.txt')
    Assert-ScopeFails -Description 'staged native package change' -Action {
        & $scopeScript -RepositoryRoot $tempRoot -BaseRef 'base-release' -ManifestPath $ManifestPath -PackageRevision 2 -NativePackageRevision 1
    }

    Invoke-TestGit -RepositoryRoot $tempRoot -Arguments @('commit', '-m', 'native change')
    Assert-ScopeFails -Description 'committed native package change' -Action {
        & $scopeScript -RepositoryRoot $tempRoot -BaseRef 'base-release' -ManifestPath $ManifestPath -PackageRevision 2 -NativePackageRevision 1
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-managed-release-scope-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary repository path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Managed release scope tests passed.'
