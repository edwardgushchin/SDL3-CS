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
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetHintWithPriority(const char *name, const char *value, SDL_HintPriority priority);</code>
    /// <summary>
    /// <para>Set a hint with a specific priority.</para>
    /// <para>The priority controls the behavior when setting a hint that already has a
    /// value. Hints will replace existing hints of their priority and lower.
    /// Environment variables are considered to have override priority.</para>
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">the value of the hint variable.</param>
    /// <param name="priority">the <see cref="HintPriority"/> level for the hint.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="ResetHint"/>
    /// <seealso cref="SetHint"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetHintWithPriority"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetHintWithPriority([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value, HintPriority priority);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetHint(const char *name, const char *value);</code>
    /// <summary>
    /// <para>Set a hint with normal priority.</para>
    /// <para>Hints will not be set if there is an existing override hint or environment
    /// variable that takes precedence. You can use <see cref="SetHintWithPriority"/> to
    /// set the hint with override priority instead.</para>
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">the value of the hint variable.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="ResetHint"/>
    /// <seealso cref="SetHintWithPriority"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetHint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ResetHint(const char *name);</code>
    /// <summary>
    /// <para>Reset a hint to the default value.</para>
    /// <para>This will reset a hint to the value of the environment variable, or <c>null</c> if
    /// the environment isn't set. Callbacks will be called normally with this
    /// change.</para>
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetHint"/>
    /// <seealso cref="ResetHints"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResetHint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetHints(void);</code>
    /// <summary>
    /// <para>Reset all hints to the default values.</para>
    /// <para>This will reset all hints to the value of the associated environment
    /// variable, or <c>null</c> if the environment isn't set. Callbacks will be called
    /// normally with this change.</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ResetHint"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResetHints"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial void ResetHints();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetHint(const char *name);</code>
    /// <summary>
    /// Get the value of a hint.
    /// </summary>
    /// <param name="name">name the hint to query.</param>
    /// <returns>the string value of a hint or <c>null</c> if the hint isn't set.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetHint"/>
    /// <seealso cref="SetHintWithPriority"/>
    public static string? GetHint(string name)
    {
        var value = SDL_GetHint(name); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetHintBoolean(const char *name, bool default_value);</code>
    /// <summary>
    /// Get the boolean value of a hint variable.
    /// </summary>
    /// <param name="name">the name of the hint to get the boolean value from.</param>
    /// <param name="defaultValue">the value to return if the hint does not exist.</param>
    /// <returns>the boolean value of a hint or the provided default value if the
    /// hint does not exist.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="SetHint"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHintBoolean"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetHintBoolean([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.I1)] bool defaultValue);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AddHintCallback(const char *name, SDL_HintCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Add a function to watch a particular hint.</para>
    /// <para>The callback function is called _during_ this function, to provide it an
    /// initial value, and again each time the hint's value changes.</para>
    /// </summary>
    /// <param name="name">the hint to watch.</param>
    /// <param name="callback">An <see cref="HintCallback"/> function that will be called when the
    /// hint value changes.</param>
    /// <param name="userdata">a pointer to pass to the callback function.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RemoveHintCallback"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddHintCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int AddHintCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        HintCallback callback, IntPtr userdata);
        
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_RemoveHintCallback(const char *name, SDL_HintCallback callback, void *userdata);</code>
    /// <summary>
    /// Remove a function watching a particular hint.
    /// </summary>
    /// <param name="name">the hint being watched.</param>
    /// <param name="callback">an <see cref="HintCallback"/> function that will be called when the
    /// hint value changes.</param>
    /// <param name="userdata">a pointer being passed to the callback function.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddHintCallback"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveHintCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveHintCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        HintCallback callback, IntPtr userdata);
}