using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Time.Timer;

internal static class PInvokeTests
{
    private static SDL3.SDL.TimerCallback? capturedTimerCallback;
    private static SDL3.SDL.NSTimerCallback? capturedNSTimerCallback;
    private static IntPtr capturedUserdata;
    private static ulong nextULong;
    private static ulong capturedNs;
    private static ulong capturedNSInterval;
    private static uint nextUInt;
    private static uint capturedMs;
    private static uint capturedInterval;
    private static uint capturedTimerId;
    private static int capturedCallCount;
    private static bool nextBool;

    public static void RunAll()
    {
        GetTicks_ReturnsNativeValue();
        GetTicksNS_ReturnsNativeValue();
        GetPerformanceCounter_ReturnsNativeValue();
        GetPerformanceFrequency_ReturnsNativeValue();
        Delay_ForwardsMilliseconds();
        DelayNS_ForwardsNanoseconds();
        DelayPrecise_ForwardsNanoseconds();
        AddTimer_ForwardsIntervalCallbackUserdataAndReturnsNativeValue();
        AddTimerNS_ForwardsIntervalCallbackUserdataAndReturnsNativeValue();
        RemoveTimer_ForwardsIdAndReturnsNativeValue();
    }

    public static void GetTicks_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextULong = 101;

        using NativeHookScope _ = NativeHookScope.Install("GetTicksNativeFunction", nameof(CaptureULongNoArgs));
        ulong result = SDL3.SDL.GetTicks();

