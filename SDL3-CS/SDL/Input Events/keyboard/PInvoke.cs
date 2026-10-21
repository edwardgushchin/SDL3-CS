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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasKeyboard"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasKeyboard();
    private delegate bool HasKeyboardNativeDelegate();
    private static HasKeyboardNativeDelegate HasKeyboardNativeFunction = SDL_HasKeyboard;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasKeyboard(void);</code>
    /// <summary>
    /// Return whether a keyboard is currently connected.
    /// </summary>
    /// <returns><c>true</c> if a keyboard is connected, <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboards"/>
    public static bool HasKeyboard()
    {
        return HasKeyboardNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboards"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboards(out int count);
    private delegate IntPtr GetKeyboardsNativeDelegate(out int count);
    private static GetKeyboardsNativeDelegate GetKeyboardsNativeFunction = SDL_GetKeyboards;
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
        var ptr = GetKeyboardsNativeFunction(out count);

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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardNameForID(uint instanceId);
    private delegate IntPtr GetKeyboardNameForIDNativeDelegate(uint instanceId);
    private static GetKeyboardNameForIDNativeDelegate GetKeyboardNameForIDNativeFunction = SDL_GetKeyboardNameForID;
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
        var value = GetKeyboardNameForIDNativeFunction(instanceId);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardFocus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardFocus();
    private delegate IntPtr GetKeyboardFocusNativeDelegate();
    private static GetKeyboardFocusNativeDelegate GetKeyboardFocusNativeFunction = SDL_GetKeyboardFocus;

    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetKeyboardFocus(void);</code>
    /// <summary>
    /// Query the window which currently has keyboard focus.
    /// </summary>
    /// <returns>the window with keyboard focus.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr GetKeyboardFocus()
    {
        return GetKeyboardFocusNativeFunction();
    }



    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyboardState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardState(out int numkeys);
    private delegate IntPtr GetKeyboardStateNativeDelegate(out int numkeys);
    private static GetKeyboardStateNativeDelegate GetKeyboardStateNativeFunction = SDL_GetKeyboardState;

    /// <code>extern SDL_DECLSPEC const bool * SDLCALL SDL_GetKeyboardState(int *numkeys);</code>
    /// <summary>
    /// <para>Get a snapshot of the current state of the keyboard.</para>
    /// <para>The pointer returned is a pointer to an internal SDL array. It will be
    /// valid for the whole lifetime of the application and should not be freed by
    /// the caller.</para>
    /// <para>A array element with a value of <c>true</c> means that the key is pressed and a
    /// value of <c>false</c> means that it is not. Indexes into this array are obtained
    /// by using <see cref="Scancode"/> values.</para>
    /// <para>Use <see cref="PumpEvents"/> to update the state array.</para>
    /// <para>This function gives you the current state after all events have been
    /// processed, so if a key or button has been pressed and released before you
    /// process events, then the pressed state will never show up in the
    /// <see cref="GetKeyboardState"/> calls.</para>
    /// <para>Note: This function doesn't take into account whether shift has been
    /// pressed or not.</para>
    /// </summary>
    /// <param name="numkeys">if non-<c>null</c>, receives the length of the returned array.</param>
    /// <returns>a pointer to an array of key states.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PumpEvents"/>
    /// <seealso cref="ResetKeyboard"/>
    public static ReadOnlySpan<bool> GetKeyboardState(out int numkeys)
    {
        var statePtr = GetKeyboardStateNativeFunction(out numkeys);
        unsafe
        {
            return MemoryMarshal.Cast<byte, bool>(new ReadOnlySpan<byte>((void*)statePtr, numkeys));
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResetKeyboard"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetKeyboard();
    private delegate void ResetKeyboardNativeDelegate();
    private static ResetKeyboardNativeDelegate ResetKeyboardNativeFunction = SDL_ResetKeyboard;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetKeyboard(void);</code>
    /// <summary>
    /// <para>Clear the state of the keyboard.</para>
    /// <para>This function will generate key up events for all pressed keys.</para>
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboardState"/>
    public static void ResetKeyboard()
    {
        ResetKeyboardNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetModState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keymod SDL_GetModState();
    private delegate Keymod GetModStateNativeDelegate();
    private static GetModStateNativeDelegate GetModStateNativeFunction = SDL_GetModState;

    /// <code>extern SDL_DECLSPEC SDL_Keymod SDLCALL SDL_GetModState(void);</code>
    /// <summary>
    /// Get the current key modifier state for the keyboard.
    /// </summary>
    /// <returns>an OR'd combination of the modifier keys for the keyboard.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetKeyboardState"/>
    /// <seealso cref="SetModState"/>
    public static Keymod GetModState()
    {
        return GetModStateNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetModState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(Keymod modstate);
    private delegate void SetModStateNativeDelegate(Keymod modstate);
    private static SetModStateNativeDelegate SetModStateNativeFunction = SDL_SetModState;

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
    public static void SetModState(Keymod modstate)
    {
        SetModStateNativeFunction(modstate);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyFromScancode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetKeyFromScancode(Scancode scancode, Keymod modstate, [MarshalAs(UnmanagedType.I1)] bool keyEvent);
    private delegate Keycode GetKeyFromScancodeNativeDelegate(Scancode scancode, Keymod modstate, bool keyEvent);
    private static GetKeyFromScancodeNativeDelegate GetKeyFromScancodeNativeFunction = SDL_GetKeyFromScancode;

    /// <code>extern SDL_DECLSPEC SDL_Keycode SDLCALL SDL_GetKeyFromScancode(SDL_Scancode scancode, SDL_Keymod modstate, bool key_event);</code>
    /// <summary>
    /// <para>Get the key code corresponding to the given scancode according to the
    /// current keyboard layout.</para>
    /// <para>If you want to get the keycode as it would be delivered in key events,
    /// including options specified in <see cref="Hints.KeycodeOptions"/>, then you should
    /// pass <c>keyEvent</c> as <c>true</c>. Otherwise this function simply translates the
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
    public static Keycode GetKeyFromScancode(Scancode scancode, Keymod modstate, bool keyEvent)
    {
        return GetKeyFromScancodeNativeFunction(scancode, modstate, keyEvent);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetScancodeFromKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromKey(Keycode key, out Keymod modstate);
    private delegate Scancode GetScancodeFromKeyNativeDelegate(Keycode key, out Keymod modstate);
    private static GetScancodeFromKeyNativeDelegate GetScancodeFromKeyNativeFunction = SDL_GetScancodeFromKey;

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
    public static Scancode GetScancodeFromKey(Keycode key, out Keymod modstate)
    {
        return GetScancodeFromKeyNativeFunction(key, out modstate);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetScancodeName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetScancodeName(Scancode scancode, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    private delegate bool SetScancodeNameNativeDelegate(Scancode scancode, string name);
    private static SetScancodeNameNativeDelegate SetScancodeNameNativeFunction = SDL_SetScancodeName;

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
    public static bool SetScancodeName(Scancode scancode, [MarshalAs(UnmanagedType.LPUTF8Str)] string name)
    {
        return SetScancodeNameNativeFunction(scancode, name);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetScancodeName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetScancodeName(Scancode scancode);
    private delegate IntPtr GetScancodeNameNativeDelegate(Scancode scancode);
    private static GetScancodeNameNativeDelegate GetScancodeNameNativeFunction = SDL_GetScancodeName;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetScancodeName(SDL_Scancode scancode);</code>
    /// <summary>
    /// <para>Get a human-readable name for a scancode.</para>
    /// <para><b>Warning</b>: The returned name is by design not stable across platforms,
    /// e.g. the name for <see cref="Scancode.LGUI"/> is "Left GUI" under Linux but "Left
    /// Windows" under Microsoft Windows, and some scancodes like
    /// <see cref="Scancode.NonUsBackSlash"/> don't have any name at all. There are even
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
        var value = GetScancodeNameNativeFunction(scancode);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetScancodeFromName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    private delegate Scancode GetScancodeFromNameNativeDelegate(string name);
    private static GetScancodeFromNameNativeDelegate GetScancodeFromNameNativeFunction = SDL_GetScancodeFromName;

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
    public static Scancode GetScancodeFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name)
    {
        return GetScancodeFromNameNativeFunction(name);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyName(Keycode key);
    private delegate IntPtr GetKeyNameNativeDelegate(Keycode key);
    private static GetKeyNameNativeDelegate GetKeyNameNativeFunction = SDL_GetKeyName;
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
        var value = GetKeyNameNativeFunction(key);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetKeyFromName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetKeyFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    private delegate Keycode GetKeyFromNameNativeDelegate(string name);
    private static GetKeyFromNameNativeDelegate GetKeyFromNameNativeFunction = SDL_GetKeyFromName;

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
    public static Keycode GetKeyFromName([MarshalAs(UnmanagedType.LPUTF8Str)] string name)
    {
        return GetKeyFromNameNativeFunction(name);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StartTextInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_StartTextInput(IntPtr window);
    private delegate bool StartTextInputNativeDelegate(IntPtr window);
    private static StartTextInputNativeDelegate StartTextInputNativeFunction = SDL_StartTextInput;

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
    public static bool StartTextInput(IntPtr window)
    {
        return StartTextInputNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StartTextInputWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_StartTextInputWithProperties(IntPtr window, uint props);
    private delegate bool StartTextInputWithPropertiesNativeDelegate(IntPtr window, uint props);
    private static StartTextInputWithPropertiesNativeDelegate StartTextInputWithPropertiesNativeFunction = SDL_StartTextInputWithProperties;

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
    /// <item><see cref="Props.TextInputAutoCorrectBoolean"/> - <c>true</c> to enable auto completion
    /// and auto correction, defaults to <c>true</c>.</item>
    /// <item><see cref="Props.TextInputMultilineBoolean"/> - <c>true</c> if multiple lines of text
    /// are allowed. This defaults to <c>true</c> if <see cref="Hints.ReturnKeyHidesIME"/> is
    /// "0" or is not set, and defaults to <c>false</c> if <see cref="Hints.ReturnKeyHidesIME"/>
    /// is "1".</item>
    /// <item><see cref="Props.TextInputTitleString"/> - a title for the top of the on-screen
    /// keyboard window, if it has one.</item>
    /// <item><see cref="Props.TextInputPlaceholderString"/> - the placeholder shown before
    /// the user starts typing, when the field is empty.</item>
    /// <item><see cref="Props.TextInputDefaultTextString"/> - text to prefill the text field
    /// with.</item>
    /// <item><see cref="Props.TextInputMaxLengthNumber"/> - maximum length for the text
    /// field, in characters (not bytes).</item>
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
    public static bool StartTextInputWithProperties(IntPtr window, uint props)
    {
        return StartTextInputWithPropertiesNativeFunction(window, props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TextInputActive"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_TextInputActive(IntPtr window);
    private delegate bool TextInputActiveNativeDelegate(IntPtr window);
    private static TextInputActiveNativeDelegate TextInputActiveNativeFunction = SDL_TextInputActive;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TextInputActive(SDL_Window *window);</code>
    /// <summary>
    /// <para>Check whether or not Unicode text input events are enabled for a window.</para>
    /// </summary>
    /// <param name="window">the window to check.</param>
    /// <returns><c>true</c> if text input events are enabled else <c>false</c>.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StartTextInput"/>
    public static bool TextInputActive(IntPtr window)
    {
        return TextInputActiveNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StopTextInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_StopTextInput(IntPtr window);
    private delegate bool StopTextInputNativeDelegate(IntPtr window);
    private static StopTextInputNativeDelegate StopTextInputNativeFunction = SDL_StopTextInput;

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
    public static bool StopTextInput(IntPtr window)
    {
        return StopTextInputNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearComposition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ClearComposition(IntPtr window);
    private delegate bool ClearCompositionNativeDelegate(IntPtr window);
    private static ClearCompositionNativeDelegate ClearCompositionNativeFunction = SDL_ClearComposition;

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
    public static bool ClearComposition(IntPtr window)
    {
        return ClearCompositionNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetTextInputArea(IntPtr window, IntPtr rect, int cursor);
    private delegate bool SetTextInputAreaPointerNativeDelegate(IntPtr window, IntPtr rect, int cursor);
    private static SetTextInputAreaPointerNativeDelegate SetTextInputAreaPointerNativeFunction = SDL_SetTextInputArea;

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
    public static bool SetTextInputArea(IntPtr window, IntPtr rect, int cursor)
    {
        return SetTextInputAreaPointerNativeFunction(window, rect, cursor);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetTextInputArea(IntPtr window, in Rect rect, int cursor);
    private delegate bool SetTextInputAreaRefNativeDelegate(IntPtr window, in Rect rect, int cursor);
    private static SetTextInputAreaRefNativeDelegate SetTextInputAreaRefNativeFunction = SDL_SetTextInputArea;

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
    public static bool SetTextInputArea(IntPtr window, in Rect rect, int cursor)
    {
        return SetTextInputAreaRefNativeFunction(window, in rect, cursor);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextInputArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetTextInputArea(IntPtr window, out Rect rect, out int cursor);
    private delegate bool GetTextInputAreaNativeDelegate(IntPtr window, out Rect rect, out int cursor);
    private static GetTextInputAreaNativeDelegate GetTextInputAreaNativeFunction = SDL_GetTextInputArea;

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
    public static bool GetTextInputArea(IntPtr window, out Rect rect, out int cursor)
    {
        return GetTextInputAreaNativeFunction(window, out rect, out cursor);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasScreenKeyboardSupport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasScreenKeyboardSupport();
    private delegate bool HasScreenKeyboardSupportNativeDelegate();
    private static HasScreenKeyboardSupportNativeDelegate HasScreenKeyboardSupportNativeFunction = SDL_HasScreenKeyboardSupport;

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
    public static bool HasScreenKeyboardSupport()
    {
        return HasScreenKeyboardSupportNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ScreenKeyboardShown"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ScreenKeyboardShown(IntPtr window);
    private delegate bool ScreenKeyboardShownNativeDelegate(IntPtr window);
    private static ScreenKeyboardShownNativeDelegate ScreenKeyboardShownNativeFunction = SDL_ScreenKeyboardShown;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ScreenKeyboardShown(SDL_Window *window);</code>
    /// <summary>
    /// <para>Check whether the screen keyboard is shown for given window.</para>
    /// </summary>
    /// <param name="window">the window for which screen keyboard should be queried.</param>
    /// <returns><c>true</c> if screen keyboard is shown or <c>false</c> if not.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasScreenKeyboardSupport"/>
    public static bool ScreenKeyboardShown(IntPtr window)
    {
        return ScreenKeyboardShownNativeFunction(window);
    }
}
