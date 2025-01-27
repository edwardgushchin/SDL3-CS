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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC SDL_BlendMode SDLCALL SDL_ComposeCustomBlendMode(SDL_BlendFactor srcColorFactor,
    /// SDL_BlendFactor dstColorFactor,
    /// SDL_BlendOperation colorOperation,
    /// SDL_BlendFactor srcAlphaFactor,
    /// SDL_BlendFactor dstAlphaFactor,
    /// SDL_BlendOperation alphaOperation);</code>
    /// <summary>
    /// <para>Compose a custom blend mode for renderers.</para>
    /// <para>The functions <see cref="SetRenderDrawBlendMode"/> and <seealso cref="SetTextureBlendMode"/> accept
    /// the <see cref="BlendMode"/> returned by this function if the renderer supports it.</para>
    /// <para>A blend mode controls how the pixels from a drawing operation (source) get
    /// combined with the pixels from the render target (destination). First, the
    /// components of the source and destination pixels get multiplied with their
    /// blend factors. Then, the blend operation takes the two products and
    /// calculates the result that will get stored in the render target.</para>
    /// <para>Expressed in pseudocode, it would look like this:</para>
    /// <code>
    /// dstRGB = colorOperation(srcRGB * srcColorFactor, dstRGB * dstColorFactor);
    /// dstA = alphaOperation(srcA * srcAlphaFactor, dstA * dstAlphaFactor);
    /// </code>
    /// <para>Where the functions <c>colorOperation(src, dst)</c> and <c>alphaOperation(src,
    /// dst)</c> can return one of the following:</para>
    /// <list type="bullet">
    /// <item><c>src + dst</c></item>
    /// <item><c>src - dst</c></item>
    /// <item><c>dst - src</c></item>
    /// <item><c>min(src, dst)</c></item>
    /// <item><c>max(src, dst)</c></item>
    /// </list>
    /// <para>The red, green, and blue components are always multiplied with the first,
    /// second, and third components of the <see cref="BlendFactor"/>, respectively. The
    /// fourth component is not used.</para>
    /// <para>The alpha component is always multiplied with the fourth component of the
    /// <see cref="BlendFactor"/>. The other components are not used in the alpha
    /// calculation.</para>
    /// <para>Support for these blend modes varies for each renderer. To check if a
    /// specific <see cref="BlendMode"/> is supported, create a renderer and pass it to
    /// either <see cref="SetRenderDrawBlendMode"/> or <see cref="SetTextureBlendMode"/>. They will
    /// return with an error if the blend mode is not supported.</para>
    /// <para>This list describes the support of custom blend modes for each renderer.
    /// All renderers support the four blend modes listed in the SDL_BlendMode
    /// enumeration.</para>
    /// <list type="bullet">
    /// <item><b>direct3d</b>: Supports all operations with all factors. However, some
    /// factors produce unexpected results with <see cref="BlendOperation.Minimum"/> and
    /// <see cref="BlendOperation.Maximum"/>.</item>
    /// <item><b>direct3d11</b>: Same as Direct3D 9.</item>
    /// <item><b>opengl</b>: Supports the <see cref="BlendOperation.Add"/> operation with all
    /// factors. OpenGL versions 1.1, 1.2, and 1.3 do not work correctly here.</item>
    /// <item><b>opengles2</b>: Supports the <see cref="BlendOperation.Add"/>,
    /// <see cref="BlendOperation.Subtract"/>, <see cref="BlendOperation.RevSubtract"/>
    /// operations with all factors.</item>
    /// <item><b>psp</b>: No custom blend mode support.</item>
    /// <item><b>software</b>: No custom blend mode support.</item>
    /// </list>
    /// <para>Some renderers do not provide an alpha component for the default render
    /// target. The <see cref="BlendFactor.DstAlpha"/> and
    /// <see cref="BlendFactor.OneMinusDstAlpha"/> factors do not have an effect in this
    /// case.</para>
    /// </summary>
    /// <param name="srcColorFactor">the <see cref="BlendFactor"/> applied to the red, green, and
    /// blue components of the source pixels.</param>
    /// <param name="dstColorFactor">the <see cref="BlendFactor"/> applied to the red, green, and
    /// blue components of the destination pixels.</param>
    /// <param name="colorOperation">the <see cref="BlendOperation"/> used to combine the red,
    /// green, and blue components of the source and
    /// destination pixels.</param>
    /// <param name="srcAlphaFactor">the <see cref="BlendFactor"/> applied to the alpha component of
    /// the source pixels.</param>
    /// <param name="dstAlphaFactor">the <see cref="BlendFactor"/> applied to the alpha component of
    /// the destination pixels.</param>
    /// <param name="alphaOperation">the <see cref="BlendOperation"/> used to combine the alpha
    /// component of the source and destination pixels.</param>
    /// <returns>an <see cref="BlendMode"/> that represents the chosen factors and
    /// operations.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderDrawBlendMode"/>
    /// <seealso cref="GetRenderDrawBlendMode"/>
    /// <seealso cref="SetTextureBlendMode"/>
    /// <seealso cref="GetTextureBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ComposeCustomBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial BlendMode ComposeCustomBlendMode(BlendFactor srcColorFactor, 
        BlendFactor dstColorFactor,
        BlendOperation colorOperation, 
        BlendFactor srcAlphaFactor, 
        BlendFactor dstAlphaFactor,
        BlendOperation alphaOperation);
}