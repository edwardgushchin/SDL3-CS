#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}

$rids = @($manifest.rids | ForEach-Object { $_.rid })
$nativePackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision 1 | Where-Object {
    $_.Kind -eq 'native' -and $Components -contains $_.VersionComponent
})
$targetsRelativePath = '..\..\.github\release-tools\NativePackageRidItems.targets'
$targetsPath = Resolve-ReleasePath '.github/release-tools/NativePackageRidItems.targets'
$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

if (-not (Test-Path -LiteralPath $targetsPath -PathType Leaf)) {
    throw "Shared native package RID items target is missing: $targetsPath"
}

[xml] $targetsXml = Get-Content -LiteralPath $targetsPath -Raw -Encoding UTF8

foreach ($rid in $rids) {
    $include = "`$(MSBuildProjectDirectory)\lib\$rid\**\*"
    $packagePath = "runtimes\$rid\native\%(RecursiveDir)"
    $matches = @($targetsXml.Project.ItemGroup.None | Where-Object {
        $_.Include -eq $include -and
        $_.PackagePath -eq $packagePath -and
        ($_.Pack -eq 'True' -or $_.Pack -eq 'true')
    })

    if ($matches.Count -ne 1) {
        $errors.Add("NativePackageRidItems.targets must pack '$include' to '$packagePath' exactly once; found $($matches.Count).")
    }
}

$unexpectedRidEntries = @($targetsXml.Project.ItemGroup.None | Where-Object {
    $_.Include -like '$(MSBuildProjectDirectory)\lib\*\**\*' -and $_.PackagePath -like 'runtimes\*\native\%(RecursiveDir)'
} | ForEach-Object {
    $includeRid = $_.Include.Substring('$(MSBuildProjectDirectory)\lib\'.Length)
    $includeRid = $includeRid.Substring(0, $includeRid.IndexOf('\'))
    $packageRid = $_.PackagePath.Substring('runtimes\'.Length)
    $packageRid = $packageRid.Substring(0, $packageRid.IndexOf('\'))
    if ($includeRid -ne $packageRid -or $rids -notcontains $includeRid) {
        [pscustomobject]@{
            Include = $_.Include
            PackagePath = $_.PackagePath
            IncludeRid = $includeRid
            PackageRid = $packageRid
        }
    }
})

foreach ($entry in $unexpectedRidEntries) {
    $errors.Add("NativePackageRidItems.targets has unexpected RID pack entry Include='$($entry.Include)' PackagePath='$($entry.PackagePath)'.")
}

foreach ($package in $nativePackages) {
    $packageProject = Resolve-ReleasePath $package.Project

    try {
        [xml] $projectXml = Get-Content -LiteralPath $packageProject -Raw -Encoding UTF8
        if ($projectXml.Project -eq $null) {
            throw "$($package.Project) does not have a Project root element."
        }

        $imports = @($projectXml.Project.Import | Where-Object {
            $_.Project -eq $targetsRelativePath
        })

        if ($imports.Count -ne 1) {
            $errors.Add("$($package.Project) must import $targetsRelativePath exactly once; found $($imports.Count).")
        }

        $nativePackagePlatform = $null
        foreach ($propertyGroup in @($projectXml.Project.PropertyGroup)) {
            if ($propertyGroup.NativePackagePlatform) {
                $nativePackagePlatform = [string] $propertyGroup.NativePackagePlatform
                break
            }
        }

        if ($nativePackagePlatform -ne $package.NativePackagePlatform) {
            $errors.Add("$($package.Project) NativePackagePlatform '$nativePackagePlatform' must be '$($package.NativePackagePlatform)'.")
        }

        $rows.Add([pscustomobject]@{
            Component = $package.VersionComponent
            PackageId = $package.Id
            Rids = @($package.Rids).Count
            SharedTargets = if ($imports.Count -eq 1) { 'imported' } else { 'missing' }
            Platform = $package.NativePackagePlatform
            Status = if ($imports.Count -eq 1 -and $unexpectedRidEntries.Count -eq 0 -and $nativePackagePlatform -eq $package.NativePackagePlatform) { 'checked' } else { 'invalid' }
        })
    }
    catch {
        $errors.Add($_.Exception.Message)
        $rows.Add([pscustomobject]@{
            Component = $package.VersionComponent
            PackageId = $package.Id
            Rids = @($package.Rids).Count
            SharedTargets = 'unknown'
            Platform = $package.NativePackagePlatform
            Status = 'invalid'
        })
    }
}

if ((@($manifest.components | Where-Object { $_.id -eq 'SDL' }).Count -eq 1) -and ($rids -contains 'tvossimulator-arm64')) {
    $collectTempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "sdl3cs-collect-native-artifacts-$([System.Guid]::NewGuid().ToString('N'))"

    try {
        $collectManifestPath = Join-Path $collectTempRoot 'release-manifest.json'
        $collectInstallRoot = Join-Path $collectTempRoot 'empty-install'
        $collectPackageDir = Join-Path $collectTempRoot 'SDL3-CS.tvOS'
        $collectPackageProject = Join-Path $collectPackageDir 'SDL3-CS.tvOS.csproj'

        New-Item -ItemType Directory -Force -Path $collectInstallRoot, $collectPackageDir | Out-Null
        Set-Content -LiteralPath $collectPackageProject -Value '<Project Sdk="Microsoft.NET.Sdk" />' -Encoding UTF8

        $collectManifest = Get-Content -LiteralPath (Resolve-ReleasePath $ManifestPath) -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 64
        $collectTvOsPlatform = @($collectManifest.nativePackagePlatforms | Where-Object { $_.id -eq 'tvOS' })[0]
        if (-not $collectTvOsPlatform.PSObject.Properties.Name.Contains('packageProjectOverrides')) {
            $collectTvOsPlatform | Add-Member -MemberType NoteProperty -Name packageProjectOverrides -Value ([pscustomobject]@{})
        }

        $collectTvOsPlatform.packageProjectOverrides | Add-Member -MemberType NoteProperty -Name SDL -Value $collectPackageProject -Force
        $collectManifest | ConvertTo-Json -Depth 64 | Set-Content -LiteralPath $collectManifestPath -Encoding UTF8

        & (Join-Path $PSScriptRoot 'Collect-NativeArtifacts.ps1') `
            -Component SDL `
            -Rid tvossimulator-arm64 `
            -ManifestPath $collectManifestPath `
            -InstallRoot $collectInstallRoot `
            -AllowEmpty `
            -DryRun *> $null
    }
    catch {
        $errors.Add("Collect-NativeArtifacts.ps1 must support missing destination parents in dry-run: $($_.Exception.Message)")
    }
    finally {
        if (Test-Path -LiteralPath $collectTempRoot) {
            Remove-Item -LiteralPath $collectTempRoot -Recurse -Force
        }
    }
}

$rows | Sort-Object Platform, Component | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native package RID pack entry validation failed with $($errors.Count) error(s)."
}

Write-Host "Native package RID pack entries are valid for $($nativePackages.Count) package project(s) and $($rids.Count) RID(s)."
