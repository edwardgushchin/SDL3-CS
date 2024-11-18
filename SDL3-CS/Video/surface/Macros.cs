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
    /*/// <summary>
    /// Evaluates to true if the surface needs to be locked before access.
    /// </summary>
    /// <since>This macro is available since SDL 3.0.0.</since>
    public static bool MustLock(Surface surface)
    {
        const int sdlSurfaceLockNeeded = 0x00000001; // Assuming this constant based on SDL documentation
        var flags = Marshal.ReadInt32(surface.Handle); // Assuming the first 4 bytes represent the flags
        return (flags & sdlSurfaceLockNeeded) == sdlSurfaceLockNeeded;
    }*/
}