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
    /// <para>Gamepad device event structure (event.gdevice.*)</para>
    /// <para>Joysticks that are supported gamepads receive both an <see cref="JoyDeviceEvent"/>
    /// and an <see cref="GamepadDeviceEvent"/>.</para>
    /// <para>SDL will send <see cref="EventType.GamepadAdded"/> events for joysticks that are already plugged
    /// in during <see cref="Init"/> and are recognized as gamepads. It will also send
    /// events for joysticks that get gamepad mappings at runtime.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="JoyDeviceEvent"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadDeviceEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadAdded"/>, <see cref="EventType.GamepadRemoved"/>,
        /// or <see cref="EventType.GamepadRemapped"/>, <see cref="EventType.GamepadUpdateComplete"/>
        /// or <see cref="EventType.GamepadSteamHandleUpdated"/>
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
    }
}