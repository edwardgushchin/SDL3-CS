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
    public const int Channels = 8;
        
    public const int DefaultFrequency = 44100;
        
    public const SDL.AudioFormat DefaultFormat = SDL.AudioFormat.AudioS16LE;
        
    public const int DefaultChannels = 2;
        
    public const int MaxVolume = 128;

    /// <summary>
    /// Magic number for effects to operate on the postmix instead of a channel.
    /// </summary>
    public const int ChannelPost = -2;

    /// <summary>
    /// Environment variable that makes some mixing effects favor speed over
    /// quality.
    /// </summary>
    public const string EffectsMaxSpeed = "MIX_EFFECTSMAXSPEED";
}