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
    /// <para>The function pointers that drive an SDL_IOStream.</para>
    /// <para>Applications can provide this struct to <see cref="OpenIO"/> to create their own
    /// implementation of SDL_IOStream. This is not necessarily required, as SDL
    /// already offers several common types of I/O streams, via functions like
    /// <see cref="IOFromFile"/> and <see cref="IOFromMem"/>.</para>
    /// <para>This structure should be initialized using <see cref="SDL.InitInterface(ref SDL3.SDL.IOStreamInterface)"/></para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="SDL.InitInterface(ref SDL3.SDL.IOStreamInterface)"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct IOStreamInterface
    {
        /// <summary>
        /// The version of this interface
        /// </summary>
        public UInt32 Version;
        
        /// <summary>
        /// Return the number of bytes in this SDL_IOStream
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
        /// <para>Read up to <c>size</c> bytes from the data stream to the area pointed
        /// at by <c>ptr</c>.</para>
        /// <para>On an incomplete read, you should set <c>*status</c> to a value from the
        /// SDL_IOStatus enum. You do not have to explicitly set this on
        /// a complete, successful read.</para>
        /// </summary>
        /// <returns>the number of bytes read</returns>
        public ReadDelegate Read;
        
        /// <summary>
        /// <para>Write exactly <c>size</c> bytes from the area pointed at by <c>ptr</c>
        /// to data stream.</para>
        /// <para>On an incomplete write, you should set <c>*status</c> to a value from the
        /// SDL_IOStatus enum. You do not have to explicitly set this on
        /// a complete, successful write.</para>
        /// </summary>
        /// <returns>the number of bytes written</returns>
        public WriteDelegate Write;

        /// <summary>
        /// <para>If the stream is buffering, make sure the data is written out.</para>
        /// <para>On failure, you should set <c>*status</c> to a value from the
        /// SDL_IOStatus enum. You do not have to explicitly set this on
        /// a successful flush.</para>
        /// </summary>
        /// <returns><c>true</c> if successful or <c>false</c> on write error when flushing data.</returns>
        public FlushDelegate Flush;
        
        /// <summary>
        /// <para>Close and free any allocated resources.</para>
        /// <para>This does not guarantee file writes will sync to physical media; they
        /// can be in the system's file cache, waiting to go to disk.</para>
        /// <para>The SDL_IOStream is still destroyed even if this fails, so clean up anything
        /// even if flushing buffers, etc, returns an error.</para>
        /// </summary>
        /// <returns><c>true</c> if successful or <c>false</c> on write error when flushing data.</returns>
        public CloseDelegate Close;
        
        
        public delegate long SizeDelegate(IntPtr userdata);
        
        public delegate long SeekDelegate(IntPtr userdata, long offset, IOWhence whence);
        
        public delegate ulong ReadDelegate(IntPtr userdata, IntPtr ptr, ulong size, out IOStatus status);
        
        public delegate ulong WriteDelegate(IntPtr userdata, IntPtr ptr, ulong size, out IOStatus status);
        
        public delegate bool FlushDelegate(IntPtr userdata, out IOStatus status);
        
        public delegate bool CloseDelegate(IntPtr userdata);
    }
}