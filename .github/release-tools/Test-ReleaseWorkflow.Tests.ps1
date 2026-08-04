#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $WorkflowPath = '.github/workflows/release-native-packages.yml',
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

$validator = Join-Path $PSScriptRoot 'Test-ReleaseWorkflow.ps1'
if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Release workflow validator was not found: $validator"
}

function Assert-WorkflowValidationFails {
    param(
        [Parameter(Mandatory)][string] $Description,
        [Parameter(Mandatory)][scriptblock] $Action
    )

    $failed = $false
    try {
        & $Action
    }
    catch {
        $failed = $true
    }

    if (-not $failed) {
        throw "Expected release workflow validation to fail: $Description"
    }
}

$resolvedWorkflow = [System.IO.Path]::GetFullPath($WorkflowPath)
$workflowText = Get-Content -LiteralPath $resolvedWorkflow -Raw -Encoding UTF8
$pinnedLoginPattern = '(?m)^\s+uses:\s+NuGet/login@[0-9a-f]{40}\s+#\s+v1\s*$'
if ($workflowText -notmatch $pinnedLoginPattern) {
    throw 'Release workflow fixture must contain a SHA-pinned NuGet/login v1 action.'
}

& $validator -WorkflowPath $resolvedWorkflow -ManifestPath $ManifestPath

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-release-workflow-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary workflow path: $tempRoot"
}

New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null
$tempWorkflow = Join-Path $tempRoot 'release-native-packages.yml'

try {
    $unpinnedWorkflow = [regex]::Replace(
        $workflowText,
        $pinnedLoginPattern,
        '        uses: NuGet/login@v1'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $unpinnedWorkflow -Encoding UTF8

    Assert-WorkflowValidationFails -Description 'unpinned NuGet/login action' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $hardcodedNdkWorkflow = $workflowText.Replace(
        'ndk_version="$SDL3CS_ANDROID_NDK_VERSION"',
        'ndk_version="28.2.13676358"'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $hardcodedNdkWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'hardcoded Android NDK version' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $environmentFallbackWorkflow = $workflowText.Replace(
        'ndk="$ANDROID_HOME/ndk/$ndk_version"',
        'ndk="${ANDROID_NDK_HOME:-$ANDROID_HOME/ndk/$ndk_version}"'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $environmentFallbackWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'Android NDK environment fallback' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $missingRevisionGateWorkflow = $workflowText.Replace(
        'test "$actual_ndk_version" = "$ndk_version"',
        'test -n "$actual_ndk_version"'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $missingRevisionGateWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'missing exact Android NDK revision gate' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $missingPartialPublishGateWorkflow = $workflowText.Replace(
        'Partial native RID scope requires all publish flags to be false.',
        'Partial scope accepted.'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $missingPartialPublishGateWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'missing partial RID publish gate' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $missingSelectedRidHandoffWorkflow = $workflowText.Replace(
        '$params.Rids = $selectedRids',
        '$null = $selectedRids'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $missingSelectedRidHandoffWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'missing selected RID assembly handoff' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $missingWikiBuildWorkflow = $workflowText.Replace(
        'dotnet build ./SDL3-CS/SDL3-CS.csproj -c Release /p:GeneratePackageOnBuild=false',
        'dotnet --info'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $missingWikiBuildWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'missing Wiki Release output build before readiness' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $latestBundletoolWorkflow = $workflowText.Replace(
        'bundletool/releases/download/$BUNDLETOOL_VERSION',
        'bundletool/releases/latest/download'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $latestBundletoolWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'latest bundletool fallback' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $missingBundletoolHashGateWorkflow = $workflowText.Replace(
        'test "$actual_bundletool_sha256" = "$BUNDLETOOL_SHA256"',
        'test -n "$actual_bundletool_sha256"'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $missingBundletoolHashGateWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'missing bundletool SHA-256 gate' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $hardcodedSystemImageWorkflow = $workflowText.Replace(
        'ANDROID_16KB_SYSTEM_IMAGE: ${{ needs.plan.outputs.android_16kb_system_image }}',
        'ANDROID_16KB_SYSTEM_IMAGE: system-images;android-35;google_apis_ps16k;x86_64'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $hardcodedSystemImageWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'hardcoded Android 16 KB system image' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $missingRuntimeSmokeWorkflow = $workflowText.Replace(
        './.github/release-tools/Test-Android16KbRuntime.ps1',
        './.github/release-tools/Test-Android16KbRuntime-disabled.ps1'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $missingRuntimeSmokeWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'missing Android 16 KB runtime smoke' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }

    $unboundedEmulatorWorkflow = $workflowText.Replace(
        'for attempt in $(seq 1 60); do',
        'while true; do'
    )
    Set-Content -LiteralPath $tempWorkflow -Value $unboundedEmulatorWorkflow -Encoding UTF8
    Assert-WorkflowValidationFails -Description 'unbounded Android emulator device wait' -Action {
        & $validator -WorkflowPath $tempWorkflow -ManifestPath $ManifestPath *> $null
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-release-workflow-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary workflow path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Release workflow tests passed.'
