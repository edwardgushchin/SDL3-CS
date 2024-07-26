#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
    /// <para>The structure that describes a virtual joystick.</para>
    /// <para>All elements of this structure are optional and can be left 0.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    /// <seealso cref="AttachVirtualJoystick"/>
    /// <seealso cref="VirtualJoystickSensorDesc"/>
    /// <seealso cref="VirtualJoystickTouchpadDesc"/>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct VirtualJoystickDesc
    {
        /// <summary>
        /// <see cref="JoystickType"/>
        /// </summary>
        public JoystickType Type;
        
        /// <summary>
        /// unused
        /// </summary>
        private UInt16 padding;
        
        /// <summary>
        /// the USB vendor ID of this joystick
        /// </summary>
        public UInt16 VendorId;
        
        /// <summary>
        /// the USB product ID of this joystick
        /// </summary>
        public UInt16 ProductId;
        
        /// <summary>
        /// the number of axes on this joystick
        /// </summary>
        public UInt16 NAxes;
        
        /// <summary>
        /// the number of buttons on this joystick
        /// </summary>
        public UInt16 NButtons;
        
        /// <summary>
        /// the number of balls on this joystick
        /// </summary>
        public UInt16 NBalls;
        
        /// <summary>
        /// the number of hats on this joystick
        /// </summary>
        public UInt16 NHats;
        
        /// <summary>
        /// the number of touchpads on this joystick, requires `touchpads` to point at valid descriptions
        /// </summary>
        public UInt16 NTouchpads;
        
        /// <summary>
        /// the number of sensors on this joystick, requires `sensors` to point at valid descriptions
        /// </summary>
        public UInt16 NSensors;
        
        /// <summary>
        /// unused
        /// </summary>
        private unsafe fixed UInt16 padding2[2];
        
        /// <summary>
        /// A mask of which buttons are valid for this controller
        /// e.g. (1 &lt;&lt; <see cref="GamepadButton.South"/>)
        /// </summary>
        public UInt32 ButtonMask;
        
        /// <summary>
        /// A mask of which axes are valid for this controller
        /// e.g. (1 &lt;&lt; <see cref="GamepadAxis.LeftX"/>)
        /// </summary>
        public UInt32 AxisMask;
        
        /// <summary>
        /// the name of the joystick
        /// </summary>
        public string? Name;
        
        /// <summary>
        /// A pointer to an array of touchpad descriptions, required if `ntouchpads` is > 0
        /// </summary>
        public VirtualJoystickTouchpadDesc[]? Touchpads;
        
        /// <summary>
        /// A pointer to an array of sensor descriptions, required if `nsensors` is > 0
        /// </summary>
        public VirtualJoystickSensorDesc[]? Sensors;
        
        /// <summary>
        /// User data pointer passed to callbacks
        /// </summary>
        public IntPtr? UserData;
        
        /// <summary>
        /// Called when the joystick state should be updated
        /// </summary>
        public VirtualJoystickUpdateCallback Update;
        
        /// <summary>
        /// Called when the player index is set
        /// </summary>
        public VirtualJoysticSetPlayerIndexCallback? SetPlayerIndex;
        
        /// <summary>
        /// Implements <see cref="RumbleJoystick"/>
        /// </summary>
        public VirtualJoysticRumbleCallback? Rumble;
        
        /// <summary>
        /// Implements <see cref="RumbleJoystickTriggers"/>
        /// </summary>
        public VirtualJoysticRumbleTriggersCallback? RumbleTriggers;
        
        /// <summary>
        /// Implements <see cref="SetJoystickLED"/>
        /// </summary>
        public VirtualJoysticSetLEDCallback? SetLED;
        
        /// <summary>
        /// Implements <see cref="SendJoystickEffect"/>
        /// </summary>
        public VirtualJoysticSendEffectCallback? SendEffect;
        
        /// <summary>
        /// Implements <see cref="SetGamepadSensorEnabled"/>
        /// </summary>
        public VirtualJoysticSetSensorsEnabledCallback? SetSensorsEnabled;
    }
}