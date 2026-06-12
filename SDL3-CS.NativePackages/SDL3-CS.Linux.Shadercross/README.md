# SDL3-CS.Linux.Shadercross

`SDL3-CS.Linux.Shadercross` contains native SDL_shadercross runtime libraries for SDL3-CS on Linux.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Linux.Shadercross` | `SDL_shadercross 3.0.0` |

## Platform Support

This package is scoped to Linux and contains native artifacts only for these RIDs:

- `linux-x64`
- `linux-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Linux
dotnet add package SDL3-CS.Linux.Shadercross
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.Linux`.
