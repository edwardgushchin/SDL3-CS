#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ManifestPath = (Join-Path $PSScriptRoot 'release-manifest.json'),
    [switch] $SkipSourceCheckoutValidation
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$manifest = Get-ReleaseManifest -ManifestPath $ManifestPath
$repoRoot = Get-ReleaseRepoRoot
$errors = New-Object System.Collections.Generic.List[string]

function Add-ValidationError {
    param([string] $Message)
    $script:errors.Add($Message)
}

$ridIds = @($manifest.rids | ForEach-Object { $_.rid })
if ($ridIds.Count -ne ($ridIds | Select-Object -Unique).Count) {
    Add-ValidationError "RID values must be unique."
}

$requiredRids = @('win-x86', 'win-x64', 'win-arm64', 'linux-x64', 'linux-arm64', 'osx-x64', 'osx-arm64', 'android-arm', 'android-arm64', 'android-x86', 'android-x64', 'ios-arm64', 'iossimulator-arm64', 'iossimulator-x64', 'tvos-arm64', 'tvossimulator-arm64', 'tvossimulator-x64')
foreach ($rid in $requiredRids) {
    if ($ridIds -notcontains $rid) {
        Add-ValidationError "Required RID is missing: $rid"
    }
}

if (-not $manifest.PSObject.Properties.Name.Contains('nativePackagePlatforms')) {
    Add-ValidationError "Manifest must declare nativePackagePlatforms for platform-specific native NuGet packages."
}
else {
    $platformIds = @($manifest.nativePackagePlatforms | ForEach-Object { $_.id })
    $platformSuffixes = @($manifest.nativePackagePlatforms | ForEach-Object { $_.suffix })
    if ($platformIds.Count -ne ($platformIds | Select-Object -Unique).Count) {
        Add-ValidationError "nativePackagePlatforms.id values must be unique."
    }
    if ($platformSuffixes.Count -ne ($platformSuffixes | Select-Object -Unique).Count) {
        Add-ValidationError "nativePackagePlatforms.suffix values must be unique."
    }

    $platformRidMembership = @{}
    foreach ($platform in @($manifest.nativePackagePlatforms)) {
        if (-not $platform.PSObject.Properties.Name.Contains('id') -or -not $platform.id) {
            Add-ValidationError "Every nativePackagePlatforms item must have id."
        }
        if (-not $platform.PSObject.Properties.Name.Contains('suffix') -or -not $platform.suffix) {
            Add-ValidationError "Every nativePackagePlatforms item must have suffix."
        }
        if (-not $platform.PSObject.Properties.Name.Contains('rids') -or @($platform.rids).Count -eq 0) {
            Add-ValidationError "Every nativePackagePlatforms item must have at least one RID."
            continue
        }

        foreach ($rid in @($platform.rids)) {
            if ($ridIds -notcontains $rid) {
                Add-ValidationError "nativePackagePlatforms.$($platform.id) references unknown RID '$rid'."
                continue
            }
            if ($platformRidMembership.ContainsKey($rid)) {
                Add-ValidationError "RID '$rid' appears in both nativePackagePlatforms.$($platformRidMembership[$rid]) and nativePackagePlatforms.$($platform.id)."
            }
            else {
                $platformRidMembership[$rid] = $platform.id
            }
        }
    }

    foreach ($rid in $ridIds) {
        if (-not $platformRidMembership.ContainsKey($rid)) {
            Add-ValidationError "RID '$rid' is not assigned to any nativePackagePlatforms item."
        }
    }
}

$requiredNextTierRids = @('linux-musl-x64', 'linux-musl-arm64', 'linux-musl-arm', 'linux-arm', 'linux-ppc64le', 'linux-s390x', 'browser-wasm')
$requiredCandidateTierRids = @('linux-bionic-arm', 'linux-bionic-arm64', 'linux-bionic-x86', 'linux-bionic-x64', 'linux-loongarch64', 'linux-riscv64', 'maccatalyst-arm64', 'maccatalyst-x64')
$requiredCommunityTierRids = @('freebsd-x64', 'freebsd-arm64', 'openbsd-x64', 'openbsd-arm64', 'haiku-x64')
$requiredExcludedPlatformFamilies = @('netbsd-qnx-riscos-dos-retro', 'visionos', 'wasi-wasm', 'tizen', 'legacy-portable-rids', 'consoles-gdk-homebrew-nda', 'legacy-unsupported')

