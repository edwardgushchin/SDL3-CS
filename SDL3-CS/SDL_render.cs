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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Render SDL_CreateRenderer(Window window,  byte* name);
    public static unsafe Render CreateRenderer(Window window, string? name)
    {
        var utf8TitleBufSize = UTF8Size(name);
        var utf8Title = stackalloc byte[utf8TitleBufSize];
        return SDL_CreateRenderer(window, UTF8Encode(name, utf8Title, utf8TitleBufSize));
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetRenderDrawColor(Render renderer, byte r, byte g, byte b, byte a);
    public static void SetRenderDrawColor(Render renderer, byte r, byte g, byte b, byte a) =>
        SDL_SetRenderDrawColor(renderer, r, g, b, a);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenderClear(Render renderer);
    public static int RenderClear(Render renderer) => SDL_RenderClear(renderer);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenderPresent(Render renderer);
    public static int RenderPresent(Render renderer) => SDL_RenderPresent(renderer);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(Render renderer);
    public static void DestroyRenderer(Render renderer) => SDL_DestroyRenderer(renderer);


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetRenderDriver(int index);
    public static string? GetRenderDriver(int index) => UTF8ToManaged(SDL_GetRenderDriver(index));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumRenderDrivers();
    public static int GetNumRenderDrivers() => SDL_GetNumRenderDrivers();
}