using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Render;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr capturedPointer;
    private static IntPtr capturedRenderer;
    private static IntPtr capturedWindow;
    private static IntPtr capturedDevice;
    private static IntPtr capturedSurface;
    private static IntPtr capturedTexture;
    private static IntPtr capturedPalette;
    private static IntPtr capturedRectPointer;
    private static IntPtr capturedSourceRectPointer;
    private static IntPtr capturedDestinationRectPointer;
    private static IntPtr capturedCenterPointer;
    private static IntPtr capturedOriginPointer;
    private static IntPtr capturedRightPointer;
    private static IntPtr capturedDownPointer;
    private static IntPtr capturedXYPointer;
    private static IntPtr capturedColorPointer;
    private static IntPtr capturedUVPointer;
    private static IntPtr capturedIndicesPointer;
    private static IntPtr capturedPixels;
    private static IntPtr capturedState;
    private static IntPtr capturedCreateInfo;
    private static IntPtr capturedData;
    private static IntPtr capturedYPlane;
    private static IntPtr capturedUPlane;
    private static IntPtr capturedVPlane;
    private static IntPtr capturedUVPlane;
    private static IntPtr nextPixels;
    private static IntPtr nextSurface;
    private static int nextInt;
    private static int capturedIndex;
    private static int capturedWidth;
    private static int capturedHeight;
    private static int nextWidth;
    private static int nextHeight;
    private static int capturedCount;
    private static int capturedXYStride;
    private static int capturedColorStride;
    private static int capturedUVStride;
    private static int capturedNumVertices;
    private static int capturedNumIndices;
    private static int capturedSizeIndices;
    private static int capturedVSync;
    private static int nextVSync;
    private static int capturedPitch;
    private static int capturedYPitch;
    private static int capturedUPitch;
    private static int capturedVPitch;
    private static int capturedUVPitch;
    private static int nextPitch;
    private static uint nextUInt;
    private static uint capturedProps;
    private static uint capturedEventType;
    private static uint capturedWaitStageMask;
    private static uint capturedSlotIndex;
    private static uint capturedLength;
    private static uint nextEventType;
    private static long capturedWaitSemaphore;
    private static long capturedSignalSemaphore;
    private static bool nextBool;
    private static string? capturedTitle;
    private static string? capturedName;
    private static string? capturedText;
    private static byte[]? capturedBytes;
    private static SDL3.SDL.FPoint[]? capturedFPoints;
    private static SDL3.SDL.FRect[]? capturedFRects;
    private static SDL3.SDL.Vertex[]? capturedVertices;
    private static int[]? capturedIntIndices;
    private static float[]? capturedXY;
    private static SDL3.SDL.FColor[]? capturedColors;
    private static float[]? capturedUV;
    private static float[]? capturedColorFloats;
    private static byte[]? capturedByteIndices;
    private static SDL3.SDL.Rect capturedRect;
    private static SDL3.SDL.Rect nextRect;
    private static SDL3.SDL.FRect capturedFRect;
    private static SDL3.SDL.FRect capturedSourceFRect;
    private static SDL3.SDL.FRect capturedDestinationFRect;
    private static SDL3.SDL.FPoint capturedCenterFPoint;
    private static SDL3.SDL.FRect capturedOriginFRect;
    private static SDL3.SDL.FRect capturedRightFRect;
    private static SDL3.SDL.FRect capturedDownFRect;
    private static SDL3.SDL.FRect nextFRect;
    private static SDL3.SDL.WindowFlags capturedWindowFlags;
    private static SDL3.SDL.PixelFormat capturedPixelFormat;
    private static SDL3.SDL.TextureAccess capturedTextureAccess;
    private static SDL3.SDL.RendererLogicalPresentation capturedPresentationMode;
    private static SDL3.SDL.RendererLogicalPresentation nextPresentationMode;
    private static byte capturedR;
    private static byte capturedG;
    private static byte capturedB;
    private static byte capturedAlpha;
    private static byte nextR;
    private static byte nextG;
    private static byte nextB;
    private static byte nextAlpha;
    private static float capturedFR;
    private static float capturedFG;
    private static float capturedFB;
    private static float capturedFAlpha;
    private static float nextFR;
    private static float nextFG;
    private static float nextFB;
    private static float nextFAlpha;
    private static float nextFloatWidth;
    private static float nextFloatHeight;
    private static float capturedX;
    private static float capturedY;
    private static float capturedWindowX;
    private static float capturedWindowY;
    private static float nextX;
    private static float nextY;
    private static float nextWindowX;
    private static float nextWindowY;
    private static float capturedScaleX;
    private static float capturedScaleY;
    private static float capturedScale;
    private static float nextScaleX;
    private static float nextScaleY;
    private static float capturedColorScale;
    private static float nextColorScale;
    private static float capturedX1;
    private static float capturedY1;
    private static float capturedX2;
    private static float capturedY2;
    private static float capturedLeftWidth;
    private static float capturedRightWidth;
    private static float capturedTopHeight;
    private static float capturedBottomHeight;
    private static float capturedTileScale;
    private static double capturedAngle;
    private static SDL3.SDL.BlendMode capturedBlendMode;
    private static SDL3.SDL.BlendMode nextBlendMode;
    private static SDL3.SDL.FlipMode capturedFlipMode;
    private static SDL3.SDL.ScaleMode capturedScaleMode;
    private static SDL3.SDL.ScaleMode nextScaleMode;
    private static SDL3.SDL.TextureAddressMode capturedTextureAddressModeU;
    private static SDL3.SDL.TextureAddressMode capturedTextureAddressModeV;
    private static SDL3.SDL.TextureAddressMode nextTextureAddressModeU;
    private static SDL3.SDL.TextureAddressMode nextTextureAddressModeV;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        RenderDriverFunctions_ForwardAndConvertStrings();
        CreateWindowAndRenderer_ForwardsArgumentsOutputsPointersAndReturnsNativeValue();
        RendererPointerFunctions_ForwardInputsAndReturnNativeValues();
        RenderOutputSizeFunctions_ForwardRendererAndReturnSizes();
        TextureCreationAndPropertyFunctions_ForwardInputsAndReturnNativeValues();
        TexturePaletteAndModulationFunctions_ForwardInputsOutputsAndReturnNativeValues();
        TextureBlendAndScaleFunctions_ForwardInputsOutputsAndReturnNativeValues();
        TextureUpdateFunctions_ForwardPointerArraySpanAndRectOverloads();
        TexturePlaneUpdateFunctions_ForwardYuvAndNvOverloads();
        TextureLockingFunctions_ForwardInputsOutputsAndUnlock();
        RenderTargetLogicalPresentationAndCoordinates_ForwardInputsOutputsAndReturnNativeValues();
        ConvertEventToRenderCoordinates_ForwardsRendererAndRefEvent();
        ViewportClipAndScaleFunctions_ForwardInputsOutputsAndReturnNativeValues();
        DrawColorBlendAndClearFunctions_ForwardInputsOutputsAndReturnNativeValues();
        PrimitiveRenderingFunctions_ForwardCoordinatesPointersArraysAndRects();
        TextureRenderingFunctions_ForwardTextureRectsRotationCenterAndFlip();
        TextureAffineRenderingFunctions_ForwardPointerAndRectCombinations();
        TextureTiledRenderingFunctions_ForwardTextureRectsAndScale();
        Texture9GridRenderingFunctions_ForwardTextureRectsScalesAndTileScale();
        GeometryRenderingFunctions_ForwardVerticesRawArraysAndGenericSpans();
        RenderTextureAddressReadPresentDestroyFlushAndMetalFunctions_ForwardInputsOutputsAndReturnNativeValues();
        VulkanVSyncDebugTextAndDefaultTextureScaleFunctions_ForwardInputsOutputsAndReturnNativeValues();
        GpuRenderStateFunctions_ForwardPointersArraysAndReturnNativeValues();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertNativeImport(GetNativeMethod("SDL_GetNumRenderDrivers"), "SDL_GetNumRenderDrivers");
        AssertNativeImport(GetNativeMethod("SDL_GetRenderDriver"), "SDL_GetRenderDriver");

        MethodInfo createWindowAndRenderer = GetNativeMethod("SDL_CreateWindowAndRenderer");
        AssertNativeBoolImport(createWindowAndRenderer, "SDL_CreateWindowAndRenderer");
        AssertStringParameterMarshal(createWindowAndRenderer, 0);

        MethodInfo createRenderer = GetNativeMethod("SDL_CreateRenderer");
        AssertNativeImport(createRenderer, "SDL_CreateRenderer");
        AssertStringParameterMarshal(createRenderer, 1);

        AssertNativeImport(GetNativeMethod("SDL_CreateRendererWithProperties"), "SDL_CreateRendererWithProperties");
        AssertNativeImport(GetNativeMethod("SDL_CreateGPURenderer"), "SDL_CreateGPURenderer");
        AssertNativeImport(GetNativeMethod("SDL_GetGPURendererDevice"), "SDL_GetGPURendererDevice");
        AssertNativeImport(GetNativeMethod("SDL_CreateSoftwareRenderer"), "SDL_CreateSoftwareRenderer");
        AssertNativeImport(GetNativeMethod("SDL_GetRenderer"), "SDL_GetRenderer");
        AssertNativeImport(GetNativeMethod("SDL_GetRenderWindow"), "SDL_GetRenderWindow");
        AssertNativeImport(GetNativeMethod("SDL_GetRendererName"), "SDL_GetRendererName");
        AssertNativeImport(GetNativeMethod("SDL_GetRendererProperties"), "SDL_GetRendererProperties");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderOutputSize"), "SDL_GetRenderOutputSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetCurrentRenderOutputSize"), "SDL_GetCurrentRenderOutputSize");
        AssertNativeImport(GetNativeMethod("SDL_CreateTexture"), "SDL_CreateTexture");
        AssertNativeImport(GetNativeMethod("SDL_CreateTextureFromSurface"), "SDL_CreateTextureFromSurface");
        AssertNativeImport(GetNativeMethod("SDL_CreateTextureWithProperties"), "SDL_CreateTextureWithProperties");
        AssertNativeImport(GetNativeMethod("SDL_GetTextureProperties"), "SDL_GetTextureProperties");
        AssertNativeImport(GetNativeMethod("SDL_GetRendererFromTexture"), "SDL_GetRendererFromTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureSize"), "SDL_GetTextureSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTexturePalette"), "SDL_SetTexturePalette");
        AssertNativeImport(GetNativeMethod("SDL_GetTexturePalette"), "SDL_GetTexturePalette");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTextureColorMod"), "SDL_SetTextureColorMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTextureColorModFloat"), "SDL_SetTextureColorModFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureColorMod"), "SDL_GetTextureColorMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureColorModFloat"), "SDL_GetTextureColorModFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTextureAlphaMod"), "SDL_SetTextureAlphaMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTextureAlphaModFloat"), "SDL_SetTextureAlphaModFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureAlphaMod"), "SDL_GetTextureAlphaMod");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureAlphaModFloat"), "SDL_GetTextureAlphaModFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTextureBlendMode"), "SDL_SetTextureBlendMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureBlendMode"), "SDL_GetTextureBlendMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetTextureScaleMode"), "SDL_SetTextureScaleMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetTextureScaleMode"), "SDL_GetTextureScaleMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateTexturePointer"), "SDL_UpdateTexture");
        MethodInfo updateTextureArray = GetNativeMethod("SDL_UpdateTextureArray");
        AssertNativeBoolImport(updateTextureArray, "SDL_UpdateTexture");
        AssertArrayParameterMarshal(updateTextureArray, 2, 3);
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateTextureRectPointer"), "SDL_UpdateTexture");
        MethodInfo updateTextureRectArray = GetNativeMethod("SDL_UpdateTextureRectArray");
        AssertNativeBoolImport(updateTextureRectArray, "SDL_UpdateTexture");
        AssertArrayParameterMarshal(updateTextureRectArray, 2, 3);
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateYUVTexturePointer"), "SDL_UpdateYUVTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateYUVTextureRect"), "SDL_UpdateYUVTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateNVTexturePointer"), "SDL_UpdateNVTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateNVTextureRect"), "SDL_UpdateNVTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_LockTexturePointer"), "SDL_LockTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_LockTextureRect"), "SDL_LockTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_LockTextureToSurfacePointer"), "SDL_LockTextureToSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_LockTextureToSurfaceRect"), "SDL_LockTextureToSurface");
        AssertNativeImport(GetNativeMethod("SDL_UnlockTexture"), "SDL_UnlockTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderTarget"), "SDL_SetRenderTarget");
        AssertNativeImport(GetNativeMethod("SDL_GetRenderTarget"), "SDL_GetRenderTarget");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderLogicalPresentation"), "SDL_SetRenderLogicalPresentation");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderLogicalPresentation"), "SDL_GetRenderLogicalPresentation");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderLogicalPresentationRect"), "SDL_GetRenderLogicalPresentationRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderCoordinatesFromWindow"), "SDL_RenderCoordinatesFromWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderCoordinatesToWindow"), "SDL_RenderCoordinatesToWindow");
        AssertNativeBoolDllImport(GetNativeMethod("SDL_ConvertEventToRenderCoordinates"), "SDL_ConvertEventToRenderCoordinates");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderViewportPointer"), "SDL_SetRenderViewport");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderViewportRect"), "SDL_SetRenderViewport");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderViewport"), "SDL_GetRenderViewport");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderViewportSet"), "SDL_RenderViewportSet");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderSafeArea"), "SDL_GetRenderSafeArea");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderClipRectPointer"), "SDL_SetRenderClipRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderClipRectRect"), "SDL_SetRenderClipRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderClipRect"), "SDL_GetRenderClipRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderClipEnabled"), "SDL_RenderClipEnabled");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderScale"), "SDL_SetRenderScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderScale"), "SDL_GetRenderScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderDrawColor"), "SDL_SetRenderDrawColor");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderDrawColorFloat"), "SDL_SetRenderDrawColorFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderDrawColor"), "SDL_GetRenderDrawColor");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderDrawColorFloat"), "SDL_GetRenderDrawColorFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderColorScale"), "SDL_SetRenderColorScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderColorScale"), "SDL_GetRenderColorScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderDrawBlendMode"), "SDL_SetRenderDrawBlendMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderDrawBlendMode"), "SDL_GetRenderDrawBlendMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderClear"), "SDL_RenderClear");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderPoint"), "SDL_RenderPoint");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderPointsArray"), "SDL_RenderPoints");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderPointsPointer"), "SDL_RenderPoints");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderLine"), "SDL_RenderLine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderLinesArray"), "SDL_RenderLines");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderLinesPointer"), "SDL_RenderLines");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderRectPointer"), "SDL_RenderRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderRectRect"), "SDL_RenderRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderRectsArray"), "SDL_RenderRects");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderRectsPointer"), "SDL_RenderRects");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderFillRectPointer"), "SDL_RenderFillRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderFillRectRect"), "SDL_RenderFillRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderFillRectsArray"), "SDL_RenderFillRects");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderFillRectsPointer"), "SDL_RenderFillRects");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexturePointers"), "SDL_RenderTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureSourceRect"), "SDL_RenderTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureDestinationRect"), "SDL_RenderTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRects"), "SDL_RenderTexture");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedPointers"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedSourceRect"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedDestinationRect"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedCenterPoint"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedRects"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedDestinationRectCenterPoint"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedSourceRectCenterPoint"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureRotatedRectsCenterPoint"), "SDL_RenderTextureRotated");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffinePointers"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineDownRect"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineRightRect"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineRightDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineOriginRect"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineOriginDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineOriginRightRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineOriginRightDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceRect"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceRightRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceRightDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceOriginRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceOriginDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceOriginRightRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureAffineSourceOriginRightDownRects"), "SDL_RenderTextureAffine");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureTiledPointers"), "SDL_RenderTextureTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureTiledSourceRect"), "SDL_RenderTextureTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureTiledDestinationRect"), "SDL_RenderTextureTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTextureTiledRects"), "SDL_RenderTextureTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridSourceRect"), "SDL_RenderTexture9Grid");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridDestinationRect"), "SDL_RenderTexture9Grid");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridRects"), "SDL_RenderTexture9Grid");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridTiledPointers"), "SDL_RenderTexture9GridTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridTiledSourceRect"), "SDL_RenderTexture9GridTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridTiledDestinationRect"), "SDL_RenderTexture9GridTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderTexture9GridTiledRects"), "SDL_RenderTexture9GridTiled");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderGeometryPointerIndices"), "SDL_RenderGeometry");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderGeometryPointers"), "SDL_RenderGeometry");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderGeometryArrayIndices"), "SDL_RenderGeometry");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderGeometryRawPointers"), "SDL_RenderGeometryRaw");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderGeometryRawPointerIndices"), "SDL_RenderGeometryRaw");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderGeometryRawByteIndices"), "SDL_RenderGeometryRaw");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderTextureAddressMode"), "SDL_SetRenderTextureAddressMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderTextureAddressMode"), "SDL_GetRenderTextureAddressMode");
        AssertNativeImport(GetNativeMethod("SDL_RenderReadPixels"), "SDL_RenderReadPixels");
        AssertNativeBoolImport(GetNativeMethod("SDL_RenderPresent"), "SDL_RenderPresent");
        AssertNativeImport(GetNativeMethod("SDL_DestroyTexture"), "SDL_DestroyTexture");
        AssertNativeImport(GetNativeMethod("SDL_DestroyRenderer"), "SDL_DestroyRenderer");
        AssertNativeBoolImport(GetNativeMethod("SDL_FlushRenderer"), "SDL_FlushRenderer");
        AssertNativeImport(GetNativeMethod("SDL_GetRenderMetalLayer"), "SDL_GetRenderMetalLayer");
        AssertNativeImport(GetNativeMethod("SDL_GetRenderMetalCommandEncoder"), "SDL_GetRenderMetalCommandEncoder");
        AssertNativeBoolImport(GetNativeMethod("SDL_AddVulkanRenderSemaphores"), "SDL_AddVulkanRenderSemaphores");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetRenderVSync"), "SDL_SetRenderVSync");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRenderVSync"), "SDL_GetRenderVSync");
        MethodInfo renderDebugText = GetNativeMethod("SDL_RenderDebugText");
        AssertNativeBoolImport(renderDebugText, "SDL_RenderDebugText");
        AssertStringParameterMarshal(renderDebugText, 3);
        MethodInfo renderDebugTextFormat = GetNativeMethod("SDL_RenderDebugTextFormat");
        AssertNativeBoolImport(renderDebugTextFormat, "SDL_RenderDebugTextFormat");
        AssertStringParameterMarshal(renderDebugTextFormat, 3);
        AssertNativeBoolImport(GetNativeMethod("SDL_SetDefaultTextureScaleMode"), "SDL_SetDefaultTextureScaleMode");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetDefaultTextureScaleMode"), "SDL_GetDefaultTextureScaleMode");
        AssertNativeImport(GetNativeMethod("SDL_CreateGPURenderState"), "SDL_CreateGPURenderState");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetGPURenderStateFragmentUniforms"), "SDL_SetGPURenderStateFragmentUniforms");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetGPURenderState"), "SDL_SetGPURenderState");
        AssertNativeImport(GetNativeMethod("SDL_DestroyGPURenderState"), "SDL_DestroyGPURenderState");
    }

    public static void RenderDriverFunctions_ForwardAndConvertStrings()
    {
        ResetCaptureState();
        nextInt = 3;

        using (NativeHookScope _ = NativeHookScope.Install("GetNumRenderDriversNativeFunction", nameof(CaptureGetNumRenderDrivers)))
        {
            int count = SDL3.SDL.GetNumRenderDrivers();

            TestAssert.Equal(3, count, "SDL.GetNumRenderDrivers must return the native hook value.");
        }

        ResetCaptureState();
        IntPtr renderDriverName = Marshal.StringToCoTaskMemUTF8("direct3d12");
        nextPointer = renderDriverName;

        using (NativeHookScope _ = NativeHookScope.Install("GetRenderDriverNativeFunction", nameof(CaptureGetRenderDriver)))
        {
            try
            {
                string? name = SDL3.SDL.GetRenderDriver(2);

                TestAssert.Equal("direct3d12", name, "SDL.GetRenderDriver must convert native UTF-8 strings.");
                TestAssert.Equal(2, capturedIndex, "SDL.GetRenderDriver must forward index.");

                nextPointer = IntPtr.Zero;
                string? missing = SDL3.SDL.GetRenderDriver(-1);

                TestAssert.Equal(null, missing, "SDL.GetRenderDriver must return null for native null.");
                TestAssert.Equal(-1, capturedIndex, "SDL.GetRenderDriver must forward invalid index.");
            }
            finally
            {
                Marshal.FreeCoTaskMem(renderDriverName);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        IntPtr rendererName = Marshal.StringToCoTaskMemUTF8("gpu");
        nextPointer = rendererName;

        using (NativeHookScope _ = NativeHookScope.Install("GetRendererNameNativeFunction", nameof(CaptureGetRendererName)))
        {
            try
            {
                string? name = SDL3.SDL.GetRendererName((IntPtr)0x7070);

                TestAssert.Equal("gpu", name, "SDL.GetRendererName must convert native UTF-8 strings.");
                TestAssert.Equal((IntPtr)0x7070, capturedRenderer, "SDL.GetRendererName must forward renderer.");

                nextPointer = IntPtr.Zero;
                string? missing = SDL3.SDL.GetRendererName((IntPtr)0x8080);

                TestAssert.Equal(null, missing, "SDL.GetRendererName must return null for native null.");
                TestAssert.Equal((IntPtr)0x8080, capturedRenderer, "SDL.GetRendererName must forward renderer for null result.");
            }
            finally
            {
                Marshal.FreeCoTaskMem(rendererName);
                nextPointer = IntPtr.Zero;
            }
        }
    }

    public static void CreateWindowAndRenderer_ForwardsArgumentsOutputsPointersAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        IntPtr nextWindow = (IntPtr)0x1111;
        IntPtr nextRenderer = (IntPtr)0x2222;
        nextPointer = nextWindow;
        capturedPointer = nextRenderer;

        using NativeHookScope _ = NativeHookScope.Install("CreateWindowAndRendererNativeFunction", nameof(CaptureCreateWindowAndRenderer));
        bool result = SDL3.SDL.CreateWindowAndRenderer("window title", 640, 480, SDL3.SDL.WindowFlags.Resizable, out IntPtr window, out IntPtr renderer);

        TestAssert.Equal(true, result, "SDL.CreateWindowAndRenderer must return the native hook value.");
        TestAssert.Equal("window title", capturedTitle, "SDL.CreateWindowAndRenderer must forward title.");
        TestAssert.Equal(640, capturedWidth, "SDL.CreateWindowAndRenderer must forward width.");
        TestAssert.Equal(480, capturedHeight, "SDL.CreateWindowAndRenderer must forward height.");
        TestAssert.Equal(SDL3.SDL.WindowFlags.Resizable, capturedWindowFlags, "SDL.CreateWindowAndRenderer must forward flags.");
        TestAssert.Equal(nextWindow, window, "SDL.CreateWindowAndRenderer must output window.");
        TestAssert.Equal(nextRenderer, renderer, "SDL.CreateWindowAndRenderer must output renderer.");
    }

    public static void RendererPointerFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x3333;

        using (NativeHookScope _ = NativeHookScope.Install("CreateRendererNativeFunction", nameof(CaptureCreateRenderer)))
        {
            IntPtr result = SDL3.SDL.CreateRenderer((IntPtr)0x1000, "software");

            TestAssert.Equal((IntPtr)0x3333, result, "SDL.CreateRenderer must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1000, capturedWindow, "SDL.CreateRenderer must forward window.");
            TestAssert.Equal("software", capturedName, "SDL.CreateRenderer must forward name.");

            SDL3.SDL.CreateRenderer((IntPtr)0x1001, null);
            TestAssert.Equal(null, capturedName, "SDL.CreateRenderer must forward null name.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x4444;
        using (NativeHookScope _ = NativeHookScope.Install("CreateRendererWithPropertiesNativeFunction", nameof(CaptureCreateRendererWithProperties)))
        {
            IntPtr result = SDL3.SDL.CreateRendererWithProperties(42);

            TestAssert.Equal((IntPtr)0x4444, result, "SDL.CreateRendererWithProperties must return the native hook value.");
            TestAssert.Equal(42u, capturedProps, "SDL.CreateRendererWithProperties must forward props.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x5555;
        using (NativeHookScope _ = NativeHookScope.Install("CreateGPURendererNativeFunction", nameof(CaptureCreateGPURenderer)))
        {
            IntPtr result = SDL3.SDL.CreateGPURenderer((IntPtr)0x6000, (IntPtr)0x7000);

            TestAssert.Equal((IntPtr)0x5555, result, "SDL.CreateGPURenderer must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6000, capturedDevice, "SDL.CreateGPURenderer must forward device.");
            TestAssert.Equal((IntPtr)0x7000, capturedWindow, "SDL.CreateGPURenderer must forward window.");
        }

        AssertSinglePointerReturn("GetGPURendererDeviceNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetGPURendererDevice, (IntPtr)0x8000, (IntPtr)0x8001, "SDL.GetGPURendererDevice");
        AssertSinglePointerReturn("CreateSoftwareRendererNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.CreateSoftwareRenderer, (IntPtr)0x8002, (IntPtr)0x8003, "SDL.CreateSoftwareRenderer");
        AssertSinglePointerReturn("GetRendererNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetRenderer, (IntPtr)0x8004, (IntPtr)0x8005, "SDL.GetRenderer");
        AssertSinglePointerReturn("GetRenderWindowNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetRenderWindow, (IntPtr)0x8006, (IntPtr)0x8007, "SDL.GetRenderWindow");

        ResetCaptureState();
        nextUInt = 77;
        using NativeHookScope rendererProperties = NativeHookScope.Install("GetRendererPropertiesNativeFunction", nameof(CaptureSinglePointerReturnUInt));
        uint properties = SDL3.SDL.GetRendererProperties((IntPtr)0x9000);

        TestAssert.Equal(77u, properties, "SDL.GetRendererProperties must return the native hook value.");
        TestAssert.Equal((IntPtr)0x9000, capturedPointer, "SDL.GetRendererProperties must forward renderer.");
    }

    public static void RenderOutputSizeFunctions_ForwardRendererAndReturnSizes()
    {
        ResetCaptureState();
        nextBool = true;
        nextWidth = 1920;
        nextHeight = 1080;

        using (NativeHookScope _ = NativeHookScope.Install("GetRenderOutputSizeNativeFunction", nameof(CaptureOutputSize)))
        {
            bool result = SDL3.SDL.GetRenderOutputSize((IntPtr)0x1010, out int w, out int h);

            TestAssert.Equal(true, result, "SDL.GetRenderOutputSize must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1010, capturedRenderer, "SDL.GetRenderOutputSize must forward renderer.");
            TestAssert.Equal(1920, w, "SDL.GetRenderOutputSize must output width.");
            TestAssert.Equal(1080, h, "SDL.GetRenderOutputSize must output height.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 1280;
        nextHeight = 720;

        using (NativeHookScope _ = NativeHookScope.Install("GetCurrentRenderOutputSizeNativeFunction", nameof(CaptureOutputSize)))
        {
            bool result = SDL3.SDL.GetCurrentRenderOutputSize((IntPtr)0x2020, out int w, out int h);

            TestAssert.Equal(true, result, "SDL.GetCurrentRenderOutputSize must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2020, capturedRenderer, "SDL.GetCurrentRenderOutputSize must forward renderer.");
            TestAssert.Equal(1280, w, "SDL.GetCurrentRenderOutputSize must output width.");
            TestAssert.Equal(720, h, "SDL.GetCurrentRenderOutputSize must output height.");
        }
    }

    public static void TextureCreationAndPropertyFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x3030;

        using (NativeHookScope _ = NativeHookScope.Install("CreateTextureNativeFunction", nameof(CaptureCreateTexture)))
        {
            IntPtr result = SDL3.SDL.CreateTexture((IntPtr)0x3031, SDL3.SDL.PixelFormat.RGBA8888, SDL3.SDL.TextureAccess.Streaming, 320, 240);

            TestAssert.Equal((IntPtr)0x3030, result, "SDL.CreateTexture must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3031, capturedRenderer, "SDL.CreateTexture must forward renderer.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, capturedPixelFormat, "SDL.CreateTexture must forward format.");
            TestAssert.Equal(SDL3.SDL.TextureAccess.Streaming, capturedTextureAccess, "SDL.CreateTexture must forward access.");
            TestAssert.Equal(320, capturedWidth, "SDL.CreateTexture must forward width.");
            TestAssert.Equal(240, capturedHeight, "SDL.CreateTexture must forward height.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x4040;
        using (NativeHookScope _ = NativeHookScope.Install("CreateTextureFromSurfaceNativeFunction", nameof(CaptureCreateTextureFromSurface)))
        {
            IntPtr result = SDL3.SDL.CreateTextureFromSurface((IntPtr)0x4041, (IntPtr)0x4042);

            TestAssert.Equal((IntPtr)0x4040, result, "SDL.CreateTextureFromSurface must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4041, capturedRenderer, "SDL.CreateTextureFromSurface must forward renderer.");
            TestAssert.Equal((IntPtr)0x4042, capturedSurface, "SDL.CreateTextureFromSurface must forward surface.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x5050;
        using (NativeHookScope _ = NativeHookScope.Install("CreateTextureWithPropertiesNativeFunction", nameof(CaptureCreateTextureWithProperties)))
        {
            IntPtr result = SDL3.SDL.CreateTextureWithProperties((IntPtr)0x5051, 123);

            TestAssert.Equal((IntPtr)0x5050, result, "SDL.CreateTextureWithProperties must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5051, capturedRenderer, "SDL.CreateTextureWithProperties must forward renderer.");
            TestAssert.Equal(123u, capturedProps, "SDL.CreateTextureWithProperties must forward props.");
        }

        ResetCaptureState();
        nextUInt = 321;
        using (NativeHookScope _ = NativeHookScope.Install("GetTexturePropertiesNativeFunction", nameof(CaptureSinglePointerReturnUInt)))
        {
            uint result = SDL3.SDL.GetTextureProperties((IntPtr)0x6060);

            TestAssert.Equal(321u, result, "SDL.GetTextureProperties must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6060, capturedPointer, "SDL.GetTextureProperties must forward texture.");
        }

        AssertSinglePointerReturn("GetRendererFromTextureNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetRendererFromTexture, (IntPtr)0x7071, (IntPtr)0x7072, "SDL.GetRendererFromTexture");

        ResetCaptureState();
        nextBool = true;
        nextFloatWidth = 192.5f;
        nextFloatHeight = 108.5f;
        using NativeHookScope size = NativeHookScope.Install("GetTextureSizeNativeFunction", nameof(CaptureGetTextureSize));
        bool sizeResult = SDL3.SDL.GetTextureSize((IntPtr)0x8080, out float w, out float h);

        TestAssert.Equal(true, sizeResult, "SDL.GetTextureSize must return the native hook value.");
        TestAssert.Equal((IntPtr)0x8080, capturedTexture, "SDL.GetTextureSize must forward texture.");
        TestAssert.Equal(192.5f, w, "SDL.GetTextureSize must output width.");
        TestAssert.Equal(108.5f, h, "SDL.GetTextureSize must output height.");
    }

    public static void TexturePaletteAndModulationFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTexturePaletteNativeFunction", nameof(CaptureSetTexturePalette)))
        {
            bool result = SDL3.SDL.SetTexturePalette((IntPtr)0x1110, (IntPtr)0x1111);

            TestAssert.Equal(true, result, "SDL.SetTexturePalette must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1110, capturedTexture, "SDL.SetTexturePalette must forward texture.");
            TestAssert.Equal((IntPtr)0x1111, capturedPalette, "SDL.SetTexturePalette must forward palette.");
        }

        AssertSinglePointerReturn("GetTexturePaletteNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetTexturePalette, (IntPtr)0x2220, (IntPtr)0x2221, "SDL.GetTexturePalette");

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextureColorModNativeFunction", nameof(CaptureSetTextureColorMod)))
        {
            bool result = SDL3.SDL.SetTextureColorMod((IntPtr)0x3330, 10, 20, 30);

            TestAssert.Equal(true, result, "SDL.SetTextureColorMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3330, capturedTexture, "SDL.SetTextureColorMod must forward texture.");
            TestAssert.Equal(10, capturedR, "SDL.SetTextureColorMod must forward red.");
            TestAssert.Equal(20, capturedG, "SDL.SetTextureColorMod must forward green.");
            TestAssert.Equal(30, capturedB, "SDL.SetTextureColorMod must forward blue.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextureColorModFloatNativeFunction", nameof(CaptureSetTextureColorModFloat)))
        {
            bool result = SDL3.SDL.SetTextureColorModFloat((IntPtr)0x4440, 0.1f, 0.2f, 0.3f);

            TestAssert.Equal(true, result, "SDL.SetTextureColorModFloat must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4440, capturedTexture, "SDL.SetTextureColorModFloat must forward texture.");
            TestAssert.Equal(0.1f, capturedFR, "SDL.SetTextureColorModFloat must forward red.");
            TestAssert.Equal(0.2f, capturedFG, "SDL.SetTextureColorModFloat must forward green.");
            TestAssert.Equal(0.3f, capturedFB, "SDL.SetTextureColorModFloat must forward blue.");
        }

        ResetCaptureState();
        nextBool = true;
        nextR = 40;
        nextG = 50;
        nextB = 60;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextureColorModNativeFunction", nameof(CaptureGetTextureColorMod)))
        {
            bool result = SDL3.SDL.GetTextureColorMod((IntPtr)0x5550, out byte r, out byte g, out byte b);

            TestAssert.Equal(true, result, "SDL.GetTextureColorMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5550, capturedTexture, "SDL.GetTextureColorMod must forward texture.");
            TestAssert.Equal(40, r, "SDL.GetTextureColorMod must output red.");
            TestAssert.Equal(50, g, "SDL.GetTextureColorMod must output green.");
            TestAssert.Equal(60, b, "SDL.GetTextureColorMod must output blue.");
        }

        ResetCaptureState();
        nextBool = true;
        nextFR = 0.4f;
        nextFG = 0.5f;
        nextFB = 0.6f;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextureColorModFloatNativeFunction", nameof(CaptureGetTextureColorModFloat)))
        {
            bool result = SDL3.SDL.GetTextureColorModFloat((IntPtr)0x6660, out float r, out float g, out float b);

            TestAssert.Equal(true, result, "SDL.GetTextureColorModFloat must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6660, capturedTexture, "SDL.GetTextureColorModFloat must forward texture.");
            TestAssert.Equal(0.4f, r, "SDL.GetTextureColorModFloat must output red.");
            TestAssert.Equal(0.5f, g, "SDL.GetTextureColorModFloat must output green.");
            TestAssert.Equal(0.6f, b, "SDL.GetTextureColorModFloat must output blue.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextureAlphaModNativeFunction", nameof(CaptureSetTextureAlphaMod)))
        {
            bool result = SDL3.SDL.SetTextureAlphaMod((IntPtr)0x7770, 200);

            TestAssert.Equal(true, result, "SDL.SetTextureAlphaMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7770, capturedTexture, "SDL.SetTextureAlphaMod must forward texture.");
            TestAssert.Equal(200, capturedAlpha, "SDL.SetTextureAlphaMod must forward alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextureAlphaModFloatNativeFunction", nameof(CaptureSetTextureAlphaModFloat)))
        {
            bool result = SDL3.SDL.SetTextureAlphaModFloat((IntPtr)0x8880, 0.75f);

            TestAssert.Equal(true, result, "SDL.SetTextureAlphaModFloat must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8880, capturedTexture, "SDL.SetTextureAlphaModFloat must forward texture.");
            TestAssert.Equal(0.75f, capturedFAlpha, "SDL.SetTextureAlphaModFloat must forward alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        nextAlpha = 201;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextureAlphaModNativeFunction", nameof(CaptureGetTextureAlphaMod)))
        {
            bool result = SDL3.SDL.GetTextureAlphaMod((IntPtr)0x9990, out byte alpha);

            TestAssert.Equal(true, result, "SDL.GetTextureAlphaMod must return the native hook value.");
            TestAssert.Equal((IntPtr)0x9990, capturedTexture, "SDL.GetTextureAlphaMod must forward texture.");
            TestAssert.Equal(201, alpha, "SDL.GetTextureAlphaMod must output alpha.");
        }

        ResetCaptureState();
        nextBool = true;
        nextFAlpha = 0.85f;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextureAlphaModFloatNativeFunction", nameof(CaptureGetTextureAlphaModFloat)))
        {
            bool result = SDL3.SDL.GetTextureAlphaModFloat((IntPtr)0x9991, out float alpha);

            TestAssert.Equal(true, result, "SDL.GetTextureAlphaModFloat must return the native hook value.");
            TestAssert.Equal((IntPtr)0x9991, capturedTexture, "SDL.GetTextureAlphaModFloat must forward texture.");
            TestAssert.Equal(0.85f, alpha, "SDL.GetTextureAlphaModFloat must output alpha.");
        }
    }

    public static void TextureBlendAndScaleFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextureBlendModeNativeFunction", nameof(CaptureSetTextureBlendMode)))
        {
            bool result = SDL3.SDL.SetTextureBlendMode((IntPtr)0x1212, SDL3.SDL.BlendMode.Add);

            TestAssert.Equal(true, result, "SDL.SetTextureBlendMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1212, capturedTexture, "SDL.SetTextureBlendMode must forward texture.");
            TestAssert.Equal(SDL3.SDL.BlendMode.Add, capturedBlendMode, "SDL.SetTextureBlendMode must forward blend mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBlendMode = SDL3.SDL.BlendMode.Blend;
        using (NativeHookScope _ = NativeHookScope.Install("GetTextureBlendModeNativeFunction", nameof(CaptureGetTextureBlendMode)))
        {
            bool result = SDL3.SDL.GetTextureBlendMode((IntPtr)0x1313, out SDL3.SDL.BlendMode blendMode);

            TestAssert.Equal(true, result, "SDL.GetTextureBlendMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1313, capturedTexture, "SDL.GetTextureBlendMode must forward texture.");
            TestAssert.Equal(SDL3.SDL.BlendMode.Blend, blendMode, "SDL.GetTextureBlendMode must output blend mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetTextureScaleModeNativeFunction", nameof(CaptureSetTextureScaleMode)))
        {
            bool result = SDL3.SDL.SetTextureScaleMode((IntPtr)0x1414, SDL3.SDL.ScaleMode.PixelArt);

            TestAssert.Equal(true, result, "SDL.SetTextureScaleMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x1414, capturedTexture, "SDL.SetTextureScaleMode must forward texture.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.SetTextureScaleMode must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextScaleMode = SDL3.SDL.ScaleMode.Linear;
        using NativeHookScope getScale = NativeHookScope.Install("GetTextureScaleModeNativeFunction", nameof(CaptureGetTextureScaleMode));
        bool scaleResult = SDL3.SDL.GetTextureScaleMode((IntPtr)0x1515, out SDL3.SDL.ScaleMode scaleMode);

        TestAssert.Equal(true, scaleResult, "SDL.GetTextureScaleMode must return the native hook value.");
        TestAssert.Equal((IntPtr)0x1515, capturedTexture, "SDL.GetTextureScaleMode must forward texture.");
        TestAssert.Equal(SDL3.SDL.ScaleMode.Linear, scaleMode, "SDL.GetTextureScaleMode must output scale mode.");
    }

    public static void TextureUpdateFunctions_ForwardPointerArraySpanAndRectOverloads()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTexturePointerNativeFunction", nameof(CaptureUpdateTexturePointer)))
        {
            bool result = SDL3.SDL.UpdateTexture((IntPtr)0x2001, (IntPtr)0x2002, (IntPtr)0x2003, 64);

            TestAssert.Equal(true, result, "SDL.UpdateTexture(IntPtr, IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2001, capturedTexture, "SDL.UpdateTexture(IntPtr, IntPtr) must forward texture.");
            TestAssert.Equal((IntPtr)0x2002, capturedRectPointer, "SDL.UpdateTexture(IntPtr, IntPtr) must forward rect pointer.");
            TestAssert.Equal((IntPtr)0x2003, capturedPixels, "SDL.UpdateTexture(IntPtr, IntPtr) must forward pixels.");
            TestAssert.Equal(64, capturedPitch, "SDL.UpdateTexture(IntPtr, IntPtr) must forward pitch.");
        }

        ResetCaptureState();
        nextBool = true;
        byte[] bytes = [1, 2, 3, 4];
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTextureArrayNativeFunction", nameof(CaptureUpdateTextureArray)))
        {
            bool result = SDL3.SDL.UpdateTexture((IntPtr)0x2011, (IntPtr)0x2012, bytes, 16);

            TestAssert.Equal(true, result, "SDL.UpdateTexture(byte[]) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2011, capturedTexture, "SDL.UpdateTexture(byte[]) must forward texture.");
            TestAssert.Equal((IntPtr)0x2012, capturedRectPointer, "SDL.UpdateTexture(byte[]) must forward rect pointer.");
            AssertBytes(bytes, capturedBytes, "SDL.UpdateTexture(byte[]) must forward pixels.");
            TestAssert.Equal(16, capturedPitch, "SDL.UpdateTexture(byte[]) must forward pitch.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTexturePointerNativeFunction", nameof(CaptureUpdateTexturePointer)))
        {
            bool result = SDL3.SDL.UpdateTexture((IntPtr)0x2021, (IntPtr)0x2022, bytes.AsSpan(), 8);

            TestAssert.Equal(true, result, "SDL.UpdateTexture(Span, IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2021, capturedTexture, "SDL.UpdateTexture(Span, IntPtr) must forward texture.");
            TestAssert.Equal((IntPtr)0x2022, capturedRectPointer, "SDL.UpdateTexture(Span, IntPtr) must forward rect pointer.");
            TestAssert.True(capturedPixels != IntPtr.Zero, "SDL.UpdateTexture(Span, IntPtr) must pin and forward pixels.");
            TestAssert.Equal(8, capturedPitch, "SDL.UpdateTexture(Span, IntPtr) must forward pitch.");
        }

        SDL3.SDL.Rect rect = CreateRect(1, 2, 3, 4);
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTextureRectPointerNativeFunction", nameof(CaptureUpdateTextureRectPointer)))
        {
            bool result = SDL3.SDL.UpdateTexture((IntPtr)0x2031, in rect, (IntPtr)0x2032, 32);

            TestAssert.Equal(true, result, "SDL.UpdateTexture(in Rect, IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2031, capturedTexture, "SDL.UpdateTexture(in Rect, IntPtr) must forward texture.");
            AssertRect(rect, capturedRect, "SDL.UpdateTexture(in Rect, IntPtr) must forward rect.");
            TestAssert.Equal((IntPtr)0x2032, capturedPixels, "SDL.UpdateTexture(in Rect, IntPtr) must forward pixels.");
            TestAssert.Equal(32, capturedPitch, "SDL.UpdateTexture(in Rect, IntPtr) must forward pitch.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTextureRectArrayNativeFunction", nameof(CaptureUpdateTextureRectArray)))
        {
            bool result = SDL3.SDL.UpdateTexture((IntPtr)0x2041, in rect, bytes, 24);

            TestAssert.Equal(true, result, "SDL.UpdateTexture(in Rect, byte[]) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2041, capturedTexture, "SDL.UpdateTexture(in Rect, byte[]) must forward texture.");
            AssertRect(rect, capturedRect, "SDL.UpdateTexture(in Rect, byte[]) must forward rect.");
            AssertBytes(bytes, capturedBytes, "SDL.UpdateTexture(in Rect, byte[]) must forward pixels.");
            TestAssert.Equal(24, capturedPitch, "SDL.UpdateTexture(in Rect, byte[]) must forward pitch.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateTextureRectPointerNativeFunction", nameof(CaptureUpdateTextureRectPointer)))
        {
            bool result = SDL3.SDL.UpdateTexture((IntPtr)0x2051, in rect, bytes.AsSpan(), 12);

            TestAssert.Equal(true, result, "SDL.UpdateTexture(in Rect, Span) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x2051, capturedTexture, "SDL.UpdateTexture(in Rect, Span) must forward texture.");
            AssertRect(rect, capturedRect, "SDL.UpdateTexture(in Rect, Span) must forward rect.");
            TestAssert.True(capturedPixels != IntPtr.Zero, "SDL.UpdateTexture(in Rect, Span) must pin and forward pixels.");
            TestAssert.Equal(12, capturedPitch, "SDL.UpdateTexture(in Rect, Span) must forward pitch.");
        }
    }

    public static void TexturePlaneUpdateFunctions_ForwardYuvAndNvOverloads()
    {
        SDL3.SDL.Rect rect = CreateRect(2, 3, 4, 5);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateYUVTexturePointerNativeFunction", nameof(CaptureUpdateYUVTexturePointer)))
        {
            bool result = SDL3.SDL.UpdateYUVTexture((IntPtr)0x3001, (IntPtr)0x3002, (IntPtr)0x3003, 10, (IntPtr)0x3004, 20, (IntPtr)0x3005, 30);

            TestAssert.Equal(true, result, "SDL.UpdateYUVTexture(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3001, capturedTexture, "SDL.UpdateYUVTexture(IntPtr) must forward texture.");
            TestAssert.Equal((IntPtr)0x3002, capturedRectPointer, "SDL.UpdateYUVTexture(IntPtr) must forward rect pointer.");
            AssertYuvPlanes((IntPtr)0x3003, 10, (IntPtr)0x3004, 20, (IntPtr)0x3005, 30, "SDL.UpdateYUVTexture(IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateYUVTextureRectNativeFunction", nameof(CaptureUpdateYUVTextureRect)))
        {
            bool result = SDL3.SDL.UpdateYUVTexture((IntPtr)0x3011, in rect, (IntPtr)0x3012, 11, (IntPtr)0x3013, 21, (IntPtr)0x3014, 31);

            TestAssert.Equal(true, result, "SDL.UpdateYUVTexture(in Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3011, capturedTexture, "SDL.UpdateYUVTexture(in Rect) must forward texture.");
            AssertRect(rect, capturedRect, "SDL.UpdateYUVTexture(in Rect) must forward rect.");
            AssertYuvPlanes((IntPtr)0x3012, 11, (IntPtr)0x3013, 21, (IntPtr)0x3014, 31, "SDL.UpdateYUVTexture(in Rect)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateNVTexturePointerNativeFunction", nameof(CaptureUpdateNVTexturePointer)))
        {
            bool result = SDL3.SDL.UpdateNVTexture((IntPtr)0x3021, (IntPtr)0x3022, (IntPtr)0x3023, 12, (IntPtr)0x3024, 22);

            TestAssert.Equal(true, result, "SDL.UpdateNVTexture(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x3021, capturedTexture, "SDL.UpdateNVTexture(IntPtr) must forward texture.");
            TestAssert.Equal((IntPtr)0x3022, capturedRectPointer, "SDL.UpdateNVTexture(IntPtr) must forward rect pointer.");
            AssertNvPlanes((IntPtr)0x3023, 12, (IntPtr)0x3024, 22, "SDL.UpdateNVTexture(IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope nvRect = NativeHookScope.Install("UpdateNVTextureRectNativeFunction", nameof(CaptureUpdateNVTextureRect));
        bool rectResult = SDL3.SDL.UpdateNVTexture((IntPtr)0x3031, in rect, (IntPtr)0x3032, 13, (IntPtr)0x3033, 23);

        TestAssert.Equal(true, rectResult, "SDL.UpdateNVTexture(in Rect) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x3031, capturedTexture, "SDL.UpdateNVTexture(in Rect) must forward texture.");
        AssertRect(rect, capturedRect, "SDL.UpdateNVTexture(in Rect) must forward rect.");
        AssertNvPlanes((IntPtr)0x3032, 13, (IntPtr)0x3033, 23, "SDL.UpdateNVTexture(in Rect)");
    }

    public static void TextureLockingFunctions_ForwardInputsOutputsAndUnlock()
    {
        SDL3.SDL.Rect rect = CreateRect(4, 5, 6, 7);

        ResetCaptureState();
        nextBool = true;
        nextPixels = (IntPtr)0x4003;
        nextPitch = 256;
        using (NativeHookScope _ = NativeHookScope.Install("LockTexturePointerNativeFunction", nameof(CaptureLockTexturePointer)))
        {
            bool result = SDL3.SDL.LockTexture((IntPtr)0x4001, (IntPtr)0x4002, out IntPtr pixels, out int pitch);

            TestAssert.Equal(true, result, "SDL.LockTexture(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4001, capturedTexture, "SDL.LockTexture(IntPtr) must forward texture.");
            TestAssert.Equal((IntPtr)0x4002, capturedRectPointer, "SDL.LockTexture(IntPtr) must forward rect pointer.");
            TestAssert.Equal((IntPtr)0x4003, pixels, "SDL.LockTexture(IntPtr) must output pixels.");
            TestAssert.Equal(256, pitch, "SDL.LockTexture(IntPtr) must output pitch.");
        }

        ResetCaptureState();
        nextBool = true;
        nextPixels = (IntPtr)0x4012;
        nextPitch = 512;
        using (NativeHookScope _ = NativeHookScope.Install("LockTextureRectNativeFunction", nameof(CaptureLockTextureRect)))
        {
            bool result = SDL3.SDL.LockTexture((IntPtr)0x4011, in rect, out IntPtr pixels, out int pitch);

            TestAssert.Equal(true, result, "SDL.LockTexture(in Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4011, capturedTexture, "SDL.LockTexture(in Rect) must forward texture.");
            AssertRect(rect, capturedRect, "SDL.LockTexture(in Rect) must forward rect.");
            TestAssert.Equal((IntPtr)0x4012, pixels, "SDL.LockTexture(in Rect) must output pixels.");
            TestAssert.Equal(512, pitch, "SDL.LockTexture(in Rect) must output pitch.");
        }

        ResetCaptureState();
        nextBool = true;
        nextSurface = (IntPtr)0x4023;
        using (NativeHookScope _ = NativeHookScope.Install("LockTextureToSurfacePointerNativeFunction", nameof(CaptureLockTextureToSurfacePointer)))
        {
            bool result = SDL3.SDL.LockTextureToSurface((IntPtr)0x4021, (IntPtr)0x4022, out IntPtr surface);

            TestAssert.Equal(true, result, "SDL.LockTextureToSurface(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4021, capturedTexture, "SDL.LockTextureToSurface(IntPtr) must forward texture.");
            TestAssert.Equal((IntPtr)0x4022, capturedRectPointer, "SDL.LockTextureToSurface(IntPtr) must forward rect pointer.");
            TestAssert.Equal((IntPtr)0x4023, surface, "SDL.LockTextureToSurface(IntPtr) must output surface.");
        }

        ResetCaptureState();
        nextBool = true;
        nextSurface = (IntPtr)0x4032;
        using (NativeHookScope _ = NativeHookScope.Install("LockTextureToSurfaceRectNativeFunction", nameof(CaptureLockTextureToSurfaceRect)))
        {
            bool result = SDL3.SDL.LockTextureToSurface((IntPtr)0x4031, in rect, out IntPtr surface);

            TestAssert.Equal(true, result, "SDL.LockTextureToSurface(in Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x4031, capturedTexture, "SDL.LockTextureToSurface(in Rect) must forward texture.");
            AssertRect(rect, capturedRect, "SDL.LockTextureToSurface(in Rect) must forward rect.");
            TestAssert.Equal((IntPtr)0x4032, surface, "SDL.LockTextureToSurface(in Rect) must output surface.");
        }

        ResetCaptureState();
        using NativeHookScope unlock = NativeHookScope.Install("UnlockTextureNativeFunction", nameof(CaptureUnlockTexture));
        SDL3.SDL.UnlockTexture((IntPtr)0x4040);

        TestAssert.Equal((IntPtr)0x4040, capturedTexture, "SDL.UnlockTexture must forward texture.");
    }

    public static void RenderTargetLogicalPresentationAndCoordinates_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderTargetNativeFunction", nameof(CaptureSetRenderTarget)))
        {
            bool result = SDL3.SDL.SetRenderTarget((IntPtr)0x5001, (IntPtr)0x5002);

            TestAssert.Equal(true, result, "SDL.SetRenderTarget must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5001, capturedRenderer, "SDL.SetRenderTarget must forward renderer.");
            TestAssert.Equal((IntPtr)0x5002, capturedTexture, "SDL.SetRenderTarget must forward texture.");
        }

        AssertSinglePointerReturn("GetRenderTargetNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetRenderTarget, (IntPtr)0x5011, (IntPtr)0x5012, "SDL.GetRenderTarget");

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderLogicalPresentationNativeFunction", nameof(CaptureSetRenderLogicalPresentation)))
        {
            bool result = SDL3.SDL.SetRenderLogicalPresentation((IntPtr)0x5021, 800, 600, SDL3.SDL.RendererLogicalPresentation.Letterbox);

            TestAssert.Equal(true, result, "SDL.SetRenderLogicalPresentation must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5021, capturedRenderer, "SDL.SetRenderLogicalPresentation must forward renderer.");
            TestAssert.Equal(800, capturedWidth, "SDL.SetRenderLogicalPresentation must forward width.");
            TestAssert.Equal(600, capturedHeight, "SDL.SetRenderLogicalPresentation must forward height.");
            TestAssert.Equal(SDL3.SDL.RendererLogicalPresentation.Letterbox, capturedPresentationMode, "SDL.SetRenderLogicalPresentation must forward mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 1024;
        nextHeight = 768;
        nextPresentationMode = SDL3.SDL.RendererLogicalPresentation.Stretch;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderLogicalPresentationNativeFunction", nameof(CaptureGetRenderLogicalPresentation)))
        {
            bool result = SDL3.SDL.GetRenderLogicalPresentation((IntPtr)0x5031, out int w, out int h, out SDL3.SDL.RendererLogicalPresentation mode);

            TestAssert.Equal(true, result, "SDL.GetRenderLogicalPresentation must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5031, capturedRenderer, "SDL.GetRenderLogicalPresentation must forward renderer.");
            TestAssert.Equal(1024, w, "SDL.GetRenderLogicalPresentation must output width.");
            TestAssert.Equal(768, h, "SDL.GetRenderLogicalPresentation must output height.");
            TestAssert.Equal(SDL3.SDL.RendererLogicalPresentation.Stretch, mode, "SDL.GetRenderLogicalPresentation must output mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderLogicalPresentationRectNativeFunction", nameof(CaptureGetRenderLogicalPresentationRect)))
        {
            bool result = SDL3.SDL.GetRenderLogicalPresentationRect((IntPtr)0x5041, out SDL3.SDL.FRect rect);

            TestAssert.Equal(true, result, "SDL.GetRenderLogicalPresentationRect must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5041, capturedRenderer, "SDL.GetRenderLogicalPresentationRect must forward renderer.");
            AssertFRect(nextFRect, rect, "SDL.GetRenderLogicalPresentationRect must output rect.");
        }

        ResetCaptureState();
        nextBool = true;
        nextX = 5.5f;
        nextY = 6.5f;
        using (NativeHookScope _ = NativeHookScope.Install("RenderCoordinatesFromWindowNativeFunction", nameof(CaptureRenderCoordinatesFromWindow)))
        {
            bool result = SDL3.SDL.RenderCoordinatesFromWindow((IntPtr)0x5051, 10.5f, 20.5f, out float x, out float y);

            TestAssert.Equal(true, result, "SDL.RenderCoordinatesFromWindow must return the native hook value.");
            TestAssert.Equal((IntPtr)0x5051, capturedRenderer, "SDL.RenderCoordinatesFromWindow must forward renderer.");
            TestAssert.Equal(10.5f, capturedWindowX, "SDL.RenderCoordinatesFromWindow must forward window x.");
            TestAssert.Equal(20.5f, capturedWindowY, "SDL.RenderCoordinatesFromWindow must forward window y.");
            TestAssert.Equal(5.5f, x, "SDL.RenderCoordinatesFromWindow must output x.");
            TestAssert.Equal(6.5f, y, "SDL.RenderCoordinatesFromWindow must output y.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWindowX = 30.5f;
        nextWindowY = 40.5f;
        using NativeHookScope toWindow = NativeHookScope.Install("RenderCoordinatesToWindowNativeFunction", nameof(CaptureRenderCoordinatesToWindow));
        bool toWindowResult = SDL3.SDL.RenderCoordinatesToWindow((IntPtr)0x5061, 7.5f, 8.5f, out float windowx, out float windowy);

        TestAssert.Equal(true, toWindowResult, "SDL.RenderCoordinatesToWindow must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5061, capturedRenderer, "SDL.RenderCoordinatesToWindow must forward renderer.");
        TestAssert.Equal(7.5f, capturedX, "SDL.RenderCoordinatesToWindow must forward x.");
        TestAssert.Equal(8.5f, capturedY, "SDL.RenderCoordinatesToWindow must forward y.");
        TestAssert.Equal(30.5f, windowx, "SDL.RenderCoordinatesToWindow must output window x.");
        TestAssert.Equal(40.5f, windowy, "SDL.RenderCoordinatesToWindow must output window y.");
    }

    public static void ConvertEventToRenderCoordinates_ForwardsRendererAndRefEvent()
    {
        ResetCaptureState();
        nextBool = true;
        nextEventType = (uint)SDL3.SDL.EventType.MouseButtonDown;
        SDL3.SDL.Event renderEvent = new() { Type = (uint)SDL3.SDL.EventType.MouseMotion };

        using NativeHookScope _ = NativeHookScope.Install("ConvertEventToRenderCoordinatesNativeFunction", nameof(CaptureConvertEventToRenderCoordinates));
        bool result = SDL3.SDL.ConvertEventToRenderCoordinates((IntPtr)0x5071, ref renderEvent);

        TestAssert.Equal(true, result, "SDL.ConvertEventToRenderCoordinates must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5071, capturedRenderer, "SDL.ConvertEventToRenderCoordinates must forward renderer.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.MouseMotion, capturedEventType, "SDL.ConvertEventToRenderCoordinates must forward the original event by reference.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.MouseButtonDown, renderEvent.Type, "SDL.ConvertEventToRenderCoordinates must expose native ref event mutations.");
    }

    public static void ViewportClipAndScaleFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        SDL3.SDL.Rect viewport = CreateRect(10, 20, 300, 200);
        SDL3.SDL.Rect clip = CreateRect(1, 2, 30, 40);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderViewportPointerNativeFunction", nameof(CaptureSetRenderViewportPointer)))
        {
            bool result = SDL3.SDL.SetRenderViewport((IntPtr)0x6001, (IntPtr)0x6002);

            TestAssert.Equal(true, result, "SDL.SetRenderViewport(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6001, capturedRenderer, "SDL.SetRenderViewport(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x6002, capturedRectPointer, "SDL.SetRenderViewport(IntPtr) must forward rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderViewportRectNativeFunction", nameof(CaptureSetRenderViewportRect)))
        {
            bool result = SDL3.SDL.SetRenderViewport((IntPtr)0x6011, viewport);

            TestAssert.Equal(true, result, "SDL.SetRenderViewport(Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6011, capturedRenderer, "SDL.SetRenderViewport(Rect) must forward renderer.");
            AssertRect(viewport, capturedRect, "SDL.SetRenderViewport(Rect) must forward rect.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(11, 22, 333, 222);
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderViewportNativeFunction", nameof(CaptureGetRenderViewport)))
        {
            bool result = SDL3.SDL.GetRenderViewport((IntPtr)0x6021, out SDL3.SDL.Rect rect);

            TestAssert.Equal(true, result, "SDL.GetRenderViewport must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6021, capturedRenderer, "SDL.GetRenderViewport must forward renderer.");
            AssertRect(nextRect, rect, "SDL.GetRenderViewport must output rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderViewportSetNativeFunction", nameof(CaptureRendererOnlyBool)))
        {
            bool result = SDL3.SDL.RenderViewportSet((IntPtr)0x6031);

            TestAssert.Equal(true, result, "SDL.RenderViewportSet must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6031, capturedRenderer, "SDL.RenderViewportSet must forward renderer.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(12, 23, 334, 223);
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderSafeAreaNativeFunction", nameof(CaptureGetRenderSafeArea)))
        {
            bool result = SDL3.SDL.GetRenderSafeArea((IntPtr)0x6041, out SDL3.SDL.Rect rect);

            TestAssert.Equal(true, result, "SDL.GetRenderSafeArea must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6041, capturedRenderer, "SDL.GetRenderSafeArea must forward renderer.");
            AssertRect(nextRect, rect, "SDL.GetRenderSafeArea must output rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderClipRectPointerNativeFunction", nameof(CaptureSetRenderClipRectPointer)))
        {
            bool result = SDL3.SDL.SetRenderClipRect((IntPtr)0x6051, (IntPtr)0x6052);

            TestAssert.Equal(true, result, "SDL.SetRenderClipRect(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6051, capturedRenderer, "SDL.SetRenderClipRect(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x6052, capturedRectPointer, "SDL.SetRenderClipRect(IntPtr) must forward rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderClipRectRectNativeFunction", nameof(CaptureSetRenderClipRectRect)))
        {
            bool result = SDL3.SDL.SetRenderClipRect((IntPtr)0x6061, in clip);

            TestAssert.Equal(true, result, "SDL.SetRenderClipRect(in Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6061, capturedRenderer, "SDL.SetRenderClipRect(in Rect) must forward renderer.");
            AssertRect(clip, capturedRect, "SDL.SetRenderClipRect(in Rect) must forward rect.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(13, 24, 335, 224);
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderClipRectNativeFunction", nameof(CaptureGetRenderClipRect)))
        {
            bool result = SDL3.SDL.GetRenderClipRect((IntPtr)0x6071, out SDL3.SDL.Rect rect);

            TestAssert.Equal(true, result, "SDL.GetRenderClipRect must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6071, capturedRenderer, "SDL.GetRenderClipRect must forward renderer.");
            AssertRect(nextRect, rect, "SDL.GetRenderClipRect must output rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderClipEnabledNativeFunction", nameof(CaptureRendererOnlyBool)))
        {
            bool result = SDL3.SDL.RenderClipEnabled((IntPtr)0x6081);

            TestAssert.Equal(true, result, "SDL.RenderClipEnabled must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6081, capturedRenderer, "SDL.RenderClipEnabled must forward renderer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderScaleNativeFunction", nameof(CaptureSetRenderScale)))
        {
            bool result = SDL3.SDL.SetRenderScale((IntPtr)0x6091, 1.25f, 2.5f);

            TestAssert.Equal(true, result, "SDL.SetRenderScale must return the native hook value.");
            TestAssert.Equal((IntPtr)0x6091, capturedRenderer, "SDL.SetRenderScale must forward renderer.");
            TestAssert.Equal(1.25f, capturedScaleX, "SDL.SetRenderScale must forward x scale.");
            TestAssert.Equal(2.5f, capturedScaleY, "SDL.SetRenderScale must forward y scale.");
        }

        ResetCaptureState();
        nextBool = true;
        nextScaleX = 3.5f;
        nextScaleY = 4.5f;
        using NativeHookScope scale = NativeHookScope.Install("GetRenderScaleNativeFunction", nameof(CaptureGetRenderScale));
        bool scaleResult = SDL3.SDL.GetRenderScale((IntPtr)0x60A1, out float scalex, out float scaley);

        TestAssert.Equal(true, scaleResult, "SDL.GetRenderScale must return the native hook value.");
        TestAssert.Equal((IntPtr)0x60A1, capturedRenderer, "SDL.GetRenderScale must forward renderer.");
        TestAssert.Equal(3.5f, scalex, "SDL.GetRenderScale must output x scale.");
        TestAssert.Equal(4.5f, scaley, "SDL.GetRenderScale must output y scale.");
    }

    public static void DrawColorBlendAndClearFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderDrawColorNativeFunction", nameof(CaptureSetRenderDrawColor)))
        {
            bool result = SDL3.SDL.SetRenderDrawColor((IntPtr)0x7001, 10, 20, 30, 40);

            TestAssert.Equal(true, result, "SDL.SetRenderDrawColor must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7001, capturedRenderer, "SDL.SetRenderDrawColor must forward renderer.");
            TestAssert.Equal((byte)10, capturedR, "SDL.SetRenderDrawColor must forward r.");
            TestAssert.Equal((byte)20, capturedG, "SDL.SetRenderDrawColor must forward g.");
            TestAssert.Equal((byte)30, capturedB, "SDL.SetRenderDrawColor must forward b.");
            TestAssert.Equal((byte)40, capturedAlpha, "SDL.SetRenderDrawColor must forward a.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderDrawColorFloatNativeFunction", nameof(CaptureSetRenderDrawColorFloat)))
        {
            bool result = SDL3.SDL.SetRenderDrawColorFloat((IntPtr)0x7011, 0.1f, 0.2f, 0.3f, 0.4f);

            TestAssert.Equal(true, result, "SDL.SetRenderDrawColorFloat must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7011, capturedRenderer, "SDL.SetRenderDrawColorFloat must forward renderer.");
            TestAssert.Equal(0.1f, capturedFR, "SDL.SetRenderDrawColorFloat must forward r.");
            TestAssert.Equal(0.2f, capturedFG, "SDL.SetRenderDrawColorFloat must forward g.");
            TestAssert.Equal(0.3f, capturedFB, "SDL.SetRenderDrawColorFloat must forward b.");
            TestAssert.Equal(0.4f, capturedFAlpha, "SDL.SetRenderDrawColorFloat must forward a.");
        }

        ResetCaptureState();
        nextBool = true;
        nextR = 50;
        nextG = 60;
        nextB = 70;
        nextAlpha = 80;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderDrawColorNativeFunction", nameof(CaptureGetRenderDrawColor)))
        {
            bool result = SDL3.SDL.GetRenderDrawColor((IntPtr)0x7021, out byte r, out byte g, out byte b, out byte a);

            TestAssert.Equal(true, result, "SDL.GetRenderDrawColor must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7021, capturedRenderer, "SDL.GetRenderDrawColor must forward renderer.");
            TestAssert.Equal((byte)50, r, "SDL.GetRenderDrawColor must output r.");
            TestAssert.Equal((byte)60, g, "SDL.GetRenderDrawColor must output g.");
            TestAssert.Equal((byte)70, b, "SDL.GetRenderDrawColor must output b.");
            TestAssert.Equal((byte)80, a, "SDL.GetRenderDrawColor must output a.");
        }

        ResetCaptureState();
        nextBool = true;
        nextFR = 0.5f;
        nextFG = 0.6f;
        nextFB = 0.7f;
        nextFAlpha = 0.8f;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderDrawColorFloatNativeFunction", nameof(CaptureGetRenderDrawColorFloat)))
        {
            bool result = SDL3.SDL.GetRenderDrawColorFloat((IntPtr)0x7031, out float r, out float g, out float b, out float a);

            TestAssert.Equal(true, result, "SDL.GetRenderDrawColorFloat must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7031, capturedRenderer, "SDL.GetRenderDrawColorFloat must forward renderer.");
            TestAssert.Equal(0.5f, r, "SDL.GetRenderDrawColorFloat must output r.");
            TestAssert.Equal(0.6f, g, "SDL.GetRenderDrawColorFloat must output g.");
            TestAssert.Equal(0.7f, b, "SDL.GetRenderDrawColorFloat must output b.");
            TestAssert.Equal(0.8f, a, "SDL.GetRenderDrawColorFloat must output a.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderColorScaleNativeFunction", nameof(CaptureSetRenderColorScale)))
        {
            bool result = SDL3.SDL.SetRenderColorScale((IntPtr)0x7041, 1.75f);

            TestAssert.Equal(true, result, "SDL.SetRenderColorScale must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7041, capturedRenderer, "SDL.SetRenderColorScale must forward renderer.");
            TestAssert.Equal(1.75f, capturedColorScale, "SDL.SetRenderColorScale must forward scale.");
        }

        ResetCaptureState();
        nextBool = true;
        nextColorScale = 2.25f;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderColorScaleNativeFunction", nameof(CaptureGetRenderColorScale)))
        {
            bool result = SDL3.SDL.GetRenderColorScale((IntPtr)0x7051, out float scale);

            TestAssert.Equal(true, result, "SDL.GetRenderColorScale must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7051, capturedRenderer, "SDL.GetRenderColorScale must forward renderer.");
            TestAssert.Equal(2.25f, scale, "SDL.GetRenderColorScale must output scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderDrawBlendModeNativeFunction", nameof(CaptureSetRenderDrawBlendMode)))
        {
            bool result = SDL3.SDL.SetRenderDrawBlendMode((IntPtr)0x7061, SDL3.SDL.BlendMode.Add);

            TestAssert.Equal(true, result, "SDL.SetRenderDrawBlendMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7061, capturedRenderer, "SDL.SetRenderDrawBlendMode must forward renderer.");
            TestAssert.Equal(SDL3.SDL.BlendMode.Add, capturedBlendMode, "SDL.SetRenderDrawBlendMode must forward blend mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextBlendMode = SDL3.SDL.BlendMode.Mod;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderDrawBlendModeNativeFunction", nameof(CaptureGetRenderDrawBlendMode)))
        {
            bool result = SDL3.SDL.GetRenderDrawBlendMode((IntPtr)0x7071, out SDL3.SDL.BlendMode blendMode);

            TestAssert.Equal(true, result, "SDL.GetRenderDrawBlendMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0x7071, capturedRenderer, "SDL.GetRenderDrawBlendMode must forward renderer.");
            TestAssert.Equal(SDL3.SDL.BlendMode.Mod, blendMode, "SDL.GetRenderDrawBlendMode must output blend mode.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope clear = NativeHookScope.Install("RenderClearNativeFunction", nameof(CaptureRendererOnlyBool));
        bool clearResult = SDL3.SDL.RenderClear((IntPtr)0x7081);

        TestAssert.Equal(true, clearResult, "SDL.RenderClear must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7081, capturedRenderer, "SDL.RenderClear must forward renderer.");
    }

    public static void PrimitiveRenderingFunctions_ForwardCoordinatesPointersArraysAndRects()
    {
        SDL3.SDL.FPoint[] points = [CreateFPoint(1.0f, 2.0f), CreateFPoint(3.0f, 4.0f)];
        SDL3.SDL.FRect rect = CreateFRect(5.0f, 6.0f, 7.0f, 8.0f);
        SDL3.SDL.FRect[] rects = [CreateFRect(9.0f, 10.0f, 11.0f, 12.0f), CreateFRect(13.0f, 14.0f, 15.0f, 16.0f)];

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderPointNativeFunction", nameof(CaptureRenderPoint)))
        {
            bool result = SDL3.SDL.RenderPoint((IntPtr)0x8001, 1.25f, 2.5f);

            TestAssert.Equal(true, result, "SDL.RenderPoint must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8001, capturedRenderer, "SDL.RenderPoint must forward renderer.");
            TestAssert.Equal(1.25f, capturedX, "SDL.RenderPoint must forward x.");
            TestAssert.Equal(2.5f, capturedY, "SDL.RenderPoint must forward y.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderPointsArrayNativeFunction", nameof(CaptureRenderPointsArray)))
        {
            bool result = SDL3.SDL.RenderPoints((IntPtr)0x8011, points, 2);

            TestAssert.Equal(true, result, "SDL.RenderPoints(FPoint[]) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8011, capturedRenderer, "SDL.RenderPoints(FPoint[]) must forward renderer.");
            AssertFPoints(points, capturedFPoints, "SDL.RenderPoints(FPoint[]) must forward points.");
            TestAssert.Equal(2, capturedCount, "SDL.RenderPoints(FPoint[]) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderPointsPointerNativeFunction", nameof(CaptureRenderPointsPointerSpan)))
        {
            bool result = SDL3.SDL.RenderPoints((IntPtr)0x8012, points.AsSpan(1, 1), 1);

            TestAssert.Equal(true, result, "SDL.RenderPoints(ReadOnlySpan<FPoint>) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8012, capturedRenderer, "SDL.RenderPoints(ReadOnlySpan<FPoint>) must forward renderer.");
            AssertFPoints([points[1]], capturedFPoints, "SDL.RenderPoints(ReadOnlySpan<FPoint>) must forward span slice.");
            TestAssert.Equal(1, capturedCount, "SDL.RenderPoints(ReadOnlySpan<FPoint>) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderPointsPointerNativeFunction", nameof(CaptureRenderPointsPointer)))
        {
            bool result = SDL3.SDL.RenderPoints((IntPtr)0x8021, (IntPtr)0x8022, 3);

            TestAssert.Equal(true, result, "SDL.RenderPoints(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8021, capturedRenderer, "SDL.RenderPoints(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x8022, capturedPointer, "SDL.RenderPoints(IntPtr) must forward points pointer.");
            TestAssert.Equal(3, capturedCount, "SDL.RenderPoints(IntPtr) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderLineNativeFunction", nameof(CaptureRenderLine)))
        {
            bool result = SDL3.SDL.RenderLine((IntPtr)0x8031, 1.0f, 2.0f, 3.0f, 4.0f);

            TestAssert.Equal(true, result, "SDL.RenderLine must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8031, capturedRenderer, "SDL.RenderLine must forward renderer.");
            TestAssert.Equal(1.0f, capturedX1, "SDL.RenderLine must forward x1.");
            TestAssert.Equal(2.0f, capturedY1, "SDL.RenderLine must forward y1.");
            TestAssert.Equal(3.0f, capturedX2, "SDL.RenderLine must forward x2.");
            TestAssert.Equal(4.0f, capturedY2, "SDL.RenderLine must forward y2.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderLinesArrayNativeFunction", nameof(CaptureRenderLinesArray)))
        {
            bool result = SDL3.SDL.RenderLines((IntPtr)0x8041, points, 2);

            TestAssert.Equal(true, result, "SDL.RenderLines(FPoint[]) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8041, capturedRenderer, "SDL.RenderLines(FPoint[]) must forward renderer.");
            AssertFPoints(points, capturedFPoints, "SDL.RenderLines(FPoint[]) must forward points.");
            TestAssert.Equal(2, capturedCount, "SDL.RenderLines(FPoint[]) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderLinesPointerNativeFunction", nameof(CaptureRenderLinesPointerSpan)))
        {
            bool result = SDL3.SDL.RenderLines((IntPtr)0x8042, points.AsSpan(0, 2), 2);

            TestAssert.Equal(true, result, "SDL.RenderLines(ReadOnlySpan<FPoint>) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8042, capturedRenderer, "SDL.RenderLines(ReadOnlySpan<FPoint>) must forward renderer.");
            AssertFPoints(points, capturedFPoints, "SDL.RenderLines(ReadOnlySpan<FPoint>) must forward span.");
            TestAssert.Equal(2, capturedCount, "SDL.RenderLines(ReadOnlySpan<FPoint>) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderLinesPointerNativeFunction", nameof(CaptureRenderLinesPointer)))
        {
            bool result = SDL3.SDL.RenderLines((IntPtr)0x8051, (IntPtr)0x8052, 3);

            TestAssert.Equal(true, result, "SDL.RenderLines(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8051, capturedRenderer, "SDL.RenderLines(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x8052, capturedPointer, "SDL.RenderLines(IntPtr) must forward points pointer.");
            TestAssert.Equal(3, capturedCount, "SDL.RenderLines(IntPtr) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderRectPointerNativeFunction", nameof(CaptureRenderRectPointer)))
        {
            bool result = SDL3.SDL.RenderRect((IntPtr)0x8061, (IntPtr)0x8062);

            TestAssert.Equal(true, result, "SDL.RenderRect(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8061, capturedRenderer, "SDL.RenderRect(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x8062, capturedPointer, "SDL.RenderRect(IntPtr) must forward rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderRectRectNativeFunction", nameof(CaptureRenderRectRect)))
        {
            bool result = SDL3.SDL.RenderRect((IntPtr)0x8071, in rect);

            TestAssert.Equal(true, result, "SDL.RenderRect(in FRect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8071, capturedRenderer, "SDL.RenderRect(in FRect) must forward renderer.");
            AssertFRect(rect, capturedFRect, "SDL.RenderRect(in FRect) must forward rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderRectsArrayNativeFunction", nameof(CaptureRenderRectsArray)))
        {
            bool result = SDL3.SDL.RenderRects((IntPtr)0x8081, rects, 2);

            TestAssert.Equal(true, result, "SDL.RenderRects(FRect[]) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8081, capturedRenderer, "SDL.RenderRects(FRect[]) must forward renderer.");
            AssertFRects(rects, capturedFRects, "SDL.RenderRects(FRect[]) must forward rects.");
            TestAssert.Equal(2, capturedCount, "SDL.RenderRects(FRect[]) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderRectsPointerNativeFunction", nameof(CaptureRenderRectsPointerSpan)))
        {
            bool result = SDL3.SDL.RenderRects((IntPtr)0x8082, rects.AsSpan(1, 1), 1);

            TestAssert.Equal(true, result, "SDL.RenderRects(ReadOnlySpan<FRect>) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8082, capturedRenderer, "SDL.RenderRects(ReadOnlySpan<FRect>) must forward renderer.");
            AssertFRects([rects[1]], capturedFRects, "SDL.RenderRects(ReadOnlySpan<FRect>) must forward span slice.");
            TestAssert.Equal(1, capturedCount, "SDL.RenderRects(ReadOnlySpan<FRect>) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderRectsPointerNativeFunction", nameof(CaptureRenderRectsPointer)))
        {
            bool result = SDL3.SDL.RenderRects((IntPtr)0x8091, (IntPtr)0x8092, 3);

            TestAssert.Equal(true, result, "SDL.RenderRects(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x8091, capturedRenderer, "SDL.RenderRects(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x8092, capturedPointer, "SDL.RenderRects(IntPtr) must forward rects pointer.");
            TestAssert.Equal(3, capturedCount, "SDL.RenderRects(IntPtr) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderFillRectPointerNativeFunction", nameof(CaptureRenderFillRectPointer)))
        {
            bool result = SDL3.SDL.RenderFillRect((IntPtr)0x80A1, (IntPtr)0x80A2);

            TestAssert.Equal(true, result, "SDL.RenderFillRect(IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x80A1, capturedRenderer, "SDL.RenderFillRect(IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x80A2, capturedPointer, "SDL.RenderFillRect(IntPtr) must forward rect pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderFillRectRectNativeFunction", nameof(CaptureRenderFillRectRect)))
        {
            bool result = SDL3.SDL.RenderFillRect((IntPtr)0x80B1, in rect);

            TestAssert.Equal(true, result, "SDL.RenderFillRect(in FRect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x80B1, capturedRenderer, "SDL.RenderFillRect(in FRect) must forward renderer.");
            AssertFRect(rect, capturedFRect, "SDL.RenderFillRect(in FRect) must forward rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderFillRectsArrayNativeFunction", nameof(CaptureRenderFillRectsArray)))
        {
            bool result = SDL3.SDL.RenderFillRects((IntPtr)0x80C1, rects, 2);

            TestAssert.Equal(true, result, "SDL.RenderFillRects(FRect[]) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x80C1, capturedRenderer, "SDL.RenderFillRects(FRect[]) must forward renderer.");
            AssertFRects(rects, capturedFRects, "SDL.RenderFillRects(FRect[]) must forward rects.");
            TestAssert.Equal(2, capturedCount, "SDL.RenderFillRects(FRect[]) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderFillRectsPointerNativeFunction", nameof(CaptureRenderFillRectsPointerSpan)))
        {
            bool result = SDL3.SDL.RenderFillRects((IntPtr)0x80C2, rects.AsSpan(), 2);

            TestAssert.Equal(true, result, "SDL.RenderFillRects(ReadOnlySpan<FRect>) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x80C2, capturedRenderer, "SDL.RenderFillRects(ReadOnlySpan<FRect>) must forward renderer.");
            AssertFRects(rects, capturedFRects, "SDL.RenderFillRects(ReadOnlySpan<FRect>) must forward span.");
            TestAssert.Equal(2, capturedCount, "SDL.RenderFillRects(ReadOnlySpan<FRect>) must forward count.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope fillRectsPointer = NativeHookScope.Install("RenderFillRectsPointerNativeFunction", nameof(CaptureRenderFillRectsPointer));
        bool fillRectsPointerResult = SDL3.SDL.RenderFillRects((IntPtr)0x80D1, (IntPtr)0x80D2, 3);

        TestAssert.Equal(true, fillRectsPointerResult, "SDL.RenderFillRects(IntPtr) must return the native hook value.");
        TestAssert.Equal((IntPtr)0x80D1, capturedRenderer, "SDL.RenderFillRects(IntPtr) must forward renderer.");
        TestAssert.Equal((IntPtr)0x80D2, capturedPointer, "SDL.RenderFillRects(IntPtr) must forward rects pointer.");
        TestAssert.Equal(3, capturedCount, "SDL.RenderFillRects(IntPtr) must forward count.");
    }

    public static void TextureRenderingFunctions_ForwardTextureRectsRotationCenterAndFlip()
    {
        SDL3.SDL.FRect source = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect destination = CreateFRect(5.0f, 6.0f, 7.0f, 8.0f);
        SDL3.SDL.FPoint center = CreateFPoint(9.0f, 10.0f);

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexturePointersNativeFunction", nameof(CaptureRenderTexturePointers)))
        {
            bool result = SDL3.SDL.RenderTexture((IntPtr)0x9001, (IntPtr)0x9002, (IntPtr)0x9003, (IntPtr)0x9004);

            TestAssert.Equal(true, result, "SDL.RenderTexture(IntPtr, IntPtr) must return the native hook value.");
            AssertTextureRenderPointers((IntPtr)0x9001, (IntPtr)0x9002, (IntPtr)0x9003, (IntPtr)0x9004, "SDL.RenderTexture(IntPtr, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureSourceRectNativeFunction", nameof(CaptureRenderTextureSourceRect)))
        {
            bool result = SDL3.SDL.RenderTexture((IntPtr)0x9011, (IntPtr)0x9012, in source, (IntPtr)0x9014);

            TestAssert.Equal(true, result, "SDL.RenderTexture(in FRect, IntPtr) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x9011, capturedRenderer, "SDL.RenderTexture(in FRect, IntPtr) must forward renderer.");
            TestAssert.Equal((IntPtr)0x9012, capturedTexture, "SDL.RenderTexture(in FRect, IntPtr) must forward texture.");
            AssertFRect(source, capturedSourceFRect, "SDL.RenderTexture(in FRect, IntPtr) must forward source rect.");
            TestAssert.Equal((IntPtr)0x9014, capturedDestinationRectPointer, "SDL.RenderTexture(in FRect, IntPtr) must forward destination pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureDestinationRectNativeFunction", nameof(CaptureRenderTextureDestinationRect)))
        {
            bool result = SDL3.SDL.RenderTexture((IntPtr)0x9021, (IntPtr)0x9022, (IntPtr)0x9023, in destination);

            TestAssert.Equal(true, result, "SDL.RenderTexture(IntPtr, in FRect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x9021, capturedRenderer, "SDL.RenderTexture(IntPtr, in FRect) must forward renderer.");
            TestAssert.Equal((IntPtr)0x9022, capturedTexture, "SDL.RenderTexture(IntPtr, in FRect) must forward texture.");
            TestAssert.Equal((IntPtr)0x9023, capturedSourceRectPointer, "SDL.RenderTexture(IntPtr, in FRect) must forward source pointer.");
            AssertFRect(destination, capturedDestinationFRect, "SDL.RenderTexture(IntPtr, in FRect) must forward destination rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRectsNativeFunction", nameof(CaptureRenderTextureRects)))
        {
            bool result = SDL3.SDL.RenderTexture((IntPtr)0x9031, (IntPtr)0x9032, in source, in destination);

            TestAssert.Equal(true, result, "SDL.RenderTexture(in FRect, in FRect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0x9031, capturedRenderer, "SDL.RenderTexture(in FRect, in FRect) must forward renderer.");
            TestAssert.Equal((IntPtr)0x9032, capturedTexture, "SDL.RenderTexture(in FRect, in FRect) must forward texture.");
            AssertFRect(source, capturedSourceFRect, "SDL.RenderTexture(in FRect, in FRect) must forward source rect.");
            AssertFRect(destination, capturedDestinationFRect, "SDL.RenderTexture(in FRect, in FRect) must forward destination rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedPointersNativeFunction", nameof(CaptureRenderTextureRotatedPointers)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x9041, (IntPtr)0x9042, (IntPtr)0x9043, (IntPtr)0x9044, 45.0, (IntPtr)0x9045, SDL3.SDL.FlipMode.Horizontal);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(IntPtr, IntPtr, IntPtr) must return the native hook value.");
            AssertTextureRenderPointers((IntPtr)0x9041, (IntPtr)0x9042, (IntPtr)0x9043, (IntPtr)0x9044, "SDL.RenderTextureRotated(IntPtr, IntPtr, IntPtr)");
            AssertRotated(45.0, (IntPtr)0x9045, SDL3.SDL.FlipMode.Horizontal, "SDL.RenderTextureRotated(IntPtr, IntPtr, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedSourceRectNativeFunction", nameof(CaptureRenderTextureRotatedSourceRect)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x9051, (IntPtr)0x9052, in source, (IntPtr)0x9054, 46.0, (IntPtr)0x9055, SDL3.SDL.FlipMode.Vertical);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(in FRect, IntPtr, IntPtr) must return the native hook value.");
            AssertTextureSourceRectAndDestinationPointer((IntPtr)0x9051, (IntPtr)0x9052, source, (IntPtr)0x9054, "SDL.RenderTextureRotated(in FRect, IntPtr, IntPtr)");
            AssertRotated(46.0, (IntPtr)0x9055, SDL3.SDL.FlipMode.Vertical, "SDL.RenderTextureRotated(in FRect, IntPtr, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedDestinationRectNativeFunction", nameof(CaptureRenderTextureRotatedDestinationRect)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x9061, (IntPtr)0x9062, (IntPtr)0x9063, in destination, 47.0, (IntPtr)0x9065, SDL3.SDL.FlipMode.HorizontalAndVertical);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(IntPtr, in FRect, IntPtr) must return the native hook value.");
            AssertTextureSourcePointerAndDestinationRect((IntPtr)0x9061, (IntPtr)0x9062, (IntPtr)0x9063, destination, "SDL.RenderTextureRotated(IntPtr, in FRect, IntPtr)");
            AssertRotated(47.0, (IntPtr)0x9065, SDL3.SDL.FlipMode.HorizontalAndVertical, "SDL.RenderTextureRotated(IntPtr, in FRect, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedCenterPointNativeFunction", nameof(CaptureRenderTextureRotatedCenterPoint)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x9071, (IntPtr)0x9072, (IntPtr)0x9073, (IntPtr)0x9074, 48.0, in center, SDL3.SDL.FlipMode.None);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(IntPtr, IntPtr, in FPoint) must return the native hook value.");
            AssertTextureRenderPointers((IntPtr)0x9071, (IntPtr)0x9072, (IntPtr)0x9073, (IntPtr)0x9074, "SDL.RenderTextureRotated(IntPtr, IntPtr, in FPoint)");
            AssertRotated(48.0, IntPtr.Zero, SDL3.SDL.FlipMode.None, "SDL.RenderTextureRotated(IntPtr, IntPtr, in FPoint)");
            AssertFPoint(center, capturedCenterFPoint, "SDL.RenderTextureRotated(IntPtr, IntPtr, in FPoint) must forward center.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedRectsNativeFunction", nameof(CaptureRenderTextureRotatedRects)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x9081, (IntPtr)0x9082, in source, in destination, 49.0, (IntPtr)0x9085, SDL3.SDL.FlipMode.Horizontal);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(in FRect, in FRect, IntPtr) must return the native hook value.");
            AssertTextureRects((IntPtr)0x9081, (IntPtr)0x9082, source, destination, "SDL.RenderTextureRotated(in FRect, in FRect, IntPtr)");
            AssertRotated(49.0, (IntPtr)0x9085, SDL3.SDL.FlipMode.Horizontal, "SDL.RenderTextureRotated(in FRect, in FRect, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedDestinationRectCenterPointNativeFunction", nameof(CaptureRenderTextureRotatedDestinationRectCenterPoint)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x9091, (IntPtr)0x9092, (IntPtr)0x9093, in destination, 50.0, in center, SDL3.SDL.FlipMode.Vertical);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(IntPtr, in FRect, in FPoint) must return the native hook value.");
            AssertTextureSourcePointerAndDestinationRect((IntPtr)0x9091, (IntPtr)0x9092, (IntPtr)0x9093, destination, "SDL.RenderTextureRotated(IntPtr, in FRect, in FPoint)");
            AssertRotated(50.0, IntPtr.Zero, SDL3.SDL.FlipMode.Vertical, "SDL.RenderTextureRotated(IntPtr, in FRect, in FPoint)");
            AssertFPoint(center, capturedCenterFPoint, "SDL.RenderTextureRotated(IntPtr, in FRect, in FPoint) must forward center.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureRotatedSourceRectCenterPointNativeFunction", nameof(CaptureRenderTextureRotatedSourceRectCenterPoint)))
        {
            bool result = SDL3.SDL.RenderTextureRotated((IntPtr)0x90A1, (IntPtr)0x90A2, in source, (IntPtr)0x90A4, 51.0, in center, SDL3.SDL.FlipMode.HorizontalAndVertical);

            TestAssert.Equal(true, result, "SDL.RenderTextureRotated(in FRect, IntPtr, in FPoint) must return the native hook value.");
            AssertTextureSourceRectAndDestinationPointer((IntPtr)0x90A1, (IntPtr)0x90A2, source, (IntPtr)0x90A4, "SDL.RenderTextureRotated(in FRect, IntPtr, in FPoint)");
            AssertRotated(51.0, IntPtr.Zero, SDL3.SDL.FlipMode.HorizontalAndVertical, "SDL.RenderTextureRotated(in FRect, IntPtr, in FPoint)");
            AssertFPoint(center, capturedCenterFPoint, "SDL.RenderTextureRotated(in FRect, IntPtr, in FPoint) must forward center.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope rectsCenter = NativeHookScope.Install("RenderTextureRotatedRectsCenterPointNativeFunction", nameof(CaptureRenderTextureRotatedRectsCenterPoint));
        bool rectsCenterResult = SDL3.SDL.RenderTextureRotated((IntPtr)0x90B1, (IntPtr)0x90B2, in source, in destination, 52.0, in center, SDL3.SDL.FlipMode.None);

        TestAssert.Equal(true, rectsCenterResult, "SDL.RenderTextureRotated(in FRect, in FRect, in FPoint) must return the native hook value.");
        AssertTextureRects((IntPtr)0x90B1, (IntPtr)0x90B2, source, destination, "SDL.RenderTextureRotated(in FRect, in FRect, in FPoint)");
        AssertRotated(52.0, IntPtr.Zero, SDL3.SDL.FlipMode.None, "SDL.RenderTextureRotated(in FRect, in FRect, in FPoint)");
        AssertFPoint(center, capturedCenterFPoint, "SDL.RenderTextureRotated(in FRect, in FRect, in FPoint) must forward center.");
    }

    public static void TextureAffineRenderingFunctions_ForwardPointerAndRectCombinations()
    {
        IntPtr renderer = (IntPtr)0xA001;
        IntPtr texture = (IntPtr)0xA002;
        IntPtr sourcePointer = (IntPtr)0xA003;
        IntPtr originPointer = (IntPtr)0xA004;
        IntPtr rightPointer = (IntPtr)0xA005;
        IntPtr downPointer = (IntPtr)0xA006;
        SDL3.SDL.FRect source = CreateFRect(1.5f, 2.5f, 3.5f, 4.5f);
        SDL3.SDL.FRect origin = CreateFRect(5.5f, 6.5f, 7.5f, 8.5f);
        SDL3.SDL.FRect right = CreateFRect(9.5f, 10.5f, 11.5f, 12.5f);
        SDL3.SDL.FRect down = CreateFRect(13.5f, 14.5f, 15.5f, 16.5f);

        void RunAffineCase(
            string fieldName,
            string methodName,
            Func<bool> invoke,
            bool sourceUsesRect,
            bool originUsesRect,
            bool rightUsesRect,
            bool downUsesRect,
            string message)
        {
            ResetCaptureState();
            nextBool = true;

            using NativeHookScope _ = NativeHookScope.Install(fieldName, methodName);
            bool result = invoke();

            TestAssert.Equal(true, result, $"{message} must return the native hook value.");
            AssertAffineState(
                renderer,
                texture,
                sourceUsesRect,
                sourcePointer,
                source,
                originUsesRect,
                originPointer,
                origin,
                rightUsesRect,
                rightPointer,
                right,
                downUsesRect,
                downPointer,
                down,
                message);
        }

        RunAffineCase(
            "RenderTextureAffinePointersNativeFunction",
            nameof(CaptureRenderTextureAffinePointers),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, originPointer, rightPointer, downPointer),
            false,
            false,
            false,
            false,
            "SDL.RenderTextureAffine(IntPtr, IntPtr, IntPtr, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineDownRectNativeFunction",
            nameof(CaptureRenderTextureAffineDownRect),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, originPointer, rightPointer, in down),
            false,
            false,
            false,
            true,
            "SDL.RenderTextureAffine(IntPtr, IntPtr, IntPtr, in FRect)");

        RunAffineCase(
            "RenderTextureAffineRightRectNativeFunction",
            nameof(CaptureRenderTextureAffineRightRect),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, originPointer, in right, downPointer),
            false,
            false,
            true,
            false,
            "SDL.RenderTextureAffine(IntPtr, IntPtr, in FRect, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineRightDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineRightDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, originPointer, in right, in down),
            false,
            false,
            true,
            true,
            "SDL.RenderTextureAffine(IntPtr, IntPtr, in FRect, in FRect)");

        RunAffineCase(
            "RenderTextureAffineOriginRectNativeFunction",
            nameof(CaptureRenderTextureAffineOriginRect),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, in origin, rightPointer, downPointer),
            false,
            true,
            false,
            false,
            "SDL.RenderTextureAffine(IntPtr, in FRect, IntPtr, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineOriginDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineOriginDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, in origin, rightPointer, in down),
            false,
            true,
            false,
            true,
            "SDL.RenderTextureAffine(IntPtr, in FRect, IntPtr, in FRect)");

        RunAffineCase(
            "RenderTextureAffineOriginRightRectsNativeFunction",
            nameof(CaptureRenderTextureAffineOriginRightRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, in origin, in right, downPointer),
            false,
            true,
            true,
            false,
            "SDL.RenderTextureAffine(IntPtr, in FRect, in FRect, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineOriginRightDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineOriginRightDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, sourcePointer, in origin, in right, in down),
            false,
            true,
            true,
            true,
            "SDL.RenderTextureAffine(IntPtr, in FRect, in FRect, in FRect)");

        RunAffineCase(
            "RenderTextureAffineSourceRectNativeFunction",
            nameof(CaptureRenderTextureAffineSourceRect),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, originPointer, rightPointer, downPointer),
            true,
            false,
            false,
            false,
            "SDL.RenderTextureAffine(in FRect, IntPtr, IntPtr, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineSourceDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, originPointer, rightPointer, in down),
            true,
            false,
            false,
            true,
            "SDL.RenderTextureAffine(in FRect, IntPtr, IntPtr, in FRect)");

        RunAffineCase(
            "RenderTextureAffineSourceRightRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceRightRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, originPointer, in right, downPointer),
            true,
            false,
            true,
            false,
            "SDL.RenderTextureAffine(in FRect, IntPtr, in FRect, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineSourceRightDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceRightDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, originPointer, in right, in down),
            true,
            false,
            true,
            true,
            "SDL.RenderTextureAffine(in FRect, IntPtr, in FRect, in FRect)");

        RunAffineCase(
            "RenderTextureAffineSourceOriginRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceOriginRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, in origin, rightPointer, downPointer),
            true,
            true,
            false,
            false,
            "SDL.RenderTextureAffine(in FRect, in FRect, IntPtr, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineSourceOriginDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceOriginDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, in origin, rightPointer, in down),
            true,
            true,
            false,
            true,
            "SDL.RenderTextureAffine(in FRect, in FRect, IntPtr, in FRect)");

        RunAffineCase(
            "RenderTextureAffineSourceOriginRightRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceOriginRightRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, in origin, in right, downPointer),
            true,
            true,
            true,
            false,
            "SDL.RenderTextureAffine(in FRect, in FRect, in FRect, IntPtr)");

        RunAffineCase(
            "RenderTextureAffineSourceOriginRightDownRectsNativeFunction",
            nameof(CaptureRenderTextureAffineSourceOriginRightDownRects),
            () => SDL3.SDL.RenderTextureAffine(renderer, texture, in source, in origin, in right, in down),
            true,
            true,
            true,
            true,
            "SDL.RenderTextureAffine(in FRect, in FRect, in FRect, in FRect)");
    }

    public static void TextureTiledRenderingFunctions_ForwardTextureRectsAndScale()
    {
        SDL3.SDL.FRect source = CreateFRect(1.25f, 2.25f, 3.25f, 4.25f);
        SDL3.SDL.FRect destination = CreateFRect(5.25f, 6.25f, 7.25f, 8.25f);
        const float scale = 2.5f;

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureTiledPointersNativeFunction", nameof(CaptureRenderTextureTiledPointers)))
        {
            bool result = SDL3.SDL.RenderTextureTiled((IntPtr)0xB001, (IntPtr)0xB002, (IntPtr)0xB003, scale, (IntPtr)0xB004);

            TestAssert.Equal(true, result, "SDL.RenderTextureTiled(IntPtr, IntPtr) must return the native hook value.");
            AssertTextureRenderPointers((IntPtr)0xB001, (IntPtr)0xB002, (IntPtr)0xB003, (IntPtr)0xB004, "SDL.RenderTextureTiled(IntPtr, IntPtr)");
            TestAssert.Equal(scale, capturedScale, "SDL.RenderTextureTiled(IntPtr, IntPtr) must forward scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureTiledSourceRectNativeFunction", nameof(CaptureRenderTextureTiledSourceRect)))
        {
            bool result = SDL3.SDL.RenderTextureTiled((IntPtr)0xB011, (IntPtr)0xB012, in source, scale, (IntPtr)0xB014);

            TestAssert.Equal(true, result, "SDL.RenderTextureTiled(in FRect, IntPtr) must return the native hook value.");
            AssertTextureSourceRectAndDestinationPointer((IntPtr)0xB011, (IntPtr)0xB012, source, (IntPtr)0xB014, "SDL.RenderTextureTiled(in FRect, IntPtr)");
            TestAssert.Equal(scale, capturedScale, "SDL.RenderTextureTiled(in FRect, IntPtr) must forward scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTextureTiledDestinationRectNativeFunction", nameof(CaptureRenderTextureTiledDestinationRect)))
        {
            bool result = SDL3.SDL.RenderTextureTiled((IntPtr)0xB021, (IntPtr)0xB022, (IntPtr)0xB023, scale, in destination);

            TestAssert.Equal(true, result, "SDL.RenderTextureTiled(IntPtr, in FRect) must return the native hook value.");
            AssertTextureSourcePointerAndDestinationRect((IntPtr)0xB021, (IntPtr)0xB022, (IntPtr)0xB023, destination, "SDL.RenderTextureTiled(IntPtr, in FRect)");
            TestAssert.Equal(scale, capturedScale, "SDL.RenderTextureTiled(IntPtr, in FRect) must forward scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope rects = NativeHookScope.Install("RenderTextureTiledRectsNativeFunction", nameof(CaptureRenderTextureTiledRects));
        bool rectsResult = SDL3.SDL.RenderTextureTiled((IntPtr)0xB031, (IntPtr)0xB032, in source, scale, in destination);

        TestAssert.Equal(true, rectsResult, "SDL.RenderTextureTiled(in FRect, in FRect) must return the native hook value.");
        AssertTextureRects((IntPtr)0xB031, (IntPtr)0xB032, source, destination, "SDL.RenderTextureTiled(in FRect, in FRect)");
        TestAssert.Equal(scale, capturedScale, "SDL.RenderTextureTiled(in FRect, in FRect) must forward scale.");
    }

    public static void Texture9GridRenderingFunctions_ForwardTextureRectsScalesAndTileScale()
    {
        SDL3.SDL.FRect source = CreateFRect(1.75f, 2.75f, 3.75f, 4.75f);
        SDL3.SDL.FRect destination = CreateFRect(5.75f, 6.75f, 7.75f, 8.75f);
        const float leftWidth = 1.0f;
        const float rightWidth = 2.0f;
        const float topHeight = 3.0f;
        const float bottomHeight = 4.0f;
        const float scale = 5.0f;
        const float tileScale = 6.0f;

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexture9GridSourceRectNativeFunction", nameof(CaptureRenderTexture9GridSourceRect)))
        {
            bool result = SDL3.SDL.RenderTexture9Grid((IntPtr)0xC001, (IntPtr)0xC002, in source, leftWidth, rightWidth, topHeight, bottomHeight, scale, (IntPtr)0xC004);

            TestAssert.Equal(true, result, "SDL.RenderTexture9Grid(in FRect, IntPtr) must return the native hook value.");
            AssertTextureSourceRectAndDestinationPointer((IntPtr)0xC001, (IntPtr)0xC002, source, (IntPtr)0xC004, "SDL.RenderTexture9Grid(in FRect, IntPtr)");
            AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9Grid(in FRect, IntPtr)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexture9GridDestinationRectNativeFunction", nameof(CaptureRenderTexture9GridDestinationRect)))
        {
            bool result = SDL3.SDL.RenderTexture9Grid((IntPtr)0xC011, (IntPtr)0xC012, (IntPtr)0xC013, leftWidth, rightWidth, topHeight, bottomHeight, scale, in destination);

            TestAssert.Equal(true, result, "SDL.RenderTexture9Grid(IntPtr, in FRect) must return the native hook value.");
            AssertTextureSourcePointerAndDestinationRect((IntPtr)0xC011, (IntPtr)0xC012, (IntPtr)0xC013, destination, "SDL.RenderTexture9Grid(IntPtr, in FRect)");
            AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9Grid(IntPtr, in FRect)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexture9GridRectsNativeFunction", nameof(CaptureRenderTexture9GridRects)))
        {
            bool result = SDL3.SDL.RenderTexture9Grid((IntPtr)0xC021, (IntPtr)0xC022, in source, leftWidth, rightWidth, topHeight, bottomHeight, scale, in destination);

            TestAssert.Equal(true, result, "SDL.RenderTexture9Grid(in FRect, in FRect) must return the native hook value.");
            AssertTextureRects((IntPtr)0xC021, (IntPtr)0xC022, source, destination, "SDL.RenderTexture9Grid(in FRect, in FRect)");
            AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9Grid(in FRect, in FRect)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexture9GridTiledPointersNativeFunction", nameof(CaptureRenderTexture9GridTiledPointers)))
        {
            bool result = SDL3.SDL.RenderTexture9GridTiled((IntPtr)0xC031, (IntPtr)0xC032, (IntPtr)0xC033, leftWidth, rightWidth, topHeight, bottomHeight, scale, (IntPtr)0xC034, tileScale);

            TestAssert.Equal(true, result, "SDL.RenderTexture9GridTiled(IntPtr, IntPtr) must return the native hook value.");
            AssertTextureRenderPointers((IntPtr)0xC031, (IntPtr)0xC032, (IntPtr)0xC033, (IntPtr)0xC034, "SDL.RenderTexture9GridTiled(IntPtr, IntPtr)");
            AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9GridTiled(IntPtr, IntPtr)");
            TestAssert.Equal(tileScale, capturedTileScale, "SDL.RenderTexture9GridTiled(IntPtr, IntPtr) must forward tile scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexture9GridTiledSourceRectNativeFunction", nameof(CaptureRenderTexture9GridTiledSourceRect)))
        {
            bool result = SDL3.SDL.RenderTexture9GridTiled((IntPtr)0xC041, (IntPtr)0xC042, in source, leftWidth, rightWidth, topHeight, bottomHeight, scale, (IntPtr)0xC044, tileScale);

            TestAssert.Equal(true, result, "SDL.RenderTexture9GridTiled(in FRect, IntPtr) must return the native hook value.");
            AssertTextureSourceRectAndDestinationPointer((IntPtr)0xC041, (IntPtr)0xC042, source, (IntPtr)0xC044, "SDL.RenderTexture9GridTiled(in FRect, IntPtr)");
            AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9GridTiled(in FRect, IntPtr)");
            TestAssert.Equal(tileScale, capturedTileScale, "SDL.RenderTexture9GridTiled(in FRect, IntPtr) must forward tile scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderTexture9GridTiledDestinationRectNativeFunction", nameof(CaptureRenderTexture9GridTiledDestinationRect)))
        {
            bool result = SDL3.SDL.RenderTexture9GridTiled((IntPtr)0xC051, (IntPtr)0xC052, (IntPtr)0xC053, leftWidth, rightWidth, topHeight, bottomHeight, scale, in destination, tileScale);

            TestAssert.Equal(true, result, "SDL.RenderTexture9GridTiled(IntPtr, in FRect) must return the native hook value.");
            AssertTextureSourcePointerAndDestinationRect((IntPtr)0xC051, (IntPtr)0xC052, (IntPtr)0xC053, destination, "SDL.RenderTexture9GridTiled(IntPtr, in FRect)");
            AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9GridTiled(IntPtr, in FRect)");
            TestAssert.Equal(tileScale, capturedTileScale, "SDL.RenderTexture9GridTiled(IntPtr, in FRect) must forward tile scale.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope rects = NativeHookScope.Install("RenderTexture9GridTiledRectsNativeFunction", nameof(CaptureRenderTexture9GridTiledRects));
        bool rectsResult = SDL3.SDL.RenderTexture9GridTiled((IntPtr)0xC061, (IntPtr)0xC062, in source, leftWidth, rightWidth, topHeight, bottomHeight, scale, in destination, tileScale);

        TestAssert.Equal(true, rectsResult, "SDL.RenderTexture9GridTiled(in FRect, in FRect) must return the native hook value.");
        AssertTextureRects((IntPtr)0xC061, (IntPtr)0xC062, source, destination, "SDL.RenderTexture9GridTiled(in FRect, in FRect)");
        AssertGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale, "SDL.RenderTexture9GridTiled(in FRect, in FRect)");
        TestAssert.Equal(tileScale, capturedTileScale, "SDL.RenderTexture9GridTiled(in FRect, in FRect) must forward tile scale.");
    }

    public static void GeometryRenderingFunctions_ForwardVerticesRawArraysAndGenericSpans()
    {
        SDL3.SDL.Vertex[] vertices =
        [
            CreateVertex(1.0f, 2.0f, 0.1f, 0.2f, 0.3f, 0.4f, 3.0f, 4.0f),
            CreateVertex(5.0f, 6.0f, 0.5f, 0.6f, 0.7f, 0.8f, 7.0f, 8.0f),
            CreateVertex(9.0f, 10.0f, 0.9f, 1.0f, 1.1f, 1.2f, 11.0f, 12.0f),
        ];
        int[] intIndices = [0, 1, 2];
        byte[] byteIndices = [0, 1, 2];
        float[] xy = [1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f];
        SDL3.SDL.FColor[] colors =
        [
            new(0.1f, 0.2f, 0.3f, 0.4f),
            new(0.5f, 0.6f, 0.7f, 0.8f),
            new(0.9f, 1.0f, 1.1f, 1.2f),
        ];
        float[] uv = [0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f];

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryPointerIndicesNativeFunction", nameof(CaptureRenderGeometryPointerIndices)))
        {
            bool result = SDL3.SDL.RenderGeometry((IntPtr)0xD001, (IntPtr)0xD002, vertices, 3, (IntPtr)0xD003, 3);

            TestAssert.Equal(true, result, "SDL.RenderGeometry(IntPtr indices) must return the native hook value.");
            AssertGeometryBase((IntPtr)0xD001, (IntPtr)0xD002, 3, 3, "SDL.RenderGeometry(IntPtr indices)");
            AssertVertices(vertices, capturedVertices, "SDL.RenderGeometry(IntPtr indices) must forward vertices.");
            TestAssert.Equal((IntPtr)0xD003, capturedIndicesPointer, "SDL.RenderGeometry(IntPtr indices) must forward indices pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        int[] pointerIndices = [7, 8];
        IntPtr nativePointerIndices = Marshal.AllocHGlobal(sizeof(int) * pointerIndices.Length);
        try
        {
            Marshal.Copy(pointerIndices, 0, nativePointerIndices, pointerIndices.Length);
            using NativeHookScope _ = NativeHookScope.Install("RenderGeometryPointersNativeFunction", nameof(CaptureRenderGeometryPointers));
            bool result = SDL3.SDL.RenderGeometry((IntPtr)0xD004, (IntPtr)0xD005, vertices.AsSpan(1, 2), 2, nativePointerIndices, 2);

            TestAssert.Equal(true, result, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, IntPtr indices) must return the native hook value.");
            AssertGeometryBase((IntPtr)0xD004, (IntPtr)0xD005, 2, 2, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, IntPtr indices)");
            AssertVertices([vertices[1], vertices[2]], capturedVertices, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, IntPtr indices) must forward vertex span slice.");
            TestAssert.Equal(nativePointerIndices, capturedIndicesPointer, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, IntPtr indices) must forward indices pointer.");
            AssertInts(pointerIndices, capturedIntIndices, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, IntPtr indices) must forward unmanaged indices.");
        }
        finally
        {
            Marshal.FreeHGlobal(nativePointerIndices);
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryArrayIndicesNativeFunction", nameof(CaptureRenderGeometryArrayIndices)))
        {
            bool result = SDL3.SDL.RenderGeometry((IntPtr)0xD011, (IntPtr)0xD012, vertices, 3, intIndices, 3);

            TestAssert.Equal(true, result, "SDL.RenderGeometry(int[] indices) must return the native hook value.");
            AssertGeometryBase((IntPtr)0xD011, (IntPtr)0xD012, 3, 3, "SDL.RenderGeometry(int[] indices)");
            AssertVertices(vertices, capturedVertices, "SDL.RenderGeometry(int[] indices) must forward vertices.");
            AssertInts(intIndices, capturedIntIndices, "SDL.RenderGeometry(int[] indices) must forward indices.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryPointersNativeFunction", nameof(CaptureRenderGeometryPointers)))
        {
            bool result = SDL3.SDL.RenderGeometry((IntPtr)0xD014, (IntPtr)0xD015, vertices.AsSpan(), 3, intIndices.AsSpan(1, 2), 2);

            TestAssert.Equal(true, result, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, ReadOnlySpan<int>) must return the native hook value.");
            AssertGeometryBase((IntPtr)0xD014, (IntPtr)0xD015, 3, 2, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, ReadOnlySpan<int>)");
            AssertVertices(vertices, capturedVertices, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, ReadOnlySpan<int>) must forward vertices.");
            AssertInts([1, 2], capturedIntIndices, "SDL.RenderGeometry(ReadOnlySpan<Vertex>, ReadOnlySpan<int>) must forward index span slice.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryRawPointersNativeFunction", nameof(CaptureRenderGeometryRawPointers)))
        {
            bool result = SDL3.SDL.RenderGeometryRaw((IntPtr)0xD021, (IntPtr)0xD022, (IntPtr)0xD023, 8, (IntPtr)0xD024, 16, (IntPtr)0xD025, 8, 3, (IntPtr)0xD026, 3, 4);

            TestAssert.Equal(true, result, "SDL.RenderGeometryRaw(pointer indices) must return the native hook value.");
            AssertRawPointerGeometry((IntPtr)0xD021, (IntPtr)0xD022, (IntPtr)0xD023, 8, (IntPtr)0xD024, 16, (IntPtr)0xD025, 8, 3, (IntPtr)0xD026, 3, 4, "SDL.RenderGeometryRaw(pointer indices)");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryRawPointerIndicesNativeFunction", nameof(CaptureRenderGeometryRawPointerIndices)))
        {
            bool result = SDL3.SDL.RenderGeometryRaw((IntPtr)0xD031, (IntPtr)0xD032, xy, 8, colors, Marshal.SizeOf<SDL3.SDL.FColor>(), uv, 8, 3, (IntPtr)0xD036, 3, 4);

            TestAssert.Equal(true, result, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices) must return the native hook value.");
            AssertGeometryBase((IntPtr)0xD031, (IntPtr)0xD032, 3, 3, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices)");
            AssertRawArrayGeometry(xy, colors, uv, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices)");
            TestAssert.Equal(8, capturedXYStride, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices) must forward xy stride.");
            TestAssert.Equal(Marshal.SizeOf<SDL3.SDL.FColor>(), capturedColorStride, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices) must forward color stride.");
            TestAssert.Equal(8, capturedUVStride, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices) must forward uv stride.");
            TestAssert.Equal((IntPtr)0xD036, capturedIndicesPointer, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices) must forward indices pointer.");
            TestAssert.Equal(4, capturedSizeIndices, "SDL.RenderGeometryRaw(float[]/FColor[]/IntPtr indices) must forward index size.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryRawByteIndicesNativeFunction", nameof(CaptureRenderGeometryRawByteIndices)))
        {
            bool result = SDL3.SDL.RenderGeometryRaw((IntPtr)0xD041, (IntPtr)0xD042, xy, 8, colors, Marshal.SizeOf<SDL3.SDL.FColor>(), uv, 8, 3, byteIndices, 3, 1);

            TestAssert.Equal(true, result, "SDL.RenderGeometryRaw(byte[] indices) must return the native hook value.");
            AssertGeometryBase((IntPtr)0xD041, (IntPtr)0xD042, 3, 3, "SDL.RenderGeometryRaw(byte[] indices)");
            AssertRawArrayGeometry(xy, colors, uv, "SDL.RenderGeometryRaw(byte[] indices)");
            AssertBytes(byteIndices, capturedByteIndices, "SDL.RenderGeometryRaw(byte[] indices) must forward indices.");
            TestAssert.Equal(1, capturedSizeIndices, "SDL.RenderGeometryRaw(byte[] indices) must forward index size.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderGeometryRawPointersNativeFunction", nameof(CaptureRenderGeometryRawPointers)))
        {
            bool result;
            unsafe
            {
                result = SDL3.SDL.RenderGeometryRaw<int>((IntPtr)0xD051, (IntPtr)0xD052, xy, 8, colors, Marshal.SizeOf<SDL3.SDL.FColor>(), uv, 8, 3, Span<int>.Empty, 0, 4);
            }

            TestAssert.Equal(true, result, "SDL.RenderGeometryRaw<TIndex>(empty indices) must return the native hook value.");
            AssertRawPointerGeometry((IntPtr)0xD051, (IntPtr)0xD052, capturedXYPointer, 8, capturedColorPointer, Marshal.SizeOf<SDL3.SDL.FColor>(), capturedUVPointer, 8, 3, IntPtr.Zero, 0, 0, "SDL.RenderGeometryRaw<TIndex>(empty indices)");
            TestAssert.True(capturedXYPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(empty indices) must pin xy.");
            TestAssert.True(capturedColorPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(empty indices) must pin colors.");
            TestAssert.True(capturedUVPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(empty indices) must pin uv.");
        }

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope genericIndices = NativeHookScope.Install("RenderGeometryRawPointersNativeFunction", nameof(CaptureRenderGeometryRawPointers));
        bool genericIndicesResult;
        unsafe
        {
            genericIndicesResult = SDL3.SDL.RenderGeometryRaw<int>((IntPtr)0xD061, (IntPtr)0xD062, xy, 8, colors, Marshal.SizeOf<SDL3.SDL.FColor>(), uv, 8, 3, intIndices, 3, 4);
        }

        TestAssert.Equal(true, genericIndicesResult, "SDL.RenderGeometryRaw<TIndex>(int indices) must return the native hook value.");
        AssertRawPointerGeometry((IntPtr)0xD061, (IntPtr)0xD062, capturedXYPointer, 8, capturedColorPointer, Marshal.SizeOf<SDL3.SDL.FColor>(), capturedUVPointer, 8, 3, capturedIndicesPointer, 3, 4, "SDL.RenderGeometryRaw<TIndex>(int indices)");
        TestAssert.True(capturedXYPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(int indices) must pin xy.");
        TestAssert.True(capturedColorPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(int indices) must pin colors.");
        TestAssert.True(capturedUVPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(int indices) must pin uv.");
        TestAssert.True(capturedIndicesPointer != IntPtr.Zero, "SDL.RenderGeometryRaw<TIndex>(int indices) must pin indices.");

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope rawFloatSpans = NativeHookScope.Install("RenderGeometryRawPointersNativeFunction", nameof(CaptureRenderGeometryRawFloatSpans));
        float[] interleavedFloats =
        [
            1.0f, 2.0f,
            0.1f, 0.2f, 0.3f, 0.4f,
            0.0f, 0.1f,
            3.0f, 4.0f,
            0.5f, 0.6f, 0.7f, 0.8f,
            0.2f, 0.3f,
        ];
        bool rawFloatResult = SDL3.SDL.RenderGeometryRaw<ushort>(
            (IntPtr)0xD071,
            (IntPtr)0xD072,
            interleavedFloats.AsSpan(0, 10),
            sizeof(float) * 8,
            interleavedFloats.AsSpan(2, 10),
            sizeof(float) * 8,
            interleavedFloats.AsSpan(6, 10),
            sizeof(float) * 8,
            2,
            ReadOnlySpan<ushort>.Empty,
            0,
            sizeof(ushort));

        TestAssert.Equal(true, rawFloatResult, "SDL.RenderGeometryRaw<TIndex>(float color spans) must return the native hook value.");
        AssertGeometryBase((IntPtr)0xD071, (IntPtr)0xD072, 2, 0, "SDL.RenderGeometryRaw<TIndex>(float color spans)");
        AssertFloats([1.0f, 2.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.0f, 0.1f, 3.0f, 4.0f], capturedXY, "SDL.RenderGeometryRaw<TIndex>(float color spans) must forward xy pointer.");
        AssertFloats([0.1f, 0.2f, 0.3f, 0.4f, 0.0f, 0.1f, 3.0f, 4.0f, 0.5f, 0.6f, 0.7f, 0.8f], capturedColorFloats, "SDL.RenderGeometryRaw<TIndex>(float color spans) must forward color pointer.");
        AssertFloats([0.0f, 0.1f, 3.0f, 4.0f, 0.5f, 0.6f, 0.7f, 0.8f, 0.2f, 0.3f], capturedUV, "SDL.RenderGeometryRaw<TIndex>(float color spans) must forward uv pointer.");
        TestAssert.Equal(IntPtr.Zero, capturedIndicesPointer, "SDL.RenderGeometryRaw<TIndex>(float color spans) must forward empty indices as null.");
        TestAssert.Equal(0, capturedSizeIndices, "SDL.RenderGeometryRaw<TIndex>(float color spans) must forward empty index size as zero.");
    }

    public static void RenderTextureAddressReadPresentDestroyFlushAndMetalFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderTextureAddressModeNativeFunction", nameof(CaptureSetRenderTextureAddressMode)))
        {
            bool result = SDL3.SDL.SetRenderTextureAddressMode((IntPtr)0xE001, SDL3.SDL.TextureAddressMode.Clamp, SDL3.SDL.TextureAddressMode.Wrap);

            TestAssert.Equal(true, result, "SDL.SetRenderTextureAddressMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0xE001, capturedRenderer, "SDL.SetRenderTextureAddressMode must forward renderer.");
            TestAssert.Equal(SDL3.SDL.TextureAddressMode.Clamp, capturedTextureAddressModeU, "SDL.SetRenderTextureAddressMode must forward U mode.");
            TestAssert.Equal(SDL3.SDL.TextureAddressMode.Wrap, capturedTextureAddressModeV, "SDL.SetRenderTextureAddressMode must forward V mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextTextureAddressModeU = SDL3.SDL.TextureAddressMode.Wrap;
        nextTextureAddressModeV = SDL3.SDL.TextureAddressMode.Clamp;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderTextureAddressModeNativeFunction", nameof(CaptureGetRenderTextureAddressMode)))
        {
            bool result = SDL3.SDL.GetRenderTextureAddressMode((IntPtr)0xE011, out SDL3.SDL.TextureAddressMode umode, out SDL3.SDL.TextureAddressMode vmode);

            TestAssert.Equal(true, result, "SDL.GetRenderTextureAddressMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0xE011, capturedRenderer, "SDL.GetRenderTextureAddressMode must forward renderer.");
            TestAssert.Equal(SDL3.SDL.TextureAddressMode.Wrap, umode, "SDL.GetRenderTextureAddressMode must write U mode.");
            TestAssert.Equal(SDL3.SDL.TextureAddressMode.Clamp, vmode, "SDL.GetRenderTextureAddressMode must write V mode.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xE021;
        using (NativeHookScope _ = NativeHookScope.Install("RenderReadPixelsNativeFunction", nameof(CaptureRenderReadPixels)))
        {
            IntPtr result = SDL3.SDL.RenderReadPixels((IntPtr)0xE022, null);

            TestAssert.Equal((IntPtr)0xE021, result, "SDL.RenderReadPixels(null) must return the native hook value.");
            TestAssert.Equal((IntPtr)0xE022, capturedRenderer, "SDL.RenderReadPixels(null) must forward renderer.");
            TestAssert.Equal(IntPtr.Zero, capturedRectPointer, "SDL.RenderReadPixels(null) must forward null rect pointer.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xE031;
        SDL3.SDL.Rect rect = CreateRect(1, 2, 3, 4);
        using (NativeHookScope _ = NativeHookScope.Install("RenderReadPixelsNativeFunction", nameof(CaptureRenderReadPixels)))
        {
            IntPtr result = SDL3.SDL.RenderReadPixels((IntPtr)0xE032, rect);

            TestAssert.Equal((IntPtr)0xE031, result, "SDL.RenderReadPixels(Rect) must return the native hook value.");
            TestAssert.Equal((IntPtr)0xE032, capturedRenderer, "SDL.RenderReadPixels(Rect) must forward renderer.");
            TestAssert.True(capturedRectPointer != IntPtr.Zero, "SDL.RenderReadPixels(Rect) must allocate a rect pointer.");
            AssertRect(rect, capturedRect, "SDL.RenderReadPixels(Rect) must forward rect.");
        }

        AssertRendererBoolFunction("RenderPresentNativeFunction", nameof(CaptureSingleRendererReturnBool), SDL3.SDL.RenderPresent, (IntPtr)0xE041, true, "SDL.RenderPresent");
        AssertTextureVoidFunction("DestroyTextureNativeFunction", nameof(CaptureDestroyTexture), SDL3.SDL.DestroyTexture, (IntPtr)0xE051, "SDL.DestroyTexture");
        AssertRendererVoidFunction("DestroyRendererNativeFunction", nameof(CaptureDestroyRenderer), SDL3.SDL.DestroyRenderer, (IntPtr)0xE061, "SDL.DestroyRenderer");
        AssertRendererBoolFunction("FlushRendererNativeFunction", nameof(CaptureSingleRendererReturnBool), SDL3.SDL.FlushRenderer, (IntPtr)0xE071, true, "SDL.FlushRenderer");
        AssertSinglePointerReturn("GetRenderMetalLayerNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetRenderMetalLayer, (IntPtr)0xE081, (IntPtr)0xE082, "SDL.GetRenderMetalLayer");
        AssertSinglePointerReturn("GetRenderMetalCommandEncoderNativeFunction", nameof(CaptureSinglePointerReturnPointer), SDL3.SDL.GetRenderMetalCommandEncoder, (IntPtr)0xE091, (IntPtr)0xE092, "SDL.GetRenderMetalCommandEncoder");
    }

    public static void VulkanVSyncDebugTextAndDefaultTextureScaleFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("AddVulkanRenderSemaphoresNativeFunction", nameof(CaptureAddVulkanRenderSemaphores)))
        {
            bool result = SDL3.SDL.AddVulkanRenderSemaphores((IntPtr)0xF001, 0xF002u, 0xF003, 0xF004);

            TestAssert.Equal(true, result, "SDL.AddVulkanRenderSemaphores must return the native hook value.");
            TestAssert.Equal((IntPtr)0xF001, capturedRenderer, "SDL.AddVulkanRenderSemaphores must forward renderer.");
            TestAssert.Equal(0xF002u, capturedWaitStageMask, "SDL.AddVulkanRenderSemaphores must forward wait stage mask.");
            TestAssert.Equal(0xF003L, capturedWaitSemaphore, "SDL.AddVulkanRenderSemaphores must forward wait semaphore.");
            TestAssert.Equal(0xF004L, capturedSignalSemaphore, "SDL.AddVulkanRenderSemaphores must forward signal semaphore.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetRenderVSyncNativeFunction", nameof(CaptureSetRenderVSync)))
        {
            bool result = SDL3.SDL.SetRenderVSync((IntPtr)0xF011, 2);

            TestAssert.Equal(true, result, "SDL.SetRenderVSync must return the native hook value.");
            TestAssert.Equal((IntPtr)0xF011, capturedRenderer, "SDL.SetRenderVSync must forward renderer.");
            TestAssert.Equal(2, capturedVSync, "SDL.SetRenderVSync must forward vsync.");
        }

        ResetCaptureState();
        nextBool = true;
        nextVSync = -1;
        using (NativeHookScope _ = NativeHookScope.Install("GetRenderVSyncNativeFunction", nameof(CaptureGetRenderVSync)))
        {
            bool result = SDL3.SDL.GetRenderVSync((IntPtr)0xF021, out int vsync);

            TestAssert.Equal(true, result, "SDL.GetRenderVSync must return the native hook value.");
            TestAssert.Equal((IntPtr)0xF021, capturedRenderer, "SDL.GetRenderVSync must forward renderer.");
            TestAssert.Equal(-1, vsync, "SDL.GetRenderVSync must write vsync.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderDebugTextNativeFunction", nameof(CaptureRenderDebugText)))
        {
            bool result = SDL3.SDL.RenderDebugText((IntPtr)0xF031, 1.25f, 2.5f, "debug text");

            TestAssert.Equal(true, result, "SDL.RenderDebugText must return the native hook value.");
            AssertDebugText((IntPtr)0xF031, 1.25f, 2.5f, "debug text", "SDL.RenderDebugText");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RenderDebugTextFormatNativeFunction", nameof(CaptureRenderDebugTextFormat)))
        {
            bool result = SDL3.SDL.RenderDebugTextFormat((IntPtr)0xF041, 3.75f, 4.5f, "format text");

            TestAssert.Equal(true, result, "SDL.RenderDebugTextFormat must return the native hook value.");
            AssertDebugText((IntPtr)0xF041, 3.75f, 4.5f, "format text", "SDL.RenderDebugTextFormat");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetDefaultTextureScaleModeNativeFunction", nameof(CaptureSetDefaultTextureScaleMode)))
        {
            bool result = SDL3.SDL.SetDefaultTextureScaleMode((IntPtr)0xF051, SDL3.SDL.ScaleMode.PixelArt);

            TestAssert.Equal(true, result, "SDL.SetDefaultTextureScaleMode must return the native hook value.");
            TestAssert.Equal((IntPtr)0xF051, capturedRenderer, "SDL.SetDefaultTextureScaleMode must forward renderer.");
            TestAssert.Equal(SDL3.SDL.ScaleMode.PixelArt, capturedScaleMode, "SDL.SetDefaultTextureScaleMode must forward scale mode.");
        }

        ResetCaptureState();
        nextBool = true;
        nextScaleMode = SDL3.SDL.ScaleMode.Nearest;
        using NativeHookScope getDefaultScaleMode = NativeHookScope.Install("GetDefaultTextureScaleModeNativeFunction", nameof(CaptureGetDefaultTextureScaleMode));
        bool getDefaultScaleModeResult = SDL3.SDL.GetDefaultTextureScaleMode((IntPtr)0xF061, out SDL3.SDL.ScaleMode scaleMode);

        TestAssert.Equal(true, getDefaultScaleModeResult, "SDL.GetDefaultTextureScaleMode must return the native hook value.");
        TestAssert.Equal((IntPtr)0xF061, capturedRenderer, "SDL.GetDefaultTextureScaleMode must forward renderer.");
        TestAssert.Equal(SDL3.SDL.ScaleMode.Nearest, scaleMode, "SDL.GetDefaultTextureScaleMode must write scale mode.");
    }

    public static void GpuRenderStateFunctions_ForwardPointersArraysAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0xA001;
        using (NativeHookScope _ = NativeHookScope.Install("CreateGPURenderStateNativeFunction", nameof(CaptureCreateGPURenderState)))
        {
            IntPtr result = SDL3.SDL.CreateGPURenderState((IntPtr)0xA011, (IntPtr)0xA012);

            TestAssert.Equal((IntPtr)0xA001, result, "SDL.CreateGPURenderState must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA011, capturedRenderer, "SDL.CreateGPURenderState must forward renderer.");
            TestAssert.Equal((IntPtr)0xA012, capturedCreateInfo, "SDL.CreateGPURenderState must forward create info.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetGPURenderStateFragmentUniformsNativeFunction", nameof(CaptureSetGPURenderStateFragmentUniforms)))
        {
            bool result = SDL3.SDL.SetGPURenderStateFragmentUniforms((IntPtr)0xA081, 5u, (IntPtr)0xA082, 16u);

            TestAssert.Equal(true, result, "SDL.SetGPURenderStateFragmentUniforms must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA081, capturedState, "SDL.SetGPURenderStateFragmentUniforms must forward state.");
            TestAssert.Equal(5u, capturedSlotIndex, "SDL.SetGPURenderStateFragmentUniforms must forward slot index.");
            TestAssert.Equal((IntPtr)0xA082, capturedData, "SDL.SetGPURenderStateFragmentUniforms must forward data.");
            TestAssert.Equal(16u, capturedLength, "SDL.SetGPURenderStateFragmentUniforms must forward length.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetGPURenderStateNativeFunction", nameof(CaptureSetGPURenderState)))
        {
            bool result = SDL3.SDL.SetGPURenderState((IntPtr)0xA091, (IntPtr)0xA092);

            TestAssert.Equal(true, result, "SDL.SetGPURenderState must return the native hook value.");
            TestAssert.Equal((IntPtr)0xA091, capturedRenderer, "SDL.SetGPURenderState must forward renderer.");
            TestAssert.Equal((IntPtr)0xA092, capturedState, "SDL.SetGPURenderState must forward state.");
        }

        ResetCaptureState();
        using NativeHookScope destroy = NativeHookScope.Install("DestroyGPURenderStateNativeFunction", nameof(CaptureDestroyGPURenderState));
        SDL3.SDL.DestroyGPURenderState((IntPtr)0xA0A1);

        TestAssert.Equal((IntPtr)0xA0A1, capturedState, "SDL.DestroyGPURenderState must forward state.");
    }

    private static int CaptureGetNumRenderDrivers()
    {
        return nextInt;
    }

    private static IntPtr CaptureGetRenderDriver(int index)
    {
        capturedIndex = index;
        return nextPointer;
    }

    private static bool CaptureCreateWindowAndRenderer(string title, int width, int height, SDL3.SDL.WindowFlags windowFlags, out IntPtr window, out IntPtr renderer)
    {
        capturedTitle = title;
        capturedWidth = width;
        capturedHeight = height;
        capturedWindowFlags = windowFlags;
        window = nextPointer;
        renderer = capturedPointer;
        return nextBool;
    }

    private static IntPtr CaptureCreateRenderer(IntPtr window, string? name)
    {
        capturedWindow = window;
        capturedName = name;
        return nextPointer;
    }

    private static IntPtr CaptureCreateRendererWithProperties(uint props)
    {
        capturedProps = props;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPURenderer(IntPtr device, IntPtr window)
    {
        capturedDevice = device;
        capturedWindow = window;
        return nextPointer;
    }

    private static IntPtr CaptureSinglePointerReturnPointer(IntPtr value)
    {
        capturedPointer = value;
        return nextPointer;
    }

    private static uint CaptureSinglePointerReturnUInt(IntPtr value)
    {
        capturedPointer = value;
        return nextUInt;
    }

    private static IntPtr CaptureGetRendererName(IntPtr renderer)
    {
        capturedRenderer = renderer;
        return nextPointer;
    }

    private static bool CaptureOutputSize(IntPtr renderer, out int w, out int h)
    {
        capturedRenderer = renderer;
        w = nextWidth;
        h = nextHeight;
        return nextBool;
    }

    private static IntPtr CaptureCreateTexture(IntPtr renderer, SDL3.SDL.PixelFormat format, SDL3.SDL.TextureAccess access, int w, int h)
    {
        capturedRenderer = renderer;
        capturedPixelFormat = format;
        capturedTextureAccess = access;
        capturedWidth = w;
        capturedHeight = h;
        return nextPointer;
    }

    private static IntPtr CaptureCreateTextureFromSurface(IntPtr renderer, IntPtr surface)
    {
        capturedRenderer = renderer;
        capturedSurface = surface;
        return nextPointer;
    }

    private static IntPtr CaptureCreateTextureWithProperties(IntPtr renderer, uint props)
    {
        capturedRenderer = renderer;
        capturedProps = props;
        return nextPointer;
    }

    private static bool CaptureGetTextureSize(IntPtr texture, out float w, out float h)
    {
        capturedTexture = texture;
        w = nextFloatWidth;
        h = nextFloatHeight;
        return nextBool;
    }

    private static bool CaptureSetTexturePalette(IntPtr texture, IntPtr palette)
    {
        capturedTexture = texture;
        capturedPalette = palette;
        return nextBool;
    }

    private static bool CaptureSetTextureColorMod(IntPtr texture, byte r, byte g, byte b)
    {
        capturedTexture = texture;
        capturedR = r;
        capturedG = g;
        capturedB = b;
        return nextBool;
    }

    private static bool CaptureSetTextureColorModFloat(IntPtr texture, float r, float g, float b)
    {
        capturedTexture = texture;
        capturedFR = r;
        capturedFG = g;
        capturedFB = b;
        return nextBool;
    }

    private static bool CaptureGetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b)
    {
        capturedTexture = texture;
        r = nextR;
        g = nextG;
        b = nextB;
        return nextBool;
    }

    private static bool CaptureGetTextureColorModFloat(IntPtr texture, out float r, out float g, out float b)
    {
        capturedTexture = texture;
        r = nextFR;
        g = nextFG;
        b = nextFB;
        return nextBool;
    }

    private static bool CaptureSetTextureAlphaMod(IntPtr texture, byte alpha)
    {
        capturedTexture = texture;
        capturedAlpha = alpha;
        return nextBool;
    }

    private static bool CaptureSetTextureAlphaModFloat(IntPtr texture, float alpha)
    {
        capturedTexture = texture;
        capturedFAlpha = alpha;
        return nextBool;
    }

    private static bool CaptureGetTextureAlphaMod(IntPtr texture, out byte alpha)
    {
        capturedTexture = texture;
        alpha = nextAlpha;
        return nextBool;
    }

    private static bool CaptureGetTextureAlphaModFloat(IntPtr texture, out float alpha)
    {
        capturedTexture = texture;
        alpha = nextFAlpha;
        return nextBool;
    }

    private static bool CaptureSetTextureBlendMode(IntPtr texture, SDL3.SDL.BlendMode blendMode)
    {
        capturedTexture = texture;
        capturedBlendMode = blendMode;
        return nextBool;
    }

    private static bool CaptureGetTextureBlendMode(IntPtr texture, out SDL3.SDL.BlendMode blendMode)
    {
        capturedTexture = texture;
        blendMode = nextBlendMode;
        return nextBool;
    }

    private static bool CaptureSetTextureScaleMode(IntPtr texture, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedTexture = texture;
        capturedScaleMode = scaleMode;
        return nextBool;
    }

    private static bool CaptureGetTextureScaleMode(IntPtr texture, out SDL3.SDL.ScaleMode scaleMode)
    {
        capturedTexture = texture;
        scaleMode = nextScaleMode;
        return nextBool;
    }

    private static bool CaptureUpdateTexturePointer(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch)
    {
        capturedTexture = texture;
        capturedRectPointer = rect;
        capturedPixels = pixels;
        capturedPitch = pitch;
        return nextBool;
    }

    private static bool CaptureUpdateTextureArray(IntPtr texture, IntPtr rect, byte[] pixels, int pitch)
    {
        capturedTexture = texture;
        capturedRectPointer = rect;
        capturedBytes = [.. pixels];
        capturedPitch = pitch;
        return nextBool;
    }

    private static bool CaptureUpdateTextureRectPointer(IntPtr texture, in SDL3.SDL.Rect rect, IntPtr pixels, int pitch)
    {
        capturedTexture = texture;
        capturedRect = rect;
        capturedPixels = pixels;
        capturedPitch = pitch;
        return nextBool;
    }

    private static bool CaptureUpdateTextureRectArray(IntPtr texture, in SDL3.SDL.Rect rect, byte[] pixels, int pitch)
    {
        capturedTexture = texture;
        capturedRect = rect;
        capturedBytes = [.. pixels];
        capturedPitch = pitch;
        return nextBool;
    }

    private static bool CaptureUpdateYUVTexturePointer(IntPtr texture, IntPtr rect, IntPtr yplane, int ypitch, IntPtr uplane, int upitch, IntPtr vplane, int vpitch)
    {
        capturedTexture = texture;
        capturedRectPointer = rect;
        capturedYPlane = yplane;
        capturedYPitch = ypitch;
        capturedUPlane = uplane;
        capturedUPitch = upitch;
        capturedVPlane = vplane;
        capturedVPitch = vpitch;
        return nextBool;
    }

    private static bool CaptureUpdateYUVTextureRect(IntPtr texture, in SDL3.SDL.Rect rect, IntPtr yplane, int ypitch, IntPtr uplane, int upitch, IntPtr vplane, int vpitch)
    {
        capturedTexture = texture;
        capturedRect = rect;
        capturedYPlane = yplane;
        capturedYPitch = ypitch;
        capturedUPlane = uplane;
        capturedUPitch = upitch;
        capturedVPlane = vplane;
        capturedVPitch = vpitch;
        return nextBool;
    }

    private static bool CaptureUpdateNVTexturePointer(IntPtr texture, IntPtr rect, IntPtr yplane, int ypitch, IntPtr uvplane, int uvpitch)
    {
        capturedTexture = texture;
        capturedRectPointer = rect;
        capturedYPlane = yplane;
        capturedYPitch = ypitch;
        capturedUVPlane = uvplane;
        capturedUVPitch = uvpitch;
        return nextBool;
    }

    private static bool CaptureUpdateNVTextureRect(IntPtr texture, in SDL3.SDL.Rect rect, IntPtr yplane, int ypitch, IntPtr uvplane, int uvpitch)
    {
        capturedTexture = texture;
        capturedRect = rect;
        capturedYPlane = yplane;
        capturedYPitch = ypitch;
        capturedUVPlane = uvplane;
        capturedUVPitch = uvpitch;
        return nextBool;
    }

    private static bool CaptureLockTexturePointer(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch)
    {
        capturedTexture = texture;
        capturedRectPointer = rect;
        pixels = nextPixels;
        pitch = nextPitch;
        return nextBool;
    }

    private static bool CaptureLockTextureRect(IntPtr texture, in SDL3.SDL.Rect rect, out IntPtr pixels, out int pitch)
    {
        capturedTexture = texture;
        capturedRect = rect;
        pixels = nextPixels;
        pitch = nextPitch;
        return nextBool;
    }

    private static bool CaptureLockTextureToSurfacePointer(IntPtr texture, IntPtr rect, out IntPtr surface)
    {
        capturedTexture = texture;
        capturedRectPointer = rect;
        surface = nextSurface;
        return nextBool;
    }

    private static bool CaptureLockTextureToSurfaceRect(IntPtr texture, in SDL3.SDL.Rect rect, out IntPtr surface)
    {
        capturedTexture = texture;
        capturedRect = rect;
        surface = nextSurface;
        return nextBool;
    }

    private static void CaptureUnlockTexture(IntPtr texture)
    {
        capturedTexture = texture;
    }

    private static bool CaptureSetRenderTarget(IntPtr renderer, IntPtr texture)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
        return nextBool;
    }

    private static bool CaptureSetRenderLogicalPresentation(IntPtr renderer, int w, int h, SDL3.SDL.RendererLogicalPresentation mode)
    {
        capturedRenderer = renderer;
        capturedWidth = w;
        capturedHeight = h;
        capturedPresentationMode = mode;
        return nextBool;
    }

    private static bool CaptureGetRenderLogicalPresentation(IntPtr renderer, out int w, out int h, out SDL3.SDL.RendererLogicalPresentation mode)
    {
        capturedRenderer = renderer;
        w = nextWidth;
        h = nextHeight;
        mode = nextPresentationMode;
        return nextBool;
    }

    private static bool CaptureGetRenderLogicalPresentationRect(IntPtr renderer, out SDL3.SDL.FRect rect)
    {
        capturedRenderer = renderer;
        rect = nextFRect;
        return nextBool;
    }

    private static bool CaptureRenderCoordinatesFromWindow(IntPtr renderer, float windowx, float windowy, out float x, out float y)
    {
        capturedRenderer = renderer;
        capturedWindowX = windowx;
        capturedWindowY = windowy;
        x = nextX;
        y = nextY;
        return nextBool;
    }

    private static bool CaptureRenderCoordinatesToWindow(IntPtr renderer, float x, float y, out float windowx, out float windowy)
    {
        capturedRenderer = renderer;
        capturedX = x;
        capturedY = y;
        windowx = nextWindowX;
        windowy = nextWindowY;
        return nextBool;
    }

    private static bool CaptureConvertEventToRenderCoordinates(IntPtr renderer, ref SDL3.SDL.Event @event)
    {
        capturedRenderer = renderer;
        capturedEventType = @event.Type;
        @event.Type = nextEventType;
        return nextBool;
    }

    private static bool CaptureSetRenderViewportPointer(IntPtr renderer, IntPtr rect)
    {
        capturedRenderer = renderer;
        capturedRectPointer = rect;
        return nextBool;
    }

    private static bool CaptureSetRenderViewportRect(IntPtr renderer, SDL3.SDL.Rect rect)
    {
        capturedRenderer = renderer;
        capturedRect = rect;
        return nextBool;
    }

    private static bool CaptureGetRenderViewport(IntPtr renderer, out SDL3.SDL.Rect rect)
    {
        capturedRenderer = renderer;
        rect = nextRect;
        return nextBool;
    }

    private static bool CaptureRendererOnlyBool(IntPtr renderer)
    {
        capturedRenderer = renderer;
        return nextBool;
    }

    private static void CaptureRendererOnlyVoid(IntPtr renderer)
    {
        capturedRenderer = renderer;
    }

    private static bool CaptureGetRenderSafeArea(IntPtr renderer, out SDL3.SDL.Rect rect)
    {
        capturedRenderer = renderer;
        rect = nextRect;
        return nextBool;
    }

    private static bool CaptureSetRenderClipRectPointer(IntPtr renderer, IntPtr rect)
    {
        capturedRenderer = renderer;
        capturedRectPointer = rect;
        return nextBool;
    }

    private static bool CaptureSetRenderClipRectRect(IntPtr renderer, in SDL3.SDL.Rect rect)
    {
        capturedRenderer = renderer;
        capturedRect = rect;
        return nextBool;
    }

    private static bool CaptureGetRenderClipRect(IntPtr renderer, out SDL3.SDL.Rect rect)
    {
        capturedRenderer = renderer;
        rect = nextRect;
        return nextBool;
    }

    private static bool CaptureSetRenderScale(IntPtr renderer, float scalex, float scaley)
    {
        capturedRenderer = renderer;
        capturedScaleX = scalex;
        capturedScaleY = scaley;
        return nextBool;
    }

    private static bool CaptureGetRenderScale(IntPtr renderer, out float scalex, out float scaley)
    {
        capturedRenderer = renderer;
        scalex = nextScaleX;
        scaley = nextScaleY;
        return nextBool;
    }

    private static bool CaptureSetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a)
    {
        capturedRenderer = renderer;
        capturedR = r;
        capturedG = g;
        capturedB = b;
        capturedAlpha = a;
        return nextBool;
    }

    private static bool CaptureSetRenderDrawColorFloat(IntPtr renderer, float r, float g, float b, float a)
    {
        capturedRenderer = renderer;
        capturedFR = r;
        capturedFG = g;
        capturedFB = b;
        capturedFAlpha = a;
        return nextBool;
    }

    private static bool CaptureGetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a)
    {
        capturedRenderer = renderer;
        r = nextR;
        g = nextG;
        b = nextB;
        a = nextAlpha;
        return nextBool;
    }

    private static bool CaptureGetRenderDrawColorFloat(IntPtr renderer, out float r, out float g, out float b, out float a)
    {
        capturedRenderer = renderer;
        r = nextFR;
        g = nextFG;
        b = nextFB;
        a = nextFAlpha;
        return nextBool;
    }

    private static bool CaptureSetRenderColorScale(IntPtr renderer, float scale)
    {
        capturedRenderer = renderer;
        capturedColorScale = scale;
        return nextBool;
    }

    private static bool CaptureGetRenderColorScale(IntPtr renderer, out float scale)
    {
        capturedRenderer = renderer;
        scale = nextColorScale;
        return nextBool;
    }

    private static bool CaptureSetRenderDrawBlendMode(IntPtr renderer, SDL3.SDL.BlendMode blendMode)
    {
        capturedRenderer = renderer;
        capturedBlendMode = blendMode;
        return nextBool;
    }

    private static bool CaptureGetRenderDrawBlendMode(IntPtr renderer, out SDL3.SDL.BlendMode blendMode)
    {
        capturedRenderer = renderer;
        blendMode = nextBlendMode;
        return nextBool;
    }

    private static bool CaptureRenderPoint(IntPtr renderer, float x, float y)
    {
        capturedRenderer = renderer;
        capturedX = x;
        capturedY = y;
        return nextBool;
    }

    private static bool CaptureRenderPointsArray(IntPtr renderer, SDL3.SDL.FPoint[] points, int count)
    {
        capturedRenderer = renderer;
        capturedFPoints = points;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderPointsPointer(IntPtr renderer, IntPtr points, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = points;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderPointsPointerSpan(IntPtr renderer, IntPtr points, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = points;
        capturedFPoints = CopyUnmanaged<SDL3.SDL.FPoint>(points, count);
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderLine(IntPtr renderer, float x1, float y1, float x2, float y2)
    {
        capturedRenderer = renderer;
        capturedX1 = x1;
        capturedY1 = y1;
        capturedX2 = x2;
        capturedY2 = y2;
        return nextBool;
    }

    private static bool CaptureRenderLinesArray(IntPtr renderer, SDL3.SDL.FPoint[] points, int count)
    {
        capturedRenderer = renderer;
        capturedFPoints = points;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderLinesPointer(IntPtr renderer, IntPtr points, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = points;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderLinesPointerSpan(IntPtr renderer, IntPtr points, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = points;
        capturedFPoints = CopyUnmanaged<SDL3.SDL.FPoint>(points, count);
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderRectPointer(IntPtr renderer, IntPtr rect)
    {
        capturedRenderer = renderer;
        capturedPointer = rect;
        return nextBool;
    }

    private static bool CaptureRenderRectRect(IntPtr renderer, in SDL3.SDL.FRect rect)
    {
        capturedRenderer = renderer;
        capturedFRect = rect;
        return nextBool;
    }

    private static bool CaptureRenderRectsArray(IntPtr renderer, SDL3.SDL.FRect[] rects, int count)
    {
        capturedRenderer = renderer;
        capturedFRects = rects;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderRectsPointer(IntPtr renderer, IntPtr rects, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = rects;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderRectsPointerSpan(IntPtr renderer, IntPtr rects, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = rects;
        capturedFRects = CopyUnmanaged<SDL3.SDL.FRect>(rects, count);
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderFillRectPointer(IntPtr renderer, IntPtr rect)
    {
        capturedRenderer = renderer;
        capturedPointer = rect;
        return nextBool;
    }

    private static bool CaptureRenderFillRectRect(IntPtr renderer, in SDL3.SDL.FRect rect)
    {
        capturedRenderer = renderer;
        capturedFRect = rect;
        return nextBool;
    }

    private static bool CaptureRenderFillRectsArray(IntPtr renderer, SDL3.SDL.FRect[] rects, int count)
    {
        capturedRenderer = renderer;
        capturedFRects = rects;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderFillRectsPointer(IntPtr renderer, IntPtr rects, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = rects;
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderFillRectsPointerSpan(IntPtr renderer, IntPtr rects, int count)
    {
        capturedRenderer = renderer;
        capturedPointer = rects;
        capturedFRects = CopyUnmanaged<SDL3.SDL.FRect>(rects, count);
        capturedCount = count;
        return nextBool;
    }

    private static bool CaptureRenderTexturePointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect)
    {
        CaptureTextureRenderPointers(renderer, texture, srcrect, dstrect);
        return nextBool;
    }

    private static bool CaptureRenderTextureSourceRect(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr dstrect)
    {
        CaptureTextureSourceRectAndDestinationPointer(renderer, texture, in srcrect, dstrect);
        return nextBool;
    }

    private static bool CaptureRenderTextureDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect dstrect)
    {
        CaptureTextureSourcePointerAndDestinationRect(renderer, texture, srcrect, in dstrect);
        return nextBool;
    }

    private static bool CaptureRenderTextureRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect dstrect)
    {
        CaptureTextureRects(renderer, texture, in srcrect, in dstrect);
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedPointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, IntPtr center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureRenderPointers(renderer, texture, srcrect, dstrect);
        CaptureRotation(angle, center, flip);
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedSourceRect(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr dstrect, double angle, IntPtr center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureSourceRectAndDestinationPointer(renderer, texture, in srcrect, dstrect);
        CaptureRotation(angle, center, flip);
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect dstrect, double angle, IntPtr center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureSourcePointerAndDestinationRect(renderer, texture, srcrect, in dstrect);
        CaptureRotation(angle, center, flip);
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedCenterPoint(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, in SDL3.SDL.FPoint center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureRenderPointers(renderer, texture, srcrect, dstrect);
        CaptureRotation(angle, IntPtr.Zero, flip);
        capturedCenterFPoint = center;
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect dstrect, double angle, IntPtr center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureRects(renderer, texture, in srcrect, in dstrect);
        CaptureRotation(angle, center, flip);
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedDestinationRectCenterPoint(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect dstrect, double angle, in SDL3.SDL.FPoint center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureSourcePointerAndDestinationRect(renderer, texture, srcrect, in dstrect);
        CaptureRotation(angle, IntPtr.Zero, flip);
        capturedCenterFPoint = center;
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedSourceRectCenterPoint(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr dstrect, double angle, in SDL3.SDL.FPoint center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureSourceRectAndDestinationPointer(renderer, texture, in srcrect, dstrect);
        CaptureRotation(angle, IntPtr.Zero, flip);
        capturedCenterFPoint = center;
        return nextBool;
    }

    private static bool CaptureRenderTextureRotatedRectsCenterPoint(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect dstrect, double angle, in SDL3.SDL.FPoint center, SDL3.SDL.FlipMode flip)
    {
        CaptureTextureRects(renderer, texture, in srcrect, in dstrect);
        CaptureRotation(angle, IntPtr.Zero, flip);
        capturedCenterFPoint = center;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffinePointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, IntPtr right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginPointer = origin;
        capturedRightPointer = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineDownRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, IntPtr right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginPointer = origin;
        capturedRightPointer = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineRightRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, in SDL3.SDL.FRect right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginPointer = origin;
        capturedRightFRect = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineRightDownRects(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, in SDL3.SDL.FRect right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginPointer = origin;
        capturedRightFRect = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineOriginRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect origin, IntPtr right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginFRect = origin;
        capturedRightPointer = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineOriginDownRects(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect origin, IntPtr right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginFRect = origin;
        capturedRightPointer = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineOriginRightRects(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect origin, in SDL3.SDL.FRect right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginFRect = origin;
        capturedRightFRect = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineOriginRightDownRects(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect origin, in SDL3.SDL.FRect right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceRectPointer = srcrect;
        capturedOriginFRect = origin;
        capturedRightFRect = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceRect(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr origin, IntPtr right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginPointer = origin;
        capturedRightPointer = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceDownRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr origin, IntPtr right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginPointer = origin;
        capturedRightPointer = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceRightRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr origin, in SDL3.SDL.FRect right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginPointer = origin;
        capturedRightFRect = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceRightDownRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr origin, in SDL3.SDL.FRect right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginPointer = origin;
        capturedRightFRect = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceOriginRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect origin, IntPtr right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginFRect = origin;
        capturedRightPointer = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceOriginDownRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect origin, IntPtr right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginFRect = origin;
        capturedRightPointer = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceOriginRightRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect origin, in SDL3.SDL.FRect right, IntPtr down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginFRect = origin;
        capturedRightFRect = right;
        capturedDownPointer = down;
        return nextBool;
    }

    private static bool CaptureRenderTextureAffineSourceOriginRightDownRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect origin, in SDL3.SDL.FRect right, in SDL3.SDL.FRect down)
    {
        CaptureAffineBase(renderer, texture);
        capturedSourceFRect = srcrect;
        capturedOriginFRect = origin;
        capturedRightFRect = right;
        capturedDownFRect = down;
        return nextBool;
    }

    private static void CaptureAffineBase(IntPtr renderer, IntPtr texture)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
    }

    private static bool CaptureRenderTextureTiledPointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, float scale, IntPtr dstrect)
    {
        CaptureTextureRenderPointers(renderer, texture, srcrect, dstrect);
        capturedScale = scale;
        return nextBool;
    }

    private static bool CaptureRenderTextureTiledSourceRect(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, float scale, IntPtr dstrect)
    {
        CaptureTextureSourceRectAndDestinationPointer(renderer, texture, in srcrect, dstrect);
        capturedScale = scale;
        return nextBool;
    }

    private static bool CaptureRenderTextureTiledDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, float scale, in SDL3.SDL.FRect dstrect)
    {
        CaptureTextureSourcePointerAndDestinationRect(renderer, texture, srcrect, in dstrect);
        capturedScale = scale;
        return nextBool;
    }

    private static bool CaptureRenderTextureTiledRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, float scale, in SDL3.SDL.FRect dstrect)
    {
        CaptureTextureRects(renderer, texture, in srcrect, in dstrect);
        capturedScale = scale;
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridSourceRect(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float sacel, IntPtr dstrect)
    {
        CaptureTextureSourceRectAndDestinationPointer(renderer, texture, in srcrect, dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, sacel);
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float sacel, in SDL3.SDL.FRect dstrect)
    {
        CaptureTextureSourcePointerAndDestinationRect(renderer, texture, srcrect, in dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, sacel);
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float sacel, in SDL3.SDL.FRect dstrect)
    {
        CaptureTextureRects(renderer, texture, in srcrect, in dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, sacel);
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridTiledPointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, IntPtr dstrect, float tileScale)
    {
        CaptureTextureRenderPointers(renderer, texture, srcrect, dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale);
        capturedTileScale = tileScale;
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridTiledSourceRect(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, IntPtr dstrect, float tileScale)
    {
        CaptureTextureSourceRectAndDestinationPointer(renderer, texture, in srcrect, dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale);
        capturedTileScale = tileScale;
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridTiledDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, in SDL3.SDL.FRect dstrect, float tileScale)
    {
        CaptureTextureSourcePointerAndDestinationRect(renderer, texture, srcrect, in dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale);
        capturedTileScale = tileScale;
        return nextBool;
    }

    private static bool CaptureRenderTexture9GridTiledRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, in SDL3.SDL.FRect dstrect, float tileScale)
    {
        CaptureTextureRects(renderer, texture, in srcrect, in dstrect);
        CaptureGridDimensions(leftWidth, rightWidth, topHeight, bottomHeight, scale);
        capturedTileScale = tileScale;
        return nextBool;
    }

    private static void CaptureGridDimensions(float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale)
    {
        capturedLeftWidth = leftWidth;
        capturedRightWidth = rightWidth;
        capturedTopHeight = topHeight;
        capturedBottomHeight = bottomHeight;
        capturedScale = scale;
    }

    private static bool CaptureRenderGeometryPointerIndices(IntPtr renderer, IntPtr texture, SDL3.SDL.Vertex[] vertices, int numVertices, IntPtr indices, int numIndices)
    {
        CaptureGeometryBase(renderer, texture, numVertices, numIndices);
        capturedVertices = vertices;
        capturedIndicesPointer = indices;
        return nextBool;
    }

    private static bool CaptureRenderGeometryArrayIndices(IntPtr renderer, IntPtr texture, SDL3.SDL.Vertex[] vertices, int numVertices, int[] indices, int numIndices)
    {
        CaptureGeometryBase(renderer, texture, numVertices, numIndices);
        capturedVertices = vertices;
        capturedIntIndices = indices;
        return nextBool;
    }

    private static bool CaptureRenderGeometryPointers(IntPtr renderer, IntPtr texture, IntPtr vertices, int numVertices, IntPtr indices, int numIndices)
    {
        CaptureGeometryBase(renderer, texture, numVertices, numIndices);
        capturedVertices = CopyUnmanaged<SDL3.SDL.Vertex>(vertices, numVertices);
        capturedIndicesPointer = indices;
        capturedIntIndices = CopyUnmanaged<int>(indices, numIndices);
        return nextBool;
    }

    private static bool CaptureRenderGeometryRawPointers(IntPtr renderer, IntPtr texture, IntPtr xy, int xyStride, IntPtr color, int colorStride, IntPtr uv, int uvStride, int numVertices, IntPtr indices, int numIndices, int sizeIndices)
    {
        CaptureGeometryBase(renderer, texture, numVertices, numIndices);
        capturedXYPointer = xy;
        capturedXYStride = xyStride;
        capturedColorPointer = color;
        capturedColorStride = colorStride;
        capturedUVPointer = uv;
        capturedUVStride = uvStride;
        capturedIndicesPointer = indices;
        capturedSizeIndices = sizeIndices;
        return nextBool;
    }

    private static bool CaptureRenderGeometryRawFloatSpans(IntPtr renderer, IntPtr texture, IntPtr xy, int xyStride, IntPtr color, int colorStride, IntPtr uv, int uvStride, int numVertices, IntPtr indices, int numIndices, int sizeIndices)
    {
        CaptureRenderGeometryRawPointers(renderer, texture, xy, xyStride, color, colorStride, uv, uvStride, numVertices, indices, numIndices, sizeIndices);
        capturedXY = CopyUnmanaged<float>(xy, FloatElementCount(numVertices, xyStride, 2));
        capturedColorFloats = CopyUnmanaged<float>(color, FloatElementCount(numVertices, colorStride, 4));
        capturedUV = CopyUnmanaged<float>(uv, FloatElementCount(numVertices, uvStride, 2));
        return nextBool;
    }

    private static bool CaptureRenderGeometryRawPointerIndices(IntPtr renderer, IntPtr texture, float[] xy, int xyStride, SDL3.SDL.FColor[] color, int colorStride, float[] uv, int uvStride, int numVertices, IntPtr indices, int numIndices, int sizeIndices)
    {
        CaptureGeometryBase(renderer, texture, numVertices, numIndices);
        capturedXY = xy;
        capturedXYStride = xyStride;
        capturedColors = color;
        capturedColorStride = colorStride;
        capturedUV = uv;
        capturedUVStride = uvStride;
        capturedIndicesPointer = indices;
        capturedSizeIndices = sizeIndices;
        return nextBool;
    }

    private static bool CaptureRenderGeometryRawByteIndices(IntPtr renderer, IntPtr texture, float[] xy, int xyStride, SDL3.SDL.FColor[] color, int colorStride, float[] uv, int uvStride, int numVertices, byte[] indices, int numIndices, int sizeIndices)
    {
        CaptureGeometryBase(renderer, texture, numVertices, numIndices);
        capturedXY = xy;
        capturedXYStride = xyStride;
        capturedColors = color;
        capturedColorStride = colorStride;
        capturedUV = uv;
        capturedUVStride = uvStride;
        capturedByteIndices = indices;
        capturedSizeIndices = sizeIndices;
        return nextBool;
    }

    private static void CaptureGeometryBase(IntPtr renderer, IntPtr texture, int numVertices, int numIndices)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
        capturedNumVertices = numVertices;
        capturedNumIndices = numIndices;
    }

    private static bool CaptureSetRenderTextureAddressMode(IntPtr renderer, SDL3.SDL.TextureAddressMode umode, SDL3.SDL.TextureAddressMode vmode)
    {
        capturedRenderer = renderer;
        capturedTextureAddressModeU = umode;
        capturedTextureAddressModeV = vmode;
        return nextBool;
    }

    private static bool CaptureGetRenderTextureAddressMode(IntPtr renderer, out SDL3.SDL.TextureAddressMode umode, out SDL3.SDL.TextureAddressMode vmode)
    {
        capturedRenderer = renderer;
        umode = nextTextureAddressModeU;
        vmode = nextTextureAddressModeV;
        return nextBool;
    }

    private static IntPtr CaptureRenderReadPixels(IntPtr renderer, IntPtr rect)
    {
        capturedRenderer = renderer;
        capturedRectPointer = rect;
        if (rect != IntPtr.Zero)
        {
            capturedRect = Marshal.PtrToStructure<SDL3.SDL.Rect>(rect);
        }

        return nextPointer;
    }

    private static bool CaptureSingleRendererReturnBool(IntPtr renderer)
    {
        capturedRenderer = renderer;
        return nextBool;
    }

    private static void CaptureDestroyTexture(IntPtr texture)
    {
        capturedTexture = texture;
    }

    private static void CaptureDestroyRenderer(IntPtr renderer)
    {
        capturedRenderer = renderer;
    }

    private static bool CaptureAddVulkanRenderSemaphores(IntPtr renderer, uint waitStageMasl, long waitSemaphore, long signalSemaphore)
    {
        capturedRenderer = renderer;
        capturedWaitStageMask = waitStageMasl;
        capturedWaitSemaphore = waitSemaphore;
        capturedSignalSemaphore = signalSemaphore;
        return nextBool;
    }

    private static bool CaptureSetRenderVSync(IntPtr renderer, int vsync)
    {
        capturedRenderer = renderer;
        capturedVSync = vsync;
        return nextBool;
    }

    private static bool CaptureGetRenderVSync(IntPtr renderer, out int vsync)
    {
        capturedRenderer = renderer;
        vsync = nextVSync;
        return nextBool;
    }

    private static bool CaptureRenderDebugText(IntPtr renderer, float x, float y, string str)
    {
        capturedRenderer = renderer;
        capturedX = x;
        capturedY = y;
        capturedText = str;
        return nextBool;
    }

    private static bool CaptureRenderDebugTextFormat(IntPtr renderer, float x, float y, string fmt)
    {
        capturedRenderer = renderer;
        capturedX = x;
        capturedY = y;
        capturedText = fmt;
        return nextBool;
    }

    private static bool CaptureSetDefaultTextureScaleMode(IntPtr renderer, SDL3.SDL.ScaleMode scaleMode)
    {
        capturedRenderer = renderer;
        capturedScaleMode = scaleMode;
        return nextBool;
    }

    private static bool CaptureGetDefaultTextureScaleMode(IntPtr renderer, out SDL3.SDL.ScaleMode scaleMode)
    {
        capturedRenderer = renderer;
        scaleMode = nextScaleMode;
        return nextBool;
    }

    private static IntPtr CaptureCreateGPURenderState(IntPtr renderer, IntPtr createinfo)
    {
        capturedRenderer = renderer;
        capturedCreateInfo = createinfo;
        return nextPointer;
    }

    private static bool CaptureSetGPURenderStateFragmentUniforms(IntPtr state, uint slotIndex, IntPtr data, uint length)
    {
        capturedState = state;
        capturedSlotIndex = slotIndex;
        capturedData = data;
        capturedLength = length;
        return nextBool;
    }

    private static bool CaptureSetGPURenderState(IntPtr renderer, IntPtr state)
    {
        capturedRenderer = renderer;
        capturedState = state;
        return nextBool;
    }

    private static void CaptureDestroyGPURenderState(IntPtr state)
    {
        capturedState = state;
    }

    private static void CaptureTextureRenderPointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
        capturedSourceRectPointer = srcrect;
        capturedDestinationRectPointer = dstrect;
    }

    private static void CaptureTextureSourceRectAndDestinationPointer(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, IntPtr dstrect)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
        capturedSourceFRect = srcrect;
        capturedDestinationRectPointer = dstrect;
    }

    private static void CaptureTextureSourcePointerAndDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, in SDL3.SDL.FRect dstrect)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
        capturedSourceRectPointer = srcrect;
        capturedDestinationFRect = dstrect;
    }

    private static void CaptureTextureRects(IntPtr renderer, IntPtr texture, in SDL3.SDL.FRect srcrect, in SDL3.SDL.FRect dstrect)
    {
        capturedRenderer = renderer;
        capturedTexture = texture;
        capturedSourceFRect = srcrect;
        capturedDestinationFRect = dstrect;
    }

    private static void CaptureRotation(double angle, IntPtr center, SDL3.SDL.FlipMode flip)
    {
        capturedAngle = angle;
        capturedCenterPointer = center;
        capturedFlipMode = flip;
    }

    private static void AssertSinglePointerReturn(string fieldName, string methodName, Func<IntPtr, IntPtr> function, IntPtr input, IntPtr output, string message)
    {
        ResetCaptureState();
        nextPointer = output;

        using NativeHookScope _ = NativeHookScope.Install(fieldName, methodName);
        IntPtr result = function(input);

        TestAssert.Equal(output, result, $"{message} must return the native hook value.");
        TestAssert.Equal(input, capturedPointer, $"{message} must forward its pointer argument.");
    }

    private static void AssertRendererBoolFunction(string fieldName, string methodName, Func<IntPtr, bool> function, IntPtr renderer, bool nativeResult, string message)
    {
        ResetCaptureState();
        nextBool = nativeResult;

        using NativeHookScope _ = NativeHookScope.Install(fieldName, methodName);
        bool result = function(renderer);

        TestAssert.Equal(nativeResult, result, $"{message} must return the native hook value.");
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
    }

    private static void AssertTextureVoidFunction(string fieldName, string methodName, Action<IntPtr> function, IntPtr texture, string message)
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install(fieldName, methodName);
        function(texture);

        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");
    }

    private static void AssertRendererVoidFunction(string fieldName, string methodName, Action<IntPtr> function, IntPtr renderer, string message)
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install(fieldName, methodName);
        function(renderer);

        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
    }

    private static SDL3.SDL.Rect CreateRect(int x, int y, int w, int h)
    {
        return new SDL3.SDL.Rect { X = x, Y = y, W = w, H = h };
    }

    private static SDL3.SDL.FRect CreateFRect(float x, float y, float w, float h)
    {
        return new SDL3.SDL.FRect { X = x, Y = y, W = w, H = h };
    }

    private static SDL3.SDL.FPoint CreateFPoint(float x, float y)
    {
        return new SDL3.SDL.FPoint { X = x, Y = y };
    }

    private static SDL3.SDL.Vertex CreateVertex(float x, float y, float r, float g, float b, float a, float u, float v)
    {
        return new SDL3.SDL.Vertex
        {
            Position = CreateFPoint(x, y),
            Color = new SDL3.SDL.FColor(r, g, b, a),
            TexCoord = CreateFPoint(u, v),
        };
    }

    private static void AssertRect(SDL3.SDL.Rect expected, SDL3.SDL.Rect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
        TestAssert.Equal(expected.W, actual.W, $"{message} W.");
        TestAssert.Equal(expected.H, actual.H, $"{message} H.");
    }

    private static void AssertFRect(SDL3.SDL.FRect expected, SDL3.SDL.FRect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
        TestAssert.Equal(expected.W, actual.W, $"{message} W.");
        TestAssert.Equal(expected.H, actual.H, $"{message} H.");
    }

    private static void AssertFPoint(SDL3.SDL.FPoint expected, SDL3.SDL.FPoint actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
    }

    private static void AssertFPoints(SDL3.SDL.FPoint[] expected, SDL3.SDL.FPoint[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            AssertFPoint(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static void AssertFRects(SDL3.SDL.FRect[] expected, SDL3.SDL.FRect[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            AssertFRect(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static void AssertVertices(SDL3.SDL.Vertex[] expected, SDL3.SDL.Vertex[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            AssertVertex(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static void AssertVertex(SDL3.SDL.Vertex expected, SDL3.SDL.Vertex actual, string message)
    {
        AssertFPoint(expected.Position, actual.Position, $"{message} Position.");
        AssertFColor(expected.Color, actual.Color, $"{message} Color.");
        AssertFPoint(expected.TexCoord, actual.TexCoord, $"{message} TexCoord.");
    }

    private static void AssertFColor(SDL3.SDL.FColor expected, SDL3.SDL.FColor actual, string message)
    {
        TestAssert.Equal(expected.R, actual.R, $"{message} R.");
        TestAssert.Equal(expected.G, actual.G, $"{message} G.");
        TestAssert.Equal(expected.B, actual.B, $"{message} B.");
        TestAssert.Equal(expected.A, actual.A, $"{message} A.");
    }

    private static void AssertInts(int[] expected, int[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static void AssertFloats(float[] expected, float[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static void AssertFColors(SDL3.SDL.FColor[] expected, SDL3.SDL.FColor[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            AssertFColor(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static void AssertTextureRenderPointers(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");
        TestAssert.Equal(srcrect, capturedSourceRectPointer, $"{message} must forward source pointer.");
        TestAssert.Equal(dstrect, capturedDestinationRectPointer, $"{message} must forward destination pointer.");
    }

    private static void AssertTextureSourceRectAndDestinationPointer(IntPtr renderer, IntPtr texture, SDL3.SDL.FRect srcrect, IntPtr dstrect, string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");
        AssertFRect(srcrect, capturedSourceFRect, $"{message} must forward source rect.");
        TestAssert.Equal(dstrect, capturedDestinationRectPointer, $"{message} must forward destination pointer.");
    }

    private static void AssertTextureSourcePointerAndDestinationRect(IntPtr renderer, IntPtr texture, IntPtr srcrect, SDL3.SDL.FRect dstrect, string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");
        TestAssert.Equal(srcrect, capturedSourceRectPointer, $"{message} must forward source pointer.");
        AssertFRect(dstrect, capturedDestinationFRect, $"{message} must forward destination rect.");
    }

    private static void AssertTextureRects(IntPtr renderer, IntPtr texture, SDL3.SDL.FRect srcrect, SDL3.SDL.FRect dstrect, string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");
        AssertFRect(srcrect, capturedSourceFRect, $"{message} must forward source rect.");
        AssertFRect(dstrect, capturedDestinationFRect, $"{message} must forward destination rect.");
    }

    private static void AssertGeometryBase(IntPtr renderer, IntPtr texture, int numVertices, int numIndices, string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");
        TestAssert.Equal(numVertices, capturedNumVertices, $"{message} must forward vertex count.");
        TestAssert.Equal(numIndices, capturedNumIndices, $"{message} must forward index count.");
    }

    private static void AssertRawPointerGeometry(IntPtr renderer, IntPtr texture, IntPtr xy, int xyStride, IntPtr color, int colorStride, IntPtr uv, int uvStride, int numVertices, IntPtr indices, int numIndices, int sizeIndices, string message)
    {
        AssertGeometryBase(renderer, texture, numVertices, numIndices, message);
        TestAssert.Equal(xy, capturedXYPointer, $"{message} must forward xy pointer.");
        TestAssert.Equal(xyStride, capturedXYStride, $"{message} must forward xy stride.");
        TestAssert.Equal(color, capturedColorPointer, $"{message} must forward color pointer.");
        TestAssert.Equal(colorStride, capturedColorStride, $"{message} must forward color stride.");
        TestAssert.Equal(uv, capturedUVPointer, $"{message} must forward uv pointer.");
        TestAssert.Equal(uvStride, capturedUVStride, $"{message} must forward uv stride.");
        TestAssert.Equal(indices, capturedIndicesPointer, $"{message} must forward indices pointer.");
        TestAssert.Equal(sizeIndices, capturedSizeIndices, $"{message} must forward index size.");
    }

    private static void AssertRawArrayGeometry(float[] xy, SDL3.SDL.FColor[] colors, float[] uv, string message)
    {
        AssertFloats(xy, capturedXY, $"{message} must forward xy.");
        AssertFColors(colors, capturedColors, $"{message} must forward colors.");
        AssertFloats(uv, capturedUV, $"{message} must forward uv.");
    }

    private static void AssertDebugText(IntPtr renderer, float x, float y, string text, string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(x, capturedX, $"{message} must forward x.");
        TestAssert.Equal(y, capturedY, $"{message} must forward y.");
        TestAssert.Equal(text, capturedText, $"{message} must forward text.");
    }

    private static void AssertRotated(double angle, IntPtr center, SDL3.SDL.FlipMode flip, string message)
    {
        TestAssert.Equal(angle, capturedAngle, $"{message} must forward angle.");
        TestAssert.Equal(center, capturedCenterPointer, $"{message} must forward center pointer.");
        TestAssert.Equal(flip, capturedFlipMode, $"{message} must forward flip mode.");
    }

    private static void AssertGridDimensions(float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, string message)
    {
        TestAssert.Equal(leftWidth, capturedLeftWidth, $"{message} must forward left width.");
        TestAssert.Equal(rightWidth, capturedRightWidth, $"{message} must forward right width.");
        TestAssert.Equal(topHeight, capturedTopHeight, $"{message} must forward top height.");
        TestAssert.Equal(bottomHeight, capturedBottomHeight, $"{message} must forward bottom height.");
        TestAssert.Equal(scale, capturedScale, $"{message} must forward scale.");
    }

    private static void AssertAffineState(
        IntPtr renderer,
        IntPtr texture,
        bool sourceUsesRect,
        IntPtr sourcePointer,
        SDL3.SDL.FRect sourceRect,
        bool originUsesRect,
        IntPtr originPointer,
        SDL3.SDL.FRect originRect,
        bool rightUsesRect,
        IntPtr rightPointer,
        SDL3.SDL.FRect rightRect,
        bool downUsesRect,
        IntPtr downPointer,
        SDL3.SDL.FRect downRect,
        string message)
    {
        TestAssert.Equal(renderer, capturedRenderer, $"{message} must forward renderer.");
        TestAssert.Equal(texture, capturedTexture, $"{message} must forward texture.");

        if (sourceUsesRect)
        {
            AssertFRect(sourceRect, capturedSourceFRect, $"{message} must forward source rect.");
        }
        else
        {
            TestAssert.Equal(sourcePointer, capturedSourceRectPointer, $"{message} must forward source pointer.");
        }

        if (originUsesRect)
        {
            AssertFRect(originRect, capturedOriginFRect, $"{message} must forward origin rect.");
        }
        else
        {
            TestAssert.Equal(originPointer, capturedOriginPointer, $"{message} must forward origin pointer.");
        }

        if (rightUsesRect)
        {
            AssertFRect(rightRect, capturedRightFRect, $"{message} must forward right rect.");
        }
        else
        {
            TestAssert.Equal(rightPointer, capturedRightPointer, $"{message} must forward right pointer.");
        }

        if (downUsesRect)
        {
            AssertFRect(downRect, capturedDownFRect, $"{message} must forward down rect.");
        }
        else
        {
            TestAssert.Equal(downPointer, capturedDownPointer, $"{message} must forward down pointer.");
        }
    }

    private static void AssertBytes(byte[] expected, byte[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], actual[i], $"{message} [{i}].");
        }
    }

    private static int FloatElementCount(int count, int stride, int elementWidth)
    {
        return count <= 0 ? 0 : ((count - 1) * (stride / sizeof(float))) + elementWidth;
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

    private static void AssertYuvPlanes(IntPtr yplane, int ypitch, IntPtr uplane, int upitch, IntPtr vplane, int vpitch, string message)
    {
        TestAssert.Equal(yplane, capturedYPlane, $"{message} must forward Y plane.");
        TestAssert.Equal(ypitch, capturedYPitch, $"{message} must forward Y pitch.");
        TestAssert.Equal(uplane, capturedUPlane, $"{message} must forward U plane.");
        TestAssert.Equal(upitch, capturedUPitch, $"{message} must forward U pitch.");
        TestAssert.Equal(vplane, capturedVPlane, $"{message} must forward V plane.");
        TestAssert.Equal(vpitch, capturedVPitch, $"{message} must forward V pitch.");
    }

    private static void AssertNvPlanes(IntPtr yplane, int ypitch, IntPtr uvplane, int uvpitch, string message)
    {
        TestAssert.Equal(yplane, capturedYPlane, $"{message} must forward Y plane.");
        TestAssert.Equal(ypitch, capturedYPitch, $"{message} must forward Y pitch.");
        TestAssert.Equal(uvplane, capturedUVPlane, $"{message} must forward UV plane.");
        TestAssert.Equal(uvpitch, capturedUVPitch, $"{message} must forward UV pitch.");
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedPointer = IntPtr.Zero;
        capturedRenderer = IntPtr.Zero;
        capturedWindow = IntPtr.Zero;
        capturedDevice = IntPtr.Zero;
        capturedSurface = IntPtr.Zero;
        capturedTexture = IntPtr.Zero;
        capturedPalette = IntPtr.Zero;
        capturedRectPointer = IntPtr.Zero;
        capturedSourceRectPointer = IntPtr.Zero;
        capturedDestinationRectPointer = IntPtr.Zero;
        capturedCenterPointer = IntPtr.Zero;
        capturedOriginPointer = IntPtr.Zero;
        capturedRightPointer = IntPtr.Zero;
        capturedDownPointer = IntPtr.Zero;
        capturedXYPointer = IntPtr.Zero;
        capturedColorPointer = IntPtr.Zero;
        capturedUVPointer = IntPtr.Zero;
        capturedIndicesPointer = IntPtr.Zero;
        capturedPixels = IntPtr.Zero;
        capturedState = IntPtr.Zero;
        capturedCreateInfo = IntPtr.Zero;
        capturedData = IntPtr.Zero;
        capturedYPlane = IntPtr.Zero;
        capturedUPlane = IntPtr.Zero;
        capturedVPlane = IntPtr.Zero;
        capturedUVPlane = IntPtr.Zero;
        nextPixels = IntPtr.Zero;
        nextSurface = IntPtr.Zero;
        nextInt = 0;
        capturedIndex = 0;
        capturedWidth = 0;
        capturedHeight = 0;
        nextWidth = 0;
        nextHeight = 0;
        capturedCount = 0;
        capturedXYStride = 0;
        capturedColorStride = 0;
        capturedUVStride = 0;
        capturedNumVertices = 0;
        capturedNumIndices = 0;
        capturedSizeIndices = 0;
        capturedVSync = 0;
        nextVSync = 0;
        capturedPitch = 0;
        capturedYPitch = 0;
        capturedUPitch = 0;
        capturedVPitch = 0;
        capturedUVPitch = 0;
        nextPitch = 0;
        nextUInt = 0;
        capturedProps = 0;
        capturedEventType = 0;
        capturedWaitStageMask = 0;
        capturedSlotIndex = 0;
        capturedLength = 0;
        nextEventType = 0;
        capturedWaitSemaphore = 0;
        capturedSignalSemaphore = 0;
        nextBool = false;
        capturedTitle = null;
        capturedName = null;
        capturedText = null;
        capturedBytes = null;
        capturedFPoints = null;
        capturedFRects = null;
        capturedVertices = null;
        capturedIntIndices = null;
        capturedXY = null;
        capturedColors = null;
        capturedUV = null;
        capturedColorFloats = null;
        capturedByteIndices = null;
        capturedRect = default;
        nextRect = default;
        capturedFRect = default;
        capturedSourceFRect = default;
        capturedDestinationFRect = default;
        capturedCenterFPoint = default;
        capturedOriginFRect = default;
        capturedRightFRect = default;
        capturedDownFRect = default;
        nextFRect = default;
        capturedWindowFlags = default;
        capturedPixelFormat = default;
        capturedTextureAccess = default;
        capturedPresentationMode = default;
        nextPresentationMode = default;
        capturedR = 0;
        capturedG = 0;
        capturedB = 0;
        capturedAlpha = 0;
        nextR = 0;
        nextG = 0;
        nextB = 0;
        nextAlpha = 0;
        capturedFR = 0.0f;
        capturedFG = 0.0f;
        capturedFB = 0.0f;
        capturedFAlpha = 0.0f;
        nextFR = 0.0f;
        nextFG = 0.0f;
        nextFB = 0.0f;
        nextFAlpha = 0.0f;
        nextFloatWidth = 0.0f;
        nextFloatHeight = 0.0f;
        capturedX = 0.0f;
        capturedY = 0.0f;
        capturedWindowX = 0.0f;
        capturedWindowY = 0.0f;
        nextX = 0.0f;
        nextY = 0.0f;
        nextWindowX = 0.0f;
        nextWindowY = 0.0f;
        capturedScaleX = 0.0f;
        capturedScaleY = 0.0f;
        capturedScale = 0.0f;
        nextScaleX = 0.0f;
        nextScaleY = 0.0f;
        capturedColorScale = 0.0f;
        nextColorScale = 0.0f;
        capturedX1 = 0.0f;
        capturedY1 = 0.0f;
        capturedX2 = 0.0f;
        capturedY2 = 0.0f;
        capturedLeftWidth = 0.0f;
        capturedRightWidth = 0.0f;
        capturedTopHeight = 0.0f;
        capturedBottomHeight = 0.0f;
        capturedTileScale = 0.0f;
        capturedAngle = 0.0;
        capturedBlendMode = default;
        nextBlendMode = default;
        capturedFlipMode = default;
        capturedScaleMode = default;
        nextScaleMode = default;
        capturedTextureAddressModeU = default;
        capturedTextureAddressModeV = default;
        nextTextureAddressModeU = default;
        nextTextureAddressModeV = default;
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

    private static void AssertNativeBoolDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
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
