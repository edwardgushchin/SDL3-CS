#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $ApkPath,

    [string] $AdbPath,

    [ValidateNotNullOrEmpty()]
    [string] $PackageName = 'com.edwardgushchin.sdl3cs.consumer.android.x64',

    [ValidateNotNullOrEmpty()]
    [string] $ActivityName = '.MainActivity',

    [ValidateRange(0, 60)]
    [int] $StartupWaitSeconds = 3,

    [ValidateRange(1, 120)]
    [int] $DeviceReconnectAttempts = 30,

    [ValidateRange(0, 5000)]
    [int] $DeviceReconnectDelayMilliseconds = 1000,

    [ValidateRange(1, 120)]
    [int] $RuntimeReadyAttempts = 30,

    [ValidateRange(0, 5000)]
    [int] $RuntimeReadyDelayMilliseconds = 1000,

    [ValidateRange(0, 5000)]
    [int] $PostReadyStabilityMilliseconds = 1000
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Resolve-AdbExecutable {
    param([AllowNull()][string] $Candidate)

    if (-not [string]::IsNullOrWhiteSpace($Candidate)) {
        if (Test-Path -LiteralPath $Candidate -PathType Leaf) {
            return (Resolve-Path -LiteralPath $Candidate).Path
        }

        $candidateCommand = Get-Command $Candidate -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($candidateCommand) {
            return $candidateCommand.Source
        }

        throw "adb was not found at '$Candidate'."
    }

    $command = Get-Command 'adb' -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($command) {
        return $command.Source
    }

    throw 'adb was not found. Pass -AdbPath or install Android SDK platform-tools on PATH.'
}

function Invoke-AdbCommand {
    param(
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][AllowEmptyCollection()][string[]] $Arguments,
        [switch] $AllowFailure
    )

    $output = @()
    $exitCode = -1
    try {
        $output = @(& $Executable @Arguments 2>&1 | ForEach-Object { $_.ToString() })
        $exitCode = if ($null -eq $LASTEXITCODE) { 0 } else { [int]$LASTEXITCODE }
    }
    catch {
        $output = @($_.Exception.Message)
        $exitCode = -1
    }

    $result = [pscustomobject]@{
        ExitCode = $exitCode
        Output = $output
        Text = ($output -join [Environment]::NewLine)
    }

    if ($exitCode -ne 0 -and -not $AllowFailure) {
        $displayCommand = (@('adb') + $Arguments) -join ' '
        $details = if ([string]::IsNullOrWhiteSpace($result.Text)) { '<no output>' } else { $result.Text }
        throw "Android runtime command failed with exit code $exitCode ($displayCommand): $details"
    }

    return $result
}

function Get-AndroidComponentName {
    param(
        [Parameter(Mandatory)][string] $ApplicationId,
        [Parameter(Mandatory)][string] $Activity
    )

    if ($Activity.Contains('/', [System.StringComparison]::Ordinal)) {
        return $Activity
    }

    if ($Activity.StartsWith('.', [System.StringComparison]::Ordinal)) {
        return "$ApplicationId/$Activity"
    }

    if ($Activity.Contains('.', [System.StringComparison]::Ordinal)) {
        return "$ApplicationId/$Activity"
    }

    return "$ApplicationId/.$Activity"
}

function Wait-AndroidDevice {
    param(
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][int] $Attempts,
        [Parameter(Mandatory)][int] $DelayMilliseconds
    )

    for ($attempt = 1; $attempt -le $Attempts; $attempt++) {
        $stateResult = Invoke-AdbCommand -Executable $Executable -Arguments @('get-state') -AllowFailure
        if ($stateResult.ExitCode -eq 0 -and
            $stateResult.Text.Trim().Equals('device', [System.StringComparison]::OrdinalIgnoreCase)) {
            return
        }

        if ($attempt -lt $Attempts -and $DelayMilliseconds -gt 0) {
            Start-Sleep -Milliseconds $DelayMilliseconds
        }
    }

    throw "Android device did not reconnect after adb root within $Attempts attempt(s)."
}

