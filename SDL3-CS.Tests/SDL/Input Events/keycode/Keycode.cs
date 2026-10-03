namespace SDL3.Tests.SDL.InputEvents.Keycode;

internal static class MainlineKeycodeTests
{
    public static void RunAll() => TestAssert.Equal(0x400000a5u, (uint)SDL3.SDL.Keycode.Front, "Front keycode must preserve its scancode mask and value.");
}
