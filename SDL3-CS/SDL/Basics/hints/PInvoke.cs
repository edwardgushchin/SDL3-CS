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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_SetHintWithPriority([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value, HintPriority priority);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_SetHintWithPriority(const char *name, const char *value, SDL_HintPriority priority);</code>
    /// <summary>
    /// <para>Set a hint with a specific priority.</para>
    /// <para>The priority controls the behavior when setting a hint that already has a
    /// value. Hints will replace existing hints of their priority and lower.
    /// Environment variables are considered to have override priority.</para>
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">the value of the hint variable.</param>
    /// <param name="priority">the <see cref="HintPriority"/> level for the hint.</param>
    /// <returns>True if the hint was set, False otherwise.</returns>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="ResetHint"/>
    /// <seealso cref="SetHint"/>
    public static bool SetHintWithPriority(string name, string value, HintPriority priority) =>
        SDL_SetHintWithPriority(name, value, priority);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_SetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_SetHint(const char *name, const char *value);</code>
    /// <summary>
    /// <para>Set a hint with normal priority.</para>
    /// <para>This will reset a hint to the value of the environment variable, or NULL if
    /// the environment isn't set. Callbacks will be called normally with this
    /// change.</para>
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">True if the hint was set, False otherwise.</param>
    /// <returns>True if the hint was set, False otherwise.</returns>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="ResetHint"/>
    /// <seealso cref="SetHintWithPriority"/>
    public static bool SetHint(string name, string value) => SDL_SetHint(name, value);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_ResetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ResetHint(const char *name);</code>
    /// <summary>
    /// <para>Reset a hint to the default value.</para>
    /// <para>This will reset a hint to the value of the environment variable, or NULL if
    /// the environment isn't set. Callbacks will be called normally with this
    /// change.</para>
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <returns>True if the hint was set, False otherwise.</returns>
    /// <seealso cref="SetHint"/>
    /// <seealso cref="ResetHints"/>
    public static bool ResetHint(string name) => SDL_ResetHint(name);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial void SDL_ResetHints();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetHints(void);</code>
    /// <summary>
    /// <para>Reset all hints to the default values.</para>
    /// <para>This will reset all hints to the value of the associated environment
    /// variable, or NULL if the environment isn't set. Callbacks will be called
    /// normally with this change.</para>
    /// </summary>
    /// <seealso cref="ResetHint"/>
    public static void ResetHints() => SDL_ResetHints();
    
    
    [LibraryImport(SDLLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHint(string name);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetHint(const char *name);</code>
    /// <summary>
    /// <para>Get the value of a hint.</para>
    /// <para>The returned string follows the <see cref="GetStringRule"/>.</para>
    /// </summary>
    /// <param name="name">the hint to query.</param>
    /// <returns>the string value of a hint or NULL if the hint isn't set.</returns>
    /// <seealso cref="SetHint"/>
    /// <seealso cref="SetHintWithPriority"/>
    public static string? GetHint(string name) => Marshal.PtrToStringUTF8(SDL_GetHint(name));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GetHintBoolean([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(SDLBool)]bool defaultValue);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetHintBoolean(const char *name, SDL_bool default_value);</code>
    /// <summary>
    /// Get the boolean value of a hint variable.
    /// </summary>
    /// <param name="name">the name of the hint to get the boolean value from.</param>
    /// <param name="defaultValue">the value to return if the hint does not exist.</param>
    /// <returns>the boolean value of a hint or the provided default value if the
    /// hint does not exist.</returns>
    /// <seealso cref="GetHint"/>
    /// <seealso cref="SetHint"/>
    public static bool GetHintBoolean(string name, bool defaultValue) => SDL_GetHintBoolean(name, defaultValue);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddHintCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        HintCallback callback, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddHintCallback(const char *name, SDL_HintCallback callback, void *userdata);</code>
    /// <summary>
    /// Add a function to watch a particular hint.
    /// </summary>
    /// <param name="name">the hint to watch.</param>
    /// <param name="callback">an <see cref="HintCallback"/> function that will be called when the
    /// hint value changes.</param>
    /// <param name="userdata">a pointer to pass to the callback function.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is **NOT** safe to call this function from two threads at
    /// once.</remarks>
    /// <seealso cref="DelHintCallback"/>
    public static int AddHintCallback(string name, HintCallback callback, IntPtr userdata) =>
        SDL_AddHintCallback(name, callback, userdata);
        
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DelHintCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        HintCallback callback, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DelHintCallback(const char *name, SDL_HintCallback callback, void *userdata);</code>
    /// <summary>
    /// Remove a function watching a particular hint.
    /// </summary>
    /// <param name="name">the hint being watched.</param>
    /// <param name="callback">an <see cref="HintCallback"/> function that will be called when the
    /// hint value changes.</param>
    /// <param name="userdata">a pointer being passed to the callback function.</param>
    /// <seealso cref="AddHintCallback"/>
    public static void DelHintCallback(string name, HintCallback callback, IntPtr userdata) =>
        SDL_DelHintCallback(name, callback, userdata);
}