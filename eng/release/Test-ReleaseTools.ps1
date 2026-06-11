#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $Rid,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $AllowCrossCompile
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$cmake = Get-ReleaseCMakePath
$nasm = Get-ReleaseNasmPath
$perl = Get-ReleasePerlPath
$dotnet = Get-ReleaseToolPath -Name 'dotnet'
$git = Get-ReleaseToolPath -Name 'git'
$gh = Get-ReleaseToolPath -Name 'gh'
$docker = Get-ReleaseToolPath -Name 'docker'
$errors = New-Object System.Collections.Generic.List[string]
$unixBuildTools = $null

if (-not $cmake) { $errors.Add('CMake was not found. Install CMake or set CMAKE_EXE.') }
if (-not $dotnet) { $errors.Add('dotnet CLI was not found.') }
if (-not $git) { $errors.Add('git was not found.') }

if ($Rid) {
    $ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $Rid
    $buildHost = if ($ridInfo.PSObject.Properties.Name.Contains('buildHost') -and $ridInfo.buildHost) { $ridInfo.buildHost } else { $ridInfo.os }

    if ($buildHost -eq 'linux-container' -and (Get-ReleaseHostOs) -ne 'linux') {
        if (-not $docker) {
            $errors.Add("Docker CLI was not found. RID '$($ridInfo.rid)' is configured for Linux container builds.")
        }
    }
    else {
        try {
            Assert-ReleaseRidHost -RidInfo $ridInfo -AllowCrossCompile:$AllowCrossCompile | Out-Null
        }
        catch {
            if ($buildHost -eq 'macos-xcode') {
                $errors.Add("RID '$($ridInfo.rid)' requires an external macOS host with Xcode command line tools; it cannot be built from Windows, WSL, or Linux Docker.")
            }
            else {
                $errors.Add($_.Exception.Message)
            }
        }
    }

    if ($ridInfo.os -eq 'windows') {
        $vs = Get-ReleaseVisualStudioPath
        if (-not $vs) {
            $errors.Add('Visual Studio Build Tools with MSBuild were not found.')
        }
        elseif (-not (Get-ReleaseVisualStudioCompilerPath -RidInfo $ridInfo)) {
            $hostToolset = Get-ReleaseVisualStudioHostToolset
            $targetToolset = Get-ReleaseVisualStudioTargetToolset -RidInfo $ridInfo
            $errors.Add("Visual Studio C++ compiler for RID '$($ridInfo.rid)' was not found. Expected $hostToolset\\$targetToolset\\cl.exe under Visual Studio Build Tools.")
        }
        if (-not $nasm) {
            $errors.Add('NASM was not found. Install NASM, set NASM_EXE, or bootstrap artifacts/tools/nasm-3.01 for Windows add-on builds.')
        }
        if (-not $perl) {
            $errors.Add('Perl was not found. Install Strawberry Perl or set PERL_EXE for Windows add-on builds.')
        }
    }
    else {
        if ((Get-ReleaseHostOs) -eq $ridInfo.os) {
            try {
                $unixBuildTools = Assert-ReleaseUnixBuildTools -RidInfo $ridInfo
            }
            catch {
                $errors.Add($_.Exception.Message)
            }
        }
        elseif ($ridInfo.os -eq 'android' -and (Get-ReleaseHostOs) -in @('linux', 'macos')) {
            try {
                $unixBuildTools = Assert-ReleaseUnixBuildTools -RidInfo $ridInfo
            }
            catch {
                $errors.Add($_.Exception.Message)
            }
        }
        elseif ($ridInfo.os -in @('ios', 'tvos') -and (Get-ReleaseHostOs) -eq 'macos') {
            try {
                $unixBuildTools = Assert-ReleaseUnixBuildTools -RidInfo $ridInfo
            }
            catch {
                $errors.Add($_.Exception.Message)
            }
        }
    }
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Release tool preflight failed with $($errors.Count) error(s)."
}

[pscustomobject]@{
    CMake = $cmake
    NASM = $nasm
    Perl = $perl
    DotNet = $dotnet
    Git = $git
    GitHubCli = $gh
    Docker = $docker
    AndroidNdk = Get-ReleaseAndroidNdkPath
    VisualStudio = Get-ReleaseVisualStudioPath
    VisualStudioCompiler = if ($Rid -and $ridInfo.os -eq 'windows') { Get-ReleaseVisualStudioCompilerPath -RidInfo $ridInfo } else { $null }
    UnixBuildTools = $unixBuildTools
    HostOs = Get-ReleaseHostOs
    HostArch = Get-ReleaseHostArch
} | Format-List
