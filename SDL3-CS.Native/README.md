# SDL3-CS.Native

This package contains native SDL libraries for SDL3-CS.

## Version Information

| Package | Native library version |
|---------|--------------|
| SDL3-CS.Native | SDL 3.4.2 |
| SDL3-CS.Native.Image | SDL_image 3.4.4 |
| SDL3-CS.Native.Mixer | SDL_mixer 3.2.4 |
| SDL3-CS.Native.TTF | SDL_ttf 3.2.2 |
| SDL3-CS.Native.Shadercross | SDL_shadercross 3.0.0 |

## Platform Support

| Platform | Architecture | Binary |
|----------|--------------|--------|
| Windows | x86 | SDL3.dll |
| Windows | x64 | SDL3.dll |
| Windows | ARM64 | SDL3.dll |
| Linux | x64 | libSDL3.so |
| Linux | ARM64 | libSDL3.so |
| macOS | x64 | libSDL3.dylib |
| macOS | ARM64 | libSDL3.dylib |
| Android | arm, arm64, x86, x64 | libSDL3.so |
| iOS | arm64, simulator arm64, simulator x64 | libSDL3.a |
| tvOS | arm64, simulator arm64, simulator x64 | libSDL3.a |

## Building from Source

If you need to build the native libraries yourself (e.g., for a custom Linux distribution or specific glibc version), refer to the [build workflows](https://github.com/edwardgushchin/SDL3-CS/tree/master/.github/workflows) for build instructions.

**Minimum glibc requirement:** 2.28 (Ubuntu 18.04+)

For more detailed version compatibility information, see [VERSIONS.md](https://github.com/edwardgushchin/SDL3-CS/blob/master/VERSIONS.md).
