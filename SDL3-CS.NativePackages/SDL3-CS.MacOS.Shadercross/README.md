# SDL3-CS.MacOS.Shadercross

`SDL3-CS.MacOS.Shadercross` contains native SDL_shadercross runtime libraries for SDL3-CS on MacOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.MacOS.Shadercross` | `SDL_shadercross 3.0.0` |

## Platform Support

This package is scoped to MacOS and contains native artifacts only for these RIDs:

- `osx-x64`
- `osx-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.MacOS
dotnet add package SDL3-CS.MacOS.Shadercross
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.MacOS`.
