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

namespace Misc_Clipboard;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly string ClipboardText = $"SDL3-CS clipboard text created at {System.DateTime.Now:O}";
    private static readonly SDL.FRect CopyButton = new() { X = 110.0f, Y = 100.0f, W = 180.0f, H = 56.0f };
    private static readonly SDL.FRect PasteButton = new() { X = 350.0f, Y = 100.0f, W = 180.0f, H = 56.0f };
    private static readonly SDL.FRect TextBox = new() { X = 80.0f, Y = 220.0f, W = 480.0f, H = 160.0f };
    private static IntPtr _renderer;
    private static bool _copyPressed;
    private static bool _pastePressed;
    private static string _pastedText = "";

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Misc Clipboard",
            "com.example.misc-clipboard",
            "examples/misc/clipboard",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent);
    }

    private static void Configure(RendererExampleContext context)
    {
        _renderer = context.Renderer;
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        if (_renderer != IntPtr.Zero)
        {
            SDL.ConvertEventToRenderCoordinates(_renderer, ref sdlEvent);
        }

        if (sdlEvent.Type == (uint)SDL.EventType.MouseButtonDown && sdlEvent.Button.Button == SDL.ButtonLeft)
        {
            _copyPressed = PointInRect(sdlEvent.Button.X, sdlEvent.Button.Y, in CopyButton);
            _pastePressed = PointInRect(sdlEvent.Button.X, sdlEvent.Button.Y, in PasteButton);
        }
        else if (sdlEvent.Type == (uint)SDL.EventType.MouseButtonUp && sdlEvent.Button.Button == SDL.ButtonLeft)
        {
            var copyReleased = _copyPressed && PointInRect(sdlEvent.Button.X, sdlEvent.Button.Y, in CopyButton);
            var pasteReleased = _pastePressed && PointInRect(sdlEvent.Button.X, sdlEvent.Button.Y, in PasteButton);
            _copyPressed = false;
            _pastePressed = false;

            if (copyReleased && !SDL.SetClipboardText(ClipboardText))
            {
                SDL.Log($"Couldn't set clipboard text: {SDL.GetError()}");
            }

            if (pasteReleased)
            {
                _pastedText = SDL.GetClipboardText();
            }
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        DrawButton(context.Renderer, CopyButton, "Copy", _copyPressed);
        DrawButton(context.Renderer, PasteButton, "Paste", _pastePressed);

        SDL.SetRenderDrawColor(context.Renderer, 64, 64, 64, 255);
        SDL.RenderRect(context.Renderer, in TextBox);
        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        DrawWrappedText(context.Renderer, string.IsNullOrEmpty(_pastedText) ? "Paste clipboard text here." : _pastedText, TextBox.X + 10.0f, TextBox.Y + 10.0f, TextBox.W - 20.0f);
        DrawCenteredText(context.Renderer, GetCurrentLocalTime(), WindowHeight - 48.0f);

        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawButton(IntPtr renderer, SDL.FRect rect, string text, bool pressed)
    {
        SDL.SetRenderDrawColor(renderer, pressed ? (byte)70 : (byte)32, pressed ? (byte)140 : (byte)96, pressed ? (byte)230 : (byte)180, 255);
        SDL.RenderFillRect(renderer, in rect);
        SDL.SetRenderDrawColor(renderer, 255, 255, 255, 255);
        SDL.RenderRect(renderer, in rect);

        var textX = rect.X + ((rect.W - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        var textY = rect.Y + ((rect.H - SDL.DebugTextFontCharacterSize) / 2.0f);
        SDL.RenderDebugText(renderer, textX, textY, text);
    }

    private static void DrawWrappedText(IntPtr renderer, string text, float x, float y, float width)
    {
        var maxChars = Math.Max(1, (int)(width / SDL.DebugTextFontCharacterSize));
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var line = "";

        foreach (var word in words)
        {
            var next = line.Length == 0 ? word : $"{line} {word}";
            if (next.Length > maxChars)
            {
                SDL.RenderDebugText(renderer, x, y, line);
                y += SDL.DebugTextFontCharacterSize + 4.0f;
                line = word;
            }
            else
            {
                line = next;
            }
        }

        if (!string.IsNullOrEmpty(line))
        {
            SDL.RenderDebugText(renderer, x, y, line);
        }
    }

    private static string GetCurrentLocalTime()
    {
        if (!SDL.GetCurrentTime(out var ticks) || !SDL.TimeToDateTime(ticks, out var dateTime, true))
        {
            return "Local time unavailable";
        }

        return $"Local time: {dateTime.Year:0000}-{dateTime.Month:00}-{dateTime.Day:00} {dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}";
    }

    private static void DrawCenteredText(IntPtr renderer, string text, float y)
    {
        var x = Math.Max(0.0f, (WindowWidth - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }

    private static bool PointInRect(float x, float y, in SDL.FRect rect)
    {
        return x >= rect.X && x < rect.X + rect.W && y >= rect.Y && y < rect.Y + rect.H;
    }

    private static void Cleanup(RendererExampleContext context)
    {
        _renderer = IntPtr.Zero;
        _copyPressed = false;
        _pastePressed = false;
        _pastedText = "";
    }
}
