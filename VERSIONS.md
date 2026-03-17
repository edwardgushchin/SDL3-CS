# SDL3-CS Version Compatibility

This document provides detailed information about version compatibility between SDL3-CS and the native SDL3 library.

## Version Matrix

| SDL3-CS Version | SDL3 Version | Release Date | Minimum glibc (Linux) | Status |
|-----------------|--------------|--------------|----------------------|--------|
| 3.4.2.x         | 3.4.2        | 2026-02-14   | 2.28 (Ubuntu 18.04+) | Current Stable |
| 3.4.0.x         | 3.4.0        | 2025-12-01   | 2.28 (Ubuntu 18.04+) | Previous Stable |

## Version Numbering

SDL3-CS follows SDL3's versioning scheme:

- **Major version** (3): Major API/ABI changes
- **Minor version** (4): New features, backward compatible
- **Patch version** (2): Bug fixes, fully compatible

The `x` in SDL3-CS version (e.g., `3.4.2.x`) represents NuGet package revisions that may include:
- C# binding improvements
- Documentation updates
- Bug fixes in the wrapper code

## Native Library Requirements

### Windows

| Architecture | Binary | Requirements |
|--------------|--------|--------------|
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

## Building Native Libraries

### From Source

If you need to build native libraries for a specific platform or glibc version:

1. Clone the SDL3 repository:
   ```bash
   git clone -b release-3.4.2 https://github.com/edwardgushchin/SDL.git
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
RUN git clone -b release-3.4.2 https://github.com/edwardgushchin/SDL.git
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

## Migration Guide

### From 3.4.0.x to 3.4.2.x

1. Update NuGet package:
   ```bash
   dotnet add package SDL3-CS --version 3.4.2.*
   dotnet add package SDL3-CS.Native --version 3.4.2.*
   ```

2. Replace native libraries if using custom builds

3. Check for any API changes in the [SDL3 migration guide](https://wiki.libsdl.org/SDL3/MigrationGuide)

## Support

For version-related questions:
- Open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues)
- Join our [Telegram chat](https://t.me/sdl3cs)
- Check the [SDL3 wiki](https://wiki.libsdl.org/SDL3/FrontPage)
