#region License
/* Copyright (c) 2024 Eduard Gushchin.
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
    public const uint AudioMaskBitSize = 0xFFu;
    
    public const uint AudioMaskFloat = 1u << 8;
    
    public const uint AudioMaskBigEndian = 1u << 12;
    
    public const uint AudioMaskSigned = 1u << 15;
    
    
    /// <summary>
    /// <para>A value used to request a default playback audio device.</para>
    /// <para>Several functions that require an SDL_AudioDeviceID will accept this value
    /// to signify the app just wants the system to choose a default device instead
    /// of the app providing a specific one.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    public const uint AudioDeviceDefaultPlayback = 0xFFFFFFFFu;
    
    
    /// <summary>
    /// <para>A value used to request a default recording audio device.</para>
    /// <para>Several functions that require an SDL_AudioDeviceID will accept this value
    /// to signify the app just wants the system to choose a default device instead
    /// of the app providing a specific one.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.1.3.</since>
    public const uint AudioDeviceDefaultRecording = 0xFFFFFFFEu;
}