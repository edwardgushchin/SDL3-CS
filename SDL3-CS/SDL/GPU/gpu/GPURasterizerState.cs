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
    /// <para>A structure specifying the parameters of the graphics pipeline rasterizer
    /// state.</para>
    /// <para>Note that <see cref="GPUFillMode.Line"/> is not supported on many Android devices.
    /// For those devices, the fill mode will automatically fall back to FILL.</para>
    /// <para>Also note that the D3D12 driver will enable depth clamping even if
    /// enable_depth_clip is true. If you need this clamp+clip behavior, consider
    /// enabling depth clip and then manually clamping depth in your fragment
    /// shaders on Metal and Vulkan.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GPUGraphicsPipelineCreateInfo"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPURasterizerState
    {
        /// <summary>
        /// Whether polygons will be filled in or drawn as lines.
        /// </summary>
        public GPUFillMode FillMode;
        
        /// <summary>
        /// The facing direction in which triangles will be culled.
        /// </summary>
        public GPUCullMode CullMode;
        
        /// <summary>
        /// The vertex winding that will cause a triangle to be determined as front-facing.
        /// </summary>
        public GPUFrontFace FrontFace;
        
        /// <summary>
        /// A scalar factor controlling the depth value added to each fragment.
        /// </summary>
        public float DepthBiasConstantFactor;
        
        /// <summary>
        /// The maximum depth bias of a fragment.
        /// </summary>
        public float DepthBiasClamp;
        
        /// <summary>
        /// A scalar factor applied to a fragment's slope in depth calculations.
        /// </summary>
        public float DepthBiasSlopeFactor;
        
        /// <summary>
        /// true to bias fragment depth values.
        /// </summary>
        public Byte EnableDepthBias;
        
        /// <summary>
        /// true to enable depth clip, false to enable depth clamp.
        /// </summary>
        public Byte EnableDepthClip;
        
        private Byte _padding1;
        
        private Byte _padding2;
    }
}