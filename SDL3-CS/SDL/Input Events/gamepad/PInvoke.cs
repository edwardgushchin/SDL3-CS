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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddGamepadMapping"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMapping([MarshalAs(UnmanagedType.LPUTF8Str)] string mapping);
    private delegate int AddGamepadMappingNativeDelegate(string mapping);
    private static AddGamepadMappingNativeDelegate AddGamepadMappingNativeFunction = SDL_AddGamepadMapping;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddGamepadMapping(const char *mapping);</code>
    /// <summary>
    /// <para>Add support for gamepads that SDL is unaware of or change the binding of an
    /// existing gamepad.</para>
    /// <para>The mapping string has the format "GUID,name,mapping", where GUID is the
    /// string value from <see cref="GUIDToString(GUID, byte[], int)"/>, name is the human readable string for
    /// the device and mappings are gamepad mappings to joystick ones. Under
    /// Windows there is a reserved GUID of "xinput" that covers all XInput
    /// devices. The mapping format for joystick is:</para>
    /// <list type="bullet">
    /// <item><c>bX</c>: a joystick button, index X</item>
    /// <item><c>hX.Y</c>: hat X with value Y</item>
    /// <item><c>aX</c>: axis X of the joystick</item>
    /// </list>
    /// <para>Buttons can be used as a gamepad axes and vice versa.</para>
    /// <para>If a device with this GUID is already plugged in, SDL will generate an
    /// <see cref="EventType.GamepadAdded"/> event.</para>
    /// <para>This string shows an example of a valid mapping for a gamepad:</para>
    /// <code>"341a3608000000000000504944564944,Afterglow PS3 Controller,a:b1,b:b2,y:b3,x:b0,start:b9,guide:b12,back:b8,dpup:h0.1,dpleft:h0.8,dpdown:h0.4,dpright:h0.2,leftshoulder:b4,rightshoulder:b5,leftstick:b10,rightstick:b11,leftx:a0,lefty:a1,rightx:a2,righty:a3,lefttrigger:b6,righttrigger:b7"</code>
    /// </summary>
    /// <param name="mapping">the mapping string.</param>
    /// <returns>1 if a new mapping is added, 0 if an existing mapping is updated,
    /// -1 on failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddGamepadMappingsFromFile"/>
    /// <seealso cref="AddGamepadMappingsFromIO"/>
    /// <seealso cref="GetGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    /// <seealso cref="Hints.GameControllerConfig"/>
    /// <seealso cref="Hints.GameControllerConfigFile"/>
    /// <seealso cref="EventType.GamepadAdded"/>
    public static int AddGamepadMapping(string mapping)
    {
        return AddGamepadMappingNativeFunction(mapping);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddGamepadMappingsFromIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    private delegate int AddGamepadMappingsFromIONativeDelegate(IntPtr src, bool closeio);
    private static AddGamepadMappingsFromIONativeDelegate AddGamepadMappingsFromIONativeFunction = SDL_AddGamepadMappingsFromIO;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddGamepadMappingsFromIO(SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load a set of gamepad mappings from an SDL_IOStream.</para>
    /// <para>You can call this function several times, if needed, to load different
    /// database files.</para>
    /// <para>If a new mapping is loaded for an already known gamepad GUID, the later
    /// version will overwrite the one currently loaded.</para>
    /// <para>Any new mappings for already plugged in controllers will generate
    /// <see cref="EventType.GamepadAdded"/> events.</para>
    /// <para>Mappings not belonging to the current platform or with no platform field
    /// specified will be ignored (i.e. mappings for Linux will be ignored in
    /// Windows, etc).</para>
    /// <para>This function will load the text database entirely in memory before
    /// processing it, so take this into consideration if you are in a memory
    /// constrained environment.</para>
    /// </summary>
    /// <param name="src">the data stream for the mappings to be added.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>src</c> before returning, even
    /// in the case of an error.</param>
    /// <returns>the number of mappings added or -1 on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="AddGamepadMappingsFromFile"/>
    /// <seealso cref="GetGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    /// <seealso cref="Hints.GameControllerConfig"/>
    /// <seealso cref="Hints.GameControllerConfigFile"/>
    /// <seealso cref="EventType.GamepadAdded"/>
    public static int AddGamepadMappingsFromIO(IntPtr src, bool closeio)
    {
        return AddGamepadMappingsFromIONativeFunction(src, closeio);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddGamepadMappingsFromFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    private delegate int AddGamepadMappingsFromFileNativeDelegate(string file);
    private static AddGamepadMappingsFromFileNativeDelegate AddGamepadMappingsFromFileNativeFunction = SDL_AddGamepadMappingsFromFile;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddGamepadMappingsFromFile(const char *file);</code>
    /// <summary>
    /// <para>Load a set of gamepad mappings from a file.</para>
    /// <para>You can call this function several times, if needed, to load different
    /// database files.</para>
    /// <para>If a new mapping is loaded for an already known gamepad GUID, the later
    /// version will overwrite the one currently loaded.</para>
    /// <para>Any new mappings for already plugged in controllers will generate
    /// <see cref="EventType.GamepadAdded"/> events.</para>
    /// <para>Mappings not belonging to the current platform or with no platform field
    /// specified will be ignored (i.e. mappings for Linux will be ignored in
    /// Windows, etc).</para>
    /// </summary>
    /// <param name="file">the mappings file to load.</param>
    /// <returns>the number of mappings added or -1 on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="AddGamepadMappingsFromIO"/>
    /// <seealso cref="GetGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    /// <seealso cref="Hints.GameControllerConfig"/>
    /// <seealso cref="Hints.GameControllerConfigFile"/>
    /// <seealso cref="EventType.GamepadAdded"/>
    public static int AddGamepadMappingsFromFile(string file)
    {
        return AddGamepadMappingsFromFileNativeFunction(file);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReloadGamepadMappings"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReloadGamepadMappings();
    private delegate bool ReloadGamepadMappingsNativeDelegate();
    private static ReloadGamepadMappingsNativeDelegate ReloadGamepadMappingsNativeFunction = SDL_ReloadGamepadMappings;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReloadGamepadMappings(void);</code>
    /// <summary>
    /// <para>Reinitialize the SDL mapping database to its initial state.</para>
    /// <para>This will generate gamepad events as needed if device mappings change.</para>
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReloadGamepadMappings()
    {
        return ReloadGamepadMappingsNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadMappings"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetGamepadMappings(out int count);
    private delegate IntPtr GetGamepadMappingsNativeDelegate(out int count);
    private static GetGamepadMappingsNativeDelegate GetGamepadMappingsNativeFunction = SDL_GetGamepadMappings;
    /// <code>extern SDL_DECLSPEC char ** SDLCALL SDL_GetGamepadMappings(int *count);</code>
    /// <summary>
    /// Get the current gamepad mappings.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of mappings returned, can
    /// be <c>null</c>.</param>
    /// <returns>an array of the mapping strings, <c>null</c>-terminated, or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information. This is a
    /// single allocation that should be freed with <see cref="Free"/> when it is
    /// no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string[]? GetGamepadMappings(out int count)
    {
        var ptr = GetGamepadMappingsNativeFunction(out count);

        try
        {
            return PointerToStringArray(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadMappingForGUID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMappingForGUID(GUID guid);
    private delegate IntPtr GetGamepadMappingForGUIDNativeDelegate(GUID guid);
    private static GetGamepadMappingForGUIDNativeDelegate GetGamepadMappingForGUIDNativeFunction = SDL_GetGamepadMappingForGUID;
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetGamepadMappingForGUID(SDL_GUID guid);</code>
    /// <summary>
    /// Get the gamepad mapping string for a given GUID.
    /// </summary>
    /// <param name="guid">a structure containing the GUID for which a mapping is desired.</param>
    /// <returns>a mapping string or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information. This should be freed with <see cref="Free"/> when it is no
    /// longer needed.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetJoystickGUIDForID"/>
    /// <seealso cref="GetJoystickGUID"/>
    public static string? GetGamepadMappingForGUID(GUID guid)
    {
        var value = GetGamepadMappingForGUIDNativeFunction(guid);
        try
        {
            return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadMapping"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMapping(IntPtr gamepad);
    private delegate IntPtr GetGamepadMappingNativeDelegate(IntPtr gamepad);
    private static GetGamepadMappingNativeDelegate GetGamepadMappingNativeFunction = SDL_GetGamepadMapping;
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetGamepadMapping(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the current mapping of a gamepad.</para>
    /// <para>Details about mappings are discussed with <see cref="AddGamepadMapping"/>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad you want to get the current mapping for.</param>
    /// <returns>a string that has the gamepad's mapping or <c>null</c> if no mapping is
    /// available; call <see cref="GetError"/> for more information. This should
    /// be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForID"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    /// <seealso cref="SetGamepadMapping"/>
    public static string? GetGamepadMapping(IntPtr gamepad)
    {
        var value = GetGamepadMappingNativeFunction(gamepad);
        try
        {
            return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGamepadMapping"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetGamepadMapping(uint instanceID, [MarshalAs(UnmanagedType.LPUTF8Str)] string? mapping);
    private delegate bool SetGamepadMappingNativeDelegate(uint instanceID, string? mapping);
    private static SetGamepadMappingNativeDelegate SetGamepadMappingNativeFunction = SDL_SetGamepadMapping;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGamepadMapping(SDL_JoystickID instance_id, const char *mapping);</code>
    /// <summary>
    /// <para>Set the current mapping of a joystick or gamepad.</para>
    /// <para>Details about mappings are discussed with <see cref="AddGamepadMapping"/>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <param name="mapping">the mapping to use for this device, or <c>null</c> to clear the
    /// mapping.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="GetGamepadMapping"/>
    public static bool SetGamepadMapping(uint instanceID, string? mapping)
    {
        return SetGamepadMappingNativeFunction(instanceID, mapping);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasGamepad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasGamepad();
    private delegate bool HasGamepadNativeDelegate();
    private static HasGamepadNativeDelegate HasGamepadNativeFunction = SDL_HasGamepad;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasGamepad(void);</code>
    /// <summary>
    /// Return whether a gamepad is currently connected.
    /// </summary>
    /// <returns><c>true</c> if a gamepad is connected, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepads"/>
    public static bool HasGamepad()
    {
        return HasGamepadNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepads"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepads(out int count);
    private delegate IntPtr GetGamepadsNativeDelegate(out int count);
    private static GetGamepadsNativeDelegate GetGamepadsNativeFunction = SDL_GetGamepads;
    /// <code>extern SDL_DECLSPEC SDL_JoystickID * SDLCALL SDL_GetGamepads(int *count);</code>
    /// <summary>
    /// Get a list of currently connected gamepads.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of gamepads returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of joystick instance IDs or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasGamepad"/>
    /// <seealso cref="OpenGamepad"/>
    public static uint[]? GetGamepads(out int count)
    {
        var ptr = GetGamepadsNativeFunction(out count);

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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsGamepad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_IsGamepad(uint instanceId);
    private delegate bool IsGamepadNativeDelegate(uint instanceId);
    private static IsGamepadNativeDelegate IsGamepadNativeFunction = SDL_IsGamepad;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsGamepad(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// Check if the given joystick is supported by the gamepad interface.
    /// </summary>
    /// <param name="instanceId">the joystick instance ID.</param>
    /// <returns><c>true</c> if the given joystick is supported by the gamepad interface,
    /// <c>false</c> if it isn't or it's an invalid index.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetJoysticks"/>
    /// <seealso cref="OpenGamepad"/>
    public static bool IsGamepad(uint instanceId)
    {
        return IsGamepadNativeFunction(instanceId);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadNameForID(uint instanceId);
    private delegate IntPtr GetGamepadNameForIDNativeDelegate(uint instanceId);
    private static GetGamepadNameForIDNativeDelegate GetGamepadNameForIDNativeFunction = SDL_GetGamepadNameForID;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadNameForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent name of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceId">the joystick instance ID.</param>
    /// <returns>the name of the selected gamepad. If no name can be found, this
    /// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadName"/>
    /// <seealso cref="GetGamepads"/>
    public static string? GetGamepadNameForID(uint instanceId)
    {
        var value = GetGamepadNameForIDNativeFunction(instanceId);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadPathForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPathForID(uint instanceId);
    private delegate IntPtr GetGamepadPathForIDNativeDelegate(uint instanceId);
    private static GetGamepadPathForIDNativeDelegate GetGamepadPathForIDNativeFunction = SDL_GetGamepadPathForID;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadPathForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent path of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceId">the joystick instance ID.</param>
    /// <returns>the path of the selected gamepad. If no path can be found, this
    /// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadPath"/>
    /// <seealso cref="GetGamepads"/>
    public static string? GetGamepadPathForID(uint instanceId)
    {
        var value = GetGamepadPathForIDNativeFunction(instanceId);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadPlayerIndexForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndexForID(uint instanceID);
    private delegate int GetGamepadPlayerIndexForIDNativeDelegate(uint instanceID);
    private static GetGamepadPlayerIndexForIDNativeDelegate GetGamepadPlayerIndexForIDNativeFunction = SDL_GetGamepadPlayerIndexForID;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetGamepadPlayerIndexForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the player index of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the player index of a gamepad, or -1 if it's not available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadPlayerIndex"/>
    /// <seealso cref="GetGamepads"/>
    public static int GetGamepadPlayerIndexForID(uint instanceID)
    {
        return GetGamepadPlayerIndexForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadGUIDForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GetGamepadGUIDForID(uint instanceID);
    private delegate GUID GetGamepadGUIDForIDNativeDelegate(uint instanceID);
    private static GetGamepadGUIDForIDNativeDelegate GetGamepadGUIDForIDNativeFunction = SDL_GetGamepadGUIDForID;

    /// <code>extern SDL_DECLSPEC SDL_GUID SDLCALL SDL_GetGamepadGUIDForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation-dependent GUID of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the GUID of the selected gamepad. If called on an invalid index,
    /// this function returns a zero GUID.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GUIDToString(GUID, byte[], int)"/>
    /// <seealso cref="GetGamepads"/>
    public static GUID GetGamepadGUIDForID(uint instanceID)
    {
        return GetGamepadGUIDForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadVendorForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendorForID(uint instanceID);
    private delegate ushort GetGamepadVendorForIDNativeDelegate(uint instanceID);
    private static GetGamepadVendorForIDNativeDelegate GetGamepadVendorForIDNativeFunction = SDL_GetGamepadVendorForID;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadVendorForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the USB vendor ID of a gamepad, if available.</para>
    /// <para>This can be called before any gamepads are opened. If the vendor ID isn't
    /// available this function returns 0.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the USB vendor ID of the selected gamepad. If called on an invalid
    /// index, this function returns zero.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadVendor"/>
    /// <seealso cref="GetGamepads"/>
    public static ushort GetGamepadVendorForID(uint instanceID)
    {
        return GetGamepadVendorForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadProductForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductForID(uint instanceID);
    private delegate ushort GetGamepadProductForIDNativeDelegate(uint instanceID);
    private static GetGamepadProductForIDNativeDelegate GetGamepadProductForIDNativeFunction = SDL_GetGamepadProductForID;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProductForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the USB product ID of a gamepad, if available.</para>
    /// <para>This can be called before any gamepads are opened. If the product ID isn't
    /// available this function returns 0.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the USB product ID of the selected gamepad. If called on an
    /// invalid index, this function returns zero.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadProduct"/>
    /// <seealso cref="GetGamepads"/>
    public static ushort GetGamepadProductForID(uint instanceID)
    {
        return GetGamepadProductForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadProductVersionForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersionForID(uint instanceID);
    private delegate ushort GetGamepadProductVersionForIDNativeDelegate(uint instanceID);
    private static GetGamepadProductVersionForIDNativeDelegate GetGamepadProductVersionForIDNativeFunction = SDL_GetGamepadProductVersionForID;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProductVersionForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the product version of a gamepad, if available.</para>
    /// <para>This can be called before any gamepads are opened. If the product version
    /// isn't available this function returns 0.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the product version of the selected gamepad. If called on an
    /// invalid index, this function returns zero.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadProductVersion"/>
    /// <seealso cref="GetGamepads"/>
    public static ushort GetGamepadProductVersionForID(uint instanceID)
    {
        return GetGamepadProductVersionForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadTypeForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeForID(uint instanceID);
    private delegate GamepadType GetGamepadTypeForIDNativeDelegate(uint instanceID);
    private static GetGamepadTypeForIDNativeDelegate GetGamepadTypeForIDNativeFunction = SDL_GetGamepadTypeForID;

    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetGamepadTypeForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the type of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the gamepad type.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadType"/>
    /// <seealso cref="GetGamepads"/>
    /// <seealso cref="GetRealGamepadTypeForID"/>
    public static GamepadType GetGamepadTypeForID(uint instanceID)
    {
        return GetGamepadTypeForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRealGamepadTypeForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instanceID);
    private delegate GamepadType GetRealGamepadTypeForIDNativeDelegate(uint instanceID);
    private static GetRealGamepadTypeForIDNativeDelegate GetRealGamepadTypeForIDNativeFunction = SDL_GetRealGamepadTypeForID;

    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetRealGamepadTypeForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the type of a gamepad, ignoring any mapping override.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the gamepad type.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadTypeForID"/>
    /// <seealso cref="GetGamepads"/>
    /// <seealso cref="GetRealGamepadType"/>
    public static GamepadType GetRealGamepadTypeForID(uint instanceID)
    {
        return GetRealGamepadTypeForIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadMappingForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMappingForID(uint instanceId);
    private delegate IntPtr GetGamepadMappingForIDNativeDelegate(uint instanceId);
    private static GetGamepadMappingForIDNativeDelegate GetGamepadMappingForIDNativeFunction = SDL_GetGamepadMappingForID;
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetGamepadMappingForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the mapping of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceId">the joystick instance ID.</param>
    /// <returns>the mapping string. Returns <c>null</c> if no mapping is available. This
    /// should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepads"/>
    /// <seealso cref="GetGamepadMapping"/>
    public static string? GetGamepadMappingForID(uint instanceId)
    {
        var value = GetGamepadMappingForIDNativeFunction(instanceId);
        try
        {
            return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenGamepad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenGamepad(uint instanceID);
    private delegate IntPtr OpenGamepadNativeDelegate(uint instanceID);
    private static OpenGamepadNativeDelegate OpenGamepadNativeFunction = SDL_OpenGamepad;

    /// <code>extern SDL_DECLSPEC SDL_Gamepad * SDLCALL SDL_OpenGamepad(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// Open a gamepad for use.
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>a gamepad identifier or <c>null</c> if an error occurred; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseGamepad"/>
    /// <seealso cref="IsGamepad"/>
    public static IntPtr OpenGamepad(uint instanceID)
    {
        return OpenGamepadNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadFromID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromID(uint instanceID);
    private delegate IntPtr GetGamepadFromIDNativeDelegate(uint instanceID);
    private static GetGamepadFromIDNativeDelegate GetGamepadFromIDNativeFunction = SDL_GetGamepadFromID;

    /// <code>extern SDL_DECLSPEC SDL_Gamepad * SDLCALL SDL_GetGamepadFromID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the SDL_Gamepad associated with a joystick instance ID, if it has been
    /// opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID of the gamepad.</param>
    /// <returns>an SDL_Gamepad on success or <c>null</c> on failure or if it hasn't been
    /// opened yet; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr GetGamepadFromID(uint instanceID)
    {
        return GetGamepadFromIDNativeFunction(instanceID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadFromPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromPlayerIndex(int playerIndex);
    private delegate IntPtr GetGamepadFromPlayerIndexNativeDelegate(int playerIndex);
    private static GetGamepadFromPlayerIndexNativeDelegate GetGamepadFromPlayerIndexNativeFunction = SDL_GetGamepadFromPlayerIndex;

    /// <code>extern SDL_DECLSPEC SDL_Gamepad * SDLCALL SDL_GetGamepadFromPlayerIndex(int player_index);</code>
    /// <summary>
    /// Get the SDL_Gamepad associated with a player index.
    /// </summary>
    /// <param name="playerIndex">the player index, which different from the instance ID.</param>
    /// <returns>the SDL_Gamepad associated with a player index.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadPlayerIndex"/>
    /// <seealso cref="SetGamepadPlayerIndex"/>
    public static IntPtr GetGamepadFromPlayerIndex(int playerIndex)
    {
        return GetGamepadFromPlayerIndexNativeFunction(playerIndex);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadProperties(IntPtr gamepad);
    private delegate uint GetGamepadPropertiesNativeDelegate(IntPtr gamepad);
    private static GetGamepadPropertiesNativeDelegate GetGamepadPropertiesNativeFunction = SDL_GetGamepadProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetGamepadProperties(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the properties associated with an opened gamepad.</para>
    /// <para>These properties are shared with the underlying joystick object.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GamepadCapMonoLedBoolean"/>: <c>true</c> if this gamepad has an LED
    /// that has adjustable brightness</item>
    /// <item><see cref="Props.GamepadCapRGBLedBoolean"/>: <c>true</c> if this gamepad has an LED
    /// that has adjustable color</item>
    /// <item><see cref="Props.GamepadCapPlayerLedBoolean"/>: <c>true</c> if this gamepad has a
    /// player LED</item>
    /// <item><see cref="Props.GamepadCapRumbleBoolean"/>: <c>true</c> if this gamepad has
    /// left/right rumble</item>
    /// <item><see cref="Props.GamepadCapTriggerRumbleBoolean"/>: <c>true</c> if this gamepad has
    /// simple trigger rumble</item>
    /// </list>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    /// <see cref="OpenGamepad"/>.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GetGamepadProperties(IntPtr gamepad)
    {
        return GetGamepadPropertiesNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadID(IntPtr gamepad);
    private delegate uint GetGamepadIDNativeDelegate(IntPtr gamepad);
    private static GetGamepadIDNativeDelegate GetGamepadIDNativeFunction = SDL_GetGamepadID;

    /// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_GetGamepadID(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the instance ID of an opened gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    /// <see cref="OpenGamepad"/>.</param>
    /// <returns>the instance ID of the specified gamepad on success or 0 on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GetGamepadID(IntPtr gamepad)
    {
        return GetGamepadIDNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadName(IntPtr gamepad);
    private delegate IntPtr GetGamepadNameNativeDelegate(IntPtr gamepad);
    private static GetGamepadNameNativeDelegate GetGamepadNameNativeFunction = SDL_GetGamepadName;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadName(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the implementation-dependent name for an opened gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    /// <see cref="OpenGamepad"/>.</param>
    /// <returns>the implementation dependent name for the gamepad, or <c>null</c> if
    /// there is no name or the identifier passed is invalid.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadNameForID"/>
    public static string? GetGamepadName(IntPtr gamepad)
    {
        var value = GetGamepadNameNativeFunction(gamepad);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadPath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPath(IntPtr gamepad);
    private delegate IntPtr GetGamepadPathNativeDelegate(IntPtr gamepad);
    private static GetGamepadPathNativeDelegate GetGamepadPathNativeFunction = SDL_GetGamepadPath;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadPath(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the implementation-dependent path for an opened gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    /// <see cref="OpenGamepad"/>.</param>
    /// <returns>the implementation dependent path for the gamepad, or <c>null</c> if
    /// there is no path or the identifier passed is invalid.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadPathForID"/>
    public static string? GetGamepadPath(IntPtr gamepad)
    {
        var value = GetGamepadPathNativeFunction(gamepad);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadType(IntPtr gamepad);
    private delegate GamepadType GetGamepadTypeNativeDelegate(IntPtr gamepad);
    private static GetGamepadTypeNativeDelegate GetGamepadTypeNativeFunction = SDL_GetGamepadType;

    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetGamepadType(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the type of an opened gamepad.
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad type, or <see cref="GamepadType.Unknown"/> if it's not
    /// available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadTypeForID"/>
    public static GamepadType GetGamepadType(IntPtr gamepad)
    {
        return GetGamepadTypeNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRealGamepadType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadType(IntPtr gamepad);
    private delegate GamepadType GetRealGamepadTypeNativeDelegate(IntPtr gamepad);
    private static GetRealGamepadTypeNativeDelegate GetRealGamepadTypeNativeFunction = SDL_GetRealGamepadType;

    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetRealGamepadType(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the type of an opened gamepad, ignoring any mapping override.
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad type, or <see cref="GamepadType.Unknown"/> if it's not
    /// available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRealGamepadTypeForID"/>
    public static GamepadType GetRealGamepadType(IntPtr gamepad)
    {
        return GetRealGamepadTypeNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndex(IntPtr gamepad);
    private delegate int GetGamepadPlayerIndexNativeDelegate(IntPtr gamepad);
    private static GetGamepadPlayerIndexNativeDelegate GetGamepadPlayerIndexNativeFunction = SDL_GetGamepadPlayerIndex;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetGamepadPlayerIndex(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the player index of an opened gamepad.</para>
    /// <para>For XInput gamepads this returns the XInput user index.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the player index for gamepad, or -1 if it's not available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetGamepadPlayerIndex"/>
    public static int GetGamepadPlayerIndex(IntPtr gamepad)
    {
        return GetGamepadPlayerIndexNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGamepadPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetGamepadPlayerIndex(IntPtr gamepad, int playerIndex);
    private delegate bool SetGamepadPlayerIndexNativeDelegate(IntPtr gamepad, int playerIndex);
    private static SetGamepadPlayerIndexNativeDelegate SetGamepadPlayerIndexNativeFunction = SDL_SetGamepadPlayerIndex;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGamepadPlayerIndex(SDL_Gamepad *gamepad, int player_index);</code>
    /// <summary>
    /// Set the player index of an opened gamepad.
    /// </summary>
    /// <param name="gamepad">the gamepad object to adjust.</param>
    /// <param name="playerIndex">player index to assign to this gamepad, or -1 to clear
    /// the player index and turn off player LEDs.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <seealso cref="GetGamepadPlayerIndex"/>
    public static bool SetGamepadPlayerIndex(IntPtr gamepad, int playerIndex)
    {
        return SetGamepadPlayerIndexNativeFunction(gamepad, playerIndex);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadVendor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendor(IntPtr gamepad);
    private delegate ushort GetGamepadVendorNativeDelegate(IntPtr gamepad);
    private static GetGamepadVendorNativeDelegate GetGamepadVendorNativeFunction = SDL_GetGamepadVendor;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadVendor(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the USB vendor ID of an opened gamepad, if available.</para>
    /// <para>If the vendor ID isn't available this function returns 0.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the USB vendor ID, or zero if unavailable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadVendorForID"/>
    public static ushort GetGamepadVendor(IntPtr gamepad)
    {
        return GetGamepadVendorNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadProduct"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProduct(IntPtr gamepad);
    private delegate ushort GetGamepadProductNativeDelegate(IntPtr gamepad);
    private static GetGamepadProductNativeDelegate GetGamepadProductNativeFunction = SDL_GetGamepadProduct;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProduct(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the USB product ID of an opened gamepad, if available.</para>
    /// <para>If the product ID isn't available this function returns 0.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the USB product ID, or zero if unavailable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadProductForID"/>
    public static ushort GetGamepadProduct(IntPtr gamepad)
    {
        return GetGamepadProductNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadProductVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersion(IntPtr gamepad);
    private delegate ushort GetGamepadProductVersionNativeDelegate(IntPtr gamepad);
    private static GetGamepadProductVersionNativeDelegate GetGamepadProductVersionNativeFunction = SDL_GetGamepadProductVersion;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProductVersion(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the product version of an opened gamepad, if available.</para>
    /// <para>If the product version isn't available this function returns 0.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the USB product version, or zero if unavailable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadProductVersionForID"/>
    public static ushort GetGamepadProductVersion(IntPtr gamepad)
    {
        return GetGamepadProductVersionNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadFirmwareVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadFirmwareVersion(IntPtr gamepad);
    private delegate ushort GetGamepadFirmwareVersionNativeDelegate(IntPtr gamepad);
    private static GetGamepadFirmwareVersionNativeDelegate GetGamepadFirmwareVersionNativeFunction = SDL_GetGamepadFirmwareVersion;

    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadFirmwareVersion(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the firmware version of an opened gamepad, if available.</para>
    /// <para>If the firmware version isn't available this function returns 0.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad firmware version, or zero if unavailable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static ushort GetGamepadFirmwareVersion(IntPtr gamepad)
    {
        return GetGamepadFirmwareVersionNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadSerial"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadSerial(IntPtr gamepad);
    private delegate IntPtr GetGamepadSerialNativeDelegate(IntPtr gamepad);
    private static GetGamepadSerialNativeDelegate GetGamepadSerialNativeFunction = SDL_GetGamepadSerial;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadSerial(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the serial number of an opened gamepad, if available.</para>
    /// <para>Returns the serial number of the gamepad, or <c>null</c> if it is not available.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the serial number, or <c>null</c> if unavailable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetGamepadSerial(IntPtr gamepad)
    {
        var value = GetGamepadSerialNativeFunction(gamepad);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadSteamHandle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetGamepadSteamHandle(IntPtr gamepad);
    private delegate ulong GetGamepadSteamHandleNativeDelegate(IntPtr gamepad);
    private static GetGamepadSteamHandleNativeDelegate GetGamepadSteamHandleNativeFunction = SDL_GetGamepadSteamHandle;

    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetGamepadSteamHandle(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the Steam Input handle of an opened gamepad, if available.</para>
    /// <para>Returns an InputHandle_t for the gamepad that can be used with Steam Input
    /// API: https://partner.steamgames.com/doc/api/ISteamInput</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad handle, or 0 if unavailable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static ulong GetGamepadSteamHandle(IntPtr gamepad)
    {
        return GetGamepadSteamHandleNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadConnectionState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetGamepadConnectionState(IntPtr gamepad);
    private delegate JoystickConnectionState GetGamepadConnectionStateNativeDelegate(IntPtr gamepad);
    private static GetGamepadConnectionStateNativeDelegate GetGamepadConnectionStateNativeFunction = SDL_GetGamepadConnectionState;

    /// <code>extern SDL_DECLSPEC SDL_JoystickConnectionState SDLCALL SDL_GetGamepadConnectionState(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the connection state of a gamepad.
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the connection state on success or
    /// <see cref="JoystickConnectionState.Invalid"/> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static JoystickConnectionState GetGamepadConnectionState(IntPtr gamepad)
    {
        return GetGamepadConnectionStateNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadPowerInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetGamepadPowerInfo(IntPtr gamepad, out int percent);
    private delegate PowerState GetGamepadPowerInfoNativeDelegate(IntPtr gamepad, out int percent);
    private static GetGamepadPowerInfoNativeDelegate GetGamepadPowerInfoNativeFunction = SDL_GetGamepadPowerInfo;

    /// <code>extern SDL_DECLSPEC SDL_PowerState SDLCALL SDL_GetGamepadPowerInfo(SDL_Gamepad *gamepad, int *percent);</code>
    /// <summary>
    /// <para>Get the battery state of a gamepad.</para>
    /// <para>You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports. It's
    /// not uncommon for older batteries to lose stored power much faster than it
    /// reports, or completely drain when reporting it has 20 percent left, etc.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <param name="percent">a pointer filled in with the percentage of battery life
    /// left, between 0 and 100, or <c>null</c> to ignore. This will be
    /// filled in with -1 we can't determine a value or there is no
    /// battery.</param>
    /// <returns>the current battery state.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static PowerState GetGamepadPowerInfo(IntPtr gamepad, out int percent)
    {
        return GetGamepadPowerInfoNativeFunction(gamepad, out percent);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GamepadConnected"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadConnected(IntPtr gamepad);
    private delegate bool GamepadConnectedNativeDelegate(IntPtr gamepad);
    private static GamepadConnectedNativeDelegate GamepadConnectedNativeFunction = SDL_GamepadConnected;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GamepadConnected(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Check if a gamepad has been opened and is currently connected.
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    /// <see cref="OpenGamepad"/>.</param>
    /// <returns><c>true</c> if the gamepad has been opened and is currently connected, or
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool GamepadConnected(IntPtr gamepad)
    {
        return GamepadConnectedNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);
    private delegate IntPtr GetGamepadJoystickNativeDelegate(IntPtr gamepad);
    private static GetGamepadJoystickNativeDelegate GetGamepadJoystickNativeFunction = SDL_GetGamepadJoystick;

    /// <code>extern SDL_DECLSPEC SDL_Joystick * SDLCALL SDL_GetGamepadJoystick(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the underlying joystick from a gamepad.</para>
    /// <para>This function will give you a SDL_Joystick object, which allows you to use
    /// the SDL_Joystick functions with a SDL_Gamepad object. This would be useful
    /// for getting a joystick's position at any given time, even if it hasn't
    /// moved (moving it would produce an event, which would have the axis' value).</para>
    /// <para>The pointer returned is owned by the SDL_Gamepad. You should not call
    /// <see cref="CloseJoystick"/> on it, for example, since doing so will likely cause
    /// SDL to crash.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object that you want to get a joystick from.</param>
    /// <returns>an SDL_Joystick object, or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr GetGamepadJoystick(IntPtr gamepad)
    {
        return GetGamepadJoystickNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGamepadEventsEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGamepadEventsEnabled([MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate void SetGamepadEventsEnabledNativeDelegate(bool enabled);
    private static SetGamepadEventsEnabledNativeDelegate SetGamepadEventsEnabledNativeFunction = SDL_SetGamepadEventsEnabled;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGamepadEventsEnabled(bool enabled);</code>
    /// <summary>
    /// <para>Set the state of gamepad event processing.</para>
    /// <para>If gamepad events are disabled, you must call <see cref="UpdateGamepads"/> yourself
    /// and check the state of the gamepad when you want gamepad information.</para>
    /// </summary>
    /// <param name="enabled">whether to process gamepad events or not.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <seealso cref="GamepadEventsEnabled"/>
    /// <seealso cref="UpdateGamepads"/>
    public static void SetGamepadEventsEnabled(bool enabled)
    {
        SetGamepadEventsEnabledNativeFunction(enabled);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GamepadEventsEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadEventsEnabled();
    private delegate bool GamepadEventsEnabledNativeDelegate();
    private static GamepadEventsEnabledNativeDelegate GamepadEventsEnabledNativeFunction = SDL_GamepadEventsEnabled;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GamepadEventsEnabled(void);</code>
    /// <summary>
    /// <para>Query the state of gamepad event processing.</para>
    /// <para>If gamepad events are disabled, you must call <see cref="UpdateGamepads"/> yourself
    /// and check the state of the gamepad when you want gamepad information.</para>
    /// </summary>
    /// <returns><c>true</c> if gamepad events are being processed, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetGamepadEventsEnabled"/>
    public static bool GamepadEventsEnabled()
    {
        return GamepadEventsEnabledNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadBindings"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadBindings(IntPtr gamepad, out int count);
    private delegate IntPtr GetGamepadBindingsNativeDelegate(IntPtr gamepad, out int count);
    private static GetGamepadBindingsNativeDelegate GetGamepadBindingsNativeFunction = SDL_GetGamepadBindings;
    /// <code>extern SDL_DECLSPEC SDL_GamepadBinding ** SDLCALL SDL_GetGamepadBindings(SDL_Gamepad *gamepad, int *count);</code>
    /// <summary>
    /// Get the SDL joystick layer bindings for a gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="count">a pointer filled in with the number of bindings returned.</param>
    /// <returns>a <c>null</c> terminated array of pointers to bindings or <c>null</c>on
    /// failure; call <see cref="GetError"/> for more information. This is a
    /// single allocation that should be freed with <see cref="Free"/> when it is
    /// no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static GamepadBinding[]? GetGamepadBindings(IntPtr gamepad, out int count)
    {
        var ptr = GetGamepadBindingsNativeFunction(gamepad, out count);

        if (ptr == IntPtr.Zero) return null;

        try
        {
            return PointerToStructureArray<GamepadBinding>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateGamepads"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateGamepads();
    private delegate void UpdateGamepadsNativeDelegate();
    private static UpdateGamepadsNativeDelegate UpdateGamepadsNativeFunction = SDL_UpdateGamepads;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateGamepads(void);</code>
    /// <summary>
    /// <para>Manually pump gamepad updates if not using the loop.</para>
    /// <para>This function is called automatically by the event loop if events are
    /// enabled. Under such circumstances, it will not be necessary to call this
    /// function.</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void UpdateGamepads()
    {
        UpdateGamepadsNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadTypeFromString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    private delegate GamepadType GetGamepadTypeFromStringNativeDelegate(string str);
    private static GetGamepadTypeFromStringNativeDelegate GetGamepadTypeFromStringNativeFunction = SDL_GetGamepadTypeFromString;

    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetGamepadTypeFromString(const char *str);</code>
    /// <summary>
    /// <para>Convert a string into <see cref="GamepadType"/> enum.</para>
    /// <para>This function is called internally to translate SDL_Gamepad mapping strings
    /// for the underlying joystick device into the consistent SDL_Gamepad mapping.
    /// You do not normally need to call this function unless you are parsing
    /// SDL_Gamepad mappings in your own code.</para>
    /// </summary>
    /// <param name="str">string representing a <see cref="GamepadType"/> type.</param>
    /// <returns>the <see cref="GamepadType"/> enum corresponding to the input string, or
    /// <see cref="GamepadType.Unknown"/> if no match was found.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadStringForType"/>
    public static GamepadType GetGamepadTypeFromString(string str)
    {
        return GetGamepadTypeFromStringNativeFunction(str);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadStringForType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForType(GamepadType type);
    private delegate IntPtr GetGamepadStringForTypeNativeDelegate(GamepadType type);
    private static GetGamepadStringForTypeNativeDelegate GetGamepadStringForTypeNativeFunction = SDL_GetGamepadStringForType;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadStringForType(SDL_GamepadType type);</code>
    /// <summary>
    /// Convert from an <see cref="GamepadType"/> enum to a string.
    /// </summary>
    /// <param name="type">an enum value for a given <see cref="GamepadType"/>.</param>
    /// <returns>a string for the given type, or <c>null</c> if an invalid type is
    /// specified. The string returned is of the format used by
    /// SDL_Gamepad mapping strings.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadTypeFromString"/>
    public static string? GetGamepadStringForType(GamepadType type)
    {
        var value = GetGamepadStringForTypeNativeFunction(type);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadAxisFromString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadAxis SDL_GetGamepadAxisFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    private delegate GamepadAxis GetGamepadAxisFromStringNativeDelegate(string str);
    private static GetGamepadAxisFromStringNativeDelegate GetGamepadAxisFromStringNativeFunction = SDL_GetGamepadAxisFromString;

    /// <code>extern SDL_DECLSPEC SDL_GamepadAxis SDLCALL SDL_GetGamepadAxisFromString(const char *str);</code>
    /// <summary>
    /// <para>Convert a string into <see cref="GamepadAxis"/> enum.</para>
    /// <para>This function is called internally to translate SDL_Gamepad mapping strings
    /// for the underlying joystick device into the consistent SDL_Gamepad mapping.
    /// You do not normally need to call this function unless you are parsing
    /// SDL_Gamepad mappings in your own code.</para>
    /// <para>Note specially that "righttrigger" and "lefttrigger" map to
    /// <see cref="GamepadAxis.RightTrigger"/> and <see cref="GamepadAxis.LeftTrigger"/>,
    /// respectively.</para>
    /// </summary>
    /// <param name="str">string representing a SDL_Gamepad axis.</param>
    /// <returns>the <see cref="GamepadAxis"/> enum corresponding to the input string, or
    /// <see cref="GamepadAxis.Invalid"/> if no match was found.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadStringForAxis"/>
    public static GamepadAxis GetGamepadAxisFromString(string str)
    {
        return GetGamepadAxisFromStringNativeFunction(str);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadStringForAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForAxis(GamepadAxis axis);
    private delegate IntPtr GetGamepadStringForAxisNativeDelegate(GamepadAxis axis);
    private static GetGamepadStringForAxisNativeDelegate GetGamepadStringForAxisNativeFunction = SDL_GetGamepadStringForAxis;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis);</code>
    /// <summary>
    /// Convert from an SDL_GamepadAxis enum to a string.
    /// </summary>
    /// <param name="axis">an enum value for a given <see cref="GamepadAxis"/>.</param>
    /// <returns>a string for the given axis, or <c>null</c> if an invalid axis is
    /// specified. The string returned is of the format used by
    /// SDL_Gamepad mapping strings.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadAxisFromString"/>
    public static string? GetGamepadStringForAxis(GamepadAxis axis)
    {
        var value = GetGamepadStringForAxisNativeFunction(axis);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GamepadHasAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadHasAxis(IntPtr gamepad, GamepadAxis axis);
    private delegate bool GamepadHasAxisNativeDelegate(IntPtr gamepad, GamepadAxis axis);
    private static GamepadHasAxisNativeDelegate GamepadHasAxisNativeFunction = SDL_GamepadHasAxis;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GamepadHasAxis(SDL_Gamepad *gamepad, SDL_GamepadAxis axis);</code>
    /// <summary>
    /// <para>Query whether a gamepad has a given axis.</para>
    /// <para>This merely reports whether the gamepad's mapping defined this axis, as
    /// that is all the information SDL has about the physical device.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="axis">an axis enum value (an <see cref="GamepadAxis"/> value).</param>
    /// <returns><c>true</c> if the gamepad has this axis, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GamepadHasButton"/>
    /// <seealso cref="GetGamepadAxis"/>
    public static bool GamepadHasAxis(IntPtr gamepad, GamepadAxis axis)
    {
        return GamepadHasAxisNativeFunction(gamepad, axis);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetGamepadAxis(IntPtr gamepad, GamepadAxis axis);
    private delegate short GetGamepadAxisNativeDelegate(IntPtr gamepad, GamepadAxis axis);
    private static GetGamepadAxisNativeDelegate GetGamepadAxisNativeFunction = SDL_GetGamepadAxis;

    /// <code>extern SDL_DECLSPEC Sint16 SDLCALL SDL_GetGamepadAxis(SDL_Gamepad *gamepad, SDL_GamepadAxis axis);</code>
    /// <summary>
    /// <para>Get the current state of an axis control on a gamepad.</para>
    /// <para>The axis indices start at index 0.</para>
    /// <para>For thumbsticks, the state is a value ranging from -32768 (up/left) to
    /// 32767 (down/right).</para>
    /// <para>Triggers range from 0 when released to 32767 when fully pressed, and never
    /// return a negative value. Note that this differs from the value reported by
    /// the lower-level <see cref="GetJoystickAxis"/>, which normally uses the full range.</para>
    /// <para>Note that for invalid gamepads or axes, this will return 0. Zero is also a
    /// valid value in normal operation; usually it means a centered axis.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="axis">an axis index (one of the <see cref="GamepadAxis"/> values).</param>
    /// <returns>axis state.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GamepadHasAxis"/>
    /// <seealso cref="GetGamepadButton"/>
    public static short GetGamepadAxis(IntPtr gamepad, GamepadAxis axis)
    {
        return GetGamepadAxisNativeFunction(gamepad, axis);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadButtonFromString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButton SDL_GetGamepadButtonFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    private delegate GamepadButton GetGamepadButtonFromStringNativeDelegate(string str);
    private static GetGamepadButtonFromStringNativeDelegate GetGamepadButtonFromStringNativeFunction = SDL_GetGamepadButtonFromString;

    /// <code>extern SDL_DECLSPEC SDL_GamepadButton SDLCALL SDL_GetGamepadButtonFromString(const char *str);</code>
    /// <summary>
    /// <para>Convert a string into an SDL_GamepadButton enum.</para>
    /// <para>This function is called internally to translate SDL_Gamepad mapping strings
    /// for the underlying joystick device into the consistent SDL_Gamepad mapping.
    /// You do not normally need to call this function unless you are parsing
    /// SDL_Gamepad mappings in your own code.</para>
    /// </summary>
    /// <param name="str">string representing a SDL_Gamepad button.</param>
    /// <returns>the <see cref="GamepadButton"/> enum corresponding to the input string, or
    /// <see cref="GamepadButton.Invalid"/> if no match was found.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadStringForButton"/>
    public static GamepadButton GetGamepadButtonFromString(string str)
    {
        return GetGamepadButtonFromStringNativeFunction(str);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadStringForButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForButton(GamepadButton button);
    private delegate IntPtr GetGamepadStringForButtonNativeDelegate(GamepadButton button);
    private static GetGamepadStringForButtonNativeDelegate GetGamepadStringForButtonNativeFunction = SDL_GetGamepadStringForButton;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadStringForButton(SDL_GamepadButton button);</code>
    /// <summary>
    /// Convert from an <see cref="GamepadButton"/> enum to a string.
    /// </summary>
    /// <param name="button">an enum value for a given <see cref="GamepadButton"/>.</param>
    /// <returns>a string for the given button, or <c>null</c> if an invalid button is
    /// specified. The string returned is of the format used by
    /// SDL_Gamepad mapping strings.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadButtonFromString"/>
    public static string? GetGamepadStringForButton(GamepadButton button)
    {
        var value = GetGamepadStringForButtonNativeFunction(button);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GamepadHasButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadHasButton(IntPtr gamepad, GamepadButton button);
    private delegate bool GamepadHasButtonNativeDelegate(IntPtr gamepad, GamepadButton button);
    private static GamepadHasButtonNativeDelegate GamepadHasButtonNativeFunction = SDL_GamepadHasButton;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GamepadHasButton(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Query whether a gamepad has a given button.</para>
    /// <para>This merely reports whether the gamepad's mapping defined this button, as
    /// that is all the information SDL has about the physical device.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button enum value (an <see cref="GamepadButton"/> value).</param>
    /// <returns><c>true</c> if the gamepad has this button, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GamepadHasAxis"/>
    public static bool GamepadHasButton(IntPtr gamepad, GamepadButton button)
    {
        return GamepadHasButtonNativeFunction(gamepad, button);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetGamepadButton(IntPtr gamepad, GamepadButton button);
    private delegate bool GetGamepadButtonNativeDelegate(IntPtr gamepad, GamepadButton button);
    private static GetGamepadButtonNativeDelegate GetGamepadButtonNativeFunction = SDL_GetGamepadButton;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetGamepadButton(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// Get the current state of a button on a gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button index (one of the SDL_GamepadButton values).</param>
    /// <returns><c>true</c> if the button is pressed, <c>false</c> otherwise.</returns>
    /// /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GamepadHasButton"/>
    /// <seealso cref="GetGamepadAxis"/>
    public static bool GetGamepadButton(IntPtr gamepad, GamepadButton button)
    {
        return GetGamepadButtonNativeFunction(gamepad, button);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadButtonLabelForType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabelForType(GamepadType type, GamepadButton button);
    private delegate GamepadButtonLabel GetGamepadButtonLabelForTypeNativeDelegate(GamepadType type, GamepadButton button);
    private static GetGamepadButtonLabelForTypeNativeDelegate GetGamepadButtonLabelForTypeNativeFunction = SDL_GetGamepadButtonLabelForType;

    /// <code>extern SDL_DECLSPEC SDL_GamepadButtonLabel SDLCALL SDL_GetGamepadButtonLabelForType(SDL_GamepadType type, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Get the label of a button on a gamepad.</para>
    /// </summary>
    /// <param name="type">the type of gamepad to check.</param>
    /// <param name="button">a button index (one of the <see cref="GamepadButton"/> values).</param>
    /// <returns>the <see cref="GamepadButtonLabel"/> enum corresponding to the button label.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadButtonLabel"/>
    public static GamepadButtonLabel GetGamepadButtonLabelForType(GamepadType type, GamepadButton button)
    {
        return GetGamepadButtonLabelForTypeNativeFunction(type, button);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadButtonLabel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabel(IntPtr gamepad, GamepadButton button);
    private delegate GamepadButtonLabel GetGamepadButtonLabelNativeDelegate(IntPtr gamepad, GamepadButton button);
    private static GetGamepadButtonLabelNativeDelegate GetGamepadButtonLabelNativeFunction = SDL_GetGamepadButtonLabel;

    /// <code>extern SDL_DECLSPEC SDL_GamepadButtonLabel SDLCALL SDL_GetGamepadButtonLabel(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// Get the label of a button on a gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button index (one of the <see cref="GamepadButton"/> values).</param>
    /// <returns>the <see cref="GamepadButtonLabel"/> enum corresponding to the button label.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <seealso cref="GetGamepadButtonLabelForType"/>
    public static GamepadButtonLabel GetGamepadButtonLabel(IntPtr gamepad, GamepadButton button)
    {
        return GetGamepadButtonLabelNativeFunction(gamepad, button);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumGamepadTouchpads"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpads(IntPtr gamepad);
    private delegate int GetNumGamepadTouchpadsNativeDelegate(IntPtr gamepad);
    private static GetNumGamepadTouchpadsNativeDelegate GetNumGamepadTouchpadsNativeFunction = SDL_GetNumGamepadTouchpads;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumGamepadTouchpads(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Get the number of touchpads on a gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <returns>number of touchpads.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumGamepadTouchpadFingers"/>
    public static int GetNumGamepadTouchpads(IntPtr gamepad)
    {
        return GetNumGamepadTouchpadsNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumGamepadTouchpadFingers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad);
    private delegate int GetNumGamepadTouchpadFingersNativeDelegate(IntPtr gamepad, int touchpad);
    private static GetNumGamepadTouchpadFingersNativeDelegate GetNumGamepadTouchpadFingersNativeFunction = SDL_GetNumGamepadTouchpadFingers;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumGamepadTouchpadFingers(SDL_Gamepad *gamepad, int touchpad);</code>
    /// <summary>
    /// <para>Get the number of supported simultaneous fingers on a touchpad on a game
    /// gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="touchpad">a touchpad.</param>
    /// <returns>number of supported simultaneous fingers.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadTouchpadFinger"/>
    /// <seealso cref="GetNumGamepadTouchpads"/>
    public static int GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad)
    {
        return GetNumGamepadTouchpadFingersNativeFunction(gamepad, touchpad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadTouchpadFinger"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, [MarshalAs(UnmanagedType.I1)] out bool down, out float x, out float y, out float pressure);
    private delegate bool GetGamepadTouchpadFingerNativeDelegate(IntPtr gamepad, int touchpad, int finger, out bool down, out float x, out float y, out float pressure);
    private static GetGamepadTouchpadFingerNativeDelegate GetGamepadTouchpadFingerNativeFunction = SDL_GetGamepadTouchpadFinger;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetGamepadTouchpadFinger(SDL_Gamepad *gamepad, int touchpad, int finger, bool *down, float *x, float *y, float *pressure);</code>
    /// <summary>
    /// Get the current state of a finger on a touchpad on a gamepad.
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="touchpad">a touchpad.</param>
    /// <param name="finger">a finger.</param>
    /// <param name="down">a pointer filled with <c>true</c> if the finger is down, <c>false</c>
    /// otherwise, may be <c>null</c>.</param>
    /// <param name="x">a pointer filled with the x position, normalized 0 to 1, with the
    /// origin in the upper left, may be <c>null</c>.</param>
    /// <param name="y">a pointer filled with the y position, normalized 0 to 1, with the
    /// origin in the upper left, may be <c>null</c>.</param>
    /// <param name="pressure">a pointer filled with pressure value, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumGamepadTouchpadFingers"/>
    public static bool GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, out bool down, out float x, out float y, out float pressure)
    {
        return GetGamepadTouchpadFingerNativeFunction(gamepad, touchpad, finger, out down, out x, out y, out pressure);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GamepadHasSensor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadHasSensor(IntPtr gamepad, SensorType type);
    private delegate bool GamepadHasSensorNativeDelegate(IntPtr gamepad, SensorType type);
    private static GamepadHasSensorNativeDelegate GamepadHasSensorNativeFunction = SDL_GamepadHasSensor;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GamepadHasSensor(SDL_Gamepad *gamepad, SDL_SensorType type);</code>
    /// <summary>
    /// Return whether a gamepad has a particular sensor.
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <returns><c>true</c> if the sensor exists, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadSensorData(nint, SensorType, Span{float}, int)"/>
    /// <seealso cref="GetGamepadSensorDataRate"/>
    /// <seealso cref="SetGamepadSensorEnabled"/>
    public static bool GamepadHasSensor(IntPtr gamepad, SensorType type)
    {
        return GamepadHasSensorNativeFunction(gamepad, type);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGamepadSensorEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetGamepadSensorEnabled(IntPtr gamepad, SensorType type, [MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate bool SetGamepadSensorEnabledNativeDelegate(IntPtr gamepad, SensorType type, bool enabled);
    private static SetGamepadSensorEnabledNativeDelegate SetGamepadSensorEnabledNativeFunction = SDL_SetGamepadSensorEnabled;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGamepadSensorEnabled(SDL_Gamepad *gamepad, SDL_SensorType type, bool enabled);</code>
    /// <summary>
    /// Set whether data reporting for a gamepad sensor is enabled.
    /// </summary>
    /// <param name="gamepad">the gamepad to update.</param>
    /// <param name="type">the type of sensor to enable/disable.</param>
    /// <param name="enabled">whether data reporting should be enabled.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GamepadHasSensor"/>
    /// <seealso cref="GamepadSensorEnabled"/>
    public static bool SetGamepadSensorEnabled(IntPtr gamepad, SensorType type, bool enabled)
    {
        return SetGamepadSensorEnabledNativeFunction(gamepad, type, enabled);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GamepadSensorEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadSensorEnabled(IntPtr gamepd, SensorType type);
    private delegate bool GamepadSensorEnabledNativeDelegate(IntPtr gamepd, SensorType type);
    private static GamepadSensorEnabledNativeDelegate GamepadSensorEnabledNativeFunction = SDL_GamepadSensorEnabled;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GamepadSensorEnabled(SDL_Gamepad *gamepad, SDL_SensorType type);</code>
    /// <summary>
    /// <para>Query whether sensor data reporting is enabled for a gamepad.</para>
    /// </summary>
    /// <param name="gamepd">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <returns><c>true</c> if the sensor is enabled, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetGamepadSensorEnabled"/>
    public static bool GamepadSensorEnabled(IntPtr gamepd, SensorType type)
    {
        return GamepadSensorEnabledNativeFunction(gamepd, type);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadSensorDataRate"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetGamepadSensorDataRate(IntPtr gamepad, SensorType type);
    private delegate float GetGamepadSensorDataRateNativeDelegate(IntPtr gamepad, SensorType type);
    private static GetGamepadSensorDataRateNativeDelegate GetGamepadSensorDataRateNativeFunction = SDL_GetGamepadSensorDataRate;

    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetGamepadSensorDataRate(SDL_Gamepad *gamepad, SDL_SensorType type);</code>
    /// <summary>
    /// Get the data rate (number of events per second) of a gamepad sensor.
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <returns>the data rate, or 0.0f if the data rate is not available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static float GetGamepadSensorDataRate(IntPtr gamepad, SensorType type)
    {
        return GetGamepadSensorDataRateNativeFunction(gamepad, type);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadSensorData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetGamepadSensorData(IntPtr gamepad, SensorType type, IntPtr data, int numValues);
    private delegate bool GetGamepadSensorDataPointerNativeDelegate(IntPtr gamepad, SensorType type, IntPtr data, int numValues);
    private static GetGamepadSensorDataPointerNativeDelegate GetGamepadSensorDataPointerNativeFunction = SDL_GetGamepadSensorData;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetGamepadSensorData(SDL_Gamepad *gamepad, SDL_SensorType type, float *data, int num_values);</code>
    /// <summary>
    /// <para>Get the current state of a gamepad sensor.</para>
    /// <para>The number of values and interpretation of the data is sensor dependent.
    /// See the remarks in <see cref="SensorType"/> for details for each type of sensor.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <param name="data">the destination buffer filled with the current sensor state.</param>
    /// <param name="numValues">the number of values to write to <c>data</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool GetGamepadSensorData(IntPtr gamepad, SensorType type, Span<float> data, int numValues)
    {
        if (numValues < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numValues));
        }

        if (numValues > data.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(numValues));
        }

        unsafe
        {
            fixed (float* pData = data)
            {
                return GetGamepadSensorDataPointerNativeFunction(gamepad, type, (IntPtr)pData, numValues);
            }
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RumbleGamepad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RumbleGamepad(IntPtr gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs);
    private delegate bool RumbleGamepadNativeDelegate(IntPtr gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs);
    private static RumbleGamepadNativeDelegate RumbleGamepadNativeFunction = SDL_RumbleGamepad;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RumbleGamepad(SDL_Gamepad *gamepad, Uint16 low_frequency_rumble, Uint16 high_frequency_rumble, Uint32 duration_ms);</code>
    /// <summary>
    /// <para>Start a rumble effect on a gamepad.</para>
    /// <para>Each call to this function cancels any previous rumble effect, and calling
    /// it with 0 intensity stops any rumbling.</para>
    /// <para>This function requires you to process SDL events or call
    /// <see cref="UpdateJoysticks"/> to update rumble state.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to vibrate.</param>
    /// <param name="lowFrequencyRumble">the intensity of the low frequency (left)
    /// rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="highFrequencyRumble">the intensity of the high frequency (right)
    /// rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool RumbleGamepad(IntPtr gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs)
    {
        return RumbleGamepadNativeFunction(gamepad, lowFrequencyRumble, highFrequencyRumble, durationMs);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RumbleGamepadTriggers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort leftRumble, ushort rightRumble, uint durationMs);
    private delegate bool RumbleGamepadTriggersNativeDelegate(IntPtr gamepad, ushort leftRumble, ushort rightRumble, uint durationMs);
    private static RumbleGamepadTriggersNativeDelegate RumbleGamepadTriggersNativeFunction = SDL_RumbleGamepadTriggers;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RumbleGamepadTriggers(SDL_Gamepad *gamepad, Uint16 left_rumble, Uint16 right_rumble, Uint32 duration_ms);</code>
    /// <summary>
    /// <para>Start a rumble effect in the gamepad's triggers.</para>
    /// <para>Each call to this function cancels any previous trigger rumble effect, and
    /// calling it with 0 intensity stops any rumbling.</para>
    /// <para>Note that this is rumbling of the _triggers_ and not the gamepad as a
    /// whole. This is currently only supported on Xbox One gamepads. If you want
    /// the (more common) whole-gamepad rumble, use <see cref="RumbleGamepad"/> instead.</para>
    /// <para>This function requires you to process SDL events or call
    /// <see cref="UpdateJoysticks"/> to update rumble state.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to vibrate.</param>
    /// <param name="leftRumble">the intensity of the left trigger rumble motor, from 0
    /// to 0xFFFF.</param>
    /// <param name="rightRumble">the intensity of the right trigger rumble motor, from 0
    /// to 0xFFFF.</param>
    /// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RumbleGamepad"/>
    public static bool RumbleGamepadTriggers(IntPtr gamepad, ushort leftRumble, ushort rightRumble, uint durationMs)
    {
        return RumbleGamepadTriggersNativeFunction(gamepad, leftRumble, rightRumble, durationMs);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGamepadLED"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue);
    private delegate bool SetGamepadLEDNativeDelegate(IntPtr gamepad, byte red, byte green, byte blue);
    private static SetGamepadLEDNativeDelegate SetGamepadLEDNativeFunction = SDL_SetGamepadLED;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGamepadLED(SDL_Gamepad *gamepad, Uint8 red, Uint8 green, Uint8 blue);</code>
    /// <summary>
    /// <para>Update a gamepad's LED color.</para>
    /// <para>An example of a joystick LED is the light on the back of a PlayStation 4's
    /// DualShock 4 controller.</para>
    /// <para>For gamepads with a single color LED, the maximum of the RGB values will be
    /// used as the LED brightness.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to update.</param>
    /// <param name="red">the intensity of the red LED.</param>
    /// <param name="green">the intensity of the green LED.</param>
    /// <param name="blue">the intensity of the blue LED.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue)
    {
        return SetGamepadLEDNativeFunction(gamepad, red, green, blue);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SendGamepadEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SendGamepadEffect(IntPtr gamepad, IntPtr data, int size);
    private delegate bool SendGamepadEffectPointerNativeDelegate(IntPtr gamepad, IntPtr data, int size);
    private static SendGamepadEffectPointerNativeDelegate SendGamepadEffectPointerNativeFunction = SDL_SendGamepadEffect;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendGamepadEffect(SDL_Gamepad *gamepad, const void *data, int size);</code>
    /// <summary>
    /// Send a gamepad specific effect packet.
    /// </summary>
    /// <param name="gamepad">the gamepad to affect.</param>
    /// <param name="data">the data to send to the gamepad.</param>
    /// <param name="size">the size of the data to send to the gamepad.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool SendGamepadEffect(IntPtr gamepad, IntPtr data, int size)
    {
        return SendGamepadEffectPointerNativeFunction(gamepad, data, size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SendGamepadEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SendGamepadEffect(IntPtr gamepad, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, int size);
    private delegate bool SendGamepadEffectArrayNativeDelegate(IntPtr gamepad, byte[] data, int size);
    private static SendGamepadEffectArrayNativeDelegate SendGamepadEffectArrayNativeFunction = SDL_SendGamepadEffect;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendGamepadEffect(SDL_Gamepad *gamepad, const void *data, int size);</code>
    /// <summary>
    /// Send a gamepad specific effect packet.
    /// </summary>
    /// <param name="gamepad">the gamepad to affect.</param>
    /// <param name="data">the data to send to the gamepad.</param>
    /// <param name="size">the size of the data to send to the gamepad.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool SendGamepadEffect(IntPtr gamepad, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, int size)
    {
        return SendGamepadEffectArrayNativeFunction(gamepad, data, size);
    }

    /// <inheritdoc cref="SendGamepadEffect(nint, byte[], int)"/>
    public static unsafe bool SendGamepadEffect(IntPtr gamepad, ReadOnlySpan<byte> data, int size)
    {
        fixed (byte* pData = data)
        {
            return SendGamepadEffectPointerNativeFunction(gamepad, (IntPtr)pData, size);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseGamepad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseGamepad(IntPtr gamepad);
    private delegate void CloseGamepadNativeDelegate(IntPtr gamepad);
    private static CloseGamepadNativeDelegate CloseGamepadNativeFunction = SDL_CloseGamepad;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseGamepad(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// Close a gamepad previously opened with <see cref="OpenGamepad"/>.
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    /// <see cref="OpenGamepad"/>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenGamepad"/>
    public static void CloseGamepad(IntPtr gamepad)
    {
        CloseGamepadNativeFunction(gamepad);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, GamepadButton button);
    private delegate IntPtr GetGamepadAppleSFSymbolsNameForButtonNativeDelegate(IntPtr gamepad, GamepadButton button);
    private static GetGamepadAppleSFSymbolsNameForButtonNativeDelegate GetGamepadAppleSFSymbolsNameForButtonNativeFunction = SDL_GetGamepadAppleSFSymbolsNameForButton;

    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadAppleSFSymbolsNameForButton(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Return the sfSymbolsName for a given button on a gamepad on Apple
    /// platforms.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="button">a button on the gamepad.</param>
    /// <returns>the sfSymbolsName or <c>null</c> if the name can't be found.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadAppleSFSymbolsNameForAxis"/>
    public static string? GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, GamepadButton button)
    {
        var value = GetGamepadAppleSFSymbolsNameForButtonNativeFunction(gamepad, button);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, GamepadAxis axis);
    private delegate IntPtr GetGamepadAppleSFSymbolsNameForAxisNativeDelegate(IntPtr gamepad, GamepadAxis axis);
    private static GetGamepadAppleSFSymbolsNameForAxisNativeDelegate GetGamepadAppleSFSymbolsNameForAxisNativeFunction = SDL_GetGamepadAppleSFSymbolsNameForAxis;

    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadAppleSFSymbolsNameForAxis(SDL_Gamepad *gamepad, SDL_GamepadAxis axis);</code>
    /// <summary>
    /// Return the sfSymbolsName for a given axis on a gamepad on Apple platforms.
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="axis">an axis on the gamepad.</param>
    /// <returns>the sfSymbolsName or <c>null</c> if the name can't be found.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGamepadAppleSFSymbolsNameForButton"/>
    public static string? GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, GamepadAxis axis)
    {
        var value = GetGamepadAppleSFSymbolsNameForAxisNativeFunction(gamepad, axis);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
}
