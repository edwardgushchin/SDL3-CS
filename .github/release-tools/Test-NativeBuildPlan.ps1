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
