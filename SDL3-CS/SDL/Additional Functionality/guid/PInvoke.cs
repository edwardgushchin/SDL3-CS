#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GUIDToString(GUID guid, IntPtr pszGUID, int cbGUID);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GUIDToString(SDL_GUID guid, char *pszGUID, int cbGUID);</code>
    /// <summary>
    /// <para>Get an ASCII string representation for a given SDL_GUID.</para>
    /// <para>You should supply at least 33 bytes for pszGUID.</para>
    /// </summary>
    /// <param name="guid">the <see cref="GUID"/> you wish to convert to string.</param>
    /// <param name="result">buffer in which to write the ASCII string</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <seealso cref="GUIDFromString"/>
    public static int GUIDToString(GUID guid, out string? result)
    {
        const int bufferSize = 33;
        var buffer = Marshal.AllocHGlobal(bufferSize);
        try
        {
            var returnValue = SDL_GUIDToString(guid, buffer, bufferSize);
            result = returnValue == 0 ? Marshal.PtrToStringAnsi(buffer) : string.Empty;
            return returnValue;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GUIDFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string pchGUID);
    /// <code>extern SDL_DECLSPEC SDL_GUID SDLCALL SDL_GUIDFromString(const char *pchGUID);</code>
    /// <summary>
    /// <para>Convert a GUID string into a <see cref="GUID"/> structure.</para>
    /// <para>Performs no error checking. If this function is given a string containing
    /// an invalid GUID, the function will silently succeed, but the GUID generated
    /// will not be useful.</para>
    /// </summary>
    /// <param name="guidString">string containing an ASCII representation of a GUID.</param>
    /// <returns>a <see cref="GUID"/> structure.</returns>
    /// <seealso cref="GUIDToString"/>
    public static GUID GUIDFromString(string guidString) => SDL_GUIDFromString(guidString);
    
}