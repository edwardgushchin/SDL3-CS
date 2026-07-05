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

public partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUSupportsShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GPUSupportsShaderFormats(GPUShaderFormat formatFlags, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name);
    private delegate bool GPUSupportsShaderFormatsNativeDelegate(GPUShaderFormat formatFlags, string? name);
    private static GPUSupportsShaderFormatsNativeDelegate GPUSupportsShaderFormatsNativeFunction = SDL_GPUSupportsShaderFormats;

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
    public static bool GPUSupportsShaderFormats(GPUShaderFormat formatFlags, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name)
    {
        return GPUSupportsShaderFormatsNativeFunction(formatFlags, name);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUSupportsProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GPUSupportsProperties(uint props);
    private delegate bool GPUSupportsPropertiesNativeDelegate(uint props);
    private static GPUSupportsPropertiesNativeDelegate GPUSupportsPropertiesNativeFunction = SDL_GPUSupportsProperties;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GPUSupportsProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// Checks for GPU runtime support.
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns><c>true</c> if supported, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    public static bool GPUSupportsProperties(uint props)
    {
        return GPUSupportsPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUDevice(GPUShaderFormat formatFlags, [MarshalAs(UnmanagedType.I1)] bool debugMode, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name);
    private delegate IntPtr CreateGPUDeviceNativeDelegate(GPUShaderFormat formatFlags, bool debugMode, string? name);
    private static CreateGPUDeviceNativeDelegate CreateGPUDeviceNativeFunction = SDL_CreateGPUDevice;

    /// <code>extern SDL_DECLSPEC SDL_GPUDevice *SDLCALL SDL_CreateGPUDevice(SDL_GPUShaderFormat format_flags, bool debug_mode, const char *name);</code>
    /// <summary>
    /// Creates a GPU context.
    /// <para>The GPU driver name can be one of the following:</para>
    /// <list type="bullet">
    /// <item><c>vulkan</c>: [Vulkan](CategoryGPU#vulkan)</item>
    /// <item><c>direct3d12</c>: [D3D12](CategoryGPU#d3d12)</item>
    /// <item><c>metal</c>: [Metal](CategoryGPU#metal)</item>
    /// <item><c>null</c>: let SDL pick the optimal driver</item>
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
    public static IntPtr CreateGPUDevice(GPUShaderFormat formatFlags, [MarshalAs(UnmanagedType.I1)] bool debugMode, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name)
    {
        return CreateGPUDeviceNativeFunction(formatFlags, debugMode, name);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUDeviceWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUDeviceWithProperties(uint props);
    private delegate IntPtr CreateGPUDeviceWithPropertiesNativeDelegate(uint props);
    private static CreateGPUDeviceWithPropertiesNativeDelegate CreateGPUDeviceWithPropertiesNativeFunction = SDL_CreateGPUDeviceWithProperties;

    /// <code>extern SDL_DECLSPEC SDL_GPUDevice *SDLCALL SDL_CreateGPUDeviceWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Creates a GPU context.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateDebugModeBoolean"/>: enable debug mode
    /// properties and validations, defaults to <c>true</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreatePreferLowPowerBoolean"/>: enable to prefer
    /// energy efficiency over maximum GPU performance, defaults to <c>false</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateVerboseBoolean"/>: enable to automatically log
    /// useful debug information on device creation, defaults to <c>true</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateNameString"/>: the name of the GPU driver to
    /// use, if a specific one is desired.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureClipDistanceBoolean"/>: Enable Vulkan
    /// device feature shaderClipDistance. If disabled, clip distances are not
    /// supported in shader code: gl_ClipDistance[] built-ins of GLSL,
    /// SV_ClipDistance0/1 semantics of HLSL and [[clip_distance]] attribute of
    /// Metal. Disabling optional features allows the application to run on some
    /// older Android devices. Defaults to <c>true</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureDepthClampingBoolean"/>: Enable
    /// Vulkan device feature depthClamp. If disabled, there is no depth clamp
    /// support and enable_depth_clip in <see cref="GPURasterizerState"/> must always be
    /// set to <c>true</c>. Disabling optional features allows the application to run on
    /// some older Android devices. Defaults to <c>true</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureIndirectDrawFirstInstanceBoolean"/>:
    /// Enable Vulkan device feature drawIndirectFirstInstance. If disabled, the
    /// argument first_instance of <seealso cref="GPUIndirectDrawCommand"/> must be set to
    /// zero. Disabling optional features allows the application to run on some
    /// older Android devices. Defaults to <c>true</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateFeatureAnisotropyBoolean"/>: Enable Vulkan
    /// device feature samplerAnisotropy. If disabled, enable_anisotropy of
    /// <see cref="GPUSamplerCreateInfo"/> must be set to <c>false</c>. Disabling optional
    /// features allows the application to run on some older Android devices.
    /// Defaults to <c>true</c>.</item>
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
    /// <para>With the D3D12 backend:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateD3D12SemanticNameString"/>: the prefix to
    /// use for all vertex semantics, default is "TEXCOORD".</item>
    /// <item><see cref="Props.GPUDeviceCreateD3D12AllowFewerResourceSlotsBoolean"/>: By
    /// default, Resourcing Binding Tier 2 is required for D3D12 support.
    /// However, an application can set this property to <c>true</c> to enable Tier 1
    /// support, if (and only if) the application uses 8 or fewer storage
    /// resources across all shader stages. As of writing, this property is
    /// useful for targeting Intel Haswell and Broadwell GPUs; other hardware
    /// either supports Tier 2 Resource Binding or does not support D3D12 in any
    /// capacity. Defaults to <c>false</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateD3D12AgilitySDKVersionNumber"/>: Certain
    /// feature checks are only possible on Windows 11 by default. By setting
    /// this alongside `SDL_PROP_GPU_DEVICE_CREATE_D3D12_AGILITY_SDK_PATH_STRING`
    /// and vendoring D3D12Core.dll from the D3D12 Agility SDK, you can make
    /// those feature checks possible on older platforms. The version you provide
    /// must match the one given in the DLL.</item>
    /// <item><see cref="Props.GPUDeviceCreateD3D12AgilitySDKPathString"/>: Certain
    /// feature checks are only possible on Windows 11 by default. By setting
    /// this alongside
    /// <see cref="Props.GPUDeviceCreateD3D12AgilitySDKVersionNumber"/> and
    /// vendoring D3D12Core.dll from the D3D12 Agility SDK, you can make those
    /// feature checks possible on older platforms. The path you provide must be
    /// relative to the executable path of your app. Be sure not to put the DLL
    /// in the same directory as the exe; Microsoft strongly advises against
    /// this!</item>
    /// </list>
    /// <para>With the Vulkan backend:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateVulkanRequireHardwareAccelerationBoolean"/>:
    /// By default, Vulkan device enumeration includes drivers of all types,
    /// including software renderers (for example, the Lavapipe Mesa driver).
    /// This can be useful if your application _requires_ SDL_GPU, but if you can
    /// provide your own fallback renderer (for example, an OpenGL renderer) this
    /// property can be set to <c>true</c>. Defaults to <c>false</c>.</item>
    /// <item><see cref="Props.GPUDeviceCreateVulkanOptionsPointer"/>: a pointer to an
    /// <see cref="GPUVulkanOptions"/> structure to be processed during device creation.
    /// This allows configuring a variety of Vulkan-specific options such as
    /// increasing the API version and opting into extensions aside from the
    /// minimal set SDL requires.</item>
    /// </list>
    /// <para>With the Metal backend:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.GPUDeviceCreateMetalAllowMacFamily1Boolean"/>: By default,
    /// macOS support requires what Apple calls <c>"MTLGPUFamilyMac2"</c> hardware or
    /// newer. However, an application can set this property to <c>true</c> to enable
    /// support for <c>"MTLGPUFamilyMac1"</c> hardware, if (and only if) the application
    /// does not write to sRGB textures. (For history's sake: MacFamily1 also does
    /// not support indirect command buffers, MSAA depth resolve, and stencil
    /// resolve/feedback, but these are not exposed features in SDL_GPU.)</item>
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
    public static IntPtr CreateGPUDeviceWithProperties(uint props)
    {
        return CreateGPUDeviceWithPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyGPUDevice(IntPtr device);
    private delegate void DestroyGPUDeviceNativeDelegate(IntPtr device);
    private static DestroyGPUDeviceNativeDelegate DestroyGPUDeviceNativeFunction = SDL_DestroyGPUDevice;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyGPUDevice(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Destroys a GPU context previously returned by SDL_CreateGPUDevice.
    /// </summary>
    /// <param name="device">a GPU Context to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUDevice"/>
    public static void DestroyGPUDevice(IntPtr device)
    {
        DestroyGPUDeviceNativeFunction(device);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumGPUDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumGPUDrivers();
    private delegate int GetNumGPUDriversNativeDelegate();
    private static GetNumGPUDriversNativeDelegate GetNumGPUDriversNativeFunction = SDL_GetNumGPUDrivers;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumGPUDrivers(void);</code>
    /// <summary>
    /// Get the number of GPU drivers compiled into SDL.
    /// </summary>
    /// <returns>the number of built in GPU drivers.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetGPUDriver"/>
    public static int GetNumGPUDrivers()
    {
        return GetNumGPUDriversNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGPUDriver(int index);
    private delegate IntPtr GetGPUDriverNativeDelegate(int index);
    private static GetGPUDriverNativeDelegate GetGPUDriverNativeFunction = SDL_GetGPUDriver;
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
        var value = GetGPUDriverNativeFunction(index);
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUDeviceDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGPUDeviceDriver(IntPtr device);
    private delegate IntPtr GetGPUDeviceDriverNativeDelegate(IntPtr device);
    private static GetGPUDeviceDriverNativeDelegate GetGPUDeviceDriverNativeFunction = SDL_GetGPUDeviceDriver;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetGPUDeviceDriver(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Returns the name of the backend used to create this GPU context.
    /// </summary>
    /// <param name="device">a GPU context to query.</param>
    /// <returns>the name of the device's driver, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetGPUDeviceDriver(IntPtr device)
    {
        var value = GetGPUDeviceDriverNativeFunction(device);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUShaderFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GPUShaderFormat SDL_GetGPUShaderFormats(IntPtr device);
    private delegate GPUShaderFormat GetGPUShaderFormatsNativeDelegate(IntPtr device);
    private static GetGPUShaderFormatsNativeDelegate GetGPUShaderFormatsNativeFunction = SDL_GetGPUShaderFormats;

    /// <code>extern SDL_DECLSPEC SDL_GPUShaderFormat SDLCALL SDL_GetGPUShaderFormats(SDL_GPUDevice *device);</code>
    /// <summary>
    /// Returns the supported shader formats for this GPU context.
    /// </summary>
    /// <param name="device">a GPU context to query.</param>
    /// <returns>a bitflag indicating which shader formats the driver is able to
    /// consume.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static GPUShaderFormat GetGPUShaderFormats(IntPtr device)
    {
        return GetGPUShaderFormatsNativeFunction(device);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUDeviceProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGPUDeviceProperties(IntPtr device);
    private delegate uint GetGPUDevicePropertiesNativeDelegate(IntPtr device);
    private static GetGPUDevicePropertiesNativeDelegate GetGPUDevicePropertiesNativeFunction = SDL_GetGPUDeviceProperties;

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
    public static uint GetGPUDeviceProperties(IntPtr device)
    {
        return GetGPUDevicePropertiesNativeFunction(device);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUComputePipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUComputePipeline(IntPtr device, in GPUComputePipelineCreateInfo createinfo);
    private delegate IntPtr CreateGPUComputePipelineNativeDelegate(IntPtr device, in GPUComputePipelineCreateInfo createinfo);
    private static CreateGPUComputePipelineNativeDelegate CreateGPUComputePipelineNativeFunction = SDL_CreateGPUComputePipeline;

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
    public static IntPtr CreateGPUComputePipeline(IntPtr device, in GPUComputePipelineCreateInfo createinfo)
    {
        return CreateGPUComputePipelineNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUGraphicsPipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUGraphicsPipeline(IntPtr device, in GPUGraphicsPipelineCreateInfo createinfo);
    private delegate IntPtr CreateGPUGraphicsPipelineNativeDelegate(IntPtr device, in GPUGraphicsPipelineCreateInfo createinfo);
    private static CreateGPUGraphicsPipelineNativeDelegate CreateGPUGraphicsPipelineNativeFunction = SDL_CreateGPUGraphicsPipeline;

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
    public static IntPtr CreateGPUGraphicsPipeline(IntPtr device, in GPUGraphicsPipelineCreateInfo createinfo)
    {
        return CreateGPUGraphicsPipelineNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUSampler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUSampler(IntPtr device, in GPUSamplerCreateInfo createinfo);
    private delegate IntPtr CreateGPUSamplerNativeDelegate(IntPtr device, in GPUSamplerCreateInfo createinfo);
    private static CreateGPUSamplerNativeDelegate CreateGPUSamplerNativeFunction = SDL_CreateGPUSampler;

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
    public static IntPtr CreateGPUSampler(IntPtr device, in GPUSamplerCreateInfo createinfo)
    {
        return CreateGPUSamplerNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUShader"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUShader(IntPtr device, in GPUShaderCreateInfo createinfo);
    private delegate IntPtr CreateGPUShaderNativeDelegate(IntPtr device, in GPUShaderCreateInfo createinfo);
    private static CreateGPUShaderNativeDelegate CreateGPUShaderNativeFunction = SDL_CreateGPUShader;

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
    public static IntPtr CreateGPUShader(IntPtr device, in GPUShaderCreateInfo createinfo)
    {
        return CreateGPUShaderNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUTexture(IntPtr device, in GPUTextureCreateInfo createinfo);
    private delegate IntPtr CreateGPUTextureNativeDelegate(IntPtr device, in GPUTextureCreateInfo createinfo);
    private static CreateGPUTextureNativeDelegate CreateGPUTextureNativeFunction = SDL_CreateGPUTexture;

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
    /// <seealso cref="BeginGPUComputePass(nint, GPUStorageTextureReadWriteBinding[], uint, GPUStorageBufferReadWriteBinding[], uint)"/>
    /// <seealso cref="BindGPUVertexSamplers(nint, uint, GPUTextureSamplerBinding[], uint)"/>
    /// <seealso cref="BindGPUVertexStorageTextures(nint, uint, nint[], uint)"/>
    /// <seealso cref="BindGPUFragmentSamplers(nint, uint, nint, uint)"/>
    /// <seealso cref="BindGPUFragmentStorageTextures(nint, uint, nint[], uint)"/>
    /// <seealso cref="BindGPUComputeStorageTextures(nint, uint, nint[], uint)"/>
    /// <seealso cref="BlitGPUTexture"/>
    /// <seealso cref="ReleaseGPUTexture"/>
    /// <seealso cref="GPUTextureSupportsFormat"/>
    public static IntPtr CreateGPUTexture(IntPtr device, in GPUTextureCreateInfo createinfo)
    {
        return CreateGPUTextureNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUBuffer(IntPtr device, in GPUBufferCreateInfo createinfo);
    private delegate IntPtr CreateGPUBufferNativeDelegate(IntPtr device, in GPUBufferCreateInfo createinfo);
    private static CreateGPUBufferNativeDelegate CreateGPUBufferNativeFunction = SDL_CreateGPUBuffer;

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
    /// <seealso cref="BindGPUVertexStorageBuffers(nint, uint, nint[], uint)"/>
    /// <seealso cref="BindGPUFragmentStorageBuffers(nint, uint, nint[], uint)"/>
    /// <seealso cref="DrawGPUPrimitivesIndirect"/>
    /// <seealso cref="DrawGPUIndexedPrimitivesIndirect"/>
    /// <seealso cref="BindGPUComputeStorageBuffers(nint, uint, nint[], uint)"/>
    /// <seealso cref="DispatchGPUComputeIndirect"/>
    /// <seealso cref="ReleaseGPUBuffer"/>
    public static IntPtr CreateGPUBuffer(IntPtr device, in GPUBufferCreateInfo createinfo)
    {
        return CreateGPUBufferNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateGPUTransferBuffer(IntPtr device, in GPUTransferBufferCreateInfo createinfo);
    private delegate IntPtr CreateGPUTransferBufferNativeDelegate(IntPtr device, in GPUTransferBufferCreateInfo createinfo);
    private static CreateGPUTransferBufferNativeDelegate CreateGPUTransferBufferNativeFunction = SDL_CreateGPUTransferBuffer;

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
    public static IntPtr CreateGPUTransferBuffer(IntPtr device, in GPUTransferBufferCreateInfo createinfo)
    {
        return CreateGPUTransferBufferNativeFunction(device, in createinfo);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUBufferName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    private delegate void SetGPUBufferNameNativeDelegate(IntPtr device, IntPtr buffer, string text);
    private static SetGPUBufferNameNativeDelegate SetGPUBufferNameNativeFunction = SDL_SetGPUBufferName;

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
    public static void SetGPUBufferName(IntPtr device, IntPtr buffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text)
    {
        SetGPUBufferNameNativeFunction(device, buffer, text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUTextureName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUTextureName(IntPtr device, IntPtr texture, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    private delegate void SetGPUTextureNameNativeDelegate(IntPtr device, IntPtr texture, string text);
    private static SetGPUTextureNameNativeDelegate SetGPUTextureNameNativeFunction = SDL_SetGPUTextureName;

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
    public static void SetGPUTextureName(IntPtr device, IntPtr texture, [MarshalAs(UnmanagedType.LPUTF8Str)] string text)
    {
        SetGPUTextureNameNativeFunction(device, texture, text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_InsertGPUDebugLabel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_InsertGPUDebugLabel(IntPtr commandBuffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    private delegate void InsertGPUDebugLabelNativeDelegate(IntPtr commandBuffer, string text);
    private static InsertGPUDebugLabelNativeDelegate InsertGPUDebugLabelNativeFunction = SDL_InsertGPUDebugLabel;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_InsertGPUDebugLabel(SDL_GPUCommandBuffer *command_buffer, const char *text);</code>
    /// <summary>
    /// <para>Inserts an arbitrary string label into the command buffer callstream.</para>
    /// <para>Useful for debugging.</para>
    /// <para>On Direct3D 12, using <see cref="InsertGPUDebugLabel"/> requires
    /// WinPixEventRuntime.dll to be in your PATH or in the same directory as your
    /// executable. See
    /// [here](https://devblogs.microsoft.com/pix/winpixeventruntime/)
    /// for instructions on how to obtain it.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="text">a UTF-8 string constant to insert as the label.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void InsertGPUDebugLabel(IntPtr commandBuffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text)
    {
        InsertGPUDebugLabelNativeFunction(commandBuffer, text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUDebugGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUDebugGroup(IntPtr commandBuffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    private delegate void PushGPUDebugGroupNativeDelegate(IntPtr commandBuffer, string text);
    private static PushGPUDebugGroupNativeDelegate PushGPUDebugGroupNativeFunction = SDL_PushGPUDebugGroup;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUDebugGroup(SDL_GPUCommandBuffer *command_buffer, const char *name);</code>
    /// <summary>
    /// <para>Begins a debug group with an arbitrary name.</para>
    /// <para>Used for denoting groups of calls when viewing the command buffer
    /// callstream in a graphics debugging tool.</para>
    /// <para>Each call to <see cref="PushGPUDebugGroup"/> must have a corresponding call to
    /// <see cref="PopGPUDebugGroup"/>.</para>
    /// <para>On Direct3D 12, using <see cref="PushGPUDebugGroup"/> requires WinPixEventRuntime.dll
    /// to be in your PATH or in the same directory as your executable. See
    /// [here](https://devblogs.microsoft.com/pix/winpixeventruntime/)
    /// for instructions on how to obtain it.</para>
    /// <para>On some backends (e.g. Metal), pushing a debug group during a
    /// render/blit/compute pass will create a group that is scoped to the native
    /// pass rather than the command buffer. For best results, if you push a debug
    /// group during a pass, always pop it in the same pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="text">a UTF-8 string constant that names the group.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PopGPUDebugGroup"/>
    public static void PushGPUDebugGroup(IntPtr commandBuffer, [MarshalAs(UnmanagedType.LPUTF8Str)] string text)
    {
        PushGPUDebugGroupNativeFunction(commandBuffer, text);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PopGPUDebugGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PopGPUDebugGroup(IntPtr commandBuffer);
    private delegate void PopGPUDebugGroupNativeDelegate(IntPtr commandBuffer);
    private static PopGPUDebugGroupNativeDelegate PopGPUDebugGroupNativeFunction = SDL_PopGPUDebugGroup;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PopGPUDebugGroup(SDL_GPUCommandBuffer *command_buffer);</code>
    /// <summary>
    /// Ends the most-recently pushed debug group.
    /// <para>On Direct3D 12, using <see cref="PopGPUDebugGroup"/> requires WinPixEventRuntime.dll
    /// to be in your PATH or in the same directory as your executable. See
    /// [here](https://devblogs.microsoft.com/pix/winpixeventruntime/)
    /// for instructions on how to obtain it.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PushGPUDebugGroup"/>
    public static void PopGPUDebugGroup(IntPtr commandBuffer)
    {
        PopGPUDebugGroupNativeFunction(commandBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUTexture(IntPtr device, IntPtr texture);
    private delegate void ReleaseGPUTextureNativeDelegate(IntPtr device, IntPtr texture);
    private static ReleaseGPUTextureNativeDelegate ReleaseGPUTextureNativeFunction = SDL_ReleaseGPUTexture;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUTexture(SDL_GPUDevice *device, SDL_GPUTexture *texture);</code>
    /// <summary>
    /// <para>Frees the given texture as soon as it is safe to do so.</para>
    /// <para>You must not reference the texture after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="texture">a texture to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUTexture(IntPtr device, IntPtr texture)
    {
        ReleaseGPUTextureNativeFunction(device, texture);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUSampler"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUSampler(IntPtr device, IntPtr sampler);
    private delegate void ReleaseGPUSamplerNativeDelegate(IntPtr device, IntPtr sampler);
    private static ReleaseGPUSamplerNativeDelegate ReleaseGPUSamplerNativeFunction = SDL_ReleaseGPUSampler;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUSampler(SDL_GPUDevice *device, SDL_GPUSampler *sampler);</code>
    /// <summary>
    /// <para>Frees the given sampler as soon as it is safe to do so.</para>
    /// <para>You must not reference the sampler after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="sampler">a sampler to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUSampler(IntPtr device, IntPtr sampler)
    {
        ReleaseGPUSamplerNativeFunction(device, sampler);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUBuffer(IntPtr device, IntPtr buffer);
    private delegate void ReleaseGPUBufferNativeDelegate(IntPtr device, IntPtr buffer);
    private static ReleaseGPUBufferNativeDelegate ReleaseGPUBufferNativeFunction = SDL_ReleaseGPUBuffer;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUBuffer(SDL_GPUDevice *device, SDL_GPUBuffer *buffer);</code>
    /// <summary>
    /// <para>Frees the given buffer as soon as it is safe to do so.</para>
    /// <para>You must not reference the buffer after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="buffer">a buffer to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUBuffer(IntPtr device, IntPtr buffer)
    {
        ReleaseGPUBufferNativeFunction(device, buffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUTransferBuffer(IntPtr device, IntPtr transferBuffer);
    private delegate void ReleaseGPUTransferBufferNativeDelegate(IntPtr device, IntPtr transferBuffer);
    private static ReleaseGPUTransferBufferNativeDelegate ReleaseGPUTransferBufferNativeFunction = SDL_ReleaseGPUTransferBuffer;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUTransferBuffer(SDL_GPUDevice *device, SDL_GPUTransferBuffer *transfer_buffer);</code>
    /// <summary>
    /// <para>Frees the given transfer buffer as soon as it is safe to do so.</para>
    /// <para>You must not reference the transfer buffer after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="transferBuffer">a transfer buffer to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUTransferBuffer(IntPtr device, IntPtr transferBuffer)
    {
        ReleaseGPUTransferBufferNativeFunction(device, transferBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUComputePipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUComputePipeline(IntPtr device, IntPtr computePipeline);
    private delegate void ReleaseGPUComputePipelineNativeDelegate(IntPtr device, IntPtr computePipeline);
    private static ReleaseGPUComputePipelineNativeDelegate ReleaseGPUComputePipelineNativeFunction = SDL_ReleaseGPUComputePipeline;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUComputePipeline(SDL_GPUDevice *device, SDL_GPUComputePipeline *compute_pipeline);</code>
    /// <summary>
    /// <para>Frees the given compute pipeline as soon as it is safe to do so.</para>
    /// <para>You must not reference the compute pipeline after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="computePipeline">a compute pipeline to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUComputePipeline(IntPtr device, IntPtr computePipeline)
    {
        ReleaseGPUComputePipelineNativeFunction(device, computePipeline);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUShader"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUShader(IntPtr device, IntPtr shader);
    private delegate void ReleaseGPUShaderNativeDelegate(IntPtr device, IntPtr shader);
    private static ReleaseGPUShaderNativeDelegate ReleaseGPUShaderNativeFunction = SDL_ReleaseGPUShader;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUShader(SDL_GPUDevice *device, SDL_GPUShader *shader);</code>
    /// <summary>
    /// <para>Frees the given shader as soon as it is safe to do so.</para>
    /// <para>You must not reference the shader after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="shader">a shader to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUShader(IntPtr device, IntPtr shader)
    {
        ReleaseGPUShaderNativeFunction(device, shader);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUGraphicsPipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUGraphicsPipeline(IntPtr device, IntPtr graphicsPipeline);
    private delegate void ReleaseGPUGraphicsPipelineNativeDelegate(IntPtr device, IntPtr graphicsPipeline);
    private static ReleaseGPUGraphicsPipelineNativeDelegate ReleaseGPUGraphicsPipelineNativeFunction = SDL_ReleaseGPUGraphicsPipeline;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUGraphicsPipeline(SDL_GPUDevice *device, SDL_GPUGraphicsPipeline *graphics_pipeline);</code>
    /// <summary>
    /// <para>Frees the given graphics pipeline as soon as it is safe to do so.</para>
    /// <para>You must not reference the graphics pipeline after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="graphicsPipeline">a graphics pipeline to be destroyed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void ReleaseGPUGraphicsPipeline(IntPtr device, IntPtr graphicsPipeline)
    {
        ReleaseGPUGraphicsPipelineNativeFunction(device, graphicsPipeline);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AcquireGPUCommandBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_AcquireGPUCommandBuffer(IntPtr device);
    private delegate IntPtr AcquireGPUCommandBufferNativeDelegate(IntPtr device);
    private static AcquireGPUCommandBufferNativeDelegate AcquireGPUCommandBufferNativeFunction = SDL_AcquireGPUCommandBuffer;

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
    public static IntPtr AcquireGPUCommandBuffer(IntPtr device)
    {
        return AcquireGPUCommandBufferNativeFunction(device);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUVertexUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    private delegate void PushGPUVertexUniformDataPointerNativeDelegate(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    private static PushGPUVertexUniformDataPointerNativeDelegate PushGPUVertexUniformDataPointerNativeFunction = SDL_PushGPUVertexUniformData;

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
    public static void PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        PushGPUVertexUniformDataPointerNativeFunction(commandBuffer, slotIndex, data, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUVertexUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length);
    private delegate void PushGPUVertexUniformDataArrayNativeDelegate(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length);
    private static PushGPUVertexUniformDataArrayNativeDelegate PushGPUVertexUniformDataArrayNativeFunction = SDL_PushGPUVertexUniformData;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUVertexUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a vertex uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls in this command buffer will use this uniform data.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the vertex uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length)
    {
        PushGPUVertexUniformDataArrayNativeFunction(commandBuffer, slotIndex, data, length);
    }

    /// <inheritdoc cref="PushGPUVertexUniformData(nint, uint, byte[], uint)"/>
    public static unsafe void PushGPUVertexUniformData(IntPtr commandBuffer, uint slotIndex, ReadOnlySpan<byte> data, uint length)
    {
        fixed (byte* pData = data)
        {
            PushGPUVertexUniformData(commandBuffer, slotIndex, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUFragmentUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    private delegate void PushGPUFragmentUniformDataPointerNativeDelegate(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    private static PushGPUFragmentUniformDataPointerNativeDelegate PushGPUFragmentUniformDataPointerNativeFunction = SDL_PushGPUFragmentUniformData;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUFragmentUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a fragment uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls in this command buffer will use this uniform data.</para>
    /// <para>The data being pushed must respect std140 layout conventions. In practical
    /// terms this means you must ensure that vec3 and vec4 fields are 16-byte
    /// aligned.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the fragment uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        PushGPUFragmentUniformDataPointerNativeFunction(commandBuffer, slotIndex, data, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUFragmentUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length);
    private delegate void PushGPUFragmentUniformDataArrayNativeDelegate(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length);
    private static PushGPUFragmentUniformDataArrayNativeDelegate PushGPUFragmentUniformDataArrayNativeFunction = SDL_PushGPUFragmentUniformData;

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
    public static void PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length)
    {
        PushGPUFragmentUniformDataArrayNativeFunction(commandBuffer, slotIndex, data, length);
    }

    /// <inheritdoc cref="PushGPUFragmentUniformData(nint, uint, byte[], uint)"/>
    public static unsafe void PushGPUFragmentUniformData(IntPtr commandBuffer, uint slotIndex, ReadOnlySpan<byte> data, uint length)
    {
        fixed (byte* pData = data)
        {
            PushGPUFragmentUniformData(commandBuffer, slotIndex, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUComputeUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    private delegate void PushGPUComputeUniformDataPointerNativeDelegate(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length);
    private static PushGPUComputeUniformDataPointerNativeDelegate PushGPUComputeUniformDataPointerNativeFunction = SDL_PushGPUComputeUniformData;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_PushGPUComputeUniformData(SDL_GPUCommandBuffer *command_buffer, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Pushes data to a uniform slot on the command buffer.</para>
    /// <para>Subsequent draw calls in this command buffer will use this uniform data.</para>
    /// <para>The data being pushed must respect std140 layout conventions. In practical
    /// terms this means you must ensure that vec3 and vec4 fields are 16-byte
    /// aligned.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="slotIndex">the uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, IntPtr data, uint length)
    {
        PushGPUComputeUniformDataPointerNativeFunction(commandBuffer, slotIndex, data, length);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PushGPUComputeUniformData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length);
    private delegate void PushGPUComputeUniformDataArrayNativeDelegate(IntPtr commandBuffer, uint slotIndex, byte[] data, uint length);
    private static PushGPUComputeUniformDataArrayNativeDelegate PushGPUComputeUniformDataArrayNativeFunction = SDL_PushGPUComputeUniformData;

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
    public static void PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, uint length)
    {
        PushGPUComputeUniformDataArrayNativeFunction(commandBuffer, slotIndex, data, length);
    }

    /// <inheritdoc cref="PushGPUComputeUniformData(nint, uint, byte[], uint)"/>
    public static unsafe void PushGPUComputeUniformData(IntPtr commandBuffer, uint slotIndex, ReadOnlySpan<byte> data, uint length)
    {
        fixed (byte* pData = data)
        {
            PushGPUComputeUniformData(commandBuffer, slotIndex, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPURenderPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_BeginGPURenderPass(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo);
    private delegate IntPtr BeginGPURenderPassPointerNativeDelegate(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo);
    private static BeginGPURenderPassPointerNativeDelegate BeginGPURenderPassPointerNativeFunction = SDL_BeginGPURenderPass;

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
    public static IntPtr BeginGPURenderPass(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo)
    {
        return BeginGPURenderPassPointerNativeFunction(commandBuffer, colorTargetInfos, numColorTargets, depthStencilTargetInfo);
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

    /// <inheritdoc cref="BeginGPURenderPass(nint, in GPUColorTargetInfo[], uint, nint)"/>
    public static unsafe IntPtr BeginGPURenderPass(IntPtr commandBuffer, ReadOnlySpan<GPUColorTargetInfo> colorTargetInfos, uint numColorTargets, IntPtr depthStencilTargetInfo)
    {
        fixed (GPUColorTargetInfo* pInfos = colorTargetInfos)
        {
            return BeginGPURenderPass(commandBuffer, (IntPtr)pInfos, numColorTargets, depthStencilTargetInfo);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPURenderPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_BeginGPURenderPass(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, in GPUDepthStencilTargetInfo depthStencilTargetInfo);
    private delegate IntPtr BeginGPURenderPassDepthStencilNativeDelegate(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, in GPUDepthStencilTargetInfo depthStencilTargetInfo);
    private static BeginGPURenderPassDepthStencilNativeDelegate BeginGPURenderPassDepthStencilNativeFunction = SDL_BeginGPURenderPass;

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
    public static IntPtr BeginGPURenderPass(IntPtr commandBuffer, IntPtr colorTargetInfos, uint numColorTargets, in GPUDepthStencilTargetInfo depthStencilTargetInfo)
    {
        return BeginGPURenderPassDepthStencilNativeFunction(commandBuffer, colorTargetInfos, numColorTargets, in depthStencilTargetInfo);
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

    /// <inheritdoc cref="BeginGPURenderPass(nint, in GPUColorTargetInfo[], uint, in GPUDepthStencilTargetInfo)"/>
    public static unsafe IntPtr BeginGPURenderPass(IntPtr commandBuffer, ReadOnlySpan<GPUColorTargetInfo> colorTargetInfos, uint numColorTargets, in GPUDepthStencilTargetInfo depthStencilTargetInfo)
    {
        fixed (GPUColorTargetInfo* pInfos = colorTargetInfos)
        {
            return BeginGPURenderPass(commandBuffer, (IntPtr)pInfos, numColorTargets, depthStencilTargetInfo);
        }
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUGraphicsPipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUGraphicsPipeline(IntPtr renderPass, IntPtr graphicsPipeline);
    private delegate void BindGPUGraphicsPipelineNativeDelegate(IntPtr renderPass, IntPtr graphicsPipeline);
    private static BindGPUGraphicsPipelineNativeDelegate BindGPUGraphicsPipelineNativeFunction = SDL_BindGPUGraphicsPipeline;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUGraphicsPipeline(SDL_GPURenderPass *render_pass, SDL_GPUGraphicsPipeline *graphics_pipeline);</code>
    /// <summary>
    /// <para>Binds a graphics pipeline on a render pass to be used in rendering.</para>
    /// <para>A graphics pipeline must be bound before making any draw calls.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="graphicsPipeline">the graphics pipeline to bind.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void BindGPUGraphicsPipeline(IntPtr renderPass, IntPtr graphicsPipeline)
    {
        BindGPUGraphicsPipelineNativeFunction(renderPass, graphicsPipeline);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUViewport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUViewport(IntPtr renderPass, in GPUViewport viewport);
    private delegate void SetGPUViewportNativeDelegate(IntPtr renderPass, in GPUViewport viewport);
    private static SetGPUViewportNativeDelegate SetGPUViewportNativeFunction = SDL_SetGPUViewport;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUViewport(SDL_GPURenderPass *render_pass, const SDL_GPUViewport *viewport);</code>
    /// <summary>
    /// Sets the current viewport state on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="viewport">the viewport to set.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void SetGPUViewport(IntPtr renderPass, in GPUViewport viewport)
    {
        SetGPUViewportNativeFunction(renderPass, in viewport);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUScissor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUScissor(IntPtr renderPass, in Rect scissor);
    private delegate void SetGPUScissorNativeDelegate(IntPtr renderPass, in Rect scissor);
    private static SetGPUScissorNativeDelegate SetGPUScissorNativeFunction = SDL_SetGPUScissor;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUScissor(SDL_GPURenderPass *render_pass, const SDL_Rect *scissor);</code>
    /// <summary>
    /// Sets the current scissor state on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="scissor">the scissor area to set.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void SetGPUScissor(IntPtr renderPass, in Rect scissor)
    {
        SetGPUScissorNativeFunction(renderPass, in scissor);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUBlendConstants"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUBlendConstants(IntPtr renderPass, in FColor blendConstants);
    private delegate void SetGPUBlendConstantsNativeDelegate(IntPtr renderPass, in FColor blendConstants);
    private static SetGPUBlendConstantsNativeDelegate SetGPUBlendConstantsNativeFunction = SDL_SetGPUBlendConstants;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUBlendConstants(SDL_GPURenderPass *render_pass, SDL_FColor blend_constants);</code>
    /// <summary>
    /// Sets the current blend constants on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="blendConstants">the blend constant color.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GPUBlendFactor.ConstantColor"/>
    /// <seealso cref="GPUBlendFactor.OneMinusConstantColor"/>
    public static void SetGPUBlendConstants(IntPtr renderPass, in FColor blendConstants)
    {
        SetGPUBlendConstantsNativeFunction(renderPass, in blendConstants);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUStencilReference"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetGPUStencilReference(IntPtr renderPass, byte reference);
    private delegate void SetGPUStencilReferenceNativeDelegate(IntPtr renderPass, byte reference);
    private static SetGPUStencilReferenceNativeDelegate SetGPUStencilReferenceNativeFunction = SDL_SetGPUStencilReference;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetGPUStencilReference(SDL_GPURenderPass *render_pass, Uint8 reference);</code>
    /// <summary>
    /// Sets the current stencil reference value on a command buffer.
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <param name="reference">the stencil reference value to set.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void SetGPUStencilReference(IntPtr renderPass, byte reference)
    {
        SetGPUStencilReferenceNativeFunction(renderPass, reference);
    }


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
    public static void BindGPUVertexBuffers(IntPtr renderPass, uint firstSlot, GPUBufferBinding[] bindings, uint numBindings)
    {
        if (bindings.Length == 0)
        {
            BindGPUVertexBuffers(renderPass, firstSlot, IntPtr.Zero, numBindings);
            return;
        }

        unsafe
        {
            fixed (GPUBufferBinding* pInfos = &bindings[0])
            {
                BindGPUVertexBuffers(renderPass, firstSlot, (nint)pInfos, numBindings);
            }
        }
    }

    /// <inheritdoc cref="BindGPUVertexBuffers(nint, uint, GPUBufferBinding[], uint)"/>
    public static unsafe void BindGPUVertexBuffers(IntPtr renderPass, uint firstSlot, ReadOnlySpan<GPUBufferBinding> bindings, uint numBindings)
    {
        fixed (GPUBufferBinding* pBindings = bindings)
        {
            BindGPUVertexBuffers(renderPass, firstSlot, (IntPtr)pBindings, numBindings);
        }
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexBuffers(IntPtr renderPass, uint firstSlot, IntPtr bindings, uint numBindings);
    private delegate void BindGPUVertexBuffersPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr bindings, uint numBindings);
    private static BindGPUVertexBuffersPointerNativeDelegate BindGPUVertexBuffersPointerNativeFunction = SDL_BindGPUVertexBuffers;

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
    public static void BindGPUVertexBuffers(IntPtr renderPass, uint firstSlot, IntPtr bindings, uint numBindings)
    {
        BindGPUVertexBuffersPointerNativeFunction(renderPass, firstSlot, bindings, numBindings);
    }
    #endregion


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUIndexBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUIndexBuffer(IntPtr renderPass, in GPUBufferBinding binding, GPUIndexElementSize indexElementSize);
    private delegate void BindGPUIndexBufferNativeDelegate(IntPtr renderPass, in GPUBufferBinding binding, GPUIndexElementSize indexElementSize);
    private static BindGPUIndexBufferNativeDelegate BindGPUIndexBufferNativeFunction = SDL_BindGPUIndexBuffer;

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
    public static void BindGPUIndexBuffer(IntPtr renderPass, in GPUBufferBinding binding, GPUIndexElementSize indexElementSize)
    {
        BindGPUIndexBufferNativeFunction(renderPass, in binding, indexElementSize);
    }


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
    public static void BindGPUVertexSamplers(IntPtr renderPass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings)
    {
        if (textureSamplerBindings.Length == 0)
        {
            BindGPUVertexSamplers(renderPass, firstSlot, IntPtr.Zero, numBindings);
            return;
        }

        unsafe
        {
            fixed (GPUTextureSamplerBinding* pInfos = &textureSamplerBindings[0])
            {
                BindGPUVertexSamplers(renderPass, firstSlot, (nint)pInfos, numBindings);
            }
        }

    }

    /// <inheritdoc cref="BindGPUVertexSamplers(nint, uint, GPUTextureSamplerBinding[], uint)"/>
    public static unsafe void BindGPUVertexSamplers(IntPtr renderPass, uint firstSlot, ReadOnlySpan<GPUTextureSamplerBinding> textureSamplerBindings, uint numBindings)
    {
        fixed (GPUTextureSamplerBinding* pTextureSamplerBindings = textureSamplerBindings)
        {
            BindGPUVertexSamplers(renderPass, firstSlot, (IntPtr)pTextureSamplerBindings, numBindings);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexSamplers(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    private delegate void BindGPUVertexSamplersPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    private static BindGPUVertexSamplersPointerNativeDelegate BindGPUVertexSamplersPointerNativeFunction = SDL_BindGPUVertexSamplers;

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
    public static void BindGPUVertexSamplers(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings)
    {
        BindGPUVertexSamplersPointerNativeFunction(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }
    #endregion


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    private delegate void BindGPUVertexStorageTexturesNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    private static BindGPUVertexStorageTexturesNativeDelegate BindGPUVertexStorageTexturesNativeFunction = SDL_BindGPUVertexStorageTextures;

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
    public static void BindGPUVertexStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings)
    {
        BindGPUVertexStorageTexturesNativeFunction(renderPass, firstSlot, storageTextures, numBindings);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    private delegate void BindGPUVertexStorageTexturesPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    private static BindGPUVertexStorageTexturesPointerNativeDelegate BindGPUVertexStorageTexturesPointerNativeFunction = SDL_BindGPUVertexStorageTextures;

    /// <inheritdoc cref="BindGPUVertexStorageTextures(nint, uint, nint[], uint)"/>
    public static unsafe void BindGPUVertexStorageTextures(IntPtr renderPass, uint firstSlot, ReadOnlySpan<IntPtr> storageTextures, uint numBindings)
    {
        fixed (IntPtr* pStorageTextures = storageTextures)
        {
            BindGPUVertexStorageTexturesPointerNativeFunction(renderPass, firstSlot, (IntPtr)pStorageTextures, numBindings);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    private delegate void BindGPUVertexStorageBuffersNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    private static BindGPUVertexStorageBuffersNativeDelegate BindGPUVertexStorageBuffersNativeFunction = SDL_BindGPUVertexStorageBuffers;

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
    public static void BindGPUVertexStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings)
    {
        BindGPUVertexStorageBuffersNativeFunction(renderPass, firstSlot, storageBuffers, numBindings);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUVertexStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUVertexStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    private delegate void BindGPUVertexStorageBuffersPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    private static BindGPUVertexStorageBuffersPointerNativeDelegate BindGPUVertexStorageBuffersPointerNativeFunction = SDL_BindGPUVertexStorageBuffers;

    /// <inheritdoc cref="BindGPUVertexStorageBuffers(nint, uint, nint[], uint)"/>
    public static unsafe void BindGPUVertexStorageBuffers(IntPtr renderPass, uint firstSlot, ReadOnlySpan<IntPtr> storageBuffers, uint numBindings)
    {
        fixed (IntPtr* pStorageBuffers = storageBuffers)
        {
            BindGPUVertexStorageBuffersPointerNativeFunction(renderPass, firstSlot, (IntPtr)pStorageBuffers, numBindings);
        }
    }


    #region BindGPUFragmentSamplers
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);
    private delegate void BindGPUFragmentSamplersArrayNativeDelegate(IntPtr renderPass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);
    private static BindGPUFragmentSamplersArrayNativeDelegate BindGPUFragmentSamplersArrayNativeFunction = SDL_BindGPUFragmentSamplers;

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
    public static void BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings)
    {
        BindGPUFragmentSamplersArrayNativeFunction(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    private delegate void BindGPUFragmentSamplersPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    private static BindGPUFragmentSamplersPointerNativeDelegate BindGPUFragmentSamplersPointerNativeFunction = SDL_BindGPUFragmentSamplers;

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
    public static void BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings)
    {
        BindGPUFragmentSamplersPointerNativeFunction(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    /// <inheritdoc cref="BindGPUFragmentSamplers(nint, uint, GPUTextureSamplerBinding[], uint)"/>
    public static unsafe void BindGPUFragmentSamplers(IntPtr renderPass, uint firstSlot, ReadOnlySpan<GPUTextureSamplerBinding> textureSamplerBindings, uint numBindings)
    {
        fixed (GPUTextureSamplerBinding* pTextureSamplerBindings = textureSamplerBindings)
        {
            BindGPUFragmentSamplers(renderPass, firstSlot, (IntPtr)pTextureSamplerBindings, numBindings);
        }
    }
    #endregion


    #region BindGPUFragmentStorageTextures
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    private delegate void BindGPUFragmentStorageTexturesArrayNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    private static BindGPUFragmentStorageTexturesArrayNativeDelegate BindGPUFragmentStorageTexturesArrayNativeFunction = SDL_BindGPUFragmentStorageTextures;

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
    public static void BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr[] storageTextures, uint numBindings)
    {
        BindGPUFragmentStorageTexturesArrayNativeFunction(renderPass, firstSlot, storageTextures, numBindings);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    private delegate void BindGPUFragmentStorageTexturesPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    private static BindGPUFragmentStorageTexturesPointerNativeDelegate BindGPUFragmentStorageTexturesPointerNativeFunction = SDL_BindGPUFragmentStorageTextures;

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
    public static void BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, IntPtr storageTextures, uint numBindings)
    {
        BindGPUFragmentStorageTexturesPointerNativeFunction(renderPass, firstSlot, storageTextures, numBindings);
    }

    /// <inheritdoc cref="BindGPUFragmentStorageTextures(nint, uint, nint[], uint)"/>
    public static unsafe void BindGPUFragmentStorageTextures(IntPtr renderPass, uint firstSlot, ReadOnlySpan<IntPtr> storageTextures, uint numBindings)
    {
        fixed (IntPtr* pStorageTextures = storageTextures)
        {
            BindGPUFragmentStorageTextures(renderPass, firstSlot, (IntPtr)pStorageTextures, numBindings);
        }
    }
    #endregion


    #region BindGPUFragmentStorageBuffers
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    private delegate void BindGPUFragmentStorageBuffersArrayNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    private static BindGPUFragmentStorageBuffersArrayNativeDelegate BindGPUFragmentStorageBuffersArrayNativeFunction = SDL_BindGPUFragmentStorageBuffers;

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
    public static void BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings)
    {
        BindGPUFragmentStorageBuffersArrayNativeFunction(renderPass, firstSlot, storageBuffers, numBindings);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUFragmentStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    private delegate void BindGPUFragmentStorageBuffersPointerNativeDelegate(IntPtr renderPass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    private static BindGPUFragmentStorageBuffersPointerNativeDelegate BindGPUFragmentStorageBuffersPointerNativeFunction = SDL_BindGPUFragmentStorageBuffers;

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
    public static void BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, IntPtr storageBuffers, uint numBindings)
    {
        BindGPUFragmentStorageBuffersPointerNativeFunction(renderPass, firstSlot, storageBuffers, numBindings);
    }

    /// <inheritdoc cref="BindGPUFragmentStorageBuffers(nint, uint, nint[], uint)"/>
    public static unsafe void BindGPUFragmentStorageBuffers(IntPtr renderPass, uint firstSlot, ReadOnlySpan<IntPtr> storageBuffers, uint numBindings)
    {
        fixed (IntPtr* pStorageBuffers = storageBuffers)
        {
            BindGPUFragmentStorageBuffers(renderPass, firstSlot, (IntPtr)pStorageBuffers, numBindings);
        }
    }
    #endregion


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUIndexedPrimitives"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUIndexedPrimitives(IntPtr renderPass, uint numIndices, uint numInstances, uint firstIndex, int vertexOffset, uint firstInstance);
    private delegate void DrawGPUIndexedPrimitivesNativeDelegate(IntPtr renderPass, uint numIndices, uint numInstances, uint firstIndex, int vertexOffset, uint firstInstance);
    private static DrawGPUIndexedPrimitivesNativeDelegate DrawGPUIndexedPrimitivesNativeFunction = SDL_DrawGPUIndexedPrimitives;

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
    public static void DrawGPUIndexedPrimitives(IntPtr renderPass, uint numIndices, uint numInstances, uint firstIndex, int vertexOffset, uint firstInstance)
    {
        DrawGPUIndexedPrimitivesNativeFunction(renderPass, numIndices, numInstances, firstIndex, vertexOffset, firstInstance);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUPrimitives"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUPrimitives(IntPtr renderPass, uint numVertices, uint numInstances, uint firstVertex, uint firstInstance);
    private delegate void DrawGPUPrimitivesNativeDelegate(IntPtr renderPass, uint numVertices, uint numInstances, uint firstVertex, uint firstInstance);
    private static DrawGPUPrimitivesNativeDelegate DrawGPUPrimitivesNativeFunction = SDL_DrawGPUPrimitives;

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
    public static void DrawGPUPrimitives(IntPtr renderPass, uint numVertices, uint numInstances, uint firstVertex, uint firstInstance)
    {
        DrawGPUPrimitivesNativeFunction(renderPass, numVertices, numInstances, firstVertex, firstInstance);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUPrimitivesIndirect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount);
    private delegate void DrawGPUPrimitivesIndirectNativeDelegate(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount);
    private static DrawGPUPrimitivesIndirectNativeDelegate DrawGPUPrimitivesIndirectNativeFunction = SDL_DrawGPUPrimitivesIndirect;

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
    public static void DrawGPUPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount)
    {
        DrawGPUPrimitivesIndirectNativeFunction(renderPass, buffer, offset, drawCount);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DrawGPUIndexedPrimitivesIndirect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DrawGPUIndexedPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount);
    private delegate void DrawGPUIndexedPrimitivesIndirectNativeDelegate(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount);
    private static DrawGPUIndexedPrimitivesIndirectNativeDelegate DrawGPUIndexedPrimitivesIndirectNativeFunction = SDL_DrawGPUIndexedPrimitivesIndirect;

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
    public static void DrawGPUIndexedPrimitivesIndirect(IntPtr renderPass, IntPtr buffer, uint offset, uint drawCount)
    {
        DrawGPUIndexedPrimitivesIndirectNativeFunction(renderPass, buffer, offset, drawCount);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EndGPURenderPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPURenderPass(IntPtr renderPass);
    private delegate void EndGPURenderPassNativeDelegate(IntPtr renderPass);
    private static EndGPURenderPassNativeDelegate EndGPURenderPassNativeFunction = SDL_EndGPURenderPass;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EndGPURenderPass(SDL_GPURenderPass *render_pass);</code>
    /// <summary>
    /// <para>Ends the given render pass.</para>
    /// <para>All bound graphics state on the render pass command buffer is unset. The
    /// render pass handle is now invalid.</para>
    /// </summary>
    /// <param name="renderPass">a render pass handle.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void EndGPURenderPass(IntPtr renderPass)
    {
        EndGPURenderPassNativeFunction(renderPass);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPUComputePass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_BeginGPUComputePass(IntPtr commandBuffer, GPUStorageTextureReadWriteBinding[] storageTextureBindings, uint numStorageTextureBindings, GPUStorageBufferReadWriteBinding[] storageBufferBindings, uint numStorageBufferBindings);
    private delegate IntPtr BeginGPUComputePassNativeDelegate(IntPtr commandBuffer, GPUStorageTextureReadWriteBinding[] storageTextureBindings, uint numStorageTextureBindings, GPUStorageBufferReadWriteBinding[] storageBufferBindings, uint numStorageBufferBindings);
    private static BeginGPUComputePassNativeDelegate BeginGPUComputePassNativeFunction = SDL_BeginGPUComputePass;

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
    public static IntPtr BeginGPUComputePass(IntPtr commandBuffer, GPUStorageTextureReadWriteBinding[] storageTextureBindings, uint numStorageTextureBindings, GPUStorageBufferReadWriteBinding[] storageBufferBindings, uint numStorageBufferBindings)
    {
        return BeginGPUComputePassNativeFunction(commandBuffer, storageTextureBindings, numStorageTextureBindings, storageBufferBindings, numStorageBufferBindings);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPUComputePass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_BeginGPUComputePass(IntPtr commandBuffer, IntPtr storageTextureBindings, uint numStorageTextureBindings, IntPtr storageBufferBindings, uint numStorageBufferBindings);
    private delegate IntPtr BeginGPUComputePassPointerNativeDelegate(IntPtr commandBuffer, IntPtr storageTextureBindings, uint numStorageTextureBindings, IntPtr storageBufferBindings, uint numStorageBufferBindings);
    private static BeginGPUComputePassPointerNativeDelegate BeginGPUComputePassPointerNativeFunction = SDL_BeginGPUComputePass;

    /// <inheritdoc cref="BeginGPUComputePass(nint, GPUStorageTextureReadWriteBinding[], uint, GPUStorageBufferReadWriteBinding[], uint)"/>
    public static IntPtr BeginGPUComputePass(IntPtr commandBuffer, IntPtr storageTextureBindings, uint numStorageTextureBindings, IntPtr storageBufferBindings, uint numStorageBufferBindings)
    {
        return BeginGPUComputePassPointerNativeFunction(commandBuffer, storageTextureBindings, numStorageTextureBindings, storageBufferBindings, numStorageBufferBindings);
    }

    /// <inheritdoc cref="BeginGPUComputePass(nint, GPUStorageTextureReadWriteBinding[], uint, GPUStorageBufferReadWriteBinding[], uint)"/>
    public static unsafe IntPtr BeginGPUComputePass(IntPtr commandBuffer, ReadOnlySpan<GPUStorageTextureReadWriteBinding> storageTextureBindings, uint numStorageTextureBindings, ReadOnlySpan<GPUStorageBufferReadWriteBinding> storageBufferBindings, uint numStorageBufferBindings)
    {
        fixed (GPUStorageTextureReadWriteBinding* pStorageTextureBindings = storageTextureBindings)
        fixed (GPUStorageBufferReadWriteBinding* pStorageBufferBindings = storageBufferBindings)
        {
            return BeginGPUComputePass(commandBuffer, (IntPtr)pStorageTextureBindings, numStorageTextureBindings, (IntPtr)pStorageBufferBindings, numStorageBufferBindings);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputePipeline"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputePipeline(IntPtr computePass, IntPtr computePipeline);
    private delegate void BindGPUComputePipelineNativeDelegate(IntPtr computePass, IntPtr computePipeline);
    private static BindGPUComputePipelineNativeDelegate BindGPUComputePipelineNativeFunction = SDL_BindGPUComputePipeline;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputePipeline(SDL_GPUComputePass *compute_pass, SDL_GPUComputePipeline *compute_pipeline);</code>
    /// <summary>
    /// Binds a compute pipeline on a command buffer for use in compute dispatch.
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="computePipeline">a compute pipeline to bind.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void BindGPUComputePipeline(IntPtr computePass, IntPtr computePipeline)
    {
        BindGPUComputePipelineNativeFunction(computePass, computePipeline);
    }


    #region BindGPUComputeSamplers
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);
    private delegate void BindGPUComputeSamplersArrayNativeDelegate(IntPtr computePass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings);
    private static BindGPUComputeSamplersArrayNativeDelegate BindGPUComputeSamplersArrayNativeFunction = SDL_BindGPUComputeSamplers;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeSamplers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the compute shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUComputePipeline"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler bindings to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUComputePipeline"/>
    public static void BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, GPUTextureSamplerBinding[] textureSamplerBindings, uint numBindings)
    {
        BindGPUComputeSamplersArrayNativeFunction(computePass, firstSlot, textureSamplerBindings, numBindings);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeSamplers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    private delegate void BindGPUComputeSamplersPointerNativeDelegate(IntPtr computePass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings);
    private static BindGPUComputeSamplersPointerNativeDelegate BindGPUComputeSamplersPointerNativeFunction = SDL_BindGPUComputeSamplers;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeSamplers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, const SDL_GPUTextureSamplerBinding *texture_sampler_bindings, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds texture-sampler pairs for use on the compute shader.</para>
    /// <para>The textures must have been created with <see cref="GPUTextureUsageFlags.Sampler"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUComputePipeline"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute sampler slot to begin binding from.</param>
    /// <param name="textureSamplerBindings">an array of texture-sampler binding
    /// structs.</param>
    /// <param name="numBindings">the number of texture-sampler bindings to bind from the
    /// array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUComputePipeline"/>
    public static void BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, IntPtr textureSamplerBindings, uint numBindings)
    {
        BindGPUComputeSamplersPointerNativeFunction(computePass, firstSlot, textureSamplerBindings, numBindings);
    }

    /// <inheritdoc cref="BindGPUComputeSamplers(nint, uint, GPUTextureSamplerBinding[], uint)"/>
    public static unsafe void BindGPUComputeSamplers(IntPtr computePass, uint firstSlot, ReadOnlySpan<GPUTextureSamplerBinding> textureSamplerBindings, uint numBindings)
    {
        fixed (GPUTextureSamplerBinding* pTextureSamplerBindings = textureSamplerBindings)
        {
            BindGPUComputeSamplers(computePass, firstSlot, (IntPtr)pTextureSamplerBindings, numBindings);
        }
    }
    #endregion


    #region BindGPUComputeStorageTextures
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    private delegate void BindGPUComputeStorageTexturesArrayNativeDelegate(IntPtr computePass, uint firstSlot, IntPtr[] storageTextures, uint numBindings);
    private static BindGPUComputeStorageTexturesArrayNativeDelegate BindGPUComputeStorageTexturesArrayNativeFunction = SDL_BindGPUComputeStorageTextures;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageTextures(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures as readonly for use on the compute pipeline.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUComputePipeline"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage textures to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUComputePipeline"/>
    public static void BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, IntPtr[] storageTextures, uint numBindings)
    {
        BindGPUComputeStorageTexturesArrayNativeFunction(computePass, firstSlot, storageTextures, numBindings);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageTextures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    private delegate void BindGPUComputeStorageTexturesPointerNativeDelegate(IntPtr computePass, uint firstSlot, IntPtr storageTextures, uint numBindings);
    private static BindGPUComputeStorageTexturesPointerNativeDelegate BindGPUComputeStorageTexturesPointerNativeFunction = SDL_BindGPUComputeStorageTextures;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageTextures(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUTexture *const *storage_textures, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage textures as readonly for use on the compute pipeline.</para>
    /// <para>These textures must have been created with
    /// <see cref="GPUTextureUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <see cref="CreateGPUComputePipeline"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage texture slot to begin binding from.</param>
    /// <param name="storageTextures">an array of storage textures.</param>
    /// <param name="numBindings">the number of storage textures to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUComputePipeline"/>
    public static void BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, IntPtr storageTextures, uint numBindings)
    {
        BindGPUComputeStorageTexturesPointerNativeFunction(computePass, firstSlot, storageTextures, numBindings);
    }

    /// <inheritdoc cref="BindGPUComputeStorageTextures(nint, uint, nint[], uint)"/>
    public static unsafe void BindGPUComputeStorageTextures(IntPtr computePass, uint firstSlot, ReadOnlySpan<IntPtr> storageTextures, uint numBindings)
    {
        fixed (IntPtr* pStorageTextures = storageTextures)
        {
            BindGPUComputeStorageTextures(computePass, firstSlot, (IntPtr)pStorageTextures, numBindings);
        }
    }
    #endregion


    #region BindGPUComputeStorageBuffers
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    private delegate void BindGPUComputeStorageBuffersArrayNativeDelegate(IntPtr computePass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings);
    private static BindGPUComputeStorageBuffersArrayNativeDelegate BindGPUComputeStorageBuffersArrayNativeFunction = SDL_BindGPUComputeStorageBuffers;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageBuffers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers as readonly for use on the compute pipeline.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUComputePipeline"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of storage buffer binding structs.</param>
    /// <param name="numBindings">the number of storage buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUComputePipeline"/>
    public static void BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, IntPtr[] storageBuffers, uint numBindings)
    {
        BindGPUComputeStorageBuffersArrayNativeFunction(computePass, firstSlot, storageBuffers, numBindings);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindGPUComputeStorageBuffers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    private delegate void BindGPUComputeStorageBuffersPointerNativeDelegate(IntPtr computePass, uint firstSlot, IntPtr storageBuffers, uint numBindings);
    private static BindGPUComputeStorageBuffersPointerNativeDelegate BindGPUComputeStorageBuffersPointerNativeFunction = SDL_BindGPUComputeStorageBuffers;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BindGPUComputeStorageBuffers(SDL_GPUComputePass *compute_pass, Uint32 first_slot, SDL_GPUBuffer *const *storage_buffers, Uint32 num_bindings);</code>
    /// <summary>
    /// <para>Binds storage buffers as readonly for use on the compute pipeline.</para>
    /// <para>These buffers must have been created with
    /// <see cref="GPUBufferUsageFlags.ComputeStorageRead"/>.</para>
    /// <para>Be sure your shader is set up according to the requirements documented in <seealso cref="CreateGPUComputePipeline"/>.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <param name="firstSlot">the compute storage buffer slot to begin binding from.</param>
    /// <param name="storageBuffers">an array of storage buffer binding structs.</param>
    /// <param name="numBindings">the number of storage buffers to bind from the array.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateGPUComputePipeline"/>
    public static void BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, IntPtr storageBuffers, uint numBindings)
    {
        BindGPUComputeStorageBuffersPointerNativeFunction(computePass, firstSlot, storageBuffers, numBindings);
    }

    /// <inheritdoc cref="BindGPUComputeStorageBuffers(nint, uint, nint[], uint)"/>
    public static unsafe void BindGPUComputeStorageBuffers(IntPtr computePass, uint firstSlot, ReadOnlySpan<IntPtr> storageBuffers, uint numBindings)
    {
        fixed (IntPtr* pStorageBuffers = storageBuffers)
        {
            BindGPUComputeStorageBuffers(computePass, firstSlot, (IntPtr)pStorageBuffers, numBindings);
        }
    }
    #endregion


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DispatchGPUCompute"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DispatchGPUCompute(IntPtr computePass, uint groupcountX, uint groupcountY, uint groupcountZ);
    private delegate void DispatchGPUComputeNativeDelegate(IntPtr computePass, uint groupcountX, uint groupcountY, uint groupcountZ);
    private static DispatchGPUComputeNativeDelegate DispatchGPUComputeNativeFunction = SDL_DispatchGPUCompute;

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
    public static void DispatchGPUCompute(IntPtr computePass, uint groupcountX, uint groupcountY, uint groupcountZ)
    {
        DispatchGPUComputeNativeFunction(computePass, groupcountX, groupcountY, groupcountZ);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DispatchGPUComputeIndirect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DispatchGPUComputeIndirect(IntPtr computePass, IntPtr buffer, uint offset);
    private delegate void DispatchGPUComputeIndirectNativeDelegate(IntPtr computePass, IntPtr buffer, uint offset);
    private static DispatchGPUComputeIndirectNativeDelegate DispatchGPUComputeIndirectNativeFunction = SDL_DispatchGPUComputeIndirect;

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
    public static void DispatchGPUComputeIndirect(IntPtr computePass, IntPtr buffer, uint offset)
    {
        DispatchGPUComputeIndirectNativeFunction(computePass, buffer, offset);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EndGPUComputePass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPUComputePass(IntPtr computePass);
    private delegate void EndGPUComputePassNativeDelegate(IntPtr computePass);
    private static EndGPUComputePassNativeDelegate EndGPUComputePassNativeFunction = SDL_EndGPUComputePass;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EndGPUComputePass(SDL_GPUComputePass *compute_pass);</code>
    /// <summary>
    /// <para>Ends the current compute pass.</para>
    /// <para>All bound compute state on the command buffer is unset. The compute pass
    /// handle is now invalid.</para>
    /// </summary>
    /// <param name="computePass">a compute pass handle.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void EndGPUComputePass(IntPtr computePass)
    {
        EndGPUComputePassNativeFunction(computePass);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MapGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_MapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer, [MarshalAs(UnmanagedType.I1)] bool cycle);
    private delegate IntPtr MapGPUTransferBufferNativeDelegate(IntPtr device, IntPtr transferBuffer, bool cycle);
    private static MapGPUTransferBufferNativeDelegate MapGPUTransferBufferNativeFunction = SDL_MapGPUTransferBuffer;

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
    public static IntPtr MapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer, [MarshalAs(UnmanagedType.I1)] bool cycle)
    {
        return MapGPUTransferBufferNativeFunction(device, transferBuffer, cycle);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnmapGPUTransferBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnmapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer);
    private delegate void UnmapGPUTransferBufferNativeDelegate(IntPtr device, IntPtr transferBuffer);
    private static UnmapGPUTransferBufferNativeDelegate UnmapGPUTransferBufferNativeFunction = SDL_UnmapGPUTransferBuffer;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnmapGPUTransferBuffer(SDL_GPUDevice *device, SDL_GPUTransferBuffer *transfer_buffer);</code>
    /// <summary>
    /// Unmaps a previously mapped transfer buffer.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="transferBuffer">a previously mapped transfer buffer.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void UnmapGPUTransferBuffer(IntPtr device, IntPtr transferBuffer)
    {
        UnmapGPUTransferBufferNativeFunction(device, transferBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BeginGPUCopyPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_BeginGPUCopyPass(IntPtr commandBuffer);
    private delegate IntPtr BeginGPUCopyPassNativeDelegate(IntPtr commandBuffer);
    private static BeginGPUCopyPassNativeDelegate BeginGPUCopyPassNativeFunction = SDL_BeginGPUCopyPass;

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
    public static IntPtr BeginGPUCopyPass(IntPtr commandBuffer)
    {
        return BeginGPUCopyPassNativeFunction(commandBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPixelFormatFromGPUTextureFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetPixelFormatFromGPUTextureFormat(GPUTextureFormat format);
    private delegate PixelFormat GetPixelFormatFromGPUTextureFormatNativeDelegate(GPUTextureFormat format);
    private static GetPixelFormatFromGPUTextureFormatNativeDelegate GetPixelFormatFromGPUTextureFormatNativeFunction = SDL_GetPixelFormatFromGPUTextureFormat;

    /// <code>extern SDL_DECLSPEC SDL_PixelFormat SDLCALL SDL_GetPixelFormatFromGPUTextureFormat(SDL_GPUTextureFormat format);</code>
    /// <summary>
    /// <para>Get the SDL pixel format corresponding to a GPU texture format.</para>
    /// </summary>
    /// <param name="format">a texture format.</param>
    /// <returns>the corresponding pixel format, or <see cref="PixelFormat.Unknown"/> if
    /// there is no corresponding pixel format.</returns>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static PixelFormat GetPixelFormatFromGPUTextureFormat(GPUTextureFormat format)
    {
        return GetPixelFormatFromGPUTextureFormatNativeFunction(format);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUTextureFormatFromPixelFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GPUTextureFormat SDL_GetGPUTextureFormatFromPixelFormat(PixelFormat format);
    private delegate GPUTextureFormat GetGPUTextureFormatFromPixelFormatNativeDelegate(PixelFormat format);
    private static GetGPUTextureFormatFromPixelFormatNativeDelegate GetGPUTextureFormatFromPixelFormatNativeFunction = SDL_GetGPUTextureFormatFromPixelFormat;

    /// <code>extern SDL_DECLSPEC SDL_GPUTextureFormat SDLCALL SDL_GetGPUTextureFormatFromPixelFormat(SDL_PixelFormat format);</code>
    /// <summary>
    /// Get the GPU texture format corresponding to an SDL pixel format.
    /// </summary>
    /// <param name="format">a pixel format.</param>
    /// <returns>the corresponding GPU texture format, or
    /// <see cref="GPUTextureFormat.Invalid"/> if there is no corresponding GPU
    /// texture format.</returns>
    public static GPUTextureFormat GetPixelFormatFromGPUTextureFormat(PixelFormat format)
    {
        return GetGPUTextureFormatFromPixelFormatNativeFunction(format);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UploadToGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UploadToGPUTexture(IntPtr copyPass, in GPUTextureTransferInfo source, in GPUTextureRegion destination, [MarshalAs(UnmanagedType.I1)] bool cycle);
    private delegate void UploadToGPUTextureNativeDelegate(IntPtr copyPass, in GPUTextureTransferInfo source, in GPUTextureRegion destination, bool cycle);
    private static UploadToGPUTextureNativeDelegate UploadToGPUTextureNativeFunction = SDL_UploadToGPUTexture;

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
    public static void UploadToGPUTexture(IntPtr copyPass, in GPUTextureTransferInfo source, in GPUTextureRegion destination, [MarshalAs(UnmanagedType.I1)] bool cycle)
    {
        UploadToGPUTextureNativeFunction(copyPass, in source, in destination, cycle);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UploadToGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UploadToGPUBuffer(IntPtr copyPass, in GPUTransferBufferLocation source, in GPUBufferRegion destination, [MarshalAs(UnmanagedType.I1)] bool cycle);
    private delegate void UploadToGPUBufferNativeDelegate(IntPtr copyPass, in GPUTransferBufferLocation source, in GPUBufferRegion destination, bool cycle);
    private static UploadToGPUBufferNativeDelegate UploadToGPUBufferNativeFunction = SDL_UploadToGPUBuffer;

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
    public static void UploadToGPUBuffer(IntPtr copyPass, in GPUTransferBufferLocation source, in GPUBufferRegion destination, [MarshalAs(UnmanagedType.I1)] bool cycle)
    {
        UploadToGPUBufferNativeFunction(copyPass, in source, in destination, cycle);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CopyGPUTextureToTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CopyGPUTextureToTexture(IntPtr copyPass, in GPUTextureLocation source, in GPUTextureLocation destination, uint w, uint h, uint d, [MarshalAs(UnmanagedType.I1)] bool cycle);
    private delegate void CopyGPUTextureToTextureNativeDelegate(IntPtr copyPass, in GPUTextureLocation source, in GPUTextureLocation destination, uint w, uint h, uint d, bool cycle);
    private static CopyGPUTextureToTextureNativeDelegate CopyGPUTextureToTextureNativeFunction = SDL_CopyGPUTextureToTexture;

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
    public static void CopyGPUTextureToTexture(IntPtr copyPass, in GPUTextureLocation source, in GPUTextureLocation destination, uint w, uint h, uint d, [MarshalAs(UnmanagedType.I1)] bool cycle)
    {
        CopyGPUTextureToTextureNativeFunction(copyPass, in source, in destination, w, h, d, cycle);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CopyGPUBufferToBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CopyGPUBufferToBuffer(IntPtr copyPass, in GPUBufferLocation source, in GPUBufferLocation destination, uint size, [MarshalAs(UnmanagedType.I1)] bool cycle);
    private delegate void CopyGPUBufferToBufferNativeDelegate(IntPtr copyPass, in GPUBufferLocation source, in GPUBufferLocation destination, uint size, bool cycle);
    private static CopyGPUBufferToBufferNativeDelegate CopyGPUBufferToBufferNativeFunction = SDL_CopyGPUBufferToBuffer;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CopyGPUBufferToBuffer(SDL_GPUCopyPass *copy_pass, const SDL_GPUBufferLocation *source, const SDL_GPUBufferLocation *destination, Uint32 size, bool cycle);</code>
    /// <summary>
    /// <para>Performs a buffer-to-buffer copy.</para>
    /// <para>This copy occurs on the GPU timeline. You may assume the copy has finished
    /// in subsequent commands.</para>
    /// <para>This function does not support copying between depth and color textures.
    /// For those, copy the texture to a buffer and then to the destination
    /// texture.</para>
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <param name="source">the buffer and offset to copy from.</param>
    /// <param name="destination">the buffer and offset to copy to.</param>
    /// <param name="size">the length of the buffer to copy.</param>
    /// <param name="cycle">if <c>true</c>, cycles the destination buffer if it is already bound,
    /// otherwise overwrites the data.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void CopyGPUBufferToBuffer(IntPtr copyPass, in GPUBufferLocation source, in GPUBufferLocation destination, uint size, [MarshalAs(UnmanagedType.I1)] bool cycle)
    {
        CopyGPUBufferToBufferNativeFunction(copyPass, in source, in destination, size, cycle);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DownloadFromGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DownloadFromGPUTexture(IntPtr copyPass, in GPUTextureRegion source, in GPUTextureTransferInfo destination);
    private delegate void DownloadFromGPUTextureNativeDelegate(IntPtr copyPass, in GPUTextureRegion source, in GPUTextureTransferInfo destination);
    private static DownloadFromGPUTextureNativeDelegate DownloadFromGPUTextureNativeFunction = SDL_DownloadFromGPUTexture;

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
    public static void DownloadFromGPUTexture(IntPtr copyPass, in GPUTextureRegion source, in GPUTextureTransferInfo destination)
    {
        DownloadFromGPUTextureNativeFunction(copyPass, in source, in destination);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DownloadFromGPUBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DownloadFromGPUBuffer(IntPtr copyPass, in GPUTextureRegion source, in GPUTransferBufferLocation destination);
    private delegate void DownloadFromGPUBufferNativeDelegate(IntPtr copyPass, in GPUTextureRegion source, in GPUTransferBufferLocation destination);
    private static DownloadFromGPUBufferNativeDelegate DownloadFromGPUBufferNativeFunction = SDL_DownloadFromGPUBuffer;

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
    public static void DownloadFromGPUBuffer(IntPtr copyPass, in GPUTextureRegion source, in GPUTransferBufferLocation destination)
    {
        DownloadFromGPUBufferNativeFunction(copyPass, in source, in destination);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_EndGPUCopyPass"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_EndGPUCopyPass(IntPtr copyPass);
    private delegate void EndGPUCopyPassNativeDelegate(IntPtr copyPass);
    private static EndGPUCopyPassNativeDelegate EndGPUCopyPassNativeFunction = SDL_EndGPUCopyPass;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_EndGPUCopyPass(SDL_GPUCopyPass *copy_pass);</code>
    /// <summary>
    /// Ends the current copy pass.
    /// </summary>
    /// <param name="copyPass">a copy pass handle.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void EndGPUCopyPass(IntPtr copyPass)
    {
        EndGPUCopyPassNativeFunction(copyPass);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GenerateMipmapsForGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GenerateMipmapsForGPUTexture(IntPtr commandBuffer, IntPtr texture);
    private delegate void GenerateMipmapsForGPUTextureNativeDelegate(IntPtr commandBuffer, IntPtr texture);
    private static GenerateMipmapsForGPUTextureNativeDelegate GenerateMipmapsForGPUTextureNativeFunction = SDL_GenerateMipmapsForGPUTexture;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GenerateMipmapsForGPUTexture(SDL_GPUCommandBuffer *command_buffer, SDL_GPUTexture *texture);</code>
    /// <summary>
    /// <para>Generates mipmaps for the given texture.</para>
    /// <para>This function must not be called inside of any pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command_buffer.</param>
    /// <param name="texture">a texture with more than 1 mip level.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void GenerateMipmapsForGPUTexture(IntPtr commandBuffer, IntPtr texture)
    {
        GenerateMipmapsForGPUTextureNativeFunction(commandBuffer, texture);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BlitGPUTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_BlitGPUTexture(IntPtr commandBuffer, in GPUBlitInfo info);
    private delegate void BlitGPUTextureNativeDelegate(IntPtr commandBuffer, in GPUBlitInfo info);
    private static BlitGPUTextureNativeDelegate BlitGPUTextureNativeFunction = SDL_BlitGPUTexture;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BlitGPUTexture(SDL_GPUCommandBuffer *command_buffer, const SDL_GPUBlitInfo *info);</code>
    /// <summary>
    /// <para>Blits from a source texture region to a destination texture region.</para>
    /// <para>This function must not be called inside of any pass.</para>
    /// </summary>
    /// <param name="commandBuffer">a command buffer.</param>
    /// <param name="info">the blit info struct containing the blit parameters.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void BlitGPUTexture(IntPtr commandBuffer, in GPUBlitInfo info)
    {
        BlitGPUTextureNativeFunction(commandBuffer, in info);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WindowSupportsGPUSwapchainComposition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition);
    private delegate bool WindowSupportsGPUSwapchainCompositionNativeDelegate(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition);
    private static WindowSupportsGPUSwapchainCompositionNativeDelegate WindowSupportsGPUSwapchainCompositionNativeFunction = SDL_WindowSupportsGPUSwapchainComposition;

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
    public static bool WindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition)
    {
        return WindowSupportsGPUSwapchainCompositionNativeFunction(device, window, swapchainComposition);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WindowSupportsGPUPresentMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WindowSupportsGPUPresentMode(IntPtr device, IntPtr window, GPUPresentMode presentMode);
    private delegate bool WindowSupportsGPUPresentModeNativeDelegate(IntPtr device, IntPtr window, GPUPresentMode presentMode);
    private static WindowSupportsGPUPresentModeNativeDelegate WindowSupportsGPUPresentModeNativeFunction = SDL_WindowSupportsGPUPresentMode;

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
    public static bool WindowSupportsGPUPresentMode(IntPtr device, IntPtr window, GPUPresentMode presentMode)
    {
        return WindowSupportsGPUPresentModeNativeFunction(device, window, presentMode);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClaimWindowForGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ClaimWindowForGPUDevice(IntPtr device, IntPtr window);
    private delegate bool ClaimWindowForGPUDeviceNativeDelegate(IntPtr device, IntPtr window);
    private static ClaimWindowForGPUDeviceNativeDelegate ClaimWindowForGPUDeviceNativeFunction = SDL_ClaimWindowForGPUDevice;

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
    public static bool ClaimWindowForGPUDevice(IntPtr device, IntPtr window)
    {
        return ClaimWindowForGPUDeviceNativeFunction(device, window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseWindowFromGPUDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseWindowFromGPUDevice(IntPtr device, IntPtr window);
    private delegate void ReleaseWindowFromGPUDeviceNativeDelegate(IntPtr device, IntPtr window);
    private static ReleaseWindowFromGPUDeviceNativeDelegate ReleaseWindowFromGPUDeviceNativeFunction = SDL_ReleaseWindowFromGPUDevice;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseWindowFromGPUDevice(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// Unclaims a window, destroying its swapchain structure.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClaimWindowForGPUDevice"/>
    public static void ReleaseWindowFromGPUDevice(IntPtr device, IntPtr window)
    {
        ReleaseWindowFromGPUDeviceNativeFunction(device, window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUSwapchainParameters"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetGPUSwapchainParameters(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition, GPUPresentMode presentMode);
    private delegate bool SetGPUSwapchainParametersNativeDelegate(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition, GPUPresentMode presentMode);
    private static SetGPUSwapchainParametersNativeDelegate SetGPUSwapchainParametersNativeFunction = SDL_SetGPUSwapchainParameters;

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
    public static bool SetGPUSwapchainParameters(IntPtr device, IntPtr window, GPUSwapchainComposition swapchainComposition, GPUPresentMode presentMode)
    {
        return SetGPUSwapchainParametersNativeFunction(device, window, swapchainComposition, presentMode);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPUAllowedFramesInFlight"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetGPUAllowedFramesInFlight(IntPtr device, uint allowedFramesInFlight);
    private delegate bool SetGPUAllowedFramesInFlightNativeDelegate(IntPtr device, uint allowedFramesInFlight);
    private static SetGPUAllowedFramesInFlightNativeDelegate SetGPUAllowedFramesInFlightNativeFunction = SDL_SetGPUAllowedFramesInFlight;

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
    public static bool SetGPUAllowedFramesInFlight(IntPtr device, uint allowedFramesInFlight)
    {
        return SetGPUAllowedFramesInFlightNativeFunction(device, allowedFramesInFlight);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUSwapchainTextureFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GPUTextureFormat SDL_GetGPUSwapchainTextureFormat(IntPtr device, IntPtr window);
    private delegate GPUTextureFormat GetGPUSwapchainTextureFormatNativeDelegate(IntPtr device, IntPtr window);
    private static GetGPUSwapchainTextureFormatNativeDelegate GetGPUSwapchainTextureFormatNativeFunction = SDL_GetGPUSwapchainTextureFormat;

    /// <code>extern SDL_DECLSPEC SDL_GPUTextureFormat SDLCALL SDL_GetGPUSwapchainTextureFormat(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// <para>Obtains the texture format of the swapchain for the given window.</para>
    /// <para>Note that this format can change if the swapchain parameters change.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="window">an SDL_Window that has been claimed.</param>
    /// <returns>the texture format of the swapchain.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static GPUTextureFormat GetGPUSwapchainTextureFormat(IntPtr device, IntPtr window)
    {
        return GetGPUSwapchainTextureFormatNativeFunction(device, window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AcquireGPUSwapchainTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_AcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);
    private delegate bool AcquireGPUSwapchainTextureNativeDelegate(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);
    private static AcquireGPUSwapchainTextureNativeDelegate AcquireGPUSwapchainTextureNativeFunction = SDL_AcquireGPUSwapchainTexture;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AcquireGPUSwapchainTexture(SDL_GPUCommandBuffer *command_buffer, SDL_Window *window, SDL_GPUTexture **swapchain_texture, Uint32 *swapchain_texture_width, Uint32 *swapchain_texture_height);</code>
    /// <summary>
    /// <para>Acquire a texture to use in presentation.</para>
    /// <para>When a swapchain texture is acquired on a command buffer, it will
    /// automatically be submitted for presentation when the command buffer is
    /// submitted. The swapchain texture should only be referenced by the command
    /// buffer used to acquire it.</para>
    /// <para>If too many frames are in flight, this function will fill the swapchain
    /// texture handle with <c>null</c> and return <c>true</c>. This is not an error. This <c>null</c>
    /// pointer should not be passed back into SDL. Instead, it should be
    /// considered as an indication to wait.</para>
    /// <para>In VSYNC present mode (which is the default) this function may block on
    /// vblank.</para>
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
    public static bool AcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight)
    {
        return AcquireGPUSwapchainTextureNativeFunction(commandBuffer, window, out swapchainTexture, out swapchainTextureWidth, out swapchainTextureHeight);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUSwapchain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitForGPUSwapchain(IntPtr device, IntPtr window);
    private delegate bool WaitForGPUSwapchainNativeDelegate(IntPtr device, IntPtr window);
    private static WaitForGPUSwapchainNativeDelegate WaitForGPUSwapchainNativeFunction = SDL_WaitForGPUSwapchain;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitForGPUSwapchain(SDL_GPUDevice *device, SDL_Window *window);</code>
    /// <summary>
    /// <para>Blocks the thread until all presenting command buffers are finished
    /// executing.</para>
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
    public static bool WaitForGPUSwapchain(IntPtr device, IntPtr window)
    {
        return WaitForGPUSwapchainNativeFunction(device, window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitAndAcquireGPUSwapchainTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitAndAcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);
    private delegate bool WaitAndAcquireGPUSwapchainTextureNativeDelegate(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight);
    private static WaitAndAcquireGPUSwapchainTextureNativeDelegate WaitAndAcquireGPUSwapchainTextureNativeFunction = SDL_WaitAndAcquireGPUSwapchainTexture;

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
    public static bool WaitAndAcquireGPUSwapchainTexture(IntPtr commandBuffer, IntPtr window, out IntPtr swapchainTexture, out uint swapchainTextureWidth, out uint swapchainTextureHeight)
    {
        return WaitAndAcquireGPUSwapchainTextureNativeFunction(commandBuffer, window, out swapchainTexture, out swapchainTextureWidth, out swapchainTextureHeight);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SubmitGPUCommandBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SubmitGPUCommandBuffer(IntPtr commandBuffer);
    private delegate bool SubmitGPUCommandBufferNativeDelegate(IntPtr commandBuffer);
    private static SubmitGPUCommandBufferNativeDelegate SubmitGPUCommandBufferNativeFunction = SDL_SubmitGPUCommandBuffer;

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
    public static bool SubmitGPUCommandBuffer(IntPtr commandBuffer)
    {
        return SubmitGPUCommandBufferNativeFunction(commandBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SubmitGPUCommandBufferAndAcquireFence"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_SubmitGPUCommandBufferAndAcquireFence(IntPtr commandBuffer);
    private delegate IntPtr SubmitGPUCommandBufferAndAcquireFenceNativeDelegate(IntPtr commandBuffer);
    private static SubmitGPUCommandBufferAndAcquireFenceNativeDelegate SubmitGPUCommandBufferAndAcquireFenceNativeFunction = SDL_SubmitGPUCommandBufferAndAcquireFence;

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
    public static IntPtr SubmitGPUCommandBufferAndAcquireFence(IntPtr commandBuffer)
    {
        return SubmitGPUCommandBufferAndAcquireFenceNativeFunction(commandBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CancelGPUCommandBuffer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CancelGPUCommandBuffer(IntPtr commandBuffer);
    private delegate bool CancelGPUCommandBufferNativeDelegate(IntPtr commandBuffer);
    private static CancelGPUCommandBufferNativeDelegate CancelGPUCommandBufferNativeFunction = SDL_CancelGPUCommandBuffer;

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
    public static bool CancelGPUCommandBuffer(IntPtr commandBuffer)
    {
        return CancelGPUCommandBufferNativeFunction(commandBuffer);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUIdle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitForGPUIdle(IntPtr device);
    private delegate bool WaitForGPUIdleNativeDelegate(IntPtr device);
    private static WaitForGPUIdleNativeDelegate WaitForGPUIdleNativeFunction = SDL_WaitForGPUIdle;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitForGPUIdle(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Blocks the thread until the GPU is completely idle.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WaitForGPUFences(nint, bool, nint[], uint)"/>
    public static bool WaitForGPUIdle(IntPtr device)
    {
        return WaitForGPUIdleNativeFunction(device);
    }


    #region WaitForGPUFences
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUFences"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, IntPtr[] fences, uint numFences);
    private delegate bool WaitForGPUFencesArrayNativeDelegate(IntPtr device, bool waitAll, IntPtr[] fences, uint numFences);
    private static WaitForGPUFencesArrayNativeDelegate WaitForGPUFencesArrayNativeFunction = SDL_WaitForGPUFences;

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
    public static bool WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, IntPtr[] fences, uint numFences)
    {
        return WaitForGPUFencesArrayNativeFunction(device, waitAll, fences, numFences);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitForGPUFences"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, IntPtr fences, uint numFences);
    private delegate bool WaitForGPUFencesPointerNativeDelegate(IntPtr device, bool waitAll, IntPtr fences, uint numFences);
    private static WaitForGPUFencesPointerNativeDelegate WaitForGPUFencesPointerNativeFunction = SDL_WaitForGPUFences;

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
    public static bool WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, IntPtr fences, uint numFences)
    {
        return WaitForGPUFencesPointerNativeFunction(device, waitAll, fences, numFences);
    }

    /// <inheritdoc cref="WaitForGPUFences(nint, bool, nint[], uint)"/>
    public static unsafe bool WaitForGPUFences(IntPtr device, [MarshalAs(UnmanagedType.I1)] bool waitAll, ReadOnlySpan<IntPtr> fences, uint numFences)
    {
        fixed (IntPtr* pFences = fences)
        {
            return WaitForGPUFences(device, waitAll, (IntPtr)pFences, numFences);
        }
    }
    #endregion


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_QueryGPUFence"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_QueryGPUFence(IntPtr device, IntPtr fence);
    private delegate bool QueryGPUFenceNativeDelegate(IntPtr device, IntPtr fence);
    private static QueryGPUFenceNativeDelegate QueryGPUFenceNativeFunction = SDL_QueryGPUFence;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_QueryGPUFence(SDL_GPUDevice *device, SDL_GPUFence *fence);</code>
    /// <summary>
    /// Checks the status of a fence.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="fence">a fence.</param>
    /// <returns><c>true</c> if the fence is signaled, <c>false</c> if it is not.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    public static bool QueryGPUFence(IntPtr device, IntPtr fence)
    {
        return QueryGPUFenceNativeFunction(device, fence);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseGPUFence"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseGPUFence(IntPtr device, IntPtr fence);
    private delegate void ReleaseGPUFenceNativeDelegate(IntPtr device, IntPtr fence);
    private static ReleaseGPUFenceNativeDelegate ReleaseGPUFenceNativeFunction = SDL_ReleaseGPUFence;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseGPUFence(SDL_GPUDevice *device, SDL_GPUFence *fence);</code>
    /// <summary>
    /// Releases a fence obtained from <see cref="SubmitGPUCommandBufferAndAcquireFence"/>.
    /// <para>You must not reference the fence after calling this function.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="fence">a fence.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SubmitGPUCommandBufferAndAcquireFence"/>
    public static void ReleaseGPUFence(IntPtr device, IntPtr fence)
    {
        ReleaseGPUFenceNativeFunction(device, fence);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUTextureFormatTexelBlockSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GPUTextureFormatTexelBlockSize(GPUTextureFormat format);
    private delegate uint GPUTextureFormatTexelBlockSizeNativeDelegate(GPUTextureFormat format);
    private static GPUTextureFormatTexelBlockSizeNativeDelegate GPUTextureFormatTexelBlockSizeNativeFunction = SDL_GPUTextureFormatTexelBlockSize;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GPUTextureFormatTexelBlockSize(SDL_GPUTextureFormat format);</code>
    /// <summary>
    /// Obtains the texel block size for a texture format.
    /// </summary>
    /// <param name="format">the texture format you want to know the texel size of.</param>
    /// <returns>the texel block size of the texture format.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GPUTextureFormatTexelBlockSize(GPUTextureFormat format)
    {
        return GPUTextureFormatTexelBlockSizeNativeFunction(format);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUTextureSupportsFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GPUTextureSupportsFormat(IntPtr device, GPUTextureFormat format, GPUTextureType type, GPUTextureUsageFlags usage);
    private delegate bool GPUTextureSupportsFormatNativeDelegate(IntPtr device, GPUTextureFormat format, GPUTextureType type, GPUTextureUsageFlags usage);
    private static GPUTextureSupportsFormatNativeDelegate GPUTextureSupportsFormatNativeFunction = SDL_GPUTextureSupportsFormat;

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
    public static bool GPUTextureSupportsFormat(IntPtr device, GPUTextureFormat format, GPUTextureType type, GPUTextureUsageFlags usage)
    {
        return GPUTextureSupportsFormatNativeFunction(device, format, type, usage);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GPUTextureSupportsSampleCount"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GPUTextureSupportsSampleCount(IntPtr device, GPUTextureFormat format, GPUSampleCount sampleCount);
    private delegate bool GPUTextureSupportsSampleCountNativeDelegate(IntPtr device, GPUTextureFormat format, GPUSampleCount sampleCount);
    private static GPUTextureSupportsSampleCountNativeDelegate GPUTextureSupportsSampleCountNativeFunction = SDL_GPUTextureSupportsSampleCount;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GPUTextureSupportsSampleCount(SDL_GPUDevice *device, SDL_GPUTextureFormat format, SDL_GPUSampleCount sample_count);</code>
    /// <summary>
    /// Determines if a sample count for a texture format is supported.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="format">the texture format to check.</param>
    /// <param name="sampleCount">the sample count to check.</param>
    /// <returns>whether the sample count is supported for this texture format.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool GPUTextureSupportsSampleCount(IntPtr device, GPUTextureFormat format, GPUSampleCount sampleCount)
    {
        return GPUTextureSupportsSampleCountNativeFunction(device, format, sampleCount);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CalculateGPUTextureFormatSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CalculateGPUTextureFormatSize(GPUTextureFormat format, uint width, uint height, uint depthOrLayerCount);
    private delegate uint CalculateGPUTextureFormatSizeNativeDelegate(GPUTextureFormat format, uint width, uint height, uint depthOrLayerCount);
    private static CalculateGPUTextureFormatSizeNativeDelegate CalculateGPUTextureFormatSizeNativeFunction = SDL_CalculateGPUTextureFormatSize;

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
    public static uint CalculateGPUTextureFormatSize(GPUTextureFormat format, uint width, uint height, uint depthOrLayerCount)
    {
        return CalculateGPUTextureFormatSizeNativeFunction(format, width, height, depthOrLayerCount);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GDKSuspendGPU"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GDKSuspendGPU(IntPtr device);
    private delegate void GDKSuspendGPUNativeDelegate(IntPtr device);
    private static GDKSuspendGPUNativeDelegate GDKSuspendGPUNativeFunction = SDL_GDKSuspendGPU;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GDKSuspendGPU(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Call this to suspend GPU operation on Xbox after receiving the
    /// <see cref="EventType.DidEnterBackground"/> event.</para>
    /// <para>Do NOT call any SDL_GPU functions after calling this function! This must
    /// also be called before calling <see cref="GDKSuspendComplete"/>.</para>
    /// <para>This function MUST be called from the application's render thread.</para>
    /// </summary>
    /// <param name="device">device a GPU context.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddEventWatch"/>
    public static void GDKSuspendGPU(IntPtr device)
    {
        GDKSuspendGPUNativeFunction(device);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GDKResumeGPU"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_GDKResumeGPU(IntPtr device);
    private delegate void GDKResumeGPUNativeDelegate(IntPtr device);
    private static GDKResumeGPUNativeDelegate GDKResumeGPUNativeFunction = SDL_GDKResumeGPU;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GDKResumeGPU(SDL_GPUDevice *device);</code>
    /// <summary>
    /// <para>Call this to resume GPU operation on Xbox after receiving the
    /// <see cref="EventType.WillEnterForeground"/> event.</para>
    /// <para>When resuming, this function MUST be called before calling any other
    /// SDL_GPU functions.</para>
    /// <para>This function MUST be called from the application's render thread.</para>
    /// </summary>
    /// <param name="device">device a GPU context.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AddEventWatch"/>
    public static void GDKResumeGPU(IntPtr device)
    {
        GDKResumeGPUNativeFunction(device);
    }
}
