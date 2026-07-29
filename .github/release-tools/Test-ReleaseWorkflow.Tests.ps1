#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $WorkflowPath = '.github/workflows/release-native-packages.yml',
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

$validator = Join-Path $PSScriptRoot 'Test-ReleaseWorkflow.ps1'
if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Release workflow validator was not found: $validator"
}

function Assert-WorkflowValidationFails {
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
        throw "Expected release workflow validation to fail: $Description"
    }
}

$resolvedWorkflow = [System.IO.Path]::GetFullPath($WorkflowPath)
$workflowText = Get-Content -LiteralPath $resolvedWorkflow -Raw -Encoding UTF8
$pinnedLoginPattern = '(?m)^\s+uses:\s+NuGet/login@[0-9a-f]{40}\s+#\s+v1\s*$'
if ($workflowText -notmatch $pinnedLoginPattern) {
    throw 'Release workflow fixture must contain a SHA-pinned NuGet/login v1 action.'
}

& $validator -WorkflowPath $resolvedWorkflow -ManifestPath $ManifestPath

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-release-workflow-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary workflow path: $tempRoot"
}

New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null
$tempWorkflow = Join-Path $tempRoot 'release-native-packages.yml'

try {
    $unpinnedWorkflow = [regex]::Replace(
        $workflowText,
        $pinnedLoginPattern,
        '        uses: NuGet/login@v1'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $unpinnedWorkflow -Encoding UTF8

    Assert-WorkflowValidationFails -Description 'unpinned NuGet/login action' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-release-workflow-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary workflow path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Release workflow tests passed.'
