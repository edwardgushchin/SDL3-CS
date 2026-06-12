using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Pixels;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr capturedFormatPointer;
    private static IntPtr capturedPalettePointer;
    private static SDL3.SDL.PixelFormat nextPixelFormat;
    private static SDL3.SDL.PixelFormat capturedPixelFormat;
    private static SDL3.SDL.PixelFormatDetails capturedFormatDetails;
    private static SDL3.SDL.Palette capturedPalette;
    private static SDL3.SDL.Color[]? capturedColors;
    private static uint nextUInt;
    private static uint capturedPixelValue;
    private static uint capturedRMask;
    private static uint capturedGMask;
    private static uint capturedBMask;
    private static uint capturedAMask;
    private static int capturedBpp;
    private static int capturedNColors;
    private static int capturedFirstColor;
    private static int capturedColorCount;
    private static byte capturedR;
    private static byte capturedG;
    private static byte capturedB;
    private static byte capturedA;
    private static byte nextR;
    private static byte nextG;
    private static byte nextB;
    private static byte nextA;
    private static bool nextBool;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        GetPixelFormatName_ReturnsUtf8StringAndForwardsFormat();
        GetPixelFormatName_ReturnsEmptyStringForNativeNull();
        GetMasksForPixelFormat_ForwardsFormatReturnsMasksAndNativeValue();
        GetPixelFormatForMasks_ForwardsMasksAndReturnsNativeValue();
        GetPixelFormatDetails_ForwardsFormatAndReturnsNativeValue();
        CreatePalette_ForwardsColorCountAndReturnsNativeValue();
        SetPaletteColors_ForwardsPaletteColorsRangeAndReturnsNativeValue();
        DestroyPalette_ForwardsPalette();
        MapRGB_ForwardsFormatPaletteAndChannelsAndReturnsNativeValue();
        MapRGBA_ForwardsFormatPaletteAndChannelsAndReturnsNativeValue();
        GetRGB_WithPointerPaletteForwardsInputsAndReturnsChannels();
        GetRGB_WithTypedPaletteForwardsInputsAndReturnsChannels();
        GetRGBA_WithPointerPaletteForwardsInputsAndReturnsChannels();
        GetRGBA_WithTypedPaletteForwardsInputsAndReturnsChannels();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetPixelFormatName"), "SDL_GetPixelFormatName");

        MethodInfo masks = GetNativeMethod("SDL_GetMasksForPixelFormat");
        AssertSdlLibraryImport(masks, "SDL_GetMasksForPixelFormat");
        AssertBoolReturnMarshal(masks);

        AssertSdlLibraryImport(GetNativeMethod("SDL_GetPixelFormatForMasks"), "SDL_GetPixelFormatForMasks");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetPixelFormatDetails"), "SDL_GetPixelFormatDetails");
        AssertSdlLibraryImport(GetNativeMethod("SDL_CreatePalette"), "SDL_CreatePalette");

        MethodInfo setPaletteColors = GetNativeMethod("SDL_SetPaletteColors");
        AssertSdlLibraryImport(setPaletteColors, "SDL_SetPaletteColors");
        AssertBoolReturnMarshal(setPaletteColors);
        AssertNativeBoolImport(GetNativeMethod("SDL_SetPaletteColorsPointer"), "SDL_SetPaletteColors");

        AssertSdlLibraryImport(GetNativeMethod("SDL_DestroyPalette"), "SDL_DestroyPalette");
        AssertSdlLibraryImport(GetNativeMethod("SDL_MapRGB"), "SDL_MapRGB");
        AssertSdlLibraryImport(GetNativeMethod("SDL_MapRGBA"), "SDL_MapRGBA");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetRGB", [typeof(uint), typeof(SDL3.SDL.PixelFormatDetails).MakeByRefType(), typeof(IntPtr), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType()]), "SDL_GetRGB");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetRGB", [typeof(uint), typeof(SDL3.SDL.PixelFormatDetails).MakeByRefType(), typeof(SDL3.SDL.Palette).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType()]), "SDL_GetRGB");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetRGBA", [typeof(uint), typeof(SDL3.SDL.PixelFormatDetails).MakeByRefType(), typeof(IntPtr), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType()]), "SDL_GetRGBA");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetRGBA", [typeof(uint), typeof(SDL3.SDL.PixelFormatDetails).MakeByRefType(), typeof(SDL3.SDL.Palette).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType()]), "SDL_GetRGBA");
    }

    public static void GetPixelFormatName_ReturnsUtf8StringAndForwardsFormat()
    {
        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("SDL_PIXELFORMAT_RGBA8888");

        using NativeHookScope _ = NativeHookScope.Install("GetPixelFormatNameNativeFunction", nameof(CaptureGetPixelFormatName));
        try
        {
            string result = SDL3.SDL.GetPixelFormatName(SDL3.SDL.PixelFormat.RGBA8888);

            TestAssert.Equal("SDL_PIXELFORMAT_RGBA8888", result, "SDL.GetPixelFormatName must convert native UTF-8 strings.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, capturedPixelFormat, "SDL.GetPixelFormatName must forward format.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    public static void GetPixelFormatName_ReturnsEmptyStringForNativeNull()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("GetPixelFormatNameNativeFunction", nameof(CaptureGetPixelFormatName));
        string result = SDL3.SDL.GetPixelFormatName(SDL3.SDL.PixelFormat.Unknown);

        TestAssert.Equal("", result, "SDL.GetPixelFormatName must return an empty string for native null.");
        TestAssert.Equal(SDL3.SDL.PixelFormat.Unknown, capturedPixelFormat, "SDL.GetPixelFormatName must forward unknown format.");
    }

    public static void GetMasksForPixelFormat_ForwardsFormatReturnsMasksAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("GetMasksForPixelFormatNativeFunction", nameof(CaptureGetMasksForPixelFormat));
        int bpp = 0;
        uint rmask = 0;
        uint gmask = 0;
        uint bmask = 0;
        uint amask = 0;
        bool result = SDL3.SDL.GetMasksForPixelFormat(SDL3.SDL.PixelFormat.RGBA8888, ref bpp, ref rmask, ref gmask, ref bmask, ref amask);

        TestAssert.Equal(true, result, "SDL.GetMasksForPixelFormat must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, capturedPixelFormat, "SDL.GetMasksForPixelFormat must forward format.");
        TestAssert.Equal(32, bpp, "SDL.GetMasksForPixelFormat must return bpp.");
        TestAssert.Equal(0x00FF0000u, rmask, "SDL.GetMasksForPixelFormat must return red mask.");
        TestAssert.Equal(0x0000FF00u, gmask, "SDL.GetMasksForPixelFormat must return green mask.");
        TestAssert.Equal(0x000000FFu, bmask, "SDL.GetMasksForPixelFormat must return blue mask.");
        TestAssert.Equal(0xFF000000u, amask, "SDL.GetMasksForPixelFormat must return alpha mask.");
    }

    public static void GetPixelFormatForMasks_ForwardsMasksAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPixelFormat = SDL3.SDL.PixelFormat.RGBA8888;

        using NativeHookScope _ = NativeHookScope.Install("GetPixelFormatForMasksNativeFunction", nameof(CaptureGetPixelFormatForMasks));
        SDL3.SDL.PixelFormat result = SDL3.SDL.GetPixelFormatForMasks(32, 1, 2, 3, 4);

        TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, result, "SDL.GetPixelFormatForMasks must return the native hook value.");
        TestAssert.Equal(32, capturedBpp, "SDL.GetPixelFormatForMasks must forward bpp.");
        TestAssert.Equal(1u, capturedRMask, "SDL.GetPixelFormatForMasks must forward red mask.");
        TestAssert.Equal(2u, capturedGMask, "SDL.GetPixelFormatForMasks must forward green mask.");
        TestAssert.Equal(3u, capturedBMask, "SDL.GetPixelFormatForMasks must forward blue mask.");
        TestAssert.Equal(4u, capturedAMask, "SDL.GetPixelFormatForMasks must forward alpha mask.");
    }

    public static void GetPixelFormatDetails_ForwardsFormatAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1010;

        using NativeHookScope _ = NativeHookScope.Install("GetPixelFormatDetailsNativeFunction", nameof(CaptureGetPixelFormatDetails));
        IntPtr result = SDL3.SDL.GetPixelFormatDetails(SDL3.SDL.PixelFormat.RGB565);

        TestAssert.Equal((IntPtr)0x1010, result, "SDL.GetPixelFormatDetails must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.PixelFormat.RGB565, capturedPixelFormat, "SDL.GetPixelFormatDetails must forward format.");
    }

    public static void CreatePalette_ForwardsColorCountAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x2020;

        using NativeHookScope _ = NativeHookScope.Install("CreatePaletteNativeFunction", nameof(CaptureCreatePalette));
        IntPtr result = SDL3.SDL.CreatePalette(16);

        TestAssert.Equal((IntPtr)0x2020, result, "SDL.CreatePalette must return the native hook value.");
        TestAssert.Equal(16, capturedNColors, "SDL.CreatePalette must forward color count.");
    }

    public static void SetPaletteColors_ForwardsPaletteColorsRangeAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.Color[] colors = [CreateColor(1, 2, 3, 4), CreateColor(5, 6, 7, 8)];

        using NativeHookScope _ = NativeHookScope.Install("SetPaletteColorsNativeFunction", nameof(CaptureSetPaletteColors));
        bool result = SDL3.SDL.SetPaletteColors((IntPtr)0x3030, colors, 4, colors.Length);

        TestAssert.Equal(true, result, "SDL.SetPaletteColors must return the native hook value.");
        TestAssert.Equal((IntPtr)0x3030, capturedPalettePointer, "SDL.SetPaletteColors must forward palette.");
        TestAssert.NotNull(capturedColors, "SDL.SetPaletteColors must forward colors.");
        AssertColor(colors[0], capturedColors![0], "SDL.SetPaletteColors must forward first color.");
        AssertColor(colors[1], capturedColors[1], "SDL.SetPaletteColors must forward second color.");
        TestAssert.Equal(4, capturedFirstColor, "SDL.SetPaletteColors must forward first color index.");
        TestAssert.Equal(2, capturedColorCount, "SDL.SetPaletteColors must forward color count.");

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope pointerHook = NativeHookScope.Install("SetPaletteColorsPointerNativeFunction", nameof(CaptureSetPaletteColorsPointer));
        result = SDL3.SDL.SetPaletteColors((IntPtr)0x3031, colors.AsSpan(1), 5, 1);

        TestAssert.Equal(true, result, "SDL.SetPaletteColors(ReadOnlySpan<Color>) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x3031, capturedPalettePointer, "SDL.SetPaletteColors(ReadOnlySpan<Color>) must forward palette.");
        TestAssert.NotNull(capturedColors, "SDL.SetPaletteColors(ReadOnlySpan<Color>) must forward colors.");
        AssertColor(colors[1], capturedColors![0], "SDL.SetPaletteColors(ReadOnlySpan<Color>) must forward color slice item 0.");
        TestAssert.Equal(5, capturedFirstColor, "SDL.SetPaletteColors(ReadOnlySpan<Color>) must forward first color index.");
        TestAssert.Equal(1, capturedColorCount, "SDL.SetPaletteColors(ReadOnlySpan<Color>) must forward color count.");
    }

    public static void DestroyPalette_ForwardsPalette()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("DestroyPaletteNativeFunction", nameof(CaptureDestroyPalette));
        SDL3.SDL.DestroyPalette((IntPtr)0x4040);

        TestAssert.Equal((IntPtr)0x4040, capturedPalettePointer, "SDL.DestroyPalette must forward palette.");
    }

    public static void MapRGB_ForwardsFormatPaletteAndChannelsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 0xAABBCCDD;

        using NativeHookScope _ = NativeHookScope.Install("MapRGBNativeFunction", nameof(CaptureMapRGB));
        uint result = SDL3.SDL.MapRGB((IntPtr)0x5050, (IntPtr)0x6060, 10, 20, 30);

        TestAssert.Equal(0xAABBCCDDu, result, "SDL.MapRGB must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5050, capturedFormatPointer, "SDL.MapRGB must forward format.");
        TestAssert.Equal((IntPtr)0x6060, capturedPalettePointer, "SDL.MapRGB must forward palette.");
        TestAssert.Equal(10, capturedR, "SDL.MapRGB must forward red.");
        TestAssert.Equal(20, capturedG, "SDL.MapRGB must forward green.");
        TestAssert.Equal(30, capturedB, "SDL.MapRGB must forward blue.");
    }

    public static void MapRGBA_ForwardsFormatPaletteAndChannelsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 0x11223344;

        using NativeHookScope _ = NativeHookScope.Install("MapRGBANativeFunction", nameof(CaptureMapRGBA));
        uint result = SDL3.SDL.MapRGBA((IntPtr)0x7070, (IntPtr)0x8080, 11, 22, 33, 44);

        TestAssert.Equal(0x11223344u, result, "SDL.MapRGBA must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7070, capturedFormatPointer, "SDL.MapRGBA must forward format.");
        TestAssert.Equal((IntPtr)0x8080, capturedPalettePointer, "SDL.MapRGBA must forward palette.");
        TestAssert.Equal(11, capturedR, "SDL.MapRGBA must forward red.");
        TestAssert.Equal(22, capturedG, "SDL.MapRGBA must forward green.");
        TestAssert.Equal(33, capturedB, "SDL.MapRGBA must forward blue.");
        TestAssert.Equal(44, capturedA, "SDL.MapRGBA must forward alpha.");
    }

    public static void GetRGB_WithPointerPaletteForwardsInputsAndReturnsChannels()
    {
        ResetCaptureState();
        nextR = 1;
        nextG = 2;
        nextB = 3;
        SDL3.SDL.PixelFormatDetails details = CreatePixelFormatDetails(SDL3.SDL.PixelFormat.RGB565);

        using NativeHookScope _ = NativeHookScope.Install("GetRGBWithPointerNativeFunction", nameof(CaptureGetRGBWithPointer));
        SDL3.SDL.GetRGB(0x12345678, in details, (IntPtr)0x9090, out byte r, out byte g, out byte b);

        TestAssert.Equal(0x12345678u, capturedPixelValue, "SDL.GetRGB(IntPtr) must forward pixel value.");
        AssertPixelFormatDetails(details, capturedFormatDetails, "SDL.GetRGB(IntPtr) must forward format details.");
        TestAssert.Equal((IntPtr)0x9090, capturedPalettePointer, "SDL.GetRGB(IntPtr) must forward palette pointer.");
        TestAssert.Equal(1, r, "SDL.GetRGB(IntPtr) must return red.");
        TestAssert.Equal(2, g, "SDL.GetRGB(IntPtr) must return green.");
        TestAssert.Equal(3, b, "SDL.GetRGB(IntPtr) must return blue.");
    }

    public static void GetRGB_WithTypedPaletteForwardsInputsAndReturnsChannels()
    {
        ResetCaptureState();
        nextR = 4;
        nextG = 5;
        nextB = 6;
        SDL3.SDL.PixelFormatDetails details = CreatePixelFormatDetails(SDL3.SDL.PixelFormat.RGB565);
        SDL3.SDL.Palette palette = CreatePaletteStruct(2, (IntPtr)0xA0A0);

        using NativeHookScope _ = NativeHookScope.Install("GetRGBWithPaletteNativeFunction", nameof(CaptureGetRGBWithPalette));
        SDL3.SDL.GetRGB(0x23456789, in details, in palette, out byte r, out byte g, out byte b);

        TestAssert.Equal(0x23456789u, capturedPixelValue, "SDL.GetRGB(Palette) must forward pixel value.");
        AssertPixelFormatDetails(details, capturedFormatDetails, "SDL.GetRGB(Palette) must forward format details.");
        AssertPalette(palette, capturedPalette, "SDL.GetRGB(Palette) must forward palette.");
        TestAssert.Equal(4, r, "SDL.GetRGB(Palette) must return red.");
        TestAssert.Equal(5, g, "SDL.GetRGB(Palette) must return green.");
        TestAssert.Equal(6, b, "SDL.GetRGB(Palette) must return blue.");
    }

    public static void GetRGBA_WithPointerPaletteForwardsInputsAndReturnsChannels()
    {
        ResetCaptureState();
        nextR = 7;
        nextG = 8;
        nextB = 9;
        nextA = 10;
        SDL3.SDL.PixelFormatDetails details = CreatePixelFormatDetails(SDL3.SDL.PixelFormat.RGBA8888);

        using NativeHookScope _ = NativeHookScope.Install("GetRGBAWithPointerNativeFunction", nameof(CaptureGetRGBAWithPointer));
        SDL3.SDL.GetRGBA(0x3456789A, in details, (IntPtr)0xB0B0, out byte r, out byte g, out byte b, out byte a);

        TestAssert.Equal(0x3456789Au, capturedPixelValue, "SDL.GetRGBA(IntPtr) must forward pixel value.");
        AssertPixelFormatDetails(details, capturedFormatDetails, "SDL.GetRGBA(IntPtr) must forward format details.");
        TestAssert.Equal((IntPtr)0xB0B0, capturedPalettePointer, "SDL.GetRGBA(IntPtr) must forward palette pointer.");
        TestAssert.Equal(7, r, "SDL.GetRGBA(IntPtr) must return red.");
        TestAssert.Equal(8, g, "SDL.GetRGBA(IntPtr) must return green.");
        TestAssert.Equal(9, b, "SDL.GetRGBA(IntPtr) must return blue.");
        TestAssert.Equal(10, a, "SDL.GetRGBA(IntPtr) must return alpha.");
    }

    public static void GetRGBA_WithTypedPaletteForwardsInputsAndReturnsChannels()
    {
        ResetCaptureState();
        nextR = 11;
        nextG = 12;
        nextB = 13;
        nextA = 14;
        SDL3.SDL.PixelFormatDetails details = CreatePixelFormatDetails(SDL3.SDL.PixelFormat.RGBA8888);
        SDL3.SDL.Palette palette = CreatePaletteStruct(3, (IntPtr)0xC0C0);

        using NativeHookScope _ = NativeHookScope.Install("GetRGBAWithPaletteNativeFunction", nameof(CaptureGetRGBAWithPalette));
        SDL3.SDL.GetRGBA(0x456789AB, in details, in palette, out byte r, out byte g, out byte b, out byte a);

        TestAssert.Equal(0x456789ABu, capturedPixelValue, "SDL.GetRGBA(Palette) must forward pixel value.");
        AssertPixelFormatDetails(details, capturedFormatDetails, "SDL.GetRGBA(Palette) must forward format details.");
        AssertPalette(palette, capturedPalette, "SDL.GetRGBA(Palette) must forward palette.");
        TestAssert.Equal(11, r, "SDL.GetRGBA(Palette) must return red.");
        TestAssert.Equal(12, g, "SDL.GetRGBA(Palette) must return green.");
        TestAssert.Equal(13, b, "SDL.GetRGBA(Palette) must return blue.");
        TestAssert.Equal(14, a, "SDL.GetRGBA(Palette) must return alpha.");
    }

    private static IntPtr CaptureGetPixelFormatName(SDL3.SDL.PixelFormat format)
    {
        capturedPixelFormat = format;
        return nextPointer;
    }

    private static bool CaptureGetMasksForPixelFormat(SDL3.SDL.PixelFormat format, ref int bpp, ref uint rmask, ref uint gmask, ref uint bmask, ref uint amask)
    {
        capturedPixelFormat = format;
        bpp = 32;
        rmask = 0x00FF0000u;
        gmask = 0x0000FF00u;
        bmask = 0x000000FFu;
        amask = 0xFF000000u;
        return nextBool;
    }

    private static SDL3.SDL.PixelFormat CaptureGetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask)
    {
        capturedBpp = bpp;
        capturedRMask = rmask;
        capturedGMask = gmask;
        capturedBMask = bmask;
        capturedAMask = amask;
        return nextPixelFormat;
    }

    private static IntPtr CaptureGetPixelFormatDetails(SDL3.SDL.PixelFormat format)
    {
        capturedPixelFormat = format;
        return nextPointer;
    }

    private static IntPtr CaptureCreatePalette(int ncolors)
    {
        capturedNColors = ncolors;
        return nextPointer;
    }

    private static bool CaptureSetPaletteColors(IntPtr palette, SDL3.SDL.Color[] colors, int firstcolor, int ncolors)
    {
        capturedPalettePointer = palette;
        capturedColors = [.. colors];
        capturedFirstColor = firstcolor;
        capturedColorCount = ncolors;
        return nextBool;
    }

    private static bool CaptureSetPaletteColorsPointer(IntPtr palette, IntPtr colors, int firstcolor, int ncolors)
    {
        capturedPalettePointer = palette;
        capturedColors = CopyUnmanaged<SDL3.SDL.Color>(colors, ncolors);
        capturedFirstColor = firstcolor;
        capturedColorCount = ncolors;
        return nextBool;
    }

    private static void CaptureDestroyPalette(IntPtr palette)
    {
        capturedPalettePointer = palette;
    }

    private static uint CaptureMapRGB(IntPtr format, IntPtr palette, byte r, byte g, byte b)
    {
        capturedFormatPointer = format;
        capturedPalettePointer = palette;
        capturedR = r;
        capturedG = g;
        capturedB = b;
        return nextUInt;
    }

    private static uint CaptureMapRGBA(IntPtr format, IntPtr palette, byte r, byte g, byte b, byte a)
    {
        capturedFormatPointer = format;
        capturedPalettePointer = palette;
        capturedR = r;
        capturedG = g;
        capturedB = b;
        capturedA = a;
        return nextUInt;
    }

    private static void CaptureGetRGBWithPointer(uint pixelvalue, in SDL3.SDL.PixelFormatDetails format, IntPtr palette, out byte r, out byte g, out byte b)
    {
        capturedPixelValue = pixelvalue;
        capturedFormatDetails = format;
        capturedPalettePointer = palette;
        r = nextR;
        g = nextG;
        b = nextB;
    }

    private static void CaptureGetRGBWithPalette(uint pixelvalue, in SDL3.SDL.PixelFormatDetails format, in SDL3.SDL.Palette palette, out byte r, out byte g, out byte b)
    {
        capturedPixelValue = pixelvalue;
        capturedFormatDetails = format;
        capturedPalette = palette;
        r = nextR;
        g = nextG;
        b = nextB;
    }

    private static void CaptureGetRGBAWithPointer(uint pixelvalue, in SDL3.SDL.PixelFormatDetails format, IntPtr palette, out byte r, out byte g, out byte b, out byte a)
    {
        capturedPixelValue = pixelvalue;
        capturedFormatDetails = format;
        capturedPalettePointer = palette;
        r = nextR;
        g = nextG;
        b = nextB;
        a = nextA;
    }

    private static void CaptureGetRGBAWithPalette(uint pixelvalue, in SDL3.SDL.PixelFormatDetails format, in SDL3.SDL.Palette palette, out byte r, out byte g, out byte b, out byte a)
    {
        capturedPixelValue = pixelvalue;
        capturedFormatDetails = format;
        capturedPalette = palette;
        r = nextR;
        g = nextG;
        b = nextB;
        a = nextA;
    }

    private static SDL3.SDL.Color CreateColor(byte r, byte g, byte b, byte a)
    {
        return new SDL3.SDL.Color { R = r, G = g, B = b, A = a };
    }

    private static SDL3.SDL.PixelFormatDetails CreatePixelFormatDetails(SDL3.SDL.PixelFormat format)
    {
        return new SDL3.SDL.PixelFormatDetails
        {
            Format = format,
            BitsPerPixel = 32,
            BytesPerPixel = 4,
            RMask = 0x00FF0000,
            GMask = 0x0000FF00,
            BMask = 0x000000FF,
            AMask = 0xFF000000,
            RBits = 8,
            GBits = 8,
            BBits = 8,
            ABits = 8,
            RShift = 16,
            GShift = 8,
            BShift = 0,
            AShift = 24
        };
    }

    private static SDL3.SDL.Palette CreatePaletteStruct(int ncolors, IntPtr colors)
    {
        return new SDL3.SDL.Palette { NColors = ncolors, Colors = colors, Version = 9, Refcount = 2 };
    }

    private static void AssertColor(SDL3.SDL.Color expected, SDL3.SDL.Color actual, string message)
    {
        TestAssert.Equal(expected.R, actual.R, $"{message} R.");
        TestAssert.Equal(expected.G, actual.G, $"{message} G.");
        TestAssert.Equal(expected.B, actual.B, $"{message} B.");
        TestAssert.Equal(expected.A, actual.A, $"{message} A.");
    }

    private static void AssertPixelFormatDetails(SDL3.SDL.PixelFormatDetails expected, SDL3.SDL.PixelFormatDetails actual, string message)
    {
        TestAssert.Equal(expected.Format, actual.Format, $"{message} Format.");
        TestAssert.Equal(expected.BitsPerPixel, actual.BitsPerPixel, $"{message} BitsPerPixel.");
        TestAssert.Equal(expected.BytesPerPixel, actual.BytesPerPixel, $"{message} BytesPerPixel.");
        TestAssert.Equal(expected.RMask, actual.RMask, $"{message} RMask.");
        TestAssert.Equal(expected.GMask, actual.GMask, $"{message} GMask.");
        TestAssert.Equal(expected.BMask, actual.BMask, $"{message} BMask.");
        TestAssert.Equal(expected.AMask, actual.AMask, $"{message} AMask.");
        TestAssert.Equal(expected.RBits, actual.RBits, $"{message} RBits.");
        TestAssert.Equal(expected.GBits, actual.GBits, $"{message} GBits.");
        TestAssert.Equal(expected.BBits, actual.BBits, $"{message} BBits.");
        TestAssert.Equal(expected.ABits, actual.ABits, $"{message} ABits.");
        TestAssert.Equal(expected.RShift, actual.RShift, $"{message} RShift.");
        TestAssert.Equal(expected.GShift, actual.GShift, $"{message} GShift.");
        TestAssert.Equal(expected.BShift, actual.BShift, $"{message} BShift.");
        TestAssert.Equal(expected.AShift, actual.AShift, $"{message} AShift.");
    }

    private static void AssertPalette(SDL3.SDL.Palette expected, SDL3.SDL.Palette actual, string message)
    {
        TestAssert.Equal(expected.NColors, actual.NColors, $"{message} NColors.");
        TestAssert.Equal(expected.Colors, actual.Colors, $"{message} Colors.");
        TestAssert.Equal(expected.Version, actual.Version, $"{message} Version.");
        TestAssert.Equal(expected.Refcount, actual.Refcount, $"{message} Refcount.");
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedFormatPointer = IntPtr.Zero;
        capturedPalettePointer = IntPtr.Zero;
        nextPixelFormat = default;
        capturedPixelFormat = default;
        capturedFormatDetails = default;
        capturedPalette = default;
        capturedColors = null;
        nextUInt = 0;
        capturedPixelValue = 0;
        capturedRMask = 0;
        capturedGMask = 0;
        capturedBMask = 0;
        capturedAMask = 0;
        capturedBpp = 0;
        capturedNColors = 0;
        capturedFirstColor = 0;
        capturedColorCount = 0;
        capturedR = 0;
        capturedG = 0;
        capturedB = 0;
        capturedA = 0;
        nextR = 0;
        nextG = 0;
        nextB = 0;
        nextA = 0;
        nextBool = false;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static MethodInfo GetNativeMethod(string methodName, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} overload must be private static.");
        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertNativeBoolImport(MethodInfo method, string entryPoint)
    {
        AssertSdlLibraryImport(method, entryPoint);
        AssertBoolReturnMarshal(method);
    }

    private static unsafe T[] CopyUnmanaged<T>(IntPtr pointer, int count) where T : unmanaged
    {
        if (pointer == IntPtr.Zero || count <= 0)
        {
            return [];
        }

        T[] result = new T[count];
        new ReadOnlySpan<T>((void*)pointer, count).CopyTo(result);
        return result;
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private NativeHookScope(FieldInfo field, object? hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL private hook field {fieldName} must exist.");

            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"Test hook method {methodName} must exist.");

            Delegate hook = Delegate.CreateDelegate(field!.FieldType, method!);

            return new NativeHookScope(field, hook);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
