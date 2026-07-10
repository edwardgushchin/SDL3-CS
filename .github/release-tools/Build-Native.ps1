#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $Component,

    [Parameter(Mandatory)]
    [string] $Rid,

    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot,
    [string] $ArtifactsRoot,
    [string] $Configuration,
    [int] $BuildParallelLevel,
    [switch] $SkipDependencies,
    [switch] $NoCollect,
    [switch] $Clean,
    [switch] $AllowCrossCompile,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Resolve-NativeBuildParallelLevel {
    param(
        [int] $RequestedBuildParallelLevel
    )

    if ($RequestedBuildParallelLevel -gt 0) {
        return $RequestedBuildParallelLevel
    }

    foreach ($environmentName in @('SDL3CS_NATIVE_BUILD_PARALLEL_LEVEL', 'CMAKE_BUILD_PARALLEL_LEVEL')) {
        $environmentValue = [Environment]::GetEnvironmentVariable($environmentName)
        if ([string]::IsNullOrWhiteSpace($environmentValue)) {
            continue
        }

        $parsedValue = 0
        if ([int]::TryParse($environmentValue, [ref]$parsedValue) -and $parsedValue -gt 0) {
            return $parsedValue
        }
    }

    return 0
}

function Get-CMakeBuildArguments {
    param(
        [Parameter(Mandatory)]
        [string] $BuildDir,

        [Parameter(Mandatory)]
        [string] $BuildConfiguration,

        [int] $ResolvedBuildParallelLevel
    )

    $buildArguments = @('--build', $BuildDir, '--config', $BuildConfiguration, '--parallel')
    if ($ResolvedBuildParallelLevel -gt 0) {
        $buildArguments += [string]$ResolvedBuildParallelLevel
    }

    return $buildArguments
}

