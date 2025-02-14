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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPlatform"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPlatform();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetPlatform(void);</code>
    /// <summary>
    /// <para>Get the name of the platform.</para>
    /// <para>Here are the names returned for some (but not all) supported platforms:</para>
    /// <list type="bullet">
    /// <item>"Windows"</item>
    /// <item>"macOS"</item>
    /// <item>"Linux"</item>
    /// <item>"iOS"</item>
    /// <item>"Android"</item>
    /// </list>
    /// </summary>
    /// <returns>the name of the platform. If the correct platform name is not
    /// available, returns a string beginning with the text "Unknown".</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string GetPlatform()
    {
        var value = SDL_GetPlatform(); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
}