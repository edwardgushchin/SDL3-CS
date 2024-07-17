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
    [Flags]
    public enum Keymod : uint
    { 
        None   = 0x0000u,
        LShift = 0x0001u,
        RShift = 0x0002u,
        LCtrl  = 0x0040u,
        RCtrl  = 0x0080u,
        LAlt   = 0x0100u,
        RAlt   = 0x0200u,
        LGUI   = 0x0400u,
        RGUI   = 0x0800u,
        Num    = 0x1000u,
        Caps   = 0x2000u,
        Mode   = 0x4000u,
        Scroll = 0x8000u,
        Ctrl   = LCtrl | RCtrl,
        Shift  = LShift | RShift,
        Alt    = LAlt | RAlt,
        Gui    = LGUI | RGUI
    }
}