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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// An enum that describes the type of a pen device.
    /// <para>A <see cref="Direct"/> device is a pen that touches a graphic display (like an Apple
    /// Pencil on an iPad's screen). <see cref="Indirect"/> devices touch an external tablet
    /// surface that is connected to the machine but is not a display (like a
    /// lower-end Wacom tablet connected over USB).</para>
    /// <para>Apps may use this information to decide if they should draw a cursor; if
    /// the pen is touching the screen directly, a cursor doesn't make sense and
    /// can be in the way, but becomes necessary for indirect devices to know where
    /// on the display they are interacting.</para>
    /// </summary>
    /// /// <since>This enum is available since SDL 3.4.0.</since>
    public enum PenDeviceType
    {
        /// <summary>
        /// Not a valid pen device.
        /// </summary>
        Invalid = -1,
        
            
        /// <summary>
        /// Don't know specifics of this pen.
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Pen touches display.
        /// </summary>
        Direct, 
        
        /// <summary>
        /// Pen touches something that isn't the display.
        /// </summary>
        Indirect
    }
}