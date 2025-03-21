﻿#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

namespace FPS_Counter;

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

        if (!SDL.CreateWindowAndRenderer("SDL3 FPS Counter", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }

        SDL.SetRenderVSync(renderer, 1);
        
        var loop = true;
        var startCounter = SDL.GetPerformanceCounter();
        var frequency = SDL.GetPerformanceFrequency();
        var fpsCounter = new FPSCounter();

        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
                if (e.Type == (uint)SDL.EventType.Quit || e is {Type: (uint)SDL.EventType.KeyDown, Key.Key: SDL.Keycode.Escape})
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
            
            fpsCounter.Update();

            SDL.SetRenderDrawColor(renderer, r, g, b, 255);
            SDL.RenderClear(renderer);
            
            SDL.SetRenderDrawColor(renderer, (byte) (255 - r), (byte) (255 - g), (byte) (255 - b), 255);
            SDL.RenderDebugText(renderer, 10, 10, $"FPS: {fpsCounter.FPS:N0}");
            
            SDL.RenderPresent(renderer);
        }
        
        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
}