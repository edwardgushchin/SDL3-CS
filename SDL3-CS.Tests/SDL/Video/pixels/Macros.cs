using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Pixels;

internal static class MacroTests
{
    public static void RunAll()
    {
        PixelFormatPacking_UnpacksFieldsAndKeepsMacroAttributes();
        PixelFormatPredicates_CoverIndexedPackedArrayFloatAlphaAndFourCC();
        ColorspacePacking_UnpacksFieldsAndKeepsMacroAttributes();
        ColorspacePredicates_CoverMatrixAndRangeBranches();
    }

    public static void PixelFormatPacking_UnpacksFieldsAndKeepsMacroAttributes()
    {
        SDL3.SDL.PixelFormat fourCc = SDL3.SDL.DefinePixelFourCC('Y', 'V', '1', '2');
        SDL3.SDL.PixelFormat rgba8888 = SDL3.SDL.DefinePixelFormat(
            SDL3.SDL.PixelType.Packed32,
            (byte)SDL3.SDL.PackedOrder.RGBA,
            SDL3.SDL.PackedLayout.Layout8888,
            32,
            4);

        TestAssert.Equal(SDL3.SDL.PixelFormat.YV12, fourCc, "SDL.DefinePixelFourCC must pack FourCC bytes.");
        TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, rgba8888, "SDL.DefinePixelFormat must pack SDL pixel format fields.");
        TestAssert.Equal(1u, SDL3.SDL.PixelFlag(rgba8888), "SDL.PixelFlag must read pixel format flag bits.");
        TestAssert.Equal(SDL3.SDL.PixelType.Packed32, SDL3.SDL.GetPixelType(rgba8888), "SDL.GetPixelType must read pixel type bits.");
        TestAssert.Equal((uint)SDL3.SDL.PackedOrder.RGBA, SDL3.SDL.PixelOrder(rgba8888), "SDL.PixelOrder must read order bits.");
        TestAssert.Equal(SDL3.SDL.PackedLayout.Layout8888, SDL3.SDL.PixelLayout(rgba8888), "SDL.PixelLayout must read layout bits.");
        TestAssert.Equal(32u, SDL3.SDL.BitsPerPixel(rgba8888), "SDL.BitsPerPixel must read non-FourCC bits-per-pixel.");
        TestAssert.Equal(0u, SDL3.SDL.BitsPerPixel(SDL3.SDL.PixelFormat.YV12), "SDL.BitsPerPixel must report zero for FourCC formats.");
        TestAssert.Equal(4u, SDL3.SDL.BytesPerPixel(rgba8888), "SDL.BytesPerPixel must read non-FourCC bytes-per-pixel.");
        TestAssert.Equal(2u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.YUY2), "SDL.BytesPerPixel must special-case YUY2.");
        TestAssert.Equal(2u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.UYVY), "SDL.BytesPerPixel must special-case UYVY.");
        TestAssert.Equal(2u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.YVYU), "SDL.BytesPerPixel must special-case YVYU.");
        TestAssert.Equal(2u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.P010), "SDL.BytesPerPixel must special-case P010.");
        TestAssert.Equal(1u, SDL3.SDL.BytesPerPixel(SDL3.SDL.PixelFormat.YV12), "SDL.BytesPerPixel must default other FourCC formats to one byte.");

        AssertMacroAttribute(nameof(SDL3.SDL.DefinePixelFourCC), [typeof(char), typeof(char), typeof(char), typeof(char)]);
        AssertMacroAttribute(nameof(SDL3.SDL.DefinePixelFormat), [typeof(SDL3.SDL.PixelType), typeof(byte), typeof(SDL3.SDL.PackedLayout), typeof(byte), typeof(byte)]);
        AssertMacroAttribute(nameof(SDL3.SDL.PixelFlag), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.GetPixelType), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.PixelOrder), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.PixelLayout), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.BitsPerPixel), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.BytesPerPixel), [typeof(SDL3.SDL.PixelFormat)]);
    }

    public static void PixelFormatPredicates_CoverIndexedPackedArrayFloatAlphaAndFourCC()
    {
        SDL3.SDL.PixelFormat arrayU32 = SDL3.SDL.DefinePixelFormat(SDL3.SDL.PixelType.ArrayU32, 0, SDL3.SDL.PackedLayout.None, 32, 4);
        SDL3.SDL.PixelFormat packed16TenBit = SDL3.SDL.DefinePixelFormat(
            SDL3.SDL.PixelType.Packed16,
            (byte)SDL3.SDL.PackedOrder.XRGB,
            SDL3.SDL.PackedLayout.Layout2101010,
            16,
            2);

        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatIndexed(SDL3.SDL.PixelFormat.Index1LSB), "SDL.IsPixelFormatIndexed must accept Index1.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatIndexed(SDL3.SDL.PixelFormat.Index2LSB), "SDL.IsPixelFormatIndexed must accept Index2.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatIndexed(SDL3.SDL.PixelFormat.Index4LSB), "SDL.IsPixelFormatIndexed must accept Index4.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatIndexed(SDL3.SDL.PixelFormat.Index8), "SDL.IsPixelFormatIndexed must accept Index8.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatIndexed(SDL3.SDL.PixelFormat.RGBA8888), "SDL.IsPixelFormatIndexed must reject packed formats.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatIndexed(SDL3.SDL.PixelFormat.YV12), "SDL.IsPixelFormatIndexed must reject FourCC formats.");

        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatPacked(SDL3.SDL.PixelFormat.RGB332), "SDL.IsPixelFormatPacked must accept Packed8.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatPacked(SDL3.SDL.PixelFormat.RGB565), "SDL.IsPixelFormatPacked must accept Packed16.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatPacked(SDL3.SDL.PixelFormat.RGBA8888), "SDL.IsPixelFormatPacked must accept Packed32.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatPacked(SDL3.SDL.PixelFormat.Index8), "SDL.IsPixelFormatPacked must reject indexed formats.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatPacked(SDL3.SDL.PixelFormat.YV12), "SDL.IsPixelFormatPacked must reject FourCC formats.");

        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatArray(SDL3.SDL.PixelFormat.RGB24), "SDL.IsPixelFormatArray must accept ArrayU8.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatArray(SDL3.SDL.PixelFormat.RGB48), "SDL.IsPixelFormatArray must accept ArrayU16.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatArray(arrayU32), "SDL.IsPixelFormatArray must accept ArrayU32.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatArray(SDL3.SDL.PixelFormat.RGBA64Float), "SDL.IsPixelFormatArray must accept ArrayF16.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatArray(SDL3.SDL.PixelFormat.RGBA128Float), "SDL.IsPixelFormatArray must accept ArrayF32.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatArray(SDL3.SDL.PixelFormat.RGBA8888), "SDL.IsPixelFormatArray must reject packed formats.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatArray(SDL3.SDL.PixelFormat.YV12), "SDL.IsPixelFormatArray must reject FourCC formats.");

        TestAssert.Equal(true, SDL3.SDL.IsPixelFormat10Bit(SDL3.SDL.PixelFormat.RGBA8888), "SDL.IsPixelFormat10Bit currently accepts Packed32 formats.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormat10Bit(packed16TenBit), "SDL.IsPixelFormat10Bit must accept Layout2101010.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormat10Bit(SDL3.SDL.PixelFormat.Index8), "SDL.IsPixelFormat10Bit must reject indexed formats.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormat10Bit(SDL3.SDL.PixelFormat.YV12), "SDL.IsPixelFormat10Bit must reject non-2101010 FourCC formats.");

        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatFloat(SDL3.SDL.PixelFormat.RGBA64Float), "SDL.IsPixelFormatFloat must accept ArrayF16.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatFloat(SDL3.SDL.PixelFormat.RGBA128Float), "SDL.IsPixelFormatFloat must accept ArrayF32.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatFloat(SDL3.SDL.PixelFormat.RGB24), "SDL.IsPixelFormatFloat must reject integer arrays.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatFloat(SDL3.SDL.PixelFormat.YV12), "SDL.IsPixelFormatFloat must reject FourCC formats.");

        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatAlpha(SDL3.SDL.PixelFormat.ARGB8888), "SDL.IsPixelFormatAlpha must accept ARGB order.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatAlpha(SDL3.SDL.PixelFormat.RGBA8888), "SDL.IsPixelFormatAlpha must accept RGBA order.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatAlpha(SDL3.SDL.PixelFormat.ABGR8888), "SDL.IsPixelFormatAlpha must accept ABGR order.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatAlpha(SDL3.SDL.PixelFormat.BGRA8888), "SDL.IsPixelFormatAlpha must accept BGRA order.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatAlpha(SDL3.SDL.PixelFormat.XRGB8888), "SDL.IsPixelFormatAlpha must reject XRGB order.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatAlpha(SDL3.SDL.PixelFormat.RGB24), "SDL.IsPixelFormatAlpha must reject non-packed formats.");

        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatFourCC(SDL3.SDL.PixelFormat.Unknown), "SDL.IsPixelFormatFourCC must reject Unknown.");
        TestAssert.Equal(false, SDL3.SDL.IsPixelFormatFourCC(SDL3.SDL.PixelFormat.RGBA8888), "SDL.IsPixelFormatFourCC must reject normal SDL pixel formats.");
        TestAssert.Equal(true, SDL3.SDL.IsPixelFormatFourCC(SDL3.SDL.PixelFormat.YV12), "SDL.IsPixelFormatFourCC must accept FourCC values.");

        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormatIndexed), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormatPacked), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormatArray), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormat10Bit), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormatFloat), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormatAlpha), [typeof(SDL3.SDL.PixelFormat)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsPixelFormatFourCC), [typeof(SDL3.SDL.PixelFormat)]);
    }

    public static void ColorspacePacking_UnpacksFieldsAndKeepsMacroAttributes()
    {
        SDL3.SDL.Colorspace colorspace = SDL3.SDL.DefineColorspace(
            SDL3.SDL.ColorType.YCBCR,
            SDL3.SDL.ColorRange.Limited,
            SDL3.SDL.ColorPrimaries.BT2020,
            SDL3.SDL.TransferCharacteristics.PQ,
            SDL3.SDL.MatrixCoefficients.BT2020NCL,
            SDL3.SDL.ChromaLocation.Left);

        TestAssert.Equal(SDL3.SDL.Colorspace.BT2020Limited, colorspace, "SDL.DefineColorspace must pack SDL colorspace fields.");
        TestAssert.Equal(SDL3.SDL.ColorType.YCBCR, SDL3.SDL.ColorspaceType(colorspace), "SDL.ColorspaceType must read type bits.");
        TestAssert.Equal(SDL3.SDL.ColorRange.Limited, SDL3.SDL.ColorspaceRange(colorspace), "SDL.ColorspaceRange must read range bits.");
        TestAssert.Equal(SDL3.SDL.ChromaLocation.Left, SDL3.SDL.ColorspaceChroma(colorspace), "SDL.ColorspaceChroma must read chroma bits.");
        TestAssert.Equal(SDL3.SDL.ColorPrimaries.BT2020, SDL3.SDL.ColorspacePrimaries(colorspace), "SDL.ColorspacePrimaries must read primaries bits.");
        TestAssert.Equal(SDL3.SDL.TransferCharacteristics.PQ, SDL3.SDL.ColorspaceTransfer(colorspace), "SDL.ColorspaceTransfer must read transfer bits.");
        TestAssert.Equal(SDL3.SDL.MatrixCoefficients.BT2020NCL, SDL3.SDL.ColorspaceMatrix(colorspace), "SDL.ColorspaceMatrix must read matrix bits.");

        AssertMacroAttribute(nameof(SDL3.SDL.DefineColorspace), [
            typeof(SDL3.SDL.ColorType),
            typeof(SDL3.SDL.ColorRange),
            typeof(SDL3.SDL.ColorPrimaries),
            typeof(SDL3.SDL.TransferCharacteristics),
            typeof(SDL3.SDL.MatrixCoefficients),
            typeof(SDL3.SDL.ChromaLocation)]);
        AssertMacroAttribute(nameof(SDL3.SDL.ColorspaceType), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.ColorspaceRange), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.ColorspaceChroma), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.ColorspacePrimaries), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.ColorspaceTransfer), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.ColorspaceMatrix), [typeof(SDL3.SDL.Colorspace)]);
    }

    public static void ColorspacePredicates_CoverMatrixAndRangeBranches()
    {
        SDL3.SDL.Colorspace bt470bg = SDL3.SDL.DefineColorspace(
            SDL3.SDL.ColorType.YCBCR,
            SDL3.SDL.ColorRange.Limited,
            SDL3.SDL.ColorPrimaries.BT601,
            SDL3.SDL.TransferCharacteristics.BT601,
            SDL3.SDL.MatrixCoefficients.BT470BG,
            SDL3.SDL.ChromaLocation.Left);

        TestAssert.Equal(true, SDL3.SDL.IsColorspaceMatrixBT601(SDL3.SDL.Colorspace.BT601Limited), "SDL.IsColorspaceMatrixBT601 must accept BT601.");
        TestAssert.Equal(true, SDL3.SDL.IsColorspaceMatrixBT601(bt470bg), "SDL.IsColorspaceMatrixBT601 must accept BT470BG.");
        TestAssert.Equal(false, SDL3.SDL.IsColorspaceMatrixBT601(SDL3.SDL.Colorspace.BT709Full), "SDL.IsColorspaceMatrixBT601 must reject BT709.");
        TestAssert.Equal(true, SDL3.SDL.IsColorspaceMatrixBT709(SDL3.SDL.Colorspace.BT709Full), "SDL.IsColorspaceMatrixBT709 must accept BT709.");
        TestAssert.Equal(false, SDL3.SDL.IsColorspaceMatrixBT709(SDL3.SDL.Colorspace.BT601Full), "SDL.IsColorspaceMatrixBT709 must reject BT601.");
        TestAssert.Equal(true, SDL3.SDL.IsColorspaceMatrixBT2020NCL(SDL3.SDL.Colorspace.BT2020Full), "SDL.IsColorspaceMatrixBT2020NCL must accept BT2020NCL.");
        TestAssert.Equal(false, SDL3.SDL.IsColorspaceMatrixBT2020NCL(SDL3.SDL.Colorspace.BT709Full), "SDL.IsColorspaceMatrixBT2020NCL must reject BT709.");
        TestAssert.Equal(true, SDL3.SDL.IsColorspaceLimitedRange(SDL3.SDL.Colorspace.BT601Limited), "SDL.IsColorspaceLimitedRange must accept limited range.");
        TestAssert.Equal(false, SDL3.SDL.IsColorspaceLimitedRange(SDL3.SDL.Colorspace.BT601Full), "SDL.IsColorspaceLimitedRange must reject full range.");
        TestAssert.Equal(true, SDL3.SDL.IsColorspaceFullRange(SDL3.SDL.Colorspace.BT601Full), "SDL.IsColorspaceFullRange must accept full range.");
        TestAssert.Equal(false, SDL3.SDL.IsColorspaceFullRange(SDL3.SDL.Colorspace.BT601Limited), "SDL.IsColorspaceFullRange must reject limited range.");

        AssertMacroAttribute(nameof(SDL3.SDL.IsColorspaceMatrixBT601), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsColorspaceMatrixBT709), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsColorspaceMatrixBT2020NCL), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsColorspaceLimitedRange), [typeof(SDL3.SDL.Colorspace)]);
        AssertMacroAttribute(nameof(SDL3.SDL.IsColorspaceFullRange), [typeof(SDL3.SDL.Colorspace)]);
    }

    private static void AssertMacroAttribute(string methodName, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), $"SDL.{methodName} must keep MacroAttribute.");
    }
}
