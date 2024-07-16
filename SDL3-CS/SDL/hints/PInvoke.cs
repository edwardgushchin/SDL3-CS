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
    private static unsafe partial int SDL_AddHintCallback(byte* name, HintCallback callback, IntPtr userdata);
    public static unsafe int AddHintCallback(string name, HintCallback callback, IntPtr userdata)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_AddHintCallback(UTF8Encode(name, utf8Name, utf8NameBufSize), callback, userdata);
    }
        
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_DelHintCallback(byte* name, HintCallback callback, IntPtr userdata);
    public static unsafe void DelHintCallback(string name, HintCallback callback, IntPtr userdata)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        SDL_DelHintCallback(UTF8Encode(name, utf8Name, utf8NameBufSize),  callback, userdata);
    }
    

    [LibraryImport(SDLLibrary, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHint(string name);
    public static string? GetHint(string name) => Marshal.PtrToStringUTF8(SDL_GetHint(name));


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool SDL_GetHintBoolean(byte* name, [MarshalAs(UnmanagedType.I1)]bool defaultValue);
    public static unsafe bool GetHintBoolean(string name, bool defaultValue)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_GetHintBoolean(UTF8Encode(name, utf8Name, utf8NameBufSize), defaultValue);
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool SDL_ResetHint(byte* name);
    public static unsafe bool ResetHint(string name)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_ResetHint(UTF8Encode(name, utf8Name, utf8NameBufSize));
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ResetHints();
    public static bool ResetHints() => SDL_ResetHints();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool SDL_SetHint(byte* name, byte* value);
    public static unsafe bool SetHint(string name, string value)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        
        var utf8ValueBufSize = UTF8Size(value);
        var utf8Value = stackalloc byte[utf8ValueBufSize];
        
        return SDL_SetHint(
            UTF8Encode(name, utf8Name, utf8NameBufSize),
            UTF8Encode(value, utf8Value, utf8ValueBufSize));
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool SDL_SetHintWithPriority(byte* name, byte* value, HintPriority priority);
    public static unsafe bool SetHintWithPriority(string name, string value, HintPriority priority)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        
        var utf8ValueBufSize = UTF8Size(value);
        var utf8Value = stackalloc byte[utf8ValueBufSize];
        
        return SDL_SetHintWithPriority(
            UTF8Encode(name, utf8Name, utf8NameBufSize),
            UTF8Encode(value, utf8Value, utf8ValueBufSize), 
            priority);
    }
}