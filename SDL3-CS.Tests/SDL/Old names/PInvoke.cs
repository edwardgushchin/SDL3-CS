using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.OldNames;

internal static class PInvokeTests
{
    private static IntPtr capturedStream;
    private static IntPtr capturedBuffer;
    private static IntPtr capturedFreeMemory;
    private static IntPtr capturedSourcePointer;
    private static IntPtr capturedDstBufferPointer;
    private static IntPtr capturedSrcBufferPointer;
    private static IntPtr capturedUserdata;
    private static IntPtr capturedGamepad;
    private static SDL3.SDL.AudioSpec capturedSrcSpec;
    private static SDL3.SDL.AudioSpec capturedDstSpec;
    private static SDL3.SDL.AudioFormat capturedFormat;
    private static SDL3.SDL.EventFilter? capturedEventFilter;
    private static SDL3.SDL.GamepadAxis capturedGamepadAxis;
    private static SDL3.SDL.GamepadButton capturedGamepadButton;
    private static string? capturedMapping;
    private static string? capturedFile;
    private static string? capturedString;
    private static uint capturedBufferLength;
    private static uint capturedInstanceID;
    private static float capturedVolume;
    private static int capturedLen;
    private static int capturedCallCount;
    private static int capturedPlayerIndex;
    private static bool capturedCloseIO;
    private static byte[]? capturedByteBuffer;
    private static int nextInt;
    private static short nextShort;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static SDL3.SDL.AudioSpec nextSpec;
    private static SDL3.SDL.GamepadAxis nextGamepadAxis;
    private static uint nextAudioLen;

    public static void RunAll()
    {
        AtomicAdd_ForwardsToAddAtomicInt();
        AtomicCAS_ForwardsToCompareAndSwapAtomicInt();
        AtomicCASPtr_ForwardsToCompareAndSwapAtomicPointer();
        AtomicGetPtr_ForwardsToGetAtomicPointer();
        AtomicLockAndUnlock_ForwardsToSpinlockFunctions();
        AtomicSet_ForwardsToSetAtomicInt();
        AtomicSetPtr_ForwardsToSetAtomicPointer();
        AtomicTryLock_ForwardsToTryLockSpinlock();
        AudioStreamAvailable_ForwardsToGetAudioStreamAvailable();
        AudioStreamClear_ForwardsToClearAudioStream();
        AudioStreamFlush_ForwardsToFlushAudioStream();
        AudioStreamGet_ForwardsToGetAudioStreamData();
        AudioStreamPut_ForwardsToPutAudioStreamData();
        FreeAudioStream_ForwardsToDestroyAudioStream();
        FreeWAV_ForwardsToFree();
        LoadWAVRW_ForwardsToLoadWAVIO();
        MixAudioFormat_ForwardsToMixAudio();
        NewAudioStream_ForwardsToCreateAudioStream();
        GetCPUCount_ForwardsToGetNumLogicalCPUCores();
        SIMDGetAlignment_ForwardsToGetSIMDAlignment();
        DelEventWatch_ForwardsFilterAndUserdata();
        GameControllerAddMapping_ForwardsMappingAndReturnsNativeValue();
        GameControllerAddMappingsFromFile_ForwardsFileAndReturnsNativeValue();
        GameControllerAddMappingsFromRW_ForwardsSourceCloseFlagAndReturnsNativeValue();
        GameControllerClose_ForwardsGamepad();
        GameControllerFromInstanceID_ForwardsInstanceIdAndReturnsNativePointer();
        GameControllerFromPlayerIndex_ForwardsPlayerIndexAndReturnsNativePointer();
        GameControllerGetAppleSFSymbolsNameForAxis_ReturnsStringAndNull();
        GameControllerGetAppleSFSymbolsNameForButton_ReturnsStringAndNull();
        GameControllerGetAttached_ForwardsGamepadAndReturnsNativeValue();
        GameControllerGetAxis_ForwardsGamepadAxisAndReturnsNativeValue();
        GameControllerGetAxisFromString_ForwardsStringAndReturnsNativeValue();
    }

