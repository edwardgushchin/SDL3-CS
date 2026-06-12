# SDL3-CS.tvOS

`SDL3-CS.tvOS` contains native SDL runtime libraries for SDL3-CS on tvOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.tvOS` | `SDL 3.4.10` |

## Platform Support

This package is scoped to tvOS and contains native artifacts only for these RIDs:

- `tvos-arm64`
- `tvossimulator-arm64`
- `tvossimulator-x64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.tvOS
```

Use the same platform family for every SDL3-CS native package in one application. This is the base native runtime package for tvOS.
