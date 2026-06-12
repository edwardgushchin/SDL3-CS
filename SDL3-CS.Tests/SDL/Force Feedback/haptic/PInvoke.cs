using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.ForceFeedback.Haptic;

internal static class PInvokeTests
{
    private static int capturedInstanceId;
    private static int capturedEffectId;
    private static int capturedIntArgument;
    private static IntPtr capturedHaptic;
    private static IntPtr capturedJoystick;
    private static uint capturedIterations;
    private static uint capturedLength;
    private static float capturedStrength;
    private static SDL3.SDL.HapticEffect capturedEffect;
    private static IntPtr nextPointer;
    private static bool nextBool;
    private static int nextInt;
    private static uint nextUInt;
    private static int nextCount;
    private static int capturedCallCount;

    public static void RunAll()
    {
        SDL_GetHaptics_UsesExpectedNativeMetadata();
        GetHaptics_ReturnsArrayAndNull();
        SDL_GetHapticNameForID_UsesExpectedNativeMetadata();
        GetHapticNameForID_ReturnsStringAndNull();
        OpenHaptic_ForwardsInstanceIdAndReturnsNativePointer();
        GetHapticFromID_ForwardsInstanceIdAndReturnsNativePointer();
        GetHapticID_ForwardsHapticAndReturnsNativeValue();
        SDL_GetHapticName_UsesExpectedNativeMetadata();
        GetHapticName_ReturnsStringAndNull();
        IsMouseHaptic_ReturnsNativeValue();
        OpenHapticFromMouse_ReturnsNativePointer();
        IsJoystickHaptic_ForwardsJoystickAndReturnsNativeValue();
        OpenHapticFromJoystick_ForwardsJoystickAndReturnsNativePointer();
        CloseHaptic_ForwardsHaptic();
        GetMaxHapticEffects_ForwardsHapticAndReturnsNativeValue();
        GetMaxHapticEffectsPlaying_ForwardsHapticAndReturnsNativeValue();
        GetHapticFeatures_ForwardsHapticAndReturnsNativeValue();
        GetNumHapticAxes_ForwardsHapticAndReturnsNativeValue();
        HapticEffectSupported_ForwardsEffectAndReturnsNativeValue();
        CreateHapticEffect_ForwardsEffectAndReturnsNativeValue();
        UpdateHapticEffect_ForwardsEffectAndReturnsNativeValue();
        RunHapticEffect_ForwardsEffectIterationsAndReturnsNativeValue();
        StopHapticEffect_ForwardsEffectAndReturnsNativeValue();
        DestroyHapticEffect_ForwardsEffect();
        GetHapticEffectStatus_ForwardsEffectAndReturnsNativeValue();
        SetHapticGain_ForwardsGainAndReturnsNativeValue();
        SetHapticAutocenter_ForwardsAutocenterAndReturnsNativeValue();
        PauseHaptic_ForwardsHapticAndReturnsNativeValue();
        ResumeHaptic_ForwardsHapticAndReturnsNativeValue();
        StopHapticEffects_ForwardsHapticAndReturnsNativeValue();
        HapticRumbleSupported_ForwardsHapticAndReturnsNativeValue();
        InitHapticRumble_ForwardsHapticAndReturnsNativeValue();
        PlayHapticRumble_ForwardsStrengthLengthAndReturnsNativeValue();
        StopHapticRumble_ForwardsHapticAndReturnsNativeValue();
    }

