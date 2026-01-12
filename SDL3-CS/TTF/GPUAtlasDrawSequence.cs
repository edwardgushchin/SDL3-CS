#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

public static partial class TTF
{
    /// <summary>
    /// Draw sequence returned by <see cref="GetGPUTextDrawData"/>
    /// </summary>
    /// <since>This struct is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetGPUTextDrawData"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUAtlasDrawSequence
    {
        /// <summary>
        /// Texture atlas that stores the glyphs
        /// </summary>
        public IntPtr AtlasTexture;
        
        /// <summary>
        /// An array of vertex positions
        /// </summary>
        public IntPtr XY;
        
        /// <summary>
        /// An array of normalized texture coordinates for each vertex
        /// </summary>
        public IntPtr UV;
        
        /// <summary>
        /// Number of vertices
        /// </summary>
        public int NumVertices;
        
        /// <summary>
        /// An array of indices into the 'vertices' arrays
        /// </summary>
        public IntPtr Indices;
        
        /// <summary>
        /// Number of indices
        /// </summary>
        public int NumIndices;

        /// <summary>
        /// The image type of this draw sequence
        /// </summary>
        public ImageType ImageType;
    }
}