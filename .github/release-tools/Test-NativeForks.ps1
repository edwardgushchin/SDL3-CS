#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ForksManifestPath = 'native-forks/forks.json',
    [string] $ReleaseManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $Fetch,
    [switch] $Update,
    [switch] $CheckUpstream,
    [switch] $RequireUpToDate,
    [switch] $RequireUpstreamCurrent
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Invoke-GitValue {
    param(
        [Parameter(Mandatory)]
        [string] $RepositoryPath,

        [Parameter(Mandatory)]
        [string[]] $Arguments
    )

    $output = & git -C $RepositoryPath @Arguments
    if ($LASTEXITCODE -ne 0) {
        throw "git -C $RepositoryPath $($Arguments -join ' ') failed with exit code $LASTEXITCODE"
    }

    return ($output | Out-String).Trim()
}

function Test-GitRefExists {
    param(
        [Parameter(Mandatory)]
        [string] $RepositoryPath,

        [Parameter(Mandatory)]
        [string] $Ref
    )

    & git -C $RepositoryPath rev-parse --verify --quiet $Ref | Out-Null
    return $LASTEXITCODE -eq 0
}

function Get-GitRemoteHead {
    param(
        [Parameter(Mandatory)]
        [string] $Url,

        [string] $Branch = 'main'
    )

    $output = & git ls-remote $Url "refs/heads/$Branch"
    if ($LASTEXITCODE -ne 0) {
        throw "git ls-remote failed for $Url refs/heads/$Branch"
    }

    $line = @($output | Select-Object -First 1)
    if ($line.Count -ne 1 -or -not $line[0]) {
        return $null
    }

    return @($line[0] -split '\s+')[0]
}

if ($Update) {
    $Fetch = $true
}
if ($RequireUpstreamCurrent) {
    $CheckUpstream = $true
}

$releaseManifest = Get-ReleaseManifest -ManifestPath $ReleaseManifestPath
$manifestPath = Resolve-ReleasePath $ForksManifestPath
if (Test-Path -LiteralPath $manifestPath -PathType Leaf) {
    $manifest = Get-Content -LiteralPath $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 16
}
else {
    $manifest = [pscustomobject]@{
        root = $releaseManifest.sourceRoot
        repositories = @($releaseManifest.components | ForEach-Object {
            [pscustomobject]@{
                name = $_.sourceFolder
                url = $_.repository
                upstreamUrl = if ($_.PSObject.Properties.Name.Contains('upstreamRepository')) { $_.upstreamRepository } else { $null }
            }
        })
    }
}

$upstreamBySourceFolder = @{}
$sourceRefBySourceFolder = @{}
foreach ($component in $releaseManifest.components) {
    if ($component.PSObject.Properties.Name.Contains('upstreamRepository')) {
        $upstreamBySourceFolder[$component.sourceFolder] = $component.upstreamRepository
    }
    if ($component.PSObject.Properties.Name.Contains('sourceRef') -and $component.sourceRef) {
        $sourceRefBySourceFolder[$component.sourceFolder] = $component.sourceRef
    }
}

$rootPath = Resolve-ReleasePath $manifest.root
$rows = New-Object System.Collections.Generic.List[object]
$errors = New-Object System.Collections.Generic.List[string]

