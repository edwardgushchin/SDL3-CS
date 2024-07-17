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
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
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

using System.Collections;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GUID 
    {
        private unsafe fixed byte data[16];
        
        public unsafe byte[] Data
        {
            get
            {
                fixed (byte* ptr = data)
                {
                    var array = new byte[16];
                    var intPtr = (IntPtr) ptr;
                    Marshal.Copy(intPtr, array, 0, 16);
                    Free(intPtr);
                    return array;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is GUID other && StructuralComparisons.StructuralEqualityComparer.Equals(Data, other.Data);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }
        
        public static bool operator ==(GUID left, GUID right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GUID left, GUID right)
        {
            return !(left == right);
        }
    }
}