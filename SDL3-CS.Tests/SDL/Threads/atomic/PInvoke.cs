using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Threads.Atomic;

internal static class PInvokeTests
{
    private static int capturedCallCount;

    public static void RunAll()
    {
        TryLockSpinlock_AcquiresUnlockedAndFailsWhenHeld();
        LockSpinlock_AcquiresAndUnlockSpinlock_Releases();
        UnlockSpinlock_ReleasesHeldLock();
        MemoryBarrierReleaseFunction_ForwardsToNativeFunction();
        MemoryBarrierAcquireFunction_ForwardsToNativeFunction();
        CompareAndSwapAtomicInt_UpdatesOnlyMatchingValue();
        SetAtomicInt_ReturnsPreviousAndSetsValue();
        GetAtomicInt_ReturnsCurrentValue();
        AddAtomicInt_ReturnsPreviousAndAddsValue();
        CompareAndSwapAtomicU32_UpdatesOnlyMatchingValue();
        SetAtomicU32_ReturnsPreviousAndSetsValue();
        GetAtomicU32_ReturnsCurrentValue();
        AddAtomicU32_ReturnsPreviousAndAddsValue();
        CompareAndSwapAtomicPointer_UpdatesOnlyMatchingValue();
        SetAtomicPointer_ReturnsPreviousAndSetsValue();
        GetAtomicPointer_ReturnsCurrentValue();
    }

    public static void TryLockSpinlock_AcquiresUnlockedAndFailsWhenHeld()
    {
        int spinlock = 0;

        bool acquired = SDL3.SDL.TryLockSpinlock(ref spinlock);

        try
        {
            TestAssert.Equal(true, acquired, "SDL.TryLockSpinlock must acquire an unlocked spinlock.");
            TestAssert.True(spinlock != 0, "SDL.TryLockSpinlock must update the spinlock state.");

            bool reacquired = SDL3.SDL.TryLockSpinlock(ref spinlock);

            TestAssert.Equal(false, reacquired, "SDL.TryLockSpinlock must fail when the spinlock is already held.");
        }
        finally
        {
            SDL3.SDL.UnlockSpinlock(ref spinlock);
        }
    }

    public static void LockSpinlock_AcquiresAndUnlockSpinlock_Releases()
    {
        int spinlock = 0;

        SDL3.SDL.LockSpinlock(ref spinlock);
        TestAssert.True(spinlock != 0, "SDL.LockSpinlock must acquire the spinlock.");

        SDL3.SDL.UnlockSpinlock(ref spinlock);
        TestAssert.Equal(0, spinlock, "SDL.UnlockSpinlock must release a lock acquired by LockSpinlock.");
    }

    public static void UnlockSpinlock_ReleasesHeldLock()
    {
        int spinlock = 1;

        SDL3.SDL.UnlockSpinlock(ref spinlock);

        TestAssert.Equal(0, spinlock, "SDL.UnlockSpinlock must clear a held spinlock.");
    }

    public static void MemoryBarrierReleaseFunction_ForwardsToNativeFunction()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("MemoryBarrierReleaseFunctionNativeFunction", nameof(CaptureVoid));
        SDL3.SDL.MemoryBarrierReleaseFunction();

