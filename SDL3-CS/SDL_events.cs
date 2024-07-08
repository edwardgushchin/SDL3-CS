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
    /// The structure for all events in SDL.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Event
    {
        [FieldOffset(0)]
        public EventType Type;
        
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