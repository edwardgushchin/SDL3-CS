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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetTouchDevices(out int count);
    public static ulong GetTouchDevices(out int count) => SDL_GetTouchDevices(out count);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchDeviceName(ulong touchID);
    public static string? GetTouchDeviceName(ulong touchID) => Marshal.PtrToStringAnsi(SDL_GetTouchDeviceName(touchID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);
    public static TouchDeviceType GetTouchDeviceType(ulong touchID) => SDL_GetTouchDeviceType(touchID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchFingers(ulong touchID, out int count);

    public static Finger[] GetTouchFingers(ulong touchID, out int count)
    {
        var fingersPtr = SDL_GetTouchFingers(touchID, out count);

        if (fingersPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return [];
        }

        try
        {
            var sizeOfFinger = Marshal.SizeOf<Finger>();
            var fingers = new Finger[count];

            for (var i = 0; i < count; i++)
            {
                var currentPtr = fingersPtr + i * sizeOfFinger;
                fingers[i]  = Marshal.PtrToStructure<Finger>(currentPtr);
            }
            
            return fingers;
        }
        finally
        {
            Free(fingersPtr);
        }
    }
    
}