#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumVideoDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumVideoDrivers();
    private delegate int GetNumVideoDriversNativeDelegate();
    private static GetNumVideoDriversNativeDelegate GetNumVideoDriversNativeFunction = SDL_GetNumVideoDrivers;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumVideoDrivers(void);</code>
    /// <summary>
    /// Get the number of video drivers compiled into SDL.
    /// </summary>
    /// <returns>the number of built in video drivers.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetVideoDriver"/>
    public static int GetNumVideoDrivers()
    {
        return GetNumVideoDriversNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetVideoDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetVideoDriver(int index);
    private delegate IntPtr GetVideoDriverNativeDelegate(int index);
    private static GetVideoDriverNativeDelegate GetVideoDriverNativeFunction = SDL_GetVideoDriver;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetVideoDriver(int index);</code>
    /// <summary>
    /// <para>Get the name of a built in video driver.</para>
    /// <para>The video drivers are presented in the order in which they are normally
    /// checked during initialization.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like <c>"cocoa"</c>,
    /// <c>"x11"</c> or <c>"windows"</c>. These never have Unicode characters, and are not meant
    /// to be proper names.</para>
    /// </summary>
    /// <param name="index">the index of a video driver.</param>
    /// <returns>the name of the video driver with the given <b>index</b>, or <c>null</c> if
    /// index is out of bounds.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumVideoDrivers"/>
    public static string GetVideoDriver(int index)
    {
        var value = GetVideoDriverNativeFunction(index);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentVideoDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentVideoDriver();
    private delegate IntPtr GetCurrentVideoDriverNativeDelegate();
    private static GetCurrentVideoDriverNativeDelegate GetCurrentVideoDriverNativeFunction = SDL_GetCurrentVideoDriver;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetCurrentVideoDriver(void);</code>
    /// <summary>
    /// <para>Get the name of the currently initialized video driver.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like <c>"cocoa"</c>,
    /// <c>"x11"</c> or <c>"windows"</c>. These never have Unicode characters, and are not meant
    /// to be proper names.</para>
    /// </summary>
    /// <returns>the name of the current video driver or <c>null</c> if no driver has been
    /// initialized.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumVideoDrivers"/>
    /// <seealso cref="GetVideoDriver"/>
    public static string? GetCurrentVideoDriver()
    {
        var value = GetCurrentVideoDriverNativeFunction();
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSystemTheme"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme SDL_GetSystemTheme();
    private delegate SystemTheme GetSystemThemeNativeDelegate();
    private static GetSystemThemeNativeDelegate GetSystemThemeNativeFunction = SDL_GetSystemTheme;

    /// <code>extern SDL_DECLSPEC SDL_SystemTheme SDLCALL SDL_GetSystemTheme(void);</code>
    /// <summary>
    /// Get the current system theme.
    /// </summary>
    /// <returns>the current system theme, light, dark, or unknown.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static SystemTheme GetSystemTheme()
    {
        return GetSystemThemeNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplays"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDisplays(out int count);
    private delegate IntPtr GetDisplaysNativeDelegate(out int count);
    private static GetDisplaysNativeDelegate GetDisplaysNativeFunction = SDL_GetDisplays;
    /// <code>extern SDL_DECLSPEC SDL_DisplayID * SDLCALL SDL_GetDisplays(int *count);</code>
    /// <summary>
    /// Get a list of currently connected displays.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of displays returned, may
    /// be <c>null</c>.</param>
    /// <returns>a <c>0</c> terminated array of display instance IDs or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint[]? GetDisplays(out int count)
    {
        var ptr = GetDisplaysNativeFunction(out count);

        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPrimaryDisplay"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPrimaryDisplay();
    private delegate uint GetPrimaryDisplayNativeDelegate();
    private static GetPrimaryDisplayNativeDelegate GetPrimaryDisplayNativeFunction = SDL_GetPrimaryDisplay;

    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetPrimaryDisplay(void);</code>
    /// <summary>
    /// Return the primary display.
    /// </summary>
    /// <returns>the instance ID of the primary display on success or <c>0</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplays"/>
    public static uint GetPrimaryDisplay()
    {
        return GetPrimaryDisplayNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayProperties(uint displayID);
    private delegate uint GetDisplayPropertiesNativeDelegate(uint displayID);
    private static GetDisplayPropertiesNativeDelegate GetDisplayPropertiesNativeFunction = SDL_GetDisplayProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetDisplayProperties(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the properties associated with a display.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.DisplayHDREnabledBoolean"/>: <c>true</c> if the display has HDR
    /// headroom above the SDR white point. This is for informational and
    /// diagnostic purposes only, as not all platforms provide this information
    /// at the display level.</item>
    /// </list>
    /// <para>On KMS/DRM:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.DisplayKMSDRMPanelOrientationNumber"/>: the "panel
    /// orientation" property for the display in degrees of clockwise rotation.
    /// Note that this is provided only as a hint, and the application is
    /// responsible for any coordinate transformations needed to conform to the
    /// requested display orientation.</item>
    /// </list>
    /// <para>On Wayland:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.DisplayWaylandWLOutputPointer"/>: the wl_output associated
    /// with the display</item>
    /// </list>
    /// <para>On Windows:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.DisplayWindowsHMonitorPointer"/>: the monitor handle
    /// (HMONITOR) associated with the display</item>
    /// </list>
    /// </summary>
    /// <param name="displayID">displayID the instance ID of the display to query.</param>
    /// <returns>a valid property ID on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GetDisplayProperties(uint displayID)
    {
        return GetDisplayPropertiesNativeFunction(displayID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDisplayName(uint displayID);
    private delegate IntPtr GetDisplayNameNativeDelegate(uint displayID);
    private static GetDisplayNameNativeDelegate GetDisplayNameNativeFunction = SDL_GetDisplayName;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetDisplayName(SDL_DisplayID displayID);</code>
    /// <summary>
    /// Get the name of a display in UTF-8 encoding.
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the name of a display or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplays"/>
    public static string? GetDisplayName(uint displayID)
    {
        var value = GetDisplayNameNativeFunction(displayID);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayBounds"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetDisplayBounds(uint displayID, out Rect rect);
    private delegate bool GetDisplayBoundsNativeDelegate(uint displayID, out Rect rect);
    private static GetDisplayBoundsNativeDelegate GetDisplayBoundsNativeFunction = SDL_GetDisplayBounds;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetDisplayBounds(SDL_DisplayID displayID, SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Get the desktop area represented by a display.</para>
    /// <para>The primary display is often located at (0,0), but may be placed at a
    /// different location depending on monitor layout.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <param name="rect">the <see cref="Rect"/> structure filled in with the display bounds.</param>
    /// <returns><c>success</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplayUsableBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static bool GetDisplayBounds(uint displayID, out Rect rect)
    {
        return GetDisplayBoundsNativeFunction(displayID, out rect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayUsableBounds"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetDisplayUsableBounds(uint displayID, out Rect rect);
    private delegate bool GetDisplayUsableBoundsNativeDelegate(uint displayID, out Rect rect);
    private static GetDisplayUsableBoundsNativeDelegate GetDisplayUsableBoundsNativeFunction = SDL_GetDisplayUsableBounds;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetDisplayUsableBounds(SDL_DisplayID displayID, SDL_Rect *rect);</code>
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
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static bool GetDisplayUsableBounds(uint displayID, out Rect rect)
    {
        return GetDisplayUsableBoundsNativeFunction(displayID, out rect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNaturalDisplayOrientation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayID);
    private delegate DisplayOrientation GetNaturalDisplayOrientationNativeDelegate(uint displayID);
    private static GetNaturalDisplayOrientationNativeDelegate GetNaturalDisplayOrientationNativeFunction = SDL_GetNaturalDisplayOrientation;

    /// <code>extern SDL_DECLSPEC SDL_DisplayOrientation SDLCALL SDL_GetNaturalDisplayOrientation(SDL_DisplayID displayID);</code>
    /// <summary>
    /// Get the orientation of a display when it is unrotated.
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the <see cref="DisplayOrientation"/> enum value of the display, or
    /// <see cref="DisplayOrientation.Unknown"/> if it isn't available.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplays"/>
    public static DisplayOrientation GetNaturalDisplayOrientation(uint displayID)
    {
        return GetNaturalDisplayOrientationNativeFunction(displayID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentDisplayOrientation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayID);
    private delegate DisplayOrientation GetCurrentDisplayOrientationNativeDelegate(uint displayID);
    private static GetCurrentDisplayOrientationNativeDelegate GetCurrentDisplayOrientationNativeFunction = SDL_GetCurrentDisplayOrientation;

    /// <code>extern SDL_DECLSPEC SDL_DisplayOrientation SDLCALL SDL_GetCurrentDisplayOrientation(SDL_DisplayID displayID);</code>
    /// <summary>
    /// Get the orientation of a display.
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the <see cref="DisplayOrientation"/> enum value of the display, or
    /// <see cref="DisplayOrientation.Unknown"/> if it isn't available.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplays"/>
    public static DisplayOrientation GetCurrentDisplayOrientation(uint displayID)
    {
        return GetCurrentDisplayOrientationNativeFunction(displayID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayContentScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetDisplayContentScale(uint displayID);
    private delegate float GetDisplayContentScaleNativeDelegate(uint displayID);
    private static GetDisplayContentScaleNativeDelegate GetDisplayContentScaleNativeFunction = SDL_GetDisplayContentScale;

    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetDisplayContentScale(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get the content scale of a display.</para>
    /// <para>The content scale is the expected scale for content based on the DPI
    /// settings of the display. For example, a 4K display might have a 2.0 (200%)
    /// display scale, which means that the user expects UI elements to be twice as
    /// big on this display, to aid in readability.</para>
    /// <para>After window creation, <see cref="GetWindowDisplayScale"/> should be used to query
    /// the content scale factor for individual windows instead of querying the
    /// display for a window and calling this function, as the per-window content
    /// scale factor may differ from the base value of the display it is on,
    /// particularly on high-DPI and/or multi-monitor desktop configurations.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>the content scale of the display, or <c>0.0f</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowDisplayScale"/>
    /// <seealso cref="GetDisplays"/>
    public static float GetDisplayContentScale(uint displayID)
    {
        return GetDisplayContentScaleNativeFunction(displayID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetFullscreenDisplayModes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetFullscreenDisplayModes(uint displayID, out int count);
    private delegate IntPtr GetFullscreenDisplayModesNativeDelegate(uint displayID, out int count);
    private static GetFullscreenDisplayModesNativeDelegate GetFullscreenDisplayModesNativeFunction = SDL_GetFullscreenDisplayModes;
    /// <code>extern SDL_DECLSPEC SDL_DisplayMode ** SDLCALL SDL_GetFullscreenDisplayModes(SDL_DisplayID displayID, int *count);</code>
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
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <param name="count">a pointer filled in with the number of display modes returned,
    /// may be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of display mode pointers or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information. This is a
    /// single allocation that should be freed with <see cref="Free"/> when it is
    /// no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplays"/>
    public static DisplayMode[]? GetFullscreenDisplayModes(uint displayID, out int count)
    {
        var ptr = GetFullscreenDisplayModesNativeFunction(displayID, out count);

        try
        {
            return PointerToStructureArray<DisplayMode>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetClosestFullscreenDisplayMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        [MarshalAs(UnmanagedType.I1)] bool includeHighDensityModes, out DisplayMode closest);
    private delegate bool GetClosestFullscreenDisplayModeNativeDelegate(uint displayID, int w, int h, float refreshRate,
        bool includeHighDensityModes, out DisplayMode closest);
    private static GetClosestFullscreenDisplayModeNativeDelegate GetClosestFullscreenDisplayModeNativeFunction = SDL_GetClosestFullscreenDisplayMode;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetClosestFullscreenDisplayMode(SDL_DisplayID displayID, int w, int h, float refresh_rate, bool include_high_density_modes, SDL_DisplayMode *closest);</code>
    /// <summary>
    /// <para>Get the closest match to the requested display mode.</para>
    /// <para>The available display modes are scanned and <c>closest</c> is filled in with the
    /// closest mode matching the requested mode and returned. The mode format and
    /// refresh rate default to the desktop mode if they are set to 0. The modes
    /// are scanned with size being first priority, format being second priority,
    /// and finally checking the refresh rate. If all the available modes are too
    /// small, then <c>false</c> is returned.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <param name="w">the width in pixels of the desired display mode.</param>
    /// <param name="h">the height in pixels of the desired display mode.</param>
    /// <param name="refreshRate">the refresh rate of the desired display mode, or 0.0f
    /// for the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">boolean to include high density modes in
    /// the search.</param>
    /// <param name="closest">a pointer filled in with the closest display mode equal to
    /// or larger than the desired mode.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplays"/>
    /// <seealso cref="GetFullscreenDisplayModes"/>
    public static bool GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        [MarshalAs(UnmanagedType.I1)] bool includeHighDensityModes, out DisplayMode closest)
    {
        return GetClosestFullscreenDisplayModeNativeFunction(displayID, w, h, refreshRate, includeHighDensityModes, out closest);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDesktopDisplayMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDesktopDisplayMode(uint displayID);
    private delegate IntPtr GetDesktopDisplayModeNativeDelegate(uint displayID);
    private static GetDesktopDisplayModeNativeDelegate GetDesktopDisplayModeNativeFunction = SDL_GetDesktopDisplayMode;
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode * SDLCALL SDL_GetDesktopDisplayMode(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get information about the desktop's display mode.</para>
    /// <para>There's a difference between this function and <see cref="GetCurrentDisplayMode"/>
    /// when SDL runs fullscreen and has changed the resolution. In that case this
    /// function will return the previous native display mode, and not the current
    /// display mode.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>a pointer to the desktop display mode or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCurrentDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    public static DisplayMode? GetDesktopDisplayMode(uint displayID) =>
        PointerToStructure<DisplayMode>(GetDesktopDisplayModeNativeFunction(displayID));


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentDisplayMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentDisplayMode(uint displayID);
    private delegate IntPtr GetCurrentDisplayModeNativeDelegate(uint displayID);
    private static GetCurrentDisplayModeNativeDelegate GetCurrentDisplayModeNativeFunction = SDL_GetCurrentDisplayMode;
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode * SDLCALL SDL_GetCurrentDisplayMode(SDL_DisplayID displayID);</code>
    /// <summary>
    /// <para>Get information about the current display mode.</para>
    /// <para>There's a difference between this function and <see cref="GetDesktopDisplayMode"/>
    /// when SDL runs fullscreen and has changed the resolution. In that case this
    /// function will return the current display mode, and not the previous native
    /// display mode.</para>
    /// </summary>
    /// <param name="displayID">the instance ID of the display to query.</param>
    /// <returns>a pointer to the desktop display mode or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDesktopDisplayMode"/>
    /// <seealso cref="GetDisplays"/>
    public static DisplayMode? GetCurrentDisplayMode(uint displayID) =>
        PointerToStructure<DisplayMode>(GetCurrentDisplayModeNativeFunction(displayID));


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayForPoint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForPoint(Point point);
    private delegate uint GetDisplayForPointNativeDelegate(Point point);
    private static GetDisplayForPointNativeDelegate GetDisplayForPointNativeFunction = SDL_GetDisplayForPoint;

    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetDisplayForPoint(const SDL_Point *point);</code>
    /// <summary>
    /// Get the display containing a point.
    /// </summary>
    /// <param name="point">the point to query.</param>
    /// <returns>the instance ID of the display containing the point or <c>0</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static uint GetDisplayForPoint(Point point)
    {
        return GetDisplayForPointNativeFunction(point);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayForRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForRect(Rect rect);
    private delegate uint GetDisplayForRectNativeDelegate(Rect rect);
    private static GetDisplayForRectNativeDelegate GetDisplayForRectNativeFunction = SDL_GetDisplayForRect;

    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetDisplayForRect(const SDL_Rect *rect);</code>
    /// <summary>
    /// Get the display primarily containing a rect.
    /// </summary>
    /// <param name="rect">the rect to query.</param>
    /// <returns>the instance ID of the display entirely containing the rect or
    /// closest to the center of the rect on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static uint GetDisplayForRect(Rect rect)
    {
        return GetDisplayForRectNativeFunction(rect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDisplayForWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForWindow(IntPtr window);
    private delegate uint GetDisplayForWindowNativeDelegate(IntPtr window);
    private static GetDisplayForWindowNativeDelegate GetDisplayForWindowNativeFunction = SDL_GetDisplayForWindow;

    /// <code>extern SDL_DECLSPEC SDL_DisplayID SDLCALL SDL_GetDisplayForWindow(SDL_Window *window);</code>
    /// <summary>
    /// Get the display associated with a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the instance ID of the display containing the center of the window
    /// on success or <c>0</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDisplayBounds"/>
    /// <seealso cref="GetDisplays"/>
    public static uint GetDisplayForWindow(IntPtr window)
    {
        return GetDisplayForWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowPixelDensity"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowPixelDensity(IntPtr window);
    private delegate float GetWindowPixelDensityNativeDelegate(IntPtr window);
    private static GetWindowPixelDensityNativeDelegate GetWindowPixelDensityNativeFunction = SDL_GetWindowPixelDensity;

    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetWindowPixelDensity(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the pixel density of a window.</para>
    /// <para>This is a ratio of pixel size to window size. For example, if the window is
    /// 1920x1080 and it has a high density back buffer of 3840x2160 pixels, it
    /// would have a pixel density of 2.0.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the pixel density or <c>0.0f</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowDisplayScale"/>
    public static float GetWindowPixelDensity(IntPtr window)
    {
        return GetWindowPixelDensityNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowDisplayScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowDisplayScale(IntPtr window);
    private delegate float GetWindowDisplayScaleNativeDelegate(IntPtr window);
    private static GetWindowDisplayScaleNativeDelegate GetWindowDisplayScaleNativeFunction = SDL_GetWindowDisplayScale;

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
    /// <returns>the display scale, or <c>0.0f</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static float GetWindowDisplayScale(IntPtr window)
    {
        return GetWindowDisplayScaleNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowFullscreenMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowFullscreenModePointer(IntPtr window, IntPtr mode);
    private delegate bool SetWindowFullscreenModePointerNativeDelegate(IntPtr window, IntPtr mode);
    private static SetWindowFullscreenModePointerNativeDelegate SetWindowFullscreenModePointerNativeFunction = SDL_SetWindowFullscreenModePointer;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowFullscreenMode(SDL_Window *window, const SDL_DisplayMode *mode);</code>
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
    /// <para>When the new mode takes effect, an <see cref="EventType.WindowResized"/> and/or an
    /// <see cref="EventType.WindowPixelSizeChanged"/> event will be emitted with the new mode
    /// dimensions.</para>
    /// </summary>
    /// <param name="window">the window to affect.</param>
    /// <param name="mode">a pointer to the display mode to use, which can be <c>null</c> for
    /// borderless fullscreen desktop mode, or one of the fullscreen
    /// modes returned by <see cref="GetFullscreenDisplayModes"/> to set an
    /// exclusive fullscreen mode.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreenMode(nint, nint)"/>
    /// <seealso cref="SyncWindow"/>
    public static bool SetWindowFullscreenMode(IntPtr window, IntPtr mode)
    {
        return SetWindowFullscreenModePointerNativeFunction(window, mode);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowFullscreenMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowFullscreenModeMode(IntPtr window, in DisplayMode mode);
    private delegate bool SetWindowFullscreenModeModeNativeDelegate(IntPtr window, in DisplayMode mode);
    private static SetWindowFullscreenModeModeNativeDelegate SetWindowFullscreenModeModeNativeFunction = SDL_SetWindowFullscreenModeMode;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowFullscreenMode(SDL_Window *window, const SDL_DisplayMode *mode);</code>
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
    /// <para>When the new mode takes effect, an <see cref="EventType.WindowResized"/> and/or an
    /// <see cref="EventType.WindowPixelSizeChanged"/> event will be emitted with the new mode
    /// dimensions.</para>
    /// </summary>
    /// <param name="window">the window to affect.</param>
    /// <param name="mode">a pointer to the display mode to use, which can be <c>null</c> for
    /// borderless fullscreen desktop mode, or one of the fullscreen
    /// modes returned by <see cref="GetFullscreenDisplayModes"/> to set an
    /// exclusive fullscreen mode.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="SyncWindow"/>
    public static bool SetWindowFullscreenMode(IntPtr window, DisplayMode mode)
    {
        return SetWindowFullscreenModeModeNativeFunction(window, in mode);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowFullscreenMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowFullscreenMode(IntPtr window);
    private delegate IntPtr GetWindowFullscreenModeNativeDelegate(IntPtr window);
    private static GetWindowFullscreenModeNativeDelegate GetWindowFullscreenModeNativeFunction = SDL_GetWindowFullscreenMode;
    /// <code>extern SDL_DECLSPEC const SDL_DisplayMode * SDLCALL SDL_GetWindowFullscreenMode(SDL_Window *window);</code>
    /// <summary>
    /// Query the display mode to use when a window is visible at fullscreen.
    /// </summary>
    /// <param name="window">window the window to query.</param>
    /// <returns>a pointer to the exclusive fullscreen mode to use or <c>null</c> for
    /// borderless fullscreen desktop mode.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowFullscreenMode(nint, nint)"/>
    /// <seealso cref="SetWindowFullscreen"/>
    public static DisplayMode? GetWindowFullscreenMode(IntPtr window) =>
        PointerToStructure<DisplayMode>(GetWindowFullscreenModeNativeFunction(window));


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowICCProfile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowICCProfile(IntPtr window, out UIntPtr size);
    private delegate IntPtr GetWindowICCProfileNativeDelegate(IntPtr window, out UIntPtr size);
    private static GetWindowICCProfileNativeDelegate GetWindowICCProfileNativeFunction = SDL_GetWindowICCProfile;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetWindowICCProfile(SDL_Window *window, size_t *size);</code>
    /// <summary>
    /// <para>Get the raw ICC profile data for the screen the window is currently on.</para>
    /// <para>Data returned should be freed with <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="size">the size of the ICC profile.</param>
    /// <returns>the raw ICC profile data on success or <c>null</c> on failure; call
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <see cref="GetError"/> for more information. This should be freed with
    /// <see cref="Free"/> when it is no longer needed.</returns>
    public static IntPtr GetWindowICCProfile(IntPtr window, out UIntPtr size)
    {
        return GetWindowICCProfileNativeFunction(window, out size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowPixelFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetWindowPixelFormat(IntPtr window);
    private delegate PixelFormat GetWindowPixelFormatNativeDelegate(IntPtr window);
    private static GetWindowPixelFormatNativeDelegate GetWindowPixelFormatNativeFunction = SDL_GetWindowPixelFormat;

    /// <code>extern SDL_DECLSPEC SDL_PixelFormat SDLCALL SDL_GetWindowPixelFormat(SDL_Window *window);</code>
    /// <summary>
    /// Get the pixel format associated with the window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the pixel format of the window on success or
    /// <see cref="PixelFormat.Unknown"/> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static PixelFormat GetWindowPixelFormat(IntPtr window)
    {
        return GetWindowPixelFormatNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindows"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindows(out int count);
    private delegate IntPtr GetWindowsNativeDelegate(out int count);
    private static GetWindowsNativeDelegate GetWindowsNativeFunction = SDL_GetWindows;
    /// <code>extern SDL_DECLSPEC SDL_Window ** SDLCALL SDL_GetWindows(int *count);</code>
    /// <summary>
    /// Get a list of valid windows.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of windows returned, may
    /// be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of SDL_Window pointers or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This is a single
    /// allocation that should be freed with <see cref="Free"/> when it is no
    /// longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr[]? GetWindows(out int count)
    {
        var ptr = GetWindowsNativeFunction(out count);

        try
        {
            return PointerToPointerArray(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateWindow([MarshalAs(UnmanagedType.LPUTF8Str)] string title, int w, int h, WindowFlags flags);
    private delegate IntPtr CreateWindowNativeDelegate(string title, int w, int h, WindowFlags flags);
    private static CreateWindowNativeDelegate CreateWindowNativeFunction = SDL_CreateWindow;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_CreateWindow(const char *title, int w, int h, SDL_WindowFlags flags);</code>
    /// <summary>
    /// <para>Create a window with the specified dimensions and flags.</para>
    /// <para>The window size is a request and may be different than expected based on
    /// the desktop layout and window manager policies. Your application should be
    /// prepared to handle a window of any size.</para>
    /// <para><c>flags</c> may be any of the following OR'd together:</para>
    /// <list type="bullet">
    /// <item><see cref="WindowFlags.Fullscreen"/>: fullscreen window at desktop resolution</item>
    /// <item><see cref="WindowFlags.OpenGL"/>: window usable with an OpenGL context</item>
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
    /// <item><see cref="WindowFlags.HighPixelDensity"/>: window uses high pixel density back
    /// buffer if possible</item>
    /// <item><see cref="WindowFlags.MouseCapture"/>: window has mouse captured (unrelated to
    /// <see cref="WindowFlags.MouseGrabbed"/>)</item>
    /// <item><see cref="WindowFlags.AlwaysOnTop"/>: window should always be above others</item>
    /// <item><see cref="WindowFlags.Utility"/>: window should be treated as a utility window, not
    /// showing in the task bar and window list</item>
    /// <item><see cref="WindowFlags.Tooltip"/>: window should be treated as a tooltip and does not
    /// get mouse or keyboard focus, requires a parent window</item>
    /// <item><see cref="WindowFlags.PopupMenu"/>: window should be treated as a popup menu,
    /// requires a parent window</item>
    /// <item><see cref="WindowFlags.KeyboardGrabbed"/>: window has grabbed keyboard input</item>
    /// <item><see cref="WindowFlags.Vulkan"/>: window usable with a Vulkan instance</item>
    /// <item><see cref="WindowFlags.Metal"/>: window usable with a Metal instance</item>
    /// <item><see cref="WindowFlags.Transparent"/>: window with transparent buffer</item>
    /// <item><see cref="WindowFlags.NotFocusable"/>: window should not be focusable</item>
    /// </list>
    /// <para>The SDL_Window will be shown if <see cref="WindowFlags.Hidden"/> is not set. If hidden at
    /// creation time, <see cref="ShowWindow"/> can be used to show it later.</para>
    /// <para>On Apple's macOS, you <b>must</b> set the NSHighResolutionCapable Info.plist
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
    /// <see cref="CreateWindow"/> will fail, because <see cref="VulkanLoadLibrary"/> will fail.</para>
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
    /// <returns>the window that was created or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateWindowAndRenderer"/>
    /// <seealso cref="CreatePopupWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// <seealso cref="DestroyWindow"/>
    public static IntPtr CreateWindow([MarshalAs(UnmanagedType.LPUTF8Str)] string title, int w, int h, WindowFlags flags)
    {
        return CreateWindowNativeFunction(title, w, h, flags);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreatePopupWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreatePopupWindow(IntPtr parent, int offsetX, int offsetY, int w, int h, WindowFlags flags);
    private delegate IntPtr CreatePopupWindowNativeDelegate(IntPtr parent, int offsetX, int offsetY, int w, int h, WindowFlags flags);
    private static CreatePopupWindowNativeDelegate CreatePopupWindowNativeFunction = SDL_CreatePopupWindow;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_CreatePopupWindow(SDL_Window *parent, int offset_x, int offset_y, int w, int h, SDL_WindowFlags flags);</code>
    /// <summary>
    /// <para>Create a child popup window of the specified parent window.</para>
    /// <para>The window size is a request and may be different than expected based on
    /// the desktop layout and window manager policies. Your application should be
    /// prepared to handle a window of any size.</para>
    /// <para>The flags parameter **must** contain at least one of the following:</para>
    /// <list type="bullet">
    /// <item><see cref="WindowFlags.Tooltip"/>: The popup window is a tooltip and will not pass any
    /// input events.</item>
    /// <item><see cref="WindowFlags.PopupMenu"/>: The popup window is a popup menu. The topmost
    /// popup menu will implicitly gain the keyboard focus.</item>
    /// </list>
    /// <para>The following flags are not relevant to popup window creation and will be
    /// ignored:</para>
    /// <list type="bullet">
    /// <item><see cref="WindowFlags.Minimized"/></item>
    /// <item><see cref="WindowFlags.Maximized"/></item>
    /// <item><see cref="WindowFlags.Fullscreen"/></item>
    /// <item><see cref="WindowFlags.Borderless"/></item>
    /// </list>
    /// <para>The following flags are incompatible with popup window creation and will
    /// cause it to fail:</para>
    /// <list type="bullet">
    /// <item><see cref="WindowFlags.Utility"/></item>
    /// <item><see cref="WindowFlags.Modal"/></item>
    /// </list>
    /// <para>The parent parameter <b>must</b> be non-<c>null</c> and a valid window. The parent of
    /// a popup window can be either a regular, toplevel window, or another popup
    /// window.</para>
    /// <para>Popup windows cannot be minimized, maximized, made fullscreen, raised,
    /// flash, be made a modal window, be the parent of a toplevel window, or grab
    /// the mouse and/or keyboard. Attempts to do so will fail.</para>
    /// <para>Popup windows implicitly do not have a border/decorations and do not appear
    /// on the taskbar/dock or in lists of windows such as alt-tab menus.</para>
    /// <para>By default, popup window positions will automatically be constrained to
    /// keep the entire window within display bounds. This can be overridden with
    /// the <see cref="Props.WindowCreateConstrainPopupBoolean"/> property.</para>
    /// <para>By default, popup menus will automatically grab keyboard focus from the
    /// parent when shown. This behavior can be overridden by setting the
    /// <see cref="WindowFlags.NotFocusable"/> flag, setting the
    /// <see cref="Props.WindowCreateFocusableBoolean"/> property to <c>false</c>, or toggling
    /// it after creation via the <see cref="SetWindowFocusable"/> function.</para>
    /// <para>If a parent window is hidden or destroyed, any child popup windows will be
    /// recursively hidden or destroyed as well. Child popup windows not explicitly
    /// hidden will be restored when the parent is shown.</para>
    /// </summary>
    /// <param name="parent">the parent of the window, must not be <c>null</c>.</param>
    /// <param name="offsetX">the x position of the popup window relative to the origin
    /// of the parent.</param>
    /// <param name="offsetY">the y position of the popup window relative to the origin
    /// of the parent window.</param>
    /// <param name="w">the width of the window.</param>
    /// <param name="h">the height of the window.</param>
    /// <param name="flags"><see cref="WindowFlags.Tooltip"/> or <see cref="WindowFlags.PopupMenu"/>, and zero or more
    /// additional <see cref="WindowFlags"/> OR'd together.</param>
    /// <returns>the window that was created or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    /// <seealso cref="DestroyWindow"/>
    /// <seealso cref="GetWindowParent"/>
    public static IntPtr CreatePopupWindow(IntPtr parent, int offsetX, int offsetY, int w, int h, WindowFlags flags)
    {
        return CreatePopupWindowNativeFunction(parent, offsetX, offsetY, w, h, flags);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateWindowWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateWindowWithProperties(uint props);
    private delegate IntPtr CreateWindowWithPropertiesNativeDelegate(uint props);
    private static CreateWindowWithPropertiesNativeDelegate CreateWindowWithPropertiesNativeFunction = SDL_CreateWindowWithProperties;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_CreateWindowWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a window with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateAlwaysOnTopBoolean"/>: <c>true</c> if the window should
    /// be always on top</item>
    /// <item><see cref="Props.WindowCreateBorderlessBoolean"/>: <c>true</c> if the window has no
    /// window decoration</item>
    /// <item><see cref="Props.WindowCreateExternalGraphicsContextBoolean"/>: <c>true</c> if the
    /// window will be used with an externally managed graphics context.</item>
    /// <item><see cref="Props.WindowCreateFocusableBoolean"/>: <c>true</c> if the window should
    /// accept keyboard input (defaults <c>true</c>)</item>
    /// <item><see cref="Props.WindowCreateFullscreenBoolean"/>: <c>true</c> if the window should
    /// start in fullscreen mode at desktop resolution</item>
    /// <item><see cref="Props.WindowCreateHeightNumber"/>: the height of the window</item>
    /// <item><see cref="Props.WindowCreateHiddenBoolean"/>: <c>true</c> if the window should start
    /// hidden</item>
    /// <item><see cref="Props.WindowCreateHighPixelDensityBoolean"/>: <c>true</c> if the window
    /// uses a high pixel density buffer if possible</item>
    /// <item><see cref="Props.WindowCreateMaximizedBoolean"/>: <c>true</c> if the window should
    /// start maximized</item>
    /// <item><see cref="Props.WindowCreateMenuBoolean"/>: <c>true</c> if the window is a popup menu</item>
    /// <item><see cref="Props.WindowCreateMetalBoolean"/>: <c>true</c> if the window will be used
    /// with Metal rendering</item>
    /// <item><see cref="Props.WindowCreateMinimizedBoolean"/>: <c>true</c> if the window should
    /// start minimized</item>
    /// <item><see cref="Props.WindowCreateModalBoolean"/>: <c>true</c> if the window is modal to
    /// its parent</item>
    /// <item><see cref="Props.WindowCreateMouseGrabbedBoolean"/>: <c>true</c> if the window starts
    /// with grabbed mouse focus</item>
    /// <item><see cref="Props.WindowCreateOpenGLBoolean"/>: <c>true</c> if the window will be used
    /// with OpenGL rendering</item>
    /// <item><see cref="Props.WindowCreateParentPointer"/>: an SDL_Window that will be the
    /// parent of this window, required for windows with the <c>"tooltip,"</c> <c>"menu"</c>,
    /// and <c>"modal"</c> properties</item>
    /// <item><see cref="Props.WindowCreateResizableBoolean"/>: <c>true</c> if the window should be
    /// resizable</item>
    /// <item><see cref="Props.WindowCreateTitleString"/>: the title of the window, in UTF-8
    /// encoding</item>
    /// <item><see cref="Props.WindowCreateTransparentBoolean"/>: <c>true</c> if the window show
    /// transparent in the areas with alpha of 0</item>
    /// <item><see cref="Props.WindowCreateTooltipBoolean"/>: <c>true</c> if the window is a tooltip</item>
    /// <item><see cref="Props.WindowCreateUtilityBoolean"/>: <c>true</c> if the window is a utility
    /// window, not showing in the task bar and window list</item>
    /// <item><see cref="Props.WindowCreateVulkanBoolean"/>: <c>true</c> if the window will be used
    /// with Vulkan rendering</item>
    /// <item><see cref="Props.WindowCreateWidthNumber"/>: the width of the window</item>
    /// <item><see cref="Props.WindowCreateXNumber"/>: the x position of the window, or
    /// <see cref="WindowPosCentered"/>, defaults to <see cref="WindowPosUndefined"/>. This is
    /// relative to the parent for windows with the <c>"tooltip"</c> or <c>"menu"</c> property set.</item>
    /// <item><see cref="Props.WindowCreateYNumber"/>: the y position of the window, or
    /// <see cref="WindowPosCentered"/>, defaults to <see cref="WindowPosUndefined"/>. This is
    /// relative to the parent for windows with the <c>"tooltip"</c> or <c>"menu"</c> property set.</item>
    /// </list>
    /// <para>These are additional supported properties on macOS:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateCocoaWindowPointer"/>: the
    /// <c>(__unsafe_unretained)</c> NSWindow associated with the window, if you want
    /// to wrap an existing window.</item>
    /// <item><see cref="Props.WindowCreateCocoaViewPointer"/>: the <c>(__unsafe_unretained)</c>
    /// NSView associated with the window, defaults to <c>[window contentView]</c></item>
    /// </list>
    /// <para>These are additional supported properties on iOS, tvOS, and visionOS:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateWindowScenePointer"/>: the <c>(__unsafe_unretained)</c>
    /// UIWindowScene associated with the window, defaults to the active window
    /// scene.</item>
    /// </list>
    /// <para>These are additional supported properties with visionOS:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateVisionOSSettingsString"/>: the settings of the window
    /// in JSON format. If this isn't set, the window will have standard UIKit
    /// behavior. If this is set to <c>""</c> or a valid setting string then the
    /// window is created with enhanced features allowing curved display. The
    /// curvature in the settings is defined as a radius in millimeters. A common
    /// value for a gaming monitor is 1000 and a setting string for that would be
    /// <c>"{\"curvatureRadius\":1000}"</c>.</item>
    /// </list>
    /// <para>These are additional supported properties on Wayland:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateWaylandSurfaceRoleCustomBoolean"/> - <c>true</c> if
    /// the application wants to use the Wayland surface for a custom role and
    /// does not want it attached to an XDG toplevel window. See
    /// [README-wayland](README-wayland) for more information on using custom
    /// surfaces.</item>
    /// <item><see cref="Props.WindowCreateWaylandCreateEGLWindowBoolean"/> - <c>true</c> if the
    /// application wants an associated <c>wl_egl_window</c> object to be created and
    /// attached to the window, even if the window does not have the OpenGL
    /// property or <see cref="WindowFlags.OpenGL"/> flag set.</item>
    /// <item><see cref="Props.WindowCreateWaylandWLSurfacePointer"/> - the wl_surface
    /// associated with the window, if you want to wrap an existing window. See
    /// [README-wayland](README-wayland) for more information.</item>
    /// </list>
    /// <para>These are additional supported properties on Windows:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateWin32HWNDPointer"/>: the HWND associated with the
    /// window, if you want to wrap an existing window.</item>
    /// <item><see cref="Props.WindowCreateWin32PixelFormatHWNDPointer"/>`: optional,
    /// another window to share pixel format with, useful for OpenGL windows</item>
    /// </list>
    /// <para>These are additional supported properties with X11:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateX11WindowNumber"/>: the X11 Window associated
    /// with the window, if you want to wrap an existing window.</item>
    /// </list>
    /// <para>The window is implicitly shown if the <c>"hidden"</c> property is not set.</para>
    /// <para>These are additional supported properties with Emscripten:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCreateEmscriptennCanvasIdString"/>: the id given to the canvas
    /// element. This should start with a <c>#</c> sign</item>
    /// <item><see cref="Props.WindowCreateEmscriptenKeyboardElementString"/>: override the
    /// binding element for keyboard inputs for this canvas. The variable can be
    /// one of:</item>
    /// <item><c>"#window"</c>: the javascript window object (default)</item>
    /// <item><c>"#document"</c>: the javascript document object</item>
    /// <item><c>"#screen"</c>: the javascript window.screen object</item>
    /// <item><c>"#canvas"</c>: the WebGL canvas element</item>
    /// <item><c>"#none"</c>: Don't bind anything at all</item>
    /// <item>any other string without a leading # sign applies to the element on the
    /// page with that ID. Windows with the "tooltip" and "menu" properties are
    /// popup windows and have the behaviors and guidelines outlined in
    /// <see cref="CreatePopupWindow"/>.</item>
    /// </list>
    /// <para>Windows with the <c>"tooltip"</c> and <c>"menu"</c> properties are popup windows and have
    /// the behaviors and guidelines outlined in <see cref="CreatePopupWindow"/>.</para>
    /// <para>If this window is being created to be used with an SDL_Renderer, you should
    /// not add a graphics API specific property
    /// (<see cref="Props.WindowCreateOpenGLBoolean"/>, etc), as SDL will handle that
    /// internally when it chooses a renderer. However, SDL might need to recreate
    /// your window at that point, which may cause the window to appear briefly,
    /// and then flicker as it is recreated. The correct approach to this is to
    /// create the window with the <see cref="Props.WindowCreateHiddenBoolean"/> property
    /// set to <c>true</c>, then create the renderer, then show the window with
    /// <see cref="ShowWindow"/>.</para>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>the window that was created or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProperties"/>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="DestroyWindow"/>
    public static IntPtr CreateWindowWithProperties(uint props)
    {
        return CreateWindowWithPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowID(IntPtr window);
    private delegate uint GetWindowIDNativeDelegate(IntPtr window);
    private static GetWindowIDNativeDelegate GetWindowIDNativeFunction = SDL_GetWindowID;

    /// <code>extern SDL_DECLSPEC SDL_WindowID SDLCALL SDL_GetWindowID(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the numeric ID of a window.</para>
    /// <para>The numeric ID is what <see cref="WindowEvent"/> references, and is necessary to map
    /// these events to specific SDL_Window objects.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the ID of the window on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFromID"/>
    public static uint GetWindowID(IntPtr window)
    {
        return GetWindowIDNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowFromID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowFromID(uint id);
    private delegate IntPtr GetWindowFromIDNativeDelegate(uint id);
    private static GetWindowFromIDNativeDelegate GetWindowFromIDNativeFunction = SDL_GetWindowFromID;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetWindowFromID(SDL_WindowID id);</code>
    /// <summary>
    /// <para>Get a window from a stored ID.</para>
    /// <para>The numeric ID is what <see cref="WindowEvent"/> references, and is necessary to map
    /// these events to specific SDL_Window objects.</para>
    /// </summary>
    /// <param name="id">the ID of the window.</param>
    /// <returns>the window associated with <c>id</c> or <c>null</c> if it doesn't exist; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowID"/>
    public static IntPtr GetWindowFromID(uint id)
    {
        return GetWindowFromIDNativeFunction(id);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowParent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowParent(IntPtr window);
    private delegate IntPtr GetWindowParentNativeDelegate(IntPtr window);
    private static GetWindowParentNativeDelegate GetWindowParentNativeFunction = SDL_GetWindowParent;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetWindowParent(SDL_Window *window);</code>
    /// <summary>
    /// Get parent of a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the parent of the window on success or <c>null</c> if the window has no
    /// parent.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreatePopupWindow"/>
    public static IntPtr GetWindowParent(IntPtr window)
    {
        return GetWindowParentNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowProperties(IntPtr window);
    private delegate uint GetWindowPropertiesNativeDelegate(IntPtr window);
    private static GetWindowPropertiesNativeDelegate GetWindowPropertiesNativeFunction = SDL_GetWindowProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetWindowProperties(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the properties associated with a window.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowShapePointer"/>: the surface associated with a shaped
    /// window</item>
    /// <item><see cref="Props.WindowHDREnabledBoolean"/>: <c>true</c> if the window has HDR
    /// headroom above the SDR white point. This property can change dynamically
    /// when <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// <item><see cref="Props.WindowSDRWhiteLevelFloat"/>: the value of SDR white in the
    /// <see cref="Colorspace.SRGBLinear"/> colorspace. On Windows this corresponds to the
    /// SDR white level in scRGB colorspace, and on Apple platforms this is
    /// always 1.0 for EDR content. This property can change dynamically when
    /// <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// <item><see cref="Props.WindowHDRHeadroomFloat"/>: the additional high dynamic range
    /// that can be displayed, in terms of the SDR white point. When HDR is not
    /// enabled, this will be 1.0. This property can change dynamically when
    /// <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// </list>
    /// <para>On Android:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowAndroidWindowPointer"/>: the ANativeWindow associated
    /// with the window</item>
    /// <item><see cref="Props.WindowAndroidSurfacePointer"/>: the EGLSurface associated with
    /// the window</item>
    /// </list>
    /// <para>On iOS:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowUIKitWindowPointer"/>: the <c>(__unsafe_unretained)</c>
    /// UIWindow associated with the window</item>
    /// <item><see cref="Props.WindowUIKitMetalViewTagNumber"/>: the NSInteger tag
    /// associated with metal views on the window</item>
    /// <item><see cref="Props.WindowUIKitOpenglFramebufferNumber"/>: the OpenGL view's
    /// framebuffer object. It must be bound when rendering to the screen using
    /// OpenGL.</item>
    /// <item><see cref="Props.WindowUIKitOpenglRenderbufferNumber"/>: the OpenGL view's
    /// renderbuffer object. It must be bound when <see cref="GLSwapWindow"/> is called.</item>
    /// <item><see cref="Props.WindowUIKitOpenglResolveFramebufferNumber"/>: the OpenGL
    /// view's resolve framebuffer, when MSAA is used.</item>
    /// </list>
    /// <para>On visionOS:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowVisionOSSettingsString"/>: the current settings of the
    /// window in JSON format, or <c>null</c> if the window has standard UIKit
    /// behavior. <see cref="EventType.WindowSettingsChanged"/> is sent when this
    /// value changes.</item>
    /// </list>
    /// <para>On KMS/DRM:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowKMSDRMDeviceIndexNumber"/>: the device index associated
    /// with the window (e.g. the X in /dev/dri/cardX)</item>
    /// <item><see cref="Props.WindowKMSDRMDRMFDNumber"/>: the DRM FD associated with the
    /// window</item>
    /// <item><see cref="Props.WindowKMSDRMGBMDevicePointer"/>: the GBM device associated
    /// with the window</item>
    /// </list>
    /// <para>On macOS:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowCocoaWindowPointer"/>: the <c>(__unsafe_unretained)</c>
    /// NSWindow associated with the window</item>
    /// <item><see cref="Props.WindowCocoaMetalViewTagNumber"/>: the NSInteger tag
    /// associated with metal views on the window</item>
    /// </list>
    /// <para>On OpenVR:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowOpenVROverlayIdNumber"/>: the OpenVR Overlay Handle ID for the
    /// associated overlay window.</item>
    /// </list>
    /// <para>On QNX:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowQNXWindowPointer"/>: the screen_window_t associated with
    /// the window.</item>
    /// <item><see cref="Props.WindowQNXSurfacePointer"/>: the EGLSurface associated with the
    /// window</item>
    /// </list>
    /// <para>On Vivante:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowVivanteDisplayPointer"/>: the EGLNativeDisplayType
    /// associated with the window</item>
    /// <item><see cref="Props.WindowVivanteWindowPointer"/>: the EGLNativeWindowType
    /// associated with the window</item>
    /// <item><see cref="Props.WindowVivanteSurfacePointer"/>: the EGLSurface associated with
    /// the window</item>
    /// </list>
    /// <para>On Windows:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowWin32HWNDPointer"/>: the HWND associated with the window</item>
    /// <item><see cref="Props.WindowWin32HDCPointer"/>: the HDC associated with the window</item>
    /// <item><see cref="Props.WindowWin32InstancePointer"/>: the HINSTANCE associated with
    /// the window</item>
    /// </list>
    /// <para>On Wayland:</para>
    /// <para>Note: The <c>xdg_*</c> window objects do not internally persist across window
    /// show/hide calls. They will be <c>null</c> if the window is hidden and must be
    /// queried each time it is shown.</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowWaylandDisplayPointer"/>: the wl_display associated with
    /// the window</item>
    /// <item><see cref="Props.WindowWaylandSurfacePointer"/>: the wl_surface associated with
    /// the window</item>
    /// <item><see cref="Props.WindowWaylandViewportPointer"/>: the wp_viewport associated
    /// with the window</item>
    /// <item><see cref="Props.WindowWaylandEGLWindowPointer"/>: the wl_egl_window
    /// associated with the window</item>
    /// <item><see cref="Props.WindowWaylandXDGSurfacePointer"/>: the xdg_surface associated
    /// with the window</item>
    /// <item><see cref="Props.WindowWaylandXDGToplevelPointer"/>: the xdg_toplevel role
    /// associated with the window</item>
    /// <item><see cref="Props.WindowWaylandXDGToplevelExportHandleString"/>: the export
    /// handle associated with the window</item>
    /// <item><see cref="Props.WindowWaylandXDGPopupPointer"/>: the xdg_popup role
    /// associated with the window</item>
    /// <item><see cref="Props.WindowWaylandXDGPositionerPointer"/>: the xdg_positioner
    /// associated with the window, in popup mode</item>
    /// </list>
    /// <para>On X11:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowX11DisplayPointer"/>: the X11 Display associated with
    /// the window</item>
    /// <item><see cref="Props.WindowX11ScreenNumber"/>: the screen number associated with
    /// the window</item>
    /// <item><see cref="Props.WindowX11WindowNumber"/>: the X11 Window associated with the
    /// window</item>
    /// </list>
    /// <para>On Emscripten:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.WindowEMScriptenCanvasIdString"/>: the id the canvas element
    /// will have</item>
    /// <item><see cref="Props.WindowEMScriptenKeyboardElementString"/>: the keyboard
    /// element that associates keyboard events to this window</item>
    /// </list>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a valid property ID on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GetWindowProperties(IntPtr window)
    {
        return GetWindowPropertiesNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowFlags"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial WindowFlags SDL_GetWindowFlags(IntPtr window);
    private delegate WindowFlags GetWindowFlagsNativeDelegate(IntPtr window);
    private static GetWindowFlagsNativeDelegate GetWindowFlagsNativeFunction = SDL_GetWindowFlags;

    /// <code>extern SDL_DECLSPEC SDL_WindowFlags SDLCALL SDL_GetWindowFlags(SDL_Window *window);</code>
    /// <summary>
    /// Get the window flags.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a mask of the <see cref="WindowFlags"/> associated with <c>window</c>.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="HideWindow"/>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="SetWindowFillDocument"/>
    /// <seealso cref="ShowWindow"/>
    public static WindowFlags GetWindowFlags(IntPtr window)
    {
        return GetWindowFlagsNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowTitle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowTitle(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string title);
    private delegate bool SetWindowTitleNativeDelegate(IntPtr window, string title);
    private static SetWindowTitleNativeDelegate SetWindowTitleNativeFunction = SDL_SetWindowTitle;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowTitle(SDL_Window *window, const char *title);</code>
    /// <summary>
    /// <para>Set the title of a window.</para>
    /// <para>This string is expected to be in UTF-8 encoding.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="title">the desired window title in UTF-8 format.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowTitle"/>
    public static bool SetWindowTitle(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string title)
    {
        return SetWindowTitleNativeFunction(window, title);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowTitle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowTitle(IntPtr window);
    private delegate IntPtr GetWindowTitleNativeDelegate(IntPtr window);
    private static GetWindowTitleNativeDelegate GetWindowTitleNativeFunction = SDL_GetWindowTitle;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetWindowTitle(SDL_Window *window);</code>
    /// <summary>
    /// Get the title of a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the title of the window in UTF-8 format or "" if there is no
    /// title.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowTitle"/>
    public static string GetWindowTitle(IntPtr window)
    {
        var value = GetWindowTitleNativeFunction(window);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowIcon"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowIcon(IntPtr window, IntPtr icon);
    private delegate bool SetWindowIconNativeDelegate(IntPtr window, IntPtr icon);
    private static SetWindowIconNativeDelegate SetWindowIconNativeFunction = SDL_SetWindowIcon;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowIcon(SDL_Window *window, SDL_Surface *icon);</code>
    /// <summary>
    /// <para>Set the icon for a window.</para>
    /// <para>If this function is passed a surface with alternate representations added
    /// using <see cref="AddSurfaceAlternateImage"/>, the surface will be interpreted as
    /// the content to be used for 100% display scale, and the alternate
    /// representations will be used for high DPI situations. For example, if the
    /// original surface is 32x32, then on a 2x macOS display or 200% display scale
    /// on Windows, a 64x64 version of the image will be used, if available. If a
    /// matching version of the image isn't available, the closest larger size
    /// image will be downscaled to the appropriate size and be used instead, if
    /// available. Otherwise, the closest smaller image will be upscaled and be
    /// used instead.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="icon">an <see cref="Surface"/> structure containing the icon for the window.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    public static bool SetWindowIcon(IntPtr window, IntPtr icon)
    {
        return SetWindowIconNativeFunction(window, icon);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowPosition(IntPtr window, int x, int y);
    private delegate bool SetWindowPositionNativeDelegate(IntPtr window, int x, int y);
    private static SetWindowPositionNativeDelegate SetWindowPositionNativeFunction = SDL_SetWindowPosition;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowPosition(SDL_Window *window, int x, int y);</code>
    /// <summary>
    /// <para>Request that the window's position be set.</para>
    /// <para>If the window is in an exclusive fullscreen or maximized state, this
    /// request has no effect.</para>
    /// <para>This can be used to reposition fullscreen-desktop windows onto a different
    /// display, however, as exclusive fullscreen windows are locked to a specific
    /// display, they can only be repositioned programmatically via
    /// <see cref="SetWindowFullscreenMode(nint, nint)"/>.</para>
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
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowPosition"/>
    /// <seealso cref="SyncWindow"/>
    public static bool SetWindowPosition(IntPtr window, int x, int y)
    {
        return SetWindowPositionNativeFunction(window, x, y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowPosition(IntPtr window, out int x, out int y);
    private delegate bool GetWindowPositionNativeDelegate(IntPtr window, out int x, out int y);
    private static GetWindowPositionNativeDelegate GetWindowPositionNativeFunction = SDL_GetWindowPosition;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowPosition(SDL_Window *window, int *x, int *y);</code>
    /// <summary>
    /// <para>Get the position of a window.</para>
    /// <para>This is the current position of the window as last reported by the
    /// windowing system.</para>
    /// <para>If you do not need the value for one of the positions a <c>null</c> may be passed
    /// in the <c>x</c> or <c>y</c> parameter.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="x">a pointer filled in with the x position of the window, may be
    /// <c>null</c>.</param>
    /// <param name="y">a pointer filled in with the y position of the window, may be
    /// <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    public static bool GetWindowPosition(IntPtr window, out int x, out int y)
    {
        return GetWindowPositionNativeFunction(window, out x, out y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowSize(IntPtr window, int w, int h);
    private delegate bool SetWindowSizeNativeDelegate(IntPtr window, int w, int h);
    private static SetWindowSizeNativeDelegate SetWindowSizeNativeFunction = SDL_SetWindowSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowSize(SDL_Window *window, int w, int h);</code>
    /// <summary>
    /// <para>Request that the size of a window's client area be set.</para>
    /// <para>If the window is in a fullscreen or maximized state, this request has no
    /// effect.</para>
    /// <para>To change the exclusive fullscreen mode of a window, use
    /// <see cref="SetWindowFullscreenMode(nint, nint)"/></para>
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
    /// <param name="w">the width of the window, must be > 0.</param>
    /// <param name="h">the height of the window, must be > 0.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSize"/>
    /// <seealso cref="SetWindowFullscreenMode(nint, nint)"/>
    /// <seealso cref="SyncWindow"/>
    public static bool SetWindowSize(IntPtr window, int w, int h)
    {
        return SetWindowSizeNativeFunction(window, w, h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowSize(IntPtr window, out int w, out int h);
    private delegate bool GetWindowSizeNativeDelegate(IntPtr window, out int w, out int h);
    private static GetWindowSizeNativeDelegate GetWindowSizeNativeFunction = SDL_GetWindowSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowSize(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// <para>Get the size of a window's client area.</para>
    /// <para>The window pixel size may differ from its window coordinate size if the
    /// window is on a high pixel density display. Use <see cref="GetWindowSizeInPixels"/>
    /// or <see cref="GetRenderOutputSize"/> to get the real client area size in pixels.</para>
    /// </summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <param name="w">a pointer filled in with the width of the window, may be <c>null</c></param>
    /// <param name="h">a pointer filled in with the height of the window, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderOutputSize"/>
    /// <seealso cref="GetWindowSizeInPixels"/>
    /// <seealso cref="SetWindowSize"/>
    /// <seealso cref="EventType.WindowResized"/>
    public static bool GetWindowSize(IntPtr window, out int w, out int h)
    {
        return GetWindowSizeNativeFunction(window, out w, out h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowSafeArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowSafeArea(IntPtr window, out Rect rect);
    private delegate bool GetWindowSafeAreaNativeDelegate(IntPtr window, out Rect rect);
    private static GetWindowSafeAreaNativeDelegate GetWindowSafeAreaNativeFunction = SDL_GetWindowSafeArea;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowSafeArea(SDL_Window *window, SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Get the safe area for this window.</para>
    /// <para>Some devices have portions of the screen which are partially obscured or
    /// not interactive, possibly due to on-screen controls, curved edges, camera
    /// notches, TV overscan, etc. This function provides the area of the window
    /// which is safe to have interactable content. You should continue rendering
    /// into the rest of the window, but it should not contain visually important
    /// or interactable content.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="rect">a pointer filled in with the client area that is safe for
    /// interactive content.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool GetWindowSafeArea(IntPtr window, out Rect rect)
    {
        return GetWindowSafeAreaNativeFunction(window, out rect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowAspectRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowAspectRatio(IntPtr window, float minAspect, float maxAspect);
    private delegate bool SetWindowAspectRatioNativeDelegate(IntPtr window, float minAspect, float maxAspect);
    private static SetWindowAspectRatioNativeDelegate SetWindowAspectRatioNativeFunction = SDL_SetWindowAspectRatio;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowAspectRatio(SDL_Window *window, float min_aspect, float max_aspect);</code>
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
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowAspectRatio"/>
    /// <seealso cref="SyncWindow"/>
    public static bool SetWindowAspectRatio(IntPtr window, float minAspect, float maxAspect)
    {
        return SetWindowAspectRatioNativeFunction(window, minAspect, maxAspect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowAspectRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowAspectRatio(IntPtr window, out float minAspect, out float maxAspect);
    private delegate bool GetWindowAspectRatioNativeDelegate(IntPtr window, out float minAspect, out float maxAspect);
    private static GetWindowAspectRatioNativeDelegate GetWindowAspectRatioNativeFunction = SDL_GetWindowAspectRatio;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowAspectRatio(SDL_Window *window, float *min_aspect, float *max_aspect);</code>
    /// <summary>
    /// Get the aspect ratio of a window's client area.
    /// </summary>
    /// <param name="window">the window to query the width and height from.</param>
    /// <param name="minAspect">a pointer filled in with the minimum aspect ratio of the
    /// window, may be <c>null</c>.</param>
    /// <param name="maxAspect">a pointer filled in with the maximum aspect ratio of the
    /// window, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowAspectRatio"/>
    public static bool GetWindowAspectRatio(IntPtr window, out float minAspect, out float maxAspect)
    {
        return GetWindowAspectRatioNativeFunction(window, out minAspect, out maxAspect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowBordersSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, out int right);
    private delegate bool GetWindowBordersSizeNativeDelegate(IntPtr window, out int top, out int left, out int bottom, out int right);
    private static GetWindowBordersSizeNativeDelegate GetWindowBordersSizeNativeFunction = SDL_GetWindowBordersSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowBordersSize(SDL_Window *window, int *top, int *left, int *bottom, int *right);</code>
    /// <summary>
    /// <para>Get the size of a window's borders (decorations) around the client area.</para>
    /// <para>Note: If this function fails (returns <c>false</c>), the size values will be
    /// initialized to 0, 0, 0, 0 (if a non-<c>null</c> pointer is provided), as if the
    /// window in question was borderless.</para>
    /// <para>Note: This function may fail on systems where the window has not yet been
    /// decorated by the display server (for example, immediately after calling
    /// <see cref="CreateWindow"/>). It is recommended that you wait at least until the
    /// window has been presented and composited, so that the window system has a
    /// chance to decorate the window and provide the border dimensions to SDL.</para>
    /// <para>This function also returns <c>false</c> if getting the information is not
    /// supported.</para>
    /// </summary>
    /// <param name="window">the window to query the size values of the border
    /// (decorations) from.</param>
    /// <param name="top">pointer to variable for storing the size of the top border; <c>null</c>
    /// is permitted.</param>
    /// <param name="left">pointer to variable for storing the size of the left border;
    /// <c>null</c> is permitted.</param>
    /// <param name="bottom">pointer to variable for storing the size of the bottom
    /// border; <c>null</c> is permitted.</param>
    /// <param name="right">pointer to variable for storing the size of the right border;
    /// <c>null</c> is permitted.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSize"/>
    public static bool GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, out int right)
    {
        return GetWindowBordersSizeNativeFunction(window, out top, out left, out bottom, out right);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowSizeInPixels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowSizeInPixels(IntPtr window, out int w, out int h);
    private delegate bool GetWindowSizeInPixelsNativeDelegate(IntPtr window, out int w, out int h);
    private static GetWindowSizeInPixelsNativeDelegate GetWindowSizeInPixelsNativeFunction = SDL_GetWindowSizeInPixels;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowSizeInPixels(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// Get the size of a window's client area, in pixels.
    /// </summary>
    /// <param name="window">the window from which the drawable size should be queried.</param>
    /// <param name="w">a pointer to variable for storing the width in pixels, may be
    /// <c>null</c>.</param>
    /// <param name="h">a pointer to variable for storing the height in pixels, may be
    /// <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="GetWindowSize"/>
    public static bool GetWindowSizeInPixels(IntPtr window, out int w, out int h)
    {
        return GetWindowSizeInPixelsNativeFunction(window, out w, out h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowMinimumSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMinimumSize(IntPtr window, int minW, int minH);
    private delegate bool SetWindowMinimumSizeNativeDelegate(IntPtr window, int minW, int minH);
    private static SetWindowMinimumSizeNativeDelegate SetWindowMinimumSizeNativeFunction = SDL_SetWindowMinimumSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowMinimumSize(SDL_Window *window, int min_w, int min_h);</code>
    /// <summary>
    /// Set the minimum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="minW">the minimum width of the window, or 0 for no limit.</param>
    /// <param name="minH">the minimum height of the window, or 0 for no limit.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    public static bool SetWindowMinimumSize(IntPtr window, int minW, int minH)
    {
        return SetWindowMinimumSizeNativeFunction(window, minW, minH);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowMinimumSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowMinimumSize(IntPtr window, out int w, out int h);
    private delegate bool GetWindowMinimumSizeNativeDelegate(IntPtr window, out int w, out int h);
    private static GetWindowMinimumSizeNativeDelegate GetWindowMinimumSizeNativeFunction = SDL_GetWindowMinimumSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowMinimumSize(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// Get the minimum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="w">a pointer filled in with the minimum width of the window, may be
    /// <c>null</c>.</param>
    /// <param name="h">a pointer filled in with the minimum height of the window, may be
    /// <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    public static bool GetWindowMinimumSize(IntPtr window, out int w, out int h)
    {
        return GetWindowMinimumSizeNativeFunction(window, out w, out h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowMaximumSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMaximumSize(IntPtr window, int maxWidth, int maxHeight);
    private delegate bool SetWindowMaximumSizeNativeDelegate(IntPtr window, int maxWidth, int maxHeight);
    private static SetWindowMaximumSizeNativeDelegate SetWindowMaximumSizeNativeFunction = SDL_SetWindowMaximumSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowMaximumSize(SDL_Window *window, int max_w, int max_h);</code>
    /// <summary>
    /// Set the maximum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="maxWidth">the maximum width of the window, or 0 for no limit.</param>
    /// <param name="maxHeight">the maximum height of the window, or 0 for no limit.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMaximumSize"/>
    /// <seealso cref="SetWindowMinimumSize"/>
    public static bool SetWindowMaximumSize(IntPtr window, int maxWidth, int maxHeight)
    {
        return SetWindowMaximumSizeNativeFunction(window, maxWidth, maxHeight);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowMaximumSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowMaximumSize(IntPtr window, out int w, out int h);
    private delegate bool GetWindowMaximumSizeNativeDelegate(IntPtr window, out int w, out int h);
    private static GetWindowMaximumSizeNativeDelegate GetWindowMaximumSizeNativeFunction = SDL_GetWindowMaximumSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowMaximumSize(SDL_Window *window, int *w, int *h);</code>
    /// <summary>
    /// Get the maximum size of a window's client area.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="w">a pointer filled in with the maximum width of the window, may be
    /// <c>null</c>.</param>
    /// <param name="h">a pointer filled in with the maximum height of the window, may be
    /// <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMinimumSize"/>
    /// <seealso cref="SetWindowMaximumSize"/>
    public static bool GetWindowMaximumSize(IntPtr window, out int w, out int h)
    {
        return GetWindowMaximumSizeNativeFunction(window, out w, out h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowBordered"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowBordered(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool bordered);
    private delegate bool SetWindowBorderedNativeDelegate(IntPtr window, bool bordered);
    private static SetWindowBorderedNativeDelegate SetWindowBorderedNativeFunction = SDL_SetWindowBordered;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowBordered(SDL_Window *window, bool bordered);</code>
    /// <summary>
    /// <para>Set the border state of a window.</para>
    /// <para>This will add or remove the window's <see cref="WindowFlags.Borderless"/> flag and add
    /// or remove the border from the actual window. This is a no-op if the
    /// window's border already matches the requested state.</para>
    /// <para>You can't change the border state of a fullscreen window.</para>
    /// </summary>
    /// <param name="window">the window of which to change the border state.</param>
    /// <param name="bordered"><c>false</c> to remove border, <c>true</c> to add border.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFlags"/>
    public static bool SetWindowBordered(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool bordered)
    {
        return SetWindowBorderedNativeFunction(window, bordered);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowResizable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowResizable(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool resizable);
    private delegate bool SetWindowResizableNativeDelegate(IntPtr window, bool resizable);
    private static SetWindowResizableNativeDelegate SetWindowResizableNativeFunction = SDL_SetWindowResizable;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowResizable(SDL_Window *window, bool resizable);</code>
    /// <summary>
    /// <para>Set the user-resizable state of a window.</para>
    /// <para>This will add or remove the window's <see cref="WindowFlags.Resizable"/> flag and
    /// allow/disallow user resizing of the window. This is a no-op if the window's
    /// resizable state already matches the requested state.</para>
    /// <para>You can't change the resizable state of a fullscreen window.</para>
    /// </summary>
    /// <param name="window">the window of which to change the resizable state.</param>
    /// <param name="resizable"><c>true</c> to allow resizing, <c>false</c> to disallow.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFlags"/>
    public static bool SetWindowResizable(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool resizable)
    {
        return SetWindowResizableNativeFunction(window, resizable);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowAlwaysOnTop"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowAlwaysOnTop(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool onTop);
    private delegate bool SetWindowAlwaysOnTopNativeDelegate(IntPtr window, bool onTop);
    private static SetWindowAlwaysOnTopNativeDelegate SetWindowAlwaysOnTopNativeFunction = SDL_SetWindowAlwaysOnTop;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowAlwaysOnTop(SDL_Window *window, bool on_top);</code>
    /// <summary>
    /// <para>Set the window to always be above the others.</para>
    /// <para>This will add or remove the window's <see cref="WindowFlags.AlwaysOnTop"/> flag. This
    /// will bring the window to the front and keep the window above the rest.</para>
    /// </summary>
    /// <param name="window">the window of which to change the always on top state.</param>
    /// <param name="onTop"><c>true</c> to set the window always on top, <c>false</c> to disable.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFlags"/>
    public static bool SetWindowAlwaysOnTop(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool onTop)
    {
        return SetWindowAlwaysOnTopNativeFunction(window, onTop);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowFillDocument"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowFillDocument(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool fill);
    private delegate bool SetWindowFillDocumentNativeDelegate(IntPtr window, bool fill);
    private static SetWindowFillDocumentNativeDelegate SetWindowFillDocumentNativeFunction = SDL_SetWindowFillDocument;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowFillDocument(SDL_Window *window, bool fill);</code>
    /// <summary>
    /// <para>Set the window to fill the current document space (Emscripten only).</para>
    /// <para>This will add or remove the window's <see cref="WindowFlags.WindowFillDocument"/> flag.</para>
    /// <para>Currently this flag only applies to the Emscripten target.</para>
    /// <para>When enabled, the canvas element fills the entire document. Resize events
    /// will be generated as the browser window is resized, as that will adjust the
    /// canvas size as well. The canvas will cover anything else on the page,
    /// including any controls provided by Emscripten in its generated HTML file
    /// (in fact, any elements on the page that aren't the canvas will be moved
    /// into a hidden <c>div</c> element).</para>
    /// /// <para>Often times this is desirable for a browser-based game, but it means
    /// several things that we expect of an SDL window on other platforms might not
    /// work as expected, such as minimum window sizes and aspect ratios.</para>
    /// </summary>
    /// <param name="window">the window of which to change the fill-document state.</param>
    /// <param name="fill"><c>true</c> to set the window to fill the document, <c>false</c> to disable.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static bool SetWindowFillDocument(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool fill)
    {
        return SetWindowFillDocumentNativeFunction(window, fill);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ShowWindow(IntPtr window);
    private delegate bool ShowWindowNativeDelegate(IntPtr window);
    private static ShowWindowNativeDelegate ShowWindowNativeFunction = SDL_ShowWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShowWindow(SDL_Window *window);</code>
    /// <summary>
    /// Show a window.
    /// </summary>
    /// <param name="window">the window to show.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HideWindow"/>
    /// <seealso cref="RaiseWindow"/>
    public static bool ShowWindow(IntPtr window)
    {
        return ShowWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HideWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HideWindow(IntPtr window);
    private delegate bool HideWindowNativeDelegate(IntPtr window);
    private static HideWindowNativeDelegate HideWindowNativeFunction = SDL_HideWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HideWindow(SDL_Window *window);</code>
    /// <summary>
    /// Hide a window.
    /// </summary>
    /// <param name="window">the window to hide.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ShowWindow"/>
    /// <seealso cref="WindowFlags.Hidden"/>
    public static bool HideWindow(IntPtr window)
    {
        return HideWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RaiseWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RaiseWindow(IntPtr window);
    private delegate bool RaiseWindowNativeDelegate(IntPtr window);
    private static RaiseWindowNativeDelegate RaiseWindowNativeFunction = SDL_RaiseWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RaiseWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Request that a window be raised above other windows and gain the input
    /// focus.</para>
    /// <para>The result of this request is subject to desktop window manager policy,
    /// particularly if raising the requested window would result in stealing focus
    /// from another application. If the window is successfully raised and gains
    /// input focus, an <see cref="EventType.WindowFocusGained"/> event will be emitted, and
    /// the window will have the <see cref="WindowFlags.InputFocus"/> flag set.</para>
    /// </summary>
    /// <param name="window">the window to raise.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool RaiseWindow(IntPtr window)
    {
        return RaiseWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MaximizeWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_MaximizeWindow(IntPtr window);
    private delegate bool MaximizeWindowNativeDelegate(IntPtr window);
    private static MaximizeWindowNativeDelegate MaximizeWindowNativeFunction = SDL_MaximizeWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_MaximizeWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Request that the window be made as large as possible.</para>
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
    /// <param name="window">the window to maximize.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="SyncWindow"/>
    public static bool MaximizeWindow(IntPtr window)
    {
        return MaximizeWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MinimizeWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_MinimizeWindow(IntPtr window);
    private delegate bool MinimizeWindowNativeDelegate(IntPtr window);
    private static MinimizeWindowNativeDelegate MinimizeWindowNativeFunction = SDL_MinimizeWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_MinimizeWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Request that the window be minimized to an iconic representation.</para>
    /// <para>If the window is in a fullscreen state, this request has no direct effect.
    /// It may alter the state the window is returned to when leaving fullscreen.</para>
    /// <para>On some windowing systems this request is asynchronous and the new window
    /// state may not have been applied immediately upon the return of this
    /// function. If an immediate change is required, call <see cref="SyncWindow"/> to
    /// block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowMinimized"/> event will be
    /// emitted. Note that, as this is just a request, the windowing system can
    /// deny the state change.</para>
    /// </summary>
    /// <param name="window">the window to minimize.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="SyncWindow"/>
    public static bool MinimizeWindow(IntPtr window)
    {
        return MinimizeWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RestoreWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RestoreWindow(IntPtr window);
    private delegate bool RestoreWindowNativeDelegate(IntPtr window);
    private static RestoreWindowNativeDelegate RestoreWindowNativeFunction = SDL_RestoreWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RestoreWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Request that the size and position of a minimized or maximized window be
    /// restored.</para>
    /// <para>If the window is in a fullscreen state, this request has no direct effect.
    /// It may alter the state the window is returned to when leaving fullscreen.</para>
    /// <para>On some windowing systems this request is asynchronous and the new window
    /// state may not have have been applied immediately upon the return of this
    /// function. If an immediate change is required, call SDL_SyncWindow() to
    /// block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowRestored"/> event will be
    /// emitted. Note that, as this is just a request, the windowing system can
    /// deny the state change.</para>
    /// </summary>
    /// <param name="window">the window to restore.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="SyncWindow"/>
    public static bool RestoreWindow(IntPtr window)
    {
        return RestoreWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowFullscreen"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowFullscreen(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool fullscreen);
    private delegate bool SetWindowFullscreenNativeDelegate(IntPtr window, bool fullscreen);
    private static SetWindowFullscreenNativeDelegate SetWindowFullscreenNativeFunction = SDL_SetWindowFullscreen;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowFullscreen(SDL_Window *window, bool fullscreen);</code>
    /// <summary>
    /// <para>Request that the window's fullscreen state be changed.</para>
    /// <para>By default a window in fullscreen state uses borderless fullscreen desktop
    /// mode, but a specific exclusive display mode can be set using
    /// <see cref="SetWindowFullscreenMode(nint, nint)"/>.</para>
    /// <para>On some windowing systems this request is asynchronous and the new
    /// fullscreen state may not have have been applied immediately upon the return
    /// of this function. If an immediate change is required, call <see cref="SyncWindow"/>
    /// to block until the changes have taken effect.</para>
    /// <para>When the window state changes, an <see cref="EventType.WindowEnterFullscreen"/> or
    /// <see cref="EventType.WindowLeaveFullscreen"/> event will be emitted. Note that, as this
    /// is just a request, it can be denied by the windowing system.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="fullscreen"><c>true</c> for fullscreen mode, <c>false</c> for windowed mode.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowFullscreenMode"/>
    /// <seealso cref="SetWindowFullscreenMode(nint, nint)"/>
    /// <seealso cref="SyncWindow"/>
    /// <seealso cref="WindowFlags.Fullscreen"/>
    public static bool SetWindowFullscreen(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool fullscreen)
    {
        return SetWindowFullscreenNativeFunction(window, fullscreen);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SyncWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SyncWindow(IntPtr window);
    private delegate bool SyncWindowNativeDelegate(IntPtr window);
    private static SyncWindowNativeDelegate SyncWindowNativeFunction = SDL_SyncWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SyncWindow(SDL_Window *window);</code>
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
    /// <returns><c>true</c> on success or <c>false</c> if the operation timed out before the
    /// window was in the requested state.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowSize"/>
    /// <seealso cref="SetWindowPosition"/>
    /// <seealso cref="SetWindowFullscreen"/>
    /// <seealso cref="MinimizeWindow"/>
    /// <seealso cref="MaximizeWindow"/>
    /// <seealso cref="RestoreWindow"/>
    /// <seealso cref="Hints.VideoSyncWindowOperations"/>
    public static bool SyncWindow(IntPtr window)
    {
        return SyncWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WindowHasSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WindowHasSurface(IntPtr window);
    private delegate bool WindowHasSurfaceNativeDelegate(IntPtr window);
    private static WindowHasSurfaceNativeDelegate WindowHasSurfaceNativeFunction = SDL_WindowHasSurface;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WindowHasSurface(SDL_Window *window);</code>
    /// <summary>
    /// Return whether the window has a surface associated with it.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns><c>true</c> if there is a surface associated with the window, or <c>false</c>
    /// otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSurface"/>
    public static bool WindowHasSurface(IntPtr window)
    {
        return WindowHasSurfaceNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowSurface(IntPtr window);
    private delegate IntPtr GetWindowSurfaceNativeDelegate(IntPtr window);
    private static GetWindowSurfaceNativeDelegate GetWindowSurfaceNativeFunction = SDL_GetWindowSurface;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_GetWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the SDL surface associated with the window.</para>
    /// <para>A new surface will be created with the optimal format for the window, if
    /// necessary. This surface will be freed when the window is destroyed. Do not
    /// free this surface.</para>
    /// <para>This surface will be invalidated if the window is resized. After resizing a
    /// window this function must be called again to return a valid surface.</para>
    /// <para>You may not combine this with 3D or the rendering API on this window.</para>
    /// <para>This function is affected by <see cref="Hints.FramebufferAcceleration"/>.</para>
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the surface associated with the window, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyWindowSurface"/>
    /// <seealso cref="WindowHasSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    /// <seealso cref="UpdateWindowSurfaceRects(nint, Rect[], int)"/>
    public static IntPtr GetWindowSurface(IntPtr window)
    {
        return GetWindowSurfaceNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowSurfaceVSync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowSurfaceVSync(IntPtr window, int vsync);
    private delegate bool SetWindowSurfaceVSyncNativeDelegate(IntPtr window, int vsync);
    private static SetWindowSurfaceVSyncNativeDelegate SetWindowSurfaceVSyncNativeFunction = SDL_SetWindowSurfaceVSync;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowSurfaceVSync(SDL_Window *window, int vsync);</code>
    /// <summary>
    /// <para>Toggle VSync for the window surface.</para>
    /// <para>When a window surface is created, vsync defaults to
    /// <see cref="WindowSurfaceVSyncDisabled"/></para>
    /// <para>The <c>vsync</c> parameter can be <c>1</c> to synchronize present with every vertical
    /// refresh, <c>2</c> to synchronize present with every second vertical refresh, etc.,
    /// <see cref="WindowSurfaceVSyncAdaptive"/> for late swap tearing (adaptive vsync),
    /// or <see cref="WindowSurfaceVSyncDisabled"/> to disable. Not every value is
    /// supported by every driver, so you should check the return value to see
    /// whether the requested setting is supported.</para>
    /// </summary>
    /// <param name="window">the window.</param>
    /// <param name="vsync">the vertical refresh sync interval.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSurfaceVSync"/>
    public static bool SetWindowSurfaceVSync(IntPtr window, int vsync)
    {
        return SetWindowSurfaceVSyncNativeFunction(window, vsync);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowSurfaceVSync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowSurfaceVSync(IntPtr window, out int vsync);
    private delegate bool GetWindowSurfaceVSyncNativeDelegate(IntPtr window, out int vsync);
    private static GetWindowSurfaceVSyncNativeDelegate GetWindowSurfaceVSyncNativeFunction = SDL_GetWindowSurfaceVSync;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowSurfaceVSync(SDL_Window *window, int *vsync);</code>
    /// <summary>
    /// Get VSync for the window surface.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <param name="vsync">an int filled with the current vertical refresh sync interval.
    /// See <see cref="SetWindowSurfaceVSync"/> for the meaning of the value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowSurfaceVSync"/>
    public static bool GetWindowSurfaceVSync(IntPtr window, out int vsync)
    {
        return GetWindowSurfaceVSyncNativeFunction(window, out vsync);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateWindowSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_UpdateWindowSurface(IntPtr window);
    private delegate bool UpdateWindowSurfaceNativeDelegate(IntPtr window);
    private static UpdateWindowSurfaceNativeDelegate UpdateWindowSurfaceNativeFunction = SDL_UpdateWindowSurface;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// <para>Copy the window surface to the screen.</para>
    /// <para>This is the function you use to reflect any changes to the surface on the
    /// screen.</para>
    /// <para>This function is equivalent to the SDL 1.2 API SDL_Flip().</para>
    /// </summary>
    /// <param name="window">the window to update.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurfaceRects(nint, Rect[], int)"/>
    public static bool UpdateWindowSurface(IntPtr window)
    {
        return UpdateWindowSurfaceNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateWindowSurfaceRects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_UpdateWindowSurfaceRects(IntPtr window, Rect[] rects, int numrects);
    private delegate bool UpdateWindowSurfaceRectsNativeDelegate(IntPtr window, Rect[] rects, int numrects);
    private static UpdateWindowSurfaceRectsNativeDelegate UpdateWindowSurfaceRectsNativeFunction = SDL_UpdateWindowSurfaceRects;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateWindowSurfaceRects(SDL_Window *window, const SDL_Rect *rects, int numrects);</code>
    /// <summary>
    /// <para>Copy areas of the window surface to the screen.</para>
    /// <para>This is the function you use to reflect changes to portions of the surface
    /// on the screen.</para>
    /// <para>TThis function is equivalent to the SDL 1.2 API SDL_UpdateRects().</para>
    /// <para>Note that this function will update _at least_ the rectangles specified,
    /// but this is only intended as an optimization; in practice, this might
    /// update more of the screen (or all of the screen!), depending on what method
    /// SDL uses to send pixels to the system.</para>
    /// </summary>
    /// <param name="window">the window to update.</param>
    /// <param name="rects">an array of SDL_Rect structures representing areas of the
    /// surface to copy, in pixels.</param>
    /// <param name="numrects">the number of rectangles.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="UpdateWindowSurface"/>
    public static bool UpdateWindowSurfaceRects(IntPtr window, Rect[] rects, int numrects)
    {
        return UpdateWindowSurfaceRectsNativeFunction(window, rects, numrects);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateWindowSurfaceRects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_UpdateWindowSurfaceRectsPointer(IntPtr window, IntPtr rects, int numrects);
    private delegate bool UpdateWindowSurfaceRectsPointerNativeDelegate(IntPtr window, IntPtr rects, int numrects);
    private static UpdateWindowSurfaceRectsPointerNativeDelegate UpdateWindowSurfaceRectsPointerNativeFunction = SDL_UpdateWindowSurfaceRectsPointer;

    /// <inheritdoc cref="UpdateWindowSurfaceRects(nint, Rect[], int)"/>
    public static unsafe bool UpdateWindowSurfaceRects(IntPtr window, ReadOnlySpan<Rect> rects, int numrects)
    {
        fixed (Rect* pRects = rects)
        {
            return UpdateWindowSurfaceRectsPointerNativeFunction(window, (IntPtr)pRects, numrects);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyWindowSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_DestroyWindowSurface(IntPtr window);
    private delegate bool DestroyWindowSurfaceNativeDelegate(IntPtr window);
    private static DestroyWindowSurfaceNativeDelegate DestroyWindowSurfaceNativeFunction = SDL_DestroyWindowSurface;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_DestroyWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// Destroy the surface associated with the window.
    /// </summary>
    /// <param name="window">the window to update.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowSurface"/>
    /// <seealso cref="WindowHasSurface"/>
    public static bool DestroyWindowSurface(IntPtr window)
    {
        return DestroyWindowSurfaceNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowKeyboardGrab"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowKeyboardGrab(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool grabbed);
    private delegate bool SetWindowKeyboardGrabNativeDelegate(IntPtr window, bool grabbed);
    private static SetWindowKeyboardGrabNativeDelegate SetWindowKeyboardGrabNativeFunction = SDL_SetWindowKeyboardGrab;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowKeyboardGrab(SDL_Window *window, bool grabbed);</code>
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
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowKeyboardGrab(nint)"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    public static bool SetWindowKeyboardGrab(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool grabbed)
    {
        return SetWindowKeyboardGrabNativeFunction(window, grabbed);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowMouseGrab"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMouseGrab(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool grabbed);
    private delegate bool SetWindowMouseGrabNativeDelegate(IntPtr window, bool grabbed);
    private static SetWindowMouseGrabNativeDelegate SetWindowMouseGrabNativeFunction = SDL_SetWindowMouseGrab;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowMouseGrab(SDL_Window *window, bool grabbed);</code>
    /// <summary>
    /// <para>Set a window's mouse grab mode.</para>
    /// <para>Mouse grab confines the mouse cursor to the window.</para>
    /// </summary>
    /// <param name="window">the window for which the mouse grab mode should be set.</param>
    /// <param name="grabbed">this is <c>true</c> to grab mouse, and <c>false</c> to release.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="SetWindowMouseRect(nint, nint)"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static bool SetWindowMouseGrab(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool grabbed)
    {
        return SetWindowMouseGrabNativeFunction(window, grabbed);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowKeyboardGrab"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowKeyboardGrab(IntPtr window);
    private delegate bool GetWindowKeyboardGrabNativeDelegate(IntPtr window);
    private static GetWindowKeyboardGrabNativeDelegate GetWindowKeyboardGrabNativeFunction = SDL_GetWindowKeyboardGrab;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowKeyboardGrab(SDL_Window *window);</code>
    /// <summary>
    /// Get a window's keyboard grab mode.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns><c>true</c> if keyboard is grabbed, and <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static bool GetWindowKeyboardGrab(IntPtr window)
    {
        return GetWindowKeyboardGrabNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowMouseGrab"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowMouseGrab(IntPtr window);
    private delegate bool GetWindowMouseGrabNativeDelegate(IntPtr window);
    private static GetWindowMouseGrabNativeDelegate GetWindowMouseGrabNativeFunction = SDL_GetWindowMouseGrab;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowMouseGrab(SDL_Window *window);</code>
    /// <summary>
    /// Get a window's mouse grab mode.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns><c>true</c> if mouse is grabbed, and <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="SetWindowMouseRect(nint, nint)"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static bool GetWindowMouseGrab(IntPtr window)
    {
        return GetWindowMouseGrabNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGrabbedWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGrabbedWindow();
    private delegate IntPtr GetGrabbedWindowNativeDelegate();
    private static GetGrabbedWindowNativeDelegate GetGrabbedWindowNativeFunction = SDL_GetGrabbedWindow;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetGrabbedWindow(void);</code>
    /// <summary>
    /// Get the window that currently has an input grab enabled.
    /// </summary>
    /// <returns>the window if input is grabbed or <c>null</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowMouseGrab"/>
    /// <seealso cref="SetWindowKeyboardGrab"/>
    public static IntPtr GetGrabbedWindow()
    {
        return GetGrabbedWindowNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowMouseRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMouseRectPointer(IntPtr window, IntPtr rect);
    private delegate bool SetWindowMouseRectPointerNativeDelegate(IntPtr window, IntPtr rect);
    private static SetWindowMouseRectPointerNativeDelegate SetWindowMouseRectPointerNativeFunction = SDL_SetWindowMouseRectPointer;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowMouseRect(SDL_Window *window, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Confines the cursor to the specified area of a window.</para>
    /// <para>Note that this does NOT grab the cursor, it only defines the area a cursor
    /// is restricted to when the window has mouse focus.</para>
    /// </summary>
    /// <param name="window">the window that will be associated with the barrier.</param>
    /// <param name="rect">a rectangle area in window-relative coordinates. If <c>null</c> the
    /// barrier for the specified window will be destroyed.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    public static bool SetWindowMouseRect(IntPtr window, IntPtr rect)
    {
        return SetWindowMouseRectPointerNativeFunction(window, rect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowMouseRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMouseRectRect(IntPtr window, in Rect rect);
    private delegate bool SetWindowMouseRectRectNativeDelegate(IntPtr window, in Rect rect);
    private static SetWindowMouseRectRectNativeDelegate SetWindowMouseRectRectNativeFunction = SDL_SetWindowMouseRectRect;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowMouseRect(SDL_Window *window, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Confines the cursor to the specified area of a window.</para>
    /// <para>Note that this does NOT grab the cursor, it only defines the area a cursor
    /// is restricted to when the window has mouse focus.</para>
    /// </summary>
    /// <param name="window">the window that will be associated with the barrier.</param>
    /// <param name="rect">a rectangle area in window-relative coordinates. If <c>null</c> the
    /// barrier for the specified window will be destroyed.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowMouseRect"/>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    public static bool SetWindowMouseRect(IntPtr window, in Rect rect)
    {
        return SetWindowMouseRectRectNativeFunction(window, in rect);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowMouseRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowMouseRect(IntPtr window);
    private delegate IntPtr GetWindowMouseRectNativeDelegate(IntPtr window);
    private static GetWindowMouseRectNativeDelegate GetWindowMouseRectNativeFunction = SDL_GetWindowMouseRect;

    /// <code>extern SDL_DECLSPEC const SDL_Rect * SDLCALL SDL_GetWindowMouseRect(SDL_Window *window);</code>
    /// <summary>
    /// Get the mouse confinement rectangle of a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>a pointer to the mouse confinement rectangle of a window, or <c>null</c>
    /// if there isn't one.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowMouseRect(nint, nint)"/>
    /// <seealso cref="GetWindowMouseGrab"/>
    /// <seealso cref="SetWindowMouseGrab"/>
    public static IntPtr GetWindowMouseRect(IntPtr window)
    {
        return GetWindowMouseRectNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowOpacity"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowOpacity(IntPtr window, float opacity);
    private delegate bool SetWindowOpacityNativeDelegate(IntPtr window, float opacity);
    private static SetWindowOpacityNativeDelegate SetWindowOpacityNativeFunction = SDL_SetWindowOpacity;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowOpacity(SDL_Window *window, float opacity);</code>
    /// <summary>
    /// <para>Set the opacity for a window.</para>
    /// <para>The parameter <c>opacity</c> will be clamped internally between 0.0f
    /// (transparent) and 1.0f (opaque).</para>
    /// </summary>
    /// <remarks>This function also returns <c>false</c> if setting the opacity isn't supported.</remarks>
    /// <param name="window">the window which will be made transparent or opaque.</param>
    /// <param name="opacity">the opacity value (0.0f - transparent, 1.0f - opaque).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowOpacity"/>
    public static bool SetWindowOpacity(IntPtr window, float opacity)
    {
        return SetWindowOpacityNativeFunction(window, opacity);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowOpacity"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowOpacity(IntPtr window);
    private delegate float GetWindowOpacityNativeDelegate(IntPtr window);
    private static GetWindowOpacityNativeDelegate GetWindowOpacityNativeFunction = SDL_GetWindowOpacity;

    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetWindowOpacity(SDL_Window *window);</code>
    /// <summary>
    /// <para>Get the opacity of a window.</para>
    /// <para>If transparency isn't supported on this platform, opacity will be returned
    /// as 1.0f without error.</para>
    /// </summary>
    /// <param name="window">the window to get the current opacity value from.</param>
    /// <returns>the opacity, (0.0f - transparent, 1.0f - opaque), or -1.0f on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowOpacity"/>
    public static float GetWindowOpacity(IntPtr window)
    {
        return GetWindowOpacityNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowParent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowParent(IntPtr window, IntPtr parent);
    private delegate bool SetWindowParentNativeDelegate(IntPtr window, IntPtr parent);
    private static SetWindowParentNativeDelegate SetWindowParentNativeFunction = SDL_SetWindowParent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowParent(SDL_Window *window, SDL_Window *parent);</code>
    /// <summary>
    /// <para>Set the window as a child of a parent window.</para>
    /// <para>If the window is already the child of an existing window, it will be
    /// reparented to the new owner. Setting the parent window to <c>null</c> unparents
    /// the window and removes child window status.</para>
    /// <para>If a parent window is hidden or destroyed, the operation will be
    /// recursively applied to child windows. Child windows hidden with the parent
    /// that did not have their hidden status explicitly set will be restored when
    /// the parent is shown.</para>
    /// <para>Attempting to set the parent of a window that is currently in the modal
    /// state will fail. Use <see cref="SetWindowModal"/> to cancel the modal status before
    /// attempting to change the parent.</para>
    /// <para>Popup windows cannot change parents and attempts to do so will fail.</para>
    /// <para>Setting a parent window that is currently the sibling or descendent of the
    /// child window results in undefined behavior.</para>
    /// </summary>
    /// <param name="window">the window that should become the child of a parent.</param>
    /// <param name="parent">the new parent window for the child window.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowModal"/>
    public static bool SetWindowParent(IntPtr window, IntPtr parent)
    {
        return SetWindowParentNativeFunction(window, parent);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowModal"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowModal(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool modal);
    private delegate bool SetWindowModalNativeDelegate(IntPtr window, bool modal);
    private static SetWindowModalNativeDelegate SetWindowModalNativeFunction = SDL_SetWindowModal;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowModal(SDL_Window *window, bool modal);</code>
    /// <summary>
    /// <para>Toggle the state of the window as modal.</para>
    /// <para>To enable modal status on a window, the window must currently be the child
    /// window of a parent, or toggling modal status on will fail.</para>
    /// </summary>
    /// <param name="window">the window on which to set the modal state.</param>
    /// <param name="modal"><c>true</c> to toggle modal status on, <c>false</c> to toggle it off.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowParent"/>
    /// <seealso cref="WindowFlags.Modal"/>
    public static bool SetWindowModal(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool modal)
    {
        return SetWindowModalNativeFunction(window, modal);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowFocusable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowFocusable(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool focusable);
    private delegate bool SetWindowFocusableNativeDelegate(IntPtr window, bool focusable);
    private static SetWindowFocusableNativeDelegate SetWindowFocusableNativeFunction = SDL_SetWindowFocusable;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowFocusable(SDL_Window *window, bool focusable);</code>
    /// <summary>
    /// Set whether the window may have input focus.
    /// </summary>
    /// <param name="window">the window to set focusable state.</param>
    /// <param name="focusable"><c>true</c> to allow input focus, <c>false</c> to not allow input focus.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool SetWindowFocusable(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool focusable)
    {
        return SetWindowFocusableNativeFunction(window, focusable);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowWindowSystemMenu"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ShowWindowSystemMenu(IntPtr window, int x, int y);
    private delegate bool ShowWindowSystemMenuNativeDelegate(IntPtr window, int x, int y);
    private static ShowWindowSystemMenuNativeDelegate ShowWindowSystemMenuNativeFunction = SDL_ShowWindowSystemMenu;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShowWindowSystemMenu(SDL_Window *window, int x, int y);</code>
    /// <summary>
    /// <para>Display the system-level window menu.</para>
    /// <para>TThis default window menu is provided by the system and on some platforms
    /// provides functionality for setting or changing privileged state on the
    /// window, such as moving it between workspaces or displays, or toggling the
    /// always-on-top property.</para>
    /// </summary>
    /// <remarks>On platforms or desktops where this is unsupported, this function does
    /// nothing.</remarks>
    /// <param name="window">the window for which the menu will be displayed.</param>
    /// <param name="x">the x coordinate of the menu, relative to the origin (top-left) of
    /// the client area.</param>
    /// <param name="y">the y coordinate of the menu, relative to the origin (top-left) of
    /// the client area.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ShowWindowSystemMenu(IntPtr window, int x, int y)
    {
        return ShowWindowSystemMenuNativeFunction(window, x, y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowHitTest"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowHitTest(IntPtr window, HitTest? callback, IntPtr callbackData);
    private delegate bool SetWindowHitTestNativeDelegate(IntPtr window, HitTest? callback, IntPtr callbackData);
    private static SetWindowHitTestNativeDelegate SetWindowHitTestNativeFunction = SDL_SetWindowHitTest;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowHitTest(SDL_Window *window, SDL_HitTest callback, void *callback_data);</code>
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
    /// <para>Specifying <c>null</c> for a callback disables hit-testing. Hit-testing is
    /// disabled by default.</para>
    /// <para>Platforms that don't support this functionality will return <c>false</c>
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
    /// <param name="callbackData">an app-defined void pointer passed to <c>callback</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for mor
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool SetWindowHitTest(IntPtr window, HitTest? callback, IntPtr callbackData)
    {
        return SetWindowHitTestNativeFunction(window, callback, callbackData);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowShape"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowShape(IntPtr window, IntPtr shape);
    private delegate bool SetWindowShapeNativeDelegate(IntPtr window, IntPtr shape);
    private static SetWindowShapeNativeDelegate SetWindowShapeNativeFunction = SDL_SetWindowShape;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowShape(SDL_Window *window, SDL_Surface *shape);</code>
    /// <summary>
    /// <para>Set the shape of a transparent window.</para>
    /// <para>This sets the alpha channel of a transparent window and any fully
    /// transparent areas are also transparent to mouse clicks. If you are using
    /// something besides the SDL render API, then you are responsible for drawing
    /// the alpha channel of the window to match the shape alpha channel to get
    /// consistent cross-platform results.</para>
    /// <para>The shape is copied inside this function, so you can free it afterwards. If
    /// your shape surface changes, you should call <see cref="SetWindowShape"/> again to
    /// update the window. This is an expensive operation, so should be done
    /// sparingly.</para>
    /// <para>The window must have been created with the <see cref="WindowFlags.Transparent"/> flag.</para>
    /// </summary>
    /// <param name="window">the window.</param>
    /// <param name="shape">the surface representing the shape of the window, or <c>null</c> to
    /// remove any current shape.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool SetWindowShape(IntPtr window, IntPtr shape)
    {
        return SetWindowShapeNativeFunction(window, shape);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlashWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_FlashWindow(IntPtr window, FlashOperation operation);
    private delegate bool FlashWindowNativeDelegate(IntPtr window, FlashOperation operation);
    private static FlashWindowNativeDelegate FlashWindowNativeFunction = SDL_FlashWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FlashWindow(SDL_Window *window, SDL_FlashOperation operation);</code>
    /// <summary>
    /// Request a window to demand attention from the user.
    /// </summary>
    /// <param name="window">the window to be flashed.</param>
    /// <param name="operation">the operation to perform.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool FlashWindow(IntPtr window, FlashOperation operation)
    {
        return FlashWindowNativeFunction(window, operation);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowProgressState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowProgressState(IntPtr window, ProgressState state);
    private delegate bool SetWindowProgressStateNativeDelegate(IntPtr window, ProgressState state);
    private static SetWindowProgressStateNativeDelegate SetWindowProgressStateNativeFunction = SDL_SetWindowProgressState;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowProgressState(SDL_Window *window, SDL_ProgressState state);</code>
    /// <summary>
    /// Sets the state of the progress bar for the given window’s taskbar icon.
    /// </summary>
    /// <param name="window">the window whose progress state is to be modified.</param>
    /// <param name="state">the progress state. <see cref="ProgressState.None"/> stops displaying
    /// the progress bar.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static bool SetWindowProgressState(IntPtr window, ProgressState state)
    {
        return SetWindowProgressStateNativeFunction(window, state);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowProgressState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProgressState SDL_GetWindowProgressState(IntPtr window);
    private delegate ProgressState GetWindowProgressStateNativeDelegate(IntPtr window);
    private static GetWindowProgressStateNativeDelegate GetWindowProgressStateNativeFunction = SDL_GetWindowProgressState;

    /// <code>extern SDL_DECLSPEC SDL_ProgressState SDLCALL SDL_GetWindowProgressState(SDL_Window *window);</code>
    /// <summary>
    /// Get the state of the progress bar for the given window’s taskbar icon.
    /// </summary>
    /// <param name="window">the window to get the current progress state from.</param>
    /// <returns>the progress state, or <see cref="ProgressState.Invalid"/> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static ProgressState GetWindowProgressState(IntPtr window)
    {
        return GetWindowProgressStateNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowProgressValue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowProgressValue(IntPtr window, float value);
    private delegate bool SetWindowProgressValueNativeDelegate(IntPtr window, float value);
    private static SetWindowProgressValueNativeDelegate SetWindowProgressValueNativeFunction = SDL_SetWindowProgressValue;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowProgressValue(SDL_Window *window, float value);</code>
    /// <summary>
    /// <para>Sets the value of the progress bar for the given window’s taskbar icon.</para>
    /// </summary>
    /// <param name="window">the window whose progress value is to be modified.</param>
    /// <param name="value">the progress value in the range of [0.0f - 1.0f]. If the value
    /// is outside the valid range, it gets clamped.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static bool SetWindowProgressValue(IntPtr window, float value)
    {
        return SetWindowProgressValueNativeFunction(window, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowProgressValue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowProgressValue(IntPtr window);
    private delegate float GetWindowProgressValueNativeDelegate(IntPtr window);
    private static GetWindowProgressValueNativeDelegate GetWindowProgressValueNativeFunction = SDL_GetWindowProgressValue;

    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetWindowProgressValue(SDL_Window *window);</code>
    /// <summary>
    /// Get the value of the progress bar for the given window’s taskbar icon.
    /// </summary>
    /// <param name="window">the window to get the current progress value from.</param>
    /// <returns>the progress value in the range of [0.0f - 1.0f], or -1.0f on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static float GetWindowProgressValue(IntPtr window)
    {
        return GetWindowProgressValueNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(IntPtr window);
    private delegate void DestroyWindowNativeDelegate(IntPtr window);
    private static DestroyWindowNativeDelegate DestroyWindowNativeFunction = SDL_DestroyWindow;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Destroy a window.</para>
    /// <para>Any child windows owned by the window will be recursively destroyed as
    /// well.</para>
    /// <para>Note that on some platforms, the visible window may not actually be removed
    /// from the screen until the SDL event loop is pumped again, even though the
    /// SDL_Window is no longer valid after this call.</para>
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <param name="window">the window to destroy.</param>
    /// <seealso cref="CreatePopupWindow"/>
    /// <seealso cref="CreateWindow"/>
    /// <seealso cref="CreateWindowWithProperties"/>
    public static void DestroyWindow(IntPtr window)
    {
        DestroyWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ScreenSaverEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ScreenSaverEnabled();
    private delegate bool ScreenSaverEnabledNativeDelegate();
    private static ScreenSaverEnabledNativeDelegate ScreenSaverEnabledNativeFunction = SDL_ScreenSaverEnabled;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ScreenSaverEnabled(void);</code>
    /// <summary>
    /// <para>Check whether the screensaver is currently enabled.</para>
    /// <para>The screensaver is disabled by default.</para>
    /// </summary>
    /// <remarks>The default can also be changed using <see cref="Hints.VideoAllowScreensaver"/></remarks>
    /// <returns><c>true</c> if the screensaver is enabled, <c>false</c> if it is disabled.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DisableScreenSaver"/>
    /// <seealso cref="EnableScreenSaver"/>
    public static bool ScreenSaverEnabled()
    {
        return ScreenSaverEnabledNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EnableScreenSaver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_EnableScreenSaver();
    private delegate bool EnableScreenSaverNativeDelegate();
    private static EnableScreenSaverNativeDelegate EnableScreenSaverNativeFunction = SDL_EnableScreenSaver;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_EnableScreenSaver(void);</code>
    /// <summary>
    /// Allow the screen to be blanked by a screen saver.
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DisableScreenSaver"/>
    /// <seealso cref="ScreenSaverEnabled"/>
    public static bool EnableScreenSaver()
    {
        return EnableScreenSaverNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DisableScreenSaver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_DisableScreenSaver();
    private delegate bool DisableScreenSaverNativeDelegate();
    private static DisableScreenSaverNativeDelegate DisableScreenSaverNativeFunction = SDL_DisableScreenSaver;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_DisableScreenSaver(void);</code>
    /// <summary>
    /// <para>Prevent the screen from being blanked by a screen saver.</para>
    /// <para>If you disable the screensaver, it is automatically re-enabled when SDL
    /// quits.</para>
    /// </summary>
    /// <remarks>The screensaver is disabled by default, but this may by changed by
    /// <see cref="Hints.VideoAllowScreensaver"/>.</remarks>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EnableScreenSaver"/>
    /// <seealso cref="ScreenSaverEnabled"/>
    public static bool DisableScreenSaver()
    {
        return DisableScreenSaverNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_LoadLibrary"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_LoadLibrary([MarshalAs(UnmanagedType.LPUTF8Str)] string? path);
    private delegate bool GLLoadLibraryNativeDelegate(string? path);
    private static GLLoadLibraryNativeDelegate GLLoadLibraryNativeFunction = SDL_GL_LoadLibrary;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_LoadLibrary(const char *path);</code>
    /// <summary>
    /// <para>Dynamically load an OpenGL library.</para>
    /// <para>This should be done after initializing the video driver, but before
    /// creating any OpenGL windows. If no OpenGL library is loaded, the default
    /// library will be loaded upon creation of the first OpenGL window.</para>
    /// <para>If you do this, you need to retrieve all of the GL functions used in your
    /// program from the dynamic library using <see cref="GLGetProcAddress"/>.</para>
    /// </summary>
    /// <param name="path">the platform dependent OpenGL library name, or <c>null</c> to open the
    /// default OpenGL library.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLGetProcAddress"/>
    /// <seealso cref="GLUnloadLibrary"/>
    public static bool GLLoadLibrary([MarshalAs(UnmanagedType.LPUTF8Str)] string? path)
    {
        return GLLoadLibraryNativeFunction(path);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_GetProcAddress"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_GetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
    private delegate IntPtr GLGetProcAddressNativeDelegate(string proc);
    private static GLGetProcAddressNativeDelegate GLGetProcAddressNativeFunction = SDL_GL_GetProcAddress;

    //public static partial FunctionPointer? GLGetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
    /// <code>extern SDL_DECLSPEC SDL_FunctionPointer SDLCALL SDL_GL_GetProcAddress(const char *proc);</code>
    /// <summary>
    /// <para>Get an OpenGL function by name.</para>
    /// <para>If the GL library is loaded at runtime with <see cref="GLLoadLibrary"/>, then all
    /// GL functions must be retrieved this way. Usually this is used to retrieve
    /// function pointers to OpenGL extensions.</para>
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
    /// look up a function that doesn't exist, you'll get a non-<c>null</c> result that
    /// is _NOT_ safe to call. You must always make sure the function is actually
    /// available for a given GL context before calling it, by checking for the
    /// existence of the appropriate extension with <see cref="GLExtensionSupported"/>,
    /// or verifying that the version of OpenGL you're using offers the function
    /// as core functionality.</item>
    /// <item>Some OpenGL drivers, on all platforms, <b>will</b> return <c>null</c>if a function
    /// isn't supported, but you can't count on this behavior. Check for
    /// extensions you use, and if you get a <c>null</c> anyway, act as if that
    /// extension wasn't available. This is probably a bug in the driver, but you
    /// can code defensively for this scenario anyhow.</item>
    /// <item>Just because you're on Linux/Unix, don't assume you'll be using X11.
    /// Next-gen display servers are waiting to replace it, and may or may not
    /// make the same promises about function pointers.</item>
    /// <item>OpenGL function pointers must be declared <c>APIENTRY</c> as in the example
    /// code. This will ensure the proper calling convention is followed on
    /// platforms where this matters (Win32) thereby avoiding stack corruption.</item>
    /// </list>
    /// </summary>
    /// <param name="proc">the name of an OpenGL function.</param>
    /// <returns>a pointer to the named OpenGL function. The returned pointer
    /// should be cast to the appropriate function signature.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <see cref="GLExtensionSupported"/>
    /// <see cref="GLLoadLibrary"/>
    /// <see cref="GLUnloadLibrary"/>
    public static IntPtr GLGetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc)
    {
        return GLGetProcAddressNativeFunction(proc);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EGL_GetProcAddress"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
    private delegate IntPtr EGLGetProcAddressNativeDelegate(string proc);
    private static EGLGetProcAddressNativeDelegate EGLGetProcAddressNativeFunction = SDL_EGL_GetProcAddress;

    //public static partial FunctionPointer? EGLGetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
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
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EGLGetCurrentDisplay"/>
    public static IntPtr EGLGetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc)
    {
        return EGLGetProcAddressNativeFunction(proc);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_UnloadLibrary"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GL_UnloadLibrary();
    private delegate void GLUnloadLibraryNativeDelegate();
    private static GLUnloadLibraryNativeDelegate GLUnloadLibraryNativeFunction = SDL_GL_UnloadLibrary;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GL_UnloadLibrary(void);</code>
    /// <summary>
    /// Unload the OpenGL library previously loaded by <see cref="GLLoadLibrary"/>.
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLLoadLibrary"/>
    public static void GLUnloadLibrary()
    {
        GLUnloadLibraryNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_ExtensionSupported"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_ExtensionSupported([MarshalAs(UnmanagedType.LPUTF8Str)] string extension);
    private delegate bool GLExtensionSupportedNativeDelegate(string extension);
    private static GLExtensionSupportedNativeDelegate GLExtensionSupportedNativeFunction = SDL_GL_ExtensionSupported;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_ExtensionSupported(const char *extension);</code>
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
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <returns><c>true</c> if the extension is supported, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool GLExtensionSupported([MarshalAs(UnmanagedType.LPUTF8Str)] string extension)
    {
        return GLExtensionSupportedNativeFunction(extension);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_ResetAttributes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GL_ResetAttributes();
    private delegate void GLResetAttributesNativeDelegate();
    private static GLResetAttributesNativeDelegate GLResetAttributesNativeFunction = SDL_GL_ResetAttributes;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GL_ResetAttributes(void);</code>
    /// <summary>
    /// Reset all previously set OpenGL context attributes to their default values.
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLGetAttribute"/>
    /// <seealso cref="GLSetAttribute"/>
    public static void GLResetAttributes()
    {
        GLResetAttributesNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_SetAttribute"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_SetAttribute(GLAttr attr, int value);
    private delegate bool GLSetAttributeNativeDelegate(GLAttr attr, int value);
    private static GLSetAttributeNativeDelegate GLSetAttributeNativeFunction = SDL_GL_SetAttribute;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_SetAttribute(SDL_GLAttr attr, int value);</code>
    /// <summary>
    /// Set an OpenGL window attribute before window creation.
    /// <para>This function sets the OpenGL attribute <c>attr</c> to <c>value</c>. The requested
    /// attributes should be set before creating an OpenGL window. You should use
    /// <see cref="GLGetAttribute"/> to check the values after creating the OpenGL
    /// context, since the values obtained can differ from the requested ones.</para>
    /// </summary>
    /// <param name="attr">an enum value specifying the OpenGL attribute to set.</param>
    /// <param name="value">the desired value for the attribute.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLCreateContext"/>
    /// <seealso cref="GLGetAttribute"/>
    /// <seealso cref="GLResetAttributes"/>
    public static bool GLSetAttribute(GLAttr attr, int value)
    {
        return GLSetAttributeNativeFunction(attr, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_GetAttribute"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_GetAttribute(GLAttr attr, out int value);
    private delegate bool GLGetAttributeNativeDelegate(GLAttr attr, out int value);
    private static GLGetAttributeNativeDelegate GLGetAttributeNativeFunction = SDL_GL_GetAttribute;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_GetAttribute(SDL_GLAttr attr, int *value);</code>
    /// <summary>
    /// Get the actual value for an attribute from the current context.
    /// </summary>
    /// <param name="attr">an <see cref="GLAttr"/> enum value specifying the OpenGL attribute to
    /// get.</param>
    /// <param name="value">a pointer filled in with the current value of <c>attr</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLResetAttributes"/>
    /// <seealso cref="GLSetAttribute"/>
    public static bool GLGetAttribute(GLAttr attr, out int value)
    {
        return GLGetAttributeNativeFunction(attr, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_CreateContext"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_CreateContext(IntPtr window);
    private delegate IntPtr GLCreateContextNativeDelegate(IntPtr window);
    private static GLCreateContextNativeDelegate GLCreateContextNativeFunction = SDL_GL_CreateContext;

    /// <code>extern SDL_DECLSPEC SDL_GLContext SDLCALL SDL_GL_CreateContext(SDL_Window *window);</code>
    /// <summary>
    /// <para>Create an OpenGL context for an OpenGL window, and make it current.</para>
    /// <para> The OpenGL context will be created with the current states set through
    /// <see cref="GLSetAttribute"/>.</para>
    /// <para>The SDL_Window specified must have been created with the <see cref="WindowFlags.OpenGL"/>
    /// flag, or context creation will fail.</para>
    /// <para>Windows users new to OpenGL should note that, for historical reasons, GL
    /// functions added after OpenGL version 1.1 are not available by default.
    /// Those functions must be loaded at run-time, either with an OpenGL
    /// extension-handling library or with <see cref="GLGetProcAddress"/> and its related
    /// functions.</para>
    /// <para>SDL_GLContext is opaque to the application.</para>
    /// </summary>
    /// <param name="window">the window to associate with the context.</param>
    /// <returns>the OpenGL context associated with <c>window</c> or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLDestroyContext"/>
    /// <seealso cref="GLMakeCurrent"/>
    public static IntPtr GLCreateContext(IntPtr window)
    {
        return GLCreateContextNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_MakeCurrent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_MakeCurrent(IntPtr window, IntPtr context);
    private delegate bool GLMakeCurrentNativeDelegate(IntPtr window, IntPtr context);
    private static GLMakeCurrentNativeDelegate GLMakeCurrentNativeFunction = SDL_GL_MakeCurrent;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_MakeCurrent(SDL_Window *window, SDL_GLContext context);</code>
    /// <summary>
    /// <para>Set up an OpenGL context for rendering into an OpenGL window.</para>
    /// <para>The context must have been created with a compatible window.</para>
    /// </summary>
    /// <param name="window">the window to associate with the context.</param>
    /// <param name="context">the OpenGL context to associate with the window.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLCreateContext"/>
    public static bool GLMakeCurrent(IntPtr window, IntPtr context)
    {
        return GLMakeCurrentNativeFunction(window, context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_GetCurrentWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_GetCurrentWindow();
    private delegate IntPtr GLGetCurrentWindowNativeDelegate();
    private static GLGetCurrentWindowNativeDelegate GLGetCurrentWindowNativeFunction = SDL_GL_GetCurrentWindow;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GL_GetCurrentWindow(void);</code>
    /// <summary>
    /// Get the currently active OpenGL window.
    /// </summary>
    /// <returns>the currently active OpenGL window on success or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr GLGetCurrentWindow()
    {
        return GLGetCurrentWindowNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_GetCurrentContext"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GL_GetCurrentContext();
    private delegate IntPtr GLGetCurrentContextNativeDelegate();
    private static GLGetCurrentContextNativeDelegate GLGetCurrentContextNativeFunction = SDL_GL_GetCurrentContext;

    /// <code>extern SDL_DECLSPEC SDL_GLContext SDLCALL SDL_GL_GetCurrentContext(void);</code>
    /// <summary>
    /// Get the currently active OpenGL context.
    /// </summary>
    /// <returns>the currently active OpenGL context or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLMakeCurrent"/>
    public static IntPtr GLGetCurrentContext()
    {
        return GLGetCurrentContextNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EGL_GetCurrentDisplay"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetCurrentDisplay();
    private delegate IntPtr EGLGetCurrentDisplayNativeDelegate();
    private static EGLGetCurrentDisplayNativeDelegate EGLGetCurrentDisplayNativeFunction = SDL_EGL_GetCurrentDisplay;

    /// <code>extern SDL_DECLSPEC SDL_EGLDisplay SDLCALL SDL_EGL_GetCurrentDisplay(void);</code>
    /// <summary>
    /// Get the currently active EGL display.
    /// </summary>
    /// <returns>the currently active EGL display or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr EGLGetCurrentDisplay()
    {
        return EGLGetCurrentDisplayNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EGL_GetCurrentConfig"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetCurrentConfig();
    private delegate IntPtr EGLGetCurrentConfigNativeDelegate();
    private static EGLGetCurrentConfigNativeDelegate EGLGetCurrentConfigNativeFunction = SDL_EGL_GetCurrentConfig;

    /// <code>extern SDL_DECLSPEC SDL_EGLConfig SDLCALL SDL_EGL_GetCurrentConfig(void);</code>
    /// <summary>
    /// Get the currently active EGL config.
    /// </summary>
    /// <returns>the currently active EGL config or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr EGLGetCurrentConfig()
    {
        return EGLGetCurrentConfigNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EGL_GetWindowSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_EGL_GetWindowSurface(IntPtr window);
    private delegate IntPtr EGLGetWindowSurfaceNativeDelegate(IntPtr window);
    private static EGLGetWindowSurfaceNativeDelegate EGLGetWindowSurfaceNativeFunction = SDL_EGL_GetWindowSurface;

    /// <code>extern SDL_DECLSPEC SDL_EGLSurface SDLCALL SDL_EGL_GetWindowSurface(SDL_Window *window);</code>
    /// <summary>
    /// Get the EGL surface associated with the window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the EGLSurface pointer associated with the window, or <c>null</c> on
    /// failure.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr EGLGetWindowSurface(IntPtr window)
    {
        return EGLGetWindowSurfaceNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EGL_SetAttributeCallbacks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EGL_SetAttributeCallbacks(EGLAttribArrayCallback? platformAttribCallback,
        EGLIntArrayCallback? surfaceAttribCallback, EGLIntArrayCallback? contextAttribCallback, IntPtr userdata);
    private delegate void EGLSetAttributeCallbacksNativeDelegate(EGLAttribArrayCallback? platformAttribCallback,
        EGLIntArrayCallback? surfaceAttribCallback, EGLIntArrayCallback? contextAttribCallback, IntPtr userdata);
    private static EGLSetAttributeCallbacksNativeDelegate EGLSetAttributeCallbacksNativeFunction = SDL_EGL_SetAttributeCallbacks;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EGL_SetAttributeCallbacks(SDL_EGLAttribArrayCallback platformAttribCallback,
    /// SDL_EGLIntArrayCallback surfaceAttribCallback,
    /// SDL_EGLIntArrayCallback contextAttribCallback, void *userdata);</code>
    /// <summary>
    /// <para>Sets the callbacks for defining custom EGLAttrib arrays for EGL
    /// initialization.</para>
    /// <para>Callbacks that aren't needed can be set to <c>null</c>.</para>
    /// <para>NOTE: These callback pointers will be reset after <see cref="GLResetAttributes"/>.</para>
    /// </summary>
    /// <param name="platformAttribCallback">callback for attributes to pass to
    /// eglGetPlatformDisplay. May be <c>null</c>.</param>
    /// <param name="surfaceAttribCallback">callback for attributes to pass to
    /// eglCreateSurface. May be <c>null</c>.</param>
    /// <param name="contextAttribCallback">callback for attributes to pass to
    /// eglCreateContext. May be <c>null</c>.</param>
    /// <param name="userdata">a pointer that is passed to the callbacks.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void EGLSetAttributeCallbacks(EGLAttribArrayCallback? platformAttribCallback,
        EGLIntArrayCallback? surfaceAttribCallback, EGLIntArrayCallback? contextAttribCallback, IntPtr userdata)
    {
        EGLSetAttributeCallbacksNativeFunction(platformAttribCallback, surfaceAttribCallback, contextAttribCallback, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_SetSwapInterval"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_SetSwapInterval(int interval);
    private delegate bool GLSetSwapIntervalNativeDelegate(int interval);
    private static GLSetSwapIntervalNativeDelegate GLSetSwapIntervalNativeFunction = SDL_GL_SetSwapInterval;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_SetSwapInterval(int interval);</code>
    /// <summary>
    /// <para>Set the swap interval for the current OpenGL context.</para>
    /// <para>Some systems allow specifying -1 for the interval, to enable adaptive
    /// vsync. Adaptive vsync works the same as vsync, but if you've already missed
    /// the vertical retrace for a given frame, it swaps buffers immediately, which
    /// might be less jarring for the user during occasional framerate drops. If an
    /// application requests adaptive vsync and the system does not support it,
    /// this function will fail and return <c>false</c>. In such a case, you should
    /// probably retry the call with 1 for the interval.</para>
    /// <para>Adaptive vsync is implemented for some glX drivers with
    /// GLX_EXT_swap_control_tear, and for some Windows drivers with
    /// WGL_EXT_swap_control_tear.</para>
    /// <para>Read more on the Khronos wiki:
    /// https://www.khronos.org/opengl/wiki/Swap_Interval#Adaptive_Vsync</para>
    /// </summary>
    /// <param name="interval">0 for immediate updates, 1 for updates synchronized with
    /// the vertical retrace, -1 for adaptive vsync.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLGetSwapInterval"/>
    public static bool GLSetSwapInterval(int interval)
    {
        return GLSetSwapIntervalNativeFunction(interval);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_GetSwapInterval"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_GetSwapInterval(out int interval);
    private delegate bool GLGetSwapIntervalNativeDelegate(out int interval);
    private static GLGetSwapIntervalNativeDelegate GLGetSwapIntervalNativeFunction = SDL_GL_GetSwapInterval;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_GetSwapInterval(int *interval);</code>
    /// <summary>
    /// <para>Get the swap interval for the current OpenGL context.</para>
    /// <para>If the system can't determine the swap interval, or there isn't a valid
    /// current context, this function will set *interval to 0 as a safe default.</para>
    /// </summary>
    /// <param name="interval">output interval value. 0 if there is no vertical retrace
    /// synchronization, 1 if the buffer swap is synchronized with
    /// the vertical retrace, and -1 if late swaps happen
    /// immediately instead of waiting for the next retrace.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLSetSwapInterval"/>
    public static bool GLGetSwapInterval(out int interval)
    {
        return GLGetSwapIntervalNativeFunction(out interval);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_SwapWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_SwapWindow(IntPtr window);
    private delegate bool GLSwapWindowNativeDelegate(IntPtr window);
    private static GLSwapWindowNativeDelegate GLSwapWindowNativeFunction = SDL_GL_SwapWindow;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_SwapWindow(SDL_Window *window);</code>
    /// <summary>
    /// <para>Update a window with OpenGL rendering.</para>
    /// <para>This is used with double-buffered OpenGL contexts, which are the default.</para>
    /// <para>On macOS, make sure you bind 0 to the draw framebuffer before swapping the
    /// window, otherwise nothing will happen. If you aren't using
    /// glBindFramebuffer(), this is the default and you won't have to do anything
    /// extra.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool GLSwapWindow(IntPtr window)
    {
        return GLSwapWindowNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GL_DestroyContext"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GL_DestroyContext(IntPtr window);
    private delegate bool GLDestroyContextNativeDelegate(IntPtr window);
    private static GLDestroyContextNativeDelegate GLDestroyContextNativeFunction = SDL_GL_DestroyContext;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GL_DestroyContext(SDL_GLContext context);</code>
    /// <summary>
    /// Delete an OpenGL context.
    /// </summary>
    /// <param name="window">the OpenGL context to be deleted.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GLCreateContext"/>
    public static bool GLDestroyContext(IntPtr window)
    {
        return GLDestroyContextNativeFunction(window);
    }

}
