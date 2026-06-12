using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Joystick;

internal static class PInvokeTests
{
    private static uint capturedInstanceId;
    private static int capturedPlayerIndex;
    private static IntPtr capturedJoystick;
    private static IntPtr capturedFreeMemory;
    private static SDL3.SDL.VirtualJoystickDesc capturedVirtualJoystickDesc;
    private static int capturedAxis;
    private static int capturedBall;
    private static int capturedButton;
    private static int capturedHatIndex;
    private static int capturedTouchpad;
    private static int capturedFinger;
    private static int capturedSize;
    private static bool capturedDown;
    private static bool capturedEnabled;
    private static short capturedAxisValue;
    private static short capturedXRel;
    private static short capturedYRel;
    private static short capturedLowFrequencyRumble;
    private static short capturedHighFrequencyRumble;
    private static short capturedLeftRumble;
    private static short capturedRightRumble;
    private static int capturedDurationMs;
    private static byte capturedRed;
    private static byte capturedGreen;
    private static byte capturedBlue;
    private static SDL3.SDL.GUID capturedGuid;
    private static SDL3.SDL.JoystickHat capturedHat;
    private static IntPtr capturedEffectData;
    private static byte[]? capturedEffectBytes;
    private static float capturedX;
    private static float capturedY;
    private static float capturedPressure;
    private static SDL3.SDL.SensorType capturedSensorType;
    private static ulong capturedSensorTimestamp;
    private static float[]? capturedFloatArray;
    private static int capturedNumValues;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static int nextInt;
    private static uint nextUInt;
    private static ushort nextUShort;
    private static short nextShort;
    private static int nextDx;
    private static int nextDy;
    private static short nextVendor;
    private static short nextProduct;
    private static short nextVersion;
    private static short nextCrc16;
    private static SDL3.SDL.GUID nextGuid;
    private static SDL3.SDL.JoystickType nextJoystickType;
    private static SDL3.SDL.JoystickHat nextJoystickHat;
    private static SDL3.SDL.JoystickConnectionState nextJoystickConnectionState;
    private static SDL3.SDL.PowerState nextPowerState;
    private static int nextPercent;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static int nextCount;

    public static void RunAll()
    {
        LockJoysticks_CallsNativeHook();
        UnlockJoysticks_CallsNativeHook();
        HasJoystick_ReturnsNativeValue();
        SDL_GetJoysticks_UsesExpectedNativeMetadata();
        GetJoysticks_ReturnsArrayNullAndFreesNativePointer();
        SDL_GetJoystickNameForID_UsesExpectedNativeMetadata();
        GetJoystickNameForID_ReturnsStringAndNull();
        SDL_GetJoystickPathForID_UsesExpectedNativeMetadata();
        GetJoystickPathForID_ReturnsStringAndNull();
        GetJoystickPlayerIndexForID_ForwardsInstanceAndReturnsNativeValue();
        GetJoystickGUIDForID_ForwardsInstanceAndReturnsNativeValue();
        GetJoystickVendorForID_ForwardsInstanceAndReturnsNativeValue();
        GetJoystickProductForID_ForwardsInstanceAndReturnsNativeValue();
        GetJoystickProductVersionForID_ForwardsInstanceAndReturnsNativeValue();
        GetJoystickTypeForID_ForwardsInstanceAndReturnsNativeValue();
        OpenJoystick_ForwardsInstanceAndReturnsNativePointer();
        GetJoystickFromID_ForwardsInstanceAndReturnsNativePointer();
        GetJoystickFromPlayerIndex_ForwardsPlayerIndexAndReturnsNativePointer();
        SDL_AttachVirtualJoystick_UsesExpectedNativeMetadata();
        AttachVirtualJoystick_ForwardsDescriptionAndReturnsNativeValue();
        DetachVirtualJoystick_ForwardsInstanceAndReturnsNativeValue();
        IsJoystickVirtual_ForwardsInstanceAndReturnsNativeValue();
        SetJoystickVirtualAxis_ForwardsVirtualAxisAndReturnsNativeValue();
        SetJoystickVirtualBall_ForwardsVirtualBallAndReturnsNativeValue();
        SetJoystickVirtualButton_ForwardsVirtualButtonAndReturnsNativeValue();
        SetJoystickVirtualHat_ForwardsVirtualHatAndReturnsNativeValue();
        SetJoystickVirtualTouchpad_ForwardsVirtualTouchpadAndReturnsNativeValue();
        SendJoystickVirtualSensorData_ForwardsVirtualSensorDataAndReturnsNativeValue();
        GetJoystickProperties_ForwardsJoystickAndReturnsNativeValue();
        SDL_GetJoystickName_UsesExpectedNativeMetadata();
        GetJoystickName_ReturnsStringAndNull();
        SDL_GetJoystickPath_UsesExpectedNativeMetadata();
        GetJoystickPath_ReturnsStringAndNull();
        GetJoystickPlayerIndex_ForwardsJoystickAndReturnsNativeValue();
        SetJoystickPlayerIndex_ForwardsJoystickPlayerIndexAndReturnsNativeValue();
        GetJoystickGUID_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickVendor_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickProduct_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickProductVersion_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickFirmwareVersion_ForwardsJoystickAndReturnsNativeValue();
        SDL_GetJoystickSerial_UsesExpectedNativeMetadata();
        GetJoystickSerial_ReturnsStringAndNull();
        GetJoystickType_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickGUIDInfo_ForwardsGuidAndOutputsNativeValues();
        JoystickConnected_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickID_ForwardsJoystickAndReturnsNativeValue();
        GetNumJoystickAxes_ForwardsJoystickAndReturnsNativeValue();
        GetNumJoystickBalls_ForwardsJoystickAndReturnsNativeValue();
        GetNumJoystickHats_ForwardsJoystickAndReturnsNativeValue();
        GetNumJoystickButtons_ForwardsJoystickAndReturnsNativeValue();
        SetJoystickEventsEnabled_ForwardsEnabled();
        JoystickEventsEnabled_ReturnsNativeValue();
        UpdateJoysticks_CallsNativeHook();
        GetJoystickAxis_ForwardsJoystickAxisAndReturnsNativeValue();
        GetJoystickAxisInitialState_ForwardsJoystickAxisAndOutputsState();
        GetJoystickBall_ForwardsJoystickBallAndOutputsDeltas();
        GetJoystickHat_ForwardsJoystickHatAndReturnsNativeValue();
        GetJoystickButton_ForwardsJoystickButtonAndReturnsNativeValue();
        RumbleJoystick_ForwardsRumbleAndReturnsNativeValue();
        RumbleJoystickTriggers_ForwardsTriggerRumbleAndReturnsNativeValue();
        SetJoystickLED_ForwardsColorAndReturnsNativeValue();
        SendJoystickEffect_WithPointer_ForwardsDataAndReturnsNativeValue();
        SendJoystickEffect_WithArray_ForwardsDataAndReturnsNativeValue();
        CloseJoystick_ForwardsJoystick();
        GetJoystickConnectionState_ForwardsJoystickAndReturnsNativeValue();
        GetJoystickPowerInfo_ForwardsJoystickAndOutputsPercent();
    }

