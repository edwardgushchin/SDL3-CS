using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.GPU.Gpu;

internal static class PInvokeTests
{
    private static SDL3.SDL.GPUShaderFormat capturedFormatFlags;
    private static string? capturedName;
    private static uint capturedProps;
    private static bool capturedDebugMode;
    private static int capturedIndex;
    private static IntPtr capturedDevice;
    private static IntPtr capturedResource;
    private static IntPtr capturedCommandBuffer;
    private static IntPtr capturedRenderPass;
    private static IntPtr capturedGraphicsPipeline;
    private static uint capturedSlotIndex;
    private static uint capturedFirstSlot;
    private static IntPtr capturedData;
    private static byte[]? capturedDataArray;
    private static uint capturedLength;
    private static IntPtr capturedBindings;
    private static uint capturedNumBindings;
    private static SDL3.SDL.GPUBufferBinding capturedBufferBinding;
    private static SDL3.SDL.GPUIndexElementSize capturedIndexElementSize;
    private static SDL3.SDL.GPUTextureSamplerBinding[]? capturedTextureSamplerBindings;
    private static IntPtr[]? capturedPointerArray;
    private static IntPtr capturedBuffer;
    private static uint capturedOffset;
    private static uint capturedDrawCount;
    private static uint capturedNumIndices;
    private static uint capturedNumInstances;
    private static uint capturedFirstIndex;
    private static int capturedVertexOffset;
    private static uint capturedFirstInstance;
    private static uint capturedNumVertices;
    private static uint capturedFirstVertex;
    private static IntPtr capturedComputePass;
    private static IntPtr capturedComputePipeline;
    private static SDL3.SDL.GPUStorageTextureReadWriteBinding[]? capturedStorageTextureBindings;
    private static uint capturedNumStorageTextureBindings;
    private static SDL3.SDL.GPUStorageBufferReadWriteBinding[]? capturedStorageBufferBindings;
    private static uint capturedNumStorageBufferBindings;
    private static uint capturedGroupCountX;
    private static uint capturedGroupCountY;
    private static uint capturedGroupCountZ;
    private static IntPtr capturedCopyPass;
    private static IntPtr capturedTransferBuffer;
    private static bool capturedCycle;
    private static SDL3.SDL.GPUTextureTransferInfo capturedTextureTransferInfo;
    private static SDL3.SDL.GPUTextureRegion capturedTextureRegion;
    private static SDL3.SDL.GPUTransferBufferLocation capturedTransferBufferLocation;
    private static SDL3.SDL.GPUBufferRegion capturedBufferRegion;
    private static SDL3.SDL.GPUTextureLocation capturedSourceTextureLocation;
    private static SDL3.SDL.GPUTextureLocation capturedDestinationTextureLocation;
    private static SDL3.SDL.GPUBufferLocation capturedSourceBufferLocation;
    private static SDL3.SDL.GPUBufferLocation capturedDestinationBufferLocation;
    private static uint capturedWidth;
    private static uint capturedHeight;
    private static uint capturedDepth;
    private static uint capturedSize;
    private static SDL3.SDL.GPUTextureFormat capturedGPUTextureFormat;
    private static SDL3.SDL.PixelFormat capturedPixelFormat;
    private static SDL3.SDL.PixelFormat nextPixelFormat;
    private static SDL3.SDL.GPUTextureFormat nextGPUTextureFormatValue;
    private static SDL3.SDL.GPUBlitInfo capturedBlitInfo;
    private static IntPtr capturedWindow;
    private static SDL3.SDL.GPUSwapchainComposition capturedSwapchainComposition;
    private static SDL3.SDL.GPUPresentMode capturedPresentMode;
    private static uint capturedAllowedFramesInFlight;
    private static IntPtr capturedSwapchainTexture;
    private static IntPtr nextSwapchainTexture;
    private static uint nextSwapchainTextureWidth;
    private static uint nextSwapchainTextureHeight;
    private static bool capturedWaitAll;
    private static IntPtr[]? capturedFencesArray;
    private static IntPtr capturedFences;
    private static uint capturedNumFences;
    private static IntPtr capturedFence;
    private static SDL3.SDL.GPUTextureType capturedTextureType;
    private static SDL3.SDL.GPUTextureUsageFlags capturedTextureUsage;
    private static SDL3.SDL.GPUSampleCount capturedSampleCount;
    private static uint capturedDepthOrLayerCount;
    private static IntPtr capturedColorTargetInfos;
    private static uint capturedNumColorTargets;
    private static IntPtr capturedDepthStencilTargetInfo;
    private static SDL3.SDL.GPUDepthStencilTargetInfo capturedDepthStencilInfo;
    private static SDL3.SDL.GPUViewport capturedViewport;
    private static SDL3.SDL.Rect capturedScissor;
    private static SDL3.SDL.FColor capturedBlendConstants;
    private static byte capturedStencilReference;
    private static string? capturedText;
    private static SDL3.SDL.GPUComputePipelineCreateInfo capturedComputePipelineInfo;
    private static SDL3.SDL.GPUGraphicsPipelineCreateInfo capturedGraphicsPipelineInfo;
    private static SDL3.SDL.GPUSamplerCreateInfo capturedSamplerInfo;
    private static SDL3.SDL.GPUShaderCreateInfo capturedShaderInfo;
    private static SDL3.SDL.GPUTextureCreateInfo capturedTextureInfo;
    private static SDL3.SDL.GPUBufferCreateInfo capturedBufferInfo;
    private static SDL3.SDL.GPUTransferBufferCreateInfo capturedTransferBufferInfo;
    private static IntPtr nextPointer;
    private static bool nextBool;
    private static int nextInt;
    private static uint nextUInt;
    private static SDL3.SDL.GPUShaderFormat nextShaderFormat;
    private static int capturedCallCount;

    public static void RunAll()
    {
        StructureAccessorProperties_SetFlagsAndManagedEntrypoint();
        GPUSupportsShaderFormats_ForwardsFormatNameAndReturnsNativeValue();
        GPUSupportsProperties_ForwardsPropsAndReturnsNativeValue();
        CreateGPUDevice_ForwardsArgumentsAndReturnsNativePointer();
        CreateGPUDeviceWithProperties_ForwardsPropsAndReturnsNativePointer();
        DestroyGPUDevice_ForwardsDevice();
        GetNumGPUDrivers_ReturnsNativeValue();
        SDL_GetGPUDriver_UsesExpectedNativeMetadata();
        GetGPUDriver_ReturnsStringAndEmpty();
        SDL_GetGPUDeviceDriver_UsesExpectedNativeMetadata();
        GetGPUDeviceDriver_ForwardsDeviceReturnsStringAndNull();
        GetGPUShaderFormats_ForwardsDeviceAndReturnsNativeValue();
        GetGPUDeviceProperties_ForwardsDeviceAndReturnsNativeValue();
        CreateGPUComputePipeline_ForwardsCreateInfoAndReturnsNativePointer();
        CreateGPUGraphicsPipeline_ForwardsCreateInfoAndReturnsNativePointer();
        CreateGPUSampler_ForwardsCreateInfoAndReturnsNativePointer();
        CreateGPUShader_ForwardsCreateInfoAndReturnsNativePointer();
        CreateGPUTexture_ForwardsCreateInfoAndReturnsNativePointer();
        CreateGPUBuffer_ForwardsCreateInfoAndReturnsNativePointer();
        CreateGPUTransferBuffer_ForwardsCreateInfoAndReturnsNativePointer();
        SetGPUBufferName_ForwardsDeviceResourceAndText();
        SetGPUTextureName_ForwardsDeviceResourceAndText();
        InsertGPUDebugLabel_ForwardsCommandBufferAndText();
        PushGPUDebugGroup_ForwardsCommandBufferAndText();
        PopGPUDebugGroup_ForwardsCommandBuffer();
        ReleaseGPUTexture_ForwardsDeviceAndTexture();
        ReleaseGPUSampler_ForwardsDeviceAndSampler();
        ReleaseGPUBuffer_ForwardsDeviceAndBuffer();
        ReleaseGPUTransferBuffer_ForwardsDeviceAndTransferBuffer();
        ReleaseGPUComputePipeline_ForwardsDeviceAndPipeline();
        ReleaseGPUShader_ForwardsDeviceAndShader();
        ReleaseGPUGraphicsPipeline_ForwardsDeviceAndPipeline();
        AcquireGPUCommandBuffer_ForwardsDeviceAndReturnsNativePointer();
        PushGPUVertexUniformDataPointer_ForwardsArguments();
        PushGPUVertexUniformDataArray_ForwardsArguments();
        PushGPUFragmentUniformDataPointer_ForwardsArguments();
        PushGPUFragmentUniformDataArray_ForwardsArguments();
        PushGPUComputeUniformDataPointer_ForwardsArguments();
        PushGPUComputeUniformDataArray_ForwardsArguments();
        BeginGPURenderPassPointer_ForwardsArgumentsAndReturnsNativePointer();
        BeginGPURenderPassColorTargetsPointer_ForwardsEmptyAndNonEmptyArrays();
        BeginGPURenderPassDepthStencil_ForwardsArgumentsAndReturnsNativePointer();
        BeginGPURenderPassColorTargetsDepthStencil_ForwardsEmptyAndNonEmptyArrays();
        BindGPUGraphicsPipeline_ForwardsRenderPassAndPipeline();
        SetGPUViewport_ForwardsViewport();
        SetGPUScissor_ForwardsScissor();
        SetGPUBlendConstants_ForwardsBlendConstants();
        SetGPUStencilReference_ForwardsReference();
        BindGPUVertexBuffersArray_ForwardsEmptyAndNonEmptyArrays();
        BindGPUVertexBuffersPointer_ForwardsArguments();
        BindGPUIndexBuffer_ForwardsBindingAndIndexElementSize();
        BindGPUVertexSamplersArray_ForwardsEmptyAndNonEmptyArrays();
        BindGPUVertexSamplersPointer_ForwardsArguments();
        BindGPUVertexStorageTextures_ForwardsPointerArray();
        BindGPUVertexStorageBuffers_ForwardsPointerArray();
        BindGPUFragmentSamplersArray_ForwardsArguments();
        BindGPUFragmentSamplersPointer_ForwardsArguments();
        BindGPUFragmentStorageTexturesArray_ForwardsPointerArray();
        BindGPUFragmentStorageTexturesPointer_ForwardsArguments();
        BindGPUFragmentStorageBuffersArray_ForwardsPointerArray();
        BindGPUFragmentStorageBuffersPointer_ForwardsArguments();
        DrawGPUIndexedPrimitives_ForwardsArguments();
        DrawGPUPrimitives_ForwardsArguments();
        DrawGPUPrimitivesIndirect_ForwardsArguments();
        DrawGPUIndexedPrimitivesIndirect_ForwardsArguments();
        EndGPURenderPass_ForwardsRenderPass();
        BeginGPUComputePass_ForwardsBindingsAndReturnsNativePointer();
        BindGPUComputePipeline_ForwardsComputePassAndPipeline();
        BindGPUComputeSamplersArray_ForwardsArguments();
        BindGPUComputeSamplersPointer_ForwardsArguments();
        BindGPUComputeStorageTexturesArray_ForwardsPointerArray();
        BindGPUComputeStorageTexturesPointer_ForwardsArguments();
        BindGPUComputeStorageBuffersArray_ForwardsPointerArray();
        BindGPUComputeStorageBuffersPointer_ForwardsArguments();
        DispatchGPUCompute_ForwardsGroupCounts();
        DispatchGPUComputeIndirect_ForwardsArguments();
        EndGPUComputePass_ForwardsComputePass();
        MapGPUTransferBuffer_ForwardsArgumentsAndReturnsNativePointer();
        UnmapGPUTransferBuffer_ForwardsDeviceAndTransferBuffer();
        BeginGPUCopyPass_ForwardsCommandBufferAndReturnsNativePointer();
        GetPixelFormatFromGPUTextureFormat_ForwardsTextureFormatAndReturnsPixelFormat();
        GetGPUTextureFormatFromPixelFormat_ForwardsPixelFormatAndReturnsTextureFormat();
        UploadToGPUTexture_ForwardsTransferInfoRegionAndCycle();
        UploadToGPUBuffer_ForwardsTransferLocationRegionAndCycle();
        CopyGPUTextureToTexture_ForwardsLocationsSizeAndCycle();
        CopyGPUBufferToBuffer_ForwardsLocationsSizeAndCycle();
        DownloadFromGPUTexture_ForwardsRegionAndTransferInfo();
        DownloadFromGPUBuffer_ForwardsRegionAndTransferLocation();
        EndGPUCopyPass_ForwardsCopyPass();
        GenerateMipmapsForGPUTexture_ForwardsCommandBufferAndTexture();
        BlitGPUTexture_ForwardsCommandBufferAndInfo();
        WindowSupportsGPUSwapchainComposition_ForwardsArgumentsAndReturnsNativeValue();
        WindowSupportsGPUPresentMode_ForwardsArgumentsAndReturnsNativeValue();
        ClaimWindowForGPUDevice_ForwardsDeviceAndWindow();
        ReleaseWindowFromGPUDevice_ForwardsDeviceAndWindow();
        SetGPUSwapchainParameters_ForwardsArgumentsAndReturnsNativeValue();
        SetGPUAllowedFramesInFlight_ForwardsArgumentsAndReturnsNativeValue();
        GetGPUSwapchainTextureFormat_ForwardsDeviceWindowAndReturnsTextureFormat();
        AcquireGPUSwapchainTexture_ForwardsArgumentsOutValuesAndReturnsNativeValue();
        WaitForGPUSwapchain_ForwardsDeviceWindowAndReturnsNativeValue();
        WaitAndAcquireGPUSwapchainTexture_ForwardsArgumentsOutValuesAndReturnsNativeValue();
        SubmitGPUCommandBuffer_ForwardsCommandBufferAndReturnsNativeValue();
        SubmitGPUCommandBufferAndAcquireFence_ForwardsCommandBufferAndReturnsFence();
        CancelGPUCommandBuffer_ForwardsCommandBufferAndReturnsNativeValue();
        WaitForGPUIdle_ForwardsDeviceAndReturnsNativeValue();
        WaitForGPUFencesArray_ForwardsArgumentsAndReturnsNativeValue();
        WaitForGPUFencesPointer_ForwardsArgumentsAndReturnsNativeValue();
        QueryGPUFence_ForwardsArgumentsAndReturnsNativeValue();
        ReleaseGPUFence_ForwardsArguments();
        GPUTextureFormatTexelBlockSize_ForwardsFormatAndReturnsNativeValue();
        GPUTextureSupportsFormat_ForwardsArgumentsAndReturnsNativeValue();
        GPUTextureSupportsSampleCount_ForwardsArgumentsAndReturnsNativeValue();
        CalculateGPUTextureFormatSize_ForwardsArgumentsAndReturnsNativeValue();
        GDKSuspendGPU_ForwardsDevice();
        GDKResumeGPU_ForwardsDevice();
    }

    public static void StructureAccessorProperties_SetFlagsAndManagedEntrypoint()
    {
        SDL3.SDL.GPUColorTargetBlendState blendState = default;
        TestAssert.Equal(false, blendState.EnableBlend, "GPUColorTargetBlendState.EnableBlend must default to false.");
        blendState.EnableBlend = true;
        TestAssert.Equal(true, blendState.EnableBlend, "GPUColorTargetBlendState.EnableBlend must read true after setting true.");
        blendState.EnableBlend = false;
        TestAssert.Equal(false, blendState.EnableBlend, "GPUColorTargetBlendState.EnableBlend must read false after setting false.");
        TestAssert.Equal(false, blendState.EnableColorWriteMask, "GPUColorTargetBlendState.EnableColorWriteMask must default to false.");
        blendState.EnableColorWriteMask = true;
        TestAssert.Equal(true, blendState.EnableColorWriteMask, "GPUColorTargetBlendState.EnableColorWriteMask must read true after setting true.");
        blendState.EnableColorWriteMask = false;
        TestAssert.Equal(false, blendState.EnableColorWriteMask, "GPUColorTargetBlendState.EnableColorWriteMask must read false after setting false.");

        SDL3.SDL.GPUColorTargetInfo colorTargetInfo = default;
        TestAssert.Equal(false, colorTargetInfo.Cycle, "GPUColorTargetInfo.Cycle must default to false.");
        colorTargetInfo.Cycle = true;
        TestAssert.Equal(true, colorTargetInfo.Cycle, "GPUColorTargetInfo.Cycle must read true after setting true.");
        colorTargetInfo.Cycle = false;
        TestAssert.Equal(false, colorTargetInfo.Cycle, "GPUColorTargetInfo.Cycle must read false after setting false.");
        TestAssert.Equal(false, colorTargetInfo.CycleResolveTexture, "GPUColorTargetInfo.CycleResolveTexture must default to false.");
        colorTargetInfo.CycleResolveTexture = true;
        TestAssert.Equal(true, colorTargetInfo.CycleResolveTexture, "GPUColorTargetInfo.CycleResolveTexture must read true after setting true.");
        colorTargetInfo.CycleResolveTexture = false;
        TestAssert.Equal(false, colorTargetInfo.CycleResolveTexture, "GPUColorTargetInfo.CycleResolveTexture must read false after setting false.");

        SDL3.SDL.GPUDepthStencilState depthStencilState = default;
        TestAssert.Equal(false, depthStencilState.EnableDepthTest, "GPUDepthStencilState.EnableDepthTest must default to false.");
        depthStencilState.EnableDepthTest = true;
        TestAssert.Equal(true, depthStencilState.EnableDepthTest, "GPUDepthStencilState.EnableDepthTest must read true after setting true.");
        depthStencilState.EnableDepthTest = false;
        TestAssert.Equal(false, depthStencilState.EnableDepthTest, "GPUDepthStencilState.EnableDepthTest must read false after setting false.");
        TestAssert.Equal(false, depthStencilState.EnableDepthWrite, "GPUDepthStencilState.EnableDepthWrite must default to false.");
        depthStencilState.EnableDepthWrite = true;
        TestAssert.Equal(true, depthStencilState.EnableDepthWrite, "GPUDepthStencilState.EnableDepthWrite must read true after setting true.");
        depthStencilState.EnableDepthWrite = false;
        TestAssert.Equal(false, depthStencilState.EnableDepthWrite, "GPUDepthStencilState.EnableDepthWrite must read false after setting false.");
        TestAssert.Equal(false, depthStencilState.EnableStencilTest, "GPUDepthStencilState.EnableStencilTest must default to false.");
        depthStencilState.EnableStencilTest = true;
        TestAssert.Equal(true, depthStencilState.EnableStencilTest, "GPUDepthStencilState.EnableStencilTest must read true after setting true.");
        depthStencilState.EnableStencilTest = false;
        TestAssert.Equal(false, depthStencilState.EnableStencilTest, "GPUDepthStencilState.EnableStencilTest must read false after setting false.");

        SDL3.SDL.GPUGraphicsPipelineTargetInfo targetInfo = default;
        TestAssert.Equal(false, targetInfo.HasDepthStencilTarget, "GPUGraphicsPipelineTargetInfo.HasDepthStencilTarget must default to false.");
        targetInfo.HasDepthStencilTarget = true;
        TestAssert.Equal(true, targetInfo.HasDepthStencilTarget, "GPUGraphicsPipelineTargetInfo.HasDepthStencilTarget must read true after setting true.");
        targetInfo.HasDepthStencilTarget = false;
        TestAssert.Equal(false, targetInfo.HasDepthStencilTarget, "GPUGraphicsPipelineTargetInfo.HasDepthStencilTarget must read false after setting false.");

        SDL3.SDL.GPURasterizerState rasterizerState = default;
        TestAssert.Equal(false, rasterizerState.EnableDepthBias, "GPURasterizerState.EnableDepthBias must default to false.");
        rasterizerState.EnableDepthBias = true;
        TestAssert.Equal(true, rasterizerState.EnableDepthBias, "GPURasterizerState.EnableDepthBias must read true after setting true.");
        rasterizerState.EnableDepthBias = false;
        TestAssert.Equal(false, rasterizerState.EnableDepthBias, "GPURasterizerState.EnableDepthBias must read false after setting false.");
        TestAssert.Equal(false, rasterizerState.EnableDepthClip, "GPURasterizerState.EnableDepthClip must default to false.");
        rasterizerState.EnableDepthClip = true;
        TestAssert.Equal(true, rasterizerState.EnableDepthClip, "GPURasterizerState.EnableDepthClip must read true after setting true.");
        rasterizerState.EnableDepthClip = false;
        TestAssert.Equal(false, rasterizerState.EnableDepthClip, "GPURasterizerState.EnableDepthClip must read false after setting false.");

        SDL3.SDL.GPUSamplerCreateInfo samplerInfo = default;
        TestAssert.Equal(false, samplerInfo.EnableAnisotropy, "GPUSamplerCreateInfo.EnableAnisotropy must default to false.");
        samplerInfo.EnableAnisotropy = true;
        TestAssert.Equal(true, samplerInfo.EnableAnisotropy, "GPUSamplerCreateInfo.EnableAnisotropy must read true after setting true.");
        samplerInfo.EnableAnisotropy = false;
        TestAssert.Equal(false, samplerInfo.EnableAnisotropy, "GPUSamplerCreateInfo.EnableAnisotropy must read false after setting false.");
        TestAssert.Equal(false, samplerInfo.EnableCompare, "GPUSamplerCreateInfo.EnableCompare must default to false.");
        samplerInfo.EnableCompare = true;
        TestAssert.Equal(true, samplerInfo.EnableCompare, "GPUSamplerCreateInfo.EnableCompare must read true after setting true.");
        samplerInfo.EnableCompare = false;
        TestAssert.Equal(false, samplerInfo.EnableCompare, "GPUSamplerCreateInfo.EnableCompare must read false after setting false.");

        SDL3.SDL.GPUShaderCreateInfo shaderInfo = default;
        shaderInfo.Entrypoint = "main";
        TestAssert.Equal("main", shaderInfo.Entrypoint, "GPUShaderCreateInfo.Entrypoint must return the managed string stored by the setter.");
    }

