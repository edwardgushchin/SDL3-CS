#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $Component,

    [Parameter(Mandatory)]
    [string] $Rid,

    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $InstallRoot,
    [switch] $CleanDestination,
    [switch] $AllowEmpty,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $Component
$ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $Rid
$repoRoot = Get-ReleaseRepoRoot
$artifactsRoot = Resolve-ReleasePath $manifest.artifactsRoot

if (-not $InstallRoot) {
    $InstallRoot = Join-Path $artifactsRoot "native/$Component/$Rid/install"
}
$InstallRoot = Resolve-ReleasePath $InstallRoot

$projectPath = Resolve-ReleasePath (Get-ReleaseNativePackageProjectForRid -Manifest $manifest -Component $componentInfo -Rid $Rid)
$projectDir = Split-Path -Parent $projectPath
$destination = Join-Path $projectDir "lib/$Rid"
$resolvedProjectDir = (Resolve-Path -LiteralPath $projectDir).Path
$resolvedDestinationParent = (Resolve-Path -LiteralPath (Split-Path -Parent $destination)).Path

if (-not $resolvedDestinationParent.StartsWith($resolvedProjectDir, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Destination parent is outside package project: $resolvedDestinationParent"
}

if ($CleanDestination) {
    if ($DryRun) {
        Write-Host "[dry-run] clean $destination"
    }
    else {
        New-Item -ItemType Directory -Force -Path $destination | Out-Null
        Get-ChildItem -LiteralPath $destination -Force | Remove-Item -Recurse -Force
    }
}
elseif (-not (Test-Path -LiteralPath $destination -PathType Container) -and -not $DryRun) {
    New-Item -ItemType Directory -Force -Path $destination | Out-Null
}

$artifactKey = Get-ReleaseOsArtifactKey -RidInfo $ridInfo
$patterns = @($componentInfo.artifactPatterns.$artifactKey)
$matches = [ordered]@{}

foreach ($pattern in $patterns) {
    foreach ($file in Get-ReleaseFilesByPattern -Root $InstallRoot -Pattern $pattern) {
        $matches[$file.FullName] = $file
    }
}

if ($matches.Count -eq 0 -and -not $AllowEmpty) {
    throw "No runtime artifacts found for component '$Component' RID '$Rid' in '$InstallRoot'."
}

foreach ($file in $matches.Values) {
    $target = Join-Path $destination $file.Name
    if ($DryRun) {
        Write-Host "[dry-run] copy $($file.FullName) -> $target"
        continue
    }

    Copy-Item -LiteralPath $file.FullName -Destination $target -Force
}

Write-Host "Collected $($matches.Count) artifact(s) for $Component/$Rid into $destination."
