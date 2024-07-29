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
    /// Pressure-sensitive pen touched or stopped touching surface (event.ptip.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenTipEvent
    {
        /// <summary>
        /// <see cref="EventType.PenDown"/> or <see cref="EventType.PenUp"/>
        /// </summary>
        public EventType Type;
        private UInt32 Reserved;
        
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
        
        /// <summary>
        /// <see cref="PenTips.Ink"/> when using a regular pen tip, or <see cref="PenTips.Eraser"/> if the pen is
        /// being used as an eraser (e.g., flipped to use the eraser tip)
        /// </summary>
        public PenTips Tip;
        
        /// <summary>
        /// <see cref="Keystate.Pressed"/> on <see cref="EventType.PenDown"/> and
        /// <see cref="Keystate.Released"/> on <see cref="EventType.PenUp"/>
        /// </summary>
        public Keystate State;
        
        /// <summary>
        /// Pen button masks (where Button(1) is the first button, Button(2) is the second button etc.),
        /// <see cref="PenCapabilityFlags.Down"/> is set
        /// if the pen is touching the surface, and <see cref="PenCapabilityFlags.Eraser"/> is set if
        /// the pen is (used as) an eraser.
        /// </summary>
        /// <seealso cref="Button"/>
        public UInt16 PenState;
        
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public float X;
        
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public float Y;
        private unsafe fixed float axes[(int)PenAxis.NumAxes];
        
        /// <summary>
        /// Pen axes such as pressure and tilt (ordered as per <see cref="PenAxis"/>)
        /// </summary>
        public unsafe float[] Axes
        {
            get
            {
                fixed (float* ptr = axes)
                {
                    var intPtr = (IntPtr) ptr;
                    try
                    {
                        var array = new float[(int)PenAxis.NumAxes];
                        Marshal.Copy(intPtr, array, 0, (int)PenAxis.NumAxes);
                        return array;
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(intPtr);
                    }
                }
            }
        }
    }
}