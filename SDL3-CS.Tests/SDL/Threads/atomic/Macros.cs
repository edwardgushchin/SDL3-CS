using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Threads.Atomic;

internal static class MacroTests
{
    public static void RunAll()
    {
        AtomicIncRef_AddsOneAndReturnsPreviousValue();
        AtomicDecRef_DecrementsAndReportsZeroTransition();
        MemoryBarrierRelease_CallsFunctionAndKeepsMacroAttribute();
        MemoryBarrierAcquire_CallsFunctionAndKeepsMacroAttribute();
    }

    public static void AtomicIncRef_AddsOneAndReturnsPreviousValue()
    {
        SDL3.SDL.AtomicInt value = new() { Value = 3 };

        int previous = SDL3.SDL.AtomicIncRef(ref value);

        TestAssert.Equal(3, previous, "SDL.AtomicIncRef must return the previous value.");
        TestAssert.Equal(4, value.Value, "SDL.AtomicIncRef must increment the atomic value.");
        AssertMacroAttribute(nameof(SDL3.SDL.AtomicIncRef));
    }

    public static void AtomicDecRef_DecrementsAndReportsZeroTransition()
    {
        SDL3.SDL.AtomicInt reachesZero = new() { Value = 1 };
        SDL3.SDL.AtomicInt remainsPositive = new() { Value = 2 };

        bool zero = SDL3.SDL.AtomicDecRef(ref reachesZero);
        bool positive = SDL3.SDL.AtomicDecRef(ref remainsPositive);

        TestAssert.Equal(true, zero, "SDL.AtomicDecRef must report a zero transition.");
        TestAssert.Equal(0, reachesZero.Value, "SDL.AtomicDecRef must decrement the value that reaches zero.");
        TestAssert.Equal(false, positive, "SDL.AtomicDecRef must report non-zero transitions as false.");
        TestAssert.Equal(1, remainsPositive.Value, "SDL.AtomicDecRef must decrement non-zero values.");
        AssertMacroAttribute(nameof(SDL3.SDL.AtomicDecRef));
    }

    public static void MemoryBarrierRelease_CallsFunctionAndKeepsMacroAttribute()
    {
        SDL3.SDL.MemoryBarrierRelease();

        AssertMacroAttribute(nameof(SDL3.SDL.MemoryBarrierRelease));
    }

    public static void MemoryBarrierAcquire_CallsFunctionAndKeepsMacroAttribute()
    {
        SDL3.SDL.MemoryBarrierAcquire();

        AssertMacroAttribute(nameof(SDL3.SDL.MemoryBarrierAcquire));
    }

    private static void AssertMacroAttribute(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);

        TestAssert.NotNull(method, $"SDL.{methodName} must exist.");
        TestAssert.True(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>() is not null, $"SDL.{methodName} must keep MacroAttribute.");
    }
}
