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
    /// Possible return values from the SDL_HitTest callback.
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="HitTest"/>
    public enum HitTestResult
    {
        /// <summary>
        /// Region is normal. No special properties.
        /// </summary>
        Normal,
        
        /// <summary>
        /// Region can drag entire window.
        /// </summary>
        Draggable,
        
        /// <summary>
        /// Region is the resizable top-left corner border.
        /// </summary>
        ResizeTopLeft,
        
        /// <summary>
        /// Region is the resizable top border.
        /// </summary>
        ResizeTop,
        
        /// <summary>
        /// Region is the resizable top-right corner border.
        /// </summary>
        ResizeTopRight,
        
        /// <summary>
        /// Region is the resizable right border.
        /// </summary>
        ResizeRight,
        
        /// <summary>
        /// Region is the resizable bottom-right corner border.
        /// </summary>
        ResizeBottomRight,
        
        /// <summary>
        /// Region is the resizable bottom border.
        /// </summary>
        ResizeBottom,
        
        /// <summary>
        /// Region is the resizable bottom-left corner border.
        /// </summary>
        ResizeBottomLeft,
        
        /// <summary>
        /// Region is the resizable left border.
        /// </summary>
        ResizeLeft 
    }
}