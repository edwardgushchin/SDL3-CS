using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.TTF;

internal static class PInvokeTests
{
    private static int nextInt;
    private static uint nextUInt;
    private static bool nextBool;
    private static float nextFloat;
    private static IntPtr nextPointer;
    private static int nextMajor;
    private static int nextMinor;
    private static int nextPatch;
    private static int nextHdpi;
    private static int nextVdpi;
    private static SDL3.TTF.FontStyleFlags nextFontStyle;
    private static SDL3.TTF.HintingFlags nextHinting;
    private static SDL3.TTF.HorizontalAlignment nextHorizontalAlignment;
    private static SDL3.TTF.Direction nextDirection;
    private static SDL3.TTF.ImageType nextImageType;
    private static SDL3.TTF.GPUTextEngineWinding nextWinding;
    private static int nextMinX;
    private static int nextMaxX;
    private static int nextMinY;
    private static int nextMaxY;
    private static int nextAdvance;
    private static int nextKerning;
    private static int nextWidth;
    private static int nextHeight;
    private static int nextMeasuredWidth;
    private static ulong nextMeasuredLength;
    private static byte nextByteR;
    private static byte nextByteG;
    private static byte nextByteB;
    private static byte nextByteA;
    private static float nextFloatR;
    private static float nextFloatG;
    private static float nextFloatB;
    private static float nextFloatA;
    private static string nextString = "";
    private static int nextCount;
    private static SDL3.TTF.SubString nextSubString;
    private static IntPtr capturedFont;
    private static IntPtr capturedFallback;
    private static IntPtr capturedSrc;
    private static IntPtr capturedImageTypePointer;
    private static IntPtr capturedTextPointer;
    private static IntPtr capturedEngine;
    private static IntPtr capturedTextHandle;
    private static IntPtr capturedRenderer;
    private static IntPtr capturedDevice;
    private static IntPtr capturedSurface;
    private static bool capturedCloseIO;
    private static bool capturedEnabled;
    private static float capturedPointSize;
    private static uint capturedProps;
    private static uint capturedUIntValue;
    private static uint capturedPreviousUIntValue;
    private static ulong capturedULongValue;
    private static UIntPtr capturedSize;
    private static UIntPtr capturedLength;
    private static int capturedIntValue;
    private static int capturedMaxWidth;
    private static int capturedWrapWidth;
    private static int capturedX;
    private static int capturedY;
    private static int capturedHdpi;
    private static int capturedVdpi;
    private static float capturedXFloat;
    private static float capturedYFloat;
    private static byte capturedByteR;
    private static byte capturedByteG;
    private static byte capturedByteB;
    private static byte capturedByteA;
    private static float capturedFloatR;
    private static float capturedFloatG;
    private static float capturedFloatB;
    private static float capturedFloatA;
    private static SDL3.TTF.FontStyleFlags capturedFontStyle;
    private static SDL3.TTF.HintingFlags capturedHinting;
    private static SDL3.TTF.HorizontalAlignment capturedHorizontalAlignment;
    private static SDL3.TTF.Direction capturedDirection;
    private static SDL3.TTF.GPUTextEngineWinding capturedWinding;
    private static SDL3.SDL.Color capturedForeground;
    private static SDL3.SDL.Color capturedBackground;
    private static int capturedCallCount;
    private static string? capturedFile;
    private static string? capturedString;
    private static SDL3.TTF.SubString capturedSubString;
    private static int capturedOffset;
    private static int capturedTextLength;
    private static int capturedLine;
    private static IntPtr capturedSdlFreeMemory;

