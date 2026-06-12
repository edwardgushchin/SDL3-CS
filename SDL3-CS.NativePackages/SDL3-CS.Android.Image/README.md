# SDL3-CS.Android.Image

`SDL3-CS.Android.Image` contains native SDL_image runtime libraries for SDL3-CS on Android.

## Native Version

| Package | Native library version |
| --- | --- |
| `SDL3-CS.Android.Image` | `SDL_image 3.4.4` |

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
dotnet add package SDL3-CS.Android.Image
```

Use the same platform family for every SDL3-CS native package in one application. Use it with the base platform package `SDL3-CS.Android`.
