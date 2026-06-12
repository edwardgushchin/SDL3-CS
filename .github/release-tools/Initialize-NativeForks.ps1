#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [string] $SourceRoot,
    [int] $Depth = 0,
    [switch] $SkipSubmodules,
    [switch] $DryRun
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Test-SourceRefIsCommitSha {
    param(
        [Parameter(Mandatory)]
        [string] $SourceRef
    )

    return $SourceRef -match '^[0-9a-fA-F]{40}$'
}

function Invoke-Git {
    param(
        [Parameter(Mandatory)]
        [string[]] $Arguments,

        [string] $WorkingDirectory = (Get-ReleaseRepoRoot)
    )

    Invoke-ReleaseCommand -FilePath 'git' -Arguments $Arguments -WorkingDirectory $WorkingDirectory -DryRun:$DryRun
}

function Invoke-GitText {
    param(
        [Parameter(Mandatory)]
        [string] $RepositoryPath,

        [Parameter(Mandatory)]
        [string[]] $Arguments
    )

    $output = & git -C $RepositoryPath @Arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "git -C $RepositoryPath $($Arguments -join ' ') failed with exit code $LASTEXITCODE`: $($output | Out-String)"
    }

    return ($output | Out-String).Trim()
}

function Update-NativeForkSourceRef {
    param(
        [Parameter(Mandatory)]
        [string] $TargetPath,

        [string] $SourceRef,

        [int] $Depth
    )

    if (-not $SourceRef) {
        if (-not $SkipSubmodules) {
            Invoke-Git -Arguments @('-C', $TargetPath, 'submodule', 'update', '--init', '--recursive')
        }
        return
    }

    if ($DryRun -and -not (Test-Path -LiteralPath $TargetPath -PathType Container)) {
        if (Test-SourceRefIsCommitSha -SourceRef $SourceRef) {
            Write-Host "[dry-run] git -C $TargetPath checkout --detach $SourceRef"
        }
        else {
            Write-Host "[dry-run] git -C $TargetPath checkout $SourceRef"
        }
        if (-not $SkipSubmodules) {
            Write-Host "[dry-run] git -C $TargetPath submodule update --init --recursive"
        }
        return
    }

    $currentHead = Invoke-GitText -RepositoryPath $TargetPath -Arguments @('rev-parse', 'HEAD')
    if ($currentHead -eq $SourceRef) {
        if (-not $SkipSubmodules) {
            Invoke-Git -Arguments @('-C', $TargetPath, 'submodule', 'update', '--init', '--recursive')
        }
        return
    }

    $dirty = Invoke-GitText -RepositoryPath $TargetPath -Arguments @('status', '--porcelain', '--untracked-files=no')
    if ($dirty) {
        throw "Cannot checkout pinned sourceRef '$SourceRef' in dirty native fork: $TargetPath"
    }

    if (Test-SourceRefIsCommitSha -SourceRef $SourceRef) {
        $isShallow = Invoke-GitText -RepositoryPath $TargetPath -Arguments @('rev-parse', '--is-shallow-repository')
        if ($isShallow -eq 'true') {
            Invoke-Git -Arguments @('-C', $TargetPath, 'fetch', '--unshallow', 'origin')
        }
        else {
            Invoke-Git -Arguments @('-C', $TargetPath, 'fetch', 'origin')
        }

        Invoke-Git -Arguments @('-C', $TargetPath, 'checkout', '--detach', $SourceRef)
    }
    else {
        Invoke-Git -Arguments @('-C', $TargetPath, 'fetch', 'origin', $SourceRef)
        Invoke-Git -Arguments @('-C', $TargetPath, 'checkout', $SourceRef)
    }

    if (-not $SkipSubmodules) {
        Invoke-Git -Arguments @('-C', $TargetPath, 'submodule', 'update', '--init', '--recursive')
    }
}

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
if (-not $SourceRoot) {
    $SourceRoot = $manifest.sourceRoot
}

$sourceRootPath = Resolve-ReleasePath $SourceRoot
if ($DryRun) {
    Write-Host "[dry-run] create native source root $sourceRootPath"
}
else {
    New-Item -ItemType Directory -Force -Path $sourceRootPath | Out-Null
}

foreach ($component in $manifest.components) {
    $targetPath = Join-Path $sourceRootPath $component.sourceFolder
    $gitDir = Join-Path $targetPath '.git'

    if (Test-Path -LiteralPath $targetPath -PathType Container) {
        if (-not (Test-Path -LiteralPath $gitDir)) {
            throw "Native fork folder already exists but is not a Git repository: $targetPath"
        }

        Write-Host "Native fork already exists: $($component.id) -> $targetPath"
        $sourceRef = if ($component.PSObject.Properties.Name.Contains('sourceRef')) { $component.sourceRef } else { $null }
        Update-NativeForkSourceRef -TargetPath $targetPath -SourceRef $sourceRef -Depth $Depth
        continue
    }

    $cloneArgs = @('clone', '--recursive')
    $sourceRef = if ($component.PSObject.Properties.Name.Contains('sourceRef')) { $component.sourceRef } else { $null }
    $sourceRefIsCommitSha = $sourceRef -and (Test-SourceRefIsCommitSha -SourceRef $sourceRef)
    if ($Depth -gt 0 -and -not $sourceRefIsCommitSha) {
        $cloneArgs += @('--depth', [string]$Depth)
    }
    if ($sourceRef -and -not $sourceRefIsCommitSha) {
        $cloneArgs += @('--branch', $component.sourceRef)
    }
    $cloneArgs += @($component.repository, $targetPath)

    Write-Host "Cloning native fork: $($component.id) -> $targetPath"
    Invoke-Git -Arguments $cloneArgs
    Update-NativeForkSourceRef -TargetPath $targetPath -SourceRef $sourceRef -Depth $Depth
}
