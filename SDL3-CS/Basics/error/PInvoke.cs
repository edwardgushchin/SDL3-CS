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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetError([MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetError(SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(1);</code>
    /// <summary>
    /// <para>Set the SDL error message for the current thread.</para>
    /// <para>Calling this function will replace any previous error message that was set.</para>
    /// <para>This function always returns -1, since SDL frequently uses -1 to signify an
    /// failing result, leading to this idiom:</para>
    /// </summary>
    /// <param name="message">style message format string</param>
    /// <returns>always -1</returns>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="GetError"/>
    public static int SetError(string message) => SDL_SetError(message);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_OutOfMemory();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_OutOfMemory(void);</code>
    /// <summary>
    /// <para>Set an error indicating that memory allocation failed.</para>
    /// <para>This function does not do any memory allocation.</para>
    /// </summary>
    /// <returns>returns -1.</returns>
    public static int OutOfMemory() => SDL_OutOfMemory();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetError();
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetError(void);</code>
    /// <summary>
    /// <para>Retrieve a message about the last error that occurred on the current
    /// thread.</para>
    /// <para>It is possible for multiple errors to occur before calling <see cref="GetError"/>.
    /// Only the last error is returned.</para>
    /// <para>The message is only applicable when an SDL function has signaled an error.
    /// You must check the return values of SDL function calls to determine when to
    /// appropriately call <see cref="GetError"/>. You should *not* use the results of
    /// <see cref="GetError"/> to decide if an error has occurred! Sometimes SDL will set
    /// an error string even when reporting success.</para>
    /// <para>SDL will *not* clear the error string for successful API calls. You *must*
    /// check return values for failure cases before you can assume the error
    /// string applies.</para>
    /// <para>Error strings are set per-thread, so an error set in a different thread
    /// will not interfere with the current thread's operation.</para>
    /// <para>The returned string does **NOT** follow the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>! The pointer
    /// is valid until the current thread's error string is changed, so the caller
    /// should make a copy if the string is to be used after calling into SDL
    /// again.</para>
    /// </summary>
    /// <returns>a message with information about the specific error that occurred,
    /// or an empty string if there hasn't been an error message set since
    /// the last call to <see cref="ClearError"/>.</returns>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="SetError"/>
    public static string? GetError() => Marshal.PtrToStringUTF8(SDL_GetError());
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ClearError();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ClearError(void);</code>
    /// <summary>
    /// Clear any previous error message for this thread.
    /// </summary>
    /// <returns>returns 0.</returns>
    /// <seealso cref="GetError"/>
    /// <seealso cref="SetError"/>
    public static int ClearError() => SDL_ClearError();
}