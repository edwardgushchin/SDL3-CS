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
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumVideoDrivers();
    public static int GetNumVideoDrivers() => SDL_GetNumVideoDrivers();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetVideoDriver(int index);
    public static string? GetVideoDriver(int index) => Marshal.PtrToStringUTF8(SDL_GetVideoDriver(index));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentVideoDriver();
    public static string? GetCurrentVideoDriver() => Marshal.PtrToStringUTF8(SDL_GetCurrentVideoDriver());
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme SDL_GetSystemTheme();
    public static SystemTheme GetSystemTheme() => SDL_GetSystemTheme();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDisplays(out int count);

    public static uint[] GetDisplays(out int count)
    {
        var pArray = SDL_GetDisplays(out count);
        try
        {
            var displayArray = new int[count];
            Marshal.Copy(pArray, displayArray, 0, count);
            return Array.ConvertAll(displayArray, item => (uint)item);
        }
        finally
        {
            Free(pArray);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPrimaryDisplay();
    public static uint GetPrimaryDisplay() => SDL_GetPrimaryDisplay();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayProperties(uint displayID);
    public static uint GetDisplayProperties(uint displayID) => SDL_GetDisplayProperties(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDisplayName(uint displayID);
    public static string? GetDisplayName(uint displayID) => Marshal.PtrToStringUTF8(SDL_GetDisplayName(displayID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDisplayBounds(uint displayID, out Rect rect);
    public static int GetDisplayBounds(uint displayID, out Rect rect) => SDL_GetDisplayBounds(displayID, out rect);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetDisplayUsableBounds(uint displayID, out Rect rect);
    public static int GetDisplayUsableBounds(uint displayID, out Rect rect) => SDL_GetDisplayUsableBounds(displayID, out rect);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayID);
    public static DisplayOrientation GetNaturalDisplayOrientation(uint displayID) => SDL_GetNaturalDisplayOrientation(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayID);
    public static DisplayOrientation GetCurrentDisplayOrientation(uint displayID) => SDL_GetCurrentDisplayOrientation(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetDisplayContentScale(uint displayID);
    public static float GetDisplayContentScale(uint displayID) => SDL_GetDisplayContentScale(displayID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetFullscreenDisplayModes(uint displayID, out int count);
    public static DisplayMode[] GetFullscreenDisplayModes(uint displayID, out int count)
    {
        var displayModesPtr = SDL_GetFullscreenDisplayModes(displayID, out count);

        if (displayModesPtr == IntPtr.Zero || count == 0)
        {
            count = 0;
            return [];
        }

        try
        {
            var displayModes = new DisplayMode[count];
            for (var i = 0; i < count; i++)
            {
                var modePtr = Marshal.ReadIntPtr(displayModesPtr, i * IntPtr.Size);
                displayModes[i] = Marshal.PtrToStructure<DisplayMode>(modePtr);
            }

            return displayModes;
        }
        finally
        {
            Free(displayModesPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate, [MarshalAs(UnmanagedType.I1)] bool includeHighDensityModes);
    public static DisplayMode? GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate, bool includeHighDensityModes)
    {
        var displayModePtr = SDL_GetClosestFullscreenDisplayMode(displayID, w, h, refreshRate, includeHighDensityModes);

        if (displayModePtr == IntPtr.Zero)
        {
            return null;
        }
        
        return Marshal.PtrToStructure<DisplayMode>(displayModePtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDesktopDisplayMode(uint displayID);
    public static DisplayMode? GetDesktopDisplayMode(uint displayID)
    {
        var displayModePtr = SDL_GetDesktopDisplayMode(displayID);

        if (displayModePtr == IntPtr.Zero)
        {
            return null;
        }
        
        return Marshal.PtrToStructure<DisplayMode>(displayModePtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentDisplayMode(uint displayID);
    public static DisplayMode? GetCurrentDisplayMode(uint displayID)
    {
        var displayModePtr = SDL_GetCurrentDisplayMode(displayID);

        if (displayModePtr == IntPtr.Zero)
        {
            return null;
        }
        
        return Marshal.PtrToStructure<DisplayMode>(displayModePtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForPoint(ref Point point);
    public static uint GetDisplayForPoint(Point point) => SDL_GetDisplayForPoint(ref point);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForRect(ref Rect rect);
    public static uint GetDisplayForRect(Rect rect) => SDL_GetDisplayForRect(ref rect);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForWindow(IntPtr window);
    public static uint GetDisplayForWindow(Window window) => SDL_GetDisplayForWindow(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowPixelDensity(IntPtr window);
    public static float GetWindowPixelDensity(Window window) => SDL_GetWindowPixelDensity(window.Handle);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetWindowDisplayScale(IntPtr window);
    public static float GetWindowDisplayScale(Window window) => SDL_GetWindowDisplayScale(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetWindowFullscreenMode(IntPtr window, in DisplayMode mode);
    public static int SetWindowFullscreenMode(Window window, DisplayMode mode) =>
        SDL_SetWindowFullscreenMode(window.Handle, in mode);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowFullscreenMode(IntPtr window);
    public static DisplayMode? GetWindowFullscreenMode(Window window)
    {
        var modePtr = SDL_GetWindowFullscreenMode(window.Handle);
        
        if (modePtr == IntPtr.Zero)
        {
            return null;
        }
        
        return Marshal.PtrToStructure<DisplayMode>(modePtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowICCProfile(IntPtr window, out nuint size);
    public static byte[] GetWindowICCProfile(Window window)
    {
        var profilePtr = SDL_GetWindowICCProfile(window.Handle, out var size);

        try
        {
            var profileData = new byte[(int)size];
            Marshal.Copy(profilePtr, profileData, 0, (int)size);
            return profileData;
        }
        finally
        {
            Free(profilePtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateWindow([MarshalAs(UnmanagedType.LPUTF8Str)] string title, int w, int h, 
        WindowFlags flags);
    public static Window CreateWindow(string title, int w, int h, WindowFlags flags) =>
        new(SDL_CreateWindow(title, w, h, flags));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(IntPtr window);
    public static void DestroyWindow(Window window) => SDL_DestroyWindow(window.Handle);
}