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

namespace SDL3;

public static partial class SDL
{
	/// <summary>
	/// <para> A macro for defining custom FourCC pixel formats.</para>
	/// <para>For example, defining <see cref="PixelFormat.YV12"/> looks like this:</para>
	/// <code>DefinePixelFourCC('Y', 'V', '1', '2')</code>
	/// </summary>
	/// <param name="a">the first character of the FourCC code.</param>
	/// <param name="b">the second character of the FourCC code.</param>
	/// <param name="c">the third character of the FourCC code.</param>
	/// <param name="d">the fourth character of the FourCC code.</param>
	/// <returns>a format value in the style of <see cref="PixelFormat"/>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static PixelFormat DefinePixelFourCC(char a, char b, char c, char d) => (PixelFormat)FourCC(a, b, c, d);
	
	
	/// <summary>
	/// <para>A macro for defining custom non-FourCC pixel formats.</para>
	/// <para>For example, defining <see cref="PixelFormat.RGBA8888"/> looks like this:</para>
	/// <code>DefinePixelFormat(PixelType.Packed32, PackedOrder.RGBA, PackedLayout.Layout8888, 32, 4)</code>
	/// </summary>
	/// <param name="type">the type of the new format, probably a <see cref="PixelType"/> value.</param>
	/// <param name="order">the order of the new format, probably a <see cref="BitmapOrder"/>,
	/// <see cref="PackedOrder"/>, or <see cref="ArrayOrder"/> value.</param>
	/// <param name="layout">the layout of the new format, probably an <see cref="PackedLayout"/>
	/// value or zero.</param>
	/// <param name="bits">the number of bits per pixel of the new format.</param>
	/// <param name="bytes">the number of bytes per pixel of the new format.</param>
	/// <returns>a format value in the style of <see cref="PixelFormat"/></returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static PixelFormat DefinePixelFormat(PixelType type, byte order, PackedLayout layout, byte bits, byte bytes) => (PixelFormat)((1 << 28) | ((byte)type << 24) | (order << 20) | ((byte)layout << 16) | (bits << 8) | bytes);
    
	
	/// <summary>
	/// <para>A macro to retrieve the flags of an <see cref="PixelFormat"/>.</para>
	/// <para>This macro is generally not needed directly by an app, which should use
	/// specific tests, like <see cref="IsPixelFormatFourCC"/>, instead.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns>the flags of <c>format</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static uint PixelFlag(PixelFormat x) => ((uint)x >> 28) & 0x0F;
    
	
	/// <summary>
	/// <para>A macro to retrieve the type of an <see cref="PixelFormat"/>.</para>
	/// <para>This is usually a value from the <see cref="PixelType"/> enumeration.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns>the type of <c>format</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static PixelType GetPixelType(PixelFormat x) => (PixelType)(((uint)x >> 24) & 0x0F);
    
	
	/// <summary>
	/// <para>A macro to retrieve the order of an <see cref="PixelFormat"/>.</para>
	/// <para>This is usually a value from the <see cref="BitmapOrder"/>, <see cref="PackedOrder"/>, or
	/// <see cref="ArrayOrder"/> enumerations, depending on the format type.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns>the order of <c>format</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
	public static uint PixelOrder(PixelFormat x) => ((uint)x >> 20) & 0x0F;

	
	/// <summary>
	/// <para> A macro to retrieve the layout of an <see cref="PixelFormat"/>.</para>
	/// <para>This is usually a value from the <see cref="PackedLayout"/> enumeration, or zero if a
	/// layout doesn't make sense for the format type.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns>the layout of <c>format</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static PackedLayout PixelLayout(PixelFormat x) => (PackedLayout)(((uint)x >> 16) & 0x0F);

	
	/// <summary>
	/// <para>A macro to determine an <see cref="PixelFormat"/>'s bits per pixel.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// <para>FourCC formats will report zero here, as it rarely makes sense to measure
	/// them per-pixel.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns>the bits-per-pixel of <c>format</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	/// <seealso cref="BytesPerPixel"/>
	[Macro]
	public static uint BitsPerPixel(PixelFormat x) => IsPixelFormatFourCC(x) ? 0 : (((uint)x >> 8) & 0xFF);

	
	/// <summary>
	/// <para>A macro to determine an <see cref="PixelFormat"/>'s bytes per pixel.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// <para>FourCC formats do their best here, but many of them don't have a meaningful
	/// measurement of bytes per pixel.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns>the bytes-per-pixel of <c>format</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	/// <seealso cref="BitsPerPixel"/>
	[Macro]
	public static uint BytesPerPixel(PixelFormat x) => IsPixelFormatFourCC(x) ? (((x == PixelFormat.YUY2) || (x == PixelFormat.UYVY) || (x == PixelFormat.YVYU) || (x == PixelFormat.P010)) ? 2u : 1u) : (((uint)x >> 0) & 0xFF);

	
	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> is an indexed format.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format is indexed, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormatIndexed(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.Index1) || (GetPixelType(x) == PixelType.Index2) || (GetPixelType(x) == PixelType.Index4) || (GetPixelType(x) == PixelType.Index8));

	
	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> is a packed format.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format is packed, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormatPacked(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.Packed8) || (GetPixelType(x) == PixelType.Packed16) || (GetPixelType(x) == PixelType.Packed32));

	
	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> is an array format.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format is an array, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormatArray(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.ArrayU8) || (GetPixelType(x) == PixelType.ArrayU16) || (GetPixelType(x) == PixelType.ArrayU32) || (GetPixelType(x) == PixelType.ArrayF16) || (GetPixelType(x) == PixelType.ArrayF32));

	
	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> is a 10-bit format.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format is 10-bit, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormat10Bit(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.Packed32) || (PixelLayout(x) == PackedLayout.Layout2101010));
	
	
	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> is a floating point format.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format is 10-bit, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormatFloat(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.ArrayF16) || (GetPixelType(x) == PixelType.ArrayF32));
	
	
	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> has an alpha channel.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format has alpha, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormatAlpha(PixelFormat x) => IsPixelFormatPacked(x) && ((PixelOrder(x) == (uint)PackedOrder.ARGB) || (PixelOrder(x) == (uint)PackedOrder.RGBA) || (PixelOrder(x) == (uint)PackedOrder.ABGR) || (PixelOrder(x) == (uint)PackedOrder.BGRA));

	/// <summary>
	/// <para>A macro to determine if an <see cref="PixelFormat"/> is a "FourCC" format.</para>
	/// <para>This covers custom and other unusual formats.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="x">an <see cref="PixelFormat"/> to check.</param>
	/// <returns><c>true</c> if the format has alpha, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsPixelFormatFourCC(PixelFormat x) => (x != PixelFormat.Unknown) && (PixelFlag(x) != 1);

	
	/// <summary>
	/// <para>A macro for defining custom <see cref="Colorspace"/> formats.</para>
	/// <para>For example, defining <see cref="Colorspace.SRGB"/> looks like this:</para>
	/// <code>
	///	DefineColorspace(ColorType.RGB,
	///                  ColorRange.Full,
	///                  ColorPrimaries.BT709,
	///                  TransferCharacteristics.SRGB,
	///                  MatrixCoefficients.Identity,
	///                  ChromaLocation.None)
	/// </code>
	/// </summary>
	/// <param name="type">the type of the new format, probably an <see cref="ColorType"/> value.</param>
	/// <param name="range">the range of the new format, probably a <see cref="ColorRange"/> value.</param>
	/// <param name="primaries">the primaries of the new format, probably an
	/// <see cref="ColorPrimaries"/> value.</param>
	/// <param name="transfer">the transfer characteristics of the new format, probably an
	/// <see cref="TransferCharacteristics"/> value.</param>
	/// <param name="matrix">the matrix coefficients of the new format, probably an
	/// <see cref="MatrixCoefficients"/> value.</param>
	/// <param name="chroma">the chroma sample location of the new format, probably an
	/// <see cref="ChromaLocation"/> value.</param>
	/// <returns>a format value in the style of <see cref="ColorRange"/>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static Colorspace DefineColorspace(ColorType type, ColorRange range, ColorPrimaries primaries, TransferCharacteristics transfer, MatrixCoefficients matrix, ChromaLocation chroma) => (Colorspace)(((byte)type << 28) | ((byte)range << 24) | ((byte)chroma << 20) | ((byte)primaries << 10) | ((byte)transfer << 5) | (byte)matrix);

	
	/// <summary>
	/// <para>A macro to retrieve the type of an <see cref="Colorspace"/>.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns>the <see cref="ColorType"/> for <c>cspace</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static ColorType ColorspaceType(Colorspace cspace) => (ColorType)(((uint)cspace >> 28) & 0x0F);

	
	/// <summary>
	/// <para>A macro to retrieve the range of an <see cref="Colorspace"/>.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns>the <see cref="ColorRange"/> of <c>cspace</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static ColorRange ColorspaceRange(Colorspace cspace) => (ColorRange)(((uint)cspace >> 24) & 0x0F);

	
	/// <summary>
	/// <para>A macro to retrieve the chroma sample location of an <see cref="Colorspace"/>.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns>the <see cref="ChromaLocation"/> of <c>cspace</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static ChromaLocation ColorspaceChroma(Colorspace cspace) => (ChromaLocation)(((uint)cspace >> 20) & 0x0F);

	
	/// <summary>
	/// A macro to retrieve the primaries of an <see cref="Colorspace"/>.
	/// </summary>
	/// <param name="cspace">an SDL_Colorspace to check.</param>
	/// <returns>the <see cref="ColorPrimaries"/> of <c>cspace</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static ColorPrimaries ColorspacePrimaries(Colorspace cspace) => (ColorPrimaries)(((uint)cspace >> 10) & 0x1F);

	
	/// <summary>
	/// <para>A macro to retrieve the transfer characteristics of an <see cref="Colorspace"/>.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns>the <see cref="TransferCharacteristics"/> of <c>cspace</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static TransferCharacteristics ColorspaceTransfer(Colorspace cspace) => (TransferCharacteristics)(((uint)cspace >> 5) & 0x1F);

	
	/// <summary>
	/// <para>A macro to retrieve the matrix coefficients of an <see cref="Colorspace"/>.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns> the <see cref="MatrixCoefficients"/> of <c>cspace</c>.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static MatrixCoefficients ColorspaceMatrix(Colorspace cspace) => (MatrixCoefficients)((uint)cspace & 0x1F);

	
	/// <summary>
	/// <para>A macro to determine if an <see cref="Colorspace"/> uses BT601 (or BT470BG) matrix
	/// coefficients.</para>
	/// <para>Note that this macro double-evaluates its parameter, so do not use
	/// expressions with side-effects here.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns><c>true</c> if BT601 or BT470BG, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsColorspaceMatrixBT601(Colorspace cspace) => (ColorspaceMatrix(cspace) == MatrixCoefficients.BT601) || (ColorspaceMatrix(cspace) == MatrixCoefficients.BT470BG);

	
	/// <summary>
	/// A macro to determine if an <see cref="Colorspace"/> uses BT709 matrix coefficients.
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns>true if BT709, false otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsColorspaceMatrixBT709(Colorspace cspace) => ColorspaceMatrix(cspace) == MatrixCoefficients.BT709;

	
	/// <summary>
	/// <para>A macro to determine if an <c>Colorspace</c> uses BT2020_NCL matrix
	/// coefficients.</para>
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns><c>true</c> if BT2020_NCL, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsColorspaceMatrixBT2020NCL(Colorspace cspace) => ColorspaceMatrix(cspace) == MatrixCoefficients.BT2020NCL;

	
	/// <summary>
	/// A macro to determine if an <see cref="Colorspace"/> has a limited range.
	/// </summary>
	/// <param name="cspace">an <see cref="Colorspace"/> to check.</param>
	/// <returns><c>true</c> if limited range, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsColorspaceLimitedRange(Colorspace cspace) => ColorspaceRange(cspace) != ColorRange.Full;

	
	/// <summary>
	/// <para>A macro to determine if an <see cref="Colorspace"/> has a full range.</para>
	/// </summary>
	/// <param name="cspace">an SDL_Colorspace to check.</param>
	/// <returns><c>true</c> if full range, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	[Macro]
	public static bool IsColorspaceFullRange(Colorspace cspace) => ColorspaceRange(cspace) == ColorRange.Full;
}