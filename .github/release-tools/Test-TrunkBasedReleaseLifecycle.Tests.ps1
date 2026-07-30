#requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$validator = Join-Path $PSScriptRoot 'Test-TrunkBasedReleaseLifecycle.ps1'
if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Trunk-based lifecycle validator was not found: $validator"
}

$repositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$tempParent = Join-Path $repositoryRoot '.agents\trunk-lifecycle-tests'
$tempRoot = Join-Path $tempParent ([guid]::NewGuid().ToString('N'))

function Write-Fixture {
    param(
        [Parameter(Mandatory)][string] $Root,
        [Parameter(Mandatory)][bool] $TrunkBased
    )

    New-Item -ItemType Directory -Force -Path (Join-Path $Root '.github\workflows') | Out-Null

    if ($TrunkBased) {
        Set-Content -LiteralPath (Join-Path $Root 'AGENTS.md') -Encoding utf8NoBOM -Value @'
## Short-Lived SDL3-CS Branch Lifecycle
Use a short-lived topic branch and release from an exact verified commit from `main`.
'@
        Set-Content -LiteralPath (Join-Path $Root 'RELEASING.md') -Encoding utf8NoBOM -Value @'
## Branches and Main
Create a short-lived topic branch. The release source is the exact verified commit from `main`.
'@
        Set-Content -LiteralPath (Join-Path $Root 'CONTRIBUTING.md') -Encoding utf8NoBOM -Value 'Pull requests target `main`.'
        Set-Content -LiteralPath (Join-Path $Root 'README.md') -Encoding utf8NoBOM -Value 'Published packages can lag behind the development state in `main`.'
        Set-Content -LiteralPath (Join-Path $Root '.github\workflows\ci.yml') -Encoding utf8NoBOM -Value @'
on:
  push:
    branches:
      - main
  pull_request:
    branches:
      - main
'@
    }
    else {
        Set-Content -LiteralPath (Join-Path $Root 'AGENTS.md') -Encoding utf8NoBOM -Value '## Release Branch Mainline Parity'
        Set-Content -LiteralPath (Join-Path $Root 'RELEASING.md') -Encoding utf8NoBOM -Value 'Work on the active `release-*` branch and publish from the verified release branch.'
        Set-Content -LiteralPath (Join-Path $Root 'CONTRIBUTING.md') -Encoding utf8NoBOM -Value 'Pull requests target the current `release-*` branch.'
        Set-Content -LiteralPath (Join-Path $Root 'README.md') -Encoding utf8NoBOM -Value 'Packages can lag behind a release branch.'
        Set-Content -LiteralPath (Join-Path $Root '.github\workflows\ci.yml') -Encoding utf8NoBOM -Value @'
on:
  push:
    branches:
      - main
      - "release-*"
'@
    }
}

try {
    $legacyRoot = Join-Path $tempRoot 'legacy'
    Write-Fixture -Root $legacyRoot -TrunkBased $false
    $legacyFailed = $false
    try {
        & $validator -RepositoryRoot $legacyRoot *> $null
    }
    catch {
        $legacyFailed = $true
    }
    if (-not $legacyFailed) {
        throw 'The validator accepted a persistent release-branch fixture.'
    }

    $trunkRoot = Join-Path $tempRoot 'trunk'
    Write-Fixture -Root $trunkRoot -TrunkBased $true
    $result = & $validator -RepositoryRoot $trunkRoot | ConvertFrom-Json
    if ($result.Status -ne 'Passed') {
        throw "Unexpected validator status: $($result.Status)"
    }

    Write-Host 'Trunk-based release lifecycle tests passed.'
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedParent = [System.IO.Path]::GetFullPath($tempParent)
        $resolvedTemp = [System.IO.Path]::GetFullPath($tempRoot)
        $relative = [System.IO.Path]::GetRelativePath($resolvedParent, $resolvedTemp)
        if ([System.IO.Path]::IsPathRooted($relative) -or $relative.StartsWith('..')) {
            throw "Unsafe test cleanup path: $resolvedTemp"
        }
        Remove-Item -LiteralPath $resolvedTemp -Recurse -Force
    }
}
