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

public partial class Mixer
{
    /// <summary>
    /// A set of per-channel gains for tracks using <see cref="SetTrackStereo"/>.
    /// <para>When forcing a track to stereo, the app can specify a per-channel gain, to
    /// further adjust the left or right outputs.</para>
    /// <para>When mixing audio that has been forced to stereo, each channel is modulated
    /// by these values. A value of 1.0f produces no change, 0.0f produces silence.</para>
    /// <para>A simple panning effect would be to set <c>left</c> to the desired value and
    /// <c>right</c> to <c>1.0f - left</c>.</para>
    /// </summary>
    /// <since>This struct is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrackStereo"/>
    public struct StereoGains
    {
        /// <summary>
        /// left channel gain
        /// </summary>
        public float Left;
        
        /// <summary>
        /// right channel gain
        /// </summary>
        public float Right;
    }
}