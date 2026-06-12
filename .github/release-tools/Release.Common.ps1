#requires -Version 7.0
Set-StrictMode -Version Latest

function Get-ReleaseRepoRoot {
    return (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..\..')).Path
}

function Resolve-ReleasePath {
    param(
        [Parameter(Mandatory)]
        [string] $Path
    )

    if ([System.IO.Path]::IsPathRooted($Path)) {
        return $Path
    }

    return (Join-Path (Get-ReleaseRepoRoot) $Path)
}

function Get-ReleaseRepoRelativePath {
    param(
        [Parameter(Mandatory)]
        [string] $Path
    )

    $repoRoot = (Resolve-Path -LiteralPath (Get-ReleaseRepoRoot)).Path
    $resolvedPath = (Resolve-Path -LiteralPath $Path).Path
    return [System.IO.Path]::GetRelativePath($repoRoot, $resolvedPath).Replace('\', '/')
}

function Assert-ReleaseSafeRelativePath {
    param(
        [Parameter(Mandatory)]
        [string] $RelativePath
    )

    if ([System.IO.Path]::IsPathRooted($RelativePath)) {
        throw "Relative path must not be rooted: $RelativePath"
    }

    $normalized = $RelativePath.Replace('\', '/')
    if ($normalized -eq '' -or $normalized -match '(^|/)\.\.(/|$)' -or $normalized -match '(^|/)\.(/|$)') {
        throw "Unsafe relative path: $RelativePath"
    }

    return $normalized
}

function Resolve-ReleaseSafeRelativePath {
    param(
        [Parameter(Mandatory)]
        [string] $Root,

        [Parameter(Mandatory)]
        [string] $RelativePath
    )

    $safeRelativePath = Assert-ReleaseSafeRelativePath -RelativePath $RelativePath
    $resolvedRoot = (Resolve-Path -LiteralPath $Root).Path
    $candidate = [System.IO.Path]::GetFullPath((Join-Path $resolvedRoot $safeRelativePath))

    if (-not $candidate.StartsWith($resolvedRoot, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Resolved path escapes root '$resolvedRoot': $RelativePath"
    }

    return $candidate
}

function Get-ReleaseManifest {
    param(
        [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
    )

    $resolved = Resolve-ReleasePath $ManifestPath
    if (-not (Test-Path -LiteralPath $resolved -PathType Leaf)) {
        throw "Release manifest not found: $resolved"
    }

    return Get-Content -LiteralPath $resolved -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 64
}

function Get-ReleaseComponent {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [string] $Component
    )

    $match = @($Manifest.components | Where-Object { $_.id -eq $Component })
    if ($match.Count -ne 1) {
        throw "Component '$Component' was not found in release manifest."
    }

    return $match[0]
}

function Get-ReleaseRid {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [string] $Rid
    )

    $match = @($Manifest.rids | Where-Object { $_.rid -eq $Rid })
    if ($match.Count -ne 1) {
        throw "RID '$Rid' was not found in release manifest."
    }

    return $match[0]
}

function Get-ReleaseHostOs {
    if ($IsWindows) { return 'windows' }
    if ($IsLinux) { return 'linux' }
    if ($IsMacOS) { return 'macos' }
    throw "Unsupported host OS."
}

function Get-ReleaseHostArch {
    $arch = [System.Runtime.InteropServices.RuntimeInformation]::OSArchitecture.ToString().ToLowerInvariant()
    switch ($arch) {
        'x64' { return 'x64' }
        'arm64' { return 'arm64' }
        default { throw "Unsupported host architecture: $arch" }
    }
}

function Assert-ReleaseRidHost {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo,

        [switch] $AllowCrossCompile
    )

    $hostOs = Get-ReleaseHostOs
    $hostArch = Get-ReleaseHostArch

    if ($RidInfo.os -eq 'android') {
        if ($hostOs -notin @('windows', 'linux', 'macos')) {
            throw "RID '$($RidInfo.rid)' requires a Windows, Linux, or macOS host with Android SDK/NDK, current host is $hostOs."
        }

        return
    }

    if ($RidInfo.os -in @('ios', 'tvos')) {
        if ($hostOs -ne 'macos') {
            throw "RID '$($RidInfo.rid)' requires a macOS host with Xcode, current host is $hostOs."
        }

        return
    }

    if ($RidInfo.os -ne $hostOs) {
        throw "RID '$($RidInfo.rid)' requires $($RidInfo.os) host, current host is $hostOs."
    }

    if (-not $AllowCrossCompile -and $RidInfo.arch -ne $hostArch) {
        throw "RID '$($RidInfo.rid)' requires $($RidInfo.arch) host architecture, current host is $hostArch. Pass -AllowCrossCompile only when the local toolchain is configured for this target."
    }
}

function Get-ReleaseOsArtifactKey {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    switch ($RidInfo.os) {
        'windows' { return 'windows' }
        'linux' { return 'linux' }
        'macos' { return 'macos' }
        'android' { return 'android' }
        'ios' { return 'ios' }
        'tvos' { return 'tvos' }
        default { throw "Unsupported RID OS '$($RidInfo.os)'." }
    }
}

function Get-ReleasePackageVersion {
    param(
        [Parameter(Mandatory)]
        [string] $NativeVersion,

        [Parameter(Mandatory)]
        [int] $PackageRevision,

        [string] $Pattern = '{nativeVersion}.{packageRevision}'
    )

    if ($PackageRevision -lt 0) {
        throw "PackageRevision must be zero or greater."
    }

    if (-not $Pattern -or -not $Pattern.Contains('{nativeVersion}') -or -not $Pattern.Contains('{packageRevision}')) {
        throw "Package version pattern must contain '{nativeVersion}' and '{packageRevision}': $Pattern"
    }

    return $Pattern.Replace('{nativeVersion}', $NativeVersion).Replace('{packageRevision}', $PackageRevision.ToString([System.Globalization.CultureInfo]::InvariantCulture))
}

function Get-ReleaseNativePackagePlatforms {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest
    )

    if ($Manifest.PSObject.Properties.Name.Contains('nativePackagePlatforms')) {
        return @($Manifest.nativePackagePlatforms)
    }

    return @([pscustomobject]@{
        id = 'All'
        suffix = ''
        rids = @($Manifest.rids | ForEach-Object { $_.rid })
    })
}

function Get-ReleaseNativePackageId {
    param(
        [Parameter(Mandatory)]
        [object] $Component,

        [Parameter(Mandatory)]
        [object] $Platform
    )

    if ($Platform.PSObject.Properties.Name.Contains('packageId') -and $Platform.packageId) {
        $componentSuffix = ''
        if ($Component.PSObject.Properties.Name.Contains('packageNameSuffix') -and $Component.packageNameSuffix) {
            $componentSuffix = [string] $Component.packageNameSuffix
        }

        if ([string]::IsNullOrWhiteSpace($componentSuffix)) {
            return [string] $Platform.packageId
        }

        return "$($Platform.packageId).$componentSuffix"
    }

    $suffix = if ($Platform.suffix) { ".$($Platform.suffix)" } else { '' }
    return "$($Component.packageId)$suffix"
}

function Get-ReleaseNativePackageProject {
    param(
        [Parameter(Mandatory)]
        [object] $Component,

        [Parameter(Mandatory)]
        [object] $Platform
    )

    if ($Platform.PSObject.Properties.Name.Contains('packageProjectOverrides') -and $Platform.packageProjectOverrides) {
        $override = $Platform.packageProjectOverrides.PSObject.Properties[$Component.id]
        if ($override -and $override.Value) {
            return [string] $override.Value
        }
    }

    $packageId = Get-ReleaseNativePackageId -Component $Component -Platform $Platform
    return "SDL3-CS.NativePackages/$packageId/$packageId.csproj"
}

function Get-ReleaseNativePackagePlatformForRid {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [string] $Rid
    )

    $matches = @(Get-ReleaseNativePackagePlatforms -Manifest $Manifest | Where-Object {
        @($_.rids) -contains $Rid
    })

    if ($matches.Count -ne 1) {
        throw "RID '$Rid' must belong to exactly one native package platform, got $($matches.Count)."
    }

    return $matches[0]
}

