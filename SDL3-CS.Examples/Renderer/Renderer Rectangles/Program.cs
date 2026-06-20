#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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
using SDL3.Examples.Common;

namespace Renderer_Rectangles;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly SDL.FRect[] Rects = new SDL.FRect[16];

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Rectangles",
            "com.example.renderer-rectangles",
            "examples/renderer/rectangles",
            WindowWidth,
            WindowHeight,
            RenderFrame);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var ticks = SDL.GetTicks();
        var direction = (ticks % 2000) >= 1000 ? 1.0f : -1.0f;
        var scale = ((((int)(ticks % 1000)) - 500) / 500.0f) * direction;

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        Rects[0] = new SDL.FRect
        {
            X = 100,
            Y = 100,
            W = 100 + (100 * scale),
            H = 100 + (100 * scale)
        };
        SDL.SetRenderDrawColor(context.Renderer, 255, 0, 0, 255);
        SDL.RenderRect(context.Renderer, in Rects[0]);

        for (var i = 0; i < 3; i++)
        {
            var size = (i + 1) * 50.0f;
            Rects[i].W = size + (size * scale);
            Rects[i].H = size + (size * scale);
            Rects[i].X = (WindowWidth - Rects[i].W) / 2;
            Rects[i].Y = (WindowHeight - Rects[i].H) / 2;
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 255, 0, 255);
        SDL.RenderRects(context.Renderer, Rects, 3);

        Rects[0] = new SDL.FRect
        {
            X = 400,
            Y = 50,
            W = 100 + (100 * scale),
            H = 50 + (50 * scale)
        };
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 255, 255);
        SDL.RenderFillRect(context.Renderer, in Rects[0]);

        for (var i = 0; i < Rects.Length; i++)
        {
            var width = (float)WindowWidth / Rects.Length;
            var height = i * 8.0f;
            Rects[i].X = i * width;
            Rects[i].Y = WindowHeight - height;
            Rects[i].W = width;
            Rects[i].H = height;
        }

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        SDL.RenderFillRects(context.Renderer, Rects, Rects.Length);

        SDL.RenderPresent(context.Renderer);
    }
}
