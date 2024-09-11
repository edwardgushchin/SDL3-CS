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