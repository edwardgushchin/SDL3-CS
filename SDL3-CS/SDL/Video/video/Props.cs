#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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
 */
#endregion

namespace SDL3;

public static partial class SDL
{
	public static partial class Props
	{
		/// <summary>
		/// <para>The pointer to the global <c>wl_display</c> object used by the Wayland video
		/// backend.</para>
		/// <para>Can be set before the video subsystem is initialized to import an external
		/// <c>wl_display</c> object from an application or toolkit for use in SDL, or read
		/// after initialization to export the <c>wl_display</c> used by the Wayland video
		/// backend. Setting this property after the video subsystem has been
		/// initialized has no effect, and reading it when the video subsystem is
		/// uninitialized will either return the user provided value, if one was set
		/// prior to initialization, or <c>null</c>. See docs/README-wayland.md for more
		/// information.</para>
		/// </summary>
		public const string GlobalVideoWaylandWLDisplayPointer = "SDL.video.wayland.wl_display";
		
		public const string DisplayHDREnabledBoolean = "SDL.display.HDR_enabled";
	    public const string DisplayKMSDRMPanelOrientationNumber = "SDL.display.KMSDRM.panel_orientation";
		public const string DisplayWaylandWLOutputPointer = "SDL.display.wayland.wl_output";
	    
	    public const string WindowCreateAlwaysOnTopBoolean = "SDL.window.create.always_on_top";
		public const string WindowCreateBorderlessBoolean = "SDL.window.create.borderless";
		public const string WindowCreateConstrainPopupBoolean = "SDL.window.create.constrain_popup";
		public const string WindowCreateFocusableBoolean = "SDL.window.create.focusable";
		public const string WindowCreateExternalGraphicsContextBoolean = "SDL.window.create.external_graphics_context";
		public const string WindowCreateFlagsNumber = "SDL.window.create.flags";
		public const string WindowCreateFullscreenBoolean = "SDL.window.create.fullscreen";
		public const string WindowCreateHeightNumber = "SDL.window.create.height";
		public const string WindowCreateHiddenBoolean = "SDL.window.create.hidden";
		public const string WindowCreateHighPixelDensityBoolean = "SDL.window.create.high_pixel_density";
		public const string WindowCreateMaximizedBoolean = "SDL.window.create.maximized";
		public const string WindowCreateMenuBoolean = "SDL.window.create.menu";
		public const string WindowCreateMetalBoolean = "SDL.window.create.metal";
		public const string WindowCreateMinimizedBoolean = "SDL.window.create.minimized";
		public const string WindowCreateModalBoolean = "SDL.window.create.modal";
		public const string WindowCreateMouseGrabbedBoolean = "SDL.window.create.mouse_grabbed";
		public const string WindowCreateOpenGLBoolean = "SDL.window.create.opengl";
		public const string WindowCreateParentPointer = "SDL.window.create.parent";
		public const string WindowCreateResizableBoolean = "SDL.window.create.resizable";
		public const string WindowCreateTitleString = "SDL.window.create.title";
		public const string WindowCreateTransparentBoolean = "SDL.window.create.transparent";
		public const string WindowCreateTooltipBoolean = "SDL.window.create.tooltip";
		public const string WindowCreateUtilityBoolean = "SDL.window.create.utility";
		public const string WindowCreateVulkanBoolean = "SDL.window.create.vulkan";
		public const string WindowCreateWidthNumber = "SDL.window.create.width";
		public const string WindowCreateXNumber = "SDL.window.create.x";
		public const string WindowCreateYNumber = "SDL.window.create.y";
		public const string WindowCreateCocoaWindowPointer = "SDL.window.create.cocoa.window";
		public const string WindowCreateCocoaViewPointer = "SDL.window.create.cocoa.view";
		public const string WindowCreateWaylandSurfaceRoleCustomBoolean = "SDL.window.create.wayland.surface_role_custom";
		public const string WindowCreateWaylandCreateEGLWindowBoolean = "SDL.window.create.wayland.create_egl_window";
		public const string WindowCreateWaylandWLSurfacePointer = "SDL.window.create.wayland.wl_surface";
		public const string WindowCreateWin32HWNDPointer = "SDL.window.create.win32.hwnd";
		public const string WindowCreateWin32PixelFormatHWNDPointer = "SDL.window.create.win32.pixel_format_hwnd";
		public const string WindowCreateX11WindowNumber = "SDL.window.create.x11.window";
		public const string WindowCreateEmscriptennCanvasIdString = "SDL.window.create.emscripten.canvas_id";
		public const string WindowCreateEmscriptenKeyboardElementString = "SDL.window.create.emscripten.keyboard_element";
		
