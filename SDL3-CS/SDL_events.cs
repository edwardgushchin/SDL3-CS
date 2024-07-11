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
    public enum Keystate : byte
    {
        Pressed = 1,
        Released = 0,
    }
    
    
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
        WindowHdrStateChanged,
        WindowFirst = WindowShown,
        WindowLast = WindowPenLeave,
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
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioDeviceEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public byte Recording;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
    }
    

    [StructLayout(LayoutKind.Sequential)]
    public struct CameraDeviceEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
    }

    
    [StructLayout(LayoutKind.Sequential)]
    public struct ClipboardEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct CommonEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 DisplayID;
        public Int32 Data1;
    }

    
    [StructLayout(LayoutKind.Sequential)]
    public struct DropEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public float X;
        public float Y;
        private IntPtr source;
        private IntPtr data;
        public string Source => UTF8ToManaged(source)!;
        public string Data => UTF8ToManaged(data)!;
    }

    
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadAxisEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public byte Axis;
        public byte Padding1;
        public byte Padding2;
        public byte Padding3;
        public Int16 Value;
        public UInt16 Padding4;
    }

    
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadButtonEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public GamepadButton Button;
        public Keystate State;
        public byte Padding1;
        public byte Padding2;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadDeviceEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GamepadSensorEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public Int32 Sensor;
        private fixed float data[3];
        public UInt64 SensorTimestamp;
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
    }

    
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadTouchpadEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public Int32 Touchpad;
        public Int32 Finger;
        public float X;
        public float Y;
        public float Pressure;
    }
    

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyAxisEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public byte Axis; 
        public byte Padding1;
        public byte Padding2;
        public byte Padding3;
        public Int16 Value;
        public UInt16 Padding4;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBallEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which; 
        public byte Ball;
        public byte Padding1;
        public byte Padding2;
        public byte Padding3;
        public Int16 XRel;
        public Int16 YRel;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBatteryEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp; 
        public UInt32 Which;
        public PowerState State;
        public int Percent;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyButtonEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which; 
        public byte Button;
        public Keystate State;
        public byte Padding1;
        public byte Padding2;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyDeviceEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public byte Hat;
        public JoystickHat Value;
        public byte Padding1;
        public byte Padding2;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardDeviceEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public Scancode Scancode;
        public Keycode Key;
        public Keymod Mod;
        public UInt16 Raw;
        public Keystate State;
        public byte Repeat;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public byte Button; 
        public Keystate State;
        public byte Clicks;
        public byte Padding;
        public float X;
        public float Y;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseDeviceEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMotionEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public MouseButtonFlags State;
        public float X;
        public float Y;
        public float XRel;
        public float YRel;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public float X;
        public float Y;
        public MouseWheelDirection Direction;
        public float MouseX;
        public float MouseY;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct PenButtonEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public byte Button;
        public Keystate State;
        public UInt64 PenState;
        public float X;
        public float Y;
        private unsafe fixed float axes[(int)PenAxis.NumAxes];
        public unsafe float[] Axes
        {
            get
            {
                fixed (float* ptr = axes)
                {
                    var array = new float[(int)PenAxis.NumAxes];
                    Marshal.Copy((IntPtr)ptr, array, 0, (int)PenAxis.NumAxes);
                    return array;
                }
            }
        }
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct PenMotionEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public byte Padding1;
        public byte Padding2;
        public UInt16 PenState;
        public float X;
        public float Y;
        private unsafe fixed float axes[(int)PenAxis.NumAxes];
        public unsafe float[] Axes
        {
            get
            {
                fixed (float* ptr = axes)
                {
                    var array = new float[(int)PenAxis.NumAxes];
                    Marshal.Copy((IntPtr)ptr, array, 0, (int)PenAxis.NumAxes);
                    return array;
                }
            }
        }
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct PenTipEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public UInt32 Which;
        public PenTips Tip;
        public Keystate State;
        public UInt16 PenState;
        public float X;
        public float Y;
        private unsafe fixed float axes[(int)PenAxis.NumAxes];
        public unsafe float[] Axes
        {
            get
            {
                fixed (float* ptr = axes)
                {
                    var array = new float[(int)PenAxis.NumAxes];
                    Marshal.Copy((IntPtr)ptr, array, 0, (int)PenAxis.NumAxes);
                    return array;
                }
            }
        }
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct QuitEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct SensorEvent
    {
        public EventType Type;
        public UInt32 reserved;
        public UInt64 Timestamp;
        public UInt32 Which;
        public unsafe fixed float data[6];
        public UInt64 SensorTimestamp;
        public unsafe float[] Data
        {
            get
            {
                fixed (float* ptr = data)
                {
                    var array = new float[6];
                    Marshal.Copy((IntPtr)ptr, array, 0, 6);
                    return array;
                }
            }
        }
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingCandidatesEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public IntPtr Candidates;
        public Int32 NumCandidates;
        public Int32 SelectedCandidate;
        private int horizontal;
        public bool Horizontal => horizontal != 0;
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingEvent
    {
        
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct TextInputEvent
    {
        
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct TouchFingerEvent
    {
        
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct UserEvent
    {
        
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowEvent
    {
        public EventType Type;
        public UInt32 Reserved;
        public UInt64 Timestamp;
        public UInt32 WindowID;
        public Int32 Data1;
        public Int32 Data2;
    }
    
    
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Event
    {
        [FieldOffset(0)]
        public EventType Type;
        
        [FieldOffset(0)]
        public CommonEvent Common;
        
        [FieldOffset(0)]
        public DisplayEvent Display;
        
        [FieldOffset(0)]
        public WindowEvent Window;
        
        [FieldOffset(0)]
        public KeyboardDeviceEvent KDevice;
        
        [FieldOffset(0)]
        public KeyboardEvent Key;
        
        [FieldOffset(0)]
        public TextEditingEvent Edit;
        
        [FieldOffset(0)]
        public TextEditingCandidatesEvent EditCandidates;
        
        [FieldOffset(0)]
        public TextInputEvent Text;
        
        [FieldOffset(0)]
        public MouseDeviceEvent MDevice;
        
        [FieldOffset(0)]
        public MouseMotionEvent Motion;
        
        [FieldOffset(0)]
        public MouseButtonEvent Button;
        
        [FieldOffset(0)]
        public MouseWheelEvent Wheel;
        
        [FieldOffset(0)]
        public JoyDeviceEvent JDevice;
        
        [FieldOffset(0)]
        public JoyAxisEvent JAxis;
        
        [FieldOffset(0)]
        public JoyBallEvent JBall;
        
        [FieldOffset(0)]
        public JoyHatEvent JHat;
        
        [FieldOffset(0)]
        public JoyButtonEvent JButton;
        
        [FieldOffset(0)]
        public JoyBatteryEvent JBattery;
        
        [FieldOffset(0)]
        public GamepadDeviceEvent GDevice;
        
        [FieldOffset(0)]
        public GamepadAxisEvent GAxis;
        
        [FieldOffset(0)]
        public GamepadButtonEvent GButton;
        
        [FieldOffset(0)]
        public GamepadTouchpadEvent GTouchpad;
        
        [FieldOffset(0)]
        public GamepadSensorEvent GSensor;
        
        [FieldOffset(0)]
        public AudioDeviceEvent ADevice;
        
        [FieldOffset(0)]
        public CameraDeviceEvent CDevice;
        
        [FieldOffset(0)]
        public SensorEvent Sensor;
        
        [FieldOffset(0)]
        public QuitEvent Quit;
        
        [FieldOffset(0)]
        public UserEvent User;
        
        [FieldOffset(0)]
        public TouchFingerEvent TFinger;
        
        [FieldOffset(0)]
        public PenTipEvent PTip;
        
        [FieldOffset(0)]
        public PenMotionEvent PMotion;

        [FieldOffset(0)]
        public PenButtonEvent PButton;

        [FieldOffset(0)]
        public DropEvent Drop;

        [FieldOffset(0)]
        public ClipboardEvent Clipboard;

        [FieldOffset(0)]
        private fixed byte Padding[128];
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PollEvent(out Event e);
    public static int PollEvent(out Event e) => SDL_PollEvent(out e);
}