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
    /// <para>A structure specifying a region of a texture.</para>
    /// <para>Used when transferring data to or from a texture.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="UploadToGPUTexture"/>
    /// <seealso cref="DownloadFromGPUTexture"/>
    /// <seealso cref="CreateGPUTexture"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUTextureRegion
    {
        /// <summary>
        /// The texture used in the copy operation.
        /// </summary>
        public IntPtr Texture;
        
        /// <summary>
        /// The mip level index to transfer.
        /// </summary>
        public UInt32 MipLevel;
        
        /// <summary>
        /// The layer index to transfer.
        /// </summary>
        public UInt32 Layer;
        
        /// <summary>
        /// The left offset of the region.
        /// </summary>
        public UInt32 X;
        
        /// <summary>
        /// The top offset of the region.
        /// </summary>
        public UInt32 Y;
        
        /// <summary>
        /// The front offset of the region.
        /// </summary>
        public UInt32 Z;
        
        /// <summary>
        /// The width of the region.
        /// </summary>
        public UInt32 W;
        
        /// <summary>
        /// The height of the region.
        /// </summary>
        public UInt32 H;
        
        /// <summary>
        /// The depth of the region.
        /// </summary>
        public UInt32 D;
    } 
}