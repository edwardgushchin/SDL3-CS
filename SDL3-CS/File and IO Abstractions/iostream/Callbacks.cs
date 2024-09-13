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
    /// <code>Sint64 (SDLCALL *size)(void *userdata);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long SizeDelegate(IntPtr userdata);

    /// <code>Sint64 (SDLCALL *seek)(void *userdata, Sint64 offset, SDL_IOWhence whence);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long SeekDelegate(IntPtr userdata, long offset, IOWhence whence);

    /// <code>size_t (SDLCALL *read)(void *userdata, void *ptr, size_t size, SDL_IOStatus *status);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ReadDelegate(IntPtr userdata, IntPtr ptr, UIntPtr size, out IOStatus status);

    /// <code>size_t (SDLCALL *write)(void *userdata, const void *ptr, size_t size, SDL_IOStatus *status);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr WriteDelegate(IntPtr userdata, IntPtr ptr, UIntPtr size, out IOStatus status);

    /// <code>int (SDLCALL *close)(void *userdata);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CloseDelegate(IntPtr userdata);
}