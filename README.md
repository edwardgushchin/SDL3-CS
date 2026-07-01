<p align="center">
  <img src="./logo.png?raw=true" alt="SDL3#" width="520">
</p>

<h3 align="center">Modern C# bindings for SDL3 and its companion libraries.</h3>

<p align="center">
  Build cross-platform .NET applications with SDL3, SDL_image, SDL_ttf, SDL_mixer, and SDL_shadercross.
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/SDL3-CS">
    <img alt="NuGet SDL3-CS version" src="https://img.shields.io/nuget/v/SDL3-CS?style=flat-square">
  </a>
  <a href="https://www.nuget.org/packages/SDL3-CS">
    <img alt="NuGet SDL3-CS downloads" src="https://img.shields.io/nuget/dt/SDL3-CS?style=flat-square">
  </a>
  <img alt="zlib license" src="https://img.shields.io/badge/license-zlib-blue?style=flat-square">
  <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/edwardgushchin/SDL3-CS?style=flat-square">
  <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/edwardgushchin/SDL3-CS?style=flat-square">
</p>

<p align="center">
  <img alt=".NET 7, 8, 9, and 10" src="https://img.shields.io/badge/.NET-7.0%20%7C%208.0%20%7C%209.0%20%7C%2010.0-512BD4?style=flat-square">
  <img alt="C# 14" src="https://img.shields.io/badge/C%23-14-239120?style=flat-square">
  <img alt="SDL3 target 3.4.10" src="https://img.shields.io/badge/SDL3%20target-3.4.10-239120?style=flat-square">
  <img alt="SDL companion libraries" src="https://img.shields.io/badge/addons-image%20%7C%20ttf%20%7C%20mixer%20%7C%20shadercross-555?style=flat-square">
</p>

<p align="center">
  <img alt="Windows win-x86, win-x64, win-arm64" src="https://img.shields.io/badge/Windows-win--x86%20%7C%20win--x64%20%7C%20win--arm64-0078D6?style=flat-square">
  <img alt="Linux x64 and arm64" src="https://img.shields.io/badge/Linux-x64%20%7C%20arm64-FCC624?style=flat-square">
  <img alt="macOS x64 and arm64" src="https://img.shields.io/badge/macOS-x64%20%7C%20arm64-000000?style=flat-square">
  <img alt="Android arm, arm64, x86, x64" src="https://img.shields.io/badge/Android-arm%20%7C%20arm64%20%7C%20x86%20%7C%20x64-3DDC84?style=flat-square">
  <img alt="iOS device and simulator" src="https://img.shields.io/badge/iOS-device%20%7C%20simulator-lightgrey?style=flat-square">
  <img alt="tvOS device and simulator" src="https://img.shields.io/badge/tvOS-device%20%7C%20simulator-lightgrey?style=flat-square">
</p>

<p align="center">
  <a href="#-about">About</a> -
  <a href="#-versioning-and-native-compatibility">Versioning</a> -
  <a href="#-documentation">Documentation</a> -
  <a href="#-supported-platforms">Platforms</a> -
  <a href="#-installation">Installation</a> -
  <a href="#-examples">Examples</a> -
  <a href="#-feedback-and-contributions">Feedback</a> -
  <a href="#-license">License</a>
</p>

<p align="center">⭐ <a href="https://github.com/edwardgushchin/SDL3-CS">Star us on GitHub</a> - it motivates us a lot!</p>

## 🚀 About

SDL3# is a C# wrapper and native package set for SDL3. It gives .NET applications direct access to SDL3 APIs while keeping native runtime distribution predictable across desktop, mobile, and Apple TV targets.

The repository contains:

- managed C# bindings for SDL3, SDL_image, SDL_ttf, SDL_mixer, and SDL_shadercross;
- platform-specific native NuGet packages for Windows, Linux, macOS, Android, iOS, and tvOS;
- Android SDLActivity bridge bindings for managed Android applications;
- examples that cover window creation, rendering, input, audio, images, fonts, GPU usage, and mobile app setup;
- release tooling and tests that validate package layout, native assets, and wrapper metadata.

SDL3# is intended for developers who want low-level SDL3 access from modern .NET without maintaining a separate native binary distribution pipeline for every supported platform.

## 🔢 Versioning and Native Compatibility

SDL3# package versions follow the native SDL component versions. The first three version segments identify the upstream native component version. The final segment is the SDL3# package revision and may contain binding, packaging, or documentation fixes.

This source tree targets the following release lines:

