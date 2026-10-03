namespace SDL3.Tests.SDL.InputEvents.Scancode;

internal static class MainlineScancodeTests
{
    public static void RunAll() => TestAssert.Equal(165, (int)SDL3.SDL.Scancode.Front, "Front scancode must preserve USB HID value.");
}