        TestAssert.Equal(1, capturedCallCount, "SDL.MemoryBarrierReleaseFunction must call the native hook once.");
    }

    public static void MemoryBarrierAcquireFunction_ForwardsToNativeFunction()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("MemoryBarrierAcquireFunctionNativeFunction", nameof(CaptureVoid));
        SDL3.SDL.MemoryBarrierAcquireFunction();

        TestAssert.Equal(1, capturedCallCount, "SDL.MemoryBarrierAcquireFunction must call the native hook once.");
    }

    public static void CompareAndSwapAtomicInt_UpdatesOnlyMatchingValue()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 10 };

        bool swapped = SDL3.SDL.CompareAndSwapAtomicInt(ref value, 10, 20);
        bool rejected = SDL3.SDL.CompareAndSwapAtomicInt(ref value, 10, 30);

        TestAssert.Equal(true, swapped, "SDL.CompareAndSwapAtomicInt must return true for a matching value.");
        TestAssert.Equal(false, rejected, "SDL.CompareAndSwapAtomicInt must return false for a stale value.");
        TestAssert.Equal(20, value.Value, "SDL.CompareAndSwapAtomicInt must only apply the matching swap.");
    }

    public static void SetAtomicInt_ReturnsPreviousAndSetsValue()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 30 };

        int previous = SDL3.SDL.SetAtomicInt(ref value, 40);

        TestAssert.Equal(30, previous, "SDL.SetAtomicInt must return the previous value.");
        TestAssert.Equal(40, value.Value, "SDL.SetAtomicInt must set the new value.");
    }

    public static void GetAtomicInt_ReturnsCurrentValue()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 50 };

        int current = SDL3.SDL.GetAtomicInt(ref value);

        TestAssert.Equal(50, current, "SDL.GetAtomicInt must return the current value.");
        TestAssert.Equal(50, value.Value, "SDL.GetAtomicInt must not modify the value.");
    }

    public static void AddAtomicInt_ReturnsPreviousAndAddsValue()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 60 };

        int previous = SDL3.SDL.AddAtomicInt(ref value, -15);

        TestAssert.Equal(60, previous, "SDL.AddAtomicInt must return the previous value.");
        TestAssert.Equal(45, value.Value, "SDL.AddAtomicInt must add the signed delta.");
    }

    public static void CompareAndSwapAtomicU32_UpdatesOnlyMatchingValue()
    {
        SDL3.SDL.AtomicU32 value = new() { Value = 100 };

        bool swapped = SDL3.SDL.CompareAndSwapAtomicU32(ref value, 100, 200);
        bool rejected = SDL3.SDL.CompareAndSwapAtomicU32(ref value, 100, 300);

        TestAssert.Equal(true, swapped, "SDL.CompareAndSwapAtomicU32 must return true for a matching value.");
        TestAssert.Equal(false, rejected, "SDL.CompareAndSwapAtomicU32 must return false for a stale value.");
        TestAssert.Equal(200u, value.Value, "SDL.CompareAndSwapAtomicU32 must only apply the matching swap.");
    }

    public static void SetAtomicU32_ReturnsPreviousAndSetsValue()
    {
        SDL3.SDL.AtomicU32 value = new() { Value = 300 };

        uint previous = SDL3.SDL.SetAtomicU32(ref value, 400);

        TestAssert.Equal(300u, previous, "SDL.SetAtomicU32 must return the previous value.");
        TestAssert.Equal(400u, value.Value, "SDL.SetAtomicU32 must set the new value.");
    }

    public static void GetAtomicU32_ReturnsCurrentValue()
    {
        SDL3.SDL.AtomicU32 value = new() { Value = 500 };

        uint current = SDL3.SDL.GetAtomicU32(ref value);

        TestAssert.Equal(500u, current, "SDL.GetAtomicU32 must return the current value.");
        TestAssert.Equal(500u, value.Value, "SDL.GetAtomicU32 must not modify the value.");
    }

    public static void AddAtomicU32_ReturnsPreviousAndAddsValue()
    {
        SDL3.SDL.AtomicU32 value = new() { Value = 600 };

        uint previous = SDL3.SDL.AddAtomicU32(ref value, -125);

        TestAssert.Equal(600u, previous, "SDL.AddAtomicU32 must return the previous value.");
        TestAssert.Equal(475u, value.Value, "SDL.AddAtomicU32 must apply a signed delta.");
    }

    public static void CompareAndSwapAtomicPointer_UpdatesOnlyMatchingValue()
    {
        IntPtr value = (IntPtr)0x1001;

        bool swapped = SDL3.SDL.CompareAndSwapAtomicPointer(ref value, (IntPtr)0x1001, (IntPtr)0x1002);
        bool rejected = SDL3.SDL.CompareAndSwapAtomicPointer(ref value, (IntPtr)0x1001, (IntPtr)0x1003);

        TestAssert.Equal(true, swapped, "SDL.CompareAndSwapAtomicPointer must return true for a matching value.");
        TestAssert.Equal(false, rejected, "SDL.CompareAndSwapAtomicPointer must return false for a stale value.");
        TestAssert.Equal((IntPtr)0x1002, value, "SDL.CompareAndSwapAtomicPointer must only apply the matching swap.");
    }

    public static void SetAtomicPointer_ReturnsPreviousAndSetsValue()
    {
        IntPtr value = (IntPtr)0x2001;

        IntPtr previous = SDL3.SDL.SetAtomicPointer(ref value, (IntPtr)0x2002);

        TestAssert.Equal((IntPtr)0x2001, previous, "SDL.SetAtomicPointer must return the previous pointer.");
        TestAssert.Equal((IntPtr)0x2002, value, "SDL.SetAtomicPointer must set the new pointer.");
    }

    public static void GetAtomicPointer_ReturnsCurrentValue()
    {
        IntPtr value = (IntPtr)0x3001;

        IntPtr current = SDL3.SDL.GetAtomicPointer(ref value);

        TestAssert.Equal((IntPtr)0x3001, current, "SDL.GetAtomicPointer must return the current pointer.");
        TestAssert.Equal((IntPtr)0x3001, value, "SDL.GetAtomicPointer must not modify the pointer.");
    }

    private static void CaptureVoid()
    {
        capturedCallCount++;
    }

    private static void ResetCaptureState()
    {
        capturedCallCount = 0;
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
