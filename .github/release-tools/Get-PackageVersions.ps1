#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $Json
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}

$versions = Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision
if ($Json) {
    $versions | ConvertTo-Json -Depth 8
}
else {
    $versions | Sort-Object Kind, Id | Format-Table -AutoSize
}
