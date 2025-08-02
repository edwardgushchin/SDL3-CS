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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowOpenFileDialog"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFileDialog(DialogFileCallback callback, IntPtr userdata, IntPtr window, 
        IntPtr filters, int nfilters, IntPtr defaultLocation, [MarshalAs(UnmanagedType.I1)] bool allowMany);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShowOpenFileDialog(SDL_DialogFileCallback callback, void *userdata, SDL_Window *window, const SDL_DialogFileFilter *filters, int nfilters, const char *default_location, bool allow_many);</code>
    /// <summary>
    /// <para>Displays a dialog that lets the user select a file on their filesystem.</para>
    /// <para>This function should only be invoked from the main thread.</para>
    /// <para>This is an asynchronous function; it will return immediately, and the
    /// result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user
    /// chose. The list will be empty if the user canceled the dialog, and it will
    /// be <c>null</c> if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one
    /// the function was invoked on.</para>
    /// <para>Depending on the platform, the user may be allowed to input paths that
    /// don't yet exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which
    /// requires an event-handling loop. Apps that do not use SDL to handle events
    /// should add a call to <see cref="PumpEvents()"/> in their main loop.</para>
    /// </summary>
    /// <param name="callback">a function pointer to be invoked when the user
    /// selects a file and accepts, or cancels the dialog, or an
    /// error occurs.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="window">the window that the dialog should be modal for, may be <c>null</c>.
    /// Not all platforms support this option.</param>
    /// <param name="filters">a list of filters, may be NULL. See the
    /// [`SDL_DialogFileFilter` documentation for
    /// examples](SDL_DialogFileFilter#code-examples). Not all
    /// platforms support this option, and platforms that do support
    /// it may allow the user to ignore the filters. If non-NULL, it
    /// must remain valid at least until the callback is invoked.</param>
    /// <param name="nfilters">the number of filters. Ignored if filters is <c>null</c>.</param>
    /// <param name="defaultLocation">the default folder or file to start the dialog at,
    /// may be <c>null</c>. Not all platforms support this option.</param>
    /// <param name="allowMany">if non-zero, the user will be allowed to select multiple
    /// entries. Not all platforms support this option.</param>
    /// <threadsafety>This function should be called only from the main thread. The
    /// callback may be invoked from the same thread or from a
    /// different one, depending on the OS's constraints.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    /// <seealso cref="ShowFileDialogWithProperties"/>
    public static void ShowOpenFileDialog(DialogFileCallback callback, IntPtr userdata, IntPtr window, 
        DialogFileFilter[]? filters, int nfilters, string? defaultLocation, bool allowMany)
    {
        var pathPointer = IntPtr.Zero;
        var filterPointer = IntPtr.Zero;
        GCHandle? filterHandle = null;
        
        try
        {
            if (filters != null)
            {
                filterHandle = GCHandle.Alloc(filters, GCHandleType.Pinned);
                filterPointer = filterHandle.Value.AddrOfPinnedObject();
            }

            if (defaultLocation != null)
            {
                pathPointer = Marshal.StringToCoTaskMemUTF8(defaultLocation);
            }
            
            SDL_ShowOpenFileDialog(callback, userdata, window, filterPointer, nfilters, pathPointer, allowMany);
        }
        finally
        {
            if (pathPointer != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pathPointer);
            }

            filterHandle?.Free();
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowSaveFileDialog"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowSaveFileDialog(DialogFileCallback callback, IntPtr userdata, IntPtr window, 
        IntPtr filters, int nfilters, IntPtr defaultLocation);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShowSaveFileDialog(SDL_DialogFileCallback callback, void *userdata, SDL_Window *window, const SDL_DialogFileFilter *filters, int nfilters, const char *default_location);</code>
    /// <summary>
    /// <para>Displays a dialog that lets the user choose a new or existing file on their
    /// filesystem.</para>
    /// <para>This function should only be invoked from the main thread.</para>
    /// <para>This is an asynchronous function; it will return immediately, and the
    /// result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user
    /// chose. The list will be empty if the user canceled the dialog, and it will
    /// be <c>null</c> if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one
    /// the function was invoked on.</para>
    /// <para>The chosen file may or may not already exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which
    /// requires an event-handling loop. Apps that do not use SDL to handle events
    /// should add a call to <see cref="PumpEvents()"/> in their main loop.</para>
    /// </summary>
    /// <param name="callback">a function pointer to be invoked when the user
    /// selects a file and accepts, or cancels the dialog, or an
    /// error occurs.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="window">the window that the dialog should be modal for, may be <c>null</c>.
    /// Not all platforms support this option.</param>
    /// <param name="filters">a list of filters, may be <c>null</c>. Not all platforms support
    /// this option, and platforms that do support it may allow the
    /// user to ignore the filters. If non-NULL, it must remain
    /// valid at least until the callback is invoked.</param>
    /// <param name="nfilters">the number of filters. Ignored if filters is <c>null</c>.</param>
    /// <param name="defaultLocation">the default folder or file to start the dialog at,
    /// may be <c>null</c>. Not all platforms support this option.</param>
    /// <threadsafety>This function should be called only from the main thread. The
    /// callback may be invoked from the same thread or from a
    /// different one, depending on the OS's constraints.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    /// <seealso cref="ShowFileDialogWithProperties"/>
    public static void ShowSaveFileDialog(DialogFileCallback callback, IntPtr userdata, IntPtr window, 
        DialogFileFilter[]? filters, int nfilters, string? defaultLocation)
    {
        var pathPointer = IntPtr.Zero;
        var filterPointer = IntPtr.Zero;
        GCHandle? filterHandle = null;
        
        try
        {
            if (filters != null)
            {
                filterHandle = GCHandle.Alloc(filters, GCHandleType.Pinned);
                filterPointer = filterHandle.Value.AddrOfPinnedObject();
            }

            if (defaultLocation != null)
            {
                pathPointer = Marshal.StringToCoTaskMemUTF8(defaultLocation);
            }
            
            SDL_ShowSaveFileDialog(callback, userdata, window, filterPointer, nfilters, pathPointer);
        }
        finally
        {
            if (pathPointer != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pathPointer);
            }

            filterHandle?.Free();
        }
    }
    

    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowOpenFolderDialog"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShowOpenFolderDialog(DialogFileCallback callback, IntPtr userdata, IntPtr window, 
        IntPtr defaultLocation, [MarshalAs(UnmanagedType.I1)] bool allowMany);
    /// <summary>
    /// <para>Displays a dialog that lets the user select a folder on their filesystem.</para>
    /// <para>This function should only be invoked from the main thread.</para>
    /// <para>This is an asynchronous function; it will return immediately, and the
    /// result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user
    /// chose. The list will be empty if the user canceled the dialog, and it will
    /// be <c>null</c> if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one
    /// the function was invoked on.</para>
    /// <para>Depending on the platform, the user may be allowed to input paths that
    /// don't yet exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which
    /// requires an event-handling loop. Apps that do not use SDL to handle events
    /// should add a call to <see cref="PumpEvents()"/> in their main loop.</para>
    /// </summary>
    /// <param name="callback">a function pointer to be invoked when the user
    /// selects a file and accepts, or cancels the dialog, or an
    /// error occurs.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="window">the window that the dialog should be modal for, may be <c>null</c>.
    /// Not all platforms support this option.</param>
    /// <param name="defaultLocation">the default folder or file to start the dialog at,
    /// may be <c>null</c>. Not all platforms support this option.</param>
    /// <param name="allowMany">if non-zero, the user will be allowed to select multiple
    /// entries. Not all platforms support this option.</param>
    /// <threadsafety>This function should be called only from the main thread. The
    /// callback may be invoked from the same thread or from a
    /// different one, depending on the OS's constraints.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowFileDialogWithProperties"/>
    public static void ShowOpenFolderDialog(DialogFileCallback callback, IntPtr userdata, IntPtr window, string? defaultLocation, bool allowMany)
    {
        var pathPointer = IntPtr.Zero;
        
        try
        {
            if (defaultLocation != null)
            {
                pathPointer = Marshal.StringToCoTaskMemUTF8(defaultLocation);
            }
            
            SDL_ShowOpenFolderDialog(callback, userdata, window,  pathPointer, allowMany);
        }
        finally
        {
            if (pathPointer != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pathPointer);
            }
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShowFileDialogWithProperties(SDL_FileDialogType type, SDL_DialogFileCallback callback, void *userdata, SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create and launch a file dialog with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.FileDialogFiltersPointer"/>: a pointer to a list of
    /// <see cref="DialogFileFilter"/> structs, which will be used as filters for
    /// file-based selections. Ignored if the dialog is an "Open Folder" dialog.
    /// If non-NULL, the array of filters must remain valid at least until the
    /// callback is invoked.</item>
    /// <item><see cref="Props.FileDialogNFiltersNumber"/>: the number of filters in the
    /// array of filters, if it exists.</item>
    /// <item><see cref="Props.FileDialogWindowPointer"/>: the window that the dialog should
    /// be modal for.</item>
    /// <item><see cref="Props.FileDialogLocationString"/>: the default folder or file to
    /// start the dialog at.</item>
    /// <item><see cref="Props.FileDialogManyBoolean"/>: true to allow the user to select
    /// more than one entry.</item>
    /// <item><see cref="Props.FileDialogTitleString"/>: the title for the dialog.</item>
    /// <item><see cref="Props.FileDialogAcceptString"/>: the label that the accept button
    /// should have.</item>
    /// <item><see cref="Props.FileDialogCancelString"/>: the label that the cancel button
    /// should have.</item>
    /// </list>
    /// <para>Note that each platform may or may not support any of the properties.</para>
    /// </summary>
    /// <param name="type">the type of file dialog.</param>
    /// <param name="callback">a function pointer to be invoked when the user selects a
    /// file and accepts, or cancels the dialog, or an error
    /// occurs.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <param name="props">the properties to use.</param>
    /// <threadsafety>This function should be called only from the main thread. The
    /// callback may be invoked from the same thread or from a
    /// different one, depending on the OS's constraints.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="FileDialogType"/>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowFileDialogWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ShowFileDialogWithProperties(FileDialogType type, DialogFileCallback callback, IntPtr userdata, uint props);
    
}