    public static void LockJoysticks_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LockJoysticks");
        AssertSdlImport(nativeMethod, "SDL_LockJoysticks");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("LockJoysticksNativeFunction", nameof(CaptureNoArgumentVoid));
        SDL3.SDL.LockJoysticks();

        TestAssert.Equal(1, capturedCallCount, "SDL.LockJoysticks must call the native hook once.");
    }

    public static void UnlockJoysticks_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UnlockJoysticks");
        AssertSdlImport(nativeMethod, "SDL_UnlockJoysticks");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("UnlockJoysticksNativeFunction", nameof(CaptureNoArgumentVoid));
        SDL3.SDL.UnlockJoysticks();

        TestAssert.Equal(1, capturedCallCount, "SDL.UnlockJoysticks must call the native hook once.");
    }

    public static void HasJoystick_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasJoystick");
        AssertSdlImport(nativeMethod, "SDL_HasJoystick");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasJoystickNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.HasJoystick();

        TestAssert.Equal(true, result, "SDL.HasJoystick must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasJoystick must call the native hook once.");
    }

    public static void SDL_GetJoysticks_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoysticks");
        AssertSdlImport(nativeMethod, "SDL_GetJoysticks");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetJoysticks_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr array = CreateNativeUIntArray(10u, 20u, 30u);

        try
        {
            nextPointer = array;
            nextCount = 3;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetJoysticksNativeFunction", nameof(CaptureArrayPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            uint[]? joysticks = SDL3.SDL.GetJoysticks(out int count);

            TestAssert.NotNull(joysticks, "SDL.GetJoysticks must convert native joystick IDs.");
            TestAssert.Equal(3, joysticks!.Length, "SDL.GetJoysticks must preserve native count.");
            TestAssert.Equal(10u, joysticks[0], "SDL.GetJoysticks must convert ID 0.");
            TestAssert.Equal(20u, joysticks[1], "SDL.GetJoysticks must convert ID 1.");
            TestAssert.Equal(30u, joysticks[2], "SDL.GetJoysticks must convert ID 2.");
            TestAssert.Equal(3, count, "SDL.GetJoysticks must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetJoysticks must free the native array pointer.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            joysticks = SDL3.SDL.GetJoysticks(out count);

            TestAssert.Equal<uint[]?>(null, joysticks, "SDL.GetJoysticks must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetJoysticks must return native count for native null.");
            TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.GetJoysticks must pass native null to SDL.Free.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetJoysticks must call the native hook for both branches.");
            TestAssert.Equal(2, capturedFreeCallCount, "SDL.GetJoysticks must call SDL.Free for both branches.");
        }
        finally
        {
            Marshal.FreeHGlobal(array);
        }
    }

    public static void SDL_GetJoystickNameForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickNameForID");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickNameForID");
    }

    public static void GetJoystickNameForID_ReturnsStringAndNull()
    {
        AssertInstanceStringMethod(
            "SDL.GetJoystickNameForID",
            "GetJoystickNameForIDNativeFunction",
            nameof(CaptureInstancePointer),
            "joystick-name",
            static () => SDL3.SDL.GetJoystickNameForID(111u),
            static () => SDL3.SDL.GetJoystickNameForID(112u));
    }

    public static void SDL_GetJoystickPathForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickPathForID");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickPathForID");
    }

    public static void GetJoystickPathForID_ReturnsStringAndNull()
    {
        AssertInstanceStringMethod(
            "SDL.GetJoystickPathForID",
            "GetJoystickPathForIDNativeFunction",
            nameof(CaptureInstancePointer),
            "joystick-path",
            static () => SDL3.SDL.GetJoystickPathForID(121u),
            static () => SDL3.SDL.GetJoystickPathForID(122u));
    }

    public static void GetJoystickPlayerIndexForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceIntMethod("SDL.GetJoystickPlayerIndexForID", "SDL_GetJoystickPlayerIndexForID", "GetJoystickPlayerIndexForIDNativeFunction", 0x101u, 4);
    }

    public static void GetJoystickGUIDForID_ForwardsInstanceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickGUIDForID");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickGUIDForID");

        ResetCaptureState();
        nextGuid = CreateGuid(0x7A);

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickGUIDForIDNativeFunction", nameof(CaptureInstanceGuid));
        SDL3.SDL.GUID result = SDL3.SDL.GetJoystickGUIDForID(0x102u);

        TestAssert.Equal((byte)0x7A, ReadGuidByte(result, 0), "SDL.GetJoystickGUIDForID must return the native hook value.");
        TestAssert.Equal(0x102u, capturedInstanceId, "SDL.GetJoystickGUIDForID must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickGUIDForID must call the native hook once.");
    }

    public static void GetJoystickVendorForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceUShortMethod("SDL.GetJoystickVendorForID", "SDL_GetJoystickVendorForID", "GetJoystickVendorForIDNativeFunction", 0x103u, 0x045E);
    }

    public static void GetJoystickProductForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceUShortMethod("SDL.GetJoystickProductForID", "SDL_GetJoystickProductForID", "GetJoystickProductForIDNativeFunction", 0x104u, 0x028E);
    }

    public static void GetJoystickProductVersionForID_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceUShortMethod("SDL.GetJoystickProductVersionForID", "SDL_GetJoystickProductVersionForID", "GetJoystickProductVersionForIDNativeFunction", 0x105u, 0x0114);
    }

    public static void GetJoystickTypeForID_ForwardsInstanceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickTypeForID");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickTypeForID");

        ResetCaptureState();
        nextJoystickType = SDL3.SDL.JoystickType.Gamepad;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickTypeForIDNativeFunction", nameof(CaptureInstanceJoystickType));
        SDL3.SDL.JoystickType result = SDL3.SDL.GetJoystickTypeForID(0x106u);

        TestAssert.Equal(SDL3.SDL.JoystickType.Gamepad, result, "SDL.GetJoystickTypeForID must return the native hook value.");
        TestAssert.Equal(0x106u, capturedInstanceId, "SDL.GetJoystickTypeForID must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickTypeForID must call the native hook once.");
    }

    public static void OpenJoystick_ForwardsInstanceAndReturnsNativePointer()
    {
        AssertInstancePointerMethod("SDL.OpenJoystick", "SDL_OpenJoystick", "OpenJoystickNativeFunction", 0x107u, (IntPtr)0x7501);
    }

    public static void GetJoystickFromID_ForwardsInstanceAndReturnsNativePointer()
    {
        AssertInstancePointerMethod("SDL.GetJoystickFromID", "SDL_GetJoystickFromID", "GetJoystickFromIDNativeFunction", 0x108u, (IntPtr)0x7502);
    }

    public static void GetJoystickFromPlayerIndex_ForwardsPlayerIndexAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickFromPlayerIndex");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickFromPlayerIndex");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7503;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickFromPlayerIndexNativeFunction", nameof(CapturePlayerIndexPointer));
        IntPtr result = SDL3.SDL.GetJoystickFromPlayerIndex(2);

        TestAssert.Equal((IntPtr)0x7503, result, "SDL.GetJoystickFromPlayerIndex must return the native hook value.");
        TestAssert.Equal(2, capturedPlayerIndex, "SDL.GetJoystickFromPlayerIndex must forward playerIndex.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickFromPlayerIndex must call the native hook once.");
    }

    public static void SDL_AttachVirtualJoystick_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AttachVirtualJoystick");
        AssertDllImport(nativeMethod, "SDL_AttachVirtualJoystick");

        ParameterInfo parameter = nativeMethod.GetParameters().Single(param => param.Name == "desc");
        TestAssert.Equal(typeof(SDL3.SDL.VirtualJoystickDesc).MakeByRefType(), parameter.ParameterType, "SDL_AttachVirtualJoystick must pass desc by readonly reference.");
    }

    public static void AttachVirtualJoystick_ForwardsDescriptionAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 0x9001u;
        SDL3.SDL.VirtualJoystickDesc desc = new()
        {
            Version = 77u,
            Type = SDL3.SDL.JoystickType.Gamepad,
            VendorID = 0x045E,
            ProductID = 0x028E,
            NAxes = 6,
            NButtons = 12
        };

        using NativeHookScope _ = NativeHookScope.Install("AttachVirtualJoystickNativeFunction", nameof(CaptureAttachVirtualJoystick));
        uint result = SDL3.SDL.AttachVirtualJoystick(in desc);

        TestAssert.Equal(0x9001u, result, "SDL.AttachVirtualJoystick must return the native hook value.");
        TestAssert.Equal(77u, capturedVirtualJoystickDesc.Version, "SDL.AttachVirtualJoystick must forward desc.Version.");
        TestAssert.Equal(SDL3.SDL.JoystickType.Gamepad, capturedVirtualJoystickDesc.Type, "SDL.AttachVirtualJoystick must forward desc.Type.");
        TestAssert.Equal((ushort)0x045E, capturedVirtualJoystickDesc.VendorID, "SDL.AttachVirtualJoystick must forward desc.VendorID.");
        TestAssert.Equal((ushort)0x028E, capturedVirtualJoystickDesc.ProductID, "SDL.AttachVirtualJoystick must forward desc.ProductID.");
        TestAssert.Equal((ushort)6, capturedVirtualJoystickDesc.NAxes, "SDL.AttachVirtualJoystick must forward desc.NAxes.");
        TestAssert.Equal((ushort)12, capturedVirtualJoystickDesc.NButtons, "SDL.AttachVirtualJoystick must forward desc.NButtons.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AttachVirtualJoystick must call the native hook once.");
    }

    public static void DetachVirtualJoystick_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceBoolMethod("SDL.DetachVirtualJoystick", "SDL_DetachVirtualJoystick", "DetachVirtualJoystickNativeFunction", 0x201u, true);
    }

    public static void IsJoystickVirtual_ForwardsInstanceAndReturnsNativeValue()
    {
        AssertInstanceBoolMethod("SDL.IsJoystickVirtual", "SDL_IsJoystickVirtual", "IsJoystickVirtualNativeFunction", 0x202u, true);
    }

    public static void SetJoystickVirtualAxis_ForwardsVirtualAxisAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickVirtualAxis");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickVirtualAxis");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickVirtualAxisNativeFunction", nameof(CaptureSetJoystickVirtualAxis));
        bool result = SDL3.SDL.SetJoystickVirtualAxis((IntPtr)0x7601, 2, -1234);

        TestAssert.Equal(true, result, "SDL.SetJoystickVirtualAxis must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7601, capturedJoystick, "SDL.SetJoystickVirtualAxis must forward joystick.");
        TestAssert.Equal(2, capturedAxis, "SDL.SetJoystickVirtualAxis must forward axis.");
        TestAssert.Equal((short)-1234, capturedAxisValue, "SDL.SetJoystickVirtualAxis must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickVirtualAxis must call the native hook once.");
    }

    public static void SetJoystickVirtualBall_ForwardsVirtualBallAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickVirtualBall");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickVirtualBall");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickVirtualBallNativeFunction", nameof(CaptureSetJoystickVirtualBall));
        bool result = SDL3.SDL.SetJoystickVirtualBall((IntPtr)0x7602, 3, -14, 15);

        TestAssert.Equal(true, result, "SDL.SetJoystickVirtualBall must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7602, capturedJoystick, "SDL.SetJoystickVirtualBall must forward joystick.");
        TestAssert.Equal(3, capturedBall, "SDL.SetJoystickVirtualBall must forward ball.");
        TestAssert.Equal((short)-14, capturedXRel, "SDL.SetJoystickVirtualBall must forward xrel.");
        TestAssert.Equal((short)15, capturedYRel, "SDL.SetJoystickVirtualBall must forward yrel.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickVirtualBall must call the native hook once.");
    }

    public static void SetJoystickVirtualButton_ForwardsVirtualButtonAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickVirtualButton");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickVirtualButton");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "down");

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickVirtualButtonNativeFunction", nameof(CaptureSetJoystickVirtualButton));
        bool result = SDL3.SDL.SetJoystickVirtualButton((IntPtr)0x7603, 4, true);

        TestAssert.Equal(true, result, "SDL.SetJoystickVirtualButton must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7603, capturedJoystick, "SDL.SetJoystickVirtualButton must forward joystick.");
        TestAssert.Equal(4, capturedButton, "SDL.SetJoystickVirtualButton must forward button.");
        TestAssert.Equal(true, capturedDown, "SDL.SetJoystickVirtualButton must forward down.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickVirtualButton must call the native hook once.");
    }

    public static void SetJoystickVirtualHat_ForwardsVirtualHatAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickVirtualHat");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickVirtualHat");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickVirtualHatNativeFunction", nameof(CaptureSetJoystickVirtualHat));
        bool result = SDL3.SDL.SetJoystickVirtualHat((IntPtr)0x7604, 5, SDL3.SDL.JoystickHat.RightDown);

        TestAssert.Equal(true, result, "SDL.SetJoystickVirtualHat must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7604, capturedJoystick, "SDL.SetJoystickVirtualHat must forward joystick.");
        TestAssert.Equal(5, capturedHatIndex, "SDL.SetJoystickVirtualHat must forward hat.");
        TestAssert.Equal(SDL3.SDL.JoystickHat.RightDown, capturedHat, "SDL.SetJoystickVirtualHat must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickVirtualHat must call the native hook once.");
    }

    public static void SetJoystickVirtualTouchpad_ForwardsVirtualTouchpadAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickVirtualTouchpad");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickVirtualTouchpad");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "down");

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickVirtualTouchpadNativeFunction", nameof(CaptureSetJoystickVirtualTouchpad));
        bool result = SDL3.SDL.SetJoystickVirtualTouchpad((IntPtr)0x7605, 1, 2, true, 0.25f, 0.5f, 0.75f);

        TestAssert.Equal(true, result, "SDL.SetJoystickVirtualTouchpad must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7605, capturedJoystick, "SDL.SetJoystickVirtualTouchpad must forward joystick.");
        TestAssert.Equal(1, capturedTouchpad, "SDL.SetJoystickVirtualTouchpad must forward touchpad.");
        TestAssert.Equal(2, capturedFinger, "SDL.SetJoystickVirtualTouchpad must forward finger.");
        TestAssert.Equal(true, capturedDown, "SDL.SetJoystickVirtualTouchpad must forward down.");
        TestAssert.Equal(0.25f, capturedX, "SDL.SetJoystickVirtualTouchpad must forward x.");
        TestAssert.Equal(0.5f, capturedY, "SDL.SetJoystickVirtualTouchpad must forward y.");
        TestAssert.Equal(0.75f, capturedPressure, "SDL.SetJoystickVirtualTouchpad must forward pressure.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickVirtualTouchpad must call the native hook once.");
    }

    public static void SendJoystickVirtualSensorData_ForwardsVirtualSensorDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendJoystickVirtualSensorData");
        AssertSdlImport(nativeMethod, "SDL_SendJoystickVirtualSensorData");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 4);

        ResetCaptureState();
        nextBool = true;
        float[] data = [1.0f, 2.0f, 3.0f];

        using NativeHookScope _ = NativeHookScope.Install("SendJoystickVirtualSensorDataNativeFunction", nameof(CaptureSendJoystickVirtualSensorData));
        bool result = SDL3.SDL.SendJoystickVirtualSensorData((IntPtr)0x7606, SDL3.SDL.SensorType.GyroL, 123456789UL, data, data.Length);

        TestAssert.Equal(true, result, "SDL.SendJoystickVirtualSensorData must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7606, capturedJoystick, "SDL.SendJoystickVirtualSensorData must forward joystick.");
        TestAssert.Equal(SDL3.SDL.SensorType.GyroL, capturedSensorType, "SDL.SendJoystickVirtualSensorData must forward type.");
        TestAssert.Equal(123456789UL, capturedSensorTimestamp, "SDL.SendJoystickVirtualSensorData must forward sensorTimestamp.");
        TestAssert.Equal(3, capturedNumValues, "SDL.SendJoystickVirtualSensorData must forward numValues.");
        TestAssert.True(ReferenceEquals(data, capturedFloatArray), "SDL.SendJoystickVirtualSensorData must forward the managed data array.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SendJoystickVirtualSensorData must call the native hook once.");
    }

    public static void GetJoystickProperties_ForwardsJoystickAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickProperties");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickProperties");

        ResetCaptureState();
        nextUInt = 0x9002u;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickPropertiesNativeFunction", nameof(CaptureJoystickUInt));
        uint result = SDL3.SDL.GetJoystickProperties((IntPtr)0x7607);

        TestAssert.Equal(0x9002u, result, "SDL.GetJoystickProperties must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7607, capturedJoystick, "SDL.GetJoystickProperties must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickProperties must call the native hook once.");
    }

    public static void SDL_GetJoystickName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickName");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickName");
    }

    public static void GetJoystickName_ReturnsStringAndNull()
    {
        AssertJoystickStringMethod(
            "SDL.GetJoystickName",
            "GetJoystickNameNativeFunction",
            "joystick-name",
            (IntPtr)0x7701,
            (IntPtr)0x7702,
            SDL3.SDL.GetJoystickName);
    }

    public static void SDL_GetJoystickPath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickPath");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickPath");
    }

    public static void GetJoystickPath_ReturnsStringAndNull()
    {
        AssertJoystickStringMethod(
            "SDL.GetJoystickPath",
            "GetJoystickPathNativeFunction",
            "joystick-path",
            (IntPtr)0x7703,
            (IntPtr)0x7704,
            SDL3.SDL.GetJoystickPath);
    }

    public static void GetJoystickPlayerIndex_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickIntMethod("SDL.GetJoystickPlayerIndex", "SDL_GetJoystickPlayerIndex", "GetJoystickPlayerIndexNativeFunction", (IntPtr)0x7705, 7);
    }

    public static void SetJoystickPlayerIndex_ForwardsJoystickPlayerIndexAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickPlayerIndex");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickPlayerIndex");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickPlayerIndexNativeFunction", nameof(CaptureSetJoystickPlayerIndex));
        bool result = SDL3.SDL.SetJoystickPlayerIndex((IntPtr)0x7706, 8);

        TestAssert.Equal(true, result, "SDL.SetJoystickPlayerIndex must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7706, capturedJoystick, "SDL.SetJoystickPlayerIndex must forward joystick.");
        TestAssert.Equal(8, capturedPlayerIndex, "SDL.SetJoystickPlayerIndex must forward playerIndex.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickPlayerIndex must call the native hook once.");
    }

    public static void GetJoystickGUID_ForwardsJoystickAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickGUID");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickGUID");

        ResetCaptureState();
        nextGuid = CreateGuid(0x7B);

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickGUIDNativeFunction", nameof(CaptureJoystickGuid));
        SDL3.SDL.GUID result = SDL3.SDL.GetJoystickGUID((IntPtr)0x7707);

        TestAssert.Equal((byte)0x7B, ReadGuidByte(result, 0), "SDL.GetJoystickGUID must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7707, capturedJoystick, "SDL.GetJoystickGUID must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickGUID must call the native hook once.");
    }

    public static void GetJoystickVendor_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickUShortMethod("SDL.GetJoystickVendor", "SDL_GetJoystickVendor", "GetJoystickVendorNativeFunction", (IntPtr)0x7708, 0x045E);
    }

    public static void GetJoystickProduct_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickUShortMethod("SDL.GetJoystickProduct", "SDL_GetJoystickProduct", "GetJoystickProductNativeFunction", (IntPtr)0x7709, 0x028E);
    }

    public static void GetJoystickProductVersion_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickUShortMethod("SDL.GetJoystickProductVersion", "SDL_GetJoystickProductVersion", "GetJoystickProductVersionNativeFunction", (IntPtr)0x770A, 0x0114);
    }

    public static void GetJoystickFirmwareVersion_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickUShortMethod("SDL.GetJoystickFirmwareVersion", "SDL_GetJoystickFirmwareVersion", "GetJoystickFirmwareVersionNativeFunction", (IntPtr)0x770B, 0x1002);
    }

    public static void SDL_GetJoystickSerial_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickSerial");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickSerial");
    }

    public static void GetJoystickSerial_ReturnsStringAndNull()
    {
        AssertJoystickStringMethod(
            "SDL.GetJoystickSerial",
            "GetJoystickSerialNativeFunction",
            "joystick-serial",
            (IntPtr)0x770C,
            (IntPtr)0x770D,
            SDL3.SDL.GetJoystickSerial);
    }

    public static void GetJoystickType_ForwardsJoystickAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickType");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickType");

        ResetCaptureState();
        nextJoystickType = SDL3.SDL.JoystickType.Wheel;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickTypeNativeFunction", nameof(CaptureJoystickType));
        SDL3.SDL.JoystickType result = SDL3.SDL.GetJoystickType((IntPtr)0x770E);

        TestAssert.Equal(SDL3.SDL.JoystickType.Wheel, result, "SDL.GetJoystickType must return the native hook value.");
        TestAssert.Equal((IntPtr)0x770E, capturedJoystick, "SDL.GetJoystickType must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickType must call the native hook once.");
    }

    public static void GetJoystickGUIDInfo_ForwardsGuidAndOutputsNativeValues()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickGUIDInfo");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickGUIDInfo");
        AssertOutParameter(nativeMethod, "vendor");
        AssertOutParameter(nativeMethod, "product");
        AssertOutParameter(nativeMethod, "version");
        AssertOutParameter(nativeMethod, "crc16");

        ResetCaptureState();
        SDL3.SDL.GUID guid = CreateGuid(0x7C);
        nextVendor = 0x045E;
        nextProduct = 0x028E;
        nextVersion = 0x0114;
        nextCrc16 = 0x2233;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickGUIDInfoNativeFunction", nameof(CaptureJoystickGuidInfo));
        SDL3.SDL.GetJoystickGUIDInfo(guid, out short vendor, out short product, out short version, out short crc16);

        TestAssert.Equal((byte)0x7C, ReadGuidByte(capturedGuid, 0), "SDL.GetJoystickGUIDInfo must forward guid.");
        TestAssert.Equal((short)0x045E, vendor, "SDL.GetJoystickGUIDInfo must output vendor.");
        TestAssert.Equal((short)0x028E, product, "SDL.GetJoystickGUIDInfo must output product.");
        TestAssert.Equal((short)0x0114, version, "SDL.GetJoystickGUIDInfo must output version.");
        TestAssert.Equal((short)0x2233, crc16, "SDL.GetJoystickGUIDInfo must output crc16.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickGUIDInfo must call the native hook once.");
    }

    public static void JoystickConnected_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickBoolMethod("SDL.JoystickConnected", "SDL_JoystickConnected", "JoystickConnectedNativeFunction", (IntPtr)0x7801, true);
    }

    public static void GetJoystickID_ForwardsJoystickAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickID");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickID");

        ResetCaptureState();
        nextUInt = 0x9003u;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickIDNativeFunction", nameof(CaptureJoystickUInt));
        uint result = SDL3.SDL.GetJoystickID((IntPtr)0x7802);

        TestAssert.Equal(0x9003u, result, "SDL.GetJoystickID must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7802, capturedJoystick, "SDL.GetJoystickID must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickID must call the native hook once.");
    }

    public static void GetNumJoystickAxes_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickIntMethod("SDL.GetNumJoystickAxes", "SDL_GetNumJoystickAxes", "GetNumJoystickAxesNativeFunction", (IntPtr)0x7803, 6);
    }

    public static void GetNumJoystickBalls_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickIntMethod("SDL.GetNumJoystickBalls", "SDL_GetNumJoystickBalls", "GetNumJoystickBallsNativeFunction", (IntPtr)0x7804, 1);
    }

    public static void GetNumJoystickHats_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickIntMethod("SDL.GetNumJoystickHats", "SDL_GetNumJoystickHats", "GetNumJoystickHatsNativeFunction", (IntPtr)0x7805, 2);
    }

    public static void GetNumJoystickButtons_ForwardsJoystickAndReturnsNativeValue()
    {
        AssertJoystickIntMethod("SDL.GetNumJoystickButtons", "SDL_GetNumJoystickButtons", "GetNumJoystickButtonsNativeFunction", (IntPtr)0x7806, 12);
    }

    public static void SetJoystickEventsEnabled_ForwardsEnabled()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickEventsEnabled");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickEventsEnabled");
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickEventsEnabledNativeFunction", nameof(CaptureSetJoystickEventsEnabled));
        SDL3.SDL.SetJoystickEventsEnabled(true);

        TestAssert.Equal(true, capturedEnabled, "SDL.SetJoystickEventsEnabled must forward enabled.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickEventsEnabled must call the native hook once.");
    }

    public static void JoystickEventsEnabled_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_JoystickEventsEnabled");
        AssertSdlImport(nativeMethod, "SDL_JoystickEventsEnabled");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("JoystickEventsEnabledNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.JoystickEventsEnabled();

        TestAssert.Equal(true, result, "SDL.JoystickEventsEnabled must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.JoystickEventsEnabled must call the native hook once.");
    }

    public static void UpdateJoysticks_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UpdateJoysticks");
        AssertSdlImport(nativeMethod, "SDL_UpdateJoysticks");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("UpdateJoysticksNativeFunction", nameof(CaptureNoArgumentVoid));
        SDL3.SDL.UpdateJoysticks();

        TestAssert.Equal(1, capturedCallCount, "SDL.UpdateJoysticks must call the native hook once.");
    }

    public static void GetJoystickAxis_ForwardsJoystickAxisAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickAxis");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickAxis");

        ResetCaptureState();
        nextShort = -2345;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickAxisNativeFunction", nameof(CaptureGetJoystickAxis));
        short result = SDL3.SDL.GetJoystickAxis((IntPtr)0x7901, 2);

        TestAssert.Equal((short)-2345, result, "SDL.GetJoystickAxis must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7901, capturedJoystick, "SDL.GetJoystickAxis must forward joystick.");
        TestAssert.Equal(2, capturedAxis, "SDL.GetJoystickAxis must forward axis.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickAxis must call the native hook once.");
    }

    public static void GetJoystickAxisInitialState_ForwardsJoystickAxisAndOutputsState()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickAxisInitialState");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickAxisInitialState");
        AssertBoolReturnMarshal(nativeMethod);
        AssertOutParameter(nativeMethod, "state");

        ResetCaptureState();
        nextBool = true;
        nextShort = 1234;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickAxisInitialStateNativeFunction", nameof(CaptureGetJoystickAxisInitialState));
        bool result = SDL3.SDL.GetJoystickAxisInitialState((IntPtr)0x7902, 3, out short state);

        TestAssert.Equal(true, result, "SDL.GetJoystickAxisInitialState must return the native hook value.");
        TestAssert.Equal((short)1234, state, "SDL.GetJoystickAxisInitialState must output state.");
        TestAssert.Equal((IntPtr)0x7902, capturedJoystick, "SDL.GetJoystickAxisInitialState must forward joystick.");
        TestAssert.Equal(3, capturedAxis, "SDL.GetJoystickAxisInitialState must forward axis.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickAxisInitialState must call the native hook once.");
    }

    public static void GetJoystickBall_ForwardsJoystickBallAndOutputsDeltas()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickBall");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickBall");
        AssertBoolReturnMarshal(nativeMethod);
        AssertOutParameter(nativeMethod, "dx");
        AssertOutParameter(nativeMethod, "dy");

        ResetCaptureState();
        nextBool = true;
        nextDx = -5;
        nextDy = 6;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickBallNativeFunction", nameof(CaptureGetJoystickBall));
        bool result = SDL3.SDL.GetJoystickBall((IntPtr)0x7903, 1, out int dx, out int dy);

        TestAssert.Equal(true, result, "SDL.GetJoystickBall must return the native hook value.");
        TestAssert.Equal(-5, dx, "SDL.GetJoystickBall must output dx.");
        TestAssert.Equal(6, dy, "SDL.GetJoystickBall must output dy.");
        TestAssert.Equal((IntPtr)0x7903, capturedJoystick, "SDL.GetJoystickBall must forward joystick.");
        TestAssert.Equal(1, capturedBall, "SDL.GetJoystickBall must forward ball.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickBall must call the native hook once.");
    }

    public static void GetJoystickHat_ForwardsJoystickHatAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickHat");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickHat");

        ResetCaptureState();
        nextJoystickHat = SDL3.SDL.JoystickHat.LeftUp;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickHatNativeFunction", nameof(CaptureGetJoystickHat));
        SDL3.SDL.JoystickHat result = SDL3.SDL.GetJoystickHat((IntPtr)0x7904, 2);

        TestAssert.Equal(SDL3.SDL.JoystickHat.LeftUp, result, "SDL.GetJoystickHat must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7904, capturedJoystick, "SDL.GetJoystickHat must forward joystick.");
        TestAssert.Equal(2, capturedHatIndex, "SDL.GetJoystickHat must forward hat.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickHat must call the native hook once.");
    }

    public static void GetJoystickButton_ForwardsJoystickButtonAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickButton");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickButton");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickButtonNativeFunction", nameof(CaptureGetJoystickButton));
        bool result = SDL3.SDL.GetJoystickButton((IntPtr)0x7905, 4);

        TestAssert.Equal(true, result, "SDL.GetJoystickButton must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7905, capturedJoystick, "SDL.GetJoystickButton must forward joystick.");
        TestAssert.Equal(4, capturedButton, "SDL.GetJoystickButton must forward button.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickButton must call the native hook once.");
    }

    public static void RumbleJoystick_ForwardsRumbleAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RumbleJoystick");
        AssertSdlImport(nativeMethod, "SDL_RumbleJoystick");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("RumbleJoystickNativeFunction", nameof(CaptureRumbleJoystick));
        bool result = SDL3.SDL.RumbleJoystick((IntPtr)0x7906, 10, 20, 30);

        TestAssert.Equal(true, result, "SDL.RumbleJoystick must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7906, capturedJoystick, "SDL.RumbleJoystick must forward joystick.");
        TestAssert.Equal((short)10, capturedLowFrequencyRumble, "SDL.RumbleJoystick must forward lowFrequencyRumble.");
        TestAssert.Equal((short)20, capturedHighFrequencyRumble, "SDL.RumbleJoystick must forward highFrequencyRumble.");
        TestAssert.Equal(30, capturedDurationMs, "SDL.RumbleJoystick must forward durationMs.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RumbleJoystick must call the native hook once.");
    }

    public static void RumbleJoystickTriggers_ForwardsTriggerRumbleAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RumbleJoystickTriggers");
        AssertSdlImport(nativeMethod, "SDL_RumbleJoystickTriggers");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("RumbleJoystickTriggersNativeFunction", nameof(CaptureRumbleJoystickTriggers));
        bool result = SDL3.SDL.RumbleJoystickTriggers((IntPtr)0x7907, 40, 50, 60);

        TestAssert.Equal(true, result, "SDL.RumbleJoystickTriggers must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7907, capturedJoystick, "SDL.RumbleJoystickTriggers must forward joystick.");
        TestAssert.Equal((short)40, capturedLeftRumble, "SDL.RumbleJoystickTriggers must forward leftRumble.");
        TestAssert.Equal((short)50, capturedRightRumble, "SDL.RumbleJoystickTriggers must forward rightRumble.");
        TestAssert.Equal(60, capturedDurationMs, "SDL.RumbleJoystickTriggers must forward durationMs.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RumbleJoystickTriggers must call the native hook once.");
    }

    public static void SetJoystickLED_ForwardsColorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetJoystickLED");
        AssertSdlImport(nativeMethod, "SDL_SetJoystickLED");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetJoystickLEDNativeFunction", nameof(CaptureSetJoystickLed));
        bool result = SDL3.SDL.SetJoystickLED((IntPtr)0x7908, 7, 8, 9);

        TestAssert.Equal(true, result, "SDL.SetJoystickLED must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7908, capturedJoystick, "SDL.SetJoystickLED must forward joystick.");
        TestAssert.Equal((byte)7, capturedRed, "SDL.SetJoystickLED must forward red.");
        TestAssert.Equal((byte)8, capturedGreen, "SDL.SetJoystickLED must forward green.");
        TestAssert.Equal((byte)9, capturedBlue, "SDL.SetJoystickLED must forward blue.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetJoystickLED must call the native hook once.");
    }

    public static void SendJoystickEffect_WithPointer_ForwardsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendJoystickEffect", typeof(IntPtr), typeof(IntPtr), typeof(int));
        AssertSdlImport(nativeMethod, "SDL_SendJoystickEffect");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SendJoystickEffectPointerNativeFunction", nameof(CaptureSendJoystickEffectPointer));
        bool result = SDL3.SDL.SendJoystickEffect((IntPtr)0x7909, (IntPtr)0x790A, 11);

        TestAssert.Equal(true, result, "SDL.SendJoystickEffect(IntPtr) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7909, capturedJoystick, "SDL.SendJoystickEffect(IntPtr) must forward joystick.");
        TestAssert.Equal((IntPtr)0x790A, capturedEffectData, "SDL.SendJoystickEffect(IntPtr) must forward data.");
        TestAssert.Equal(11, capturedSize, "SDL.SendJoystickEffect(IntPtr) must forward size.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SendJoystickEffect(IntPtr) must call the native hook once.");
    }

    public static void SendJoystickEffect_WithArray_ForwardsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendJoystickEffect", typeof(IntPtr), typeof(byte[]), typeof(int));
        AssertSdlImport(nativeMethod, "SDL_SendJoystickEffect");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);

        ResetCaptureState();
        nextBool = true;
        byte[] effect = [1, 2, 3, 4];

        using NativeHookScope _ = NativeHookScope.Install("SendJoystickEffectArrayNativeFunction", nameof(CaptureSendJoystickEffectArray));
        bool result = SDL3.SDL.SendJoystickEffect((IntPtr)0x790B, effect, effect.Length);

        TestAssert.Equal(true, result, "SDL.SendJoystickEffect(byte[]) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x790B, capturedJoystick, "SDL.SendJoystickEffect(byte[]) must forward joystick.");
        TestAssert.True(ReferenceEquals(effect, capturedEffectBytes), "SDL.SendJoystickEffect(byte[]) must forward the same data array.");
        TestAssert.Equal(4, capturedSize, "SDL.SendJoystickEffect(byte[]) must forward size.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SendJoystickEffect(byte[]) must call the native hook once.");
    }

    public static void CloseJoystick_ForwardsJoystick()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CloseJoystick");
        AssertSdlImport(nativeMethod, "SDL_CloseJoystick");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("CloseJoystickNativeFunction", nameof(CaptureCloseJoystick));
        SDL3.SDL.CloseJoystick((IntPtr)0x790C);

        TestAssert.Equal((IntPtr)0x790C, capturedJoystick, "SDL.CloseJoystick must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CloseJoystick must call the native hook once.");
    }

    public static void GetJoystickConnectionState_ForwardsJoystickAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickConnectionState");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickConnectionState");

        ResetCaptureState();
        nextJoystickConnectionState = SDL3.SDL.JoystickConnectionState.Wireless;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickConnectionStateNativeFunction", nameof(CaptureJoystickConnectionState));
        SDL3.SDL.JoystickConnectionState result = SDL3.SDL.GetJoystickConnectionState((IntPtr)0x790D);

        TestAssert.Equal(SDL3.SDL.JoystickConnectionState.Wireless, result, "SDL.GetJoystickConnectionState must return the native hook value.");
        TestAssert.Equal((IntPtr)0x790D, capturedJoystick, "SDL.GetJoystickConnectionState must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickConnectionState must call the native hook once.");
    }

    public static void GetJoystickPowerInfo_ForwardsJoystickAndOutputsPercent()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetJoystickPowerInfo");
        AssertSdlImport(nativeMethod, "SDL_GetJoystickPowerInfo");
        AssertOutParameter(nativeMethod, "percent");

        ResetCaptureState();
        nextPowerState = SDL3.SDL.PowerState.Charging;
        nextPercent = 67;

        using NativeHookScope _ = NativeHookScope.Install("GetJoystickPowerInfoNativeFunction", nameof(CaptureJoystickPowerInfo));
        SDL3.SDL.PowerState result = SDL3.SDL.GetJoystickPowerInfo((IntPtr)0x790E, out int percent);

        TestAssert.Equal(SDL3.SDL.PowerState.Charging, result, "SDL.GetJoystickPowerInfo must return the native hook value.");
        TestAssert.Equal(67, percent, "SDL.GetJoystickPowerInfo must output percent.");
        TestAssert.Equal((IntPtr)0x790E, capturedJoystick, "SDL.GetJoystickPowerInfo must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetJoystickPowerInfo must call the native hook once.");
    }

    private static void AssertInstanceStringMethod(string apiName, string hookFieldName, string hookMethodName, string nativeValue, Func<string?> firstCall, Func<string?> secondCall)
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8(nativeValue);

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);
            string? result = firstCall();

            TestAssert.Equal(nativeValue, result, $"{apiName} must convert native UTF-8 strings.");

            nextPointer = IntPtr.Zero;
            result = secondCall();

            TestAssert.Equal<string?>(null, result, $"{apiName} must return null for native null.");
            TestAssert.Equal(2, capturedCallCount, $"{apiName} must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static void AssertJoystickStringMethod(string apiName, string hookFieldName, string nativeValue, IntPtr firstJoystick, IntPtr secondJoystick, Func<IntPtr, string?> call)
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8(nativeValue);

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureJoystickPointer));
            string? result = call(firstJoystick);

            TestAssert.Equal(nativeValue, result, $"{apiName} must convert native UTF-8 strings.");
            TestAssert.Equal(firstJoystick, capturedJoystick, $"{apiName} must forward joystick for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = call(secondJoystick);

            TestAssert.Equal<string?>(null, result, $"{apiName} must return null for native null.");
            TestAssert.Equal(secondJoystick, capturedJoystick, $"{apiName} must forward joystick for null strings.");
            TestAssert.Equal(2, capturedCallCount, $"{apiName} must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static void AssertInstanceIntMethod(string apiName, string nativeName, string hookFieldName, uint instanceId, int nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextInt = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstanceInt));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceId);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceId, capturedInstanceId, $"{apiName} must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertInstanceUShortMethod(string apiName, string nativeName, string hookFieldName, uint instanceId, ushort nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextUShort = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstanceUShort));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceId);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceId, capturedInstanceId, $"{apiName} must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertInstancePointerMethod(string apiName, string nativeName, string hookFieldName, uint instanceId, IntPtr nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextPointer = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstancePointer));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceId);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceId, capturedInstanceId, $"{apiName} must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertJoystickIntMethod(string apiName, string nativeName, string hookFieldName, IntPtr joystick, int nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextInt = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureJoystickInt));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], joystick);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(joystick, capturedJoystick, $"{apiName} must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertJoystickUShortMethod(string apiName, string nativeName, string hookFieldName, IntPtr joystick, ushort nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextUShort = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureJoystickUShort));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], joystick);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(joystick, capturedJoystick, $"{apiName} must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertJoystickBoolMethod(string apiName, string nativeName, string hookFieldName, IntPtr joystick, bool nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureJoystickBool));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], joystick);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(joystick, capturedJoystick, $"{apiName} must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertInstanceBoolMethod(string apiName, string nativeName, string hookFieldName, uint instanceId, bool nativeValue)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstanceBool));
        object? result = InvokePublic(apiName[(apiName.LastIndexOf('.') + 1)..], instanceId);

        TestAssert.Equal(nativeValue, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(instanceId, capturedInstanceId, $"{apiName} must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void CaptureNoArgumentVoid()
    {
        capturedCallCount++;
    }

    private static bool CaptureNoArgumentBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureJoystickGuidInfo(SDL3.SDL.GUID guid, out short vendor, out short product, out short version, out short crc16)
    {
        capturedCallCount++;
        capturedGuid = guid;
        vendor = nextVendor;
        product = nextProduct;
        version = nextVersion;
        crc16 = nextCrc16;
    }

    private static IntPtr CaptureArrayPointer(out int count)
    {
        capturedCallCount++;
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureInstancePointer(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextPointer;
    }

    private static int CaptureInstanceInt(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextInt;
    }

    private static ushort CaptureInstanceUShort(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextUShort;
    }

    private static SDL3.SDL.GUID CaptureInstanceGuid(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextGuid;
    }

    private static SDL3.SDL.JoystickType CaptureInstanceJoystickType(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextJoystickType;
    }

    private static IntPtr CapturePlayerIndexPointer(int playerIndex)
    {
        capturedCallCount++;
        capturedPlayerIndex = playerIndex;
        return nextPointer;
    }

    private static uint CaptureAttachVirtualJoystick(in SDL3.SDL.VirtualJoystickDesc desc)
    {
        capturedCallCount++;
        capturedVirtualJoystickDesc = desc;
        return nextUInt;
    }

    private static bool CaptureInstanceBool(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextBool;
    }

    private static bool CaptureSetJoystickVirtualAxis(IntPtr joystick, int axis, short value)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedAxis = axis;
        capturedAxisValue = value;
        return nextBool;
    }

    private static bool CaptureSetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedBall = ball;
        capturedXRel = xrel;
        capturedYRel = yrel;
        return nextBool;
    }

    private static bool CaptureSetJoystickVirtualButton(IntPtr joystick, int button, bool down)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedButton = button;
        capturedDown = down;
        return nextBool;
    }

    private static bool CaptureSetJoystickVirtualHat(IntPtr joystick, int hat, SDL3.SDL.JoystickHat value)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedHatIndex = hat;
        capturedHat = value;
        return nextBool;
    }

    private static bool CaptureSetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, bool down, float x, float y, float pressure)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedTouchpad = touchpad;
        capturedFinger = finger;
        capturedDown = down;
        capturedX = x;
        capturedY = y;
        capturedPressure = pressure;
        return nextBool;
    }

    private static bool CaptureSendJoystickVirtualSensorData(IntPtr joystick, SDL3.SDL.SensorType type, ulong sensorTimestamp, float[] data, int numValues)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedSensorType = type;
        capturedSensorTimestamp = sensorTimestamp;
        capturedFloatArray = data;
        capturedNumValues = numValues;
        return nextBool;
    }

    private static uint CaptureJoystickUInt(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextUInt;
    }

    private static IntPtr CaptureJoystickPointer(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextPointer;
    }

    private static int CaptureJoystickInt(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextInt;
    }

    private static bool CaptureSetJoystickPlayerIndex(IntPtr joystick, int playerIndex)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedPlayerIndex = playerIndex;
        return nextBool;
    }

    private static SDL3.SDL.GUID CaptureJoystickGuid(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextGuid;
    }

    private static ushort CaptureJoystickUShort(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextUShort;
    }

    private static SDL3.SDL.JoystickType CaptureJoystickType(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextJoystickType;
    }

    private static bool CaptureJoystickBool(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextBool;
    }

    private static short CaptureGetJoystickAxis(IntPtr joystick, int axis)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedAxis = axis;
        return nextShort;
    }

    private static bool CaptureGetJoystickAxisInitialState(IntPtr joystick, int axis, out short state)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedAxis = axis;
        state = nextShort;
        return nextBool;
    }

    private static bool CaptureGetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedBall = ball;
        dx = nextDx;
        dy = nextDy;
        return nextBool;
    }

    private static SDL3.SDL.JoystickHat CaptureGetJoystickHat(IntPtr joystick, int hat)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedHatIndex = hat;
        return nextJoystickHat;
    }

    private static bool CaptureGetJoystickButton(IntPtr joystick, int button)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedButton = button;
        return nextBool;
    }

    private static bool CaptureRumbleJoystick(IntPtr joystick, short lowFrequencyRumble, short highFrequencyRumble, int durationMs)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedLowFrequencyRumble = lowFrequencyRumble;
        capturedHighFrequencyRumble = highFrequencyRumble;
        capturedDurationMs = durationMs;
        return nextBool;
    }

    private static bool CaptureRumbleJoystickTriggers(IntPtr joystick, short leftRumble, short rightRumble, int durationMs)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedLeftRumble = leftRumble;
        capturedRightRumble = rightRumble;
        capturedDurationMs = durationMs;
        return nextBool;
    }

    private static bool CaptureSetJoystickLed(IntPtr joystick, byte red, byte green, byte blue)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedRed = red;
        capturedGreen = green;
        capturedBlue = blue;
        return nextBool;
    }

    private static bool CaptureSendJoystickEffectPointer(IntPtr joystick, IntPtr data, int size)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedEffectData = data;
        capturedSize = size;
        return nextBool;
    }

    private static bool CaptureSendJoystickEffectArray(IntPtr joystick, byte[] data, int size)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        capturedEffectBytes = data;
        capturedSize = size;
        return nextBool;
    }

    private static void CaptureCloseJoystick(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
    }

    private static SDL3.SDL.JoystickConnectionState CaptureJoystickConnectionState(IntPtr joystick)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        return nextJoystickConnectionState;
    }

    private static SDL3.SDL.PowerState CaptureJoystickPowerInfo(IntPtr joystick, out int percent)
    {
        capturedCallCount++;
        capturedJoystick = joystick;
        percent = nextPercent;
        return nextPowerState;
    }

    private static void CaptureSetJoystickEventsEnabled(bool enabled)
    {
        capturedCallCount++;
        capturedEnabled = enabled;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeCallCount++;
        capturedFreeMemory = mem;
    }

    private static void ResetCaptureState()
    {
        capturedInstanceId = 0;
        capturedPlayerIndex = 0;
        capturedJoystick = IntPtr.Zero;
        capturedFreeMemory = IntPtr.Zero;
        capturedVirtualJoystickDesc = default;
        capturedAxis = 0;
        capturedBall = 0;
        capturedButton = 0;
        capturedHatIndex = 0;
        capturedTouchpad = 0;
        capturedFinger = 0;
        capturedSize = 0;
        capturedDown = false;
        capturedEnabled = false;
        capturedAxisValue = 0;
        capturedXRel = 0;
        capturedYRel = 0;
        capturedLowFrequencyRumble = 0;
        capturedHighFrequencyRumble = 0;
        capturedLeftRumble = 0;
        capturedRightRumble = 0;
        capturedDurationMs = 0;
        capturedRed = 0;
        capturedGreen = 0;
        capturedBlue = 0;
        capturedGuid = default;
        capturedHat = SDL3.SDL.JoystickHat.Centered;
        capturedEffectData = IntPtr.Zero;
        capturedEffectBytes = null;
        capturedX = 0;
        capturedY = 0;
        capturedPressure = 0;
        capturedSensorType = SDL3.SDL.SensorType.Invalid;
        capturedSensorTimestamp = 0;
        capturedFloatArray = null;
        capturedNumValues = 0;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextInt = 0;
        nextUInt = 0;
        nextUShort = 0;
        nextShort = 0;
        nextDx = 0;
        nextDy = 0;
        nextVendor = 0;
        nextProduct = 0;
        nextVersion = 0;
        nextCrc16 = 0;
        nextGuid = default;
        nextJoystickType = SDL3.SDL.JoystickType.Unknown;
        nextJoystickHat = SDL3.SDL.JoystickHat.Centered;
        nextJoystickConnectionState = SDL3.SDL.JoystickConnectionState.Unknown;
        nextPowerState = SDL3.SDL.PowerState.Unknown;
        nextPercent = 0;
        nextBool = false;
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

    private static void AssertDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
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
