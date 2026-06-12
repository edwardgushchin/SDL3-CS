# SDL3-CS.MacOS.TTF

`SDL3-CS.MacOS.TTF` contains native SDL_ttf runtime libraries for SDL3-CS on MacOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.MacOS.TTF` | `SDL_ttf 3.2.2` |

## Platform Support

This package is scoped to MacOS and contains native artifacts only for these RIDs:

- `osx-x64`
- `osx-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.MacOS
dotnet add package SDL3-CS.MacOS.TTF
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.MacOS`.
