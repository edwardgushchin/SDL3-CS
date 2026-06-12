using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.PlatformAndCPUInformation.Cpuinfo;

internal static class PInvokeTests
{
    private static int nextInt;
    private static bool nextBool;
    private static UIntPtr nextUIntPtr;
    private static int capturedCallCount;

    public static void RunAll()
    {
        GetNumLogicalCPUCores_ReturnsNativeValue();
        GetCPUCacheLineSize_ReturnsNativeValue();
        HasAltiVec_ReturnsNativeValues();
        HasMMX_ReturnsNativeValues();
        HasSSE_ReturnsNativeValues();
        HasSSE2_ReturnsNativeValues();
        HasSSE3_ReturnsNativeValues();
        HasSSE41_ReturnsNativeValues();
        HasSSE42_ReturnsNativeValues();
        HasAVX_ReturnsNativeValues();
        HasAVX2_ReturnsNativeValues();
        HasAVX512F_ReturnsNativeValues();
        HasARMSIMD_ReturnsNativeValues();
        HasNEON_ReturnsNativeValues();
        HasSVE2_ReturnsNativeValues();
        HasLSX_ReturnsNativeValues();
        HasLASX_ReturnsNativeValues();
        GetSystemRAM_ReturnsNativeValue();
        GetSIMDAlignment_ReturnsNativeValue();
        GetSystemPageSize_ReturnsNativeValue();
    }

    public static void GetNumLogicalCPUCores_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 12;

        using NativeHookScope _ = NativeHookScope.Install("GetNumLogicalCPUCoresNativeFunction", nameof(CaptureInt));
        int result = SDL3.SDL.GetNumLogicalCPUCores();

        TestAssert.Equal(12, result, "SDL.GetNumLogicalCPUCores must return the native value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetNumLogicalCPUCores must call the native hook once.");
    }

    public static void GetCPUCacheLineSize_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 64;

        using NativeHookScope _ = NativeHookScope.Install("GetCPUCacheLineSizeNativeFunction", nameof(CaptureInt));
        int result = SDL3.SDL.GetCPUCacheLineSize();

        TestAssert.Equal(64, result, "SDL.GetCPUCacheLineSize must return the native value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetCPUCacheLineSize must call the native hook once.");
    }

    public static void HasAltiVec_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasAltiVecNativeFunction", SDL3.SDL.HasAltiVec, "SDL.HasAltiVec");
    }

    public static void HasMMX_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasMMXNativeFunction", SDL3.SDL.HasMMX, "SDL.HasMMX");
    }

    public static void HasSSE_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasSSENativeFunction", SDL3.SDL.HasSSE, "SDL.HasSSE");
    }

    public static void HasSSE2_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasSSE2NativeFunction", SDL3.SDL.HasSSE2, "SDL.HasSSE2");
    }

    public static void HasSSE3_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasSSE3NativeFunction", SDL3.SDL.HasSSE3, "SDL.HasSSE3");
    }

    public static void HasSSE41_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasSSE41NativeFunction", SDL3.SDL.HasSSE41, "SDL.HasSSE41");
    }

    public static void HasSSE42_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasSSE42NativeFunction", SDL3.SDL.HasSSE42, "SDL.HasSSE42");
    }

    public static void HasAVX_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasAVXNativeFunction", SDL3.SDL.HasAVX, "SDL.HasAVX");
    }

    public static void HasAVX2_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasAVX2NativeFunction", SDL3.SDL.HasAVX2, "SDL.HasAVX2");
    }

    public static void HasAVX512F_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasAVX512FNativeFunction", SDL3.SDL.HasAVX512F, "SDL.HasAVX512F");
    }

    public static void HasARMSIMD_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasARMSIMDNativeFunction", SDL3.SDL.HasARMSIMD, "SDL.HasARMSIMD");
    }

    public static void HasNEON_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasNEONNativeFunction", SDL3.SDL.HasNEON, "SDL.HasNEON");
    }

    public static void HasSVE2_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasSVE2NativeFunction", SDL3.SDL.HasSVE2, "SDL.HasSVE2");
    }

    public static void HasLSX_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasLSXNativeFunction", SDL3.SDL.HasLSX, "SDL.HasLSX");
    }

    public static void HasLASX_ReturnsNativeValues()
    {
        AssertBooleanWrapper("HasLASXNativeFunction", SDL3.SDL.HasLASX, "SDL.HasLASX");
    }

    public static void GetSystemRAM_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 32768;

        using NativeHookScope _ = NativeHookScope.Install("GetSystemRAMNativeFunction", nameof(CaptureInt));
        int result = SDL3.SDL.GetSystemRAM();

        TestAssert.Equal(32768, result, "SDL.GetSystemRAM must return the native value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSystemRAM must call the native hook once.");
    }

    public static void GetSIMDAlignment_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextUIntPtr = (UIntPtr)64;

        using NativeHookScope _ = NativeHookScope.Install("GetSIMDAlignmentNativeFunction", nameof(CaptureUIntPtr));
        UIntPtr result = SDL3.SDL.GetSIMDAlignment();

        TestAssert.Equal((UIntPtr)64, result, "SDL.GetSIMDAlignment must return the native value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSIMDAlignment must call the native hook once.");
    }

    public static void GetSystemPageSize_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 0;

        using NativeHookScope _ = NativeHookScope.Install("GetSystemPageSizeNativeFunction", nameof(CaptureInt));
        int result = SDL3.SDL.GetSystemPageSize();

        TestAssert.Equal(0, result, "SDL.GetSystemPageSize must return the native value, including the documented 0 case.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSystemPageSize must call the native hook once.");
    }

    private static void AssertBooleanWrapper(string fieldName, Func<bool> wrapper, string functionName)
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install(fieldName, nameof(CaptureBool));

        nextBool = true;
        TestAssert.Equal(true, wrapper(), $"{functionName} must return a true native value.");

        nextBool = false;
        TestAssert.Equal(false, wrapper(), $"{functionName} must return a false native value.");

        TestAssert.Equal(2, capturedCallCount, $"{functionName} must call the native hook once per invocation.");
    }

    private static int CaptureInt()
    {
        capturedCallCount++;
        return nextInt;
    }

    private static bool CaptureBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static UIntPtr CaptureUIntPtr()
    {
        capturedCallCount++;
        return nextUIntPtr;
    }

    private static void ResetCaptureState()
    {
        nextInt = 0;
        nextBool = false;
        nextUIntPtr = UIntPtr.Zero;
        capturedCallCount = 0;
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
