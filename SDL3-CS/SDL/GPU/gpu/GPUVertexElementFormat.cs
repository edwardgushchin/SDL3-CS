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
    /// Specifies the format of a vertex attribute.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUGraphicsPipeline"/>
    public enum GPUVertexElementFormat
    {
        Invalid,

        /* 32-bit Signed Integers */
        Int,
        Int2,
        Int3,
        Int4,

        /* 32-bit Unsigned Integers */
        Uint,
        Uint2,
        Uint3,
        Uint4,

        /* 32-bit Floats */
        Float,
        Float2,
        Float3,
        Float4,

        /* 8-bit Signed Integers */
        Byte2,
        Byte4,

        /* 8-bit Unsigned Integers */
        Ubyte2,
        Ubyte4,

        /* 8-bit Signed Normalized */
        Byte2Norm,
        Byte4Norm,

        /* 8-bit Unsigned Normalized */
        Ubyte2Norm,
        Ubyte4Norm,

        /* 16-bit Signed Integers */
        Short2,
        Short4,

        /* 16-bit Unsigned Integers */
        Ushort2,
        Ushort4,

        /* 16-bit Signed Normalized */
        Short2Norm,
        Short4Norm,

        /* 16-bit Unsigned Normalized */
        Ushort2Norm,
        Ushort4Norm,

        /* 16-bit Floats */
        Half2,
        Half4
    }
}