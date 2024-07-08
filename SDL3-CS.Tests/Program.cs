#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using SDL3;

namespace SDL3_CS.Tests;

public static class Program
{
    private static void Main()
    {
        if (SDL.Init(SDL.InitFlags.Video) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Create Window", 800, 600, SDL.WindowFlags.Fullscreen);
        
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
        
        var h = 0.0f; // Начальный оттенок
        
        while (loop)
        {
            
            while (SDL.PollEvent(out var e) != 0)
            {
                if (e.Type == SDL.EventType.Quit)
                {
                    loop = false;
                }

                if (e.Type == SDL.EventType.KeyDown)
                {
                    loop = false;
                }
            }
            
            var (r, g, b, a, newH) = GetNextColor(h, 1.0f, 1.0f);
            
            h = newH;
            
            SDL.SetRenderDrawColor(renderer, r,g,b,a);

            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
    
    private static (byte r, byte g, byte b, byte a, float h) GetNextColor(float h, float s, float v, float step = 0.0001f)
    {
        h = (float)((h + step) % 1.0);
        var (r, g, b) = HsvToRgb(h, s, v);
        byte a = 255;
        return (r, g, b, a, h);
    }
    
    private static (byte r, byte g, byte b) HsvToRgb(double h, double s, double v)
    {
        var i = (int)(h * 6);
        var f = h * 6 - i;
        var p = v * (1 - s);
        var q = v * (1 - f * s);
        var t = v * (1 - (1 - f) * s);

        double r = 0, g = 0, b = 0;

        switch (i % 6)
        {
            case 0: r = v; g = t; b = p; break;
            case 1: r = q; g = v; b = p; break;
            case 2: r = p; g = v; b = t; break;
            case 3: r = p; g = q; b = v; break;
            case 4: r = t; g = p; b = v; break;
            case 5: r = v; g = p; b = q; break;
        }

        return ((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
    }
}