function Resolve-NativeCMakeArgument {
    param(
        [Parameter(Mandatory)]
        [string] $Argument,

        [Parameter(Mandatory)]
        [string] $InstallRoot,

        [switch] $DryRun
    )

    $resolved = $Argument.Replace('{installRoot}', $InstallRoot)
    if ($resolved.Contains('{androidNdk}')) {
        $androidNdk = Get-ReleaseAndroidNdkPath
        if (-not $androidNdk) {
            if ($DryRun) {
                $androidNdk = '<androidNdk>'
            }
            else {
                $androidNdk = Assert-ReleaseAndroidNdk
            }
        }
        $resolved = $resolved.Replace('{androidNdk}', $androidNdk.Replace('\', '/'))
    }

    return $resolved
}

function Get-ReleaseCMakePathForBuild {
    param(
        [switch] $DryRun
    )

    $cmakePath = Get-ReleaseCMakePath
    if ($cmakePath) {
        return $cmakePath
    }

    if ($DryRun) {
        return 'cmake'
    }

    $cmakePath = Assert-ReleaseCMake
    return $cmakePath
}

function Get-NativeRidCMakeArgumentValue {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo,

        [Parameter(Mandatory)]
        [string] $Name
    )

    $prefix = "-D$Name="
    foreach ($argument in @($RidInfo.cmakeArgs)) {
        if ($argument.StartsWith($prefix, [System.StringComparison]::Ordinal)) {
            return $argument.Substring($prefix.Length)
        }
    }

    return $null
}

function Get-AppleDeploymentMinimumFlag {
    param(
        [Parameter(Mandatory)]
        [string] $AppleSdk,

        [Parameter(Mandatory)]
        [string] $DeploymentTarget
    )

    switch ($AppleSdk) {
        'iphoneos' { return "-miphoneos-version-min=$DeploymentTarget" }
        'iphonesimulator' { return "-mios-simulator-version-min=$DeploymentTarget" }
        'appletvos' { return "-mtvos-version-min=$DeploymentTarget" }
        'appletvsimulator' { return "-mtvos-simulator-version-min=$DeploymentTarget" }
        default { throw "Unsupported Apple SDK '$AppleSdk' for SDL main stub build." }
    }
}

function Add-SdlAppleMainStubArchive {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo,

        [Parameter(Mandatory)]
        [string] $BuildRoot,

        [Parameter(Mandatory)]
        [string] $InstallRoot,

        [switch] $DryRun
    )

    if ($RidInfo.os -notin @('ios', 'tvos')) {
        return
    }

    $appleSdk = if ($RidInfo.PSObject.Properties.Name.Contains('appleSdk') -and $RidInfo.appleSdk) {
        [string] $RidInfo.appleSdk
    }
    else {
        Get-NativeRidCMakeArgumentValue -RidInfo $RidInfo -Name 'CMAKE_OSX_SYSROOT'
    }
    $architecture = Get-NativeRidCMakeArgumentValue -RidInfo $RidInfo -Name 'CMAKE_OSX_ARCHITECTURES'
    $deploymentTarget = Get-NativeRidCMakeArgumentValue -RidInfo $RidInfo -Name 'CMAKE_OSX_DEPLOYMENT_TARGET'

    if (-not $appleSdk -or -not $architecture -or -not $deploymentTarget) {
        throw "RID '$($RidInfo.rid)' must define Apple SDK, architecture and deployment target to build SDL main stubs."
    }

    $stubRoot = Join-Path $BuildRoot 'SDL3-CSMainStubs'
    $sourcePath = Join-Path $stubRoot 'SDL3-CSMainStubs.c'
    $objectPath = Join-Path $stubRoot 'SDL3-CSMainStubs.o'
    $libDir = Join-Path $InstallRoot 'lib'
    $archivePath = Join-Path $libDir 'libSDL3-CSMainStubs.a'

    if ($DryRun) {
        Write-Host "[dry-run] build Apple SDL main weak stub archive $archivePath for $($RidInfo.rid)"
        return
    }

    New-Item -ItemType Directory -Force -Path $stubRoot, $libDir | Out-Null
    Set-Content -LiteralPath $sourcePath -Encoding UTF8 -Value @'
typedef enum SDL_AppResult
{
    SDL_APP_CONTINUE = 0,
    SDL_APP_SUCCESS = 1,
    SDL_APP_FAILURE = 2
} SDL_AppResult;

__attribute__((weak, visibility("default"))) SDL_AppResult SDL_AppInit(void **appstate, int argc, char *argv[])
{
    (void) argc;
    (void) argv;
    if (appstate != 0) {
        *appstate = 0;
    }
    return SDL_APP_CONTINUE;
}

__attribute__((weak, visibility("default"))) SDL_AppResult SDL_AppIterate(void *appstate)
{
    (void) appstate;
    return SDL_APP_CONTINUE;
}

__attribute__((weak, visibility("default"))) SDL_AppResult SDL_AppEvent(void *appstate, void *event)
{
    (void) appstate;
    (void) event;
    return SDL_APP_CONTINUE;
}

__attribute__((weak, visibility("default"))) void SDL_AppQuit(void *appstate, SDL_AppResult result)
{
    (void) appstate;
    (void) result;
}

__attribute__((weak, visibility("default"))) int SDL_main(int argc, char *argv[])
{
    (void) argc;
    (void) argv;
    return 0;
}
'@

    $deploymentFlag = Get-AppleDeploymentMinimumFlag -AppleSdk $appleSdk -DeploymentTarget $deploymentTarget
    Invoke-ReleaseCommand -FilePath 'xcrun' -Arguments @(
        '-sdk', $appleSdk,
        'clang',
        '-arch', $architecture,
        $deploymentFlag,
        '-c', $sourcePath,
        '-o', $objectPath
    ) -DryRun:$DryRun

    if (Test-Path -LiteralPath $archivePath -PathType Leaf) {
        Remove-Item -LiteralPath $archivePath -Force
    }
    Invoke-ReleaseCommand -FilePath 'xcrun' -Arguments @(
        '-sdk', $appleSdk,
        'ar',
        'rcs',
        $archivePath,
        $objectPath
    ) -DryRun:$DryRun

    if (-not (Test-Path -LiteralPath $archivePath -PathType Leaf)) {
        throw "Apple SDL main weak stub archive was not created: $archivePath"
    }
}

