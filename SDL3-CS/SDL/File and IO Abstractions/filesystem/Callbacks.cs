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
    /// <code>typedef SDL_EnumerationResult (SDLCALL *SDL_EnumerateDirectoryCallback)(void *userdata, const char *dirname, const char *fname);</code>
    /// <summary>
    /// <para>Callback for directory enumeration.</para>
    /// <para>Enumeration of directory entries will continue until either all entries
    /// have been provided to the callback, or the callback has requested a stop
    /// through its return value.</para>
    /// <para>Returning <see cref="EnumerationResult.Continue"/> will let enumeration proceed, calling the
    /// callback with further entries. <see cref="EnumerationResult.Success"/> and <see cref="EnumerationResult.Failure"/> will
    /// terminate the enumeration early, and dictate the return value of the
    /// enumeration function itself.</para>
    /// <para><c>dirname</c> is guaranteed to end with a path separator (<c>\\</c> on
    /// Windows, <c>/</c> on most other platforms).</para>
    /// </summary>
    /// <param name="userdata">an app-controlled pointer that is passed to the callback.</param>
    /// <param name="dirname">the directory that is being enumerated.</param>
    /// <param name="fname">the next entry in the enumeration.</param>
    /// <returns>how the enumeration should proceed.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="EnumerateDirectory"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate EnumerationResult EnumerateDirectoryCallback(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string dirname, [MarshalAs(UnmanagedType.LPUTF8Str)] string fname);

}