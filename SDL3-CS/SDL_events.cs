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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// The types of events that can be delivered.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Unused (do not remove)
        /// </summary>
        First = 0,

        // Application events
        /// <summary>
        /// User-requested quit
        /// </summary>
        Quit = 0x100,

        /// <summary>
        /// <para>The application is being terminated by the OS</para>
        /// <para>Called on iOS in applicationWillTerminate()</para>
        /// <para>Called on Android in onDestroy()</para>
        /// </summary>
        Terminating,
        
        /// <summary>
        /// <para>The application is low on memory, free memory if possible.</para>
        /// <para>Called on iOS in applicationDidReceiveMemoryWarning()</para>
        /// <para>Called on Android in onLowMemory()</para>
        /// </summary>
        LowMemory,
        
        /// <summary>
        /// <para>The application is about to enter the background</para>
        /// <para>Called on iOS in applicationWillResignActive()</para>
        /// <para>Called on Android in onPause()</para>
        /// </summary>
        WillEnterBackground,
        
        /// <summary>
        /// <para>The application did enter the background and may not get CPU for some time</para>
        /// <para>Called on iOS in applicationDidEnterBackground()</para>
        /// <para>Called on Android in onPause()</para>
        /// </summary>
        DidEnterBackground,
        
        /// <summary>
        /// <para>The application is about to enter the foreground</para>
        /// <para>Called on iOS in applicationWillEnterForeground()</para>
        /// <para>Called on Android in onResume()</para>
        /// </summary>
        WillEnterForeground,
        
        /// <summary>
        /// <para>The application is now interactive</para>
        /// <para>Called on iOS in applicationDidBecomeActive()</para>
        /// <para>Called on Android in onResume()</para>
        /// </summary>
        DidEnterForeground,

        /// <summary>
        /// The user's locale preferences have changed
        /// </summary>
        LocaleChanged,

        /// <summary>
        /// The system theme changed
        /// </summary>
        SystemThemeChanged,

        // Display events
        //0x150 was SDL_DISPLAYEVENT, reserve the number for sdl2-compat
        
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
        /// Display has changed content scale
        /// </summary>
        DisplayContentScaleChanged,
        
        /// <summary>
        /// First display event.
        /// </summary>
        DisplayFirst = DisplayOrientation,
        
        /// <summary>
        /// Last display event.
        /// </summary>
        DisplayLast = DisplayContentScaleChanged,

        // Window events
        // 0x200 was SDL_WINDOWEVENT, reserve the number for sdl2-compat
        // 0x201 was SDL_EVENT_SYSWM, reserve the number for sdl2-compat
        
        /// <summary>
        /// Window has been shown
        /// </summary>
        WindowShown = 0x202,
        
        /// <summary>
        /// Window has been hidden
        /// </summary>
        WindowHidden,
        
        /// <summary>
        /// Window has been exposed and should be redrawn,
        /// and can be redrawn directly from event watchers for this event
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
        /// Window had a hit test that wasn't SDL_HITTEST_NORMAL
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
        WindowHdrStateChanged,
        
        /// <summary>
        /// First window event
        /// </summary>
        WindowFirst = WindowShown,
        
        /// <summary>
        /// Last window event
        /// </summary>
        WindowLast = WindowPenLeave,

        //Keyboard events
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
        /// Keymap changed due to a system event such as an input language or keyboard layout change.
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

        // Mouse events
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

        // Joystick events
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

        // Gamepad events
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

        // Touch events
        /// <summary>
        /// A finger has touched the screen
        /// </summary>
        FingerDown      = 0x700,
        
        /// <summary>
        /// A finger has been lifted off the screen
        /// </summary>
        FingerUp,
        
        /// <summary>
        /// A finger is moving on the screen
        /// </summary>
        FingerMotion,

        //0x800, 0x801, and 0x802 were the Gesture events from SDL2. Do not reuse these values! sdl2-compat needs them!

        //Clipboard events
        /// <summary>
        /// The clipboard or primary selection changed
        /// </summary>
        ClipboardUpdate = 0x900,

        //Drag and drop events
        
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

        // Audio hotplug events
        /// <summary>
        /// A new audio device is available
        /// </summary>
        AudioDeviceAdded = 0x1100,
        
        /// <summary>
        /// An audio device has been removed
        /// </summary>
        AudioDeviceRemoved,
        
        /// <summary>
        /// An audio device's format has been changed by the system
        /// </summary>
        AudioDeviceFormatChanged,

        // Sensor events
        /// <summary>
        /// A sensor was updated
        /// </summary>
        SensorUpdate = 0x1200,

        // Pressure-sensitive pen events
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

        // Camera hotplug events
        /// <summary>
        /// A new camera device is available
        /// </summary>
        CameraDeviceAdded = 0x1400,
        
        /// <summary>
        /// A camera device has been removed
        /// </summary>
        CameraDeviceRemoved,
        
        /// <summary>
        /// A camera device has been approved for use by the user
        /// </summary>
        CameraDeviceApproved,
        
        /// <summary>
        /// A camera device has been denied for use by the user
        /// </summary>
        CameraDeviceDenied,

        // Render events
        /// <summary>
        /// The render targets have been reset and their contents need to be updated
        /// </summary>
        RenderTargetsReset = 0x2000,
        
        /// <summary>
        /// The device has been reset and all textures need to be recreated
        /// </summary>
        RenderDeviceReset,

        // Internal events
        
        /// <summary>
        /// Signals the end of an event poll cycle
        /// </summary>
        PollSentinel = 0x7F00,
        
        /// <summary>
        /// Events <see cref="User"/> through <see cref="Last"/> are for your use,
        /// and should be allocated with SDL_RegisterEvents()
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
    
    
    /// <summary>
    /// General keyboard/mouse state definitions
    /// </summary>
    public enum Keystate : byte
    {
        Pressed = 1,
        Released = 0
    }
    
    
    /// <summary>
    /// Audio device event structure (event.adevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioDeviceEvent
    {
        /// <summary>
        /// <see cref="EventType.AudioDeviceAdded"/>, or <see cref="EventType.AudioDeviceRemoved"/>,
        /// or <see cref="EventType.AudioDeviceFormatChanged"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// SDL_AudioDeviceID for the device being added or removed or changing
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// zero if a playback device, non-zero if a recording device.
        /// </summary>
        public byte Recording;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
    }
    
    
    /// <summary>
    /// Camera device event structure (event.cdevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CameraDeviceEvent
    {
        /// <summary>
        /// <see cref="EventType.CameraDeviceAdded"/>, <see cref="EventType.CameraDeviceRemoved"/>,
        /// <see cref="EventType.CameraDeviceApproved"/>, <see cref="EventType.CameraDeviceDenied"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// SDL_CameraDeviceID for the device being added or removed or changing
        /// </summary>
        public UInt32 Which;
    }

    
    /// <summary>
    /// An event triggered when the clipboard contents have changed (event.clipboard.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ClipboardEvent
    {
        /// <summary>
        /// <see cref="EventType.ClipboardUpdate"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
    }
    
    
    /// <summary>
    /// Fields shared by every event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CommonEvent
    {
        /// <summary>
        /// Event type, shared with all events, Uint32 to cover user events which are not in the
        /// <see cref="EventType"/> enumeration
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
    }
    
    
    /// <summary>
    /// //Display state change event data (event.display.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayEvent
    {
        /// <summary>
        /// <see cref="EventType.DisplayAdded"/>, <see cref="EventType.DisplayFirst"/>,
        /// <see cref="EventType.DisplayOrientation"/>, <see cref="EventType.DisplayMoved"/>,
        /// <see cref="EventType.DisplayLast"/>, <see cref="EventType.DisplayRemoved"/> or
        /// <see cref="EventType.DisplayContentScaleChanged"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The associated display
        /// </summary>
        public UInt32 DisplayID;
        
        /// <summary>
        /// event dependent data
        /// </summary>
        public Int32 Data1;
    }


    /// <summary>
    /// An event used to drop text or request a file open by the system (event.drop.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DropEvent
    {
        /// <summary>
        /// <see cref="EventType.DropBegin"/>, <see cref="EventType.DropFile"/>, <see cref="EventType.DropText"/>,
        /// <see cref="EventType.DropComplete"/>,  <see cref="EventType.DropPosition"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The window that was dropped on, if any
        /// </summary>
        public UInt32 WindowID;
        
        /// <summary>
        /// X coordinate, relative to window (not on begin)
        /// </summary>
        public float X;
        
        /// <summary>
        /// Y coordinate, relative to window (not on begin)
        /// </summary>
        public float Y;
        
        /// <summary>
        /// The source app that sent this drop event, or NULL if that isn't available
        /// </summary>
        public IntPtr Source;
        
        /// <summary>
        /// The text for <see cref="EventType.DropText"/> and the file name for <see cref="EventType.DropFile"/>,
        /// NULL for other events
        /// </summary>
        public IntPtr Data;
    }

    
    /// <summary>
    /// Gamepad axis motion event structure (event.gaxis.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadAxisEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadAxisMotion"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The gamepad axis (<see cref="GamepadAxis"/>)
        /// </summary>
        public byte Axis;
        public byte Padding1;
        public byte Padding2;
        public byte Padding3;
        
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public Int16 Value;
        public UInt16 Padding4;
    }

    
    /// <summary>
    /// Gamepad button event structure (event.gbutton.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadButtonEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadButtonDown"/> or <see cref="EventType.GamepadButtonUp"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The gamepad button (<see cref="GamepadButton"/>)
        /// </summary>
        public byte Button;
        
        /// <summary>
        /// <see cref="Keystate.Pressed"/> or <see cref="Keystate.Released"/>
        /// </summary>
        public Keystate State;
        public byte Padding1;
        public byte Padding2;
    }
    
    
    /// <summary>
    /// Gamepad device event structure (event.gdevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadDeviceEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadAdded"/>, <see cref="EventType.GamepadRemoved"/>, or
        /// <see cref="EventType.GamepadRemapped"/>, <see cref="EventType.GamepadUpdateComplete"/> or
        /// <see cref="EventType.GamepadSteamHandleUpdated"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id 
        /// </summary>
        public UInt32 Which;
    }


    /// <summary>
    /// Gamepad sensor event structure (event.gsensor.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GamepadSensorEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadSensorUpdate"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The type of the sensor, one of the values of <see cref="SDL_SensorType"/>
        /// </summary>
        public Int32 Sensor;
        
        /// <summary>
        /// Up to 3 values from the sensor, as defined in SDL_sensor.h
        /// </summary>
        public fixed float data[3];
        
        public float[] Data
        {
            get
            {
                fixed (float* ptr = data)
                {
                    var array = new float[3];
                    Marshal.Copy((IntPtr)ptr, array, 0, 3);
                    return array;
                }
            }
        }
        
        /// <summary>
        /// The timestamp of the sensor reading in nanoseconds, not necessarily synchronized with the system clock
        /// </summary>
        public UInt64 SensorTimestamp;
    }


    /// <summary>
    /// Gamepad touchpad event structure (event.gtouchpad.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadTouchpadEvent
    {
        /// <summary>
        /// <see cref="EventType.GamepadTouchpadDown"/> or <see cref="EventType.GamepadTouchpadMotion"/> or
        /// <see cref="EventType.GamepadTouchpadUp"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The index of the touchpad
        /// </summary>
        public Int32 Touchpad;
        
        /// <summary>
        /// The index of the finger on the touchpad
        /// </summary>
        public Int32 Finger;
        
        /// <summary>
        /// Normalized in the range 0...1 with 0 being on the left
        /// </summary>
        public float X;
        
        /// <summary>
        /// Normalized in the range 0...1 with 0 being at the top
        /// </summary>
        public float Y;
        
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public float Pressure;
    }
    
    /// <summary>
    /// Joystick axis motion event structure (event.jaxis.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyAxisEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickAxisMotion"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The joystick axis index
        /// </summary>
        public byte Axis; 
        public byte Padding1;
        public byte Padding2;
        public byte Padding3;
        
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public Int16 Value;
        public UInt16 Padding4;
    }
    
    
    /// <summary>
    /// Joystick trackball motion event structure (event.jball.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBallEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickBallMotion"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which; 
        
        /// <summary>
        /// The joystick trackball index
        /// </summary>
        public byte Ball;
        public byte Padding1;
        public byte Padding2;
        public byte Padding3;
        
        /// <summary>
        /// The relative motion in the X direction
        /// </summary>
        public Int16 XRel;
        
        /// <summary>
        /// The relative motion in the Y direction
        /// </summary>
        public Int16 YRel;
    }
    
    
    /// <summary>
    /// Joysick battery level change event structure (event.jbattery.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBatteryEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickBatteryUpdated"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp; 
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The joystick battery state
        /// </summary>
        public PowerState State;
        
        /// <summary>
        /// The joystick battery percent charge remaining
        /// </summary>
        public int Percent;
    }
    
    
    /// <summary>
    /// Joystick button event structure (event.jbutton.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyButtonEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickButtonDown"/> or <see cref="EventType.JoystickButtonUp"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which; 
        
        /// <summary>
        /// The joystick button index
        /// </summary>
        public byte Button;
        
        /// <summary>
        /// <see cref="Keystate.Pressed"/> or <see cref="Keystate.Released"/>
        /// </summary>
        public Keystate State;
        public byte Padding1;
        public byte Padding2;
    }
    
    
    /// <summary>
    /// Joystick device event structure (event.jdevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyDeviceEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickAdded"/> or <see cref="EventType.JoystickRemoved"/>
        /// or <see cref="EventType.JoystickUpdateComplete"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
    }
    
    
    /// <summary>
    /// Joystick hat position change event structure (event.jhat.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        /// <summary>
        /// <see cref="EventType.JoystickHatMotion"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// The joystick hat index
        /// </summary>
        public byte Hat;
        
        /// <summary>
        /// The hat position value.
        /// sa <see cref="JoystickHat.LeftUp"/> <see cref="JoystickHat.Up"/> <see cref="JoystickHat.RightUp"/>
        /// sa <see cref="JoystickHat.Left"/> <see cref="JoystickHat.Centered"/> <see cref="JoystickHat.Right"/>
        /// sa <see cref="JoystickHat.LeftDown"/> <see cref="JoystickHat.Down"/> <see cref="JoystickHat.RightDown"/>
        /// Note that zero means the POV is centered.
        /// </summary>
        public JoystickHat Value;
        public byte Padding1;
        public byte Padding2;
    }
    
    
    /// <summary>
    /// Keyboard device event structure (event.kdevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardDeviceEvent
    {
        /// <summary>
        /// <see cref="EventType.KeyboardAdded"/> or <see cref="EventType.KeyboardRemoved"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The keyboard instance id
        /// </summary>
        public UInt32 Which;
    }
    
    
    /// <summary>
    /// Keyboard button event structure (event.key.*)
    /// </summary>
    /// <remarks>
    /// The key is the base <see cref="Keycode"/> generated by pressing the scancode using the current keyboard layout,
    /// applying any options specified in <see cref="HintKeycodeOptions"/>. You can get the <see cref="Keycode"/>
    /// corresponding to the event scancode and modifiers directly from the keyboard layout,
    /// bypassing <see cref="HintKeycodeOptions"/>, by calling <see cref="GetKeyFromScancode"/>.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardEvent
    {
        /// <summary>
        /// <see cref="EventType.KeyDown"/> or <see cref="EventType.KeyUp"/>
        /// </summary>
        public EventType Type;
        public UInt32 Reserved;
        
        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;
        
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public UInt32 WindowID;
        
        /// <summary>
        /// The keyboard instance id, or 0 if unknown or virtual
        /// </summary>
        public UInt32 Which;
        
        /// <summary>
        /// SDL physical key code
        /// </summary>
        public Scancode Scancode;
        
        /// <summary>
        /// SDL virtual key code
        /// </summary>
        public Keycode Key;
        
        /// <summary>
        /// current key modifiers
        /// </summary>
        public Keymod Mod;
        
        /// <summary>
        /// The platform dependent scancode for this event 
        /// </summary>
        public UInt16 Raw;
        
        /// <summary>
        /// <see cref="Keystate.Pressed"/> or <see cref="Keystate.Released"/>
        /// </summary>
        public Keystate State;
        
        /// <summary>
        /// Non-zero if this is a key repeat
        /// </summary>
        public byte Repeat;
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseDeviceEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMotionEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenButtonEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenMotionEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PenTipEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QuitEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SensorEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingCandidatesEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TextInputEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TouchFingerEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct UserEvent
    {
        
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowEvent
    {
        
    }
    
    
    /// <summary>
    /// The structure for all events in SDL.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Event
    {
        /// <summary>
        /// Event type, shared with all events, Uint32 to cover user events which are not in the
        /// <see cref="EventType"/> enumeration
        /// </summary>
        [FieldOffset(0)]
        public EventType Type;
        
        /// <summary>
        /// Common event data
        /// </summary>
        [FieldOffset(0)]
        public CommonEvent Common;
        
        /// <summary>
        /// Display event data
        /// </summary>
        [FieldOffset(0)]
        public DisplayEvent Display;
        
        /// <summary>
        /// Window event data
        /// </summary>
        [FieldOffset(0)]
        public WindowEvent Window;
        
        /// <summary>
        /// Keyboard device change event data
        /// </summary>
        [FieldOffset(0)]
        public KeyboardDeviceEvent KDevice;
        
        /// <summary>
        /// Keyboard event data
        /// </summary>
        [FieldOffset(0)]
        public KeyboardEvent Key;
        
        /// <summary>
        /// Text editing event data
        /// </summary>
        [FieldOffset(0)]
        public TextEditingEvent Edit;
        
        /// <summary>
        /// Text editing candidates event data
        /// </summary>
        [FieldOffset(0)]
        public TextEditingCandidatesEvent EditCandidates;
        
        /// <summary>
        /// Text input event data
        /// </summary>
        [FieldOffset(0)]
        public TextInputEvent Text;
        
        /// <summary>
        /// Mouse device change event data
        /// </summary>
        [FieldOffset(0)]
        public MouseDeviceEvent MDevice;
        
        /// <summary>
        /// Mouse motion event data
        /// </summary>
        [FieldOffset(0)]
        public MouseMotionEvent Motion;
        
        /// <summary>
        /// Mouse button event data
        /// </summary>
        [FieldOffset(0)]
        public MouseButtonEvent Button;
        
        /// <summary>
        /// Mouse wheel event data
        /// </summary>
        [FieldOffset(0)]
        public MouseWheelEvent Wheel;
        
        /// <summary>
        /// Joystick device change event data
        /// </summary>
        [FieldOffset(0)]
        public JoyDeviceEvent JDevice;
        
        /// <summary>
        /// Joystick axis event data
        /// </summary>
        [FieldOffset(0)]
        public JoyAxisEvent JAxis;
        
        /// <summary>
        /// Joystick ball event data
        /// </summary>
        [FieldOffset(0)]
        public JoyBallEvent JBall;
        
        /// <summary>
        /// Joystick hat event data
        /// </summary>
        [FieldOffset(0)]
        public JoyHatEvent JHat;
        
        /// <summary>
        /// Joystick button event data
        /// </summary>
        [FieldOffset(0)]
        public JoyButtonEvent JButton;
        
        /// <summary>
        /// Joystick battery event data
        /// </summary>
        [FieldOffset(0)]
        public JoyBatteryEvent JBattery;
        
        /// <summary>
        /// Gamepad device event data
        /// </summary>
        [FieldOffset(0)]
        public GamepadDeviceEvent GDevice;
        
        /// <summary>
        /// Gamepad axis event data
        /// </summary>
        [FieldOffset(0)]
        public GamepadAxisEvent GAxis;
        
        /// <summary>
        /// Gamepad button event data
        /// </summary>
        [FieldOffset(0)]
        public GamepadButtonEvent GButton;
        
        /// <summary>
        /// Gamepad touchpad event data
        /// </summary>
        [FieldOffset(0)]
        public GamepadTouchpadEvent GTouchpad;
        
        /// <summary>
        /// Gamepad sensor event data
        /// </summary>
        [FieldOffset(0)]
        public GamepadSensorEvent GSensor;
        
        /// <summary>
        /// Audio device event data
        /// </summary>
        [FieldOffset(0)]
        public AudioDeviceEvent ADevice;
        
        /// <summary>
        /// Camera device event data
        /// </summary>
        [FieldOffset(0)]
        public CameraDeviceEvent CDevice;
        
        /// <summary>
        /// Sensor event data
        /// </summary>
        [FieldOffset(0)]
        public SensorEvent Sensor;
        
        /// <summary>
        /// Quit request event data
        /// </summary>
        [FieldOffset(0)]
        public QuitEvent Quit;
        
        /// <summary>
        /// Custom event data
        /// </summary>
        [FieldOffset(0)]
        public UserEvent User;
        
        /// <summary>
        /// Touch finger event data
        /// </summary>
        [FieldOffset(0)]
        public TouchFingerEvent TFinger;
        
        /// <summary>
        /// Pen tip touching or leaving drawing surface
        /// </summary>
        [FieldOffset(0)]
        public PenTipEvent PTip;
        
        /// <summary>
        /// Pen change in position, pressure, or angle
        /// </summary>
        [FieldOffset(0)]
        public PenMotionEvent PMotion;
        
        /// <summary>
        /// Pen button press
        /// </summary>
        [FieldOffset(0)]
        public PenButtonEvent PButton;
        
        /// <summary>
        /// Drag and drop event data
        /// </summary>
        [FieldOffset(0)]
        public DropEvent Drop;
        
        /// <summary>
        /// Clipboard event data
        /// </summary>
        [FieldOffset(0)]
        public ClipboardEvent Clipboard;
        
        /// <summary>
        /// <para>This is necessary for ABI compatibility between Visual C++ and GCC. Visual C++ will respect
        /// the push pack pragma and use 52 bytes (size of SDL_TextEditingEvent, the largest structure for 32-bit
        /// and 64-bit architectures) for this union, and GCC will use the alignment of the largest datatype within
        /// the union, which is 8 bytes on 64-bit architectures.</para>
        /// <para> So... we'll add padding to force the size to be the same for both.</para>
        /// <para>On architectures where pointers are 16 bytes, this needs rounding up to
        /// the next multiple of 16, 64, and on architectures where pointers are
        /// even larger the size of SDL_UserEvent will dominate as being 3 pointers.</para>
        /// </summary>
        [FieldOffset(0)]
        private fixed byte padding[128];
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PollEvent(out Event e);
    
    /// <summary>
    /// Poll for currently pending events.
    /// </summary>
    /// <param name="e">
    /// the <see cref="Event"/> structure to be filled with the next event from the queue, or NULL.
    /// </param>
    /// <returns>
    /// Returns 1 if this got an event or 0 if there are none available.
    /// </returns>
    /// <remarks>
    /// <para>If event is not NULL, the next event is removed from the queue and stored in the <see cref="EventType"/>
    /// structure pointed to by event. The 1 returned refers to this event, immediately stored in the SDL Event
    /// structure -- not an event to follow.</para>
    /// <para>If event is NULL, it simply returns 1 if there is an event in the queue, but will not remove it from
    /// the queue.</para>
    /// <para>As this function may implicitly call <see cref="PumpEvents"/>, you can only call this function in
    /// the thread that set the video mode.</para>
    /// <para><see cref="PollEvent"/> is the favored way of receiving system events since it can be done from the
    /// main loop and does not suspend the main loop while waiting on an event to be posted.</para>
    /// <para>The common practice is to fully process the event queue once every frame, usually as a first step
    /// before updating the game's state</para>
    /// </remarks>
    /// <seealso cref="PushEvent"/>
    /// <seealso cref="WaitEvent"/>
    /// <seealso cref="WaitEventTimeout"/>
    public static int PollEvent(out Event e) => SDL_PollEvent(out e);
}