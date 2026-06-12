# SDL3-CS.MacOS.Mixer

`SDL3-CS.MacOS.Mixer` contains native SDL_mixer runtime libraries for SDL3-CS on MacOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.MacOS.Mixer` | `SDL_mixer 3.2.4` |

## Platform Support

This package is scoped to MacOS and contains native artifacts only for these RIDs:

- `osx-x64`
- `osx-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.MacOS
dotnet add package SDL3-CS.MacOS.Mixer
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.MacOS`.
