#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class TTF
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_Version"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_Version();
    private delegate int VersionNativeDelegate();
    private static VersionNativeDelegate VersionNativeFunction = TTF_Version;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_Version(void);</code>
    /// <summary>
    /// This function gets the version of the dynamically linked SDL_ttf library.
    /// </summary>
    /// <returns>SDL_ttf version.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static int Version()
    {
        return VersionNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFreeTypeVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_GetFreeTypeVersion(out int major, out int minor, out int patch);
    private delegate void GetFreeTypeVersionNativeDelegate(out int major, out int minor, out int patch);
    private static GetFreeTypeVersionNativeDelegate GetFreeTypeVersionNativeFunction = TTF_GetFreeTypeVersion;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_GetFreeTypeVersion(int *major, int *minor, int *patch);</code>
    /// <summary>
    /// <para>Query the version of the FreeType library in use.</para>
    /// <para><see cref="Init"/> should be called before calling this function.</para>
    /// </summary>
    /// <param name="major">to be filled in with the major version number. Can be <c>null</c>.</param>
    /// <param name="minor">to be filled in with the minor version number. Can be <c>null</c>..</param>
    /// <param name="patch">to be filled in with the param version number. Can be <c>null</c>..</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="Init"/>
    public static void GetFreeTypeVersion(out int major, out int minor, out int patch)
    {
        GetFreeTypeVersionNativeFunction(out major, out minor, out patch);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetHarfBuzzVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_GetHarfBuzzVersion(out int major, out int minor, out int patch);
    private delegate void GetHarfBuzzVersionNativeDelegate(out int major, out int minor, out int patch);
    private static GetHarfBuzzVersionNativeDelegate GetHarfBuzzVersionNativeFunction = TTF_GetHarfBuzzVersion;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_GetHarfBuzzVersion(int *major, int *minor, int *patch);</code>
    /// <summary>
    /// <para>Query the version of the HarfBuzz library in use.</para>
    /// <para>If HarfBuzz is not available, the version reported is 0.0.0.</para>
    /// </summary>
    /// <param name="major">to be filled in with the major version number. Can be <c>null</c>.</param>
    /// <param name="minor">to be filled in with the minor version number. Can be <c>null</c>..</param>
    /// <param name="patch">to be filled in with the param version number. Can be <c>null</c>..</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static void GetHarfBuzzVersion(out int major, out int minor, out int patch)
    {
        GetHarfBuzzVersionNativeFunction(out major, out minor, out patch);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_Init();
    private delegate bool InitNativeDelegate();
    private static InitNativeDelegate InitNativeFunction = TTF_Init;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_Init(void);</code>
    /// <summary>
    /// <para>Initialize SDL_ttf.</para>
    /// <para>You must successfully call this function before it is safe to call any
    /// other function in this library.</para>
    /// <para>It is safe to call this more than once, and each successful <see cref="Init"/> call
    /// should be paired with a matching <see cref="Quit"/> call.</para>
    /// </summary>
    /// <returns>><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="Quit"/>
    public static bool Init()
    {
        return InitNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_OpenFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_OpenFont([MarshalAs(UnmanagedType.LPUTF8Str)] string file, float ptsize);
    private delegate IntPtr OpenFontNativeDelegate(string file, float ptsize);
    private static OpenFontNativeDelegate OpenFontNativeFunction = TTF_OpenFont;

    /// <code>extern SDL_DECLSPEC TTF_Font * SDLCALL TTF_OpenFont(const char *file, float ptsize);</code>
    /// <summary>
    /// <para>Create a font from a file, using a specified point size.</para>
    /// <para>Some .fon fonts will have several sizes embedded in the file, so the point
    /// size becomes the index of choosing which size. If the value is too high,
    /// the last indexed size will be the default.</para>
    /// <para>When done with the returned TTF_Font, use <see cref="CloseFont"/> to dispose of it.</para>
    /// </summary>
    /// <param name="file">path to font file.</param>
    /// <param name="ptsize">point size to use for the newly-opened font.</param>
    /// <returns>a valid TTF_Font, or <c>null</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CloseFont"/>
    public static IntPtr OpenFont([MarshalAs(UnmanagedType.LPUTF8Str)] string file, float ptsize)
    {
        return OpenFontNativeFunction(file, ptsize);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_OpenFontIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_OpenFontIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio, float ptsize);
    private delegate IntPtr OpenFontIONativeDelegate(IntPtr src, bool closeio, float ptsize);
    private static OpenFontIONativeDelegate OpenFontIONativeFunction = TTF_OpenFontIO;

    /// <code>extern SDL_DECLSPEC TTF_Font * SDLCALL TTF_OpenFontIO(SDL_IOStream *src, bool closeio, float ptsize);</code>
    /// <summary>
    /// <para>Create a font from an SDL_IOStream, using a specified point size.</para>
    /// <para>Some .fon fonts will have several sizes embedded in the file, so the point
    /// size becomes the index of choosing which size. If the value is too high,
    /// the last indexed size will be the default.</para>
    /// <para>If <c>closeio</c> is <c>true</c>, <c>src</c> will be automatically closed once the font is
    /// closed. Otherwise you should keep <c>src</c> open until the font is closed.</para>
    /// <para>When done with the returned TTF_Font, use <see cref="CloseFont"/> to dispose of it.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream to provide a font file's data.</param>
    /// <param name="closeio"><c>true</c> to close <c>src</c> when the font is closed, <c>false</c> to leave
    /// it open.</param>
    /// <param name="ptsize">point size to use for the newly-opened font.</param>
    /// <returns>a valid TTF_Font, or <c>null</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CloseFont"/>
    public static IntPtr OpenFontIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio, float ptsize)
    {
        return OpenFontIONativeFunction(src, closeio, ptsize);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_OpenFontWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_OpenFontWithProperties(uint props);
    private delegate IntPtr OpenFontWithPropertiesNativeDelegate(uint props);
    private static OpenFontWithPropertiesNativeDelegate OpenFontWithPropertiesNativeFunction = TTF_OpenFontWithProperties;

    /// <code>extern SDL_DECLSPEC TTF_Font * SDLCALL TTF_OpenFontWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a font with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.FontCreateFilenameString"/>: the font file to open, if an
    /// SDL_IOStream isn't being used. This is required if
    /// <see cref="Props.FontCreateIOStreamPointer"/> and
    /// <see cref="Props.FontCreateExistingFont"/> aren't set.</item>
    /// <item><see cref="Props.FontCreateIOStreamPointer"/>: an SDL_IOStream containing the
    /// font to be opened. This should not be closed until the font is closed.
    /// This is required if <see cref="Props.FontCreateFilenameString"/> and
    /// <see cref="Props.FontCreateExistingFont"/> aren't set.</item>
    /// <item><see cref="Props.FontCreateIOStreamOffsetNumber"/>: the offset in the iostream
    /// for the beginning of the font, defaults to 0.</item>
    /// <item><see cref="Props.FontCreateIOStreamAutoCloseBoolean"/>: <c>true</c> if closing the
    /// font should also close the associated SDL_IOStream.</item>
    /// <item><see cref="Props.FontCreateSizeFloat"/>: the point size of the font. Some .fon
    /// fonts will have several sizes embedded in the file, so the point size
    /// becomes the index of choosing which size. If the value is too high, the
    /// last indexed size will be the default.</item>
    /// <item><see cref="Props.FontCreateFaceNumber"/>: the face index of the font, if the
    /// font contains multiple font faces.</item>
    /// <item><see cref="Props.FontCreateHorizontalDPINumber"/>: the horizontal DPI to use
    /// for font rendering, defaults to
    /// <see cref="Props.FontCreateVerticalDPINumber"/> if set, or 72 otherwise.</item>
    /// <item><see cref="Props.FontCreateVerticalDPINumber"/>: the vertical DPI to use for
    /// font rendering, defaults to <see cref="Props.FontCreateHorizontalDPINumber"/>
    /// if set, or 72 otherwise.</item>
    /// <item><see cref="Props.FontCreateExistingFont"/>: an optional TTF_Font that, if set,
    /// if set, will be used as the font data source and the initial size and
    /// style of the new font.</item>
    /// </list>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>a valid TTF_Font, or <c>null</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CloseFont"/>
    public static IntPtr OpenFontWithProperties(uint props)
    {
        return OpenFontWithPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CopyFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CopyFont(IntPtr existingFont);
    private delegate IntPtr CopyFontNativeDelegate(IntPtr existingFont);
    private static CopyFontNativeDelegate CopyFontNativeFunction = TTF_CopyFont;

    /// <code>extern SDL_DECLSPEC TTF_Font * SDLCALL TTF_CopyFont(TTF_Font *existing_font);</code>
    /// <summary>
    /// <para>Create a copy of an existing font.</para>
    /// <para>The copy will be distinct from the original, but will share the font file
    /// and have the same size and style as the original.</para>
    /// <para>When done with the returned TTF_Font, use <see cref="CloseFont"/> to dispose of it.</para>
    /// </summary>
    /// <param name="existingFont">the font to copy.</param>
    /// <returns>a valid TTF_Font, or <c>null</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// original font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CloseFont"/>
    public static IntPtr CopyFont(IntPtr existingFont)
    {
        return CopyFontNativeFunction(existingFont);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetFontProperties(IntPtr font);
    private delegate uint GetFontPropertiesNativeDelegate(IntPtr font);
    private static GetFontPropertiesNativeDelegate GetFontPropertiesNativeFunction = TTF_GetFontProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL TTF_GetFontProperties(TTF_Font *font);</code>
    /// <summary>
    /// <para>Get the properties associated with a font.</para>
    /// <para>The following read-write properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.FontOutlineLineCapNumber"/>: The FT_Stroker_LineCap value
    /// used when setting the font outline, defaults to
    /// <c>FT_STROKER_LINECAP_ROUND</c>.</item>
    /// <item><see cref="Props.FontOutlineLineJoinNumber"/>: The FT_Stroker_LineJoin value
    /// used when setting the font outline, defaults to
    /// <c>FT_STROKER_LINEJOIN_ROUND</c>.</item>
    /// <item><see cref="Props.FontOutlineMiterLimitNumber"/>: The FT_Fixed miter limit used
    /// when setting the font outline, defaults to 0.</item>
    /// </list>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static uint GetFontProperties(IntPtr font)
    {
        return GetFontPropertiesNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontGeneration"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetFontGeneration(IntPtr font);
    private delegate uint GetFontGenerationNativeDelegate(IntPtr font);
    private static GetFontGenerationNativeDelegate GetFontGenerationNativeFunction = TTF_GetFontGeneration;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL TTF_GetFontGeneration(TTF_Font *font);</code>
    /// <summary>
    /// <para>Get the font generation.</para>
    /// <para>The generation is incremented each time font properties change that require
    /// rebuilding glyphs, such as style, size, etc.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font generation or 0 on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static uint GetFontGeneration(IntPtr font)
    {
        return GetFontGenerationNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_AddFallbackFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_AddFallbackFont(IntPtr font, IntPtr fallback);
    private delegate bool AddFallbackFontNativeDelegate(IntPtr font, IntPtr fallback);
    private static AddFallbackFontNativeDelegate AddFallbackFontNativeFunction = TTF_AddFallbackFont;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_AddFallbackFont(TTF_Font *font, TTF_Font *fallback);</code>
    /// <summary>
    /// <para>Add a fallback font.</para>
    /// <para>Add a font that will be used for glyphs that are not in the current font.
    /// The fallback font should have the same size and style as the current font.</para>
    /// <para>If there are multiple fallback fonts, they are used in the order added.</para>
    /// <para>This updates any TTF_Text objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="fallback">the font to add as a fallback.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created
    /// both fonts.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="ClearFallbackFonts"/>
    /// <seealso cref="RemoveFallbackFont"/>
    public static bool AddFallbackFont(IntPtr font, IntPtr fallback)
    {
        return AddFallbackFontNativeFunction(font, fallback);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RemoveFallbackFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_RemoveFallbackFont(IntPtr font, IntPtr fallback);
    private delegate void RemoveFallbackFontNativeDelegate(IntPtr font, IntPtr fallback);
    private static RemoveFallbackFontNativeDelegate RemoveFallbackFontNativeFunction = TTF_RemoveFallbackFont;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_RemoveFallbackFont(TTF_Font *font, TTF_Font *fallback);</code>
    /// <summary>
    /// <para>Remove a fallback font.</para>
    /// <para>This updates any TTF_Text objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="fallback">the font to remove as a fallback.</param>
    /// <threadsafety>This function should be called on the thread that created
    /// both fonts.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="AddFallbackFont"/>
    /// <seealso cref="ClearFallbackFonts"/>
    public static void RemoveFallbackFont(IntPtr font, IntPtr fallback)
    {
        RemoveFallbackFontNativeFunction(font, fallback);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_ClearFallbackFonts"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_ClearFallbackFonts(IntPtr font);
    private delegate void ClearFallbackFontsNativeDelegate(IntPtr font);
    private static ClearFallbackFontsNativeDelegate ClearFallbackFontsNativeFunction = TTF_ClearFallbackFonts;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_ClearFallbackFonts(TTF_Font *font);</code>
    /// <summary>
    /// <para>Remove all fallback fonts.</para>
    /// <para>This updates any TTF_Text objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to modify.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="AddFallbackFont"/>
    /// <seealso cref="RemoveFallbackFont"/>
    public static void ClearFallbackFonts(IntPtr font)
    {
        ClearFallbackFontsNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontSize(IntPtr font, float ptsize);
    private delegate bool SetFontSizeNativeDelegate(IntPtr font, float ptsize);
    private static SetFontSizeNativeDelegate SetFontSizeNativeFunction = TTF_SetFontSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetFontSize(TTF_Font *font, float ptsize);</code>
    /// <summary>
    /// <para>Set a font's size dynamically.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.</para>
    /// </summary>
    /// <param name="font">the font to resize.</param>
    /// <param name="ptsize">the new point size.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontSize"/>
    public static bool SetFontSize(IntPtr font, float ptsize)
    {
        return SetFontSizeNativeFunction(font, ptsize);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontSizeDPI"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontSizeDPI(IntPtr font, float ptsize, int hdpi, int vdpi);
    private delegate bool SetFontSizeDPINativeDelegate(IntPtr font, float ptsize, int hdpi, int vdpi);
    private static SetFontSizeDPINativeDelegate SetFontSizeDPINativeFunction = TTF_SetFontSizeDPI;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetFontSizeDPI(TTF_Font *font, float ptsize, int hdpi, int vdpi);</code>
    /// <summary>
    /// <para>Set font size dynamically with target resolutions, in dots per inch.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.</para>
    /// </summary>
    /// <param name="font">the font to resize.</param>
    /// <param name="ptsize">the new point size.</param>
    /// <param name="hdpi">the target horizontal DPI.</param>
    /// <param name="vdpi">the target vertical DPI.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontSize"/>
    public static bool SetFontSizeDPI(IntPtr font, float ptsize, int hdpi, int vdpi)
    {
        return SetFontSizeDPINativeFunction(font, ptsize, hdpi, vdpi);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float TTF_GetFontSize(IntPtr font);
    private delegate float GetFontSizeNativeDelegate(IntPtr font);
    private static GetFontSizeNativeDelegate GetFontSizeNativeFunction = TTF_GetFontSize;

    /// <code>extern SDL_DECLSPEC float SDLCALL TTF_GetFontSize(TTF_Font *font);</code>
    /// <summary>
    /// Get the size of a font.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the size of the font, or 0.0f on failure; call <see cref="SDL.GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontSize"/>
    /// <seealso cref="SetFontSizeDPI"/>
    public static float GetFontSize(IntPtr font)
    {
        return GetFontSizeNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontDPI"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetFontDPI(IntPtr font, out int hdpi, out int vdpi);
    private delegate bool GetFontDPINativeDelegate(IntPtr font, out int hdpi, out int vdpi);
    private static GetFontDPINativeDelegate GetFontDPINativeFunction = TTF_GetFontDPI;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetFontDPI(TTF_Font *font, int *hdpi, int *vdpi);</code>
    /// <summary>
    /// <para>Get font target resolutions, in dots per inch.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="hdpi">a pointer filled in with the target horizontal DPI.</param>
    /// <param name="vdpi">a pointer filled in with the target vertical DPI.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontSizeDPI"/>
    public static bool GetFontDPI(IntPtr font, out int hdpi, out int vdpi)
    {
        return GetFontDPINativeFunction(font, out hdpi, out vdpi);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontStyle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontStyle(IntPtr font, FontStyleFlags style);
    private delegate void SetFontStyleNativeDelegate(IntPtr font, FontStyleFlags style);
    private static SetFontStyleNativeDelegate SetFontStyleNativeFunction = TTF_SetFontStyle;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_SetFontStyle(TTF_Font *font, TTF_FontStyleFlags style);</code>
    /// <summary>
    /// <para>Set a font's current style.</para>
    /// <para>This updates any TTF_Text objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.</para>
    /// <para>The font styles are a set of bit flags, OR'd together:</para>
    /// <list type="bullet">
    /// <item><see cref="FontStyleFlags.Normal"/> (is zero)</item>
    /// <item><see cref="FontStyleFlags.Bold"/></item>
    /// <item><see cref="FontStyleFlags.Italic"/></item>
    /// <item><see cref="FontStyleFlags.Underline"/></item>
    /// <item><see cref="FontStyleFlags.Strikethrough"/></item>
    /// </list>
    /// </summary>
    /// <param name="font">the font to set a new style on.</param>
    /// <param name="style">the new style values to set, OR'd together.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontStyle"/>
    public static void SetFontStyle(IntPtr font, FontStyleFlags style)
    {
        SetFontStyleNativeFunction(font, style);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontStyle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial FontStyleFlags TTF_GetFontStyle(IntPtr font);
    private delegate FontStyleFlags GetFontStyleNativeDelegate(IntPtr font);
    private static GetFontStyleNativeDelegate GetFontStyleNativeFunction = TTF_GetFontStyle;

    /// <code>extern SDL_DECLSPEC TTF_FontStyleFlags SDLCALL TTF_GetFontStyle(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query a font's current style.</para>
    /// <para>The font styles are a set of bit flags, OR'd together:</para>
    /// <list type="bullet">
    /// <item><see cref="FontStyleFlags.Normal"/> (is zero)</item>
    /// <item><see cref="FontStyleFlags.Bold"/></item>
    /// <item><see cref="FontStyleFlags.Italic"/></item>
    /// <item><see cref="FontStyleFlags.Underline"/></item>
    /// <item><see cref="FontStyleFlags.Strikethrough"/></item>
    /// </list>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the current font style, as a set of bit flags.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontStyle"/>
    public static FontStyleFlags GetFontStyle(IntPtr font)
    {
        return GetFontStyleNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontOutline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontOutline(IntPtr font, int outline);
    private delegate bool SetFontOutlineNativeDelegate(IntPtr font, int outline);
    private static SetFontOutlineNativeDelegate SetFontOutlineNativeFunction = TTF_SetFontOutline;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetFontOutline(TTF_Font *font, int outline);</code>
    /// <summary>
    /// <para>Set a font's current outline.</para>
    /// <para>This uses the font properties <see cref="Props.FontOutlineLineCapNumber"/>,
    /// <see cref="Props.FontOutlineLineJoinNumber"/>, and
    /// <see cref="Props.FontOutlineMiterLimitNumber"/> when setting the font outline.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.</para>
    /// </summary>
    /// <param name="font">the font to set a new outline on.</param>
    /// <param name="outline">positive outline value, 0 to default.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontOutline"/>
    public static bool SetFontOutline(IntPtr font, int outline)
    {
        return SetFontOutlineNativeFunction(font, outline);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontOutline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontOutline(IntPtr font);
    private delegate int GetFontOutlineNativeDelegate(IntPtr font);
    private static GetFontOutlineNativeDelegate GetFontOutlineNativeFunction = TTF_GetFontOutline;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetFontOutline(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query a font's current outline.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's current outline value.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontOutline"/>
    public static int GetFontOutline(IntPtr font)
    {
        return GetFontOutlineNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontHinting"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontHinting(IntPtr font, HintingFlags hinting);
    private delegate void SetFontHintingNativeDelegate(IntPtr font, HintingFlags hinting);
    private static SetFontHintingNativeDelegate SetFontHintingNativeFunction = TTF_SetFontHinting;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_SetFontHinting(TTF_Font *font, TTF_HintingFlags hinting);</code>
    /// <summary>
    /// <para>Set a font's current hinter setting.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.</para>
    /// <para>The hinter setting is a single value:</para>
    /// <list type="bullet">
    /// <item><see cref="HintingFlags.Normal"/></item>
    /// <item><see cref="HintingFlags.Light"/></item>
    /// <item><see cref="HintingFlags.Mono"/></item>
    /// <item><see cref="HintingFlags.None"/></item>
    /// <item><see cref="HintingFlags.LightSubpixel"/> (available in SDL_ttf 3.0.0 and later)</item>
    /// </list>
    /// </summary>
    /// <param name="font">the font to set a new hinter setting on.</param>
    /// <param name="hinting">the new hinter setting.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontHinting"/>
    public static void SetFontHinting(IntPtr font, HintingFlags hinting)
    {
        SetFontHintingNativeFunction(font, hinting);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetNumFontFaces"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetNumFontFaces(IntPtr font);
    private delegate int GetNumFontFacesNativeDelegate(IntPtr font);
    private static GetNumFontFacesNativeDelegate GetNumFontFacesNativeFunction = TTF_GetNumFontFaces;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetNumFontFaces(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query the number of faces of a font.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the number of FreeType font faces.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static int GetNumFontFaces(IntPtr font)
    {
        return GetNumFontFacesNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontHinting"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HintingFlags TTF_GetFontHinting(IntPtr font);
    private delegate HintingFlags GetFontHintingNativeDelegate(IntPtr font);
    private static GetFontHintingNativeDelegate GetFontHintingNativeFunction = TTF_GetFontHinting;

    /// <code>extern SDL_DECLSPEC TTF_HintingFlags SDLCALL TTF_GetFontHinting(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query a font's current FreeType hinter setting.</para>
    /// <para>The hinter setting is a single value:</para>
    /// <list type="bullet">
    /// <item><see cref="HintingFlags.Normal"/></item>
    /// <item><see cref="HintingFlags.Light"/></item>
    /// <item><see cref="HintingFlags.Mono"/></item>
    /// <item><see cref="HintingFlags.None"/></item>
    /// <item><see cref="HintingFlags.LightSubpixel"/> (available in SDL_ttf 3.0.0 and later)</item>
    /// </list>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's current hinter value.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontHinting"/>
    public static HintingFlags GetFontHinting(IntPtr font)
    {
        return GetFontHintingNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontSDF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontSDF(IntPtr font, [MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate bool SetFontSDFNativeDelegate(IntPtr font, bool enabled);
    private static SetFontSDFNativeDelegate SetFontSDFNativeFunction = TTF_SetFontSDF;

    /// <code>extern SDL_DECLSPEC bool TTF_SetFontSDF(TTF_Font *font, bool enabled);</code>
    /// <summary>
    /// <para>Enable Signed Distance Field rendering for a font.</para>
    /// <para>SDF is a technique that helps fonts look sharp even when scaling and
    /// rotating, and requires special shader support for display.</para>
    /// <para>This works with Blended APIs, and generates the raw signed distance values
    /// in the alpha channel of the resulting texture.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font, and clears
    /// already-generated glyphs, if any, from the cache.</para>
    /// </summary>
    /// <param name="font">the font to set SDF support on.</param>
    /// <param name="enabled"><c>true</c> to enable SDF, <c>false</c> to disable.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontSDF"/>
    public static bool SetFontSDF(IntPtr font, [MarshalAs(UnmanagedType.I1)] bool enabled)
    {
        return SetFontSDFNativeFunction(font, enabled);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontSDF"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetFontSDF(IntPtr font);
    private delegate bool GetFontSDFNativeDelegate(IntPtr font);
    private static GetFontSDFNativeDelegate GetFontSDFNativeFunction = TTF_GetFontSDF;

    /// <code>extern SDL_DECLSPEC bool TTF_GetFontSDF(const TTF_Font *font);</code>
    /// <summary>
    /// Query whether Signed Distance Field rendering is enabled for a font.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns><c>true</c> if enabled, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontSDF"/>
    public static bool GetFontSDF(IntPtr font)
    {
        return GetFontSDFNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontWeight"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontWeight(in IntPtr font);
    private delegate int GetFontWeightNativeDelegate(in IntPtr font);
    private static GetFontWeightNativeDelegate GetFontWeightNativeFunction = TTF_GetFontWeight;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetFontWeight(const TTF_Font *font);</code>
    /// <summary>
    /// Query a font's weight, in terms of the lightness/heaviness of the strokes.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's current weight.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.2.2.</since>
    public static int GetFontWeight(in IntPtr font)
    {
        return GetFontWeightNativeFunction(in font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontWrapAlignment"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontWrapAlignment(IntPtr font, HorizontalAlignment align);
    private delegate void SetFontWrapAlignmentNativeDelegate(IntPtr font, HorizontalAlignment align);
    private static SetFontWrapAlignmentNativeDelegate SetFontWrapAlignmentNativeFunction = TTF_SetFontWrapAlignment;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_SetFontWrapAlignment(TTF_Font *font, TTF_HorizontalAlignment align);</code>
    /// <summary>
    /// <para>Set a font's current wrap alignment option.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to set a new wrap alignment option on.</param>
    /// <param name="align">he new wrap alignment option.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontWrapAlignment"/>
    public static void SetFontWrapAlignment(IntPtr font, HorizontalAlignment align)
    {
        SetFontWrapAlignmentNativeFunction(font, align);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontWrapAlignment"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HorizontalAlignment TTF_GetFontWrapAlignment(IntPtr font);
    private delegate HorizontalAlignment GetFontWrapAlignmentNativeDelegate(IntPtr font);
    private static GetFontWrapAlignmentNativeDelegate GetFontWrapAlignmentNativeFunction = TTF_GetFontWrapAlignment;

    /// <code>extern SDL_DECLSPEC TTF_HorizontalAlignment SDLCALL TTF_GetFontWrapAlignment(const TTF_Font *font);</code>
    /// <summary>
    /// Query a font's current wrap alignment option.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's current wrap alignment option.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontWrapAlignment"/>
    public static HorizontalAlignment GetFontWrapAlignment(IntPtr font)
    {
        return GetFontWrapAlignmentNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontHeight"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontHeight(IntPtr font);
    private delegate int GetFontHeightNativeDelegate(IntPtr font);
    private static GetFontHeightNativeDelegate GetFontHeightNativeFunction = TTF_GetFontHeight;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetFontHeight(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query the total height of a font.</para>
    /// <para>This is usually equal to point size.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's height.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static int GetFontHeight(IntPtr font)
    {
        return GetFontHeightNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontAscent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontAscent(IntPtr font);
    private delegate int GetFontAscentNativeDelegate(IntPtr font);
    private static GetFontAscentNativeDelegate GetFontAscentNativeFunction = TTF_GetFontAscent;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetFontAscent(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query the offset from the baseline to the top of a font.</para>
    /// <para>This is a positive value, relative to the baseline.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's ascent.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static int GetFontAscent(IntPtr font)
    {
        return GetFontAscentNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontDescent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontDescent(IntPtr font);
    private delegate int GetFontDescentNativeDelegate(IntPtr font);
    private static GetFontDescentNativeDelegate GetFontDescentNativeFunction = TTF_GetFontDescent;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetFontDescent(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query the offset from the baseline to the bottom of a font.</para>
    /// <para>This is a negative value, relative to the baseline.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's descent.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static int GetFontDescent(IntPtr font)
    {
        return GetFontDescentNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontLineSkip"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontLineSkip(IntPtr font, int lineskip);
    private delegate void SetFontLineSkipNativeDelegate(IntPtr font, int lineskip);
    private static SetFontLineSkipNativeDelegate SetFontLineSkipNativeFunction = TTF_SetFontLineSkip;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_SetFontLineSkip(TTF_Font *font, int lineskip);</code>
    /// <summary>
    /// <para>Set the spacing between lines of text for a font.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="lineskip">the new line spacing for the font.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetFontLineSkip"/>
    public static void SetFontLineSkip(IntPtr font, int lineskip)
    {
        SetFontLineSkipNativeFunction(font, lineskip);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontLineSkip"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_GetFontLineSkip(IntPtr font);
    private delegate int GetFontLineSkipNativeDelegate(IntPtr font);
    private static GetFontLineSkipNativeDelegate GetFontLineSkipNativeFunction = TTF_GetFontLineSkip;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_GetFontLineSkip(const TTF_Font *font);</code>
    /// <summary>
    /// Query the spacing between lines of text for a font.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's recommended spacing.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontLineSkip"/>
    public static int GetFontLineSkip(IntPtr font)
    {
        return GetFontLineSkipNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontKerning"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetFontKerning(IntPtr font, [MarshalAs(UnmanagedType.I1)] bool enabled);
    private delegate void SetFontKerningNativeDelegate(IntPtr font, bool enabled);
    private static SetFontKerningNativeDelegate SetFontKerningNativeFunction = TTF_SetFontKerning;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_SetFontKerning(TTF_Font *font, bool enabled);</code>
    /// <summary>
    /// <para>Set if kerning is enabled for a font.</para>
    /// <para>Newly-opened fonts default to allowing kerning. This is generally a good
    /// policy unless you have a strong reason to disable it, as it tends to
    /// produce better rendering (with kerning disabled, some fonts might render
    /// the word <c>kerning</c> as something that looks like <c>keming</c> for example).</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to set kerning on.</param>
    /// <param name="enabled"><c>true</c> to enable kerning, <c>false</c> to disable.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static void SetFontKerning(IntPtr font, [MarshalAs(UnmanagedType.I1)] bool enabled)
    {
        SetFontKerningNativeFunction(font, enabled);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontKerning"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetFontKerning(IntPtr font);
    private delegate bool GetFontKerningNativeDelegate(IntPtr font);
    private static GetFontKerningNativeDelegate GetFontKerningNativeFunction = TTF_GetFontKerning;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetFontKerning(const TTF_Font *font);</code>
    /// <summary>
    /// Query whether or not kerning is enabled for a font.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns><c>true</c> if kerning is enabled, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontKerning"/>
    public static bool GetFontKerning(IntPtr font)
    {
        return GetFontKerningNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_FontIsFixedWidth"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_FontIsFixedWidth(IntPtr font);
    private delegate bool FontIsFixedWidthNativeDelegate(IntPtr font);
    private static FontIsFixedWidthNativeDelegate FontIsFixedWidthNativeFunction = TTF_FontIsFixedWidth;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_FontIsFixedWidth(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query whether a font is fixed-width.</para>
    /// <para>A "fixed-width" font means all glyphs are the same width across; a
    /// lowercase 'i' will be the same size across as a capital 'W', for example.
    /// This is common for terminals and text editors, and other apps that treat
    /// text as a grid. Most other things (WYSIWYG word processors, web pages, etc)
    /// are more likely to not be fixed-width in most cases.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns><c>true</c> if the font is fixed-width, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool FontIsFixedWidth(IntPtr font)
    {
        return FontIsFixedWidthNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_FontIsScalable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_FontIsScalable(IntPtr font);
    private delegate bool FontIsScalableNativeDelegate(IntPtr font);
    private static FontIsScalableNativeDelegate FontIsScalableNativeFunction = TTF_FontIsScalable;

    /// <code>extern SDL_DECLSPEC bool TTF_FontIsScalable(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query whether a font is scalable or not.</para>
    /// <para>Scalability lets us distinguish between outline and bitmap fonts.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns><c>true</c> if the font is scalable, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetFontSDF"/>
    public static bool FontIsScalable(IntPtr font)
    {
        return FontIsScalableNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontFamilyName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetFontFamilyName(IntPtr font);
    private delegate IntPtr GetFontFamilyNameNativeDelegate(IntPtr font);
    private static GetFontFamilyNameNativeDelegate GetFontFamilyNameNativeFunction = TTF_GetFontFamilyName;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL TTF_GetFontFamilyName(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query a font's family name.</para>
    /// <para>This string is dictated by the contents of the font file.</para>
    /// <para>Note that the returned string is to internal storage, and should not be
    /// modified or free'd by the caller. The string becomes invalid, with the rest
    /// of the font, when `font` is handed to <see cref="CloseFont"/>.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's family name.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static string GetFontFamilyName(IntPtr font)
    {
        var value = GetFontFamilyNameNativeFunction(font);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontStyleName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetFontStyleName(IntPtr font);
    private delegate IntPtr GetFontStyleNameNativeDelegate(IntPtr font);
    private static GetFontStyleNameNativeDelegate GetFontStyleNameNativeFunction = TTF_GetFontStyleName;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL TTF_GetFontStyleName(const TTF_Font *font);</code>
    /// <summary>
    /// <para>Query a font's style name.</para>
    /// <para>This string is dictated by the contents of the font file.</para>
    /// <para>Note that the returned string is to internal storage, and should not be
    /// modified or free'd by the caller. The string becomes invalid, with the rest
    /// of the font, when `font` is handed to <see cref="CloseFont"/>.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the font's style name.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static string GetFontStyleName(IntPtr font)
    {
        var value = GetFontStyleNameNativeFunction(font);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontDirection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontDirection(IntPtr font, Direction direction);
    private delegate bool SetFontDirectionNativeDelegate(IntPtr font, Direction direction);
    private static SetFontDirectionNativeDelegate SetFontDirectionNativeFunction = TTF_SetFontDirection;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetFontDirection(TTF_Font *font, TTF_Direction direction);</code>
    /// <summary>
    /// <para>Set the direction to be used for text shaping by a font.</para>
    /// <para>This function only supports left-to-right text shaping if SDL_ttf was not
    /// built with HarfBuzz support.</para>
    /// <para>This updates any TTF_Text objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="direction">the new direction for text to flow.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool SetFontDirection(IntPtr font, Direction direction)
    {
        return SetFontDirectionNativeFunction(font, direction);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontDirection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Direction TTF_GetFontDirection(IntPtr font);
    private delegate Direction GetFontDirectionNativeDelegate(IntPtr font);
    private static GetFontDirectionNativeDelegate GetFontDirectionNativeFunction = TTF_GetFontDirection;

    /// <code>extern SDL_DECLSPEC TTF_Direction SDLCALL TTF_GetFontDirection(TTF_Font *font);</code>
    /// <summary>
    /// <para>Get the direction to be used for text shaping by a font.</para>
    /// <para>This defaults to <see cref="Direction.Invalid"/> if it hasn't been set.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>the direction to be used for text shaping.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static Direction GetFontDirection(IntPtr font)
    {
        return GetFontDirectionNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_StringToTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_StringToTag([MarshalAs(UnmanagedType.LPUTF8Str)] string @string);
    private delegate uint StringToTagNativeDelegate(string @string);
    private static StringToTagNativeDelegate StringToTagNativeFunction = TTF_StringToTag;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL TTF_StringToTag(const char *string);</code>
    /// <summary>
    /// Convert from a 4 character string to a 32-bit tag.
    /// </summary>
    /// <param name="string">the 4 character string to convert.</param>
    /// <returns>the 32-bit representation of the string.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="TagToString"/>
    public static uint StringToTag([MarshalAs(UnmanagedType.LPUTF8Str)] string @string)
    {
        return StringToTagNativeFunction(@string);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_TagToString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_TagToString(uint tag, [MarshalAs(UnmanagedType.LPUTF8Str)] out string @string, UIntPtr size);
    private delegate void TagToStringNativeDelegate(uint tag, out string @string, UIntPtr size);
    private static TagToStringNativeDelegate TagToStringNativeFunction = TTF_TagToString;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_TagToString(Uint32 tag, char *string, size_t size);</code>
    /// <summary>
    /// Convert from a 32-bit tag to a 4 character string.
    /// </summary>
    /// <param name="tag">the 32-bit tag to convert.</param>
    /// <param name="string">a pointer filled in with the 4 character representation of
    /// the tag.</param>
    /// <param name="size">the size of the buffer pointed at by string, should be at least
    /// 4.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="StringToTag"/>
    public static void TagToString(uint tag, [MarshalAs(UnmanagedType.LPUTF8Str)] out string @string, UIntPtr size)
    {
        TagToStringNativeFunction(tag, out @string, size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontScript"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontScript(IntPtr font, uint script);
    private delegate bool SetFontScriptNativeDelegate(IntPtr font, uint script);
    private static SetFontScriptNativeDelegate SetFontScriptNativeFunction = TTF_SetFontScript;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetFontScript(TTF_Font *font, Uint32 script);</code>
    /// <summary>
    /// <para>Set the script to be used for text shaping by a font.</para>
    /// <para>This returns <c>false</c> if SDL_ttf isn't built with HarfBuzz support.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to modify.</param>
    /// <param name="script">an
    /// [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
    /// .</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread-safe.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="StringToTag"/>
    public static bool SetFontScript(IntPtr font, uint script)
    {
        return SetFontScriptNativeFunction(font, script);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetFontScript"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetFontScript(IntPtr font);
    private delegate uint GetFontScriptNativeDelegate(IntPtr font);
    private static GetFontScriptNativeDelegate GetFontScriptNativeFunction = TTF_GetFontScript;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL TTF_GetFontScript(TTF_Font *font);</code>
    /// <summary>
    /// <para>Get the script used for text shaping a font.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <returns>an
    /// [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
    /// or 0 if a script hasn't been set.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="TagToString"/>
    public static uint GetFontScript(IntPtr font)
    {
        return GetFontScriptNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphScript"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetGlyphScript(uint ch);
    private delegate uint GetGlyphScriptNativeDelegate(uint ch);
    private static GetGlyphScriptNativeDelegate GetGlyphScriptNativeFunction = TTF_GetGlyphScript;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL TTF_GetGlyphScript(Uint32 ch);</code>
    /// <summary>
    /// <para>Get the script used by a 32-bit codepoint.</para>
    /// </summary>
    /// <param name="ch">the character code to check.</param>
    /// <returns>an
    /// [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
    /// on success, or 0 on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="TagToString"/>
    public static uint GetGlyphScript(uint ch)
    {
        return GetGlyphScriptNativeFunction(ch);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetFontLanguage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontLanguage(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string? languageBcp47);
    private delegate bool SetFontLanguageNativeDelegate(IntPtr font, string? languageBcp47);
    private static SetFontLanguageNativeDelegate SetFontLanguageNativeFunction = TTF_SetFontLanguage;

    /// <code>extern SDL_DECLSPEC bool TTF_SetFontLanguage(TTF_Font *font, const char *language_bcp47);</code>
    /// <summary>
    /// <para>Set language to be used for text shaping by a font.</para>
    /// <para>If SDL_ttf was not built with HarfBuzz support, this function returns
    /// <c>false</c>.</para>
    /// <para>This updates any <see cref="TTFText"/> objects using this font.</para>
    /// </summary>
    /// <param name="font">the font to specify a language for.</param>
    /// <param name="languageBcp47">a <c>null</c>-terminated string containing the desired
    /// language's BCP47 code. Or <c>null</c> to reset the value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool SetFontLanguage(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string? languageBcp47)
    {
        return SetFontLanguageNativeFunction(font, languageBcp47);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_FontHasGlyph"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_FontHasGlyph(IntPtr font, uint ch);
    private delegate bool FontHasGlyphNativeDelegate(IntPtr font, uint ch);
    private static FontHasGlyphNativeDelegate FontHasGlyphNativeFunction = TTF_FontHasGlyph;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_FontHasGlyph(TTF_Font *font, Uint32 ch);</code>
    /// <summary>
    /// Check whether a glyph is provided by the font for a UNICODE codepoint.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <returns><c>true</c> if font provides a glyph for this character, <c>false</c> if not.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool FontHasGlyph(IntPtr font, uint ch)
    {
        return FontHasGlyphNativeFunction(font, ch);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphImage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetGlyphImage(IntPtr font, uint ch, IntPtr imageType);
    private delegate IntPtr GetGlyphImageNativeDelegate(IntPtr font, uint ch, IntPtr imageType);
    private static GetGlyphImageNativeDelegate GetGlyphImageNativeFunction = TTF_GetGlyphImage;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_GetGlyphImage(TTF_Font *font, Uint32 ch, TTF_ImageType *image_type);</code>
    /// <summary>
    /// Get the pixel image for a UNICODE codepoint.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <param name="imageType">a pointer filled in with the glyph image type, may be
    /// <c>null</c>.</param>
    /// <returns>an <see cref="SDL.Surface"/> containing the glyph, or <c>null</c> on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static IntPtr GetGlyphImage(IntPtr font, uint ch, IntPtr imageType)
    {
        return GetGlyphImageNativeFunction(font, ch, imageType);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphImage"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetGlyphImageOut(IntPtr font, uint ch, out ImageType imageType);
    private delegate IntPtr GetGlyphImageOutNativeDelegate(IntPtr font, uint ch, out ImageType imageType);
    private static GetGlyphImageOutNativeDelegate GetGlyphImageOutNativeFunction = TTF_GetGlyphImageOut;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_GetGlyphImage(TTF_Font *font, Uint32 ch, TTF_ImageType *image_type);</code>
    /// <summary>
    /// Get the pixel image for a UNICODE codepoint.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <param name="imageType">a pointer filled in with the glyph image type, may be
    /// <c>null</c>.</param>
    /// <returns>an <see cref="SDL.Surface"/> containing the glyph, or <c>null</c> on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static IntPtr GetGlyphImage(IntPtr font, uint ch, out ImageType imageType)
    {
        return GetGlyphImageOutNativeFunction(font, ch, out imageType);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphImageForIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetGlyphImageForIndex(IntPtr font, uint glyphIndex, IntPtr imageType);
    private delegate IntPtr GetGlyphImageForIndexNativeDelegate(IntPtr font, uint glyphIndex, IntPtr imageType);
    private static GetGlyphImageForIndexNativeDelegate GetGlyphImageForIndexNativeFunction = TTF_GetGlyphImageForIndex;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_GetGlyphImageForIndex(TTF_Font *font, Uint32 glyph_index, TTF_ImageType *image_type);</code>
    /// <summary>
    /// <para>Get the pixel image for a character index.</para>
    /// <para>This is useful for text engine implementations, which can call this with
    /// the <c>glyphIndex</c> in a TTF_CopyOperation</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="glyphIndex">the index of the glyph to return.</param>
    /// <param name="imageType">a pointer filled in with the glyph image type, may be
    /// <c>null</c>.</param>
    /// <returns>an <see cref="SDL.Surface"/> containing the glyph, or <c>null</c> on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static IntPtr GetGlyphImageForIndex(IntPtr font, uint glyphIndex, IntPtr imageType)
    {
        return GetGlyphImageForIndexNativeFunction(font, glyphIndex, imageType);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphImageForIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetGlyphImageForIndexOut(IntPtr font, uint glyphIndex, out ImageType imageType);
    private delegate IntPtr GetGlyphImageForIndexOutNativeDelegate(IntPtr font, uint glyphIndex, out ImageType imageType);
    private static GetGlyphImageForIndexOutNativeDelegate GetGlyphImageForIndexOutNativeFunction = TTF_GetGlyphImageForIndexOut;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_GetGlyphImageForIndex(TTF_Font *font, Uint32 glyph_index, TTF_ImageType *image_type);</code>
    /// <summary>
    /// <para>Get the pixel image for a character index.</para>
    /// <para>This is useful for text engine implementations, which can call this with
    /// the <c>glyphIndex</c> in a TTF_CopyOperation</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="glyphIndex">the index of the glyph to return.</param>
    /// <param name="imageType">a pointer filled in with the glyph image type, may be
    /// <c>null</c>.</param>
    /// <returns>an <see cref="SDL.Surface"/> containing the glyph, or <c>null</c> on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static IntPtr GetGlyphImageForIndex(IntPtr font, uint glyphIndex, out ImageType imageType)
    {
        return GetGlyphImageForIndexOutNativeFunction(font, glyphIndex, out imageType);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphMetrics"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetGlyphMetrics(IntPtr font, uint ch, out int minx, out int maxx, out int miny, out int maxy, out int advance);
    private delegate bool GetGlyphMetricsNativeDelegate(IntPtr font, uint ch, out int minx, out int maxx, out int miny, out int maxy, out int advance);
    private static GetGlyphMetricsNativeDelegate GetGlyphMetricsNativeFunction = TTF_GetGlyphMetrics;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetGlyphMetrics(TTF_Font *font, Uint32 ch, int *minx, int *maxx, int *miny, int *maxy, int *advance);</code>
    /// <summary>
    /// <para>Query the metrics (dimensions) of a font's glyph for a UNICODE codepoint.</para>
    /// <para>To understand what these metrics mean, here is a useful link:</para>
    /// <para>https://freetype.sourceforge.net/freetype2/docs/tutorial/step2.html</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="ch">the codepoint to check.</param>
    /// <param name="minx">a pointer filled in with the minimum x coordinate of the glyph
    /// from the left edge of its bounding box. This value may be
    /// negative.</param>
    /// <param name="maxx">a pointer filled in with the maximum x coordinate of the glyph
    /// from the left edge of its bounding box.</param>
    /// <param name="miny">a pointer filled in with the minimum y coordinate of the glyph
    /// from the bottom edge of its bounding box. This value may be
    /// negative.</param>
    /// <param name="maxy">a pointer filled in with the maximum y coordinate of the glyph
    /// from the bottom edge of its bounding box.</param>
    /// <param name="advance">a pointer filled in with the distance to the next glyph from
    /// the left edge of this glyph's bounding box.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetGlyphMetrics(IntPtr font, uint ch, out int minx, out int maxx, out int miny, out int maxy, out int advance)
    {
        return GetGlyphMetricsNativeFunction(font, ch, out minx, out maxx, out miny, out maxy, out advance);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGlyphKerning"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetGlyphKerning(IntPtr font, uint previousCh, uint ch, out int kerning);
    private delegate bool GetGlyphKerningNativeDelegate(IntPtr font, uint previousCh, uint ch, out int kerning);
    private static GetGlyphKerningNativeDelegate GetGlyphKerningNativeFunction = TTF_GetGlyphKerning;

    /// <code>extern SDL_DECLSPEC bool TTF_GetGlyphKerning(TTF_Font *font, Uint32 previous_ch, Uint32 ch, int *kerning);</code>
    /// <summary>
    /// Query the kerning size between the glyphs of two UNICODE codepoints.
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="previousCh">the previous codepoint.</param>
    /// <param name="ch">the current codepoint.</param>
    /// <param name="kerning">a pointer filled in with the kerning size between the two
    /// glyphs, in pixels, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetGlyphKerning(IntPtr font, uint previousCh, uint ch, out int kerning)
    {
        return GetGlyphKerningNativeFunction(font, previousCh, ch, out kerning);
    }

    #region  GetStringSize

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetStringSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetStringSize(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, out int w, out int h);
    private delegate bool GetStringSizeNativeDelegate(IntPtr font, string text, UIntPtr length, out int w, out int h);
    private static GetStringSizeNativeDelegate GetStringSizeNativeFunction = TTF_GetStringSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetStringSize(TTF_Font *font, const char *text, size_t length, int *w, int *h);</code>
    /// <summary>
    /// <para>Calculate the dimensions of a rendered string of UTF-8 text.</para>
    /// <para>This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetStringSize(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, out int w, out int h)
    {
        return GetStringSizeNativeFunction(font, text, length, out w, out h);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetStringSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool TTF_GetStringSizeBytePointer(IntPtr font, byte* text, UIntPtr length, out int w, out int h);
    private unsafe delegate bool GetStringSizeBytePointerNativeDelegate(IntPtr font, byte* text, UIntPtr length, out int w, out int h);
    private static unsafe GetStringSizeBytePointerNativeDelegate GetStringSizeBytePointerNativeFunction = TTF_GetStringSizeBytePointer;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetStringSize(TTF_Font *font, const char *text, size_t length, int *w, int *h);</code>
    /// <summary>
    /// <para>Calculate the dimensions of a rendered string of UTF-8 text.</para>
    /// <para>This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static unsafe bool GetStringSize(IntPtr font, byte* text, UIntPtr length, out int w, out int h)
    {
        return GetStringSizeBytePointerNativeFunction(font, text, length, out w, out h);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetStringSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetStringSizePointer(IntPtr font, IntPtr text, UIntPtr length, out int w, out int h);
    private delegate bool GetStringSizePointerNativeDelegate(IntPtr font, IntPtr text, UIntPtr length, out int w, out int h);
    private static GetStringSizePointerNativeDelegate GetStringSizePointerNativeFunction = TTF_GetStringSizePointer;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetStringSize(TTF_Font *font, const char *text, size_t length, int *w, int *h);</code>
    /// <summary>
    /// <para>Calculate the dimensions of a rendered string of UTF-8 text.</para>
    /// <para>This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetStringSize(IntPtr font, IntPtr text, UIntPtr length, out int w, out int h)
    {
        return GetStringSizePointerNativeFunction(font, text, length, out w, out h);
    }

    #endregion


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetStringSizeWrapped"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetStringSizeWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, int wrapWidth, out int w, out int h);
    private delegate bool GetStringSizeWrappedNativeDelegate(IntPtr font, string text, UIntPtr length, int wrapWidth, out int w, out int h);
    private static GetStringSizeWrappedNativeDelegate GetStringSizeWrappedNativeFunction = TTF_GetStringSizeWrapped;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetStringSizeWrapped(TTF_Font *font, const char *text, size_t length, int wrap_width, int *w, int *h);</code>
    /// <summary>
    /// <para>Calculate the dimensions of a rendered string of UTF-8 text.</para>
    /// <para>This will report the width and height, in pixels, of the space that the
    /// specified string will take to fully render.</para>
    /// <para>Text is wrapped to multiple lines on line endings and on word boundaries if
    /// it extends beyond <c>wrapWidth</c> in pixels.</para>
    /// <para>If wrap_width is 0, this function will only wrap on newline characters.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="wrapWidth">the maximum width or 0 to wrap on newline characters.</param>
    /// <param name="w">will be filled with width, in pixels, on return.</param>
    /// <param name="h">will be filled with height, in pixels, on return.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetStringSizeWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, int wrapWidth, out int w, out int h)
    {
        return GetStringSizeWrappedNativeFunction(font, text, length, wrapWidth, out w, out h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_MeasureString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_MeasureString(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, int maxWidth, out int measuredWidth, out ulong measuredLength);
    private delegate bool MeasureStringNativeDelegate(IntPtr font, string text, UIntPtr length, int maxWidth, out int measuredWidth, out ulong measuredLength);
    private static MeasureStringNativeDelegate MeasureStringNativeFunction = TTF_MeasureString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_MeasureString(TTF_Font *font, const char *text, size_t length, int max_width, int *measured_width, size_t *measured_length);</code>
    /// <summary>
    /// <para>Calculate how much of a UTF-8 string will fit in a given width.</para>
    /// <para>This reports the number of characters that can be rendered before reaching
    /// <c>maxWidth</c>.</para>
    /// <para>This does not need to render the string to do this calculation.</para>
    /// </summary>
    /// <param name="font">the font to query.</param>
    /// <param name="text">text to calculate, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="maxWidth">maximum width, in pixels, available for the string, or 0
    /// for unbounded width.</param>
    /// <param name="measuredWidth">a pointer filled in with the width, in pixels, of the
    /// string that will fit, may be <c>null</c>.</param>
    /// <param name="measuredLength">a pointer filled in with the length, in bytes, of
    /// the string that will fit, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool MeasureString(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, int maxWidth, out int measuredWidth, out ulong measuredLength)
    {
        return MeasureStringNativeFunction(font, text, length, maxWidth, out measuredWidth, out measuredLength);
    }

    #region RenderTextSolid

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Solid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextSolidString(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg);
    private delegate IntPtr RenderTextSolidStringNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg);
    private static RenderTextSolidStringNativeDelegate RenderTextSolidStringNativeFunction = TTF_RenderTextSolidString;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Solid(TTF_Font *font, const char *text, size_t length, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at fast quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
    /// will be set to the text color.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextSolidWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextShaded"/>,
    /// TTF_RenderText_Blended, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    public static IntPtr RenderTextSolid(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg)
    {
        return RenderTextSolidStringNativeFunction(font, text, length, fg);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Solid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextSolidPointer(IntPtr font, IntPtr text, UIntPtr length, SDL.Color fg);
    private delegate IntPtr RenderTextSolidPointerNativeDelegate(IntPtr font, IntPtr text, UIntPtr length, SDL.Color fg);
    private static RenderTextSolidPointerNativeDelegate RenderTextSolidPointerNativeFunction = TTF_RenderTextSolidPointer;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Solid(TTF_Font *font, const char *text, size_t length, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at fast quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
    /// will be set to the text color.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextSolidWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextShaded"/>,
    /// TTF_RenderText_Blended, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    public static IntPtr RenderTextSolid(IntPtr font, IntPtr text, UIntPtr length, SDL.Color fg)
    {
        return RenderTextSolidPointerNativeFunction(font, text, length, fg);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Solid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr TTF_RenderTextSolidBytePointer(IntPtr font, byte* text, UIntPtr length, SDL.Color fg);
    private unsafe delegate IntPtr RenderTextSolidBytePointerNativeDelegate(IntPtr font, byte* text, UIntPtr length, SDL.Color fg);
    private static unsafe RenderTextSolidBytePointerNativeDelegate RenderTextSolidBytePointerNativeFunction = TTF_RenderTextSolidBytePointer;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Solid(TTF_Font *font, const char *text, size_t length, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at fast quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
    /// will be set to the text color.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextSolidWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextShaded"/>,
    /// TTF_RenderText_Blended, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    public static unsafe IntPtr RenderTextSolid(IntPtr font, byte* text, UIntPtr length, SDL.Color fg)
    {
        return RenderTextSolidBytePointerNativeFunction(font, text, length, fg);
    }

    #endregion

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Solid_Wrapped"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextSolidWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, int wrapLength);
    private delegate IntPtr RenderTextSolidWrappedNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg, int wrapLength);
    private static RenderTextSolidWrappedNativeDelegate RenderTextSolidWrappedNativeFunction = TTF_RenderTextSolidWrapped;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Solid_Wrapped(TTF_Font *font, const char *text, size_t length, SDL_Color fg, int wrapLength);</code>
    /// <summary>
    /// <para>Render word-wrapped UTF-8 text at fast quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
    /// will be set to the text color.</para>
    /// <para>Text is wrapped to multiple lines on line endings and on word boundaries if
    /// it extends beyond <c>wrapLength</c> in pixels.</para>
    /// <para>If wrapLength is 0, this function will only wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextShadedWrapped"/>,
    /// <see cref="RenderTextBlendedWrapped"/>, and TTF_RenderText_LCD_Wrapped.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="wrapLength">the maximum width of the text surface or 0 to wrap on
    /// newline characters.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlendedWrapped"/>
    /// <seealso cref="RenderTextLCDWrapped"/>
    /// <seealso cref="RenderTextShadedWrapped"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    public static IntPtr RenderTextSolidWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, int wrapLength)
    {
        return RenderTextSolidWrappedNativeFunction(font, text, length, fg, wrapLength);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderGlyph_Solid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderGlyphSolid(IntPtr font, uint ch, SDL.Color fg);
    private delegate IntPtr RenderGlyphSolidNativeDelegate(IntPtr font, uint ch, SDL.Color fg);
    private static RenderGlyphSolidNativeDelegate RenderGlyphSolidNativeFunction = TTF_RenderGlyphSolid;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderGlyph_Solid(TTF_Font *font, Uint32 ch, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render a single 32-bit glyph at fast quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the colorkey, giving a transparent background. The 1 pixel
    /// will be set to the text color.</para>
    /// <para>The glyph is rendered without any padding or centering in the X direction,
    /// and aligned normally in the Y direction.</para>
    /// <para>You can render at other quality levels with <see cref="RenderGlyphShaded"/>,
    /// <see cref="RenderGlyphBlended"/>, and <see cref="RenderGlyphLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="ch">the character to render.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderGlyphBlended"/>
    /// <seealso cref="RenderGlyphLCD"/>
    /// <seealso cref="RenderGlyphShaded"/>
    public static IntPtr RenderGlyphSolid(IntPtr font, uint ch, SDL.Color fg)
    {
        return RenderGlyphSolidNativeFunction(font, ch, fg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Shaded"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextShaded(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg);
    private delegate IntPtr RenderTextShadedNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg, SDL.Color bg);
    private static RenderTextShadedNativeDelegate RenderTextShadedNativeFunction = TTF_RenderTextShaded;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Shaded(TTF_Font *font, const char *text, size_t length, SDL_Color fg, SDL_Color bg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at high quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the specified background color, while other pixels have
    /// varying degrees of the foreground color. This function returns the new
    /// surface, or <c>null</c> if there was an error.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextShadedWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>,
    /// <see cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="bg">the background color for the text.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShadedWrapped"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    public static IntPtr RenderTextShaded(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg)
    {
        return RenderTextShadedNativeFunction(font, text, length, fg, bg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Shaded_Wrapped"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextShadedWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg, int wrapWidth);
    private delegate IntPtr RenderTextShadedWrappedNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg, SDL.Color bg, int wrapWidth);
    private static RenderTextShadedWrappedNativeDelegate RenderTextShadedWrappedNativeFunction = TTF_RenderTextShadedWrapped;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Shaded_Wrapped(TTF_Font *font, const char *text, size_t length, SDL_Color fg, SDL_Color bg, int wrap_width);</code>
    /// <summary>
    /// <para>Render word-wrapped UTF-8 text at high quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the specified background color, while other pixels have
    /// varying degrees of the foreground color. This function returns the new
    /// surface, or <c>null</c> if there was an error.</para>
    /// <para>Text is wrapped to multiple lines on line endings and on word boundaries if
    /// it extends beyond <c>wrapWidth</c> in pixels.</para>
    /// <para>If wrap_width is 0, this function will only wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolidWrapped"/>,
    /// <see cref="RenderTextBlendedWrapped"/>, and <see cref="RenderTextLCDWrapped"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="bg">the background color for the text.</param>
    /// <param name="wrapWidth">the maximum width of the text surface or 0 to wrap on
    /// newline characters.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    public static IntPtr RenderTextShadedWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg, int wrapWidth)
    {
        return RenderTextShadedWrappedNativeFunction(font, text, length, fg, bg, wrapWidth);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderGlyph_Shaded"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderGlyphShaded(IntPtr font, uint ch, SDL.Color fg, SDL.Color bg);
    private delegate IntPtr RenderGlyphShadedNativeDelegate(IntPtr font, uint ch, SDL.Color fg, SDL.Color bg);
    private static RenderGlyphShadedNativeDelegate RenderGlyphShadedNativeFunction = TTF_RenderGlyphShaded;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderGlyph_Shaded(TTF_Font *font, Uint32 ch, SDL_Color fg, SDL_Color bg);</code>
    /// <summary>
    /// <para>Render a single UNICODE codepoint at high quality to a new 8-bit surface.</para>
    /// <para>This function will allocate a new 8-bit, palettized surface. The surface's
    /// 0 pixel will be the specified background color, while other pixels have
    /// varying degrees of the foreground color. This function returns the new
    /// surface, or <c>null</c> if there was an error.</para>
    /// <para>The glyph is rendered without any padding or centering in the X direction,
    /// and aligned normally in the Y direction.</para>
    /// <para>You can render at other quality levels with <see cref="RenderGlyphSolid"/>,
    /// <see cref="RenderGlyphBlended"/>, and <see cref="RenderGlyphLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="ch">the codepoint to render.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="bg">the background color for the text.</param>
    /// <returns>a new 8-bit, palettized surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderGlyphBlended"/>
    /// <seealso cref="RenderGlyphLCD"/>
    /// <seealso cref="RenderGlyphSolid"/>
    public static IntPtr RenderGlyphShaded(IntPtr font, uint ch, SDL.Color fg, SDL.Color bg)
    {
        return RenderGlyphShadedNativeFunction(font, ch, fg, bg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Blended"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextBlendedString(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg);
    private delegate IntPtr RenderTextBlendedStringNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg);
    private static RenderTextBlendedStringNativeDelegate RenderTextBlendedStringNativeFunction = TTF_RenderTextBlendedString;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Blended(TTF_Font *font, const char *text, size_t length, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at high quality to a new ARGB surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, using alpha
    /// blending to dither the font with the given color. This function returns the
    /// new surface, or <c>null</c> if there was an error.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextBlendedWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>,
    /// <see cref="RenderTextShaded"/>, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlendedWrapped"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    public static IntPtr RenderTextBlended(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg)
    {
        return RenderTextBlendedStringNativeFunction(font, text, length, fg);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Blended"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextBlendedPointer(IntPtr font, IntPtr text, UIntPtr length, SDL.Color fg);
    private delegate IntPtr RenderTextBlendedPointerNativeDelegate(IntPtr font, IntPtr text, UIntPtr length, SDL.Color fg);
    private static RenderTextBlendedPointerNativeDelegate RenderTextBlendedPointerNativeFunction = TTF_RenderTextBlendedPointer;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Blended(TTF_Font *font, const char *text, size_t length, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at high quality to a new ARGB surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, using alpha
    /// blending to dither the font with the given color. This function returns the
    /// new surface, or <c>null</c> if there was an error.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextBlendedWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>,
    /// <see cref="RenderTextShaded"/>, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlendedWrapped"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    public static IntPtr RenderTextBlended(IntPtr font, IntPtr text, UIntPtr length, SDL.Color fg)
    {
        return RenderTextBlendedPointerNativeFunction(font, text, length, fg);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Blended"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr TTF_RenderTextBlendedBytePointer(IntPtr font, byte* text, UIntPtr length, SDL.Color fg);
    private unsafe delegate IntPtr RenderTextBlendedBytePointerNativeDelegate(IntPtr font, byte* text, UIntPtr length, SDL.Color fg);
    private static unsafe RenderTextBlendedBytePointerNativeDelegate RenderTextBlendedBytePointerNativeFunction = TTF_RenderTextBlendedBytePointer;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Blended(TTF_Font *font, const char *text, size_t length, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at high quality to a new ARGB surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, using alpha
    /// blending to dither the font with the given color. This function returns the
    /// new surface, or <c>null</c> if there was an error.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextBlendedWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>,
    /// <see cref="RenderTextShaded"/>, and <see cref="RenderTextLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlendedWrapped"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    public static unsafe IntPtr RenderTextBlended(IntPtr font, byte* text, UIntPtr length, SDL.Color fg)
    {
        return RenderTextBlendedBytePointerNativeFunction(font, text, length, fg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_Blended_Wrapped"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextBlendedWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, int wrapWidth);
    private delegate IntPtr RenderTextBlendedWrappedNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg, int wrapWidth);
    private static RenderTextBlendedWrappedNativeDelegate RenderTextBlendedWrappedNativeFunction = TTF_RenderTextBlendedWrapped;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_Blended_Wrapped(TTF_Font *font, const char *text, size_t length, SDL_Color fg, int wrap_width);</code>
    /// <summary>
    /// <para>Render word-wrapped UTF-8 text at high quality to a new ARGB surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, using alpha
    /// blending to dither the font with the given color. This function returns the
    /// new surface, or <c>null</c> if there was an error.</para>
    /// <para>Text is wrapped to multiple lines on line endings and on word boundaries if
    /// it extends beyond <c>wrapWidth</c> in pixels.</para>
    /// <para>If wrap_width is 0, this function will only wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolidWrapped"/>,
    /// <see cref="RenderTextShadedWrapped"/>, and <see cref="RenderTextLCDWrapped"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="wrapWidth">the maximum width of the text surface or 0 to wrap on
    /// newline characters.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextLCDWrapped"/>
    /// <seealso cref="RenderTextShadedWrapped"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    public static IntPtr RenderTextBlendedWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, int wrapWidth)
    {
        return RenderTextBlendedWrappedNativeFunction(font, text, length, fg, wrapWidth);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderGlyph_Blended"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderGlyphBlended(IntPtr font, ulong ch, SDL.Color fg);
    private delegate IntPtr RenderGlyphBlendedNativeDelegate(IntPtr font, ulong ch, SDL.Color fg);
    private static RenderGlyphBlendedNativeDelegate RenderGlyphBlendedNativeFunction = TTF_RenderGlyphBlended;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderGlyph_Blended(TTF_Font *font, Uint32 ch, SDL_Color fg);</code>
    /// <summary>
    /// <para>Render a single UNICODE codepoint at high quality to a new ARGB surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, using alpha
    /// blending to dither the font with the given color. This function returns the
    /// new surface, or <c>null</c> if there was an error.</para>
    /// <para>The glyph is rendered without any padding or centering in the X direction,
    /// and aligned normally in the Y direction.</para>
    /// <para>You can render at other quality levels with <see cref="RenderGlyphSolid"/>,
    /// <see cref="RenderGlyphShaded"/>, and <see cref="RenderGlyphLCD"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="ch">the codepoint to render.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderGlyphLCD"/>
    /// <seealso cref="RenderGlyphShaded"/>
    /// <seealso cref="RenderGlyphSolid"/>
    public static IntPtr RenderGlyphBlended(IntPtr font, ulong ch, SDL.Color fg)
    {
        return RenderGlyphBlendedNativeFunction(font, ch, fg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_LCD"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextLCD(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg);
    private delegate IntPtr RenderTextLCDNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg, SDL.Color bg);
    private static RenderTextLCDNativeDelegate RenderTextLCDNativeFunction = TTF_RenderTextLCD;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_LCD(TTF_Font *font, const char *text, size_t length, SDL_Color fg, SDL_Color bg);</code>
    /// <summary>
    /// <para>Render UTF-8 text at LCD subpixel quality to a new ARGB surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, and render
    /// alpha-blended text using FreeType's LCD subpixel rendering. This function
    /// returns the new surface, or <c>null</c> if there was an error.</para>
    /// <para>This will not word-wrap the string; you'll get a surface with a single line
    /// of text, as long as the string requires. You can use
    /// <see cref="RenderTextLCDWrapped"/> instead if you need to wrap the output to
    /// multiple lines.</para>
    /// <para>This will not wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>,
    /// <see cref="RenderTextShaded"/>, and <see cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="bg">the background color for the text.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlended(IntPtr, string, UIntPtr, SDL.Color)"/>
    /// <seealso cref="RenderTextLCDWrapped"/>
    /// <seealso cref="RenderTextShaded"/>
    /// <seealso cref="RenderTextSolid(IntPtr, string, UIntPtr, SDL.Color)"/>
    public static IntPtr RenderTextLCD(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg)
    {
        return RenderTextLCDNativeFunction(font, text, length, fg, bg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderText_LCD_Wrapped"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderTextLCDWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg, int wrapWidth);
    private delegate IntPtr RenderTextLCDWrappedNativeDelegate(IntPtr font, string text, UIntPtr length, SDL.Color fg, SDL.Color bg, int wrapWidth);
    private static RenderTextLCDWrappedNativeDelegate RenderTextLCDWrappedNativeFunction = TTF_RenderTextLCDWrapped;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderText_LCD_Wrapped(TTF_Font *font, const char *text, size_t length, SDL_Color fg, SDL_Color bg, int wrap_width);</code>
    /// <summary>
    /// <para>Render word-wrapped UTF-8 text at LCD subpixel quality to a new ARGB
    /// surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, and render
    /// alpha-blended text using FreeType's LCD subpixel rendering. This function
    /// returns the new surface, or <c>null</c> if there was an error.</para>
    /// <para>Text is wrapped to multiple lines on line endings and on word boundaries if
    /// it extends beyond <c>wrapWidth</c> in pixels.</para>
    /// <para>If <c>wrapWidth</c> is 0, this function will only wrap on newline characters.</para>
    /// <para>You can render at other quality levels with <see cref="RenderTextSolidWrapped"/>,
    /// <see cref="RenderTextShadedWrapped"/>, and <see cref="RenderTextBlendedWrapped"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">text to render, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="bg">the background color for the text.</param>
    /// <param name="wrapWidth">the maximum width of the text surface or 0 to wrap on
    /// newline characters.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderTextBlendedWrapped"/>
    /// <seealso cref="RenderTextLCD"/>
    /// <seealso cref="RenderTextShadedWrapped"/>
    /// <seealso cref="RenderTextSolidWrapped"/>
    public static IntPtr RenderTextLCDWrapped(IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length, SDL.Color fg, SDL.Color bg, int wrapWidth)
    {
        return RenderTextLCDWrappedNativeFunction(font, text, length, fg, bg, wrapWidth);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_RenderGlyph_LCD"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_RenderGlyphLCD(IntPtr font, uint ch, SDL.Color fg, SDL.Color bg);
    private delegate IntPtr RenderGlyphLCDNativeDelegate(IntPtr font, uint ch, SDL.Color fg, SDL.Color bg);
    private static RenderGlyphLCDNativeDelegate RenderGlyphLCDNativeFunction = TTF_RenderGlyphLCD;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL TTF_RenderGlyph_LCD(TTF_Font *font, Uint32 ch, SDL_Color fg, SDL_Color bg);</code>
    /// <summary>
    /// <para>Render a single UNICODE codepoint at LCD subpixel quality to a new ARGB
    /// surface.</para>
    /// <para>This function will allocate a new 32-bit, ARGB surface, and render
    /// alpha-blended text using FreeType's LCD subpixel rendering. This function
    /// returns the new surface, or <c>null</c> if there was an error.</para>
    /// <para>The glyph is rendered without any padding or centering in the X direction,
    /// and aligned normally in the Y direction.</para>
    /// <para>You can render at other quality levels with <see cref="RenderGlyphSolid"/>,
    /// <see cref="RenderGlyphShaded"/>, and <see cref="RenderGlyphBlended"/>.</para>
    /// </summary>
    /// <param name="font">the font to render with.</param>
    /// <param name="ch">the codepoint to render.</param>
    /// <param name="fg">the foreground color for the text.</param>
    /// <param name="bg">the background color for the text.</param>
    /// <returns>a new 32-bit, ARGB surface, or <c>null</c> if there was an error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="RenderGlyphBlended"/>
    /// <seealso cref="RenderGlyphShaded"/>
    /// <seealso cref="RenderGlyphSolid"/>
    public static IntPtr RenderGlyphLCD(IntPtr font, uint ch, SDL.Color fg, SDL.Color bg)
    {
        return RenderGlyphLCDNativeFunction(font, ch, fg, bg);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CreateSurfaceTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CreateSurfaceTextEngine();
    private delegate IntPtr CreateSurfaceTextEngineNativeDelegate();
    private static CreateSurfaceTextEngineNativeDelegate CreateSurfaceTextEngineNativeFunction = TTF_CreateSurfaceTextEngine;

    /// <code>extern SDL_DECLSPEC TTF_TextEngine * SDLCALL TTF_CreateSurfaceTextEngine(void);</code>
    /// <summary>
    /// Create a text engine for drawing text on SDL surfaces.
    /// </summary>
    /// <returns>a TTF_TextEngine object or <c>null</c> on failure; call <see cref="SDL.GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="DestroySurfaceTextEngine"/>
    /// <seealso cref="DrawSurfaceText"/>
    public static IntPtr CreateSurfaceTextEngine()
    {
        return CreateSurfaceTextEngineNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DrawSurfaceText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_DrawSurfaceText(IntPtr text, int x, int y, IntPtr surface);
    private delegate bool DrawSurfaceTextNativeDelegate(IntPtr text, int x, int y, IntPtr surface);
    private static DrawSurfaceTextNativeDelegate DrawSurfaceTextNativeFunction = TTF_DrawSurfaceText;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_DrawSurfaceText(TTF_Text *text, int x, int y, SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Draw text to an SDL surface.</para>
    /// <para><c>text</c> must have been created using a TTF_TextEngine from
    /// <see cref="CreateSurfaceTextEngine"/>.</para>
    /// </summary>
    /// <param name="text">the text to draw.</param>
    /// <param name="x">the x coordinate in pixels, positive from the left edge towards
    /// the right.</param>
    /// <param name="y">the y coordinate in pixels, positive from the top edge towards the
    /// bottom.</param>
    /// <param name="surface">the surface to draw on.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateSurfaceTextEngine"/>
    /// <seealso cref="CreateText"/>
    public static bool DrawSurfaceText(IntPtr text, int x, int y, IntPtr surface)
    {
        return DrawSurfaceTextNativeFunction(text, x, y, surface);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DestroySurfaceTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroySurfaceTextEngine(IntPtr engine);
    private delegate void DestroySurfaceTextEngineNativeDelegate(IntPtr engine);
    private static DestroySurfaceTextEngineNativeDelegate DestroySurfaceTextEngineNativeFunction = TTF_DestroySurfaceTextEngine;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_DestroySurfaceTextEngine(TTF_TextEngine *engine);</code>
    /// <summary>
    /// <para>Destroy a text engine created for drawing text on SDL surfaces.</para>
    /// <para>All text created by this engine should be destroyed before calling this
    /// function.</para>
    /// </summary>
    /// <param name="engine">a TTF_TextEngine object created with
    /// <see cref="CreateSurfaceTextEngine"/>.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// engine.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateSurfaceTextEngine"/>
    public static void DestroySurfaceTextEngine(IntPtr engine)
    {
        DestroySurfaceTextEngineNativeFunction(engine);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CreateRendererTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CreateRendererTextEngine(IntPtr renderer);
    private delegate IntPtr CreateRendererTextEngineNativeDelegate(IntPtr renderer);
    private static CreateRendererTextEngineNativeDelegate CreateRendererTextEngineNativeFunction = TTF_CreateRendererTextEngine;

    /// <code>extern SDL_DECLSPEC TTF_TextEngine * SDLCALL TTF_CreateRendererTextEngine(SDL_Renderer *renderer);</code>
    /// <summary>
    /// Create a text engine for drawing text on an SDL renderer.
    /// </summary>
    /// <param name="renderer">the renderer to use for creating textures and drawing text.</param>
    /// <returns>a TTF_TextEngine object or <c>null</c> on failure; call <see cref="SDL.GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// renderer.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="DestroyRendererTextEngine"/>
    /// <seealso cref="DrawRendererText"/>
    /// <seealso cref="CreateRendererTextEngineWithProperties"/>
    public static IntPtr CreateRendererTextEngine(IntPtr renderer)
    {
        return CreateRendererTextEngineNativeFunction(renderer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CreateRendererTextEngineWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CreateRendererTextEngineWithProperties(uint props);
    private delegate IntPtr CreateRendererTextEngineWithPropertiesNativeDelegate(uint props);
    private static CreateRendererTextEngineWithPropertiesNativeDelegate CreateRendererTextEngineWithPropertiesNativeFunction = TTF_CreateRendererTextEngineWithProperties;

    /// <code>extern SDL_DECLSPEC TTF_TextEngine * SDLCALL TTF_CreateRendererTextEngineWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a text engine for drawing text on an SDL renderer, with the
    /// specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererTextEngineRenderer"/>: the renderer to use for
    /// creating textures and drawing text</item>
    /// <item><see cref="Props.RendererTextEngineAtlasTextureSize"/>: the size of the
    /// texture atlas</item>
    /// </list>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>a TTF_TextEngine object or <c>null</c> on failure; call <see cref="SDL.GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// renderer.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateRendererTextEngine"/>
    /// <seealso cref="DestroyRendererTextEngine"/>
    /// <seealso cref="DrawRendererText"/>
    public static IntPtr CreateRendererTextEngineWithProperties(uint props)
    {
        return CreateRendererTextEngineWithPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DrawRendererText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_DrawRendererText(IntPtr text, float x, float y);
    private delegate bool DrawRendererTextNativeDelegate(IntPtr text, float x, float y);
    private static DrawRendererTextNativeDelegate DrawRendererTextNativeFunction = TTF_DrawRendererText;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_DrawRendererText(TTF_Text *text, float x, float y);</code>
    /// <summary>
    /// <para>Draw text to an SDL renderer.</para>
    /// <para><c>text</c> must have been created using a TTF_TextEngine from
    /// <see cref="CreateRendererTextEngine"/>, and will draw using the renderer passed to
    /// that function.</para>
    /// </summary>
    /// <param name="text">the text to draw.</param>
    /// <param name="x">the x coordinate in pixels, positive from the left edge towards
    /// the right.</param>
    /// <param name="y">the y coordinate in pixels, positive from the top edge towards the
    /// bottom.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateRendererTextEngine"/>
    /// <seealso cref="CreateText"/>
    public static bool DrawRendererText(IntPtr text, float x, float y)
    {
        return DrawRendererTextNativeFunction(text, x, y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DestroyRendererTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyRendererTextEngine(IntPtr engine);
    private delegate void DestroyRendererTextEngineNativeDelegate(IntPtr engine);
    private static DestroyRendererTextEngineNativeDelegate DestroyRendererTextEngineNativeFunction = TTF_DestroyRendererTextEngine;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_DestroyRendererTextEngine(TTF_TextEngine *engine);</code>
    /// <summary>
    /// <para>Destroy a text engine created for drawing text on an SDL renderer.</para>
    /// <para>All text created by this engine should be destroyed before calling this
    /// function.</para>
    /// </summary>
    /// <param name="engine">a TTF_TextEngine object created with
    /// <see cref="CreateRendererTextEngine"/>.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// engine.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateRendererTextEngine"/>
    public static void DestroyRendererTextEngine(IntPtr engine)
    {
        DestroyRendererTextEngineNativeFunction(engine);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CreateGPUTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CreateGPUTextEngine(IntPtr device);
    private delegate IntPtr CreateGPUTextEngineNativeDelegate(IntPtr device);
    private static CreateGPUTextEngineNativeDelegate CreateGPUTextEngineNativeFunction = TTF_CreateGPUTextEngine;

    /// <code>extern SDL_DECLSPEC TTF_TextEngine * SDLCALL TTF_CreateGPUTextEngine(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Create a text engine for drawing text with the SDL GPU API.
    /// </summary>
    /// <param name="device">the SDL_GPUDevice to use for creating textures and drawing
    /// text.</param>
    /// <returns>a TTF_TextEngine object or <c>null</c> on failure; call <see cref="SDL.GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// device.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateGPUTextEngineWithProperties"/>
    /// <seealso cref="DestroyGPUTextEngine"/>
    /// <seealso cref="GetGPUTextDrawData"/>
    public static IntPtr CreateGPUTextEngine(IntPtr device)
    {
        return CreateGPUTextEngineNativeFunction(device);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CreateGPUTextEngineWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CreateGPUTextEngineWithProperties(uint props);
    private delegate IntPtr CreateGPUTextEngineWithPropertiesNativeDelegate(uint props);
    private static CreateGPUTextEngineWithPropertiesNativeDelegate CreateGPUTextEngineWithPropertiesNativeFunction = TTF_CreateGPUTextEngineWithProperties;

    /// <code>extern SDL_DECLSPEC TTF_TextEngine * SDLCALL TTF_CreateGPUTextEngineWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a text engine for drawing text with the SDL GPU API, with the
    /// specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUTextEngineDevice"/>: the SDL_GPUDevice to use for creating
    /// textures and drawing text.</item>
    /// <item><see cref="Props.GPUTextEngineAtlasTextureSize"/>: the size of the texture
    /// atlas</item>
    /// </list>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>a TTF_TextEngine object or <c>null</c> on failure; call <see cref="SDL.GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// device.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateGPUTextEngine"/>
    /// <seealso cref="DestroyGPUTextEngine"/>
    /// <seealso cref="GetGPUTextDrawData"/>
    public static IntPtr CreateGPUTextEngineWithProperties(uint props)
    {
        return CreateGPUTextEngineWithPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGPUTextDrawData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetGPUTextDrawData(IntPtr text);
    private delegate IntPtr GetGPUTextDrawDataNativeDelegate(IntPtr text);
    private static GetGPUTextDrawDataNativeDelegate GetGPUTextDrawDataNativeFunction = TTF_GetGPUTextDrawData;

    /// <code>extern SDL_DECLSPEC TTF_GPUAtlasDrawSequence * SDLCALL TTF_GetGPUTextDrawData(TTF_Text *text);</code>
    /// <summary>
    /// <para>Get the geometry data needed for drawing the text.</para>
    /// <para><c>text</c> must have been created using a TTF_TextEngine from
    /// <see cref="CreateGPUTextEngine"/>.</para>
    /// <para>The positive X-axis is taken towards the right and the positive Y-axis is
    /// taken upwards for both the vertex and the texture coordinates, i.e, it
    /// follows the same convention used by the SDL_GPU API. If you want to use a
    /// different coordinate system you will need to transform the vertices
    /// yourself.</para>
    /// <para>If the text looks blocky use linear filtering.</para>
    /// </summary>
    /// <param name="text">the text to draw.</param>
    /// <returns>a <c>null</c> terminated linked list of <see cref="GPUAtlasDrawSequence"/> objects
    /// or <c>null</c> if the passed text is empty or in case of failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateGPUTextEngine"/>
    /// <seealso cref="CreateText"/>
    public static IntPtr GetGPUTextDrawData(IntPtr text)
    {
        return GetGPUTextDrawDataNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DestroyGPUTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyGPUTextEngine(IntPtr engine);
    private delegate void DestroyGPUTextEngineNativeDelegate(IntPtr engine);
    private static DestroyGPUTextEngineNativeDelegate DestroyGPUTextEngineNativeFunction = TTF_DestroyGPUTextEngine;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_DestroyGPUTextEngine(TTF_TextEngine *engine);</code>
    /// <summary>
    /// <para>Destroy a text engine created for drawing text with the SDL GPU API.</para>
    /// <para>All text created by this engine should be destroyed before calling this
    /// function.</para>
    /// </summary>
    /// <param name="engine">a TTF_TextEngine object created with
    /// <see cref="CreateGPUTextEngine"/>.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// engine.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateGPUTextEngine"/>
    public static void DestroyGPUTextEngine(IntPtr engine)
    {
        DestroyGPUTextEngineNativeFunction(engine);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetGPUTextEngineWinding"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_SetGPUTextEngineWinding(IntPtr engine, GPUTextEngineWinding winding);
    private delegate void SetGPUTextEngineWindingNativeDelegate(IntPtr engine, GPUTextEngineWinding winding);
    private static SetGPUTextEngineWindingNativeDelegate SetGPUTextEngineWindingNativeFunction = TTF_SetGPUTextEngineWinding;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_SetGPUTextEngineWinding(TTF_TextEngine *engine, TTF_GPUTextEngineWinding winding);</code>
    /// <summary>
    /// Sets the winding order of the vertices returned by <see cref="GetGPUTextDrawData"/>
    /// for a particular GPU text engine.
    /// </summary>
    /// <param name="engine">a TTF_TextEngine object created with
    /// <see cref="CreateGPUTextEngine"/>.</param>
    /// <param name="winding">the new winding order option.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// engine.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetGPUTextEngineWinding"/>
    public static void SetGPUTextEngineWinding(IntPtr engine, GPUTextEngineWinding winding)
    {
        SetGPUTextEngineWindingNativeFunction(engine, winding);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetGPUTextEngineWinding"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GPUTextEngineWinding TTF_GetGPUTextEngineWinding(IntPtr engine);
    private delegate GPUTextEngineWinding GetGPUTextEngineWindingNativeDelegate(IntPtr engine);
    private static GetGPUTextEngineWindingNativeDelegate GetGPUTextEngineWindingNativeFunction = TTF_GetGPUTextEngineWinding;

    /// <code>extern SDL_DECLSPEC TTF_GPUTextEngineWinding SDLCALL TTF_GetGPUTextEngineWinding(const TTF_TextEngine *engine);</code>
    /// <summary>
    /// <para>Get the winding order of the vertices returned by <see cref="GetGPUTextDrawData"/>
    /// for a particular GPU text engine</para>
    /// </summary>
    /// <param name="engine">a TTF_TextEngine object created with
    /// <see cref="CreateGPUTextEngine"/>.</param>
    /// <returns>the winding order used by the GPU text engine or
    /// <see cref="GPUTextEngineWinding.Invalid"/> in case of error.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// engine.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetGPUTextEngineWinding"/>
    public static GPUTextEngineWinding GetGPUTextEngineWinding(IntPtr engine)
    {
        return GetGPUTextEngineWindingNativeFunction(engine);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CreateText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_CreateText(IntPtr engine, IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length);
    private delegate IntPtr CreateTextNativeDelegate(IntPtr engine, IntPtr font, string text, UIntPtr length);
    private static CreateTextNativeDelegate CreateTextNativeFunction = TTF_CreateText;

    /// <code>extern SDL_DECLSPEC TTF_Text * SDLCALL TTF_CreateText(TTF_TextEngine *engine, TTF_Font *font, const char *text, size_t length);</code>
    /// <summary>
    /// Create a text object from UTF-8 text and a text engine.
    /// </summary>
    /// <param name="engine">the text engine to use when creating the text object, may be
    /// <c>null</c>.</param>
    /// <param name="font">the font to render with.</param>
    /// <param name="text">the text to use, in UTF-8 encoding.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <returns>a <see cref="TTFText"/> object or <c>null</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// font and text engine.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="DestroyText"/>
    public static IntPtr CreateText(IntPtr engine, IntPtr font, [MarshalAs(UnmanagedType.LPUTF8Str)] string text, UIntPtr length)
    {
        return CreateTextNativeFunction(engine, font, text, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetTextProperties(IntPtr text);
    private delegate uint GetTextPropertiesNativeDelegate(IntPtr text);
    private static GetTextPropertiesNativeDelegate GetTextPropertiesNativeFunction = TTF_GetTextProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL TTF_GetTextProperties(TTF_Text *text);</code>
    /// <summary>
    /// Get the properties associated with a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static uint GetTextProperties(IntPtr text)
    {
        return GetTextPropertiesNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextEngine(IntPtr text, IntPtr engine);
    private delegate bool SetTextEngineNativeDelegate(IntPtr text, IntPtr engine);
    private static SetTextEngineNativeDelegate SetTextEngineNativeFunction = TTF_SetTextEngine;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextEngine(TTF_Text *text, TTF_TextEngine *engine);</code>
    /// <summary>
    /// Set the text engine used by a text object.
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="engine">the text engine to use for drawing.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextEngine"/>
    public static bool SetTextEngine(IntPtr text, IntPtr engine)
    {
        return SetTextEngineNativeFunction(text, engine);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextEngine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetTextEngine(IntPtr text);
    private delegate IntPtr GetTextEngineNativeDelegate(IntPtr text);
    private static GetTextEngineNativeDelegate GetTextEngineNativeFunction = TTF_GetTextEngine;

    /// <code>extern SDL_DECLSPEC TTF_TextEngine * SDLCALL TTF_GetTextEngine(TTF_Text *text);</code>
    /// <summary>
    /// Get the text engine used by a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <returns>the TTF_TextEngine used by the text on success or <c>null</c> on failure;
    /// call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetTextEngine"/>
    public static IntPtr GetTextEngine(IntPtr text)
    {
        return GetTextEngineNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextFont(IntPtr text, IntPtr font);
    private delegate bool SetTextFontNativeDelegate(IntPtr text, IntPtr font);
    private static SetTextFontNativeDelegate SetTextFontNativeFunction = TTF_SetTextFont;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextFont(TTF_Text *text, TTF_Font *font);</code>
    /// <summary>
    /// <para>Set the font used by a text object.</para>
    /// <para>When a text object has a font, any changes to the font will automatically
    /// regenerate the text. If you set the font to <c>null</c>, the text will continue to
    /// render but changes to the font will no longer affect the text.</para>
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="font">the font to use, may be <c>null</c>.</param>
    /// <returns><c>false</c> if the <paramref name="text"/> pointer is <c>null</c>;
    /// otherwise, <c>true</c>. call <see cref="SDL.GetError"/> for more
    /// information</returns>
    public static bool SetTextFont(IntPtr text, IntPtr font)
    {
        return SetTextFontNativeFunction(text, font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetTextFont(IntPtr text);
    private delegate IntPtr GetTextFontNativeDelegate(IntPtr text);
    private static GetTextFontNativeDelegate GetTextFontNativeFunction = TTF_GetTextFont;

    /// <code>extern SDL_DECLSPEC TTF_Font * SDLCALL TTF_GetTextFont(TTF_Text *text);</code>
    /// <summary>
    /// Get the font used by a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <returns>the TTF_Font used by the text on success or <c>null</c> on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetTextFont"/>
    public static IntPtr GetTextFont(IntPtr text)
    {
        return GetTextFontNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextDirection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextDirection(IntPtr text, Direction direction);
    private delegate bool SetTextDirectionNativeDelegate(IntPtr text, Direction direction);
    private static SetTextDirectionNativeDelegate SetTextDirectionNativeFunction = TTF_SetTextDirection;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextDirection(TTF_Text *text, TTF_Direction direction);</code>
    /// <summary>
    /// <para>Set the direction to be used for text shaping a text object.</para>
    /// <para>This function only supports left-to-right text shaping if SDL_ttf was not
    /// built with HarfBuzz support.</para>
    /// </summary>
    /// <param name="text">the text to modify.</param>
    /// <param name="direction">the new direction for text to flow.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool SetTextDirection(IntPtr text, Direction direction)
    {
        return SetTextDirectionNativeFunction(text, direction);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextDirection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Direction TTF_GetTextDirection(IntPtr text);
    private delegate Direction GetTextDirectionNativeDelegate(IntPtr text);
    private static GetTextDirectionNativeDelegate GetTextDirectionNativeFunction = TTF_GetTextDirection;

    /// <code>extern SDL_DECLSPEC TTF_Direction SDLCALL TTF_GetTextDirection(TTF_Text *text);</code>
    /// <summary>
    /// <para>Get the direction to be used for text shaping a text object.</para>
    /// <para>This defaults to the direction of the font used by the text object.</para>
    /// </summary>
    /// <param name="text">the text to query.</param>
    /// <returns>the direction to be used for text shaping.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static Direction GetTextDirection(IntPtr text)
    {
        return GetTextDirectionNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextScript"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextScript(IntPtr text, uint script);
    private delegate bool SetTextScriptNativeDelegate(IntPtr text, uint script);
    private static SetTextScriptNativeDelegate SetTextScriptNativeFunction = TTF_SetTextScript;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextScript(TTF_Text *text, Uint32 script);</code>
    /// <summary>
    /// <para>Set the script to be used for text shaping a text object.</para>
    /// <para>TThis returns <c>false</c> if SDL_ttf isn't built with HarfBuzz support.</para>
    /// </summary>
    /// <param name="text">an
    /// [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
    /// .</param>
    /// <param name="script">a script tag in the format used by HarfBuzz.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="StringToTag"/>
    public static bool SetTextScript(IntPtr text, uint script)
    {
        return SetTextScriptNativeFunction(text, script);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextScript"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint TTF_GetTextScript(IntPtr text);
    private delegate uint GetTextScriptNativeDelegate(IntPtr text);
    private static GetTextScriptNativeDelegate GetTextScriptNativeFunction = TTF_GetTextScript;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL TTF_GetTextScript(TTF_Text *text);</code>
    /// <summary>
    /// <para>Get the script used for text shaping a text object.</para>
    /// <para>This defaults to the script of the font used by the text object.</para>
    /// </summary>
    /// <param name="text">the text to query.</param>
    /// <returns>an
    /// [ISO 15924 code](https://unicode.org/iso15924/iso15924-codes.html)
    /// or 0 if a script hasn't been set on either the text object or the
    /// font.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="TagToString"/>
    public static uint GetTextScript(IntPtr text)
    {
        return GetTextScriptNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextColor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextColor(IntPtr text, byte r, byte g, byte b, byte a);
    private delegate bool SetTextColorNativeDelegate(IntPtr text, byte r, byte g, byte b, byte a);
    private static SetTextColorNativeDelegate SetTextColorNativeFunction = TTF_SetTextColor;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextColor(TTF_Text *text, Uint8 r, Uint8 g, Uint8 b, Uint8 a);</code>
    /// <summary>
    /// <para>Set the color of a text object.</para>
    /// <para>The default text color is white (255, 255, 255, 255).</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="r">the red color value in the range of 0-255.</param>
    /// <param name="g">the green color value in the range of 0-255.</param>
    /// <param name="b">the blue color value in the range of 0-255.</param>
    /// <param name="a">the alpha value in the range of 0-255.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextColor"/>
    /// <seealso cref="SetTextColorFloat"/>
    public static bool SetTextColor(IntPtr text, byte r, byte g, byte b, byte a)
    {
        return SetTextColorNativeFunction(text, r, g, b, a);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextColorFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextColorFloat(IntPtr text, float r, float g, float b, float a);
    private delegate bool SetTextColorFloatNativeDelegate(IntPtr text, float r, float g, float b, float a);
    private static SetTextColorFloatNativeDelegate SetTextColorFloatNativeFunction = TTF_SetTextColorFloat;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextColorFloat(TTF_Text *text, float r, float g, float b, float a);</code>
    /// <summary>
    /// <para>Set the color of a text object.</para>
    /// <para>The default text color is white (1.0f, 1.0f, 1.0f, 1.0f).</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="r">the red color value, normally in the range of 0-1.</param>
    /// <param name="g">the green color value, normally in the range of 0-1.</param>
    /// <param name="b">the blue color value, normally in the range of 0-1.</param>
    /// <param name="a">the alpha value in the range of 0-1.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextColorFloat"/>
    /// <seealso cref="SetTextColor"/>
    public static bool SetTextColorFloat(IntPtr text, float r, float g, float b, float a)
    {
        return SetTextColorFloatNativeFunction(text, r, g, b, a);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextColor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextColor(IntPtr text, out byte r, out byte g, out byte b, out byte a);
    private delegate bool GetTextColorNativeDelegate(IntPtr text, out byte r, out byte g, out byte b, out byte a);
    private static GetTextColorNativeDelegate GetTextColorNativeFunction = TTF_GetTextColor;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextColor(TTF_Text *text, Uint8 *r, Uint8 *g, Uint8 *b, Uint8 *a);</code>
    /// <summary>
    /// Get the color of a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="r">a pointer filled in with the red color value in the range of
    /// 0-255, may be <c>null</c>.</param>
    /// <param name="g">a pointer filled in with the green color value in the range of
    /// 0-255, may be <c>null</c>.</param>
    /// <param name="b">a pointer filled in with the blue color value in the range of
    /// 0-255, may be <c>null</c>.</param>
    /// <param name="a">a pointer filled in with the alpha value in the range of 0-255,
    /// may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextColorFloat"/>
    /// <seealso cref="SetTextColor"/>
    public static bool GetTextColor(IntPtr text, out byte r, out byte g, out byte b, out byte a)
    {
        return GetTextColorNativeFunction(text, out r, out g, out b, out a);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextColorFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextColorFloat(IntPtr text, out float r, out float g, out float b, out float a);
    private delegate bool GetTextColorFloatNativeDelegate(IntPtr text, out float r, out float g, out float b, out float a);
    private static GetTextColorFloatNativeDelegate GetTextColorFloatNativeFunction = TTF_GetTextColorFloat;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextColorFloat(TTF_Text *text, float *r, float *g, float *b, float *a);</code>
    /// <summary>
    /// Get the color of a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="r">a pointer filled in with the red color value, normally in the
    /// range of 0-1, may be <c>null</c>.</param>
    /// <param name="g">a pointer filled in with the green color value, normally in the
    /// range of 0-1, may be <c>null</c>.</param>
    /// <param name="b">a pointer filled in with the blue color value, normally in the
    /// range of 0-1, may be <c>null</c>.</param>
    /// <param name="a">a pointer filled in with the alpha value in the range of 0-1, may
    /// be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextColor"/>
    /// <seealso cref="SetTextColorFloat"/>
    public static bool GetTextColorFloat(IntPtr text, out float r, out float g, out float b, out float a)
    {
        return GetTextColorFloatNativeFunction(text, out r, out g, out b, out a);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextPosition(IntPtr text, int x, int y);
    private delegate bool SetTextPositionNativeDelegate(IntPtr text, int x, int y);
    private static SetTextPositionNativeDelegate SetTextPositionNativeFunction = TTF_SetTextPosition;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextPosition(TTF_Text *text, int x, int y);</code>
    /// <summary>
    /// <para>Set the position of a text object.</para>
    /// <para>This can be used to position multiple text objects within a single wrapping
    /// text area.</para>
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="x">the x offset of the upper left corner of this text in pixels.</param>
    /// <param name="y">the y offset of the upper left corner of this text in pixels.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextPosition"/>
    public static bool SetTextPosition(IntPtr text, int x, int y)
    {
        return SetTextPositionNativeFunction(text, x, y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextPosition(IntPtr text, out int x, out int y);
    private delegate bool GetTextPositionNativeDelegate(IntPtr text, out int x, out int y);
    private static GetTextPositionNativeDelegate GetTextPositionNativeFunction = TTF_GetTextPosition;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextPosition(TTF_Text *text, int *x, int *y);</code>
    /// <summary>
    /// <para>Get the position of a text object.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="x">a pointer filled in with the x offset of the upper left corner of
    /// this text in pixels, may be <c>null</c>.</param>
    /// <param name="y">a pointer filled in with the y offset of the upper left corner of
    /// this text in pixels, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetTextPosition"/>
    public static bool GetTextPosition(IntPtr text, out int x, out int y)
    {
        return GetTextPositionNativeFunction(text, out x, out y);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextWrapWidth"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextWrapWidth(IntPtr text, int wrapWidth);
    private delegate bool SetTextWrapWidthNativeDelegate(IntPtr text, int wrapWidth);
    private static SetTextWrapWidthNativeDelegate SetTextWrapWidthNativeFunction = TTF_SetTextWrapWidth;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextWrapWidth(TTF_Text *text, int wrap_width);</code>
    /// <summary>
    /// Set whether wrapping is enabled on a text object.
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="wrapWidth">the maximum width in pixels, 0 to wrap on newline
    /// characters.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="GetTextWrapWidth"/>
    public static bool SetTextWrapWidth(IntPtr text, int wrapWidth)
    {
        return SetTextWrapWidthNativeFunction(text, wrapWidth);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextWrapWidth"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextWrapWidth(IntPtr text, out int wrapWidth);
    private delegate bool GetTextWrapWidthNativeDelegate(IntPtr text, out int wrapWidth);
    private static GetTextWrapWidthNativeDelegate GetTextWrapWidthNativeFunction = TTF_GetTextWrapWidth;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextWrapWidth(TTF_Text *text, int *wrap_width);</code>
    /// <summary>
    /// Get whether wrapping is enabled on a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="wrapWidth">a pointer filled in with the maximum width in pixels or 0
    /// if the text is being wrapped on newline characters.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetTextWrapWidth"/>
    public static bool GetTextWrapWidth(IntPtr text, out int wrapWidth)
    {
        return GetTextWrapWidthNativeFunction(text, out wrapWidth);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextWrapWhitespaceVisible"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextWrapWhitespaceVisible(IntPtr text, [MarshalAs(UnmanagedType.I1)] bool visible);
    private delegate bool SetTextWrapWhitespaceVisibleNativeDelegate(IntPtr text, bool visible);
    private static SetTextWrapWhitespaceVisibleNativeDelegate SetTextWrapWhitespaceVisibleNativeFunction = TTF_SetTextWrapWhitespaceVisible;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextWrapWhitespaceVisible(TTF_Text *text, bool visible);</code>
    /// <summary>
    /// <para>Set whether whitespace should be visible when wrapping a text object.</para>
    /// <para>If the whitespace is visible, it will take up space for purposes of
    /// alignment and wrapping. This is good for editing, but looks better when
    /// centered or aligned if whitespace around line wrapping is hidden. This
    /// defaults <c>false</c>.</para>
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="visible"><c>true</c> to show whitespace when wrapping text, <c>false</c> to hide
    /// it.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="TextWrapWhitespaceVisible"/>
    public static bool SetTextWrapWhitespaceVisible(IntPtr text, [MarshalAs(UnmanagedType.I1)] bool visible)
    {
        return SetTextWrapWhitespaceVisibleNativeFunction(text, visible);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_TextWrapWhitespaceVisible"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_TextWrapWhitespaceVisible(IntPtr text);
    private delegate bool TextWrapWhitespaceVisibleNativeDelegate(IntPtr text);
    private static TextWrapWhitespaceVisibleNativeDelegate TextWrapWhitespaceVisibleNativeFunction = TTF_TextWrapWhitespaceVisible;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_TextWrapWhitespaceVisible(TTF_Text *text);</code>
    /// <summary>
    /// Return whether whitespace is shown when wrapping a text object.
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <returns><c>true</c> if whitespace is shown when wrapping text, or <c>false</c>
    /// otherwise.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="SetTextWrapWhitespaceVisible"/>
    public static bool TextWrapWhitespaceVisible(IntPtr text)
    {
        return TextWrapWhitespaceVisibleNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_SetTextString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetTextString(IntPtr text, [MarshalAs(UnmanagedType.LPUTF8Str)] string @string, UIntPtr length);
    private delegate bool SetTextStringNativeDelegate(IntPtr text, string @string, UIntPtr length);
    private static SetTextStringNativeDelegate SetTextStringNativeFunction = TTF_SetTextString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_SetTextString(TTF_Text *text, const char *string, size_t length);</code>
    /// <summary>
    /// Set the UTF-8 text used by a text object.
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="string">the UTF-8 text to use, may be <c>null</c>.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="InsertTextString"/>
    public static bool SetTextString(IntPtr text, [MarshalAs(UnmanagedType.LPUTF8Str)] string @string, UIntPtr length)
    {
        return SetTextStringNativeFunction(text, @string, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_InsertTextString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_InsertTextString(IntPtr text, int offset, [MarshalAs(UnmanagedType.LPUTF8Str)] string @string, UIntPtr length);
    private delegate bool InsertTextStringNativeDelegate(IntPtr text, int offset, string @string, UIntPtr length);
    private static InsertTextStringNativeDelegate InsertTextStringNativeFunction = TTF_InsertTextString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_InsertTextString(TTF_Text *text, int offset, const char *string, size_t length);</code>
    /// <summary>
    /// Insert UTF-8 text into a text object.
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="offset">the offset, in bytes, from the beginning of the string if >=
    /// 0, the offset from the end of the string if &lt; 0. Note that
    /// this does not do UTF-8 validation, so you should only insert
    /// at UTF-8 sequence boundaries.</param>
    /// <param name="string">the UTF-8 text to insert.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="SetTextString"/>
    public static bool InsertTextString(IntPtr text, int offset, [MarshalAs(UnmanagedType.LPUTF8Str)] string @string, UIntPtr length)
    {
        return InsertTextStringNativeFunction(text, offset, @string, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_AppendTextString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_AppendTextString(IntPtr text, [MarshalAs(UnmanagedType.LPUTF8Str)] string @string, UIntPtr length);
    private delegate bool AppendTextStringNativeDelegate(IntPtr text, string @string, UIntPtr length);
    private static AppendTextStringNativeDelegate AppendTextStringNativeFunction = TTF_AppendTextString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_AppendTextString(TTF_Text *text, const char *string, size_t length);</code>
    /// <summary>
    /// Append UTF-8 text to a text object.
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="string">the UTF-8 text to insert.</param>
    /// <param name="length">the length of the text, in bytes, or 0 for <c>null</c> terminated
    /// text.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="DeleteTextString"/>
    /// <seealso cref="InsertTextString"/>
    /// <seealso cref="SetTextString"/>
    public static bool AppendTextString(IntPtr text, [MarshalAs(UnmanagedType.LPUTF8Str)] string @string, UIntPtr length)
    {
        return AppendTextStringNativeFunction(text, @string, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DeleteTextString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_DeleteTextString(IntPtr text, int offset, int length);
    private delegate bool DeleteTextStringNativeDelegate(IntPtr text, int offset, int length);
    private static DeleteTextStringNativeDelegate DeleteTextStringNativeFunction = TTF_DeleteTextString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_DeleteTextString(TTF_Text *text, int offset, int length);</code>
    /// <summary>
    /// Delete UTF-8 text from a text object.
    /// <para>This function may cause the internal text representation to be rebuilt.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to modify.</param>
    /// <param name="offset">the offset, in bytes, from the beginning of the string if >=
    /// 0, the offset from the end of the string if &lt; 0. Note that
    /// this does not do UTF-8 validation, so you should only delete
    /// at UTF-8 sequence boundaries.</param>
    /// <param name="length">the length of text to delete, in bytes, or -1 for the
    /// remainder of the string.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="AppendTextString"/>
    /// <seealso cref="InsertTextString"/>
    /// <seealso cref="SetTextString"/>
    public static bool DeleteTextString(IntPtr text, int offset, int length)
    {
        return DeleteTextStringNativeFunction(text, offset, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextSize(IntPtr text, out int w, out int h);
    private delegate bool GetTextSizeNativeDelegate(IntPtr text, out int w, out int h);
    private static GetTextSizeNativeDelegate GetTextSizeNativeFunction = TTF_GetTextSize;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextSize(TTF_Text *text, int *w, int *h);</code>
    /// <summary>
    /// <para>Get the size of a text object.</para>
    /// <para>The size of the text may change when the font or font style and size
    /// change.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="w">a pointer filled in with the width of the text, in pixels, may be
    /// <c>null</c>.</param>
    /// <param name="h">a pointer filled in with the height of the text, in pixels, may be
    /// <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>his function is available since SDL_ttf 3.0.0.</since>
    public static bool GetTextSize(IntPtr text, out int w, out int h)
    {
        return GetTextSizeNativeFunction(text, out w, out h);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextSubString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextSubString(IntPtr text, int offset, out SubString substring);
    private delegate bool GetTextSubStringNativeDelegate(IntPtr text, int offset, out SubString substring);
    private static GetTextSubStringNativeDelegate GetTextSubStringNativeFunction = TTF_GetTextSubString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextSubString(TTF_Text *text, int offset, TTF_SubString *substring);</code>
    /// <summary>
    /// <para>Get the substring of a text object that surrounds a text offset.</para>
    /// <para>If <c>offset</c> is less than 0, this will return a zero length substring at the
    /// beginning of the text with the <see cref="SubStringFlags.TextStart"/> flag set. If
    /// <c>offset</c> is greater than or equal to the length of the text string, this
    /// will return a zero length substring at the end of the text with the
    /// <see cref="SubStringFlags.TextEnd"/> flag set.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="offset">a byte offset into the text string.</param>
    /// <param name="substring">a pointer filled in with the substring containing the
    /// offset.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetTextSubString(IntPtr text, int offset, out SubString substring)
    {
        return GetTextSubStringNativeFunction(text, offset, out substring);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextSubStringForLine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextSubStringForLine(IntPtr text, int line, out SubString substring);
    private delegate bool GetTextSubStringForLineNativeDelegate(IntPtr text, int line, out SubString substring);
    private static GetTextSubStringForLineNativeDelegate GetTextSubStringForLineNativeFunction = TTF_GetTextSubStringForLine;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextSubStringForLine(TTF_Text *text, int line, TTF_SubString *substring);</code>
    /// <summary>
    /// <para>Get the substring of a text object that contains the given line.</para>
    /// <para>If <c>line</c> is less than 0, this will return a zero length substring at the
    /// beginning of the text with the <see cref="SubStringFlags.TextStart"/> flag set. If `line`
    /// is greater than or equal to <c>Text.NumLines</c> this will return a zero
    /// length substring at the end of the text with the <see cref="SubStringFlags.TextEnd"/>
    /// flag set.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="line">a zero-based line index, in the range [0 .. Text.NumLines-1].</param>
    /// <param name="substring">a pointer filled in with the substring containing the
    /// offset.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetTextSubStringForLine(IntPtr text, int line, out SubString substring)
    {
        return GetTextSubStringForLineNativeFunction(text, line, out substring);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextSubStringsForRange"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr TTF_GetTextSubStringsForRange(IntPtr text, int offset, int length, out int count);
    private delegate IntPtr GetTextSubStringsForRangeNativeDelegate(IntPtr text, int offset, int length, out int count);
    private static GetTextSubStringsForRangeNativeDelegate GetTextSubStringsForRangeNativeFunction = TTF_GetTextSubStringsForRange;

    /// <code>extern SDL_DECLSPEC TTF_SubString ** SDLCALL TTF_GetTextSubStringsForRange(TTF_Text *text, int offset, int length, int *count);</code>
    /// <summary>
    /// <para>Get the substrings of a text object that contain a range of text.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="offset">a byte offset into the text string.</param>
    /// <param name="length">the length of the range being queried, in bytes, or -1 for
    /// the remainder of the string.</param>
    /// <param name="count">a pointer filled in with the number of substrings returned,
    /// may be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of substring pointers or <c>null</c> on failure;
    /// call <see cref="SDL.GetError"/> for more information. This is a single
    /// allocation that should be freed with <see cref="SDL.Free"/> when it is no
    /// longer needed.</returns>
    public static SubString[]? GetTextSubStringsForRange(IntPtr text, int offset, int length, out int count)
    {
        IntPtr ptr = GetTextSubStringsForRangeNativeFunction(text, offset, length, out count);

        if (ptr == IntPtr.Zero)
        {
            return null;
        }

        try
        {
            SubString[] substrings = new SubString[count];

            for (int i = 0; i < count; i++)
            {
                IntPtr item = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);
                substrings[i] = Marshal.PtrToStructure<SubString>(item);
            }

            return substrings;
        }
        finally
        {
            SDL.Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetTextSubStringForPoint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetTextSubStringForPoint(IntPtr text, int x, int y, out SubString substring);
    private delegate bool GetTextSubStringForPointNativeDelegate(IntPtr text, int x, int y, out SubString substring);
    private static GetTextSubStringForPointNativeDelegate GetTextSubStringForPointNativeFunction = TTF_GetTextSubStringForPoint;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetTextSubStringForPoint(TTF_Text *text, int x, int y, TTF_SubString *substring);</code>
    /// <summary>
    /// <para>Get the portion of a text string that is closest to a point.</para>
    /// <para>This will return the closest substring of text to the given point.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to query.</param>
    /// <param name="x">the x coordinate relative to the left side of the text, may be
    /// outside the bounds of the text area.</param>
    /// <param name="y">the y coordinate relative to the top side of the text, may be
    /// outside the bounds of the text area.</param>
    /// <param name="substring">a pointer filled in with the closest substring of text to
    /// the given point.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetTextSubStringForPoint(IntPtr text, int x, int y, out SubString substring)
    {
        return GetTextSubStringForPointNativeFunction(text, x, y, out substring);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetPreviousTextSubString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetPreviousTextSubString(IntPtr text, in SubString substring, out SubString previous);
    private delegate bool GetPreviousTextSubStringNativeDelegate(IntPtr text, in SubString substring, out SubString previous);
    private static GetPreviousTextSubStringNativeDelegate GetPreviousTextSubStringNativeFunction = TTF_GetPreviousTextSubString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetPreviousTextSubString(TTF_Text *text, const TTF_SubString *substring, TTF_SubString *previous);</code>
    /// <summary>
    /// <para>Get the previous substring in a text object</para>
    /// <para>If called at the start of the text, this will return a zero length
    /// substring with the <see cref="SubStringFlags.TextStart"/> flag set.</para>
    /// </summary>
    /// <param name="text">the TTF_Text to query.</param>
    /// <param name="substring">the <see cref="SubString"/> to query.</param>
    /// <param name="previous"><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</param>
    /// <returns></returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetPreviousTextSubString(IntPtr text, in SubString substring, out SubString previous)
    {
        return GetPreviousTextSubStringNativeFunction(text, in substring, out previous);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_GetNextTextSubString"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetNextTextSubString(IntPtr text, in SubString substring, out SubString next);
    private delegate bool GetNextTextSubStringNativeDelegate(IntPtr text, in SubString substring, out SubString next);
    private static GetNextTextSubStringNativeDelegate GetNextTextSubStringNativeFunction = TTF_GetNextTextSubString;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_GetNextTextSubString(TTF_Text *text, const TTF_SubString *substring, TTF_SubString *next);</code>
    /// <summary>
    /// <para>Get the next substring in a text object</para>
    /// <para>If called at the end of the text, this will return a zero length substring
    /// with the <see cref="SubStringFlags.TextEnd"/> flag set.</para>
    /// </summary>
    /// <param name="text">the TTF_Text to query.</param>
    /// <param name="substring">the <see cref="SubString"/> to query.</param>
    /// <param name="next">a pointer filled in with the next substring.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool GetNextTextSubString(IntPtr text, in SubString substring, out SubString next)
    {
        return GetNextTextSubStringNativeFunction(text, in substring, out next);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_UpdateText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_UpdateText(IntPtr text);
    private delegate bool UpdateTextNativeDelegate(IntPtr text);
    private static UpdateTextNativeDelegate UpdateTextNativeFunction = TTF_UpdateText;

    /// <code>extern SDL_DECLSPEC bool SDLCALL TTF_UpdateText(TTF_Text *text);</code>
    /// <summary>
    /// <para>Update the layout of a text object.</para>
    /// <para>This is automatically done when the layout is requested or the text is
    /// rendered, but you can call this if you need more control over the timing of
    /// when the layout and text engine representation are updated.</para>
    /// </summary>
    /// <param name="text">the <see cref="TTFText"/> to update.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static bool UpdateText(IntPtr text)
    {
        return UpdateTextNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_DestroyText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_DestroyText(IntPtr text);
    private delegate void DestroyTextNativeDelegate(IntPtr text);
    private static DestroyTextNativeDelegate DestroyTextNativeFunction = TTF_DestroyText;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_DestroyText(TTF_Text *text);</code>
    /// <summary>
    /// Destroy a text object created by a text engine.
    /// </summary>
    /// <param name="text">the text to destroy.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// text.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="CreateText"/>
    public static void DestroyText(IntPtr text)
    {
        DestroyTextNativeFunction(text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_CloseFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_CloseFont(IntPtr font);
    private delegate void CloseFontNativeDelegate(IntPtr font);
    private static CloseFontNativeDelegate CloseFontNativeFunction = TTF_CloseFont;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_CloseFont(TTF_Font *font);</code>
    /// <summary>
    /// <para>Dispose of a previously-created font.</para>
    /// <para>Call this when done with a font. This function will free any resources
    /// associated with it. It is safe to call this function on <c>null</c>, for example
    /// on the result of a failed call to <see cref="OpenFont"/>.</para>
    /// <para>The font is not valid after being passed to this function. String pointers
    /// from functions that return information on this font, such as
    /// <see cref="GetFontFamilyName"/> and <see cref="GetFontStyleName"/>, are no longer valid
    /// after this call, as well.</para>
    /// </summary>
    /// <param name="font">the font to dispose of.</param>
    /// <threadsafety>This function should not be called while any other thread is
    /// using the font.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="OpenFont"/>
    /// <seealso cref="OpenFontIO"/>
    public static void CloseFont(IntPtr font)
    {
        CloseFontNativeFunction(font);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void TTF_Quit();
    private delegate void QuitNativeDelegate();
    private static QuitNativeDelegate QuitNativeFunction = TTF_Quit;

    /// <code>extern SDL_DECLSPEC void SDLCALL TTF_Quit(void);</code>
    /// <summary>
    /// <para>Deinitialize SDL_ttf.</para>
    /// <para>You must call this when done with the library, to free internal resources.
    /// It is safe to call this when the library isn't initialized, as it will just
    /// return immediately.</para>
    /// <para>Once you have as many quit calls as you have had successful calls to
    /// <see cref="Init"/>, the library will actually deinitialize.</para>
    /// <para>Please note that this does not automatically close any fonts that are still
    /// open at the time of deinitialization, and it is possibly not safe to close
    /// them afterwards, as parts of the library will no longer be initialized
    /// deal with it. A well-written program should call <see cref="CloseFont"/> on any
    /// open fonts before calling this function!</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    public static void Quit()
    {
        QuitNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(FontLibrary, EntryPoint = "TTF_WasInit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int TTF_WasInit();
    private delegate int WasInitNativeDelegate();
    private static WasInitNativeDelegate WasInitNativeFunction = TTF_WasInit;

    /// <code>extern SDL_DECLSPEC int SDLCALL TTF_WasInit(void);</code>
    /// <summary>
    /// <para>Check if SDL_ttf is initialized.</para>
    /// <para>This reports the number of times the library has been initialized by a call
    /// to <see cref="Init"/>, without a paired deinitialization request from <see cref="Quit"/>.</para>
    /// <para>In short: if it's greater than zero, the library is currently initialized
    /// and ready to work. If zero, it is not initialized.</para>
    /// <para>Despite the return value being a signed integer, this function should not
    /// return a negative number.</para>
    /// </summary>
    /// <returns>the current number of initialization calls, that need to
    /// eventually be paired with this many calls to <see cref="Quit"/>.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_ttf 3.0.0.</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="Quit"/>
    public static int WasInit()
    {
        return WasInitNativeFunction();
    }
}
