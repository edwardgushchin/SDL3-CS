#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)] [string] $WikiPath,
    [string] $HomePageName = ('SDL3' + [char]0x2010 + 'CS-Wiki'),
    [string] $ExpectedManagedVersion,
    [ValidatePattern('^[0-9a-fA-F]{40}$')]
    [string] $ExpectedSourceCommit,
    [string] $ExpectedGeneratedAtUtc,
    [ValidatePattern('^[0-9a-fA-F]{64}$')]
    [string] $ExpectedContentHash,
    [switch] $RequireExactPages
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$WikiPath = [System.IO.Path]::GetFullPath($WikiPath)
if (-not (Test-Path -LiteralPath $WikiPath)) {
    throw "WikiPath not found: $WikiPath"
}

$requiredPages = @(
    "$HomePageName.md",
    '_Sidebar.md',
    'API-Reference.md',
    'API-SDL.md',
    'API-Image.md',
    'API-Mixer.md',
    'API-TTF.md',
    'API-ShaderCross.md'
)

$errors = [System.Collections.Generic.List[string]]::new()
$metadataPattern = '(?m)^<!-- sdl3-cs-wiki managed-version="([^"]+)" source-commit="([0-9a-fA-F]{40})" generated-at="([^"]+)" -->\r?$'
$metadata = $null

function Get-WikiContentHash {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string[]] $FileNames
    )

    $manifest = [System.Text.StringBuilder]::new()
    foreach ($fileName in $FileNames | Sort-Object) {
        $filePath = Join-Path $Path $fileName
        if (-not (Test-Path -LiteralPath $filePath -PathType Leaf)) {
            continue
        }
        $canonicalContent = [System.IO.File]::ReadAllText($filePath).Replace("`r`n", "`n").Replace("`r", "`n")
        $fileHash = [Convert]::ToHexString([System.Security.Cryptography.SHA256]::HashData([System.Text.Encoding]::UTF8.GetBytes($canonicalContent))).ToLowerInvariant()
        [void] $manifest.Append($fileName).Append(':').Append($fileHash).Append("`n")
    }

    $manifestBytes = [System.Text.Encoding]::UTF8.GetBytes($manifest.ToString())
    return [Convert]::ToHexString([System.Security.Cryptography.SHA256]::HashData($manifestBytes)).ToLowerInvariant()
}

foreach ($page in $requiredPages) {
    $path = Join-Path $WikiPath $page
    if (-not (Test-Path -LiteralPath $path)) {
        $errors.Add("Missing required page: $page")
        continue
    }

    $item = Get-Item -LiteralPath $path
    if ($item.Length -lt 20) {
        $errors.Add("Page is unexpectedly small: $page")
    }

    $content = Get-Content -LiteralPath $path -Raw
    $match = [regex]::Match($content, $metadataPattern)
    if (-not $match.Success) {
        $errors.Add("Page is missing valid Wiki metadata: $page")
        continue
    }

    $pageMetadata = [pscustomobject]@{
        ManagedVersion = $match.Groups[1].Value
        SourceCommit = $match.Groups[2].Value.ToLowerInvariant()
        GeneratedAtUtc = $match.Groups[3].Value
    }
    $parsedTimestamp = [DateTimeOffset]::MinValue
    if (-not [DateTimeOffset]::TryParse($pageMetadata.GeneratedAtUtc, [ref] $parsedTimestamp)) {
        $errors.Add("Page has invalid generated-at timestamp: $page")
    }

    if ($null -eq $metadata) {
        $metadata = $pageMetadata
    }
    elseif ($pageMetadata.ManagedVersion -ne $metadata.ManagedVersion -or
        $pageMetadata.SourceCommit -ne $metadata.SourceCommit -or
        $pageMetadata.GeneratedAtUtc -ne $metadata.GeneratedAtUtc) {
        $errors.Add("Page metadata differs from the other managed pages: $page")
    }
}

