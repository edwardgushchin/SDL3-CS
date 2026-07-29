#requires -Version 7.0
[CmdletBinding()]
param()

$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$validator = Join-Path $PSScriptRoot 'Test-ExampleProjects.ps1'
$ciPath = Join-Path $repoRoot '.github/workflows/ci.yml'

if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Example project validator was not found: $validator"
}

function Assert-TextContains {
    param(
        [Parameter(Mandatory)][string] $Text,
        [Parameter(Mandatory)][string] $Expected,
        [Parameter(Mandatory)][string] $Description
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        throw "$Description is missing expected text: $Expected"
    }
}

function Assert-ValidationFails {
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
        throw "Expected example project validation to fail: $Description"
    }
}

& $validator -ValidateOnly -Scope All

$ciText = Get-Content -LiteralPath $ciPath -Raw -Encoding UTF8
foreach ($expectation in @(
    @{ Text = 'name: Build desktop examples'; Description = 'desktop examples CI job' },
    @{ Text = 'name: Build Android example'; Description = 'Android example CI job' },
    @{ Text = 'dotnet workload install android'; Description = 'Android workload installation' },
    @{ Text = './.github/release-tools/Test-ExampleProjects.ps1 -Configuration Release -Scope Desktop'; Description = 'desktop examples validation command' },
    @{ Text = './.github/release-tools/Test-ExampleProjects.ps1 -Configuration Release -Scope Android'; Description = 'Android example validation command' }
)) {
    Assert-TextContains -Text $ciText -Expected $expectation.Text -Description $expectation.Description
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-example-projects-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary example project path: $tempRoot"
}

$examplesRoot = Join-Path $tempRoot 'Examples'
$includedRoot = Join-Path $examplesRoot 'Included'
$missingRoot = Join-Path $examplesRoot 'Missing'
New-Item -ItemType Directory -Force -Path $includedRoot, $missingRoot | Out-Null

try {
    Set-Content -LiteralPath (Join-Path $includedRoot 'Included.csproj') -Value '<Project Sdk="Microsoft.NET.Sdk" />' -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $missingRoot 'Missing.csproj') -Value '<Project Sdk="Microsoft.NET.Sdk" />' -Encoding UTF8

    $solutionPath = Join-Path $tempRoot 'Examples.sln'
    @'
Microsoft Visual Studio Solution File, Format Version 12.00
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Included", "Examples\Included\Included.csproj", "{11111111-1111-1111-1111-111111111111}"
EndProject
Global
EndGlobal
'@ | Set-Content -LiteralPath $solutionPath -Encoding UTF8

    Assert-ValidationFails -Description 'project exists on disk but is absent from solution' -Action {
        & $validator -ExamplesRoot $examplesRoot -SolutionPath $solutionPath -ValidateOnly -Scope All *> $null
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-example-projects-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary example project path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Example project tests passed.'
