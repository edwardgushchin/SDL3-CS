#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $WorkflowPath = '.github/workflows/release-native-packages.yml'
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Add-ManagedWorkflowError {
    param([Parameter(Mandatory)][string] $Message)
    $script:errors.Add($Message)
}

function Assert-TextContains {
    param(
        [Parameter(Mandatory)][AllowEmptyString()][string] $Text,
        [Parameter(Mandatory)][string] $Expected,
        [Parameter(Mandatory)][string] $Description
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        Add-ManagedWorkflowError "$Description is missing expected text: $Expected"
    }
}

function Assert-TextExcludes {
    param(
        [Parameter(Mandatory)][AllowEmptyString()][string] $Text,
        [Parameter(Mandatory)][string] $Forbidden,
        [Parameter(Mandatory)][string] $Description
    )

    if ($Text.Contains($Forbidden, [System.StringComparison]::Ordinal)) {
        Add-ManagedWorkflowError "$Description contains forbidden text: $Forbidden"
    }
}

$errors = New-Object System.Collections.Generic.List[string]
$workflowFile = Resolve-ReleasePath $WorkflowPath
$workflowText = ''
if (-not (Test-Path -LiteralPath $workflowFile -PathType Leaf)) {
    Add-ManagedWorkflowError "Managed release workflow was not found: $workflowFile"
}
else {
    $workflowText = Get-Content -LiteralPath $workflowFile -Raw -Encoding UTF8
}

$obsoleteWorkflow = Resolve-ReleasePath '.github/workflows/release-managed-package.yml'
if (Test-Path -LiteralPath $obsoleteWorkflow -PathType Leaf) {
    Add-ManagedWorkflowError "Obsolete standalone managed release workflow must be removed: $obsoleteWorkflow"
}

foreach ($expected in @(
    'workflow_dispatch:',
    'managed_only:',
    'package_revision:',
    'native_package_revision:',
    'base_release_ref:',
    'publish_github:',
    'publish_nuget:',
    '  managed-build:',
    '  managed-publish:',
    'fetch-depth: 0',
    './.github/release-tools/Test-ManagedReleaseScope.ps1',
    '-CheckNuGet',
    './.github/release-tools/Restore-ManagedReleaseTestRuntime.ps1',
    'dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release',
    'dotnet build .\SDL3-CS.Tests\SDL3-CS.Tests.csproj -c Release',
    'dotnet run --project .\SDL3-CS.Tests\SDL3-CS.Tests.csproj -c Release --no-build --no-restore',
    './.github/release-tools/Test-PublicWrapperXmlDocs.ps1',
    './.github/release-tools/Pack-NuGet.ps1',
    '-ManagedOnly',
    'name: managed-nuget-package',
    'path: artifacts/release/nuget/SDL3-CS.*.nupkg',
    'uses: NuGet/login@v1',
    'environment: production',
    "NATIVE_PACKAGE_REVISION: `${{ inputs.native_package_revision }}",
    "BASE_RELEASE_REF: `${{ inputs.base_release_ref }}",
    "if: `${{ inputs.managed_only }}",
    "if: `${{ inputs.managed_only && (inputs.publish_github || inputs.publish_nuget) }}",
    "if: `${{ !inputs.managed_only && (inputs.publish_github || inputs.publish_nuget) }}",
    'NativePackageRevision = [int]$env:NATIVE_PACKAGE_REVISION',
    'BaseReleaseRef = $env:BASE_RELEASE_REF',
    'ManagedOnly = $true',
    './.github/release-tools/Publish-Release.ps1 @params'
)) {
    Assert-TextContains -Text $workflowText -Expected $expected -Description 'managed release workflow'
}

