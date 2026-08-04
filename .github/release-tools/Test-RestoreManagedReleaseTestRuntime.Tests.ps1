#requires -Version 7.0
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$scriptPath = Join-Path $PSScriptRoot 'Restore-ManagedReleaseTestRuntime.ps1'
$ciPath = Join-Path $repoRoot '.github/workflows/ci.yml'
$manifestPath = Join-Path $PSScriptRoot 'release-manifest.json'
$tempBase = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$tempRoot = [System.IO.Path]::GetFullPath((Join-Path $tempBase "sdl3-cs-managed-runtime-test-$([guid]::NewGuid().ToString('N'))"))
$stalePayloadPath = Join-Path $repoRoot "SDL3-CS.NativePackages/SDL3-CS.Windows/lib/win-x64/.stale-$([guid]::NewGuid().ToString('N')).dll"

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

if (-not ('SDL3CS.ReleaseTests.NativeRuntimeProbe' -as [type])) {
    Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;

namespace SDL3CS.ReleaseTests
{
    public static class NativeRuntimeProbe
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetVersionDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GetRevisionDelegate();

        public static string[] Probe(string libraryPath)
        {
            IntPtr library = NativeLibrary.Load(libraryPath);
            try
            {
                GetVersionDelegate getVersion = Marshal.GetDelegateForFunctionPointer<GetVersionDelegate>(
                    NativeLibrary.GetExport(library, "SDL_GetVersion"));
                GetRevisionDelegate getRevision = Marshal.GetDelegateForFunctionPointer<GetRevisionDelegate>(
                    NativeLibrary.GetExport(library, "SDL_GetRevision"));
                IntPtr revision = getRevision();
                return new[] { getVersion().ToString(), revision == IntPtr.Zero ? string.Empty : Marshal.PtrToStringUTF8(revision) };
            }
            finally
            {
                NativeLibrary.Free(library);
            }
        }
    }
}
'@
}