function Test-SdlShadercrossDxcBinaries {
    param(
        [Parameter(Mandatory)]
        [string] $DxcRoot,

        [Parameter(Mandatory)]
        [string] $Architecture
    )

    $includeCandidates = @(
        'inc\dxcapi.h',
        'windows\inc\dxcapi.h'
    )
    $dxcompilerBinaryCandidates = @(
        'bin\dxcompiler.dll',
        "bin\$Architecture\dxcompiler.dll",
        "windows\bin\$Architecture\dxcompiler.dll"
    )
    $dxcompilerLibraryCandidates = @(
        'lib\dxcompiler.lib',
        "lib\$Architecture\dxcompiler.lib",
        "windows\lib\$Architecture\dxcompiler.lib"
    )
    $dxilBinaryCandidates = @(
        'bin\dxil.dll',
        "bin\$Architecture\dxil.dll",
        "windows\bin\$Architecture\dxil.dll"
    )

    $groups = @(
        $includeCandidates,
        $dxcompilerBinaryCandidates,
        $dxcompilerLibraryCandidates,
        $dxilBinaryCandidates
    )

    foreach ($group in $groups) {
        $found = $false
        foreach ($relativePath in $group) {
            if (Test-Path -LiteralPath (Join-Path $DxcRoot $relativePath) -PathType Leaf) {
                $found = $true
                break
            }
        }

        if (-not $found) {
            return $false
        }
    }

    return $true
}

function Test-SdlShadercrossUnixDxcBinaries {
    param(
        [Parameter(Mandatory)]
        [string] $DxcRoot
    )

    $groups = @(
        @('include\dxc\dxcapi.h', 'linux\include\dxc\dxcapi.h'),
        @('lib\libdxcompiler.so', 'linux\lib\libdxcompiler.so'),
        @('lib\libdxil.so', 'linux\lib\libdxil.so')
    )

    foreach ($group in $groups) {
        $found = $false
        foreach ($relativePath in $group) {
            if (Test-Path -LiteralPath (Join-Path $DxcRoot $relativePath) -PathType Leaf) {
                $found = $true
                break
            }
        }

        if (-not $found) {
            return $false
        }
    }

    return $true
}

function Test-SdlShadercrossVendoredBuild {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    return $RidInfo.os -eq 'macos' -or ($RidInfo.os -eq 'linux' -and $RidInfo.arch -eq 'arm64')
}

function Get-SdlShadercrossDxcDownloadAsset {
    $explicitUrl = [Environment]::GetEnvironmentVariable('DXC_ZIP_URL')
    if ($explicitUrl) {
        return [pscustomobject]@{
            Name = Split-Path -Leaf $explicitUrl
            Url = $explicitUrl
        }
    }

    $headers = @{
        'User-Agent' = 'SDL3-CS-LocalRelease'
        'Accept' = 'application/vnd.github+json'
        'X-GitHub-Api-Version' = '2022-11-28'
    }
    $token = [Environment]::GetEnvironmentVariable('GITHUB_TOKEN')
    if ($token) {
        $headers['Authorization'] = "Bearer $token"
    }

    $release = Invoke-RestMethod -Uri 'https://api.github.com/repos/microsoft/DirectXShaderCompiler/releases/latest' -Headers $headers
    $asset = @($release.assets | Where-Object { $_.name -like 'dxc_*.zip' -and $_.name -notlike 'pdb_*' } | Select-Object -First 1)
    if ($asset.Count -ne 1) {
        throw 'DXC .zip asset was not found in the latest DirectXShaderCompiler release. Set DXC_ZIP_URL to a dxc_*.zip asset URL.'
    }

    return [pscustomobject]@{
        Name = $asset[0].name
        Url = $asset[0].browser_download_url
    }
}