foreach ($publishInput in @('publish_github', 'publish_nuget')) {
    if ($workflowText -notmatch "(?ms)^\s{6}$publishInput\s*:\s*\r?\n.*?^\s{8}default:\s+false\s*$") {
        Add-ManagedWorkflowError "Workflow input '$publishInput' must default to false."
    }
}
if ($workflowText -notmatch '(?ms)^\s{6}managed_only\s*:\s*\r?\n.*?^\s{8}default:\s+false\s*$') {
    Add-ManagedWorkflowError "Workflow input 'managed_only' must default to false."
}

$managedStart = $workflowText.IndexOf('  managed-build:', [System.StringComparison]::Ordinal)
if ($managedStart -ge 0) {
    $managedText = $workflowText.Substring($managedStart)
    foreach ($forbidden in @(
        'native_matrix',
        'Invoke-NativeHostBuild.ps1',
        'Initialize-NativeForks.ps1',
        'native-bundle-',
        'dotnet workload install android',
        'dotnet workload install ios',
        'dotnet workload install tvos',
        'Test-AppleConsumerPackageBuild.ps1',
        'Test-AndroidConsumerPackageBuild.ps1',
        "NativePackageRevision = [int]'`${{ inputs.native_package_revision }}'",
        "BaseReleaseRef = '`${{ inputs.base_release_ref }}'"
    )) {
        Assert-TextExcludes -Text $managedText -Forbidden $forbidden -Description 'managed-only jobs'
    }
}

$nativeSkipCondition = "if: `${{ !inputs.managed_only }}"
$nativeSkipCount = ([regex]::Matches($workflowText, [regex]::Escape($nativeSkipCondition))).Count
if ($nativeSkipCount -lt 5) {
    Add-ManagedWorkflowError "Expected at least 5 full-release jobs guarded by '$nativeSkipCondition', got $nativeSkipCount."
}

$scriptExpectations = [ordered]@{
    'Pack-NuGet.ps1' = @('[switch] $ManagedOnly', "`$_.Kind -eq 'managed'", '-ManagedOnly:$ManagedOnly')
    'Test-NuGetPackageContents.ps1' = @('[switch] $ManagedOnly', 'lib/net7.0/SDL3-CS.dll', 'lib/net10.0/SDL3-CS.xml', "StartsWith('runtimes/'")
    'Test-ReleasePublishState.ps1' = @('[switch] $ManagedOnly', "`$_.Kind -eq 'managed'")
    'Publish-Release.ps1' = @('[switch] $ManagedOnly', '-ManagedOnly:$ManagedOnly', 'Test-ManagedReleaseScope.ps1')
    'Test-ManagedReleaseScope.ps1' = @('SDL3-CS.NativePackages/', 'native-forks/', 'release-manifest.json', '[switch] $CheckNuGet')
    'Restore-ManagedReleaseTestRuntime.ps1' = @('runtimes/$Rid/native/', 'Invoke-WebRequest', '[switch] $DryRun')
    'Invoke-ManagedReleaseWorkflow.ps1' = @('release-native-packages.yml', 'managed_only=true', 'base_release_ref=', 'native_package_revision=', '[switch] $Run')
}

foreach ($scriptName in $scriptExpectations.Keys) {
    $scriptPath = Join-Path $PSScriptRoot $scriptName
    if (-not (Test-Path -LiteralPath $scriptPath -PathType Leaf)) {
        Add-ManagedWorkflowError "Managed release helper was not found: $scriptPath"
        continue
    }

    $scriptText = Get-Content -LiteralPath $scriptPath -Raw -Encoding UTF8
    foreach ($expected in $scriptExpectations[$scriptName]) {
        Assert-TextContains -Text $scriptText -Expected $expected -Description $scriptName
    }
}

if ($workflowText.Contains('${{ secrets.NUGET_API_KEY }}', [System.StringComparison]::Ordinal)) {
    Add-ManagedWorkflowError 'Managed workflow must use NuGet Trusted Publishing instead of a repository API key secret.'
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Managed release workflow validation failed with $($errors.Count) error(s)."
}

Write-Host 'Managed release workflow is isolated from native builds and uses the required validation and publication gates.'
