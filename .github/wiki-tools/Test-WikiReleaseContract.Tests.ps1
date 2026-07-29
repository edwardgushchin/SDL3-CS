#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

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

$ci = Get-Content -LiteralPath (Join-Path $ProjectRoot '.github/workflows/ci.yml') -Raw
$readiness = Get-Content -LiteralPath (Join-Path $ProjectRoot '.github/release-tools/Test-ReleaseReadiness.ps1') -Raw
$publisher = Get-Content -LiteralPath (Join-Path $ProjectRoot '.github/release-tools/Publish-Release.ps1') -Raw
$policy = Get-Content -LiteralPath (Join-Path $ProjectRoot 'RELEASING.md') -Raw

Assert-Contains -Text $ci -Expected './.github/wiki-tools/Test-WikiTools.Tests.ps1' -Description 'CI Wiki candidate verification'
Assert-Contains -Text $readiness -Expected "Test-WikiTools.Tests.ps1" -Description 'release readiness Wiki tooling verification'
Assert-Contains -Text $publisher -Expected "Test-PublishedApiWiki.ps1" -Description 'production publish Wiki freshness gate'
Assert-Contains -Text $publisher -Expected '-ExpectedManagedVersion $wrapper.PackageVersion' -Description 'Wiki managed version gate'
Assert-Contains -Text $publisher -Expected '-ExpectedSourceCommit $releaseTarget' -Description 'Wiki exact release commit gate'
Assert-Contains -Text $policy -Expected 'GitHub Wiki' -Description 'release policy Wiki section'
Assert-Contains -Text $policy -Expected 'A Wiki publication failure' -Description 'release policy fail-closed Wiki rule'

Write-Host 'Wiki release contract tests passed.'
