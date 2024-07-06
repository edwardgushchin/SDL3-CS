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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetError();

    /// <summary>
    /// Retrieve a message about the last error that occurred on the current thread.
    /// </summary>
    /// <returns>Returns a message with information about the specific error that occurred, or an empty string
    /// if there hasn't been an error message set since the last call to <see cref="ClearError"/>.</returns>
    /// <remarks>
    /// <para>It is possible for multiple errors to occur before calling <see cref="ClearError"/>.
    /// Only the last error is returned.</para>
    /// <para>The message is only applicable when an SDL function has signaled an error.
    /// You must check the return values of SDL function calls to determine when to appropriately call
    /// <see cref="GetError"/>. You should not use the results of <see cref="GetError"/> to decide if an error
    /// has occurred!
    /// Sometimes SDL will set an error string even when reporting success.</para>
    /// <para>SDL will not clear the error string for successful API calls.
    /// You must check return values for failure cases before you can assume the error string applies.</para>
    /// <para>Error strings are set per-thread, so an error set in a different thread will not interfere with the
    /// current thread's operation.</para>
    /// <para>The returned string does NOT follow the <see cref="GetStringRule"/>! The pointer is valid until the
    /// current thread's error string is changed, so the caller should make a copy if the string is to be
    /// used after calling into SDL again.</para>
    /// </remarks>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="SetError"/>
    public static string? GetError()
    {
        return UTF8_ToManaged(SDL_GetError());
    }
}