#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $OutputPath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if (-not $IsWindows) {
    throw 'The win-x86 ShaderCross DXC smoke helper can only be compiled on Windows.'
}

$sourcePath = Join-Path $PSScriptRoot 'ShaderCrossDxcSmoke.cs'
if (-not (Test-Path -LiteralPath $sourcePath -PathType Leaf)) {
    throw "win-x86 ShaderCross DXC smoke helper source was not found: $sourcePath"
}

$resolvedOutputPath = [System.IO.Path]::GetFullPath($OutputPath)
$outputDirectory = [System.IO.Path]::GetDirectoryName($resolvedOutputPath)
if ([string]::IsNullOrWhiteSpace($outputDirectory) -or -not (Test-Path -LiteralPath $outputDirectory -PathType Container)) {
    throw "win-x86 ShaderCross DXC smoke helper output directory was not found: $outputDirectory"
}

if (Test-Path -LiteralPath $resolvedOutputPath) {
    $outputItem = Get-Item -LiteralPath $resolvedOutputPath -Force
    if (($outputItem.Attributes -band [System.IO.FileAttributes]::ReparsePoint) -ne 0) {
        throw "Refusing to replace reparse-point win-x86 smoke helper output: $resolvedOutputPath"
    }
}

$windowsDirectory = [Environment]::GetFolderPath([Environment+SpecialFolder]::Windows)
$compilerPath = Join-Path $windowsDirectory 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
if (-not (Test-Path -LiteralPath $compilerPath -PathType Leaf)) {
    throw "The 32-bit .NET Framework C# compiler was not found: $compilerPath"
}

$compilerArguments = @(
    '/nologo'
    '/target:exe'
    '/platform:x86'
    '/optimize+'
    "/out:$resolvedOutputPath"
    $sourcePath
)
& $compilerPath @compilerArguments
if ($LASTEXITCODE -ne 0) {
    throw "Compiling the win-x86 ShaderCross DXC smoke helper failed with exit code $LASTEXITCODE."
}
if (-not (Test-Path -LiteralPath $resolvedOutputPath -PathType Leaf)) {
    throw "The win-x86 ShaderCross DXC smoke helper compiler did not create: $resolvedOutputPath"
}

$stream = [System.IO.File]::OpenRead($resolvedOutputPath)
$reader = [System.IO.BinaryReader]::new($stream)
try {
    if ($reader.ReadUInt16() -ne 0x5A4D) {
        throw "The win-x86 ShaderCross DXC smoke helper has no MZ header: $resolvedOutputPath"
    }

    $stream.Position = 0x3C
    $peOffset = $reader.ReadInt32()
    if ($peOffset -lt 0 -or $peOffset -gt ($stream.Length - 6)) {
        throw "The win-x86 ShaderCross DXC smoke helper has an invalid PE offset: $resolvedOutputPath"
    }

    $stream.Position = $peOffset
    if ($reader.ReadUInt32() -ne 0x00004550) {
        throw "The win-x86 ShaderCross DXC smoke helper has no PE header: $resolvedOutputPath"
    }

    $machine = $reader.ReadUInt16()
    if ($machine -ne 0x014C) {
        throw "The ShaderCross DXC smoke helper is not PE32/x86 (machine=0x$($machine.ToString('X4'))): $resolvedOutputPath"
    }
}
finally {
    $reader.Dispose()
    $stream.Dispose()
}

Write-Output $resolvedOutputPath
