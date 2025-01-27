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
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_OpenURL(const char *url);</code>
    /// <summary>
    /// <para>Open a URL/URI in the browser or other appropriate external application.</para>
    /// <para>Open a URL in a separate, system-provided application. How this works will
    /// vary wildly depending on the platform. This will likely launch what makes
    /// sense to handle a specific URL's protocol (a web browser for <c>http://</c>,
    /// etc), but it might also be able to launch file managers for directories and
    /// other things.</para>
    /// <para>What happens when you open a URL varies wildly as well: your game window
    /// may lose focus (and may or may not lose focus if your game was fullscreen
    /// or grabbing input at the time). On mobile devices, your app will likely
    /// move to the background or your process might be paused. Any given platform
    /// may or may not handle a given URL.</para>
    /// <para>If this is unimplemented (or simply unavailable) for a platform, this will
    /// fail with an error. A successful result does not mean the URL loaded, just
    /// that we launched _something_ to handle it (or at least believe we did).</para>
    /// <para>All this to say: this function can be useful, but you should definitely
    /// test it on every platform you target.</para>
    /// </summary>
    /// <param name="url">a valid URL/URI to open. Use <c>file:///full/path/to/file</c> for
    /// local files, if supported.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenURL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool OpenURL([MarshalAs(UnmanagedType.LPUTF8Str)] string url);
}