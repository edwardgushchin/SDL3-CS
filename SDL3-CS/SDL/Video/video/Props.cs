#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

namespace SDL3;

public static partial class SDL
{
	/// <summary>
	/// <para>The pointer to the global `wl_display` object used by the Wayland video
	/// backend.</para>
	/// <para>Can be set before the video subsystem is initialized to import an external
	/// `wl_display` object from an application or toolkit for use in SDL, or read
	/// after initialization to export the `wl_display` used by the Wayland video
	/// backend. Setting this property after the video subsystem has been
	/// initialized has no effect, and reading it when the video subsystem is
	/// uninitialized will either return the user provided value, if one was set
	/// prior to initialization, or NULL. See docs/README-wayland.md for more
	/// information.</para>
	/// </summary>
	public const string PropGlobalVideoWaylandWLDisplayPointer = "SDL.video.wayland.wl_display";
	
	/// <summary>
	/// true if the display has HDR headroom above the SDR white point. This is for informational and
	/// diagnostic purposes only, as not all platforms provide this information
	/// at the display level.
	/// </summary>
	public const string PropDisplayHDREnabledBoolean = "SDL.display.HDR_enabled";
	
	/// <summary>
	/// The "panel orientation"
	/// property for the display in degrees of clockwise rotation. Note that this
	/// is provided only as a hint, and the application is responsible for any
	/// coordinate transformations needed to conform to the requested display
	/// orientation.
	/// </summary>
    public const string PropDisplayKMSDRMPanelOrientationNumber = "SDL.display.KMSDRM.panel_orientation";
    
    /// <summary>
    /// True if the window should be always on top
    /// </summary>
    public const string PropWindowCreateAlwaysOnTopBoolean = "always_on_top";
    
    /// <summary>
    /// True if the window has no window decoration
    /// </summary>
	public const string PropWindowCreateBorderlessBoolean = "borderless";
    
    /// <summary>
    /// True if the window should accept keyboard input (defaults true)
    /// </summary>
	public const string PropWindowCreateFocusableBoolean = "focusable";
	
	/// <summary>
	/// True if the window will be used with an externally managed graphics context.
	/// </summary>
	public const string PropWindowCreateExternalGraphicsContextBoolean = "external_graphics_context";
	
	/// <summary>
	/// True if the window should start in fullscreen mode at desktop resolution
	/// </summary>
	public const string PropWindowCreateFullscreenBoolean = "fullscreen";
	
	/// <summary>
	/// The height of the window
	/// </summary>
	public const string PropWindowCreateHeightNumber = "height";
	
	/// <summary>
	/// True if the window should start hidden
	/// </summary>
	public const string PropWindowCreateHiddenBoolean = "hidden";
	
	/// <summary>
	/// True if the window uses a high pixel density buffer if possible
	/// </summary>
	public const string PropWindowCreateHighPixelDensityBoolean = "high_pixel_density";
	
	/// <summary>
	/// True if the window should start maximized
	/// </summary>
	public const string PropWindowCreateMaximizedBoolean = "maximized";
	
	/// <summary>
	/// True if the window is a popup menu
	/// </summary>
	public const string PropWindowCreateMenuBoolean = "menu";
	
	/// <summary>
	/// True if the window will be used with Metal rendering
	/// </summary>
	public const string PropWindowCreateMetalBoolean = "metal";
	
	/// <summary>
	/// True if the window should start minimized
	/// </summary>
	public const string PropWindowCreateMinimizedBoolean = "minimized";
	
	/// <summary>
	/// True if the window is modal to its parent
	/// </summary>
	public const string PropWindowCreateModalBoolean = "modal";
	
	/// <summary>
	/// True if the window starts with grabbed mouse focus
	/// </summary>
	public const string PropWindowCreateMouseGrabbedBoolean = "mouse_grabbed";
	
	/// <summary>
	/// True if the window will be used with OpenGL rendering
	/// </summary>
	public const string PropWindowCreateOpenGLBoolean = "opengl";
	
	/// <summary>
	/// An <see cref="Window"/> that will be the parent of this window, required for windows with the "toolip", "menu",
	/// and "modal" properties
	/// </summary>
	public const string PropWindowCreateParentPointer = "parent";
	
	/// <summary>
	/// True if the window should be resizable
	/// </summary>
	public const string PropWindowCreateResizableBoolean = "resizable";
	
	/// <summary>
	/// The title of the window, in UTF-8 encoding
	/// </summary>
	public const string PropWindowCreateTitleString = "title";
	
	/// <summary>
	/// True if the window show transparent in the areas with alpha of 0
	/// </summary>
	public const string PropWindowCreateTransparentBoolean = "transparent";
	
	/// <summary>
	/// True if the window is a tooltip
	/// </summary>
	public const string PropWindowCreateTooltipBoolean = "tooltip";
	
	/// <summary>
	/// True if the window is a utility window, not showing in the task bar and window list
	/// </summary>
	public const string PropWindowCreateUtilityBoolean = "utility";
	
