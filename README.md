<p align="center">
  <img src="logo.png?raw=true" alt="SDL3#">
</p>

<h4 align="center">This is SDL3#, a C# wrapper for SDL3.</h4>

<p align="center">
    <img alt="GitHub contributors" src="https://img.shields.io/github/contributors/edwardgushchin/SDL3-CS">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/edwardgushchin/SDL3-CS">
    <img alt="Static Badge" src="https://img.shields.io/badge/license-zlib-blue">
</p>

<p align="center">
    <img alt="Static Badge" src="https://img.shields.io/badge/.NET-7.0,_8.0,_9.0-512BD4">
    <img alt="Static Badge" src="https://img.shields.io/badge/Language-C%23_12-239120">
    <img alt="Static Badge" src="https://img.shields.io/badge/OS-Windows%2C%20Linux%2C%20macOS-blue">
    <img alt="Static Badge" src="https://img.shields.io/badge/CPU-x86%2C%20x64%2C%20ARM%2C%20ARM64-FF8C00">
</p>

<p align="center">
  <a href="#-about">About</a> •
  <a href="#-documentation">Documentation</a> •
  <a href="#-installation">Installation</a> •
  <a href="#-examples">Examples</a> •
  <a href="#-readiness">Readiness</a>
</p>
<p align="center">
  <a href="#-mobile-platform-support">Mobile platform support</a> •
  <a href="#-feedback-and-contributions">Feedback and Contributions</a> •
  <a href="#-authors">Authors</a> •
  <a href="#-license">License</a>
</p>

<p align="center">⭐ Star us on GitHub — it motivates us a lot!</p>


## 🚀 About

SDL3 is still under active development, and the shell follows suit.

For more information on what is currently ready for use, see the <a href="#-readiness">Readiness</a> section.

## 📚 Documentation

