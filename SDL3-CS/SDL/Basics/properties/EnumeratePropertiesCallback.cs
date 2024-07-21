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

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef void (SDLCALL *SDL_EnumeratePropertiesCallback)(void *userdata, SDL_PropertiesID props, const char *name);</code>
    /// <summary>
    /// <para>A callback used to enumerate all the properties in a group of properties.</para>
    /// <para>This callback is called from <see cref="EnumerateProperties"/>, and is called once
    /// per property in the set.</para>
    /// </summary>
    /// <param name="userdata">an app-defined pointer passed to the callback.</param>
    /// <param name="props">the SDL_PropertiesID that is being enumerated.</param>
    /// <param name="name">the next property name in the enumeration.</param>
    /// <remarks><see cref="EnumerateProperties"/> holds a lock on `props` during this
    ///               callback.</remarks>
    /// <seealso cref="EnumerateProperties"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EnumeratePropertiesCallback(
        IntPtr userdata,
        uint props,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name
    );
}