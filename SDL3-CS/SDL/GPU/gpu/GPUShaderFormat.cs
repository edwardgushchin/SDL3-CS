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
    /// <para>Specifies the format of shader code.</para>
    /// <para>Each format corresponds to a specific backend that accepts it.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    public enum GPUShaderFormat : uint
    {
        Invalid = 0,
        
        /// <summary>
        /// Shaders for NDA'd platforms.
        /// </summary>
        Private = 1u << 0,
        
        /// <summary>
        /// SPIR-V shaders for Vulkan.
        /// </summary>
        SPIRV = 1u << 1,
        
        /// <summary>
        /// DXBC SM5_1 shaders for D3D12.
        /// </summary>
        DXBC = 1u << 2,
        
        /// <summary>
        /// DXIL SM6_0 shaders for D3D12.
        /// </summary>
        DXIL = 1u << 3,
        
        /// <summary>
        /// MSL shaders for Metal.
        /// </summary>
        MSL = 1u << 4,
        
        /// <summary>
        /// Precompiled metallib shaders for Metal.
        /// </summary>
        MetalLib = 1u << 5
    }
}