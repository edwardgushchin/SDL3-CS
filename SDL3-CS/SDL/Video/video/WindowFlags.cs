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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>The flags on a window.</para>
    /// <para>These cover a lot of true/false, or on/off, window state. Some of it is
    /// immutable after being set through SDL_CreateWindow(), some of it can be
    /// changed on existing windows by the app, and some of it might be altered by
    /// the user or system outside of the app's control.</para>
    /// </summary>
    /// <seealso cref="GetWindowFlags"/>
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
}