function Get-AndroidActivityForegroundState {
    param(
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][string] $Component
    )

    $applicationId = $Component.Split('/', 2)[0]
    $componentCandidates = @(
        $Component,
        $Component.Replace('/.', "/$applicationId.", [System.StringComparison]::Ordinal)
    ) | Select-Object -Unique
    $componentPattern = '(?:' + (($componentCandidates | ForEach-Object { [regex]::Escape($_) }) -join '|') + ')(?=$|[\s}\]])'
    $windowPattern = '(?im)^\s*(?:mCurrentFocus|mFocusedApp)\s*=.*' + $componentPattern
    $activityPattern = '(?im)^\s*(?:topResumedActivity|mResumedActivity|ResumedActivity)\b.*' + $componentPattern
    $visibleProcessPattern = '(?im)^\s*VisibleActivityProcess\s*:.*' + [regex]::Escape($applicationId) + '(?=$|[/:\s}\]])'
    $windowResult = Invoke-AdbCommand -Executable $Executable -Arguments @('shell', 'dumpsys', 'window', 'windows') -AllowFailure
    $activityResult = Invoke-AdbCommand -Executable $Executable -Arguments @('shell', 'dumpsys', 'activity', 'activities') -AllowFailure
    $windowState = if ([string]::IsNullOrWhiteSpace($windowResult.Text)) { '<no window state>' } else { $windowResult.Text }
    $activityState = if ([string]::IsNullOrWhiteSpace($activityResult.Text)) { '<no activity state>' } else { $activityResult.Text }
    $windowFocused = $windowResult.Text -match $windowPattern
    $activityResumed = $activityResult.Text -match $activityPattern
    $android35VisibleFocused = $false
    $topFocusedDisplayMatch = [regex]::Match(
        $windowResult.Text,
        '(?im)^\s*mTopFocusedDisplayId\s*=\s*(?<Display>\d+)\s*$'
    )
    if ($topFocusedDisplayMatch.Success) {
        $displayId = [regex]::Escape($topFocusedDisplayMatch.Groups['Display'].Value)
        $android35WindowPattern = '(?im)^\s*imeLayeringTarget\s+in\s+display#\s+' + $displayId + '\b.*' + $componentPattern
        $android35VisibleFocused =
            $windowResult.Text -match $android35WindowPattern -and
            $activityResult.Text -match $visibleProcessPattern
    }

    $mode = if ($windowFocused -and $activityResumed) {
        'Legacy'
    }
    elseif ($android35VisibleFocused) {
        'Android35'
    }
    else {
        'None'
    }

    return [pscustomobject]@{
        Ready = $mode -ne 'None'
        Mode = $mode
        WindowFocused = $windowFocused
        ActivityResumed = $activityResumed
        Android35VisibleFocused = $android35VisibleFocused
        WindowState = $windowState
        ActivityState = $activityState
    }
}

function Set-AndroidCompatibilityProperty {
    param(
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][string] $Name,
        [Parameter(Mandatory)][string] $Value
    )

    $setResult = Invoke-AdbCommand -Executable $Executable -Arguments @('shell', 'setprop', $Name, $Value) -AllowFailure
    if ($setResult.ExitCode -ne 0) {
        throw "Android compatibility property '$Name' could not be set to '$Value': $($setResult.Text)"
    }

    $getResult = Invoke-AdbCommand -Executable $Executable -Arguments @('shell', 'getprop', $Name) -AllowFailure
    if ($getResult.ExitCode -ne 0) {
        throw "Android compatibility property '$Name' was set but could not be read back: $($getResult.Text)"
    }

    $actual = $getResult.Text.Trim()
    if ([string]::IsNullOrWhiteSpace($actual) -or
        -not $actual.Equals($Value, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Android compatibility property '$Name' was not disabled as requested. Expected '$Value', got '$actual'."
    }
}

if (-not (Test-Path -LiteralPath $ApkPath -PathType Leaf)) {
    throw "Signed Android x86_64 APK was not found: $ApkPath"
}

