﻿#region License
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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VirtualJoystickUpdateCallback(IntPtr userData);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int VirtualJoysticRumbleCallback(IntPtr userData, ushort lowFrequencyRumble, 
        ushort highFrequencyRumble);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int VirtualJoysticRumbleTriggersCallback(IntPtr userData, ushort leftRumble, ushort rightRumble);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int VirtualJoysticSendEffectCallback(IntPtr userData, IntPtr data, int size);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int VirtualJoysticSetLEDCallback(IntPtr userData, byte red, byte green, byte blue);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VirtualJoysticSetPlayerIndexCallback(IntPtr userData, int playerIndex);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int VirtualJoysticSetSensorsEnabledCallback(IntPtr userData, bool enabled);
}