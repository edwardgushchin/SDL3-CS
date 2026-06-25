# SDL3-CS.Android.TTF

<p align="center">
  <img alt="Android native package" src="https://img.shields.io/badge/platform-Android-555?style=flat-square">
  <img alt="SDL_ttf" src="https://img.shields.io/badge/component-SDL_ttf-239120?style=flat-square">
  <img alt="SDL_ttf 3.2.2" src="https://img.shields.io/badge/native-SDL_ttf%203.2.2-004880?style=flat-square">
</p>

`SDL3-CS.Android.TTF` contains SDL_ttf native runtime libraries for TrueType/OpenType font and text APIs for Android applications that use SDL3-CS.

## When To Use This Package

Use this package when a .NET application targets Android and needs font loading, glyph metrics, and rendered text through `SDL3.TTF`. Keep all SDL3-CS packages in the same application on the same platform family; do not mix `SDL3-CS.Android` with native packages from another platform family. This add-on package is not a replacement for the base runtime package. Install it together with `SDL3-CS` and `SDL3-CS.Android`.

## Native Version

| Package | Native library version | Package line |
|---------|------------------------|--------------|
| `SDL3-CS.Android.TTF` | SDL_ttf 3.2.2 | `3.2.2.x` |

## Supported Runtime Identifiers

- `android-arm`
- `android-arm64`
- `android-x86`
- `android-x64`

## Package Contents

- `runtimes/<rid>/native/` assets for each supported runtime identifier.
- `buildTransitive/SDL3-CS.Android.TTF.targets` for package-time native asset integration.
- `README.md`, `LICENSE`, and the SDL3-CS package icon for NuGet display.
- No application entry point and no managed SDL wrapper API beyond any bridge bindings required by the platform.

Native asset mode: Android ABI-specific native libraries and, for the base package, SDLActivity bridge bindings.

## Installation

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Android
dotnet add package SDL3-CS.Android.TTF
```

## Companion Packages

| Package | Native component | Use when you need |
|---------|------------------|-------------------|
| `SDL3-CS.Android` | SDL 3.4.10 | Core SDL3 runtime assets. |
| `SDL3-CS.Android.Image` | SDL_image 3.4.4 | Image loading and saving. |
| `SDL3-CS.Android.TTF` | SDL_ttf 3.2.2 | Font and text rendering APIs. |
| `SDL3-CS.Android.Mixer` | SDL_mixer 3.2.4 | Music and mixer playback APIs. |
| `SDL3-CS.Android.Shadercross` | SDL_shadercross 3.0.0 | Shader translation APIs. |

## Build From This Repository

From the repository root:

```powershell
dotnet pack .\SDL3-CS.NativePackages\SDL3-CS.Android.TTF\SDL3-CS.Android.TTF.csproj -c Release
```

The release pipeline populates the `lib/<rid>/` folders before packaging. A local pack without those native assets is useful only for metadata validation.

## Verification Checklist

- The package RID list matches the release manifest and native artifact layout.
- `PackageReadmeFile` points to this README.
- `buildTransitive` targets are packed under `buildTransitive/$(PackageId).targets`.
- Applications reference `SDL3-CS` plus the matching Android native package family.

## Related Documentation

- [Repository README](../../README.md)
- [Managed wrapper project](../../SDL3-CS/README.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [NuGet package search](https://www.nuget.org/profiles/edwardgushchin)
