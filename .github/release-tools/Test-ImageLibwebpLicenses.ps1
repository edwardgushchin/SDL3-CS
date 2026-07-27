#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $RepositoryRoot = (Join-Path $PSScriptRoot '../..'),
    [string[]] $PackagePaths
)

$repoRoot = [System.IO.Path]::GetFullPath($RepositoryRoot)
$licenseRelativePath = 'SDL3-CS.NativePackages/ThirdPartyLicenses/libwebp/COPYING'
$licensePath = Join-Path $repoRoot $licenseRelativePath
$projectRelativePaths = @(
    'SDL3-CS.NativePackages/SDL3-CS.Android.Image/SDL3-CS.Android.Image.csproj',
    'SDL3-CS.NativePackages/SDL3-CS.Linux.Image/SDL3-CS.Linux.Image.csproj',
    'SDL3-CS.NativePackages/SDL3-CS.Windows.Image/SDL3-CS.Windows.Image.csproj'
)
$excludedProjectRelativePaths = @(
    'SDL3-CS.NativePackages/SDL3-CS.iOS.Image/SDL3-CS.iOS.Image.csproj',
    'SDL3-CS.NativePackages/SDL3-CS.MacOS.Image/SDL3-CS.MacOS.Image.csproj',
    'SDL3-CS.NativePackages/SDL3-CS.tvOS.Image/SDL3-CS.tvOS.Image.csproj'
)
$expectedInclude = '../ThirdPartyLicenses/libwebp/COPYING'
$expectedPackagePath = 'licenses/libwebp'
$expectedPackageEntry = 'licenses/libwebp/COPYING'
$errors = New-Object System.Collections.Generic.List[string]

function ConvertTo-NormalizedLicenseText {
    param([Parameter(Mandatory)][AllowEmptyString()][string] $Text)

    return $Text.Replace("`r`n", "`n").Replace("`r", "`n")
}

if (-not (Test-Path -LiteralPath $licensePath -PathType Leaf)) {
    $errors.Add("Missing libwebp license source: $licenseRelativePath")
}

foreach ($relativeProjectPath in $projectRelativePaths) {
    $projectPath = Join-Path $repoRoot $relativeProjectPath
    if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
        $errors.Add("Missing Image package project: $relativeProjectPath")
        continue
    }

    $projectText = Get-Content -LiteralPath $projectPath -Raw -Encoding UTF8
    $expectedItem = '<None Include="{0}" Pack="true" PackagePath="{1}" />' -f $expectedInclude, $expectedPackagePath
    if (-not $projectText.Contains($expectedItem, [System.StringComparison]::Ordinal)) {
        $errors.Add("$relativeProjectPath must pack libwebp license as $expectedPackageEntry")
    }
}

foreach ($relativeProjectPath in $excludedProjectRelativePaths) {
    $projectPath = Join-Path $repoRoot $relativeProjectPath
    if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
        $errors.Add("Missing excluded Image package project: $relativeProjectPath")
        continue
    }

    $projectText = Get-Content -LiteralPath $projectPath -Raw -Encoding UTF8
    if ($projectText.Contains($expectedInclude, [System.StringComparison]::Ordinal)) {
        $errors.Add("$relativeProjectPath must not pack libwebp license while WebP is disabled")
    }
}

if ($PackagePaths) {
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $expectedLicenseText = Get-Content -LiteralPath $licensePath -Raw -Encoding UTF8
    foreach ($packagePath in $PackagePaths) {
        $resolvedPackagePath = [System.IO.Path]::GetFullPath($packagePath)
        if (-not (Test-Path -LiteralPath $resolvedPackagePath -PathType Leaf)) {
            $errors.Add("Package does not exist: $resolvedPackagePath")
            continue
        }

        $zip = [System.IO.Compression.ZipFile]::OpenRead($resolvedPackagePath)
        try {
            $licenseEntry = $zip.GetEntry($expectedPackageEntry)
            if (-not $licenseEntry) {
                $errors.Add("Package is missing libwebp license entry: $resolvedPackagePath -> $expectedPackageEntry")
            }
            else {
                $stream = $licenseEntry.Open()
                try {
                    $reader = [System.IO.StreamReader]::new($stream, [System.Text.Encoding]::UTF8, $true)
                    try {
                        $actualLicenseText = $reader.ReadToEnd()
                        if ((ConvertTo-NormalizedLicenseText -Text $actualLicenseText) -ne
                            (ConvertTo-NormalizedLicenseText -Text $expectedLicenseText)) {
                            $errors.Add("Package libwebp license entry does not match source: $resolvedPackagePath -> $expectedPackageEntry")
                        }
                    }
                    finally {
                        $reader.Dispose()
                    }
                }
                finally {
                    $stream.Dispose()
                }
            }
        }
        finally {
            $zip.Dispose()
        }
    }
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "libwebp license package validation failed with $($errors.Count) error(s)."
}

Write-Host 'libwebp license package validation passed.'
