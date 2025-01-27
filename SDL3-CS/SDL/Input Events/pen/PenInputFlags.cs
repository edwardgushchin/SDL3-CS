#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024-2025 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Pen input flags, as reported by various pen events' <c>pen_state</c> field.
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [Flags]
    public enum PenInputFlags : uint
    {
        /// <summary>
        /// pen is pressed down
        /// </summary>
        Down = 1u << 0,
        
        /// <summary>
        /// button 1 is pressed
        /// </summary>
        Button1 = 1u << 1,
        
        /// <summary>
        /// button 2 is pressed
        /// </summary>
        Button2 = 1u << 2,
        
        /// <summary>
        /// button 3 is pressed
        /// </summary>
        Button3 = 1u << 3,
        
        /// <summary>
        /// button 4 is pressed
        /// </summary>
        Button4 = 1u << 4,
        
        /// <summary>
        /// button 5 is pressed
        /// </summary>
        Button5 = 1u << 5,
        
        /// <summary>
        /// eraser tip is used
        /// </summary>
        EraserTip = 1u << 30
    }
}