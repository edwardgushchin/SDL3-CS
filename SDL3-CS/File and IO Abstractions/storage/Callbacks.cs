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
    /*[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CloseDelegate(IntPtr userdata);*/

    /// <code>SDL_bool (SDLCALL *ready)(void *userdata);</code>
    /// <summary>Optional, returns whether the storage is currently ready for access</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool ReadyDelegate(IntPtr userdata);

    
    /// <code>int (SDLCALL *enumerate)(void *userdata, const char *path, SDL_EnumerateDirectoryCallback callback, void *callback_userdata);</code>
    /// <summary>Enumerate a directory, optional for write-only storage</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EnumerateDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, EnumerateDirectoryCallback callback, IntPtr callback_userdata);

    
    /// <code>int (SDLCALL *info)(void *userdata, const char *path, SDL_PathInfo *info);</code>
    /// <summary>Get path information, optional for write-only storage</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int InfoDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, out PathInfo info);

    
    /// <code>Read a file from storage, optional for write-only storage</code>
    /// <summary>int (SDLCALL *read_file)(void *userdata, const char *path, void *destination, Uint64 length);</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ReadFileDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, IntPtr destination, ulong length);

    
    /// <code>Write a file to storage, optional for read-only storage</code>
    /// <summary>int (SDLCALL *write_file)(void *userdata, const char *path, const void *source, Uint64 length);</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int WriteFileDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, IntPtr source, ulong length);

    
    /// <code>Create a directory, optional for read-only storage</code>
    /// <summary>int (SDLCALL *mkdir)(void *userdata, const char *path);</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MkdirDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    
    /// <code>Remove a file or empty directory, optional for read-only storage</code>
    /// <summary>int (SDLCALL *remove)(void *userdata, const char *path);</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RemoveDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    
    /// <code>Rename a path, optional for read-only storage</code>
    /// <summary>int (SDLCALL *rename)(void *userdata, const char *oldpath, const char *newpath);</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RenameDelegate(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string oldpath, [MarshalAs(UnmanagedType.LPUTF8Str)] string newpath);

    
    /// <code>Uint64 (SDLCALL *space_remaining)(void *userdata);</code>
    /// <summary>Get the space remaining, optional for read-only storage</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong SpaceRemainingDelegate(IntPtr userdata);
}