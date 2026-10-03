using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PenButtonEventTests
{
    public static void DeviceType_MatchesNativeLayout()
    {
        FieldInfo? field = typeof(SDL3.SDL.PenButtonEvent).GetField(nameof(SDL3.SDL.PenButtonEvent.DeviceType));
        TestAssert.NotNull(field, "SDL.PenButtonEvent.DeviceType must be public.");
        TestAssert.Equal(typeof(SDL3.SDL.PenDeviceType), field!.FieldType, "SDL.PenButtonEvent.DeviceType must use SDL.PenDeviceType.");
        TestAssert.Equal(40, Marshal.OffsetOf<SDL3.SDL.PenButtonEvent>(nameof(SDL3.SDL.PenButtonEvent.DeviceType)).ToInt32(), "SDL.PenButtonEvent.DeviceType must keep the SDL 3.4.18 native offset.");
        TestAssert.Equal(48, Marshal.SizeOf<SDL3.SDL.PenButtonEvent>(), "SDL.PenButtonEvent must match SDL 3.4.18 native size.");
    }
}
