using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Dialog;

internal static class DialogFileFilterTests
{
    public static void Dispose_FreesAllocatedUtf8PointersAndAllowsDefault()
    {
        SDL3.SDL.DialogFileFilter filter = new("Text files", "txt;md");
        TestAssert.True(filter.Name != IntPtr.Zero, "SDL.DialogFileFilter must allocate a UTF-8 name pointer.");
        TestAssert.True(filter.Pattern != IntPtr.Zero, "SDL.DialogFileFilter must allocate a UTF-8 pattern pointer.");
        TestAssert.Equal("Text files", Marshal.PtrToStringUTF8(filter.Name), "SDL.DialogFileFilter must store the filter name as UTF-8.");
        TestAssert.Equal("txt;md", Marshal.PtrToStringUTF8(filter.Pattern), "SDL.DialogFileFilter must store the filter pattern as UTF-8.");

        filter.Dispose();
        default(SDL3.SDL.DialogFileFilter).Dispose();
    }
}
