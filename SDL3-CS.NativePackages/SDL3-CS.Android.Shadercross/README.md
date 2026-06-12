# SDL3-CS.Android.Shadercross

`SDL3-CS.Android.Shadercross` contains native SDL_shadercross runtime libraries for SDL3-CS on Android.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Android.Shadercross` | `SDL_shadercross 3.0.0` |

## Platform Support

This package is scoped to Android and contains native artifacts only for these RIDs:

- `android-arm`
- `android-arm64`
- `android-x86`
- `android-x64`

## Usage

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Android
dotnet add package SDL3-CS.Android.Shadercross
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.Android`.
