# SDL3-CS.MacOS

This package contains native sdl runtime libraries for sdl3-cs.

## Version Information

| Package | Native library version |
|---------|------------------------|
| SDL3-CS.MacOS | SDL 3.4.10 |

## Platform Support

This package is scoped to MacOS and contains native artifacts only for these RIDs: osx-x64, osx-arm64.

Use it together with SDL3-CS. Add-on packages may also require the base platform package $(@{Id=MacOS; PackageBase=SDL3-CS.MacOS; Rids=System.Object[]}.PackageBase).
