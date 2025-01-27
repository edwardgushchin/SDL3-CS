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
    /// <para>The predefined log categories</para>
    /// <para>By default the application and gpu categories are enabled at the <see cref="LogPriority.Info"/>
    /// level, the assert category is enabled at the see <see cref="LogPriority.Warn"/> level, test is enabled at
    /// the see <see cref="LogPriority.Verbose"/> level and all other categories are enabled at the see <see cref="LogPriority.Error"/> level.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum LogCategory
    {
        Application,
        Error,
        Assert,
        System,
        Audio,
        Video,
        Render,
        Input,
        Test,
        GPU,

        /* Reserved for future SDL library use */
        Reserved2,
        Reserved3,
        Reserved4,
        Reserved5,
        Reserved6,
        Reserved7,
        Reserved8,
        Reserved9,
        Reserved10,

        /* Beyond this point is reserved for application use, e.g. */
        Custom
    }
}