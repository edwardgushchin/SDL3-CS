# SDL3-CS.tvOS.Mixer

`SDL3-CS.tvOS.Mixer` contains native SDL_mixer runtime libraries for SDL3-CS on tvOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.tvOS.Mixer` | `SDL_mixer 3.2.4` |

## Platform Support

This package is scoped to tvOS and contains native artifacts only for these RIDs:

- `tvos-arm64`
- `tvossimulator-arm64`
- `tvossimulator-x64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.tvOS
dotnet add package SDL3-CS.tvOS.Mixer
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.tvOS`.
