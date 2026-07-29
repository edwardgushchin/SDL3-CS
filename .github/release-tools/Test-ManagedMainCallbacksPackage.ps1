[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$PackagePath,

    [Parameter(Mandatory = $true)]
    [string]$PackageVersion
)

$ErrorActionPreference = 'Stop'
$resolvedPackage = (Resolve-Path -LiteralPath $PackagePath).Path
$packageDirectory = Split-Path -Parent $resolvedPackage
$temporaryBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$temporaryRoot = [System.IO.Path]::GetFullPath((Join-Path $temporaryBase ("sdl3-cs-main-callbacks-" + [Guid]::NewGuid().ToString('N'))))

if (-not $temporaryRoot.StartsWith($temporaryBase, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Temporary consumer path escaped the system temporary directory: $temporaryRoot"
}

New-Item -ItemType Directory -Path $temporaryRoot | Out-Null

try {
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($resolvedPackage)
    try {
        $entryNames = @($archive.Entries | ForEach-Object FullName)
        if ($entryNames -notcontains 'analyzers/dotnet/cs/SDL3-CS.Generators.dll') {
            throw 'SDL3-CS package does not contain the managed main callback generator under analyzers/dotnet/cs.'
        }

        $nuspecEntry = $archive.Entries | Where-Object { $_.FullName -like '*.nuspec' } | Select-Object -First 1
        if ($null -eq $nuspecEntry) {
            throw 'SDL3-CS package does not contain a nuspec.'
        }

        $reader = [System.IO.StreamReader]::new($nuspecEntry.Open())
        try {
            $nuspec = $reader.ReadToEnd()
        }
        finally {
            $reader.Dispose()
        }

        if ($nuspec -match 'Microsoft\.CodeAnalysis') {
            throw 'SDL3-CS runtime package must not expose a Microsoft.CodeAnalysis dependency.'
        }
    }
    finally {
        $archive.Dispose()
    }

    $projectPath = Join-Path $temporaryRoot 'Consumer.csproj'
    $sourcePath = Join-Path $temporaryRoot 'Game.cs'
    $nugetConfigPath = Join-Path $temporaryRoot 'NuGet.Config'

    @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
    <CompilerGeneratedFilesOutputPath>Generated</CompilerGeneratedFilesOutputPath>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="SDL3-CS" Version="$PackageVersion" />
  </ItemGroup>
</Project>
"@ | Set-Content -LiteralPath $projectPath -Encoding utf8NoBOM

    @'
using SDL3;

[SDL.GenerateMain]
internal sealed partial class Game : SDL.IMainCallbacks<Game>
{
    public static SDL.AppResult AppInit(out Game? appState, string[] args)
    {
        appState = new Game();
        return SDL.AppResult.Continue;
    }

    public SDL.AppResult AppIterate() => SDL.AppResult.Success;

    public SDL.AppResult AppEvent(ref SDL.Event @event) => SDL.AppResult.Continue;

    public void AppQuit(SDL.AppResult result)
    {
    }
}
'@ | Set-Content -LiteralPath $sourcePath -Encoding utf8NoBOM

    @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="local" value="$([System.Security.SecurityElement]::Escape($packageDirectory))" />
  </packageSources>
</configuration>
"@ | Set-Content -LiteralPath $nugetConfigPath -Encoding utf8NoBOM

    dotnet restore $projectPath --configfile $nugetConfigPath
    if ($LASTEXITCODE -ne 0) {
        throw "Managed main callback consumer restore failed with exit code $LASTEXITCODE."
    }

    dotnet build $projectPath -c Release --no-restore
    if ($LASTEXITCODE -ne 0) {
        throw "Managed main callback consumer build failed with exit code $LASTEXITCODE."
    }

    $generatedSource = Get-ChildItem -LiteralPath (Join-Path $temporaryRoot 'Generated') -Recurse -Filter '*.g.cs' |
        Where-Object { (Get-Content -LiteralPath $_.FullName -Raw) -match 'RunMainCallbacks<global::Game>' } |
        Select-Object -First 1
    if ($null -eq $generatedSource) {
        throw 'The package consumer build did not emit the expected SDL managed Main source.'
    }

    Write-Output 'Managed main callback package consumer validation passed.'
}
finally {
    if (Test-Path -LiteralPath $temporaryRoot) {
        $resolvedCleanupTarget = [System.IO.Path]::GetFullPath($temporaryRoot)
        if (-not $resolvedCleanupTarget.StartsWith($temporaryBase, [StringComparison]::OrdinalIgnoreCase)) {
            throw "Refusing to clean an unexpected path: $resolvedCleanupTarget"
        }

        Remove-Item -LiteralPath $resolvedCleanupTarget -Recurse -Force
    }
}
