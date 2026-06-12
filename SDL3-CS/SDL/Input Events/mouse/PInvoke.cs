#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024-2026 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
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

using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasMouse"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasMouse();
    private delegate bool HasMouseNativeDelegate();
    private static HasMouseNativeDelegate HasMouseNativeFunction = SDL_HasMouse;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasMouse(void);</code>
    /// <summary>
    /// Return whether a mouse is currently connected.
    /// </summary>
    /// <returns><c>true</c> if a mouse is connected, <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMice"/>
    public static bool HasMouse()
    {
        return HasMouseNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMice(out int count);
    private delegate IntPtr GetMiceNativeDelegate(out int count);
    private static GetMiceNativeDelegate GetMiceNativeFunction = SDL_GetMice;
    /// <code>extern SDL_DECLSPEC SDL_MouseID * SDLCALL SDL_GetMice(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently connected mice.</para>
    /// <para>Note that this will include any device or virtual driver that includes
    /// mouse functionality, including some game controllers, KVM switches, etc.
    /// You should wait for input from a device before you consider it actively in
    /// use.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of mice returned, may be
    /// <c>null</c>.</param>
    /// <returns>a 0 terminated array of mouse instance IDs or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMouseNameForID"/>
    /// <seealso cref="HasMouse"/>
    public static uint[]? GetMice(out int count)
    {
        var ptr = GetMiceNativeFunction(out count);

        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMouseNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMouseNameForID(uint instanceId);
    private delegate IntPtr GetMouseNameForIDNativeDelegate(uint instanceId);
    private static GetMouseNameForIDNativeDelegate GetMouseNameForIDNativeFunction = SDL_GetMouseNameForID;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetMouseNameForID(SDL_MouseID instance_id);</code>
    /// <summary>
    /// <para>Get the name of a mouse.</para>
    /// <para>This function returns "" if the mouse doesn't have a name.</para>
    /// </summary>
    /// <param name="instanceId">the mouse instance ID.</param>
    /// <returns>the name of the selected mouse, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMice"/>
    public static string? GetMouseNameForID(uint instanceId)
    {
        var value = GetMouseNameForIDNativeFunction(instanceId);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMouseFocus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMouseFocus();
    private delegate IntPtr GetMouseFocusNativeDelegate();
    private static GetMouseFocusNativeDelegate GetMouseFocusNativeFunction = SDL_GetMouseFocus;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetMouseFocus(void);</code>
    /// <summary>
    /// Get the window which currently has mouse focus.
    /// </summary>
    /// <returns>the window with mouse focus.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr GetMouseFocus()
    {
        return GetMouseFocusNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMouseState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetMouseState(out float x, out float y);
    private delegate MouseButtonFlags GetMouseStateNativeDelegate(out float x, out float y);
    private static GetMouseStateNativeDelegate GetMouseStateNativeFunction = SDL_GetMouseState;

    /// <code>extern SDL_DECLSPEC SDL_MouseButtonFlags SDLCALL SDL_GetMouseState(float *x, float *y);</code>
    /// <summary>
    /// <para>Query SDL's cache for the synchronous mouse button state and the
    /// window-relative SDL-cursor position.</para>
    /// <para>This function returns the cached synchronous state as SDL understands it
    /// from the last pump of the event queue.</para>
    /// <para>To query the platform for immediate asynchronous state, use
    /// <see cref="GetGlobalMouseState"/>.</para>
    /// </summary>
    /// <param name="x">a pointer to receive the SDL-cursor's x-position from the focused
    /// window's top left corner, can be <c>null</c> if unused.</param>
    /// <param name="y">a pointer to receive the SDL-cursor's y-position from the focused
    /// window's top left corner, can be <c>null</c> if unused.</param>
    /// <returns>a 32-bit bitmask of the button state that can be bitwise-compared
    /// against the <see cref="ButtonMask"/> macro.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGlobalMouseState"/>
    /// <seealso cref="GetRelativeMouseState"/>
    public static MouseButtonFlags GetMouseState(out float x, out float y)
    {
        return GetMouseStateNativeFunction(out x, out y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGlobalMouseState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);
    private delegate MouseButtonFlags GetGlobalMouseStateNativeDelegate(out float x, out float y);
    private static GetGlobalMouseStateNativeDelegate GetGlobalMouseStateNativeFunction = SDL_GetGlobalMouseState;

    /// <code>extern SDL_DECLSPEC SDL_MouseButtonFlags SDLCALL SDL_GetGlobalMouseState(float *x, float *y);</code>
    /// <summary>
    /// <para>Query the platform for the asynchronous mouse button state and the
    /// desktop-relative platform-cursor position.</para>
    /// <para>This function immediately queries the platform for the most recent
    /// asynchronous state, more costly than retrieving SDL's cached state in
    /// <see cref="GetMouseState"/>.</para>
    /// <para>Passing non-<c>null</c> pointers to <c>x</c> or <c>y</c> will write the destination with
    /// respective x or y coordinates relative to the desktop.</para>
    /// <para>In Relative Mode, the platform-cursor's position usually contradicts the
    /// SDL-cursor's position as manually calculated from <see cref="GetMouseState"/> and
    /// <see cref="GetWindowPosition"/>.</para>
    /// <para>This function can be useful if you need to track the mouse outside of a
    /// specific window and <see cref="CaptureMouse"/> doesn't fit your needs. For example,
    /// it could be useful if you need to track the mouse while dragging a window,
    /// where coordinates relative to a window might not be in sync at all times.</para>
    /// </summary>
    /// <param name="x">a pointer to receive the platform-cursor's x-position from the
    /// desktop's top left corner, can be <c>null</c> if unused.</param>
    /// <param name="y">a pointer to receive the platform-cursor's y-position from the
    /// desktop's top left corner, can be <c>null</c> if unused.</param>
    /// <returns>a 32-bit bitmask of the button state that can be bitwise-compared
    /// against the <see cref="ButtonMask"/> macro.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CaptureMouse"/>
    /// <seealso cref="GetMouseState"/>
    /// <seealso cref="GetGlobalMouseState"/>
    public static MouseButtonFlags GetGlobalMouseState(out float x, out float y)
    {
        return GetGlobalMouseStateNativeFunction(out x, out y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRelativeMouseState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);
    private delegate MouseButtonFlags GetRelativeMouseStateNativeDelegate(out float x, out float y);
    private static GetRelativeMouseStateNativeDelegate GetRelativeMouseStateNativeFunction = SDL_GetRelativeMouseState;

    /// <code>extern SDL_DECLSPEC SDL_MouseButtonFlags SDLCALL SDL_GetRelativeMouseState(float *x, float *y);</code>
    /// <summary>
    /// <para>Query SDL's cache for the synchronous mouse button state and accumulated
    /// mouse delta since last call.</para>
    /// <para>This function returns the cached synchronous state as SDL understands it
    /// from the last pump of the event queue.</para>
    /// <para>To query the platform for immediate asynchronous state, use
    /// <see cref="GetGlobalMouseState"/>.</para>
    /// <para>In Relative Mode, the platform-cursor's position usually contradicts the
    /// SDL-cursor's position as manually calculated from <see cref="GetMouseState"/> and
    /// <see cref="GetWindowPosition"/>.</para>
    /// <para>Passing non-<c>null</c> pointers to <c>x</c> or <c>y</c> will write the destination with
    /// respective x or y deltas accumulated since the last call to this function
    /// (or since event initialization).</para>
    /// <para>This function is useful for reducing overhead by processing relative mouse
    /// inputs in one go per-frame instead of individually per-event, at the
    /// expense of losing the order between events within the frame (e.g. quickly
    /// pressing and releasing a button within the same frame).</para>
    /// </summary>
    /// <param name="x">a pointer to receive the x mouse delta accumulated since last
    /// call, can be <c>null</c> if unused.</param>
    /// <param name="y">a pointer to receive the y mouse delta accumulated since last
    /// call, can be <c>null</c> if unused.</param>
    /// <returns>a 32-bit bitmask of the button state that can be bitwise-compared
    /// against the <see cref="ButtonMask"/> macro.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMouseState"/>
    /// <seealso cref="GetGlobalMouseState"/>
    public static MouseButtonFlags GetRelativeMouseState(out float x, out float y)
    {
        return GetRelativeMouseStateNativeFunction(out x, out y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WarpMouseInWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WarpMouseInWindow(IntPtr window, float x, float y);
    private delegate void WarpMouseInWindowNativeDelegate(IntPtr window, float x, float y);
    private static WarpMouseInWindowNativeDelegate WarpMouseInWindowNativeFunction = SDL_WarpMouseInWindow;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_WarpMouseInWindow(SDL_Window * window, float x, float y);</code>
    /// <summary>
    /// <para>Move the mouse cursor to the given position within the window.</para>
    /// <para>This function generates a mouse motion event if relative mode is not
    /// enabled. If relative mode is enabled, you can force mouse events for the
    /// warp by setting the <see cref="Hints.MouseRelativeWarpMotion"/> hint.</para>
    /// <para>Note that this function will appear to succeed, but not actually move the
    /// mouse when used over Microsoft Remote Desktop.</para>
    /// </summary>
    /// <param name="window">the window to move the mouse into, or <c>null</c> for the current
    /// mouse focus.</param>
    /// <param name="x">the x coordinate within the window.</param>
    /// <param name="y">the y coordinate within the window.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WarpMouseGlobal"/>
    public static void WarpMouseInWindow(IntPtr window, float x, float y)
    {
        WarpMouseInWindowNativeFunction(window, x, y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WarpMouseGlobal"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WarpMouseGlobal(float x, float y);
    private delegate bool WarpMouseGlobalNativeDelegate(float x, float y);
    private static WarpMouseGlobalNativeDelegate WarpMouseGlobalNativeFunction = SDL_WarpMouseGlobal;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WarpMouseGlobal(float x, float y);</code>
    /// <summary>
    /// <para>Move the mouse to the given position in global screen space.</para>
    /// <para>This function generates a mouse motion event.</para>
    /// <para>A failure of this function usually means that it is unsupported by a
    /// platform.</para>
    /// <para>Note that this function will appear to succeed, but not actually move the
    /// mouse when used over Microsoft Remote Desktop.</para>
    /// </summary>
    /// <param name="x">the x coordinate.</param>
    /// <param name="y">the y coordinate.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WarpMouseInWindow"/>
    public static bool WarpMouseGlobal(float x, float y)
    {
        return WarpMouseGlobalNativeFunction(x, y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRelativeMouseTransform"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetRelativeMouseTransform(MouseMotionTransformCallback callback, IntPtr userdata);
    private delegate bool SetRelativeMouseTransformNativeDelegate(MouseMotionTransformCallback callback, IntPtr userdata);
    private static SetRelativeMouseTransformNativeDelegate SetRelativeMouseTransformNativeFunction = SDL_SetRelativeMouseTransform;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRelativeMouseTransform(SDL_MouseMotionTransformCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a user-defined function by which to transform relative mouse inputs.</para>
    /// <para>This overrides the relative system scale and relative speed scale hints.
    /// Should be called prior to enabling relative mouse mode, fails otherwise.</para>
    /// </summary>
    /// <param name="callback">a callback used to transform relative mouse motion, or <c>null</c>
    /// for default behavior.</param>
    /// <param name="userdata">a pointer that will be passed to <c>callback</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static bool SetRelativeMouseTransform(MouseMotionTransformCallback callback, IntPtr userdata)
    {
        return SetRelativeMouseTransformNativeFunction(callback, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetWindowRelativeMouseMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowRelativeMouseMode(IntPtr window, [MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate bool SetWindowRelativeMouseModeNativeDelegate(IntPtr window, bool enabled);
    private static SetWindowRelativeMouseModeNativeDelegate SetWindowRelativeMouseModeNativeFunction = SDL_SetWindowRelativeMouseMode;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetWindowRelativeMouseMode(SDL_Window *window, bool enabled);</code>
    /// <summary>
    /// <para>Set relative mouse mode for a window.</para>
    /// <para>While the window has focus and relative mouse mode is enabled, the cursor
    /// is hidden, the mouse position is constrained to the window, and SDL will
    /// report continuous relative mouse motion even if the mouse is at the edge of
    /// the window.</para>
    /// <para>If you'd like to keep the mouse position fixed while in relative mode you
    /// can use <see cref="SetWindowMouseRect(IntPtr, in Rect)"/>. If you'd like the cursor to be at a
    /// specific location when relative mode ends, you should use
    /// <see cref="WarpMouseInWindow"/> before disabling relative mode.</para>
    /// <para>This function will flush any pending mouse motion for this window.</para>
    /// </summary>
    /// <param name="window">the window to change.</param>
    /// <param name="enabled"><c>true</c> to enable relative mode, <c>false</c> to disable.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowRelativeMouseMode"/>
    public static bool SetWindowRelativeMouseMode(IntPtr window, bool enabled)
    {
        return SetWindowRelativeMouseModeNativeFunction(window, enabled);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetWindowRelativeMouseMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetWindowRelativeMouseMode(IntPtr window);
    private delegate bool GetWindowRelativeMouseModeNativeDelegate(IntPtr window);
    private static GetWindowRelativeMouseModeNativeDelegate GetWindowRelativeMouseModeNativeFunction = SDL_GetWindowRelativeMouseMode;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetWindowRelativeMouseMode(SDL_Window *window);</code>
    /// <summary>
    /// Query whether relative mouse mode is enabled for a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns><c>true</c> if relative mode is enabled for a window or <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowRelativeMouseMode"/>
    public static bool GetWindowRelativeMouseMode(IntPtr window)
    {
        return GetWindowRelativeMouseModeNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CaptureMouse"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CaptureMouse([MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate bool CaptureMouseNativeDelegate(bool enabled);
    private static CaptureMouseNativeDelegate CaptureMouseNativeFunction = SDL_CaptureMouse;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CaptureMouse(bool enabled);</code>
    /// <summary>
    /// <para>Capture the mouse and to track input outside an SDL window.</para>
    /// <para>Capturing enables your app to obtain mouse events globally, instead of just
    /// within your window. Not all video targets support this function. When
    /// capturing is enabled, the current window will get all mouse events, but
    /// unlike relative mode, no change is made to the cursor and it is not
    /// restrained to your window.</para>
    /// <para>This function may also deny mouse input to other windows--both those in
    /// your application and others on the system--so you should use this function
    /// sparingly, and in small bursts. For example, you might want to track the
    /// mouse while the user is dragging something, until the user releases a mouse
    /// button. It is not recommended that you capture the mouse for long periods
    /// of time, such as the entire time your app is running. For that, you should
    /// probably use <see cref="SetWindowRelativeMouseMode"/> or <see cref="SetWindowMouseGrab"/>,
    /// depending on your goals.</para>
    /// <para>While captured, mouse events still report coordinates relative to the
    /// current (foreground) window, but those coordinates may be outside the
    /// bounds of the window (including negative values). Capturing is only allowed
    /// for the foreground window. If the window loses focus while capturing, the
    /// capture will be disabled automatically.</para>
    /// <para>While capturing is enabled, the current window will have the
    /// <see cref="WindowFlags.MouseCapture"/> flag set.</para>
    /// <para>Please note that SDL will attempt to "auto capture" the mouse while the
    /// user is pressing a button; this is to try and make mouse behavior more
    /// consistent between platforms, and deal with the common case of a user
    /// dragging the mouse outside of the window. This means that if you are
    /// calling <see cref="CaptureMouse"/> only to deal with this situation, you do not
    /// have to (although it is safe to do so). If this causes problems for your
    /// app, you can disable auto capture by setting the
    /// <see cref="Hints.MouseAutoCapture"/> hint to zero.</para>
    /// </summary>
    /// <param name="enabled"><c>true</c> to enable capturing, <c>false</c> to disable.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGlobalMouseState"/>
    public static bool CaptureMouse(bool enabled)
    {
        return CaptureMouseNativeFunction(enabled);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);
    private delegate IntPtr CreateCursorNativeDelegate(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);
    private static CreateCursorNativeDelegate CreateCursorNativeFunction = SDL_CreateCursor;

    /// <code>extern SDL_DECLSPEC SDL_Cursor * SDLCALL SDL_CreateCursor(const Uint8 * data, const Uint8 * mask, int w, int h, int hot_x, int hot_y);</code>
    /// <summary>
    /// <para>Create a cursor using the specified bitmap data and mask (in MSB format).</para>
    /// <para><c>mask</c> has to be in MSB (Most Significant Bit) format.</para>
    /// <para>The cursor width (<c>w</c>) must be a multiple of 8 bits.</para>
    /// <para>The cursor is created in black and white according to the following:</para>
    /// <list type="bullet">
    /// <item>data=0, mask=1: white</item>
    /// <item>data=1, mask=1: black</item>
    /// <item>data=0, mask=0: transparent</item>
    /// <item>data=1, mask=0: inverted color if possible, black if not.</item>
    /// </list>
    /// <para>Cursors created with this function must be freed with <see cref="DestroyCursor"/>.</para>
    /// <para>If you want to have a color cursor, or create your cursor from an
    /// SDL_Surface, you should use <see cref="CreateColorCursor"/>. Alternately, you can
    /// hide the cursor and draw your own as part of your game's rendering, but it
    /// will be bound to the framerate.</para>
    /// <para>Also, <see cref="CreateSystemCursor"/> is available, which provides several
    /// readily-available system cursors to pick from.</para>
    /// </summary>
    /// <param name="data">the color value for each pixel of the cursor.</param>
    /// <param name="mask">the mask value for each pixel of the cursor.</param>
    /// <param name="w">the width of the cursor.</param>
    /// <param name="h">the height of the cursor.</param>
    /// <param name="hotX">the x-axis offset from the left of the cursor image to the
    /// mouse x position, in the range of 0 to <c>w</c> - 1.</param>
    /// <param name="hotY">the y-axis offset from the top of the cursor image to the
    /// mouse y position, in the range of 0 to <c>h</c> - 1.</param>
    /// <returns>a new cursor with the specified parameters on success or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateAnimatedCursor(CursorFrameInfo[], int, int, int)"/>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    public static IntPtr CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY)
    {
        return CreateCursorNativeFunction(data, mask, w, h, hotX, hotY);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateCursor(IntPtr data, IntPtr mask, int w, int h, int hotX, int hotY);
    private delegate IntPtr CreateCursorPointerNativeDelegate(IntPtr data, IntPtr mask, int w, int h, int hotX, int hotY);
    private static CreateCursorPointerNativeDelegate CreateCursorPointerNativeFunction = SDL_CreateCursor;

    /// <inheritdoc cref="CreateCursor(byte[], byte[], int, int, int, int)"/>
    public static unsafe IntPtr CreateCursor(ReadOnlySpan<byte> data, ReadOnlySpan<byte> mask, int w, int h, int hotX, int hotY)
    {
        fixed (byte* pData = data)
        fixed (byte* pMask = mask)
        {
            return CreateCursorPointerNativeFunction((IntPtr)pData, (IntPtr)pMask, w, h, hotX, hotY);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateColorCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateColorCursor(IntPtr surface, int hotX, int hotY);
    private delegate IntPtr CreateColorCursorNativeDelegate(IntPtr surface, int hotX, int hotY);
    private static CreateColorCursorNativeDelegate CreateColorCursorNativeFunction = SDL_CreateColorCursor;

    /// <code>extern SDL_DECLSPEC SDL_Cursor * SDLCALL SDL_CreateColorCursor(SDL_Surface *surface, int hot_x, int hot_y);</code>
    /// <summary>
    /// <para>Create a color cursor.</para>
    /// <para>If this function is passed a surface with alternate representations added
    /// with <see cref="AddSurfaceAlternateImage"/>, the surface will be interpreted as the
    /// content to be used for 100% display scale, and the alternate
    /// representations will be used for high DPI situations if
    /// <see cref="Hints.MouseDPIScaleCursors"/> is enabled. For example, if the original
    /// surface is 32x32, then on a 2x macOS display or 200% display scale on
    /// Windows, a 64x64 version of the image will be used, if available. If a
    /// matching version of the image isn't available, the closest larger size
    /// image will be downscaled to the appropriate size and be used instead, if
    /// available. Otherwise, the closest smaller image will be upscaled and be
    /// used instead.</para>
    /// </summary>
    /// <param name="surface">an <see cref="Surface"/> structure representing the cursor image.</param>
    /// <param name="hotX">the x position of the cursor hot spot.</param>
    /// <param name="hotY">the y position of the cursor hot spot.</param>
    /// <returns>the new cursor on success or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>+
    /// <seealso cref="CreateAnimatedCursor(CursorFrameInfo[], int, int, int)"/>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="CreateCursor(byte[], byte[], int, int, int, int)"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    public static IntPtr CreateColorCursor(IntPtr surface, int hotX, int hotY)
    {
        return CreateColorCursorNativeFunction(surface, hotX, hotY);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateAnimatedCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateAnimatedCursor([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] CursorFrameInfo[] frames, int frameCount, int hotX, int hotY);
    private delegate IntPtr CreateAnimatedCursorNativeDelegate(CursorFrameInfo[] frames, int frameCount, int hotX, int hotY);
    private static CreateAnimatedCursorNativeDelegate CreateAnimatedCursorNativeFunction = SDL_CreateAnimatedCursor;

    /// <code>extern SDL_DECLSPEC SDL_Cursor *SDLCALL SDL_CreateAnimatedCursor(SDL_CursorFrameInfo *frames,int frame_count, int hot_x, int hot_y);</code>
    /// <summary>
    /// Create an animated color cursor.
    /// <para>Animated cursors are composed of a sequential array of frames, specified as
    /// surfaces and durations in an array of <see cref="CursorFrameInfo"/> structs. The hot
    /// spot coordinates are universal to all frames, and all frames must have the
    /// same dimensions.</para>
    /// <para>Frame durations are specified in milliseconds. A duration of 0 implies an
    /// infinite frame time, and the animation will stop on that frame. To create a
    /// one-shot animation, set the duration of the last frame in the sequence to
    /// 0.</para>
    /// <para>If this function is passed surfaces with alternate representations added
    /// with <see cref="AddSurfaceAlternateImage"/>, the surfaces will be interpreted as
    /// the content to be used for 100% display scale, and the alternate
    /// representations will be used for high DPI situations. For example, if the
    /// original surfaces are 32x32, then on a 2x macOS display or 200% display
    /// scale on Windows, a 64x64 version of the image will be used, if available.
    /// If a matching version of the image isn't available, the closest larger size
    /// image will be downscaled to the appropriate size and be used instead, if
    /// available. Otherwise, the closest smaller image will be upscaled and be
    /// used instead.</para>
    /// <para>If the underlying platform does not support animated cursors, this function
    /// will fall back to creating a static color cursor using the first frame in
    /// the sequence.</para>
    /// </summary>
    /// <param name="frames">an array of cursor images composing the animation.</param>
    /// <param name="frameCount">the number of frames in the sequence.</param>
    /// <param name="hotX">the x position of the cursor hot spot.</param>
    /// <param name="hotY">the y position of the cursor hot spot.</param>
    /// <returns>the new cursor on success or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="CreateCursor(byte[], byte[], int, int, int, int)"/>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    public static IntPtr CreateAnimatedCursor(CursorFrameInfo[] frames, int frameCount, int hotX, int hotY)
    {
        return CreateAnimatedCursorNativeFunction(frames, frameCount, hotX, hotY);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateAnimatedCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateAnimatedCursor(IntPtr frames, int frameCount, int hotX, int hotY);
    private delegate IntPtr CreateAnimatedCursorPointerNativeDelegate(IntPtr frames, int frameCount, int hotX, int hotY);
    private static CreateAnimatedCursorPointerNativeDelegate CreateAnimatedCursorPointerNativeFunction = SDL_CreateAnimatedCursor;

    /// <inheritdoc cref="CreateAnimatedCursor(CursorFrameInfo[], int, int, int)"/>
    public static unsafe IntPtr CreateAnimatedCursor(ReadOnlySpan<CursorFrameInfo> frames, int frameCount, int hotX, int hotY)
    {
        fixed (CursorFrameInfo* pFrames = frames)
        {
            return CreateAnimatedCursorPointerNativeFunction((IntPtr)pFrames, frameCount, hotX, hotY);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSystemCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateSystemCursor(SystemCursor id);
    private delegate IntPtr CreateSystemCursorNativeDelegate(SystemCursor id);
    private static CreateSystemCursorNativeDelegate CreateSystemCursorNativeFunction = SDL_CreateSystemCursor;

    /// <code>extern SDL_DECLSPEC SDL_Cursor * SDLCALL SDL_CreateSystemCursor(SDL_SystemCursor id);</code>
    /// <summary>
    /// Create a system cursor.
    /// </summary>
    /// <param name="id">an <see cref="SystemCursor"/> enum value.</param>
    /// <returns>a cursor on success or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyCursor"/>
    public static IntPtr CreateSystemCursor(SystemCursor id)
    {
        return CreateSystemCursorNativeFunction(id);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetCursor(IntPtr cursor);
    private delegate bool SetCursorNativeDelegate(IntPtr cursor);
    private static SetCursorNativeDelegate SetCursorNativeFunction = SDL_SetCursor;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetCursor(SDL_Cursor *cursor);</code>
    /// <summary>
    /// <para>Set the active cursor.</para>
    /// <para>This function sets the currently active cursor to the specified one. If the
    /// cursor is currently visible, the change will be immediately represented on
    /// the display. SetCursor(IntPtr.Zero) can be used to force cursor redraw, if
    /// this is desired for any reason.</para>
    /// </summary>
    /// <param name="cursor">a cursor to make active.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCursor"/>
    public static bool SetCursor(IntPtr cursor)
    {
        return SetCursorNativeFunction(cursor);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCursor();
    private delegate IntPtr GetCursorNativeDelegate();
    private static GetCursorNativeDelegate GetCursorNativeFunction = SDL_GetCursor;

    /// <code>extern SDL_DECLSPEC SDL_Cursor * SDLCALL SDL_GetCursor(void);</code>
    /// <summary>
    /// <para>Get the active cursor.</para>
    /// <para>This function returns a pointer to the current cursor which is owned by the
    /// library. It is not necessary to free the cursor with <see cref="DestroyCursor"/>.</para>
    /// </summary>
    /// <returns>the active cursor or <c>null</c> if there is no mouse.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetCursor"/>
    public static IntPtr GetCursor()
    {
        return GetCursorNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDefaultCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDefaultCursor();
    private delegate IntPtr GetDefaultCursorNativeDelegate();
    private static GetDefaultCursorNativeDelegate GetDefaultCursorNativeFunction = SDL_GetDefaultCursor;

    /// <code>extern SDL_DECLSPEC SDL_Cursor * SDLCALL SDL_GetDefaultCursor(void);</code>
    /// <summary>
    /// <para>Get the default cursor.</para>
    /// <para>You do not have to call <see cref="DestroyCursor"/> on the return value, but it is
    /// safe to do so.</para>
    /// </summary>
    /// <returns>the default cursor on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr GetDefaultCursor()
    {
        return GetDefaultCursorNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyCursor(IntPtr cursor);
    private delegate void DestroyCursorNativeDelegate(IntPtr cursor);
    private static DestroyCursorNativeDelegate DestroyCursorNativeFunction = SDL_DestroyCursor;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyCursor(SDL_Cursor *cursor);</code>
    /// <summary>
    /// <para>Free a previously-created cursor.</para>
    /// <para>Use this function to free cursor resources created with <see cref="CreateCursor(byte[], byte[], int, int, int, int)"/>,
    /// <see cref="CreateColorCursor"/> or <see cref="CreateSystemCursor"/>.</para>
    /// </summary>
    /// <param name="cursor">the cursor to free.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateAnimatedCursor(CursorFrameInfo[], int, int, int)"/>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateCursor(byte[], byte[], int, int, int, int)"/>
    /// <seealso cref="CreateSystemCursor"/>
    public static void DestroyCursor(IntPtr cursor)
    {
        DestroyCursorNativeFunction(cursor);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ShowCursor();
    private delegate bool ShowCursorNativeDelegate();
    private static ShowCursorNativeDelegate ShowCursorNativeFunction = SDL_ShowCursor;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShowCursor(void);</code>
    /// <summary>
    /// Show the cursor.
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CursorVisible"/>
    /// <seealso cref="HideCursor"/>
    public static bool ShowCursor()
    {
        return ShowCursorNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HideCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HideCursor();
    private delegate bool HideCursorNativeDelegate();
    private static HideCursorNativeDelegate HideCursorNativeFunction = SDL_HideCursor;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HideCursor(void);</code>
    /// <summary>
    /// Hide the cursor.
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CursorVisible"/>
    /// <seealso cref="ShowCursor"/>
    public static bool HideCursor()
    {
        return HideCursorNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CursorVisible"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CursorVisible();
    private delegate bool CursorVisibleNativeDelegate();
    private static CursorVisibleNativeDelegate CursorVisibleNativeFunction = SDL_CursorVisible;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CursorVisible(void);</code>
    /// <summary>
    /// Return whether the cursor is currently being shown.
    /// </summary>
    /// <returns><c>true</c> if the cursor is being shown, or <c>false</c> if the cursor is
    /// hidden.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HideCursor"/>
    /// <seealso cref="ShowCursor"/>
    public static bool CursorVisible()
    {
        return CursorVisibleNativeFunction();
    }
}
