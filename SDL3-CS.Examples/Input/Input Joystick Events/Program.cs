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

namespace Input_Joystick_Events;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const ulong MotionEventCooldownMs = 40;
    private static readonly SDL.Color[] Colors = ExampleColors.Create(64, whiteFirst: true);
    private static readonly EventMessageLog Messages = new(Colors);
    private static ulong _axisMotionCooldownTime;
    private static ulong _ballMotionCooldownTime;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Input Joystick Events",
            "com.example.input-joystick-events",
            "examples/input/joystick-events",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            handleEvent: HandleEvent,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Joystick);
    }

    private static void Configure(RendererExampleContext context)
    {
        Messages.Add(0, "Please plug in a joystick.");
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.JoystickAdded:
            {
                var which = sdlEvent.JDevice.Which;
                var joystick = SDL.OpenJoystick(which);
                if (joystick == IntPtr.Zero)
                {
                    Messages.Add(which, $"Joystick #{which} add, but not opened: {SDL.GetError()}");
                }
                else
                {
                    Messages.Add(which, $"Joystick #{which} ('{SDL.GetJoystickName(joystick) ?? "Unknown"}') added");
                }

                break;
            }

            case SDL.EventType.JoystickRemoved:
            {
                var which = sdlEvent.JDevice.Which;
                var joystick = SDL.GetJoystickFromID(which);
                if (joystick != IntPtr.Zero)
                {
                    SDL.CloseJoystick(joystick);
                }

                Messages.Add(which, $"Joystick #{which} removed");
                break;
            }

            case SDL.EventType.JoystickAxisMotion:
            {
                var now = SDL.GetTicks();
                if (now >= _axisMotionCooldownTime)
                {
                    var which = sdlEvent.JAxis.Which;
                    _axisMotionCooldownTime = now + MotionEventCooldownMs;
                    Messages.Add(which, $"Joystick #{which} axis {sdlEvent.JAxis.Axis} -> {sdlEvent.JAxis.Value}");
                }

                break;
            }

            case SDL.EventType.JoystickBallMotion:
            {
                var now = SDL.GetTicks();
                if (now >= _ballMotionCooldownTime)
                {
                    var which = sdlEvent.JBall.Which;
                    _ballMotionCooldownTime = now + MotionEventCooldownMs;
                    Messages.Add(which, $"Joystick #{which} ball {sdlEvent.JBall.Ball} -> {sdlEvent.JBall.XRel}, {sdlEvent.JBall.YRel}");
                }

                break;
            }

            case SDL.EventType.JoystickHatMotion:
            {
                var which = sdlEvent.JHat.Which;
                Messages.Add(which, $"Joystick #{which} hat {sdlEvent.JHat.Hat} -> {HatStateString((SDL.JoystickHat)sdlEvent.JHat.Value)}");
                break;
            }

            case SDL.EventType.JoystickButtonDown:
            case SDL.EventType.JoystickButtonUp:
            {
                var which = sdlEvent.JButton.Which;
                Messages.Add(which, $"Joystick #{which} button {sdlEvent.JButton.Button} -> {(sdlEvent.JButton.Down ? "PRESSED" : "RELEASED")}");
                break;
            }

            case SDL.EventType.JoystickBatteryUpdated:
            {
                var which = sdlEvent.JBattery.Which;
                Messages.Add(which, $"Joystick #{which} battery -> {BatteryStateString(sdlEvent.JBattery.State)} - {sdlEvent.JBattery.Percent}%");
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

    private static string HatStateString(SDL.JoystickHat state)
    {
        return state switch
        {
            SDL.JoystickHat.Centered => "CENTERED",
            SDL.JoystickHat.Up => "UP",
            SDL.JoystickHat.Right => "RIGHT",
            SDL.JoystickHat.Down => "DOWN",
            SDL.JoystickHat.Left => "LEFT",
            SDL.JoystickHat.RightUp => "RIGHT+UP",
            SDL.JoystickHat.RightDown => "RIGHT+DOWN",
            SDL.JoystickHat.LeftUp => "LEFT+UP",
            SDL.JoystickHat.LeftDown => "LEFT+DOWN",
            _ => "UNKNOWN"
        };
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
}
