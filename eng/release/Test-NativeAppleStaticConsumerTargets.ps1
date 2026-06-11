#requires -Version 7.0
[CmdletBinding()]
param(
    [string[]] $Rids,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $ScratchRoot
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Get-AppleStaticFakeLibraryName {
    param(
        [Parameter(Mandatory)]
        [string] $Component
    )

    switch ($Component) {
        'SDL' { return 'libSDL3.a' }
        'SDL_image' { return 'libSDL3_image.a' }
        'SDL_mixer' { return 'libSDL3_mixer.a' }
        'SDL_ttf' { return 'libSDL3_ttf.a' }
        'SDL_shadercross' { return 'libSDL3_shadercross.a' }
        default { throw "Unsupported component for Apple static consumer smoke: $Component" }
    }
}

function Get-LinkerTokensForAppleRid {
    param(
        [Parameter(Mandatory)]
        [string] $Rid
    )

    $tokens = @(
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
        'ImageIO',
        'MobileCoreServices',
        '-lobjc',
        'CoreText',
        'CoreFoundation',
        '-lc++'
    )

    if ($Rid -like 'ios*') {
        $tokens += 'CoreMotion'
    }

    return $tokens
}

function Convert-NativeReferenceLine {
    param(
        [Parameter(Mandatory)]
        [string] $Line
    )

    $parts = $Line -split '\|', 5
    if ($parts.Count -lt 5) {
        throw "Unexpected NativeReference dump line: $Line"
    }

    return [pscustomobject]@{
        Identity = $parts[0]
        Leaf = [System.IO.Path]::GetFileName($parts[0].Replace('/', [System.IO.Path]::DirectorySeparatorChar))
        Kind = $parts[1]
        ForceLoad = $parts[2]
        SmartLink = $parts[3]
        IsCxx = $parts[4]
    }
}

function Assert-Condition {
    param(
        [Parameter(Mandatory)]
        [bool] $Condition,

        [Parameter(Mandatory)]
        [string] $Message
    )

    if (-not $Condition) {
        throw $Message
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$appleRids = @($manifest.rids | Where-Object { $_.os -in @('ios', 'tvos') } | ForEach-Object { $_.rid })
if (-not $Rids -or $Rids.Count -eq 0) {
    $Rids = $appleRids
}

foreach ($rid in $Rids) {
    if ($rid -notin $appleRids) {
        throw "RID '$rid' is not an Apple static RID in release manifest."
    }
}

if (-not $ScratchRoot) {
    $ScratchRoot = Join-Path (Resolve-ReleasePath $manifest.artifactsRoot) 'apple-static-consumer-targets-smoke'
}

$runRoot = Join-Path (Resolve-ReleasePath $ScratchRoot) ([System.Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Force -Path $runRoot | Out-Null

$components = @($manifest.components)
$rows = New-Object System.Collections.Generic.List[object]
$errors = New-Object System.Collections.Generic.List[string]

foreach ($rid in $Rids) {
    $ridRoot = Join-Path $runRoot $rid
    $packageRoot = Join-Path $ridRoot 'packages'
    New-Item -ItemType Directory -Force -Path $packageRoot | Out-Null

    foreach ($component in $components) {
        $sourceProject = Resolve-ReleasePath $component.packageProject
        $sourcePackageRoot = Split-Path -Parent $sourceProject
        $sourceTargets = Join-Path $sourcePackageRoot "buildTransitive\$($component.packageId).targets"
        if (-not (Test-Path -LiteralPath $sourceTargets -PathType Leaf)) {
            throw "Missing source targets for $($component.id): $sourceTargets"
        }

        $tempComponentPackageRoot = Join-Path $packageRoot $component.packageId
        $tempTargetsRoot = Join-Path $tempComponentPackageRoot 'buildTransitive'
        $tempNativeRoot = Join-Path $tempComponentPackageRoot "runtimes\$rid\native"
        New-Item -ItemType Directory -Force -Path $tempTargetsRoot, $tempNativeRoot | Out-Null
        Copy-Item -LiteralPath $sourceTargets -Destination (Join-Path $tempTargetsRoot "$($component.packageId).targets") -Force

        $fakeLibraryPath = Join-Path $tempNativeRoot (Get-AppleStaticFakeLibraryName -Component $component.id)
        Set-Content -LiteralPath $fakeLibraryPath -Value '' -Encoding ascii
    }

    $imports = @($components | ForEach-Object {
        "  <Import Project=`"packages\$($_.packageId)\buildTransitive\$($_.packageId).targets`" />"
    }) -join [Environment]::NewLine

    $consumerProject = @"
<Project>
  <PropertyGroup>
    <RuntimeIdentifier>$rid</RuntimeIdentifier>
    <OutputRoot>`$(MSBuildProjectDirectory)\out</OutputRoot>
  </PropertyGroup>
$imports
  <Target Name="DumpSdl3CsNativeItems">
    <MakeDir Directories="`$(OutputRoot)" />
    <WriteLinesToFile File="`$(OutputRoot)\native-references.txt" Lines="@(NativeReference->'%(Identity)|%(Kind)|%(ForceLoad)|%(SmartLink)|%(IsCxx)')" Overwrite="true" />
    <WriteLinesToFile File="`$(OutputRoot)\linker-arguments.txt" Lines="@(LinkerArgument)" Overwrite="true" />
  </Target>
</Project>
"@

    $consumerProjectPath = Join-Path $ridRoot 'Consumer.proj'
    Set-Content -LiteralPath $consumerProjectPath -Value $consumerProject -Encoding UTF8

    $msbuildOutput = & dotnet msbuild $consumerProjectPath /nologo /t:DumpSdl3CsNativeItems /p:RuntimeIdentifier=$rid /v:minimal
    if ($LASTEXITCODE -ne 0) {
        $errors.Add("MSBuild consumer smoke failed for $rid with exit code $LASTEXITCODE`: $($msbuildOutput -join [Environment]::NewLine)")
        continue
    }

    $nativeDumpPath = Join-Path $ridRoot 'out\native-references.txt'
    $linkerDumpPath = Join-Path $ridRoot 'out\linker-arguments.txt'
    $nativeLines = @()
    $linkerLines = @()
    if (Test-Path -LiteralPath $nativeDumpPath -PathType Leaf) {
        $nativeLines = @(Get-Content -LiteralPath $nativeDumpPath)
    }
    if (Test-Path -LiteralPath $linkerDumpPath -PathType Leaf) {
        $linkerLines = @(Get-Content -LiteralPath $linkerDumpPath)
    }

    try {
        $nativeRows = @($nativeLines | Where-Object { $_ } | ForEach-Object { Convert-NativeReferenceLine -Line $_ })
        Assert-Condition -Condition ($nativeRows.Count -eq $components.Count) -Message "Expected $($components.Count) NativeReference item(s) for $rid, got $($nativeRows.Count)."

        foreach ($component in $components) {
            $expectedLeaf = Get-AppleStaticFakeLibraryName -Component $component.id
            $matches = @($nativeRows | Where-Object { $_.Leaf -eq $expectedLeaf })
            Assert-Condition -Condition ($matches.Count -eq 1) -Message "Expected one NativeReference for $expectedLeaf on $rid, got $($matches.Count)."

            $match = $matches[0]
            Assert-Condition -Condition ($match.Kind -eq 'Static') -Message "NativeReference $expectedLeaf on $rid has Kind '$($match.Kind)', expected Static."
            Assert-Condition -Condition ($match.ForceLoad -eq 'true') -Message "NativeReference $expectedLeaf on $rid has ForceLoad '$($match.ForceLoad)', expected true."
            Assert-Condition -Condition ($match.SmartLink -eq 'true') -Message "NativeReference $expectedLeaf on $rid has SmartLink '$($match.SmartLink)', expected true."

            if ($component.id -eq 'SDL_shadercross') {
                Assert-Condition -Condition ($match.IsCxx -eq 'true') -Message "NativeReference $expectedLeaf on $rid has IsCxx '$($match.IsCxx)', expected true."
            }
        }

        foreach ($token in (Get-LinkerTokensForAppleRid -Rid $rid)) {
            Assert-Condition -Condition ($token -in $linkerLines) -Message "Expected LinkerArgument '$token' for $rid."
        }

        if ($rid -like 'tvos*') {
            Assert-Condition -Condition ('CoreMotion' -notin $linkerLines) -Message "CoreMotion must not be linked for tvOS RID $rid."
        }

        $rows.Add([pscustomobject]@{
            Rid = $rid
            NativeReferences = $nativeRows.Count
            LinkerArguments = $linkerLines.Count
            Status = 'valid'
            Scratch = $ridRoot
        })
    }
    catch {
        $errors.Add($_.Exception.Message)
        $rows.Add([pscustomobject]@{
            Rid = $rid
            NativeReferences = $nativeLines.Count
            LinkerArguments = $linkerLines.Count
            Status = 'invalid'
            Scratch = $ridRoot
        })
    }
}

$rows | Sort-Object Rid | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Apple static consumer targets smoke failed with $($errors.Count) error(s)."
}

Write-Host "Apple static consumer targets smoke passed."
