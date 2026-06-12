using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.ShaderCross;

internal static class PInvokeTests
{
    private static SDL3.SDL.GPUShaderFormat nextShaderFormat;
    private static IntPtr nextPointer;
    private static UIntPtr nextSize;
    private static IntPtr capturedDevice;
    private static IntPtr capturedBytecode;
    private static UIntPtr capturedBytecodeSize;
    private static uint capturedProps;
    private static bool nextBool;
    private static int capturedCallCount;
    private static SDL3.ShaderCross.SPIRVInfo capturedSpirvInfo;
    private static SDL3.ShaderCross.HLSLInfo capturedHlslInfo;
    private static SDL3.ShaderCross.GraphicsShaderResourceInfo capturedResourceInfo;
    private static SDL3.ShaderCross.ComputePipelineMetadata capturedComputeMetadata;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        PropsAndComputeMetadata_MatchUpstreamHeader();
        ManagedStringProperties_SetPointersAndRoundTripValues();
        LifecycleAndFormatFunctions_ForwardInputsAndReturnNativeValues();
        SPIRVFunctions_ForwardInputsOutputsAndReturnNativeValues();
        HLSLFunctions_ForwardInputsOutputsAndReturnNativeValues();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertNativeBoolImport(GetNativeMethod("SDL_ShaderCross_Init"), "SDL_ShaderCross_Init");
        AssertNativeImport(GetNativeMethod("SDL_ShaderCross_Quit"), "SDL_ShaderCross_Quit");

        MethodInfo getSpirvFormats = GetNativeMethod("SDL_ShaderCross_GetSPIRVShaderFormats");
        AssertNativeImport(getSpirvFormats, "SDL_ShaderCross_GetSPIRVShaderFormats");
        AssertParameterTypes(getSpirvFormats);

        MethodInfo transpileMsl = GetNativeMethod("SDL_ShaderCross_TranspileMSLFromSPIRV");
        AssertNativeImport(transpileMsl, "SDL_ShaderCross_TranspileMSLFromSPIRV");
        AssertParameterTypes(transpileMsl, typeof(SDL3.ShaderCross.SPIRVInfo).MakeByRefType());

        MethodInfo transpileHlsl = GetNativeMethod("SDL_ShaderCross_TranspileHLSLFromSPIRV");
        AssertNativeImport(transpileHlsl, "SDL_ShaderCross_TranspileHLSLFromSPIRV");
        AssertParameterTypes(transpileHlsl, typeof(SDL3.ShaderCross.SPIRVInfo).MakeByRefType());

        MethodInfo compileDxbcSpirv = GetNativeMethod("SDL_ShaderCross_CompileDXBCFromSPIRV");
        AssertNativeImport(compileDxbcSpirv, "SDL_ShaderCross_CompileDXBCFromSPIRV");
        AssertParameterTypes(compileDxbcSpirv, typeof(SDL3.ShaderCross.SPIRVInfo).MakeByRefType(), typeof(UIntPtr).MakeByRefType());
        AssertOutParameter(compileDxbcSpirv, 1);

        MethodInfo compileDxilSpirv = GetNativeMethod("SDL_ShaderCross_CompileDXILFromSPIRV");
        AssertNativeImport(compileDxilSpirv, "SDL_ShaderCross_CompileDXILFromSPIRV");
        AssertParameterTypes(compileDxilSpirv, typeof(SDL3.ShaderCross.SPIRVInfo).MakeByRefType(), typeof(UIntPtr).MakeByRefType());
        AssertOutParameter(compileDxilSpirv, 1);

        MethodInfo compileGraphics = GetNativeMethod("SDL_ShaderCross_CompileGraphicsShaderFromSPIRV");
        AssertNativeImport(compileGraphics, "SDL_ShaderCross_CompileGraphicsShaderFromSPIRV");
        AssertParameterTypes(
            compileGraphics,
            typeof(IntPtr),
            typeof(SDL3.ShaderCross.SPIRVInfo).MakeByRefType(),
            typeof(SDL3.ShaderCross.GraphicsShaderResourceInfo).MakeByRefType(),
            typeof(uint));

        MethodInfo compileCompute = GetNativeMethod("SDL_ShaderCross_CompileComputePipelineFromSPIRV");
        AssertNativeImport(compileCompute, "SDL_ShaderCross_CompileComputePipelineFromSPIRV");
        AssertParameterTypes(
            compileCompute,
            typeof(IntPtr),
            typeof(SDL3.ShaderCross.SPIRVInfo).MakeByRefType(),
            typeof(SDL3.ShaderCross.ComputePipelineMetadata).MakeByRefType(),
            typeof(uint));

