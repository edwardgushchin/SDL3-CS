#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string[]] $Rids,
    [string] $ScratchRoot,
    [string] $TargetFrameworkVersion = 'net10.0',
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-AndroidConsumerAbi {
    param(
        [Parameter(Mandatory)]
        [string] $Rid
    )

    switch ($Rid) {
        'android-arm' { return 'armeabi-v7a' }
        'android-arm64' { return 'arm64-v8a' }
        'android-x86' { return 'x86' }
        'android-x64' { return 'x86_64' }
        default { throw "RID '$Rid' is not a supported Android RID for consumer package build validation." }
    }
}

function Get-AndroidConsumerPackages {
    param(
        [Parameter(Mandatory)]
        [object[]] $Packages
    )

    return @($Packages | Where-Object {
        $_.Kind -eq 'managed' -or
        ($_.Kind -eq 'native' -and $_.NativePackagePlatform -eq 'Android')
    })
}

function ConvertTo-XmlPackageReference {
    param(
        [Parameter(Mandatory)]
        [object[]] $Packages
    )

    return (($Packages | ForEach-Object {
        "    <PackageReference Include=`"$($_.Id)`" Version=`"$($_.PackageVersion)`" />"
    }) -join [Environment]::NewLine)
}

function Get-AndroidConsumerMainActivity {
    return @"
using Android.App;
using Android.Content.PM;
using Org.Libsdl.App;
using SDL = SDL3.SDL;

[Activity(
    Label = "SDL3CSConsumer",
    MainLauncher = true,
    Exported = true,
    ConfigurationChanges =
        ConfigChanges.Orientation |
        ConfigChanges.ScreenSize |
        ConfigChanges.Keyboard |
        ConfigChanges.KeyboardHidden |
        ConfigChanges.Navigation |
        ConfigChanges.UiMode)]
public sealed class MainActivity : SDLActivity
{
    protected override string[] GetLibraries() =>
    [
        "SDL3",
        "SDL3_image",
        "SDL3_mixer",
        "SDL3_ttf",
        "SDL3_shadercross"
    ];

    protected override void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            return;
        }

        try
        {
            if (SDL.CreateWindowAndRenderer("SDL3-CS Android consumer", 64, 64, 0, out var window, out var renderer))
            {
                SDL.RenderClear(renderer);
                SDL.RenderPresent(renderer);
                SDL.DestroyRenderer(renderer);
                SDL.DestroyWindow(window);
            }
        }
        finally
        {
            SDL.Quit();
        }
    }
}
"@
}

function Get-AndroidConsumerManifest {
    return @"
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
  <uses-sdk android:minSdkVersion="23" />
  <application android:label="SDL3CSConsumer" android:allowBackup="false" android:supportsRtl="true" />
</manifest>
"@
}

function Read-ZipEntryBytes {
    param(
        [Parameter(Mandatory)]
        [System.IO.Compression.ZipArchiveEntry] $Entry
    )

    $stream = $Entry.Open()
    try {
        $memory = [System.IO.MemoryStream]::new()
        try {
            $stream.CopyTo($memory)
            return $memory.ToArray()
        }
        finally {
            $memory.Dispose()
        }
    }
    finally {
        $stream.Dispose()
    }
}

function Test-BytePattern {
    param(
        [Parameter(Mandatory)]
        [byte[]] $Bytes,

        [Parameter(Mandatory)]
        [byte[]] $Pattern
    )

    if ($Pattern.Length -eq 0 -or $Bytes.Length -lt $Pattern.Length) {
        return $false
    }

    for ($i = 0; $i -le $Bytes.Length - $Pattern.Length; $i++) {
        $matched = $true
        for ($j = 0; $j -lt $Pattern.Length; $j++) {
            if ($Bytes[$i + $j] -ne $Pattern[$j]) {
                $matched = $false
                break
            }
        }

        if ($matched) {
            return $true
        }
    }

    return $false
}

