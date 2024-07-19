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
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
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
    public static ulong PenCapability(int capbit)
    {
        return 1ul << capbit;
    }

    
    public static ulong PenAxisCapability(int axis)
    {
        return PenCapability(axis + PenFlagAxisBitOffset);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPens(out int count);
    public static uint GetPens(out int count) => SDL_GetPens(out count);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPenStatus(uint instanceID, out float x, out float y, out float axes,
        ulong numAxes);
    public static uint GetPenStatus(uint instanceID, out float x, out float y, out float axes, ulong numAxes) =>
        SDL_GetPenStatus(instanceID, out x, out y, out axes, numAxes);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPenFromGUID(GUID guid);
    public static uint GetPenFromGUID(GUID guid) => SDL_GetPenFromGUID(guid);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GetPenGUID(uint instanceID);
    public static GUID GetPenGUID(uint instanceID) => SDL_GetPenGUID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_PenConnected(uint instanceID);
    public static bool PenConnected(uint instanceID) => SDL_PenConnected(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPenName(uint instanceID);
    public static string? GetPenName(uint instanceID) => Marshal.PtrToStringAnsi(SDL_GetPenName(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PenCapabilityFlags SDL_GetPenCapabilities(uint instanceID,
        out PenCapabilityInfo capabilities);
    public static PenCapabilityFlags GetPenCapabilities(uint instanceID, out PenCapabilityInfo capabilities) =>
        SDL_GetPenCapabilities(instanceID, out capabilities);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PenSubtype SDL_GetPenType(uint instanceID);
    public static PenSubtype GetPenType(uint instanceID) => SDL_GetPenType(instanceID);
}