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
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_CreateSurface(int width, int height, SDL_PixelFormat format);</code>
    /// <summary>
    /// <para>Allocate a new surface with a specific pixel format.</para>
    /// <para>The pixels of the new surface are initialized to zero.</para>
    /// </summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat"/> for the new surface's pixel format.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateSurfaceFrom"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSurface(int width, int height, PixelFormat format);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_CreateSurfaceFrom(int width, int height, SDL_PixelFormat format, void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Allocate a new surface with a specific pixel format and existing pixel
    /// data.</para>
    /// <para>No copy is made of the pixel data. Pixel data is not managed automatically;
    /// you must free the surface before you free the pixel data.</para>
    /// <para>Pitch is the offset in bytes from one row of pixels to the next, e.g.
    /// <c>width*4</c> for <see cref="PixelFormat.RGBA8888"/>.</para>
    /// <para>You may pass <c>null</c> for pixels and 0 for pitch to create a surface that you
    /// will fill in with valid values later.</para>
    /// </summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat"/> for the new surface's pixel format.</param>
    /// <param name="pixels">a pointer to existing pixel data.</param>
    /// <param name="pitch">the number of bytes between each row, including padding.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSurfaceFrom"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSurfaceFrom(int width, int height, PixelFormat format, IntPtr pixels, int pitch);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroySurface(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Free a surface.</para>
    /// <para>It is safe to pass <c>null</c> to this function.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> to free.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <threadsafety>No other thread should be using the surface when it is freed.</threadsafety>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="CreateSurfaceFrom"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroySurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySurface(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetSurfaceProperties(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Get the properties associated with a surface.</para>
    /// <para>The following properties are understood by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.SurfaceSDRWhitePointFloat"/>: for HDR10 and floating point
    /// surfaces, this defines the value of 100% diffuse white, with higher
    /// values being displayed in the High Dynamic Range headroom. This defaults
    /// to 203 for HDR10 surfaces and 1.0 for floating point surfaces.</item>
    /// <item><see cref="Props.SurfaceHDRHeadroomFloat"/>: for HDR10 and floating point
    /// surfaces, this defines the maximum dynamic range used by the content, in
    /// terms of the SDR white point. This defaults to 0.0, which disables tone
    /// mapping.</item>
    /// <item><see cref="Props.SurfaceHDRHeadroomFloat"/>: for HDR10 and floating point
    /// surfaces, this defines the maximum dynamic range used by the content, in
    /// terms of the SDR white point. This defaults to 0.0, which disables tone
    /// mapping.</item>
    /// <item><see cref="Props.SurfaceTonemapOperatorString"/>: the tone mapping operator
    /// used when compressing from a surface with high dynamic range to another
    /// with lower dynamic range. Currently this supports "chrome", which uses
    /// the same tone mapping that Chrome uses for HDR content, the form "*=N",
    /// where N is a floating point scale factor applied in linear space, and
    /// "none", which disables tone mapping. This defaults to "chrome".</item>
    /// <item><see cref="Props.SurfaceHotspotXNumber"/>: the hotspot pixel offset from the
    /// left edge of the image, if this surface is being used as a cursor.</item>
    /// <item><see cref="Props.SurfaceHotspotYNumber"/>: the hotspot pixel offset from the
    /// top edge of the image, if this surface is being used as a cursor.</item>
    /// </list>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns>a valid property ID on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetSurfaceProperties(IntPtr surface);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceColorspace(SDL_Surface *surface, SDL_Colorspace colorspace);</code>
    /// <summary>
    /// <para>Set the colorspace used by a surface.</para>
    /// <para>Setting the colorspace doesn't change the pixels, only how they are
    /// interpreted in color operations.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="colorspace">an <see cref="Colorspace"/> value describing the surface
    /// colorspace.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceColorspace"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceColorspace(IntPtr surface, Colorspace colorspace);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Colorspace SDLCALL SDL_GetSurfaceColorspace(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Get the colorspace used by a surface.</para>
    /// <para>The colorspace defaults to <see cref="Colorspace.SRGBLinear"/> for floating point
    /// formats, <see cref="Colorspace.HDR10"/> for 10-bit formats, <see cref="Colorspace.SRGB"/> for
    /// other RGB surfaces and <see cref="Colorspace.BT709Full"/> for YUV textures.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns>the colorspace used by the surface, or <see cref="Colorspace.Unknown"/> if
    /// the surface is <c>null</c>.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfaceColorspace"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Colorspace GetSurfaceColorspace(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Palette * SDLCALL SDL_CreateSurfacePalette(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Create a palette and associate it with a surface.</para>
    /// <para>This function creates a palette compatible with the provided surface. The
    /// palette is then returned for you to modify, and the surface will
    /// automatically use the new palette in future operations. You do not need to
    /// destroy the returned palette, it will be freed when the reference count
    /// reaches 0, usually when the surface is destroyed.</para>
    /// <para>Bitmap surfaces (with format <see cref="PixelFormat.Index1LSB"/> or
    /// <see cref="PixelFormat.Index1MSB"/>) will have the palette initialized with 0 as
    /// white and 1 as black. Other surfaces will get a palette initialized with
    /// white in every entry.</para>
    /// <para>If this function is called for a surface that already has a palette, a new
    /// palette will be created to replace it.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <returns>a new <see cref="Palette"/> structure on success or <c>null</c> on failure (e.g. if
    /// the surface didn't have an index format); call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetPaletteColors"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSurfacePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSurfacePalette(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfacePalette(SDL_Surface *surface, SDL_Palette *palette);</code>
    /// <summary>
    /// <para>Set the palette used by a surface.</para>
    /// <para>Setting the palette keeps an internal reference to the palette, which can
    /// be safely destroyed afterwards.</para>
    /// <para>A single palette can be shared with many surfaces.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="palette">the <see cref="Palette"/> structure to use.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreatePalette"/>
    /// <seealso cref="GetSurfacePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfacePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfacePalette(IntPtr surface, IntPtr palette);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Palette * SDLCALL SDL_GetSurfacePalette(SDL_Surface *surface);</code>
    /// <summary>
    /// Get the palette used by a surface.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns>a pointer to the palette used by the surface, or <c>null</c> if there is
    /// no palette used.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfacePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfacePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetSurfacePalette(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AddSurfaceAlternateImage(SDL_Surface *surface, SDL_Surface *image);</code>
    /// <summary>
    /// <para>Add an alternate version of a surface.</para>
    /// <para>This function adds an alternate version of this surface, usually used for
    /// content with high DPI representations like cursors or icons. The size,
    /// format, and content do not need to match the original surface, and these
    /// alternate versions will not be updated when the original surface changes.</para>
    /// <para>This function adds a reference to the alternate version, so you should call
    /// <see cref="DestroySurface"/> on the image after this call.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="image">a pointer to an alternate <see cref="Surface"/> to associate with this
    /// surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RemoveSurfaceAlternateImages"/>
    /// <seealso cref="GetSurfaceImages"/>
    /// <seealso cref="SurfaceHasAlternateImages"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddSurfaceAlternateImage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool AddSurfaceAlternateImage(IntPtr surface, IntPtr image);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SurfaceHasAlternateImages(SDL_Surface *surface);</code>
    /// <summary>
    /// Return whether a surface has alternate versions available.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns><c>true</c> if alternate versions are available or <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="RemoveSurfaceAlternateImages"/>
    /// <seealso cref="GetSurfaceImages"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SurfaceHasAlternateImages"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SurfaceHasAlternateImages(IntPtr surface);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceImages"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSurfaceImages(IntPtr surface, out int count);
    /// <code>extern SDL_DECLSPEC SDL_Surface ** SDLCALL SDL_GetSurfaceImages(SDL_Surface *surface, int *count);</code>
    /// <summary>
    /// <para>Get an array including all versions of a surface.</para>
    /// <para>This returns all versions of a surface, with the surface being queried as
    /// the first element in the returned array.</para>
    /// <para>Freeing the array of surfaces does not affect the surfaces in the array.
    /// They are still referenced by the surface being queried and will be cleaned
    /// up normally.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="count">a pointer filled in with the number of surface pointers
    /// returned, may be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of <see cref="Surface"/> pointers or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information. This should be
    /// freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="RemoveSurfaceAlternateImages"/>
    /// <seealso cref="SurfaceHasAlternateImages"/>
    public static IntPtr[]? GetSurfaceImages(IntPtr surface, out int count)
    {
        var ptr = SDL_GetSurfaceImages(surface, out count);
        
        try
        {
            return PointerToPointerArray(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_RemoveSurfaceAlternateImages(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Remove all alternate versions of a surface.</para>
    /// <para>This function removes a reference from all the alternative versions,
    /// destroying them if this is the last reference to them.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddSurfaceAlternateImage"/>
    /// <seealso cref="GetSurfaceImages"/>
    /// <seealso cref="SurfaceHasAlternateImages"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveSurfaceAlternateImages"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveSurfaceAlternateImages(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LockSurface(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Set up a surface for directly accessing the pixels.</para>
    /// <para>Between calls to <see cref="LockSurface"/> / <see cref="UnlockSurface"/>, you can write to
    /// and read from <c>Surface.Pixels</c>, using the pixel format stored in
    /// <c>Surface.Format</c>. Once you are done accessing the surface, you should use
    /// <see cref="UnlockSurface"/> to release it.</para>
    /// <para>Not all surfaces require locking. If <c>MustLock(surface)</c> evaluates to
    /// 0, then you can read and write to the surface at any time, and the pixel
    /// format of the surface will not change.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be locked.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe. The locking referred to by
    /// this function is making the pixels available for direct
    /// access, not thread-safe locking.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MustLock"/>
    /// <seealso cref="UnlockSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockSurface(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockSurface(SDL_Surface *surface);</code>
    /// <summary>
    /// Release a surface after directly accessing the pixels.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be unlocked.</param>
    /// <threadsafety>This function is not thread safe. The locking referred to by
    /// this function is making the pixels available for direct
    /// access, not thread-safe locking.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockSurface(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_LoadBMP_IO(SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load a BMP image from a seekable SDL data stream.</para>
    /// <para>The new surface should be freed with <see cref="DestroySurface"/>. Not doing so
    /// will result in a memory leak.</para>
    /// </summary>
    /// <param name="src">the data stream for the surface.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>src</c> before returning, even
    /// in the case of an error.</param>
    /// <returns>a pointer to a new <see cref="Surface"/> structure or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroySurface"/>
    /// <seealso cref="LoadBMP"/>
    /// <seealso cref="SaveBMPIO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadBMP_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadBMPIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_LoadBMP(const char *file);</code>
    /// <summary>
    /// <para>Load a BMP image from a file.</para>
    /// <para>The new surface should be freed with <see cref="DestroySurface"/>. Not doing so
    /// will result in a memory leak.</para>
    /// </summary>
    /// <param name="file">the BMP file to load.</param>
    /// <returns>a pointer to a new <see cref="Surface"/> structure or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroySurface"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="SaveBMP"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadBMP"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadBMP([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SaveBMP_IO(SDL_Surface *surface, SDL_IOStream *dst, bool closeio);</code>
    /// <summary>
    /// <para>Save a surface to a seekable SDL data stream in BMP format.</para>
    /// <para>Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the
    /// BMP directly. Other RGB formats with 8-bit or higher get converted to a
    /// 24-bit surface or, if they have an alpha mask or a colorkey, to a 32-bit
    /// surface before they are saved. YUV and paletted 1-bit and 4-bit formats are
    /// not supported.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure containing the image to be saved.</param>
    /// <param name="dst">a data stream to save to.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>dst</c> before returning, even
    /// in the case of an error.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="SaveBMP"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SaveBMP_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveBMPIO(IntPtr surface, IntPtr dst, [MarshalAs(UnmanagedType.I1)] bool closeio);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SaveBMP(SDL_Surface *surface, const char *file);</code>
    /// <summary>
    /// <para>Save a surface to a file.</para>
    /// <para>Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the
    /// BMP directly. Other RGB formats with 8-bit or higher get converted to a
    /// 24-bit surface or, if they have an alpha mask or a colorkey, to a 32-bit
    /// surface before they are saved. YUV and paletted 1-bit and 4-bit formats are
    /// not supported.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure containing the image to be saved.</param>
    /// <param name="file">a file to save to.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LoadBMP"/>
    /// <seealso cref="SaveBMPIO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SaveBMP"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveBMP(IntPtr surface, [MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceRLE(SDL_Surface *surface, bool enabled);</code>
    /// <summary>
    /// <para>Set the RLE acceleration hint for a surface.</para>
    /// <para>If RLE is enabled, color key and alpha blending blits are much faster, but
    /// the surface must be locked before directly accessing the pixels.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to optimize.</param>
    /// <param name="enabled"><c>true</c> to enable RLE acceleration, <c>false</c> to disable it.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    /// <seealso cref="LockSurface"/>
    /// <seealso cref="UnlockSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceRLE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceRLE(IntPtr surface, [MarshalAs(UnmanagedType.I1)] bool enabled);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SurfaceHasRLE(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Returns whether the surface is RLE enabled.</para>
    /// <para>It is safe to pass a <c>null</c> <c>surface</c> here; it will return false.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns><c>true</c> if the surface is RLE enabled, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfaceRLE"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SurfaceHasRLE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SurfaceHasRLE(IntPtr surface);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceColorKey(SDL_Surface *surface, bool enabled, Uint32 key);</code>
    /// <summary>
    /// <para>Set the color key (transparent pixel) in a surface.</para>
    /// <para>The color key defines a pixel value that will be treated as transparent in
    /// a blit. For example, one can use this to specify that cyan pixels should be
    /// considered transparent, and therefore not rendered.</para>
    /// <para>It is a pixel of the format used by the surface, as generated by
    /// <see cref="MapRGB"/>.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="enabled"><c>true</c> to enable color key, false to disable color key.</param>
    /// <param name="key">the transparent pixel.</param>
    /// <returns><c>true</c> on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceColorKey"/>
    /// <seealso cref="SetSurfaceRLE"/>
    /// <seealso cref="SurfaceHasColorKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceColorKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceColorKey(IntPtr surface, [MarshalAs(UnmanagedType.I1)] bool enabled, uint key);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SurfaceHasColorKey(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Returns whether the surface has a color key.</para>
    /// <para>It is safe to pass a <c>null</c> <c>surface</c> here; it will return false.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns><c>true</c> if the surface has a color key, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfaceColorKey"/>
    /// <seealso cref="GetSurfaceColorKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SurfaceHasColorKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SurfaceHasColorKey(IntPtr surface);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetSurfaceColorKey(SDL_Surface *surface, Uint32 *key);</code>
    /// <summary>
    /// <para>Get the color key (transparent pixel) for a surface.</para>
    /// <para>The color key is a pixel of the format used by the surface, as generated by
    /// <see cref="MapRGB"/>.</para>
    /// <para>If the surface doesn't have color key enabled this function returns false.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="key">a pointer filled in with the transparent pixel.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfaceColorKey"/>
    /// <seealso cref="SurfaceHasColorKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceColorKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetSurfaceColorKey(IntPtr surface, out uint key);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceColorMod(SDL_Surface *surface, Uint8 r, Uint8 g, Uint8 b);</code>
    /// <summary>
    /// <para>Set an additional color value multiplied into blit operations.</para>
    /// <para>When this surface is blitted, during the blit operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:</para>
    /// <code>srcC = srcC * (color / 255)</code>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="r">the red color value multiplied into blit operations.</param>
    /// <param name="g">the green color value multiplied into blit operations.</param>
    /// <param name="b">the blue color value multiplied into blit operations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceColorMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetSurfaceColorMod(SDL_Surface *surface, Uint8 *r, Uint8 *g, Uint8 *b);</code>
    /// <summary>
    /// Get the additional color value multiplied into blit operations.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceAlphaMod"/>
    /// <seealso cref="SetSurfaceColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceColorMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceAlphaMod(SDL_Surface *surface, Uint8 alpha);</code>
    /// <summary>
    /// <para>Set an additional alpha value used in blit operations.</para>
    /// <para>When this surface is blitted, during the blit operation the source alpha
    /// value is modulated by this alpha value according to the following formula:</para>
    /// <code>srcA = srcA * (alpha / 255)</code>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="alpha">the alpha value multiplied into blit operations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceAlphaMod"/>
    /// <seealso cref="SetSurfaceColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceAlphaMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceAlphaMod(IntPtr surface, byte alpha);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetSurfaceAlphaMod(SDL_Surface *surface, Uint8 *alpha);</code>
    /// <summary>
    /// Get the additional alpha value used in blit operations.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceAlphaMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetSurfaceAlphaMod(IntPtr surface, out byte alpha);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceBlendMode(SDL_Surface *surface, SDL_BlendMode blendMode);</code>
    /// <summary>
    /// <para>Set the blend mode used for blit operations.</para>
    /// <para>To copy a surface to another surface (or texture) without blending with the
    /// existing data, the blendmode of the SOURCE surface should be set to
    /// <see cref="BlendMode.None"/>.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="blendMode">the <see cref="BlendMode"/> to use for blit blending.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceBlendMode(IntPtr surface, BlendMode blendMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetSurfaceBlendMode(SDL_Surface *surface, SDL_BlendMode *blendMode);</code>
    /// <summary>
    /// Get the blend mode used for blit operations.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfaceBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetSurfaceBlendMode(IntPtr surface, out BlendMode blendMode);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceClipRect(SDL_Surface *surface, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Set the clipping rectangle for a surface.</para>
    /// <para>When <c>surface</c> is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.</para>
    /// <para>Note that blits are automatically clipped to the edges of the source and
    /// destination surfaces.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be clipped.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the clipping rectangle, or
    /// <c>null</c> to disable clipping.</param>
    /// <returns>true if the rectangle intersects the surface, otherwise false and
    /// blits will be completely clipped.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceClipRect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceClipRect(IntPtr surface, IntPtr rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetSurfaceClipRect(SDL_Surface *surface, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Set the clipping rectangle for a surface.</para>
    /// <para>When <c>surface</c> is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.</para>
    /// <para>Note that blits are automatically clipped to the edges of the source and
    /// destination surfaces.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be clipped.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the clipping rectangle, or
    /// <c>null</c> to disable clipping.</param>
    /// <returns>true if the rectangle intersects the surface, otherwise false and
    /// blits will be completely clipped.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetSurfaceClipRect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSurfaceClipRect(IntPtr surface, in Rect rect);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetSurfaceClipRect(SDL_Surface *surface, SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Get the clipping rectangle for a surface.</para>
    /// <para>When <c>surface</c> is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure representing the surface to be
    /// clipped.</param>
    /// <param name="rect">an <see cref="Rect"/> structure filled in with the clipping rectangle for
    /// the surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetSurfaceClipRect(nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetSurfaceClipRect(IntPtr surface, out Rect rect);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FlipSurface(SDL_Surface *surface, SDL_FlipMode flip);</code>
    /// <summary>
    /// Flip a surface vertically or horizontally.
    /// </summary>
    /// <param name="surface">the surface to flip.</param>
    /// <param name="flip">the direction to flip.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlipSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FlipSurface(IntPtr surface, FlipMode flip);
    

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_DuplicateSurface(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Creates a new surface identical to the existing surface.</para>
    /// <para>If the original surface has alternate images, the new surface will have a
    /// reference to them as well.</para>
    /// <para>The returned surface should be freed with <see cref="DestroySurface"/>.</para>
    /// </summary>
    /// <param name="surface">the surface to duplicate.</param>
    /// <returns>a copy of the surface or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DuplicateSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr DuplicateSurface(IntPtr surface);

    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_ScaleSurface(SDL_Surface *surface, int width, int height, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// <para>Creates a new surface identical to the existing surface, scaled to the
    /// desired size.</para>
    /// <para>The returned surface should be freed with <see cref="DestroySurface"/>.</para>
    /// </summary>
    /// <param name="surface">the surface to duplicate and scale.</param>
    /// <param name="width">the width of the new surface.</param>
    /// <param name="height">the height of the new surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns>a copy of the surface or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ScaleSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ScaleSurface(IntPtr surface, int width, int height, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_ConvertSurface(SDL_Surface *surface, SDL_PixelFormat format);</code>
    /// <summary>
    /// <para>Copy an existing surface to a new surface of the specified format.</para>
    /// <para>This function is used to optimize images for faster <c>repeat</c> blitting. This
    /// is accomplished by converting the original and storing the result as a new
    /// surface. The new, optimized surface can then be used as the source for
    /// future blits, making them faster.</para>
    /// <para>If you are converting to an indexed surface and want to map colors to a
    /// palette, you can use <see cref="ConvertSurfaceAndColorspace"/> instead.</para>
    /// <para>If the original surface has alternate images, the new surface will have a
    /// reference to them as well.</para>
    /// </summary>
    /// <param name="surface">the existing <see cref="Surface"/> structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertSurfaceAndColorspace"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ConvertSurface(IntPtr surface, PixelFormat format);
    

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_ConvertSurfaceAndColorspace(SDL_Surface *surface, SDL_PixelFormat format, SDL_Palette *palette, SDL_Colorspace colorspace, SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Copy an existing surface to a new surface of the specified format and
    /// colorspace.</para>
    /// <para>This function converts an existing surface to a new format and colorspace
    /// and returns the new surface. This will perform any pixel format and
    /// colorspace conversion needed.</para>
    /// <para>If the original surface has alternate images, the new surface will have a
    /// reference to them as well.</para>
    /// </summary>
    /// <param name="surface">the existing <see cref="Surface"/> structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <param name="palette">an optional palette to use for indexed formats, may be <c>null</c>.</param>
    /// <param name="colorspace">the new colorspace.</param>
    /// <param name="props">an SDL_PropertiesID with additional color properties, or 0.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertSurface"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertSurfaceAndColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ConvertSurfaceAndColorspace(IntPtr surface, PixelFormat format, IntPtr palette, Colorspace colorspace, uint props);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format to another format.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixelsAndColorspace(int, int, PixelFormat, Colorspace, uint, byte[], int, PixelFormat, Colorspace, uint, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixels(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, out IntPtr dst, int dstPitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format to another format.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixelsAndColorspace(int, int, PixelFormat, Colorspace, uint, byte[], int, PixelFormat, Colorspace, uint, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixels(int width, int height, PixelFormat srcFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] src, int srcPitch, PixelFormat dstFormat, out IntPtr dst, int dstPitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format to another format.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixelsAndColorspace(int, int, PixelFormat, Colorspace, uint, byte[], int, PixelFormat, Colorspace, uint, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixels(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 7)] out byte[] dst, int dstPitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format to another format.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixelsAndColorspace(int, int, PixelFormat, Colorspace, uint, byte[], int, PixelFormat, Colorspace, uint, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixels(int width, int height, PixelFormat srcFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] src, int srcPitch, PixelFormat dstFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 7)] out byte[] dst, int dstPitch);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, SDL_PropertiesID src_properties, const void *src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, SDL_PropertiesID dst_properties, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format and colorspace to another format and
    /// colorspace.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="srcColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>src</c> pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color
    /// properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dstColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>dst</c> pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color
    /// properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixels(int, int, PixelFormat, byte[], int, PixelFormat, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixelsAndColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, IntPtr src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, out IntPtr dst, int dstPitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, SDL_PropertiesID src_properties, const void *src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, SDL_PropertiesID dst_properties, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format and colorspace to another format and
    /// colorspace.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="srcColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>src</c> pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color
    /// properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dstColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>dst</c> pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color
    /// properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixels(int, int, PixelFormat, byte[], int, PixelFormat, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixelsAndColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, out IntPtr dst, int dstPitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, SDL_PropertiesID src_properties, const void *src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, SDL_PropertiesID dst_properties, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format and colorspace to another format and
    /// colorspace.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="srcColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>src</c> pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color
    /// properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dstColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>dst</c> pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color
    /// properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixels(int, int, PixelFormat, byte[], int, PixelFormat, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixelsAndColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, IntPtr src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 11)] out byte[] dst, int dstPitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, SDL_PropertiesID src_properties, const void *src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, SDL_PropertiesID dst_properties, void *dst, int dst_pitch);</code>
    /// <summary>
    /// Copy a block of pixels of one format and colorspace to another format and
    /// colorspace.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="srcColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>src</c> pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color
    /// properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dstColorspace">an <see cref="Colorspace"/> value describing the colorspace of
    /// the <c>dst</c> pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color
    /// properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertPixels(int, int, PixelFormat, byte[], int, PixelFormat, out byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertPixelsAndColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 11)] out byte[] dst, int dstPitch);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch, bool linear);</code>
    /// <summary>
    /// <para>Premultiply the alpha on a block of pixels.</para>
    /// <para>This is safe to use with src == dst, but not for other overlapping areas.</para>
    /// </summary>
    /// <param name="width">the width of the block to convert, in pixels.</param>
    /// <param name="height">the height of the block to convert, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <param name="linear">true to convert from sRGB to linear space for the alpha
    /// multiplication, false to do multiplication in sRGB space.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PremultiplyAlpha"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, IntPtr dst, int dstPitch, [MarshalAs(UnmanagedType.I1)] bool linear);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch, bool linear);</code>
    /// <summary>
    /// <para>Premultiply the alpha on a block of pixels.</para>
    /// <para>This is safe to use with src == dst, but not for other overlapping areas.</para>
    /// </summary>
    /// <param name="width">the width of the block to convert, in pixels.</param>
    /// <param name="height">the height of the block to convert, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <param name="linear">true to convert from sRGB to linear space for the alpha
    /// multiplication, false to do multiplication in sRGB space.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PremultiplyAlpha"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] src, int srcPitch, PixelFormat dstFormat, IntPtr dst, int dstPitch, [MarshalAs(UnmanagedType.I1)] bool linear);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch, bool linear);</code>
    /// <summary>
    /// <para>Premultiply the alpha on a block of pixels.</para>
    /// <para>This is safe to use with src == dst, but not for other overlapping areas.</para>
    /// </summary>
    /// <param name="width">the width of the block to convert, in pixels.</param>
    /// <param name="height">the height of the block to convert, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <param name="linear">true to convert from sRGB to linear space for the alpha
    /// multiplication, false to do multiplication in sRGB space.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PremultiplyAlpha"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 7)] out byte[] dst, int dstPitch, [MarshalAs(UnmanagedType.I1)] bool linear);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, const void *src, int src_pitch, SDL_PixelFormat dst_format, void *dst, int dst_pitch, bool linear);</code>
    /// <summary>
    /// <para>Premultiply the alpha on a block of pixels.</para>
    /// <para>This is safe to use with src == dst, but not for other overlapping areas.</para>
    /// </summary>
    /// <param name="width">the width of the block to convert, in pixels.</param>
    /// <param name="height">the height of the block to convert, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <param name="linear">true to convert from sRGB to linear space for the alpha
    /// multiplication, false to do multiplication in sRGB space.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>The same destination pixels should not be used from two
    /// threads at once. It is safe to use the same source pixels
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PremultiplyAlpha"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PremultiplyAlpha(int width, int height, PixelFormat srcFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] src, int srcPitch, PixelFormat dstFormat, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 7)] out byte[] dst, int dstPitch, [MarshalAs(UnmanagedType.I1)] bool linear);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PremultiplySurfaceAlpha(SDL_Surface *surface, bool linear);</code>
    /// <summary>
    /// <para>Premultiply the alpha in a surface.</para>
    /// <para>This is safe to use with src == dst, but not for other overlapping areas.</para>
    /// </summary>
    /// <param name="surface">the surface to modify.</param>
    /// <param name="linear"><c>true</c> to convert from sRGB to linear space for the alpha
    /// multiplication, false to do multiplication in sRGB space.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PremultiplySurfaceAlpha"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PremultiplySurfaceAlpha(IntPtr surface, [MarshalAs(UnmanagedType.I1)] bool linear);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ClearSurface(SDL_Surface *surface, float r, float g, float b, float a);</code>
    /// <summary>
    /// <para>Clear a surface with a specific color, with floating point precision.</para>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// <para>If the surface is YUV, the color is assumed to be in the sRGB colorspace,
    /// otherwise the color is assumed to be in the colorspace of the suface.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> to clear.</param>
    /// <param name="r">the red component of the pixel, normally in the range 0-1.</param>
    /// <param name="g">the green component of the pixel, normally in the range 0-1.</param>
    /// <param name="b">the blue component of the pixel, normally in the range 0-1.</param>
    /// <param name="a">the alpha component of the pixel, normally in the range 0-1.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClearSurface(IntPtr surface, float r, float g, float b, float a);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FillSurfaceRect(SDL_Surface *dst, const SDL_Rect *rect, Uint32 color);</code>
    /// <summary>
    /// <para>Perform a fast fill of a rectangle with a specific color.</para>
    /// <para><c>color</c> should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRGB"/> or <see cref="MapRGBA"/>. If the color value contains an
    /// alpha component then the destination is simply filled with that alpha
    /// information, no blending takes place.</para>
    /// <para>If there is a clip rectangle set on the destination (set via
    /// <see cref="SetSurfaceClipRect(nint, nint)"/>), then this function will fill based on the
    /// intersection of the clip rectangle and <c>rect</c>.</para>
    /// </summary>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the rectangle to fill, or
    /// <c>null</c> to fill the entire surface.</param>
    /// <param name="color">the color to fill with.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="FillSurfaceRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FillSurfaceRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FillSurfaceRect(IntPtr dst, IntPtr rect, uint color);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FillSurfaceRect(SDL_Surface *dst, const SDL_Rect *rect, Uint32 color);</code>
    /// <summary>
    /// <para>Perform a fast fill of a rectangle with a specific color.</para>
    /// <para><c>color</c> should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRGB"/> or <see cref="MapRGBA"/>. If the color value contains an
    /// alpha component then the destination is simply filled with that alpha
    /// information, no blending takes place.</para>
    /// <para>If there is a clip rectangle set on the destination (set via
    /// <see cref="SetSurfaceClipRect(nint, nint)"/>), then this function will fill based on the
    /// intersection of the clip rectangle and <c>rect</c>.</para>
    /// </summary>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the rectangle to fill, or
    /// <c>null</c> to fill the entire surface.</param>
    /// <param name="color">the color to fill with.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="FillSurfaceRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FillSurfaceRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FillSurfaceRect(IntPtr dst, in Rect rect, uint color);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FillSurfaceRects(SDL_Surface *dst, const SDL_Rect *rects, int count, Uint32 color);</code>
    /// <summary>
    /// <para>Perform a fast fill of a set of rectangles with a specific color.</para>
    /// <para><c>color</c> should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRGB"/> or <see cref="MapRGBA"/>. If the color value contains an
    /// alpha component then the destination is simply filled with that alpha
    /// information, no blending takes place.</para>
    /// <para>If there is a clip rectangle set on the destination (set via
    /// <see cref="SetSurfaceClipRect(nint, nint)"/>), then this function will fill based on the
    /// intersection of the clip rectangle and `rect`.</para>
    /// </summary>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rects">an array of <see cref="Rect"/> representing the rectangles to fill.</param>
    /// <param name="count">the number of rectangles in the array.</param>
    /// <param name="color">the color to fill with.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="FillSurfaceRect(nint, nint, uint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FillSurfaceRects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FillSurfaceRects(IntPtr dst, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] Rect[] rects, int count, uint color);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Performs a fast blit from the source surface to the destination surface
    /// with clipping.</para>
    /// <para>If either <c>srcrect</c> or <c>dstrect</c> are <c>null</c>, the entire surface (<c>src</c> or
    /// <c>dst</c>) is copied while ensuring clipping to <c>dst.clip_rect</c>.</para>
    /// <para>The blit function should not be called on a locked surface.</para>
    /// <para>The blit semantics for surfaces with and without blending and colorkey are
    /// defined as follows:</para>
    /// <code>
    /// RGBA->RGB:
    ///      Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///       alpha-blend (using the source alpha-channel and per-surface alpha)
    ///       SDL_SRCCOLORKEY ignored.
    ///     Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///       copy RGB.
    ///       if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB, set destination alpha to source per-surface alpha value.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    ///
    ///RGBA->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source alpha-channel and per-surface alpha)
    ///    SDL_SRCCOLORKEY ignored.
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy all of RGBA to the destination.
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGB:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    /// </code>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c>to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the x and y position in
    /// the destination surface, or <c>null</c> for (0,0). The width and
    /// height are ignored, and are copied from <c>srcrect</c>. If you
    /// want a specific width and height, you should use
    /// <see cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Performs a fast blit from the source surface to the destination surface.</para>
    /// <para>This assumes that the source and destination rectangles are the same size.
    /// If either <c>srcrect</c> or <c>dstrect</c> are <c>null</c>, the entire surface (<c>src</c> or
    /// <c>dst</c>) is copied. The final blit rectangles are saved in <c>srcrect</c> and
    /// <c>dstrect</c> after all clipping is performed.</para>
    /// <para>The blit semantics for surfaces with and without blending and colorkey are
    /// defined as follows:</para>
    /// <code>
    /// RGBA->RGB:
    ///      Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///       alpha-blend (using the source alpha-channel and per-surface alpha)
    ///       SDL_SRCCOLORKEY ignored.
    ///     Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///       copy RGB.
    ///       if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB, set destination alpha to source per-surface alpha value.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    ///
    ///RGBA->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source alpha-channel and per-surface alpha)
    ///    SDL_SRCCOLORKEY ignored.
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy all of RGBA to the destination.
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGB:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    /// </code>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c>to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the x and y position in
    /// the destination surface, or <c>null</c> for (0,0). The width and
    /// height are ignored, and are copied from <c>srcrect</c>. If you
    /// want a specific width and height, you should use
    /// <see cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Performs a fast blit from the source surface to the destination surface.</para>
    /// <para>This assumes that the source and destination rectangles are the same size.
    /// If either <c>srcrect</c> or <c>dstrect</c> are <c>null</c>, the entire surface (<c>src</c> or
    /// <c>dst</c>) is copied. The final blit rectangles are saved in <c>srcrect</c> and
    /// <c>dstrect</c> after all clipping is performed.</para>
    /// <para>The blit semantics for surfaces with and without blending and colorkey are
    /// defined as follows:</para>
    /// <code>
    /// RGBA->RGB:
    ///      Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///       alpha-blend (using the source alpha-channel and per-surface alpha)
    ///       SDL_SRCCOLORKEY ignored.
    ///     Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///       copy RGB.
    ///       if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB, set destination alpha to source per-surface alpha value.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    ///
    ///RGBA->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source alpha-channel and per-surface alpha)
    ///    SDL_SRCCOLORKEY ignored.
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy all of RGBA to the destination.
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGB:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    /// </code>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c>to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the x and y position in
    /// the destination surface, or <c>null</c> for (0,0). The width and
    /// height are ignored, and are copied from <c>srcrect</c>. If you
    /// want a specific width and height, you should use
    /// <see cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface(IntPtr src, in Rect srcrect, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Performs a fast blit from the source surface to the destination surface.</para>
    /// <para>This assumes that the source and destination rectangles are the same size.
    /// If either <c>srcrect</c> or <c>dstrect</c> are <c>null</c>, the entire surface (<c>src</c> or
    /// <c>dst</c>) is copied. The final blit rectangles are saved in <c>srcrect</c> and
    /// <c>dstrect</c> after all clipping is performed.</para>
    /// <para>The blit semantics for surfaces with and without blending and colorkey are
    /// defined as follows:</para>
    /// <code>
    /// RGBA->RGB:
    ///      Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///       alpha-blend (using the source alpha-channel and per-surface alpha)
    ///       SDL_SRCCOLORKEY ignored.
    ///     Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///       copy RGB.
    ///       if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB, set destination alpha to source per-surface alpha value.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    ///
    ///RGBA->RGBA:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source alpha-channel and per-surface alpha)
    ///    SDL_SRCCOLORKEY ignored.
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy all of RGBA to the destination.
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    RGB values of the source color key, ignoring alpha in the
    ///    comparison.
    ///
    ///RGB->RGB:
    ///  Source surface blend mode set to SDL_BLENDMODE_BLEND:
    ///    alpha-blend (using the source per-surface alpha)
    ///  Source surface blend mode set to SDL_BLENDMODE_NONE:
    ///    copy RGB.
    ///  both:
    ///    if SDL_SRCCOLORKEY set, only copy the pixels that do not match the
    ///    source color key.
    /// </code>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c>to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the x and y position in
    /// the destination surface, or <c>null</c> for (0,0). The width and
    /// height are ignored, and are copied from <c>srcrect</c>. If you
    /// want a specific width and height, you should use
    /// <see cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceUnchecked(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform low-level surface blitting only.</para>
    /// <para>This is a semi-private blit function and it performs low-level surface
    /// blitting, assuming the input rectangles have already been clipped.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, may not be <c>null</c>.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, may not be <c>null</c>.</param>
    /// <returns><c>true on</c> success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceUnchecked"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceUnchecked(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceScaled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different
    /// format.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceScaled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different
    /// format.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceScaled(IntPtr src, in Rect srcrect, IntPtr dst, IntPtr dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceScaled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different
    /// format.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceScaled(IntPtr src, IntPtr srcrect, IntPtr dst, in Rect dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceScaled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different
    /// format.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceScaled(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceUncheckedScaled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// <para>Perform low-level surface scaled blitting only.</para>
    /// <para>This is a semi-private function and it performs low-level surface blitting,
    /// assuming the input rectangles have already been clipped.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, may not be <c>null</c>.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, may not be <c>null</c>.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurfaceScaled(nint, nint, nint, nint, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceUncheckedScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceUncheckedScaled(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StretchSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a stretched pixel copy from one surface to another.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or NULL to copy the entire surface..</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or NULL to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="BlitSurfaceScaled(IntPtr, IntPtr, IntPtr, IntPtr, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StretchSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    [return: MarshalAs(UnmanagedType.I1)] 
    public static partial bool StretchSurface(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StretchSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a stretched pixel copy from one surface to another.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or NULL to copy the entire surface..</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or NULL to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="BlitSurfaceScaled(IntPtr, IntPtr, IntPtr, IntPtr, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StretchSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    [return: MarshalAs(UnmanagedType.I1)] 
    public static partial bool StretchSurface(IntPtr src, IntPtr srcrect, IntPtr dst, in Rect dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StretchSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a stretched pixel copy from one surface to another.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or NULL to copy the entire surface..</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or NULL to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="BlitSurfaceScaled(IntPtr, IntPtr, IntPtr, IntPtr, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StretchSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    [return: MarshalAs(UnmanagedType.I1)] 
    public static partial bool StretchSurface(IntPtr src, in Rect srcrect, IntPtr dst, IntPtr dstrect, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StretchSurface(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// Perform a stretched pixel copy from one surface to another.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or NULL to copy the entire surface..</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or NULL to fill the entire
    /// destination surface.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="BlitSurfaceScaled(IntPtr, IntPtr, IntPtr, IntPtr, ScaleMode)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StretchSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    [return: MarshalAs(UnmanagedType.I1)] 
    public static partial bool StretchSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, ScaleMode scaleMode);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a tiled blit to a destination surface, which may be of a different
    /// format.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    ///<threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a tiled blit to a destination surface, which may be of a different
    /// format.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiled(IntPtr src, in Rect srcrect, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a tiled blit to a destination surface, which may be of a different
    /// format.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiled(IntPtr src, IntPtr srcrect, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiled(SDL_Surface *src, const SDL_Rect *srcrect, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a tiled blit to a destination surface, which may be of a different
    /// format.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiled(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiledWithScale(SDL_Surface *src, const SDL_Rect *srcrect, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled and tiled blit to a destination surface, which may be of a
    /// different format.</para>
    /// <para>The pixels in <c>srcrect</c> will be scaled and repeated as many times as needed
    /// to completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiledWithScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiledWithScale(IntPtr src, IntPtr srcrect, float scale, ScaleMode scaleMode, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiledWithScale(SDL_Surface *src, const SDL_Rect *srcrect, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled and tiled blit to a destination surface, which may be of a
    /// different format.</para>
    /// <para>The pixels in <c>srcrect</c> will be scaled and repeated as many times as needed
    /// to completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiledWithScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiledWithScale(IntPtr src, in Rect srcrect, float scale, ScaleMode scaleMode, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiledWithScale(SDL_Surface *src, const SDL_Rect *srcrect, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled and tiled blit to a destination surface, which may be of a
    /// different format.</para>
    /// <para>The pixels in <c>srcrect</c> will be scaled and repeated as many times as needed
    /// to completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiledWithScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiledWithScale(IntPtr src, IntPtr srcrect, float scale, ScaleMode scaleMode, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurfaceTiledWithScale(SDL_Surface *src, const SDL_Rect *srcrect, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled and tiled blit to a destination surface, which may be of a
    /// different format.</para>
    /// <para>The pixels in <c>srcrect</c> will be scaled and repeated as many times as needed
    /// to completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceTiledWithScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurfaceTiledWithScale(IntPtr src, in Rect srcrect, float scale, ScaleMode scaleMode, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface9Grid(SDL_Surface *src, const SDL_Rect *srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled blit using the 9-grid algorithm to a destination surface,
    /// which may be of a different format.</para>
    /// <para>The pixels in the source surface are split into a 3x3 grid, using the
    /// different corner sizes for each corner, and the sides and center making up
    /// the remaining pixels. The corners are then scaled using <c>scale</c> and fit
    /// into the corners of the destination rectangle. The sides and center are
    /// then stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire surface.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled blit.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface9Grid(IntPtr src, IntPtr srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface9Grid(SDL_Surface *src, const SDL_Rect *srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled blit using the 9-grid algorithm to a destination surface,
    /// which may be of a different format.</para>
    /// <para>The pixels in the source surface are split into a 3x3 grid, using the
    /// different corner sizes for each corner, and the sides and center making up
    /// the remaining pixels. The corners are then scaled using <c>scale</c> and fit
    /// into the corners of the destination rectangle. The sides and center are
    /// then stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire surface.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled blit.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface9Grid(IntPtr src, in Rect srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, IntPtr dst, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface9Grid(SDL_Surface *src, const SDL_Rect *srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled blit using the 9-grid algorithm to a destination surface,
    /// which may be of a different format.</para>
    /// <para>The pixels in the source surface are split into a 3x3 grid, using the
    /// different corner sizes for each corner, and the sides and center making up
    /// the remaining pixels. The corners are then scaled using <c>scale</c> and fit
    /// into the corners of the destination rectangle. The sides and center are
    /// then stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire surface.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled blit.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface9Grid(IntPtr src, IntPtr srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BlitSurface9Grid(SDL_Surface *src, const SDL_Rect *srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, SDL_Surface *dst, const SDL_Rect *dstrect);</code>
    /// <summary>
    /// <para>Perform a scaled blit using the 9-grid algorithm to a destination surface,
    /// which may be of a different format.</para>
    /// <para>The pixels in the source surface are split into a 3x3 grid, using the
    /// different corner sizes for each corner, and the sides and center making up
    /// the remaining pixels. The corners are then scaled using <c>scale</c> and fit
    /// into the corners of the destination rectangle. The sides and center are
    /// then stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire surface.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled blit.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, or <c>null</c> to fill the entire surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Only one thread should be using the <c>src</c> and <c>dst</c> surfaces
    /// at any given time.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BlitSurface(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BlitSurface9Grid(IntPtr src, in Rect srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, IntPtr dst, in Rect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_MapSurfaceRGB(SDL_Surface *surface, Uint8 r, Uint8 g, Uint8 b);</code>
    /// <summary>
    /// <para>Map an RGB triple to an opaque pixel value for a surface.</para>
    /// <para>This function maps the RGB color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGB color value for
    /// the given pixel format.</para>
    /// <para>If the surface has a palette, the index of the closest matching color in
    /// the palette will be returned.</para>
    /// <para>If the surface pixel format has an alpha component it will be returned as
    /// all 1 bits (fully opaque).</para>
    /// <para>If the pixel format bpp (color depth) is less than 32-bpp then the unused
    /// upper bits of the return value can safely be ignored (e.g., with a 16-bpp
    /// format the return value can be assigned to a Uint16, and similarly a Uint8
    /// for an 8-bpp format).</para>
    /// </summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MapSurfaceRGBA"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MapSurfaceRGB"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MapSurfaceRGB(IntPtr surface, byte r, byte g, byte b);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_MapSurfaceRGBA(SDL_Surface *surface, Uint8 r, Uint8 g, Uint8 b, Uint8 a);</code>
    /// <summary>
    /// <para>Map an RGBA quadruple to a pixel value for a surface.</para>
    /// <para>This function maps the RGBA color value to the specified pixel format and
    /// returns the pixel value best approximating the given RGBA color value for
    /// the given pixel format.</para>
    /// <para>If the surface pixel format has no alpha component the alpha value will be
    /// ignored (as it will be in formats with a palette).</para>
    /// <para>If the surface has a palette, the index of the closest matching color in
    /// the palette will be returned.</para>
    /// <para>If the pixel format bpp (color depth) is less than 32-bpp then the unused
    /// upper bits of the return value can safely be ignored (e.g., with a 16-bpp
    /// format the return value can be assigned to a Uint16, and similarly a Uint8
    /// for an 8-bpp format).</para>
    /// </summary>
    /// <param name="surface">the surface to use for the pixel format and palette.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MapSurfaceRGB"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MapSurfaceRGBA"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MapSurfaceRGBA(IntPtr surface, byte r, byte g, byte b, byte a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadSurfacePixel(SDL_Surface *surface, int x, int y, Uint8 *r, Uint8 *g, Uint8 *b, Uint8 *a);</code>
    /// <summary>
    /// <para>Retrieves a single pixel from a surface.</para>
    /// <para>This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.</para>
    /// <para>Like <see cref="GetRGBA(uint, in PixelFormatDetails, IntPtr, out byte, out byte, out byte, out byte)"/>, this uses the entire 0..255 range when converting color
    /// components from pixel formats with less than 8 bits per RGB component.</para>
    /// </summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">a pointer filled in with the red channel, 0-255, or <c>null</c> to ignore
    /// this channel.</param>
    /// <param name="g">a pointer filled in with the green channel, 0-255, or <c>null</c> to
    /// ignore this channel.</param>
    /// <param name="b">a pointer filled in with the blue channel, 0-255, or <c>null</c> to
    /// ignore this channel.</param>
    /// <param name="a">a pointer filled in with the alpha channel, 0-255, or <c>null</c> to
    /// ignore this channel.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadSurfacePixel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ReadSurfacePixel(IntPtr surface, int x, int y, out byte r, out byte g, out byte b, out byte a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadSurfacePixelFloat(SDL_Surface *surface, int x, int y, float *r, float *g, float *b, float *a);</code>
    /// <summary>
    /// <para>Retrieves a single pixel from a surface.</para>
    /// <para>This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.</para>
    /// </summary>
    /// <param name="surface">the surface to read.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">a pointer filled in with the red channel, normally in the range
    /// 0-1, or <c>null</c> to ignore this channel.</param>
    /// <param name="g">a pointer filled in with the green channel, normally in the range
    /// 0-1, or <c>null</c> to ignore this channel.</param>
    /// <param name="b">a pointer filled in with the blue channel, normally in the range
    /// 0-1, or <c>null</c> to ignore this channel.</param>
    /// <param name="a">a pointer filled in with the alpha channel, normally in the range
    /// 0-1, or <c>null</c> to ignore this channel.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadSurfacePixelFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ReadSurfacePixelFloat(IntPtr surface, int x, int y, out float r, out float g, out float b, out float a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteSurfacePixel(SDL_Surface *surface, int x, int y, Uint8 r, Uint8 g, Uint8 b, Uint8 a);</code>
    /// <summary>
    /// <para>Writes a single pixel to a surface.</para>
    /// <para>This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.</para>
    /// <para>Like <see cref="MapRGBA"/>, this uses the entire 0..255 range when converting color
    /// components from pixel formats with less than 8 bits per RGB component.</para>
    /// </summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">the red channel value, 0-255.</param>
    /// <param name="g">the green channel value, 0-255.</param>
    /// <param name="b">the blue channel value, 0-255.</param>
    /// <param name="a">the alpha channel value, 0-255.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteSurfacePixel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WriteSurfacePixel(IntPtr surface, int x, int y, byte r, byte g, byte b, byte a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteSurfacePixelFloat(SDL_Surface *surface, int x, int y, float r, float g, float b, float a);</code>
    /// <summary>
    /// <para>Writes a single pixel to a surface.</para>
    /// <para>This function prioritizes correctness over speed: it is suitable for unit
    /// tests, but is not intended for use in a game engine.</para>
    /// </summary>
    /// <param name="surface">the surface to write.</param>
    /// <param name="x">the horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">the vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">the red channel value, normally in the range 0-1.</param>
    /// <param name="g">the green channel value, normally in the range 0-1.</param>
    /// <param name="b">the blue channel value, normally in the range 0-1.</param>
    /// <param name="a">the alpha channel value, normally in the range 0-1.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteSurfacePixelFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WriteSurfacePixelFloat(IntPtr surface, int x, int y, float r, float g, float b, float a);
}