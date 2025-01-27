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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// A structure holding a calendar date and time broken down public into its
    /// components.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct DateTime
    {
        /// <summary>
        /// Year
        /// </summary>
        public int Year;
        
        /// <summary>
        /// Month [01-12]
        /// </summary>
        public int Month;
        
        /// <summary>
        /// Day of the month [01-31]
        /// </summary>
        public int Day;
        
        /// <summary>
        /// Hour [0-23]
        /// </summary>
        public int Hour;
        
        /// <summary>
        /// Minute [0-59]
        /// </summary>
        public int Minute;
        
        /// <summary>
        /// Seconds [0-60]
        /// </summary>
        public int Second;
        
        /// <summary>
        /// Nanoseconds [0-999999999]
        /// </summary>
        public int Nanosecond;
        
        /// <summary>
        /// Day of the week [0-6] (0 being Sunday)
        /// </summary>
        public int DayOfWeek;
        
        /// <summary>
        /// Seconds east of UTC
        /// </summary>
        public int UTCOffset;
    }
}