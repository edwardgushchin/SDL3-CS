using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

/**
 * # CategoryGamepad
 *
 * SDL provides a low-level joystick API, which just treats joysticks as an
 * arbitrary pile of buttons, axes, and hat switches. If you're planning to
 * write your own control configuration screen, this can give you a lot of
 * flexibility, but that's a lot of work, and most things that we consider
 * "joysticks" now are actually console-style gamepads. So SDL provides the
 * gamepad API on top of the lower-level joystick functionality.
 *
 * The difference betweena joystick and a gamepad is that a gamepad tells you
 * _where_ a button or axis is on the device. You don't speak to gamepads in
 * terms of arbitrary numbers like "button 3" or "axis 2" but in standard
 * locations: the d-pad, the shoulder buttons, triggers, A/B/X/Y (or
 * X/O/Square/Triangle, if you will).
 *
 * One turns a joystick into a gamepad by providing a magic configuration
 * string, which tells SDL the details of a specific device: when you see this
 * specific hardware, if button 2 gets pressed, this is actually D-Pad Up,
 * etc.
 *
 * SDL has many popular controllers configured out of the box, and users can
 * add their own controller details through an environment variable if it's
 * otherwise unknown to SDL.
 *
 * In order to use these functions, SDL_Init() must have been called with the
 * SDL_INIT_GAMEPAD flag. This causes SDL to scan the system for gamepads, and
 * load appropriate drivers.
 *
 * If you would like to receive gamepad updates while the application is in
 * the background, you should set the following hint before calling
 * SDL_Init(): SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMapping([MarshalAs(UnmanagedType.LPUTF8Str)] string mapping);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddGamepadMapping(const char *mapping);</code>
    /// <summary>
    /// <para>Add support for gamepads that SDL is unaware of or change the binding of an
    /// existing gamepad.</para>
    /// <para>The mapping string has the format <c>GUID,name,mapping</c>, where GUID is the
    /// string value from <see cref="GetJoystickGUIDString"/>, name is the human readable
    /// string for the device and mappings are gamepad mappings to joystick ones.
    /// Under Windows there is a reserved GUID of "xinput" that covers all XInput
    /// devices. The mapping format for joystick is:</para>
    /// <list type="bullet">
    /// <item><c>bX</c>: a joystick button, index X</item>
    /// <item><c>hX.Y</c>: hat X with value Y</item>
    /// <item><c>aX</c>: axis X of the joystick</item>
    /// </list>
    /// <para>Buttons can be used as a gamepad axes and vice versa.</para>
    /// <para>This string shows an example of a valid mapping for a gamepad:</para>
    /// <code>"341a3608000000000000504944564944,Afterglow PS3 Controller,a:b1,b:b2,y:b3,x:b0,start:b9,guide:b12,back:b8,dpup:h0.1,dpleft:h0.8,dpdown:h0.4,dpright:h0.2,leftshoulder:b4,rightshoulder:b5,leftstick:b10,rightstick:b11,leftx:a0,lefty:a1,rightx:a2,righty:a3,lefttrigger:b6,righttrigger:b7"</code>
    /// </summary>
    /// <param name="mapping">the mapping string.</param>
    /// <returns><c>1</c> if a new mapping is added, <c>0</c> if an existing mapping is updated,
    /// <c>-1</c> on error; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    public static int AddGamepadMapping(string mapping) => SDL_AddGamepadMapping(mapping);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromIO(IntPtr src, [MarshalAs(SDLBool)] bool closeio);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddGamepadMappingsFromIO(SDL_IOStream *src, SDL_bool closeio);</code>
    /// <summary>
    /// <para>Load a set of gamepad mappings from an <see cref="IOStream"/>.</para>
    /// <para>You can call this function several times, if needed, to load different
    /// database files.</para>
    /// <para>If a new mapping is loaded for an already known gamepad GUID, the later
    /// version will overwrite the one currently loaded.</para>
    /// <para>Mappings not belonging to the current platform or with no platform field
    /// specified will be ignored (i.e. mappings for Linux will be ignored in
    /// Windows, etc).</para>
    /// <para>This function will load the text database entirely in memory before
    /// processing it, so take this into consideration if you are in a memory
    /// constrained environment.</para>
    /// </summary>
    /// <param name="src">the data stream for the mappings to be added.</param>
    /// <param name="closeIO"><c>true</c> if <see cref="CloseIO"/> should be called on <c>src</c> before returning,
    /// even in the case of an error.</param>
    /// <returns>the number of mappings added or <c>-1</c> on error; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="AddGamepadMappingsFromFile"/>
    /// <seealso cref="GetGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    public static int AddGamepadMappingsFromIO(IOStream src, bool closeIO) =>
        SDL_AddGamepadMappingsFromIO(src.Handle, closeIO);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddGamepadMappingsFromFile(const char *file);</code>
    /// <summary>
    /// <para>Load a set of gamepad mappings from a file.</para>
    /// <para>You can call this function several times, if needed, to load different
    /// database files.</para>
    /// <para>If a new mapping is loaded for an already known gamepad GUID, the later
    /// version will overwrite the one currently loaded.</para>
    /// <para>Mappings not belonging to the current platform or with no platform field
    /// specified will be ignored (i.e. mappings for Linux will be ignored in
    /// Windows, etc).</para>
    /// </summary>
    /// <param name="file">the mappings file to load.</param>
    /// <returns>the number of mappings added or <c>-1</c> on error; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="AddGamepadMappingsFromIO"/>
    /// <seealso cref="GetGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    public static int AddGamepadMappingsFromFile(string file) => SDL_AddGamepadMappingsFromFile(file);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ReloadGamepadMappings();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ReloadGamepadMappings(void);</code>
    /// <summary>
    /// <para>Reinitialize the SDL mapping database to its initial state.</para>
    /// <para>This will generate gamepad events as needed if device mappings change.</para>
    /// </summary>
    /// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int ReloadGamepadMappings() => SDL_ReloadGamepadMappings();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetGamepadMappings(out int count);
    /// <code>extern SDL_DECLSPEC const char * const * SDLCALL SDL_GetGamepadMappings(int *count);</code>
    /// <summary>
    /// <para>Get the current gamepad mappings.</para>
    /// <para>The returned pointer follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of mappings returned, can
    /// be <c>null</c>.</param>
    /// <returns>an array of the mapping strings, NULL-terminated. Returns <c>null</c> on
    /// error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string?[] GetGamepadMappings(out int count)
    {
        var arrayPtr = SDL_GetGamepadMappings(out count);

        if (arrayPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return [];
        }

        try
        {
            var mappings = new string?[count];
            var ptrArray = new IntPtr[count];
            Marshal.Copy(arrayPtr, ptrArray, 0, count);
            
            for (var i = 0; i < count; i++)
            {
                mappings[i] = Marshal.PtrToStringUTF8(ptrArray[i]);
            }

            return mappings;
            
        }
        finally
        {
            Free(arrayPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetGamepadMappingForGUID(GUID guid);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadMappingForGUID(SDL_JoystickGUID guid);</code>
    /// <summary>
    /// <para>Get the gamepad mapping string for a given GUID.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="guid">a structure containing the GUID for which a mapping is desired.</param>
    /// <returns>a mapping string or <c>null</c> on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetJoystickGUIDForID"/>
    /// <seealso cref="GetJoystickGUID"/>
    public static string? GetGamepadMappingForGUID(GUID guid) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadMappingForGUID(guid));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMapping(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadMapping(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the current mapping of a gamepad.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// <para>Details about mappings are discussed with <see cref="AddGamepadMapping"/>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad you want to get the current mapping for.</param>
    /// <returns>a string that has the gamepad's mapping or <c>null</c> if no mapping is
    /// available; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="GetGamepadMappingForID"/>
    /// <seealso cref="GetGamepadMappingForGUID"/>
    /// <seealso cref="SetGamepadMapping"/>
    public static string? GetGamepadMapping(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadMapping(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadMapping(uint instanceID, 
        [MarshalAs(UnmanagedType.LPUTF8Str)]string mapping);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetGamepadMapping(SDL_JoystickID instance_id, const char *mapping);</code>
    /// <summary>
    /// <para>Set the current mapping of a joystick or gamepad.</para>
    /// <para>Details about mappings are discussed with <see cref="AddGamepadMapping"/>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <param name="mapping">the mapping to use for this device, or <c>null</c> to clear the
    /// mapping.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="AddGamepadMapping"/>
    /// <seealso cref="GetGamepadMapping"/>
    public static int SetGamepadMapping(uint instanceID, string mapping) =>
        SDL_SetGamepadMapping(instanceID, mapping);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasGamepad();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasGamepad(void);</code>
    /// <summary>
    /// <para>Return whether a gamepad is currently connected.</para>
    /// </summary>
    /// <returns><c>true</c> if a gamepad is connected, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepads"/>
    public static bool HasGamepad() => SDL_HasGamepad();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepads(out int count);
    /// <code>extern SDL_DECLSPEC SDL_JoystickID *SDLCALL SDL_GetGamepads(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently connected gamepads.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of gamepads returned.</param>
    /// <returns>a <c>0</c> terminated array of joystick instance IDs which should be
    /// freed with <see cref="Free"/>, or <c>null</c> on error; call <see cref="GetError"/> for
    /// more details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasGamepad"/>
    /// <seealso cref="OpenGamepad"/>
    public static uint[] GetGamepads(out int count)
    {
        var gamepadsPtr = SDL_GetGamepads(out count);
        
        if (gamepadsPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return [];
        }
        
        try
        {
            var joystickArray = new int[count];
            Marshal.Copy(gamepadsPtr, joystickArray, 0, count);
            return Array.ConvertAll(joystickArray, item => (uint)item);
        }
        finally
        {
            Free(gamepadsPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_IsGamepad(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_IsGamepad(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Check if the given joystick is supported by the gamepad interface.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns><c>true</c> if the given joystick is supported by the gamepad
    /// interface, <c>false</c> if it isn't or it's an invalid index.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetJoysticks"/>
    /// <seealso cref="OpenGamepad"/>
    public static bool IsGamepad(uint instanceID) => SDL_IsGamepad(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadNameForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetGamepadNameForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent name of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the name of the selected gamepad. If no name can be found, this
    /// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadName"/>
    /// <seealso cref="GetGamepads"/>
    public static string? GetGamepadNameForID(uint instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadNameForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPathForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetGamepadPathForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent path of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the path of the selected gamepad. If no path can be found, this
    /// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL_GetGamepadPath"/>
    /// <seealso cref="SDL_GetGamepads"/>
    public static string? GetGamepadPathForID(uint instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadPathForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndexForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetGamepadPlayerIndexForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the player index of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the player index of a gamepad, or <c>-1</c> if it's not available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadPlayerIndex"/>
    /// <seealso cref="GetGamepads"/>
    public static int GetGamepadPlayerIndexForID(uint instanceID) => SDL_GetGamepadPlayerIndexForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GetGamepadGUIDForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_JoystickGUID SDLCALL SDL_GetGamepadGUIDForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation-dependent GUID of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the GUID of the selected gamepad. If called on an invalid index,
    /// this function returns a zero GUID.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <see cref="GUIDToString"/>
    /// <seealso cref="GetGamepadGUID"/>
    /// <seealso cref="GetGamepadGUIDString"/>
    /// <seealso cref="GetGamepads"/>
    public static GUID GetGamepadGUIDForID(uint instanceID) => SDL_GetGamepadGUIDForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendorForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadVendorForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the USB vendor ID of a gamepad, if available.</para>
    /// <para>This can be called before any gamepads are opened. If the vendor ID isn't
    /// available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the USB vendor ID of the selected gamepad. If called on an invalid
    /// index, this function returns zero.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadVendor"/>
    /// <seealso cref="GetGamepads"/>
    public static ushort GetGamepadVendorForID(uint instanceID) => SDL_GetGamepadVendorForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProductForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the USB product ID of a gamepad, if available.</para>
    /// <para>This can be called before any gamepads are opened. If the product ID isn't
    /// available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the USB product ID of the selected gamepad. If called on an
    /// invalid index, this function returns zero.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadProduct"/>
    /// <seealso cref="GetGamepads"/>
    public static ushort GetGamepadProductForID(uint instanceID) => SDL_GetGamepadProductForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersionForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProductVersionForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the product version of a gamepad, if available.</para>
    /// <para>This can be called before any gamepads are opened. If the product version
    /// isn't available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the product version of the selected gamepad. If called on an
    /// invalid index, this function returns zero.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadProductVersion"/>
    /// <seealso cref="GetGamepads"/>
    public static ushort GetGamepadProductVersionForID(uint instanceID) => 
        SDL_GetGamepadProductVersionForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetGamepadTypeForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the type of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the gamepad type.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadType"/>
    /// <seealso cref="GetGamepads"/>
    /// <seealso cref="GetRealGamepadTypeForID"/>
    public static GamepadType GetGamepadTypeForID(uint instanceID) => 
        SDL_GetGamepadTypeForID(instanceID);


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetRealGamepadTypeForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the type of a gamepad, ignoring any mapping override.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the gamepad type.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadTypeForID"/>
    /// <seealso cref="GetGamepads"/>
    /// <seealso cref="GetRealGamepadType"/>
    public static GamepadType GetRealGamepadTypeForID(uint instanceID) => 
        SDL_GetRealGamepadTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMappingForID(uint instanceID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetGamepadMappingForID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the mapping of a gamepad.</para>
    /// <para>This can be called before any gamepads are opened.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>the mapping string. Returns <c>NULL</c> if no mapping is available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepads"/>
    /// <seealso cref="GetGamepadMapping"/>
    public static string? GetGamepadMappingForID(uint instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadMappingForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenGamepad(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_Gamepad *SDLCALL SDL_OpenGamepad(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Open a gamepad for use.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID.</param>
    /// <returns>a gamepad identifier or <c>NULL</c> if an error occurred;
    /// call <see cref="SDL_GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseGamepad"/>
    /// <seealso cref="IsGamepad"/>
    public static Gamepad? OpenGamepad(uint instanceID)
    {
        var gamepadHandle = SDL_OpenGamepad(instanceID);
        return gamepadHandle != IntPtr.Zero ? new Gamepad(gamepadHandle) : null;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromID(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_Gamepad *SDLCALL SDL_GetGamepadFromID(SDL_JoystickID instance_id);</code>
    /// <summary>
    /// <para>Get the SDL_Gamepad associated with a joystick instance ID, if it has been opened.</para>
    /// </summary>
    /// <param name="instanceID">the joystick instance ID of the gamepad.</param>
    /// <returns>an <see cref="Gamepad"/> on success or <c>NULL</c> on failure or if it hasn't been opened yet; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Gamepad GetGamepadFromID(uint instanceID) => new(SDL_GetGamepadFromID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromPlayerIndex(int playerIndex);
    /// <code>extern SDL_DECLSPEC SDL_Gamepad *SDLCALL SDL_GetGamepadFromPlayerIndex(int player_index);</code>
    /// <summary>
    /// <para>Get the <see cref="Gamepad"/> associated with a player index.</para>
    /// </summary>
    /// <param name="playerIndex">the player index, which is different from the instance ID.</param>
    /// <returns>the <see cref="Gamepad"/> associated with the player index.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadPlayerIndex"/>
    /// <seealso cref="SetGamepadPlayerIndex"/>
    public static Gamepad GetGamepadFromPlayerIndex(int playerIndex) => 
        new(SDL_GetGamepadFromPlayerIndex(playerIndex));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadProperties(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetGamepadProperties(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the properties associated with an opened gamepad.</para>
    /// <para>These properties are shared with the underlying joystick object.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="PropGamepadCapMonoLedBoolean"/> true if this gamepad has an LED that has adjustable brightness</item>
    /// <item><see cref="PropGamepadCapRGBLedBoolean"/> true if this gamepad has an LED that has adjustable color</item>
    /// <item><see cref="PropGamepadCapPlayerLedBoolean"/> true if this gamepad has a player LED</item>
    /// <item><see cref="PropGamepadCapRumbleBoolean"/> true if this gamepad has left/right rumble</item>
    /// <item><see cref="PropGamepadCapTriggerRumbleBoolean"/> true if this gamepad has simple trigger rumble</item>
    /// </list>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by <see cref="OpenGamepad"/>.</param>
    /// <returns>a valid property ID on success or 0 on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetGamepadProperties(Gamepad gamepad) => SDL_GetGamepadProperties(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadID(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_GetGamepadID(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the instance ID of an opened gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by <see cref="OpenGamepad"/>.</param>
    /// <returns>the instance ID of the specified gamepad on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetGamepadID(Gamepad gamepad) => SDL_GetGamepadID(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadName(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetGamepadName(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the implementation-dependent name for an opened gamepad.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by <see cref="OpenGamepad"/>.</param>
    /// <returns>the implementation dependent name for the gamepad,
    /// or NULL if there is no name or the identifier passed is invalid.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadNameForID"/>
    public static string? GetGamepadName(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadName(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPath(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetGamepadPath(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the implementation-dependent path for an opened gamepad.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by <see cref="OpenGamepad"/>.</param>
    /// <returns>the implementation dependent path for the gamepad, or <c>NULL</c> if
    ///          there is no path or the identifier passed is invalid.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadPathForID"/>
    public static string? GetGamepadPath(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadPath(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadType(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetGamepadType(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the type of an opened gamepad.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad type, or <see cref="GamepadType.Unknown"/> if it's not
    ///          available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadTypeForID"/>
    public static GamepadType GetGamepadType(Gamepad gamepad) => SDL_GetGamepadType(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadType(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetRealGamepadType(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the type of an opened gamepad, ignoring any mapping override.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad type, or <see cref="GamepadType.Unknown"/> if it's not
    ///          available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRealGamepadTypeForID"/>
    public static GamepadType GetRealGamepadType(Gamepad gamepad) => SDL_GetRealGamepadType(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndex(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetGamepadPlayerIndex(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the player index of an opened gamepad.</para>
    /// <para>For XInput gamepads this returns the XInput user index.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the player index for gamepad, or <c>-1</c> if it's not available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetGamepadPlayerIndex"/>
    public static int GetGamepadPlayerIndex(Gamepad gamepad) => SDL_GetGamepadPlayerIndex(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadPlayerIndex(IntPtr gamepad, int playerIndex);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetGamepadPlayerIndex(SDL_Gamepad *gamepad, int player_index);</code>
    /// <summary>
    /// <para>Set the player index of an opened gamepad.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to adjust.</param>
    /// <param name="playerIndex">player index to assign to this gamepad, or <c>-1</c> to clear
    ///                     the player index and turn off player LEDs.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadPlayerIndex"/>
    public static int SetGamepadPlayerIndex(Gamepad gamepad, int playerIndex) =>
        SDL_SetGamepadPlayerIndex(gamepad.Handle, playerIndex);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendor(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadVendor(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the USB vendor ID of an opened gamepad, if available.</para>
    /// <para>If the vendor ID isn't available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the USB vendor ID, or zero if unavailable.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadVendorForID"/>
    public static ushort GetGamepadVendor(Gamepad gamepad) => SDL_GetGamepadVendor(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProduct(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProduct(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the USB product ID of an opened gamepad, if available.</para>
    /// <para>If the product ID isn't available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the USB product ID, or zero if unavailable.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadProductForID"/>
    public static ushort GetGamepadProduct(Gamepad gamepad) => SDL_GetGamepadProduct(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersion(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadProductVersion(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the product version of an opened gamepad, if available.</para>
    /// <para>If the product version isn't available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the USB product version, or zero if unavailable.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadProductVersionForID"/>
    public static ushort GetGamepadProductVersion(Gamepad gamepad) => SDL_GetGamepadProductVersion(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadFirmwareVersion(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetGamepadFirmwareVersion(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the firmware version of an opened gamepad, if available.</para>
    /// <para>If the firmware version isn't available this function returns <c>0</c>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad firmware version, or zero if unavailable.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static ushort GetGamepadFirmwareVersion(Gamepad gamepad) => SDL_GetGamepadFirmwareVersion(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadSerial(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadSerial(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the serial number of an opened gamepad, if available.</para>
    /// <para>Returns the serial number of the gamepad, or <c>NULL</c> if it is not available.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the serial number, or <c>NULL</c> if unavailable.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string? GetGamepadSerial(Gamepad gamepad) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadSerial(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetGamepadSteamHandle(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC Uint64 SDLCALL SDL_GetGamepadSteamHandle(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the Steam Input handle of an opened gamepad, if available.</para>
    /// <para>Returns an <c>InputHandle_t</c> for the gamepad that can be used with Steam Input
    /// API: <a href="https://partner.steamgames.com/doc/api/ISteamInput">ISteamInput</a>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the gamepad handle, or <c>0</c> if unavailable.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static ulong GetGamepadSteamHandle(Gamepad gamepad) => SDL_GetGamepadSteamHandle(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetGamepadConnectionState(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_JoystickConnectionState SDLCALL SDL_GetGamepadConnectionState(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the connection state of a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object to query.</param>
    /// <returns>the connection state on success or
    ///          <see cref="JoystickConnectionState.Invalid"/> on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static JoystickConnectionState GetGamepadConnectionState(Gamepad gamepad) =>
        SDL_GetGamepadConnectionState(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetGamepadPowerInfo(IntPtr gamepad, out int percent);
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
    ///                left, between 0 and 100, or <c>NULL</c> to ignore. This will be
    ///                filled in with <c>-1</c> we can't determine a value or there is no
    ///                battery.</param>
    /// <returns>the current battery state.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static PowerState GetGamepadPowerInfo(Gamepad gamepad, out int percent) =>
        SDL_GetGamepadPowerInfo(gamepad.Handle, out percent);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadConnected(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GamepadConnected(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Check if a gamepad has been opened and is currently connected.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    ///                <see cref="OpenGamepad"/>.</param>
    /// <returns><c>true</c> if the gamepad has been opened and is currently
    ///          connected, or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool GamepadConnected(Gamepad gamepad) => SDL_GamepadConnected(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC SDL_Joystick *SDLCALL SDL_GetGamepadJoystick(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the underlying joystick from a gamepad.</para>
    /// <para>This function will give you a <see cref="Joystick"/> object, which allows you to use
    /// the <see cref="Joystick"/> functions with a <see cref="Gamepad"/> object. This would be useful
    /// for getting a joystick's position at any given time, even if it hasn't
    /// moved (moving it would produce an event, which would have the axis' value).</para>
    /// <para>The pointer returned is owned by the <see cref="Gamepad"/>. You should not call
    /// <see cref="CloseJoystick"/> on it, for example, since doing so will likely cause
    /// SDL to crash.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad object that you want to get a joystick from.</param>
    /// <returns>an <see cref="Joystick"/> object; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Joystick GetGamepadJoystick(Gamepad gamepad) => new(SDL_GetGamepadJoystick(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGamepadEventsEnabled([MarshalAs(SDLBool)] bool enabled);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGamepadEventsEnabled(SDL_bool enabled);</code>
    /// <summary>
    /// <para>Set the state of gamepad event processing.</para>
    /// <para>If gamepad events are disabled, you must call <see cref="UpdateGamepads"/> yourself
    /// and check the state of the gamepad when you want gamepad information.</para>
    /// </summary>
    /// <param name="enabled">whether to process gamepad events or not.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GamepadEventsEnabled"/>
    /// <seealso cref="UpdateGamepads"/>
    public static void SetGamepadEventsEnabled(bool enabled) => SDL_SetGamepadEventsEnabled(enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadEventsEnabled();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GamepadEventsEnabled(void);</code>
    /// <summary>
    /// <para>Query the state of gamepad event processing.</para>
    /// <para>If gamepad events are disabled, you must call <see cref="UpdateGamepads"/> yourself
    /// and check the state of the gamepad when you want gamepad information.</para>
    /// </summary>
    /// <returns><c>true</c> if gamepad events are being processed, <c>false</c>
    ///          otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetGamepadEventsEnabled"/>

    public static bool GamepadEventsEnabled() => SDL_GamepadEventsEnabled();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadBindings(IntPtr gamepad, out int count);
    /// <code>extern SDL_DECLSPEC SDL_GamepadBinding **SDLCALL SDL_GetGamepadBindings(SDL_Gamepad *gamepad, int *count);</code>
    /// <summary>
    /// <para>Get the SDL joystick layer bindings for a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="count">a pointer filled in with the number of bindings returned.</param>
    /// <returns>a NULL terminated array of pointers to bindings which should be
    ///          freed with <c>SDL_free()</c>, or <c>NULL</c> on error; call <see cref="GetError"/> for
    ///          more details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static GamepadBinding[]? GetGamepadBindings(Gamepad gamepad, out int count)
    {
        var bindingsPtr = SDL_GetGamepadBindings(gamepad.Handle, out count);

        if (bindingsPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return null;
        }

        try
        {
            var bindings = new GamepadBinding[count];
            for (var i = 0; i < count; i++)
            {
                var bindPtr = Marshal.ReadIntPtr(bindingsPtr, i * IntPtr.Size);
                bindings[i] = Marshal.PtrToStructure<GamepadBinding>(bindPtr);
            }

            return bindings;
        }
        finally
        {
            Free(bindingsPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateGamepads();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateGamepads(void);</code>
    /// <summary>
    /// <para>Manually pump gamepad updates if not using the loop.</para>
    /// <para>This function is called automatically by the event loop if events are
    /// enabled. Under such circumstances, it will not be necessary to call this
    /// function.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void UpdateGamepads() => SDL_UpdateGamepads();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    /// <code>extern SDL_DECLSPEC SDL_GamepadType SDLCALL SDL_GetGamepadTypeFromString(const char *str);</code>
    /// <summary>
    /// <para>Convert a string into <see cref="GamepadType"/> enum.</para>
    /// <para>This function is called internally to translate <see cref="Gamepad"/> mapping strings
    /// for the underlying joystick device into the consistent <see cref="GamepadType"/> mapping.
    /// You do not normally need to call this function unless you are parsing
    /// SDL_Gamepad mappings in your own code.</para>
    /// </summary>
    /// <param name="str">string representing a <see cref="GamepadType"/> type.</param>
    /// <returns>the <see cref="GamepadType"/> enum corresponding to the input string, or
    ///          <see cref="GamepadType.Unknown"/> if no match was found.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadStringForType"/>
    public static GamepadType GetGamepadTypeFromString(string str) => SDL_GetGamepadTypeFromString(str);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForType(GamepadType type);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetGamepadStringForType(SDL_GamepadType type);</code>
    /// <summary>
    /// <para>Convert from an <c>SDL_GamepadType</c> enum to a string.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="type">an enum value for a given <c>SDL_GamepadType</c>.</param>
    /// <returns>a string for the given type, or <c>NULL</c> if an invalid type is
    ///          specified. The string returned is of the format used by
    ///          <see cref="Gamepad"/> mapping strings.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadTypeFromString"/>
    public static string? GetGamepadStringForType(GamepadType type) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadStringForType(type));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadAxis SDL_GetGamepadAxisFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    /// <code>extern SDL_DECLSPEC SDL_GamepadAxis SDLCALL SDL_GetGamepadAxisFromString(const char *str);</code>
    /// <summary>
    /// <para>Convert a string into <see cref="GamepadAxis"/> enum.</para>
    /// <para>This function is called internally to translate <see cref="Gamepad"/> mapping strings
    /// for the underlying joystick device into the consistent <see cref="Gamepad"/> mapping.
    /// You do not normally need to call this function unless you are parsing
    /// <see cref="Gamepad"/> mappings in your own code.</para>
    /// <para>Note specially that <c>"righttrigger"</c> and <c>"lefttrigger"</c> map to
    /// <see cref="GamepadAxis.RightTrigger"/> and <see cref="GamepadAxis.LeftTrigger"/>,
    /// respectively.</para>
    /// </summary>
    /// <param name="str">string representing a SDL_Gamepad axis.</param>
    /// <returns>the <see cref="GamepadAxis"/> enum corresponding to the input string, or
    ///          <see cref="GamepadAxis.Invalid"/> if no match was found.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadStringForAxis"/>
    public static GamepadAxis GetGamepadAxisFromString(string str) => SDL_GetGamepadAxisFromString(str);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForAxis(GamepadAxis axis);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis);</code>
    /// <summary>
    /// <para>Convert from an <see cref="GamepadAxis"/> enum to a string.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="axis">an enum value for a given <see cref="GamepadAxis"/>.</param>
    /// <returns>a string for the given axis, or <c>NULL</c> if an invalid axis is
    ///          specified. The string returned is of the format used by
    ///          <see cref="Gamepad"/> mapping strings.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadAxisFromString"/>
    public static string? GetGamepadStringForAxis(GamepadAxis axis) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadStringForAxis(axis));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadHasAxis(IntPtr gamepad, GamepadAxis axis);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GamepadHasAxis(SDL_Gamepad *gamepad, SDL_GamepadAxis axis);</code>
    /// <summary>
    /// <para>Query whether a gamepad has a given axis.</para>
    /// <para>This merely reports whether the gamepad's mapping defined this axis, as
    /// that is all the information SDL has about the physical device.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="axis">an axis enum value (an <see cref="GamepadAxis"/> value).</param>
    /// <returns><c>true</c> if the gamepad has this axis, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GamepadHasButton"/>
    /// <seealso cref="GetGamepadAxis"/>
    public static bool GamepadHasAxis(Gamepad gamepad, GamepadAxis axis) => SDL_GamepadHasAxis(gamepad.Handle, axis);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetGamepadAxis(IntPtr gamepad, GamepadAxis axis);
    /// <code>extern SDL_DECLSPEC Sint16 SDLCALL SDL_GetGamepadAxis(SDL_Gamepad *gamepad, SDL_GamepadAxis axis);</code>
    /// <summary>
    /// <para>Get the current state of an axis control on a gamepad.</para>
    /// <para>The axis indices start at index 0.</para>
    /// <para>For thumbsticks, the state is a value ranging from -32768 (up/left) to
    /// 32767 (down/right).</para>
    /// <para>Triggers range from 0 when released to 32767 when fully pressed, and never
    /// return a negative value. Note that this differs from the value reported by
    /// the lower-level <see cref="GetJoystickAxis"/>, which normally uses the full range.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="axis">an axis index (one of the <see cref="GamepadAxis"/> values).</param>
    /// <returns>axis state (including 0) on success or 0 (also) on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GamepadHasAxis"/>
    /// <seealso cref="GetGamepadButton"/>
    public static short GetGamepadAxis(Gamepad gamepad, GamepadAxis axis) => SDL_GetGamepadAxis(gamepad.Handle, axis);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButton SDL_GetGamepadButtonFromString(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    /// <code>extern SDL_DECLSPEC SDL_GamepadButton SDLCALL SDL_GetGamepadButtonFromString(const char *str);</code>
    /// <summary>
    /// <para>Convert a string into an <see cref="GamepadButton"/> enum.</para>
    /// <para>This function is called internally to translate <see cref="Gamepad"/> mapping strings
    /// for the underlying joystick device into the consistent <see cref="Gamepad"/> mapping.
    /// You do not normally need to call this function unless you are parsing
    /// <see cref="Gamepad"/> mappings in your own code.</para>
    /// </summary>
    /// <param name="str">string representing a <see cref="Gamepad"/> axis.</param>
    /// <returns>the <see cref="GamepadButton"/> enum corresponding to the input string, or
    ///          <see cref="GamepadButton.Invalid"/> if no match was found.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadStringForButton"/>
    public static GamepadButton GetGamepadButtonFromString(string str) => SDL_GetGamepadButtonFromString(str);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForButton(GamepadButton button);
    /// <code>extern SDL_DECLSPEC const char* SDLCALL SDL_GetGamepadStringForButton(SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Convert from an <see cref="GamepadButton"/> enum to a string.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="button">an enum value for a given <see cref="GamepadButton"/>.</param>
    /// <returns>a string for the given button, or <c>NULL</c> if an invalid button is
    ///          specified. The string returned is of the format used by
    ///          <see cref="Gamepad"/> mapping strings.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadButtonFromString"/>
    public static string? GetGamepadStringForButton(GamepadButton button) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadStringForButton(button));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadHasButton(IntPtr gamepad, GamepadButton button);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GamepadHasButton(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Query whether a gamepad has a given button.</para>
    /// <para>This merely reports whether the gamepad's mapping defined this button, as
    /// that is all the information SDL has about the physical device.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button enum value (an <see cref="GamepadButton"/> value).</param>
    /// <returns><c>true</c> if the gamepad has this button, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GamepadHasAxis"/>
    public static bool GamepadHasButton(Gamepad gamepad, GamepadButton button) => 
        SDL_GamepadHasButton(gamepad.Handle, button);
    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte SDL_GetGamepadButton(IntPtr gamepad, GamepadButton button);
    /// <code>extern SDL_DECLSPEC Uint8 SDLCALL SDL_GetGamepadButton(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Get the current state of a button on a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button index (one of the <see cref="GamepadButton"/> values).</param>
    /// <returns><c>1</c> for pressed state or <c>0</c> for not pressed state or error; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GamepadHasButton"/>
    /// <seealso cref="GetGamepadAxis"/>
    public static byte GetGamepadButton(Gamepad gamepad, GamepadButton button) => 
        SDL_GetGamepadButton(gamepad.Handle, button);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabelForType(GamepadType type, 
        GamepadButton button);
    /// <code>extern SDL_DECLSPEC SDL_GamepadButtonLabel SDLCALL SDL_GetGamepadButtonLabelForType(SDL_GamepadType type, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Get the label of a button on a gamepad.</para>
    /// </summary>
    /// <param name="type">the type of gamepad to check.</param>
    /// <param name="button">a button index (one of the <see cref="GamepadButton"/> values).</param>
    /// <returns>the <see cref="GamepadButtonLabel"/> enum corresponding to the button label.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadButtonLabel"/>
    public static GamepadButtonLabel GetGamepadButtonLabelForType(GamepadType type, GamepadButton button) =>
        SDL_GetGamepadButtonLabelForType(type, button);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabel(IntPtr gamepad, GamepadButton button);
    /// <code>extern SDL_DECLSPEC SDL_GamepadButtonLabel SDLCALL SDL_GetGamepadButtonLabel(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Get the label of a button on a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="button">a button index (one of the <see cref="GamepadButton"/> values).</param>
    /// <returns>the <see cref="GamepadButtonLabel"/> enum corresponding to the button label.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadButtonLabelForType"/>
    public static GamepadButtonLabel GetGamepadButtonLabel(Gamepad gamepad, GamepadButton button) =>
        SDL_GetGamepadButtonLabel(gamepad.Handle, button);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpads(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumGamepadTouchpads(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Get the number of touchpads on a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <returns>number of touchpads.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetNumGamepadTouchpadFingers"/>
    public static int GetNumGamepadTouchpads(Gamepad gamepad) => SDL_GetNumGamepadTouchpads(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumGamepadTouchpadFingers(SDL_Gamepad *gamepad, int touchpad);</code>
    /// <summary>
    /// <para>Get the number of supported simultaneous fingers on a touchpad on a game
    /// gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="touchpad">a touchpad.</param>
    /// <returns>number of supported simultaneous fingers.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadTouchpadFinger"/>
    /// <seealso cref="GetNumGamepadTouchpads"/>
    public static int GetNumGamepadTouchpadFingers(Gamepad gamepad, int touchpad) =>
        SDL_GetNumGamepadTouchpadFingers(gamepad.Handle, touchpad);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, out byte state,
        out float x, out float y, out float pressure);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetGamepadTouchpadFinger(SDL_Gamepad *gamepad, int touchpad, int finger, Uint8 *state, float *x, float *y, float *pressure);</code>
    /// <summary>
    /// <para>Get the current state of a finger on a touchpad on a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad.</param>
    /// <param name="touchpad">a touchpad.</param>
    /// <param name="finger">a finger.</param>
    /// <param name="state">filled with state.</param>
    /// <param name="x">filled with x position, normalized 0 to 1, with the origin in the
    ///          upper left.</param>
    /// <param name="y">filled with y position, normalized 0 to 1, with the origin in the
    ///          upper left.</param>
    /// <param name="pressure">filled with pressure value.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetNumGamepadTouchpadFingers"/>
    public static int GetGamepadTouchpadFinger(Gamepad gamepad, int touchpad, int finger, out byte state, out float x,
        out float y, out float pressure) =>
        SDL_GetGamepadTouchpadFinger(gamepad.Handle, touchpad, finger, out state, out x, out y, out pressure);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadHasSensor(IntPtr gamepad, SensorType type);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GamepadHasSensor(SDL_Gamepad *gamepad, SDL_SensorType type);</code>
    /// <summary>
    /// <para>Return whether a gamepad has a particular sensor.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <returns><c>true</c> if the sensor exists, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadSensorData"/>
    /// <seealso cref="GetGamepadSensorDataRate"/>
    /// <seealso cref="SetGamepadSensorEnabled"/>
    public static bool GamepadHasSensor(Gamepad gamepad, SensorType type) => SDL_GamepadHasSensor(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadSensorEnabled(IntPtr gamepad, SensorType type, 
        [MarshalAs(SDLBool)]bool enabled);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetGamepadSensorEnabled(SDL_Gamepad *gamepad, SDL_SensorType type, SDL_bool enabled);</code>
    /// <summary>
    /// <para>Set whether data reporting for a gamepad sensor is enabled.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to update.</param>
    /// <param name="type">the type of sensor to enable/disable.</param>
    /// <param name="enabled">whether data reporting should be enabled.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GamepadHasSensor"/>
    /// <seealso cref="GamepadSensorEnabled"/>
    public static int SetGamepadSensorEnabled(Gamepad gamepad, SensorType type, bool enabled) =>
        SDL_SetGamepadSensorEnabled(gamepad.Handle, type, enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadSensorEnabled(IntPtr gamepd, SensorType type);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GamepadSensorEnabled(SDL_Gamepad *gamepad, SDL_SensorType type);</code>
    /// <summary>
    /// <para>Query whether sensor data reporting is enabled for a gamepad.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <returns><c>true</c> if the sensor is enabled, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetGamepadSensorEnabled"/>
    public static bool GamepadSensorEnabled(Gamepad gamepad, SensorType type) =>
        SDL_GamepadSensorEnabled(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetGamepadSensorDataRate(IntPtr gamepad, SensorType type);
    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetGamepadSensorDataRate(SDL_Gamepad *gamepad, SDL_SensorType type);</code>
    /// <summary>
    /// <para>Get the data rate (number of events per second) of a gamepad sensor.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <returns>the data rate, or <c>0.0f</c> if the data rate is not available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static float GetGamepadSensorDataRate(Gamepad gamepad, SensorType type) =>
        SDL_GetGamepadSensorDataRate(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadSensorData(IntPtr gamepad, SensorType type, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] data, int numValues);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetGamepadSensorData(SDL_Gamepad *gamepad, SDL_SensorType type, float *data, int num_values);</code>
    /// <summary>
    /// <para>Get the current state of a gamepad sensor.</para>
    /// <para>The number of values and interpretation of the data is sensor dependent.
    /// See <a href="https://github.com/libsdl-org/SDL/blob/main/src/joystick/SDL_sensor.h">SDL_sensor.h</a>
    /// for the details for each type of sensor.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="type">the type of sensor to query.</param>
    /// <param name="data">a pointer filled with the current sensor state.</param>
    /// <param name="numValues">the number of values to write to data.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetGamepadSensorData(Gamepad gamepad, SensorType type, out float[] data, int numValues)
    {
        data = new float[numValues];
        return SDL_GetGamepadSensorData(gamepad.Handle, type, data, numValues);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RumbleGamepad(IntPtr gamepad, ushort lowFrequencyRumble,
        ushort highFrequencyRumble, ushort durationMs);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RumbleGamepad(SDL_Gamepad *gamepad, Uint16 low_frequency_rumble, Uint16 high_frequency_rumble, Uint32 duration_ms);</code>
    /// <summary>
    /// <para>Start a rumble effect on a gamepad.</para>
    /// <para>Each call to this function cancels any previous rumble effect, and calling
    /// it with <c>0</c> intensity stops any rumbling.</para>
    /// <para>This function requires you to process SDL events or call
    /// <see cref="UpdateJoysticks"/> to update rumble state.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to vibrate.</param>
    /// <param name="lowFrequencyRumble">the intensity of the low frequency (left)
    ///                             rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="highFrequencyRumble">the intensity of the high frequency (right)
    ///                              rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
    /// <returns>0, or -1 if rumble isn't supported on this gamepad.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int RumbleGamepad(Gamepad gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble,
        ushort durationMs) =>
        SDL_RumbleGamepad(gamepad.Handle, lowFrequencyRumble, highFrequencyRumble, durationMs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort leftRumble, ushort rightRumble, 
        ushort durationMs);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_RumbleGamepadTriggers(SDL_Gamepad *gamepad, Uint16 left_rumble, Uint16 right_rumble, Uint32 duration_ms);</code>
    /// <summary>
    /// <para>Start a rumble effect in the gamepad's triggers.</para>
    /// <para>Each call to this function cancels any previous trigger rumble effect, and
    /// calling it with <c>0</c> intensity stops any rumbling.</para>
    /// <para>Note that this is rumbling of the <i>triggers</i> and not the gamepad as a
    /// whole. This is currently only supported on Xbox One gamepads. If you want
    /// the (more common) whole-gamepad rumble, use <see cref="RumbleGamepad"/> instead.</para>
    /// <para>This function requires you to process SDL events or call
    /// <see cref="UpdateJoysticks"/> to update rumble state.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to vibrate.</param>
    /// <param name="leftRumble">the intensity of the left trigger rumble motor, from 0
    ///                    to 0xFFFF.</param>
    /// <param name="rightRumble">the intensity of the right trigger rumble motor, from 0
    ///                     to 0xFFFF.</param>
    /// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="RumbleGamepad"/>
    public static int RumbleGamepadTriggers(Gamepad gamepad, ushort leftRumble, ushort rightRumble,
        ushort durationMs) => SDL_RumbleGamepadTriggers(gamepad.Handle, leftRumble, rightRumble, durationMs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetGamepadLED(SDL_Gamepad *gamepad, Uint8 red, Uint8 green, Uint8 blue);</code>
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
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int SetGamepadLED(Gamepad gamepad, byte red, byte green, byte blue) =>
        SDL_SetGamepadLED(gamepad.Handle, red, green, blue);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SendGamepadEffect(IntPtr gamepad, IntPtr data, int size);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SendGamepadEffect(SDL_Gamepad *gamepad, const void *data, int size);</code>
    /// <summary>
    /// <para>Send a gamepad specific effect packet.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to affect.</param>
    /// <param name="data">the data to send to the gamepad.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int SendGamepadEffect(Gamepad gamepad, byte[] data)
    {
        var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        var dataPtr = handle.AddrOfPinnedObject();

        try 
        {
            return SDL_SendGamepadEffect(gamepad.Handle, dataPtr, data.Length);
        } 
        finally 
        {
            handle.Free();
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseGamepad(IntPtr gamepad);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseGamepad(SDL_Gamepad *gamepad);</code>
    /// <summary>
    /// <para>Close a gamepad previously opened with <see cref="OpenGamepad"/>.</para>
    /// </summary>
    /// <param name="gamepad">a gamepad identifier previously returned by
    ///                <see cref="OpenGamepad"/>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void CloseGamepad(Gamepad gamepad) => SDL_CloseGamepad(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, GamepadButton button);
    /// <code>extern SDL_DECLSPEC const char* SDLCALL SDL_GetGamepadAppleSFSymbolsNameForButton(SDL_Gamepad *gamepad, SDL_GamepadButton button);</code>
    /// <summary>
    /// <para>Return the sfSymbolsName for a given button on a gamepad on Apple platforms.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="button">a button on the gamepad.</param>
    /// <returns>the sfSymbolsName or NULL if the name can't be found.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SDL_GetGamepadAppleSFSymbolsNameForAxis"/>
    public static string? GetGamepadAppleSFSymbolsNameForButton(Gamepad gamepad, GamepadButton button) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadAppleSFSymbolsNameForButton(gamepad.Handle, button));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, GamepadAxis axis);
    /// <code>extern SDL_DECLSPEC const char* SDLCALL SDL_GetGamepadAppleSFSymbolsNameForAxis(SDL_Gamepad *gamepad, SDL_GamepadAxis axis);</code>
    /// <summary>
    /// <para>Return the sfSymbolsName for a given axis on a gamepad on Apple platforms.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="gamepad">the gamepad to query.</param>
    /// <param name="axis">an axis on the gamepad.</param>
    /// <returns>the sfSymbolsName or <c>NULL</c> if the name can't be found.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetGamepadAppleSFSymbolsNameForButton"/>
    public static string? GetGamepadAppleSFSymbolsNameForAxis(Gamepad gamepad, GamepadAxis axis) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadAppleSFSymbolsNameForAxis(gamepad.Handle, axis));
    
}