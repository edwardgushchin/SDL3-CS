using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Video;

internal static class MainlinePropsTests
{
    public static void RunAll()
    {
        TestAssert.Equal("SDL.video.wayland.session_id", SDL3.SDL.Props.GlobalVideoWaylandSessionIDString, "SDL_PROP_GLOBAL_VIDEO_WAYLAND_SESSION_ID_STRING must retain its native identifier.");
        TestAssert.Equal("SDL.window.create.wayland.window_id", SDL3.SDL.Props.WindowCreateWaylandWindowIDString, "SDL_PROP_WINDOW_CREATE_WAYLAND_WINDOW_ID_STRING must retain its native identifier.");
        TestAssert.Equal("SDL.window.create.wayland.enable_insets", SDL3.SDL.Props.WindowCreateWaylandEnableInsetsBoolean, "SDL_PROP_WINDOW_CREATE_WAYLAND_ENABLE_INSETS_BOOLEAN must retain its native identifier.");
        TestAssert.Equal("SDL.window.create.win32.style_ex", SDL3.SDL.Props.WindowCreateWin32StyleExNumber, "SDL_PROP_WINDOW_CREATE_WIN32_STYLE_EX_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.window.openharmony.xcomponent", SDL3.SDL.Props.WindowOpenHarmonyXComponentPointer, "SDL_PROP_WINDOW_OPENHARMONY_XCOMPONENT_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.window.openharmony.window", SDL3.SDL.Props.WindowOpenHarmonyWindowPointer, "SDL_PROP_WINDOW_OPENHARMONY_WINDOW_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.window.openharmony.surface", SDL3.SDL.Props.WindowOpenHarmonySurfacePointer, "SDL_PROP_WINDOW_OPENHARMONY_SURFACE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.window.wayland.window_id", SDL3.SDL.Props.WindowWaylandWindowIDString, "SDL_PROP_WINDOW_WAYLAND_WINDOW_ID_STRING must retain its native identifier.");
        TestAssert.Equal("SDL.window.wayland.border_inset_left", SDL3.SDL.Props.WindowWaylandBorderInsetLeftNumber, "SDL_PROP_WINDOW_WAYLAND_BORDER_INSET_LEFT_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.window.wayland.border_inset_top", SDL3.SDL.Props.WindowWaylandBorderInsetTopNumber, "SDL_PROP_WINDOW_WAYLAND_BORDER_INSET_TOP_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.window.wayland.border_inset_right", SDL3.SDL.Props.WindowWaylandBorderInsetRightNumber, "SDL_PROP_WINDOW_WAYLAND_BORDER_INSET_RIGHT_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.window.wayland.border_inset_bottom", SDL3.SDL.Props.WindowWaylandBorderInsetBottomNumber, "SDL_PROP_WINDOW_WAYLAND_BORDER_INSET_BOTTOM_NUMBER must retain its native identifier.");
    }
}
