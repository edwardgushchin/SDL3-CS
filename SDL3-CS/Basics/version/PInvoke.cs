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

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetVersion();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetVersion(void);</code>
    /// <summary>
    /// <para>Get the version of SDL that is linked against your program.</para>
    /// <para>If you are linking to SDL dynamically, then it is possible that the current
    /// version will be different than the version you compiled against. This
    /// function returns the current version, while SDL_VERSION is the version you
    /// compiled with.</para>
    /// </summary>
    /// <returns>the version of the linked library.</returns>
    /// <remarks>This function may be called safely at any time, even before SDL_Init().</remarks>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRevision"/>
    public static Version GetVersion()
    {
        var version = SDL_GetVersion();
        return new Version
        {
            Major = (byte)VersionNumMajor(version),
            Minor = (byte)VersionNumMinor(version),
            Patch = (byte)VersionNumMicro(version)
        };
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetRevision();
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetRevision(void);</code>
    /// <summary>
    /// <para>Get the code revision of SDL that is linked against your program.</para>
    /// <para>This value is the revision of the code you are linked with and may be
    /// different from the code you are compiling with, which is found in the
    /// constant SDL_REVISION.</para>
    /// <para>The revision is arbitrary string (a hash value) uniquely identifying the
    /// exact revision of the SDL library in use, and is only useful in comparing
    /// against other revisions. It is NOT an incrementing number.</para>
    /// <para>If SDL wasn't built from a git repository with the appropriate tools, this
    /// will return an empty string.</para>
    /// <para>You shouldn't use this function for anything but logging it for debugging
    /// purposes. The string is not intended to be reliable in any way.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <returns>an arbitrary string, uniquely identifying the exact revision of
    /// the SDL library in use.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetVersion"/>
    public static string? GetRevision() => Marshal.PtrToStringUTF8(SDL_GetRevision());
}