if (-not $manifest.PSObject.Properties.Name.Contains('platformCoverage')) {
    Add-ValidationError "Manifest must declare platformCoverage for the .NET/C# and SDL3 maximum coverage policy."
}
else {
    $coverage = $manifest.platformCoverage
    $activeReleaseTier = @()
    $nextTierRids = @()
    $candidateTierRids = @()
    $communityTierRids = @()

    if (-not $coverage.PSObject.Properties.Name.Contains('policy') -or $coverage.policy -ne 'maximum-public-intersection') {
        Add-ValidationError "platformCoverage.policy must be 'maximum-public-intersection'."
    }

    if (-not $coverage.PSObject.Properties.Name.Contains('reviewedOn') -or -not $coverage.reviewedOn) {
        Add-ValidationError "platformCoverage.reviewedOn must record the source review date."
    }

    if (-not $coverage.PSObject.Properties.Name.Contains('activeReleaseTier')) {
        Add-ValidationError "platformCoverage.activeReleaseTier is missing."
    }
    else {
        $activeReleaseTier = @($coverage.activeReleaseTier)
        if ($activeReleaseTier.Count -ne ($activeReleaseTier | Select-Object -Unique).Count) {
            Add-ValidationError "platformCoverage.activeReleaseTier values must be unique."
        }

        foreach ($rid in $ridIds) {
            if ($activeReleaseTier -notcontains $rid) {
                Add-ValidationError "RID '$rid' is in manifest.rids but missing from platformCoverage.activeReleaseTier."
            }
        }

        foreach ($rid in $activeReleaseTier) {
            if ($ridIds -notcontains $rid) {
                Add-ValidationError "platformCoverage.activeReleaseTier contains '$rid', but manifest.rids does not."
            }
        }
    }

    if (-not $coverage.PSObject.Properties.Name.Contains('nextReleaseTier')) {
        Add-ValidationError "platformCoverage.nextReleaseTier is missing."
    }
    else {
        $nextTierRids = @($coverage.nextReleaseTier | ForEach-Object {
            if ($_.PSObject.Properties.Name.Contains('rid')) { $_.rid }
        })
        foreach ($rid in $requiredNextTierRids) {
            if ($nextTierRids -notcontains $rid) {
                Add-ValidationError "platformCoverage.nextReleaseTier must track required expansion RID '$rid'."
            }
        }

        foreach ($item in @($coverage.nextReleaseTier)) {
            if (-not $item.PSObject.Properties.Name.Contains('rid') -or -not $item.rid) {
                Add-ValidationError "Every platformCoverage.nextReleaseTier item must have rid."
            }
            if (-not $item.PSObject.Properties.Name.Contains('reason') -or -not $item.reason) {
                Add-ValidationError "Every platformCoverage.nextReleaseTier item must have reason."
            }
        }
    }

    if (-not $coverage.PSObject.Properties.Name.Contains('candidateTier')) {
        Add-ValidationError "platformCoverage.candidateTier is missing."
    }
    else {
        $candidateTierRids = @($coverage.candidateTier | ForEach-Object {
            if ($_.PSObject.Properties.Name.Contains('rid')) { $_.rid }
        })
        foreach ($rid in $requiredCandidateTierRids) {
            if ($candidateTierRids -notcontains $rid) {
                Add-ValidationError "platformCoverage.candidateTier must track candidate RID '$rid'."
            }
        }

        foreach ($item in @($coverage.candidateTier)) {
            if (-not $item.PSObject.Properties.Name.Contains('rid') -or -not $item.rid) {
                Add-ValidationError "Every platformCoverage.candidateTier item must have rid."
            }
            if (-not $item.PSObject.Properties.Name.Contains('reason') -or -not $item.reason) {
                Add-ValidationError "Every platformCoverage.candidateTier item must have reason."
            }
        }
    }

    if (-not $coverage.PSObject.Properties.Name.Contains('excludedPlatformFamilies')) {
        Add-ValidationError "platformCoverage.excludedPlatformFamilies is missing."
    }
    else {
        $excludedFamilies = @($coverage.excludedPlatformFamilies | ForEach-Object {
            if ($_.PSObject.Properties.Name.Contains('family')) { $_.family }
        })
        foreach ($family in $requiredExcludedPlatformFamilies) {
            if ($excludedFamilies -notcontains $family) {
                Add-ValidationError "platformCoverage.excludedPlatformFamilies must track excluded family '$family'."
            }
        }

        foreach ($item in @($coverage.excludedPlatformFamilies)) {
            if (-not $item.PSObject.Properties.Name.Contains('family') -or -not $item.family) {
                Add-ValidationError "Every platformCoverage.excludedPlatformFamilies item must have family."
            }
            if (-not $item.PSObject.Properties.Name.Contains('reason') -or -not $item.reason) {
                Add-ValidationError "Every platformCoverage.excludedPlatformFamilies item must have reason."
            }
        }
    }

    if (-not $coverage.PSObject.Properties.Name.Contains('communityTier')) {
        Add-ValidationError "platformCoverage.communityTier is missing."
    }
    else {
        $communityTierRids = @($coverage.communityTier | ForEach-Object {
            if ($_.PSObject.Properties.Name.Contains('rid')) { $_.rid }
        })
        foreach ($rid in $requiredCommunityTierRids) {
            if ($communityTierRids -notcontains $rid) {
                Add-ValidationError "platformCoverage.communityTier must track community RID '$rid'."
            }
        }

        foreach ($item in @($coverage.communityTier)) {
            if (-not $item.PSObject.Properties.Name.Contains('rid') -or -not $item.rid) {
                Add-ValidationError "Every platformCoverage.communityTier item must have rid."
            }
            if (-not $item.PSObject.Properties.Name.Contains('reason') -or -not $item.reason) {
                Add-ValidationError "Every platformCoverage.communityTier item must have reason."
            }
        }
    }

    $tierMembership = @{}
    foreach ($tier in @(
        [pscustomobject]@{ Name = 'activeReleaseTier'; Rids = $activeReleaseTier },
        [pscustomobject]@{ Name = 'nextReleaseTier'; Rids = $nextTierRids },
        [pscustomobject]@{ Name = 'candidateTier'; Rids = $candidateTierRids },
        [pscustomobject]@{ Name = 'communityTier'; Rids = $communityTierRids }
    )) {
        foreach ($rid in @($tier.Rids)) {
            if (-not $rid) { continue }
            if ($tierMembership.ContainsKey($rid)) {
                Add-ValidationError "RID '$rid' appears in both platformCoverage.$($tierMembership[$rid]) and platformCoverage.$($tier.Name)."
            }
            else {
                $tierMembership[$rid] = $tier.Name
            }
        }
    }
}

