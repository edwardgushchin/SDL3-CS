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

namespace Renderer_Primitives;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly Random Random = new(1);
    private static readonly SDL.FPoint[] Points = new SDL.FPoint[500];

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Primitives",
            "com.example.renderer-primitives",
            "examples/renderer/primitives",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure);
    }

    private static void Configure(RendererExampleContext context)
    {
        for (var i = 0; i < Points.Length; i++)
        {
            Points[i].X = (Random.NextSingle() * 440.0f) + 100.0f;
            Points[i].Y = (Random.NextSingle() * 280.0f) + 100.0f;
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 33, 33, 33, 255);
        SDL.RenderClear(context.Renderer);

        var rect = new SDL.FRect
        {
            X = 100,
            Y = 100,
            W = 440,
            H = 280
        };

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 255, 255);
        SDL.RenderFillRect(context.Renderer, in rect);

        SDL.SetRenderDrawColor(context.Renderer, 255, 0, 0, 255);
        SDL.RenderPoints(context.Renderer, Points, Points.Length);

        rect.X += 30;
        rect.Y += 30;
        rect.W -= 60;
        rect.H -= 60;

        SDL.SetRenderDrawColor(context.Renderer, 0, 255, 0, 255);
        SDL.RenderRect(context.Renderer, in rect);

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 0, 255);
        SDL.RenderLine(context.Renderer, 0, 0, WindowWidth, WindowHeight);
        SDL.RenderLine(context.Renderer, 0, WindowHeight, WindowWidth, 0);

        SDL.RenderPresent(context.Renderer);
    }
}
