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
    /// Keyboard IME candidates event structure (event.edit_candidates.*)
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingCandidatesEvent
    {
        /// <summary>
        /// <see cref="EventType.TextEditingCandidates"/>
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
        /// The list of candidates, or <c>null</c> if there are no candidates available
        /// </summary>
        public IntPtr Candidates;
        
        /// <summary>
        /// The number of strings in <c>candidates</c>
        /// </summary>
        public Int32 NumCandidates;
        
        /// <summary>
        /// The index of the selected candidate, or -1 if no candidate is selected
        /// </summary>
        public Int32 SelectedCandidate;
        
        /// <summary>
        /// true if the list is horizontal, false if it's vertical
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] public bool Horizontal;

        private Byte _padding1;

        private Byte _padding2;

        private Byte _padding3;
    }
}