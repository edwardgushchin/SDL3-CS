#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $AllowDirtySources
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Convert-ReceiptRelativePath {
    param(
        [AllowNull()]
        [string] $Path
    )

    if ([string]::IsNullOrWhiteSpace($Path)) {
        return $Path
    }

    return $Path.Replace('\', '/')
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

foreach ($componentId in $Components) {
    $componentInfo = Get-ReleaseComponent -Manifest $manifest -Component $componentId
    $packageProject = Resolve-ReleasePath $componentInfo.packageProject
    $packageRoot = Split-Path -Parent $packageProject

    foreach ($rid in $Rids) {
        $receiptPath = Get-ReleaseNativeReceiptPath -Manifest $manifest -Component $componentId -Rid $rid
        if (-not (Test-Path -LiteralPath $receiptPath -PathType Leaf)) {
            $errors.Add("Missing native build receipt for $componentId/$rid`: $receiptPath")
            $rows.Add([pscustomobject]@{
                Component = $componentId
                Rid = $rid
                Status = 'missing'
                Receipt = $receiptPath
                Artifacts = 0
            })
            continue
        }

        $receipt = Get-Content -LiteralPath $receiptPath -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 32
        $status = 'passed'
        $collectedPackagePaths = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
        $receiptArtifactPaths = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)

        if (-not $receipt.PSObject.Properties.Name.Contains('SchemaVersion') -or [int]$receipt.SchemaVersion -lt 2) {
            $errors.Add("Receipt $componentId/$rid was created with an old schema and has no install collection validation.")
            $status = 'failed'
        }
        if ($receipt.Component -ne $componentId) {
            $errors.Add("Receipt component mismatch for $componentId/$rid`: $($receipt.Component)")
            $status = 'failed'
        }
        if ($receipt.Rid -ne $rid) {
            $errors.Add("Receipt RID mismatch for $componentId/$rid`: $($receipt.Rid)")
            $status = 'failed'
        }
        if (-not $receipt.PSObject.Properties.Name.Contains('InstallCollection') -or @($receipt.InstallCollection).Count -eq 0) {
            $errors.Add("Receipt $componentId/$rid has no install collection evidence.")
            $status = 'failed'
        }
        else {
            foreach ($installArtifact in @($receipt.InstallCollection)) {
                if ($installArtifact.Disposition -notin @('collected', 'dependency')) {
                    $errors.Add("Receipt $componentId/$rid contains uncollected install runtime artifact: $($installArtifact.InstallRelativePath)")
                    $status = 'failed'
                }
                elseif ($installArtifact.Disposition -eq 'collected') {
                    if ([string]::IsNullOrWhiteSpace($installArtifact.PackageRelativePath)) {
                        $errors.Add("Receipt $componentId/$rid collected install artifact has no package relative path: $($installArtifact.InstallRelativePath)")
                        $status = 'failed'
                    }
                    else {
                        [void]$collectedPackagePaths.Add((Convert-ReceiptRelativePath $installArtifact.PackageRelativePath))
                    }
                }
            }
        }

        foreach ($sourceReference in @($receipt.SourceReferences)) {
            $sourceComponent = Get-ReleaseComponent -Manifest $manifest -Component $sourceReference.Component
            $sourcePath = Resolve-ReleasePath (Join-Path $manifest.sourceRoot $sourceComponent.sourceFolder)
            $currentHead = Invoke-ReleaseGitValue -RepositoryPath $sourcePath -Arguments @('rev-parse', 'HEAD')
            $dirtyCount = Get-ReleaseGitDirtyCount -RepositoryPath $sourcePath

            if ($currentHead -ne $sourceReference.Head) {
                $errors.Add("Receipt source head mismatch for $componentId/$rid source $($sourceReference.Component): receipt $($sourceReference.ShortHead), current $($currentHead.Substring(0, 12)).")
                $status = 'failed'
            }
            if (-not $AllowDirtySources -and $dirtyCount -ne 0) {
                $errors.Add("Source $($sourceReference.Component) is dirty while validating receipt $componentId/$rid.")
                $status = 'failed'
            }
        }

        foreach ($artifact in @($receipt.Artifacts)) {
            $artifactRelativePath = Convert-ReceiptRelativePath $artifact.RelativePath
            if (-not $collectedPackagePaths.Contains($artifactRelativePath)) {
                $errors.Add("Receipt artifact is not backed by collected install output for $componentId/$rid`: $artifactRelativePath")
                $status = 'failed'
            }
            [void]$receiptArtifactPaths.Add($artifactRelativePath)

            $artifactPath = Join-Path $packageRoot $artifact.RelativePath
            if (-not (Test-Path -LiteralPath $artifactPath -PathType Leaf)) {
                $errors.Add("Receipt artifact is missing for $componentId/$rid`: $($artifact.RelativePath)")
                $status = 'failed'
                continue
            }

            $fileInfo = Get-Item -LiteralPath $artifactPath
            if ($fileInfo.Length -ne [int64]$artifact.Length) {
                $errors.Add("Receipt artifact length mismatch for $componentId/$rid $($artifact.RelativePath).")
                $status = 'failed'
            }

            $sha256 = (Get-FileHash -LiteralPath $artifactPath -Algorithm SHA256).Hash.ToLowerInvariant()
            if ($sha256 -ne $artifact.Sha256) {
                $errors.Add("Receipt artifact hash mismatch for $componentId/$rid $($artifact.RelativePath).")
                $status = 'failed'
            }
        }

        foreach ($collectedPackagePath in $collectedPackagePaths) {
            if (-not $receiptArtifactPaths.Contains($collectedPackagePath)) {
                $errors.Add("Collected install artifact is missing from receipt artifacts for $componentId/$rid`: $collectedPackagePath")
                $status = 'failed'
            }
        }

        $packageRidRoot = Join-Path $packageRoot "lib\$rid"

        if (Test-Path -LiteralPath $packageRidRoot -PathType Container) {
            foreach ($packageFile in Get-ChildItem -LiteralPath $packageRidRoot -File -Recurse) {
                $actualRelativePath = Convert-ReceiptRelativePath ([System.IO.Path]::GetRelativePath($packageRoot, $packageFile.FullName))
                if (-not $receiptArtifactPaths.Contains($actualRelativePath)) {
                    $errors.Add("Package RID folder contains file not backed by receipt for $componentId/$rid`: $actualRelativePath")
                    $status = 'failed'
                }
            }
        }
        else {
            $errors.Add("Package RID folder is missing for $componentId/$rid`: $packageRidRoot")
            $status = 'failed'
        }

        $rows.Add([pscustomobject]@{
            Component = $componentId
            Rid = $rid
            Status = $status
            Receipt = $receiptPath
            Artifacts = @($receipt.Artifacts).Count
        })
    }
}

$rows | Sort-Object Component, Rid | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native build receipt validation failed with $($errors.Count) issue(s)."
}

Write-Host "Native build receipts are valid."
