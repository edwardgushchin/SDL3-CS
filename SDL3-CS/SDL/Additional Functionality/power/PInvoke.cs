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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
	/// <code>extern SDL_DECLSPEC SDL_PowerState SDLCALL SDL_GetPowerInfo(int *seconds, int *percent);</code>
	/// <summary>
	/// <para>Get the current power supply details.</para>
	/// <para>You should never take a battery status as absolute truth. Batteries
	/// (especially failing batteries) are delicate hardware, and the values
	/// reported here are best estimates based on what that hardware reports. It's
	/// not uncommon for older batteries to lose stored power much faster than it
	/// reports, or completely drain when reporting it has 20 percent left, etc.</para>
	/// <para>Battery status can change at any time; if you are concerned with power
	/// state, you should call this function frequently, and perhaps ignore changes
	/// until they seem to be stable for a few seconds.</para>
	/// <para>It's possible a platform can only report battery percentage or time left
	/// but not both.</para>
	/// <para>On some platforms, retrieving power supply details might be expensive. If
	/// you want to display continuous status you could call this function every
	/// minute or so.</para>
	/// </summary>
	/// <param name="seconds">a pointer filled in with the seconds of battery life left,
	/// or <c>null</c> to ignore. This will be filled in with -1 if we
	/// can't determine a value or there is no battery.</param>
	/// <param name="percent">a pointer filled in with the percentage of battery life
	/// left, between 0 and 100, or <c>null</c> to ignore. This will be
	/// filled in with -1 when we can't determine a value or there
	/// is no batter</param>
	/// <returns>the current battery state or <see cref="PowerState.Error"/> on failure;
	/// call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPowerInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial PowerState GetPowerInfo(out int seconds, out int percent);
}