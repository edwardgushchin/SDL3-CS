#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
    /// <para>Pixel format.</para>
    /// <para>SDL's pixel formats have the following naming convention:</para>
    /// <list type="bullet">
    /// <item>Names with a list of components and a single bit count, such as RGB24 and
    /// ABGR32, define a platform-independent encoding into bytes in the order
    /// specified. For example, in RGB24 data, each pixel is encoded in 3 bytes
    /// (red, green, blue) in that order, and in ABGR32 data, each pixel is
    /// encoded in 4 bytes alpha, blue, green, red) in that order. Use these
    /// names if the property of a format that is important to you is the order
    /// of the bytes in memory or on disk.</item>
    /// <item>Names with a bit count per component, such as ARGB8888 and XRGB1555, are
    /// "packed" into an appropriately-sized integer in the platform's native
    /// endianness. For example, ARGB8888 is a sequence of 32-bit integers; in
    /// each integer, the most significant bits are alpha, and the least
    /// significant bits are blue. On a little-endian CPU such as x86, the least
    /// significant bits of each integer are arranged first in memory, but on a
    /// big-endian CPU such as s390x, the most significant bits are arranged
    /// first. Use these names if the property of a format that is important to
    /// you is the meaning of each bit position within a native-endianness
    /// integer.</item>
    /// <item>In indexed formats such as INDEX4LSB, each pixel is represented by
    /// encoding an index into the palette into the indicated number of bits,
    /// with multiple pixels packed into each byte if appropriate. In LSB
    /// formats, the first (leftmost) pixel is stored in the least-significant
    /// bits of the byte; in MSB formats, it's stored in the most-significant
    /// bits. INDEX8 does not need LSB/MSB variants, because each pixel exactly
    /// fills one byte.</item>
    /// </list>
    /// <para>The 32-bit byte-array encodings such as RGBA32 are aliases for the
    /// appropriate 8888 encoding for the current platform. For example, RGBA32 is
    /// an alias for ABGR8888 on little-endian CPUs like x86, or an alias for
    /// RGBA8888 on big-endian CPUs.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.0.0.</since>
    public enum PixelFormat : uint
    {
        Unknown = 0,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX1, SDL_BITMAPORDER_4321, 0, 1, 0)
        /// </summary>
        Index1LSB = 0x11100100u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX1, SDL_BITMAPORDER_1234, 0, 1, 0)
        /// </summary>
        Index1MSB = 0x11200100u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX2, SDL_BITMAPORDER_4321, 0, 2, 0)
        /// </summary>
        Index2LSB = 0x1c100200u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX2, SDL_BITMAPORDER_1234, 0, 2, 0)
        /// </summary>
        Index2MSB = 0x1c200200u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX4, SDL_BITMAPORDER_4321, 0, 4, 0)
        /// </summary>
        Index4LSB = 0x12100400u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX4, SDL_BITMAPORDER_1234, 0, 4, 0)
        /// </summary>
        Index4MSB = 0x12200400u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_INDEX8, 0, 0, 8, 1)
        /// </summary>
        Index8 = 0x13000801u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED8, SDL_PACKEDORDER_XRGB, SDL_PACKEDLAYOUT_332, 8, 1)
        /// </summary>
        RGB332 = 0x14110801u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_XRGB, SDL_PACKEDLAYOUT_4444, 12, 2)
        /// </summary>
        XRGB4444 = 0x15120c02u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_XBGR, SDL_PACKEDLAYOUT_4444, 12, 2)
        /// </summary>
        XBGR4444 = 0x15520c02u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_XRGB, SDL_PACKEDLAYOUT_1555, 15, 2)
        /// </summary>
        XRGB1555 = 0x15130f02u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_XBGR, SDL_PACKEDLAYOUT_1555, 15, 2)
        /// </summary>
        XBGR1555 = 0x15530f02u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_ARGB, SDL_PACKEDLAYOUT_4444, 16, 2)
        /// </summary>
        ARGB4444 = 0x15321002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_RGBA, SDL_PACKEDLAYOUT_4444, 16, 2)
        /// </summary>
        RGBA4444 = 0x15421002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_ABGR, SDL_PACKEDLAYOUT_4444, 16, 2)
        /// </summary>
        ABGR4444 = 0x15721002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_BGRA, SDL_PACKEDLAYOUT_4444, 16, 2)
        /// </summary>
        BGRA4444 = 0x15821002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_ARGB, SDL_PACKEDLAYOUT_1555, 16, 2)
        /// </summary>
        ARGB1555 = 0x15331002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_RGBA, SDL_PACKEDLAYOUT_5551, 16, 2)
        /// </summary>
        RGBA5551 = 0x15441002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_ABGR, SDL_PACKEDLAYOUT_1555, 16, 2)
        /// </summary>
        ABGR1555 = 0x15731002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_BGRA, SDL_PACKEDLAYOUT_5551, 16, 2)
        /// </summary>
        BGRA5551 = 0x15841002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_XRGB, SDL_PACKEDLAYOUT_565, 16, 2)
        /// </summary>
        RGB565 = 0x15151002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED16, SDL_PACKEDORDER_XBGR, SDL_PACKEDLAYOUT_565, 16, 2)
        /// </summary>
        BGR565 = 0x15551002u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU8, SDL_ARRAYORDER_RGB, 0, 24, 3)
        /// </summary>
        RGB24 = 0x17101803u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU8, SDL_ARRAYORDER_BGR, 0, 24, 3)
        /// </summary>
        BGR24 = 0x17401803u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_XRGB, SDL_PACKEDLAYOUT_8888, 24, 4)
        /// </summary>
        XRGB8888 = 0x16161804u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_RGBX, SDL_PACKEDLAYOUT_8888, 24, 4)
        /// </summary>
        RGBX8888 = 0x16261804u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_XBGR, SDL_PACKEDLAYOUT_8888, 24, 4)
        /// </summary>
        XBGR8888 = 0x16561804u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_BGRX, SDL_PACKEDLAYOUT_8888, 24, 4)
        /// </summary>
        BGRX8888 = 0x16661804u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_ARGB, SDL_PACKEDLAYOUT_8888, 32, 4)
        /// </summary>
        ARGB8888 = 0x16362004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_RGBA, SDL_PACKEDLAYOUT_8888, 32, 4)
        /// </summary>
        RGBA8888 = 0x16462004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_ABGR, SDL_PACKEDLAYOUT_8888, 32, 4)
        /// </summary>
        ABGR8888 = 0x16762004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_BGRA, SDL_PACKEDLAYOUT_8888, 32, 4)
        /// </summary>
        BGRA8888 = 0x16862004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_XRGB, SDL_PACKEDLAYOUT_2101010, 32, 4)
        /// </summary>
        XRGB2101010 = 0x16172004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_XBGR, SDL_PACKEDLAYOUT_2101010, 32, 4)
        /// </summary>
        XBGR2101010 = 0x16572004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_ARGB, SDL_PACKEDLAYOUT_2101010, 32, 4)
        /// </summary>
        ARGB2101010 = 0x16372004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_PACKED32, SDL_PACKEDORDER_ABGR, SDL_PACKEDLAYOUT_2101010, 32, 4)
        /// </summary>
        ABGR2101010 = 0x16772004u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU16, SDL_ARRAYORDER_RGB, 0, 48, 6)
        /// </summary>
        RGB48 = 0x18103006u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU16, SDL_ARRAYORDER_BGR, 0, 48, 6)
        /// </summary>
        BGR48 = 0x18403006u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU16, SDL_ARRAYORDER_RGBA, 0, 64, 8)
        /// </summary>
        RGBA64 = 0x18204008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU16, SDL_ARRAYORDER_ARGB, 0, 64, 8)
        /// </summary>
        ARGB64 = 0x18304008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU16, SDL_ARRAYORDER_BGRA, 0, 64, 8)
        /// </summary>
        BGRA64 = 0x18504008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYU16, SDL_ARRAYORDER_ABGR, 0, 64, 8)
        /// </summary>
        ABGR64 = 0x18604008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF16, SDL_ARRAYORDER_RGB, 0, 48, 6)
        /// </summary>
        RGB48Float = 0x1a103006u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF16, SDL_ARRAYORDER_BGR, 0, 48, 6)
        /// </summary>
        BGR48Float = 0x1a403006u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF16, SDL_ARRAYORDER_RGBA, 0, 64, 8)
        /// </summary>
        RGBA64Float = 0x1a204008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF16, SDL_ARRAYORDER_ARGB, 0, 64, 8)
        /// </summary>
        ARGB64Float = 0x1a304008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF16, SDL_ARRAYORDER_BGRA, 0, 64, 8)
        /// </summary>
        BGRA64Float = 0x1a504008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF16, SDL_ARRAYORDER_ABGR, 0, 64, 8)
        /// </summary>
        ABGR64Float = 0x1a604008u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF32, SDL_ARRAYORDER_RGB, 0, 96, 12)
        /// </summary>
        RGB96Float = 0x1b10600cu,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF32, SDL_ARRAYORDER_BGR, 0, 96, 12)
        /// </summary>
        BGR96Float = 0x1b40600cu,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF32, SDL_ARRAYORDER_RGBA, 0, 128, 16)
        /// </summary>
        RGBA128Float = 0x1b208010u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF32, SDL_ARRAYORDER_ARGB, 0, 128, 16)
        /// </summary>
        ARGB128Float = 0x1b308010u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF32, SDL_ARRAYORDER_BGRA, 0, 128, 16)
        /// </summary>
        BGRA128Float = 0x1b508010u,
        
        /// <summary>
        /// SDL_DEFINE_PIXELFORMAT(SDL_PIXELTYPE_ARRAYF32, SDL_ARRAYORDER_ABGR, 0, 128, 16)
        /// </summary>
        ABGR128Float = 0x1b608010u,
        
        /// <summary>
        /// <para>Planar mode: Y + V + U  (3 planes)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('Y', 'V', '1', '2')</para>
        /// </summary>
        YV12 = 0x32315659u,
        
        /// <summary>
        /// <para>Planar mode: Y + U + V  (3 planes)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('I', 'Y', 'U', 'V')</para>
        /// </summary>
        IYUV = 0x56555949u,
        
        /// <summary>
        /// <para>Packed mode: Y0+U0+Y1+V0 (1 plane)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('Y', 'U', 'Y', '2')</para>
        /// </summary>
        YUY2 = 0x32595559u,
        
        /// <summary>
        /// <para>Packed mode: U0+Y0+V0+Y1 (1 plane)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('U', 'Y', 'V', 'Y')</para>
        /// </summary>
        UYVY = 0x59565955u,
        
        /// <summary>
        /// <para>Packed mode: Y0+V0+Y1+U0 (1 plane)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('Y', 'V', 'Y', 'U')</para>
        /// </summary>
        YVYU = 0x55595659u,
        
        /// <summary>
        /// <para>Planar mode: Y + U/V interleaved  (2 planes)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('N', 'V', '1', '2')</para>
        /// </summary>
        NV12 = 0x3231564eu,
        
        /// <summary>
        /// <para>Planar mode: Y + V/U interleaved  (2 planes)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('N', 'V', '2', '1')</para>
        /// </summary>
        NV21 = 0x3132564eu,
        
        /// <summary>
        /// <para>Planar mode: Y + U/V interleaved  (2 planes)</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('P', '0', '1', '0')</para>
        /// </summary>
        P010 = 0x30313050u,
        
        /// <summary>
        /// <para>Android video texture format</para>
        /// <para>SDL_DEFINE_PIXELFOURCC('O', 'E', 'S', ' ')</para>
        /// </summary>
        ExternalOES = 0x2053454fu
    }
}