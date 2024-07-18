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
            return [];
        }

        var mappings = new string?[count];
        var ptrArray = new IntPtr[count];
        Marshal.Copy(arrayPtr, ptrArray, 0, count);
        Free(arrayPtr);

        for (var i = 0; i < count; i++)
        {
            mappings[i] = Marshal.PtrToStringAnsi(ptrArray[i]);
        }

        return mappings;
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
    private static partial int SDL_SetGamepadMapping(uint instance_id, 
        [MarshalAs(UnmanagedType.LPUTF8Str)]string mapping);
    public static int SetGamepadMapping(uint instance_id, string mapping) =>
        SDL_SetGamepadMapping(instance_id, mapping);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasGamepad();
    public static bool HasGamepad() => SDL_HasGamepad();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepads(out int count);
    public static uint[] GetGamepads(out int cout)
    {
        var pArray = SDL_GetGamepads(out cout);
        var joystickArray = new int[cout];
        Marshal.Copy(pArray, joystickArray, 0, cout);
        Free(pArray);
        return Array.ConvertAll(joystickArray, item => (uint)item);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_IsGamepad(uint instance_id);
    public static bool IsGamepad(uint instance_id) => SDL_IsGamepad(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadNameForID(uint instance_id);

    public static string? GetGamepadNameForID(uint instance_id) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadNameForID(instance_id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadPathForID(uint instance_id);
    public static string? GetGamepadPathForID(uint instance_id) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadPathForID(instance_id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetGamepadPlayerIndexForID(uint instance_id);
    public static int GetGamepadPlayerIndexForID(uint instance_id) => SDL_GetGamepadPlayerIndexForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GetGamepadGUIDForID(uint instance_id);
    public static GUID GetGamepadGUIDForID(uint instance_id) => SDL_GetGamepadGUIDForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadVendorForID(uint instance_id);
    public static ushort GetGamepadVendorForID(uint instance_id) => SDL_GetGamepadVendorForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductForID(uint instance_id);
    public static ushort GetGamepadProductForID(uint instance_id) => SDL_GetGamepadProductForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort SDL_GetGamepadProductVersionForID(uint instance_id);
    public static ushort GetGamepadProductVersionForID(uint instance_id) => 
        SDL_GetGamepadProductVersionForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetGamepadTypeForID(uint instance_id);
    public static GamepadType GetGamepadTypeForID(uint instance_id) => 
        SDL_GetGamepadTypeForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType SDL_GetRealGamepadTypeForID(uint instance_id);
    public static GamepadType GetRealGamepadTypeForID(uint instance_id) => 
        SDL_GetRealGamepadTypeForID(instance_id);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMappingForID(uint instance_id);
    public static string? GetGamepadMappingForID(uint instance_id) =>
        Marshal.PtrToStringUTF8(SDL_GetGamepadMappingForID(instance_id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenGamepad(uint instance_id);
    public static Gamepad OpenGamepad(uint instance_id) => new(SDL_OpenGamepad(instance_id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromID(uint instance_id);
    public static Gamepad GetGamepadFromID(uint instance_id) => new(SDL_GetGamepadFromID(instance_id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadFromPlayerIndex(int player_index);
    public static Gamepad GetGamepadFromPlayerIndex(int player_index) => 
        new(SDL_GetGamepadFromPlayerIndex(player_index));
    
    
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
    private static partial int SDL_SetGamepadPlayerIndex(IntPtr gamepad, int player_index);
    public static int SetGamepadPlayerIndex(Gamepad gamepad, int player_index) =>
        SDL_SetGamepadPlayerIndex(gamepad.Handle, player_index);
    
    
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
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadConnected(IntPtr gamepad);
    public static bool GamepadConnected(Gamepad gamepad) => SDL_GamepadConnected(gamepad.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);
    public static Joystick GetGamepadJoystick(Gamepad gamepad) => new(SDL_GetGamepadJoystick(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGamepadEventsEnabled([MarshalAs(UnmanagedType.I1)] bool enabled);
    public static void SetGamepadEventsEnabled(bool enabled) => SDL_SetGamepadEventsEnabled(enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
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

        var sizeOfBinding = Marshal.SizeOf<GamepadBinding>();
        var bindings = new GamepadBinding[count];

        for (var i = 0; i < count; i++)
        {
            var currentPtr = bindingsPtr + i * sizeOfBinding;
            bindings[i]  = Marshal.PtrToStructure<GamepadBinding>(currentPtr);
        }
        
        Free(bindingsPtr);

        return bindings;
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
    [return: MarshalAs(UnmanagedType.I1)]
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
    [return: MarshalAs(UnmanagedType.I1)]
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
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GamepadHasSensor(IntPtr gamepad, SensorType type);
    public static bool GamepadHasSensor(Gamepad gamepad, SensorType type) => SDL_GamepadHasSensor(gamepad.Handle, type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetGamepadSensorEnabled(IntPtr gamepad, SensorType type, 
        [MarshalAs(UnmanagedType.I1)]bool enabled);
    public static int SetGamepadSensorEnabled(Gamepad gamepad, SensorType type, bool enabled) =>
        SDL_SetGamepadSensorEnabled(gamepad.Handle, type, enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
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
    private static partial int SDL_GetGamepadSensorData(IntPtr gamepad, SensorType type, IntPtr data, int num_values);

    public static int GetGamepadSensorData(Gamepad gamepad, SensorType type, out float[] data, int num_values)
    {
        data = new float[num_values];
        var dataPtr = Marshal.AllocHGlobal(num_values * sizeof(float));

        try
        {
            var result = SDL_GetGamepadSensorData(gamepad.Handle, type, dataPtr, num_values);
            if (result == 0)
            {
                Marshal.Copy(dataPtr, data, 0, num_values);
            }
            else
            {
                data = [];
            }

            return result;
        }
        finally
        {
            Free(dataPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RumbleGamepad(IntPtr gamepad, ushort low_frequency_rumble,
        ushort high_frequency_rumble, ushort duration_ms);

    public static int RumbleGamepad(Gamepad gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble,
        ushort duration_ms) =>
        SDL_RumbleGamepad(gamepad.Handle, low_frequency_rumble, high_frequency_rumble, duration_ms);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort left_rumble, ushort right_rumble, 
        ushort duration_ms);
    public static int RumbleGamepadTriggers(Gamepad gamepad, ushort left_rumble, ushort right_rumble,
        ushort duration_ms) => SDL_RumbleGamepadTriggers(gamepad.Handle, left_rumble, right_rumble, duration_ms);
    
    
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
        var size = data.Length;
        var dataPtr = Marshal.AllocHGlobal(size);

        try
        {
            Marshal.Copy(data, 0, dataPtr, size);
            return SDL_SendGamepadEffect(gamepad.Handle, dataPtr, size);
        }
        finally
        {
            Free(dataPtr);
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