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
    /// <para>A structure specifying the parameters of vertex buffers used in a graphics
    /// pipeline.</para>
    /// <para>When you call <see cref="BindGPUVertexBuffers(nint, uint, GPUBufferBinding[], uint)"/>, you specify the binding slots of
    /// the vertex buffers. For example if you called <see cref="BindGPUVertexBuffers(nint, uint, GPUBufferBinding[], uint)"/> with
    /// a first_slot of 2 and num_bindings of 3, the binding slots 2, 3, 4 would be
    /// used by the vertex buffers you pass in.</para>
    /// <para>Vertex attributes are linked to buffers via the buffer_slot field of
    /// <see cref="GPUVertexAttribute"/>. For example, if an attribute has a buffer_slot of
    /// 0, then that attribute belongs to the vertex buffer bound at slot 0.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GPUVertexAttribute"/>
    /// <seealso cref="GPUVertexInputRate"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUVertexBufferDescription
    {
        /// <summary>
        /// The binding slot of the vertex buffer.
        /// </summary>
        public UInt32 Slot;
        
        /// <summary>
        /// The size of a single element + the offset between elements.
        /// </summary>
        public UInt32 Pitch;
        
        /// <summary>
        /// Whether attribute addressing is a function of the vertex index or instance index.
        /// </summary>
        public GPUVertexInputRate InputRate;
        
        /// <summary>
        /// Reserved for future use. Must be set to 0.
        /// </summary>
        public UInt32 InstanceStepRate;
    }
}