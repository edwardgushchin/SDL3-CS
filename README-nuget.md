# SDL3-CS

Modern C# bindings for SDL3 and its companion libraries.

SDL3-CS gives .NET applications direct access to SDL3 APIs and pairs the managed wrapper with platform-specific native runtime packages for Windows, Linux, macOS, Android, iOS, and tvOS.

## 🚀 About

The project provides:

- managed C# bindings for SDL3, SDL_image, SDL_ttf, SDL_mixer, and SDL_shadercross;
- platform-specific native NuGet packages named `SDL3-CS.{Platform}` and `SDL3-CS.{Platform}.{Addon}`;
- Android SDLActivity bridge bindings for managed Android applications;
- examples and release checks that validate package layout and wrapper metadata.

Use `SDL3-CS` when you want low-level SDL3 access from modern .NET while keeping native runtime deployment predictable across supported platforms.

## 🔢 Versioning and Native Compatibility

SDL3-CS package versions follow the native SDL component versions. The first three version segments identify the upstream native component version. The final segment is the SDL3-CS package revision.

| Component | Package pattern | Native target | Package line |
|-----------|-----------------|---------------|--------------|
| SDL3 managed bindings | `SDL3-CS` | SDL `3.4.10` | `3.4.10.x` |
| SDL3 native runtime | `SDL3-CS.{Platform}` | SDL `3.4.10` | `3.4.10.x` |
| SDL_image native runtime | `SDL3-CS.{Platform}.Image` | SDL_image `3.4.4` | `3.4.4.x` |
| SDL_ttf native runtime | `SDL3-CS.{Platform}.TTF` | SDL_ttf `3.2.2` | `3.2.2.x` |
| SDL_mixer native runtime | `SDL3-CS.{Platform}.Mixer` | SDL_mixer `3.2.4` | `3.2.4.x` |
| SDL_shadercross native runtime | `SDL3-CS.{Platform}.Shadercross` | SDL_shadercross `3.0.0` | `3.0.0.x` |

`{Platform}` is one of `Windows`, `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS`.

Published NuGet packages can lag behind a release branch while native packages are being assembled. The NuGet package page is authoritative for what is currently published.

## 📚 Documentation

Project documentation lives in the [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki). Use the wiki, examples, and [GitHub Releases](https://github.com/edwardgushchin/SDL3-CS/releases) for version notes, migration guidance, platform details, and release documentation.

For upstream SDL API documentation, see the official [SDL3 Wiki](https://wiki.libsdl.org/SDL3/FrontPage).

## 🧭 Supported Platforms

The managed `SDL3-CS` wrapper targets .NET 7, .NET 8, .NET 9, and .NET 10.

Official native package families are built for the following release targets:

| Platform family | Package suffix | Supported RIDs / ABIs |
|-----------------|----------------|------------------------|
| Windows | `Windows` | `win-x86`, `win-x64`, `win-arm64` |
| Linux | `Linux` | `linux-x64`, `linux-arm64` |
| macOS | `MacOS` | `osx-x64`, `osx-arm64` |
| Android | `Android` | `android-arm`, `android-arm64`, `android-x86`, `android-x64` |
| iOS | `iOS` | `ios-arm64`, `iossimulator-arm64`, `iossimulator-x64` |
| tvOS | `tvOS` | `tvos-arm64`, `tvossimulator-arm64`, `tvossimulator-x64` |

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

Replace `Windows` with `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS`.

Optional SDL companion libraries use the same platform suffix:

```bash
dotnet add package SDL3-CS.Windows.Image
dotnet add package SDL3-CS.Windows.TTF
dotnet add package SDL3-CS.Windows.Mixer
dotnet add package SDL3-CS.Windows.Shadercross
```

Android applications should reference `SDL3-CS.Android` and use `MainActivity : Org.Libsdl.App.SDLActivity` with a managed `Main()` override. The Android package includes the SDL Java bridge bindings and ABI-specific `libSDL3.so` files.

## 🎓 Example

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

Please follow the [Code of Conduct](https://github.com/edwardgushchin/SDL3-CS/blob/main/CODE_OF_CONDUCT.md) in all project interactions.

You can contact the maintainer at [eduardgushchin@yandex.ru](mailto:eduardgushchin@yandex.ru) or join the [Telegram chat](https://t.me/sdl3cs) for questions and feedback.

## 📃 License

SDL3 and SDL3# are released under the zlib license. See [LICENSE](https://github.com/edwardgushchin/SDL3-CS/blob/main/LICENSE) for details.
