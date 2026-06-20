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

namespace Input_Gamepad_Events;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const ulong MotionEventCooldownMs = 40;
    private static readonly SDL.Color[] Colors = ExampleColors.Create(64, whiteFirst: true);
    private static readonly EventMessageLog Messages = new(Colors);
    private static ulong _axisMotionCooldownTime;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Input Gamepad Events",
            "com.example.input-gamepad-events",
            "examples/input/gamepad-events",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            handleEvent: HandleEvent,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Gamepad);
    }

    private static void Configure(RendererExampleContext context)
    {
        Messages.Add(0, "Please plug in a gamepad.");
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.GamepadAdded:
            {
                var which = sdlEvent.GDevice.Which;
                var gamepad = SDL.OpenGamepad(which);
                if (gamepad == IntPtr.Zero)
                {
                    Messages.Add(which, $"Gamepad #{which} add, but not opened: {SDL.GetError()}");
                }
                else
                {
                    Messages.Add(which, $"Gamepad #{which} ('{SDL.GetGamepadName(gamepad) ?? "Unknown"}') added");

                    var mapping = SDL.GetGamepadMapping(gamepad);
                    if (!string.IsNullOrWhiteSpace(mapping))
                    {
                        Messages.Add(which, $"Gamepad #{which} mapping: {Shorten(mapping, 108)}");
                    }
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

                Messages.Add(which, $"Gamepad #{which} removed");
                break;
            }

            case SDL.EventType.GamepadAxisMotion:
            {
                var now = SDL.GetTicks();
                if (now >= _axisMotionCooldownTime)
                {
                    var which = sdlEvent.GAxis.Which;
                    _axisMotionCooldownTime = now + MotionEventCooldownMs;
                    var axis = (SDL.GamepadAxis)sdlEvent.GAxis.Axis;
                    Messages.Add(which, $"Gamepad #{which} axis {SDL.GetGamepadStringForAxis(axis) ?? axis.ToString()} -> {sdlEvent.GAxis.Value}");
                }

                break;
            }

            case SDL.EventType.GamepadButtonDown:
            case SDL.EventType.GamepadButtonUp:
            {
                var which = sdlEvent.GButton.Which;
                var button = (SDL.GamepadButton)sdlEvent.GButton.Button;
                Messages.Add(which, $"Gamepad #{which} button {SDL.GetGamepadStringForButton(button) ?? button.ToString()} -> {(sdlEvent.GButton.Down ? "PRESSED" : "RELEASED")}");
                break;
            }

            case SDL.EventType.JoystickBatteryUpdated when SDL.IsGamepad(sdlEvent.JBattery.Which):
            {
                var which = sdlEvent.JBattery.Which;
                Messages.Add(which, $"Gamepad #{which} battery -> {BatteryStateString(sdlEvent.JBattery.State)} - {sdlEvent.JBattery.Percent}%");
                break;
            }
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        if (!SDL.GetWindowSize(context.Window, out var width, out var height))
        {
            width = WindowWidth;
            height = WindowHeight;
        }

        Messages.Render(context.Renderer, width, height);
        SDL.RenderPresent(context.Renderer);
    }

    private static string BatteryStateString(SDL.PowerState state)
    {
        return state switch
        {
            SDL.PowerState.Error => "ERROR",
            SDL.PowerState.Unknown => "UNKNOWN",
            SDL.PowerState.OnBattery => "ON BATTERY",
            SDL.PowerState.NoBattery => "NO BATTERY",
            SDL.PowerState.Charging => "CHARGING",
            SDL.PowerState.Charged => "CHARGED",
            _ => "UNKNOWN"
        };
    }

    private static string Shorten(string text, int maxLength)
    {
        return text.Length <= maxLength ? text : text[..(maxLength - 3)] + "...";
    }
}
