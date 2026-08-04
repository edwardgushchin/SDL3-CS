#requires -Version 7.0
[CmdletBinding()]
param()

$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$validator = Join-Path $PSScriptRoot 'Test-AndroidBridgeJar.ps1'
$manifestPath = Join-Path $PSScriptRoot 'release-manifest.json'
$jarPath = Join-Path $repoRoot 'SDL3-CS.NativePackages/SDL3-CS.Android/SDL3Bridge.jar'

foreach ($requiredPath in @($validator, $manifestPath, $jarPath)) {
    if (-not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) {
        throw "Android bridge JAR test dependency was not found: $requiredPath"
    }
}

function Assert-ValidationFails {
    param(
        [Parameter(Mandatory)][string] $Description,
        [Parameter(Mandatory)][scriptblock] $Action
    )

    $failed = $false
    try {
        & $Action
    }
    catch {
        $failed = $true
    }

    if (-not $failed) {
        throw "Expected Android bridge JAR validation to fail: $Description"
    }
}

function New-ZipFixture {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][System.Collections.IDictionary] $Entries
    )

    Add-Type -AssemblyName System.IO.Compression
    $stream = [System.IO.File]::Open($Path, [System.IO.FileMode]::CreateNew, [System.IO.FileAccess]::Write, [System.IO.FileShare]::None)
    try {
        $archive = [System.IO.Compression.ZipArchive]::new($stream, [System.IO.Compression.ZipArchiveMode]::Create, $true)
        try {
            foreach ($entryName in $Entries.Keys) {
                $entry = $archive.CreateEntry([string]$entryName, [System.IO.Compression.CompressionLevel]::NoCompression)
                $entry.LastWriteTime = [System.DateTimeOffset]::new(1980, 1, 1, 0, 0, 0, [System.TimeSpan]::Zero)
                $entryStream = $entry.Open()
                try {
                    [byte[]]$bytes = $Entries[$entryName]
                    $entryStream.Write($bytes, 0, $bytes.Length)
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

function New-MutatedJar {
    param(
        [Parameter(Mandatory)][string] $SourcePath,
        [Parameter(Mandatory)][string] $DestinationPath,
        [scriptblock] $TransformEntry,
        [object[]] $ExtraEntries = @()
    )

    Add-Type -AssemblyName System.IO.Compression
    $sourceArchive = [System.IO.Compression.ZipFile]::OpenRead($SourcePath)
    $destinationStream = [System.IO.File]::Open($DestinationPath, [System.IO.FileMode]::CreateNew, [System.IO.FileAccess]::Write, [System.IO.FileShare]::None)
    try {
        $destinationArchive = [System.IO.Compression.ZipArchive]::new($destinationStream, [System.IO.Compression.ZipArchiveMode]::Create, $true)
        try {
            foreach ($sourceEntry in $sourceArchive.Entries) {
                $sourceStream = $sourceEntry.Open()
                try {
                    $memory = [System.IO.MemoryStream]::new()
                    try {
                        $sourceStream.CopyTo($memory)
                        [byte[]]$bytes = $memory.ToArray()
                    }
                    finally {
                        $memory.Dispose()
                    }
                }
                finally {
                    $sourceStream.Dispose()
                }
                if ($TransformEntry) {
                    [byte[]]$bytes = & $TransformEntry ([string]$sourceEntry.FullName) $bytes
                }
                $entry = $destinationArchive.CreateEntry($sourceEntry.FullName, [System.IO.Compression.CompressionLevel]::NoCompression)
                $entry.LastWriteTime = $sourceEntry.LastWriteTime
                $entryStream = $entry.Open()
                try {
                    $entryStream.Write($bytes, 0, $bytes.Length)
                }
                finally {
                    $entryStream.Dispose()
                }
            }
            foreach ($extraEntry in $ExtraEntries) {
                $entry = $destinationArchive.CreateEntry([string]$extraEntry.Name, [System.IO.Compression.CompressionLevel]::NoCompression)
                $entry.LastWriteTime = [System.DateTimeOffset]::new(1980, 1, 1, 0, 0, 0, [System.TimeSpan]::Zero)
                $entryStream = $entry.Open()
                try {
                    [byte[]]$bytes = $extraEntry.Bytes
                    $entryStream.Write($bytes, 0, $bytes.Length)
                }
                finally {
                    $entryStream.Dispose()
                }
            }
        }
        finally {
            $destinationArchive.Dispose()
        }
    }
    finally {
        $destinationStream.Dispose()
        $sourceArchive.Dispose()
    }
}

function Replace-AsciiSequence {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][string] $OldValue,
        [Parameter(Mandatory)][string] $NewValue
    )

    [byte[]]$oldBytes = [System.Text.Encoding]::ASCII.GetBytes($OldValue)
    [byte[]]$newBytes = [System.Text.Encoding]::ASCII.GetBytes($NewValue)
    if ($oldBytes.Length -ne $newBytes.Length) {
        throw 'Mutation strings must have equal byte lengths.'
    }
    [byte[]]$result = $Bytes.Clone()
    $replacementCount = 0
    for ($offset = 0; $offset -le $result.Length - $oldBytes.Length; $offset++) {
        $matches = $true
        for ($index = 0; $index -lt $oldBytes.Length; $index++) {
            if ($result[$offset + $index] -ne $oldBytes[$index]) {
                $matches = $false
                break
            }
        }
        if ($matches) {
            [System.Array]::Copy($newBytes, 0, $result, $offset, $newBytes.Length)
            $replacementCount++
            $offset += $oldBytes.Length - 1
        }
    }
    if ($replacementCount -eq 0) {
        throw "Mutation source sequence was not found: $OldValue"
    }
    return $result
}

function Read-JavaU1 {
    param([Parameter(Mandatory)][byte[]] $Bytes, [Parameter(Mandatory)][ref] $Offset)
    if ($Offset.Value -ge $Bytes.Length) { throw 'Unexpected end of Java class.' }
    $value = [int]$Bytes[$Offset.Value]
    $Offset.Value++
    return $value
}

function Read-JavaU2 {
    param([Parameter(Mandatory)][byte[]] $Bytes, [Parameter(Mandatory)][ref] $Offset)
    return ((Read-JavaU1 -Bytes $Bytes -Offset $Offset) -shl 8) -bor (Read-JavaU1 -Bytes $Bytes -Offset $Offset)
}

function Read-JavaU4 {
    param([Parameter(Mandatory)][byte[]] $Bytes, [Parameter(Mandatory)][ref] $Offset)
    [uint32]$value = 0
    for ($index = 0; $index -lt 4; $index++) {
        $value = ($value -shl 8) -bor [uint32](Read-JavaU1 -Bytes $Bytes -Offset $Offset)
    }
    return $value
}

function Skip-JavaBytes {
    param([Parameter(Mandatory)][byte[]] $Bytes, [Parameter(Mandatory)][ref] $Offset, [Parameter(Mandatory)][uint32] $Count)
    if ([uint64]$Offset.Value + $Count -gt [uint64]$Bytes.Length) { throw 'Unexpected end of Java class.' }
    $Offset.Value += [int]$Count
}

function Set-JavaMethodAccessFlags {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][string] $MethodName,
        [Parameter(Mandatory)][string] $Descriptor,
        [Parameter(Mandatory)][int] $AccessFlags
    )

    [byte[]]$result = $Bytes.Clone()
    $offset = 0
    if ([uint64](Read-JavaU4 -Bytes $result -Offset ([ref]$offset)) -ne 3405691582) { throw 'Java class magic is invalid.' }
    Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 4
    $poolCount = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
    $pool = [object[]]::new($poolCount)
    for ($poolIndex = 1; $poolIndex -lt $poolCount; $poolIndex++) {
        $tag = Read-JavaU1 -Bytes $result -Offset ([ref]$offset)
        switch ($tag) {
            1 {
                $length = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
                if ($offset + $length -gt $result.Length) { throw 'Unexpected end of Java class UTF-8 entry.' }
                $pool[$poolIndex] = [System.Text.Encoding]::UTF8.GetString($result, $offset, $length)
                $offset += $length
            }
            { $_ -in @(3, 4) } { Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 4 }
            { $_ -in @(5, 6) } { Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 8; $poolIndex++ }
            { $_ -in @(7, 8, 16, 19, 20) } { Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 2 }
            { $_ -in @(9, 10, 11, 12, 17, 18) } { Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 4 }
            15 { Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 3 }
            default { throw "Unsupported Java constant-pool tag: $tag" }
        }
    }
    Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 6
    $interfaceCount = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
    Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count ([uint32](2 * $interfaceCount))
    $fieldCount = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
    for ($fieldIndex = 0; $fieldIndex -lt $fieldCount; $fieldIndex++) {
        Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 6
        $attributeCount = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
        for ($attributeIndex = 0; $attributeIndex -lt $attributeCount; $attributeIndex++) {
            Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 2
            Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count (Read-JavaU4 -Bytes $result -Offset ([ref]$offset))
        }
    }
    $methodCount = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
    $matchCount = 0
    for ($methodIndex = 0; $methodIndex -lt $methodCount; $methodIndex++) {
        $accessOffset = $offset
        $null = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
        $nameIndex = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
        $descriptorIndex = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
        $attributeCount = Read-JavaU2 -Bytes $result -Offset ([ref]$offset)
        if ([string]$pool[$nameIndex] -ceq $MethodName -and [string]$pool[$descriptorIndex] -ceq $Descriptor) {
            $result[$accessOffset] = [byte](($AccessFlags -shr 8) -band 0xff)
            $result[$accessOffset + 1] = [byte]($AccessFlags -band 0xff)
            $matchCount++
        }
        for ($attributeIndex = 0; $attributeIndex -lt $attributeCount; $attributeIndex++) {
            Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count 2
            Skip-JavaBytes -Bytes $result -Offset ([ref]$offset) -Count (Read-JavaU4 -Bytes $result -Offset ([ref]$offset))
        }
    }
    if ($matchCount -ne 1) { throw "Expected one Java method mutation target, found $matchCount." }
    return $result
}

