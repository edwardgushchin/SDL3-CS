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
    /// <para>The normalized factor used to multiply pixel components.</para>
    /// <para>The blend factors are multiplied with the pixels from a drawing operation
    /// (src) and the pixels from the render target (dst) before the blend
    /// operation. The comma-separated factors listed above are always applied in
    /// the component order red, green, blue, and alpha.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum BlendFactor
    {
        /// <summary>
        /// 0, 0, 0, 0
        /// </summary>
        Zero = 0x1,
        
        /// <summary>
        /// 1, 1, 1, 1
        /// </summary>
        One = 0x2,
        
        /// <summary>
        /// srcR, srcG, srcB, srcA
        /// </summary>
        SrcColor = 0x3,
        
        /// <summary>
        /// 1-srcR, 1-srcG, 1-srcB, 1-srcA
        /// </summary>
        OneMinusSrcColor = 0x4,
        
        /// <summary>
        /// srcA, srcA, srcA, srcA
        /// </summary>
        SrcAlpha = 0x5,
        
        /// <summary>
        /// 1-srcA, 1-srcA, 1-srcA, 1-srcA
        /// </summary>
        OneMinusSrcAlpha = 0x6,
        
        /// <summary>
        /// dstR, dstG, dstB, dstA
        /// </summary>
        DstColor = 0x7,
        
        /// <summary>
        /// 1-dstR, 1-dstG, 1-dstB, 1-dstA
        /// </summary>
        OneMinusDstColor = 0x8,
        
        /// <summary>
        /// dstA, dstA, dstA, dstA
        /// </summary>
        DstAlpha = 0x9,
        
        /// <summary>
        /// 1-dstA, 1-dstA, 1-dstA, 1-dstA
        /// </summary>
        OneMinusDstAlpha = 0xA
    }
}