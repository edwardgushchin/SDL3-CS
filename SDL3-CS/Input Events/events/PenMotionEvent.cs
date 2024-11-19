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
    /// <summary>
    /// Pressure-sensitive pen motion / pressure / angle event structure
    /// (event.pmotion.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenMotionEvent
    {
        /// <summary>
        /// <see cref="EventType.PenMotion"/>
        /// </summary>
        public EventType Type;
        private UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>()
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
        private byte Padding1;
        private byte Padding2;
        
        /// <summary>
        /// Pen button masks (where Button(1) is the first button, Button(2) is the second button etc.),
        /// <see cref="PenCapabilityFlags.Down"/> is set if the pen is touching the surface,
        /// and <see cref="PenCapabilityFlags.Eraser"/> is set if the pen is (used as) an eraser.
        /// </summary>
        /// <seealso cref="SDL.Button"/>
        public UInt16 PenState;
        
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public float X;
        
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public float Y;
        
        /// <summary>
        /// Pen axes such as pressure and tilt (ordered as per <see cref="PenAxis"/>)
        /// </summary>
        public unsafe fixed float Axes[(int)PenAxis.NumAxes];
    }
}