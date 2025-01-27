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
    /// <para>Specifies the texture format and colorspace of the swapchain textures.</para>
    /// <para>SDR will always be supported. Other compositions may not be supported on
    /// certain systems.</para>
    /// <para>It is recommended to query <see cref="WindowSupportsGPUSwapchainComposition"/> after
    /// claiming the window if you wish to change the swapchain composition from
    /// SDR.</para>
    /// <list type="bullet">
    /// <item><see cref="SDR"/>: B8G8R8A8 or R8G8B8A8 swapchain. Pixel values are in sRGB encoding.</item>
    /// <item><see cref="SDRLinear"/>: B8G8R8A8_SRGB or R8G8B8A8_SRGB swapchain. Pixel values are
    /// stored in memory in sRGB encoding but accessed in shaders in "linear
    /// sRGB" encoding which is sRGB but with a linear transfer function.</item>
    /// <item><see cref="HDRExtendedLinear"/>: <see cref="GPUTextureFormat.R16G16B16A16Float"/> swapchain. Pixel values are in
    /// extended linear sRGB encoding and permits values outside of the [0, 1]
    /// range.</item>
    /// <item><see cref="HDR10ST2084"/>: A2R10G10B10 or A2B10G10R10 swapchain. Pixel values are in
    /// BT.2020 ST2084 (PQ) encoding.</item>
    /// </list>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="SetGPUSwapchainParameters"/>
    /// <seealso cref="WindowSupportsGPUSwapchainComposition"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    public enum GPUSwapchainComposition
    {
        SDR,
        SDRLinear,
        HDRExtendedLinear,
        HDR10ST2084
    }
}