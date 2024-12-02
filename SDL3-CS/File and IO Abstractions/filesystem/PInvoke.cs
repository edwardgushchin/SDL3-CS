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
    private static partial IntPtr SDL_GetBasePath();
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetBasePath(void);</code>
    /// <summary>
    /// <para>Get the directory where the application was run from.</para>
    /// <para>SDL caches the result of this call internally, but the first call to this
    /// function is not necessarily fast, so plan accordingly.</para>
    /// <para>**macOS and iOS Specific Functionality**: If the application is in a ".app"
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
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <returns>an absolute path in UTF-8 encoding to the application data
    /// directory. <c>null</c> will be returned on error or when the platform
    /// doesn't implement this functionality, call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetPrefPath"/>
    public static string? GetBasePath() => Marshal.PtrToStringUTF8(SDL_GetBasePath());
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPrefPath([MarshalAs(UnmanagedType.LPUTF8Str)] string org, [MarshalAs(UnmanagedType.LPUTF8Str)] string app);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetPrefPath(const char *org, const char *app);</code>
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
    /// </summary>
    /// <param name="org">the name of your organization.</param>
    /// <param name="app">the name of your application.</param>
    /// <returns>a UTF-8 string of the user directory in platform-dependent
    /// notation. <c>null</c> if there's a problem (creating directory failed,
    /// etc.).</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetBasePath"/>
    public static string? GetPrefPath(string org, string app) => Marshal.PtrToStringUTF8(SDL_GetPrefPath(org, app));
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetUserFolder(Folder folder);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetUserFolder(SDL_Folder folder);</code>
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
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// <para>If <c>null</c> is returned, the error may be obtained with <see cref="GetError"/>.</para>
    /// </summary>
    /// <param name="folder">the type of folder to find.</param>
    /// <returns>either a null-terminated C string containing the full path to the
    /// folder, or <c>null</c> if an error happened.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string? GetUserFolder(Folder folder) => Marshal.PtrToStringUTF8(SDL_GetUserFolder(folder));

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CreateDirectory([MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CreateDirectory(const char *path);</code>
    /// <summary>
    /// Create a directory.
    /// </summary>
    /// <param name="path">the path of the directory to create.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int CreateDirectory(string path) => SDL_CreateDirectory(path);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_EnumerateDirectory(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        EnumerateDirectoryCallback callback,
        IntPtr userdata);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_EnumerateDirectory(const char *path, SDL_EnumerateDirectoryCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Enumerate a directory through a callback function.</para>
    /// <para>This function provides every directory entry through an app-provided
    /// callback, called once for each directory entry, until all results have been
    /// provided or the callback returns &lt;= 0.</para>
    /// </summary>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="callback">a function that is called for each entry in the directory.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int EnumerateDirectory(string path, EnumerateDirectoryCallback callback, IntPtr userdata) =>
        SDL_EnumerateDirectory(path, callback, userdata);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RemovePath([MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RemovePath(const char *path);</code>
    /// <summary>
    /// Remove a file or an empty directory.
    /// </summary>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int RemovePath(string path) => SDL_RemovePath(path);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenamePath(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RenamePath(const char *oldpath, const char *newpath);</code>
    /// <summary>
    /// Rename a file or directory.
    /// </summary>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int RenamePath(string oldpath, string newpath) => SDL_RenamePath(oldpath, newpath);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetPathInfo([MarshalAs(UnmanagedType.LPUTF8Str)] string path, out PathInfo info);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetPathInfo(const char *path, SDL_PathInfo *info);</code>
    /// <summary>
    /// Get information about a filesystem path.
    /// </summary>
    /// <param name="path">the path to query.</param>
    /// <param name="info">a pointer filled in with information about the path, or NULL to
    /// check for the existence of a file.</param>
    /// <returns>0 on success or a negative error code if the file doesn't exist,
    /// or another failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetPathInfo(string path, out PathInfo info) => SDL_GetPathInfo(path, out info);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GlobDirectory(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? pattern,
        UInt32 flags,
        out int count);
    /// <code>extern SDL_DECLSPEC const char * const *SDLCALL SDL_GlobDirectory(const char *path, const char *pattern, SDL_GlobFlags flags, int *count);</code>
    /// <summary>
    /// <para>Enumerate a directory tree, filtered by pattern, and return a list.</para>
    /// <para>Files are filtered out if they don't match the string in <c>pattern</c>, which
    /// may contain wildcard characters <c>*</c> (match everything) and <c>?</c> (match one
    /// character). If pattern is <c>null</c>, no filtering is done and all results are
    /// returned. Subdirectories are permitted, and are specified with a path
    /// separator of <c>/</c>. Wildcard characters <c>*</c> and <c>?</c> never match a path
    /// separator.</para>
    /// <para><c>flags</c> may be set to SDL_GLOB_CASEINSENSITIVE to make the pattern matching
    /// case-insensitive.</para>
    /// <para>The returned pointer follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="pattern">the pattern that files in the directory must match. Can be
    /// NULL.</param>
    /// <param name="flags"><c>SDL_GLOB_*</c> bitflags that affect this search.</param>
    /// <param name="count">on return, will be set to the number of items in the returned
    /// array. Can be NULL.</param>
    /// <returns>an array of strings on success or NULL on failure; call
    /// <see cref="GetError"/> for more information. The caller should pass the
    /// returned pointer to SDL_free when done with it.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string[]? GlobDirectory(string path, string? pattern, uint flags, out int count)
    {
        var arrayPtr = SDL_GlobDirectory(path, pattern, flags, out count);
        if (arrayPtr == IntPtr.Zero)
            return null;

        try
        {
            unsafe
            {
                var list = new List<string>();
                var ptr = (IntPtr*)arrayPtr;
                for (var i = 0; ptr[i] != IntPtr.Zero; i++)
                {
                    list.Add(Marshal.PtrToStringUTF8(ptr[i])!);
                }
                return list.ToArray();
            }
        }
        finally
        {
            Free(arrayPtr);
        }
    }

}