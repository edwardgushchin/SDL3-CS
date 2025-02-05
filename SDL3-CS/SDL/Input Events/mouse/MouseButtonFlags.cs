#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024-2025 Eduard Gushchin.
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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// A bitmask of pressed mouse buttons, as reported by <see cref="GetMouseState"/>, etc.
    /// <list type="bullet">
    /// <item>Button 1: Left mouse button</item>
    /// <item>Button 2: Middle mouse button</item>
    /// <item>Button 3: Right mouse button</item>
    /// <item>Button 4: Side mouse button 1</item>
    /// <item>Button 5: Side mouse button 2</item>
    /// </list>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="GetMouseState"/>
    /// <seealso cref="GetGlobalMouseState"/>
    /// <seealso cref="GetRelativeMouseState"/>
    [Flags]
    public enum MouseButtonFlags : uint
    {
         Left = 1 << (ButtonLeft - 1),
         Middle = 1 << (ButtonMiddle - 1),
         Right = 1 << (ButtonRight - 1),
         X1 = 1 << (ButtonX1 - 1),
         X2 = 1 << (ButtonX2 - 1)
    }
}