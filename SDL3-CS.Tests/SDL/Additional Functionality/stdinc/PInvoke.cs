using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Stdinc;

internal static class PInvokeTests
{
    private static bool setMemoryFunctionsCalled;
    private static SDL3.SDL.MallocFunc? capturedMallocFunc;
    private static SDL3.SDL.CallocFunc? capturedCallocFunc;
    private static SDL3.SDL.ReallocFunc? capturedReallocFunc;
    private static SDL3.SDL.FreeFunc? capturedFreeFunc;
    private static bool testFreeFuncCalled;
    private static IntPtr capturedFreeMemory;
    private static UIntPtr capturedAlignedAllocZeroAlignment;
    private static UIntPtr capturedAlignedAllocZeroSize;
    private static string? capturedWideString;
    private static IntPtr capturedWideEndp;
    private static int capturedWideBase;

    public static void Malloc_AllocatesWritableMemory()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Malloc), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Malloc method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_malloc");

        IntPtr memory = SDL3.SDL.Malloc((UIntPtr)4);
        TestAssert.True(memory != IntPtr.Zero, "SDL.Malloc must allocate memory.");

        try
        {
            Marshal.WriteByte(memory, 0, 0x5A);
            TestAssert.Equal((byte)0x5A, Marshal.ReadByte(memory, 0), "SDL.Malloc memory must be writable.");
        }
        finally
        {
            SDL3.SDL.Free(memory);
        }
    }

    public static void Calloc_AllocatesZeroedMemory()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Calloc), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Calloc method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_calloc");

        IntPtr memory = SDL3.SDL.Calloc((UIntPtr)4, (UIntPtr)1);
        TestAssert.True(memory != IntPtr.Zero, "SDL.Calloc must allocate memory.");

        try
        {
            TestAssert.Equal((byte)0, Marshal.ReadByte(memory, 0), "SDL.Calloc must zero byte 0.");
            TestAssert.Equal((byte)0, Marshal.ReadByte(memory, 1), "SDL.Calloc must zero byte 1.");
            TestAssert.Equal((byte)0, Marshal.ReadByte(memory, 2), "SDL.Calloc must zero byte 2.");
            TestAssert.Equal((byte)0, Marshal.ReadByte(memory, 3), "SDL.Calloc must zero byte 3.");
        }
        finally
        {
            SDL3.SDL.Free(memory);
        }
    }

    public static void Realloc_ResizesAndPreservesMemory()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Realloc), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Realloc method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_realloc");

        IntPtr memory = SDL3.SDL.Malloc((UIntPtr)4);
        TestAssert.True(memory != IntPtr.Zero, "SDL.Malloc must allocate memory before realloc.");

        try
        {
            Marshal.WriteByte(memory, 0, 0x6B);
            IntPtr resized = SDL3.SDL.Realloc(memory, (UIntPtr)8);
            TestAssert.True(resized != IntPtr.Zero, "SDL.Realloc must return a valid pointer.");
            memory = resized;
            TestAssert.Equal((byte)0x6B, Marshal.ReadByte(memory, 0), "SDL.Realloc must preserve existing bytes.");
        }
        finally
        {
            SDL3.SDL.Free(memory);
        }
    }

    public static void Free_FreesAllocatedMemoryAndAllowsNull()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_Free", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_Free method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_free");
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Free), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Free method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        SDL3.SDL.Free(IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.Free must forward null to the native hook.");
        IntPtr memory = new(0x1234);
        SDL3.SDL.Free(memory);
        TestAssert.Equal(memory, capturedFreeMemory, "SDL.Free must forward memory to the native hook.");
    }

    public static void GetOriginalMemoryFunctions_ReturnsCallableFunctions()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_GetOriginalMemoryFunctions", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_GetOriginalMemoryFunctions method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_GetOriginalMemoryFunctions");
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetOriginalMemoryFunctions), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetOriginalMemoryFunctions method must be public static.");

        SDL3.SDL.GetOriginalMemoryFunctions(out SDL3.SDL.MallocFunc mallocFunc, out SDL3.SDL.CallocFunc callocFunc, out SDL3.SDL.ReallocFunc reallocFunc, out SDL3.SDL.FreeFunc freeFunc);

        AssertMemoryFunctionsAreCallable(mallocFunc, callocFunc, reallocFunc, freeFunc, "SDL.GetOriginalMemoryFunctions");
    }

    public static void GetMemoryFunctions_ReturnsCallableFunctions()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_GetMemoryFunctions", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_GetMemoryFunctions method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_GetMemoryFunctions");
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetMemoryFunctions), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetMemoryFunctions method must be public static.");

        SDL3.SDL.GetMemoryFunctions(out SDL3.SDL.MallocFunc mallocFunc, out SDL3.SDL.CallocFunc callocFunc, out SDL3.SDL.ReallocFunc reallocFunc, out SDL3.SDL.FreeFunc freeFunc);

        AssertMemoryFunctionsAreCallable(mallocFunc, callocFunc, reallocFunc, freeFunc, "SDL.GetMemoryFunctions");
    }

    public static void SetMemoryFunctions_ForwardsCallbacksThroughHook()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_SetMemoryFunctions", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_SetMemoryFunctions method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_SetMemoryFunctions");
        AssertBoolReturnMarshal(nativeMethod!);

        setMemoryFunctionsCalled = false;
        testFreeFuncCalled = false;
        SDL3.SDL.MallocFunc mallocFunc = TestMallocFunc;
        SDL3.SDL.CallocFunc callocFunc = TestCallocFunc;
        SDL3.SDL.ReallocFunc reallocFunc = TestReallocFunc;
        SDL3.SDL.FreeFunc freeFunc = TestFreeFunc;

        using NativeHookScope _ = NativeHookScope.Install("SetMemoryFunctionsNativeFunction", nameof(CaptureSetMemoryFunctions));
        bool result = SDL3.SDL.SetMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);

        TestAssert.Equal(true, result, "SDL.SetMemoryFunctions must return the native hook result.");
        TestAssert.Equal(true, setMemoryFunctionsCalled, "SDL.SetMemoryFunctions must call the native hook.");
        TestAssert.Equal(mallocFunc, capturedMallocFunc, "SDL.SetMemoryFunctions must forward malloc callback.");
        TestAssert.Equal(callocFunc, capturedCallocFunc, "SDL.SetMemoryFunctions must forward calloc callback.");
        TestAssert.Equal(reallocFunc, capturedReallocFunc, "SDL.SetMemoryFunctions must forward realloc callback.");
        TestAssert.Equal(freeFunc, capturedFreeFunc, "SDL.SetMemoryFunctions must forward free callback.");
        TestAssert.Equal(true, testFreeFuncCalled, "SDL.SetMemoryFunctions hook must keep the free callback callable.");
    }

    public static void AlignedAlloc_ReturnsAlignedMemory()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.AlignedAlloc), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.AlignedAlloc method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_aligned_alloc");

        IntPtr memory = SDL3.SDL.AlignedAlloc((UIntPtr)16, (UIntPtr)16);
        TestAssert.True(memory != IntPtr.Zero, "SDL.AlignedAlloc must allocate memory.");

        try
        {
            TestAssert.Equal(0, memory.ToInt64() % 16, "SDL.AlignedAlloc must return memory aligned to the requested boundary.");
            Marshal.WriteByte(memory, 0, 0x3C);
            TestAssert.Equal((byte)0x3C, Marshal.ReadByte(memory, 0), "SDL.AlignedAlloc memory must be writable.");
        }
        finally
        {
            SDL3.SDL.AlignedFree(memory);
        }
    }

    public static void AlignedAllocZero_ReturnsZeroedAlignedMemory()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_AlignedAllocZero", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_AlignedAllocZero method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_aligned_alloc_zero");

        using NativeHookScope _ = NativeHookScope.Install("AlignedAllocZeroNativeFunction", nameof(CaptureAlignedAllocZero));
        IntPtr memory = SDL3.SDL.AlignedAllocZero((UIntPtr)16, (UIntPtr)16);
        TestAssert.True(memory != IntPtr.Zero, "SDL.AlignedAllocZero must allocate memory.");
        TestAssert.Equal((UIntPtr)16, capturedAlignedAllocZeroAlignment, "SDL.AlignedAllocZero must forward alignment.");
        TestAssert.Equal((UIntPtr)16, capturedAlignedAllocZeroSize, "SDL.AlignedAllocZero must forward size.");

        try
        {
            TestAssert.Equal(0, memory.ToInt64() % 16, "SDL.AlignedAllocZero must return memory aligned to the requested boundary.");
            TestAssert.Equal((byte)0, Marshal.ReadByte(memory, 0), "SDL.AlignedAllocZero must zero byte 0.");
            TestAssert.Equal((byte)0, Marshal.ReadByte(memory, 15), "SDL.AlignedAllocZero must zero the last requested byte.");
        }
        finally
        {
            SDL3.SDL.AlignedFree(memory);
        }
    }

    public static void AlignedFree_FreesAlignedMemoryAndAllowsNull()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.AlignedFree), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.AlignedFree method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_aligned_free");

        SDL3.SDL.AlignedFree(IntPtr.Zero);
        IntPtr memory = SDL3.SDL.AlignedAlloc((UIntPtr)16, (UIntPtr)16);
        TestAssert.True(memory != IntPtr.Zero, "SDL.AlignedAlloc must allocate memory for SDL.AlignedFree.");
        SDL3.SDL.AlignedFree(memory);
    }

    public static void GetNumAllocations_ReturnsKnownSentinelOrCount()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetNumAllocations), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetNumAllocations method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GetNumAllocations");

        int count = SDL3.SDL.GetNumAllocations();

        TestAssert.True(count >= -1, "SDL.GetNumAllocations must return a count or -1 when counting is disabled.");
    }

    public static void GetEnvironment_ReturnsGlobalEnvironment()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetEnvironment), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetEnvironment method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GetEnvironment");

        IntPtr environment = SDL3.SDL.GetEnvironment();

        TestAssert.True(environment != IntPtr.Zero, "SDL.GetEnvironment must return the process environment.");
    }

    public static void CreateEnvironment_CreatesEmptyAndPopulatedEnvironments()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.CreateEnvironment), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.CreateEnvironment method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_CreateEnvironment");
        AssertBooleanParameterMarshal(method!, "populated");

        IntPtr emptyEnvironment = SDL3.SDL.CreateEnvironment(populated: false);
        TestAssert.True(emptyEnvironment != IntPtr.Zero, "SDL.CreateEnvironment(false) must create an environment.");
        SDL3.SDL.DestroyEnvironment(emptyEnvironment);

        IntPtr populatedEnvironment = SDL3.SDL.CreateEnvironment(populated: true);
        TestAssert.True(populatedEnvironment != IntPtr.Zero, "SDL.CreateEnvironment(true) must create an environment.");
        SDL3.SDL.DestroyEnvironment(populatedEnvironment);
    }

    public static void SDL_GetEnvironmentVariable_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_GetEnvironmentVariable", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_GetEnvironmentVariable method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_GetEnvironmentVariable");
        AssertStringParameterMarshal(method!, "name");
    }

    public static void GetEnvironmentVariable_ReturnsValueAndNull()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetEnvironmentVariable), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetEnvironmentVariable method must be public static.");

        using EnvironmentScope environment = EnvironmentScope.CreateEmpty();
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_VALUE", "first", overwrite: true), "SDL.SetEnvironmentVariable must set a variable for GetEnvironmentVariable.");

        string? value = SDL3.SDL.GetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_VALUE");
        string? missing = SDL3.SDL.GetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_MISSING");

        TestAssert.Equal("first", value, "SDL.GetEnvironmentVariable must return the stored value.");
        TestAssert.Equal<string?>(null, missing, "SDL.GetEnvironmentVariable must return null for a missing value.");
    }

    public static void SDL_GetEnvironmentVariables_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_GetEnvironmentVariables", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_GetEnvironmentVariables method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_GetEnvironmentVariables");
    }

    public static void GetEnvironmentVariables_ReturnsVariableArrayAndFreesNativeArray()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetEnvironmentVariables), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetEnvironmentVariables method must be public static.");

        using EnvironmentScope environment = EnvironmentScope.CreateEmpty();
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_ALPHA", "one", overwrite: true), "SDL.SetEnvironmentVariable must set alpha variable.");
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_BETA", "two", overwrite: true), "SDL.SetEnvironmentVariable must set beta variable.");

        string[]? variables = SDL3.SDL.GetEnvironmentVariables(environment.Handle);

        TestAssert.NotNull(variables, "SDL.GetEnvironmentVariables must return a variable array for a valid environment.");
        TestAssert.True(variables!.Contains("SDL3_CS_ENV_ALPHA=one"), "SDL.GetEnvironmentVariables must include the alpha variable.");
        TestAssert.True(variables.Contains("SDL3_CS_ENV_BETA=two"), "SDL.GetEnvironmentVariables must include the beta variable.");
    }

    public static void SetEnvironmentVariable_SetsAndHonorsOverwrite()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.SetEnvironmentVariable), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SetEnvironmentVariable method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_SetEnvironmentVariable");
        AssertBoolReturnMarshal(method!);
        AssertStringParameterMarshal(method!, "name");
        AssertStringParameterMarshal(method!, "value");
        AssertBooleanParameterMarshal(method!, "overwrite");

        using EnvironmentScope environment = EnvironmentScope.CreateEmpty();
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_SET", "first", overwrite: true), "SDL.SetEnvironmentVariable must set an initial value.");
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_SET", "second", overwrite: false), "SDL.SetEnvironmentVariable must succeed without overwriting.");
        TestAssert.Equal("first", SDL3.SDL.GetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_SET"), "SDL.SetEnvironmentVariable(false) must preserve an existing value.");
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_SET", "third", overwrite: true), "SDL.SetEnvironmentVariable must overwrite when requested.");
        TestAssert.Equal("third", SDL3.SDL.GetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_SET"), "SDL.SetEnvironmentVariable(true) must replace an existing value.");
    }

    public static void UnsetEnvironmentVariable_RemovesVariable()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.UnsetEnvironmentVariable), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.UnsetEnvironmentVariable method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_UnsetEnvironmentVariable");
        AssertBoolReturnMarshal(method!);
        AssertStringParameterMarshal(method!, "name");

        using EnvironmentScope environment = EnvironmentScope.CreateEmpty();
        TestAssert.True(SDL3.SDL.SetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_UNSET", "value", overwrite: true), "SDL.SetEnvironmentVariable must set a value to unset.");
        TestAssert.True(SDL3.SDL.UnsetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_UNSET"), "SDL.UnsetEnvironmentVariable must remove a value.");
        TestAssert.Equal<string?>(null, SDL3.SDL.GetEnvironmentVariable(environment.Handle, "SDL3_CS_ENV_UNSET"), "SDL.UnsetEnvironmentVariable must remove the value from the environment.");
    }

    public static void DestroyEnvironment_DestroysCreatedEnvironment()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.DestroyEnvironment), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.DestroyEnvironment method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_DestroyEnvironment");

        IntPtr environment = SDL3.SDL.CreateEnvironment(populated: false);
        TestAssert.True(environment != IntPtr.Zero, "SDL.CreateEnvironment must create an environment for destruction.");

        SDL3.SDL.DestroyEnvironment(environment);
    }

    public static void SRand_SeedsRandSequence()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.SRand), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SRand method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_srand");

        SDL3.SDL.SRand(123);
        int first = SDL3.SDL.Rand(1000);
        SDL3.SDL.SRand(123);
        int second = SDL3.SDL.Rand(1000);

        TestAssert.Equal(first, second, "SDL.SRand must make SDL.Rand deterministic for the same seed.");
    }

    public static void Rand_ReturnsValueWithinBound()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Rand), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Rand method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_rand");

        SDL3.SDL.SRand(42);
        int value = SDL3.SDL.Rand(10);

        TestAssert.True(value >= 0, "SDL.Rand must not return negative values for a positive bound.");
        TestAssert.True(value < 10, "SDL.Rand must return values below the positive bound.");
    }

    public static void RandF_ReturnsUnitRange()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.RandF), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.RandF method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_randf");

        SDL3.SDL.SRand(43);
        float value = SDL3.SDL.RandF();

        TestAssert.True(value >= 0.0f, "SDL.RandF must not return negative values.");
        TestAssert.True(value < 1.0f, "SDL.RandF must return values below 1.");
    }

    public static void RandBits_ReturnsDeterministicValueAfterSeed()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.RandBits), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.RandBits method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_rand_bits");

        SDL3.SDL.SRand(44);
        uint first = SDL3.SDL.RandBits();
        SDL3.SDL.SRand(44);
        uint second = SDL3.SDL.RandBits();

        TestAssert.Equal(first, second, "SDL.RandBits must be deterministic after resetting the seed.");
    }

    public static void RandR_ReturnsValueWithinBoundAndUpdatesState()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.RandR), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.RandR method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_rand_r");

        ulong state = 45;
        ulong original = state;
        int value = SDL3.SDL.RandR(ref state, 10);

        TestAssert.True(value >= 0, "SDL.RandR must not return negative values for a positive bound.");
        TestAssert.True(value < 10, "SDL.RandR must return values below the positive bound.");
        TestAssert.True(state != original, "SDL.RandR must update the caller-provided state.");
    }

    public static void RandFR_ReturnsUnitRangeAndUpdatesState()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.RandFR), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.RandFR method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_randf_r");

        ulong state = 46;
        ulong original = state;
        float value = SDL3.SDL.RandFR(ref state);

        TestAssert.True(value >= 0.0f, "SDL.RandFR must not return negative values.");
        TestAssert.True(value < 1.0f, "SDL.RandFR must return values below 1.");
        TestAssert.True(state != original, "SDL.RandFR must update the caller-provided state.");
    }

    public static void RandBitsR_ReturnsValueAndUpdatesState()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.RandBitsR), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.RandBitsR method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_rand_bits_r");

        ulong state = 47;
        ulong original = state;
        uint value = SDL3.SDL.RandBitsR(ref state);

        TestAssert.Equal(value, value, "SDL.RandBitsR must return a UInt32 value.");
        TestAssert.True(state != original, "SDL.RandBitsR must update the caller-provided state.");
    }

    public static void Wcstoul_ParsesWideUnsignedLong()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Wcstoul), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Wcstoul method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("WcstoulNativeFunction", nameof(CaptureWcstoul));
        CULong value = SDL3.SDL.Wcstoul(" 0x2A", (IntPtr)123, @base: 0);

        TestAssert.Equal(new CULong((UIntPtr)42), value, "SDL.Wcstoul must parse an unsigned long from a wide string.");
        TestAssert.Equal(" 0x2A", capturedWideString, "SDL.Wcstoul must marshal and forward the wide string.");
        TestAssert.Equal((IntPtr)123, capturedWideEndp, "SDL.Wcstoul must forward endp.");
        TestAssert.Equal(0, capturedWideBase, "SDL.Wcstoul must forward base.");
    }

    public static void SDL_wcstoul32_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_wcstoul32", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_wcstoul32 method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_wcstoul");
    }

    public static void SDL_wcstoul64_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_wcstoul64", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_wcstoul64 method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_wcstoul");
    }

    public static void Wcstoll_ParsesWideSignedLongLong()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_Wcstoll", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_Wcstoll method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_wcstoll");
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Wcstoll), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Wcstoll method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("WcstollNativeFunction", nameof(CaptureWcstoll));
        long value = SDL3.SDL.Wcstoll(" -42", (IntPtr)124, @base: 0);

        TestAssert.Equal(-42L, value, "SDL.Wcstoll must parse a signed long long from a wide string.");
        TestAssert.Equal(" -42", capturedWideString, "SDL.Wcstoll must marshal and forward the wide string.");
        TestAssert.Equal((IntPtr)124, capturedWideEndp, "SDL.Wcstoll must forward endp.");
        TestAssert.Equal(0, capturedWideBase, "SDL.Wcstoll must forward base.");
    }

    public static void Wcstoull_ParsesWideUnsignedLongLong()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_Wcstoull", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_Wcstoull method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_wcstoull");
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Wcstoull), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Wcstoull method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("WcstoullNativeFunction", nameof(CaptureWcstoull));
        ulong value = SDL3.SDL.Wcstoull(" 0x2A", (IntPtr)125, @base: 0);

        TestAssert.Equal(42UL, value, "SDL.Wcstoull must parse an unsigned long long from a wide string.");
        TestAssert.Equal(" 0x2A", capturedWideString, "SDL.Wcstoull must marshal and forward the wide string.");
        TestAssert.Equal((IntPtr)125, capturedWideEndp, "SDL.Wcstoull must forward endp.");
        TestAssert.Equal(0, capturedWideBase, "SDL.Wcstoull must forward base.");
    }

    public static void Memset_FillsMemoryAndReturnsDestination()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.Memset), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.Memset method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_memset");

        IntPtr memory = SDL3.SDL.Malloc((UIntPtr)4);
        TestAssert.True(memory != IntPtr.Zero, "SDL.Malloc must allocate memory for SDL.Memset.");

        try
        {
            IntPtr result = SDL3.SDL.Memset(memory, 0xA5, (UIntPtr)4);
            TestAssert.Equal(memory, result, "SDL.Memset must return the destination pointer.");
            TestAssert.Equal((byte)0xA5, Marshal.ReadByte(memory, 0), "SDL.Memset must set byte 0.");
            TestAssert.Equal((byte)0xA5, Marshal.ReadByte(memory, 1), "SDL.Memset must set byte 1.");
            TestAssert.Equal((byte)0xA5, Marshal.ReadByte(memory, 2), "SDL.Memset must set byte 2.");
            TestAssert.Equal((byte)0xA5, Marshal.ReadByte(memory, 3), "SDL.Memset must set byte 3.");
        }
        finally
        {
            SDL3.SDL.Free(memory);
        }
    }

    private static void AssertMemoryFunctionsAreCallable(SDL3.SDL.MallocFunc mallocFunc, SDL3.SDL.CallocFunc callocFunc, SDL3.SDL.ReallocFunc reallocFunc, SDL3.SDL.FreeFunc freeFunc, string owner)
    {
        AssertCdeclDelegate(mallocFunc, owner + " malloc callback");
        AssertCdeclDelegate(callocFunc, owner + " calloc callback");
        AssertCdeclDelegate(reallocFunc, owner + " realloc callback");
        AssertCdeclDelegate(freeFunc, owner + " free callback");

        IntPtr mallocMemory = mallocFunc((UIntPtr)4);
        TestAssert.True(mallocMemory != IntPtr.Zero, owner + " malloc callback must allocate memory.");
        freeFunc(mallocMemory);

        IntPtr callocMemory = callocFunc((UIntPtr)4, (UIntPtr)1);
        TestAssert.True(callocMemory != IntPtr.Zero, owner + " calloc callback must allocate memory.");
        TestAssert.Equal((byte)0, Marshal.ReadByte(callocMemory, 0), owner + " calloc callback must zero memory.");
        IntPtr resized = reallocFunc(callocMemory, (UIntPtr)8);
        TestAssert.True(resized != IntPtr.Zero, owner + " realloc callback must resize memory.");
        freeFunc(resized);
    }

    private static bool CaptureSetMemoryFunctions(SDL3.SDL.MallocFunc mallocFunc, SDL3.SDL.CallocFunc callocFunc, SDL3.SDL.ReallocFunc reallocFunc, SDL3.SDL.FreeFunc freeFunc)
    {
        setMemoryFunctionsCalled = true;
        capturedMallocFunc = mallocFunc;
        capturedCallocFunc = callocFunc;
        capturedReallocFunc = reallocFunc;
        capturedFreeFunc = freeFunc;

        TestAssert.Equal(IntPtr.Zero, mallocFunc((UIntPtr)1), "The test malloc callback must be callable.");
        TestAssert.Equal(IntPtr.Zero, callocFunc((UIntPtr)1, (UIntPtr)1), "The test calloc callback must be callable.");
        TestAssert.Equal(IntPtr.Zero, reallocFunc(IntPtr.Zero, (UIntPtr)1), "The test realloc callback must be callable.");
        freeFunc(IntPtr.Zero);

        return true;
    }

    private static IntPtr CaptureAlignedAllocZero(UIntPtr alignment, UIntPtr size)
    {
        capturedAlignedAllocZeroAlignment = alignment;
        capturedAlignedAllocZeroSize = size;
        IntPtr memory = SDL3.SDL.AlignedAlloc(alignment, size);
        TestAssert.True(memory != IntPtr.Zero, "The aligned_alloc_zero hook must allocate memory.");
        SDL3.SDL.Memset(memory, 0, size);
        return memory;
    }

    private static CULong CaptureWcstoul(IntPtr str, IntPtr endp, int @base)
    {
        CaptureWideArguments(str, endp, @base);
        return new CULong((UIntPtr)42);
    }

    private static long CaptureWcstoll(IntPtr str, IntPtr endp, int @base)
    {
        CaptureWideArguments(str, endp, @base);
        return -42;
    }

    private static ulong CaptureWcstoull(IntPtr str, IntPtr endp, int @base)
    {
        CaptureWideArguments(str, endp, @base);
        return 42;
    }

    private static void CaptureWideArguments(IntPtr str, IntPtr endp, int @base)
    {
        capturedWideString = WCharStringMarshaller.ConvertToManaged(str);
        capturedWideEndp = endp;
        capturedWideBase = @base;
    }

    private static IntPtr TestMallocFunc(UIntPtr size)
    {
        return IntPtr.Zero;
    }

    private static IntPtr TestCallocFunc(UIntPtr nmemb, UIntPtr size)
    {
        return IntPtr.Zero;
    }

    private static IntPtr TestReallocFunc(IntPtr mem, UIntPtr size)
    {
        return IntPtr.Zero;
    }

    private static void TestFreeFunc(IntPtr mem)
    {
        testFreeFuncCalled = true;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeMemory = mem;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertBooleanParameterMarshal(MethodInfo method, string parameterName)
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
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 string marshalling.");
    }

    private static void AssertCdeclDelegate(Delegate callback, string message)
    {
        UnmanagedFunctionPointerAttribute? unmanagedFunctionPointer = callback.GetType().GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(unmanagedFunctionPointer, message + " must keep unmanaged function pointer metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, unmanagedFunctionPointer!.CallingConvention, message + " must use cdecl calling convention.");
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

    private sealed class EnvironmentScope : IDisposable
    {
        private EnvironmentScope(IntPtr handle)
        {
            Handle = handle;
        }

        public IntPtr Handle { get; }

        public static EnvironmentScope CreateEmpty()
        {
            IntPtr handle = SDL3.SDL.CreateEnvironment(populated: false);
            TestAssert.True(handle != IntPtr.Zero, "SDL.CreateEnvironment(false) must create an environment.");
            return new EnvironmentScope(handle);
        }

        public void Dispose()
        {
            SDL3.SDL.DestroyEnvironment(Handle);
        }
    }
}
