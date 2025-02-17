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
    /// A structure specifying the descriptions of render targets used in a
    /// graphics pipeline.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GPUGraphicsPipelineCreateInfo"/>
    /// <seealso cref="GPUColorTargetDescription"/>
    /// <seealso cref="GPUTextureFormat"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUGraphicsPipelineTargetInfo
    {
        /// <summary>
        /// A pointer to an array of color target descriptions.
        /// </summary>
        public IntPtr ColorTargetDescriptions;
        
        /// <summary>
        /// The number of color target descriptions in the above array.
        /// </summary>
        public UInt32 NumColorTargets;
        
        /// <summary>
        /// The pixel format of the depth-stencil target. Ignored if has_depth_stencil_target is false.
        /// </summary>
        public GPUTextureFormat DepthStencilFormat;
        
        /// <summary>
        /// true specifies that the pipeline uses a depth-stencil target.
        /// </summary>
        public  Byte HasDepthStencilTarget;
        
        private Byte _padding1;
        
        private Byte _padding2;
        
        private Byte _padding3;
    }
}