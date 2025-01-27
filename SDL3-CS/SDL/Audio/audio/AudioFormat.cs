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
    /// Audio format.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum AudioFormat : uint
    {
        /// <summary>
        /// Unspecified audio format
        /// </summary>
        Unknown = 0x0000u,
        
        /// <summary>
        /// Unsigned 8-bit samples
        /// </summary>
        AudioU8 = 0x0008u,
        
        /// <summary>
        /// Signed 8-bit samples
        /// </summary>
        AudioS8 = 0x8008u,
        
        /// <summary>
        /// Signed 16-bit samples
        /// </summary>
        AudioS16LE = 0x8010u,
        
        /// <summary>
        /// As above, but big-endian byte order
        /// </summary>
        AudioS16BE = 0x9010u,
        
        /// <summary>
        /// 32-bit integer samples
        /// </summary>
        AudioS32LE = 0x8020u,
        
        /// <summary>
        /// As above, but big-endian byte order
        /// </summary>
        AudioS32BE = 0x9020u,
        
        /// <summary>
        /// 32-bit floating point samples
        /// </summary>
        AudioF32LE = 0x8120u,
        
        /// <summary>
        /// As above, but big-endian byte order
        /// </summary>
        AudioF32BE = 0x9120u,
    }
}