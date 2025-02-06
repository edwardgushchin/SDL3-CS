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
    /// <para>Colorspace transfer characteristics.</para>
    /// <para>These are as described by https://www.itu.int/rec/T-REC-H.273-201612-S/en</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum TransferCharacteristics
    {
        Unknown = 0,
        
        /// <summary>
        /// Rec. ITU-R BT.709-6 / ITU-R BT1361
        /// </summary>
        BT709 = 1,
        
        Unspecified = 2,
        
        /// <summary>
        /// ITU-R BT.470-6 System M / ITU-R BT1700 625 PAL &amp; SECAM
        /// </summary>
        Gamma22 = 4,
        
        /// <summary>
        /// ITU-R BT.470-6 System B, G
        /// </summary>
        Gamma28 = 5,
        
        /// <summary>
        /// SMPTE ST 170M / ITU-R BT.601-7 525 or 625
        /// </summary>
        BT601 = 6,
        
        /// <summary>
        /// SMPTE ST 240M
        /// </summary>
        SMPTE240 = 7,
        
        Linear = 8,
        
        Log100 = 9,
        
        Log100Sqrt10 = 10,
        
        /// <summary>
        /// IEC 61966-2-4
        /// </summary>
        IEC61966 = 11,
        
        /// <summary>
        /// ITU-R BT1361 Extended Colour Gamut
        /// </summary>
        BT1361 = 12,
        
        /// <summary>
        /// IEC 61966-2-1 (sRGB or sYCC)
        /// </summary>
        SRGB = 13,
        
        /// <summary>
        /// ITU-R BT2020 for 10-bit system
        /// </summary>
        BT202010Bit = 14,
        
        /// <summary>
        /// ITU-R BT2020 for 12-bit system
        /// </summary>
        BT202012Bit = 15,
        
        /// <summary>
        /// SMPTE ST 2084 for 10-, 12-, 14- and 16-bit systems
        /// </summary>
        PQ = 16,
        
        /// <summary>
        /// SMPTE ST 428-1
        /// </summary>
        SMPTE428 = 17,
        
        /// <summary>
        /// ARIB STD-B67, known as "hybrid log-gamma" (HLG)
        /// </summary>
        HLG = 18,
        
        Custom = 31
    }
}