	/// <summary>
	/// True if the window will be used with Vulkan rendering
	/// </summary>
	public const string PropWindowCreateVulkanBoolean = "vulkan";
	
	/// <summary>
	/// The width of the window
	/// </summary>
	public const string PropWindowCreateWidthNumber = "width";
	
	/// <summary>
	/// The x position of the window, or
	/// <see cref="WindowPosCentered"/>, defaults to <see cref="WindowPosUndefined"/>`. This is
	/// relative to the parent for windows with the "parent" property set.
	/// </summary>
	public const string PropWindowCreateXNumber = "x";
	
	/// <summary>
	/// The y position of the window, or
	/// <see cref="WindowPosCentered"/>, defaults to <see cref="WindowPosUndefined"/>`. This is
	/// relative to the parent for windows with the "parent" property set.
	/// </summary>
	public const string PropWindowCreateYNumber = "y";
	
	/// <summary>
	/// The`(__unsafe_unretained)` NSWindow associated with the window, if you want to wrap an existing window.
	/// </summary>
	public const string PropWindowCreateCocoaWindowPointer = "cocoa.window";
	
	/// <summary>
	/// The `(__unsafe_unretained)`NSView associated with the window, defaults to `[window contentView]`
	/// </summary>
	public const string PropWindowCreateCocoaViewPointer = "cocoa.view";
	
	/// <summary>
	/// True if
	/// the application wants to use the Wayland surface for a custom role and
	/// does not want it attached to an XDG toplevel window. See
	/// [README/wayland](README/wayland) for more information on using custom
	/// surfaces.
	/// </summary>
	public const string PropWindowCreateWaylandSurfaceRoleCustomBoolean = "wayland.surface_role_custom";
	
	/// <summary>
	/// True if the
	/// application wants an associated `wl_egl_window` object to be created,
	/// even if the window does not have the OpenGL property or flag set.
	/// </summary>
	public const string PropWindowCreateCreateEGLWindowBoolean = "wayland.create_egl_window";
	
	/// <summary>
	/// The wl_surface
	/// associated with the window, if you want to wrap an existing window. See
	/// [README/wayland](README/wayland) for more information.
	/// </summary>
	public const string PropWindowCreateWaylandWLSurfacePointer = "wayland.wl_surface";
	
	/// <summary>
	/// The HWND associated with the window, if you want to wrap an existing window.
	/// </summary>
	public const string PropWindowCreateWin32HWNDPointer = "win32.hwnd";
	
	/// <summary>
	/// Optional, another window to share pixel format with, useful for OpenGL windows
	/// </summary>
	public const string PropWindowCreateWin32PixelFormatHWNDPointer = "win32.pixel_format_hwnd";
	
	/// <summary>
	/// The X11 Window associated with the window, if you want to wrap an existing window.
	/// </summary>
	public const string PropWindowCreateX11WindowNumber = "x11.window";
	
	/// <summary>
	/// The surface associated with a shaped window
	/// </summary>
	public const string PropWindowShapePointer = "SDL.window.shape";
	
	/// <summary>
	/// True if the window has HDR headroom above the SDR white point. This property can change dynamically
	/// when <see cref="EventType.WindowHDRStateChanged"/> is sent.
	/// </summary>
	public const string PropWindowHDREnabledBoolean = "SDL.window.HDR_enabled";
	
	/// <summary>
	/// The value of SDR white in the <see cref="Colorspace.SRGBLinear"/> colorspace. On Windows this corresponds to the
	/// SDR white level in scRGB colorspace, and on Apple platforms this is
	/// always 1.0 for EDR content. This property can change dynamically when
	/// <see cref="EventType.WindowHDRStateChanged"/> is sent.
	/// </summary>
	public const string PropWindowSDRWhiteLevelFloat = "SDL.window.SDR_white_level";
	
	/// <summary>
	/// The additional high dynamic range
	/// that can be displayed, in terms of the SDR white point. When HDR is not
	/// enabled, this will be 1.0. This property can change dynamically when
	/// <see cref="EventType.WindowHDRStateChanged"/> is sent.
	/// </summary>
	public const string PropWindowHDRHeadroomFloat = "SDL.window.HDR_headroom";
	
	/// <summary>
	/// The ANativeWindow associated with the window
	/// </summary>
	public const string PropWindowAndroidWindowPointer = "SDL.window.android.window";
	
	/// <summary>
	/// The EGLSurface associated with the window
	/// </summary>
	public const string PropWindowAndroidSurfacePointer = "SDL.window.android.surface";
	
	/// <summary>
	/// The `(__unsafe_unretained)` UIWindow associated with the window
	/// </summary>
	public const string PropWindowUIKitWindowPointer = "SDL.window.uikit.window";
	
	/// <summary>
	/// The NSInteger tag assocated with metal views on the window
	/// </summary>
	public const string PropWindowUIKitMetalViewTagNumber = "SDL.window.uikit.metal_view_tag";
	
