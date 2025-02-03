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
    /// Direction flags
    /// <para>The values here are chosen to match
    /// [hb_direction_t](https://harfbuzz.github.io/harfbuzz-hb-common.html#hb-direction-t)</para>
    /// </summary>
    /// <since>This enum is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontDirection"/>
    public enum Direction
    {
        Invalid = 0,
        
        /// <summary>
        /// Left to Right
        /// </summary>
        LTR = 4,
        
        /// <summary>
        /// Right to Left
        /// </summary>
        RTL,
        
        /// <summary>
        /// Top to Bottom
        /// </summary>
        TTB,
        
        /// <summary>
        /// Bottom to Top
        /// </summary>
        BTT
    }
}