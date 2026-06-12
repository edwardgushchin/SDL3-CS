# SDL3-CS.tvOS.Shadercross

`SDL3-CS.tvOS.Shadercross` contains native SDL_shadercross runtime libraries for SDL3-CS on tvOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.tvOS.Shadercross` | `SDL_shadercross 3.0.0` |

## Platform Support

This package is scoped to tvOS and contains native artifacts only for these RIDs:

- `tvos-arm64`
- `tvossimulator-arm64`
- `tvossimulator-x64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.tvOS
dotnet add package SDL3-CS.tvOS.Shadercross
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.tvOS`.
