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
    /// <para>Information about an assertion failure.</para>
    /// <para>This structure is filled in with information about a triggered assertion,
    /// used by the assertion handler, then added to the assertion report. This is
    /// returned as a linked list from <see cref="GetAssertionReport"/>.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct AssertData
    {
        /// <summary>
        /// true if app should always continue when assertion is triggered.
        /// </summary>
        public bool AlwaysIgonre;

        /// <summary>
        /// Number of times this assertion has been triggered.
        /// </summary>
        public uint TriggerCount;

        /// <summary>
        /// A string of this assert's test code.
        /// </summary>
        public string Condition;

        /// <summary>
        /// The source file where this assert lives.
        /// </summary>
        public string Filename;

        /// <summary>
        /// The line in <c>filename</c> where this assert lives.
        /// </summary>
        public int Lineum;

        /// <summary>
        /// The name of the function where this assert lives.
        /// </summary>
        public string Function;

        /// <summary>
        /// next item in the linked list.
        /// </summary>
        public IntPtr Next;
    }
}