using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Gamepad;

internal static class PInvokeTests
{
    private static string? capturedMapping;
    private static string? capturedFile;
    private static string? capturedString;
    private static IntPtr capturedSource;
    private static bool capturedCloseIO;
    private static bool capturedEnabled;
    private static uint capturedInstanceId;
    private static IntPtr capturedGamepad;
    private static int capturedPlayerIndex;
    private static int capturedTouchpad;
    private static int capturedFinger;
    private static SDL3.SDL.GamepadType capturedGamepadType;
    private static SDL3.SDL.GamepadAxis capturedAxis;
    private static SDL3.SDL.GamepadButton capturedButton;
    private static SDL3.SDL.SensorType capturedSensorType;
    private static SDL3.SDL.GamepadCapSenseType capturedCapSenseType;
    private static ushort capturedLowFrequencyRumble;
    private static ushort capturedHighFrequencyRumble;
    private static ushort capturedLeftRumble;
    private static ushort capturedRightRumble;
    private static uint capturedDurationMs;
    private static byte capturedRed;
    private static byte capturedGreen;
    private static byte capturedBlue;
    private static IntPtr capturedEffectData;
    private static byte[]? capturedEffectBytes;
    private static int capturedSize;
    private static int capturedCount;
    private static IntPtr capturedFreeMemory;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static int nextInt;
    private static uint nextUInt;
    private static ushort nextUShort;
    private static ulong nextULong;
    private static short nextShort;
    private static SDL3.SDL.GUID nextGuid;
    private static SDL3.SDL.GamepadType nextGamepadType;
    private static SDL3.SDL.GamepadAxis nextGamepadAxis;
    private static SDL3.SDL.GamepadButton nextGamepadButton;
    private static SDL3.SDL.GamepadButtonLabel nextGamepadButtonLabel;
    private static SDL3.SDL.JoystickConnectionState nextConnectionState;
    private static SDL3.SDL.PowerState nextPowerState;
    private static int nextPercent;
    private static bool nextBool;
    private static bool nextDown;
    private static float nextFloat;
    private static float nextX;
    private static float nextY;
    private static float nextPressure;
    private static float[]? nextFloatArray;
    private static IntPtr nextPointer;
    private static int nextCount;

    public static void RunAll()
    {
        AddGamepadMapping_ForwardsMappingAndReturnsNativeValue();
        AddGamepadMappingsFromIO_ForwardsSourceCloseioAndReturnsNativeValue();
        AddGamepadMappingsFromFile_ForwardsFileAndReturnsNativeValue();
        ReloadGamepadMappings_ReturnsNativeValue();
        SDL_GetGamepadMappings_UsesExpectedNativeMetadata();
        GetGamepadMappings_ReturnsArrayNullAndFreesNativePointer();
        SDL_GetGamepadMappingForGUID_UsesExpectedNativeMetadata();
        GetGamepadMappingForGUID_ReturnsStringNullAndFreesNativePointer();
        SDL_GetGamepadMapping_UsesExpectedNativeMetadata();
        GetGamepadMapping_ReturnsStringNullAndFreesNativePointer();
        SetGamepadMapping_ForwardsInstanceMappingAndReturnsNativeValue();
        HasGamepad_ReturnsNativeValue();
        SDL_GetGamepads_UsesExpectedNativeMetadata();
        GetGamepads_ReturnsArrayNullAndFreesNativePointer();
        IsGamepad_ForwardsInstanceAndReturnsNativeValue();
        SDL_GetGamepadNameForID_UsesExpectedNativeMetadata();
        GetGamepadNameForID_ReturnsStringAndNull();
        SDL_GetGamepadPathForID_UsesExpectedNativeMetadata();
        GetGamepadPathForID_ReturnsStringAndNull();
        GetGamepadPlayerIndexForID_ForwardsInstanceAndReturnsNativeValue();
        GetGamepadGUIDForID_ForwardsInstanceAndReturnsNativeValue();
        GetGamepadVendorForID_ForwardsInstanceAndReturnsNativeValue();
        GetGamepadProductForID_ForwardsInstanceAndReturnsNativeValue();
        GetGamepadProductVersionForID_ForwardsInstanceAndReturnsNativeValue();
        GetGamepadTypeForID_ForwardsInstanceAndReturnsNativeValue();
        GetRealGamepadTypeForID_ForwardsInstanceAndReturnsNativeValue();
        SDL_GetGamepadMappingForID_UsesExpectedNativeMetadata();
        GetGamepadMappingForID_ReturnsStringNullAndFreesNativePointer();
        OpenGamepad_ForwardsInstanceAndReturnsNativePointer();
        GetGamepadFromID_ForwardsInstanceAndReturnsNativePointer();
        GetGamepadFromPlayerIndex_ForwardsPlayerIndexAndReturnsNativePointer();
        GetGamepadProperties_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadID_ForwardsGamepadAndReturnsNativeValue();
        SDL_GetGamepadName_UsesExpectedNativeMetadata();
        GetGamepadName_ReturnsStringAndNull();
        SDL_GetGamepadPath_UsesExpectedNativeMetadata();
        GetGamepadPath_ReturnsStringAndNull();
        GetGamepadType_ForwardsGamepadAndReturnsNativeValue();
        GetRealGamepadType_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadPlayerIndex_ForwardsGamepadAndReturnsNativeValue();
        SetGamepadPlayerIndex_ForwardsGamepadPlayerIndexAndReturnsNativeValue();
        GetGamepadVendor_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadProduct_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadProductVersion_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadFirmwareVersion_ForwardsGamepadAndReturnsNativeValue();
        SDL_GetGamepadSerial_UsesExpectedNativeMetadata();
        GetGamepadSerial_ReturnsStringAndNull();
        GetGamepadSteamHandle_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadConnectionState_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadPowerInfo_ForwardsGamepadAndOutputsPercent();
        GamepadConnected_ForwardsGamepadAndReturnsNativeValue();
        GetGamepadJoystick_ForwardsGamepadAndReturnsNativePointer();
        SetGamepadEventsEnabled_ForwardsEnabled();
        GamepadEventsEnabled_ReturnsNativeValue();
        SDL_GetGamepadBindings_UsesExpectedNativeMetadata();
        GetGamepadBindings_ReturnsArrayNullAndFreesNativePointer();
        UpdateGamepads_CallsNativeHook();
        GetGamepadTypeFromString_ForwardsStringAndReturnsNativeValue();
        SDL_GetGamepadStringForType_UsesExpectedNativeMetadata();
        GetGamepadStringForType_ReturnsStringAndNull();
        GetGamepadAxisFromString_ForwardsStringAndReturnsNativeValue();
        SDL_GetGamepadStringForAxis_UsesExpectedNativeMetadata();
        GetGamepadStringForAxis_ReturnsStringAndNull();
        GamepadHasAxis_ForwardsGamepadAxisAndReturnsNativeValue();
        GetGamepadAxis_ForwardsGamepadAxisAndReturnsNativeValue();
        GetGamepadButtonFromString_ForwardsStringAndReturnsNativeValue();
        SDL_GetGamepadStringForButton_UsesExpectedNativeMetadata();
        GetGamepadStringForButton_ReturnsStringAndNull();
        GamepadHasButton_ForwardsGamepadButtonAndReturnsNativeValue();
        GetGamepadButton_ForwardsGamepadButtonAndReturnsNativeValue();
        GetGamepadButtonLabelForType_ForwardsTypeButtonAndReturnsNativeValue();
        GetGamepadButtonLabel_ForwardsGamepadButtonAndReturnsNativeValue();
        GetNumGamepadTouchpads_ForwardsGamepadAndReturnsNativeValue();
        GetNumGamepadTouchpadFingers_ForwardsGamepadTouchpadAndReturnsNativeValue();
        GetGamepadTouchpadFinger_ForwardsArgumentsOutputsValuesAndReturnsNativeValue();
        GamepadHasSensor_ForwardsGamepadSensorAndReturnsNativeValue();
        SetGamepadSensorEnabled_ForwardsGamepadSensorEnabledAndReturnsNativeValue();
        GamepadSensorEnabled_ForwardsGamepadSensorAndReturnsNativeValue();
        GetGamepadSensorDataRate_ForwardsGamepadSensorAndReturnsNativeValue();
        GetGamepadSensorData_ForwardsGamepadSensorDataAndReturnsNativeValue();
        GamepadHasCapSense_ForwardsGamepadCapSenseAndReturnsNativeValue();
        GetGamepadCapSense_ForwardsGamepadCapSenseAndReturnsNativeValue();
        RumbleGamepad_ForwardsRumbleValuesAndReturnsNativeValue();
        RumbleGamepadTriggers_ForwardsTriggerRumbleValuesAndReturnsNativeValue();
        SetGamepadLED_ForwardsColorAndReturnsNativeValue();
        SendGamepadEffect_WithPointer_ForwardsDataAndReturnsNativeValue();
        SendGamepadEffect_WithArray_ForwardsDataAndReturnsNativeValue();
        CloseGamepad_ForwardsGamepad();
        SDL_GetGamepadAppleSFSymbolsNameForButton_UsesExpectedNativeMetadata();
        GetGamepadAppleSFSymbolsNameForButton_ReturnsStringAndNull();
        SDL_GetGamepadAppleSFSymbolsNameForAxis_UsesExpectedNativeMetadata();
        GetGamepadAppleSFSymbolsNameForAxis_ReturnsStringAndNull();
    }

    public static void AddGamepadMapping_ForwardsMappingAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AddGamepadMapping");
        AssertSdlImport(nativeMethod, "SDL_AddGamepadMapping");
        AssertStringParameterMarshal(nativeMethod, "mapping");

        ResetCaptureState();
        nextInt = 1;

        using NativeHookScope _ = NativeHookScope.Install("AddGamepadMappingNativeFunction", nameof(CaptureAddGamepadMapping));
        int result = SDL3.SDL.AddGamepadMapping("03000000deadc0de,Test Pad,a:b0");