if ($RequireExactPages) {
    $actualPages = @(Get-ChildItem -LiteralPath $WikiPath -Filter '*.md' -File | Select-Object -ExpandProperty Name | Sort-Object)
    $expectedPages = @($requiredPages | Sort-Object)
    foreach ($difference in Compare-Object -ReferenceObject $expectedPages -DifferenceObject $actualPages) {
        $errors.Add("Managed Wiki page set differs: $($difference.SideIndicator) $($difference.InputObject)")
    }
}

foreach ($page in $requiredPages | Where-Object { $_ -like 'API-*.md' -and $_ -ne 'API-Reference.md' }) {
    $path = Join-Path $WikiPath $page
    if (Test-Path -LiteralPath $path) {
        $content = Get-Content -LiteralPath $path -Raw
        if ($content -notmatch '(?m)^### `') {
            $errors.Add("API page has no function headings: $page")
        }
        if ($content -match 'TODO|TBD') {
            $errors.Add("API page contains TODO/TBD marker: $page")
        }
    }
}

$localLinks = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
foreach ($file in Get-ChildItem -LiteralPath $WikiPath -Filter '*.md') {
    $content = Get-Content -LiteralPath $file.FullName -Raw
    foreach ($match in [regex]::Matches($content, '\[[^\]]+\]\(([^)]+)\)')) {
        $target = $match.Groups[1].Value
        if ($target -match '^[a-z]+://' -or $target.StartsWith('#') -or $target.StartsWith('mailto:')) {
            continue
        }

        $targetPage = ($target -split '#')[0]
        if ([string]::IsNullOrWhiteSpace($targetPage)) {
            continue
        }
        if (-not $targetPage.EndsWith('.md', [System.StringComparison]::OrdinalIgnoreCase)) {
            $targetPage += '.md'
        }

        [void] $localLinks.Add($targetPage)
    }
}

foreach ($targetPage in $localLinks) {
    $targetPath = Join-Path $WikiPath $targetPage
    if (-not (Test-Path -LiteralPath $targetPath)) {
        $errors.Add("Broken local wiki link: $targetPage")
    }
}

if ($null -ne $metadata) {
    if ($ExpectedManagedVersion -and $metadata.ManagedVersion -ne $ExpectedManagedVersion) {
        $errors.Add("Managed version mismatch: expected $ExpectedManagedVersion, actual $($metadata.ManagedVersion)")
    }
    if ($ExpectedSourceCommit -and $metadata.SourceCommit -ne $ExpectedSourceCommit.ToLowerInvariant()) {
        $errors.Add("Source commit mismatch: expected $ExpectedSourceCommit, actual $($metadata.SourceCommit)")
    }
    if ($ExpectedGeneratedAtUtc) {
        $expectedTimestamp = [DateTimeOffset]::Parse($ExpectedGeneratedAtUtc).UtcDateTime.ToString('yyyy-MM-ddTHH:mm:ssZ')
        $actualTimestamp = [DateTimeOffset]::Parse($metadata.GeneratedAtUtc).UtcDateTime.ToString('yyyy-MM-ddTHH:mm:ssZ')
        if ($actualTimestamp -ne $expectedTimestamp) {
            $errors.Add("Generated-at mismatch: expected $expectedTimestamp, actual $actualTimestamp")
        }
    }
}

$contentHash = Get-WikiContentHash -Path $WikiPath -FileNames $requiredPages
if ($ExpectedContentHash -and $contentHash -ne $ExpectedContentHash.ToLowerInvariant()) {
    $errors.Add("Wiki content hash mismatch: expected $ExpectedContentHash, actual $contentHash")
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Wiki validation failed with $($errors.Count) error(s)."
}

[pscustomobject]@{
    WikiPath = $WikiPath
    Pages = @(Get-ChildItem -LiteralPath $WikiPath -Filter '*.md').Count
    RequiredPages = $requiredPages.Count
    LocalLinks = $localLinks.Count
    ManagedVersion = if ($null -ne $metadata) { $metadata.ManagedVersion } else { $null }
    SourceCommit = if ($null -ne $metadata) { $metadata.SourceCommit } else { $null }
    GeneratedAtUtc = if ($null -ne $metadata) { $metadata.GeneratedAtUtc } else { $null }
    ContentHash = $contentHash
    Status = 'Passed'
} | ConvertTo-Json -Depth 3 -Compress
