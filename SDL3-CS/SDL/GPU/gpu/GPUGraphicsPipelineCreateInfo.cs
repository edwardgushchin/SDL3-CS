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
    /// A structure specifying the parameters of a graphics pipeline state.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUGraphicsPipeline"/>
    /// <seealso cref="GPUVertexInputState"/>
    /// <seealso cref="GPUPrimitiveType"/>
    /// <seealso cref="GPURasterizerState"/>
    /// <seealso cref="GPUMultisampleState"/>
    /// <seealso cref="GPUDepthStencilState"/>
    /// <seealso cref="GPUGraphicsPipelineTargetInfo"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUGraphicsPipelineCreateInfo
    {
        /// <summary>
        /// The vertex shader used by the graphics pipeline.
        /// </summary>
        public IntPtr VertexShader;
        
        /// <summary>
        /// The fragment shader used by the graphics pipeline.
        /// </summary>
        public IntPtr FragmentShader;
        
        /// <summary>
        /// The vertex layout of the graphics pipeline.
        /// </summary>
        public GPUVertexInputState VertexInputState;
        
        /// <summary>
        /// The primitive topology of the graphics pipeline.
        /// </summary>
        public GPUPrimitiveType PrimitiveType;
        
        /// <summary>
        /// The rasterizer state of the graphics pipeline.
        /// </summary>
        public GPURasterizerState RasterizerState;
        
        /// <summary>
        /// The multisample state of the graphics pipeline.
        /// </summary>
        public GPUMultisampleState MultisampleState;
        
        /// <summary>
        /// The depth-stencil state of the graphics pipeline.
        /// </summary>
        public GPUDepthStencilState DepthStencilState;
        
        /// <summary>
        /// Formats and blend modes for the render targets of the graphics pipeline.
        /// </summary>
        public GPUGraphicsPipelineTargetInfo TargetInfo;
        
        /// <summary>
        /// A properties ID for extensions. Should be 0 if no extensions are needed.
        /// </summary>
        public UInt32 Props;
    }
}