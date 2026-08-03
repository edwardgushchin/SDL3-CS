#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [int] $BuildParallelLevel = 1
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$expectedAndroidNdkVersion = '28.2.13676358'
if (-not $manifest.PSObject.Properties.Name.Contains('toolchains') -or
    -not $manifest.toolchains.PSObject.Properties.Name.Contains('androidNdkVersion') -or
    [string]$manifest.toolchains.androidNdkVersion -ne $expectedAndroidNdkVersion) {
    throw "Native build plan requires manifest toolchains.androidNdkVersion '$expectedAndroidNdkVersion'."
}
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

foreach ($component in $Components) {
    Get-ReleaseComponent -Manifest $manifest -Component $component | Out-Null
}
foreach ($rid in $Rids) {
    Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
}

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

function Assert-NativePlanContains {
    param(
        [Parameter(Mandatory)]
        [string] $PlanOutput,

        [Parameter(Mandatory)]
        [string] $Expected,

        [Parameter(Mandatory)]
        [string] $Context
    )

    if (-not $PlanOutput.Contains($Expected, [System.StringComparison]::Ordinal)) {
        throw "$Context must contain '$Expected'."
    }
}

function Assert-NativePlanDoesNotContain {
    param(
        [Parameter(Mandatory)]
        [string] $PlanOutput,

        [Parameter(Mandatory)]
        [string] $Unexpected,

        [Parameter(Mandatory)]
        [string] $Context
    )

    if ($PlanOutput.Contains($Unexpected, [System.StringComparison]::Ordinal)) {
        throw "$Context must not contain '$Unexpected'."
    }
}

function Test-AndroidNdkResolutionContract {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest
    )

    $expectedVersion = Get-ReleaseAndroidNdkExpectedVersion -Manifest $Manifest
    $environmentNames = @('ANDROID_NDK_HOME', 'ANDROID_NDK_ROOT', 'ANDROID_HOME', 'ANDROID_SDK_ROOT')
    $savedEnvironment = @{}
    foreach ($environmentName in $environmentNames) {
        $savedEnvironment[$environmentName] = [Environment]::GetEnvironmentVariable($environmentName, [EnvironmentVariableTarget]::Process)
    }

    $tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
    $tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-android-ndk-contract-$([guid]::NewGuid().ToString('N'))"))
    if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Unsafe Android NDK contract test path: $tempRoot"
    }

    function New-AndroidNdkFixture {
        param(
            [Parameter(Mandatory)][string] $Path,
            [Parameter(Mandatory)][string] $Version
        )

        New-Item -ItemType Directory -Force -Path (Join-Path $Path 'build/cmake') | Out-Null
        Set-Content -LiteralPath (Join-Path $Path 'build/cmake/android.toolchain.cmake') -Value '# test toolchain' -Encoding UTF8
        Set-Content -LiteralPath (Join-Path $Path 'source.properties') -Value @(
            'Pkg.Desc = Android NDK',
            "Pkg.Revision = $Version"
        ) -Encoding UTF8
    }

    try {
        $sdkRoot = Join-Path $tempRoot 'sdk'
        $exactNdk = Join-Path $sdkRoot "ndk/$expectedVersion"
        $wrongNdk = Join-Path $tempRoot 'wrong-ndk'
        New-AndroidNdkFixture -Path $exactNdk -Version $expectedVersion
        New-AndroidNdkFixture -Path $wrongNdk -Version '27.2.12479018'

        [Environment]::SetEnvironmentVariable('ANDROID_NDK_HOME', $wrongNdk, [EnvironmentVariableTarget]::Process)
        [Environment]::SetEnvironmentVariable('ANDROID_NDK_ROOT', $null, [EnvironmentVariableTarget]::Process)
        [Environment]::SetEnvironmentVariable('ANDROID_HOME', $sdkRoot, [EnvironmentVariableTarget]::Process)
        [Environment]::SetEnvironmentVariable('ANDROID_SDK_ROOT', $null, [EnvironmentVariableTarget]::Process)

        $resolvedFromSdk = Get-ReleaseAndroidNdkPath -Manifest $Manifest
        if ($resolvedFromSdk -ne (Resolve-Path -LiteralPath $exactNdk).Path) {
            throw 'Android NDK resolver accepted a mismatched environment candidate or failed to select the exact SDK revision.'
        }

        [Environment]::SetEnvironmentVariable('ANDROID_HOME', $null, [EnvironmentVariableTarget]::Process)
        if ($null -ne (Get-ReleaseAndroidNdkPath -Manifest $Manifest)) {
            throw 'Android NDK resolver accepted a mismatched revision from ANDROID_NDK_HOME.'
        }

        $assertFailedClosed = $false
        try {
            Assert-ReleaseAndroidNdk -Manifest $Manifest | Out-Null
        }
        catch {
            $assertFailedClosed = $true
        }
        if (-not $assertFailedClosed) {
            throw 'Android NDK assertion did not fail closed when only a mismatched revision was available.'
        }

        [Environment]::SetEnvironmentVariable('ANDROID_NDK_HOME', $exactNdk, [EnvironmentVariableTarget]::Process)
        $resolvedFromEnvironment = Assert-ReleaseAndroidNdk -Manifest $Manifest
        if ($resolvedFromEnvironment -ne (Resolve-Path -LiteralPath $exactNdk).Path) {
            throw 'Android NDK resolver did not accept an exact environment candidate.'
        }
    }
    finally {
        foreach ($environmentName in $environmentNames) {
            [Environment]::SetEnvironmentVariable(
                $environmentName,
                $savedEnvironment[$environmentName],
                [EnvironmentVariableTarget]::Process
            )
        }

        if (Test-Path -LiteralPath $tempRoot) {
            $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
            if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
                -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-android-ndk-contract-', [System.StringComparison]::Ordinal)) {
                throw "Refusing to remove unsafe Android NDK contract test path: $resolvedTempRoot"
            }

            Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
        }
    }
}

