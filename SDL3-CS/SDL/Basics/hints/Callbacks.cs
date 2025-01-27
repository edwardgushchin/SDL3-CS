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
    /// <code>typedef void(SDLCALL *SDL_HintCallback)(void *userdata, const char *name, const char *oldValue, const char *newValue);</code>
    /// <summary>
    /// <para>A callback used to send notifications of hint value changes.</para>
    /// <para>This is called an initial time during <see cref="AddHintCallback"/> with the hint's
    /// current value, and then again each time the hint's value changes.</para>
    /// </summary>
    /// <param name="userdata">what was passed as <c>userdata</c> to <see cref="AddHintCallback"/>.</param>
    /// <param name="name">what was passed as <c>name</c> to <see cref="AddHintCallback"/>.</param>
    /// <param name="oldValue">the previous hint value.</param>
    /// <param name="newValue">the new value hint is to be set to.</param>
    /// <threadsafety>This callback is fired from whatever thread is setting a new
    /// hint value. SDL holds a lock on the hint subsystem when
    /// calling this callback.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="AddHintCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HintCallback(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string oldValue,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string newValue);
}