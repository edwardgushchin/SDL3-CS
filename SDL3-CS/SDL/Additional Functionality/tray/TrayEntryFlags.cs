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

public partial class SDL
{
    /// <summary>
    /// <para>Flags that control the creation of system tray entries.</para>
    /// <para>Some of these flags are required; exactly one of them must be specified at
    /// the time a tray entry is created. Other flags are optional; zero or more of
    /// those can be OR'ed together with the required flag.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.0.0.</since>
    /// <seealso cref="InsertTrayEntryAt"/>
    [Flags]
    public enum TrayEntryFlags : uint
    { 
        /// <summary>
        /// Make the entry a simple button. Required.
        /// </summary>
        Button = 0x00000001u,
        
        /// <summary>
        /// Make the entry a checkbox. Required.
        /// </summary>
        CheckBox = 0x00000002u,
        
        /// <summary>
        /// Prepare the entry to have a submenu. Required
        /// </summary>
        SubMenu = 0x00000004u,
        
        /// <summary>
        /// Make the entry disabled. Optional.
        /// </summary>
        Disabled = 0x80000000u,
        
        /// <summary>
        /// Make the entry checked. This is valid only for checkboxes. Optional.
        /// </summary>
        Checked = 0x40000000u,
    }
}