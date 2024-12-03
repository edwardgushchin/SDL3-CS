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

namespace SDL3;

public static partial class SDL
{
    [Macro]
	public static PixelFormat DefinePixelFourCC(byte a, byte b, byte c, byte d) => (PixelFormat)FourCC(a, b, c, d);
	
    [Macro]
    public static PixelFormat DefinePixelFormat(PixelType type, byte order, PackedLayout layout, byte bits, byte bytes) => (PixelFormat)((1 << 28) | ((byte)type << 24) | (order << 20) | ((byte)layout << 16) | (bits << 8) | bytes);
    
    [Macro]
    public static uint PixelFlag(PixelFormat x) => ((uint)x >> 28) & 0x0F;
    
    [Macro]
    public static PixelType GetPixelType(PixelFormat x) => (PixelType)(((uint)x >> 24) & 0x0F);
    
    [Macro]
	public static uint PixelOrder(PixelFormat x) => ((uint)x >> 20) & 0x0F;

	[Macro]
	public static PackedLayout PixelLayout(PixelFormat x) => (PackedLayout)(((uint)x >> 16) & 0x0F);

	[Macro]
	public static uint BitsPerPixel(PixelFormat x) => IsPixelFormatFourCC(x) ? 0 : (((uint)x >> 8) & 0xFF);

	[Macro]
	public static uint BytesPerPixel(PixelFormat x) => IsPixelFormatFourCC(x) ? (((x == PixelFormat.YUY2) || (x == PixelFormat.UYVY) || (x == PixelFormat.YVYU) || (x == PixelFormat.P010)) ? 2u : 1u) : (((uint)x >> 0) & 0xFF);

	[Macro]
	public static bool IsPixelFormatIndexed(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.Index1) || (GetPixelType(x) == PixelType.Index2) || (GetPixelType(x) == PixelType.Index4) || (GetPixelType(x) == PixelType.Index8));

	[Macro]
	public static bool IsPixelFormatPacked(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.Packed8) || (GetPixelType(x) == PixelType.Packed16) || (GetPixelType(x) == PixelType.Packed32));

	[Macro]
	public static bool IsPixelFormatArray(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.ArrayU8) || (GetPixelType(x) == PixelType.ArrayU16) || (GetPixelType(x) == PixelType.ArrayU32) || (GetPixelType(x) == PixelType.ArrayF16) || (GetPixelType(x) == PixelType.ArrayF32));

	[Macro]
	public static bool IsPixelFormat10Bit(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.Packed32) || (PixelLayout(x) == PackedLayout.Layout2101010));
	
	[Macro]
	public static bool IsPixelFormatFloat(PixelFormat x) => (!IsPixelFormatFourCC(x)) && ((GetPixelType(x) == PixelType.ArrayF16) || (GetPixelType(x) == PixelType.ArrayF32));
	
	[Macro]
	public static bool IsPixelFormatAlpha(PixelFormat x) => IsPixelFormatPacked(x) && ((PixelOrder(x) == (uint)PackedOrder.ARGB) || (PixelOrder(x) == (uint)PackedOrder.RGBA) || (PixelOrder(x) == (uint)PackedOrder.ABGR) || (PixelOrder(x) == (uint)PackedOrder.BGRA));

	[Macro]
	public static bool IsPixelFormatFourCC(PixelFormat x) => (x != PixelFormat.Unknown) && (PixelFlag(x) != 1);

	[Macro]
	public static Colorspace DefineColorspace(ColorType type, ColorRange range, ColorPrimaries primaries, TransferCharacteristics transfer, MatrixCoefficients matrix, ChromaLocation chroma) => (Colorspace)(((byte)type << 28) | ((byte)range << 24) | ((byte)chroma << 20) | ((byte)primaries << 10) | ((byte)transfer << 5) | (byte)matrix);

	[Macro]
	public static ColorType ColorspaceType(Colorspace x) => (ColorType)(((uint)x >> 28) & 0x0F);

	[Macro]
	public static ColorRange ColorspaceRange(Colorspace x) => (ColorRange)(((uint)x >> 24) & 0x0F);

	[Macro]
	public static ChromaLocation ColorspaceChroma(Colorspace x) => (ChromaLocation)(((uint)x >> 20) & 0x0F);

	[Macro]
	public static ColorPrimaries ColorspacePrimaries(Colorspace x) => (ColorPrimaries)(((uint)x >> 10) & 0x1F);

	[Macro]
	public static TransferCharacteristics ColorspaceTransfer(Colorspace x) => (TransferCharacteristics)(((uint)x >> 5) & 0x1F);

	[Macro]
	public static MatrixCoefficients ColorspaceMatrix(Colorspace x) => (MatrixCoefficients)((uint)x & 0x1F);

	[Macro]
	public static bool IsColorspaceMatrixBT601(Colorspace x) => (ColorspaceMatrix(x) == MatrixCoefficients.BT601) || (ColorspaceMatrix(x) == MatrixCoefficients.BT470BG);

	[Macro]
	public static bool IsColorspaceMatrixBT709(Colorspace x) => ColorspaceMatrix(x) == MatrixCoefficients.BT709;

	[Macro]
	public static bool IsColorspaceMatrixBT2020Ncl(Colorspace x) => ColorspaceMatrix(x) == MatrixCoefficients.BT2020NCL;

	[Macro]
	public static bool IsColorspaceLimitedRange(Colorspace x) => ColorspaceRange(x) != ColorRange.Full;

	[Macro]
	public static bool IsColorspaceFullRange(Colorspace x) => ColorspaceRange(x) == ColorRange.Full;
}