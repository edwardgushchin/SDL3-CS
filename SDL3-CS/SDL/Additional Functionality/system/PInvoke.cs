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
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetWindowsMessageHook(SDL_WindowsMessageHook callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback for every Windows message, run before TranslateMessage().</para>
    /// <para>The callback may modify the message, and should return true if the message
    /// should continue to be processed, or false to prevent further processing.</para>
    /// </summary>
    /// <param name="callback">the <see cref="WindowsMessageHook"/> function to call.</param>
    /// <param name="userdata">a pointer to pass to every iteration of <c>callback</c>.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowsMessageHook"/>
    /// <seealso cref="Hints.WindowsEnableMessageLoop"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowsMessageHook"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowsMessageHook(WindowsMessageHook callback, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDirect3D9AdapterIndex(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the D3D9 adapter index that matches the specified display.</para>
    /// <para>The returned adapter index can be passed to <c>IDirect3D9::CreateDevice</c> and
    /// controls on which monitor a full screen application will appear.</para>
    /// </summary>
    /// <param name="displayID">the instance of the display to query.</param>
    /// <returns>the D3D9 adapter index on success or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDirect3D9AdapterIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDirect3D9AdapterIndex(uint displayID);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetDXGIOutputInfo(SDL_DisplayID displayID, int *adapterIndex, int *outputIndex);</code>
    /// <summary>
    /// <para>Get the DXGI Adapter and Output indices for the specified display.</para>
    /// <para>The DXGI Adapter and Output indices can be passed to <c>EnumAdapters</c> and
    /// <c>EnumOutputs</c> respectively to get the objects required to create a DX10 or
    /// DX11 device and swap chain.</para>
    /// </summary>
    /// <param name="displayID">the instance of the display to query.</param>
    /// <param name="adapterIndex">a pointer to be filled in with the adapter index.</param>
    /// <param name="outputIndex">a pointer to be filled in with the output index.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDXGIOutputInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetDXGIOutputInfo(uint displayID, out int adapterIndex, out int outputIndex);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetX11EventHook(SDL_X11EventHook callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback for every X11 event.</para>
    /// <para>The callback may modify the event, and should return true if the event
    /// should continue to be processed, or false to prevent further processing.</para>
    /// </summary>
    /// <param name="callback">the <see cref="X11EventHook"/> function to call.</param>
    /// <param name="userdata">a pointer to pass to every iteration of <c>callback</c>.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetX11EventHook"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetX11EventHook(X11EventHook callback, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetLinuxThreadPriority(Sint64 threadID, int priority);</code>
    /// <summary>
    /// <para>Sets the UNIX nice value for a thread.</para>
    /// <para>This uses setpriority() if possible, and RealtimeKit if available.</para>
    /// </summary>
    /// <param name="threadID">the Unix thread ID to change priority of.</param>
    /// <param name="priority">the new, Unix-specific, priority value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetLinuxThreadPriority"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetLinuxThreadPriority(long threadID, int priority);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetLinuxThreadPriorityAndPolicy(Sint64 threadID, int sdlPriority, int schedPolicy);</code>
    /// <summary>
    /// <para>Sets the priority (not nice level) and scheduling policy for a thread.</para>
    /// <para>This uses setpriority() if possible, and RealtimeKit if available.</para>
    /// </summary>
    /// <param name="threadID">the Unix thread ID to change priority of.</param>
    /// <param name="priority">the new <see cref="ThreadPriority"/> value.</param>
    /// <param name="schedPolicy">the new scheduling policy (SCHED_FIFO, SCHED_RR,
    /// SCHED_OTHER, etc...).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetLinuxThreadPriorityAndPolicy"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetLinuxThreadPriorityAndPolicy(long threadID, int priority, int schedPolicy);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetiOSAnimationCallback(SDL_Window *window, int interval, SDL_iOSAnimationCallback callback, void *callbackParam);</code>
    /// <summary>
    /// <para>Use this function to set the animation callback on Apple iOS.</para>
    /// <para>The function prototype for <c>callback</c> is:</para>
    /// <para><code>void callback(void *callbackParam);</code></para>
    /// <para>Where its parameter, <c>callbackParam</c>, is what was passed as <c>callbackParam</c>
    /// to <see cref="SetiOSAnimationCallback"/>.</para>
    /// <para>This function is only available on Apple iOS.</para>
    /// <para>For more information see:</para>
    /// <para>https://wiki.libsdl.org/SDL3/README/ios</para>
    /// <para>Note that if you use the "main callbacks" instead of a standard C <c>main</c>
    /// function, you don't have to use this API, as SDL will manage this for you.</para>
    /// <para>Details on main callbacks are here:</para>
    /// <para>https://wiki.libsdl.org/SDL3/README/main-functions</para>
    /// </summary>
    /// <param name="window">the window for which the animation callback should be set.</param>
    /// <param name="interval">the number of frames after which <b>callback</b> will be
    /// called.</param>
    /// <param name="callback">the function to call for every frame.</param>
    /// <param name="callbackParam">a pointer that is passed to <c>callback</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetiOSEventPump"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetiOSAnimationCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetiOSAnimationCallback(IntPtr window, int interval, IOSAnimationCallback callback, IntPtr callbackParam);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetiOSEventPump(bool enabled);</code>
    /// <summary>
    /// <para>Use this function to enable or disable the SDL event pump on Apple iOS.</para>
    /// <para>This function is only available on Apple iOS.</para>
    /// </summary>
    /// <param name="enabled"></param>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetiOSEventPump"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetiOSEventPump([MarshalAs(UnmanagedType.I1)] bool enabled);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetAndroidJNIEnv(void);</code>
    /// <summary>
    /// <para>Get the Android Java Native Interface Environment of the current thread.</para>
    /// <para>This is the JNIEnv one needs to access the Java virtual machine from native
    /// code, and is needed for many Android APIs to be usable from C.</para>
    /// <para>The prototype of the function in SDL's code actually declare a void* return
    /// type, even if the implementation returns a pointer to a JNIEnv. The
    /// rationale being that the SDL headers can avoid including jni.h.</para>
    /// </summary>
    /// <returns>a pointer to Java native interface object (JNIEnv) to which the
    /// current thread is attached, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAndroidActivity"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidJNIEnv"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetAndroidJNIEnv();
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetAndroidActivity(void);</code>
    /// <summary>
    /// <para>Retrieve the Java instance of the Android activity class.</para>
    /// <para>The prototype of the function in SDL's code actually declares a void*
    /// return type, even if the implementation returns a jobject. The rationale
    /// being that the SDL headers can avoid including jni.h.</para>
    /// <para>The jobject returned by the function is a local reference and must be
    /// released by the caller. See the PushLocalFrame() and PopLocalFrame() or
    /// DeleteLocalRef() functions of the Java native interface:</para>
    /// <para>https://docs.oracle.com/javase/1.5.0/docs/guide/jni/spec/functions.html</para>
    /// </summary>
    /// <returns>the jobject representing the instance of the Activity class of the
    /// Android application, or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAndroidJNIEnv"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidActivity"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetAndroidActivity();

    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetAndroidSDKVersion(void);</code>
    /// <summary>
    /// <para>Query Android API level of the current device.</para>
    /// <list type="bullet">
    /// <item>API level 35: Android 15 (VANILLA_ICE_CREAM)</item>
    /// <item>API level 34: Android 14 (UPSIDE_DOWN_CAKE)</item>
    /// <item>API level 33: Android 13 (TIRAMISU)</item>
    /// <item>API level 32: Android 12L (S_V2)</item>
    /// <item>API level 31: Android 12 (S)</item>
    /// <item>API level 30: Android 11 (R)</item>
    /// <item>API level 29: Android 10 (Q)</item>
    /// <item>API level 28: Android 9 (P)</item>
    /// <item>API level 27: Android 8.1 (O_MR1)</item>
    /// <item>API level 26: Android 8.0 (O)</item>
    /// <item>API level 25: Android 7.1 (N_MR1)</item>
    /// <item>API level 24: Android 7.0 (N)</item>
    /// <item>API level 23: Android 6.0 (M)</item>
    /// <item>API level 22: Android 5.1 (LOLLIPOP_MR1)</item>
    /// <item>API level 21: Android 5.0 (LOLLIPOP, L)</item>
    /// <item>API level 20: Android 4.4W (KITKAT_WATCH)</item>
    /// <item>API level 19: Android 4.4 (KITKAT)</item>
    /// <item>API level 18: Android 4.3 (JELLY_BEAN_MR2)</item>
    /// <item>API level 17: Android 4.2 (JELLY_BEAN_MR1)</item>
    /// <item>API level 16: Android 4.1 (JELLY_BEAN)</item>
    /// <item>API level 15: Android 4.0.3 (ICE_CREAM_SANDWICH_MR1)</item>
    /// <item>API level 14: Android 4.0 (ICE_CREAM_SANDWICH)</item>
    /// <item>API level 13: Android 3.2 (HONEYCOMB_MR2)</item>
    /// <item>API level 12: Android 3.1 (HONEYCOMB_MR1)</item>
    /// <item>API level 11: Android 3.0 (HONEYCOMB)</item>
    /// <item>API level 10: Android 2.3.3 (GINGERBREAD_MR1)</item>
    /// </list>
    /// </summary>
    /// <returns>the Android API level.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidSDKVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAndroidSDKVersion();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsChromebook(void);</code>
    /// <summary>
    /// Query if the application is running on a Chromebook.
    /// </summary>
    /// <returns><c>true</c> if this is a Chromebook, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsChromebook"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsChromebook();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsDeXMode(void);</code>
    /// <summary>
    /// Query if the application is running on a Samsung DeX docking station.
    /// </summary>
    /// <returns><c>true</c> if this is a DeX docking station, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsDeXMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsDeXMode();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SendAndroidBackButton(void);</code>
    /// <summary>
    /// Trigger the Android system back button behavior.
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SendAndroidBackButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SendAndroidBackButton();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidInternalStoragePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAndroidInternalStoragePath();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAndroidInternalStoragePath(void);</code>
    /// <summary>
    /// <para>Get the path used for internal storage for this Android application.</para>
    /// <para>This path is unique to your application and cannot be written to by other
    /// applications.</para>
    /// <para>Your internal storage path is typically:
    /// <c>/data/data/your.app.package/files</c>.</para>
    /// <para>This is a C wrapper over <c>android.content.Context.getFilesDir()</c>:</para>
    /// <para>https://developer.android.com/reference/android/content/Context#getFilesDir()</para>
    /// </summary>
    /// <returns>the path used for internal storage or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAndroidExternalStoragePath"/>
    /// <seealso cref="GetAndroidCachePath"/>
    public static string? GetAndroidInternalStoragePath()
    {
        var value = SDL_GetAndroidInternalStoragePath(); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GetAndroidExternalStorageState(void);</code>
    /// <summary>
    /// <para>Get the current state of external storage for this Android application.</para>
    /// <para>The current state of external storage, a bitmask of these values:
    /// <see cref="AndroidExternalStorageRead"/>, <see cref="AndroidExternalStorageWrite"/>.</para>
    /// <para>If external storage is currently unavailable, this will return 0.</para>
    /// </summary>
    /// <returns>the current state of external storage, or 0 if external storage is
    /// currently unavailable.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAndroidExternalStoragePath"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidExternalStorageState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetAndroidExternalStorageState();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidExternalStoragePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAndroidExternalStoragePath();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAndroidExternalStoragePath(void);</code>
    /// <summary>
    /// <para>Get the path used for external storage for this Android application.</para>
    /// <para>This path is unique to your application, but is public and can be written
    /// to by other applications.</para>
    /// <para>Your external storage path is typically:
    /// <c>/storage/sdcard0/Android/data/your.app.package/files</c>.</para>
    /// <para>This is a C wrapper over <c>android.content.Context.getExternalFilesDir()</c>:</para>
    /// <para>https://developer.android.com/reference/android/content/Context#getExternalFilesDir()</para>
    /// </summary>
    /// <returns>the path used for external storage for this application on success
    /// or <c>null</c> on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAndroidExternalStorageState"/>
    /// <seealso cref="GetAndroidInternalStoragePath"/>
    /// <seealso cref="GetAndroidCachePath"/>
    public static string? GetAndroidExternalStoragePath()
    {
        var value = SDL_GetAndroidExternalStoragePath(); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAndroidCachePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAndroidCachePath();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAndroidCachePath(void);</code>
    /// <summary>
    /// <para>Get the path used for caching data for this Android application.</para>
    /// <para>This path is unique to your application, but is public and can be written
    /// to by other applications.</para>
    /// <para>Your cache path is typically: <c>/data/data/your.app.package/cache/</c>.</para>
    /// <para>This is a C wrapper over <c>android.content.Context.getCacheDir()</c>:</para>
    /// <para>https://developer.android.com/reference/android/content/Context#getCacheDir()</para>
    /// </summary>
    /// <returns>the path used for caches for this application on success or <c>null</c>
    /// on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAndroidInternalStoragePath"/>
    /// <seealso cref="GetAndroidExternalStoragePath"/>
    public static string? GetAndroidCachePath()
    {
        var value = SDL_GetAndroidCachePath(); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RequestAndroidPermission(const char *permission, SDL_RequestAndroidPermissionCallback cb, void *userdata);</code>
    /// <summary>
    /// <para>Request permissions at runtime, asynchronously.</para>
    /// <para>You do not need to call this for built-in functionality of SDL; recording
    /// from a microphone or reading images from a camera, using standard SDL APIs,
    /// will manage permission requests for you.</para>
    /// <para>This function never blocks. Instead, the app-supplied callback will be
    /// called when a decision has been made. This callback may happen on a
    /// different thread, and possibly much later, as it might wait on a user to
    /// respond to a system dialog. If permission has already been granted for a
    /// specific entitlement, the callback will still fire, probably on the current
    /// thread and before this function returns.</para>
    /// <para>If the request submission fails, this function returns -1 and the callback
    /// will NOT be called, but this should only happen in catastrophic conditions,
    /// like memory running out. Normally there will be a yes or no to the request
    /// through the callback.</para>
    /// <para>For the <c>permission</c> parameter, choose a value from here:</para>
    /// <para>https://developer.android.com/reference/android/Manifest.permission</para>
    /// </summary>
    /// <param name="permission">the permission to request.</param>
    /// <param name="cb">the callback to trigger when the request has a response.</param>
    /// <param name="userdata">an app-controlled pointer that is passed to the callback.</param>
    /// <returns><c>true</c> if the request was submitted, <c>false</c> if there was an error
    /// submitting. The result of the request is only ever reported
    /// through the callback, not this return value.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RequestAndroidPermission"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RequestAndroidPermission([MarshalAs(UnmanagedType.LPUTF8Str)] string permission, RequestAndroidPermissionCallback cb, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShowAndroidToast(const char *message, int duration, int gravity, int xoffset, int yoffset);</code>
    /// <summary>
    /// <para>Shows an Android toast notification.</para>
    /// <para>Toasts are a sort of lightweight notification that are unique to Android.</para>
    /// <para>https://developer.android.com/guide/topics/ui/notifiers/toasts</para>
    /// <para>Shows toast in UI thread.</para>
    /// <para>For the <c>gravity</c> parameter, choose a value from here, or -1 if you don't
    /// have a preference:</para>
    /// <para>https://developer.android.com/reference/android/view/Gravity</para>
    /// </summary>
    /// <param name="message">text message to be shown.</param>
    /// <param name="duration">0=short, 1=long.</param>
    /// <param name="gravity">where the notification should appear on the screen.</param>
    /// <param name="xoffset">set this parameter only when gravity >=0.</param>
    /// <param name="yoffset">set this parameter only when gravity >=0.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowAndroidToast"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ShowAndroidToast([MarshalAs(UnmanagedType.LPUTF8Str)] string message, int duration, int gravity, int xoffset, int yoffset);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendAndroidMessage(Uint32 command, int param);</code>
    /// <summary>
    /// <para>Send a user command to SDLActivity.</para>
    /// <para>Override "boolean onUnhandledMessage(Message msg)" to handle the message.</para>
    /// </summary>
    /// <param name="command">user command that must be greater or equal to 0x8000.</param>
    /// <param name="param">user parameter.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SendAndroidMessage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SendAndroidMessage(uint command, int param);
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsTablet(void);</code>
    /// <summary>
    /// <para>Query if the current device is a tablet.</para>
    /// <para>If SDL can't determine this, it will return false.</para>
    /// </summary>
    /// <returns><c>true</c> if the device is a tablet, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsTablet"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsTablet();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsTV(void);</code>
    /// <summary>
    /// <para>Query if the current device is a TV.</para>
    /// <para>If SDL can't determine this, it will return false.</para>
    /// </summary>
    /// <returns><c>true</c> if the <c>device</c> is a TV, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsTV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsTV();
    
    
    /// <code>extern SDL_DECLSPEC SDL_Sandbox SDLCALL SDL_GetSandbox(void);</code>
    /// <summary>
    /// Get the application sandbox environment, if any.
    /// </summary>
    /// <returns>the application sandbox environment or <see cref="Sandbox.None"/> if the
    /// application is not running in a sandbox environment.</returns>
    /// <since>This function is available since SDL 3.1.6.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSandbox"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Sandbox GetSandbox();
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationWillTerminate(void);</code>
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationWillTerminate.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationWillTerminate"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationWillTerminate();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationDidReceiveMemoryWarning(void);</code>
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationDidReceiveMemoryWarning.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationDidReceiveMemoryWarning"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationDidReceiveMemoryWarning();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationWillEnterBackground(void);</code>
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationWillResignActive.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationWillEnterBackground"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationWillEnterBackground();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationDidEnterBackground(void);</code>
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationDidEnterBackground.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationDidEnterBackground"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationDidEnterBackground();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationWillEnterForeground(void);</code>
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationWillEnterForeground.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationWillEnterForeground"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationWillEnterForeground();
    
    
    //extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationDidEnterForeground(void);
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationDidBecomeActive.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationDidEnterForeground"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationDidEnterForeground();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OnApplicationDidChangeStatusBarOrientation(void);</code>
    /// <summary>
    /// <para>Let iOS apps with external event handling report
    /// onApplicationDidChangeStatusBarOrientation.</para>
    /// <para>This functions allows iOS apps that have their own event handling to hook
    /// into SDL to generate SDL events. This maps directly to an iOS-specific
    /// event, but since it doesn't do anything iOS-specific internally, it is
    /// available on all platforms, in case it might be useful for some specific
    /// paradigm. Most apps do not need to use this directly; SDL's internal event
    /// code will handle all this for windows created by <see cref="CreateWindow"/>!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OnApplicationDidChangeStatusBarOrientation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OnApplicationDidChangeStatusBarOrientation();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetGDKTaskQueue(XTaskQueueHandle *outTaskQueue);</code>
    /// <summary>
    /// <para>Gets a reference to the global async task queue handle for GDK,
    /// initializing if needed.</para>
    /// <para>Once you are done with the task queue, you should call
    /// XTaskQueueCloseHandle to reduce the reference count to avoid a resource
    /// leak.</para>
    /// </summary>
    /// <param name="outTaskQueue">a pointer to be filled in with task queue handle.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call SDL_GetError() for more
    /// information.</returns>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGDKTaskQueue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetGDKTaskQueue(out IntPtr outTaskQueue);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetGDKDefaultUser(XUserHandle *outUserHandle);</code>
    /// <summary>
    /// <para>Gets a reference to the default user handle for GDK.</para>
    /// <para>This is effectively a synchronous version of XUserAddAsync, which always
    /// prefers the default user and allows a sign-in UI.</para>
    /// </summary>
    /// <param name="outUserHandle">a pointer to be filled in with the default user
    /// handle.</param>
    /// <returns><c>true</c> if success or <c>false</c> on failure; call SDL_GetError() for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGDKDefaultUser"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetGDKDefaultUser(out IntPtr outUserHandle);
}