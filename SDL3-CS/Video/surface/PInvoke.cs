using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC SDL_Surface *SDLCALL SDL_CreateSurface(int width, int height, SDL_PixelFormat format);</code>
    /// <summary>
    /// Allocate a new surface with a specific pixel format.
    /// </summary>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat"/> for the new surface's pixel format.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or null if it fails; call SDL_GetError() for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreateSurfaceFrom"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSurface(int width, int height, PixelFormat format);
    
    /// <code>extern SDL_DECLSPEC SDL_Surface *SDLCALL SDL_CreateSurfaceFrom(int width, int height, SDL_PixelFormat format, void *pixels, int pitch);</code>
    /// <summary>
    /// Allocate a new surface with a specific pixel format and existing pixel data.
    /// </summary>
    /// <para>No copy is made of the pixel data. Pixel data is not managed automatically;
    /// you must free the surface before you free the pixel data.</para>
    /// <para>Pitch is the offset in bytes from one row of pixels to the next, e.g.
    /// width*4 for <see cref="PixelFormat.RGBA8888"/>.</para>
    /// <para>You may pass <c>NULL</c> for pixels and 0 for pitch to create a surface that you
    /// will fill in with valid values later.</para>
    /// <param name="width">the width of the surface.</param>
    /// <param name="height">the height of the surface.</param>
    /// <param name="format">the <see cref="PixelFormat"/> for the new surface's pixel format.</param>
    /// <param name="pixels">an array of existing pixel data.</param>
    /// <param name="pitch">the number of bytes between each row, including padding.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or <c>NULL</c> if it fails;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSurfaceFrom"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSurfaceFrom(int width, int height, PixelFormat format, IntPtr pixels, int pitch);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroySurface(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Free a surface.</para>
    /// <para>It is safe to pass NULL to this function.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> to free.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreateStackSurface"/>
    /// <seealso cref="CreateSurface"/>
    /// <seealso cref="CreateSurfaceFrom"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroySurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySurface(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetSurfaceProperties(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Get the properties associated with a surface.</para>
    /// <para>The following properties are understood by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="PropSurfaceColorspaceNumber"/>: an <see cref="Colorspace"/> value describing
    /// the surface colorspace, defaults to <see cref="Colorspace.SRGBLinear"/> for
    /// floating point formats, <see cref="Colorspace.HDR10"/> for 10-bit formats,
    /// <see cref="Colorspace.SRGB"/> for other RGB surfaces and <see cref="Colorspace.BT709Full"/>
    /// for YUV surfaces.</item>
    /// <item><see cref="PropSurfaceSDRWhitePointFloat"/>: for HDR10 and floating point
    /// surfaces, this defines the value of 100% diffuse white, with higher
    /// values being displayed in the High Dynamic Range headroom. This defaults
    /// to 203 for HDR10 surfaces and 1.0 for floating point surfaces.</item>
    /// <item><see cref="PropSurfaceHDRHeadroomFloat"/>: for HDR10 and floating point
    /// surfaces, this defines the maximum dynamic range used by the content, in
    /// terms of the SDR white point. This defaults to 0.0, which disables tone
    /// mapping.</item>
    /// <item><see cref="PropSurfaceTonemapOperatorString"/>: the tone mapping operator
    /// used when compressing from a surface with high dynamic range to another
    /// with lower dynamic range. Currently this supports "chrome", which uses
    /// the same tone mapping that Chrome uses for HDR content, the form "*=N",
    /// where N is a floating point scale factor applied in linear space, and
    /// "none", which disables tone mapping. This defaults to "chrome".</item>
    /// </list>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns>a valid property ID on success or <c>0</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetSurfaceProperties(IntPtr surface);

    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetSurfaceColorspace(SDL_Surface *surface, SDL_Colorspace colorspace);</code>
    /// <summary>
    /// Set the colorspace used by a surface.
    /// <para>Setting the colorspace doesn't change the pixels, only how they are interpreted in color operations.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="colorspace">an <see cref="Colorspace"/> value describing the surface colorspace.</param>
    /// <returns>0 on success or a negative error code on failure; call SDL_GetError() for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceColorspace"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SetSurfaceColorspace(IntPtr surface, Colorspace colorspace);
    
    
    /// <summary>
    /// <para>Get the colorspace used by a surface.</para>
    /// <para>The colorspace defaults to <see cref="Colorspace.SRGBLinear"/> for floating point
    /// formats, <see cref="Colorspace.HDR10"/> for 10-bit formats, <see cref="Colorspace.SRGB"/> for
    /// other RGB surfaces and <see cref="Colorspace.BT709Full"/> for YUV textures.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns>the colorspace used by the surface, or <see cref="Colorspace.Unknown"/> if
    /// the surface is <c>NULL</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfaceColorspace"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Colorspace GetSurfaceColorspace(IntPtr surface);
    
    
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
    /// <returns>a new <see cref="Palette"/> structure on success or <c>NULL</c> on failure (e.g. if
    /// the surface didn't have an index format); call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetPaletteColors"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSurfacePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSurfacePalette(IntPtr surface);
    
    
    /// <summary>
    /// <para>Set the palette used by a surface.</para>
    /// <remarks>A single palette can be shared with many surfaces.</remarks>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="palette">the <see cref="Palette"/> structure to use.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CreatePalette"/>
    /// <seealso cref="GetSurfacePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfacePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfacePalette(IntPtr surface, IntPtr palette);
    
        
    /// <summary>
    /// Get the palette used by a surface.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns>a pointer to the palette used by the surface, or <c>NULL</c> if there is
    /// no palette used.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfacePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfacePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetSurfacePalette(IntPtr surface);
    
    /// <summary>
    /// <para>Set up a surface for directly accessing the pixels.</para>
    /// <para>Between calls to <see cref="LockSurface"/> / <see cref="UnlockSurface"/>, you can write to
    /// and read from <c>surface->pixels</c>, using the pixel format stored in
    /// <c>surface->format</c>. Once you are done accessing the surface, you should use
    /// <see cref="UnlockSurface"/> to release it.</para>
    /// <para>Not all surfaces require locking. If <see cref="MustLock"/> evaluates to
    /// <c>0</c>, then you can read and write to the surface at any time, and the pixel
    /// format of the surface will not change.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be locked.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="MustLock"/>
    /// <seealso cref="UnlockSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LockSurface(IntPtr surface);
    
    
    /// <summary>
    /// Release a surface after directly accessing the pixels.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to be unlocked.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LockSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockSurface(IntPtr surface);
    
    
    /// <summary>
    /// Load a BMP image from a seekable SDL data stream.
    /// <remarks>The new surface should be freed with <see cref="DestroySurface"/>. Not doing so
    /// will result in a memory leak.</remarks>
    /// </summary>
    /// <param name="src">the data stream for the surface.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>src</c> before returning,
    /// even in the case of an error.</param>
    /// <returns>a pointer to a new <see cref="Surface"/> structure or <c>null</c> if there was an
    /// error; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DestroySurface"/>
    /// <seealso cref="LoadBMP"/>
    /// <seealso cref="SaveBMP_IO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadBMP_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadBMPIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface *SDLCALL SDL_LoadBMP(const char *file);</code>
    /// <summary>
    /// Load a BMP image from a file.
    /// The new surface should be freed with <see cref="DestroySurface"/>. Not doing so
    /// will result in a memory leak.
    /// </summary>
    /// <param name="file">the BMP file to load.</param>
    /// <returns>a pointer to a new <see cref="Surface"/> structure or null if there was an error; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="DestroySurface"/>
    /// <seealso cref="LoadBMP_IO"/>
    /// <seealso cref="SaveBMP"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadBMP"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadBMP([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    

    /// <summary>
    /// <para>Save a surface to a seekable SDL data stream in BMP format.</para>
    /// <remarks>Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the
    /// BMP directly. Other RGB formats with 8-bit or higher get converted to a
    /// 24-bit surface or, if they have an alpha mask or a colorkey, to a 32-bit
    /// surface before they are saved. YUV and paletted 1-bit and 4-bit formats are
    /// not supported.</remarks>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure containing the image to be saved.</param>
    /// <param name="dst">a data stream to save to.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>dst</c> before returning,
    /// even in the case of an error.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LoadBMP_IO"/>
    /// <seealso cref="SaveBMP"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SaveBMP_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SaveBMPIO(IntPtr surface, IntPtr dst, [MarshalAs(UnmanagedType.I1)] bool closeio);
    

    /// <summary>
    /// Save a surface to a file.
    /// <remarks>Surfaces with a 24-bit, 32-bit and paletted 8-bit format get saved in the
    /// BMP directly. Other RGB formats with 8-bit or higher get converted to a
    /// 24-bit surface or, if they have an alpha mask or a colorkey, to a 32-bit
    /// surface before they are saved. YUV and paletted 1-bit and 4-bit formats are
    /// not supported.</remarks>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure containing the image to be saved.</param>
    /// <param name="file">a file to save to.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LoadBMP"/>
    /// <seealso cref="SaveBMPIO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SaveBMP"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SaveBMP(IntPtr surface, [MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <summary>
    /// Set the RLE acceleration hint for a surface.
    /// </summary>
    /// <remarks>If RLE is enabled, color key and alpha blending blits are much faster, but
    /// the surface must be locked before directly accessing the pixels.</remarks>
    /// <param name="surface">the <see cref="Surface"/> structure to optimize.</param>
    /// <param name="enabled"><c>true</c> to enable RLE acceleration, <c>false</c> to disable
    /// it.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="BlitSurface"/>
    /// <seealso cref="LockSurface"/>
    /// <seealso cref="UnlockSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceRLE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceRLE(IntPtr surface, [MarshalAs(UnmanagedType.I1)] bool enabled);
    

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_SurfaceHasRLE(IntPtr surface);
    /// <summary>
    /// Returns whether the surface is RLE enabled.
    /// </summary>
    /// <remarks>It is safe to pass a <c>null</c> <c>surface</c> here; it will return <c>false</c>.</remarks>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <returns><c>true</c> if the surface is RLE enabled, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfaceRLE"/>
    public static bool SurfaceHasRLE(IntPtr surface) => SDL_SurfaceHasRLE(surface);


    /// <summary>
    /// <para>Set the color key (transparent pixel) in a surface.</para>
    /// <para>The color key defines a pixel value that will be treated as transparent in
    /// a blit. For example, one can use this to specify that cyan pixels should be
    /// considered transparent, and therefore not rendered.</para>
    /// </summary>
    /// <remarks>
    /// It is a pixel of the format used by the surface, as generated by
    /// <see cref="MapRGB"/>.
    /// </remarks>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="enabled"><c>true</c> to enable color key, <c>false</c> to disable color
    /// key.</param>
    /// <param name="key">the transparent pixel.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceColorKey"/>
    /// <seealso cref="SetSurfaceRLE"/>
    /// <seealso cref="SurfaceHasColorKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceColorKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceColorKey(IntPtr surface, [MarshalAs(UnmanagedType.I1)] bool enabled, uint key);
    
    
    /// <summary>
    /// Returns whether the surface has a color key.
    /// </summary>
    /// <remarks>
    /// It is safe to pass a <c>null</c> <c>surface</c> here; it will return <c>false</c>.
    /// </remarks>
    /// <param name="surface">the <c>Surface</c> structure to query.</param>
    /// <returns><c>true</c> if the surface has a color key, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfaceColorKey"/>
    /// <seealso cref="GetSurfaceColorKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SurfaceHasColorKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SurfaceHasColorKey(IntPtr surface);
    

    /// <summary>
    /// <para>Get the color key (transparent pixel) for a surface.</para>
    /// <para>The color key is a pixel of the format used by the surface, as generated by
    /// <see cref="MapRGB"/>.</para>
    /// <remarks>
    /// If the surface doesn't have color key enabled this function returns -1.
    /// </remarks>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="key">a pointer filled in with the transparent pixel.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfaceColorKey"/>
    /// <seealso cref="SurfaceHasColorKey"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceColorKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceColorKey(IntPtr surface, out uint key);
    

    /// <summary>
    /// <para>Set an additional color value multiplied into blit operations.</para>
    /// <para>When this surface is blitted, during the blit operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:</para>
    /// <para><code>srcC = srcC * (color / 255)</code></para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="r">the red color value multiplied into blit operations.</param>
    /// <param name="g">the green color value multiplied into blit operations.</param>
    /// <param name="b">the blue color value multiplied into blit operations.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceColorMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);
    
    
    /// <summary>
    /// Get the additional color value multiplied into blit operations.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceAlphaMod"/>
    /// <seealso cref="SetSurfaceColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceColorMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);
    

    /// <summary>
    /// <para>Set an additional alpha value used in blit operations.</para>
    /// <para>When this surface is blitted, during the blit operation the source alpha
    /// value is modulated by this alpha value according to the following formula:</para>
    /// <code>srcA = srcA * (alpha / 255)</code>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="alpha">the alpha value multiplied into blit operations.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceAlphaMod"/>
    /// <seealso cref="SetSurfaceColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceAlphaMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceAlphaMod(IntPtr surface, byte alpha);
    
    
    /// <summary>
    /// Get the additional alpha value used in blit operations.
    /// </summary>
    /// <param name="surface">the <seealso cref="Surface"/> structure to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceColorMod"/>
    /// <seealso cref="SetSurfaceAlphaMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceAlphaMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceAlphaMod(IntPtr surface, out byte alpha);
    
    
    /// <summary>
    /// Set the blend mode used for blit operations.
    /// </summary>
    /// <remarks>To copy a surface to another surface (or texture) without blending with the
    /// existing data, the blendmode of the SOURCE surface should be set to
    /// <see cref="BlendMode.None"/>.</remarks>
    /// <param name="surface">the <see cref="Surface"/> structure to update.</param>
    /// <param name="blendMode">the <see cref="BlendMode"/> to use for blit blending.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceBlendMode(IntPtr surface, BlendMode blendMode);
    
    
    /// <summary>
    /// Get the blend mode used for blit operations.
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure to query.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode"/>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfaceBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceBlendMode(IntPtr surface, out BlendMode blendMode);


    /// <summary>
    /// <para>Set the clipping rectangle for a surface.</para>
    /// <para>When <c></c> is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.</para>
    /// </summary>
    /// <remarks>Note that blits are automatically clipped to the edges of the source and
    /// destination surfaces.</remarks>
    /// <param name="surface">the <see cref="Surface"/> structure to be clipped.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the clipping rectangle, or
    /// <c>null</c> to disable clipping.</param>
    /// <returns><c>true</c> if the rectangle intersects the surface, otherwise
    /// <c>false</c> and blits will be completely clipped.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetSurfaceClipRect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetSurfaceClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool SetSurfaceClipRect(IntPtr surface, Rect rect);
    

    /// <summary>
    /// <para>Get the clipping rectangle for a surface.</para>
    /// <para>When <c>surface</c> is the destination of a blit, only the area within the clip
    /// rectangle is drawn into.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure representing the surface to be
    /// clipped.</param>
    /// <param name="rect">an <seealso cref="Rect"/> structure filled in with the clipping rectangle for
    /// the surface.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetSurfaceClipRect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSurfaceClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceClipRect(IntPtr surface, out Rect rect);


    /// <summary>
    /// Flip a surface vertically or horizontally.
    /// </summary>
    /// <param name="surface">the surface to flip.</param>
    /// <param name="flip">the direction to flip.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlipSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FlipSurface(IntPtr surface, FlipMode flip);
    

    /// <summary>
    /// Creates a new surface identical to the existing surface.
    /// </summary>
    /// <remarks>The returned surface should be freed with <see cref="DestroySurface"/>.</remarks>
    /// <param name="surface">the surface to duplicate.</param>
    /// <returns>a copy of the surface, or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DuplicateSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr DuplicateSurface(IntPtr surface);

    
    /// <summary>
    /// <para>Copy an existing surface to a new surface of the specified format.</para>
    /// <para>This function is used to optimize images for faster *repeat* blitting. This
    /// is accomplished by converting the original and storing the result as a new
    /// surface. The new, optimized surface can then be used as the source for
    /// future blits, making them faster.</para>
    /// </summary>
    /// <remarks>If you are converting to an indexed surface and want to map colors to a
    /// palette, you can use <see cref="ConvertSurfaceAndColorspace"/> instead.</remarks>
    /// <param name="surface">the existing <see cref="Surface"/> structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or NULL if it fails;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ConvertSurfaceAndColorspace"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ConvertSurface(IntPtr surface, PixelFormat format);
    

    /// <summary>
    /// Copy an existing surface to a new surface of the specified format and
    /// colorspace.
    /// </summary>
    /// <para>This function converts an existing surface to a new format and colorspace
    /// and returns the new surface. This will perform any pixel format and
    /// colorspace conversion needed.</para>
    /// <param name="surface">the existing <see cref="Surface"/> structure to convert.</param>
    /// <param name="format">the new pixel format.</param>
    /// <param name="palette">an optional palette to use for indexed formats, may be <c>null</c>.</param>
    /// <param name="colorspace">the new colorspace.</param>
    /// <param name="props">an SDL_PropertiesID with additional color properties, or 0.</param>
    /// <returns>the new <see cref="Surface"/> structure that is created or <c>null</c> if it fails;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ConvertSurface"/>
    /// <seealso cref="ConvertSurface"/>
    /// <seealso cref="DestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertSurfaceAndColorspace"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ConvertSurfaceAndColorspace(IntPtr surface, PixelFormat format, IntPtr palette, Colorspace colorspace, uint props);
    
        

    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ConvertPixels(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, out IntPtr dst, int dstPitch);
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
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ConvertPixelsAndColorspace"/>
    public static int ConvertPixels(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, out IntPtr dst, int dstPitch) =>
        SDL_ConvertPixels(width, height, srcFormat, src, srcPitch, dstFormat, out dst, dstPitch);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, IntPtr src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, out IntPtr dst, int dstPitch);
    /// <summary>
    /// Copy a block of pixels of one format and colorspace to another format and
    /// colorspace.
    /// </summary>
    /// <param name="width">the width of the block to copy, in pixels.</param>
    /// <param name="height">the height of the block to copy, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="srcColorspace">an <see cref="ColorSpace"/> value describing the colorspace of
    /// the <c>src</c> pixels.</param>
    /// <param name="srcProperties">an SDL_PropertiesID with additional source color
    /// properties, or 0.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dstColorspace">an <see cref="ColorSpace"/> value describing the colorspace of
    /// the <c>dst</c> pixels.</param>
    /// <param name="dstProperties">an SDL_PropertiesID with additional destination color
    /// properties, or 0.</param>
    /// <param name="dst">a pointer to be filled in with new pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <see cref="ConvertPixels"/>
    public static int ConvertPixelsAndColorspace(int width, int height, PixelFormat srcFormat, Colorspace srcColorspace, uint srcProperties, IntPtr src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, uint dstProperties, out IntPtr dst, int dstPitch) =>
        SDL_ConvertPixelsAndColorspace(width, height, srcFormat, srcColorspace, srcProperties, src, srcPitch, dstFormat, dstColorspace, dstProperties, out dst, dstPitch);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PremultiplyAlpha(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, IntPtr dst, int dstPitch, [MarshalAs(UnmanagedType.I1)] bool linear);
    /// <summary>
    /// Premultiply the alpha on a block of pixels.
    /// </summary>
    /// <remarks>This is safe to use with src == dst, but not for other overlapping areas.</remarks>
    /// <param name="width">the width of the block to convert, in pixels.</param>
    /// <param name="height">the height of the block to convert, in pixels.</param>
    /// <param name="srcFormat">an <see cref="PixelFormat"/> value of the <c>src</c> pixels format.</param>
    /// <param name="src">a pointer to the source pixels.</param>
    /// <param name="srcPitch">the pitch of the source pixels, in bytes.</param>
    /// <param name="dstFormat">an <see cref="PixelFormat"/> value of the <c>dst</c> pixels format.</param>
    /// <param name="dst">a pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="dstPitch">the pitch of the destination pixels, in bytes.</param>
    /// <param name="linear"><c>true</c> to convert from sRGB to linear space for the alpha
    /// multiplication, <c>false</c> to do multiplication in sRGB space.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int PremultiplyAlpha(int width, int height, PixelFormat srcFormat, IntPtr src, int srcPitch, PixelFormat dstFormat, IntPtr dst, int dstPitch, bool linear) =>
        SDL_PremultiplyAlpha(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch, linear);

    
    /// <summary>
    /// Premultiply the alpha in a surface.
    /// </summary>
    /// <remarks>This is safe to use with src == dst, but not for other overlapping areas.</remarks>
    /// <param name="surface">the surface to modify.</param>
    /// <param name="linear"><c>tree</c> to convert from sRGB to linear space for the alpha
    /// multiplication, <c>false</c> to do multiplication in sRGB space.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PremultiplySurfaceAlpha"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int PremultiplySurfaceAlpha(IntPtr surface, [MarshalAs(UnmanagedType.I1)] bool linear);
    
    
    /// <summary>
    /// <para>Clear a surface with a specific color, with floating point precision.</para>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// </summary>
    /// <remarks>If the surface is YUV, the color is assumed to be in the sRGB colorspace,
    /// otherwise the color is assumed to be in the colorspace of the suface.</remarks>
    /// <param name="surface">the <see cref="Surface"/> to clear.</param>
    /// <param name="r">the red component of the pixel, normally in the range 0-1.</param>
    /// <param name="g">the green component of the pixel, normally in the range 0-1.</param>
    /// <param name="b">the blue component of the pixel, normally in the range 0-1.</param>
    /// <param name="a">the alpha component of the pixel, normally in the range 0-1.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ClearSurface(IntPtr surface, float r, float g, float b, float a);
    

    /// <summary>
    /// <para>Perform a fast fill of a rectangle with a specific color.</para>
    /// <para><c>color</c> should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRGB"/> or <see cref="MapRGBA"/>. If the color value contains an
    /// alpha component then the destination is simply filled with that alpha
    /// information, no blending takes place.</para>
    /// </summary>
    /// <remarks>If there is a clip rectangle set on the destination (set via
    /// <see cref="SetSurfaceClipRect"/>), then this function will fill based on the
    /// intersection of the clip rectangle and <c>rect</c>.</remarks>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the rectangle to fill, or
    /// <c>null</c> to fill the entire surface.</param>
    /// <param name="color">the color to fill with.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="FillSurfaceRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FillSurfaceRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FillSurfaceRect(IntPtr dst, Rect rect, uint color);
    

    /// <summary>
    /// <para>Perform a fast fill of a set of rectangles with a specific color.</para>
    /// <para><c>color</c> should be a pixel of the format used by the surface, and can be
    /// generated by <see cref="MapRGB"/> or <see cref="MapRGBA"/>. If the color value contains an
    /// alpha component then the destination is simply filled with that alpha
    /// information, no blending takes place.</para>
    /// </summary>
    /// <remarks>If there is a clip rectangle set on the destination (set via
    /// <see cref="SetSurfaceClipRect"/>), then this function will fill based on the
    /// intersection of the clip rectangle and <c>rect</c>.</remarks>
    /// <param name="dst">the <see cref="Surface"/> structure that is the drawing target.</param>
    /// <param name="rects">an array of <see cref="Rect"/> representing the rectangles to fill.</param>
    /// <param name="count">the number of rectangles in the array.</param>
    /// <param name="color">the color to fill with.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="FillSurfaceRect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FillSurfaceRects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FillSurfaceRects(IntPtr dst, IntPtr rects, int count, uint color);
    
    
    
    /// <summary>
    /// <para>Performs a fast blit from the source surface to the destination surface.</para>
    /// <para>This assumes that the source and destination rectangles are the same size.
    /// If either <c>srcrect</c> or <c>dstrect</c> are <c>null</c>, the entire surface (<c>src</c> or
    /// <c>dst</c>) is copied. The final blit rectangles are saved in <c>srcrect</c> and
    /// <c>dstrect</c> after all clipping is performed.</para>
    /// <para>The blit function should not be called on a locked surface.</para>
    /// <para>The blit semantics for surfaces with and without blending and colorkey are
    /// defined as follows:</para>
    /// <list type="bullet">
    /// <item>RGBA->RGB:<code>
    /// Source surface blend mode set to BlendMode.Blend:
    ///   alpha-blend (using the source alpha-channel and per-surface alpha)
    ///   SDL_SRCCOLORKEY ignored.
    /// Source surface blend mode set to BlendMode.None:
    ///   copy RGB.
    ///   if SDL_SRCCOLORKEY set, only copy the pixels matching the
    ///   RGB values of the source color key, ignoring alpha in the
    ///   comparison.</code></item>
    /// <item>RGB->RGBA: <code>Source surface blend mode set to BlendMode.Blend:
    ///   alpha-blend (using the source per-surface alpha)
    /// Source surface blend mode set to BlendMode.None:
    ///   copy RGB, set destination alpha to source per-surface alpha value.
    /// both:
    ///   if SDL_SRCCOLORKEY set, only copy the pixels matching the
    ///   source color key.</code></item>
    /// <item>RGBA->RGBA: <code>Source surface blend mode set to BlendMode.Blend:
    ///   alpha-blend (using the source alpha-channel and per-surface alpha)
    ///   SDL_SRCCOLORKEY ignored.
    /// Source surface blend mode set to BlendMode.None:
    ///   copy all of RGBA to the destination.
    ///   if SDL_SRCCOLORKEY set, only copy the pixels matching the
    ///   RGB values of the source color key, ignoring alpha in the
    ///   comparison.</code></item>
    /// <item>RGB->RGB: <code>Source surface blend mode set to BlendMode.Blend:
    ///   alpha-blend (using the source per-surface alpha)
    /// Source surface blend mode set to BlendMode.None:
    ///   copy RGB.
    /// both:
    ///   if SDL_SRCCOLORKEY set, only copy the pixels matching the
    ///   source color key.</code></item>
    /// </list>
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the x and y position in
    /// the destination surface. On input the width and height are
    /// ignored (taken from srcrect), and on output this is filled
    /// in with the actual rectangle used after clipping.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>The same destination surface should not be used from two
    /// threads at once. It is safe to use the same source surface
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="BlitSurfaceScaled"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int BlitSurface(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect);
    
    
    /// <summary>
    /// Perform low-level surface blitting only.
    /// </summary>
    /// <remarks>This is a semi-private blit function and it performs low-level surface
    /// blitting, assuming the input rectangles have already been clipped.</remarks>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied, or <c>null</c> to copy the entire surface.</param>
    /// <param name="dst"><see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>The same destination surface should not be used from two
    /// threads at once. It is safe to use the same source surface
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="BlitSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceUnchecked"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int BlitSurfaceUnchecked(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect);
    
    
    
    /// <summary>
    /// Perform stretch blit between two surfaces of the same format.
    /// </summary>
    /// <remarks>Using <see cref="ScaleMode.Nearest"/>: fast, low quality. Using <see cref="ScaleMode.Linear"/>:
    /// bilinear scaling, slower, better quality, only 32BPP.</remarks>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface.</param>
    /// <param name="scaleMode">scale algorithm to be used.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="BlitSurfaceScaled"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SoftStretch"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SoftStretch(IntPtr src, Rect srcrect, IntPtr dst, Rect dstrect, ScaleMode scaleMode);
    

    
    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different
    /// format.
    /// </summary>
    /// <param name="src">the <see cref="Surface"/> structure to be copied from.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be
    /// copied.</param>
    /// <param name="dst">the <see cref="Surface"/> structure that is the blit target.</param>
    /// <param name="dstrect">the <see cref="Rect"/> structure representing the target rectangle in
    /// the destination surface, filled with the actual rectangle
    /// used after clipping.</param>
    /// <param name="scaleMode">the <see cref="ScaleMode"/> to be used.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>The same destination surface should not be used from two
    /// threads at once. It is safe to use the same source surface
    /// from multiple threads.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="BlitSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int BlitSurfaceScaled(IntPtr src, Rect srcrect, IntPtr dst, Rect dstrect, ScaleMode scaleMode);
    
    
    /// <summary>
    /// Perform low-level surface scaled blitting only.
    /// </summary>
    /// <remarks>This is a semi-private function and it performs low-level surface blitting,
    /// assuming the input rectangles have already been clipped.</remarks>
    /// <param name="src"></param>
    /// <param name="srcrect"></param>
    /// <param name="dst"></param>
    /// <param name="dstrect"></param>
    /// <param name="scaleMode"></param>
    /// <returns></returns>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitSurfaceUncheckedScaled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int BlitSurfaceUncheckedScaled(IntPtr src, in Rect srcrect, IntPtr dst, in Rect dstrect, ScaleMode scaleMode);
    
}