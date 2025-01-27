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
    /// <para>A structure specifying the parameters of a texture.</para>
    /// <para>Usage flags can be bitwise OR'd together for combinations of usages. Note
    /// that certain usage combinations are invalid, for example <see cref="GPUTextureUsageFlags.Sampler"/> and
    /// GRAPHICS_STORAGE.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUTexture"/>
    /// <seealso cref="GPUTextureType"/>
    /// <seealso cref="GPUTextureFormat"/>
    /// <seealso cref="GPUTextureUsageFlags"/>
    /// <seealso cref="GPUSampleCount"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUTextureCreateInfo
    {
        /// <summary>
        /// The base dimensionality of the texture.
        /// </summary>
        public GPUTextureType Type;
        
        /// <summary>
        /// The pixel format of the texture.
        /// </summary>
        public GPUTextureFormat Format;
        
        /// <summary>
        /// How the texture is intended to be used by the client.
        /// </summary>
        public GPUTextureUsageFlags Usage;
        
        /// <summary>
        /// The width of the texture.
        /// </summary>
        public UInt32 Width;
        
        /// <summary>
        /// The height of the texture.
        /// </summary>
        public UInt32 Height;
        
        /// <summary>
        /// The layer count or depth of the texture. This value is treated as a layer count on 2D array textures, and as a depth value on 3D textures.
        /// </summary>
        public UInt32 LayerCountOrDepth;
        
        /// <summary>
        /// The number of mip levels in the texture.
        /// </summary>
        public UInt32 NumLevels;
        
        /// <summary>
        /// The number of samples per texel. Only applies if the texture is used as a render target.
        /// </summary>
        public GPUSampleCount SampleCount;

        /// <summary>
        /// A properties ID for extensions. Should be 0 if no extensions are needed.
        /// </summary>
        public UInt32 Props;
    }
}