## About

SDL3-CS is a C# wrapper for SDL3. The managed `SDL3-CS` package contains the C# wrapper, while native binaries are published as platform-specific package families.

## Latest Release

Current stable release: [`SDL3-CS 3.4.12.1`](https://www.nuget.org/packages/SDL3-CS/3.4.12.1).

| Package family | Version |
|----------------|---------|
| `SDL3-CS` | `3.4.12.1` |
| `SDL3-CS.<Platform>` | `3.4.12.1` |
| `SDL3-CS.<Platform>.Image` | `3.4.4.7` |
| `SDL3-CS.<Platform>.Mixer` | `3.2.4.7` |
| `SDL3-CS.<Platform>.TTF` | `3.2.2.7` |
| `SDL3-CS.<Platform>.Shadercross` | `3.0.0.7` |

## Documentation

- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [SDL3-CS API Reference](https://github.com/edwardgushchin/SDL3-CS/wiki/API-Reference)
- [SDL3 upstream wiki](https://wiki.libsdl.org/SDL3/FrontPage)
- [GitHub Releases](https://github.com/edwardgushchin/SDL3-CS/releases)

## Supported Platforms

The managed wrapper targets .NET 7, .NET 8, .NET 9, and .NET 10.

| Platform family | Native package suffix | Supported RIDs / ABIs | Notes |
|-----------------|-----------------------|------------------------|-------|
| Windows | `Windows` | `win-x86`, `win-x64`, `win-arm64` | Dynamic SDL libraries for desktop Windows apps. |
| Linux | `Linux` | `linux-x64`, `linux-arm64` | Built against glibc 2.28 or newer. |
| macOS | `MacOS` | `osx-x64`, `osx-arm64` | Dynamic SDL libraries for Intel and Apple Silicon macOS apps. |
| Android | `Android` | `android-arm`, `android-arm64`, `android-x86`, `android-x64` | Includes the SDL Android bridge in `SDL3-CS.Android`. |
| iOS | `iOS` | `ios-arm64`, `iossimulator-arm64`, `iossimulator-x64` | Static native assets are linked through package `buildTransitive` targets. |
| tvOS | `tvOS` | `tvos-arm64`, `tvossimulator-arm64`, `tvossimulator-x64` | Static native assets are linked through package `buildTransitive` targets. |

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

Replace `Windows` with `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS`.

| Platform | SDL | SDL_image | SDL_ttf | SDL_mixer | SDL_shadercross |
|----------|-----|-----------|---------|-----------|-----------------|
| Windows | `SDL3-CS.Windows` | `SDL3-CS.Windows.Image` | `SDL3-CS.Windows.TTF` | `SDL3-CS.Windows.Mixer` | `SDL3-CS.Windows.Shadercross` |
| Linux | `SDL3-CS.Linux` | `SDL3-CS.Linux.Image` | `SDL3-CS.Linux.TTF` | `SDL3-CS.Linux.Mixer` | `SDL3-CS.Linux.Shadercross` |
| macOS | `SDL3-CS.MacOS` | `SDL3-CS.MacOS.Image` | `SDL3-CS.MacOS.TTF` | `SDL3-CS.MacOS.Mixer` | `SDL3-CS.MacOS.Shadercross` |
| Android | `SDL3-CS.Android` | `SDL3-CS.Android.Image` | `SDL3-CS.Android.TTF` | `SDL3-CS.Android.Mixer` | `SDL3-CS.Android.Shadercross` |
| iOS | `SDL3-CS.iOS` | `SDL3-CS.iOS.Image` | `SDL3-CS.iOS.TTF` | `SDL3-CS.iOS.Mixer` | `SDL3-CS.iOS.Shadercross` |
| tvOS | `SDL3-CS.tvOS` | `SDL3-CS.tvOS.Image` | `SDL3-CS.tvOS.TTF` | `SDL3-CS.tvOS.Mixer` | `SDL3-CS.tvOS.Shadercross` |

Android applications use `MainActivity : Org.Libsdl.App.SDLActivity`, override `GetLibraries()`, and run SDL from the managed `Main()` override.

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
        SDL.RenderClear(renderer);
        SDL.RenderPresent(renderer);

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        SDL.Quit();
    }
}
```

## Feedback and Contributions

Open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues) or start a [discussion](https://github.com/edwardgushchin/SDL3-CS/discussions) for bugs, ideas, and questions.

See the [repository README](https://github.com/edwardgushchin/SDL3-CS#readme) for the full project overview.
