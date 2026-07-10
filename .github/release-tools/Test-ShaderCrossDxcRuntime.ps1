#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $Rid,

    [Parameter(Mandatory)]
    [string] $InstallRoot,

    [switch] $ChildProcess
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest
$ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $Rid
if ($ridInfo.os -notin @('windows', 'linux', 'macos')) {
    throw "ShaderCross DXC runtime smoke test only supports desktop RID, got '$Rid'."
}

$resolvedInstallRoot = [System.IO.Path]::GetFullPath((Resolve-ReleasePath $InstallRoot))
$runtimeDir = if ($ridInfo.os -eq 'windows') {
    Join-Path $resolvedInstallRoot 'bin'
}
else {
    Join-Path $resolvedInstallRoot 'lib'
}

if (-not (Test-Path -LiteralPath $runtimeDir -PathType Container)) {
    throw "ShaderCross runtime directory was not found: $runtimeDir"
}

if (-not $ChildProcess) {
    $oldPath = $env:PATH
    $oldLdLibraryPath = $env:LD_LIBRARY_PATH
    $oldDyldLibraryPath = $env:DYLD_LIBRARY_PATH
    try {
        $env:PATH = "$runtimeDir$([System.IO.Path]::PathSeparator)$oldPath"
        if ($ridInfo.os -eq 'linux') {
            $env:LD_LIBRARY_PATH = "$runtimeDir$([System.IO.Path]::PathSeparator)$oldLdLibraryPath"
        }
        elseif ($ridInfo.os -eq 'macos') {
            $env:DYLD_LIBRARY_PATH = "$runtimeDir$([System.IO.Path]::PathSeparator)$oldDyldLibraryPath"
        }

        $pwshPath = (Get-Command pwsh -ErrorAction Stop).Source
        & $pwshPath -NoProfile -File $PSCommandPath -Rid $Rid -InstallRoot $resolvedInstallRoot -ChildProcess
        if ($LASTEXITCODE -ne 0) {
            throw "ShaderCross DXC runtime child process failed for $Rid with exit code $LASTEXITCODE."
        }
    }
    finally {
        $env:PATH = $oldPath
        $env:LD_LIBRARY_PATH = $oldLdLibraryPath
        $env:DYLD_LIBRARY_PATH = $oldDyldLibraryPath
    }
    return
}

$shaderCrossLibrary = switch ($ridInfo.os) {
    'windows' { Join-Path $runtimeDir 'SDL3_shadercross.dll' }
    'linux' { Join-Path $runtimeDir 'libSDL3_shadercross.so' }
    'macos' { Join-Path $runtimeDir 'libSDL3_shadercross.dylib' }
}
$sdlLibrary = switch ($ridInfo.os) {
    'windows' { Join-Path $runtimeDir 'SDL3.dll' }
    'linux' { Join-Path $runtimeDir 'libSDL3.so' }
    'macos' { Join-Path $runtimeDir 'libSDL3.dylib' }
}

foreach ($libraryPath in @($shaderCrossLibrary, $sdlLibrary)) {
    if (-not (Test-Path -LiteralPath $libraryPath -PathType Leaf)) {
        throw "Required runtime library was not found: $libraryPath"
    }
}

function ConvertTo-CSharpVerbatimLiteral {
    param([Parameter(Mandatory)][string] $Value)
    return '@"' + $Value.Replace('"', '""') + '"'
}

$shaderCrossLiteral = ConvertTo-CSharpVerbatimLiteral -Value $shaderCrossLibrary
$sdlLiteral = ConvertTo-CSharpVerbatimLiteral -Value $sdlLibrary
$nativeSource = @"
using System;
using System.Runtime.InteropServices;

public static class ShaderCrossDxcSmokeNative
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HlslInfo
    {
        public IntPtr Source;
        public IntPtr Entrypoint;
        public IntPtr IncludeDir;
        public IntPtr Defines;
        public int ShaderStage;
        public uint Props;
    }

    [DllImport($shaderCrossLiteral, EntryPoint = "SDL_ShaderCross_Init", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Init();

    [DllImport($shaderCrossLiteral, EntryPoint = "SDL_ShaderCross_Quit", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Quit();

    [DllImport($shaderCrossLiteral, EntryPoint = "SDL_ShaderCross_GetHLSLShaderFormats", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint GetHlslShaderFormats();

    [DllImport($shaderCrossLiteral, EntryPoint = "SDL_ShaderCross_CompileSPIRVFromHLSL", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CompileSpirvFromHlsl(ref HlslInfo info, out UIntPtr size);

    [DllImport($sdlLiteral, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetError();

    [DllImport($sdlLiteral, EntryPoint = "SDL_free", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Free(IntPtr memory);
}
"@

Add-Type -TypeDefinition $nativeSource -Language CSharp

$sourcePointer = [Runtime.InteropServices.Marshal]::StringToCoTaskMemUTF8('float4 main(float4 position : POSITION) : SV_Position { return position; }')
$entrypointPointer = [Runtime.InteropServices.Marshal]::StringToCoTaskMemUTF8('main')
$initialized = $false
$compiled = [IntPtr]::Zero
try {
    $initialized = [ShaderCrossDxcSmokeNative]::Init()
    if (-not $initialized) {
        $errorText = [Runtime.InteropServices.Marshal]::PtrToStringUTF8([ShaderCrossDxcSmokeNative]::GetError())
        throw "SDL_ShaderCross_Init failed for $Rid`: $errorText"
    }

    $formats = [ShaderCrossDxcSmokeNative]::GetHlslShaderFormats()
    if (($formats -band 0x2u) -eq 0) {
        throw "SDL_ShaderCross_GetHLSLShaderFormats returned 0x$($formats.ToString('x8')) for $Rid without SPIR-V support."
    }

    $info = [ShaderCrossDxcSmokeNative+HlslInfo]::new()
    $info.Source = $sourcePointer
    $info.Entrypoint = $entrypointPointer
    $info.ShaderStage = 0
    $size = [UIntPtr]::Zero
    $compiled = [ShaderCrossDxcSmokeNative]::CompileSpirvFromHlsl([ref] $info, [ref] $size)
    if ($compiled -eq [IntPtr]::Zero -or $size.ToUInt64() -eq 0) {
        $errorText = [Runtime.InteropServices.Marshal]::PtrToStringUTF8([ShaderCrossDxcSmokeNative]::GetError())
        throw "SDL_ShaderCross_CompileSPIRVFromHLSL failed for $Rid`: $errorText"
    }

    Write-Host "ShaderCross DXC runtime smoke test passed for $Rid (formats=0x$($formats.ToString('x8')), SPIR-V bytes=$($size.ToUInt64()))."
}
finally {
    if ($compiled -ne [IntPtr]::Zero) {
        [ShaderCrossDxcSmokeNative]::Free($compiled)
    }
    if ($initialized) {
        [ShaderCrossDxcSmokeNative]::Quit()
    }
    [Runtime.InteropServices.Marshal]::FreeCoTaskMem($sourcePointer)
    [Runtime.InteropServices.Marshal]::FreeCoTaskMem($entrypointPointer)
}
