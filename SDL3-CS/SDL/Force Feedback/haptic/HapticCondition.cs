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
    /// <para>A structure containing a template for a Condition effect.</para>
    /// <para>The struct handles the following effects:</para>
    /// <list type="bullet">
    /// <item><see cref="HAPTIC_SPRING"/>: Effect based on axes position.</item>
    /// <item><see cref="HAPTIC_DAMPER"/>: Effect based on axes velocity.</item>
    /// <item><see cref="HAPTIC_INERTIA"/>: Effect based on axes acceleration.</item>
    /// <item><see cref="HAPTIC_FRICTION"/>: Effect based on axes movement.</item>
    /// </list>
    /// <para>Direction is handled by condition internals instead of a direction member.
    /// The condition effect specific members have three parameters. The first
    /// refers to the X axis, the second refers to the Y axis and the third refers
    /// to the Z axis. The right terms refer to the positive side of the axis and
    /// the left terms refer to the negative side of the axis. Please refer to the
    /// <see cref="HapticDirection"/> diagram for which side is positive and which is
    /// negative.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="HapticDirection"/>
    /// <seealso cref="HAPTIC_SPRING"/>
    /// <seealso cref="HAPTIC_DAMPER"/>
    /// <seealso cref="HAPTIC_INERTIA"/>
    /// <seealso cref="HAPTIC_FRICTION"/>
    /// <seealso cref="HapticEffect"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct HapticCondition
    {
        /// <summary>
        /// <see cref="HAPTIC_SPRING"/>, <see cref="HAPTIC_DAMPER"/>,
        /// <see cref="HAPTIC_INERTIA"/> or <see cref="HAPTIC_FRICTION"/>
        /// </summary>
        public UInt16 Type;

        /// <summary>
        /// Direction of the effect.
        /// </summary>
        public HapticDirection Direction;

        /// <summary>
        /// Duration of the effect.
        /// </summary>
        public int Length;

        /// <summary>
        /// Delay before starting the effect.
        /// </summary>
        public UInt16 Delay;

        /// <summary>
        /// Button that triggers the effect.
        /// </summary>
        public UInt16 Button;

        /// <summary>
        /// How soon it can be triggered again after button.
        /// </summary>
        public UInt16 Interval;

        /// <summary>
        /// Level when joystick is to the positive side; max 0xFFFF.
        /// </summary>
        public unsafe fixed UInt16 RightSat[3];
        
        /// <summary>
        /// Level when joystick is to the negative side; max 0xFFFF.
        /// </summary>
        public unsafe fixed UInt16 LeftSat[3];
        
        /// <summary>
        /// How fast to increase the force towards the positive side.
        /// </summary>
        public unsafe fixed UInt16 RightCoeff[3];
        
        /// <summary>
        /// How fast to increase the force towards the negative side.
        /// </summary>
        public unsafe fixed UInt16 LeftCoeff[3];
        
        /// <summary>
        /// Size of the dead zone; max 0xFFFF: whole axis-range when 0-centered.
        /// </summary>
        public unsafe fixed UInt16 Deadband[3];
        
        /// <summary>
        /// Position of the dead zone.
        /// </summary>
        public unsafe fixed UInt16 Center[3];
    }
}