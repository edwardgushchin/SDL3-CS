using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Tray;

internal static class PropsTests
{
    public static void TrayCreatePropertyNames_MatchNativeIdentifiers()
    {
        TestAssert.Equal("SDL.tray.create.icon", SDL3.SDL.Props.TrayCreateIconPointer, "SDL.Props.TrayCreateIconPointer must match SDL_PROP_TRAY_CREATE_ICON_POINTER.");
        TestAssert.Equal("SDL.tray.create.tooltip", SDL3.SDL.Props.TrayCreateTooltipString, "SDL.Props.TrayCreateTooltipString must match SDL_PROP_TRAY_CREATE_TOOLTIP_STRING.");
        TestAssert.Equal("SDL.tray.create.userdata", SDL3.SDL.Props.TrayCreateUserdataPointer, "SDL.Props.TrayCreateUserdataPointer must match SDL_PROP_TRAY_CREATE_USERDATA_POINTER.");
        TestAssert.Equal("SDL.tray.create.leftclick_callback", SDL3.SDL.Props.TrayCreateLeftClickCallbackPointer, "SDL.Props.TrayCreateLeftClickCallbackPointer must match SDL_PROP_TRAY_CREATE_LEFTCLICK_CALLBACK_POINTER.");
        TestAssert.Equal("SDL.tray.create.rightclick_callback", SDL3.SDL.Props.TrayCreateRightClickCallbackPointer, "SDL.Props.TrayCreateRightClickCallbackPointer must match SDL_PROP_TRAY_CREATE_RIGHTCLICK_CALLBACK_POINTER.");
        TestAssert.Equal("SDL.tray.create.middleclick_callback", SDL3.SDL.Props.TrayCreateMiddleClickCallbackPointer, "SDL.Props.TrayCreateMiddleClickCallbackPointer must match SDL_PROP_TRAY_CREATE_MIDDLECLICK_CALLBACK_POINTER.");
    }
}
