#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
 */
#endregion

using SDL3;

namespace Circular_Color_Fade;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        SDL.SetLogPriorities(SDL.LogPriority.Trace);
        
        var init = SDL.CreateWindowAndRenderer("SDL3 Create Window", 800, 600, 0, out var window, out var renderer);

        if (!init)
        {
            if (window == IntPtr.Zero)
                Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            
            if (renderer == IntPtr.Zero) 
                Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            
            return;
        }
        
        SDL.SetRenderVSync(renderer, 1);

        var loop = true;
        var startCounter = SDL.GetPerformanceCounter();
        var frequency = SDL.GetPerformanceFrequency();

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
            var currentCounter = SDL.GetPerformanceCounter();
            var elapsed = (currentCounter - startCounter) / (double)frequency;

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