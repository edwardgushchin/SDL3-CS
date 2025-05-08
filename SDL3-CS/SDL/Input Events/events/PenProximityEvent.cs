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
    /// <para>Pressure-sensitive pen proximity event structure (event.pproximity.*)</para>
    /// <para>When a pen becomes visible to the system (it is close enough to a tablet,
    /// etc), SDL will send an <see cref="EventType.PenProximityIn"/> event with the new pen's
    /// ID. This ID is valid until the pen leaves proximity again (has been removed
    /// from the tablet's area, the tablet has been unplugged, etc). If the same
    /// pen reenters proximity again, it will be given a new ID.</para>
    /// <para>Note that "proximity" means "close enough for the tablet to know the tool
    /// is there." The pen touching and lifting off from the tablet while not
    /// leaving the area are handled by <see cref="EventType.PenDown"/> and <see cref="EventType.PenUp"/>.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenProximityEvent
    {
        /// <summary>
        /// <see cref="EventType.PenProximityIn"/> or <see cref="EventType.PenProximityOut"/>
        /// </summary>
        public EventType Type;

        private UInt32 _reserved;

        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;

        /// <summary>
        /// The window with pen focus, if any
        /// </summary>
        public UInt32 WindowID;

        /// <summary>
        /// The pen instance id
        /// </summary>
        public UInt32 Which;
    }
}