$allowedBuildHosts = @('github-actions-windows', 'github-actions-linux', 'github-actions-macos', 'github-actions-android', 'github-actions-ios', 'github-actions-tvos', 'windows-msvc', 'linux-container', 'macos-xcode')
foreach ($rid in $manifest.rids) {
    if (-not $rid.PSObject.Properties.Name.Contains('buildHost') -or -not $rid.buildHost) {
        Add-ValidationError "RID $($rid.rid) has no buildHost."
        continue
    }

    if ($allowedBuildHosts -notcontains $rid.buildHost) {
        Add-ValidationError "RID $($rid.rid) has unsupported buildHost '$($rid.buildHost)'."
    }

    if ($rid.buildHost -like 'github-actions-*') {
        if (-not $rid.PSObject.Properties.Name.Contains('runner') -or -not $rid.runner) {
            Add-ValidationError "RID $($rid.rid) uses GitHub Actions buildHost but has no runner."
        }
    }

    if ($rid.buildHost -in @('github-actions-linux', 'linux-container')) {
        if ($rid.os -ne 'linux') {
            Add-ValidationError "RID $($rid.rid) uses Linux buildHost but has os '$($rid.os)'."
        }
        if ($rid.buildHost -eq 'linux-container') {
            if (-not $rid.PSObject.Properties.Name.Contains('dockerPlatform') -or -not $rid.dockerPlatform) {
                Add-ValidationError "RID $($rid.rid) uses linux-container buildHost but has no dockerPlatform."
            }
            if (-not $rid.PSObject.Properties.Name.Contains('dockerfile') -or -not $rid.dockerfile) {
                Add-ValidationError "RID $($rid.rid) uses linux-container buildHost but has no dockerfile."
            }
            else {
                $dockerfilePath = Resolve-ReleasePath $rid.dockerfile
                if (-not (Test-Path -LiteralPath $dockerfilePath -PathType Leaf)) {
                    Add-ValidationError "RID $($rid.rid) Dockerfile was not found: $dockerfilePath"
                }
            }
        }
    }

    if ($rid.buildHost -in @('github-actions-macos', 'macos-xcode')) {
        if ($rid.os -ne 'macos') {
            Add-ValidationError "RID $($rid.rid) uses macOS buildHost but has os '$($rid.os)'."
        }
    }

    if ($rid.buildHost -eq 'github-actions-android' -and $rid.os -ne 'android') {
        Add-ValidationError "RID $($rid.rid) uses github-actions-android buildHost but has os '$($rid.os)'."
    }
    elseif ($rid.buildHost -eq 'github-actions-android') {
        if (-not $rid.PSObject.Properties.Name.Contains('androidAbi') -or -not $rid.androidAbi) {
            Add-ValidationError "RID $($rid.rid) uses github-actions-android but has no androidAbi."
        }
        if (-not $rid.PSObject.Properties.Name.Contains('androidPlatform') -or -not $rid.androidPlatform) {
            Add-ValidationError "RID $($rid.rid) uses github-actions-android but has no androidPlatform."
        }
    }

    if ($rid.buildHost -eq 'github-actions-ios' -and $rid.os -ne 'ios') {
        Add-ValidationError "RID $($rid.rid) uses github-actions-ios buildHost but has os '$($rid.os)'."
    }
    elseif ($rid.buildHost -eq 'github-actions-ios') {
        if (-not $rid.PSObject.Properties.Name.Contains('appleSdk') -or -not $rid.appleSdk) {
            Add-ValidationError "RID $($rid.rid) uses github-actions-ios but has no appleSdk."
        }
    }

    if ($rid.buildHost -eq 'github-actions-tvos' -and $rid.os -ne 'tvos') {
        Add-ValidationError "RID $($rid.rid) uses github-actions-tvos buildHost but has os '$($rid.os)'."
    }
    elseif ($rid.buildHost -eq 'github-actions-tvos') {
        if (-not $rid.PSObject.Properties.Name.Contains('appleSdk') -or -not $rid.appleSdk) {
            Add-ValidationError "RID $($rid.rid) uses github-actions-tvos but has no appleSdk."
        }
    }
}

