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
    /// <para>Specifies how a buffer is intended to be used by the client.</para>
    /// <para>A buffer must have at least one usage flag. Note that some usage flag
    /// combinations are invalid.</para>
    /// <para>Unlike textures, READ | WRITE can be used for simultaneous read-write
    /// usage. The same data synchronization concerns as textures apply.</para>
    /// <para>If you use a STORAGE flag, the data in the buffer must respect std140
    /// layout conventions. In practical terms this means you must ensure that vec3
    /// and vec4 fields are 16-byte aligned.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUBuffer"/>
    [Flags]
    public enum GPUBufferUsageFlags : uint
    {
        /// <summary>
        /// Buffer is a vertex buffer.
        /// </summary>
        Vertex = 1u << 0,
        
        /// <summary>
        /// Buffer is an index buffer.
        /// </summary>
        Index = 1u << 1,
        
        /// <summary>
        /// Buffer is an indirect buffer.
        /// </summary>
        Indirect = 1u << 2,
        
        /// <summary>
        /// Buffer supports storage reads in graphics stages.
        /// </summary>
        GraphicsStorageRead = 1u << 3,
        
        /// <summary>
        /// Buffer supports storage reads in the compute stage.
        /// </summary>
        ComputeStorageRead = 1u << 4,
        
        /// <summary>
        /// Buffer supports storage writes in the compute stage.
        /// </summary>
        ComputeStorageWrite = 1u << 5
    }
}