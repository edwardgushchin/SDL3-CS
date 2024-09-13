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
    /// <summary>
    /// Valid key modifiers (possibly OR'd together).
    /// </summary>
    /// <since>This datatype is available since SDL 3.0.0.</since>
    [Flags]
    public enum Keymod : uint
    { 
        /// <summary>
        /// No modifier is applicable.
        /// </summary>
        None   = 0x0000u,
        
        /// <summary>
        /// The left Shift key is down.
        /// </summary>
        LShift = 0x0001u,
        
        /// <summary>
        /// The right Shift key is down.
        /// </summary>
        RShift = 0x0002u,
        
        /// <summary>
        /// The left Ctrl (Control) key is down.
        /// </summary>
        LCtrl  = 0x0040u,
        
        /// <summary>
        /// The right Ctrl (Control) key is down.
        /// </summary>
        RCtrl  = 0x0080u,
        
        /// <summary>
        /// The left Alt key is down.
        /// </summary>
        LAlt   = 0x0100u,
        
        /// <summary>
        /// The right Alt key is down.
        /// </summary>
        RAlt   = 0x0200u,
        
        /// <summary>
        /// The left GUI key (often the Windows key) is down.
        /// </summary>
        LGUI   = 0x0400u,
        
        /// <summary>
        /// The right GUI key (often the Windows key) is down.
        /// </summary>
        RGUI   = 0x0800u,
        
        /// <summary>
        /// The Num Lock key (may be located on an extended keypad) is down.
        /// </summary>
        Num    = 0x1000u,
        
        /// <summary>
        /// The Caps Lock key is down.
        /// </summary>
        Caps   = 0x2000u,
        
        /// <summary>
        /// The !AltGr key is down.
        /// </summary>
        Mode   = 0x4000u,
        
        /// <summary>
        /// The Scroll Lock key is down.
        /// </summary>
        Scroll = 0x8000u,
        
        /// <summary>
        /// Any Ctrl key is down.
        /// </summary>
        Ctrl   = LCtrl | RCtrl,
        
        /// <summary>
        /// Any Shift key is down.
        /// </summary>
        Shift  = LShift | RShift,
        
        /// <summary>
        /// Any Alt key is down.
        /// </summary>
        Alt    = LAlt | RAlt,
        
        /// <summary>
        /// Any GUI key is down.
        /// </summary>
        GUI    = LGUI | RGUI
    }
}