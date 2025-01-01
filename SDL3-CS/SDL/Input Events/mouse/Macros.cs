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
    [Macro]
    public static uint ButtonMask(int x) => 1u << (x - 1);

    public static uint ButtonLMask = ButtonMask((int)MouseButtonFlags.Left);
    
    public static uint ButtonMMask = ButtonMask((int)MouseButtonFlags.Middle);
    
    public static uint ButtonRMask = ButtonMask((int)MouseButtonFlags.Right);
    
    public static uint ButtonX1Mask = ButtonMask((int)MouseButtonFlags.X1);
    
    public static uint ButtonX2Mask = ButtonMask((int)MouseButtonFlags.X2);
}