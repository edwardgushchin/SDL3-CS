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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumVideoDrivers();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumVideoDrivers(void);</code>
    /// <summary>
    /// Get the number of video drivers compiled into SDL.
    /// </summary>
    /// <returns>a number >= 1 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetVideoDriver"/>
    public static int GetNumVideoDrivers() => SDL_GetNumVideoDrivers();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetVideoDriver(int index);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetVideoDriver(int index);</code>
    /// <summary>
    /// <para>Get the name of a built in video driver.</para>
    /// <para>The video drivers are presented in the order in which they are normally
    /// checked during initialization.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "cocoa",
    /// "x11" or "windows". These never have Unicode characters, and are not meant
    /// to be proper names.</para>
    /// <para>The returned string follows the <see cref="GetStringRule"/>.</para>
    /// </summary>
    /// <param name="index">the index of a video driver.</param>
    /// <returns>the name of the video driver with the given **index**.</returns>
    /// <seealso cref="GetNumVideoDrivers"/>
    public static string? GetVideoDriver(int index) => Marshal.PtrToStringUTF8(SDL_GetVideoDriver(index));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentVideoDriver();
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetCurrentVideoDriver(void);</code>
    /// <summary>
    /// <para>Get the name of the currently initialized video driver.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "cocoa",
    /// "x11" or "windows". These never have Unicode characters, and are not meant
    /// to be proper names.</para>
    /// <para>The returned string follows the <see cref="GetStringRule"/>.</para>
    /// </summary>
    /// <returns>the name of the current video driver or NULL if no driver has been
    /// initialized.</returns>
    /// <seealso cref="GetNumVideoDrivers"/>
    /// <seealso cref="GetVideoDriver"/>
    public static string? GetCurrentVideoDriver() => Marshal.PtrToStringUTF8(SDL_GetCurrentVideoDriver());
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme SDL_GetSystemTheme();
    /// <code>extern SDL_DECLSPEC SDL_SystemTheme SDLCALL SDL_GetSystemTheme(void);</code>
    /// <summary>
    /// Get the current system theme.
    /// </summary>
    /// <returns>the current system theme, light, dark, or unknown.</returns>
    public static SystemTheme GetSystemTheme() => SDL_GetSystemTheme();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDisplays(out int count);
    /// <code>extern SDL_DECLSPEC SDL_DisplayID *SDLCALL SDL_GetDisplays(int *count);</code>
    /// <summary>
    /// Get a list of currently connected displays.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of displays returned, may
    /// be NULL.</param>
    /// <returns>a 0 terminated array of display instance IDs which should be freed
    /// with <see cref="Free"/>, or NULL on error; call <see cref="GetError"/> for more
    /// details.</returns>
    public static uint[] GetDisplays(out int count)
    {
        var pArray = SDL_GetDisplays(out count);
        try
        {
            var displayArray = new int[count];
            Marshal.Copy(pArray, displayArray, 0, count);
            return Array.ConvertAll(displayArray, item => (uint)item);
        }
        finally
        {
            Marshal.FreeHGlobal(pArray);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPrimaryDisplay();
    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetPrimaryDisplay(void);</code>
    /// <summary>
    /// Return the primary display.
    /// </summary>
    /// <returns>the instance ID of the primary display on success or 0 on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetDisplays"/>
    public static uint GetPrimaryDisplay() => SDL_GetPrimaryDisplay();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayProperties(uint displayID);
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetDisplayProperties(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the properties associated with a display.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="PropDisplayHDREnabledBoolean"/>: true if the display has HDR
    /// headroom above the SDR white point. This is for informational and
    /// diagnostic purposes only, as not all platforms provide this information
    /// at the display level.</item>
    /// </list>
    /// <para>On KMS/DRM:</para>
    /// <list type="bullet">
    /// <item><see cref="PropDisplayKMSDRMPanelOrientationNumber"/>: the "panel orientation"
    /// property for the display in degrees of clockwise rotation. Note that this
    /// is provided only as a hint, and the application is responsible for any
    /// coordinate transformations needed to conform to the requested display
    /// orientation.</item>
    /// </list>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static uint GetDisplayProperties(uint displayID) => SDL_GetDisplayProperties(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDisplayName(uint displayID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetDisplayName(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the name of a display in UTF-8 encoding.</para>
    /// <para>The returned string follows the <see cref="GetStringRule"/>.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the name of a display or NULL on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <seealso cref="GetDisplays"/>
    public static string? GetDisplayName(uint displayID) => Marshal.PtrToStringUTF8(SDL_GetDisplayName(displayID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDisplayBounds(uint displayID, out Rect rect);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDisplayBounds(SDL_DisplayID displayID, SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Get the desktop area represented by a display.</para>
    /// <para>The primary display is always located at (0,0).</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <param name="rect">the <see cref="Rect"/> structure filled in with the display bounds.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError()"/> for more information.</returns>
    public static int GetDisplayBounds(uint displayID, out Rect rect) => SDL_GetDisplayBounds(displayID, out rect);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDisplayUsableBounds(uint displayID, out Rect rect);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDisplayUsableBounds(SDL_DisplayID displayID, SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Get the usable desktop area represented by a display, in screen
    /// coordinates.</para>
    /// <para>This is the same area as <see cref="GetDisplayBounds"/> reports, but with portions
    /// reserved by the system removed. For example, on Apple's macOS, this
    /// subtracts the area occupied by the menu bar and dock.</para>
    /// <para>Setting a window to be fullscreen generally bypasses these unusable areas,
    /// so these are good guidelines for the maximum space available to a
    /// non-fullscreen window.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <param name="rect">the <see cref="Rect"/> structure filled in with the display bounds.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static int GetDisplayUsableBounds(uint displayID, out Rect rect) => 
        SDL_GetDisplayUsableBounds(displayID, out rect);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayID);
    /// <code>extern SDL_DECLSPEC SDL_DisplayOrientation SDLCALL SDL_GetNaturalDisplayOrientation(SDL_DisplayID displayID);</code>
    /// <summary>
    /// Get the orientation of a display.
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the SDL_DisplayOrientation enum value of the display, or
    /// <see cref="DisplayOrientation.Unknown"/> if it isn't available.</returns>
    /// <seealso cref="GetDisplays"/>
    public static DisplayOrientation GetNaturalDisplayOrientation(uint displayID) => 
        SDL_GetNaturalDisplayOrientation(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayID);
    /// <code>extern SDL_DECLSPEC SDL_DisplayOrientation SDLCALL SDL_GetCurrentDisplayOrientation(SDL_DisplayID displayID);</code>
    /// <summary>
    /// Get the orientation of a display.
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the SDL_DisplayOrientation enum value of the display, or
    /// <see cref="DisplayOrientation.Unknown"/>`SDL_ORIENTATION_UNKNOWN` if it isn't available.</returns>
    /// <seealso cref="GetDisplays"/>
    public static DisplayOrientation GetCurrentDisplayOrientation(uint displayID) => 
        SDL_GetCurrentDisplayOrientation(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetDisplayContentScale(uint displayID);
    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetDisplayContentScale(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the content scale of a display.</para>
    /// <para>The content scale is the expected scale for content based on the DPI
    /// settings of the display. For example, a 4K display might have a 2.0 (200%)
    /// display scale, which means that the user expects UI elements to be twice as
    /// big on this display, to aid in readability.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the content scale of the display, or 0.0f on error; call
    /// <see cref="GetError"/> for more details.</returns>
    /// <seealso cref="GetDisplays"/>
    public static float GetDisplayContentScale(uint displayID) => SDL_GetDisplayContentScale(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetFullscreenDisplayModes(uint displayID, out int count);
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode **SDLCALL SDL_GetFullscreenDisplayModes(SDL_DisplayID displayID, int *count);</code>
    /// <summary>
    /// <para>Get a list of fullscreen display modes available on a display.</para>
    /// <para>The display modes are sorted in this priority:</para>
    /// <list type="bullet">
    /// <item>w -> largest to smallest</item>
    /// <item>h -> largest to smallest</item>
    /// <item>bits per pixel -> more colors to fewer colors</item>
    /// <item>packed pixel layout -> largest to smallest</item>
    /// <item>refresh rate -> highest to lowest</item>
    /// <item>pixel density -> lowest to highest</item>
    /// </list>
    /// </summary>
    /// <param name="displayID">displayID the instance ID of the display to query.</param>
    /// <param name="count">count a pointer filled in with the number of display modes returned.</param>
    /// <returns>a NULL terminated array of display mode pointers which should be
    /// freed with <see cref="Free"/>, or NULL on error; call <see cref="GetError"/> for
    /// more details.</returns>
    /// <seealso cref="GetDisplays"/>
    public static DisplayMode[] GetFullscreenDisplayModes(uint displayID, out int count)
    {
        var displayModesPtr = SDL_GetFullscreenDisplayModes(displayID, out count);

        if (displayModesPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return [];
        }

        try
        {
            var displayModes = new DisplayMode[count];
            for (var i = 0; i < count; i++)
            {
                var modePtr = Marshal.ReadIntPtr(displayModesPtr, i * IntPtr.Size);
                displayModes[i] = Marshal.PtrToStructure<DisplayMode>(modePtr);
            }

            return displayModes;
        }
        finally
        {
            Marshal.FreeHGlobal(displayModesPtr);
        }
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        [MarshalAs(SDLBool)] bool includeHighDensityModes);
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode *SDLCALL SDL_GetClosestFullscreenDisplayMode(SDL_DisplayID displayID, int w, int h, float refresh_rate, SDL_bool include_high_density_modes);</code>
    /// <summary>
    /// <para>Get the closest match to the requested display mode.</para>
    /// <para>The available display modes are scanned and `closest` is filled in with the
    /// closest mode matching the requested mode and returned. The mode format and
    /// refresh rate default to the desktop mode if they are set to 0. The modes
    /// are scanned with size being first priority, format being second priority,
    /// and finally checking the refresh rate. If all the available modes are too
    /// small, then NULL is returned.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <param name="w">the width in pixels of the desired display mode.</param>
    /// <param name="h">the height in pixels of the desired display mode.</param>
    /// <param name="refreshRate">the refresh rate of the desired display mode, or 0.0f
    /// for the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">boolean to include high density modes in
    /// the search.</param>
    /// <returns>a pointer to the closest display mode equal to or larger than the
    /// desired mode, or NULL on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <seealso cref="GetDisplays"/>
    /// <seealso cref="GetFullscreenDisplayModes"/>
    public static DisplayMode? GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        bool includeHighDensityModes) =>
        Marshal.PtrToStructure<DisplayMode>(
            SDL_GetClosestFullscreenDisplayMode(displayID, w, h, refreshRate, includeHighDensityModes));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDesktopDisplayMode(uint displayID);
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode *SDLCALL SDL_GetDesktopDisplayMode(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get information about the desktop's display mode.</para>
    /// <para>There's a difference between this function and <see cref="GetCurrentDisplayMode"/>
    /// when SDL runs fullscreen and has changed the resolution. In that case this
    /// function will return the previous native display mode, and not the current
    /// display mode.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>a pointer to the desktop display mode or NULL on error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetCurrentDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    public static DisplayMode? GetDesktopDisplayMode(uint displayID) =>
        Marshal.PtrToStructure<DisplayMode>(SDL_GetDesktopDisplayMode(displayID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentDisplayMode(uint displayID);
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode *SDLCALL SDL_GetCurrentDisplayMode(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get information about the current display mode.</para>
    /// <para>There's a difference between this function and <see cref="GetCurrentDisplayMode"/>
    /// when SDL runs fullscreen and has changed the resolution. In that case this
    /// function will return the current display mode, and not the previous native
    /// display mode.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>a pointer to the desktop display mode or NULL on error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetDesktopDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    public static DisplayMode? GetCurrentDisplayMode(uint displayID) =>
        Marshal.PtrToStructure<DisplayMode>(SDL_GetCurrentDisplayMode(displayID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForPoint(in Point point);
    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetDisplayForPoint(const SDL_Point *point);</code>
    /// <summary>
    /// Get the display containing a point.
    /// </summary>
    /// <param name="point">the point to query.</param>
    /// <returns>the instance ID of the display containing the point or 0 on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static uint GetDisplayForPoint(in Point point) => SDL_GetDisplayForPoint(point);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForRect(in Rect rect);
    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetDisplayForRect(const SDL_Rect *rect);</code>
    /// <summary>
    /// Get the display primarily containing a rect.
    /// </summary>
    /// <param name="rect">rect the rect to query.</param>
    /// <returns>the instance ID of the display entirely containing the rect or
    /// closest to the center of the rect on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static uint GetDisplayForRect(in Rect rect) => SDL_GetDisplayForRect(rect);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetDisplayForWindow(SDL_Window *window);</code>
    /// <summary>
    /// Get the display associated with a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the instance ID of the display containing the center of the window
    /// on success or 0 on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static uint GetDisplayForWindow(Window window) => SDL_GetDisplayForWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowPixelDensity(IntPtr window);
    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetWindowPixelDensity(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the pixel density of a window.</para>
    /// <para>This is a ratio of pixel size to window size. For example, if the window is
    /// 1920x1080 and it has a high density back buffer of 3840x2160 pixels, it
    /// would have a pixel density of 2.0.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the pixel density or 0.0f on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <seealso cref="GetWindowDisplayScale"/>
    public static float GetWindowPixelDensity(Window window) => SDL_GetWindowPixelDensity(window.Handle);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowDisplayScale(IntPtr window);
    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetWindowDisplayScale(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the content display scale relative to a window's pixel size.</para>
    /// <para>This is a combination of the window pixel density and the display content
    /// scale, and is the expected scale for displaying content in this window. For
    /// example, if a 3840x2160 window had a display scale of 2.0, the user expects
    /// the content to take twice as many pixels and be the same physical size as
    /// if it were being displayed in a 1920x1080 window with a display scale of
    /// 1.0.</para>
    /// <para>Conceptually this value corresponds to the scale display setting, and is
    /// updated when that setting is changed, or the window moves to a display with
    /// a different scale setting.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the display scale, or 0.0f on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    public static float GetWindowDisplayScale(Window window) => SDL_GetWindowDisplayScale(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowFullscreenMode(IntPtr window, in DisplayMode mode);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowFullscreenMode(SDL_Window *window, const SDL_DisplayMode *mode);</code>
    /// <summary>
    /// <para>Set the display mode to use when a window is visible and fullscreen.</para>
    /// <para>This only affects the display mode used when the window is fullscreen. To
    /// change the window size when the window is not fullscreen, use
    /// <see cref="SetWindowSize"/>.</para>
    /// <para>If the window is currently in the fullscreen state, this request is
    /// asynchronous on some windowing systems and the new mode dimensions may not
    /// be applied immediately upon the return of this function. If an immediate
    /// change is required, call <see cref="SyncWindow"/> to block until the changes have
    /// taken effect.</para>
    /// <para>When the new mode takes effect, an SDL_EVENT_WINDOW_RESIZED and/or an
    /// <see cref="EventType.WindowPixelSizeChanged"/> event will be emitted with the new
    /// mode dimensions.</para>
    /// </summary>
    /// <param name="window">the window to affect.</param>
    /// <param name="mode">a pointer to the display mode to use, which can be NULL for
    /// borderless fullscreen desktop mode, or one of the fullscreen
    /// modes returned by <see cref="GetFullscreenDisplayModes"/> to set an
    /// exclusive fullscreen mode.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <see cref="SyncWindow"/>
    public static int SetWindowFullscreenMode(Window window, DisplayMode mode) =>
        SDL_SetWindowFullscreenMode(window.Handle, in mode);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowFullscreenMode(IntPtr window);
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode *SDLCALL SDL_GetWindowFullscreenMode(SDL_Window *window);</code>
    /// <summary>
    /// Query the display mode to use when a window is visible at fullscreen.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a pointer to the exclusive fullscreen mode to use or NULL for
    /// borderless fullscreen desktop mode.</returns>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreen"/>
    public static DisplayMode? GetWindowFullscreenMode(Window window) =>
        Marshal.PtrToStructure<DisplayMode>(SDL_GetWindowFullscreenMode(window.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowICCProfile(IntPtr window, out nuint size);
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_GetWindowICCProfile(SDL_Window *window, size_t *size);</code>
    /// <summary>
    /// <para>Get the raw ICC profile data for the screen the window is currently on.</para>
    /// <para>Data returned should be freed with <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the raw ICC profile data on success or NULL on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static byte[]? GetWindowICCProfile(Window window)
    {
        var profilePtr = SDL_GetWindowICCProfile(window.Handle, out var size);
        
        if (profilePtr == IntPtr.Zero) return null;

        try
        {
            var profileData = new byte[(int)size];
            Marshal.Copy(profilePtr, profileData, 0, (int)size);
            return profileData;
        }
        finally
        {
            Free(profilePtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetWindowPixelFormat(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_PixelFormat SDLCALL SDL_GetWindowPixelFormat(SDL_Window *window);</code>
    /// <summary>
    /// Get the pixel format associated with the window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the pixel format of the window on success or
    /// <see cref="PixelFormat.Unknown"/> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    public static PixelFormat GetWindowPixelFormat(Window window) => SDL_GetWindowPixelFormat(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindows(out int count);
    /// <code>extern SDL_DECLSPEC SDL_Window **SDLCALL SDL_GetWindows(int *count);</code>
    /// <summary>
    /// Get a list of valid windows.
    /// </summary>
    /// <param name="count">count a pointer filled in with the number of windows returned, may
    /// be NULL.</param>
    /// <returns>a 0 terminated array of window pointers which should be freed with
    /// <see cref="Free"/>, or NULL on error; call <see cref="GetError"/> for more
    /// details.</returns>
    public static Window[]? GetWindows(out int count)
    {
        var windowsPtr = SDL_GetWindows(out count);
        if (windowsPtr == IntPtr.Zero) return null;

        try
        {
            var windowPtrs = new IntPtr[count];
            Marshal.Copy(windowsPtr, windowPtrs, 0, count);
            var windows = new Window[count];
            for (var i = 0; i < count; i++)
            {
                windows[i] = new Window(windowPtrs[i]);
            }
            return windows;
        }
        finally
        {
            Free(windowsPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateWindow([MarshalAs(UnmanagedType.LPUTF8Str)] string title, int w, int h, 
        WindowFlags flags);
    /// <code>extern SDL_DECLSPEC SDL_Window *SDLCALL SDL_CreateWindow(const char *title, int w, int h, SDL_WindowFlags flags);</code>
    /// <summary>
    /// <para>Create a window with the specified dimensions and flags.</para>
    /// <para><c>flags</c> may be any of the following OR'd together:</para>
    /// <list type="bullet">
    /// <item><see cref="WindowFlags.Fullscreen"/>: fullscreen window at desktop resolution</item>
    /// <item><see cref="WindowFlags.OpenGL"/>: window usable with an OpenGL context</item>
    /// <item><see cref="WindowFlags.Occluded"/>: window partially or completely obscured by another window</item>
    /// <item><see cref="WindowFlags.Hidden"/>: window is not visible</item>
    /// <item><see cref="WindowFlags.Borderless"/>: no window decoration</item>
    /// <item><see cref="WindowFlags.Resizable"/>: window can be resized</item>
    /// <item><see cref="WindowFlags.Minimized"/>: window is minimized</item>
    /// <item><see cref="WindowFlags.Maximized"/>: window is maximized</item>
    /// <item><see cref="WindowFlags.MouseGrabbed"/>: window has grabbed mouse focus</item>
    /// <item><see cref="WindowFlags.InputFocus"/>: window has input focus</item>
    /// <item><see cref="WindowFlags.MouseFocus"/>: window has mouse focus</item>
    /// <item><see cref="WindowFlags.External"/>: window not created by SDL</item>
    /// <item><see cref="WindowFlags.Modal"/>: window is modal</item>
    /// <item><see cref="WindowFlags.HighPixelDensity"/>: window uses high pixel density back buffer if possible</item>
    /// <item><see cref="WindowFlags.MouseCapture"/>: window has mouse captured (unrelated to <see cref="WindowFlags.MouseGrabbed"/>)</item>
    /// <item><see cref="WindowFlags.AlwaysOnTop"/>: window should always be above others</item>
    /// <item><see cref="WindowFlags.Utility"/>: window should be treated as a utility window, not showing in the task bar and window list</item>
    /// <item><see cref="WindowFlags.Tooltip"/>: window should be treated as a tooltip and does not get mouse or keyboard focus, requires a parent window</item>
    /// <item><see cref="WindowFlags.PopupMenu"/>: window should be treated as a popup menu, requires a parent window</item>
    /// <item><see cref="WindowFlags.KeyboardGrabbed"/>: window has grabbed keyboard input</item>
    /// <item><see cref="WindowFlags.Vulkan"/>: window usable with a Vulkan instance</item>
    /// <item><see cref="WindowFlags.Metal"/>`: window usable with a Metal instance</item>
    /// <item><see cref="WindowFlags.Transparent"/>: window with transparent buffer</item>
    /// <item><see cref="WindowFlags.NotFocusable"/>: window should not be focusable</item>
    /// </list>
    /// <para>The <see cref="Window"/> is implicitly shown if <see cref="WindowFlags.Hidden"/> is not set.</para>
    /// <para>On Apple's macOS, you **must** set the NSHighResolutionCapable Info.plist
    /// property to YES, otherwise you will not receive a High-DPI OpenGL canvas.</para>
    /// <para>The window pixel size may differ from its window coordinate size if the
    /// window is on a high pixel density display. Use <see cref="GetWindowSize"/> to query
    /// the client area's size in window coordinates, and
    /// <see cref="GetWindowSizeInPixels"/> or <see cref="GetRenderOutputSize"/> to query the
    /// drawable size in pixels. Note that the drawable size can vary after the
    /// window is created and should be queried again if you get an
    /// <see cref="EventType.WindowPixelSizeChanged"/> event.</para>
    /// <para>If the window is created with any of the <see cref="WindowFlags.OpenGL"/> or
    /// <see cref="WindowFlags.Vulkan"/> flags, then the corresponding LoadLibrary function
    /// (<see cref="GLLoadLibrary"/> or <see cref="VulkanLoadLibrary"/>) is called and the
    /// corresponding UnloadLibrary function is called by <see cref="DestroyWindow"/>.</para>
    /// <para>If <see cref="WindowFlags.Vulkan"/> is specified and there isn't a working Vulkan driver,
    /// <see cref="CreateWindow"/> will fail because <see cref="VulkanLoadLibrary"/>() will fail.</para>
    /// <para>If <see cref="WindowFlags.Metal"/> is specified on an OS that does not support Metal,
    /// <see cref="CreateWindow"/> will fail.</para>
    /// <para>If you intend to use this window with an SDL_Renderer, you should use
    /// <see cref="CreateWindowAndRenderer"/> instead of this function, to avoid window
    /// flicker.</para>
    /// <para>On non-Apple devices, SDL requires you to either not link to the Vulkan
    /// loader or link to a dynamic library version. This limitation may be removed
    /// in a future version of SDL.</para>
    /// </summary>
    /// <param name="title">the title of the window, in UTF-8 encoding.</param>
    /// <param name="w">the width of the window.</param>
    /// <param name="h">the height of the window.</param>
    /// <param name="flags">0, or one or more <see cref="WindowFlags"/> OR'd together.</param>
    /// <returns>the window that was created or NULL on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="CreatePopupWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// <seealso cref="DestroyWindow"/>
    public static Window CreateWindow(string title, int w, int h, WindowFlags flags) =>
        new(SDL_CreateWindow(title, w, h, flags));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreatePopupWindow(IntPtr parent, int offsetX, int offsetY, int w, int h, 
        WindowFlags flags);
    /// <code>extern SDL_DECLSPEC SDL_Window *SDLCALL SDL_CreatePopupWindow(SDL_Window *parent, int offset_x, int offset_y, int w, int h, SDL_WindowFlags flags);</code>
    /// <summary>
    /// <para>Create a child popup window of the specified parent window.</para>
    /// <para>'flags' **must** contain exactly one of the following: -
    /// <see cref="WindowFlags.Tooltip"/>: The popup window is a tooltip and will not pass any
    /// input events. - <see cref="WindowFlags.PopupMenu"/>: The popup window is a popup menu.
    /// The topmost popup menu will implicitly gain the keyboard focus.</para>
    /// <para>The following flags are not relevant to popup window creation and will be
    /// ignored:</para>
    /// <list type="bullet">
    /// <item><see cref="WindowFlags.Minimized"/></item>
    /// <item><see cref="WindowFlags.Maximized"/></item>
    /// <item><see cref="WindowFlags.Fullscreen"/></item>
    /// <item><see cref="WindowFlags.Borderless"/></item>
    /// </list>
    /// <para>The parent parameter **must** be non-null and a valid window. The parent of
    /// a popup window can be either a regular, toplevel window, or another popup
    /// window.</para>
    /// <para>Popup windows cannot be minimized, maximized, made fullscreen, raised,
    /// flash, be made a modal window, be the parent of a modal window, or grab the
    /// mouse and/or keyboard. Attempts to do so will fail.</para>
    /// <para>Popup windows implicitly do not have a border/decorations and do not appear
    /// on the taskbar/dock or in lists of windows such as alt-tab menus.</para>
    /// <para>If a parent window is hidden, any child popup windows will be recursively
    /// hidden as well. Child popup windows not explicitly hidden will be restored
    /// when the parent is shown.</para>
    /// <para>If the parent window is destroyed, any child popup windows will be
    /// recursively destroyed as well.</para>
    /// </summary>
    /// <param name="parent">the parent of the window, must not be NULL.</param>
    /// <param name="offsetX">the x position of the popup window relative to the origin
    /// of the parent.</param>
    /// <param name="offsetY">the y position of the popup window relative to the origin
    /// of the parent window.</param>
    /// <param name="w">the width of the window</param>
    /// <param name="h">h the height of the window</param>
    /// <param name="flags"><see cref="WindowFlags.Tooltip"/> or <see cref="WindowFlags.PopupMenu"/>, and zero or more
    /// additional <see cref="WindowFlags"/> OR'd together.</param>
    /// <returns>the window that was created or NULL on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// <seealso cref="DestroyWindow"/>
    /// <seealso cref="GetWindowParent"/>
    public static Window CreatePopupWindow(Window parent, int offsetX, int offsetY, int w, int h, WindowFlags flags) =>
        new(SDL_CreatePopupWindow(parent.Handle, offsetX, offsetY, w, h, flags));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateWindowWithProperties(uint props);
    /// <code>extern SDL_DECLSPEC SDL_Window *SDLCALL SDL_CreateWindowWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a window with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowCreateAlwaysOnTopBoolean"/>: true if the window should be always on top</item>
    /// <item><see cref="PropWindowCreateBorderlessBoolean"/>: true if the window has no window decoration</item>
    /// <item><see cref="PropWindowCreateExternalGraphicsContextBoolean"/>: true if the
    /// window will be used with an externally managed graphics context.</item>
    /// <item><see cref="PropWindowCreateFocusableBoolean"/>: true if the window should
    /// accept keyboard input (defaults true)</item>
    /// <item><see cref="PropWindowCreateFullscreenBoolean"/>: true if the window should
    /// start in fullscreen mode at desktop resolution</item>
    /// <item><see cref="PropWindowCreateHeightNumber"/>: the height of the window</item>
    /// <item><see cref="PropWindowCreateHiddenBoolean"/>: true if the window should start hidden</item>
    /// <item><see cref="PropWindowCreateHighPixelDensityBoolean"/>: true if the window
    /// uses a high pixel density buffer if possible</item>
    /// <item><see cref="PropWindowCreateMaximizedBoolean"/>: true if the window should
    /// start maximized</item>
    /// <item><see cref="PropWindowCreateMenuBoolean"/>: true if the window is a popup menu</item>
    /// <item><see cref="PropWindowCreateMetalBoolean"/>: true if the window will be used
    /// with Metal rendering</item>
    /// <item><see cref="PropWindowCreateMinimizedBoolean"/>: true if the window should
    /// start minimized</item>
    /// <item><see cref="PropWindowCreateModalBoolean"/>: true if the window is modal to
    /// its parent</item>
    /// <item><see cref="PropWindowCreateMouseGrabbedBoolean"/>: true if the window starts
    /// with grabbed mouse focus</item>
    /// <item><see cref="PropWindowCreateOpenGLBoolean"/>: true if the window will be used
    /// with OpenGL rendering</item>
    /// <item><see cref="PropWindowCreateParentPointer"/>: an SDL_Window that will be the
    /// parent of this window, required for windows with the "toolip", "menu",
    /// and "modal" properties</item>
    /// <item><see cref="PropWindowCreateResizableBoolean"/>: true if the window should be
    /// resizable</item>
    /// <item><see cref="PropWindowCreateTitleString"/>: the title of the window, in UTF-8
    /// encoding</item>
    /// <item><see cref="PropWindowCreateTransparentBoolean"/>: true if the window show
    /// transparent in the areas with alpha of 0</item>
    /// <item><see cref="PropWindowCreateTooltipBoolean"/>: true if the window is a tooltip</item>
    /// <item><see cref="PropWindowCreateUtilityBoolean"/>: true if the window is a utility
    /// window, not showing in the task bar and window list</item>
    /// <item><see cref="PropWindowCreateVulkanBoolean"/>: true if the window will be used
    /// with Vulkan rendering</item>
    /// <item><see cref="PropWindowCreateWidthNumber"/>: the width of the window</item>
    /// <item><see cref="PropWindowCreateXNumber"/>: the x position of the window, or
    /// <see cref="WindowPosCentered"/>, defaults to <see cref="WindowPosUndefined"/>. This is
    /// relative to the parent for windows with the "parent" property set.</item>
    /// <item><see cref="PropWindowCreateYNumber"/>: the y position of the window, or
    /// <see cref="WindowPosCentered"/>, defaults to <see cref="WindowPosUndefined"/>. This is
    /// relative to the parent for windows with the "parent" property set.</item>
    /// </list>
    /// <para>These are additional supported properties on macOS:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowCreateCocoaWindowPointer"/>: the
    /// `(__unsafe_unretained)` NSWindow associated with the window, if you want
    /// to wrap an existing window.</item>
    /// <item><see cref="PropWindowCreateCocoaViewPointer"/>: the `(__unsafe_unretained)`
    /// NSView associated with the window, defaults to `[window contentView]`</item>
    /// </list>
    /// <para>These are additional supported properties on Wayland:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowCreateWaylandSurfaceRoleCustomBoolean"/> - true if
    /// the application wants to use the Wayland surface for a custom role and
    /// does not want it attached to an XDG toplevel window. See
    /// [README/wayland](README/wayland) for more information on using custom
    /// surfaces.</item>
    /// <item><see cref="PropWindowCreateCreateEGLWindowBoolean"/> - true if the
    /// application wants an associated `wl_egl_window` object to be created,
    /// even if the window does not have the OpenGL property or flag set.</item>
    /// <item><see cref="PropWindowCreateWaylandWLSurfacePointer"/> - the wl_surface
    /// associated with the window, if you want to wrap an existing window. See
    /// [README/wayland](README/wayland) for more information.</item>
    /// </list>
    /// <para>These are additional supported properties on Windows:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowCreateWin32HWNDPointer"/>: the HWND associated with the
    /// window, if you want to wrap an existing window.</item>
    /// <item><see cref="PropWindowCreateWin32PixelFormatHWNDPointer"/>: optional,
    /// another window to share pixel format with, useful for OpenGL windows</item>
    /// </list>
    /// <para>These are additional supported properties with X11:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowCreateX11WindowNumber"/>: the X11 Window associated
    /// with the window, if you want to wrap an existing window.</item>
    /// </list>
    /// <para>The window is implicitly shown if the "hidden" property is not set.</para>
    /// <para>Windows with the "tooltip" and "menu" properties are popup windows and have
    /// the behaviors and guidelines outlined in <see cref="CreatePopupWindow"/>.</para>
    /// <para>If this window is being created to be used with an SDL_Renderer, you should
    /// not add a graphics API specific property
    /// (<see cref="PropWindowCreateOpenGLBoolean"/>, etc), as SDL will handle that
    /// internally when it chooses a renderer. However, SDL might need to recreate
    /// your window at that point, which may cause the window to appear briefly,
    /// and then flicker as it is recreated. The correct approach to this is to
    /// create the window with the <see cref="PropWindowCreateHiddenBoolean"/> property
    /// set to true, then create the renderer, then show the window wit
    /// <see cref="ShowWindow"/>.</para>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>the window that was created or NULL on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="CreateProperties"/>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="DestroyWindow"/>
    public static Window CreateWindowWithProperties(uint props) =>
        new Window(SDL_CreateWindowWithProperties(props));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowID(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_WindowID SDLCALL SDL_GetWindowID(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the numeric ID of a window.</para>
    /// <para>The numeric ID is what <see cref="WindowEvent"/> references, and is necessary to map
    /// these events to specific <see cref="Window"/> objects.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the ID of the window on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowFromID"/>
    public static uint GetWindowID(Window window) => SDL_GetWindowID(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowFromID(uint id);
    /// <code>extern SDL_DECLSPEC SDL_Window *SDLCALL SDL_GetWindowFromID(SDL_WindowID id);</code>
    /// <summary>
    /// <para>Get a window from a stored ID.</para>
    /// <para>The numeric ID is what <see cref="WindowEvent"/> references, and is necessary to map
    /// these events to specific <see cref="Window"/> objects.</para>
    /// </summary>
    /// <param name="id">the ID of the window.</param>
    /// <returns>he window associated with <c>id</c> or <c>NULL</c> if it doesn't exist; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowID"/>
    public static Window GetWindowFromID(uint id) => new(SDL_GetWindowFromID(id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowParent(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_Window *SDLCALL SDL_GetWindowParent(SDL_Window *window);</code>
    /// <summary>
    /// Get parent of a window.
    /// </summary>
    /// <param name="window">window the window to query.</param>
    /// <returns>the parent of the window on success or <c>NULL</c> if the window has no
    /// parent.</returns>
    /// <seealso cref="CreatePopupWindow"/>
    public static Window GetWindowParent(IntPtr window) => new(SDL_GetWindowParent(window));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowProperties(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetWindowProperties(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the properties associated with a window.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowShapePointer"/>: the surface associated with a shaped
    /// window</item>
    /// <item><see cref="PropDisplayHDREnabledBoolean"/>: true if the window has HDR
    /// headroom above the SDR white point. This property can change dynamically
    /// when <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// <item><see cref="PropWindowSDRWhiteLevelFloat"/>: the value of SDR white in the
    /// <see cref="Colorspace.SRGBLinear"/> colorspace. On Windows this corresponds to the
    /// SDR white level in scRGB colorspace, and on Apple platforms this is
    /// always 1.0 for EDR content. This property can change dynamically when
    /// <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// <item><see cref="PropWindowHDRHeadroomFloat"/>: the additional high dynamic range
    /// that can be displayed, in terms of the SDR white point. When HDR is not
    /// enabled, this will be 1.0. This property can change dynamically when
    /// <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// </list>
    /// <para>On Android:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowAndroidWindowPointer"/>: the ANativeWindow associated
    /// with the window</item>
    /// <item><see cref="PropWindowAndroidSurfacePointer"/>: the EGLSurface associated with
    /// the window</item>
    /// </list>
    /// <para>On iOS:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowUIKitWindowPointer"/>: the `(__unsafe_unretained)`
    /// UIWindow associated with the window</item>
    /// <item><see cref="PropWindowUIKitMetalViewTagNumber"/>: the NSInteger tag
    /// assocated with metal views on the window</item>
    /// <item><see cref="PropWindowUIKitOpenGLFrameBufferNumber"/>: the OpenGL view's
    /// framebuffer object. It must be bound when rendering to the screen using
    /// OpenGL.</item>
    /// <item><see cref="PropWindowUIKitOpenGLRenderBufferNumber"/>: the OpenGL view's
    /// renderbuffer object. It must be bound when SDL_GL_SwapWindow is called.</item>
    /// <item><see cref="PropWindowUIKitOpenGLResolveFrameBufferNumber"/>: the OpenGL
    /// view's resolve framebuffer, when MSAA is used.</item>
    /// </list>
    /// <para>On KMS/DRM:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowKMSDRMDeviceIndexNumber"/>: the device index associated
    /// with the window (e.g. the X in /dev/dri/cardX)</item>
    /// <item><see cref="PropWindowKMSDRMDRMFDNumber"/>: the DRM FD associated with the
    /// window</item>
    /// <item><see cref="PropWindowKMSDRMGBMDevicePointer"/>: the GBM device associated
    /// with the window</item>
    /// </list>
    /// <para>On macOS:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowCocoaWindowPointer"/>: the `(__unsafe_unretained)`
    /// NSWindow associated with the window</item>
    /// <item><see cref="PropWindowCocoaMetalViewTagNumber"/>: the NSInteger tag
    /// assocated with metal views on the window</item>
    /// </list>
    /// <para>On Vivante:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowVivanteDisplayPointer"/>: the EGLNativeDisplayType
    /// associated with the window</item>
    /// <item><see cref="PropWindowVivanteWindowPointer"/>: the EGLNativeWindowType
    /// associated with the window</item>
    /// <item><see cref="PropWindowVivanteSurfacePointer"/>: the EGLSurface associated with
    /// the window</item>
    /// </list>
    /// <para>On UWP:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowWinRTWindowPointer"/>: the IInspectable CoreWindow
    /// associated with the window</item>
    /// </list>
    /// <para>On Windows:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowWin32HWNDPointer"/>: the HWND associated with the window</item>
    /// <item><see cref="PropWindowWin32HDCPointer"/>: the HDC associated with the window</item>
    /// <item><see cref="PropWindowWin32InstancePointer"/>: the HINSTANCE associated with
    /// the window</item>
    /// </list>
    /// <para>On Wayland:</para>
    /// <para>Note: The `xdg_*` window objects do not internally persist across window
    /// show/hide calls. They will be null if the window is hidden and must be
    /// queried each time it is shown.</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowWaylandDisplayPointer"/>: the wl_display associated with
    /// the window</item>
    /// <item><see cref="PropWindowWaylandSurfacePointer"/>: the wl_surface associated with
    /// the window</item>
    /// <item><see cref="PropWindowWaylandEGLWindowPointer"/>: the wl_egl_window
    /// associated with the window</item>
    /// <item><see cref="PropWindowWaylandXDGSurfacePointer"/>: the xdg_surface associated
    /// with the window</item>
    /// <item><see cref="PropWindowWaylandXDGTopLevelPointer"/>: the xdg_toplevel role
    /// associated with the window</item>
    /// <item><see cref="PropWindowWaylandXDGTopLevelExportHandleString"/>: the export
    /// handle associated with the window</item>
    /// <item><see cref="PropWindowWaylandXDGPopupPointer"/>: the xdg_popup role
    /// associated with the window</item>
    /// <item><see cref="PropWindowWaylandXDGPositionerPointer"/>: the xdg_positioner
    /// associated with the window, in popup mode</item>
    /// </list>
    /// <para>On X11:</para>
    /// <list type="bullet">
    /// <item><see cref="PropWindowX11DisplayPointer"/>: the X11 Display associated with
    /// the window</item>
    /// <item><see cref="PropWindowX11ScreenNumber"/>: the screen number associated with
    /// the window</item>
    /// <item><see cref="PropWindowX11WindowNumber"/>: the X11 Window associated with the
    /// window</item>
    /// </list>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a valid property ID on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static uint GetWindowProperties(Window window) => SDL_GetWindowProperties(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(IntPtr window);
    public static void DestroyWindow(Window window) => SDL_DestroyWindow(window.Handle);
}