#requires -Version 7.0
[CmdletBinding()]
param()

$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$validator = Join-Path $PSScriptRoot 'Test-AndroidPageSizeCompatibility.ps1'
$manifestPath = Join-Path $PSScriptRoot 'release-manifest.json'
$workflowPath = Join-Path $repoRoot '.github/workflows/release-native-packages.yml'
$consumerPath = Join-Path $PSScriptRoot 'Test-AndroidConsumerPackageBuild.ps1'
$requiredAndroidNdkVersion = '28.2.13676358'
$requiredFlexiblePageArgument = '-DANDROID_SUPPORT_FLEXIBLE_PAGE_SIZES=ON'

foreach ($requiredPath in @($validator, $manifestPath, $workflowPath, $consumerPath)) {
    if (-not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) {
        throw "Android page-size compatibility test dependency was not found: $requiredPath"
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
        throw "Expected Android page-size compatibility validation to fail: $Description"
    }
}

function Assert-TextContains {
    param(
        [Parameter(Mandatory)][string] $Text,
        [Parameter(Mandatory)][string] $Expected,
        [Parameter(Mandatory)][string] $Description
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        throw "$Description is missing expected text: $Expected"
    }
}

function Assert-TextDoesNotContain {
    param(
        [Parameter(Mandatory)][string] $Text,
        [Parameter(Mandatory)][string] $Unexpected,
        [Parameter(Mandatory)][string] $Description
    )

    if ($Text.Contains($Unexpected, [System.StringComparison]::Ordinal)) {
        throw "$Description contains forbidden text: $Unexpected"
    }
}

function Set-UInt16LittleEndian {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][int] $Offset,
        [Parameter(Mandatory)][uint16] $Value
    )

    $encoded = [System.BitConverter]::GetBytes($Value)
    [System.Array]::Copy($encoded, 0, $Bytes, $Offset, $encoded.Length)
}

function Set-UInt32LittleEndian {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][int] $Offset,
        [Parameter(Mandatory)][uint32] $Value
    )

    $encoded = [System.BitConverter]::GetBytes($Value)
    [System.Array]::Copy($encoded, 0, $Bytes, $Offset, $encoded.Length)
}

function Set-UInt64LittleEndian {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][int] $Offset,
        [Parameter(Mandatory)][uint64] $Value
    )

    $encoded = [System.BitConverter]::GetBytes($Value)
    [System.Array]::Copy($encoded, 0, $Bytes, $Offset, $encoded.Length)
}

