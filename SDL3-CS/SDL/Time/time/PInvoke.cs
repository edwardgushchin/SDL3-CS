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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetDateTimeLocalePreferences(SDL_DateFormat *dateFormat, SDL_TimeFormat *timeFormat);</code>
    /// <summary>
    /// <para>Gets the current preferred date and time format for the system locale.</para>
    /// <para>This might be a "slow" call that has to query the operating system. It's
    /// best to ask for this once and save the results. However, the preferred
    /// formats can change, usually because the user has changed a system
    /// preference outside of your program.</para>
    /// </summary>
    /// <param name="dateFormat">a pointer to the <see cref="DateFormat"/> to hold the returned date
    /// format, may be <c>null</c>.</param>
    /// <param name="timeFormat">a pointer to the <see cref="TimeFormat"/> to hold the returned time
    /// format, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDateTimeLocalePreferences"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool GetDateTimeLocalePreferences(out DateFormat dateFormat, out TimeFormat timeFormat);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetCurrentTime(SDL_Time *ticks);</code>
    /// <summary>
    /// <para>Gets the current value of the system realtime clock in nanoseconds since
    /// Jan 1, 1970 in Universal Coordinated Time (UTC).</para>
    /// </summary>
    /// <param name="ticks">the SDL_Time to hold the returned tick count.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentTime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetCurrentTime(out long ticks);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TimeToDateTime(SDL_Time ticks, SDL_DateTime *dt, bool localTime);</code>
    /// <summary>
    /// Converts an SDL_Time in nanoseconds since the epoch to a calendar time in
    /// the <see cref="DateTime"/> format.
    /// </summary>
    /// <param name="ticks">the SDL_Time to be converted.</param>
    /// <param name="dt">the resulting <see cref="DateTime"/>.</param>
    /// <param name="localTime">the resulting <see cref="DateTime"/> will be expressed in local time
    /// if true, otherwise it will be in Universal Coordinated
    /// Time (UTC).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TimeToDateTime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TimeToDateTime(long ticks, out DateTime dt, [MarshalAs(UnmanagedType.I1)] bool localTime);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_DateTimeToTime(const SDL_DateTime *dt, SDL_Time *ticks);</code>
    /// <summary>
    /// <para>Converts a calendar time to an SDL_Time in nanoseconds since the epoch.</para>
    /// <para>This function ignores the day_of_week member of the <see cref="DateTime"/> struct, so
    /// it may remain unset.</para>
    /// </summary>
    /// <param name="dt">the source <see cref="DateTime"/>.</param>
    /// <param name="ticks">the resulting SDL_Time.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DateTimeToTime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool DateTimeToTime(in DateTime dt, out long ticks);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_TimeToWindows(SDL_Time ticks, Uint32 *dwLowDateTime, Uint32 *dwHighDateTime);</code>
    /// <summary>
    /// <para>Converts an SDL time into a Windows FILETIME (100-nanosecond intervals
    /// since January 1, 1601).</para>
    /// <para>This function fills in the two 32-bit values of the FILETIME structure.</para>
    /// </summary>
    /// <param name="ticks">the time to convert.</param>
    /// <param name="dwLowDateTime">a pointer filled in with the low portion of the
    /// Windows FILETIME value.</param>
    /// <param name="dwHighDateTime">a pointer filled in with the high portion of the
    /// Windows FILETIME value.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TimeToWindows"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Time SDLCALL SDL_TimeFromWindows(Uint32 dwLowDateTime, Uint32 dwHighDateTime);</code>
    /// <summary>
    /// <para>Converts a Windows FILETIME (100-nanosecond intervals since January 1,
    /// 1601) to an SDL time.</para>
    /// <para>This function takes the two 32-bit values of the FILETIME structure as
    /// parameters.</para>
    /// </summary>
    /// <param name="dwLowDateTime">the low portion of the Windows FILETIME value.</param>
    /// <param name="dwHighDateTime">the high portion of the Windows FILETIME value.</param>
    /// <returns>the converted SDL time.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TimeFromWindows"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDaysInMonth(int year, int month);</code>
    /// <summary>
    /// Get the number of days in a month for a given year.
    /// </summary>
    /// <param name="year">the year.</param>
    /// <param name="month">the month [1-12].</param>
    /// <returns>the number of days in the requested month or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDaysInMonth"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDaysInMonth(int year, int month);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDayOfYear(int year, int month, int day);</code>
    /// <summary>
    /// Get the day of year for a calendar date.
    /// </summary>
    /// <param name="year">the year component of the date.</param>
    /// <param name="month">the month component of the date.</param>
    /// <param name="day">the day component of the date.</param>
    /// <returns>the day of year [0-365] if the date is valid or -1 on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDayOfYear"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDayOfYear(int year, int month, int day);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetDayOfWeek(int year, int month, int day);</code>
    /// <summary>
    /// Get the day of week for a calendar date.
    /// </summary>
    /// <param name="year">the year component of the date.</param>
    /// <param name="month">the month component of the date.</param>
    /// <param name="day">the day component of the date.</param>
    /// <returns>a value between 0 and 6 (0 being Sunday) if the date is valid or
    /// -1 on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDayOfWeek"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDayOfWeek(int year, int month, int day);
}