# SDL3-CS.Windows.TTF

<p align="center">
  <img alt="Windows native package" src="https://img.shields.io/badge/platform-Windows-555?style=flat-square">
  <img alt="SDL_ttf" src="https://img.shields.io/badge/component-SDL_ttf-239120?style=flat-square">
  <img alt="SDL_ttf 3.2.2" src="https://img.shields.io/badge/native-SDL_ttf%203.2.2-004880?style=flat-square">
</p>

`SDL3-CS.Windows.TTF` contains SDL_ttf native runtime libraries for TrueType/OpenType font and text APIs for Windows applications that use SDL3-CS.

## When To Use This Package

Use this package when a .NET application targets Windows and needs font loading, glyph metrics, and rendered text through `SDL3.TTF`. Keep all SDL3-CS packages in the same application on the same platform family; do not mix `SDL3-CS.Windows` with native packages from another platform family. This add-on package is not a replacement for the base runtime package. Install it together with `SDL3-CS` and `SDL3-CS.Windows`.

## Native Version

| Package | Native library version | Package line |
|---------|------------------------|--------------|
| `SDL3-CS.Windows.TTF` | SDL_ttf 3.2.2 | `3.2.2.x` |

## Supported Runtime Identifiers

- `win-x86`
- `win-x64`
- `win-arm64`

## Package Contents

- `runtimes/<rid>/native/` assets for each supported runtime identifier.
- `buildTransitive/SDL3-CS.Windows.TTF.targets` for package-time native asset integration.
- `README.md`, `LICENSE`, and the SDL3-CS package icon for NuGet display.
- No application entry point and no managed SDL wrapper API beyond any bridge bindings required by the platform.

Native asset mode: dynamic native libraries copied from the NuGet runtime assets.

## Installation

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Windows
dotnet add package SDL3-CS.Windows.TTF
```

## Companion Packages

| Package | Native component | Use when you need |
|---------|------------------|-------------------|
| `SDL3-CS.Windows` | SDL 3.4.10 | Core SDL3 runtime assets. |
| `SDL3-CS.Windows.Image` | SDL_image 3.4.4 | Image loading and saving. |
| `SDL3-CS.Windows.TTF` | SDL_ttf 3.2.2 | Font and text rendering APIs. |
| `SDL3-CS.Windows.Mixer` | SDL_mixer 3.2.4 | Music and mixer playback APIs. |
| `SDL3-CS.Windows.Shadercross` | SDL_shadercross 3.0.0 | Shader translation APIs. |

## Build From This Repository

From the repository root:

```powershell
dotnet pack .\SDL3-CS.NativePackages\SDL3-CS.Windows.TTF\SDL3-CS.Windows.TTF.csproj -c Release
```

The release pipeline populates the `lib/<rid>/` folders before packaging. A local pack without those native assets is useful only for metadata validation.

## Verification Checklist

- The package RID list matches the release manifest and native artifact layout.
- `PackageReadmeFile` points to this README.
- `buildTransitive` targets are packed under `buildTransitive/$(PackageId).targets`.
- Applications reference `SDL3-CS` plus the matching Windows native package family.

## Related Documentation

- [Repository README](../../README.md)
- [Managed wrapper project](../../SDL3-CS/README.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [NuGet package search](https://www.nuget.org/profiles/edwardgushchin)
