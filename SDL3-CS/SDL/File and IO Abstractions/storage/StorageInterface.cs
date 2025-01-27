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
    /// <para>Function interface for SDL_Storage.</para>
    /// <para>Apps that want to supply a custom implementation of SDL_Storage will fill
    /// in all the functions in this struct, and then pass it to <see cref="OpenStorage"/> to
    /// create a custom SDL_Storage object.</para>
    /// <para>It is not usually necessary to do this; SDL provides standard
    /// implementations for many things you might expect to do with an SDL_Storage.</para>
    /// <para>This structure should be initialized using <see cref="InitInterface(ref StorageInterface)"/></para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="InitInterface(ref StorageInterface)"/>
    public struct StorageInterface
    {
        /// <summary>
        /// The version of this interface
        /// </summary>
        public UInt32 Version;
        
        /// <summary>
        /// Called when the storage is closed
        /// </summary>
        public CloseDelegate Close;
        
        /// <summary>
        /// Optional, returns whether the storage is currently ready for access
        /// </summary>
        public ReadyDelegate Ready;
        
        /// <summary>
        /// Enumerate a directory, optional for write-only storage
        /// </summary>
        public EnumerateDelegate Enumerate;
        
        /// <summary>
        /// Get path information, optional for write-only storage
        /// </summary>
        public InfoDelegate Info;
        
        /// <summary>
        /// Read a file from storage, optional for write-only storage
        /// </summary>
        public ReadFileDelegate ReadFile;
        
        /// <summary>
        /// Write a file to storage, optional for read-only storage
        /// </summary>
        public WriteFileDelegate WriteFile;
        
        /// <summary>
        /// Create a directory, optional for read-only storage
        /// </summary>
        public MkdirDelegate Mkdir;
        
        /// <summary>
        /// Remove a file or empty directory, optional for read-only storage
        /// </summary>
        public RemoveDelegate Remove;
        
        /// <summary>
        /// Rename a path, optional for read-only storage
        /// </summary>
        public RenameDelegate Rename;
        
        /// <summary>
        /// Copy a file, optional for read-only storage
        /// </summary>
        public CopyDelegate Copy;
        
        /// <summary>
        /// Get the space remaining, optional for read-only storage
        /// </summary>
        public SpaceRemainingDelegate SpaceRemaining;
        
        
        public delegate bool CloseDelegate(IntPtr userdata);
        
        public delegate bool ReadyDelegate(IntPtr userdata);
        
        public delegate bool EnumerateDelegate(IntPtr userdata, string path, EnumerateDirectoryCallback callback, IntPtr callbackUserdata);
        
        public delegate bool InfoDelegate(IntPtr userdata, string path, out PathInfo info);
        
        public delegate bool ReadFileDelegate(IntPtr userdata, string path, IntPtr destination, ulong length);
        
        public delegate bool WriteFileDelegate(IntPtr userdata, string path, IntPtr source, ulong length);
        
        public delegate bool MkdirDelegate(IntPtr userdata, string path);
        
        public delegate bool RemoveDelegate(IntPtr userdata, string path);
        
        public delegate bool RenameDelegate(IntPtr userdata, string oldpath, string newpath);
        
        public delegate bool CopyDelegate(IntPtr userdata, string oldpath, string newpath);
        
        public delegate ulong SpaceRemainingDelegate(IntPtr userdata);
    }
}