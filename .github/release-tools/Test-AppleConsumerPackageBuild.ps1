#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $PackageDir,
    [string[]] $Rids,
    [string] $ScratchRoot,
    [string] $TargetFrameworkVersion = 'net10.0',
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Debug',
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-AppleConsumerPlatform {
    param(
        [Parameter(Mandatory)]
        [string] $Rid
    )

    if ($Rid -like 'iossimulator-*') {
        return 'ios'
    }

    if ($Rid -like 'tvossimulator-*') {
        return 'tvos'
    }

    throw "RID '$Rid' is not a supported Apple simulator RID for consumer package build validation."
}

function Get-AppleConsumerTargetFramework {
    param(
        [Parameter(Mandatory)]
        [string] $Platform,

        [Parameter(Mandatory)]
        [string] $TargetFrameworkVersion
    )

    return "$TargetFrameworkVersion-$Platform"
}

function Get-AppleConsumerInfoPlist {
    param(
        [Parameter(Mandatory)]
        [string] $Platform,

        [Parameter(Mandatory)]
        [string] $BundleIdentifier
    )

    $deviceFamily = if ($Platform -eq 'tvos') { '<integer>3</integer>' } else { '<integer>1</integer><integer>2</integer>' }

    return @"
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "https://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
  <key>CFBundleIdentifier</key>
  <string>$BundleIdentifier</string>
  <key>CFBundleName</key>
  <string>SDL3CSConsumer</string>
  <key>CFBundleDisplayName</key>
  <string>SDL3CSConsumer</string>
  <key>CFBundleVersion</key>
  <string>1</string>
  <key>CFBundleShortVersionString</key>
  <string>1.0</string>
  <key>UIDeviceFamily</key>
  <array>
    $deviceFamily
  </array>
</dict>
</plist>
"@
}

function Get-AppleConsumerProgram {
    return @"
using Foundation;
using UIKit;

UIApplication.Main(args, null, typeof(AppDelegate));

[Register("AppDelegate")]
public sealed class AppDelegate : UIApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        return true;
    }
}
"@
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

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if (-not $PackageDir) {
    $PackageDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$PackageDir = Resolve-ReleasePath $PackageDir
if (-not $ScratchRoot) {
    $ScratchRoot = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'apple-consumer-package-build'
}

$simulatorRids = @($manifest.rids | Where-Object { $_.rid -like 'iossimulator-*' -or $_.rid -like 'tvossimulator-*' } | ForEach-Object { $_.rid })
if (-not $Rids -or $Rids.Count -eq 0) {
    if ($IsMacOS) {
        $hostArch = Get-ReleaseHostArch
        if ($hostArch -eq 'arm64') {
            $Rids = @('iossimulator-arm64', 'tvossimulator-arm64')
        }
        else {
            $Rids = @('iossimulator-x64', 'tvossimulator-x64')
        }
    }
    else {
        $Rids = $simulatorRids
    }
}

foreach ($rid in $Rids) {
    if ($rid -notin $simulatorRids) {
        throw "RID '$rid' is not configured as an Apple simulator RID in release manifest."
    }
}

& (Join-Path $PSScriptRoot 'Test-PackageVersioning.ps1') -PackageRevision $PackageRevision -ManifestPath $ManifestPath

if (-not $DryRun) {
    if (-not $IsMacOS) {
        throw "Apple consumer package build validation requires macOS with Xcode and .NET iOS/tvOS workloads. Use -DryRun on non-macOS hosts."
    }
}

$packages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
if (-not $DryRun) {
    foreach ($package in $packages) {
        $packagePath = Get-ReleaseNuGetPackagePath -PackageDir $PackageDir -Package $package
        if (-not (Test-Path -LiteralPath $packagePath -PathType Leaf)) {
            throw "Expected NuGet package for Apple consumer validation is missing: $packagePath"
        }
    }
}
$packageReferences = ConvertTo-XmlPackageReference -Packages $packages
$runRoot = Join-Path (Resolve-ReleasePath $ScratchRoot) ([System.Guid]::NewGuid().ToString('N'))
$rows = New-Object System.Collections.Generic.List[object]

foreach ($rid in $Rids) {
    $platform = Get-AppleConsumerPlatform -Rid $rid
    $targetFramework = Get-AppleConsumerTargetFramework -Platform $platform -TargetFrameworkVersion $TargetFrameworkVersion
    $projectRoot = Join-Path $runRoot $rid
    $projectPath = Join-Path $projectRoot 'SDL3CSConsumer.csproj'
    $bundleIdentifier = "com.edwardgushchin.sdl3cs.consumer.$($rid.Replace('-', '.'))"

    $projectXml = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>$targetFramework</TargetFramework>
    <OutputType>Exe</OutputType>
    <RuntimeIdentifier>$rid</RuntimeIdentifier>
    <Nullable>enable</Nullable>
    <ImplicitUsings>true</ImplicitUsings>
    <SupportedOSPlatformVersion>13.0</SupportedOSPlatformVersion>
    <EnableDefaultItems>false</EnableDefaultItems>
    <EnableCodeSigning>false</EnableCodeSigning>
    <MtouchLink>None</MtouchLink>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="Program.cs" />
    <None Include="Info.plist" />
$packageReferences
  </ItemGroup>
</Project>
"@

    if ($DryRun) {
        $rows.Add([pscustomobject]@{
            Rid = $rid
            TargetFramework = $targetFramework
            Project = $projectPath
            Status = 'dry-run'
        })
        Write-Host "[dry-run] create Apple consumer project for $rid at $projectPath"
        Write-Host "[dry-run] dotnet restore $projectPath -r $rid --configfile <generated NuGet.config>"
        Write-Host "[dry-run] dotnet build $projectPath -c $Configuration -f $targetFramework -r $rid --no-restore"
        continue
    }

    New-Item -ItemType Directory -Force -Path $projectRoot | Out-Null
    Set-Content -LiteralPath $projectPath -Value $projectXml -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'Program.cs') -Value (Get-AppleConsumerProgram) -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $projectRoot 'Info.plist') -Value (Get-AppleConsumerInfoPlist -Platform $platform -BundleIdentifier $bundleIdentifier) -Encoding UTF8

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
        throw "dotnet restore failed for Apple consumer RID '$rid' with exit code $LASTEXITCODE."
    }

    & dotnet build $projectPath -c $Configuration -f $targetFramework -r $rid --no-restore
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet build failed for Apple consumer RID '$rid' with exit code $LASTEXITCODE."
    }

    $rows.Add([pscustomobject]@{
        Rid = $rid
        TargetFramework = $targetFramework
        Project = $projectPath
        Status = 'built'
    })
}

$rows | Sort-Object Rid | Format-Table -AutoSize
Write-Host "Apple consumer package build validation completed."
