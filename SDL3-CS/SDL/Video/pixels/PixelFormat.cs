#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

namespace SDL3;

public static partial class SDL
{
    public enum PixelFormat : uint
    {
        Unknown = 0,
        Index1LSB = 0x11100100u,
        Index1MSB = 0x11200100u,
        Index2LSB = 0x1c100200u,
        Index2MSB = 0x1c200200u,
        Index4LSB = 0x12100400u,
        Index4MSB = 0x12200400u,
        Index8 = 0x13000801u,
        RGB332 = 0x14110801u,
        XRGB4444 = 0x15120c02u,
        XBGR4444 = 0x15520c02u,
        XRGB1555 = 0x15130f02u,
        XBGR1555 = 0x15530f02u,
        ARGB4444 = 0x15321002u,
        RGBA4444 = 0x15421002u,
        ABGR4444 = 0x15721002u,
        BGRA4444 = 0x15821002u,
        ARGB1555 = 0x15331002u,
        RGBA5551 = 0x15441002u,
        ABGR1555 = 0x15731002u,
        BGRA5551 = 0x15841002u,
        RGB565 = 0x15151002u,
        BGR565 = 0x15551002u,
        RGB24 = 0x17101803u,
        BGR24 = 0x17401803u,
        XRGB8888 = 0x16161804u,
        RGBX8888 = 0x16261804u,
        XBGR8888 = 0x16561804u,
        BGRX8888 = 0x16661804u,
        ARGB8888 = 0x16362004u,
        RGBA8888 = 0x16462004u,
        ABGR8888 = 0x16762004u,
        BGRA8888 = 0x16862004u,
        XRGB2101010 = 0x16172004u,
        XBGR2101010 = 0x16572004u,
        ARGB2101010 = 0x16372004u,
        ABGR2101010 = 0x16772004u,
        RGB48 = 0x18103006u,
        BGR48 = 0x18403006u,
        RGBA64 = 0x18204008u,
        ARGB64 = 0x18304008u,
        BGRA64 = 0x18504008u,
        ABGR64 = 0x18604008u,
        RGB48Float = 0x1a103006u,
        BGR48Float = 0x1a403006u,
        RGBA64Float = 0x1a204008u,
        ARGB64Float = 0x1a304008u,
        BGRA64Float = 0x1a504008u,
        ABGR64Float = 0x1a604008u,
        RGB96Float = 0x1b10600cu,
        BGR96Float = 0x1b40600cu,
        RGBA128Float = 0x1b208010u,
        ARGB128Float = 0x1b308010u,
        BGRA128Float = 0x1b508010u,
        ABGR128Float = 0x1b608010u,
        YV12 = 0x32315659u,
        IYUV = 0x56555949u,
        YUY2 = 0x32595559u,
        UYVY = 0x59565955u,
        YVYU = 0x55595659u,
        NV12 = 0x3231564eu,
        NV21 = 0x3132564eu,
        P010 = 0x30313050u,
        ExternalOES = 0x2053454fu
    }
}