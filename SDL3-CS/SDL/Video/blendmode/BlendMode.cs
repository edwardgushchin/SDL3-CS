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
    /// <para>A set of blend modes used in drawing operations.</para>
    /// <para>These predefined blend modes are supported everywhere.</para>
    /// <para>Additional values may be obtained from <see cref="ComposeCustomBlendMode"/>.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="ComposeCustomBlendMode"/>
    public enum BlendMode : uint
    {
        /// <summary>
        /// no blending: dstRGBA = srcRGBA
        /// </summary>
        None = 0x00000000u,
        
        /// <summary>
        /// alpha blending: dstRGB = (srcRGB * srcA) + (dstRGB * (1-srcA)), dstA = srcA + (dstA * (1-srcA))
        /// </summary>
        Blend = 0x00000001u,
        
        /// <summary>
        /// pre-multiplied alpha blending: dstRGBA = srcRGBA + (dstRGBA * (1-srcA))
        /// </summary>
        BlendPremultiplied = 0x00000010u,
        
        /// <summary>
        /// additive blending: dstRGB = (srcRGB * srcA) + dstRGB, dstA = dstA
        /// </summary>
        Add =                   0x00000002u,
        
        /// <summary>
        /// pre-multiplied additive blending: dstRGB = srcRGB + dstRGB, dstA = dstA
        /// </summary>
        AddPremultiplied =     0x00000020u,
        
        /// <summary>
        /// color modulate: dstRGB = srcRGB * dstRGB, dstA = dstA
        /// </summary>
        Mod =                   0x00000004u,
        
        /// <summary>
        /// color multiply: dstRGB = (srcRGB * dstRGB) + (dstRGB * (1-srcA)), dstA = dstA
        /// </summary>
        Mul =                   0x00000008u,
        Invalid =               0x7FFFFFFFu,
    }
}