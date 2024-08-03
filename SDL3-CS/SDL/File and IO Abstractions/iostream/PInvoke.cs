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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string mode);
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_IOFromFile(const char *file, const char *mode);</code>
    /// <summary>
    /// <para>Use this function to create a new <see cref="IOStream"/> structure for reading from
    /// and/or writing to a named file.</para>
    /// <para>The <c>mode</c> string is treated roughly the same as in a call to the C
    /// library's fopen(), even if SDL doesn't happen to use fopen() behind the
    /// scenes.</para>
    /// <para>Available <c>mode</c> strings:</para>
    /// <list type="bullet">
    /// <item><c>"r"</c>: Open a file for reading. The file must exist</item>
    /// <item><c>"w"</c>: Create an empty file for writing. If a file with the same name
    /// already exists its content is erased and the file is treated as a new
    /// empty file.</item>
    /// <item><c>"a"</c>: Append to a file. Writing operations append data at the end of the
    /// file. The file is created if it does not exist.</item>
    /// <item><c>"r+"</c>: Open a file for update both reading and writing. The file must
    /// exist.</item>
    /// <item><c>"w+"</c>: Create an empty file for both reading and writing. If a file with
    /// the same name already exists its content is erased and the file is
    /// treated as a new empty file.</item>
    /// <item><c>"a+"</c>: Open a file for reading and appending. All writing operations are
    /// performed at the end of the file, protecting the previous content to be
    /// overwritten. You can reposition (fseek, rewind) the internal pointer to
    /// anywhere in the file for reading, but writing operations will move it
    /// back to the end of file. The file is created if it does not exist.</item>
    /// </list>
    /// <para><b>NOTE</b>: In order to open a file as a binary file, a <c>"b"</c> character has to
    /// be included in the <c>mode</c> string. This additional <c>"b"</c> character can either
    /// be appended at the end of the string (thus making the following compound
    /// modes: <c>"rb"</c>, <c>"wb"</c>, <c>"ab"</c>, <c>"r+b"</c>, <c>"w+b"</c>, <c>"a+b"</c>)
    /// or be inserted between the
    /// letter and the <c>"+"</c> sign for the mixed modes (<c>"rb+"</c>, <c>"wb+"</c>, <c>"ab+"</c>).
    /// Additional characters may follow the sequence, although they should have no
    /// effect. For example, <c>"t"</c> is sometimes appended to make explicit the file is
    /// a text file.</para>
    /// <para>This function supports Unicode filenames, but they must be encoded in UTF-8
    /// format, regardless of the underlying operating system.</para>
    /// <para>In Android, <see cref="IOFromFile"/> can be used to open content:// URIs. As a
    /// fallback, <see cref="IOFromFile"/> will transparently open a matching filename in
    /// the app's <c>assets</c>.</para>
    /// <para>Closing the <see cref="IOStream"/> will close SDL's internal file handle.</para>
    /// <para>The following properties may be set at creation time by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="PropIOStreamWindowsHandlePointer"/>: a pointer, that can be cast
    /// to a win32 <c>HANDLE</c>, that this <see cref="IOStream"/> is using to access the
    /// filesystem. If the program isn't running on Windows, or SDL used some
    /// other method to access the filesystem, this property will not be set.</item>
    /// <item><see cref="PropIOStreamSTDIOFilePointer"/>: a pointer, that can be cast to a
    /// stdio <c>FILE *</c>, that this <see cref="IOStream"/> is using to access the filesystem.
    /// If SDL used some other method to access the filesystem, this property
    /// will not be set. <b>PLEASE NOTE</b> that if SDL is using a different C runtime
    /// than your app, trying to use this pointer will almost certainly result in
    /// a crash! This is mostly a problem on Windows; make sure you build SDL and
    /// your app with the same compiler and settings to avoid it.</item>
    /// <item><see cref="PropIOStreamAndroidAAssetPointer"/>: a pointer,
    /// that can be cast
    /// to an Android NDK <c>AAsset *</c>, that this <see cref="IOStream"/> is using to access
    /// the filesystem. If SDL used some other method to access the filesystem,
    /// this property will not be set.</item>
    /// </list>
    /// </summary>
    /// <param name="file">a UTF-8 string representing the filename to open.</param>
    /// <param name="mode">an ASCII string representing the mode to be used for opening
    /// the file.</param>
    /// <returns>a pointer to the <see cref="IOStream"/> structure that is created, or <c>NULL</c>
    /// on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IOStream? IOFromFile(string file, string mode)
    {
        var ioFile = SDL_IOFromFile(file, mode);
        return ioFile == IntPtr.Zero ? null : new IOStream(ioFile);
    }
}