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
    /// <code>typedef void (SDLCALL *SDL_DialogFileCallback)(void *userdata, const char * const *filelist, int filter);</code>
    /// <summary>
    /// <para>Callback used by file dialog functions.</para>
    /// <para>The specific usage is described in each function.</para>
    /// <para>If <c>filelist</c> is:</para>
    /// <list type="bullet">
    /// <item><c>null</c>, an error occurred. Details can be obtained with <see cref="GetError"/>.</item>
    /// <item>A pointer to <c>null</c>, the user either didn't choose any file or canceled the
    /// dialog.</item>
    /// <item>A pointer to non-<c>null</c>, the user chose one or more files. The argument
    /// is a null-terminated array of pointers to UTF-8 encoded strings, each containing a
    /// path.</item>
    /// </list>
    /// <para>The filelist argument should not be freed; it will automatically be
    /// freed when the callback returns.</para>
    /// <para>The filter argument is the index of the filter that was selected, or <c>-1</c> if
    /// no filter was selected or if the platform or method doesn't support
    /// fetching the selected filter.</para>
    /// <para>In Android, the <c>filelist</c> are <c>content://</c> URIs. They should be opened
    /// using <see cref="IOFromFile"/> with appropriate modes. This applies both to open
    /// and save file dialog.</para>
    /// </summary>
    /// <param name="userdata">an app-provided pointer, for the callback's use.</param>
    /// <param name="filelist">the file(s) chosen by the user.</param>
    /// <param name="filter">index of the selected filter.</param>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="DialogFileFilter"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    /// <seealso cref="ShowFileDialogWithProperties"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DialogFileCallback(IntPtr userdata, IntPtr filelist, int filter);
}