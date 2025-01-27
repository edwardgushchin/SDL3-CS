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

public static partial class SDL
{
    /// <summary>
    /// <para>A structure specifying a region of a texture used in the blit operation.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="BlitGPUTexture"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUBlitRegion
    {
        /// <summary>
        /// The texture.
        /// </summary>
        public IntPtr Texture;
        
        /// <summary>
        /// The mip level index of the region.
        /// </summary>
        public UInt32 MipLevel;
        
        /// <summary>
        /// The layer index or depth plane of the region. This value is treated as a layer index on 2D array and cube textures, and as a depth plane on 3D textures.
        /// </summary>
        public UInt32 LayerOrDepthPlane;
        
        /// <summary>
        /// The left offset of the region.
        /// </summary>
        public UInt32 X;
        
        /// <summary>
        /// The top offset of the region.
        /// </summary>
        public UInt32 Y;
        
        /// <summary>
        /// The width of the region.
        /// </summary>
        public UInt32 W;
        
        /// <summary>
        /// The height of the region.
        /// </summary>
        public UInt32 H;
    }
}