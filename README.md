# SDL3-CS
This is SDL3#, a C# wrapper for SDL3.

Project Website: https://github.com/edwardgushchin/SDL3-CS

## License

SDL3 and SDL3# are released under the zlib license. See LICENSE for details.

## About SDL3

For more information about SDL3, visit the SDL wiki:

https://wiki.libsdl.org/SDL3/FrontPage


## Installation

```
git clone https://github.com/edwardgushchin/SDL3-CS
cd SDL3-CS
dotnet build -c Release
```

## Usage/Examples

```C#
using SDL3;

namespace SDL3Test;

public static class Program
{
    private static void Main()
    {
        if (SDL.Init(SDL.InitFlags.Video) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Create Window", 800, 600, 0);
        
        if (window == null)
        {
            Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var renderer = SDL.CreateRenderer(window, null);
        
        if (renderer == null)
        {
            Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            return;
        }

        SDL.SetRenderDrawColor(renderer, 100, 149, 237, 0);
        
        var loop = true;
        
        while (loop)
        {
            
            while (SDL.PollEvent(out var sdlEvent))
            {
                if (sdlEvent.Type == SDL.EventType.Quit)
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

## Readiness

### Basics

| **View information and functions related to...**       | **View the header**                                                                           | **Stage**                                               |
|--------------------------------------------------------| --------------------------------------------------------------------------------------------- | ------------------------------------------------------- |
| Application entry points                               | [SDL_main.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_main.h)             | ![Skipped](https://img.shields.io/badge/Skipped-FFA500) |
| [Initialization and Shutdown](SDL3-CS/SDL/Basics/init) | [SDL_init.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_init.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Configuration Variables](SDL3-CS/SDL/Basics/hints)    | [SDL_hints.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_hints.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Object Properties](SDL3-CS/SDL/Basics/properties)     | [SDL_properties.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_properties.h) | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Error Handling](SDL3-CS/SDL/Basics/error)             | [SDL_error.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_error.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Log Handling](SDL3-CS/SDL/Basics/log)                 | [SDL_log.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_log.h)               | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| Assertions                                             | [SDL_assert.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_assert.h)         | ![Skipped](https://img.shields.io/badge/Skipped-FFA500) |
| [Querying SDL Version](SDL3-CS/SDL/Basics/version)     | [SDL_version.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_version.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)     |


### Video

| **View information and functions related to...**                  | **View the header**                                                                         | **Stage**                                                         |
|-------------------------------------------------------------------| ------------------------------------------------------------------------------------------- |-------------------------------------------------------------------|
| [Display and Window Management](SDL3-CS/SDL/Video/video)          | [SDL_video.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_video.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [2D Accelerated Rendering](SDL3-CS/SDL/Video/render)              | [SDL_render.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_render.h)       | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Pixel Formats and Conversion Routines](SDL3-CS/SDL/Video/pixels) | [SDL_pixels.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_pixels.h)       | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Blend modes](SDL3-CS/SDL/Video/blendmode)                        | [SDL_blendmode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_blendmode.h) | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Rectangle Functions](SDL3-CS/SDL/Video/rect)                     | [SDL_rect.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_rect.h)           | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Surface Creation and Simple Drawing](SDL3-CS/SDL/Video/surface)  | [SDL_surface.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_surface.h)     | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Clipboard Handling](SDL3-CS/SDL/Video/clipboard)                 | [SDL_clipboard.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_clipboard.h) | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Vulkan Support](SDL3-CS/SDL/Video/vulkan)                        | [SDL_vulkan.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_vulkan.h)       | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Metal Support](SDL3-CS/SDL/Video/metal)                          | [SDL_metal.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL_metal.h)              | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Camera Support](SDL3-CS/SDL/Video/camera)                        | [SDL_camera.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL_camera.h)            | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Input Events

| **View information and functions related to...**          | **View the header**                                                                       | **Stage**                                            |
|-----------------------------------------------------------| ----------------------------------------------------------------------------------------- |------------------------------------------------------|
| [Event Handling](SDL3-CS/SDL/Input%20Events/events)       | [SDL_events.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_events.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Keyboard Support](SDL3-CS/SDL/Input%20Events/keyboard)   | [SDL_keyboard.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_keyboard.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Keyboard Keycodes](SDL3-CS/SDL/Input%20Events/keycode)   | [SDL_keycode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_keycode.h)   | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Keyboard Scancodes](SDL3-CS/SDL/Input%20Events/scancode) | [SDL_scancode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_scancode.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Mouse Support](SDL3-CS/SDL/Input%20Events/mouse)         | [SDL_mouse.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_mouse.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Joystick Support](SDL3-CS/SDL/Input%20Events/joystick)   | [SDL_joystick.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_joystick.h) | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Gamepad Support](SDL3-CS/SDL/Input%20Events/gamepad)     | [SDL_gamepad.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_gamepad.h)   | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Touch Support](SDL3-CS/SDL/Input%20Events/touch)         | [SDL_touch.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_touch.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Pen Support](SDL3-CS/SDL/Input%20Events/pen)             | [SDL_pen.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_pen.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [Sensors](SDL3-CS/SDL/Input%20Events/sensor)              | [SDL_sensor.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_sensor.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)  |
| [HIDAPI](SDL3-CS/SDL/Input%20Events/hidapi)               | [SDL_hidapi.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_hidapi.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)  |


### Force Feedback ("Haptic")

| **View information and functions related to...**              | **View the header**                                                                     | **Stage**                                                         |
|---------------------------------------------------------------| --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Force Feedback Support](SDL3-CS/SDL/Force%20Feedback/haptic) | [SDL_haptic.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_haptic.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Audio

| **View information and functions related to...**                 | **View the header**                                                                     | **Stage**                                                         |
|------------------------------------------------------------------| --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Audio Playback, Recording, and Mixing](SDL3-CS/SDL/Audio/audio) | [SDL_audio.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_audio.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Threads

| **View information and functions related to...**               | **View the header**                                                                     | **Stage**                                                         |
|----------------------------------------------------------------| --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Thread Management](SDL3-CS/SDL/Threads/thread)                | [SDL_thread.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_thread.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Thread Synchronization Primitives](SDL3-CS/SDL/Threads/mutex) | [SDL_mutex.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_mutex.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Atomic Operations](SDL3-CS/SDL/Threads/atomic)                | [SDL_atomic.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_atomic.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Time

| **View information and functions related to...** | **View the header**                                                                     | **Stage**                                                         |
|--------------------------------------------------| --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Timer Support](SDL3-CS/SDL/Time/timer)          | [SDL_timer.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_timer.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Date and Time](SDL3-CS/SDL/Time/time)           | [SDL_time.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_time.h)       | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### File and I/O Abstractions

| **View information and functions related to...**                           | **View the header**                                                                           | **Stage**                                                         |
|----------------------------------------------------------------------------| --------------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Filesystem Access](SDL3-CS/SDL/File%20and%20IO%20Abstractions/filesystem) | [SDL_filesystem.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_filesystem.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Storage Abstraction](SDL3-CS/SDL/File%20and%20IO%20Abstractions/storage)  | [SDL_storage.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_storage.h)       | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [I/O Streams](SDL3-CS/SDL/File%20and%20IO%20Abstractions/iostream)         | [SDL_iostream.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_iostream.h)     | ![In progress](https://img.shields.io/badge/In%20progress-828282) |


### Platform and CPU Information

| **View information and functions related to...**                                      | **View the header**                                                                       | **Stage**                                                         |
|---------------------------------------------------------------------------------------| ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Platform Detection](SDL3-CS/SDL/Platform%20and%20CPU%20Information/platform)         | [SDL_platform.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_platform.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [CPU Feature Detection](SDL3-CS/SDL/Platform%20and%20CPU%20Information/cpuinfo)       | [SDL_cpuinfo.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_cpuinfo.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Byte Order and Byte Swapping](SDL3-CS/SDL/Platform%20and%20CPU%20Information/endian) | [SDL_endian.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_endian.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Bit Manipulation](SDL3-CS/SDL/Platform%20and%20CPU%20Information/bits)               | [SDL_bits.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_bits.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Additional Functionality

| **View information and functions related to...**                                 | **View the header**                                                                           | **Stage**                                                         |
|----------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------| ----------------------------------------------------------------- |
| [Shared Object/DLL Management](SDL3-CS/SDL/Additional%20Functionality/loadso)    | [SDL_loadso.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_loadso.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Power Management Status](SDL3-CS/SDL/Additional%20Functionality/power)          | [SDL_power.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_power.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Message Boxes](SDL3-CS/SDL/Additional%20Functionality/messagebox)               | [SDL_messagebox.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_messagebox.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [File Dialogs](SDL3-CS/SDL/Additional%20Functionality/dialog)                    | [SDL_dialog.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_dialog.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Locale Info](SDL3-CS/SDL/Additional%20Functionality/locale)                     | [SDL_locale.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_locale.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Platform-specific Functionality](SDL3-CS/SDL/Additional%20Functionality/system) | [SDL_system.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_system.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Standard Library Functionality](SDL3-CS/SDL/Additional%20Functionality/stdinc)  | [SDL_stdinc.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_stdinc.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [GUIDs](SDL3-CS/SDL/Additional%20Functionality/guid)                             | [SDL_guid.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_guid.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Miscellaneous](SDL3-CS/SDL/Additional%20Functionality/misc)                     | [SDL_misc.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_misc.h)             | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |

## Feedback

If you have any feedback, please reach out to us at eduardgushchin@yandex.ru