    public static void SDL_GetHaptics_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetHaptics");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetHaptics");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetHaptics_ReturnsArrayAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetHapticsNativeFunction", nameof(CaptureGetHaptics));

        nextPointer = AllocateSdlIntArray([101, 202, 303], out nextCount);
        int[]? haptics = SDL3.SDL.GetHaptics(out int count);

        TestAssert.NotNull(haptics, "SDL.GetHaptics must convert native haptic IDs.");
        TestAssert.Equal(3, haptics!.Length, "SDL.GetHaptics must preserve native haptic count.");
        TestAssert.Equal(101, haptics[0], "SDL.GetHaptics must convert first haptic ID.");
        TestAssert.Equal(202, haptics[1], "SDL.GetHaptics must convert second haptic ID.");
        TestAssert.Equal(303, haptics[2], "SDL.GetHaptics must convert third haptic ID.");
        TestAssert.Equal(3, count, "SDL.GetHaptics must forward native count.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        haptics = SDL3.SDL.GetHaptics(out count);

        TestAssert.Equal<int[]?>(null, haptics, "SDL.GetHaptics must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GetHaptics must keep native zero count.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetHaptics must call native hook for both branches.");
    }

    public static void SDL_GetHapticNameForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetHapticNameForID");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetHapticNameForID");
    }

    public static void GetHapticNameForID_ReturnsStringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetHapticNameForIDNativeFunction", nameof(CaptureGetHapticNameForID));
        IntPtr name = Marshal.StringToCoTaskMemUTF8("Wheel");

        try
        {
            nextPointer = name;
            string? result = SDL3.SDL.GetHapticNameForID(42);

            TestAssert.Equal("Wheel", result, "SDL.GetHapticNameForID must convert UTF-8 native names.");
            TestAssert.Equal(42, capturedInstanceId, "SDL.GetHapticNameForID must forward instanceId.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetHapticNameForID(43);

            TestAssert.Equal<string?>(null, result, "SDL.GetHapticNameForID must return null for native null.");
            TestAssert.Equal(43, capturedInstanceId, "SDL.GetHapticNameForID must forward instanceId for null branch.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetHapticNameForID must call native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(name);
        }
    }

    public static void OpenHaptic_ForwardsInstanceIdAndReturnsNativePointer()
    {
        AssertInstancePointerMethod("OpenHaptic", "SDL_OpenHaptic", "OpenHapticNativeFunction", 51, (IntPtr)1001);
    }

    public static void GetHapticFromID_ForwardsInstanceIdAndReturnsNativePointer()
    {
        AssertInstancePointerMethod("GetHapticFromID", "SDL_GetHapticFromID", "GetHapticFromIDNativeFunction", 52, (IntPtr)1002);
    }

    public static void GetHapticID_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticIntMethod("GetHapticID", "SDL_GetHapticID", "GetHapticIDNativeFunction", (IntPtr)1101, 901);
    }

    public static void SDL_GetHapticName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetHapticName");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetHapticName");
    }

    public static void GetHapticName_ReturnsStringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetHapticNameNativeFunction", nameof(CaptureGetHapticName));
        IntPtr name = Marshal.StringToCoTaskMemUTF8("Rumble Pad");

        try
        {
            nextPointer = name;
            string? result = SDL3.SDL.GetHapticName((IntPtr)1201);

            TestAssert.Equal("Rumble Pad", result, "SDL.GetHapticName must convert UTF-8 native names.");
            TestAssert.Equal((IntPtr)1201, capturedHaptic, "SDL.GetHapticName must forward haptic.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetHapticName((IntPtr)1202);

            TestAssert.Equal<string?>(null, result, "SDL.GetHapticName must return null for native null.");
            TestAssert.Equal((IntPtr)1202, capturedHaptic, "SDL.GetHapticName must forward haptic for null branch.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetHapticName must call native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(name);
        }
    }

    public static void IsMouseHaptic_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsMouseHaptic");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsMouseHaptic");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("IsMouseHapticNativeFunction", nameof(CaptureNoArgumentBool));

        bool result = SDL3.SDL.IsMouseHaptic();

        TestAssert.Equal(true, result, "SDL.IsMouseHaptic must return native bool value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IsMouseHaptic must call native hook once.");
    }

    public static void OpenHapticFromMouse_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenHapticFromMouse");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenHapticFromMouse");

        ResetCaptureState();
        nextPointer = (IntPtr)1003;
        using NativeHookScope _ = NativeHookScope.Install("OpenHapticFromMouseNativeFunction", nameof(CaptureNoArgumentPointer));

        IntPtr result = SDL3.SDL.OpenHapticFromMouse();

        TestAssert.Equal((IntPtr)1003, result, "SDL.OpenHapticFromMouse must return native haptic pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenHapticFromMouse must call native hook once.");
    }

    public static void IsJoystickHaptic_ForwardsJoystickAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsJoystickHaptic");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsJoystickHaptic");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("IsJoystickHapticNativeFunction", nameof(CaptureJoystickBool));

        bool result = SDL3.SDL.IsJoystickHaptic((IntPtr)1301);

        TestAssert.Equal(false, result, "SDL.IsJoystickHaptic must return native bool value.");
        TestAssert.Equal((IntPtr)1301, capturedJoystick, "SDL.IsJoystickHaptic must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IsJoystickHaptic must call native hook once.");
    }

    public static void OpenHapticFromJoystick_ForwardsJoystickAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenHapticFromJoystick");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenHapticFromJoystick");

        ResetCaptureState();
        nextPointer = (IntPtr)1004;
        using NativeHookScope _ = NativeHookScope.Install("OpenHapticFromJoystickNativeFunction", nameof(CaptureJoystickPointer));

        IntPtr result = SDL3.SDL.OpenHapticFromJoystick((IntPtr)1302);

        TestAssert.Equal((IntPtr)1004, result, "SDL.OpenHapticFromJoystick must return native haptic pointer.");
        TestAssert.Equal((IntPtr)1302, capturedJoystick, "SDL.OpenHapticFromJoystick must forward joystick.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenHapticFromJoystick must call native hook once.");
    }

    public static void CloseHaptic_ForwardsHaptic()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CloseHaptic");
        AssertSdlLibraryImport(nativeMethod, "SDL_CloseHaptic");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("CloseHapticNativeFunction", nameof(CaptureHapticVoid));

        SDL3.SDL.CloseHaptic((IntPtr)1401);

        TestAssert.Equal((IntPtr)1401, capturedHaptic, "SDL.CloseHaptic must forward haptic.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CloseHaptic must call native hook once.");
    }

    public static void GetMaxHapticEffects_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticIntMethod("GetMaxHapticEffects", "SDL_GetMaxHapticEffects", "GetMaxHapticEffectsNativeFunction", (IntPtr)1501, 12);
    }

    public static void GetMaxHapticEffectsPlaying_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticIntMethod("GetMaxHapticEffectsPlaying", "SDL_GetMaxHapticEffectsPlaying", "GetMaxHapticEffectsPlayingNativeFunction", (IntPtr)1502, 3);
    }

    public static void GetHapticFeatures_ForwardsHapticAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetHapticFeatures");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetHapticFeatures");

        ResetCaptureState();
        nextUInt = 0x22;
        using NativeHookScope _ = NativeHookScope.Install("GetHapticFeaturesNativeFunction", nameof(CaptureHapticUInt));

        uint result = SDL3.SDL.GetHapticFeatures((IntPtr)1503);

        TestAssert.Equal(0x22u, result, "SDL.GetHapticFeatures must return native feature flags.");
        TestAssert.Equal((IntPtr)1503, capturedHaptic, "SDL.GetHapticFeatures must forward haptic.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetHapticFeatures must call native hook once.");
    }

    public static void GetNumHapticAxes_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticIntMethod("GetNumHapticAxes", "SDL_GetNumHapticAxes", "GetNumHapticAxesNativeFunction", (IntPtr)1504, 4);
    }

    public static void HapticEffectSupported_ForwardsEffectAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HapticEffectSupported");
        AssertSdlDllImport(nativeMethod, "SDL_HapticEffectSupported");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "effect");

        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.HapticEffect effect = new() { Type = 321 };
        using NativeHookScope _ = NativeHookScope.Install("HapticEffectSupportedNativeFunction", nameof(CaptureHapticEffectBool));

        bool result = SDL3.SDL.HapticEffectSupported((IntPtr)1601, in effect);

        TestAssert.Equal(true, result, "SDL.HapticEffectSupported must return native bool value.");
        AssertHapticEffect((IntPtr)1601, 321, "SDL.HapticEffectSupported");
    }

    public static void CreateHapticEffect_ForwardsEffectAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateHapticEffect");
        AssertSdlDllImport(nativeMethod, "SDL_CreateHapticEffect");
        AssertByRefParameter(nativeMethod, "effect");

        ResetCaptureState();
        nextInt = 77;
        SDL3.SDL.HapticEffect effect = new() { Type = 322 };
        using NativeHookScope _ = NativeHookScope.Install("CreateHapticEffectNativeFunction", nameof(CaptureHapticEffectInt));

        int result = SDL3.SDL.CreateHapticEffect((IntPtr)1602, in effect);

        TestAssert.Equal(77, result, "SDL.CreateHapticEffect must return native effect ID.");
        AssertHapticEffect((IntPtr)1602, 322, "SDL.CreateHapticEffect");
    }

    public static void UpdateHapticEffect_ForwardsEffectAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UpdateHapticEffect");
        AssertSdlDllImport(nativeMethod, "SDL_UpdateHapticEffect");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "data");

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.HapticEffect data = new() { Type = 323 };
        using NativeHookScope _ = NativeHookScope.Install("UpdateHapticEffectNativeFunction", nameof(CaptureUpdateHapticEffect));

        bool result = SDL3.SDL.UpdateHapticEffect((IntPtr)1603, 78, in data);

        TestAssert.Equal(false, result, "SDL.UpdateHapticEffect must return native bool value.");
        TestAssert.Equal((IntPtr)1603, capturedHaptic, "SDL.UpdateHapticEffect must forward haptic.");
        TestAssert.Equal(78, capturedEffectId, "SDL.UpdateHapticEffect must forward effect ID.");
        TestAssert.Equal((ushort)323, capturedEffect.Type, "SDL.UpdateHapticEffect must forward effect data.");
        TestAssert.Equal(1, capturedCallCount, "SDL.UpdateHapticEffect must call native hook once.");
    }

    public static void RunHapticEffect_ForwardsEffectIterationsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RunHapticEffect");
        AssertSdlLibraryImport(nativeMethod, "SDL_RunHapticEffect");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("RunHapticEffectNativeFunction", nameof(CaptureRunHapticEffect));

        bool result = SDL3.SDL.RunHapticEffect((IntPtr)1701, 79, 5);

        TestAssert.Equal(true, result, "SDL.RunHapticEffect must return native bool value.");
        TestAssert.Equal((IntPtr)1701, capturedHaptic, "SDL.RunHapticEffect must forward haptic.");
        TestAssert.Equal(79, capturedEffectId, "SDL.RunHapticEffect must forward effect ID.");
        TestAssert.Equal(5u, capturedIterations, "SDL.RunHapticEffect must forward iterations.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RunHapticEffect must call native hook once.");
    }

    public static void StopHapticEffect_ForwardsEffectAndReturnsNativeValue()
    {
        AssertHapticEffectIdBoolMethod("StopHapticEffect", "SDL_StopHapticEffect", "StopHapticEffectNativeFunction", (IntPtr)1702, 80, true);
    }

    public static void DestroyHapticEffect_ForwardsEffect()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyHapticEffect");
        AssertSdlLibraryImport(nativeMethod, "SDL_DestroyHapticEffect");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DestroyHapticEffectNativeFunction", nameof(CaptureHapticEffectIdVoid));

        SDL3.SDL.DestroyHapticEffect((IntPtr)1703, 81);

        AssertHapticEffectId((IntPtr)1703, 81, "SDL.DestroyHapticEffect");
    }

    public static void GetHapticEffectStatus_ForwardsEffectAndReturnsNativeValue()
    {
        AssertHapticEffectIdBoolMethod("GetHapticEffectStatus", "SDL_GetHapticEffectStatus", "GetHapticEffectStatusNativeFunction", (IntPtr)1704, 82, false);
    }

    public static void SetHapticGain_ForwardsGainAndReturnsNativeValue()
    {
        AssertHapticIntArgumentBoolMethod("SetHapticGain", "SDL_SetHapticGain", "SetHapticGainNativeFunction", (IntPtr)1801, 60, true);
    }

    public static void SetHapticAutocenter_ForwardsAutocenterAndReturnsNativeValue()
    {
        AssertHapticIntArgumentBoolMethod("SetHapticAutocenter", "SDL_SetHapticAutocenter", "SetHapticAutocenterNativeFunction", (IntPtr)1802, 70, false);
    }

    public static void PauseHaptic_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticBoolMethod("PauseHaptic", "SDL_PauseHaptic", "PauseHapticNativeFunction", (IntPtr)1901, true);
    }

    public static void ResumeHaptic_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticBoolMethod("ResumeHaptic", "SDL_ResumeHaptic", "ResumeHapticNativeFunction", (IntPtr)1902, false);
    }

    public static void StopHapticEffects_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticBoolMethod("StopHapticEffects", "SDL_StopHapticEffects", "StopHapticEffectsNativeFunction", (IntPtr)1903, true);
    }

    public static void HapticRumbleSupported_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticBoolMethod("HapticRumbleSupported", "SDL_HapticRumbleSupported", "HapticRumbleSupportedNativeFunction", (IntPtr)1904, false);
    }

    public static void InitHapticRumble_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticBoolMethod("InitHapticRumble", "SDL_InitHapticRumble", "InitHapticRumbleNativeFunction", (IntPtr)1905, true);
    }

    public static void PlayHapticRumble_ForwardsStrengthLengthAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PlayHapticRumble");
        AssertSdlLibraryImport(nativeMethod, "SDL_PlayHapticRumble");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("PlayHapticRumbleNativeFunction", nameof(CapturePlayHapticRumble));

        bool result = SDL3.SDL.PlayHapticRumble((IntPtr)1906, 0.75f, 250);

        TestAssert.Equal(true, result, "SDL.PlayHapticRumble must return native bool value.");
        TestAssert.Equal((IntPtr)1906, capturedHaptic, "SDL.PlayHapticRumble must forward haptic.");
        TestAssert.Equal(0.75f, capturedStrength, "SDL.PlayHapticRumble must forward strength.");
        TestAssert.Equal(250u, capturedLength, "SDL.PlayHapticRumble must forward length.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PlayHapticRumble must call native hook once.");
    }

    public static void StopHapticRumble_ForwardsHapticAndReturnsNativeValue()
    {
        AssertHapticBoolMethod("StopHapticRumble", "SDL_StopHapticRumble", "StopHapticRumbleNativeFunction", (IntPtr)1907, false);
    }

    private static void ResetCaptureState()
    {
        capturedInstanceId = 0;
        capturedEffectId = 0;
        capturedIntArgument = 0;
        capturedHaptic = IntPtr.Zero;
        capturedJoystick = IntPtr.Zero;
        capturedIterations = 0;
        capturedLength = 0;
        capturedStrength = 0;
        capturedEffect = default;
        nextPointer = IntPtr.Zero;
        nextBool = false;
        nextInt = 0;
        nextUInt = 0;
        nextCount = 0;
        capturedCallCount = 0;
    }

    private static IntPtr CaptureGetHaptics(out int count)
    {
        count = nextCount;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGetHapticNameForID(int instanceId)
    {
        capturedInstanceId = instanceId;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureInstancePointer(int instanceId)
    {
        capturedInstanceId = instanceId;
        capturedCallCount++;
        return nextPointer;
    }

    private static int CaptureHapticInt(IntPtr haptic)
    {
        capturedHaptic = haptic;
        capturedCallCount++;
        return nextInt;
    }

    private static IntPtr CaptureGetHapticName(IntPtr haptic)
    {
        capturedHaptic = haptic;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureNoArgumentBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureNoArgumentPointer()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureJoystickBool(IntPtr joystick)
    {
        capturedJoystick = joystick;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureJoystickPointer(IntPtr joystick)
    {
        capturedJoystick = joystick;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureHapticVoid(IntPtr haptic)
    {
        capturedHaptic = haptic;
        capturedCallCount++;
    }

    private static uint CaptureHapticUInt(IntPtr haptic)
    {
        capturedHaptic = haptic;
        capturedCallCount++;
        return nextUInt;
    }

    private static bool CaptureHapticEffectBool(IntPtr haptic, in SDL3.SDL.HapticEffect effect)
    {
        capturedHaptic = haptic;
        capturedEffect = effect;
        capturedCallCount++;
        return nextBool;
    }

    private static int CaptureHapticEffectInt(IntPtr haptic, in SDL3.SDL.HapticEffect effect)
    {
        capturedHaptic = haptic;
        capturedEffect = effect;
        capturedCallCount++;
        return nextInt;
    }

    private static bool CaptureUpdateHapticEffect(IntPtr haptic, int effect, in SDL3.SDL.HapticEffect data)
    {
        capturedHaptic = haptic;
        capturedEffectId = effect;
        capturedEffect = data;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureRunHapticEffect(IntPtr haptic, int effect, uint iterations)
    {
        capturedHaptic = haptic;
        capturedEffectId = effect;
        capturedIterations = iterations;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureHapticEffectIdBool(IntPtr haptic, int effect)
    {
        capturedHaptic = haptic;
        capturedEffectId = effect;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureHapticEffectIdVoid(IntPtr haptic, int effect)
    {
        capturedHaptic = haptic;
        capturedEffectId = effect;
        capturedCallCount++;
    }

    private static bool CaptureHapticIntArgumentBool(IntPtr haptic, int value)
    {
        capturedHaptic = haptic;
        capturedIntArgument = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureHapticBool(IntPtr haptic)
    {
        capturedHaptic = haptic;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CapturePlayHapticRumble(IntPtr haptic, float strength, uint length)
    {
        capturedHaptic = haptic;
        capturedStrength = strength;
        capturedLength = length;
        capturedCallCount++;
        return nextBool;
    }

    private static void AssertInstancePointerMethod(string publicName, string nativeName, string hookFieldName, int instanceId, IntPtr expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextPointer = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureInstancePointer));

        IntPtr result = (IntPtr)InvokePublic(publicName, instanceId)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native haptic pointer.");
        TestAssert.Equal(instanceId, capturedInstanceId, $"SDL.{publicName} must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertHapticIntMethod(string publicName, string nativeName, string hookFieldName, IntPtr haptic, int expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextInt = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureHapticInt));

        int result = (int)InvokePublic(publicName, haptic)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native int value.");
        TestAssert.Equal(haptic, capturedHaptic, $"SDL.{publicName} must forward haptic.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertHapticEffectIdBoolMethod(string publicName, string nativeName, string hookFieldName, IntPtr haptic, int effect, bool expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureHapticEffectIdBool));

        bool result = (bool)InvokePublic(publicName, haptic, effect)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        AssertHapticEffectId(haptic, effect, $"SDL.{publicName}");
    }

    private static void AssertHapticIntArgumentBoolMethod(string publicName, string nativeName, string hookFieldName, IntPtr haptic, int argument, bool expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureHapticIntArgumentBool));

        bool result = (bool)InvokePublic(publicName, haptic, argument)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        TestAssert.Equal(haptic, capturedHaptic, $"SDL.{publicName} must forward haptic.");
        TestAssert.Equal(argument, capturedIntArgument, $"SDL.{publicName} must forward numeric argument.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertHapticBoolMethod(string publicName, string nativeName, string hookFieldName, IntPtr haptic, bool expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureHapticBool));

        bool result = (bool)InvokePublic(publicName, haptic)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        TestAssert.Equal(haptic, capturedHaptic, $"SDL.{publicName} must forward haptic.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertHapticEffect(IntPtr expectedHaptic, ushort expectedEffectType, string apiName)
    {
        TestAssert.Equal(expectedHaptic, capturedHaptic, $"{apiName} must forward haptic.");
        TestAssert.Equal(expectedEffectType, capturedEffect.Type, $"{apiName} must forward effect.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static void AssertHapticEffectId(IntPtr expectedHaptic, int expectedEffect, string apiName)
    {
        TestAssert.Equal(expectedHaptic, capturedHaptic, $"{apiName} must forward haptic.");
        TestAssert.Equal(expectedEffect, capturedEffectId, $"{apiName} must forward effect ID.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static IntPtr AllocateSdlIntArray(int[] values, out int count)
    {
        count = values.Length;
        IntPtr block = SDL3.SDL.Malloc((UIntPtr)(values.Length * Marshal.SizeOf<int>()));
        TestAssert.True(block != IntPtr.Zero, "SDL.Malloc must allocate haptic ID array test memory.");

        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(block, i * Marshal.SizeOf<int>(), values[i]);
        }

        return block;
    }

    private static object? InvokePublic(string methodName, params object[] arguments)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} public wrapper must exist.");
        return method!.Invoke(null, arguments);
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertSdlDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertCdecl(MethodInfo method, string apiName)
    {
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"{apiName} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"{apiName} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"{apiName} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method, UnmanagedType unmanagedType)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} return value must use expected bool marshalling.");
    }

    private static void AssertOutParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.IsOut, $"SDL.{method.Name} parameter {parameterName} must stay out.");
        TestAssert.True(parameter.ParameterType.IsByRef, $"SDL.{method.Name} parameter {parameterName} must stay by reference.");
    }

    private static void AssertByRefParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"SDL.{method.Name} parameter {parameterName} must stay by reference.");
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
