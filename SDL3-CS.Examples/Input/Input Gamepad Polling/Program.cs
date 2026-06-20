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

namespace Input_Gamepad_Polling;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly ButtonSpot[] ButtonSpots =
    [
        new(SDL.GamepadButton.South, 497, 266, 38, 38),
        new(SDL.GamepadButton.East, 550, 217, 38, 38),
        new(SDL.GamepadButton.West, 445, 221, 38, 38),
        new(SDL.GamepadButton.North, 499, 173, 38, 38),
        new(SDL.GamepadButton.Back, 235, 228, 32, 29),
        new(SDL.GamepadButton.Guide, 287, 195, 69, 69),
        new(SDL.GamepadButton.Start, 377, 228, 32, 29),
        new(SDL.GamepadButton.LeftStick, 91, 234, 63, 63),
        new(SDL.GamepadButton.RightStick, 381, 354, 63, 63),
        new(SDL.GamepadButton.LeftShoulder, 74, 73, 102, 29),
        new(SDL.GamepadButton.RightShoulder, 468, 73, 102, 29),
        new(SDL.GamepadButton.DPadUp, 207, 316, 32, 32),
        new(SDL.GamepadButton.DPadDown, 207, 384, 32, 32),
        new(SDL.GamepadButton.DPadLeft, 173, 351, 32, 32),
        new(SDL.GamepadButton.DPadRight, 242, 351, 32, 32),
        new(SDL.GamepadButton.Misc1, 310, 286, 23, 27)
    ];
    private static IntPtr _gamepad;
    private static ulong _leftThumbLast;
    private static ulong _rightThumbLast;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Input Gamepad Polling",
            "com.example.input-gamepad-polling",
            "examples/input/gamepad-polling",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            cleanup: Cleanup,
            handleEvent: HandleEvent,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Gamepad,
            presentation: SDL.RendererLogicalPresentation.Stretch);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.GamepadAdded when _gamepad == IntPtr.Zero:
                _gamepad = SDL.OpenGamepad(sdlEvent.GDevice.Which);
                if (_gamepad == IntPtr.Zero)
                {
                    SDL.Log($"Failed to open gamepad ID {sdlEvent.GDevice.Which}: {SDL.GetError()}");
                }
                break;

            case SDL.EventType.GamepadRemoved when _gamepad != IntPtr.Zero && SDL.GetGamepadID(_gamepad) == sdlEvent.GDevice.Which:
                SDL.CloseGamepad(_gamepad);
                _gamepad = IntPtr.Zero;
                break;
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double nowSeconds)
    {
        var text = "Plug in a gamepad, please.";
        if (_gamepad != IntPtr.Zero)
        {
            text = SDL.GetGamepadName(_gamepad) ?? "Unknown gamepad";
        }

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        SDL.RenderClear(context.Renderer);

        if (_gamepad != IntPtr.Zero)
        {
            DrawGamepad(context.Renderer);
        }

        var y = _gamepad != IntPtr.Zero
            ? WindowHeight - (SDL.DebugTextFontCharacterSize + 2)
            : (WindowHeight - SDL.DebugTextFontCharacterSize) / 2.0f;

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 255, 255);
        DrawCenteredText(context.Renderer, y, text);
        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawGamepad(IntPtr renderer)
    {
        DrawGamepadBody(renderer);
        DrawPressedButtons(renderer);

        var now = SDL.GetTicks();
        SDL.SetRenderDrawColor(renderer, 255, 255, 0, 255);

        DrawThumb(renderer, SDL.GamepadAxis.LeftX, SDL.GamepadAxis.LeftY, 107.0f, 252.0f, ref _leftThumbLast, now);
        DrawThumb(renderer, SDL.GamepadAxis.RightX, SDL.GamepadAxis.RightY, 397.0f, 370.0f, ref _rightThumbLast, now);
        DrawTrigger(renderer, SDL.GamepadAxis.LeftTrigger, 127.0f);
        DrawTrigger(renderer, SDL.GamepadAxis.RightTrigger, 481.0f);
    }

    private static void DrawGamepadBody(IntPtr renderer)
    {
        SDL.SetRenderDrawColor(renderer, 230, 230, 230, 255);
        var body = new[]
        {
            new SDL.FRect { X = 90, Y = 125, W = 460, H = 240 },
            new SDL.FRect { X = 60, Y = 215, W = 160, H = 185 },
            new SDL.FRect { X = 420, Y = 215, W = 160, H = 185 }
        };
        SDL.RenderFillRects(renderer, body, body.Length);

        SDL.SetRenderDrawColor(renderer, 90, 90, 90, 255);
        SDL.RenderRects(renderer, body, body.Length);

        foreach (var spot in ButtonSpots)
        {
            var rect = spot.Rect;
            SDL.RenderRect(renderer, in rect);
        }

        SDL.SetRenderDrawColor(renderer, 180, 180, 180, 255);
        var labels = new[]
        {
            ("South", 492.0f, 309.0f),
            ("East", 548.0f, 260.0f),
            ("West", 443.0f, 264.0f),
            ("North", 492.0f, 159.0f),
            ("D-Pad", 188.0f, 426.0f)
        };

        foreach (var (text, x, y) in labels)
        {
            SDL.RenderDebugText(renderer, x, y, text);
        }
    }

    private static void DrawPressedButtons(IntPtr renderer)
    {
        SDL.SetRenderDrawColor(renderer, 0, 255, 0, 255);
        foreach (var spot in ButtonSpots)
        {
            if (SDL.GetGamepadButton(_gamepad, spot.Button))
            {
                var rect = spot.Rect;
                SDL.RenderFillRect(renderer, in rect);
            }
        }
    }

    private static void DrawThumb(IntPtr renderer, SDL.GamepadAxis axisX, SDL.GamepadAxis axisY, float originX, float originY, ref ulong lastMotion, ulong now)
    {
        var x = SDL.GetGamepadAxis(_gamepad, axisX);
        var y = SDL.GetGamepadAxis(_gamepad, axisY);
        if (Math.Abs((int)x) > 1000 || Math.Abs((int)y) > 1000)
        {
            lastMotion = now;
        }

        if (lastMotion == 0 || now - lastMotion >= 500)
        {
            return;
        }

        var box = new SDL.FRect
        {
            X = originX + ((x / 32767.0f) * 30.0f),
            Y = originY + ((y / 32767.0f) * 30.0f),
            W = 30.0f,
            H = 30.0f
        };
        SDL.RenderFillRect(renderer, in box);
    }

    private static void DrawTrigger(IntPtr renderer, SDL.GamepadAxis axis, float x)
    {
        var value = SDL.GetGamepadAxis(_gamepad, axis);
        if (value <= 1000)
        {
            return;
        }

        var height = (value / 32767.0f) * 65.0f;
        var box = new SDL.FRect { X = x, Y = 1.0f + (65.0f - height), W = 37.0f, H = height };
        SDL.RenderFillRect(renderer, in box);
    }

    private static void DrawCenteredText(IntPtr renderer, float y, string text)
    {
        var x = Math.Max(0.0f, (WindowWidth - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_gamepad == IntPtr.Zero)
        {
            return;
        }

        SDL.CloseGamepad(_gamepad);
        _gamepad = IntPtr.Zero;
    }

    private readonly struct ButtonSpot
    {
        public ButtonSpot(SDL.GamepadButton button, float x, float y, float width, float height)
        {
            Button = button;
            Rect = new SDL.FRect { X = x, Y = y, W = width, H = height };
        }

        public SDL.GamepadButton Button { get; }

        public SDL.FRect Rect { get; }
    }
}