function Get-ReleaseNativePackageProjectForRid {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [object] $Component,

        [Parameter(Mandatory)]
        [string] $Rid
    )

    $platform = Get-ReleaseNativePackagePlatformForRid -Manifest $Manifest -Rid $Rid
    return Get-ReleaseNativePackageProject -Component $Component -Platform $platform
}

function Get-ReleaseNativePackageIdForRid {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [object] $Component,

        [Parameter(Mandatory)]
        [string] $Rid
    )

    $platform = Get-ReleaseNativePackagePlatformForRid -Manifest $Manifest -Rid $Rid
    return Get-ReleaseNativePackageId -Component $Component -Platform $platform
}

function Get-ReleasePackageVersions {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [int] $PackageRevision
    )

    $rows = New-Object System.Collections.Generic.List[object]
    $pattern = '{nativeVersion}.{packageRevision}'
    if ($Manifest.PSObject.Properties.Name.Contains('versioning') -and
        $Manifest.versioning.PSObject.Properties.Name.Contains('packageVersionPattern') -and
        $Manifest.versioning.packageVersionPattern) {
        $pattern = $Manifest.versioning.packageVersionPattern
    }

    foreach ($managed in $Manifest.managedPackages) {
        $component = Get-ReleaseComponent -Manifest $Manifest -Component $managed.versionComponent
        $rows.Add([pscustomobject]@{
            Id = $managed.id
            Project = $managed.project
            VersionComponent = $component.id
            NativeVersion = $component.nativeVersion
            PackageVersion = Get-ReleasePackageVersion -NativeVersion $component.nativeVersion -PackageRevision $PackageRevision -Pattern $pattern
            Kind = 'managed'
        })
    }

    $nativePackagePlatforms = Get-ReleaseNativePackagePlatforms -Manifest $Manifest
    foreach ($component in $Manifest.components) {
        foreach ($platform in $nativePackagePlatforms) {
            $id = Get-ReleaseNativePackageId -Component $component -Platform $platform
            $project = Get-ReleaseNativePackageProject -Component $component -Platform $platform
            $rows.Add([pscustomobject]@{
                Id = $id
                Project = $project
                VersionComponent = $component.id
                NativeVersion = $component.nativeVersion
                PackageVersion = Get-ReleasePackageVersion -NativeVersion $component.nativeVersion -PackageRevision $PackageRevision -Pattern $pattern
                Kind = 'native'
                NativePackageBaseId = $platform.packageId
                NativePackagePlatform = $platform.id
                NativeArtifactProject = $project
                ExpectedProjectPackageId = $id
                Rids = @($platform.rids)
            })
        }
    }

    return $rows
}

