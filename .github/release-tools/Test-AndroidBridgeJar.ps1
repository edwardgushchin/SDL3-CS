#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $JarPath = 'SDL3-CS.NativePackages/SDL3-CS.Android/SDL3Bridge.jar',
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot,
    [switch] $SkipPinnedJarHashValidation
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

if (-not ('Sdl3Cs.AndroidBridgeClassInfo' -as [type])) {
    Add-Type -TypeDefinition @'
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sdl3Cs
{
    public sealed class AndroidBridgeClassInfo
    {
        public int MajorVersion { get; set; }
        public Dictionary<string, int> ConstantIntegers { get; } = new Dictionary<string, int>(StringComparer.Ordinal);
        public List<AndroidBridgeMethodInfo> Methods { get; } = new List<AndroidBridgeMethodInfo>();
    }

    public sealed class AndroidBridgeMethodInfo
    {
        public int AccessFlags { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Descriptor { get; set; } = string.Empty;
    }

    public static class AndroidBridgeClassReader
    {
        private static int ReadU1(BinaryReader reader)
        {
            int value = reader.BaseStream.ReadByte();
            if (value < 0) throw new EndOfStreamException();
            return value;
        }

        private static int ReadU2(BinaryReader reader)
        {
            return (ReadU1(reader) << 8) | ReadU1(reader);
        }

        private static uint ReadU4(BinaryReader reader)
        {
            return ((uint)ReadU1(reader) << 24) |
                   ((uint)ReadU1(reader) << 16) |
                   ((uint)ReadU1(reader) << 8) |
                   (uint)ReadU1(reader);
        }

        private static byte[] ReadExact(BinaryReader reader, int count)
        {
            byte[] bytes = reader.ReadBytes(count);
            if (bytes.Length != count) throw new EndOfStreamException();
            return bytes;
        }

        private static void SkipExact(BinaryReader reader, uint count)
        {
            if (count > int.MaxValue) throw new InvalidDataException("Class attribute is too large.");
            ReadExact(reader, (int)count);
        }

        public static AndroidBridgeClassInfo Read(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            using (var stream = new MemoryStream(bytes, false))
            using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
            {
                if (ReadU4(reader) != 0xCAFEBABE) throw new InvalidDataException("Java class magic is invalid.");
                ReadU2(reader);
                var result = new AndroidBridgeClassInfo { MajorVersion = ReadU2(reader) };
                int poolCount = ReadU2(reader);
                var pool = new object[poolCount];

                for (int index = 1; index < poolCount; index++)
                {
                    int tag = ReadU1(reader);
                    switch (tag)
                    {
                        case 1:
                            int length = ReadU2(reader);
                            pool[index] = Encoding.UTF8.GetString(ReadExact(reader, length));
                            break;
                        case 3:
                            pool[index] = unchecked((int)ReadU4(reader));
                            break;
                        case 4:
                            ReadU4(reader);
                            break;
                        case 5:
                        case 6:
                            ReadU4(reader);
                            ReadU4(reader);
                            index++;
                            break;
                        case 7:
                        case 8:
                        case 16:
                        case 19:
                        case 20:
                            ReadU2(reader);
                            break;
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 17:
                        case 18:
                            ReadU2(reader);
                            ReadU2(reader);
                            break;
                        case 15:
                            ReadU1(reader);
                            ReadU2(reader);
                            break;
                        default:
                            throw new InvalidDataException("Unsupported Java constant-pool tag: " + tag);
                    }
                }

                ReadU2(reader);
                ReadU2(reader);
                ReadU2(reader);
                int interfaceCount = ReadU2(reader);
                for (int index = 0; index < interfaceCount; index++) ReadU2(reader);

                int fieldCount = ReadU2(reader);
                for (int fieldIndex = 0; fieldIndex < fieldCount; fieldIndex++)
                {
                    ReadU2(reader);
                    int nameIndex = ReadU2(reader);
                    int descriptorIndex = ReadU2(reader);
                    string name = pool[nameIndex] as string ?? throw new InvalidDataException("Field name is invalid.");
                    string descriptor = pool[descriptorIndex] as string ?? throw new InvalidDataException("Field descriptor is invalid.");
                    int attributeCount = ReadU2(reader);
                    for (int attributeIndex = 0; attributeIndex < attributeCount; attributeIndex++)
                    {
                        int attributeNameIndex = ReadU2(reader);
                        string attributeName = pool[attributeNameIndex] as string ?? throw new InvalidDataException("Field attribute name is invalid.");
                        uint attributeLength = ReadU4(reader);
                        if (attributeName == "ConstantValue")
                        {
                            if (attributeLength != 2) throw new InvalidDataException("ConstantValue attribute length is invalid.");
                            int constantIndex = ReadU2(reader);
                            if (descriptor == "I" && pool[constantIndex] is int value) result.ConstantIntegers[name] = value;
                        }
                        else
                        {
                            SkipExact(reader, attributeLength);
                        }
                    }
                }

                int methodCount = ReadU2(reader);
                for (int methodIndex = 0; methodIndex < methodCount; methodIndex++)
                {
                    int accessFlags = ReadU2(reader);
                    int nameIndex = ReadU2(reader);
                    int descriptorIndex = ReadU2(reader);
                    string name = pool[nameIndex] as string ?? throw new InvalidDataException("Method name is invalid.");
                    string descriptor = pool[descriptorIndex] as string ?? throw new InvalidDataException("Method descriptor is invalid.");
                    result.Methods.Add(new AndroidBridgeMethodInfo
                    {
                        AccessFlags = accessFlags,
                        Name = name,
                        Descriptor = descriptor
                    });
                    int attributeCount = ReadU2(reader);
                    for (int attributeIndex = 0; attributeIndex < attributeCount; attributeIndex++)
                    {
                        ReadU2(reader);
                        SkipExact(reader, ReadU4(reader));
                    }
                }

                int classAttributeCount = ReadU2(reader);
                for (int attributeIndex = 0; attributeIndex < classAttributeCount; attributeIndex++)
                {
                    ReadU2(reader);
                    SkipExact(reader, ReadU4(reader));
                }
                if (stream.Position != stream.Length)
                {
                    throw new InvalidDataException("Java class contains trailing data.");
                }

                return result;
            }
        }
    }
}
'@
}

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

function Get-GitText {
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

$resolvedJarPath = Resolve-ReleasePath $JarPath
$resolvedManifestPath = Resolve-ReleasePath $ManifestPath
if (-not (Test-Path -LiteralPath $resolvedJarPath -PathType Leaf)) {
    throw "Android bridge JAR was not found: $resolvedJarPath"
}

$manifest = Get-ReleaseManifest -ManifestPath $resolvedManifestPath
$sdlComponents = @($manifest.components | Where-Object id -EQ 'SDL')
if ($sdlComponents.Count -ne 1) {
    throw "Release manifest must declare component 'SDL' exactly once."
}
$sdl = $sdlComponents[0]
$expectedSourceRef = [string]$sdl.sourceRef
$expectedVersion = [string]$sdl.nativeVersion
if ($expectedSourceRef -notmatch '^[0-9a-f]{40}$') {
    throw "SDL sourceRef must be a full 40-character lowercase commit SHA: $expectedSourceRef"
}
if ($expectedVersion -notmatch '^(\d+)\.(\d+)\.(\d+)$') {
    throw "SDL nativeVersion must be a three-part numeric version: $expectedVersion"
}
$expectedVersionParts = @([int]$Matches[1], [int]$Matches[2], [int]$Matches[3])
$expectedCompileSdk = [int](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidCompileSdkVersion')
$expectedPlatformHash = ([string](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidPlatformJarSha256')).ToLowerInvariant()
$expectedBridgeHash = ([string](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidBridgeJarSha256')).ToLowerInvariant()
$expectedJavaRelease = [int](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidBridgeJavaRelease')
$expectedJavacVersion = [string](Get-RequiredToolchainValue -Manifest $manifest -Name 'androidBridgeJavacVersion')
$expectedClassMajor = $expectedJavaRelease + 44
$expectedSourcePath = 'android-project/app/src/main/java/org/libsdl/app'
$actualBridgeHash = (Get-FileHash -LiteralPath $resolvedJarPath -Algorithm SHA256).Hash.ToLowerInvariant()
if ($expectedBridgeHash -notmatch '^[0-9a-f]{64}$') {
    throw "Android bridge JAR manifest hash is invalid: $expectedBridgeHash"
}
if (-not $SkipPinnedJarHashValidation -and $actualBridgeHash -ne $expectedBridgeHash) {
    throw "Android bridge JAR SHA-256 $actualBridgeHash does not match manifest hash $expectedBridgeHash."
}

Add-Type -AssemblyName System.IO.Compression
$archive = [System.IO.Compression.ZipFile]::OpenRead($resolvedJarPath)
try {
    $maximumEntryCount = 512
    $maximumEntryLength = 4MB
    $maximumTotalLength = 16MB
    if ($archive.Entries.Count -gt $maximumEntryCount) {
        throw "Android bridge JAR contains $($archive.Entries.Count) entries; maximum is $maximumEntryCount."
    }
    $entryNames = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    $entryNamesIgnoreCase = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    [long]$totalLength = 0
    foreach ($archiveEntry in $archive.Entries) {
        $entryName = [string]$archiveEntry.FullName
        if ([string]::IsNullOrWhiteSpace($entryName) -or
            $entryName.StartsWith('/', [System.StringComparison]::Ordinal) -or
            $entryName.StartsWith('\', [System.StringComparison]::Ordinal) -or
            $entryName.Contains('\', [System.StringComparison]::Ordinal) -or
            $entryName.Contains(':', [System.StringComparison]::Ordinal)) {
            throw "Android bridge JAR contains a noncanonical entry name: '$entryName'."
        }
        $segments = @($entryName -split '/', -1)
        if ($segments.Count -eq 0 -or @($segments | Where-Object { $_ -in @('', '.', '..') }).Count -gt 0) {
            throw "Android bridge JAR contains a noncanonical entry name: '$entryName'."
        }
        if (-not $entryNames.Add($entryName) -or -not $entryNamesIgnoreCase.Add($entryName)) {
            throw "Android bridge JAR contains a duplicate entry name: '$entryName'."
        }
        if ($entryName -cne 'META-INF/MANIFEST.MF' -and
            $entryName -cne 'META-INF/SDL3-CS-BRIDGE.json' -and
            $entryName -notmatch '^org/libsdl/app/(?:[A-Za-z_$][A-Za-z0-9_$]*/)*[A-Za-z_$][A-Za-z0-9_$]*\.class$') {
            throw "Android bridge JAR contains an unexpected entry: '$entryName'."
        }
        if ([long]$archiveEntry.Length -gt $maximumEntryLength) {
            throw "Android bridge JAR entry '$entryName' exceeds the $maximumEntryLength-byte limit."
        }
        $totalLength += [long]$archiveEntry.Length
        if ($totalLength -gt $maximumTotalLength) {
            throw "Android bridge JAR exceeds the $maximumTotalLength-byte uncompressed-size limit."
        }
    }

    $activityEntries = @($archive.Entries | Where-Object FullName -CEQ 'org/libsdl/app/SDLActivity.class')
    if ($activityEntries.Count -ne 1) {
        throw "Android bridge JAR must contain org/libsdl/app/SDLActivity.class exactly once."
    }
    $metadataEntries = @($archive.Entries | Where-Object FullName -CEQ 'META-INF/SDL3-CS-BRIDGE.json')
    if ($metadataEntries.Count -ne 1) {
        throw "Android bridge JAR must contain META-INF/SDL3-CS-BRIDGE.json exactly once."
    }

    $activityStream = $activityEntries[0].Open()
    try {
        $activityMemory = [System.IO.MemoryStream]::new()
        try {
            $activityStream.CopyTo($activityMemory)
            $classInfo = [Sdl3Cs.AndroidBridgeClassReader]::Read($activityMemory.ToArray())
        }
        finally {
            $activityMemory.Dispose()
        }
    }
    finally {
        $activityStream.Dispose()
    }

    if ($classInfo.MajorVersion -ne $expectedClassMajor) {
        throw "SDLActivity.class major version $($classInfo.MajorVersion) does not match Java release $expectedJavaRelease (expected $expectedClassMajor)."
    }
    $constantNames = @('SDL_MAJOR_VERSION', 'SDL_MINOR_VERSION', 'SDL_MICRO_VERSION')
    for ($index = 0; $index -lt $constantNames.Count; $index++) {
        $name = $constantNames[$index]
        if (-not $classInfo.ConstantIntegers.ContainsKey($name)) {
            throw "SDLActivity.class is missing integer ConstantValue '$name'."
        }
        if ($classInfo.ConstantIntegers[$name] -ne $expectedVersionParts[$index]) {
            $actualVersion = @($constantNames | ForEach-Object {
                if ($classInfo.ConstantIntegers.ContainsKey($_)) { $classInfo.ConstantIntegers[$_] } else { '?' }
            }) -join '.'
            throw "SDLActivity.class version $actualVersion does not match SDL nativeVersion $expectedVersion."
        }
    }
    $nativeGetVersionMethods = @($classInfo.Methods | Where-Object Name -CEQ 'nativeGetVersion')
    if ($nativeGetVersionMethods.Count -ne 1 -or
        [string]$nativeGetVersionMethods[0].Descriptor -cne '()Ljava/lang/String;' -or
        [int]$nativeGetVersionMethods[0].AccessFlags -ne 0x0109) {
        throw 'SDLActivity.class must contain exactly public static native nativeGetVersion()Ljava/lang/String;.'
    }

    $metadataStream = $metadataEntries[0].Open()
    try {
        $reader = [System.IO.StreamReader]::new($metadataStream, [System.Text.UTF8Encoding]::new($false), $true, 1024, $true)
        try {
            $metadata = $reader.ReadToEnd() | ConvertFrom-Json -Depth 16
        }
        finally {
            $reader.Dispose()
        }
    }
    finally {
        $metadataStream.Dispose()
    }

    $expectedMetadata = [ordered]@{
        schemaVersion = 1
        component = 'SDL'
        sourceRef = $expectedSourceRef
        nativeVersion = $expectedVersion
        sourcePath = $expectedSourcePath
        compileSdkVersion = $expectedCompileSdk
        androidPlatformJarSha256 = $expectedPlatformHash
        javaRelease = $expectedJavaRelease
        javacVersion = $expectedJavacVersion
        classFileMajor = $expectedClassMajor
    }
    foreach ($entry in $expectedMetadata.GetEnumerator()) {
        if (-not $metadata.PSObject.Properties.Name.Contains($entry.Key) -or
            [string]$metadata.($entry.Key) -cne [string]$entry.Value) {
            throw "Android bridge metadata '$($entry.Key)' must be '$($entry.Value)'."
        }
    }

    if (-not $metadata.PSObject.Properties.Name.Contains('classEntries')) {
        throw 'Android bridge metadata must declare classEntries.'
    }
    $actualClassEntries = @($archive.Entries.FullName | Where-Object { $_ -match '\.class$' } | Sort-Object)
    $declaredClassEntries = @($metadata.classEntries | ForEach-Object { [string]$_ })
    if ($declaredClassEntries.Count -ne $actualClassEntries.Count) {
        throw "Android bridge metadata declares $($declaredClassEntries.Count) class entries, but the JAR contains $($actualClassEntries.Count)."
    }
    for ($index = 0; $index -lt $actualClassEntries.Count; $index++) {
        if ($declaredClassEntries[$index] -cne $actualClassEntries[$index]) {
            throw "Android bridge metadata classEntries do not match the canonical JAR contents at index $index."
        }
    }

    if ($SourceRoot) {
        $resolvedSourceRoot = [System.IO.Path]::GetFullPath((Resolve-Path -LiteralPath $SourceRoot -ErrorAction Stop).Path)
        $null = Get-GitText -Repository $resolvedSourceRoot -Arguments @('cat-file', '-e', "$expectedSourceRef`^{commit}") -Description 'SDL sourceRef lookup'
        $sourceObject = "$expectedSourceRef`:$expectedSourcePath/SDLActivity.java"
        $activitySource = Get-GitText -Repository $resolvedSourceRoot -Arguments @('show', '--no-textconv', $sourceObject) -Description 'Exact SDLActivity.java read'
        $sourceConstants = @{}
        foreach ($name in $constantNames) {
            $match = [regex]::Match($activitySource, "(?m)^\s*private\s+static\s+final\s+int\s+$name\s*=\s*(\d+)\s*;")
            if (-not $match.Success) {
                throw "Exact SDLActivity.java is missing version constant '$name'."
            }
            $sourceConstants[$name] = [int]$match.Groups[1].Value
        }
        $sourceVersion = @($constantNames | ForEach-Object { $sourceConstants[$_] }) -join '.'
        if ($sourceVersion -ne $expectedVersion) {
            throw "Exact SDL sourceRef declares Java bridge version $sourceVersion, not manifest version $expectedVersion."
        }

        $buildScriptObject = "$expectedSourceRef`:android-project/app/build.gradle"
        $buildScript = Get-GitText -Repository $resolvedSourceRoot -Arguments @('show', '--no-textconv', $buildScriptObject) -Description 'Exact Android build.gradle read'
        $compileSdkMatch = [regex]::Match($buildScript, '(?m)^\s*compileSdkVersion\s+(\d+)\s*$')
        if (-not $compileSdkMatch.Success -or [int]$compileSdkMatch.Groups[1].Value -ne $expectedCompileSdk) {
            throw "Exact SDL sourceRef compileSdkVersion does not match manifest Android compile SDK $expectedCompileSdk."
        }

        $sourceListText = Get-GitText -Repository $resolvedSourceRoot -Arguments @('ls-tree', '-r', '--name-only', $expectedSourceRef, '--', $expectedSourcePath) -Description 'Exact Android Java source enumeration'
        $sourceFiles = @($sourceListText -split "`n" | Where-Object { $_ -match '\.java$' } | Sort-Object -Unique)
        if ($sourceFiles.Count -eq 0) {
            throw "Exact SDL sourceRef contains no Android bridge Java files under $expectedSourcePath."
        }
        foreach ($sourceFile in $sourceFiles) {
            $sourceRelativePath = $sourceFile.Substring($expectedSourcePath.Length + 1)
            $expectedClassStem = "org/libsdl/app/$($sourceRelativePath.Substring(0, $sourceRelativePath.Length - '.java'.Length))"
            $expectedClassEntry = "$expectedClassStem.class"
            if (@($archive.Entries | Where-Object FullName -CEQ $expectedClassEntry).Count -ne 1) {
                throw "Android bridge JAR is missing top-level class for exact source '$sourceFile': $expectedClassEntry"
            }
        }

        $commitText = Get-GitText -Repository $resolvedSourceRoot -Arguments @('show', '-s', '--format=%cI', $expectedSourceRef) -Description 'Exact SDL commit timestamp read'
        $expectedTimestamp = ([System.DateTimeOffset]::Parse($commitText.Trim(), [System.Globalization.CultureInfo]::InvariantCulture)).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ', [System.Globalization.CultureInfo]::InvariantCulture)
        if (-not $metadata.PSObject.Properties.Name.Contains('sourceCommitTimestamp')) {
            throw "Android bridge metadata sourceCommitTimestamp must be '$expectedTimestamp'."
        }
        try {
            $actualTimestamp = ([System.DateTimeOffset]$metadata.sourceCommitTimestamp).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ', [System.Globalization.CultureInfo]::InvariantCulture)
        }
        catch {
            throw "Android bridge metadata sourceCommitTimestamp is invalid: $($metadata.sourceCommitTimestamp)"
        }
        if ($actualTimestamp -cne $expectedTimestamp) {
            throw "Android bridge metadata sourceCommitTimestamp must be '$expectedTimestamp'."
        }
    }
}
finally {
    $archive.Dispose()
}

$hash = (Get-FileHash -LiteralPath $resolvedJarPath -Algorithm SHA256).Hash.ToLowerInvariant()
Write-Host "Android bridge JAR is valid: SDL $expectedVersion, source $expectedSourceRef, SHA-256 $hash"
