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

public partial class ShaderCross
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ShaderCross_Init();
    private delegate bool InitNativeDelegate();
    private static InitNativeDelegate InitNativeFunction = SDL_ShaderCross_Init;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShaderCross_Init(void);</code>
    /// <summary>
    /// Initializes SDL_shadercross
    /// </summary>
    /// <returns><c>true</c> on success, <c>false</c> otherwise.</returns>
    /// <threadsafety>This should only be called once, from a single thread.</threadsafety>
    public static bool Init()
    {
        return InitNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ShaderCross_Quit();
    private delegate void QuitNativeDelegate();
    private static QuitNativeDelegate QuitNativeFunction = SDL_ShaderCross_Quit;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShaderCross_Quit(void);</code>
    /// <summary>
    /// De-initializes SDL_shadercross
    /// </summary>
    /// <threadsafety>This should only be called once, from a single thread.</threadsafety>
    public static void Quit()
    {
        QuitNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_GetSPIRVShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SDL.GPUShaderFormat SDL_ShaderCross_GetSPIRVShaderFormats();
    private delegate SDL.GPUShaderFormat GetSPIRVShaderFormatsNativeDelegate();
    private static GetSPIRVShaderFormatsNativeDelegate GetSPIRVShaderFormatsNativeFunction = SDL_ShaderCross_GetSPIRVShaderFormats;

    /// <code>extern SDL_DECLSPEC SDL_GPUShaderFormat SDLCALL SDL_ShaderCross_GetSPIRVShaderFormats(void);</code>
    /// <summary>
    /// Get the supported shader formats that SPIRV cross-compilation can output
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <returns>GPU shader formats supported by SPIRV cross-compilation.</returns>
    public static SDL.GPUShaderFormat GetSPIRVShaderFormats()
    {
        return GetSPIRVShaderFormatsNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_TranspileMSLFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_TranspileMSLFromSPIRV(in SPIRVInfo info);
    private delegate IntPtr TranspileMSLFromSPIRVNativeDelegate(in SPIRVInfo info);
    private static TranspileMSLFromSPIRVNativeDelegate TranspileMSLFromSPIRVNativeFunction = SDL_ShaderCross_TranspileMSLFromSPIRV;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_TranspileMSLFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info);</code>
    /// <summary>
    /// <para>Transpile to MSL code from SPIRV code.</para>
    /// You must <see cref="SDL.Free"/> the returned string once you are done with it.
    /// <para>These are the optional properties that can be used:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.SPIRVMSLVersionString"/>: specifies the MSL version that should be emitted. Defaults to 1.2.0.</item>
    /// </list>
    /// </summary>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <returns>an SDL_malloc'd string containing MSL code.</returns>
    public static IntPtr TranspileMSLFromSPIRV(in SPIRVInfo info)
    {
        return TranspileMSLFromSPIRVNativeFunction(in info);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_TranspileHLSLFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_TranspileHLSLFromSPIRV(in SPIRVInfo info);
    private delegate IntPtr TranspileHLSLFromSPIRVNativeDelegate(in SPIRVInfo info);
    private static TranspileHLSLFromSPIRVNativeDelegate TranspileHLSLFromSPIRVNativeFunction = SDL_ShaderCross_TranspileHLSLFromSPIRV;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_TranspileHLSLFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info);</code>
    /// <summary>
    /// <para>Transpile to HLSL code from SPIRV code.</para>
    /// You must SDL_free the returned string once you are done with it.
    /// <para>These are the optional properties that can be used:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.SPIRVPSSLCompatibilityBoolean"/>: generates PSSL-compatible shader.</item>
    /// </list>
    /// </summary>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <returns>an SDL_malloc'd string containing HLSL code.</returns>
    public static IntPtr TranspileHLSLFromSPIRV(in SPIRVInfo info)
    {
        return TranspileHLSLFromSPIRVNativeFunction(in info);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXBCFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileDXBCFromSPIRV(in SPIRVInfo info, out UIntPtr size);
    private delegate IntPtr CompileDXBCFromSPIRVNativeDelegate(in SPIRVInfo info, out UIntPtr size);
    private static CompileDXBCFromSPIRVNativeDelegate CompileDXBCFromSPIRVNativeFunction = SDL_ShaderCross_CompileDXBCFromSPIRV;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXBCFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile DXBC bytecode from SPIRV code.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXBC bytecode.</returns>
    public static IntPtr CompileDXBCFromSPIRV(in SPIRVInfo info, out UIntPtr size)
    {
        return CompileDXBCFromSPIRVNativeFunction(in info, out size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXILFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileDXILFromSPIRV(in SPIRVInfo info, out UIntPtr size);
    private delegate IntPtr CompileDXILFromSPIRVNativeDelegate(in SPIRVInfo info, out UIntPtr size);
    private static CompileDXILFromSPIRVNativeDelegate CompileDXILFromSPIRVNativeFunction = SDL_ShaderCross_CompileDXILFromSPIRV;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXILFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile DXIL bytecode from SPIRV code.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXIL bytecode.</returns>
    public static IntPtr CompileDXILFromSPIRV(in SPIRVInfo info, out UIntPtr size)
    {
        return CompileDXILFromSPIRVNativeFunction(in info, out size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileGraphicsShaderFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileGraphicsShaderFromSPIRV(IntPtr device, ref SPIRVInfo info, ref GraphicsShaderResourceInfo resourceInfo, uint props);
    private delegate IntPtr CompileGraphicsShaderFromSPIRVNativeDelegate(IntPtr device, ref SPIRVInfo info, ref GraphicsShaderResourceInfo resourceInfo, uint props);
    private static CompileGraphicsShaderFromSPIRVNativeDelegate CompileGraphicsShaderFromSPIRVNativeFunction = SDL_ShaderCross_CompileGraphicsShaderFromSPIRV;

    /// <code>extern SDL_DECLSPEC SDL_GPUShader * SDLCALL SDL_ShaderCross_CompileGraphicsShaderFromSPIRV(SDL_GPUDevice *device, const SDL_ShaderCross_SPIRV_Info *info, const SDL_ShaderCross_GraphicsShaderMetadata *metadata, SDL_PropertiesID props);</code>
    /// <summary>
    /// Compile an SDL GPU shader from SPIRV code. If your shader source is HLSL, you should obtain SPIR-V bytecode from <see cref="CompileSPIRVFromHLSL"/>.
    /// </summary>
    /// <param name="device">the SDL GPU device.</param>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="resourceInfo">a struct describing resource info of the shader. Can be obtained from <see cref="ReflectGraphicsSPIRV"/>.</param>
    /// <param name="props">a properties object filled in with extra shader metadata.</param>
    /// <returns>a compiled SDL_GPUShader</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr CompileGraphicsShaderFromSPIRV(IntPtr device, ref SPIRVInfo info, ref GraphicsShaderResourceInfo resourceInfo, uint props)
    {
        return CompileGraphicsShaderFromSPIRVNativeFunction(device, ref info, ref resourceInfo, props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileComputePipelineFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileComputePipelineFromSPIRV(IntPtr device, in SPIRVInfo info, in ComputePipelineMetadata metadata, uint props);
    private delegate IntPtr CompileComputePipelineFromSPIRVNativeDelegate(IntPtr device, in SPIRVInfo info, in ComputePipelineMetadata metadata, uint props);
    private static CompileComputePipelineFromSPIRVNativeDelegate CompileComputePipelineFromSPIRVNativeFunction = SDL_ShaderCross_CompileComputePipelineFromSPIRV;

    /// <code>extern SDL_DECLSPEC SDL_GPUComputePipeline * SDLCALL SDL_ShaderCross_CompileComputePipelineFromSPIRV(SDL_GPUDevice *device, const SDL_ShaderCross_SPIRV_Info *info, const SDL_ShaderCross_ComputePipelineMetadata *metadata, SDL_PropertiesID props);</code>
    /// <summary>
    /// Compile an SDL GPU compute pipeline from SPIRV code. If your shader source is HLSL, you should obtain SPIR-V bytecode from <see cref="CompileSPIRVFromHLSL"/>.
    /// </summary>
    /// <param name="device">the SDL GPU device.</param>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="metadata">a struct describing shader metadata. Can be obtained from <see cref="ReflectComputeSPIRV"/>.</param>
    /// <param name="props">a properties object filled in with extra shader metadata.</param>
    /// <returns>a compiled SDL_GPUComputePipeline.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr CompileComputePipelineFromSPIRV(IntPtr device, in SPIRVInfo info, in ComputePipelineMetadata metadata, uint props)
    {
        return CompileComputePipelineFromSPIRVNativeFunction(device, in info, in metadata, props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_ReflectGraphicsSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_ReflectGraphicsSPIRV(IntPtr bytecode, UIntPtr bytecodeSize, uint props);
    private delegate IntPtr ReflectGraphicsSPIRVNativeDelegate(IntPtr bytecode, UIntPtr bytecodeSize, uint props);
    private static ReflectGraphicsSPIRVNativeDelegate ReflectGraphicsSPIRVNativeFunction = SDL_ShaderCross_ReflectGraphicsSPIRV;

    /// <code>extern SDL_DECLSPEC SDL_ShaderCross_GraphicsShaderMetadata * SDLCALL SDL_ShaderCross_ReflectGraphicsSPIRV(const Uint8 *bytecode, size_t bytecode_size, SDL_PropertiesID props);</code>
    /// <summary>
    /// Reflect graphics shader info from SPIRV code. If your shader source is HLSL, you should obtain SPIR-V bytecode from <see cref="CompileSPIRVFromHLSL"/>. This must be freed with <see cref="SDL.Free"/> when you are done with the metadata.
    /// </summary>
    /// <param name="bytecode">the SPIRV bytecode.</param>
    /// <param name="bytecodeSize">the length of the SPIRV bytecode.</param>
    /// <param name="props">a properties object filled in with extra shader metadata, provided by the user.</param>
    /// <returns>A metadata struct on success, <c>null</c> otherwise. The struct must be free'd when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr ReflectGraphicsSPIRV(IntPtr bytecode, UIntPtr bytecodeSize, uint props)
    {
        return ReflectGraphicsSPIRVNativeFunction(bytecode, bytecodeSize, props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_ReflectComputeSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_ReflectComputeSPIRV(IntPtr bytecode, UIntPtr bytecodeSize, uint props);
    private delegate IntPtr ReflectComputeSPIRVNativeDelegate(IntPtr bytecode, UIntPtr bytecodeSize, uint props);
    private static ReflectComputeSPIRVNativeDelegate ReflectComputeSPIRVNativeFunction = SDL_ShaderCross_ReflectComputeSPIRV;

    /// <code>extern SDL_DECLSPEC SDL_ShaderCross_ComputePipelineMetadata * SDLCALL SDL_ShaderCross_ReflectComputeSPIRV(const Uint8 *bytecode, size_t bytecode_size, SDL_PropertiesID props);</code>
    /// <summary>
    /// Reflect compute pipeline info from SPIRV code.
    /// </summary>
    /// <param name="bytecode">the SPIRV bytecode.</param>
    /// <param name="bytecodeSize">the length of the SPIRV bytecode.</param>
    /// <param name="props">a properties object filled in with extra shader metadata, provided by the user.</param>
    /// <returns>A metadata struct on success, <c>null</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr ReflectComputeSPIRV(IntPtr bytecode, UIntPtr bytecodeSize, uint props)
    {
        return ReflectComputeSPIRVNativeFunction(bytecode, bytecodeSize, props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_GetHLSLShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SDL.GPUShaderFormat SDL_ShaderCross_GetHLSLShaderFormats();
    private delegate SDL.GPUShaderFormat GetHLSLShaderFormatsNativeDelegate();
    private static GetHLSLShaderFormatsNativeDelegate GetHLSLShaderFormatsNativeFunction = SDL_ShaderCross_GetHLSLShaderFormats;

    /// <code>extern SDL_DECLSPEC SDL_GPUShaderFormat SDLCALL SDL_ShaderCross_GetHLSLShaderFormats(void);</code>
    /// <summary>
    /// Get the supported shader formats that HLSL cross-compilation can output
    /// </summary>
    /// <returns>GPU shader formats supported by HLSL cross-compilation.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static SDL.GPUShaderFormat GetHLSLShaderFormats()
    {
        return GetHLSLShaderFormatsNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXBCFromHLSL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileDXBCFromHLSL(in HLSLInfo info, out UIntPtr size);
    private delegate IntPtr CompileDXBCFromHLSLNativeDelegate(in HLSLInfo info, out UIntPtr size);
    private static CompileDXBCFromHLSLNativeDelegate CompileDXBCFromHLSLNativeFunction = SDL_ShaderCross_CompileDXBCFromHLSL;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXBCFromHLSL(const SDL_ShaderCross_HLSL_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile to DXBC bytecode from HLSL code via a SPIRV-Cross round trip.
    /// <para>You must <see cref="SDL.Free"/> the returned buffer once you are done with it.</para>
    /// <para>These are the optional properties that can be used:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.ShaderDebugEnableBoolean"/>: allows debug info to be emitted when relevant. Should only be used with debugging tools like Renderdoc.</item>
    /// <item><see cref="Props.ShaderDebugNameString"/>: a UTF-8 name to be used with the shader. Relevant for use with debugging tools like Renderdoc.</item>
    /// <item><see cref="Props.ShaderCullUnusedBindingsBoolean"/>: When <c>true</c>, indicates that the compiler should cull unused shader resources. This behavior is disabled by default.</item>
    /// <item><see cref="Props.HLSLSkipSPIRVRoundtripBoolean"/>: When <c>true</c>, the SPIRV roundtrip is skipped. This behavior is disabled by default. Do not use this property if your shader uses Structured Buffers.</item>
    /// </list>
    /// </summary>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXBC bytecode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr CompileDXBCFromHLSL(in HLSLInfo info, out UIntPtr size)
    {
        return CompileDXBCFromHLSLNativeFunction(in info, out size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXILFromHLSL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileDXILFromHLSL(in HLSLInfo info, out UIntPtr size);
    private delegate IntPtr CompileDXILFromHLSLNativeDelegate(in HLSLInfo info, out UIntPtr size);
    private static CompileDXILFromHLSLNativeDelegate CompileDXILFromHLSLNativeFunction = SDL_ShaderCross_CompileDXILFromHLSL;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXILFromHLSL(const SDL_ShaderCross_HLSL_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile to DXIL bytecode from HLSL code via a SPIRV-Cross round trip.
    /// <para>You must <see cref="SDL.Free"/> the returned buffer once you are done with it.</para>
    /// <para>These are the optional properties that can be used:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.ShaderDebugEnableBoolean"/>: allows debug info to be emitted when relevant. Should only be used with debugging tools like Renderdoc.</item>
    /// <item><see cref="Props.ShaderDebugNameString"/>: a UTF-8 name to be used with the shader. Relevant for use with debugging tools like Renderdoc.</item>
    /// <item><see cref="Props.ShaderCullUnusedBindingsBoolean"/>: when <c>true</c>, indicates that the compiler should cull unused shader resources. This behavior is disabled by default.</item>
    /// <item><see cref="Props.HLSLSkipSPIRVRoundtripBoolean"/>: when <c>true</c>, the SPIRV roundtrip is skipped. This behavior is disabled by default. Do not use this property if your shader uses Structured Buffers.</item>
    /// </list>
    /// </summary>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXIL bytecode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr CompileDXILFromHLSL(in HLSLInfo info, out UIntPtr size)
    {
        return CompileDXILFromHLSLNativeFunction(in info, out size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileSPIRVFromHLSL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_ShaderCross_CompileSPIRVFromHLSL(ref HLSLInfo info, out UIntPtr size);
    private delegate IntPtr CompileSPIRVFromHLSLNativeDelegate(ref HLSLInfo info, out UIntPtr size);
    private static CompileSPIRVFromHLSLNativeDelegate CompileSPIRVFromHLSLNativeFunction = SDL_ShaderCross_CompileSPIRVFromHLSL;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileSPIRVFromHLSL(const SDL_ShaderCross_HLSL_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile to SPIRV bytecode from HLSL code.
    /// <para>You must <see cref="SDL.Free"/> the returned buffer once you are done with it.</para>
    /// <para>These are the optional properties that can be used:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.ShaderDebugEnableBoolean"/>: allows debug info to be emitted when relevant. Should only be used with debugging tools like Renderdoc.</item>
    /// <item><see cref="Props.ShaderDebugNameString"/>: a UTF-8 name to be used with the shader. Relevant for use with debugging tools like Renderdoc.</item>
    /// <item><see cref="Props.ShaderCullUnusedBindingsBoolean"/>: when <c>true</c>, indicates that the compiler should cull unused shader resources. This behavior is disabled by default.</item>
    /// </list>
    /// </summary>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing SPIRV bytecode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    public static IntPtr CompileSPIRVFromHLSL(ref HLSLInfo info, out UIntPtr size)
    {
        return CompileSPIRVFromHLSLNativeFunction(ref info, out size);
    }
}
