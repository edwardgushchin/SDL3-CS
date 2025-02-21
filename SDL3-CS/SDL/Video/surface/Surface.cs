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
    /// <para>A collection of pixels used in software blitting.</para>
    /// <para>Pixels are arranged in memory in rows, with the top row first. Each row
    /// occupies an amount of memory given by the pitch (sometimes known as the row
    /// stride in non-SDL APIs).</para>
    /// <para>Within each row, pixels are arranged from left to right until the width is
    /// reached. Each pixel occupies a number of bits appropriate for its format,
    /// with most formats representing each pixel as one or more whole bytes (in
    /// some indexed formats, instead multiple pixels are packed into each byte),
    /// and a byte order given by the format. After encoding all pixels, any
    /// remaining bytes to reach the pitch are used as padding to reach a desired
    /// alignment, and have undefined contents.</para>
    /// <para>When a surface holds YUV format data, the planes are assumed to be
    /// contiguous without padding between them, e.g. a 32x32 surface in NV12
    /// format with a pitch of 32 would consist of 32x32 bytes of Y plane followed
    /// by 32x16 bytes of UV plane.</para>
    /// <para>When a surface holds MJPG format data, pixels points at the compressed JPEG
    /// image and pitch is the length of that data.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="DestroySurface"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct Surface
    {
        /// <summary>
        /// RThe flags of the surface, read-only
        /// </summary>
        public SurfaceFlags Flags;

        /// <summary>
        /// The format of the surface, read-only
        /// </summary>
        public PixelFormat Format;

        /// <summary>
        /// The width of the surface, read-only.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the surface, read-only.
        /// </summary>
        public int Height;

        /// <summary>
        /// The distance in bytes between rows of pixels, read-only
        /// </summary>
        public int Pitch;

        /// <summary>
        /// A pointer to the pixels of the surface, the pixels are writeable if non-NULL
        /// </summary>
        public IntPtr Pixels;

        /// <summary>
        /// Application reference count, used when freeing surface
        /// </summary>
        public int Refcount;

        /// <summary>
        /// Reserved for internal use
        /// </summary>
        private IntPtr _internal;
    }
}