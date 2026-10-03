using SDL3.Tests;

namespace SDL3.Tests.SDL.FileAndIOAbstractions.IOStream;

internal static class MainlinePropsTests
{
    public static void RunAll()
    {
        TestAssert.Equal("SDL.iostream.openharmony.rawfile64", SDL3.SDL.Props.IOStreamOpenHarmonyRawFile64Pointer, "SDL_PROP_IOSTREAM_OPENHARMONY_RAWFILE64_POINTER must retain its native identifier.");
    }
}
