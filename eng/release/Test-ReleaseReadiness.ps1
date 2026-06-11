#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string[]] $Rids,
    [string[]] $Components,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $FetchForks,
    [switch] $RequireForksUpToDate,
    [switch] $CheckUpstream,
    [switch] $RequireUpstreamCurrent,
    [switch] $AllowCrossCompile,
    [switch] $SkipToolchainValidation,
    [switch] $SkipNativeArtifactValidation,
    [switch] $SkipNativeBuildReceiptValidation,
    [switch] $FailOnError
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Invoke-ReadinessStep {
    param(
        [Parameter(Mandatory)]
        [string] $Name,

        [Parameter(Mandatory)]
        [scriptblock] $Script
    )

    Write-Host ""
    Write-Host "== $Name =="

    try {
        & $Script
        $script:results.Add([pscustomobject]@{
            Step = $Name
            Status = 'passed'
            Message = ''
        })
    }
    catch {
        $script:results.Add([pscustomobject]@{
            Step = $Name
            Status = 'failed'
            Message = $_.Exception.Message
        })
        Write-Warning $_.Exception.Message
    }
}

$results = New-Object System.Collections.Generic.List[object]

Invoke-ReadinessStep -Name 'Release manifest' -Script {
    & (Join-Path $PSScriptRoot 'Test-ReleaseManifest.ps1') -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Release workflow' -Script {
    & (Join-Path $PSScriptRoot 'Test-ReleaseWorkflow.ps1') -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Release publish plan' -Script {
    & (Join-Path $PSScriptRoot 'Test-ReleasePublishPlan.ps1')
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}

Invoke-ReadinessStep -Name 'Native build dry-run plan' -Script {
    & (Join-Path $PSScriptRoot 'Test-NativeBuildPlan.ps1') -Components $Components -Rids $Rids -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Native fork initialization plan' -Script {
    & (Join-Path $PSScriptRoot 'Test-NativeForkInitializationPlan.ps1') -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Native package RID entries' -Script {
    & (Join-Path $PSScriptRoot 'Test-NativePackageRidEntries.ps1') -Components $Components -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Native package targets' -Script {
    & (Join-Path $PSScriptRoot 'Test-NativePackageTargets.ps1') -Components $Components -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Apple static consumer targets' -Script {
    & (Join-Path $PSScriptRoot 'Test-NativeAppleStaticConsumerTargets.ps1') -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Apple consumer package build plan' -Script {
    & (Join-Path $PSScriptRoot 'Test-AppleConsumerPackageBuild.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath -DryRun
}

Invoke-ReadinessStep -Name 'Android consumer package build plan' -Script {
    & (Join-Path $PSScriptRoot 'Test-AndroidConsumerPackageBuild.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath -DryRun
}

Invoke-ReadinessStep -Name 'Package versioning' -Script {
    & (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath
}

Invoke-ReadinessStep -Name 'Native forks' -Script {
    & (Join-Path $PSScriptRoot 'Test-NativeForks.ps1') `
        -ReleaseManifestPath $ManifestPath `
        -Fetch:$FetchForks `
        -RequireUpToDate:$RequireForksUpToDate `
        -CheckUpstream:$CheckUpstream `
        -RequireUpstreamCurrent:$RequireUpstreamCurrent
}

if (-not $SkipToolchainValidation) {
    foreach ($rid in $Rids) {
        Invoke-ReadinessStep -Name "Toolchain $rid" -Script {
            & (Join-Path $PSScriptRoot 'Test-ReleaseTools.ps1') -Rid $rid -ManifestPath $ManifestPath -AllowCrossCompile:$AllowCrossCompile
        }
    }
}
else {
    Write-Host ""
    Write-Host "== Toolchains =="
    Write-Host "Skipped host toolchain validation. Native build receipts remain required unless -SkipNativeBuildReceiptValidation is passed."
}

if (-not $SkipNativeArtifactValidation) {
    Invoke-ReadinessStep -Name 'Native package artifacts' -Script {
        & (Join-Path $PSScriptRoot 'Test-NativePackageArtifacts.ps1') -Components $Components -Rids $Rids -ManifestPath $ManifestPath
    }
}

if (-not $SkipNativeBuildReceiptValidation) {
    Invoke-ReadinessStep -Name 'Native build receipts' -Script {
        & (Join-Path $PSScriptRoot 'Test-NativeBuildReceipts.ps1') -Components $Components -Rids $Rids -ManifestPath $ManifestPath
    }
}

Invoke-ReadinessStep -Name 'Package versions' -Script {
    & (Join-Path $PSScriptRoot 'Get-PackageVersions.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath
}

Write-Host ""
Write-Host "Release readiness summary:"
$results | Format-Table -AutoSize

$failed = @($results | Where-Object { $_.Status -ne 'passed' })
if ($failed.Count -gt 0) {
    if ($FailOnError) {
        throw "Release readiness failed with $($failed.Count) failed step(s)."
    }

    Write-Warning "Release readiness has $($failed.Count) failed step(s). Pass -FailOnError to make this command fail."
}
else {
    Write-Host "Release readiness checks passed."
}