$componentIds = @($manifest.components | ForEach-Object { $_.id })
if ($componentIds.Count -ne ($componentIds | Select-Object -Unique).Count) {
    Add-ValidationError "Component ids must be unique."
}

foreach ($component in $manifest.components) {
    if (-not $component.PSObject.Properties.Name.Contains('repository') -or -not $component.repository) {
        Add-ValidationError "Component $($component.id) has no repository."
    }

    if (-not $component.PSObject.Properties.Name.Contains('sourceFolder') -or -not $component.sourceFolder) {
        Add-ValidationError "Component $($component.id) has no sourceFolder."
    }

    if (-not $component.PSObject.Properties.Name.Contains('sourceRef') -or -not $component.sourceRef) {
        Add-ValidationError "Component $($component.id) has no sourceRef commit pin."
    }
    elseif ($component.sourceRef -notmatch '^[0-9a-fA-F]{40}$') {
        Add-ValidationError "Component $($component.id) sourceRef must be a full 40-character commit SHA."
    }

    if (-not $SkipSourceCheckoutValidation) {
        $sourcePath = Join-Path (Resolve-ReleasePath $manifest.sourceRoot) $component.sourceFolder
        if (-not (Test-Path -LiteralPath $sourcePath -PathType Container)) {
            Add-ValidationError "Source folder not found for $($component.id): $sourcePath"
        }
        elseif ((Test-Path -LiteralPath (Join-Path $sourcePath '.git')) -and
            $component.PSObject.Properties.Name.Contains('sourceRef') -and
            $component.sourceRef -match '^[0-9a-fA-F]{40}$') {
            try {
                $head = Invoke-ReleaseGitValue -RepositoryPath $sourcePath -Arguments @('rev-parse', 'HEAD')
                if ($head -ne $component.sourceRef) {
                    Add-ValidationError "Component $($component.id) source folder HEAD does not match sourceRef. HEAD $head, sourceRef $($component.sourceRef)."
                }
            }
            catch {
                Add-ValidationError "Component $($component.id) source folder HEAD could not be read: $($_.Exception.Message)"
            }
        }
    }

    if ($component.PSObject.Properties.Name.Contains('packageId')) {
        Add-ValidationError "Component $($component.id) must not declare packageId; platform package IDs are computed from nativePackagePlatforms."
    }
    if ($component.PSObject.Properties.Name.Contains('packageProject')) {
        Add-ValidationError "Component $($component.id) must not declare packageProject; platform package projects are computed from nativePackagePlatforms."
    }

    if (-not $component.PSObject.Properties.Name.Contains('upstreamRepository') -or -not $component.upstreamRepository) {
        Add-ValidationError "Component $($component.id) has no upstreamRepository."
    }

    foreach ($dependency in $component.dependencies) {
        if ($componentIds -notcontains $dependency) {
            Add-ValidationError "Component $($component.id) references missing dependency $dependency."
        }
    }

    if ($component.PSObject.Properties.Name.Contains('ridCmakeArgs')) {
        foreach ($ridArgumentSet in $component.ridCmakeArgs.PSObject.Properties) {
            if ($ridIds -notcontains $ridArgumentSet.Name) {
                Add-ValidationError "Component $($component.id) has ridCmakeArgs for unknown RID '$($ridArgumentSet.Name)'."
            }

            if (@($ridArgumentSet.Value).Count -eq 0) {
                Add-ValidationError "Component $($component.id) has empty ridCmakeArgs for RID '$($ridArgumentSet.Name)'."
            }
        }
    }

    $artifactKeys = @($manifest.rids | ForEach-Object { Get-ReleaseOsArtifactKey -RidInfo $_ } | Select-Object -Unique)
    foreach ($key in $artifactKeys) {
        if (-not $component.artifactPatterns.PSObject.Properties.Name.Contains($key)) {
            Add-ValidationError "Component $($component.id) has no artifactPatterns.$key."
            continue
        }

        if (@($component.artifactPatterns.$key).Count -eq 0) {
            Add-ValidationError "Component $($component.id) has empty artifactPatterns.$key."
        }
    }

    if ($component.id -eq 'SDL') {
        $sdlMacOsPatterns = @($component.artifactPatterns.macos)
        if ($sdlMacOsPatterns -notcontains 'lib/libSDL3*.dylib*') {
            Add-ValidationError "Component SDL artifactPatterns.macos must include versioned libSDL3*.dylib* files."
        }
    }

    if ($component.id -eq 'SDL_image') {
        foreach ($androidRid in @('android-arm', 'android-arm64', 'android-x86', 'android-x64')) {
            $ridArgs = @($component.ridCmakeArgs.PSObject.Properties[$androidRid].Value)
            if ($ridArgs -notcontains '-DSDLIMAGE_AVIF=OFF') {
                Add-ValidationError "Component SDL_image must disable AVIF for $androidRid to avoid vendored AOM/dav1d Android toolchain failures."
            }
        }

        $androidPatterns = @($component.artifactPatterns.android)
        foreach ($disabledAvifPattern in @('lib/libaom.so*', 'lib/libdav1d.so*', 'lib/libavif.so*', '**/libaom.so*', '**/libdav1d.so*', '**/libavif.so*')) {
            if ($androidPatterns -contains $disabledAvifPattern) {
                Add-ValidationError "Component SDL_image artifactPatterns.android must not require '$disabledAvifPattern' while Android AVIF is disabled."
            }
        }

        foreach ($appleRid in @('ios-arm64', 'iossimulator-arm64', 'iossimulator-x64', 'osx-arm64', 'osx-x64', 'tvos-arm64', 'tvossimulator-arm64', 'tvossimulator-x64')) {
            $ridArgs = @($component.ridCmakeArgs.PSObject.Properties[$appleRid].Value)
            if ($ridArgs -notcontains '-DSDLIMAGE_WEBP=OFF') {
                Add-ValidationError "Component SDL_image must disable WEBP for $appleRid to avoid vendored libwebp MACOSX_BUNDLE install failures."
            }
        }

        foreach ($appleStaticKey in @('ios', 'tvos')) {
            $patterns = @($component.artifactPatterns.$appleStaticKey)
            foreach ($requiredPattern in @('lib/libz*.a', '**/libz*.a')) {
                if ($patterns -notcontains $requiredPattern) {
                    Add-ValidationError "Component SDL_image artifactPatterns.$appleStaticKey must collect vendored zlib static library '$requiredPattern'."
                }
            }
        }
    }

    if ($component.id -eq 'SDL_mixer') {
        foreach ($wavpackDisabledRid in @('android-arm', 'android-x86', 'osx-arm64', 'osx-x64')) {
            $ridArgs = @($component.ridCmakeArgs.PSObject.Properties[$wavpackDisabledRid].Value)
            if ($ridArgs -notcontains '-DSDLMIXER_WAVPACK=OFF') {
                Add-ValidationError "Component SDL_mixer must disable WavPack for $wavpackDisabledRid to avoid vendored WavPack toolchain failures."
            }
        }

        foreach ($flacDisabledRid in @('android-arm', 'android-x86')) {
            $ridArgs = @($component.ridCmakeArgs.PSObject.Properties[$flacDisabledRid].Value)
            if ($ridArgs -notcontains '-DSDLMIXER_FLAC=OFF') {
                Add-ValidationError "Component SDL_mixer must disable FLAC for $flacDisabledRid to avoid vendored FLAC fseeko/ftello Android 32-bit failures."
            }
        }
    }

    if ($component.id -eq 'SDL_shadercross') {
        foreach ($appleStaticKey in @('ios', 'tvos')) {
            $patterns = @($component.artifactPatterns.$appleStaticKey)
            foreach ($requiredPattern in @('lib/libspirv-cross*.a', '**/libspirv-cross*.a')) {
                if ($patterns -notcontains $requiredPattern) {
                    Add-ValidationError "Component SDL_shadercross artifactPatterns.$appleStaticKey must collect all SPIRV-Cross static libraries with '$requiredPattern'."
                }
            }
        }
    }
}

