#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$root = (Resolve-Path -LiteralPath $RepositoryRoot).Path
$requiredFiles = @{
    Agents = 'AGENTS.md'
    Releasing = 'RELEASING.md'
    Contributing = 'CONTRIBUTING.md'
    Readme = 'README.md'
    Ci = '.github\workflows\ci.yml'
}
$content = @{}
$errors = [System.Collections.Generic.List[string]]::new()

foreach ($entry in $requiredFiles.GetEnumerator()) {
    $path = Join-Path $root $entry.Value
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
        $errors.Add("Required lifecycle file is missing: $($entry.Value)")
        continue
    }

    $content[$entry.Key] = Get-Content -LiteralPath $path -Raw
}

if ($errors.Count -eq 0) {
    if ($content.Agents -notmatch '(?m)^## Short-Lived SDL3-CS Branch Lifecycle\s*$') {
        $errors.Add('AGENTS.md does not define the short-lived SDL3-CS branch lifecycle.')
    }
    if ($content.Agents -notmatch 'exact verified commit from `main`') {
        $errors.Add('AGENTS.md does not require an exact verified commit from main.')
    }
    if ($content.Agents -match '(?m)^## Release Branch Mainline Parity\s*$') {
        $errors.Add('AGENTS.md still defines persistent release-branch parity.')
    }

    if ($content.Releasing -notmatch 'short-lived topic branch') {
        $errors.Add('RELEASING.md does not require short-lived topic branches.')
    }
    if ($content.Releasing -notmatch 'exact verified commit from `main`') {
        $errors.Add('RELEASING.md does not identify the exact verified main commit as the release source.')
    }
    if ($content.Releasing -match 'active `release-\*` branch|verified release branch') {
        $errors.Add('RELEASING.md still requires a persistent wrapper release branch.')
    }

    if ($content.Contributing -notmatch 'Pull requests (?:should )?target `main`') {
        $errors.Add('CONTRIBUTING.md does not direct pull requests to main.')
    }
    if ($content.Readme -match '(?i)release branch') {
        $errors.Add('README.md still describes a wrapper release branch.')
    }

    if ($content.Ci -notmatch '(?m)^\s*- main\s*$') {
        $errors.Add('CI does not target main.')
    }
    if ($content.Ci -match '(?m)^\s*- ["'']?release-\*["'']?\s*$') {
        $errors.Add('CI still targets persistent release-* wrapper branches.')
    }
}

if ($errors.Count -gt 0) {
    throw "Trunk-based release lifecycle validation failed:`n- $($errors -join "`n- ")"
}

[ordered]@{
    Status = 'Passed'
    RepositoryRoot = $root
    CheckedFiles = $requiredFiles.Values.Count
} | ConvertTo-Json -Compress