function Get-ReleaseToolPath {
    param(
        [Parameter(Mandatory)]
        [string] $Name,

        [string] $EnvironmentVariable,

        [string[]] $WindowsCandidates = @()
    )

    if ($EnvironmentVariable -and [Environment]::GetEnvironmentVariable($EnvironmentVariable)) {
        $candidate = [Environment]::GetEnvironmentVariable($EnvironmentVariable)
        if (Test-Path -LiteralPath $candidate -PathType Leaf) {
            return (Resolve-Path -LiteralPath $candidate).Path
        }

        throw "$EnvironmentVariable points to a missing file: $candidate"
    }

    $command = Get-Command $Name -ErrorAction SilentlyContinue
    if ($command) {
        return $command.Source
    }

    if ($IsWindows) {
        foreach ($candidate in $WindowsCandidates) {
            $expanded = [Environment]::ExpandEnvironmentVariables($candidate)
            if (Test-Path -LiteralPath $expanded -PathType Leaf) {
                return (Resolve-Path -LiteralPath $expanded).Path
            }
        }
    }

    return $null
}

function Get-ReleaseFirstToolPath {
    param(
        [Parameter(Mandatory)]
        [string[]] $Names
    )

    foreach ($name in $Names) {
        $path = Get-ReleaseToolPath -Name $name
        if ($path) {
            return $path
        }
    }

    return $null
}

function Get-ReleaseCMakePath {
    $candidates = @(
        '%ProgramFiles%\CMake\bin\cmake.exe',
        '%LocalAppData%\Programs\CMake\bin\cmake.exe',
        '%ProgramFiles(x86)%\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin\cmake.exe',
        '%ProgramFiles%\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin\cmake.exe',
        '%ProgramFiles%\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin\cmake.exe',
        '%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin\cmake.exe'
    )

    return Get-ReleaseToolPath -Name 'cmake' -EnvironmentVariable 'CMAKE_EXE' -WindowsCandidates $candidates
}

function Get-ReleaseNinjaPath {
    return Get-ReleaseToolPath -Name 'ninja' -EnvironmentVariable 'NINJA_EXE'
}

