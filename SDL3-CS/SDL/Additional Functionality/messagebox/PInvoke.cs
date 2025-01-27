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
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShowMessageBox(const SDL_MessageBoxData *messageboxdata, int *buttonid);</code>
    /// <summary>
    /// <para>Create a modal message box.</para>
    /// <para>If your needs aren't complex, it might be easier to use
    /// <see cref="ShowSimpleMessageBox"/>.</para>
    /// <para>This function should be called on the thread that created the parent
    /// window, or on the main thread if the messagebox has no parent. It will
    /// block execution of that thread until the user clicks a button or closes the
    /// messagebox.</para>
    /// <para>This function may be called at any time, even before <see cref="Init"/>. This makes
    /// it useful for reporting errors like a failure to create a renderer or
    /// OpenGL context.</para>
    /// <para>On X11, SDL rolls its own dialog box with X11 primitives instead of a
    /// formal toolkit like GTK+ or Qt.</para>
    /// <para>Note that if <see cref="Init"/> would fail because there isn't any available video
    /// target, this function is likely to fail for the same reasons. If this is a
    /// concern, check the return value from this function and fall back to writing
    /// to stderr if you can.</para>
    /// </summary>
    /// <param name="messageboxdata">the <see cref="MessageBoxData"/> structure with title, text and
    /// other options.</param>
    /// <param name="buttonid">the pointer to which user id of hit button should be
    /// copied.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [DllImport(SDLLibrary, EntryPoint = "SDL_ShowMessageBox"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool ShowMessageBox(in MessageBoxData messageboxdata, out int buttonid);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags flags, const char *title, const char *message, SDL_Window *window);</code>
    /// <summary>
    /// <para>Display a simple modal message box.</para>
    /// <para>If your needs aren't complex, this function is preferred over
    /// <see cref="ShowMessageBox"/>.</para>
    /// <para><c>flags</c> may be any of the following:</para>
    /// <list type="bullet">
    /// <item><see cref="MessageBoxFlags.Error"/>: error dialog</item>
    /// <item><see cref="MessageBoxFlags.Warning"/>: warning dialog</item>
    /// <item><see cref="MessageBoxFlags.Information"/>: informational dialog</item>
    /// </list>
    /// <para>This function should be called on the thread that created the parent
    /// window, or on the main thread if the messagebox has no parent. It will
    /// block execution of that thread until the user clicks a button or closes the
    /// messagebox.</para>
    /// <para>This function may be called at any time, even before <see cref="Init"/>. This makes
    /// it useful for reporting errors like a failure to create a renderer or
    /// OpenGL context.</para>
    /// <para>On X11, SDL rolls its own dialog box with X11 primitives instead of a
    /// formal toolkit like GTK+ or Qt.</para>
    /// <para>Note that if <see cref="Init"/> would fail because there isn't any available video
    /// target, this function is likely to fail for the same reasons. If this is a
    /// concern, check the return value from this function and fall back to writing
    /// to stderr if you can.</para>
    /// </summary>
    /// <param name="flags">an <see cref="MessageBoxFlags"/> value.</param>
    /// <param name="title">UTF-8 title text.</param>
    /// <param name="message">UTF-8 message text.</param>
    /// <param name="window">the parent window, or <c>null</c> for no parent.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ShowMessageBox"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowSimpleMessageBox"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ShowSimpleMessageBox(MessageBoxFlags flags, [MarshalAs(UnmanagedType.LPUTF8Str)] string title, [MarshalAs(UnmanagedType.LPUTF8Str)] string message, IntPtr window);
}