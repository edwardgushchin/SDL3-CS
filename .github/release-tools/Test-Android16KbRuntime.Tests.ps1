#requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$validator = Join-Path $PSScriptRoot 'Test-Android16KbRuntime.ps1'
if (-not (Test-Path -LiteralPath $validator -PathType Leaf)) {
    throw "Android 16 KB runtime validator was not found: $validator"
}

$consumerBuilder = Join-Path $PSScriptRoot 'Test-AndroidConsumerPackageBuild.ps1'
$consumerBuilderText = Get-Content -LiteralPath $consumerBuilder -Raw -Encoding UTF8
foreach ($targetSdkContract in @(
    '[Parameter(Mandatory)][int] $TargetSdkVersion',
    'android:targetSdkVersion="$TargetSdkVersion"',
    '$androidTargetSdkVersion = [int]$androidTargetSdkMatch.Groups[''Api''].Value',
    'Get-AndroidConsumerManifest -TargetSdkVersion $androidTargetSdkVersion'
)) {
    if (-not $consumerBuilderText.Contains($targetSdkContract, [System.StringComparison]::Ordinal)) {
        throw "Android consumer runtime manifest must derive an explicit target SDK from the exact 16 KB system image: missing '$targetSdkContract'."
    }
}
foreach ($activityVisibilityContract in @(
    'ShowWhenLocked = true',
    'TurnScreenOn = true',
    'android:theme="@android:style/Theme.NoTitleBar.Fullscreen"',
    'android:hardwareAccelerated="true"'
)) {
    if (-not $consumerBuilderText.Contains($activityVisibilityContract, [System.StringComparison]::Ordinal)) {
        throw "Android runtime consumer must guarantee a visible accelerated SDL surface on a headless emulator: missing '$activityVisibilityContract'."
    }
}

$onCreateContract = [regex]::Match(
    $consumerBuilderText,
    'protected override void OnCreate\(Android\.OS\.Bundle\? savedInstanceState\)\s*\{\s*base\.OnCreate\(savedInstanceState\);\s*ProbeNativeComponents\(\);\s*\}',
    [System.Text.RegularExpressions.RegexOptions]::CultureInvariant
)
if (-not $onCreateContract.Success) {
    throw 'Android consumer must run its managed native-component probe after SDLActivity has loaded the requested libraries.'
}

$nativeProbe = [regex]::Match(
    $consumerBuilderText,
    'private static void ProbeNativeComponents\(\)\s*\{(?<Body>[\s\S]*?)\n    \}',
    [System.Text.RegularExpressions.RegexOptions]::CultureInvariant
)
if (-not $nativeProbe.Success) {
    throw 'Android consumer must define a deterministic managed native-component probe independent of SDL surface callbacks.'
}
$nativeProbeBody = $nativeProbe.Groups['Body'].Value
$readyMarker = 'Android.Util.Log.Info("SDL3CSConsumer", "SDL3CS_RUNTIME_READY");'
$readyMarkerIndex = $nativeProbeBody.IndexOf($readyMarker, [System.StringComparison]::Ordinal)
if ($readyMarkerIndex -lt 0) {
    throw 'Android native-component probe must emit the exact SDL3CS_RUNTIME_READY marker.'
}
foreach ($nativeCall in @(
    'SDL.GetVersion()',
    'SDL3.Image.Version()',
    'SDL3.Mixer.Version()',
    'SDL3.TTF.Version()',
    'SDL3.ShaderCross.GetSPIRVShaderFormats()'
)) {
    $nativeCallIndex = $nativeProbeBody.IndexOf($nativeCall, [System.StringComparison]::Ordinal)
    if ($nativeCallIndex -lt 0 -or $readyMarkerIndex -le $nativeCallIndex) {
        throw "Android consumer must emit SDL3CS_RUNTIME_READY only after successfully calling '$nativeCall'."
    }
}
if (-not $nativeProbeBody.Contains('SDL3CS_RUNTIME_FAILED', [System.StringComparison]::Ordinal)) {
    throw 'Android native-component probe must emit a fatal-classified diagnostic when a managed native call fails.'
}

