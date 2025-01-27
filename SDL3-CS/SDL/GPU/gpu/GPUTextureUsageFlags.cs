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
    /// <para>Specifies how a texture is intended to be used by the client.</para>
    /// <para>A texture must have at least one usage flag. Note that some usage flag
    /// combinations are invalid.</para>
    /// <para>With regards to compute storage usage, READ | WRITE means that you can have
    /// shader A that only writes into the texture and shader B that only reads
    /// from the texture and bind the same texture to either shader respectively.
    /// SIMULTANEOUS means that you can do reads and writes within the same shader
    /// or compute pass. It also implies that atomic ops can be used, since those
    /// are read-modify-write operations. If you use SIMULTANEOUS, you are
    /// responsible for avoiding data races, as there is no data synchronization
    /// within a compute pass. Note that SIMULTANEOUS usage is only supported by a
    /// limited number of texture formats.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUTexture"/>
    [Flags]
    public enum GPUTextureUsageFlags : uint
    {
        /// <summary>
        /// Texture supports sampling.
        /// </summary>
        Sampler = 1u << 0,
        
        /// <summary>
        /// Texture is a color render target.
        /// </summary>
        ColorTarget = 1u << 1,
        
        /// <summary>
        /// Texture is a depth stencil target.
        /// </summary>
        DepthStencilTarget = 1u << 2,
        
        /// <summary>
        /// Texture supports storage reads in graphics stages.
        /// </summary>
        GraphicsStorageRead = 1u << 3,
        
        /// <summary>
        /// Texture supports storage reads in the compute stage.
        /// </summary>
        ComputeStorageRead = 1u << 4,
        
        /// <summary>
        /// Texture supports storage writes in the compute stage.
        /// </summary>
        ComputeStorageWrite = 1u << 5,
        
        /// <summary>
        /// Texture supports reads and writes in the same compute shader. This is NOT equivalent to READ | WRITE.
        /// </summary>
        ComputeStorageSimultaneousReadWrite = 1u << 6
    }
}