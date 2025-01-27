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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>Description of where standard I/O should be directed when creating a
    /// process.</para>
    /// <para>If a standard I/O stream is set to <see cref="Inherited"/>, it will go
    /// to the same place as the application's I/O stream. This is the default for
    /// standard output and standard error.</para>
    /// <para>If a standard I/O stream is set to <see cref="Null"/>, it is connected
    /// to <c>NUL:</c> on Windows and <c>/dev/null</c> on POSIX systems. This is the default
    /// for standard input.</para>
    /// <para>If a standard I/O stream is set to <see cref="App"/>, it is connected
    /// to a new SDL_IOStream that is available to the application. Standard input
    /// will be available as <see cref="Props.ProcessSTDInPointer"/> and allows
    /// <see cref="GetProcessInput"/>, standard output will be available as
    /// <see cref="Props.ProcessSTDOutPointer"/> and allows <see cref="ReadProcess"/> and
    /// <see cref="GetProcessOutput"/>, and standard error will be available as
    /// <see cref="Props.ProcessSTDErrPointer"/> in the properties for the created
    /// process.</para>
    /// <para>If a standard I/O stream is set to <see cref="Redirect"/>, it is
    /// connected to an existing SDL_IOStream provided by the application. Standard
    /// input is provided using <see cref="Props.ProcessCreateSTDInPointer"/>, standard
    /// output is provided using <see cref="Props.ProcessCreateSTDOutPointer"/>, and
    /// standard error is provided using <see cref="Props.ProcessCreateSTDErrPointer"/>
    /// in the creation properties. These existing streams should be closed by the
    /// application once the new process is created.</para>
    /// <para>In order to use an SDL_IOStream with <see cref="Redirect"/>, it must
    /// have <see cref="Props.IOStreamWindowsHandlePointer"/> or
    /// <see cref="Props.IOStreamFileDescriptorNumber"/> set. This is true for streams
    /// representing files and process I/O.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessProperties"/>
    /// <seealso cref="ReadProcess"/>
    /// <seealso cref="GetProcessInput"/>
    /// <seealso cref="GetProcessOutput"/>
    public enum ProcessIO
    {
        /// <summary>
        /// The I/O stream is inherited from the application.
        /// </summary>
        Inherited,
        
        /// <summary>
        /// The I/O stream is ignored.
        /// </summary>
        Null,
        
        /// <summary>
        /// The I/O stream is connected to a new SDL_IOStream that the application can read or write
        /// </summary>
        App,
        
        /// <summary>
        /// The I/O stream is redirected to an existing SDL_IOStream.
        /// </summary>
        Redirect
    }
}