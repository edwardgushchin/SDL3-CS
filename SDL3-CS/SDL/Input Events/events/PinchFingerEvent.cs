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
    /// Pinch event structure (event.pinch.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PinchFingerEvent
    {
        /// <summary>
        /// <see cref="EventType.PinchBegin"/> or <see cref="EventType.PinchUpdate"/> or <see cref="EventType.PinchEnd"/>
        /// </summary>
        public EventType Type;
        
        private UInt32 _reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The scale change since the last <see cref="EventType.PinchUpdate"/>. Scale &lt; 1 is "zoom out". Scale &gt; 1 is "zoom in".
        /// </summary>
        public float Scale;
        
        /// <summary>
        /// The window underneath the finger, if any
        /// </summary>
        public UInt32 WindowID;
    }
}