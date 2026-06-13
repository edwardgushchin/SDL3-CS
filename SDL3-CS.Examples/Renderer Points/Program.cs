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

namespace Renderer_Points;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const int MinimumPixelsPerSecond = 30;
    private const int MaximumPixelsPerSecond = 60;
    private static readonly Random Random = new(1);
    private static readonly SDL.FPoint[] Points = new SDL.FPoint[500];
    private static readonly float[] PointSpeeds = new float[Points.Length];
    private static ulong _lastTime;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Points",
            "com.example.renderer-points",
            "examples/renderer/points",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure);
    }

    private static void Configure(RendererExampleContext context)
    {
        for (var i = 0; i < Points.Length; i++)
        {
            Points[i].X = Random.NextSingle() * WindowWidth;
            Points[i].Y = Random.NextSingle() * WindowHeight;
            PointSpeeds[i] = RandomSpeed();
        }

        _lastTime = SDL.GetTicks();
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var currentTime = SDL.GetTicks();
        var elapsed = (currentTime - _lastTime) / 1000.0f;

        for (var i = 0; i < Points.Length; i++)
        {
            var distance = elapsed * PointSpeeds[i];
            Points[i].X += distance;
            Points[i].Y += distance;

            if (Points[i].X < WindowWidth && Points[i].Y < WindowHeight)
            {
                continue;
            }

            if (Random.Next(2) == 0)
            {
                Points[i].X = Random.NextSingle() * WindowWidth;
                Points[i].Y = 0.0f;
            }
            else
            {
                Points[i].X = 0.0f;
                Points[i].Y = Random.NextSingle() * WindowHeight;
            }

            PointSpeeds[i] = RandomSpeed();
        }

        _lastTime = currentTime;

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        SDL.RenderPoints(context.Renderer, Points, Points.Length);
        SDL.RenderPresent(context.Renderer);
    }

    private static float RandomSpeed()
    {
        return MinimumPixelsPerSecond + (Random.NextSingle() * (MaximumPixelsPerSecond - MinimumPixelsPerSecond));
    }
}
