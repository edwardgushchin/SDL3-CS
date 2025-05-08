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
    /// A structure specifying parameters related to transferring data to or from a
    /// texture.
    /// <para>If either of <c>PixelsPerRow</c> or <c>RowsPerLayer</c> is zero, then width and
    /// height of passed <see cref="GPUTextureRegion"/> to <see cref="UploadToGPUTexture"/> or
    /// <see cref="DownloadFromGPUTexture"/> are used as default values respectively and data
    /// is considered to be tightly packed.</para>
    /// <para><b>WARNING</b>: Direct3D 12 requires texture data row pitch to be 256 byte
    /// aligned, and offsets to be aligned to 512 bytes. If they are not, SDL will
    /// make a temporary copy of the data that is properly aligned, but this adds
    /// overhead to the transfer process. Apps can avoid this by aligning their
    /// data appropriately, or using a different GPU backend than Direct3D 12.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="UploadToGPUTexture"/>
    /// <seealso cref="DownloadFromGPUTexture"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUTextureTransferInfo
    {
        /// <summary>
        /// The transfer buffer used in the transfer operation.
        /// </summary>
        public IntPtr TransferBuffer;

        /// <summary>
        /// The starting byte of the image data in the transfer buffer.
        /// </summary>
        public UInt32 Offset;
        
        /// <summary>
        /// The number of pixels from one row to the next.
        /// </summary>
        public UInt32 PixelsPerRow;

        /// <summary>
        /// The number of rows from one layer/depth-slice to the next.
        /// </summary>
        public UInt32 RowsPerLayer;
    }
}