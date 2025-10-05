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
    /// A structure specifying the parameters of a GPU render state.
    /// </summary>
    /// <since>This struct is available since SDL 3.4.0.</since>
    /// <seealso cref="CreateGPURenderState"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPURenderStateCreateInfo
    {
        /// <summary>
        /// The fragment shader to use when this render state is active 
        /// </summary>
        public IntPtr FragmentShader;
    
        /// <summary>
        /// The number of additional fragment samplers to bind when this render state is active 
        /// </summary>
        public int NumSamplerBindings;
        
        /// <summary>
        /// Additional fragment samplers to bind when this render state is active
        /// </summary>
        public IntPtr SamplerBindings;
    
        /// <summary>
        /// The number of storage textures to bind when this render state is active
        /// </summary>
        public int NumStorageTextures;
        
        /// <summary>
        /// The number of storage textures to bind when this render state is active
        /// </summary>
        public IntPtr StorageTextures;
    
        /// <summary>
        /// The number of storage buffers to bind when this render state is active
        /// </summary>
        public int NumStorageBuffers;
    
        /// <summary>
        /// Storage buffers to bind when this render state is active
        /// </summary>
        public IntPtr StorageBuffers;
        
        /// <summary>
        /// A properties ID for extensions. Should be 0 if no extensions are needed.
        /// </summary>
        public uint Props;
    }
}
