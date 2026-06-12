#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTouchDevices"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchDevices(out int count);
    private delegate IntPtr GetTouchDevicesNativeDelegate(out int count);
    private static GetTouchDevicesNativeDelegate GetTouchDevicesNativeFunction = SDL_GetTouchDevices;

    /// <code>extern SDL_DECLSPEC SDL_TouchID * SDLCALL SDL_GetTouchDevices(int *count);</code>
    /// <summary>
    /// <para>Get a list of registered touch devices.</para>
    /// <para>On some platforms SDL first sees the touch device if it was actually used.
    /// Therefore the returned list might be empty, although devices are available.
    /// After using all devices at least once the number will be correct.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of devices returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of touch device IDs or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. This should be freed with
    /// <see cref="Free"/> when it is no longer needed.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static ulong[]? GetTouchDevices(out int count)
    {
        var ptr = GetTouchDevicesNativeFunction(out count);

        try
        {
            return PointerToStructureArray<ulong>(ptr, count);
        }
        finally
        {
            if(ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTouchDeviceName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchDeviceName(ulong touchID);
    private delegate IntPtr GetTouchDeviceNameNativeDelegate(ulong touchID);
    private static GetTouchDeviceNameNativeDelegate GetTouchDeviceNameNativeFunction = SDL_GetTouchDeviceName;

    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetTouchDeviceName(SDL_TouchID touchID);</code>
    /// <summary>
    /// Get the touch device name as reported from the driver.
    /// </summary>
    /// <param name="touchID">the touch device instance ID.</param>
    /// <returns>touch device name, or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetTouchDeviceName(ulong touchID)
    {
        var value = GetTouchDeviceNameNativeFunction(touchID);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTouchDeviceType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);
    private delegate TouchDeviceType GetTouchDeviceTypeNativeDelegate(ulong touchID);
    private static GetTouchDeviceTypeNativeDelegate GetTouchDeviceTypeNativeFunction = SDL_GetTouchDeviceType;

    /// <code>extern SDL_DECLSPEC SDL_TouchDeviceType SDLCALL SDL_GetTouchDeviceType(SDL_TouchID touchID);</code>
    /// <summary>
    /// Get the type of the given touch device.
    /// </summary>
    /// <param name="touchID">the ID of a touch device.</param>
    /// <returns>touch device type.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static TouchDeviceType GetTouchDeviceType(ulong touchID)
    {
        return GetTouchDeviceTypeNativeFunction(touchID);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTouchFingers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchFingers(ulong touchID, out int count);
    private delegate IntPtr GetTouchFingersNativeDelegate(ulong touchID, out int count);
    private static GetTouchFingersNativeDelegate GetTouchFingersNativeFunction = SDL_GetTouchFingers;

    /// <code>extern SDL_DECLSPEC SDL_Finger ** SDLCALL SDL_GetTouchFingers(SDL_TouchID touchID, int *count);</code>
    /// <summary>
    /// Get a list of active fingers for a given touch device.
    /// </summary>
    /// <param name="touchID">the ID of a touch device.</param>
    /// <param name="count">a pointer filled in with the number of fingers returned, can
    /// be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of <see cref="Finger"/> pointers or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This is a single
    /// allocation that should be freed with <see cref="Free"/> when it is no
    /// longer needed.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static Finger[]? GetTouchFingers(ulong touchID, out int count)
    {
        var ptr = GetTouchFingersNativeFunction(touchID, out count);

        try
        {
            return PointerToStructureArray<Finger>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }
}
