using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Surface;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr capturedSurface;
    private static IntPtr capturedPalette;
    private static IntPtr capturedImage;
    private static IntPtr capturedPixels;
    private static IntPtr capturedSrc;
    private static IntPtr capturedDst;
    private static IntPtr capturedFreePointer;
    private static uint nextUInt;
    private static uint capturedKey;
    private static uint capturedColor;
    private static int capturedWidth;
    private static int capturedHeight;
    private static int capturedLeftWidth;
    private static int capturedRightWidth;
    private static int capturedTopHeight;
    private static int capturedBottomHeight;
    private static int capturedX;
    private static int capturedY;
    private static int capturedPitch;
    private static int capturedSrcPitch;
    private static int capturedDstPitch;
    private static int nextCount;
    private static int capturedFreeCallCount;
    private static uint capturedProps;
    private static uint capturedSrcProperties;
    private static uint capturedDstProperties;
    private static bool nextBool;
    private static bool capturedCloseIO;
    private static bool capturedEnabled;
    private static bool capturedLinear;
    private static byte nextRed;
    private static byte nextGreen;
    private static byte nextBlue;
    private static byte nextAlpha;
    private static byte capturedRed;
    private static byte capturedGreen;
    private static byte capturedBlue;
    private static byte capturedAlpha;
    private static float nextFloatRed;
    private static float nextFloatGreen;
    private static float nextFloatBlue;
    private static float nextFloatAlpha;
    private static string? capturedFile;
    private static byte[]? nextBytes;
    private static byte[]? capturedBytes;
    private static SDL3.SDL.Rect[]? capturedRects;
    private static SDL3.SDL.PixelFormat capturedPixelFormat;
    private static SDL3.SDL.PixelFormat capturedSrcPixelFormat;
    private static SDL3.SDL.PixelFormat capturedDstPixelFormat;
    private static SDL3.SDL.Colorspace nextColorspace;
    private static SDL3.SDL.Colorspace capturedColorspace;
    private static SDL3.SDL.Colorspace capturedSrcColorspace;
    private static SDL3.SDL.Colorspace capturedDstColorspace;
    private static SDL3.SDL.BlendMode nextBlendMode;
    private static SDL3.SDL.BlendMode capturedBlendMode;
    private static SDL3.SDL.FlipMode capturedFlipMode;
    private static SDL3.SDL.ScaleMode capturedScaleMode;
    private static float capturedAngle;
    private static float capturedScale;
    private static float capturedFloatRed;
    private static float capturedFloatGreen;
    private static float capturedFloatBlue;
    private static float capturedFloatAlpha;
    private static IntPtr capturedRectPointer;
    private static IntPtr capturedSrcRectPointer;
    private static IntPtr capturedDstRectPointer;
    private static SDL3.SDL.Rect nextRect;
    private static SDL3.SDL.Rect capturedRect;
    private static SDL3.SDL.Rect capturedSrcRect;
    private static SDL3.SDL.Rect capturedDstRect;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        SurfaceCreationPropertiesColorspaceAndPaletteFunctions_ForwardInputsOutputsAndReturnNativeValues();
        SurfacePaletteAlternateImageAndLockFunctions_ForwardInputsOutputsAndManageNativeArrays();
        SurfaceLoadAndBmpSaveFunctions_ForwardInputsAndReturnNativeValues();
        SurfacePngLoadAndSaveFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceJpgLoadFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceRleColorBlendAndClipFunctions_ForwardInputsOutputsAndReturnNativeValues();
        SurfaceTransformAndConversionFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceConvertPixelsFunctions_ForwardInputsOutputsAndReturnNativeValues();
        SurfacePremultiplyAlphaFunctions_ForwardInputsOutputsAndReturnNativeValues();
        SurfaceClearAndFillFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceBlitFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceScaledBlitFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceStretchFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceTiledBlitFunctions_ForwardInputsAndReturnNativeValues();
        SurfaceTiledBlitWithScaleFunctions_ForwardInputsAndReturnNativeValues();
        Surface9GridBlitFunctions_ForwardInputsAndReturnNativeValues();
        SurfacePixelFunctions_ForwardInputsOutputsAndReturnNativeValues();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertNativeImport(GetNativeMethod("SDL_CreateSurface"), "SDL_CreateSurface");
        AssertNativeImport(GetNativeMethod("SDL_CreateSurfaceFrom"), "SDL_CreateSurfaceFrom");
        AssertNativeImport(GetNativeMethod("SDL_DestroySurface"), "SDL_DestroySurface");
        AssertNativeImport(GetNativeMethod("SDL_GetSurfaceProperties"), "SDL_GetSurfaceProperties");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfaceColorspace"), "SDL_SetSurfaceColorspace");
        AssertNativeImport(GetNativeMethod("SDL_GetSurfaceColorspace"), "SDL_GetSurfaceColorspace");
        AssertNativeImport(GetNativeMethod("SDL_CreateSurfacePalette"), "SDL_CreateSurfacePalette");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfacePalette"), "SDL_SetSurfacePalette");
        AssertNativeImport(GetNativeMethod("SDL_GetSurfacePalette"), "SDL_GetSurfacePalette");
        AssertNativeBoolImport(GetNativeMethod("SDL_AddSurfaceAlternateImage"), "SDL_AddSurfaceAlternateImage");
        AssertNativeBoolImport(GetNativeMethod("SDL_SurfaceHasAlternateImages"), "SDL_SurfaceHasAlternateImages");
        AssertNativeImport(GetNativeMethod("SDL_GetSurfaceImages"), "SDL_GetSurfaceImages");
        AssertNativeImport(GetNativeMethod("SDL_RemoveSurfaceAlternateImages"), "SDL_RemoveSurfaceAlternateImages");
        AssertNativeBoolImport(GetNativeMethod("SDL_LockSurface"), "SDL_LockSurface");
        AssertNativeImport(GetNativeMethod("SDL_UnlockSurface"), "SDL_UnlockSurface");
        MethodInfo loadSurfaceIO = GetNativeMethod("SDL_LoadSurfaceIO");
        AssertNativeImport(loadSurfaceIO, "SDL_LoadSurface_IO");
        AssertBoolParameterMarshal(loadSurfaceIO, 1);
        MethodInfo loadSurface = GetNativeMethod("SDL_LoadSurface");
        AssertNativeImport(loadSurface, "SDL_LoadSurface");
        AssertStringParameterMarshal(loadSurface, 0);
        MethodInfo loadBmpIO = GetNativeMethod("SDL_LoadBMPIO");
        AssertNativeImport(loadBmpIO, "SDL_LoadBMP_IO");
        AssertBoolParameterMarshal(loadBmpIO, 1);
        MethodInfo loadBmp = GetNativeMethod("SDL_LoadBMP");
        AssertNativeImport(loadBmp, "SDL_LoadBMP");
        AssertStringParameterMarshal(loadBmp, 0);
        MethodInfo saveBmpIO = GetNativeMethod("SDL_SaveBMPIO");
        AssertNativeBoolImport(saveBmpIO, "SDL_SaveBMP_IO");
        AssertBoolParameterMarshal(saveBmpIO, 2);
        MethodInfo saveBmp = GetNativeMethod("SDL_SaveBMP");
        AssertNativeBoolImport(saveBmp, "SDL_SaveBMP");
        AssertStringParameterMarshal(saveBmp, 1);
        MethodInfo loadPngIO = GetNativeMethod("SDL_LoadPNGIO");
        AssertNativeImport(loadPngIO, "SDL_LoadPNG_IO");
        AssertBoolParameterMarshal(loadPngIO, 1);
        MethodInfo loadPng = GetNativeMethod("SDL_LoadPNG");
        AssertNativeImport(loadPng, "SDL_LoadPNG");
        AssertStringParameterMarshal(loadPng, 0);
        MethodInfo savePngIO = GetNativeMethod("SDL_SavePNGIO");
        AssertNativeBoolImport(savePngIO, "SDL_SavePNG_IO");
        AssertBoolParameterMarshal(savePngIO, 2);
        MethodInfo savePng = GetNativeMethod("SDL_SavePNG");
        AssertNativeBoolImport(savePng, "SDL_SavePNG");
        AssertStringParameterMarshal(savePng, 1);
        MethodInfo loadJpgIO = GetNativeMethod("SDL_LoadJPGIO");
        AssertNativeImport(loadJpgIO, "SDL_LoadJPG_IO");
        AssertBoolParameterMarshal(loadJpgIO, 1);
        MethodInfo loadJpg = GetNativeMethod("SDL_LoadJPG");
        AssertNativeImport(loadJpg, "SDL_LoadJPG");
        AssertStringParameterMarshal(loadJpg, 0);
        MethodInfo setSurfaceRLE = GetNativeMethod("SDL_SetSurfaceRLE");
        AssertNativeBoolImport(setSurfaceRLE, "SDL_SetSurfaceRLE");
        AssertBoolParameterMarshal(setSurfaceRLE, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_SurfaceHasRLE"), "SDL_SurfaceHasRLE");
        MethodInfo setSurfaceColorKey = GetNativeMethod("SDL_SetSurfaceColorKey");
        AssertNativeBoolImport(setSurfaceColorKey, "SDL_SetSurfaceColorKey");
        AssertBoolParameterMarshal(setSurfaceColorKey, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_SurfaceHasColorKey"), "SDL_SurfaceHasColorKey");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetSurfaceColorKey"), "SDL_GetSurfaceColorKey");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfaceColorMod"), "SDL_SetSurfaceColorMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetSurfaceColorMod"), "SDL_GetSurfaceColorMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfaceAlphaMod"), "SDL_SetSurfaceAlphaMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetSurfaceAlphaMod"), "SDL_GetSurfaceAlphaMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfaceBlendMode"), "SDL_SetSurfaceBlendMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetSurfaceBlendMode"), "SDL_GetSurfaceBlendMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfaceClipRectPointer"), "SDL_SetSurfaceClipRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetSurfaceClipRectRect"), "SDL_SetSurfaceClipRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetSurfaceClipRect"), "SDL_GetSurfaceClipRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_FlipSurface"), "SDL_FlipSurface");
        AssertNativeImport(GetNativeMethod("SDL_RotateSurface"), "SDL_RotateSurface");
        AssertNativeImport(GetNativeMethod("SDL_DuplicateSurface"), "SDL_DuplicateSurface");
        AssertNativeImport(GetNativeMethod("SDL_ScaleSurface"), "SDL_ScaleSurface");
        AssertNativeImport(GetNativeMethod("SDL_ConvertSurface"), "SDL_ConvertSurface");
        AssertNativeImport(GetNativeMethod("SDL_ConvertSurfaceAndColorspace"), "SDL_ConvertSurfaceAndColorspace");
        AssertNativeBoolImport(GetNativeMethod("SDL_ConvertPixelsPointerToPointer"), "SDL_ConvertPixels");
        MethodInfo convertPixelsArrayToPointer = GetNativeMethod("SDL_ConvertPixelsArrayToPointer");
        AssertNativeBoolImport(convertPixelsArrayToPointer, "SDL_ConvertPixels");
        AssertArrayParameterMarshal(convertPixelsArrayToPointer, 3, 4);
        MethodInfo convertPixelsPointerToArray = GetNativeMethod("SDL_ConvertPixelsPointerToArray");
        AssertNativeBoolImport(convertPixelsPointerToArray, "SDL_ConvertPixels");
        AssertArrayParameterMarshal(convertPixelsPointerToArray, 6, 7);
        MethodInfo convertPixelsArrayToArray = GetNativeMethod("SDL_ConvertPixelsArrayToArray");
        AssertNativeBoolImport(convertPixelsArrayToArray, "SDL_ConvertPixels");
        AssertArrayParameterMarshal(convertPixelsArrayToArray, 3, 4);
        AssertArrayParameterMarshal(convertPixelsArrayToArray, 6, 7);
        AssertNativeBoolImport(GetNativeMethod("SDL_ConvertPixelsAndColorspacePointerToPointer"), "SDL_ConvertPixelsAndColorspace");
        MethodInfo convertPixelsAndColorspaceArrayToPointer = GetNativeMethod("SDL_ConvertPixelsAndColorspaceArrayToPointer");
        AssertNativeBoolImport(convertPixelsAndColorspaceArrayToPointer, "SDL_ConvertPixelsAndColorspace");
        AssertArrayParameterMarshal(convertPixelsAndColorspaceArrayToPointer, 5, 6);
        MethodInfo convertPixelsAndColorspacePointerToArray = GetNativeMethod("SDL_ConvertPixelsAndColorspacePointerToArray");
        AssertNativeBoolImport(convertPixelsAndColorspacePointerToArray, "SDL_ConvertPixelsAndColorspace");
        AssertArrayParameterMarshal(convertPixelsAndColorspacePointerToArray, 10, 11);
        MethodInfo convertPixelsAndColorspaceArrayToArray = GetNativeMethod("SDL_ConvertPixelsAndColorspaceArrayToArray");
        AssertNativeBoolImport(convertPixelsAndColorspaceArrayToArray, "SDL_ConvertPixelsAndColorspace");
        AssertArrayParameterMarshal(convertPixelsAndColorspaceArrayToArray, 5, 6);
        AssertArrayParameterMarshal(convertPixelsAndColorspaceArrayToArray, 10, 11);
        MethodInfo premultiplyAlphaPointerToPointer = GetNativeMethod("SDL_PremultiplyAlphaPointerToPointer");
        AssertNativeBoolImport(premultiplyAlphaPointerToPointer, "SDL_PremultiplyAlpha");
        AssertBoolParameterMarshal(premultiplyAlphaPointerToPointer, 8);
        MethodInfo premultiplyAlphaArrayToPointer = GetNativeMethod("SDL_PremultiplyAlphaArrayToPointer");
        AssertNativeBoolImport(premultiplyAlphaArrayToPointer, "SDL_PremultiplyAlpha");
        AssertArrayParameterMarshal(premultiplyAlphaArrayToPointer, 3, 4);
        AssertBoolParameterMarshal(premultiplyAlphaArrayToPointer, 8);
        MethodInfo premultiplyAlphaPointerToArray = GetNativeMethod("SDL_PremultiplyAlphaPointerToArray");
        AssertNativeBoolImport(premultiplyAlphaPointerToArray, "SDL_PremultiplyAlpha");
        AssertArrayParameterMarshal(premultiplyAlphaPointerToArray, 6, 7);
        AssertBoolParameterMarshal(premultiplyAlphaPointerToArray, 8);
        MethodInfo premultiplyAlphaArrayToArray = GetNativeMethod("SDL_PremultiplyAlphaArrayToArray");
        AssertNativeBoolImport(premultiplyAlphaArrayToArray, "SDL_PremultiplyAlpha");
        AssertArrayParameterMarshal(premultiplyAlphaArrayToArray, 3, 4);
        AssertArrayParameterMarshal(premultiplyAlphaArrayToArray, 6, 7);
        AssertBoolParameterMarshal(premultiplyAlphaArrayToArray, 8);
        MethodInfo premultiplySurfaceAlpha = GetNativeMethod("SDL_PremultiplySurfaceAlpha");
        AssertNativeBoolImport(premultiplySurfaceAlpha, "SDL_PremultiplySurfaceAlpha");
        AssertBoolParameterMarshal(premultiplySurfaceAlpha, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_ClearSurface"), "SDL_ClearSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_FillSurfaceRectPointer"), "SDL_FillSurfaceRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_FillSurfaceRectRect"), "SDL_FillSurfaceRect");
        MethodInfo fillSurfaceRects = GetNativeMethod("SDL_FillSurfaceRects");
        AssertNativeBoolImport(fillSurfaceRects, "SDL_FillSurfaceRects");
        AssertArrayParameterMarshal(fillSurfaceRects, 1, 2);
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfacePointerPointer"), "SDL_BlitSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfacePointerRect"), "SDL_BlitSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceRectPointer"), "SDL_BlitSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceRectRect"), "SDL_BlitSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceUnchecked"), "SDL_BlitSurfaceUnchecked");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceScaledPointerPointer"), "SDL_BlitSurfaceScaled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceScaledPointerRect"), "SDL_BlitSurfaceScaled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceScaledRectPointer"), "SDL_BlitSurfaceScaled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceScaledRectRect"), "SDL_BlitSurfaceScaled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceUncheckedScaled"), "SDL_BlitSurfaceUncheckedScaled");
        AssertNativeBoolImport(GetNativeMethod("SDL_StretchSurfaceRectRect"), "SDL_StretchSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_StretchSurfacePointerRect"), "SDL_StretchSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_StretchSurfaceRectPointer"), "SDL_StretchSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_StretchSurfacePointerPointer"), "SDL_StretchSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledPointerPointer"), "SDL_BlitSurfaceTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledPointerRect"), "SDL_BlitSurfaceTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledRectPointer"), "SDL_BlitSurfaceTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledRectRect"), "SDL_BlitSurfaceTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledWithScalePointerPointer"), "SDL_BlitSurfaceTiledWithScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledWithScalePointerRect"), "SDL_BlitSurfaceTiledWithScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledWithScaleRectPointer"), "SDL_BlitSurfaceTiledWithScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurfaceTiledWithScaleRectRect"), "SDL_BlitSurfaceTiledWithScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurface9GridPointerPointer"), "SDL_BlitSurface9Grid");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurface9GridPointerRect"), "SDL_BlitSurface9Grid");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurface9GridRectPointer"), "SDL_BlitSurface9Grid");
        AssertNativeBoolImport(GetNativeMethod("SDL_BlitSurface9GridRectRect"), "SDL_BlitSurface9Grid");
        AssertNativeImport(GetNativeMethod("SDL_MapSurfaceRGB"), "SDL_MapSurfaceRGB");
        AssertNativeImport(GetNativeMethod("SDL_MapSurfaceRGBA"), "SDL_MapSurfaceRGBA");
        AssertNativeBoolImport(GetNativeMethod("SDL_ReadSurfacePixel"), "SDL_ReadSurfacePixel");
        AssertNativeBoolImport(GetNativeMethod("SDL_ReadSurfacePixelFloat"), "SDL_ReadSurfacePixelFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_WriteSurfacePixel"), "SDL_WriteSurfacePixel");
        AssertNativeBoolImport(GetNativeMethod("SDL_WriteSurfacePixelFloat"), "SDL_WriteSurfacePixelFloat");
    }

    public static void SurfaceCreationPropertiesColorspaceAndPaletteFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1001;
        using (NativeHookScope _ = NativeHookScope.Install("CreateSurfaceNativeFunction", nameof(CaptureCreateSurface)))
        {
            IntPtr result = SDL3.SDL.CreateSurface(320, 200, SDL3.SDL.PixelFormat.RGBA8888);

            TestAssert.Equal((IntPtr)0x1001, result, "SDL.CreateSurface must return the native hook value.");
            TestAssert.Equal(320, capturedWidth, "SDL.CreateSurface must forward width.");
            TestAssert.Equal(200, capturedHeight, "SDL.CreateSurface must forward height.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, capturedPixelFormat, "SDL.CreateSurface must forward pixel format.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1011;
        using (NativeHookScope _ = NativeHookScope.Install("CreateSurfaceFromNativeFunction", nameof(CaptureCreateSurfaceFrom)))
        {
            IntPtr result = SDL3.SDL.CreateSurfaceFrom(64, 32, SDL3.SDL.PixelFormat.BGRA8888, (IntPtr)0x1012, 256);

            TestAssert.Equal((IntPtr)0x1011, result, "SDL.CreateSurfaceFrom must return the native hook value.");
            TestAssert.Equal(64, capturedWidth, "SDL.CreateSurfaceFrom must forward width.");
            TestAssert.Equal(32, capturedHeight, "SDL.CreateSurfaceFrom must forward height.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.BGRA8888, capturedPixelFormat, "SDL.CreateSurfaceFrom must forward pixel format.");
            TestAssert.Equal((IntPtr)0x1012, capturedPixels, "SDL.CreateSurfaceFrom must forward pixels.");
            TestAssert.Equal(256, capturedPitch, "SDL.CreateSurfaceFrom must forward pitch.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("DestroySurfaceNativeFunction", nameof(CaptureDestroySurface)))
        {
            SDL3.SDL.DestroySurface((IntPtr)0x1021);

            TestAssert.Equal((IntPtr)0x1021, capturedSurface, "SDL.DestroySurface must forward surface.");
        }

        ResetCaptureState();
        nextUInt = 0x1031u;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfacePropertiesNativeFunction", nameof(CaptureGetSurfaceProperties)))
        {
            uint result = SDL3.SDL.GetSurfaceProperties((IntPtr)0x1032);

            TestAssert.Equal(0x1031u, result, "SDL.GetSurfaceProperties must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1032, capturedSurface, "SDL.GetSurfaceProperties must forward surface.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceColorspaceNativeFunction", nameof(CaptureSetSurfaceColorspace)))
        {
            bool result = SDL3.SDL.SetSurfaceColorspace((IntPtr)0x1041, SDL3.SDL.Colorspace.BT709Full);

            TestAssert.Equal(true, result, "SDL.SetSurfaceColorspace must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1041, capturedSurface, "SDL.SetSurfaceColorspace must forward surface.");
            TestAssert.Equal(SDL3.SDL.Colorspace.BT709Full, capturedColorspace, "SDL.SetSurfaceColorspace must forward colorspace.");
        }

        ResetCaptureState();
        nextColorspace = SDL3.SDL.Colorspace.SRGBLinear;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfaceColorspaceNativeFunction", nameof(CaptureGetSurfaceColorspace)))
        {
            SDL3.SDL.Colorspace result = SDL3.SDL.GetSurfaceColorspace((IntPtr)0x1051);

            TestAssert.Equal(SDL3.SDL.Colorspace.SRGBLinear, result, "SDL.GetSurfaceColorspace must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1051, capturedSurface, "SDL.GetSurfaceColorspace must forward surface.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1061;
        using NativeHookScope createPalette = NativeHookScope.Install("CreateSurfacePaletteNativeFunction", nameof(CaptureCreateSurfacePalette));
        IntPtr palette = SDL3.SDL.CreateSurfacePalette((IntPtr)0x1062);

        TestAssert.Equal((IntPtr)0x1061, palette, "SDL.CreateSurfacePalette must return the native hook value.");
        TestAssert.Equal((IntPtr)0x1062, capturedSurface, "SDL.CreateSurfacePalette must forward surface.");
    }

    public static void SurfacePaletteAlternateImageAndLockFunctions_ForwardInputsOutputsAndManageNativeArrays()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfacePaletteNativeFunction", nameof(CaptureSetSurfacePalette)))
        {
            bool result = SDL3.SDL.SetSurfacePalette((IntPtr)0x2001, (IntPtr)0x2002);

            TestAssert.Equal(true, result, "SDL.SetSurfacePalette must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2001, capturedSurface, "SDL.SetSurfacePalette must forward surface.");
            TestAssert.Equal((IntPtr)0x2002, capturedPalette, "SDL.SetSurfacePalette must forward palette.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x2011;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfacePaletteNativeFunction", nameof(CaptureSurfacePointerReturn)))
        {
            IntPtr result = SDL3.SDL.GetSurfacePalette((IntPtr)0x2012);

            TestAssert.Equal((IntPtr)0x2011, result, "SDL.GetSurfacePalette must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2012, capturedSurface, "SDL.GetSurfacePalette must forward surface.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("AddSurfaceAlternateImageNativeFunction", nameof(CaptureAddSurfaceAlternateImage)))
        {
            bool result = SDL3.SDL.AddSurfaceAlternateImage((IntPtr)0x2021, (IntPtr)0x2022);

            TestAssert.Equal(true, result, "SDL.AddSurfaceAlternateImage must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2021, capturedSurface, "SDL.AddSurfaceAlternateImage must forward surface.");
            TestAssert.Equal((IntPtr)0x2022, capturedImage, "SDL.AddSurfaceAlternateImage must forward image.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SurfaceHasAlternateImagesNativeFunction", nameof(CaptureSurfaceBoolReturn)))
        {
            bool result = SDL3.SDL.SurfaceHasAlternateImages((IntPtr)0x2031);

            TestAssert.Equal(true, result, "SDL.SurfaceHasAlternateImages must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2031, capturedSurface, "SDL.SurfaceHasAlternateImages must forward surface.");
        }

        ResetCaptureState();
        IntPtr[] expectedImages = [(IntPtr)0x2041, (IntPtr)0x2042, (IntPtr)0x2043];
        nextCount = expectedImages.Length;
        nextPointer = AllocatePointerArray(expectedImages);
        using (NativeHookScope imagesHook = NativeHookScope.Install("GetSurfaceImagesNativeFunction", nameof(CaptureGetSurfaceImages)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            try
            {
                IntPtr[]? actual = SDL3.SDL.GetSurfaceImages((IntPtr)0x2044, out int count);

                TestAssert.Equal((IntPtr)0x2044, capturedSurface, "SDL.GetSurfaceImages must forward surface.");
                TestAssert.Equal(expectedImages.Length, count, "SDL.GetSurfaceImages must return native count.");
                TestAssert.NotNull(actual, "SDL.GetSurfaceImages must copy native image pointer arrays.");
                TestAssert.Equal(expectedImages.Length, actual!.Length, "SDL.GetSurfaceImages must preserve array length.");
                for (int i = 0; i < expectedImages.Length; i++)
                {
                    TestAssert.Equal(expectedImages[i], actual[i], $"SDL.GetSurfaceImages must copy image pointer {i}.");
                }

                TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetSurfaceImages must free the native pointer array.");
                TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetSurfaceImages must free the native pointer array once.");
            }
            finally
            {
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        nextCount = 2;
        using (NativeHookScope imagesHook = NativeHookScope.Install("GetSurfaceImagesNativeFunction", nameof(CaptureGetSurfaceImages)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            IntPtr[]? actual = SDL3.SDL.GetSurfaceImages((IntPtr)0x2051, out int count);

            TestAssert.Equal((IntPtr)0x2051, capturedSurface, "SDL.GetSurfaceImages null path must forward surface.");
            TestAssert.Equal(2, count, "SDL.GetSurfaceImages null path must return native count.");
            TestAssert.Equal<IntPtr[]?>(null, actual, "SDL.GetSurfaceImages must return null for native null.");
            TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetSurfaceImages must not free native null.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("RemoveSurfaceAlternateImagesNativeFunction", nameof(CaptureSurfaceVoid)))
        {
            SDL3.SDL.RemoveSurfaceAlternateImages((IntPtr)0x2061);

            TestAssert.Equal((IntPtr)0x2061, capturedSurface, "SDL.RemoveSurfaceAlternateImages must forward surface.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("LockSurfaceNativeFunction", nameof(CaptureSurfaceBoolReturn)))
        {
            bool result = SDL3.SDL.LockSurface((IntPtr)0x2071);

            TestAssert.Equal(true, result, "SDL.LockSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2071, capturedSurface, "SDL.LockSurface must forward surface.");
        }

        ResetCaptureState();
        using NativeHookScope unlock = NativeHookScope.Install("UnlockSurfaceNativeFunction", nameof(CaptureSurfaceVoid));
        SDL3.SDL.UnlockSurface((IntPtr)0x2081);

        TestAssert.Equal((IntPtr)0x2081, capturedSurface, "SDL.UnlockSurface must forward surface.");
    }

    public static void SurfaceLoadAndBmpSaveFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x3001;
        using (NativeHookScope _ = NativeHookScope.Install("LoadSurfaceIONativeFunction", nameof(CaptureLoadSurfaceIO)))
        {
            IntPtr result = SDL3.SDL.LoadSurfaceIO((IntPtr)0x3002, true);

            TestAssert.Equal((IntPtr)0x3001, result, "SDL.LoadSurfaceIO must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3002, capturedSrc, "SDL.LoadSurfaceIO must forward source stream.");
            TestAssert.Equal(true, capturedCloseIO, "SDL.LoadSurfaceIO must forward closeio.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x3011;
        using (NativeHookScope _ = NativeHookScope.Install("LoadSurfaceNativeFunction", nameof(CaptureLoadSurface)))
        {
            IntPtr result = SDL3.SDL.LoadSurface("surface.any");

            TestAssert.Equal((IntPtr)0x3011, result, "SDL.LoadSurface must return the native hook value.");
            TestAssert.Equal("surface.any", capturedFile, "SDL.LoadSurface must forward file.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x3021;
        using (NativeHookScope _ = NativeHookScope.Install("LoadBMPIONativeFunction", nameof(CaptureLoadBMPIO)))
        {
            IntPtr result = SDL3.SDL.LoadBMPIO((IntPtr)0x3022, false);

            TestAssert.Equal((IntPtr)0x3021, result, "SDL.LoadBMPIO must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3022, capturedSrc, "SDL.LoadBMPIO must forward source stream.");
            TestAssert.Equal(false, capturedCloseIO, "SDL.LoadBMPIO must forward closeio.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x3031;
        using (NativeHookScope _ = NativeHookScope.Install("LoadBMPNativeFunction", nameof(CaptureLoadBMP)))
        {
            IntPtr result = SDL3.SDL.LoadBMP("surface.bmp");

            TestAssert.Equal((IntPtr)0x3031, result, "SDL.LoadBMP must return the native hook value.");
            TestAssert.Equal("surface.bmp", capturedFile, "SDL.LoadBMP must forward file.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SaveBMPIONativeFunction", nameof(CaptureSaveBMPIO)))
        {
            bool result = SDL3.SDL.SaveBMPIO((IntPtr)0x3041, (IntPtr)0x3042, true);

            TestAssert.Equal(true, result, "SDL.SaveBMPIO must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3041, capturedSurface, "SDL.SaveBMPIO must forward surface.");
            TestAssert.Equal((IntPtr)0x3042, capturedDst, "SDL.SaveBMPIO must forward destination stream.");
            TestAssert.Equal(true, capturedCloseIO, "SDL.SaveBMPIO must forward closeio.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope saveBmp = NativeHookScope.Install("SaveBMPNativeFunction", nameof(CaptureSaveBMP));
        bool saved = SDL3.SDL.SaveBMP((IntPtr)0x3051, "surface-out.bmp");

        TestAssert.Equal(true, saved, "SDL.SaveBMP must return the native hook value.");
        TestAssert.Equal((IntPtr)0x3051, capturedSurface, "SDL.SaveBMP must forward surface.");
        TestAssert.Equal("surface-out.bmp", capturedFile, "SDL.SaveBMP must forward file.");
    }

    public static void SurfacePngLoadAndSaveFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x4001;
        using (NativeHookScope _ = NativeHookScope.Install("LoadPNGIONativeFunction", nameof(CaptureLoadPNGIO)))
        {
            IntPtr result = SDL3.SDL.LoadPNGIO((IntPtr)0x4002, true);

            TestAssert.Equal((IntPtr)0x4001, result, "SDL.LoadPNGIO must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4002, capturedSrc, "SDL.LoadPNGIO must forward source stream.");
            TestAssert.Equal(true, capturedCloseIO, "SDL.LoadPNGIO must forward closeio.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x4011;
        using (NativeHookScope _ = NativeHookScope.Install("LoadPNGNativeFunction", nameof(CaptureLoadPNG)))
        {
            IntPtr result = SDL3.SDL.LoadPNG("surface.png");

            TestAssert.Equal((IntPtr)0x4011, result, "SDL.LoadPNG must return the native hook value.");
            TestAssert.Equal("surface.png", capturedFile, "SDL.LoadPNG must forward file.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SavePNGIONativeFunction", nameof(CaptureSavePNGIO)))
        {
            bool result = SDL3.SDL.SavePNGIO((IntPtr)0x4021, (IntPtr)0x4022, false);

            TestAssert.Equal(true, result, "SDL.SavePNGIO must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4021, capturedSurface, "SDL.SavePNGIO must forward surface.");
            TestAssert.Equal((IntPtr)0x4022, capturedDst, "SDL.SavePNGIO must forward destination stream.");
            TestAssert.Equal(false, capturedCloseIO, "SDL.SavePNGIO must forward closeio.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope savePng = NativeHookScope.Install("SavePNGNativeFunction", nameof(CaptureSavePNG));
        bool saved = SDL3.SDL.SavePNG((IntPtr)0x4031, "surface-out.png");

        TestAssert.Equal(true, saved, "SDL.SavePNG must return the native hook value.");
        TestAssert.Equal((IntPtr)0x4031, capturedSurface, "SDL.SavePNG must forward surface.");
        TestAssert.Equal("surface-out.png", capturedFile, "SDL.SavePNG must forward file.");
    }

    public static void SurfaceJpgLoadFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x5001;
        using (NativeHookScope _ = NativeHookScope.Install("LoadJPGIONativeFunction", nameof(CaptureLoadJPGIO)))
        {
            IntPtr result = SDL3.SDL.LoadJPGIO((IntPtr)0x5002, true);

            TestAssert.Equal((IntPtr)0x5001, result, "SDL.LoadJPGIO must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5002, capturedSrc, "SDL.LoadJPGIO must forward source stream.");
            TestAssert.Equal(true, capturedCloseIO, "SDL.LoadJPGIO must forward closeio.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x5011;
        using NativeHookScope loadJpg = NativeHookScope.Install("LoadJPGNativeFunction", nameof(CaptureLoadJPG));
        IntPtr loaded = SDL3.SDL.LoadJPG("surface.jpg");

        TestAssert.Equal((IntPtr)0x5011, loaded, "SDL.LoadJPG must return the native hook value.");
        TestAssert.Equal("surface.jpg", capturedFile, "SDL.LoadJPG must forward file.");
    }

    public static void SurfaceRleColorBlendAndClipFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceRLENativeFunction", nameof(CaptureSetSurfaceRLE)))
        {
            bool result = SDL3.SDL.SetSurfaceRLE((IntPtr)0x6001, true);

            TestAssert.Equal(true, result, "SDL.SetSurfaceRLE must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6001, capturedSurface, "SDL.SetSurfaceRLE must forward surface.");
            TestAssert.Equal(true, capturedEnabled, "SDL.SetSurfaceRLE must forward enabled.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SurfaceHasRLENativeFunction", nameof(CaptureSurfaceBoolReturn)))
        {
            bool result = SDL3.SDL.SurfaceHasRLE((IntPtr)0x6011);

            TestAssert.Equal(true, result, "SDL.SurfaceHasRLE must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6011, capturedSurface, "SDL.SurfaceHasRLE must forward surface.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceColorKeyNativeFunction", nameof(CaptureSetSurfaceColorKey)))
        {
            bool result = SDL3.SDL.SetSurfaceColorKey((IntPtr)0x6021, false, 0x00FF00u);

            TestAssert.Equal(true, result, "SDL.SetSurfaceColorKey must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6021, capturedSurface, "SDL.SetSurfaceColorKey must forward surface.");
            TestAssert.Equal(false, capturedEnabled, "SDL.SetSurfaceColorKey must forward enabled.");
            TestAssert.Equal(0x00FF00u, capturedKey, "SDL.SetSurfaceColorKey must forward key.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SurfaceHasColorKeyNativeFunction", nameof(CaptureSurfaceBoolReturn)))
        {
            bool result = SDL3.SDL.SurfaceHasColorKey((IntPtr)0x6031);

            TestAssert.Equal(true, result, "SDL.SurfaceHasColorKey must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6031, capturedSurface, "SDL.SurfaceHasColorKey must forward surface.");
        }

        ResetCaptureState();
        nextBool = true;
        nextUInt = 0x00ABCDEFu;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfaceColorKeyNativeFunction", nameof(CaptureGetSurfaceColorKey)))
        {
            bool result = SDL3.SDL.GetSurfaceColorKey((IntPtr)0x6041, out uint key);

            TestAssert.Equal(true, result, "SDL.GetSurfaceColorKey must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6041, capturedSurface, "SDL.GetSurfaceColorKey must forward surface.");
            TestAssert.Equal(0x00ABCDEFu, key, "SDL.GetSurfaceColorKey must output key.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceColorModNativeFunction", nameof(CaptureSetSurfaceColorMod)))
        {
            bool result = SDL3.SDL.SetSurfaceColorMod((IntPtr)0x6051, 11, 22, 33);

            TestAssert.Equal(true, result, "SDL.SetSurfaceColorMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6051, capturedSurface, "SDL.SetSurfaceColorMod must forward surface.");
            TestAssert.Equal((byte)11, capturedRed, "SDL.SetSurfaceColorMod must forward red.");
            TestAssert.Equal((byte)22, capturedGreen, "SDL.SetSurfaceColorMod must forward green.");
            TestAssert.Equal((byte)33, capturedBlue, "SDL.SetSurfaceColorMod must forward blue.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRed = 44;
        nextGreen = 55;
        nextBlue = 66;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfaceColorModNativeFunction", nameof(CaptureGetSurfaceColorMod)))
        {
            bool result = SDL3.SDL.GetSurfaceColorMod((IntPtr)0x6061, out byte r, out byte g, out byte b);

            TestAssert.Equal(true, result, "SDL.GetSurfaceColorMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6061, capturedSurface, "SDL.GetSurfaceColorMod must forward surface.");
            TestAssert.Equal((byte)44, r, "SDL.GetSurfaceColorMod must output red.");
            TestAssert.Equal((byte)55, g, "SDL.GetSurfaceColorMod must output green.");
            TestAssert.Equal((byte)66, b, "SDL.GetSurfaceColorMod must output blue.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceAlphaModNativeFunction", nameof(CaptureSetSurfaceAlphaMod)))
        {
            bool result = SDL3.SDL.SetSurfaceAlphaMod((IntPtr)0x6071, 77);

            TestAssert.Equal(true, result, "SDL.SetSurfaceAlphaMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6071, capturedSurface, "SDL.SetSurfaceAlphaMod must forward surface.");
            TestAssert.Equal((byte)77, capturedAlpha, "SDL.SetSurfaceAlphaMod must forward alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        nextAlpha = 88;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfaceAlphaModNativeFunction", nameof(CaptureGetSurfaceAlphaMod)))
        {
            bool result = SDL3.SDL.GetSurfaceAlphaMod((IntPtr)0x6081, out byte alpha);

            TestAssert.Equal(true, result, "SDL.GetSurfaceAlphaMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6081, capturedSurface, "SDL.GetSurfaceAlphaMod must forward surface.");
            TestAssert.Equal((byte)88, alpha, "SDL.GetSurfaceAlphaMod must output alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceBlendModeNativeFunction", nameof(CaptureSetSurfaceBlendMode)))
        {
            bool result = SDL3.SDL.SetSurfaceBlendMode((IntPtr)0x6091, SDL3.SDL.BlendMode.BlendPremultiplied);

            TestAssert.Equal(true, result, "SDL.SetSurfaceBlendMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6091, capturedSurface, "SDL.SetSurfaceBlendMode must forward surface.");
            TestAssert.Equal(SDL3.SDL.BlendMode.BlendPremultiplied, capturedBlendMode, "SDL.SetSurfaceBlendMode must forward blend mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBlendMode = SDL3.SDL.BlendMode.Add;
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfaceBlendModeNativeFunction", nameof(CaptureGetSurfaceBlendMode)))
        {
            bool result = SDL3.SDL.GetSurfaceBlendMode((IntPtr)0x60A1, out SDL3.SDL.BlendMode blendMode);

            TestAssert.Equal(true, result, "SDL.GetSurfaceBlendMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x60A1, capturedSurface, "SDL.GetSurfaceBlendMode must forward surface.");
            TestAssert.Equal(SDL3.SDL.BlendMode.Add, blendMode, "SDL.GetSurfaceBlendMode must output blend mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceClipRectPointerNativeFunction", nameof(CaptureSetSurfaceClipRectPointer)))
        {
            bool result = SDL3.SDL.SetSurfaceClipRect((IntPtr)0x60B1, (IntPtr)0x60B2);

            TestAssert.Equal(true, result, "SDL.SetSurfaceClipRect(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x60B1, capturedSurface, "SDL.SetSurfaceClipRect(IntPtr) must forward surface.");
            TestAssert.Equal((IntPtr)0x60B2, capturedRectPointer, "SDL.SetSurfaceClipRect(IntPtr) must forward rect pointer.");
        }

        SDL3.SDL.Rect clip = CreateRect(1, 2, 30, 40);
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetSurfaceClipRectRectNativeFunction", nameof(CaptureSetSurfaceClipRectRect)))
        {
            bool result = SDL3.SDL.SetSurfaceClipRect((IntPtr)0x60C1, in clip);

            TestAssert.Equal(true, result, "SDL.SetSurfaceClipRect(in Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x60C1, capturedSurface, "SDL.SetSurfaceClipRect(in Rect) must forward surface.");
            AssertRect(clip, capturedRect, "SDL.SetSurfaceClipRect(in Rect) must forward rect.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(3, 4, 50, 60);
        using (NativeHookScope _ = NativeHookScope.Install("GetSurfaceClipRectNativeFunction", nameof(CaptureGetSurfaceClipRect)))
        {
            bool result = SDL3.SDL.GetSurfaceClipRect((IntPtr)0x60D1, out SDL3.SDL.Rect rect);

            TestAssert.Equal(true, result, "SDL.GetSurfaceClipRect must return the native hook value.");
            TestAssert.Equal((IntPtr)0x60D1, capturedSurface, "SDL.GetSurfaceClipRect must forward surface.");
            AssertRect(nextRect, rect, "SDL.GetSurfaceClipRect must output rect.");
        }
    }

    public static void SurfaceTransformAndConversionFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("FlipSurfaceNativeFunction", nameof(CaptureFlipSurface)))
        {
            bool result = SDL3.SDL.FlipSurface((IntPtr)0x7001, SDL3.SDL.FlipMode.HorizontalAndVertical);

            TestAssert.Equal(true, result, "SDL.FlipSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7001, capturedSurface, "SDL.FlipSurface must forward surface.");
            TestAssert.Equal(SDL3.SDL.FlipMode.HorizontalAndVertical, capturedFlipMode, "SDL.FlipSurface must forward flip mode.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x7011;
        using (NativeHookScope _ = NativeHookScope.Install("RotateSurfaceNativeFunction", nameof(CaptureRotateSurface)))
        {
            IntPtr result = SDL3.SDL.RotateSurface((IntPtr)0x7012, 45.5f);

            TestAssert.Equal((IntPtr)0x7011, result, "SDL.RotateSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7012, capturedSurface, "SDL.RotateSurface must forward surface.");
            TestAssert.Equal(45.5f, capturedAngle, "SDL.RotateSurface must forward angle.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x7021;
        using (NativeHookScope _ = NativeHookScope.Install("DuplicateSurfaceNativeFunction", nameof(CaptureSurfacePointerReturn)))
        {
            IntPtr result = SDL3.SDL.DuplicateSurface((IntPtr)0x7022);

            TestAssert.Equal((IntPtr)0x7021, result, "SDL.DuplicateSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7022, capturedSurface, "SDL.DuplicateSurface must forward surface.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x7031;
        using (NativeHookScope _ = NativeHookScope.Install("ScaleSurfaceNativeFunction", nameof(CaptureScaleSurface)))
        {
            IntPtr result = SDL3.SDL.ScaleSurface((IntPtr)0x7032, 640, 480, SDL3.SDL.ScaleMode.PixelArt);

            TestAssert.Equal((IntPtr)0x7031, result, "SDL.ScaleSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7032, capturedSurface, "SDL.ScaleSurface must forward surface.");
            TestAssert.Equal(640, capturedWidth, "SDL.ScaleSurface must forward width.");
            TestAssert.Equal(480, capturedHeight, "SDL.ScaleSurface must forward height.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.ScaleSurface must forward scale mode.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x7041;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertSurfaceNativeFunction", nameof(CaptureConvertSurface)))
        {
            IntPtr result = SDL3.SDL.ConvertSurface((IntPtr)0x7042, SDL3.SDL.PixelFormat.RGBA8888);

            TestAssert.Equal((IntPtr)0x7041, result, "SDL.ConvertSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7042, capturedSurface, "SDL.ConvertSurface must forward surface.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, capturedPixelFormat, "SDL.ConvertSurface must forward pixel format.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x7051;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertSurfaceAndColorspaceNativeFunction", nameof(CaptureConvertSurfaceAndColorspace)))
        {
            IntPtr result = SDL3.SDL.ConvertSurfaceAndColorspace(
                (IntPtr)0x7052,
                SDL3.SDL.PixelFormat.BGRA8888,
                (IntPtr)0x7053,
                SDL3.SDL.Colorspace.SRGB,
                0x7054u);

            TestAssert.Equal((IntPtr)0x7051, result, "SDL.ConvertSurfaceAndColorspace must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7052, capturedSurface, "SDL.ConvertSurfaceAndColorspace must forward surface.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.BGRA8888, capturedPixelFormat, "SDL.ConvertSurfaceAndColorspace must forward pixel format.");
            TestAssert.Equal((IntPtr)0x7053, capturedPalette, "SDL.ConvertSurfaceAndColorspace must forward palette.");
            TestAssert.Equal(SDL3.SDL.Colorspace.SRGB, capturedColorspace, "SDL.ConvertSurfaceAndColorspace must forward colorspace.");
            TestAssert.Equal(0x7054u, capturedProps, "SDL.ConvertSurfaceAndColorspace must forward props.");
        }
    }

    public static void SurfaceConvertPixelsFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        byte[] srcBytes = [1, 2, 3, 4];
        byte[] outputBytes = [5, 6, 7, 8];

        ResetCaptureState();
        nextBool = true;
        nextPointer = (IntPtr)0x8001;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsPointerToPointerNativeFunction", nameof(CaptureConvertPixelsPointerToPointer)))
        {
            bool result = SDL3.SDL.ConvertPixels(2, 3, SDL3.SDL.PixelFormat.RGBA8888, (IntPtr)0x8002, 16, SDL3.SDL.PixelFormat.BGRA8888, out IntPtr dst, 32);

            TestAssert.Equal(true, result, "SDL.ConvertPixels(IntPtr, out IntPtr) must return the native hook value.");
            AssertConvertPixelsCommon(2, 3, SDL3.SDL.PixelFormat.RGBA8888, SDL3.SDL.PixelFormat.BGRA8888, 16, 32);
            TestAssert.Equal((IntPtr)0x8002, capturedSrc, "SDL.ConvertPixels(IntPtr, out IntPtr) must forward source pointer.");
            TestAssert.Equal((IntPtr)0x8001, dst, "SDL.ConvertPixels(IntPtr, out IntPtr) must output destination pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        nextPointer = (IntPtr)0x8011;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsArrayToPointerNativeFunction", nameof(CaptureConvertPixelsArrayToPointer)))
        {
            bool result = SDL3.SDL.ConvertPixels(4, 5, SDL3.SDL.PixelFormat.RGB24, srcBytes, 24, SDL3.SDL.PixelFormat.RGBA8888, out IntPtr dst, 48);

            TestAssert.Equal(true, result, "SDL.ConvertPixels(byte[], out IntPtr) must return the native hook value.");
            AssertConvertPixelsCommon(4, 5, SDL3.SDL.PixelFormat.RGB24, SDL3.SDL.PixelFormat.RGBA8888, 24, 48);
            AssertBytes(srcBytes, capturedBytes, "SDL.ConvertPixels(byte[], out IntPtr) must forward source bytes.");
            TestAssert.Equal((IntPtr)0x8011, dst, "SDL.ConvertPixels(byte[], out IntPtr) must output destination pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBytes = outputBytes;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsPointerToArrayNativeFunction", nameof(CaptureConvertPixelsPointerToArray)))
        {
            bool result = SDL3.SDL.ConvertPixels(6, 7, SDL3.SDL.PixelFormat.ARGB8888, (IntPtr)0x8022, 64, SDL3.SDL.PixelFormat.ABGR8888, out byte[] dst, 96);

            TestAssert.Equal(true, result, "SDL.ConvertPixels(IntPtr, out byte[]) must return the native hook value.");
            AssertConvertPixelsCommon(6, 7, SDL3.SDL.PixelFormat.ARGB8888, SDL3.SDL.PixelFormat.ABGR8888, 64, 96);
            TestAssert.Equal((IntPtr)0x8022, capturedSrc, "SDL.ConvertPixels(IntPtr, out byte[]) must forward source pointer.");
            AssertBytes(outputBytes, dst, "SDL.ConvertPixels(IntPtr, out byte[]) must output destination bytes.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBytes = outputBytes;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsArrayToArrayNativeFunction", nameof(CaptureConvertPixelsArrayToArray)))
        {
            bool result = SDL3.SDL.ConvertPixels(8, 9, SDL3.SDL.PixelFormat.XRGB8888, srcBytes, 128, SDL3.SDL.PixelFormat.XBGR8888, out byte[] dst, 256);

            TestAssert.Equal(true, result, "SDL.ConvertPixels(byte[], out byte[]) must return the native hook value.");
            AssertConvertPixelsCommon(8, 9, SDL3.SDL.PixelFormat.XRGB8888, SDL3.SDL.PixelFormat.XBGR8888, 128, 256);
            AssertBytes(srcBytes, capturedBytes, "SDL.ConvertPixels(byte[], out byte[]) must forward source bytes.");
            AssertBytes(outputBytes, dst, "SDL.ConvertPixels(byte[], out byte[]) must output destination bytes.");
        }

        ResetCaptureState();
        nextBool = true;
        nextPointer = (IntPtr)0x8041;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsAndColorspacePointerToPointerNativeFunction", nameof(CaptureConvertPixelsAndColorspacePointerToPointer)))
        {
            bool result = SDL3.SDL.ConvertPixelsAndColorspace(10, 11, SDL3.SDL.PixelFormat.RGBA8888, SDL3.SDL.Colorspace.SRGB, 0x10u, (IntPtr)0x8042, 512, SDL3.SDL.PixelFormat.BGRA8888, SDL3.SDL.Colorspace.SRGBLinear, 0x11u, out IntPtr dst, 1024);

            TestAssert.Equal(true, result, "SDL.ConvertPixelsAndColorspace(IntPtr, out IntPtr) must return the native hook value.");
            AssertConvertPixelsCommon(10, 11, SDL3.SDL.PixelFormat.RGBA8888, SDL3.SDL.PixelFormat.BGRA8888, 512, 1024);
            AssertConvertPixelsColorspaceCommon(SDL3.SDL.Colorspace.SRGB, 0x10u, SDL3.SDL.Colorspace.SRGBLinear, 0x11u);
            TestAssert.Equal((IntPtr)0x8042, capturedSrc, "SDL.ConvertPixelsAndColorspace(IntPtr, out IntPtr) must forward source pointer.");
            TestAssert.Equal((IntPtr)0x8041, dst, "SDL.ConvertPixelsAndColorspace(IntPtr, out IntPtr) must output destination pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        nextPointer = (IntPtr)0x8051;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsAndColorspaceArrayToPointerNativeFunction", nameof(CaptureConvertPixelsAndColorspaceArrayToPointer)))
        {
            bool result = SDL3.SDL.ConvertPixelsAndColorspace(12, 13, SDL3.SDL.PixelFormat.RGB24, SDL3.SDL.Colorspace.SRGBLinear, 0x12u, srcBytes, 2048, SDL3.SDL.PixelFormat.RGBA8888, SDL3.SDL.Colorspace.SRGB, 0x13u, out IntPtr dst, 4096);

            TestAssert.Equal(true, result, "SDL.ConvertPixelsAndColorspace(byte[], out IntPtr) must return the native hook value.");
            AssertConvertPixelsCommon(12, 13, SDL3.SDL.PixelFormat.RGB24, SDL3.SDL.PixelFormat.RGBA8888, 2048, 4096);
            AssertConvertPixelsColorspaceCommon(SDL3.SDL.Colorspace.SRGBLinear, 0x12u, SDL3.SDL.Colorspace.SRGB, 0x13u);
            AssertBytes(srcBytes, capturedBytes, "SDL.ConvertPixelsAndColorspace(byte[], out IntPtr) must forward source bytes.");
            TestAssert.Equal((IntPtr)0x8051, dst, "SDL.ConvertPixelsAndColorspace(byte[], out IntPtr) must output destination pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBytes = outputBytes;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsAndColorspacePointerToArrayNativeFunction", nameof(CaptureConvertPixelsAndColorspacePointerToArray)))
        {
            bool result = SDL3.SDL.ConvertPixelsAndColorspace(14, 15, SDL3.SDL.PixelFormat.ARGB8888, SDL3.SDL.Colorspace.SRGB, 0x14u, (IntPtr)0x8062, 8192, SDL3.SDL.PixelFormat.ABGR8888, SDL3.SDL.Colorspace.SRGBLinear, 0x15u, out byte[] dst, 16384);

            TestAssert.Equal(true, result, "SDL.ConvertPixelsAndColorspace(IntPtr, out byte[]) must return the native hook value.");
            AssertConvertPixelsCommon(14, 15, SDL3.SDL.PixelFormat.ARGB8888, SDL3.SDL.PixelFormat.ABGR8888, 8192, 16384);
            AssertConvertPixelsColorspaceCommon(SDL3.SDL.Colorspace.SRGB, 0x14u, SDL3.SDL.Colorspace.SRGBLinear, 0x15u);
            TestAssert.Equal((IntPtr)0x8062, capturedSrc, "SDL.ConvertPixelsAndColorspace(IntPtr, out byte[]) must forward source pointer.");
            AssertBytes(outputBytes, dst, "SDL.ConvertPixelsAndColorspace(IntPtr, out byte[]) must output destination bytes.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBytes = outputBytes;
        using (NativeHookScope _ = NativeHookScope.Install("ConvertPixelsAndColorspaceArrayToArrayNativeFunction", nameof(CaptureConvertPixelsAndColorspaceArrayToArray)))
        {
            bool result = SDL3.SDL.ConvertPixelsAndColorspace(16, 17, SDL3.SDL.PixelFormat.XRGB8888, SDL3.SDL.Colorspace.SRGBLinear, 0x16u, srcBytes, 32768, SDL3.SDL.PixelFormat.XBGR8888, SDL3.SDL.Colorspace.SRGB, 0x17u, out byte[] dst, 65536);

            TestAssert.Equal(true, result, "SDL.ConvertPixelsAndColorspace(byte[], out byte[]) must return the native hook value.");
            AssertConvertPixelsCommon(16, 17, SDL3.SDL.PixelFormat.XRGB8888, SDL3.SDL.PixelFormat.XBGR8888, 32768, 65536);
            AssertConvertPixelsColorspaceCommon(SDL3.SDL.Colorspace.SRGBLinear, 0x16u, SDL3.SDL.Colorspace.SRGB, 0x17u);
            AssertBytes(srcBytes, capturedBytes, "SDL.ConvertPixelsAndColorspace(byte[], out byte[]) must forward source bytes.");
            AssertBytes(outputBytes, dst, "SDL.ConvertPixelsAndColorspace(byte[], out byte[]) must output destination bytes.");
        }
    }

    public static void SurfacePremultiplyAlphaFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        byte[] srcBytes = [9, 10, 11, 12];
        byte[] outputBytes = [13, 14, 15, 16];

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("PremultiplyAlphaPointerToPointerNativeFunction", nameof(CapturePremultiplyAlphaPointerToPointer)))
        {
            bool result = SDL3.SDL.PremultiplyAlpha(3, 4, SDL3.SDL.PixelFormat.RGBA8888, (IntPtr)0x9001, 24, SDL3.SDL.PixelFormat.BGRA8888, (IntPtr)0x9002, 48, true);

            TestAssert.Equal(true, result, "SDL.PremultiplyAlpha(IntPtr, IntPtr) must return the native hook value.");
            AssertConvertPixelsCommon(3, 4, SDL3.SDL.PixelFormat.RGBA8888, SDL3.SDL.PixelFormat.BGRA8888, 24, 48);
            TestAssert.Equal((IntPtr)0x9001, capturedSrc, "SDL.PremultiplyAlpha(IntPtr, IntPtr) must forward source pointer.");
            TestAssert.Equal((IntPtr)0x9002, capturedDst, "SDL.PremultiplyAlpha(IntPtr, IntPtr) must forward destination pointer.");
            TestAssert.Equal(true, capturedLinear, "SDL.PremultiplyAlpha(IntPtr, IntPtr) must forward linear.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("PremultiplyAlphaArrayToPointerNativeFunction", nameof(CapturePremultiplyAlphaArrayToPointer)))
        {
            bool result = SDL3.SDL.PremultiplyAlpha(5, 6, SDL3.SDL.PixelFormat.RGB24, srcBytes, 64, SDL3.SDL.PixelFormat.RGBA8888, (IntPtr)0x9012, 128, false);

            TestAssert.Equal(true, result, "SDL.PremultiplyAlpha(byte[], IntPtr) must return the native hook value.");
            AssertConvertPixelsCommon(5, 6, SDL3.SDL.PixelFormat.RGB24, SDL3.SDL.PixelFormat.RGBA8888, 64, 128);
            AssertBytes(srcBytes, capturedBytes, "SDL.PremultiplyAlpha(byte[], IntPtr) must forward source bytes.");
            TestAssert.Equal((IntPtr)0x9012, capturedDst, "SDL.PremultiplyAlpha(byte[], IntPtr) must forward destination pointer.");
            TestAssert.Equal(false, capturedLinear, "SDL.PremultiplyAlpha(byte[], IntPtr) must forward linear.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBytes = outputBytes;
        using (NativeHookScope _ = NativeHookScope.Install("PremultiplyAlphaPointerToArrayNativeFunction", nameof(CapturePremultiplyAlphaPointerToArray)))
        {
            bool result = SDL3.SDL.PremultiplyAlpha(7, 8, SDL3.SDL.PixelFormat.ARGB8888, (IntPtr)0x9021, 256, SDL3.SDL.PixelFormat.ABGR8888, out byte[] dst, 512, true);

            TestAssert.Equal(true, result, "SDL.PremultiplyAlpha(IntPtr, out byte[]) must return the native hook value.");
            AssertConvertPixelsCommon(7, 8, SDL3.SDL.PixelFormat.ARGB8888, SDL3.SDL.PixelFormat.ABGR8888, 256, 512);
            TestAssert.Equal((IntPtr)0x9021, capturedSrc, "SDL.PremultiplyAlpha(IntPtr, out byte[]) must forward source pointer.");
            AssertBytes(outputBytes, dst, "SDL.PremultiplyAlpha(IntPtr, out byte[]) must output destination bytes.");
            TestAssert.Equal(true, capturedLinear, "SDL.PremultiplyAlpha(IntPtr, out byte[]) must forward linear.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBytes = outputBytes;
        using (NativeHookScope _ = NativeHookScope.Install("PremultiplyAlphaArrayToArrayNativeFunction", nameof(CapturePremultiplyAlphaArrayToArray)))
        {
            bool result = SDL3.SDL.PremultiplyAlpha(9, 10, SDL3.SDL.PixelFormat.XRGB8888, srcBytes, 1024, SDL3.SDL.PixelFormat.XBGR8888, out byte[] dst, 2048, false);

            TestAssert.Equal(true, result, "SDL.PremultiplyAlpha(byte[], out byte[]) must return the native hook value.");
            AssertConvertPixelsCommon(9, 10, SDL3.SDL.PixelFormat.XRGB8888, SDL3.SDL.PixelFormat.XBGR8888, 1024, 2048);
            AssertBytes(srcBytes, capturedBytes, "SDL.PremultiplyAlpha(byte[], out byte[]) must forward source bytes.");
            AssertBytes(outputBytes, dst, "SDL.PremultiplyAlpha(byte[], out byte[]) must output destination bytes.");
            TestAssert.Equal(false, capturedLinear, "SDL.PremultiplyAlpha(byte[], out byte[]) must forward linear.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("PremultiplySurfaceAlphaNativeFunction", nameof(CapturePremultiplySurfaceAlpha)))
        {
            bool result = SDL3.SDL.PremultiplySurfaceAlpha((IntPtr)0x9041, true);

            TestAssert.Equal(true, result, "SDL.PremultiplySurfaceAlpha must return the native hook value.");
            TestAssert.Equal((IntPtr)0x9041, capturedSurface, "SDL.PremultiplySurfaceAlpha must forward surface.");
            TestAssert.Equal(true, capturedLinear, "SDL.PremultiplySurfaceAlpha must forward linear.");
        }
    }

    public static void SurfaceClearAndFillFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect rect = CreateRect(2, 4, 8, 16);
        SDL3.SDL.Rect[] rects = [CreateRect(1, 2, 3, 4), CreateRect(5, 6, 7, 8)];

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("ClearSurfaceNativeFunction", nameof(CaptureClearSurface)))
        {
            bool result = SDL3.SDL.ClearSurface((IntPtr)0xA001, 0.1f, 0.2f, 0.3f, 0.4f);

            TestAssert.Equal(true, result, "SDL.ClearSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA001, capturedSurface, "SDL.ClearSurface must forward surface.");
            TestAssert.Equal(0.1f, capturedFloatRed, "SDL.ClearSurface must forward red.");
            TestAssert.Equal(0.2f, capturedFloatGreen, "SDL.ClearSurface must forward green.");
            TestAssert.Equal(0.3f, capturedFloatBlue, "SDL.ClearSurface must forward blue.");
            TestAssert.Equal(0.4f, capturedFloatAlpha, "SDL.ClearSurface must forward alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("FillSurfaceRectPointerNativeFunction", nameof(CaptureFillSurfaceRectPointer)))
        {
            bool result = SDL3.SDL.FillSurfaceRect((IntPtr)0xA011, (IntPtr)0xA012, 0xA013u);

            TestAssert.Equal(true, result, "SDL.FillSurfaceRect(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA011, capturedDst, "SDL.FillSurfaceRect(IntPtr) must forward destination surface.");
            TestAssert.Equal((IntPtr)0xA012, capturedRectPointer, "SDL.FillSurfaceRect(IntPtr) must forward rect pointer.");
            TestAssert.Equal(0xA013u, capturedColor, "SDL.FillSurfaceRect(IntPtr) must forward color.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("FillSurfaceRectRectNativeFunction", nameof(CaptureFillSurfaceRectRect)))
        {
            bool result = SDL3.SDL.FillSurfaceRect((IntPtr)0xA021, in rect, 0xA023u);

            TestAssert.Equal(true, result, "SDL.FillSurfaceRect(in Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA021, capturedDst, "SDL.FillSurfaceRect(in Rect) must forward destination surface.");
            AssertRect(rect, capturedRect, "SDL.FillSurfaceRect(in Rect) must forward rect.");
            TestAssert.Equal(0xA023u, capturedColor, "SDL.FillSurfaceRect(in Rect) must forward color.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("FillSurfaceRectsNativeFunction", nameof(CaptureFillSurfaceRects)))
        {
            bool result = SDL3.SDL.FillSurfaceRects((IntPtr)0xA031, rects, rects.Length, 0xA034u);

            TestAssert.Equal(true, result, "SDL.FillSurfaceRects must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA031, capturedDst, "SDL.FillSurfaceRects must forward destination surface.");
            TestAssert.Equal(rects.Length, nextCount, "SDL.FillSurfaceRects must forward count.");
            AssertRectArray(rects, capturedRects, "SDL.FillSurfaceRects must forward rect array.");
            TestAssert.Equal(0xA034u, capturedColor, "SDL.FillSurfaceRects must forward color.");
        }
    }

    public static void SurfaceBlitFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect srcRect = CreateRect(1, 2, 30, 40);
        SDL3.SDL.Rect dstRect = CreateRect(5, 6, 70, 80);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfacePointerPointerNativeFunction", nameof(CaptureBlitSurfacePointerPointer)))
        {
            bool result = SDL3.SDL.BlitSurface((IntPtr)0xB001, (IntPtr)0xB002, (IntPtr)0xB003, (IntPtr)0xB004);

            TestAssert.Equal(true, result, "SDL.BlitSurface(IntPtr, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB001, (IntPtr)0xB003);
            TestAssert.Equal((IntPtr)0xB002, capturedSrcRectPointer, "SDL.BlitSurface(IntPtr, IntPtr) must forward source rect pointer.");
            TestAssert.Equal((IntPtr)0xB004, capturedDstRectPointer, "SDL.BlitSurface(IntPtr, IntPtr) must forward destination rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfacePointerRectNativeFunction", nameof(CaptureBlitSurfacePointerRect)))
        {
            bool result = SDL3.SDL.BlitSurface((IntPtr)0xB011, (IntPtr)0xB012, (IntPtr)0xB013, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurface(IntPtr, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB011, (IntPtr)0xB013);
            TestAssert.Equal((IntPtr)0xB012, capturedSrcRectPointer, "SDL.BlitSurface(IntPtr, in Rect) must forward source rect pointer.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurface(IntPtr, in Rect) must forward destination rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceRectPointerNativeFunction", nameof(CaptureBlitSurfaceRectPointer)))
        {
            bool result = SDL3.SDL.BlitSurface((IntPtr)0xB021, in srcRect, (IntPtr)0xB023, (IntPtr)0xB024);

            TestAssert.Equal(true, result, "SDL.BlitSurface(in Rect, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB021, (IntPtr)0xB023);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurface(in Rect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0xB024, capturedDstRectPointer, "SDL.BlitSurface(in Rect, IntPtr) must forward destination rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceRectRectNativeFunction", nameof(CaptureBlitSurfaceRectRect)))
        {
            bool result = SDL3.SDL.BlitSurface((IntPtr)0xB031, in srcRect, (IntPtr)0xB033, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurface(in Rect, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB031, (IntPtr)0xB033);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurface(in Rect, in Rect) must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurface(in Rect, in Rect) must forward destination rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceUncheckedNativeFunction", nameof(CaptureBlitSurfaceRectRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceUnchecked((IntPtr)0xB041, in srcRect, (IntPtr)0xB043, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceUnchecked must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB041, (IntPtr)0xB043);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceUnchecked must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceUnchecked must forward destination rect.");
        }
    }

    public static void SurfaceScaledBlitFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect srcRect = CreateRect(11, 12, 130, 140);
        SDL3.SDL.Rect dstRect = CreateRect(15, 16, 170, 180);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceScaledPointerPointerNativeFunction", nameof(CaptureBlitSurfaceScaledPointerPointer)))
        {
            bool result = SDL3.SDL.BlitSurfaceScaled((IntPtr)0xB101, (IntPtr)0xB102, (IntPtr)0xB103, (IntPtr)0xB104, SDL3.SDL.ScaleMode.PixelArt);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceScaled(IntPtr, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB101, (IntPtr)0xB103);
            TestAssert.Equal((IntPtr)0xB102, capturedSrcRectPointer, "SDL.BlitSurfaceScaled(IntPtr, IntPtr) must forward source rect pointer.");
            TestAssert.Equal((IntPtr)0xB104, capturedDstRectPointer, "SDL.BlitSurfaceScaled(IntPtr, IntPtr) must forward destination rect pointer.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.BlitSurfaceScaled(IntPtr, IntPtr) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceScaledPointerRectNativeFunction", nameof(CaptureBlitSurfaceScaledPointerRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceScaled((IntPtr)0xB111, (IntPtr)0xB112, (IntPtr)0xB113, in dstRect, SDL3.SDL.ScaleMode.Linear);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceScaled(IntPtr, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB111, (IntPtr)0xB113);
            TestAssert.Equal((IntPtr)0xB112, capturedSrcRectPointer, "SDL.BlitSurfaceScaled(IntPtr, in Rect) must forward source rect pointer.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceScaled(IntPtr, in Rect) must forward destination rect.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Linear, capturedScaleMode, "SDL.BlitSurfaceScaled(IntPtr, in Rect) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceScaledRectPointerNativeFunction", nameof(CaptureBlitSurfaceScaledRectPointer)))
        {
            bool result = SDL3.SDL.BlitSurfaceScaled((IntPtr)0xB121, in srcRect, (IntPtr)0xB123, (IntPtr)0xB124, SDL3.SDL.ScaleMode.Nearest);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceScaled(in Rect, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB121, (IntPtr)0xB123);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceScaled(in Rect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0xB124, capturedDstRectPointer, "SDL.BlitSurfaceScaled(in Rect, IntPtr) must forward destination rect pointer.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Nearest, capturedScaleMode, "SDL.BlitSurfaceScaled(in Rect, IntPtr) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceScaledRectRectNativeFunction", nameof(CaptureBlitSurfaceScaledRectRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceScaled((IntPtr)0xB131, in srcRect, (IntPtr)0xB133, in dstRect, SDL3.SDL.ScaleMode.PixelArt);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceScaled(in Rect, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB131, (IntPtr)0xB133);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceScaled(in Rect, in Rect) must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceScaled(in Rect, in Rect) must forward destination rect.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.BlitSurfaceScaled(in Rect, in Rect) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceUncheckedScaledNativeFunction", nameof(CaptureBlitSurfaceScaledRectRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceUncheckedScaled((IntPtr)0xB141, in srcRect, (IntPtr)0xB143, in dstRect, SDL3.SDL.ScaleMode.Linear);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceUncheckedScaled must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB141, (IntPtr)0xB143);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceUncheckedScaled must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceUncheckedScaled must forward destination rect.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Linear, capturedScaleMode, "SDL.BlitSurfaceUncheckedScaled must forward scale mode.");
        }
    }

    public static void SurfaceStretchFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect srcRect = CreateRect(21, 22, 230, 240);
        SDL3.SDL.Rect dstRect = CreateRect(25, 26, 270, 280);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("StretchSurfaceRectRectNativeFunction", nameof(CaptureBlitSurfaceScaledRectRect)))
        {
            bool result = SDL3.SDL.StretchSurface((IntPtr)0xB201, in srcRect, (IntPtr)0xB203, in dstRect, SDL3.SDL.ScaleMode.Linear);

            TestAssert.Equal(true, result, "SDL.StretchSurface(in Rect, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB201, (IntPtr)0xB203);
            AssertRect(srcRect, capturedSrcRect, "SDL.StretchSurface(in Rect, in Rect) must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.StretchSurface(in Rect, in Rect) must forward destination rect.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Linear, capturedScaleMode, "SDL.StretchSurface(in Rect, in Rect) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("StretchSurfacePointerRectNativeFunction", nameof(CaptureBlitSurfaceScaledPointerRect)))
        {
            bool result = SDL3.SDL.StretchSurface((IntPtr)0xB211, (IntPtr)0xB212, (IntPtr)0xB213, in dstRect, SDL3.SDL.ScaleMode.PixelArt);

            TestAssert.Equal(true, result, "SDL.StretchSurface(IntPtr, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB211, (IntPtr)0xB213);
            TestAssert.Equal((IntPtr)0xB212, capturedSrcRectPointer, "SDL.StretchSurface(IntPtr, in Rect) must forward source rect pointer.");
            AssertRect(dstRect, capturedDstRect, "SDL.StretchSurface(IntPtr, in Rect) must forward destination rect.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.StretchSurface(IntPtr, in Rect) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("StretchSurfaceRectPointerNativeFunction", nameof(CaptureBlitSurfaceScaledRectPointer)))
        {
            bool result = SDL3.SDL.StretchSurface((IntPtr)0xB221, in srcRect, (IntPtr)0xB223, (IntPtr)0xB224, SDL3.SDL.ScaleMode.Nearest);

            TestAssert.Equal(true, result, "SDL.StretchSurface(in Rect, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB221, (IntPtr)0xB223);
            AssertRect(srcRect, capturedSrcRect, "SDL.StretchSurface(in Rect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0xB224, capturedDstRectPointer, "SDL.StretchSurface(in Rect, IntPtr) must forward destination rect pointer.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Nearest, capturedScaleMode, "SDL.StretchSurface(in Rect, IntPtr) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("StretchSurfacePointerPointerNativeFunction", nameof(CaptureBlitSurfaceScaledPointerPointer)))
        {
            bool result = SDL3.SDL.StretchSurface((IntPtr)0xB231, (IntPtr)0xB232, (IntPtr)0xB233, (IntPtr)0xB234, SDL3.SDL.ScaleMode.Linear);

            TestAssert.Equal(true, result, "SDL.StretchSurface(IntPtr, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB231, (IntPtr)0xB233);
            TestAssert.Equal((IntPtr)0xB232, capturedSrcRectPointer, "SDL.StretchSurface(IntPtr, IntPtr) must forward source rect pointer.");
            TestAssert.Equal((IntPtr)0xB234, capturedDstRectPointer, "SDL.StretchSurface(IntPtr, IntPtr) must forward destination rect pointer.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Linear, capturedScaleMode, "SDL.StretchSurface(IntPtr, IntPtr) must forward scale mode.");
        }
    }

    public static void SurfaceTiledBlitFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect srcRect = CreateRect(31, 32, 330, 340);
        SDL3.SDL.Rect dstRect = CreateRect(35, 36, 370, 380);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledPointerPointerNativeFunction", nameof(CaptureBlitSurfacePointerPointer)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiled((IntPtr)0xB301, (IntPtr)0xB302, (IntPtr)0xB303, (IntPtr)0xB304);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiled(IntPtr, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB301, (IntPtr)0xB303);
            TestAssert.Equal((IntPtr)0xB302, capturedSrcRectPointer, "SDL.BlitSurfaceTiled(IntPtr, IntPtr) must forward source rect pointer.");
            TestAssert.Equal((IntPtr)0xB304, capturedDstRectPointer, "SDL.BlitSurfaceTiled(IntPtr, IntPtr) must forward destination rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledPointerRectNativeFunction", nameof(CaptureBlitSurfacePointerRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiled((IntPtr)0xB311, (IntPtr)0xB312, (IntPtr)0xB313, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiled(IntPtr, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB311, (IntPtr)0xB313);
            TestAssert.Equal((IntPtr)0xB312, capturedSrcRectPointer, "SDL.BlitSurfaceTiled(IntPtr, in Rect) must forward source rect pointer.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceTiled(IntPtr, in Rect) must forward destination rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledRectPointerNativeFunction", nameof(CaptureBlitSurfaceRectPointer)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiled((IntPtr)0xB321, in srcRect, (IntPtr)0xB323, (IntPtr)0xB324);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiled(in Rect, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB321, (IntPtr)0xB323);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceTiled(in Rect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0xB324, capturedDstRectPointer, "SDL.BlitSurfaceTiled(in Rect, IntPtr) must forward destination rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledRectRectNativeFunction", nameof(CaptureBlitSurfaceRectRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiled((IntPtr)0xB331, in srcRect, (IntPtr)0xB333, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiled(in Rect, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB331, (IntPtr)0xB333);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceTiled(in Rect, in Rect) must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceTiled(in Rect, in Rect) must forward destination rect.");
        }
    }

    public static void SurfaceTiledBlitWithScaleFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect srcRect = CreateRect(41, 42, 430, 440);
        SDL3.SDL.Rect dstRect = CreateRect(45, 46, 470, 480);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledWithScalePointerPointerNativeFunction", nameof(CaptureBlitSurfaceTiledWithScalePointerPointer)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiledWithScale((IntPtr)0xB401, (IntPtr)0xB402, 1.5f, SDL3.SDL.ScaleMode.PixelArt, (IntPtr)0xB403, (IntPtr)0xB404);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiledWithScale(IntPtr, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB401, (IntPtr)0xB403);
            TestAssert.Equal((IntPtr)0xB402, capturedSrcRectPointer, "SDL.BlitSurfaceTiledWithScale(IntPtr, IntPtr) must forward source rect pointer.");
            TestAssert.Equal((IntPtr)0xB404, capturedDstRectPointer, "SDL.BlitSurfaceTiledWithScale(IntPtr, IntPtr) must forward destination rect pointer.");
            TestAssert.Equal(1.5f, capturedScale, "SDL.BlitSurfaceTiledWithScale(IntPtr, IntPtr) must forward scale.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.BlitSurfaceTiledWithScale(IntPtr, IntPtr) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledWithScalePointerRectNativeFunction", nameof(CaptureBlitSurfaceTiledWithScalePointerRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiledWithScale((IntPtr)0xB411, (IntPtr)0xB412, 2.5f, SDL3.SDL.ScaleMode.Linear, (IntPtr)0xB413, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiledWithScale(IntPtr, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB411, (IntPtr)0xB413);
            TestAssert.Equal((IntPtr)0xB412, capturedSrcRectPointer, "SDL.BlitSurfaceTiledWithScale(IntPtr, in Rect) must forward source rect pointer.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceTiledWithScale(IntPtr, in Rect) must forward destination rect.");
            TestAssert.Equal(2.5f, capturedScale, "SDL.BlitSurfaceTiledWithScale(IntPtr, in Rect) must forward scale.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Linear, capturedScaleMode, "SDL.BlitSurfaceTiledWithScale(IntPtr, in Rect) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledWithScaleRectPointerNativeFunction", nameof(CaptureBlitSurfaceTiledWithScaleRectPointer)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiledWithScale((IntPtr)0xB421, in srcRect, 3.5f, SDL3.SDL.ScaleMode.Nearest, (IntPtr)0xB423, (IntPtr)0xB424);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiledWithScale(in Rect, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB421, (IntPtr)0xB423);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceTiledWithScale(in Rect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0xB424, capturedDstRectPointer, "SDL.BlitSurfaceTiledWithScale(in Rect, IntPtr) must forward destination rect pointer.");
            TestAssert.Equal(3.5f, capturedScale, "SDL.BlitSurfaceTiledWithScale(in Rect, IntPtr) must forward scale.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.Nearest, capturedScaleMode, "SDL.BlitSurfaceTiledWithScale(in Rect, IntPtr) must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurfaceTiledWithScaleRectRectNativeFunction", nameof(CaptureBlitSurfaceTiledWithScaleRectRect)))
        {
            bool result = SDL3.SDL.BlitSurfaceTiledWithScale((IntPtr)0xB431, in srcRect, 4.5f, SDL3.SDL.ScaleMode.PixelArt, (IntPtr)0xB433, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurfaceTiledWithScale(in Rect, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB431, (IntPtr)0xB433);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurfaceTiledWithScale(in Rect, in Rect) must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurfaceTiledWithScale(in Rect, in Rect) must forward destination rect.");
            TestAssert.Equal(4.5f, capturedScale, "SDL.BlitSurfaceTiledWithScale(in Rect, in Rect) must forward scale.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.BlitSurfaceTiledWithScale(in Rect, in Rect) must forward scale mode.");
        }
    }

    public static void Surface9GridBlitFunctions_ForwardInputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect srcRect = CreateRect(51, 52, 530, 540);
        SDL3.SDL.Rect dstRect = CreateRect(55, 56, 570, 580);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurface9GridPointerPointerNativeFunction", nameof(CaptureBlitSurface9GridPointerPointer)))
        {
            bool result = SDL3.SDL.BlitSurface9Grid((IntPtr)0xB501, (IntPtr)0xB502, 1, 2, 3, 4, 1.25f, SDL3.SDL.ScaleMode.PixelArt, (IntPtr)0xB503, (IntPtr)0xB504);

            TestAssert.Equal(true, result, "SDL.BlitSurface9Grid(IntPtr, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB501, (IntPtr)0xB503);
            TestAssert.Equal((IntPtr)0xB502, capturedSrcRectPointer, "SDL.BlitSurface9Grid(IntPtr, IntPtr) must forward source rect pointer.");
            TestAssert.Equal((IntPtr)0xB504, capturedDstRectPointer, "SDL.BlitSurface9Grid(IntPtr, IntPtr) must forward destination rect pointer.");
            Assert9GridScalars(1, 2, 3, 4, 1.25f, SDL3.SDL.ScaleMode.PixelArt, "SDL.BlitSurface9Grid(IntPtr, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurface9GridPointerRectNativeFunction", nameof(CaptureBlitSurface9GridPointerRect)))
        {
            bool result = SDL3.SDL.BlitSurface9Grid((IntPtr)0xB511, (IntPtr)0xB512, 5, 6, 7, 8, 2.25f, SDL3.SDL.ScaleMode.Linear, (IntPtr)0xB513, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurface9Grid(IntPtr, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB511, (IntPtr)0xB513);
            TestAssert.Equal((IntPtr)0xB512, capturedSrcRectPointer, "SDL.BlitSurface9Grid(IntPtr, in Rect) must forward source rect pointer.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurface9Grid(IntPtr, in Rect) must forward destination rect.");
            Assert9GridScalars(5, 6, 7, 8, 2.25f, SDL3.SDL.ScaleMode.Linear, "SDL.BlitSurface9Grid(IntPtr, in Rect)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurface9GridRectPointerNativeFunction", nameof(CaptureBlitSurface9GridRectPointer)))
        {
            bool result = SDL3.SDL.BlitSurface9Grid((IntPtr)0xB521, in srcRect, 9, 10, 11, 12, 3.25f, SDL3.SDL.ScaleMode.Nearest, (IntPtr)0xB523, (IntPtr)0xB524);

            TestAssert.Equal(true, result, "SDL.BlitSurface9Grid(in Rect, IntPtr) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB521, (IntPtr)0xB523);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurface9Grid(in Rect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0xB524, capturedDstRectPointer, "SDL.BlitSurface9Grid(in Rect, IntPtr) must forward destination rect pointer.");
            Assert9GridScalars(9, 10, 11, 12, 3.25f, SDL3.SDL.ScaleMode.Nearest, "SDL.BlitSurface9Grid(in Rect, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("BlitSurface9GridRectRectNativeFunction", nameof(CaptureBlitSurface9GridRectRect)))
        {
            bool result = SDL3.SDL.BlitSurface9Grid((IntPtr)0xB531, in srcRect, 13, 14, 15, 16, 4.25f, SDL3.SDL.ScaleMode.PixelArt, (IntPtr)0xB533, in dstRect);

            TestAssert.Equal(true, result, "SDL.BlitSurface9Grid(in Rect, in Rect) must return the native hook value.");
            AssertBlitSurfaces((IntPtr)0xB531, (IntPtr)0xB533);
            AssertRect(srcRect, capturedSrcRect, "SDL.BlitSurface9Grid(in Rect, in Rect) must forward source rect.");
            AssertRect(dstRect, capturedDstRect, "SDL.BlitSurface9Grid(in Rect, in Rect) must forward destination rect.");
            Assert9GridScalars(13, 14, 15, 16, 4.25f, SDL3.SDL.ScaleMode.PixelArt, "SDL.BlitSurface9Grid(in Rect, in Rect)");
        }
    }

    public static void SurfacePixelFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextUInt = 0xC001u;
        using (NativeHookScope _ = NativeHookScope.Install("MapSurfaceRGBNativeFunction", nameof(CaptureMapSurfaceRGB)))
        {
            uint result = SDL3.SDL.MapSurfaceRGB((IntPtr)0xC002, 10, 20, 30);

            TestAssert.Equal(0xC001u, result, "SDL.MapSurfaceRGB must return the native hook value.");
            TestAssert.Equal((IntPtr)0xC002, capturedSurface, "SDL.MapSurfaceRGB must forward surface.");
            AssertCapturedByteChannels(10, 20, 30, 0, "SDL.MapSurfaceRGB");
        }

        ResetCaptureState();
        nextUInt = 0xC011u;
        using (NativeHookScope _ = NativeHookScope.Install("MapSurfaceRGBANativeFunction", nameof(CaptureMapSurfaceRGBA)))
        {
            uint result = SDL3.SDL.MapSurfaceRGBA((IntPtr)0xC012, 11, 21, 31, 41);

            TestAssert.Equal(0xC011u, result, "SDL.MapSurfaceRGBA must return the native hook value.");
            TestAssert.Equal((IntPtr)0xC012, capturedSurface, "SDL.MapSurfaceRGBA must forward surface.");
            AssertCapturedByteChannels(11, 21, 31, 41, "SDL.MapSurfaceRGBA");
        }

        ResetCaptureState();
        nextBool = true;
        nextRed = 12;
        nextGreen = 22;
        nextBlue = 32;
        nextAlpha = 42;
        using (NativeHookScope _ = NativeHookScope.Install("ReadSurfacePixelNativeFunction", nameof(CaptureReadSurfacePixel)))
        {
            bool result = SDL3.SDL.ReadSurfacePixel((IntPtr)0xC021, 17, 18, out byte r, out byte g, out byte b, out byte a);

            TestAssert.Equal(true, result, "SDL.ReadSurfacePixel must return the native hook value.");
            AssertSurfaceAndCoordinates((IntPtr)0xC021, 17, 18, "SDL.ReadSurfacePixel");
            TestAssert.Equal(12, r, "SDL.ReadSurfacePixel must output red.");
            TestAssert.Equal(22, g, "SDL.ReadSurfacePixel must output green.");
            TestAssert.Equal(32, b, "SDL.ReadSurfacePixel must output blue.");
            TestAssert.Equal(42, a, "SDL.ReadSurfacePixel must output alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        nextFloatRed = 0.12f;
        nextFloatGreen = 0.22f;
        nextFloatBlue = 0.32f;
        nextFloatAlpha = 0.42f;
        using (NativeHookScope _ = NativeHookScope.Install("ReadSurfacePixelFloatNativeFunction", nameof(CaptureReadSurfacePixelFloat)))
        {
            bool result = SDL3.SDL.ReadSurfacePixelFloat((IntPtr)0xC031, 19, 20, out float r, out float g, out float b, out float a);

            TestAssert.Equal(true, result, "SDL.ReadSurfacePixelFloat must return the native hook value.");
            AssertSurfaceAndCoordinates((IntPtr)0xC031, 19, 20, "SDL.ReadSurfacePixelFloat");
            TestAssert.Equal(0.12f, r, "SDL.ReadSurfacePixelFloat must output red.");
            TestAssert.Equal(0.22f, g, "SDL.ReadSurfacePixelFloat must output green.");
            TestAssert.Equal(0.32f, b, "SDL.ReadSurfacePixelFloat must output blue.");
            TestAssert.Equal(0.42f, a, "SDL.ReadSurfacePixelFloat must output alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("WriteSurfacePixelNativeFunction", nameof(CaptureWriteSurfacePixel)))
        {
            bool result = SDL3.SDL.WriteSurfacePixel((IntPtr)0xC041, 21, 22, 13, 23, 33, 43);

            TestAssert.Equal(true, result, "SDL.WriteSurfacePixel must return the native hook value.");
            AssertSurfaceAndCoordinates((IntPtr)0xC041, 21, 22, "SDL.WriteSurfacePixel");
            AssertCapturedByteChannels(13, 23, 33, 43, "SDL.WriteSurfacePixel");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("WriteSurfacePixelFloatNativeFunction", nameof(CaptureWriteSurfacePixelFloat)))
        {
            bool result = SDL3.SDL.WriteSurfacePixelFloat((IntPtr)0xC051, 23, 24, 0.13f, 0.23f, 0.33f, 0.43f);

            TestAssert.Equal(true, result, "SDL.WriteSurfacePixelFloat must return the native hook value.");
            AssertSurfaceAndCoordinates((IntPtr)0xC051, 23, 24, "SDL.WriteSurfacePixelFloat");
            AssertCapturedFloatChannels(0.13f, 0.23f, 0.33f, 0.43f, "SDL.WriteSurfacePixelFloat");
        }
    }

    private static IntPtr CaptureCreateSurface(int width, int height, SDL3.SDL.PixelFormat format)
    {
        capturedWidth = width;
        capturedHeight = height;
        capturedPixelFormat = format;
        return nextPointer;
    }

    private static IntPtr CaptureCreateSurfaceFrom(int width, int height, SDL3.SDL.PixelFormat format, IntPtr pixels, int pitch)
    {
        capturedWidth = width;
        capturedHeight = height;
        capturedPixelFormat = format;
        capturedPixels = pixels;
        capturedPitch = pitch;
        return nextPointer;
    }

    private static void CaptureDestroySurface(IntPtr surface)
    {
        capturedSurface = surface;
    }

    private static uint CaptureGetSurfaceProperties(IntPtr surface)
    {
        capturedSurface = surface;
        return nextUInt;
    }

    private static bool CaptureSetSurfaceColorspace(IntPtr surface, SDL3.SDL.Colorspace colorspace)
    {
        capturedSurface = surface;
        capturedColorspace = colorspace;
        return nextBool;
    }

    private static SDL3.SDL.Colorspace CaptureGetSurfaceColorspace(IntPtr surface)
    {
        capturedSurface = surface;
        return nextColorspace;
    }

    private static IntPtr CaptureCreateSurfacePalette(IntPtr surface)
    {
        capturedSurface = surface;
        return nextPointer;
    }

    private static bool CaptureSetSurfacePalette(IntPtr surface, IntPtr palette)
    {
        capturedSurface = surface;
        capturedPalette = palette;
        return nextBool;
    }

    private static IntPtr CaptureSurfacePointerReturn(IntPtr surface)
    {
        capturedSurface = surface;
        return nextPointer;
    }

    private static bool CaptureAddSurfaceAlternateImage(IntPtr surface, IntPtr image)
    {
        capturedSurface = surface;
        capturedImage = image;
        return nextBool;
    }

    private static bool CaptureSurfaceBoolReturn(IntPtr surface)
    {
        capturedSurface = surface;
        return nextBool;
    }

    private static IntPtr CaptureGetSurfaceImages(IntPtr surface, out int count)
    {
        capturedSurface = surface;
        count = nextCount;
        return nextPointer;
    }

    private static void CaptureSurfaceVoid(IntPtr surface)
    {
        capturedSurface = surface;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreePointer = mem;
        capturedFreeCallCount++;
    }

    private static IntPtr AllocatePointerArray(IntPtr[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(IntPtr.Size * values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteIntPtr(pointer, i * IntPtr.Size, values[i]);
        }

        return pointer;
    }

    private static IntPtr CaptureLoadSurfaceIO(IntPtr src, bool closeio)
    {
        capturedSrc = src;
        capturedCloseIO = closeio;
        return nextPointer;
    }

    private static IntPtr CaptureLoadSurface(string file)
    {
        capturedFile = file;
        return nextPointer;
    }

    private static IntPtr CaptureLoadBMPIO(IntPtr src, bool closeio)
    {
        capturedSrc = src;
        capturedCloseIO = closeio;
        return nextPointer;
    }

    private static IntPtr CaptureLoadBMP(string file)
    {
        capturedFile = file;
        return nextPointer;
    }

    private static bool CaptureSaveBMPIO(IntPtr surface, IntPtr dst, bool closeio)
    {
        capturedSurface = surface;
        capturedDst = dst;
        capturedCloseIO = closeio;
        return nextBool;
    }

    private static bool CaptureSaveBMP(IntPtr surface, string file)
    {
        capturedSurface = surface;
        capturedFile = file;
        return nextBool;
    }

    private static IntPtr CaptureLoadPNGIO(IntPtr src, bool closeio)
    {
        capturedSrc = src;
        capturedCloseIO = closeio;
        return nextPointer;
    }

    private static IntPtr CaptureLoadPNG(string file)
    {
        capturedFile = file;
        return nextPointer;
    }

    private static bool CaptureSavePNGIO(IntPtr surface, IntPtr dst, bool closeio)
    {
        capturedSurface = surface;
        capturedDst = dst;
        capturedCloseIO = closeio;
        return nextBool;
    }

    private static bool CaptureSavePNG(IntPtr surface, string file)
    {
        capturedSurface = surface;
        capturedFile = file;
        return nextBool;
    }

    private static IntPtr CaptureLoadJPGIO(IntPtr src, bool closeio)
    {
        capturedSrc = src;
        capturedCloseIO = closeio;
        return nextPointer;
    }

    private static IntPtr CaptureLoadJPG(string file)
    {
        capturedFile = file;
        return nextPointer;
    }

    private static bool CaptureSetSurfaceRLE(IntPtr surface, bool enabled)
    {
        capturedSurface = surface;
        capturedEnabled = enabled;
        return nextBool;
    }

    private static bool CaptureSetSurfaceColorKey(IntPtr surface, bool enabled, uint key)
    {
        capturedSurface = surface;
        capturedEnabled = enabled;
        capturedKey = key;
        return nextBool;
    }

    private static bool CaptureGetSurfaceColorKey(IntPtr surface, out uint key)
    {
        capturedSurface = surface;
        key = nextUInt;
        return nextBool;
    }

    private static bool CaptureSetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b)
    {
        capturedSurface = surface;
        capturedRed = r;
        capturedGreen = g;
        capturedBlue = b;
        return nextBool;
    }

    private static bool CaptureGetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b)
    {
        capturedSurface = surface;
        r = nextRed;
        g = nextGreen;
        b = nextBlue;
        return nextBool;
    }

    private static bool CaptureSetSurfaceAlphaMod(IntPtr surface, byte alpha)
    {
        capturedSurface = surface;
        capturedAlpha = alpha;
        return nextBool;
    }

    private static bool CaptureGetSurfaceAlphaMod(IntPtr surface, out byte alpha)
    {
        capturedSurface = surface;
        alpha = nextAlpha;
        return nextBool;
    }

    private static bool CaptureSetSurfaceBlendMode(IntPtr surface, SDL3.SDL.BlendMode blendMode)
    {
        capturedSurface = surface;
        capturedBlendMode = blendMode;
        return nextBool;
    }

    private static bool CaptureGetSurfaceBlendMode(IntPtr surface, out SDL3.SDL.BlendMode blendMode)
    {
        capturedSurface = surface;
        blendMode = nextBlendMode;
        return nextBool;
    }

    private static bool CaptureSetSurfaceClipRectPointer(IntPtr surface, IntPtr rect)
    {
        capturedSurface = surface;
        capturedRectPointer = rect;
        return nextBool;
    }

    private static bool CaptureSetSurfaceClipRectRect(IntPtr surface, in SDL3.SDL.Rect rect)
    {
        capturedSurface = surface;
        capturedRect = rect;
        return nextBool;
    }

    private static bool CaptureGetSurfaceClipRect(IntPtr surface, out SDL3.SDL.Rect rect)
    {
        capturedSurface = surface;
        rect = nextRect;
        return nextBool;
    }

    private static bool CaptureFlipSurface(IntPtr surface, SDL3.SDL.FlipMode flip)
    {
        capturedSurface = surface;
        capturedFlipMode = flip;
        return nextBool;
    }

    private static IntPtr CaptureRotateSurface(IntPtr surface, float angle)
    {
        capturedSurface = surface;
        capturedAngle = angle;
        return nextPointer;
    }

    private static IntPtr CaptureScaleSurface(IntPtr surface, int width, int height, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedSurface = surface;
        capturedWidth = width;
        capturedHeight = height;
        capturedScaleMode = scaleMode;
        return nextPointer;
    }

    private static IntPtr CaptureConvertSurface(IntPtr surface, SDL3.SDL.PixelFormat format)
    {
        capturedSurface = surface;
        capturedPixelFormat = format;
        return nextPointer;
    }

    private static IntPtr CaptureConvertSurfaceAndColorspace(IntPtr surface, SDL3.SDL.PixelFormat format, IntPtr palette, SDL3.SDL.Colorspace colorspace, uint props)
    {
        capturedSurface = surface;
        capturedPixelFormat = format;
        capturedPalette = palette;
        capturedColorspace = colorspace;
        capturedProps = props;
        return nextPointer;
    }

    private static bool CaptureConvertPixelsPointerToPointer(int width, int height, SDL3.SDL.PixelFormat srcFormat, IntPtr src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, out IntPtr dst, int dstPitch)
    {
        CaptureConvertPixelsCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch);
        capturedSrc = src;
        dst = nextPointer;
        return nextBool;
    }

    private static bool CaptureConvertPixelsArrayToPointer(int width, int height, SDL3.SDL.PixelFormat srcFormat, byte[] src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, out IntPtr dst, int dstPitch)
    {
        CaptureConvertPixelsCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch);
        capturedBytes = src;
        dst = nextPointer;
        return nextBool;
    }

    private static bool CaptureConvertPixelsPointerToArray(int width, int height, SDL3.SDL.PixelFormat srcFormat, IntPtr src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, out byte[] dst, int dstPitch)
    {
        CaptureConvertPixelsCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch);
        capturedSrc = src;
        dst = nextBytes ?? [];
        return nextBool;
    }

    private static bool CaptureConvertPixelsArrayToArray(int width, int height, SDL3.SDL.PixelFormat srcFormat, byte[] src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, out byte[] dst, int dstPitch)
    {
        CaptureConvertPixelsCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch);
        capturedBytes = src;
        dst = nextBytes ?? [];
        return nextBool;
    }

    private static bool CaptureConvertPixelsAndColorspacePointerToPointer(
        int width,
        int height,
        SDL3.SDL.PixelFormat srcFormat,
        SDL3.SDL.Colorspace srcColorspace,
        uint srcProperties,
        IntPtr src,
        int srcPitch,
        SDL3.SDL.PixelFormat dstFormat,
        SDL3.SDL.Colorspace dstColorspace,
        uint dstProperties,
        out IntPtr dst,
        int dstPitch)
    {
        CaptureConvertPixelsAndColorspaceCore(width, height, srcFormat, srcColorspace, srcProperties, srcPitch, dstFormat, dstColorspace, dstProperties, dstPitch);
        capturedSrc = src;
        dst = nextPointer;
        return nextBool;
    }

    private static bool CaptureConvertPixelsAndColorspaceArrayToPointer(
        int width,
        int height,
        SDL3.SDL.PixelFormat srcFormat,
        SDL3.SDL.Colorspace srcColorspace,
        uint srcProperties,
        byte[] src,
        int srcPitch,
        SDL3.SDL.PixelFormat dstFormat,
        SDL3.SDL.Colorspace dstColorspace,
        uint dstProperties,
        out IntPtr dst,
        int dstPitch)
    {
        CaptureConvertPixelsAndColorspaceCore(width, height, srcFormat, srcColorspace, srcProperties, srcPitch, dstFormat, dstColorspace, dstProperties, dstPitch);
        capturedBytes = src;
        dst = nextPointer;
        return nextBool;
    }

    private static bool CaptureConvertPixelsAndColorspacePointerToArray(
        int width,
        int height,
        SDL3.SDL.PixelFormat srcFormat,
        SDL3.SDL.Colorspace srcColorspace,
        uint srcProperties,
        IntPtr src,
        int srcPitch,
        SDL3.SDL.PixelFormat dstFormat,
        SDL3.SDL.Colorspace dstColorspace,
        uint dstProperties,
        out byte[] dst,
        int dstPitch)
    {
        CaptureConvertPixelsAndColorspaceCore(width, height, srcFormat, srcColorspace, srcProperties, srcPitch, dstFormat, dstColorspace, dstProperties, dstPitch);
        capturedSrc = src;
        dst = nextBytes ?? [];
        return nextBool;
    }

    private static bool CaptureConvertPixelsAndColorspaceArrayToArray(
        int width,
        int height,
        SDL3.SDL.PixelFormat srcFormat,
        SDL3.SDL.Colorspace srcColorspace,
        uint srcProperties,
        byte[] src,
        int srcPitch,
        SDL3.SDL.PixelFormat dstFormat,
        SDL3.SDL.Colorspace dstColorspace,
        uint dstProperties,
        out byte[] dst,
        int dstPitch)
    {
        CaptureConvertPixelsAndColorspaceCore(width, height, srcFormat, srcColorspace, srcProperties, srcPitch, dstFormat, dstColorspace, dstProperties, dstPitch);
        capturedBytes = src;
        dst = nextBytes ?? [];
        return nextBool;
    }

    private static void CaptureConvertPixelsCore(int width, int height, SDL3.SDL.PixelFormat srcFormat, int srcPitch, SDL3.SDL.PixelFormat dstFormat, int dstPitch)
    {
        capturedWidth = width;
        capturedHeight = height;
        capturedSrcPixelFormat = srcFormat;
        capturedSrcPitch = srcPitch;
        capturedDstPixelFormat = dstFormat;
        capturedDstPitch = dstPitch;
    }

    private static void CaptureConvertPixelsAndColorspaceCore(
        int width,
        int height,
        SDL3.SDL.PixelFormat srcFormat,
        SDL3.SDL.Colorspace srcColorspace,
        uint srcProperties,
        int srcPitch,
        SDL3.SDL.PixelFormat dstFormat,
        SDL3.SDL.Colorspace dstColorspace,
        uint dstProperties,
        int dstPitch)
    {
        CaptureConvertPixelsCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch);
        capturedSrcColorspace = srcColorspace;
        capturedSrcProperties = srcProperties;
        capturedDstColorspace = dstColorspace;
        capturedDstProperties = dstProperties;
    }

    private static bool CapturePremultiplyAlphaPointerToPointer(int width, int height, SDL3.SDL.PixelFormat srcFormat, IntPtr src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, IntPtr dst, int dstPitch, bool linear)
    {
        CapturePremultiplyAlphaCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch, linear);
        capturedSrc = src;
        capturedDst = dst;
        return nextBool;
    }

    private static bool CapturePremultiplyAlphaArrayToPointer(int width, int height, SDL3.SDL.PixelFormat srcFormat, byte[] src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, IntPtr dst, int dstPitch, bool linear)
    {
        CapturePremultiplyAlphaCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch, linear);
        capturedBytes = src;
        capturedDst = dst;
        return nextBool;
    }

    private static bool CapturePremultiplyAlphaPointerToArray(int width, int height, SDL3.SDL.PixelFormat srcFormat, IntPtr src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, out byte[] dst, int dstPitch, bool linear)
    {
        CapturePremultiplyAlphaCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch, linear);
        capturedSrc = src;
        dst = nextBytes ?? [];
        return nextBool;
    }

    private static bool CapturePremultiplyAlphaArrayToArray(int width, int height, SDL3.SDL.PixelFormat srcFormat, byte[] src, int srcPitch, SDL3.SDL.PixelFormat dstFormat, out byte[] dst, int dstPitch, bool linear)
    {
        CapturePremultiplyAlphaCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch, linear);
        capturedBytes = src;
        dst = nextBytes ?? [];
        return nextBool;
    }

    private static bool CapturePremultiplySurfaceAlpha(IntPtr surface, bool linear)
    {
        capturedSurface = surface;
        capturedLinear = linear;
        return nextBool;
    }

    private static void CapturePremultiplyAlphaCore(int width, int height, SDL3.SDL.PixelFormat srcFormat, int srcPitch, SDL3.SDL.PixelFormat dstFormat, int dstPitch, bool linear)
    {
        CaptureConvertPixelsCore(width, height, srcFormat, srcPitch, dstFormat, dstPitch);
        capturedLinear = linear;
    }

    private static bool CaptureClearSurface(IntPtr surface, float r, float g, float b, float a)
    {
        capturedSurface = surface;
        capturedFloatRed = r;
        capturedFloatGreen = g;
        capturedFloatBlue = b;
        capturedFloatAlpha = a;
        return nextBool;
    }

    private static bool CaptureFillSurfaceRectPointer(IntPtr dst, IntPtr rect, uint color)
    {
        capturedDst = dst;
        capturedRectPointer = rect;
        capturedColor = color;
        return nextBool;
    }

    private static bool CaptureFillSurfaceRectRect(IntPtr dst, in SDL3.SDL.Rect rect, uint color)
    {
        capturedDst = dst;
        capturedRect = rect;
        capturedColor = color;
        return nextBool;
    }

    private static bool CaptureFillSurfaceRects(IntPtr dst, SDL3.SDL.Rect[] rects, int count, uint color)
    {
        capturedDst = dst;
        capturedRects = rects;
        nextCount = count;
        capturedColor = color;
        return nextBool;
    }

    private static bool CaptureBlitSurfacePointerPointer(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfacePointerRect(IntPtr src, IntPtr srcrect, IntPtr dst, in SDL3.SDL.Rect dstrect)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        capturedDst = dst;
        capturedDstRect = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceRectPointer(IntPtr src, in SDL3.SDL.Rect srcrect, IntPtr dst, IntPtr dstrect)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceRectRect(IntPtr src, in SDL3.SDL.Rect srcrect, IntPtr dst, in SDL3.SDL.Rect dstrect)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        capturedDst = dst;
        capturedDstRect = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceScaledPointerPointer(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        capturedScaleMode = scaleMode;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceScaledPointerRect(IntPtr src, IntPtr srcrect, IntPtr dst, in SDL3.SDL.Rect dstrect, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        capturedDst = dst;
        capturedDstRect = dstrect;
        capturedScaleMode = scaleMode;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceScaledRectPointer(IntPtr src, in SDL3.SDL.Rect srcrect, IntPtr dst, IntPtr dstrect, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        capturedScaleMode = scaleMode;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceScaledRectRect(IntPtr src, in SDL3.SDL.Rect srcrect, IntPtr dst, in SDL3.SDL.Rect dstrect, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        capturedDst = dst;
        capturedDstRect = dstrect;
        capturedScaleMode = scaleMode;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceTiledWithScalePointerPointer(IntPtr src, IntPtr srcrect, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, IntPtr dstrect)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        capturedScale = scale;
        capturedScaleMode = scaleMode;
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceTiledWithScalePointerRect(IntPtr src, IntPtr srcrect, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, in SDL3.SDL.Rect dstrect)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        capturedScale = scale;
        capturedScaleMode = scaleMode;
        capturedDst = dst;
        capturedDstRect = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceTiledWithScaleRectPointer(IntPtr src, in SDL3.SDL.Rect srcrect, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, IntPtr dstrect)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        capturedScale = scale;
        capturedScaleMode = scaleMode;
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurfaceTiledWithScaleRectRect(IntPtr src, in SDL3.SDL.Rect srcrect, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, in SDL3.SDL.Rect dstrect)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        capturedScale = scale;
        capturedScaleMode = scaleMode;
        capturedDst = dst;
        capturedDstRect = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurface9GridPointerPointer(IntPtr src, IntPtr srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, IntPtr dstrect)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        Capture9GridScalars(leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode);
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurface9GridPointerRect(IntPtr src, IntPtr srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, in SDL3.SDL.Rect dstrect)
    {
        capturedSrc = src;
        capturedSrcRectPointer = srcrect;
        Capture9GridScalars(leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode);
        capturedDst = dst;
        capturedDstRect = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurface9GridRectPointer(IntPtr src, in SDL3.SDL.Rect srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, IntPtr dstrect)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        Capture9GridScalars(leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode);
        capturedDst = dst;
        capturedDstRectPointer = dstrect;
        return nextBool;
    }

    private static bool CaptureBlitSurface9GridRectRect(IntPtr src, in SDL3.SDL.Rect srcrect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, SDL3.SDL.ScaleMode scaleMode, IntPtr dst, in SDL3.SDL.Rect dstrect)
    {
        capturedSrc = src;
        capturedSrcRect = srcrect;
        Capture9GridScalars(leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode);
        capturedDst = dst;
        capturedDstRect = dstrect;
        return nextBool;
    }

    private static void Capture9GridScalars(int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedLeftWidth = leftWidth;
        capturedRightWidth = rightWidth;
        capturedTopHeight = topHeight;
        capturedBottomHeight = bottomHeight;
        capturedScale = scale;
        capturedScaleMode = scaleMode;
    }

    private static uint CaptureMapSurfaceRGB(IntPtr surface, byte r, byte g, byte b)
    {
        capturedSurface = surface;
        CaptureByteChannels(r, g, b, 0);
        return nextUInt;
    }

    private static uint CaptureMapSurfaceRGBA(IntPtr surface, byte r, byte g, byte b, byte a)
    {
        capturedSurface = surface;
        CaptureByteChannels(r, g, b, a);
        return nextUInt;
    }

    private static bool CaptureReadSurfacePixel(IntPtr surface, int x, int y, out byte r, out byte g, out byte b, out byte a)
    {
        CaptureSurfaceAndCoordinates(surface, x, y);
        r = nextRed;
        g = nextGreen;
        b = nextBlue;
        a = nextAlpha;
        return nextBool;
    }

    private static bool CaptureReadSurfacePixelFloat(IntPtr surface, int x, int y, out float r, out float g, out float b, out float a)
    {
        CaptureSurfaceAndCoordinates(surface, x, y);
        r = nextFloatRed;
        g = nextFloatGreen;
        b = nextFloatBlue;
        a = nextFloatAlpha;
        return nextBool;
    }

    private static bool CaptureWriteSurfacePixel(IntPtr surface, int x, int y, byte r, byte g, byte b, byte a)
    {
        CaptureSurfaceAndCoordinates(surface, x, y);
        CaptureByteChannels(r, g, b, a);
        return nextBool;
    }

    private static bool CaptureWriteSurfacePixelFloat(IntPtr surface, int x, int y, float r, float g, float b, float a)
    {
        CaptureSurfaceAndCoordinates(surface, x, y);
        capturedFloatRed = r;
        capturedFloatGreen = g;
        capturedFloatBlue = b;
        capturedFloatAlpha = a;
        return nextBool;
    }

    private static void CaptureSurfaceAndCoordinates(IntPtr surface, int x, int y)
    {
        capturedSurface = surface;
        capturedX = x;
        capturedY = y;
    }

    private static void CaptureByteChannels(byte r, byte g, byte b, byte a)
    {
        capturedRed = r;
        capturedGreen = g;
        capturedBlue = b;
        capturedAlpha = a;
    }

    private static SDL3.SDL.Rect CreateRect(int x, int y, int w, int h)
    {
        return new SDL3.SDL.Rect
        {
            X = x,
            Y = y,
            W = w,
            H = h
        };
    }

    private static void AssertRect(SDL3.SDL.Rect expected, SDL3.SDL.Rect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
        TestAssert.Equal(expected.W, actual.W, $"{message} W.");
        TestAssert.Equal(expected.H, actual.H, $"{message} H.");
    }

    private static void AssertConvertPixelsCommon(int width, int height, SDL3.SDL.PixelFormat srcFormat, SDL3.SDL.PixelFormat dstFormat, int srcPitch, int dstPitch)
    {
        TestAssert.Equal(width, capturedWidth, "pixel conversion must forward width.");
        TestAssert.Equal(height, capturedHeight, "pixel conversion must forward height.");
        TestAssert.Equal(srcFormat, capturedSrcPixelFormat, "pixel conversion must forward source format.");
        TestAssert.Equal(dstFormat, capturedDstPixelFormat, "pixel conversion must forward destination format.");
        TestAssert.Equal(srcPitch, capturedSrcPitch, "pixel conversion must forward source pitch.");
        TestAssert.Equal(dstPitch, capturedDstPitch, "pixel conversion must forward destination pitch.");
    }

    private static void AssertConvertPixelsColorspaceCommon(SDL3.SDL.Colorspace srcColorspace, uint srcProperties, SDL3.SDL.Colorspace dstColorspace, uint dstProperties)
    {
        TestAssert.Equal(srcColorspace, capturedSrcColorspace, "pixel colorspace conversion must forward source colorspace.");
        TestAssert.Equal(srcProperties, capturedSrcProperties, "pixel colorspace conversion must forward source properties.");
        TestAssert.Equal(dstColorspace, capturedDstColorspace, "pixel colorspace conversion must forward destination colorspace.");
        TestAssert.Equal(dstProperties, capturedDstProperties, "pixel colorspace conversion must forward destination properties.");
    }

    private static void AssertBytes(byte[] expected, byte[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} length.");
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], actual[i], $"{message} byte {i}.");
        }
    }

    private static void AssertRectArray(SDL3.SDL.Rect[] expected, SDL3.SDL.Rect[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} length.");
        for (int i = 0; i < expected.Length; i++)
        {
            AssertRect(expected[i], actual[i], $"{message} item {i}.");
        }
    }

    private static void AssertBlitSurfaces(IntPtr expectedSrc, IntPtr expectedDst)
    {
        TestAssert.Equal(expectedSrc, capturedSrc, "surface blit must forward source surface.");
        TestAssert.Equal(expectedDst, capturedDst, "surface blit must forward destination surface.");
    }

    private static void Assert9GridScalars(int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, SDL3.SDL.ScaleMode scaleMode, string message)
    {
        TestAssert.Equal(leftWidth, capturedLeftWidth, $"{message} must forward left width.");
        TestAssert.Equal(rightWidth, capturedRightWidth, $"{message} must forward right width.");
        TestAssert.Equal(topHeight, capturedTopHeight, $"{message} must forward top height.");
        TestAssert.Equal(bottomHeight, capturedBottomHeight, $"{message} must forward bottom height.");
        TestAssert.Equal(scale, capturedScale, $"{message} must forward scale.");
        TestAssert.Equal(scaleMode, capturedScaleMode, $"{message} must forward scale mode.");
    }

    private static void AssertSurfaceAndCoordinates(IntPtr surface, int x, int y, string message)
    {
        TestAssert.Equal(surface, capturedSurface, $"{message} must forward surface.");
        TestAssert.Equal(x, capturedX, $"{message} must forward x coordinate.");
        TestAssert.Equal(y, capturedY, $"{message} must forward y coordinate.");
    }

    private static void AssertCapturedByteChannels(byte r, byte g, byte b, byte a, string message)
    {
        TestAssert.Equal(r, capturedRed, $"{message} must forward red channel.");
        TestAssert.Equal(g, capturedGreen, $"{message} must forward green channel.");
        TestAssert.Equal(b, capturedBlue, $"{message} must forward blue channel.");
        TestAssert.Equal(a, capturedAlpha, $"{message} must forward alpha channel.");
    }

    private static void AssertCapturedFloatChannels(float r, float g, float b, float a, string message)
    {
        TestAssert.Equal(r, capturedFloatRed, $"{message} must forward red channel.");
        TestAssert.Equal(g, capturedFloatGreen, $"{message} must forward green channel.");
        TestAssert.Equal(b, capturedFloatBlue, $"{message} must forward blue channel.");
        TestAssert.Equal(a, capturedFloatAlpha, $"{message} must forward alpha channel.");
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedSurface = IntPtr.Zero;
        capturedPalette = IntPtr.Zero;
        capturedImage = IntPtr.Zero;
        capturedPixels = IntPtr.Zero;
        capturedSrc = IntPtr.Zero;
        capturedDst = IntPtr.Zero;
        capturedFreePointer = IntPtr.Zero;
        nextUInt = 0;
        capturedKey = 0;
        capturedColor = 0;
        capturedWidth = 0;
        capturedHeight = 0;
        capturedLeftWidth = 0;
        capturedRightWidth = 0;
        capturedTopHeight = 0;
        capturedBottomHeight = 0;
        capturedX = 0;
        capturedY = 0;
        capturedPitch = 0;
        capturedSrcPitch = 0;
        capturedDstPitch = 0;
        nextCount = 0;
        capturedFreeCallCount = 0;
        capturedProps = 0;
        capturedSrcProperties = 0;
        capturedDstProperties = 0;
        nextBool = false;
        capturedCloseIO = false;
        capturedEnabled = false;
        capturedLinear = false;
        nextRed = 0;
        nextGreen = 0;
        nextBlue = 0;
        nextAlpha = 0;
        capturedRed = 0;
        capturedGreen = 0;
        capturedBlue = 0;
        capturedAlpha = 0;
        nextFloatRed = 0;
        nextFloatGreen = 0;
        nextFloatBlue = 0;
        nextFloatAlpha = 0;
        capturedFile = null;
        nextBytes = null;
        capturedBytes = null;
        capturedRects = null;
        capturedPixelFormat = default;
        capturedSrcPixelFormat = default;
        capturedDstPixelFormat = default;
        nextColorspace = default;
        capturedColorspace = default;
        capturedSrcColorspace = default;
        capturedDstColorspace = default;
        nextBlendMode = default;
        capturedBlendMode = default;
        capturedFlipMode = default;
        capturedScaleMode = default;
        capturedAngle = 0;
        capturedScale = 0;
        capturedFloatRed = 0;
        capturedFloatGreen = 0;
        capturedFloatBlue = 0;
        capturedFloatAlpha = 0;
        capturedRectPointer = IntPtr.Zero;
        capturedSrcRectPointer = IntPtr.Zero;
        capturedDstRectPointer = IntPtr.Zero;
        nextRect = default;
        capturedRect = default;
        capturedSrcRect = default;
        capturedDstRect = default;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertNativeImport(MethodInfo method, string entryPoint)
    {
        AssertSdlLibraryImport(method, entryPoint);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertNativeBoolImport(MethodInfo method, string entryPoint)
    {
        AssertSdlLibraryImport(method, entryPoint);
        AssertBoolReturnMarshal(method);
        AssertExcludedFromCoverage(method);
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

    private static void AssertBoolParameterMarshal(MethodInfo method, int index)
    {
        MarshalAsAttribute? marshalAs = method.GetParameters()[index].GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} bool parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} bool parameter must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, int index)
    {
        MarshalAsAttribute? marshalAs = method.GetParameters()[index].GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} string parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} string parameter must use UTF-8 marshalling.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, int index, int sizeParamIndex)
    {
        MarshalAsAttribute? marshalAs = method.GetParameters()[index].GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} array parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} array parameter must use LPArray marshalling.");
        TestAssert.Equal(sizeParamIndex, marshalAs.SizeParamIndex, $"SDL.{method.Name} array parameter must keep SizeParamIndex.");
    }

    private static void AssertExcludedFromCoverage(MethodInfo method)
    {
        ExcludeFromCodeCoverageAttribute? attribute = method.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
        TestAssert.NotNull(attribute, $"SDL.{method.Name} native stub must be excluded from code coverage.");
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
