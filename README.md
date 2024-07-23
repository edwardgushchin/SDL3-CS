# SDL3-CS
This is SDL3#, a C# wrapper for SDL3.

Project Website: https://github.com/edwardgushchin/SDL3-CS

## License

SDL3 and SDL3# are released under the zlib license. See LICENSE for details.

## About SDL3

For more information about SDL3, visit the SDL wiki:

https://wiki.libsdl.org/SDL3/FrontPage


## Install

```
git clone https://github.com/edwardgushchin/SDL3-CS
cd SDL3-CS
dotnet build -c Release
```

## Usage

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
