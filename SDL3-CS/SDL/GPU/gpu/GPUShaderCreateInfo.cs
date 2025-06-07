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
    /// <para>A structure specifying code and metadata for creating a shader object.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    /// <seealso cref="GPUShaderFormat"/>
    /// <seealso cref="GPUShaderStage"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUShaderCreateInfo
    {
        /// <summary>
        /// The size in bytes of the code pointed to.
        /// </summary>
        public UIntPtr CodeSize;
        
        /// <summary>
        /// A pointer to shader code.
        /// </summary>
        public IntPtr Code;
        
        /// <summary>
        /// A pointer to a null-terminated UTF-8 string specifying the entry point function name for the shader.
        /// </summary>
        public IntPtr Entrypoint;
        
        /// <summary>
        /// The format of the shader code.
        /// </summary>
        public GPUShaderFormat Format;
        
        /// <summary>
        /// The stage the shader program corresponds to.
        /// </summary>
        public GPUShaderStage Stage;
        
        /// <summary>
        /// The number of samplers defined in the shader.
        /// </summary>
        public UInt32 NumSamplers;
        
        /// <summary>
        /// The number of storage textures defined in the shader.
        /// </summary>
        public UInt32 NumStorageTextures;
        
        /// <summary>
        /// The number of storage buffers defined in the shader.
        /// </summary>
        public UInt32 NumStorageBuffers;
        
        /// <summary>
        /// The number of uniform buffers defined in the shader.
        /// </summary>
        public UInt32 NumUniformBuffers;
        
        /// <summary>
        /// A properties ID for extensions. Should be 0 if no extensions are needed.
        /// </summary>
        public UInt32 Props;
    }
}