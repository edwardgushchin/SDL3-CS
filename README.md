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

| **View information and functions related to...** | **View the header**                                                                           | **Stage**                                               |
| ------------------------------------------------ | --------------------------------------------------------------------------------------------- | ------------------------------------------------------- |
| [Application entry points](CategoryMain)         | [SDL_main.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_main.h)             | ![Skipped](https://img.shields.io/badge/Skipped-FFA500) |
| [Initialization and Shutdown](CategoryInit)      | [SDL_init.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_init.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Configuration Variables](CategoryHints)         | [SDL_hints.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_hints.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Object Properties](CategoryProperties)          | [SDL_properties.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_properties.h) | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Error Handling](CategoryError)                  | [SDL_error.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_error.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Log Handling](CategoryLog)                      | [SDL_log.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_log.h)               | ![Ready](https://img.shields.io/badge/Ready-008000)     |
| [Assertions](CategoryAssert)                     | [SDL_assert.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_assert.h)         | ![Skipped](https://img.shields.io/badge/Skipped-FFA500) |
| [Querying SDL Version](CategoryVersion)          | [SDL_version.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_version.h)       | ![Ready](https://img.shields.io/badge/Ready-008000)     |


### Video

| **View information and functions related to...**        | **View the header**                                                                         | **Stage**                                                         |
| ------------------------------------------------------- | ------------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Display and Window Management](CategoryVideo)          | [SDL_video.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_video.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [2D Accelerated Rendering](CategoryRender)              | [SDL_render.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_render.h)       | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Pixel Formats and Conversion Routines](CategoryPixels) | [SDL_pixels.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_pixels.h)       | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Blend modes](CategoryBlendmode)                        | [SDL_blendmode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_blendmode.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Rectangle Functions](CategoryRect)                     | [SDL_rect.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_rect.h)           | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Surface Creation and Simple Drawing](CategorySurface)  | [SDL_surface.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_surface.h)     | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Clipboard Handling](CategoryClipboard)                 | [SDL_clipboard.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_clipboard.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Vulkan Support](CategoryVulkan)                        | [SDL_vulkan.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_vulkan.h)       | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Metal Support](CategoryMetal)                          | [SDL_metal.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL_metal.h)              | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Camera Support](CategoryCamera)                        | [SDL_camera.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL_camera.h)            | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Input Events

| **View information and functions related to...** | **View the header**                                                                       |**Stage**                                                          |
| ------------------------------------------------ | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Event Handling](CategoryEvents)                 | [SDL_events.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_events.h)     | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Keyboard Support](CategoryKeyboard)             | [SDL_keyboard.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_keyboard.h) | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Keyboard Keycodes](CategoryKeycode)             | [SDL_keycode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_keycode.h)   | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Keyboard Scancodes](CategoryScancode)           | [SDL_scancode.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_scancode.h) | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Mouse Support](CategoryMouse)                   | [SDL_mouse.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_mouse.h)       | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Joystick Support](CategoryJoystick)             | [SDL_joystick.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_joystick.h) | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Gamepad Support](CategoryGamepad)               | [SDL_gamepad.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_gamepad.h)   | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Touch Support](CategoryTouch)                   | [SDL_touch.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_touch.h)       | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Pen Support](CategoryPen)                       | [SDL_pen.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_pen.h)           | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [Sensors](CategorySensor)                        | [SDL_sensor.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_sensor.h)     | ![In progress](https://img.shields.io/badge/In%20progress-828282) |
| [HIDAPI](CategoryHIDAPI)                         | [SDL_hidapi.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_hidapi.h)     | ![In progress](https://img.shields.io/badge/In%20progress-828282) |


### Force Feedback ("Haptic")

| **View information and functions related to...** | **View the header**                                                                     | **Stage**                                                         |
| ------------------------------------------------ | --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Force Feedback Support](CategoryHaptic)         | [SDL_haptic.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_haptic.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Audio

| **View information and functions related to...**       | **View the header**                                                                     | **Stage**                                                         |
| ------------------------------------------------------ | --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Audio Playback, Recording, and Mixing](CategoryAudio) | [SDL_audio.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_audio.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Threads

| **View information and functions related to...**   | **View the header**                                                                     | **Stage**                                                         |
| -------------------------------------------------- | --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Thread Management](CategoryThread)                | [SDL_thread.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_thread.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Thread Synchronization Primitives](CategoryMutex) | [SDL_mutex.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_mutex.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Atomic Operations](CategoryAtomic)                | [SDL_atomic.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_atomic.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Time

| **View information and functions related to...** | **View the header**                                                                     | **Stage**                                                         |
| ------------------------------------------------ | --------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Timer Support](CategoryTimer)                   | [SDL_timer.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_timer.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Date and Time](CategoryTime)                    | [SDL_time.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_time.h)       | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### File and I/O Abstractions

| **View information and functions related to...** | **View the header**                                                                           | **Stage**                                                         |
| ------------------------------------------------ | --------------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Filesystem Access](CategoryFilesystem)          | [SDL_filesystem.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_filesystem.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Storage Abstraction](CategoryStorage)           | [SDL_storage.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_storage.h)       | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [I/O Streams](CategoryIOStream)                  | [SDL_iostream.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_iostream.h)     | ![In progress](https://img.shields.io/badge/In%20progress-828282) |


### Platform and CPU Information

| **View information and functions related to...** | **View the header**                                                                       | **Stage**                                                         |
| ------------------------------------------------ | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Platform Detection](CategoryPlatform)           | [SDL_platform.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_platform.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [CPU Feature Detection](CategoryCPUInfo)         | [SDL_cpuinfo.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_cpuinfo.h)   | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Byte Order and Byte Swapping](CategoryEndian)   | [SDL_endian.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_endian.h)     | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Bit Manipulation](CategoryBits)                 | [SDL_bits.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_bits.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |


### Additional Functionality

| **View information and functions related to...**     | **View the header**                                                                           | **Stage**                                                         |
| ---------------------------------------------------- | --------------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Shared Object/DLL Management](CategorySharedObject) | [SDL_loadso.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_loadso.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Power Management Status](CategoryPower)             | [SDL_power.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_power.h)           | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Message Boxes](CategoryMessagebox)                  | [SDL_messagebox.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_messagebox.h) | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [File Dialogs](CategoryDialog)                       | [SDL_dialog.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_dialog.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Locale Info](CategoryLocale)                        | [SDL_locale.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_locale.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Platform-specific Functionality](CategorySystem)    | [SDL_system.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_system.h)         | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |
| [Standard Library Functionality](CategoryStdinc)     | [SDL_stdinc.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_stdinc.h)         | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [GUIDs](CategoryGUID)                                | [SDL_guid.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_guid.h)             | ![Ready](https://img.shields.io/badge/Ready-008000)               |
| [Miscellaneous](CategoryMisc)                        | [SDL_misc.h](https://github.com/libsdl-org/SDL/blob/main/include/SDL3/SDL_stdinc.h)           | ![Not ready](https://img.shields.io/badge/Not%20ready-D0312D)     |

## Feedback

If you have any feedback, please reach out to us at eduardgushchin@yandex.ru
