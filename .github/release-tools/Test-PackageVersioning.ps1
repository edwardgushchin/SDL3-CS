#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Add-VersioningError {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:errors.Add($Message)
}

function Get-ProjectPropertyValue {
    param(
        [Parameter(Mandatory)]
        [xml] $ProjectXml,

        [Parameter(Mandatory)]
        [string] $Name
    )

    foreach ($propertyGroup in $ProjectXml.Project.PropertyGroup) {
        $node = $propertyGroup.SelectSingleNode($Name)
        if ($node -and $node.InnerText) {
            return $node.InnerText
        }
    }

    return $null
}

function Test-SemVerCore {
    param(
        [Parameter(Mandatory)]
        [string] $Version
    )

    return $Version -match '^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)$'
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}

$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

if ($PackageRevision -lt 0) {
    Add-VersioningError "PackageRevision must be zero or greater."
}

if (-not $manifest.PSObject.Properties.Name.Contains('versioning')) {
    Add-VersioningError "Manifest has no versioning section."
}
else {
    if ($manifest.versioning.packageVersionPattern -ne '{nativeVersion}.{packageRevision}') {
        Add-VersioningError "Manifest packageVersionPattern must be '{nativeVersion}.{packageRevision}', actual: '$($manifest.versioning.packageVersionPattern)'."
    }

    if ($manifest.versioning.wrapperVersionComponent -ne 'SDL') {
        Add-VersioningError "Manifest wrapperVersionComponent must be 'SDL', actual: '$($manifest.versioning.wrapperVersionComponent)'."
    }
}

foreach ($component in $manifest.components) {
    if (-not (Test-SemVerCore -Version $component.nativeVersion)) {
        Add-VersioningError "Component $($component.id) nativeVersion must be SemVer core x.y.z, actual: '$($component.nativeVersion)'."
    }
}

$packageRows = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision)
$nativePackageRows = @($packageRows | Where-Object { $_.Kind -eq 'native' } | ForEach-Object {
    $component = Get-ReleaseComponent -Manifest $manifest -Component $_.VersionComponent
    [pscustomobject]@{
        Id = $_.Id
        Project = $_.Project
        VersionComponent = $component.id
        ExpectedVersionPrefix = $component.nativeVersion
        ExpectedProjectPackageId = $_.ExpectedProjectPackageId
        Kind = 'native'
        NativePackagePlatform = $_.NativePackagePlatform
        Rids = @($_.Rids)
    }
})
$managedPackageRows = @($manifest.managedPackages | ForEach-Object {
    $managed = $_
    $component = Get-ReleaseComponent -Manifest $manifest -Component $managed.versionComponent
    [pscustomobject]@{
        Id = $managed.id
        Project = $managed.project
        VersionComponent = $component.id
        ExpectedVersionPrefix = $component.nativeVersion
        Kind = 'managed'
    }
})

foreach ($package in @($managedPackageRows + $nativePackageRows)) {
    $projectPath = Resolve-ReleasePath $package.Project
    if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
        Add-VersioningError "Package project not found for $($package.Id): $projectPath"
        continue
    }

    [xml] $projectXml = Get-Content -LiteralPath $projectPath -Raw -Encoding UTF8
    $packageId = Get-ProjectPropertyValue -ProjectXml $projectXml -Name 'PackageId'
    $versionPrefix = Get-ProjectPropertyValue -ProjectXml $projectXml -Name 'VersionPrefix'
    $explicitVersion = Get-ProjectPropertyValue -ProjectXml $projectXml -Name 'Version'

    $expectedProjectPackageId = if ($package.Kind -eq 'native') { $package.ExpectedProjectPackageId } else { $package.Id }
    if ($packageId -ne $expectedProjectPackageId) {
        Add-VersioningError "$($package.Project) PackageId '$packageId' does not match expected project package id '$expectedProjectPackageId' for computed package '$($package.Id)'."
    }

    if ($versionPrefix -ne $package.ExpectedVersionPrefix) {
        Add-VersioningError "$($package.Project) VersionPrefix '$versionPrefix' does not match $($package.VersionComponent) nativeVersion '$($package.ExpectedVersionPrefix)'."
    }

    if ($explicitVersion -and $explicitVersion -ne $versionPrefix) {
        Add-VersioningError "$($package.Project) Version '$explicitVersion' must match VersionPrefix '$versionPrefix' or be removed."
    }

    $computed = @($packageRows | Where-Object { $_.Id -eq $package.Id })
    if ($computed.Count -ne 1) {
        Add-VersioningError "Expected one computed package version row for $($package.Id), got $($computed.Count)."
        continue
    }

    $expectedPackageVersion = "$($package.ExpectedVersionPrefix).$PackageRevision"
    if ($computed[0].PackageVersion -ne $expectedPackageVersion) {
        Add-VersioningError "$($package.Id) computed PackageVersion '$($computed[0].PackageVersion)' does not match expected '$expectedPackageVersion'."
    }

    $rows.Add([pscustomobject]@{
        Id = $package.Id
        Kind = $package.Kind
        VersionComponent = $package.VersionComponent
        VersionPrefix = $versionPrefix
        PackageRevision = $PackageRevision
        PackageVersion = if ($computed.Count -eq 1) { $computed[0].PackageVersion } else { '' }
        ExpectedNupkg = if ($computed.Count -eq 1) { Get-ReleaseNuGetPackageFileName -Package $computed[0] } else { '' }
        Platform = if ($package.Kind -eq 'native') { $package.NativePackagePlatform } else { '' }
        Rids = if ($package.Kind -eq 'native') { (@($package.Rids) -join ',') } else { '' }
    })
}

$duplicatePackageVersions = @($packageRows | Group-Object Id | Where-Object { $_.Count -ne 1 })
foreach ($duplicate in $duplicatePackageVersions) {
    Add-VersioningError "Package '$($duplicate.Name)' has $($duplicate.Count) computed version rows."
}

$rows | Sort-Object Kind, Id | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Package versioning validation failed with $($errors.Count) error(s)."
}

Write-Host "Package versioning is valid."
