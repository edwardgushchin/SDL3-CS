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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef SDL_bool (SDLCALL *SDL_WindowsMessageHook)(void *userdata, MSG *msg);</code>
    /// <summary>
    /// <para>A callback to be used with SDL_SetWindowsMessageHook.</para>
    /// <para>This callback may modify the message, and should return <c>true</c> if the
    /// message should continue to be processed, or <c>false</c> to prevent further
    /// processing.</para>
    /// <para>As this is processing a message directly from the Windows event loop, this
    /// callback should do the minimum required work and return quickly.</para>
    /// </summary>
    /// <param name="userdata">the app-defined pointer provided to
    /// <see cref="SetWindowsMessageHook"/>.</param>
    /// <param name="msg">a pointer to a Win32 event structure to process.</param>
    /// <returns><c>true</c> to let event continue on, <c>false</c> to drop it.</returns>
    /// <threadsafety>This may only be called (by SDL) from the thread handling the
    /// Windows event loop.</threadsafety>
    /// <since>This datatype is available since SDL 3.0.0.</since>
    /// <seealso cref="SetWindowsMessageHook"/>
    /// <seealso cref="Hints.WindowsEnableMessageLoop"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(SDLBool)]
    public delegate bool WindowsMessageHook(IntPtr userdata, IntPtr msg);
    
    
    /// <code>typedef SDL_bool (SDLCALL *SDL_X11EventHook)(void *userdata, XEvent *xevent);</code>
    /// <summary>
    /// Platform specific functions for UNIX
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(SDLBool)]
    public delegate bool X11EventHook(IntPtr userdata, IntPtr xevent);
    
    
    // ReSharper disable once InconsistentNaming
    /// <code>typedef void (SDLCALL *SDL_iOSAnimationCallback)(void *userdata);</code>
    /// <summary>
    /// <para>The prototype for an Apple iOS animation callback.</para>
    /// <para>This datatype is only useful on Apple iOS.</para>
    /// <para>After passing a function pointer of this type to
    /// <see cref="SetIOSAnimationCallback"/>, the system will call that function pointer at
    /// a regular interval.</para>
    /// <param name="userdata">what was passed as <c>callbackParam</c> to
    /// <see cref="SetIOSAnimationCallback"/> as <c>callbackParam</c>.</param>
    /// </summary>
    /// <since>This datatype is available since SDL 3.0.0.</since>
    /// <seealso cref="SetIOSAnimationCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void IOSAnimationCallback(IntPtr userdata);
    
    
    /// <code>typedef void (SDLCALL *SDL_RequestAndroidPermissionCallback)(void *userdata, const char *permission, SDL_bool granted);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RequestAndroidPermissionCallback(IntPtr userdata,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string permission, [MarshalAs(SDLBool)] bool granted);
}