# SDL3-CS.Linux.Mixer

`SDL3-CS.Linux.Mixer` contains native SDL_mixer runtime libraries for SDL3-CS on Linux.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Linux.Mixer` | `SDL_mixer 3.2.4` |

## Platform Support

This package is scoped to Linux and contains native artifacts only for these RIDs:

- `linux-x64`
- `linux-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Linux
dotnet add package SDL3-CS.Linux.Mixer
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.Linux`.
