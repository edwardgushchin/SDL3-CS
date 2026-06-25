# AndroidCircularColorFade

<p align="center">
  <strong>AndroidCircularColorFade</strong>
</p>

<p align="center">
  <img alt="SDL3-CS example" src="https://img.shields.io/badge/SDL3-CS%20example-555?style=flat-square">
  <img alt="Android" src="https://img.shields.io/badge/Android-555?style=flat-square">
  <img alt="net10.0-android" src="https://img.shields.io/badge/net10.0-android-555?style=flat-square">
  <img alt="C# 14" src="https://img.shields.io/badge/C%23%2014-555?style=flat-square">
</p>

AndroidCircularColorFade is the Android version of the SDL3-CS circular color fade sample. It uses the official SDLActivity bridge supplied by the Android native package and runs the render loop from managed C#.

## What This Project Demonstrates

- Hosting an SDL3-CS app inside `Org.Libsdl.App.SDLActivity`.
- Loading `libSDL3.so` through `GetLibraries()`.
- Creating an SDL window and renderer from an Android activity.
- Animating the clear color with SDL performance counters.

## Expected Result

The Android activity opens a full SDL surface and smoothly cycles the background color.

## Project Model

This project is part of the SDL3-CS examples tree. The common example configuration lives in [`../../Directory.Build.props`](../../Directory.Build.props) and [`../../Directory.Build.targets`](../../Directory.Build.targets):

- examples default to `net10.0`, `Exe`, nullable reference types, implicit usings, and C# 14;
- local source builds reference [`SDL3-CS`](../../../SDL3-CS/SDL3-CS.csproj) directly;
- desktop examples receive the matching native runtime package for Windows, Linux, or macOS;
- optional SDL companion runtimes are added only when an example opts into them.

Runtime dependency for this example: `SDL3-CS.Android` runtime and SDLActivity bridge package.

## Source Files

- `MainActivity.cs`

## Assets

- `Resources/AboutResources.txt`
- `Resources/mipmap-hdpi/appicon.png`
- `Resources/mipmap-hdpi/appicon_background.png`
- `Resources/mipmap-hdpi/appicon_foreground.png`
- `Resources/mipmap-mdpi/appicon.png`
- `Resources/mipmap-mdpi/appicon_background.png`
- `Resources/mipmap-mdpi/appicon_foreground.png`
- `Resources/mipmap-xhdpi/appicon.png`
- Additional Android resource density assets live under `Resources/`.

## Requirements

- .NET 10 SDK.
- A platform supported by the SDL3-CS native packages.
- For desktop examples: Windows, Linux, or macOS with a graphics environment available.
- For device-specific examples: the matching device or host capability, such as camera, gamepad, joystick, pen, or audio output.

## Run

From the repository root:

```powershell
dotnet workload restore .\SDL3-CS.Examples\Android\AndroidCircularColorFade\AndroidCircularColorFade.csproj
dotnet build .\SDL3-CS.Examples\Android\AndroidCircularColorFade\AndroidCircularColorFade.csproj -c Release -f net10.0-android
```

Deploy from your IDE, from the .NET Android tooling, or by adding the usual Android publish properties for your device or emulator.

## Notes

- This README replaces the previous non-English Android example notes from issue #192.
- The bridge is supplied by `SDL3-CS.Android`; it is not copied into the example source tree.
- The Android project targets API level 23 or newer and enables full trimming in Release builds.

## Related Documentation

- [Repository README](../../../README.md)
- [Examples inventory](../../UPSTREAM-SDL3-EXAMPLES.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [Official SDL examples](https://examples.libsdl.org/SDL3/)