function Initialize-SdlShadercrossDxcBinaries {
    param(
        [Parameter(Mandatory)]
        [string] $SourcePath,

        [Parameter(Mandatory)]
        [object] $RidInfo,

        [Parameter(Mandatory)]
        [string] $BuildRoot,

        [switch] $DryRun
    )

    $targetIsWindows = $RidInfo.os -eq 'windows'
    $targetIsLinuxX64 = $RidInfo.os -eq 'linux' -and $RidInfo.arch -eq 'x64'
    if (-not $targetIsWindows -and -not $targetIsLinuxX64) {
        return
    }

    $dxcRoot = Join-Path $SourcePath 'external\DirectXShaderCompiler-binaries'
    if ($targetIsWindows) {
        if (Test-SdlShadercrossDxcBinaries -DxcRoot $dxcRoot -Architecture $RidInfo.arch) {
            return
        }

        if ($DryRun) {
            Write-Host "[dry-run] bootstrap DXC binaries into $dxcRoot"
            return
        }

        New-Item -ItemType Directory -Force -Path $dxcRoot | Out-Null

        $asset = Get-SdlShadercrossDxcDownloadAsset
        $downloadRoot = Join-Path $BuildRoot '_downloads\DirectXShaderCompiler'
        New-Item -ItemType Directory -Force -Path $downloadRoot | Out-Null

        $zipPath = Join-Path $downloadRoot $asset.Name
        Write-Host "Downloading DXC binaries: $($asset.Name)"
        Invoke-WebRequest -Uri $asset.Url -OutFile $zipPath

        $extractRoot = Join-Path $downloadRoot ([System.IO.Path]::GetFileNameWithoutExtension($asset.Name))
        if (Test-Path -LiteralPath $extractRoot) {
            Remove-Item -LiteralPath $extractRoot -Recurse -Force
        }
        New-Item -ItemType Directory -Force -Path $extractRoot | Out-Null
        Expand-Archive -LiteralPath $zipPath -DestinationPath $extractRoot -Force

        $payloadRoot = $extractRoot
        $topFolder = @(Get-ChildItem -LiteralPath $extractRoot -Directory -Filter 'dxc_*' | Select-Object -First 1)
        if ($topFolder.Count -eq 1 -and (Test-Path -LiteralPath (Join-Path $topFolder[0].FullName 'bin') -PathType Container)) {
            $payloadRoot = $topFolder[0].FullName
        }

        Copy-Item -Path (Join-Path $payloadRoot '*') -Destination $dxcRoot -Recurse -Force

        if (-not (Test-SdlShadercrossDxcBinaries -DxcRoot $dxcRoot -Architecture $RidInfo.arch)) {
            throw "DXC binaries were downloaded but the expected $($RidInfo.arch) layout was not found under $dxcRoot."
        }
        return
    }

    if (Test-SdlShadercrossUnixDxcBinaries -DxcRoot $dxcRoot) {
        if ($DryRun) {
            Write-Host "[dry-run] reuse pinned DXC binaries from $dxcRoot"
        }
        return
    }

    if ($DryRun) {
        Write-Host "[dry-run] bootstrap pinned DXC binaries into $dxcRoot"
        return
    }

    $downloadScript = Join-Path $SourcePath 'build-scripts\download-prebuilt-DirectXShaderCompiler.cmake'
    if (-not (Test-Path -LiteralPath $downloadScript -PathType Leaf)) {
        throw "Pinned SDL_shadercross DXC download script was not found: $downloadScript"
    }

    New-Item -ItemType Directory -Force -Path $dxcRoot | Out-Null
    $downloadRoot = Join-Path $BuildRoot '_downloads\DirectXShaderCompiler'
    New-Item -ItemType Directory -Force -Path $downloadRoot | Out-Null
    $cmakePath = Get-ReleaseCMakePathForBuild
    Invoke-ReleaseCommand -FilePath $cmakePath -Arguments @(
        '-DCMAKE_SYSTEM_NAME=Linux',
        "-DDXC_ROOT=$dxcRoot",
        '-P', $downloadScript
    ) -WorkingDirectory $downloadRoot

    if (-not (Test-SdlShadercrossUnixDxcBinaries -DxcRoot $dxcRoot)) {
        throw "Pinned DXC binaries were downloaded but the expected linux-x64 layout was not found under $dxcRoot."
    }
}

