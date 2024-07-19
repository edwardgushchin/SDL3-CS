using System;
using SDL3;

namespace Circular_Color_Fade;

internal static class Program
{
    private static void Main()
    {
        if (SDL.Init(SDL.InitFlags.Video) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Circular Color Fade", 800, 600, 0);
        
        if (window.Handle == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var renderer = SDL.CreateRenderer(window, null);
        
        if (renderer.Handle == IntPtr.Zero)
        {
            Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            return;
        }

        var loop = true;
        var startTime = DateTime.Now;

        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
                if (e.Type == SDL.EventType.Quit || e is {Type: SDL.EventType.KeyDown, Key.Key: SDL.Keycode.Escape})
                {
                    loop = false;
                }
            }

            // Calculate elapsed time
            var elapsed = (DateTime.Now - startTime).TotalSeconds;

            // Calculate color components based on sine wave functions
            var r = (byte)(Math.Sin(elapsed) * 127 + 128);
            var g = (byte)(Math.Sin(elapsed + Math.PI / 2) * 127 + 128);
            var b = (byte)(Math.Sin(elapsed + Math.PI) * 127 + 128);

            SDL.SetRenderDrawColor(renderer, r, g, b, 255);
            
            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
}