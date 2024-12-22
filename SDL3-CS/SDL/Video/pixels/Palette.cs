﻿#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

public static partial class SDL
{
    /// <summary>
    /// A set of indexed colors representing a palette.
    /// </summary>
    /// <since>This struct is available since SDL 3.1.3.</since>
    /// <seealso cref="SDL.SetPaletteColors"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct Palette
    {
        /// <summary>
        /// number of elements in <c>colors</c>.
        /// </summary>
        public int NColors;
        
        /// <summary>
        /// an array of colors, <c>ncolors</c> long.
        /// </summary>
        public Color[] Colors;
        
        /// <summary>
        /// internal use only, do not touch.
        /// </summary>
        public uint Version;
        
        /// <summary>
        /// internal use only, do not touch.
        /// </summary>
        public int Refcount;
    }
}
