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

namespace Renderer_Clear;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Clear",
            "com.example.renderer-clear",
            "examples/renderer/clear",
            WindowWidth,
            WindowHeight,
            RenderFrame);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var red = (float)(0.5 + (0.5 * Math.Sin(now)));
        var green = (float)(0.5 + (0.5 * Math.Sin(now + (Math.PI * 2 / 3))));
        var blue = (float)(0.5 + (0.5 * Math.Sin(now + (Math.PI * 4 / 3))));

        SDL.SetRenderDrawColorFloat(context.Renderer, red, green, blue, SDL.AlphaOpaqueFloat);
        SDL.RenderClear(context.Renderer);
        SDL.RenderPresent(context.Renderer);
    }
}