		public const string WindowShapePointer = "SDL.window.shape";
		public const string WindowHDREnabledBoolean = "SDL.window.HDR_enabled";
		public const string WindowSDRWhiteLevelFloat = "SDL.window.SDR_white_level";
		public const string WindowHDRHeadroomFloat = "SDL.window.HDR_headroom";
		public const string WindowAndroidWindowPointer = "SDL.window.android.window";
		public const string WindowAndroidSurfacePointer = "SDL.window.android.surface";
		public const string WindowUIKitWindowPointer = "SDL.window.uikit.window";
		public const string WindowUIKitMetalViewTagNumber = "SDL.window.uikit.metal_view_tag";
		public const string WindowUIKitOpenglFramebufferNumber = "SDL.window.uikit.opengl.framebuffer";
		public const string WindowUIKitOpenglRenderbufferNumber = "SDL.window.uikit.opengl.renderbuffer";
		public const string WindowUIKitOpenglResolveFramebufferNumber = "SDL.window.uikit.opengl.resolve_framebuffer";
		public const string WindowKMSDRMDeviceIndexNumber = "SDL.window.kmsdrm.dev_index";
		public const string WindowKMSDRMDRMFDNumber = "SDL.window.kmsdrm.drm_fd";
		public const string WindowKMSDRMGBMDevicePointer = "SDL.window.kmsdrm.gbm_dev";
		public const string WindowCocoaWindowPointer = "SDL.window.cocoa.window";
		public const string WindowCocoaMetalViewTagNumber = "SDL.window.cocoa.metal_view_tag";
		public const string WindowOpenVROverlayIdNumber = "SDL.window.openvr.overlay_id";
		public const string WindowVivanteDisplayPointer = "SDL.window.vivante.display";
		public const string WindowVivanteWindowPointer = "SDL.window.vivante.window";
		public const string WindowVivanteSurfacePointer = "SDL.window.vivante.surface";
		public const string WindowWin32HWNDPointer = "SDL.window.win32.hwnd";
		public const string WindowWin32HDCPointer = "SDL.window.win32.hdc";
		public const string WindowWin32InstancePointer = "SDL.window.win32.instance";
		public const string WindowWaylandDisplayPointer = "SDL.window.wayland.display";
		public const string WindowWaylandSurfacePointer = "SDL.window.wayland.surface";
		public const string WindowWaylandViewportPointer = "SDL.window.wayland.viewport";
		public const string WindowWaylandEGLWindowPointer = "SDL.window.wayland.egl_window";
		public const string WindowWaylandXDGSurfacePointer = "SDL.window.wayland.xdg_surface";
		public const string WindowWaylandXDGToplevelPointer  = "SDL.window.wayland.xdg_toplevel";
		public const string WindowWaylandXDGToplevelExportHandleString = "SDL.window.wayland.xdg_toplevel_export_handle";
		public const string WindowWaylandXDGPopupPointer = "SDL.window.wayland.xdg_popup";
		public const string WindowWaylandXDGPositionerPointer = "SDL.window.wayland.xdg_positioner";
		public const string WindowX11DisplayPointer = "SDL.window.x11.display";
		public const string WindowX11ScreenNumber = "SDL.window.x11.screen";
		public const string WindowX11WindowNumber = "SDL.window.x11.window";
		public const string WindowEMScriptenCanvasIdString = "SDL.window.emscripten.canvas_id";
		public const string WindowEMScriptenFillDocumentBoolean = "SDL.window.emscripten.fill_document";
		public const string WindowEMScriptenKeyboardElementString = "SDL.window.emscripten.keyboard_element";
	}
}