For more information about SDL3, visit the [SDL wiki](https://wiki.libsdl.org/SDL3/FrontPage).


## 📝 Installation

```
git clone https://github.com/edwardgushchin/SDL3-CS
cd SDL3-CS
dotnet build -c Release
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
                if (e.Type == SDL.EventType.Quit)
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

### Basics

| **View information and functions related to...** | **View the header**                                                                           | **Stage**                                            |
|-------------------------------------------------|-----------------------------------------------------------------------------------------------|------------------------------------------------------|
| [Application entry points](SDL3-CS/SDL/Basics/main) | [SDL_main.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_main.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Initialization and Shutdown](SDL3-CS/SDL/Basics/init) | [SDL_init.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_init.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Configuration Variables](SDL3-CS/SDL/Basics/hints) | [SDL_hints.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_hints.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Object Properties](SDL3-CS/SDL/Basics/properties)  | [SDL_properties.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_properties.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Error Handling](SDL3-CS/SDL/Basics/error)          | [SDL_error.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_error.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Log Handling](SDL3-CS/SDL/Basics/log)              | [SDL_log.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_log.h)               | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Assertions](SDL3-CS/SDL/Basics/assert)             | [SDL_assert.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_assert.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Querying SDL Version](SDL3-CS/SDL/Basics/version)  | [SDL_version.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_version.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### Video

| **View information and functions related to...**              | **View the header**                                                                         | **Stage**                                            |
|---------------------------------------------------------------|---------------------------------------------------------------------------------------------|------------------------------------------------------|
| [Display and Window Management](SDL3-CS/SDL/Video/video)          | [SDL_video.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_video.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [2D Accelerated Rendering](SDL3-CS/SDL/Video/render)              | [SDL_render.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_render.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Pixel Formats and Conversion Routines](SDL3-CS/SDL/Video/pixels) | [SDL_pixels.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_pixels.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Blend modes](SDL3-CS/SDL/Video/blendmode)                        | [SDL_blendmode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_blendmode.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Rectangle Functions](SDL3-CS/SDL/Video/rect)                     | [SDL_rect.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_rect.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Surface Creation and Simple Drawing](SDL3-CS/SDL/Video/surface)  | [SDL_surface.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_surface.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Clipboard Handling](SDL3-CS/SDL/Video/clipboard)                 | [SDL_clipboard.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_clipboard.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Vulkan Support](SDL3-CS/SDL/Video/vulkan)                        | [SDL_vulkan.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_vulkan.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Metal Support](SDL3-CS/SDL/Video/metal)                          | [SDL_metal.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL_metal.h)              | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Camera Support](SDL3-CS/SDL/Video/camera)                        | [SDL_camera.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL_camera.h)            | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### Input Events

| **View information and functions related to...**      | **View the header**                                                                       | **Stage**                                             |
|-------------------------------------------------------|-------------------------------------------------------------------------------------------|-------------------------------------------------------|
| [Event Handling](SDL3-CS/SDL/Input%20Events/events)       | [SDL_events.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_events.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Keyboard Support](SDL3-CS/SDL/Input%20Events/keyboard)   | [SDL_keyboard.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_keyboard.h) | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Keyboard Keycodes](SDL3-CS/SDL/Input%20Events/keycode)   | [SDL_keycode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_keycode.h)   | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Keyboard Scancodes](SDL3-CS/SDL/Input%20Events/scancode) | [SDL_scancode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_scancode.h) | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Mouse Support](SDL3-CS/SDL/Input%20Events/mouse)         | [SDL_mouse.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_mouse.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Joystick Support](SDL3-CS/SDL/Input%20Events/joystick)   | [SDL_joystick.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_joystick.h) | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Gamepad Support](SDL3-CS/SDL/Input%20Events/gamepad)     | [SDL_gamepad.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_gamepad.h)   | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Touch Support](SDL3-CS/SDL/Input%20Events/touch)         | [SDL_touch.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_touch.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Pen Support](SDL3-CS/SDL/Input%20Events/pen)             | [SDL_pen.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_pen.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [Sensors](SDL3-CS/SDL/Input%20Events/sensor)              | [SDL_sensor.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_sensor.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)   |
| [HIDAPI](SDL3-CS/SDL/Input%20Events/hidapi)               | [SDL_hidapi.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_hidapi.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)   |


### Force Feedback ("Haptic")

| **View information and functions related to...**            | **View the header**                                                                      | **Stage**                                            |
|-------------------------------------------------------------|------------------------------------------------------------------------------------------|------------------------------------------------------|
| [Force Feedback Support](SDL3-CS/SDL/Force%20Feedback/haptic)   | [SDL_haptic.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_haptic.h)    | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### Audio

| **View information and functions related to...**               | **View the header**                                                                     | **Stage**                                            |
|----------------------------------------------------------------|-----------------------------------------------------------------------------------------|------------------------------------------------------|
| [Audio Playback, Recording, and Mixing](SDL3-CS/SDL/Audio/audio)   | [SDL_audio.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_audio.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)  |

### GPU

| **View information and functions related to...**  | **View the header**                                                               | **Stage**                                            |
|---------------------------------------------------|-----------------------------------------------------------------------------------|------------------------------------------------------|
| [3D Rendering and GPU Compute](SDL3-CS/SDL/GPU/gpu)   | [SDL_gpu.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_gpu.h)   | ![Ready](https://img.shields.io/badge/Ready-008000)  |

### Threads

| **View information and functions related to...**           | **View the header**                                                                    | **Stage**                                            |
|------------------------------------------------------------|----------------------------------------------------------------------------------------|------------------------------------------------------|
| [Thread Management](SDL3-CS/SDL/Threads/thread)                | [SDL_thread.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_thread.h)  | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Thread Synchronization Primitives](SDL3-CS/SDL/Threads/mutex) | [SDL_mutex.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_mutex.h)    | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Atomic Operations](SDL3-CS/SDL/Threads/atomic)                | [SDL_atomic.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_atomic.h)  | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### Time

| **View information and functions related to...**          | **View the header**                                                                     | **Stage**                                            |
|-----------------------------------------------------------|-----------------------------------------------------------------------------------------|------------------------------------------------------|
| [Timer Support](SDL3-CS/SDL/Time/timer)                       | [SDL_timer.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_timer.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Date and Time](SDL3-CS/SDL/Time/time)                        | [SDL_time.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_time.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### File and I/O Abstractions

| **View information and functions related to...**                        | **View the header**                                                                            | **Stage**                                            |
|-------------------------------------------------------------------------|------------------------------------------------------------------------------------------------|------------------------------------------------------|
| [Filesystem Access](SDL3-CS/SDL/File%20and%20IO%20Abstractions/filesystem)  | [SDL_filesystem.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_filesystem.h)  | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Storage Abstraction](SDL3-CS/SDL/File%20and%20IO%20Abstractions/storage)   | [SDL_storage.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_storage.h)        | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [I/O Streams](SDL3-CS/SDL/File%20and%20IO%20Abstractions/iostream)          | [SDL_iostream.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_iostream.h)      | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Async I/O](SDL3-CS/SDL/File%20and%20IO%20Abstractions/asyncio)             | [SDL_asyncio.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_asyncio.h)        | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### Platform and CPU Information

| **View information and functions related to...**                                  | **View the header**                                                                       | **Stage**                                                |
|-----------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------|----------------------------------------------------------|
| [Platform Detection](SDL3-CS/SDL/Platform%20and%20CPU%20Information/platform)     | [SDL_platform.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_platform.h) | ![Ready](https://img.shields.io/badge/Ready-008000)      |
| [CPU Feature Detection](SDL3-CS/SDL/Platform%20and%20CPU%20Information/cpuinfo)   | [SDL_cpuinfo.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_cpuinfo.h)   | ![Ready](https://img.shields.io/badge/Ready-008000)      |
| Compiler Intrinsics Detection                                                     | [SDL_intrin.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_intrin.h)     | ![Skipped](https://img.shields.io/badge/Skipped-FFA500)  |
| Byte Order and Byte Swapping                                                      | [SDL_endian.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_endian.h)     | ![Skipped](https://img.shields.io/badge/Skipped-FFA500)  |
| Bit Manipulation                                                                  | [SDL_bits.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_bits.h)         | ![Skipped](https://img.shields.io/badge/Skipped-FFA500)  |


### Additional Functionality

| **View information and functions related to...**                                  | **View the header**                                                                           | **Stage**                                            |
|-----------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------|------------------------------------------------------|
| [Shared Object/DLL Management](SDL3-CS/SDL/Additional%20Functionality/loadso)     | [SDL_loadso.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_loadso.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Process Control](SDL3-CS/SDL/Additional%20Functionality/process)                 | [SDL_process.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_process.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Power Management Status](SDL3-CS/SDL/Additional%20Functionality/power)           | [SDL_power.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_power.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Message Boxes](SDL3-CS/SDL/Additional%20Functionality/messagebox)                | [SDL_messagebox.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_messagebox.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [File Dialogs](SDL3-CS/SDL/Additional%20Functionality/dialog)                     | [SDL_dialog.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_dialog.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Locale Info](SDL3-CS/SDL/Additional%20Functionality/locale)                      | [SDL_locale.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_locale.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Platform-specific Functionality](SDL3-CS/SDL/Additional%20Functionality/system)  | [SDL_system.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_system.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Standard Library Functionality](SDL3-CS/SDL/Additional%20Functionality/stdinc)   | [SDL_stdinc.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_stdinc.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [GUIDs](SDL3-CS/SDL/Additional%20Functionality/guid)                              | [SDL_guid.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_guid.h)             | [Ready](https://img.shields.io/badge/Ready-008000)   |
| [Miscellaneous](SDL3-CS/SDL/Additional%20Functionality/misc)                      | [SDL_misc.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_misc.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)  |


## 📱 Mobile platform support

In theory, there is no reason why this shell cannot run on Android and iOS, but I have never worked with these platforms and cannot guarantee 100% work. If you can add support for mobile platforms, I look forward to your [Pull requests](https://github.com/edwardgushchin/SDL3-CS/pulls)!


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
