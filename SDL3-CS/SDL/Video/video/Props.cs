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
    public const string PropDisplayHDREnabledBoolean = "SDL.display.HDR_enabled";
    public const string PropDisplayKMSDRMPanelOrientationNumber = "SDL.display.KMSDRM.panel_orientation";
    
    public const string PropWindowCreateAlwaysOnTopBoolean = "always_on_top";
	public const string PropWindowCreateBorderlessBoolean = "borderless";
	public const string PropWindowCreateCocoaViewPointer = "cocoa.view";
	public const string PropWindowCreateCocoaWindowPointer = "cocoa.window";
	public const string PropWindowCreateCreateEGLWindowBoolean = "wayland.create_egl_window";
	public const string PropWindowCreateExternalGraphicsContextBoolean = "external_graphics_context";
	public const string PropWindowCreateFocusableBoolean = "focusable";
	public const string PropWindowCreateFullscreenBoolean = "fullscreen";
	public const string PropWindowCreateHeightNumber = "height";
	public const string PropWindowCreateHiddenBoolean = "hidden";
	public const string PropWindowCreateHighPixelDensityBoolean = "high_pixel_density";
	public const string PropWindowCreateMaximizedBoolean = "maximized";
	public const string PropWindowCreateMenuBoolean = "menu";
	public const string PropWindowCreateMetalBoolean = "metal";
	public const string PropWindowCreateMinimizedBoolean = "minimized";
	public const string PropWindowCreateModalBoolean = "modal";
	public const string PropWindowCreateMouseGrabbedBoolean = "mouse_grabbed";
	public const string PropWindowCreateOpenGLBoolean = "opengl";
	public const string PropWindowCreateParentPointer = "parent";
	public const string PropWindowCreateResizableBoolean = "resizable";
	public const string PropWindowCreateTitleString = "title";
	public const string PropWindowCreateTooltipBoolean = "tooltip";
	public const string PropWindowCreateTransparentBoolean = "transparent";
	public const string PropWindowCreateUtilityBoolean = "utility";
	public const string PropWindowCreateVulkanBoolean = "vulkan";
	public const string PropWindowCreateWaylandSurfaceRoleCustomBoolean = "wayland.surface_role_custom";
	public const string PropWindowCreateWaylandWLSurfacePointer = "wayland.wl_surface";
	public const string PropWindowCreateWidthNumber = "width";
	public const string PropWindowCreateWin32HwndPointer = "win32.hwnd";
	public const string PropWindowCreateWin32PixelFormatHwndPointer = "win32.pixel_format_hwnd";
	public const string PropWindowCreateX11WindowNumber = "x11.window";
	public const string PropWindowCreateXNumber = "x";
	public const string PropWindowCreateYNumber = "y";
	public const string PropWindowAndroidSurfacePointer = "SDL.window.android.surface";
	public const string PropWindowAndroidWindowPointer = "SDL.window.android.window";
	public const string PropWindowCocoaMetalViewTagNumber = "SDL.window.cocoa.metal_view_tag";
	public const string PropWindowCocoaWindowPointer = "SDL.window.cocoa.window";
	public const string PropWindowKMSDRMDeviceIndexNumber = "SDL.window.kmsdrm.dev_index";
	public const string PropWindowKMSDRMDrmFDNumber = "SDL.window.kmsdrm.drm_fd";
	public const string PropWindowKMSDRMGBMDevicePointer = "SDL.window.kmsdrm.gbm_dev";
	public const string PropWindowShapePointer = "SDL.window.shape";
	public const string PropWindowUIKitMetalViewTagNumber = "SDL.window.uikit.metal_view_tag";
	public const string PropWindowUIKitOpenGLFrameBufferNumber = "SDL.window.uikit.opengl.framebuffer";
	public const string PropWindowUIKitOpenGLRenderBufferNumber = "SDL.window.uikit.opengl.renderbuffer";
	public const string PropWindowUIKitOpenGLResolveFrameBufferNumber = "SDL.window.uikit.opengl.resolve_framebuffer";
	public const string PropWindowUIKitWindowPointer = "SDL.window.uikit.window";
	public const string PropWindowVivanteDisplayPointer = "SDL.window.vivante.display";
	public const string PropWindowVivanteSurfacePointer = "SDL.window.vivante.surface";
	public const string PropWindowVivanteWindowPointer = "SDL.window.vivante.window";
	public const string PropWindowWaylandDisplayPointer = "SDL.window.wayland.display";
	public const string PropWindowWaylandEGLWindowPointer = "SDL.window.wayland.egl_window";
	public const string PropWindowWaylandSurfacePointer = "SDL.window.wayland.surface";
	public const string PropWindowWaylandXDGPopupPointer = "SDL.window.wayland.xdg_popup";
	public const string PropWindowWaylandXDGPositionerPointer = "SDL.window.wayland.xdg_positioner";
	public const string PropWindowWaylandXDGSurfacePointer = "SDL.window.wayland.xdg_surface";
	public const string PropWindowWaylandXDGTopLevelExportHandleString = "SDL.window.wayland.xdg_toplevel_export_handle";
	public const string PropWindowWaylandXDGTopLevelPointer = "SDL.window.wayland.xdg_toplevel";
	public const string PropWindowWin32HDCPointer = "SDL.window.win32.hdc";
	public const string PropWindowWin32HwndPointer = "SDL.window.win32.hwnd";
	public const string PropWindowWin32InstancePointer = "SDL.window.win32.instance";
	public const string PropWindowWinRTWindowPointer = "SDL.window.winrt.window";
	public const string PropWindowX11DisplayPointer = "SDL.window.x11.display";
	public const string PropWindowX11ScreenNumber = "SDL.window.x11.screen";
	public const string PropWindowX11WindowNumber = "SDL.window.x11.window";
}