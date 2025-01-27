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
    /// Colorspace chroma sample location.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum ChromaLocation
    {
        /// <summary>
        /// RGB, no chroma sampling
        /// </summary>
        None = 0,
        
        /// <summary>
        /// In MPEG-2, MPEG-4, and AVC, Cb and Cr are taken on midpoint of the left-edge of the 2x2 square.
        /// In other words, they have the same horizontal location as the top-left pixel, but is shifted
        /// one-half pixel down vertically.
        /// </summary>
        Left = 1,
        
        /// <summary>
        /// In JPEG/JFIF, H.261, and MPEG-1, Cb and Cr are taken at the center of the 2x2 square.
        /// In other words, they are offset one-half pixel to the right and one-half pixel down compared to
        /// the top-left pixel.
        /// </summary>
        Center = 2,
        
        /// <summary>
        /// In HEVC for BT.2020 and BT.2100 content (in particular on Blu-rays), Cb and Cr are sampled at the
        /// same location as the group's top-left Y pixel ("co-sited", "co-located").
        /// </summary>
        TopLeft = 3        
    }
}