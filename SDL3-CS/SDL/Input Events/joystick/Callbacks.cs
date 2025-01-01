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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VirtualJoystickUpdateCallback(IntPtr userdata);

    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VirtualJoystickSetPlayerIndexCallback(IntPtr userdata, int playerIndex);
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool VirtualJoystickRumbleCallback(IntPtr userdata, ushort lowFrequencyRumble, ushort highFrequencyRumble);
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool VirtualJoystickRumbleTriggersCallback(IntPtr userdata, ushort leftRumble, ushort rightRumble);
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool VirtualJoystickSetLEDCallback(IntPtr userdata, byte red, byte green, byte blue);
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool VirtualJoystickSendEffectCallback(IntPtr userdata, IntPtr data, int size);
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool VirtualJoystickSetSensorsEnabledCallback(IntPtr userdata, bool enabled);
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VirtualJoystickCleanupCallback(IntPtr userdata);
}