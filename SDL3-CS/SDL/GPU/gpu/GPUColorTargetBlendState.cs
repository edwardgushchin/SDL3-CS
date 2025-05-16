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
    /// A structure specifying the blend state of a color target.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GPUColorTargetDescription"/>
    /// <seealso cref="GPUBlendFactor"/>
    /// <seealso cref="GPUBlendOp"/>
    /// <seealso cref="GPUColorComponentFlags"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUColorTargetBlendState
    {
        /// <summary>
        /// The value to be multiplied by the source RGB value.
        /// </summary>
        public GPUBlendFactor SrcColorBlendfactor;
        
        /// <summary>
        /// The value to be multiplied by the destination RGB value.
        /// </summary>
        public GPUBlendFactor DstColorBlendfactor;
        
        /// <summary>
        /// The blend operation for the RGB components.
        /// </summary>
        public GPUBlendOp ColorBlendOp;
        
        /// <summary>
        /// The value to be multiplied by the source alpha.
        /// </summary>
        public GPUBlendFactor SrcAlphaBlendfactor;
        
        /// <summary>
        /// The value to be multiplied by the destination alpha.
        /// </summary>
        public GPUBlendFactor DstAlphaBlendfactor;
        
        /// <summary>
        /// The blend operation for the alpha component.
        /// </summary>
        public GPUBlendOp AlphaBlendOp;
        
        /// <summary>
        /// A bitmask specifying which of the RGBA components are enabled for writing. Writes to all channels if enable_color_write_mask is false.
        /// </summary>
        public GPUColorComponentFlags ColorWriteMask;
        
        /// <summary>
        /// Whether blending is enabled for the color target.
        /// </summary>
        public Byte EnableBlend;
        
        /// <summary>
        /// Whether the color write mask is enabled.
        /// </summary>
        public Byte EnableColorWriteMask;
        
        private Byte _padding1;
        
        private Byte _padding2;
    }
}