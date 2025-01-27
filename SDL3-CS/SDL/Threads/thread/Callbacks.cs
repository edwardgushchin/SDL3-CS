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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef int (SDLCALL * SDL_ThreadFunction) (void *data);</code>
    /// <summary>
    /// <para>The function passed to <see cref="CreateThread"/> as the new thread's entry point.</para>
    /// </summary>
    /// <param name="data">what was passed as <c>data</c> to <see cref="CreateThread"/>.</param>
    /// <returns>a value that can be reported through <see cref="WaitThread"/>.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ThreadFunction(IntPtr data);
    
    
    /// <code>typedef void (SDLCALL *SDL_TLSDestructorCallback)(void *value);</code>
    /// <summary>
    /// <para>The callback used to cleanup data passed to <see cref="SetTLS"/>.</para>
    /// <para>This is called when a thread exits, to allow an app to free any resources.</para>
    /// </summary>
    /// <param name="value">a pointer previously handed to <see cref="SetTLS"/>.</param>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="SetTLS"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TLSDestructorCallback(IntPtr value);
}