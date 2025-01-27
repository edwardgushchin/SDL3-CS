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
    /// Gamepad touchpad event structure (event.gtouchpad.*)
    /// </summary>
    /// <since>TThis struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadTouchpadEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadTouchpadDown"/> or <see cref="EventType.GamepadTouchpadMotion"/>
        /// or <see cref="EventType.GamepadTouchpadUp"/>
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
        /// The index of the touchpad
        /// </summary>
        public Int32 Touchpad;
        
        /// <summary>
        /// The index of the finger on the touchpad
        /// </summary>
        public Int32 Finger;
        
        /// <summary>
        /// Normalized in the range 0...1 with 0 being on the left
        /// </summary>
        public float X;
        
        /// <summary>
        /// Normalized in the range 0...1 with 0 being at the top
        /// </summary>
        public float Y;
        
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public float Pressure;
    }
}