function Invoke-SdlShadercrossSpirvCrossBuild {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo,

        [Parameter(Mandatory)]
        [string] $SourcePath,

        [Parameter(Mandatory)]
        [string] $BuildRoot,

        [Parameter(Mandatory)]
        [string] $InstallRoot,

        [Parameter(Mandatory)]
        [string] $BuildConfiguration,

        [int] $ResolvedBuildParallelLevel,

        [switch] $CleanBuild,
        [switch] $DryRun
    )

    $spirvCrossSource = Join-Path $SourcePath 'external\SPIRV-Cross'
    if (-not (Test-Path -LiteralPath (Join-Path $spirvCrossSource 'CMakeLists.txt') -PathType Leaf)) {
        if ($DryRun) {
            Write-Host "[dry-run] SPIRV-Cross source not present yet: $spirvCrossSource"
        }
        else {
            throw "SPIRV-Cross source was not found: $spirvCrossSource. Update SDL_shadercross submodules before building."
        }
    }

    $buildDir = Join-Path $BuildRoot 'SPIRV-Cross'
    if ($CleanBuild -and (Test-Path -LiteralPath $buildDir)) {
        if ($DryRun) {
            Write-Host "[dry-run] clean $buildDir"
        }
        else {
            Remove-Item -LiteralPath $buildDir -Recurse -Force
        }
    }

    if (-not $DryRun) {
        New-Item -ItemType Directory -Force -Path $buildDir, $InstallRoot | Out-Null
    }

    $spirvCrossShared = if ($RidInfo.os -in @('ios', 'tvos')) { 'OFF' } else { 'ON' }
    $configureArgs = @(
        '-S', $spirvCrossSource,
        '-B', $buildDir,
        "-DCMAKE_BUILD_TYPE=$BuildConfiguration",
        "-DCMAKE_INSTALL_PREFIX=$InstallRoot",
        '-DSPIRV_CROSS_ENABLE_C_API=ON',
        "-DSPIRV_CROSS_SHARED=$spirvCrossShared",
        '-DSPIRV_CROSS_ENABLE_TESTS=OFF',
        '-DSPIRV_CROSS_CLI=OFF',
        "-DBUILD_SHARED_LIBS=$spirvCrossShared"
    )

    if ($RidInfo.os -eq 'android' -and (Get-ReleaseHostOs) -eq 'windows') {
        $configureArgs += @('-G', 'Ninja')
    }
    elseif ($RidInfo.os -eq 'windows') {
        $configureArgs += @('-G', (Get-ReleaseVisualStudioCMakeGenerator), '-A', $RidInfo.vsArchitecture)
    }

    foreach ($arg in @($RidInfo.cmakeArgs)) {
        $configureArgs += Resolve-NativeCMakeArgument -Argument $arg -InstallRoot $InstallRoot -DryRun:$DryRun
    }

    $cmakePath = Get-ReleaseCMakePathForBuild -DryRun:$DryRun
    Invoke-ReleaseCommand -FilePath $cmakePath -Arguments $configureArgs -DryRun:$DryRun
    Invoke-ReleaseCommand -FilePath $cmakePath -Arguments (Get-CMakeBuildArguments -BuildDir $buildDir -BuildConfiguration $BuildConfiguration -ResolvedBuildParallelLevel $ResolvedBuildParallelLevel) -DryRun:$DryRun
    Invoke-ReleaseCommand -FilePath $cmakePath -Arguments @('--install', $buildDir, '--config', $BuildConfiguration) -DryRun:$DryRun
}

