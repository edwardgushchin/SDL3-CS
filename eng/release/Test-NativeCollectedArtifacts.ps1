#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $SkipMissingInstallRoots,
    [switch] $PassThru
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

function Get-NativeRuntimeLeafPatterns {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    switch ($RidInfo.os) {
        'windows' { return @('*.dll') }
        'linux' { return @('*.so', '*.so.*') }
        'android' { return @('*.so', '*.so.*') }
        'macos' { return @('*.dylib', '*.dylib.*') }
        'ios' { return @('*.a') }
        'tvos' { return @('*.a') }
        default { throw "Unsupported RID OS '$($RidInfo.os)'." }
    }
}

function Test-NameMatchesAnyPattern {
    param(
        [Parameter(Mandatory)]
        [string] $Name,

        [Parameter(Mandatory)]
        [string[]] $Patterns
    )

    foreach ($pattern in $Patterns) {
        if ($Name -like $pattern) {
            return $true
        }
    }

    return $false
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

$errors = New-Object System.Collections.Generic.List[string]
$details = New-Object System.Collections.Generic.List[object]
$summary = New-Object System.Collections.Generic.List[object]
$artifactsRoot = Resolve-ReleasePath $manifest.artifactsRoot

foreach ($componentId in $Components) {
    $componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $componentId
    $packageProject = Resolve-ReleasePath $componentInfo.packageProject
    $packageRoot = Split-Path -Parent $packageProject

    foreach ($rid in $Rids) {
        $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid
        $artifactKey = Get-ReleaseOsArtifactKey -RidInfo $ridInfo
        $installRoot = Join-Path $artifactsRoot "native/$componentId/$rid/install"
        $ridRoot = Join-Path $packageRoot "lib/$rid"

        if (-not (Test-Path -LiteralPath $installRoot -PathType Container)) {
            $summary.Add([pscustomobject]@{
                Component = $componentId
                Rid = $rid
                Status = if ($SkipMissingInstallRoots) { 'skipped' } else { 'missing-install-root' }
                InstallRuntimeArtifacts = 0
                Collected = 0
                Dependency = 0
                Missing = 0
                InstallRoot = $installRoot
            })

            if (-not $SkipMissingInstallRoots) {
                $errors.Add("Install root is missing for $componentId/$rid`: $installRoot")
            }
            continue
        }

        if (-not (Test-Path -LiteralPath $ridRoot -PathType Container)) {
            $errors.Add("Package RID folder is missing for $componentId/$rid`: $ridRoot")
            continue
        }

        $runtimeLeafPatterns = Get-NativeRuntimeLeafPatterns -RidInfo $ridInfo
        $installRuntimeFiles = @(Get-ChildItem -LiteralPath $installRoot -Recurse -File -Force | Where-Object {
            Test-NameMatchesAnyPattern -Name $_.Name -Patterns $runtimeLeafPatterns
        })

        if ($installRuntimeFiles.Count -eq 0) {
            $errors.Add("No runtime artifacts were installed for $componentId/$rid in $installRoot")
        }

        $packageFilesByName = [System.Collections.Generic.Dictionary[string, object]]::new([System.StringComparer]::OrdinalIgnoreCase)
        foreach ($packageFile in @(Get-ChildItem -LiteralPath $ridRoot -File -Force)) {
            if (-not $packageFilesByName.ContainsKey($packageFile.Name)) {
                $packageFilesByName.Add($packageFile.Name, $packageFile)
            }
        }

        $dependencyLeafPatterns = New-Object System.Collections.Generic.List[string]
        foreach ($dependencyId in @($componentInfo.dependencies)) {
            $dependencyInfo = Get-ReleaseComponent -Manifest $manifest -Component $dependencyId
            foreach ($leafPattern in Get-PackageArtifactLeafPatterns -Patterns @($dependencyInfo.artifactPatterns.$artifactKey)) {
                if (-not $dependencyLeafPatterns.Contains($leafPattern)) {
                    $dependencyLeafPatterns.Add($leafPattern)
                }
            }
        }

        $collectedCount = 0
        $dependencyCount = 0
        $missingCount = 0

        foreach ($installFile in $installRuntimeFiles) {
            $installRelativePath = [System.IO.Path]::GetRelativePath($installRoot, $installFile.FullName).Replace('\', '/')
            $packageRelativePath = $null
            $disposition = 'missing'

            if ($packageFilesByName.ContainsKey($installFile.Name)) {
                $disposition = 'collected'
                $collectedCount++
                $packageFile = $packageFilesByName[$installFile.Name]
                $packageRelativePath = [System.IO.Path]::GetRelativePath($packageRoot, $packageFile.FullName).Replace('\', '/')
            }
            elseif ($dependencyLeafPatterns.Count -gt 0 -and (Test-NameMatchesAnyPattern -Name $installFile.Name -Patterns @($dependencyLeafPatterns))) {
                $disposition = 'dependency'
                $dependencyCount++
            }
            else {
                $missingCount++
                $errors.Add("Installed runtime artifact was not collected for $componentId/$rid`: $installRelativePath")
            }

            $details.Add([pscustomobject]@{
                Component = $componentId
                Rid = $rid
                Name = $installFile.Name
                InstallRelativePath = $installRelativePath
                Disposition = $disposition
                PackageRelativePath = $packageRelativePath
            })
        }

        $summary.Add([pscustomobject]@{
            Component = $componentId
            Rid = $rid
            Status = if ($missingCount -eq 0) { 'passed' } else { 'failed' }
            InstallRuntimeArtifacts = $installRuntimeFiles.Count
            Collected = $collectedCount
            Dependency = $dependencyCount
            Missing = $missingCount
            InstallRoot = $installRoot
        })
    }
}

if ($errors.Count -gt 0) {
    if (-not $PassThru) {
        $summary | Sort-Object Component, Rid | Format-Table -AutoSize
    }
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native collected artifact validation failed with $($errors.Count) issue(s)."
}

if ($PassThru) {
    foreach ($detail in $details) {
        $detail
    }
    return
}

$summary | Sort-Object Component, Rid | Format-Table -AutoSize
Write-Host "Native collected artifacts are valid."