foreach ($repository in $manifest.repositories) {
    $repositoryPath = Join-Path $rootPath $repository.name
    if (-not (Test-Path -LiteralPath $repositoryPath -PathType Container)) {
        $errors.Add("Native fork folder is missing: $repositoryPath")
        continue
    }

    $gitMarker = Join-Path $repositoryPath '.git'
    if (-not (Test-Path -LiteralPath $gitMarker)) {
        $errors.Add("Native fork folder is not a Git worktree: $repositoryPath")
        continue
    }

    if ($Fetch) {
        & git -C $repositoryPath fetch --prune origin
        if ($LASTEXITCODE -ne 0) {
            throw "git fetch failed for $($repository.name)"
        }
    }

    $branch = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('branch', '--show-current')
    $head = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-parse', '--short=12', 'HEAD')
    $originUrl = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('config', '--get', 'remote.origin.url')
    $status = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('status', '--porcelain', '--untracked-files=no')
    $dirtyCount = if ($status) { @($status -split '\r?\n').Count } else { 0 }
    $remoteRef = if ($branch) { "origin/$branch" } else { $null }
    $remoteHead = $null
    $fullHead = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-parse', 'HEAD')
    $upstreamUrl = if ($repository.PSObject.Properties.Name.Contains('upstreamUrl')) { $repository.upstreamUrl } else { $upstreamBySourceFolder[$repository.name] }
    $sourceRef = if ($repository.PSObject.Properties.Name.Contains('sourceRef') -and $repository.sourceRef) { $repository.sourceRef } else { $sourceRefBySourceFolder[$repository.name] }
    $upstreamHead = $null
    $upstreamCurrent = $null
    $forkMainHead = $null
    $forkMainCurrent = $null
    $ahead = $null
    $behind = $null

    if ($remoteRef -and (Test-GitRefExists -RepositoryPath $repositoryPath -Ref $remoteRef)) {
        $remoteHead = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-parse', '--short=12', $remoteRef)
        $counts = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-list', '--left-right', '--count', "HEAD...$remoteRef")
        $parts = @($counts -split '\s+')
        if ($parts.Count -eq 2) {
            $ahead = [int] $parts[0]
            $behind = [int] $parts[1]
        }

        if ($Update) {
            if ($dirtyCount -ne 0) {
                $errors.Add("Cannot update dirty native fork $($repository.name): $dirtyCount pending change(s).")
            }
            else {
                & git -C $repositoryPath pull --ff-only origin $branch
                if ($LASTEXITCODE -ne 0) {
                    throw "git pull --ff-only failed for $($repository.name)"
                }

                & git -C $repositoryPath submodule update --init --recursive
                if ($LASTEXITCODE -ne 0) {
                    throw "git submodule update failed for $($repository.name)"
                }

                $head = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-parse', '--short=12', 'HEAD')
                $remoteHead = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-parse', '--short=12', $remoteRef)
                $counts = Invoke-GitValue -RepositoryPath $repositoryPath -Arguments @('rev-list', '--left-right', '--count', "HEAD...$remoteRef")
                $parts = @($counts -split '\s+')
                $ahead = [int] $parts[0]
                $behind = [int] $parts[1]
            }
        }
    }
    elseif ($RequireUpToDate) {
        if (-not $sourceRef -or $fullHead -ne $sourceRef) {
            $errors.Add("Native fork $($repository.name) has no matching remote tracking ref for branch '$branch' and is not at manifest sourceRef.")
        }
    }

    if ($CheckUpstream) {
        if (-not $upstreamUrl) {
            $errors.Add("Native fork $($repository.name) has no upstream URL in forks or release manifest.")
        }
        else {
            $upstreamHeadFull = Get-GitRemoteHead -Url $upstreamUrl -Branch 'main'
            if (-not $upstreamHeadFull) {
                $errors.Add("Native fork $($repository.name) has no upstream main ref at $upstreamUrl.")
            }
            else {
                $upstreamHead = $upstreamHeadFull.Substring(0, 12)
                $upstreamCurrent = $fullHead -eq $upstreamHeadFull
                if ($RequireUpstreamCurrent) {
                    $forkMainHeadFull = Get-GitRemoteHead -Url $originUrl -Branch 'main'
                    if (-not $forkMainHeadFull) {
                        $errors.Add("Native fork $($repository.name) has no origin main ref at $originUrl.")
                    }
                    else {
                        $forkMainHead = $forkMainHeadFull.Substring(0, 12)
                        $forkMainCurrent = $forkMainHeadFull -eq $upstreamHeadFull
                        if (-not $forkMainCurrent) {
                            $errors.Add("Native fork $($repository.name) origin/main is not at upstream main. Fork main $forkMainHead, upstream $upstreamHead.")
                        }
                    }
                }
            }
        }
    }

    if ($RequireUpToDate) {
        if ($sourceRef -and $fullHead -ne $sourceRef) {
            $errors.Add("Native fork $($repository.name) is not at manifest sourceRef. Local $head, sourceRef $($sourceRef.Substring(0, 12)).")
        }
        if ($dirtyCount -ne 0) {
            $errors.Add("Native fork $($repository.name) is dirty: $dirtyCount pending change(s).")
        }
        if ($null -ne $ahead -and $ahead -ne 0) {
            $errors.Add("Native fork $($repository.name) is ahead of $remoteRef by $ahead commit(s).")
        }
        if ($null -ne $behind -and $behind -ne 0) {
            $errors.Add("Native fork $($repository.name) is behind $remoteRef by $behind commit(s).")
        }
    }

    $rows.Add([pscustomobject]@{
        Name = $repository.name
        Branch = $branch
        Head = $head
        RemoteRef = $remoteRef
        RemoteHead = $remoteHead
        Ahead = $ahead
        Behind = $behind
        Dirty = $dirtyCount
        UpstreamHead = $upstreamHead
        UpstreamCurrent = $upstreamCurrent
        ForkMainHead = $forkMainHead
        ForkMainCurrent = $forkMainCurrent
        Origin = $originUrl
        SourceRef = if ($sourceRef) { $sourceRef.Substring(0, 12) } else { $null }
    })
}

$rows | Sort-Object Name | Format-Table -AutoSize

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Native forks validation failed with $($errors.Count) issue(s)."
}

Write-Host "Native forks validation completed."
