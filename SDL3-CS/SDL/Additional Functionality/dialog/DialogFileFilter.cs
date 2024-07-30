#region License
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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// An entry for filters for file dialogs.
    /// </summary>
    /// <param name="name">is a user-readable label for the filter (for example, "Office
    /// document").</param>
    /// <param name="pattern">is a semicolon-separated list of file extensions (for example,
    /// "doc;docx"). File extensions may only contain alphanumeric characters,
    /// hyphens, underscores and periods. Alternatively, the whole string can be a
    /// single asterisk ("*"), which serves as an "All files" filter.</param>
    /// <since>This struct is available since SDL 3.0.0.</since>
    /// <seealso cref="DialogFileCallback"/>
    /// <seealso cref="ShowOpenFileDialog"/>
    /// <seealso cref="ShowSaveFileDialog"/>
    /// <seealso cref="ShowOpenFolderDialog"/>
    public readonly struct DialogFileFilter(string name, string pattern) : IDisposable
    {
        private readonly IntPtr _name = Marshal.StringToCoTaskMemUTF8(name); 
        private readonly IntPtr _pattern = Marshal.StringToCoTaskMemUTF8(pattern);

        public void Dispose()
        {
            Marshal.ZeroFreeCoTaskMemUTF8(_name);
            Marshal.ZeroFreeCoTaskMemUTF8(_pattern);
        }
    }
}