using System.Runtime.InteropServices;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PinchFingerEventTests
{
    public static void RunAll()
    {
        TestAssert.Equal(40, Marshal.SizeOf<SDL3.SDL.PinchFingerEvent>(), "Pinch event must include all four appended native floats.");
        TestAssert.Equal(24, Marshal.OffsetOf<SDL3.SDL.PinchFingerEvent>(nameof(SDL3.SDL.PinchFingerEvent.SpanX)).ToInt32(), "Pinch span must follow stable prefix.");
        TestAssert.Equal(36, Marshal.OffsetOf<SDL3.SDL.PinchFingerEvent>(nameof(SDL3.SDL.PinchFingerEvent.FocusY)).ToInt32(), "Pinch focus must preserve native offset.");
        SDL3.SDL.PinchFingerEvent e = new() { SpanX = 12, SpanY = 14, FocusX = 18, FocusY = 20 };
        TestAssert.Equal(12f, e.SpanX, "Pinch X span must remain readable.");
        TestAssert.Equal(14f, e.SpanY, "Pinch Y span must remain readable.");
        TestAssert.Equal(18f, e.FocusX, "Pinch X focus must remain readable.");
        TestAssert.Equal(20f, e.FocusY, "Pinch Y focus must remain readable.");
    }
}
