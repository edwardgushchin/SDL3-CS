using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.System;

internal static class MainlinePropsTests
{
    public static void RunAll()
    {
        TestAssert.Equal("SDL.system.ubuntu_touch.appid", SDL3.SDL.Props.GlobalSystemUbuntuTouchAppIDString, "SDL_PROP_GLOBAL_SYSTEM_UBUNTU_TOUCH_APPID_STRING must retain its native identifier.");
        TestAssert.Equal("SDL.system.ubuntu_touch.hook", SDL3.SDL.Props.GlobalSystemUbuntuTouchHookString, "SDL_PROP_GLOBAL_SYSTEM_UBUNTU_TOUCH_HOOK_STRING must retain its native identifier.");
        TestAssert.Equal("SDL.system.ubuntu_touch.app_version", SDL3.SDL.Props.GlobalSystemUbuntuTouchAppVersionString, "SDL_PROP_GLOBAL_SYSTEM_UBUNTU_TOUCH_APP_VERSION_STRING must retain its native identifier.");
    }
}
