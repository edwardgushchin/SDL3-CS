using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Hints;

internal static class HintTests
{
    public static void AndroidAAudioInputPreset_UsesNativeName()
    {
        TestAssert.Equal("SDL_ANDROID_AAUDIO_INPUT_PRESET", SDL3.SDL.Hints.AndroidAAudioInputPreset, "SDL.Hints.AndroidAAudioInputPreset must match SDL 3.4.16.");
    }
}

internal static class MainlineHintsTests
{
    public static void RunAll()
    {
        TestAssert.Equal("SDL_ANDROID_ALLOW_PERSISTENT_FOLDER_ACCESS", SDL3.SDL.Hints.AndroidAllowPersistentFolderAccess, "SDL_HINT_ANDROID_ALLOW_PERSISTENT_FOLDER_ACCESS must retain its native identifier.");
        TestAssert.Equal("SDL_AUDIO_DUCK_OTHERS", SDL3.SDL.Hints.AudioDuckOthers, "SDL_HINT_AUDIO_DUCK_OTHERS must retain its native identifier.");
        TestAssert.Equal("SDL_DOS_ALLOW_DIRECT_FRAMEBUFFER", SDL3.SDL.Hints.DOSAllowDirectFramebuffer, "SDL_HINT_DOS_ALLOW_DIRECT_FRAMEBUFFER must retain its native identifier.");
        TestAssert.Equal("SDL_OPENXR_LIBRARY", SDL3.SDL.Hints.OpenXRLibrary, "SDL_HINT_OPENXR_LIBRARY must retain its native identifier.");
        TestAssert.Equal("SDL_MAC_USE_GCMOUSE", SDL3.SDL.Hints.MacUseGCMouse, "SDL_HINT_MAC_USE_GCMOUSE must retain its native identifier.");
        TestAssert.Equal("SDL_VISIONOS_HDR_HEADROOM_UI", SDL3.SDL.Hints.VisionOSHDRHeadroomUI, "SDL_HINT_VISIONOS_HDR_HEADROOM_UI must retain its native identifier.");
        TestAssert.Equal("SDL_WINDOWS_RAW_MOUSE_NOLEGACY", SDL3.SDL.Hints.WindowsRawMouseNoLegacy, "SDL_HINT_WINDOWS_RAW_MOUSE_NOLEGACY must retain its native identifier.");
    }
}
