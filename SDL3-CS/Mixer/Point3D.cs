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

public partial class Mixer
{
    /// <summary>
    /// 3D coordinates for <see cref="SetTrack3DPosition"/>.
    /// <para>The coordinates use a "right-handed" coordinate system, like OpenGL and
    /// OpenAL.</para>
    /// </summary>
    /// <since>This struct is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrack3DPosition"/>
    public struct Point3D
    {
        /// <summary>
        /// X coordinate (negative left, positive right).
        /// </summary>
        public float X;
        
        /// <summary>
        /// Y coordinate (negative down, positive up).
        /// </summary>
        public float Y;
        
        /// <summary>
        /// Z coordinate (negative forward, positive back).
        /// </summary>
        public float Z;
    }
}