$videoReadyMarker = 'Android.Util.Log.Info("SDL3CSConsumer", "SDL3CS_VIDEO_READY");'
$videoReadyMarkerIndex = $consumerBuilderText.IndexOf($videoReadyMarker, [System.StringComparison]::Ordinal)
if ($videoReadyMarkerIndex -lt 0) {
    throw 'Android consumer Main must retain the supplemental SDL video smoke marker.'
}
if (-not $consumerBuilderText.Contains('SDL3CS_INIT_FAILED', [System.StringComparison]::Ordinal)) {
    throw 'Android consumer Main must emit an explicit SDL3CS_INIT_FAILED diagnostic when SDL initialization fails.'
}
foreach ($completedOperation in @(
    'SDL.CreateWindowAndRenderer(',
    'SDL.RenderClear(',
    'SDL.RenderPresent(',
    'SDL.DestroyRenderer(',
    'SDL.DestroyWindow(',
    'SDL.Quit()'
)) {
    $operationIndex = $consumerBuilderText.LastIndexOf($completedOperation, [System.StringComparison]::Ordinal)
    if ($operationIndex -lt 0 -or $videoReadyMarkerIndex -le $operationIndex) {
        throw "Android consumer must emit SDL3CS_VIDEO_READY only after successful '$completedOperation' completion."
    }
}
foreach ($failureContract in @('SDL3CS_RUNTIME_FAILED', 'Android.Util.Log.Error(', 'SDL.GetError()')) {
    if (-not $consumerBuilderText.Contains($failureContract, [System.StringComparison]::Ordinal)) {
        throw "Android consumer Main must emit fatal-classified runtime diagnostics: missing '$failureContract'."
    }
}

function Assert-RuntimeValidationFails {
    param(
        [Parameter(Mandatory)][string] $Description,
        [Parameter(Mandatory)][string] $ExpectedMessage,
        [Parameter(Mandatory)][scriptblock] $Action
    )

    $failed = $false
    try {
        & $Action
    }
    catch {
        $failed = $true
        if ($_.Exception.Message -notmatch $ExpectedMessage) {
            throw "Android 16 KB runtime validation failed for '$Description' with an unexpected error: $($_.Exception.Message)"
        }
    }

    if (-not $failed) {
        throw "Expected Android 16 KB runtime validation to fail: $Description"
    }
}

function Assert-CommandLogged {
    param(
        [Parameter(Mandatory)][string[]] $Commands,
        [Parameter(Mandatory)][string] $Expected
    )

    if ($Commands -notcontains $Expected) {
        throw "Fake adb command log is missing '$Expected'. Commands: $($Commands -join '; ')"
    }
}

$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-android-16kb-runtime-$([guid]::NewGuid().ToString('N'))"))
if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe temporary Android runtime test path: $tempRoot"
}

New-Item -ItemType Directory -Force -Path $tempRoot | Out-Null
$fakeAdb = Join-Path $tempRoot 'fake-adb.ps1'
$apkPath = Join-Path $tempRoot 'SDL3CSConsumer-Signed.apk'
$commandLog = Join-Path $tempRoot 'adb-commands.log'
$originalScenario = [Environment]::GetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO')
$originalLog = [Environment]::GetEnvironmentVariable('SDL3CS_FAKE_ADB_LOG')

