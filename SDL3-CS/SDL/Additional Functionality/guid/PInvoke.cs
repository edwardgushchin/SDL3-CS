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
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GUIDToString(SDL_GUID guid, char *pszGUID, int cbGUID);</code>
    /// <summary>
    /// Get an ASCII string representation for a given <see cref="GUID"/>.
    /// </summary>
    /// <param name="guid">the <see cref="GUID"/> you wish to convert to string.</param>
    /// <param name="pszGUID">buffer in which to write the ASCII string.</param>
    /// <param name="cbGUID">the size of pszGUID, should be at least 33 bytes.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="StringToGUID"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GUIDToString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial void GUIDToString(GUID guid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] ref byte[] pszGUID, int cbGUID);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GUID SDLCALL SDL_StringToGUID(const char *pchGUID);</code>
    /// <summary>
    /// <para>Convert a GUID string into a <see cref="GUID"/> structure.</para>
    /// <para>Performs no error checking. If this function is given a string containing
    /// an invalid GUID, the function will silently succeed, but the GUID generated
    /// will not be useful.</para>
    /// </summary>
    /// <param name="pchGUID">string containing an ASCII representation of a GUID.</param>
    /// <returns>a <see cref="GUID"/> structure.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GUIDToString"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_StringToGUID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern GUID StringToGUID([MarshalAs(UnmanagedType.LPUTF8Str)] string pchGUID);
}