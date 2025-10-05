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

public partial class SDL
{
    /// <code>extern SDLMAIN_DECLSPEC SDL_AppResult SDLCALL SDL_AppInit(void **appstate, int argc, char *argv[]);</code>
    /// <summary>
    /// <para>App-implemented initial entry point for SDL_MAIN_USE_CALLBACKS apps.</para>
    /// <para>Apps implement this function when using SDL_MAIN_USE_CALLBACKS. If using a
    /// standard "main" function, you should not supply this.</para>
    /// <para>This function is called by SDL once, at startup. The function should
    /// initialize whatever is necessary, possibly create windows and open audio
    /// devices, etc. The <c>argc</c> and <c>argv</c> parameters work like they would with a
    /// standard "main" function.</para>
    /// <para>This function should not go into an infinite mainloop; it should do any
    /// one-time setup it requires and then return.</para>
    /// <para>The app may optionally assign a pointer to <c>appstate</c>. This pointer will
    /// be provided on every future call to the other entry points, to allow
    /// application state to be preserved between functions without the app needing
    /// to use a global variable. If this isn't set, the pointer will be <c>null</c> in
    /// future entry points.</para>
    /// <para>If this function returns <see cref="AppResult.Continue"/>, the app will proceed to normal
    /// operation, and will begin receiving repeated calls to <see cref="AppIterate"/> and
    /// <see cref="AppEvent"/> for the life of the program. If this function returns
    /// <see cref="AppResult.Failure"/>, SDL will call <see cref="AppQuit"/> and terminate the process with
    /// an exit code that reports an error to the platform. If it returns
    /// <see cref="AppResult.Success"/>, SDL calls <see cref="AppQuit"/> and terminates with an exit code
    /// that reports success to the platform.</para>
    /// <para>This function is called by SDL on the main thread.</para>
    /// </summary>
    /// <param name="appstate">a place where the app can optionally store a pointer for
    /// future use.</param>
    /// <param name="argc">the standard ANSI C main's argc; number of elements in <c>argv</c>.</param>
    /// <param name="argv">the standard ANSI C main's argv; array of command line
    /// arguments.</param>
    /// <returns><see cref="AppResult.Failure"/> to terminate with an error, <see cref="AppResult.Success"/> to
    /// terminate with success, <see cref="AppResult.Continue"/> to continue.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AppIterate"/>
    /// <seealso cref="AppEvent"/>
    /// <seealso cref="AppQuit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_main"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AppResult AppInit(IntPtr appstate, int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] argv);
    
    
    /// <code>extern SDLMAIN_DECLSPEC SDL_AppResult SDLCALL SDL_AppIterate(void *appstate);</code>
    /// <summary>
    /// <para>App-implemented iteration entry point for SDL_MAIN_USE_CALLBACKS apps.</para>
    /// <para>Apps implement this function when using SDL_MAIN_USE_CALLBACKS. If using a
    /// standard "main" function, you should not supply this.</para>
    /// <para>This function is called repeatedly by SDL after <see cref="AppInit"/> returns 0. The
    /// function should operate as a single iteration the program's primary loop;
    /// it should update whatever state it needs and draw a new frame of video,
    /// usually.</para>
    /// <para>On some platforms, this function will be called at the refresh rate of the
    /// display (which might change during the life of your app!). There are no
    /// promises made about what frequency this function might run at. You should
    /// use SDL's timer functions if you need to see how much time has passed since
    /// the last iteration.</para>
    /// <para>There is no need to process the SDL event queue during this function; SDL
    /// will send events as they arrive in <see cref="AppEvent"/>, and in most cases the
    /// event queue will be empty when this function runs anyhow.</para>
    /// <para>This function should not go into an infinite mainloop; it should do one
    /// iteration of whatever the program does and return.</para>
    /// <para>The <c>appstate</c> parameter is an optional pointer provided by the app during
    /// <see cref="AppInit"/>. If the app never provided a pointer, this will be <c>null</c>.</para>
    /// <para>If this function returns <see cref="AppResult.Continue"/>, the app will continue normal
    /// operation, receiving repeated calls to <see cref="AppIterate"/> and SDL_AppEvent for
    /// the life of the program. If this function returns <see cref="AppResult.Failure"/>, SDL will
    /// call <see cref="AppQuit"/> and terminate the process with an exit code that reports
    /// an error to the platform. If it returns <see cref="AppResult.Success"/>, SDL calls
    /// <see cref="AppQuit"/> and terminates with an exit code that reports success to the
    /// platform.</para>
    /// <para>This function is called by SDL on the main thread.</para>
    /// </summary>
    /// <param name="appstate">an optional pointer, provided by the app in <see cref="AppInit"/>.</param>
    /// <returns><see cref="AppResult.Failure"/> to terminate with an error, <see cref="AppResult.Success"/> to
    /// terminate with success, <see cref="AppResult.Continue"/> to continue.</returns>
    /// <threadsafety>This function may get called concurrently with <see cref="AppEvent"/>
    /// for events not pushed on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AppInit"/>
    /// <seealso cref="AppEvent"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AppIterate"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AppResult AppIterate(IntPtr appstate);
    
    
    //extern SDLMAIN_DECLSPEC SDL_AppResult SDLCALL SDL_AppEvent(void *appstate, SDL_Event *event);
    [DllImport(SDLLibrary, EntryPoint = "SDL_AppEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static extern AppResult AppEvent(IntPtr appstate, ref Event @event);
    
    
    /// <code>extern SDLMAIN_DECLSPEC void SDLCALL SDL_AppQuit(void *appstate, SDL_AppResult result);</code>
    /// <summary>
    /// <para>App-implemented deinit entry point for SDL_MAIN_USE_CALLBACKS apps.</para>
    /// <para>Apps implement this function when using SDL_MAIN_USE_CALLBACKS. If using a
    /// standard "main" function, you should not supply this.</para>
    /// <para>This function is called once by SDL before terminating the program.</para>
    /// <para>This function will be called in all cases, even if <see cref="AppInit"/> requests
    /// termination at startup.</para>
    /// <para>This function should not go into an infinite mainloop; it should
    /// deinitialize any resources necessary, perform whatever shutdown activities,
    /// and return.</para>
    /// <para>You do not need to call <see cref="Quit"/> in this function, as SDL will call it
    /// after this function returns and before the process terminates, but it is
    /// safe to do so.</para>
    /// <para>The <c>appstate</c> parameter is an optional pointer provided by the app during
    /// <see cref="AppInit"/>. If the app never provided a pointer, this will be <c>null</c>. This
    /// function call is the last time this pointer will be provided, so any
    /// resources to it should be cleaned up here.</para>
    /// <para>This function is called by SDL on the main thread.</para>
    /// </summary>
    /// <param name="appstate">an optional pointer, provided by the app in <see cref="AppInit"/>.</param>
    /// <param name="result">the result code that terminated the app (success or failure).</param>
    /// <threadsafety><see cref="AppEvent"/> may get called concurrently with this function
    /// if other threads that push events are still active.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AppInit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AppQuit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AppQuit(IntPtr appstate, AppResult result);
    
    
    /// <code>extern SDLMAIN_DECLSPEC int SDLCALL SDL_main(int argc, char *argv[]);</code>
    /// <summary>
    /// <para>An app-supplied function for program entry.</para>
    /// <para>Apps do not directly create this function; they should create a standard
    /// ANSI-C <c>main</c> function instead. If SDL needs to insert some startup code
    /// before <c>main</c> runs, or the platform doesn't actually _use_ a function
    /// called "main", SDL will do some macro magic to redefine <c>main</c> to
    /// Main and provide its own <c>main</c>.</para>
    /// <para>Apps should include <c>SDL_main.h</c> in the same file as their <c>main</c> function,
    /// and they should not use that symbol for anything else in that file, as it
    /// might get redefined.</para>
    /// <para>Program startup is a surprisingly complex topic. Please see
    /// [README/main-functions](README/main-functions), (or
    /// docs/README-main-functions.md in the source tree) for a more detailed
    /// explanation.</para>
    /// </summary>
    /// <param name="argc">an ANSI-C style main function's argc.</param>
    /// <param name="argv">an ANSI-C style main function's argv.</param>
    /// <returns>an ANSI-C main return code; generally 0 is considered successful
    /// program completion, and small non-zero values are considered
    /// errors.</returns>
    /// <threadsafety>This is the program entry point.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_main"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Main(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] argv);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetMainReady(void);</code>
    /// <summary>
    /// <para>Circumvent failure of <see cref="Init"/> when not using <see cref="Main"/> as an entry
    /// point.</para>
    /// <para>This function is defined in SDL_main.h, along with the preprocessor rule to
    /// redefine main() as <see cref="Main"/>. Thus to ensure that your main() function
    /// will not be changed it is necessary to define SDL_MAIN_HANDLED before
    /// including SDL.h.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Init"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetMainReady"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetMainReady();
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RunApp(int argc, char *argv[], SDL_main_func mainFunction, void *reserved);</code>
    /// <summary>
    /// <para>Initializes and launches an SDL application, by doing platform-specific
    /// initialization before calling your mainFunction and cleanups after it
    /// returns, if that is needed for a specific platform, otherwise it just calls
    /// mainFunction.</para>
    /// <para>You can use this if you want to use your own main() implementation without
    /// using <see cref="Main"/> (like when using SDL_MAIN_HANDLED). When using this, you do
    /// *not* need <see cref="SetMainReady"/>.</para>
    /// </summary>
    /// <param name="argc">the argc parameter from the application's main() function, or 0
    /// if the platform's main-equivalent has no argc.</param>
    /// <param name="argv">the argv parameter from the application's main() function, or
    /// <c>null</c> if the platform's main-equivalent has no argv.</param>
    /// <param name="mainFunction">your SDL app's C-style main(). NOT the function you're
    /// calling this from! Its name doesn't matter; it doesn't
    /// literally have to be <c>main</c>.</param>
    /// <param name="reserved">should be <c>null</c> (reserved for future use, will probably be
    /// platform-specific then).</param>
    /// <returns>the return value from mainFunction: 0 on success, otherwise
    /// failure; <see cref="GetError"/> might have more information on the
    /// failure.</returns>
    /// <threadsafety>Generally this is called once, near startup, from the
    /// process's initial thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RunApp"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RunApp(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] argv, MainFunc mainFunction, IntPtr reserved);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_EnterAppMainCallbacks(int argc, char *argv[], SDL_AppInit_func appinit, SDL_AppIterate_func appiter, SDL_AppEvent_func appevent, SDL_AppQuit_func appquit);</code>
    /// <summary>
    /// <para>An entry point for SDL's use in SDL_MAIN_USE_CALLBACKS.</para>
    /// <para>Generally, you should not call this function directly. This only exists to
    /// hand off work into SDL as soon as possible, where it has a lot more control
    /// and functionality available, and make the inline code in SDL_main.h as
    /// small as possible.</para>
    /// <para>Not all platforms use this, it's actual use is hidden in a magic
    /// header-only library, and you should not call this directly unless you
    /// _really_ know what you're doing.</para>
    /// </summary>
    /// <param name="argc">standard Unix main argc.</param>
    /// <param name="argv">standard Unix main argv.</param>
    /// <param name="appinit">the application's <see cref="AppInit"/> function.</param>
    /// <param name="appiter">the application's <see cref="AppIterate"/> function.</param>
    /// <param name="appevent">the application's <see cref="AppEvent"/> function.</param>
    /// <param name="appquit"> the application's <see cref="AppQuit"/> function.</param>
    /// <returns>standard Unix main return value.</returns>
    /// <threadsafety>It is not safe to call this anywhere except as the only
    /// function call in SDL_main.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EnterAppMainCallbacks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int EnterAppMainCallbacks(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] argv, AppInitFunc appinit, AppIterateFunc appiter, AppEventFunc appevent, AppQuitFunc appquit);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RegisterApp(const char *name, Uint32 style, void *hInst);</code>
    /// <summary>
    /// <para>Register a win32 window class for SDL's use.</para>
    /// <para>This can be called to set the application window class at startup. It is
    /// safe to call this multiple times, as long as every call is eventually
    /// paired with a call to SDL_UnregisterApp, but a second registration attempt
    /// while a previous registration is still active will be ignored, other than
    /// to increment a counter.</para>
    /// <para>Most applications do not need to, and should not, call this directly; SDL
    /// will call it when initializing the video subsystem.</para>
    /// <para>If <c>`name`</c> is <c>null</c>, SDL currently uses <c>`(CS_BYTEALIGNCLIENT | CS_OWNDC)`</c> for
    /// the style, regardless of what is specified here.</para>
    /// </summary>
    /// <param name="name">the window class name, in UTF-8 encoding. If <c>null</c>, SDL
    /// currently uses "SDL_app" but this isn't guaranteed.</param>
    /// <param name="style">the value to use in WNDCLASSEX::style.</param>
    /// <param name="hInst">the HINSTANCE to use in WNDCLASSEX::hInstance. If zero, SDL
    /// will use <c>GetModuleHandle(NULL)</c> instead.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "RegisterApp"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RegisterApp([MarshalAs(UnmanagedType.LPUTF8Str)] string? name, uint style, IntPtr hInst);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnregisterApp(void);</code>
    /// <summary>
    /// <para>Deregister the win32 window class from an <see cref="RegisterApp"/> call.</para>
    /// <para>This can be called to undo the effects of <see cref="RegisterApp"/>.</para>
    /// <para>Most applications do not need to, and should not, call this directly; SDL
    /// will call it when deinitializing the video subsystem.</para>
    /// <para>It is safe to call this multiple times, as long as every call is eventually
    /// paired with a prior call to <see cref="RegisterApp"/>. The window class will only be
    /// deregistered when the registration counter in <see cref="RegisterApp"/> decrements to
    /// zero through calls to this function.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnregisterApp"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnregisterApp();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GDKSuspendComplete(void);</code>
    /// <summary>
    /// <para>Callback from the application to let the suspend continue.</para>
    /// <para>This function is only needed for Xbox GDK support; all other platforms will
    /// do nothing and set an "unsupported" error message.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GDKSuspendComplete"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GDKSuspendComplete();
}