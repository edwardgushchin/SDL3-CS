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

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFileDialog(
        SDL_DialogFileCallback callback, 
        IntPtr userdata, 
        IntPtr window, 
        IntPtr filters, 
        int nfilters, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? defaultLocation, 
        [MarshalAs(SDLBool)] bool allowMany);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShowOpenFileDialog(SDL_DialogFileCallback callback, void *userdata, SDL_Window *window, const SDL_DialogFileFilter *filters, int nfilters, const char *default_location, SDL_bool allow_many);</code>
    /// <summary>
    /// <para>Displays a dialog that lets the user select a file on their filesystem.</para>
    /// <para>This function should only be invoked from the main thread.</para>
    /// <para>This is an asynchronous function; it will return immediately, and the
    /// result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user
    /// chose. The list will be empty if the user canceled the dialog, and it will
    /// be <c>NULL</c> if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one
    /// the function was invoked on.</para>
    /// <para>Depending on the platform, the user may be allowed to input paths that
    /// don't yet exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which
    /// requires an event-handling loop. Apps that do not use SDL to handle events
    /// should add a call to <see cref="PumpEvents"/> in their main loop.</para>
    /// </summary>
    /// <param name="callback">an SDL_DialogFileCallback to be invoked when the user
    /// selects a file and accepts, or cancels the dialog, or an
    /// error occurs. The first argument is a null-terminated list
    /// of C strings, representing the paths chosen by the user.
    /// The list will be empty if the user canceled the dialog, and
    /// it will be <c>NULL</c> if an error occurred. If an error occurred,
    /// it can be fetched with <see cref="GetError"/>. The second argument
    /// is the userdata pointer passed to the function. The third
    /// argument is the index of the filter selected by the user,
    /// or one past the index of the last filter (therefore the
    /// index of the terminating <c>NULL</c> filter) if no filter was
    /// chosen, or -1 if the platform does not support detecting
    /// the selected filter.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="window">the window that the dialog should be modal for. May be <c>NULL</c>.
    /// Not all platforms support this option.</param>
    /// <param name="filters">a list of SDL_DialogFileFilter's. May be <c>NULL</c>. Not all
    /// platforms support this option, and platforms that do support
    /// it may allow the user to ignore the filters.</param>
    /// <param name="defaultLocation">the default folder or file to start the dialog at.
    /// May be <c>NULL</c>. Not all platforms support this option.</param>
    /// <param name="allowMany">if non-zero, the user will be allowed to select multiple
    /// entries. Not all platforms support this option.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    public static void ShowOpenFileDialog(DialogFileCallback callback, object? userdata, Window? window, DialogFileFilter[]? filters, string? defaultLocation, bool allowMany)
    {
        var userDataHandle = GCHandle.Alloc(userdata);
        var userDataParameter = (IntPtr)userDataHandle;
        
        var w = window == null ? IntPtr.Zero : window.Handle;
        var f = filters != null ? Marshal.UnsafeAddrOfPinnedArrayElement(filters, 0) : IntPtr.Zero;
        var l = filters?.Length ?? 0;
            
        SDL_ShowOpenFileDialog(SDLCallback, userDataParameter, w, f, l, defaultLocation, allowMany);
        
        return;

        void SDLCallback(IntPtr sdlUserdata, IntPtr sdlFilelist, int sdlFilter)
        {
            var managedFileList = GetFileList(sdlFilelist);

            var sdlUserDataHandle = (GCHandle) sdlUserdata;
            var userDataObject = sdlUserDataHandle.Target;

            callback(userDataObject, managedFileList, sdlFilter);
            
            userDataHandle.Free();
            sdlUserDataHandle.Free();
        }
    }
    

    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowSaveFileDialog(
        SDL_DialogFileCallback callback, 
        IntPtr userdata, 
        IntPtr window, 
        IntPtr filters, 
        int nfilters, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? defaultLocation);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShowSaveFileDialog(SDL_DialogFileCallback callback, void *userdata, SDL_Window *window, const SDL_DioalgFileFilter *filters, int nfilters, const char *default_location);</code>
    /// <summary>
    /// <para>Displays a dialog that lets the user choose a new or existing file on their
    /// filesystem.</para>
    /// <para>This function should only be invoked from the main thread.</para>
    /// <para>This is an asynchronous function; it will return immediately, and the
    /// result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user
    /// chose. The list will be empty if the user canceled the dialog, and it will
    /// be <c>NULL</c> if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one
    /// the function was invoked on.</para>
    /// <para>The chosen file may or may not already exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which
    /// requires an event-handling loop. Apps that do not use SDL to handle events
    /// should add a call to <see cref="PumpEvents"/> in their main loop.</para>
    /// </summary>
    /// <param name="callback">an <see cref="DialogFileCallback"/> to be invoked when the user
    /// selects a file and accepts, or cancels the dialog, or an
    /// error occurs. The first argument is a null-terminated list
    /// of C strings, representing the paths chosen by the user.
    /// The list will be empty if the user canceled the dialog, and
    /// it will be <c>NULL</c> if an error occurred. If an error occurred,
    /// it can be fetched with <see cref="GetError"/>. The second argument
    /// is the userdata pointer passed to the function. The third
    /// argument is the index of the filter selected by the user,
    /// or one past the index of the last filter (therefore the
    /// index of the terminating <c>NULL</c> filter) if no filter was
    /// chosen, or -1 if the platform does not support detecting
    /// the selected filter.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="window">the window that the dialog should be modal for. May be <c>NULL</c>.
    /// Not all platforms support this option.</param>
    /// <param name="filters">a list of <see cref="DialogFileFilter"/>. May be <c>NULL</c>. Not all
    /// platforms support this option, and platforms that do support
    /// it may allow the user to ignore the filters.</param>
    /// <param name="defaultLocation">the default folder or file to start the dialog at.
    /// May be <c>NULL</c>. Not all platforms support this option.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    public static void ShowSaveFileDialog(DialogFileCallback callback, object? userdata, Window? window, DialogFileFilter[]? filters, string? defaultLocation)
    {
        var userDataHandle = GCHandle.Alloc(userdata);
        var userDataParameter = (IntPtr)userDataHandle;
        
        var w = window == null ? IntPtr.Zero : window.Handle;
        var f = filters != null ? Marshal.UnsafeAddrOfPinnedArrayElement(filters, 0) : IntPtr.Zero;
        var l = filters?.Length ?? 0;
            
        SDL_ShowSaveFileDialog(SDLCallback, userDataParameter, w, f, l, defaultLocation);
        
        return;

        void SDLCallback(IntPtr sdlUserdata, IntPtr sdlFilelist, int sdlFilter)
        {
            var managedFileList = GetFileList(sdlFilelist);

            var sdlUserDataHandle = (GCHandle) sdlUserdata;
            var userDataObject = sdlUserDataHandle.Target;

            callback(userDataObject, managedFileList, sdlFilter);
            
            userDataHandle.Free();
            sdlUserDataHandle.Free();
        }
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFolderDialog(SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? defaultLocation, [MarshalAs(SDLBool)] bool allowMany);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShowOpenFolderDialog(SDL_DialogFileCallback callback, void *userdata, SDL_Window *window, const char *default_location, SDL_bool allow_many);</code>
    /// <summary>
    /// <para>Displays a dialog that lets the user select a folder on their filesystem.</para>
    /// <para>This function should only be invoked from the main thread.</para>
    /// <para>This is an asynchronous function; it will return immediately, and the
    /// result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user
    /// chose. The list will be empty if the user canceled the dialog, and it will
    /// be NULL if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one
    /// the function was invoked on.</para>
    /// <para>Depending on the platform, the user may be allowed to input paths that
    /// don't yet exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which
    /// requires an event-handling loop. Apps that do not use SDL to handle events
    /// should add a call to SDL_PumpEvents in their main loop.</para>
    /// </summary>
    /// <param name="callback">an <see cref="DialogFileCallback"/> to be invoked when the user
    /// selects a file and accepts, or cancels the dialog, or an
    /// error occurs. The first argument is a null-terminated list
    /// of C strings, representing the paths chosen by the user.
    /// The list will be empty if the user canceled the dialog, and
    /// it will be NULL if an error occurred. If an error occurred,
    /// it can be fetched with <see cref="GetError"/>. The second argument
    /// is the userdata pointer passed to the function. The third
    /// argument is always -1 for <see cref="ShowOpenFolderDialog"/>.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="window">the window that the dialog should be modal for. May be NULL.
    /// Not all platforms support this option.</param>
    /// <param name="defaultLocation">the default folder or file to start the dialog at.
    /// May be NULL. Not all platforms support this option</param>
    /// <param name="allowMany">if non-zero, the user will be allowed to select multiple
    /// entries. Not all platforms support this option.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    public static void ShowOpenFolderDialog(DialogFileCallback callback, object? userdata, Window? window, 
        string? defaultLocation, bool allowMany)
    {
        var userDataHandle = GCHandle.Alloc(userdata);
        var userDataParameter = (IntPtr) userDataHandle;
            
        var w = window == null ? IntPtr.Zero : window.Handle;
        
        SDL_ShowOpenFolderDialog(SDLCallback, userDataParameter, w, defaultLocation, allowMany);
        
        return;

        void SDLCallback(IntPtr sdlUserdata, IntPtr sdlFilelist, int sdlFilter)
        {
            var managedFileList = GetFileList(sdlFilelist);

            var sdlUserDataHandle = (GCHandle) sdlUserdata;
            var userDataObject = sdlUserDataHandle.Target;

            callback(userDataObject, managedFileList, sdlFilter);
            
            userDataHandle.Free();
            sdlUserDataHandle.Free();
        }
    }
    
    
    private static string[]? GetFileList(IntPtr sdlFilelist)
    {
        string[]? managedFileList = null;

        if (sdlFilelist == IntPtr.Zero) return managedFileList;
        var result = new List<string>();
        for (var ptr = Marshal.ReadIntPtr(sdlFilelist);
             ptr != IntPtr.Zero;
             ptr = Marshal.ReadIntPtr(sdlFilelist, result.Count * IntPtr.Size))
        {
            result.Add(Marshal.PtrToStringUTF8(ptr)!);
        }

        managedFileList = result.ToArray();

        return managedFileList;
    }
}