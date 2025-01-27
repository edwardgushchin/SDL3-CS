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
    /// <para>Colorspace color primaries, as described by</para>
    /// <para>https://www.itu.int/rec/T-REC-H.273-201612-S/en</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum ColorPrimaries
    {
        Unknown = 0,
        
        /// <summary>
        /// ITU-R BT.709-6
        /// </summary>
        BT709 = 1,
        
        Unspecified = 2,
        
        /// <summary>
        /// ITU-R BT.470-6 System M
        /// </summary>
        BT470M = 4,
        
        /// <summary>
        /// ITU-R BT.470-6 System B, G / ITU-R BT.601-7 625
        /// </summary>
        BT470BG = 5,
        
        /// <summary>
        /// ITU-R BT.601-7 525, SMPTE 170M
        /// </summary>
        BT601 = 6,
        
        /// <summary>
        /// SMPTE 240M, functionally the same as <see cref="ColorPrimaries.BT601"/>
        /// </summary>
        SMPTE240 = 7,
        
        /// <summary>
        /// Generic film (color filters using Illuminant C)
        /// </summary>
        GenericFilm = 8,
        
        /// <summary>
        /// ITU-R BT.2020-2 / ITU-R BT.2100-0
        /// </summary>
        BT2020 = 9,
        
        /// <summary>
        /// SMPTE ST 428-1
        /// </summary>
        XYZ = 10,
        
        /// <summary>
        /// SMPTE RP 431-2
        /// </summary>
        SMPTE431 = 11,
        
        /// <summary>
        /// SMPTE EG 432-1 / DCI P3
        /// </summary>
        SMPTE432 = 12,
        
        /// <summary>
        /// EBU Tech. 3213-E
        /// </summary>
        EBU3213 = 22,
        
        Custom = 31
    }
}