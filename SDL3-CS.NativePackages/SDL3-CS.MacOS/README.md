# SDL3-CS.MacOS

`SDL3-CS.MacOS` contains native SDL runtime libraries for SDL3-CS on MacOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.MacOS` | `SDL 3.4.10` |

## Platform Support

This package is scoped to MacOS and contains native artifacts only for these RIDs:

- `osx-x64`
- `osx-arm64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.MacOS
```

Use the same platform family for every SDL3-CS native package in one application. This is the base native runtime package for MacOS.
