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
    /// How the logical size is mapped to the output.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum RendererLogicalPresentation
    {
        /// <summary>
        /// There is no logical size in effect
        /// </summary>
        Disabled,
        
        /// <summary>
        /// The rendered content is stretched to the output resolution
        /// </summary>
        Stretch,
        
        /// <summary>
        /// The rendered content is fit to the largest dimension and the other dimension is letterboxed with the clear color
        /// </summary>
        Letterbox,
        
        /// <summary>
        /// The rendered content is fit to the smallest dimension and the other dimension extends beyond the output bounds
        /// </summary>
        Overscan,
        
        /// <summary>
        /// The rendered content is scaled up by integer multiples to fit the output resolution
        /// </summary>
        IntegerScale
    }
}