$resolvedApkPath = (Resolve-Path -LiteralPath $ApkPath).Path
if (-not [System.IO.Path]::GetExtension($resolvedApkPath).Equals('.apk', [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Android runtime validation requires an APK file: $resolvedApkPath"
}

$resolvedAdbPath = Resolve-AdbExecutable -Candidate $AdbPath
$componentName = Get-AndroidComponentName -ApplicationId $PackageName -Activity $ActivityName
$installed = $false

try {
    $state = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('get-state')
    if (-not $state.Text.Trim().Equals('device', [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "adb does not report a connected, authorized device: $($state.Text.Trim())"
    }

    $pageSizeResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'getconf', 'PAGE_SIZE')
    $pageSizeLines = @($pageSizeResult.Output | Where-Object { $_ -match '^\s*\d+\s*$' })
    if ($pageSizeLines.Count -ne 1) {
        throw "Unable to determine an unambiguous Android PAGE_SIZE from adb output: $($pageSizeResult.Text)"
    }

    $pageSize = [int64]$pageSizeLines[0].Trim()
    if ($pageSize -ne 16384) {
        throw "Android runtime device PAGE_SIZE is $pageSize bytes; a real 16384-byte page-size device is required."
    }

    $rootResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('root') -AllowFailure
    if ($rootResult.ExitCode -eq 0) {
        Wait-AndroidDevice `
            -Executable $resolvedAdbPath `
            -Attempts $DeviceReconnectAttempts `
            -DelayMilliseconds $DeviceReconnectDelayMilliseconds
    }
    else {
        Write-Verbose 'adb root is unavailable; compatibility properties will be attempted with the current adb identity.'
    }

    Set-AndroidCompatibilityProperty `
        -Executable $resolvedAdbPath `
        -Name 'bionic.linker.16kb.app_compat.enabled' `
        -Value 'false'
    Set-AndroidCompatibilityProperty `
        -Executable $resolvedAdbPath `
        -Name 'pm.16kb.app_compat.disabled' `
        -Value 'true'

    $installResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('install', '-r', '-t', $resolvedApkPath)
    if ($installResult.Text -notmatch '(?m)^\s*Success\s*$') {
        throw "adb install did not report success for '$resolvedApkPath': $($installResult.Text)"
    }
    $installed = $true

    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'input', 'keyevent', 'KEYCODE_WAKEUP')
    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'wm', 'dismiss-keyguard')
    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'input', 'keyevent', '82')
    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('logcat', '-c')
    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'am', 'force-stop', $PackageName)

    $startResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'am', 'start', '-W', '-n', $componentName)
    if ($startResult.Text -match '(?im)^\s*(?:Error|Exception):|Status:\s*(?:timeout|canceled)\b') {
        throw "Android activity launch failed for '$componentName': $($startResult.Text)"
    }

    if ($StartupWaitSeconds -gt 0) {
        Start-Sleep -Seconds $StartupWaitSeconds
    }

    $foregroundState = Get-AndroidActivityForegroundState -Executable $resolvedAdbPath -Component $componentName

    $fatalPatterns = @(
        'java\.lang\.UnsatisfiedLinkError',
        '\bUnsatisfiedLinkError\b',
        '\bdlopen failed\b',
        '\bCANNOT LINK EXECUTABLE\b',
        '\bcannot locate symbol\b',
        '\blibrary ["''][^"'']+\.so["''] not found\b',
        '\b(?:linker|NativeLoader)\b.*\b(?:error|failed|fatal)\b',
        '\bFATAL EXCEPTION\b',
        '\bFatal signal\s+\d+\b',
        '\bSIG(?:ABRT|SEGV|BUS|ILL)\b',
        '\bAbort message:',
        '\bJNI_ERR returned from JNI_OnLoad\b',
        '\bSDL\w*\b.*\bversion mismatch\b',
        '\bSDL3CS_(?:INIT|RUNTIME)_FAILED\b'
    )
    $fatalRegex = '(?im)' + ($fatalPatterns -join '|')
    $runtimeReady = $false
    $processId = $null
    $lastRuntimeLog = @()
    for ($attempt = 1; $attempt -le $RuntimeReadyAttempts; $attempt++) {
        $pidResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'pidof', $PackageName) -AllowFailure
        if ($pidResult.ExitCode -eq 0 -and $pidResult.Text.Trim() -match '^\d+(?:\s+\d+)*$') {
            $processId = ($pidResult.Text.Trim() -split '\s+')[0]
            $logResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('logcat', "--pid=$processId", '-d', '-v', 'brief')
            $lastRuntimeLog = @($logResult.Output)

            $fatalLines = @($lastRuntimeLog | Where-Object { $_ -match $fatalRegex } | Select-Object -First 10)
            if ($fatalLines.Count -gt 0) {
                throw "Android runtime log contains native loader, linker, or fatal errors:$([Environment]::NewLine)$($fatalLines -join [Environment]::NewLine)"
            }

            if (@($lastRuntimeLog | Where-Object { $_ -match '\bSDL3CS_RUNTIME_READY\b' }).Count -gt 0) {
                $runtimeReady = $true
                break
            }
        }

        if ($attempt -lt $RuntimeReadyAttempts -and $RuntimeReadyDelayMilliseconds -gt 0) {
            Start-Sleep -Milliseconds $RuntimeReadyDelayMilliseconds
        }
    }

    if (-not $runtimeReady) {
        $foregroundState = Get-AndroidActivityForegroundState -Executable $resolvedAdbPath -Component $componentName
        $diagnosticLines = @($lastRuntimeLog | Select-Object -Last 40)
        $diagnostics = if ($diagnosticLines.Count -eq 0) { '<no process log output>' } else { $diagnosticLines -join [Environment]::NewLine }
        $windowDiagnostics = (@($foregroundState.WindowState -split "`r?`n") | Select-Object -Last 40) -join [Environment]::NewLine
        $activityDiagnostics = (@($foregroundState.ActivityState -split "`r?`n") | Select-Object -Last 40) -join [Environment]::NewLine
        throw "Android runtime log does not contain readiness marker SDL3CS_RUNTIME_READY after $RuntimeReadyAttempts bounded attempt(s). Foreground evidence: ready=$($foregroundState.Ready); mode=$($foregroundState.Mode); windowFocused=$($foregroundState.WindowFocused); activityResumed=$($foregroundState.ActivityResumed); android35VisibleFocused=$($foregroundState.Android35VisibleFocused). Last process log:$([Environment]::NewLine)$diagnostics$([Environment]::NewLine)Window state:$([Environment]::NewLine)$windowDiagnostics$([Environment]::NewLine)Activity state:$([Environment]::NewLine)$activityDiagnostics"
    }

    if ($PostReadyStabilityMilliseconds -gt 0) {
        Start-Sleep -Milliseconds $PostReadyStabilityMilliseconds
    }

    $finalPidResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'pidof', $PackageName) -AllowFailure
    $finalProcessIds = if ($finalPidResult.ExitCode -eq 0 -and $finalPidResult.Text.Trim() -match '^\d+(?:\s+\d+)*$') {
        @($finalPidResult.Text.Trim() -split '\s+')
    }
    else {
        @()
    }
    if ($finalProcessIds -notcontains $processId) {
        throw "Android readiness marker-emitting process '$processId' is no longer active for package '$PackageName'. Current PID output: $($finalPidResult.Text)"
    }

    $stabilityLogResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('logcat', "--pid=$processId", '-d', '-v', 'brief')
    $stabilityFatalLines = @($stabilityLogResult.Output | Where-Object { $_ -match $fatalRegex } | Select-Object -First 10)
    if ($stabilityFatalLines.Count -gt 0) {
        throw "Android runtime log contains native loader, linker, or fatal errors after readiness:$([Environment]::NewLine)$($stabilityFatalLines -join [Environment]::NewLine)"
    }

    [pscustomobject]@{
        Status = 'Passed'
        ApkPath = $resolvedApkPath
        PackageName = $PackageName
        Activity = $componentName
        PageSize = $pageSize
        ProcessId = [int]$processId
        ForegroundReady = [bool]$foregroundState.Ready
        ForegroundMode = $foregroundState.Mode
    }
}
finally {
    if ($installed) {
        $stopResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'am', 'force-stop', $PackageName) -AllowFailure
        if ($stopResult.ExitCode -ne 0) {
            Write-Warning "Failed to stop Android runtime validation package '$PackageName'."
        }

        $uninstallResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('uninstall', $PackageName) -AllowFailure
        if ($uninstallResult.ExitCode -ne 0 -or $uninstallResult.Text -notmatch '(?m)^\s*Success\s*$') {
            Write-Warning "Failed to uninstall Android runtime validation package '$PackageName'."
        }
    }
}
