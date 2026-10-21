using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Hints;

internal static class HintTests
{
    public static void AndroidAAudioInputPreset_UsesNativeName()
    {
        TestAssert.Equal("SDL_ANDROID_AAUDIO_INPUT_PRESET", SDL3.SDL.Hints.AndroidAAudioInputPreset, "SDL.Hints.AndroidAAudioInputPreset must match SDL 3.4.16.");
    }
}
