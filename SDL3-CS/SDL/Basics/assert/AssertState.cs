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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>Possible outcomes from a triggered assertion.</para>
    /// <para>When an enabled assertion triggers, it may call the assertion handler
    /// (possibly one provided by the app via <see cref="SetAssertionHandler"/>), which will
    /// return one of these values, possibly after asking the user.</para>
    /// <para>Then SDL will respond based on this outcome (loop around to retry the
    /// condition, try to break in a debugger, kill the program, or ignore the
    /// problem).</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum AssertState
    {
        /// <summary>
        /// Retry the assert immediately.
        /// </summary>
        Retry,
        
        /// <summary>
        /// Make the debugger trigger a breakpoint.
        /// </summary>
        Break,
        
        /// <summary>
        /// Terminate the program.
        /// </summary>
        Abort,
        
        /// <summary>
        /// Ignore the assert.
        /// </summary>
        Ignore,
        
        /// <summary>
        /// Ignore the assert from now on.
        /// </summary>
        AlwatsIgnore
    }
}