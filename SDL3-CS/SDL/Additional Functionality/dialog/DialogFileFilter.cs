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
    /// <summary>
    /// <para>An entry for filters for file dialogs.</para>
    /// </summary>
    /// <param name="name">is a user-readable label for the filter (for example, "Office
    /// document").</param>
    /// <param name="pattern">is a semicolon-separated list of file extensions (for example,
    /// <c>"doc;docx"</c>). File extensions may only contain alphanumeric characters,
    /// hyphens, underscores and periods. Alternatively, the whole string can be a
    /// single asterisk (<c>"*"</c>), which serves as an "<c>All files</c>" filter.</param>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    /// <seealso cref="ShowFileDialogWithProperties"/>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DialogFileFilter(string name, string pattern) : IDisposable
    {
        public readonly IntPtr Name = Marshal.StringToCoTaskMemUTF8(name);
        public readonly IntPtr Pattern = Marshal.StringToCoTaskMemUTF8(pattern);

        public void Dispose()
        {
            if (Name != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(Name);
            }

            if (Pattern != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(Pattern);
            }
        }
    }
}