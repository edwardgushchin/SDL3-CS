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
    /// <code>#define SDL_VERSIONNUM(major, minor, patch) \ ((major) * 1000000 + (minor) * 1000 + (patch))</code>
    /// <summary>
    /// <para>This macro turns the version numbers into a numeric value.</para>
    /// <para>(1,2,3) becomes 1002003.</para>
    /// </summary>
    /// <param name="major">the major version number.</param>
    /// <param name="minor">the minorversion number.</param>
    /// <param name="patch">the patch version number.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static int VersionNum(int major, int minor, int patch) => ((major) * 1000000 + (minor) * 1000 + (patch));
    
    
    /// <code>#define SDL_VERSIONNUM_MAJOR(version) ((version) / 1000000)</code>
    /// <summary>
    /// <para>This macro extracts the major version from a version number</para>
    /// <para>1002003 becomes 1.</para>
    /// </summary>
    /// <param name="version">the version number.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static int VersionNumMajor(int version) => ((version) / 1000000);


    /// <code>#define SDL_VERSIONNUM_MINOR(version) (((version) / 1000) % 1000)</code>
    /// <summary>
    /// <para>This macro extracts the minor version from a version number</para>
    /// <para>1002003 becomes 2.</para>
    /// </summary>
    /// <param name="version">version the version number.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static int VersionNumMinor(int version) => (((version) / 1000) % 1000);


    /// <code>#define SDL_VERSIONNUM_MICRO(version) ((version) % 1000)</code>
    /// <summary>
    /// <para>This macro extracts the micro version from a version number</para>
    /// <para>1002003 becomes 3.</para>
    /// </summary>
    /// <param name="version">the version number.</param>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static int VersionNumMicro(int version) => ((version) % 1000);
}