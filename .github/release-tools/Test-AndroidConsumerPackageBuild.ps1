#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string[]] $Rids,
    [string] $ScratchRoot,
    [string] $TargetFrameworkVersion = 'net10.0',
    [string] $ZipAlignPath,
    [string] $BundleToolPath,
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

function Resolve-AndroidToolPath {
    param(
        [AllowNull()][string] $Candidate,
        [Parameter(Mandatory)][string] $CommandName
    )

    if (-not [string]::IsNullOrWhiteSpace($Candidate)) {
        if (Test-Path -LiteralPath $Candidate -PathType Leaf) {
            return (Resolve-Path -LiteralPath $Candidate).Path
        }

        $candidateCommand = Get-Command $Candidate -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($candidateCommand) {
            return $candidateCommand.Source
        }

        return $null
    }

    $command = Get-Command $CommandName -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($command) {
        return $command.Source
    }

    return $null
}

function Get-AndroidZipAlignPath {
    param(
        [AllowNull()][string] $Candidate,
        [Parameter(Mandatory)][string] $BuildToolsVersion
    )

    if (-not [string]::IsNullOrWhiteSpace($Candidate)) {
        return Resolve-AndroidToolPath -Candidate $Candidate -CommandName 'zipalign'
    }

    foreach ($environmentVariable in @('ANDROID_HOME', 'ANDROID_SDK_ROOT')) {
        $androidSdkRoot = [Environment]::GetEnvironmentVariable($environmentVariable)
        if (-not $androidSdkRoot) {
            continue
        }

        $exactBuildToolsRoot = Join-Path $androidSdkRoot "build-tools/$BuildToolsVersion"
        foreach ($fileName in @('zipalign', 'zipalign.exe')) {
            $path = Join-Path $exactBuildToolsRoot $fileName
            if (Test-Path -LiteralPath $path -PathType Leaf) {
                return (Resolve-Path -LiteralPath $path).Path
            }
        }
    }

    return $null
}

function Invoke-AndroidZipAlignmentCheck {
    param(
        [Parameter(Mandatory)][string] $ToolPath,
        [Parameter(Mandatory)][string] $ApkPath
    )

    & $ToolPath '-c' '-P' '16' '-v' '4' $ApkPath
    if ($LASTEXITCODE -ne 0) {
        throw "zipalign 16 KB validation failed for Android APK '$ApkPath' with exit code $LASTEXITCODE."
    }
}

