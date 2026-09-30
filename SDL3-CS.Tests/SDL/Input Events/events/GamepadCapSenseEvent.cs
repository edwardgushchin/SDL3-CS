using System.Runtime.InteropServices;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class GamepadCapSenseEventTests
{
    public static void RunAll()
    {
        TestAssert.Equal(0x65Cu, (uint)SDL3.SDL.EventType.GamepadCapSenseTouch, "Capsense touch must preserve native event value.");
        TestAssert.Equal(0x65Du, (uint)SDL3.SDL.EventType.GamepadCapSenseRelease, "Capsense release must preserve native event value.");
        TestAssert.Equal(24, Marshal.SizeOf<SDL3.SDL.GamepadCapSenseEvent>(), "Capsense event must preserve native size.");
        TestAssert.Equal(20, Marshal.OffsetOf<SDL3.SDL.GamepadCapSenseEvent>(nameof(SDL3.SDL.GamepadCapSenseEvent.CapSense)).ToInt32(), "Capsense byte must preserve native offset.");
        TestAssert.Equal(21, Marshal.OffsetOf<SDL3.SDL.GamepadCapSenseEvent>(nameof(SDL3.SDL.GamepadCapSenseEvent.Down)).ToInt32(), "Capsense bool must preserve native byte offset.");
        SDL3.SDL.Event e = default;
        e.GCapSense = new() { Type = SDL3.SDL.EventType.GamepadCapSenseTouch, Which = 123, CapSense = (byte)SDL3.SDL.GamepadCapSenseType.LeftGrip, Down = true };
        TestAssert.Equal((uint)SDL3.SDL.EventType.GamepadCapSenseTouch, e.Common.Type, "Capsense event must overlay Event union.");
        TestAssert.Equal(123u, e.GCapSense.Which, "Capsense joystick ID must survive union access.");
        TestAssert.Equal(true, e.GCapSense.Down, "Capsense state must survive union access.");
    }
}
