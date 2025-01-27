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
    /// Specifies the primitive topology of a graphics pipeline.
    /// <para>If you are using POINTLIST you must include a point size output in the
    /// vertex shader.</para>
    /// <list type="bullet">
    /// <item>For HLSL compiling to SPIRV you must decorate a float output
    /// with [[vk::builtin("PointSize")]].</item>
    /// <item>For GLSL you must set the gl_PointSize
    /// builtin. For MSL you must include a float output with the [[point_size]]
    /// decorator.</item>
    /// </list>
    /// <para>Note that sized point topology is totally unsupported on D3D12.
    /// Any size other than 1 will be ignored. In general, you should avoid using
    /// point topology for both compatibility and performance reasons. You WILL
    /// regret using it.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUGraphicsPipeline"/>
    public enum GPUPrimitiveType
    {
        /// <summary>
        /// A series of separate triangles.
        /// </summary>
        TriangleList,
        
        /// <summary>
        /// A series of connected triangles.
        /// </summary>
        TriangleStrip,
        
        /// <summary>
        /// A series of separate lines.
        /// </summary>
        LineList,
        
        /// <summary>
        /// A series of connected lines.
        /// </summary>
        LineStrip,
        
        /// <summary>
        /// A series of separate points.
        /// </summary>
        PointList
    }
}