#requires -Version 7.0
[CmdletBinding()]
param()

$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$builder = Join-Path $PSScriptRoot 'New-NativePackageRepack.ps1'
$validator = Join-Path $PSScriptRoot 'Test-NativePackageRepack.ps1'
$publisher = Join-Path $PSScriptRoot 'Publish-NativePackageRepack.ps1'
$workflow = Join-Path $repoRoot '.github/workflows/release-native-packages.yml'
$manifest = Join-Path $PSScriptRoot 'release-manifest.json'

foreach ($requiredPath in @($builder, $validator, $publisher, $workflow, $manifest)) {
    if (-not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) {
        throw "Selective native repack dependency was not found: $requiredPath"
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

function Assert-ActionFails {
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
        throw "Expected selective native repack validation to fail: $Description"
    }
}

function New-PackageFixture {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $Version,
        [Parameter(Mandatory)][byte[]] $NativeBytes,
        [switch] $IncludeLibwebpLicense
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    if (Test-Path -LiteralPath $Path) {
        Remove-Item -LiteralPath $Path -Force
    }

    $stream = [System.IO.File]::Open($Path, [System.IO.FileMode]::CreateNew)
    try {
        $archive = [System.IO.Compression.ZipArchive]::new($stream, [System.IO.Compression.ZipArchiveMode]::Create, $false)
        try {
            $entries = [ordered]@{
                '[Content_Types].xml' = [System.Text.Encoding]::UTF8.GetBytes('<?xml version="1.0" encoding="utf-8"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="psmdcp" ContentType="application/vnd.openxmlformats-package.core-properties+xml"/><Default Extension="nuspec" ContentType="application/octet"/><Default Extension="dll" ContentType="application/octet"/><Default Extension="targets" ContentType="application/octet"/><Default Extension="p7s" ContentType="application/octet"/></Types>')
                'SDL3-CS.Windows.Image.nuspec' = [System.Text.Encoding]::UTF8.GetBytes(('<?xml version="1.0"?><package xmlns="http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd"><metadata><id>SDL3-CS.Windows.Image</id><version>{0}</version><repository type="git" url="https://github.com/edwardgushchin/SDL3-CS" commit="0000000000000000000000000000000000000000" /></metadata></package>' -f $Version))
                'package/services/metadata/core-properties/test.psmdcp' = [System.Text.Encoding]::UTF8.GetBytes(('<?xml version="1.0"?><coreProperties xmlns="http://schemas.openxmlformats.org/package/2006/metadata/core-properties"><version>{0}</version></coreProperties>' -f $Version))
                'runtimes/win-x64/native/SDL3_image.dll' = $NativeBytes
                'buildTransitive/SDL3-CS.Windows.Image.targets' = [System.Text.Encoding]::UTF8.GetBytes('<Project />')
                'LICENSE' = [System.Text.Encoding]::UTF8.GetBytes('SDL license')
                '.signature.p7s' = [System.Text.Encoding]::UTF8.GetBytes('old signature')
            }
            if ($IncludeLibwebpLicense) {
                $entries['licenses/libwebp/COPYING'] = [System.Text.Encoding]::UTF8.GetBytes('libwebp license')
            }

            foreach ($item in $entries.GetEnumerator()) {
                $entry = $archive.CreateEntry($item.Key)
                $entryStream = $entry.Open()
                try {
                    $entryStream.Write($item.Value, 0, $item.Value.Length)
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

function Get-ZipEntryText {
    param(
        [Parameter(Mandatory)][string] $PackagePath,
        [Parameter(Mandatory)][string] $EntryName
    )

    $archive = [System.IO.Compression.ZipFile]::OpenRead($PackagePath)
    try {
        $entry = $archive.GetEntry($EntryName)
        if (-not $entry) {
            throw "Package entry was not found: $EntryName"
        }
        $reader = [System.IO.StreamReader]::new($entry.Open(), [System.Text.Encoding]::UTF8)
        try {
            return $reader.ReadToEnd()
        }
        finally {
            $reader.Dispose()
        }
    }
    finally {
        $archive.Dispose()
    }
}

$workflowText = Get-Content -LiteralPath $workflow -Raw -Encoding UTF8
$selectiveStart = $workflowText.IndexOf('  selective-repack:', [System.StringComparison]::Ordinal)
if ($selectiveStart -lt 0) {
    throw 'Trusted release workflow is missing the selective-repack job.'
}
$selectiveText = $workflowText.Substring($selectiveStart)
foreach ($expectation in @(
    @{ Text = 'uses: NuGet/login@8d196754b4036150537f80ac539e15c2f1028841 # v1'; Description = 'SHA-pinned trusted publishing login' },
    @{ Text = './.github/release-tools/New-NativePackageRepack.ps1'; Description = 'package clone command' },
    @{ Text = './.github/release-tools/Publish-NativePackageRepack.ps1'; Description = 'selective publish command' },
    @{ Text = 'environment: production'; Description = 'protected publication environment' }
)) {
    Assert-TextContains -Text $selectiveText -Expected $expectation.Text -Description $expectation.Description
}
foreach ($forbidden in @('Build-Native.ps1', 'Invoke-NativeHostBuild.ps1', 'Invoke-ReleasePipeline.ps1', 'Pack-NuGet.ps1', '-SkipSourceSignatureValidation')) {
    if ($selectiveText.Contains($forbidden, [System.StringComparison]::Ordinal)) {
        throw "Package-only workflow must not invoke build/pack tooling: $forbidden"
    }
}
foreach ($modeExpectation in @(
    'selective_native_repack:',
    'publish_selective_nuget:',
    "throw 'Selective native repack requires managed_only=true",
    'if: ${{ inputs.managed_only && !inputs.selective_native_repack }}'
)) {
    Assert-TextContains -Text $workflowText -Expected $modeExpectation -Description 'fail-closed selective release mode'
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-native-repack-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary repack test path: $tempRoot"
}

$previousDir = Join-Path $tempRoot 'previous'
$targetDir = Join-Path $tempRoot 'target'
New-Item -ItemType Directory -Force -Path $previousDir, $targetDir | Out-Null
$previousPackage = Join-Path $previousDir 'SDL3-CS.Windows.Image.3.4.4.7.nupkg'
$targetPackage = Join-Path $targetDir 'SDL3-CS.Windows.Image.3.4.4.8.nupkg'

try {
    $unchangedBytes = [byte[]](1, 2, 3, 4, 5)
    New-PackageFixture -Path $previousPackage -Version '3.4.4.7' -NativeBytes $unchangedBytes

    & $builder `
        -PackageRevision 8 `
        -PreviousPackageRevision 7 `
        -PackageIds 'SDL3-CS.Windows.Image' `
        -OutputDir $targetDir `
        -PreviousPackageDir $previousDir `
        -RepositoryCommit ('a' * 40) `
        -SkipSourceSignatureValidation

    & $validator `
        -PackageRevision 8 `
        -PreviousPackageRevision 7 `
        -PackageIds 'SDL3-CS.Windows.Image' `
        -PackageDir $targetDir `
        -PreviousPackageDir $previousDir `
        -SkipTargetAvailabilityCheck

    $archive = [System.IO.Compression.ZipFile]::OpenRead($targetPackage)
    try {
        if ($archive.GetEntry('.signature.p7s')) {
            throw 'Repacked package must not preserve the source package signature.'
        }
        if (-not $archive.GetEntry('licenses/libwebp/COPYING')) {
            throw 'Repacked package must contain the libwebp license.'
        }
    }
    finally {
        $archive.Dispose()
    }
    Assert-TextContains -Text (Get-ZipEntryText -PackagePath $targetPackage -EntryName 'SDL3-CS.Windows.Image.nuspec') -Expected '<version>3.4.4.8</version>' -Description 'nuspec target version'
    Assert-TextContains -Text (Get-ZipEntryText -PackagePath $targetPackage -EntryName 'package/services/metadata/core-properties/test.psmdcp') -Expected '<version>3.4.4.8</version>' -Description 'core properties target version'

    Assert-ActionFails -Description 'unsafe additional package path' -Action {
        & $builder `
            -PackageRevision 8 `
            -PreviousPackageRevision 7 `
            -PackageIds 'SDL3-CS.Windows.Image' `
            -OutputDir $targetDir `
            -PreviousPackageDir $previousDir `
            -AdditionalPackagePath '../COPYING' `
            -RepositoryCommit ('a' * 40) `
            -SkipSourceSignatureValidation *> $null
    }

    New-PackageFixture -Path $targetPackage -Version '3.4.4.8' -NativeBytes ([byte[]](9, 2, 3, 4, 5)) -IncludeLibwebpLicense
    Assert-ActionFails -Description 'changed native binary' -Action {
        & $validator `
            -PackageRevision 8 `
            -PreviousPackageRevision 7 `
            -PackageIds 'SDL3-CS.Windows.Image' `
            -PackageDir $targetDir `
            -PreviousPackageDir $previousDir `
            -SkipTargetAvailabilityCheck *> $null
    }

    foreach ($invalidId in @('SDL3-CS', 'SDL3-CS.Does.Not.Exist')) {
        Assert-ActionFails -Description "invalid package id $invalidId" -Action {
            & $validator `
                -PackageRevision 8 `
                -PreviousPackageRevision 7 `
                -PackageIds $invalidId `
                -PackageDir $targetDir `
                -PreviousPackageDir $previousDir `
                -SkipTargetAvailabilityCheck *> $null
        }
    }
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-native-repack-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary repack test path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Selective native package repack tests passed.'
