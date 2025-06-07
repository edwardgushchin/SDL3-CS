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
    /// <para>Pixel format.</para>
    /// <para>SDL's pixel formats have the following naming convention:</para>
    /// <list type="bullet">
    /// <item>Names with a list of components and a single bit count, such as RGB24 and
    /// ABGR32, define a platform-independent encoding into bytes in the order
    /// specified. For example, in RGB24 data, each pixel is encoded in 3 bytes
    /// (red, green, blue) in that order, and in ABGR32 data, each pixel is
    /// encoded in 4 bytes (alpha, blue, green, red) in that order. Use these
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
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum PixelFormat : uint
    {
        Unknown = 0,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index1, (byte)BitmapOrder.Order4321, 0, 1, 0);</code>
        /// </summary>
        Index1LSB = 0x11100100u,

        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index1, (byte)BitmapOrder.Order1234, 0, 1, 0);</code>
        /// </summary>
        Index1MSB = 0x11200100u,

        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index2, (byte)BitmapOrder.Order4321, 0, 2, 0);</code>
        /// </summary>
        Index2LSB = 0x1c100200u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index2, (byte)BitmapOrder.Order1234, 0, 2, 0);</code>
        /// </summary>
        Index2MSB = 0x1c200200u,

        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index4, (byte)BitmapOrder.Order4321, 0, 4, 0);</code>
        /// </summary>
        Index4LSB = 0x12100400u,

        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index4, (byte)BitmapOrder.Order1234, 0, 4, 0);</code>
        /// </summary>
        Index4MSB = 0x12200400u,

        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Index8, 0, 0, 8, 1);</code>
        /// </summary>
        Index8 = 0x13000801u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed8, (byte)PackedOrder.XRGB, PackedLayout.Layout332, 8, 1);</code>
        /// </summary>
        RGB332 = 0x14110801u,
         
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.XRGB, PackedLayout.Layout4444, 12, 2);</code>
        /// </summary>
        XRGB4444 = 0x15120c02u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.XBGR, PackedLayout.Layout4444, 12, 2);</code>
        /// </summary>
        XBGR4444 = 0x15520c02u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.XRGB, PackedLayout.Layout1555, 15, 2);</code>
        /// </summary>
        XRGB1555 = 0x15130f02u,
         
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.XBGR, PackedLayout.Layout1555, 15, 2);</code>
        /// </summary>
        XBGR1555 = 0x15530f02u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.ARGB, PackedLayout.Layout4444, 16, 2);</code>
        /// </summary>
        ARGB4444 = 0x15321002u,
         
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.RGBA, PackedLayout.Layout4444, 16, 2);</code>
        /// </summary>
        RGBA4444 = 0x15421002u,
         
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.ARGB, PackedLayout.Layout4444, 16, 2);</code>
        /// </summary>
        ABGR4444 = 0x15721002u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.BGRA, PackedLayout.Layout4444, 16, 2);</code>
        /// </summary>
        BGRA4444 = 0x15821002u,
          
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.ARGB, PackedLayout.Layout1555, 16, 2);</code>
        /// </summary>
        ARGB1555 = 0x15331002u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.RGBA, PackedLayout.Layout5551, 16, 2);</code>
        /// </summary>
        RGBA5551 = 0x15441002u,
           
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.ABGR, PackedLayout.Layout5551, 16, 2);</code>
        /// </summary>
        ABGR1555 = 0x15731002u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.BGRA, PackedLayout.Layout5551, 16, 2);</code>
        /// </summary>
        BGRA5551 = 0x15841002u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.XRGB, PackedLayout.Layout565, 16, 2);</code>
        /// </summary>
        RGB565 = 0x15151002u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed16, (byte)PackedOrder.XBGR, PackedLayout.Layout565, 16, 2);</code>
        /// </summary>
        BGR565 = 0x15551002u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU8, (byte)ArrayOrder.RGB, 0, 24, 3);</code>
        /// </summary>
        RGB24 = 0x17101803u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU8, (byte)ArrayOrder.BGR, 0, 24, 3);</code>
        /// </summary>
        BGR24 = 0x17401803u,
           
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.XRGB, PackedLayout.Layout8888, 24, 4);</code>
        /// </summary>
        XRGB8888 = 0x16161804u,
        
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.RGBX, PackedLayout.Layout8888, 24, 4);</code>
        /// </summary>
        RGBX8888 = 0x16261804u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.XBGR, PackedLayout.Layout8888, 24, 4);</code>
        /// </summary>
        XBGR8888 = 0x16561804u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.BGRX, PackedLayout.Layout8888, 24, 4);</code>
        /// </summary>
        BGRX8888 = 0x16661804u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.ARGB, PackedLayout.Layout8888, 32, 4);</code>
        /// </summary>
        ARGB8888 = 0x16362004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.RGBA, PackedLayout.Layout8888, 32, 4);</code>
        /// </summary>
        RGBA8888 = 0x16462004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.ARGB, PackedLayout.Layout8888, 32, 4);</code>
        /// </summary>
        ABGR8888 = 0x16762004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.BGRA, PackedLayout.Layout8888, 32, 4);</code>
        /// </summary>
        BGRA8888 = 0x16862004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.XRGB, PackedLayout.Layout2101010, 32, 4);</code>
        /// </summary>
        XRGB2101010 = 0x16172004u,
           
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.XBGR, PackedLayout.Layout2101010, 32, 4);</code>
        /// </summary>
        XBGR2101010 = 0x16572004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.ARGB, PackedLayout.Layout2101010, 32, 4);</code>
        /// </summary>
        ARGB2101010 = 0x16372004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.Packed32, (byte)PackedOrder.ABGR, PackedLayout.Layout2101010, 32, 4);</code>
        /// </summary>
        ABGR2101010 = 0x16772004u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU16, (byte)ArrayOrder.RGB, 0, 48, 6);</code>
        /// </summary>
        RGB48 = 0x18103006u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU16, (byte)ArrayOrder.BGR, 0, 48, 6);</code>
        /// </summary>
        BGR48 = 0x18403006u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU16, (byte)ArrayOrder.RGBA, 0, 64, 8);</code>
        /// </summary>
        RGBA64 = 0x18204008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU16, (byte)ArrayOrder.ARGB, 0, 64, 8);</code>
        /// </summary>
        ARGB64 = 0x18304008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU16, (byte)ArrayOrder.BGRA, 0, 64, 8);</code>
        /// </summary>
        BGRA64 = 0x18504008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayU16, (byte)ArrayOrder.ABGR, 0, 64, 8);</code>
        /// </summary>
        ABGR64 = 0x18604008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF16, (byte)ArrayOrder.RGB, 0, 48, 6);</code>
        /// </summary>
        RGB48Float = 0x1a103006u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF16, (byte)ArrayOrder.BGR, 0, 48, 6);</code>
        /// </summary>
        BGR48Float = 0x1a403006u,
           
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF16, (byte)ArrayOrder.RGBA, 0, 64, 8);</code>
        /// </summary>
        RGBA64Float = 0x1a204008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF16, (byte)ArrayOrder.ARGB, 0, 64, 8);</code>
        /// </summary>
        ARGB64Float = 0x1a304008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF16, (byte)ArrayOrder.BGRA, 0, 64, 8);</code>
        /// </summary>
        BGRA64Float = 0x1a504008u,
           
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF16, (byte)ArrayOrder.ABGR, 0, 64, 8);</code>
        /// </summary>
        ABGR64Float = 0x1a604008u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF32, (byte)ArrayOrder.RGB, 0, 96, 12);</code>
        /// </summary>
        RGB96Float = 0x1b10600cu,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF32, (byte)ArrayOrder.BGR, 0, 96, 12);</code>
        /// </summary>
        BGR96Float = 0x1b40600cu,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF32, (byte)ArrayOrder.RGBA, 0, 128, 16);</code>
        /// </summary>
        RGBA128Float = 0x1b208010u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF32, (byte)ArrayOrder.ARGB, 0, 128, 16);</code>
        /// </summary>
        ARGB128Float = 0x1b308010u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF32, (byte)ArrayOrder.BGRA, 0, 128, 16);</code>
        /// </summary>
        BGRA128Float = 0x1b508010u,
            
        /// <summary>
        /// <code>DefinePixelFormat(PixelType.ArrayF32, (byte)ArrayOrder.ARGB, 0, 128, 16);</code>
        /// </summary>
        ABGR128Float = 0x1b608010u,

        /// <summary>
        /// <para>Planar mode: Y + V + U  (3 planes)</para>
        /// <code>DefinePixelFourCC('Y', 'V', '1', '2'),</code>
        /// </summary>
        YV12 = 0x32315659u,

        /// <summary>
        /// <para>Planar mode: Y + U + V  (3 planes)</para>
        /// <code>DefinePixelFourCC('I', 'Y', 'U', 'V'),</code>
        /// </summary>
        IYUV = 0x56555949u,
        
        /// <summary>
        /// <para>Packed mode: Y0+U0+Y1+V0 (1 plane)</para>
        /// <code>DefinePixelFourCC('Y', 'U', 'Y', '2'),</code>
        /// </summary>
        YUY2 = 0x32595559u, 
        
        /// <summary>
        /// <para>Packed mode: U0+Y0+V0+Y1 (1 plane)</para>
        /// <code>DefinePixelFourCC('U', 'Y', 'V', 'Y'),</code>
        /// </summary>
        UYVY = 0x59565955u,
        
        /// <summary>
        /// <para>Packed mode: Y0+V0+Y1+U0 (1 plane)</para>
        /// <code>DefinePixelFourCC('Y', 'V', 'Y', 'U'),</code>
        /// </summary>
        YVYU = 0x55595659u, 
        
        /// <summary>
        /// <para>Planar mode: Y + U/V interleaved  (2 planes)</para>
        /// <code>DefinePixelFourCC('N', 'V', '1', '2'),</code>
        /// </summary>
        NV12 = 0x3231564eu,
        
        /// <summary>
        /// <para>Planar mode: Y + V/U interleaved  (2 planes)</para>
        /// <code>DefinePixelFourCC('N', 'V', '2', '1'),</code>
        /// </summary>
        NV21 = 0x3132564eu,
        
        /// <summary>
        /// <para>Planar mode: Y + U/V interleaved  (2 planes)</para>
        /// <code>DefinePixelFourCC('P', '0', '1', '0'),</code>
        /// </summary>
        P010 = 0x30313050u,
        
        /// <summary>
        /// <para>Android video texture format</para>
        /// <code>DefinePixelFourCC('O', 'E', 'S', ' ')</code>
        /// </summary>
        ExternalOES = 0x2053454fu,
        
        /// <summary>
        /// <para>Motion JPEG</para>
        /// <code>DefinePixelFourCC('M', 'J', 'P', 'G')</code>
        /// </summary>
        MJPG = 0x47504a4du,
    }
}