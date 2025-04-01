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

public static partial class TTF
{
    /// <summary>
    /// <para>Hinting flags for TTF (TrueType Fonts)</para>
    /// <para>This enum specifies the level of hinting to be applied to the font
    /// rendering. The hinting level determines how much the font's outlines are
    /// adjusted for better alignment on the pixel grid.</para>
    /// </summary>
    /// <since>his enum is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontHinting"/>
    /// <seealso cref="GetFontHinting"/>
    public enum HintingFlags
    {
        Invalid = -1,
        
        /// <summary>
        /// Normal hinting applies standard grid-fitting.
        /// </summary>
        Normal = 0,
        
        /// <summary>
        /// Light hinting applies subtle adjustments to improve rendering.
        /// </summary>
        Light,
        
        /// <summary>
        /// Monochrome hinting adjusts the font for better rendering at lower resolutions.
        /// </summary>
        Mono,
        
        /// <summary>
        /// No hinting, the font is rendered without any grid-fitting.
        /// </summary>
        None,
        
        /// <summary>
        /// Light hinting with subpixel rendering for more precise font edges.
        /// </summary>
        LightSubpixel,
    }
}