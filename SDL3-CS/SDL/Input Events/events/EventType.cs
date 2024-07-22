#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

namespace SDL3;

public static partial class SDL
{
    public enum EventType
    {
        First = 0,
        Quit = 0x100,
        Terminating,
        LowMemory,
        WillEnterBackground,
        DidEnterBackground,
        WillEnterForeground,
        DidEnterForeground,
        LocaleChanged,
        SystemThemeChanged,
        DisplayOrientation = 0x151,
        DisplayAdded,
        DisplayRemoved,
        DisplayMoved,
        DisplayDesktopModeChanged,
        DisplayCurrentModeChanged,
        DisplayContentScaleChanged,
        DisplayFirst = DisplayOrientation,
        DisplayLast = DisplayContentScaleChanged,
        WindowShown = 0x202,
        WindowHidden,
        WindowExposed,
        WindowMoved,
        WindowResized,
        WindowPixelSizeChanged,
        WindowMinimized,
        WindowMaximized,
        WindowRestored,
        WindowMouseEnter,
        WindowMouseLeave,
        WindowFocusGained,
        WindowFocusLost,
        WindowCloseRequested,
        WindowHitTest,
        WindowIccProfChanged,
        WindowDisplayChanged,
        WindowDisplayScaleChanged,
        WindowOccluded,
        WindowEnterFullscreen,
        WindowLeaveFullscreen,
        WindowDestroyed,
        WindowPenEnter,
        WindowPenLeave,
        WindowHDRStateChanged,
        WindowFirst = WindowShown,
        WindowLast = WindowHDRStateChanged,
        KeyDown = 0x300,
        KeyUp,
        TextEditing,
        TextInput,
        KeymapChanged,
        KeyboardAdded,
        KeyboardRemoved,
        TextEditingCandidates,
        MouseMotion = 0x400,
        MouseButtonDown,
        MouseButtonUp,
        MouseWheel,
        MouseAdded,
        MouseRemoved,
        JoystickAxisMotion  = 0x600,
        JoystickBallMotion,
        JoystickHatMotion,
        JoystickButtonDown,
        JoystickButtonUp,
        JoystickAdded,
        JoystickRemoved,
        JoystickBatteryUpdated,
        JoystickUpdateComplete,
        GamepadAxisMotion = 0x650,
        GamepadButtonDown,
        GamepadButtonUp,
        GamepadAdded,
        GamepadRemoved,
        GamepadRemapped,
        GamepadTouchpadDown,
        GamepadTouchpadMotion,
        GamepadTouchpadUp,
        GamepadSensorUpdate,
        GamepadUpdateComplete,
        GamepadSteamHandleUpdated,
        FingerDown      = 0x700,
        FingerUp,
        FingerMotion,
        ClipboardUpdate = 0x900,
        DropFile = 0x1000,
        DropText,
        DropBegin,
        DropComplete,
        DropPosition,
        AudioDeviceAdded = 0x1100,
        AudioDeviceRemoved,
        AudioDeviceFormatChanged,
        SensorUpdate = 0x1200,
        PenDown = 0x1300,
        PenUp,
        PenMotion,
        PenButtonDown,
        PenButtonUp,
        CameraDeviceAdded = 0x1400,
        CameraDeviceRemoved,
        CameraDeviceApproved,
        CameraDeviceDenied,
        RenderTargetsReset = 0x2000,
        RenderDeviceReset,
        PollSentinel = 0x7F00,
        User = 0x8000,
        Last = 0xFFFF,
        EnumPadding = 0x7FFFFFFF
    }
}