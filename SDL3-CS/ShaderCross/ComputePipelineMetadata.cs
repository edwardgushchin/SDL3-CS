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

public partial class ShaderCross
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ComputePipelineMetadata
    {
        /// <summary>
        /// The number of samplers defined in the shader.
        /// </summary>
        public uint NumSamplers;
        
        /// <summary>
        /// The number of readonly storage textures defined in the shader. 
        /// </summary>
        public uint NumReadOnlyStorageTextures;
        
        /// <summary>
        /// The number of readonly storage buffers defined in the shader.
        /// </summary>
        public uint NumReadOnlyStorageBuffers;
        
        /// <summary>
        /// The number of read-write storage textures defined in the shader.
        /// </summary>
        public uint NumReadWriteStorageTextures;
        
        /// <summary>
        /// The number of read-write storage buffers defined in the shader.
        /// </summary>
        public uint NumReadwriteStorageBuffers;
        
        /// <summary>
        /// The number of uniform buffers defined in the shader.
        /// </summary>
        public uint NumUniformBuffers;
        
        /// <summary>
        /// The number of threads in the X dimension.
        /// </summary>
        public uint ThreadCountX;
        
        /// <summary>
        /// The number of threads in the Y dimension.
        /// </summary>
        public uint ThreadCountY;
        
        /// <summary>
        /// The number of threads in the Z dimension.
        /// </summary>
        public uint ThreadCountZ;

        /// <summary>
        /// A properties ID for extensions. This is allocated and freed by the caller, and should be 0 if no extensions are needed.
        /// </summary>
        public uint Props;
    }
}