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
    /// Raw mouse wheel event structure (event.raw_wheel.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.1.3.</since>
    [StructLayout(LayoutKind.Sequential)]
    public class RawMouseWheelEvent
    {
        /// <summary>
        /// <see cref="EventType.RawMouseWheel"/>
        /// </summary>
        public EventType Type;
        
        private UInt32 _reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp; 
        
        /// <summary>
        /// The mouse instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// X axis delta, positive to the right and negative to the left
        /// </summary>
        public int Dx;
        
        /// <summary>
        /// Y axis delta, positive away from the user and negative toward the user
        /// </summary>
        public int Dy;
        
        /// <summary>
        /// X value scale to convert to logical scroll units
        /// </summary>
        public float ScaleX;
        
        /// <summary>
        /// Y value scale to convert to logical scroll units
        /// </summary>
        public float ScaleY;
    }
}