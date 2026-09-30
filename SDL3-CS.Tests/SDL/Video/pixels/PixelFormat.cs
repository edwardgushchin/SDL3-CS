namespace SDL3.Tests.SDL.Video.Pixels;

internal static class MainlinePixelFormatTests
{
    public static void RunAll()
    {
        TestAssert.Equal(0x34343449u, (uint)SDL3.SDL.PixelFormat.I444, "I444 FourCC must match native.");
        TestAssert.Equal(0x4C463049u, (uint)SDL3.SDL.PixelFormat.I0FL, "I0FL FourCC must match native.");
        TestAssert.Equal(0x4C463449u, (uint)SDL3.SDL.PixelFormat.I4FL, "I4FL FourCC must match native.");
        TestAssert.Equal(1u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.I444), "8-bit planar I444 must report one byte per component.");
        TestAssert.Equal(2u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.I0FL), "16-bit planar I0FL must report two bytes per component.");
        TestAssert.Equal(2u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.I4FL), "16-bit planar I4FL must report two bytes per component.");
    }
}
