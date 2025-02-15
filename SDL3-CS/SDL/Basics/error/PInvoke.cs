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
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetError(SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(1);</code>
    /// <summary>
    /// <para>Calling this function will replace any previous error message that was set.</para>
    /// <para>This function always returns false, since SDL frequently uses false to
    /// signify a failing result, leading to this idiom:</para>
    /// <code>
    /// if (error_code) {
    ///     return SetError("This operation has failed: {error_code});
    /// }
    /// </code>
    /// </summary>
    /// <param name="message">a printf()-style message format string.</param>
    /// <returns>false</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="GetError"/>
    /// <seealso cref="SetErrorV"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetError"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetError([MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetErrorV(SDL_PRINTF_FORMAT_STRING const char *fmt, va_list ap) SDL_PRINTF_VARARG_FUNCV(1);</code>
    /// <summary>
    /// <para>Set the SDL error message for the current thread.</para>
    /// <para>Calling this function will replace any previous error message that was set.</para>
    /// </summary>
    /// <param name="fmt">a printf()-style message format string.</param>
    /// <param name="ap">a variable argument list.</param>
    /// <returns>false</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.6.</since>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="GetError"/>
    /// <seealso cref="SetError"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetErrorV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SetErrorV([MarshalAs(UnmanagedType.LPUTF8Str)] string fmt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] ap);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_OutOfMemory(void);</code>
    /// <summary>
    /// <para>Set an error indicating that memory allocation failed.</para>
    /// </summary>
    /// <remarks>This function does not do any memory allocation.</remarks>
    /// <returns>false</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OutOfMemory"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool OutOfMemory();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetError"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetError();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetError(void);</code>
    /// <summary>
    /// <para>Retrieve a message about the last error that occurred on the current
    /// thread.</para>
    /// <para>It is possible for multiple errors to occur before calling <see cref="GetError"/>.
    /// Only the last error is returned.</para>
    /// <para>The message is only applicable when an SDL function has signaled an error.
    /// You must check the return values of SDL function calls to determine when to
    /// appropriately call <see cref="GetError"/>. You should <b>not</b> use the results of
    /// <see cref="GetError"/> to decide if an error has occurred! Sometimes SDL will set
    /// an error string even when reporting success.</para>
    /// <para>SDL will <b>not</b> clear the error string for successful API calls. You <b>must</b>
    /// check return values for failure cases before you can assume the error
    /// string applies.
    /// </para>
    /// <para>Error strings are set per-thread, so an error set in a different thread
    /// will not interfere with the current thread's operation.</para>
    /// <para>The returned value is a thread-local string which will remain valid until
    /// the current thread's error string is changed. The caller should make a copy
    /// if the value is needed after the next SDL API call.</para>
    /// </summary>
    /// <returns>a message with information about the specific error that occurred,
    /// or an empty string if there hasn't been an error message set since
    /// the last call to <see cref="ClearError"/>.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearError"/>
    /// <seealso cref="SetError"/>
    public static string GetError()
    {
        var value = SDL_GetError(); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ClearError(void);</code>
    /// <summary>
    /// Clear any previous error message for this thread.
    /// </summary>
    /// <returns>true.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetError"/>
    /// <seealso cref="SetError"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearError"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool ClearError();
}