#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $OutputPath,
    [Parameter(Mandatory)][string] $AndroidJarPath,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot = 'native-forks/SDL',
    [string] $JavaHome,
    [switch] $VerifyDeterministic
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-RequiredToolchainValue {
    param(
        [Parameter(Mandatory)] $Manifest,
        [Parameter(Mandatory)][string] $Name
    )

    if (-not $Manifest.PSObject.Properties.Name.Contains('toolchains') -or
        $null -eq $Manifest.toolchains -or
        -not $Manifest.toolchains.PSObject.Properties.Name.Contains($Name) -or
        [string]::IsNullOrWhiteSpace([string]$Manifest.toolchains.$Name)) {
        throw "Release manifest must declare toolchains.$Name."
    }
    return $Manifest.toolchains.$Name
}

function Invoke-Git {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string[]] $Arguments,
        [Parameter(Mandatory)][string] $Description
    )

    $output = @(& git -C $Repository @Arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        throw "$Description failed: $($output -join [Environment]::NewLine)"
    }
    return ($output -join "`n")
}

function New-DeterministicJar {
    param(
        [Parameter(Mandatory)][string] $ClassesRoot,
        [Parameter(Mandatory)][string] $Destination,
        [Parameter(Mandatory)][System.DateTimeOffset] $Timestamp,
        [Parameter(Mandatory)][System.Collections.IDictionary] $Metadata
    )

    Add-Type -AssemblyName System.IO.Compression
    $fixedTimestamp = [System.DateTimeOffset]::new(
        $Timestamp.Year,
        $Timestamp.Month,
        $Timestamp.Day,
        $Timestamp.Hour,
        $Timestamp.Minute,
        $Timestamp.Second - ($Timestamp.Second % 2),
        [System.TimeSpan]::Zero)
    $manifestBytes = [System.Text.Encoding]::ASCII.GetBytes("Manifest-Version: 1.0`r`nCreated-By: SDL3-CS Build-AndroidBridgeJar.ps1`r`n`r`n")
    $classFiles = @(Get-ChildItem -LiteralPath $ClassesRoot -Recurse -Filter '*.class' -File | Sort-Object {
        [System.IO.Path]::GetRelativePath($ClassesRoot, $_.FullName).Replace('\', '/')
    })
    if ($classFiles.Count -eq 0) {
        throw 'Android bridge compilation did not produce any class files.'
    }
    $classEntries = @($classFiles | ForEach-Object {
        [System.IO.Path]::GetRelativePath($ClassesRoot, $_.FullName).Replace('\', '/')
    })
    $metadataWithClasses = [ordered]@{}
    foreach ($metadataEntry in $Metadata.GetEnumerator()) {
        $metadataWithClasses[$metadataEntry.Key] = $metadataEntry.Value
    }
    $metadataWithClasses['classEntries'] = $classEntries
    $metadataBytes = [System.Text.UTF8Encoding]::new($false).GetBytes(($metadataWithClasses | ConvertTo-Json -Depth 16 -Compress) + "`n")

    $destinationParent = Split-Path -Parent $Destination
    if ($destinationParent) {
        New-Item -ItemType Directory -Force -Path $destinationParent | Out-Null
    }
    $stream = [System.IO.File]::Open($Destination, [System.IO.FileMode]::Create, [System.IO.FileAccess]::Write, [System.IO.FileShare]::None)
    try {
        $archive = [System.IO.Compression.ZipArchive]::new($stream, [System.IO.Compression.ZipArchiveMode]::Create, $true)
        try {
            $entries = [ordered]@{
                'META-INF/MANIFEST.MF' = $manifestBytes
                'META-INF/SDL3-CS-BRIDGE.json' = $metadataBytes
            }
            foreach ($classFile in $classFiles) {
                $entryName = [System.IO.Path]::GetRelativePath($ClassesRoot, $classFile.FullName).Replace('\', '/')
                $entries[$entryName] = [System.IO.File]::ReadAllBytes($classFile.FullName)
            }
            foreach ($entryName in @($entries.Keys | Sort-Object)) {
                $entry = $archive.CreateEntry([string]$entryName, [System.IO.Compression.CompressionLevel]::NoCompression)
                $entry.LastWriteTime = $fixedTimestamp
                $entryStream = $entry.Open()
                try {
                    [byte[]]$entryBytes = $entries[$entryName]
                    $entryStream.Write($entryBytes, 0, $entryBytes.Length)
                }
                finally {
                    $entryStream.Dispose()
                }
            }
        }
        finally {
            $archive.Dispose()
        }
    }
    finally {
        $stream.Dispose()
    }
}

$resolvedManifestPath = Resolve-ReleasePath $ManifestPath
$resolvedSourceRoot = [System.IO.Path]::GetFullPath((Resolve-Path -LiteralPath (Resolve-ReleasePath $SourceRoot) -ErrorAction Stop).Path)
$resolvedAndroidJarPath = [System.IO.Path]::GetFullPath((Resolve-Path -LiteralPath $AndroidJarPath -ErrorAction Stop).Path)
$resolvedOutputPath = [System.IO.Path]::GetFullPath((Resolve-ReleasePath $OutputPath))
$manifest = Get-ReleaseManifest -ManifestPath $resolvedManifestPath
$sdlComponents = @($manifest.components | Where-Object id -EQ 'SDL')
if ($sdlComponents.Count -ne 1) {
    throw "Release manifest must declare component 'SDL' exactly once."
}
$sdl = $sdlComponents[0]
$sourceRef = [string]$sdl.sourceRef
$nativeVersion = [string]$sdl.nativeVersion
if ($sourceRef -notmatch '^[0-9a-f]{40}$') {
    throw "SDL sourceRef must be a full 40-character lowercase commit SHA: $sourceRef"
}
if ($nativeVersion -notmatch '^\d+\.\d+\.\d+$') {
    throw "SDL nativeVersion must be a three-part numeric version: $nativeVersion"
}
$compileSdkVersion = [int](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidCompileSdkVersion')
$expectedAndroidJarHash = ([string](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidPlatformJarSha256')).ToLowerInvariant()
$javaRelease = [int](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidBridgeJavaRelease')
$expectedJavacVersion = [string](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidBridgeJavacVersion')
$actualAndroidJarHash = (Get-FileHash -LiteralPath $resolvedAndroidJarPath -Algorithm SHA256).Hash.ToLowerInvariant()
if ($actualAndroidJarHash -ne $expectedAndroidJarHash) {
    throw "Android platform JAR SHA-256 $actualAndroidJarHash does not match manifest hash $expectedAndroidJarHash."
}

$null = Invoke-Git -Repository $resolvedSourceRoot -Arguments @('cat-file', '-e', "$sourceRef`^{commit}") -Description 'SDL sourceRef lookup'
$sourcePath = 'android-project/app/src/main/java/org/libsdl/app'
$sourceListText = Invoke-Git -Repository $resolvedSourceRoot -Arguments @('ls-tree', '-r', '--name-only', $sourceRef, '--', $sourcePath) -Description 'Exact Android Java source enumeration'
$sourceFilesInGit = @($sourceListText -split "`n" | Where-Object { $_ -match '\.java$' } | Sort-Object -Unique)
if ($sourceFilesInGit.Count -eq 0) {
    throw "Exact SDL sourceRef contains no Java bridge sources under $sourcePath."
}
$buildScriptText = Invoke-Git -Repository $resolvedSourceRoot -Arguments @('show', '--no-textconv', "$sourceRef`:android-project/app/build.gradle") -Description 'Exact Android build.gradle read'
$compileSdkMatch = [regex]::Match($buildScriptText, '(?m)^\s*compileSdkVersion\s+(\d+)\s*$')
if (-not $compileSdkMatch.Success -or [int]$compileSdkMatch.Groups[1].Value -ne $compileSdkVersion) {
    throw "Exact SDL sourceRef compileSdkVersion does not match manifest Android compile SDK $compileSdkVersion."
}
$commitTimestampText = Invoke-Git -Repository $resolvedSourceRoot -Arguments @('show', '-s', '--format=%cI', $sourceRef) -Description 'Exact SDL commit timestamp read'
$commitTimestamp = ([System.DateTimeOffset]::Parse($commitTimestampText.Trim(), [System.Globalization.CultureInfo]::InvariantCulture)).ToUniversalTime()
$commitTimestampString = $commitTimestamp.ToString('yyyy-MM-ddTHH:mm:ssZ', [System.Globalization.CultureInfo]::InvariantCulture)

$javacExecutable = if ($IsWindows) { 'javac.exe' } else { 'javac' }
$javacPath = if ($JavaHome) {
    Join-Path ([System.IO.Path]::GetFullPath($JavaHome)) "bin/$javacExecutable"
}
elseif ($env:JAVA_HOME) {
    $candidate = Join-Path ([System.IO.Path]::GetFullPath($env:JAVA_HOME)) "bin/$javacExecutable"
    if (Test-Path -LiteralPath $candidate -PathType Leaf) { $candidate } else { (Get-Command javac -ErrorAction Stop).Source }
}
else {
    (Get-Command javac -ErrorAction Stop).Source
}
if (-not (Test-Path -LiteralPath $javacPath -PathType Leaf)) {
    throw "javac was not found: $javacPath"
}
$javacOutput = @(& $javacPath -version 2>&1)
if ($LASTEXITCODE -ne 0 -or ($javacOutput -join ' ') -notmatch '^javac\s+(.+)$') {
    throw "Unable to determine javac version: $($javacOutput -join ' ')"
}
$actualJavacVersion = $Matches[1].Trim()
if ($actualJavacVersion -ne $expectedJavacVersion) {
    throw "javac version $actualJavacVersion does not match manifest version $expectedJavacVersion."
}

$metadata = [ordered]@{
    schemaVersion = 1
    component = 'SDL'
    sourceRef = $sourceRef
    nativeVersion = $nativeVersion
    sourcePath = $sourcePath
    sourceCommitTimestamp = $commitTimestampString
    compileSdkVersion = $compileSdkVersion
    androidPlatformJarSha256 = $expectedAndroidJarHash
    javaRelease = $javaRelease
    javacVersion = $actualJavacVersion
    classFileMajor = $javaRelease + 44
}
$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-android-bridge-build-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary Android bridge build path: $tempRoot"
}
New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null

try {
    $sourceArchive = Join-Path $tempRoot 'source.zip'
    $archiveOutput = @(& git -C $resolvedSourceRoot archive --format=zip --output=$sourceArchive $sourceRef $sourcePath 2>&1)
    if ($LASTEXITCODE -ne 0) {
        throw "Exact Android Java source export failed: $($archiveOutput -join [Environment]::NewLine)"
    }
    $sourceExportRoot = Join-Path $tempRoot 'source'
    Expand-Archive -LiteralPath $sourceArchive -DestinationPath $sourceExportRoot
    $exportedSourceRoot = Join-Path $sourceExportRoot $sourcePath
    $sourceFiles = @(Get-ChildItem -LiteralPath $exportedSourceRoot -Recurse -Filter '*.java' -File | Sort-Object {
        [System.IO.Path]::GetRelativePath($exportedSourceRoot, $_.FullName).Replace('\', '/')
    })
    if ($sourceFiles.Count -ne $sourceFilesInGit.Count) {
        throw "Expected $($sourceFilesInGit.Count) exact Java sources, exported $($sourceFiles.Count)."
    }
    $exportedSourceFilesInGit = @($sourceFiles | ForEach-Object {
        "$sourcePath/$([System.IO.Path]::GetRelativePath($exportedSourceRoot, $_.FullName).Replace('\', '/'))"
    })
    for ($sourceIndex = 0; $sourceIndex -lt $sourceFilesInGit.Count; $sourceIndex++) {
        if ($sourceFilesInGit[$sourceIndex] -cne $exportedSourceFilesInGit[$sourceIndex]) {
            throw "Exact Java source export mismatch at index $sourceIndex."
        }
    }

    $candidateCount = if ($VerifyDeterministic) { 2 } else { 1 }
    $candidatePaths = @()
    for ($candidateIndex = 1; $candidateIndex -le $candidateCount; $candidateIndex++) {
        $classesRoot = Join-Path $tempRoot "classes-$candidateIndex"
        New-Item -ItemType Directory -Force -Path $classesRoot | Out-Null
        $javacArguments = @(
            '-encoding', 'UTF-8',
            '--release', [string]$javaRelease,
            '-classpath', $resolvedAndroidJarPath,
            '-d', $classesRoot
        ) + @($sourceFiles | ForEach-Object FullName)
        $compileOutput = @(& $javacPath @javacArguments 2>&1)
        if ($LASTEXITCODE -ne 0) {
            throw "Android bridge Java compilation failed: $($compileOutput -join [Environment]::NewLine)"
        }

        $candidatePath = Join-Path $tempRoot "SDL3Bridge-$candidateIndex.jar"
        New-DeterministicJar -ClassesRoot $classesRoot -Destination $candidatePath -Timestamp $commitTimestamp -Metadata $metadata
        & (Join-Path $PSScriptRoot 'Test-AndroidBridgeJar.ps1') -JarPath $candidatePath -ManifestPath $resolvedManifestPath -SourceRoot $resolvedSourceRoot -SkipPinnedJarHashValidation
        $candidatePaths += $candidatePath
    }

    if ($VerifyDeterministic) {
        $firstHash = (Get-FileHash -LiteralPath $candidatePaths[0] -Algorithm SHA256).Hash.ToLowerInvariant()
        $secondHash = (Get-FileHash -LiteralPath $candidatePaths[1] -Algorithm SHA256).Hash.ToLowerInvariant()
        if ($firstHash -ne $secondHash) {
            throw "Android bridge build is not deterministic: $firstHash != $secondHash"
        }
    }

    $outputParent = Split-Path -Parent $resolvedOutputPath
    if ($outputParent) {
        New-Item -ItemType Directory -Force -Path $outputParent | Out-Null
    }
    [System.IO.File]::Copy($candidatePaths[0], $resolvedOutputPath, $true)
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-android-bridge-build-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary Android bridge build path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

$outputHash = (Get-FileHash -LiteralPath $resolvedOutputPath -Algorithm SHA256).Hash.ToLowerInvariant()
Write-Host "Built deterministic Android bridge JAR: $resolvedOutputPath (SHA-256 $outputHash)"
