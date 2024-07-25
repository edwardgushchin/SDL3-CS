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

/**
 * # CategoryLog
 *
 * Simple log messages with priorities and categories. A message’s
 * SDL_LogPriority signifies how important the message is. A message's
 * SDL_LogCategory signifies from what domain it belongs to. Every category
 * has a minimum priority specified: when a message belongs to that category,
 * it will only be sent out if it has that minimum priority or higher.
 *
 * SDL's own logs are sent below the default priority threshold, so they are
 * quiet by default. If you're debugging SDL you might want:
 *
 * SDL_SetLogPriorities(SDL_LOG_PRIORITY_WARN);
 *
 * Here's where the messages go on different platforms:
 *
 * - Windows: debug output stream
 * - Android: log output
 * - Others: standard error output (stderr)
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogPriorities(LogPriority priority);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetLogPriorities(SDL_LogPriority priority);</code>
    /// <summary>
    /// Set the priority of all log categories.
    /// </summary>
    /// <param name="priority">the <see cref="LogPriority"/> to assign.</param>
    /// <since> * \since This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ResetLogPriorities"/>
    /// <seealso cref="SetLogPriority"/>
    public static void SetLogPriorities(LogPriority priority) => SDL_SetLogPriorities(priority);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogPriority(LogCategory category, LogPriority priority);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetLogPriority(int category, SDL_LogPriority priority);</code>
    /// <summary>
    /// Set the priority of a particular log category.
    /// </summary>
    /// <param name="category">the category to assign a priority to.</param>
    /// <param name="priority">the <see cref="LogPriority"/> to assign.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetLogPriority"/>
    /// <seealso cref="ResetLogPriorities"/>
    /// <seealso cref="SetLogPriorities"/>
    public static void SetLogPriority(LogCategory category, LogPriority priority) => SDL_SetLogPriority(category, priority);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial LogPriority SDL_GetLogPriority(LogCategory category);
    /// <code>extern SDL_DECLSPEC SDL_LogPriority SDLCALL SDL_GetLogPriority(int category);</code>
    /// <summary>
    /// Get the priority of a particular log category.
    /// </summary>
    /// <param name="category">the category to query.</param>
    /// <returns>the <see cref="LogPriority"/> for the requested category.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetLogPriority"/>
    public static LogPriority GetLogPriority(LogCategory category) => SDL_GetLogPriority(category);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetLogPriorities();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetLogPriorities(void);</code>
    /// <summary>
    /// Reset all priorities to default.
    /// </summary>
    /// <remarks>This is called by <see cref="Quit"/>.</remarks>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetLogPriorities"/>
    /// <seealso cref="SetLogPriority"/>
    public static void ResetLogPriorities() => SDL_ResetLogPriorities();

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Log([MarshalAs(UnmanagedType.LPUTF8Str)]string fmt);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Log(SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(1);</code>
    /// <summary>
    /// Log a message with <see cref="LogCategory.Application"/> and <see cref="LogPriority.Info"/>
    /// </summary>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void Log(string message) => SDL_Log(message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogVerbose(LogCategory category, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogVerbose(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Verbose"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogWarn"/>
    public static void LogVerbose(LogCategory category, string message) => SDL_LogVerbose(category, message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogDebug(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogDebug(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Debug"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogDebug(LogCategory category, string message) => SDL_LogDebug(category, message);  
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogInfo(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogInfo(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Info"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogInfo(LogCategory category, string message) => SDL_LogInfo(category, message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogWarn(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogWarn(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Warn"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogVerbose"/>
    public static void LogWarn(LogCategory category, string message) => SDL_LogWarn(category, message);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogError(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogError(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Error"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogError(LogCategory category, string message) => SDL_LogError(category, message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogCritical(LogCategory category, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogCritical(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Critical"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogCritical(LogCategory category, string message) => SDL_LogCritical(category, message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogMessage(LogCategory category, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogMessage(int category, SDL_LogPriority priority, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(3);</code>
    /// <summary>
    /// Log a message with the specified category and priority.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogMessage(LogCategory category, string message) => SDL_LogMessage(category, message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetLogOutputFunction(out IntPtr callback, out IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetLogOutputFunction(SDL_LogOutputFunction *callback, void **userdata);</code>
    /// <summary>
    /// Get the current log output function.
    /// </summary>
    /// <param name="callback">an <see cref="LogOutputFunction"/> filled in with the current log callback.</param>
    /// <param name="userdata">a pointer filled in with the pointer that is passed to <c>callback</c>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetLogOutputFunction"/>
    public static void GetLogOutputFunction(out LogOutputFunction? callback, out IntPtr userdata)
    {
        SDL_GetLogOutputFunction(out var cbPtr, out userdata);
        callback = Marshal.GetDelegateForFunctionPointer<LogOutputFunction>(cbPtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogOutputFunction(LogOutputFunction callback, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetLogOutputFunction(SDL_LogOutputFunction callback, void *userdata);</code>
    /// <summary>
    /// Replace the default log output function with one of your own.
    /// </summary>
    /// <param name="callback">an <see cref="LogOutputFunction"/> to call instead of the default.</param>
    /// <param name="userdata">a pointer that is passed to <see cref="callback"/>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetLogOutputFunction"/>
    public static void SetLogOutputFunction(LogOutputFunction callback, IntPtr userdata) =>
        SDL_SetLogOutputFunction(callback, userdata);
}