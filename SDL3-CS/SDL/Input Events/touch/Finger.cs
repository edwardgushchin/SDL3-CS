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
    /// <para>Data about a single finger in a multitouch event.</para>
    /// <para>Each touch event is a collection of fingers that are simultaneously in
    /// contact with the touch device (so a "touch" can be a "multitouch," in
    /// reality), and this struct reports details of the specific fingers.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GetTouchFingers"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct Finger
    {
        /// <summary>
        /// the finger ID
        /// </summary>
        public UInt64 ID;
        
        /// <summary>
        /// the x-axis location of the touch event, normalized (0...1)
        /// </summary>
        public float X;
        
        /// <summary>
        /// the y-axis location of the touch event, normalized (0...1)
        /// </summary>
        public float Y;
        
        /// <summary>
        /// the quantity of pressure applied, normalized (0...1)
        /// </summary>
        public float Pressure;
    }
}