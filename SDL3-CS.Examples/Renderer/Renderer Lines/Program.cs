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

namespace Renderer_Lines;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly Random Random = new();
    private static readonly SDL.FPoint[] LinePoints =
    [
        new() { X = 100, Y = 354 },
        new() { X = 220, Y = 230 },
        new() { X = 140, Y = 230 },
        new() { X = 320, Y = 100 },
        new() { X = 500, Y = 230 },
        new() { X = 420, Y = 230 },
        new() { X = 540, Y = 354 },
        new() { X = 400, Y = 354 },
        new() { X = 100, Y = 354 }
    ];

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Lines",
            "com.example.renderer-lines",
            "examples/renderer/lines",
            WindowWidth,
            WindowHeight,
            RenderFrame);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 100, 100, 100, 255);
        SDL.RenderClear(context.Renderer);

        SDL.SetRenderDrawColor(context.Renderer, 127, 49, 32, 255);
        SDL.RenderLine(context.Renderer, 240, 450, 400, 450);
        SDL.RenderLine(context.Renderer, 240, 356, 400, 356);
        SDL.RenderLine(context.Renderer, 240, 356, 240, 450);
        SDL.RenderLine(context.Renderer, 400, 356, 400, 450);

        SDL.SetRenderDrawColor(context.Renderer, 0, 255, 0, 255);
        SDL.RenderLines(context.Renderer, LinePoints, LinePoints.Length);

        for (var i = 0; i < 360; i++)
        {
            const float size = 30.0f;
            const float x = 320.0f;
            const float y = 95.0f - (size / 2.0f);
            var radians = i * (MathF.PI / 180.0f);

            SDL.SetRenderDrawColor(
                context.Renderer,
                (byte)Random.Next(256),
                (byte)Random.Next(256),
                (byte)Random.Next(256),
                255);
            SDL.RenderLine(context.Renderer, x, y, x + (MathF.Cos(radians) * size), y + (MathF.Sin(radians) * size));
        }

        SDL.RenderPresent(context.Renderer);
    }
}
