using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateSurface(int width, int height, PixelFormat format);
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
    public static Surface? CreateSurface(int width, int height, PixelFormat format)
    {
        var ptr = SDL_CreateSurface(width, height, format);
        return ptr == IntPtr.Zero ? null : new Surface(ptr);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateSurfaceFrom(int width, int height, PixelFormat format, IntPtr pixels, int pitch);
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
    public static Surface? CreateSurfaceFrom(int width, int height, PixelFormat format, byte[] pixels, int pitch)
    {
        var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
        try
        {
            var ptr = SDL_CreateSurfaceFrom(width, height, format, handle.AddrOfPinnedObject(), pitch);
            return ptr == IntPtr.Zero ? null : new Surface(ptr);
        }
        finally
        {
            handle.Free();
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroySurface(IntPtr surface);
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
    public static void DestroySurface(Surface? surface)
    {
        if (surface != null) SDL_DestroySurface(surface.Handle);
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSurfaceProperties(IntPtr surface);
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
    public static uint GetSurfaceProperties(Surface surface) => SDL_GetSurfaceProperties(surface.Handle);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetSurfaceColorspace(IntPtr surface, Colorspace colorspace);
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
    public static int SetSurfaceColorspace(Surface surface, Colorspace colorspace) => SDL_SetSurfaceColorspace(surface.Handle, colorspace);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Colorspace SDL_GetSurfaceColorspace(IntPtr surface);
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
    public static Colorspace GetSurfaceColorspace(Surface? surface) => SDL_GetSurfaceColorspace(surface != null ? surface.Handle : IntPtr.Zero);


    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_LoadBMP([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
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
    public static Surface? LoadBMP(string file)
    {
        var ptr = SDL_LoadBMP(file);
        return ptr == IntPtr.Zero ? null : new Surface(ptr);
    }

}