function New-ElfFixtureBytes {
    param(
        [Parameter(Mandatory)]
        [ValidateSet(32, 64)]
        [int] $Class,

        [Parameter(Mandatory)]
        [uint64[]] $LoadAlignments,

        [ValidateRange(-1, 65535)]
        [int] $Machine = -1,

        [switch] $NoLoadSegments
    )

    $headerSize = if ($Class -eq 32) { 52 } else { 64 }
    $programHeaderSize = if ($Class -eq 32) { 32 } else { 56 }
    $programHeaderCount = [Math]::Max(1, $LoadAlignments.Count)
    $bytes = [byte[]]::new($headerSize + ($programHeaderSize * $programHeaderCount))

    $bytes[0] = 0x7f
    $bytes[1] = [byte][char]'E'
    $bytes[2] = [byte][char]'L'
    $bytes[3] = [byte][char]'F'
    $bytes[4] = if ($Class -eq 32) { 1 } else { 2 }
    $bytes[5] = 1
    $bytes[6] = 1
    $resolvedMachine = if ($Machine -ge 0) { [uint16]$Machine } elseif ($Class -eq 32) { [uint16]40 } else { [uint16]183 }
    Set-UInt16LittleEndian -Bytes $bytes -Offset 18 -Value $resolvedMachine
    Set-UInt32LittleEndian -Bytes $bytes -Offset 20 -Value ([uint32]1)

    if ($Class -eq 32) {
        Set-UInt32LittleEndian -Bytes $bytes -Offset 28 -Value ([uint32]$headerSize)
        Set-UInt16LittleEndian -Bytes $bytes -Offset 40 -Value ([uint16]$headerSize)
        Set-UInt16LittleEndian -Bytes $bytes -Offset 42 -Value ([uint16]$programHeaderSize)
        Set-UInt16LittleEndian -Bytes $bytes -Offset 44 -Value ([uint16]$programHeaderCount)
    }
    else {
        Set-UInt64LittleEndian -Bytes $bytes -Offset 32 -Value ([uint64]$headerSize)
        Set-UInt16LittleEndian -Bytes $bytes -Offset 52 -Value ([uint16]$headerSize)
        Set-UInt16LittleEndian -Bytes $bytes -Offset 54 -Value ([uint16]$programHeaderSize)
        Set-UInt16LittleEndian -Bytes $bytes -Offset 56 -Value ([uint16]$programHeaderCount)
    }

    for ($index = 0; $index -lt $programHeaderCount; $index++) {
        $programHeaderOffset = $headerSize + ($programHeaderSize * $index)
        $programType = if ($NoLoadSegments) { 2 } else { 1 }
        $alignment = if ($LoadAlignments.Count -gt $index) { $LoadAlignments[$index] } else { [uint64]0x4000 }

        Set-UInt32LittleEndian -Bytes $bytes -Offset $programHeaderOffset -Value ([uint32]$programType)
        if ($Class -eq 32) {
            Set-UInt32LittleEndian -Bytes $bytes -Offset ($programHeaderOffset + 28) -Value ([uint32]$alignment)
        }
        else {
            Set-UInt64LittleEndian -Bytes $bytes -Offset ($programHeaderOffset + 48) -Value $alignment
        }
    }

    return $bytes
}

