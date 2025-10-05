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
    /// <para>A magic value used with <see cref="WindowPosUndefined"/>.</para>
    /// <para>Generally this macro isn't used directly, but rather through
    /// <see cref="WindowPosUndefined"/> or <see cref="WindowPosUndefinedDisplay"/>.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    public const uint WindowPosUndefinedMask = 0x1FFF0000u;
    
    
    /// <summary>
    /// <para>A magic value used with <see cref="WindowPosCentered"/>.</para>
    /// <para>Generally this macro isn't used directly, but rather through
    /// <see cref="WindowPosCentered"/> or <see cref="WindowPosCenteredDisplay"/>.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetWindowPosition"/>
    public const uint WindowPosCenteredMask = 0x2FFF0000u;
    
    public const int WindowSurfaceVSyncDisabled = 0;
    public const int WindowSurfaceVSyncAdaptive = -1;
}