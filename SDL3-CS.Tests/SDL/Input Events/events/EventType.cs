using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class EventTypeTests
{
    public static void GroupBoundaries_MatchSupportedEvents()
    {
        AssertBoundary(SDL3.SDL.EventType.KeyboardFirst, SDL3.SDL.EventType.KeyDown, SDL3.SDL.EventType.KeyboardLast, SDL3.SDL.EventType.ScreenKeyboardHidden, "keyboard");
        AssertBoundary(SDL3.SDL.EventType.MouseFirst, SDL3.SDL.EventType.MouseMotion, SDL3.SDL.EventType.MouseLast, SDL3.SDL.EventType.MouseRemoved, "mouse");
        AssertBoundary(SDL3.SDL.EventType.JoystickFirst, SDL3.SDL.EventType.JoystickAxisMotion, SDL3.SDL.EventType.JoystickLast, SDL3.SDL.EventType.JoystickUpdateComplete, "joystick");
        AssertBoundary(SDL3.SDL.EventType.GamepadFirst, SDL3.SDL.EventType.GamepadAxisMotion, SDL3.SDL.EventType.GamepadLast, SDL3.SDL.EventType.GamepadSteamHandleUpdated, "gamepad");
        AssertBoundary(SDL3.SDL.EventType.FingerFirst, SDL3.SDL.EventType.FingerDown, SDL3.SDL.EventType.FingerLast, SDL3.SDL.EventType.FingerCanceled, "finger");
        AssertBoundary(SDL3.SDL.EventType.PinchFirst, SDL3.SDL.EventType.PinchBegin, SDL3.SDL.EventType.PinchLast, SDL3.SDL.EventType.PinchEnd, "pinch");
        AssertBoundary(SDL3.SDL.EventType.ClipboardFirst, SDL3.SDL.EventType.ClipboardUpdate, SDL3.SDL.EventType.ClipboardLast, SDL3.SDL.EventType.ClipboardUpdate, "clipboard");
        AssertBoundary(SDL3.SDL.EventType.DropFirst, SDL3.SDL.EventType.DropFile, SDL3.SDL.EventType.DropLast, SDL3.SDL.EventType.DropPosition, "drop");
        AssertBoundary(SDL3.SDL.EventType.AudioDeviceFirst, SDL3.SDL.EventType.AudioDeviceAdded, SDL3.SDL.EventType.AudioDeviceLast, SDL3.SDL.EventType.AudioDeviceFormatChanged, "audio device");
        AssertBoundary(SDL3.SDL.EventType.SensorFirst, SDL3.SDL.EventType.SensorUpdate, SDL3.SDL.EventType.SensorLast, SDL3.SDL.EventType.SensorUpdate, "sensor");
        AssertBoundary(SDL3.SDL.EventType.PenFirst, SDL3.SDL.EventType.PenProximityIn, SDL3.SDL.EventType.PenLast, SDL3.SDL.EventType.PenAxis, "pen");
        AssertBoundary(SDL3.SDL.EventType.CameraDeviceFirst, SDL3.SDL.EventType.CameraDeviceAdded, SDL3.SDL.EventType.CameraDeviceLast, SDL3.SDL.EventType.CameraDeviceDenied, "camera device");
        AssertBoundary(SDL3.SDL.EventType.RenderFirst, SDL3.SDL.EventType.RenderTargetsReset, SDL3.SDL.EventType.RenderLast, SDL3.SDL.EventType.RenderDeviceLost, "render");
    }

    private static void AssertBoundary(SDL3.SDL.EventType first, SDL3.SDL.EventType expectedFirst, SDL3.SDL.EventType last, SDL3.SDL.EventType expectedLast, string group)
    {
        TestAssert.Equal(expectedFirst, first, $"SDL.EventType {group} first alias must match its first event.");
        TestAssert.Equal(expectedLast, last, $"SDL.EventType {group} last alias must match its last supported event.");
    }
}
