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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>The basic state for the system's power supply.</para>
    /// <para>These are results returned by <see cref="GetPowerInfo"/>.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum PowerState
    {
        /// <summary>
        /// error determining power status
        /// </summary>
        Error = -1,
        
        /// <summary>
        /// cannot determine power status
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Not plugged in, running on the battery
        /// </summary>
        OnBattery,
        
        /// <summary>
        /// Plugged in, no battery available
        /// </summary>
        NoBattery,
        
        /// <summary>
        /// Plugged in, charging battery
        /// </summary>
        Charging,
        
        /// <summary>
        /// Plugged in, battery charged
        /// </summary>
        Charged
    }
}