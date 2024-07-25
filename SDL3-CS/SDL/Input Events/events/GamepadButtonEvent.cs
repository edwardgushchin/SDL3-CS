#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Gamepad button event structure (event.gbutton.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadButtonEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadButtonDown"/> or <see cref="EventType.GamepadButtonUp"/>
        /// </summary>
        public EventType Type;
        private UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The gamepad button (<see cref="GamepadButton"/>)
        /// </summary>
        public GamepadButton Button;
        
        /// <summary>
        /// <see cref="Keystate.Pressed"/> or <see cref="Keystate.Released"/>
        /// </summary>
        public Keystate State;
        private byte Padding1;
        private byte Padding2;
    }
}