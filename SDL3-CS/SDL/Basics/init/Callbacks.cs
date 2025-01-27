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

public partial class SDL
{
    /// <code>typedef SDL_AppResult (SDLCALL *SDL_AppInit_func)(void **appstate, int argc, char *argv[]);</code>
    /// <summary>
    /// <para>Function pointer typedef for <see cref="AppInit"/>.</para>
    /// <para>These are used by <see cref="EnterAppMainCallbacks"/>. This mechanism operates behind
    /// the scenes for apps using the optional main callbacks. Apps that want to
    /// use this should just implement <see cref="AppInit"/> directly.</para>
    /// </summary>
    /// <param name="appstate">a place where the app can optionally store a pointer for
    /// future use.</param>
    /// <param name="argc">the standard ANSI C main's argc; number of elements in <c>argv</c>.</param>
    /// <param name="argv">the standard ANSI C main's argv; array of command line
    /// arguments.</param>
    /// <returns><see cref="AppResult.Failure"/> to terminate with an error, <see cref="AppResult.Success"/> to
    /// terminate with success, <see cref="AppResult.Continue"/> to continue.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AppResult AppInitFunc(IntPtr appstate, int argc, string[] argv);
    
    
    /// <code>typedef SDL_AppResult (SDLCALL *SDL_AppIterate_func)(void *appstate);</code>
    /// <summary>
    /// <para>Function pointer typedef for <see cref="AppIterate"/>.</para>
    /// <para>TThese are used by <see cref="EnterAppMainCallbacks"/>. This mechanism operates behind
    /// the scenes for apps using the optional main callbacks. Apps that want to
    /// use this should just implement <see cref="AppIterate"/> directly.</para>
    /// </summary>
    /// <param name="appstate">an optional pointer, provided by the app in <see cref="AppInit"/>.</param>
    /// <returns><see cref="AppResult.Failure"/> to terminate with an error, <see cref="AppResult.Success"/> to
    /// terminate with success, <see cref="AppResult.Continue"/> to continue.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AppResult AppIterateFunc(IntPtr appstate);
    
    
    /// <code>typedef SDL_AppResult (SDLCALL *SDL_AppEvent_func)(void *appstate, SDL_Event *event);</code>
    /// <summary>
    /// <para>Function pointer typedef for <see cref="AppEvent"/>.</para>
    /// <para>These are used by <see cref="EnterAppMainCallbacks"/>. This mechanism operates behind
    /// the scenes for apps using the optional main callbacks. Apps that want to
    /// use this should just implement <see cref="AppEvent"/> directly.</para>
    /// </summary>
    /// <param name="appstate">an optional pointer, provided by the app in SDL_AppInit.</param>
    /// <param name="event">the new event for the app to examine.</param>
    /// <returns><see cref="AppResult.Failure"/> to terminate with an error, <see cref="AppResult.Success"/> to
    /// terminate with success, <see cref="AppResult.Continue"/> to continue.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate AppResult AppEventFunc(IntPtr appstate, ref Event @event);
    
    
    /// <code>typedef void (SDLCALL *SDL_AppQuit_func)(void *appstate, SDL_AppResult result);</code>
    /// <summary>
    /// <para>Function pointer typedef for <see cref="AppQuit"/>.</para>
    /// <para>These are used by <see cref="EnterAppMainCallbacks"/>. This mechanism operates behind
    /// the scenes for apps using the optional main callbacks. Apps that want to
    /// use this should just implement <see cref="AppEvent"/> directly.</para>
    /// </summary>
    /// <param name="appstate">an optional pointer, provided by the app in <see cref="AppInit"/>.</param>
    /// <param name="result">the result code that terminated the app (success or failure).</param>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AppQuitFunc(IntPtr appstate, AppResult result);
    
    
    /// <code>typedef void (SDLCALL *SDL_MainThreadCallback)(void *userdata);</code>
    /// <summary>
    /// Callback run on the main thread.
    /// </summary>
    /// <param name="userdata">an app-controlled pointer that is passed to the callback.</param>
    /// <since>This datatype is available since SDL 3.1.8.</since>
    /// <seealso cref="RunOnMainThread"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MainThreadCallback(IntPtr userdata);
}