    public static void RunAll()
    {
        GPUAtlasDrawSequenceTests.RunAll();
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        PropertyConstants_MatchSdlTtf322Header();
        VersionAndInitFunctions_ForwardOutputsAndReturnNativeValues();
        OpenFontFunctions_ForwardInputsAndReturnNativePointers();
        BasicFontStateFunctions_ForwardInputsOutputsAndReturnNativeValues();
        StyleOutlineHintingFunctions_ForwardInputsAndReturnNativeValues();
        FontMetricFunctions_ForwardInputsAndReturnNativeValues();
        KerningAndNameFunctions_ForwardInputsAndReturnNativeValues();
        DirectionScriptAndGlyphFunctions_ForwardInputsOutputsAndReturnNativeValues();
        GlyphImageMetricsAndStringSizingFunctions_ForwardInputsOutputsAndReturnNativeValues();
        RenderSurfaceFunctions_ForwardInputsAndReturnNativeSurfaces();
        TextEngineFunctions_ForwardInputsAndReturnNativeValues();
        TextObjectStateFunctions_ForwardInputsOutputsAndReturnNativeValues();
        TextContentAndSubstringFunctions_ForwardInputsOutputsAndReturnNativeValues();
        ShutdownFunctions_ForwardInputsAndReturnNativeValues();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        MethodInfo version = GetNativeMethod("TTF_Version");
        AssertNativeImport(version, "TTF_Version");
        AssertParameterTypes(version);

        MethodInfo freeType = GetNativeMethod("TTF_GetFreeTypeVersion");
        AssertNativeImport(freeType, "TTF_GetFreeTypeVersion");
        AssertParameterTypes(freeType, typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(freeType, 0);
        AssertOutParameter(freeType, 1);
        AssertOutParameter(freeType, 2);

        MethodInfo harfBuzz = GetNativeMethod("TTF_GetHarfBuzzVersion");
        AssertNativeImport(harfBuzz, "TTF_GetHarfBuzzVersion");
        AssertParameterTypes(harfBuzz, typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(harfBuzz, 0);
        AssertOutParameter(harfBuzz, 1);
        AssertOutParameter(harfBuzz, 2);

        AssertNativeBoolImport(GetNativeMethod("TTF_Init"), "TTF_Init");

        MethodInfo openFont = GetNativeMethod("TTF_OpenFont");
        AssertNativeImport(openFont, "TTF_OpenFont");
        AssertParameterTypes(openFont, typeof(string), typeof(float));
        AssertParameterMarshal(openFont, 0, UnmanagedType.LPUTF8Str);

        MethodInfo openFontIO = GetNativeMethod("TTF_OpenFontIO");
        AssertNativeImport(openFontIO, "TTF_OpenFontIO");
        AssertParameterTypes(openFontIO, typeof(IntPtr), typeof(bool), typeof(float));
        AssertParameterMarshal(openFontIO, 1, UnmanagedType.I1);

        MethodInfo openFontWithProperties = GetNativeMethod("TTF_OpenFontWithProperties");
        AssertNativeImport(openFontWithProperties, "TTF_OpenFontWithProperties");
        AssertParameterTypes(openFontWithProperties, typeof(uint));

        MethodInfo copyFont = GetNativeMethod("TTF_CopyFont");
        AssertNativeImport(copyFont, "TTF_CopyFont");
        AssertParameterTypes(copyFont, typeof(IntPtr));

        MethodInfo getFontProperties = GetNativeMethod("TTF_GetFontProperties");
        AssertNativeImport(getFontProperties, "TTF_GetFontProperties");
        AssertParameterTypes(getFontProperties, typeof(IntPtr));

        MethodInfo getFontGeneration = GetNativeMethod("TTF_GetFontGeneration");
        AssertNativeImport(getFontGeneration, "TTF_GetFontGeneration");
        AssertParameterTypes(getFontGeneration, typeof(IntPtr));

        MethodInfo addFallback = GetNativeMethod("TTF_AddFallbackFont");
        AssertNativeBoolImport(addFallback, "TTF_AddFallbackFont");
        AssertParameterTypes(addFallback, typeof(IntPtr), typeof(IntPtr));

        MethodInfo removeFallback = GetNativeMethod("TTF_RemoveFallbackFont");
        AssertNativeImport(removeFallback, "TTF_RemoveFallbackFont");
        AssertParameterTypes(removeFallback, typeof(IntPtr), typeof(IntPtr));

        MethodInfo clearFallback = GetNativeMethod("TTF_ClearFallbackFonts");
        AssertNativeImport(clearFallback, "TTF_ClearFallbackFonts");
        AssertParameterTypes(clearFallback, typeof(IntPtr));

        MethodInfo setFontSize = GetNativeMethod("TTF_SetFontSize");
        AssertNativeBoolImport(setFontSize, "TTF_SetFontSize");
        AssertParameterTypes(setFontSize, typeof(IntPtr), typeof(float));

        MethodInfo setFontSizeDpi = GetNativeMethod("TTF_SetFontSizeDPI");
        AssertNativeBoolImport(setFontSizeDpi, "TTF_SetFontSizeDPI");
        AssertParameterTypes(setFontSizeDpi, typeof(IntPtr), typeof(float), typeof(int), typeof(int));

        MethodInfo getFontSize = GetNativeMethod("TTF_GetFontSize");
        AssertNativeImport(getFontSize, "TTF_GetFontSize");
        AssertParameterTypes(getFontSize, typeof(IntPtr));

        MethodInfo getFontDpi = GetNativeMethod("TTF_GetFontDPI");
        AssertNativeBoolImport(getFontDpi, "TTF_GetFontDPI");
        AssertParameterTypes(getFontDpi, typeof(IntPtr), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(getFontDpi, 1);
        AssertOutParameter(getFontDpi, 2);

        MethodInfo setFontStyle = GetNativeMethod("TTF_SetFontStyle");
        AssertNativeImport(setFontStyle, "TTF_SetFontStyle");
        AssertParameterTypes(setFontStyle, typeof(IntPtr), typeof(SDL3.TTF.FontStyleFlags));

        MethodInfo getFontStyle = GetNativeMethod("TTF_GetFontStyle");
        AssertNativeImport(getFontStyle, "TTF_GetFontStyle");
        AssertParameterTypes(getFontStyle, typeof(IntPtr));

        MethodInfo setFontOutline = GetNativeMethod("TTF_SetFontOutline");
        AssertNativeBoolImport(setFontOutline, "TTF_SetFontOutline");
        AssertParameterTypes(setFontOutline, typeof(IntPtr), typeof(int));

        MethodInfo getFontOutline = GetNativeMethod("TTF_GetFontOutline");
        AssertNativeImport(getFontOutline, "TTF_GetFontOutline");
        AssertParameterTypes(getFontOutline, typeof(IntPtr));

        MethodInfo setFontHinting = GetNativeMethod("TTF_SetFontHinting");
        AssertNativeImport(setFontHinting, "TTF_SetFontHinting");
        AssertParameterTypes(setFontHinting, typeof(IntPtr), typeof(SDL3.TTF.HintingFlags));

        MethodInfo getNumFontFaces = GetNativeMethod("TTF_GetNumFontFaces");
        AssertNativeImport(getNumFontFaces, "TTF_GetNumFontFaces");
        AssertParameterTypes(getNumFontFaces, typeof(IntPtr));

        MethodInfo getFontHinting = GetNativeMethod("TTF_GetFontHinting");
        AssertNativeImport(getFontHinting, "TTF_GetFontHinting");
        AssertParameterTypes(getFontHinting, typeof(IntPtr));

        MethodInfo setFontSdf = GetNativeMethod("TTF_SetFontSDF");
        AssertNativeBoolImport(setFontSdf, "TTF_SetFontSDF");
        AssertParameterTypes(setFontSdf, typeof(IntPtr), typeof(bool));
        AssertParameterMarshal(setFontSdf, 1, UnmanagedType.I1);

        MethodInfo getFontSdf = GetNativeMethod("TTF_GetFontSDF");
        AssertNativeBoolImport(getFontSdf, "TTF_GetFontSDF");
        AssertParameterTypes(getFontSdf, typeof(IntPtr));

        MethodInfo getFontWeight = GetNativeMethod("TTF_GetFontWeight");
        AssertNativeImport(getFontWeight, "TTF_GetFontWeight");
        AssertParameterTypes(getFontWeight, typeof(IntPtr).MakeByRefType());

        MethodInfo setFontWrapAlignment = GetNativeMethod("TTF_SetFontWrapAlignment");
        AssertNativeImport(setFontWrapAlignment, "TTF_SetFontWrapAlignment");
        AssertParameterTypes(setFontWrapAlignment, typeof(IntPtr), typeof(SDL3.TTF.HorizontalAlignment));

        MethodInfo getFontWrapAlignment = GetNativeMethod("TTF_GetFontWrapAlignment");
        AssertNativeImport(getFontWrapAlignment, "TTF_GetFontWrapAlignment");
        AssertParameterTypes(getFontWrapAlignment, typeof(IntPtr));

        AssertSingleFontIntReturnImport("TTF_GetFontHeight", "TTF_GetFontHeight");
        AssertSingleFontIntReturnImport("TTF_GetFontAscent", "TTF_GetFontAscent");
        AssertSingleFontIntReturnImport("TTF_GetFontDescent", "TTF_GetFontDescent");

        MethodInfo setFontLineSkip = GetNativeMethod("TTF_SetFontLineSkip");
        AssertNativeImport(setFontLineSkip, "TTF_SetFontLineSkip");
        AssertParameterTypes(setFontLineSkip, typeof(IntPtr), typeof(int));

        AssertSingleFontIntReturnImport("TTF_GetFontLineSkip", "TTF_GetFontLineSkip");

        MethodInfo setFontKerning = GetNativeMethod("TTF_SetFontKerning");
        AssertNativeImport(setFontKerning, "TTF_SetFontKerning");
        AssertParameterTypes(setFontKerning, typeof(IntPtr), typeof(bool));
        AssertParameterMarshal(setFontKerning, 1, UnmanagedType.I1);

        AssertSingleFontBoolReturnImport("TTF_GetFontKerning", "TTF_GetFontKerning");
        AssertSingleFontBoolReturnImport("TTF_FontIsFixedWidth", "TTF_FontIsFixedWidth");
        AssertSingleFontBoolReturnImport("TTF_FontIsScalable", "TTF_FontIsScalable");
        AssertSingleFontPointerReturnImport("TTF_GetFontFamilyName", "TTF_GetFontFamilyName");
        AssertSingleFontPointerReturnImport("TTF_GetFontStyleName", "TTF_GetFontStyleName");

        MethodInfo setFontDirection = GetNativeMethod("TTF_SetFontDirection");
        AssertNativeBoolImport(setFontDirection, "TTF_SetFontDirection");
        AssertParameterTypes(setFontDirection, typeof(IntPtr), typeof(SDL3.TTF.Direction));

        MethodInfo getFontDirection = GetNativeMethod("TTF_GetFontDirection");
        AssertNativeImport(getFontDirection, "TTF_GetFontDirection");
        AssertParameterTypes(getFontDirection, typeof(IntPtr));

        AssertNoNativeMethod("TTF_SetFontCharSpacing");
        AssertNoNativeMethod("TTF_GetFontCharSpacing");
        AssertNoPublicMethod("SetFontCharSpacing", typeof(IntPtr), typeof(int));
        AssertNoPublicMethod("GetFontCharSpacing", typeof(IntPtr));

        MethodInfo stringToTag = GetNativeMethod("TTF_StringToTag");
        AssertNativeImport(stringToTag, "TTF_StringToTag");
        AssertParameterTypes(stringToTag, typeof(string));
        AssertParameterMarshal(stringToTag, 0, UnmanagedType.LPUTF8Str);

        MethodInfo tagToString = GetNativeMethod("TTF_TagToString");
        AssertNativeImport(tagToString, "TTF_TagToString");
        AssertParameterTypes(tagToString, typeof(uint), typeof(string).MakeByRefType(), typeof(UIntPtr));
        AssertOutParameter(tagToString, 1);
        AssertParameterMarshal(tagToString, 1, UnmanagedType.LPUTF8Str);

        MethodInfo setFontScript = GetNativeMethod("TTF_SetFontScript");
        AssertNativeBoolImport(setFontScript, "TTF_SetFontScript");
        AssertParameterTypes(setFontScript, typeof(IntPtr), typeof(uint));

        MethodInfo getFontScript = GetNativeMethod("TTF_GetFontScript");
        AssertNativeImport(getFontScript, "TTF_GetFontScript");
        AssertParameterTypes(getFontScript, typeof(IntPtr));

        MethodInfo getGlyphScript = GetNativeMethod("TTF_GetGlyphScript");
        AssertNativeImport(getGlyphScript, "TTF_GetGlyphScript");
        AssertParameterTypes(getGlyphScript, typeof(uint));

        MethodInfo setFontLanguage = GetNativeMethod("TTF_SetFontLanguage");
        AssertNativeBoolImport(setFontLanguage, "TTF_SetFontLanguage");
        AssertParameterTypes(setFontLanguage, typeof(IntPtr), typeof(string));
        AssertParameterMarshal(setFontLanguage, 1, UnmanagedType.LPUTF8Str);

        MethodInfo fontHasGlyph = GetNativeMethod("TTF_FontHasGlyph");
        AssertNativeBoolImport(fontHasGlyph, "TTF_FontHasGlyph");
        AssertParameterTypes(fontHasGlyph, typeof(IntPtr), typeof(uint));

        MethodInfo getGlyphImage = GetNativeMethod("TTF_GetGlyphImage");
        AssertNativeImport(getGlyphImage, "TTF_GetGlyphImage");
        AssertParameterTypes(getGlyphImage, typeof(IntPtr), typeof(uint), typeof(IntPtr));

        MethodInfo getGlyphImageOut = GetNativeMethod("TTF_GetGlyphImageOut");
        AssertNativeImport(getGlyphImageOut, "TTF_GetGlyphImage");
        AssertParameterTypes(getGlyphImageOut, typeof(IntPtr), typeof(uint), typeof(SDL3.TTF.ImageType).MakeByRefType());
        AssertOutParameter(getGlyphImageOut, 2);

        MethodInfo getGlyphImageForIndex = GetNativeMethod("TTF_GetGlyphImageForIndex");
        AssertNativeImport(getGlyphImageForIndex, "TTF_GetGlyphImageForIndex");
        AssertParameterTypes(getGlyphImageForIndex, typeof(IntPtr), typeof(uint), typeof(IntPtr));

        MethodInfo getGlyphImageForIndexOut = GetNativeMethod("TTF_GetGlyphImageForIndexOut");
        AssertNativeImport(getGlyphImageForIndexOut, "TTF_GetGlyphImageForIndex");
        AssertParameterTypes(getGlyphImageForIndexOut, typeof(IntPtr), typeof(uint), typeof(SDL3.TTF.ImageType).MakeByRefType());
        AssertOutParameter(getGlyphImageForIndexOut, 2);

        MethodInfo getGlyphMetrics = GetNativeMethod("TTF_GetGlyphMetrics");
        AssertNativeBoolImport(getGlyphMetrics, "TTF_GetGlyphMetrics");
        AssertParameterTypes(
            getGlyphMetrics,
            typeof(IntPtr),
            typeof(uint),
            typeof(int).MakeByRefType(),
            typeof(int).MakeByRefType(),
            typeof(int).MakeByRefType(),
            typeof(int).MakeByRefType(),
            typeof(int).MakeByRefType());
        AssertOutParameter(getGlyphMetrics, 2);
        AssertOutParameter(getGlyphMetrics, 3);
        AssertOutParameter(getGlyphMetrics, 4);
        AssertOutParameter(getGlyphMetrics, 5);
        AssertOutParameter(getGlyphMetrics, 6);

        MethodInfo getGlyphKerning = GetNativeMethod("TTF_GetGlyphKerning");
        AssertNativeBoolImport(getGlyphKerning, "TTF_GetGlyphKerning");
        AssertParameterTypes(getGlyphKerning, typeof(IntPtr), typeof(uint), typeof(uint), typeof(int).MakeByRefType());
        AssertOutParameter(getGlyphKerning, 3);

        MethodInfo getStringSize = GetNativeMethod("TTF_GetStringSize");
        AssertNativeBoolImport(getStringSize, "TTF_GetStringSize");
        AssertParameterTypes(getStringSize, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertParameterMarshal(getStringSize, 1, UnmanagedType.LPUTF8Str);
        AssertOutParameter(getStringSize, 3);
        AssertOutParameter(getStringSize, 4);

        MethodInfo getStringSizeBytePointer = GetNativeMethod("TTF_GetStringSizeBytePointer");
        AssertNativeBoolImport(getStringSizeBytePointer, "TTF_GetStringSize");
        AssertParameterTypes(getStringSizeBytePointer, typeof(IntPtr), typeof(byte).MakePointerType(), typeof(UIntPtr), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(getStringSizeBytePointer, 3);
        AssertOutParameter(getStringSizeBytePointer, 4);

        MethodInfo getStringSizePointer = GetNativeMethod("TTF_GetStringSizePointer");
        AssertNativeBoolImport(getStringSizePointer, "TTF_GetStringSize");
        AssertParameterTypes(getStringSizePointer, typeof(IntPtr), typeof(IntPtr), typeof(UIntPtr), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(getStringSizePointer, 3);
        AssertOutParameter(getStringSizePointer, 4);

        MethodInfo getStringSizeWrapped = GetNativeMethod("TTF_GetStringSizeWrapped");
        AssertNativeBoolImport(getStringSizeWrapped, "TTF_GetStringSizeWrapped");
        AssertParameterTypes(getStringSizeWrapped, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(int), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertParameterMarshal(getStringSizeWrapped, 1, UnmanagedType.LPUTF8Str);
        AssertOutParameter(getStringSizeWrapped, 4);
        AssertOutParameter(getStringSizeWrapped, 5);

        MethodInfo measureString = GetNativeMethod("TTF_MeasureString");
        AssertNativeBoolImport(measureString, "TTF_MeasureString");
        AssertParameterTypes(measureString, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(int), typeof(int).MakeByRefType(), typeof(ulong).MakeByRefType());
        AssertParameterMarshal(measureString, 1, UnmanagedType.LPUTF8Str);
        AssertOutParameter(measureString, 4);
        AssertOutParameter(measureString, 5);

        AssertRenderTextStringImport("TTF_RenderTextSolidString", "TTF_RenderText_Solid");
        AssertRenderTextPointerImport("TTF_RenderTextSolidPointer", "TTF_RenderText_Solid");
        AssertRenderTextBytePointerImport("TTF_RenderTextSolidBytePointer", "TTF_RenderText_Solid");
        AssertRenderTextWrappedImport("TTF_RenderTextSolidWrapped", "TTF_RenderText_Solid_Wrapped");
        AssertRenderGlyphUIntImport("TTF_RenderGlyphSolid", "TTF_RenderGlyph_Solid");
        AssertRenderTextStringWithBackgroundImport("TTF_RenderTextShaded", "TTF_RenderText_Shaded");
        AssertRenderTextWrappedWithBackgroundImport("TTF_RenderTextShadedWrapped", "TTF_RenderText_Shaded_Wrapped");
        AssertRenderGlyphUIntWithBackgroundImport("TTF_RenderGlyphShaded", "TTF_RenderGlyph_Shaded");
        AssertRenderTextStringImport("TTF_RenderTextBlendedString", "TTF_RenderText_Blended");
        AssertRenderTextPointerImport("TTF_RenderTextBlendedPointer", "TTF_RenderText_Blended");
        AssertRenderTextBytePointerImport("TTF_RenderTextBlendedBytePointer", "TTF_RenderText_Blended");
        AssertRenderTextWrappedImport("TTF_RenderTextBlendedWrapped", "TTF_RenderText_Blended_Wrapped");
        AssertRenderGlyphULongImport("TTF_RenderGlyphBlended", "TTF_RenderGlyph_Blended");
        AssertRenderTextStringWithBackgroundImport("TTF_RenderTextLCD", "TTF_RenderText_LCD");
        AssertRenderTextWrappedWithBackgroundImport("TTF_RenderTextLCDWrapped", "TTF_RenderText_LCD_Wrapped");
        AssertRenderGlyphUIntWithBackgroundImport("TTF_RenderGlyphLCD", "TTF_RenderGlyph_LCD");

        MethodInfo createSurfaceTextEngine = GetNativeMethod("TTF_CreateSurfaceTextEngine");
        AssertNativeImport(createSurfaceTextEngine, "TTF_CreateSurfaceTextEngine");
        AssertParameterTypes(createSurfaceTextEngine);

        MethodInfo drawSurfaceText = GetNativeMethod("TTF_DrawSurfaceText");
        AssertNativeBoolImport(drawSurfaceText, "TTF_DrawSurfaceText");
        AssertParameterTypes(drawSurfaceText, typeof(IntPtr), typeof(int), typeof(int), typeof(IntPtr));

        MethodInfo destroySurfaceTextEngine = GetNativeMethod("TTF_DestroySurfaceTextEngine");
        AssertNativeImport(destroySurfaceTextEngine, "TTF_DestroySurfaceTextEngine");
        AssertParameterTypes(destroySurfaceTextEngine, typeof(IntPtr));

        MethodInfo createRendererTextEngine = GetNativeMethod("TTF_CreateRendererTextEngine");
        AssertNativeImport(createRendererTextEngine, "TTF_CreateRendererTextEngine");
        AssertParameterTypes(createRendererTextEngine, typeof(IntPtr));

        MethodInfo createRendererTextEngineWithProperties = GetNativeMethod("TTF_CreateRendererTextEngineWithProperties");
        AssertNativeImport(createRendererTextEngineWithProperties, "TTF_CreateRendererTextEngineWithProperties");
        AssertParameterTypes(createRendererTextEngineWithProperties, typeof(uint));

        MethodInfo drawRendererText = GetNativeMethod("TTF_DrawRendererText");
        AssertNativeBoolImport(drawRendererText, "TTF_DrawRendererText");
        AssertParameterTypes(drawRendererText, typeof(IntPtr), typeof(float), typeof(float));

        MethodInfo destroyRendererTextEngine = GetNativeMethod("TTF_DestroyRendererTextEngine");
        AssertNativeImport(destroyRendererTextEngine, "TTF_DestroyRendererTextEngine");
        AssertParameterTypes(destroyRendererTextEngine, typeof(IntPtr));

        MethodInfo createGpuTextEngine = GetNativeMethod("TTF_CreateGPUTextEngine");
        AssertNativeImport(createGpuTextEngine, "TTF_CreateGPUTextEngine");
        AssertParameterTypes(createGpuTextEngine, typeof(IntPtr));

        MethodInfo createGpuTextEngineWithProperties = GetNativeMethod("TTF_CreateGPUTextEngineWithProperties");
        AssertNativeImport(createGpuTextEngineWithProperties, "TTF_CreateGPUTextEngineWithProperties");
        AssertParameterTypes(createGpuTextEngineWithProperties, typeof(uint));

        MethodInfo getGpuTextDrawData = GetNativeMethod("TTF_GetGPUTextDrawData");
        AssertNativeImport(getGpuTextDrawData, "TTF_GetGPUTextDrawData");
        AssertParameterTypes(getGpuTextDrawData, typeof(IntPtr));

        MethodInfo destroyGpuTextEngine = GetNativeMethod("TTF_DestroyGPUTextEngine");
        AssertNativeImport(destroyGpuTextEngine, "TTF_DestroyGPUTextEngine");
        AssertParameterTypes(destroyGpuTextEngine, typeof(IntPtr));

        MethodInfo setGpuTextEngineWinding = GetNativeMethod("TTF_SetGPUTextEngineWinding");
        AssertNativeImport(setGpuTextEngineWinding, "TTF_SetGPUTextEngineWinding");
        AssertParameterTypes(setGpuTextEngineWinding, typeof(IntPtr), typeof(SDL3.TTF.GPUTextEngineWinding));

        MethodInfo getGpuTextEngineWinding = GetNativeMethod("TTF_GetGPUTextEngineWinding");
        AssertNativeImport(getGpuTextEngineWinding, "TTF_GetGPUTextEngineWinding");
        AssertParameterTypes(getGpuTextEngineWinding, typeof(IntPtr));

        MethodInfo createText = GetNativeMethod("TTF_CreateText");
        AssertNativeImport(createText, "TTF_CreateText");
        AssertParameterTypes(createText, typeof(IntPtr), typeof(IntPtr), typeof(string), typeof(UIntPtr));
        AssertParameterMarshal(createText, 2, UnmanagedType.LPUTF8Str);

        MethodInfo getTextProperties = GetNativeMethod("TTF_GetTextProperties");
        AssertNativeImport(getTextProperties, "TTF_GetTextProperties");
        AssertParameterTypes(getTextProperties, typeof(IntPtr));

        MethodInfo setTextEngine = GetNativeMethod("TTF_SetTextEngine");
        AssertNativeBoolImport(setTextEngine, "TTF_SetTextEngine");
        AssertParameterTypes(setTextEngine, typeof(IntPtr), typeof(IntPtr));

        MethodInfo getTextEngine = GetNativeMethod("TTF_GetTextEngine");
        AssertNativeImport(getTextEngine, "TTF_GetTextEngine");
        AssertParameterTypes(getTextEngine, typeof(IntPtr));

        MethodInfo setTextFont = GetNativeMethod("TTF_SetTextFont");
        AssertNativeBoolImport(setTextFont, "TTF_SetTextFont");
        AssertParameterTypes(setTextFont, typeof(IntPtr), typeof(IntPtr));

        MethodInfo getTextFont = GetNativeMethod("TTF_GetTextFont");
        AssertNativeImport(getTextFont, "TTF_GetTextFont");
        AssertParameterTypes(getTextFont, typeof(IntPtr));

        MethodInfo setTextDirection = GetNativeMethod("TTF_SetTextDirection");
        AssertNativeBoolImport(setTextDirection, "TTF_SetTextDirection");
        AssertParameterTypes(setTextDirection, typeof(IntPtr), typeof(SDL3.TTF.Direction));

        MethodInfo getTextDirection = GetNativeMethod("TTF_GetTextDirection");
        AssertNativeImport(getTextDirection, "TTF_GetTextDirection");
        AssertParameterTypes(getTextDirection, typeof(IntPtr));

        MethodInfo setTextScript = GetNativeMethod("TTF_SetTextScript");
        AssertNativeBoolImport(setTextScript, "TTF_SetTextScript");
        AssertParameterTypes(setTextScript, typeof(IntPtr), typeof(uint));

        MethodInfo getTextScript = GetNativeMethod("TTF_GetTextScript");
        AssertNativeImport(getTextScript, "TTF_GetTextScript");
        AssertParameterTypes(getTextScript, typeof(IntPtr));

        MethodInfo setTextColor = GetNativeMethod("TTF_SetTextColor");
        AssertNativeBoolImport(setTextColor, "TTF_SetTextColor");
        AssertParameterTypes(setTextColor, typeof(IntPtr), typeof(byte), typeof(byte), typeof(byte), typeof(byte));

        MethodInfo setTextColorFloat = GetNativeMethod("TTF_SetTextColorFloat");
        AssertNativeBoolImport(setTextColorFloat, "TTF_SetTextColorFloat");
        AssertParameterTypes(setTextColorFloat, typeof(IntPtr), typeof(float), typeof(float), typeof(float), typeof(float));

        MethodInfo getTextColor = GetNativeMethod("TTF_GetTextColor");
        AssertNativeBoolImport(getTextColor, "TTF_GetTextColor");
        AssertParameterTypes(getTextColor, typeof(IntPtr), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType(), typeof(byte).MakeByRefType());
        AssertOutParameter(getTextColor, 1);
        AssertOutParameter(getTextColor, 2);
        AssertOutParameter(getTextColor, 3);
        AssertOutParameter(getTextColor, 4);

        MethodInfo getTextColorFloat = GetNativeMethod("TTF_GetTextColorFloat");
        AssertNativeBoolImport(getTextColorFloat, "TTF_GetTextColorFloat");
        AssertParameterTypes(getTextColorFloat, typeof(IntPtr), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(float).MakeByRefType());
        AssertOutParameter(getTextColorFloat, 1);
        AssertOutParameter(getTextColorFloat, 2);
        AssertOutParameter(getTextColorFloat, 3);
        AssertOutParameter(getTextColorFloat, 4);

        MethodInfo setTextPosition = GetNativeMethod("TTF_SetTextPosition");
        AssertNativeBoolImport(setTextPosition, "TTF_SetTextPosition");
        AssertParameterTypes(setTextPosition, typeof(IntPtr), typeof(int), typeof(int));

        MethodInfo getTextPosition = GetNativeMethod("TTF_GetTextPosition");
        AssertNativeBoolImport(getTextPosition, "TTF_GetTextPosition");
        AssertParameterTypes(getTextPosition, typeof(IntPtr), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(getTextPosition, 1);
        AssertOutParameter(getTextPosition, 2);

        MethodInfo setTextWrapWidth = GetNativeMethod("TTF_SetTextWrapWidth");
        AssertNativeBoolImport(setTextWrapWidth, "TTF_SetTextWrapWidth");
        AssertParameterTypes(setTextWrapWidth, typeof(IntPtr), typeof(int));

        MethodInfo getTextWrapWidth = GetNativeMethod("TTF_GetTextWrapWidth");
        AssertNativeBoolImport(getTextWrapWidth, "TTF_GetTextWrapWidth");
        AssertParameterTypes(getTextWrapWidth, typeof(IntPtr), typeof(int).MakeByRefType());
        AssertOutParameter(getTextWrapWidth, 1);

        MethodInfo setTextWrapWhitespaceVisible = GetNativeMethod("TTF_SetTextWrapWhitespaceVisible");
        AssertNativeBoolImport(setTextWrapWhitespaceVisible, "TTF_SetTextWrapWhitespaceVisible");
        AssertParameterTypes(setTextWrapWhitespaceVisible, typeof(IntPtr), typeof(bool));
        AssertParameterMarshal(setTextWrapWhitespaceVisible, 1, UnmanagedType.I1);

        MethodInfo textWrapWhitespaceVisible = GetNativeMethod("TTF_TextWrapWhitespaceVisible");
        AssertNativeBoolImport(textWrapWhitespaceVisible, "TTF_TextWrapWhitespaceVisible");
        AssertParameterTypes(textWrapWhitespaceVisible, typeof(IntPtr));

        MethodInfo setTextString = GetNativeMethod("TTF_SetTextString");
        AssertNativeBoolImport(setTextString, "TTF_SetTextString");
        AssertParameterTypes(setTextString, typeof(IntPtr), typeof(string), typeof(UIntPtr));
        AssertParameterMarshal(setTextString, 1, UnmanagedType.LPUTF8Str);

        MethodInfo insertTextString = GetNativeMethod("TTF_InsertTextString");
        AssertNativeBoolImport(insertTextString, "TTF_InsertTextString");
        AssertParameterTypes(insertTextString, typeof(IntPtr), typeof(int), typeof(string), typeof(UIntPtr));
        AssertParameterMarshal(insertTextString, 2, UnmanagedType.LPUTF8Str);

        MethodInfo appendTextString = GetNativeMethod("TTF_AppendTextString");
        AssertNativeBoolImport(appendTextString, "TTF_AppendTextString");
        AssertParameterTypes(appendTextString, typeof(IntPtr), typeof(string), typeof(UIntPtr));
        AssertParameterMarshal(appendTextString, 1, UnmanagedType.LPUTF8Str);

        MethodInfo deleteTextString = GetNativeMethod("TTF_DeleteTextString");
        AssertNativeBoolImport(deleteTextString, "TTF_DeleteTextString");
        AssertParameterTypes(deleteTextString, typeof(IntPtr), typeof(int), typeof(int));

        MethodInfo getTextSize = GetNativeMethod("TTF_GetTextSize");
        AssertNativeBoolImport(getTextSize, "TTF_GetTextSize");
        AssertParameterTypes(getTextSize, typeof(IntPtr), typeof(int).MakeByRefType(), typeof(int).MakeByRefType());
        AssertOutParameter(getTextSize, 1);
        AssertOutParameter(getTextSize, 2);

        MethodInfo getTextSubString = GetNativeMethod("TTF_GetTextSubString");
        AssertNativeBoolImport(getTextSubString, "TTF_GetTextSubString");
        AssertParameterTypes(getTextSubString, typeof(IntPtr), typeof(int), typeof(SDL3.TTF.SubString).MakeByRefType());
        AssertOutParameter(getTextSubString, 2);

        MethodInfo getTextSubStringForLine = GetNativeMethod("TTF_GetTextSubStringForLine");
        AssertNativeBoolImport(getTextSubStringForLine, "TTF_GetTextSubStringForLine");
        AssertParameterTypes(getTextSubStringForLine, typeof(IntPtr), typeof(int), typeof(SDL3.TTF.SubString).MakeByRefType());
        AssertOutParameter(getTextSubStringForLine, 2);

        MethodInfo getTextSubStringsForRange = GetNativeMethod("TTF_GetTextSubStringsForRange");
        AssertNativeImport(getTextSubStringsForRange, "TTF_GetTextSubStringsForRange");
        AssertParameterTypes(getTextSubStringsForRange, typeof(IntPtr), typeof(int), typeof(int), typeof(int).MakeByRefType());
        AssertOutParameter(getTextSubStringsForRange, 3);

        MethodInfo getTextSubStringForPoint = GetNativeMethod("TTF_GetTextSubStringForPoint");
        AssertNativeBoolImport(getTextSubStringForPoint, "TTF_GetTextSubStringForPoint");
        AssertParameterTypes(getTextSubStringForPoint, typeof(IntPtr), typeof(int), typeof(int), typeof(SDL3.TTF.SubString).MakeByRefType());
        AssertOutParameter(getTextSubStringForPoint, 3);

        MethodInfo getPreviousTextSubString = GetNativeMethod("TTF_GetPreviousTextSubString");
        AssertNativeBoolImport(getPreviousTextSubString, "TTF_GetPreviousTextSubString");
        AssertParameterTypes(getPreviousTextSubString, typeof(IntPtr), typeof(SDL3.TTF.SubString).MakeByRefType(), typeof(SDL3.TTF.SubString).MakeByRefType());
        AssertOutParameter(getPreviousTextSubString, 2);

        MethodInfo getNextTextSubString = GetNativeMethod("TTF_GetNextTextSubString");
        AssertNativeBoolImport(getNextTextSubString, "TTF_GetNextTextSubString");
        AssertParameterTypes(getNextTextSubString, typeof(IntPtr), typeof(SDL3.TTF.SubString).MakeByRefType(), typeof(SDL3.TTF.SubString).MakeByRefType());
        AssertOutParameter(getNextTextSubString, 2);

        MethodInfo updateText = GetNativeMethod("TTF_UpdateText");
        AssertNativeBoolImport(updateText, "TTF_UpdateText");
        AssertParameterTypes(updateText, typeof(IntPtr));

        MethodInfo destroyText = GetNativeMethod("TTF_DestroyText");
        AssertNativeImport(destroyText, "TTF_DestroyText");
        AssertParameterTypes(destroyText, typeof(IntPtr));

        MethodInfo closeFont = GetNativeMethod("TTF_CloseFont");
        AssertNativeImport(closeFont, "TTF_CloseFont");
        AssertParameterTypes(closeFont, typeof(IntPtr));

        MethodInfo quit = GetNativeMethod("TTF_Quit");
        AssertNativeImport(quit, "TTF_Quit");
        AssertParameterTypes(quit);

        MethodInfo wasInit = GetNativeMethod("TTF_WasInit");
        AssertNativeImport(wasInit, "TTF_WasInit");
        AssertParameterTypes(wasInit);
    }

    public static void PropertyConstants_MatchSdlTtf322Header()
    {
        TestAssert.Equal("SDL_ttf.font.create.filename", SDL3.TTF.Props.FontCreateFilenameString, "TTF.Props.FontCreateFilenameString must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.iostream", SDL3.TTF.Props.FontCreateIOStreamPointer, "TTF.Props.FontCreateIOStreamPointer must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.iostream.offset", SDL3.TTF.Props.FontCreateIOStreamOffsetNumber, "TTF.Props.FontCreateIOStreamOffsetNumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.iostream.autoclose", SDL3.TTF.Props.FontCreateIOStreamAutoCloseBoolean, "TTF.Props.FontCreateIOStreamAutoCloseBoolean must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.size", SDL3.TTF.Props.FontCreateSizeFloat, "TTF.Props.FontCreateSizeFloat must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.face", SDL3.TTF.Props.FontCreateFaceNumber, "TTF.Props.FontCreateFaceNumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.hdpi", SDL3.TTF.Props.FontCreateHorizontalDPINumber, "TTF.Props.FontCreateHorizontalDPINumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.vdpi", SDL3.TTF.Props.FontCreateVerticalDPINumber, "TTF.Props.FontCreateVerticalDPINumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.create.existing_font", SDL3.TTF.Props.FontCreateExistingFont, "TTF.Props.FontCreateExistingFont must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.outline.line_cap", SDL3.TTF.Props.FontOutlineLineCapNumber, "TTF.Props.FontOutlineLineCapNumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.outline.line_join", SDL3.TTF.Props.FontOutlineLineJoinNumber, "TTF.Props.FontOutlineLineJoinNumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.font.outline.miter_limit", SDL3.TTF.Props.FontOutlineMiterLimitNumber, "TTF.Props.FontOutlineMiterLimitNumber must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.renderer_text_engine.create.renderer", SDL3.TTF.Props.RendererTextEngineRenderer, "TTF.Props.RendererTextEngineRenderer must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.renderer_text_engine.create.atlas_texture_size", SDL3.TTF.Props.RendererTextEngineAtlasTextureSize, "TTF.Props.RendererTextEngineAtlasTextureSize must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.gpu_text_engine.create.device", SDL3.TTF.Props.GPUTextEngineDevice, "TTF.Props.GPUTextEngineDevice must match SDL_ttf 3.2.2.");
        TestAssert.Equal("SDL_ttf.gpu_text_engine.create.atlas_texture_size", SDL3.TTF.Props.GPUTextEngineAtlasTextureSize, "TTF.Props.GPUTextEngineAtlasTextureSize must match SDL_ttf 3.2.2.");

        AssertNoPropertyConstant("FontCreateExistingFontPointer");
        AssertNoPropertyConstant("RendererTextEngineRendererPointer");
        AssertNoPropertyConstant("RendererTextEngineAtlasTextureSizeNumber");
        AssertNoPropertyConstant("GPUTextEngineDevicePointer");
        AssertNoPropertyConstant("GPUTextEngineAtlasTextureSizeNumber");
    }

    public static void VersionAndInitFunctions_ForwardOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextInt = 3004005;
        using (NativeHookScope _ = NativeHookScope.Install("VersionNativeFunction", nameof(ReturnNextInt)))
        {
            int actual = SDL3.TTF.Version();

            TestAssert.Equal(3004005, actual, "TTF.Version must return native version.");
            TestAssert.Equal(1, capturedCallCount, "TTF.Version must call native hook once.");
        }

        ResetCaptureState();
        nextMajor = 2;
        nextMinor = 13;
        nextPatch = 4;
        using (NativeHookScope _ = NativeHookScope.Install("GetFreeTypeVersionNativeFunction", nameof(CaptureVersionTriplet)))
        {
            SDL3.TTF.GetFreeTypeVersion(out int major, out int minor, out int patch);

            TestAssert.Equal(2, major, "TTF.GetFreeTypeVersion must return native major.");
            TestAssert.Equal(13, minor, "TTF.GetFreeTypeVersion must return native minor.");
            TestAssert.Equal(4, patch, "TTF.GetFreeTypeVersion must return native patch.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFreeTypeVersion must call native hook once.");
        }

        ResetCaptureState();
        nextMajor = 8;
        nextMinor = 2;
        nextPatch = 1;
        using (NativeHookScope _ = NativeHookScope.Install("GetHarfBuzzVersionNativeFunction", nameof(CaptureVersionTriplet)))
        {
            SDL3.TTF.GetHarfBuzzVersion(out int major, out int minor, out int patch);

            TestAssert.Equal(8, major, "TTF.GetHarfBuzzVersion must return native major.");
            TestAssert.Equal(2, minor, "TTF.GetHarfBuzzVersion must return native minor.");
            TestAssert.Equal(1, patch, "TTF.GetHarfBuzzVersion must return native patch.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetHarfBuzzVersion must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("InitNativeFunction", nameof(ReturnNextBool)))
        {
            bool actual = SDL3.TTF.Init();

            TestAssert.Equal(true, actual, "TTF.Init must return native success value.");
            TestAssert.Equal(1, capturedCallCount, "TTF.Init must call native hook once.");
        }
    }

    public static void OpenFontFunctions_ForwardInputsAndReturnNativePointers()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1001;
        using (NativeHookScope _ = NativeHookScope.Install("OpenFontNativeFunction", nameof(CaptureOpenFont)))
        {
            IntPtr actual = SDL3.TTF.OpenFont("font.ttf", 16.5f);

            TestAssert.Equal(nextPointer, actual, "TTF.OpenFont must return native font pointer.");
            TestAssert.Equal("font.ttf", capturedFile, "TTF.OpenFont must forward file.");
            TestAssert.Equal(16.5f, capturedPointSize, "TTF.OpenFont must forward point size.");
            TestAssert.Equal(1, capturedCallCount, "TTF.OpenFont must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1002;
        using (NativeHookScope _ = NativeHookScope.Install("OpenFontIONativeFunction", nameof(CaptureOpenFontIO)))
        {
            IntPtr actual = SDL3.TTF.OpenFontIO((IntPtr)0x2001, true, 18.25f);

            TestAssert.Equal(nextPointer, actual, "TTF.OpenFontIO must return native font pointer.");
            TestAssert.Equal((IntPtr)0x2001, capturedSrc, "TTF.OpenFontIO must forward source stream.");
            TestAssert.Equal(true, capturedCloseIO, "TTF.OpenFontIO must forward closeio.");
            TestAssert.Equal(18.25f, capturedPointSize, "TTF.OpenFontIO must forward point size.");
            TestAssert.Equal(1, capturedCallCount, "TTF.OpenFontIO must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1003;
        using (NativeHookScope _ = NativeHookScope.Install("OpenFontWithPropertiesNativeFunction", nameof(CaptureUIntReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.OpenFontWithProperties(42);

            TestAssert.Equal(nextPointer, actual, "TTF.OpenFontWithProperties must return native font pointer.");
            TestAssert.Equal(42u, capturedProps, "TTF.OpenFontWithProperties must forward properties.");
            TestAssert.Equal(1, capturedCallCount, "TTF.OpenFontWithProperties must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1004;
        using (NativeHookScope _ = NativeHookScope.Install("CopyFontNativeFunction", nameof(CaptureFontReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.CopyFont((IntPtr)0x2002);

            TestAssert.Equal(nextPointer, actual, "TTF.CopyFont must return native font pointer.");
            TestAssert.Equal((IntPtr)0x2002, capturedFont, "TTF.CopyFont must forward existing font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CopyFont must call native hook once.");
        }
    }

    public static void BasicFontStateFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextUInt = 301;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontPropertiesNativeFunction", nameof(CaptureFontReturnUInt)))
        {
            uint actual = SDL3.TTF.GetFontProperties((IntPtr)0x3001);

            TestAssert.Equal(301u, actual, "TTF.GetFontProperties must return native properties.");
            TestAssert.Equal((IntPtr)0x3001, capturedFont, "TTF.GetFontProperties must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontProperties must call native hook once.");
        }

        ResetCaptureState();
        nextUInt = 302;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontGenerationNativeFunction", nameof(CaptureFontReturnUInt)))
        {
            uint actual = SDL3.TTF.GetFontGeneration((IntPtr)0x3002);

            TestAssert.Equal(302u, actual, "TTF.GetFontGeneration must return native generation.");
            TestAssert.Equal((IntPtr)0x3002, capturedFont, "TTF.GetFontGeneration must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontGeneration must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("AddFallbackFontNativeFunction", nameof(CaptureFontFallbackReturnBool)))
        {
            bool actual = SDL3.TTF.AddFallbackFont((IntPtr)0x3003, (IntPtr)0x3004);

            TestAssert.Equal(true, actual, "TTF.AddFallbackFont must return native success value.");
            TestAssert.Equal((IntPtr)0x3003, capturedFont, "TTF.AddFallbackFont must forward font.");
            TestAssert.Equal((IntPtr)0x3004, capturedFallback, "TTF.AddFallbackFont must forward fallback.");
            TestAssert.Equal(1, capturedCallCount, "TTF.AddFallbackFont must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("RemoveFallbackFontNativeFunction", nameof(CaptureFontFallbackVoid)))
        {
            SDL3.TTF.RemoveFallbackFont((IntPtr)0x3005, (IntPtr)0x3006);

            TestAssert.Equal((IntPtr)0x3005, capturedFont, "TTF.RemoveFallbackFont must forward font.");
            TestAssert.Equal((IntPtr)0x3006, capturedFallback, "TTF.RemoveFallbackFont must forward fallback.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RemoveFallbackFont must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("ClearFallbackFontsNativeFunction", nameof(CaptureFontVoid)))
        {
            SDL3.TTF.ClearFallbackFonts((IntPtr)0x3007);

            TestAssert.Equal((IntPtr)0x3007, capturedFont, "TTF.ClearFallbackFonts must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.ClearFallbackFonts must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontSizeNativeFunction", nameof(CaptureFontPointSizeReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontSize((IntPtr)0x3008, 20.5f);

            TestAssert.Equal(true, actual, "TTF.SetFontSize must return native success value.");
            TestAssert.Equal((IntPtr)0x3008, capturedFont, "TTF.SetFontSize must forward font.");
            TestAssert.Equal(20.5f, capturedPointSize, "TTF.SetFontSize must forward point size.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontSize must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontSizeDPINativeFunction", nameof(CaptureFontPointSizeDpiReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontSizeDPI((IntPtr)0x3009, 22.75f, 96, 144);

            TestAssert.Equal(false, actual, "TTF.SetFontSizeDPI must return native success value.");
            TestAssert.Equal((IntPtr)0x3009, capturedFont, "TTF.SetFontSizeDPI must forward font.");
            TestAssert.Equal(22.75f, capturedPointSize, "TTF.SetFontSizeDPI must forward point size.");
            TestAssert.Equal(96, capturedHdpi, "TTF.SetFontSizeDPI must forward horizontal DPI.");
            TestAssert.Equal(144, capturedVdpi, "TTF.SetFontSizeDPI must forward vertical DPI.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontSizeDPI must call native hook once.");
        }

        ResetCaptureState();
        nextFloat = 24.5f;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontSizeNativeFunction", nameof(CaptureFontReturnFloat)))
        {
            float actual = SDL3.TTF.GetFontSize((IntPtr)0x3010);

            TestAssert.Equal(24.5f, actual, "TTF.GetFontSize must return native point size.");
            TestAssert.Equal((IntPtr)0x3010, capturedFont, "TTF.GetFontSize must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontSize must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextHdpi = 120;
        nextVdpi = 160;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontDPINativeFunction", nameof(CaptureFontReturnDpi)))
        {
            bool actual = SDL3.TTF.GetFontDPI((IntPtr)0x3011, out int hdpi, out int vdpi);

            TestAssert.Equal(true, actual, "TTF.GetFontDPI must return native success value.");
            TestAssert.Equal((IntPtr)0x3011, capturedFont, "TTF.GetFontDPI must forward font.");
            TestAssert.Equal(120, hdpi, "TTF.GetFontDPI must return native horizontal DPI.");
            TestAssert.Equal(160, vdpi, "TTF.GetFontDPI must return native vertical DPI.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontDPI must call native hook once.");
        }
    }

    public static void StyleOutlineHintingFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("SetFontStyleNativeFunction", nameof(CaptureFontStyleVoid)))
        {
            SDL3.TTF.SetFontStyle((IntPtr)0x4001, SDL3.TTF.FontStyleFlags.Bold | SDL3.TTF.FontStyleFlags.Italic);

            TestAssert.Equal((IntPtr)0x4001, capturedFont, "TTF.SetFontStyle must forward font.");
            TestAssert.Equal(SDL3.TTF.FontStyleFlags.Bold | SDL3.TTF.FontStyleFlags.Italic, capturedFontStyle, "TTF.SetFontStyle must forward style flags.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontStyle must call native hook once.");
        }

        ResetCaptureState();
        nextFontStyle = SDL3.TTF.FontStyleFlags.Underline | SDL3.TTF.FontStyleFlags.Strikethrough;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontStyleNativeFunction", nameof(CaptureFontReturnStyle)))
        {
            SDL3.TTF.FontStyleFlags actual = SDL3.TTF.GetFontStyle((IntPtr)0x4002);

            TestAssert.Equal(nextFontStyle, actual, "TTF.GetFontStyle must return native style flags.");
            TestAssert.Equal((IntPtr)0x4002, capturedFont, "TTF.GetFontStyle must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontStyle must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontOutlineNativeFunction", nameof(CaptureFontIntReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontOutline((IntPtr)0x4003, 3);

            TestAssert.Equal(true, actual, "TTF.SetFontOutline must return native success value.");
            TestAssert.Equal((IntPtr)0x4003, capturedFont, "TTF.SetFontOutline must forward font.");
            TestAssert.Equal(3, capturedIntValue, "TTF.SetFontOutline must forward outline.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontOutline must call native hook once.");
        }

        ResetCaptureState();
        nextInt = 4;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontOutlineNativeFunction", nameof(CaptureFontReturnInt)))
        {
            int actual = SDL3.TTF.GetFontOutline((IntPtr)0x4004);

            TestAssert.Equal(4, actual, "TTF.GetFontOutline must return native outline.");
            TestAssert.Equal((IntPtr)0x4004, capturedFont, "TTF.GetFontOutline must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontOutline must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("SetFontHintingNativeFunction", nameof(CaptureFontHintingVoid)))
        {
            SDL3.TTF.SetFontHinting((IntPtr)0x4005, SDL3.TTF.HintingFlags.LightSubpixel);

            TestAssert.Equal((IntPtr)0x4005, capturedFont, "TTF.SetFontHinting must forward font.");
            TestAssert.Equal(SDL3.TTF.HintingFlags.LightSubpixel, capturedHinting, "TTF.SetFontHinting must forward hinting.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontHinting must call native hook once.");
        }

        ResetCaptureState();
        nextInt = 5;
        using (NativeHookScope _ = NativeHookScope.Install("GetNumFontFacesNativeFunction", nameof(CaptureFontReturnInt)))
        {
            int actual = SDL3.TTF.GetNumFontFaces((IntPtr)0x4006);

            TestAssert.Equal(5, actual, "TTF.GetNumFontFaces must return native face count.");
            TestAssert.Equal((IntPtr)0x4006, capturedFont, "TTF.GetNumFontFaces must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetNumFontFaces must call native hook once.");
        }

        ResetCaptureState();
        nextHinting = SDL3.TTF.HintingFlags.Mono;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontHintingNativeFunction", nameof(CaptureFontReturnHinting)))
        {
            SDL3.TTF.HintingFlags actual = SDL3.TTF.GetFontHinting((IntPtr)0x4007);

            TestAssert.Equal(SDL3.TTF.HintingFlags.Mono, actual, "TTF.GetFontHinting must return native hinting.");
            TestAssert.Equal((IntPtr)0x4007, capturedFont, "TTF.GetFontHinting must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontHinting must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontSDFNativeFunction", nameof(CaptureFontEnabledReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontSDF((IntPtr)0x4008, true);

            TestAssert.Equal(true, actual, "TTF.SetFontSDF must return native success value.");
            TestAssert.Equal((IntPtr)0x4008, capturedFont, "TTF.SetFontSDF must forward font.");
            TestAssert.Equal(true, capturedEnabled, "TTF.SetFontSDF must forward enabled flag.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontSDF must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontSDFNativeFunction", nameof(CaptureFontReturnBool)))
        {
            bool actual = SDL3.TTF.GetFontSDF((IntPtr)0x4009);

            TestAssert.Equal(false, actual, "TTF.GetFontSDF must return native SDF flag.");
            TestAssert.Equal((IntPtr)0x4009, capturedFont, "TTF.GetFontSDF must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontSDF must call native hook once.");
        }
    }

    public static void FontMetricFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextInt = 700;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontWeightNativeFunction", nameof(CaptureFontInReturnInt)))
        {
            IntPtr font = (IntPtr)0x5001;
            int actual = SDL3.TTF.GetFontWeight(in font);

            TestAssert.Equal(700, actual, "TTF.GetFontWeight must return native weight.");
            TestAssert.Equal((IntPtr)0x5001, capturedFont, "TTF.GetFontWeight must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontWeight must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("SetFontWrapAlignmentNativeFunction", nameof(CaptureFontHorizontalAlignmentVoid)))
        {
            SDL3.TTF.SetFontWrapAlignment((IntPtr)0x5002, SDL3.TTF.HorizontalAlignment.Center);

            TestAssert.Equal((IntPtr)0x5002, capturedFont, "TTF.SetFontWrapAlignment must forward font.");
            TestAssert.Equal(SDL3.TTF.HorizontalAlignment.Center, capturedHorizontalAlignment, "TTF.SetFontWrapAlignment must forward alignment.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontWrapAlignment must call native hook once.");
        }

        ResetCaptureState();
        nextHorizontalAlignment = SDL3.TTF.HorizontalAlignment.Right;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontWrapAlignmentNativeFunction", nameof(CaptureFontReturnHorizontalAlignment)))
        {
            SDL3.TTF.HorizontalAlignment actual = SDL3.TTF.GetFontWrapAlignment((IntPtr)0x5003);

            TestAssert.Equal(SDL3.TTF.HorizontalAlignment.Right, actual, "TTF.GetFontWrapAlignment must return native alignment.");
            TestAssert.Equal((IntPtr)0x5003, capturedFont, "TTF.GetFontWrapAlignment must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontWrapAlignment must call native hook once.");
        }

        AssertFontIntGetter("GetFontHeightNativeFunction", "GetFontHeight", 0x5004, 41);
        AssertFontIntGetter("GetFontAscentNativeFunction", "GetFontAscent", 0x5005, 32);
        AssertFontIntGetter("GetFontDescentNativeFunction", "GetFontDescent", 0x5006, -9);

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("SetFontLineSkipNativeFunction", nameof(CaptureFontIntVoid)))
        {
            SDL3.TTF.SetFontLineSkip((IntPtr)0x5007, 44);

            TestAssert.Equal((IntPtr)0x5007, capturedFont, "TTF.SetFontLineSkip must forward font.");
            TestAssert.Equal(44, capturedIntValue, "TTF.SetFontLineSkip must forward line skip.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontLineSkip must call native hook once.");
        }

        AssertFontIntGetter("GetFontLineSkipNativeFunction", "GetFontLineSkip", 0x5008, 45);
    }

    public static void KerningAndNameFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("SetFontKerningNativeFunction", nameof(CaptureFontEnabledVoid)))
        {
            SDL3.TTF.SetFontKerning((IntPtr)0x6001, true);

            TestAssert.Equal((IntPtr)0x6001, capturedFont, "TTF.SetFontKerning must forward font.");
            TestAssert.Equal(true, capturedEnabled, "TTF.SetFontKerning must forward enabled flag.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontKerning must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontKerningNativeFunction", nameof(CaptureFontReturnBool)))
        {
            bool actual = SDL3.TTF.GetFontKerning((IntPtr)0x6002);

            TestAssert.Equal(true, actual, "TTF.GetFontKerning must return native kerning flag.");
            TestAssert.Equal((IntPtr)0x6002, capturedFont, "TTF.GetFontKerning must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontKerning must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("FontIsFixedWidthNativeFunction", nameof(CaptureFontReturnBool)))
        {
            bool actual = SDL3.TTF.FontIsFixedWidth((IntPtr)0x6003);

            TestAssert.Equal(true, actual, "TTF.FontIsFixedWidth must return native fixed-width flag.");
            TestAssert.Equal((IntPtr)0x6003, capturedFont, "TTF.FontIsFixedWidth must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.FontIsFixedWidth must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("FontIsScalableNativeFunction", nameof(CaptureFontReturnBool)))
        {
            bool actual = SDL3.TTF.FontIsScalable((IntPtr)0x6004);

            TestAssert.Equal(false, actual, "TTF.FontIsScalable must return native scalable flag.");
            TestAssert.Equal((IntPtr)0x6004, capturedFont, "TTF.FontIsScalable must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.FontIsScalable must call native hook once.");
        }

        AssertFontNameWrapper("GetFontFamilyNameNativeFunction", "GetFontFamilyName", 0x6005, "Family Name");
        AssertFontNameWrapper("GetFontStyleNameNativeFunction", "GetFontStyleName", 0x6006, "Style Name");
    }

    public static void DirectionScriptAndGlyphFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontDirectionNativeFunction", nameof(CaptureFontDirectionReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontDirection((IntPtr)0x7001, SDL3.TTF.Direction.RTL);

            TestAssert.Equal(true, actual, "TTF.SetFontDirection must return native success value.");
            TestAssert.Equal((IntPtr)0x7001, capturedFont, "TTF.SetFontDirection must forward font.");
            TestAssert.Equal(SDL3.TTF.Direction.RTL, capturedDirection, "TTF.SetFontDirection must forward direction.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontDirection must call native hook once.");
        }

        ResetCaptureState();
        nextDirection = SDL3.TTF.Direction.BTT;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontDirectionNativeFunction", nameof(CaptureFontReturnDirection)))
        {
            SDL3.TTF.Direction actual = SDL3.TTF.GetFontDirection((IntPtr)0x7002);

            TestAssert.Equal(SDL3.TTF.Direction.BTT, actual, "TTF.GetFontDirection must return native direction.");
            TestAssert.Equal((IntPtr)0x7002, capturedFont, "TTF.GetFontDirection must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontDirection must call native hook once.");
        }

        ResetCaptureState();
        nextUInt = 0x4C61746Eu;
        using (NativeHookScope _ = NativeHookScope.Install("StringToTagNativeFunction", nameof(CaptureStringReturnUInt)))
        {
            uint actual = SDL3.TTF.StringToTag("Latn");

            TestAssert.Equal(0x4C61746Eu, actual, "TTF.StringToTag must return native tag.");
            TestAssert.Equal("Latn", capturedString, "TTF.StringToTag must forward string.");
            TestAssert.Equal(1, capturedCallCount, "TTF.StringToTag must call native hook once.");
        }

        ResetCaptureState();
        nextString = "Cyrl";
        using (NativeHookScope _ = NativeHookScope.Install("TagToStringNativeFunction", nameof(CaptureTagToString)))
        {
            SDL3.TTF.TagToString(0x4379726Cu, out string value, (UIntPtr)5);

            TestAssert.Equal("Cyrl", value, "TTF.TagToString must return native string.");
            TestAssert.Equal(0x4379726Cu, capturedUIntValue, "TTF.TagToString must forward tag.");
            TestAssert.Equal((UIntPtr)5, capturedSize, "TTF.TagToString must forward buffer size.");
            TestAssert.Equal(1, capturedCallCount, "TTF.TagToString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontScriptNativeFunction", nameof(CaptureFontUIntReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontScript((IntPtr)0x7005, 0x4C61746Eu);

            TestAssert.Equal(true, actual, "TTF.SetFontScript must return native success value.");
            TestAssert.Equal((IntPtr)0x7005, capturedFont, "TTF.SetFontScript must forward font.");
            TestAssert.Equal(0x4C61746Eu, capturedUIntValue, "TTF.SetFontScript must forward script.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontScript must call native hook once.");
        }

        ResetCaptureState();
        nextUInt = 0x4772656Bu;
        using (NativeHookScope _ = NativeHookScope.Install("GetFontScriptNativeFunction", nameof(CaptureFontReturnUInt)))
        {
            uint actual = SDL3.TTF.GetFontScript((IntPtr)0x7006);

            TestAssert.Equal(0x4772656Bu, actual, "TTF.GetFontScript must return native script.");
            TestAssert.Equal((IntPtr)0x7006, capturedFont, "TTF.GetFontScript must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetFontScript must call native hook once.");
        }

        ResetCaptureState();
        nextUInt = 0x48616E73u;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphScriptNativeFunction", nameof(CaptureUIntReturnUInt)))
        {
            uint actual = SDL3.TTF.GetGlyphScript(0x4E2D);

            TestAssert.Equal(0x48616E73u, actual, "TTF.GetGlyphScript must return native script.");
            TestAssert.Equal(0x4E2Du, capturedUIntValue, "TTF.GetGlyphScript must forward codepoint.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphScript must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontLanguageNativeFunction", nameof(CaptureFontStringReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontLanguage((IntPtr)0x7007, "en-US");

            TestAssert.Equal(true, actual, "TTF.SetFontLanguage must return native success value.");
            TestAssert.Equal((IntPtr)0x7007, capturedFont, "TTF.SetFontLanguage must forward font.");
            TestAssert.Equal("en-US", capturedString, "TTF.SetFontLanguage must forward language.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontLanguage must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetFontLanguageNativeFunction", nameof(CaptureFontStringReturnBool)))
        {
            bool actual = SDL3.TTF.SetFontLanguage((IntPtr)0x7008, null);

            TestAssert.Equal(false, actual, "TTF.SetFontLanguage must return native reset result.");
            TestAssert.Equal((IntPtr)0x7008, capturedFont, "TTF.SetFontLanguage null path must forward font.");
            TestAssert.Equal<string?>(null, capturedString, "TTF.SetFontLanguage must forward null language.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetFontLanguage null path must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("FontHasGlyphNativeFunction", nameof(CaptureFontUIntReturnBool)))
        {
            bool actual = SDL3.TTF.FontHasGlyph((IntPtr)0x7009, 0x41);

            TestAssert.Equal(true, actual, "TTF.FontHasGlyph must return native glyph availability.");
            TestAssert.Equal((IntPtr)0x7009, capturedFont, "TTF.FontHasGlyph must forward font.");
            TestAssert.Equal(0x41u, capturedUIntValue, "TTF.FontHasGlyph must forward codepoint.");
            TestAssert.Equal(1, capturedCallCount, "TTF.FontHasGlyph must call native hook once.");
        }
    }

    public static void GlyphImageMetricsAndStringSizingFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x8001;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphImageNativeFunction", nameof(CaptureGlyphImagePointer)))
        {
            IntPtr actual = SDL3.TTF.GetGlyphImage((IntPtr)0x8002, 0x41, (IntPtr)0x8003);

            TestAssert.Equal((IntPtr)0x8001, actual, "TTF.GetGlyphImage pointer overload must return native surface.");
            TestAssert.Equal((IntPtr)0x8002, capturedFont, "TTF.GetGlyphImage pointer overload must forward font.");
            TestAssert.Equal(0x41u, capturedUIntValue, "TTF.GetGlyphImage pointer overload must forward codepoint.");
            TestAssert.Equal((IntPtr)0x8003, capturedImageTypePointer, "TTF.GetGlyphImage pointer overload must forward image type pointer.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphImage pointer overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x8004;
        nextImageType = SDL3.TTF.ImageType.Color;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphImageOutNativeFunction", nameof(CaptureGlyphImageOut)))
        {
            IntPtr actual = SDL3.TTF.GetGlyphImage((IntPtr)0x8005, 0x42, out SDL3.TTF.ImageType imageType);

            TestAssert.Equal((IntPtr)0x8004, actual, "TTF.GetGlyphImage out overload must return native surface.");
            TestAssert.Equal(SDL3.TTF.ImageType.Color, imageType, "TTF.GetGlyphImage out overload must return native image type.");
            TestAssert.Equal((IntPtr)0x8005, capturedFont, "TTF.GetGlyphImage out overload must forward font.");
            TestAssert.Equal(0x42u, capturedUIntValue, "TTF.GetGlyphImage out overload must forward codepoint.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphImage out overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x8006;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphImageForIndexNativeFunction", nameof(CaptureGlyphImagePointer)))
        {
            IntPtr actual = SDL3.TTF.GetGlyphImageForIndex((IntPtr)0x8007, 77, (IntPtr)0x8008);

            TestAssert.Equal((IntPtr)0x8006, actual, "TTF.GetGlyphImageForIndex pointer overload must return native surface.");
            TestAssert.Equal((IntPtr)0x8007, capturedFont, "TTF.GetGlyphImageForIndex pointer overload must forward font.");
            TestAssert.Equal(77u, capturedUIntValue, "TTF.GetGlyphImageForIndex pointer overload must forward glyph index.");
            TestAssert.Equal((IntPtr)0x8008, capturedImageTypePointer, "TTF.GetGlyphImageForIndex pointer overload must forward image type pointer.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphImageForIndex pointer overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x8009;
        nextImageType = SDL3.TTF.ImageType.SDF;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphImageForIndexOutNativeFunction", nameof(CaptureGlyphImageOut)))
        {
            IntPtr actual = SDL3.TTF.GetGlyphImageForIndex((IntPtr)0x8010, 88, out SDL3.TTF.ImageType imageType);

            TestAssert.Equal((IntPtr)0x8009, actual, "TTF.GetGlyphImageForIndex out overload must return native surface.");
            TestAssert.Equal(SDL3.TTF.ImageType.SDF, imageType, "TTF.GetGlyphImageForIndex out overload must return native image type.");
            TestAssert.Equal((IntPtr)0x8010, capturedFont, "TTF.GetGlyphImageForIndex out overload must forward font.");
            TestAssert.Equal(88u, capturedUIntValue, "TTF.GetGlyphImageForIndex out overload must forward glyph index.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphImageForIndex out overload must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextMinX = -2;
        nextMaxX = 12;
        nextMinY = -5;
        nextMaxY = 18;
        nextAdvance = 14;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphMetricsNativeFunction", nameof(CaptureGlyphMetrics)))
        {
            bool actual = SDL3.TTF.GetGlyphMetrics((IntPtr)0x8011, 0x43, out int minx, out int maxx, out int miny, out int maxy, out int advance);

            TestAssert.Equal(true, actual, "TTF.GetGlyphMetrics must return native success value.");
            TestAssert.Equal(-2, minx, "TTF.GetGlyphMetrics must return native minx.");
            TestAssert.Equal(12, maxx, "TTF.GetGlyphMetrics must return native maxx.");
            TestAssert.Equal(-5, miny, "TTF.GetGlyphMetrics must return native miny.");
            TestAssert.Equal(18, maxy, "TTF.GetGlyphMetrics must return native maxy.");
            TestAssert.Equal(14, advance, "TTF.GetGlyphMetrics must return native advance.");
            TestAssert.Equal((IntPtr)0x8011, capturedFont, "TTF.GetGlyphMetrics must forward font.");
            TestAssert.Equal(0x43u, capturedUIntValue, "TTF.GetGlyphMetrics must forward codepoint.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphMetrics must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        nextKerning = -3;
        using (NativeHookScope _ = NativeHookScope.Install("GetGlyphKerningNativeFunction", nameof(CaptureGlyphKerning)))
        {
            bool actual = SDL3.TTF.GetGlyphKerning((IntPtr)0x8012, 0x41, 0x56, out int kerning);

            TestAssert.Equal(false, actual, "TTF.GetGlyphKerning must return native success value.");
            TestAssert.Equal(-3, kerning, "TTF.GetGlyphKerning must return native kerning.");
            TestAssert.Equal((IntPtr)0x8012, capturedFont, "TTF.GetGlyphKerning must forward font.");
            TestAssert.Equal(0x41u, capturedPreviousUIntValue, "TTF.GetGlyphKerning must forward previous codepoint.");
            TestAssert.Equal(0x56u, capturedUIntValue, "TTF.GetGlyphKerning must forward current codepoint.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGlyphKerning must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 90;
        nextHeight = 18;
        using (NativeHookScope _ = NativeHookScope.Install("GetStringSizeNativeFunction", nameof(CaptureStringSize)))
        {
            bool actual = SDL3.TTF.GetStringSize((IntPtr)0x8013, "Hello", (UIntPtr)5, out int w, out int h);

            TestAssert.Equal(true, actual, "TTF.GetStringSize string overload must return native success value.");
            TestAssert.Equal(90, w, "TTF.GetStringSize string overload must return native width.");
            TestAssert.Equal(18, h, "TTF.GetStringSize string overload must return native height.");
            TestAssert.Equal((IntPtr)0x8013, capturedFont, "TTF.GetStringSize string overload must forward font.");
            TestAssert.Equal("Hello", capturedString, "TTF.GetStringSize string overload must forward text.");
            TestAssert.Equal((UIntPtr)5, capturedLength, "TTF.GetStringSize string overload must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetStringSize string overload must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 44;
        nextHeight = 11;
        using (NativeHookScope _ = NativeHookScope.Install("GetStringSizeBytePointerNativeFunction", nameof(CaptureStringSizeBytePointer)))
        {
            unsafe
            {
                byte[] textBytes = [65, 66, 0];
                fixed (byte* text = textBytes)
                {
                    bool actual = SDL3.TTF.GetStringSize((IntPtr)0x8014, text, (UIntPtr)2, out int w, out int h);

                    TestAssert.Equal(true, actual, "TTF.GetStringSize byte pointer overload must return native success value.");
                    TestAssert.Equal(44, w, "TTF.GetStringSize byte pointer overload must return native width.");
                    TestAssert.Equal(11, h, "TTF.GetStringSize byte pointer overload must return native height.");
                    TestAssert.Equal((IntPtr)0x8014, capturedFont, "TTF.GetStringSize byte pointer overload must forward font.");
                    TestAssert.Equal((IntPtr)text, capturedTextPointer, "TTF.GetStringSize byte pointer overload must forward text pointer.");
                    TestAssert.Equal((UIntPtr)2, capturedLength, "TTF.GetStringSize byte pointer overload must forward length.");
                    TestAssert.Equal(1, capturedCallCount, "TTF.GetStringSize byte pointer overload must call native hook once.");
                }
            }
        }

        ResetCaptureState();
        nextBool = false;
        nextWidth = 30;
        nextHeight = 9;
        using (NativeHookScope _ = NativeHookScope.Install("GetStringSizePointerNativeFunction", nameof(CaptureStringSizePointer)))
        {
            bool actual = SDL3.TTF.GetStringSize((IntPtr)0x8015, (IntPtr)0x8016, (UIntPtr)3, out int w, out int h);

            TestAssert.Equal(false, actual, "TTF.GetStringSize pointer overload must return native success value.");
            TestAssert.Equal(30, w, "TTF.GetStringSize pointer overload must return native width.");
            TestAssert.Equal(9, h, "TTF.GetStringSize pointer overload must return native height.");
            TestAssert.Equal((IntPtr)0x8015, capturedFont, "TTF.GetStringSize pointer overload must forward font.");
            TestAssert.Equal((IntPtr)0x8016, capturedTextPointer, "TTF.GetStringSize pointer overload must forward text pointer.");
            TestAssert.Equal((UIntPtr)3, capturedLength, "TTF.GetStringSize pointer overload must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetStringSize pointer overload must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 120;
        nextHeight = 42;
        using (NativeHookScope _ = NativeHookScope.Install("GetStringSizeWrappedNativeFunction", nameof(CaptureStringSizeWrapped)))
        {
            bool actual = SDL3.TTF.GetStringSizeWrapped((IntPtr)0x8017, "Wrapped", (UIntPtr)7, 64, out int w, out int h);

            TestAssert.Equal(true, actual, "TTF.GetStringSizeWrapped must return native success value.");
            TestAssert.Equal(120, w, "TTF.GetStringSizeWrapped must return native width.");
            TestAssert.Equal(42, h, "TTF.GetStringSizeWrapped must return native height.");
            TestAssert.Equal((IntPtr)0x8017, capturedFont, "TTF.GetStringSizeWrapped must forward font.");
            TestAssert.Equal("Wrapped", capturedString, "TTF.GetStringSizeWrapped must forward text.");
            TestAssert.Equal((UIntPtr)7, capturedLength, "TTF.GetStringSizeWrapped must forward length.");
            TestAssert.Equal(64, capturedMaxWidth, "TTF.GetStringSizeWrapped must forward wrap width.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetStringSizeWrapped must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextMeasuredWidth = 53;
        nextMeasuredLength = 6;
        using (NativeHookScope _ = NativeHookScope.Install("MeasureStringNativeFunction", nameof(CaptureMeasureString)))
        {
            bool actual = SDL3.TTF.MeasureString((IntPtr)0x8018, "Measure", (UIntPtr)7, 54, out int measuredWidth, out ulong measuredLength);

            TestAssert.Equal(true, actual, "TTF.MeasureString must return native success value.");
            TestAssert.Equal(53, measuredWidth, "TTF.MeasureString must return native measured width.");
            TestAssert.Equal(6ul, measuredLength, "TTF.MeasureString must return native measured length.");
            TestAssert.Equal((IntPtr)0x8018, capturedFont, "TTF.MeasureString must forward font.");
            TestAssert.Equal("Measure", capturedString, "TTF.MeasureString must forward text.");
            TestAssert.Equal((UIntPtr)7, capturedLength, "TTF.MeasureString must forward length.");
            TestAssert.Equal(54, capturedMaxWidth, "TTF.MeasureString must forward max width.");
            TestAssert.Equal(1, capturedCallCount, "TTF.MeasureString must call native hook once.");
        }
    }

    public static void RenderSurfaceFunctions_ForwardInputsAndReturnNativeSurfaces()
    {
        SDL3.SDL.Color fg = Color(1, 2, 3, 4);
        SDL3.SDL.Color bg = Color(5, 6, 7, 8);

        ResetCaptureState();
        nextPointer = (IntPtr)0x9001;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextSolidStringNativeFunction", nameof(CaptureRenderTextString)))
        {
            IntPtr actual = SDL3.TTF.RenderTextSolid((IntPtr)0x9002, "Solid", (UIntPtr)5, fg);

            TestAssert.Equal((IntPtr)0x9001, actual, "TTF.RenderTextSolid string overload must return native surface.");
            TestAssert.Equal((IntPtr)0x9002, capturedFont, "TTF.RenderTextSolid string overload must forward font.");
            TestAssert.Equal("Solid", capturedString, "TTF.RenderTextSolid string overload must forward text.");
            TestAssert.Equal((UIntPtr)5, capturedLength, "TTF.RenderTextSolid string overload must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextSolid string overload must forward foreground color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextSolid string overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9003;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextSolidPointerNativeFunction", nameof(CaptureRenderTextPointer)))
        {
            IntPtr actual = SDL3.TTF.RenderTextSolid((IntPtr)0x9004, (IntPtr)0x9005, (UIntPtr)6, fg);

            TestAssert.Equal((IntPtr)0x9003, actual, "TTF.RenderTextSolid pointer overload must return native surface.");
            TestAssert.Equal((IntPtr)0x9004, capturedFont, "TTF.RenderTextSolid pointer overload must forward font.");
            TestAssert.Equal((IntPtr)0x9005, capturedTextPointer, "TTF.RenderTextSolid pointer overload must forward text pointer.");
            TestAssert.Equal((UIntPtr)6, capturedLength, "TTF.RenderTextSolid pointer overload must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextSolid pointer overload must forward foreground color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextSolid pointer overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9006;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextSolidBytePointerNativeFunction", nameof(CaptureRenderTextBytePointer)))
        {
            unsafe
            {
                byte[] textBytes = [83, 111, 108, 105, 100, 0];
                fixed (byte* text = textBytes)
                {
                    IntPtr actual = SDL3.TTF.RenderTextSolid((IntPtr)0x9007, text, (UIntPtr)5, fg);

                    TestAssert.Equal((IntPtr)0x9006, actual, "TTF.RenderTextSolid byte pointer overload must return native surface.");
                    TestAssert.Equal((IntPtr)0x9007, capturedFont, "TTF.RenderTextSolid byte pointer overload must forward font.");
                    TestAssert.Equal((IntPtr)text, capturedTextPointer, "TTF.RenderTextSolid byte pointer overload must forward text pointer.");
                    TestAssert.Equal((UIntPtr)5, capturedLength, "TTF.RenderTextSolid byte pointer overload must forward length.");
                    TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextSolid byte pointer overload must forward foreground color.");
                    TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextSolid byte pointer overload must call native hook once.");
                }
            }
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9008;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextSolidWrappedNativeFunction", nameof(CaptureRenderTextWrapped)))
        {
            IntPtr actual = SDL3.TTF.RenderTextSolidWrapped((IntPtr)0x9009, "Wrap", (UIntPtr)4, fg, 80);

            TestAssert.Equal((IntPtr)0x9008, actual, "TTF.RenderTextSolidWrapped must return native surface.");
            TestAssert.Equal((IntPtr)0x9009, capturedFont, "TTF.RenderTextSolidWrapped must forward font.");
            TestAssert.Equal("Wrap", capturedString, "TTF.RenderTextSolidWrapped must forward text.");
            TestAssert.Equal((UIntPtr)4, capturedLength, "TTF.RenderTextSolidWrapped must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextSolidWrapped must forward foreground color.");
            TestAssert.Equal(80, capturedWrapWidth, "TTF.RenderTextSolidWrapped must forward wrap length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextSolidWrapped must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9010;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGlyphSolidNativeFunction", nameof(CaptureRenderGlyphUInt)))
        {
            IntPtr actual = SDL3.TTF.RenderGlyphSolid((IntPtr)0x9011, 0x41, fg);

            TestAssert.Equal((IntPtr)0x9010, actual, "TTF.RenderGlyphSolid must return native surface.");
            TestAssert.Equal((IntPtr)0x9011, capturedFont, "TTF.RenderGlyphSolid must forward font.");
            TestAssert.Equal(0x41u, capturedUIntValue, "TTF.RenderGlyphSolid must forward codepoint.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderGlyphSolid must forward foreground color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderGlyphSolid must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9012;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextShadedNativeFunction", nameof(CaptureRenderTextStringWithBackground)))
        {
            IntPtr actual = SDL3.TTF.RenderTextShaded((IntPtr)0x9013, "Shaded", (UIntPtr)6, fg, bg);

            TestAssert.Equal((IntPtr)0x9012, actual, "TTF.RenderTextShaded must return native surface.");
            TestAssert.Equal((IntPtr)0x9013, capturedFont, "TTF.RenderTextShaded must forward font.");
            TestAssert.Equal("Shaded", capturedString, "TTF.RenderTextShaded must forward text.");
            TestAssert.Equal((UIntPtr)6, capturedLength, "TTF.RenderTextShaded must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextShaded must forward foreground color.");
            TestAssert.Equal(bg, capturedBackground, "TTF.RenderTextShaded must forward background color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextShaded must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9014;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextShadedWrappedNativeFunction", nameof(CaptureRenderTextWrappedWithBackground)))
        {
            IntPtr actual = SDL3.TTF.RenderTextShadedWrapped((IntPtr)0x9015, "ShadeWrap", (UIntPtr)9, fg, bg, 96);

            TestAssert.Equal((IntPtr)0x9014, actual, "TTF.RenderTextShadedWrapped must return native surface.");
            TestAssert.Equal((IntPtr)0x9015, capturedFont, "TTF.RenderTextShadedWrapped must forward font.");
            TestAssert.Equal("ShadeWrap", capturedString, "TTF.RenderTextShadedWrapped must forward text.");
            TestAssert.Equal((UIntPtr)9, capturedLength, "TTF.RenderTextShadedWrapped must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextShadedWrapped must forward foreground color.");
            TestAssert.Equal(bg, capturedBackground, "TTF.RenderTextShadedWrapped must forward background color.");
            TestAssert.Equal(96, capturedWrapWidth, "TTF.RenderTextShadedWrapped must forward wrap width.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextShadedWrapped must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9016;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGlyphShadedNativeFunction", nameof(CaptureRenderGlyphUIntWithBackground)))
        {
            IntPtr actual = SDL3.TTF.RenderGlyphShaded((IntPtr)0x9017, 0x42, fg, bg);

            TestAssert.Equal((IntPtr)0x9016, actual, "TTF.RenderGlyphShaded must return native surface.");
            TestAssert.Equal((IntPtr)0x9017, capturedFont, "TTF.RenderGlyphShaded must forward font.");
            TestAssert.Equal(0x42u, capturedUIntValue, "TTF.RenderGlyphShaded must forward codepoint.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderGlyphShaded must forward foreground color.");
            TestAssert.Equal(bg, capturedBackground, "TTF.RenderGlyphShaded must forward background color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderGlyphShaded must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9018;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextBlendedStringNativeFunction", nameof(CaptureRenderTextString)))
        {
            IntPtr actual = SDL3.TTF.RenderTextBlended((IntPtr)0x9019, "Blend", (UIntPtr)5, fg);

            TestAssert.Equal((IntPtr)0x9018, actual, "TTF.RenderTextBlended string overload must return native surface.");
            TestAssert.Equal((IntPtr)0x9019, capturedFont, "TTF.RenderTextBlended string overload must forward font.");
            TestAssert.Equal("Blend", capturedString, "TTF.RenderTextBlended string overload must forward text.");
            TestAssert.Equal((UIntPtr)5, capturedLength, "TTF.RenderTextBlended string overload must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextBlended string overload must forward foreground color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextBlended string overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9020;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextBlendedPointerNativeFunction", nameof(CaptureRenderTextPointer)))
        {
            IntPtr actual = SDL3.TTF.RenderTextBlended((IntPtr)0x9021, (IntPtr)0x9022, (UIntPtr)6, fg);

            TestAssert.Equal((IntPtr)0x9020, actual, "TTF.RenderTextBlended pointer overload must return native surface.");
            TestAssert.Equal((IntPtr)0x9021, capturedFont, "TTF.RenderTextBlended pointer overload must forward font.");
            TestAssert.Equal((IntPtr)0x9022, capturedTextPointer, "TTF.RenderTextBlended pointer overload must forward text pointer.");
            TestAssert.Equal((UIntPtr)6, capturedLength, "TTF.RenderTextBlended pointer overload must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextBlended pointer overload must forward foreground color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextBlended pointer overload must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9023;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextBlendedBytePointerNativeFunction", nameof(CaptureRenderTextBytePointer)))
        {
            unsafe
            {
                byte[] textBytes = [66, 108, 101, 110, 100, 0];
                fixed (byte* text = textBytes)
                {
                    IntPtr actual = SDL3.TTF.RenderTextBlended((IntPtr)0x9024, text, (UIntPtr)5, fg);

                    TestAssert.Equal((IntPtr)0x9023, actual, "TTF.RenderTextBlended byte pointer overload must return native surface.");
                    TestAssert.Equal((IntPtr)0x9024, capturedFont, "TTF.RenderTextBlended byte pointer overload must forward font.");
                    TestAssert.Equal((IntPtr)text, capturedTextPointer, "TTF.RenderTextBlended byte pointer overload must forward text pointer.");
                    TestAssert.Equal((UIntPtr)5, capturedLength, "TTF.RenderTextBlended byte pointer overload must forward length.");
                    TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextBlended byte pointer overload must forward foreground color.");
                    TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextBlended byte pointer overload must call native hook once.");
                }
            }
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9025;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextBlendedWrappedNativeFunction", nameof(CaptureRenderTextWrapped)))
        {
            IntPtr actual = SDL3.TTF.RenderTextBlendedWrapped((IntPtr)0x9026, "BlendWrap", (UIntPtr)9, fg, 112);

            TestAssert.Equal((IntPtr)0x9025, actual, "TTF.RenderTextBlendedWrapped must return native surface.");
            TestAssert.Equal((IntPtr)0x9026, capturedFont, "TTF.RenderTextBlendedWrapped must forward font.");
            TestAssert.Equal("BlendWrap", capturedString, "TTF.RenderTextBlendedWrapped must forward text.");
            TestAssert.Equal((UIntPtr)9, capturedLength, "TTF.RenderTextBlendedWrapped must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextBlendedWrapped must forward foreground color.");
            TestAssert.Equal(112, capturedWrapWidth, "TTF.RenderTextBlendedWrapped must forward wrap width.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextBlendedWrapped must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9027;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGlyphBlendedNativeFunction", nameof(CaptureRenderGlyphULong)))
        {
            IntPtr actual = SDL3.TTF.RenderGlyphBlended((IntPtr)0x9028, 0x1F642, fg);

            TestAssert.Equal((IntPtr)0x9027, actual, "TTF.RenderGlyphBlended must return native surface.");
            TestAssert.Equal((IntPtr)0x9028, capturedFont, "TTF.RenderGlyphBlended must forward font.");
            TestAssert.Equal(0x1F642ul, capturedULongValue, "TTF.RenderGlyphBlended must forward codepoint.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderGlyphBlended must forward foreground color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderGlyphBlended must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9029;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextLCDNativeFunction", nameof(CaptureRenderTextStringWithBackground)))
        {
            IntPtr actual = SDL3.TTF.RenderTextLCD((IntPtr)0x9030, "LCD", (UIntPtr)3, fg, bg);

            TestAssert.Equal((IntPtr)0x9029, actual, "TTF.RenderTextLCD must return native surface.");
            TestAssert.Equal((IntPtr)0x9030, capturedFont, "TTF.RenderTextLCD must forward font.");
            TestAssert.Equal("LCD", capturedString, "TTF.RenderTextLCD must forward text.");
            TestAssert.Equal((UIntPtr)3, capturedLength, "TTF.RenderTextLCD must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextLCD must forward foreground color.");
            TestAssert.Equal(bg, capturedBackground, "TTF.RenderTextLCD must forward background color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextLCD must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9031;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextLCDWrappedNativeFunction", nameof(CaptureRenderTextWrappedWithBackground)))
        {
            IntPtr actual = SDL3.TTF.RenderTextLCDWrapped((IntPtr)0x9032, "LCDWrap", (UIntPtr)7, fg, bg, 128);

            TestAssert.Equal((IntPtr)0x9031, actual, "TTF.RenderTextLCDWrapped must return native surface.");
            TestAssert.Equal((IntPtr)0x9032, capturedFont, "TTF.RenderTextLCDWrapped must forward font.");
            TestAssert.Equal("LCDWrap", capturedString, "TTF.RenderTextLCDWrapped must forward text.");
            TestAssert.Equal((UIntPtr)7, capturedLength, "TTF.RenderTextLCDWrapped must forward length.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderTextLCDWrapped must forward foreground color.");
            TestAssert.Equal(bg, capturedBackground, "TTF.RenderTextLCDWrapped must forward background color.");
            TestAssert.Equal(128, capturedWrapWidth, "TTF.RenderTextLCDWrapped must forward wrap width.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderTextLCDWrapped must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x9033;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGlyphLCDNativeFunction", nameof(CaptureRenderGlyphUIntWithBackground)))
        {
            IntPtr actual = SDL3.TTF.RenderGlyphLCD((IntPtr)0x9034, 0x43, fg, bg);

            TestAssert.Equal((IntPtr)0x9033, actual, "TTF.RenderGlyphLCD must return native surface.");
            TestAssert.Equal((IntPtr)0x9034, capturedFont, "TTF.RenderGlyphLCD must forward font.");
            TestAssert.Equal(0x43u, capturedUIntValue, "TTF.RenderGlyphLCD must forward codepoint.");
            TestAssert.Equal(fg, capturedForeground, "TTF.RenderGlyphLCD must forward foreground color.");
            TestAssert.Equal(bg, capturedBackground, "TTF.RenderGlyphLCD must forward background color.");
            TestAssert.Equal(1, capturedCallCount, "TTF.RenderGlyphLCD must call native hook once.");
        }
    }

    public static void TextEngineFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0xA001;
        using (NativeHookScope _ = NativeHookScope.Install("CreateSurfaceTextEngineNativeFunction", nameof(ReturnNextPointer)))
        {
            IntPtr actual = SDL3.TTF.CreateSurfaceTextEngine();

            TestAssert.Equal((IntPtr)0xA001, actual, "TTF.CreateSurfaceTextEngine must return native engine pointer.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CreateSurfaceTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("DrawSurfaceTextNativeFunction", nameof(CaptureDrawSurfaceText)))
        {
            bool actual = SDL3.TTF.DrawSurfaceText((IntPtr)0xA002, 10, 20, (IntPtr)0xA003);

            TestAssert.Equal(true, actual, "TTF.DrawSurfaceText must return native success value.");
            TestAssert.Equal((IntPtr)0xA002, capturedTextHandle, "TTF.DrawSurfaceText must forward text.");
            TestAssert.Equal(10, capturedX, "TTF.DrawSurfaceText must forward x.");
            TestAssert.Equal(20, capturedY, "TTF.DrawSurfaceText must forward y.");
            TestAssert.Equal((IntPtr)0xA003, capturedSurface, "TTF.DrawSurfaceText must forward surface.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DrawSurfaceText must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("DestroySurfaceTextEngineNativeFunction", nameof(CaptureEngineVoid)))
        {
            SDL3.TTF.DestroySurfaceTextEngine((IntPtr)0xA004);

            TestAssert.Equal((IntPtr)0xA004, capturedEngine, "TTF.DestroySurfaceTextEngine must forward engine.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DestroySurfaceTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xA005;
        using (NativeHookScope _ = NativeHookScope.Install("CreateRendererTextEngineNativeFunction", nameof(CaptureRendererReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.CreateRendererTextEngine((IntPtr)0xA006);

            TestAssert.Equal((IntPtr)0xA005, actual, "TTF.CreateRendererTextEngine must return native engine pointer.");
            TestAssert.Equal((IntPtr)0xA006, capturedRenderer, "TTF.CreateRendererTextEngine must forward renderer.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CreateRendererTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xA007;
        using (NativeHookScope _ = NativeHookScope.Install("CreateRendererTextEngineWithPropertiesNativeFunction", nameof(CaptureUIntReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.CreateRendererTextEngineWithProperties(0xA008);

            TestAssert.Equal((IntPtr)0xA007, actual, "TTF.CreateRendererTextEngineWithProperties must return native engine pointer.");
            TestAssert.Equal(0xA008u, capturedProps, "TTF.CreateRendererTextEngineWithProperties must forward properties.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CreateRendererTextEngineWithProperties must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("DrawRendererTextNativeFunction", nameof(CaptureDrawRendererText)))
        {
            bool actual = SDL3.TTF.DrawRendererText((IntPtr)0xA009, 12.5f, 24.25f);

            TestAssert.Equal(false, actual, "TTF.DrawRendererText must return native success value.");
            TestAssert.Equal((IntPtr)0xA009, capturedTextHandle, "TTF.DrawRendererText must forward text.");
            TestAssert.Equal(12.5f, capturedXFloat, "TTF.DrawRendererText must forward x.");
            TestAssert.Equal(24.25f, capturedYFloat, "TTF.DrawRendererText must forward y.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DrawRendererText must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("DestroyRendererTextEngineNativeFunction", nameof(CaptureEngineVoid)))
        {
            SDL3.TTF.DestroyRendererTextEngine((IntPtr)0xA010);

            TestAssert.Equal((IntPtr)0xA010, capturedEngine, "TTF.DestroyRendererTextEngine must forward engine.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DestroyRendererTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xA011;
        using (NativeHookScope _ = NativeHookScope.Install("CreateGPUTextEngineNativeFunction", nameof(CaptureDeviceReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.CreateGPUTextEngine((IntPtr)0xA012);

            TestAssert.Equal((IntPtr)0xA011, actual, "TTF.CreateGPUTextEngine must return native engine pointer.");
            TestAssert.Equal((IntPtr)0xA012, capturedDevice, "TTF.CreateGPUTextEngine must forward device.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CreateGPUTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xA013;
        using (NativeHookScope _ = NativeHookScope.Install("CreateGPUTextEngineWithPropertiesNativeFunction", nameof(CaptureUIntReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.CreateGPUTextEngineWithProperties(0xA014);

            TestAssert.Equal((IntPtr)0xA013, actual, "TTF.CreateGPUTextEngineWithProperties must return native engine pointer.");
            TestAssert.Equal(0xA014u, capturedProps, "TTF.CreateGPUTextEngineWithProperties must forward properties.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CreateGPUTextEngineWithProperties must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xA015;
        using (NativeHookScope _ = NativeHookScope.Install("GetGPUTextDrawDataNativeFunction", nameof(CaptureTextReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.GetGPUTextDrawData((IntPtr)0xA016);

            TestAssert.Equal((IntPtr)0xA015, actual, "TTF.GetGPUTextDrawData must return native draw sequence pointer.");
            TestAssert.Equal((IntPtr)0xA016, capturedTextHandle, "TTF.GetGPUTextDrawData must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGPUTextDrawData must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("DestroyGPUTextEngineNativeFunction", nameof(CaptureEngineVoid)))
        {
            SDL3.TTF.DestroyGPUTextEngine((IntPtr)0xA017);

            TestAssert.Equal((IntPtr)0xA017, capturedEngine, "TTF.DestroyGPUTextEngine must forward engine.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DestroyGPUTextEngine must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("SetGPUTextEngineWindingNativeFunction", nameof(CaptureEngineWindingVoid)))
        {
            SDL3.TTF.SetGPUTextEngineWinding((IntPtr)0xA018, SDL3.TTF.GPUTextEngineWinding.CounterClockwise);

            TestAssert.Equal((IntPtr)0xA018, capturedEngine, "TTF.SetGPUTextEngineWinding must forward engine.");
            TestAssert.Equal(SDL3.TTF.GPUTextEngineWinding.CounterClockwise, capturedWinding, "TTF.SetGPUTextEngineWinding must forward winding.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetGPUTextEngineWinding must call native hook once.");
        }

        ResetCaptureState();
        nextWinding = SDL3.TTF.GPUTextEngineWinding.Clockwise;
        using (NativeHookScope _ = NativeHookScope.Install("GetGPUTextEngineWindingNativeFunction", nameof(CaptureEngineReturnWinding)))
        {
            SDL3.TTF.GPUTextEngineWinding actual = SDL3.TTF.GetGPUTextEngineWinding((IntPtr)0xA019);

            TestAssert.Equal(SDL3.TTF.GPUTextEngineWinding.Clockwise, actual, "TTF.GetGPUTextEngineWinding must return native winding.");
            TestAssert.Equal((IntPtr)0xA019, capturedEngine, "TTF.GetGPUTextEngineWinding must forward engine.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetGPUTextEngineWinding must call native hook once.");
        }
    }

    public static void TextObjectStateFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0xB001;
        using (NativeHookScope _ = NativeHookScope.Install("CreateTextNativeFunction", nameof(CaptureCreateText)))
        {
            IntPtr actual = SDL3.TTF.CreateText((IntPtr)0xB002, (IntPtr)0xB003, "Text", (UIntPtr)4);

            TestAssert.Equal((IntPtr)0xB001, actual, "TTF.CreateText must return native text pointer.");
            TestAssert.Equal((IntPtr)0xB002, capturedEngine, "TTF.CreateText must forward engine.");
            TestAssert.Equal((IntPtr)0xB003, capturedFont, "TTF.CreateText must forward font.");
            TestAssert.Equal("Text", capturedString, "TTF.CreateText must forward text.");
            TestAssert.Equal((UIntPtr)4, capturedLength, "TTF.CreateText must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CreateText must call native hook once.");
        }

        ResetCaptureState();
        nextUInt = 0xB004;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextPropertiesNativeFunction", nameof(CaptureTextReturnUInt)))
        {
            uint actual = SDL3.TTF.GetTextProperties((IntPtr)0xB005);

            TestAssert.Equal(0xB004u, actual, "TTF.GetTextProperties must return native properties.");
            TestAssert.Equal((IntPtr)0xB005, capturedTextHandle, "TTF.GetTextProperties must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextProperties must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextEngineNativeFunction", nameof(CaptureTextEngineReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextEngine((IntPtr)0xB006, (IntPtr)0xB007);

            TestAssert.Equal(true, actual, "TTF.SetTextEngine must return native success value.");
            TestAssert.Equal((IntPtr)0xB006, capturedTextHandle, "TTF.SetTextEngine must forward text.");
            TestAssert.Equal((IntPtr)0xB007, capturedEngine, "TTF.SetTextEngine must forward engine.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xB008;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextEngineNativeFunction", nameof(CaptureTextReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.GetTextEngine((IntPtr)0xB009);

            TestAssert.Equal((IntPtr)0xB008, actual, "TTF.GetTextEngine must return native engine pointer.");
            TestAssert.Equal((IntPtr)0xB009, capturedTextHandle, "TTF.GetTextEngine must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextEngine must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextFontNativeFunction", nameof(CaptureTextFontReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextFont((IntPtr)0xB010, (IntPtr)0xB011);

            TestAssert.Equal(false, actual, "TTF.SetTextFont must return native success value.");
            TestAssert.Equal((IntPtr)0xB010, capturedTextHandle, "TTF.SetTextFont must forward text.");
            TestAssert.Equal((IntPtr)0xB011, capturedFont, "TTF.SetTextFont must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextFont must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xB012;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextFontNativeFunction", nameof(CaptureTextReturnPointer)))
        {
            IntPtr actual = SDL3.TTF.GetTextFont((IntPtr)0xB013);

            TestAssert.Equal((IntPtr)0xB012, actual, "TTF.GetTextFont must return native font pointer.");
            TestAssert.Equal((IntPtr)0xB013, capturedTextHandle, "TTF.GetTextFont must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextFont must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextDirectionNativeFunction", nameof(CaptureTextDirectionReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextDirection((IntPtr)0xB014, SDL3.TTF.Direction.BTT);

            TestAssert.Equal(true, actual, "TTF.SetTextDirection must return native success value.");
            TestAssert.Equal((IntPtr)0xB014, capturedTextHandle, "TTF.SetTextDirection must forward text.");
            TestAssert.Equal(SDL3.TTF.Direction.BTT, capturedDirection, "TTF.SetTextDirection must forward direction.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextDirection must call native hook once.");
        }

        ResetCaptureState();
        nextDirection = SDL3.TTF.Direction.TTB;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextDirectionNativeFunction", nameof(CaptureTextReturnDirection)))
        {
            SDL3.TTF.Direction actual = SDL3.TTF.GetTextDirection((IntPtr)0xB015);

            TestAssert.Equal(SDL3.TTF.Direction.TTB, actual, "TTF.GetTextDirection must return native direction.");
            TestAssert.Equal((IntPtr)0xB015, capturedTextHandle, "TTF.GetTextDirection must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextDirection must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextScriptNativeFunction", nameof(CaptureTextUIntReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextScript((IntPtr)0xB016, 0x4C61746Eu);

            TestAssert.Equal(true, actual, "TTF.SetTextScript must return native success value.");
            TestAssert.Equal((IntPtr)0xB016, capturedTextHandle, "TTF.SetTextScript must forward text.");
            TestAssert.Equal(0x4C61746Eu, capturedUIntValue, "TTF.SetTextScript must forward script.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextScript must call native hook once.");
        }

        ResetCaptureState();
        nextUInt = 0x4379726Cu;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextScriptNativeFunction", nameof(CaptureTextReturnUInt)))
        {
            uint actual = SDL3.TTF.GetTextScript((IntPtr)0xB017);

            TestAssert.Equal(0x4379726Cu, actual, "TTF.GetTextScript must return native script.");
            TestAssert.Equal((IntPtr)0xB017, capturedTextHandle, "TTF.GetTextScript must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextScript must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextColorNativeFunction", nameof(CaptureSetTextColor)))
        {
            bool actual = SDL3.TTF.SetTextColor((IntPtr)0xB018, 10, 20, 30, 40);

            TestAssert.Equal(true, actual, "TTF.SetTextColor must return native success value.");
            TestAssert.Equal((IntPtr)0xB018, capturedTextHandle, "TTF.SetTextColor must forward text.");
            TestAssert.Equal((byte)10, capturedByteR, "TTF.SetTextColor must forward red.");
            TestAssert.Equal((byte)20, capturedByteG, "TTF.SetTextColor must forward green.");
            TestAssert.Equal((byte)30, capturedByteB, "TTF.SetTextColor must forward blue.");
            TestAssert.Equal((byte)40, capturedByteA, "TTF.SetTextColor must forward alpha.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextColor must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextColorFloatNativeFunction", nameof(CaptureSetTextColorFloat)))
        {
            bool actual = SDL3.TTF.SetTextColorFloat((IntPtr)0xB019, 0.1f, 0.2f, 0.3f, 0.4f);

            TestAssert.Equal(false, actual, "TTF.SetTextColorFloat must return native success value.");
            TestAssert.Equal((IntPtr)0xB019, capturedTextHandle, "TTF.SetTextColorFloat must forward text.");
            TestAssert.Equal(0.1f, capturedFloatR, "TTF.SetTextColorFloat must forward red.");
            TestAssert.Equal(0.2f, capturedFloatG, "TTF.SetTextColorFloat must forward green.");
            TestAssert.Equal(0.3f, capturedFloatB, "TTF.SetTextColorFloat must forward blue.");
            TestAssert.Equal(0.4f, capturedFloatA, "TTF.SetTextColorFloat must forward alpha.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextColorFloat must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextByteR = 11;
        nextByteG = 22;
        nextByteB = 33;
        nextByteA = 44;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextColorNativeFunction", nameof(CaptureGetTextColor)))
        {
            bool actual = SDL3.TTF.GetTextColor((IntPtr)0xB020, out byte r, out byte g, out byte b, out byte a);

            TestAssert.Equal(true, actual, "TTF.GetTextColor must return native success value.");
            TestAssert.Equal((byte)11, r, "TTF.GetTextColor must return red.");
            TestAssert.Equal((byte)22, g, "TTF.GetTextColor must return green.");
            TestAssert.Equal((byte)33, b, "TTF.GetTextColor must return blue.");
            TestAssert.Equal((byte)44, a, "TTF.GetTextColor must return alpha.");
            TestAssert.Equal((IntPtr)0xB020, capturedTextHandle, "TTF.GetTextColor must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextColor must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextFloatR = 0.5f;
        nextFloatG = 0.6f;
        nextFloatB = 0.7f;
        nextFloatA = 0.8f;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextColorFloatNativeFunction", nameof(CaptureGetTextColorFloat)))
        {
            bool actual = SDL3.TTF.GetTextColorFloat((IntPtr)0xB021, out float r, out float g, out float b, out float a);

            TestAssert.Equal(true, actual, "TTF.GetTextColorFloat must return native success value.");
            TestAssert.Equal(0.5f, r, "TTF.GetTextColorFloat must return red.");
            TestAssert.Equal(0.6f, g, "TTF.GetTextColorFloat must return green.");
            TestAssert.Equal(0.7f, b, "TTF.GetTextColorFloat must return blue.");
            TestAssert.Equal(0.8f, a, "TTF.GetTextColorFloat must return alpha.");
            TestAssert.Equal((IntPtr)0xB021, capturedTextHandle, "TTF.GetTextColorFloat must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextColorFloat must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextPositionNativeFunction", nameof(CaptureSetTextPosition)))
        {
            bool actual = SDL3.TTF.SetTextPosition((IntPtr)0xB022, -5, 15);

            TestAssert.Equal(true, actual, "TTF.SetTextPosition must return native success value.");
            TestAssert.Equal((IntPtr)0xB022, capturedTextHandle, "TTF.SetTextPosition must forward text.");
            TestAssert.Equal(-5, capturedX, "TTF.SetTextPosition must forward x.");
            TestAssert.Equal(15, capturedY, "TTF.SetTextPosition must forward y.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextPosition must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 21;
        nextHeight = 34;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextPositionNativeFunction", nameof(CaptureGetTextPosition)))
        {
            bool actual = SDL3.TTF.GetTextPosition((IntPtr)0xB023, out int x, out int y);

            TestAssert.Equal(true, actual, "TTF.GetTextPosition must return native success value.");
            TestAssert.Equal(21, x, "TTF.GetTextPosition must return x.");
            TestAssert.Equal(34, y, "TTF.GetTextPosition must return y.");
            TestAssert.Equal((IntPtr)0xB023, capturedTextHandle, "TTF.GetTextPosition must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextPosition must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextWrapWidthNativeFunction", nameof(CaptureTextWrapWidthReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextWrapWidth((IntPtr)0xB024, 320);

            TestAssert.Equal(false, actual, "TTF.SetTextWrapWidth must return native success value.");
            TestAssert.Equal((IntPtr)0xB024, capturedTextHandle, "TTF.SetTextWrapWidth must forward text.");
            TestAssert.Equal(320, capturedWrapWidth, "TTF.SetTextWrapWidth must forward wrap width.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextWrapWidth must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 480;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextWrapWidthNativeFunction", nameof(CaptureGetTextWrapWidth)))
        {
            bool actual = SDL3.TTF.GetTextWrapWidth((IntPtr)0xB025, out int wrapWidth);

            TestAssert.Equal(true, actual, "TTF.GetTextWrapWidth must return native success value.");
            TestAssert.Equal(480, wrapWidth, "TTF.GetTextWrapWidth must return wrap width.");
            TestAssert.Equal((IntPtr)0xB025, capturedTextHandle, "TTF.GetTextWrapWidth must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextWrapWidth must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextWrapWhitespaceVisibleNativeFunction", nameof(CaptureTextEnabledReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextWrapWhitespaceVisible((IntPtr)0xB026, true);

            TestAssert.Equal(true, actual, "TTF.SetTextWrapWhitespaceVisible must return native success value.");
            TestAssert.Equal((IntPtr)0xB026, capturedTextHandle, "TTF.SetTextWrapWhitespaceVisible must forward text.");
            TestAssert.Equal(true, capturedEnabled, "TTF.SetTextWrapWhitespaceVisible must forward visible flag.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextWrapWhitespaceVisible must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("TextWrapWhitespaceVisibleNativeFunction", nameof(CaptureTextReturnBool)))
        {
            bool actual = SDL3.TTF.TextWrapWhitespaceVisible((IntPtr)0xB027);

            TestAssert.Equal(false, actual, "TTF.TextWrapWhitespaceVisible must return native visible flag.");
            TestAssert.Equal((IntPtr)0xB027, capturedTextHandle, "TTF.TextWrapWhitespaceVisible must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.TextWrapWhitespaceVisible must call native hook once.");
        }
    }

    public static void TextContentAndSubstringFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextStringNativeFunction", nameof(CaptureTextStringReturnBool)))
        {
            bool actual = SDL3.TTF.SetTextString((IntPtr)0xC001, "Set", (UIntPtr)3);

            TestAssert.Equal(true, actual, "TTF.SetTextString must return native success value.");
            TestAssert.Equal((IntPtr)0xC001, capturedTextHandle, "TTF.SetTextString must forward text.");
            TestAssert.Equal("Set", capturedString, "TTF.SetTextString must forward string.");
            TestAssert.Equal((UIntPtr)3, capturedLength, "TTF.SetTextString must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.SetTextString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("InsertTextStringNativeFunction", nameof(CaptureInsertTextStringReturnBool)))
        {
            bool actual = SDL3.TTF.InsertTextString((IntPtr)0xC002, 4, "Insert", (UIntPtr)6);

            TestAssert.Equal(false, actual, "TTF.InsertTextString must return native success value.");
            TestAssert.Equal((IntPtr)0xC002, capturedTextHandle, "TTF.InsertTextString must forward text.");
            TestAssert.Equal(4, capturedOffset, "TTF.InsertTextString must forward offset.");
            TestAssert.Equal("Insert", capturedString, "TTF.InsertTextString must forward string.");
            TestAssert.Equal((UIntPtr)6, capturedLength, "TTF.InsertTextString must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.InsertTextString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("AppendTextStringNativeFunction", nameof(CaptureTextStringReturnBool)))
        {
            bool actual = SDL3.TTF.AppendTextString((IntPtr)0xC003, "Append", (UIntPtr)6);

            TestAssert.Equal(true, actual, "TTF.AppendTextString must return native success value.");
            TestAssert.Equal((IntPtr)0xC003, capturedTextHandle, "TTF.AppendTextString must forward text.");
            TestAssert.Equal("Append", capturedString, "TTF.AppendTextString must forward string.");
            TestAssert.Equal((UIntPtr)6, capturedLength, "TTF.AppendTextString must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.AppendTextString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("DeleteTextStringNativeFunction", nameof(CaptureDeleteTextStringReturnBool)))
        {
            bool actual = SDL3.TTF.DeleteTextString((IntPtr)0xC004, 2, 5);

            TestAssert.Equal(true, actual, "TTF.DeleteTextString must return native success value.");
            TestAssert.Equal((IntPtr)0xC004, capturedTextHandle, "TTF.DeleteTextString must forward text.");
            TestAssert.Equal(2, capturedOffset, "TTF.DeleteTextString must forward offset.");
            TestAssert.Equal(5, capturedTextLength, "TTF.DeleteTextString must forward length.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DeleteTextString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 128;
        nextHeight = 32;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextSizeNativeFunction", nameof(CaptureTextSize)))
        {
            bool actual = SDL3.TTF.GetTextSize((IntPtr)0xC005, out int w, out int h);

            TestAssert.Equal(true, actual, "TTF.GetTextSize must return native success value.");
            TestAssert.Equal(128, w, "TTF.GetTextSize must return native width.");
            TestAssert.Equal(32, h, "TTF.GetTextSize must return native height.");
            TestAssert.Equal((IntPtr)0xC005, capturedTextHandle, "TTF.GetTextSize must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextSize must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        nextSubString = SubString(7, 3, 1, SDL3.TTF.SubStringFlags.LineStart);
        using (NativeHookScope _ = NativeHookScope.Install("GetTextSubStringNativeFunction", nameof(CaptureTextOffsetSubString)))
        {
            bool actual = SDL3.TTF.GetTextSubString((IntPtr)0xC006, 7, out SDL3.TTF.SubString substring);

            TestAssert.Equal(true, actual, "TTF.GetTextSubString must return native success value.");
            AssertSubString(nextSubString, substring, "TTF.GetTextSubString must return native substring.");
            TestAssert.Equal((IntPtr)0xC006, capturedTextHandle, "TTF.GetTextSubString must forward text.");
            TestAssert.Equal(7, capturedOffset, "TTF.GetTextSubString must forward offset.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextSubString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        nextSubString = SubString(12, 4, 2, SDL3.TTF.SubStringFlags.LineEnd);
        using (NativeHookScope _ = NativeHookScope.Install("GetTextSubStringForLineNativeFunction", nameof(CaptureTextLineSubString)))
        {
            bool actual = SDL3.TTF.GetTextSubStringForLine((IntPtr)0xC007, 2, out SDL3.TTF.SubString substring);

            TestAssert.Equal(false, actual, "TTF.GetTextSubStringForLine must return native success value.");
            AssertSubString(nextSubString, substring, "TTF.GetTextSubStringForLine must return native substring.");
            TestAssert.Equal((IntPtr)0xC007, capturedTextHandle, "TTF.GetTextSubStringForLine must forward text.");
            TestAssert.Equal(2, capturedLine, "TTF.GetTextSubStringForLine must forward line.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextSubStringForLine must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = IntPtr.Zero;
        nextCount = 0;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextSubStringsForRangeNativeFunction", nameof(CaptureTextSubStringsForRange)))
        {
            SDL3.TTF.SubString[]? substrings = SDL3.TTF.GetTextSubStringsForRange((IntPtr)0xC008, 3, 4, out int count);

            TestAssert.True(substrings is null, "TTF.GetTextSubStringsForRange must return null when native returns null.");
            TestAssert.Equal(0, count, "TTF.GetTextSubStringsForRange null path must return native count.");
            TestAssert.Equal((IntPtr)0xC008, capturedTextHandle, "TTF.GetTextSubStringsForRange null path must forward text.");
            TestAssert.Equal(3, capturedOffset, "TTF.GetTextSubStringsForRange null path must forward offset.");
            TestAssert.Equal(4, capturedTextLength, "TTF.GetTextSubStringsForRange null path must forward length.");
            TestAssert.Equal(IntPtr.Zero, capturedSdlFreeMemory, "TTF.GetTextSubStringsForRange null path must not call SDL.Free.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextSubStringsForRange null path must call native hook once.");
        }

        SDL3.TTF.SubString first = SubString(1, 2, 0, SDL3.TTF.SubStringFlags.TextStart);
        SDL3.TTF.SubString second = SubString(3, 5, 1, SDL3.TTF.SubStringFlags.TextEnd);
        IntPtr firstPointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.TTF.SubString>());
        IntPtr secondPointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.TTF.SubString>());
        IntPtr arrayPointer = Marshal.AllocHGlobal(IntPtr.Size * 2);

        try
        {
            Marshal.StructureToPtr(first, firstPointer, false);
            Marshal.StructureToPtr(second, secondPointer, false);
            Marshal.WriteIntPtr(arrayPointer, 0, firstPointer);
            Marshal.WriteIntPtr(arrayPointer, IntPtr.Size, secondPointer);

            ResetCaptureState();
            nextPointer = arrayPointer;
            nextCount = 2;
            using (NativeHookScope ttfHook = NativeHookScope.Install("GetTextSubStringsForRangeNativeFunction", nameof(CaptureTextSubStringsForRange)))
            using (NativeHookScope freeHook = NativeHookScope.Install(typeof(SDL3.SDL), "FreeNativeFunction", nameof(CaptureSdlFree)))
            {
                SDL3.TTF.SubString[]? substrings = SDL3.TTF.GetTextSubStringsForRange((IntPtr)0xC009, 5, -1, out int count);

                TestAssert.NotNull(substrings, "TTF.GetTextSubStringsForRange must return substring array when native returns pointers.");
                TestAssert.Equal(2, count, "TTF.GetTextSubStringsForRange must return native count.");
                TestAssert.Equal(2, substrings!.Length, "TTF.GetTextSubStringsForRange must return one managed item per native pointer.");
                AssertSubString(first, substrings[0], "TTF.GetTextSubStringsForRange must marshal first substring.");
                AssertSubString(second, substrings[1], "TTF.GetTextSubStringsForRange must marshal second substring.");
                TestAssert.Equal((IntPtr)0xC009, capturedTextHandle, "TTF.GetTextSubStringsForRange must forward text.");
                TestAssert.Equal(5, capturedOffset, "TTF.GetTextSubStringsForRange must forward offset.");
                TestAssert.Equal(-1, capturedTextLength, "TTF.GetTextSubStringsForRange must forward length.");
                TestAssert.Equal(arrayPointer, capturedSdlFreeMemory, "TTF.GetTextSubStringsForRange must free native pointer array.");
                TestAssert.Equal(1, capturedCallCount, "TTF.GetTextSubStringsForRange must call native hook once.");
            }
        }
        finally
        {
            Marshal.FreeHGlobal(arrayPointer);
            Marshal.FreeHGlobal(secondPointer);
            Marshal.FreeHGlobal(firstPointer);
        }

        ResetCaptureState();
        nextBool = true;
        nextSubString = SubString(9, 1, 3, SDL3.TTF.SubStringFlags.DirectionMask);
        using (NativeHookScope _ = NativeHookScope.Install("GetTextSubStringForPointNativeFunction", nameof(CaptureTextPointSubString)))
        {
            bool actual = SDL3.TTF.GetTextSubStringForPoint((IntPtr)0xC010, 42, -6, out SDL3.TTF.SubString substring);

            TestAssert.Equal(true, actual, "TTF.GetTextSubStringForPoint must return native success value.");
            AssertSubString(nextSubString, substring, "TTF.GetTextSubStringForPoint must return native substring.");
            TestAssert.Equal((IntPtr)0xC010, capturedTextHandle, "TTF.GetTextSubStringForPoint must forward text.");
            TestAssert.Equal(42, capturedX, "TTF.GetTextSubStringForPoint must forward x.");
            TestAssert.Equal(-6, capturedY, "TTF.GetTextSubStringForPoint must forward y.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetTextSubStringForPoint must call native hook once.");
        }

        SDL3.TTF.SubString input = SubString(18, 2, 4, SDL3.TTF.SubStringFlags.LineStart);

        ResetCaptureState();
        nextBool = true;
        nextSubString = SubString(16, 2, 4, SDL3.TTF.SubStringFlags.TextStart);
        using (NativeHookScope _ = NativeHookScope.Install("GetPreviousTextSubStringNativeFunction", nameof(CapturePreviousTextSubString)))
        {
            bool actual = SDL3.TTF.GetPreviousTextSubString((IntPtr)0xC011, in input, out SDL3.TTF.SubString previous);

            TestAssert.Equal(true, actual, "TTF.GetPreviousTextSubString must return native success value.");
            AssertSubString(input, capturedSubString, "TTF.GetPreviousTextSubString must forward input substring.");
            AssertSubString(nextSubString, previous, "TTF.GetPreviousTextSubString must return native previous substring.");
            TestAssert.Equal((IntPtr)0xC011, capturedTextHandle, "TTF.GetPreviousTextSubString must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetPreviousTextSubString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        nextSubString = SubString(20, 2, 4, SDL3.TTF.SubStringFlags.TextEnd);
        using (NativeHookScope _ = NativeHookScope.Install("GetNextTextSubStringNativeFunction", nameof(CaptureNextTextSubString)))
        {
            bool actual = SDL3.TTF.GetNextTextSubString((IntPtr)0xC012, in input, out SDL3.TTF.SubString next);

            TestAssert.Equal(false, actual, "TTF.GetNextTextSubString must return native success value.");
            AssertSubString(input, capturedSubString, "TTF.GetNextTextSubString must forward input substring.");
            AssertSubString(nextSubString, next, "TTF.GetNextTextSubString must return native next substring.");
            TestAssert.Equal((IntPtr)0xC012, capturedTextHandle, "TTF.GetNextTextSubString must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.GetNextTextSubString must call native hook once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTextNativeFunction", nameof(CaptureTextReturnBool)))
        {
            bool actual = SDL3.TTF.UpdateText((IntPtr)0xC013);

            TestAssert.Equal(true, actual, "TTF.UpdateText must return native success value.");
            TestAssert.Equal((IntPtr)0xC013, capturedTextHandle, "TTF.UpdateText must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.UpdateText must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("DestroyTextNativeFunction", nameof(CaptureTextVoid)))
        {
            SDL3.TTF.DestroyText((IntPtr)0xC014);

            TestAssert.Equal((IntPtr)0xC014, capturedTextHandle, "TTF.DestroyText must forward text.");
            TestAssert.Equal(1, capturedCallCount, "TTF.DestroyText must call native hook once.");
        }
    }

    public static void ShutdownFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("CloseFontNativeFunction", nameof(CaptureFontVoid)))
        {
            SDL3.TTF.CloseFont((IntPtr)0xD001);

            TestAssert.Equal((IntPtr)0xD001, capturedFont, "TTF.CloseFont must forward font.");
            TestAssert.Equal(1, capturedCallCount, "TTF.CloseFont must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("QuitNativeFunction", nameof(CaptureNoArgsVoid)))
        {
            SDL3.TTF.Quit();

            TestAssert.Equal(1, capturedCallCount, "TTF.Quit must call native hook once.");
        }

        ResetCaptureState();
        nextInt = 3;
        using (NativeHookScope _ = NativeHookScope.Install("WasInitNativeFunction", nameof(ReturnNextInt)))
        {
            int actual = SDL3.TTF.WasInit();

            TestAssert.Equal(3, actual, "TTF.WasInit must return native initialization count.");
            TestAssert.Equal(1, capturedCallCount, "TTF.WasInit must call native hook once.");
        }
    }

    private static int ReturnNextInt()
    {
        capturedCallCount++;
        return nextInt;
    }

    private static bool ReturnNextBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr ReturnNextPointer()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureNoArgsVoid()
    {
        capturedCallCount++;
    }

    private static void CaptureVersionTriplet(out int major, out int minor, out int patch)
    {
        major = nextMajor;
        minor = nextMinor;
        patch = nextPatch;
        capturedCallCount++;
    }

    private static IntPtr CaptureOpenFont(string file, float ptsize)
    {
        capturedFile = file;
        capturedPointSize = ptsize;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureOpenFontIO(IntPtr src, bool closeio, float ptsize)
    {
        capturedSrc = src;
        capturedCloseIO = closeio;
        capturedPointSize = ptsize;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureUIntReturnPointer(uint props)
    {
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureFontReturnPointer(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextPointer;
    }

    private static uint CaptureFontReturnUInt(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextUInt;
    }

    private static bool CaptureFontFallbackReturnBool(IntPtr font, IntPtr fallback)
    {
        capturedFont = font;
        capturedFallback = fallback;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureFontFallbackVoid(IntPtr font, IntPtr fallback)
    {
        capturedFont = font;
        capturedFallback = fallback;
        capturedCallCount++;
    }

    private static void CaptureFontVoid(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
    }

    private static bool CaptureFontPointSizeReturnBool(IntPtr font, float ptsize)
    {
        capturedFont = font;
        capturedPointSize = ptsize;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureFontStyleVoid(IntPtr font, SDL3.TTF.FontStyleFlags style)
    {
        capturedFont = font;
        capturedFontStyle = style;
        capturedCallCount++;
    }

    private static SDL3.TTF.FontStyleFlags CaptureFontReturnStyle(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextFontStyle;
    }

    private static bool CaptureFontIntReturnBool(IntPtr font, int value)
    {
        capturedFont = font;
        capturedIntValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static int CaptureFontReturnInt(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextInt;
    }

    private static void CaptureFontHintingVoid(IntPtr font, SDL3.TTF.HintingFlags hinting)
    {
        capturedFont = font;
        capturedHinting = hinting;
        capturedCallCount++;
    }

    private static SDL3.TTF.HintingFlags CaptureFontReturnHinting(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextHinting;
    }

    private static bool CaptureFontEnabledReturnBool(IntPtr font, bool enabled)
    {
        capturedFont = font;
        capturedEnabled = enabled;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureFontReturnBool(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextBool;
    }

    private static int CaptureFontInReturnInt(in IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextInt;
    }

    private static void CaptureFontHorizontalAlignmentVoid(IntPtr font, SDL3.TTF.HorizontalAlignment align)
    {
        capturedFont = font;
        capturedHorizontalAlignment = align;
        capturedCallCount++;
    }

    private static SDL3.TTF.HorizontalAlignment CaptureFontReturnHorizontalAlignment(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextHorizontalAlignment;
    }

    private static void CaptureFontIntVoid(IntPtr font, int value)
    {
        capturedFont = font;
        capturedIntValue = value;
        capturedCallCount++;
    }

    private static void CaptureFontEnabledVoid(IntPtr font, bool enabled)
    {
        capturedFont = font;
        capturedEnabled = enabled;
        capturedCallCount++;
    }

    private static bool CaptureFontDirectionReturnBool(IntPtr font, SDL3.TTF.Direction direction)
    {
        capturedFont = font;
        capturedDirection = direction;
        capturedCallCount++;
        return nextBool;
    }

    private static SDL3.TTF.Direction CaptureFontReturnDirection(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextDirection;
    }

    private static uint CaptureStringReturnUInt(string value)
    {
        capturedString = value;
        capturedCallCount++;
        return nextUInt;
    }

    private static void CaptureTagToString(uint tag, out string value, UIntPtr size)
    {
        capturedUIntValue = tag;
        capturedSize = size;
        value = nextString;
        capturedCallCount++;
    }

    private static bool CaptureFontUIntReturnBool(IntPtr font, uint value)
    {
        capturedFont = font;
        capturedUIntValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static uint CaptureUIntReturnUInt(uint value)
    {
        capturedUIntValue = value;
        capturedCallCount++;
        return nextUInt;
    }

    private static bool CaptureFontStringReturnBool(IntPtr font, string? value)
    {
        capturedFont = font;
        capturedString = value;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureGlyphImagePointer(IntPtr font, uint value, IntPtr imageType)
    {
        capturedFont = font;
        capturedUIntValue = value;
        capturedImageTypePointer = imageType;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGlyphImageOut(IntPtr font, uint value, out SDL3.TTF.ImageType imageType)
    {
        capturedFont = font;
        capturedUIntValue = value;
        imageType = nextImageType;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureGlyphMetrics(IntPtr font, uint ch, out int minx, out int maxx, out int miny, out int maxy, out int advance)
    {
        capturedFont = font;
        capturedUIntValue = ch;
        minx = nextMinX;
        maxx = nextMaxX;
        miny = nextMinY;
        maxy = nextMaxY;
        advance = nextAdvance;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGlyphKerning(IntPtr font, uint previousCh, uint ch, out int kerning)
    {
        capturedFont = font;
        capturedPreviousUIntValue = previousCh;
        capturedUIntValue = ch;
        kerning = nextKerning;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureStringSize(IntPtr font, string text, UIntPtr length, out int w, out int h)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        w = nextWidth;
        h = nextHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static unsafe bool CaptureStringSizeBytePointer(IntPtr font, byte* text, UIntPtr length, out int w, out int h)
    {
        capturedFont = font;
        capturedTextPointer = (IntPtr)text;
        capturedLength = length;
        w = nextWidth;
        h = nextHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureStringSizePointer(IntPtr font, IntPtr text, UIntPtr length, out int w, out int h)
    {
        capturedFont = font;
        capturedTextPointer = text;
        capturedLength = length;
        w = nextWidth;
        h = nextHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureStringSizeWrapped(IntPtr font, string text, UIntPtr length, int wrapWidth, out int w, out int h)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedMaxWidth = wrapWidth;
        w = nextWidth;
        h = nextHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureMeasureString(IntPtr font, string text, UIntPtr length, int maxWidth, out int measuredWidth, out ulong measuredLength)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedMaxWidth = maxWidth;
        measuredWidth = nextMeasuredWidth;
        measuredLength = nextMeasuredLength;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureRenderTextString(IntPtr font, string text, UIntPtr length, SDL3.SDL.Color fg)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedForeground = fg;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderTextPointer(IntPtr font, IntPtr text, UIntPtr length, SDL3.SDL.Color fg)
    {
        capturedFont = font;
        capturedTextPointer = text;
        capturedLength = length;
        capturedForeground = fg;
        capturedCallCount++;
        return nextPointer;
    }

    private static unsafe IntPtr CaptureRenderTextBytePointer(IntPtr font, byte* text, UIntPtr length, SDL3.SDL.Color fg)
    {
        capturedFont = font;
        capturedTextPointer = (IntPtr)text;
        capturedLength = length;
        capturedForeground = fg;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderTextWrapped(IntPtr font, string text, UIntPtr length, SDL3.SDL.Color fg, int wrapWidth)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedForeground = fg;
        capturedWrapWidth = wrapWidth;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderGlyphUInt(IntPtr font, uint ch, SDL3.SDL.Color fg)
    {
        capturedFont = font;
        capturedUIntValue = ch;
        capturedForeground = fg;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderTextStringWithBackground(IntPtr font, string text, UIntPtr length, SDL3.SDL.Color fg, SDL3.SDL.Color bg)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedForeground = fg;
        capturedBackground = bg;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderTextWrappedWithBackground(IntPtr font, string text, UIntPtr length, SDL3.SDL.Color fg, SDL3.SDL.Color bg, int wrapWidth)
    {
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedForeground = fg;
        capturedBackground = bg;
        capturedWrapWidth = wrapWidth;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderGlyphUIntWithBackground(IntPtr font, uint ch, SDL3.SDL.Color fg, SDL3.SDL.Color bg)
    {
        capturedFont = font;
        capturedUIntValue = ch;
        capturedForeground = fg;
        capturedBackground = bg;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureRenderGlyphULong(IntPtr font, ulong ch, SDL3.SDL.Color fg)
    {
        capturedFont = font;
        capturedULongValue = ch;
        capturedForeground = fg;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureDrawSurfaceText(IntPtr text, int x, int y, IntPtr surface)
    {
        capturedTextHandle = text;
        capturedX = x;
        capturedY = y;
        capturedSurface = surface;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureEngineVoid(IntPtr engine)
    {
        capturedEngine = engine;
        capturedCallCount++;
    }

    private static IntPtr CaptureRendererReturnPointer(IntPtr renderer)
    {
        capturedRenderer = renderer;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureDrawRendererText(IntPtr text, float x, float y)
    {
        capturedTextHandle = text;
        capturedXFloat = x;
        capturedYFloat = y;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureDeviceReturnPointer(IntPtr device)
    {
        capturedDevice = device;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureTextReturnPointer(IntPtr text)
    {
        capturedTextHandle = text;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureEngineWindingVoid(IntPtr engine, SDL3.TTF.GPUTextEngineWinding winding)
    {
        capturedEngine = engine;
        capturedWinding = winding;
        capturedCallCount++;
    }

    private static SDL3.TTF.GPUTextEngineWinding CaptureEngineReturnWinding(IntPtr engine)
    {
        capturedEngine = engine;
        capturedCallCount++;
        return nextWinding;
    }

    private static IntPtr CaptureCreateText(IntPtr engine, IntPtr font, string text, UIntPtr length)
    {
        capturedEngine = engine;
        capturedFont = font;
        capturedString = text;
        capturedLength = length;
        capturedCallCount++;
        return nextPointer;
    }

    private static uint CaptureTextReturnUInt(IntPtr text)
    {
        capturedTextHandle = text;
        capturedCallCount++;
        return nextUInt;
    }

    private static bool CaptureTextEngineReturnBool(IntPtr text, IntPtr engine)
    {
        capturedTextHandle = text;
        capturedEngine = engine;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextFontReturnBool(IntPtr text, IntPtr font)
    {
        capturedTextHandle = text;
        capturedFont = font;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextDirectionReturnBool(IntPtr text, SDL3.TTF.Direction direction)
    {
        capturedTextHandle = text;
        capturedDirection = direction;
        capturedCallCount++;
        return nextBool;
    }

    private static SDL3.TTF.Direction CaptureTextReturnDirection(IntPtr text)
    {
        capturedTextHandle = text;
        capturedCallCount++;
        return nextDirection;
    }

    private static bool CaptureTextUIntReturnBool(IntPtr text, uint value)
    {
        capturedTextHandle = text;
        capturedUIntValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetTextColor(IntPtr text, byte r, byte g, byte b, byte a)
    {
        capturedTextHandle = text;
        capturedByteR = r;
        capturedByteG = g;
        capturedByteB = b;
        capturedByteA = a;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetTextColorFloat(IntPtr text, float r, float g, float b, float a)
    {
        capturedTextHandle = text;
        capturedFloatR = r;
        capturedFloatG = g;
        capturedFloatB = b;
        capturedFloatA = a;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetTextColor(IntPtr text, out byte r, out byte g, out byte b, out byte a)
    {
        capturedTextHandle = text;
        r = nextByteR;
        g = nextByteG;
        b = nextByteB;
        a = nextByteA;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetTextColorFloat(IntPtr text, out float r, out float g, out float b, out float a)
    {
        capturedTextHandle = text;
        r = nextFloatR;
        g = nextFloatG;
        b = nextFloatB;
        a = nextFloatA;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetTextPosition(IntPtr text, int x, int y)
    {
        capturedTextHandle = text;
        capturedX = x;
        capturedY = y;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetTextPosition(IntPtr text, out int x, out int y)
    {
        capturedTextHandle = text;
        x = nextWidth;
        y = nextHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextWrapWidthReturnBool(IntPtr text, int wrapWidth)
    {
        capturedTextHandle = text;
        capturedWrapWidth = wrapWidth;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetTextWrapWidth(IntPtr text, out int wrapWidth)
    {
        capturedTextHandle = text;
        wrapWidth = nextWidth;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextEnabledReturnBool(IntPtr text, bool enabled)
    {
        capturedTextHandle = text;
        capturedEnabled = enabled;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextReturnBool(IntPtr text)
    {
        capturedTextHandle = text;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextStringReturnBool(IntPtr text, string value, UIntPtr length)
    {
        capturedTextHandle = text;
        capturedString = value;
        capturedLength = length;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureInsertTextStringReturnBool(IntPtr text, int offset, string value, UIntPtr length)
    {
        capturedTextHandle = text;
        capturedOffset = offset;
        capturedString = value;
        capturedLength = length;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureDeleteTextStringReturnBool(IntPtr text, int offset, int length)
    {
        capturedTextHandle = text;
        capturedOffset = offset;
        capturedTextLength = length;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextSize(IntPtr text, out int w, out int h)
    {
        capturedTextHandle = text;
        w = nextWidth;
        h = nextHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextOffsetSubString(IntPtr text, int offset, out SDL3.TTF.SubString substring)
    {
        capturedTextHandle = text;
        capturedOffset = offset;
        substring = nextSubString;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTextLineSubString(IntPtr text, int line, out SDL3.TTF.SubString substring)
    {
        capturedTextHandle = text;
        capturedLine = line;
        substring = nextSubString;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureTextSubStringsForRange(IntPtr text, int offset, int length, out int count)
    {
        capturedTextHandle = text;
        capturedOffset = offset;
        capturedTextLength = length;
        count = nextCount;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureTextPointSubString(IntPtr text, int x, int y, out SDL3.TTF.SubString substring)
    {
        capturedTextHandle = text;
        capturedX = x;
        capturedY = y;
        substring = nextSubString;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CapturePreviousTextSubString(IntPtr text, in SDL3.TTF.SubString substring, out SDL3.TTF.SubString previous)
    {
        capturedTextHandle = text;
        capturedSubString = substring;
        previous = nextSubString;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureNextTextSubString(IntPtr text, in SDL3.TTF.SubString substring, out SDL3.TTF.SubString next)
    {
        capturedTextHandle = text;
        capturedSubString = substring;
        next = nextSubString;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureTextVoid(IntPtr text)
    {
        capturedTextHandle = text;
        capturedCallCount++;
    }

    private static void CaptureSdlFree(IntPtr mem)
    {
        capturedSdlFreeMemory = mem;
    }

    private static bool CaptureFontPointSizeDpiReturnBool(IntPtr font, float ptsize, int hdpi, int vdpi)
    {
        capturedFont = font;
        capturedPointSize = ptsize;
        capturedHdpi = hdpi;
        capturedVdpi = vdpi;
        capturedCallCount++;
        return nextBool;
    }

    private static float CaptureFontReturnFloat(IntPtr font)
    {
        capturedFont = font;
        capturedCallCount++;
        return nextFloat;
    }

    private static bool CaptureFontReturnDpi(IntPtr font, out int hdpi, out int vdpi)
    {
        capturedFont = font;
        hdpi = nextHdpi;
        vdpi = nextVdpi;
        capturedCallCount++;
        return nextBool;
    }

    private static void ResetCaptureState()
    {
        nextInt = 0;
        nextUInt = 0;
        nextBool = false;
        nextFloat = 0;
        nextPointer = IntPtr.Zero;
        nextMajor = 0;
        nextMinor = 0;
        nextPatch = 0;
        nextHdpi = 0;
        nextVdpi = 0;
        nextFontStyle = SDL3.TTF.FontStyleFlags.Normal;
        nextHinting = SDL3.TTF.HintingFlags.Invalid;
        nextHorizontalAlignment = SDL3.TTF.HorizontalAlignment.Invalid;
        nextDirection = SDL3.TTF.Direction.Invalid;
        nextImageType = SDL3.TTF.ImageType.Invalid;
        nextWinding = SDL3.TTF.GPUTextEngineWinding.Invalid;
        nextMinX = 0;
        nextMaxX = 0;
        nextMinY = 0;
        nextMaxY = 0;
        nextAdvance = 0;
        nextKerning = 0;
        nextWidth = 0;
        nextHeight = 0;
        nextMeasuredWidth = 0;
        nextMeasuredLength = 0;
        nextByteR = 0;
        nextByteG = 0;
        nextByteB = 0;
        nextByteA = 0;
        nextFloatR = 0;
        nextFloatG = 0;
        nextFloatB = 0;
        nextFloatA = 0;
        nextString = "";
        nextCount = 0;
        nextSubString = default;
        capturedFont = IntPtr.Zero;
        capturedFallback = IntPtr.Zero;
        capturedSrc = IntPtr.Zero;
        capturedImageTypePointer = IntPtr.Zero;
        capturedTextPointer = IntPtr.Zero;
        capturedEngine = IntPtr.Zero;
        capturedTextHandle = IntPtr.Zero;
        capturedRenderer = IntPtr.Zero;
        capturedDevice = IntPtr.Zero;
        capturedSurface = IntPtr.Zero;
        capturedCloseIO = false;
        capturedEnabled = false;
        capturedPointSize = 0;
        capturedProps = 0;
        capturedUIntValue = 0;
        capturedPreviousUIntValue = 0;
        capturedULongValue = 0;
        capturedSize = UIntPtr.Zero;
        capturedLength = UIntPtr.Zero;
        capturedIntValue = 0;
        capturedMaxWidth = 0;
        capturedWrapWidth = 0;
        capturedX = 0;
        capturedY = 0;
        capturedHdpi = 0;
        capturedVdpi = 0;
        capturedXFloat = 0;
        capturedYFloat = 0;
        capturedByteR = 0;
        capturedByteG = 0;
        capturedByteB = 0;
        capturedByteA = 0;
        capturedFloatR = 0;
        capturedFloatG = 0;
        capturedFloatB = 0;
        capturedFloatA = 0;
        capturedFontStyle = SDL3.TTF.FontStyleFlags.Normal;
        capturedHinting = SDL3.TTF.HintingFlags.Invalid;
        capturedHorizontalAlignment = SDL3.TTF.HorizontalAlignment.Invalid;
        capturedDirection = SDL3.TTF.Direction.Invalid;
        capturedWinding = SDL3.TTF.GPUTextEngineWinding.Invalid;
        capturedForeground = default;
        capturedBackground = default;
        capturedCallCount = 0;
        capturedFile = null;
        capturedString = null;
        capturedSubString = default;
        capturedOffset = 0;
        capturedTextLength = 0;
        capturedLine = 0;
        capturedSdlFreeMemory = IntPtr.Zero;
    }

    private static SDL3.SDL.Color Color(byte r, byte g, byte b, byte a)
    {
        return new SDL3.SDL.Color
        {
            R = r,
            G = g,
            B = b,
            A = a
        };
    }

    private static SDL3.TTF.SubString SubString(int offset, int length, int lineIndex, SDL3.TTF.SubStringFlags flags)
    {
        return new SDL3.TTF.SubString
        {
            Flags = flags,
            Offset = offset,
            Length = length,
            LineIndex = lineIndex,
            ClusterIndex = offset + length,
            Rect = new SDL3.SDL.Rect
            {
                X = offset * 2,
                Y = lineIndex * 3,
                W = length * 4,
                H = 5 + lineIndex
            }
        };
    }

    private static void AssertSubString(SDL3.TTF.SubString expected, SDL3.TTF.SubString actual, string message)
    {
        TestAssert.Equal(expected.Flags, actual.Flags, $"{message} Flags mismatch.");
        TestAssert.Equal(expected.Offset, actual.Offset, $"{message} Offset mismatch.");
        TestAssert.Equal(expected.Length, actual.Length, $"{message} Length mismatch.");
        TestAssert.Equal(expected.LineIndex, actual.LineIndex, $"{message} LineIndex mismatch.");
        TestAssert.Equal(expected.ClusterIndex, actual.ClusterIndex, $"{message} ClusterIndex mismatch.");
        TestAssert.Equal(expected.Rect.X, actual.Rect.X, $"{message} Rect.X mismatch.");
        TestAssert.Equal(expected.Rect.Y, actual.Rect.Y, $"{message} Rect.Y mismatch.");
        TestAssert.Equal(expected.Rect.W, actual.Rect.W, $"{message} Rect.W mismatch.");
        TestAssert.Equal(expected.Rect.H, actual.Rect.H, $"{message} Rect.H mismatch.");
    }

    private static void AssertFontIntGetter(string fieldName, string methodName, int fontValue, int nativeValue)
    {
        ResetCaptureState();
        nextInt = nativeValue;
        using (NativeHookScope _ = NativeHookScope.Install(fieldName, nameof(CaptureFontReturnInt)))
        {
            MethodInfo? method = typeof(SDL3.TTF).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
            TestAssert.NotNull(method, $"TTF.{methodName} public method must exist.");
            int actual = (int)method!.Invoke(null, [(IntPtr)fontValue])!;

            TestAssert.Equal(nativeValue, actual, $"TTF.{methodName} must return native value.");
            TestAssert.Equal((IntPtr)fontValue, capturedFont, $"TTF.{methodName} must forward font.");
            TestAssert.Equal(1, capturedCallCount, $"TTF.{methodName} must call native hook once.");
        }
    }

    private static void AssertFontNameWrapper(string fieldName, string methodName, int fontValue, string nativeValue)
    {
        IntPtr utf8 = Marshal.StringToCoTaskMemUTF8(nativeValue);

        try
        {
            ResetCaptureState();
            nextPointer = utf8;
            using (NativeHookScope _ = NativeHookScope.Install(fieldName, nameof(CaptureFontReturnPointer)))
            {
                MethodInfo? method = typeof(SDL3.TTF).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
                TestAssert.NotNull(method, $"TTF.{methodName} public method must exist.");
                string actual = (string)method!.Invoke(null, [(IntPtr)fontValue])!;

                TestAssert.Equal(nativeValue, actual, $"TTF.{methodName} must convert native UTF-8 string.");
                TestAssert.Equal((IntPtr)fontValue, capturedFont, $"TTF.{methodName} must forward font.");
                TestAssert.Equal(1, capturedCallCount, $"TTF.{methodName} must call native hook once.");
            }
        }
        finally
        {
            Marshal.FreeCoTaskMem(utf8);
        }

        ResetCaptureState();
        nextPointer = IntPtr.Zero;
        using (NativeHookScope _ = NativeHookScope.Install(fieldName, nameof(CaptureFontReturnPointer)))
        {
            MethodInfo? method = typeof(SDL3.TTF).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
            TestAssert.NotNull(method, $"TTF.{methodName} public method must exist.");
            string actual = (string)method!.Invoke(null, [(IntPtr)(fontValue + 1)])!;

            TestAssert.Equal("", actual, $"TTF.{methodName} must return empty string for native null.");
            TestAssert.Equal((IntPtr)(fontValue + 1), capturedFont, $"TTF.{methodName} null path must forward font.");
            TestAssert.Equal(1, capturedCallCount, $"TTF.{methodName} null path must call native hook once.");
        }
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.TTF).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"TTF.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertNoNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.TTF).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.True(method is null, $"TTF.{methodName} must not exist for SDL_ttf 3.2.2.");
    }

    private static void AssertNoPublicMethod(string methodName, params Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.TTF).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);
        TestAssert.True(method is null, $"TTF.{methodName} public wrapper must not exist for SDL_ttf 3.2.2.");
    }

    private static void AssertNoPropertyConstant(string fieldName)
    {
        FieldInfo? field = typeof(SDL3.TTF.Props).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        TestAssert.True(field is null, $"TTF.Props.{fieldName} must not exist for SDL_ttf 3.2.2.");
    }

    private static void AssertNativeImport(MethodInfo method, string entryPoint)
    {
        AssertTtfLibraryImport(method, entryPoint);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertNativeBoolImport(MethodInfo method, string entryPoint)
    {
        AssertTtfLibraryImport(method, entryPoint);
        AssertBoolReturnMarshal(method);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertSingleFontIntReturnImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr));
    }

    private static void AssertSingleFontBoolReturnImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeBoolImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr));
    }

    private static void AssertSingleFontPointerReturnImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr));
    }

    private static void AssertRenderTextStringImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(SDL3.SDL.Color));
        AssertParameterMarshal(method, 1, UnmanagedType.LPUTF8Str);
    }

    private static void AssertRenderTextPointerImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(IntPtr), typeof(UIntPtr), typeof(SDL3.SDL.Color));
    }

    private static void AssertRenderTextBytePointerImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(byte).MakePointerType(), typeof(UIntPtr), typeof(SDL3.SDL.Color));
    }

    private static void AssertRenderTextWrappedImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(SDL3.SDL.Color), typeof(int));
        AssertParameterMarshal(method, 1, UnmanagedType.LPUTF8Str);
    }

    private static void AssertRenderGlyphUIntImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(uint), typeof(SDL3.SDL.Color));
    }

    private static void AssertRenderGlyphULongImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(ulong), typeof(SDL3.SDL.Color));
    }

    private static void AssertRenderTextStringWithBackgroundImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(SDL3.SDL.Color), typeof(SDL3.SDL.Color));
        AssertParameterMarshal(method, 1, UnmanagedType.LPUTF8Str);
    }

    private static void AssertRenderTextWrappedWithBackgroundImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(string), typeof(UIntPtr), typeof(SDL3.SDL.Color), typeof(SDL3.SDL.Color), typeof(int));
        AssertParameterMarshal(method, 1, UnmanagedType.LPUTF8Str);
    }

    private static void AssertRenderGlyphUIntWithBackgroundImport(string methodName, string entryPoint)
    {
        MethodInfo method = GetNativeMethod(methodName);
        AssertNativeImport(method, entryPoint);
        AssertParameterTypes(method, typeof(IntPtr), typeof(uint), typeof(SDL3.SDL.Color), typeof(SDL3.SDL.Color));
    }

    private static void AssertTtfLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"TTF.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_ttf", libraryImport!.LibraryName, $"TTF.{method.Name} must import from SDL3_ttf.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"TTF.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"TTF.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"TTF.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertParameterTypes(MethodInfo method, params Type[] expectedTypes)
    {
        ParameterInfo[] parameters = method.GetParameters();
        TestAssert.Equal(expectedTypes.Length, parameters.Length, $"TTF.{method.Name} must keep expected parameter count.");

        for (int i = 0; i < expectedTypes.Length; i++)
        {
            TestAssert.Equal(expectedTypes[i], parameters[i].ParameterType, $"TTF.{method.Name} parameter {i} must keep expected type.");
        }
    }

    private static void AssertOutParameter(MethodInfo method, int index)
    {
        ParameterInfo parameter = method.GetParameters()[index];
        TestAssert.True(parameter.IsOut, $"TTF.{method.Name} parameter {index} must remain an out parameter.");
    }

    private static void AssertParameterMarshal(MethodInfo method, int index, UnmanagedType expected)
    {
        MarshalAsAttribute? marshalAs = method.GetParameters()[index].GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"TTF.{method.Name} parameter {index} must keep MarshalAs metadata.");
        TestAssert.Equal(expected, marshalAs!.Value, $"TTF.{method.Name} parameter {index} must keep expected MarshalAs value.");
    }

    private static void AssertExcludedFromCoverage(MethodInfo method)
    {
        ExcludeFromCodeCoverageAttribute? attribute = method.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
        TestAssert.NotNull(attribute, $"TTF.{method.Name} native stub must be excluded from code coverage.");
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
            return Install(typeof(SDL3.TTF), fieldName, methodName);
        }

        public static NativeHookScope Install(Type ownerType, string fieldName, string methodName)
        {
            FieldInfo? field = ownerType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"{ownerType.Name} private hook field {fieldName} must exist.");

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
