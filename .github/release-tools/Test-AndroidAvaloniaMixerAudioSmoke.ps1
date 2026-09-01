#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $AndroidSdkDirectory,
    [string] $JavaSdkDirectory,
    [string] $DeviceId,
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release',
    [switch] $KeepInstalled
)

$ErrorActionPreference = 'Stop'
$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$projectPath = Join-Path $repoRoot 'SDL3-CS.Examples/Android/AndroidAvaloniaMixerAudio/AndroidAvaloniaMixerAudio.csproj'
$adbFileName = if ($IsWindows) { 'adb.exe' } else { 'adb' }
$adbPath = Join-Path ([System.IO.Path]::GetFullPath($AndroidSdkDirectory)) "platform-tools/$adbFileName"
$applicationId = 'org.libsdl.example.avaloniamixeraudio'
$logTag = 'SDL3CS-AvaloniaAudio'

if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
    throw "Android Avalonia SDL_mixer example project was not found: $projectPath"
}
if (-not (Test-Path -LiteralPath $adbPath -PathType Leaf)) {
    throw "adb was not found: $adbPath"
}

$adbPrefix = @()
if ($DeviceId) {
    $adbPrefix = @('-s', $DeviceId)
}

function Invoke-Adb {
    param([Parameter(Mandatory)][string[]] $AdbArguments)

    $output = & $adbPath @adbPrefix @AdbArguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "adb failed with exit code $LASTEXITCODE`: $($AdbArguments -join ' ')`n$($output -join "`n")"
    }
    return @($output)
}

$deviceState = (Invoke-Adb -AdbArguments @('get-state') | Select-Object -Last 1).Trim()
if ($deviceState -ne 'device') {
    throw "Android device is not ready: $deviceState"
}

$abi = (Invoke-Adb -AdbArguments @('shell', 'getprop', 'ro.product.cpu.abi') | Select-Object -Last 1).Trim()
$runtimeIdentifier = switch ($abi) {
    'x86_64' { 'android-x64' }
    'x86' { 'android-x86' }
    'arm64-v8a' { 'android-arm64' }
    'armeabi-v7a' { 'android-arm' }
    default { throw "Unsupported Android ABI: $abi" }
}

$buildArguments = @(
    'build',
    $projectPath,
    '--configuration', $Configuration,
    '--nologo',
    "-p:RuntimeIdentifier=$runtimeIdentifier",
    "-p:AndroidSdkDirectory=$([System.IO.Path]::GetFullPath($AndroidSdkDirectory))"
)
if ($JavaSdkDirectory) {
    $buildArguments += "-p:JavaSdkDirectory=$([System.IO.Path]::GetFullPath($JavaSdkDirectory))"
}

& dotnet @buildArguments
if ($LASTEXITCODE -ne 0) {
    throw "Android Avalonia SDL_mixer example build failed with exit code $LASTEXITCODE."
}

$outputDirectory = Join-Path $repoRoot "SDL3-CS.Examples/Android/AndroidAvaloniaMixerAudio/bin/$Configuration/net10.0-android/$runtimeIdentifier"
$apk = @(Get-ChildItem -LiteralPath $outputDirectory -Filter '*-Signed.apk' -File | Sort-Object LastWriteTimeUtc -Descending)
if ($apk.Count -ne 1) {
    throw "Expected exactly one signed APK under $outputDirectory, found $($apk.Count)."
}

$installed = $false
try {
    Invoke-Adb -AdbArguments @('install', '--no-incremental', '-r', $apk[0].FullName) | Out-Host
    $installed = $true

    $componentLines = Invoke-Adb -AdbArguments @('shell', 'cmd', 'package', 'resolve-activity', '--brief', $applicationId)
    $component = @($componentLines | Where-Object { $_ -like "$applicationId/*" } | Select-Object -Last 1)
    if ($component.Count -ne 1) {
        throw "Unable to resolve launcher Activity for $applicationId.`n$($componentLines -join "`n")"
    }

    Invoke-Adb -AdbArguments @('shell', 'am', 'force-stop', $applicationId) | Out-Null
    Invoke-Adb -AdbArguments @('logcat', '-c') | Out-Null
    Invoke-Adb -AdbArguments @('shell', 'am', 'start', '-n', $component[0]) | Out-Host

    $deadline = [DateTimeOffset]::UtcNow.AddSeconds(25)
    $logText = ''
    while ([DateTimeOffset]::UtcNow -lt $deadline) {
        Start-Sleep -Milliseconds 250
        $logText = (Invoke-Adb -AdbArguments @('logcat', '-d', '-s', "${logTag}:I", 'AndroidRuntime:E', '*:S')) -join "`n"
        if ($logText.Contains('playback-failed', [System.StringComparison]::Ordinal)) {
            throw "Android audio smoke reported a failure.`n$logText"
        }
        if ($logText.Contains('playback-complete', [System.StringComparison]::Ordinal)) {
            break
        }
    }

    foreach ($marker in @('bridge-ready', 'playback-started', 'playback-progress', 'playback-complete')) {
        if (-not $logText.Contains($marker, [System.StringComparison]::Ordinal)) {
            throw "Android audio smoke is missing marker '$marker'.`n$logText"
        }
    }

    Write-Host $logText
    Write-Host "Android Avalonia SDL_mixer smoke passed on ABI $abi ($runtimeIdentifier)."
}
finally {
    if ($installed) {
        Invoke-Adb -AdbArguments @('shell', 'am', 'force-stop', $applicationId) | Out-Null
        if (-not $KeepInstalled) {
            Invoke-Adb -AdbArguments @('uninstall', $applicationId) | Out-Host
        }
    }
}
