#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Components,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Assert-TextContains {
    param(
        [Parameter(Mandatory)]
        [string] $Text,

        [Parameter(Mandatory)]
        [string] $Expected,

        [Parameter(Mandatory)]
        [string] $Context
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        throw "$Context is missing '$Expected'."
    }
}

function Get-ExpectedLinkerTokens {
    param(
        [Parameter(Mandatory)]
        [string] $Component
    )

    switch ($Component) {
        'SDL' {
            return @(
                'Foundation',
                'CoreVideo',
                'CoreMedia',
                'CoreAudio',
                'AudioToolbox',
                'AVFoundation',
                'CoreGraphics',
                'QuartzCore',
                'UIKit',
                'GameController',
                'Metal',
                'OpenGLES',
                'CoreHaptics',
                'CoreMotion'
            )
        }
        'SDL_image' {
            return @(
                'CoreGraphics',
                'Foundation',
                'ImageIO',
                'MobileCoreServices',
                'UIKit',
                '-lobjc'
            )
        }
        'SDL_mixer' {
            return @()
        }
        'SDL_ttf' {
            return @(
                'CoreText',
                'CoreGraphics',
                'CoreFoundation'
            )
        }
        'SDL_shadercross' {
            return @('-lc++')
        }
        default {
            throw "Unsupported component for package targets validation: $Component"
        }
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $Components -or $Components.Count -eq 0) {
    $Components = @($manifest.components | ForEach-Object { $_.id })
}

$appleRids = @($manifest.rids | Where-Object { $_.os -in @('ios', 'tvos') } | ForEach-Object { $_.rid })
$errors = New-Object System.Collections.Generic.List[string]
$rows = New-Object System.Collections.Generic.List[object]

foreach ($componentId in $Components) {
    $component = Get-ReleaseComponent -Manifest $manifest -Component $componentId
    $packageProject = Resolve-ReleasePath $component.packageProject
    $packageRoot = Split-Path -Parent $packageProject
    $targetsRelativePath = "buildTransitive\$($component.packageId).targets"
    $targetsPath = Join-Path $packageRoot $targetsRelativePath

    try {
        if (-not (Test-Path -LiteralPath $targetsPath -PathType Leaf)) {
            throw "Native package targets file is missing: $targetsPath"
        }

        [xml] $projectXml = Get-Content -LiteralPath $packageProject -Raw -Encoding UTF8
        $packedTargets = @($projectXml.Project.ItemGroup.None | Where-Object {
            $_.Include -eq $targetsRelativePath -and $_.Pack -eq 'true' -and $_.PackagePath -eq 'buildTransitive\'
        })
        if ($packedTargets.Count -ne 1) {
            throw "$($component.packageProject) must pack $targetsRelativePath to buildTransitive\ exactly once."
        }

        [xml] $targetsXml = Get-Content -LiteralPath $targetsPath -Raw -Encoding UTF8
        if ($targetsXml.Project -eq $null) {
            throw "$targetsPath does not have a Project root element."
        }

        $targetsText = Get-Content -LiteralPath $targetsPath -Raw -Encoding UTF8
        foreach ($rid in $appleRids) {
            Assert-TextContains -Text $targetsText -Expected $rid -Context $targetsPath
        }

        foreach ($token in @('NativeReference', '*.a', '<Kind>Static</Kind>', '<ForceLoad>true</ForceLoad>', '<SmartLink>true</SmartLink>')) {
            Assert-TextContains -Text $targetsText -Expected $token -Context $targetsPath
        }

        foreach ($linkerToken in (Get-ExpectedLinkerTokens -Component $component.id)) {
            Assert-TextContains -Text $targetsText -Expected $linkerToken -Context $targetsPath
        }

        if ($component.id -eq 'SDL_shadercross') {
            Assert-TextContains -Text $targetsText -Expected '<IsCxx>true</IsCxx>' -Context $targetsPath
        }

        $rows.Add([pscustomobject]@{
            Component = $component.id
            PackageId = $component.packageId
            Targets = $targetsRelativePath
            Status = 'valid'
        })
    }
    catch {
        $errors.Add($_.Exception.Message)
        $rows.Add([pscustomobject]@{
            Component = $component.id
            PackageId = $component.packageId
            Targets = $targetsRelativePath
            Status = 'invalid'
        })
    }
}

$rows | Sort-Object Component | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native package targets validation failed with $($errors.Count) error(s)."
}

Write-Host "Native package buildTransitive targets are valid."
