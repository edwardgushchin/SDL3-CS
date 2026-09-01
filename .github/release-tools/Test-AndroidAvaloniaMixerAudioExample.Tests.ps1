#requires -Version 7.0
[CmdletBinding()]
param()

$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$exampleRoot = Join-Path $repoRoot 'SDL3-CS.Examples/Android/AndroidAvaloniaMixerAudio'
$projectPath = Join-Path $exampleRoot 'AndroidAvaloniaMixerAudio.csproj'
$mainActivityPath = Join-Path $exampleRoot 'MainActivity.cs'
$mixerAudioPath = Join-Path $exampleRoot 'MixerAudio.cs'
$targetsPath = Join-Path $repoRoot 'SDL3-CS.Examples/Directory.Build.targets'
$solutionPath = Join-Path $repoRoot 'SDL3-CS.sln'
$ciPath = Join-Path $repoRoot '.github/workflows/ci.yml'
$inventoryValidator = Join-Path $PSScriptRoot 'Test-ExampleProjects.ps1'

function Assert-FileExists {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $Description
    )

    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
        throw "$Description was not found: $Path"
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

Assert-FileExists -Path $projectPath -Description 'Avalonia SDL_mixer Android example project'
Assert-FileExists -Path $mainActivityPath -Description 'Avalonia SDL_mixer MainActivity'
Assert-FileExists -Path $mixerAudioPath -Description 'Avalonia SDL_mixer resource owner'

[xml] $projectXml = Get-Content -LiteralPath $projectPath -Raw -Encoding UTF8
$targetFramework = [string]($projectXml.Project.PropertyGroup.TargetFramework | Select-Object -First 1)
if ($targetFramework -ne 'net10.0-android') {
    throw "Android Avalonia SDL_mixer example must target net10.0-android, actual: $targetFramework"
}

$useMixer = [string]($projectXml.Project.PropertyGroup.SDL3CSExampleUseMixer | Select-Object -First 1)
if ($useMixer -ne 'true') {
    throw 'Android Avalonia SDL_mixer example must set SDL3CSExampleUseMixer=true.'
}

$avaloniaReference = @($projectXml.SelectNodes("//PackageReference[@Include='Avalonia.Android']"))
if ($avaloniaReference.Count -ne 1) {
    throw 'Android Avalonia SDL_mixer example must reference Avalonia.Android exactly once.'
}

[xml] $targetsXml = Get-Content -LiteralPath $targetsPath -Raw -Encoding UTF8
$androidMixerReferences = @($targetsXml.SelectNodes("//PackageReference[@Include='SDL3-CS.Android.Mixer']"))
if ($androidMixerReferences.Count -ne 1) {
    throw 'Android example infrastructure must add SDL3-CS.Android.Mixer exactly once.'
}

$androidMixerReference = $androidMixerReferences[0]
if (-not $androidMixerReference.Condition.Contains('SDL3CSExampleUseMixer', [System.StringComparison]::Ordinal)) {
    throw 'SDL3-CS.Android.Mixer package reference must be conditioned on SDL3CSExampleUseMixer.'
}
if (-not $androidMixerReference.ParentNode.Condition.Contains('SDL3CSExampleUseAndroidRuntime', [System.StringComparison]::Ordinal)) {
    throw 'SDL3-CS.Android.Mixer package reference must stay inside the Android runtime ItemGroup.'
}

$mainActivityText = Get-Content -LiteralPath $mainActivityPath -Raw -Encoding UTF8
foreach ($expectation in @(
    'class MainActivity : AvaloniaMainActivity',
    'JavaSystem.LoadLibrary("SDL3")',
    'AndroidSdl.SetupJNI();',
    'AndroidSdl.Initialize();',
    'AndroidSdl.Context = this;',
    '_audio?.Pause();',
    '_audio?.Resume();',
    '_audio?.Dispose();',
    'AndroidSdl.Context = null;'
)) {
    Assert-TextContains -Text $mainActivityText -Expected $expectation -Description 'MainActivity lifecycle contract'
}

$mixerAudioText = Get-Content -LiteralPath $mixerAudioPath -Raw -Encoding UTF8
foreach ($expectation in @(
    'Mixer.LoadAudioIO',
    'SDL.IOFromConstMem',
    'finally',
    'SDL.GetError()',
    'Mixer.DestroyTrack',
    'Mixer.DestroyAudio',
    'Mixer.DestroyMixer',
    'Mixer.Quit()',
    'SDL.QuitSubSystem(SDL.InitFlags.Audio)'
)) {
    Assert-TextContains -Text $mixerAudioText -Expected $expectation -Description 'MixerAudio ownership contract'
}

$callbackIndex = $mixerAudioText.IndexOf('Mixer.SetTrackStoppedCallback', [System.StringComparison]::Ordinal)
$playIndex = $mixerAudioText.IndexOf('Mixer.PlayTrack', [System.StringComparison]::Ordinal)
if ($callbackIndex -lt 0 -or $playIndex -lt 0 -or $callbackIndex -gt $playIndex) {
    throw 'MixerAudio must register the stopped callback before starting playback.'
}

& $inventoryValidator -ExamplesRoot (Join-Path $repoRoot 'SDL3-CS.Examples') -SolutionPath $solutionPath -ValidateOnly -Scope All
if ($null -ne $LASTEXITCODE -and $LASTEXITCODE -ne 0) {
    throw "Example inventory validation failed with exit code $LASTEXITCODE."
}

$ciText = Get-Content -LiteralPath $ciPath -Raw -Encoding UTF8
Assert-TextContains `
    -Text $ciText `
    -Expected './.github/release-tools/Test-AndroidAvaloniaMixerAudioExample.Tests.ps1' `
    -Description 'Android example CI contract test invocation'

Write-Host 'Android Avalonia SDL_mixer example contract tests passed.'
