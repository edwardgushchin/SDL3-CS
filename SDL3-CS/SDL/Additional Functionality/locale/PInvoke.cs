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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPreferredLocales"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPreferredLocales(out int count);
    /// <code>extern SDL_DECLSPEC SDL_Locale ** SDLCALL SDL_GetPreferredLocales(int *count);</code>
    /// <summary>
    /// <para>Report the user's preferred locale.</para>
    /// <para>Returned language strings are in the format xx, where 'xx' is an ISO-639
    /// language specifier (such as "en" for English, "de" for German, etc).
    /// Country strings are in the format YY, where "YY" is an ISO-3166 country
    /// code (such as "US" for the United States, "CA" for Canada, etc). Country
    /// might be <c>null</c> if there's no specific guidance on them (so you might get {
    /// "en", "US" } for American English, but { "en", <c>null</c>} means "English
    /// language, generically"). Language strings are never <c>null</c>, except to
    /// terminate the array.</para>
    /// <para>Please note that not all of these strings are 2 characters; some are three
    /// or more.</para>
    /// <para>The returned list of locales are in the order of the user's preference. For
    /// example, a German citizen that is fluent in US English and knows enough
    /// Japanese to navigate around Tokyo might have a list like: { "de", "en_US",
    /// "jp", <c>null</c>}. Someone from England might prefer British English (where
    /// "color" is spelled "colour", etc), but will settle for anything like it: {
    /// "en_GB", "en", <c>null</c>}.</para>
    /// <para>This function returns <c>null</c> on error, including when the platform does not
    /// supply this information at all.</para>
    /// <para>This might be a "slow" call that has to query the operating system. It's
    /// best to ask for this once and save the results. However, this list can
    /// change, usually because the user has changed a system preference outside of
    /// your program; SDL will send an <see cref="EventType.LocaleChanged"/> event in this case,
    /// if possible, and you can call this function again to get an updated copy of
    /// preferred locales.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of locales returned, may
    /// be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of locale pointers, or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This is a single
    /// allocation that should be freed with <see cref="Free"/> when it is no
    /// longer needed.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static Locale[]? GetPreferredLocales(out int count)
    {
        var ptr = SDL_GetPreferredLocales(out count);

        try
        {
            return PointerToStructureArray<Locale>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }
}