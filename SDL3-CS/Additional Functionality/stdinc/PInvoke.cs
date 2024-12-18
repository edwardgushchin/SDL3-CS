﻿#region License
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

public partial class SDL
{
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_free(void *mem);</code>
    /// <summary>
    /// <para>Free allocated memory.</para>
    /// <para>The pointer is no longer valid after this call and cannot be dereferenced
    /// anymore.</para>
    /// <para>If <c>mem</c> is <c>null</c>, this function does nothing.</para>
    /// </summary>
    /// <param name="mem">a pointer to allocated memory, or <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_free"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Free(IntPtr mem);


    [LibraryImport(SDLLibrary, EntryPoint = "SDL_memset"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Memset(IntPtr dst, int c, uint len);
}