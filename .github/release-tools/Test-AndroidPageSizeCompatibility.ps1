#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory, Position = 0, ValueFromPipeline, ValueFromPipelineByPropertyName)]
    [Alias('FullName')]
    [ValidateNotNullOrEmpty()]
    [string[]] $Path
)

$minimumElf64LoadAlignment = [uint64]0x4000
$maximumArchiveDepth = 8
$archiveExtensions = [System.Collections.Generic.HashSet[string]]::new(
    [string[]]@('.zip', '.nupkg', '.aar', '.apk', '.aab'),
    [System.StringComparer]::OrdinalIgnoreCase)
$validatedFiles = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
$results = [System.Collections.Generic.List[object]]::new()
$errors = [System.Collections.Generic.List[string]]::new()
$candidateCount = 0
$androidMachineInfo = @{
    3 = [pscustomobject]@{ Name = 'EM_386'; Rid = 'android-x86'; ElfClass = 1 }
    40 = [pscustomobject]@{ Name = 'EM_ARM'; Rid = 'android-arm'; ElfClass = 1 }
    62 = [pscustomobject]@{ Name = 'EM_X86_64'; Rid = 'android-x64'; ElfClass = 2 }
    183 = [pscustomobject]@{ Name = 'EM_AARCH64'; Rid = 'android-arm64'; ElfClass = 2 }
}
$androidPathSegmentToRid = @{
    'android-arm' = 'android-arm'
    'armeabi-v7a' = 'android-arm'
    'android-arm64' = 'android-arm64'
    'arm64-v8a' = 'android-arm64'
    'android-x86' = 'android-x86'
    'x86' = 'android-x86'
    'android-x64' = 'android-x64'
    'x86_64' = 'android-x64'
}

function Test-SharedLibraryName {
    param(
        [Parameter(Mandatory)]
        [string] $Name
    )

    return [System.IO.Path]::GetFileName($Name) -match '(?i)\.so(?:\..+)?$'
}

function Test-ArchiveName {
    param(
        [Parameter(Mandatory)]
        [string] $Name
    )

    return $script:archiveExtensions.Contains([System.IO.Path]::GetExtension($Name))
}

function Read-ExactBytes {
    param(
        [Parameter(Mandatory)]
        [System.IO.Stream] $Stream,

        [Parameter(Mandatory)]
        [ValidateRange(0, [int]::MaxValue)]
        [int] $Count,

        [Parameter(Mandatory)]
        [string] $DisplayPath
    )

    $buffer = [byte[]]::new($Count)
    $offset = 0
    while ($offset -lt $Count) {
        $read = $Stream.Read($buffer, $offset, $Count - $offset)
        if ($read -le 0) {
            throw "ELF '$DisplayPath' is truncated: expected $Count byte(s), read $offset."
        }
        $offset += $read
    }

    return $buffer
}

function Move-StreamToOffset {
    param(
        [Parameter(Mandatory)]
        [System.IO.Stream] $Stream,

        [Parameter(Mandatory)]
        [uint64] $CurrentOffset,

        [Parameter(Mandatory)]
        [uint64] $TargetOffset,

        [Parameter(Mandatory)]
        [string] $DisplayPath
    )

    if ($TargetOffset -lt $CurrentOffset) {
        throw "ELF '$DisplayPath' has a program-header table that overlaps its ELF header."
    }

    $remaining = $TargetOffset - $CurrentOffset
    if ($remaining -eq 0) {
        return
    }

    if ($Stream.CanSeek) {
        [void]$Stream.Seek([int64]$TargetOffset, [System.IO.SeekOrigin]::Begin)
        return
    }

    $scratch = [byte[]]::new(8192)
    while ($remaining -gt 0) {
        $requested = [int][Math]::Min([uint64]$scratch.Length, $remaining)
        $read = $Stream.Read($scratch, 0, $requested)
        if ($read -le 0) {
            throw "ELF '$DisplayPath' is truncated before its program-header table."
        }
        $remaining -= [uint64]$read
    }
}

