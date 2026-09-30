using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Notification;

internal static class PropsTests
{
    public static void NotificationProps_MatchNativeNames()
    {
        TestAssert.Equal("SDL.notification.header_icon", SDL3.SDL.Props.GlobalNotificationHeaderIconString, "Global notification header icon property must match SDL.");
        TestAssert.Equal("SDL.notification.actions", SDL3.SDL.Props.NotificationActionsPointer, "Notification actions property must match SDL.");
        TestAssert.Equal("SDL.notification.action_count", SDL3.SDL.Props.NotificationActionCountNumber, "Notification action count property must match SDL.");
        TestAssert.Equal("SDL.notification.image", SDL3.SDL.Props.NotificationImagePointer, "Notification image property must match SDL.");
        TestAssert.Equal("SDL.notification.message", SDL3.SDL.Props.NotificationMessageString, "Notification message property must match SDL.");
        TestAssert.Equal("SDL.notification.priority", SDL3.SDL.Props.NotificationPriorityNumber, "Notification priority property must match SDL.");
        TestAssert.Equal("SDL.notification.replaces", SDL3.SDL.Props.NotificationReplacesNumber, "Notification replace property must match SDL.");
        TestAssert.Equal("SDL.notification.sound", SDL3.SDL.Props.NotificationSoundString, "Notification sound property must match SDL.");
        TestAssert.Equal("SDL.notification.transient", SDL3.SDL.Props.NotificationTransientBoolean, "Notification transient property must match SDL.");
        TestAssert.Equal("SDL.notification.title", SDL3.SDL.Props.NotificationTitleString, "Notification title property must match SDL.");
    }
}
