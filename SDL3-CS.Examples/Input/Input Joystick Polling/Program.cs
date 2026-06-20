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

namespace Input_Joystick_Polling;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const float ItemSize = 30.0f;
    private static readonly SDL.Color[] Colors = ExampleColors.Create(64);
    private static IntPtr _joystick;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Input Joystick Polling",
            "com.example.input-joystick-polling",
            "examples/input/joystick-polling",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            cleanup: Cleanup,
            handleEvent: HandleEvent,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Joystick);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.JoystickAdded when _joystick == IntPtr.Zero:
                _joystick = SDL.OpenJoystick(sdlEvent.JDevice.Which);
                if (_joystick == IntPtr.Zero)
                {
                    SDL.Log($"Failed to open joystick ID {sdlEvent.JDevice.Which}: {SDL.GetError()}");
                }
                break;

            case SDL.EventType.JoystickRemoved when _joystick != IntPtr.Zero && SDL.GetJoystickID(_joystick) == sdlEvent.JDevice.Which:
                SDL.CloseJoystick(_joystick);
                _joystick = IntPtr.Zero;
                break;
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        if (!SDL.GetWindowSize(context.Window, out var windowWidth, out var windowHeight))
        {
            windowWidth = WindowWidth;
            windowHeight = WindowHeight;
        }

        var text = "Plug in a joystick, please.";
        if (_joystick != IntPtr.Zero)
        {
            text = SDL.GetJoystickName(_joystick) ?? "Unknown joystick";
            DrawJoystickState(context.Renderer, windowWidth, windowHeight);
        }

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        DrawCenteredText(context.Renderer, windowWidth, windowHeight / 2.0f, text);
        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawJoystickState(IntPtr renderer, int windowWidth, int windowHeight)
    {
        var axisCount = SDL.GetNumJoystickAxes(_joystick);
        var axisY = (windowHeight - (axisCount * ItemSize)) / 2.0f;
        var centerX = windowWidth / 2.0f;

        for (var i = 0; i < axisCount; i++)
        {
            var color = Colors[i % Colors.Length];
            var value = SDL.GetJoystickAxis(_joystick, i) / 32767.0f;
            var length = value * centerX;
            var dst = new SDL.FRect
            {
                X = length >= 0.0f ? centerX : centerX + length,
                Y = axisY,
                W = MathF.Abs(length),
                H = ItemSize
            };

            SDL.SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            SDL.RenderFillRect(renderer, in dst);
            axisY += ItemSize;
        }

        var buttonCount = SDL.GetNumJoystickButtons(_joystick);
        var buttonX = (windowWidth - (buttonCount * ItemSize)) / 2.0f;
        for (var i = 0; i < buttonCount; i++)
        {
            var color = Colors[i % Colors.Length];
            var dst = new SDL.FRect { X = buttonX, Y = 0.0f, W = ItemSize, H = ItemSize };

            if (SDL.GetJoystickButton(_joystick, i))
            {
                SDL.SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            }
            else
            {
                SDL.SetRenderDrawColor(renderer, 0, 0, 0, 255);
            }

            SDL.RenderFillRect(renderer, in dst);
            SDL.SetRenderDrawColor(renderer, 255, 255, 255, 255);
            SDL.RenderRect(renderer, in dst);
            buttonX += ItemSize;
        }

        var hatCount = SDL.GetNumJoystickHats(_joystick);
        var hatX = ((windowWidth - (hatCount * (ItemSize * 2.0f))) / 2.0f) + (ItemSize / 2.0f);
        var hatY = windowHeight - ItemSize;
        for (var i = 0; i < hatCount; i++)
        {
            var color = Colors[i % Colors.Length];
            var thirdSize = ItemSize / 3.0f;
            var cross = new[]
            {
                new SDL.FRect { X = hatX, Y = hatY + thirdSize, W = ItemSize, H = thirdSize },
                new SDL.FRect { X = hatX + thirdSize, Y = hatY, W = thirdSize, H = ItemSize }
            };

            SDL.SetRenderDrawColor(renderer, 90, 90, 90, 255);
            SDL.RenderFillRects(renderer, cross, cross.Length);

            var hat = SDL.GetJoystickHat(_joystick, i);
            SDL.SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            DrawHatDirection(renderer, hat, SDL.JoystickHat.Up, hatX + thirdSize, hatY, thirdSize);
            DrawHatDirection(renderer, hat, SDL.JoystickHat.Right, hatX + (thirdSize * 2.0f), hatY + thirdSize, thirdSize);
            DrawHatDirection(renderer, hat, SDL.JoystickHat.Down, hatX + thirdSize, hatY + (thirdSize * 2.0f), thirdSize);
            DrawHatDirection(renderer, hat, SDL.JoystickHat.Left, hatX, hatY + thirdSize, thirdSize);

            hatX += ItemSize * 2.0f;
        }
    }

    private static void DrawHatDirection(IntPtr renderer, SDL.JoystickHat hat, SDL.JoystickHat direction, float x, float y, float size)
    {
        if ((hat & direction) == SDL.JoystickHat.Centered)
        {
            return;
        }

        var dst = new SDL.FRect { X = x, Y = y, W = size, H = size };
        SDL.RenderFillRect(renderer, in dst);
    }

    private static void DrawCenteredText(IntPtr renderer, int width, float y, string text)
    {
        var x = Math.Max(0.0f, (width - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_joystick == IntPtr.Zero)
        {
            return;
        }

        SDL.CloseJoystick(_joystick);
        _joystick = IntPtr.Zero;
    }
}