function Test-AndroidApkNativeLibraries {
    param(
        [Parameter(Mandatory)]
        [string] $ApkPath,

        [Parameter(Mandatory)]
        [string] $Abi,

        [Parameter(Mandatory)]
        [string[]] $ExpectedLibraries
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($ApkPath)
    try {
        $entries = @($archive.Entries | ForEach-Object { $_.FullName })
        foreach ($library in $ExpectedLibraries) {
            $entryName = "lib/$Abi/$library"
            if ($entries -notcontains $entryName) {
                throw "Android APK '$ApkPath' is missing expected native entry '$entryName'."
            }
        }
    }
    finally {
        $archive.Dispose()
    }
}

function Test-AndroidApkManagedAssembly {
    param(
        [Parameter(Mandatory)]
        [string] $ApkPath,

        [Parameter(Mandatory)]
        [string] $AssemblyName
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($ApkPath)
    try {
        $entryMatches = @($archive.Entries | Where-Object {
            $entryName = $_.FullName.Replace('\', '/')
            $entryName.EndsWith("/$AssemblyName", [System.StringComparison]::Ordinal) -or
            $entryName -eq $AssemblyName
        })
        if ($entryMatches.Count -gt 0) {
            return
        }

        $assemblyStoreEntries = @($archive.Entries | Where-Object {
            $_.FullName.Replace('\', '/') -match '^lib/[^/]+/libassembly-store\.so$'
        })
        $assemblyNameBytes = [System.Text.Encoding]::ASCII.GetBytes($AssemblyName)
        foreach ($entry in $assemblyStoreEntries) {
            if (Test-BytePattern -Bytes (Read-ZipEntryBytes -Entry $entry) -Pattern $assemblyNameBytes) {
                return
            }
        }

        throw "Android APK '$ApkPath' is missing managed assembly '$AssemblyName' as an APK entry or assembly-store payload."
    }
    finally {
        $archive.Dispose()
    }
}

function Test-AndroidApkDexClasses {
    param(
        [Parameter(Mandatory)]
        [string] $ApkPath,

        [Parameter(Mandatory)]
        [string[]] $ExpectedClassDescriptors
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($ApkPath)
    try {
        $dexEntries = @($archive.Entries | Where-Object { $_.FullName -match '^classes[0-9]*\.dex$' })
        if ($dexEntries.Count -eq 0) {
            throw "Android APK '$ApkPath' does not contain classes.dex."
        }

        foreach ($descriptor in $ExpectedClassDescriptors) {
            $pattern = [System.Text.Encoding]::ASCII.GetBytes($descriptor)
            $found = $false
            foreach ($entry in $dexEntries) {
                $bytes = Read-ZipEntryBytes -Entry $entry
                if (Test-BytePattern -Bytes $bytes -Pattern $pattern) {
                    $found = $true
                    break
                }
            }

            if (-not $found) {
                throw "Android APK '$ApkPath' DEX payload is missing class descriptor '$descriptor'."
            }
        }
    }
    finally {
        $archive.Dispose()
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir
if (-not $ScratchRoot) {
    $ScratchRoot = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'android-consumer-package-build'
}

$androidRids = @($manifest.rids | Where-Object { $_.os -eq 'android' } | ForEach-Object { $_.rid })
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = $androidRids
}

foreach ($rid in $Rids) {
    if ($rid -notin $androidRids) {
        throw "RID '$rid' is not configured as an Android RID in release manifest."
    }
}

& (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath

$allPackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
$packages = @(Get-AndroidConsumerPackages -Packages $allPackages)
$requiredPackageIds = @(
    'SDL3-CS',
    'SDL3-CS.Android',
    'SDL3-CS.Android.Image',
    'SDL3-CS.Android.Mixer',
    'SDL3-CS.Android.TTF',
    'SDL3-CS.Android.Shadercross'
)
foreach ($requiredPackageId in $requiredPackageIds) {
    if (@($packages | Where-Object { $_.Id -eq $requiredPackageId }).Count -ne 1) {
        throw "Android consumer validation expected package '$requiredPackageId' in release package versioning output."
    }
}

if (-not $DryRun) {
    foreach ($package in $packages) {
        $packagePath = Get-ReleaseNuGetPackagePath -PackageDir $PackageDir -Package $package
        if (-not (Test-Path -LiteralPath $packagePath -PathType Leaf)) {
            throw "Expected NuGet package for Android consumer validation is missing: $packagePath"
        }
    }
}

$packageReferences = ConvertTo-XmlPackageReference -Packages $packages
$targetFramework = "$TargetFrameworkVersion-android"
$runRoot = Join-Path (Resolve-ReleasePath $ScratchRoot) ([System.Guid]::NewGuid().ToString('N'))
$expectedPrimaryLibraries = @(
    'libSDL3.so',
    'libSDL3_image.so',
    'libSDL3_mixer.so',
    'libSDL3_ttf.so',
    'libSDL3_shadercross.so'
)
$expectedBridgeClassDescriptors = @(
    'Lorg/libsdl/app/SDLActivity;',
    'Lorg/libsdl/app/SDLMain;'
)
$rows = New-Object System.Collections.Generic.List[object]

foreach ($rid in $Rids) {
    $abi = Get-AndroidConsumerAbi -Rid $rid
    $projectRoot = Join-Path $runRoot $rid
    $projectPath = Join-Path $projectRoot 'SDL3CSConsumer.csproj'
    $applicationId = "com.edwardgushchin.sdl3cs.consumer.$($rid.Replace('-', '.'))"

    $projectXml = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>$targetFramework</TargetFramework>
    <OutputType>Exe</OutputType>
    <RuntimeIdentifier>$rid</RuntimeIdentifier>
    <Nullable>enable</Nullable>
    <ImplicitUsings>true</ImplicitUsings>
    <SupportedOSPlatformVersion>23.0</SupportedOSPlatformVersion>
    <ApplicationId>$applicationId</ApplicationId>
    <ApplicationVersion>1</ApplicationVersion>
    <ApplicationDisplayVersion>1.0</ApplicationDisplayVersion>
    <AndroidPackageFormat>apk</AndroidPackageFormat>
    <RunAOTCompilation>false</RunAOTCompilation>
    <AndroidEnableProfiledAot>false</AndroidEnableProfiledAot>
    <EnableDefaultItems>false</EnableDefaultItems>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="MainActivity.cs" />
    <None Include="AndroidManifest.xml" />
$packageReferences
  </ItemGroup>
</Project>
"@

    if ($DryRun) {
        $rows.Add([pscustomobject]@{
            Rid = $rid
            Abi = $abi
            TargetFramework = $targetFramework
            Project = $projectPath
            Status = 'dry-run'
        })
        Write-Host "[dry-run] create Android SDLActivity consumer project for $rid at $projectPath"
        Write-Host "[dry-run] add package references: $((@($packages | ForEach-Object { $_.Id })) -join ', ')"
        Write-Host "[dry-run] dotnet restore $projectPath -r $rid --configfile <generated NuGet.config>"
        Write-Host "[dry-run] dotnet build $projectPath -c Release -f $targetFramework -r $rid --no-restore"
        Write-Host "[dry-run] inspect APK for lib/$abi/{libSDL3.so,libSDL3_image.so,libSDL3_mixer.so,libSDL3_ttf.so,libSDL3_shadercross.so}"
        Write-Host "[dry-run] inspect APK for managed assemblies SDL3CSConsumer.dll, SDL3-CS.dll, SDL3-CS.Android.dll and DEX descriptors Lorg/libsdl/app/SDLActivity; / Lorg/libsdl/app/SDLMain;"
        continue
    }

    New-Item -ItemType Directory -Force -Path $projectRoot | Out-Null
    Set-Content -LiteralPath $projectPath -Value $projectXml -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'MainActivity.cs') -Value (Get-AndroidConsumerMainActivity) -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'AndroidManifest.xml') -Value (Get-AndroidConsumerManifest) -Encoding UTF8

    $nugetConfig = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="local-release" value="$($PackageDir.Replace('\', '/'))" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
"@
    $nugetConfigPath = Join-Path $projectRoot 'NuGet.config'
    Set-Content -LiteralPath $nugetConfigPath -Value $nugetConfig -Encoding UTF8

    & dotnet restore $projectPath -r $rid --configfile $nugetConfigPath
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet restore failed for Android consumer RID '$rid' with exit code $LASTEXITCODE."
    }

    & dotnet build $projectPath -c Release -f $targetFramework -r $rid --no-restore
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet build failed for Android consumer RID '$rid' with exit code $LASTEXITCODE."
    }

    $apk = @(Get-ChildItem -LiteralPath $projectRoot -Recurse -Filter '*.apk' -File | Sort-Object LastWriteTimeUtc -Descending | Select-Object -First 1)
    if ($apk.Count -ne 1) {
        throw "Android consumer build for RID '$rid' did not produce a single APK under $projectRoot."
    }

    Test-AndroidApkNativeLibraries -ApkPath $apk[0].FullName -Abi $abi -ExpectedLibraries $expectedPrimaryLibraries
    Test-AndroidApkManagedAssembly -ApkPath $apk[0].FullName -AssemblyName 'SDL3CSConsumer.dll'
    Test-AndroidApkManagedAssembly -ApkPath $apk[0].FullName -AssemblyName 'SDL3-CS.dll'
    Test-AndroidApkManagedAssembly -ApkPath $apk[0].FullName -AssemblyName 'SDL3-CS.Android.dll'
    Test-AndroidApkDexClasses -ApkPath $apk[0].FullName -ExpectedClassDescriptors $expectedBridgeClassDescriptors

    $rows.Add([pscustomobject]@{
        Rid = $rid
        Abi = $abi
        TargetFramework = $targetFramework
        Project = $projectPath
        Status = 'built-managed-main'
    })
}

$rows | Sort-Object Rid | Format-Table -AutoSize
Write-Host "Android SDLActivity managed-main consumer package validation completed."
