# SDL3-CS Versioning and Native Compatibility

This document records how SDL3-CS package versions map to native SDL component versions and platform packages.

NuGet publication can lag behind a release branch while native packages are being assembled. Use the NuGet package pages as the authority for what is currently published, and use this file as the source-tree compatibility reference.

## Current Source-Tree Target

The current release branch targets these native component versions:

| Component | Package pattern | Native version | Package line |
|-----------|-----------------|----------------|--------------|
| SDL3 managed bindings | `SDL3-CS` | SDL `3.4.10` | `3.4.10.x` |
| SDL3 native runtime | `SDL3-CS.{Platform}` | SDL `3.4.10` | `3.4.10.x` |
| SDL_image native runtime | `SDL3-CS.{Platform}.Image` | SDL_image `3.4.4` | `3.4.4.x` |
| SDL_ttf native runtime | `SDL3-CS.{Platform}.TTF` | SDL_ttf `3.2.2` | `3.2.2.x` |
| SDL_mixer native runtime | `SDL3-CS.{Platform}.Mixer` | SDL_mixer `3.2.4` | `3.2.4.x` |
| SDL_shadercross native runtime | `SDL3-CS.{Platform}.Shadercross` | SDL_shadercross `3.0.0` | `3.0.0.x` |

`{Platform}` is one of `Windows`, `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS`.

## Version Numbering

SDL3-CS follows the native SDL component version as the package line:

- `3` is the native major version.
- `4`, `2`, or another middle segment comes from the native component version.
- The third segment comes from the native patch version.
- The final `x` segment is the SDL3-CS package revision.

For example, a package version `3.4.10.1` means:

- native component version `3.4.10`;
- SDL3-CS package revision `1`.

Package revisions may include managed binding fixes, package layout fixes, documentation updates, or release metadata changes without changing the native component baseline.

## Platform Package IDs

The managed wrapper is installed separately from native runtime assets:

```bash
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Windows
```

Use the platform suffix that matches the target application:

| Target platform | SDL package | Add-on package pattern |
|-----------------|-------------|------------------------|
| Windows | `SDL3-CS.Windows` | `SDL3-CS.Windows.{Addon}` |
| Linux | `SDL3-CS.Linux` | `SDL3-CS.Linux.{Addon}` |
| macOS | `SDL3-CS.MacOS` | `SDL3-CS.MacOS.{Addon}` |
| Android | `SDL3-CS.Android` | `SDL3-CS.Android.{Addon}` |
| iOS | `SDL3-CS.iOS` | `SDL3-CS.iOS.{Addon}` |
| tvOS | `SDL3-CS.tvOS` | `SDL3-CS.tvOS.{Addon}` |

Add-on names are `Image`, `TTF`, `Mixer`, and `Shadercross`.

Examples:

```bash
dotnet add package SDL3-CS.Windows.Image
dotnet add package SDL3-CS.Android.TTF
dotnet add package SDL3-CS.MacOS.Mixer
```

The older root-style native package IDs such as `SDL3-CS.Native`, `SDL3-CS.Native.Image`, and `SDL3-CS.Native.TTF` are not used by this release model.

## Supported Native Targets

Official native package families are built for the active release tier:

| Platform family | RIDs / ABIs | Native asset shape |
|-----------------|-------------|--------------------|
| Windows | `win-x86`, `win-x64`, `win-arm64` | Dynamic libraries such as `SDL3.dll`. |
| Linux | `linux-x64`, `linux-arm64` | Dynamic libraries such as `libSDL3.so`; built against glibc 2.28 or newer. |
| macOS | `osx-x64`, `osx-arm64` | Dynamic libraries such as `libSDL3.dylib`. |
| Android | `android-arm`, `android-arm64`, `android-x86`, `android-x64` | ABI-specific `.so` files plus SDL Android bridge bindings in `SDL3-CS.Android`. |
| iOS | `ios-arm64`, `iossimulator-arm64`, `iossimulator-x64` | Static libraries linked through package build targets. |
| tvOS | `tvos-arm64`, `tvossimulator-arm64`, `tvossimulator-x64` | Static libraries linked through package build targets. |

Other platforms can use the managed bindings if the application provides compatible SDL native libraries manually.

## Linux glibc Compatibility

Official Linux native assets target glibc 2.28 or newer. If an application runs on an older distribution and fails with an error such as `GLIBC_2.XX not found`, build SDL native libraries in an environment that matches the target runtime.

Common options:

1. Build against the target distribution or container image.
2. Use a VM or CI image with the required glibc baseline.
3. Use the Steam Runtime SDK when that baseline matches the deployment target.

## Building Native Libraries Manually

Custom native builds should match the native component version used by the managed wrapper and platform package line.

Example for SDL `3.4.10`:

```bash
git clone -b release-3.4.10 https://github.com/edwardgushchin/SDL.git
cd SDL
cmake -S . -B build -DCMAKE_BUILD_TYPE=Release -DSDL_INSTALL=ON
cmake --build build --parallel
cmake --install build --prefix /path/to/install
```

When building add-ons, use the matching upstream component version from the current source-tree target table.

## Migration Notes

### From the old root native packages

Replace root native package IDs with platform-scoped package IDs:

```bash
dotnet remove package SDL3-CS.Native
dotnet add package SDL3-CS.Windows
```

Use the suffix for the actual target platform. For add-ons, replace package IDs in the same way:

```bash
dotnet remove package SDL3-CS.Native.Image
dotnet add package SDL3-CS.Windows.Image
```

### From SDL3-CS `3.4.2.x` to `3.4.10.x`

Update the managed wrapper and matching platform runtime packages together:

```bash
dotnet add package SDL3-CS --version 3.4.10.*
dotnet add package SDL3-CS.Windows --version 3.4.10.*
```

Then update add-on packages to their matching native component lines:

```bash
dotnet add package SDL3-CS.Windows.Image --version 3.4.4.*
dotnet add package SDL3-CS.Windows.TTF --version 3.2.2.*
dotnet add package SDL3-CS.Windows.Mixer --version 3.2.4.*
dotnet add package SDL3-CS.Windows.Shadercross --version 3.0.0.*
```

If you supply native libraries manually, replace them with binaries built from the matching native versions.

For upstream API changes, see the official [SDL3 migration guide](https://wiki.libsdl.org/SDL3/MigrationGuide).

## Support

For version-related questions:

- open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues);
- start a [discussion](https://github.com/edwardgushchin/SDL3-CS/discussions);
- join the [Telegram chat](https://t.me/sdl3cs);
- check the upstream [SDL3 Wiki](https://wiki.libsdl.org/SDL3/FrontPage).
