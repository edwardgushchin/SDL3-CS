#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
 */
#endregion

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// The types of events that can be delivered.
    /// </summary>
    /// <since>This enum is available since SDL 3.0.0.</since>
    public enum EventType
    {
        /// <summary>
        /// Unused (do not remove)
        /// </summary>
        First = 0,
        
        /// <summary>
        /// User-requested quit
        /// </summary>
        Quit = 0x100,
        
        /// <summary>
        /// These application events have special meaning on iOS, see README-ios.md for details
        /// The application is being terminated by the OS
        /// Called on iOS in applicationWillTerminate()
        /// Called on Android in onDestroy()
        /// </summary>
        Terminating,
        
        /// <summary>
        /// The application is low on memory, free memory if possible.
        /// Called on iOS in applicationDidReceiveMemoryWarning()
        /// Called on Android in onLowMemory()
        /// </summary>
        LowMemory,
        
        /// <summary>
        /// The application is about to enter the background
        /// Called on iOS in applicationWillResignActive()
        /// Called on Android in onPause()
        /// </summary>
        WillEnterBackground,
        
        /// <summary>
        /// The application did enter the background and may not get CPU for some time
        /// Called on iOS in applicationDidEnterBackground()
        /// Called on Android in onPause()
        /// </summary>
        DidEnterBackground,
        
        /// <summary>
        /// The application is about to enter the foreground
        /// Called on iOS in applicationWillEnterForeground()
        /// Called on Android in onResume()
        /// </summary>
        WillEnterForeground,
        
        /// <summary>
        /// The application is now interactive
        /// Called on iOS in applicationDidBecomeActive()
        /// Called on Android in onResume()
        /// </summary>
        DidEnterForeground,
        
        /// <summary>
        /// The user's locale preferences have changed.
        /// </summary>
        LocaleChanged,
        
        /// <summary>
        /// The system theme changed
        /// </summary>
        SystemThemeChanged,
        
        /// <summary>
        /// Display orientation has changed to data1
        /// </summary>
        DisplayOrientation = 0x151,
        
        /// <summary>
        /// Display has been added to the system
        /// </summary>
        DisplayAdded,
        
        /// <summary>
        /// Display has been removed from the system
        /// </summary>
        DisplayRemoved,
        
        /// <summary>
        /// Display has changed position
        /// </summary>
        DisplayMoved,
        
        /// <summary>
        /// Display has changed desktop mode
        /// </summary>
        DisplayDesktopModeChanged,
        
        /// <summary>
        /// Display has changed current mode
        /// </summary>
        DisplayCurrentModeChanged,
        
        /// <summary>
        /// Display has changed content scale
        /// </summary>
        DisplayContentScaleChanged,
        DisplayFirst = DisplayOrientation,
        DisplayLast = DisplayContentScaleChanged,
        
        /// <summary>
        /// Window has been shown
        /// </summary>
        WindowShown = 0x202,
        
        /// <summary>
        /// Window has been hidden
        /// </summary>
        WindowHidden,
        
        /// <summary>
        /// Window has been exposed and should be redrawn, and can be redrawn directly from event watchers for this event
        /// </summary>
        WindowExposed,
        
        /// <summary>
        /// Window has been moved to data1, data2
        /// </summary>
        WindowMoved,
        
        /// <summary>
        /// Window has been resized to data1xdata2
        /// </summary>
        WindowResized,
        
        /// <summary>
        /// The pixel size of the window has changed to data1xdata2
        /// </summary>
        WindowPixelSizeChanged,
        
        /// <summary>
        /// Window has been minimized
        /// </summary>
        WindowMinimized,
        
        /// <summary>
        /// Window has been maximized
        /// </summary>
        WindowMaximized,
        
        /// <summary>
        /// Window has been restored to normal size and position
        /// </summary>
        WindowRestored,
        
        /// <summary>
        /// Window has gained mouse focus
        /// </summary>
        WindowMouseEnter,
        
        /// <summary>
        /// Window has lost mouse focus
        /// </summary>
        WindowMouseLeave,
        
        /// <summary>
        /// Window has gained keyboard focus
        /// </summary>
        WindowFocusGained,
        
        /// <summary>
        /// Window has lost keyboard focus
        /// </summary>
        WindowFocusLost,
        
        /// <summary>
        /// The window manager requests that the window be closed
        /// </summary>
        WindowCloseRequested,
        
        /// <summary>
        /// Window had a hit test that wasn't <see cref="HitTestResult.Normal"/>
        /// </summary>
        WindowHitTest,
        
        /// <summary>
        /// The ICC profile of the window's display has changed
        /// </summary>
        WindowIccProfChanged,
        
        /// <summary>
        /// Window has been moved to display data1
        /// </summary>
        WindowDisplayChanged,
        
        /// <summary>
        /// Window display scale has been changed
        /// </summary>
        WindowDisplayScaleChanged,
        
        /// <summary>
        /// The window has been occluded
        /// </summary>
        WindowOccluded,
        
        /// <summary>
        /// The window has entered fullscreen mode
        /// </summary>
        WindowEnterFullscreen,
        
        /// <summary>
        /// The window has left fullscreen mode
        /// </summary>
        WindowLeaveFullscreen,
        
        /// <summary>
        /// The window with the associated ID is being or has been destroyed. If this message is being handled
        /// in an event watcher, the window handle is still valid and can still be used to retrieve any userdata
        /// associated with the window. Otherwise, the handle has already been destroyed and all resources
        /// associated with it are invalid
        /// </summary>
        WindowDestroyed,
        
        /// <summary>
        /// Window has gained focus of the pressure-sensitive pen with ID "data1"
        /// </summary>
        WindowPenEnter,
        
        /// <summary>
        /// Window has lost focus of the pressure-sensitive pen with ID "data1"
        /// </summary>
        WindowPenLeave,
        
        /// <summary>
        /// Window HDR properties have changed
        /// </summary>
        WindowHDRStateChanged,
        WindowFirst = WindowShown,
        WindowLast = WindowHDRStateChanged,
        
        /// <summary>
        /// Key pressed
        /// </summary>
        KeyDown = 0x300,
        
        /// <summary>
        /// Key released
        /// </summary>
        KeyUp,
        
        /// <summary>
        /// Keyboard text editing (composition)
        /// </summary>
        TextEditing,
        
        /// <summary>
        /// Keyboard text input
        /// </summary>
        TextInput,
        
        /// <summary>
        /// Keymap changed due to a system event such as an
        /// input language or keyboard layout change.
        /// </summary>
        KeymapChanged,
        
        /// <summary>
        /// A new keyboard has been inserted into the system
        /// </summary>
        KeyboardAdded,
        
        /// <summary>
        /// A keyboard has been removed
        /// </summary>
        KeyboardRemoved,
        
        /// <summary>
        /// Keyboard text editing candidates
        /// </summary>
        TextEditingCandidates,
        
        /// <summary>
        /// Mouse moved
        /// </summary>
        MouseMotion = 0x400,
        
        /// <summary>
        /// Mouse button pressed
        /// </summary>
        MouseButtonDown,
        
        /// <summary>
        /// Mouse button released
        /// </summary>
        MouseButtonUp,
        
        /// <summary>
        /// Mouse wheel motion
        /// </summary>
        MouseWheel,
        
        /// <summary>
        /// A new mouse has been inserted into the system
        /// </summary>
        MouseAdded,
        
        /// <summary>
        /// A mouse has been removed
        /// </summary>
        MouseRemoved,
        
        /// <summary>
        /// Joystick axis motion
        /// </summary>
        JoystickAxisMotion  = 0x600,
        
        /// <summary>
        /// Joystick trackball motion
        /// </summary>
        JoystickBallMotion,
        
        /// <summary>
        /// Joystick hat position change
        /// </summary>
        JoystickHatMotion,
        
        /// <summary>
        /// Joystick button pressed
        /// </summary>
        JoystickButtonDown,
        
        /// <summary>
        /// Joystick button released
        /// </summary>
        JoystickButtonUp,
        
        /// <summary>
        /// A new joystick has been inserted into the system
        /// </summary>
        JoystickAdded,
        
        /// <summary>
        /// An opened joystick has been removed
        /// </summary>
        JoystickRemoved,
        
        /// <summary>
        /// Joystick battery level change
        /// </summary>
        JoystickBatteryUpdated,
        
        /// <summary>
        /// Joystick update is complete
        /// </summary>
        JoystickUpdateComplete,
        
        /// <summary>
        /// Gamepad axis motion
        /// </summary>
        GamepadAxisMotion = 0x650,
        
        /// <summary>
        /// Gamepad button pressed
        /// </summary>
        GamepadButtonDown,
        
        /// <summary>
        /// Gamepad button released
        /// </summary>
        GamepadButtonUp,
        
        /// <summary>
        /// A new gamepad has been inserted into the system
        /// </summary>
        GamepadAdded,
        
        /// <summary>
        /// A gamepad has been removed
        /// </summary>
        GamepadRemoved,
        
        /// <summary>
        /// The gamepad mapping was updated
        /// </summary>
        GamepadRemapped,
        
        /// <summary>
        /// Gamepad touchpad was touched
        /// </summary>
        GamepadTouchpadDown,
        
        /// <summary>
        /// Gamepad touchpad finger was moved
        /// </summary>
        GamepadTouchpadMotion,
        
        /// <summary>
        /// Gamepad touchpad finger was lifted
        /// </summary>
        GamepadTouchpadUp,
        
        /// <summary>
        /// Gamepad sensor was updated
        /// </summary>
        GamepadSensorUpdate,
        
        /// <summary>
        /// Gamepad update is complete
        /// </summary>
        GamepadUpdateComplete,
        
        /// <summary>
        /// Gamepad Steam handle has changed
        /// </summary>
        GamepadSteamHandleUpdated,
        FingerDown      = 0x700,
        FingerUp,
        FingerMotion,
        
        /// <summary>
        /// The clipboard or primary selection changed
        /// </summary>
        ClipboardUpdate = 0x900,
        
        /// <summary>
        /// The system requests a file open
        /// </summary>
        DropFile = 0x1000,
        
        /// <summary>
        /// text/plain drag-and-drop event
        /// </summary>
        DropText,
        
        /// <summary>
        /// A new set of drops is beginning (NULL filename)
        /// </summary>
        DropBegin,
        
        /// <summary>
        /// Current set of drops is now complete (NULL filename)
        /// </summary>
        DropComplete,
        
        /// <summary>
        /// Position while moving over the window
        /// </summary>
        DropPosition,
        
        /// <summary>
        /// A new audio device is available
        /// </summary>
        AudioDeviceAdded = 0x1100,
        
        /// <summary>
        /// An audio device has been removed.
        /// </summary>
        AudioDeviceRemoved,
        
        /// <summary>
        /// An audio device's format has been changed by the system.
        /// </summary>
        AudioDeviceFormatChanged,
        
        /// <summary>
        /// A sensor was updated
        /// </summary>
        SensorUpdate = 0x1200,
        
        /// <summary>
        /// Pressure-sensitive pen touched drawing surface
        /// </summary>
        PenDown = 0x1300,
        
        /// <summary>
        /// Pressure-sensitive pen stopped touching drawing surface
        /// </summary>
        PenUp,
        
        /// <summary>
        /// Pressure-sensitive pen moved, or angle/pressure changed
        /// </summary>
        PenMotion,
        
        /// <summary>
        /// Pressure-sensitive pen button pressed
        /// </summary>
        PenButtonDown,
        
        /// <summary>
        /// Pressure-sensitive pen button released
        /// </summary>
        PenButtonUp,
        
        /// <summary>
        /// A new camera device is available
        /// </summary>
        CameraDeviceAdded = 0x1400,
        
        /// <summary>
        /// A camera device has been removed.
        /// </summary>
        CameraDeviceRemoved,
        
        /// <summary>
        /// A camera device has been approved for use by the user.
        /// </summary>
        CameraDeviceApproved,
        
        /// <summary>
        /// A camera device has been denied for use by the user.
        /// </summary>
        CameraDeviceDenied,
        
        /// <summary>
        /// The render targets have been reset and their contents need to be updated
        /// </summary>
        RenderTargetsReset = 0x2000,
        
        /// <summary>
        /// The device has been reset and all textures need to be recreated
        /// </summary>
        RenderDeviceReset,
        
        /// <summary>
        /// Signals the end of an event poll cycle
        /// </summary>
        PollSentinel = 0x7F00,
        
        /// <summary>
        /// vents SDL_EVENT_USER through <see cref="Last"/> are for your use,
        /// and should be allocated with <see cref="RegisterEvents"/>
        /// </summary>
        User = 0x8000,
        
        /// <summary>
        /// This last event is only for bounding internal arrays
        /// </summary>
        Last = 0xFFFF,
        
        /// <summary>
        /// This just makes sure the enum is the size of Uint32
        /// </summary>
        EnumPadding = 0x7FFFFFFF
    }
}