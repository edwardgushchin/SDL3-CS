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
    /// <code>#define SDL_SECONDS_TO_NS(S)    (((Uint64)(S)) * SDL_NS_PER_SECOND)</code>
    /// <summary>
    /// <para>Convert seconds to nanoseconds.</para>
    /// <para>This only converts whole numbers, not fractional seconds.</para>
    /// </summary>
    /// <param name="s">the number of seconds to convert.</param>
    /// <returns>expressed in nanoseconds.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    [Macro]
    public static ulong SecondsToNs(ulong s) => s * NsPerSecond;
    
    
    /// <code>#define SDL_NS_TO_SECONDS(NS)   ((NS) / SDL_NS_PER_SECOND)</code>
    /// <summary>
    /// <para>Convert nanoseconds to seconds.</para>
    /// <para>This performs a division, so the results can be dramatically different if
    /// <c>ns</c> is an integer or floating point value.</para>
    /// </summary>
    /// <param name="ns">the number of nanoseconds to convert.</param>
    /// <returns>expressed in seconds.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static ulong NsToSeconds(ulong ns) => ns / NsPerSecond;
    
    
    /// <code>#define SDL_MS_TO_NS(MS)        (((Uint64)(MS)) * SDL_NS_PER_MS)</code>
    /// <summary>
    /// <para>Convert milliseconds to nanoseconds.</para>
    /// <para>This only converts whole numbers, not fractional milliseconds.</para>
    /// </summary>
    /// <param name="ms">the number of milliseconds to convert.</param>
    /// <returns>expressed in nanoseconds.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static ulong MsToNs(ulong ms) => ms * NsPerMs;
    
    
    /// <code>#define SDL_NS_TO_MS(NS)        ((NS) / SDL_NS_PER_MS)</code>
    /// <summary>
    /// <para>Convert nanoseconds to milliseconds.</para>
    /// <para>This performs a division, so the results can be dramatically different if
    /// <c>ns</c> is an integer or floating point value.</para>
    /// </summary>
    /// <param name="ns">the number of nanoseconds to convert.</param>
    /// <returns>expressed in milliseconds.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static ulong NsToMs(ulong ns) => ns / NsPerMs;
    
    
    /// <code>#define SDL_US_TO_NS(US)        (((Uint64)(US)) * SDL_NS_PER_US)</code>
    /// <summary>
    /// <para>Convert microseconds to nanoseconds.</para>
    /// <para>This only converts whole numbers, not fractional microseconds.</para>
    /// </summary>
    /// <param name="us">the number of microseconds to convert.</param>
    /// <returns>expressed in nanoseconds.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static ulong UsToNs(ulong us) => us * NsPerUs;
    
    
    /// <code>#define SDL_NS_TO_US(NS)        ((NS) / SDL_NS_PER_US)</code>
    /// <summary>
    /// <para>Convert nanoseconds to microseconds.</para>
    /// <para>This performs a division, so the results can be dramatically different if
    /// <c>ns</c> is an integer or floating point value.</para>
    /// </summary>
    /// <param name="ns">the number of nanoseconds to convert.</param>
    /// <returns>expressed in microseconds.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static ulong NsToUs(ulong ns) => ns / NsPerUs;
}