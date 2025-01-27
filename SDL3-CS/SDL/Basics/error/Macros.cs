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
    /// <code>#define SDL_Unsupported() SDL_SetError("That operation is not supported")</code>
    /// <summary>
    /// <para>A macro to standardize error reporting on unsupported operations.</para>
    /// <para>This simply calls <see cref="SetError"/> with a standardized error string, for
    /// convenience, consistency, and clarity.</para>
    /// </summary>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool Unsupported() => SetError("That operation is not supported");
    
    
    /// <code>#define SDL_InvalidParamError(param) SDL_SetError("Parameter '%s' is invalid", (param))</code>
    /// <summary>
    /// <para>A macro to standardize error reporting on unsupported operations.</para>
    /// <para>This simply calls <see cref="SetError"/> with a standardized error string, for
    /// convenience, consistency, and clarity.</para>
    /// <para>A common usage pattern inside SDL is this:</para>
    /// <code>bool MyFunction(string str) {
    ///     if (!str) {
    ///         return InvalidParamError("str");  // returns false.
    ///     }
    ///     DoSomething(str);
    ///     return true;
    /// }</code>
    /// </summary>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool InvalidParamError(string param) => SetError($"Parameter '{param}' is invalid");
}