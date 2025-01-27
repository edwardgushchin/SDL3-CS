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
    /// <code>typedef void (SDLCALL *SDL_LogOutputFunction)(void *userdata, int category, SDL_LogPriority priority, const char *message);</code>
    /// <summary>
    /// The prototype for the log output callback function.
    /// </summary>
    /// <remarks>This function is called by SDL when there is new text to be logged. A mutex
    /// is held so that this function is never called by more than one thread at
    /// once.</remarks>
    /// <param name="userdata">what was passed as <c>userdata</c> to
    /// <see cref="SetLogOutputFunction"/>.</param>
    /// <param name="category">the category of the message.</param>
    /// <param name="priority">the priority of the message.</param>
    /// <param name="message">the message being output.</param>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogOutputFunction(IntPtr userdata, LogCategory category, LogPriority priority, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
}