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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Pen capabilities, as reported by <see cref="GetPenCapabilities"/>
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenCapabilityInfo
    {
        /// <summary>
        /// Physical maximum tilt angle, for XTILT and YTILT, or <see cref="PenInfoUnknown"/>.
        /// Pens cannot typically tilt all the way to 90 degrees, so this value is usually less than 90.0.
        /// </summary>
        public float MaxTilt;
        
        /// <summary>
        /// For Wacom devices: wacom tool type ID, otherwise 0 (useful e.g. with libwacom)
        /// </summary>
        public UInt32 WacomId;
        
        /// <summary>
        /// Number of pen buttons (not counting the pen tip), or <see cref="PenInfoUnknown"/>
        /// </summary>
        public sbyte NumButtons;
    }
}