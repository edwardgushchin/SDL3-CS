#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
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
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
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
    public static uint[]? GetDisplays(out int count)
    {
        var pArray = SDL_GetDisplays(out count);

        if (pArray == IntPtr.Zero) return null;
        
        if (count == 0) return [];
        
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
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
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
    public static DisplayMode[]? GetFullscreenDisplayModes(uint displayID, out int count)
    {
        var displayModesPtr = SDL_GetFullscreenDisplayModes(displayID, out count);
        
        if (displayModesPtr == IntPtr.Zero) return null;
        
        if (count == 0) return [];

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
            Free(displayModesPtr);
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
        bool includeHighDensityModes)
    {
        var rectPtr = SDL_GetClosestFullscreenDisplayMode(displayID, w, h, refreshRate, includeHighDensityModes);
        return rectPtr == IntPtr.Zero ? null : Marshal.PtrToStructure<DisplayMode>(rectPtr);
    }
    
    
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
    public static DisplayMode? GetDesktopDisplayMode(uint displayID)
    {
        var rectPtr = SDL_GetDesktopDisplayMode(displayID);
        return rectPtr == IntPtr.Zero ? null : Marshal.PtrToStructure<DisplayMode>(rectPtr);
    }


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
    public static DisplayMode? GetCurrentDisplayMode(uint displayID)
    {
        var rectPtr = SDL_GetCurrentDisplayMode(displayID);
        return rectPtr == IntPtr.Zero ? null : Marshal.PtrToStructure<DisplayMode>(rectPtr);
    }


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
    private static partial int SDL_SetWindowFullscreenMode(IntPtr window, IntPtr mode);
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
    public static int SetWindowFullscreenMode(Window window, DisplayMode? mode)
    {
        var modePtr = IntPtr.Zero;

        try
        {
            if (mode.HasValue)
            {
                modePtr = Marshal.AllocHGlobal(Marshal.SizeOf<DisplayMode>());
                Marshal.StructureToPtr(mode.Value, modePtr, false);
            }
            
            return SDL_SetWindowFullscreenMode(window.Handle, modePtr);
        }
        finally
        {
            if (modePtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(modePtr);
            }
        }
    }
    
    
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
    public static DisplayMode? GetWindowFullscreenMode(Window window)
    {
        var rectPtr = SDL_GetWindowFullscreenMode(window.Handle);
        return rectPtr == IntPtr.Zero ? null : Marshal.PtrToStructure<DisplayMode>(rectPtr);
    }
    
    
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
        
        if (count == 0) return [];

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
    public static Window? CreateWindow(string title, int w, int h, WindowFlags flags)
    {
        var windowHandle = SDL_CreateWindow(title, w, h, flags);
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
        
    
    
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
    public static Window? CreatePopupWindow(Window? parent, int offsetX, int offsetY, int w, int h, WindowFlags flags)
    {
        var windowHandle = SDL_CreatePopupWindow(parent?.Handle ?? IntPtr.Zero, offsetX, offsetY, w, h, flags);
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
        
    
    
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
    public static Window? CreateWindowWithProperties(uint props)
    {
        var windowHandle = SDL_CreateWindowWithProperties(props);
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
        
    
    
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
    public static Window? GetWindowFromID(uint id)
    {
        var windowHandle = SDL_GetWindowFromID(id);
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
    
    
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
    public static Window? GetWindowParent(IntPtr window)
    {
        var windowHandle = SDL_GetWindowParent(window);
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
    
    
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
    private static partial WindowFlags SDL_GetWindowFlags(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_WindowFlags SDLCALL SDL_GetWindowFlags(SDL_Window *window);</code>
    /// <summary>
    /// Get the window flags.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a mask of the <see cref="WindowFlags"/> associated with <c>window</c>.</returns>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="HideWindow"/>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="ShowWindow"/>
    public static WindowFlags GetWindowFlags(IntPtr window) => SDL_GetWindowFlags(window);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowTitle(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string title);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowTitle(SDL_Window *window, const char *title);</code>
    /// <summary>
    /// <para>Set the title of a window.</para>
    /// <para>This string is expected to be in UTF-8 encoding.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="title">the desired window title in UTF-8 format.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowTitle"/>
    public static int SetWindowTitle(Window window, string title) => SDL_SetWindowTitle(window.Handle, title);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowTitle(IntPtr window);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetWindowTitle(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the title of a window.</para>
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="window">window the window to query.</param>
    /// <returns>the title of the window in UTF-8 format or <see cref="string.Empty"/> if there is notitle.</returns>
    public static string GetWindowTitle(Window window) => 
        Marshal.PtrToStringUTF8(SDL_GetWindowTitle(window.Handle))!;
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowIcon(IntPtr window, Surface icon);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowIcon(SDL_Window *window, SDL_Surface *icon);</code>
    /// <summary>
    /// Set the icon for a window.
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="icon">an <see cref="Surface"/> structure containing the icon for the window.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int SetWindowIcon(Window window, Surface icon) => SDL_SetWindowIcon(window.Handle, icon);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowPosition(IntPtr window, int x, int y);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowPosition(SDL_Window *window, int x, int y);</code>
    /// <summary>
    /// <para>Request that the window's position be set.</para>
    /// <para>If, at the time of this request, the window is in a fixed-size state such
    /// as maximized, this request may be deferred until the window returns to a
    /// resizable state.</para>
    /// <para>This can be used to reposition fullscreen-desktop windows onto a different
    /// display, however, exclusive fullscreen windows are locked to a specific
    /// display and can only be repositioned programmatically via
    /// <see cref="SetWindowFullscreenMode"/>.</para>
    /// <para>On some windowing systems this request is asynchronous and the new
    /// coordinates may not have have been applied immediately upon the return of
    /// this function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window position changes, an <see cref="EventType.WindowMoved"/> event will be
    /// emitted with the window's new coordinates. Note that the new coordinates
    /// may not match the exact coordinates requested, as some windowing systems
    /// can restrict the position of the window in certain scenarios (e.g.
    /// constraining the position so the window is always within desktop bounds).
    /// Additionally, as this is just a request, it can be denied by the windowing
    /// system.</para>
    /// </summary>
    /// <param name="window">the window to reposition.</param>
    /// <param name="x">the x coordinate of the window, or <see cref="WindowPosCentered"/> or
    /// <see cref="WindowPosUndefined"/>.</param>
    /// <param name="y">the y coordinate of the window, or <see cref="WindowPosCentered"/> or
    /// <see cref="WindowPosUndefined"/>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowPosition"/>
    /// <seealso cref="SyncWindow"/>
    public static int SetWindowPosition(Window window, int x, int y) => SDL_SetWindowPosition(window.Handle, x, y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowPosition(IntPtr window, out int x, out int y);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowPosition(SDL_Window *window, int *x, int *y);</code>
    /// <summary>
    /// <para>Get the position of a window.</para>
    /// <para>This is the current position of the window as last reported by the
    /// windowing system.</para>
    /// <para>If you do not need the value for one of the positions a <c>NULL</c> may be passed
    /// in the <c>x</c> or <c>y</c> parameter.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="x">a pointer filled in with the x position of the window, may be
    /// <c>NULL</c>.</param>
    /// <param name="y">a pointer filled in with the y position of the window, may be
    /// <c>NULL</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="SetWindowPosition"/>
    public static int GetWindowPosition(Window window, out int x, out int y) => 
        SDL_GetWindowPosition(window.Handle, out x, out y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowSize(IntPtr window, int w, int h);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowSize(SDL_Window *window, int w, int h);</code>
    /// <summary>
    /// <para>Request that the size of a window's client area be set.</para>
    /// <para>If, at the time of this request, the window in a fixed-size state, such as
    /// maximized or fullscreen, the request will be deferred until the window
    /// exits this state and becomes resizable again.</para>
    /// <para>To change the fullscreen mode of a window, use
    /// <see cref="SetWindowFullscreenMode"/></para>
    /// <para>On some windowing systems, this request is asynchronous and the new window
    /// size may not have have been applied immediately upon the return of this
    /// function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window size changes, an <see cref="EventType.WindowResized"/> event will be
    /// emitted with the new window dimensions. Note that the new dimensions may
    /// not match the exact size requested, as some windowing systems can restrict
    /// the window size in certain scenarios (e.g. constraining the size of the
    /// content area to remain within the usable desktop bounds). Additionally, as
    /// this is just a request, it can be denied by the windowing system.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="width">the width of the window, must be > 0.</param>
    /// <param name="height">the height of the window, must be > 0.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowSize"/>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SyncWindow"/>
    public static int SetWindowSize(Window window, int width, int height) => 
        SDL_SetWindowSize(window.Handle, width, height);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowSize(IntPtr window, out int w, out int h);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowSize(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// <para>Get the size of a window's client area.</para>
    /// <para>The window pixel size may differ from its window coordinate size if the
    /// window is on a high pixel density display. Use <see cref="GetWindowSizeInPixels"/>
    /// or <see cref="GetRenderOutputSize"/> to get the real client area size in pixels.</para>
    /// </summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <param name="width">a pointer filled in with the width of the window, may be NULL.</param>
    /// <param name="height">a pointer filled in with the height of the window, may be NULL.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetRenderOutputSize"/>
    /// <seealso cref="GetWindowSizeInPixels"/>
    /// <seealso cref="SetWindowSize"/>
    public static int GetWindowSize(Window window, out int width, out int height) =>
        SDL_GetWindowSize(window.Handle, out width, out height);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowAspectRatio(IntPtr window, float minAspect, float maxAspect);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowAspectRatio(SDL_Window *window, float min_aspect, float max_aspect);</code>
    /// <summary>
    /// <para>Request that the aspect ratio of a window's client area be set.</para>
    /// <para>The aspect ratio is the ratio of width divided by height, e.g. 2560x1600
    /// would be 1.6. Larger aspect ratios are wider and smaller aspect ratios are
    /// narrower.</para>
    /// <para>If, at the time of this request, the window in a fixed-size state, such as
    /// maximized or fullscreen, the request will be deferred until the window
    /// exits this state and becomes resizable again.</para>
    /// <para>On some windowing systems, this request is asynchronous and the new window
    /// aspect ratio may not have have been applied immediately upon the return of
    /// this function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window size changes, an <see cref="EventType.WindowResized"/> event will be
    /// emitted with the new window dimensions. Note that the new dimensions may
    /// not match the exact aspect ratio requested, as some windowing systems can
    /// restrict the window size in certain scenarios (e.g. constraining the size
    /// of the content area to remain within the usable desktop bounds).
    /// Additionally, as this is just a request, it can be denied by the windowing
    /// system.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="minAspect">the minimum aspect ratio of the window, or 0.0f for no
    /// limit.</param>
    /// <param name="maxAspect">the maximum aspect ratio of the window, or 0.0f for no
    /// limit.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowAspectRatio"/>
    /// <seealso cref="SyncWindow"/>
    public static int SetWindowAspectRatio(Window window, float minAspect, float maxAspect) => 
        SDL_SetWindowAspectRatio(window.Handle, minAspect, maxAspect);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowAspectRatio(IntPtr window, out float minAspect, out float maxAspect);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowAspectRatio(SDL_Window *window, float *min_aspect, float *max_aspect);</code>
    /// <summary>
    /// Get the size of a window's client area.
    /// </summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <param name="minAspect">a pointer filled in with the minimum aspect ratio of the
    /// window, may be NULL.</param>
    /// <param name="maxAspect">a pointer filled in with the maximum aspect ratio of the
    /// window, may be NULL.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int GetWindowAspectRatio(Window window, out float minAspect, out float maxAspect) =>
        SDL_GetWindowAspectRatio(window.Handle, out minAspect, out maxAspect);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, 
        out int right);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowBordersSize(SDL_Window *window, int *top, int *left, int *bottom, int *right);</code>
    /// <summary>
    /// <para>Get the size of a window's borders (decorations) around the client area.</para>
    /// <para>Note: If this function fails (returns -1), the size values will be
    /// initialized to 0, 0, 0, 0 (if a non-NULL pointer is provided), as if the
    /// window in question was borderless.</para>
    /// <para>Note: This function may fail on systems where the window has not yet been
    /// decorated by the display server (for example, immediately after calling
    /// <see cref="CreateWindow"/>). It is recommended that you wait at least until the
    /// window has been presented and composited, so that the window system has a
    /// chance to decorate the window and provide the border dimensions to SDL.</para>
    /// </summary>
    /// <param name="window">the window to query the size values of the border
    /// (decorations) from.</param>
    /// <param name="top">pointer to variable for storing the size of the top border; NULL
    /// is permitted.</param>
    /// <param name="left">pointer to variable for storing the size of the left border;
    /// NULL is permitted.</param>
    /// <param name="bottom">pointer to variable for storing the size of the bottom
    /// border; NULL is permitted.</param>
    /// <param name="right">pointer to variable for storing the size of the right border;
    /// NULL is permitted.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowSize"/>
    public static int GetWindowBordersSize(Window window, out int top, out int left, out int bottom, out int right) => 
        SDL_GetWindowBordersSize(window.Handle, out top, out left, out bottom, out right);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowSizeInPixels(IntPtr window, out int w, out int h);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowSizeInPixels(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// Get the size of a window's client area, in pixels.
    /// </summary>
    /// <param name="window">the window from which the drawable size should be queried.</param>
    /// <param name="width">a pointer to variable for storing the width in pixels, may be
    /// NULL.</param>
    /// <param name="height">a pointer to variable for storing the height in pixels, may be
    /// NULL.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="GetWindowSize"/>
    public static int GetWindowSizeInPixels(Window window, out int width, out int height) =>
        SDL_GetWindowSizeInPixels(window.Handle, out width, out height);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowMinimumSize(IntPtr window, int minW, int minH);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowMinimumSize(SDL_Window *window, int min_w, int min_h);</code>
    /// <summary>
    /// Set the minimum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="minWidth">the minimum width of the window, or 0 for no limit.</param>
    /// <param name="minHeight">the minimum height of the window, or 0 for no limit.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    public static int SetWindowMinimumSize(Window window, int minWidth, int minHeight) =>
        SDL_SetWindowMinimumSize(window.Handle, minWidth, minHeight);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowMinimumSize(IntPtr window, out int w, out int h);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowMinimumSize(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// Get the minimum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="minWidth">a pointer filled in with the minimum width of the window, may be
    /// NULL.</param>
    /// <param name="minHeight">a pointer filled in with the minimum height of the window, may be
    /// NULL.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowMaximumSize"/>
    /// <seealso cref="SetWindowMinimumSize"/>
    public static int GetWindowMinimumSize(Window window, out int minWidth, out int minHeight) => 
        SDL_GetWindowMinimumSize(window.Handle, out minWidth, out minHeight);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowMaximumSize(IntPtr window, int maxWidth, int maxHeight);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowMaximumSize(SDL_Window *window, int max_w, int max_h);</code>
    /// <summary>
    /// Set the maximum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="maxWidth">the maximum width of the window, or 0 for no limit.</param>
    /// <param name="maxHeight">the maximum height of the window, or 0 for no limit.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetWindowMaximumSize"/>
    /// <seealso cref="SetWindowMinimumSize"/>
    public static int SetWindowMaximumSize(Window window, int maxWidth, int maxHeight) =>
        SDL_SetWindowMaximumSize(window.Handle, maxWidth, maxHeight);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowMaximumSize(IntPtr window, out int w, out int h);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowMaximumSize(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// Get the maximum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="width">a pointer filled in with the maximum width of the window, may be
    /// NULL.</param>
    /// <param name="height">a pointer filled in with the maximum height of the window, may be
    /// NULL.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    public static int GetWindowMaximumSize(Window window, out int width, out int height) =>
        SDL_GetWindowMaximumSize(window.Handle, out width, out height);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowBordered(IntPtr window, [MarshalAs(SDLBool)]bool bordered);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowBordered(SDL_Window *window, SDL_bool bordered);</code>
    /// <summary>
    /// <para>Set the border state of a window.</para>
    /// <para>This will add or remove the window's <seealso cref="WindowFlags.Borderless"/> flag and add
    /// or remove the border from the actual window. This is a no-op if the
    /// window's border already matches the requested state.</para>
    /// <para>You can't change the border state of a fullscreen window.</para>
    /// </summary>
    /// <param name="window">the window of which to change the border state.</param>
    /// <param name="bordered">False to remove border, True to add border.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowFlags"/>
    public static int SetWindowBordered(Window window, bool bordered) =>
        SDL_SetWindowBordered(window.Handle, bordered);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowResizable(IntPtr window, [MarshalAs(SDLBool)] bool resizable);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowResizable(SDL_Window *window, SDL_bool resizable);</code>
    /// <summary>
    /// <para>Set the user-resizable state of a window.</para>
    /// <para>This will add or remove the window's <see cref="WindowFlags.Resizable"/> flag and
    /// allow/disallow user resizing of the window. This is a no-op if the window's
    /// resizable state already matches the requested state.</para>
    /// </summary>
    /// <param name="window">the window of which to change the resizable state.</param>
    /// <param name="resizable">True to allow resizing, False to disallow.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowFlags"/>
    public static int SetWindowResizable(Window window, bool resizable) =>
        SDL_SetWindowResizable(window.Handle, resizable);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowAlwaysOnTop(IntPtr window, [MarshalAs(SDLBool)] bool onTop);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowAlwaysOnTop(SDL_Window *window, SDL_bool on_top);</code>
    /// <summary>
    /// <para>Set the window to always be above the others.</para>
    /// <para>This will add or remove the window's <see cref="WindowFlags.AlwaysOnTop"/> flag. This
    /// will bring the window to the front and keep the window above the rest.</para>
    /// </summary>
    /// <param name="window">the window of which to change the always on top state.</param>
    /// <param name="onTop">True to set the window always on top, False to
    /// disable.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowFlags"/>
    public static int SetWindowAlwaysOnTop(Window window, bool onTop) =>
        SDL_SetWindowAlwaysOnTop(window.Handle, onTop);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ShowWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ShowWindow(SDL_Window *window);</code>
    /// <summary>
    /// Show a window.
    /// </summary>
    /// <param name="window">The window to show.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="HideWindow"/>
    /// <seealso cref="RaiseWindow"/>
    public static int ShowWindow(Window window) => SDL_ShowWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_HideWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_HideWindow(SDL_Window *window);</code>
    /// <summary>
    /// Hide a window.
    /// </summary>
    /// <param name="window">The window to hide.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="ShowWindow"/>
    public static int HideWindow(Window window) => SDL_HideWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RaiseWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RaiseWindow(SDL_Window *window);</code>
    /// <summary>
    /// Request that a window be raised above other windows and gain the input
    /// focus.
    /// </summary>
    /// <param name="window">The window to raise.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int RaiseWindow(Window window) => SDL_RaiseWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_MaximizeWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_MaximizeWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para> * Request that the window be made as large as possible.</para>
    /// <para>Non-resizable windows can't be maximized. The window must have the
    /// <see cref="WindowFlags.Resizable"/> flag set, or this will have no effect.</para>
    /// <para>On some windowing systems this request is asynchronous and the new window
    /// state may not have have been applied immediately upon the return of this
    /// function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowMaximized"/> event will be
    /// emitted. Note that, as this is just a request, the windowing system can
    /// deny the state change.</para>
    /// <para>When maximizing a window, whether the constraints set via
    /// <see cref="SetWindowMaximumSize"/> are honored depends on the policy of the window
    /// manager. Win32 and macOS enforce the constraints when maximizing, while X11
    /// and Wayland window managers may vary.</para>
    /// </summary>
    /// <param name="window">The window to maximize.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="SyncWindow"/>
    public static int MaximizeWindow(Window window) => SDL_MaximizeWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_MinimizeWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_MinimizeWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Request that the window be minimized to an iconic representation.</para>
    /// <para>On some windowing systems this request is asynchronous and the new window
    /// state may not have have been applied immediately upon the return of this
    /// function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowMinimized"/> event will be
    /// emitted. Note that, as this is just a request, the windowing system can
    /// deny the state change.</para>
    /// </summary>
    /// <param name="window">The window to minimize.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="SyncWindow"/>
    public static int MinimizeWindow(Window window) => SDL_MinimizeWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RestoreWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RestoreWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Request that the size and position of a minimized or maximized window be
    /// restored.</para>
    /// <para>On some windowing systems this request is asynchronous and the new window
    /// state may not have have been applied immediately upon the return of this
    /// function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowRestored"/> event will be
    /// emitted. Note that, as this is just a request, the windowing system can
    /// deny the state change.</para>
    /// </summary>
    /// <param name="window">The window to restore.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="SyncWindow"/>
    public static int RestoreWindow(Window window) => SDL_RestoreWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowFullscreen(IntPtr window, [MarshalAs(SDLBool)] bool fullscreen);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowFullscreen(SDL_Window *window, SDL_bool fullscreen);</code>
    /// <summary>
    /// <para>Request that the window's fullscreen state be changed.</para>
    /// <para>By default a window in fullscreen state uses borderless fullscreen desktop
    /// mode, but a specific exclusive display mode can be set using
    /// <see cref="SetWindowFullscreenMode"/>.</para>
    /// <para>On some windowing systems this request is asynchronous and the new
    /// fullscreen state may not have have been applied immediately upon the return
    /// of this function. If an immediate change is required, call <see cref="SyncWindow"/>
    /// to block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowEnterFullscreen"/> or
    /// <see cref="EventType.WindowLeaveFullscreen"/> event will be emitted. Note that, as this
    /// is just a request, it can be denied by the windowing system.</para>
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="fullscreen">True for fullscreen mode, false for windowed mode.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreenMode"/>
    /// <seealso cref="SyncWindow"/>
    public static int SetWindowFullscreen(Window window, bool fullscreen) => 
        SDL_SetWindowFullscreen(window.Handle, fullscreen);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SyncWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SyncWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Block until any pending window state is finalized.</para>
    /// <para>On asynchronous windowing systems, this acts as a synchronization barrier
    /// for pending window state. It will attempt to wait until any pending window
    /// state has been applied and is guaranteed to return within finite time. Note
    /// that for how long it can potentially block depends on the underlying window
    /// system, as window state changes may involve somewhat lengthy animations
    /// that must complete before the window is in its final requested state.</para>
    /// <para>On windowing systems where changes are immediate, this does nothing.</para>
    /// </summary>
    /// <param name="window">the window for which to wait for the pending state to be
    /// applied.</param>
    /// <returns>0 on success, a positive value if the operation timed out before
    /// the window was in the requested state, or a negative error code on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="SetWindowSize"/>
    /// <seealso cref="SetWindowPosition"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="Hints.VideoSyncWindowOperations"/>
    public static int SyncWindow(Window window) => SDL_SyncWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_WindowHasSurface(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WindowHasSurface(SDL_Window *window);</code>
    /// <summary>
    /// Return whether the window has a surface associated with it.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>True if there is a surface associated with the window, otherwise false.</returns>
    /// <seealso cref="GetWindowSurface"/>
    public static bool WindowHasSurface(Window window) => SDL_WindowHasSurface(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowSurface(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_GetWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the SDL surface associated with the window.</para>
    /// <para>A new surface will be created with the optimal format for the window, if
    /// necessary. This surface will be freed when the window is destroyed. Do not
    /// free this surface.</para>
    /// <para>This surface will be invalidated if the window is resized. After resizing a
    /// window this function must be called again to return a valid surface.</para>
    /// <para>You may not combine this with 3D or the rendering API on this window.</para>
    /// <para>This function is affected by <see cref="Hints.FrameBufferAcceleration"/>.</para>
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The surface associated with the window, or null on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="DestroyWindowSurface"/>
    /// <seealso cref="WindowHasSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    /// <seealso cref="UpdateWindowSurfaceRects"/>
    public static Surface GetWindowSurface(Window window) => new(SDL_GetWindowSurface(window.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowSurfaceVSync(IntPtr window, int vsync);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowSurfaceVSync(SDL_Window *window, int vsync);</code>
    /// <summary>
    /// <para>Toggle VSync for the window surface.</para>
    /// <para>When a window surface is created, vsync defaults to 0.</para>
    /// <para>The `vsync` parameter can be 1 to synchronize present with every vertical
    /// refresh, 2 to synchronize present with every second vertical refresh, etc.,
    /// -1 for late swap tearing (adaptive vsync),
    /// or 0 to disable. Not every value is
    /// supported by every driver, so you should check the return value to see
    /// whether the requested setting is supported.</para>
    /// </summary>
    /// <param name="window">the window.</param>
    /// <param name="vsync">the vertical refresh sync interval.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowSurfaceVSync"/>
    public static int SetWindowSurfaceVSync(Window window, int vsync) => 
        SDL_SetWindowSurfaceVSync(window.Handle, vsync);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetWindowSurfaceVSync(IntPtr window, out int vsync);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetWindowSurfaceVSync(SDL_Window *window, int *vsync);</code>
    /// <summary>
    /// Get VSync for the window surface.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="vsync">an int filled with the current vertical refresh sync interval.
    /// See <see cref="SetWindowSurfaceVSync"/> for the meaning of the value.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="SetWindowSurfaceVSync"/>
    public static int GetWindowSurfaceVSync(Window window, out int vsync) => 
        SDL_GetWindowSurfaceVSync(window.Handle, out vsync);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_UpdateWindowSurface(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_UpdateWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// <para>Copy the window surface to the screen.</para>
    /// <para>This is the function you use to reflect any changes to the surface on the
    /// screen.</para>
    /// <para>This function is equivalent to the SDL 1.2 API SDL_Flip().</para>
    /// </summary>
    /// <param name="window">The window to update.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurfaceRects"/>
    public static int UpdateWindowSurface(Window window) => SDL_UpdateWindowSurface(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_UpdateWindowSurfaceRects(IntPtr window, IntPtr rects, int numrects);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_UpdateWindowSurfaceRects(SDL_Window *window, const SDL_Rect *rects, int numrects);</code>
    /// <summary>
    /// <para>Copy areas of the window surface to the screen.</para>
    /// <para>This is the function you use to reflect changes to portions of the surface
    /// on the screen.</para>
    /// <para>This function is equivalent to the SDL 1.2 API SDL_UpdateRects().</para>
    /// <para>Note that this function will update _at least_ the rectangles specified,
    /// but this is only intended as an optimization; in practice, this might
    /// update more of the screen (or all of the screen!), depending on what method
    /// SDL uses to send pixels to the system.</para>
    /// </summary>
    /// <param name="window">the window to update.</param>
    /// <param name="rects">an array of <see cref="Rect"/> structures representing areas of the
    /// surface to copy, in pixels.</param>
    /// <param name="numrects">the number of rectangles.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    public static int UpdateWindowSurfaceRects(Window window, Rect[] rects, int numrects)
    {
        var rectsPtr = rects != null ? Marshal.UnsafeAddrOfPinnedArrayElement(rects, 0) : IntPtr.Zero;
        return SDL_UpdateWindowSurfaceRects(window.Handle, rectsPtr, numrects);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_DestroyWindowSurface(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_DestroyWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// Destroy the surface associated with the window.
    /// </summary>
    /// <param name="window">window the window to update.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="WindowHasSurface"/>
    public static int DestroyWindowSurface(Window window) => SDL_DestroyWindowSurface(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowKeyboardGrab(IntPtr window, [MarshalAs(SDLBool)] bool grabbed);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowKeyboardGrab(SDL_Window *window, SDL_bool grabbed);</code>
    /// <summary>
    /// <para>Set a window's keyboard grab mode.</para>
    /// <para>Keyboard grab enables capture of system keyboard shortcuts like Alt+Tab or
    /// the Meta/Super key. Note that not all system keyboard shortcuts can be
    /// captured by applications (one example is Ctrl+Alt+Del on Windows).</para>
    /// <para>This is primarily intended for specialized applications such as VNC clients
    /// or VM frontends. Normal games should not use keyboard grab.</para>
    /// <para>When keyboard grab is enabled, SDL will continue to handle Alt+Tab when the
    /// window is full-screen to ensure the user is not trapped in your
    /// application. If you have a custom keyboard shortcut to exit fullscreen
    /// mode, you may suppress this behavior with
    /// <see cref="Hints.AllowAltTabWhileGrabbed"/>.</para>
    /// <para>If the caller enables a grab while another window is currently grabbed, the
    /// other window loses its grab in favor of the caller's window.</para>
    /// </summary>
    /// <param name="window">the window for which the keyboard grab mode should be set.</param>
    /// <param name="grabbed">this is <c>true</c> to grab keyboard, and <c>false</c> to release.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowKeyboardGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    public static int SetWindowKeyboardGrab(Window window, bool grabbed) =>
        SDL_SetWindowKeyboardGrab(window.Handle, grabbed);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowMouseGrab(IntPtr window, [MarshalAs(SDLBool)] bool grabbed);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowMouseGrab(SDL_Window *window, SDL_bool grabbed);</code>
    /// <summary>
    /// <para>Set a window's mouse grab mode.</para>
    /// <para>Mouse grab confines the mouse cursor to the window.</para>
    /// </summary>
    /// <param name="window">The window for which the mouse grab mode should be set.</param>
    /// <param name="grabbed">this is <c>true</c> to grab mouse, and <c>false</c> to release.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static int SetWindowMouseGrab(Window window, bool grabbed) =>
        SDL_SetWindowMouseGrab(window.Handle, grabbed);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GetWindowKeyboardGrab(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetWindowKeyboardGrab(SDL_Window *window);</code>
    /// <summary>
    /// Get a window's keyboard grab mode.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns><c>true</c> if keyboard is grabbed, and <c>false</c> otherwise.</returns>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static bool GetWindowKeyboardGrab(Window window) => SDL_GetWindowKeyboardGrab(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GetWindowMouseGrab(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetWindowMouseGrab(SDL_Window *window);</code>
    /// <summary>
    /// Get a window's mouse grab mode.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns><c>true</c> if mouse is grabbed, and <c>false</c> otherwise.</returns>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static bool GetWindowMouseGrab(Window window) => SDL_GetWindowMouseGrab(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGrabbedWindow();

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetGrabbedWindow(void);</code>
    /// <summary>
    /// Get the window that currently has an input grab enabled.
    /// </summary>
    /// <returns>the window if input is grabbed or NULL otherwise.</returns>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static Window? GetGrabbedWindow()
    {
        var windowHandle = SDL_GetGrabbedWindow();
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowMouseRect(IntPtr window, IntPtr rect);

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowMouseRect(SDL_Window *window, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Confines the cursor to the specified area of a window.</para>
    /// <para>Note that this does NOT grab the cursor, it only defines the area a cursor
    /// is restricted to when the window has mouse focus.</para>
    /// </summary>
    /// <param name="window">the window that will be associated with the barrier.</param>
    /// <param name="rect">a rectangle area in window-relative coordinates. If NULL the
    /// barrier for the specified window will be destroyed.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    public static int SetWindowMouseRect(Window window, Rect? rect)
    {
        var modePtr = IntPtr.Zero;

        try
        {
            if (rect.HasValue)
            {
                modePtr = Marshal.AllocHGlobal(Marshal.SizeOf<Rect>());
                Marshal.StructureToPtr(rect.Value, modePtr, false);
            }

            return SDL_SetWindowMouseRect(window.Handle, modePtr);
        }
        finally
        {
            if (modePtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(modePtr);
            }
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowMouseRect(IntPtr window);
    /// <code>extern SDL_DECLSPEC const SDL_Rect * SDLCALL SDL_GetWindowMouseRect(SDL_Window *window);</code>
    /// <summary>
    /// Get the mouse confinement rectangle of a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a pointer to the mouse confinement rectangle of a window, or NULL
    /// if there isn't one.</returns>
    /// <seealso cref="SetWindowMouseRect"/>
    public static Rect? GetWindowMouseRect(Window window)
    {
        var rectPtr = SDL_GetWindowMouseRect(window.Handle);
        return rectPtr == IntPtr.Zero ? null : Marshal.PtrToStructure<Rect>(rectPtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowOpacity(IntPtr window, float opacity);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowOpacity(SDL_Window *window, float opacity);</code>
    /// <summary>
    /// <para>Set the opacity for a window.</para>
    /// <para>The parameter `opacity` will be clamped internally between 0.0f
    /// (transparent) and 1.0f (opaque).</para>
    /// <para>This function also returns -1 if setting the opacity isn't supported.</para>
    /// </summary>
    /// <param name="window">the window which will be made transparent or opaque.</param>
    /// <param name="opacity">the opacity value (0.0f - transparent, 1.0f - opaque).</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GetWindowOpacity"/>
    public static int SetWindowOpacity(Window window, float opacity) => SDL_SetWindowOpacity(window.Handle, opacity);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowOpacity(IntPtr window);
    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetWindowOpacity(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the opacity of a window.</para>
    /// <para>If transparency isn't supported on this platform, opacity will be returned
    /// as 1.0f without error.</para>
    /// </summary>
    /// <param name="window">the window to get the current opacity value from.</param>
    /// <returns>the opacity, (0.0f - transparent, 1.0f - opaque), or a negative
    /// error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="SetWindowOpacity"/>
    public static float GetWindowOpacity(Window window) => SDL_GetWindowOpacity(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowModalFor(IntPtr modalWindow, IntPtr parentWindow);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowModalFor(SDL_Window *modal_window, SDL_Window *parent_window);</code>
    /// <summary>
    /// <para>Set the window as a modal to a parent window.</para>
    /// <para>If the window is already modal to an existing window, it will be reparented
    /// to the new owner. Setting the parent window to null unparents the modal
    /// window and removes modal status.</para>
    /// <para>Setting a window as modal to a parent that is a descendent of the modal
    /// window results in undefined behavior.</para>
    /// </summary>
    /// <param name="modalWindow">the window that should be set modal.</param>
    /// <param name="parentWindow">the parent window for the modal window.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int SetWindowModalFor(Window modalWindow, Window? parentWindow)
    {
        var parentHandle = parentWindow?.Handle ?? IntPtr.Zero;
        return SDL_SetWindowModalFor(modalWindow.Handle, parentHandle);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowFocusable(IntPtr window, [MarshalAs(SDLBool)] bool focusable);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowFocusable(SDL_Window *window, SDL_bool focusable);</code>
    /// <summary>
    /// Set whether the window may have input focus.
    /// </summary>
    /// <param name="window">the window to set focusable state.</param>
    /// <param name="focusable">true to allow input focus, false to not allow
    /// input focus.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int SetWindowFocusable(Window window, bool focusable) => 
        SDL_SetWindowFocusable(window.Handle, focusable);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ShowWindowSystemMenu(IntPtr window, int x, int y);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ShowWindowSystemMenu(SDL_Window *window, int x, int y);</code>
    /// <summary>
    /// <para>Display the system-level window menu.</para>
    /// <para>This default window menu is provided by the system and on some platforms
    /// provides functionality for setting or changing privileged state on the
    /// window, such as moving it between workspaces or displays, or toggling the
    /// always-on-top property.</para>
    /// <para>On platforms or desktops where this is unsupported, this function does
    /// nothing.</para>
    /// </summary>
    /// <param name="window">the window for which the menu will be displayed.</param>
    /// <param name="x">the x coordinate of the menu, relative to the origin (top-left) of
    /// the client area.</param>
    /// <param name="y">the y coordinate of the menu, relative to the origin (top-left) of
    /// the client area.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int ShowWindowSystemMenu(Window window, int x, int y) =>
        SDL_ShowWindowSystemMenu(window.Handle, x, y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowHitTest(IntPtr window, HitTest callback, IntPtr callbackData);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowHitTest(SDL_Window *window, SDL_HitTest callback, void *callback_data);</code>
    /// <summary>
    /// <para>Provide a callback that decides if a window region has special properties.</para>
    /// <para>Normally windows are dragged and resized by decorations provided by the
    /// system window manager (a title bar, borders, etc), but for some apps, it
    /// makes sense to drag them from somewhere else inside the window itself; for
    /// example, one might have a borderless window that wants to be draggable from
    /// any part, or simulate its own title bar, etc.</para>
    /// <para>This function lets the app provide a callback that designates pieces of a
    /// given window as special. This callback is run during event processing if we
    /// need to tell the OS to treat a region of the window specially; the use of
    /// this callback is known as "hit testing."</para>
    /// <para>Mouse input may not be delivered to your application if it is within a
    /// special area; the OS will often apply that input to moving the window or
    /// resizing the window and not deliver it to the application.</para>
    /// <para>Specifying NULL for a callback disables hit-testing. Hit-testing is
    /// disabled by default.</para>
    /// <para>Platforms that don't support this functionality will return -1
    /// unconditionally, even if you're attempting to disable hit-testing.</para>
    /// <para>Your callback may fire at any time, and its firing does not indicate any
    /// specific behavior (for example, on Windows, this certainly might fire when
    /// the OS is deciding whether to drag your window, but it fires for lots of
    /// other reasons, too, some unrelated to anything you probably care about _and
    /// when the mouse isn't actually at the location it is testing_). Since this
    /// can fire at any time, you should try to keep your callback efficient,
    /// devoid of allocations, etc.</para>
    /// </summary>
    /// <param name="window">the window to set hit-testing on.</param>
    /// <param name="callback">the function to call when doing a hit-test.</param>
    /// <param name="callbackData">an app-defined void pointer passed to **callback**.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int SetWindowHitTest(Window window, HitTest callback, IntPtr callbackData) =>
        SDL_SetWindowHitTest(window.Handle, callback, callbackData);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowShape(IntPtr window, IntPtr shape);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetWindowShape(SDL_Window *window, SDL_Surface *shape);</code>
    /// <summary>
    /// <para>Set the shape of a transparent window.</para>
    /// <para>This sets the alpha channel of a transparent window and any fully
    /// transparent areas are also transparent to mouse clicks. If you are using
    /// something besides the SDL render API, then you are responsible for setting
    /// the alpha channel of the window yourself.</para>
    /// <para>The shape is copied inside this function, so you can free it afterwards. If
    /// your shape surface changes, you should call <see cref="SetWindowShape"/> again to
    /// update the window.</para>
    /// <para>The window must have been created with the <see cref="WindowFlags.Transparent"/> flag.</para>
    /// </summary>
    /// <param name="window">the window.</param>
    /// <param name="shape">the surface representing the shape of the window, or NULL to
    /// remove any current shape.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int SetWindowShape(Window window, Surface? shape) =>
        SDL_SetWindowShape(window.Handle, shape?.Handle ?? IntPtr.Zero);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_FlashWindow(IntPtr window, FlashOperation operation);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_FlashWindow(SDL_Window *window, SDL_FlashOperation operation);</code>
    /// <summary>
    /// Request a window to demand attention from the user.
    /// </summary>
    /// <param name="window">the window to be flashed.</param>
    /// <param name="operation">the operation to perform.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int FlashWindow(Window window, FlashOperation operation) =>
        SDL_FlashWindow(window.Handle, operation);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Destroy a window.</para>
    /// <para>Any popups or modal windows owned by the window will be recursively
    /// destroyed as well.</para>
    /// </summary>
    /// <param name="window">the window to destroy.</param>
    /// <seealso cref="CreatePopupWindow"/>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    public static void DestroyWindow(Window window) => SDL_DestroyWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_ScreenSaverEnabled();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ScreenSaverEnabled(void);</code>
    /// <summary>
    /// <para>Check whether the screensaver is currently enabled</para>
    /// <para>The screensaver is disabled by default.</para>
    /// <para>The default can also be changed using <see cref="Hints.VideoAllowScreensaver"/>.</para>
    /// </summary>
    /// <returns><c>true</c> if the screensaver is enabled, <c>false</c> if it is
    /// disabled.</returns>
    /// <seealso cref="DisableScreenSaver"/>
    /// <seealso cref="EnableScreenSaver"/>
    public static bool ScreenSaverEnabled() => SDL_ScreenSaverEnabled();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_EnableScreenSaver();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_EnableScreenSaver(void);</code>
    /// <summary>
    /// <para>Prevent the screen from being blanked by a screen saver.</para>
    /// <para>If you disable the screensaver, it is automatically re-enabled when SDL
    /// quits.</para>
    /// <para>The screensaver is disabled by default, but this may by changed by
    /// <see cref="Hints.VideoAllowScreensaver"/>.</para>
    /// </summary>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="EnableScreenSaver"/>
    /// <seealso cref="ScreenSaverEnabled"/>
    public static int EnableScreenSaver() => SDL_EnableScreenSaver();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_DisableScreenSaver();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_DisableScreenSaver(void);</code>
    /// <summary>
    /// <para>Prevent the screen from being blanked by a screen saver.</para>
    /// <para>If you disable the screensaver, it is automatically re-enabled when SDL
    /// quits.</para>
    /// <para>The screensaver is disabled by default, but this may by changed by
    /// <see cref="Hints.VideoAllowScreensaver"/>.</para>
    /// </summary>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="EnableScreenSaver"/>
    /// <seealso cref="ScreenSaverEnabled"/>
    public static int DisableScreenSaver() => SDL_DisableScreenSaver();

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_LoadLibrary([MarshalAs(UnmanagedType.LPUTF8Str)] string? path);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_LoadLibrary(const char *path);</code>
    /// <summary>
    /// <para>Dynamically load an OpenGL library.</para>
    /// <para>This should be done after initializing the video driver, but before
    /// creating any OpenGL windows. If no OpenGL library is loaded, the default
    /// library will be loaded upon creation of the first OpenGL window.</para>
    /// <para>If you do this, you need to retrieve all of the GL functions used in your
    /// program from the dynamic library using <see cref="GLGetProcAddress"/>.</para>
    /// </summary>
    /// <param name="path">the platform dependent OpenGL library name, or NULL to open the
    /// default OpenGL library.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLGetProcAddress"/>
    /// <seealso cref="GLUnloadLibrary"/>
    public static int GLLoadLibrary(string? path) => SDL_GL_LoadLibrary(path);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_GetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
    /// <code>extern SDL_DECLSPEC SDL_FunctionPointer SDLCALL SDL_GL_GetProcAddress(const char *proc);</code>
    /// <summary>
    /// <para>Get an OpenGL function by name.</para>
    /// <para>If the GL library is loaded at runtime with <see cref="GLLoadLibrary"/>, then all
    /// GL functions must be retrieved this way. Usually this is used to retrieve
    /// function pointers to OpenGL extensions.</para>
    /// <para>There are some quirks to looking up OpenGL functions that require some
    /// extra care from the application. If you code carefully, you can handle
    /// these quirks without any platform-specific code, though:</para>
    /// <list type="bullet">
    /// <item>On Windows, function pointers are specific to the current GL context;
    /// this means you need to have created a GL context and made it current
    /// before calling <see cref="GLGetProcAddress"/>. If you recreate your context or
    /// create a second context, you should assume that any existing function
    /// pointers aren't valid to use with it. This is (currently) a
    /// Windows-specific limitation, and in practice lots of drivers don't suffer
    /// this limitation, but it is still the way the wgl API is documented to
    /// work and you should expect crashes if you don't respect it. Store a copy
    /// of the function pointers that comes and goes with context lifespan.</item>
    /// <item>On X11, function pointers returned by this function are valid for any
    /// context, and can even be looked up before a context is created at all.
    /// This means that, for at least some common OpenGL implementations, if you
    /// look up a function that doesn't exist, you'll get a non-NULL result that
    /// is _NOT_ safe to call. You must always make sure the function is actually
    /// available for a given GL context before calling it, by checking for the
    /// existence of the appropriate extension with <see cref="GLExtensionSupported"/>,
    /// or verifying that the version of OpenGL you're using offers the function
    /// as core functionality.</item>
    /// <item>Some OpenGL drivers, on all platforms, *will* return NULL if a function
    /// isn't supported, but you can't count on this behavior. Check for
    /// extensions you use, and if you get a NULL anyway, act as if that
    /// extension wasn't available. This is probably a bug in the driver, but you
    /// can code defensively for this scenario anyhow.</item>
    /// <item>Just because you're on Linux/Unix, don't assume you'll be using X11.
    /// Next-gen display servers are waiting to replace it, and may or may not
    /// make the same promises about function pointers.</item>
    /// <item>OpenGL function pointers must be declared `APIENTRY` as in the example
    /// code. This will ensure the proper calling convention is followed on
    /// platforms where this matters (Win32) thereby avoiding stack corruption.</item>
    /// </list>
    /// </summary>
    /// <param name="proc">the name of an OpenGL function.</param>
    /// <returns>a pointer to the named OpenGL function. The returned pointer
    /// should be cast to the appropriate function signature.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLExtensionSupported"/>
    /// <seealso cref="GLLoadLibrary"/>
    /// <seealso cref="GLUnloadLibrary"/>
    public static FunctionPointer? GLGetProcAddress(string proc)
    {
        var ptr = SDL_GL_GetProcAddress(proc);
        return ptr != IntPtr.Zero ? Marshal.GetDelegateForFunctionPointer<FunctionPointer>(ptr) : null;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
    /// <code>extern SDL_DECLSPEC SDL_FunctionPointer SDLCALL SDL_EGL_GetProcAddress(const char *proc);</code>
    /// <summary>
    /// <para>Get an EGL library function by name.</para>
    /// <para>If an EGL library is loaded, this function allows applications to get entry
    /// points for EGL functions. This is useful to provide to an EGL API and
    /// extension loader.</para>
    /// </summary>
    /// <param name="proc">the name of an EGL function.</param>
    /// <returns>a pointer to the named EGL function. The returned pointer should
    /// be cast to the appropriate function signature.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="EGLGetCurrentEGLDisplay"/>
    public static FunctionPointer? EGLGetProcAddress(string proc)
    {
        var ptr = SDL_EGL_GetProcAddress(proc);
        return ptr != IntPtr.Zero ? Marshal.GetDelegateForFunctionPointer<FunctionPointer>(ptr) : null;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GL_UnloadLibrary();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GL_UnloadLibrary(void);</code>
    /// <summary>
    /// Unload the OpenGL library previously loaded by <see cref="GLLoadLibrary"/>.
    /// </summary>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLLoadLibrary"/>
    public static void GLUnloadLibrary() => SDL_GL_UnloadLibrary();
    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GL_ExtensionSupported([MarshalAs(UnmanagedType.LPUTF8Str)] string extension);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GL_ExtensionSupported(const char *extension);</code>
    /// <summary>
    /// <para>Check if an OpenGL extension is supported for the current context.</para>
    /// <para>This function operates on the current GL context; you must have created a
    /// context and it must be current before calling this function. Do not assume
    /// that all contexts you create will have the same set of extensions
    /// available, or that recreating an existing context will offer the same
    /// extensions again.</para>
    /// <para>While it's probably not a massive overhead, this function is not an O(1)
    /// operation. Check the extensions you care about after creating the GL
    /// context and save that information somewhere instead of calling the function
    /// every time you need to know.</para>
    /// </summary>
    /// <param name="extension">the name of the extension to check.</param>
    /// <returns><c>true</c> if the extension is supported, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0</since>
    public static bool GLExtensionSupported(string extension) => SDL_GL_ExtensionSupported(extension);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GL_ResetAttributes();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GL_ResetAttributes(void);</code>
    /// <summary>
    /// Reset all previously set OpenGL context attributes to their default values.
    /// </summary>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLGetAttribute"/>
    /// <seealso cref="GLSetAttribute"/>
    public static void GLResetAttributes() => SDL_GL_ResetAttributes();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_SetAttribute(GLAttr attr, int value);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_SetAttribute(SDL_GLattr attr, int value);</code>
    /// <summary>
    /// <para>Set an OpenGL window attribute before window creation.</para>
    /// <para>This function sets the OpenGL attribute <c>attr</c> to <c>value</c>. The requested
    /// attributes should be set before creating an OpenGL window. You should use
    /// <see cref="GLGetAttribute"/> to check the values after creating the OpenGL
    /// context, since the values obtained can differ from the requested ones.</para>
    /// </summary>
    /// <param name="attr">an <see cref="GLAttr"/> enum value specifying the OpenGL attribute to
    /// set.</param>
    /// <param name="value">the desired value for the attribute.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLGetAttribute"/>
    /// <seealso cref="GLResetAttributes"/>
    public static int GLSetAttribute(GLAttr attr, int value) => SDL_GL_SetAttribute(attr, value);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_GetAttribute(GLAttr attr, out int value);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_GetAttribute(SDL_GLattr attr, int *value);</code>
    /// <summary>
    /// Get the actual value for an attribute from the current context.
    /// </summary>
    /// <param name="attr">an <see cref="GLAttr"/> enum value specifying the OpenGL attribute to
    /// get.</param>
    /// <param name="value">a pointer filled in with the current value of <c>attr</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLResetAttributes"/>
    /// <seealso cref="GLSetAttribute"/>
    public static int GLGetAttribute(GLAttr attr, out int value) => SDL_GL_GetAttribute(attr, out value);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_CreateContext(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_GLContext SDLCALL SDL_GL_CreateContext(SDL_Window *window);</code>
    /// <summary>
    /// <para>Create an OpenGL context for an OpenGL window, and make it current.</para>
    /// <para>Windows users new to OpenGL should note that, for historical reasons, GL
    /// functions added after OpenGL version 1.1 are not available by default.
    /// Those functions must be loaded at run-time, either with an OpenGL
    /// extension-handling library or with <see cref="GLGetProcAddress"/> and its related
    /// functions.</para>
    /// <para><see cref="GLContext"/> is opaque to the application.</para>
    /// </summary>
    /// <param name="window">the window to associate with the context.</param>
    /// <returns>the OpenGL context associated with `window` or NULL on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLDestroyContext"/>
    /// <seealso cref="GLMakeCurrent"/>
    public static GLContext? GLCreateContext(Window window)
    {
        var contextHandle = SDL_GL_CreateContext(window.Handle);
        return contextHandle != IntPtr.Zero ? new GLContext(contextHandle) : null;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_MakeCurrent(IntPtr window, IntPtr context);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_MakeCurrent(SDL_Window *window, SDL_GLContext context);</code>
    /// <summary>
    /// <para>Set up an OpenGL context for rendering into an OpenGL window.</para>
    /// <para>The context must have been created with a compatible window.</para>
    /// </summary>
    /// <param name="window">the window to associate with the context.</param>
    /// <param name="context">the OpenGL context to associate with the window.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLCreateContext"/>
    public static int GLMakeCurrent(Window window, GLContext context) =>
        SDL_GL_MakeCurrent(window.Handle, context.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_GetCurrentWindow();
    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GL_GetCurrentWindow(void);</code>
    /// <summary>
    /// Get the currently active OpenGL window.
    /// </summary>
    /// <returns>the currently active OpenGL window on success or NULL on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Window? GLGetCurrentWindow()
    {
        var windowHandle = SDL_GL_GetCurrentWindow();
        return windowHandle != IntPtr.Zero ? new Window(windowHandle) : null;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_GetCurrentContext();
    /// <code>extern SDL_DECLSPEC SDL_GLContext SDLCALL SDL_GL_GetCurrentContext(void);</code>
    /// <summary>
    /// Get the currently active OpenGL context.
    /// </summary>
    /// <returns>the currently active OpenGL context or NULL on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLMakeCurrent"/>
    public static GLContext? GLGetCurrentContext()
    {
        var contextHandle = SDL_GL_GetCurrentContext();
        return contextHandle == IntPtr.Zero ? null : new GLContext(contextHandle);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetCurrentEGLDisplay();
    /// <code>extern SDL_DECLSPEC SDL_EGLDisplay SDLCALL SDL_EGL_GetCurrentEGLDisplay(void);</code>
    /// <summary>
    /// Get the currently active EGL display.
    /// </summary>
    /// <returns>the currently active EGL display or <see cref="IntPtr.Zero"/> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static IntPtr EGLGetCurrentEGLDisplay() => SDL_EGL_GetCurrentEGLDisplay();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetCurrentEGLConfig();
    /// <code>extern SDL_DECLSPEC SDL_EGLConfig SDLCALL SDL_EGL_GetCurrentEGLConfig(void);</code>
    /// <summary>
    /// Get the currently active EGL config.
    /// </summary>
    /// <returns>the currently active EGL config or <see cref="IntPtr.Zero"/> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static IntPtr EGLGetCurrentConfig() => SDL_EGL_GetCurrentEGLConfig();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetWindowEGLSurface(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_EGLSurface SDLCALL SDL_EGL_GetWindowEGLSurface(SDL_Window *window);</code>
    /// <summary>
    /// Get the EGL surface associated with the window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the EGLSurface pointer associated with the window, or <see cref="IntPtr.Zero"/> on
    /// failure.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static IntPtr EGLGetWindowEGLSurface(Window window) => SDL_EGL_GetWindowEGLSurface(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EGL_SetEGLAttributeCallbacks(EGLAttribArrayCallback platformAttribCallback,
        EGLIntArrayCallback surfaceAttribCallback, EGLIntArrayCallback contextAttribCallback);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EGL_SetEGLAttributeCallbacks(SDL_EGLAttribArrayCallback platformAttribCallback, SDL_EGLIntArrayCallback surfaceAttribCallback, SDL_EGLIntArrayCallback contextAttribCallback);</code>
    /// <summary>
    /// <para>Sets the callbacks for defining custom EGLAttrib arrays for EGL
    /// initialization.</para>
    /// <para>Each callback should return a pointer to an EGL attribute array terminated
    /// with EGL_NONE. Callbacks may return NULL pointers to signal an error, which
    /// will cause the SDL_CreateWindow process to fail gracefully.</para>
    /// <para>The arrays returned by each callback will be appended to the existing
    /// attribute arrays defined by SDL.</para>
    /// <para>NOTE: These callback pointers will be reset after <see cref="GLResetAttributes"/>.</para>
    /// </summary>
    /// <param name="platformAttribCallback">callback for attributes to pass to eglGetPlatformDisplay.</param>
    /// <param name="surfaceAttribCallback">callback for attributes to pass to eglCreateSurface.</param>
    /// <param name="contextAttribCallback">callback for attributes to pass to eglCreateContext.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void SetEGLAttributeCallbacks(EGLAttribArrayCallback platformAttribCallback,
        EGLIntArrayCallback surfaceAttribCallback, EGLIntArrayCallback contextAttribCallback) => 
        SDL_EGL_SetEGLAttributeCallbacks(platformAttribCallback, surfaceAttribCallback, contextAttribCallback);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_SetSwapInterval(int interval);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_SetSwapInterval(int interval);</code>
    /// <summary>
    /// <para>Set the swap interval for the current OpenGL context.</para>
    /// <para>Some systems allow specifying -1 for the interval, to enable adaptive
    /// vsync. Adaptive vsync works the same as vsync, but if you've already missed
    /// the vertical retrace for a given frame, it swaps buffers immediately, which
    /// might be less jarring for the user during occasional framerate drops. If an
    /// application requests adaptive vsync and the system does not support it,
    /// this function will fail and return -1. In such a case, you should probably
    /// retry the call with 1 for the interval.</para>
    /// <para>Adaptive vsync is implemented for some glX drivers with
    /// GLX_EXT_swap_control_tear, and for some Windows drivers with
    /// WGL_EXT_swap_control_tear.</para>
    /// <para>Read more on the Khronos wiki:
    /// https://www.khronos.org/opengl/wiki/Swap_Interval#Adaptive_Vsync</para>
    /// </summary>
    /// <param name="interval">0 for immediate updates, 1 for updates synchronized with
    /// the vertical retrace, -1 for adaptive vsync.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLGetSwapInterval"/>
    public static int GLSetSwapInterval(int interval) => SDL_GL_SetSwapInterval(interval);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_GetSwapInterval(out int interval);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_GetSwapInterval(int *interval);</code>
    /// <summary>
    /// <para>Get the swap interval for the current OpenGL context.</para>
    /// <para>If the system can't determine the swap interval, or there isn't a valid
    /// current context, this function will set *interval to 0 as a safe default.</para>
    /// </summary>
    /// <param name="interval">output interval value. 0 if there is no vertical retrace
    /// synchronization, 1 if the buffer swap is synchronized with
    /// the vertical retrace, and -1 if late swaps happen
    /// immediately instead of waiting for the next retrace.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLSetSwapInterval"/>
    public static int GLGetSwapInterval(out int interval) => SDL_GL_GetSwapInterval(out interval);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_SwapWindow(IntPtr window);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_SwapWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Update a window with OpenGL rendering.</para>
    /// <para>This is used with double-buffered OpenGL contexts, which are the default.</para>
    /// <para>On macOS, make sure you bind 0 to the draw framebuffer before swapping the
    /// window, otherwise nothing will happen. If you aren't using
    /// glBindFramebuffer(), this is the default and you won't have to do anything
    /// extra.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GLSwapWindow(Window window) => SDL_GL_SwapWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GL_DestroyContext(IntPtr context);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GL_DestroyContext(SDL_GLContext context);</code>
    /// <summary>
    /// Delete an OpenGL context.
    /// </summary>
    /// <param name="context">the OpenGL context to be deleted.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GLCreateContext"/>
    public static int GLDestroyContext(GLContext context) => SDL_GL_DestroyContext(context.Handle);
}