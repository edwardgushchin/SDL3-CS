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
    /// Raw keyboard button event structure (event.raw_key.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.1.8.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct RawKeyboardEvent
    {
        /// <summary>
        /// <see cref="EventType.RawKeyDown"/> or <see cref="EventType.RawKeyUp"/>
        /// </summary>
        public EventType Type;

        private UInt32 _reserved;

        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;

        /// <summary>
        /// The keyboard instance id
        /// </summary>
        public UInt32 Which;

        /// <summary>
        /// SDL physical key code
        /// </summary>
        public Scancode Scancode;

        /// <summary>
        /// The platform dependent scancode for this event
        /// </summary>
        public UInt16 Raw;

        /// <summary>
        /// true if the key is pressed
        /// </summary>
        public Byte Down;
    }
}