        TestAssert.Equal(1, result, "SDL.AddGamepadMapping must return the native hook value.");
        TestAssert.Equal("03000000deadc0de,Test Pad,a:b0", capturedMapping, "SDL.AddGamepadMapping must forward mapping.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AddGamepadMapping must call the native hook once.");
    }

    public static void AddGamepadMappingsFromIO_ForwardsSourceCloseioAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AddGamepadMappingsFromIO");
        AssertSdlImport(nativeMethod, "SDL_AddGamepadMappingsFromIO");
        AssertBoolParameterMarshal(nativeMethod, "closeio");

        ResetCaptureState();
        nextInt = 3;

        using NativeHookScope _ = NativeHookScope.Install("AddGamepadMappingsFromIONativeFunction", nameof(CaptureAddGamepadMappingsFromIO));
        int result = SDL3.SDL.AddGamepadMappingsFromIO((IntPtr)0x1010, closeio: true);

        TestAssert.Equal(3, result, "SDL.AddGamepadMappingsFromIO must return the native hook value.");
        TestAssert.Equal((IntPtr)0x1010, capturedSource, "SDL.AddGamepadMappingsFromIO must forward source.");
        TestAssert.Equal(true, capturedCloseIO, "SDL.AddGamepadMappingsFromIO must forward closeio.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AddGamepadMappingsFromIO must call the native hook once.");
    }

    public static void AddGamepadMappingsFromFile_ForwardsFileAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AddGamepadMappingsFromFile");
        AssertSdlImport(nativeMethod, "SDL_AddGamepadMappingsFromFile");
        AssertStringParameterMarshal(nativeMethod, "file");

        ResetCaptureState();
        nextInt = 4;

        using NativeHookScope _ = NativeHookScope.Install("AddGamepadMappingsFromFileNativeFunction", nameof(CaptureAddGamepadMappingsFromFile));
        int result = SDL3.SDL.AddGamepadMappingsFromFile("gamecontrollerdb.txt");

        TestAssert.Equal(4, result, "SDL.AddGamepadMappingsFromFile must return the native hook value.");
        TestAssert.Equal("gamecontrollerdb.txt", capturedFile, "SDL.AddGamepadMappingsFromFile must forward file.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AddGamepadMappingsFromFile must call the native hook once.");
    }

    public static void ReloadGamepadMappings_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ReloadGamepadMappings");
        AssertSdlImport(nativeMethod, "SDL_ReloadGamepadMappings");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("ReloadGamepadMappingsNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.ReloadGamepadMappings();

        TestAssert.Equal(true, result, "SDL.ReloadGamepadMappings must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ReloadGamepadMappings must call the native hook once.");
    }

    public static void SDL_GetGamepadMappings_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadMappings");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadMappings");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetGamepadMappings_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr first = Marshal.StringToCoTaskMemUTF8("mapping-a");
        IntPtr second = Marshal.StringToCoTaskMemUTF8("mapping-b");
        IntPtr array = Marshal.AllocHGlobal(IntPtr.Size * 2);

        try
        {
            Marshal.WriteIntPtr(array, 0, first);
            Marshal.WriteIntPtr(array, IntPtr.Size, second);
            nextPointer = array;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetGamepadMappingsNativeFunction", nameof(CaptureArrayPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            string[]? mappings = SDL3.SDL.GetGamepadMappings(out int count);

            TestAssert.NotNull(mappings, "SDL.GetGamepadMappings must convert native mappings.");
            TestAssert.Equal(2, mappings!.Length, "SDL.GetGamepadMappings must preserve native mapping count.");
            TestAssert.Equal("mapping-a", mappings[0], "SDL.GetGamepadMappings must convert mapping 0.");
            TestAssert.Equal("mapping-b", mappings[1], "SDL.GetGamepadMappings must convert mapping 1.");
            TestAssert.Equal(2, count, "SDL.GetGamepadMappings must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetGamepadMappings must free the native array pointer.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            mappings = SDL3.SDL.GetGamepadMappings(out count);

            TestAssert.Equal<string[]?>(null, mappings, "SDL.GetGamepadMappings must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetGamepadMappings must return native count for native null.");
            TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.GetGamepadMappings must pass native null to SDL.Free.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadMappings must call the native hook for both branches.");
            TestAssert.Equal(2, capturedFreeCallCount, "SDL.GetGamepadMappings must call SDL.Free for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(first);
            Marshal.FreeCoTaskMem(second);
            Marshal.FreeHGlobal(array);
        }
    }

    public static void SDL_GetGamepadMappingForGUID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadMappingForGUID");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadMappingForGUID");
    }

    public static void GetGamepadMappingForGUID_ReturnsStringNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr mapping = Marshal.StringToCoTaskMemUTF8("guid-mapping");

        try
        {
            nextPointer = mapping;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetGamepadMappingForGUIDNativeFunction", nameof(CaptureGetGamepadMappingForGUID));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            string? result = SDL3.SDL.GetGamepadMappingForGUID(default);

            TestAssert.Equal("guid-mapping", result, "SDL.GetGamepadMappingForGUID must convert native UTF-8 strings.");
            TestAssert.Equal(mapping, capturedFreeMemory, "SDL.GetGamepadMappingForGUID must free the native string pointer.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadMappingForGUID(default);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadMappingForGUID must return null for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadMappingForGUID must call the native hook for both branches.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetGamepadMappingForGUID must only free non-null native strings.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(mapping);
        }
    }

    public static void SDL_GetGamepadMapping_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadMapping");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadMapping");
    }

    public static void GetGamepadMapping_ReturnsStringNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr mapping = Marshal.StringToCoTaskMemUTF8("gamepad-mapping");

        try
        {
            nextPointer = mapping;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetGamepadMappingNativeFunction", nameof(CaptureGetGamepadMapping));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            string? result = SDL3.SDL.GetGamepadMapping((IntPtr)0x2020);

            TestAssert.Equal("gamepad-mapping", result, "SDL.GetGamepadMapping must convert native UTF-8 strings.");
            TestAssert.Equal((IntPtr)0x2020, capturedGamepad, "SDL.GetGamepadMapping must forward gamepad.");
            TestAssert.Equal(mapping, capturedFreeMemory, "SDL.GetGamepadMapping must free the native string pointer.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadMapping((IntPtr)0x3030);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadMapping must return null for native null.");
            TestAssert.Equal((IntPtr)0x3030, capturedGamepad, "SDL.GetGamepadMapping must forward gamepad for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadMapping must call the native hook for both branches.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetGamepadMapping must only free non-null native strings.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(mapping);
        }
    }

    public static void SetGamepadMapping_ForwardsInstanceMappingAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGamepadMapping");
        AssertSdlImport(nativeMethod, "SDL_SetGamepadMapping");
        AssertBoolReturnMarshal(nativeMethod);
        AssertStringParameterMarshal(nativeMethod, "mapping");

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetGamepadMappingNativeFunction", nameof(CaptureSetGamepadMapping));
        bool result = SDL3.SDL.SetGamepadMapping(77, "current-mapping");

        TestAssert.Equal(true, result, "SDL.SetGamepadMapping must return the native hook value.");
        TestAssert.Equal(77u, capturedInstanceId, "SDL.SetGamepadMapping must forward instanceID.");
        TestAssert.Equal("current-mapping", capturedMapping, "SDL.SetGamepadMapping must forward mapping.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGamepadMapping must call the native hook once.");
    }

    public static void HasGamepad_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasGamepad");
        AssertSdlImport(nativeMethod, "SDL_HasGamepad");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasGamepadNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.HasGamepad();

        TestAssert.Equal(true, result, "SDL.HasGamepad must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasGamepad must call the native hook once.");
    }

    public static void SDL_GetGamepads_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepads");
        AssertSdlImport(nativeMethod, "SDL_GetGamepads");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetGamepads_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr array = CreateNativeUIntArray(10, 20, 30);

        try
        {
            nextPointer = array;
            nextCount = 3;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetGamepadsNativeFunction", nameof(CaptureArrayPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            uint[]? gamepads = SDL3.SDL.GetGamepads(out int count);

            TestAssert.NotNull(gamepads, "SDL.GetGamepads must convert native gamepad IDs.");
            TestAssert.Equal(3, gamepads!.Length, "SDL.GetGamepads must preserve native gamepad count.");
            TestAssert.Equal(10u, gamepads[0], "SDL.GetGamepads must convert gamepad 0.");
            TestAssert.Equal(20u, gamepads[1], "SDL.GetGamepads must convert gamepad 1.");
            TestAssert.Equal(30u, gamepads[2], "SDL.GetGamepads must convert gamepad 2.");
            TestAssert.Equal(3, count, "SDL.GetGamepads must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetGamepads must free the native array pointer.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            gamepads = SDL3.SDL.GetGamepads(out count);

            TestAssert.Equal<uint[]?>(null, gamepads, "SDL.GetGamepads must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetGamepads must return native count for native null.");
            TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.GetGamepads must pass native null to SDL.Free.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepads must call the native hook for both branches.");
            TestAssert.Equal(2, capturedFreeCallCount, "SDL.GetGamepads must call SDL.Free for both branches.");
        }
        finally
        {
            Marshal.FreeHGlobal(array);
        }
    }

    public static void IsGamepad_ForwardsInstanceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsGamepad");
        AssertSdlImport(nativeMethod, "SDL_IsGamepad");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("IsGamepadNativeFunction", nameof(CaptureInstanceBool));
        bool result = SDL3.SDL.IsGamepad(99);

        TestAssert.Equal(true, result, "SDL.IsGamepad must return the native hook value.");
        TestAssert.Equal(99u, capturedInstanceId, "SDL.IsGamepad must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IsGamepad must call the native hook once.");
    }

    public static void SDL_GetGamepadNameForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadNameForID");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadNameForID");
    }

    public static void GetGamepadNameForID_ReturnsStringAndNull()
    {
        AssertInstanceStringMethod(
            "SDL.GetGamepadNameForID",
            "GetGamepadNameForIDNativeFunction",
            nameof(CaptureInstancePointer),
            () => SDL3.SDL.GetGamepadNameForID(111),
            () => SDL3.SDL.GetGamepadNameForID(112));
    }

    public static void SDL_GetGamepadPathForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadPathForID");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadPathForID");
    }

    public static void GetGamepadPathForID_ReturnsStringAndNull()
    {
        AssertInstanceStringMethod(
            "SDL.GetGamepadPathForID",
            "GetGamepadPathForIDNativeFunction",
            nameof(CaptureInstancePointer),
            () => SDL3.SDL.GetGamepadPathForID(111),
            () => SDL3.SDL.GetGamepadPathForID(112));
    }

    public static void GetGamepadPlayerIndexForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceIntMethod(
            "SDL.GetGamepadPlayerIndexForID",
            "SDL_GetGamepadPlayerIndexForID",
            "GetGamepadPlayerIndexForIDNativeFunction",
            121,
            2);
    }

    public static void GetGamepadGUIDForID_ForwardsInstanceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadGUIDForID");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadGUIDForID");

        ResetCaptureState();
        nextGuid = CreateGuid(0x5A);

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadGUIDForIDNativeFunction", nameof(CaptureInstanceGuid));
        SDL3.SDL.GUID result = SDL3.SDL.GetGamepadGUIDForID(122);

        TestAssert.Equal((byte)0x5A, ReadGuidByte(result, 0), "SDL.GetGamepadGUIDForID must return the native hook GUID.");
        TestAssert.Equal(122u, capturedInstanceId, "SDL.GetGamepadGUIDForID must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadGUIDForID must call the native hook once.");
    }

    public static void GetGamepadVendorForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceUShortMethod(
            "SDL.GetGamepadVendorForID",
            "SDL_GetGamepadVendorForID",
            "GetGamepadVendorForIDNativeFunction",
            123,
            0x045E);
    }

    public static void GetGamepadProductForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceUShortMethod(
            "SDL.GetGamepadProductForID",
            "SDL_GetGamepadProductForID",
            "GetGamepadProductForIDNativeFunction",
            124,
            0x02FF);
    }

    public static void GetGamepadProductVersionForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceUShortMethod(
            "SDL.GetGamepadProductVersionForID",
            "SDL_GetGamepadProductVersionForID",
            "GetGamepadProductVersionForIDNativeFunction",
            125,
            0x0102);
    }

    public static void GetGamepadTypeForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceGamepadTypeMethod(
            "SDL.GetGamepadTypeForID",
            "SDL_GetGamepadTypeForID",
            "GetGamepadTypeForIDNativeFunction",
            126,
            SDL3.SDL.GamepadType.PS5);
    }

    public static void GetRealGamepadTypeForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceGamepadTypeMethod(
            "SDL.GetRealGamepadTypeForID",
            "SDL_GetRealGamepadTypeForID",
            "GetRealGamepadTypeForIDNativeFunction",
            127,
            SDL3.SDL.GamepadType.Steam);
    }

    public static void SDL_GetGamepadMappingForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadMappingForID");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadMappingForID");
    }

    public static void GetGamepadMappingForID_ReturnsStringNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr mapping = Marshal.StringToCoTaskMemUTF8("id-mapping");

        try
        {
            nextPointer = mapping;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetGamepadMappingForIDNativeFunction", nameof(CaptureGetGamepadMappingForID));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            string? result = SDL3.SDL.GetGamepadMappingForID(128);

            TestAssert.Equal("id-mapping", result, "SDL.GetGamepadMappingForID must convert native UTF-8 strings.");
            TestAssert.Equal(128u, capturedInstanceId, "SDL.GetGamepadMappingForID must forward instanceId.");
            TestAssert.Equal(mapping, capturedFreeMemory, "SDL.GetGamepadMappingForID must free the native string pointer.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadMappingForID(129);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadMappingForID must return null for native null.");
            TestAssert.Equal(129u, capturedInstanceId, "SDL.GetGamepadMappingForID must forward instanceId for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadMappingForID must call the native hook for both branches.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetGamepadMappingForID must only free non-null native strings.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(mapping);
        }
    }

    public static void OpenGamepad_ForwardsInstanceAndReturnsNativePointer()
    {
        AssertInstancePointerMethod("SDL.OpenGamepad", "SDL_OpenGamepad", "OpenGamepadNativeFunction", 130, (IntPtr)0x5001);
    }

    public static void GetGamepadFromID_ForwardsInstanceAndReturnsNativePointer()
    {
        AssertInstancePointerMethod("SDL.GetGamepadFromID", "SDL_GetGamepadFromID", "GetGamepadFromIDNativeFunction", 131, (IntPtr)0x5002);
    }

    public static void GetGamepadFromPlayerIndex_ForwardsPlayerIndexAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadFromPlayerIndex");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadFromPlayerIndex");

        ResetCaptureState();
        nextPointer = (IntPtr)0x5003;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadFromPlayerIndexNativeFunction", nameof(CapturePlayerIndexPointer));
        IntPtr result = SDL3.SDL.GetGamepadFromPlayerIndex(4);

        TestAssert.Equal((IntPtr)0x5003, result, "SDL.GetGamepadFromPlayerIndex must return the native hook pointer.");
        TestAssert.Equal(4, capturedPlayerIndex, "SDL.GetGamepadFromPlayerIndex must forward playerIndex.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadFromPlayerIndex must call the native hook once.");
    }

    public static void GetGamepadProperties_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadUIntMethod("SDL.GetGamepadProperties", "SDL_GetGamepadProperties", "GetGamepadPropertiesNativeFunction", (IntPtr)0x5101, 42);
    }

    public static void GetGamepadID_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadUIntMethod("SDL.GetGamepadID", "SDL_GetGamepadID", "GetGamepadIDNativeFunction", (IntPtr)0x5102, 132);
    }

    public static void SDL_GetGamepadName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadName");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadName");
    }

    public static void GetGamepadName_ReturnsStringAndNull()
    {
        AssertGamepadStringMethod(
            "SDL.GetGamepadName",
            "GetGamepadNameNativeFunction",
            nameof(CaptureGamepadPointer),
            (IntPtr)0x5201,
            (IntPtr)0x5202,
            () => SDL3.SDL.GetGamepadName((IntPtr)0x5201),
            () => SDL3.SDL.GetGamepadName((IntPtr)0x5202));
    }

    public static void SDL_GetGamepadPath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadPath");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadPath");
    }

    public static void GetGamepadPath_ReturnsStringAndNull()
    {
        AssertGamepadStringMethod(
            "SDL.GetGamepadPath",
            "GetGamepadPathNativeFunction",
            nameof(CaptureGamepadPointer),
            (IntPtr)0x5203,
            (IntPtr)0x5204,
            () => SDL3.SDL.GetGamepadPath((IntPtr)0x5203),
            () => SDL3.SDL.GetGamepadPath((IntPtr)0x5204));
    }

    public static void GetGamepadType_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadTypeMethod("SDL.GetGamepadType", "SDL_GetGamepadType", "GetGamepadTypeNativeFunction", (IntPtr)0x5301, SDL3.SDL.GamepadType.XboxOne);
    }

    public static void GetRealGamepadType_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadTypeMethod("SDL.GetRealGamepadType", "SDL_GetRealGamepadType", "GetRealGamepadTypeNativeFunction", (IntPtr)0x5302, SDL3.SDL.GamepadType.NintendoSwitchPro);
    }

    public static void GetGamepadPlayerIndex_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadIntMethod("SDL.GetGamepadPlayerIndex", "SDL_GetGamepadPlayerIndex", "GetGamepadPlayerIndexNativeFunction", (IntPtr)0x5303, 5);
    }

    public static void SetGamepadPlayerIndex_ForwardsGamepadPlayerIndexAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGamepadPlayerIndex");
        AssertSdlImport(nativeMethod, "SDL_SetGamepadPlayerIndex");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetGamepadPlayerIndexNativeFunction", nameof(CaptureSetGamepadPlayerIndex));
        bool result = SDL3.SDL.SetGamepadPlayerIndex((IntPtr)0x5304, 6);

        TestAssert.Equal(true, result, "SDL.SetGamepadPlayerIndex must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5304, capturedGamepad, "SDL.SetGamepadPlayerIndex must forward gamepad.");
        TestAssert.Equal(6, capturedPlayerIndex, "SDL.SetGamepadPlayerIndex must forward playerIndex.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGamepadPlayerIndex must call the native hook once.");
    }

    public static void GetGamepadVendor_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadUShortMethod("SDL.GetGamepadVendor", "SDL_GetGamepadVendor", "GetGamepadVendorNativeFunction", (IntPtr)0x5401, 0x045E);
    }

    public static void GetGamepadProduct_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadUShortMethod("SDL.GetGamepadProduct", "SDL_GetGamepadProduct", "GetGamepadProductNativeFunction", (IntPtr)0x5402, 0x02FF);
    }

    public static void GetGamepadProductVersion_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadUShortMethod("SDL.GetGamepadProductVersion", "SDL_GetGamepadProductVersion", "GetGamepadProductVersionNativeFunction", (IntPtr)0x5403, 0x0102);
    }

    public static void GetGamepadFirmwareVersion_ForwardsGamepadAndReturnsNativeValue()
    {
        AssertGamepadUShortMethod("SDL.GetGamepadFirmwareVersion", "SDL_GetGamepadFirmwareVersion", "GetGamepadFirmwareVersionNativeFunction", (IntPtr)0x5404, 0x0304);
    }

    public static void SDL_GetGamepadSerial_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadSerial");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadSerial");
    }

    public static void GetGamepadSerial_ReturnsStringAndNull()
    {
        AssertGamepadStringMethod(
            "SDL.GetGamepadSerial",
            "GetGamepadSerialNativeFunction",
            nameof(CaptureGamepadPointer),
            (IntPtr)0x5501,
            (IntPtr)0x5502,
            () => SDL3.SDL.GetGamepadSerial((IntPtr)0x5501),
            () => SDL3.SDL.GetGamepadSerial((IntPtr)0x5502));
    }

    public static void GetGamepadSteamHandle_ForwardsGamepadAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadSteamHandle");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadSteamHandle");

        ResetCaptureState();
        nextULong = 0x0102030405060708;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadSteamHandleNativeFunction", nameof(CaptureGamepadULong));
        ulong result = SDL3.SDL.GetGamepadSteamHandle((IntPtr)0x5601);

        TestAssert.Equal(0x0102030405060708UL, result, "SDL.GetGamepadSteamHandle must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5601, capturedGamepad, "SDL.GetGamepadSteamHandle must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadSteamHandle must call the native hook once.");
    }

    public static void GetGamepadConnectionState_ForwardsGamepadAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadConnectionState");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadConnectionState");

        ResetCaptureState();
        nextConnectionState = SDL3.SDL.JoystickConnectionState.Wireless;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadConnectionStateNativeFunction", nameof(CaptureGamepadConnectionState));
        SDL3.SDL.JoystickConnectionState result = SDL3.SDL.GetGamepadConnectionState((IntPtr)0x5602);

        TestAssert.Equal(SDL3.SDL.JoystickConnectionState.Wireless, result, "SDL.GetGamepadConnectionState must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5602, capturedGamepad, "SDL.GetGamepadConnectionState must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadConnectionState must call the native hook once.");
    }

    public static void GetGamepadPowerInfo_ForwardsGamepadAndOutputsPercent()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadPowerInfo");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadPowerInfo");
        AssertOutParameter(nativeMethod, "percent");

        ResetCaptureState();
        nextPowerState = SDL3.SDL.PowerState.Charging;
        nextPercent = 67;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadPowerInfoNativeFunction", nameof(CaptureGamepadPowerInfo));
        SDL3.SDL.PowerState result = SDL3.SDL.GetGamepadPowerInfo((IntPtr)0x5603, out int percent);

        TestAssert.Equal(SDL3.SDL.PowerState.Charging, result, "SDL.GetGamepadPowerInfo must return the native hook value.");
        TestAssert.Equal(67, percent, "SDL.GetGamepadPowerInfo must forward native percent.");
        TestAssert.Equal((IntPtr)0x5603, capturedGamepad, "SDL.GetGamepadPowerInfo must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadPowerInfo must call the native hook once.");
    }

    public static void GamepadConnected_ForwardsGamepadAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadConnected");
        AssertSdlImport(nativeMethod, "SDL_GamepadConnected");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadConnectedNativeFunction", nameof(CaptureGamepadBool));
        bool result = SDL3.SDL.GamepadConnected((IntPtr)0x5701);

        TestAssert.Equal(true, result, "SDL.GamepadConnected must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5701, capturedGamepad, "SDL.GamepadConnected must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadConnected must call the native hook once.");
    }

    public static void GetGamepadJoystick_ForwardsGamepadAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadJoystick");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadJoystick");

        ResetCaptureState();
        nextPointer = (IntPtr)0x5702;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadJoystickNativeFunction", nameof(CaptureGamepadPointer));
        IntPtr result = SDL3.SDL.GetGamepadJoystick((IntPtr)0x5703);

        TestAssert.Equal((IntPtr)0x5702, result, "SDL.GetGamepadJoystick must return the native hook pointer.");
        TestAssert.Equal((IntPtr)0x5703, capturedGamepad, "SDL.GetGamepadJoystick must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadJoystick must call the native hook once.");
    }

    public static void SetGamepadEventsEnabled_ForwardsEnabled()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGamepadEventsEnabled");
        AssertSdlImport(nativeMethod, "SDL_SetGamepadEventsEnabled");
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("SetGamepadEventsEnabledNativeFunction", nameof(CaptureSetGamepadEventsEnabled));
        SDL3.SDL.SetGamepadEventsEnabled(true);

        TestAssert.Equal(true, capturedEnabled, "SDL.SetGamepadEventsEnabled must forward enabled.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGamepadEventsEnabled must call the native hook once.");
    }

    public static void GamepadEventsEnabled_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadEventsEnabled");
        AssertSdlImport(nativeMethod, "SDL_GamepadEventsEnabled");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadEventsEnabledNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.GamepadEventsEnabled();

        TestAssert.Equal(true, result, "SDL.GamepadEventsEnabled must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadEventsEnabled must call the native hook once.");
    }

    public static void SDL_GetGamepadBindings_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadBindings");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadBindings");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetGamepadBindings_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        SDL3.SDL.GamepadBinding first = new()
        {
            InputType = SDL3.SDL.GamepadBindingType.Button,
            OutputType = SDL3.SDL.GamepadBindingType.Button
        };
        first.Input.Button = 7;
        first.Output.Button = SDL3.SDL.GamepadButton.South;

        SDL3.SDL.GamepadBinding second = new()
        {
            InputType = SDL3.SDL.GamepadBindingType.Axis,
            OutputType = SDL3.SDL.GamepadBindingType.Axis
        };
        second.Input.Axis.Axis = 2;
        second.Input.Axis.AxisMin = -32768;
        second.Input.Axis.AxisMax = 32767;
        second.Output.Axis.Axis = SDL3.SDL.GamepadAxis.RightX;
        second.Output.Axis.AxisMin = -100;
        second.Output.Axis.AxisMax = 100;

        IntPtr firstPtr = AllocateBinding(first);
        IntPtr secondPtr = AllocateBinding(second);
        IntPtr pointerArray = Marshal.AllocHGlobal(IntPtr.Size * 2);

        try
        {
            Marshal.WriteIntPtr(pointerArray, 0, firstPtr);
            Marshal.WriteIntPtr(pointerArray, IntPtr.Size, secondPtr);
            nextPointer = pointerArray;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetGamepadBindingsNativeFunction", nameof(CaptureGetGamepadBindings));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            SDL3.SDL.GamepadBinding[]? bindings = SDL3.SDL.GetGamepadBindings((IntPtr)0x5704, out int count);

            TestAssert.NotNull(bindings, "SDL.GetGamepadBindings must convert native bindings.");
            TestAssert.Equal(2, bindings!.Length, "SDL.GetGamepadBindings must preserve native binding count.");
            TestAssert.Equal(SDL3.SDL.GamepadBindingType.Button, bindings[0].InputType, "SDL.GetGamepadBindings must convert first input type.");
            TestAssert.Equal(7, bindings[0].Input.Button, "SDL.GetGamepadBindings must convert first button input.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.South, bindings[0].Output.Button, "SDL.GetGamepadBindings must convert first button output.");
            TestAssert.Equal(SDL3.SDL.GamepadBindingType.Axis, bindings[1].InputType, "SDL.GetGamepadBindings must convert second input type.");
            TestAssert.Equal(2, bindings[1].Input.Axis.Axis, "SDL.GetGamepadBindings must convert second axis input.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.RightX, bindings[1].Output.Axis.Axis, "SDL.GetGamepadBindings must convert second axis output.");
            TestAssert.Equal(2, count, "SDL.GetGamepadBindings must return native count.");
            TestAssert.Equal((IntPtr)0x5704, capturedGamepad, "SDL.GetGamepadBindings must forward gamepad.");
            TestAssert.Equal(pointerArray, capturedFreeMemory, "SDL.GetGamepadBindings must free the native pointer array.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            bindings = SDL3.SDL.GetGamepadBindings((IntPtr)0x5705, out count);

            TestAssert.Equal<SDL3.SDL.GamepadBinding[]?>(null, bindings, "SDL.GetGamepadBindings must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetGamepadBindings must return native count for native null.");
            TestAssert.Equal((IntPtr)0x5705, capturedGamepad, "SDL.GetGamepadBindings must forward gamepad for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadBindings must call the native hook for both branches.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetGamepadBindings must only free non-null native arrays.");
        }
        finally
        {
            Marshal.FreeHGlobal(firstPtr);
            Marshal.FreeHGlobal(secondPtr);
            Marshal.FreeHGlobal(pointerArray);
        }
    }

    public static void UpdateGamepads_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UpdateGamepads");
        AssertSdlImport(nativeMethod, "SDL_UpdateGamepads");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("UpdateGamepadsNativeFunction", nameof(CaptureNoArgumentVoid));
        SDL3.SDL.UpdateGamepads();

        TestAssert.Equal(1, capturedCallCount, "SDL.UpdateGamepads must call the native hook once.");
    }

    public static void GetGamepadTypeFromString_ForwardsStringAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadTypeFromString");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadTypeFromString");
        AssertStringParameterMarshal(nativeMethod, "str");

        ResetCaptureState();
        nextGamepadType = SDL3.SDL.GamepadType.Xbox360;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadTypeFromStringNativeFunction", nameof(CaptureGamepadTypeFromString));
        SDL3.SDL.GamepadType result = SDL3.SDL.GetGamepadTypeFromString("xbox360");

        TestAssert.Equal(SDL3.SDL.GamepadType.Xbox360, result, "SDL.GetGamepadTypeFromString must return the native hook value.");
        TestAssert.Equal("xbox360", capturedString, "SDL.GetGamepadTypeFromString must forward string.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadTypeFromString must call the native hook once.");
    }

    public static void SDL_GetGamepadStringForType_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadStringForType");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadStringForType");
    }

    public static void GetGamepadStringForType_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("ps5");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadStringForTypeNativeFunction", nameof(CaptureStringForGamepadType));
            string? result = SDL3.SDL.GetGamepadStringForType(SDL3.SDL.GamepadType.PS5);

            TestAssert.Equal("ps5", result, "SDL.GetGamepadStringForType must convert native UTF-8 strings.");
            TestAssert.Equal(SDL3.SDL.GamepadType.PS5, capturedGamepadType, "SDL.GetGamepadStringForType must forward type.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadStringForType(SDL3.SDL.GamepadType.Unknown);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadStringForType must return null for native null.");
            TestAssert.Equal(SDL3.SDL.GamepadType.Unknown, capturedGamepadType, "SDL.GetGamepadStringForType must forward type for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadStringForType must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GetGamepadAxisFromString_ForwardsStringAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadAxisFromString");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadAxisFromString");
        AssertStringParameterMarshal(nativeMethod, "str");

        ResetCaptureState();
        nextGamepadAxis = SDL3.SDL.GamepadAxis.LeftTrigger;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadAxisFromStringNativeFunction", nameof(CaptureGamepadAxisFromString));
        SDL3.SDL.GamepadAxis result = SDL3.SDL.GetGamepadAxisFromString("lefttrigger");

        TestAssert.Equal(SDL3.SDL.GamepadAxis.LeftTrigger, result, "SDL.GetGamepadAxisFromString must return the native hook value.");
        TestAssert.Equal("lefttrigger", capturedString, "SDL.GetGamepadAxisFromString must forward string.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadAxisFromString must call the native hook once.");
    }

    public static void SDL_GetGamepadStringForAxis_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadStringForAxis");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadStringForAxis");
    }

    public static void GetGamepadStringForAxis_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("rightx");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadStringForAxisNativeFunction", nameof(CaptureStringForAxis));
            string? result = SDL3.SDL.GetGamepadStringForAxis(SDL3.SDL.GamepadAxis.RightX);

            TestAssert.Equal("rightx", result, "SDL.GetGamepadStringForAxis must convert native UTF-8 strings.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.RightX, capturedAxis, "SDL.GetGamepadStringForAxis must forward axis.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadStringForAxis(SDL3.SDL.GamepadAxis.Invalid);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadStringForAxis must return null for native null.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.Invalid, capturedAxis, "SDL.GetGamepadStringForAxis must forward axis for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadStringForAxis must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GamepadHasAxis_ForwardsGamepadAxisAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadHasAxis");
        AssertSdlImport(nativeMethod, "SDL_GamepadHasAxis");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadHasAxisNativeFunction", nameof(CaptureGamepadAxisBool));
        bool result = SDL3.SDL.GamepadHasAxis((IntPtr)0x5801, SDL3.SDL.GamepadAxis.LeftX);

        TestAssert.Equal(true, result, "SDL.GamepadHasAxis must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5801, capturedGamepad, "SDL.GamepadHasAxis must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadAxis.LeftX, capturedAxis, "SDL.GamepadHasAxis must forward axis.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadHasAxis must call the native hook once.");
    }

    public static void GetGamepadAxis_ForwardsGamepadAxisAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadAxis");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadAxis");

        ResetCaptureState();
        nextShort = -1234;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadAxisNativeFunction", nameof(CaptureGamepadAxisShort));
        short result = SDL3.SDL.GetGamepadAxis((IntPtr)0x5802, SDL3.SDL.GamepadAxis.RightY);

        TestAssert.Equal((short)-1234, result, "SDL.GetGamepadAxis must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5802, capturedGamepad, "SDL.GetGamepadAxis must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadAxis.RightY, capturedAxis, "SDL.GetGamepadAxis must forward axis.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadAxis must call the native hook once.");
    }

    public static void GetGamepadButtonFromString_ForwardsStringAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadButtonFromString");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadButtonFromString");
        AssertStringParameterMarshal(nativeMethod, "str");

        ResetCaptureState();
        nextGamepadButton = SDL3.SDL.GamepadButton.South;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadButtonFromStringNativeFunction", nameof(CaptureGamepadButtonFromString));
        SDL3.SDL.GamepadButton result = SDL3.SDL.GetGamepadButtonFromString("south");

        TestAssert.Equal(SDL3.SDL.GamepadButton.South, result, "SDL.GetGamepadButtonFromString must return the native hook value.");
        TestAssert.Equal("south", capturedString, "SDL.GetGamepadButtonFromString must forward string.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadButtonFromString must call the native hook once.");
    }

    public static void SDL_GetGamepadStringForButton_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadStringForButton");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadStringForButton");
    }

    public static void GetGamepadStringForButton_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("east");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadStringForButtonNativeFunction", nameof(CaptureStringForButton));
            string? result = SDL3.SDL.GetGamepadStringForButton(SDL3.SDL.GamepadButton.East);

            TestAssert.Equal("east", result, "SDL.GetGamepadStringForButton must convert native UTF-8 strings.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.East, capturedButton, "SDL.GetGamepadStringForButton must forward button.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadStringForButton(SDL3.SDL.GamepadButton.Invalid);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadStringForButton must return null for native null.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.Invalid, capturedButton, "SDL.GetGamepadStringForButton must forward button for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadStringForButton must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GamepadHasButton_ForwardsGamepadButtonAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadHasButton");
        AssertSdlImport(nativeMethod, "SDL_GamepadHasButton");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadHasButtonNativeFunction", nameof(CaptureGamepadButtonBool));
        bool result = SDL3.SDL.GamepadHasButton((IntPtr)0x5901, SDL3.SDL.GamepadButton.West);

        TestAssert.Equal(true, result, "SDL.GamepadHasButton must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5901, capturedGamepad, "SDL.GamepadHasButton must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadButton.West, capturedButton, "SDL.GamepadHasButton must forward button.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadHasButton must call the native hook once.");
    }

    public static void GetGamepadButton_ForwardsGamepadButtonAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadButton");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadButton");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadButtonNativeFunction", nameof(CaptureGamepadButtonBool));
        bool result = SDL3.SDL.GetGamepadButton((IntPtr)0x5902, SDL3.SDL.GamepadButton.North);

        TestAssert.Equal(true, result, "SDL.GetGamepadButton must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5902, capturedGamepad, "SDL.GetGamepadButton must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadButton.North, capturedButton, "SDL.GetGamepadButton must forward button.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadButton must call the native hook once.");
    }

    public static void GetGamepadButtonLabelForType_ForwardsTypeButtonAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadButtonLabelForType");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadButtonLabelForType");

        ResetCaptureState();
        nextGamepadButtonLabel = SDL3.SDL.GamepadButtonLabel.Cross;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadButtonLabelForTypeNativeFunction", nameof(CaptureButtonLabelForType));
        SDL3.SDL.GamepadButtonLabel result = SDL3.SDL.GetGamepadButtonLabelForType(SDL3.SDL.GamepadType.PS5, SDL3.SDL.GamepadButton.South);

        TestAssert.Equal(SDL3.SDL.GamepadButtonLabel.Cross, result, "SDL.GetGamepadButtonLabelForType must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.GamepadType.PS5, capturedGamepadType, "SDL.GetGamepadButtonLabelForType must forward type.");
        TestAssert.Equal(SDL3.SDL.GamepadButton.South, capturedButton, "SDL.GetGamepadButtonLabelForType must forward button.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadButtonLabelForType must call the native hook once.");
    }

    public static void GetGamepadButtonLabel_ForwardsGamepadButtonAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadButtonLabel");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadButtonLabel");

        ResetCaptureState();
        nextGamepadButtonLabel = SDL3.SDL.GamepadButtonLabel.A;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadButtonLabelNativeFunction", nameof(CaptureGamepadButtonLabel));
        SDL3.SDL.GamepadButtonLabel result = SDL3.SDL.GetGamepadButtonLabel((IntPtr)0x5903, SDL3.SDL.GamepadButton.South);

        TestAssert.Equal(SDL3.SDL.GamepadButtonLabel.A, result, "SDL.GetGamepadButtonLabel must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5903, capturedGamepad, "SDL.GetGamepadButtonLabel must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadButton.South, capturedButton, "SDL.GetGamepadButtonLabel must forward button.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadButtonLabel must call the native hook once.");
    }

    public static void GetNumGamepadTouchpads_ForwardsGamepadAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetNumGamepadTouchpads");
        AssertSdlImport(nativeMethod, "SDL_GetNumGamepadTouchpads");

        ResetCaptureState();
        nextInt = 2;

        using NativeHookScope _ = NativeHookScope.Install("GetNumGamepadTouchpadsNativeFunction", nameof(CaptureGamepadInt));
        int result = SDL3.SDL.GetNumGamepadTouchpads((IntPtr)0x5A01);

        TestAssert.Equal(2, result, "SDL.GetNumGamepadTouchpads must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5A01, capturedGamepad, "SDL.GetNumGamepadTouchpads must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetNumGamepadTouchpads must call the native hook once.");
    }

    public static void GetNumGamepadTouchpadFingers_ForwardsGamepadTouchpadAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetNumGamepadTouchpadFingers");
        AssertSdlImport(nativeMethod, "SDL_GetNumGamepadTouchpadFingers");

        ResetCaptureState();
        nextInt = 5;

        using NativeHookScope _ = NativeHookScope.Install("GetNumGamepadTouchpadFingersNativeFunction", nameof(CaptureGamepadTouchpadInt));
        int result = SDL3.SDL.GetNumGamepadTouchpadFingers((IntPtr)0x5A02, 3);

        TestAssert.Equal(5, result, "SDL.GetNumGamepadTouchpadFingers must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5A02, capturedGamepad, "SDL.GetNumGamepadTouchpadFingers must forward gamepad.");
        TestAssert.Equal(3, capturedTouchpad, "SDL.GetNumGamepadTouchpadFingers must forward touchpad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetNumGamepadTouchpadFingers must call the native hook once.");
    }

    public static void GetGamepadTouchpadFinger_ForwardsArgumentsOutputsValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadTouchpadFinger");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadTouchpadFinger");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "down");
        AssertOutParameter(nativeMethod, "down");
        AssertOutParameter(nativeMethod, "x");
        AssertOutParameter(nativeMethod, "y");
        AssertOutParameter(nativeMethod, "pressure");

        ResetCaptureState();
        nextBool = true;
        nextDown = true;
        nextX = 0.25f;
        nextY = 0.5f;
        nextPressure = 0.75f;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadTouchpadFingerNativeFunction", nameof(CaptureGamepadTouchpadFinger));
        bool result = SDL3.SDL.GetGamepadTouchpadFinger((IntPtr)0x5A03, 4, 2, out bool down, out float x, out float y, out float pressure);

        TestAssert.Equal(true, result, "SDL.GetGamepadTouchpadFinger must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5A03, capturedGamepad, "SDL.GetGamepadTouchpadFinger must forward gamepad.");
        TestAssert.Equal(4, capturedTouchpad, "SDL.GetGamepadTouchpadFinger must forward touchpad.");
        TestAssert.Equal(2, capturedFinger, "SDL.GetGamepadTouchpadFinger must forward finger.");
        TestAssert.Equal(true, down, "SDL.GetGamepadTouchpadFinger must output down.");
        TestAssert.Equal(0.25f, x, "SDL.GetGamepadTouchpadFinger must output x.");
        TestAssert.Equal(0.5f, y, "SDL.GetGamepadTouchpadFinger must output y.");
        TestAssert.Equal(0.75f, pressure, "SDL.GetGamepadTouchpadFinger must output pressure.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadTouchpadFinger must call the native hook once.");
    }

    public static void GamepadHasSensor_ForwardsGamepadSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadHasSensor");
        AssertSdlImport(nativeMethod, "SDL_GamepadHasSensor");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadHasSensorNativeFunction", nameof(CaptureGamepadSensorBool));
        bool result = SDL3.SDL.GamepadHasSensor((IntPtr)0x5B01, SDL3.SDL.SensorType.Accel);

        TestAssert.Equal(true, result, "SDL.GamepadHasSensor must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5B01, capturedGamepad, "SDL.GamepadHasSensor must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.SensorType.Accel, capturedSensorType, "SDL.GamepadHasSensor must forward sensor type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadHasSensor must call the native hook once.");
    }

    public static void SetGamepadSensorEnabled_ForwardsGamepadSensorEnabledAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGamepadSensorEnabled");
        AssertSdlImport(nativeMethod, "SDL_SetGamepadSensorEnabled");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetGamepadSensorEnabledNativeFunction", nameof(CaptureSetGamepadSensorEnabled));
        bool result = SDL3.SDL.SetGamepadSensorEnabled((IntPtr)0x5B02, SDL3.SDL.SensorType.Gyro, enabled: true);

        TestAssert.Equal(true, result, "SDL.SetGamepadSensorEnabled must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5B02, capturedGamepad, "SDL.SetGamepadSensorEnabled must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.SensorType.Gyro, capturedSensorType, "SDL.SetGamepadSensorEnabled must forward sensor type.");
        TestAssert.Equal(true, capturedEnabled, "SDL.SetGamepadSensorEnabled must forward enabled.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGamepadSensorEnabled must call the native hook once.");
    }

    public static void GamepadSensorEnabled_ForwardsGamepadSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadSensorEnabled");
        AssertSdlImport(nativeMethod, "SDL_GamepadSensorEnabled");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadSensorEnabledNativeFunction", nameof(CaptureGamepadSensorBool));
        bool result = SDL3.SDL.GamepadSensorEnabled((IntPtr)0x5B03, SDL3.SDL.SensorType.GyroR);

        TestAssert.Equal(true, result, "SDL.GamepadSensorEnabled must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5B03, capturedGamepad, "SDL.GamepadSensorEnabled must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.SensorType.GyroR, capturedSensorType, "SDL.GamepadSensorEnabled must forward sensor type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadSensorEnabled must call the native hook once.");
    }

    public static void GetGamepadSensorDataRate_ForwardsGamepadSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadSensorDataRate");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadSensorDataRate");

        ResetCaptureState();
        nextFloat = 120.5f;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadSensorDataRateNativeFunction", nameof(CaptureGamepadSensorFloat));
        float result = SDL3.SDL.GetGamepadSensorDataRate((IntPtr)0x5B04, SDL3.SDL.SensorType.AccelL);

        TestAssert.Equal(120.5f, result, "SDL.GetGamepadSensorDataRate must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5B04, capturedGamepad, "SDL.GetGamepadSensorDataRate must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.SensorType.AccelL, capturedSensorType, "SDL.GetGamepadSensorDataRate must forward sensor type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadSensorDataRate must call the native hook once.");
    }

    public static void GetGamepadSensorData_ForwardsGamepadSensorDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadSensorData");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadSensorData");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 3);

        ResetCaptureState();
        nextBool = true;
        nextFloatArray = [1.25f, 2.5f, 3.75f];

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadSensorDataNativeFunction", nameof(CaptureGamepadSensorData));
        bool result = SDL3.SDL.GetGamepadSensorData((IntPtr)0x5B05, SDL3.SDL.SensorType.GyroL, out float[] data, 3);

        TestAssert.Equal(true, result, "SDL.GetGamepadSensorData must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5B05, capturedGamepad, "SDL.GetGamepadSensorData must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.SensorType.GyroL, capturedSensorType, "SDL.GetGamepadSensorData must forward sensor type.");
        TestAssert.Equal(3, capturedCount, "SDL.GetGamepadSensorData must forward numValues.");
        TestAssert.Equal(3, data.Length, "SDL.GetGamepadSensorData must output all sensor values.");
        TestAssert.Equal(1.25f, data[0], "SDL.GetGamepadSensorData must output value 0.");
        TestAssert.Equal(2.5f, data[1], "SDL.GetGamepadSensorData must output value 1.");
        TestAssert.Equal(3.75f, data[2], "SDL.GetGamepadSensorData must output value 2.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadSensorData must call the native hook once.");
    }

    public static void GamepadHasCapSense_ForwardsGamepadCapSenseAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GamepadHasCapSense");
        AssertSdlImport(nativeMethod, "SDL_GamepadHasCapSense");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadHasCapSenseNativeFunction", nameof(CaptureGamepadCapSenseBool));
        bool result = SDL3.SDL.GamepadHasCapSense((IntPtr)0x5C01, SDL3.SDL.GamepadCapSenseType.LeftGrip);

        TestAssert.Equal(true, result, "SDL.GamepadHasCapSense must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5C01, capturedGamepad, "SDL.GamepadHasCapSense must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadCapSenseType.LeftGrip, capturedCapSenseType, "SDL.GamepadHasCapSense must forward capsense type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GamepadHasCapSense must call the native hook once.");
    }

    public static void GetGamepadCapSense_ForwardsGamepadCapSenseAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadCapSense");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadCapSense");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadCapSenseNativeFunction", nameof(CaptureGamepadCapSenseBool));
        bool result = SDL3.SDL.GetGamepadCapSense((IntPtr)0x5C02, SDL3.SDL.GamepadCapSenseType.RightStick);

        TestAssert.Equal(true, result, "SDL.GetGamepadCapSense must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5C02, capturedGamepad, "SDL.GetGamepadCapSense must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadCapSenseType.RightStick, capturedCapSenseType, "SDL.GetGamepadCapSense must forward capsense type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGamepadCapSense must call the native hook once.");
    }

    public static void RumbleGamepad_ForwardsRumbleValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RumbleGamepad");
        AssertSdlImport(nativeMethod, "SDL_RumbleGamepad");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("RumbleGamepadNativeFunction", nameof(CaptureRumbleGamepad));
        bool result = SDL3.SDL.RumbleGamepad((IntPtr)0x5D01, 100, 200, 300u);

        TestAssert.Equal(true, result, "SDL.RumbleGamepad must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5D01, capturedGamepad, "SDL.RumbleGamepad must forward gamepad.");
        TestAssert.Equal((ushort)100, capturedLowFrequencyRumble, "SDL.RumbleGamepad must forward low frequency rumble.");
        TestAssert.Equal((ushort)200, capturedHighFrequencyRumble, "SDL.RumbleGamepad must forward high frequency rumble.");
        TestAssert.Equal(300u, capturedDurationMs, "SDL.RumbleGamepad must forward durationMs.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RumbleGamepad must call the native hook once.");
    }

    public static void RumbleGamepadTriggers_ForwardsTriggerRumbleValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RumbleGamepadTriggers");
        AssertSdlImport(nativeMethod, "SDL_RumbleGamepadTriggers");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("RumbleGamepadTriggersNativeFunction", nameof(CaptureRumbleGamepadTriggers));
        bool result = SDL3.SDL.RumbleGamepadTriggers((IntPtr)0x5D02, 111, 222, 333u);

        TestAssert.Equal(true, result, "SDL.RumbleGamepadTriggers must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5D02, capturedGamepad, "SDL.RumbleGamepadTriggers must forward gamepad.");
        TestAssert.Equal((ushort)111, capturedLeftRumble, "SDL.RumbleGamepadTriggers must forward left rumble.");
        TestAssert.Equal((ushort)222, capturedRightRumble, "SDL.RumbleGamepadTriggers must forward right rumble.");
        TestAssert.Equal(333u, capturedDurationMs, "SDL.RumbleGamepadTriggers must forward durationMs.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RumbleGamepadTriggers must call the native hook once.");
    }

    public static void SetGamepadLED_ForwardsColorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGamepadLED");
        AssertSdlImport(nativeMethod, "SDL_SetGamepadLED");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetGamepadLEDNativeFunction", nameof(CaptureSetGamepadLED));
        bool result = SDL3.SDL.SetGamepadLED((IntPtr)0x5D03, 10, 20, 30);

        TestAssert.Equal(true, result, "SDL.SetGamepadLED must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5D03, capturedGamepad, "SDL.SetGamepadLED must forward gamepad.");
        TestAssert.Equal((byte)10, capturedRed, "SDL.SetGamepadLED must forward red.");
        TestAssert.Equal((byte)20, capturedGreen, "SDL.SetGamepadLED must forward green.");
        TestAssert.Equal((byte)30, capturedBlue, "SDL.SetGamepadLED must forward blue.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGamepadLED must call the native hook once.");
    }

    public static void SendGamepadEffect_WithPointer_ForwardsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendGamepadEffect", typeof(IntPtr), typeof(IntPtr), typeof(int));
        AssertSdlImport(nativeMethod, "SDL_SendGamepadEffect");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SendGamepadEffectPointerNativeFunction", nameof(CaptureSendGamepadEffectPointer));
        bool result = SDL3.SDL.SendGamepadEffect((IntPtr)0x5D04, (IntPtr)0x5D05, 7);

        TestAssert.Equal(true, result, "SDL.SendGamepadEffect(IntPtr) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5D04, capturedGamepad, "SDL.SendGamepadEffect(IntPtr) must forward gamepad.");
        TestAssert.Equal((IntPtr)0x5D05, capturedEffectData, "SDL.SendGamepadEffect(IntPtr) must forward data.");
        TestAssert.Equal(7, capturedSize, "SDL.SendGamepadEffect(IntPtr) must forward size.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SendGamepadEffect(IntPtr) must call the native hook once.");
    }

    public static void SendGamepadEffect_WithArray_ForwardsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendGamepadEffect", typeof(IntPtr), typeof(byte[]), typeof(int));
        AssertSdlImport(nativeMethod, "SDL_SendGamepadEffect");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);

        ResetCaptureState();
        nextBool = true;
        byte[] effect = [1, 2, 3, 4];

        using NativeHookScope _ = NativeHookScope.Install("SendGamepadEffectArrayNativeFunction", nameof(CaptureSendGamepadEffectArray));
        bool result = SDL3.SDL.SendGamepadEffect((IntPtr)0x5D06, effect, effect.Length);

        TestAssert.Equal(true, result, "SDL.SendGamepadEffect(byte[]) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5D06, capturedGamepad, "SDL.SendGamepadEffect(byte[]) must forward gamepad.");
        TestAssert.Equal(effect, capturedEffectBytes, "SDL.SendGamepadEffect(byte[]) must forward the same data array.");
        TestAssert.Equal(4, capturedSize, "SDL.SendGamepadEffect(byte[]) must forward size.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SendGamepadEffect(byte[]) must call the native hook once.");
    }

    public static void CloseGamepad_ForwardsGamepad()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CloseGamepad");
        AssertSdlImport(nativeMethod, "SDL_CloseGamepad");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("CloseGamepadNativeFunction", nameof(CaptureCloseGamepad));
        SDL3.SDL.CloseGamepad((IntPtr)0x5D07);

        TestAssert.Equal((IntPtr)0x5D07, capturedGamepad, "SDL.CloseGamepad must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CloseGamepad must call the native hook once.");
    }

    public static void SDL_GetGamepadAppleSFSymbolsNameForButton_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadAppleSFSymbolsNameForButton");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadAppleSFSymbolsNameForButton");
    }

    public static void GetGamepadAppleSFSymbolsNameForButton_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("button.symbol");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadAppleSFSymbolsNameForButtonNativeFunction", nameof(CaptureStringForGamepadButton));
            string? result = SDL3.SDL.GetGamepadAppleSFSymbolsNameForButton((IntPtr)0x5E01, SDL3.SDL.GamepadButton.South);

            TestAssert.Equal("button.symbol", result, "SDL.GetGamepadAppleSFSymbolsNameForButton must convert native UTF-8 strings.");
            TestAssert.Equal((IntPtr)0x5E01, capturedGamepad, "SDL.GetGamepadAppleSFSymbolsNameForButton must forward gamepad.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.South, capturedButton, "SDL.GetGamepadAppleSFSymbolsNameForButton must forward button.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadAppleSFSymbolsNameForButton((IntPtr)0x5E02, SDL3.SDL.GamepadButton.Invalid);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadAppleSFSymbolsNameForButton must return null for native null.");
            TestAssert.Equal((IntPtr)0x5E02, capturedGamepad, "SDL.GetGamepadAppleSFSymbolsNameForButton must forward gamepad for native null.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.Invalid, capturedButton, "SDL.GetGamepadAppleSFSymbolsNameForButton must forward button for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadAppleSFSymbolsNameForButton must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void SDL_GetGamepadAppleSFSymbolsNameForAxis_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGamepadAppleSFSymbolsNameForAxis");
        AssertSdlImport(nativeMethod, "SDL_GetGamepadAppleSFSymbolsNameForAxis");
    }

    public static void GetGamepadAppleSFSymbolsNameForAxis_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("axis.symbol");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadAppleSFSymbolsNameForAxisNativeFunction", nameof(CaptureStringForGamepadAxis));
            string? result = SDL3.SDL.GetGamepadAppleSFSymbolsNameForAxis((IntPtr)0x5E03, SDL3.SDL.GamepadAxis.LeftTrigger);

            TestAssert.Equal("axis.symbol", result, "SDL.GetGamepadAppleSFSymbolsNameForAxis must convert native UTF-8 strings.");
            TestAssert.Equal((IntPtr)0x5E03, capturedGamepad, "SDL.GetGamepadAppleSFSymbolsNameForAxis must forward gamepad.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.LeftTrigger, capturedAxis, "SDL.GetGamepadAppleSFSymbolsNameForAxis must forward axis.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGamepadAppleSFSymbolsNameForAxis((IntPtr)0x5E04, SDL3.SDL.GamepadAxis.Invalid);

            TestAssert.Equal<string?>(null, result, "SDL.GetGamepadAppleSFSymbolsNameForAxis must return null for native null.");
            TestAssert.Equal((IntPtr)0x5E04, capturedGamepad, "SDL.GetGamepadAppleSFSymbolsNameForAxis must forward gamepad for native null.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.Invalid, capturedAxis, "SDL.GetGamepadAppleSFSymbolsNameForAxis must forward axis for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGamepadAppleSFSymbolsNameForAxis must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static void AssertInstanceStringMethod(string apiName, string hookFieldName, string hookMethodName, Func<string?> firstCall, Func<string?> secondCall)
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8(apiName);

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);
            string? result = firstCall();

            TestAssert.Equal(apiName, result, $"{apiName} must convert native UTF-8 strings.");
            TestAssert.Equal(111u, capturedInstanceId, $"{apiName} must forward instanceId.");

            nextPointer = IntPtr.Zero;
            result = secondCall();

            TestAssert.Equal<string?>(null, result, $"{apiName} must return null for native null.");
            TestAssert.Equal(112u, capturedInstanceId, $"{apiName} must forward instanceId for native null.");
            TestAssert.Equal(2, capturedCallCount, $"{apiName} must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static void AssertInstanceIntMethod(string apiName, string nativeName, string hookFieldName, uint instanceID, int nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextInt = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstanceInt));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceID);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceID, capturedInstanceId, $"{apiName} must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertInstanceUShortMethod(string apiName, string nativeName, string hookFieldName, uint instanceID, ushort nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextUShort = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstanceUShort));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceID);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceID, capturedInstanceId, $"{apiName} must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertInstanceGamepadTypeMethod(string apiName, string nativeName, string hookFieldName, uint instanceID, SDL3.SDL.GamepadType nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextGamepadType = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstanceGamepadType));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceID);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceID, capturedInstanceId, $"{apiName} must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertInstancePointerMethod(string apiName, string nativeName, string hookFieldName, uint instanceID, IntPtr nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextPointer = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstancePointer));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceID);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook pointer.");
        TestAssert.Equal(instanceID, capturedInstanceId, $"{apiName} must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertGamepadUIntMethod(string apiName, string nativeName, string hookFieldName, IntPtr gamepad, uint nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextUInt = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureGamepadUInt));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], gamepad);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(gamepad, capturedGamepad, $"{apiName} must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertGamepadIntMethod(string apiName, string nativeName, string hookFieldName, IntPtr gamepad, int nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextInt = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureGamepadInt));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], gamepad);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(gamepad, capturedGamepad, $"{apiName} must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertGamepadUShortMethod(string apiName, string nativeName, string hookFieldName, IntPtr gamepad, ushort nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextUShort = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureGamepadUShort));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], gamepad);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(gamepad, capturedGamepad, $"{apiName} must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertGamepadTypeMethod(string apiName, string nativeName, string hookFieldName, IntPtr gamepad, SDL3.SDL.GamepadType nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextGamepadType = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureGamepadType));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], gamepad);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(gamepad, capturedGamepad, $"{apiName} must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertGamepadStringMethod(string apiName, string hookFieldName, string hookMethodName, IntPtr firstGamepad, IntPtr secondGamepad, Func<string?> firstCall, Func<string?> secondCall)
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8(apiName);

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);
            string? result = firstCall();

            TestAssert.Equal(apiName, result, $"{apiName} must convert native UTF-8 strings.");
            TestAssert.Equal(firstGamepad, capturedGamepad, $"{apiName} must forward gamepad.");

            nextPointer = IntPtr.Zero;
            result = secondCall();

            TestAssert.Equal<string?>(null, result, $"{apiName} must return null for native null.");
            TestAssert.Equal(secondGamepad, capturedGamepad, $"{apiName} must forward gamepad for native null.");
            TestAssert.Equal(2, capturedCallCount, $"{apiName} must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static int CaptureAddGamepadMapping(string mapping)
    {
        capturedCallCount++;
        capturedMapping = mapping;
        return nextInt;
    }

    private static int CaptureAddGamepadMappingsFromIO(IntPtr src, bool closeio)
    {
        capturedCallCount++;
        capturedSource = src;
        capturedCloseIO = closeio;
        return nextInt;
    }

    private static int CaptureAddGamepadMappingsFromFile(string file)
    {
        capturedCallCount++;
        capturedFile = file;
        return nextInt;
    }

    private static bool CaptureNoArgumentBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureNoArgumentVoid()
    {
        capturedCallCount++;
    }

    private static IntPtr CaptureArrayPointer(out int count)
    {
        capturedCallCount++;
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureGetGamepadMappingForGUID(SDL3.SDL.GUID guid)
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGetGamepadMapping(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextPointer;
    }

    private static bool CaptureSetGamepadMapping(uint instanceID, string? mapping)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        capturedMapping = mapping;
        return nextBool;
    }

    private static bool CaptureInstanceBool(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextBool;
    }

    private static int CaptureInstanceInt(uint instanceID)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        return nextInt;
    }

    private static SDL3.SDL.GUID CaptureInstanceGuid(uint instanceID)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        return nextGuid;
    }

    private static ushort CaptureInstanceUShort(uint instanceID)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        return nextUShort;
    }

    private static SDL3.SDL.GamepadType CaptureInstanceGamepadType(uint instanceID)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        return nextGamepadType;
    }

    private static IntPtr CaptureInstancePointer(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextPointer;
    }

    private static IntPtr CapturePlayerIndexPointer(int playerIndex)
    {
        capturedCallCount++;
        capturedPlayerIndex = playerIndex;
        return nextPointer;
    }

    private static uint CaptureGamepadUInt(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextUInt;
    }

    private static int CaptureGamepadInt(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextInt;
    }

    private static ushort CaptureGamepadUShort(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextUShort;
    }

    private static SDL3.SDL.GamepadType CaptureGamepadType(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextGamepadType;
    }

    private static IntPtr CaptureGamepadPointer(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextPointer;
    }

    private static bool CaptureSetGamepadPlayerIndex(IntPtr gamepad, int playerIndex)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedPlayerIndex = playerIndex;
        return nextBool;
    }

    private static bool CaptureGamepadBool(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextBool;
    }

    private static void CaptureSetGamepadEventsEnabled(bool enabled)
    {
        capturedCallCount++;
        capturedEnabled = enabled;
    }

    private static IntPtr CaptureGetGamepadBindings(IntPtr gamepad, out int count)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        count = nextCount;
        return nextPointer;
    }

    private static SDL3.SDL.GamepadType CaptureGamepadTypeFromString(string str)
    {
        capturedCallCount++;
        capturedString = str;
        return nextGamepadType;
    }

    private static IntPtr CaptureStringForGamepadType(SDL3.SDL.GamepadType type)
    {
        capturedCallCount++;
        capturedGamepadType = type;
        return nextPointer;
    }

    private static SDL3.SDL.GamepadAxis CaptureGamepadAxisFromString(string str)
    {
        capturedCallCount++;
        capturedString = str;
        return nextGamepadAxis;
    }

    private static IntPtr CaptureStringForAxis(SDL3.SDL.GamepadAxis axis)
    {
        capturedCallCount++;
        capturedAxis = axis;
        return nextPointer;
    }

    private static bool CaptureGamepadAxisBool(IntPtr gamepad, SDL3.SDL.GamepadAxis axis)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedAxis = axis;
        return nextBool;
    }

    private static short CaptureGamepadAxisShort(IntPtr gamepad, SDL3.SDL.GamepadAxis axis)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedAxis = axis;
        return nextShort;
    }

    private static SDL3.SDL.GamepadButton CaptureGamepadButtonFromString(string str)
    {
        capturedCallCount++;
        capturedString = str;
        return nextGamepadButton;
    }

    private static IntPtr CaptureStringForButton(SDL3.SDL.GamepadButton button)
    {
        capturedCallCount++;
        capturedButton = button;
        return nextPointer;
    }

    private static bool CaptureGamepadButtonBool(IntPtr gamepad, SDL3.SDL.GamepadButton button)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedButton = button;
        return nextBool;
    }

    private static SDL3.SDL.GamepadButtonLabel CaptureButtonLabelForType(SDL3.SDL.GamepadType type, SDL3.SDL.GamepadButton button)
    {
        capturedCallCount++;
        capturedGamepadType = type;
        capturedButton = button;
        return nextGamepadButtonLabel;
    }

    private static SDL3.SDL.GamepadButtonLabel CaptureGamepadButtonLabel(IntPtr gamepad, SDL3.SDL.GamepadButton button)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedButton = button;
        return nextGamepadButtonLabel;
    }

    private static int CaptureGamepadTouchpadInt(IntPtr gamepad, int touchpad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedTouchpad = touchpad;
        return nextInt;
    }

    private static bool CaptureGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, out bool down, out float x, out float y, out float pressure)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedTouchpad = touchpad;
        capturedFinger = finger;
        down = nextDown;
        x = nextX;
        y = nextY;
        pressure = nextPressure;
        return nextBool;
    }

    private static bool CaptureGamepadSensorBool(IntPtr gamepad, SDL3.SDL.SensorType type)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedSensorType = type;
        return nextBool;
    }

    private static bool CaptureSetGamepadSensorEnabled(IntPtr gamepad, SDL3.SDL.SensorType type, bool enabled)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedSensorType = type;
        capturedEnabled = enabled;
        return nextBool;
    }

    private static float CaptureGamepadSensorFloat(IntPtr gamepad, SDL3.SDL.SensorType type)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedSensorType = type;
        return nextFloat;
    }

    private static bool CaptureGamepadSensorData(IntPtr gamepad, SDL3.SDL.SensorType type, out float[] data, int numValues)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedSensorType = type;
        capturedCount = numValues;
        data = nextFloatArray ?? [];
        return nextBool;
    }

    private static bool CaptureGamepadCapSenseBool(IntPtr gamepad, SDL3.SDL.GamepadCapSenseType type)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedCapSenseType = type;
        return nextBool;
    }

    private static bool CaptureRumbleGamepad(IntPtr gamepad, ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedLowFrequencyRumble = lowFrequencyRumble;
        capturedHighFrequencyRumble = highFrequencyRumble;
        capturedDurationMs = durationMs;
        return nextBool;
    }

    private static bool CaptureRumbleGamepadTriggers(IntPtr gamepad, ushort leftRumble, ushort rightRumble, uint durationMs)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedLeftRumble = leftRumble;
        capturedRightRumble = rightRumble;
        capturedDurationMs = durationMs;
        return nextBool;
    }

    private static bool CaptureSetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedRed = red;
        capturedGreen = green;
        capturedBlue = blue;
        return nextBool;
    }

    private static bool CaptureSendGamepadEffectPointer(IntPtr gamepad, IntPtr data, int size)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedEffectData = data;
        capturedSize = size;
        return nextBool;
    }

    private static bool CaptureSendGamepadEffectArray(IntPtr gamepad, byte[] data, int size)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedEffectBytes = data;
        capturedSize = size;
        return nextBool;
    }

    private static void CaptureCloseGamepad(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
    }

    private static IntPtr CaptureStringForGamepadButton(IntPtr gamepad, SDL3.SDL.GamepadButton button)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedButton = button;
        return nextPointer;
    }

    private static IntPtr CaptureStringForGamepadAxis(IntPtr gamepad, SDL3.SDL.GamepadAxis axis)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        capturedAxis = axis;
        return nextPointer;
    }

    private static ulong CaptureGamepadULong(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextULong;
    }

    private static SDL3.SDL.JoystickConnectionState CaptureGamepadConnectionState(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        return nextConnectionState;
    }

    private static SDL3.SDL.PowerState CaptureGamepadPowerInfo(IntPtr gamepad, out int percent)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
        percent = nextPercent;
        return nextPowerState;
    }

    private static IntPtr CaptureGetGamepadMappingForID(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextPointer;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeCallCount++;
        capturedFreeMemory = mem;
    }

    private static void ResetCaptureState()
    {
        capturedMapping = null;
        capturedFile = null;
        capturedString = null;
        capturedSource = IntPtr.Zero;
        capturedCloseIO = false;
        capturedEnabled = false;
        capturedInstanceId = 0;
        capturedGamepad = IntPtr.Zero;
        capturedPlayerIndex = 0;
        capturedTouchpad = 0;
        capturedFinger = 0;
        capturedGamepadType = SDL3.SDL.GamepadType.Unknown;
        capturedAxis = SDL3.SDL.GamepadAxis.Invalid;
        capturedButton = SDL3.SDL.GamepadButton.Invalid;
        capturedSensorType = SDL3.SDL.SensorType.Invalid;
        capturedCapSenseType = SDL3.SDL.GamepadCapSenseType.Invalid;
        capturedLowFrequencyRumble = 0;
        capturedHighFrequencyRumble = 0;
        capturedLeftRumble = 0;
        capturedRightRumble = 0;
        capturedDurationMs = 0;
        capturedRed = 0;
        capturedGreen = 0;
        capturedBlue = 0;
        capturedEffectData = IntPtr.Zero;
        capturedEffectBytes = null;
        capturedSize = 0;
        capturedCount = 0;
        capturedFreeMemory = IntPtr.Zero;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextInt = 0;
        nextUInt = 0;
        nextUShort = 0;
        nextULong = 0;
        nextShort = 0;
        nextGuid = default;
        nextGamepadType = SDL3.SDL.GamepadType.Unknown;
        nextGamepadAxis = SDL3.SDL.GamepadAxis.Invalid;
        nextGamepadButton = SDL3.SDL.GamepadButton.Invalid;
        nextGamepadButtonLabel = SDL3.SDL.GamepadButtonLabel.Unknown;
        nextConnectionState = SDL3.SDL.JoystickConnectionState.Invalid;
        nextPowerState = SDL3.SDL.PowerState.Error;
        nextPercent = 0;
        nextBool = false;
        nextDown = false;
        nextFloat = 0;
        nextX = 0;
        nextY = 0;
        nextPressure = 0;
        nextFloatArray = null;
        nextPointer = IntPtr.Zero;
        nextCount = 0;
    }

    private static SDL3.SDL.GUID CreateGuid(byte firstByte)
    {
        IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.GUID>());

        try
        {
            byte[] data = new byte[16];
            data[0] = firstByte;
            Marshal.Copy(data, 0, pointer, data.Length);
            return Marshal.PtrToStructure<SDL3.SDL.GUID>(pointer);
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    private static byte ReadGuidByte(SDL3.SDL.GUID guid, int index)
    {
        IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.GUID>());

        try
        {
            Marshal.StructureToPtr(guid, pointer, fDeleteOld: false);
            return Marshal.ReadByte(pointer, index);
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    private static IntPtr CreateNativeUIntArray(params uint[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(sizeof(uint) * values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(pointer, i * sizeof(uint), unchecked((int)values[i]));
        }

        return pointer;
    }

    private static IntPtr AllocateBinding(SDL3.SDL.GamepadBinding binding)
    {
        IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.GamepadBinding>());
        Marshal.StructureToPtr(binding, pointer, fDeleteOld: false);
        return pointer;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static MethodInfo GetNativeMethod(string methodName, params Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(
            methodName,
            BindingFlags.NonPublic | BindingFlags.Static,
            binder: null,
            types: parameterTypes,
            modifiers: null);

        TestAssert.NotNull(method, $"SDL.{methodName} overload must be private static.");
        return method!;
    }

    private static object? InvokePublic(string methodName, params object[] arguments)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} public wrapper must exist.");
        return method!.Invoke(null, arguments);
    }

    private static void AssertSdlImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method);
    }

    private static void AssertCdecl(MethodInfo method)
    {
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"SDL.{method.Name} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"SDL.{method.Name} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"SDL.{method.Name} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 marshalling.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType, short sizeParamIndex)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected array marshalling.");
        TestAssert.Equal(sizeParamIndex, marshalAs.SizeParamIndex, $"SDL.{method.Name} parameter {parameterName} must keep expected SizeParamIndex.");
    }

    private static void AssertOutParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.IsOut, $"SDL.{method.Name} parameter {parameterName} must be an out parameter.");
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? originalValue;

        private NativeHookScope(FieldInfo field, object? originalValue)
        {
            this.field = field;
            this.originalValue = originalValue;
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL.{fieldName} native hook field must exist.");
            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"{methodName} hook method must exist.");
            object? originalValue = field!.GetValue(null);
            Delegate hook = Delegate.CreateDelegate(field.FieldType, method!);
            field.SetValue(null, hook);
            return new NativeHookScope(field, originalValue);
        }

        public void Dispose()
        {
            field.SetValue(null, originalValue);
        }
    }
}
