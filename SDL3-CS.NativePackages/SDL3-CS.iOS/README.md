# SDL3-CS.iOS

`SDL3-CS.iOS` contains native SDL runtime libraries for SDL3-CS on iOS.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.iOS` | `SDL 3.4.10` |

## Platform Support

This package is scoped to iOS and contains native artifacts only for these RIDs:

- `ios-arm64`
- `iossimulator-arm64`
- `iossimulator-x64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.iOS
```

Use the same platform family for every SDL3-CS native package in one application. This is the base native runtime package for iOS.
