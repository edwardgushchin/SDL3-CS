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
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Pen axis indices
    /// </summary>
    /// <para>Below are the valid indices to the "axis" array from <see cref="PenMotionEvent"/>
    /// and <see cref="PenButtonEvent"/>. The axis indices form a contiguous range of ints from <c>0</c> to
    /// <see cref="PenAxis.Last"/>, inclusive. All "axis[]" entries are either normalised to <c>0..1</c> or
    /// report a (positive or negative) angle in degrees, with <c>0.0</c> representing the centre.
    /// Not all pens/backends support all axes: unsupported entries are always <c>0.0f</c>.</para>
    /// <para>To convert angles for tilt and rotation into vector representation, use
    /// <see cref="MathF.Sin"/> on the XTILT, YTILT, or ROTATION component, for example:</para>
    /// <para><c>MathF.Sin(xtilt * MathF.PI / 180.0)</c>.</para>
    /// <since>This enum is available since SDL 3.0.0</since>
    public enum PenAxis
    {
        /// <summary>
        /// Pen pressure.  Unidirectional: 0..1.0
        /// </summary>
        Pressure = 0,
        
        /// <summary>
        /// Pen horizontal tilt angle.  Bidirectional: -90.0..90.0 (left-to-right)
        /// The physical max/min tilt may be smaller than -90.0 / 90.0, cf. <see cref="PenCapabilityInfo"/>
        /// </summary>
        XTilt,  
        
        /// <summary>
        /// Pen vertical tilt angle.  Bidirectional: -90.0..90.0 (top-to-down).
        /// The physical max/min tilt may be smaller than -90.0 / 90.0, cf. <see cref="PenCapabilityInfo"/>
        /// </summary>
        YTilt,
        
        /// <summary>
        /// Pen distance to drawing surface.  Unidirectional: 0.0..1.0
        /// </summary>
        Distance,
        
        /// <summary>
        /// Pen barrel rotation.  Bidirectional: -180..179.9 (clockwise, 0 is facing up, -180.0 is facing down).
        /// </summary>
        Rotation,
        
        /// <summary>
        /// Pen finger wheel or slider (e.g., Airbrush Pen).  Unidirectional: 0..1.0
        /// </summary>
        Slider,
        
        /// <summary>
        ///  Last valid axis index
        /// </summary>
        NumAxes,
        
        /// <summary>
        /// Last axis index plus 1
        /// </summary>
        Last = NumAxes - 1
    }
}