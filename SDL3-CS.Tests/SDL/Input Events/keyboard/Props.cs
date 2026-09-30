using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Keyboard;

internal static class MainlinePropsTests
{
    public static void RunAll()
    {
        TestAssert.Equal("SDL.textinput.openharmony.inputtype", SDL3.SDL.Props.TextInputOpenHarmonyInputTypeNumber, "SDL_PROP_TEXTINPUT_OPENHARMONY_INPUTTYPE_NUMBER must retain its native identifier.");
    }
}
