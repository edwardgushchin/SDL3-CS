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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public partial class ShaderCross
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShaderCross_Init(void);</code>
    /// <summary>
    /// Initializes SDL_shadercross
    /// </summary>
    /// <returns>true on success, false otherwise.</returns>
    /// <threadsafety>This should only be called once, from a single thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool Init();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ShaderCross_Quit(void);</code>
    /// <summary>
    /// De-initializes SDL_shadercross
    /// </summary>
    /// <threadsafety>This should only be called once, from a single thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUShaderFormat SDLCALL SDL_ShaderCross_GetSPIRVShaderFormats(void);</code>
    /// <summary>
    /// Get the supported shader formats that SPIRV cross-compilation can output
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <returns>GPU shader formats supported by SPIRV cross-compilation.</returns>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_GetSPIRVShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL.GPUShaderFormat GetSPIRVShaderFormats();
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_TranspileMSLFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info);</code>
    /// <summary>
    /// <para>Transpile to MSL code from SPIRV code.</para>
    /// </summary>
    /// <remarks>You must SDL_free the returned string once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <returns>an SDL_malloc'd string containing MSL code.</returns>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_TranspileMSLFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr TranspileMSLFromSPIRV(in SPIRVInfo info);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_TranspileHLSLFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info);</code>
    /// <summary>
    /// <para>Transpile to HLSL code from SPIRV code.</para>
    /// </summary>
    /// <remarks>You must SDL_free the returned string once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <returns>an SDL_malloc'd string containing HLSL code.</returns>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_TranspileHLSLFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr TranspileHLSLFromSPIRV(in SPIRVInfo info);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXBCFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile DXBC bytecode from SPIRV code.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXBC bytecode.</returns>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXBCFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileDXBCFromSPIRV(in SPIRVInfo info, out UIntPtr size);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXILFromSPIRV(const SDL_ShaderCross_SPIRV_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile DXIL bytecode from SPIRV code.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXIL bytecode.</returns>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXILFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileDXILFromSPIRV(in SPIRVInfo info, out UIntPtr size);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUShader * SDLCALL SDL_ShaderCross_CompileGraphicsShaderFromSPIRV(SDL_GPUDevice *device, const SDL_ShaderCross_SPIRV_Info *info, const SDL_ShaderCross_GraphicsShaderMetadata *metadata, SDL_PropertiesID props);</code>
    /// <summary>
    /// Compile an SDL GPU shader from SPIRV code. If your shader source is HLSL, you should obtain SPIR-V bytecode from <see cref="CompileSPIRVFromHLSL"/>.
    /// </summary>
    /// <param name="device">the SDL GPU device.</param>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="metadata">a struct describing shader metadata. Can be obtained from <see cref="ReflectGraphicsSPIRV"/>.</param>
    /// <param name="props">a properties object filled in with extra shader metadata.</param>
    /// <returns>a compiled SDL_GPUShader</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileGraphicsShaderFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileGraphicsShaderFromSPIRV(IntPtr device, in SPIRVInfo info, in GraphicsShaderMetadata metadata, uint props);
    
    
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
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileComputePipelineFromSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileComputePipelineFromSPIRV(IntPtr device, in SPIRVInfo info, in GraphicsShaderMetadata metadata, uint props);
    
    
    /// <code>extern SDL_DECLSPEC SDL_ShaderCross_GraphicsShaderMetadata * SDLCALL SDL_ShaderCross_ReflectGraphicsSPIRV(const Uint8 *bytecode, size_t bytecode_size, SDL_PropertiesID props);</code>
    /// <summary>
    /// Reflect graphics shader info from SPIRV code. If your shader source is HLSL, you should obtain SPIR-V bytecode from <see cref="CompileSPIRVFromHLSL"/>. This must be freed with <see cref="SDL.Free"/> when you are done with the metadata.
    /// </summary>
    /// <param name="bytecode">the SPIRV bytecode.</param>
    /// <param name="bytecodeSize">the length of the SPIRV bytecode.</param>
    /// <param name="props">a properties object filled in with extra shader metadata, provided by the user.</param>
    /// <returns>A metadata struct on success, NULL otherwise. The struct must be free'd when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_ReflectGraphicsSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ReflectGraphicsSPIRV(IntPtr bytecode, UIntPtr bytecodeSize, uint props);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShaderCross_ReflectComputeSPIRV(const Uint8 *bytecode, size_t bytecode_size, SDL_ShaderCross_ComputePipelineMetadata *metadata);</code>
    /// <summary>
    /// Reflect compute pipeline info from SPIRV code.
    /// </summary>
    /// <param name="bytecode">the SPIRV bytecode.</param>
    /// <param name="bytecodeSize">the length of the SPIRV bytecode.</param>
    /// <param name="metadata">a pointer filled in with compute pipeline metadata.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_ReflectComputeSPIRV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ReflectComputeSPIRV(IntPtr bytecode, UIntPtr bytecodeSize, out GraphicsShaderMetadata metadata);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUShaderFormat SDLCALL SDL_ShaderCross_GetHLSLShaderFormats(void);</code>
    /// <summary>
    /// Get the supported shader formats that HLSL cross-compilation can output
    /// </summary>
    /// <returns>GPU shader formats supported by HLSL cross-compilation.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_GetHLSLShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL.GPUShaderFormat GetHLSLShaderFormats();
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXBCFromHLSL(const SDL_ShaderCross_HLSL_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile to DXBC bytecode from HLSL code via a SPIRV-Cross round trip.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXBC bytecode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXBCFromHLSL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileDXBCFromHLSL(in HLSLInfo info, out UIntPtr size);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileDXILFromHLSL(const SDL_ShaderCross_HLSL_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile to DXIL bytecode from HLSL code via a SPIRV-Cross round trip.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing DXIL bytecode.</returns>
    /// <threadsafety> It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileDXILFromHLSL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileDXILFromHLSL(in HLSLInfo info, out UIntPtr size);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ShaderCross_CompileSPIRVFromHLSL(const SDL_ShaderCross_HLSL_Info *info, size_t *size);</code>
    /// <summary>
    /// Compile to SPIRV bytecode from HLSL code.
    /// </summary>
    /// <remarks>You must SDL_free the returned buffer once you are done with it.</remarks>
    /// <param name="info">a struct describing the shader to transpile.</param>
    /// <param name="size">filled in with the bytecode buffer size.</param>
    /// <returns>an SDL_malloc'd buffer containing SPIRV bytecode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    [LibraryImport(ShaderCrossLibrary, EntryPoint = "SDL_ShaderCross_CompileSPIRVFromHLSL"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CompileSPIRVFromHLSL(in HLSLInfo info, out UIntPtr size);
}