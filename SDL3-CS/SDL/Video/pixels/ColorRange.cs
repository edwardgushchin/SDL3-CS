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
    /// <para>Colorspace color range, as described by</para>
    /// <para>https://www.itu.int/rec/R-REC-BT.2100-2-201807-I/en</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum ColorRange
    {
        Unknown = 0,
        
        /// <summary>
        /// Narrow range, e.g. 16-235 for 8-bit RGB and luma, and 16-240 for 8-bit chroma
        /// </summary>
        Limited = 1,
        
        /// <summary>
        /// Full range, e.g. 0-255 for 8-bit RGB and luma, and 1-255 for 8-bit chroma
        /// </summary>
        Full = 2
    }
}