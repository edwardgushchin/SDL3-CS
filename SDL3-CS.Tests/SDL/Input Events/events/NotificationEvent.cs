using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class NotificationEventTests
{
    public static void NotificationEvent_UsesExpectedNativeLayoutAndEventUnion()
    {
        TestAssert.Equal(0x1500u, (uint)SDL3.SDL.EventType.NotificationActionInvoked, "SDL notification event type must preserve native value.");
        TestAssert.Equal(IntPtr.Size == 8 ? 32 : 24, Marshal.SizeOf<SDL3.SDL.NotificationEvent>(), "SDL.NotificationEvent must preserve native size.");
        TestAssert.Equal(IntPtr.Size == 8 ? 24 : 20, Marshal.OffsetOf<SDL3.SDL.NotificationEvent>(nameof(SDL3.SDL.NotificationEvent.ActionId)).ToInt32(), "SDL.NotificationEvent action ID pointer must preserve native offset.");
        SDL3.SDL.Event value = default;
        value.Notification = new SDL3.SDL.NotificationEvent
        {
            Type = SDL3.SDL.EventType.NotificationActionInvoked,
            Which = 0xA301u,
            ActionId = (IntPtr)0xA302
        };
        TestAssert.Equal((uint)SDL3.SDL.EventType.NotificationActionInvoked, value.Common.Type, "SDL.Event must expose notification event type.");
        TestAssert.Equal(0xA301u, value.Notification.Which, "SDL.Event must preserve notification ID.");
        TestAssert.Equal((IntPtr)0xA302, value.Notification.ActionId, "SDL.Event must preserve notification action ID pointer.");
    }
}
