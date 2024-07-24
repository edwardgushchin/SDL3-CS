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
    /// <code>typedef struct SDL_IOStream SDL_IOStream;</code>
    /// <summary>
    /// <para>The read/write operation structure.</para>
    /// <para>This operates as an opaque handle. There are several APIs to create various
    /// types of I/O streams, or an app can supply an <see cref="IOStreamInterface"/> to
    /// <see cref="OpenIO"/> to provide their own stream implementation behind this
    /// struct's abstract interface.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    public class IOStream(IntPtr handle)
    {
        internal IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            if (obj is IOStream other) 
                return Handle == other.Handle;
            return false;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(IOStream? left, IOStream? right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            
            return left.Handle == right.Handle;
        }

        public static bool operator !=(IOStream? left, IOStream? right)
        {
            return !(left == right);
        }
    }
}