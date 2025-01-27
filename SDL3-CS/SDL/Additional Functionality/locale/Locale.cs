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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>A struct to provide locale data.</para>
    /// <para>Locale data is split into a spoken language, like English, and an optional
    /// country, like Canada. The language will be in ISO-639 format (so English
    /// would be "en"), and the country, if not NULL, will be an ISO-3166 country
    /// code (so Canada would be "CA").</para>
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPreferredLocales"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct Locale
    {
        /// <summary>
        /// A language name, like "en" for English.
        /// </summary>
        [MarshalAs(UnmanagedType.LPUTF8Str)] public string Language;
        
        /// <summary>
        /// A country, like "US" for America. Can be <c>null</c>.
        /// </summary>
        [MarshalAs(UnmanagedType.LPUTF8Str)] public string? Country;
    }
}