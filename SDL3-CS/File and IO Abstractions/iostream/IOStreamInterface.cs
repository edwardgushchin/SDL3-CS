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
    /// The function pointers that drive an <seealso cref="IOStream"/>.
    /// <para>Applications can provide this struct to <see cref="OpenIO"/> to create their own
    /// implementation of <see cref="IOStream"/>. This is not necessarily required, as SDL
    /// already offers several common types of I/O streams, via functions like
    /// <see cref="IOFromFile"/> and <see cref="IOFromMem"/>.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct IOStreamInterface
    {
        public UInt32 Version;
        
        /// <summary>
        /// Return the number of bytes in this <see cref="IOStream"/>
        /// </summary>
        /// <returns>the total size of the data stream, or -1 on error.</returns>
        public SizeDelegate Size;
        
        /// <summary>
        /// Seek to <c>offset</c> relative to <c>whence</c>, one of stdio's whence values:
        /// <see cref="IOWhence.Set"/>, <see cref="IOWhence.Cur"/>, <see cref="IOWhence.End"/> 
        /// </summary>
        /// <returns>the final offset in the data stream, or -1 on error.</returns>
        public SeekDelegate Seek;
        
        /// <summary>
        /// <para>Read up to `size` bytes from the data stream to the area pointed
        /// at by <c>ptr</c>.</para>
        /// <para>On an incomplete read, you should set <c>*status</c> to a value from the
        /// <see cref="IOStatus"/> enum. You do not have to explicitly set this on
        /// a complete, successful read.</para>
        /// </summary>
        /// <returns>the number of bytes read</returns>
        public ReadDelegate Read;
        
        /// <summary>
        /// <para>Write exactly <c>size</c> bytes from the area pointed at by <c>ptr</c>
        /// to data stream.</para>
        /// <para>On an incomplete write, you should set <c>*status</c> to a value from the
        /// <seealso cref="IOStatus"/> enum. You do not have to explicitly set this on
        /// a complete, successful write.</para>
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public WriteDelegate Write;
        
        /// <summary>
        /// <para>Close and free any allocated resources.</para>
        /// <para>The <seealso cref="IOStream"/> is still destroyed even if this fails, so clean up anything
        /// even if flushing to disk returns an error.</para>
        /// </summary>
        /// <returns>0 if successful or -1 on write error when flushing data.</returns>
        public CloseDelegate Close;
    }
}