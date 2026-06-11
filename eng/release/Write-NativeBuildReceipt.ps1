#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $Component,

    [Parameter(Mandatory)]
    [string] $Rid,

    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $Configuration,
    [string] $ReceiptPath
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-SourceReference {
    param(
        [Parameter(Mandatory)]
        [object] $Manifest,

        [Parameter(Mandatory)]
        [string] $ComponentId
    )

    $componentInfo = Get-ReleaseComponent -Manifest $Manifest -Component $ComponentId
    $repositoryPath = Resolve-ReleasePath (Join-Path $Manifest.sourceRoot $componentInfo.sourceFolder)
    $branch = Invoke-ReleaseGitValue -RepositoryPath $repositoryPath -Arguments @('branch', '--show-current')
    $head = Invoke-ReleaseGitValue -RepositoryPath $repositoryPath -Arguments @('rev-parse', 'HEAD')
    $originUrl = Invoke-ReleaseGitValue -RepositoryPath $repositoryPath -Arguments @('config', '--get', 'remote.origin.url')

    return [pscustomobject]@{
        Component = $componentInfo.id
        SourceFolder = $componentInfo.sourceFolder
        Repository = $componentInfo.repository
        UpstreamRepository = if ($componentInfo.PSObject.Properties.Name.Contains('upstreamRepository')) { $componentInfo.upstreamRepository } else { $null }
        Origin = $originUrl
        Branch = $branch
        Head = $head
        ShortHead = $head.Substring(0, 12)
        Dirty = Get-ReleaseGitDirtyCount -RepositoryPath $repositoryPath
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $Component
$ridInfo = Get-ReleaseRid -Manifest $manifest -Rid $Rid
if (-not $Configuration) {
    $Configuration = $manifest.configuration
}
if (-not $ReceiptPath) {
    $ReceiptPath = Get-ReleaseNativeReceiptPath -Manifest $manifest -Component $Component -Rid $Rid
}

$packageProject = Resolve-ReleasePath $componentInfo.packageProject
$packageRoot = Split-Path -Parent $packageProject
$ridRoot = Join-Path $packageRoot "lib\$Rid"
$artifactMap = [ordered]@{}
$installCollection = @(& (Join-Path $PSScriptRoot 'Test-NativeCollectedArtifacts.ps1') -Components $Component -Rids $Rid -ManifestPath $ManifestPath -PassThru)

foreach ($installArtifact in $installCollection) {
    if ($installArtifact.Disposition -ne 'collected') {
        continue
    }
    if ([string]::IsNullOrWhiteSpace($installArtifact.PackageRelativePath)) {
        throw "Collected install artifact has no package relative path for receipt $Component/$Rid`: $($installArtifact.InstallRelativePath)"
    }

    $artifactPath = Join-Path $packageRoot $installArtifact.PackageRelativePath.Replace('/', [string][System.IO.Path]::DirectorySeparatorChar)
    if (-not (Test-Path -LiteralPath $artifactPath -PathType Leaf)) {
        throw "Collected package artifact was not found for receipt $Component/$Rid`: $($installArtifact.PackageRelativePath)"
    }

    $file = Get-Item -LiteralPath $artifactPath
    $artifactMap[$file.FullName] = $file
}

if ($artifactMap.Count -eq 0) {
    throw "No package artifacts found for receipt $Component/$Rid in $ridRoot."
}

$sourceComponentIds = New-Object System.Collections.Generic.List[string]
foreach ($dependency in @($componentInfo.dependencies)) {
    if (-not $sourceComponentIds.Contains($dependency)) {
        $sourceComponentIds.Add($dependency)
    }
}
if (-not $sourceComponentIds.Contains($componentInfo.id)) {
    $sourceComponentIds.Add($componentInfo.id)
}

$sourceReferences = @($sourceComponentIds | ForEach-Object { Get-SourceReference -Manifest $manifest -ComponentId $_ })
$artifacts = @($artifactMap.Values | Sort-Object Name | ForEach-Object {
    [pscustomobject]@{
        Name = $_.Name
        RelativePath = [System.IO.Path]::GetRelativePath($packageRoot, $_.FullName).Replace('\', '/')
        Length = $_.Length
        Sha256 = (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash.ToLowerInvariant()
    }
})

$toolchain = [ordered]@{
    HostOs = Get-ReleaseHostOs
    HostArch = Get-ReleaseHostArch
    CMake = Get-ReleaseCMakePath
    DotNet = Get-ReleaseToolPath -Name 'dotnet'
    Git = Get-ReleaseToolPath -Name 'git'
}
if ($ridInfo.os -eq 'windows') {
    $toolchain.VisualStudio = Get-ReleaseVisualStudioPath
    $toolchain.VisualStudioCompiler = Get-ReleaseVisualStudioCompilerPath -RidInfo $ridInfo
    $toolchain.NASM = Get-ReleaseNasmPath
    $toolchain.Perl = Get-ReleasePerlPath
}
else {
    $unixBuildTools = Get-ReleaseUnixBuildTools
    if ($unixBuildTools) {
        $toolchain.CCompiler = $unixBuildTools.CCompiler
        $toolchain.CxxCompiler = $unixBuildTools.CxxCompiler
        $toolchain.Make = $unixBuildTools.Make
        $toolchain.Shell = $unixBuildTools.Shell
        $toolchain.PkgConfig = $unixBuildTools.PkgConfig
        $toolchain.Bison = $unixBuildTools.Bison
        $toolchain.NASM = $unixBuildTools.NASM
        $toolchain.Patchelf = $unixBuildTools.Patchelf
        $toolchain.InstallNameTool = $unixBuildTools.InstallNameTool
    }
    if ($ridInfo.os -eq 'android') {
        $toolchain.AndroidNdk = Get-ReleaseAndroidNdkPath
    }
}

$receipt = [pscustomobject]@{
    SchemaVersion = 2
    CreatedAtUtc = [DateTime]::UtcNow.ToString('o')
    Component = $componentInfo.id
    PackageId = $componentInfo.packageId
    Rid = $ridInfo.rid
    Configuration = $Configuration
    SourceReferences = $sourceReferences
    Toolchain = [pscustomobject]$toolchain
    PackageRoot = $packageRoot
    PackageRidRoot = $ridRoot
    InstallCollection = $installCollection
    Artifacts = $artifacts
}

$receiptDir = Split-Path -Parent $ReceiptPath
New-Item -ItemType Directory -Force -Path $receiptDir | Out-Null
$receipt | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath $ReceiptPath -Encoding UTF8

Write-Host "Wrote native build receipt: $ReceiptPath"
