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
    /// <code>typedef Uint32 (SDLCALL *SDL_TimerCallback)(void *userdata, SDL_TimerID timerID, Uint32 interval);</code>
    /// <summary>
    /// <para>The callback function is passed the current timer interval and returns the
    /// next timer interval, in milliseconds. If the returned value is the same as
    /// the one passed in, the periodic alarm continues, otherwise a new alarm is
    /// scheduled. If the callback returns 0, the periodic alarm is canceled and
    /// will be removed.</para>
    /// </summary>
    /// <param name="userdata">an arbitrary pointer provided by the app through
    /// <see cref="AddTimer"/>, for its own use.</param>
    /// <param name="timerId">the current timer being processed.</param>
    /// <param name="interval">the current callback time interval.</param>
    /// <returns>the new callback time interval, or 0 to disable further runs of
    /// the callback.</returns>
    /// <threadsafety>SDL may call this callback at any time from a background
    /// thread; the application is responsible for locking resources
    /// the callback touches that need to be protected.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="AddTimer"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TimerCallback(IntPtr userdata, uint timerId, uint interval);

    
    /// <code>typedef Uint64 (SDLCALL *SDL_NSTimerCallback)(void *userdata, SDL_TimerID timerID, Uint64 interval);</code>
    /// <summary>
    /// <para>Function prototype for the nanosecond timer callback function.</para>
    /// <para>The callback function is passed the current timer interval and returns the
    /// next timer interval, in nanoseconds. If the returned value is the same as
    /// the one passed in, the periodic alarm continues, otherwise a new alarm is
    /// scheduled. If the callback returns 0, the periodic alarm is canceled and
    /// will be removed.</para>
    /// </summary>
    /// <param name="userdata">an arbitrary pointer provided by the app through
    /// <see cref="AddTimer"/>, for its own use.</param>
    /// <param name="timerId">the current timer being processed.</param>
    /// <param name="interval">the current callback time interval.</param>
    /// <returns>the new callback time interval, or 0 to disable further runs of
    /// the callback.</returns>
    /// <threadsafety>SDL may call this callback at any time from a background
    /// thread; the application is responsible for locking resources
    /// the callback touches that need to be protected.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="AddTimerNS"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong NSTimerCallback(IntPtr userdata, uint timerId, ulong interval);
}