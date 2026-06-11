#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string[]] $BundlePath,

    [string[]] $Components,
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Read-NativeBundleManifest {
    param(
        [Parameter(Mandatory)]
        [string] $Path
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $archive = [System.IO.Compression.ZipFile]::OpenRead($Path)
    try {
        $entry = @($archive.Entries | Where-Object { $_.FullName -eq 'bundle-manifest.json' })
        if ($entry.Count -ne 1) {
            throw "Bundle '$Path' must contain exactly one bundle-manifest.json entry; found $($entry.Count)."
        }

        $reader = [System.IO.StreamReader]::new($entry[0].Open())
        try {
            return ($reader.ReadToEnd() | ConvertFrom-Json -Depth 32)
        }
        finally {
            $reader.Dispose()
        }
    }
    finally {
        $archive.Dispose()
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = @($manifest.rids | ForEach-Object { $_.rid })
}

foreach ($component in $Components) {
    Get-ReleaseComponent -Manifest $manifest -Component $component | Out-Null
}
foreach ($rid in $Rids) {
    Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
}

$expectedKeys = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
foreach ($component in $Components) {
    foreach ($rid in $Rids) {
        [void]$expectedKeys.Add("$component/$rid")
    }
}

$knownKeys = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
foreach ($component in @($manifest.components | ForEach-Object { $_.id })) {
    foreach ($rid in @($manifest.rids | ForEach-Object { $_.rid })) {
        [void]$knownKeys.Add("$component/$rid")
    }
}

$errors = New-Object System.Collections.Generic.List[string]
$fileRows = New-Object System.Collections.Generic.List[object]
$bundleRows = New-Object System.Collections.Generic.List[object]

foreach ($path in $BundlePath) {
    $resolvedPath = Resolve-ReleasePath $path
    if (-not (Test-Path -LiteralPath $resolvedPath -PathType Leaf)) {
        $errors.Add("Native bundle was not found: $resolvedPath")
        continue
    }

    try {
        $bundleManifest = Read-NativeBundleManifest -Path $resolvedPath
        if ($bundleManifest.SchemaVersion -ne 1) {
            $errors.Add("Unsupported bundle schema version in '$resolvedPath': $($bundleManifest.SchemaVersion)")
            continue
        }

        foreach ($component in @($bundleManifest.Components)) {
            try {
                Get-ReleaseComponent -Manifest $manifest -Component $component | Out-Null
            }
            catch {
                $errors.Add("Bundle '$resolvedPath' references unknown component '$component'.")
            }
        }
        foreach ($rid in @($bundleManifest.Rids)) {
            try {
                Get-ReleaseRid -Manifest $manifest -Rid $rid | Out-Null
            }
            catch {
                $errors.Add("Bundle '$resolvedPath' references unknown RID '$rid'.")
            }
        }

        $bundleRows.Add([pscustomobject]@{
            Bundle = $resolvedPath
            Components = (@($bundleManifest.Components) -join ',')
            Rids = (@($bundleManifest.Rids) -join ',')
            Files = @($bundleManifest.Files).Count
        })

        foreach ($file in @($bundleManifest.Files)) {
            if (-not $file.Component -or -not $file.Rid -or -not $file.Kind -or -not $file.RelativePath) {
                $errors.Add("Bundle '$resolvedPath' contains a file entry with missing Component/Rid/Kind/RelativePath.")
                continue
            }

            $key = "$($file.Component)/$($file.Rid)"
            $isKnownKey = $knownKeys.Contains($key)

            if (-not $isKnownKey) {
                $errors.Add("Bundle '$resolvedPath' contains unknown component/RID '$key'.")
            }
            if ($file.Kind -notin @('artifact', 'receipt')) {
                $errors.Add("Bundle '$resolvedPath' contains unsupported file kind '$($file.Kind)' for '$key'.")
            }

            $lengthValue = [int64]0
            if ($null -eq $file.Length -or -not [int64]::TryParse([string]$file.Length, [ref]$lengthValue) -or $lengthValue -lt 0) {
                $errors.Add("Bundle '$resolvedPath' contains invalid Length for '$key': $($file.Length)")
            }
            if (-not $file.Sha256 -or $file.Sha256 -notmatch '^[0-9a-fA-F]{64}$') {
                $errors.Add("Bundle '$resolvedPath' contains invalid Sha256 for '$key': $($file.Sha256)")
            }

            try {
                Assert-ReleaseSafeRelativePath -RelativePath $file.RelativePath | Out-Null
            }
            catch {
                $errors.Add("Bundle '$resolvedPath' contains unsafe relative path '$($file.RelativePath)': $($_.Exception.Message)")
            }

            if (-not $expectedKeys.Contains($key)) {
                continue
            }

            $fileRows.Add([pscustomobject]@{
                Bundle = $resolvedPath
                Component = $file.Component
                Rid = $file.Rid
                Kind = $file.Kind
                RelativePath = $file.RelativePath
                Length = $file.Length
                Sha256 = $file.Sha256
            })
        }
    }
    catch {
        $errors.Add($_.Exception.Message)
    }
}

$bundleRows | Sort-Object Bundle | Format-Table -AutoSize

foreach ($key in @($expectedKeys | Sort-Object)) {
    $parts = $key -split '/', 2
    $component = $parts[0]
    $rid = $parts[1]
    $entries = @($fileRows | Where-Object { $_.Component -eq $component -and $_.Rid -eq $rid })
    $receipts = @($entries | Where-Object { $_.Kind -eq 'receipt' })
    $artifacts = @($entries | Where-Object { $_.Kind -eq 'artifact' })

    if ($receipts.Count -ne 1) {
        $errors.Add("Expected exactly one receipt entry for $key across bundle set; found $($receipts.Count).")
    }
    if ($artifacts.Count -eq 0) {
        $errors.Add("Expected at least one artifact entry for $key across bundle set; found 0.")
    }

    $duplicatePaths = @($entries | Group-Object RelativePath | Where-Object { $_.Count -gt 1 })
    foreach ($duplicatePath in $duplicatePaths) {
        $errors.Add("Duplicate bundle file path for $key`: $($duplicatePath.Name)")
    }
}

$summary = @($fileRows |
    Group-Object Component, Rid |
    ForEach-Object {
        $first = $_.Group[0]
        [pscustomobject]@{
            Component = $first.Component
            Rid = $first.Rid
            Files = $_.Count
            Receipts = @($_.Group | Where-Object { $_.Kind -eq 'receipt' }).Count
            Artifacts = @($_.Group | Where-Object { $_.Kind -eq 'artifact' }).Count
        }
    })

$summary | Sort-Object Component, Rid | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native bundle set validation failed with $($errors.Count) issue(s)."
}

Write-Host "Native bundle set is valid for $($Components.Count) component(s), $($Rids.Count) RID(s), and $($BundlePath.Count) bundle file(s)."
