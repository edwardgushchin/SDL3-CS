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
    /// The blend operation used when combining source and destination pixel
    /// components.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum BlendOperation
    {
        /// <summary>
        /// dst + src: supported by all renderers
        /// </summary>
        Add = 0x1,
        
        /// <summary>
        /// src - dst : supported by D3D, OpenGL, OpenGLES, and Vulkan
        /// </summary>
        Subtract = 0x2,
        
        /// <summary>
        /// dst - src : supported by D3D, OpenGL, OpenGLES, and Vulkan
        /// </summary>
        RevSubtract = 0x3,
        
        /// <summary>
        /// min(dst, src) : supported by D3D, OpenGL, OpenGLES, and Vulkan
        /// </summary>
        Minimum = 0x4,
        
        /// <summary>
        /// max(dst, src) : supported by D3D, OpenGL, OpenGLES, and Vulkan
        /// </summary>
        Maximum = 0x5
    }
}