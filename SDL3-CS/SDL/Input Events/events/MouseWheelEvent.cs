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
    /// Mouse wheel event structure (event.wheel.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        /// <summary>
        /// <see cref="EventType.MouseWheel"/>
        /// </summary>
        public EventType Type;
        
        private UInt32 _reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public UInt32 WindowID;
        
        /// <summary>
        /// The mouse instance id in relative mode or 0
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The amount scrolled horizontally, positive to the right and negative to the left
        /// </summary>
        public float X;
        
        /// <summary>
        /// The amount scrolled vertically, positive away from the user and negative toward the user
        /// </summary>
        public float Y;
        
        /// <summary>
        /// Set to one of the SDL_MOUSEWHEEL_* defines. When FLIPPED the values in X and Y will be opposite. Multiply by -1 to change them back
        /// </summary>
        public MouseWheelDirection Direction;
        
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public float MouseX;
        
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public float MouseY;
        
        /// <summary>
        /// The amount scrolled horizontally, accumulated to whole scroll "ticks" (added in 3.2.12)
        /// </summary>
        public int IntegerX;
        
        /// <summary>
        /// The amount scrolled vertically, accumulated to whole scroll "ticks" (added in 3.2.12)
        /// </summary>
        public int IntegerY;
    }
}