foreach ($package in $manifest.managedPackages) {
    $projectPath = Resolve-ReleasePath $package.project
    if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
        Add-ValidationError "Managed project not found for $($package.id): $projectPath"
    }

    if ($componentIds -notcontains $package.versionComponent) {
        Add-ValidationError "Managed package $($package.id) references missing versionComponent $($package.versionComponent)."
    }
}

foreach ($requiredManagedPackage in @('SDL3-CS')) {
    if (@($manifest.managedPackages | Where-Object { $_.id -eq $requiredManagedPackage }).Count -ne 1) {
        Add-ValidationError "Manifest must declare managed package '$requiredManagedPackage' exactly once."
    }
}

$computedPackages = @(Get-ReleasePackageVersions -Manifest $manifest -PackageRevision 1)
$computedNativePackages = @($computedPackages | Where-Object { $_.Kind -eq 'native' })
$expectedNativePlatformPackageIds = [ordered]@{
    Windows = 'SDL3-CS.Windows'
    Linux = 'SDL3-CS.Linux'
    MacOS = 'SDL3-CS.MacOS'
    Android = 'SDL3-CS.Android'
    iOS = 'SDL3-CS.iOS'
    tvOS = 'SDL3-CS.tvOS'
}
$expectedComponentPackageSuffixes = [ordered]@{
    SDL = ''
    SDL_image = 'Image'
    SDL_mixer = 'Mixer'
    SDL_ttf = 'TTF'
    SDL_shadercross = 'Shadercross'
}

