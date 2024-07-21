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
            Marshal.FreeHGlobal(pArray);
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
            Marshal.FreeHGlobal(displayModesPtr);
        }
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        [MarshalAs(SDLBool)] bool includeHighDensityModes);
    public static DisplayMode? GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        bool includeHighDensityModes) =>
        Marshal.PtrToStructure<DisplayMode>(
            SDL_GetClosestFullscreenDisplayMode(displayID, w, h, refreshRate, includeHighDensityModes));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDesktopDisplayMode(uint displayID);
    public static DisplayMode? GetDesktopDisplayMode(uint displayID) =>
        Marshal.PtrToStructure<DisplayMode>(SDL_GetDesktopDisplayMode(displayID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentDisplayMode(uint displayID);
    public static DisplayMode? GetCurrentDisplayMode(uint displayID) =>
        Marshal.PtrToStructure<DisplayMode>(SDL_GetCurrentDisplayMode(displayID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForPoint(in Point point);
    public static uint GetDisplayForPoint(in Point point) => SDL_GetDisplayForPoint(point);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayForRect(in Rect rect);
    public static uint GetDisplayForRect(in Rect rect) => SDL_GetDisplayForRect(rect);
    
    
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
    public static DisplayMode? GetWindowFullscreenMode(Window window) =>
        Marshal.PtrToStructure<DisplayMode>(SDL_GetWindowFullscreenMode(window.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindowICCProfile(IntPtr window, out nuint size);
    public static byte[]? GetWindowICCProfile(Window window)
    {
        var profilePtr = SDL_GetWindowICCProfile(window.Handle, out var size);
        
        if (profilePtr == IntPtr.Zero) return null;

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
    private static partial PixelFormat SDL_GetWindowPixelFormat(IntPtr window);
    public static PixelFormat GetWindowPixelFormat(Window window) => SDL_GetWindowPixelFormat(window.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetWindows(out int count);
    public static Window[]? GetWindows(out int count)
    {
        var windowsPtr = SDL_GetWindows(out count);
        if (windowsPtr == IntPtr.Zero) return null;

        try
        {
            var windowPtrs = new IntPtr[count];
            Marshal.Copy(windowsPtr, windowPtrs, 0, count);
            var windows = new Window[count];
            for (var i = 0; i < count; i++)
            {
                windows[i] = new Window(windowPtrs[i]);
            }
            return windows;
        }
        finally
        {
            Free(windowsPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreatePopupWindow(IntPtr parent, int offsetX, int offsetY, int w, int h, 
        WindowFlags flags);
    public static Window CreatePopupWindow(Window parent, int offsetX, int offsetY, int w, int h, WindowFlags flags) =>
        new(SDL_CreatePopupWindow(parent.Handle, offsetX, offsetY, w, h, flags));
    
    
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