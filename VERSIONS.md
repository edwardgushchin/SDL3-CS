# SDL3-CS Version Compatibility

This document provides detailed information about version compatibility between SDL3-CS and the native SDL3 library.

## Version Matrix

| SDL3-CS Version | SDL3 Version | Release Date | Minimum glibc (Linux) | Status |
|-----------------|--------------|--------------|----------------------|--------|
| 3.4.10.x        | 3.4.10       | 2026-05-31   | 2.28 (Ubuntu 18.04+) | Current Stable |
| 3.4.2.x         | 3.4.2        | 2026-02-14   | 2.28 (Ubuntu 18.04+) | Previous Stable |

## Version Numbering

SDL3-CS follows SDL3's versioning scheme:

- **Major version** (3): Major API/ABI changes
- **Minor version** (4): New features, backward compatible
- **Patch version** (10): Bug fixes, fully compatible

The `x` in SDL3-CS version (e.g., `3.4.10.x`) represents NuGet package revisions that may include:
- C# binding improvements
- Documentation updates
- Bug fixes in the wrapper code

## Native Library Requirements

### Windows

| Architecture | Binary | Requirements |
|--------------|--------|--------------|
| x86 | SDL3.dll | Windows 10 version 1903+ |
| x64 | SDL3.dll | Windows 10 version 1903+ |
| ARM64 | SDL3.dll | Windows 10 version 1903+ |

### Linux

| Architecture | Binary | Minimum glibc | Tested Distributions |
|--------------|--------|---------------|---------------------|
| x64 | libSDL3.so | 2.28 | Ubuntu 18.04+, Debian 10+, Fedora 30+ |
| ARM64 | libSDL3.so | 2.28 | Ubuntu 18.04+, Debian 10+ |

### macOS

| Architecture | Binary | Minimum Version |
|--------------|--------|-----------------|
| x64 | libSDL3.dylib | macOS 12.0 (Monterey) |
| ARM64 | libSDL3.dylib | macOS 12.0 (Monterey) |

### Mobile and Apple TV

| Platform | RID | Binary |
|----------|-----|--------|
| Android | `android-arm` | libSDL3.so |
| Android | `android-arm64` | libSDL3.so |
| Android | `android-x86` | libSDL3.so |
| Android | `android-x64` | libSDL3.so |
| iOS | `ios-arm64` | libSDL3.a |
| iOS Simulator | `iossimulator-arm64` | libSDL3.a |
| iOS Simulator | `iossimulator-x64` | libSDL3.a |
| tvOS | `tvos-arm64` | libSDL3.a |
| tvOS Simulator | `tvossimulator-arm64` | libSDL3.a |
| tvOS Simulator | `tvossimulator-x64` | libSDL3.a |

## Building Native Libraries

### From Source

If you need to build native libraries for a specific platform or glibc version:

1. Clone the SDL3 repository:
   ```bash
   git clone -b release-3.4.10 https://github.com/edwardgushchin/SDL.git
   cd SDL
   ```

2. Build using CMake:
   ```bash
   cmake -S . -B build -DCMAKE_BUILD_TYPE=Release
   cmake --build build --parallel
   cmake --install build --prefix /path/to/install
   ```

### Docker / Steam Runtime

For building in Docker against Steam Runtime SDK:

```dockerfile
FROM steamrt/sdk:latest

RUN apt-get update && apt-get install -y \
    build-essential cmake ninja-build \
    libasound2-dev libpulse-dev libdbus-1-dev \
    libx11-dev libxext-dev libxrandr-dev \
    libwayland-dev libxkbcommon-dev

WORKDIR /build
RUN git clone -b release-3.4.10 https://github.com/edwardgushchin/SDL.git
WORKDIR /build/SDL

RUN cmake -S . -B build \
    -DCMAKE_BUILD_TYPE=Release \
    -DCMAKE_INSTALL_PREFIX=/install \
    -DSDL_INSTALL=ON

RUN cmake --build build --parallel
RUN cmake --install build
```

## Known Issues

### glibc Compatibility

If you encounter errors like `GLIBC_2.XX not found`, you need to:

1. Build native libraries against your target environment's glibc version
2. Use a container/VM with the target glibc version for building
3. Consider using the Steam Runtime SDK for a balanced glibc version

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

This release model uses only platform-scoped native package IDs.

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

## Migration Guide

### From previous root native package references

Replace previous root native package references with platform-scoped package IDs:

```bash
dotnet remove package <previous-root-native-package-id>
dotnet add package SDL3-CS.Windows
```

Use the suffix for the actual target platform. For add-ons, replace package IDs in the same way:

```bash
dotnet remove package <previous-root-native-addon-package-id>
dotnet add package SDL3-CS.Windows.Image
```

### From 3.4.2.x to 3.4.10.x

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
- Open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues)
- Join our [Telegram chat](https://t.me/sdl3cs)
- Check the [SDL3 wiki](https://wiki.libsdl.org/SDL3/FrontPage)
