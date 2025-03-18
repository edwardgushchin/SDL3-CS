#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>An enum of some common joystick types.</para>
    /// <para>In some cases, SDL can identify a low-level joystick as being a certain
    /// type of device, and will report it through <see cref="GetJoystickType"/> (or
    /// <see cref="GetJoystickTypeForID"/>).</para>
    /// <para>This is by no means a complete list of everything that can be plugged into
    /// a computer.</para>
    /// <para>You may refer to
    /// <a href="https://learn.microsoft.com/en-us/windows/win32/xinput/xinput-and-controller-subtypes">XInput Controller Types</a>
    /// table for a general understanding of each joystick type.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum JoystickType : ushort
    {
        Unknown,
        Gamepad,
        Wheel,
        ArcadeStick,
        FlightStick,
        DancePad,
        Guitar,
        DrumKit,
        ArcadePad,
        Throttle,
        Count
    }
}