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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class TTF
{
    /// <summary>
    /// The representation of a substring within text.
    /// </summary>
    /// <since>This struct is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetNextTextSubString"/>
    /// <seealso cref="GetPreviousTextSubString"/>
    /// <seealso cref="GetTextSubString"/>
    /// <seealso cref="GetTextSubStringForLine"/>
    /// <seealso cref="GetTextSubStringForPoint"/>
    /// <seealso cref="GetTextSubStringsForRange"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct SubString
    {
        /// <summary>
        /// The flags for this substring
        /// </summary>
        public SubStringFlags Flags;

        /// <summary>
        /// The byte offset from the beginning of the text
        /// </summary>
        public int Offset;
        
        /// <summary>
        /// The byte length starting at the offset
        /// </summary>
        public int Length;

        /// <summary>
        /// The index of the line that contains this substring
        /// </summary>
        public int LineIndex;

        /// <summary>
        /// The internal cluster index, used for quickly iterating
        /// </summary>
        public int ClusterIndex;

        /// <summary>
        /// The rectangle, relative to the top left of the text, containing the substring
        /// </summary>
        public SDL.Rect Rect;
    }
}