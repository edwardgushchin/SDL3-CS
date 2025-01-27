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
    /// <para>Specifies the timing that will be used to present swapchain textures to the
    /// OS.</para>
    /// <para><see cref="VSync"/> mode will always be supported. <see cref="Immediate"/> and <see cref="Mailbox"/> modes may not be
    /// supported on certain systems.</para>
    /// <para>It is recommended to query <see cref="WindowSupportsGPUPresentMode"/> after claiming
    /// the window if you wish to change the present mode to <see cref="Immediate"/> or <see cref="Mailbox"/>.</para>
    /// <list type="bullet">
    /// <item><see cref="VSync"/>: Waits for vblank before presenting. No tearing is possible. If
    /// there is a pending image to present, the new image is enqueued for
    /// presentation. Disallows tearing at the cost of visual latency.</item>
    /// <item><see cref="Immediate"/>: Immediately presents. Lowest latency option, but tearing may
    /// occur.</item>
    /// <item><see cref="Mailbox"/>: Waits for vblank before presenting. No tearing is possible. If
    /// there is a pending image to present, the pending image is replaced by the
    /// new image. Similar to VSYNC, but with reduced visual latency.</item>
    /// </list>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="SetGPUSwapchainParameters"/>
    /// <seealso cref="WindowSupportsGPUPresentMode"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    public enum GPUPresentMode
    {
        VSync,
        Immediate,
        Mailbox
    }
}