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

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    private static partial IntPtr SDL_GetPixelFormatName(int format); 
    /// <code>extern SDL_DECLSPEC const char* SDLCALL SDL_GetPixelFormatName(SDL_PixelFormat format);</code>
    /// <summary>
    /// <para>Get the human readable name of a pixel format.</para>
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="format">the pixel format to query.</param>
    /// <returns>the human readable name of the specified pixel format or
    /// <c>SDL_PIXELFORMAT_UNKNOWN</c> if the format isn't recognized.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string GetPixelFormatName(int format) => Marshal.PtrToStringUTF8(SDL_GetPixelFormatName(format)) ?? PixelFormat.Unknown.ToString();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetMasksForPixelFormat(int format, ref int bpp, ref uint rmask, ref uint gmask, ref uint bmask, ref uint amask);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetMasksForPixelFormat(SDL_PixelFormat format, int *bpp, Uint32 *Rmask, Uint32 *Gmask, Uint32 *Bmask, Uint32 *Amask);</code>
    /// <summary>
    /// Convert one of the enumerated pixel formats to a bpp value and RGBA masks.
    /// </summary>
    /// <param name="format">one of the <see cref="PixelFormat"/> values.</param>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="rmask">a pointer filled in with the red mask for the format.</param>
    /// <param name="gmask">a pointer filled in with the green mask for the format.</param>
    /// <param name="bmask">a pointer filled in with the blue mask for the format.</param>
    /// <param name="amask">a pointer filled in with the alpha mask for the format.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetPixelFormatForMasks"/>
    public static int GetMasksForPixelFormat(int format, ref int bpp, ref uint rmask, ref uint gmask, ref uint bmask, ref uint amask) =>
        SDL_GetMasksForPixelFormat(format, ref bpp, ref rmask, ref gmask, ref bmask, ref amask);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask);
    /// <code>extern SDL_DECLSPEC SDL_PixelFormat SDLCALL SDL_GetPixelFormatForMasks(int bpp, Uint32 Rmask, Uint32 Gmask, Uint32 Bmask, Uint32 Amask);</code>
    /// <summary>
    /// Convert a bpp value and RGBA masks to an enumerated pixel format.
    /// This will return <see cref="PixelFormat.Unknown"/> if the conversion wasn't
    /// possible.
    /// </summary>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="rmask">the red mask for the format.</param>
    /// <param name="gmask">the green mask for the format.</param>
    /// <param name="bmask">the blue mask for the format.</param>
    /// <param name="amask">the alpha mask for the format.</param>
    /// <returns>he <see cref="PixelFormat"/> value corresponding to the format masks, or
    /// <see cref="PixelFormat.Unknown"/> if there isn't a match.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetMasksForPixelFormat"/>
    public static int GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask) =>
        SDL_GetPixelFormatForMasks(bpp, rmask, gmask, bmask, amask);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPixelFormatDetails(int format);
    /// <code>extern SDL_DECLSPEC const SDL_PixelFormatDetails * SDLCALL SDL_GetPixelFormatDetails(SDL_PixelFormat format);</code>
    /// <summary>
    /// <para>Create an <see cref="PixelFormatDetails"/> structure corresponding to a pixel format.</para>
    /// <para>Returned structure may come from a shared global cache (i.e. not newly
    /// allocated), and hence should not be modified, especially the palette. Weird
    /// errors such as `Blit combination not supported` may occur.</para>
    /// </summary>
    /// <param name="format">one of the <see cref="PixelFormat"/> values.</param>
    /// <returns>a pointer to a <see cref="PixelFormatDetails"/> structure or NULL on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static PixelFormatDetails? GetPixelFormatDetails(int format) =>
        Marshal.PtrToStructure<PixelFormatDetails>(SDL_GetPixelFormatDetails(format));

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreatePalette(int ncolors);
    /// <code>extern SDL_DECLSPEC SDL_Palette *SDLCALL SDL_CreatePalette(int ncolors);</code>
    /// <summary>
    /// <para>Create a palette structure with the specified number of color entries.</para>
    /// <para>The palette entries are initialized to white.</para>
    /// </summary>
    /// <param name="ncolors">represents the number of color entries in the color palette.</param>
    /// <returns>a new <see cref="Palette"/> structure on success or <c>NULL</c> on failure (e.g. if
    /// there wasn't enough memory); call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DestroyPalette"/>
    /// <seealso cref="SetPaletteColors"/>
    /// <seealso cref="SetSurfacePalette"/>
    public static Palette? CreatePalette(int ncolors) =>
        Marshal.PtrToStructure<Palette>(SDL_CreatePalette(ncolors));
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetPaletteColors(IntPtr palette, [MarshalAs(UnmanagedType.LPArray)] Color[] colors, int firstcolor, int ncolors);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetPaletteColors(SDL_Palette *palette, const SDL_Color *colors, int firstcolor, int ncolors);</code>
    /// <summary>
    /// Set a range of colors in a palette.
    /// </summary>
    /// <param name="palette">the <see cref="Palette"/> structure to modify.</param>
    /// <param name="colors">an array of <see cref="Color"/> structures to copy into the palette.</param>
    /// <param name="firstcolor">the index of the first palette entry to modify.</param>
    /// <param name="ncolors">the number of entries to modify.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified or destroyed in another thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int SetPaletteColors(Palette palette, Color[] colors, int firstcolor, int ncolors)
    {
        var palettePtr = IntPtr.Zero;

        try
        {
            palettePtr = Marshal.AllocHGlobal(Marshal.SizeOf<DisplayMode>());
            Marshal.StructureToPtr(palette, palettePtr, false);
            
            return SDL_SetPaletteColors(palettePtr, colors, firstcolor, ncolors);
        }
        finally
        {
            if (palettePtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(palettePtr);
            }
        }
    }
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyPalette(IntPtr palette);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyPalette(SDL_Palette *palette);</code>
    /// <summary>
    /// Free a palette created with <see cref="CreatePalette"/>.
    /// </summary>
    /// <param name="palette">the <see cref="Palette"/> structure to be freed.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified or destroyed in another thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreatePalette"/>
    public static void DestroyPalette(Palette? palette)
    {
        if (palette == null) return;
        var palettePtr = IntPtr.Zero;
        try
        {
            palettePtr = Marshal.AllocHGlobal(Marshal.SizeOf<Palette>());
            Marshal.StructureToPtr(palette, palettePtr, false);
        }
        finally
        {
            SDL_DestroyPalette(palettePtr);
            Marshal.FreeHGlobal(palettePtr);
        }
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapRGB(IntPtr format, IntPtr palette, byte r, byte g, byte b);
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_MapRGB(const SDL_PixelFormatDetails *format, const SDL_Palette *palette, Uint8 r, Uint8 g, Uint8 b);</code>
    /// <summary>
    /// <para>Map an RGB triple to an opaque pixel value for a given pixel format.</para>
    /// <para>This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.</para>
    /// <para>If the format has a palette (8-bit) the index of the closest matching color
    /// in the palette will be returned.</para>
    /// <para>If the specified pixel format has an alpha component it will be returned as
    /// all 1 bits (fully opaque).</para>
    /// <para>If the pixel format bpp (color depth) is less than 32-bpp then the unused
    /// upper bits of the return value can safely be ignored (e.g., with a 16-bpp
    /// format the return value can be assigned to a Uint16, and similarly a Uint8
    /// for an 8-bpp format).</para>
    /// </summary>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>NULL</c>.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static uint MapRGB(PixelFormatDetails format, Palette? palette, byte r, byte g, byte b)
    {
        var formatPtr = Marshal.AllocHGlobal(Marshal.SizeOf<PixelFormatDetails>());
        Marshal.StructureToPtr(format, formatPtr, false);

        var palettePtr = palette.HasValue 
            ? Marshal.AllocHGlobal(Marshal.SizeOf<Palette>()) 
            : IntPtr.Zero;

        if (palette.HasValue)
        {
            Marshal.StructureToPtr(palette.Value, palettePtr, false);
        }

        try
        {
            return SDL_MapRGB(formatPtr, palettePtr, r, g, b);
        }
        finally
        {
            if (palette.HasValue) Marshal.FreeHGlobal(palettePtr);
            Marshal.FreeHGlobal(formatPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_MapRGBA(IntPtr format, IntPtr palette, byte r, byte g, byte b, byte a);
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_MapRGBA(const SDL_PixelFormatDetails *format, const SDL_Palette *palette, Uint8 r, Uint8 g, Uint8 b, Uint8 a);</code>
    /// <summary>
    /// <para>Map an RGBA quadruple to a pixel value for a given pixel format.</para>
    /// <para>This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.</para>
    /// <para>If the specified pixel format has no alpha component the alpha value will
    /// be ignored (as it will be in formats with a palette).</para>
    /// <para>If the format has a palette (8-bit) the index of the closest matching color
    /// in the palette will be returned.</para>
    /// <para>If the pixel format bpp (color depth) is less than 32-bpp then the unused
    /// upper bits of the return value can safely be ignored (e.g., with a 16-bpp
    /// format the return value can be assigned to a Uint16, and similarly a Uint8
    /// for an 8-bpp format).</para>
    /// </summary>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>NULL</c>.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRGBA"/>
    /// <seealso cref="MapRGB"/>
    public static uint MapRGBA(PixelFormatDetails format, Palette? palette, byte r, byte g, byte b, byte a)
    {
        var formatPtr = Marshal.AllocHGlobal(Marshal.SizeOf<PixelFormatDetails>());
        Marshal.StructureToPtr(format, formatPtr, false);

        var palettePtr = palette.HasValue 
            ? Marshal.AllocHGlobal(Marshal.SizeOf<Palette>()) 
            : IntPtr.Zero;

        if (palette.HasValue)
        {
            Marshal.StructureToPtr(palette.Value, palettePtr, false);
        }

        try
        {
            return SDL_MapRGBA(formatPtr, palettePtr, r, g, b, a);
        }
        finally
        {
            if (palette.HasValue) Marshal.FreeHGlobal(palettePtr);
            Marshal.FreeHGlobal(formatPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetRGB(uint pixel, IntPtr format, IntPtr palette, ref byte r, ref byte g, ref byte b);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetRGB(Uint32 pixel, const SDL_PixelFormatDetails *format, const SDL_Palette *palette, Uint8 *r, Uint8 *g, Uint8 *b);</code>
    /// <summary>
    /// <para>Get RGB values from a pixel in the specified format.</para>
    /// <para>This function uses the entire 8-bit [0..255] range when converting color
    /// components from pixel formats with less than 8-bits per RGB component
    /// (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff,
    /// 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).</para>
    /// </summary>
    /// <param name="pixel">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>NULL</c>.</param>
    /// <param name="r">a pointer filled in with the red component, may be <c>NULL</c>.</param>
    /// <param name="g">a pointer filled in with the green component, may be <c>NULL</c>.</param>
    /// <param name="b">a pointer filled in with the blue component, may be <c>NULL</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRGBA"/>
    /// <seealso cref="MapRGB"/>
    /// <seealso cref="MapRGBA"/>
    public static void GetRGB(uint pixel, PixelFormatDetails format, Palette? palette, ref byte r, ref byte g, ref byte b)
    {
        var formatPtr = Marshal.AllocHGlobal(Marshal.SizeOf<PixelFormatDetails>());
        Marshal.StructureToPtr(format, formatPtr, false);

        var palettePtr = palette.HasValue 
            ? Marshal.AllocHGlobal(Marshal.SizeOf<Palette>()) 
            : IntPtr.Zero;

        if (palette.HasValue)
        {
            Marshal.StructureToPtr(palette.Value, palettePtr, false);
        }

        try
        {
            SDL_GetRGB(pixel, formatPtr, palettePtr, ref r, ref g, ref b);
        }
        finally
        {
            if (palette.HasValue) Marshal.FreeHGlobal(palettePtr);
            Marshal.FreeHGlobal(formatPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GetRGBA(uint pixel, IntPtr format, IntPtr palette, ref byte r, ref byte g, ref byte b, ref byte a);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetRGBA(Uint32 pixel, const SDL_PixelFormatDetails *format, const SDL_Palette *palette, Uint8 *r, Uint8 *g, Uint8 *b, Uint8 *a);</code>
    /// <summary>
    /// <para>Get RGBA values from a pixel in the specified format.</para>
    /// <para>This function uses the entire 8-bit [0..255] range when converting color
    /// components from pixel formats with less than 8-bits per RGB component
    /// (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff,
    /// 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).</para>
    /// <para>If the surface has no alpha component, the alpha will be returned as 0xff
    /// (100% opaque).</para>
    /// </summary>
    /// <param name="pixel">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>NULL</c>.</param>
    /// <param name="r">a pointer filled in with the red component, may be <c>NULL</c>.</param>
    /// <param name="g">a pointer filled in with the green component, may be <c>NULL</c>.</param>
    /// <param name="b">a pointer filled in with the blue component, may be <c>NULL</c>.</param>
    /// <param name="a">a pointer filled in with the alpha component, may be <c>NULL</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRGB"/>
    /// <seealso cref="MapRGB"/>
    /// <seealso cref="MapRGBA"/>
    public static void GetRGBA(uint pixel, PixelFormatDetails format, Palette? palette, ref byte r, ref byte g, ref byte b, ref byte a)
    {
        IntPtr formatPtr = Marshal.AllocHGlobal(Marshal.SizeOf<PixelFormatDetails>());
        Marshal.StructureToPtr(format, formatPtr, false);

        IntPtr palettePtr = palette.HasValue 
            ? Marshal.AllocHGlobal(Marshal.SizeOf<Palette>()) 
            : IntPtr.Zero;

        if (palette.HasValue)
        {
            Marshal.StructureToPtr(palette.Value, palettePtr, false);
        }

        try
        {
            SDL_GetRGBA(pixel, formatPtr, palettePtr, ref r, ref g, ref b, ref a);
        }
        finally
        {
            if (palette.HasValue) Marshal.FreeHGlobal(palettePtr);
            Marshal.FreeHGlobal(formatPtr);
        }
    }
}