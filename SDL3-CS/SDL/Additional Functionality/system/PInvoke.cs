#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetWindowsMessageHook(WindowsMessageHook callback, IntPtr userdata);
    ///<code>extern SDL_DECLSPEC void SDLCALL SDL_SetWindowsMessageHook(SDL_WindowsMessageHook callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback for every Windows message, run before TranslateMessage().</para>
    /// <para>The callback may modify the message, and should return <c>true</c> if the
    /// message should continue to be processed, or <c>false</c> to prevent further
    /// processing.</para>
    /// </summary>
    /// <param name="callback">the <see cref="WindowsMessageHook"/> function to call.</param>
    /// <param name="userdata">a pointer to pass to every iteration of <c>callback</c></param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="WindowsMessageHook"/>
    /// <seealso cref="Hints.WindowsEnableMessageLoop"/>
    public static void SetWindowsMessageHook(WindowsMessageHook callback, IntPtr userdata) =>
        SDL_SetWindowsMessageHook(callback, userdata);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDirect3D9AdapterIndex(uint displayID);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDirect3D9AdapterIndex(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the D3D9 adapter index that matches the specified display.</para>
    /// <para>The returned adapter index can be passed to <c>IDirect3D9::CreateDevice</c> and
    /// controls on which monitor a full screen application will appear.</para>
    /// </summary>
    /// <param name="displayID">the instance of the display to query.</param>
    /// <returns>the D3D9 adapter index on success or a negative error code on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetDirect3D9AdapterIndex(uint displayID) => SDL_GetDirect3D9AdapterIndex(displayID);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDXGIOutputInfo(uint displayID, out int adapterIndex, out int outputIndex);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDXGIOutputInfo(SDL_DisplayID displayID, int *adapterIndex, int *outputIndex);</code>
    /// <summary>
    /// <para>Get the DXGI Adapter and Output indices for the specified display.</para>
    /// <para>The DXGI Adapter and Output indices can be passed to <c>EnumAdapters</c> and
    /// <c>EnumOutputs</c> respectively to get the objects required to create a DX10 or
    /// DX11 device and swap chain.</para>
    /// </summary>
    /// <param name="displayID">the instance of the display to query.</param>
    /// <param name="adapterIndex">a pointer to be filled in with the adapter index.</param>
    /// <param name="outputIndex">a pointer to be filled in with the output index.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetDXGIOutputInfo(uint displayID, out int adapterIndex, out int outputIndex) =>
        SDL_GetDXGIOutputInfo(displayID, out adapterIndex, out outputIndex);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetX11EventHook(X11EventHook callback, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetX11EventHook(SDL_X11EventHook callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback for every X11 event.</para>
    /// <para>The callback may modify the event, and should return SDL_TRUE if the event
    /// should continue to be processed, or SDL_FALSE to prevent further
    /// processing.</para>
    /// </summary>
    /// <param name="callback">the <see cref="X11EventHook"/> function to call.</param>
    /// <param name="userdata">a pointer to pass to every iteration of <c>callback</c>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void SetX11EventHook(X11EventHook callback, IntPtr userdata) =>
        SDL_SetX11EventHook(callback, userdata);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetLinuxThreadPriority(long threadID, int priority);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetLinuxThreadPriority(Sint64 threadID, int priority);</code>
    /// <summary>
    /// <para>Sets the UNIX nice value for a thread.</para>
    /// <para>This uses setpriority() if possible, and RealtimeKit if available.</para>
    /// </summary>
    /// <param name="threadID">the Unix thread ID to change priority of.</param>
    /// <param name="priority">the new, Unix-specific, priority value.</param>
    /// <returns>0 on success, or -1 on error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int SetLinuxThreadPriority(long threadID, int priority) =>
        SDL_SetLinuxThreadPriority(threadID, priority);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetLinuxThreadPriorityAndPolicy(long threadID, int priority, int schedPolicy);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetLinuxThreadPriorityAndPolicy(Sint64 threadID, int sdlPriority, int schedPolicy);</code>
    /// <summary>
    /// <para>Sets the priority (not nice level) and scheduling policy for a thread.</para>
    /// <para>This uses setpriority() if possible, and RealtimeKit if available.</para>
    /// </summary>
    /// <param name="threadID">the Unix thread ID to change priority of.</param>
    /// <param name="priority">the new <see cref="ThreadPriority"/> value.</param>
    /// <param name="schedPolicy">the new scheduling policy (SCHED_FIFO, SCHED_RR,
    /// SCHED_OTHER, etc...).</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int SetLinuxThreadPriorityAndPolicy(long threadID, int priority, int schedPolicy) =>
        SDL_SetLinuxThreadPriorityAndPolicy(threadID, priority, schedPolicy);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetiOSAnimationCallback(IntPtr window, int interval, IOSAnimationCallback callback, 
        IntPtr callbackParam);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetiOSAnimationCallback(SDL_Window * window, int interval, SDL_iOSAnimationCallback callback, void *callbackParam);</code>
    /// <summary>
    /// <para>Use this function to set the animation callback on Apple iOS.</para>
    /// <para>The function prototype for <c>callback</c> is:</para>
    /// <para><c>void callback(void* callbackParam);</c></para>
    /// <para>Where its parameter, <c>callbackParam</c>, is what was passed as `callbackParam`
    /// to <see cref="SetiOSAnimationCallback"/>.</para>
    /// <para>This function is only available on Apple iOS.</para>
    /// <para>For more information see: https://wiki.libsdl.org/SDL3/README/ios</para>
    /// <para>Note that if you use the "main callbacks" instead of a standard C <c>main</c>
    /// function, you don't have to use this API, as SDL will manage this for you.</para>
    /// <para>Details on main callbacks are here: https://wiki.libsdl.org/SDL3/README/main-functions</para>
    /// </summary>
    /// <param name="window">the window for which the animation callback should be set.</param>
    /// <param name="interval">the number of frames after which <b>callback</b> will be
    /// called.</param>
    /// <param name="callback">the function to call for every frame.</param>
    /// <param name="callbackParam">a pointer that is passed to <c>callback</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetiOSEventPump"/>
    public static int SetiOSAnimationCallback(Window window, int interval, IOSAnimationCallback callback,
        IntPtr callbackParam) => SDL_SetiOSAnimationCallback(window.Handle, interval, callback, callbackParam);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetiOSEventPump([MarshalAs(SDLBool)] bool enabled);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetiOSEventPump(SDL_bool enabled);</code>
    /// <summary>
    /// <para>Use this function to enable or disable the SDL event pump on Apple iOS.</para>
    /// <para>This function is only available on Apple iOS.</para>
    /// </summary>
    /// <param name="enabled"><c>true</c> to enable the event pump, SDL_FALSE to disable it.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetiOSAnimationCallback"/>
    public static void SetiOSEventPump(bool enabled) => SDL_SetiOSEventPump(enabled);
}