function Invoke-AndroidBundleAlignmentCheck {
    param(
        [Parameter(Mandatory)][string] $ToolPath,
        [Parameter(Mandatory)][string] $AabPath
    )

    $arguments = @('dump', 'config', "--bundle=$AabPath")
    if ([System.IO.Path]::GetExtension($ToolPath).Equals('.jar', [System.StringComparison]::OrdinalIgnoreCase)) {
        $java = Resolve-AndroidToolPath -Candidate $null -CommandName 'java'
        if (-not $java) {
            throw "Java was not found; it is required to run bundletool '$ToolPath'."
        }

        $output = @(& $java '-jar' $ToolPath @arguments 2>&1 | ForEach-Object { $_.ToString() })
    }
    else {
        $output = @(& $ToolPath @arguments 2>&1 | ForEach-Object { $_.ToString() })
    }

    if ($LASTEXITCODE -ne 0) {
        throw "bundletool config validation failed for Android App Bundle '$AabPath' with exit code $LASTEXITCODE`: $($output -join [Environment]::NewLine)"
    }
    if (($output -join [Environment]::NewLine) -notmatch '(?m)\bPAGE_ALIGNMENT_16K\b') {
        throw "Android App Bundle '$AabPath' does not request PAGE_ALIGNMENT_16K."
    }

    $output | ForEach-Object { Write-Host $_ }
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
    param(
        [Parameter(Mandatory)]
        [string] $ApplicationId
    )

    return @"
using Android.App;
using Android.Content.PM;
using Org.Libsdl.App;
using SDL = SDL3.SDL;

[Activity(
    Name = "$ApplicationId.MainActivity",
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

        Android.Util.Log.Info("SDL3CSConsumer", "SDL3CS_RUNTIME_READY");

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

function Get-AndroidConsumerBundleConfiguration {
    return @'
{
  "optimizations": {
    "uncompressNativeLibraries": {
      "enabled": true,
      "alignment": "PAGE_ALIGNMENT_16K"
    }
  }
}
'@
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
        [string[]] $ExpectedLibraries,

        [Parameter(Mandatory)]
        [string] $PageSizeValidatorPath
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($ApkPath)
    try {
        $nativeEntries = @($archive.Entries | Where-Object {
            $_.FullName.Replace('\', '/') -match "^lib/$([regex]::Escape($Abi))/.+\.so(?:\..+)?$"
        })
        if ($nativeEntries.Count -eq 0) {
            throw "Android APK '$ApkPath' contains no native shared libraries for ABI '$Abi'."
        }

        $entryNames = @($nativeEntries | ForEach-Object { $_.FullName.Replace('\', '/') })
        foreach ($library in $ExpectedLibraries) {
            $entryName = "lib/$Abi/$library"
            if ($entryNames -notcontains $entryName) {
                throw "Android APK '$ApkPath' is missing expected native entry '$entryName'."
            }
        }

    }
    finally {
        $archive.Dispose()
    }

    & $PageSizeValidatorPath -Path $ApkPath
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
$androidBuildToolsVersion = if ($manifest.PSObject.Properties.Name.Contains('toolchains') -and
    $manifest.toolchains -and
    $manifest.toolchains.PSObject.Properties.Name.Contains('androidBuildToolsVersion')) {
    [string]$manifest.toolchains.androidBuildToolsVersion
}
else {
    $null
}
if ([string]::IsNullOrWhiteSpace($androidBuildToolsVersion)) {
    throw 'Release manifest must declare toolchains.androidBuildToolsVersion for exact zipalign selection.'
}
$bundletoolVersion = if ($manifest.toolchains.PSObject.Properties.Name.Contains('bundletoolVersion')) {
    [string]$manifest.toolchains.bundletoolVersion
}
else {
    $null
}
$bundletoolSha256 = if ($manifest.toolchains.PSObject.Properties.Name.Contains('bundletoolSha256')) {
    [string]$manifest.toolchains.bundletoolSha256
}
else {
    $null
}
if ([string]::IsNullOrWhiteSpace($bundletoolVersion) -or $bundletoolVersion -notmatch '^\d+\.\d+\.\d+$') {
    throw 'Release manifest must declare an exact toolchains.bundletoolVersion.'
}
if ([string]::IsNullOrWhiteSpace($bundletoolSha256) -or $bundletoolSha256 -notmatch '^[0-9a-f]{64}$') {
    throw 'Release manifest must declare a lowercase toolchains.bundletoolSha256.'
}

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
$pageSizeValidatorPath = Join-Path $PSScriptRoot 'Test-AndroidPageSizeCompatibility.ps1'
$resolvedZipAlignPath = $null
$resolvedBundleToolPath = $null

if (-not $DryRun) {
    if (-not (Test-Path -LiteralPath $pageSizeValidatorPath -PathType Leaf)) {
        throw "Android page-size compatibility validator was not found: $pageSizeValidatorPath"
    }

    $resolvedZipAlignPath = Get-AndroidZipAlignPath -Candidate $ZipAlignPath -BuildToolsVersion $androidBuildToolsVersion
    if (-not $resolvedZipAlignPath) {
        throw "zipalign was not found. Pass -ZipAlignPath or install Android SDK build-tools $androidBuildToolsVersion under ANDROID_HOME/ANDROID_SDK_ROOT."
    }
    $resolvedBuildToolsRoot = Split-Path -Parent $resolvedZipAlignPath
    if ((Split-Path -Leaf $resolvedBuildToolsRoot) -ne $androidBuildToolsVersion) {
        throw "zipalign '$resolvedZipAlignPath' is not from exact Android SDK build-tools $androidBuildToolsVersion."
    }
    $buildToolsSourceProperties = Join-Path $resolvedBuildToolsRoot 'source.properties'
    if (-not (Test-Path -LiteralPath $buildToolsSourceProperties -PathType Leaf)) {
        throw "Android SDK build-tools source.properties was not found beside zipalign: $buildToolsSourceProperties"
    }
    $buildToolsRevisionMatches = @(Get-Content -LiteralPath $buildToolsSourceProperties -Encoding UTF8 | Where-Object {
        $_ -match '^\s*Pkg\.Revision\s*=\s*(\S+)\s*$'
    })
    if ($buildToolsRevisionMatches.Count -ne 1 -or
        [regex]::Match($buildToolsRevisionMatches[0], '^\s*Pkg\.Revision\s*=\s*(\S+)\s*$').Groups[1].Value -ne $androidBuildToolsVersion) {
        throw "zipalign '$resolvedZipAlignPath' does not prove Android SDK build-tools revision $androidBuildToolsVersion."
    }

    $bundleToolCandidate = if ($BundleToolPath) { $BundleToolPath } else { [Environment]::GetEnvironmentVariable('BUNDLETOOL_PATH') }
    $resolvedBundleToolPath = Resolve-AndroidToolPath -Candidate $bundleToolCandidate -CommandName 'bundletool'
    if (-not $resolvedBundleToolPath) {
        throw 'bundletool was not found. Pass -BundleToolPath, set BUNDLETOOL_PATH, or install bundletool on PATH.'
    }
    $expectedBundletoolFileName = "bundletool-all-$bundletoolVersion.jar"
    if ((Split-Path -Leaf $resolvedBundleToolPath) -ne $expectedBundletoolFileName) {
        throw "bundletool must use exact manifest asset '$expectedBundletoolFileName', got '$resolvedBundleToolPath'."
    }
    $actualBundletoolSha256 = (Get-FileHash -LiteralPath $resolvedBundleToolPath -Algorithm SHA256).Hash.ToLowerInvariant()
    if ($actualBundletoolSha256 -ne $bundletoolSha256) {
        throw "bundletool SHA-256 mismatch for '$resolvedBundleToolPath': expected $bundletoolSha256, got $actualBundletoolSha256."
    }
}

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
    <AndroidPackageFormats>apk;aab</AndroidPackageFormats>
    <AndroidBundleConfigurationFile>BundleConfig.json</AndroidBundleConfigurationFile>
    <RunAOTCompilation>false</RunAOTCompilation>
    <AndroidEnableProfiledAot>false</AndroidEnableProfiledAot>
    <EnableDefaultItems>false</EnableDefaultItems>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="MainActivity.cs" />
    <None Include="AndroidManifest.xml" />
    <None Include="BundleConfig.json" />
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
        Write-Host "[dry-run] inspect every lib/$abi/*.so in the final signed APK with Test-AndroidPageSizeCompatibility.ps1"
        Write-Host "[dry-run] Android SDK build-tools $androidBuildToolsVersion`: zipalign -c -P 16 -v 4 <signed APK>"
        Write-Host "[dry-run] bundletool dump config --bundle=<AAB>; require PAGE_ALIGNMENT_16K"
        Write-Host "[dry-run] inspect APK for managed assemblies SDL3CSConsumer.dll, SDL3-CS.dll, SDL3-CS.Android.dll and DEX descriptors Lorg/libsdl/app/SDLActivity; / Lorg/libsdl/app/SDLMain;"
        continue
    }

    New-Item -ItemType Directory -Force -Path $projectRoot | Out-Null
    Set-Content -LiteralPath $projectPath -Value $projectXml -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'MainActivity.cs') -Value (Get-AndroidConsumerMainActivity -ApplicationId $applicationId) -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'AndroidManifest.xml') -Value (Get-AndroidConsumerManifest) -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'BundleConfig.json') -Value (Get-AndroidConsumerBundleConfiguration) -Encoding UTF8

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

    $consumerBinRoot = Join-Path $projectRoot 'bin'
    $apk = @(Get-ChildItem -LiteralPath $consumerBinRoot -Recurse -Filter '*-Signed.apk' -File | Sort-Object LastWriteTimeUtc -Descending | Select-Object -First 1)
    if ($apk.Count -ne 1) {
        throw "Android consumer build for RID '$rid' did not produce a signed APK under $consumerBinRoot."
    }

    $aab = @(Get-ChildItem -LiteralPath $consumerBinRoot -Recurse -Filter '*.aab' -File | Sort-Object LastWriteTimeUtc -Descending | Select-Object -First 1)
    if ($aab.Count -ne 1) {
        throw "Android consumer build for RID '$rid' did not produce an Android App Bundle under $consumerBinRoot."
    }

    Test-AndroidApkNativeLibraries `
        -ApkPath $apk[0].FullName `
        -Abi $abi `
        -ExpectedLibraries $expectedPrimaryLibraries `
        -PageSizeValidatorPath $pageSizeValidatorPath
    Invoke-AndroidZipAlignmentCheck -ToolPath $resolvedZipAlignPath -ApkPath $apk[0].FullName
    Invoke-AndroidBundleAlignmentCheck -ToolPath $resolvedBundleToolPath -AabPath $aab[0].FullName
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
