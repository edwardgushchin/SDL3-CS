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
    /// The addressing mode for a texture when used in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>.
    /// <para>This affects how texture coordinates are interpreted outside of [0, 1]</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.4.0.</since>
    public enum TextureAddressMode
    {
        Invalid = -1,
        
        /// <summary>
        /// Wrapping is enabled if texture coordinates are outside [0, 1], this is the default
        /// </summary>
        Auto,
        
        /// <summary>
        /// Texture coordinates are clamped to the [0, 1] range
        /// </summary>
        Clamp,
        
        /// <summary>
        /// The texture is repeated (tiled)
        /// </summary>
        Wrap,
    }
}
