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

public partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GPUSupportsShaderFormats(SDL_GPUShaderFormat format_flags, const char *name);</code>
    /// <summary>
    /// Checks for GPU runtime support.
    /// </summary>
    /// <param name="formatFlags">a bitflag indicating which shader formats the app is
    /// able to provide.</param>
    /// <param name="name">the preferred GPU driver, or <c>null</c> to let SDL pick the optimal
    /// driver.</param>
    /// <returns><c>true</c> if supported, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUSupportsShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GPUSupportsShaderFormats(GPUShaderFormat formatFlags, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GPUSupportsProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// Checks for GPU runtime support.
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns><c>true</c> if supported, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUSupportsProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GPUSupportsProperties(uint props);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUDevice *SDLCALL SDL_CreateGPUDevice(SDL_GPUShaderFormat format_flags, bool debug_mode, const char *name);</code>
    /// <summary>
    /// Creates a GPU context.
    /// <para>The GPU driver name can be one of the following:</para>
    /// <list type="bullet">
    /// <item><c>vulkan</c>: [Vulkan](CategoryGPU#vulkan)</item>
    /// <item><c>direct3d12</c>: [D3D12](CategoryGPU#d3d12)</item>
    /// <item><c>metal</c>: [Metal](CategoryGPU#metal)</item>
    /// <item><c>NULL</c>: let SDL pick the optimal driver</item>
    /// </list>
    /// </summary>
    /// <param name="formatFlags">a bitflag indicating which shader formats the app is
    /// able to provide.</param>
    /// <param name="debugMode">enable debug mode properties and validations.</param>
    /// <param name="name">the preferred GPU driver, or <c>null</c> to let SDL pick the optimal
    /// driver.</param>
    /// <returns>a GPU context on success or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    /// <seealso cref="GetGPUShaderFormats"/>
    /// <seealso cref="GetGPUDeviceDriver"/>
    /// <seealso cref="DestroyGPUDevice"/>
    /// <seealso cref="GPUSupportsShaderFormats"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUDevice(GPUShaderFormat formatFlags, [MarshalAs(UnmanagedType.I1)] bool debugMode, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUDevice *SDLCALL SDL_CreateGPUDeviceWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Creates a GPU context.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateDebugModeBoolean"/>: enable debug mode
    /// properties and validations, defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreatePreferLowPowerBoolean"/>: enable to prefer
    /// energy efficiency over maximum GPU performance, defaults to false.</item>
    /// <item><see cref="Props.GPUDeviceCreateVerboseBoolean"/>: enable to automatically log
    /// useful debug information on device creation, defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateNameString"/>: the name of the GPU driver to
    /// use, if a specific one is desired.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureClipDistanceBoolean"/>: Enable Vulkan
    /// device feature shaderClipDistance. If disabled, clip distances are not
    /// supported in shader code: gl_ClipDistance[] built-ins of GLSL,
    /// SV_ClipDistance0/1 semantics of HLSL and [[clip_distance]] attribute of
    /// Metal. Disabling optional features allows the application to run on some
    /// older Android devices. Defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureDepthClampingBoolean"/>: Enable
    /// Vulkan device feature depthClamp. If disabled, there is no depth clamp
    /// support and enable_depth_clip in <see cref="GPURasterizerState"/> must always be
    /// set to true. Disabling optional features allows the application to run on
    /// some older Android devices. Defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureIndirectDrawFirstInstanceBoolean"/>:
    /// Enable Vulkan device feature drawIndirectFirstInstance. If disabled, the
    /// argument first_instance of <seealso cref="GPUIndirectDrawCommand"/> must be set to
    /// zero. Disabling optional features allows the application to run on some
    /// older Android devices. Defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureAnisotropyBoolean"/>: Enable Vulkan
    /// device feature samplerAnisotropy. If disabled, enable_anisotropy of
    /// <see cref="GPUSamplerCreateInfo"/> must be set to false. Disabling optional
    /// features allows the application to run on some older Android devices.
    /// Defaults to true.</item>
    /// </list>
    /// <para>These are the current shader format properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateShadersPrivateBoolean"/>: The app is able to
    /// provide shaders for an NDA platform.</item>
    /// <item><see cref="Props.GPUDeviceCreateShadersSPIRVBoolean"/>: The app is able to
    /// provide SPIR-V shaders if applicable.</item>
    /// <item><see cref="Props.GPUDeviceCreateShadersDXBCBoolean"/>: The app is able to
    /// provide DXBC shaders if applicable
    /// <see cref="Props.GPUDeviceCreateShadersDXILBoolean"/>: The app is able to
    /// provide DXIL shaders if applicable.</item>
    /// <item><see cref="Props.GPUDeviceCreateShadersMSLBoolean"/>: The app is able to
    /// provide MSL shaders if applicable.</item>
    /// <item><see cref="Props.GPUDeviceCreateShadersMetalLibBoolean"/>: The app is able to
    /// provide Metal shader libraries if applicable.</item>
    /// </list>
    /// <para>With the D3D12 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateD3D12SemanticNameString"/>: the prefix to
    /// use for all vertex semantics, default is "TEXCOORD".</item>
    /// </list>
    /// <para>With the Vulkan renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateVulkanShaderClipDistanceBoolean"/>: Enable
    /// device feature shaderClipDistance. If disabled, clip distances are not
    /// supported in shader code: gl_ClipDistance[] built-ins of GLSL,
    /// SV_ClipDistance0/1 semantics of HLSL and [[clip_distance]] attribute of
    /// Metal. Defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateVulkanDepthClampBoolean"/>: Enable device
    /// feature depthClamp. If disabled, there is no depth clamp support and
    /// enable_depth_clip in <see cref="GPURasterizerState"/> must always be set to true.
    /// Defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateVulkanDrawInDirectFirstBoolean"/>: Enable device
    /// feature drawIndirectFirstInstance. If disabled, the argument
    /// first_instance of <see cref="GPUIndirectDrawCommand"/> must be set to zero.
    /// Defaults to true.</item>
    /// <item><see cref="Props.GPUDeviceCreateVulkanSamplerAnisotropyBoolean"/>: Enable device
    /// feature samplerAnisotropy. If disabled, enable_anisotropy of 
    /// <see cref="GPUSamplerCreateInfo"/> must be set to false. Defaults to true.</item>
    /// </list>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>a GPU context on success or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGPUShaderFormats"/>
    /// <seealso cref="GetGPUDeviceDriver"/>
    /// <seealso cref="DestroyGPUDevice"/>
    /// <seealso cref="GPUSupportsProperties"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUDeviceWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUDeviceWithProperties(uint props);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyGPUDevice(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Destroys a GPU context previously returned by SDL_CreateGPUDevice.
    /// </summary>
    /// <param name="device">a GPU Context to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyGPUDevice(IntPtr device);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumGPUDrivers(void);</code>
    /// <summary>
    /// Get the number of GPU drivers compiled into SDL.
    /// </summary>
    /// <returns>the number of built in GPU drivers.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGPUDriver"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumGPUDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumGPUDrivers();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGPUDriver(int index);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGPUDriver(int index);</code>
    /// <summary>
    /// <para>Get the name of a built in GPU driver.</para>
    /// <para>The GPU drivers are presented in the order in which they are normally
    /// checked during initialization.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "vulkan",
    /// "metal" or "direct3d12". These never have Unicode characters, and are not
    /// meant to be proper names.</para>
    /// </summary>
    /// <param name="index">the index of a GPU driver.</param>
    /// <returns>the name of the GPU driver with the given <b>index</b>.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumGPUDrivers"/>
    public static string GetGPUDriver(int index)
    {
        var value = SDL_GetGPUDriver(index); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUDeviceDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGPUDeviceDriver(IntPtr device);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGPUDeviceDriver(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Returns the name of the backend used to create this GPU context.
    /// </summary>
    /// <param name="device">a GPU context to query.</param>
    /// <returns>the name of the device's driver, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetGPUDeviceDriver(IntPtr device)
    {
        var value = SDL_GetGPUDeviceDriver(device); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUShaderFormat SDLCALL SDL_GetGPUShaderFormats(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Returns the supported shader formats for this GPU context.
    /// </summary>
    /// <param name="device">a GPU context to query.</param>
    /// <returns>a bitflag indicating which shader formats the driver is able to
    /// consume.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GPUShaderFormat GetGPUShaderFormats(IntPtr device);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetGPUDeviceProperties(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Get the properties associated with a GPU device.</para>
    /// <para>All properties are optional and may differ between GPU backends and SDL
    /// versions.</para>
    /// <para>The following properties are provided by SDL:</para>
    /// <para><see cref="Props.GPUDeviceNameString"/>: Contains the name of the underlying
    /// device as reported by the system driver. This string has no standardized
    /// format, is highly inconsistent between hardware devices and drivers, and is
    /// able to change at any time. Do not attempt to parse this string as it is
    /// bound to fail at some point in the future when system drivers are updated,
    /// new hardware devices are introduced, or when SDL adds new GPU backends or
    /// modifies existing ones.</para>
    /// <para>Strings that have been found in the wild include:</para>
    /// <list type="bullet">
    /// <item>GTX 970</item>
    /// <item>GeForce GTX 970</item>
    /// <item>NVIDIA GeForce GTX 970</item>
    /// <item>Microsoft Direct3D12 (NVIDIA GeForce GTX 970)</item>
    /// <item>NVIDIA Graphics Device</item>
    /// <item>GeForce GPU</item>
    /// <item>P106-100</item>
    /// <item>AMD 15D8:C9</item>
    /// <item>AMD Custom GPU 0405</item>
    /// <item>AMD Radeon (TM) Graphics</item>
    /// <item>ASUS Radeon RX 470 Series</item>
    /// <item>Intel(R) Arc(tm) A380 Graphics (DG2)</item>
    /// <item>Virtio-GPU Venus (NVIDIA TITAN V)</item>
    /// <item>SwiftShader Device (LLVM 16.0.0)</item>
    /// <item>llvmpipe (LLVM 15.0.4, 256 bits)</item>
    /// <item>Microsoft Basic Render Driver</item>
    /// <item>unknown device</item>
    /// </list>
    /// <para>The above list shows that the same device can have different formats, the
    /// vendor name may or may not appear in the string, the included vendor name
    /// may not be the vendor of the chipset on the device, some manufacturers
    /// include pseudo-legal marks while others don't, some devices may not use a
    /// marketing name in the string, the device string may be wrapped by the name
    /// of a translation interface, the device may be emulated in software, or the
    /// string may contain generic text that does not identify the device at all.</para>
    /// <para><see cref="Props.GPUDeviceDriverNameString"/>: Contains the self-reported name
    /// of the underlying system driver.</para>
    /// <para>Strings that have been found in the wild include:</para>
    /// <list type="bullet">
    /// <item>Intel Corporation</item>
    /// <item>Intel open-source Mesa driver</item>
    /// <item>Qualcomm Technologies Inc. Adreno Vulkan Driver</item>
    /// <item>MoltenVK</item>
    /// <item>Mali-G715</item>
    /// <item>venus</item>
    /// </list>
    /// <para><see cref="Props.GPUDeviceDriverVersionString"/>: Contains the self-reported
    /// version of the underlying system driver. This is a relatively short version
    /// string in an unspecified format. If <see cref="Props.GPUDeviceDriverInfoString"/>
    /// is available then that property should be preferred over this one as it may
    /// contain additional information that is useful for identifying the exact
    /// driver version used.</para>
    /// <para>Strings that have been found in the wild include:</para>
    /// <list type="bullet">
    /// <item>53.0.0</item>
    /// <item>0.405.2463</item>
    /// <item>32.0.15.6614</item>
    /// </list>
    /// <para><see cref="Props.GPUDeviceDriverInfoString"/>: Contains the detailed version
    /// information of the underlying system driver as reported by the driver. This
    /// is an arbitrary string with no standardized format and it may contain
    /// newlines. This property should be preferred over
    /// <see cref="Props.GPUDeviceDriverVersionString"/> if it is available as it usually
    /// contains the same information but in a format that is easier to read.</para>
    /// <para>Strings that have been found in the wild include:</para>
    /// <list type="bullet">
    /// <item>101.6559</item>
    /// <item>1.2.11</item>
    /// <item>Mesa 21.2.2 (LLVM 12.0.1)</item>
    /// <item>Mesa 22.2.0-devel (git-f226222 2022-04-14 impish-oibaf-ppa)</item>
    /// <item>v1.r53p0-00eac0.824c4f31403fb1fbf8ee1042422c2129</item>
    /// </list>
    /// <para>This string has also been observed to be a multiline string (which has a
    /// trailing newline):</para>
    /// <code>
    /// Driver Build: 85da404, I46ff5fc46f, 1606794520
    /// Date: 11/30/20
    /// Compiler Version: EV031.31.04.01
    /// Driver Branch: promo490_3_Google
    /// </code>
    /// </summary>
    /// <param name="device">a GPU context to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUDeviceProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetGPUDeviceProperties(IntPtr device);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUComputePipeline *SDLCALL SDL_CreateGPUComputePipeline(SDL_GPUDevice *device, const SDL_GPUComputePipelineCreateInfo *createinfo);</code>
    /// <summary>
    /// <para>Creates a pipeline object to be used in a compute workflow.</para>
    /// <para>Shader resource bindings must be authored to follow a particular order
    /// depending on the shader format.</para>
    /// <para>For SPIR-V shaders, use the following resource sets:</para>
    /// <list type="bullet">
    /// <item>0: Sampled textures, followed by read-only storage textures, followed by
    /// read-only storage buffers</item>
    /// <item>1: Read-write storage textures, followed by read-write storage buffers</item>
    /// <item>2: Uniform buffers</item>
    /// </list>
    /// <para>For DXBC and DXIL shaders, use the following register order:</para>
    /// <list type="bullet">
    /// <item>(t[n], space0): Sampled textures, followed by read-only storage textures,
    /// followed by read-only storage buffers</item>
    /// <item>(u[n], space1): Read-write storage textures, followed by read-write
    /// storage buffers</item>
    /// <item>(b[n], space2): Uniform buffers</item>
    /// </list>
    /// <para>For MSL/metallib, use the following order:</para>
    /// <list type="bullet">
    /// <item>[[buffer]]: Uniform buffers, followed by read-only storage buffers,
    /// followed by read-write storage buffers</item>
    /// <item>[[texture]]: Sampled textures, followed by read-only storage textures,
    /// followed by read-write storage textures</item>
    /// </list>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the compute pipeline to
    /// create.</param>
    /// <returns>a compute pipeline object on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindGPUComputePipeline"/>
    /// <seealso cref="ReleaseGPUComputePipeline"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUComputePipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUComputePipeline(IntPtr device, in GPUComputePipelineCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUGraphicsPipeline *SDLCALL SDL_CreateGPUGraphicsPipeline(SDL_GPUDevice *device, const SDL_GPUGraphicsPipelineCreateInfo *createinfo);</code>
    /// <summary>
    /// Creates a pipeline object to be used in a graphics workflow.
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the graphics pipeline to
    /// create.</param>
    /// <returns>a graphics pipeline object on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    /// <seealso cref="BindGPUGraphicsPipeline"/>
    /// <seealso cref="ReleaseGPUGraphicsPipeline"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUGraphicsPipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUGraphicsPipeline(IntPtr device, in GPUGraphicsPipelineCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUSampler *SDLCALL SDL_CreateGPUSampler(SDL_GPUDevice *device, const SDL_GPUSamplerCreateInfo *createinfo);</code>
    /// <summary>
    /// Creates a sampler object to be used when binding textures in a graphics
    /// workflow.
    /// <para>There are optional properties that can be provided through <c>props</c>. These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUGraphicsPipelineCreateNameString"/>: a name that can be displayed in debugging tools.</item>
    /// </list>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the sampler to create.</param>
    /// <returns>a sampler object on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindGPUVertexSamplers(nint, uint, GPUTextureSamplerBinding[], uint)"/>
    /// <seealso cref="BindGPUFragmentSamplers(nint, uint, nint, uint)"/>
    /// <seealso cref="ReleaseGPUSampler"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUSampler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUSampler(IntPtr device, in GPUSamplerCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUShader *SDLCALL SDL_CreateGPUShader(SDL_GPUDevice *device, const SDL_GPUShaderCreateInfo *createinfo);</code>
    /// <summary>
    /// <para>Creates a shader to be used when creating a graphics pipeline.</para>
    /// <para>Shader resource bindings must be authored to follow a particular order
    /// depending on the shader format.</para>
    /// <para>For SPIR-V shaders, use the following resource sets:</para>
    /// <para>For vertex shaders:</para>
    /// <list type="bullet">
    /// <item>0: Sampled textures, followed by storage textures, followed by storage
    /// buffers</item>
    /// <item>1: Uniform buffers</item>
    /// </list>
    /// <para>For fragment shaders:</para>
    /// <list type="bullet">
    /// <item>2: Sampled textures, followed by storage textures, followed by storage
    /// buffers</item>
    /// <item>3: Uniform buffers</item>
    /// </list>
    /// <para>For DXBC and DXIL shaders, use the following register order:</para>
    /// <para>For vertex shaders:</para>
    /// <list type="bullet">
    /// <item>(t[n], space0): Sampled textures, followed by storage textures, followed
    /// by storage buffers</item>
    /// <item>(s[n], space0): Samplers with indices corresponding to the sampled
    /// textures</item>
    /// <item>(b[n], space1): Uniform buffers</item>
    /// </list>
    /// <para>For pixel shaders:</para>
    /// <list type="bullet">
    /// <item>(t[n], space2): Sampled textures, followed by storage textures, followed
    /// by storage buffers</item>
    /// <item>(s[n], space2): Samplers with indices corresponding to the sampled
    /// textures</item>
    /// <item>(b[n], space3): Uniform buffers</item>
    /// </list>
    /// <para>For MSL/metallib, use the following order:</para>
    /// <list type="bullet">
    /// <item>[[texture]]: Sampled textures, followed by storage textures</item>
    /// <item>[[sampler]]: Samplers with indices corresponding to the sampled textures</item>
    /// <item>[[buffer]]: Uniform buffers, followed by storage buffers. Vertex buffer 0
    /// is bound at [[buffer(14)]], vertex buffer 1 at [[buffer(15)]], and so on.
    /// Rather than manually authoring vertex buffer indices, use the
    /// [[stage_in]] attribute which will automatically use the vertex input
    /// information from the SDL_GPUGraphicsPipeline.</item>
    /// </list>
    /// <para>Shader semantics other than system-value semantics do not matter in D3D12
    /// and for ease of use the SDL implementation assumes that non system-value
    /// semantics will all be TEXCOORD. If you are using HLSL as the shader source
    /// language, your vertex semantics should start at TEXCOORD0 and increment
    /// like so: TEXCOORD1, TEXCOORD2, etc. If you wish to change the semantic
    /// prefix to something other than TEXCOORD you can use
    /// <see cref="Props.GPUDeviceCreateD3D12SemanticNameString"/> with
    /// <see cref="CreateGPUDeviceWithProperties"/>.</para>
    /// <para>There are optional properties that can be provided through <c>props</c>. These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUSamplerCreateNameString"/>: a name that can be displayed in debugging tools.</item>
    /// </list>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the shader to create.</param>
    /// <returns>a shader object on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUGraphicsPipeline"/>
    /// <seealso cref="ReleaseGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUShader"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUShader(IntPtr device, in GPUShaderCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUTexture *SDLCALL SDL_CreateGPUTexture(SDL_GPUDevice *device, const SDL_GPUTextureCreateInfo *createinfo);</code>
    /// <summary>
    /// <para>Creates a texture object to be used in graphics or compute workflows.</para>
    /// <para>The contents of this texture are undefined until data is written to the
    /// texture, either via <see cref="UploadToGPUTexture"/> or by performing a render or
    /// compute pass with this texture as a target.</para>
    /// <para>Note that certain combinations of usage flags are invalid. For example, a
    /// texture cannot have both the <see cref="GPUTextureUsageFlags.Sampler"/> and <see cref="GPUTextureUsageFlags.GraphicsStorageRead"/> flags.</para>
    /// <para>If you request a sample count higher than the hardware supports, the
    /// implementation will automatically fall back to the highest available sample
    /// count.</para>
    /// <para>There are optional properties that can be provided through
    /// <see cref="GPUTextureCreateInfo"/>'s <c>props</c>. These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearRFloat"/>: (Direct3D 12 only)
    /// if the texture usage is <see cref="GPUTextureUsageFlags.ColorTarget"/>, clear the
    /// texture to a color with this red intensity. Defaults to zero.</item>
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearGFloat"/>: (Direct3D 12 only)
    /// if the texture usage is <see cref="GPUTextureUsageFlags.ColorTarget"/>, clear the
    /// texture to a color with this green intensity.  Defaults to zero.</item>
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearBFloat"/>: (Direct3D 12 only)
    /// if the texture usage is <see cref="GPUTextureUsageFlags.ColorTarget"/>, clear the
    /// texture to a color with this blue intensity. Defaults to zero.</item>
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearAFloat"/>: (Direct3D 12 only)
    /// if the texture usage is <see cref="GPUTextureUsageFlags.ColorTarget"/>, clear the
    /// texture to a color with this alpha intensity. Defaults to zero.</item>
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearDepthFloat"/>: (Direct3D 12 only)
    /// if the texture usage is <see cref="GPUTextureUsageFlags.DepthStencilTarget"/>, clear
    /// the texture to a depth of this value. Defaults to zero.</item>
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearStencilUint8"/>: (Direct3D 12 only)
    /// if the texture usage is <see cref="GPUTextureUsageFlags.DepthStencilTarget"/>, clear
    /// the texture to a stencil of this value. Defaults to zero.</item>
    /// <item><see cref="Props.GPUTextureCreateD3D12ClearStencilNumber"/>: (Direct3D 12
    /// only) if the texture usage <see cref="GPUTextureUsageFlags.DepthStencilTarget"/>,
    /// clear the texture to a stencil of this Uint8 value. Defaults to zero.</item>
    /// <item><see cref="Props.GPUShaderCreateNameString"/>: a name that can be displayed in debugging tools.</item>
    /// </list>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the texture to create.</param>
    /// <returns>a texture object on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UploadToGPUTexture"/>
    /// <seealso cref="DownloadFromGPUTexture"/>
    /// <seealso cref="BeginGPURenderPass(nint, nint, uint, nint)"/>
    /// <seealso cref="BeginGPURenderPass(nint, in GPUColorTargetInfo[], uint, nint)"/>
    /// <seealso cref="BeginGPURenderPass(nint, nint, uint, in GPUDepthStencilTargetInfo)"/>
    /// <seealso cref="BeginGPURenderPass(nint, in GPUColorTargetInfo[], uint, in GPUDepthStencilTargetInfo)"/>
    /// <seealso cref="BeginGPUComputePass"/>
    /// <seealso cref="BindGPUVertexSamplers(nint, uint, GPUTextureSamplerBinding[], uint)"/>
    /// <seealso cref="BindGPUVertexStorageTextures"/>
    /// <seealso cref="BindGPUFragmentSamplers(nint, uint, nint, uint)"/>
    /// <seealso cref="BindGPUFragmentStorageTextures(nint, uint, nint[], uint)"/>
    /// <seealso cref="BindGPUComputeStorageTextures(nint, uint, nint[], uint)"/>
    /// <seealso cref="BlitGPUTexture"/>
    /// <seealso cref="ReleaseGPUTexture"/>
    /// <seealso cref="GPUTextureSupportsFormat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUTexture(IntPtr device, in GPUTextureCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUBuffer *SDLCALL SDL_CreateGPUBuffer(SDL_GPUDevice *device, const SDL_GPUBufferCreateInfo *createinfo);</code>
    /// <summary>
    /// <para>Creates a buffer object to be used in graphics or compute workflows.</para>
    /// <para>The contents of this buffer are undefined until data is written to the
    /// buffer.</para>
    /// <para>Note that certain combinations of usage flags are invalid. For example, a
    /// buffer cannot have both the <see cref="GPUBufferUsageFlags.Vertex"/> and <see cref="GPUBufferUsageFlags.Index"/> flags.</para>
    /// <para>If you use a STORAGE flag, the data in the buffer must respect std140
    /// layout conventions. In practical terms this means you must ensure that vec3
    /// and vec4 fields are 16-byte aligned.</para>
    /// <para>For better understanding of underlying concepts and memory management with
    /// SDL GPU API, you may refer
    /// [this blog post](https://moonside.games/posts/sdl-gpu-concepts-cycling/).</para>
    /// <para>There are optional properties that can be provided through <c>props</c>. These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUBufferCreateNameString"/>: a name that can be displayed in debugging tools.</item>
    /// </list>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the buffer to create.</param>
    /// <returns>a buffer object on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetGPUBufferName"/>
    /// <seealso cref="UploadToGPUBuffer"/>
    /// <seealso cref="DownloadFromGPUBuffer"/>
    /// <seealso cref="CopyGPUBufferToBuffer"/>
    /// <seealso cref="BindGPUVertexBuffers(nint, uint, GPUBufferBinding[], uint)"/>
    /// <seealso cref="BindGPUIndexBuffer"/>
    /// <seealso cref="BindGPUVertexStorageBuffers"/>
    /// <seealso cref="BindGPUFragmentStorageBuffers(nint, uint, nint[], uint)"/>
    /// <seealso cref="DrawGPUPrimitivesIndirect"/>
    /// <seealso cref="DrawGPUIndexedPrimitivesIndirect"/>
    /// <seealso cref="BindGPUComputeStorageBuffers(nint, uint, nint[], uint)"/>
    /// <seealso cref="DispatchGPUComputeIndirect"/>
    /// <seealso cref="ReleaseGPUBuffer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUBuffer(IntPtr device, in GPUBufferCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUTransferBuffer *SDLCALL SDL_CreateGPUTransferBuffer(SDL_GPUDevice *device, const SDL_GPUTransferBufferCreateInfo *createinfo);</code>
    /// <summary>
    /// <para>Creates a transfer buffer to be used when uploading to or downloading from
    /// graphics resources.</para>
    /// <para>Download buffers can be particularly expensive to create, so it is good
    /// practice to reuse them if data will be downloaded regularly.</para>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="createinfo">a struct describing the state of the transfer buffer to
    /// create.</param>
    /// <returns>a transfer buffer on success, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UploadToGPUBuffer"/>
    /// <seealso cref="DownloadFromGPUBuffer"/>
    /// <seealso cref="UploadToGPUTexture"/>
    /// <seealso cref="DownloadFromGPUTexture"/>
    /// <seealso cref="ReleaseGPUTransferBuffer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPUTransferBuffer(IntPtr device, in GPUTransferBufferCreateInfo createinfo);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUBufferName(SDL_GPUDevice *device, SDL_GPUBuffer *buffer, const char *text);</code>
    /// <summary>
    /// <para>Sets an arbitrary string constant to label a buffer.</para>
    /// <para>You should use <see cref="Props.GPUBufferCreateNameString"/> with <see cref="CreateGPUBuffer"/> instead of this function to avoid thread safety issues.</para>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="buffer">a buffer to attach the name to.</param>
    /// <param name="text">a UTF-8 string constant to mark as the name of the buffer.</param>
    /// <threadsafety>This function is not thread safe, you must make sure the buffer is not simultaneously used by any other thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUBuffer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUBufferName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGPUBufferName(IntPtr device, IntPtr buffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUTextureName(SDL_GPUDevice *device, SDL_GPUTexture *texture, const char *text);</code>
    /// <summary>
    /// <para>Sets an arbitrary string constant to label a texture.</para>
    /// <para>You should use <see cref="Props.GPUTextureCreateNameString"/> with <see cref="CreateGPUTexture"/> instead of this function to avoid thread safety issues.</para>
    /// </summary>
    /// <param name="device">a GPU Context.</param>
    /// <param name="texture">a texture to attach the name to.</param>
    /// <param name="text">a UTF-8 string constant to mark as the name of the texture.</param>
    /// <threadsafety>This function is not thread safe, you must make sure the texture is not simultaneously used by any other thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUTextureName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGPUTextureName(IntPtr device, IntPtr texture, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_InsertGPUDebugLabel(SDL_GPUCommandBuffer *command_buffer, const char *text);</code>
    /// <summary>
    /// <para>Inserts an arbitrary string label into the command buffer callstream.</para>
    /// <para>Useful for debugging.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="text">a UTF-8 string constant to insert as the label.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_InsertGPUDebugLabel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InsertGPUDebugLabel(IntPtr commandBuffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUDebugGroup(SDL_GPUCommandBuffer *command_buffer, const char *name);</code>
    /// <summary>
    /// <para>Begins a debug group with an arbitrary name.</para>
    /// <para>Used for denoting groups of calls when viewing the command buffer
    /// callstream in a graphics debugging tool.</para>
    /// <para>Each call to <see cref="PushGPUDebugGroup"/> must have a corresponding call to
    /// <see cref="PopGPUDebugGroup"/>.</para>
    /// <para>On some backends (e.g. Metal), pushing a debug group during a
    /// render/blit/compute pass will create a group that is scoped to the native
    /// pass rather than the command buffer. For best results, if you push a debug
    /// group during a pass, always pop it in the same pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="text">a UTF-8 string constant that names the group.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PopGPUDebugGroup"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUDebugGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUDebugGroup(IntPtr commandBuffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PopGPUDebugGroup(SDL_GPUCommandBuffer *command_buffer);</code>
    /// <summary>
    /// Ends the most-recently pushed debug group.
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PushGPUDebugGroup"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PopGPUDebugGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PopGPUDebugGroup(IntPtr commandBuffer);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUTexture(SDL_GPUDevice *device, SDL_GPUTexture *texture);</code>
    /// <summary>
    /// <para>Frees the given texture as soon as it is safe to do so.</para>
    /// <para>You must not reference the texture after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="texture">a texture to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUTexture(IntPtr device, IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUSampler(SDL_GPUDevice *device, SDL_GPUSampler *sampler);</code>
    /// <summary>
    /// <para>Frees the given sampler as soon as it is safe to do so.</para>
    /// <para>You must not reference the sampler after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="sampler">a sampler to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUSampler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUSampler(IntPtr device, IntPtr sampler);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUBuffer(SDL_GPUDevice *device, SDL_GPUBuffer *buffer);</code>
    /// <summary>
    /// <para>Frees the given buffer as soon as it is safe to do so.</para>
    /// <para>You must not reference the buffer after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="buffer">a buffer to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUBuffer(IntPtr device, IntPtr buffer);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUTransferBuffer(SDL_GPUDevice *device, SDL_GPUTransferBuffer *transfer_buffer);</code>
    /// <summary>
    /// <para>Frees the given transfer buffer as soon as it is safe to do so.</para>
    /// <para>You must not reference the transfer buffer after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="transferBuffer">a transfer buffer to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUTransferBuffer(IntPtr device, IntPtr transferBuffer);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUComputePipeline(SDL_GPUDevice *device, SDL_GPUComputePipeline *compute_pipeline);</code>
    /// <summary>
    /// <para>Frees the given compute pipeline as soon as it is safe to do so.</para>
    /// <para>You must not reference the compute pipeline after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="computePipeline">a compute pipeline to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUComputePipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUComputePipeline(IntPtr device, IntPtr computePipeline);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUShader(SDL_GPUDevice *device, SDL_GPUShader *shader);</code>
    /// <summary>
    /// <para>Frees the given shader as soon as it is safe to do so.</para>
    /// <para>You must not reference the shader after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="shader">a shader to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUShader"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUShader(IntPtr device, IntPtr shader);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUGraphicsPipeline(SDL_GPUDevice *device, SDL_GPUGraphicsPipeline *graphics_pipeline);</code>
    /// <summary>
    /// <para>Frees the given graphics pipeline as soon as it is safe to do so.</para>
    /// <para>You must not reference the graphics pipeline after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="graphicsPipeline">a graphics pipeline to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUGraphicsPipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUGraphicsPipeline(IntPtr device, IntPtr graphicsPipeline);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUCommandBuffer *SDLCALL SDL_AcquireGPUCommandBuffer(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Acquire a command buffer.</para>
    /// <para>This command buffer is managed by the implementation and should not be
    /// freed by the user. The command buffer may only be used on the thread it was
    /// acquired on. The command buffer should be submitted on the thread it was
    /// acquired on.</para>
    /// <para>It is valid to acquire multiple command buffers on the same thread at once.
    /// In fact a common design pattern is to acquire two command buffers per frame
    /// where one is dedicated to render and compute passes and the other is
    /// dedicated to copy passes and other preparatory work such as generating
    /// mipmaps. Interleaving commands between the two command buffers reduces the
    /// total amount of passes overall which improves rendering performance.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <returns>a command buffer, or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AcquireGPUCommandBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr AcquireGPUCommandBuffer(IntPtr device);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUVertexUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a vertex uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls will use this uniform data.</para>
    /// <para>The data being pushed must respect std140 layout conventions. In practical
    /// terms this means you must ensure that vec3 and vec4 fields are 16-byte
    /// aligned.</para>
    /// <para>For detailed information about accessing uniform data from a shader, please
    /// refer to <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the vertex uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUVertexUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUVertexUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a vertex uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls will use this uniform data.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the vertex uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUVertexUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUFragmentUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a fragment uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls will use this uniform data.</para>
    /// <para>The data being pushed must respect std140 layout conventions. In practical
    /// terms this means you must ensure that vec3 and vec4 fields are 16-byte
    /// aligned.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the fragment uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUFragmentUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUFragmentUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a fragment uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls will use this uniform data.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the fragment uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUFragmentUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUComputeUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls will use this uniform data.</para>
    /// <para>The data being pushed must respect std140 layout conventions. In practical
    /// terms this means you must ensure that vec3 and vec4 fields are 16-byte
    /// aligned.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUComputeUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUComputeUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls will use this uniform data.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUComputeUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPURenderPass *SDLCALL SDL_BeginGPURenderPass(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUColorTargetInfo *color_target_infos, Uint32 num_color_targets, const SDL_GPUDepthStencilTargetInfo *depth_stencil_target_info);</code>
    /// <summary>
    /// <para>Begins a render pass on a command buffer.</para>
    /// <para>A render pass consists of a set of texture subresources (or depth slices in
    /// the 3D texture case) which will be rendered to during the render pass,
    /// along with corresponding clear values and load/store operations. All
    /// operations related to graphics pipelines must take place inside of a render
    /// pass. A default viewport and scissor state are automatically set when this
    /// is called. You cannot begin another render pass, or begin a compute pass or
    /// copy pass until you have ended the render pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="colorTargetInfos">an array of texture subresources with
    /// corresponding clear values and load/store ops.</param>
    /// <param name="numColorTargets">the number of color targets in the
    /// <c>colorTargetInfos</c> array.</param>
    /// <param name="depthStencilTargetInfo">a texture subresource with corresponding
    /// clear value and load/store ops, may be
    /// <c>null</c>.</param>
    /// <returns>a render pass handle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EndGPURenderPass"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPURenderPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr BeginGPURenderPass(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPURenderPass *SDLCALL SDL_BeginGPURenderPass(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUColorTargetInfo *color_target_infos, Uint32 num_color_targets, const SDL_GPUDepthStencilTargetInfo *depth_stencil_target_info);</code>
    /// <summary>
    /// <para>Begins a render pass on a command buffer.</para>
    /// <para>A render pass consists of a set of texture subresources (or depth slices in
    /// the 3D texture case) which will be rendered to during the render pass,
    /// along with corresponding clear values and load/store operations. All
    /// operations related to graphics pipelines must take place inside of a render
    /// pass. A default viewport and scissor state are automatically set when this
    /// is called. You cannot begin another render pass, or begin a compute pass or
    /// copy pass until you have ended the render pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="colorTargetInfos">an array of texture subresources with
    /// corresponding clear values and load/store ops.</param>
    /// <param name="numColorTargets">the number of color targets in the
    /// <c>colorTargetInfos</c> array.</param>
    /// <param name="depthStencilTargetInfo">a texture subresource with corresponding
    /// clear value and load/store ops, may be
    /// <c>null</c>.</param>
    /// <returns>a render pass handle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EndGPURenderPass"/>
    public static IntPtr BeginGPURenderPass(IntPtr commandBuffer, in GPUColorTargetInfo[] colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo)
    {
        if (colorTargetInfos.Length == 0)
            return BeginGPURenderPass(commandBuffer, IntPtr.Zero, 0, depthStencilTargetInfo);
        
        unsafe
        {
            fixed (GPUColorTargetInfo* pInfos = &colorTargetInfos[0])
            { 
                return BeginGPURenderPass(commandBuffer, (nint)pInfos, numColorTargets, depthStencilTargetInfo);
            }
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPURenderPass *SDLCALL SDL_BeginGPURenderPass(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUColorTargetInfo *color_target_infos, Uint32 num_color_targets, const SDL_GPUDepthStencilTargetInfo *depth_stencil_target_info);</code>
    /// <summary>
    /// <para>Begins a render pass on a command buffer.</para>
    /// <para>A render pass consists of a set of texture subresources (or depth slices in
    /// the 3D texture case) which will be rendered to during the render pass,
    /// along with corresponding clear values and load/store operations. All
    /// operations related to graphics pipelines must take place inside of a render
    /// pass. A default viewport and scissor state are automatically set when this
    /// is called. You cannot begin another render pass, or begin a compute pass or
    /// copy pass until you have ended the render pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="colorTargetInfos">an array of texture subresources with
    /// corresponding clear values and load/store ops.</param>
    /// <param name="numColorTargets">the number of color targets in the
    /// <c>colorTargetInfos</c> array.</param>
    /// <param name="depthStencilTargetInfo">a texture subresource with corresponding
    /// clear value and load/store ops, may be
    /// <c>null</c>.</param>
    /// <returns>a render pass handle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EndGPURenderPass"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPURenderPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr BeginGPURenderPass(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, in GPUDepthStencilTargetInfo depthStencilTargetInfo);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPURenderPass *SDLCALL SDL_BeginGPURenderPass(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUColorTargetInfo *color_target_infos, Uint32 num_color_targets, const SDL_GPUDepthStencilTargetInfo *depth_stencil_target_info);</code>
    /// <summary>
    /// <para>Begins a render pass on a command buffer.</para>
    /// <para>A render pass consists of a set of texture subresources (or depth slices in
    /// the 3D texture case) which will be rendered to during the render pass,
    /// along with corresponding clear values and load/store operations. All
    /// operations related to graphics pipelines must take place inside of a render
    /// pass. A default viewport and scissor state are automatically set when this
    /// is called. You cannot begin another render pass, or begin a compute pass or
    /// copy pass until you have ended the render pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="colorTargetInfos">an array of texture subresources with
    /// corresponding clear values and load/store ops.</param>
    /// <param name="numColorTargets">the number of color targets in the
    /// <c>colorTargetInfos</c> array.</param>
    /// <param name="depthStencilTargetInfo">a texture subresource with corresponding
    /// clear value and load/store ops, may be
    /// <c>null</c>.</param>
    /// <returns>a render pass handle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EndGPURenderPass"/>
    public static IntPtr BeginGPURenderPass(IntPtr commandBuffer, in GPUColorTargetInfo[] colorTargetInfos, uint numColorTargets, in GPUDepthStencilTargetInfo depthStencilTargetInfo)
    {
        if (colorTargetInfos.Length == 0)
            return BeginGPURenderPass(commandBuffer, IntPtr.Zero, 0, depthStencilTargetInfo);
                
        unsafe
        {
            fixed (GPUColorTargetInfo* pInfos = &colorTargetInfos[0])
            { 
                return BeginGPURenderPass(commandBuffer, (nint)pInfos, numColorTargets, depthStencilTargetInfo);
            }
        }
    }
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUGraphicsPipeline(SDL_GPURenderPass *render_pass, SDL_GPUGraphicsPipeline *graphics_pipeline);</code>
    /// <summary>
    /// <para>Binds a graphics pipeline on a render pass to be used in rendering.</para>
    /// <para>A graphics pipeline must be bound before making any draw calls.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="graphicsPipeline">the graphics pipeline to bind.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUGraphicsPipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUGraphicsPipeline(IntPtr renderPass, IntPtr graphicsPipeline);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUViewport(SDL_GPURenderPass *render_pass, const SDL_GPUViewport *viewport);</code>
    /// <summary>
    /// Sets the current viewport state on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="viewport">the viewport to set.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUViewport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGPUViewport(IntPtr renderPass, in GPUViewport viewport);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUScissor(SDL_GPURenderPass *render_pass, const SDL_Rect *scissor);</code>
    /// <summary>
    /// Sets the current scissor state on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="scissor">the scissor area to set.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUScissor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGPUScissor(IntPtr renderPass, in Rect scissor);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUBlendConstants(SDL_GPURenderPass *render_pass, SDL_FColor blend_constants);</code>
    /// <summary>
    /// Sets the current blend constants on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="blendConstants">the blend constant color.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GPUBlendFactor.ConstantColor"/>
    /// <seealso cref="GPUBlendFactor.OneMinusConstantColor"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUBlendConstants"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGPUBlendConstants(IntPtr renderPass, in FColor blendConstants);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUStencilReference(SDL_GPURenderPass *render_pass, Uint8 reference);</code>
    /// <summary>
    /// Sets the current stencil reference value on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="reference">the stencil reference value to set.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUStencilReference"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGPUStencilReference(IntPtr renderPass, byte reference);

    
    #region BindGPUVertexBuffers
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUVertexBuffers(SDL_GPURenderPass *render_pass, Uint32 first_slot, const SDL_GPUBufferBinding *bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// Binds vertex buffers on a command buffer for use with subsequent draw
    /// calls.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the vertex buffer slot to begin binding from.</param>
    /// <param name="bindings">an array of <see cref="GPUBufferBinding"/> structs containing vertex
    /// buffers and offset values.</param>
    /// <param name="numBindings">the number of bindings in the bindings array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUVertexBuffers(IntPtr renderPass, uint firstSlot, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] GPUBufferBinding[] bindings, uint numBindings);

    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUVertexBuffers(SDL_GPURenderPass *render_pass, Uint32 first_slot, const SDL_GPUBufferBinding *bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// Binds vertex buffers on a command buffer for use with subsequent draw
    /// calls.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the vertex buffer slot to begin binding from.</param>
    /// <param name="bindings">a pointer to an array of <see cref="GPUBufferBinding"/> structs containing vertex
    /// buffers and offset values.</param>
    /// <param name="numBindings">the number of bindings in the bindings array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUVertexBuffers(IntPtr renderPass, uint firstSlot, IntPtr bindings, uint numBindings);
    #endregion
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUIndexBuffer(SDL_GPURenderPass *render_pass, const SDL_GPUBufferBinding *binding, SDL_GPUIndexElementSize index_element_size);</code>
    /// <summary>
    /// Binds an index buffer on a command buffer for use with subsequent draw
    /// calls.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="binding">a pointer to a struct containing an index buffer and offset.</param>
    /// <param name="indexElementSize">whether the index values in the buffer are 16- or
    /// 32-bit.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUIndexBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUIndexBuffer(IntPtr renderPass, in GPUBufferBinding binding, GPUIndexElementSize indexElementSize);
    
    
    #region BindGPUVertexSamplers
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUVertexSamplers(SDL_GPURenderPass *render_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the vertex shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the vertex sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler pairs to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUVertexSamplers(IntPtr renderPass, uint firstSlot, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);


    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUVertexSamplers(SDL_GPURenderPass *render_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the vertex shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the vertex sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">a pointer an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler pairs to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUVertexSamplers(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    #endregion
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUVertexStorageTextures(SDL_GPURenderPass *render_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures for use on the vertex shader.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.GraphicsStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the vertex storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage texture to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUVertexStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUVertexStorageBuffers(SDL_GPURenderPass *render_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers for use on the vertex shader.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.GraphicsStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the vertex storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of buffers.</param>
    /// <param name="numBindings">the number of buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUVertexStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);

    
    #region BindGPUFragmentSamplers
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUFragmentSamplers(SDL_GPURenderPass *render_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the fragment shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the fragment sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler pairs to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUFragmentSamplers(SDL_GPURenderPass *render_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the fragment shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the fragment sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler pairs to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    #endregion
    
    
    #region BindGPUFragmentStorageTextures
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUFragmentStorageTextures(SDL_GPURenderPass *render_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures for use on the fragment shader.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.GraphicsStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the fragment storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage textures to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUFragmentStorageTextures(SDL_GPURenderPass *render_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures for use on the fragment shader.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.GraphicsStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the fragment storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage textures to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    public static partial void BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    #endregion
    
    
    #region BindGPUFragmentStorageBuffers
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUFragmentStorageBuffers(SDL_GPURenderPass *render_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers for use on the fragment shader.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.GraphicsStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the fragment storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of storage buffers.</param>
    /// <param name="numBindings">the number of storage buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUFragmentStorageBuffers(SDL_GPURenderPass *render_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers for use on the fragment shader.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.GraphicsStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="firstSlot">the fragment storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of storage buffers.</param>
    /// <param name="numBindings">the number of storage buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    #endregion
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DrawGPUIndexedPrimitives(SDL_GPURenderPass *render_pass, Uint32 num_indices, Uint32 num_instances, Uint32 first_index, Sint32 vertex_offset, Uint32 first_instance);</code>
    /// <summary>
    /// <para>Draws data using bound graphics state with an index buffer and instancing
    /// enabled.</para>
    /// <para>You must not call this function before binding a graphics pipeline.</para>
    /// <para>Note that the <c>firstIndex</c> and <c>firstInstance</c> parameters are NOT
    /// compatible with built-in vertex/instance ID variables in shaders (for
    /// example, SV_VertexID); GPU APIs and shader languages do not define these
    /// built-in variables consistently, so if your shader depends on them, the 
    /// only way to keep behavior consistent and portable is to always pass 0 for 
    /// the correlating parameter in the draw calls.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="numIndices">the number of indices to draw per instance.</param>
    /// <param name="numInstances">the number of instances to draw.</param>
    /// <param name="firstIndex">the starting index within the index buffer.</param>
    /// <param name="vertexOffset">value added to vertex index before indexing into the
    /// vertex buffer.</param>
    /// <param name="firstInstance">the ID of the first instance to draw.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUIndexedPrimitives"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DrawGPUIndexedPrimitives(IntPtr renderPass, uint numIndices, uint numInstances, uint firstIndex, int vertexOffset, uint firstInstance);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DrawGPUPrimitives(SDL_GPURenderPass *render_pass, Uint32 num_vertices, Uint32 num_instances, Uint32 first_vertex, Uint32 first_instance);</code>
    /// <summary>
    /// <para>Draws data using bound graphics state.</para>
    /// <para>You must not call this function before binding a graphics pipeline.</para>
    /// <para>Note that the <c>firstVertex</c> and <c>firstInstance</c> parameters are NOT
    /// compatible with built-in vertex/instance ID variables in shaders (for
    /// example, SV_VertexID); GPU APIs and shader languages do not define these
    /// built-in variables consistently, so if your shader depends on them, the
    /// only way to keep behavior consistent and portable is to always pass 0 for
    /// the correlating parameter in the draw calls.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="numVertices">the number of vertices to draw.</param>
    /// <param name="numInstances">the number of instances that will be drawn.</param>
    /// <param name="firstVertex">the index of the first vertex to draw.</param>
    /// <param name="firstInstance">the ID of the first instance to draw.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUPrimitives"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DrawGPUPrimitives(IntPtr renderPass, uint numVertices, uint numInstances, uint firstVertex, uint firstInstance);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DrawGPUPrimitivesIndirect(SDL_GPURenderPass *render_pass, SDL_GPUBuffer *buffer, Uint32 offset, Uint32 draw_count);</code>
    /// <summary>
    /// <para>Draws data using bound graphics state and with draw parameters set from a
    /// buffer.</para>
    /// <para>The buffer must consist of tightly-packed draw parameter sets that each
    /// match the layout of <see cref="GPUIndirectDrawCommand"/>. You must not call this
    /// function before binding a graphics pipeline.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="buffer">a buffer containing draw parameters.</param>
    /// <param name="offset">the offset to start reading from the draw buffer.</param>
    /// <param name="drawCount">the number of draw parameter sets that should be read
    /// from the draw buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUPrimitivesIndirect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DrawGPUPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DrawGPUIndexedPrimitivesIndirect(SDL_GPURenderPass *render_pass, SDL_GPUBuffer *buffer, Uint32 offset, Uint32 draw_count);</code>
    /// <summary>
    /// <para>Draws data using bound graphics state with an index buffer enabled and with
    /// draw parameters set from a buffer.</para>
    /// <para>The buffer must consist of tightly-packed draw parameter sets that each
    /// match the layout of <see cref="GPUIndexedIndirectDrawCommand"/>. You must not call
    /// this function before binding a graphics pipeline.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="buffer">a buffer containing draw parameters.</param>
    /// <param name="offset">the offset to start reading from the draw buffer.</param>
    /// <param name="drawCount">the number of draw parameter sets that should be read
    /// from the draw buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUIndexedPrimitivesIndirect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DrawGPUIndexedPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EndGPURenderPass(SDL_GPURenderPass *render_pass);</code>
    /// <summary>
    /// <para>Ends the given render pass.</para>
    /// <para>All bound graphics state on the render pass command buffer is unset. The
    /// render pass handle is now invalid.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EndGPURenderPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void EndGPURenderPass(IntPtr renderPass);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUComputePass *SDLCALL SDL_BeginGPUComputePass(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUStorageTextureReadWriteBinding *storage_texture_bindings, Uint32 num_storage_texture_bindings, const SDL_GPUStorageBufferReadWriteBinding *storage_buffer_bindings, Uint32 num_storage_buffer_bindings);</code>
    /// <summary>
    /// <para>Begins a compute pass on a command buffer.</para>
    /// <para>A compute pass is defined by a set of texture subresources and buffers that
    /// may be written to by compute pipelines. These textures and buffers must
    /// have been created with the <see cref="GPUTextureUsageFlags.ComputeStorageWrite"/> bit or the
    /// <see cref="GPUTextureUsageFlags.ComputeStorageSimultaneousReadWrite"/> bit. If you do not create a texture
    /// with <see cref="GPUTextureUsageFlags.ComputeStorageSimultaneousReadWrite"/>, you must not read from the
    /// texture in the compute pass. All operations related to compute pipelines
    /// must take place inside of a compute pass. You must not begin another
    /// compute pass, or a render pass or copy pass before ending the compute pass.</para>
    /// <para>A VERY IMPORTANT NOTE - Reads and writes in compute passes are NOT
    /// implicitly synchronized. This means you may cause data races by both
    /// reading and writing a resource region in a compute pass, or by writing
    /// multiple times to a resource region. If your compute work depends on
    /// reading the completed output from a previous dispatch, you MUST end the
    /// current compute pass and begin a new one before you can safely access the
    /// data. Otherwise you will receive unexpected results. Reading and writing a
    /// texture in the same compute pass is only supported by specific texture
    /// formats. Make sure you check the format support!</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="storageTextureBindings">an array of writeable storage texture
    /// binding structs.</param>
    /// <param name="numStorageTextureBindings">the number of storage textures to bind
    /// from the array.</param>
    /// <param name="storageBufferBindings">an array of writeable storage buffer binding
    /// structs.</param>
    /// <param name="numStorageBufferBindings">the number of storage buffers to bind
    /// from the array.</param>
    /// <returns>a compute pass handle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EndGPUComputePass"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPUComputePass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr BeginGPUComputePass(IntPtr commandBuffer, GPUStorageTextureReadWriteBinding[] storageTextureBindings, uint numStorageTextureBindings, GPUStorageBufferReadWriteBinding[] storageBufferBindings, uint numStorageBufferBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputePipeline(SDL_GPUComputePass *compute_pass, SDL_GPUComputePipeline *compute_pipeline);</code>
    /// <summary>
    /// Binds a compute pipeline on a command buffer for use in compute dispatch.
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="computePipeline">a compute pipeline to bind.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputePipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUComputePipeline(IntPtr computePass, IntPtr computePipeline);
    
    
    #region BindGPUComputeSamplers
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeSamplers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the compute shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler bindings to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeSamplers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the compute shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler bindings to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    #endregion
    
    
    #region BindGPUComputeStorageTextures
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageTextures(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures as readonly for use on the compute pipeline.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage textures to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageTextures(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures as readonly for use on the compute pipeline.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage textures to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    public static partial void BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    #endregion
    
    
    #region BindGPUComputeStorageBuffers
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageBuffers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers as readonly for use on the compute pipeline.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of storage buffer binding structs.</param>
    /// <param name="numBindings">the number of storage buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageBuffers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers as readonly for use on the compute pipeline.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUShader"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of storage buffer binding structs.</param>
    /// <param name="numBindings">the number of storage buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUShader"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])] 
    public static partial void BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    #endregion
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DispatchGPUCompute(SDL_GPUComputePass *compute_pass, Uint32 groupcount_x, Uint32 groupcount_y, Uint32 groupcount_z);</code>
    /// <summary>
    /// <para>Dispatches compute work.</para>
    /// <para>You must not call this function before binding a compute pipeline.</para>
    /// <para>A VERY IMPORTANT NOTE If you dispatch multiple times in a compute pass, and
    /// the dispatches write to the same resource region as each other, there is no
    /// guarantee of which order the writes will occur. If the write order matters,
    /// you MUST end the compute pass and begin another one.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="groupcountX">number of local workgroups to dispatch in the X
    /// dimension.</param>
    /// <param name="groupcountY">number of local workgroups to dispatch in the Y
    /// dimension.</param>
    /// <param name="groupcountZ">number of local workgroups to dispatch in the Z
    /// dimension.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DispatchGPUCompute"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DispatchGPUCompute(IntPtr computePass, uint groupcountX, uint groupcountY, uint groupcountZ);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DispatchGPUComputeIndirect(SDL_GPUComputePass *compute_pass, SDL_GPUBuffer *buffer, Uint32 offset);</code>
    /// <summary>
    /// <para>Dispatches compute work with parameters set from a buffer.</para>
    /// <para>The buffer layout should match the layout of
    /// <see cref="GPUIndirectDispatchCommand"/>. You must not call this function before
    /// binding a compute pipeline.</para>
    /// <para>A VERY IMPORTANT NOTE If you dispatch multiple times in a compute pass, and
    /// the dispatches write to the same resource region as each other, there is no
    /// guarantee of which order the writes will occur. If the write order matters,
    /// you MUST end the compute pass and begin another one.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="buffer">a buffer containing dispatch parameters.</param>
    /// <param name="offset">the offset to start reading from the dispatch buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DispatchGPUComputeIndirect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DispatchGPUComputeIndirect(IntPtr computePass, IntPtr buffer, uint offset);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EndGPUComputePass(SDL_GPUComputePass *compute_pass);</code>
    /// <summary>
    /// <para>Ends the current compute pass.</para>
    /// <para>All bound compute state on the command buffer is unset. The compute pass
    /// handle is now invalid.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EndGPUComputePass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void EndGPUComputePass(IntPtr computePass);
    
    
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_MapGPUTransferBuffer(SDL_GPUDevice *device, SDL_GPUTransferBuffer *transfer_buffer, bool cycle);</code>
    /// <summary>
    /// <para>Maps a transfer buffer into application address space.</para>
    /// <para>You must unmap the transfer buffer before encoding upload commands. The
    /// memory is owned by the graphics driver - do NOT call <see cref="Free"/> on the
    /// returned pointer.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="transferBuffer">a transfer buffer.</param>
    /// <param name="cycle">if <c>true</c>, cycles the transfer buffer if it is already bound.</param>
    /// <returns>the address of the mapped transfer buffer memory, or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MapGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr MapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer, [MarshalAs(UnmanagedType.I1)] bool cycle);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnmapGPUTransferBuffer(SDL_GPUDevice *device, SDL_GPUTransferBuffer *transfer_buffer);</code>
    /// <summary>
    /// Unmaps a previously mapped transfer buffer.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="transferBuffer">a previously mapped transfer buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnmapGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnmapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUCopyPass *SDLCALL SDL_BeginGPUCopyPass(SDL_GPUCommandBuffer *command_buffer);</code>
    /// <summary>
    /// <para>Begins a copy pass on a command buffer.</para>
    /// <para>All operations related to copying to or from buffers or textures take place
    /// inside a copy pass. You must not begin another copy pass, or a render pass
    /// or compute pass before ending the copy pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <returns>a copy pass handle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="EndGPUCopyPass"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPUCopyPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr BeginGPUCopyPass(IntPtr commandBuffer);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PixelFormat SDLCALL SDL_GetPixelFormatFromGPUTextureFormat(SDL_GPUTextureFormat format);</code>
    /// <summary>
    /// <para>Get the SDL pixel format corresponding to a GPU texture format.</para>
    /// </summary>
    /// <param name="format">a texture format.</param>
    /// <returns>the corresponding pixel format, or <see cref="PixelFormat.Unknown"/> if
    /// there is no corresponding pixel format.</returns>
    /// <since>This function is available since SDL 3.4.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPixelFormatFromGPUTextureFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PixelFormat GetPixelFormatFromGPUTextureFormat(GPUTextureFormat format);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUTextureFormat SDLCALL SDL_GetGPUTextureFormatFromPixelFormat(SDL_PixelFormat format);</code>
    /// <summary>
    /// Get the GPU texture format corresponding to an SDL pixel format.
    /// </summary>
    /// <param name="format">a pixel format.</param>
    /// <returns>the corresponding GPU texture format, or
    /// <see cref="GPUTextureFormat.Invalid"/> if there is no corresponding GPU
    /// texture format.</returns>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUTextureFormatFromPixelFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GPUTextureFormat GetPixelFormatFromGPUTextureFormat(PixelFormat format);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UploadToGPUTexture(SDL_GPUCopyPass *copy_pass, const SDL_GPUTextureTransferInfo *source, const SDL_GPUTextureRegion *destination, bool cycle);</code>
    /// <summary>
    /// <para>Uploads data from a transfer buffer to a texture.</para>
    /// <para>The upload occurs on the GPU timeline. You may assume that the upload has
    /// finished in subsequent commands.</para>
    /// <para>You must align the data in the transfer buffer to a multiple of the texel
    /// size of the texture format.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">the source transfer buffer with image layout information.</param>
    /// <param name="destination">the destination texture region.</param>
    /// <param name="cycle">if <c>true</c>, cycles the texture if the texture is bound, otherwise
    /// overwrites the data.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UploadToGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UploadToGPUTexture(IntPtr copyPass, in GPUTextureTransferInfo source, in GPUTextureRegion destination, [MarshalAs(UnmanagedType.I1)] bool cycle);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UploadToGPUBuffer(SDL_GPUCopyPass *copy_pass, const SDL_GPUTransferBufferLocation *source, const SDL_GPUBufferRegion *destination, bool cycle);</code>
    /// <summary>
    /// <para>Uploads data from a transfer buffer to a buffer.</para>
    /// <para>The upload occurs on the GPU timeline. You may assume that the upload has
    /// finished in subsequent commands.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">the source transfer buffer with offset.</param>
    /// <param name="destination">the destination buffer with offset and size.</param>
    /// <param name="cycle">if <c>true</c>, cycles the buffer if it is already bound, otherwise
    /// overwrites the data.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UploadToGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UploadToGPUBuffer(IntPtr copyPass, in GPUTransferBufferLocation source, in GPUBufferRegion destination, [MarshalAs(UnmanagedType.I1)] bool cycle);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CopyGPUTextureToTexture(SDL_GPUCopyPass *copy_pass, const SDL_GPUTextureLocation *source, const SDL_GPUTextureLocation *destination, Uint32 w, Uint32 h, Uint32 d, bool cycle);</code>
    /// <summary>
    /// <para>Performs a texture-to-texture copy.</para>
    /// <para>This copy occurs on the GPU timeline. You may assume the copy has finished
    /// in subsequent commands.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">a source texture region.</param>
    /// <param name="destination">a destination texture region.</param>
    /// <param name="w">the width of the region to copy.</param>
    /// <param name="h">the height of the region to copy.</param>
    /// <param name="d">the depth of the region to copy.</param>
    /// <param name="cycle">if <c>true</c>, cycles the destination texture if the destination
    /// texture is bound, otherwise overwrites the data.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CopyGPUTextureToTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CopyGPUTextureToTexture(IntPtr copyPass, in GPUTextureLocation source, in GPUTextureLocation destination, uint w, uint h, uint d, [MarshalAs(UnmanagedType.I1)] bool cycle);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CopyGPUBufferToBuffer(SDL_GPUCopyPass *copy_pass, const SDL_GPUBufferLocation *source, const SDL_GPUBufferLocation *destination, Uint32 size, bool cycle);</code>
    /// <summary>
    /// <para>Performs a buffer-to-buffer copy.</para>
    /// <para>This copy occurs on the GPU timeline. You may assume the copy has finished
    /// in subsequent commands.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">the buffer and offset to copy from.</param>
    /// <param name="destination">the buffer and offset to copy to.</param>
    /// <param name="size">the length of the buffer to copy.</param>
    /// <param name="cycle">if <c>true</c>, cycles the destination buffer if it is already bound,
    /// otherwise overwrites the data.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CopyGPUBufferToBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CopyGPUBufferToBuffer(IntPtr copyPass, in GPUBufferLocation source, in GPUBufferLocation destination, uint size, [MarshalAs(UnmanagedType.I1)] bool cycle);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DownloadFromGPUTexture(SDL_GPUCopyPass *copy_pass, const SDL_GPUTextureRegion *source, const SDL_GPUTextureTransferInfo *destination);</code>
    /// <summary>
    /// <para>Copies data from a texture to a transfer buffer on the GPU timeline.</para>
    /// <para>This data is not guaranteed to be copied until the command buffer fence is
    /// signaled.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">the source texture region.</param>
    /// <param name="destination">the destination transfer buffer with image layout
    /// information.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DownloadFromGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DownloadFromGPUTexture(IntPtr copyPass, in GPUTextureRegion source, in GPUTextureTransferInfo destination);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DownloadFromGPUBuffer(SDL_GPUCopyPass *copy_pass, const SDL_GPUBufferRegion *source, const SDL_GPUTransferBufferLocation *destination);</code>
    /// <summary>
    /// <para>Copies data from a buffer to a transfer buffer on the GPU timeline.</para>
    /// <para>This data is not guaranteed to be copied until the command buffer fence is
    /// signaled.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">the source buffer with offset and size.</param>
    /// <param name="destination">the destination transfer buffer with offset.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DownloadFromGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DownloadFromGPUBuffer(IntPtr copyPass, in GPUTextureRegion source, in GPUTransferBufferLocation destination);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EndGPUCopyPass(SDL_GPUCopyPass *copy_pass);</code>
    /// <summary>
    /// Ends the current copy pass.
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EndGPUCopyPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void EndGPUCopyPass(IntPtr copyPass);

    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GenerateMipmapsForGPUTexture(SDL_GPUCommandBuffer *command_buffer, SDL_GPUTexture *texture);</code>
    /// <summary>
    /// <para>Generates mipmaps for the given texture.</para>
    /// <para>This function must not be called inside of any pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command_buffer.</param>
    /// <param name="texture">a texture with more than 1 mip level.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GenerateMipmapsForGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GenerateMipmapsForGPUTexture(IntPtr commandBuffer, IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BlitGPUTexture(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUBlitInfo *info);</code>
    /// <summary>
    /// <para>Blits from a source texture region to a destination texture region.</para>
    /// <para>This function must not be called inside of any pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="info">the blit info struct containing the blit parameters.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BlitGPUTexture(IntPtr commandBuffer, in GPUBlitInfo info);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WindowSupportsGPUSwapchainComposition(SDL_GPUDevice *device, SDL_Window *window, SDL_GPUSwapchainComposition swapchain_composition);</code>
    /// <summary>
    /// <para>Determines whether a swapchain composition is supported by the window.</para>
    /// <para>The window must be claimed before calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window.</param>
    /// <param name="swapchainComposition">the swapchain composition to check.</param>
    /// <returns><c>true</c> if supported, <c>false</c> if unsupported.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WindowSupportsGPUSwapchainComposition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WindowSupportsGPUPresentMode(SDL_GPUDevice *device, SDL_Window *window, SDL_GPUPresentMode present_mode);</code>
    /// <summary>
    /// <para>Determines whether a presentation mode is supported by the window.</para>
    /// <para>The window must be claimed before calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window.</param>
    /// <param name="presentMode">the presentation mode to check.</param>
    /// <returns><c>true</c> if supported, <c>false</c> if unsupported.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WindowSupportsGPUPresentMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WindowSupportsGPUPresentMode(IntPtr device, IntPtr window, GPUPresentMode presentMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ClaimWindowForGPUDevice(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// <para>Claims a window, creating a swapchain structure for it.</para>
    /// <para>This must be called before <see cref="AcquireGPUSwapchainTexture"/> is called using
    /// the window. You should only call this function from the thread that created
    /// the window.</para>
    /// <para>The swapchain will be created with <see cref="GPUSwapchainComposition.SDR"/> and
    /// <see cref="GPUPresentMode.VSync"/>. If you want to have different swapchain
    /// parameters, you must call <see cref="SetGPUSwapchainParameters"/> after claiming the
    /// window.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window.</param>
    /// <returns><c>true</c> on success, or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called from the thread that
    /// created the window.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="ReleaseWindowFromGPUDevice"/>
    /// <seealso cref="WindowSupportsGPUPresentMode"/>
    /// <seealso cref="WindowSupportsGPUSwapchainComposition"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClaimWindowForGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClaimWindowForGPUDevice(IntPtr device, IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseWindowFromGPUDevice(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// Unclaims a window, destroying its swapchain structure.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseWindowFromGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseWindowFromGPUDevice(IntPtr device, IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGPUSwapchainParameters(SDL_GPUDevice *device, SDL_Window *window, SDL_GPUSwapchainComposition swapchain_composition, SDL_GPUPresentMode present_mode);</code>
    /// <summary>
    /// <para>Changes the swapchain parameters for the given claimed window.</para>
    /// <para>This function will fail if the requested present mode or swapchain
    /// composition are unsupported by the device. Check if the parameters are
    /// supported via <see cref="WindowSupportsGPUPresentMode"/> /
    /// <see cref="WindowSupportsGPUSwapchainComposition"/> prior to calling this function.</para>
    /// <para><see cref="GPUPresentMode.VSync"/> with <see cref="GPUSwapchainComposition.SDR"/> is always
    /// supported.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <param name="swapchainComposition">the desired composition of the swapchain.</param>
    /// <param name="presentMode">the desired present mode for the swapchain.</param>
    /// <returns><c>true</c> if successful, <c>false</c> on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WindowSupportsGPUPresentMode"/>
    /// <seealso cref="WindowSupportsGPUSwapchainComposition"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUSwapchainParameters"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetGPUSwapchainParameters(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition, GPUPresentMode presentMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGPUAllowedFramesInFlight(SDL_GPUDevice *device, Uint32 allowed_frames_in_flight);</code>
    /// <summary>
    /// <para>Configures the maximum allowed number of frames in flight.</para>
    /// <para>The default value when the device is created is 2. This means that after
    /// you have submitted 2 frames for presentation, if the GPU has not finished
    /// working on the first frame, <see cref="AcquireGPUSwapchainTexture"/> will fill the
    /// swapchain texture pointer with <c>null</c>, and
    /// <see cref="WaitAndAcquireGPUSwapchainTexture"/> will block.</para>
    /// <para>Higher values increase throughput at the expense of visual latency. Lower
    /// values decrease visual latency at the expense of throughput.</para>
    /// <para>Note that calling this function will stall and flush the command queue to
    /// prevent synchronization issues.</para>
    /// <para>The minimum value of allowed frames in flight is 1, and the maximum is 3.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="allowedFramesInFlight">the maximum number of frames that can be
    /// pending on the GPU.</param>
    /// <returns><c>true</c> if successful, <c>false</c> on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.1.8.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUAllowedFramesInFlight"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetGPUAllowedFramesInFlight(IntPtr device, uint allowedFramesInFlight);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUTextureFormat SDLCALL SDL_GetGPUSwapchainTextureFormat(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// <para>Obtains the texture format of the swapchain for the given window.</para>
    /// <para>Note that this format can change if the swapchain parameters change.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <returns>the texture format of the swapchain.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUSwapchainTextureFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GPUTextureFormat GetGPUSwapchainTextureFormat(IntPtr device, IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AcquireGPUSwapchainTexture(SDL_GPUCommandBuffer *command_buffer, SDL_Window *window, SDL_GPUTexture **swapchain_texture, Uint32 *swapchain_texture_width, Uint32 *swapchain_texture_height);</code>
    /// <summary>
    /// <para>Acquire a texture to use in presentation.</para>
    /// <para>When a swapchain texture is acquired on a command buffer, it will
    /// automatically be submitted for presentation when the command buffer is
    /// submitted. The swapchain texture should only be referenced by the command
    /// buffer used to acquire it.</para>
    /// <para>This function will fill the swapchain texture handle with NULL if too many
    /// frames are in flight. This is not an error. This NULL pointer should not be
    /// passed back into SDL. Instead, it should be considered as an indication to
    /// wait until the swapchain is available.</para>
    /// <para>If you use this function, it is possible to create a situation where many
    /// command buffers are allocated while the rendering context waits for the GPU
    /// to catch up, which will cause memory usage to grow. You should use
    /// <see cref="WaitAndAcquireGPUSwapchainTexture"/> unless you know what you are doing
    /// with timing.</para>
    /// <para>The swapchain texture is managed by the implementation and must not be
    /// freed by the user. You MUST NOT call this function from any thread other
    /// than the one that created the window.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="window">a window that has been claimed.</param>
    /// <param name="swapchainTexture">a pointer filled in with a swapchain texture
    /// handle.</param>
    /// <param name="swapchainTextureWidth">a pointer filled in with the swapchain
    /// texture width, may be <c>null</c>.</param>
    /// <param name="swapchainTextureHeight">a pointer filled in with the swapchain
    /// texture height, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success, <c>false</c> on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called from the thread that
    /// created the window.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="CancelGPUCommandBuffer"/>
    /// <seealso cref="GetWindowSizeInPixels"/>
    /// <seealso cref="WaitForGPUSwapchain"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="SetGPUAllowedFramesInFlight"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AcquireGPUSwapchainTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool AcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitForGPUSwapchain(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// <para>Blocks the thread until a swapchain texture is available to be acquired.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">a window that has been claimed.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called from the thread that
    /// created the window.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="SetGPUAllowedFramesInFlight"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUSwapchain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitForGPUSwapchain(IntPtr device, IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitAndAcquireGPUSwapchainTexture(SDL_GPUCommandBuffer *command_buffer, SDL_Window *window, SDL_GPUTexture **swapchain_texture, Uint32 *swapchain_texture_width, Uint32 *swapchain_texture_height);</code>
    /// <summary>
    /// <para>Blocks the thread until a swapchain texture is available to be acquired,
    /// and then acquires it.</para>
    /// <para>When a swapchain texture is acquired on a command buffer, it will
    /// automatically be submitted for presentation when the command buffer is
    /// submitted. The swapchain texture should only be referenced by the command
    /// buffer used to acquire it. It is an error to call
    /// <see cref="CancelGPUCommandBuffer"/> after a swapchain texture is acquired.</para>
    /// <para>This function can fill the swapchain texture handle with <c>null</c> in certain
    /// cases, for example if the window is minimized. This is not an error. You
    /// should always make sure to check whether the pointer is <c>null</c> before
    /// actually using it.</para>
    /// <para>The swapchain texture is managed by the implementation and must not be
    /// freed by the user. You MUST NOT call this function from any thread other
    /// than the one that created the window.</para>
    /// <para>The swapchain texture is write-only and cannot be used as a sampler or for
    /// another reading operation.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="window">a window that has been claimed.</param>
    /// <param name="swapchainTexture">a pointer filled in with a swapchain texture
    /// handle.</param>
    /// <param name="swapchainTextureWidth">a pointer filled in with the swapchain
    /// texture width, may be <c>null</c>.</param>
    /// <param name="swapchainTextureHeight">a pointer filled in with the swapchain
    /// texture height, may be <c>null</c>.</param>
    /// <returns><c>true</c> on success, <c>false</c> on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called from the thread that
    /// created the window.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitAndAcquireGPUSwapchainTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitAndAcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SubmitGPUCommandBuffer(SDL_GPUCommandBuffer *command_buffer);</code>
    /// <summary>
    /// <para>Submits a command buffer so its commands can be processed on the GPU.</para>
    /// <para>It is invalid to use the command buffer after this is called.</para>
    /// <para>This must be called from the thread the command buffer was acquired on.</para>
    /// <para>All commands in the submission are guaranteed to begin executing before any
    /// command in a subsequent submission begins executing.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AcquireGPUCommandBuffer"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SubmitGPUCommandBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SubmitGPUCommandBuffer(IntPtr commandBuffer);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUFence *SDLCALL SDL_SubmitGPUCommandBufferAndAcquireFence(SDL_GPUCommandBuffer *command_buffer);</code>
    /// <summary>
    /// <para>Submits a command buffer so its commands can be processed on the GPU, and
    /// acquires a fence associated with the command buffer.</para>
    /// <para>You must release this fence when it is no longer needed or it will cause a
    /// leak. It is invalid to use the command buffer after this is called.</para>
    /// <para>This must be called from the thread the command buffer was acquired on.</para>
    /// <para>All commands in the submission are guaranteed to begin executing before any
    /// command in a subsequent submission begins executing.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <returns>a fence associated with the command buffer, or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AcquireGPUCommandBuffer"/>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    /// <seealso cref="SubmitGPUCommandBuffer"/>
    /// <seealso cref="ReleaseGPUFence"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SubmitGPUCommandBufferAndAcquireFence"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr SubmitGPUCommandBufferAndAcquireFence(IntPtr commandBuffer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CancelGPUCommandBuffer(SDL_GPUCommandBuffer *command_buffer);</code>
    /// <summary>
    /// <para>Cancels a command buffer.</para>
    /// <para>None of the enqueued commands are executed.</para>
    /// <para>It is an error to call this function after a swapchain texture has been
    /// acquired.</para>
    /// <para>This must be called from the thread the command buffer was acquired on.</para>
    /// <para>You must not reference the command buffer after calling this function.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <returns><c>true</c> on success, <c>false</c> on error; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.1.6.</since>
    /// <seealso cref="WaitAndAcquireGPUSwapchainTexture"/>
    /// <seealso cref="AcquireGPUCommandBuffer"/>
    /// <seealso cref="AcquireGPUSwapchainTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CancelGPUCommandBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CancelGPUCommandBuffer(IntPtr commandBuffer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitForGPUIdle(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Blocks the thread until the GPU is completely idle.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WaitForGPUFences(nint, bool, nint[], uint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUIdle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitForGPUIdle(IntPtr device);
    
    
    #region WaitForGPUFences
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitForGPUFences(SDL_GPUDevice *device, bool wait_all, SDL_GPUFence *const *fences, Uint32 num_fences);</code>
    /// <summary>
    /// Blocks the thread until the given fences are signaled.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="waitAll">if 0, wait for any fence to be signaled, if 1, wait for all
    /// fences to be signaled.</param>
    /// <param name="fences">an array of fences to wait on.</param>
    /// <param name="numFences">the number of fences in the fences array.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="WaitForGPUIdle"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUFences"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, IntPtr[] fences, uint numFences);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitForGPUFences(SDL_GPUDevice *device, bool wait_all, SDL_GPUFence *const *fences, Uint32 num_fences);</code>
    /// <summary>
    /// Blocks the thread until the given fences are signaled.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="waitAll">if 0, wait for any fence to be signaled, if 1, wait for all
    /// fences to be signaled.</param>
    /// <param name="fences">an array of fences to wait on.</param>
    /// <param name="numFences">the number of fences in the fences array.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    /// <seealso cref="WaitForGPUIdle"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUFences"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, IntPtr fences, uint numFences);
    #endregion
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_QueryGPUFence(SDL_GPUDevice *device, SDL_GPUFence *fence);</code>
    /// <summary>
    /// Checks the status of a fence.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="fence">a fence.</param>
    /// <returns><c>true</c> if the fence is signaled, <c>false</c> if it is not.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_QueryGPUFence"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool QueryGPUFence(IntPtr device, IntPtr fence);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUFence(SDL_GPUDevice *device, SDL_GPUFence *fence);</code>
    /// <summary>
    /// Releases a fence obtained from <see cref="SubmitGPUCommandBufferAndAcquireFence"/>.
    /// <para>You must not reference the fence after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="fence">a fence.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUFence"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseGPUFence(IntPtr device, IntPtr fence);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GPUTextureFormatTexelBlockSize(SDL_GPUTextureFormat format);</code>
    /// <summary>
    /// Obtains the texel block size for a texture format.
    /// </summary>
    /// <param name="format">the texture format you want to know the texel size of.</param>
    /// <returns>the texel block size of the texture format.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUTextureFormatTexelBlockSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GPUTextureFormatTexelBlockSize(GPUTextureFormat format);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GPUTextureSupportsFormat(SDL_GPUDevice *device, SDL_GPUTextureFormat format, SDL_GPUTextureType type, SDL_GPUTextureUsageFlags usage);</code>
    /// <summary>
    /// <para>Determines whether a texture format is supported for a given type and
    /// usage.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="format">the texture format to check.</param>
    /// <param name="type">the type of texture (2D, 3D, Cube).</param>
    /// <param name="usage">a bitmask of all usage scenarios to check.</param>
    /// <returns>whether the texture format is supported for this type and usage.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUTextureSupportsFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GPUTextureSupportsFormat(IntPtr device, GPUTextureFormat format, GPUTextureType type, GPUTextureUsageFlags usage);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GPUTextureSupportsSampleCount(SDL_GPUDevice *device, SDL_GPUTextureFormat format, SDL_GPUSampleCount sample_count);</code>
    /// <summary>
    /// Determines if a sample count for a texture format is supported.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="format">the texture format to check.</param>
    /// <param name="sampleСount">the sample count to check.</param>
    /// <returns>whether the sample count is supported for this texture format.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUTextureSupportsSampleCount"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GPUTextureSupportsSampleCount(IntPtr device, GPUTextureFormat format, GPUSampleCount sampleСount);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_CalculateGPUTextureFormatSize(SDL_GPUTextureFormat format, Uint32 width, Uint32 height, Uint32 depth_or_layer_count);</code>
    /// <summary>
    /// Calculate the size in bytes of a texture format with dimensions.
    /// </summary>
    /// <param name="format">a texture format.</param>
    /// <param name="width">width in pixels.</param>
    /// <param name="height">height in pixels.</param>
    /// <param name="depthOrLayerCount">depth for 3D textures or layer count otherwise.</param>
    /// <returns>the size of a texture with this format and dimensions.</returns>
    /// <since>This function is available since SDL 3.1.6.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CalculateGPUTextureFormatSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint CalculateGPUTextureFormatSize(GPUTextureFormat format, uint width, uint height, uint depthOrLayerCount);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GDKSuspendGPU(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Call this to suspend GPU operation on Xbox when you receive the
    /// <see cref="EventType.DidEnterBackground"/> event.</para>
    /// <para>Do NOT call any SDL_GPU functions after calling this function! This must
    /// also be called before calling <see cref="GDKSuspendComplete"/>.</para>
    /// </summary>
    /// <param name="device">device a GPU context.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddEventWatch"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GDKSuspendGPU"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GDKSuspendGPU(IntPtr device);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GDKResumeGPU(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Call this to resume GPU operation on Xbox when you receive the
    /// <see cref="EventType.WillEnterForeground"/> event.</para>
    /// <para>When resuming, this function MUST be called before calling any other
    /// SDL_GPU functions.</para>
    /// </summary>
    /// <param name="device">device a GPU context.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddEventWatch"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GDKResumeGPU"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GDKResumeGPU(IntPtr device);
}
