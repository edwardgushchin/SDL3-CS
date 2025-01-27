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
    /// <para>A structure containing a template for a Left/Right effect.</para>
    /// <para>This struct is exclusively for the <see cref="HAPTIC_LEFTRIGHT"/> effect.</para>
    /// <para>The Left/Right effect is used to explicitly control the large and small
    /// motors, commonly found in modern game controllers. The small (right) motor
    /// is high frequency, and the large (left) motor is low frequency.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="HAPTIC_LEFTRIGHT"/>
    /// <seealso cref="HapticEffect"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct HapticLeftRight
    {
        /// <summary>
        /// <see cref="HAPTIC_LEFTRIGHT"/>
        /// </summary>
        public UInt16 Type;

        /// <summary>
        /// Duration of the effect in milliseconds.
        /// </summary>
        public int Length;

        /// <summary>
        /// Control of the large controller motor.
        /// </summary>
        public UInt16 LargeMagnitude;

        /// <summary>
        /// Control of the small controller motor.
        /// </summary>
        public UInt16 SmallMagnitude;
    }
}