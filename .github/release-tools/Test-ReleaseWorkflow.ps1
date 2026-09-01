#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $WorkflowPath = '.github/workflows/release-native-packages.yml',
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

function Get-WorkflowJobText {
    param(
        [Parameter(Mandatory)]
        [string] $Text,

        [Parameter(Mandatory)]
        [string] $JobName
    )

    $pattern = '(?ms)^  ' + [regex]::Escape($JobName) + ':\r?\n.*?(?=^  [A-Za-z0-9_-]+:\r?\n|\z)'
    $match = [regex]::Match($Text, $pattern)
    if (-not $match.Success) {
        Add-WorkflowError "Workflow job '$JobName' was not found or could not be isolated."
        return ''
    }

    return $match.Value
}

$errors = New-Object System.Collections.Generic.List[string]
$workflowFile = Resolve-ReleasePath $WorkflowPath
if (-not (Test-Path -LiteralPath $workflowFile -PathType Leaf)) {
    throw "Release workflow was not found: $workflowFile"
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$workflowText = Get-Content -LiteralPath $workflowFile -Raw -Encoding UTF8
$requiredToolchains = [ordered]@{
    androidNdkVersion = '28.2.13676358'
    androidCompileSdkVersion = '35'
    androidPlatformArchiveUrl = 'https://dl.google.com/android/repository/platform-35_r02.zip'
    androidPlatformArchiveSha256 = '0988cacad01b38a18a47bac14a0695f246bc76c1b06c0eeb8eb0dc825ab0c8e0'
    androidPlatformJarSha256 = '4566663c3876e022b4fa4ced8c8697c4ab1688267f090114fd92d027b32e619b'
    androidBridgeJarSha256 = '5f5d8fb68f22f4b0adf5425ddcd24f08dc03461cf0354259a2809d99b81f15d3'
    androidBridgeJavaRelease = '11'
    androidBridgeSetupJavaVersion = '11.0.32.1+1'
    androidBridgeJavacVersion = '11.0.32.1'
    androidBuildToolsVersion = '35.0.0'
    bundletoolVersion = '1.18.3'
    bundletoolSha256 = 'a099cfa1543f55593bc2ed16a70a7c67fe54b1747bb7301f37fdfd6d91028e29'
    android16KbSystemImage = 'system-images;android-35;google_apis_ps16k;x86_64'
    appleXcodeVersion = '26.6'
    appleXcodeBuild = '17F113'
    appleDotnetSdkVersion = '10.0.302'
    appleDotnetWorkloadVersion = '10.0.302.1'
}
$resolvedToolchains = @{}
foreach ($toolchain in $requiredToolchains.GetEnumerator()) {
    $value = ''
    if ($manifest.PSObject.Properties.Name.Contains('toolchains') -and
        $null -ne $manifest.toolchains -and
        $manifest.toolchains.PSObject.Properties.Name.Contains($toolchain.Key)) {
        $value = [string]$manifest.toolchains.($toolchain.Key)
    }
    if ($value -ne $toolchain.Value) {
        Add-WorkflowError "Release manifest toolchains.$($toolchain.Key) must be '$($toolchain.Value)'."
    }
    $resolvedToolchains[$toolchain.Key] = $value
}

Assert-WorkflowContains -Text $workflowText -Expected 'workflow_dispatch:' -Description 'manual workflow trigger'
foreach ($inputName in @('package_revision', 'build_parallel_level', 'require_upstream_current', 'publish_github', 'publish_nuget')) {
    Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{6}$([regex]::Escape($inputName)):\s*\r?\n.*?^\s{8}required:\s+true" -Description "workflow input '$inputName'"
}

$manifestPackageRevisionDefault = 0
$hasManifestPackageRevisionDefault = $manifest.PSObject.Properties.Name.Contains('versioning') -and
    $null -ne $manifest.versioning -and
    $manifest.versioning.PSObject.Properties.Name.Contains('packageRevisionDefault') -and
    [int]::TryParse([string]$manifest.versioning.packageRevisionDefault, [ref]$manifestPackageRevisionDefault) -and
    $manifestPackageRevisionDefault -ge 0
if (-not $hasManifestPackageRevisionDefault) {
    Add-WorkflowError 'Release manifest versioning.packageRevisionDefault must be a non-negative integer.'
}
else {
    $packageRevisionDefaultPattern = '(?m)^[ ]{6}package_revision:[ \t]*\r?\n' +
        '(?:^[ ]{8,}[^\r\n]*\r?\n)*?' +
        '^[ ]{8}default:[ \t]+["'']?' +
        [regex]::Escape([string]$manifestPackageRevisionDefault) +
        '["'']?[ \t]*\r?$'
    Assert-WorkflowRegex -Text $workflowText -Pattern $packageRevisionDefaultPattern -Description 'package_revision default matching release manifest versioning.packageRevisionDefault'
}

foreach ($publishInput in @('publish_github', 'publish_nuget')) {
    Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{6}$([regex]::Escape($publishInput)):\s*\r?\n.*?^\s{8}default:\s+false" -Description "workflow input '$publishInput' default"
}
Assert-WorkflowRegex -Text $workflowText -Pattern '(?ms)^\s{6}native_rids:\s*\r?\n.*?^\s{8}required:\s+false\s*$.*?^\s{8}default:\s+""\s*$' -Description "optional workflow input 'native_rids'"
Assert-WorkflowRegex -Text $workflowText -Pattern '(?ms)if \(\[string\]::IsNullOrWhiteSpace\(\$requestedText\)\)\s*\{\s*\$requestedRids = \$allRids\s*\}' -Description 'empty requested RID input selecting the full manifest'

Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{2}contents:\s+write\s*$" -Description 'workflow permissions for GitHub release creation'
Assert-WorkflowRegex -Text $workflowText -Pattern "(?ms)^\s{2}id-token:\s+write\s*$" -Description 'workflow permissions for NuGet Trusted Publishing OIDC'

foreach ($sdkVersion in @('10.0.x', '9.0.x', '8.0.x', '7.0.x')) {
    Assert-WorkflowContains -Text $workflowText -Expected $sdkVersion -Description ".NET SDK setup"
}

Assert-WorkflowContains -Text $workflowText -Expected "Get-Content -LiteralPath '.github/release-tools/release-manifest.json'" -Description 'plan job manifest load'
Assert-WorkflowContains -Text $workflowText -Expected '$manifest.rids | ForEach-Object' -Description 'plan job RID enumeration'
Assert-WorkflowContains -Text $workflowText -Expected 'rid = $_.rid' -Description 'plan job RID field'
Assert-WorkflowContains -Text $workflowText -Expected 'runner = $_.runner' -Description 'plan job runner field'
Assert-WorkflowContains -Text $workflowText -Expected 'bundle_count=$($include.Count)' -Description 'plan job bundle count output'
Assert-WorkflowContains -Text $workflowText -Expected 'selected_rids: ${{ steps.native-matrix.outputs.selected_rids }}' -Description 'plan job selected RID output binding'
Assert-WorkflowContains -Text $workflowText -Expected 'full_scope: ${{ steps.native-matrix.outputs.full_scope }}' -Description 'plan job full scope output binding'
Assert-WorkflowContains -Text $workflowText -Expected 'has_android: ${{ steps.native-matrix.outputs.has_android }}' -Description 'plan job Android scope output binding'
Assert-WorkflowContains -Text $workflowText -Expected 'REQUESTED_NATIVE_RIDS: ${{ inputs.native_rids }}' -Description 'requested RID input handoff'
Assert-WorkflowContains -Text $workflowText -Expected '$ErrorActionPreference = ''Stop''' -Description 'plan job fail-closed PowerShell behavior'
Assert-WorkflowContains -Text $workflowText -Expected 'Requested native RIDs must be a comma-separated list without empty entries.' -Description 'empty requested RID rejection'
Assert-WorkflowContains -Text $workflowText -Expected 'Requested native RIDs must be unique:' -Description 'duplicate requested RID rejection'
Assert-WorkflowContains -Text $workflowText -Expected 'Requested native RID is not declared in the release manifest:' -Description 'unknown requested RID rejection'
Assert-WorkflowContains -Text $workflowText -Expected 'Partial native RID scope requires all publish flags to be false.' -Description 'partial scope publish fail-closed gate'
Assert-WorkflowContains -Text $workflowText -Expected 'selected_rids=$($selectedRids -join '','')' -Description 'selected RID plan output'
Assert-WorkflowContains -Text $workflowText -Expected 'android_ndk_version: ${{ steps.native-matrix.outputs.android_ndk_version }}' -Description 'plan job Android NDK version output binding'
Assert-WorkflowContains -Text $workflowText -Expected 'android_ndk_version=$($manifest.toolchains.androidNdkVersion)' -Description 'plan job manifest Android NDK version output'
foreach ($toolchainOutput in @(
    'android_compile_sdk_version',
    'android_platform_archive_url',
    'android_platform_archive_sha256',
    'android_platform_jar_sha256',
    'android_bridge_jar_sha256',
    'android_bridge_java_release',
    'android_bridge_setup_java_version',
    'android_bridge_javac_version',
    'apple_xcode_version',
    'apple_xcode_build',
    'apple_dotnet_sdk_version',
    'apple_dotnet_workload_version'
)) {
    $manifestProperty = [regex]::Replace($toolchainOutput, '_([a-z])', { param($match) $match.Groups[1].Value.ToUpperInvariant() })
    Assert-WorkflowContains -Text $workflowText -Expected "$toolchainOutput`: `${{ steps.native-matrix.outputs.$toolchainOutput }}" -Description "plan job $toolchainOutput output binding"
    Assert-WorkflowContains -Text $workflowText -Expected "$toolchainOutput=`$(`$manifest.toolchains.$manifestProperty)" -Description "plan job manifest $manifestProperty output"
}
Assert-WorkflowContains -Text $workflowText -Expected 'android_build_tools_version=$($manifest.toolchains.androidBuildToolsVersion)' -Description 'plan job manifest Android build-tools version output'
Assert-WorkflowContains -Text $workflowText -Expected 'bundletool_version=$($manifest.toolchains.bundletoolVersion)' -Description 'plan job manifest bundletool version output'
Assert-WorkflowContains -Text $workflowText -Expected 'bundletool_sha256=$($manifest.toolchains.bundletoolSha256)' -Description 'plan job manifest bundletool hash output'
Assert-WorkflowContains -Text $workflowText -Expected 'android_16kb_system_image=$($manifest.toolchains.android16KbSystemImage)' -Description 'plan job manifest Android 16 KB system image output'
Assert-WorkflowContains -Text $workflowText -Expected 'has_android_x64: ${{ steps.native-matrix.outputs.has_android_x64 }}' -Description 'plan job Android x64 scope output binding'
Assert-WorkflowContains -Text $workflowText -Expected 'matrix: ${{ fromJson(needs.plan.outputs.native_matrix) }}' -Description 'native job dynamic matrix'
Assert-WorkflowContains -Text $workflowText -Expected 'runs-on: ${{ matrix.runner }}' -Description 'native job runner binding'
Assert-WorkflowContains -Text $workflowText -Expected 'fail-fast: false' -Description 'native matrix fail-fast setting'
Assert-WorkflowContains -Text $workflowText -Expected 'SDL3CS_NATIVE_BUILD_PARALLEL_LEVEL: ${{ inputs.build_parallel_level }}' -Description 'native build parallel env'
Assert-WorkflowContains -Text $workflowText -Expected 'SDL3CS_ANDROID_NDK_VERSION: ${{ needs.plan.outputs.android_ndk_version }}' -Description 'manifest-derived Android NDK version env'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Initialize-NativeForks.ps1 -Depth 1 -Retries 3' -Description 'native fork initialization with clone retry'
Assert-WorkflowContains -Text $workflowText -Expected '$buildParams = @{' -Description 'native build hashtable parameter splatting'
Assert-WorkflowContains -Text $workflowText -Expected 'Rids = @(''${{ matrix.rid }}'')' -Description 'native build RID named parameter'
Assert-WorkflowContains -Text $workflowText -Expected '$buildParams.AllowCrossCompile = $true' -Description 'native build cross compile named parameter'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Invoke-NativeHostBuild.ps1 @buildParams' -Description 'native build script invocation'
Assert-WorkflowContains -Text $workflowText -Expected 'Smoke test ShaderCross DXC runtime' -Description 'ShaderCross DXC runtime smoke step'
Assert-WorkflowContains -Text $workflowText -Expected "if: startsWith(matrix.rid, 'linux-') || startsWith(matrix.rid, 'osx-') || matrix.rid == 'win-x86' || matrix.rid == 'win-x64' || matrix.rid == 'win-arm64'" -Description 'ShaderCross DXC smoke executable desktop RID gate'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Test-ShaderCrossDxcRuntime.ps1' -Description 'ShaderCross DXC smoke script invocation'
Assert-WorkflowContains -Text $workflowText -Expected 'artifacts/release/bundles/native-all-components-${{ matrix.rid }}.zip' -Description 'native bundle upload path'
Assert-WorkflowContains -Text $workflowText -Expected 'libjson-perl' -Description 'Linux vkd3d Perl JSON dependency'
Assert-WorkflowContains -Text $workflowText -Expected 'brew install nasm bison cpanminus' -Description 'macOS native tool installation'
Assert-WorkflowContains -Text $workflowText -Expected 'cpanm --local-lib="$HOME/perl5" --notest JSON' -Description 'macOS vkd3d Perl JSON dependency'
Assert-WorkflowContains -Text $workflowText -Expected 'PERL5LIB=$HOME/perl5/lib/perl5' -Description 'macOS Perl JSON module path'
Assert-WorkflowContains -Text $workflowText -Expected 'ndk_version="$SDL3CS_ANDROID_NDK_VERSION"' -Description 'Android NDK manifest version handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'ndk="$ANDROID_HOME/ndk/$ndk_version"' -Description 'Android NDK exact SDK path'
Assert-WorkflowContains -Text $workflowText -Expected 'source_properties="$ndk/source.properties"' -Description 'Android NDK source properties validation'
Assert-WorkflowContains -Text $workflowText -Expected 'actual_ndk_version=' -Description 'Android NDK actual revision read'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_ndk_version" = "$ndk_version"' -Description 'Android NDK exact revision gate'
Assert-WorkflowContains -Text $workflowText -Expected 'for attempt in 1 2 3; do' -Description 'Android NDK install retry loop'
Assert-WorkflowContains -Text $workflowText -Expected 'rm -rf "$ndk" "$ANDROID_HOME/.temp"' -Description 'Android NDK partial install cleanup'

$hardcodeSensitiveToolchains = @(
    'androidNdkVersion',
    'androidPlatformArchiveUrl',
    'androidPlatformArchiveSha256',
    'androidPlatformJarSha256',
    'androidBridgeJarSha256',
    'androidBridgeSetupJavaVersion',
    'androidBridgeJavacVersion',
    'androidBuildToolsVersion',
    'bundletoolVersion',
    'bundletoolSha256',
    'android16KbSystemImage',
    'appleXcodeVersion',
    'appleXcodeBuild',
    'appleDotnetSdkVersion',
    'appleDotnetWorkloadVersion'
)
foreach ($toolchain in $requiredToolchains.GetEnumerator() | Where-Object Key -In $hardcodeSensitiveToolchains) {
    if (-not [string]::IsNullOrWhiteSpace($resolvedToolchains[$toolchain.Key]) -and
        $workflowText.Contains($resolvedToolchains[$toolchain.Key], [System.StringComparison]::Ordinal)) {
        Add-WorkflowError "Release workflow must obtain toolchains.$($toolchain.Key) from the manifest instead of hardcoding it."
    }
}

foreach ($forbiddenAndroidNdkFallback in @(
    '${ANDROID_NDK_HOME:-',
    '${ANDROID_NDK_ROOT:-',
    'existing_ndk=',
    'find "$ANDROID_HOME/ndk"'
)) {
    if ($workflowText.Contains($forbiddenAndroidNdkFallback, [System.StringComparison]::Ordinal)) {
        Add-WorkflowError "Release workflow contains a forbidden Android NDK fallback: $forbiddenAndroidNdkFallback"
    }
}

Assert-WorkflowContains -Text $workflowText -Expected 'pattern: native-bundle-*' -Description 'assembly bundle download pattern'
Assert-WorkflowContains -Text $workflowText -Expected '$expectedBundleCount = [int]''${{ needs.plan.outputs.bundle_count }}''' -Description 'assembly expected bundle count'
Assert-WorkflowContains -Text $workflowText -Expected 'throw "Expected $expectedBundleCount native bundle(s)' -Description 'assembly bundle count gate'
Assert-WorkflowContains -Text $workflowText -Expected 'PackageRevision = [int]''${{ inputs.package_revision }}''' -Description 'assembly package revision input'
Assert-WorkflowContains -Text $workflowText -Expected 'BundlePath = @($bundles | ForEach-Object { $_.FullName })' -Description 'assembly bundle path handoff'
Assert-WorkflowContains -Text $workflowText -Expected '$params.Rids = $selectedRids' -Description 'assembly exact selected RID handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'if: ${{ needs.plan.outputs.full_scope == ''true'' }}' -Description 'partial scope release-state skip gate'
Assert-WorkflowContains -Text $workflowText -Expected '$params.RequireForksUpToDate = $true' -Description 'assembly upstream current gate'
Assert-WorkflowContains -Text $workflowText -Expected '$params.RequireUpstreamCurrent = $true' -Description 'assembly upstream current strict flag'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Invoke-ReleaseAssembly.ps1 @params' -Description 'assembly script invocation'
Assert-WorkflowContains -Text $workflowText -Expected 'Install Android .NET workload for bridge package' -Description 'assembly Android bridge workload installation step'
Assert-WorkflowContains -Text $workflowText -Expected 'dotnet workload install android --source https://api.nuget.org/v3/index.json' -Description 'assembly Android workload installation command'
Assert-WorkflowContains -Text $workflowText -Expected 'actions/setup-java@dd06d9cba3e5552c54d9f8ea23572deb30010f7c # v6.0.0' -Description 'assembly SHA-pinned exact Java setup'
Assert-WorkflowContains -Text $workflowText -Expected 'java-version: ${{ needs.plan.outputs.android_bridge_setup_java_version }}' -Description 'manifest-derived Android bridge setup-java version'
Assert-WorkflowContains -Text $workflowText -Expected 'verify-signature: true' -Description 'assembly Java signature verification'
Assert-WorkflowContains -Text $workflowText -Expected 'Rebuild and verify exact Android bridge' -Description 'assembly exact Android bridge rebuild step'
Assert-WorkflowContains -Text $workflowText -Expected 'Invoke-WebRequest -Uri $env:ANDROID_PLATFORM_ARCHIVE_URL' -Description 'manifest-derived Android platform download'
Assert-WorkflowContains -Text $workflowText -Expected '$archiveHash -ne $env:ANDROID_PLATFORM_ARCHIVE_SHA256' -Description 'Android platform archive hash gate'
Assert-WorkflowContains -Text $workflowText -Expected '$androidJarHash -ne $env:ANDROID_PLATFORM_JAR_SHA256' -Description 'Android platform JAR hash gate'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Build-AndroidBridgeJar.ps1' -Description 'exact Android bridge builder invocation'
Assert-WorkflowContains -Text $workflowText -Expected '-VerifyDeterministic' -Description 'Android bridge deterministic repeat gate'
Assert-WorkflowContains -Text $workflowText -Expected 'if ($candidateHash -ne $trackedHash)' -Description 'tracked Android bridge candidate parity gate'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Test-AndroidBridgeJar.ps1 -JarPath $trackedPath -SourceRoot ''native-forks/SDL''' -Description 'exact-source Android bridge validation'
Assert-WorkflowContains -Text $workflowText -Expected 'Setup Java for Android package build' -Description 'supported Java restore before Android binding build'
Assert-WorkflowContains -Text $workflowText -Expected "java-version: '21'" -Description 'supported Java major for Android .NET package build'
$bridgeBuildIndex = $workflowText.IndexOf('./.github/release-tools/Build-AndroidBridgeJar.ps1', [System.StringComparison]::Ordinal)
$androidPackageJavaIndex = $workflowText.IndexOf('Setup Java for Android package build', [System.StringComparison]::Ordinal)
$assemblyIndex = $workflowText.IndexOf('./.github/release-tools/Invoke-ReleaseAssembly.ps1 @params', [System.StringComparison]::Ordinal)
if ($bridgeBuildIndex -lt 0 -or $androidPackageJavaIndex -le $bridgeBuildIndex -or $assemblyIndex -le $androidPackageJavaIndex) {
    Add-WorkflowError 'Exact Android bridge rebuild must run before release assembly.'
}
$wikiReleaseBuildCommand = 'dotnet build ./SDL3-CS/SDL3-CS.csproj -c Release /p:GeneratePackageOnBuild=false'
Assert-WorkflowContains -Text $workflowText -Expected $wikiReleaseBuildCommand -Description 'assembly Wiki Release output build'
$publishJobText = Get-WorkflowJobText -Text $workflowText -JobName 'publish'
Assert-WorkflowContains -Text $publishJobText -Expected 'Build wrapper Release output for publish Wiki readiness' -Description 'publish Wiki Release output build step'
Assert-WorkflowContains -Text $publishJobText -Expected $wikiReleaseBuildCommand -Description 'publish Wiki Release output build command'
$publishWikiBuildIndex = $publishJobText.IndexOf($wikiReleaseBuildCommand, [System.StringComparison]::Ordinal)
$publishEntryPointIndex = $publishJobText.IndexOf('./.github/release-tools/Publish-Release.ps1 @params', [System.StringComparison]::Ordinal)
if ($publishWikiBuildIndex -lt 0 -or $publishEntryPointIndex -lt 0 -or $publishWikiBuildIndex -ge $publishEntryPointIndex) {
    Add-WorkflowError 'Publish job must build wrapper Release output before invoking Publish-Release.ps1.'
}
Assert-WorkflowContains -Text $workflowText -Expected 'path: artifacts/release/nuget/*.nupkg' -Description 'NuGet artifact upload'
Assert-WorkflowContains -Text $workflowText -Expected 'release-assembly-state.zip' -Description 'release assembly state artifact'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Test-ReleaseAssemblyState.ps1' -Description 'release assembly state validation'
Assert-WorkflowContains -Text $workflowText -Expected "-StatePath 'artifacts/release/release-assembly-state.zip'" -Description 'release assembly state zip validation'

Assert-WorkflowContains -Text $workflowText -Expected 'apple-consumer:' -Description 'Apple consumer validation job'
Assert-WorkflowContains -Text $workflowText -Expected '- assemble' -Description 'Apple consumer job dependency'
Assert-WorkflowContains -Text $workflowText -Expected 'if: ${{ !inputs.managed_only && needs.plan.outputs.full_scope == ''true'' }}' -Description 'Apple consumer full-scope gate'
Assert-WorkflowContains -Text $workflowText -Expected 'runner: macos-26' -Description 'Apple arm64 consumer macOS 26 runner'
Assert-WorkflowContains -Text $workflowText -Expected 'runner: macos-26-intel' -Description 'Apple x64 consumer macOS 26 Intel runner'
Assert-WorkflowContains -Text $workflowText -Expected 'timeout-minutes: 45' -Description 'Apple consumer timeout'
Assert-WorkflowContains -Text $workflowText -Expected 'APPLE_XCODE_VERSION: ${{ needs.plan.outputs.apple_xcode_version }}' -Description 'manifest-derived Apple Xcode version handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'APPLE_XCODE_BUILD: ${{ needs.plan.outputs.apple_xcode_build }}' -Description 'manifest-derived Apple Xcode build handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'APPLE_DOTNET_SDK_VERSION: ${{ needs.plan.outputs.apple_dotnet_sdk_version }}' -Description 'manifest-derived Apple .NET SDK version handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'APPLE_DOTNET_WORKLOAD_VERSION: ${{ needs.plan.outputs.apple_dotnet_workload_version }}' -Description 'manifest-derived Apple workload-set version handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'dotnet-version: ${{ needs.plan.outputs.apple_dotnet_sdk_version }}' -Description 'exact Apple .NET SDK setup'
Assert-WorkflowContains -Text $workflowText -Expected 'xcode_path="/Applications/Xcode_${APPLE_XCODE_VERSION}.app"' -Description 'manifest-derived Apple Xcode application path'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_xcode_version" = "$APPLE_XCODE_VERSION"' -Description 'exact Apple Xcode version gate'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_xcode_build" = "$APPLE_XCODE_BUILD"' -Description 'exact Apple Xcode build gate'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_dotnet_sdk_version" = "$APPLE_DOTNET_SDK_VERSION"' -Description 'exact Apple .NET SDK gate'
Assert-WorkflowContains -Text $workflowText -Expected 'dotnet workload install ios tvos --version "$APPLE_DOTNET_WORKLOAD_VERSION"' -Description 'exact Apple workload-set installation'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_workload_version" = "$APPLE_DOTNET_WORKLOAD_VERSION"' -Description 'exact Apple workload-set gate'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Test-AppleConsumerPackageBuild.ps1' -Description 'Apple consumer package build validation'
Assert-WorkflowContains -Text $workflowText -Expected '-Configuration Debug' -Description 'Apple consumer Debug build configuration'
Assert-WorkflowContains -Text $workflowText -Expected 'iossimulator-arm64,tvossimulator-arm64' -Description 'Apple arm64 simulator consumer RID matrix'
Assert-WorkflowContains -Text $workflowText -Expected 'iossimulator-x64,tvossimulator-x64' -Description 'Apple x64 simulator consumer RID matrix'

Assert-WorkflowContains -Text $workflowText -Expected 'android-consumer:' -Description 'Android consumer validation job'
Assert-WorkflowContains -Text $workflowText -Expected 'if: ${{ !inputs.managed_only && needs.plan.outputs.has_android == ''true'' }}' -Description 'Android consumer selected-scope gate'
Assert-WorkflowContains -Text $workflowText -Expected 'dotnet workload install android' -Description 'Android .NET workload installation'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Test-AndroidConsumerPackageBuild.ps1' -Description 'Android consumer package build validation'
Assert-WorkflowContains -Text $workflowText -Expected '$selectedRids | Where-Object { $_ -like ''android-*'' }' -Description 'Android consumer selected RID list'
Assert-WorkflowContains -Text $workflowText -Expected 'Build Android consumer apps' -Description 'Android SDLActivity managed-main consumer build step'
Assert-WorkflowContains -Text $workflowText -Expected 'Install exact Android verification tools' -Description 'Android verification tool install step'
Assert-WorkflowContains -Text $workflowText -Expected 'build-tools;$ANDROID_BUILD_TOOLS_VERSION' -Description 'exact Android build-tools install'
Assert-WorkflowContains -Text $workflowText -Expected 'actual_build_tools_version=' -Description 'Android build-tools actual revision read'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_build_tools_version" = "$ANDROID_BUILD_TOOLS_VERSION"' -Description 'Android build-tools exact revision gate'
Assert-WorkflowContains -Text $workflowText -Expected 'bundletool-all-$BUNDLETOOL_VERSION.jar' -Description 'versioned bundletool asset'
Assert-WorkflowContains -Text $workflowText -Expected 'bundletool/releases/download/$BUNDLETOOL_VERSION/bundletool-all-$BUNDLETOOL_VERSION.jar' -Description 'exact bundletool release URL'
Assert-WorkflowContains -Text $workflowText -Expected 'test "$actual_bundletool_sha256" = "$BUNDLETOOL_SHA256"' -Description 'bundletool SHA-256 gate'
Assert-WorkflowContains -Text $workflowText -Expected 'ZIPALIGN_PATH=$zipalign' -Description 'exact zipalign path export'
Assert-WorkflowContains -Text $workflowText -Expected 'BUNDLETOOL_PATH=$bundletool' -Description 'exact bundletool path export'
foreach ($forbiddenToolDownload in @('bundletool/releases/latest', 'bundletool/latest/', 'cmdline-tools/latest/bin/bundletool')) {
    if ($workflowText.Contains($forbiddenToolDownload, [System.StringComparison]::OrdinalIgnoreCase)) {
        Add-WorkflowError "Release workflow contains a forbidden latest-tool fallback: $forbiddenToolDownload"
    }
}

Assert-WorkflowContains -Text $workflowText -Expected 'name: android-x64-runtime-apk' -Description 'Android x64 runtime APK artifact upload'
Assert-WorkflowContains -Text $workflowText -Expected 'android-16kb-runtime:' -Description 'Android 16 KB runtime job'
Assert-WorkflowContains -Text $workflowText -Expected 'needs.plan.outputs.has_android_x64 == ''true''' -Description 'Android 16 KB runtime x64 scope gate'
Assert-WorkflowContains -Text $workflowText -Expected 'ANDROID_16KB_SYSTEM_IMAGE: ${{ needs.plan.outputs.android_16kb_system_image }}' -Description 'manifest-derived Android 16 KB system image handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'avd_home="$RUNNER_TEMP/android-avd"' -Description 'persistent Android AVD home'
Assert-WorkflowContains -Text $workflowText -Expected 'echo "ANDROID_AVD_HOME=$avd_home" >> "$GITHUB_ENV"' -Description 'Android AVD environment handoff'
Assert-WorkflowContains -Text $workflowText -Expected 'export ANDROID_AVD_HOME="$avd_home"' -Description 'Android AVD environment export'
Assert-WorkflowContains -Text $workflowText -Expected '--path "$ANDROID_AVD_HOME/sdl3cs-16kb.avd"' -Description 'explicit persistent Android AVD path'
Assert-WorkflowContains -Text $workflowText -Expected '"$sdkmanager" --install "platform-tools" "emulator" "$ANDROID_16KB_SYSTEM_IMAGE"' -Description 'Android 16 KB emulator package installation'
Assert-WorkflowContains -Text $workflowText -Expected '"$avdmanager" create avd' -Description 'Android 16 KB AVD creation'
Assert-WorkflowContains -Text $workflowText -Expected 'test -f "$ANDROID_AVD_HOME/sdl3cs-16kb.ini"' -Description 'Android 16 KB AVD metadata gate'
Assert-WorkflowContains -Text $workflowText -Expected '"$emulator" -list-avds | grep -Fx ''sdl3cs-16kb''' -Description 'Android 16 KB AVD visibility gate'
Assert-WorkflowContains -Text $workflowText -Expected 'test -e /dev/kvm' -Description 'Android emulator KVM availability gate'
Assert-WorkflowContains -Text $workflowText -Expected 'sudo chmod 0666 /dev/kvm' -Description 'hosted-runner Android emulator KVM access'
Assert-WorkflowContains -Text $workflowText -Expected '"$emulator" -accel-check' -Description 'Android emulator acceleration gate'
Assert-WorkflowContains -Text $workflowText -Expected '-accel on' -Description 'Android emulator hardware acceleration requirement'
Assert-WorkflowContains -Text $workflowText -Expected 'for attempt in $(seq 1 60); do' -Description 'bounded Android emulator device wait'
Assert-WorkflowContains -Text $workflowText -Expected 'cat "$RUNNER_TEMP/android-16kb-emulator.log"' -Description 'Android emulator failure diagnostics'
Assert-WorkflowContains -Text $workflowText -Expected 'name: android-16kb-emulator-log' -Description 'Android emulator log artifact'
Assert-WorkflowContains -Text $workflowText -Expected 'sys.boot_completed' -Description 'Android 16 KB emulator boot gate'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Test-Android16KbRuntime.ps1' -Description 'Android 16 KB runtime smoke invocation'

Assert-WorkflowContains -Text $workflowText -Expected 'publish:' -Description 'publish job'
Assert-WorkflowContains -Text $workflowText -Expected '- apple-consumer' -Description 'publish job Apple consumer dependency'
Assert-WorkflowContains -Text $workflowText -Expected '- android-consumer' -Description 'publish job Android consumer dependency'
Assert-WorkflowContains -Text $workflowText -Expected '- android-16kb-runtime' -Description 'publish job Android 16 KB runtime dependency'
Assert-WorkflowContains -Text $workflowText -Expected 'if: ${{ !inputs.managed_only && (inputs.publish_github || inputs.publish_nuget) }}' -Description 'full publish job gated condition'
Assert-WorkflowContains -Text $workflowText -Expected 'environment: production' -Description 'publish job Trusted Publishing environment'
Assert-WorkflowContains -Text $workflowText -Expected 'name: release-assembly-state' -Description 'publish job assembly state download'
Assert-WorkflowContains -Text $workflowText -Expected "Expand-Archive -LiteralPath 'artifacts/release/state/release-assembly-state.zip'" -Description 'publish job assembly state import'
Assert-WorkflowContains -Text $workflowText -Expected '$statePaths = @(' -Description 'publish job assembly state cleanup list'
Assert-WorkflowContains -Text $workflowText -Expected 'Remove-Item -LiteralPath $path -Recurse -Force' -Description 'publish job assembly state cleanup'
Assert-WorkflowContains -Text $workflowText -Expected "-StatePath '.'" -Description 'publish job imported assembly state validation'
Assert-WorkflowRegex -Text $workflowText -Pattern '(?m)^\s+uses:\s+NuGet/login@[0-9a-f]{40}\s+#\s+v1\s*$' -Description 'publish job SHA-pinned NuGet Trusted Publishing login action'
Assert-WorkflowContains -Text $workflowText -Expected 'id: nuget_login' -Description 'publish job NuGet Trusted Publishing login id'
Assert-WorkflowContains -Text $workflowText -Expected 'if: ${{ inputs.publish_nuget }}' -Description 'publish job NuGet login gate'
Assert-WorkflowContains -Text $workflowText -Expected 'user: edwardgushchin' -Description 'publish job NuGet Trusted Publishing user'
Assert-WorkflowContains -Text $workflowText -Expected 'NUGET_API_KEY: ${{ steps.nuget_login.outputs.NUGET_API_KEY }}' -Description 'publish job NuGet temporary API key handoff'
Assert-WorkflowContains -Text $workflowText -Expected '$params.GitHubRelease = $true' -Description 'publish job GitHub release flag'
Assert-WorkflowContains -Text $workflowText -Expected '$params.NuGetPush = $true' -Description 'publish job NuGet push flag'
Assert-WorkflowContains -Text $workflowText -Expected '$params.RequireUpstreamCurrent = $true' -Description 'publish job upstream current strict flag'
Assert-WorkflowContains -Text $workflowText -Expected './.github/release-tools/Publish-Release.ps1 @params' -Description 'publish script invocation'

if ($workflowText.Contains('${{ secrets.NUGET_API_KEY }}', [System.StringComparison]::Ordinal)) {
    Add-WorkflowError 'Release workflow must use NuGet Trusted Publishing instead of the repository secret NUGET_API_KEY.'
}

$initializeCount = ([regex]::Matches($workflowText, [regex]::Escape('./.github/release-tools/Initialize-NativeForks.ps1 -Depth 1 -Retries 3'))).Count
if ($initializeCount -lt 2) {
    Add-WorkflowError "Expected Initialize-NativeForks.ps1 to run in both native and assemble jobs, found $initializeCount occurrence(s)."
}

$strictUpstreamCount = ([regex]::Matches($workflowText, [regex]::Escape('$params.RequireUpstreamCurrent = $true'))).Count
if ($strictUpstreamCount -lt 2) {
    Add-WorkflowError "Expected require_upstream_current to flow into both assemble and publish jobs, found $strictUpstreamCount occurrence(s)."
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
