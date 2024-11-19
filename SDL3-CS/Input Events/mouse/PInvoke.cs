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
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasMouse();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasMouse(void);</code>
    /// <summary>
    /// Return whether a mouse is currently connected.
    /// </summary>
    /// <returns><c>true</c> if a mouse is connected, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetMice"/>
    public static bool HasMouse() => SDL_HasMouse();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMice(out int count);
    /// <code>extern SDL_DECLSPEC SDL_MouseID *SDLCALL SDL_GetMice(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently connected mice.</para>
    /// <para>Note that this will include any device or virtual driver that includes
    /// mouse functionality, including some game controllers, KVM switches, etc.
    /// You should wait for input from a device before you consider it actively in
    /// use.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of mice returned.</param>
    /// <returns>a 0 terminated array of mouse instance IDs which should be freed
    /// with <c>SDL_free()</c>, or <c>NULL</c> on error; call <see cref="GetError"/> for more
    /// details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetMouseNameForID"/>
    /// <seealso cref="HasMouse"/>
    public static uint[]? GetMice(out int count)
    {
        var pArray = SDL_GetMice(out count);

        if (pArray == IntPtr.Zero) return null;

        if (count == 0) return [];
        
        try
        {
            var miceArray = new int[count];
            Marshal.Copy(pArray, miceArray, 0, count);
            return Array.ConvertAll(miceArray, item => (uint)item);
        }
        finally
        {
            Free(pArray);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMouseNameForID(uint instanceId);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetMouseNameForID(SDL_MouseID instance_id);</code>
    /// <summary>
    /// <para>Get the name of a mouse.</para>
    /// <para>This function returns "" if the mouse doesn't have a name.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="instanceId">the mouse instance ID.</param>
    /// <returns>the name of the selected mouse, or <c>NULL</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetMice"/>
    public static string? GetMouseNameForID(uint instanceId) =>
        Marshal.PtrToStringAnsi(SDL_GetMouseNameForID(instanceId));

    
    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetMouseFocus(void);</code>
    /// <summary>
    /// Get the window which currently has mouse focus.
    /// </summary>
    /// <returns>the window with mouse focus.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMouseFocus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetMouseFocus();
    

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetMouseState(out float x, out float y);
    /// <code>extern SDL_DECLSPEC SDL_MouseButtonFlags SDLCALL SDL_GetMouseState(float *x, float *y);</code>
    /// <summary>
    /// <para>Retrieve the current state of the mouse.</para>
    /// <para>The current button state is returned as a button bitmask, which can be
    /// tested using the <c>Button(x)</c> macro (where `x` is generally 1 for the
    /// left, 2 for middle, 3 for the right button), and `x` and `y` are set to the
    /// mouse cursor position relative to the focus window. You can pass <c>NULL</c> for
    /// either `x` or `y`.</para>
    /// </summary>
    /// <param name="x">the x coordinate of the mouse cursor position relative to the
    /// focus window.</param>
    /// <param name="y">the y coordinate of the mouse cursor position relative to the
    /// focus window.</param>
    /// <returns>a 32-bit button bitmask of the current button state.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGlobalMouseState"/>
    /// <seealso cref="GetRelativeMouseState"/>
    public static MouseButtonFlags GetMouseState(out float x, out float y) => SDL_GetMouseState(out x, out y);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);
    /// <code>extern SDL_DECLSPEC SDL_MouseButtonFlags SDLCALL SDL_GetGlobalMouseState(float *x, float *y);</code>
    /// <summary>
    /// <para>Get the current state of the mouse in relation to the desktop.</para>
    /// <para>This works similarly to <see cref="GetMouseState"/>, but the coordinates will be
    /// reported relative to the top-left of the desktop. This can be useful if you
    /// need to track the mouse outside of a specific window and <see cref="CaptureMouse"/>
    /// doesn't fit your needs. For example, it could be useful if you need to
    /// track the mouse while dragging a window, where coordinates relative to a
    /// window might not be in sync at all times.</para>
    /// <para>Note: <see cref="GetMouseState"/> returns the mouse position as SDL understands it
    /// from the last pump of the event queue. This function, however, queries the
    /// OS for the current mouse position, and as such, might be a slightly less
    /// efficient function. Unless you know what you're doing and have a good
    /// reason to use this function, you probably want <see cref="GetMouseState"/> instead.</para>
    /// </summary>
    /// <param name="x">filled in with the current X coord relative to the desktop; can be
    /// <c>NULL</c>.</param>
    /// <param name="y">filled in with the current Y coord relative to the desktop; can be
    /// <c>NULL</c>.</param>
    /// <returns>the current button state as a bitmask which can be tested using
    /// the <c>Button(x)</c> macros.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CaptureMouse"/>
    /// <seealso cref="GetMouseState"/>
    public static MouseButtonFlags GetGlobalMouseState(out float x, out float y) => SDL_GetGlobalMouseState(out x, out y);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);
    /// <code>extern SDL_DECLSPEC SDL_MouseButtonFlags SDLCALL SDL_GetRelativeMouseState(float *x, float *y);</code>
    /// <summary>
    /// Retrieve the relative state of the mouse.
    /// </summary>
    /// <para>The current button state is returned as a button bitmask, which can be
    /// tested using the <c>Button(x)</c> macros (where `x` is generally 1 for the
    /// left, 2 for middle, 3 for the right button), and `x` and `y` are set to the
    /// mouse deltas since the last call to <see cref="GetRelativeMouseState"/> or since
    /// event initialization. You can pass <c>NULL</c> for either `x` or `y`.</para>
    /// <param name="x">a pointer filled with the last recorded x coordinate of the mouse.</param>
    /// <param name="y">a pointer filled with the last recorded y coordinate of the mouse.</param>
    /// <returns>a 32-bit button bitmask of the relative button state.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetMouseState"/>
    public static MouseButtonFlags GetRelativeMouseState(out float x, out float y) => SDL_GetRelativeMouseState(out x, out y);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_WarpMouseInWindow(SDL_Window *window, float x, float y);</code>
    /// <summary>
    /// Move the mouse cursor to the given position within the window.
    /// </summary>
    /// <remarks>
    /// This function generates a mouse motion event if relative mode is not
    /// enabled. If relative mode is enabled, you can force mouse events for the
    /// warp by setting the <see cref="Hints.MouseRelativeWarpMotion"/> hint.
    /// Note that this function will appear to succeed, but not actually move the
    /// mouse when used over Microsoft Remote Desktop.
    /// </remarks>
    /// <param name="window">The window to move the mouse into, or <c>NULL</c> for the current mouse focus.</param>
    /// <param name="x">The x coordinate within the window.</param>
    /// <param name="y">The y coordinate within the window.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="WarpMouseGlobal"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WarpMouseInWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WarpMouseInWindow(IntPtr window, float x, float y);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_WarpMouseGlobal(float x, float y);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_WarpMouseGlobal(float x, float y);</code>
    /// <summary>
    /// Move the mouse to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// This function generates a mouse motion event. A failure of this function
    /// usually means that it is unsupported by a platform.
    /// Note that this function will appear to succeed, but not actually move the
    /// mouse when used over Microsoft Remote Desktop.
    /// </remarks>
    /// <param name="x">The x coordinate in global screen space.</param>
    /// <param name="y">The y coordinate in global screen space.</param>
    /// <returns>0 on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="WarpMouseInWindow"/>
    public static int WarpMouseGlobal(float x, float y) => SDL_WarpMouseGlobal(x, y);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetRelativeMouseMode([MarshalAs(SDLBool)] bool enabled);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetRelativeMouseMode(SDL_bool enabled);</code>
    /// <summary>
    /// <para>Set relative mouse mode.</para>
    /// <para>While the mouse is in relative mode, the cursor is hidden, the mouse
    /// position is constrained to the window, and SDL will report continuous
    /// relative mouse motion even if the mouse is at the edge of the window.</para>
    /// <para>This function will flush any pending mouse motion.</para>
    /// </summary>
    /// <param name="enabled"><c>true</c> to enable relative mode, <c>false</c> to disable.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRelativeMouseMode"/>
    public static int SetRelativeMouseMode(bool enabled) => SDL_SetRelativeMouseMode(enabled);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CaptureMouse([MarshalAs(SDLBool)] bool enabled);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CaptureMouse(SDL_bool enabled);</code>
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
    /// probably use <see cref="SetRelativeMouseMode"/> or <see cref="SetWindowMouseGrab"/>,
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
    /// <see cref="WindowFlags.MouseCapture"/> hint to zero.</para>
    /// </summary>
    /// <param name="enabled"><c>true</c> to enable capturing, <c>false</c> to disable.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError()"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGlobalMouseState"/>
    public static int CaptureMouse(bool enabled) => SDL_CaptureMouse(enabled);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GetRelativeMouseMode();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetRelativeMouseMode(void);</code>
    /// <summary>
    /// <para>Query whether relative mouse mode is enabled.</para>
    /// </summary>
    /// <returns><c>true</c> if relative mode is enabled or <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetRelativeMouseMode"/>
    public static bool GetRelativeMouseMode() => SDL_GetRelativeMouseMode();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateCursor(byte[] data,  byte[] mask, int w, int h, int hotX, int hotY);
    /// <code>extern SDL_DECLSPEC SDL_Cursor *SDLCALL SDL_CreateCursor(const Uint8 * data, const Uint8 * mask, int w, int h, int hot_x, int hot_y);</code>
    /// <summary>
    /// <para>Create a cursor using the specified bitmap data and mask (in MSB format).</para>
    /// <para><c>mask</c> has to be in MSB (Most Significant Bit) format.</para>
    /// <para>The cursor width (<c>w</c>) must be a multiple of 8 bits.</para>
    /// <para>The cursor is created in black and white according to the following:</para>
    /// <list type="bullet">
    /// <item><c>data=0, mask=1</c>: white</item>
    /// <item><c>data=1, mask=1</c>: black</item>
    /// <item><c>data=0, mask=0</c>: transparent</item>
    /// <item><c>data=1, mask=0</c>: inverted color if possible, black if not.</item>
    /// </list>
    /// <para>Cursors created with this function must be freed with <see cref="DestroyCursor"/>.</para>
    /// <para>If you want to have a color cursor, or create your cursor from an
    /// <see cref="Surface"/>, you should use <see cref="CreateColorCursor"/>. Alternately, you can
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
    /// <returns>a new cursor with the specified parameters on success or <c>NULL</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    public static Cursor? CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY)
    {
        var pArray = SDL_CreateCursor(data, mask, w, h, hotX, hotY);

        return pArray == IntPtr.Zero ? null : new Cursor(pArray);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateColorCursor(IntPtr surface, int hotX, int hotY);
    /// <code>extern SDL_DECLSPEC SDL_Cursor *SDLCALL SDL_CreateColorCursor(SDL_Surface *surface, int hot_x, int hot_y);</code>
    /// <summary>
    /// Create a color cursor.
    /// </summary>
    /// <param name="surface">an <see cref="Surface"/> structure representing the cursor image.</param>
    /// <param name="hotX">the x position of the cursor hot spot.</param>
    /// <param name="hotY">the y position of the cursor hot spot.</param>
    /// <returns>the new cursor on success or <c>null</c> on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreateCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    /// <seealso cref="DestroyCursor"/>
    /// <seealso cref="SetCursor"/>
    public static Cursor? CreateColorCursor(Surface surface, int hotX, int hotY)
    {
        var pArray = SDL_CreateColorCursor(surface.Handle, hotX, hotY);
        return pArray == IntPtr.Zero ? null : new Cursor(pArray);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateSystemCursor(SystemCursor id);
    /// <code>extern SDL_DECLSPEC SDL_Cursor *SDLCALL SDL_CreateSystemCursor(SDL_SystemCursor id);</code>
    /// <summary>
    /// Create a system cursor.
    /// </summary>
    /// <param name="id">an <see cref="SystemCursor"/> enum value.</param>
    /// <returns>a cursor on success or <c>null</c> on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DestroyCursor"/>
    public static Cursor? CreateSystemCursor(SystemCursor id)
    {
        var pArray = SDL_CreateSystemCursor(id);
        return pArray == IntPtr.Zero ? null : new Cursor(pArray);
    }


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetCursor(IntPtr cursor);

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetCursor(SDL_Cursor * cursor);</code>
    /// <summary>
    /// Set the active cursor.
    /// </summary>
    /// <para>This function sets the currently active cursor to the specified one. If the
    /// cursor is currently visible, the change will be immediately represented on the display.
    /// <c>SetCursor(null)</c> can be used to force cursor redraw, if this is desired for any reason.</para>
    /// <param name="cursor">a cursor to make active.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetCursor"/>
    public static int SetCursor(Cursor? cursor)
    {
        return cursor != null ? SDL_SetCursor(cursor.Handle) : SDL_SetCursor(IntPtr.Zero);
    }


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCursor();
    /// <code>extern SDL_DECLSPEC SDL_Cursor *SDLCALL SDL_GetCursor(void);</code>
    /// <summary>
    /// Get the active cursor.
    /// </summary>
    /// <para>This function returns a pointer to the current cursor which is owned by the library.
    /// It is not necessary to free the cursor with <see cref="DestroyCursor"/>.</para>
    /// <returns>the active cursor or <c>null</c> if there is no mouse.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetCursor"/>
    public static Cursor? GetCursor()
    {
        var pArray = SDL_GetCursor();
        return pArray == IntPtr.Zero ? null : new Cursor(pArray);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDefaultCursor();
    /// <code>extern SDL_DECLSPEC SDL_Cursor *SDLCALL SDL_GetDefaultCursor(void);</code>
    /// <summary>
    /// Get the default cursor.
    /// </summary>
    /// <para>You do not have to call <see cref="DestroyCursor"/> on the return value, but it is
    /// safe to do so.</para>
    /// <returns>the default cursor on success or <c>null</c> on failure.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Cursor? GetDefaultCursor()
    {
        var pArray = SDL_GetDefaultCursor();
        
        return pArray == IntPtr.Zero ? null : new Cursor(pArray);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyCursor(IntPtr cursor);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyCursor(SDL_Cursor * cursor);</code>
    /// <summary>
    /// Free a previously-created cursor.
    /// </summary>
    /// <para>Use this function to free cursor resources created with <see cref="CreateCursor"/>,
    /// <see cref="CreateColorCursor"/> or <see cref="CreateSystemCursor"/>.</para>
    /// <param name="cursor">the cursor to free.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreateColorCursor"/>
    /// <seealso cref="CreateCursor"/>
    /// <seealso cref="CreateSystemCursor"/>
    public static void DestroyCursor(Cursor cursor) => SDL_DestroyCursor(cursor.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ShowCursor();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ShowCursor(void);</code>
    /// <summary>
    /// Show the cursor.
    /// </summary>
    /// <returns><c>0</c> on success or a negative error code on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CursorVisible"/>
    /// <seealso cref="HideCursor"/>
    public static int ShowCursor() => SDL_ShowCursor();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_HideCursor();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_HideCursor(void);</code>
    /// <summary>
    /// Hide the cursor.
    /// </summary>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CursorVisible"/>
    /// <seealso cref="ShowCursor"/>
    public static int HideCursor() => SDL_HideCursor();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_CursorVisible();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_CursorVisible(void);</code>
    /// <summary>
    /// Return whether the cursor is currently being shown.
    /// </summary>
    /// <returns><c>true</c> if the cursor is being shown, or <c>false</c> if the cursor is hidden.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HideCursor"/>
    /// <seealso cref="ShowCursor"/>
    public static bool CursorVisible() => SDL_CursorVisible();
}