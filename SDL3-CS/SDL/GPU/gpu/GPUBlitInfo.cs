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
    /// A structure containing parameters for a blit command.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="BlitGPUTexture"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUBlitInfo
    {
        /// <summary>
        /// The source region for the blit.
        /// </summary>
        public GPUBlitRegion Source;
        
        /// <summary>
        /// The destination region for the blit.
        /// </summary>
        public GPUBlitRegion Destination;
        
        /// <summary>
        /// What is done with the contents of the destination before the blit.
        /// </summary>
        public GPULoadOp LoadOp;
        
        /// <summary>
        /// The color to clear the destination region to before the blit. Ignored if load_op is not public GPU_LOADOP_CLEAR.
        /// </summary>
        public FColor ClearColor;
        
        /// <summary>
        /// The flip mode for the source region.
        /// </summary>
        public FlipMode FlipMode;
        
        /// <summary>
        /// The filter mode used when blitting.
        /// </summary>
        public GPUFilter Filter;
        
        /// <summary>
        /// true cycles the destination texture if it is already bound.
        /// </summary>
        public Byte Cycle;
        
        private Byte _padding1;
        
        private Byte _padding2;
        
        private Byte _padding3;
    }
}