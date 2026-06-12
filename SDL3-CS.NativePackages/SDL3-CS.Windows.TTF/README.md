# SDL3-CS.Windows.TTF

`SDL3-CS.Windows.TTF` contains native SDL_ttf runtime libraries for SDL3-CS on Windows.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Windows.TTF` | `SDL_ttf 3.2.2` |

## Platform Support

This package is scoped to Windows and contains native artifacts only for these RIDs:

- `win-x86`
- `win-x64`
- `win-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Windows
dotnet add package SDL3-CS.Windows.TTF
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.Windows`.
