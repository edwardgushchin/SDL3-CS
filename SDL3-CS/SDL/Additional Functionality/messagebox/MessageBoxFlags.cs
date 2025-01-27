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
    /// <para>Message box flags.</para>
    /// <para>If supported will display warning icon, etc.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [Flags]
    public enum MessageBoxFlags : uint
    {
        /// <summary>
        /// error dialog
        /// </summary>
        Error = 0x00000010u,
        
        /// <summary>
        /// warning dialog
        /// </summary>
        Warning = 0x00000020u,
        
        /// <summary>
        /// informational dialog
        /// </summary>
        Information = 0x00000040u,
        
        /// <summary>
        /// buttons placed left to right
        /// </summary>
        ButtonsLeftToRight = 0x00000080u,
        
        /// <summary>
        /// buttons placed right to left
        /// </summary>
        ButtonsRightToLeft = 0x00000100u
    }
}