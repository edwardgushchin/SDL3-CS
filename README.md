<p align="center">
  <img src="./logo.png?raw=true" alt="SDL3#">
</p>

<h4 align="center">SDL3# is a C# wrapper for SDL3.</h4>

<p align="center">
    <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/edwardgushchin/SDL3-CS">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/edwardgushchin/SDL3-CS">
    <img alt="License: zlib" src="https://img.shields.io/badge/license-zlib-blue">
</p>

<p align="center">
    <img alt=".NET 7, 8, 9, 10" src="https://img.shields.io/badge/.NET-7.0,_8.0,_9.0,_10.0-512BD4">
    <img alt="C# 14" src="https://img.shields.io/badge/Language-C%23_14-239120">
    <img alt="SDL 3.4.10" src="https://img.shields.io/badge/SDL-3.4.10-239120">
</p>

<p align="center">
  <a href="#about">About</a> -
  <a href="#latest-release">Latest Release</a> -
  <a href="#documentation">Documentation</a> -
  <a href="#supported-platforms">Supported Platforms</a> -
  <a href="#installation">Installation</a> -
  <a href="#example">Example</a>
</p>

## About

SDL3-CS exposes SDL3, SDL_image, SDL_mixer, SDL_ttf, and SDL_shadercross to .NET applications through C# bindings and NuGet packages.

The managed `SDL3-CS` package contains the C# wrapper. Native binaries are published separately as platform-specific package families so applications can reference only the platforms they ship:

- `SDL3-CS.<Platform>` for SDL.
- `SDL3-CS.<Platform>.Image` for SDL_image.
- `SDL3-CS.<Platform>.Mixer` for SDL_mixer.
- `SDL3-CS.<Platform>.TTF` for SDL_ttf.
- `SDL3-CS.<Platform>.Shadercross` for SDL_shadercross.

Supported platform suffixes are `Windows`, `Linux`, `MacOS`, `Android`, `iOS`, and `tvOS`.

## Latest Release

