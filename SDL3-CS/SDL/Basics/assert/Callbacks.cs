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
    /// <code>typedef SDL_AssertState (SDLCALL *SDL_AssertionHandler)(const SDL_AssertData *data, void *userdata);</code>
    /// <summary>
    /// A callback that fires when an SDL assertion fails.
    /// </summary>
    /// <param name="data">a pointer to the <see cref="AssertData"/> structure corresponding to the
    /// current assertion.</param>
    /// <param name="userdata">what was passed as <c>userdata</c> to <see cref="SetAssertionHandler"/>.</param>
    /// <returns>an <see cref="AssertState"/> value indicating how to handle the failure.</returns>
    /// <threadsafety>This callback may be called from any thread that triggers an
    /// assert at any time.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AssertState AssertionHandler(in AssertData data, IntPtr userdata);
}