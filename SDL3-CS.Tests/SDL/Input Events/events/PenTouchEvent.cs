using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PenTouchEventTests
{
    public static void DeviceType_MatchesNativeLayout()
    {
        FieldInfo? field = typeof(SDL3.SDL.PenTouchEvent).GetField(nameof(SDL3.SDL.PenTouchEvent.DeviceType));
        TestAssert.NotNull(field, "SDL.PenTouchEvent.DeviceType must be public.");
        TestAssert.Equal(typeof(SDL3.SDL.PenDeviceType), field!.FieldType, "SDL.PenTouchEvent.DeviceType must use SDL.PenDeviceType.");
        TestAssert.Equal(40, Marshal.OffsetOf<SDL3.SDL.PenTouchEvent>(nameof(SDL3.SDL.PenTouchEvent.DeviceType)).ToInt32(), "SDL.PenTouchEvent.DeviceType must keep the SDL 3.4.18 native offset.");
        TestAssert.Equal(48, Marshal.SizeOf<SDL3.SDL.PenTouchEvent>(), "SDL.PenTouchEvent must match SDL 3.4.18 native size.");
    }
}
