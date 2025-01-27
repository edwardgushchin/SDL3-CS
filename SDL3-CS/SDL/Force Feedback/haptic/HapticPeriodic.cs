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
    /// <para>A structure containing a template for a Periodic effect.</para>
    /// <para>The struct handles the following effects:</para>
    /// <list type="bullet">
    /// <item><see cref="HAPTIC_SINE"/></item>
    /// <item><see cref="HAPTIC_SQUARE"/></item>
    /// <item><see cref="HAPTIC_TRIANGLE"/></item>
    /// <item><see cref="HAPTIC_SAWTOOTHUP"/></item>
    /// <item><see cref="HAPTIC_SAWTOOTHDOWN"/></item>
    /// </list>
    /// <para>A periodic effect consists in a wave-shaped effect that repeats itself over
    /// time. The type determines the shape of the wave and the parameters
    /// determine the dimensions of the wave.</para>
    /// <para>Phase is given by hundredth of a degree meaning that giving the phase a
    /// value of 9000 will displace it 25% of its period. Here are sample values:</para>
    /// <list type="bullet">
    /// <item>0: No phase displacement.</item>
    /// <item>9000: Displaced 25% of its period.</item>
    /// <item>18000: Displaced 50% of its period.</item>
    /// <item>27000: Displaced 75% of its period.</item>
    /// <item>36000: Displaced 100% of its period, same as 0, but 0 is preferred.</item>
    /// </list>
    /// <para>Examples:</para>
    /// <code>
    ///    SDL_HAPTIC_SINE
    ///     __      __      __      __
    ///    /  \    /  \    /  \    /
    ///   /    \__/    \__/    \__/
    ///
    ///   SDL_HAPTIC_SQUARE
    ///    __    __    __    __    __
    ///   |  |  |  |  |  |  |  |  |  |
    ///   |  |__|  |__|  |__|  |__|  |
    ///
    ///   SDL_HAPTIC_TRIANGLE
    ///     /\    /\    /\    /\    /\
    ///    /  \  /  \  /  \  /  \  /
    ///   /    \/    \/    \/    \/
    ///
    ///   SDL_HAPTIC_SAWTOOTHUP
    ///     /|  /|  /|  /|  /|  /|  /|
    ///    / | / | / | / | / | / | / |
    ///   /  |/  |/  |/  |/  |/  |/  |
    ///
    ///   SDL_HAPTIC_SAWTOOTHDOWN
    ///   \  |\  |\  |\  |\  |\  |\  |
    ///    \ | \ | \ | \ | \ | \ | \ |
    ///     \|  \|  \|  \|  \|  \|  \|
    /// </code>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="HAPTIC_SINE"/>
    /// <seealso cref="HAPTIC_SQUARE"/>
    /// <seealso cref="HAPTIC_TRIANGLE"/>
    /// <seealso cref="HAPTIC_SAWTOOTHUP"/>
    /// <seealso cref="HAPTIC_SAWTOOTHDOWN"/>
    /// <seealso cref="HapticEffect"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct HapticPeriodic
    {
        /// <summary>
        /// <see cref="HAPTIC_SINE"/>, <see cref="HAPTIC_SQUARE"/>
        /// <see cref="HAPTIC_TRIANGLE"/>, <see cref="HAPTIC_SAWTOOTHUP"/> or
        /// <see cref="HAPTIC_SAWTOOTHDOWN"/>
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
        /// Period of the wave.
        /// </summary>
        public UInt16 Period;

        /// <summary>
        /// Peak value; if negative, equivalent to 180 degrees extra phase shift.
        /// </summary>
        public short Magnitude;

        /// <summary>
        /// Mean value of the wave.
        /// </summary>
        public short Offset;

        /// <summary>
        /// Positive phase shift given by hundredth of a degree.
        /// </summary>
        public UInt16 Phase;

        /// <summary>
        /// Duration of the attack.
        /// </summary>
        public UInt16 AttackLength;

        /// <summary>
        /// Level at the start of the attack.
        /// </summary>
        public UInt16 AttackLevel;

        /// <summary>
        /// Duration of the fade.
        /// </summary>
        public UInt16 FadeLength;

        /// <summary>
        /// Level at the end of the fade.
        /// </summary>
        public UInt16 FadeLevel;
    }
}