function Read-UInt16LittleEndian {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][int] $Offset
    )

    return [uint16]([uint16]$Bytes[$Offset] -bor ([uint16]$Bytes[$Offset + 1] -shl 8))
}

function Read-UInt32LittleEndian {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][int] $Offset
    )

    $value = [uint32]0
    for ($index = 0; $index -lt 4; $index++) {
        $value = $value -bor ([uint32]$Bytes[$Offset + $index] -shl (8 * $index))
    }
    return $value
}

function Read-UInt64LittleEndian {
    param(
        [Parameter(Mandatory)][byte[]] $Bytes,
        [Parameter(Mandatory)][int] $Offset
    )

    $value = [uint64]0
    for ($index = 0; $index -lt 8; $index++) {
        $value = $value -bor ([uint64]$Bytes[$Offset + $index] -shl (8 * $index))
    }
    return $value
}

function Test-PowerOfTwoAlignment {
    param(
        [Parameter(Mandatory)]
        [uint64] $Alignment
    )

    return $Alignment -le 1 -or ($Alignment -band ($Alignment - 1)) -eq 0
}

function Get-AndroidRidFromDisplayPath {
    param(
        [Parameter(Mandatory)]
        [string] $DisplayPath
    )

    $matches = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    $normalized = $DisplayPath.Replace('\', '/').Replace('!', '/')
    foreach ($segment in $normalized.Split('/', [System.StringSplitOptions]::RemoveEmptyEntries)) {
        $normalizedSegment = $segment.ToLowerInvariant()
        if ($script:androidPathSegmentToRid.ContainsKey($normalizedSegment)) {
            [void]$matches.Add([string]$script:androidPathSegmentToRid[$normalizedSegment])
        }
    }

    if ($matches.Count -gt 1) {
        throw "Shared library path '$DisplayPath' contains conflicting Android ABI scopes: $(@($matches) -join ', ')."
    }

    if ($matches.Count -eq 1) {
        return @($matches)[0]
    }

    return $null
}

function Test-ElfStream {
    param(
        [Parameter(Mandatory)]
        [System.IO.Stream] $Stream,

        [Parameter(Mandatory)]
        [uint64] $Length,

        [Parameter(Mandatory)]
        [string] $DisplayPath
    )

    if ($Length -lt 16) {
        throw "ELF '$DisplayPath' is truncated before the ELF identification header."
    }

    [byte[]]$identification = Read-ExactBytes -Stream $Stream -Count 16 -DisplayPath $DisplayPath
    if ($identification[0] -ne 0x7f -or
        $identification[1] -ne [byte][char]'E' -or
        $identification[2] -ne [byte][char]'L' -or
        $identification[3] -ne [byte][char]'F') {
        throw "Shared library '$DisplayPath' is not an ELF file."
    }
    if ($identification[5] -ne 1) {
        throw "ELF '$DisplayPath' is not little-endian."
    }
    if ($identification[6] -ne 1) {
        throw "ELF '$DisplayPath' has unsupported identification version $($identification[6])."
    }

    $elfClass = [int]$identification[4]
    if ($elfClass -notin @(1, 2)) {
        throw "ELF '$DisplayPath' has unsupported class $elfClass."
    }

    $headerSize = if ($elfClass -eq 1) { 52 } else { 64 }
    $minimumProgramHeaderSize = if ($elfClass -eq 1) { 32 } else { 56 }
    if ($Length -lt [uint64]$headerSize) {
        throw "ELF '$DisplayPath' is truncated: class requires a $headerSize-byte header, file has $Length byte(s)."
    }

    [byte[]]$headerTail = Read-ExactBytes -Stream $Stream -Count ($headerSize - 16) -DisplayPath $DisplayPath
    $header = [byte[]]::new($headerSize)
    [System.Array]::Copy($identification, 0, $header, 0, $identification.Length)
    [System.Array]::Copy($headerTail, 0, $header, $identification.Length, $headerTail.Length)

    $machine = [int](Read-UInt16LittleEndian -Bytes $header -Offset 18)
    if (-not $script:androidMachineInfo.ContainsKey($machine)) {
        throw "ELF '$DisplayPath' declares unsupported Android e_machine $machine."
    }
    $machineInfo = $script:androidMachineInfo[$machine]
    if ([int]$machineInfo.ElfClass -ne $elfClass) {
        throw "ELF '$DisplayPath' declares $($machineInfo.Name) with incompatible ELF class $elfClass."
    }

    $expectedRid = Get-AndroidRidFromDisplayPath -DisplayPath $DisplayPath
    if ($expectedRid -and -not $machineInfo.Rid.Equals($expectedRid, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "ELF '$DisplayPath' declares $($machineInfo.Name) for $($machineInfo.Rid), expected machine for $expectedRid."
    }

    if ($elfClass -eq 1) {
        $programHeaderOffset = [uint64](Read-UInt32LittleEndian -Bytes $header -Offset 28)
        $declaredHeaderSize = Read-UInt16LittleEndian -Bytes $header -Offset 40
        $programHeaderEntrySize = Read-UInt16LittleEndian -Bytes $header -Offset 42
        $programHeaderCount = Read-UInt16LittleEndian -Bytes $header -Offset 44
    }
    else {
        $programHeaderOffset = Read-UInt64LittleEndian -Bytes $header -Offset 32
        $declaredHeaderSize = Read-UInt16LittleEndian -Bytes $header -Offset 52
        $programHeaderEntrySize = Read-UInt16LittleEndian -Bytes $header -Offset 54
        $programHeaderCount = Read-UInt16LittleEndian -Bytes $header -Offset 56
    }

    if ($declaredHeaderSize -lt $headerSize) {
        throw "ELF '$DisplayPath' declares invalid ELF header size $declaredHeaderSize; expected at least $headerSize."
    }
    if ($programHeaderCount -eq 0) {
        throw "ELF '$DisplayPath' has no program headers and therefore no PT_LOAD segment."
    }
    if ($programHeaderCount -eq [uint16]::MaxValue) {
        throw "ELF '$DisplayPath' uses an unsupported extended program-header count."
    }
    if ($programHeaderEntrySize -lt $minimumProgramHeaderSize) {
        throw "ELF '$DisplayPath' declares invalid program-header entry size $programHeaderEntrySize; expected at least $minimumProgramHeaderSize."
    }
    if ($programHeaderOffset -lt [uint64]$headerSize) {
        throw "ELF '$DisplayPath' has an invalid program-header offset $programHeaderOffset."
    }

    $programHeaderTableSize = [uint64]$programHeaderEntrySize * [uint64]$programHeaderCount
    if ($programHeaderOffset -gt $Length -or $programHeaderTableSize -gt ($Length - $programHeaderOffset)) {
        throw "ELF '$DisplayPath' has a truncated program-header table."
    }

    Move-StreamToOffset -Stream $Stream -CurrentOffset ([uint64]$headerSize) -TargetOffset $programHeaderOffset -DisplayPath $DisplayPath

    $loadAlignments = [System.Collections.Generic.List[uint64]]::new()
    for ($index = 0; $index -lt $programHeaderCount; $index++) {
        [byte[]]$programHeader = Read-ExactBytes -Stream $Stream -Count $programHeaderEntrySize -DisplayPath $DisplayPath
        $programType = Read-UInt32LittleEndian -Bytes $programHeader -Offset 0
        if ($programType -ne 1) {
            continue
        }

        if ($elfClass -eq 1) {
            $segmentOffset = [uint64](Read-UInt32LittleEndian -Bytes $programHeader -Offset 4)
            $virtualAddress = [uint64](Read-UInt32LittleEndian -Bytes $programHeader -Offset 8)
            $fileSize = [uint64](Read-UInt32LittleEndian -Bytes $programHeader -Offset 16)
            $memorySize = [uint64](Read-UInt32LittleEndian -Bytes $programHeader -Offset 20)
            $alignment = [uint64](Read-UInt32LittleEndian -Bytes $programHeader -Offset 28)
        }
        else {
            $segmentOffset = Read-UInt64LittleEndian -Bytes $programHeader -Offset 8
            $virtualAddress = Read-UInt64LittleEndian -Bytes $programHeader -Offset 16
            $fileSize = Read-UInt64LittleEndian -Bytes $programHeader -Offset 32
            $memorySize = Read-UInt64LittleEndian -Bytes $programHeader -Offset 40
            $alignment = Read-UInt64LittleEndian -Bytes $programHeader -Offset 48
        }

        if ($fileSize -gt $memorySize) {
            throw "ELF '$DisplayPath' PT_LOAD segment $index has file size $fileSize greater than memory size $memorySize."
        }
        if ($segmentOffset -gt $Length -or $fileSize -gt ($Length - $segmentOffset)) {
            throw "ELF '$DisplayPath' PT_LOAD segment $index extends beyond the end of the file."
        }
        if (-not (Test-PowerOfTwoAlignment -Alignment $alignment)) {
            throw "ELF '$DisplayPath' PT_LOAD segment $index has non-power-of-two alignment 0x$($alignment.ToString('x'))."
        }
        if ($alignment -gt 1 -and ($virtualAddress % $alignment) -ne ($segmentOffset % $alignment)) {
            throw "ELF '$DisplayPath' PT_LOAD segment $index has incongruent virtual address and file offset for alignment 0x$($alignment.ToString('x'))."
        }
        if ($elfClass -eq 2 -and $alignment -lt $script:minimumElf64LoadAlignment) {
            throw "ELF64 '$DisplayPath' PT_LOAD segment $index alignment 0x$($alignment.ToString('x')) is below the required 0x$($script:minimumElf64LoadAlignment.ToString('x'))."
        }

        $loadAlignments.Add($alignment)
    }

    if ($loadAlignments.Count -eq 0) {
        throw "ELF '$DisplayPath' has no PT_LOAD segments."
    }

    $minimumAlignment = ($loadAlignments | Measure-Object -Minimum).Minimum
    $script:results.Add([pscustomobject]@{
        Path = $DisplayPath
        ElfClass = if ($elfClass -eq 1) { 'ELF32' } else { 'ELF64' }
        Machine = $machineInfo.Name
        AndroidRid = if ($expectedRid) { $expectedRid } else { $machineInfo.Rid }
        LoadSegments = $loadAlignments.Count
        MinimumAlignment = "0x$(([uint64]$minimumAlignment).ToString('x'))"
        Status = 'compatible'
    })
}

function Invoke-ElfValidation {
    param(
        [Parameter(Mandatory)]
        [System.IO.Stream] $Stream,

        [Parameter(Mandatory)]
        [uint64] $Length,

        [Parameter(Mandatory)]
        [string] $DisplayPath
    )

    $script:candidateCount++
    try {
        Test-ElfStream -Stream $Stream -Length $Length -DisplayPath $DisplayPath
    }
    catch {
        $script:errors.Add($_.Exception.Message)
    }
}

function Invoke-ZipArchiveValidation {
    param(
        [Parameter(Mandatory)]
        [System.IO.Compression.ZipArchive] $Archive,

        [Parameter(Mandatory)]
        [string] $DisplayPath,

        [Parameter(Mandatory)]
        [int] $Depth
    )

    if ($Depth -gt $script:maximumArchiveDepth) {
        $script:errors.Add("Archive nesting exceeds $($script:maximumArchiveDepth) levels at '$DisplayPath'.")
        return
    }

    foreach ($entry in $Archive.Entries) {
        if ([string]::IsNullOrWhiteSpace($entry.Name)) {
            continue
        }

        $entryDisplayPath = "$DisplayPath!$($entry.FullName.Replace('\', '/'))"
        if (Test-SharedLibraryName -Name $entry.Name) {
            $entryStream = $null
            try {
                $entryStream = $entry.Open()
                Invoke-ElfValidation -Stream $entryStream -Length ([uint64]$entry.Length) -DisplayPath $entryDisplayPath
            }
            catch {
                $script:candidateCount++
                $script:errors.Add("Could not read shared library '$entryDisplayPath': $($_.Exception.Message)")
            }
            finally {
                if ($entryStream) {
                    $entryStream.Dispose()
                }
            }
            continue
        }

        if (-not (Test-ArchiveName -Name $entry.Name)) {
            continue
        }

        $entryStream = $null
        $nestedStream = $null
        $nestedArchive = $null
        try {
            $entryStream = $entry.Open()
            $nestedStream = [System.IO.MemoryStream]::new()
            $entryStream.CopyTo($nestedStream)
            $nestedStream.Position = 0
            $nestedArchive = [System.IO.Compression.ZipArchive]::new($nestedStream, [System.IO.Compression.ZipArchiveMode]::Read, $true)
            Invoke-ZipArchiveValidation -Archive $nestedArchive -DisplayPath $entryDisplayPath -Depth ($Depth + 1)
        }
        catch {
            $script:errors.Add("Could not read nested archive '$entryDisplayPath': $($_.Exception.Message)")
        }
        finally {
            if ($nestedArchive) { $nestedArchive.Dispose() }
            if ($nestedStream) { $nestedStream.Dispose() }
            if ($entryStream) { $entryStream.Dispose() }
        }
    }
}

function Invoke-ArchiveFileValidation {
    param(
        [Parameter(Mandatory)]
        [string] $FilePath
    )

    $archive = $null
    try {
        Add-Type -AssemblyName System.IO.Compression.FileSystem
        $archive = [System.IO.Compression.ZipFile]::OpenRead($FilePath)
        Invoke-ZipArchiveValidation -Archive $archive -DisplayPath $FilePath -Depth 1
    }
    catch {
        $script:errors.Add("Could not read archive '$FilePath': $($_.Exception.Message)")
    }
    finally {
        if ($archive) {
            $archive.Dispose()
        }
    }
}

function Invoke-FileValidation {
    param(
        [Parameter(Mandatory)]
        [string] $FilePath,

        [switch] $Explicit
    )

    $resolvedFile = [System.IO.Path]::GetFullPath($FilePath)
    if (-not $script:validatedFiles.Add($resolvedFile)) {
        return
    }

    if (Test-ArchiveName -Name $resolvedFile) {
        Invoke-ArchiveFileValidation -FilePath $resolvedFile
        return
    }

    if (-not $Explicit -and -not (Test-SharedLibraryName -Name $resolvedFile)) {
        return
    }

    $stream = $null
    try {
        $stream = [System.IO.File]::Open($resolvedFile, [System.IO.FileMode]::Open, [System.IO.FileAccess]::Read, [System.IO.FileShare]::Read)
        Invoke-ElfValidation -Stream $stream -Length ([uint64]$stream.Length) -DisplayPath $resolvedFile
    }
    catch {
        $script:candidateCount++
        $script:errors.Add("Could not read shared library '$resolvedFile': $($_.Exception.Message)")
    }
    finally {
        if ($stream) {
            $stream.Dispose()
        }
    }
}

foreach ($inputPath in $Path) {
    if (-not (Test-Path -LiteralPath $inputPath)) {
        $errors.Add("Android page-size validation path does not exist: $inputPath")
        continue
    }

    $item = Get-Item -LiteralPath $inputPath -Force
    if ($item.PSIsContainer) {
        foreach ($file in Get-ChildItem -LiteralPath $item.FullName -File -Recurse -Force) {
            if ((Test-SharedLibraryName -Name $file.Name) -or (Test-ArchiveName -Name $file.Name)) {
                Invoke-FileValidation -FilePath $file.FullName
            }
        }
    }
    else {
        Invoke-FileValidation -FilePath $item.FullName -Explicit
    }
}

if ($candidateCount -eq 0 -and $errors.Count -eq 0) {
    $errors.Add('Android page-size compatibility validation found no shared libraries.')
}

if ($results.Count -gt 0) {
    $results | Sort-Object Path | Format-Table -AutoSize
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Android page-size compatibility validation failed with $($errors.Count) issue(s) across $candidateCount shared library candidate(s)."
}

Write-Host "Android page-size compatibility is valid for $candidateCount shared library candidate(s)."
