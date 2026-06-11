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

    if ($RidInfo.os -ne 'windows') {
        return
    }

    $dxcRoot = Join-Path $SourcePath 'external\DirectXShaderCompiler-binaries'
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

    if ($RidInfo.os -eq 'windows') {
        $configureArgs += @('-G', 'Visual Studio 17 2022', '-A', $RidInfo.vsArchitecture)
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

    if ($ComponentInfo.id -eq 'SDL_shadercross') {
        Invoke-SdlShadercrossSpirvCrossBuild -RidInfo $RidInfo -SourcePath $sourcePath -BuildRoot $BuildRoot -InstallRoot $InstallRoot -BuildConfiguration $BuildConfiguration -ResolvedBuildParallelLevel $ResolvedBuildParallelLevel -CleanBuild:$CleanBuild -DryRun:$DryRun
        Initialize-SdlShadercrossDxcBinaries -SourcePath $sourcePath -RidInfo $RidInfo -BuildRoot $BuildRoot -DryRun:$DryRun
    }

    $configureArgs = @(
        '-S', $sourcePath,
        '-B', $buildDir,
        "-DCMAKE_BUILD_TYPE=$BuildConfiguration",
        "-DCMAKE_INSTALL_PREFIX=$InstallRoot"
    )

    if ($RidInfo.os -eq 'windows') {
        $configureArgs += @('-G', 'Visual Studio 17 2022', '-A', $RidInfo.vsArchitecture)
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
    elseif ($RidInfo.os -eq 'android' -and $ComponentInfo.id -eq 'SDL_shadercross') {
        $configureArgs += '-DSDLSHADERCROSS_INSTALL_RUNTIME=OFF'
    }

    if ($RidInfo.os -eq 'windows') {
        $nasmPath = Get-ReleaseNasmPath
        if ($nasmPath) {
            $configureArgs += "-DCMAKE_ASM_NASM_COMPILER=$nasmPath"
        }
        $perlPath = Get-ReleasePerlPath
    }

    if ($ComponentInfo.id -eq 'SDL_shadercross') {
        if ($RidInfo.os -eq 'windows') {
            $configureArgs += '-DSDLSHADERCROSS_DXC=ON'
        }
        else {
            $configureArgs += '-DSDLSHADERCROSS_DXC=OFF'
        }
    }

    $cmakePath = Get-ReleaseCMakePathForBuild -DryRun:$DryRun

    $oldPath = $env:PATH
    try {
        if ($RidInfo.os -eq 'windows' -and $nasmPath) {
            $nasmDir = Split-Path -Parent $nasmPath
            $env:PATH = "$nasmDir;$oldPath"
        }
        if ($RidInfo.os -eq 'windows' -and $perlPath) {
            $perlDir = Split-Path -Parent $perlPath
            $env:PATH = "$perlDir;$env:PATH"
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
    Repair-KnownNativeSourceMutation -ComponentInfo $item -SourceRootPath $sourceRootPath -DryRun:$DryRun
}

if (-not $NoCollect) {
    & (Join-Path $PSScriptRoot 'Collect-NativeArtifacts.ps1') -Component $Component -Rid $Rid -ManifestPath $ManifestPath -InstallRoot $installRoot -CleanDestination -AllowEmpty:$DryRun -DryRun:$DryRun
    if (-not $DryRun) {
        & (Join-Path $PSScriptRoot 'Write-NativeBuildReceipt.ps1') -Component $Component -Rid $Rid -ManifestPath $ManifestPath -Configuration $Configuration
    }
}
