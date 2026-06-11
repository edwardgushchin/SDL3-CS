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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// The list of capsense types on a gamepad
    /// </summary>
    /// <since>This enum is available since SDL 3.6.0.</since>
    /// <seealso cref="GamepadHasCapSense"/>
    /// <seealso cref="GetGamepadCapSense"/>
    public enum GamepadCapSenseType
    {
        Invalid = -1,

        /// <summary>
        /// Activated by touching the top of the left thumbstick
        /// </summary>
        LeftStick,

        /// <summary>
        /// Activated by touching the top of the right thumbstick
        /// </summary>
        RightStick,

        /// <summary>
        /// Activated by gripping the left handle of the controller
        /// </summary>
        LeftGrip,

        /// <summary>
        /// Activated by gripping the right handle of the controller
        /// </summary>
        RightGrip,
        Count
    }
}
