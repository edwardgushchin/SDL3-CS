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
    /// <para>Structure that represents a haptic direction.</para>
    /// <para>This is the direction where the force comes from, instead of the direction
    /// in which the force is exerted.</para>
    /// <para>Directions can be specified by:</para>
    /// <list type="bullet">
    /// <item><see cref="HAPTIC_POLAR"/> : Specified by polar coordinates.</item>
    /// <item><see cref="HAPTIC_CARTESIAN"/> : Specified by cartesian coordinates.</item>
    /// <item><see cref="HAPTIC_SPHERICAL"/> : Specified by spherical coordinates.</item>
    /// </list>
    /// <para>Cardinal directions of the haptic device are relative to the positioning of
    /// the device. North is considered to be away from the user.</para>
    /// <para>The following diagram represents the cardinal directions:</para>
    /// <code>
    ///                .--.
    ///                |__| .-------.
    ///                |=.| |.-----.|
    ///                |--| ||     ||
    ///                |  | |'-----'|
    ///                |__|~')_____('
    ///                  [ COMPUTER ]
    ///
    ///
    ///                    North (0,-1)
    ///                        ^
    ///                        |
    ///                        |
    ///  (-1,0)  West &lt;----[ HAPTIC ]----&gt; East (1,0)
    ///                        |
    ///                        |
    ///                        v
    ///                     South (0,1)
    ///
    ///
    ///                     [ USER ]
    ///                       \|||/
    ///                       (o o)
    ///                 ---ooO-(_)-Ooo---
    /// </code>
    /// <para>If type is <see cref="HAPTIC_POLAR"/>, direction is encoded by hundredths of a degree
    /// starting north and turning clockwise. <see cref="HAPTIC_POLAR"/> only uses the first
    /// <c>dir</c> parameter. The cardinal directions would be:</para>
    /// <list type="bullet">
    /// <item>North: 0 (0 degrees)</item>
    /// <item>East: 9000 (90 degrees)</item>
    /// <item>South: 18000 (180 degrees)</item>
    /// <item>West: 27000 (270 degrees)</item>
    /// </list>
    /// <para>If type is <see cref="HAPTIC_CARTESIAN"/>, direction is encoded by three positions (X
    /// axis, Y axis and Z axis (with 3 axes)). <see cref="HAPTIC_CARTESIAN"/> uses the first
    /// three <c>dir`</c> parameters. The cardinal directions would be:</para>
    /// <list type="bullet">
    /// <item>North: 0,-1, 0</item>
    /// <item>East: 1, 0, 0</item>
    /// <item>South: 0, 1, 0</item>
    /// <item>West: -1, 0, 0</item>
    /// </list>
    /// <para>The Z axis represents the height of the effect if supported, otherwise it's
    /// unused. In cartesian encoding (1, 2) would be the same as (2, 4), you can
    /// use any multiple you want, only the direction matters.</para>
    /// <para>If type is <see cref="HAPTIC_SPHERICAL"/>, direction is encoded by two rotations. The
    /// first two <c>dir</c> parameters are used. The `dir` parameters are as follows
    /// (all values are in hundredths of degrees):</para>
    /// <list type="bullet">
    /// <item>Degrees from (1, 0) rotated towards (0, 1).</item>
    /// <item>Degrees towards (0, 0, 1) (device needs at least 3 axes).</item>
    /// </list>
    /// <para>Example of force coming from the south with all encodings (force coming
    /// from the south means the user will have to pull the stick to counteract):</para>
    /// <code>
    /// SDL_HapticDirection direction;
    /// 
    /// // Cartesian directions
    /// direction.type = SDL_HAPTIC_CARTESIAN; // Using cartesian direction encoding.
    /// direction.dir[0] = 0; // X position
    /// direction.dir[1] = 1; // Y position
    /// // Assuming the device has 2 axes, we don't need to specify third parameter.
    ///
    /// // Polar directions
    /// direction.type = SDL_HAPTIC_POLAR; // We'll be using polar direction encoding.
    /// direction.dir[0] = 18000; // Polar only uses first parameter
    ///
    /// // Spherical coordinates
    /// direction.type = SDL_HAPTIC_SPHERICAL; // Spherical encoding
    /// direction.dir[0] = 9000; // Since we only have two axes we don't need more parameters.
    /// </code>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="HAPTIC_POLAR"/>
    /// <seealso cref="HAPTIC_CARTESIAN"/>
    /// <seealso cref="HAPTIC_SPHERICAL"/>
    /// <seealso cref="HAPTIC_STEERING_AXIS"/>
    /// <seealso cref="HapticEffect"/>
    /// <seealso cref="GetNumHapticAxes"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct HapticDirection
    {
        /// <summary>
        /// The type of encoding.
        /// </summary>
        public byte Type;

        /// <summary>
        /// The encoded direction.
        /// </summary>
        public unsafe fixed int Dir[3];
    }
}