        TestAssert.Equal(101ul, result, "SDL.GetTicks must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetTicks must call the native hook once.");
    }

    public static void GetTicksNS_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextULong = 202;

        using NativeHookScope _ = NativeHookScope.Install("GetTicksNSNativeFunction", nameof(CaptureULongNoArgs));
        ulong result = SDL3.SDL.GetTicksNS();

        TestAssert.Equal(202ul, result, "SDL.GetTicksNS must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetTicksNS must call the native hook once.");
    }

    public static void GetPerformanceCounter_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextULong = 303;

        using NativeHookScope _ = NativeHookScope.Install("GetPerformanceCounterNativeFunction", nameof(CaptureULongNoArgs));
        ulong result = SDL3.SDL.GetPerformanceCounter();

        TestAssert.Equal(303ul, result, "SDL.GetPerformanceCounter must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPerformanceCounter must call the native hook once.");
    }

    public static void GetPerformanceFrequency_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextULong = 404;

        using NativeHookScope _ = NativeHookScope.Install("GetPerformanceFrequencyNativeFunction", nameof(CaptureULongNoArgs));
        ulong result = SDL3.SDL.GetPerformanceFrequency();

        TestAssert.Equal(404ul, result, "SDL.GetPerformanceFrequency must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPerformanceFrequency must call the native hook once.");
    }

    public static void Delay_ForwardsMilliseconds()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("DelayNativeFunction", nameof(CaptureDelay));
        SDL3.SDL.Delay(55);

        TestAssert.Equal(55u, capturedMs, "SDL.Delay must forward milliseconds.");
        TestAssert.Equal(1, capturedCallCount, "SDL.Delay must call the native hook once.");
    }

    public static void DelayNS_ForwardsNanoseconds()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("DelayNSNativeFunction", nameof(CaptureDelayNS));
        SDL3.SDL.DelayNS(66);

        TestAssert.Equal(66ul, capturedNs, "SDL.DelayNS must forward nanoseconds.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DelayNS must call the native hook once.");
    }

    public static void DelayPrecise_ForwardsNanoseconds()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("DelayPreciseNativeFunction", nameof(CaptureDelayNS));
        SDL3.SDL.DelayPrecise(77);

        TestAssert.Equal(77ul, capturedNs, "SDL.DelayPrecise must forward nanoseconds.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DelayPrecise must call the native hook once.");
    }

    public static void AddTimer_ForwardsIntervalCallbackUserdataAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 808;

        using NativeHookScope _ = NativeHookScope.Install("AddTimerNativeFunction", nameof(CaptureAddTimer));
        uint result = SDL3.SDL.AddTimer(100, TestTimerCallback, (IntPtr)0x1001);

        TestAssert.Equal(808u, result, "SDL.AddTimer must return the native hook value.");
        TestAssert.Equal(100u, capturedInterval, "SDL.AddTimer must forward interval.");
        TestAssert.Equal((IntPtr)0x1001, capturedUserdata, "SDL.AddTimer must forward userdata.");
        TestAssert.NotNull(capturedTimerCallback, "SDL.AddTimer must forward callback.");
        TestAssert.Equal(123u, capturedTimerCallback!((IntPtr)0x1002, 9, 101), "SDL.AddTimer must preserve callback.");
    }

    public static void AddTimerNS_ForwardsIntervalCallbackUserdataAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 909;

        using NativeHookScope _ = NativeHookScope.Install("AddTimerNSNativeFunction", nameof(CaptureAddTimerNS));
        uint result = SDL3.SDL.AddTimerNS(1000, TestNSTimerCallback, (IntPtr)0x2001);

        TestAssert.Equal(909u, result, "SDL.AddTimerNS must return the native hook value.");
        TestAssert.Equal(1000ul, capturedNSInterval, "SDL.AddTimerNS must forward interval.");
        TestAssert.Equal((IntPtr)0x2001, capturedUserdata, "SDL.AddTimerNS must forward userdata.");
        TestAssert.NotNull(capturedNSTimerCallback, "SDL.AddTimerNS must forward callback.");
        TestAssert.Equal(456ul, capturedNSTimerCallback!((IntPtr)0x2002, 10, 1001), "SDL.AddTimerNS must preserve callback.");
    }

    public static void RemoveTimer_ForwardsIdAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("RemoveTimerNativeFunction", nameof(CaptureRemoveTimer));
        bool result = SDL3.SDL.RemoveTimer(42);

        TestAssert.Equal(true, result, "SDL.RemoveTimer must return the native hook value.");
        TestAssert.Equal(42u, capturedTimerId, "SDL.RemoveTimer must forward timer id.");
    }

    private static uint TestTimerCallback(IntPtr userdata, uint timerId, uint interval)
    {
        capturedUserdata = userdata;
        capturedTimerId = timerId;
        capturedInterval = interval;
        return 123;
    }

    private static ulong TestNSTimerCallback(IntPtr userdata, uint timerId, ulong interval)
    {
        capturedUserdata = userdata;
        capturedTimerId = timerId;
        capturedNSInterval = interval;
        return 456;
    }

    private static ulong CaptureULongNoArgs()
    {
        capturedCallCount++;
        return nextULong;
    }

    private static void CaptureDelay(uint ms)
    {
        capturedMs = ms;
        capturedCallCount++;
    }

    private static void CaptureDelayNS(ulong ns)
    {
        capturedNs = ns;
        capturedCallCount++;
    }

    private static uint CaptureAddTimer(uint interval, SDL3.SDL.TimerCallback callback, IntPtr userdata)
    {
        capturedInterval = interval;
        capturedTimerCallback = callback;
        capturedUserdata = userdata;
        return nextUInt;
    }

    private static uint CaptureAddTimerNS(ulong interval, SDL3.SDL.NSTimerCallback callback, IntPtr userdata)
    {
        capturedNSInterval = interval;
        capturedNSTimerCallback = callback;
        capturedUserdata = userdata;
        return nextUInt;
    }

    private static bool CaptureRemoveTimer(uint id)
    {
        capturedTimerId = id;
        return nextBool;
    }

    private static void ResetCaptureState()
    {
        capturedTimerCallback = null;
        capturedNSTimerCallback = null;
        capturedUserdata = IntPtr.Zero;
        nextULong = 0;
        capturedNs = 0;
        capturedNSInterval = 0;
        nextUInt = 0;
        capturedMs = 0;
        capturedInterval = 0;
        capturedTimerId = 0;
        capturedCallCount = 0;
        nextBool = false;
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private NativeHookScope(FieldInfo field, object? hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL private hook field {fieldName} must exist.");

            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"Test hook method {methodName} must exist.");

            Delegate hook = Delegate.CreateDelegate(field!.FieldType, method!);

            return new NativeHookScope(field, hook);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
