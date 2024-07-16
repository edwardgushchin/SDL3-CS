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

namespace SDL3;

public static partial class SDL
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Event
    {
        [FieldOffset(0)] public EventType Type;
        [FieldOffset(0)] public CommonEvent Common;
        [FieldOffset(0)] public DisplayEvent Display;
        [FieldOffset(0)] public WindowEvent Window;
        [FieldOffset(0)] public KeyboardDeviceEvent KDevice;
        [FieldOffset(0)] public KeyboardEvent Key;
        [FieldOffset(0)] public TextEditingEvent Edit;
        [FieldOffset(0)] public TextEditingCandidatesEvent EditCandidates;
        [FieldOffset(0)] public TextInputEvent Text;
        [FieldOffset(0)] public MouseDeviceEvent MDevice;
        [FieldOffset(0)] public MouseMotionEvent Motion;
        [FieldOffset(0)] public MouseButtonEvent Button;
        [FieldOffset(0)] public MouseWheelEvent Wheel;
        [FieldOffset(0)] public JoyDeviceEvent JDevice;
        [FieldOffset(0)] public JoyAxisEvent JAxis;
        [FieldOffset(0)] public JoyBallEvent JBall;
        [FieldOffset(0)] public JoyHatEvent JHat;
        [FieldOffset(0)] public JoyButtonEvent JButton;
        [FieldOffset(0)] public JoyBatteryEvent JBattery;
        [FieldOffset(0)] public GamepadDeviceEvent GDevice;
        [FieldOffset(0)] public GamepadAxisEvent GAxis;
        [FieldOffset(0)] public GamepadButtonEvent GButton;
        [FieldOffset(0)] public GamepadTouchpadEvent GTouchpad;
        [FieldOffset(0)] public GamepadSensorEvent GSensor;
        [FieldOffset(0)] public AudioDeviceEvent ADevice;
        [FieldOffset(0)] public CameraDeviceEvent CDevice;
        [FieldOffset(0)] public SensorEvent Sensor;
        [FieldOffset(0)] public QuitEvent Quit;
        [FieldOffset(0)] public UserEvent User;
        [FieldOffset(0)] public TouchFingerEvent TFinger;
        [FieldOffset(0)] public PenTipEvent PTip;
        [FieldOffset(0)] public PenMotionEvent PMotion;
        [FieldOffset(0)] public PenButtonEvent PButton;
        [FieldOffset(0)] public DropEvent Drop;
        [FieldOffset(0)] public ClipboardEvent Clipboard;
        [FieldOffset(0)] private unsafe fixed byte padding[128];
    }
}