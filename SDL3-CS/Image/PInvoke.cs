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

public partial class Image
{
    /// <code>extern SDL_DECLSPEC int SDLCALL IMG_Version(void);</code>
    /// <summary>
    /// This function gets the version of the dynamically linked SDL_image library.
    /// </summary>
    /// <returns>SDL_image version.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_Version"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Version();
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadTyped_IO(SDL_IOStream *src, bool closeio, const char *type);</code>
    /// <summary>
    /// <para>Load an image from an SDL data source into a software surface.</para>
    /// <para>An <see cref="SDL.Surface"/> is a buffer of pixels in memory accessible by the CPU. Use
    /// this if you plan to hand the data to something else or manipulate it
    /// further in code.</para>
    /// <para>There are no guarantees about what format the new <see cref="SDL.Surface"/> data will be;
    /// in many cases, SDL_image will attempt to supply a surface that exactly
    /// matches the provided image, but in others it might have to convert (either
    /// because the image is in a format that SDL doesn't directly support or
    /// because it's compressed data that could reasonably uncompress to various
    /// formats and SDL_image had to pick one). You can inspect an <see cref="SDL.Surface"/> for
    /// its specifics, and use <see cref="SDL.ConvertSurface"/> to then migrate to any supported
    /// format.</para>
    /// <para>If the image format supports a transparent pixel, SDL will set the colorkey
    /// for the surface. You can enable RLE acceleration on the surface afterwards
    /// by calling: SDL.SetSurfaceColorKey(image, SDL_RLEACCEL,
    /// image.Format.Colorkey);</para>
    /// <para>If <c>closeio</c> is true, <c>src</c> will be closed before returning, whether this
    /// function succeeds or not. SDL_image reads everything it needs from <c>src</c>
    /// during this call in any case.</para>
    /// <para>Even though this function accepts a file type, SDL_image may still try
    /// other decoders that are capable of detecting file type from the contents of
    /// the image data, but may rely on the caller-provided type string for formats
    /// that it cannot autodetect. If <c>type</c> is <c>null</c>, SDL_image will rely solely on
    /// its ability to guess the format.</para>
    /// <para>There is a separate function to read files from disk without having to deal
    /// with SDL_IOStream: <c>Load("filename.jpg")</c> will call this function and
    /// manage those details for you, determining the file type from the filename's
    /// extension.</para>
    /// <para>There is also <see cref="LoadIO"/>, which is equivalent to this function except
    /// that it will rely on SDL_image to determine what type of data it is
    /// loading, much like passing a <c>null</c> for type.</para>
    /// <para>If you are using SDL's 2D rendering API, there is an equivalent call to
    /// load images directly into an SDL_Texture for use by the GPU without using a
    /// software surface: call <see cref="LoadTextureTypedIO"/> instead.</para>
    /// <para>When done with the returned surface, the app should dispose of it with a
    /// call to <see cref="SDL.DestroySurface"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <param name="type">a filename extension that represent this data ("BMP", "GIF",
    /// "PNG", etc).</param>
    /// <returns>a new SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="Load"/>
    /// <seealso cref="LoadIO"/>
    /// <seealso cref="SDL.DestroySurface"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadTyped_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadTypedIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio, [MarshalAs(UnmanagedType.LPUTF8Str)] string type);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_Load(const char *file);</code>
    /// <summary>
    /// <para>Load an image from a filesystem path into a software surface.</para>
    /// <para>An <see cref="SDL.Surface"/> is a buffer of pixels in memory accessible by the CPU. Use
    /// this if you plan to hand the data to something else or manipulate it
    /// further in code.</para>
    /// <para>There are no guarantees about what format the new <see cref="SDL.Surface"/> data will be;
    /// in many cases, SDL_image will attempt to supply a surface that exactly
    /// matches the provided image, but in others it might have to convert (either
    /// because the image is in a format that SDL doesn't directly support or
    /// because it's compressed data that could reasonably uncompress to various
    /// formats and SDL_image had to pick one). You can inspect an <see cref="SDL.Surface"/> for
    /// its specifics, and use <see cref="SDL.ConvertSurface"/> to then migrate to any supported
    /// format.</para>
    /// <para>If the image format supports a transparent pixel, SDL will set the colorkey
    /// for the surface. You can enable RLE acceleration on the surface afterwards
    /// by calling: SDL.SetSurfaceColorKey(image, SDL_RLEACCEL,
    /// image.Format.Colorkey);</para>
    /// <para>There is a separate function to read files from an SDL_IOStream, if you
    /// need an i/o abstraction to provide data from anywhere instead of a simple
    /// filesystem read; that function is <see cref="LoadIO"/>.</para>
    /// <para>If you are using SDL's 2D rendering API, there is an equivalent call to
    /// load images directly into an SDL_Texture for use by the GPU without using a
    /// software surface: call <see cref="LoadTexture"/> instead.</para>
    /// <para>When done with the returned surface, the app should dispose of it with a
    /// call to
    /// [SDL_DestroySurface](https://wiki.libsdl.org/SDL3/SDL_DestroySurface)
    /// ().</para>
    /// </summary>
    /// <param name="file">a path on the filesystem to load an image from.</param>
    /// <returns>a new SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadTypedIO"/>
    /// <seealso cref="LoadIO"/>
    /// <seealso cref="SDL.DestroySurface"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_Load"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Load([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_Load_IO(SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load an image from an SDL data source into a software surface.</para>
    /// <para>An <see cref="SDL.Surface"/> is a buffer of pixels in memory accessible by the CPU. Use
    /// this if you plan to hand the data to something else or manipulate it
    /// further in code.</para>
    /// <para>There are no guarantees about what format the new <see cref="SDL.Surface"/> data will be;
    /// in many cases, SDL_image will attempt to supply a surface that exactly
    /// matches the provided image, but in others it might have to convert (either
    /// because the image is in a format that SDL doesn't directly support or
    /// because it's compressed data that could reasonably uncompress to various
    /// formats and SDL_image had to pick one). You can inspect an <see cref="SDL.Surface"/> for
    /// its specifics, and use <see cref="SDL.ConvertSurface"/> to then migrate to any supported
    /// format.</para>
    /// <para>If the image format supports a transparent pixel, SDL will set the colorkey
    /// for the surface. You can enable RLE acceleration on the surface afterwards
    /// by calling: SDL.SetSurfaceColorKey(image, SDL_RLEACCEL,
    /// image.Format.Colorkey);</para>
    /// <para>If <c>closeio</c> is true, <c>src</c> will be closed before returning, whether this
    /// function succeeds or not. SDL_image reads everything it needs from <c>src</c>
    /// during this call in any case.</para>
    /// <para>There is a separate function to read files from disk without having to deal
    /// with SDL_IOStream: <c>Image.Load("filename.jpg")</c> will call this function and
    /// manage those details for you, determining the file type from the filename's
    /// extension.</para>
    /// <para>There is also <see cref="LoadTypedIO"/>, which is equivalent to this function
    /// except a file extension (like "BMP", "JPG", etc) can be specified, in case
    /// SDL_image cannot autodetect the file format.</para>
    /// <para>If you are using SDL's 2D rendering API, there is an equivalent call to
    /// load images directly into an SDL_Texture for use by the GPU without using a
    /// software surface: call <see cref="LoadTextureIO"/> instead.</para>
    /// <para>When done with the returned surface, the app should dispose of it with a
    /// call to <see cref="SDL.DestroySurface"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <returns>a new SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="Load"/>
    /// <seealso cref="LoadTypedIO"/>
    /// <seealso cref="SDL.DestroySurface"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_Load_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL IMG_LoadTexture(SDL_Renderer *renderer, const char *file);</code>
    /// <summary>
    /// <para>Load an image from a filesystem path into a GPU texture.</para>
    /// <para>An SDL_Texture represents an image in GPU memory, usable by SDL's 2D Render
    /// API. This can be significantly more efficient than using a CPU-bound
    /// <see cref="SDL.Surface"/>  if you don't need to manipulate the image directly after
    /// loading it.</para>
    /// <para>If the loaded image has transparency or a colorkey, a texture with an alpha
    /// channel will be created. Otherwise, SDL_image will attempt to create an
    /// SDL_Texture in the most format that most reasonably represents the image
    /// data (but in many cases, this will just end up being 32-bit RGB or 32-bit
    /// RGBA).</para>
    /// <para>There is a separate function to read files from an SDL_IOStream, if you
    /// need an i/o abstraction to provide data from anywhere instead of a simple
    /// filesystem read; that function is <see cref="LoadTextureIO"/>.</para>
    /// <para>If you would rather decode an image to an <see cref="SDL.Surface"/>  (a buffer of pixels
    /// in CPU memory), call <see cref="Load"/> instead.</para>
    /// <para>When done with the returned texture, the app should dispose of it with a
    /// call to <see cref="SDL.DestroyTexture"/>.</para>
    /// </summary>
    /// <param name="renderer">the SDL_Renderer to use to create the GPU texture.</param>
    /// <param name="file">a path on the filesystem to load an image from.</param>
    /// <returns>a new texture, or <c>null</c>on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadTextureTypedIO"/>
    /// <seealso cref="LoadTextureIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadTexture(IntPtr renderer, [MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL IMG_LoadTexture_IO(SDL_Renderer *renderer, SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load an image from an SDL data source into a GPU texture.</para>
    /// <para>An SDL_Texture represents an image in GPU memory, usable by SDL's 2D Render
    /// API. This can be significantly more efficient than using a CPU-bound
    /// <see cref="SDL.Surface"/> if you don't need to manipulate the image directly after
    /// loading it.</para>
    /// <para>If the loaded image has transparency or a colorkey, a texture with an alpha
    /// channel will be created. Otherwise, SDL_image will attempt to create an
    /// SDL_Texture in the most format that most reasonably represents the image
    /// data (but in many cases, this will just end up being 32-bit RGB or 32-bit
    /// RGBA).</para>
    /// <para>If <c>closeio</c> is true, <c>src</c> will be closed before returning, whether this
    /// function succeeds or not. SDL_image reads everything it needs from <c>src</c>
    /// during this call in any case.</para>
    /// <para>There is a separate function to read files from disk without having to deal
    /// with SDL_IOStream: <c>LoadTexture(renderer, "filename.jpg")</c> will call
    /// this function and manage those details for you, determining the file type
    /// from the filename's extension.</para>
    /// <para>There is also <see cref="LoadTextureTypedIO"/>, which is equivalent to this
    /// function except a file extension (like "BMP", "JPG", etc) can be specified,
    /// in case SDL_image cannot autodetect the file format.</para>
    /// <para>If you would rather decode an image to an SDL_Surface (a buffer of pixels
    /// in CPU memory), call <see cref="Load"/> instead.</para>
    /// <para>When done with the returned texture, the app should dispose of it with a
    /// call to <see cref="SDL.DestroyTexture"/>.</para>
    /// </summary>
    /// <param name="renderer">the SDL_Renderer to use to create the GPU texture.</param>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <returns>a new texture, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadTexture"/>
    /// <seealso cref="LoadTextureTypedIO"/>
    /// <seealso cref="SDL.DestroyTexture"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadTexture_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadTextureIO(IntPtr renderer, IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL IMG_LoadTextureTyped_IO(SDL_Renderer *renderer, SDL_IOStream *src, bool closeio, const char *type);</code>
    /// <summary>
    /// <para>Load an image from an SDL data source into a GPU texture.</para>
    /// <para>An SDL_Texture represents an image in GPU memory, usable by SDL's 2D Render
    /// API. This can be significantly more efficient than using a CPU-bound
    /// SDL_Surface if you don't need to manipulate the image directly after
    /// loading it.</para>
    /// <para>If the loaded image has transparency or a colorkey, a texture with an alpha
    /// channel will be created. Otherwise, SDL_image will attempt to create an
    /// SDL_Texture in the most format that most reasonably represents the image
    /// data (but in many cases, this will just end up being 32-bit RGB or 32-bit
    /// RGBA).</para>
    /// <para>If <c>closeio</c> is true, <c>src</c> will be closed before returning, whether this
    /// function succeeds or not. SDL_image reads everything it needs from <c>src</c>
    /// during this call in any case.</para>
    /// <para>Even though this function accepts a file type, SDL_image may still try
    /// other decoders that are capable of detecting file type from the contents of
    /// the image data, but may rely on the caller-provided type string for formats
    /// that it cannot autodetect. If <c>type</c> is <c>null</c>, SDL_image will rely solely on
    /// its ability to guess the format.</para>
    /// <para>There is a separate function to read files from disk without having to deal
    /// with SDL_IOStream: <c>Image.LoadTexture("filename.jpg")</c> will call this
    /// function and manage those details for you, determining the file type from
    /// the filename's extension.</para>
    /// <para>There is also <see cref="LoadTextureIO"/>, which is equivalent to this function
    /// except that it will rely on SDL_image to determine what type of data it is
    /// loading, much like passing a <c>null</c> for type.</para>
    /// <para>If you would rather decode an image to an SDL_Surface (a buffer of pixels
    /// in CPU memory), call <see cref="LoadTypedIO"/> instead.</para>
    /// <para>When done with the returned texture, the app should dispose of it with a
    /// call to <see cref="SDL.DestroyTexture"/>.</para>
    /// </summary>
    /// <param name="renderer">the SDL_Renderer to use to create the GPU texture.</param>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <param name="type">a filename extension that represent this data ("BMP", "GIF",
    /// "PNG", etc).</param>
    /// <returns>a new texture, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadTexture"/>
    /// <seealso cref="LoadTextureIO"/>
    /// <seealso cref="SDL.DestroyTexture"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadTextureTyped_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadTextureTypedIO(IntPtr renderer, IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio, [MarshalAs(UnmanagedType.LPUTF8Str)] string type);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isAVIF(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect AVIF image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is AVIF data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isAVIF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAVIF(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isICO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect ICO image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is ICO data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isICO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsICO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isCUR(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect CUR image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is CUR data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isCUR"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsCUR(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isBMP(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect BMP image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is BMP data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isBMP"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsBMP(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isGIF(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect GIF image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek `src` back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is GIF data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isGIF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGIF(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isJPG(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect JPG image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IMG_isTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is JPG data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isJPG"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsJPG(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isJXL(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect JXL image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is JXL data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isJXL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsJXL(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isLBM(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect LBM image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is LBM data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isLBM"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsLBM(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isPCX(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect PCX image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is PCX data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isPCX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsPCX(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isPNG(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect PNG image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is PNG data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isPNG"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsPNG(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isPNM(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect PNM image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IMG_isTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is PNM data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isPNM"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsPNM(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isSVG(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect SVG image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is SVG data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isSVG"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsSVG(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isQOI(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect QOI image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and\
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is QOI data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isQOI"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsQOI(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isTIF(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect TIFF image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is TIFF data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isTIF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsTIF(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isXCF(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect XCF image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is XCF data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isXCF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsXCF(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isXPM(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect XPM image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is XPM data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXV"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isXPM"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsXPM(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isXV(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect XV image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is XV data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsWEBP"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isXV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsXV(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_isWEBP(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Detect WEBP image data on a readable/seekable SDL_IOStream.</para>
    /// <para>This function attempts to determine if a file is a given filetype, reading
    /// the least amount possible from the SDL_IOStream (usually a few bytes).</para>
    /// <para>There is no distinction made between "not the filetype in question" and
    /// basic i/o errors.</para>
    /// <para>This function will always attempt to seek <c>src</c> back to where it started
    /// when this function was called, but it will not report any errors in doing
    /// so, but assuming seeking works, this means you can immediately use this
    /// with a different IsTYPE function, or load the image without further
    /// seeking.</para>
    /// <para>You do not need to call this function to load data; SDL_image can work to
    /// determine file type in many cases in its standard load functions.</para>
    /// </summary>
    /// <param name="src">a seekable/readable SDL_IOStream to provide image data.</param>
    /// <returns>non-zero if this is WEBP data, zero otherwise.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="IsAVIF"/>
    /// <seealso cref="IsICO"/>
    /// <seealso cref="IsCUR"/>
    /// <seealso cref="IsBMP"/>
    /// <seealso cref="IsGIF"/>
    /// <seealso cref="IsJPG"/>
    /// <seealso cref="IsJXL"/>
    /// <seealso cref="IsLBM"/>
    /// <seealso cref="IsPCX"/>
    /// <seealso cref="IsPNG"/>
    /// <seealso cref="IsPNM"/>
    /// <seealso cref="IsSVG"/>
    /// <seealso cref="IsQOI"/>
    /// <seealso cref="IsTIF"/>
    /// <seealso cref="IsXCF"/>
    /// <seealso cref="IsXPM"/>
    /// <seealso cref="IsXV"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_isWEBP"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWEBP(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadAVIF_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a AVIF image directly.</para>
    /// <para>If you know you definitely have a AVIF image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadAVIF_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAVIFIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadICO_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a ICO image directly.</para>
    /// <para>If you know you definitely have a ICO image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadICO_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadICOIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadCUR_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a CUR image directly.</para>
    /// <para>If you know you definitely have a CUR image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadCUR_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadCURIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadBMP_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a BMP image directly.</para>
    /// <para>If you know you definitely have a BMP image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadBMP_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadBMPIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadGIF_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a GIF image directly.</para>
    /// <para>If you know you definitely have a GIF image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadGIF_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadGIFIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadJPG_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a JPG image directly.</para>
    /// <para>If you know you definitely have a JPG image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadJPG_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadJPGIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadJXL_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a JXL image directly.</para>
    /// <para>If you know you definitely have a JXL image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadJXL_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadJXLIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadLBM_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a LBM image directly.</para>
    /// <para>If you know you definitely have a LBM image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadLBM_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadLBMIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadPCX_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a PCX image directly.</para>
    /// <para>If you know you definitely have a PCX image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadPCX_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadPCXIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadPNG_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a PNG image directly.</para>
    /// <para>If you know you definitely have a PNG image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadPNG_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadPNGIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadPNM_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a PNM image directly.</para>
    /// <para>If you know you definitely have a PNM image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadPNM_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadPNMIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadSVG_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a SVG image directly.</para>
    /// <para>If you know you definitely have a SVG image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadSVG_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadSVGIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadQOI_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a QOI image directly.</para>
    /// <para>If you know you definitely have a QOI image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadQOI_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadQOIIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadTGA_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a TGA image directly.</para>
    /// <para>If you know you definitely have a TGA image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadTGA_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadTGAIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadTIF_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a TIFF image directly.</para>
    /// <para>If you know you definitely have a TIFF image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadTIF_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadTIFIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadXCF_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a XCF image directly.</para>
    /// <para>If you know you definitely have a XCF image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c>on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadXCF_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadXCFIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadXPM_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a XPM image directly.</para>
    /// <para>If you know you definitely have a XPM image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXVIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadXPM_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadXPMIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadXV_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a XV image directly.</para>
    /// <para>If you know you definitely have a XV image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadWEBPIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadXV_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadXVIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadWEBP_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a WEBP image directly.</para>
    /// <para>If you know you definitely have a WEBP image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load image data from.</param>
    /// <returns>SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAVIFIO"/>
    /// <seealso cref="LoadICOIO"/>
    /// <seealso cref="LoadCURIO"/>
    /// <seealso cref="LoadBMPIO"/>
    /// <seealso cref="LoadGIFIO"/>
    /// <seealso cref="LoadJPGIO"/>
    /// <seealso cref="LoadJXLIO"/>
    /// <seealso cref="LoadLBMIO"/>
    /// <seealso cref="LoadPCXIO"/>
    /// <seealso cref="LoadPNGIO"/>
    /// <seealso cref="LoadPNMIO"/>
    /// <seealso cref="LoadSVGIO"/>
    /// <seealso cref="LoadQOIIO"/>
    /// <seealso cref="LoadTGAIO"/>
    /// <seealso cref="LoadTIFIO"/>
    /// <seealso cref="LoadXCFIO"/>
    /// <seealso cref="LoadXPMIO"/>
    /// <seealso cref="LoadXVIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadWEBP_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadWEBPIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_LoadSizedSVG_IO(SDL_IOStream *src, int width, int height);</code>
    /// <summary>
    /// <para>Load an SVG image, scaled to a specific size.</para>
    /// <para>Since SVG files are resolution-independent, you specify the size you would
    /// like the output image to be and it will be generated at those dimensions.</para>
    /// <para>Either width or height may be 0 and the image will be auto-sized to
    /// preserve aspect ratio.</para>
    /// <para>When done with the returned surface, the app should dispose of it with a
    /// call to <see cref="SDL.DestroySurface"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to load SVG data from.</param>
    /// <param name="width">desired width of the generated surface, in pixels.</param>
    /// <param name="height">desired height of the generated surface, in pixels.</param>
    /// <returns>a new SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadSizedSVG_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadSizedSVGIO(IntPtr src, int width, int height);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_ReadXPMFromArray(char **xpm);</code>
    /// <summary>
    /// <para>Load an XPM image from a memory array.</para>
    /// <para>The returned surface will be an 8bpp indexed surface, if possible,
    /// otherwise it will be 32bpp. If you always want 32-bit data, use
    /// <see cref="ReadXPMFromArrayToRGB888"/> instead.</para>
    /// <para>When done with the returned surface, the app should dispose of it with a
    /// call to <see cref="SDL.DestroySurface"/>.</para>
    /// </summary>
    /// <param name="xpm">a null-terminated array of strings that comprise XPM data.</param>
    /// <returns>a new SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_ReadXPMFromArray"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ReadXPMFromArray([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] xpm);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL IMG_ReadXPMFromArrayToRGB888(char **xpm);</code>
    /// <summary>
    /// <para>Load an XPM image from a memory array.</para>
    /// <para>The returned surface will always be a 32-bit RGB surface. If you want 8-bit
    /// indexed colors (and the XPM data allows it), use <see cref="ReadXPMFromArray"/>
    /// instead.</para>
    /// <para>When done with the returned surface, the app should dispose of it with a
    /// call to <see cref="SDL.DestroySurface"/>.</para>
    /// </summary>
    /// <param name="xpm">a null-terminated array of strings that comprise XPM data.</param>
    /// <returns>a new SDL surface, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="ReadXPMFromArray"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_ReadXPMFromArrayToRGB888"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ReadXPMFromArrayToRGB888([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] xpm);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_SaveAVIF(SDL_Surface *surface, const char *file, int quality);</code>
    /// <summary>
    /// <para>Save an SDL_Surface into a AVIF image file.</para>
    /// <para>If the file already exists, it will be overwritten.</para>
    /// </summary>
    /// <param name="surface">the SDL surface to save.</param>
    /// <param name="file">path on the filesystem to write new file to.</param>
    /// <param name="quality">the desired quality, ranging between 0 (lowest) and 100
    /// (highest).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="SaveAVIFIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_SaveAVIF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveAVIF(IntPtr surface, [MarshalAs(UnmanagedType.LPUTF8Str)] string file, int quality);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_SaveAVIF_IO(SDL_Surface *surface, SDL_IOStream *dst, bool closeio, int quality);</code>
    /// <summary>
    /// <para>Save an SDL_Surface into AVIF image data, via an SDL_IOStream.</para>
    /// <para>If you just want to save to a filename, you can use <see cref="SaveAVIF"/> instead.</para>
    /// <para>If <c>closeio</c> is true, <c>dst</c> will be closed before returning, whether this
    /// function succeeds or not.</para>
    /// </summary>
    /// <param name="surface">the SDL surface to save.</param>
    /// <param name="dst">the SDL_IOStream to save the image data to.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <param name="quality">the desired quality, ranging between 0 (lowest) and 100
    /// (highest).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="SaveAVIF"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_SaveAVIF_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveAVIFIO(IntPtr surface, IntPtr dst, [MarshalAs(UnmanagedType.I1)] bool closeio, int quality);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_SavePNG(SDL_Surface *surface, const char *file);</code>
    /// <summary>
    /// <para>Save an SDL_Surface into a PNG image file.</para>
    /// <para>If the file already exists, it will be overwritten.</para>
    /// </summary>
    /// <param name="surface">the SDL surface to save.</param>
    /// <param name="file">path on the filesystem to write new file to.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="SavePNGIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_SavePNG"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SavePNG(IntPtr surface, [MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_SavePNG_IO(SDL_Surface *surface, SDL_IOStream *dst, bool closeio);</code>
    /// <summary>
    /// <para>Save an SDL_Surface into PNG image data, via an SDL_IOStream.</para>
    /// <para>If you just want to save to a filename, you can use <see cref="SavePNG"/> instead.</para>
    /// <para>If <c>closeio</c> is true, <c>dst</c> will be closed before returning, whether this
    /// function succeeds or not.</para>
    /// </summary>
    /// <param name="surface">the SDL surface to save.</param>
    /// <param name="dst">the SDL_IOStream to save the image data to.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="SavePNG"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_SavePNG_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SavePNGIO(IntPtr surface, IntPtr dst, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_SaveJPG(SDL_Surface *surface, const char *file, int quality);</code>
    /// <summary>
    /// <para>Save an SDL_Surface into a JPEG image file.</para>
    /// <para>If the file already exists, it will be overwritten.</para>
    /// </summary>
    /// <param name="surface">the SDL surface to save.</param>
    /// <param name="file">path on the filesystem to write new file to.</param>
    /// <param name="quality">[0; 33] is Lowest quality, [34; 66] is Middle quality, [67;
    /// 100] is Highest quality.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="SaveJPGIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_SaveJPG"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveJPG(IntPtr surface, [MarshalAs(UnmanagedType.LPUTF8Str)] string file, int quality);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL IMG_SaveJPG_IO(SDL_Surface *surface, SDL_IOStream *dst, bool closeio, int quality);</code>
    /// <summary>
    /// <para>Save an SDL_Surface into JPEG image data, via an SDL_IOStream.</para>
    /// <para>If you just want to save to a filename, you can use <see cref="SaveJPG"/> instead.</para>
    /// <para>If <c>closeio</c> is true, <c>dst</c> will be closed before returning, whether this
    /// function succeeds or not.</para>
    /// </summary>
    /// <param name="surface">the SDL surface to save.</param>
    /// <param name="dst">the SDL_IOStream to save the image data to.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <param name="quality">[0; 33] is Lowest quality, [34; 66] is Middle quality, [67;
    /// 100] is Highest quality.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="SaveJPG"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_SaveJPG_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveJPGIO(IntPtr surface, IntPtr dst, [MarshalAs(UnmanagedType.I1)] bool closeio, int quality);
    
    
    /// <code>extern SDL_DECLSPEC IMG_Animation * SDLCALL IMG_LoadAnimation(const char *file);</code>
    /// <summary>
    /// <para>Load an animation from a file.</para>
    /// <para>When done with the returned animation, the app should dispose of it with a
    /// call to <see cref="FreeAnimation"/>.</para>
    /// </summary>
    /// <param name="file">path on the filesystem containing an animated image.</param>
    /// <returns>a new IMG_Animation, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="FreeAnimation"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadAnimation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAnimation([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC IMG_Animation * SDLCALL IMG_LoadAnimation_IO(SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load an animation from an SDL_IOStream.</para>
    /// <para>If <c>closeio</c> is true, <c>src</c> will be closed before returning, whether this
    /// function succeeds or not. SDL_image reads everything it needs from <c>src</c>
    /// during this call in any case.</para>
    /// <para>When done with the returned animation, the app should dispose of it with a
    /// call to <see cref="FreeAnimation"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <returns>a new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="FreeAnimation"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadAnimation_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAnimationIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC IMG_Animation * SDLCALL IMG_LoadAnimationTyped_IO(SDL_IOStream *src, bool closeio, const char *type);</code>
    /// <summary>
    /// <para>Load an animation from an SDL datasource</para>
    /// <para>Even though this function accepts a file type, SDL_image may still try
    /// other decoders that are capable of detecting file type from the contents of
    /// the image data, but may rely on the caller-provided type string for formats
    /// that it cannot autodetect. If <c>type</c> is <c>null</c>, SDL_image will rely solely on
    /// its ability to guess the format.</para>
    /// <para>If <c>closeio</c> is true, <c>src</c> will be closed before returning, whether this
    /// function succeeds or not. SDL_image reads everything it needs from <c>src</c>
    /// during this call in any case.</para>
    /// <para>When done with the returned animation, the app should dispose of it with a
    /// call to <see cref="FreeAnimation"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close/free the SDL_IOStream before returning, <c>false</c>
    /// to leave it open.</param>
    /// <param name="type">a filename extension that represent this data ("GIF", etc).</param>
    /// <returns>a new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAnimation"/>
    /// <seealso cref="LoadAnimationIO"/>
    /// <seealso cref="FreeAnimation"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadAnimationTyped_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAnimationTypedIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio, [MarshalAs(UnmanagedType.LPUTF8Str)] string type);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL IMG_FreeAnimation(IMG_Animation *anim);</code>
    /// <summary>
    /// <para>Dispose of an <see cref="Animation"/> and free its resources.</para>
    /// <para>The provided <c>anim</c> pointer is not valid once this call returns.</para>
    /// </summary>
    /// <param name="anim"><see cref="Animation"/> to dispose of.</param>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAnimation"/>
    /// <seealso cref="LoadAnimationIO"/>
    /// <seealso cref="LoadAnimationTypedIO"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_FreeAnimation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAnimation(IntPtr anim);
    
    
    /// <code>extern SDL_DECLSPEC IMG_Animation * SDLCALL IMG_LoadGIFAnimation_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a GIF animation directly.</para>
    /// <para>If you know you definitely have a GIF image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <returns>a new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAnimation"/>
    /// <seealso cref="LoadAnimationIO"/>
    /// <seealso cref="LoadAnimationTypedIO"/>
    /// <seealso cref="FreeAnimation"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadGIFAnimation_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadGIFAnimationIO(IntPtr src);
    
    
    /// <code>extern SDL_DECLSPEC IMG_Animation * SDLCALL IMG_LoadWEBPAnimation_IO(SDL_IOStream *src);</code>
    /// <summary>
    /// <para>Load a WEBP animation directly.</para>
    /// <para>If you know you definitely have a WEBP image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's
    /// better to use the abstract interfaces; also, there is only an SDL_IOStream
    /// interface available here.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <returns>a new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_image 3.0.0.</since>
    /// <seealso cref="LoadAnimation"/>
    /// <seealso cref="LoadAnimationIO"/>
    /// <seealso cref="LoadAnimationTypedIO"/>
    /// <seealso cref="FreeAnimation"/>
    [LibraryImport(ImageLibrary, EntryPoint = "IMG_LoadWEBPAnimation_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadWEBPAnimationIO(IntPtr src);
}