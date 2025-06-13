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

/// <summary>
/// <para>SDL_mixer:  An audio mixer library based on the SDL library
/// Copyright (C) 1997-2024 Sam Lantinga slouken@libsdl.org</para>
/// <para>This software is provided 'as-is', without any express or implied
/// warranty.  In no event will the authors be held liable for any damages
/// arising from the use of this software.</para>
/// <para>Permission is granted to anyone to use this software for any purpose,
/// including commercial applications, and to alter it and redistribute it
/// freely, subject to the following restrictions:</para>
/// <list type="numeric">
/// <item>1. The origin of this software must not be misrepresented; you must not
/// claim that you wrote the original software. If you use this software
/// in a product, an acknowledgment in the product documentation would be
/// appreciated but is not required.</item>
/// <item>2. Altered source versions must be plainly marked as such, and must not be
/// misrepresented as being the original software.</item>
/// <item>3. This notice may not be removed or altered from any source distribution.</item>
/// </list>
/// </summary>
public partial class Mixer
{
    private const string MixerLibrary = "SDL2_mixer";
}