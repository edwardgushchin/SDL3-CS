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
    /// <para>Specifies the pixel format of a texture.</para>
    /// <para>Texture format support varies depending on driver, hardware, and usage
    /// flags. In general, you should use <see cref="GPUTextureSupportsFormat"/> to query if
    /// a format is supported before using it. However, there are a few guaranteed
    /// formats.</para>
    /// <para>FIXME: Check universal support for 32-bit component formats FIXME: Check
    /// universal support for <see cref="GPUTextureUsageFlags.ComputeStorageSimultaneousReadWrite"/></para>
    /// <para>For <see cref="GPUTextureUsageFlags.Sampler"/> usage, the following formats are universally supported:</para>
    /// <list type="bullet">
    /// <item><see cref="R8G8B8A8Unorm"/></item>
    /// <item><see cref="B8G8R8A8Unorm"/></item>
    /// <item><see cref="R8Unorm"/></item>
    /// <item><see cref="R8Snorm"/></item>
    /// <item><see cref="R8G8Unorm"/></item>
    /// <item><see cref="R8G8Snorm"/></item>
    /// <item><see cref="R8G8B8A8Snorm"/></item>
    /// <item><see cref="R16Float"/></item>
    /// <item><see cref="R16G16Float"/></item>
    /// <item><see cref="R16G16B16A16Float"/></item>
    /// <item><see cref="R32Float"/></item>
    /// <item><see cref="R32G32Float"/></item>
    /// <item><see cref="R32G32B32A32Float"/></item>
    /// <item><see cref="R11G11B10UFloat"/></item>
    /// <item><see cref="R8G8B8A8UnormSRGB"/></item>
    /// <item><see cref="B8G8R8A8UnormSRGB"/></item>
    /// <item><see cref="D16Unorm"/></item>
    /// </list>
    /// <para>For <see cref="GPUTextureUsageFlags.ColorTarget"/> usage, the following formats are universally supported:</para>
    /// <list type="bullet">
    /// <item><see cref="R8G8B8A8Unorm"/></item>
    /// <item><see cref="B8G8R8A8Unorm"/></item>
    /// <item><see cref="R8Unorm"/></item>
    /// <item><see cref="R16Float"/></item>
    /// <item><see cref="R16G16Float"/></item>
    /// <item><see cref="R16G16B16A16Float"/></item>
    /// <item><see cref="R32Float"/></item>
    /// <item><see cref="R32G32Float"/></item>
    /// <item><see cref="R32G32B32A32Float"/></item>
    /// <item><see cref="R8Uint"/></item>
    /// <item><see cref="R8G8Uint"/></item>
    /// <item><see cref="R8G8B8A8Uint"/></item>
    /// <item><see cref="R16Uint"/></item>
    /// <item><see cref="R16G16Uint"/></item>
    /// <item><see cref="R16G16B16A16Uint"/></item>
    /// <item><see cref="R8Int"/></item>
    /// <item><see cref="R8G8Int"/></item>
    /// <item><see cref="R8G8B8A8Int"/></item>
    /// <item><see cref="R16Int"/></item>
    /// <item><see cref="R16G16Int"/></item>
    /// <item><see cref="R16G16B16A16Int"/></item>
    /// <item><see cref="R8G8B8A8UnormSRGB"/></item>
    /// <item><see cref="B8G8R8A8UnormSRGB"/></item>
    /// </list>
    /// <para>For <see cref="GPUTextureUsageFlags.Sampler"/> usages, the following formats are universally supported:</para>
    /// <list type="bullet">
    /// <item><see cref="R8G8B8A8Unorm"/></item>
    /// <item><see cref="R8G8B8A8Snorm"/></item>
    /// <item><see cref="R16G16B16A16Float"/></item>
    /// <item><see cref="R32Float"/></item>
    /// <item><see cref="R32G32Float"/></item>
    /// <item><see cref="R32G32B32A32Float"/></item>
    /// <item><see cref="R8G8B8A8Uint"/></item>
    /// <item><see cref="R16G16B16A16Uint"/></item>
    /// <item><see cref="R8G8B8A8Int"/></item>
    /// <item><see cref="R16G16B16A16Int"/></item>
    /// </list>
    /// <para>For <see cref="GPUTextureUsageFlags.DepthStencilTarget"/> usage, the following formats are universally
    /// supported:</para>
    /// <list type="bullet">
    /// <item><see cref="D16Unorm"/></item>
    /// <item>Either (but not necessarily both!) <see cref="D24Unorm"/> or <see cref="GPUTextureFormat.D32Float"/></item>
    /// <item>Either (but not necessarily both!) <see cref="D24UnormS8Uint"/> or <see cref="GPUTextureFormat.D32FloatS8Uint"/></item>
    /// </list>
    /// <para>Unless D16Unorm is sufficient for your purposes, always check which of
    /// D24/D32 is supported before creating a depth-stencil texture!</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUTexture"/>
    /// <seealso cref="GPUTextureSupportsFormat"/>
    public enum GPUTextureFormat
    {
        Invalid,
        
        /* Unsigned Normalized Float Color Formats */
        A8Unorm,
        R8Unorm,
        R8G8Unorm,
        R8G8B8A8Unorm,
        R16Unorm,
        R16G16Unorm,
        R16G16B16A16Unorm,
        R10G10B10A2Unorm,
        B5G6R5Unorm,
        B5G5R5A1Unorm,
        B4G4R4A4Unorm,
        B8G8R8A8Unorm,
        
        /* Compressed Unsigned Normalized Float Color Formats */
        BC1RGBAUnorm,
        BC2RGBAUnorm,
        BC3RGBAUnorm,
        BC4RUnorm,
        BC5RGUnorm,
        BC7RGBAUnorm,
        
        /* Compressed Signed Float Color Formats */
        BC6HRGBFloat,
        
        /* Compressed Unsigned Float Color Formats */
        BC6HRGBUFloat,
        
        /* Signed Normalized Float Color Formats  */
        R8Snorm,
        R8G8Snorm,
        R8G8B8A8Snorm,
        R16Snorm,
        R16G16Snorm,
        R16G16B16A16Snorm,
        
        /* Signed Float Color Formats */
        R16Float,
        R16G16Float,
        R16G16B16A16Float,
        R32Float,
        R32G32Float,
        R32G32B32A32Float,
        
        /* Unsigned Float Color Formats */
        R11G11B10UFloat,
        
        /* Unsigned Integer Color Formats */
        R8Uint,
        R8G8Uint,
        R8G8B8A8Uint,
        R16Uint,
        R16G16Uint,
        R16G16B16A16Uint,
        R32Uint,
        R32G32Uint,
        R32G32B32A32Uint,
        
        /* Signed Integer Color Formats */
        R8Int,
        R8G8Int,
        R8G8B8A8Int,
        R16Int,
        R16G16Int,
        R16G16B16A16Int,
        R32Int,
        R32G32Int,
        R32G32B32A32Int,
        
        /* SRGB Unsigned Normalized Color Formats */
        R8G8B8A8UnormSRGB,
        B8G8R8A8UnormSRGB,
        
        /* Compressed SRGB Unsigned Normalized Color Formats */
        BC1RGBAUnormSRGB,
        BC2RGBAUnormSRGB,
        BC3RGBAUnormSRGB,
        BC7RGBAUnormSRGB,
        
        /* Depth Formats */
        D16Unorm,
        D24Unorm,
        D32Float,
        D24UnormS8Uint,
        D32FloatS8Uint,
        
        /* Compressed ASTC Normalized Float Color Formats */
        ASTC4X4Unorm,
        ASTC5X4Unorm,
        ASTC5X5Unorm,
        ASTC6X5Unorm,
        ASTC6X6Unorm,
        ASTC8X5Unorm,
        ASTC8X6Unorm,
        ASTC8X8Unorm,
        ASTC10X5Unorm,
        ASTC10X6Unorm,
        ASTC10X8Unorm,
        ASTC10X10Unorm,
        ASTC12X10Unorm,
        ASTC12X12Unorm,
        
        /* Compressed SRGB ASTC Normalized Float Color Formats */
        ASTC4X4UnormSRGB,
        ASTC5X4UnormSRGB,
        ASTC5X5UnormSRGB,
        ASTC6X5UnormSRGB,
        ASTC6X6UnormSRGB,
        ASTC8X5UnormSRGB,
        ASTC8X6UnormSRGB,
        ASTC8X8UnormSRGB,
        ASTC10X5UnormSRGB,
        ASTC10X6UnormSRGB,
        ASTC10X8UnormSRGB,
        ASTC10X10UnormSRGB,
        ASTC12X10UnormSRGB,
        ASTC12X12UnormSRGB,
        
        /* Compressed ASTC Signed Float Color Formats */
        ASTC4X4Float,
        ASTC5X4Float,
        ASTC5X5Float,
        ASTC6X5Float,
        ASTC6X6Float,
        ASTC8X5Float,
        ASTC8X6Float,
        ASTC8X8Float,
        ASTC10X5Float,
        ASTC10X6Float,
        ASTC10X8Float,
        ASTC10X10Float,
        ASTC12X10Float,
        ASTC12X12Float
    }
}