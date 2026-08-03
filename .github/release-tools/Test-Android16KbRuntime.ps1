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
    [int] $StartupWaitSeconds = 3
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
        $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('wait-for-device')
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

    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('logcat', '-c')
    $null = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'am', 'force-stop', $PackageName)

    $startResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'am', 'start', '-W', '-n', $componentName)
    if ($startResult.Text -match '(?im)^\s*(?:Error|Exception):|Status:\s*(?:timeout|canceled)\b') {
        throw "Android activity launch failed for '$componentName': $($startResult.Text)"
    }

    if ($StartupWaitSeconds -gt 0) {
        Start-Sleep -Seconds $StartupWaitSeconds
    }

    $pidResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'pidof', $PackageName)
    if ($pidResult.Text.Trim() -notmatch '^\d+(?:\s+\d+)*$') {
        throw "Android consumer process '$PackageName' is not alive after launch: $($pidResult.Text)"
    }
    $processId = ($pidResult.Text.Trim() -split '\s+')[0]

    $logResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('logcat', "--pid=$processId", '-d', '-v', 'brief')
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
        '\bAbort message:'
    )
    $fatalRegex = '(?im)' + ($fatalPatterns -join '|')
    $fatalLines = @($logResult.Output | Where-Object { $_ -match $fatalRegex } | Select-Object -First 10)
    if ($fatalLines.Count -gt 0) {
        throw "Android runtime log contains native loader, linker, or fatal errors:$([Environment]::NewLine)$($fatalLines -join [Environment]::NewLine)"
    }

    $finalPidResult = Invoke-AdbCommand -Executable $resolvedAdbPath -Arguments @('shell', 'pidof', $PackageName)
    if ($finalPidResult.Text.Trim() -notmatch '^\d+(?:\s+\d+)*$') {
        throw "Android consumer process '$PackageName' stopped during runtime log validation."
    }

    [pscustomobject]@{
        Status = 'Passed'
        ApkPath = $resolvedApkPath
        PackageName = $PackageName
        Activity = $componentName
        PageSize = $pageSize
        ProcessId = [int]$processId
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
