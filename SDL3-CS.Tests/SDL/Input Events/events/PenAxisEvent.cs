using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PenAxisEventTests
{
    public static void DeviceType_MatchesNativeLayout()
    {
        FieldInfo? field = typeof(SDL3.SDL.PenAxisEvent).GetField(nameof(SDL3.SDL.PenAxisEvent.DeviceType));
        TestAssert.NotNull(field, "SDL.PenAxisEvent.DeviceType must be public.");
        TestAssert.Equal(typeof(SDL3.SDL.PenDeviceType), field!.FieldType, "SDL.PenAxisEvent.DeviceType must use SDL.PenDeviceType.");
        TestAssert.Equal(44, Marshal.OffsetOf<SDL3.SDL.PenAxisEvent>(nameof(SDL3.SDL.PenAxisEvent.DeviceType)).ToInt32(), "SDL.PenAxisEvent.DeviceType must keep the SDL 3.4.18 native offset.");
        TestAssert.Equal(48, Marshal.SizeOf<SDL3.SDL.PenAxisEvent>(), "SDL.PenAxisEvent must match SDL 3.4.18 native size.");
    }
}
