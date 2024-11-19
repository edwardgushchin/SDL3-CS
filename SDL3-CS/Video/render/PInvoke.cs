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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateRenderer(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderVSync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetRenderVSync(IntPtr renderer, int vsync);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderVSync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRenderVSync(IntPtr renderer, out int vsync);


    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderDrawColor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderClear"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RenderClear(IntPtr renderer);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderPresent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RenderPresent(IntPtr renderer);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyTexture(IntPtr texture);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyRenderer(IntPtr renderer);


    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRenderDriver(int index);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumRenderDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumRenderDrivers();

    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTexture(IntPtr renderer, PixelFormat format, TextureAccess access, int w, int h);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RenderTexture(IntPtr renderer, IntPtr texture, FRect srcrect, FRect dstrect);
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RenderTexture(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTextureFromSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTextureFromSurface(IntPtr renderer, IntPtr surface);
}