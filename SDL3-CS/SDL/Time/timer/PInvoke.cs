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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetTicks(void);</code>
    /// <summary>
    /// Get the number of milliseconds that have elapsed since the SDL library
    /// initialization.
    /// </summary>
    /// <returns>n unsigned 64‑bit integer that represents the number of
    /// milliseconds that have elapsed since the SDL library was
    /// initialized (typically via a call to SDL_Init).</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTicksNS"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTicks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetTicks();
    
    
    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetTicksNS(void);</code>
    /// <summary>
    /// Get the number of nanoseconds since SDL library initialization.
    /// </summary>
    /// <returns>an unsigned 64-bit value representing the number of nanoseconds
    /// since the SDL library initialized.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTicksNS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetTicksNS();
    
    
    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetPerformanceCounter(void);</code>
    /// <summary>
    /// <para>Get the current value of the high resolution counter.</para>
    /// <para>This function is typically used for profiling.</para>
    /// <para>The counter values are only meaningful relative to each other. Differences
    /// between values can be converted to times by using
    /// <see cref="GetPerformanceFrequency"/>.</para>
    /// </summary>
    /// <returns>the current counter value.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPerformanceFrequency"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPerformanceCounter"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetPerformanceCounter();


    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetPerformanceFrequency(void);</code>
    /// <summary>
    /// Get the count per second of the high resolution counter.
    /// </summary>
    /// <returns>a platform-specific count per second.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPerformanceCounter"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPerformanceFrequency"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetPerformanceFrequency();


    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Delay(Uint32 ms);</code>
    /// <summary>
    /// <para>Wait a specified number of milliseconds before returning.</para>
    /// <para>This function waits a specified number of milliseconds before returning. It
    /// waits at least the specified time, but possibly longer due to OS
    /// scheduling.</para>
    /// </summary>
    /// <param name="ms">the number of milliseconds to delay.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DelayNS"/>
    /// <seealso cref="DelayPrecise"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Delay"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Delay(uint ms);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DelayNS(Uint64 ns);</code>
    /// <summary>
    /// <para>Wait a specified number of nanoseconds before returning.</para>
    /// <para>This function waits a specified number of nanoseconds before returning. It
    /// waits at least the specified time, but possibly longer due to OS
    /// scheduling.</para>
    /// </summary>
    /// <param name="ns">the number of nanoseconds to delay.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Delay"/>
    /// <seealso cref="DelayPrecise"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DelayNS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DelayNS(ulong ns);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DelayPrecise(Uint64 ns);</code>
    /// <summary>
    /// <para>Wait a specified number of nanoseconds before returning.</para>
    /// <para>This function waits a specified number of nanoseconds before returning. It
    /// will attempt to wait as close to the requested time as possible, busy
    /// waiting if necessary, but could return later due to OS scheduling.</para>
    /// </summary>
    /// <param name="ns">the number of nanoseconds to delay.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.6.</since>
    /// <seealso cref="Delay"/>
    /// <seealso cref="DelayNS"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DelayPrecise"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DelayPrecise(ulong ns);


    /// <code>extern SDL_DECLSPEC SDL_TimerID SDLCALL SDL_AddTimer(Uint32 interval, SDL_TimerCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Call a callback function at a future time.</para>
    /// <para>The callback function is passed the current timer interval and the user
    /// supplied parameter from the <see cref="AddTimer"/> call and should return the next
    /// timer interval. If the value returned from the callback is 0, the timer is
    /// canceled and will be removed.</para>
    /// <para>The callback is run on a separate thread, and for short timeouts can
    /// potentially be called before this function returns.</para>
    /// <para>Timers take into account the amount of time it took to execute the
    /// callback. For example, if the callback took 250 ms to execute and returned
    /// 1000 (ms), the timer would only wait another 750 ms before its next
    /// iteration.</para>
    /// <para>Timing may be inexact due to OS scheduling. Be sure to note the current
    /// time with <see cref="GetTicksNS"/> or <see cref="GetPerformanceCounter"/> in case your
    /// callback needs to adjust for variances.</para>
    /// </summary>
    /// <param name="interval">the timer delay, in milliseconds, passed to <c>callback</c>.</param>
    /// <param name="callback">the <see cref="TimerCallback"/> function to call when the specified
    /// <c>interval</c> elapses.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <returns>a timer ID or 0 on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddTimerNS"/>
    /// <seealso cref="RemoveTimer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddTimer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint AddTimer(uint interval, TimerCallback callback, IntPtr userdata);


    /// <code>extern SDL_DECLSPEC SDL_TimerID SDLCALL SDL_AddTimerNS(Uint64 interval, SDL_NSTimerCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Call a callback function at a future time.</para>
    /// <para>The callback function is passed the current timer interval and the user
    /// supplied parameter from the <see cref="AddTimerNS"/> call and should return the
    /// next timer interval. If the value returned from the callback is 0, the
    /// timer is canceled and will be removed.</para>
    /// <para>The callback is run on a separate thread, and for short timeouts can
    /// potentially be called before this function returns.</para>
    /// <para>Timers take into account the amount of time it took to execute the
    /// callback. For example, if the callback took 250 ns to execute and returned
    /// 1000 (ns), the timer would only wait another 750 ns before its next
    /// iteration.</para>
    /// <para>Timing may be inexact due to OS scheduling. Be sure to note the current
    /// time with <see cref="GetTicksNS"/> or <see cref="GetPerformanceCounter"/> in case your
    /// callback needs to adjust for variances.</para>
    /// </summary>
    /// <param name="interval">the timer delay, in nanoseconds, passed to <c>callback</c>.</param>
    /// <param name="callback">the <see cref="TimerCallback"/> function to call when the specified
    /// <c>interval</c> elapses.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <returns>a timer ID or 0 on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddTimer"/>
    /// <seealso cref="RemoveTimer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddTimerNS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint AddTimerNS(ulong interval, NSTimerCallback callback, IntPtr userdata);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RemoveTimer(SDL_TimerID id);</code>
    /// <summary>
    /// Remove a timer created with <see cref="AddTimer"/>.
    /// </summary>
    /// <param name="id">the ID of the timer to remove.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddTimer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveTimer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RemoveTimer(uint id);
}