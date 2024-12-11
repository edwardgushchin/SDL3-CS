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
    private static partial IntPtr SDL_OpenTitleStorage([MarshalAs(UnmanagedType.LPUTF8Str)] string? overridePath, uint props); 
    /// <code>extern SDL_DECLSPEC SDL_Storage *SDLCALL SDL_OpenTitleStorage(const char *override, SDL_PropertiesID props);</code>
    /// <summary>
    /// Opens up a read-only container for the application's filesystem.
    /// </summary>
    /// <param name="overridePath">a path to override the backend's default title root.</param>
    /// <param name="props">a property list that may contain backend-specific information.</param>
    /// <returns>a title storage container on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="OpenUserStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    public static Storage? OpenTitleStorage(string? overridePath, uint props) 
    { 
        var storagePtr = SDL_OpenTitleStorage(overridePath, props); 
        return storagePtr == IntPtr.Zero ? null : new Storage(storagePtr); 
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenUserStorage([MarshalAs(UnmanagedType.LPUTF8Str)] string org, [MarshalAs(UnmanagedType.LPUTF8Str)] string app, uint props);
    /// <code>extern SDL_DECLSPEC SDL_Storage *SDLCALL SDL_OpenUserStorage(const char *org, const char *app, SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Opens up a container for a user's unique read/write filesystem.</para>
    /// <para>While title storage can generally be kept open throughout runtime, user
    /// storage should only be opened when the client is ready to read/write files.
    /// This allows the backend to properly batch file operations and flush them
    /// when the container has been closed; ensuring safe and optimal save I/O.</para>
    /// </summary>
    /// <param name="org">the name of your organization.</param>
    /// <param name="app">the name of your application.</param>
    /// <param name="props">a property list that may contain backend-specific information.</param>
    /// <returns>a user storage container on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static Storage? OpenUserStorage(string org, string app, uint props)
    {
        var storagePtr = SDL_OpenUserStorage(org, app, props);
        return storagePtr == IntPtr.Zero ? null : new Storage(storagePtr);
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenFileStorage([MarshalAs(UnmanagedType.LPUTF8Str)] string? path);
    /// <code>extern SDL_DECLSPEC SDL_Storage *SDLCALL SDL_OpenFileStorage(const char *path);</code>
    /// <summary>
    /// <para>Opens up a container for local filesystem storage.</para>
    /// <para>This is provided for development and tools. Portable applications should
    /// use <see cref="OpenTitleStorage"/> for access to game data and
    /// <see cref="OpenUserStorage"/> for access to user data.</para>
    /// </summary>
    /// <param name="path">the base path prepended to all storage paths, or <c>null</c> for no
    /// base path.</param>
    /// <returns>a filesystem storage container on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="OpenUserStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="WriteStorageFile"/>
    public static Storage? OpenFileStorage(string? path)
    {
        var storagePtr = SDL_OpenFileStorage(path);
        return storagePtr == IntPtr.Zero ? null : new Storage(storagePtr);
    }

    
    [DllImport(SDLLibrary, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern IntPtr SDL_OpenStorage([In] in StorageInterface iface, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC SDL_Storage *SDLCALL SDL_OpenStorage(const SDL_StorageInterface *iface, void *userdata);</code>
    /// <summary>
    /// <para>Opens up a container using a client-provided storage interface.</para>
    /// <para>Applications do not need to use this function unless they are providing
    /// their own SDL_Storage implementation. If you just need an SDL_Storage, you
    /// should use the built-in implementations in SDL, like <see cref="OpenTitleStorage"/>
    /// or <see cref="OpenUserStorage"/>.</para>
    /// </summary>
    /// <param name="iface">the function table to be used by this container.</param>
    /// <param name="userdata">the pointer that will be passed to the store interface.</param>
    /// <returns>a storage container on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static Storage? OpenStorage(StorageInterface iface, IntPtr userdata)
    {
        var storagePtr = SDL_OpenStorage(iface, userdata);
        return storagePtr == IntPtr.Zero ? null : new Storage(storagePtr);
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CloseStorage(IntPtr storage);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CloseStorage(SDL_Storage *storage);</code>
    /// <summary>
    /// <para>Closes and frees a storage container.</para>
    /// </summary>
    /// <param name="storage">a storage container to close.</param>
    /// <returns>0 if the container was freed with no errors, a negative value
    /// otherwise; call <see cref="GetError"/> for more information. Even if the
    /// function returns an error, the container data will be freed; the
    /// error is only for informational purposes.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="OpenFileStorage"/>
    /// <seealso cref="OpenStorage"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="OpenUserStorage"/>
    public static int CloseStorage(Storage storage) => SDL_CloseStorage(storage.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_StorageReady(IntPtr storage);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_StorageReady(SDL_Storage *storage);</code>
    /// <summary>
    /// <para>Checks if the storage container is ready to use.</para>
    /// <para>This function should be called in regular intervals until it returns
    /// <c>true</c> - however, it is not recommended to spinwait on this call, as the
    /// backend may depend on a synchronous message loop.</para>
    /// </summary>
    /// <param name="storage">a storage container to query.</param>
    /// <returns><c>true</c> if the container is ready, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool StorageReady(Storage storage) => SDL_StorageReady(storage.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetStorageFileSize(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, out ulong length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetStorageFileSize(SDL_Storage *storage, const char *path, Uint64 *length);</code>
    /// <summary>
    /// <para>Query the size of a file within a storage container.</para>
    /// </summary>
    /// <param name="storage">a storage container to query.</param>
    /// <param name="path">the relative path of the file to query.</param>
    /// <param name="length">a pointer to be filled with the file's length.</param>
    /// <returns>0 if the file could be queried, a negative value otherwise; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    public static int GetStorageFileSize(Storage storage, string path, out ulong length) => SDL_GetStorageFileSize(storage.Handle, path, out length);


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ReadStorageFile(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, IntPtr destination, ulong length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ReadStorageFile(SDL_Storage *storage, const char *path, void *destination, Uint64 length);</code>
    /// <summary>
    /// <para>Synchronously read a file from a storage container into a client-provided
    /// buffer.</para>
    /// </summary>
    /// <param name="storage">a storage container to read from.</param>
    /// <param name="path">the relative path of the file to read.</param>
    /// <param name="destination">a client-provided buffer to read the file into.</param>
    /// <returns>0 if the file was read, a negative value otherwise; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static int ReadStorageFile(Storage storage, string path, byte[] destination)
    {
        unsafe
        {
            fixed (byte* destPtr = destination)
            {
                return SDL_ReadStorageFile(storage.Handle, path, (IntPtr)destPtr, (ulong)destination.Length);
            }
        }
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_WriteStorageFile(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, IntPtr source, ulong length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_WriteStorageFile(SDL_Storage *storage, const char *path, const void *source, Uint64 length);</code>
    /// <summary>
    /// Synchronously write a file from client memory into a storage container.
    /// </summary>
    /// <param name="storage">a storage container to write to.</param>
    /// <param name="path">the relative path of the file to write.</param>
    /// <param name="source">a client-provided buffer to write from.</param>
    /// <returns>0 if the file was written, a negative value otherwise; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    public static int WriteStorageFile(Storage storage, string path, byte[] source)
    {
        unsafe
        {
            fixed (byte* sourcePtr = source)
            {
                return SDL_WriteStorageFile(storage.Handle, path, (IntPtr)sourcePtr, (ulong)source.Length);
            }
        }
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CreateStorageDirectory(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CreateStorageDirectory(SDL_Storage *storage, const char *path);</code>
    /// <summary>
    /// Create a directory in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to create.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StorageReady"/>
    public static int CreateStorageDirectory(Storage storage, string path) => SDL_CreateStorageDirectory(storage.Handle, path);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_EnumerateStorageDirectory(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, EnumerateDirectoryCallback callback, IntPtr userdata);
    /// <summary>
    /// <para>Enumerate a directory in a storage container through a callback function.</para>
    /// <para>This function provides every directory entry through an app-provided
    /// callback, called once for each directory entry, until all results have been
    /// provided or the callback returns &lt;= 0.</para>
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="callback">a function that is called for each entry in the directory.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StorageReady"/>
    public static int EnumerateStorageDirectory(Storage storage, string path, EnumerateDirectoryCallback callback, IntPtr userdata) => 
        SDL_EnumerateStorageDirectory(storage.Handle, path, callback, userdata);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RemoveStoragePath(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RemoveStoragePath(SDL_Storage *storage, const char *path);</code>
    /// <summary>
    /// Remove a file or an empty directory in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StorageReady"/>
    public static int RemoveStoragePath(Storage storage, string path) => SDL_RemoveStoragePath(storage.Handle, path);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenameStoragePath(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath, [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RenameStoragePath(SDL_Storage *storage, const char *oldpath, const char *newpath);</code>
    /// <summary>
    /// Rename a file or directory in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StorageReady"/>
    public static int RenameStoragePath(Storage storage, string oldpath, string newpath) => SDL_RenameStoragePath(storage.Handle, oldpath, newpath);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetStoragePathInfo(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, ref PathInfo info);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetStoragePathInfo(SDL_Storage *storage, const char *path, SDL_PathInfo *info);</code>
    /// <summary>
    /// Get information about a filesystem path in a storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path to query.</param>
    /// <param name="info">a pointer filled in with information about the path, or NULL to
    /// check for the existence of a file.</param>
    /// <returns>0 on success or a negative error code if the file doesn't exist,
    /// or another failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StorageReady"/>
    public static int GetStoragePathInfo(Storage storage, string path, ref PathInfo info)
    {
        return SDL_GetStoragePathInfo(storage.Handle, path, ref info);
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetStorageSpaceRemaining(IntPtr storage);
    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetStorageSpaceRemaining(SDL_Storage *storage);</code>
    /// <summary>
    /// Queries the remaining space in a storage container.
    /// </summary>
    /// <param name="storage">a storage container to query.</param>
    /// <returns>the amount of remaining space, in bytes.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static ulong GetStorageSpaceRemaining(Storage storage) => SDL_GetStorageSpaceRemaining(storage.Handle);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GlobStorageDirectory(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, [MarshalAs(UnmanagedType.LPUTF8Str)] string? pattern, uint flags, out int count);
    /// <code>extern SDL_DECLSPEC const char * const *SDLCALL SDL_GlobStorageDirectory(SDL_Storage *storage, const char *path, const char *pattern, SDL_GlobFlags flags, int *count);</code>
    /// <summary>
    /// <para>Enumerate a directory tree, filtered by pattern, and return a list.</para>
    /// <para>Files are filtered out if they don't match the string in <c>pattern</c>, which
    /// may contain wildcard characters <c>*</c> (match everything) and <c>?</c> (match one
    /// character). If pattern is <c>null</c>, no filtering is done and all results are
    /// returned. Subdirectories are permitted, and are specified with a path
    /// separator of <c>/</c>. Wildcard characters <c>*</c> and <c>?</c> never match a path
    /// separator.</para>
    /// <para>The returned array is always NULL-terminated, for your iterating
    /// convenience, but if <c>count</c> is non-NULL, on return it will contain the
    /// number of items in the array, not counting the <c>null</c> terminator.</para>
    /// <para>The returned pointer follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate.</param>
    /// <param name="pattern">the pattern that files in the directory must match. Can be
    /// <c>null</c>.</param>
    /// <param name="flags"><c>SDL_GLOB_*</c> bitflags that affect this search.</param>
    /// <param name="count">on return, will be set to the number of items in the returned
    /// array. Can be <c>null</c>.</param>
    /// <returns>an array of strings on success or NULL on failure; call
    /// <see cref="GetError"/> for more information. The caller should pass the
    /// returned pointer to <see cref="Free"/> when done with it.</returns>
    /// <threadsafety>It is safe to call this function from any thread, assuming
    /// the `storage` object is thread-safe.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string[]? GlobStorageDirectory(Storage storage, string path, string? pattern, uint flags, out int count)
    {
        var result = SDL_GlobStorageDirectory(storage.Handle, path, pattern, flags, out count);
    
        if (result == IntPtr.Zero)
        {
            return null;
        }

        try
        {
            var entries = new List<string>();
            for (var current = Marshal.ReadIntPtr(result); current != IntPtr.Zero; result += IntPtr.Size, current = Marshal.ReadIntPtr(result))
            {
                entries.Add(Marshal.PtrToStringUTF8(current)!);
            }

            return entries.ToArray();
        }
        finally
        {
            Free(result);
        }
    }

}