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
    /// <code>#define SDL_CACHELINE_SIZE  128</code>
    /// <summary>
    /// <para>A guess for the cacheline size used for padding.</para>
    /// <para>Most x86 processors have a 64 byte cache line. The 64-bit PowerPC
    /// processors have a 128 byte cache line. We use the larger value to be
    /// generally safe.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    public const int CacheLineSize = 128;
}