        MethodInfo reflectGraphics = GetNativeMethod("SDL_ShaderCross_ReflectGraphicsSPIRV");
        AssertNativeImport(reflectGraphics, "SDL_ShaderCross_ReflectGraphicsSPIRV");
        AssertParameterTypes(reflectGraphics, typeof(IntPtr), typeof(UIntPtr), typeof(uint));

        MethodInfo reflectCompute = GetNativeMethod("SDL_ShaderCross_ReflectComputeSPIRV");
        AssertNativeImport(reflectCompute, "SDL_ShaderCross_ReflectComputeSPIRV");
        AssertParameterTypes(reflectCompute, typeof(IntPtr), typeof(UIntPtr), typeof(uint));

        MethodInfo getHlslFormats = GetNativeMethod("SDL_ShaderCross_GetHLSLShaderFormats");
        AssertNativeImport(getHlslFormats, "SDL_ShaderCross_GetHLSLShaderFormats");
        AssertParameterTypes(getHlslFormats);

        MethodInfo compileDxbcHlsl = GetNativeMethod("SDL_ShaderCross_CompileDXBCFromHLSL");
        AssertNativeImport(compileDxbcHlsl, "SDL_ShaderCross_CompileDXBCFromHLSL");
        AssertParameterTypes(compileDxbcHlsl, typeof(SDL3.ShaderCross.HLSLInfo).MakeByRefType(), typeof(UIntPtr).MakeByRefType());
        AssertOutParameter(compileDxbcHlsl, 1);

        MethodInfo compileDxilHlsl = GetNativeMethod("SDL_ShaderCross_CompileDXILFromHLSL");
        AssertNativeImport(compileDxilHlsl, "SDL_ShaderCross_CompileDXILFromHLSL");
        AssertParameterTypes(compileDxilHlsl, typeof(SDL3.ShaderCross.HLSLInfo).MakeByRefType(), typeof(UIntPtr).MakeByRefType());
        AssertOutParameter(compileDxilHlsl, 1);