Current stable release: [`SDL3-CS 3.4.10.1`](https://github.com/edwardgushchin/SDL3-CS/releases/tag/v3.4.10.1).

| Package family | Version |
|----------------|---------|
| `SDL3-CS` | `3.4.10.1` |
| `SDL3-CS.<Platform>` | `3.4.10.1` |
| `SDL3-CS.<Platform>.Image` | `3.4.4.1` |
| `SDL3-CS.<Platform>.Mixer` | `3.2.4.1` |
| `SDL3-CS.<Platform>.TTF` | `3.2.2.1` |
| `SDL3-CS.<Platform>.Shadercross` | `3.0.0.1` |

### SDL 3.4.10 Changes

The SDL3-CS `3.4.10.1` release carries the upstream SDL 3.4.10 stable bugfix release. The GitHub release notes list the upstream changes with SDL3-CS C# names where public wrapper symbols apply.

Highlights include GPU depth texture arrays, Vulkan swapchain copy fixes, packed 16-bit texture formats in the Metal renderer, Windows cursor and Korean IME fixes, GameInput v3 controller sensors, Android gamepad motion sensors, controller compatibility fixes, X11 focus/clickthrough fixes, and a Raspberry Pi window creation crash fix.

## Documentation

- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [SDL3-CS API Reference](https://github.com/edwardgushchin/SDL3-CS/wiki/API-Reference)
- [SDL3 upstream wiki](https://wiki.libsdl.org/SDL3/FrontPage)
- [GitHub Releases](https://github.com/edwardgushchin/SDL3-CS/releases)

## Supported Platforms

The managed wrapper targets .NET 7, .NET 8, .NET 9, and .NET 10.

Official native NuGet packages are built by GitHub Actions for these platform families:

| Platform family | Native package suffix | Supported RIDs / ABIs | Notes |
|-----------------|-----------------------|------------------------|-------|
| Windows | `Windows` | `win-x86`, `win-x64`, `win-arm64` | Dynamic SDL libraries for desktop Windows apps. |
| Linux | `Linux` | `linux-x64`, `linux-arm64` | Built against glibc 2.28 or newer. |
| macOS | `MacOS` | `osx-x64`, `osx-arm64` | Dynamic SDL libraries for Intel and Apple Silicon macOS apps. |
| Android | `Android` | `android-arm`, `android-arm64`, `android-x86`, `android-x64` | Includes the SDL Android bridge in `SDL3-CS.Android`. |
| iOS | `iOS` | `ios-arm64`, `iossimulator-arm64`, `iossimulator-x64` | Static native assets are linked through package `buildTransitive` targets. |
| tvOS | `tvOS` | `tvos-arm64`, `tvossimulator-arm64`, `tvossimulator-x64` | Static native assets are linked through package `buildTransitive` targets. |

Other platforms can still use the managed wrapper if the application supplies compatible SDL native libraries manually.

## Installation

Install the managed wrapper:

```bash
dotnet add package SDL3-CS
```

Add the native package family that matches your target platform. For a Windows desktop app:

```bash
dotnet add package SDL3-CS.Windows
dotnet add package SDL3-CS.Windows.Image
dotnet add package SDL3-CS.Windows.TTF
dotnet add package SDL3-CS.Windows.Mixer
dotnet add package SDL3-CS.Windows.Shadercross
```

Replace `Windows` with `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS` for another platform family.

| Platform | SDL | SDL_image | SDL_ttf | SDL_mixer | SDL_shadercross |
|----------|-----|-----------|---------|-----------|-----------------|
| Windows | `SDL3-CS.Windows` | `SDL3-CS.Windows.Image` | `SDL3-CS.Windows.TTF` | `SDL3-CS.Windows.Mixer` | `SDL3-CS.Windows.Shadercross` |
| Linux | `SDL3-CS.Linux` | `SDL3-CS.Linux.Image` | `SDL3-CS.Linux.TTF` | `SDL3-CS.Linux.Mixer` | `SDL3-CS.Linux.Shadercross` |
| macOS | `SDL3-CS.MacOS` | `SDL3-CS.MacOS.Image` | `SDL3-CS.MacOS.TTF` | `SDL3-CS.MacOS.Mixer` | `SDL3-CS.MacOS.Shadercross` |
| Android | `SDL3-CS.Android` | `SDL3-CS.Android.Image` | `SDL3-CS.Android.TTF` | `SDL3-CS.Android.Mixer` | `SDL3-CS.Android.Shadercross` |
| iOS | `SDL3-CS.iOS` | `SDL3-CS.iOS.Image` | `SDL3-CS.iOS.TTF` | `SDL3-CS.iOS.Mixer` | `SDL3-CS.iOS.Shadercross` |
| tvOS | `SDL3-CS.tvOS` | `SDL3-CS.tvOS.Image` | `SDL3-CS.tvOS.TTF` | `SDL3-CS.tvOS.Mixer` | `SDL3-CS.tvOS.Shadercross` |

Android applications use `MainActivity : Org.Libsdl.App.SDLActivity`, override `GetLibraries()`, and run SDL from the managed `Main()` override.

To build the repository from source:

```bash
git clone https://github.com/edwardgushchin/SDL3-CS
cd SDL3-CS
dotnet build -c Release
```

## Example

```csharp
using SDL3;

namespace CreateWindow;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("SDL3 Create Window", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and renderer: {SDL.GetError()}");
            return;
        }

        SDL.SetRenderDrawColor(renderer, 100, 149, 237, 255);

        var loop = true;
        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
                if ((SDL.EventType)e.Type == SDL.EventType.Quit)
                {
                    loop = false;
                }
            }

            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        SDL.Quit();
    }
}
```

More examples are available in [`SDL3-CS.Examples`](https://github.com/edwardgushchin/SDL3-CS/tree/main/SDL3-CS.Examples).

## Feedback and Contributions

Open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues) or start a [discussion](https://github.com/edwardgushchin/SDL3-CS/discussions) for bugs, ideas, and questions.

Please follow the [Code of Conduct](CODE_OF_CONDUCT.md) in all project spaces.

You can also join the [Telegram chat](https://t.me/sdl3cs).

## Authors

- Eduard Gushchin - initial work - [edwardgushchin](https://github.com/edwardgushchin)

See also the [contributors](https://github.com/edwardgushchin/SDL3-CS/graphs/contributors).

## License

SDL3 and SDL3# are released under the zlib license. See [LICENSE](LICENSE) for details.
