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

namespace Demo_Infinite_Monkeys;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const int MonkeyCount = 100;
    private const string TypeableCharacters = "abcdefghijklmnopqrstuvwxyz     \n";
    private const string TargetText =
        "to be or not to be\n" +
        "that is the question\n" +
        "whether tis nobler in the mind\n" +
        "to suffer the slings and arrows\n";
    private static readonly Queue<string> Lines = new();
    private static char[] _monkeyChars = [];
    private static string _currentLine = "";
    private static int _rows;
    private static int _cols;
    private static int _progress;
    private static long _startTicks;
    private static long _endTicks;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Infinite Monkeys",
            "com.example.infinite-monkeys",
            "examples/demo/infinite-monkeys",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            cleanup: null,
            HandleEvent,
            presentation: SDL.RendererLogicalPresentation.Disabled);
    }

    private static void Configure(RendererExampleContext context)
    {
        SDL.SetRenderVSync(context.Renderer, 1);
        RecalculateGrid(context.Renderer);
        SDL.GetCurrentTime(out _startTicks);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        if (sdlEvent.Type == (uint)SDL.EventType.WindowPixelSizeChanged)
        {
            RecalculateGrid(IntPtr.Zero);
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        if (_cols <= 0 || _rows <= 0)
        {
            RecalculateGrid(context.Renderer);
        }

        SimulateTyping();

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);

        var y = 0.0f;
        foreach (var line in Lines.TakeLast(_rows))
        {
            SDL.RenderDebugText(context.Renderer, 0.0f, y, line);
            y += SDL.DebugTextFontCharacterSize;
        }

        var captionY = (_rows + 1) * SDL.DebugTextFontCharacterSize;
        SDL.RenderDebugText(context.Renderer, 0.0f, captionY, $"Monkeys: {MonkeyCount} - {FormatElapsed()}");
        SDL.RenderDebugText(context.Renderer, 0.0f, captionY + SDL.DebugTextFontCharacterSize, new string(_monkeyChars));

        SDL.SetRenderDrawColor(context.Renderer, 0, 255, 0, 255);
        var progressRect = new SDL.FRect
        {
            X = 0.0f,
            Y = captionY + (SDL.DebugTextFontCharacterSize * 2),
            W = TargetText.Length == 0 ? 0.0f : (_progress / (float)TargetText.Length) * (_cols * SDL.DebugTextFontCharacterSize),
            H = SDL.DebugTextFontCharacterSize
        };
        SDL.RenderFillRect(context.Renderer, in progressRect);
        SDL.RenderPresent(context.Renderer);
    }

    private static void SimulateTyping()
    {
        for (var monkey = 0; monkey < MonkeyCount && _progress < TargetText.Length; monkey++)
        {
            var expected = TargetText[_progress];
            if (!TypeableCharacters.Contains(expected))
            {
                AddTypedCharacter(expected, -1);
                continue;
            }

            var typed = TypeableCharacters[Random.Shared.Next(TypeableCharacters.Length)];
            _monkeyChars[monkey % _monkeyChars.Length] = typed == '\n' ? ' ' : typed;
            if (typed == expected)
            {
                AddTypedCharacter(typed, monkey);
            }
        }

        if (_progress == TargetText.Length && _endTicks == 0)
        {
            SDL.GetCurrentTime(out _endTicks);
        }
    }

    private static void AddTypedCharacter(char character, int monkey)
    {
        if (monkey >= 0 && _monkeyChars.Length > 0)
        {
            _monkeyChars[monkey % _monkeyChars.Length] = character == '\n' ? ' ' : character;
        }

        if (character == '\n' || _currentLine.Length >= _cols)
        {
            Lines.Enqueue(_currentLine);
            _currentLine = "";
            while (Lines.Count > _rows)
            {
                Lines.Dequeue();
            }
        }

        if (character != '\n')
        {
            _currentLine += character;
        }

        _progress++;
    }

    private static string FormatElapsed()
    {
        var stop = _endTicks == 0 ? GetNowTicks() : _endTicks;
        var elapsedSeconds = Math.Max(0, (stop - _startTicks) / 1_000_000_000L);
        var hours = elapsedSeconds / 3600;
        var minutes = (elapsedSeconds % 3600) / 60;
        var seconds = elapsedSeconds % 60;
        return $"{hours}H:{minutes}M:{seconds}S";
    }

    private static long GetNowTicks()
    {
        SDL.GetCurrentTime(out var ticks);
        return ticks;
    }

    private static void RecalculateGrid(IntPtr renderer)
    {
        var width = WindowWidth;
        var height = WindowHeight;
        if (renderer != IntPtr.Zero && SDL.GetCurrentRenderOutputSize(renderer, out var outputWidth, out var outputHeight))
        {
            width = outputWidth;
            height = outputHeight;
        }

        _rows = Math.Max(1, (height / SDL.DebugTextFontCharacterSize) - 4);
        _cols = Math.Max(1, width / SDL.DebugTextFontCharacterSize);
        _monkeyChars = Enumerable.Repeat(' ', _cols).ToArray();
    }
}
