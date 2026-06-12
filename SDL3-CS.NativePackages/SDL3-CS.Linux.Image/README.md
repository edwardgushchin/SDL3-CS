# SDL3-CS.Linux.Image

`SDL3-CS.Linux.Image` contains native SDL_image runtime libraries for SDL3-CS on Linux.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Linux.Image` | `SDL_image 3.4.4` |

## Platform Support

This package is scoped to Linux and contains native artifacts only for these RIDs:

- `linux-x64`
- `linux-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Linux
dotnet add package SDL3-CS.Linux.Image
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.Linux`.
