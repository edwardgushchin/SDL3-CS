#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef void (SDLCALL *SDL_LogOutputFunction)(void *userdata, int category, SDL_LogPriority priority, const char *message);</code>
    /// <summary>
    /// <para>The prototype for the log output callback function.</para>
    /// <para>The prototype for the log output callback function.</para>
    /// </summary>
    /// <remarks>This function is called by SDL when there is new text to be logged.</remarks>
    /// <param name="userdata">what was passed as <c>userdata</c> to <see cref="SetLogOutputFunction"/>.</param>
    /// <param name="category">the category of the message.</param>
    /// <param name="priority">the priority of the message.</param>
    /// <param name="message">the message being output.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogOutputFunction(IntPtr userdata, LogCategory category, LogPriority priority, string message);
}