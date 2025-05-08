#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPixelFormatName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    private static partial IntPtr SDL_GetPixelFormatName(PixelFormat format);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetPixelFormatName(SDL_PixelFormat format);</code>
    /// <summary>
    /// Get the human readable name of a pixel format.
    /// </summary>
    /// <param name="format">the pixel format to query.</param>
    /// <returns>the human readable name of the specified pixel format or
    /// <see cref="PixelFormat.Unknown"/> if the format isn't recognized.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string GetPixelFormatName(PixelFormat format)
    {
        var value = SDL_GetPixelFormatName(format); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetMasksForPixelFormat(SDL_PixelFormat format, int *bpp, Uint32 *Rmask, Uint32 *Gmask, Uint32 *Bmask, Uint32 *Amask);</code>
    /// <summary>
    /// Convert one of the enumerated pixel formats to a bpp value and RGBA masks.
    /// </summary>
    /// <param name="format">one of the <see cref="PixelFormat"/> values.</param>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="rmask">a pointer filled in with the red mask for the format.</param>
    /// <param name="gmask">a pointer filled in with the green mask for the format.</param>
    /// <param name="bmask">a pointer filled in with the blue mask for the format.</param>
    /// <param name="amask">a pointer filled in with the alpha mask for the format.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPixelFormatForMasks"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMasksForPixelFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetMasksForPixelFormat(PixelFormat format, ref int bpp, ref uint rmask, ref uint gmask, ref uint bmask, ref uint amask);

    
    /// <code>extern SDL_DECLSPEC SDL_PixelFormat SDLCALL SDL_GetPixelFormatForMasks(int bpp, Uint32 Rmask, Uint32 Gmask, Uint32 Bmask, Uint32 Amask);</code>
    /// <summary>
    /// <para>Convert a bpp value and RGBA masks to an enumerated pixel format.</para>
    /// <para>This will return <see cref="PixelFormat.Unknown"/> if the conversion wasn't
    /// possible.</para>
    /// </summary>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="rmask">the red mask for the format.</param>
    /// <param name="gmask">the green mask for the format.</param>
    /// <param name="bmask">the blue mask for the format.</param>
    /// <param name="amask">the alpha mask for the format.</param>
    /// <returns>the <see cref="PixelFormat"/> value corresponding to the format masks, or
    /// <see cref="PixelFormat.Unknown"/> if there isn't a match.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMasksForPixelFormat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPixelFormatForMasks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PixelFormat GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask);

    
    /// <code>extern SDL_DECLSPEC const SDL_PixelFormatDetails * SDLCALL SDL_GetPixelFormatDetails(SDL_PixelFormat format);</code>
    /// <summary>
    /// <para>Create an SDL_PixelFormatDetails structure corresponding to a pixel format.</para>
    /// <para>Returned structure may come from a shared global cache (i.e. not newly
    /// allocated), and hence should not be modified, especially the palette. Weird
    /// errors such as `Blit combination not supported` may occur.</para>
    /// </summary>
    /// <param name="format">one of the <see cref="PixelFormat"/> values.</param>
    /// <returns>a pointer to a <see cref="PixelFormatDetails"/> structure or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPixelFormatDetails"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetPixelFormatDetails(PixelFormat format);

    
    /// <code>extern SDL_DECLSPEC SDL_Palette * SDLCALL SDL_CreatePalette(int ncolors);</code>
    /// <summary>
    /// <para>Create a palette structure with the specified number of color entries.</para>
    /// <para>The palette entries are initialized to white.</para>
    /// </summary>
    /// <param name="ncolors">represents the number of color entries in the color palette.</param>
    /// <returns>a new <see cref="Palette"/> structure on success or <c>null</c> on failure (e.g. if
    /// there wasn't enough memory); call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyPalette"/>
    /// <seealso cref="SetPaletteColors"/>
    /// <seealso cref="SetSurfacePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreatePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreatePalette(int ncolors);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetPaletteColors(SDL_Palette *palette, const SDL_Color *colors, int firstcolor, int ncolors);</code>
    /// <summary>
    /// Set a range of colors in a palette.
    /// </summary>
    /// <param name="palette">the <see cref="Palette"/> structure to modify.</param>
    /// <param name="colors">an array of <see cref="Color"/> structures to copy into the palette.</param>
    /// <param name="firstcolor">the index of the first palette entry to modify.</param>
    /// <param name="ncolors">the number of entries to modify.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified or destroyed in another thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetPaletteColors"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPaletteColors(IntPtr palette, [MarshalAs(UnmanagedType.LPArray)] Color[] colors, int firstcolor, int ncolors);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyPalette(SDL_Palette *palette);</code>
    /// <summary>
    /// Free a palette created with <see cref="CreatePalette"/>.
    /// </summary>
    /// <param name="palette">the <see cref="Palette"/> structure to be freed.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified or destroyed in another thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreatePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyPalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyPalette(IntPtr palette);

    
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
    /// <param name="palette">an optional palette for indexed formats, may be <c>null</c>.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MapRGB"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MapRGB(IntPtr format, IntPtr palette, byte r, byte g, byte b);
    
    
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
    /// <param name="palette">an optional palette for indexed formats, may be <c>null</c>.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// ReSharper disable once InvalidXmlDocComment
    /// <seealso cref="GetRGBA(uint, in SDL.PixelFormatDetails, nint, out byte, out byte, out byte, out byte)"/>
    /// <seealso cref="MapRGB"/>
    /// <seealso cref="MapSurfaceRGBA"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MapRGBA"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MapRGBA(IntPtr format, IntPtr palette, byte r, byte g, byte b, byte a);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetRGB(Uint32 pixel, const SDL_PixelFormatDetails *format, const SDL_Palette *palette, Uint8 *r, Uint8 *g, Uint8 *b);</code>
    /// <summary>
    /// <para>Get RGB values from a pixel in the specified format.</para>
    /// <para>This function uses the entire 8-bit [0..255] range when converting color
    /// components from pixel formats with less than 8-bits per RGB component
    /// (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff,
    /// 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).</para>
    /// </summary>
    /// <param name="pixelvalue">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>null</c>.</param>
    /// <param name="r">a pointer filled in with the red component, may be <c>null</c>.</param>
    /// <param name="g">a pointer filled in with the green component, may be <c>null</c>.</param>
    /// <param name="b">a pointer filled in with the blue component, may be <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRGB"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetRGB(uint pixelvalue, in PixelFormatDetails format, IntPtr palette, out byte r, out byte g, out byte b);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetRGB(Uint32 pixel, const SDL_PixelFormatDetails *format, const SDL_Palette *palette, Uint8 *r, Uint8 *g, Uint8 *b);</code>
    /// <summary>
    /// <para>Get RGB values from a pixel in the specified format.</para>
    /// <para>This function uses the entire 8-bit [0..255] range when converting color
    /// components from pixel formats with less than 8-bits per RGB component
    /// (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff,
    /// 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).</para>
    /// </summary>
    /// <param name="pixelvalue">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>null</c>.</param>
    /// <param name="r">a pointer filled in with the red component, may be <c>null</c>.</param>
    /// <param name="g">a pointer filled in with the green component, may be <c>null</c>.</param>
    /// <param name="b">a pointer filled in with the blue component, may be <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [DllImport(SDLLibrary, EntryPoint = "SDL_GetRGB"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static extern void GetRGB(uint pixelvalue, in PixelFormatDetails format, in Palette palette, out byte r, out byte g, out byte b);

    
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
    /// <param name="pixelvalue">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>null</c>.</param>
    /// <param name="r">a pointer filled in with the red component, may be <c>null</c>.</param>
    /// <param name="g">a pointer filled in with the green component, may be <c>null</c>.</param>
    /// <param name="b">a pointer filled in with the blue component, may be <c>null</c>.</param>
    /// <param name="a">a pointer filled in with the alpha component, may be <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// ReSharper disable once InvalidXmlDocComment
    /// <seealso cref="GetRGB(uint, in SDL.PixelFormatDetails, nint, out byte, out byte, out byte)"/>
    /// <seealso cref="MapRGB"/>
    /// <seealso cref="MapRGBA"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRGBA"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetRGBA(uint pixelvalue, in PixelFormatDetails format, IntPtr palette, out byte r, out byte g, out byte b, out byte a);
    
    
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
    /// <param name="pixelvalue">a pixel value.</param>
    /// <param name="format">a pointer to <see cref="PixelFormatDetails"/> describing the pixel
    /// format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be <c>null</c>.</param>
    /// <param name="r">a pointer filled in with the red component, may be <c>null</c>.</param>
    /// <param name="g">a pointer filled in with the green component, may be <c>null</c>.</param>
    /// <param name="b">a pointer filled in with the blue component, may be <c>null</c>.</param>
    /// <param name="a">a pointer filled in with the alpha component, may be <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread, as long as
    /// the palette is not modified.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPixelFormatDetails"/>
    /// ReSharper disable once InvalidXmlDocComment
    /// <seealso cref="GetRGB(uint, in SDL.PixelFormatDetails, nint, out byte, out byte, out byte)"/>
    /// <seealso cref="MapRGB"/>
    /// <seealso cref="MapRGBA"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_GetRGBA"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static extern void GetRGBA(uint pixelvalue, in PixelFormatDetails format, in Palette palette, out byte r, out byte g, out byte b, out byte a);
}