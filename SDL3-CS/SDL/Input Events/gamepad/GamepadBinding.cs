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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>A mapping between one joystick input to a gamepad control.</para>
    /// <para>A gamepad has a collection of several bindings, to say, for example, when
    /// joystick button number 5 is pressed, that should be treated like the
    /// gamepad's "start" button.</para>
    /// <para>SDL has these bindings built-in for many popular controllers, and can add
    /// more with a simple text string. Those strings are parsed into a collection
    /// of these structs to make it easier to operate on the data.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadBindings"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadBinding
    {
        public GamepadBindingType InputType;

        public InputData Input;

        public GamepadBindingType OutputType;

        public OutputData Output;

        [StructLayout(LayoutKind.Explicit)]
        public struct InputData
        {
            [FieldOffset(0)]
            public int Button;

            [FieldOffset(0)]
            public AxisInfo Axis;

            [FieldOffset(0)]
            public HatInfo Hat;

            [StructLayout(LayoutKind.Sequential)]
            public struct AxisInfo
            {
                public int Axis;

                public int AxisMin;

                public int AxisMax;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct HatInfo
            {
                public int Hat;

                public int HatMask;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct OutputData
        {
            [FieldOffset(0)]
            public GamepadButton Button;

            [FieldOffset(0)]
            public AxisInfo Axis;

            [StructLayout(LayoutKind.Sequential)]
            public struct AxisInfo
            {
                public GamepadAxis Axis;

                public int AxisMin;

                public int AxisMax;
            }
        }
    }
}