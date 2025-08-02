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
    /// <code>typedef const void *(SDLCALL *SDL_ClipboardDataCallback)(void *userdata, const char *mime_type, size_t *size);</code>
    /// <summary>
    /// <para>Callback function that will be called when data for the specified mime-type
    /// is requested by the OS.</para>
    /// <para>The callback function is called with <c>null</c> as the mime_type when the
    /// clipboard is cleared or new data is set. The clipboard is automatically
    /// cleared in <see cref="Quit()"/>.</para>
    /// </summary>
    /// <param name="userdata">a pointer to the provided user data.</param>
    /// <param name="mimeType">the requested mime-type.</param>
    /// <param name="size">a pointer filled in with the length of the returned data.</param>
    /// <returns>a pointer to the data for the provided mime-type. Returning <c>null</c>
    /// or setting the length to 0 will cause no data to be sent to the
    /// "receiver". It is up to the receiver to handle this. Essentially
    /// returning no data is more or less undefined behavior and may cause
    /// breakage in receiving applications. The returned data will not be
    /// freed, so it needs to be retained and dealt with internally.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetClipboardData"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ClipboardDataCallback(IntPtr userdata, [MarshalAs(UnmanagedType.LPUTF8Str)] string mimeType, out UIntPtr size);
    
    
    /// <code>typedef void (SDLCALL *SDL_ClipboardCleanupCallback)(void *userdata);</code>
    /// <summary>
    /// Callback function that will be called when the clipboard is cleared, or
    /// when new data is set.
    /// </summary>
    /// <param name="userdata">a pointer to the provided user data.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetClipboardData"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ClipboardCleanupCallback(IntPtr userdata);
}