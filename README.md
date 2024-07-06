# SDL3-CS
This is SDL3#, a C# wrapper for SDL3.

Project Website: https://github.com/edwardgushchin/SDL3-CS

## License

SDL3 and SDL3# are released under the zlib license. See LICENSE for details.

## About SDL3

For more information about SDL2, visit the SDL wiki:

https://wiki.libsdl.org/SDL3/FrontPage

## Usage

```C#
using SDL3;

namespace SDL3Test;

public static class Program
{
    private static bool _quit;

    private static void Main()
    {
        if (SDL.Init(SDL.InitFlags.Video) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL_Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Create Window", 800, 600, SDL.WindowFlags.Transparent);
        
        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL_Error: {SDL.GetError()}");
            return;
        }

        _quit = false;
        
        while (!_quit)
        {
            while (SDL.PollEvent(out var sdlEvent) != 0)
            {
                if (sdlEvent == SDL.EventType.Quit)
                {
                    _quit = true;
                }
            }
        }

        SDL.DestroyWindow(window);
        
        Console.WriteLine($"SDL_Error: {SDL.GetError()}");
        
        SDL.Quit();
    }
}
```