try {
    [System.IO.File]::WriteAllBytes($apkPath, [byte[]]@(0x50, 0x4b, 0x03, 0x04))

    @'
#requires -Version 7.0
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$scenario = [Environment]::GetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO')
$logPath = [Environment]::GetEnvironmentVariable('SDL3CS_FAKE_ADB_LOG')
$command = $args -join ' '
if ($logPath) {
    Add-Content -LiteralPath $logPath -Value $command -Encoding utf8NoBOM
}

switch -Regex ($command) {
    '^get-state$' {
        if ($scenario -eq 'root-reconnect-timeout') {
            $stateCallCount = @(Get-Content -LiteralPath $logPath -Encoding UTF8 | Where-Object { $_ -eq 'get-state' }).Count
            if ($stateCallCount -gt 1) { 'offline' } else { 'device' }
        }
        else {
            'device'
        }
        exit 0
    }
    '^shell getconf PAGE_SIZE$' {
        if ($scenario -eq 'page-4096') { '4096' } else { '16384' }
        exit 0
    }
    '^root$' {
        'adbd is already running as root'
        exit 0
    }
    '^wait-for-device$' {
        exit 0
    }
    '^shell setprop bionic\.linker\.16kb\.app_compat\.enabled false$' {
        if ($scenario -eq 'compat-set-failure') {
            'Failed to set property'
            exit 1
        }
        exit 0
    }
    '^shell getprop bionic\.linker\.16kb\.app_compat\.enabled$' {
        if ($scenario -eq 'compat-read-mismatch') {
            'true'
            exit 0
        }
        'false'
        exit 0
    }
    '^shell setprop pm\.16kb\.app_compat\.disabled true$' {
        exit 0
    }
    '^shell getprop pm\.16kb\.app_compat\.disabled$' {
        'true'
        exit 0
    }
    '^install -r -t ' {
        if ($scenario -eq 'install-failure') {
            'Failure [INSTALL_FAILED_INVALID_APK]'
            exit 1
        }
        'Success'
        exit 0
    }
    '^logcat -c$' {
        exit 0
    }
    '^shell input keyevent KEYCODE_WAKEUP$' {
        exit 0
    }
    '^shell wm dismiss-keyguard$' {
        exit 0
    }
    '^shell input keyevent 82$' {
        exit 0
    }
    '^shell am force-stop ' {
        exit 0
    }
    '^shell am start -W -n ' {
        if ($scenario -eq 'start-failure') {
            'Error: Activity class does not exist.'
            exit 1
        }
        'Status: ok'
        exit 0
    }
    '^shell dumpsys window windows$' {
        if ($scenario -in @('ready-after-finish', 'alive-no-ready-marker')) {
            'mTopFocusedDisplayId=-1'
            'imeLayeringTarget in display# 0 null'
        }
        elseif ($scenario -eq 'legacy-foreground') {
            'mCurrentFocus=Window{abc u0 com.edwardgushchin.sdl3cs.consumer.android.x64/com.edwardgushchin.sdl3cs.consumer.android.x64.MainActivity}'
        }
        elseif ($scenario -eq 'wrong-activity-suffix') {
            'mTopFocusedDisplayId=0'
            'imeLayeringTarget in display# 0 Window{abc u0 com.edwardgushchin.sdl3cs.consumer.android.x64/com.edwardgushchin.sdl3cs.consumer.android.x64.MainActivityOverlay}'
        }
        elseif ($scenario -eq 'mismatched-display') {
            'mTopFocusedDisplayId=0'
            'imeLayeringTarget in display# 1 Window{abc u0 com.edwardgushchin.sdl3cs.consumer.android.x64/com.edwardgushchin.sdl3cs.consumer.android.x64.MainActivity}'
        }
        else {
            'mTopFocusedDisplayId=0'
            'imeLayeringTarget in display# 0 Window{abc u0 com.edwardgushchin.sdl3cs.consumer.android.x64/com.edwardgushchin.sdl3cs.consumer.android.x64.MainActivity}'
        }
        exit 0
    }
    '^shell dumpsys activity activities$' {
        if ($scenario -in @('ready-after-finish', 'alive-no-ready-marker')) {
            'VisibleActivityProcess:[]'
        }
        elseif ($scenario -eq 'legacy-foreground') {
            'topResumedActivity=ActivityRecord{def u0 com.edwardgushchin.sdl3cs.consumer.android.x64/.MainActivity t1}'
        }
        else {
            'VisibleActivityProcess:[ ProcessRecord{def 4242:com.edwardgushchin.sdl3cs.consumer.android.x64/u0a207}]'
        }
        exit 0
    }
    '^shell pidof ' {
        $pidCallCount = @(Get-Content -LiteralPath $logPath -Encoding UTF8 | Where-Object { $_ -like 'shell pidof *' }).Count
        if ($scenario -eq 'never-pid' -or ($scenario -eq 'delayed-pid' -and $pidCallCount -eq 1)) {
            exit 1
        }
        if ($scenario -eq 'post-marker-pid-turnover' -and $pidCallCount -gt 1) {
            '5252'
            exit 0
        }
        '4242'
        exit 0
    }
    '^logcat --pid=4242 -d -v brief$' {
        if ($scenario -eq 'linker-log') {
            'E/linker(4242): dlopen failed: library "libSDL3.so" not found'
        }
        elseif ($scenario -eq 'init-failure-log') {
            'E/SDL3CSConsumer(4242): SDL3CS_INIT_FAILED: video initialization failed'
        }
        elseif ($scenario -eq 'alive-no-ready-marker') {
            'I/SDL3CSConsumer(4242): SDL initialized'
        }
        elseif ($scenario -eq 'delayed-ready-marker') {
            $logcatCallCount = @(Get-Content -LiteralPath $logPath -Encoding UTF8 | Where-Object { $_ -eq 'logcat --pid=4242 -d -v brief' }).Count
            if ($logcatCallCount -eq 1) {
                'I/SDL3CSConsumer(4242): runtime starting'
            }
            else {
                'I/SDL3CSConsumer(4242): SDL3CS_RUNTIME_READY'
            }
        }
        elseif ($scenario -eq 'ready-with-fatal') {
            'I/SDL3CSConsumer(4242): SDL3CS_RUNTIME_READY'
            'E/linker(4242): dlopen failed after readiness'
        }
        elseif ($scenario -eq 'ready-then-fatal') {
            $logcatCallCount = @(Get-Content -LiteralPath $logPath -Encoding UTF8 | Where-Object { $_ -eq 'logcat --pid=4242 -d -v brief' }).Count
            'I/SDL3CSConsumer(4242): SDL3CS_RUNTIME_READY'
            if ($logcatCallCount -gt 1) {
                'E/SDL3CSConsumer(4242): SDL3CS_RUNTIME_FAILED: post-marker failure'
            }
        }
        else {
            'I/SDL3CSConsumer(4242): SDL3CS_RUNTIME_READY'
        }
        exit 0
    }
    '^uninstall ' {
        'Success'
        exit 0
    }
    default {
        "Unexpected fake adb command: $command"
        exit 64
    }
}
'@ | Set-Content -LiteralPath $fakeAdb -Encoding utf8NoBOM

    [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_LOG', $commandLog)

    $failureScenarios = @(
        @{ Name = 'page-4096'; ExpectedMessage = 'PAGE_SIZE is 4096 bytes' },
        @{ Name = 'compat-set-failure'; ExpectedMessage = 'compatibility property.*could not be set' },
        @{ Name = 'compat-read-mismatch'; ExpectedMessage = 'compatibility property.*Expected.*false.*got.*true' },
        @{ Name = 'install-failure'; ExpectedMessage = 'adb install.*INSTALL_FAILED_INVALID_APK' },
        @{ Name = 'start-failure'; ExpectedMessage = 'adb shell am start.*Activity class does not exist' },
        @{ Name = 'linker-log'; ExpectedMessage = 'runtime log contains native loader, linker, or fatal errors' },
        @{ Name = 'init-failure-log'; ExpectedMessage = 'runtime log contains native loader, linker, or fatal errors' },
        @{ Name = 'alive-no-ready-marker'; ExpectedMessage = 'runtime log does not contain readiness marker' },
        @{ Name = 'never-pid'; ExpectedMessage = 'runtime log does not contain readiness marker' },
        @{ Name = 'ready-with-fatal'; ExpectedMessage = 'runtime log contains native loader, linker, or fatal errors' },
        @{ Name = 'ready-then-fatal'; ExpectedMessage = 'runtime log contains native loader, linker, or fatal errors' },
        @{ Name = 'post-marker-pid-turnover'; ExpectedMessage = 'marker-emitting process.*no longer active' },
        @{ Name = 'root-reconnect-timeout'; ExpectedMessage = 'did not reconnect after adb root' }
    )
    foreach ($failureScenario in $failureScenarios) {
        $scenario = $failureScenario.Name
        [System.IO.File]::WriteAllText($commandLog, [string]::Empty)
        [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO', $scenario)

        Assert-RuntimeValidationFails -Description $scenario -ExpectedMessage $failureScenario.ExpectedMessage -Action {
            if ($scenario -eq 'root-reconnect-timeout') {
                & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 -DeviceReconnectAttempts 2 -DeviceReconnectDelayMilliseconds 0 *> $null
            }
            elseif ($scenario -in @('alive-no-ready-marker', 'never-pid', 'ready-with-fatal', 'ready-then-fatal', 'post-marker-pid-turnover')) {
                & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 -RuntimeReadyAttempts 2 -RuntimeReadyDelayMilliseconds 0 -PostReadyStabilityMilliseconds 0 *> $null
            }
            else {
                & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 *> $null
            }
        }

        $commands = @(Get-Content -LiteralPath $commandLog -Encoding UTF8 | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
        Assert-CommandLogged -Commands $commands -Expected 'shell getconf PAGE_SIZE'

        if ($scenario -eq 'page-4096' -and @($commands | Where-Object { $_ -like 'install -r -t *' }).Count -ne 0) {
            throw 'A 4096-byte page-size device reached APK installation instead of failing closed.'
        }
        if ($scenario -in @('compat-set-failure', 'compat-read-mismatch') -and @($commands | Where-Object { $_ -like 'install -r -t *' }).Count -ne 0) {
            throw "A compatibility-property failure in scenario '$scenario' reached APK installation instead of failing closed."
        }
        if ($scenario -eq 'install-failure' -and @($commands | Where-Object { $_ -like 'install -r -t *' }).Count -ne 1) {
            throw 'The install-failure scenario did not exercise exactly one APK installation attempt.'
        }
        if ($scenario -eq 'start-failure' -and @($commands | Where-Object { $_ -like 'shell am start -W -n *' }).Count -ne 1) {
            throw 'The start-failure scenario did not exercise exactly one activity launch attempt.'
        }
        if ($scenario -in @('linker-log', 'init-failure-log', 'alive-no-ready-marker', 'ready-with-fatal', 'ready-then-fatal', 'post-marker-pid-turnover')) {
            Assert-CommandLogged -Commands $commands -Expected 'logcat --pid=4242 -d -v brief'
        }
        if ($scenario -in @('start-failure', 'linker-log', 'init-failure-log', 'alive-no-ready-marker', 'never-pid', 'ready-with-fatal', 'ready-then-fatal', 'post-marker-pid-turnover')) {
            Assert-CommandLogged -Commands $commands -Expected 'uninstall com.edwardgushchin.sdl3cs.consumer.android.x64'
        }
        if ($scenario -eq 'root-reconnect-timeout' -and @($commands | Where-Object { $_ -like 'install -r -t *' }).Count -ne 0) {
            throw 'An adb root reconnect timeout reached APK installation instead of failing closed.'
        }
        if ($scenario -eq 'never-pid') {
            if (@($commands | Where-Object { $_ -like 'shell pidof *' }).Count -ne 2) {
                throw 'The never-pid scenario did not stop after exactly two configured bounded PID attempts.'
            }
            if (@($commands | Where-Object { $_ -like 'logcat --pid=*' }).Count -ne 0) {
                throw 'The never-pid scenario queried process logs without a valid process ID.'
            }
        }
    }

    foreach ($foregroundScenario in @(
        @{ Name = 'legacy-foreground'; Expected = $true; Mode = 'Legacy' },
        @{ Name = 'ready-after-finish'; Expected = $false; Mode = 'None' },
        @{ Name = 'wrong-activity-suffix'; Expected = $false; Mode = 'None' },
        @{ Name = 'mismatched-display'; Expected = $false; Mode = 'None' }
    )) {
        [System.IO.File]::WriteAllText($commandLog, [string]::Empty)
        [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO', $foregroundScenario.Name)
        $foregroundResult = & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 -PostReadyStabilityMilliseconds 0
        if ($foregroundResult.Status -ne 'Passed' -or
            $foregroundResult.ForegroundReady -ne $foregroundScenario.Expected -or
            $foregroundResult.ForegroundMode -ne $foregroundScenario.Mode) {
            throw "Unexpected foreground evidence for '$($foregroundScenario.Name)': $($foregroundResult | ConvertTo-Json -Compress)"
        }
    }

    [System.IO.File]::WriteAllText($commandLog, [string]::Empty)
    [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO', 'delayed-pid')
    $delayedPidResult = & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 -RuntimeReadyAttempts 2 -RuntimeReadyDelayMilliseconds 0 -PostReadyStabilityMilliseconds 0
    if ($delayedPidResult.Status -ne 'Passed') {
        throw "Android runtime polling did not accept a delayed process start: $($delayedPidResult | ConvertTo-Json -Compress)"
    }
    $delayedPidCommands = @(Get-Content -LiteralPath $commandLog -Encoding UTF8 | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
    if (@($delayedPidCommands | Where-Object { $_ -like 'shell pidof *' }).Count -ne 3) {
        throw 'Android runtime polling did not perform two startup PID attempts plus one same-PID stability check.'
    }

    [System.IO.File]::WriteAllText($commandLog, [string]::Empty)
    [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO', 'delayed-ready-marker')
    $delayedResult = & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 -RuntimeReadyAttempts 2 -RuntimeReadyDelayMilliseconds 0 -PostReadyStabilityMilliseconds 0
    if ($delayedResult.Status -ne 'Passed') {
        throw "Android runtime polling did not accept a delayed readiness marker: $($delayedResult | ConvertTo-Json -Compress)"
    }
    $delayedCommands = @(Get-Content -LiteralPath $commandLog -Encoding UTF8 | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
    if (@($delayedCommands | Where-Object { $_ -eq 'logcat --pid=4242 -d -v brief' }).Count -ne 3) {
        throw 'Android runtime polling did not perform two bounded marker attempts plus one post-marker stability scan.'
    }

    [System.IO.File]::WriteAllText($commandLog, [string]::Empty)
    [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO', 'success')
    $result = & $validator -ApkPath $apkPath -AdbPath $fakeAdb -StartupWaitSeconds 0 -PostReadyStabilityMilliseconds 0

    if ($result.Status -ne 'Passed' -or $result.PageSize -ne 16384 -or $result.ProcessId -ne 4242 -or
        -not $result.ForegroundReady -or $result.ForegroundMode -ne 'Android35') {
        throw "Unexpected Android 16 KB runtime validation result: $($result | ConvertTo-Json -Compress)"
    }

    $successCommands = @(Get-Content -LiteralPath $commandLog -Encoding UTF8 | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
    foreach ($expectedCommand in @(
        'get-state',
        'shell getconf PAGE_SIZE',
        'root',
        'shell setprop bionic.linker.16kb.app_compat.enabled false',
        'shell getprop bionic.linker.16kb.app_compat.enabled',
        'shell setprop pm.16kb.app_compat.disabled true',
        'shell getprop pm.16kb.app_compat.disabled',
        'shell input keyevent KEYCODE_WAKEUP',
        'shell wm dismiss-keyguard',
        'shell input keyevent 82',
        'logcat -c',
        'shell dumpsys window windows',
        'shell dumpsys activity activities',
        'logcat --pid=4242 -d -v brief',
        'uninstall com.edwardgushchin.sdl3cs.consumer.android.x64'
    )) {
        Assert-CommandLogged -Commands $successCommands -Expected $expectedCommand
    }

    if (@($successCommands | Where-Object { $_ -eq 'get-state' }).Count -lt 2) {
        throw 'The successful runtime scenario did not verify adb reconnection after adb root.'
    }

    if (@($successCommands | Where-Object { $_ -like 'install -r -t *SDL3CSConsumer-Signed.apk' }).Count -ne 1) {
        throw 'The successful runtime scenario did not install the signed APK exactly once.'
    }
    $wakeIndex = [Array]::IndexOf($successCommands, 'shell input keyevent KEYCODE_WAKEUP')
    $dismissIndex = [Array]::IndexOf($successCommands, 'shell wm dismiss-keyguard')
    $menuIndex = [Array]::IndexOf($successCommands, 'shell input keyevent 82')
    $startIndex = [Array]::IndexOf($successCommands, 'shell am start -W -n com.edwardgushchin.sdl3cs.consumer.android.x64/.MainActivity')
    $focusIndex = [Array]::IndexOf($successCommands, 'shell dumpsys window windows')
    $pidIndex = [Array]::IndexOf($successCommands, 'shell pidof com.edwardgushchin.sdl3cs.consumer.android.x64')
    if ($wakeIndex -lt 0 -or $dismissIndex -le $wakeIndex -or $menuIndex -le $dismissIndex -or
        $startIndex -le $menuIndex -or $focusIndex -le $startIndex -or $pidIndex -le $focusIndex) {
        throw 'The successful runtime scenario did not wake and unlock the headless emulator before starting the SDL activity.'
    }
    if (@($successCommands | Where-Object { $_ -eq 'shell am start -W -n com.edwardgushchin.sdl3cs.consumer.android.x64/.MainActivity' }).Count -ne 1) {
        throw 'The successful runtime scenario did not start the expected default Android activity exactly once.'
    }

    Write-Host 'Android 16 KB runtime tests passed.'
}
finally {
    [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_SCENARIO', $originalScenario)
    [Environment]::SetEnvironmentVariable('SDL3CS_FAKE_ADB_LOG', $originalLog)

    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not ([System.IO.Path]::GetFileName($resolvedTempRoot)).StartsWith('sdl3-cs-android-16kb-runtime-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe temporary Android runtime test path: $resolvedTempRoot"
        }

        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}
