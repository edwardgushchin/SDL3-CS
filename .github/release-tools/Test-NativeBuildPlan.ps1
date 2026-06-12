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

foreach ($rid in $Rids) {
    $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $rid

    foreach ($component in $Components) {
        try {
            & (Join-Path $PSScriptRoot 'Build-Native.ps1') `
                -Component $component `
                -Rid $rid `
                -ManifestPath $ManifestPath `
                -BuildParallelLevel $BuildParallelLevel `
                -SkipDependencies `
                -NoCollect `
                -DryRun *> $null

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
