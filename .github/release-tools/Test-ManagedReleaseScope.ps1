#requires -Version 7.0
[CmdletBinding()]
param(
    [int] $PackageRevision = -1,
    [int] $NativePackageRevision = -1,
    [Parameter(Mandatory)]
    [string] $BaseRef,
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $RepositoryRoot,
    [switch] $CheckNuGet
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Invoke-ScopeGit {
    param(
        [Parameter(Mandatory)][string[]] $Arguments,
        [switch] $AllowFailure
    )

    $output = & git -C $script:repositoryRoot @Arguments 2>&1
    $exitCode = $LASTEXITCODE
    if ($exitCode -ne 0 -and -not $AllowFailure) {
        throw "git $($Arguments -join ' ') failed with exit code $exitCode`: $($output | Out-String)"
    }

    return [pscustomobject]@{
        ExitCode = $exitCode
        Output = (($output | Out-String).Trim())
    }
}

function Test-NativeSensitivePath {
    param([Parameter(Mandatory)][string] $Path)

    $normalized = $Path.Replace('\', '/')
    while ($normalized.StartsWith('./', [System.StringComparison]::Ordinal)) {
        $normalized = $normalized.Substring(2)
    }
    $normalized = $normalized.TrimStart('/')
    return $normalized.StartsWith('SDL3-CS.NativePackages/', [System.StringComparison]::Ordinal) -or
        $normalized.Equals('SDL3-CS.NativePackages', [System.StringComparison]::Ordinal) -or
        $normalized.StartsWith('native-forks/', [System.StringComparison]::Ordinal) -or
        $normalized.Equals('native-forks', [System.StringComparison]::Ordinal) -or
        $normalized.Equals('.github/release-tools/release-manifest.json', [System.StringComparison]::Ordinal)
}

function Add-ChangedPaths {
    param(
        [Parameter(Mandatory)][string] $Source,
        [AllowEmptyString()][string] $Text
    )

    foreach ($line in @($Text -split "`r?`n")) {
        $path = $line.Trim()
        if (-not $path) {
            continue
        }

        $key = "$Source`0$path"
        if ($script:changeKeys.Add($key)) {
            $script:changes.Add([pscustomobject]@{ Source = $Source; Path = $path })
        }
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if ($PackageRevision -lt 0) {
    $PackageRevision = [int] $manifest.versioning.packageRevisionDefault
}
if ($NativePackageRevision -lt 0) {
    throw 'NativePackageRevision must identify the already published native package matrix.'
}
if ($PackageRevision -lt 0) {
    throw 'PackageRevision must be zero or greater.'
}

if (-not $RepositoryRoot) {
    $RepositoryRoot = Get-ReleaseRepoRoot
}
$script:repositoryRoot = [System.IO.Path]::GetFullPath($RepositoryRoot)
if (-not (Test-Path -LiteralPath $script:repositoryRoot -PathType Container)) {
    throw "Repository root was not found: $script:repositoryRoot"
}

$insideWorkTree = Invoke-ScopeGit -Arguments @('rev-parse', '--is-inside-work-tree') -AllowFailure
if ($insideWorkTree.ExitCode -ne 0 -or $insideWorkTree.Output -ne 'true') {
    throw "Managed release scope requires a Git worktree: $script:repositoryRoot"
}

$baseState = Invoke-ScopeGit -Arguments @('rev-parse', '--verify', "$BaseRef^{commit}") -AllowFailure
if ($baseState.ExitCode -ne 0 -or -not $baseState.Output) {
    throw "Managed release base ref does not exist: $BaseRef"
}
$baseCommit = $baseState.Output
$headCommit = (Invoke-ScopeGit -Arguments @('rev-parse', 'HEAD')).Output
$ancestorState = Invoke-ScopeGit -Arguments @('merge-base', '--is-ancestor', $baseCommit, $headCommit) -AllowFailure
if ($ancestorState.ExitCode -ne 0) {
    throw "Managed release base ref '$BaseRef' ($baseCommit) is not an ancestor of HEAD ($headCommit)."
}

$script:changes = New-Object System.Collections.Generic.List[object]
$script:changeKeys = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
Add-ChangedPaths -Source 'committed' -Text (Invoke-ScopeGit -Arguments @('diff', '--name-only', '--diff-filter=ACDMRTUXB', "$baseCommit..$headCommit", '--')).Output
Add-ChangedPaths -Source 'staged' -Text (Invoke-ScopeGit -Arguments @('diff', '--cached', '--name-only', '--diff-filter=ACDMRTUXB', '--')).Output
Add-ChangedPaths -Source 'unstaged' -Text (Invoke-ScopeGit -Arguments @('diff', '--name-only', '--diff-filter=ACDMRTUXB', '--')).Output
Add-ChangedPaths -Source 'untracked' -Text (Invoke-ScopeGit -Arguments @('ls-files', '--others', '--exclude-standard')).Output

$nativeChanges = @($script:changes | Where-Object { Test-NativeSensitivePath -Path $_.Path })
if ($nativeChanges.Count -gt 0) {
    $nativeChanges | Sort-Object Source, Path | Format-Table -AutoSize
    throw "Managed-only release is blocked by $($nativeChanges.Count) native-sensitive changed path(s). Use the full native release workflow."
}

$targetPackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $PackageRevision | Where-Object { $_.Kind -eq 'managed' })
if ($targetPackages.Count -ne 1 -or $targetPackages[0].Id -ne 'SDL3-CS') {
    throw "Managed-only release requires exactly one managed SDL3-CS package, got $($targetPackages.Count)."
}

$nativePackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision $NativePackageRevision | Where-Object { $_.Kind -eq 'native' })
if ($nativePackages.Count -eq 0) {
    throw "Native package revision $NativePackageRevision produced no reusable native packages."
}

$rows = New-Object System.Collections.Generic.List[object]
foreach ($package in $nativePackages) {
    $status = 'not-checked'
    $detail = 'NuGet check disabled'
    if ($CheckNuGet) {
        $lowerId = $package.Id.ToLowerInvariant()
        $lowerVersion = (Get-ReleaseNormalizedNuGetVersion -PackageVersion $package.PackageVersion).ToLowerInvariant()
        $uri = "https://api.nuget.org/v3-flatcontainer/$lowerId/$lowerVersion/$lowerId.$lowerVersion.nupkg"
        try {
            $response = Invoke-WebRequest -Uri $uri -Method Head -TimeoutSec 30 -SkipHttpErrorCheck
            $statusCode = [int] $response.StatusCode
            $status = if ($statusCode -eq 200) { 'published' } else { 'missing' }
            $detail = "HTTP $statusCode"
        }
        catch {
            $status = 'error'
            $detail = $_.Exception.Message
        }

        if ($status -ne 'published') {
            throw "Reusable native package is not available on NuGet: $($package.Id) $($package.PackageVersion) ($detail)."
        }
    }

    $rows.Add([pscustomobject]@{
        Package = $package.Id
        Version = $package.PackageVersion
        Status = $status
        Detail = $detail
    })
}

$rows | Sort-Object Package | Format-Table -AutoSize
Write-Host "Managed release scope is valid: $($targetPackages[0].Id) $($targetPackages[0].PackageVersion); reusable native packages $($nativePackages.Count); native-sensitive changes 0."
