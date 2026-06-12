#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-PackageArtifactLeafPatterns {
    param(
        [Parameter(Mandatory)]
        [string[]] $Patterns
    )

    $leafPatterns = New-Object System.Collections.Generic.List[string]
    foreach ($pattern in $Patterns) {
        $normalized = $pattern.Replace('\', '/')
        $leaf = $normalized.Substring($normalized.LastIndexOf('/') + 1)
        if (-not $leafPatterns.Contains($leaf)) {
            $leafPatterns.Add($leaf)
        }
    }

    return @($leafPatterns)
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

foreach ($componentId in $Components) {
    $component = Get-ReleaseComponent -Manifest $manifest -Component $componentId

    foreach ($rid in $Rids) {
        $packageProject = Resolve-ReleasePath (Get-ReleaseNativePackageProjectForRid -Manifest $manifest -Component $component -Rid $rid)
        $packageRoot = Split-Path -Parent $packageProject
        $packageId = Get-ReleaseNativePackageIdForRid -Manifest $manifest -Component $component -Rid $rid
        $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid
        $artifactKey = Get-ReleaseOsArtifactKey -RidInfo $ridInfo
        $patterns = @($component.artifactPatterns.$artifactKey)
        $leafPatterns = Get-PackageArtifactLeafPatterns -Patterns $patterns
        $ridRoot = Join-Path $packageRoot "lib\$rid"

        foreach ($leafPattern in $leafPatterns) {
            $matches = @(Get-ReleaseFilesByPattern -Root $ridRoot -Pattern $leafPattern)
            $rows.Add([pscustomobject]@{
                Component = $component.id
                PackageId = $packageId
                Rid = $rid
                Pattern = $leafPattern
                Count = $matches.Count
                Path = $ridRoot
            })

            if ($matches.Count -eq 0) {
                $errors.Add("Missing native package artifact for $($component.id)/$rid`: $leafPattern in $ridRoot")
            }
        }
    }
}

$rows | Sort-Object Component, Rid, Pattern | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native package artifact validation failed with $($errors.Count) missing pattern(s)."
}

Write-Host "Native package artifacts are valid."