    public static void AtomicAdd_ForwardsToAddAtomicInt()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 10 };

        int previous = SDL3.SDL.AtomicAdd(ref value, 5);

        TestAssert.Equal(10, previous, "SDL.AtomicAdd must return the previous atomic value.");
        TestAssert.Equal(15, value.Value, "SDL.AtomicAdd must add through AddAtomicInt.");
    }

    public static void AtomicCAS_ForwardsToCompareAndSwapAtomicInt()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 20 };

        bool swapped = SDL3.SDL.AtomicCAS(ref value, 20, 30);

        TestAssert.Equal(true, swapped, "SDL.AtomicCAS must return the compare-and-swap result.");
        TestAssert.Equal(30, value.Value, "SDL.AtomicCAS must update through CompareAndSwapAtomicInt.");
    }

    public static void AtomicCASPtr_ForwardsToCompareAndSwapAtomicPointer()
    {
        IntPtr value = (IntPtr)0x1001;

        bool swapped = SDL3.SDL.AtomicCASPtr(ref value, (IntPtr)0x1001, (IntPtr)0x1002);

        TestAssert.Equal(true, swapped, "SDL.AtomicCASPtr must return the compare-and-swap result.");
        TestAssert.Equal((IntPtr)0x1002, value, "SDL.AtomicCASPtr must update through CompareAndSwapAtomicPointer.");
    }

    public static void AtomicGetPtr_ForwardsToGetAtomicPointer()
    {
        IntPtr value = (IntPtr)0x2001;

        IntPtr result = SDL3.SDL.AtomicGetPtr(ref value);

        TestAssert.Equal((IntPtr)0x2001, result, "SDL.AtomicGetPtr must return the current atomic pointer.");
        TestAssert.Equal((IntPtr)0x2001, value, "SDL.AtomicGetPtr must not change the pointer.");
    }

    public static void AtomicLockAndUnlock_ForwardsToSpinlockFunctions()
    {
        int spinlock = 0;

        SDL3.SDL.AtomicLock(ref spinlock);
        TestAssert.True(spinlock != 0, "SDL.AtomicLock must acquire the spinlock.");

        SDL3.SDL.AtomicUnlock(ref spinlock);
        TestAssert.Equal(0, spinlock, "SDL.AtomicUnlock must release the spinlock.");
    }

    public static void AtomicSet_ForwardsToSetAtomicInt()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 40 };

        int previous = SDL3.SDL.AtomicSet(ref value, 50);

        TestAssert.Equal(40, previous, "SDL.AtomicSet must return the previous atomic value.");
        TestAssert.Equal(50, value.Value, "SDL.AtomicSet must set through SetAtomicInt.");
    }

    public static void AtomicSetPtr_ForwardsToSetAtomicPointer()
    {
        IntPtr value = (IntPtr)0x3001;

        IntPtr previous = SDL3.SDL.AtomicSetPtr(ref value, (IntPtr)0x3002);

        TestAssert.Equal((IntPtr)0x3001, previous, "SDL.AtomicSetPtr must return the previous pointer.");
        TestAssert.Equal((IntPtr)0x3002, value, "SDL.AtomicSetPtr must set through SetAtomicPointer.");
    }

    public static void AtomicTryLock_ForwardsToTryLockSpinlock()
    {
        int spinlock = 0;

        bool locked = SDL3.SDL.AtomicTryLock(ref spinlock);

        try
        {
            TestAssert.Equal(true, locked, "SDL.AtomicTryLock must acquire an unlocked spinlock.");
            TestAssert.True(spinlock != 0, "SDL.AtomicTryLock must update the spinlock state.");
        }
        finally
        {
            if (locked)
            {
                SDL3.SDL.AtomicUnlock(ref spinlock);
            }
        }
    }

    public static void AudioStreamAvailable_ForwardsToGetAudioStreamAvailable()
    {
        ResetCaptureState();
        nextInt = 64;

        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamAvailableNativeFunction", nameof(CaptureAudioStreamInt));
        int result = SDL3.SDL.AudioStreamAvailable((IntPtr)0x4001);

        TestAssert.Equal(64, result, "SDL.AudioStreamAvailable must return the GetAudioStreamAvailable value.");
        TestAssert.Equal((IntPtr)0x4001, capturedStream, "SDL.AudioStreamAvailable must forward stream.");
    }

    public static void AudioStreamClear_ForwardsToClearAudioStream()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("ClearAudioStreamNativeFunction", nameof(CaptureAudioStreamBool));
        bool result = SDL3.SDL.AudioStreamClear((IntPtr)0x4002);

        TestAssert.Equal(true, result, "SDL.AudioStreamClear must return the ClearAudioStream value.");
        TestAssert.Equal((IntPtr)0x4002, capturedStream, "SDL.AudioStreamClear must forward stream.");
    }

    public static void AudioStreamFlush_ForwardsToFlushAudioStream()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("FlushAudioStreamNativeFunction", nameof(CaptureAudioStreamBool));
        bool result = SDL3.SDL.AudioStreamFlush((IntPtr)0x4003);

        TestAssert.Equal(true, result, "SDL.AudioStreamFlush must return the FlushAudioStream value.");
        TestAssert.Equal((IntPtr)0x4003, capturedStream, "SDL.AudioStreamFlush must forward stream.");
    }

    public static void AudioStreamGet_ForwardsToGetAudioStreamData()
    {
        ResetCaptureState();
        nextInt = 3;
        byte[] buffer = [1, 2, 3];

        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamDataWithBytesNativeFunction", nameof(CaptureAudioStreamDataWithBytes));
        int result = SDL3.SDL.AudioStreamGet((IntPtr)0x4004, buffer, buffer.Length);

        TestAssert.Equal(3, result, "SDL.AudioStreamGet must return the GetAudioStreamData value.");
        TestAssert.Equal((IntPtr)0x4004, capturedStream, "SDL.AudioStreamGet must forward stream.");
        TestAssert.True(ReferenceEquals(buffer, capturedByteBuffer), "SDL.AudioStreamGet must forward the buffer.");
        TestAssert.Equal(buffer.Length, capturedLen, "SDL.AudioStreamGet must forward len.");
    }

    public static void AudioStreamPut_ForwardsToPutAudioStreamData()
    {
        ResetCaptureState();
        nextBool = true;
        byte[] buffer = [4, 5, 6, 7];

        using NativeHookScope _ = NativeHookScope.Install("PutAudioStreamDataWithBytesNativeFunction", nameof(CapturePutAudioStreamDataWithBytes));
        bool result = SDL3.SDL.AudioStreamPut((IntPtr)0x4005, buffer, buffer.Length);

        TestAssert.Equal(true, result, "SDL.AudioStreamPut must return the PutAudioStreamData value.");
        TestAssert.Equal((IntPtr)0x4005, capturedStream, "SDL.AudioStreamPut must forward stream.");
        TestAssert.True(ReferenceEquals(buffer, capturedByteBuffer), "SDL.AudioStreamPut must forward the buffer.");
        TestAssert.Equal(buffer.Length, capturedLen, "SDL.AudioStreamPut must forward len.");
    }

    public static void FreeAudioStream_ForwardsToDestroyAudioStream()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("DestroyAudioStreamNativeFunction", nameof(CaptureAudioStreamVoid));
        SDL3.SDL.FreeAudioStream((IntPtr)0x4006);

        TestAssert.Equal((IntPtr)0x4006, capturedStream, "SDL.FreeAudioStream must forward stream.");
        TestAssert.Equal(1, capturedCallCount, "SDL.FreeAudioStream must call DestroyAudioStream once.");
    }

    public static void FreeWAV_ForwardsToFree()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        SDL3.SDL.FreeWAV((IntPtr)0x4007);

        TestAssert.Equal((IntPtr)0x4007, capturedFreeMemory, "SDL.FreeWAV must forward memory to Free.");
        TestAssert.Equal(1, capturedCallCount, "SDL.FreeWAV must call Free once.");
    }

    public static void LoadWAVRW_ForwardsToLoadWAVIO()
    {
        ResetCaptureState();
        nextBool = true;
        nextSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS16LE, 2, 44100);
        nextPointer = (IntPtr)0x4008;
        nextAudioLen = 512;

        using NativeHookScope _ = NativeHookScope.Install("LoadWAVIONativeFunction", nameof(CaptureLoadWAVIO));
        bool result = SDL3.SDL.LoadWAVRW((IntPtr)0x4009, true, out SDL3.SDL.AudioSpec spec, out IntPtr audioBuf, out uint audioLen);

        TestAssert.Equal(true, result, "SDL.LoadWAVRW must return the LoadWAVIO value.");
        TestAssert.Equal((IntPtr)0x4009, capturedSourcePointer, "SDL.LoadWAVRW must forward source pointer.");
        TestAssert.Equal(true, capturedCloseIO, "SDL.LoadWAVRW must forward closeio.");
        AssertAudioSpec(nextSpec, spec, "SDL.LoadWAVRW must forward output spec.");
        TestAssert.Equal((IntPtr)0x4008, audioBuf, "SDL.LoadWAVRW must forward output buffer.");
        TestAssert.Equal(512u, audioLen, "SDL.LoadWAVRW must forward output length.");
    }

    public static void MixAudioFormat_ForwardsToMixAudio()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("MixAudioNativeFunction", nameof(CaptureMixAudio));
        bool result = SDL3.SDL.MixAudioFormat((IntPtr)0x4010, (IntPtr)0x4011, SDL3.SDL.AudioFormat.AudioS16LE, 128, 0.5f);

        TestAssert.Equal(true, result, "SDL.MixAudioFormat must return the MixAudio value.");
        TestAssert.Equal((IntPtr)0x4010, capturedDstBufferPointer, "SDL.MixAudioFormat must forward dst.");
        TestAssert.Equal((IntPtr)0x4011, capturedSrcBufferPointer, "SDL.MixAudioFormat must forward src.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioS16LE, capturedFormat, "SDL.MixAudioFormat must forward format.");
        TestAssert.Equal(128u, capturedBufferLength, "SDL.MixAudioFormat must forward len.");
        TestAssert.Equal(0.5f, capturedVolume, "SDL.MixAudioFormat must forward volume.");
    }

    public static void NewAudioStream_ForwardsToCreateAudioStream()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x4012;
        SDL3.SDL.AudioSpec srcSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS16LE, 1, 22050);
        SDL3.SDL.AudioSpec dstSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioF32LE, 2, 48000);

        using NativeHookScope _ = NativeHookScope.Install("CreateAudioStreamWithSpecsNativeFunction", nameof(CaptureCreateAudioStreamWithSpecs));
        IntPtr result = SDL3.SDL.NewAudioStream(in srcSpec, in dstSpec);

        TestAssert.Equal((IntPtr)0x4012, result, "SDL.NewAudioStream must return the CreateAudioStream value.");
        AssertAudioSpec(srcSpec, capturedSrcSpec, "SDL.NewAudioStream must forward srcSpec.");
        AssertAudioSpec(dstSpec, capturedDstSpec, "SDL.NewAudioStream must forward dstSpec.");
    }

    public static void GetCPUCount_ForwardsToGetNumLogicalCPUCores()
    {
        int result = SDL3.SDL.GetCPUCount();

        TestAssert.True(result > 0, "SDL.GetCPUCount must return the logical CPU core count.");
    }

    public static void SIMDGetAlignment_ForwardsToGetSIMDAlignment()
    {
        UIntPtr result = SDL3.SDL.SIMDGetAlignment();

        TestAssert.True(result != UIntPtr.Zero, "SDL.SIMDGetAlignment must return the SIMD alignment.");
    }

    public static void DelEventWatch_ForwardsFilterAndUserdata()
    {
        ResetCaptureState();
        SDL3.SDL.EventFilter filter = static (IntPtr userdata, ref SDL3.SDL.Event @event) => true;

        using NativeHookScope _ = NativeHookScope.Install("RemoveEventWatchNativeFunction", nameof(CaptureRemoveEventWatch));
        SDL3.SDL.DelEventWatch(filter, (IntPtr)0x5001);

        TestAssert.True(ReferenceEquals(filter, capturedEventFilter), "SDL.DelEventWatch must forward filter.");
        TestAssert.Equal((IntPtr)0x5001, capturedUserdata, "SDL.DelEventWatch must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DelEventWatch must call RemoveEventWatch once.");
    }

    public static void GameControllerAddMapping_ForwardsMappingAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 2;

        using NativeHookScope _ = NativeHookScope.Install("AddGamepadMappingNativeFunction", nameof(CaptureAddGamepadMapping));
        int result = SDL3.SDL.GameControllerAddMapping("alias-mapping");

        TestAssert.Equal(2, result, "SDL.GameControllerAddMapping must return the AddGamepadMapping value.");
        TestAssert.Equal("alias-mapping", capturedMapping, "SDL.GameControllerAddMapping must forward mapping.");
    }

    public static void GameControllerAddMappingsFromFile_ForwardsFileAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 3;

        using NativeHookScope _ = NativeHookScope.Install("AddGamepadMappingsFromFileNativeFunction", nameof(CaptureAddGamepadMappingsFromFile));
        int result = SDL3.SDL.GameControllerAddMappingsFromFile("gamecontrollerdb.txt");

        TestAssert.Equal(3, result, "SDL.GameControllerAddMappingsFromFile must return the AddGamepadMappingsFromFile value.");
        TestAssert.Equal("gamecontrollerdb.txt", capturedFile, "SDL.GameControllerAddMappingsFromFile must forward file.");
    }

    public static void GameControllerAddMappingsFromRW_ForwardsSourceCloseFlagAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 4;

        using NativeHookScope _ = NativeHookScope.Install("AddGamepadMappingsFromIONativeFunction", nameof(CaptureAddGamepadMappingsFromIO));
        int result = SDL3.SDL.GameControllerAddMappingsFromRW((IntPtr)0x5002, true);

        TestAssert.Equal(4, result, "SDL.GameControllerAddMappingsFromRW must return the AddGamepadMappingsFromIO value.");
        TestAssert.Equal((IntPtr)0x5002, capturedSourcePointer, "SDL.GameControllerAddMappingsFromRW must forward src.");
        TestAssert.Equal(true, capturedCloseIO, "SDL.GameControllerAddMappingsFromRW must forward closeio.");
    }

    public static void GameControllerClose_ForwardsGamepad()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("CloseGamepadNativeFunction", nameof(CaptureCloseGamepad));
        SDL3.SDL.GameControllerClose((IntPtr)0x5003);

        TestAssert.Equal((IntPtr)0x5003, capturedGamepad, "SDL.GameControllerClose must forward gamepad.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GameControllerClose must call CloseGamepad once.");
    }

    public static void GameControllerFromInstanceID_ForwardsInstanceIdAndReturnsNativePointer()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x5004;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadFromIDNativeFunction", nameof(CaptureGetGamepadFromID));
        IntPtr result = SDL3.SDL.GameControllerFromInstanceID(77);

        TestAssert.Equal((IntPtr)0x5004, result, "SDL.GameControllerFromInstanceID must return the GetGamepadFromID value.");
        TestAssert.Equal(77u, capturedInstanceID, "SDL.GameControllerFromInstanceID must forward instanceID.");
    }

    public static void GameControllerFromPlayerIndex_ForwardsPlayerIndexAndReturnsNativePointer()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x5005;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadFromPlayerIndexNativeFunction", nameof(CaptureGetGamepadFromPlayerIndex));
        IntPtr result = SDL3.SDL.GameControllerFromPlayerIndex(2);

        TestAssert.Equal((IntPtr)0x5005, result, "SDL.GameControllerFromPlayerIndex must return the GetGamepadFromPlayerIndex value.");
        TestAssert.Equal(2, capturedPlayerIndex, "SDL.GameControllerFromPlayerIndex must forward playerIndex.");
    }

    public static void GameControllerGetAppleSFSymbolsNameForAxis_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr symbol = Marshal.StringToCoTaskMemUTF8("axis.symbol");

        try
        {
            nextPointer = symbol;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadAppleSFSymbolsNameForAxisNativeFunction", nameof(CaptureGetGamepadAppleSFSymbolsNameForAxis));
            string? result = SDL3.SDL.GameControllerGetAppleSFSymbolsNameForAxis((IntPtr)0x5006, SDL3.SDL.GamepadAxis.LeftX);

            TestAssert.Equal("axis.symbol", result, "SDL.GameControllerGetAppleSFSymbolsNameForAxis must return the converted string.");
            TestAssert.Equal((IntPtr)0x5006, capturedGamepad, "SDL.GameControllerGetAppleSFSymbolsNameForAxis must forward gamepad.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.LeftX, capturedGamepadAxis, "SDL.GameControllerGetAppleSFSymbolsNameForAxis must forward axis.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GameControllerGetAppleSFSymbolsNameForAxis((IntPtr)0x5007, SDL3.SDL.GamepadAxis.RightY);

            TestAssert.Equal<string?>(null, result, "SDL.GameControllerGetAppleSFSymbolsNameForAxis must return null for a null native pointer.");
            TestAssert.Equal((IntPtr)0x5007, capturedGamepad, "SDL.GameControllerGetAppleSFSymbolsNameForAxis must forward gamepad for null results.");
            TestAssert.Equal(SDL3.SDL.GamepadAxis.RightY, capturedGamepadAxis, "SDL.GameControllerGetAppleSFSymbolsNameForAxis must forward axis for null results.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(symbol);
        }
    }

    public static void GameControllerGetAppleSFSymbolsNameForButton_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr symbol = Marshal.StringToCoTaskMemUTF8("button.symbol");

        try
        {
            nextPointer = symbol;

            using NativeHookScope _ = NativeHookScope.Install("GetGamepadAppleSFSymbolsNameForButtonNativeFunction", nameof(CaptureGetGamepadAppleSFSymbolsNameForButton));
            string? result = SDL3.SDL.GameControllerGetAppleSFSymbolsNameForButton((IntPtr)0x5008, SDL3.SDL.GamepadButton.South);

            TestAssert.Equal("button.symbol", result, "SDL.GameControllerGetAppleSFSymbolsNameForButton must return the converted string.");
            TestAssert.Equal((IntPtr)0x5008, capturedGamepad, "SDL.GameControllerGetAppleSFSymbolsNameForButton must forward gamepad.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.South, capturedGamepadButton, "SDL.GameControllerGetAppleSFSymbolsNameForButton must forward button.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GameControllerGetAppleSFSymbolsNameForButton((IntPtr)0x5009, SDL3.SDL.GamepadButton.East);

            TestAssert.Equal<string?>(null, result, "SDL.GameControllerGetAppleSFSymbolsNameForButton must return null for a null native pointer.");
            TestAssert.Equal((IntPtr)0x5009, capturedGamepad, "SDL.GameControllerGetAppleSFSymbolsNameForButton must forward gamepad for null results.");
            TestAssert.Equal(SDL3.SDL.GamepadButton.East, capturedGamepadButton, "SDL.GameControllerGetAppleSFSymbolsNameForButton must forward button for null results.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(symbol);
        }
    }

    public static void GameControllerGetAttached_ForwardsGamepadAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GamepadConnectedNativeFunction", nameof(CaptureGamepadConnected));
        bool result = SDL3.SDL.GameControllerGetAttached((IntPtr)0x5010);

        TestAssert.Equal(true, result, "SDL.GameControllerGetAttached must return the GamepadConnected value.");
        TestAssert.Equal((IntPtr)0x5010, capturedGamepad, "SDL.GameControllerGetAttached must forward gamepad.");
    }

    public static void GameControllerGetAxis_ForwardsGamepadAxisAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextShort = -1234;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadAxisNativeFunction", nameof(CaptureGetGamepadAxis));
        short result = SDL3.SDL.GameControllerGetAxis((IntPtr)0x5011, SDL3.SDL.GamepadAxis.RightTrigger);

        TestAssert.Equal((short)-1234, result, "SDL.GameControllerGetAxis must return the GetGamepadAxis value.");
        TestAssert.Equal((IntPtr)0x5011, capturedGamepad, "SDL.GameControllerGetAxis must forward gamepad.");
        TestAssert.Equal(SDL3.SDL.GamepadAxis.RightTrigger, capturedGamepadAxis, "SDL.GameControllerGetAxis must forward axis.");
    }

    public static void GameControllerGetAxisFromString_ForwardsStringAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextGamepadAxis = SDL3.SDL.GamepadAxis.LeftTrigger;

        using NativeHookScope _ = NativeHookScope.Install("GetGamepadAxisFromStringNativeFunction", nameof(CaptureGetGamepadAxisFromString));
        SDL3.SDL.GamepadAxis result = SDL3.SDL.GameControllerGetAxisFromString("lefttrigger");

        TestAssert.Equal(SDL3.SDL.GamepadAxis.LeftTrigger, result, "SDL.GameControllerGetAxisFromString must return the GetGamepadAxisFromString value.");
        TestAssert.Equal("lefttrigger", capturedString, "SDL.GameControllerGetAxisFromString must forward str.");
    }

    private static int CaptureAudioStreamInt(IntPtr stream)
    {
        capturedStream = stream;
        return nextInt;
    }

    private static bool CaptureAudioStreamBool(IntPtr stream)
    {
        capturedStream = stream;
        return nextBool;
    }

    private static int CaptureAudioStreamDataWithBytes(IntPtr stream, byte[] buf, int len)
    {
        capturedStream = stream;
        capturedByteBuffer = buf;
        capturedLen = len;
        return nextInt;
    }

    private static bool CapturePutAudioStreamDataWithBytes(IntPtr stream, byte[] buf, int len)
    {
        capturedStream = stream;
        capturedByteBuffer = buf;
        capturedLen = len;
        return nextBool;
    }

    private static void CaptureAudioStreamVoid(IntPtr stream)
    {
        capturedCallCount++;
        capturedStream = stream;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedCallCount++;
        capturedFreeMemory = mem;
    }

    private static bool CaptureLoadWAVIO(IntPtr src, bool closeio, out SDL3.SDL.AudioSpec spec, out IntPtr audioBuf, out uint audioLen)
    {
        capturedSourcePointer = src;
        capturedCloseIO = closeio;
        spec = nextSpec;
        audioBuf = nextPointer;
        audioLen = nextAudioLen;
        return nextBool;
    }

    private static bool CaptureMixAudio(IntPtr dst, IntPtr src, SDL3.SDL.AudioFormat format, uint len, float volume)
    {
        capturedDstBufferPointer = dst;
        capturedSrcBufferPointer = src;
        capturedFormat = format;
        capturedBufferLength = len;
        capturedVolume = volume;
        return nextBool;
    }

    private static IntPtr CaptureCreateAudioStreamWithSpecs(in SDL3.SDL.AudioSpec srcSpec, in SDL3.SDL.AudioSpec dstSpec)
    {
        capturedSrcSpec = srcSpec;
        capturedDstSpec = dstSpec;
        return nextPointer;
    }

    private static void CaptureRemoveEventWatch(SDL3.SDL.EventFilter filter, IntPtr userdata)
    {
        capturedCallCount++;
        capturedEventFilter = filter;
        capturedUserdata = userdata;
    }

    private static int CaptureAddGamepadMapping(string mapping)
    {
        capturedMapping = mapping;
        return nextInt;
    }

    private static int CaptureAddGamepadMappingsFromFile(string file)
    {
        capturedFile = file;
        return nextInt;
    }

    private static int CaptureAddGamepadMappingsFromIO(IntPtr src, bool closeio)
    {
        capturedSourcePointer = src;
        capturedCloseIO = closeio;
        return nextInt;
    }

    private static void CaptureCloseGamepad(IntPtr gamepad)
    {
        capturedCallCount++;
        capturedGamepad = gamepad;
    }

    private static IntPtr CaptureGetGamepadFromID(uint instanceID)
    {
        capturedInstanceID = instanceID;
        return nextPointer;
    }

    private static IntPtr CaptureGetGamepadFromPlayerIndex(int playerIndex)
    {
        capturedPlayerIndex = playerIndex;
        return nextPointer;
    }

    private static IntPtr CaptureGetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, SDL3.SDL.GamepadAxis axis)
    {
        capturedGamepad = gamepad;
        capturedGamepadAxis = axis;
        return nextPointer;
    }

    private static IntPtr CaptureGetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, SDL3.SDL.GamepadButton button)
    {
        capturedGamepad = gamepad;
        capturedGamepadButton = button;
        return nextPointer;
    }

    private static bool CaptureGamepadConnected(IntPtr gamepad)
    {
        capturedGamepad = gamepad;
        return nextBool;
    }

    private static short CaptureGetGamepadAxis(IntPtr gamepad, SDL3.SDL.GamepadAxis axis)
    {
        capturedGamepad = gamepad;
        capturedGamepadAxis = axis;
        return nextShort;
    }

    private static SDL3.SDL.GamepadAxis CaptureGetGamepadAxisFromString(string str)
    {
        capturedString = str;
        return nextGamepadAxis;
    }

    private static void ResetCaptureState()
    {
        capturedStream = IntPtr.Zero;
        capturedBuffer = IntPtr.Zero;
        capturedFreeMemory = IntPtr.Zero;
        capturedSourcePointer = IntPtr.Zero;
        capturedDstBufferPointer = IntPtr.Zero;
        capturedSrcBufferPointer = IntPtr.Zero;
        capturedUserdata = IntPtr.Zero;
        capturedGamepad = IntPtr.Zero;
        capturedSrcSpec = default;
        capturedDstSpec = default;
        capturedFormat = default;
        capturedEventFilter = null;
        capturedGamepadAxis = default;
        capturedGamepadButton = default;
        capturedMapping = null;
        capturedFile = null;
        capturedString = null;
        capturedBufferLength = 0;
        capturedInstanceID = 0;
        capturedVolume = 0;
        capturedLen = 0;
        capturedCallCount = 0;
        capturedPlayerIndex = 0;
        capturedCloseIO = false;
        capturedByteBuffer = null;
        nextInt = 0;
        nextShort = 0;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextSpec = default;
        nextGamepadAxis = default;
        nextAudioLen = 0;
    }

    private static SDL3.SDL.AudioSpec CreateAudioSpec(SDL3.SDL.AudioFormat format, int channels, int freq)
    {
        return new SDL3.SDL.AudioSpec
        {
            Format = format,
            Channels = channels,
            Freq = freq
        };
    }

    private static void AssertAudioSpec(SDL3.SDL.AudioSpec expected, SDL3.SDL.AudioSpec actual, string message)
    {
        TestAssert.Equal(expected.Format, actual.Format, $"{message} Format mismatch.");
        TestAssert.Equal(expected.Channels, actual.Channels, $"{message} Channels mismatch.");
        TestAssert.Equal(expected.Freq, actual.Freq, $"{message} Freq mismatch.");
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
