<p align="center">
  <img src="./logo.png?raw=true" alt="SDL3#">
</p>

<h4 align="center">This is SDL3#, a C# wrapper for SDL3.</h4>

<p align="center">
    <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/edwardgushchin/SDL3-CS">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/edwardgushchin/SDL3-CS">
    <img alt="Static Badge" src="https://img.shields.io/badge/license-zlib-blue">
</p>

<p align="center">
    <img alt="Static Badge" src="https://img.shields.io/badge/.NET-7.0,_8.0,_9.0,_10.0-512BD4">
    <img alt="Static Badge" src="https://img.shields.io/badge/Language-C%23_14-239120">
    <img alt="Static Badge" src="https://img.shields.io/badge/SDL3-3.4.2-239120">
</p>

<p align="center">
  <a href="#-about">About</a> •
  <a href="#-documentation">Documentation</a> •
  <a href="#-installation">Installation</a> •
  <a href="#-examples">Examples</a> •
  <a href="#-readiness">Readiness</a>
</p>
<p align="center">
  <a href="#-feedback-and-contributions">Feedback and Contributions</a> •
  <a href="#-authors">Authors</a> •
  <a href="#-license">License</a>
</p>

<p align="center">⭐ Star us on GitHub — it motivates us a lot!</p>

## 🚀 About

This is SDL3#, a C# wrapper for SDL3.

## 🔢 Version Compatibility

| SDL3-CS Version | SDL3 Version | Minimum glibc (Linux) | Notes |
|-----------------|--------------|----------------------|-------|
| 3.4.2.x         | 3.4.2        | 2.28 (Ubuntu 18.04+) | Current stable |
| 3.4.0.x         | 3.4.0        | 2.28 (Ubuntu 18.04+) | Previous stable |

> **Note:** The version numbering scheme follows SDL3's versioning. For example, SDL3-CS version `3.4.2.x` is designed for SDL3 version `3.4.2`.

### Building Native Libraries on Linux

If you're building native libraries in a custom environment (e.g., Docker, Steam Runtime SDK), ensure you're building against SDL3 version **3.4.2**. The native packages (`SDL3-CS.Native*`) contain pre-built binaries for common platforms, but custom builds should match this version.

For more detailed version information, see [VERSIONS.md](VERSIONS.md).

## 📚 Documentation

For more information about SDL3, visit the [SDL wiki](https://wiki.libsdl.org/SDL3/FrontPage).

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

Optional:

```
dotnet add package SDL3-CS.Native
dotnet add package SDL3-CS.Native.Image
dotnet add package SDL3-CS.Native.TTF
dotnet add package SDL3-CS.Native.Mixer
dotnet add package SDL3-CS.Native.Shadercross
```

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
| [SDL_tff](SDL3-CS/TTF)                    | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [SDL_shadercross](SDL3-CS/TTFShaderCross) | ![Ready](https://img.shields.io/badge/Ready-008000)   |


## 🤝 Feedback and Contributions

Do you have an idea or found a bug? Please open an [issue](https://github.com/edwardgushchin/SDL3-CS/issues) or start a [discussion](https://github.com/edwardgushchin/SDL3-CS/discussions).

Please note we have a code of conduct, please follow it in all your interactions with the project.

If you have any feedback, please reach out to us at [eduardgushchin@yandex.ru](mailto://eduardgushchin@yandex.ru).

We also have a [chat](https://t.me/sdl3cs) in Telegram, where I am ready to answer any of your questions.

## 💻 Contributors

<a href = "https://github.com/edwardgushchin/SDL3-CS/graphs/contributors">
  <img src = "https://contrib.rocks/image?repo=edwardgushchin/SDL3-CS"/>
</a>

See also the list of [contributors](https://github.com/edwardgushchin/SDL3-CS/graphs/contributors) who participated in this project.

## 📃 License

SDL3 and SDL3# are released under the zlib license. See [LICENSE](LICENSE) for details.
