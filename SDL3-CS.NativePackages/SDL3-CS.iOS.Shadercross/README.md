# SDL3-CS.iOS.Shadercross

`SDL3-CS.iOS.Shadercross` contains native SDL_shadercross runtime libraries for SDL3-CS on iOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.iOS.Shadercross` | `SDL_shadercross 3.0.0` |

## Platform Support

This package is scoped to iOS and contains native artifacts only for these RIDs:

- `ios-arm64`
- `iossimulator-arm64`
- `iossimulator-x64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.iOS
dotnet add package SDL3-CS.iOS.Shadercross
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.iOS`.
