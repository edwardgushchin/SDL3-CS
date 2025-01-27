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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>Colorspace matrix coefficients.</para>
    /// <para>These are as described by https://www.itu.int/rec/T-REC-H.273-201612-S/en</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum MatrixCoefficients
    {
        Identity = 0,
        
        /// <summary>
        /// ITU-R BT.709-6
        /// </summary>
        BT709 = 1,
        
        Unspecified = 2,
        
        /// <summary>
        /// US FCC Title 47
        /// </summary>
        FCC = 4,
        
        /// <summary>
        /// ITU-R BT.470-6 System B, G / ITU-R BT.601-7 625, functionally the same as <see cref="BT601"/>
        /// </summary>
        BT470BG = 5,
        
        /// <summary>
        /// ITU-R BT.601-7 525
        /// </summary>
        BT601 = 6,
        
        /// <summary>
        /// SMPTE 240M
        /// </summary>
        SMPTE240 = 7,
        
        YCGCO = 8,
        
        /// <summary>
        /// ITU-R BT.2020-2 non-constant luminance
        /// </summary>
        BT2020NCL = 9,
        
        /// <summary>
        /// ITU-R BT.2020-2 constant luminance
        /// </summary>
        BT2020CL = 10,
        
        /// <summary>
        /// SMPTE ST 2085
        /// </summary>
        SMPTE2085 = 11,
        
        ChromaDerivedNCL = 12,
        
        ChromaDerivedCL = 13,
        
        /// <summary>
        /// ITU-R BT.2100-0 ICTCP
        /// </summary>
        ICTCP = 14,
        
        Custom = 31
    }
}