    public static void GPUSupportsShaderFormats_ForwardsFormatNameAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GPUSupportsShaderFormats");
        AssertSdlLibraryImport(nativeMethod, "SDL_GPUSupportsShaderFormats");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("GPUSupportsShaderFormatsNativeFunction", nameof(CaptureGPUSupportsShaderFormats));

        SDL3.SDL.GPUShaderFormat format = SDL3.SDL.GPUShaderFormat.SPIRV | SDL3.SDL.GPUShaderFormat.DXIL;
        bool result = SDL3.SDL.GPUSupportsShaderFormats(format, "vulkan");

        TestAssert.Equal(false, result, "SDL.GPUSupportsShaderFormats must return native bool value.");
        TestAssert.Equal(format, capturedFormatFlags, "SDL.GPUSupportsShaderFormats must forward format flags.");
        TestAssert.Equal("vulkan", capturedName, "SDL.GPUSupportsShaderFormats must forward driver name.");

        nextBool = true;
        result = SDL3.SDL.GPUSupportsShaderFormats(SDL3.SDL.GPUShaderFormat.MSL, null);

        TestAssert.Equal(true, result, "SDL.GPUSupportsShaderFormats must return native bool value for null name.");
        TestAssert.Equal(SDL3.SDL.GPUShaderFormat.MSL, capturedFormatFlags, "SDL.GPUSupportsShaderFormats must forward format flags for null name.");
        TestAssert.Equal<string?>(null, capturedName, "SDL.GPUSupportsShaderFormats must forward null name.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GPUSupportsShaderFormats must call native hook for both branches.");
    }

    public static void GPUSupportsProperties_ForwardsPropsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GPUSupportsProperties");
        AssertSdlLibraryImport(nativeMethod, "SDL_GPUSupportsProperties");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("GPUSupportsPropertiesNativeFunction", nameof(CapturePropsBool));

        bool result = SDL3.SDL.GPUSupportsProperties(42);

        TestAssert.Equal(true, result, "SDL.GPUSupportsProperties must return native bool value.");
        TestAssert.Equal(42u, capturedProps, "SDL.GPUSupportsProperties must forward props.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GPUSupportsProperties must call native hook once.");
    }

    public static void CreateGPUDevice_ForwardsArgumentsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUDevice");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUDevice");
        AssertBoolParameterMarshal(nativeMethod, "debugMode", UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)1001;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUDeviceNativeFunction", nameof(CaptureCreateGPUDevice));

        IntPtr result = SDL3.SDL.CreateGPUDevice(SDL3.SDL.GPUShaderFormat.DXIL, true, "direct3d12");

        TestAssert.Equal((IntPtr)1001, result, "SDL.CreateGPUDevice must return native device pointer.");
        TestAssert.Equal(SDL3.SDL.GPUShaderFormat.DXIL, capturedFormatFlags, "SDL.CreateGPUDevice must forward format flags.");
        TestAssert.Equal(true, capturedDebugMode, "SDL.CreateGPUDevice must forward debug mode.");
        TestAssert.Equal("direct3d12", capturedName, "SDL.CreateGPUDevice must forward driver name.");

        result = SDL3.SDL.CreateGPUDevice(SDL3.SDL.GPUShaderFormat.SPIRV, false, null);

        TestAssert.Equal((IntPtr)1001, result, "SDL.CreateGPUDevice must return native device pointer for null name.");
        TestAssert.Equal(false, capturedDebugMode, "SDL.CreateGPUDevice must forward false debug mode.");
        TestAssert.Equal<string?>(null, capturedName, "SDL.CreateGPUDevice must forward null name.");
        TestAssert.Equal(2, capturedCallCount, "SDL.CreateGPUDevice must call native hook for both branches.");
    }

    public static void CreateGPUDeviceWithProperties_ForwardsPropsAndReturnsNativePointer()
    {
        AssertPropsPointerMethod("CreateGPUDeviceWithProperties", "SDL_CreateGPUDeviceWithProperties", "CreateGPUDeviceWithPropertiesNativeFunction", 43, (IntPtr)1002);
    }

    public static void DestroyGPUDevice_ForwardsDevice()
    {
        AssertDeviceVoidMethod("DestroyGPUDevice", "SDL_DestroyGPUDevice", "DestroyGPUDeviceNativeFunction", (IntPtr)1101);
    }

    public static void GetNumGPUDrivers_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetNumGPUDrivers");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetNumGPUDrivers");

        ResetCaptureState();
        nextInt = 3;
        using NativeHookScope _ = NativeHookScope.Install("GetNumGPUDriversNativeFunction", nameof(CaptureNoArgumentInt));

        int result = SDL3.SDL.GetNumGPUDrivers();

        TestAssert.Equal(3, result, "SDL.GetNumGPUDrivers must return native driver count.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetNumGPUDrivers must call native hook once.");
    }

    public static void SDL_GetGPUDriver_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGPUDriver");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGPUDriver");
    }

    public static void GetGPUDriver_ReturnsStringAndEmpty()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetGPUDriverNativeFunction", nameof(CaptureGetGPUDriver));
        IntPtr name = Marshal.StringToCoTaskMemUTF8("vulkan");

        try
        {
            nextPointer = name;
            string result = SDL3.SDL.GetGPUDriver(1);

            TestAssert.Equal("vulkan", result, "SDL.GetGPUDriver must convert UTF-8 driver name.");
            TestAssert.Equal(1, capturedIndex, "SDL.GetGPUDriver must forward index.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGPUDriver(2);

            TestAssert.Equal("", result, "SDL.GetGPUDriver must return empty string for native null.");
            TestAssert.Equal(2, capturedIndex, "SDL.GetGPUDriver must forward index for null branch.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGPUDriver must call native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(name);
        }
    }

    public static void SDL_GetGPUDeviceDriver_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGPUDeviceDriver");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGPUDeviceDriver");
    }

    public static void GetGPUDeviceDriver_ForwardsDeviceReturnsStringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetGPUDeviceDriverNativeFunction", nameof(CaptureDevicePointer));
        IntPtr name = Marshal.StringToCoTaskMemUTF8("metal");

        try
        {
            nextPointer = name;
            string? result = SDL3.SDL.GetGPUDeviceDriver((IntPtr)1201);

            TestAssert.Equal("metal", result, "SDL.GetGPUDeviceDriver must convert UTF-8 driver name.");
            TestAssert.Equal((IntPtr)1201, capturedDevice, "SDL.GetGPUDeviceDriver must forward device.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetGPUDeviceDriver((IntPtr)1202);

            TestAssert.Equal<string?>(null, result, "SDL.GetGPUDeviceDriver must return null for native null.");
            TestAssert.Equal((IntPtr)1202, capturedDevice, "SDL.GetGPUDeviceDriver must forward device for null branch.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetGPUDeviceDriver must call native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(name);
        }
    }

    public static void GetGPUShaderFormats_ForwardsDeviceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGPUShaderFormats");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGPUShaderFormats");

        ResetCaptureState();
        nextShaderFormat = SDL3.SDL.GPUShaderFormat.SPIRV | SDL3.SDL.GPUShaderFormat.MSL;
        using NativeHookScope _ = NativeHookScope.Install("GetGPUShaderFormatsNativeFunction", nameof(CaptureDeviceShaderFormat));

        SDL3.SDL.GPUShaderFormat result = SDL3.SDL.GetGPUShaderFormats((IntPtr)1301);

        TestAssert.Equal(nextShaderFormat, result, "SDL.GetGPUShaderFormats must return native shader format flags.");
        TestAssert.Equal((IntPtr)1301, capturedDevice, "SDL.GetGPUShaderFormats must forward device.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGPUShaderFormats must call native hook once.");
    }

    public static void GetGPUDeviceProperties_ForwardsDeviceAndReturnsNativeValue()
    {
        AssertDeviceUIntMethod("GetGPUDeviceProperties", "SDL_GetGPUDeviceProperties", "GetGPUDevicePropertiesNativeFunction", (IntPtr)1302, 99);
    }

    public static void CreateGPUComputePipeline_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUComputePipeline");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUComputePipeline");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1401;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUComputePipelineNativeFunction", nameof(CaptureCreateGPUComputePipeline));
        SDL3.SDL.GPUComputePipelineCreateInfo createInfo = new() { Format = SDL3.SDL.GPUShaderFormat.SPIRV, ThreadcountX = 8, Props = 44 };

        IntPtr result = SDL3.SDL.CreateGPUComputePipeline((IntPtr)1303, in createInfo);

        TestAssert.Equal((IntPtr)1401, result, "SDL.CreateGPUComputePipeline must return native pipeline pointer.");
        TestAssert.Equal((IntPtr)1303, capturedDevice, "SDL.CreateGPUComputePipeline must forward device.");
        TestAssert.Equal(8u, capturedComputePipelineInfo.ThreadcountX, "SDL.CreateGPUComputePipeline must forward createinfo.");
    }

    public static void CreateGPUGraphicsPipeline_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUGraphicsPipeline");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUGraphicsPipeline");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1402;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUGraphicsPipelineNativeFunction", nameof(CaptureCreateGPUGraphicsPipeline));
        SDL3.SDL.GPUGraphicsPipelineCreateInfo createInfo = new() { VertexShader = (IntPtr)1, FragmentShader = (IntPtr)2, Props = 45 };

        IntPtr result = SDL3.SDL.CreateGPUGraphicsPipeline((IntPtr)1304, in createInfo);

        TestAssert.Equal((IntPtr)1402, result, "SDL.CreateGPUGraphicsPipeline must return native pipeline pointer.");
        TestAssert.Equal((IntPtr)1304, capturedDevice, "SDL.CreateGPUGraphicsPipeline must forward device.");
        TestAssert.Equal((IntPtr)1, capturedGraphicsPipelineInfo.VertexShader, "SDL.CreateGPUGraphicsPipeline must forward createinfo.");
    }

    public static void CreateGPUSampler_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUSampler");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUSampler");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1403;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUSamplerNativeFunction", nameof(CaptureCreateGPUSampler));
        SDL3.SDL.GPUSamplerCreateInfo createInfo = new() { MinFilter = SDL3.SDL.GPUFilter.Linear, MaxLod = 7 };

        IntPtr result = SDL3.SDL.CreateGPUSampler((IntPtr)1305, in createInfo);

        TestAssert.Equal((IntPtr)1403, result, "SDL.CreateGPUSampler must return native sampler pointer.");
        TestAssert.Equal((IntPtr)1305, capturedDevice, "SDL.CreateGPUSampler must forward device.");
        TestAssert.Equal(SDL3.SDL.GPUFilter.Linear, capturedSamplerInfo.MinFilter, "SDL.CreateGPUSampler must forward createinfo.");
    }

    public static void CreateGPUShader_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUShader");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUShader");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1404;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUShaderNativeFunction", nameof(CaptureCreateGPUShader));
        SDL3.SDL.GPUShaderCreateInfo createInfo = new() { Format = SDL3.SDL.GPUShaderFormat.DXIL, NumUniformBuffers = 5, Props = 46 };

        IntPtr result = SDL3.SDL.CreateGPUShader((IntPtr)1306, in createInfo);

        TestAssert.Equal((IntPtr)1404, result, "SDL.CreateGPUShader must return native shader pointer.");
        TestAssert.Equal((IntPtr)1306, capturedDevice, "SDL.CreateGPUShader must forward device.");
        TestAssert.Equal(5u, capturedShaderInfo.NumUniformBuffers, "SDL.CreateGPUShader must forward createinfo.");
    }

    public static void CreateGPUTexture_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUTexture");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1405;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUTextureNativeFunction", nameof(CaptureCreateGPUTexture));
        SDL3.SDL.GPUTextureCreateInfo createInfo = new() { Width = 640, Height = 480, Props = 47 };

        IntPtr result = SDL3.SDL.CreateGPUTexture((IntPtr)1307, in createInfo);

        TestAssert.Equal((IntPtr)1405, result, "SDL.CreateGPUTexture must return native texture pointer.");
        TestAssert.Equal((IntPtr)1307, capturedDevice, "SDL.CreateGPUTexture must forward device.");
        TestAssert.Equal(640u, capturedTextureInfo.Width, "SDL.CreateGPUTexture must forward createinfo.");
    }

    public static void CreateGPUBuffer_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUBuffer");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1406;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUBufferNativeFunction", nameof(CaptureCreateGPUBuffer));
        SDL3.SDL.GPUBufferCreateInfo createInfo = new() { Size = 2048, Props = 48 };

        IntPtr result = SDL3.SDL.CreateGPUBuffer((IntPtr)1308, in createInfo);

        TestAssert.Equal((IntPtr)1406, result, "SDL.CreateGPUBuffer must return native buffer pointer.");
        TestAssert.Equal((IntPtr)1308, capturedDevice, "SDL.CreateGPUBuffer must forward device.");
        TestAssert.Equal(2048u, capturedBufferInfo.Size, "SDL.CreateGPUBuffer must forward createinfo.");
    }

    public static void CreateGPUTransferBuffer_ForwardsCreateInfoAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUTransferBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUTransferBuffer");
        AssertByRefParameter(nativeMethod, "createinfo");

        ResetCaptureState();
        nextPointer = (IntPtr)1407;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUTransferBufferNativeFunction", nameof(CaptureCreateGPUTransferBuffer));
        SDL3.SDL.GPUTransferBufferCreateInfo createInfo = new() { Usage = SDL3.SDL.GPUTransferBufferUsage.Download, Size = 4096, Props = 49 };

        IntPtr result = SDL3.SDL.CreateGPUTransferBuffer((IntPtr)1309, in createInfo);

        TestAssert.Equal((IntPtr)1407, result, "SDL.CreateGPUTransferBuffer must return native transfer buffer pointer.");
        TestAssert.Equal((IntPtr)1309, capturedDevice, "SDL.CreateGPUTransferBuffer must forward device.");
        TestAssert.Equal(4096u, capturedTransferBufferInfo.Size, "SDL.CreateGPUTransferBuffer must forward createinfo.");
    }

    public static void SetGPUBufferName_ForwardsDeviceResourceAndText()
    {
        AssertDeviceResourceTextMethod("SetGPUBufferName", "SDL_SetGPUBufferName", "SetGPUBufferNameNativeFunction", "buffer");
    }

    public static void SetGPUTextureName_ForwardsDeviceResourceAndText()
    {
        AssertDeviceResourceTextMethod("SetGPUTextureName", "SDL_SetGPUTextureName", "SetGPUTextureNameNativeFunction", "texture");
    }

    public static void InsertGPUDebugLabel_ForwardsCommandBufferAndText()
    {
        AssertCommandTextMethod("InsertGPUDebugLabel", "SDL_InsertGPUDebugLabel", "InsertGPUDebugLabelNativeFunction");
    }

    public static void PushGPUDebugGroup_ForwardsCommandBufferAndText()
    {
        AssertCommandTextMethod("PushGPUDebugGroup", "SDL_PushGPUDebugGroup", "PushGPUDebugGroupNativeFunction");
    }

    public static void PopGPUDebugGroup_ForwardsCommandBuffer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PopGPUDebugGroup");
        AssertSdlLibraryImport(nativeMethod, "SDL_PopGPUDebugGroup");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("PopGPUDebugGroupNativeFunction", nameof(CaptureCommandVoid));

        SDL3.SDL.PopGPUDebugGroup((IntPtr)1501);

        TestAssert.Equal((IntPtr)1501, capturedCommandBuffer, "SDL.PopGPUDebugGroup must forward commandBuffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PopGPUDebugGroup must call native hook once.");
    }

    public static void ReleaseGPUTexture_ForwardsDeviceAndTexture()
    {
        AssertReleaseMethod("ReleaseGPUTexture", "SDL_ReleaseGPUTexture", "ReleaseGPUTextureNativeFunction", "texture");
    }

    public static void ReleaseGPUSampler_ForwardsDeviceAndSampler()
    {
        AssertReleaseMethod("ReleaseGPUSampler", "SDL_ReleaseGPUSampler", "ReleaseGPUSamplerNativeFunction", "sampler");
    }

    public static void ReleaseGPUBuffer_ForwardsDeviceAndBuffer()
    {
        AssertReleaseMethod("ReleaseGPUBuffer", "SDL_ReleaseGPUBuffer", "ReleaseGPUBufferNativeFunction", "buffer");
    }

    public static void ReleaseGPUTransferBuffer_ForwardsDeviceAndTransferBuffer()
    {
        AssertReleaseMethod("ReleaseGPUTransferBuffer", "SDL_ReleaseGPUTransferBuffer", "ReleaseGPUTransferBufferNativeFunction", "transferBuffer");
    }

    public static void ReleaseGPUComputePipeline_ForwardsDeviceAndPipeline()
    {
        AssertReleaseMethod("ReleaseGPUComputePipeline", "SDL_ReleaseGPUComputePipeline", "ReleaseGPUComputePipelineNativeFunction", "computePipeline");
    }

    public static void ReleaseGPUShader_ForwardsDeviceAndShader()
    {
        AssertReleaseMethod("ReleaseGPUShader", "SDL_ReleaseGPUShader", "ReleaseGPUShaderNativeFunction", "shader");
    }

    public static void ReleaseGPUGraphicsPipeline_ForwardsDeviceAndPipeline()
    {
        AssertReleaseMethod("ReleaseGPUGraphicsPipeline", "SDL_ReleaseGPUGraphicsPipeline", "ReleaseGPUGraphicsPipelineNativeFunction", "graphicsPipeline");
    }

    public static void AcquireGPUCommandBuffer_ForwardsDeviceAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AcquireGPUCommandBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_AcquireGPUCommandBuffer");

        ResetCaptureState();
        nextPointer = (IntPtr)1601;
        using NativeHookScope _ = NativeHookScope.Install("AcquireGPUCommandBufferNativeFunction", nameof(CaptureDevicePointer));

        IntPtr result = SDL3.SDL.AcquireGPUCommandBuffer((IntPtr)1602);

        TestAssert.Equal((IntPtr)1601, result, "SDL.AcquireGPUCommandBuffer must return native command buffer pointer.");
        TestAssert.Equal((IntPtr)1602, capturedDevice, "SDL.AcquireGPUCommandBuffer must forward device.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AcquireGPUCommandBuffer must call native hook once.");
    }

    public static void PushGPUVertexUniformDataPointer_ForwardsArguments()
    {
        AssertUniformPointerMethod("PushGPUVertexUniformData", "SDL_PushGPUVertexUniformData", "PushGPUVertexUniformDataPointerNativeFunction", InvokePushGPUVertexUniformDataPointer);
    }

    public static void PushGPUVertexUniformDataArray_ForwardsArguments()
    {
        AssertUniformArrayMethod("PushGPUVertexUniformData", "SDL_PushGPUVertexUniformData", "PushGPUVertexUniformDataArrayNativeFunction", InvokePushGPUVertexUniformDataArray);
    }

    public static void PushGPUFragmentUniformDataPointer_ForwardsArguments()
    {
        AssertUniformPointerMethod("PushGPUFragmentUniformData", "SDL_PushGPUFragmentUniformData", "PushGPUFragmentUniformDataPointerNativeFunction", InvokePushGPUFragmentUniformDataPointer);
    }

    public static void PushGPUFragmentUniformDataArray_ForwardsArguments()
    {
        AssertUniformArrayMethod("PushGPUFragmentUniformData", "SDL_PushGPUFragmentUniformData", "PushGPUFragmentUniformDataArrayNativeFunction", InvokePushGPUFragmentUniformDataArray);
    }

    public static void PushGPUComputeUniformDataPointer_ForwardsArguments()
    {
        AssertUniformPointerMethod("PushGPUComputeUniformData", "SDL_PushGPUComputeUniformData", "PushGPUComputeUniformDataPointerNativeFunction", InvokePushGPUComputeUniformDataPointer);
    }

    public static void PushGPUComputeUniformDataArray_ForwardsArguments()
    {
        AssertUniformArrayMethod("PushGPUComputeUniformData", "SDL_PushGPUComputeUniformData", "PushGPUComputeUniformDataArrayNativeFunction", InvokePushGPUComputeUniformDataArray);
    }

    public static void BeginGPURenderPassPointer_ForwardsArgumentsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod(
            "SDL_BeginGPURenderPass",
            typeof(IntPtr),
            typeof(IntPtr),
            typeof(uint),
            typeof(IntPtr));
        AssertSdlLibraryImport(nativeMethod, "SDL_BeginGPURenderPass");

        ResetCaptureState();
        nextPointer = (IntPtr)2101;
        using NativeHookScope _ = NativeHookScope.Install("BeginGPURenderPassPointerNativeFunction", nameof(CaptureBeginGPURenderPassPointer));

        IntPtr result = SDL3.SDL.BeginGPURenderPass((IntPtr)2102, (IntPtr)2103, 2, (IntPtr)2104);

        TestAssert.Equal((IntPtr)2101, result, "SDL.BeginGPURenderPass pointer overload must return native render pass.");
        TestAssert.Equal((IntPtr)2102, capturedCommandBuffer, "SDL.BeginGPURenderPass pointer overload must forward commandBuffer.");
        TestAssert.Equal((IntPtr)2103, capturedColorTargetInfos, "SDL.BeginGPURenderPass pointer overload must forward colorTargetInfos.");
        TestAssert.Equal<uint>(2, capturedNumColorTargets, "SDL.BeginGPURenderPass pointer overload must forward numColorTargets.");
        TestAssert.Equal((IntPtr)2104, capturedDepthStencilTargetInfo, "SDL.BeginGPURenderPass pointer overload must forward depthStencilTargetInfo.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BeginGPURenderPass pointer overload must call native hook once.");
    }

    public static void BeginGPURenderPassColorTargetsPointer_ForwardsEmptyAndNonEmptyArrays()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)2201;
        using NativeHookScope _ = NativeHookScope.Install("BeginGPURenderPassPointerNativeFunction", nameof(CaptureBeginGPURenderPassPointer));

        SDL3.SDL.GPUColorTargetInfo[] emptyTargets = Array.Empty<SDL3.SDL.GPUColorTargetInfo>();
        IntPtr emptyResult = SDL3.SDL.BeginGPURenderPass((IntPtr)2202, in emptyTargets, 0, (IntPtr)2203);

        TestAssert.Equal((IntPtr)2201, emptyResult, "SDL.BeginGPURenderPass array overload must return native render pass for empty arrays.");
        TestAssert.Equal(IntPtr.Zero, capturedColorTargetInfos, "SDL.BeginGPURenderPass array overload must pass null colorTargetInfos for empty arrays.");
        TestAssert.Equal<uint>(0, capturedNumColorTargets, "SDL.BeginGPURenderPass array overload must pass zero numColorTargets for empty arrays.");
        TestAssert.Equal((IntPtr)2203, capturedDepthStencilTargetInfo, "SDL.BeginGPURenderPass array overload must forward depthStencilTargetInfo for empty arrays.");

        ResetCaptureState();
        nextPointer = (IntPtr)2204;
        SDL3.SDL.GPUColorTargetInfo[] targets =
        [
            new()
            {
                Texture = (IntPtr)2205,
                MipLevel = 3,
                LayerOrDepthPlane = 4
            }
        ];

        IntPtr result = SDL3.SDL.BeginGPURenderPass((IntPtr)2206, in targets, 1, (IntPtr)2207);

        TestAssert.Equal((IntPtr)2204, result, "SDL.BeginGPURenderPass array overload must return native render pass for non-empty arrays.");
        TestAssert.True(capturedColorTargetInfos != IntPtr.Zero, "SDL.BeginGPURenderPass array overload must pin non-empty colorTargetInfos.");
        TestAssert.Equal<uint>(1, capturedNumColorTargets, "SDL.BeginGPURenderPass array overload must forward numColorTargets for non-empty arrays.");
        TestAssert.Equal((IntPtr)2207, capturedDepthStencilTargetInfo, "SDL.BeginGPURenderPass array overload must forward depthStencilTargetInfo for non-empty arrays.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BeginGPURenderPass array overload must call native hook once for non-empty arrays.");
    }

    public static void BeginGPURenderPassDepthStencil_ForwardsArgumentsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod(
            "SDL_BeginGPURenderPass",
            typeof(IntPtr),
            typeof(IntPtr),
            typeof(uint),
            typeof(SDL3.SDL.GPUDepthStencilTargetInfo).MakeByRefType());
        AssertSdlLibraryImport(nativeMethod, "SDL_BeginGPURenderPass");
        AssertByRefParameter(nativeMethod, "depthStencilTargetInfo");

        ResetCaptureState();
        nextPointer = (IntPtr)2301;
        using NativeHookScope _ = NativeHookScope.Install("BeginGPURenderPassDepthStencilNativeFunction", nameof(CaptureBeginGPURenderPassDepthStencil));
        SDL3.SDL.GPUDepthStencilTargetInfo depthStencil = CreateDepthStencilInfo((IntPtr)2302);

        IntPtr result = SDL3.SDL.BeginGPURenderPass((IntPtr)2303, (IntPtr)2304, 5, in depthStencil);

        TestAssert.Equal((IntPtr)2301, result, "SDL.BeginGPURenderPass depth-stencil overload must return native render pass.");
        TestAssert.Equal((IntPtr)2303, capturedCommandBuffer, "SDL.BeginGPURenderPass depth-stencil overload must forward commandBuffer.");
        TestAssert.Equal((IntPtr)2304, capturedColorTargetInfos, "SDL.BeginGPURenderPass depth-stencil overload must forward colorTargetInfos.");
        TestAssert.Equal<uint>(5, capturedNumColorTargets, "SDL.BeginGPURenderPass depth-stencil overload must forward numColorTargets.");
        TestAssert.Equal(depthStencil.Texture, capturedDepthStencilInfo.Texture, "SDL.BeginGPURenderPass depth-stencil overload must forward depthStencilTargetInfo.");
        TestAssert.Equal(depthStencil.ClearStencil, capturedDepthStencilInfo.ClearStencil, "SDL.BeginGPURenderPass depth-stencil overload must preserve depthStencilTargetInfo fields.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BeginGPURenderPass depth-stencil overload must call native hook once.");
    }

    public static void BeginGPURenderPassColorTargetsDepthStencil_ForwardsEmptyAndNonEmptyArrays()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)2401;
        using NativeHookScope _ = NativeHookScope.Install("BeginGPURenderPassDepthStencilNativeFunction", nameof(CaptureBeginGPURenderPassDepthStencil));
        SDL3.SDL.GPUColorTargetInfo[] emptyTargets = Array.Empty<SDL3.SDL.GPUColorTargetInfo>();
        SDL3.SDL.GPUDepthStencilTargetInfo emptyDepthStencil = CreateDepthStencilInfo((IntPtr)2402);

        IntPtr emptyResult = SDL3.SDL.BeginGPURenderPass((IntPtr)2403, in emptyTargets, 0, in emptyDepthStencil);

        TestAssert.Equal((IntPtr)2401, emptyResult, "SDL.BeginGPURenderPass array depth-stencil overload must return native render pass for empty arrays.");
        TestAssert.Equal(IntPtr.Zero, capturedColorTargetInfos, "SDL.BeginGPURenderPass array depth-stencil overload must pass null colorTargetInfos for empty arrays.");
        TestAssert.Equal<uint>(0, capturedNumColorTargets, "SDL.BeginGPURenderPass array depth-stencil overload must pass zero numColorTargets for empty arrays.");
        TestAssert.Equal(emptyDepthStencil.Texture, capturedDepthStencilInfo.Texture, "SDL.BeginGPURenderPass array depth-stencil overload must forward depthStencilTargetInfo for empty arrays.");

        ResetCaptureState();
        nextPointer = (IntPtr)2404;
        SDL3.SDL.GPUColorTargetInfo[] targets =
        [
            new()
            {
                Texture = (IntPtr)2405,
                MipLevel = 7,
                LayerOrDepthPlane = 8
            }
        ];
        SDL3.SDL.GPUDepthStencilTargetInfo depthStencil = CreateDepthStencilInfo((IntPtr)2406);

        IntPtr result = SDL3.SDL.BeginGPURenderPass((IntPtr)2407, in targets, 1, in depthStencil);

        TestAssert.Equal((IntPtr)2404, result, "SDL.BeginGPURenderPass array depth-stencil overload must return native render pass for non-empty arrays.");
        TestAssert.True(capturedColorTargetInfos != IntPtr.Zero, "SDL.BeginGPURenderPass array depth-stencil overload must pin non-empty colorTargetInfos.");
        TestAssert.Equal<uint>(1, capturedNumColorTargets, "SDL.BeginGPURenderPass array depth-stencil overload must forward numColorTargets for non-empty arrays.");
        TestAssert.Equal(depthStencil.Texture, capturedDepthStencilInfo.Texture, "SDL.BeginGPURenderPass array depth-stencil overload must forward depthStencilTargetInfo for non-empty arrays.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BeginGPURenderPass array depth-stencil overload must call native hook once for non-empty arrays.");
    }

    public static void BindGPUGraphicsPipeline_ForwardsRenderPassAndPipeline()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindGPUGraphicsPipeline");
        AssertSdlLibraryImport(nativeMethod, "SDL_BindGPUGraphicsPipeline");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUGraphicsPipelineNativeFunction", nameof(CaptureBindGPUGraphicsPipeline));

        SDL3.SDL.BindGPUGraphicsPipeline((IntPtr)2501, (IntPtr)2502);

        TestAssert.Equal((IntPtr)2501, capturedRenderPass, "SDL.BindGPUGraphicsPipeline must forward renderPass.");
        TestAssert.Equal((IntPtr)2502, capturedGraphicsPipeline, "SDL.BindGPUGraphicsPipeline must forward graphicsPipeline.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUGraphicsPipeline must call native hook once.");
    }

    public static void SetGPUViewport_ForwardsViewport()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGPUViewport");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetGPUViewport");
        AssertByRefParameter(nativeMethod, "viewport");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetGPUViewportNativeFunction", nameof(CaptureSetGPUViewport));
        SDL3.SDL.GPUViewport viewport = new() { X = 1, Y = 2, W = 3, H = 4, MinDepth = 0.25f, MaxDepth = 0.75f };

        SDL3.SDL.SetGPUViewport((IntPtr)2601, in viewport);

        TestAssert.Equal((IntPtr)2601, capturedRenderPass, "SDL.SetGPUViewport must forward renderPass.");
        TestAssert.Equal(viewport.W, capturedViewport.W, "SDL.SetGPUViewport must forward viewport fields.");
        TestAssert.Equal(viewport.MaxDepth, capturedViewport.MaxDepth, "SDL.SetGPUViewport must preserve viewport depth.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGPUViewport must call native hook once.");
    }

    public static void SetGPUScissor_ForwardsScissor()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGPUScissor");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetGPUScissor");
        AssertByRefParameter(nativeMethod, "scissor");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetGPUScissorNativeFunction", nameof(CaptureSetGPUScissor));
        SDL3.SDL.Rect scissor = new() { X = 5, Y = 6, W = 7, H = 8 };

        SDL3.SDL.SetGPUScissor((IntPtr)2701, in scissor);

        TestAssert.Equal((IntPtr)2701, capturedRenderPass, "SDL.SetGPUScissor must forward renderPass.");
        TestAssert.Equal(scissor.X, capturedScissor.X, "SDL.SetGPUScissor must forward scissor X.");
        TestAssert.Equal(scissor.H, capturedScissor.H, "SDL.SetGPUScissor must forward scissor H.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGPUScissor must call native hook once.");
    }

    public static void SetGPUBlendConstants_ForwardsBlendConstants()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGPUBlendConstants");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetGPUBlendConstants");
        AssertByRefParameter(nativeMethod, "blendConstants");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetGPUBlendConstantsNativeFunction", nameof(CaptureSetGPUBlendConstants));
        SDL3.SDL.FColor blendConstants = new(0.1f, 0.2f, 0.3f, 0.4f);

        SDL3.SDL.SetGPUBlendConstants((IntPtr)2801, in blendConstants);

        TestAssert.Equal((IntPtr)2801, capturedRenderPass, "SDL.SetGPUBlendConstants must forward renderPass.");
        TestAssert.Equal(blendConstants.R, capturedBlendConstants.R, "SDL.SetGPUBlendConstants must forward blend R.");
        TestAssert.Equal(blendConstants.A, capturedBlendConstants.A, "SDL.SetGPUBlendConstants must forward blend A.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGPUBlendConstants must call native hook once.");
    }

    public static void SetGPUStencilReference_ForwardsReference()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGPUStencilReference");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetGPUStencilReference");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetGPUStencilReferenceNativeFunction", nameof(CaptureSetGPUStencilReference));

        SDL3.SDL.SetGPUStencilReference((IntPtr)2901, 27);

        TestAssert.Equal((IntPtr)2901, capturedRenderPass, "SDL.SetGPUStencilReference must forward renderPass.");
        TestAssert.Equal((byte)27, capturedStencilReference, "SDL.SetGPUStencilReference must forward reference.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGPUStencilReference must call native hook once.");
    }

    public static void BindGPUVertexBuffersArray_ForwardsEmptyAndNonEmptyArrays()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUVertexBuffersPointerNativeFunction", nameof(CaptureRenderPassSlotPointer));

        SDL3.SDL.GPUBufferBinding[] emptyBindings = Array.Empty<SDL3.SDL.GPUBufferBinding>();
        SDL3.SDL.BindGPUVertexBuffers((IntPtr)3201, 4, emptyBindings, 0);

        TestAssert.Equal((IntPtr)3201, capturedRenderPass, "SDL.BindGPUVertexBuffers array overload must forward renderPass for empty arrays.");
        TestAssert.Equal<uint>(4, capturedFirstSlot, "SDL.BindGPUVertexBuffers array overload must forward firstSlot for empty arrays.");
        TestAssert.Equal(IntPtr.Zero, capturedBindings, "SDL.BindGPUVertexBuffers array overload must pass null bindings for empty arrays.");
        TestAssert.Equal<uint>(0, capturedNumBindings, "SDL.BindGPUVertexBuffers array overload must forward numBindings for empty arrays.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUVertexBuffers array overload must return after empty native call.");

        ResetCaptureState();
        SDL3.SDL.GPUBufferBinding[] bindings =
        [
            new() { Buffer = (IntPtr)3202, Offset = 12 }
        ];

        SDL3.SDL.BindGPUVertexBuffers((IntPtr)3203, 5, bindings, 1);

        TestAssert.Equal((IntPtr)3203, capturedRenderPass, "SDL.BindGPUVertexBuffers array overload must forward renderPass for non-empty arrays.");
        TestAssert.Equal<uint>(5, capturedFirstSlot, "SDL.BindGPUVertexBuffers array overload must forward firstSlot for non-empty arrays.");
        TestAssert.True(capturedBindings != IntPtr.Zero, "SDL.BindGPUVertexBuffers array overload must pin non-empty bindings.");
        TestAssert.Equal<uint>(1, capturedNumBindings, "SDL.BindGPUVertexBuffers array overload must forward numBindings for non-empty arrays.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUVertexBuffers array overload must call native hook once for non-empty arrays.");
    }

    public static void BindGPUVertexBuffersPointer_ForwardsArguments()
    {
        AssertRenderPassSlotPointerMethod("BindGPUVertexBuffers", "SDL_BindGPUVertexBuffers", "BindGPUVertexBuffersPointerNativeFunction", "bindings");
    }

    public static void BindGPUIndexBuffer_ForwardsBindingAndIndexElementSize()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindGPUIndexBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_BindGPUIndexBuffer");
        AssertByRefParameter(nativeMethod, "binding");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUIndexBufferNativeFunction", nameof(CaptureBindGPUIndexBuffer));
        SDL3.SDL.GPUBufferBinding binding = new() { Buffer = (IntPtr)3301, Offset = 24 };

        SDL3.SDL.BindGPUIndexBuffer((IntPtr)3302, in binding, SDL3.SDL.GPUIndexElementSize.IndexElementSize32Bit);

        TestAssert.Equal((IntPtr)3302, capturedRenderPass, "SDL.BindGPUIndexBuffer must forward renderPass.");
        TestAssert.Equal(binding.Buffer, capturedBufferBinding.Buffer, "SDL.BindGPUIndexBuffer must forward binding buffer.");
        TestAssert.Equal(binding.Offset, capturedBufferBinding.Offset, "SDL.BindGPUIndexBuffer must forward binding offset.");
        TestAssert.Equal(SDL3.SDL.GPUIndexElementSize.IndexElementSize32Bit, capturedIndexElementSize, "SDL.BindGPUIndexBuffer must forward indexElementSize.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUIndexBuffer must call native hook once.");
    }

    public static void BindGPUVertexSamplersArray_ForwardsEmptyAndNonEmptyArrays()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUVertexSamplersPointerNativeFunction", nameof(CaptureRenderPassSlotPointer));

        SDL3.SDL.GPUTextureSamplerBinding[] emptyBindings = Array.Empty<SDL3.SDL.GPUTextureSamplerBinding>();
        SDL3.SDL.BindGPUVertexSamplers((IntPtr)3401, 6, emptyBindings, 0);

        TestAssert.Equal((IntPtr)3401, capturedRenderPass, "SDL.BindGPUVertexSamplers array overload must forward renderPass for empty arrays.");
        TestAssert.Equal<uint>(6, capturedFirstSlot, "SDL.BindGPUVertexSamplers array overload must forward firstSlot for empty arrays.");
        TestAssert.Equal(IntPtr.Zero, capturedBindings, "SDL.BindGPUVertexSamplers array overload must pass null bindings for empty arrays.");
        TestAssert.Equal<uint>(0, capturedNumBindings, "SDL.BindGPUVertexSamplers array overload must forward numBindings for empty arrays.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUVertexSamplers array overload must return after empty native call.");

        ResetCaptureState();
        SDL3.SDL.GPUTextureSamplerBinding[] bindings =
        [
            new() { Texture = (IntPtr)3402, Sampler = (IntPtr)3403 }
        ];

        SDL3.SDL.BindGPUVertexSamplers((IntPtr)3404, 7, bindings, 1);

        TestAssert.Equal((IntPtr)3404, capturedRenderPass, "SDL.BindGPUVertexSamplers array overload must forward renderPass for non-empty arrays.");
        TestAssert.Equal<uint>(7, capturedFirstSlot, "SDL.BindGPUVertexSamplers array overload must forward firstSlot for non-empty arrays.");
        TestAssert.True(capturedBindings != IntPtr.Zero, "SDL.BindGPUVertexSamplers array overload must pin non-empty bindings.");
        TestAssert.Equal<uint>(1, capturedNumBindings, "SDL.BindGPUVertexSamplers array overload must forward numBindings for non-empty arrays.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUVertexSamplers array overload must call native hook once for non-empty arrays.");
    }

    public static void BindGPUVertexSamplersPointer_ForwardsArguments()
    {
        AssertRenderPassSlotPointerMethod("BindGPUVertexSamplers", "SDL_BindGPUVertexSamplers", "BindGPUVertexSamplersPointerNativeFunction", "textureSamplerBindings");
    }

    public static void BindGPUVertexStorageTextures_ForwardsPointerArray()
    {
        AssertRenderPassSlotPointerArrayMethod("BindGPUVertexStorageTextures", "SDL_BindGPUVertexStorageTextures", "BindGPUVertexStorageTexturesNativeFunction", InvokeBindGPUVertexStorageTextures);
    }

    public static void BindGPUVertexStorageBuffers_ForwardsPointerArray()
    {
        AssertRenderPassSlotPointerArrayMethod("BindGPUVertexStorageBuffers", "SDL_BindGPUVertexStorageBuffers", "BindGPUVertexStorageBuffersNativeFunction", InvokeBindGPUVertexStorageBuffers);
    }

    public static void BindGPUFragmentSamplersArray_ForwardsArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindGPUFragmentSamplers", typeof(IntPtr), typeof(uint), typeof(SDL3.SDL.GPUTextureSamplerBinding[]), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, "SDL_BindGPUFragmentSamplers");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUFragmentSamplersArrayNativeFunction", nameof(CaptureRenderPassSlotSamplerArray));
        SDL3.SDL.GPUTextureSamplerBinding[] bindings =
        [
            new() { Texture = (IntPtr)3501, Sampler = (IntPtr)3502 }
        ];

        SDL3.SDL.BindGPUFragmentSamplers((IntPtr)3503, 8, bindings, 1);

        TestAssert.Equal((IntPtr)3503, capturedRenderPass, "SDL.BindGPUFragmentSamplers array overload must forward renderPass.");
        TestAssert.Equal<uint>(8, capturedFirstSlot, "SDL.BindGPUFragmentSamplers array overload must forward firstSlot.");
        TestAssert.True(ReferenceEquals(bindings, capturedTextureSamplerBindings), "SDL.BindGPUFragmentSamplers array overload must forward textureSamplerBindings.");
        TestAssert.Equal<uint>(1, capturedNumBindings, "SDL.BindGPUFragmentSamplers array overload must forward numBindings.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUFragmentSamplers array overload must call native hook once.");
    }

    public static void BindGPUFragmentSamplersPointer_ForwardsArguments()
    {
        AssertRenderPassSlotPointerMethod("BindGPUFragmentSamplers", "SDL_BindGPUFragmentSamplers", "BindGPUFragmentSamplersPointerNativeFunction", "textureSamplerBindings");
    }

    public static void BindGPUFragmentStorageTexturesArray_ForwardsPointerArray()
    {
        AssertRenderPassSlotPointerArrayMethod("BindGPUFragmentStorageTextures", "SDL_BindGPUFragmentStorageTextures", "BindGPUFragmentStorageTexturesArrayNativeFunction", InvokeBindGPUFragmentStorageTexturesArray);
    }

    public static void BindGPUFragmentStorageTexturesPointer_ForwardsArguments()
    {
        AssertRenderPassSlotPointerMethod("BindGPUFragmentStorageTextures", "SDL_BindGPUFragmentStorageTextures", "BindGPUFragmentStorageTexturesPointerNativeFunction", "storageTextures");
    }

    public static void BindGPUFragmentStorageBuffersArray_ForwardsPointerArray()
    {
        AssertRenderPassSlotPointerArrayMethod("BindGPUFragmentStorageBuffers", "SDL_BindGPUFragmentStorageBuffers", "BindGPUFragmentStorageBuffersArrayNativeFunction", InvokeBindGPUFragmentStorageBuffersArray);
    }

    public static void BindGPUFragmentStorageBuffersPointer_ForwardsArguments()
    {
        AssertRenderPassSlotPointerMethod("BindGPUFragmentStorageBuffers", "SDL_BindGPUFragmentStorageBuffers", "BindGPUFragmentStorageBuffersPointerNativeFunction", "storageBuffers");
    }

    public static void DrawGPUIndexedPrimitives_ForwardsArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DrawGPUIndexedPrimitives");
        AssertSdlLibraryImport(nativeMethod, "SDL_DrawGPUIndexedPrimitives");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DrawGPUIndexedPrimitivesNativeFunction", nameof(CaptureDrawGPUIndexedPrimitives));

        SDL3.SDL.DrawGPUIndexedPrimitives((IntPtr)3601, 2, 3, 4, -5, 6);

        TestAssert.Equal((IntPtr)3601, capturedRenderPass, "SDL.DrawGPUIndexedPrimitives must forward renderPass.");
        TestAssert.Equal<uint>(2, capturedNumIndices, "SDL.DrawGPUIndexedPrimitives must forward numIndices.");
        TestAssert.Equal<uint>(3, capturedNumInstances, "SDL.DrawGPUIndexedPrimitives must forward numInstances.");
        TestAssert.Equal<uint>(4, capturedFirstIndex, "SDL.DrawGPUIndexedPrimitives must forward firstIndex.");
        TestAssert.Equal(-5, capturedVertexOffset, "SDL.DrawGPUIndexedPrimitives must forward vertexOffset.");
        TestAssert.Equal<uint>(6, capturedFirstInstance, "SDL.DrawGPUIndexedPrimitives must forward firstInstance.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DrawGPUIndexedPrimitives must call native hook once.");
    }

    public static void DrawGPUPrimitives_ForwardsArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DrawGPUPrimitives");
        AssertSdlLibraryImport(nativeMethod, "SDL_DrawGPUPrimitives");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DrawGPUPrimitivesNativeFunction", nameof(CaptureDrawGPUPrimitives));

        SDL3.SDL.DrawGPUPrimitives((IntPtr)3701, 7, 8, 9, 10);

        TestAssert.Equal((IntPtr)3701, capturedRenderPass, "SDL.DrawGPUPrimitives must forward renderPass.");
        TestAssert.Equal<uint>(7, capturedNumVertices, "SDL.DrawGPUPrimitives must forward numVertices.");
        TestAssert.Equal<uint>(8, capturedNumInstances, "SDL.DrawGPUPrimitives must forward numInstances.");
        TestAssert.Equal<uint>(9, capturedFirstVertex, "SDL.DrawGPUPrimitives must forward firstVertex.");
        TestAssert.Equal<uint>(10, capturedFirstInstance, "SDL.DrawGPUPrimitives must forward firstInstance.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DrawGPUPrimitives must call native hook once.");
    }

    public static void DrawGPUPrimitivesIndirect_ForwardsArguments()
    {
        AssertDrawIndirectMethod("DrawGPUPrimitivesIndirect", "SDL_DrawGPUPrimitivesIndirect", "DrawGPUPrimitivesIndirectNativeFunction", InvokeDrawGPUPrimitivesIndirect);
    }

    public static void DrawGPUIndexedPrimitivesIndirect_ForwardsArguments()
    {
        AssertDrawIndirectMethod("DrawGPUIndexedPrimitivesIndirect", "SDL_DrawGPUIndexedPrimitivesIndirect", "DrawGPUIndexedPrimitivesIndirectNativeFunction", InvokeDrawGPUIndexedPrimitivesIndirect);
    }

    public static void EndGPURenderPass_ForwardsRenderPass()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EndGPURenderPass");
        AssertSdlLibraryImport(nativeMethod, "SDL_EndGPURenderPass");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("EndGPURenderPassNativeFunction", nameof(CaptureRenderPassVoid));

        SDL3.SDL.EndGPURenderPass((IntPtr)3801);

        TestAssert.Equal((IntPtr)3801, capturedRenderPass, "SDL.EndGPURenderPass must forward renderPass.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EndGPURenderPass must call native hook once.");
    }

    public static void BeginGPUComputePass_ForwardsBindingsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BeginGPUComputePass");
        AssertSdlLibraryImport(nativeMethod, "SDL_BeginGPUComputePass");

        ResetCaptureState();
        nextPointer = (IntPtr)4201;
        using NativeHookScope _ = NativeHookScope.Install("BeginGPUComputePassNativeFunction", nameof(CaptureBeginGPUComputePass));
        SDL3.SDL.GPUStorageTextureReadWriteBinding[] textureBindings =
        [
            new() { Texture = (IntPtr)4202, MipLevel = 1, Layer = 2, Cycle = 1 }
        ];
        SDL3.SDL.GPUStorageBufferReadWriteBinding[] bufferBindings =
        [
            new() { Buffer = (IntPtr)4203, Cycle = 1 }
        ];

        IntPtr result = SDL3.SDL.BeginGPUComputePass((IntPtr)4204, textureBindings, 1, bufferBindings, 1);

        TestAssert.Equal((IntPtr)4201, result, "SDL.BeginGPUComputePass must return native compute pass.");
        TestAssert.Equal((IntPtr)4204, capturedCommandBuffer, "SDL.BeginGPUComputePass must forward commandBuffer.");
        TestAssert.True(ReferenceEquals(textureBindings, capturedStorageTextureBindings), "SDL.BeginGPUComputePass must forward storageTextureBindings.");
        TestAssert.Equal<uint>(1, capturedNumStorageTextureBindings, "SDL.BeginGPUComputePass must forward numStorageTextureBindings.");
        TestAssert.True(ReferenceEquals(bufferBindings, capturedStorageBufferBindings), "SDL.BeginGPUComputePass must forward storageBufferBindings.");
        TestAssert.Equal<uint>(1, capturedNumStorageBufferBindings, "SDL.BeginGPUComputePass must forward numStorageBufferBindings.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BeginGPUComputePass must call native hook once.");
    }

    public static void BindGPUComputePipeline_ForwardsComputePassAndPipeline()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindGPUComputePipeline");
        AssertSdlLibraryImport(nativeMethod, "SDL_BindGPUComputePipeline");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUComputePipelineNativeFunction", nameof(CaptureComputePassPipeline));

        SDL3.SDL.BindGPUComputePipeline((IntPtr)4301, (IntPtr)4302);

        TestAssert.Equal((IntPtr)4301, capturedComputePass, "SDL.BindGPUComputePipeline must forward computePass.");
        TestAssert.Equal((IntPtr)4302, capturedComputePipeline, "SDL.BindGPUComputePipeline must forward computePipeline.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUComputePipeline must call native hook once.");
    }

    public static void BindGPUComputeSamplersArray_ForwardsArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindGPUComputeSamplers", typeof(IntPtr), typeof(uint), typeof(SDL3.SDL.GPUTextureSamplerBinding[]), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, "SDL_BindGPUComputeSamplers");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BindGPUComputeSamplersArrayNativeFunction", nameof(CaptureComputePassSlotSamplerArray));
        SDL3.SDL.GPUTextureSamplerBinding[] bindings =
        [
            new() { Texture = (IntPtr)4401, Sampler = (IntPtr)4402 }
        ];

        SDL3.SDL.BindGPUComputeSamplers((IntPtr)4403, 11, bindings, 1);

        TestAssert.Equal((IntPtr)4403, capturedComputePass, "SDL.BindGPUComputeSamplers array overload must forward computePass.");
        TestAssert.Equal<uint>(11, capturedFirstSlot, "SDL.BindGPUComputeSamplers array overload must forward firstSlot.");
        TestAssert.True(ReferenceEquals(bindings, capturedTextureSamplerBindings), "SDL.BindGPUComputeSamplers array overload must forward textureSamplerBindings.");
        TestAssert.Equal<uint>(1, capturedNumBindings, "SDL.BindGPUComputeSamplers array overload must forward numBindings.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BindGPUComputeSamplers array overload must call native hook once.");
    }

    public static void BindGPUComputeSamplersPointer_ForwardsArguments()
    {
        AssertComputePassSlotPointerMethod("BindGPUComputeSamplers", "SDL_BindGPUComputeSamplers", "BindGPUComputeSamplersPointerNativeFunction", "textureSamplerBindings");
    }

    public static void BindGPUComputeStorageTexturesArray_ForwardsPointerArray()
    {
        AssertComputePassSlotPointerArrayMethod("BindGPUComputeStorageTextures", "SDL_BindGPUComputeStorageTextures", "BindGPUComputeStorageTexturesArrayNativeFunction", InvokeBindGPUComputeStorageTexturesArray);
    }

    public static void BindGPUComputeStorageTexturesPointer_ForwardsArguments()
    {
        AssertComputePassSlotPointerMethod("BindGPUComputeStorageTextures", "SDL_BindGPUComputeStorageTextures", "BindGPUComputeStorageTexturesPointerNativeFunction", "storageTextures");
    }

    public static void BindGPUComputeStorageBuffersArray_ForwardsPointerArray()
    {
        AssertComputePassSlotPointerArrayMethod("BindGPUComputeStorageBuffers", "SDL_BindGPUComputeStorageBuffers", "BindGPUComputeStorageBuffersArrayNativeFunction", InvokeBindGPUComputeStorageBuffersArray);
    }

    public static void BindGPUComputeStorageBuffersPointer_ForwardsArguments()
    {
        AssertComputePassSlotPointerMethod("BindGPUComputeStorageBuffers", "SDL_BindGPUComputeStorageBuffers", "BindGPUComputeStorageBuffersPointerNativeFunction", "storageBuffers");
    }

    public static void DispatchGPUCompute_ForwardsGroupCounts()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DispatchGPUCompute");
        AssertSdlLibraryImport(nativeMethod, "SDL_DispatchGPUCompute");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DispatchGPUComputeNativeFunction", nameof(CaptureDispatchGPUCompute));

        SDL3.SDL.DispatchGPUCompute((IntPtr)4501, 2, 3, 4);

        TestAssert.Equal((IntPtr)4501, capturedComputePass, "SDL.DispatchGPUCompute must forward computePass.");
        TestAssert.Equal<uint>(2, capturedGroupCountX, "SDL.DispatchGPUCompute must forward groupcountX.");
        TestAssert.Equal<uint>(3, capturedGroupCountY, "SDL.DispatchGPUCompute must forward groupcountY.");
        TestAssert.Equal<uint>(4, capturedGroupCountZ, "SDL.DispatchGPUCompute must forward groupcountZ.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DispatchGPUCompute must call native hook once.");
    }

    public static void DispatchGPUComputeIndirect_ForwardsArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DispatchGPUComputeIndirect");
        AssertSdlLibraryImport(nativeMethod, "SDL_DispatchGPUComputeIndirect");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DispatchGPUComputeIndirectNativeFunction", nameof(CaptureComputeIndirect));

        SDL3.SDL.DispatchGPUComputeIndirect((IntPtr)4601, (IntPtr)4602, 128);

        TestAssert.Equal((IntPtr)4601, capturedComputePass, "SDL.DispatchGPUComputeIndirect must forward computePass.");
        TestAssert.Equal((IntPtr)4602, capturedBuffer, "SDL.DispatchGPUComputeIndirect must forward buffer.");
        TestAssert.Equal<uint>(128, capturedOffset, "SDL.DispatchGPUComputeIndirect must forward offset.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DispatchGPUComputeIndirect must call native hook once.");
    }

    public static void EndGPUComputePass_ForwardsComputePass()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EndGPUComputePass");
        AssertSdlLibraryImport(nativeMethod, "SDL_EndGPUComputePass");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("EndGPUComputePassNativeFunction", nameof(CaptureComputePassVoid));

        SDL3.SDL.EndGPUComputePass((IntPtr)4701);

        TestAssert.Equal((IntPtr)4701, capturedComputePass, "SDL.EndGPUComputePass must forward computePass.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EndGPUComputePass must call native hook once.");
    }

    public static void MapGPUTransferBuffer_ForwardsArgumentsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_MapGPUTransferBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_MapGPUTransferBuffer");
        AssertBoolParameterMarshal(nativeMethod, "cycle", UnmanagedType.I1);

        ResetCaptureState();
        nextPointer = (IntPtr)5001;
        using NativeHookScope _ = NativeHookScope.Install("MapGPUTransferBufferNativeFunction", nameof(CaptureMapGPUTransferBuffer));

        IntPtr result = SDL3.SDL.MapGPUTransferBuffer((IntPtr)5002, (IntPtr)5003, true);

        TestAssert.Equal((IntPtr)5001, result, "SDL.MapGPUTransferBuffer must return native mapped pointer.");
        TestAssert.Equal((IntPtr)5002, capturedDevice, "SDL.MapGPUTransferBuffer must forward device.");
        TestAssert.Equal((IntPtr)5003, capturedTransferBuffer, "SDL.MapGPUTransferBuffer must forward transferBuffer.");
        TestAssert.True(capturedCycle, "SDL.MapGPUTransferBuffer must forward cycle.");
        TestAssert.Equal(1, capturedCallCount, "SDL.MapGPUTransferBuffer must call native hook once.");
    }

    public static void UnmapGPUTransferBuffer_ForwardsDeviceAndTransferBuffer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UnmapGPUTransferBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_UnmapGPUTransferBuffer");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("UnmapGPUTransferBufferNativeFunction", nameof(CaptureDeviceTransferBufferVoid));

        SDL3.SDL.UnmapGPUTransferBuffer((IntPtr)5101, (IntPtr)5102);

        TestAssert.Equal((IntPtr)5101, capturedDevice, "SDL.UnmapGPUTransferBuffer must forward device.");
        TestAssert.Equal((IntPtr)5102, capturedTransferBuffer, "SDL.UnmapGPUTransferBuffer must forward transferBuffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.UnmapGPUTransferBuffer must call native hook once.");
    }

    public static void BeginGPUCopyPass_ForwardsCommandBufferAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BeginGPUCopyPass");
        AssertSdlLibraryImport(nativeMethod, "SDL_BeginGPUCopyPass");

        ResetCaptureState();
        nextPointer = (IntPtr)5201;
        using NativeHookScope _ = NativeHookScope.Install("BeginGPUCopyPassNativeFunction", nameof(CaptureCommandPointer));

        IntPtr result = SDL3.SDL.BeginGPUCopyPass((IntPtr)5202);

        TestAssert.Equal((IntPtr)5201, result, "SDL.BeginGPUCopyPass must return native copy pass.");
        TestAssert.Equal((IntPtr)5202, capturedCommandBuffer, "SDL.BeginGPUCopyPass must forward commandBuffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BeginGPUCopyPass must call native hook once.");
    }

    public static void GetPixelFormatFromGPUTextureFormat_ForwardsTextureFormatAndReturnsPixelFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPixelFormatFromGPUTextureFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetPixelFormatFromGPUTextureFormat");

        ResetCaptureState();
        nextPixelFormat = SDL3.SDL.PixelFormat.RGBA8888;
        using NativeHookScope _ = NativeHookScope.Install("GetPixelFormatFromGPUTextureFormatNativeFunction", nameof(CaptureGetPixelFormatFromGPUTextureFormat));

        SDL3.SDL.PixelFormat result = SDL3.SDL.GetPixelFormatFromGPUTextureFormat(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm);

        TestAssert.Equal(SDL3.SDL.PixelFormat.RGBA8888, result, "SDL.GetPixelFormatFromGPUTextureFormat(GPUTextureFormat) must return native pixel format.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, capturedGPUTextureFormat, "SDL.GetPixelFormatFromGPUTextureFormat(GPUTextureFormat) must forward format.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPixelFormatFromGPUTextureFormat(GPUTextureFormat) must call native hook once.");
    }

    public static void GetGPUTextureFormatFromPixelFormat_ForwardsPixelFormatAndReturnsTextureFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGPUTextureFormatFromPixelFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGPUTextureFormatFromPixelFormat");

        ResetCaptureState();
        nextGPUTextureFormatValue = SDL3.SDL.GPUTextureFormat.B8G8R8A8Unorm;
        using NativeHookScope _ = NativeHookScope.Install("GetGPUTextureFormatFromPixelFormatNativeFunction", nameof(CaptureGetGPUTextureFormatFromPixelFormat));

        SDL3.SDL.GPUTextureFormat result = SDL3.SDL.GetPixelFormatFromGPUTextureFormat(SDL3.SDL.PixelFormat.BGRA8888);

        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.B8G8R8A8Unorm, result, "SDL.GetPixelFormatFromGPUTextureFormat(PixelFormat) must return native GPU texture format.");
        TestAssert.Equal(SDL3.SDL.PixelFormat.BGRA8888, capturedPixelFormat, "SDL.GetPixelFormatFromGPUTextureFormat(PixelFormat) must forward format.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPixelFormatFromGPUTextureFormat(PixelFormat) must call native hook once.");
    }

    public static void UploadToGPUTexture_ForwardsTransferInfoRegionAndCycle()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UploadToGPUTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_UploadToGPUTexture");
        AssertByRefParameter(nativeMethod, "source");
        AssertByRefParameter(nativeMethod, "destination");
        AssertBoolParameterMarshal(nativeMethod, "cycle", UnmanagedType.I1);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("UploadToGPUTextureNativeFunction", nameof(CaptureUploadToGPUTexture));
        SDL3.SDL.GPUTextureTransferInfo source = CreateTextureTransferInfo((IntPtr)5301);
        SDL3.SDL.GPUTextureRegion destination = CreateTextureRegion((IntPtr)5302);

        SDL3.SDL.UploadToGPUTexture((IntPtr)5303, in source, in destination, true);

        TestAssert.Equal((IntPtr)5303, capturedCopyPass, "SDL.UploadToGPUTexture must forward copyPass.");
        TestAssert.Equal(source.TransferBuffer, capturedTextureTransferInfo.TransferBuffer, "SDL.UploadToGPUTexture must forward source transfer buffer.");
        TestAssert.Equal(destination.Texture, capturedTextureRegion.Texture, "SDL.UploadToGPUTexture must forward destination texture.");
        TestAssert.True(capturedCycle, "SDL.UploadToGPUTexture must forward cycle.");
        TestAssert.Equal(1, capturedCallCount, "SDL.UploadToGPUTexture must call native hook once.");
    }

    public static void UploadToGPUBuffer_ForwardsTransferLocationRegionAndCycle()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UploadToGPUBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_UploadToGPUBuffer");
        AssertByRefParameter(nativeMethod, "source");
        AssertByRefParameter(nativeMethod, "destination");
        AssertBoolParameterMarshal(nativeMethod, "cycle", UnmanagedType.I1);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("UploadToGPUBufferNativeFunction", nameof(CaptureUploadToGPUBuffer));
        SDL3.SDL.GPUTransferBufferLocation source = CreateTransferBufferLocation((IntPtr)5401);
        SDL3.SDL.GPUBufferRegion destination = CreateBufferRegion((IntPtr)5402);

        SDL3.SDL.UploadToGPUBuffer((IntPtr)5403, in source, in destination, true);

        TestAssert.Equal((IntPtr)5403, capturedCopyPass, "SDL.UploadToGPUBuffer must forward copyPass.");
        TestAssert.Equal(source.TransferBuffer, capturedTransferBufferLocation.TransferBuffer, "SDL.UploadToGPUBuffer must forward source transfer buffer.");
        TestAssert.Equal(destination.Buffer, capturedBufferRegion.Buffer, "SDL.UploadToGPUBuffer must forward destination buffer.");
        TestAssert.True(capturedCycle, "SDL.UploadToGPUBuffer must forward cycle.");
        TestAssert.Equal(1, capturedCallCount, "SDL.UploadToGPUBuffer must call native hook once.");
    }

    public static void CopyGPUTextureToTexture_ForwardsLocationsSizeAndCycle()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CopyGPUTextureToTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_CopyGPUTextureToTexture");
        AssertBoolParameterMarshal(nativeMethod, "cycle", UnmanagedType.I1);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("CopyGPUTextureToTextureNativeFunction", nameof(CaptureCopyGPUTextureToTexture));
        SDL3.SDL.GPUTextureLocation source = CreateTextureLocation((IntPtr)5501);
        SDL3.SDL.GPUTextureLocation destination = CreateTextureLocation((IntPtr)5502);

        SDL3.SDL.CopyGPUTextureToTexture((IntPtr)5503, in source, in destination, 64, 32, 4, true);

        TestAssert.Equal((IntPtr)5503, capturedCopyPass, "SDL.CopyGPUTextureToTexture must forward copyPass.");
        TestAssert.Equal(source.Texture, capturedSourceTextureLocation.Texture, "SDL.CopyGPUTextureToTexture must forward source texture.");
        TestAssert.Equal(destination.Texture, capturedDestinationTextureLocation.Texture, "SDL.CopyGPUTextureToTexture must forward destination texture.");
        TestAssert.Equal<uint>(64, capturedWidth, "SDL.CopyGPUTextureToTexture must forward width.");
        TestAssert.Equal<uint>(32, capturedHeight, "SDL.CopyGPUTextureToTexture must forward height.");
        TestAssert.Equal<uint>(4, capturedDepth, "SDL.CopyGPUTextureToTexture must forward depth.");
        TestAssert.True(capturedCycle, "SDL.CopyGPUTextureToTexture must forward cycle.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CopyGPUTextureToTexture must call native hook once.");
    }

    public static void CopyGPUBufferToBuffer_ForwardsLocationsSizeAndCycle()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CopyGPUBufferToBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_CopyGPUBufferToBuffer");
        AssertBoolParameterMarshal(nativeMethod, "cycle", UnmanagedType.I1);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("CopyGPUBufferToBufferNativeFunction", nameof(CaptureCopyGPUBufferToBuffer));
        SDL3.SDL.GPUBufferLocation source = CreateBufferLocation((IntPtr)5601);
        SDL3.SDL.GPUBufferLocation destination = CreateBufferLocation((IntPtr)5602);

        SDL3.SDL.CopyGPUBufferToBuffer((IntPtr)5603, in source, in destination, 128, true);

        TestAssert.Equal((IntPtr)5603, capturedCopyPass, "SDL.CopyGPUBufferToBuffer must forward copyPass.");
        TestAssert.Equal(source.Buffer, capturedSourceBufferLocation.Buffer, "SDL.CopyGPUBufferToBuffer must forward source buffer.");
        TestAssert.Equal(destination.Buffer, capturedDestinationBufferLocation.Buffer, "SDL.CopyGPUBufferToBuffer must forward destination buffer.");
        TestAssert.Equal<uint>(128, capturedSize, "SDL.CopyGPUBufferToBuffer must forward size.");
        TestAssert.True(capturedCycle, "SDL.CopyGPUBufferToBuffer must forward cycle.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CopyGPUBufferToBuffer must call native hook once.");
    }

    public static void DownloadFromGPUTexture_ForwardsRegionAndTransferInfo()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DownloadFromGPUTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_DownloadFromGPUTexture");
        AssertByRefParameter(nativeMethod, "source");
        AssertByRefParameter(nativeMethod, "destination");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DownloadFromGPUTextureNativeFunction", nameof(CaptureDownloadFromGPUTexture));
        SDL3.SDL.GPUTextureRegion source = CreateTextureRegion((IntPtr)5701);
        SDL3.SDL.GPUTextureTransferInfo destination = CreateTextureTransferInfo((IntPtr)5702);

        SDL3.SDL.DownloadFromGPUTexture((IntPtr)5703, in source, in destination);

        TestAssert.Equal((IntPtr)5703, capturedCopyPass, "SDL.DownloadFromGPUTexture must forward copyPass.");
        TestAssert.Equal(source.Texture, capturedTextureRegion.Texture, "SDL.DownloadFromGPUTexture must forward source texture.");
        TestAssert.Equal(destination.TransferBuffer, capturedTextureTransferInfo.TransferBuffer, "SDL.DownloadFromGPUTexture must forward destination transfer buffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DownloadFromGPUTexture must call native hook once.");
    }

    public static void DownloadFromGPUBuffer_ForwardsRegionAndTransferLocation()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DownloadFromGPUBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_DownloadFromGPUBuffer");
        AssertByRefParameter(nativeMethod, "source");
        AssertByRefParameter(nativeMethod, "destination");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DownloadFromGPUBufferNativeFunction", nameof(CaptureDownloadFromGPUBuffer));
        SDL3.SDL.GPUTextureRegion source = CreateTextureRegion((IntPtr)5801);
        SDL3.SDL.GPUTransferBufferLocation destination = CreateTransferBufferLocation((IntPtr)5802);

        SDL3.SDL.DownloadFromGPUBuffer((IntPtr)5803, in source, in destination);

        TestAssert.Equal((IntPtr)5803, capturedCopyPass, "SDL.DownloadFromGPUBuffer must forward copyPass.");
        TestAssert.Equal(source.Texture, capturedTextureRegion.Texture, "SDL.DownloadFromGPUBuffer must forward source region.");
        TestAssert.Equal(destination.TransferBuffer, capturedTransferBufferLocation.TransferBuffer, "SDL.DownloadFromGPUBuffer must forward destination transfer buffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DownloadFromGPUBuffer must call native hook once.");
    }

    public static void EndGPUCopyPass_ForwardsCopyPass()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EndGPUCopyPass");
        AssertSdlLibraryImport(nativeMethod, "SDL_EndGPUCopyPass");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("EndGPUCopyPassNativeFunction", nameof(CaptureCopyPassVoid));

        SDL3.SDL.EndGPUCopyPass((IntPtr)5901);

        TestAssert.Equal((IntPtr)5901, capturedCopyPass, "SDL.EndGPUCopyPass must forward copyPass.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EndGPUCopyPass must call native hook once.");
    }

    public static void GenerateMipmapsForGPUTexture_ForwardsCommandBufferAndTexture()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GenerateMipmapsForGPUTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_GenerateMipmapsForGPUTexture");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GenerateMipmapsForGPUTextureNativeFunction", nameof(CaptureCommandResourceVoid));

        SDL3.SDL.GenerateMipmapsForGPUTexture((IntPtr)6001, (IntPtr)6002);

        TestAssert.Equal((IntPtr)6001, capturedCommandBuffer, "SDL.GenerateMipmapsForGPUTexture must forward commandBuffer.");
        TestAssert.Equal((IntPtr)6002, capturedResource, "SDL.GenerateMipmapsForGPUTexture must forward texture.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GenerateMipmapsForGPUTexture must call native hook once.");
    }

    public static void BlitGPUTexture_ForwardsCommandBufferAndInfo()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BlitGPUTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_BlitGPUTexture");
        AssertByRefParameter(nativeMethod, "info");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("BlitGPUTextureNativeFunction", nameof(CaptureBlitGPUTexture));
        SDL3.SDL.GPUBlitInfo info = CreateBlitInfo((IntPtr)6101, (IntPtr)6102);

        SDL3.SDL.BlitGPUTexture((IntPtr)6103, in info);

        TestAssert.Equal((IntPtr)6103, capturedCommandBuffer, "SDL.BlitGPUTexture must forward commandBuffer.");
        TestAssert.Equal(info.Source.Texture, capturedBlitInfo.Source.Texture, "SDL.BlitGPUTexture must forward source texture.");
        TestAssert.Equal(info.Destination.Texture, capturedBlitInfo.Destination.Texture, "SDL.BlitGPUTexture must forward destination texture.");
        TestAssert.True(capturedBlitInfo.Cycle, "SDL.BlitGPUTexture must forward cycle.");
        TestAssert.Equal(1, capturedCallCount, "SDL.BlitGPUTexture must call native hook once.");
    }

    public static void WindowSupportsGPUSwapchainComposition_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WindowSupportsGPUSwapchainComposition");
        AssertSdlLibraryImport(nativeMethod, "SDL_WindowSupportsGPUSwapchainComposition");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("WindowSupportsGPUSwapchainCompositionNativeFunction", nameof(CaptureWindowSupportsGPUSwapchainComposition));

        bool result = SDL3.SDL.WindowSupportsGPUSwapchainComposition((IntPtr)6201, (IntPtr)6202, SDL3.SDL.GPUSwapchainComposition.HDRExtendedLinear);

        TestAssert.Equal(true, result, "SDL.WindowSupportsGPUSwapchainComposition must return native bool value.");
        TestAssert.Equal((IntPtr)6201, capturedDevice, "SDL.WindowSupportsGPUSwapchainComposition must forward device.");
        TestAssert.Equal((IntPtr)6202, capturedWindow, "SDL.WindowSupportsGPUSwapchainComposition must forward window.");
        TestAssert.Equal(SDL3.SDL.GPUSwapchainComposition.HDRExtendedLinear, capturedSwapchainComposition, "SDL.WindowSupportsGPUSwapchainComposition must forward swapchain composition.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WindowSupportsGPUSwapchainComposition must call native hook once.");
    }

    public static void WindowSupportsGPUPresentMode_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WindowSupportsGPUPresentMode");
        AssertSdlLibraryImport(nativeMethod, "SDL_WindowSupportsGPUPresentMode");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("WindowSupportsGPUPresentModeNativeFunction", nameof(CaptureWindowSupportsGPUPresentMode));

        bool result = SDL3.SDL.WindowSupportsGPUPresentMode((IntPtr)6301, (IntPtr)6302, SDL3.SDL.GPUPresentMode.Mailbox);

        TestAssert.Equal(true, result, "SDL.WindowSupportsGPUPresentMode must return native bool value.");
        TestAssert.Equal((IntPtr)6301, capturedDevice, "SDL.WindowSupportsGPUPresentMode must forward device.");
        TestAssert.Equal((IntPtr)6302, capturedWindow, "SDL.WindowSupportsGPUPresentMode must forward window.");
        TestAssert.Equal(SDL3.SDL.GPUPresentMode.Mailbox, capturedPresentMode, "SDL.WindowSupportsGPUPresentMode must forward present mode.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WindowSupportsGPUPresentMode must call native hook once.");
    }

    public static void ClaimWindowForGPUDevice_ForwardsDeviceAndWindow()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ClaimWindowForGPUDevice");
        AssertSdlLibraryImport(nativeMethod, "SDL_ClaimWindowForGPUDevice");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("ClaimWindowForGPUDeviceNativeFunction", nameof(CaptureDeviceWindowBool));

        bool result = SDL3.SDL.ClaimWindowForGPUDevice((IntPtr)6401, (IntPtr)6402);

        TestAssert.Equal(true, result, "SDL.ClaimWindowForGPUDevice must return native bool value.");
        TestAssert.Equal((IntPtr)6401, capturedDevice, "SDL.ClaimWindowForGPUDevice must forward device.");
        TestAssert.Equal((IntPtr)6402, capturedWindow, "SDL.ClaimWindowForGPUDevice must forward window.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ClaimWindowForGPUDevice must call native hook once.");
    }

    public static void ReleaseWindowFromGPUDevice_ForwardsDeviceAndWindow()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ReleaseWindowFromGPUDevice");
        AssertSdlLibraryImport(nativeMethod, "SDL_ReleaseWindowFromGPUDevice");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("ReleaseWindowFromGPUDeviceNativeFunction", nameof(CaptureDeviceWindowVoid));

        SDL3.SDL.ReleaseWindowFromGPUDevice((IntPtr)6501, (IntPtr)6502);

        TestAssert.Equal((IntPtr)6501, capturedDevice, "SDL.ReleaseWindowFromGPUDevice must forward device.");
        TestAssert.Equal((IntPtr)6502, capturedWindow, "SDL.ReleaseWindowFromGPUDevice must forward window.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ReleaseWindowFromGPUDevice must call native hook once.");
    }

    public static void SetGPUSwapchainParameters_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGPUSwapchainParameters");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetGPUSwapchainParameters");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetGPUSwapchainParametersNativeFunction", nameof(CaptureSetGPUSwapchainParameters));

        bool result = SDL3.SDL.SetGPUSwapchainParameters((IntPtr)6601, (IntPtr)6602, SDL3.SDL.GPUSwapchainComposition.HDR10ST2084, SDL3.SDL.GPUPresentMode.Immediate);

        TestAssert.Equal(true, result, "SDL.SetGPUSwapchainParameters must return native bool value.");
        TestAssert.Equal((IntPtr)6601, capturedDevice, "SDL.SetGPUSwapchainParameters must forward device.");
        TestAssert.Equal((IntPtr)6602, capturedWindow, "SDL.SetGPUSwapchainParameters must forward window.");
        TestAssert.Equal(SDL3.SDL.GPUSwapchainComposition.HDR10ST2084, capturedSwapchainComposition, "SDL.SetGPUSwapchainParameters must forward swapchain composition.");
        TestAssert.Equal(SDL3.SDL.GPUPresentMode.Immediate, capturedPresentMode, "SDL.SetGPUSwapchainParameters must forward present mode.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGPUSwapchainParameters must call native hook once.");
    }

    public static void SetGPUAllowedFramesInFlight_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetGPUAllowedFramesInFlight");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetGPUAllowedFramesInFlight");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetGPUAllowedFramesInFlightNativeFunction", nameof(CaptureSetGPUAllowedFramesInFlight));

        bool result = SDL3.SDL.SetGPUAllowedFramesInFlight((IntPtr)6701, 3);

        TestAssert.Equal(true, result, "SDL.SetGPUAllowedFramesInFlight must return native bool value.");
        TestAssert.Equal((IntPtr)6701, capturedDevice, "SDL.SetGPUAllowedFramesInFlight must forward device.");
        TestAssert.Equal<uint>(3, capturedAllowedFramesInFlight, "SDL.SetGPUAllowedFramesInFlight must forward allowedFramesInFlight.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetGPUAllowedFramesInFlight must call native hook once.");
    }

    public static void GetGPUSwapchainTextureFormat_ForwardsDeviceWindowAndReturnsTextureFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGPUSwapchainTextureFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGPUSwapchainTextureFormat");

        ResetCaptureState();
        nextGPUTextureFormatValue = SDL3.SDL.GPUTextureFormat.B8G8R8A8Unorm;
        using NativeHookScope _ = NativeHookScope.Install("GetGPUSwapchainTextureFormatNativeFunction", nameof(CaptureDeviceWindowTextureFormat));

        SDL3.SDL.GPUTextureFormat result = SDL3.SDL.GetGPUSwapchainTextureFormat((IntPtr)6801, (IntPtr)6802);

        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.B8G8R8A8Unorm, result, "SDL.GetGPUSwapchainTextureFormat must return native texture format.");
        TestAssert.Equal((IntPtr)6801, capturedDevice, "SDL.GetGPUSwapchainTextureFormat must forward device.");
        TestAssert.Equal((IntPtr)6802, capturedWindow, "SDL.GetGPUSwapchainTextureFormat must forward window.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGPUSwapchainTextureFormat must call native hook once.");
    }

    public static void AcquireGPUSwapchainTexture_ForwardsArgumentsOutValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AcquireGPUSwapchainTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_AcquireGPUSwapchainTexture");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "swapchainTexture");
        AssertByRefParameter(nativeMethod, "swapchainTextureWidth");
        AssertByRefParameter(nativeMethod, "swapchainTextureHeight");

        ResetCaptureState();
        nextBool = true;
        nextSwapchainTexture = (IntPtr)6901;
        nextSwapchainTextureWidth = 1920;
        nextSwapchainTextureHeight = 1080;
        using NativeHookScope _ = NativeHookScope.Install("AcquireGPUSwapchainTextureNativeFunction", nameof(CaptureAcquireGPUSwapchainTexture));

        bool result = SDL3.SDL.AcquireGPUSwapchainTexture((IntPtr)6902, (IntPtr)6903, out IntPtr texture, out uint width, out uint height);

        TestAssert.Equal(true, result, "SDL.AcquireGPUSwapchainTexture must return native bool value.");
        TestAssert.Equal((IntPtr)6902, capturedCommandBuffer, "SDL.AcquireGPUSwapchainTexture must forward commandBuffer.");
        TestAssert.Equal((IntPtr)6903, capturedWindow, "SDL.AcquireGPUSwapchainTexture must forward window.");
        TestAssert.Equal((IntPtr)6901, texture, "SDL.AcquireGPUSwapchainTexture must forward native texture out value.");
        TestAssert.Equal<uint>(1920, width, "SDL.AcquireGPUSwapchainTexture must forward native width out value.");
        TestAssert.Equal<uint>(1080, height, "SDL.AcquireGPUSwapchainTexture must forward native height out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AcquireGPUSwapchainTexture must call native hook once.");
    }

    public static void WaitForGPUSwapchain_ForwardsDeviceWindowAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitForGPUSwapchain");
        AssertSdlLibraryImport(nativeMethod, "SDL_WaitForGPUSwapchain");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("WaitForGPUSwapchainNativeFunction", nameof(CaptureDeviceWindowBool));

        bool result = SDL3.SDL.WaitForGPUSwapchain((IntPtr)7001, (IntPtr)7002);

        TestAssert.Equal(true, result, "SDL.WaitForGPUSwapchain must return native bool value.");
        TestAssert.Equal((IntPtr)7001, capturedDevice, "SDL.WaitForGPUSwapchain must forward device.");
        TestAssert.Equal((IntPtr)7002, capturedWindow, "SDL.WaitForGPUSwapchain must forward window.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitForGPUSwapchain must call native hook once.");
    }

    public static void WaitAndAcquireGPUSwapchainTexture_ForwardsArgumentsOutValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitAndAcquireGPUSwapchainTexture");
        AssertSdlLibraryImport(nativeMethod, "SDL_WaitAndAcquireGPUSwapchainTexture");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "swapchainTexture");
        AssertByRefParameter(nativeMethod, "swapchainTextureWidth");
        AssertByRefParameter(nativeMethod, "swapchainTextureHeight");

        ResetCaptureState();
        nextBool = true;
        nextSwapchainTexture = (IntPtr)7101;
        nextSwapchainTextureWidth = 1280;
        nextSwapchainTextureHeight = 720;
        using NativeHookScope _ = NativeHookScope.Install("WaitAndAcquireGPUSwapchainTextureNativeFunction", nameof(CaptureAcquireGPUSwapchainTexture));

        bool result = SDL3.SDL.WaitAndAcquireGPUSwapchainTexture((IntPtr)7102, (IntPtr)7103, out IntPtr texture, out uint width, out uint height);

        TestAssert.Equal(true, result, "SDL.WaitAndAcquireGPUSwapchainTexture must return native bool value.");
        TestAssert.Equal((IntPtr)7102, capturedCommandBuffer, "SDL.WaitAndAcquireGPUSwapchainTexture must forward commandBuffer.");
        TestAssert.Equal((IntPtr)7103, capturedWindow, "SDL.WaitAndAcquireGPUSwapchainTexture must forward window.");
        TestAssert.Equal((IntPtr)7101, texture, "SDL.WaitAndAcquireGPUSwapchainTexture must forward native texture out value.");
        TestAssert.Equal<uint>(1280, width, "SDL.WaitAndAcquireGPUSwapchainTexture must forward native width out value.");
        TestAssert.Equal<uint>(720, height, "SDL.WaitAndAcquireGPUSwapchainTexture must forward native height out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitAndAcquireGPUSwapchainTexture must call native hook once.");
    }

    public static void SubmitGPUCommandBuffer_ForwardsCommandBufferAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SubmitGPUCommandBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_SubmitGPUCommandBuffer");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SubmitGPUCommandBufferNativeFunction", nameof(CaptureCommandBool));

        bool result = SDL3.SDL.SubmitGPUCommandBuffer((IntPtr)7201);

        TestAssert.Equal(true, result, "SDL.SubmitGPUCommandBuffer must return native bool value.");
        TestAssert.Equal((IntPtr)7201, capturedCommandBuffer, "SDL.SubmitGPUCommandBuffer must forward commandBuffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SubmitGPUCommandBuffer must call native hook once.");
    }

    public static void SubmitGPUCommandBufferAndAcquireFence_ForwardsCommandBufferAndReturnsFence()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SubmitGPUCommandBufferAndAcquireFence");
        AssertSdlLibraryImport(nativeMethod, "SDL_SubmitGPUCommandBufferAndAcquireFence");

        ResetCaptureState();
        nextPointer = (IntPtr)7301;
        using NativeHookScope _ = NativeHookScope.Install("SubmitGPUCommandBufferAndAcquireFenceNativeFunction", nameof(CaptureCommandPointer));

        IntPtr result = SDL3.SDL.SubmitGPUCommandBufferAndAcquireFence((IntPtr)7302);

        TestAssert.Equal((IntPtr)7301, result, "SDL.SubmitGPUCommandBufferAndAcquireFence must return native fence.");
        TestAssert.Equal((IntPtr)7302, capturedCommandBuffer, "SDL.SubmitGPUCommandBufferAndAcquireFence must forward commandBuffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SubmitGPUCommandBufferAndAcquireFence must call native hook once.");
    }

    public static void CancelGPUCommandBuffer_ForwardsCommandBufferAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CancelGPUCommandBuffer");
        AssertSdlLibraryImport(nativeMethod, "SDL_CancelGPUCommandBuffer");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("CancelGPUCommandBufferNativeFunction", nameof(CaptureCommandBool));

        bool result = SDL3.SDL.CancelGPUCommandBuffer((IntPtr)7401);

        TestAssert.Equal(true, result, "SDL.CancelGPUCommandBuffer must return native bool value.");
        TestAssert.Equal((IntPtr)7401, capturedCommandBuffer, "SDL.CancelGPUCommandBuffer must forward commandBuffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CancelGPUCommandBuffer must call native hook once.");
    }

    public static void WaitForGPUIdle_ForwardsDeviceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitForGPUIdle");
        AssertSdlLibraryImport(nativeMethod, "SDL_WaitForGPUIdle");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("WaitForGPUIdleNativeFunction", nameof(CaptureDeviceBool));

        bool result = SDL3.SDL.WaitForGPUIdle((IntPtr)7501);

        TestAssert.Equal(true, result, "SDL.WaitForGPUIdle must return native bool value.");
        TestAssert.Equal((IntPtr)7501, capturedDevice, "SDL.WaitForGPUIdle must forward device.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitForGPUIdle must call native hook once.");
    }

    public static void WaitForGPUFencesArray_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitForGPUFences", typeof(IntPtr), typeof(bool), typeof(IntPtr[]), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, "SDL_WaitForGPUFences");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertBoolParameterMarshal(nativeMethod, "waitAll", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("WaitForGPUFencesArrayNativeFunction", nameof(CaptureWaitForGPUFencesArray));
        IntPtr[] fences = [(IntPtr)7601, (IntPtr)7602];

        bool result = SDL3.SDL.WaitForGPUFences((IntPtr)7603, true, fences, (uint)fences.Length);

        TestAssert.Equal(true, result, "SDL.WaitForGPUFences array overload must return native bool value.");
        TestAssert.Equal((IntPtr)7603, capturedDevice, "SDL.WaitForGPUFences array overload must forward device.");
        TestAssert.True(capturedWaitAll, "SDL.WaitForGPUFences array overload must forward waitAll.");
        TestAssert.True(ReferenceEquals(fences, capturedFencesArray), "SDL.WaitForGPUFences array overload must forward fences array.");
        TestAssert.Equal<uint>((uint)fences.Length, capturedNumFences, "SDL.WaitForGPUFences array overload must forward numFences.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitForGPUFences array overload must call native hook once.");
    }

    public static void WaitForGPUFencesPointer_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitForGPUFences", typeof(IntPtr), typeof(bool), typeof(IntPtr), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, "SDL_WaitForGPUFences");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertBoolParameterMarshal(nativeMethod, "waitAll", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("WaitForGPUFencesPointerNativeFunction", nameof(CaptureWaitForGPUFencesPointer));

        bool result = SDL3.SDL.WaitForGPUFences((IntPtr)7701, false, (IntPtr)7702, 4);

        TestAssert.Equal(true, result, "SDL.WaitForGPUFences pointer overload must return native bool value.");
        TestAssert.Equal((IntPtr)7701, capturedDevice, "SDL.WaitForGPUFences pointer overload must forward device.");
        TestAssert.Equal(false, capturedWaitAll, "SDL.WaitForGPUFences pointer overload must forward waitAll.");
        TestAssert.Equal((IntPtr)7702, capturedFences, "SDL.WaitForGPUFences pointer overload must forward fences pointer.");
        TestAssert.Equal<uint>(4, capturedNumFences, "SDL.WaitForGPUFences pointer overload must forward numFences.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitForGPUFences pointer overload must call native hook once.");
    }

    public static void QueryGPUFence_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_QueryGPUFence");
        AssertSdlLibraryImport(nativeMethod, "SDL_QueryGPUFence");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("QueryGPUFenceNativeFunction", nameof(CaptureGPUFenceBool));

        bool result = SDL3.SDL.QueryGPUFence((IntPtr)7801, (IntPtr)7802);

        TestAssert.Equal(true, result, "SDL.QueryGPUFence must return native bool value.");
        TestAssert.Equal((IntPtr)7801, capturedDevice, "SDL.QueryGPUFence must forward device.");
        TestAssert.Equal((IntPtr)7802, capturedFence, "SDL.QueryGPUFence must forward fence.");
        TestAssert.Equal(1, capturedCallCount, "SDL.QueryGPUFence must call native hook once.");
    }

    public static void ReleaseGPUFence_ForwardsArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ReleaseGPUFence");
        AssertSdlLibraryImport(nativeMethod, "SDL_ReleaseGPUFence");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("ReleaseGPUFenceNativeFunction", nameof(CaptureGPUFenceVoid));

        SDL3.SDL.ReleaseGPUFence((IntPtr)7901, (IntPtr)7902);

        TestAssert.Equal((IntPtr)7901, capturedDevice, "SDL.ReleaseGPUFence must forward device.");
        TestAssert.Equal((IntPtr)7902, capturedFence, "SDL.ReleaseGPUFence must forward fence.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ReleaseGPUFence must call native hook once.");
    }

    public static void GPUTextureFormatTexelBlockSize_ForwardsFormatAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GPUTextureFormatTexelBlockSize");
        AssertSdlLibraryImport(nativeMethod, "SDL_GPUTextureFormatTexelBlockSize");

        ResetCaptureState();
        nextUInt = 16;
        using NativeHookScope _ = NativeHookScope.Install("GPUTextureFormatTexelBlockSizeNativeFunction", nameof(CaptureGPUTextureFormatTexelBlockSize));

        uint result = SDL3.SDL.GPUTextureFormatTexelBlockSize(SDL3.SDL.GPUTextureFormat.BC7RGBAUnorm);

        TestAssert.Equal<uint>(16, result, "SDL.GPUTextureFormatTexelBlockSize must return native texel block size.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.BC7RGBAUnorm, capturedGPUTextureFormat, "SDL.GPUTextureFormatTexelBlockSize must forward format.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GPUTextureFormatTexelBlockSize must call native hook once.");
    }

    public static void GPUTextureSupportsFormat_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GPUTextureSupportsFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GPUTextureSupportsFormat");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.GPUTextureUsageFlags usage = SDL3.SDL.GPUTextureUsageFlags.Sampler | SDL3.SDL.GPUTextureUsageFlags.ColorTarget;
        using NativeHookScope _ = NativeHookScope.Install("GPUTextureSupportsFormatNativeFunction", nameof(CaptureGPUTextureSupportsFormat));

        bool result = SDL3.SDL.GPUTextureSupportsFormat((IntPtr)8001, SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, SDL3.SDL.GPUTextureType.TextureType2D, usage);

        TestAssert.Equal(true, result, "SDL.GPUTextureSupportsFormat must return native bool value.");
        TestAssert.Equal((IntPtr)8001, capturedDevice, "SDL.GPUTextureSupportsFormat must forward device.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, capturedGPUTextureFormat, "SDL.GPUTextureSupportsFormat must forward format.");
        TestAssert.Equal(SDL3.SDL.GPUTextureType.TextureType2D, capturedTextureType, "SDL.GPUTextureSupportsFormat must forward texture type.");
        TestAssert.Equal(usage, capturedTextureUsage, "SDL.GPUTextureSupportsFormat must forward usage.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GPUTextureSupportsFormat must call native hook once.");
    }

    public static void GPUTextureSupportsSampleCount_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GPUTextureSupportsSampleCount");
        AssertSdlLibraryImport(nativeMethod, "SDL_GPUTextureSupportsSampleCount");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("GPUTextureSupportsSampleCountNativeFunction", nameof(CaptureGPUTextureSupportsSampleCount));

        bool result = SDL3.SDL.GPUTextureSupportsSampleCount((IntPtr)8101, SDL3.SDL.GPUTextureFormat.R16G16B16A16Float, SDL3.SDL.GPUSampleCount.SampleCount4);

        TestAssert.Equal(true, result, "SDL.GPUTextureSupportsSampleCount must return native bool value.");
        TestAssert.Equal((IntPtr)8101, capturedDevice, "SDL.GPUTextureSupportsSampleCount must forward device.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R16G16B16A16Float, capturedGPUTextureFormat, "SDL.GPUTextureSupportsSampleCount must forward format.");
        TestAssert.Equal(SDL3.SDL.GPUSampleCount.SampleCount4, capturedSampleCount, "SDL.GPUTextureSupportsSampleCount must forward sample count.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GPUTextureSupportsSampleCount must call native hook once.");
    }

    public static void CalculateGPUTextureFormatSize_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CalculateGPUTextureFormatSize");
        AssertSdlLibraryImport(nativeMethod, "SDL_CalculateGPUTextureFormatSize");

        ResetCaptureState();
        nextUInt = 8192;
        using NativeHookScope _ = NativeHookScope.Install("CalculateGPUTextureFormatSizeNativeFunction", nameof(CaptureCalculateGPUTextureFormatSize));

        uint result = SDL3.SDL.CalculateGPUTextureFormatSize(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, 64, 32, 1);

        TestAssert.Equal<uint>(8192, result, "SDL.CalculateGPUTextureFormatSize must return native byte size.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, capturedGPUTextureFormat, "SDL.CalculateGPUTextureFormatSize must forward format.");
        TestAssert.Equal<uint>(64, capturedWidth, "SDL.CalculateGPUTextureFormatSize must forward width.");
        TestAssert.Equal<uint>(32, capturedHeight, "SDL.CalculateGPUTextureFormatSize must forward height.");
        TestAssert.Equal<uint>(1, capturedDepthOrLayerCount, "SDL.CalculateGPUTextureFormatSize must forward depthOrLayerCount.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CalculateGPUTextureFormatSize must call native hook once.");
    }

    public static void GDKSuspendGPU_ForwardsDevice()
    {
        AssertDeviceVoidMethod("GDKSuspendGPU", "SDL_GDKSuspendGPU", "GDKSuspendGPUNativeFunction", (IntPtr)8201);
    }

    public static void GDKResumeGPU_ForwardsDevice()
    {
        AssertDeviceVoidMethod("GDKResumeGPU", "SDL_GDKResumeGPU", "GDKResumeGPUNativeFunction", (IntPtr)8301);
    }

    private static void ResetCaptureState()
    {
        capturedFormatFlags = default;
        capturedName = null;
        capturedProps = 0;
        capturedDebugMode = false;
        capturedIndex = 0;
        capturedDevice = IntPtr.Zero;
        capturedResource = IntPtr.Zero;
        capturedCommandBuffer = IntPtr.Zero;
        capturedRenderPass = IntPtr.Zero;
        capturedGraphicsPipeline = IntPtr.Zero;
        capturedSlotIndex = 0;
        capturedFirstSlot = 0;
        capturedData = IntPtr.Zero;
        capturedDataArray = null;
        capturedLength = 0;
        capturedBindings = IntPtr.Zero;
        capturedNumBindings = 0;
        capturedBufferBinding = default;
        capturedIndexElementSize = default;
        capturedTextureSamplerBindings = null;
        capturedPointerArray = null;
        capturedBuffer = IntPtr.Zero;
        capturedOffset = 0;
        capturedDrawCount = 0;
        capturedNumIndices = 0;
        capturedNumInstances = 0;
        capturedFirstIndex = 0;
        capturedVertexOffset = 0;
        capturedFirstInstance = 0;
        capturedNumVertices = 0;
        capturedFirstVertex = 0;
        capturedComputePass = IntPtr.Zero;
        capturedComputePipeline = IntPtr.Zero;
        capturedStorageTextureBindings = null;
        capturedNumStorageTextureBindings = 0;
        capturedStorageBufferBindings = null;
        capturedNumStorageBufferBindings = 0;
        capturedGroupCountX = 0;
        capturedGroupCountY = 0;
        capturedGroupCountZ = 0;
        capturedCopyPass = IntPtr.Zero;
        capturedTransferBuffer = IntPtr.Zero;
        capturedCycle = false;
        capturedTextureTransferInfo = default;
        capturedTextureRegion = default;
        capturedTransferBufferLocation = default;
        capturedBufferRegion = default;
        capturedSourceTextureLocation = default;
        capturedDestinationTextureLocation = default;
        capturedSourceBufferLocation = default;
        capturedDestinationBufferLocation = default;
        capturedWidth = 0;
        capturedHeight = 0;
        capturedDepth = 0;
        capturedSize = 0;
        capturedGPUTextureFormat = default;
        capturedPixelFormat = default;
        nextPixelFormat = default;
        nextGPUTextureFormatValue = default;
        capturedBlitInfo = default;
        capturedWindow = IntPtr.Zero;
        capturedSwapchainComposition = default;
        capturedPresentMode = default;
        capturedAllowedFramesInFlight = 0;
        capturedSwapchainTexture = IntPtr.Zero;
        nextSwapchainTexture = IntPtr.Zero;
        nextSwapchainTextureWidth = 0;
        nextSwapchainTextureHeight = 0;
        capturedWaitAll = false;
        capturedFencesArray = null;
        capturedFences = IntPtr.Zero;
        capturedNumFences = 0;
        capturedFence = IntPtr.Zero;
        capturedTextureType = default;
        capturedTextureUsage = default;
        capturedSampleCount = default;
        capturedDepthOrLayerCount = 0;
        capturedColorTargetInfos = IntPtr.Zero;
        capturedNumColorTargets = 0;
        capturedDepthStencilTargetInfo = IntPtr.Zero;
        capturedDepthStencilInfo = default;
        capturedViewport = default;
        capturedScissor = default;
        capturedBlendConstants = default;
        capturedStencilReference = 0;
        capturedText = null;
        capturedComputePipelineInfo = default;
        capturedGraphicsPipelineInfo = default;
        capturedSamplerInfo = default;
        capturedShaderInfo = default;
        capturedTextureInfo = default;
        capturedBufferInfo = default;
        capturedTransferBufferInfo = default;
        nextPointer = IntPtr.Zero;
        nextBool = false;
        nextInt = 0;
        nextUInt = 0;
        nextShaderFormat = default;
        capturedCallCount = 0;
    }

    private static bool CaptureGPUSupportsShaderFormats(SDL3.SDL.GPUShaderFormat formatFlags, string? name)
    {
        capturedFormatFlags = formatFlags;
        capturedName = name;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CapturePropsBool(uint props)
    {
        capturedProps = props;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureCreateGPUDevice(SDL3.SDL.GPUShaderFormat formatFlags, bool debugMode, string? name)
    {
        capturedFormatFlags = formatFlags;
        capturedDebugMode = debugMode;
        capturedName = name;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CapturePropsPointer(uint props)
    {
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureDeviceVoid(IntPtr device)
    {
        capturedDevice = device;
        capturedCallCount++;
    }

    private static int CaptureNoArgumentInt()
    {
        capturedCallCount++;
        return nextInt;
    }

    private static IntPtr CaptureGetGPUDriver(int index)
    {
        capturedIndex = index;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureDevicePointer(IntPtr device)
    {
        capturedDevice = device;
        capturedCallCount++;
        return nextPointer;
    }

    private static SDL3.SDL.GPUShaderFormat CaptureDeviceShaderFormat(IntPtr device)
    {
        capturedDevice = device;
        capturedCallCount++;
        return nextShaderFormat;
    }

    private static uint CaptureDeviceUInt(IntPtr device)
    {
        capturedDevice = device;
        capturedCallCount++;
        return nextUInt;
    }

    private static IntPtr CaptureCreateGPUComputePipeline(IntPtr device, in SDL3.SDL.GPUComputePipelineCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedComputePipelineInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPUGraphicsPipeline(IntPtr device, in SDL3.SDL.GPUGraphicsPipelineCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedGraphicsPipelineInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPUSampler(IntPtr device, in SDL3.SDL.GPUSamplerCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedSamplerInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPUShader(IntPtr device, in SDL3.SDL.GPUShaderCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedShaderInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPUTexture(IntPtr device, in SDL3.SDL.GPUTextureCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedTextureInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPUBuffer(IntPtr device, in SDL3.SDL.GPUBufferCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedBufferInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCreateGPUTransferBuffer(IntPtr device, in SDL3.SDL.GPUTransferBufferCreateInfo createinfo)
    {
        capturedDevice = device;
        capturedTransferBufferInfo = createinfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureDeviceResourceText(IntPtr device, IntPtr resource, string text)
    {
        capturedDevice = device;
        capturedResource = resource;
        capturedText = text;
        capturedCallCount++;
    }

    private static void CaptureCommandText(IntPtr commandBuffer, string text)
    {
        capturedCommandBuffer = commandBuffer;
        capturedText = text;
        capturedCallCount++;
    }

    private static void CaptureCommandVoid(IntPtr commandBuffer)
    {
        capturedCommandBuffer = commandBuffer;
        capturedCallCount++;
    }

    private static void CaptureDeviceResourceVoid(IntPtr device, IntPtr resource)
    {
        capturedDevice = device;
        capturedResource = resource;
        capturedCallCount++;
    }

    private static void CaptureUniformPointer(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        capturedCommandBuffer = commandBuffer;
        capturedSlotIndex = slotIndex;
        capturedData = data;
        capturedLength = length;
        capturedCallCount++;
    }

    private static void CaptureUniformArray(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length)
    {
        capturedCommandBuffer = commandBuffer;
        capturedSlotIndex = slotIndex;
        capturedDataArray = data;
        capturedLength = length;
        capturedCallCount++;
    }

    private static IntPtr CaptureBeginGPURenderPassPointer(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo)
    {
        capturedCommandBuffer = commandBuffer;
        capturedColorTargetInfos = colorTargetInfos;
        capturedNumColorTargets = numColorTargets;
        capturedDepthStencilTargetInfo = depthStencilTargetInfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureBeginGPURenderPassDepthStencil(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, in SDL3.SDL.GPUDepthStencilTargetInfo depthStencilTargetInfo)
    {
        capturedCommandBuffer = commandBuffer;
        capturedColorTargetInfos = colorTargetInfos;
        capturedNumColorTargets = numColorTargets;
        capturedDepthStencilInfo = depthStencilTargetInfo;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureBindGPUGraphicsPipeline(IntPtr renderPass, IntPtr graphicsPipeline)
    {
        capturedRenderPass = renderPass;
        capturedGraphicsPipeline = graphicsPipeline;
        capturedCallCount++;
    }

    private static void CaptureSetGPUViewport(IntPtr renderPass, in SDL3.SDL.GPUViewport viewport)
    {
        capturedRenderPass = renderPass;
        capturedViewport = viewport;
        capturedCallCount++;
    }

    private static void CaptureSetGPUScissor(IntPtr renderPass, in SDL3.SDL.Rect scissor)
    {
        capturedRenderPass = renderPass;
        capturedScissor = scissor;
        capturedCallCount++;
    }

    private static void CaptureSetGPUBlendConstants(IntPtr renderPass, in SDL3.SDL.FColor blendConstants)
    {
        capturedRenderPass = renderPass;
        capturedBlendConstants = blendConstants;
        capturedCallCount++;
    }

    private static void CaptureSetGPUStencilReference(IntPtr renderPass, byte reference)
    {
        capturedRenderPass = renderPass;
        capturedStencilReference = reference;
        capturedCallCount++;
    }

    private static void CaptureRenderPassSlotPointer(IntPtr renderPass, uint firstSlot, IntPtr bindings, uint numBindings)
    {
        capturedRenderPass = renderPass;
        capturedFirstSlot = firstSlot;
        capturedBindings = bindings;
        capturedNumBindings = numBindings;
        capturedCallCount++;
    }

    private static void CaptureBindGPUIndexBuffer(IntPtr renderPass, in SDL3.SDL.GPUBufferBinding binding, SDL3.SDL.GPUIndexElementSize indexElementSize)
    {
        capturedRenderPass = renderPass;
        capturedBufferBinding = binding;
        capturedIndexElementSize = indexElementSize;
        capturedCallCount++;
    }

    private static void CaptureRenderPassSlotPointerArray(IntPtr renderPass, uint firstSlot, IntPtr[] pointerArray, uint numBindings)
    {
        capturedRenderPass = renderPass;
        capturedFirstSlot = firstSlot;
        capturedPointerArray = pointerArray;
        capturedNumBindings = numBindings;
        capturedCallCount++;
    }

    private static void CaptureRenderPassSlotSamplerArray(IntPtr renderPass, uint firstSlot, SDL3.SDL.GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings)
    {
        capturedRenderPass = renderPass;
        capturedFirstSlot = firstSlot;
        capturedTextureSamplerBindings = textureSamplerBindings;
        capturedNumBindings = numBindings;
        capturedCallCount++;
    }

    private static void CaptureDrawGPUIndexedPrimitives(IntPtr renderPass, uint numIndices, uint numInstances, uint firstIndex, int vertexOffset, uint firstInstance)
    {
        capturedRenderPass = renderPass;
        capturedNumIndices = numIndices;
        capturedNumInstances = numInstances;
        capturedFirstIndex = firstIndex;
        capturedVertexOffset = vertexOffset;
        capturedFirstInstance = firstInstance;
        capturedCallCount++;
    }

    private static void CaptureDrawGPUPrimitives(IntPtr renderPass, uint numVertices, uint numInstances, uint firstVertex, uint firstInstance)
    {
        capturedRenderPass = renderPass;
        capturedNumVertices = numVertices;
        capturedNumInstances = numInstances;
        capturedFirstVertex = firstVertex;
        capturedFirstInstance = firstInstance;
        capturedCallCount++;
    }

    private static void CaptureDrawIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount)
    {
        capturedRenderPass = renderPass;
        capturedBuffer = buffer;
        capturedOffset = offset;
        capturedDrawCount = drawCount;
        capturedCallCount++;
    }

    private static void CaptureRenderPassVoid(IntPtr renderPass)
    {
        capturedRenderPass = renderPass;
        capturedCallCount++;
    }

    private static IntPtr CaptureBeginGPUComputePass(
        IntPtr commandBuffer,
        SDL3.SDL.GPUStorageTextureReadWriteBinding[] storageTextureBindings,
        uint numStorageTextureBindings,
        SDL3.SDL.GPUStorageBufferReadWriteBinding[] storageBufferBindings,
        uint numStorageBufferBindings)
    {
        capturedCommandBuffer = commandBuffer;
        capturedStorageTextureBindings = storageTextureBindings;
        capturedNumStorageTextureBindings = numStorageTextureBindings;
        capturedStorageBufferBindings = storageBufferBindings;
        capturedNumStorageBufferBindings = numStorageBufferBindings;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureComputePassPipeline(IntPtr computePass, IntPtr computePipeline)
    {
        capturedComputePass = computePass;
        capturedComputePipeline = computePipeline;
        capturedCallCount++;
    }

    private static void CaptureComputePassSlotSamplerArray(IntPtr computePass, uint firstSlot, SDL3.SDL.GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings)
    {
        capturedComputePass = computePass;
        capturedFirstSlot = firstSlot;
        capturedTextureSamplerBindings = textureSamplerBindings;
        capturedNumBindings = numBindings;
        capturedCallCount++;
    }

    private static void CaptureComputePassSlotPointer(IntPtr computePass, uint firstSlot, IntPtr bindings, uint numBindings)
    {
        capturedComputePass = computePass;
        capturedFirstSlot = firstSlot;
        capturedBindings = bindings;
        capturedNumBindings = numBindings;
        capturedCallCount++;
    }

    private static void CaptureComputePassSlotPointerArray(IntPtr computePass, uint firstSlot, IntPtr[] pointerArray, uint numBindings)
    {
        capturedComputePass = computePass;
        capturedFirstSlot = firstSlot;
        capturedPointerArray = pointerArray;
        capturedNumBindings = numBindings;
        capturedCallCount++;
    }

    private static void CaptureDispatchGPUCompute(IntPtr computePass, uint groupcountX, uint groupcountY, uint groupcountZ)
    {
        capturedComputePass = computePass;
        capturedGroupCountX = groupcountX;
        capturedGroupCountY = groupcountY;
        capturedGroupCountZ = groupcountZ;
        capturedCallCount++;
    }

    private static void CaptureComputeIndirect(IntPtr computePass, IntPtr buffer, uint offset)
    {
        capturedComputePass = computePass;
        capturedBuffer = buffer;
        capturedOffset = offset;
        capturedCallCount++;
    }

    private static void CaptureComputePassVoid(IntPtr computePass)
    {
        capturedComputePass = computePass;
        capturedCallCount++;
    }

    private static IntPtr CaptureMapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer, bool cycle)
    {
        capturedDevice = device;
        capturedTransferBuffer = transferBuffer;
        capturedCycle = cycle;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureDeviceTransferBufferVoid(IntPtr device, IntPtr transferBuffer)
    {
        capturedDevice = device;
        capturedTransferBuffer = transferBuffer;
        capturedCallCount++;
    }

    private static IntPtr CaptureCommandPointer(IntPtr commandBuffer)
    {
        capturedCommandBuffer = commandBuffer;
        capturedCallCount++;
        return nextPointer;
    }

    private static SDL3.SDL.PixelFormat CaptureGetPixelFormatFromGPUTextureFormat(SDL3.SDL.GPUTextureFormat format)
    {
        capturedGPUTextureFormat = format;
        capturedCallCount++;
        return nextPixelFormat;
    }

    private static SDL3.SDL.GPUTextureFormat CaptureGetGPUTextureFormatFromPixelFormat(SDL3.SDL.PixelFormat format)
    {
        capturedPixelFormat = format;
        capturedCallCount++;
        return nextGPUTextureFormatValue;
    }

    private static void CaptureUploadToGPUTexture(IntPtr copyPass, in SDL3.SDL.GPUTextureTransferInfo source, in SDL3.SDL.GPUTextureRegion destination, bool cycle)
    {
        capturedCopyPass = copyPass;
        capturedTextureTransferInfo = source;
        capturedTextureRegion = destination;
        capturedCycle = cycle;
        capturedCallCount++;
    }

    private static void CaptureUploadToGPUBuffer(IntPtr copyPass, in SDL3.SDL.GPUTransferBufferLocation source, in SDL3.SDL.GPUBufferRegion destination, bool cycle)
    {
        capturedCopyPass = copyPass;
        capturedTransferBufferLocation = source;
        capturedBufferRegion = destination;
        capturedCycle = cycle;
        capturedCallCount++;
    }

    private static void CaptureCopyGPUTextureToTexture(IntPtr copyPass, in SDL3.SDL.GPUTextureLocation source, in SDL3.SDL.GPUTextureLocation destination, uint w, uint h, uint d, bool cycle)
    {
        capturedCopyPass = copyPass;
        capturedSourceTextureLocation = source;
        capturedDestinationTextureLocation = destination;
        capturedWidth = w;
        capturedHeight = h;
        capturedDepth = d;
        capturedCycle = cycle;
        capturedCallCount++;
    }

    private static void CaptureCopyGPUBufferToBuffer(IntPtr copyPass, in SDL3.SDL.GPUBufferLocation source, in SDL3.SDL.GPUBufferLocation destination, uint size, bool cycle)
    {
        capturedCopyPass = copyPass;
        capturedSourceBufferLocation = source;
        capturedDestinationBufferLocation = destination;
        capturedSize = size;
        capturedCycle = cycle;
        capturedCallCount++;
    }

    private static void CaptureDownloadFromGPUTexture(IntPtr copyPass, in SDL3.SDL.GPUTextureRegion source, in SDL3.SDL.GPUTextureTransferInfo destination)
    {
        capturedCopyPass = copyPass;
        capturedTextureRegion = source;
        capturedTextureTransferInfo = destination;
        capturedCallCount++;
    }

    private static void CaptureDownloadFromGPUBuffer(IntPtr copyPass, in SDL3.SDL.GPUTextureRegion source, in SDL3.SDL.GPUTransferBufferLocation destination)
    {
        capturedCopyPass = copyPass;
        capturedTextureRegion = source;
        capturedTransferBufferLocation = destination;
        capturedCallCount++;
    }

    private static void CaptureCopyPassVoid(IntPtr copyPass)
    {
        capturedCopyPass = copyPass;
        capturedCallCount++;
    }

    private static void CaptureCommandResourceVoid(IntPtr commandBuffer, IntPtr resource)
    {
        capturedCommandBuffer = commandBuffer;
        capturedResource = resource;
        capturedCallCount++;
    }

    private static void CaptureBlitGPUTexture(IntPtr commandBuffer, in SDL3.SDL.GPUBlitInfo info)
    {
        capturedCommandBuffer = commandBuffer;
        capturedBlitInfo = info;
        capturedCallCount++;
    }

    private static bool CaptureWindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, SDL3.SDL.GPUSwapchainComposition swapchainComposition)
    {
        capturedDevice = device;
        capturedWindow = window;
        capturedSwapchainComposition = swapchainComposition;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWindowSupportsGPUPresentMode(IntPtr device, IntPtr window, SDL3.SDL.GPUPresentMode presentMode)
    {
        capturedDevice = device;
        capturedWindow = window;
        capturedPresentMode = presentMode;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureDeviceWindowBool(IntPtr device, IntPtr window)
    {
        capturedDevice = device;
        capturedWindow = window;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureDeviceWindowVoid(IntPtr device, IntPtr window)
    {
        capturedDevice = device;
        capturedWindow = window;
        capturedCallCount++;
    }

    private static bool CaptureSetGPUSwapchainParameters(IntPtr device, IntPtr window, SDL3.SDL.GPUSwapchainComposition swapchainComposition, SDL3.SDL.GPUPresentMode presentMode)
    {
        capturedDevice = device;
        capturedWindow = window;
        capturedSwapchainComposition = swapchainComposition;
        capturedPresentMode = presentMode;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetGPUAllowedFramesInFlight(IntPtr device, uint allowedFramesInFlight)
    {
        capturedDevice = device;
        capturedAllowedFramesInFlight = allowedFramesInFlight;
        capturedCallCount++;
        return nextBool;
    }

    private static SDL3.SDL.GPUTextureFormat CaptureDeviceWindowTextureFormat(IntPtr device, IntPtr window)
    {
        capturedDevice = device;
        capturedWindow = window;
        capturedCallCount++;
        return nextGPUTextureFormatValue;
    }

    private static bool CaptureAcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight)
    {
        capturedCommandBuffer = commandBuffer;
        capturedWindow = window;
        capturedSwapchainTexture = nextSwapchainTexture;
        swapchainTexture = nextSwapchainTexture;
        swapchainTextureWidth = nextSwapchainTextureWidth;
        swapchainTextureHeight = nextSwapchainTextureHeight;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureCommandBool(IntPtr commandBuffer)
    {
        capturedCommandBuffer = commandBuffer;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureDeviceBool(IntPtr device)
    {
        capturedDevice = device;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWaitForGPUFencesArray(IntPtr device, bool waitAll, IntPtr[] fences, uint numFences)
    {
        capturedDevice = device;
        capturedWaitAll = waitAll;
        capturedFencesArray = fences;
        capturedNumFences = numFences;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWaitForGPUFencesPointer(IntPtr device, bool waitAll, IntPtr fences, uint numFences)
    {
        capturedDevice = device;
        capturedWaitAll = waitAll;
        capturedFences = fences;
        capturedNumFences = numFences;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGPUFenceBool(IntPtr device, IntPtr fence)
    {
        capturedDevice = device;
        capturedFence = fence;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureGPUFenceVoid(IntPtr device, IntPtr fence)
    {
        capturedDevice = device;
        capturedFence = fence;
        capturedCallCount++;
    }

    private static uint CaptureGPUTextureFormatTexelBlockSize(SDL3.SDL.GPUTextureFormat format)
    {
        capturedGPUTextureFormat = format;
        capturedCallCount++;
        return nextUInt;
    }

    private static bool CaptureGPUTextureSupportsFormat(IntPtr device, SDL3.SDL.GPUTextureFormat format, SDL3.SDL.GPUTextureType type, SDL3.SDL.GPUTextureUsageFlags usage)
    {
        capturedDevice = device;
        capturedGPUTextureFormat = format;
        capturedTextureType = type;
        capturedTextureUsage = usage;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGPUTextureSupportsSampleCount(IntPtr device, SDL3.SDL.GPUTextureFormat format, SDL3.SDL.GPUSampleCount sampleCount)
    {
        capturedDevice = device;
        capturedGPUTextureFormat = format;
        capturedSampleCount = sampleCount;
        capturedCallCount++;
        return nextBool;
    }

    private static uint CaptureCalculateGPUTextureFormatSize(SDL3.SDL.GPUTextureFormat format, uint width, uint height, uint depthOrLayerCount)
    {
        capturedGPUTextureFormat = format;
        capturedWidth = width;
        capturedHeight = height;
        capturedDepthOrLayerCount = depthOrLayerCount;
        capturedCallCount++;
        return nextUInt;
    }

    private static void InvokePushGPUVertexUniformDataPointer(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        SDL3.SDL.PushGPUVertexUniformData(commandBuffer, slotIndex, data, length);
    }

    private static void InvokePushGPUVertexUniformDataArray(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length)
    {
        SDL3.SDL.PushGPUVertexUniformData(commandBuffer, slotIndex, data, length);
    }

    private static void InvokePushGPUFragmentUniformDataPointer(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        SDL3.SDL.PushGPUFragmentUniformData(commandBuffer, slotIndex, data, length);
    }

    private static void InvokePushGPUFragmentUniformDataArray(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length)
    {
        SDL3.SDL.PushGPUFragmentUniformData(commandBuffer, slotIndex, data, length);
    }

    private static void InvokePushGPUComputeUniformDataPointer(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        SDL3.SDL.PushGPUComputeUniformData(commandBuffer, slotIndex, data, length);
    }

    private static void InvokePushGPUComputeUniformDataArray(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length)
    {
        SDL3.SDL.PushGPUComputeUniformData(commandBuffer, slotIndex, data, length);
    }

    private static void InvokeBindGPUVertexStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] pointers, uint numBindings)
    {
        SDL3.SDL.BindGPUVertexStorageTextures(renderPass, firstSlot, pointers, numBindings);
    }

    private static void InvokeBindGPUVertexStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] pointers, uint numBindings)
    {
        SDL3.SDL.BindGPUVertexStorageBuffers(renderPass, firstSlot, pointers, numBindings);
    }

    private static void InvokeBindGPUFragmentStorageTexturesArray(IntPtr renderPass, uint firstSlot, IntPtr[] pointers, uint numBindings)
    {
        SDL3.SDL.BindGPUFragmentStorageTextures(renderPass, firstSlot, pointers, numBindings);
    }

    private static void InvokeBindGPUFragmentStorageBuffersArray(IntPtr renderPass, uint firstSlot, IntPtr[] pointers, uint numBindings)
    {
        SDL3.SDL.BindGPUFragmentStorageBuffers(renderPass, firstSlot, pointers, numBindings);
    }

    private static void InvokeDrawGPUPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount)
    {
        SDL3.SDL.DrawGPUPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    private static void InvokeDrawGPUIndexedPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount)
    {
        SDL3.SDL.DrawGPUIndexedPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    private static void InvokeBindGPUComputeStorageTexturesArray(IntPtr computePass, uint firstSlot, IntPtr[] pointers, uint numBindings)
    {
        SDL3.SDL.BindGPUComputeStorageTextures(computePass, firstSlot, pointers, numBindings);
    }

    private static void InvokeBindGPUComputeStorageBuffersArray(IntPtr computePass, uint firstSlot, IntPtr[] pointers, uint numBindings)
    {
        SDL3.SDL.BindGPUComputeStorageBuffers(computePass, firstSlot, pointers, numBindings);
    }

    private static SDL3.SDL.GPUDepthStencilTargetInfo CreateDepthStencilInfo(IntPtr texture)
    {
        return new SDL3.SDL.GPUDepthStencilTargetInfo
        {
            Texture = texture,
            ClearDepth = 0.5f,
            LoadOp = SDL3.SDL.GPULoadOp.Clear,
            StoreOp = SDL3.SDL.GPUStoreOp.Store,
            StencilLoadOp = SDL3.SDL.GPULoadOp.Load,
            StencilStoreOp = SDL3.SDL.GPUStoreOp.DontCare,
            Cycle = 1,
            ClearStencil = 9,
            MipLevel = 2,
            Layer = 3
        };
    }

    private static SDL3.SDL.GPUTextureTransferInfo CreateTextureTransferInfo(IntPtr transferBuffer)
    {
        return new SDL3.SDL.GPUTextureTransferInfo
        {
            TransferBuffer = transferBuffer,
            Offset = 16,
            PixelsPerRow = 64,
            RowsPerLayer = 8
        };
    }

    private static SDL3.SDL.GPUTextureRegion CreateTextureRegion(IntPtr texture)
    {
        return new SDL3.SDL.GPUTextureRegion
        {
            Texture = texture,
            MipLevel = 1,
            Layer = 2,
            X = 3,
            Y = 4,
            Z = 5,
            W = 64,
            H = 32,
            D = 4
        };
    }

    private static SDL3.SDL.GPUTransferBufferLocation CreateTransferBufferLocation(IntPtr transferBuffer)
    {
        return new SDL3.SDL.GPUTransferBufferLocation
        {
            TransferBuffer = transferBuffer,
            Offset = 24
        };
    }

    private static SDL3.SDL.GPUBufferRegion CreateBufferRegion(IntPtr buffer)
    {
        return new SDL3.SDL.GPUBufferRegion
        {
            Buffer = buffer,
            Offset = 32,
            Size = 128
        };
    }

    private static SDL3.SDL.GPUTextureLocation CreateTextureLocation(IntPtr texture)
    {
        return new SDL3.SDL.GPUTextureLocation
        {
            Texture = texture,
            MipLevel = 1,
            Layer = 2,
            X = 3,
            Y = 4,
            Z = 5
        };
    }

    private static SDL3.SDL.GPUBufferLocation CreateBufferLocation(IntPtr buffer)
    {
        return new SDL3.SDL.GPUBufferLocation
        {
            Buffer = buffer,
            Offset = 40
        };
    }

    private static SDL3.SDL.GPUBlitInfo CreateBlitInfo(IntPtr sourceTexture, IntPtr destinationTexture)
    {
        return new SDL3.SDL.GPUBlitInfo
        {
            Source = new SDL3.SDL.GPUBlitRegion
            {
                Texture = sourceTexture,
                MipLevel = 1,
                LayerOrDepthPlane = 2,
                X = 3,
                Y = 4,
                W = 64,
                H = 32
            },
            Destination = new SDL3.SDL.GPUBlitRegion
            {
                Texture = destinationTexture,
                MipLevel = 5,
                LayerOrDepthPlane = 6,
                X = 7,
                Y = 8,
                W = 16,
                H = 8
            },
            LoadOp = SDL3.SDL.GPULoadOp.Clear,
            ClearColor = new SDL3.SDL.FColor(0.1f, 0.2f, 0.3f, 0.4f),
            FlipMode = SDL3.SDL.FlipMode.Horizontal,
            Filter = SDL3.SDL.GPUFilter.Linear,
            Cycle = true
        };
    }

    private static void AssertUniformPointerMethod(
        string publicName,
        string nativeName,
        string hookFieldName,
        Action<IntPtr, uint, IntPtr, uint> invoke)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName, typeof(IntPtr), typeof(uint), typeof(IntPtr), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureUniformPointer));

        invoke((IntPtr)3001, 2, (IntPtr)3002, 16);

        TestAssert.Equal((IntPtr)3001, capturedCommandBuffer, $"SDL.{publicName} pointer overload must forward commandBuffer.");
        TestAssert.Equal<uint>(2, capturedSlotIndex, $"SDL.{publicName} pointer overload must forward slotIndex.");
        TestAssert.Equal((IntPtr)3002, capturedData, $"SDL.{publicName} pointer overload must forward data.");
        TestAssert.Equal<uint>(16, capturedLength, $"SDL.{publicName} pointer overload must forward length.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} pointer overload must call native hook once.");
    }

    private static void AssertUniformArrayMethod(
        string publicName,
        string nativeName,
        string hookFieldName,
        Action<IntPtr, uint, byte[], uint> invoke)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName, typeof(IntPtr), typeof(uint), typeof(byte[]), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 3);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureUniformArray));
        byte[] data = [1, 2, 3, 4];

        invoke((IntPtr)3101, 3, data, (uint)data.Length);

        TestAssert.Equal((IntPtr)3101, capturedCommandBuffer, $"SDL.{publicName} array overload must forward commandBuffer.");
        TestAssert.Equal<uint>(3, capturedSlotIndex, $"SDL.{publicName} array overload must forward slotIndex.");
        TestAssert.True(ReferenceEquals(data, capturedDataArray), $"SDL.{publicName} array overload must forward the byte array.");
        TestAssert.Equal<uint>((uint)data.Length, capturedLength, $"SDL.{publicName} array overload must forward length.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} array overload must call native hook once.");
    }

    private static void AssertRenderPassSlotPointerMethod(string publicName, string nativeName, string hookFieldName, string pointerParameterName)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName, typeof(IntPtr), typeof(uint), typeof(IntPtr), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureRenderPassSlotPointer));

        InvokePublic(publicName, [typeof(IntPtr), typeof(uint), typeof(IntPtr), typeof(uint)], (IntPtr)3901, 9u, (IntPtr)3902, 2u);

        TestAssert.Equal((IntPtr)3901, capturedRenderPass, $"SDL.{publicName} pointer overload must forward renderPass.");
        TestAssert.Equal<uint>(9, capturedFirstSlot, $"SDL.{publicName} pointer overload must forward firstSlot.");
        TestAssert.Equal((IntPtr)3902, capturedBindings, $"SDL.{publicName} pointer overload must forward {pointerParameterName}.");
        TestAssert.Equal<uint>(2, capturedNumBindings, $"SDL.{publicName} pointer overload must forward numBindings.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} pointer overload must call native hook once.");
    }

    private static void AssertRenderPassSlotPointerArrayMethod(
        string publicName,
        string nativeName,
        string hookFieldName,
        Action<IntPtr, uint, IntPtr[], uint> invoke)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName, typeof(IntPtr), typeof(uint), typeof(IntPtr[]), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureRenderPassSlotPointerArray));
        IntPtr[] pointers = [(IntPtr)4001, (IntPtr)4002];

        invoke((IntPtr)4003, 10, pointers, (uint)pointers.Length);

        TestAssert.Equal((IntPtr)4003, capturedRenderPass, $"SDL.{publicName} array overload must forward renderPass.");
        TestAssert.Equal<uint>(10, capturedFirstSlot, $"SDL.{publicName} array overload must forward firstSlot.");
        TestAssert.True(ReferenceEquals(pointers, capturedPointerArray), $"SDL.{publicName} array overload must forward pointer array.");
        TestAssert.Equal<uint>((uint)pointers.Length, capturedNumBindings, $"SDL.{publicName} array overload must forward numBindings.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} array overload must call native hook once.");
    }

    private static void AssertDrawIndirectMethod(
        string publicName,
        string nativeName,
        string hookFieldName,
        Action<IntPtr, IntPtr, uint, uint> invoke)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureDrawIndirect));

        invoke((IntPtr)4101, (IntPtr)4102, 64, 3);

        TestAssert.Equal((IntPtr)4101, capturedRenderPass, $"SDL.{publicName} must forward renderPass.");
        TestAssert.Equal((IntPtr)4102, capturedBuffer, $"SDL.{publicName} must forward buffer.");
        TestAssert.Equal<uint>(64, capturedOffset, $"SDL.{publicName} must forward offset.");
        TestAssert.Equal<uint>(3, capturedDrawCount, $"SDL.{publicName} must forward drawCount.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertComputePassSlotPointerMethod(string publicName, string nativeName, string hookFieldName, string pointerParameterName)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName, typeof(IntPtr), typeof(uint), typeof(IntPtr), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureComputePassSlotPointer));

        InvokePublic(publicName, [typeof(IntPtr), typeof(uint), typeof(IntPtr), typeof(uint)], (IntPtr)4801, 12u, (IntPtr)4802, 2u);

        TestAssert.Equal((IntPtr)4801, capturedComputePass, $"SDL.{publicName} pointer overload must forward computePass.");
        TestAssert.Equal<uint>(12, capturedFirstSlot, $"SDL.{publicName} pointer overload must forward firstSlot.");
        TestAssert.Equal((IntPtr)4802, capturedBindings, $"SDL.{publicName} pointer overload must forward {pointerParameterName}.");
        TestAssert.Equal<uint>(2, capturedNumBindings, $"SDL.{publicName} pointer overload must forward numBindings.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} pointer overload must call native hook once.");
    }

    private static void AssertComputePassSlotPointerArrayMethod(
        string publicName,
        string nativeName,
        string hookFieldName,
        Action<IntPtr, uint, IntPtr[], uint> invoke)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName, typeof(IntPtr), typeof(uint), typeof(IntPtr[]), typeof(uint));
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureComputePassSlotPointerArray));
        IntPtr[] pointers = [(IntPtr)4901, (IntPtr)4902];

        invoke((IntPtr)4903, 13, pointers, (uint)pointers.Length);

        TestAssert.Equal((IntPtr)4903, capturedComputePass, $"SDL.{publicName} array overload must forward computePass.");
        TestAssert.Equal<uint>(13, capturedFirstSlot, $"SDL.{publicName} array overload must forward firstSlot.");
        TestAssert.True(ReferenceEquals(pointers, capturedPointerArray), $"SDL.{publicName} array overload must forward pointer array.");
        TestAssert.Equal<uint>((uint)pointers.Length, capturedNumBindings, $"SDL.{publicName} array overload must forward numBindings.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} array overload must call native hook once.");
    }

    private static void AssertPropsPointerMethod(string publicName, string nativeName, string hookFieldName, uint props, IntPtr expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextPointer = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CapturePropsPointer));

        IntPtr result = (IntPtr)InvokePublic(publicName, props)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native pointer.");
        TestAssert.Equal(props, capturedProps, $"SDL.{publicName} must forward props.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertDeviceVoidMethod(string publicName, string nativeName, string hookFieldName, IntPtr device)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureDeviceVoid));

        InvokePublic(publicName, device);

        TestAssert.Equal(device, capturedDevice, $"SDL.{publicName} must forward device.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertDeviceUIntMethod(string publicName, string nativeName, string hookFieldName, IntPtr device, uint expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextUInt = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureDeviceUInt));

        uint result = (uint)InvokePublic(publicName, device)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native uint value.");
        TestAssert.Equal(device, capturedDevice, $"SDL.{publicName} must forward device.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertDeviceResourceTextMethod(string publicName, string nativeName, string hookFieldName, string parameterName)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertStringParameterMarshal(nativeMethod, "text", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureDeviceResourceText));

        InvokePublic(publicName, (IntPtr)1701, (IntPtr)1702, $"{parameterName}-name");

        TestAssert.Equal((IntPtr)1701, capturedDevice, $"SDL.{publicName} must forward device.");
        TestAssert.Equal((IntPtr)1702, capturedResource, $"SDL.{publicName} must forward {parameterName}.");
        TestAssert.Equal($"{parameterName}-name", capturedText, $"SDL.{publicName} must forward text.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertCommandTextMethod(string publicName, string nativeName, string hookFieldName)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertStringParameterMarshal(nativeMethod, "text", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureCommandText));

        InvokePublic(publicName, (IntPtr)1801, "debug");

        TestAssert.Equal((IntPtr)1801, capturedCommandBuffer, $"SDL.{publicName} must forward commandBuffer.");
        TestAssert.Equal("debug", capturedText, $"SDL.{publicName} must forward text.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertReleaseMethod(string publicName, string nativeName, string hookFieldName, string parameterName)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureDeviceResourceVoid));

        InvokePublic(publicName, (IntPtr)1901, (IntPtr)1902);

        TestAssert.Equal((IntPtr)1901, capturedDevice, $"SDL.{publicName} must forward device.");
        TestAssert.Equal((IntPtr)1902, capturedResource, $"SDL.{publicName} must forward {parameterName}.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static object? InvokePublic(string methodName, params object[] arguments)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} public wrapper must exist.");
        return method!.Invoke(null, arguments);
    }

    private static object? InvokePublic(string methodName, Type[] parameterTypes, params object[] arguments)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} public wrapper overload must exist.");
        return method!.Invoke(null, arguments);
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static MethodInfo GetNativeMethod(string methodName, params Type[] parameterTypes)
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
        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertCdecl(MethodInfo method, string apiName)
    {
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"{apiName} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"{apiName} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"{apiName} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method, UnmanagedType unmanagedType)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} return value must use expected bool marshalling.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected bool marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected string marshalling.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType, short sizeParamIndex)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected array marshalling.");
        TestAssert.Equal(sizeParamIndex, marshalAs.SizeParamIndex, $"SDL.{method.Name} parameter {parameterName} must keep expected SizeParamIndex.");
    }

    private static void AssertByRefParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"SDL.{method.Name} parameter {parameterName} must stay by reference.");
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? originalValue;

        private NativeHookScope(FieldInfo field, object? originalValue)
        {
            this.field = field;
            this.originalValue = originalValue;
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL.{fieldName} native hook field must exist.");
            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"{methodName} hook method must exist.");
            object? originalValue = field!.GetValue(null);
            Delegate hook = Delegate.CreateDelegate(field.FieldType, method!);
            field.SetValue(null, hook);
            return new NativeHookScope(field, originalValue);
        }

        public void Dispose()
        {
            field.SetValue(null, originalValue);
        }
    }
}
