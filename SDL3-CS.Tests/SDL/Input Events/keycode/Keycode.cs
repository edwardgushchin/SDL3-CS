using SDL3;

namespace SDL3.Tests.SDL.InputEvents.Keycode;

internal static class KeycodeTests
{
    public static void Front_MatchesStableNativeValue()
    {
        TestAssert.Equal(0x400000a5u, (uint)SDL3.SDL.Keycode.Front, "SDLK_FRONT must match SDL 3.4.18.");
        TestAssert.Equal((uint)SDL3.SDL.Scancode.Front | (uint)SDL3.SDL.Keycode.ScanCodeMask, (uint)SDL3.SDL.Keycode.Front, "Front keycode must use the native scancode mask.");
    }
}
