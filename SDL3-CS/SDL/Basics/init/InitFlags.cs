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
    /// <para>Initialization flags for <see cref="Init"/> and/or <see cref="InitSubSystem"/></para>
    /// <para>These are the flags which may be passed to <see cref="Init"/>. You should specify
    /// the subsystems which you will be using in your application.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="Quit"/>
    /// <seealso cref="InitSubSystem"/>
    /// <seealso cref="QuitSubSystem"/>
    /// <seealso cref="WasInit"/>
    [Flags]
    public enum InitFlags : uint
    {
        /// <summary>
        /// <see cref="Audio"/> implies <see cref="Events"/>
        /// </summary>
        Audio =     0x00000010u,
        
        /// <summary>
        /// <see cref="Video"/> implies <see cref="Events"/>, should be initialized on the main thread
        /// </summary>
        Video =     0x00000020u,
        
        /// <summary>
        /// <see cref="Joystick"/> implies <see cref="Events"/>
        /// </summary>
        Joystick =  0x00000200u,
        Haptic =    0x00001000u,
        
        /// <summary>
        /// <see cref="Gamepad"/> implies <see cref="Events"/>
        /// </summary>
        Gamepad =   0x00002000u,
        Events =    0x00004000u,
        
        /// <summary>
        /// <see cref="Sensor"/> implies <see cref="Events"/>
        /// </summary>
        Sensor =    0x00008000u,
        
        /// <summary>
        /// <see cref="Camera"/> implies <see cref="Events"/>
        /// </summary>
        Camera =    0x00010000u
    }
}