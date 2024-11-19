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
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateRenderer(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name);

    public static Render? CreateRenderer(Window window, string? name)
    {
        var renderHandle = SDL_CreateRenderer(window.Handle, name);
        return renderHandle != IntPtr.Zero ? new Render(renderHandle) : null;
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetRenderVSync(IntPtr renderer, int vsync);
    public static int SetRenderVSync(Render renderer, int vsync) => SDL_SetRenderVSync(renderer.Handle, vsync);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetRenderVSync(IntPtr renderer, out int vsync);
    public static int GetRenderVSync(Render renderer, out int vsync) =>
        SDL_GetRenderVSync(renderer.Handle, out vsync);


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);
    public static void SetRenderDrawColor(Render renderer, byte r, byte g, byte b, byte a) =>
        SDL_SetRenderDrawColor(renderer.Handle, r, g, b, a);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenderClear(IntPtr renderer);
    public static int RenderClear(Render renderer) => SDL_RenderClear(renderer.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenderPresent(IntPtr renderer);
    public static int RenderPresent(Render renderer) => SDL_RenderPresent(renderer.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(IntPtr texture);
    public static void DestroyTexture(Texture texture) => SDL_DestroyTexture(texture.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(IntPtr renderer);
    public static void DestroyRenderer(Render renderer) => SDL_DestroyRenderer(renderer.Handle);


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetRenderDriver(int index);
    public static string? GetRenderDriver(int index) => Marshal.PtrToStringUTF8(SDL_GetRenderDriver(index));
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumRenderDrivers();
    public static int GetNumRenderDrivers() => SDL_GetNumRenderDrivers();

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateTexture(IntPtr renderer, PixelFormat format, TextureAccess access, int w, int h);

    public static Texture? CreateTexture(Render renderer, PixelFormat format, TextureAccess access, int w, int h)
    {
        var ptr = SDL_CreateTexture(renderer.Handle, format, access, w, h);
        return ptr == IntPtr.Zero ? null : new Texture(ptr);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);
    /*public static int UpdateTexture(Texture texture, Rect? rect, byte[] pixels, int pitch)
    {
        var rectPtr = IntPtr.Zero;
        var pixelsPtr = IntPtr.Zero;

        try
        {
            if (rect.HasValue)
            {
                rectPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Rect>());
                Marshal.StructureToPtr(rect.Value, rectPtr, false);
            }

            pixelsPtr = Marshal.AllocHGlobal(pixels.Length);
            Marshal.Copy(pixels, 0, pixelsPtr, pixels.Length);

            return SDL_UpdateTexture(texture.Handle, rectPtr, pixelsPtr, pitch);
        }
        finally
        {
            if (rectPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(rectPtr);

            if (pixelsPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(pixelsPtr);
        }
    }*/
    
    public static int UpdateTexture(Texture texture, Rect? rect, IntPtr pixels, int pitch)
    {
        var rectPtr = IntPtr.Zero;

        try
        {
            if (rect.HasValue)
            {
                rectPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Rect>());
                Marshal.StructureToPtr(rect.Value, rectPtr, false);
            }

            return SDL_UpdateTexture(texture.Handle, rectPtr, pixels, pitch);
        }
        finally
        {
            if (rectPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(rectPtr);
            }
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_RenderTexture(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);
    public static int RenderTexture(Render renderer, Texture texture, FRect? srcrect, FRect? dstrect)
    {
        IntPtr srcPtr = IntPtr.Zero;
        IntPtr dstPtr = IntPtr.Zero;

        try
        {
            if (srcrect.HasValue)
            {
                srcPtr = Marshal.AllocHGlobal(Marshal.SizeOf<FRect>());
                Marshal.StructureToPtr(srcrect.Value, srcPtr, false);
            }

            if (dstrect.HasValue)
            {
                dstPtr = Marshal.AllocHGlobal(Marshal.SizeOf<FRect>());
                Marshal.StructureToPtr(dstrect.Value, dstPtr, false);
            }

            return SDL_RenderTexture(renderer.Handle, texture.Handle, srcPtr, dstPtr);
        }
        finally
        {
            if (srcPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(srcPtr);

            if (dstPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(dstPtr);
        }
    }
}