function Invoke-CMakeBuild {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [object] $ComponentInfo,

        [Parameter(Mandatory)]
        [object] $RidInfo,

        [Parameter(Mandatory)]
        [string] $SourceRootPath,

        [Parameter(Mandatory)]
        [string] $BuildRoot,

        [Parameter(Mandatory)]
        [string] $InstallRoot,

        [Parameter(Mandatory)]
        [string] $BuildConfiguration,

        [int] $ResolvedBuildParallelLevel,

        [switch] $CleanBuild,
        [switch] $DryRun
    )

    $sourcePath = Join-Path $SourceRootPath $ComponentInfo.sourceFolder
    if (-not (Test-Path -LiteralPath $sourcePath -PathType Container)) {
        if ($DryRun) {
            Write-Host "[dry-run] source folder not present yet for $($ComponentInfo.id): $sourcePath"
        }
        else {
            throw "Source folder not found for $($ComponentInfo.id): $sourcePath"
        }
    }

    $buildDir = Join-Path $BuildRoot $ComponentInfo.id
    if ($CleanBuild -and (Test-Path -LiteralPath $buildDir)) {
        if ($DryRun) {
            Write-Host "[dry-run] clean $buildDir"
        }
        else {
            Remove-Item -LiteralPath $buildDir -Recurse -Force
        }
    }

    if (-not $DryRun) {
        New-Item -ItemType Directory -Force -Path $buildDir, $InstallRoot | Out-Null
    }

    $shadercrossVendoredBuild = $false
    if ($ComponentInfo.id -eq 'SDL_shadercross') {
        $shadercrossVendoredBuild = Test-SdlShadercrossVendoredBuild -RidInfo $RidInfo
        if ($shadercrossVendoredBuild) {
            $vendoredDxcSource = Join-Path $sourcePath 'external\DirectXShaderCompiler\CMakeLists.txt'
            if (-not $DryRun -and -not (Test-Path -LiteralPath $vendoredDxcSource -PathType Leaf)) {
                throw "Vendored DirectXShaderCompiler source was not found: $vendoredDxcSource. Update SDL_shadercross submodules before building."
            }
        }
        else {
            Invoke-SdlShadercrossSpirvCrossBuild -RidInfo $RidInfo -SourcePath $sourcePath -BuildRoot $BuildRoot -InstallRoot $InstallRoot -BuildConfiguration $BuildConfiguration -ResolvedBuildParallelLevel $ResolvedBuildParallelLevel -CleanBuild:$CleanBuild -DryRun:$DryRun
            Initialize-SdlShadercrossDxcBinaries -SourcePath $sourcePath -RidInfo $RidInfo -BuildRoot $BuildRoot -DryRun:$DryRun
        }
    }

    $configureArgs = @(
        '-S', $sourcePath,
        '-B', $buildDir,
        "-DCMAKE_BUILD_TYPE=$BuildConfiguration",
        "-DCMAKE_INSTALL_PREFIX=$InstallRoot"
    )

    if ($RidInfo.os -eq 'android' -and (Get-ReleaseHostOs) -eq 'windows') {
        $configureArgs += @('-G', 'Ninja')
    }
    elseif ($RidInfo.os -eq 'windows') {
        $configureArgs += @('-G', (Get-ReleaseVisualStudioCMakeGenerator), '-A', $RidInfo.vsArchitecture)
    }

    foreach ($arg in @($RidInfo.cmakeArgs)) {
        $configureArgs += Resolve-NativeCMakeArgument -Argument $arg -InstallRoot $InstallRoot -DryRun:$DryRun
    }

    foreach ($arg in @($ComponentInfo.cmakeArgs)) {
        $configureArgs += Resolve-NativeCMakeArgument -Argument $arg -InstallRoot $InstallRoot -DryRun:$DryRun
    }

    if ($ComponentInfo.PSObject.Properties.Name.Contains('ridCmakeArgs')) {
        $ridCMakeArgsProperty = $ComponentInfo.ridCmakeArgs.PSObject.Properties[$RidInfo.rid]
        if ($ridCMakeArgsProperty) {
            foreach ($arg in @($ridCMakeArgsProperty.Value)) {
                $configureArgs += Resolve-NativeCMakeArgument -Argument $arg -InstallRoot $InstallRoot -DryRun:$DryRun
            }
        }
    }

    if ($RidInfo.os -in @('ios', 'tvos')) {
        switch ($ComponentInfo.id) {
            'SDL_image' {
                $configureArgs += '-DSDLIMAGE_DEPS_SHARED=OFF'
            }
            'SDL_mixer' {
                $configureArgs += '-DSDLMIXER_DEPS_SHARED=OFF'
            }
            'SDL_shadercross' {
                $configureArgs += @(
                    '-DSDLSHADERCROSS_SHARED=OFF',
                    '-DSDLSHADERCROSS_STATIC=ON',
                    '-DSDLSHADERCROSS_SPIRVCROSS_SHARED=OFF',
                    '-DSDLSHADERCROSS_INSTALL_RUNTIME=OFF'
                )
            }
        }
    }
    elseif ($RidInfo.os -eq 'android' -and $ComponentInfo.id -eq 'SDL_image') {
        $configureArgs += '-DSDLIMAGE_DEPS_SHARED=OFF'
    }
    elseif ($RidInfo.os -eq 'android' -and $ComponentInfo.id -eq 'SDL_shadercross') {
        $configureArgs += '-DSDLSHADERCROSS_INSTALL_RUNTIME=OFF'
    }

    $nasmPath = $null
    $perlPath = $null
    if ($RidInfo.os -eq 'windows' -or ($RidInfo.os -eq 'android' -and $RidInfo.arch -in @('x86', 'x64'))) {
        $nasmPath = Get-ReleaseNasmPath
        if ($nasmPath) {
            $configureArgs += "-DCMAKE_ASM_NASM_COMPILER=$nasmPath"
        }
    }
    if ($RidInfo.os -eq 'windows') {
        $perlPath = Get-ReleasePerlPath
    }

    if ($ComponentInfo.id -eq 'SDL_shadercross') {
        $desktopDxc = $RidInfo.os -in @('windows', 'linux', 'macos')
        $configureArgs += if ($desktopDxc) { '-DSDLSHADERCROSS_DXC=ON' } else { '-DSDLSHADERCROSS_DXC=OFF' }
        $configureArgs += if ($shadercrossVendoredBuild) { '-DSDLSHADERCROSS_VENDORED=ON' } else { '-DSDLSHADERCROSS_VENDORED=OFF' }

        if ($RidInfo.os -eq 'linux' -and $RidInfo.arch -eq 'x64') {
            $dxcRoot = Join-Path $sourcePath 'external\DirectXShaderCompiler-binaries'
            $configureArgs += "-DDirectXShaderCompiler_ROOT=$dxcRoot"
        }
    }

    $cmakePath = Get-ReleaseCMakePathForBuild -DryRun:$DryRun

    $oldPath = $env:PATH
    try {
        if ($nasmPath) {
            $nasmDir = Split-Path -Parent $nasmPath
            $env:PATH = "$nasmDir$([System.IO.Path]::PathSeparator)$oldPath"
        }
        if ($RidInfo.os -eq 'windows' -and $perlPath) {
            $perlDir = Split-Path -Parent $perlPath
            $env:PATH = "$perlDir$([System.IO.Path]::PathSeparator)$env:PATH"
        }

        Invoke-ReleaseCommand -FilePath $cmakePath -Arguments $configureArgs -DryRun:$DryRun
        Invoke-ReleaseCommand -FilePath $cmakePath -Arguments (Get-CMakeBuildArguments -BuildDir $buildDir -BuildConfiguration $BuildConfiguration -ResolvedBuildParallelLevel $ResolvedBuildParallelLevel) -DryRun:$DryRun
        Invoke-ReleaseCommand -FilePath $cmakePath -Arguments @('--install', $buildDir, '--config', $BuildConfiguration) -DryRun:$DryRun
    }
    finally {
        $env:PATH = $oldPath
    }
}

