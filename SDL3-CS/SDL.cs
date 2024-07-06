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

using System.Text;

namespace SDL3;

public static partial class SDL
{
    private const string SDLLibrary = "SDL3.dll";

    /// <summary>
    /// Calculates the maximum possible size in bytes of a given string when encoded in UTF-8.
    /// </summary>
    /// <param name="str">The input string to calculate the size for. If the string is null, the size will be 0.</param>
    /// <returns>The maximum possible byte size of the UTF-8 encoded string. If the input string is null, returns 0.</returns>
    /// <remarks>
    /// The calculation is based on the assumption that each character can take up to 4 bytes in UTF-8 encoding, plus an additional byte for the null terminator.
    /// </remarks>
    private static int Utf8Size(string? str)
    {
        if (str == null) return 0;
        return str.Length * 4 + 1;
    }

    /// <summary>
    /// Encodes a given string into UTF-8 and stores the result in the provided buffer.
    /// </summary>
    /// <param name="str">The input string to encode. If the string is null, the function returns a null pointer.</param>
    /// <param name="buffer">The buffer where the UTF-8 encoded bytes will be stored.</param>
    /// <param name="bufferSize">The size of the provided buffer.</param>
    /// <returns>A pointer to the buffer containing the UTF-8 encoded bytes. If the input string is null, returns a null pointer.</returns>
    /// <remarks>
    /// This function uses unsafe code to handle pointers and perform the encoding. It assumes that the provided buffer is large enough to hold the encoded string.
    /// </remarks>
    private static unsafe byte* Utf8Encode(string? str, byte* buffer, int bufferSize)
    {
        if (str == null) return (byte*) 0;
        fixed (char* strPtr = str)
        {
            Encoding.UTF8.GetBytes(strPtr, str.Length + 1, buffer, bufferSize);
        }
        return buffer;
    }

    /// <summary>
    /// Converts a UTF-8 encoded string from an unmanaged memory pointer to a managed string.
    /// </summary>
    /// <param name="s">A pointer to the UTF-8 encoded string in unmanaged memory.</param>
    /// <param name="freePtr">If set to true, the memory pointed to by <paramref name="s"/> will be freed after conversion.</param>
    /// <returns>A managed string that represents the UTF-8 encoded string. If the input pointer is zero, returns null.</returns>
    /// <remarks>
    /// This function reads a UTF-8 encoded string from unmanaged memory, converts it to a managed string, 
    /// and optionally frees the unmanaged memory if <paramref name="freePtr"/> is true.
    /// </remarks>
    private static unsafe string? UTF8_ToManaged(IntPtr s, bool freePtr = false)
    {
        if (s == IntPtr.Zero) return null;
        
        var ptr = (byte*) s;
        while (*ptr != 0) ptr++;
        
        var result = Encoding.UTF8.GetString((byte*) s, (int) (ptr - (byte*) s));
        
        if (freePtr) SDL_free(s);
        return result;
    }
}