Test-AndroidNdkResolutionContract -Manifest $manifest

foreach ($rid in $Rids) {
    $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid

    foreach ($component in $Components) {
        try {
            $planOutput = @(& (Join-Path $PSScriptRoot 'Build-Native.ps1') `
                -Component $component `
                -Rid $rid `
                -ManifestPath $ManifestPath `
                -BuildParallelLevel $BuildParallelLevel `
                -SkipDependencies `
                -NoCollect `
                -DryRun *>&1 | ForEach-Object { $_.ToString() }) -join [Environment]::NewLine

            if ($component -eq 'SDL_shadercross') {
                $context = "SDL_shadercross/$rid dry-run plan"
                $desktopDxc = $ridInfo.os -in @('windows', 'linux', 'macos')
                $vendoredDxc = $ridInfo.os -eq 'macos' -or ($ridInfo.os -eq 'linux' -and $ridInfo.arch -eq 'arm64')

                $expectedDxcFlag = if ($desktopDxc) { '-DSDLSHADERCROSS_DXC=ON' } else { '-DSDLSHADERCROSS_DXC=OFF' }
                $expectedVendoredFlag = if ($vendoredDxc) { '-DSDLSHADERCROSS_VENDORED=ON' } else { '-DSDLSHADERCROSS_VENDORED=OFF' }
                Assert-NativePlanContains -PlanOutput $planOutput -Expected $expectedDxcFlag -Context $context
                Assert-NativePlanContains -PlanOutput $planOutput -Expected $expectedVendoredFlag -Context $context

                if ($vendoredDxc) {
                    Assert-NativePlanDoesNotContain -PlanOutput $planOutput -Unexpected "external$([System.IO.Path]::DirectorySeparatorChar)SPIRV-Cross -B" -Context $context
                }

                if ($ridInfo.os -eq 'linux' -and $ridInfo.arch -eq 'x64') {
                    Assert-NativePlanContains -PlanOutput $planOutput -Expected 'pinned DXC binaries' -Context $context
                    Assert-NativePlanContains -PlanOutput $planOutput -Expected '-DDirectXShaderCompiler_ROOT=' -Context $context
                }
            }

            if ($ridInfo.os -eq 'android') {
                Assert-NativePlanContains `
                    -PlanOutput $planOutput `
                    -Expected '-DANDROID_SUPPORT_FLEXIBLE_PAGE_SIZES=ON' `
                    -Context "$component/$rid dry-run plan"
            }

            $rows.Add([pscustomobject]@{
                Component = $component
                Rid = $rid
                TargetOs = $ridInfo.os
                TargetArch = $ridInfo.arch
                Status = 'passed'
                Message = ''
            })
        }
        catch {
            $message = $_.Exception.Message
            $rows.Add([pscustomobject]@{
                Component = $component
                Rid = $rid
                TargetOs = $ridInfo.os
                TargetArch = $ridInfo.arch
                Status = 'failed'
                Message = $message
            })
            $errors.Add("$component/$rid`: $message")
        }
    }
}

$rows | Sort-Object Rid, Component | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native build dry-run plan validation failed with $($errors.Count) issue(s)."
}

Write-Host "Native build dry-run plan is valid for $($Components.Count) component(s) and $($Rids.Count) RID(s)."
