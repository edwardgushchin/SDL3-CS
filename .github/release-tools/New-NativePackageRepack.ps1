#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][int] $PackageRevision,
    [Parameter(Mandatory)][int] $PreviousPackageRevision,
    [Parameter(Mandatory)][string[]] $PackageIds,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $OutputDir,
    [string] $PreviousPackageDir,
    [string] $AdditionalFilePath = 'SDL3-CS.NativePackages/ThirdPartyLicenses/libwebp/COPYING',
    [string] $AdditionalPackagePath = 'licenses/libwebp/COPYING',
    [string] $RepositoryCommit,
    [switch] $VersionOnly,
    [switch] $SkipSourceSignatureValidation
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-PackageUri {
    param([Parameter(Mandatory)][object] $Package)

    $id = $Package.Id.ToLowerInvariant()
    $version = (Get-ReleaseNormalizedNuGetVersion -PackageVersion $Package.PackageVersion).ToLowerInvariant()
    return "https://api.nuget.org/v3-flatcontainer/$id/$version/$id.$version.nupkg"
}

function Get-EntryBytes {
    param([Parameter(Mandatory)][System.IO.Compression.ZipArchiveEntry] $Entry)

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

function Set-XmlElementValue {
    param(
        [Parameter(Mandatory)][xml] $Xml,
        [Parameter(Mandatory)][string] $LocalName,
        [Parameter(Mandatory)][string] $Value
    )

    $nodes = @($Xml.SelectNodes("//*[local-name()='$LocalName']"))
    if ($nodes.Count -ne 1) {
        throw "Expected exactly one XML element '$LocalName', got $($nodes.Count)."
    }
    $nodes[0].InnerText = $Value
}

function Convert-XmlToBytes {
    param([Parameter(Mandatory)][xml] $Xml)

    $settings = [System.Xml.XmlWriterSettings]::new()
    $settings.Encoding = [System.Text.UTF8Encoding]::new($false)
    $settings.Indent = $true
    $settings.NewLineChars = "`r`n"
    $settings.NewLineHandling = [System.Xml.NewLineHandling]::Replace
    $memory = [System.IO.MemoryStream]::new()
    try {
        $writer = [System.Xml.XmlWriter]::Create($memory, $settings)
        try {
            $Xml.Save($writer)
        }
        finally {
            $writer.Dispose()
        }
        return $memory.ToArray()
    }
    finally {
        $memory.Dispose()
    }
}

function Convert-BytesToXml {
    param([Parameter(Mandatory)][byte[]] $Bytes)

    $memory = [System.IO.MemoryStream]::new($Bytes, $false)
    try {
        $document = [System.Xml.XmlDocument]::new()
        $document.PreserveWhitespace = $false
        $document.Load($memory)
        return $document
    }
    finally {
        $memory.Dispose()
    }
}

if ($PackageRevision -lt 0 -or $PreviousPackageRevision -lt 0) {
    throw 'Package revisions must be zero or greater.'
}
if (-not $PackageIds -or $PackageIds.Count -eq 0) {
    throw 'PackageIds must select at least one native package.'
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $OutputDir) {
    $OutputDir = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'nuget'
}
$OutputDir = Resolve-ReleasePath $OutputDir
New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null
if ($PreviousPackageDir) {
    $PreviousPackageDir = Resolve-ReleasePath $PreviousPackageDir
}
$additionalFile = $null
if ($VersionOnly) {
    if ($PSBoundParameters.ContainsKey('AdditionalFilePath') -or $PSBoundParameters.ContainsKey('AdditionalPackagePath')) {
        throw 'VersionOnly cannot be combined with an additional file or package path.'
    }
}
else {
    $additionalFile = Resolve-ReleasePath $AdditionalFilePath
    if (-not (Test-Path -LiteralPath $additionalFile -PathType Leaf)) {
        throw "Additional repack file was not found: $additionalFile"
    }
    $additionalPackageSegments = @($AdditionalPackagePath.Replace('\', '/').Split('/'))
    if ([string]::IsNullOrWhiteSpace($AdditionalPackagePath) -or
        $AdditionalPackagePath.StartsWith('/', [System.StringComparison]::Ordinal) -or
        $additionalPackageSegments -contains '' -or
        $additionalPackageSegments -contains '.' -or
        $additionalPackageSegments -contains '..') {
        throw "AdditionalPackagePath must be a safe relative package path: $AdditionalPackagePath"
    }
}
if (-not $RepositoryCommit) {
    $RepositoryCommit = (& git -C (Get-ReleaseRepoRoot) rev-parse HEAD 2>&1 | Out-String).Trim()
    if ($LASTEXITCODE -ne 0 -or $RepositoryCommit -notmatch '^[0-9a-f]{40}$') {
        throw "Could not determine release repository commit: $RepositoryCommit"
    }
}
elseif ($RepositoryCommit -notmatch '^[0-9a-f]{40}$') {
    throw "RepositoryCommit must be a full 40-character hexadecimal commit: $RepositoryCommit"
}

$targetRows = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
$previousRows = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PreviousPackageRevision)
$selectedIds = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
$selections = @()
foreach ($packageId in $PackageIds) {
    if ([string]::IsNullOrWhiteSpace($packageId)) {
        throw 'PackageIds must not contain empty values.'
    }
    if (-not $selectedIds.Add($packageId)) {
        throw "PackageIds contains duplicate package id: $packageId"
    }
    $target = @($targetRows | Where-Object { $_.Id -eq $packageId })
    $previous = @($previousRows | Where-Object { $_.Id -eq $packageId })
    if ($target.Count -ne 1 -or $previous.Count -ne 1) {
        throw "PackageIds contains unknown package id: $packageId"
    }
    if ($target[0].Kind -ne 'native' -or $previous[0].Kind -ne 'native') {
        throw "PackageIds can select only native packages: $packageId"
    }
    if ([version]$target[0].PackageVersion -le [version]$previous[0].PackageVersion) {
        throw "Target package version must be newer than previous package version for $packageId."
    }
    $selections += [pscustomobject]@{ Target = $target[0]; Previous = $previous[0] }
}

$tempRoot = $null
$rows = @()
try {
    if (-not $PreviousPackageDir) {
        $tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
        $tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-native-repack-input-$([guid]::NewGuid().ToString('N'))"))
        if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
            throw "Unsafe temporary repack input path: $tempRoot"
        }
        New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null
        $PreviousPackageDir = $tempRoot
    }

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    foreach ($selection in $selections) {
        $target = $selection.Target
        $previous = $selection.Previous
        $previousPath = Get-ReleaseNuGetPackagePath -PackageDir $PreviousPackageDir -Package $previous
        if (-not (Test-Path -LiteralPath $previousPath -PathType Leaf)) {
            Invoke-WebRequest -Uri (Get-PackageUri -Package $previous) -OutFile $previousPath -TimeoutSec 120
        }
        if (-not $SkipSourceSignatureValidation) {
            & dotnet nuget verify $previousPath --all
            if ($LASTEXITCODE -ne 0) {
                throw "Published source package signature validation failed for $($previous.Id) $($previous.PackageVersion)."
            }
        }
        $targetPath = Get-ReleaseNuGetPackagePath -PackageDir $OutputDir -Package $target
        if (Test-Path -LiteralPath $targetPath) {
            Remove-Item -LiteralPath $targetPath -Force
        }

        $sourceZip = [System.IO.Compression.ZipFile]::OpenRead($previousPath)
        try {
            if (-not $VersionOnly -and $sourceZip.GetEntry($AdditionalPackagePath)) {
                throw "Previous package already contains target additional entry: $($previous.Id) -> $AdditionalPackagePath"
            }
            $targetStream = [System.IO.File]::Open($targetPath, [System.IO.FileMode]::CreateNew)
            try {
                $targetZip = [System.IO.Compression.ZipArchive]::new($targetStream, [System.IO.Compression.ZipArchiveMode]::Create, $false)
                try {
                    foreach ($sourceEntry in $sourceZip.Entries) {
                        $entryName = $sourceEntry.FullName.Replace('\', '/')
                        if (-not $entryName -or $entryName.EndsWith('/', [System.StringComparison]::Ordinal) -or
                            $entryName -eq '.signature.p7s') {
                            continue
                        }

                        $bytes = Get-EntryBytes -Entry $sourceEntry
                        if ($entryName.EndsWith('.nuspec', [System.StringComparison]::OrdinalIgnoreCase)) {
                            [xml] $nuspec = Convert-BytesToXml -Bytes $bytes
                            Set-XmlElementValue -Xml $nuspec -LocalName 'version' -Value $target.PackageVersion
                            $repositoryNodes = @($nuspec.SelectNodes("//*[local-name()='repository']"))
                            if ($repositoryNodes.Count -eq 1) {
                                $repositoryNodes[0].SetAttribute('commit', $RepositoryCommit)
                            }
                            $bytes = Convert-XmlToBytes -Xml $nuspec
                        }
                        elseif ($entryName.EndsWith('.psmdcp', [System.StringComparison]::OrdinalIgnoreCase)) {
                            [xml] $coreProperties = Convert-BytesToXml -Bytes $bytes
                            Set-XmlElementValue -Xml $coreProperties -LocalName 'version' -Value $target.PackageVersion
                            $bytes = Convert-XmlToBytes -Xml $coreProperties
                        }
                        elseif (-not $VersionOnly -and $entryName -eq '[Content_Types].xml') {
                            [xml] $contentTypes = Convert-BytesToXml -Bytes $bytes
                            $namespace = $contentTypes.DocumentElement.NamespaceURI
                            $override = $contentTypes.CreateElement('Override', $namespace)
                            [void] $override.SetAttribute('PartName', "/$AdditionalPackagePath")
                            [void] $override.SetAttribute('ContentType', 'application/octet')
                            [void] $contentTypes.DocumentElement.AppendChild($override)
                            $bytes = Convert-XmlToBytes -Xml $contentTypes
                        }

                        $targetEntry = $targetZip.CreateEntry($entryName, [System.IO.Compression.CompressionLevel]::Optimal)
                        $targetEntry.LastWriteTime = $sourceEntry.LastWriteTime
                        $entryStream = $targetEntry.Open()
                        try {
                            $entryStream.Write($bytes, 0, $bytes.Length)
                        }
                        finally {
                            $entryStream.Dispose()
                        }
                    }

                    if (-not $VersionOnly) {
                        $additionalBytes = [System.IO.File]::ReadAllBytes($additionalFile)
                        $additionalEntry = $targetZip.CreateEntry($AdditionalPackagePath, [System.IO.Compression.CompressionLevel]::Optimal)
                        $additionalStream = $additionalEntry.Open()
                        try {
                            $additionalStream.Write($additionalBytes, 0, $additionalBytes.Length)
                        }
                        finally {
                            $additionalStream.Dispose()
                        }
                    }
                }
                finally {
                    $targetZip.Dispose()
                }
            }
            finally {
                $targetStream.Dispose()
            }
        }
        catch {
            if (Test-Path -LiteralPath $targetPath) {
                Remove-Item -LiteralPath $targetPath -Force
            }
            throw
        }
        finally {
            $sourceZip.Dispose()
        }

        $rows += [pscustomobject]@{
            Package = $target.Id
            SourceVersion = $previous.PackageVersion
            TargetVersion = $target.PackageVersion
            Mode = if ($VersionOnly) { 'version-only' } else { 'content-add' }
            Output = $targetPath
        }
    }
}
finally {
    if ($tempRoot -and (Test-Path -LiteralPath $tempRoot)) {
        $tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-native-repack-input-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary repack input path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

$rows | Format-Table -AutoSize
Write-Host "Created $($rows.Count) selective native package repack(s)."
