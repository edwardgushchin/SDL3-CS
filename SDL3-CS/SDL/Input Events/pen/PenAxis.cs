#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024-2025 Eduard Gushchin.
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
    /// <para>Pen axis indices.</para>
    /// <para>These are the valid values for the <c>axis</c> field in <see cref="PenAxisEvent"/>. All
    /// axes are either normalised to 0..1 or report a (positive or negative) angle
    /// in degrees, with 0.0 representing the centre. Not all pens/backends support
    /// all axes: unsupported axes are always zero.</para>
    /// <para>To convert angles for tilt and rotation into vector representation, use
    /// SDL_sinf on the XTILT, YTILT, or ROTATION component, for example:</para>
    /// <para><c>SDL_sinf(xtilt * SDL_PI_F / 180.0)</c></para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum PenAxis
    {
        /// <summary>
        /// Pen pressure.  Unidirectional: 0 to 1.0
        /// </summary>
        Pressure,
        
        /// <summary>
        /// Pen horizontal tilt angle.  Bidirectional: -90.0 to 90.0 (left-to-right).
        /// </summary>
        XTilt,  
        
        /// <summary>
        /// Pen vertical tilt angle.  Bidirectional: -90.0 to 90.0 (top-to-down).
        /// </summary>
        YTilt,
        
        /// <summary>
        /// Pen distance to drawing surface.  Unidirectional: 0.0 to 1.0
        /// </summary>
        Distance,
        
        /// <summary>
        /// Pen barrel rotation.  Bidirectional: -180 to 179.9 (clockwise, 0 is facing up, -180.0 is facing down).
        /// </summary>
        Rotation,
        
        /// <summary>
        /// Pen finger wheel or slider (e.g., Airbrush Pen).  Unidirectional: 0 to 1.0
        /// </summary>
        Slider,
        
        /// <summary>
        /// Pressure from squeezing the pen ("barrel pressure").
        /// </summary>
        TangetialPressure,
        
        /// <summary>
        /// Total known pen axis types in this version of SDL. This number may grow in future releases!
        /// </summary>
        Count
    }
}