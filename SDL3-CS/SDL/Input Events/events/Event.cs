#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// The structure for all events in SDL.
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Explicit)]
    public struct Event
    {
        /// <summary>
        /// Event type, shared with all events, Uint32 to cover user events which are not in the SDL_EventType enumeration
        /// </summary>
        [FieldOffset(0)] public UInt32 Type;
        
        /// <summary>
        /// Common event data
        /// </summary>
        [FieldOffset(0)] public CommonEvent Common;
        
        /// <summary>
        /// Display event data
        /// </summary>
        [FieldOffset(0)] public DisplayEvent Display;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        /// <summary>
        /// Window event data
        /// </summary>
        [FieldOffset(0)] public WindowEvent Window;
        
        /// <summary>
        /// Keyboard device change event data
        /// </summary>
        [FieldOffset(0)] public KeyboardDeviceEvent KDevice;
        
        /// <summary>
        /// Keyboard event data
        /// </summary>
        [FieldOffset(0)] public KeyboardEvent Key;
        
        /// <summary>
        /// Text editing event data
        /// </summary>
        [FieldOffset(0)] public TextEditingEvent Edit;
        
        /// <summary>
        /// Text editing candidates event data
        /// </summary>
        [FieldOffset(0)] public TextEditingCandidatesEvent EditCandidates;
        
        /// <summary>
        /// Text input event data
        /// </summary>
        [FieldOffset(0)] public TextInputEvent Text;
        
        /// <summary>
        /// Mouse device change event data
        /// </summary>
        [FieldOffset(0)] public MouseDeviceEvent MDevice;
        
        /// <summary>
        /// Mouse motion event data
        /// </summary>
        [FieldOffset(0)] public MouseMotionEvent Motion;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        /// <summary>
        /// Mouse button event data
        /// </summary>
        [FieldOffset(0)] public MouseButtonEvent Button;
        
        /// <summary>
        /// Mouse wheel event data
        /// </summary>
        [FieldOffset(0)] public MouseWheelEvent Wheel;
        
        /// <summary>
        /// Joystick device change event data
        /// </summary>
        [FieldOffset(0)] public JoyDeviceEvent JDevice;
        
        /// <summary>
        /// Joystick axis event data
        /// </summary>
        [FieldOffset(0)] public JoyAxisEvent JAxis;
        
        /// <summary>
        /// Joystick ball event data
        /// </summary>
        [FieldOffset(0)] public JoyBallEvent JBall;
        
        /// <summary>
        /// Joystick hat event data
        /// </summary>
        [FieldOffset(0)] public JoyHatEvent JHat;
        
        /// <summary>
        /// Joystick button event data
        /// </summary>
        [FieldOffset(0)] public JoyButtonEvent JButton;
        
        /// <summary>
        /// Joystick battery event data
        /// </summary>
        [FieldOffset(0)] public JoyBatteryEvent JBattery;
        
        /// <summary>
        /// Gamepad device event data
        /// </summary>
        [FieldOffset(0)] public GamepadDeviceEvent GDevice;
        
        /// <summary>
        /// Gamepad axis event data
        /// </summary>
        [FieldOffset(0)] public GamepadAxisEvent GAxis;
        
        /// <summary>
        /// Gamepad button event data
        /// </summary>
        [FieldOffset(0)] public GamepadButtonEvent GButton;
        
        /// <summary>
        /// Gamepad touchpad event data
        /// </summary>
        [FieldOffset(0)] public GamepadTouchpadEvent GTouchpad;
        
        /// <summary>
        /// Gamepad sensor event data
        /// </summary>
        [FieldOffset(0)] public GamepadSensorEvent GSensor;
        
        /// <summary>
        /// Audio device event data
        /// </summary>
        [FieldOffset(0)] public AudioDeviceEvent ADevice;
        
        /// <summary>
        /// Camera device event data
        /// </summary>
        [FieldOffset(0)] public CameraDeviceEvent CDevice;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        /// <summary>
        /// Sensor event data
        /// </summary>
        [FieldOffset(0)] public SensorEvent Sensor;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        /// <summary>
        /// Quit request event data
        /// </summary>
        [FieldOffset(0)] public QuitEvent Quit;
        
        /// <summary>
        /// Custom event data
        /// </summary>
        [FieldOffset(0)] public UserEvent User;
        
        /// <summary>
        /// Touch finger event data
        /// </summary>
        [FieldOffset(0)] public TouchFingerEvent TFinger;

        /// <summary>
        /// Pen proximity event data
        /// </summary>
        [FieldOffset(0)] public PenProximityEvent PProximity;
        
        /// <summary>
        /// Pen tip touching event data
        /// </summary>
        [FieldOffset(0)] public PenTouchEvent PTouch;
        
        /// <summary>
        /// Pen change in position, pressure, or angle
        /// </summary>
        [FieldOffset(0)] public PenMotionEvent PMotion;
        
        /// <summary>
        /// Pen button press
        /// </summary>
        [FieldOffset(0)] public PenButtonEvent PButton;
        
        /// <summary>
        /// Pen axis event data
        /// </summary>
        [FieldOffset(0)] public PenAxisEvent PAxis;

        /// <summary>
        /// Render event data
        /// </summary>
        [FieldOffset(0)] public RenderEvent Render;
        
        /// <summary>
        /// Drag and drop event data
        /// </summary>
        [FieldOffset(0)] public DropEvent Drop;
        
        /// <summary>
        /// Clipboard event data
        /// </summary>
        [FieldOffset(0)] public ClipboardEvent Clipboard;
        
        /// <summary>
        /// This is necessary for ABI compatibility between Visual C++ and GCC.
        /// Visual C++ will respect the push pack pragma and use 52 bytes (size of
        /// SDL_TextEditingEvent, the largest structure for 32-bit and 64-bit
        /// architectures) for this union, and GCC will use the alignment of the
        /// largest datatype within the union, which is 8 bytes on 64-bit
        /// architectures.
        ///
        /// So... we'll add padding to force the size to be the same for both.
        ///
        /// On architectures where pointers are 16 bytes, this needs rounding up to
        /// the next multiple of 16, 64, and on architectures where pointers are
        /// even larger the size of SDL_UserEvent will dominate as being 3 pointers.
        /// </summary>
        [FieldOffset(0)] private unsafe fixed byte padding[128];
    }
}