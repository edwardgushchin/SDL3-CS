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
    /// A structure specifying the parameters of the graphics pipeline depth
    /// stencil state.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GPUGraphicsPipelineCreateInfo"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUDepthStencilState
    {
        /// <summary>
        /// The comparison operator used for depth testing.
        /// </summary>
        public GPUCompareOp CompareOp;
        
        /// <summary>
        /// The stencil op state for back-facing triangles.
        /// </summary>
        public GPUStencilOpState BackStencilState;
        
        /// <summary>
        /// The stencil op state for front-facing triangles.
        /// </summary>
        public GPUStencilOpState FrontStencilState;
        
        /// <summary>
        /// Selects the bits of the stencil values participating in the stencil test.
        /// </summary>
        public Byte CompareMask;
        
        /// <summary>
        /// Selects the bits of the stencil values updated by the stencil test.
        /// </summary>
        public Byte WriteMask;
        
        private Byte _enableDepthTest;
        
        private Byte _enableDepthWrite;
        
        public Byte _enableStencilTest;
        
        private Byte _padding1;
        
        private Byte _padding2;
        
        private Byte _padding3;

        /// <summary>
        /// true enables the depth test.
        /// </summary>
        public bool EnableDepthTest
        {
            get => _enableDepthTest > 0;
            set => _enableDepthTest = (byte)(value ? 1 : 0);
        }

        /// <summary>
        /// true enables depth writes. Depth writes are always disabled when enable_depth_test is false.
        /// </summary>
        public bool EnableDepthWrite
        {
            get => _enableDepthWrite > 0;
            set => _enableDepthWrite = (byte)(value ? 1 : 0);
        }

        /// <summary>
        /// true enables the stencil test.
        /// </summary>
        public bool EnableStencilTest
        {
            get => _enableStencilTest > 0;
            set => _enableStencilTest = (byte)(value ? 1 : 0);
        }
    }
}