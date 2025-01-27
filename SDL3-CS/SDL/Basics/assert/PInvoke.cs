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
    /// <code>extern SDL_DECLSPEC SDL_AssertState SDLCALL SDL_ReportAssertion(SDL_AssertData *data, const char *func, const char *file, int line) SDL_ANALYZER_NORETURN;</code>
    /// <summary>
    /// <para>Never call this directly.</para>
    /// <para>Use the SDL_assert macros instead.</para>
    /// </summary>
    /// <param name="data">assert data structure.</param>
    /// <param name="func">function name.</param>
    /// <param name="file">file name.</param>
    /// <param name="line">line number.</param>
    /// <returns>assert state.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReportAssertion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AssertState ReportAssertion(IntPtr data, [MarshalAs(UnmanagedType.LPUTF8Str)] string func, [MarshalAs(UnmanagedType.LPUTF8Str)] string file, int line);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetAssertionHandler(SDL_AssertionHandler handler, void *userdata);</code>
    /// <summary>
    /// <para>Set an application-defined assertion handler.</para>
    /// <para>This function allows an application to show its own assertion UI and/or
    /// force the response to an assertion failure. If the application doesn't
    /// provide this, SDL will try to do the right thing, popping up a
    /// system-specific GUI dialog, and probably minimizing any fullscreen windows.</para>
    /// <para>This callback may fire from any thread, but it runs wrapped in a mutex, so
    /// it will only fire from one thread at a time.</para>
    /// <para>This callback is NOT reset to SDL's internal handler upon <see cref="Quit"/>!</para>
    /// </summary>
    /// <param name="handler">the <see cref="AssertionHandler"/> function to call when an assertion
    /// fails or <c>null</c> for the default handler.</param>
    /// <param name="userdata">a pointer that is passed to <c>handler</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAssertionHandler"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAssertionHandler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAssertionHandler(AssertionHandler handler, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC SDL_AssertionHandler SDLCALL SDL_GetDefaultAssertionHandler(void);</code>
    /// <summary>
    /// <para>Get the default assertion handler.</para>
    /// <para>This returns the function pointer that is called by default when an
    /// assertion is triggered. This is an internal function provided by SDL, that
    /// is used for assertions when <see cref="SetAssertionHandler"/> hasn't been used to
    /// provide a different function.</para>
    /// </summary>
    /// <returns>the default <see cref="AssertionHandler"/> that is called when an assert
    /// triggers.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAssertionHandler"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDefaultAssertionHandler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AssertionHandler GetDefaultAssertionHandler();
    
    
    /// <code>extern SDL_DECLSPEC SDL_AssertionHandler SDLCALL SDL_GetAssertionHandler(void **puserdata);</code>
    /// <summary>
    /// <para>Get the current assertion handler.</para>
    /// <para>This returns the function pointer that is called when an assertion is
    /// triggered. This is either the value last passed to
    /// <see cref="SetAssertionHandler"/>, or if no application-specified function is set,
    /// is equivalent to calling <see cref="GetDefaultAssertionHandler"/>.</para>
    /// <para>The parameter <c>puserdata</c> is a pointer to a void*, which will store the
    /// "userdata" pointer that was passed to <see cref="SetAssertionHandler"/>. This value
    /// will always be <c>null</c> for the default handler. If you don't care about this
    /// data, it is safe to pass a <c>null</c> pointer to this function to ignore it.</para>
    /// </summary>
    /// <param name="puserdata">pointer which is filled with the "userdata" pointer that
    /// was passed to <see cref="SetAssertionHandler"/>.</param>
    /// <returns>the <see cref="AssertionHandler"/> that is called when an assert triggers.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAssertionHandler"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAssertionHandler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial AssertionHandler GetAssertionHandler(IntPtr puserdata);
    
    
    /// <code>extern SDL_DECLSPEC const SDL_AssertData * SDLCALL SDL_GetAssertionReport(void);</code>
    /// <summary>
    /// <para>Get a list of all assertion failures.</para>
    /// <para>This function gets all assertions triggered since the last call to
    /// <see cref="ResetAssertionReport"/>, or the start of the program.</para>
    /// <para>The proper way to examine this data looks something like this:</para>
    /// <code>const SDL_AssertData *item = SDL_GetAssertionReport();
    /// while (item) {
    ///    printf("'%s', %s (%s:%d), triggered %u times, always ignore: %s.\\n",
    ///           item->condition, item->function, item->filename,
    ///           item->linenum, item->trigger_count,
    ///           item->always_ignore ? "yes" : "no");
    ///    item = item->next;</code>
    /// </summary>
    /// <returns>a list of all failed assertions or <c>null</c> if the list is empty. This
    /// memory should not be modified or freed by the application. This
    /// pointer remains valid until the next call to <see cref="Quit"/> or
    /// <see cref="ResetAssertionReport"/>.</returns>
    /// <threadsafety>This function is not thread safe. Other threads calling
    /// <see cref="ResetAssertionReport"/> simultaneously, may render the
    /// returned pointer invalid.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ResetAssertionReport"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAssertionReport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetAssertionReport();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetAssertionReport(void);</code>
    /// <summary>
    /// <para>Clear the list of all assertion failures.</para>
    /// <para>This function will clear the list of all assertions triggered up to that
    /// point. Immediately following this call, <see cref="GetAssertionReport"/> will return
    /// no items. In addition, any previously-triggered assertions will be reset to
    /// a trigger_count of zero, and their always_ignore state will be false.</para>
    /// </summary>
    /// <threadsafety>This function is not thread safe. Other threads triggering an
    /// assertion, or simultaneously calling this function may cause
    /// memory leaks or crashes.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAssertionReport"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResetAssertionReport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAssertionReport();
}