| Component | Package pattern | Native target | Package line |
|-----------|-----------------|---------------|--------------|
| SDL3 managed bindings | `SDL3-CS` | SDL `3.4.10` | `3.4.10.x` |
| SDL3 native runtime | `SDL3-CS.{Platform}` | SDL `3.4.10` | `3.4.10.x` |
| SDL_image native runtime | `SDL3-CS.{Platform}.Image` | SDL_image `3.4.4` | `3.4.4.x` |
| SDL_ttf native runtime | `SDL3-CS.{Platform}.TTF` | SDL_ttf `3.2.2` | `3.2.2.x` |
| SDL_mixer native runtime | `SDL3-CS.{Platform}.Mixer` | SDL_mixer `3.2.4` | `3.2.4.x` |
| SDL_shadercross native runtime | `SDL3-CS.{Platform}.Shadercross` | SDL_shadercross `3.0.0` | `3.0.0.x` |

`{Platform}` is one of `Windows`, `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS`.

Published NuGet packages can lag behind a release branch while native packages are being assembled. The NuGet badge, package pages, and [GitHub Releases](https://github.com/edwardgushchin/SDL3-CS/releases) are authoritative for what is currently published.

## 📚 Documentation

Project documentation lives in the [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki). Use the wiki, examples, and [GitHub Releases](https://github.com/edwardgushchin/SDL3-CS/releases) for version notes, migration guidance, platform details, and release documentation.

For upstream SDL API documentation, see the official [SDL3 Wiki](https://wiki.libsdl.org/SDL3/FrontPage).

## 🧭 Supported Platforms

The managed `SDL3-CS` wrapper targets .NET 7, .NET 8, .NET 9, and .NET 10.

Official native package families are built for the following release targets:

| Platform family | Package suffix | Supported RIDs / ABIs | Notes |
|-----------------|----------------|------------------------|-------|
| Windows | `Windows` | `win-x86`, `win-x64`, `win-arm64` | Dynamic SDL libraries for desktop Windows apps. |
| Linux | `Linux` | `linux-x64`, `linux-arm64` | Built against glibc 2.28 or newer. |
| macOS | `MacOS` | `osx-x64`, `osx-arm64` | Dynamic SDL libraries for Intel and Apple Silicon macOS apps. |
| Android | `Android` | `android-arm` (`armeabi-v7a`), `android-arm64` (`arm64-v8a`), `android-x86` (`x86`), `android-x64` (`x86_64`) | Includes SDL Android bridge bindings and ABI-specific native libraries. |
| iOS | `iOS` | `ios-arm64`, `iossimulator-arm64`, `iossimulator-x64` | Static native assets are linked through package build targets. |
| tvOS | `tvOS` | `tvos-arm64`, `tvossimulator-arm64`, `tvossimulator-x64` | Static native assets are linked through package build targets. |

Other platforms can use the managed bindings if the application supplies compatible SDL native libraries manually.

## 📝 Installation

Install the managed bindings:

```bash
dotnet add package SDL3-CS
```

Add the native package for your target platform:

```bash
dotnet add package SDL3-CS.Windows
```

Replace `Windows` with the package suffix for your target platform:

| Target platform | Package suffix |
|-----------------|----------------|
| Windows | `Windows` |
| Linux | `Linux` |
| macOS | `MacOS` |
| Android | `Android` |
| iOS | `iOS` |
| tvOS | `tvOS` |

Optional SDL companion libraries use the same platform suffix:

```bash
dotnet add package SDL3-CS.Windows.Image
dotnet add package SDL3-CS.Windows.TTF
dotnet add package SDL3-CS.Windows.Mixer
dotnet add package SDL3-CS.Windows.Shadercross
```

Use the same platform suffix for every SDL3# package in the same application.

### Android

Android applications should reference `SDL3-CS.Android` and use `MainActivity : Org.Libsdl.App.SDLActivity` with a managed `Main()` override. The Android package includes the SDL Java bridge bindings and ABI-specific `libSDL3.so` files.

### Build from Source

```bash
git clone https://github.com/edwardgushchin/SDL3-CS
cd SDL3-CS
dotnet build SDL3-CS.sln -c Release
```

## 🎓 Examples

```csharp
using SDL3;

namespace Create_Window;

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
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
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

More examples can be found in [SDL3-CS.Examples](https://github.com/edwardgushchin/SDL3-CS/tree/main/SDL3-CS.Examples).

## 🤝 Feedback and Contributions

Found a bug or have an idea? Open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues) or start a [discussion](https://github.com/edwardgushchin/SDL3-CS/discussions).

Please follow the [Code of Conduct](CODE_OF_CONDUCT.md) in all project interactions.

You can contact the maintainer at [eduardgushchin@yandex.ru](mailto:eduardgushchin@yandex.ru) or join the [Telegram chat](https://t.me/sdl3cs) for questions and feedback.

## 💻 Contributors

<a href="https://github.com/edwardgushchin/SDL3-CS/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=edwardgushchin/SDL3-CS" alt="SDL3-CS contributors">
</a>

See the full list of [contributors](https://github.com/edwardgushchin/SDL3-CS/graphs/contributors) who participated in this project.

## 📃 License

SDL3 and SDL3# are released under the zlib license. See [LICENSE](LICENSE) for details.
