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
    [Flags]
    public enum WindowFlags : ulong
    {
        Fullscreen = 0x0000000000000001,
        OpenGL = 0x0000000000000002,
        Occluded = 0x0000000000000004,
        Hidden = 0x0000000000000008,
        Borderless = 0x0000000000000010,
        Resizable = 0x0000000000000020,
        Minimized = 0x0000000000000040,
        Maximized = 0x0000000000000080,
        MouseGrabbed = 0x0000000000000100,
        InputFocus = 0x0000000000000200,
        MouseFocus = 0x0000000000000400,
        External = 0x0000000000000800,
        Modal = 0x0000000000001000,
        HighPixelDensity = 0x0000000000002000,
        MouseCapture = 0x0000000000004000,
        AlwaysOnTop = 0x0000000000008000,
        Utility = 0x0000000000020000,
        Tooltip = 0x0000000000040000,
        PopupMenu = 0x0000000000080000,
        KeyboardGrabbed = 0x0000000000100000,
        Vulkan = 0x0000000010000000,
        Metal = 0x0000000020000000,
        Transparent = 0x0000000040000000,
        NotFocusable = 0x0000000080000000
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)]) ]
    private static unsafe partial IntPtr SDL_CreateWindow(byte* title, int w, int h, WindowFlags flags);
    public static unsafe Window CreateWindow(string title, int w, int h, WindowFlags flags)
    { 
        var utf8StrBufSize = UTF8Size(title); 
        var utf8Str = stackalloc byte[utf8StrBufSize];
        return new Window( SDL_CreateWindow(UTF8Encode(title, utf8Str, utf8StrBufSize), w, h, flags));
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(IntPtr window);
    public static void DestroyWindow(Window window) => SDL_DestroyWindow(window.Handle);
}