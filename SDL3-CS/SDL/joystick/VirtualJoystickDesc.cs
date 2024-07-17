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
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct VirtualJoystickDesc
    {
        public JoystickType Type;
        private UInt16 padding;
        public UInt16 VendorId;
        public UInt16 ProductId;
        public UInt16 NAxes;
        public UInt16 NButtons;
        public UInt16 NBalls;
        public UInt16 NHats;
        public UInt16 NTouchpads;
        public UInt16 NSensors;
        private unsafe fixed UInt16 padding2[2];
        public UInt32 ButtonMask;
        public UInt32 AxisMask;
        public string? Name;
        public VirtualJoystickTouchpadDesc[]? Touchpads;
        public VirtualJoystickSensorDesc[]? Sensors;
        public IntPtr? UserData;
        public VirtualJoystickUpdateCallback Update;
        public VirtualJoysticSetPlayerIndexCallback? SetPlayerIndex;
        public VirtualJoysticRumbleCallback? Rumble;
        public VirtualJoysticRumbleTriggersCallback? RumbleTriggers;
        public VirtualJoysticSetLEDCallback? SetLED;
        public VirtualJoysticSendEffectCallback? SendEffect;
        public VirtualJoysticSetSensorsEnabledCallback? SetSensorsEnabled;
    }
}