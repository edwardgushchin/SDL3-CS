# SDL3-CS.Linux

`SDL3-CS.Linux` contains native SDL runtime libraries for SDL3-CS on Linux.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Linux` | `SDL 3.4.10` |

## Platform Support

This package is scoped to Linux and contains native artifacts only for these RIDs:

- `linux-x64`
- `linux-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Linux
```

Use the same platform family for every SDL3-CS native package in one application. This is the base native runtime package for Linux.