        MethodInfo compileSpirvHlsl = GetNativeMethod("SDL_ShaderCross_CompileSPIRVFromHLSL");
        AssertNativeImport(compileSpirvHlsl, "SDL_ShaderCross_CompileSPIRVFromHLSL");
        AssertParameterTypes(compileSpirvHlsl, typeof(SDL3.ShaderCross.HLSLInfo).MakeByRefType(), typeof(UIntPtr).MakeByRefType());
        AssertOutParameter(compileSpirvHlsl, 1);
    }

    public static void PropsAndComputeMetadata_MatchUpstreamHeader()
    {
        TestAssert.Equal("SDL_shadercross.spirv.debug.enable", SDL3.ShaderCross.Props.ShaderDebugEnableBoolean, "ShaderCross debug enable property must match upstream.");
        TestAssert.Equal("SDL_shadercross.spirv.debug.name", SDL3.ShaderCross.Props.ShaderDebugNameString, "ShaderCross debug name property must match upstream.");
        TestAssert.Equal("SDL_shadercross.spirv.cull_unused_bindings", SDL3.ShaderCross.Props.ShaderCullUnusedBindingsBoolean, "ShaderCross cull unused bindings property must match upstream.");
        TestAssert.Equal("SDL_shadercross.spirv.pssl.compatibility", SDL3.ShaderCross.Props.SPIRVPSSLCompatibilityBoolean, "ShaderCross PSSL compatibility property must match upstream.");
        TestAssert.Equal("SDL_shadercross.spirv.msl.version", SDL3.ShaderCross.Props.SPIRVMSLVersionString, "ShaderCross MSL version property must match upstream.");
        TestAssert.Equal("SDL_shadercross.hlsl.skip_spirv_roundtrip", SDL3.ShaderCross.Props.HLSLSkipSPIRVRoundtripBoolean, "ShaderCross HLSL skip SPIRV roundtrip property must match upstream.");

        FieldInfo[] fields = typeof(SDL3.ShaderCross.ComputePipelineMetadata).GetFields(BindingFlags.Public | BindingFlags.Instance);
        string[] fieldNames = fields.Select(static field => field.Name).ToArray();
        string[] expectedNames =
        [
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.NumSamplers),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.NumReadOnlyStorageTextures),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.NumReadOnlyStorageBuffers),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.NumReadWriteStorageTextures),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.NumReadWriteStorageBuffers),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.NumUniformBuffers),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.ThreadCountX),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.ThreadCountY),
            nameof(SDL3.ShaderCross.ComputePipelineMetadata.ThreadCountZ)
        ];

        TestAssert.Equal(expectedNames.Length, fieldNames.Length, "ComputePipelineMetadata must expose exactly the upstream C fields.");

        for (int i = 0; i < expectedNames.Length; i++)
        {
            TestAssert.Equal(expectedNames[i], fieldNames[i], $"ComputePipelineMetadata field {i} must match upstream order.");
            TestAssert.Equal(typeof(uint), fields[i].FieldType, $"ComputePipelineMetadata field {expectedNames[i]} must marshal as Uint32.");
        }

        TestAssert.Equal(36, Marshal.SizeOf<SDL3.ShaderCross.ComputePipelineMetadata>(), "ComputePipelineMetadata size must match nine Uint32 fields.");
    }

    public static void ManagedStringProperties_SetPointersAndRoundTripValues()
    {
        SDL3.ShaderCross.HLSLDefine define = default;
        TestAssert.Equal<string?>(null, define.ManagedName, "HLSLDefine.ManagedName must decode null pointer as null.");
        define.ManagedName = null!;
        TestAssert.Equal(IntPtr.Zero, define.Name, "HLSLDefine.ManagedName setter must store zero for null.");
        define.ManagedName = "USE_LIGHTING";
        IntPtr defineName = define.Name;

        try
        {
            TestAssert.True(defineName != IntPtr.Zero, "HLSLDefine.ManagedName setter must allocate pointer for text.");
            TestAssert.Equal("USE_LIGHTING", define.ManagedName, "HLSLDefine.ManagedName must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(defineName);
        }

        define.ManagedValue = null;
        TestAssert.Equal(IntPtr.Zero, define.Value, "HLSLDefine.ManagedValue setter must store zero for null.");
        TestAssert.Equal<string?>(null, define.ManagedValue, "HLSLDefine.ManagedValue must decode null pointer as null.");
        define.ManagedValue = "1";
        IntPtr defineValue = define.Value;

        try
        {
            TestAssert.True(defineValue != IntPtr.Zero, "HLSLDefine.ManagedValue setter must allocate pointer for text.");
            TestAssert.Equal("1", define.ManagedValue, "HLSLDefine.ManagedValue must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(defineValue);
        }

        SDL3.ShaderCross.HLSLInfo hlsl = default;
        TestAssert.Equal<string?>(null, hlsl.ManagedSource, "HLSLInfo.ManagedSource must decode null pointer as null.");
        hlsl.ManagedSource = null!;
        TestAssert.Equal(IntPtr.Zero, hlsl.Source, "HLSLInfo.ManagedSource setter must store zero for null.");
        hlsl.ManagedSource = "float4 main() : SV_Target { return 1; }";
        IntPtr hlslSource = hlsl.Source;

        try
        {
            TestAssert.True(hlslSource != IntPtr.Zero, "HLSLInfo.ManagedSource setter must allocate pointer for text.");
            TestAssert.Equal("float4 main() : SV_Target { return 1; }", hlsl.ManagedSource, "HLSLInfo.ManagedSource must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(hlslSource);
        }

        TestAssert.Equal<string?>(null, hlsl.ManagedEntrypoint, "HLSLInfo.ManagedEntrypoint must decode null pointer as null.");
        hlsl.ManagedEntrypoint = null!;
        TestAssert.Equal(IntPtr.Zero, hlsl.Entrypoint, "HLSLInfo.ManagedEntrypoint setter must store zero for null.");
        hlsl.ManagedEntrypoint = "main";
        IntPtr hlslEntrypoint = hlsl.Entrypoint;

        try
        {
            TestAssert.True(hlslEntrypoint != IntPtr.Zero, "HLSLInfo.ManagedEntrypoint setter must allocate pointer for text.");
            TestAssert.Equal("main", hlsl.ManagedEntrypoint, "HLSLInfo.ManagedEntrypoint must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(hlslEntrypoint);
        }

        hlsl.ManagedIncludeDir = null;
        TestAssert.Equal(IntPtr.Zero, hlsl.IncludeDir, "HLSLInfo.ManagedIncludeDir setter must store zero for null.");
        TestAssert.Equal<string?>(null, hlsl.ManagedIncludeDir, "HLSLInfo.ManagedIncludeDir must decode null pointer as null.");
        hlsl.ManagedIncludeDir = "shaders/includes";
        IntPtr hlslIncludeDir = hlsl.IncludeDir;

        try
        {
            TestAssert.True(hlslIncludeDir != IntPtr.Zero, "HLSLInfo.ManagedIncludeDir setter must allocate pointer for text.");
            TestAssert.Equal("shaders/includes", hlsl.ManagedIncludeDir, "HLSLInfo.ManagedIncludeDir must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(hlslIncludeDir);
        }

        SDL3.ShaderCross.IOVarMetadata metadata = default;
        TestAssert.Equal<string?>(null, metadata.Name, "IOVarMetadata.Name must decode null pointer as null.");
        metadata.Name = null!;
        TestAssert.Equal(IntPtr.Zero, GetIOVarMetadataNamePointer(metadata), "IOVarMetadata.Name setter must store zero for null.");
        metadata.Name = "TEXCOORD0";
        IntPtr metadataName = GetIOVarMetadataNamePointer(metadata);

        try
        {
            TestAssert.True(metadataName != IntPtr.Zero, "IOVarMetadata.Name setter must allocate pointer for text.");
            TestAssert.Equal("TEXCOORD0", metadata.Name, "IOVarMetadata.Name must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(metadataName);
        }

        SDL3.ShaderCross.SPIRVInfo spirv = default;
        TestAssert.Equal<string?>(null, spirv.ManagedEntrypoint, "SPIRVInfo.ManagedEntrypoint must decode null pointer as null.");
        spirv.ManagedEntrypoint = null!;
        TestAssert.Equal(IntPtr.Zero, spirv.Entrypoint, "SPIRVInfo.ManagedEntrypoint setter must store zero for null.");
        spirv.ManagedEntrypoint = "cs_main";
        IntPtr spirvEntrypoint = spirv.Entrypoint;

        try
        {
            TestAssert.True(spirvEntrypoint != IntPtr.Zero, "SPIRVInfo.ManagedEntrypoint setter must allocate pointer for text.");
            TestAssert.Equal("cs_main", spirv.ManagedEntrypoint, "SPIRVInfo.ManagedEntrypoint must round-trip text.");
        }
        finally
        {
            Marshal.FreeHGlobal(spirvEntrypoint);
        }
    }

    public static void LifecycleAndFormatFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("InitNativeFunction", nameof(ReturnNextBool)))
        {
            bool actual = SDL3.ShaderCross.Init();

            TestAssert.Equal(true, actual, "ShaderCross.Init must return native success value.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.Init must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("QuitNativeFunction", nameof(CaptureNoArgVoid)))
        {
            SDL3.ShaderCross.Quit();

            TestAssert.Equal(1, capturedCallCount, "ShaderCross.Quit must call native hook once.");
        }

        ResetCaptureState();
        nextShaderFormat = SDL3.SDL.GPUShaderFormat.SPIRV | SDL3.SDL.GPUShaderFormat.MSL;
        using (NativeHookScope _ = NativeHookScope.Install("GetSPIRVShaderFormatsNativeFunction", nameof(ReturnNextShaderFormat)))
        {
            SDL3.SDL.GPUShaderFormat actual = SDL3.ShaderCross.GetSPIRVShaderFormats();

            TestAssert.Equal(nextShaderFormat, actual, "ShaderCross.GetSPIRVShaderFormats must return native format flags.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.GetSPIRVShaderFormats must call native hook once.");
        }

        ResetCaptureState();
        nextShaderFormat = SDL3.SDL.GPUShaderFormat.DXBC | SDL3.SDL.GPUShaderFormat.DXIL;
        using (NativeHookScope _ = NativeHookScope.Install("GetHLSLShaderFormatsNativeFunction", nameof(ReturnNextShaderFormat)))
        {
            SDL3.SDL.GPUShaderFormat actual = SDL3.ShaderCross.GetHLSLShaderFormats();

            TestAssert.Equal(nextShaderFormat, actual, "ShaderCross.GetHLSLShaderFormats must return native format flags.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.GetHLSLShaderFormats must call native hook once.");
        }
    }

    public static void SPIRVFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        SDL3.ShaderCross.SPIRVInfo info = CreateSpirvInfo();
        SDL3.ShaderCross.GraphicsShaderResourceInfo resourceInfo = CreateResourceInfo();
        SDL3.ShaderCross.ComputePipelineMetadata computeMetadata = CreateComputeMetadata();

        ResetCaptureState();
        nextPointer = (IntPtr)0x1001;
        using (NativeHookScope _ = NativeHookScope.Install("TranspileMSLFromSPIRVNativeFunction", nameof(CaptureSpirvInfoReturnPointer)))
        {
            IntPtr actual = SDL3.ShaderCross.TranspileMSLFromSPIRV(in info);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.TranspileMSLFromSPIRV must return native pointer.");
            AssertSpirvInfoEqual(info, capturedSpirvInfo, "ShaderCross.TranspileMSLFromSPIRV must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.TranspileMSLFromSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1002;
        using (NativeHookScope _ = NativeHookScope.Install("TranspileHLSLFromSPIRVNativeFunction", nameof(CaptureSpirvInfoReturnPointer)))
        {
            IntPtr actual = SDL3.ShaderCross.TranspileHLSLFromSPIRV(in info);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.TranspileHLSLFromSPIRV must return native pointer.");
            AssertSpirvInfoEqual(info, capturedSpirvInfo, "ShaderCross.TranspileHLSLFromSPIRV must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.TranspileHLSLFromSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1003;
        nextSize = (UIntPtr)3072;
        using (NativeHookScope _ = NativeHookScope.Install("CompileDXBCFromSPIRVNativeFunction", nameof(CaptureSpirvInfoReturnPointerAndSize)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileDXBCFromSPIRV(in info, out UIntPtr size);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileDXBCFromSPIRV must return native pointer.");
            TestAssert.Equal(nextSize, size, "ShaderCross.CompileDXBCFromSPIRV must return native size.");
            AssertSpirvInfoEqual(info, capturedSpirvInfo, "ShaderCross.CompileDXBCFromSPIRV must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileDXBCFromSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1004;
        nextSize = (UIntPtr)4096;
        using (NativeHookScope _ = NativeHookScope.Install("CompileDXILFromSPIRVNativeFunction", nameof(CaptureSpirvInfoReturnPointerAndSize)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileDXILFromSPIRV(in info, out UIntPtr size);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileDXILFromSPIRV must return native pointer.");
            TestAssert.Equal(nextSize, size, "ShaderCross.CompileDXILFromSPIRV must return native size.");
            AssertSpirvInfoEqual(info, capturedSpirvInfo, "ShaderCross.CompileDXILFromSPIRV must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileDXILFromSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1005;
        using (NativeHookScope _ = NativeHookScope.Install("CompileGraphicsShaderFromSPIRVNativeFunction", nameof(CaptureCompileGraphicsShaderFromSPIRV)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileGraphicsShaderFromSPIRV((IntPtr)0x2001, ref info, ref resourceInfo, 77);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileGraphicsShaderFromSPIRV must return native pointer.");
            TestAssert.Equal((IntPtr)0x2001, capturedDevice, "ShaderCross.CompileGraphicsShaderFromSPIRV must forward device.");
            TestAssert.Equal(77u, capturedProps, "ShaderCross.CompileGraphicsShaderFromSPIRV must forward props.");
            AssertSpirvInfoEqual(info, capturedSpirvInfo, "ShaderCross.CompileGraphicsShaderFromSPIRV must forward info.");
            AssertResourceInfoEqual(resourceInfo, capturedResourceInfo, "ShaderCross.CompileGraphicsShaderFromSPIRV must forward resource info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileGraphicsShaderFromSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1006;
        using (NativeHookScope _ = NativeHookScope.Install("CompileComputePipelineFromSPIRVNativeFunction", nameof(CaptureCompileComputePipelineFromSPIRV)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileComputePipelineFromSPIRV((IntPtr)0x2002, in info, in computeMetadata, 78);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileComputePipelineFromSPIRV must return native pointer.");
            TestAssert.Equal((IntPtr)0x2002, capturedDevice, "ShaderCross.CompileComputePipelineFromSPIRV must forward device.");
            TestAssert.Equal(78u, capturedProps, "ShaderCross.CompileComputePipelineFromSPIRV must forward props.");
            AssertSpirvInfoEqual(info, capturedSpirvInfo, "ShaderCross.CompileComputePipelineFromSPIRV must forward info.");
            AssertComputeMetadataEqual(computeMetadata, capturedComputeMetadata, "ShaderCross.CompileComputePipelineFromSPIRV must forward metadata.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileComputePipelineFromSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1007;
        using (NativeHookScope _ = NativeHookScope.Install("ReflectGraphicsSPIRVNativeFunction", nameof(CaptureReflectBytecode)))
        {
            IntPtr actual = SDL3.ShaderCross.ReflectGraphicsSPIRV((IntPtr)0x3001, (UIntPtr)512, 79);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.ReflectGraphicsSPIRV must return native pointer.");
            TestAssert.Equal((IntPtr)0x3001, capturedBytecode, "ShaderCross.ReflectGraphicsSPIRV must forward bytecode.");
            TestAssert.Equal((UIntPtr)512, capturedBytecodeSize, "ShaderCross.ReflectGraphicsSPIRV must forward bytecode size.");
            TestAssert.Equal(79u, capturedProps, "ShaderCross.ReflectGraphicsSPIRV must forward props.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.ReflectGraphicsSPIRV must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1008;
        using (NativeHookScope _ = NativeHookScope.Install("ReflectComputeSPIRVNativeFunction", nameof(CaptureReflectBytecode)))
        {
            IntPtr actual = SDL3.ShaderCross.ReflectComputeSPIRV((IntPtr)0x3002, (UIntPtr)1024, 80);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.ReflectComputeSPIRV must return native pointer.");
            TestAssert.Equal((IntPtr)0x3002, capturedBytecode, "ShaderCross.ReflectComputeSPIRV must forward bytecode.");
            TestAssert.Equal((UIntPtr)1024, capturedBytecodeSize, "ShaderCross.ReflectComputeSPIRV must forward bytecode size.");
            TestAssert.Equal(80u, capturedProps, "ShaderCross.ReflectComputeSPIRV must forward props.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.ReflectComputeSPIRV must call native hook once.");
        }
    }

    public static void HLSLFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        SDL3.ShaderCross.HLSLInfo info = CreateHlslInfo();

        ResetCaptureState();
        nextPointer = (IntPtr)0x4001;
        nextSize = (UIntPtr)1536;
        using (NativeHookScope _ = NativeHookScope.Install("CompileDXBCFromHLSLNativeFunction", nameof(CaptureHlslInfoReturnPointerAndSize)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileDXBCFromHLSL(in info, out UIntPtr size);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileDXBCFromHLSL must return native pointer.");
            TestAssert.Equal(nextSize, size, "ShaderCross.CompileDXBCFromHLSL must return native size.");
            AssertHlslInfoEqual(info, capturedHlslInfo, "ShaderCross.CompileDXBCFromHLSL must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileDXBCFromHLSL must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x4002;
        nextSize = (UIntPtr)2048;
        using (NativeHookScope _ = NativeHookScope.Install("CompileDXILFromHLSLNativeFunction", nameof(CaptureHlslInfoReturnPointerAndSize)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileDXILFromHLSL(in info, out UIntPtr size);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileDXILFromHLSL must return native pointer.");
            TestAssert.Equal(nextSize, size, "ShaderCross.CompileDXILFromHLSL must return native size.");
            AssertHlslInfoEqual(info, capturedHlslInfo, "ShaderCross.CompileDXILFromHLSL must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileDXILFromHLSL must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x4003;
        nextSize = (UIntPtr)2560;
        using (NativeHookScope _ = NativeHookScope.Install("CompileSPIRVFromHLSLNativeFunction", nameof(CaptureCompileSPIRVFromHLSL)))
        {
            IntPtr actual = SDL3.ShaderCross.CompileSPIRVFromHLSL(ref info, out UIntPtr size);

            TestAssert.Equal(nextPointer, actual, "ShaderCross.CompileSPIRVFromHLSL must return native pointer.");
            TestAssert.Equal(nextSize, size, "ShaderCross.CompileSPIRVFromHLSL must return native size.");
            AssertHlslInfoEqual(info, capturedHlslInfo, "ShaderCross.CompileSPIRVFromHLSL must forward info.");
            TestAssert.Equal(1, capturedCallCount, "ShaderCross.CompileSPIRVFromHLSL must call native hook once.");
        }
    }

    private static bool ReturnNextBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureNoArgVoid()
    {
        capturedCallCount++;
    }

    private static SDL3.SDL.GPUShaderFormat ReturnNextShaderFormat()
    {
        capturedCallCount++;
        return nextShaderFormat;
    }

    private static IntPtr CaptureSpirvInfoReturnPointer(in SDL3.ShaderCross.SPIRVInfo info)
    {
        capturedSpirvInfo = info;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureSpirvInfoReturnPointerAndSize(in SDL3.ShaderCross.SPIRVInfo info, out UIntPtr size)
    {
        capturedSpirvInfo = info;
        size = nextSize;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCompileGraphicsShaderFromSPIRV(
        IntPtr device,
        ref SDL3.ShaderCross.SPIRVInfo info,
        ref SDL3.ShaderCross.GraphicsShaderResourceInfo resourceInfo,
        uint props)
    {
        capturedDevice = device;
        capturedSpirvInfo = info;
        capturedResourceInfo = resourceInfo;
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCompileComputePipelineFromSPIRV(
        IntPtr device,
        in SDL3.ShaderCross.SPIRVInfo info,
        in SDL3.ShaderCross.ComputePipelineMetadata metadata,
        uint props)
    {
        capturedDevice = device;
        capturedSpirvInfo = info;
        capturedComputeMetadata = metadata;
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureReflectBytecode(IntPtr bytecode, UIntPtr bytecodeSize, uint props)
    {
        capturedBytecode = bytecode;
        capturedBytecodeSize = bytecodeSize;
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureHlslInfoReturnPointerAndSize(in SDL3.ShaderCross.HLSLInfo info, out UIntPtr size)
    {
        capturedHlslInfo = info;
        size = nextSize;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCompileSPIRVFromHLSL(ref SDL3.ShaderCross.HLSLInfo info, out UIntPtr size)
    {
        capturedHlslInfo = info;
        size = nextSize;
        capturedCallCount++;
        return nextPointer;
    }

    private static SDL3.ShaderCross.SPIRVInfo CreateSpirvInfo()
    {
        return new SDL3.ShaderCross.SPIRVInfo
        {
            ByteCode = (IntPtr)0x501,
            ByteCodeSize = (UIntPtr)64,
            Entrypoint = (IntPtr)0x502,
            ShaderStage = SDL3.ShaderCross.ShaderStage.Compute,
            Props = 91
        };
    }

    private static IntPtr GetIOVarMetadataNamePointer(SDL3.ShaderCross.IOVarMetadata metadata)
    {
        FieldInfo? field = typeof(SDL3.ShaderCross.IOVarMetadata).GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);
        TestAssert.NotNull(field, "ShaderCross.IOVarMetadata private name field must exist.");
        return (IntPtr)field!.GetValue(metadata)!;
    }

    private static SDL3.ShaderCross.HLSLInfo CreateHlslInfo()
    {
        return new SDL3.ShaderCross.HLSLInfo
        {
            Source = (IntPtr)0x601,
            Entrypoint = (IntPtr)0x602,
            IncludeDir = (IntPtr)0x603,
            Defines = (IntPtr)0x604,
            ShaderStage = SDL3.ShaderCross.ShaderStage.Fragment,
            Props = 92
        };
    }

    private static SDL3.ShaderCross.GraphicsShaderResourceInfo CreateResourceInfo()
    {
        return new SDL3.ShaderCross.GraphicsShaderResourceInfo
        {
            NumSamplers = 2,
            NumStorageTextures = 3,
            NumStorageBuffers = 5,
            NumUniformBuffers = 7
        };
    }

    private static SDL3.ShaderCross.GraphicsShaderMetadata CreateMetadata()
    {
        return new SDL3.ShaderCross.GraphicsShaderMetadata
        {
            ResourceInfo = CreateResourceInfo(),
            NumInputs = 11,
            Inputs = (IntPtr)0x701,
            NumOutputs = 13,
            Outputs = (IntPtr)0x702
        };
    }

    private static SDL3.ShaderCross.ComputePipelineMetadata CreateComputeMetadata()
    {
        return new SDL3.ShaderCross.ComputePipelineMetadata
        {
            NumSamplers = 17,
            NumReadOnlyStorageTextures = 19,
            NumReadOnlyStorageBuffers = 23,
            NumReadWriteStorageTextures = 29,
            NumReadWriteStorageBuffers = 31,
            NumUniformBuffers = 37,
            ThreadCountX = 41,
            ThreadCountY = 43,
            ThreadCountZ = 47
        };
    }

    private static void AssertSpirvInfoEqual(
        SDL3.ShaderCross.SPIRVInfo expected,
        SDL3.ShaderCross.SPIRVInfo actual,
        string message)
    {
        TestAssert.Equal(expected.ByteCode, actual.ByteCode, $"{message} ByteCode mismatch.");
        TestAssert.Equal(expected.ByteCodeSize, actual.ByteCodeSize, $"{message} ByteCodeSize mismatch.");
        TestAssert.Equal(expected.Entrypoint, actual.Entrypoint, $"{message} Entrypoint mismatch.");
        TestAssert.Equal(expected.ShaderStage, actual.ShaderStage, $"{message} ShaderStage mismatch.");
        TestAssert.Equal(expected.Props, actual.Props, $"{message} Props mismatch.");
    }

    private static void AssertHlslInfoEqual(
        SDL3.ShaderCross.HLSLInfo expected,
        SDL3.ShaderCross.HLSLInfo actual,
        string message)
    {
        TestAssert.Equal(expected.Source, actual.Source, $"{message} Source mismatch.");
        TestAssert.Equal(expected.Entrypoint, actual.Entrypoint, $"{message} Entrypoint mismatch.");
        TestAssert.Equal(expected.IncludeDir, actual.IncludeDir, $"{message} IncludeDir mismatch.");
        TestAssert.Equal(expected.Defines, actual.Defines, $"{message} Defines mismatch.");
        TestAssert.Equal(expected.ShaderStage, actual.ShaderStage, $"{message} ShaderStage mismatch.");
        TestAssert.Equal(expected.Props, actual.Props, $"{message} Props mismatch.");
    }

    private static void AssertResourceInfoEqual(
        SDL3.ShaderCross.GraphicsShaderResourceInfo expected,
        SDL3.ShaderCross.GraphicsShaderResourceInfo actual,
        string message)
    {
        TestAssert.Equal(expected.NumSamplers, actual.NumSamplers, $"{message} NumSamplers mismatch.");
        TestAssert.Equal(expected.NumStorageTextures, actual.NumStorageTextures, $"{message} NumStorageTextures mismatch.");
        TestAssert.Equal(expected.NumStorageBuffers, actual.NumStorageBuffers, $"{message} NumStorageBuffers mismatch.");
        TestAssert.Equal(expected.NumUniformBuffers, actual.NumUniformBuffers, $"{message} NumUniformBuffers mismatch.");
    }

    private static void AssertMetadataEqual(
        SDL3.ShaderCross.GraphicsShaderMetadata expected,
        SDL3.ShaderCross.GraphicsShaderMetadata actual,
        string message)
    {
        AssertResourceInfoEqual(expected.ResourceInfo, actual.ResourceInfo, $"{message} ResourceInfo mismatch.");
        TestAssert.Equal(expected.NumInputs, actual.NumInputs, $"{message} NumInputs mismatch.");
        TestAssert.Equal(expected.Inputs, actual.Inputs, $"{message} Inputs mismatch.");
        TestAssert.Equal(expected.NumOutputs, actual.NumOutputs, $"{message} NumOutputs mismatch.");
        TestAssert.Equal(expected.Outputs, actual.Outputs, $"{message} Outputs mismatch.");
    }

    private static void AssertComputeMetadataEqual(
        SDL3.ShaderCross.ComputePipelineMetadata expected,
        SDL3.ShaderCross.ComputePipelineMetadata actual,
        string message)
    {
        TestAssert.Equal(expected.NumSamplers, actual.NumSamplers, $"{message} NumSamplers mismatch.");
        TestAssert.Equal(expected.NumReadOnlyStorageTextures, actual.NumReadOnlyStorageTextures, $"{message} NumReadOnlyStorageTextures mismatch.");
        TestAssert.Equal(expected.NumReadOnlyStorageBuffers, actual.NumReadOnlyStorageBuffers, $"{message} NumReadOnlyStorageBuffers mismatch.");
        TestAssert.Equal(expected.NumReadWriteStorageTextures, actual.NumReadWriteStorageTextures, $"{message} NumReadWriteStorageTextures mismatch.");
        TestAssert.Equal(expected.NumReadWriteStorageBuffers, actual.NumReadWriteStorageBuffers, $"{message} NumReadWriteStorageBuffers mismatch.");
        TestAssert.Equal(expected.NumUniformBuffers, actual.NumUniformBuffers, $"{message} NumUniformBuffers mismatch.");
        TestAssert.Equal(expected.ThreadCountX, actual.ThreadCountX, $"{message} ThreadCountX mismatch.");
        TestAssert.Equal(expected.ThreadCountY, actual.ThreadCountY, $"{message} ThreadCountY mismatch.");
        TestAssert.Equal(expected.ThreadCountZ, actual.ThreadCountZ, $"{message} ThreadCountZ mismatch.");
    }

    private static void ResetCaptureState()
    {
        nextShaderFormat = SDL3.SDL.GPUShaderFormat.Invalid;
        nextPointer = IntPtr.Zero;
        nextSize = UIntPtr.Zero;
        capturedDevice = IntPtr.Zero;
        capturedBytecode = IntPtr.Zero;
        capturedBytecodeSize = UIntPtr.Zero;
        capturedProps = 0;
        nextBool = false;
        capturedCallCount = 0;
        capturedSpirvInfo = default;
        capturedHlslInfo = default;
        capturedResourceInfo = default;
        capturedComputeMetadata = default;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.ShaderCross).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"ShaderCross.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertNativeImport(MethodInfo method, string entryPoint)
    {
        AssertShaderCrossLibraryImport(method, entryPoint);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertNativeBoolImport(MethodInfo method, string entryPoint)
    {
        AssertShaderCrossLibraryImport(method, entryPoint);
        AssertBoolReturnMarshal(method);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertShaderCrossLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"ShaderCross.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_shadercross", libraryImport!.LibraryName, $"ShaderCross.{method.Name} must import from SDL3_shadercross.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"ShaderCross.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"ShaderCross.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"ShaderCross.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertParameterTypes(MethodInfo method, params Type[] expectedTypes)
    {
        ParameterInfo[] parameters = method.GetParameters();
        TestAssert.Equal(expectedTypes.Length, parameters.Length, $"ShaderCross.{method.Name} must keep expected parameter count.");

        for (int i = 0; i < expectedTypes.Length; i++)
        {
            TestAssert.Equal(expectedTypes[i], parameters[i].ParameterType, $"ShaderCross.{method.Name} parameter {i} must keep expected type.");
        }
    }

    private static void AssertOutParameter(MethodInfo method, int index)
    {
        ParameterInfo parameter = method.GetParameters()[index];
        TestAssert.True(parameter.IsOut, $"ShaderCross.{method.Name} parameter {index} must remain an out parameter.");
    }

    private static void AssertExcludedFromCoverage(MethodInfo method)
    {
        ExcludeFromCodeCoverageAttribute? attribute = method.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
        TestAssert.NotNull(attribute, $"ShaderCross.{method.Name} native stub must be excluded from code coverage.");
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
            FieldInfo? field = typeof(SDL3.ShaderCross).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"ShaderCross private hook field {fieldName} must exist.");

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
