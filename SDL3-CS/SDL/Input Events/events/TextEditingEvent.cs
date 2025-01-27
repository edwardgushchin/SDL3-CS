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
    /// <para>Keyboard text editing event structure (event.edit.*)</para>
    /// <para>The start cursor is the position, in UTF-8 characters, where new typing
    /// will be inserted into the editing text. The length is the number of UTF-8
    /// characters that will be replaced by new typing.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingEvent
    {
        /// <summary>
        /// <see cref="EventType.TextEditing"/>
        /// </summary>
        public EventType Type;
        
        private UInt32 _reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public UInt32 WindowID;
        
        /// <summary>
        /// The editing text
        /// </summary>
        public IntPtr Text;
        
        /// <summary>
        /// The start cursor of selected editing text, or -1 if not set
        /// </summary>
        public Int32 Start;
        
        /// <summary>
        /// The length of selected editing text, or -1 if not set
        /// </summary>
        public Int32 Length;
    }
}