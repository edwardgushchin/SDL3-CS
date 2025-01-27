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
    /// <code>typedef bool (SDLCALL *SDL_WindowsMessageHook)(void *userdata, MSG *msg);</code>
    /// <summary>
    /// <para>A callback to be used with <see cref="SetWindowsMessageHook"/>.</para>
    /// <para>This callback may modify the message, and should return true if the message
    /// should continue to be processed, or false to prevent further processing.</para>
    /// <para>As this is processing a message directly from the Windows event loop, this
    /// callback should do the minimum required work and return quickly.</para>
    /// </summary>
    /// <param name="userdata">the app-defined pointer provided to
    /// <see cref="SetWindowsMessageHook"/>.</param>
    /// <param name="msg">a pointer to a Win32 event structure to process.</param>
    /// <returns><c>true</c> to let event continue on, <c>false</c> to drop it.</returns>
    /// <threadsafety>This may only be called (by SDL) from the thread handling the
    /// Windows event loop.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowsMessageHook"/>
    /// <seealso cref="Hints.WindowsEnableMessageLoop"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool WindowsMessageHook(IntPtr userdata, IntPtr msg);
    
    
    /// <code>typedef bool (SDLCALL *SDL_X11EventHook)(void *userdata, XEvent *xevent);</code>
    /// <summary>
    /// A callback to be used with <see cref="SetX11EventHook"/>.
    /// <para>This callback may modify the event, and should return true if the event
    /// should continue to be processed, or false to prevent further processing.</para>
    /// <para>As this is processing an event directly from the X11 event loop, this
    /// callback should do the minimum required work and return quickly.</para>
    /// </summary>
    /// <param name="userdata">the app-defined pointer provided to <see cref="SetX11EventHook"/>.</param>
    /// <param name="xevent">a pointer to an Xlib XEvent union to process.</param>
    /// <returns><c>true</c> to let event continue on, <c>false</c> to drop it.</returns>
    /// <threadsafety>This may only be called (by SDL) from the thread handling the
    /// X11 event loop.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <see cref="SetX11EventHook"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool X11EventHook(IntPtr userdata, IntPtr xevent);
    
    
    /// <code>typedef void (SDLCALL *SDL_iOSAnimationCallback)(void *userdata);</code>
    /// <summary>
    /// <para>The prototype for an Apple iOS animation callback.</para>
    /// <para>This datatype is only useful on Apple iOS.</para>
    /// <para>After passing a function pointer of this type to
    /// <see cref="SetiOSAnimationCallback"/>, the system will call that function pointer at
    /// a regular interval.</para>
    /// <param name="userdata">what was passed as <c>callbackParam</c> to
    /// <see cref="SetiOSAnimationCallback"/> as <c>callbackParam</c>.</param>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="SetiOSAnimationCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void IOSAnimationCallback(IntPtr userdata);
    
    
    /// <code>typedef void (SDLCALL *SDL_RequestAndroidPermissionCallback)(void *userdata, const char *permission, bool granted);</code>
    /// <summary>
    /// Callback that presents a response from a <see cref="RequestAndroidPermission"/> call.
    /// </summary>
    /// <param name="userdata">an app-controlled pointer that is passed to the callback.</param>
    /// <param name="permission">the Android-specific permission name that was requested.</param>
    /// <param name="granted">true if permission is granted, false if denied.</param>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="RequestAndroidPermission"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RequestAndroidPermissionCallback(IntPtr userdata,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string permission, [MarshalAs(UnmanagedType.I1)] bool granted);
}