foreach ($platformName in $expectedNativePlatformPackageIds.Keys) {
    $platform = @($manifest.nativePackagePlatforms | Where-Object { $_.id -eq $platformName })
    if ($platform.Count -ne 1) {
        Add-ValidationError "Manifest must declare nativePackagePlatforms.$platformName exactly once."
        continue
    }

    $expectedPlatformPackageId = $expectedNativePlatformPackageIds[$platformName]
    if ($platform[0].packageId -ne $expectedPlatformPackageId) {
        Add-ValidationError "nativePackagePlatforms.$platformName packageId must be '$expectedPlatformPackageId'."
    }
}

$androidPlatform = @($manifest.nativePackagePlatforms | Where-Object { $_.id -eq 'Android' })
if ($androidPlatform.Count -eq 1) {
    foreach ($rid in @('android-arm', 'android-arm64', 'android-x86', 'android-x64')) {
        if (@($androidPlatform[0].rids) -notcontains $rid) {
            Add-ValidationError "nativePackagePlatforms.Android must include '$rid'."
        }
    }
}

foreach ($componentId in $expectedComponentPackageSuffixes.Keys) {
    $component = @($manifest.components | Where-Object { $_.id -eq $componentId })
    if ($component.Count -ne 1) {
        Add-ValidationError "Manifest must declare component '$componentId' exactly once."
        continue
    }

    $expectedComponentSuffix = $expectedComponentPackageSuffixes[$componentId]
    $actualComponentSuffix = if ($component[0].PSObject.Properties.Name.Contains('packageNameSuffix')) {
        [string] $component[0].packageNameSuffix
    }
    else {
        $null
    }

    if ($actualComponentSuffix -ne $expectedComponentSuffix) {
        Add-ValidationError "Component $componentId packageNameSuffix must be '$expectedComponentSuffix'."
    }
}

