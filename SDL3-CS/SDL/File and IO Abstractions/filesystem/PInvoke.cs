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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetBasePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetBasePath();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetBasePath(void);</code>
    /// <summary>
    /// <para>Get the directory where the application was run from.</para>
    /// <para>SDL caches the result of this call internally, but the first call to this
    /// function is not necessarily fast, so plan accordingly.</para>
    /// <para><b>macOS and iOS Specific Functionality</b>: If the application is in a ".app"
    /// bundle, this function returns the Resource directory (e.g.
    /// MyApp.app/Contents/Resources/). This behaviour can be overridden by adding
    /// a property to the Info.plist file. Adding a string key with the name
    /// SDL_FILESYSTEM_BASE_DIR_TYPE with a supported value will change the
    /// behaviour.</para>
    /// <para>Supported values for the SDL_FILESYSTEM_BASE_DIR_TYPE property (Given an
    /// application in /Applications/SDLApp/MyApp.app):</para>
    /// <list type="bullet">
    /// <item><c>resource</c>: bundle resource directory (the default). For example:
    /// <c>/Applications/SDLApp/MyApp.app/Contents/Resources</c></item>
    /// <item><c>bundle</c>: the Bundle directory. For example:
    /// <c>/Applications/SDLApp/MyApp.app/</c></item>
    /// <item><c>parent</c>: the containing directory of the bundle. For example:
    /// <c>/Applications/SDLApp/</c></item>
    /// </list>
    /// <para><b>Nintendo 3DS Specific Functionality</b>: This function returns "romfs"
    /// directory of the application as it is uncommon to store resources outside
    /// the executable. As such it is not a writable directory.</para>
    /// <para>The returned path is guaranteed to end with a path separator ('\\' on
    /// Windows, '/' on most other platforms).</para>
    /// </summary>
    /// <returns>an absolute path in UTF-8 encoding to the application data
    /// directory. <c>null</c> will be returned on error or when the platform
    /// doesn't implement this functionality, call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPrefPath"/>
    public static string? GetBasePath()
    {
        var value = SDL_GetBasePath(); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPrefPath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPrefPath([MarshalAs(UnmanagedType.LPUTF8Str)] string org, [MarshalAs(UnmanagedType.LPUTF8Str)] string app);
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetPrefPath(const char *org, const char *app);</code>
    /// <summary>
    /// <para>Get the user-and-app-specific path where files can be written.</para>
    /// <para>Get the "pref dir". This is meant to be where users can write personal
    /// files (preferences and save games, etc) that are specific to your
    /// application. This directory is unique per user, per application.</para>
    /// <para>This function will decide the appropriate location in the native
    /// filesystem, create the directory if necessary, and return a string of the
    /// absolute path to the directory in UTF-8 encoding.</para>
    /// <para>On Windows, the string might look like:</para>
    /// <para><c>C:\\Users\\bob\\AppData\\Roaming\\My Company\\My Program Name\\</c></para>
    /// <para>On Linux, the string might look like:</para>
    /// <para><c>/home/bob/.local/share/My Program Name/</c></para>
    /// <para>On macOS, the string might look like:</para>
    /// <para><c>/Users/bob/Library/Application Support/My Program Name/</c></para>
    /// <para>You should assume the path returned by this function is the only safe place
    /// to write files (and that <see cref="GetBasePath"/>, while it might be writable, or
    /// even the parent of the returned path, isn't where you should be writing
    /// things).</para>
    /// <para>Both the org and app strings may become part of a directory name, so please
    /// follow these rules:</para>
    /// <list type="bullet">
    /// <item>Try to use the same org string (_including case-sensitivity_) for all
    /// your applications that use this function.</item>
    /// <item>Always use a unique app string for each one, and make sure it never
    /// changes for an app once you've decided on it.</item>
    /// <item>Unicode characters are legal, as long as they are UTF-8 encoded, but...</item>
    /// <item>...only use letters, numbers, and spaces. Avoid punctuation like "Game
    /// Name 2: Bad Guy's Revenge!" ... "Game Name 2" is sufficient.</item>
    /// </list>
    /// <para>Due to historical mistakes, <c>org</c> is allowed to be <c>null</c> or <c>""</c>. In such
    /// cases, SDL will omit the org subdirectory, including on platforms where it
    /// shouldn't, and including on platforms where this would make your app fail
    /// certification for an app store. New apps should definitely specify a real
    /// string for <c>org</c>.</para>
    /// <para>The returned path is guaranteed to end with a path separator ('\\' on
    /// Windows, '/' on most other platforms).</para>
    /// </summary>
    /// <param name="org">the name of your organization.</param>
    /// <param name="app">the name of your application.</param>
    /// <returns>a UTF-8 string of the user directory in platform-dependent
    /// notation. <c>null</c> if there's a problem (creating directory failed,
    /// etc.). This should be freed with <see cref="Free"/> when it is no longer
    /// needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetBasePath"/>
    public static string? GetPrefPath(string org, string app)
    {
        var value = SDL_GetPrefPath(org, app); 
        try
        {
            return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetUserFolder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetUserFolder(Folder folder);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetUserFolder(SDL_Folder folder);</code>
    /// <summary>
    /// <para>Finds the most suitable user folder for a specific purpose.</para>
    /// <para>Many OSes provide certain standard folders for certain purposes, such as
    /// storing pictures, music or videos for a certain user. This function gives
    /// the path for many of those special locations.</para>
    /// <para>This function is specifically for _user_ folders, which are meant for the
    /// user to access and manage. For application-specific folders, meant to hold
    /// data for the application to manage, see <see cref="GetBasePath"/> and
    /// <see cref="GetPrefPath"/>.</para>
    /// <para>The returned path is guaranteed to end with a path separator ('\\' on
    /// Windows, '/' on most other platforms).</para>
    /// <para>If <c>null</c> is returned, the error may be obtained with <see cref="GetError"/>.</para>
    /// </summary>
    /// <param name="folder">the type of folder to find.</param>
    /// <returns>either a null-terminated C string containing the full path to the
    /// folder, or <c>null</c> if an error happened.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetUserFolder(Folder folder)
    {
        var value = SDL_GetUserFolder(folder); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CreateDirectory(const char *path);</code>
    /// <summary>
    /// <para>Create a directory, and any missing parent directories.</para>
    /// <para>This reports success if <c>path</c> already exists as a directory.</para>
    /// <para>If parent directories are missing, it will also create them. Note that if
    /// this fails, it will not remove any parent directories it already made.</para>
    /// </summary>
    /// <param name="path">the path of the directory to create.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call SDL_GetError() for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CreateDirectory([MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_EnumerateDirectory(const char *path, SDL_EnumerateDirectoryCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Enumerate a directory through a callback function.</para>
    /// <para>This function provides every directory entry through an app-provided
    /// callback, called once for each directory entry, until all results have been
    /// provided or the callback returns either <see cref="EnumerationResult.Success"/> or
    /// <see cref="EnumerationResult.Failure"/>.</para>
    /// <para>This will return false if there was a system problem in general, or if a
    /// callback returns <see cref="EnumerationResult.Failure"/>. A successful return means a callback
    /// returned <see cref="EnumerationResult.Success"/> to halt enumeration, or all directory entries
    /// were enumerated.</para>
    /// </summary>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="callback">a function that is called for each entry in the directory.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EnumerateDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool EnumerateDirectory([MarshalAs(UnmanagedType.LPUTF8Str)] string path, EnumerateDirectoryCallback callback, IntPtr userdata);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RemovePath(const char *path);</code>
    /// <summary>
    /// <para>Remove a file or an empty directory.</para>
    /// <para>Directories that are not empty will fail; this function will not recursely
    /// delete directory trees.</para>
    /// </summary>
    /// <param name="path">the path to remove from the filesystem.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemovePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RemovePath([MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenamePath(const char *oldpath, const char *newpath);</code>
    /// <summary>
    /// <para>Rename a file or directory.</para>
    /// <para>If the file at <c>newpath</c> already exists, it will be replaced.</para>
    /// <para>Note that this will not copy files across filesystems/drives/volumes, as
    /// that is a much more complicated (and possibly time-consuming) operation.</para>
    /// <para>Which is to say, if this function fails, <see cref="CopyFile"/> to a temporary file
    /// in the same directory as <c>newpath</c>, then <see cref="RenamePath"/> from the
    /// temporary file to <c>newpath</c> and <see cref="RemovePath"/> on <c>oldpath</c> might work
    /// for files. Renaming a non-empty directory across filesystems is
    /// dramatically more complex, however.</para>
    /// </summary>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenamePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenamePath([MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath, [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CopyFile(const char *oldpath, const char *newpath);</code>
    /// <summary>
    /// <para>Copy a file.</para>
    /// <para>If the file at <c>newpath</c> already exists, it will be overwritten with the
    /// contents of the file at <c>oldpath</c>.</para>
    /// <para>This function will block until the copy is complete, which might be a
    /// significant time for large files on slow disks. On some platforms, the copy
    /// can be handed off to the OS itself, but on others SDL might just open both
    /// paths, and read from one and write to the other.</para>
    /// <para>Note that this is not an atomic operation! If something tries to read from
    /// <c>newpath</c> while the copy is in progress, it will see an incomplete copy of
    /// the data, and if the calling thread terminates (or the power goes out)
    /// during the copy, <c>newpath</c>'s previous contents will be gone, replaced with
    /// an incomplete copy of the data. To avoid this risk, it is recommended that
    /// the app copy to a temporary file in the same directory as <c>newpath</c>, and if
    /// the copy is successful, use <see cref="RenamePath"/> to replace <c>newpath</c> with the
    /// temporary file. This will ensure that reads of <c>newpath</c> will either see a
    /// complete copy of the data, or it will see the pre-copy state of <c>newpath</c>.</para>
    /// <para>This function attempts to synchronize the newly-copied data to disk before
    /// returning, if the platform allows it, so that the renaming trick will not
    /// have a problem in a system crash or power failure, where the file could be
    /// renamed but the contents never made it from the system file cache to the
    /// physical disk.</para>
    /// <para>If the copy fails for any reason, the state of <c>newpath</c> is undefined. It
    /// might be half a copy, it might be the untouched data of what was already
    /// there, or it might be a zero-byte file, etc.</para>
    /// </summary>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but this
    /// operation is not atomic, so the app might need to protect
    /// access to specific paths from other threads if appropriate.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CopyFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CopyFile([MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath, [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetPathInfo(const char *path, SDL_PathInfo *info);</code>
    /// <summary>
    /// Get information about a filesystem path.
    /// </summary>
    /// <param name="path">the path to query.</param>
    /// <param name="info">a pointer filled in with information about the path, or <c>null</c> to
    /// check for the existence of a file.</param>
    /// <returns><c>true</c> on success or <c>false</c> if the file doesn't exist, or another
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPathInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetPathInfo([MarshalAs(UnmanagedType.LPUTF8Str)] string path, out PathInfo info);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GlobDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GlobDirectory([MarshalAs(UnmanagedType.LPUTF8Str)] string path, [MarshalAs(UnmanagedType.LPUTF8Str)] string? pattern, GlobFlags flags, out int count);
    /// <code>extern SDL_DECLSPEC char ** SDLCALL SDL_GlobDirectory(const char *path, const char *pattern, SDL_GlobFlags flags, int *count);</code>
    /// <summary>
    /// <para>Files are filtered out if they don't match the string in `pattern`, which
    /// may contain wildcard characters `*` (match everything) and `?` (match one
    /// character). If pattern is NULL, no filtering is done and all results are
    /// returned. Subdirectories are permitted, and are specified with a path
    /// separator of `/`. Wildcard characters `*` and `?` never match a path
    /// separator.</para>
    /// <para><c>flags</c> may be set to <see cref="GlobFlags.CaseInsensitive"/> to make the pattern matching
    /// case-insensitive.</para>
    /// <para>The returned array is always NULL-terminated, for your iterating
    /// convenience, but if `count` is non-NULL, on return it will contain the
    /// number of items in the array, not counting the <c>null</c> terminator.</para>
    /// </summary>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="pattern">the pattern that files in the directory must match. Can be
    /// <c>null</c>.</param>
    /// <param name="flags"><c>SDL_GLOB_*</c> bitflags that affect this search.</param>
    /// <param name="count">on return, will be set to the number of items in the returned
    /// array. Can be <c>null</c>.</param>
    /// <returns>an array of strings on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. This is a single allocation
    /// that should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string[]? GlobDirectory(string path, string? pattern, GlobFlags flags, out int count)
    {
        var ptr = SDL_GlobDirectory(path, pattern, flags, out count);

        try
        {
            return PointerToStringArray(ptr, count);
        }
        finally
        {
            if(ptr != IntPtr.Zero) Free(ptr);
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentDirectory();
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetCurrentDirectory(void);</code>
    /// <summary>
    /// <para>Get what the system believes is the "current working directory."</para>
    /// <para>For systems without a concept of a current working directory, this will
    /// still attempt to provide something reasonable.</para>
    /// <para>SDL does not provide a means to _change_ the current working directory; for
    /// platforms without this concept, this would cause surprises with file access
    /// outside of SDL.</para>
    /// <para>The returned path is guaranteed to end with a path separator (<c>\\</c> on
    /// Windows, <c>/</c> on most other platforms).</para>
    /// </summary>
    /// <returns>a UTF-8 string of the current working directory in
    /// platform-dependent notation. <c>null</c> if there's a problem. This
    /// should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    public static string? GetCurrentDirectory()
    {
        var value = SDL_GetCurrentDirectory(); 
        try
        {
            return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }
}