#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $WorkflowPath = (Join-Path (Join-Path (Join-Path $PSScriptRoot '..\..') '.github') 'workflows\release-native-packages.yml'),
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Add-WorkflowError {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:errors.Add($Message)
}

function Assert-WorkflowContains {
    param(
        [Parameter(Mandatory)]
        [string] $Text,

        [Parameter(Mandatory)]
        [string] $Expected,

        [Parameter(Mandatory)]
        [string] $Description
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        Add-WorkflowError "$Description is missing expected text: $Expected"
    }
}

function Assert-WorkflowRegex {
    param(
        [Parameter(Mandatory)]
        [string] $Text,

        [Parameter(Mandatory)]
        [string] $Pattern,

        [Parameter(Mandatory)]
        [string] $Description
    )

    if ($Text -notmatch $Pattern) {
        Add-WorkflowError "$Description is missing or does not match expected shape."
    }
}

$errors = New-Object System.Collections.Generic.List[string]
$workflowFile = Resolve-ReleasePath $WorkflowPath
if (-not (Test-Path -LiteralPath $workflowFile -PathType Leaf)) {
    throw "Release workflow was not found: $workflowFile"
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$workflowText = Get-Content -LiteralPath $workflowFile -Raw -Encoding UTF8

Assert-WorkflowContains -Text $workflowText -Expected 'workflow_dispatch:' -Description 'manual workflow trigger'
foreach ($inputName in @('package_revision', 'build_parallel_level', 'require_upstream_current', 'publish_github', 'publish_nuget')) {
    Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{6}$([regex]::Escape($inputName)):\s*\r?\n.*?^\s{8}required:\s+true" -Description "workflow input '$inputName'"
}

foreach ($publishInput in @('publish_github', 'publish_nuget')) {
    Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{6}$([regex]::Escape($publishInput)):\s*\r?\n.*?^\s{8}default:\s+false" -Description "workflow input '$publishInput' default"
}

Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{2}contents:\s+write\s*$" -Description 'workflow permissions for GitHub release creation'

foreach ($sdkVersion in @('10.0.x', '9.0.x', '8.0.x', '7.0.x')) {
    Assert-WorkflowContains -Text $workflowText -Expected $sdkVersion -Description ".NET SDK setup"
}

Assert-WorkflowContains -Text $workflowText -Expected "Get-Content -LiteralPath 'eng/release/release-manifest.json'" -Description 'plan job manifest load'
Assert-WorkflowContains -Text $workflowText -Expected '$manifest.rids | ForEach-Object' -Description 'plan job RID enumeration'
Assert-WorkflowContains -Text $workflowText -Expected 'rid = $_.rid' -Description 'plan job RID field'
Assert-WorkflowContains -Text $workflowText -Expected 'runner = $_.runner' -Description 'plan job runner field'
Assert-WorkflowContains -Text $workflowText -Expected 'bundle_count=$($include.Count)' -Description 'plan job bundle count output'
Assert-WorkflowContains -Text $workflowText -Expected 'matrix: ${{ fromJson(needs.plan.outputs.native_matrix) }}' -Description 'native job dynamic matrix'
Assert-WorkflowContains -Text $workflowText -Expected 'runs-on: ${{ matrix.runner }}' -Description 'native job runner binding'
Assert-WorkflowContains -Text $workflowText -Expected 'fail-fast: false' -Description 'native matrix fail-fast setting'
Assert-WorkflowContains -Text $workflowText -Expected 'SDL3CS_NATIVE_BUILD_PARALLEL_LEVEL: ${{ inputs.build_parallel_level }}' -Description 'native build parallel env'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Initialize-NativeForks.ps1 -Depth 1' -Description 'native fork initialization'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Invoke-NativeHostBuild.ps1 @buildArgs' -Description 'native build script invocation'
Assert-WorkflowContains -Text $workflowText -Expected 'artifacts/release/bundles/native-all-components-${{ matrix.rid }}.zip' -Description 'native bundle upload path'

Assert-WorkflowContains -Text $workflowText -Expected 'pattern: native-bundle-*' -Description 'assembly bundle download pattern'
Assert-WorkflowContains -Text $workflowText -Expected '$expectedBundleCount = [int]''${{ needs.plan.outputs.bundle_count }}''' -Description 'assembly expected bundle count'
Assert-WorkflowContains -Text $workflowText -Expected 'throw "Expected $expectedBundleCount native bundle(s)' -Description 'assembly bundle count gate'
Assert-WorkflowContains -Text $workflowText -Expected 'PackageRevision = [int]''${{ inputs.package_revision }}''' -Description 'assembly package revision input'
Assert-WorkflowContains -Text $workflowText -Expected 'BundlePath = @($bundles | ForEach-Object { $_.FullName })' -Description 'assembly bundle path handoff'
Assert-WorkflowContains -Text $workflowText -Expected '$params.RequireForksUpToDate = $true' -Description 'assembly upstream current gate'
Assert-WorkflowContains -Text $workflowText -Expected '$params.RequireUpstreamCurrent = $true' -Description 'assembly upstream current strict flag'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Invoke-ReleaseAssembly.ps1 @params' -Description 'assembly script invocation'
Assert-WorkflowContains -Text $workflowText -Expected 'path: artifacts/release/nuget/*.nupkg' -Description 'NuGet artifact upload'
Assert-WorkflowContains -Text $workflowText -Expected 'release-assembly-state.zip' -Description 'release assembly state artifact'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Test-ReleaseAssemblyState.ps1' -Description 'release assembly state validation'
Assert-WorkflowContains -Text $workflowText -Expected "-StatePath 'artifacts/release/release-assembly-state.zip'" -Description 'release assembly state zip validation'

Assert-WorkflowContains -Text $workflowText -Expected 'apple-consumer:' -Description 'Apple consumer validation job'
Assert-WorkflowContains -Text $workflowText -Expected 'needs: assemble' -Description 'Apple consumer job dependency'
Assert-WorkflowContains -Text $workflowText -Expected 'dotnet workload install ios tvos' -Description 'Apple .NET workload installation'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Test-AppleConsumerPackageBuild.ps1' -Description 'Apple consumer package build validation'
Assert-WorkflowContains -Text $workflowText -Expected 'iossimulator-arm64,tvossimulator-arm64' -Description 'Apple arm64 simulator consumer RID matrix'
Assert-WorkflowContains -Text $workflowText -Expected 'iossimulator-x64,tvossimulator-x64' -Description 'Apple x64 simulator consumer RID matrix'

Assert-WorkflowContains -Text $workflowText -Expected 'android-consumer:' -Description 'Android consumer validation job'
Assert-WorkflowContains -Text $workflowText -Expected 'dotnet workload install android' -Description 'Android .NET workload installation'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Test-AndroidConsumerPackageBuild.ps1' -Description 'Android consumer package build validation'
Assert-WorkflowContains -Text $workflowText -Expected 'android-arm,android-arm64,android-x86,android-x64' -Description 'Android consumer RID list'

Assert-WorkflowContains -Text $workflowText -Expected 'publish:' -Description 'publish job'
Assert-WorkflowContains -Text $workflowText -Expected '- apple-consumer' -Description 'publish job Apple consumer dependency'
Assert-WorkflowContains -Text $workflowText -Expected '- android-consumer' -Description 'publish job Android consumer dependency'
Assert-WorkflowContains -Text $workflowText -Expected 'if: ${{ inputs.publish_github || inputs.publish_nuget }}' -Description 'publish job gated condition'
Assert-WorkflowContains -Text $workflowText -Expected 'name: release-assembly-state' -Description 'publish job assembly state download'
Assert-WorkflowContains -Text $workflowText -Expected "Expand-Archive -LiteralPath 'artifacts/release/state/release-assembly-state.zip'" -Description 'publish job assembly state import'
Assert-WorkflowContains -Text $workflowText -Expected '$statePaths = @(' -Description 'publish job assembly state cleanup list'
Assert-WorkflowContains -Text $workflowText -Expected 'Remove-Item -LiteralPath $path -Recurse -Force' -Description 'publish job assembly state cleanup'
Assert-WorkflowContains -Text $workflowText -Expected "-StatePath '.'" -Description 'publish job imported assembly state validation'
Assert-WorkflowContains -Text $workflowText -Expected '$params.GitHubRelease = $true' -Description 'publish job GitHub release flag'
Assert-WorkflowContains -Text $workflowText -Expected '$params.NuGetPush = $true' -Description 'publish job NuGet push flag'
Assert-WorkflowContains -Text $workflowText -Expected './eng/release/Publish-Release.ps1 @params' -Description 'publish script invocation'

$initializeCount = ([regex]::Matches($workflowText, [regex]::Escape('./eng/release/Initialize-NativeForks.ps1 -Depth 1'))).Count
if ($initializeCount -lt 2) {
    Add-WorkflowError "Expected Initialize-NativeForks.ps1 to run in both native and assemble jobs, found $initializeCount occurrence(s)."
}

$expectedRows = @($manifest.rids | ForEach-Object {
    [pscustomobject]@{
        Rid = $_.rid
        Runner = $_.runner
        AllowCrossCompile = ($_.os -eq 'windows' -and $_.arch -eq 'x86')
    }
})

$missingRunnerRows = @($expectedRows | Where-Object { -not $_.Runner })
foreach ($row in $missingRunnerRows) {
    Add-WorkflowError "Manifest RID '$($row.Rid)' has no GitHub Actions runner."
}

$matrixJson = @{ include = @($expectedRows | ForEach-Object {
    [ordered]@{
        rid = $_.Rid
        runner = $_.Runner
        allow_cross_compile = $_.AllowCrossCompile
    }
}) } | ConvertTo-Json -Depth 8 -Compress

$roundTripMatrix = $matrixJson | ConvertFrom-Json -Depth 8
$roundTripRows = @($roundTripMatrix.include)
if ($roundTripRows.Count -ne $manifest.rids.Count) {
    Add-WorkflowError "Manifest-derived workflow matrix has $($roundTripRows.Count) row(s), expected $($manifest.rids.Count)."
}

$wrongCrossCompileRows = @($roundTripRows | Where-Object {
    ($_.allow_cross_compile -eq $true) -and $_.rid -ne 'win-x86'
})
foreach ($row in $wrongCrossCompileRows) {
    Add-WorkflowError "Only win-x86 should enable allow_cross_compile in workflow matrix, but '$($row.rid)' does too."
}

$rows = @($roundTripRows | ForEach-Object {
    [pscustomobject]@{
        Rid = $_.rid
        Runner = $_.runner
        AllowCrossCompile = $_.allow_cross_compile
    }
})
$rows | Sort-Object Rid | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Release workflow validation failed with $($errors.Count) error(s)."
}

Write-Host "Release GitHub Actions workflow is consistent with release manifest."
