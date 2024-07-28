#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

/**
 * # CategoryLocale
 *
 * SDL locale services.
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPreferredLocales();
    /// <code>extern SDL_DECLSPEC SDL_Locale * SDLCALL SDL_GetPreferredLocales(void);</code>
    /// <summary>
    /// <para>Report the user's preferred locale.</para>
    /// <para>This returns an array of SDL_Locale structs, the final item zeroed out.
    /// When the caller is done with this array, it should call <see cref="Free"/> on the
    /// returned value; all the memory involved is allocated in a single block, so
    /// a single <see cref="Free"/> will suffice.</para>
    /// <para>Returned language strings are in the format xx, where 'xx' is an ISO-639
    /// language specifier (such as "en" for English, "de" for German, etc).
    /// Country strings are in the format YY, where "YY" is an ISO-3166 country
    /// code (such as "US" for the United States, "CA" for Canada, etc). Country
    /// might be NULL if there's no specific guidance on them (so you might get {
    /// "en", "US" } for American English, but { "en", NULL } means "English
    /// language, generically"). Language strings are never NULL, except to
    /// terminate the array.</para>
    /// <para>Please note that not all of these strings are 2 characters; some are three
    /// or more.</para>
    /// <para>The returned list of locales are in the order of the user's preference. For
    /// example, a German citizen that is fluent in US English and knows enough
    /// Japanese to navigate around Tokyo might have a list like: { "de", "en_US",
    /// "jp", NULL }. Someone from England might prefer British English (where
    /// "color" is spelled "colour", etc), but will settle for anything like it: {
    /// "en_GB", "en", NULL }.</para>
    /// <para>This function returns NULL on error, including when the platform does not
    /// supply this information at all.</para>
    /// <para>This might be a "slow" call that has to query the operating system. It's
    /// best to ask for this once and save the results. However, this list can
    /// change, usually because the user has changed a system preference outside of
    /// your program; SDL will send an <see cref="EventType.LocaleChanged"/> event in this case,
    /// if possible, and you can call this function again to get an updated copy of
    /// preferred locales.</para>
    /// </summary>
    /// <returns>array of locales, terminated with a locale with a NULL language
    /// field. Will return NULL on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Locale[]? GetPreferredLocales()
    {
        var ptr = SDL_GetPreferredLocales();
        
        if (ptr == IntPtr.Zero) return null;
        
        var size = Marshal.SizeOf<Locale>();
        
        var count = 0;
        
        while (true)
        {
            var current = IntPtr.Add(ptr, count * size);
            var locale = Marshal.PtrToStructure<Locale>(current);
            if (locale.Language == null)
            {
                break;
            }
            count++;
        }
        
        var locales = new Locale[count];
        for (var i = 0; i < count; i++)
        {
            var current = IntPtr.Add(ptr, i * size);
            locales[i] = Marshal.PtrToStructure<Locale>(current);
        }
        
        Free(ptr);

        return locales;
    }
}