function New-ZipFixture {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][System.Collections.IDictionary] $Entries
    )

    Add-Type -AssemblyName System.IO.Compression
    $fileStream = [System.IO.File]::Open($Path, [System.IO.FileMode]::CreateNew, [System.IO.FileAccess]::Write, [System.IO.FileShare]::None)
    try {
        $archive = [System.IO.Compression.ZipArchive]::new($fileStream, [System.IO.Compression.ZipArchiveMode]::Create, $true)
        try {
            foreach ($entryName in $Entries.Keys) {
                $entry = $archive.CreateEntry([string]$entryName)
                $entryStream = $entry.Open()
                try {
                    [byte[]]$entryBytes = $Entries[$entryName]
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
        $fileStream.Dispose()
    }
}

$manifest = Get-Content -LiteralPath $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 64
if (-not $manifest.PSObject.Properties.Name.Contains('toolchains') -or
    -not $manifest.toolchains.PSObject.Properties.Name.Contains('androidNdkVersion') -or
    [string]$manifest.toolchains.androidNdkVersion -ne $requiredAndroidNdkVersion) {
    throw "Release manifest must pin toolchains.androidNdkVersion to exact NDK revision $requiredAndroidNdkVersion."
}

$androidRids = @($manifest.rids | Where-Object { $_.os -eq 'android' })
if ($androidRids.Count -eq 0) {
    throw 'Release manifest does not define any Android RIDs.'
}

foreach ($androidRid in $androidRids) {
    if (@($androidRid.cmakeArgs) -notcontains $requiredFlexiblePageArgument) {
        throw "Android RID '$($androidRid.rid)' must require $requiredFlexiblePageArgument."
    }
}

$workflowText = Get-Content -LiteralPath $workflowPath -Raw -Encoding UTF8
$consumerText = Get-Content -LiteralPath $consumerPath -Raw -Encoding UTF8
Assert-TextContains -Text $consumerText -Expected 'Name = "$ApplicationId.MainActivity",' -Description 'fully qualified Android runtime activity name'
Assert-TextContains -Text $consumerText -Expected 'Get-AndroidConsumerMainActivity -ApplicationId $applicationId' -Description 'RID-specific Android application ID handoff'
Assert-TextContains -Text $consumerText -Expected '<AndroidBundleConfigurationFile>BundleConfig.json</AndroidBundleConfigurationFile>' -Description 'custom Android App Bundle configuration handoff'
Assert-TextContains -Text $consumerText -Expected '"alignment": "PAGE_ALIGNMENT_16K"' -Description 'Android App Bundle 16 KB page alignment request'
Assert-TextContains -Text $consumerText -Expected '"enabled": true' -Description 'uncompressed Android native library bundle policy'
Assert-TextContains -Text $consumerText -Expected "Get-AndroidConsumerBundleConfiguration" -Description 'Android App Bundle configuration generator'
Assert-TextContains -Text $workflowText -Expected 'android_ndk_version: ${{ steps.native-matrix.outputs.android_ndk_version }}' -Description 'plan job Android NDK version output binding'
Assert-TextContains -Text $workflowText -Expected 'android_ndk_version=$($manifest.toolchains.androidNdkVersion)' -Description 'manifest Android NDK version output'
Assert-TextContains -Text $workflowText -Expected 'SDL3CS_ANDROID_NDK_VERSION: ${{ needs.plan.outputs.android_ndk_version }}' -Description 'manifest-derived Android NDK version environment handoff'
Assert-TextContains -Text $workflowText -Expected 'ndk_version="$SDL3CS_ANDROID_NDK_VERSION"' -Description 'Android NDK workflow manifest value handoff'
Assert-TextContains -Text $workflowText -Expected 'ndk="$ANDROID_HOME/ndk/$ndk_version"' -Description 'exact Android NDK installation path'
foreach ($forbiddenFallback in @(
    "ndk_version=`"$requiredAndroidNdkVersion`"",
    '${ANDROID_NDK_HOME:-${ANDROID_NDK_ROOT:-}}',
    'existing_ndk=',
    'find "$ANDROID_HOME/ndk"'
)) {
    Assert-TextDoesNotContain -Text $workflowText -Unexpected $forbiddenFallback -Description 'Android NDK workflow selection'
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-android-page-size-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary Android page-size test path: $tempRoot"
}

New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null

try {
    $elf32Aligned = Join-Path $tempRoot 'libElf32Aligned.so'
    $elf32LegacyAligned = Join-Path $tempRoot 'libElf32LegacyAligned.so'
    $elf64Aligned = Join-Path $tempRoot 'libElf64Aligned.so'
    $elf64Misaligned = Join-Path $tempRoot 'libElf64Misaligned.so'
    $noLoadSegments = Join-Path $tempRoot 'libNoLoadSegments.so'
    $malformed = Join-Path $tempRoot 'libMalformed.so'
    $truncated = Join-Path $tempRoot 'libTruncated.so'
    $unsupportedMachine = Join-Path $tempRoot 'libUnsupportedMachine.so'

    [System.IO.File]::WriteAllBytes($elf32Aligned, (New-ElfFixtureBytes -Class 32 -LoadAlignments @([uint64]0x4000)))
    [System.IO.File]::WriteAllBytes($elf32LegacyAligned, (New-ElfFixtureBytes -Class 32 -LoadAlignments @([uint64]0x1000)))
    [System.IO.File]::WriteAllBytes($elf64Aligned, (New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000, [uint64]0x10000)))
    [System.IO.File]::WriteAllBytes($elf64Misaligned, (New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000, [uint64]0x1000)))
    [System.IO.File]::WriteAllBytes($noLoadSegments, (New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000) -NoLoadSegments))
    [System.IO.File]::WriteAllBytes($malformed, [System.Text.Encoding]::ASCII.GetBytes('not an ELF file'))
    [System.IO.File]::WriteAllBytes($unsupportedMachine, (New-ElfFixtureBytes -Class 64 -Machine 0 -LoadAlignments @([uint64]0x4000)))

    $validElf64 = New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000)
    $truncatedBytes = [byte[]]::new(70)
    [System.Array]::Copy($validElf64, $truncatedBytes, $truncatedBytes.Length)
    [System.IO.File]::WriteAllBytes($truncated, $truncatedBytes)

    & $validator -Path @($elf32Aligned, $elf32LegacyAligned, $elf64Aligned)

    Assert-ValidationFails -Description '64-bit Android PT_LOAD alignment 0x1000' -Action {
        & $validator -Path $elf64Misaligned *> $null
    }
    Assert-ValidationFails -Description 'ELF without PT_LOAD segments' -Action {
        & $validator -Path $noLoadSegments *> $null
    }
    Assert-ValidationFails -Description 'malformed ELF input' -Action {
        & $validator -Path $malformed *> $null
    }
    Assert-ValidationFails -Description 'truncated ELF program-header table' -Action {
        & $validator -Path $truncated *> $null
    }
    Assert-ValidationFails -Description 'unsupported Android ELF machine' -Action {
        & $validator -Path $unsupportedMachine *> $null
    }

    $wrongMachineRoot = Join-Path $tempRoot 'wrong-machine/android-arm64'
    New-Item -ItemType Directory -Force -Path $wrongMachineRoot | Out-Null
    [System.IO.File]::WriteAllBytes(
        (Join-Path $wrongMachineRoot 'libWrongMachine.so'),
        (New-ElfFixtureBytes -Class 64 -Machine 62 -LoadAlignments @([uint64]0x4000)))
    Assert-ValidationFails -Description 'x86_64 ELF inside android-arm64 payload' -Action {
        & $validator -Path $wrongMachineRoot *> $null
    }

    $recursivePassRoot = Join-Path $tempRoot 'recursive-pass'
    $recursivePassDependencyRoot = Join-Path $recursivePassRoot 'nested/dependencies'
    New-Item -ItemType Directory -Force -Path $recursivePassDependencyRoot | Out-Null
    [System.IO.File]::WriteAllBytes((Join-Path $recursivePassRoot 'libSDL3.so'), (New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000)))
    [System.IO.File]::WriteAllBytes((Join-Path $recursivePassDependencyRoot 'libspirv-cross-c-shared.so.1'), (New-ElfFixtureBytes -Class 32 -LoadAlignments @([uint64]0x1000)))
    & $validator -Path $recursivePassRoot

    $mixedRoot = Join-Path $tempRoot 'mixed-good-bad'
    $mixedDependencyRoot = Join-Path $mixedRoot 'nested/dependencies'
    New-Item -ItemType Directory -Force -Path $mixedDependencyRoot | Out-Null
    [System.IO.File]::WriteAllBytes((Join-Path $mixedRoot 'libSDL3_mixer.so'), (New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000)))
    [System.IO.File]::WriteAllBytes((Join-Path $mixedDependencyRoot 'libopus.so'), (New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x1000)))
    Assert-ValidationFails -Description 'recursively discovered misaligned dependency beside aligned primary library' -Action {
        & $validator -Path $mixedRoot *> $null
    }

    $alignedPackage = Join-Path $tempRoot 'aligned.nupkg'
    New-ZipFixture -Path $alignedPackage -Entries ([ordered]@{
        'runtimes/android-arm64/native/libSDL3.so' = New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000)
        'runtimes/android-arm/native/libdependency.so.1' = New-ElfFixtureBytes -Class 32 -LoadAlignments @([uint64]0x1000)
    })
    & $validator -Path $alignedPackage

    $misalignedApk = Join-Path $tempRoot 'misaligned.apk'
    New-ZipFixture -Path $misalignedApk -Entries ([ordered]@{
        'lib/arm64-v8a/libSDL3.so' = New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x4000)
        'lib/arm64-v8a/libdependency.so' = New-ElfFixtureBytes -Class 64 -LoadAlignments @([uint64]0x1000)
    })
    Assert-ValidationFails -Description 'misaligned ELF64 dependency inside final APK archive' -Action {
        & $validator -Path $misalignedApk *> $null
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-android-page-size-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary Android page-size test path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Android 16 KB page-size compatibility tests passed.'
