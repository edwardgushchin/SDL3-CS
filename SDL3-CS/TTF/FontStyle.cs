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
    /// <para>Font style flags for TTF_Font</para>
    /// <para>These are the flags which can be used to set the style of a font in
    /// SDL_ttf. A combination of these flags can be used with functions that set
    /// or query font style, such as <see cref="SetFontStyle"/> or <see cref="GetFontStyle"/>.</para>
    /// </summary>
    /// <since>This datatype is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontStyle"/>
    /// <seealso cref="GetFontStyle"/>
    [Flags]
    public enum FontStyleFlags
    {
        /// <summary>
        /// No special style
        /// </summary>
        Normal = 0x00,
        
        /// <summary>
        /// Bold style
        /// </summary>
        Bold = 0x01,
        
        /// <summary>
        /// Italic style
        /// </summary>
        Italic = 0x02,
        
        /// <summary>
        /// Underlined text
        /// </summary>
        Underline = 0x04,
        
        /// <summary>
        /// Strikethrough text
        /// </summary>
        Strikethrough = 0x08,
    }
}