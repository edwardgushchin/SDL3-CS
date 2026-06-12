## 🚀 About

This is SDL3#, a C# wrapper for SDL3.

## 🔢 Version Compatibility

| SDL3-CS Version | SDL3 Version | Minimum glibc (Linux) | Notes |
|-----------------|--------------|----------------------|-------|
| 3.4.10.x        | 3.4.10       | 2.28 (Ubuntu 18.04+) | Current stable |
| 3.4.2.x         | 3.4.2        | 2.28 (Ubuntu 18.04+) | Previous stable |

> **Note:** The version numbering scheme follows SDL3's versioning. For example, SDL3-CS version `3.4.10.x` is designed for SDL3 version `3.4.10`.

### Building Native Libraries on Linux

If you're building native libraries in a custom environment (e.g., Docker, Steam Runtime SDK), ensure you're building against SDL3 version **3.4.10**. The platform packages (`SDL3-CS.<Platform>*`) contain pre-built binaries for common platforms, but custom builds should match this version.

For more detailed version information, see [VERSIONS.md](https://github.com/edwardgushchin/SDL3-CS/blob/master/VERSIONS.md).

## 📚 Documentation

For more information about SDL3, visit the [SDL wiki](https://wiki.libsdl.org/SDL3/FrontPage).

## 🧭 Supported Platforms

The managed `SDL3-CS` wrapper targets .NET 7, .NET 8, .NET 9, and .NET 10. Official native NuGet assets are published as platform-specific package families for the following release targets:

| Platform family | Native package suffix | Supported RIDs / ABIs | Notes |
|-----------------|-----------------------|------------------------|-------|
| Windows | `Windows` | `win-x86`, `win-x64`, `win-arm64` | Dynamic SDL libraries for desktop Windows apps. |
| Linux | `Linux` | `linux-x64`, `linux-arm64` | Built against glibc 2.28 or newer. |
| macOS | `MacOS` | `osx-x64`, `osx-arm64` | Dynamic SDL libraries for Intel and Apple Silicon macOS apps. |
| Android | `Android` | `android-arm` (`armeabi-v7a`), `android-arm64` (`arm64-v8a`), `android-x86` (`x86`), `android-x64` (`x86_64`) | Use `SDL3-CS.Android` and `MainActivity : Org.Libsdl.App.SDLActivity` with a managed `Main()` override. |
| iOS | `iOS` | `ios-arm64`, `iossimulator-arm64`, `iossimulator-x64` | Static native assets are linked through package `buildTransitive` targets. |
| tvOS | `tvOS` | `tvos-arm64`, `tvossimulator-arm64`, `tvossimulator-x64` | Static native assets are linked through package `buildTransitive` targets. |

Other platforms can still use the managed wrapper if the application supplies compatible SDL native libraries manually. They are not part of the official native NuGet asset set until they are added to the release manifest and CI validation.

The native package projects live under `SDL3-CS.NativePackages/`. It contains only the platform package projects (`SDL3-CS.<Platform>` and `SDL3-CS.<Platform>.<Addon>`) so the repository root stays focused on the managed wrapper, examples, and tests.

## 📝 Installation

```
git clone https://github.com/edwardgushchin/SDL3-CS
cd SDL3-CS
dotnet build -c Release
```

or

```
dotnet add package SDL3-CS
```

Add the platform package family that matches your target platform. For example, Windows desktop apps use:

```
dotnet add package SDL3-CS.Windows
dotnet add package SDL3-CS.Windows.Image
dotnet add package SDL3-CS.Windows.TTF
dotnet add package SDL3-CS.Windows.Mixer
dotnet add package SDL3-CS.Windows.Shadercross
```

Replace `Windows` with `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS` for the corresponding platform family.

The full platform package families are:

| Platform | SDL | SDL_image | SDL_ttf | SDL_mixer | SDL_shadercross |
|----------|-----|-----------|---------|-----------|-----------------|
| Windows | `SDL3-CS.Windows` | `SDL3-CS.Windows.Image` | `SDL3-CS.Windows.TTF` | `SDL3-CS.Windows.Mixer` | `SDL3-CS.Windows.Shadercross` |
| Linux | `SDL3-CS.Linux` | `SDL3-CS.Linux.Image` | `SDL3-CS.Linux.TTF` | `SDL3-CS.Linux.Mixer` | `SDL3-CS.Linux.Shadercross` |
| macOS | `SDL3-CS.MacOS` | `SDL3-CS.MacOS.Image` | `SDL3-CS.MacOS.TTF` | `SDL3-CS.MacOS.Mixer` | `SDL3-CS.MacOS.Shadercross` |
| Android | `SDL3-CS.Android` | `SDL3-CS.Android.Image` | `SDL3-CS.Android.TTF` | `SDL3-CS.Android.Mixer` | `SDL3-CS.Android.Shadercross` |
| iOS | `SDL3-CS.iOS` | `SDL3-CS.iOS.Image` | `SDL3-CS.iOS.TTF` | `SDL3-CS.iOS.Mixer` | `SDL3-CS.iOS.Shadercross` |
| tvOS | `SDL3-CS.tvOS` | `SDL3-CS.tvOS.Image` | `SDL3-CS.tvOS.TTF` | `SDL3-CS.tvOS.Mixer` | `SDL3-CS.tvOS.Shadercross` |

Android apps use a single base Android package that includes both `libSDL3.so` and the SDL Android bridge bindings:

```
dotnet add package SDL3-CS.Android
dotnet add package SDL3-CS.Android.Image
dotnet add package SDL3-CS.Android.TTF
dotnet add package SDL3-CS.Android.Mixer
dotnet add package SDL3-CS.Android.Shadercross
```

### Current NuGet Release

| Package | Version | Purpose |
|---------|---------|---------|
| `SDL3-CS` | `3.4.10.1` | Managed wrapper for SDL 3.4.10 |

Platform package IDs use one of the supported platform suffixes: `Windows`, `Linux`, `MacOS`, `Android`, `iOS`, or `tvOS`.

| Native component | Package ID pattern | Version |
|------------------|--------------------|---------|
| SDL | `SDL3-CS.<Platform>` | `3.4.10.1` |
| SDL_image | `SDL3-CS.<Platform>.Image` | `3.4.4.1` |
| SDL_mixer | `SDL3-CS.<Platform>.Mixer` | `3.2.4.1` |
| SDL_ttf | `SDL3-CS.<Platform>.TTF` | `3.2.2.1` |
| SDL_shadercross | `SDL3-CS.<Platform>.Shadercross` | `3.0.0.1` |

Android applications should use `MainActivity : Org.Libsdl.App.SDLActivity`, override `GetLibraries()` and run SDL from the managed `Main()` override. The `SDL3-CS.Android` package supplies both the SDL Java bridge bindings and the ABI-specific `libSDL3.so` files.

## 🎓 Examples

```C#
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

        SDL.SetRenderDrawColor(renderer, 100, 149, 237, 0);

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

More examples can be found [here](https://github.com/edwardgushchin/SDL3-CS/tree/master/SDL3-CS.Examples).

## ✅ Readiness

| **Library**                               | **Stage**                                             |
|-------------------------------------------|-------------------------------------------------------|
| [SDL3](SDL3-CS/SDL)                       | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [SDL_image](SDL3-CS/Image)                | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [SDL_mixer](SDL3-CS/Mixer)                | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [SDL_ttf](SDL3-CS/TTF)                    | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [SDL_shadercross](SDL3-CS/ShaderCross)    | ![Ready](https://img.shields.io/badge/Ready-008000)   |


## 🤝 Feedback and Contributions

Do you have an idea or found a bug? Please open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues) or start a [discussion](https://github.com/edwardgushchin/SDL3-CS/discussions).

Please note we have a code of conduct, please follow it in all your interactions with the project.

If you have any feedback, please reach out to us at [eduardgushchin@yandex.ru](mailto://eduardgushchin@yandex.ru).

We also have a [chat](https://t.me/sdl3cs) in Telegram, where I am ready to answer any of your questions.

## 💻 Authors

- Eduard Gushchin - Initial work - [edwardgushchin](https://github.com/edwardgushchin)

See also the list of [contributors](https://github.com/edwardgushchin/SDL3-CS/graphs/contributors) who participated in this project.

## 📃 License

SDL3 and SDL3# are released under the zlib license. See [LICENSE](LICENSE) for details.