$expectedNativePackageCount = $expectedNativePlatformPackageIds.Count * $expectedComponentPackageSuffixes.Count
if ($computedNativePackages.Count -ne $expectedNativePackageCount) {
    Add-ValidationError "Manifest must compute $expectedNativePackageCount native package(s), got $($computedNativePackages.Count)."
}

foreach ($package in $computedNativePackages) {
    if ($package.Id.StartsWith('SDL3-CS.Native', [System.StringComparison]::Ordinal)) {
        Add-ValidationError "Computed native package '$($package.Id)' uses internal SDL3-CS.Native prefix instead of public platform package naming."
    }

    $expectedProject = "SDL3-CS.NativePackages/$($package.Id)/$($package.Id).csproj"
    if ($package.Project -ne $expectedProject) {
        Add-ValidationError "Computed native package '$($package.Id)' must use project '$expectedProject', got '$($package.Project)'."
    }

    $projectPath = Resolve-ReleasePath $package.Project
    if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
        Add-ValidationError "Package project not found for computed native package '$($package.Id)': $projectPath"
    }

    $rootPackageFolder = Resolve-ReleasePath $package.Id
    if (Test-Path -LiteralPath $rootPackageFolder -PathType Container) {
        Add-ValidationError "Native package folder '$($package.Id)' must be under SDL3-CS.NativePackages, not in the repository root."
    }
}

$nativePackageRoot = Resolve-ReleasePath 'SDL3-CS.NativePackages'
if (-not (Test-Path -LiteralPath $nativePackageRoot -PathType Container)) {
    Add-ValidationError "Native package root folder is missing: $nativePackageRoot"
}

foreach ($platformName in $expectedNativePlatformPackageIds.Keys) {
    $platformPackageId = $expectedNativePlatformPackageIds[$platformName]
    foreach ($componentId in $expectedComponentPackageSuffixes.Keys) {
        $componentSuffix = $expectedComponentPackageSuffixes[$componentId]
        $expectedPackageId = if ([string]::IsNullOrWhiteSpace($componentSuffix)) {
            $platformPackageId
        }
        else {
            "$platformPackageId.$componentSuffix"
        }

        $matchingPackages = @($computedNativePackages | Where-Object {
            $_.Id -eq $expectedPackageId -and
            $_.NativePackagePlatform -eq $platformName -and
            $_.VersionComponent -eq $componentId
        })
        if ($matchingPackages.Count -ne 1) {
            Add-ValidationError "Manifest must compute native package '$expectedPackageId' exactly once for platform '$platformName' and component '$componentId'."
        }
    }
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Release manifest validation failed with $($errors.Count) error(s)."
}

Write-Host "Release manifest is valid."
Write-Host "Components: $($manifest.components.Count)"
Write-Host "RIDs: $($manifest.rids.Count)"
Write-Host "Managed packages: $($manifest.managedPackages.Count)"