function Get-ReleaseNasmPath {
    $repoRoot = Get-ReleaseRepoRoot
    $candidates = @(
        '%ProgramFiles%\NASM\nasm.exe',
        '%ProgramFiles(x86)%\NASM\nasm.exe',
        (Join-Path $repoRoot 'artifacts\tools\nasm-3.01\nasm-3.01\nasm.exe')
    )

    return Get-ReleaseToolPath -Name 'nasm' -EnvironmentVariable 'NASM_EXE' -WindowsCandidates $candidates
}

function Get-ReleasePerlPath {
    $candidates = @(
        'C:\Strawberry\perl\bin\perl.exe',
        '%ProgramFiles%\Strawberry Perl\perl\bin\perl.exe',
        '%ProgramFiles(x86)%\Strawberry Perl\perl\bin\perl.exe'
    )

    return Get-ReleaseToolPath -Name 'perl' -EnvironmentVariable 'PERL_EXE' -WindowsCandidates $candidates
}

function Assert-ReleaseCMake {
    $cmake = Get-ReleaseCMakePath
    if (-not $cmake) {
        throw "CMake was not found. Install CMake or set CMAKE_EXE to cmake.exe before running native release builds."
    }

    return $cmake
}

function Assert-ReleaseNinja {
    $ninja = Get-ReleaseNinjaPath
    if (-not $ninja) {
        throw "Ninja was not found. Install Ninja or set NINJA_EXE before running Android native release builds on Windows."
    }

    return $ninja
}

function Assert-ReleaseNasm {
    $nasm = Get-ReleaseNasmPath
    if (-not $nasm) {
        throw "NASM was not found. Install NASM, set NASM_EXE, or bootstrap artifacts/tools/nasm-3.01 before building components that use vendored assembly dependencies."
    }

    return $nasm
}

function Assert-ReleasePerl {
    $perl = Get-ReleasePerlPath
    if (-not $perl) {
        throw "Perl was not found. Install Strawberry Perl or set PERL_EXE before building components that use vendored dependencies requiring Perl."
    }

    return $perl
}

function Get-ReleaseAndroidNdkPath {
    foreach ($environmentVariable in @('ANDROID_NDK_HOME', 'ANDROID_NDK_ROOT')) {
        $candidate = [Environment]::GetEnvironmentVariable($environmentVariable)
        if ($candidate -and (Test-Path -LiteralPath $candidate -PathType Container)) {
            return (Resolve-Path -LiteralPath $candidate).Path
        }
    }

    $androidHome = [Environment]::GetEnvironmentVariable('ANDROID_HOME')
    if (-not $androidHome) {
        $androidHome = [Environment]::GetEnvironmentVariable('ANDROID_SDK_ROOT')
    }

    if ($androidHome) {
        $ndkRoot = Join-Path $androidHome 'ndk'
        if (Test-Path -LiteralPath $ndkRoot -PathType Container) {
            $ndkVersions = @(Get-ChildItem -LiteralPath $ndkRoot -Directory | Sort-Object Name -Descending)
            foreach ($ndkVersion in $ndkVersions) {
                $toolchain = Join-Path $ndkVersion.FullName 'build/cmake/android.toolchain.cmake'
                if (Test-Path -LiteralPath $toolchain -PathType Leaf) {
                    return $ndkVersion.FullName
                }
            }
        }
    }

    return $null
}

function Assert-ReleaseAndroidNdk {
    $ndk = Get-ReleaseAndroidNdkPath
    if (-not $ndk) {
        throw "Android NDK was not found. Set ANDROID_NDK_HOME/ANDROID_NDK_ROOT or install an NDK under ANDROID_HOME/ndk before building Android native RIDs."
    }

    $toolchain = Join-Path $ndk 'build/cmake/android.toolchain.cmake'
    if (-not (Test-Path -LiteralPath $toolchain -PathType Leaf)) {
        throw "Android CMake toolchain file was not found: $toolchain"
    }

    return $ndk
}

function Get-ReleaseVisualStudioPath {
    if (-not $IsWindows) {
        return $null
    }

    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (-not (Test-Path -LiteralPath $vswhere -PathType Leaf)) {
        return $null
    }

    $path = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
    if ($LASTEXITCODE -ne 0 -or -not $path) {
        return $null
    }

    return $path
}

