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

namespace Renderer_Debug_Text;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Debug Text",
            "com.example.renderer-debug-text",
            "examples/renderer/debug-text",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            cleanup: Cleanup);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        SDL.RenderDebugText(context.Renderer, 272, 100, "Hello world!");
        SDL.RenderDebugText(context.Renderer, 224, 150, "This is some debug text.");

        SDL.SetRenderDrawColor(context.Renderer, 51, 102, 255, 255);
        SDL.RenderDebugText(context.Renderer, 184, 200, "You can do it in different colors.");

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        SDL.SetRenderScale(context.Renderer, 4.0f, 4.0f);
        SDL.RenderDebugText(context.Renderer, 14, 65, "It can be scaled.");
        SDL.SetRenderScale(context.Renderer, 1.0f, 1.0f);
        SDL.RenderDebugText(context.Renderer, 64, 350, "This only does ASCII chars.");

        var seconds = SDL.GetTicks() / 1000;
        var message = $"This program has been running for {seconds} seconds.";
        var x = (WindowWidth - (SDL.DebugTextFontCharacterSize * message.Length)) / 2.0f;
        SDL.RenderDebugText(context.Renderer, x, 400, message);

        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        SDL.SetRenderScale(context.Renderer, 1.0f, 1.0f);
    }
}
