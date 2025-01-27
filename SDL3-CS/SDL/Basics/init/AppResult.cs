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
    /// <para>Return values for optional main callbacks.</para>
    /// <para>Returning <see cref="Success"/> or <see cref="Failure"/> from <see cref="AppInit"/>,
    /// <see cref="AppEvent"/>, or <see cref="AppIterate"/> will terminate the program and report
    /// success/failure to the operating system. What that means is
    /// platform-dependent. On Unix, for example, on success, the process error
    /// code will be zero, and on failure it will be 1. This interface doesn't
    /// allow you to return specific exit codes, just whether there was an error
    /// generally or not.</para>
    /// <para>Returning <see cref="Continue"/> from these functions will let the app continue
    /// to run.</para>
    /// <para>See
    /// <a href="https://wiki.libsdl.org/SDL3/README/main-functions#main-callbacks-in-sdl3">Main callbacks in SDL3</a>
    /// for complete details.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum AppResult
    {
        Continue,
        Success,
        Failure
    }
}