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
    /// Joystick hat position change event structure (event.jhat.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickHatMotion"/>
        /// </summary>
        public EventType Type;
        
        private UInt32 _reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The joystick hat index
        /// </summary>
        public Byte Hat;
        
        /// <summary>
        /// The hat position value.
        /// </summary>
        /// <seealso cref="JoystickHat.LeftUp"/>
        /// <seealso cref="JoystickHat.Up"/>
        /// <seealso cref="JoystickHat.RightUp"/>
        /// <seealso cref="JoystickHat.Left"/>
        /// <seealso cref="JoystickHat.Centered"/>
        /// <seealso cref="JoystickHat.Right"/>
        /// <seealso cref="JoystickHat.LeftDown"/>
        /// <seealso cref="JoystickHat.Down"/>
        /// <seealso cref="JoystickHat.RightDown"/>
        public Byte Value;
        
        private Byte _padding1;
        
        private Byte _padding2;
    }
}