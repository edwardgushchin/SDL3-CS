using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.System;

internal static class FormFactorTests
{
    public static void FormFactor_UsesExpectedNativeValues()
    {
        TestAssert.Equal(typeof(int), Enum.GetUnderlyingType(typeof(SDL3.SDL.FormFactor)), "SDL.FormFactor must use the native int underlying type.");
        TestAssert.Equal(0, (int)SDL3.SDL.FormFactor.Unknown, "SDL.FormFactor.Unknown must match SDL_FORMFACTOR_UNKNOWN.");
        TestAssert.Equal(1, (int)SDL3.SDL.FormFactor.Desktop, "SDL.FormFactor.Desktop must match SDL_FORMFACTOR_DESKTOP.");
        TestAssert.Equal(2, (int)SDL3.SDL.FormFactor.Laptop, "SDL.FormFactor.Laptop must match SDL_FORMFACTOR_LAPTOP.");
        TestAssert.Equal(3, (int)SDL3.SDL.FormFactor.Phone, "SDL.FormFactor.Phone must match SDL_FORMFACTOR_PHONE.");
        TestAssert.Equal(4, (int)SDL3.SDL.FormFactor.Tablet, "SDL.FormFactor.Tablet must match SDL_FORMFACTOR_TABLET.");
        TestAssert.Equal(5, (int)SDL3.SDL.FormFactor.Console, "SDL.FormFactor.Console must match SDL_FORMFACTOR_CONSOLE.");
        TestAssert.Equal(6, (int)SDL3.SDL.FormFactor.Handheld, "SDL.FormFactor.Handheld must match SDL_FORMFACTOR_HANDHELD.");
        TestAssert.Equal(7, (int)SDL3.SDL.FormFactor.Watch, "SDL.FormFactor.Watch must match SDL_FORMFACTOR_WATCH.");
        TestAssert.Equal(8, (int)SDL3.SDL.FormFactor.TV, "SDL.FormFactor.TV must match SDL_FORMFACTOR_TV.");
        TestAssert.Equal(9, (int)SDL3.SDL.FormFactor.Headset, "SDL.FormFactor.Headset must match SDL_FORMFACTOR_HEADSET.");
        TestAssert.Equal(10, (int)SDL3.SDL.FormFactor.Car, "SDL.FormFactor.Car must match SDL_FORMFACTOR_CAR.");
    }
}
