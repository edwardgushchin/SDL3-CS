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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Used to indicate that you don't care what the window position is.
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    [Macro]
    public static uint WindowPosUndefinedDisplay(int displayIndex) => WindowposUndefinedMask | (uint)displayIndex;

    
    /// <summary>
    /// Used to indicate that you don't care what the window position is.
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    [Macro]
    public static bool WindowPosIsUndefined(uint position) => (position & 0xFFFF0000u) == WindowposUndefinedMask;
    
    
    /// <summary>
    /// Used to indicate that the window position should be centered.
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    [Macro]
    public static uint WindowPosCenteredDisplay(int displayIndex) => WindowposCenteredMask | (uint)displayIndex;
    
    
    /// <summary>
    /// Used to indicate that the window position should be centered.
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    [Macro]
    public static bool WindowPosIsCentered(uint position) => (position & 0xFFFF0000u) == WindowposCenteredMask;
    
    
    /// <summary>
    /// Used to indicate that you don't care what the window position is.
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    public static readonly uint WindowPosUndefined = WindowPosUndefinedDisplay(0);
    
    
    /// <summary>
    /// Used to indicate that the window position should be centered.
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    public static readonly uint WindowPosCentered = WindowPosCenteredDisplay(0);
}