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
    /// Mouse raw motion and wheel event structure (event.maxis.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseRawAxisEvent
    {
        /// <summary>
        /// <see cref="EventType.MouseRawMotion"/> or <see cref="EventType.MouseRawScroll"/>
        /// </summary>
        public EventType Type;

        public UInt32 Reserved;

        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;

        /// <summary>
        /// The mouse instance id, SDL_TOUCH_MOUSEID, or SDL_PEN_MOUSEID
        /// </summary>
        public UInt32 Which;

        /// <summary>
        /// The axis delta value in the X direction
        /// </summary>
        public int Dx;

        /// <summary>
        /// The axis delta value in the Y direction
        /// </summary>
        public int Dy;

        /// <summary>
        /// The denominator unit in the X direction
        /// </summary>
        public float Ux;

        /// <summary>
        /// The denominator unit in the Y direction
        /// </summary>
        public float Uy;
    }
}