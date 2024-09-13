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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchDevices(out int count);
    /// <code>extern SDL_DECLSPEC SDL_TouchID *SDLCALL SDL_GetTouchDevices(int *count);</code>
    /// <summary>
    /// <para>Get a list of registered touch devices.</para>
    /// <para>On some platforms SDL first sees the touch device if it was actually used.
    /// Therefore the returned list might be empty, although devices are available.
    /// After using all devices at least once the number will be correct.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of devices returned, can be <c>NULL</c>.</param>
    /// <returns>a 0 terminated array of touch device IDs which should be freed with <see cref="Free"/>,
    /// or <c>NULL</c> on error; call <see cref="GetError"/> for more details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static ulong[]? GetTouchDevices(out int count)
    {
        var touchPtr = SDL_GetTouchDevices(out count);
        if (touchPtr == IntPtr.Zero) return null;

        try
        {
            var sensorIds = new ulong[count];
            for (var i = 0; i < count; i++)
            {
                sensorIds[i] = (ulong)Marshal.ReadInt64(touchPtr, i * sizeof(uint));
            }

            return sensorIds;
        }
        finally
        {
            Free(touchPtr);
        }
    }


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchDeviceName(ulong touchID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetTouchDeviceName(SDL_TouchID touchID);</code>
    /// <summary>
    /// <para>Get the touch device name as reported from the driver.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="touchID">the touch device instance ID.</param>
    /// <returns>touch device name, or <c>NULL</c> on error; call <see cref="GetError"/> for more details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string? GetTouchDeviceName(ulong touchID) => Marshal.PtrToStringAnsi(SDL_GetTouchDeviceName(touchID));
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);
    /// <code>extern SDL_DECLSPEC SDL_TouchDeviceType SDLCALL SDL_GetTouchDeviceType(SDL_TouchID touchID);</code>
    /// <summary>
    /// <para>Get the type of the given touch device.</para>
    /// </summary>
    /// <param name="touchID">the ID of a touch device.</param>
    /// <returns>touch device type.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static TouchDeviceType GetTouchDeviceType(ulong touchID) => SDL_GetTouchDeviceType(touchID);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTouchFingers(ulong touchID, out int count);
    /// <code>extern SDL_DECLSPEC SDL_Finger **SDLCALL SDL_GetTouchFingers(SDL_TouchID touchID, int *count);</code>
    /// <summary>
    /// <para>Get a list of active fingers for a given touch device.</para>
    /// </summary>
    /// <param name="touchID">the ID of a touch device.</param>
    /// <param name="count">a pointer filled in with the number of fingers returned, can be <c>NULL</c>.</param>
    /// <returns>a <c>NULL</c> terminated array of <see cref="Finger"/> pointers which should be freed with
    /// <see cref="Free"/>, or <c>NULL</c> on error; call <see cref="GetError"/> for more details.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Finger[]? GetTouchFingers(ulong touchID, out int count)
    {
        var fingersPtr = SDL_GetTouchFingers(touchID, out count);

        if (fingersPtr == IntPtr.Zero) return null;

        if (count == 0) return [];

        try
        {
            var fingers = new Finger[count];
            for (var i = 0; i < count; i++)
            {
                var fingerPtr = Marshal.ReadIntPtr(fingersPtr, i * IntPtr.Size);
                fingers[i] = Marshal.PtrToStructure<Finger>(fingerPtr);
            }

            return fingers;
        }
        finally
        {
            Free(fingersPtr);
        }
    }
}