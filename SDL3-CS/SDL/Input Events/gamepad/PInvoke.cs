using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMapping([MarshalAs(UnmanagedType.LPUTF8Str)] string mapping);
    public static int AddGamepadMapping(string mapping) => SDL_AddGamepadMapping(mapping);
    

    public static int AddGamepadMappingsFromIO(Stream src, bool closeio = true)
    {
        var reader = new StreamReader(src);

        while (!reader.EndOfStream)
        {
            var mapping = reader.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(mapping))
            {
                return AddGamepadMapping(mapping);
            }
        }

        if (closeio) reader.Close();
        return -1;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    public static int AddGamepadMappingsFromFile(string file) => SDL_AddGamepadMappingsFromFile(file);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ReloadGamepadMappings();
    public static int ReloadGamepadMappings() => SDL_ReloadGamepadMappings();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetGamepadMappings(out int count);
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
    public static string? GetGamepadMappingForGUID(GUID guid) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadMappingForGUID(guid));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMapping(IntPtr gamepad);
    public static string? GetGamepadMapping(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadMapping(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadMapping(uint instanceID, 
        [MarshalAs(UnmanagedType.LPUTF8Str)]string mapping);
    public static int SetGamepadMapping(uint instanceID, string mapping) =>
        SDL_SetGamepadMapping(instanceID, mapping);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasGamepad();
    public static bool HasGamepad() => SDL_HasGamepad();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepads(out int count);
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
    public static bool IsGamepad(uint instanceID) => SDL_IsGamepad(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadNameForID(uint instanceID);

    public static string? GetGamepadNameForID(uint instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadNameForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPathForID(uint instanceID);
    public static string? GetGamepadPathForID(uint instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadPathForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndexForID(uint instanceID);
    public static int GetGamepadPlayerIndexForID(uint instanceID) => SDL_GetGamepadPlayerIndexForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GetGamepadGUIDForID(uint instanceID);
    public static GUID GetGamepadGUIDForID(uint instanceID) => SDL_GetGamepadGUIDForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendorForID(uint instanceID);
    public static ushort GetGamepadVendorForID(uint instanceID) => SDL_GetGamepadVendorForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductForID(uint instanceID);
    public static ushort GetGamepadProductForID(uint instanceID) => SDL_GetGamepadProductForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersionForID(uint instanceID);
    public static ushort GetGamepadProductVersionForID(uint instanceID) => 
        SDL_GetGamepadProductVersionForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeForID(uint instanceID);
    public static GamepadType GetGamepadTypeForID(uint instanceID) => 
        SDL_GetGamepadTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instanceID);
    public static GamepadType GetRealGamepadTypeForID(uint instanceID) => 
        SDL_GetRealGamepadTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMappingForID(uint instanceID);
    public static string? GetGamepadMappingForID(uint instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadMappingForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenGamepad(uint instanceID);
    public static Gamepad OpenGamepad(uint instanceID) => new(SDL_OpenGamepad(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromID(uint instanceID);
    public static Gamepad GetGamepadFromID(uint instanceID) => new(SDL_GetGamepadFromID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromPlayerIndex(int playerIndex);
    public static Gamepad GetGamepadFromPlayerIndex(int playerIndex) => 
        new(SDL_GetGamepadFromPlayerIndex(playerIndex));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadProperties(IntPtr gamepad);
    public static uint GetGamepadProperties(Gamepad gamepad) => SDL_GetGamepadProperties(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGamepadID(IntPtr gamepad);
    public static uint GetGamepadID(Gamepad gamepad) => SDL_GetGamepadID(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadName(IntPtr gamepad);
    public static string? GetGamepadName(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadName(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPath(IntPtr gamepad);
    public static string? GetGamepadPath(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadPath(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadType(IntPtr gamepad);
    public static GamepadType GetGamepadType(Gamepad gamepad) => SDL_GetGamepadType(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadType(IntPtr gamepad);
    public static GamepadType GetRealGamepadType(Gamepad gamepad) => SDL_GetRealGamepadType(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndex(IntPtr gamepad);
    public static int GetGamepadPlayerIndex(Gamepad gamepad) => SDL_GetGamepadPlayerIndex(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadPlayerIndex(IntPtr gamepad, int playerIndex);
    public static int SetGamepadPlayerIndex(Gamepad gamepad, int playerIndex) =>
        SDL_SetGamepadPlayerIndex(gamepad.Handle, playerIndex);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendor(IntPtr gamepad);
    public static ushort GetGamepadVendor(Gamepad gamepad) => SDL_GetGamepadVendor(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProduct(IntPtr gamepad);
    public static ushort GetGamepadProduct(Gamepad gamepad) => SDL_GetGamepadProduct(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersion(IntPtr gamepad);
    public static ushort GetGamepadProductVersion(Gamepad gamepad) => SDL_GetGamepadProductVersion(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadFirmwareVersion(IntPtr gamepad);
    public static ushort GetGamepadFirmwareVersion(Gamepad gamepad) => SDL_GetGamepadFirmwareVersion(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadSerial(IntPtr gamepad);
    public static string? GetGamepadSerial(Gamepad gamepad) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadSerial(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetGamepadSteamHandle(IntPtr gamepad);
    public static ulong GetGamepadSteamHandle(Gamepad gamepad) => SDL_GetGamepadSteamHandle(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState SDL_GetGamepadConnectionState(IntPtr gamepad);
    public static JoystickConnectionState GetGamepadConnectionState(Gamepad gamepad) =>
        SDL_GetGamepadConnectionState(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetGamepadPowerInfo(IntPtr gamepad, out int percent);
    public static PowerState GetGamepadPowerInfo(Gamepad gamepad, out int percent) =>
        SDL_GetGamepadPowerInfo(gamepad.Handle, out percent);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadConnected(IntPtr gamepad);
    public static bool GamepadConnected(Gamepad gamepad) => SDL_GamepadConnected(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);
    public static Joystick GetGamepadJoystick(Gamepad gamepad) => new(SDL_GetGamepadJoystick(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGamepadEventsEnabled([MarshalAs(SDLBool)] bool enabled);
    public static void SetGamepadEventsEnabled(bool enabled) => SDL_SetGamepadEventsEnabled(enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadEventsEnabled();
    public static bool GamepadEventsEnabled() => SDL_GamepadEventsEnabled();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadBindings(IntPtr gamepad, out int count);

    public static GamepadBinding[] GetGamepadBindings(Gamepad gamepad, out int count)
    {
        var bindingsPtr = SDL_GetGamepadBindings(gamepad.Handle, out count);

        if (bindingsPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return [];
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
    public static void UpdateGamepads() => SDL_UpdateGamepads();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    public static GamepadType GetGamepadTypeFromString(string str) => SDL_GetGamepadTypeFromString(str);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForType(GamepadType type);
    public static string? GetGamepadStringForType(GamepadType type) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadStringForType(type));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadAxis SDL_GetGamepadAxisFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    public static GamepadAxis GetGamepadAxisFromString(string str) => SDL_GetGamepadAxisFromString(str);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForAxis(GamepadAxis axis);
    public static string? GetGamepadStringForAxis(GamepadAxis axis) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadStringForAxis(axis));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadHasAxis(IntPtr gamepad, GamepadAxis axis);
    public static bool GamepadHasAxis(Gamepad gamepad, GamepadAxis axis) => SDL_GamepadHasAxis(gamepad.Handle, axis);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short SDL_GetGamepadAxis(IntPtr gamepad, GamepadAxis axis);
    public static short GetGamepadAxis(Gamepad gamepad, GamepadAxis axis) => SDL_GetGamepadAxis(gamepad.Handle, axis);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButton SDL_GetGamepadButtonFromString(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    public static GamepadButton GetGamepadButtonFromString(string str) => SDL_GetGamepadButtonFromString(str);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadStringForButton(GamepadButton button);
    public static string? GetGamepadStringForButton(GamepadButton button) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadStringForButton(button));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadHasButton(IntPtr gamepad, GamepadButton button);
    public static bool GamepadHasButton(Gamepad gamepad, GamepadButton button) => 
        SDL_GamepadHasButton(gamepad.Handle, button);
    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte SDL_GetGamepadButton(IntPtr gamepad, GamepadButton button);
    public static byte GetGamepadButton(Gamepad gamepad, GamepadButton button) => 
        SDL_GetGamepadButton(gamepad.Handle, button);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabelForType(GamepadType type, 
        GamepadButton button);
    public static GamepadButtonLabel GetGamepadButtonLabelForType(GamepadType type, GamepadButton button) =>
        SDL_GetGamepadButtonLabelForType(type, button);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel SDL_GetGamepadButtonLabel(IntPtr gamepad, GamepadButton button);
    public static GamepadButtonLabel GetGamepadButtonLabel(Gamepad gamepad, GamepadButton button) =>
        SDL_GetGamepadButtonLabel(gamepad.Handle, button);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpads(IntPtr gamepad);
    public static int GetNumGamepadTouchpads(Gamepad gamepad) => SDL_GetNumGamepadTouchpads(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad);
    public static int GetNumGamepadTouchpadFingers(Gamepad gamepad, int touchpad) =>
        SDL_GetNumGamepadTouchpadFingers(gamepad.Handle, touchpad);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, out byte state,
        out float x, out float y, out float pressure);

    public static int GetGamepadTouchpadFinger(Gamepad gamepad, int touchpad, int finger, out byte state, out float x,
        out float y, out float pressure) =>
        SDL_GetGamepadTouchpadFinger(gamepad.Handle, touchpad, finger, out state, out x, out y, out pressure);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadHasSensor(IntPtr gamepad, SensorType type);
    public static bool GamepadHasSensor(Gamepad gamepad, SensorType type) => SDL_GamepadHasSensor(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadSensorEnabled(IntPtr gamepad, SensorType type, 
        [MarshalAs(SDLBool)]bool enabled);
    public static int SetGamepadSensorEnabled(Gamepad gamepad, SensorType type, bool enabled) =>
        SDL_SetGamepadSensorEnabled(gamepad.Handle, type, enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GamepadSensorEnabled(IntPtr gamepd, SensorType type);
    public static bool GamepadSensorEnabled(Gamepad gamepad, SensorType type) =>
        SDL_GamepadSensorEnabled(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetGamepadSensorDataRate(IntPtr gamepad, SensorType type);
    public static float GetGamepadSensorDataRate(Gamepad gamepad, SensorType type) =>
        SDL_GetGamepadSensorDataRate(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadSensorData(IntPtr gamepad, SensorType type, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] data, int numValues);

    public static int GetGamepadSensorData(Gamepad gamepad, SensorType type, out float[] data, int numValues)
    {
        data = new float[numValues];
        return SDL_GetGamepadSensorData(gamepad.Handle, type, data, numValues);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RumbleGamepad(IntPtr gamepad, ushort lowFrequencyRumble,
        ushort highFrequencyRumble, ushort durationMs);

    public static int RumbleGamepad(Gamepad gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble,
        ushort durationMs) =>
        SDL_RumbleGamepad(gamepad.Handle, lowFrequencyRumble, highFrequencyRumble, durationMs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort leftRumble, ushort rightRumble, 
        ushort durationMs);
    public static int RumbleGamepadTriggers(Gamepad gamepad, ushort leftRumble, ushort rightRumble,
        ushort durationMs) => SDL_RumbleGamepadTriggers(gamepad.Handle, leftRumble, rightRumble, durationMs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue);
    public static int SetGamepadLED(Gamepad gamepad, byte red, byte green, byte blue) =>
        SDL_SetGamepadLED(gamepad.Handle, red, green, blue);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SendGamepadEffect(IntPtr gamepad, IntPtr data, int size);
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
    public static void CloseGamepad(Gamepad gamepad) => SDL_CloseGamepad(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, GamepadButton button);
    public static string? GetGamepadAppleSFSymbolsNameForButton(Gamepad gamepad, GamepadButton button) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadAppleSFSymbolsNameForButton(gamepad.Handle, button));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, GamepadAxis axis);
    public static string? GetGamepadAppleSFSymbolsNameForAxis(Gamepad gamepad, GamepadAxis axis) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadAppleSFSymbolsNameForAxis(gamepad.Handle, axis));
    
}