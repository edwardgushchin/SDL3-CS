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
    /// <para>Specifies a blending factor to be used when pixels in a render target are
    /// blended with existing pixels in the texture.</para>
    /// <para>The source color is the value written by the fragment shader. The
    /// destination color is the value currently existing in the texture.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUGraphicsPipeline"/>
    public enum GPUBlendFactor
    {
        Invalid,
        
        /// <summary>
        /// 0
        /// </summary>
        Zero,
        
        /// <summary>
        /// 1
        /// </summary>
        One,
        
        /// <summary>
        /// source color
        /// </summary>
        SrcColor,
        
        /// <summary>
        /// 1 - source color
        /// </summary>
        OneMinusSrcColor,
        
        /// <summary>
        /// destination color
        /// </summary>
        DstColor,
        
        
        /// <summary>
        /// 1 - destination color
        /// </summary>
        OneMinusDstColor,
        
        /// <summary>
        /// source alpha
        /// </summary>
        SrcAlpha,
        
        /// <summary>
        /// 1 - source alpha
        /// </summary>
        OneMinusSrcAlpha,
        
        /// <summary>
        /// destination alpha
        /// </summary>
        DstAlpha,
        
        /// <summary>
        /// 1 - destination alpha
        /// </summary>
        OneMinusDstAlpha,
        
        /// <summary>
        /// blend constant
        /// </summary>
        ConstantColor,
        
        /// <summary>
        /// 1 - blend constant
        /// </summary>
        OneMinusConstantColor,
        
        /// <summary>
        /// min(source alpha, 1 - destination alpha)
        /// </summary>
        SrcAlphaSaturate
    }
}