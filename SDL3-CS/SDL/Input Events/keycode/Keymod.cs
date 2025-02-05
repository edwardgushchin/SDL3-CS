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
    /// Valid key modifiers (possibly OR'd together).
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [Flags]
    public enum Keymod : ushort
    { 
        /// <summary>
        /// no modifier is applicable.
        /// </summary>
        None = 0x0000,
        
        /// <summary>
        /// the left Shift key is down.
        /// </summary>
        LShift = 0x0001,
        
        /// <summary>
        /// the right Shift key is down.
        /// </summary>
        RShift = 0x0002,
        
        /// <summary>
        /// the Level 5 Shift key is down.
        /// </summary>
        Level5 = 0x0004, 
        
        /// <summary>
        /// the left Ctrl (Control) key is down.
        /// </summary>
        LCtrl = 0x0040,
        
        /// <summary>
        /// the right Ctrl (Control) key is down.
        /// </summary>
        RCtrl = 0x0080,
        
        /// <summary>
        /// the left Alt key is down.
        /// </summary>
        LAlt = 0x0100,
        
        /// <summary>
        /// the right Alt key is down.
        /// </summary>
        RAlt = 0x0200,
        
        /// <summary>
        /// the left GUI key (often the Windows key) is down.
        /// </summary>
        LGUI = 0x0400,
        
        /// <summary>
        /// the right GUI key (often the Windows key) is down.
        /// </summary>
        RGUI = 0x0800,
        
        /// <summary>
        /// the Num Lock key (may be located on an extended keypad) is down.
        /// </summary>
        Num = 0x1000,
        
        /// <summary>
        /// the Caps Lock key is down.
        /// </summary>
        Caps = 0x2000,
        
        /// <summary>
        /// the !AltGr key is down.
        /// </summary>
        Mode = 0x4000,
        
        /// <summary>
        /// the Scroll Lock key is down.
        /// </summary>
        Scroll = 0x8000,
        
        /// <summary>
        /// Any Ctrl key is down.
        /// </summary>
        Ctrl = LCtrl | RCtrl,
        
        /// <summary>
        /// Any Shift key is down.
        /// </summary>
        Shift = LShift | RShift,
        
        /// <summary>
        /// Any Alt key is down.
        /// </summary>
        Alt = LAlt | RAlt,
        
        /// <summary>
        /// Any GUI key is down.
        /// </summary>p0
        GUI = LGUI | RGUI
    }
}