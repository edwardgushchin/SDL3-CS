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

namespace Misc_Power;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Misc Power",
            "com.example.misc-power",
            "examples/misc/power",
            WindowWidth,
            WindowHeight,
            RenderFrame);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var state = SDL.GetPowerInfo(out var seconds, out var percent);
        var powerText = state switch
        {
            SDL.PowerState.OnBattery => "Running on battery",
            SDL.PowerState.NoBattery => "No battery",
            SDL.PowerState.Charging => "Charging",
            SDL.PowerState.Charged => "Charged",
            SDL.PowerState.Error => "Power information unavailable",
            _ => "Power state unknown"
        };

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);

        DrawCenteredText(context.Renderer, powerText, 190.0f);
        DrawCenteredText(context.Renderer, percent >= 0 ? $"Battery: {percent}%" : "Battery percentage unknown", 220.0f);
        DrawCenteredText(context.Renderer, FormatRemaining(seconds), 250.0f);

        SDL.RenderPresent(context.Renderer);
    }

    private static string FormatRemaining(int seconds)
    {
        if (seconds < 0)
        {
            return "Battery time remaining unknown";
        }

        var hours = seconds / 3600;
        var minutes = (seconds % 3600) / 60;
        return $"Time remaining: {hours}h {minutes:00}m";
    }

    private static void DrawCenteredText(IntPtr renderer, string text, float y)
    {
        var x = Math.Max(0.0f, (WindowWidth - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }
}
