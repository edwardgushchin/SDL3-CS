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
    /// <para>A structure specifying the parameters of a depth-stencil target used by a
    /// render pass.</para>
    /// <para>The load_op field determines what is done with the depth contents of the
    /// texture at the beginning of the render pass.</para>
    /// <list type="bullet">
    /// <item>LOAD: Loads the depth values currently in the texture.</item>
    /// <item>CLEAR: Clears the texture to a single depth.</item>
    /// <item>DONT_CARE: The driver will do whatever it wants with the memory. This is
    /// a good option if you know that every single pixel will be touched in the
    /// render pass.</item>
    /// </list>
    /// <para>The store_op field determines what is done with the depth results of the
    /// render pass.</para>
    /// <list type="bullet">
    /// <item>STORE: Stores the depth results in the texture.</item>
    /// <item>DONT_CARE: The driver will do whatever it wants with the depth results.
    /// This is often a good option for depth/stencil textures that don't need to
    /// be reused again.</item>
    /// </list>
    /// <para>The stencil_load_op field determines what is done with the stencil contents
    /// of the texture at the beginning of the render pass.</para>
    /// <list type="bullet">
    /// <item>LOAD: Loads the stencil values currently in the texture.</item>
    /// <item>CLEAR: Clears the stencil values to a single value.</item>
    /// <item>DONT_CARE: The driver will do whatever it wants with the memory. This is
    /// a good option if you know that every single pixel will be touched in the
    /// render pass.</item>
    /// </list>
    /// <para>The stencil_store_op field determines what is done with the stencil results
    /// of the render pass.</para>
    /// <list type="bullet">
    /// <item>STORE: Stores the stencil results in the texture.</item>
    /// <item>DONT_CARE: The driver will do whatever it wants with the stencil results.
    /// This is often a good option for depth/stencil textures that don't need to
    /// be reused again.</item>
    /// </list>
    /// <para>Note that depth/stencil targets do not support multisample resolves.</para>
    /// <para>Due to ABI limitations, depth textures with more than 255 layers are not
    /// supported.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="BeginGPURenderPass(nint, nint, uint, nint)"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUDepthStencilTargetInfo
    {
        /// <summary>
        /// The texture that will be used as the depth stencil target by the render pass.
        /// </summary>
        public IntPtr Texture;
        
        /// <summary>
        /// The value to clear the depth component to at the beginning of the render pass. Ignored if public GPU_LOADOP_CLEAR is not used.
        /// </summary>
        public float ClearDepth;
        
        /// <summary>
        /// What is done with the depth contents at the beginning of the render pass.
        /// </summary>
        public GPULoadOp LoadOp;
        
        /// <summary>
        /// What is done with the depth results of the render pass.
        /// </summary>
        public GPUStoreOp StoreOp;
        
        /// <summary>
        /// What is done with the stencil contents at the beginning of the render pass.
        /// </summary>
        public GPULoadOp StencilLoadOp;
        
        /// <summary>
        /// What is done with the stencil results of the render pass.
        /// </summary>
        public GPUStoreOp StencilStoreOp;
        
        /// <summary>
        /// true cycles the texture if the texture is bound and any load ops are not LOAD 
        /// </summary>
        public Byte Cycle;
        
        /// <summary>
        /// The value to clear the stencil component to at the beginning of the render pass. Ignored if public GPU_LOADOP_CLEAR is not used.
        /// </summary>
        public Byte ClearStencil;
        
        /// <summary>
        /// The mip level to use as the depth stencil target.
        /// </summary>
        public Byte MipLevel;
        
        /// <summary>
        /// The layer index to use as the depth stencil target.
        /// </summary>
        public Byte Layer;
    }
}