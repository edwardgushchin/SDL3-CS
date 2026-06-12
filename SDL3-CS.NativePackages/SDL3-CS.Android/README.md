# SDL3-CS.Android

`SDL3-CS.Android` contains native SDL runtime libraries for SDL3-CS on Android.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Android` | `SDL 3.4.10` |

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
```

Use the same platform family for every SDL3-CS native package in one application. This is the base native runtime package for Android.
Android applications should use `MainActivity : Org.Libsdl.App.SDLActivity` and override managed `Main()`. This package also carries the SDL Android Java bridge.
