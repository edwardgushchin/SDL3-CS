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
    /// <code>typedef bool (SDLCALL *SDL_EventFilter)(void *userdata, SDL_Event *event);</code>
    /// <summary>
    /// A function pointer used for callbacks that watch the event queue.
    /// </summary>
    /// <param name="userdata">what was passed as <c>userdata</c> to <see cref="SetEventFilter"/> or
    /// <see cref="AddEventWatch"/>, etc.</param>
    /// <param name="event">the event that triggered the callback.</param>
    /// <returns>true to permit event to be added to the queue, and false to
    /// disallow it. When used with <see cref="AddEventWatch"/>, the return value is
    /// ignored.</returns>
    /// <threadsafety>SDL may call this callback at any time from any thread; the
    /// application is responsible for locking resources the callback
    /// touches that need to be protected.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="SetEventFilter"/>
    /// <seealso cref="AddEventWatch"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool EventFilter(IntPtr userdata, ref Event @event);
}