function Get-ReleaseVisualStudioCMakeGenerator {
    $fallbackGenerator = 'Visual Studio 17 2022'

    if (-not $IsWindows) {
        return $fallbackGenerator
    }

    $vs = Get-ReleaseVisualStudioPath
    if (-not $vs) {
        return $fallbackGenerator
    }

    if ($vs -match 'Microsoft Visual Studio[\\/](?<major>\d+)[\\/]') {
        switch ($Matches.major) {
            '18' { return 'Visual Studio 18 2026' }
            '17' { return 'Visual Studio 17 2022' }
        }
    }

    return $fallbackGenerator
}

function Get-ReleaseVisualStudioHostToolset {
    $hostArch = Get-ReleaseHostArch
    switch ($hostArch) {
        'x64' { return 'Hostx64' }
        'arm64' { return 'Hostarm64' }
        default { throw "Unsupported Visual Studio host architecture: $hostArch" }
    }
}

function Get-ReleaseVisualStudioTargetToolset {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    switch ($RidInfo.vsArchitecture) {
        'Win32' { return 'x86' }
        'x64' { return 'x64' }
        'ARM64' { return 'arm64' }
        default { throw "Unsupported Visual Studio target architecture: $($RidInfo.vsArchitecture)" }
    }
}

function Get-ReleaseVisualStudioCompilerPath {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    if (-not $IsWindows) {
        return $null
    }

    $vs = Get-ReleaseVisualStudioPath
    if (-not $vs) {
        return $null
    }

    $msvcRoot = Join-Path $vs 'VC\Tools\MSVC'
    if (-not (Test-Path -LiteralPath $msvcRoot -PathType Container)) {
        return $null
    }

    $hostToolset = Get-ReleaseVisualStudioHostToolset
    $targetToolset = Get-ReleaseVisualStudioTargetToolset -RidInfo $RidInfo
    $toolsetVersions = @(Get-ChildItem -LiteralPath $msvcRoot -Directory | Sort-Object Name -Descending)
    foreach ($toolsetVersion in $toolsetVersions) {
        $compilerPath = Join-Path $toolsetVersion.FullName "bin\$hostToolset\$targetToolset\cl.exe"
        if (Test-Path -LiteralPath $compilerPath -PathType Leaf) {
            return $compilerPath
        }
    }

    return $null
}

function Assert-ReleaseVisualStudio {
    $vs = Get-ReleaseVisualStudioPath
    if (-not $vs) {
        throw "Visual Studio Build Tools with MSBuild were not found. Install Visual Studio Build Tools 2022 with the C++ workload for Windows native release builds."
    }

    return $vs
}

function Assert-ReleaseVisualStudioCompiler {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    $compilerPath = Get-ReleaseVisualStudioCompilerPath -RidInfo $RidInfo
    if (-not $compilerPath) {
        $hostToolset = Get-ReleaseVisualStudioHostToolset
        $targetToolset = Get-ReleaseVisualStudioTargetToolset -RidInfo $RidInfo
        throw "Visual Studio C++ compiler for RID '$($RidInfo.rid)' was not found. Expected a $hostToolset\\$targetToolset\\cl.exe toolchain under Visual Studio Build Tools."
    }

    return $compilerPath
}

function Get-ReleaseUnixBuildTools {
    if ($IsWindows) {
        return $null
    }

    $tools = [ordered]@{
        CCompiler = Get-ReleaseFirstToolPath -Names @('cc', 'clang', 'gcc')
        CxxCompiler = Get-ReleaseFirstToolPath -Names @('c++', 'clang++', 'g++')
        Make = Get-ReleaseToolPath -Name 'make'
        Shell = Get-ReleaseToolPath -Name 'sh'
        PkgConfig = Get-ReleaseToolPath -Name 'pkg-config'
        Bison = Get-ReleaseToolPath -Name 'bison'
        NASM = Get-ReleaseToolPath -Name 'nasm'
        Patchelf = Get-ReleaseToolPath -Name 'patchelf'
        InstallNameTool = Get-ReleaseToolPath -Name 'install_name_tool'
    }

    return [pscustomobject]$tools
}

