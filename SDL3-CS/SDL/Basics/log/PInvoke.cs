#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetLogPriorities"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogPriorities(LogPriority priority);
    private delegate void SetLogPrioritiesNative(LogPriority priority);
    private static SetLogPrioritiesNative SetLogPrioritiesNativeFunction = SDL_SetLogPriorities;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetLogPriorities(SDL_LogPriority priority);</code>
    /// <summary>
    /// Set the priority of all log categories.
    /// </summary>
    /// <param name="priority">the <see cref="LogPriority"/> to assign.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ResetLogPriorities"/>
    /// <seealso cref="SetLogPriority"/>
    public static void SetLogPriorities(LogPriority priority)
    {
        SetLogPrioritiesNativeFunction(priority);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetLogPriority"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogPriority(LogCategory category, LogPriority priority);
    private delegate void SetLogPriorityNative(LogCategory category, LogPriority priority);
    private static SetLogPriorityNative SetLogPriorityNativeFunction = SDL_SetLogPriority;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetLogPriority(int category, SDL_LogPriority priority);</code>
    /// <summary>
    /// Set the priority of a particular log category.
    /// </summary>
    /// <param name="category">the category to assign a priority to.</param>
    /// <param name="priority">the <see cref="LogPriority"/> to assign.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetLogPriority"/>
    /// <seealso cref="ResetLogPriorities"/>
    /// <seealso cref="SetLogPriorities"/>
    public static void SetLogPriority(LogCategory category, LogPriority priority)
    {
        SetLogPriorityNativeFunction(category, priority);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetLogPriority"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial LogPriority SDL_GetLogPriority(LogCategory category);
    private delegate LogPriority GetLogPriorityNative(LogCategory category);
    private static GetLogPriorityNative GetLogPriorityNativeFunction = SDL_GetLogPriority;

    /// <code>extern SDL_DECLSPEC SDL_LogPriority SDLCALL SDL_GetLogPriority(int category);</code>
    /// <summary>
    /// Get the priority of a particular log category.
    /// </summary>
    /// <param name="category">the category to query.</param>
    /// <returns>the <see cref="LogPriority"/> for the requested category.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetLogPriority"/>
    public static LogPriority GetLogPriority(LogCategory category)
    {
        return GetLogPriorityNativeFunction(category);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResetLogPriorities"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetLogPriorities();
    private delegate void ResetLogPrioritiesNative();
    private static ResetLogPrioritiesNative ResetLogPrioritiesNativeFunction = SDL_ResetLogPriorities;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ResetLogPriorities(void);</code>
    /// <summary>
    /// Reset all priorities to default.
    /// </summary>
    /// <remarks>This is called by <see cref="Quit"/>.</remarks>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetLogPriorities"/>
    /// <seealso cref="SetLogPriority"/>
    public static void ResetLogPriorities()
    {
        ResetLogPrioritiesNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetLogPriorityPrefix"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetLogPriorityPrefix(LogPriority priority, [MarshalAs(UnmanagedType.LPUTF8Str)] string? prefix);
    private delegate bool SetLogPriorityPrefixNative(LogPriority priority, string? prefix);
    private static SetLogPriorityPrefixNative SetLogPriorityPrefixNativeFunction = SDL_SetLogPriorityPrefix;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetLogPriorityPrefix(SDL_LogPriority priority, const char *prefix);</code>
    /// <summary>
    /// <para>Set the text prepended to log messages of a given priority.</para>
    /// <para>By default <see cref="LogPriority.Info"/> and below have no prefix, and
    /// <see cref="LogPriority.Warn"/> and higher have a prefix showing their priority, e.g.
    /// <c>"WARNING: "</c>.</para>
    /// <para>This function makes a copy of its string argument, <b>prefix</b>, so it is not
    /// necessary to keep the value of **prefix** alive after the call returns.</para>
    /// </summary>
    /// <param name="priority">the <see cref="LogPriority"/> to modify.</param>
    /// <param name="prefix">the prefix to use for that log priority, or <c>null</c> to use no
    /// prefix.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetLogPriorities"/>
    /// <seealso cref="SetLogPriority"/>
    public static bool SetLogPriorityPrefix(LogPriority priority, string? prefix)
    {
        return SetLogPriorityPrefixNativeFunction(priority, prefix);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Log"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Log([MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogNative(string fmt);
    private static LogNative LogNativeFunction = SDL_Log;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Log(SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(1);</code>
    /// <summary>
    /// Log a message with <see cref="LogCategory.Application"/> and <see cref="LogPriority.Info"/>.
    /// </summary>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void Log(string fmt)
    {
        LogNativeFunction(fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogTrace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogTrace(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogTraceNative(LogCategory category, string fmt);
    private static LogTraceNative LogTraceNativeFunction = SDL_LogTrace;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogTrace(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Trace"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogTrace(LogCategory category, string fmt)
    {
        LogTraceNativeFunction(category, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogVerbose"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogVerbose(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogVerboseNative(LogCategory category, string fmt);
    private static LogVerboseNative LogVerboseNativeFunction = SDL_LogVerbose;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogVerbose(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Verbose"/>,
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogWarn"/>
    public static void LogVerbose(LogCategory category, string fmt)
    {
        LogVerboseNativeFunction(category, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogDebug"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogDebug(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    private delegate void LogDebugNative(LogCategory category, string message);
    private static LogDebugNative LogDebugNativeFunction = SDL_LogDebug;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogDebug(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Debug"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="message">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogDebug(LogCategory category, string message)
    {
        LogDebugNativeFunction(category, message);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogInfo(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogInfoNative(LogCategory category, string fmt);
    private static LogInfoNative LogInfoNativeFunction = SDL_LogInfo;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogInfo(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Info"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogInfo(LogCategory category, string fmt)
    {
        LogInfoNativeFunction(category, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogWarn"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogWarn(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogWarnNative(LogCategory category, string fmt);
    private static LogWarnNative LogWarnNativeFunction = SDL_LogWarn;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogWarn(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Warn"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    public static void LogWarn(LogCategory category, string fmt)
    {
        LogWarnNativeFunction(category, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogError"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogError(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogErrorNative(LogCategory category, string fmt);
    private static LogErrorNative LogErrorNativeFunction = SDL_LogError;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogError(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Error"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogError(LogCategory category, string fmt)
    {
        LogErrorNativeFunction(category, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogCritical"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogCritical(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogCriticalNative(LogCategory category, string fmt);
    private static LogCriticalNative LogCriticalNativeFunction = SDL_LogCritical;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogCritical(int category, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// Log a message with <see cref="LogPriority.Critical"/>.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogCritical(LogCategory category, string fmt)
    {
        LogCriticalNativeFunction(category, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogMessage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogMessage(LogCategory category, LogPriority priority, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate void LogMessageNative(LogCategory category, LogPriority priority, string fmt);
    private static LogMessageNative LogMessageNativeFunction = SDL_LogMessage;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogMessage(int category, SDL_LogPriority priority, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(3);</code>
    /// <summary>
    /// Log a message with the specified category and priority.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="priority">the priority of the message.</param>
    /// <param name="fmt">the priority of the message.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessageV"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogMessage(LogCategory category, LogPriority priority, string fmt)
    {
        LogMessageNativeFunction(category, priority, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LogMessageV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogMessageV(LogCategory category, LogPriority priority, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] ap);
    private delegate void LogMessageVNative(LogCategory category, LogPriority priority, string fmt, string[] ap);
    private static LogMessageVNative LogMessageVNativeFunction = SDL_LogMessageV;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LogMessageV(int category, SDL_LogPriority priority, SDL_PRINTF_FORMAT_STRING const char *fmt, va_list ap) SDL_PRINTF_VARARG_FUNCV(3);</code>
    /// <summary>
    /// Log a message with the specified category and priority.
    /// </summary>
    /// <param name="category">the category of the message.</param>
    /// <param name="priority">the priority of the message.</param>
    /// <param name="fmt">a printf() style message format string.</param>
    /// <param name="ap">a variable argument list.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Log"/>
    /// <seealso cref="LogCritical"/>
    /// <seealso cref="LogDebug"/>
    /// <seealso cref="LogError"/>
    /// <seealso cref="LogInfo"/>
    /// <seealso cref="LogMessage"/>
    /// <seealso cref="LogTrace"/>
    /// <seealso cref="LogVerbose"/>
    /// <seealso cref="LogWarn"/>
    public static void LogMessageV(LogCategory category, LogPriority priority, string fmt, string[] ap)
    {
        LogMessageVNativeFunction(category, priority, fmt, ap);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDefaultLogOutputFunction"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial LogOutputFunction SDL_GetDefaultLogOutputFunction();
    private delegate LogOutputFunction GetDefaultLogOutputFunctionNative();
    private static GetDefaultLogOutputFunctionNative GetDefaultLogOutputFunctionNativeFunction = SDL_GetDefaultLogOutputFunction;

    /// <code>extern SDL_DECLSPEC SDL_LogOutputFunction SDLCALL SDL_GetDefaultLogOutputFunction(void);</code>
    /// <summary>
    /// Get the default log output function.
    /// </summary>
    /// <returns>the default log output callback. It should be called with <c>null</c> for the userdata argument.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.6.</since>
    /// <seealso cref="SetLogOutputFunction"/>
    /// <seealso cref="GetLogOutputFunction"/>
    public static LogOutputFunction GetDefaultLogOutputFunction()
    {
        return GetDefaultLogOutputFunctionNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetLogOutputFunction"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetLogOutputFunction(out LogOutputFunction callback, out IntPtr userdata);
    private delegate void GetLogOutputFunctionNative(out LogOutputFunction callback, out IntPtr userdata);
    private static GetLogOutputFunctionNative GetLogOutputFunctionNativeFunction = SDL_GetLogOutputFunction;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetLogOutputFunction(SDL_LogOutputFunction *callback, void **userdata);</code>
    /// <summary>
    /// <para>Get the current log output function.</para>
    /// </summary>
    /// <param name="callback">an <see cref="LogOutputFunction"/> filled in with the current log
    /// callback.</param>
    /// <param name="userdata">a pointer filled in with the pointer that is passed to
    /// <c>callback</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDefaultLogOutputFunction"/>
    /// <seealso cref="SetLogOutputFunction"/>
    public static void GetLogOutputFunction(out LogOutputFunction callback, out IntPtr userdata)
    {
        GetLogOutputFunctionNativeFunction(out callback, out userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetLogOutputFunction"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetLogOutputFunction(LogOutputFunction callback, IntPtr userdata);
    private delegate void SetLogOutputFunctionNative(LogOutputFunction callback, IntPtr userdata);
    private static SetLogOutputFunctionNative SetLogOutputFunctionNativeFunction = SDL_SetLogOutputFunction;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetLogOutputFunction(SDL_LogOutputFunction callback, void *userdata);</code>
    /// <summary>
    /// Replace the default log output function with one of your own.
    /// </summary>
    /// <param name="callback">an <see cref="LogOutputFunction"/> to call instead of the default.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetDefaultLogOutputFunction"/>
    /// <seealso cref="GetLogOutputFunction"/>
    public static void SetLogOutputFunction(LogOutputFunction callback, IntPtr userdata)
    {
        SetLogOutputFunctionNativeFunction(callback, userdata);
    }
}
