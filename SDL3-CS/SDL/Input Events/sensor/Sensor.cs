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
    /// In order to use these functions, <see cref="Init"/> must have been called
    /// with the <see cref="InitFlags.Sensor"/> flag.  This causes SDL to scan the system
    /// for sensors, and load appropriate drivers.
    /// </summary>
    /// <param name="handle"></param>
    public class Sensor(IntPtr handle)
    {
        internal IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            if (obj is Window other) 
                return Handle == other.Handle;
            return false;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Sensor? left, Sensor? right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Sensor? left, Sensor? right)
        {
            return !(left == right);
        }
    }
}