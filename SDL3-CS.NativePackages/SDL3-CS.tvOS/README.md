# SDL3-CS.tvOS

<p align="center">
  <img alt="tvOS native package" src="https://img.shields.io/badge/platform-tvOS-555?style=flat-square">
  <img alt="SDL3" src="https://img.shields.io/badge/component-SDL3-239120?style=flat-square">
  <img alt="SDL 3.4.10" src="https://img.shields.io/badge/native-SDL%203.4.10-004880?style=flat-square">
</p>

`SDL3-CS.tvOS` contains base SDL runtime libraries required by SDL3-CS applications for tvOS applications that use SDL3-CS.

## When To Use This Package

Use this package when a .NET application targets tvOS and needs windowing, events, rendering, audio, input, files, threading, timers, and the rest of the core SDL3 API. Keep all SDL3-CS packages in the same application on the same platform family; do not mix `SDL3-CS.tvOS` with native packages from another platform family.

## Native Version

| Package | Native library version | Package line |
|---------|------------------------|--------------|
| `SDL3-CS.tvOS` | SDL 3.4.10 | `3.4.10.x` |

## Supported Runtime Identifiers

- `tvos-arm64`
- `tvossimulator-arm64`
- `tvossimulator-x64`

## Package Contents

- `runtimes/<rid>/native/` assets for each supported runtime identifier.
- `buildTransitive/SDL3-CS.tvOS.targets` for package-time native asset integration.
- `README.md`, `LICENSE`, and the SDL3-CS package icon for NuGet display.
- No application entry point and no managed SDL wrapper API beyond any bridge bindings required by the platform.

Native asset mode: static native libraries linked through buildTransitive targets.

## Installation

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.tvOS
```

## Companion Packages

| Package | Native component | Use when you need |
|---------|------------------|-------------------|
| `SDL3-CS.tvOS` | SDL 3.4.10 | Core SDL3 runtime assets. |
| `SDL3-CS.tvOS.Image` | SDL_image 3.4.4 | Image loading and saving. |
| `SDL3-CS.tvOS.TTF` | SDL_ttf 3.2.2 | Font and text rendering APIs. |
| `SDL3-CS.tvOS.Mixer` | SDL_mixer 3.2.4 | Music and mixer playback APIs. |
| `SDL3-CS.tvOS.Shadercross` | SDL_shadercross 3.0.0 | Shader translation APIs. |

## Build From This Repository

From the repository root:

```powershell
dotnet pack .\SDL3-CS.NativePackages\SDL3-CS.tvOS\SDL3-CS.tvOS.csproj -c Release
```

The release pipeline populates the `lib/<rid>/` folders before packaging. A local pack without those native assets is useful only for metadata validation.

## Verification Checklist

- The package RID list matches the release manifest and native artifact layout.
- `PackageReadmeFile` points to this README.
- `buildTransitive` targets are packed under `buildTransitive/$(PackageId).targets`.
- Applications reference `SDL3-CS` plus the matching tvOS native package family.

## Related Documentation

- [Repository README](../../README.md)
- [Managed wrapper project](../../SDL3-CS/README.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [NuGet package search](https://www.nuget.org/profiles/edwardgushchin)