function Repair-KnownNativeSourceMutation {
    param(
        [Parameter(Mandatory)]
        [object] $ComponentInfo,

        [Parameter(Mandatory)]
        [string] $SourceRootPath,

        [switch] $DryRun
    )

    if ($ComponentInfo.id -ne 'SDL_image') {
        return
    }

    $zlibRoot = Join-Path $SourceRootPath "$($ComponentInfo.sourceFolder)\external\zlib"
    if (-not (Test-Path -LiteralPath $zlibRoot -PathType Container)) {
        return
    }

    $zconfPath = Join-Path $zlibRoot 'zconf.h'
    $includedPath = Join-Path $zlibRoot 'zconf.h.included'

    if (-not (Test-Path -LiteralPath $zconfPath -PathType Leaf)) {
        if ($DryRun) {
            Write-Host "[dry-run] restore $zconfPath"
        }
        else {
            Invoke-ReleaseCommand -FilePath 'git' -Arguments @('-C', $zlibRoot, 'restore', '--', 'zconf.h')
        }
    }

    if (Test-Path -LiteralPath $includedPath -PathType Leaf) {
        if ($DryRun) {
            Write-Host "[dry-run] remove $includedPath"
        }
        else {
            Remove-Item -LiteralPath $includedPath -Force
        }
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $Component
$ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $Rid
if (-not $DryRun) {
    Assert-ReleaseRidHost -RidInfo $ridInfo -AllowCrossCompile:$AllowCrossCompile
    Assert-ReleaseCMake | Out-Null
}
if ($ridInfo.os -eq 'windows' -and -not $DryRun) {
    Assert-ReleaseVisualStudio | Out-Null
    Assert-ReleaseVisualStudioCompiler -RidInfo $ridInfo | Out-Null
}
elseif ($ridInfo.os -eq 'android' -and (Get-ReleaseHostOs) -eq 'windows' -and -not $DryRun) {
    Assert-ReleaseAndroidNdk | Out-Null
    Assert-ReleaseNinja | Out-Null
}
elseif ($ridInfo.os -ne 'windows' -and -not $DryRun) {
    Assert-ReleaseUnixBuildTools -RidInfo $ridInfo | Out-Null
}

if (-not $SourceRoot) {
    $SourceRoot = $manifest.sourceRoot
}
if (-not $ArtifactsRoot) {
    $ArtifactsRoot = $manifest.artifactsRoot
}
if (-not $Configuration) {
    $Configuration = $manifest.configuration
}

$sourceRootPath = Resolve-ReleasePath $SourceRoot
$artifactsRootPath = Resolve-ReleasePath $ArtifactsRoot
$buildRoot = Join-Path $artifactsRootPath "native/$Component/$Rid/build"
$installRoot = Join-Path $artifactsRootPath "native/$Component/$Rid/install"
$resolvedBuildParallelLevel = Resolve-NativeBuildParallelLevel -RequestedBuildParallelLevel $BuildParallelLevel
if ($resolvedBuildParallelLevel -gt 0) {
    Write-Host "CMake build parallel level: $resolvedBuildParallelLevel"
}

$buildList = New-Object System.Collections.Generic.List[string]
if (-not $SkipDependencies) {
    foreach ($dependency in @($componentInfo.dependencies)) {
        if (-not $buildList.Contains($dependency)) {
            $buildList.Add($dependency)
        }
    }
}
$buildList.Add($componentInfo.id)

foreach ($componentId in $buildList) {
    $item = Get-ReleaseComponent -Manifest $manifest -Component $componentId
    Invoke-CMakeBuild -Manifest $manifest -ComponentInfo $item -RidInfo $ridInfo -SourceRootPath $sourceRootPath -BuildRoot $buildRoot -InstallRoot $installRoot -BuildConfiguration $Configuration -ResolvedBuildParallelLevel $resolvedBuildParallelLevel -CleanBuild:$Clean -DryRun:$DryRun
    if ($item.id -eq 'SDL') {
        Add-SdlAppleMainStubArchive -RidInfo $ridInfo -BuildRoot $buildRoot -InstallRoot $installRoot -DryRun:$DryRun
    }
    Repair-KnownNativeSourceMutation -ComponentInfo $item -SourceRootPath $sourceRootPath -DryRun:$DryRun
}

if (-not $NoCollect) {
    & (Join-Path $PSScriptRoot 'Collect-NativeArtifacts.ps1') -Component $Component -Rid $Rid -ManifestPath $ManifestPath -InstallRoot $installRoot -CleanDestination -AllowEmpty:$DryRun -DryRun:$DryRun
    if (-not $DryRun) {
        & (Join-Path $PSScriptRoot 'Write-NativeBuildReceipt.ps1') -Component $Component -Rid $Rid -ManifestPath $ManifestPath -Configuration $Configuration
    }
}
