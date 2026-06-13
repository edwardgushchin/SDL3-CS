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

namespace Misc_Locale;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Misc Locale",
            "com.example.misc-locale",
            "examples/misc/locale",
            WindowWidth,
            WindowHeight,
            RenderFrame);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var locales = SDL.GetPreferredLocales(out var count);

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);

        if (locales is null || count == 0)
        {
            DrawCenteredText(context.Renderer, $"Couldn't get preferred locales: {SDL.GetError()}", WindowHeight / 2.0f);
        }
        else
        {
            SDL.RenderDebugText(context.Renderer, 40.0f, 40.0f, "Preferred locales:");

            for (var i = 0; i < locales.Length; i++)
            {
                var locale = locales[i];
                var country = string.IsNullOrEmpty(locale.Country) ? "" : $"-{locale.Country}";
                SDL.RenderDebugText(context.Renderer, 64.0f, 72.0f + (i * 20.0f), $"{i + 1}. {locale.Language}{country}");
            }
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawCenteredText(IntPtr renderer, string text, float y)
    {
        var x = Math.Max(0.0f, (WindowWidth - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }
}
