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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
	/// <summary>
	/// The basic state for the system's power supply.
	/// </summary>
	/// <remarks>These are results returned by <see cref="GetPowerInfo"/>.</remarks>
	public enum PowerState
	{
		/// <summary>
		/// error determining power status
		/// </summary>
		Error = -1,

		/// <summary>
		/// cannot determine power status
		/// </summary>
		Unknown,

		/// <summary>
		/// Not plugged in, running on the battery 
		/// </summary>
		OnBattery,

		/// <summary>
		/// Plugged in, no battery available
		/// </summary>
		NoBattery,

		/// <summary>
		/// Plugged in, charging battery
		/// </summary>
		Charging,

		/// <summary>
		/// Plugged in, battery charged
		/// </summary>
		Charged
	}

	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetPowerInfo(int seconds, int percent);

	/// <summary>
	/// Get the current power supply details.
	/// </summary>
	/// <param name="seconds">a pointer filled in with the seconds of battery life left, or NULL to ignore.
	/// This will be filled in with -1 if we can't determine a value or there is no battery.</param>
	/// <param name="percent">a pointer filled in with the percentage of battery life left,
	/// between 0 and 100, or NULL to ignore. This will be filled in with -1 we can't determine a value or
	/// there is no battery.</param>
	/// <returns>Returns the current battery state or <see cref="PowerState.Error"/> on failure;
	/// call <see cref="GetError"/> for more information.</returns>
	public static PowerState GetPowerInfo(int seconds, int percent) => SDL_GetPowerInfo(seconds, percent);
}