function Get-FileInventory {
    param([Parameter(Mandatory)][string] $Root)

    $inventory = [System.Collections.Generic.Dictionary[string, string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    foreach ($file in @(Get-ChildItem -LiteralPath $Root -Recurse -File | Sort-Object FullName)) {
        $relativePath = [System.IO.Path]::GetRelativePath($Root, $file.FullName).Replace('\', '/')
        $inventory[$relativePath] = (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash
    }

    return $inventory
}

function Assert-InventoriesEqual {
    param(
        [Parameter(Mandatory)][System.Collections.Generic.Dictionary[string, string]] $Expected,
        [Parameter(Mandatory)][System.Collections.Generic.Dictionary[string, string]] $Actual,
        [Parameter(Mandatory)][string] $Context
    )

    if ($Expected.Count -ne $Actual.Count) {
        throw "$Context inventory count mismatch: expected $($Expected.Count), got $($Actual.Count)."
    }

    foreach ($entry in $Expected.GetEnumerator()) {
        if (-not $Actual.ContainsKey($entry.Key) -or $Actual[$entry.Key] -ne $entry.Value) {
            throw "$Context inventory mismatch for $($entry.Key)."
        }
    }
}

if (-not $tempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw "Unsafe managed runtime test path: $tempRoot"
}

try {
    $destination = Join-Path $tempRoot 'runtime'
    & $scriptPath `
        -NativePackageRevision 0 `
        -Rid win-x64 `
        -Destination $destination `
        -UseTrackedPayload

    $firstInventory = Get-FileInventory -Root $destination
    & $scriptPath `
        -NativePackageRevision 0 `
        -Rid win-x64 `
        -Destination $destination `
        -UseTrackedPayload
    $secondInventory = Get-FileInventory -Root $destination
    Assert-InventoriesEqual -Expected $firstInventory -Actual $secondInventory -Context 'Repeated tracked restore'

    $manifest = Get-ReleaseManifest -ManifestPath $manifestPath
    if ([int]$manifest.versioning.packageRevisionDefault -ne 0) {
        throw 'The SDL 3.4.14 candidate must use manifest packageRevisionDefault 0.'
    }

    $expectedInventory = [System.Collections.Generic.Dictionary[string, string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    $packages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision 0 |
        Where-Object { $_.Kind -eq 'native' -and @($_.Rids) -contains 'win-x64' } |
        Sort-Object Id)
    foreach ($package in $packages) {
        $projectDirectory = Split-Path -Parent ([string]$package.Project)
        $runtimeRelativePath = (Join-Path $projectDirectory 'lib/win-x64').Replace('\', '/')
        $runtimePath = Join-Path $repoRoot $runtimeRelativePath
        $trackedFiles = @(& git -C $repoRoot ls-files -- "$runtimeRelativePath/*")
        if ($LASTEXITCODE -ne 0) {
            throw "Unable to enumerate tracked payload for $($package.Id)."
        }

        foreach ($trackedFile in $trackedFiles) {
            $sourcePath = Join-Path $repoRoot $trackedFile
            $relativePath = [System.IO.Path]::GetRelativePath($runtimePath, $sourcePath).Replace('\', '/')
            $sourceHash = (Get-FileHash -LiteralPath $sourcePath -Algorithm SHA256).Hash
            if ($expectedInventory.ContainsKey($relativePath) -and $expectedInventory[$relativePath] -ne $sourceHash) {
                throw "Tracked source packages contain conflicting runtime entry: $relativePath"
            }
            $expectedInventory[$relativePath] = $sourceHash
        }
    }
    Assert-InventoriesEqual -Expected $expectedInventory -Actual $firstInventory -Context 'Tracked source-to-destination'

    $sdlComponent = Get-ReleaseComponent -Manifest $manifest -Component 'SDL'
    $nativeVersionParts = @(([string]$sdlComponent.nativeVersion).Split('.') | ForEach-Object { [int]$_ })
    if ($nativeVersionParts.Count -ne 3) {
        throw "SDL nativeVersion must contain exactly three numeric components: $($sdlComponent.nativeVersion)"
    }
    $expectedEncodedVersion = ($nativeVersionParts[0] * 1000000) + ($nativeVersionParts[1] * 1000) + $nativeVersionParts[2]
    $expectedRevision = "SDL-$($sdlComponent.nativeVersion)-release-$($sdlComponent.nativeVersion)"
    $runtimeIdentity = [SDL3CS.ReleaseTests.NativeRuntimeProbe]::Probe((Join-Path $destination 'SDL3.dll'))
    if ([int]$runtimeIdentity[0] -ne $expectedEncodedVersion) {
        throw "Tracked SDL3.dll reports SDL_GetVersion()=$($runtimeIdentity[0]); expected $expectedEncodedVersion from manifest nativeVersion $($sdlComponent.nativeVersion)."
    }
    if ($runtimeIdentity[1] -ne $expectedRevision) {
        throw "Tracked SDL3.dll reports SDL_GetRevision()='$($runtimeIdentity[1])'; expected '$expectedRevision'."
    }

    foreach ($requiredFile in @('SDL3.dll', 'SDL3_image.dll', 'SDL3_mixer.dll', 'SDL3_ttf.dll', 'SDL3_shadercross.dll')) {
        $path = Join-Path $destination $requiredFile
        if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
            throw "Tracked managed test runtime is missing $requiredFile."
        }
    }

    $ciText = Get-Content -LiteralPath $ciPath -Raw -Encoding UTF8
    if (-not $ciText.Contains('-UseTrackedPayload', [System.StringComparison]::Ordinal)) {
        throw 'CI must restore the managed test runtime from the tracked payload of the exact commit.'
    }
    if (-not $ciText.Contains('-NativePackageRevision 0', [System.StringComparison]::Ordinal)) {
        throw 'CI must restore the current manifest package revision 0.'
    }

    [System.IO.File]::WriteAllText($stalePayloadPath, 'untracked stale payload')
    $rejectedStalePayload = $false
    try {
        & $scriptPath `
            -NativePackageRevision 0 `
            -Rid win-x64 `
            -Destination (Join-Path $tempRoot 'stale-runtime') `
            -UseTrackedPayload
    }
    catch {
        $rejectedStalePayload = $_.Exception.Message.Contains('untracked', [System.StringComparison]::OrdinalIgnoreCase)
    }
    if (-not $rejectedStalePayload) {
        throw 'Tracked restore must reject untracked or stale files in a package payload directory.'
    }
}
finally {
    if (Test-Path -LiteralPath $stalePayloadPath) {
        Remove-Item -LiteralPath $stalePayloadPath -Force
    }
    if (Test-Path -LiteralPath $tempRoot) {
        $resolvedTempRoot = [System.IO.Path]::GetFullPath($tempRoot)
        $leaf = [System.IO.Path]::GetFileName($resolvedTempRoot)
        if (-not $resolvedTempRoot.StartsWith($tempBase, [System.StringComparison]::OrdinalIgnoreCase) -or
            -not $leaf.StartsWith('sdl3-cs-managed-runtime-test-', [System.StringComparison]::Ordinal)) {
            throw "Refusing to remove unsafe managed runtime test path: $resolvedTempRoot"
        }
        Remove-Item -LiteralPath $resolvedTempRoot -Recurse -Force
    }
}

Write-Host 'Tracked managed release test runtime restore tests passed.'
