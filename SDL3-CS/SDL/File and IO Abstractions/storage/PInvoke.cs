#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenTitleStorage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenTitleStorage([MarshalAs(UnmanagedType.LPUTF8Str)] string? @override, uint props);
    private delegate IntPtr OpenTitleStorageNative(string? @override, uint props);
    private static OpenTitleStorageNative OpenTitleStorageNativeFunction = SDL_OpenTitleStorage;

    /// <code>extern SDL_DECLSPEC SDL_Storage * SDLCALL SDL_OpenTitleStorage(const char *override, SDL_PropertiesID props);</code>
    /// <summary>
    /// Opens up a read-only container for the application's filesystem.
    /// <para>By default, <see cref="OpenTitleStorage"/> uses the generic storage implementation.
    /// When the path override is not provided, the generic implementation will use
    /// the output of <see cref="GetBasePath"/> as the base path.</para>
    /// </summary>
    /// <param name="override">a path to override the backend's default title root.</param>
    /// <param name="props">a property list that may contain backend-specific information.</param>
    /// <returns>a title storage container on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="OpenUserStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    public static IntPtr OpenTitleStorage(string? @override, uint props)
    {
        return OpenTitleStorageNativeFunction(@override, props);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenUserStorage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenUserStorage([MarshalAs(UnmanagedType.LPUTF8Str)] string org, [MarshalAs(UnmanagedType.LPUTF8Str)] string app, uint props);
    private delegate IntPtr OpenUserStorageNative(string org, string app, uint props);
    private static OpenUserStorageNative OpenUserStorageNativeFunction = SDL_OpenUserStorage;

    /// <code>extern SDL_DECLSPEC SDL_Storage * SDLCALL SDL_OpenUserStorage(const char *org, const char *app, SDL_PropertiesID props);</code>
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
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static IntPtr OpenUserStorage(string org, string app, uint props)
    {
        return OpenUserStorageNativeFunction(org, app, props);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenFileStorage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenFileStorage([MarshalAs(UnmanagedType.LPUTF8Str)] string? path);
    private delegate IntPtr OpenFileStorageNative(string? path);
    private static OpenFileStorageNative OpenFileStorageNativeFunction = SDL_OpenFileStorage;

    /// <code>extern SDL_DECLSPEC SDL_Storage * SDLCALL SDL_OpenFileStorage(const char *path);</code>
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
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="OpenUserStorage"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="WriteStorageFile"/>
    public static IntPtr OpenFileStorage(string? path)
    {
        return OpenFileStorageNativeFunction(path);
    }
    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_OpenStorage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static extern IntPtr SDL_OpenStorage(in StorageInterface iface, IntPtr userdata);
    private delegate IntPtr OpenStorageNative(in StorageInterface iface, IntPtr userdata);
    private static OpenStorageNative OpenStorageNativeFunction = SDL_OpenStorage;

    /// <code>extern SDL_DECLSPEC SDL_Storage * SDLCALL SDL_OpenStorage(const SDL_StorageInterface *iface, void *userdata);</code>
    /// <summary>
    /// <para>Opens up a container using a client-provided storage interface.</para>
    /// <para>Applications do not need to use this function unless they are providing
    /// their own SDL_Storage implementation. If you just need an SDL_Storage, you
    /// should use the built-in implementations in SDL, like <see cref="OpenTitleStorage"/>
    /// or <see cref="OpenUserStorage"/>.</para>
    /// <para>This function makes a copy of <c>iface</c> and the caller does not need to keep
    /// it around after this call.</para>
    /// </summary>
    /// <param name="iface">the interface that implements this storage, initialized using
    /// <see cref="InitInterface(ref StorageInterface)"/>.</param>
    /// <param name="userdata">the pointer that will be passed to the interface functions.</param>
    /// <returns>a storage container on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseStorage"/>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="InitInterface(ref StorageInterface)"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static IntPtr OpenStorage(in StorageInterface iface, IntPtr userdata)
    {
        return OpenStorageNativeFunction(in iface, userdata);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseStorage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CloseStorage(IntPtr storage);
    private delegate bool CloseStorageNative(IntPtr storage);
    private static CloseStorageNative CloseStorageNativeFunction = SDL_CloseStorage;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CloseStorage(SDL_Storage *storage);</code>
    /// <summary>
    /// <para>Closes and frees a storage container.</para>
    /// </summary>
    /// <param name="storage">a storage container to close.</param>
    /// <returns><c>true</c> if the container was freed with no errors, <c>false</c> otherwise;
    /// call <see cref="GetError"/> for more information. Even if the function
    /// returns an error, the container data will be freed; the error is
    /// only for informational purposes.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenFileStorage"/>
    /// <seealso cref="OpenStorage"/>
    /// <seealso cref="OpenTitleStorage"/>
    /// <seealso cref="OpenUserStorage"/>
    public static bool CloseStorage(IntPtr storage)
    {
        return CloseStorageNativeFunction(storage);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StorageReady"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_StorageReady(IntPtr storage);
    private delegate bool StorageReadyNative(IntPtr storage);
    private static StorageReadyNative StorageReadyNativeFunction = SDL_StorageReady;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StorageReady(SDL_Storage *storage);</code>
    /// <summary>
    /// <para>Checks if the storage container is ready to use.</para>
    /// <para>This function should be called in regular intervals until it returns <c>true</c> -
    /// however, it is not recommended to spinwait on this call, as the backend may
    /// depend on a synchronous message loop. You might instead poll this
    /// game's main loop while processing events and drawing a loading screen.</para>
    /// </summary>
    /// <param name="storage">a storage container to query.</param>
    /// <returns><c>true</c> if the container is ready, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool StorageReady(IntPtr storage)
    {
        return StorageReadyNativeFunction(storage);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetStorageFileSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetStorageFileSize(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, out ulong length);
    private delegate bool GetStorageFileSizeNative(IntPtr storage, string path, out ulong length);
    private static GetStorageFileSizeNative GetStorageFileSizeNativeFunction = SDL_GetStorageFileSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetStorageFileSize(SDL_Storage *storage, const char *path, Uint64 *length);</code>
    /// <summary>
    /// Query the size of a file within a storage container.
    /// </summary>
    /// <param name="storage">a storage container to query.</param>
    /// <param name="path">the relative path of the file to query.</param>
    /// <param name="length">a pointer to be filled with the file's length.</param>
    /// <returns><c>true</c> if the file could be queried or <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    public static bool GetStorageFileSize(IntPtr storage, string path, out ulong length)
    {
        return GetStorageFileSizeNativeFunction(storage, path, out length);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadStorageFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadStorageFile(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, IntPtr destination, ulong length);
    private delegate bool ReadStorageFileNative(IntPtr storage, string path, IntPtr destination, ulong length);
    private static ReadStorageFileNative ReadStorageFileNativeFunction = SDL_ReadStorageFile;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadStorageFile(SDL_Storage *storage, const char *path, void *destination, Uint64 length);</code>
    /// <summary>
    /// <para>Synchronously read a file from a storage container into a client-provided
    /// buffer.</para>
    /// <para>The value of <c>length</c> must match the length of the file exactly; call
    /// <see cref="GetStorageFileSize"/> to get this value. This behavior may be relaxed in
    /// a future release.</para>
    /// </summary>
    /// <param name="storage">a storage container to read from.</param>
    /// <param name="path">the relative path of the file to read.</param>
    /// <param name="destination">a client-provided buffer to read the file into.</param>
    /// <param name="length">the length of the destination buffer.</param>
    /// <returns><c>true</c> if the file was read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetStorageFileSize"/>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static bool ReadStorageFile(IntPtr storage, string path, IntPtr destination, ulong length)
    {
        return ReadStorageFileNativeFunction(storage, path, destination, length);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteStorageFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteStorageFile(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, IntPtr source, ulong length);
    private delegate bool WriteStorageFileNative(IntPtr storage, string path, IntPtr source, ulong length);
    private static WriteStorageFileNative WriteStorageFileNativeFunction = SDL_WriteStorageFile;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteStorageFile(SDL_Storage *storage, const char *path, const void *source, Uint64 length);</code>
    /// <summary>
    /// <para>Synchronously write a file from client memory into a storage container.</para>
    /// </summary>
    /// <param name="storage">a storage container to write to.</param>
    /// <param name="path">the relative path of the file to write.</param>
    /// <param name="source">a client-provided buffer to write from.</param>
    /// <param name="length">the length of the source buffer.</param>
    /// <returns><c>true</c> if the file was written or <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetStorageSpaceRemaining"/>
    /// <seealso cref="ReadStorageFile"/>
    /// <seealso cref="StorageReady"/>
    public static bool WriteStorageFile(IntPtr storage, string path, IntPtr source, ulong length)
    {
        return WriteStorageFileNativeFunction(storage, path, source, length);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateStorageDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CreateStorageDirectory(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    private delegate bool CreateStorageDirectoryNative(IntPtr storage, string path);
    private static CreateStorageDirectoryNative CreateStorageDirectoryNativeFunction = SDL_CreateStorageDirectory;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CreateStorageDirectory(SDL_Storage *storage, const char *path);</code>
    /// <summary>
    /// Create a directory in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to create.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    public static bool CreateStorageDirectory(IntPtr storage, string path)
    {
        return CreateStorageDirectoryNativeFunction(storage, path);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EnumerateStorageDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_EnumerateStorageDirectory(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, EnumerateDirectoryCallback callback, IntPtr userdata);
    private delegate bool EnumerateStorageDirectoryNative(IntPtr storage, string path, EnumerateDirectoryCallback callback, IntPtr userdata);
    private static EnumerateStorageDirectoryNative EnumerateStorageDirectoryNativeFunction = SDL_EnumerateStorageDirectory;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_EnumerateStorageDirectory(SDL_Storage *storage, const char *path, SDL_EnumerateDirectoryCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Enumerate a directory in a storage container through a callback function.</para>
    /// <para>This function provides every directory entry through an app-provided
    /// callback, called once for each directory entry, until all results have been
    /// provided or the callback returns either <see cref="EnumerationResult.Success"/> or
    /// <see cref="EnumerationResult.Failure"/>.</para>
    /// <para>This will return <c>false</c> if there was a system problem in general, or if a
    /// callback returns <see cref="EnumerationResult.Failure"/>. A successful return means a callback
    /// returned <see cref="EnumerationResult.Success"/> to halt enumeration, or all directory entries
    /// were enumerated.</para>
    /// <para>If <c>path</c> is <c>null</c>, this is treated as a request to enumerate the root of
    /// the storage container's tree. An empty string also works for this.</para>
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate, or <c>null</c> for the root</param>
    /// <param name="callback">a function that is called for each entry in the directory.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    public static bool EnumerateStorageDirectory(IntPtr storage, string path, EnumerateDirectoryCallback callback, IntPtr userdata)
    {
        return EnumerateStorageDirectoryNativeFunction(storage, path, callback, userdata);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveStoragePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RemoveStoragePath(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    private delegate bool RemoveStoragePathNative(IntPtr storage, string path);
    private static RemoveStoragePathNative RemoveStoragePathNativeFunction = SDL_RemoveStoragePath;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RemoveStoragePath(SDL_Storage *storage, const char *path);</code>
    /// <summary>
    /// Remove a file or an empty directory in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path to remove from the filesystem.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    public static bool RemoveStoragePath(IntPtr storage, string path)
    {
        return RemoveStoragePathNativeFunction(storage, path);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenameStoragePath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenameStoragePath(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath, [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);
    private delegate bool RenameStoragePathNative(IntPtr storage, string oldpath, string newpath);
    private static RenameStoragePathNative RenameStoragePathNativeFunction = SDL_RenameStoragePath;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenameStoragePath(SDL_Storage *storage, const char *oldpath, const char *newpath);</code>
    /// <summary>
    /// Rename a file or directory in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    public static bool RenameStoragePath(IntPtr storage, string oldpath, string newpath)
    {
        return RenameStoragePathNativeFunction(storage, oldpath, newpath);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CopyStorageFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CopyStorageFile(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath, [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);
    private delegate bool CopyStorageFileNative(IntPtr storage, string oldpath, string newpath);
    private static CopyStorageFileNative CopyStorageFileNativeFunction = SDL_CopyStorageFile;

    /// <code><code>extern SDL_DECLSPEC bool SDLCALL SDL_CopyStorageFile(SDL_Storage *storage, const char *oldpath, const char *newpath);</code></code>
    /// <summary>
    /// Copy a file in a writable storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="oldpath">the old path.</param>
    /// <param name="newpath">the new path.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    public static bool CopyStorageFile(IntPtr storage, string oldpath, string newpath)
    {
        return CopyStorageFileNativeFunction(storage, oldpath, newpath);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetStoragePathInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetStoragePathInfo(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, out PathInfo info);
    private delegate bool GetStoragePathInfoNative(IntPtr storage, string path, out PathInfo info);
    private static GetStoragePathInfoNative GetStoragePathInfoNativeFunction = SDL_GetStoragePathInfo;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetStoragePathInfo(SDL_Storage *storage, const char *path, SDL_PathInfo *info);</code>
    /// <summary>
    /// Get information about a filesystem path in a storage container.
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path to query.</param>
    /// <param name="info">a pointer filled in with information about the path, or <c>null</c> to
    /// check for the existence of a file.</param>
    /// <returns><c>true</c> on success or <c>false</c> if the file doesn't exist, or another
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    public static bool GetStoragePathInfo(IntPtr storage, string path, out PathInfo info)
    {
        return GetStoragePathInfoNativeFunction(storage, path, out info);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetStorageSpaceRemaining"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetStorageSpaceRemaining(IntPtr storage);
    private delegate ulong GetStorageSpaceRemainingNative(IntPtr storage);
    private static GetStorageSpaceRemainingNative GetStorageSpaceRemainingNativeFunction = SDL_GetStorageSpaceRemaining;

    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetStorageSpaceRemaining(SDL_Storage *storage);</code>
    /// <summary>
    /// Queries the remaining space in a storage container.
    /// </summary>
    /// <param name="storage">a storage container to query.</param>
    /// <returns>the amount of remaining space, in bytes.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StorageReady"/>
    /// <seealso cref="WriteStorageFile"/>
    public static ulong GetStorageSpaceRemaining(IntPtr storage)
    {
        return GetStorageSpaceRemainingNativeFunction(storage);
    }
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GlobStorageDirectory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GlobStorageDirectory(IntPtr storage, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, [MarshalAs(UnmanagedType.LPUTF8Str)] string? pattern, GlobFlags flags, out int count);
    private delegate IntPtr GlobStorageDirectoryNative(IntPtr storage, string path, string? pattern, GlobFlags flags, out int count);
    private static GlobStorageDirectoryNative GlobStorageDirectoryNativeFunction = SDL_GlobStorageDirectory;

    /// <code>extern SDL_DECLSPEC char ** SDLCALL SDL_GlobStorageDirectory(SDL_Storage *storage, const char *path, const char *pattern, SDL_GlobFlags flags, int *count);</code>
    /// <summary>
    /// <para>Enumerate a directory tree, filtered by pattern, and return a list.</para>
    /// <para>Files are filtered out if they don't match the string in <c>pattern</c>, which
    /// may contain wildcard characters '*' (match everything) and '?' (match one
    /// character). If pattern is <c>null</c>, no filtering is done and all results are
    /// returned. Subdirectories are permitted, and are specified with a path
    /// separator of '/'. Wildcard characters '*' and '?' never match a path
    /// separator.</para>
    /// <para><c>flags</c> may be set to <see cref="GlobFlags.CaseInsensitive"/> to make the pattern matching
    /// case-insensitive.</para>
    /// <para>The returned array is always <c>null</c>-terminated, for your iterating
    /// convenience, but if <c>count</c> is non-<c>null</c>, on return it will contain the
    /// number of items in the array, not counting the <c>null</c> terminator.</para>
    /// <para>If <c>path</c> is <c>null</c>, this is treated as a request to enumerate the root of
    /// the storage container's tree. An empty string also works for this.</para>
    /// </summary>
    /// <param name="storage">a storage container.</param>
    /// <param name="path">the path of the directory to enumerate, or <c>null</c> for the root.</param>
    /// <param name="pattern">the pattern that files in the directory must match. Can be
    /// <c>null</c>.</param>
    /// <param name="flags"><see cref="GlobFlags"/> bitflags that affect this search.</param>
    /// <param name="count">on return, will be set to the number of items in the returned
    /// array. Can be <c>null</c>.</param>
    /// <returns>an array of strings on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. The caller should pass the
    /// returned pointer to <see cref="Free"/> when done with it. This is a single
    /// allocation that should be freed with <see cref="Free"/> when it is no
    /// longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread, assuming
    /// the <c>storage</c> object is thread-safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string[]? GlobStorageDirectory(IntPtr storage, string path, string? pattern, GlobFlags flags, out int count)
    {
        var ptr = GlobStorageDirectoryNativeFunction(storage, path, pattern, flags, out count);

        try
        {
            return PointerToStringArray(ptr, count);
        }
        finally
        {
            if(ptr != IntPtr.Zero) Free(ptr);
        }
    }
}
