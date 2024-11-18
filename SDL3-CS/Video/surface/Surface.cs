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
    public struct SDLSurface(
        SurfaceFlags flags,
        PixelFormat format,
        int width,
        int height,
        int pitch,
        IntPtr pixels,
        IntPtr @internal)
    {
        /// <summary>
        /// Read-only
        /// </summary>
        public readonly SurfaceFlags Flags = flags;

        /// <summary>
        /// Read-only
        /// </summary>
        public readonly PixelFormat Format = format;

        /// <summary>
        /// Read-only
        /// </summary>
        public readonly int Width = width;

        /// <summary>
        /// Read-only
        /// </summary>
        public readonly int Height = height;

        /// <summary>
        /// Read-only
        /// </summary>
        public readonly int Pitch = pitch;

        /// <summary>
        /// Read-only pointer, writable pixels if non-NULL
        /// </summary>
        private IntPtr _pixels = pixels;

        /// <summary>
        /// Application reference count, used when freeing surface
        /// </summary>
        public int Refcount;

        /// <summary>
        /// Private
        /// </summary>
        private IntPtr _internal = @internal;

        /// <summary>
        /// Gets the pixels as a byte array.
        /// </summary>
        public byte[]? Pixels
        {
            get
            {
                if (_pixels == IntPtr.Zero) return null;
                var managedArray = new byte[Pitch * Height];
                Marshal.Copy(_pixels, managedArray, 0, managedArray.Length);
                return managedArray;
            }
        }
    }
    
    public class Surface(IntPtr handle)
    {
        internal IntPtr Handle { get; } = handle;

        public SDLSurface? GetSurfaceFromPtr()
        {
            return Marshal.PtrToStructure<SDLSurface>(Handle);
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is Render other) 
                return Handle == other.Handle;
            return false;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Surface? left, Surface? right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Surface? left, Surface? right)
        {
            return !(left == right);
        }
    }
}