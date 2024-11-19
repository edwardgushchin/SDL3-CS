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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasKeyboard();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasKeyboard(void);</code>
    /// <summary>
    /// Return whether a keyboard is currently connected.
    /// </summary>
    /// <returns><c>true</c> if a keyboard is connected, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyboards"/>
    public static bool HasKeyboard() => SDL_HasKeyboard();
    

    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboards(out int count);
    /// <code>extern SDL_DECLSPEC SDL_KeyboardID *SDLCALL SDL_GetKeyboards(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently connected keyboards.</para>
    /// <para>Note that this will include any device or virtual driver that includes
    /// keyboard functionality, including some mice, KVM switches, motherboard
    /// power buttons, etc. You should wait for input from a device before you
    /// consider it actively in use.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of keyboards returned.</param>
    /// <returns>a 0 terminated array of keyboards instance IDs which should be
    /// freed with SDL_free, or NULL on error; call <see cref="GetError"/> for
    /// more details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyboardNameForID"/>
    /// <seealso cref="HasKeyboard"/>
    public static uint[]? GetKeyboards(out int count)
    {
        var pArray = SDL_GetKeyboards(out count);

        if (pArray == IntPtr.Zero) return null;
        
        if (count == 0) return [];
        
        try
        {
            var keyboardArray = new int[count];
            Marshal.Copy(pArray, keyboardArray, 0, count);
            return Array.ConvertAll(keyboardArray, item => (uint)item);
        }
        finally
        {
            Free(pArray);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static partial string? SDL_GetKeyboardNameForID(uint instanceId);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetKeyboardNameForID(SDL_KeyboardID instance_id);</code>
    /// <summary>
    /// Get the name of a keyboard.
    /// </summary>
    /// <para>This function returns "" if the keyboard doesn't have a name.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// <param name="instanceId">the keyboard instance ID.</param>
    /// <returns>the name instanceId the selected keyboard, or NULL on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyboards"/>
    public static string? GetKeyboardNameForID(uint instanceId) => SDL_GetKeyboardNameForID(instanceId);
    
    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetKeyboardFocus(void);</code>
    /// <summary>
    /// Query the window which currently has keyboard focus.
    /// </summary>
    /// <returns>the window with keyboard focus.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardFocus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetKeyboardFocus();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardState(out int numkeys);
    /// <code>extern SDL_DECLSPEC const Uint8 *SDLCALL SDL_GetKeyboardState(int *numkeys);</code>
    /// <summary>
    /// <para>Get a snapshot of the current state of the keyboard.</para>
    /// <para>The pointer returned is a pointer to an internal SDL array. It will be
    /// valid for the whole lifetime of the application and should not be freed by
    /// the caller.</para>
    /// <para>An array element with a value of 1 means that the key is pressed and a value
    /// of 0 means that it is not. Indexes into this array are obtained by using
    /// SDL_Scancode values.</para>
    /// <para>Use <see cref="PumpEvents"/> to update the state array.</para>
    /// <para>This function gives you the current state after all events have been
    /// processed, so if a key or button has been pressed and released before you
    /// process events, then the pressed state will never show up in the
    /// <see cref="GetKeyboardState"/> calls.</para>
    /// <para>Note: This function doesn't take into account whether shift has been
    /// pressed or not.</para>
    /// </summary>
    /// <param name="numkeys">if non-NULL, receives the length of the returned array.</param>
    /// <returns>a pointer to an array of key states.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="ResetKeyboard"/>
    public static byte[] GetKeyboardState(out int numkeys)
    {
        var pArray = SDL_GetKeyboardState(out numkeys);
        try
        {
            var keyboardState = new byte[numkeys];
            Marshal.Copy(pArray, keyboardState, 0, numkeys);
            return keyboardState;
        }
        finally
        {
            Free(pArray);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetKeyboard();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetKeyboard(void);</code>
    /// <summary>
    /// Clear the state of the keyboard.
    /// </summary>
    /// <para>This function will generate key up events for all pressed keys.</para>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyboardState"/>
    public static void ResetKeyboard() => SDL_ResetKeyboard();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keymod SDL_GetModState();
    /// <code>extern SDL_DECLSPEC SDL_Keymod SDLCALL SDL_GetModState(void);</code>
    /// <summary>
    /// Get the current key modifier state for the keyboard.
    /// </summary>
    /// <returns>an OR'd combination of the modifier keys for the keyboard. See
    /// <see cref="Keymod"/> for details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyboardState"/>
    /// <seealso cref="SetModState"/>
    public static Keymod GetModState() => SDL_GetModState();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(Keymod modstate);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetModState(SDL_Keymod modstate);</code>
    /// <summary>
    /// Set the current key modifier state for the keyboard.
    /// </summary>
    /// <para>The inverse of <see cref="GetModState"/>, <see cref="SetModState"/> allows you to impose
    /// modifier key states on your application. Simply pass your desired modifier
    /// states into <c>modstate</c>. This value may be a bitwise OR'd combination of
    /// <see cref="Keymod"/> values.</para>
    /// <para>This does not change the keyboard state, only the key modifier flags that
    /// SDL reports.</para>
    /// <param name="modstate">the desired <see cref="Keymod"/> for the keyboard.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetModState"/>
    public static void SetModState(Keymod modstate) => SDL_SetModState(modstate);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetDefaultKeyFromScancode(Scancode scancode, Keymod modstate);
    /// <code>extern SDL_DECLSPEC SDL_Keycode SDLCALL SDL_GetDefaultKeyFromScancode(SDL_Scancode scancode, SDL_Keymod modstate);</code>
    /// <summary>
    /// Get the key code corresponding to the given scancode according to a default
    /// en_US keyboard layout.
    /// </summary>
    /// <para>See <see cref="Keycode"/> for details.</para>
    /// <param name="scancode">the desired <see cref="Scancode"/> to query.</param>
    /// <param name="modstate">the modifier state to use when translating the scancode to
    /// a keycode.</param>
    /// <returns>the <see cref="Keycode"/> that corresponds to the given <see cref="Scancode"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    public static Keycode GetDefaultKeyFromScancode(Scancode scancode, Keymod modstate) => 
        SDL_GetDefaultKeyFromScancode(scancode, modstate);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetKeyFromScancode(Scancode scancode, Keymod modstate);
    /// <code>extern SDL_DECLSPEC SDL_Keycode SDLCALL SDL_GetKeyFromScancode(SDL_Scancode scancode, SDL_Keymod modstate);</code>
    /// <summary>
    /// Get the key code corresponding to the given scancode according to the
    /// current keyboard layout.
    /// </summary>
    /// <para>See <see cref="Keycode"/> for details.</para>
    /// <param name="scancode">the desired <see cref="Scancode"/> to query.</param>
    /// <param name="modstate">the modifier state to use when translating the scancode to
    /// a keycode.</param>
    /// <returns>the <see cref="Keycode"/> that corresponds to the given <see cref="Scancode"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetDefaultKeyFromScancode"/>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    public static Keycode GetKeyFromScancode(Scancode scancode, Keymod modstate) => 
        SDL_GetKeyFromScancode(scancode, modstate);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetDefaultScancodeFromKey(Keycode key, out Keymod modstate);
    /// <code>extern SDL_DECLSPEC SDL_Scancode SDLCALL SDL_GetDefaultScancodeFromKey(SDL_Keycode key, SDL_Keymod *modstate);</code>
    /// <summary>
    /// Get the scancode corresponding to the given key code according to a default
    /// en_US keyboard layout.
    /// </summary>
    /// <para>Note that there may be multiple scancode+modifier states that can generate
    /// this keycode, this will just return the first one found.</para>
    /// <param name="key">the desired <see cref="Keycode"/> to query.</param>
    /// <param name="modstate">a pointer to the modifier state that would be used when the
    /// scancode generates this key, may be NULL.</param>
    /// <returns>the <see cref="Scancode"/> that corresponds to the given <see cref="Keycode"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeName"/>
    public static Scancode GetDefaultScancodeFromKey(Keycode key, out Keymod modstate) => 
        SDL_GetDefaultScancodeFromKey(key, out modstate);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromKey(Keycode key, out Keymod modstate);
    /// <code>extern SDL_DECLSPEC SDL_Scancode SDLCALL SDL_GetScancodeFromKey(SDL_Keycode key, SDL_Keymod *modstate);</code>
    /// <summary>
    /// Get the scancode corresponding to the given key code according to the
    /// current keyboard layout.
    /// </summary>
    /// <para>Note that there may be multiple scancode+modifier states that can generate
    /// this keycode, this will just return the first one found.</para>
    /// <param name="key">the desired <see cref="Keycode"/> to query.</param>
    /// <param name="modstate">a pointer to the modifier state that would be used when the
    /// scancode generates this key, may be NULL.</param>
    /// <returns>the <see cref="Scancode"/> that corresponds to the given <see cref="Keycode"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetDefaultScancodeFromKey"/>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetScancodeName"/>
    public static Scancode GetScancodeFromKey(Keycode key, out Keymod modstate) => 
        SDL_GetScancodeFromKey(key, out modstate);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetScancodeName(Scancode scancode, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetScancodeName(SDL_Scancode scancode, const char *name);</code>
    /// <summary>
    /// Set a human-readable name for a scancode.
    /// </summary>
    /// <param name="scancode">the desired <see cref="Scancode"/>.</param>
    /// <param name="name">the name to use for the scancode, encoded as UTF-8. The string
    /// is not copied, so the pointer given to this function must stay
    /// valid while SDL is being used.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetScancodeName"/>
    public static int SetScancodeName(Scancode scancode, string name) => SDL_SetScancodeName(scancode, name);
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetScancodeName(Scancode scancode);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetScancodeName(SDL_Scancode scancode);</code>
    /// <summary>
    /// <para>Get a human-readable name for a scancode.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// <para><b>Warning</b>: The returned name is by design not stable across platforms,
    /// e.g. the name for <see cref="Scancode.LGUI"/> is "Left GUI" under Linux but "Left
    /// Windows" under Microsoft Windows, and some scancodes like
    /// <see cref="Scancode.NonUsBackSlash"/> don't have any name at all. There are even
    /// scancodes that share names, e.g. <see cref="Scancode.Return"/> and
    /// <see cref="Scancode.Return2"/> (both called "Return"). This function is therefore
    /// unsuitable for creating a stable cross-platform two-way mapping between
    /// strings and scancodes.</para>
    /// </summary>
    /// <param name="scancode">the desired <see cref="Scancode"/> to query.</param>
    /// <returns>a pointer to the name for the scancode. If the scancode doesn't
    /// <c>have</c> a name this function returns an empty string ("").</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeFromName"/>
    /// <seealso cref="SetScancodeName"/>
    public static string GetScancodeName(Scancode scancode) => Marshal.PtrToStringUTF8(SDL_GetScancodeName(scancode))!;
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC SDL_Scancode SDLCALL SDL_GetScancodeFromName(const char *name);</code>
    /// <summary>
    /// <para>Get a scancode from a human-readable name.</para>
    /// </summary>
    /// <param name="name">the human-readable scancode name.</param>
    /// <returns>the <see cref="Scancode"/>, or <see cref="Scancode.Unknown"/> if the name wasn't
    /// recognized; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyFromName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeName"/>
    public static Scancode GetScancodeFromName(string name) => SDL_GetScancodeFromName(name);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyName(Keycode key);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetKeyName(SDL_Keycode key);</code>
    /// <summary>
    /// <para>Get a human-readable name for a key.</para>
    /// <para>Both lowercase and uppercase alphabetic keycodes have uppercase names, e.g.
    /// <see cref="Keycode"/> 'a' and 'A' both have the name "A".</para>
    /// <para>If the key doesn't have a name, this function returns an empty string ("").</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="key">the desired <see cref="Keycode"/> to query.</param>
    /// <returns>a UTF-8 encoded string of the key name.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyFromName"/>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetScancodeFromKey"/>
    public static string GetKeyName(Keycode key) => Marshal.PtrToStringUTF8(SDL_GetKeyName(key))!;
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetKeyFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC SDL_Keycode SDLCALL SDL_GetKeyFromName(const char *name);</code>
    /// <summary>
    /// <para>Get a key code from a human-readable name.</para>
    /// </summary>
    /// <param name="name">the human-readable key name.</param>
    /// <returns>key code, or <see cref="Keycode.Unknown"/> if the name wasn't recognized; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromName"/>
    public static Keycode GetKeyFromName(string name) => SDL_GetKeyFromName(name);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_StartTextInput(SDL_Window *window);</code>
    /// <summary>
    /// <para>Start accepting Unicode text input events in a window.</para>
    /// <para>This function will enable text input (<see cref="EventType.TextInput"/> and
    /// <see cref="EventType.TextEditing"/> events) in the specified window. Please use this
    /// function paired with <see cref="StopTextInput"/>.</para>
    /// <para>Text input events are not received by default.</para>
    /// <para>On some platforms using this function shows the screen keyboard.</para>
    /// </summary>
    /// <param name="window">the window to enable text input.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetTextInputArea"/>
    /// <seealso cref="StopTextInput"/>
    /// <seealso cref="TextInputActive"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StartTextInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int StartTextInput(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_TextInputActive(SDL_Window *window);</code>
    /// <summary>
    /// <para>Check whether or not Unicode text input events are enabled for a window.</para>
    /// </summary>
    /// <param name="window">the window to check.</param>
    /// <returns><c>true</c> if text input events are enabled else <c>false</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TextInputActive"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    public static partial bool TextInputActive(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_StopTextInput(SDL_Window *window);</code>
    /// <summary>
    /// <para>Stop receiving any text input events in a window.</para>
    /// <para>If <see cref="StartTextInput"/> showed the screen keyboard, this function will hide
    /// it.</para>
    /// </summary>
    /// <param name="window">the window to disable text input.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StopTextInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int StopTextInput(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ClearComposition(SDL_Window *window);</code>
    /// <summary>
    /// <para>Dismiss the composition window/IME without disabling the subsystem.</para>
    /// </summary>
    /// <param name="window">the window to affect.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="StopTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearComposition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ClearComposition(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetTextInputArea(SDL_Window *window, const SDL_Rect *rect, int cursor);</code>
    /// <summary>
    /// <para>Set the area used to type Unicode text input.</para>
    /// <para>Native input methods may place a window with word suggestions near the
    /// cursor, without covering the text being entered.</para>
    /// </summary>
    /// <param name="window">the window for which to set the text input area.</param>
    /// <param name="rect">the <see cref="Rect"/> representing the text input area, in window
    /// coordinates, or <c>NULL</c> to clear it.</param>
    /// <param name="cursor">the offset of the current cursor location relative to
    /// <c>rect->x</c>, in window coordinates.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetTextInputArea"/>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetTextInputArea(IntPtr window, in Rect rect, int cursor);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetTextInputArea(SDL_Window *window, SDL_Rect *rect, int *cursor);</code>
    /// <summary>
    /// <para>Get the area used to type Unicode text input.</para>
    /// <para>This returns the values previously set by <see cref="SetTextInputArea"/>.</para>
    /// </summary>
    /// <param name="window">the window for which to query the text input area.</param>
    /// <param name="rect">a pointer to an <see cref="Rect"/> filled in with the text input area,
    /// may be <c>NULL</c>.</param>
    /// <param name="cursor">a pointer to the offset of the current cursor location
    /// relative to <c>rect->x</c>, may be <c>NULL</c>.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetTextInputArea"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetTextInputArea(IntPtr window, out Rect rect, out int cursor);
    
    
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasScreenKeyboardSupport(void);</code>
    /// <summary>
    /// <para>Check whether the platform has screen keyboard support.</para>
    /// </summary>
    /// <returns><c>true</c> if the platform has some screen keyboard support or
    /// <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="ScreenKeyboardShown"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasScreenKeyboardSupport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    public static partial bool HasScreenKeyboardSupport();
    
    
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ScreenKeyboardShown(SDL_Window *window);</code>
    /// <summary>
    /// <para>Check whether the screen keyboard is shown for given window.</para>
    /// </summary>
    /// <param name="window">the window for which screen keyboard should be queried.</param>
    /// <returns><c>true</c> if screen keyboard is shown or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasScreenKeyboardSupport"/>
    [LibraryImport(SDLLibrary, EntryPoint = "ScreenKeyboardShown"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    public static partial bool ScreenKeyboardShown(IntPtr window);
    
}
