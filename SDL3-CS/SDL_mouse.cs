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

namespace SDL3;

public static partial class SDL
{
    public enum MouseButtonFlags : uint
    {
        Left     = 1,
        Middle   = 2,
        Right    = 3,
        X1       = 4,
        X2       = 5,
    }
    
    
    public enum MouseWheelDirection
    {
        Normal,
        Flipped
    }
    
    
    public static uint Button(uint x)
    {
        return 1u << ((int)x - 1);
    }
    
    public static readonly uint ButtonLMask = Button((uint)MouseButtonFlags.Left);
    public static readonly uint ButtonMMask = Button((uint)MouseButtonFlags.Middle);
    public static readonly uint ButtonRMask = Button((uint)MouseButtonFlags.Right);
    public static readonly uint ButtonX1Mask = Button((uint)MouseButtonFlags.X1);
    public static readonly uint ButtonX2Mask = Button((uint)MouseButtonFlags.X2);
}