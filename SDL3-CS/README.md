# SDL3-CS Managed Wrapper Project

<p align="center">
  <img alt=".NET 7, 8, 9, and 10" src="https://img.shields.io/badge/.NET-7.0%20%7C%208.0%20%7C%209.0%20%7C%2010.0-512BD4?style=flat-square">
  <img alt="C# 14" src="https://img.shields.io/badge/C%23-14-239120?style=flat-square">
  <img alt="SDL target 3.4.12" src="https://img.shields.io/badge/SDL3%20target-3.4.12-239120?style=flat-square">
  <img alt="NuGet package SDL3-CS" src="https://img.shields.io/badge/NuGet-SDL3--CS-004880?style=flat-square">
</p>

`SDL3-CS` is the managed C# binding project for SDL3 and the companion SDL_image, SDL_ttf, SDL_mixer, and SDL_shadercross APIs exposed by this repository. It provides the public `SDL3` namespace that application and example projects consume.

## What This Project Contains

- P/Invoke declarations and managed convenience wrappers for SDL3 core APIs.
- Add-on bindings for Image, TTF, Mixer, and ShaderCross namespaces.
- C# representations of SDL enums, flags, structs, callbacks, opaque handles, and constants.
- XML documentation generated from upstream SDL documentation and kept warning-clean by repository checks.
- NuGet package metadata for the managed `SDL3-CS` package.

## What This Project Does Not Ship

The managed wrapper package does not carry platform native binaries. Applications should pair it with exactly one native runtime package family for their target platform, such as `SDL3-CS.Windows`, `SDL3-CS.Linux`, `SDL3-CS.MacOS`, `SDL3-CS.Android`, `SDL3-CS.iOS`, or `SDL3-CS.tvOS`.

## Target Frameworks

| Target | Purpose |
|--------|---------|
| `net7.0` | Long-running applications that still target .NET 7. |
| `net8.0` | Current LTS-friendly applications and the test runner baseline. |
| `net9.0` | Applications on the .NET 9 line. |
| `net10.0` | Latest SDK and example project line in this repository. |

## Build

From the repository root:

```powershell
dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release
```

To create the managed NuGet package locally:

```powershell
dotnet pack .\SDL3-CS\SDL3-CS.csproj -c Release
```

## Consumer Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Windows
```

Replace `Windows` with the native package family for the platform that will run the application. Add companion native packages such as `SDL3-CS.Windows.Image` only when the application uses those companion bindings.

## Source Layout

- `SDL/` contains core SDL3 namespaces grouped by upstream SDL header categories.
- `Image/`, `TTF/`, `Mixer/`, and `ShaderCross/` contain companion library bindings.
- Public wrapper methods carry XML documentation; private native entry points stay implementation-focused.
- Unsafe and fixed-buffer shapes are used where SDL ABI compatibility requires them.

## Verification

Use these checks when changing this project:

```powershell
dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release
pwsh .\.github\release-tools\Test-PublicWrapperXmlDocs.ps1 -SourceRoot .\SDL3-CS
```

Wrapper behavior is covered from the mirrored test project in [`../SDL3-CS.Tests`](../SDL3-CS.Tests/README.md).

## Related Documentation

- [Repository README](../README.md)
- [NuGet README](../README-nuget.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [Official SDL3 Wiki](https://wiki.libsdl.org/SDL3/FrontPage)
