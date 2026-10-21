#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $SourceRoot = (Join-Path $PSScriptRoot '../../SDL3-CS'),
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$manifest = Get-Content -LiteralPath $ManifestPath -Raw | ConvertFrom-Json
$versions = @{}
foreach ($component in $manifest.components) {
    $versions[$component.id] = [version] $component.nativeVersion
}
if (-not (Test-Path -LiteralPath $SourceRoot -PathType Container)) {
    throw "Wrapper source root not found: $SourceRoot"
}
$files = @(Get-ChildItem -LiteralPath $SourceRoot -Recurse -File -Filter '*.cs' |
    Where-Object FullName -NotMatch '[/\\](bin|obj)[/\\]')
if ($files.Count -eq 0) { throw 'No wrapper source files found.' }
$failures = @(
    foreach ($file in $files) {
        $source = Get-Content -LiteralPath $file.FullName -Raw
        foreach ($since in [regex]::Matches($source, '(?s)<since>(.*?)</since>')) {
            foreach ($match in [regex]::Matches($since.Groups[1].Value, '\b(SDL(?:_[A-Za-z]+)?)\s+(\d+\.\d+\.\d+)')) {
                $component = $match.Groups[1].Value
                $required = [version] $match.Groups[2].Value
                if (-not $versions.ContainsKey($component)) {
                    throw "Unknown since component $component in $($file.FullName)."
                }
                if ($required -gt $versions[$component]) {
                    "$($file.FullName): requires $component $required, pinned $($versions[$component])"
                }
            }
        }
    }
)
if ($failures.Count -gt 0) {
    throw "Stable API boundary violation(s):`n$($failures -join "`n")"
}
Write-Host "Stable API since versions match the manifest ($($files.Count) wrapper source files)."