	/// <summary>
	/// The OpenGL view's framebuffer object. It must be bound when rendering to the screen using OpenGL.
	/// </summary>
	public const string PropWindowUIKitOpenGLFrameBufferNumber = "SDL.window.uikit.opengl.framebuffer";
	
	/// <summary>
	/// The OpenGL view's renderbuffer object. It must be bound when <see cref="GLSwapWindow"/> is called.
	/// </summary>
	public const string PropWindowUIKitOpenGLRenderBufferNumber = "SDL.window.uikit.opengl.renderbuffer";
	
	/// <summary>
	/// The OpenGL view's resolve framebuffer, when MSAA is used.
	/// </summary>
	public const string PropWindowUIKitOpenGLResolveFrameBufferNumber = "SDL.window.uikit.opengl.resolve_framebuffer";
	
	/// <summary>
	/// The device index associated with the window (e.g. the X in /dev/dri/cardX)
	/// </summary>
	public const string PropWindowKMSDRMDeviceIndexNumber = "SDL.window.kmsdrm.dev_index";
	
	/// <summary>
	/// The DRM FD associated with the window
	/// </summary>
	public const string PropWindowKMSDRMDRMFDNumber = "SDL.window.kmsdrm.drm_fd";
	
	/// <summary>
	/// The GBM device associated with the window
	/// </summary>
	public const string PropWindowKMSDRMGBMDevicePointer = "SDL.window.kmsdrm.gbm_dev";
	
	/// <summary>
	/// The `(__unsafe_unretained)` NSWindow associated with the window
	/// </summary>
	public const string PropWindowCocoaWindowPointer = "SDL.window.cocoa.window";
	
	/// <summary>
	/// The NSInteger tag assocated with metal views on the window
	/// </summary>
	public const string PropWindowCocoaMetalViewTagNumber = "SDL.window.cocoa.metal_view_tag";
	
	/// <summary>
	/// The EGLNativeDisplayType associated with the window
	/// </summary>
	public const string PropWindowVivanteDisplayPointer = "SDL.window.vivante.display";
	
	/// <summary>
	/// The EGLNativeWindowType associated with the window
	/// </summary>
	public const string PropWindowVivanteWindowPointer = "SDL.window.vivante.window";
	
	/// <summary>
	/// The EGLSurface associated with the window
	/// </summary>
	public const string PropWindowVivanteSurfacePointer = "SDL.window.vivante.surface";
	
	/// <summary>
	/// The IInspectable CoreWindow associated with the window
	/// </summary>
	public const string PropWindowWinRTWindowPointer = "SDL.window.winrt.window";
	
	/// <summary>
	/// The HWND associated with the window
	/// </summary>
	public const string PropWindowWin32HWNDPointer = "SDL.window.win32.hwnd";
	
	/// <summary>
	/// The HDC associated with the window
	/// </summary>
	public const string PropWindowWin32HDCPointer = "SDL.window.win32.hdc";
	
	/// <summary>
	/// The HINSTANCE associated with the window
	/// </summary>
	public const string PropWindowWin32InstancePointer = "SDL.window.win32.instance";
	
	/// <summary>
	/// The wl_display associated with the window
	/// </summary>
	public const string PropWindowWaylandDisplayPointer = "SDL.window.wayland.display";
	
	/// <summary>
	/// The wl_surface associated with the window
	/// </summary>
	public const string PropWindowWaylandSurfacePointer = "SDL.window.wayland.surface";
	
	/// <summary>
	/// The wl_egl_window associated with the window
	/// </summary>
	public const string PropWindowWaylandEGLWindowPointer = "SDL.window.wayland.egl_window";
	
	/// <summary>
	/// The xdg_surface associated with the window
	/// </summary>
	public const string PropWindowWaylandXDGSurfacePointer = "SDL.window.wayland.xdg_surface";
	
	/// <summary>
	/// The xdg_toplevel role associated with the window
	/// </summary>
	public const string PropWindowWaylandXDGTopLevelPointer = "SDL.window.wayland.xdg_toplevel";
	
	/// <summary>
	/// The export handle associated with the window
	/// </summary>
	public const string PropWindowWaylandXDGTopLevelExportHandleString = "SDL.window.wayland.xdg_toplevel_export_handle";
	
	/// <summary>
	/// The xdg_popup role associated with the window
	/// </summary>
	public const string PropWindowWaylandXDGPopupPointer = "SDL.window.wayland.xdg_popup";
	
	/// <summary>
	/// The xdg_positioner associated with the window, in popup mode
	/// </summary>
	public const string PropWindowWaylandXDGPositionerPointer = "SDL.window.wayland.xdg_positioner";
	
	/// <summary>
	/// The X11 Display associated with the window
	/// </summary>
	public const string PropWindowX11DisplayPointer = "SDL.window.x11.display";
	
	/// <summary>
	/// The screen number associated with the window
	/// </summary>
	public const string PropWindowX11ScreenNumber = "SDL.window.x11.screen";
	
	/// <summary>
	/// The X11 Window associated with the window
	/// </summary>
	public const string PropWindowX11WindowNumber = "SDL.window.x11.window";
}