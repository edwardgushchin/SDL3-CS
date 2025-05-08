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
    /// <para>An enumeration of OpenGL configuration attributes.</para>
    /// <para>While you can set most OpenGL attributes normally, the attributes listed
    /// above must be known before SDL creates the window that will be used with
    /// the OpenGL context. These attributes are set and read with
    /// <see cref="GLSetAttribute"/> and <see cref="GLGetAttribute"/>.</para>
    /// <para>In some cases, these attributes are minimum requests; the GL does not
    /// promise to give you exactly what you asked for. It's possible to ask for a
    /// 16-bit depth buffer and get a 24-bit one instead, for example, or to ask
    /// for no stencil buffer and still have one available. Context creation should
    /// fail if the GL can't provide your requested attributes at a minimum, but
    /// you should check to see exactly what you got.</para>
    /// </summary>
    public enum GLAttr
    {
        /// <summary>
        /// the minimum number of bits for the red channel of the color buffer; defaults to 8.
        /// </summary>
        RedSize,
        
        /// <summary>
        /// the minimum number of bits for the green channel of the color buffer; defaults to 8.
        /// </summary>
        GreenSize,
        
        /// <summary>
        /// the minimum number of bits for the blue channel of the color buffer; defaults to 8.
        /// </summary>
        BlueSize,
        
        /// <summary>
        /// the minimum number of bits for the alpha channel of the color buffer; defaults to 8
        /// </summary>
        AlphaSize,
        
        /// <summary>
        /// the minimum number of bits for frame buffer size; defaults to 0.
        /// </summary>
        BufferSize,
        
        /// <summary>
        /// whether the output is single or double buffered; defaults to double buffering on.
        /// </summary>
        DoubleBuffer,
        
        /// <summary>
        /// the minimum number of bits in the depth buffer; defaults to 16.
        /// </summary>
        DepthSize,
        
        /// <summary>
        /// the minimum number of bits in the stencil buffer; defaults to 0.
        /// </summary>
        StencilSize,
        
        /// <summary>
        /// the minimum number of bits for the red channel of the accumulation buffer; defaults to 0.
        /// </summary>
        AccumRedSize,
        
        /// <summary>
        /// the minimum number of bits for the green channel of the accumulation buffer; defaults to 0.
        /// </summary>
        AccumGreenSize,
        
        /// <summary>
        /// the minimum number of bits for the blue channel of the accumulation buffer; defaults to 0.
        /// </summary>
        AccumBlueSize,
        
        /// <summary>
        /// the minimum number of bits for the alpha channel of the accumulation buffer; defaults to 0.
        /// </summary>
        AccumAlphaSize,
        
        /// <summary>
        /// whether the output is stereo 3D; defaults to off.
        /// </summary>
        Stereo,
        
        /// <summary>
        /// the number of buffers used for multisample anti-aliasing; defaults to 0.
        /// </summary>
        MultisampleBuffers,
        
        /// <summary>
        /// the number of samples used around the current pixel used for multisample anti-aliasing.
        /// </summary>
        MultisampleSamples,
        
        /// <summary>
        /// set to 1 to require hardware acceleration, set to 0 to force software rendering; defaults to allow either.
        /// </summary>
        AcceleratedVisual,
        
        /// <summary>
        /// not used (deprecated).
        /// </summary>
        RetainedBacking,
        
        /// <summary>
        /// OpenGL context major version.
        /// </summary>
        ContextMajorVersion,
        
        /// <summary>
        /// OpenGL context minor version.
        /// </summary>
        ContextMinorVersion,
        
        /// <summary>
        /// some combination of 0 or more of elements of the SDL_GLContextFlag enumeration; defaults to 0.
        /// </summary>
        ContextFlags,
        
        /// <summary>
        /// type of GL context (Core, Compatibility, ES). See SDL_GLProfile; default value depends on platform.
        /// </summary>
        ContextProfileMask,
        
        /// <summary>
        /// OpenGL context sharing; defaults to 0.
        /// </summary>
        ShareWithCurrentContext,
        
        
        /// <summary>
        /// requests sRGB capable visual; defaults to 0.
        /// </summary>
        FrameBufferSRGBCapable,
        
        /// <summary>
        /// sets context the release behavior. See <see cref="GLСontextReleaseFlag"/>; defaults to <see cref="GLСontextReleaseFlag.Flush"/>.
        /// </summary>
        ContextReleaseBehavior,
        
        /// <summary>
        /// set context reset notification. See <see cref="GLContextResetNotification"/>; defaults to <see cref="GLContextResetNotification.NoNotification"/>.
        /// </summary>
        ContextResetNotification,
        ContextNoError,
        FloatBuffers,
        EGLPlatform
    }
}