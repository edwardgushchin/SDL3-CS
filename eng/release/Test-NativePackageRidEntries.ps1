#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}

$rids = @($manifest.rids | ForEach-Object { $_.rid })
$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

foreach ($componentId in $Components) {
    $component = Get-ReleaseComponent -Manifest $manifest -Component $componentId
    $packageProject = Resolve-ReleasePath $component.packageProject

    try {
        [xml] $projectXml = Get-Content -LiteralPath $packageProject -Raw -Encoding UTF8
        if ($projectXml.Project -eq $null) {
            throw "$($component.packageProject) does not have a Project root element."
        }

        foreach ($rid in $rids) {
            $include = "lib\$rid\*"
            $packagePath = "runtimes\$rid\native\"
            $matches = @($projectXml.Project.ItemGroup.None | Where-Object {
                $_.Include -eq $include -and
                $_.PackagePath -eq $packagePath -and
                ($_.Pack -eq 'True' -or $_.Pack -eq 'true')
            })

            if ($matches.Count -ne 1) {
                $errors.Add("$($component.packageProject) must pack '$include' to '$packagePath' exactly once; found $($matches.Count).")
            }
        }

        $unexpectedRidEntries = @($projectXml.Project.ItemGroup.None | Where-Object {
            $_.Include -like 'lib\*\*' -and $_.PackagePath -like 'runtimes\*\native\'
        } | ForEach-Object {
            $includeRid = $_.Include.Substring(4)
            $includeRid = $includeRid.Substring(0, $includeRid.IndexOf('\'))
            $packageRid = $_.PackagePath.Substring(9)
            $packageRid = $packageRid.Substring(0, $packageRid.IndexOf('\'))
            if ($includeRid -ne $packageRid -or $rids -notcontains $includeRid) {
                [pscustomobject]@{
                    Include = $_.Include
                    PackagePath = $_.PackagePath
                    IncludeRid = $includeRid
                    PackageRid = $packageRid
                }
            }
        })

        foreach ($entry in $unexpectedRidEntries) {
            $errors.Add("$($component.packageProject) has unexpected RID pack entry Include='$($entry.Include)' PackagePath='$($entry.PackagePath)'.")
        }

        $rows.Add([pscustomobject]@{
            Component = $component.id
            PackageId = $component.packageId
            Rids = $rids.Count
            Status = if ($unexpectedRidEntries.Count -eq 0) { 'checked' } else { 'invalid' }
        })
    }
    catch {
        $errors.Add($_.Exception.Message)
        $rows.Add([pscustomobject]@{
            Component = $component.id
            PackageId = $component.packageId
            Rids = $rids.Count
            Status = 'invalid'
        })
    }
}

$rows | Sort-Object Component | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native package RID pack entry validation failed with $($errors.Count) error(s)."
}

Write-Host "Native package RID pack entries are valid for $($Components.Count) package project(s) and $($rids.Count) RID(s)."
