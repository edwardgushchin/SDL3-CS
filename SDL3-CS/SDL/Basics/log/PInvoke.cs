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
    private static partial void SDL_GetLogOutputFunction(LogOutputFunction callback, IntPtr userdata);
    public static void GetLogOutputFunction(LogOutputFunction callback, IntPtr userdata) 
        => SDL_GetLogOutputFunction(callback, userdata);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial LogPriority SDL_GetLogPriority(LogCategory category);
    public static LogPriority GetLogPriority(LogCategory category) => SDL_GetLogPriority(category);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Log([MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    public static void Log(string message) => SDL_Log(message);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogCritical(LogCategory category, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    public static void LogCritical(LogCategory category, string message) => SDL_LogCritical(category, message);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogDebug(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    public static void LogDebug(LogCategory category, string message) => SDL_LogDebug(category, message);    
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogError(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);
    public static void LogError(LogCategory category, string message) => SDL_LogError(category, message);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_LogInfo(LogCategory category, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);

    public static void LogInfo(LogCategory category, string message) => SDL_LogInfo(category, message);
}