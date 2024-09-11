using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef int (SDLCALL *SDL_EnumerateDirectoryCallback)(void *userdata, const char *dirname, const char *fname);</code>
    /// <summary>
    /// Callback for directory enumeration. Return 1 to keep enumerating,
    /// 0 to stop enumerating (no error), -1 to stop enumerating an
    /// report an error. <c>dirname</c> is the directory being enumerated,
    /// <c>fname</c> is the enumerated entry.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EnumerateDirectoryCallback(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string dirname, [MarshalAs(UnmanagedType.LPUTF8Str)] string fname);

}