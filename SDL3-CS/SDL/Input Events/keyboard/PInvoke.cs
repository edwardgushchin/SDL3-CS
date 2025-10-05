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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasKeyboard(void);</code>
    /// <summary>
    /// Return whether a keyboard is currently connected.
    /// </summary>
    /// <returns><c>true</c> if a keyboard is connected, <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboards"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasKeyboard"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasKeyboard();
    

    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboards"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboards(out int count);
    /// <code>extern SDL_DECLSPEC SDL_KeyboardID * SDLCALL SDL_GetKeyboards(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently connected keyboards.</para>
    /// <para>Note that this will include any device or virtual driver that includes
    /// keyboard functionality, including some mice, KVM switches, motherboard
    /// power buttons, etc. You should wait for input from a device before you
    /// consider it actively in use.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of keyboards returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of keyboards instance IDs or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboardNameForID"/>
    /// <seealso cref="HasKeyboard"/>
    public static uint[]? GetKeyboards(out int count)
    {
        var ptr = SDL_GetKeyboards(out count);

        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardNameForID(uint instanceId);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetKeyboardNameForID(SDL_KeyboardID instance_id);</code>
    /// <summary>
    /// <para>Get the name of a keyboard.</para>
    /// <para>This function returns "" if the keyboard doesn't have a name.</para>
    /// </summary>
    /// <param name="instanceId">the keyboard instance ID.</param>
    /// <returns>the name of the selected keyboard or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboards"/>
    public static string? GetKeyboardNameForID(uint instanceId)
    {
        var value = SDL_GetKeyboardNameForID(instanceId); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }

    
    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetKeyboardFocus(void);</code>
    /// <summary>
    /// Query the window which currently has keyboard focus.
    /// </summary>
    /// <returns>the window with keyboard focus.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardFocus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetKeyboardFocus();
    
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardState(out int numkeys);

    /// <code>extern SDL_DECLSPEC const bool * SDLCALL SDL_GetKeyboardState(int *numkeys);</code>
    /// <summary>
    /// <para>Get a snapshot of the current state of the keyboard.</para>
    /// <para>The pointer returned is a pointer to an internal SDL array. It will be
    /// valid for the whole lifetime of the application and should not be freed by
    /// the caller.</para>
    /// <para>A array element with a value of true means that the key is pressed and a
    /// value of false means that it is not. Indexes into this array are obtained
    /// by using <see cref="Scancode"/> values.</para>
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
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="ResetKeyboard"/> 
    public static ReadOnlySpan<bool> GetKeyboardState(out int numkeys)
    {
        var statePtr = SDL_GetKeyboardState(out numkeys);
        unsafe
        {
            return MemoryMarshal.Cast<byte, bool>(new ReadOnlySpan<byte>((void*)statePtr, numkeys));
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetKeyboard(void);</code>
    /// <summary>
    /// <para>Clear the state of the keyboard.</para>
    /// <para>This function will generate key up events for all pressed keys.</para>
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboardState"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResetKeyboard"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetKeyboard();
    
    
    /// <code>extern SDL_DECLSPEC SDL_Keymod SDLCALL SDL_GetModState(void);</code>
    /// <summary>
    /// Get the current key modifier state for the keyboard.
    /// </summary>
    /// <returns>an OR'd combination of the modifier keys for the keyboard.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboardState"/>
    /// <seealso cref="SetModState"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetModState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Keymod GetModState();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetModState(SDL_Keymod modstate);</code>
    /// <summary>
    /// <para>Set the current key modifier state for the keyboard.</para>
    /// <para>The inverse of <see cref="GetModState"/>, <see cref="SetModState"/> allows you to impose
    /// modifier key states on your application. Simply pass your desired modifier
    /// states into <c>modstate</c>. This value may be a bitwise, OR'd combination of
    /// <see cref="Keymod"/> values.</para>
    /// <para>This does not change the keyboard state, only the key modifier flags that
    /// SDL reports.</para>
    /// </summary>
    /// <param name="modstate">the desired <see cref="Keymod"/> for the keyboard.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetModState"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetModState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetModState(Keymod modstate);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Keycode SDLCALL SDL_GetKeyFromScancode(SDL_Scancode scancode, SDL_Keymod modstate, bool key_event);</code>
    /// <summary>
    /// <para>Get the key code corresponding to the given scancode according to the
    /// current keyboard layout.</para>
    /// <para>If you want to get the keycode as it would be delivered in key events,
    /// including options specified in <see cref="Hints.KeycodeOptions"/>, then you should
    /// pass <c>keyEvent</c> as true. Otherwise this function simply translates the
    /// scancode based on the given modifier state.</para>
    /// </summary>
    /// <param name="scancode">the desired <see cref="Scancode"/> to query.</param>
    /// <param name="modstate">the modifier state to use when translating the scancode to
    /// a keycode.</param>
    /// <param name="keyEvent"><c>true</c> if the keycode will be used in key events.</param>
    /// <returns>the <see cref="Keycode"/> that corresponds to the given <see cref="Scancode"/>.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyFromScancode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Keycode GetKeyFromScancode(Scancode scancode, Keymod modstate, [MarshalAs(UnmanagedType.I1)] bool keyEvent);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Scancode SDLCALL SDL_GetScancodeFromKey(SDL_Keycode key, SDL_Keymod *modstate);</code>
    /// <summary>
    /// <para>Get the scancode corresponding to the given key code according to the
    /// current keyboard layout.</para>
    /// <para>Note that there may be multiple scancode+modifier states that can generate
    /// this keycode, this will just return the first one found.</para>
    /// </summary>
    /// <param name="key">the desired <see cref="Keycode"/> to query.</param>
    /// <param name="modstate">a pointer to the modifier state that would be used when the
    /// scancode generates this key, may be <c>null</c>.</param>
    /// <returns>the <see cref="Scancode"/> that corresponds to the given <see cref="Keycode"/>.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetScancodeName"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetScancodeFromKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Scancode GetScancodeFromKey(Keycode key, out Keymod modstate);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetScancodeName(SDL_Scancode scancode, const char *name);</code>
    /// <summary>
    /// Set a human-readable name for a scancode.
    /// </summary>
    /// <param name="scancode">the desired <see cref="Scancode"/>.</param>
    /// <param name="name">the name to use for the scancode, encoded as UTF-8. The string
    /// is not copied, so the pointer given to this function must stay
    /// valid while SDL is being used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetScancodeName"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetScancodeName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetScancodeName(Scancode scancode, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetScancodeName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetScancodeName(Scancode scancode);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetScancodeName(SDL_Scancode scancode);</code>
    /// <summary>
    /// <para>Get a human-readable name for a scancode.</para>
    /// <para><b>Warning</b>: The returned name is by design not stable across platforms,
    /// e.g. the name for <see cref="Scancode.LGUI"/> is "Left GUI" under Linux but "Left
    /// Windows" under Microsoft Windows, and some scancodes like
    /// <see cref="Scancode.NonUsbackslash"/> don't have any name at all. There are even
    /// scancodes that share names, e.g. <see cref="Scancode.Return"/> and
    /// <see cref="Scancode.Return2"/> (both called "Return"). This function is therefore
    /// unsuitable for creating a stable cross-platform two-way mapping between
    /// strings and scancodes.</para>
    /// </summary>
    /// <param name="scancode">the desired SDL_Scancode to query.</param>
    /// <returns>a pointer to the name for the scancode. If the scancode doesn't
    /// have a name this function returns an empty string ("").</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeFromName"/>
    /// <seealso cref="SetScancodeName"/>
    public static string GetScancodeName(Scancode scancode)
    {
        var value = SDL_GetScancodeName(scancode); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_Scancode SDLCALL SDL_GetScancodeFromName(const char *name);</code>
    /// <summary>
    /// Get a scancode from a human-readable name.
    /// </summary>
    /// <param name="name">the human-readable scancode name.</param>
    /// <returns>the <see cref="Scancode"/>, or <see cref="Scancode.Unknown"/> if the name wasn't
    /// recognized; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyFromName"/>
    /// <seealso cref="GetScancodeFromKey"/>
    /// <seealso cref="GetScancodeName"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetScancodeFromName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Scancode GetScancodeFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyName(Keycode key);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetKeyName(SDL_Keycode key);</code>
    /// <summary>
    /// <para>Get a human-readable name for a key.</para>
    /// <para>If the key doesn't have a name, this function returns an empty string ("").</para>
    /// <para>Letters will be presented in their uppercase form, if applicable.</para>
    /// </summary>
    /// <param name="key">the desired <see cref="Keycode"/> to query.</param>
    /// <returns>a UTF-8 encoded string of the key name.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyFromName"/>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetScancodeFromKey"/>
    public static string GetKeyName(Keycode key)
    {
        var value = SDL_GetKeyName(key); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_Keycode SDLCALL SDL_GetKeyFromName(const char *name);</code>
    /// <summary>
    /// Get a key code from a human-readable name.
    /// </summary>
    /// <param name="name">the human-readable key name.</param>
    /// <returns>key code, or <see cref="Keycode.Unknown"/> if the name wasn't recognized; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyFromScancode"/>
    /// <seealso cref="GetKeyName"/>
    /// <seealso cref="GetScancodeFromName"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyFromName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Keycode GetKeyFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StartTextInput(SDL_Window *window);</code>
    /// <summary>
    /// <para>Start accepting Unicode text input events in a window.</para>
    /// <para>This function will enable text input (<see cref="EventType.TextInput"/> and
    /// <see cref="EventType.TextEditing"/> events) in the specified window. Please use this
    /// function paired with <see cref="StopTextInput"/>.</para>
    /// <para>Text input events are not received by default.</para>
    /// <para>On some platforms using this function shows the screen keyboard and/or
    /// activates an IME, which can prevent some key press events from being passed
    /// through.</para>
    /// </summary>
    /// <param name="window">the window to enable text input.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetTextInputArea(nint, nint, int)"/>
    /// <seealso cref="StartTextInputWithProperties"/>
    /// <seealso cref="StopTextInput"/>
    /// <seealso cref="TextInputActive"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StartTextInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StartTextInput(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StartTextInputWithProperties(SDL_Window *window, SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Start accepting Unicode text input events in a window, with properties
    /// describing the input.</para>
    /// <para>This function will enable text input (<see cref="EventType.TextInput"/> and
    /// <see cref="EventType.TextEditing"/> events) in the specified window. Please use this
    /// function paired with <see cref="StopTextInput"/>.</para>
    /// <para>Text input events are not received by default.</para>
    /// <para>On some platforms using this function shows the screen keyboard and/or
    /// activates an IME, which can prevent some key press events from being passed
    /// through.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextInputTypeNumber"/> - an <see cref="TextInputType"/> value that
    /// describes text being input, defaults to <see cref="TextInputType.Text"/>.</item>
    /// <item><see cref="Props.TextInputCapitalizationNumber"/> - an <see cref="Capitalization"/> value
    /// that describes how text should be capitalized, defaults to
    /// <see cref="Capitalization.Sentences"/> for normal text entry, <see cref="Capitalization.Words"/> for
    /// <see cref="TextInputType.TextName"/>, and <see cref="Capitalization.None"/> for e-mail
    /// addresses, usernames, and passwords.</item>
    /// <item><see cref="Props.TextInputAutoCorrectBoolean"/> - true to enable auto completion
    /// and auto correction, defaults to true.</item>
    /// <item><see cref="Props.TextInputMultilineBoolean"/> - true if multiple lines of text
    /// are allowed. This defaults to true if <see cref="Hints.ReturnKeyHidesIME"/> is
    /// "0" or is not set, and defaults to false if <see cref="Hints.ReturnKeyHidesIME"/>
    /// is "1".</item>
    /// </list>
    /// <para>On Android you can directly specify the input type:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextInputAndroidInputTypeNumber"/> - the text input type to
    /// use, overriding other properties. This is documented at
    /// https://developer.android.com/reference/android/text/InputType</item>
    /// </list>
    /// </summary>
    /// <param name="window">the window to enable text input.</param>
    /// <param name="props">the properties to use.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetTextInputArea(nint, nint, int)"/>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="StopTextInput"/>
    /// <seealso cref="TextInputActive"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StartTextInputWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StartTextInputWithProperties(IntPtr window, uint props);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TextInputActive(SDL_Window *window);</code>
    /// <summary>
    /// <para>Check whether or not Unicode text input events are enabled for a window.</para>
    /// </summary>
    /// <param name="window">the window to check.</param>
    /// <returns><c>true</c> if text input events are enabled else <c>false</c>.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TextInputActive"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TextInputActive(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StopTextInput(SDL_Window *window);</code>
    /// <summary>
    /// <para>Stop receiving any text input events in a window.</para>
    /// <para>If <see cref="StartTextInput"/> showed the screen keyboard, this function will hide
    /// it.</para>
    /// </summary>
    /// <param name="window">the window to disable text input.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StopTextInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopTextInput(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ClearComposition(SDL_Window *window);</code>
    /// <summary>
    /// <para>Dismiss the composition window/IME without disabling the subsystem.</para>
    /// </summary>
    /// <param name="window">the window to affect.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="StopTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearComposition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClearComposition(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextInputArea(SDL_Window *window, const SDL_Rect *rect, int cursor);</code>
    /// <summary>
    /// <para>Set the area used to type Unicode text input.</para>
    /// <para>Native input methods may place a window with word suggestions near the
    /// cursor, without covering the text being entered.</para>
    /// </summary>
    /// <param name="window">the window for which to set the text input area.</param>
    /// <param name="rect">the <see cref="Rect"/> representing the text input area, in window
    /// coordinates, or <c>null</c> to clear it.</param>
    /// <param name="cursor">the offset of the current cursor location relative to
    /// <c>rect.x</c>, in window coordinates.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextInputArea"/>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextInputArea(IntPtr window, IntPtr rect, int cursor);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextInputArea(SDL_Window *window, const SDL_Rect *rect, int cursor);</code>
    /// <summary>
    /// <para>Set the area used to type Unicode text input.</para>
    /// <para>Native input methods may place a window with word suggestions near the
    /// cursor, without covering the text being entered.</para>
    /// </summary>
    /// <param name="window">the window for which to set the text input area.</param>
    /// <param name="rect">the <see cref="Rect"/> representing the text input area, in window
    /// coordinates, or <c>null</c> to clear it.</param>
    /// <param name="cursor">the offset of the current cursor location relative to
    /// <c>rect.x</c>, in window coordinates.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextInputArea"/>
    /// <seealso cref="StartTextInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextInputArea(IntPtr window, in Rect rect, int cursor);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextInputArea(SDL_Window *window, SDL_Rect *rect, int *cursor);</code>
    /// <summary>
    /// <para>Get the area used to type Unicode text input.</para>
    /// <para>This returns the values previously set by <see cref="SetTextInputArea(nint, nint, int)"/>.</para>
    /// </summary>
    /// <param name="window">the window for which to query the text input area.</param>
    /// <param name="rect">a pointer to an <see cref="Rect"/> filled in with the text input area,
    /// may be <c>null</c>.</param>
    /// <param name="cursor">a pointer to the offset of the current cursor location
    /// relative to <c>rect->x</c>, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetTextInputArea(nint, nint, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextInputArea(IntPtr window, out Rect rect, out int cursor);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasScreenKeyboardSupport(void);</code>
    /// <summary>
    /// <para>whether the platform has screen keyboard support.</para>
    /// </summary>
    /// <returns><c>true</c> if the platform has some screen keyboard support or <c>false</c> if
    /// not.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StartTextInput"/>
    /// <seealso cref="ScreenKeyboardShown"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasScreenKeyboardSupport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasScreenKeyboardSupport();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ScreenKeyboardShown(SDL_Window *window);</code>
    /// <summary>
    /// <para>Check whether the screen keyboard is shown for given window.</para>
    /// </summary>
    /// <param name="window">the window for which screen keyboard should be queried.</param>
    /// <returns><c>true</c> if screen keyboard is shown or <c>false</c> if not.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasScreenKeyboardSupport"/>
    [LibraryImport(SDLLibrary, EntryPoint = "ScreenKeyboardShown"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ScreenKeyboardShown(IntPtr window);
}
