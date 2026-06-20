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

namespace Input_Gamepad_Rumble;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly GamepadInfo[] GamepadsInfo = new GamepadInfo[16];

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Input Gamepad Rumble",
            "com.example.input-gamepad-rumble",
            "examples/input/gamepad-rumble",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            cleanup: Cleanup,
            handleEvent: HandleEvent,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Gamepad);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.GamepadAdded:
            {
                var which = sdlEvent.GDevice.Which;
                if (SDL.OpenGamepad(which) == IntPtr.Zero)
                {
                    SDL.Log($"Failed to open gamepad ID {which}: {SDL.GetError()}");
                    break;
                }

                var index = FindGamepadInfoIndex(0);
                if (index >= 0)
                {
                    GamepadsInfo[index] = new GamepadInfo(which, "idle");
                }

                break;
            }

            case SDL.EventType.GamepadRemoved:
            {
                var which = sdlEvent.GDevice.Which;
                var gamepad = SDL.GetGamepadFromID(which);
                if (gamepad != IntPtr.Zero)
                {
                    SDL.CloseGamepad(gamepad);
                }

                var index = FindGamepadInfoIndex(which);
                if (index >= 0)
                {
                    GamepadsInfo[index] = default;
                }

                break;
            }

            case SDL.EventType.GamepadButtonDown:
                HandleButtonDown(sdlEvent);
                break;

            case SDL.EventType.GamepadButtonUp:
                HandleButtonUp(sdlEvent);
                break;
        }

        return true;
    }

    private static void HandleButtonDown(SDL.Event sdlEvent)
    {
        var which = sdlEvent.GButton.Which;
        var gamepad = SDL.GetGamepadFromID(which);
        var index = FindGamepadInfoIndex(which);
        if (gamepad == IntPtr.Zero)
        {
            return;
        }

        switch ((SDL.GamepadButton)sdlEvent.GButton.Button)
        {
            case SDL.GamepadButton.South:
                SDL.RumbleGamepad(gamepad, 0xFFFF, 0x0000, 5000);
                SetAction(index, "rumble high frequency");
                break;

            case SDL.GamepadButton.East:
                SDL.RumbleGamepad(gamepad, 0x0000, 0xFFFF, 5000);
                SetAction(index, "rumble low frequency");
                break;
        }
    }

    private static void HandleButtonUp(SDL.Event sdlEvent)
    {
        var which = sdlEvent.GButton.Which;
        var gamepad = SDL.GetGamepadFromID(which);
        if (gamepad != IntPtr.Zero)
        {
            SDL.RumbleGamepad(gamepad, 0x0000, 0x0000, 0);
        }

        SetAction(FindGamepadInfoIndex(which), "idle");
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.GetCurrentRenderOutputSize(context.Renderer, out var renderWidth, out _);

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        var y = SDL.DebugTextFontCharacterSize * 8;
        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 0, 255);
        DrawCenteredText(context.Renderer, renderWidth, ref y, "Connect gamepads and press South/East to rumble.");
        y += SDL.DebugTextFontCharacterSize * 3;

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
        foreach (var info in GamepadsInfo)
        {
            if (info.GamepadId == 0)
            {
                DrawCenteredText(context.Renderer, renderWidth, ref y, string.Empty);
                continue;
            }

            var name = SDL.GetGamepadNameForID(info.GamepadId) ?? "Unknown gamepad";
            DrawCenteredText(context.Renderer, renderWidth, ref y, $"{name}: {info.Action}");
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawCenteredText(IntPtr renderer, int renderWidth, ref int y, string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var x = Math.Max(0, (renderWidth - (text.Length * SDL.DebugTextFontCharacterSize)) / 2);
            SDL.RenderDebugText(renderer, x, y, text);
        }

        y += SDL.DebugTextFontCharacterSize * 2;
    }

    private static int FindGamepadInfoIndex(uint which)
    {
        for (var i = 0; i < GamepadsInfo.Length; i++)
        {
            if (GamepadsInfo[i].GamepadId == which)
            {
                return i;
            }
        }

        return -1;
    }

    private static void SetAction(int index, string action)
    {
        if (index < 0)
        {
            return;
        }

        GamepadsInfo[index] = GamepadsInfo[index] with { Action = action };
    }

    private static void Cleanup(RendererExampleContext context)
    {
        for (var i = 0; i < GamepadsInfo.Length; i++)
        {
            var gamepad = SDL.GetGamepadFromID(GamepadsInfo[i].GamepadId);
            if (gamepad != IntPtr.Zero)
            {
                SDL.CloseGamepad(gamepad);
            }

            GamepadsInfo[i] = default;
        }
    }

    private readonly record struct GamepadInfo(uint GamepadId, string Action);
}
