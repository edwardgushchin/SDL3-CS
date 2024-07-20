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
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetHintWithPriority([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value, HintPriority priority);
    public static bool SetHintWithPriority(string name, string value, HintPriority priority) =>
        SDL_SetHintWithPriority(name, value, priority);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value);
    public static bool SetHint(string name, string value) => SDL_SetHint(name, value);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ResetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    public static bool ResetHint(string name) => SDL_ResetHint(name);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ResetHints();
    public static bool ResetHints() => SDL_ResetHints();
    
    
    [LibraryImport(SDLLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHint(string name);
    public static string? GetHint(string name) => Marshal.PtrToStringUTF8(SDL_GetHint(name));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetHintBoolean([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.I1)]bool defaultValue);
    public static bool GetHintBoolean(string name, bool defaultValue) => SDL_GetHintBoolean(name, defaultValue);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddHintCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        HintCallback callback, IntPtr userdata);
    public static int AddHintCallback(string name, HintCallback callback, IntPtr userdata) =>
        SDL_AddHintCallback(name, callback, userdata);
        
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DelHintCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        HintCallback callback, IntPtr userdata);
    public static void DelHintCallback(string name, HintCallback callback, IntPtr userdata) =>
        SDL_DelHintCallback(name, callback, userdata);
}