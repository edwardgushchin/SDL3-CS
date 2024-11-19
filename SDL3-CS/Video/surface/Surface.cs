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
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct Surface
    {
        /// <summary>
        /// Read-only
        /// </summary>
        public SurfaceFlags Flags;

        /// <summary>
        /// Read-only
        /// </summary>
        public PixelFormat Format;

        /// <summary>
        /// Read-only
        /// </summary>
        public int Width;

        /// <summary>
        /// Read-only
        /// </summary>
        public int Height;

        /// <summary>
        /// Read-only
        /// </summary>
        public int Pitch;

        /// <summary>
        /// Read-only pointer, writable pixels if non-NULL
        /// </summary>
        public IntPtr Pixels;

        /// <summary>
        /// Application reference count, used when freeing surface
        /// </summary>
        public int Refcount;

        /// <summary>
        /// Private
        /// </summary>
        private IntPtr _internal;
        
        /// <summary>
        /// Retrieves the pixel data from the surface as a managed byte array.
        /// </summary>
        /// <returns>
        /// A byte array containing the pixel data, or null if the surface has no pixels (i.e., if <see cref="Pixels"/> is <c>null</c>).
        /// The byte array will have a size of <c>Pitch * Height</c> and contain the raw pixel data in the specified format.
        /// </returns>
        /// <remarks>
        /// This method copies the raw pixel data from the unmanaged memory pointed to by <see cref="Pixels"/> into a managed byte array.
        /// The resulting array represents the pixel data as it is stored in memory, taking into account the pitch (row stride) of the surface.
        /// Use this method if you need to work with the pixel data in managed code or for processing the pixels outside of the SDL context.
        /// </remarks>
        public byte[]? GetManagedPixels()
        {
            if (Pixels == IntPtr.Zero) return null;
            var managedArray = new byte[Pitch * Height];
            Marshal.Copy(Pixels, managedArray, 0, managedArray.Length);
            return managedArray;
        }
    }
}