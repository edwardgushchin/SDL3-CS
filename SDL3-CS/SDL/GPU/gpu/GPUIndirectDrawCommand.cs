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
    /// <para>A structure specifying the parameters of an indirect draw command.</para>
    /// <para>Note that the <c>FirstVertex</c> and <c>FirstInstance</c> parameters are NOT
    /// compatible with built-in vertex/instance ID variables in shaders (for
    /// example, SV_VertexID); GPU APIs and shader languages do not define these
    /// built-in variables consistently, so if your shader depends on them, the 
    /// only way to keep behavior consistent and portable is to always pass 0 for 
    /// the correlating parameter in the draw calls.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="DrawGPUPrimitivesIndirect"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUIndirectDrawCommand
    {
        /// <summary>
        /// The number of vertices to draw.
        /// </summary>
        public UInt32 NumVertices;
        
        /// <summary>
        /// The number of instances to draw.
        /// </summary>
        public UInt32 NumInstances;
        
        /// <summary>
        /// The index of the first vertex to draw.
        /// </summary>
        public UInt32 FirstVertex;
        
        /// <summary>
        /// The ID of the first instance to draw.
        /// </summary>
        public UInt32 FirstInstance;
    }
}