using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PenProximityEventTests
{
    public static void PenState_MatchesNativeLayout()
    {
        FieldInfo? field = typeof(SDL3.SDL.PenProximityEvent).GetField(nameof(SDL3.SDL.PenProximityEvent.PenState));
        TestAssert.NotNull(field, "SDL.PenProximityEvent.PenState must be public.");
        TestAssert.Equal(typeof(SDL3.SDL.PenInputFlags), field!.FieldType, "SDL.PenProximityEvent.PenState must use SDL.PenInputFlags.");
        TestAssert.Equal(24, Marshal.OffsetOf<SDL3.SDL.PenProximityEvent>(nameof(SDL3.SDL.PenProximityEvent.PenState)).ToInt32(), "SDL.PenProximityEvent.PenState must keep the native offset.");
        TestAssert.Equal(32, Marshal.SizeOf<SDL3.SDL.PenProximityEvent>(), "SDL.PenProximityEvent must match SDL 3.4.16 native size.");
    }
}
