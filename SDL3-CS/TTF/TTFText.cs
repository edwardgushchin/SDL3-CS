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
    /// Text created with <see cref="CreateText"/>
    /// </summary>
    /// <since>This struct is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateText"/>
    /// <seealso cref="GetTextProperties"/>
    /// <seealso cref="DestroyText"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct TTFText
    {
        /// <summary>
        /// A copy of the UTF-8 string that this text object represents, useful for layout, debugging and retrieving substring text. This is updated when the text object is modified and will be freed automatically when the object is destroyed.
        /// </summary>
        public IntPtr Text;

        /// <summary>
        /// The number of lines in the text, 0 if it's empty
        /// </summary>
        public int NumLines;

        /// <summary>
        /// Application reference count, used when freeing surface
        /// </summary>
        public int Refcount;
        
        /// <summary>
        /// Private
        /// </summary>
        private IntPtr _internal;
    }
}