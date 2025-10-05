#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>Used to indicate that you don't care what the window position is.</para>
    /// <para>If you _really_ don't care, <see cref="WindowPosUndefined"/> is the same, but always
    /// uses the primary display instead of specifying one.</para>
    /// </summary>
    /// <param name="x">the SDL_DisplayID of the display to use.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    [Macro]
    public static uint WindowPosUndefinedDisplay(int x) => WindowPosUndefinedMask | (uint)x;

    
    /// <summary>
    /// <para>Used to indicate that you don't care what the window position/display is.</para>
    /// <para>This always uses the primary display.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    [Macro]
    public static uint WindowPosUndefined() => WindowPosUndefinedDisplay(0);
    
    
    /// <summary>
    /// <para> Used to indicate that you don't care what the window position/display is.</para>
    /// <para>This always uses the primary display.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    [Macro]
    public static bool WindowPosIsUndefined(uint x) => (x & 0xFFFF0000u) == WindowPosUndefinedMask;
    
    
    /// <summary>
    /// <para>Used to indicate that the window position should be centered.</para>
    /// <para><see cref="WindowPosCentered"/> is the same, but always uses the primary display
    /// instead of specifying one.</para>
    /// </summary>
    /// <param name="x">the SDL_DisplayID of the display to use.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    [Macro]
    public static uint WindowPosCenteredDisplay(int x) => WindowPosCenteredMask | (uint)x;
    
    
    /// <summary>
    /// <para>Used to indicate that the window position should be centered.</para>
    /// <para>This always uses the primary display.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    [Macro]
    public static uint WindowPosCentered() => WindowPosCenteredDisplay(0);
    
    
    /// <summary>
    /// A macro to test if the window position is marked as "centered."
    /// </summary>
    /// <param name="x">the window position value.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="GetWindowPosition"/>
    [Macro]
    public static bool WindowPosIsCentered(uint x) => (x & 0xFFFF0000u) == WindowPosCenteredMask;
    
    
    
}