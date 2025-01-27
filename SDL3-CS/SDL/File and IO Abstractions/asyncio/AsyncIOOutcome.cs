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
    /// Information about a completed asynchronous I/O request.
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct AsyncIOOutcome
    {
        /// <summary>
        /// what generated this task. This pointer will be invalid if it was closed!
        /// </summary>
        public IntPtr ASyncIO;
        
        /// <summary>
        /// What sort of task was this? Read, write, etc?
        /// </summary>
        public AsyncIOTaskType Type;
        
        /// <summary>
        /// the result of the work (success, failure, cancellation).
        /// </summary>
        public AsyncIOResult Result;
        
        /// <summary>
        /// buffer where data was read/written.
        /// </summary>
        public IntPtr Buffer;
        
        /// <summary>
        /// offset in the SDL_AsyncIO where data was read/written.
        /// </summary>
        public UInt64 Offset;
        
        /// <summary>
        /// number of bytes the task was to read/write.
        /// </summary>
        public UInt64 BytesRequested;
        
        /// <summary>
        /// actual number of bytes that were read/written.
        /// </summary>
        public UInt64 BytesTransferred;
        
        /// <summary>
        /// pointer provided by the app when starting the task
        /// </summary>
        public IntPtr Userdata;
    }
}