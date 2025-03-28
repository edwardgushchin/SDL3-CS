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
    /// <para>A structure specifying the parameters of a buffer.</para>
    /// <para>Usage flags can be bitwise OR'd together for combinations of usages. Note
    /// that certain combinations are invalid, for example VERTEX and INDEX.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUBuffer"/>
    /// <seealso cref="GPUBufferUsageFlags"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUBufferCreateInfo
    {
        /// <summary>
        /// How the buffer is intended to be used by the client.
        /// </summary>
        public GPUBufferUsageFlags Usage;
        
        /// <summary>
        /// The size in bytes of the buffer.
        /// </summary>
        public UInt32 Size;

        /// <summary>
        /// A properties ID for extensions. Should be 0 if no extensions are needed.
        /// </summary>
        public UInt32 Props;
    }
}