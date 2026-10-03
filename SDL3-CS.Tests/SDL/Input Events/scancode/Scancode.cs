namespace SDL3.Tests.SDL.InputEvents.Scancode;

internal static class ScancodeTests
{
    public static void Front_MatchesStableNativeValue()
    {
        TestAssert.Equal(165, (int)SDL3.SDL.Scancode.Front, "SDL_SCANCODE_FRONT must match SDL 3.4.18.");
    }
}
