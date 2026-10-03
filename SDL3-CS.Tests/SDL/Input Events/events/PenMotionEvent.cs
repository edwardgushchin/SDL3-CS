using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PenMotionEventTests
{
    public static void DeviceType_MatchesNativeLayout()
    {
        FieldInfo? field = typeof(SDL3.SDL.PenMotionEvent).GetField(nameof(SDL3.SDL.PenMotionEvent.DeviceType));
        TestAssert.NotNull(field, "SDL.PenMotionEvent.DeviceType must be public.");
        TestAssert.Equal(typeof(SDL3.SDL.PenDeviceType), field!.FieldType, "SDL.PenMotionEvent.DeviceType must use SDL.PenDeviceType.");
        TestAssert.Equal(36, Marshal.OffsetOf<SDL3.SDL.PenMotionEvent>(nameof(SDL3.SDL.PenMotionEvent.DeviceType)).ToInt32(), "SDL.PenMotionEvent.DeviceType must keep the SDL 3.4.18 native offset.");
        TestAssert.Equal(40, Marshal.SizeOf<SDL3.SDL.PenMotionEvent>(), "SDL.PenMotionEvent must match SDL 3.4.18 native size.");
    }
}
