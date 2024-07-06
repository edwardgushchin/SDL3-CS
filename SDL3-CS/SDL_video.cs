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
    /// <summary>
    /// The flags on a window.
    /// </summary>
    [Flags]
    public enum WindowFlags : ulong
    {
        /// <summary>
        /// Window is in fullscreen mode
        /// </summary>
        Fullscreen = 0x0000000000000001,
        
        /// <summary>
        /// Window usable with OpenGL context
        /// </summary>
        OpenGL = 0x0000000000000002,
        
        /// <summary>
        /// Window is occluded
        /// </summary>
        Occluded = 0x0000000000000004,
        
        /// <summary>
        /// Window is neither mapped onto the desktop nor shown in the taskbar/dock/window list;
        /// <see cref="ShowWindow"/> is required for it to become visible
        /// </summary>
        Hidden = 0x0000000000000008,
        
        /// <summary>
        /// No window decoration
        /// </summary>
        Borderless = 0x0000000000000010,
        
        /// <summary>
        /// Window can be resized
        /// </summary>
        Resizable = 0x0000000000000020,
        
        /// <summary>
        /// Window is minimized
        /// </summary>
        Minimized = 0x0000000000000040,
        
        /// <summary>
        /// Window is maximized
        /// </summary>
        Maximized = 0x0000000000000080,
        
        /// <summary>
        /// Window has grabbed mouse input
        /// </summary>
        MouseGrabbed = 0x0000000000000100,
        
        /// <summary>
        /// Window has input focus
        /// </summary>
        InputFocus = 0x0000000000000200,
        
        /// <summary>
        /// Window has mouse focus
        /// </summary>
        MouseFocus = 0x0000000000000400,
        
        /// <summary>
        /// Window not created by SDL
        /// </summary>
        External = 0x0000000000000800,
        
        /// <summary>
        /// Window is modal
        /// </summary>
        Modal = 0x0000000000001000,
        
        /// <summary>
        /// Window uses high pixel density back buffer if possible
        /// </summary>
        HighPixelDensity = 0x0000000000002000,
        
        /// <summary>
        /// Window has mouse captured (unrelated to <see cref="MouseGrabbed"/>)
        /// </summary>
        MouseCapture = 0x0000000000004000,
        
        /// <summary>
        /// Window should always be above others
        /// </summary>
        AlwaysOnTop = 0x0000000000008000,
        
        /// <summary>
        /// Window should be treated as a utility window, not showing in the task bar and window list
        /// </summary>
        Utility = 0x0000000000020000,
        
        /// <summary>
        /// Window should be treated as a tooltip and does not get mouse or keyboard focus, requires a parent window
        /// </summary>
        Tooltip = 0x0000000000040000,
        
        /// <summary>
        /// Window should be treated as a popup menu, requires a parent window
        /// </summary>
        PopupMenu = 0x0000000000080000,
        
        /// <summary>
        /// Window has grabbed keyboard input
        /// </summary>
        KeyboardGrabbed = 0x0000000000100000,
        
        /// <summary>
        /// Window usable for Vulkan surface
        /// </summary>
        Vulkan = 0x0000000010000000,
        
        /// <summary>
        /// Window usable for Metal view
        /// </summary>
        Metal = 0x0000000020000000,
        
        /// <summary>
        /// Window with transparent buffer
        /// </summary>
        Transparent = 0x0000000040000000,
        
        /// <summary>
        /// Window should not be focusable
        /// </summary>
        NotFocusable = 0x0000000080000000
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_CreateWindow(byte* title, int w, int h, WindowFlags flags);
    /// <summary>
    /// Create a window with the specified dimensions and flags.
    /// </summary>
    /// <param name="title">the title of the window, in UTF-8 encoding.</param>
    /// <param name="w">the width of the window.</param>
    /// <param name="h">the height of the window.</param>
    /// <param name="flags">0, or one or more <see cref="WindowFlags"/> OR'd together.</param>
    /// <returns>
    /// Returns the window that was created or <see cref="IntPtr.Zero"/> on failure;
    /// call <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// The Window is implicitly shown if <see cref="WindowFlags.Hidden"/> is not set.
    /// On Apple's macOS, you must set the NSHighResolutionCapable Info.plist property to YES,
    /// otherwise you will not receive a High-DPI OpenGL canvas.
    /// The window pixel size may differ from its window coordinate size if the window is on a high pixel density
    /// display. Use <see cref="GetWindowSize"/> to query the client area's size in window coordinates, and
    /// <see cref="GetWindowSizeInPixels"/> or <see cref="GetRenderOutputSize"/> to query the drawable size in pixels.
    /// Note that the drawable size can vary after the window is created and should be queried again if you get an
    /// <see cref="EventType.WindowPixelSizeChanged"/> event.
    /// If the window is created with any of the <see cref="WindowFlags.OpenGL"/> or <see cref="WindowFlags.Vulkan"/>
    /// flags, then the corresponding LoadLibrary function (SDL_GL_LoadLibrary or SDL_Vulkan_LoadLibrary) is called
    /// and the corresponding UnloadLibrary function is called by <see cref="DestroyWindow"/>.
    /// If <see cref="WindowFlags.Vulkan"/> is specified and there isn't a working Vulkan driver,
    /// <see cref="CreateWindow"/> will fail because SDL_Vulkan_LoadLibrary() will fail.
    /// If <see cref="WindowFlags.Metal"/> is specified on an OS that does not support Metal,
    /// <see cref="CreateWindow"/> will fail.
    /// If you intend to use this window with an SDL_Renderer, you should use <see cref="CreateWindowAndRenderer"/>
    /// instead of this function, to avoid window flicker.
    /// On non-Apple devices, SDL requires you to either not link to the Vulkan loader or link to a dynamic
    /// library version. This limitation may be removed in a future version of SDL.
    /// </remarks>
    public static unsafe IntPtr CreateWindow(string? title, int w, int h, WindowFlags flags) {
        var utf8TitleBufSize = Utf8Size(title);
        var utf8Title = stackalloc byte[utf8TitleBufSize];
        return SDL_CreateWindow(Utf8Encode(title, utf8Title, utf8TitleBufSize), w, h, flags);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_DestroyWindow(IntPtr window);
    /// <summary>
    /// Destroy a window.
    /// </summary>
    /// <param name="window">the window to destroy.</param>
    /// <remarks>
    /// Any popups or modal windows owned by the window will be recursively destroyed as well.
    /// </remarks>
    public static void DestroyWindow(IntPtr window) => SDL_DestroyWindow(window);
}