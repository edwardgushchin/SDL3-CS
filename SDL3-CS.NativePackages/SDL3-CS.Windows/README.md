# SDL3-CS.Windows

`SDL3-CS.Windows` contains native SDL runtime libraries for SDL3-CS on Windows.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Windows` | `SDL 3.4.10` |

## Platform Support

This package is scoped to Windows and contains native artifacts only for these RIDs:

- `win-x86`
- `win-x64`
- `win-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Windows
```

Use the same platform family for every SDL3-CS native package in one application. This is the base native runtime package for Windows.
