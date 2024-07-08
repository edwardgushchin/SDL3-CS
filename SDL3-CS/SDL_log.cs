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
    /// <summary>
    /// By default the application category is enabled at the <see cref="LogPriority.Info"/> level,
    /// the assert category is enabled at the <see cref="LogPriority.Warn"/> level,
    /// test is enabled at the <see cref="LogPriority.Verbose"/> level and all other categories are
    /// enabled at the <see cref="LogPriority.Error"/> level.
    /// </summary>
    public enum LogCategory
    {
        Application,
        Error,
        Assert,
        System,
        Audio,
        Video,
        Render,
        Input,
        Test,

        /* Reserved for future SDL library use */
        Reserved1,
        Reserved2,
        Reserved3,
        Reserved4,
        Reserved5,
        Reserved6,
        Reserved7,
        Reserved8,
        Reserved9,
        Reserved10,

        /* Beyond this point is reserved for application use, e.g. */
        Custom
    }
    
    /// <summary>
    /// The predefined log priorities
    /// </summary>
    public enum LogPriority
    {
        Verbose = 1,
        Debug,
        Info,
        Warn,
        Error,
        Critical,
        Priorities
    }
    
    /// <summary>
    /// The prototype for the log output callback function.
    /// </summary>
    /// <param name="userdata">what was passed as userdata to <see cref="SetLogOutputFunction"/></param>
    /// <param name="category">the category of the message.</param>
    /// <param name="priority">the priority of the message.</param>
    /// <param name="message">the message being output.</param>
    /// <remarks>This function is called by SDL when there is new text to be logged.</remarks>
    public delegate void LogOutputFunction(IntPtr userdata, LogCategory category, LogPriority priority, string message);


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetLogOutputFunction(LogOutputFunction callback);
    
    /// <summary>
    /// Get the current log output function.
    /// </summary>
    /// <param name="callback">an <see cref="LogOutputFunction"/> filled in with the current log callback.</param>
    /// <seealso cref="SetLogOutputFunction"/>
    public static void GetLogOutputFunction(LogOutputFunction callback) => SDL_GetLogOutputFunction(callback);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial LogPriority SDL_GetLogPriority(LogCategory category);
    
    /// <summary>
    /// Get the priority of a particular log category.
    /// </summary>
    /// <param name="category">the category to query.</param>
    /// <returns>Returns the <see cref="LogPriority"/> for the requested category.</returns>
    /// <seealso cref="SetLogPriority"/>
    public static LogPriority GetLogPriority(LogCategory category) => SDL_GetLogPriority(category);

    
    [LibraryImport(SDLLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Log(string message);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public static void Log(string message) => SDL_Log(message);


}