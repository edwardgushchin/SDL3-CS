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
    /// <para>A structure specifying the parameters of a color target used by a render
    /// pass.</para>
    /// <para>The load_op field determines what is done with the texture at the beginning
    /// of the render pass.</para>
    /// <list type="bullet">
    /// <item>LOAD: Loads the data currently in the texture. Not recommended for
    /// multisample textures as it requires significant memory bandwidth.</item>
    /// <item>CLEAR: Clears the texture to a single color.</item>
    /// <item>DONT_CARE: The driver will do whatever it wants with the texture memory.
    /// This is a good option if you know that every single pixel will be touched
    /// in the render pass.</item>
    /// </list>
    /// <para>The store_op field determines what is done with the color results of the
    /// render pass.</para>
    /// <list type="bullet">
    /// <item>STORE: Stores the results of the render pass in the texture. Not
    /// recommended for multisample textures as it requires significant memory
    /// bandwidth.</item>
    /// <item>DONT_CARE: The driver will do whatever it wants with the texture memory.
    /// This is often a good option for depth/stencil textures.</item>
    /// <item>RESOLVE: Resolves a multisample texture into resolve_texture, which must
    /// have a sample count of 1. Then the driver may discard the multisample
    /// texture memory. This is the most performant method of resolving a
    /// multisample target.</item>
    /// <item>RESOLVE_AND_STORE: Resolves a multisample texture into the
    /// resolve_texture, which must have a sample count of 1. Then the driver
    /// stores the multisample texture's contents. Not recommended as it requires
    /// significant memory bandwidth.</item>
    /// </list>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="BeginGPURenderPass(nint, nint, uint, nint)"/>
    /// <seealso cref="FColor"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUColorTargetInfo
    {
        /// <summary>
        /// The texture that will be used as a color target by a render pass.
        /// </summary>
        public IntPtr Texture;
        
        /// <summary>
        /// The mip level to use as a color target.
        /// </summary>
        public UInt32 MipLevel;
        
        /// <summary>
        /// The layer index or depth plane to use as a color target. This value is treated as a layer index on 2D array and cube textures, and as a depth plane on 3D textures.
        /// </summary>
        public UInt32 LayerOrDepthPlane;
        
        /// <summary>
        /// The color to clear the color target to at the start of the render pass. Ignored if public GPU_LOADOP_CLEAR is not used.
        /// </summary>
        public FColor ClearColor;
        
        /// <summary>
        /// What is done with the contents of the color target at the beginning of the render pass.
        /// </summary>
        public GPULoadOp LoadOp;
        
        /// <summary>
        /// What is done with the results of the render pass.
        /// </summary>
        public GPUStoreOp StoreOp;
        
        /// <summary>
        /// The texture that will receive the results of a multisample resolve operation. Ignored if a RESOLVE* store_op is not used.
        /// </summary>
        public IntPtr ResolveTexture;
        
        /// <summary>
        /// The mip level of the resolve texture to use for the resolve operation. Ignored if a RESOLVE* store_op is not used.
        /// </summary>
        public UInt32 ResolveMipLevel;
        
        /// <summary>
        /// The layer index of the resolve texture to use for the resolve operation. Ignored if a RESOLVE* store_op is not used.
        /// </summary>
        public UInt32 ResolveLayer;
        
        /// <summary>
        /// true cycles the texture if the texture is bound and load_op is not LOAD
        /// </summary>
        public Byte Cycle;
        
        /// <summary>
        /// true cycles the resolve texture if the resolve texture is bound. Ignored if a RESOLVE* store_op is not used.
        /// </summary>
        public Byte CycleResolveTexture;
        
        private Byte _padding1;
        
        private Byte _padding2;
    }
}