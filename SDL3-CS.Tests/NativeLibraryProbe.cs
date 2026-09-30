using System.Runtime.InteropServices;

namespace SDL3.Tests;

internal static class NativeLibraryProbe
{
    public static bool SupportsSDL3Export(string entryPoint)
    {
        string libraryName = OperatingSystem.IsWindows() ? "SDL3.dll" : OperatingSystem.IsMacOS() ? "SDL3" : "libSDL3.so";
        if (!NativeLibrary.TryLoad(libraryName, out IntPtr library))
        {
            return false;
        }

        try
        {
            return NativeLibrary.TryGetExport(library, entryPoint, out _);
        }
        finally
        {
            NativeLibrary.Free(library);
        }
    }
}
