#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
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
       RedSize,
       GreenSize,
       BlueSize,
       AlphaSize,
       BufferSize,
       Doublebuffer,
       DepthSize,
       StencilSize,
       AccumRedSize,
       AccumGreenSize,
       AccumBlueSize,
       AccumAlphaSize,
       Stereo,
       Multisamplebuffers,
       Multisamplesamples,
       AcceleratedVisual,
       RetainedBacking,
       ContextMajorVersion,
       ContextMinorVersion,
       ContextFlags,
       ContextProfileMask,
       ShareWithCurrentContext,
       FramebufferSrgbCapable,
       ContextReleaseBehavior,
       ContextResetNotification,
       ContextNoError,
       Floatbuffers,
       EGLPlatform
    }
}