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
    /// A structure specifying the parameters of a sampler.
    /// <para>Note that mip_lod_bias is a no-op for the Metal driver. For Metal, LOD bias
    /// must be applied via shader instead.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUSampler"/>
    /// <seealso cref="GPUFilter"/>
    /// <seealso cref="GPUSamplerMipmapMode"/>
    /// <seealso cref="GPUSamplerAddressMode"/>
    /// <seealso cref="GPUCompareOp"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUSamplerCreateInfo
    {
        /// <summary>
        /// The minification filter to apply to lookups.
        /// </summary>
        public GPUFilter MinFilter;
        
        /// <summary>
        /// The magnification filter to apply to lookups.
        /// </summary>
        public GPUFilter MagFilter;
        
        /// <summary>
        /// The mipmap filter to apply to lookups.
        /// </summary>
        public GPUSamplerMipmapMode MipmapMode;
        
        /// <summary>
        /// The addressing mode for U coordinates outside [0, 1).
        /// </summary>
        public GPUSamplerAddressMode AddressModeU;
        
        /// <summary>
        /// The addressing mode for V coordinates outside [0, 1).
        /// </summary>
        public GPUSamplerAddressMode AddressModeV;
        
        /// <summary>
        /// The addressing mode for W coordinates outside [0, 1).
        /// </summary>
        public GPUSamplerAddressMode AddressModeW;
        
        /// <summary>
        /// The bias to be added to mipmap LOD calculation.
        /// </summary>
        public float MipLodBias;
        
        /// <summary>
        /// The anisotropy value clamp used by the sampler. If enable_anisotropy is false, this is ignored.
        /// </summary>
        public float MaxAnisotropy;
        
        /// <summary>
        /// The comparison operator to apply to fetched data before filtering.
        /// </summary>
        public GPUCompareOp CompareOp;
        
        /// <summary>
        /// Clamps the minimum of the computed LOD value.
        /// </summary>
        public float MinLod;
        
        /// <summary>
        /// Clamps the maximum of the computed LOD value.
        /// </summary>
        public float MaxLod;
        
        /// <summary>
        /// true to enable anisotropic filtering.
        /// </summary>
        public Byte EnableAnisotropy;
        
        /// <summary>
        /// true to enable comparison against a reference value during lookups.
        /// </summary>
        public Byte EnableCompare;
        
        private byte padding1;
        
        private byte padding2;
    }
}