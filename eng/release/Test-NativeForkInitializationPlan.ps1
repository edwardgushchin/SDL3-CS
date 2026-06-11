#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Add-InitializationPlanError {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:errors.Add($Message)
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$errors = New-Object System.Collections.Generic.List[string]

foreach ($component in $manifest.components) {
    if (-not $component.PSObject.Properties.Name.Contains('sourceRef') -or -not $component.sourceRef) {
        Add-InitializationPlanError "Component $($component.id) has no sourceRef."
    }
    elseif ($component.sourceRef -notmatch '^[0-9a-fA-F]{40}$') {
        Add-InitializationPlanError "Component $($component.id) sourceRef must be a full 40-character commit SHA."
    }
}

$scratchSourceRoot = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) "native-forks-initialization-plan/$([guid]::NewGuid().ToString('N'))"
$dryRunSucceeded = $true
try {
    $output = & (Join-Path $PSScriptRoot 'Initialize-NativeForks.ps1') `
        -ManifestPath $ManifestPath `
        -SourceRoot $scratchSourceRoot `
        -Depth 1 `
        -DryRun *>&1
}
catch {
    $dryRunSucceeded = $false
    $output = @($_)
}

if (-not $dryRunSucceeded) {
    Add-InitializationPlanError "Initialize-NativeForks.ps1 -DryRun failed: $($output | Out-String)"
}

$text = ($output | Out-String)
foreach ($component in $manifest.components) {
    $expectedClone = "git clone --recursive $($component.repository)"
    if (-not $text.Contains($expectedClone, [System.StringComparison]::Ordinal)) {
        Add-InitializationPlanError "Fresh clone plan for $($component.id) is missing expected clone command: $expectedClone"
    }

    $expectedCheckout = "checkout --detach $($component.sourceRef)"
    if (-not $text.Contains($expectedCheckout, [System.StringComparison]::Ordinal)) {
        Add-InitializationPlanError "Fresh clone plan for $($component.id) is missing pinned checkout: $expectedCheckout"
    }
}

$depthCloneRows = @($text -split '\r?\n' | Where-Object {
    $_.Contains('git clone', [System.StringComparison]::Ordinal) -and
    $_.Contains('--depth', [System.StringComparison]::Ordinal)
})
if ($depthCloneRows.Count -gt 0) {
    Add-InitializationPlanError "Pinned commit SHA clone plan must not use shallow clone depth: $($depthCloneRows -join '; ')"
}

if (Test-Path -LiteralPath $scratchSourceRoot) {
    Add-InitializationPlanError "Dry-run initialization unexpectedly created source root: $scratchSourceRoot"
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native fork initialization plan validation failed with $($errors.Count) error(s)."
}

Write-Host "Native fork initialization plan is valid for $($manifest.components.Count) pinned source ref(s)."
