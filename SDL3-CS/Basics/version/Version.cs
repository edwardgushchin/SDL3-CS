﻿using System.Runtime.InteropServices;

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
    [StructLayout(LayoutKind.Sequential)]
    public struct Version
    {
        public byte Major;
        public byte Minor;
        public byte Patch;
        
        public static bool operator ==(Version v1, Version v2)
        {
            return v1.Major == v2.Major && v1.Minor == v2.Minor && v1.Patch == v2.Patch;
        }
        
        public static bool operator !=(Version v1, Version v2)
        {
            return !(v1 == v2);
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is not Version version) return false;
            return this == version;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Patch);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }
    }
}