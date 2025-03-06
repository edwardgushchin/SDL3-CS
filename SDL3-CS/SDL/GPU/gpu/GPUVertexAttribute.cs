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
    /// <para>A structure specifying a vertex attribute.</para>
    /// <para>All vertex attribute locations provided to an <see cref="GPUVertexInputState"/> must
    /// be unique.</para>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// </summary>
    /// <seealso cref="GPUVertexBufferDescription"/>
    /// <seealso cref="GPUVertexInputState"/>
    /// <seealso cref="GPUVertexElementFormat"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUVertexAttribute
    {
        /// <summary>
        /// The shader input location index.
        /// </summary>
        public UInt32 Location;
        
        /// <summary>
        /// The binding slot of the associated vertex buffer.
        /// </summary>
        public UInt32 BufferSlot;
        
        /// <summary>
        /// The size and type of the attribute data.
        /// </summary>
        public GPUVertexElementFormat Format;
        
        /// <summary>
        /// The byte offset of this attribute relative to the start of the vertex element.
        /// </summary>
        public UInt32 Offset;
    }
}