& $validator -JarPath $jarPath -ManifestPath $manifestPath
$sourceRoot = Join-Path $repoRoot 'native-forks/SDL'
if (Test-Path -LiteralPath (Join-Path $sourceRoot '.git')) {
    & $validator -JarPath $jarPath -ManifestPath $manifestPath -SourceRoot $sourceRoot
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-android-bridge-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary Android bridge path: $tempRoot"
}
New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null

try {
    $mismatchManifestPath = Join-Path $tempRoot 'release-manifest-mismatch.json'
    $mismatchManifest = Get-Content -LiteralPath $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 64
    $mismatchManifest.components | Where-Object id -EQ 'SDL' | ForEach-Object { $_.nativeVersion = '3.4.13' }
    $mismatchManifest | ConvertTo-Json -Depth 64 | Set-Content -LiteralPath $mismatchManifestPath -Encoding UTF8
    Assert-ValidationFails -Description 'Java/native version mismatch' -Action {
        & $validator -JarPath $jarPath -ManifestPath $mismatchManifestPath *> $null
    }

    $missingClassJar = Join-Path $tempRoot 'missing-class.jar'
    New-ZipFixture -Path $missingClassJar -Entries ([ordered]@{
        'META-INF/SDL3-CS-BRIDGE.json' = [System.Text.Encoding]::UTF8.GetBytes('{}')
    })
    Assert-ValidationFails -Description 'missing SDLActivity.class' -Action {
        & $validator -JarPath $missingClassJar -ManifestPath $manifestPath *> $null
    }

    $malformedClassJar = Join-Path $tempRoot 'malformed-class.jar'
    New-ZipFixture -Path $malformedClassJar -Entries ([ordered]@{
        'org/libsdl/app/SDLActivity.class' = [byte[]](0xca, 0xfe, 0xba)
        'META-INF/SDL3-CS-BRIDGE.json' = [System.Text.Encoding]::UTF8.GetBytes('{}')
    })
    Assert-ValidationFails -Description 'malformed SDLActivity.class' -Action {
        & $validator -JarPath $malformedClassJar -ManifestPath $manifestPath *> $null
    }

    $wrongDescriptorJar = Join-Path $tempRoot 'wrong-native-get-version-descriptor.jar'
    New-MutatedJar -SourcePath $jarPath -DestinationPath $wrongDescriptorJar -TransformEntry {
        param([string] $Name, [byte[]] $Bytes)
        if ($Name -ceq 'org/libsdl/app/SDLActivity.class') {
            return Replace-AsciiSequence -Bytes $Bytes -OldValue '()Ljava/lang/String;' -NewValue '()Ljava/lang/Object;'
        }
        return $Bytes
    }
    Assert-ValidationFails -Description 'runtime-incompatible nativeGetVersion descriptor' -Action {
        & $validator -JarPath $wrongDescriptorJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
    }

    $wrongAccessJar = Join-Path $tempRoot 'wrong-native-get-version-access.jar'
    New-MutatedJar -SourcePath $jarPath -DestinationPath $wrongAccessJar -TransformEntry {
        param([string] $Name, [byte[]] $Bytes)
        if ($Name -ceq 'org/libsdl/app/SDLActivity.class') {
            return Set-JavaMethodAccessFlags -Bytes $Bytes -MethodName 'nativeGetVersion' -Descriptor '()Ljava/lang/String;' -AccessFlags 0x0009
        }
        return $Bytes
    }
    Assert-ValidationFails -Description 'non-native nativeGetVersion access flags' -Action {
        & $validator -JarPath $wrongAccessJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
    }

    $truncatedClassJar = Join-Path $tempRoot 'truncated-class.jar'
    New-MutatedJar -SourcePath $jarPath -DestinationPath $truncatedClassJar -TransformEntry {
        param([string] $Name, [byte[]] $Bytes)
        if ($Name -ceq 'org/libsdl/app/SDLActivity.class') {
            [byte[]]$truncated = [byte[]]::new($Bytes.Length - 1)
            [System.Array]::Copy($Bytes, $truncated, $truncated.Length)
            return $truncated
        }
        return $Bytes
    }
    Assert-ValidationFails -Description 'structured class truncation' -Action {
        & $validator -JarPath $truncatedClassJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
    }

    $staleClassJar = Join-Path $tempRoot 'stale-class.jar'
    New-MutatedJar -SourcePath $jarPath -DestinationPath $staleClassJar -ExtraEntries @(
        [pscustomobject]@{ Name = 'org/libsdl/app/Stale.class'; Bytes = [byte[]](0xca, 0xfe, 0xba, 0xbe) }
    )
    Assert-ValidationFails -Description 'pinned JAR hash mismatch' -Action {
        & $validator -JarPath $staleClassJar -ManifestPath $manifestPath *> $null
    }
    Assert-ValidationFails -Description 'undeclared stale class entry' -Action {
        & $validator -JarPath $staleClassJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
    }

    foreach ($unsafeEntryName in @('../evil.class', 'META-INF/STALE.SF')) {
        $safeFileName = $unsafeEntryName.Replace('/', '-').Replace('.', '_')
        $unsafeEntryJar = Join-Path $tempRoot "unsafe-$safeFileName.jar"
        New-MutatedJar -SourcePath $jarPath -DestinationPath $unsafeEntryJar -ExtraEntries @(
            [pscustomobject]@{ Name = $unsafeEntryName; Bytes = [byte[]](1) }
        )
        Assert-ValidationFails -Description "unsafe or unexpected entry $unsafeEntryName" -Action {
            & $validator -JarPath $unsafeEntryJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
        }
    }

    $duplicateEntryJar = Join-Path $tempRoot 'duplicate-entry.jar'
    New-MutatedJar -SourcePath $jarPath -DestinationPath $duplicateEntryJar -ExtraEntries @(
        [pscustomobject]@{ Name = 'META-INF/SDL3-CS-BRIDGE.json'; Bytes = [System.Text.Encoding]::UTF8.GetBytes('{}') }
    )
    Assert-ValidationFails -Description 'duplicate canonical entry' -Action {
        & $validator -JarPath $duplicateEntryJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
    }

    $oversizedEntryJar = Join-Path $tempRoot 'oversized-entry.jar'
    New-MutatedJar -SourcePath $jarPath -DestinationPath $oversizedEntryJar -ExtraEntries @(
        [pscustomobject]@{ Name = 'org/libsdl/app/Oversized.class'; Bytes = [byte[]]::new(4MB + 1) }
    )
    Assert-ValidationFails -Description 'oversized uncompressed entry' -Action {
        & $validator -JarPath $oversizedEntryJar -ManifestPath $manifestPath -SkipPinnedJarHashValidation *> $null
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-android-bridge-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary Android bridge path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Android bridge JAR tests passed.'
