﻿#region License
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
    /// A structure specifying the stencil operation state of a graphics pipeline.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GPUDepthStencilState"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUStencilOpState
    {
        /// <summary>
        /// The action performed on samples that fail the stencil test.
        /// </summary>
        public GPUStencilOp FailOp;
        
        /// <summary>
        /// The action performed on samples that pass the depth and stencil tests.
        /// </summary>
        public GPUStencilOp PassOp;
        
        /// <summary>
        /// The action performed on samples that pass the stencil test and fail the depth test.
        /// </summary>
        public GPUStencilOp DepthFailOp;
        
        /// <summary>
        /// The comparison operator used in the stencil test.
        /// </summary>
        public GPUCompareOp CompareOp;
    }
}