function Assert-ReleaseUnixBuildTools {
    param(
        [Parameter(Mandatory)]
        [object] $RidInfo
    )

    if ($RidInfo.os -eq 'windows') {
        return $null
    }

    $tools = Get-ReleaseUnixBuildTools
    $errors = New-Object System.Collections.Generic.List[string]

    if (-not $tools.CCompiler) { $errors.Add("C compiler was not found. Install clang/gcc or Xcode command line tools for RID '$($RidInfo.rid)'.") }
    if (-not $tools.CxxCompiler) { $errors.Add("C++ compiler was not found. Install clang++/g++ or Xcode command line tools for RID '$($RidInfo.rid)'.") }
    if (-not $tools.Make) { $errors.Add("make was not found. Install build-essential/make or Xcode command line tools for RID '$($RidInfo.rid)'.") }
    if (-not $tools.Shell) { $errors.Add("sh was not found. A POSIX shell is required for RID '$($RidInfo.rid)'.") }
    if (-not $tools.Bison) { $errors.Add("bison was not found. SDL_shadercross runtime build uses vkd3d and requires bison for RID '$($RidInfo.rid)'.") }

    if ($RidInfo.os -eq 'linux') {
        if (-not $tools.PkgConfig) { $errors.Add("pkg-config was not found. Install pkg-config for Linux native dependency discovery.") }
        if (-not $tools.NASM) { $errors.Add("nasm was not found. Install nasm for vendored SDL_image/libaom Linux builds.") }
        if (-not $tools.Patchelf) { $errors.Add("patchelf was not found. SDL_shadercross runtime install uses patchelf to set Linux RPATH.") }
    }
    elseif ($RidInfo.os -eq 'android') {
        if (-not $tools.NASM) { $errors.Add("nasm was not found. Install nasm for vendored SDL_image/libaom Android builds.") }
        try {
            Assert-ReleaseAndroidNdk | Out-Null
        }
        catch {
            $errors.Add($_.Exception.Message)
        }
    }
    elseif ($RidInfo.os -in @('macos', 'ios', 'tvos')) {
        if (-not $tools.InstallNameTool) { $errors.Add("install_name_tool was not found. Install Xcode command line tools for macOS runtime RPATH updates.") }
    }
    else {
        $errors.Add("Unsupported Unix RID OS '$($RidInfo.os)'.")
    }

    if ($errors.Count -gt 0) {
        throw "Unix build tool preflight failed with $($errors.Count) error(s): $($errors -join '; ')"
    }

    return $tools
}

function Invoke-ReleaseGitValue {
    param(
        [Parameter(Mandatory)]
        [string] $RepositoryPath,

        [Parameter(Mandatory)]
        [string[]] $Arguments
    )

    $output = & git -C $RepositoryPath @Arguments
    if ($LASTEXITCODE -ne 0) {
        throw "git -C $RepositoryPath $($Arguments -join ' ') failed with exit code $LASTEXITCODE"
    }

    return ($output | Out-String).Trim()
}

function Get-ReleaseGitDirtyCount {
    param(
        [Parameter(Mandatory)]
        [string] $RepositoryPath
    )

    $status = Invoke-ReleaseGitValue -RepositoryPath $RepositoryPath -Arguments @('status', '--porcelain', '--untracked-files=no')
    if (-not $status) {
        return 0
    }

    return @($status -split '\r?\n').Count
}

function Get-ReleaseNativeReceiptPath {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [string] $Component,

        [Parameter(Mandatory)]
        [string] $Rid
    )

    return (Join-Path (Resolve-ReleasePath $Manifest.artifactsRoot) "receipts/$Component/$Rid.json")
}

function Invoke-ReleaseCommand {
    param(
        [Parameter(Mandatory)]
        [string] $FilePath,

        [Parameter()]
        [string[]] $Arguments = @(),

        [Parameter()]
        [string] $WorkingDirectory = (Get-ReleaseRepoRoot),

        [switch] $DryRun
    )

    $rendered = "$FilePath $($Arguments -join ' ')"
    if ($DryRun) {
        Write-Host "[dry-run] $rendered"
        return
    }

    Push-Location $WorkingDirectory
    try {
        & $FilePath @Arguments
        if ($LASTEXITCODE -ne 0) {
            throw "Command failed with exit code $LASTEXITCODE`: $rendered"
        }
    }
    finally {
        Pop-Location
    }
}

function Get-ReleaseFilesByPattern {
    param(
        [Parameter(Mandatory)]
        [string] $Root,

        [Parameter(Mandatory)]
        [string] $Pattern
    )

    if (-not (Test-Path -LiteralPath $Root -PathType Container)) {
        return @()
    }

    $normalizedPattern = $Pattern.Replace('\', '/')
    $files = Get-ChildItem -LiteralPath $Root -Recurse -File -Force

    return @($files | Where-Object {
        $relative = [System.IO.Path]::GetRelativePath($Root, $_.FullName).Replace('\', '/')
        $relative -like $normalizedPattern
    })
}
