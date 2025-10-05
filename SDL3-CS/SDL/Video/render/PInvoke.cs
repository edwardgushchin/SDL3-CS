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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumRenderDrivers(void);</code>
    /// <summary>
    /// <para>Get the number of 2D rendering drivers available for the current display.</para>
    /// <para>A render driver is a set of code that handles rendering and texture
    /// management on a particular display. Normally there is only one, but some
    /// drivers may have several available with different capabilities.</para>
    /// <para>There may be none if SDL was compiled without render support.</para>
    /// </summary>
    /// <returns>the number of built in render drivers.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRenderer"/>
    /// <seealso cref="GetRenderDriver"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumRenderDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumRenderDrivers();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetRenderDriver(int index);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetRenderDriver(int index);</code>
    /// <summary>
    /// <para>Use this function to get the name of a built in 2D rendering driver.</para>
    /// <para>The list of rendering drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "opengl",
    /// "direct3d12" or "metal". These never have Unicode characters, and are not
    /// meant to be proper names.</para>
    /// </summary>
    /// <param name="index">the index of the rendering driver; the value ranges from 0 to
    /// <see cref="GetNumRenderDrivers"/> - 1.</param>
    /// <returns>the name of the rendering driver at the requested index, or <c>null</c>
    /// if an invalid index was specified.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumRenderDrivers"/>
    public static string? GetRenderDriver(int index)
    {
        var value = SDL_GetRenderDriver(index); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CreateWindowAndRenderer(const char *title, int width, int height, SDL_WindowFlags window_flags, SDL_Window **window, SDL_Renderer **renderer);</code>
    /// <summary>
    /// Create a window and default renderer.
    /// </summary>
    /// <param name="title">the title of the window, in UTF-8 encoding.</param>
    /// <param name="width">the width of the window.</param>
    /// <param name="height">the height of the window.</param>
    /// <param name="windowFlags">the flags used to create the window (see
    /// <see cref="CreateWindow"/>).</param>
    /// <param name="window">a pointer filled with the window, or <c>null</c> on error.</param>
    /// <param name="renderer">a pointer filled with the renderer, or <c>null</c> on error.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRenderer"/>
    /// <seealso cref="CreateWindow"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateWindowAndRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CreateWindowAndRenderer([MarshalAs(UnmanagedType.LPUTF8Str)] string title, int width, int height, WindowFlags windowFlags,
        out IntPtr window, out IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Renderer * SDLCALL SDL_CreateRenderer(SDL_Window *window, const char *name);</code>
    /// <summary>
    /// <para>Create a 2D rendering context for a window.</para>
    /// <para>If you want a specific renderer, you can specify its name here. A list of
    /// available renderers can be obtained by calling <see cref="GetRenderDriver"/> 
    /// multiple times, with indices from 0 to <see cref="GetNumRenderDrivers"/>-1. If you
    /// don't need a specific renderer, specify <c>null</c> and SDL will attempt to choose
    /// the best option for you, based on what is available on the user's system.</para>
    /// <para>If <c>name</c> is a comma-separated list, SDL will try each name, in the order
    /// listed, until one succeeds or all of them fail.</para>
    /// <para>By default the rendering size matches the window size in pixels, but you
    /// can call <see cref="SetRenderLogicalPresentation"/> to change the content size and
    /// scaling options.</para>
    /// </summary>
    /// <param name="window">the window where rendering is displayed.</param>
    /// <param name="name">the name of the rendering driver to initialize, or <c>null</c> to let
    /// SDL choose one.</param>
    /// <returns>a valid rendering context or <c>null</c> if there was an error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRendererWithProperties"/>
    /// <seealso cref="CreateSoftwareRenderer"/>
    /// <seealso cref="DestroyRenderer"/>
    /// <seealso cref="GetNumRenderDrivers"/>
    /// <seealso cref="GetRenderDriver"/>
    /// <seealso cref="GetRendererName"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateRenderer(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string? name);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Renderer * SDLCALL SDL_CreateRendererWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a 2D rendering context for a window, with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererCreateNameString"/>: the name of the rendering driver
    /// to use, if a specific one is desired</item>
    /// <item><see cref="Props.RendererCreateWindowPointer"/>: the window where rendering is
    /// displayed, required if this isn't a software renderer using a surface</item>
    /// <item><see cref="Props.RendererCreateSurfacePointer"/>: the surface where rendering
    /// is displayed, if you want a software renderer without a window</item>
    /// <item><see cref="Props.RendererCreateOutputColorspaceNumber"/>: an SDL_Colorspace
    /// value describing the colorspace for output to the display, defaults to
    /// <see cref="Colorspace.SRGB"/>. The direct3d11, direct3d12, and metal renderers
    /// support <see cref="Colorspace.SRGBLinear"/>, which is a linear color space and
    /// supports HDR output. If you select <see cref="Colorspace.SRGBLinear"/>, drawing
    /// still uses the sRGB colorspace, but values can go beyond 1.0 and float
    /// (linear) format textures can be used for HDR content.</item>
    /// <item><see cref="Props.RendererCreatePresentVSyncNumber"/>: non-zero if you want
    /// present synchronized with the refresh rate. This property can take any
    /// value that is supported by <see cref="SetRenderVSync"/> for the renderer.</item>
    /// </list>
    /// <para>With the SDL GPU renderer (since SDL 3.4.0):</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererCreateGPUShadersSPIRVBoolean"/>: the app is able to
    /// provide SPIR-V shaders to SDL_GPURenderState, optional.</item>
    /// <item><see cref="Props.RendererCreateGPUShadersDXILBoolean"/>: the app is able to
    /// provide DXIL shaders to SDL_GPURenderState, optional.</item>
    /// <item><see cref="Props.RendererCreateGPUShadersMSLBoolean"/>: the app is able to
    /// provide MSL shaders to SDL_GPURenderState, optional.</item>
    /// </list>
    /// <para>With the vulkan renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererCreateVulkanInstancePointer"/>: the VkInstance to use
    /// with the renderer, optional.</item>
    /// <item><see cref="Props.RendererCreateVulkanSurfaceNumber"/>: the VkSurfaceKHR to use
    /// with the renderer, optional.</item>
    /// <item><see cref="Props.RendererCreateVulkanPhysicalDevicePointer"/>: the
    /// VkPhysicalDevice to use with the renderer, optional.</item>
    /// <item><see cref="Props.RendererCreateVulkanDevicePointer"/>: the VkDevice to use
    /// with the renderer, optional.</item>
    /// <item><see cref="Props.RendererCreateVulkanGraphicsQueueFamilyIndexNumber"/>: the
    /// queue family index used for rendering.</item>
    /// <item><see cref="Props.RendererCreateVulkanPresentQueueFamilyIndexNumber"/>: the
    /// queue family index used for presentation.</item>
    /// </list>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>a valid rendering context or <c>null</c> if there was an error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProperties"/>
    /// <seealso cref="CreateRenderer"/>
    /// <seealso cref="CreateSoftwareRenderer"/>
    /// <seealso cref="DestroyRenderer"/>
    /// <seealso cref="GetRendererName"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateRendererWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateRendererWithProperties(uint props);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Renderer * SDLCALL SDL_CreateGPURenderer(SDL_Window *window, SDL_GPUShaderFormat format_flags, SDL_GPUDevice **device);</code>
    /// <summary>
    /// <para>Create a 2D GPU rendering context.</para>
    /// <para>The GPU device to use is passed in as a parameter. If this is <c>null</c>, then a
    /// device will be created normally and can be retrieved using
    /// <see cref="GetGPURendererDevice"/>.</para>
    /// <para>The window to use is passed in as a parameter. If this is <c>null</c>, then this
    /// will become an offscreen renderer. In that case, you should call
    /// <see cref="SetRenderTarget"/> to setup rendering to a texture, and then call
    /// <see cref="RenderPresent"/> normally to complete drawing a frame.</para>
    /// </summary>
    /// <param name="device">the window where rendering is displayed, or <c>null</c> to create an
    /// offscreen renderer.</param>
    /// <param name="window">a bitflag indicating which shader formats the app is
    /// able to provide.</param>
    /// <returns>a valid rendering context or <c>null</c> if there was an error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>If this function is called with a valid GPU device, it should
    /// be called on the thread that created the device. If this
    /// function is called with a valid window, it should be called
    /// on the thread that created the window.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="CreateRendererWithProperties"/>
    /// <seealso cref="GetGPURendererDevice"/>
    /// <seealso cref="CreateGPUShader"/>
    /// <seealso cref="CreateGPURenderState"/>
    /// <seealso cref="SetGPURenderState"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPURenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPURenderer(IntPtr device, IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUDevice * SDLCALL SDL_GetGPURendererDevice(SDL_Renderer *renderer);</code>
    /// <summary>
    /// Return the GPU device used by a renderer.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>the GPU device used by the renderer, or <c>null</c> if the renderer is
    /// not a GPU renderer; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPURendererDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetGPURendererDevice(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Renderer * SDLCALL SDL_CreateSoftwareRenderer(SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Create a 2D software rendering context for a surface.</para>
    /// <para>Two other API which can be used to create SDL_Renderer:
    /// <see cref="CreateRenderer"/> and <see cref="CreateWindowAndRenderer"/>. These can _also_
    /// create a software renderer, but they are intended to be used with an
    /// SDL_Window as the final destination and not an <see cref="Surface"/>.</para>
    /// </summary>
    /// <param name="surface">the <see cref="Surface"/> structure representing the surface where
    /// rendering is done.</param>
    /// <returns>a valid rendering context or <c>null</c> if there was an error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyRenderer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSoftwareRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSoftwareRenderer(IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Renderer * SDLCALL SDL_GetRenderer(SDL_Window *window);</code>
    /// <summary>
    /// Get the renderer associated with a window.
    /// </summary>
    /// <param name="window">the window to query.</param>
    /// <returns>the rendering context on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRenderer(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Window * SDLCALL SDL_GetRenderWindow(SDL_Renderer *renderer);</code>
    /// <summary>
    /// Get the window associated with a renderer.
    /// </summary>
    /// <param name="renderer">the renderer to query.</param>
    /// <returns>the window on success or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRenderWindow(IntPtr renderer);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRendererName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetRendererName(IntPtr renderer);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetRendererName(SDL_Renderer *renderer);</code>
    /// <summary>
    /// Get the name of a renderer.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>the name of the selected renderer, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRenderer"/>
    /// <seealso cref="CreateRendererWithProperties"/>
    public static string? GetRendererName(IntPtr renderer)
    {
        var value = SDL_GetRendererName(renderer); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetRendererProperties(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Get the properties associated with a renderer.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererNameString"/>: the name of the rendering driver</item>
    /// <item><see cref="Props.RendererWindowPointer"/>: the window where rendering is
    /// displayed, if any</item>
    /// <item><see cref="Props.RendererSurfacePointer"/>: the surface where rendering is
    /// displayed, if this is a software renderer without a window</item>
    /// <item><see cref="Props.RendererVSyncNumber"/>: the current vsync setting</item>
    /// <item><see cref="Props.RendererMaxTextureSizeNumber"/>: the maximum texture width
    /// and height</item>
    /// <item><see cref="Props.RendererTextureFormatsPointer"/>: a (const SDL_PixelFormat *)
    /// array of pixel formats, terminated with <see cref="PixelFormat.Unknown"/>,
    /// representing the available texture formats for this renderer.</item>
    /// <item><see cref="Props.RendererOutputColorspaceNumber"/>: an SDL_Colorspace value
    /// describing the colorspace for output to the display, defaults to
    /// <see cref="Colorspace.SRGB"/>.</item>
    /// <item><see cref="Props.RendererHDREnabledBoolean"/>: true if the output colorspace is
    /// <see cref="Colorspace.SRGBLinear"/> and the renderer is showing on a display with
    /// HDR enabled. This property can change dynamically when
    /// <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// <item><see cref="Props.RendererSDRWhitePointFloat"/>: the value of SDR white in the
    /// <see cref="Colorspace.SRGBLinear"/> colorspace. When HDR is enabled, this value is
    /// automatically multiplied into the color scale. This property can change
    /// dynamically when <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// <item><see cref="Props.RendererHDRHeadroomFloat"/>: the additional high dynamic range
    /// that can be displayed, in terms of the SDR white point. When HDR is not
    /// enabled, this will be 1.0. This property can change dynamically when
    /// <see cref="EventType.WindowHDRStateChanged"/> is sent.</item>
    /// </list>
    /// <para>With the direct3d renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererD3D9DevicePointer"/>: the IDirect3DDevice9 associated
    /// with the renderer</item>
    /// </list>
    /// <para>With the direct3d11 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererD3D11DevicePointer"/>: the ID3D11Device associated
    /// with the renderer</item>
    /// <item><see cref="Props.RendererD3D11SwapchainPointer"/>: the IDXGISwapChain1
    /// associated with the renderer. This may change when the window is resized.</item>
    /// </list>
    /// <para>With the direct3d12 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererD3D12DevicePointer"/>: the ID3D12Device associated
    /// with the renderer</item>
    /// <item><see cref="Props.RendererD3D12SwapchainPointer"/>: the IDXGISwapChain4
    /// associated with the renderer.</item>
    /// <item><see cref="Props.RendererD3D12CommandQueuePointer"/>: the ID3D12CommandQueue
    /// associated with the renderer</item>
    /// </list>
    /// <para>With the vulkan renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererVulkanInstancePointer"/>: the VkInstance associated
    /// with the renderer</item>
    /// <item><see cref="Props.RendererVulkanSurfaceNumber"/>: the VkSurfaceKHR associated
    /// with the renderer</item>
    /// <item><see cref="Props.RendererVulkanPhysicalDevicePointer"/>: the VkPhysicalDevice
    /// associated with the renderer</item>
    /// <item><see cref="Props.RendererVulkanDevicePointer"/>: the VkDevice associated with
    /// the renderer</item>
    /// <item><see cref="Props.RendererVulkanGraphicsQueueFamilyIndexNumber"/>: the queue
    /// family index used for rendering</item>
    /// <item><see cref="Props.RendererVulkanPresentQueueFamilyIndexNumber"/>: the queue
    /// family index used for presentation</item>
    /// <item><see cref="Props.RendererVulkanSwapchainImageCountNumber"/>: the number of
    /// swapchain images, or potential frames in flight, used by the Vulkan
    /// renderer</item>
    /// <para>With the gpu renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.RendererGPUDevicePointer"/>: the SDL_GPUDevice associated with
    /// the renderer</item>
    /// </list>
    /// </list>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRendererProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetRendererProperties(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderOutputSize(SDL_Renderer *renderer, int *w, int *h);</code>
    /// <summary>
    /// <para>Get the output size in pixels of a rendering context.</para>
    /// <para>This returns the true output size in pixels, ignoring any render targets or
    /// logical size and presentation.</para>
    /// <para>For the output size of the current rendering target, with logical size
    /// adjustments, use <see cref="GetCurrentRenderOutputSize"/> instead.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">a pointer filled in with the width in pixels.</param>
    /// <param name="h">a pointer filled in with the height in pixels.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCurrentRenderOutputSize"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderOutputSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderOutputSize(IntPtr renderer, out int w, out int h);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetCurrentRenderOutputSize(SDL_Renderer *renderer, int *w, int *h);</code>
    /// <summary>
    /// <para>Get the current output size in pixels of a rendering context.</para>
    /// <para>If a rendering target is active, this will return the size of the rendering
    /// target in pixels, otherwise return the value of <see cref="GetRenderOutputSize"/>.</para>
    /// <para>Rendering target or not, the output will be adjusted by the current
    /// logical presentation state, dictated by <see cref="SetRenderLogicalPresentation"/>.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">a pointer filled in with the current width.</param>
    /// <param name="h">a pointer filled in with the current height.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderOutputSize"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentRenderOutputSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetCurrentRenderOutputSize(IntPtr renderer, out int w, out int h);
    

    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL SDL_CreateTexture(SDL_Renderer *renderer, SDL_PixelFormat format, SDL_TextureAccess access, int w, int h);</code>
    /// <summary>
    /// Create a texture for a rendering context.
    /// </summary>
    /// <remarks>The contents of a texture when first created are not defined.</remarks>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="format">one of the enumerated values in <see cref="PixelFormat"/>.</param>
    /// <param name="access">one of the enumerated values in <see cref="TextureAccess"/></param>
    /// <param name="w">the width of the texture in pixels.</param>
    /// <param name="h">the height of the texture in pixels.</param>
    /// <returns>the created texture or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateTextureFromSurface"/>
    /// <seealso cref="CreateTextureWithProperties"/>
    /// <seealso cref="DestroyTexture"/>
    /// <seealso cref="GetTextureSize"/>
    /// <seealso cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTexture(IntPtr renderer, PixelFormat format, TextureAccess access, int w, int h);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL SDL_CreateTextureFromSurface(SDL_Renderer *renderer, SDL_Surface *surface);</code>
    /// <summary>
    /// <para>Create a texture from an existing surface.</para>
    /// <para>The surface is not modified or freed by this function.</para>
    /// <para>The <see cref="TextureAccess"/> hint for the created texture is
    /// <see cref="TextureAccess.Static"/>.</para>
    /// <para>The pixel format of the created texture may be different from the pixel
    /// format of the surface, and can be queried using the
    /// <see cref="Props.TextureFormatNumber"/> property.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="surface">the <see cref="Surface"/> structure containing pixel data used to fill
    /// the texture.</param>
    /// <returns>the created texture or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateTexture"/>
    /// <seealso cref="CreateTextureWithProperties"/>
    /// <seealso cref="DestroyTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTextureFromSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTextureFromSurface(IntPtr renderer, IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL SDL_CreateTextureWithProperties(SDL_Renderer *renderer, SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a texture for a rendering context with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateColorspaceNumber"/>: an <see cref="Colorspace"/> value
    /// describing the texture colorspace, defaults to <see cref="Colorspace.SRGBLinear"/>
    /// for floating point textures, <see cref="Colorspace.HDR10"/> for 10-bit textures,
    /// <see cref="Colorspace.SRGB"/> for other RGB textures and <see cref="Colorspace.JPEG"/> for
    /// YUV textures.</item>
    /// <item><see cref="Props.TextureCreateFormatNumber"/>: one of the enumerated values in
    /// <see cref="PixelFormat"/>, defaults to the best RGBA format for the renderer</item>
    /// <item><see cref="Props.TextureCreateAccessNumber"/>: one of the enumerated values in
    /// <see cref="TextureAccess"/>, defaults to <see cref="TextureAccess.Static"/></item>
    /// <item><see cref="Props.TextureCreateWidthNumber"/>: the width of the texture in
    /// pixels, required</item>
    /// <item><see cref="Props.TextureCreateHeightNumber"/>: the height of the texture in
    /// pixels, required</item>
    /// <item><see cref="Props.TextureCreatePalettePointer"/>: an SDL_Palette to use with
    /// palettized texture formats. This can be set later with
    /// <see cref="SetTexturePalette"/></item>
    /// <item><see cref="Props.TextureCreateSDRWhitePointFloat"/>: for HDR10 and floating
    /// point textures, this defines the value of 100% diffuse white, with higher
    /// values being displayed in the High Dynamic Range headroom. This defaults
    /// to 100 for HDR10 textures and 1.0 for floating point textures.</item>
    /// <item><see cref="Props.TextureCreateHDRHeadroomFloat"/>: for HDR10 and floating
    /// point textures, this defines the maximum dynamic range used by the
    /// content, in terms of the SDR white point. This would be equivalent to
    /// maxCLL / <see cref="Props.TextureCreateSDRWhitePointFloat"/> for HDR10 content.
    /// If this is defined, any values outside the range supported by the display
    /// will be scaled into the available HDR headroom, otherwise they are
    /// clipped.</item>
    /// </list>
    /// <para>With the direct3d11 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateD3D11TexturePointer"/>: the ID3D11Texture2D
    /// associated with the texture, if you want to wrap an existing texture.</item>
    /// <item><see cref="Props.TextureCreateD3D11TextureUPointer"/>: the ID3D11Texture2D
    /// associated with the U plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// <item><see cref="Props.TextureCreateD3D11TextureVPointer"/>: the ID3D11Texture2D
    /// associated with the V plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// </list>
    /// <para>With the direct3d12 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateD3D12TexturePointer"/>: the ID3D12Resource
    /// associated with the texture, if you want to wrap an existing texture.</item>
    /// <item><see cref="Props.TextureCreateD3D12TextureUPointer"/>: the ID3D12Resource
    /// associated with the U plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// <item><see cref="Props.TextureCreateD3D12TextureVPointer"/>: the ID3D12Resource
    /// associated with the V plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// </list>
    /// <para>With the metal renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateMetalPixelbufferPointer"/>: the CVPixelBufferRef
    /// associated with the texture, if you want to create a texture from an
    /// existing pixel buffer.</item>
    /// </list>
    /// <para>With the opengl renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateOpenGLTextureNumber"/> the GLuint texture
    /// associated with the texture, if you want to wrap an existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGLTextureUVNumber"/>: the GLuint texture
    /// associated with the UV plane of an NV12 texture, if you want to wrap an
    /// existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGlTextureUNumber"/>: the GLuint texture
    /// associated with the U plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGLTextureVNumber"/>: the GLuint texture
    /// associated with the V plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// </list>
    /// <para>With the opengles2 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateOpenGLES2TextureNumber"/>: the GLuint texture
    /// associated with the texture, if you want to wrap an existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGLES2TextureNumber"/>: the GLuint texture
    /// associated with the texture, if you want to wrap an existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGLES2TextureUVNumber"/>: the GLuint texture
    /// associated with the UV plane of an NV12 texture, if you want to wrap an
    /// existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGLES2TextureUNumber"/>: the GLuint texture
    /// associated with the U plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// <item><see cref="Props.TextureCreateOpenGLES2TextureVNumber"/>: the GLuint texture
    /// associated with the V plane of a YUV texture, if you want to wrap an
    /// existing texture.</item>
    /// </list>
    /// <para>With the vulkan renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureCreateVulkanTextureNumber"/>: the VkImage with layout
    /// VK_IMAGE_LAYOUT_SHADER_READ_ONLY_OPTIMAL associated with the texture, if
    /// you want to wrap an existing texture.</item>
    /// </list>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="props">the properties to use.</param>
    /// <returns>the created texture or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProperties"/>
    /// <seealso cref="CreateTexture"/>
    /// <seealso cref="CreateTextureFromSurface"/>
    /// <seealso cref="DestroyTexture"/>
    /// <seealso cref="GetTextureSize"/>
    /// <seealso cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTextureWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTextureWithProperties(IntPtr renderer, uint props);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetTextureProperties(SDL_Texture *texture);</code>
    /// <summary>
    /// <para>Get the properties associated with a texture.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureColorspaceNumber"/>: an SDL_Colorspace value describing
    /// the texture colorspace.</item>
    /// <item><see cref="Props.TextureFormatNumber"/>: one of the enumerated values in
    /// <see cref="PixelFormat"/>.</item>
    /// <item><see cref="Props.TextureAccessNumber"/>: one of the enumerated values in
    /// <see cref="TextureAccess"/>.</item>
    /// <item><see cref="Props.TextureWidthNumber"/>: the width of the texture in pixels.</item>
    /// <item><see cref="Props.TextureHeightNumber"/>: the height of the texture in pixels.</item>
    /// <item><see cref="Props.TextureSDRWhitePointFloat"/>: for HDR10 and floating point
    /// textures, this defines the value of 100% diffuse white, with higher
    /// values being displayed in the High Dynamic Range headroom. This defaults
    /// to 100 for HDR10 textures and 1.0 for other textures.</item>
    /// <item><see cref="Props.TextureHDRHeadroomFloat"/>: for HDR10 and floating point
    /// textures, this defines the maximum dynamic range used by the content, in
    /// terms of the SDR white point. If this is defined, any values outside the
    /// range supported by the display will be scaled into the available HDR
    /// headroom, otherwise they are clipped. This defaults to 1.0 for SDR
    /// textures, 4.0 for HDR10 textures, and no default for floating point
    /// textures.</item>
    /// </list>
    /// <para>With the direct3d11 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureD3D11TexturePointer"/>: the ID3D11Texture2D associated
    /// with the texture</item>
    /// <item><see cref="Props.TextureD3D11TextureUPointer"/>: the ID3D11Texture2D
    /// associated with the U plane of a YUV texture</item>
    /// <item><see cref="Props.TextureD3D11TextureVPointer"/>: the ID3D11Texture2D
    /// associated with the V plane of a YUV texture</item>
    /// </list>
    /// <para>With the direct3d12 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureD3D12TexturePointer"/>: the ID3D12Resource associated
    /// with the texture</item>
    /// <item><see cref="Props.TextureD3D12TextureUPointer"/>: the ID3D12Resource associated
    /// with the U plane of a YUV texture</item>
    /// <item><see cref="Props.TextureD3D12TextureVPointer"/>: the ID3D12Resource associated
    /// with the V plane of a YUV texture</item>
    /// </list>
    /// <para>With the vulkan renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureVulkanTextureNumber"/>: the VkImage associated with the
    /// texture</item>
    /// </list>
    /// <para>With the opengl renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureOpenGLTextureNumber"/>: the GLuint texture associated
    /// with the texture</item>
    /// <item><see cref="Props.TextureOpenGLTextureUVNumber"/>: the GLuint texture
    /// associated with the UV plane of an NV12 texture</item>
    /// <item><see cref="Props.TextureOpenGLTextureUNumber"/>: the GLuint texture associated
    /// with the U plane of a YUV texture</item>
    /// <item><see cref="Props.TextureOpenGLTextureVNumber"/>: the GLuint texture associated
    /// with the V plane of a YUV texture</item>
    /// <item><see cref="Props.TextureOpenGLTextureTargetNumber"/>: the GLenum for the
    /// texture target (`GL_TEXTURE_2D`, `GL_TEXTURE_RECTANGLE_ARB`, etc)</item>
    /// <item><see cref="Props.TextureOpenGLTexWFloat"/>: the texture coordinate width of
    /// the texture (0.0 - 1.0)</item>
    /// <item><see cref="Props.TextureOpenGLTexHFloat"/>: the texture coordinate height of
    /// the texture (0.0 - 1.0)</item>
    /// </list>
    /// <para>With the opengles2 renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureOpenGLES2TextureNumber"/>: the GLuint texture
    /// associated with the texture</item>
    /// <item><see cref="Props.TextureOpenGLES2TextureUVNumber"/>: the GLuint texture
    /// associated with the UV plane of an NV12 texture</item>
    /// <item><see cref="Props.TextureOpenGLES2TextureUNumber"/>: the GLuint texture
    /// associated with the U plane of a YUV texture</item>
    /// <item><see cref="Props.TextureOpenGLES2TextureVNumber"/>: the GLuint texture
    /// associated with the V plane of a YUV texture</item>
    /// <item><see cref="Props.TextureOpenGLES2TextureTargetNumber"/>: the GLenum for the
    /// texture target (`GL_TEXTURE_2D`, `GL_TEXTURE_EXTERNAL_OES`, etc)</item>
    /// </list>
    /// <para>With the gpu renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureOpenGLES2TextureTargetNumber"/>: the SDL_GPUTexture associated
    /// with the texture</item>
    /// </list>
    /// <para>With the gpu renderer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.TextureGPUTexturePointer"/>: the SDL_GPUTexture associated
    /// with the texture</item>
    /// <item><see cref="Props.TextureGPUTextureUVPointer"/>: the SDL_GPUTexture associated
    /// with the UV plane of an NV12 texture</item>
    /// <item><see cref="Props.TextureGPUTextureUPointer"/>: the SDL_GPUTexture associated
    /// with the U plane of a YUV texture</item>
    /// <item><see cref="Props.TextureGPUTextureVPointer"/>: the SDL_GPUTexture associated
    /// with the V plane of a YUV texture</item>
    /// </list>
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetTextureProperties(IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Renderer * SDLCALL SDL_GetRendererFromTexture(SDL_Texture *texture);</code>
    /// <summary>
    /// Get the renderer that created an SDL_Texture.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <returns>a pointer to the SDL_Renderer that created the texture, or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRendererFromTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRendererFromTexture(IntPtr texture);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureSize(SDL_Texture *texture, float *w, float *h);</code>
    /// <summary>
    /// Get the size of a texture, as floating point values.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="w">a pointer filled in with the width of the texture in pixels. This
    /// argument can be <c>null</c> if you don't need this information.</param>
    /// <param name="h">a pointer filled in with the height of the texture in pixels. This
    /// argument can be <c>null</c> if you don't need this information.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureSize(IntPtr texture, out float w, out float h);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTexturePalette(SDL_Texture *texture, SDL_Palette *palette);</code>
    /// <summary>
    /// <para>Set the palette used by a texture.</para>
    /// <para>Setting the palette keeps an internal reference to the palette, which can
    /// be safely destroyed afterwards.</para>
    /// <para>A single palette can be shared with many textures.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="palette">the <see cref="Palette"/> structure to use.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="CreatePalette"/>
    /// <seealso cref="GetTexturePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTexturePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTexturePalette(IntPtr texture, IntPtr palette);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Palette * SDLCALL SDL_GetTexturePalette(SDL_Texture *texture);</code>
    /// <summary>
    /// Get the palette used by a texture.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <returns>a pointer to the palette used by the texture, or <c>null</c> if there is
    /// no palette used.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="SetTexturePalette"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTexturePalette"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTexturePalette(IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextureColorMod(SDL_Texture *texture, Uint8 r, Uint8 g, Uint8 b);</code>
    /// <summary>
    /// <para>Set an additional color value multiplied into render copy operations.</para>
    /// <para>When this texture is rendered, during the copy operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:</para>
    /// <para><c>srcC = srcC * (color / 255)</c></para>
    /// <para>Color modulation is not always supported by the renderer; it will return
    /// false if color modulation is not supported.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="r">the red color value multiplied into copy operations.</param>
    /// <param name="g">the green color value multiplied into copy operations.</param>
    /// <param name="b">the blue color value multiplied into copy operations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextureColorMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextureColorModFloat(SDL_Texture *texture, float r, float g, float b);</code>
    /// <summary>
    /// <para>Set an additional color value multiplied into render copy operations.</para>
    /// <para>When this texture is rendered, during the copy operation each source color
    /// channel is modulated by the appropriate color value according to the
    /// following formula:</para>
    /// <para><c>srcC = srcC * color</c></para>
    /// <para>Color modulation is not always supported by the renderer; it will return
    /// false if color modulation is not supported.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="r">the red color value multiplied into copy operations.</param>
    /// <param name="g">the green color value multiplied into copy operations.</param>
    /// <param name="b">the blue color value multiplied into copy operations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextureColorModFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextureColorModFloat(IntPtr texture, float r, float g, float b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureColorMod(SDL_Texture *texture, Uint8 *r, Uint8 *g, Uint8 *b);</code>
    /// <summary>
    /// Get the additional color value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureAlphaMod"/>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureColorMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureColorModFloat(SDL_Texture *texture, float *r, float *g, float *b);</code>
    /// <summary>
    /// Get the additional color value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="r">a pointer filled in with the current red color value.</param>
    /// <param name="g">a pointer filled in with the current green color value.</param>
    /// <param name="b">a pointer filled in with the current blue color value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureAlphaModFloat"/>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureColorModFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureColorModFloat(IntPtr texture, out float r, out float g, out float b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextureAlphaMod(SDL_Texture *texture, Uint8 alpha);</code>
    /// <summary>
    /// <para>Set an additional alpha value multiplied into render copy operations.</para>
    /// <para>When this texture is rendered, during the copy operation the source alpha
    /// value is modulated by this alpha value according to the following formula:</para>
    /// <para><c>srcA = srcA * (alpha / 255)</c></para>
    /// <para>Alpha modulation is not always supported by the renderer; it will return
    /// false if alpha modulation is not supported.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="alpha">the source alpha value multiplied into copy operations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureAlphaMod"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureColorMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextureAlphaMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextureAlphaMod(IntPtr texture, byte alpha);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextureAlphaModFloat(SDL_Texture *texture, float alpha);</code>
    /// <summary>
    /// <para>Set an additional alpha value multiplied into render copy operations.</para>
    /// <para>When this texture is rendered, during the copy operation the source alpha
    /// value is modulated by this alpha value according to the following formula:</para>
    /// <para><c>srcA = srcA * alpha</c></para>
    /// <para>Alpha modulation is not always supported by the renderer; it will return
    /// false if alpha modulation is not supported.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="alpha">the source alpha value multiplied into copy operations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureAlphaModFloat"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    /// <seealso cref="SetTextureColorModFloat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextureAlphaModFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextureAlphaModFloat(IntPtr texture, float alpha);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureAlphaMod(SDL_Texture *texture, Uint8 *alpha);</code>
    /// <summary>
    /// Get the additional alpha value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureAlphaModFloat"/>
    /// <seealso cref="GetTextureColorMod"/>
    /// <seealso cref="SetTextureAlphaMod"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureAlphaMod"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureAlphaMod(IntPtr texture, out byte alpha);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureAlphaModFloat(SDL_Texture *texture, float *alpha);</code>
    /// <summary>
    /// Get the additional alpha value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="alpha">a pointer filled in with the current alpha value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureAlphaMod"/>
    /// <seealso cref="GetTextureColorModFloat"/>
    /// <seealso cref="SetTextureAlphaModFloat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureAlphaModFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureAlphaModFloat(IntPtr texture, out float alpha);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextureBlendMode(SDL_Texture *texture, SDL_BlendMode blendMode);</code>
    /// <summary>
    /// <para>Set the blend mode for a texture, used by <see cref="RenderTexture(nint, nint, nint, nint)"/>.</para>
    /// <para>If the blend mode is not supported, the closest supported mode is chosen
    /// and this function returns false.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="blendMode">the <see cref="BlendMode"/> to use for texture blending.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextureBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextureBlendMode(IntPtr texture, BlendMode blendMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureBlendMode(SDL_Texture *texture, SDL_BlendMode *blendMode);</code>
    /// <summary>
    /// Get the blend mode used for texture copy operations.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetTextureBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureBlendMode(IntPtr texture, out BlendMode blendMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTextureScaleMode(SDL_Texture *texture, SDL_ScaleMode scaleMode);</code>
    /// <summary>
    /// <para>Set the scale mode used for texture scale operations.</para>
    /// <para>The default texture scale mode is <see cref="ScaleMode.Linear"/>.</para>
    /// <para>If the scale mode is not supported, the closest supported mode is chosen. 
    /// Palettized textures will use <see cref="ScaleMode.PixelArt"/> instead of
    /// <see cref="ScaleMode.Linear"/>.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="scaleMode">the <seealso cref="ScaleMode"/> to use for texture scaling.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <seealso cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetTextureScaleMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTextureScaleMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTextureScaleMode(IntPtr texture, ScaleMode scaleMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTextureScaleMode(SDL_Texture *texture, SDL_ScaleMode *scaleMode);</code>
    /// <summary>
    /// Get the scale mode used for texture scale operations.
    /// </summary>
    /// <param name="texture">the texture to query.</param>
    /// <param name="scaleMode">a pointer filled in with the current scale mode.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetTextureScaleMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTextureScaleMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTextureScaleMode(IntPtr texture, out ScaleMode scaleMode);


    #region UpdateTexture
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateTexture(SDL_Texture *texture, const SDL_Rect *rect, const void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Update the given texture rectangle with new pixel data.</para>
    /// <para>The pixel data must be in the pixel format of the texture, which can be
    /// queried using the <see cref="Props.TextureFormatNumber"/> property.</para>
    /// <para>This is a fairly slow function, intended for use with static textures that
    /// do not change often.</para>
    /// <para>If the texture is intended to be updated often, it is preferred to create
    /// the texture as streaming and use the locking functions referenced below.
    /// While this function will work with streaming textures, for optimization
    /// reasons you may not get the pixels back if you lock the texture afterward.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <c>null</c>
    /// to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding
    /// between lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateTexture(SDL_Texture *texture, const SDL_Rect *rect, const void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Update the given texture rectangle with new pixel data.</para>
    /// <para>The pixel data must be in the pixel format of the texture, which can be
    /// queried using the <see cref="Props.TextureFormatNumber"/> property.</para>
    /// <para>This is a fairly slow function, intended for use with static textures that
    /// do not change often.</para>
    /// <para>If the texture is intended to be updated often, it is preferred to create
    /// the texture as streaming and use the locking functions referenced below.
    /// While this function will work with streaming textures, for optimization
    /// reasons you may not get the pixels back if you lock the texture afterward.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <c>null</c>
    /// to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding
    /// between lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateTexture(IntPtr texture, IntPtr rect, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pixels, int pitch);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateTexture(SDL_Texture *texture, const SDL_Rect *rect, const void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Update the given texture rectangle with new pixel data.</para>
    /// <para>The pixel data must be in the pixel format of the texture, which can be
    /// queried using the <see cref="Props.TextureFormatNumber"/> property.</para>
    /// <para>This is a fairly slow function, intended for use with static textures that
    /// do not change often.</para>
    /// <para>If the texture is intended to be updated often, it is preferred to create
    /// the texture as streaming and use the locking functions referenced below.
    /// While this function will work with streaming textures, for optimization
    /// reasons you may not get the pixels back if you lock the texture afterward.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <c>null</c>
    /// to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding
    /// between lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    public static unsafe bool UpdateTexture(IntPtr texture, IntPtr rect, Span<byte> pixels, int pitch)
    {
        fixed (byte* p = pixels)
        {
            return UpdateTexture(texture, rect, (nint)p, pitch);
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateTexture(SDL_Texture *texture, const SDL_Rect *rect, const void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Update the given texture rectangle with new pixel data.</para>
    /// <para>The pixel data must be in the pixel format of the texture, which can be
    /// queried using the <see cref="Props.TextureFormatNumber"/> property.</para>
    /// <para>This is a fairly slow function, intended for use with static textures that
    /// do not change often.</para>
    /// <para>If the texture is intended to be updated often, it is preferred to create
    /// the texture as streaming and use the locking functions referenced below.
    /// While this function will work with streaming textures, for optimization
    /// reasons you may not get the pixels back if you lock the texture afterward.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <c>null</c>
    /// to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding
    /// between lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateTexture(IntPtr texture, in Rect rect, IntPtr pixels, int pitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateTexture(SDL_Texture *texture, const SDL_Rect *rect, const void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Update the given texture rectangle with new pixel data.</para>
    /// <para>The pixel data must be in the pixel format of the texture, which can be
    /// queried using the <see cref="Props.TextureFormatNumber"/> property.</para>
    /// <para>This is a fairly slow function, intended for use with static textures that
    /// do not change often.</para>
    /// <para>If the texture is intended to be updated often, it is preferred to create
    /// the texture as streaming and use the locking functions referenced below.
    /// While this function will work with streaming textures, for optimization
    /// reasons you may not get the pixels back if you lock the texture afterward.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <c>null</c>
    /// to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding
    /// between lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateTexture(IntPtr texture, in Rect rect, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] pixels, int pitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateTexture(SDL_Texture *texture, const SDL_Rect *rect, const void *pixels, int pitch);</code>
    /// <summary>
    /// <para>Update the given texture rectangle with new pixel data.</para>
    /// <para>The pixel data must be in the pixel format of the texture, which can be
    /// queried using the <see cref="Props.TextureFormatNumber"/> property.</para>
    /// <para>This is a fairly slow function, intended for use with static textures that
    /// do not change often.</para>
    /// <para>If the texture is intended to be updated often, it is preferred to create
    /// the texture as streaming and use the locking functions referenced below.
    /// While this function will work with streaming textures, for optimization
    /// reasons you may not get the pixels back if you lock the texture afterward.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to update, or <c>null</c>
    /// to update the entire texture.</param>
    /// <param name="pixels">the raw pixel data in the format of the texture.</param>
    /// <param name="pitch">the number of bytes in a row of pixel data, including padding
    /// between lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    public static unsafe bool UpdateTexture(IntPtr texture, in Rect rect, Span<byte> pixels, int pitch)
    {
        fixed (byte* p = pixels)
        {
            return UpdateTexture(texture, rect, (nint)p, pitch);
        }
    }
    
    #endregion
    
    
    #region UpdateYUVTexture
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateYUVTexture(SDL_Texture *texture, const SDL_Rect *rect, const Uint8 *Yplane, int Ypitch, const Uint8 *Uplane, int Upitch, const Uint8 *Vplane, int Vpitch);</code>
    /// <summary>
    /// <para>Update a rectangle within a planar YV12 or IYUV texture with new pixel
    /// data.</para>
    /// <para>You can use <see cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/> as long as your pixel data is a contiguous
    /// block of Y and U/V planes in the proper order, but this function is
    /// available if your pixel data is not contiguous.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">a pointer to the rectangle of pixels to update, or <c>null</c> to
    /// update the entire texture.</param>
    /// <param name="yplane">the raw pixel data for the Y plane.</param>
    /// <param name="ypitch">the number of bytes between rows of pixel data for the Y
    /// plane.</param>
    /// <param name="uplane">the raw pixel data for the U plane.</param>
    /// <param name="upitch">the number of bytes between rows of pixel data for the U
    /// plane.</param>
    /// <param name="vplane">the raw pixel data for the V plane.</param>
    /// <param name="vpitch">the number of bytes between rows of pixel data for the V
    /// plane.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateYUVTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateYUVTexture(IntPtr texture, IntPtr rect, IntPtr yplane, int ypitch, IntPtr uplane, int upitch, IntPtr vplane, int vpitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateYUVTexture(SDL_Texture *texture, const SDL_Rect *rect, const Uint8 *Yplane, int Ypitch, const Uint8 *Uplane, int Upitch, const Uint8 *Vplane, int Vpitch);</code>
    /// <summary>
    /// <para>Update a rectangle within a planar YV12 or IYUV texture with new pixel
    /// data.</para>
    /// <para>You can use <see cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/> as long as your pixel data is a contiguous
    /// block of Y and U/V planes in the proper order, but this function is
    /// available if your pixel data is not contiguous.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">a pointer to the rectangle of pixels to update, or <c>null</c> to
    /// update the entire texture.</param>
    /// <param name="yplane">the raw pixel data for the Y plane.</param>
    /// <param name="ypitch">the number of bytes between rows of pixel data for the Y
    /// plane.</param>
    /// <param name="uplane">the raw pixel data for the U plane.</param>
    /// <param name="upitch">the number of bytes between rows of pixel data for the U
    /// plane.</param>
    /// <param name="vplane">the raw pixel data for the V plane.</param>
    /// <param name="vpitch">the number of bytes between rows of pixel data for the V
    /// plane.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UpdateNVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int)"/>
    /// <seealso cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateYUVTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateYUVTexture(IntPtr texture, in Rect rect, IntPtr yplane, int ypitch, IntPtr uplane, int upitch, IntPtr vplane, int vpitch);
    #endregion
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateNVTexture(SDL_Texture *texture, const SDL_Rect *rect, const Uint8 *Yplane, int Ypitch, const Uint8 *UVplane, int UVpitch);</code>
    /// <summary>
    /// <para>Update a rectangle within a planar NV12 or NV21 texture with new pixels.</para>
    /// <para>You can use <see cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/> as long as your pixel data is a contiguous
    /// block of NV12/21 planes in the proper order, but this function is available
    /// if your pixel data is not contiguous.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">a pointer to the rectangle of pixels to update, or <c>null</c> to
    /// update the entire texture.</param>
    /// <param name="yplane">the raw pixel data for the Y plane.</param>
    /// <param name="ypitch">the number of bytes between rows of pixel data for the Y
    /// plane.</param>
    /// <param name="uvplane">the raw pixel data for the UV plane.</param>
    /// <param name="uvpitch">the number of bytes between rows of pixel data for the UV
    /// plane.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateNVTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateNVTexture(IntPtr texture, IntPtr rect, IntPtr yplane, int ypitch, IntPtr uvplane, int uvpitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateNVTexture(SDL_Texture *texture, const SDL_Rect *rect, const Uint8 *Yplane, int Ypitch, const Uint8 *UVplane, int UVpitch);</code>
    /// <summary>
    /// <para>Update a rectangle within a planar NV12 or NV21 texture with new pixels.</para>
    /// <para>You can use <see cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/> as long as your pixel data is a contiguous
    /// block of NV12/21 planes in the proper order, but this function is available
    /// if your pixel data is not contiguous.</para>
    /// </summary>
    /// <param name="texture">the texture to update.</param>
    /// <param name="rect">a pointer to the rectangle of pixels to update, or <c>null</c> to
    /// update the entire texture.</param>
    /// <param name="yplane">the raw pixel data for the Y plane.</param>
    /// <param name="ypitch">the number of bytes between rows of pixel data for the Y
    /// plane.</param>
    /// <param name="uvplane">the raw pixel data for the UV plane.</param>
    /// <param name="uvpitch">the number of bytes between rows of pixel data for the UV
    /// plane.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UpdateTexture(IntPtr, IntPtr, IntPtr, int)"/>
    /// <seealso cref="UpdateYUVTexture(IntPtr, IntPtr, IntPtr, int, IntPtr, int, IntPtr, int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateNVTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UpdateNVTexture(IntPtr texture, in Rect rect, IntPtr yplane, int ypitch, IntPtr  uvplane, int uvpitch);
    
    
    //extern SDL_DECLSPEC bool SDLCALL SDL_LockTexture(SDL_Texture *texture, const SDL_Rect *rect, void **pixels, int *pitch);
    /// <summary>
    /// <para>Lock a portion of the texture for <b>write-only</b> pixel access.</para>
    /// <para>As an optimization, the pixels made available for editing don't necessarily
    /// contain the old texture data. This is a write-only operation, and if you
    /// need to keep a copy of the texture data you should do that at the
    /// application level.</para>
    /// <para>You must use <see cref="UnlockTexture"/> to unlock the pixels and apply any
    /// changes.</para>
    /// </summary>
    /// <param name="texture">the texture to lock for access, which was created with
    /// <see cref="TextureAccess.Streaming"/>.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to lock for access;
    /// <c>null</c> to lock the entire texture.</param>
    /// <param name="pixels">this is filled in with a pointer to the locked pixels,
    /// appropriately offset by the locked area.</param>
    /// <param name="pitch">this is filled in with the pitch of the locked pixels; the
    /// pitch is the length of one row in bytes.</param>
    /// <returns>true on success or false if the texture is not valid or was not
    /// created with <see cref="TextureAccess.Streaming"/>; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTextureToSurface(nint, nint, out nint)"/>
    /// <seealso cref="UnlockTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch);
    
    
    //extern SDL_DECLSPEC bool SDLCALL SDL_LockTexture(SDL_Texture *texture, const SDL_Rect *rect, void **pixels, int *pitch);
    /// <summary>
    /// <para>Lock a portion of the texture for <b>write-only</b> pixel access.</para>
    /// <para>As an optimization, the pixels made available for editing don't necessarily
    /// contain the old texture data. This is a write-only operation, and if you
    /// need to keep a copy of the texture data you should do that at the
    /// application level.</para>
    /// <para>You must use <see cref="UnlockTexture"/> to unlock the pixels and apply any
    /// changes.</para>
    /// </summary>
    /// <param name="texture">the texture to lock for access, which was created with
    /// <see cref="TextureAccess.Streaming"/>.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to lock for access;
    /// <c>null</c> to lock the entire texture.</param>
    /// <param name="pixels">this is filled in with a pointer to the locked pixels,
    /// appropriately offset by the locked area.</param>
    /// <param name="pitch">this is filled in with the pitch of the locked pixels; the
    /// pitch is the length of one row in bytes.</param>
    /// <returns>true on success or false if the texture is not valid or was not
    /// created with <see cref="TextureAccess.Streaming"/>; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTextureToSurface(nint, nint, out nint)"/>
    /// <seealso cref="UnlockTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockTexture(IntPtr texture, in Rect rect, out IntPtr pixels, out int pitch);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LockTextureToSurface(SDL_Texture *texture, const SDL_Rect *rect, SDL_Surface **surface);</code>
    /// <summary>
    /// <para>Lock a portion of the texture for <b>write-only</b> pixel access, and expose
    /// it as a SDL surface.</para>
    /// <para>Besides providing an <see cref="Surface"/> instead of raw pixel data, this function
    /// operates like <see cref="LockTexture(nint, nint, out nint, out int)"/>.</para>
    /// <para>As an optimization, the pixels made available for editing don't necessarily
    /// contain the old texture data. This is a write-only operation, and if you
    /// need to keep a copy of the texture data you should do that at the
    /// application level.</para>
    /// <para>You must use <see cref="UnlockTexture"/> to unlock the pixels and apply any
    /// changes.</para>
    /// <para>The returned surface is freed internally after calling <see cref="UnlockTexture"/>
    /// or <see cref="DestroyTexture"/>. The caller should not free it.</para>
    /// </summary>
    /// <param name="texture">the texture to lock for access, which must be created with
    /// <see cref="TextureAccess.Streaming"/>.</param>
    /// <param name="rect">a pointer to the rectangle to lock for access. If the rect is
    /// <c>null</c>, the entire texture will be locked.</param>
    /// <param name="surface">a pointer to an SDL surface of size <b>rect</b>. Don't assume
    /// any specific pixel content.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockTextureToSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockTextureToSurface(IntPtr texture, IntPtr rect, out IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LockTextureToSurface(SDL_Texture *texture, const SDL_Rect *rect, SDL_Surface **surface);</code>
    /// <summary>
    /// <para>Lock a portion of the texture for <b>write-only</b> pixel access, and expose
    /// it as a SDL surface.</para>
    /// <para>Besides providing an <see cref="Surface"/> instead of raw pixel data, this function
    /// operates like <see cref="LockTexture(nint, nint, out nint, out int)"/>.</para>
    /// <para>As an optimization, the pixels made available for editing don't necessarily
    /// contain the old texture data. This is a write-only operation, and if you
    /// need to keep a copy of the texture data you should do that at the
    /// application level.</para>
    /// <para>You must use <see cref="UnlockTexture"/> to unlock the pixels and apply any
    /// changes.</para>
    /// <para>The returned surface is freed internally after calling <see cref="UnlockTexture"/>
    /// or <see cref="DestroyTexture"/>. The caller should not free it.</para>
    /// </summary>
    /// <param name="texture">the texture to lock for access, which must be created with
    /// <see cref="TextureAccess.Streaming"/>.</param>
    /// <param name="rect">a pointer to the rectangle to lock for access. If the rect is
    /// <c>null</c>, the entire texture will be locked.</param>
    /// <param name="surface">a pointer to an SDL surface of size <b>rect</b>. Don't assume
    /// any specific pixel content.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    /// <seealso cref="UnlockTexture"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockTextureToSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockTextureToSurface(IntPtr texture, in Rect rect, out IntPtr surface);
    

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockTexture(SDL_Texture *texture);</code>
    /// <summary>
    /// <para>Unlock a texture, uploading the changes to video memory, if needed.</para>
    /// <para><b>Warning</b>: Please note that <see cref="LockTexture(nint, nint, out nint, out int)"/> is intended to be
    /// write-only; it will not guarantee the previous contents of the texture will
    /// be provided. You must fully initialize any area of a texture that you lock
    /// before unlocking it, as the pixels might otherwise be uninitialized memory.</para>
    /// <para>Which is to say: locking and immediately unlocking a texture can result in
    /// corrupted textures, depending on the renderer in use.</para>
    /// </summary>
    /// <param name="texture">a texture locked by <see cref="LockTexture(nint, nint, out nint, out int)"/>.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockTexture(nint, nint, out nint, out int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockTexture(IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderTarget(SDL_Renderer *renderer, SDL_Texture *texture);</code>
    /// <summary>
    /// <para>Set a texture as the current rendering target.</para>
    /// <para>The default render target is the window for which the renderer was created.
    /// To stop rendering to a texture and render to the window again, call this
    /// function with a <c>null</c> <c>texture</c>.</para>
    /// <para>Viewport, cliprect, scale, and logical presentation are unique to each
    /// render target. Get and set functions for these states apply to the current
    /// render target set by this function, and those states persist on each target
    /// when the current render target changes.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">the targeted texture, which must be created with the
    /// <see cref="TextureAccess.Target"/> flag, or <c>null</c> to render to the
    /// window instead of a texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderTarget"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderTarget"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderTarget(IntPtr renderer, IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Texture * SDLCALL SDL_GetRenderTarget(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Get the current render target.</para>
    /// <para>The default render target is the window for which the renderer was created,
    /// and is reported a <c>null</c> here.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>the current render target or <c>null</c> for the default render target.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderTarget"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderTarget"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRenderTarget(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderLogicalPresentation(SDL_Renderer *renderer, int w, int h, SDL_RendererLogicalPresentation mode);</code>
    /// <summary>
    /// <para>Set a device-independent resolution and presentation mode for rendering.</para>
    /// <para>This function sets the width and height of the logical rendering output.
    /// The renderer will act as if the current render target is always the
    /// requested dimensions, scaling to the actual resolution as necessary.</para>
    /// <para>This can be useful for games that expect a fixed size, but would like to
    /// scale the output to whatever is available, regardless of how a user resizes
    /// a window, or if the display is high DPI.</para>
    /// <para>Logical presentation can be used with both render target textures
    /// and the renderer's window; the state is unique to each render target, and
    /// this function sets the state for the current render target. It might be
    /// useful to draw to a texture that matches the window dimensions with logical
    /// presentation enabled, and then draw that texture across the entire window
    /// with logical presentation disabled. Be careful not to render both with
    /// logical presentation enabled, however, as this could produce
    /// double-letterboxing, etc.</para>
    /// <para>You can disable logical coordinates by setting the mode to
    /// <see cref="RendererLogicalPresentation.Disabled"/>, and in that case you get the full pixel
    /// resolution of the render target; it is safe to toggle logical presentation
    /// during the rendering of a frame: perhaps most of the rendering is done to
    /// specific dimensions but to make fonts look sharp, the app turns off logical
    /// presentation while drawing text, for example.</para>
    /// <para>You can convert coordinates in an event into rendering coordinates using
    /// <see cref="ConvertEventToRenderCoordinates"/>.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">the width of the logical resolution.</param>
    /// <param name="h">the height of the logical resolution.</param>
    /// <param name="mode">the presentation mode used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ConvertEventToRenderCoordinates"/>
    /// <seealso cref="GetRenderLogicalPresentation"/>
    /// <seealso cref="GetRenderLogicalPresentationRect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderLogicalPresentation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderLogicalPresentation(IntPtr renderer, int w, int h, RendererLogicalPresentation mode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderLogicalPresentation(SDL_Renderer *renderer, int *w, int *h, SDL_RendererLogicalPresentation *mode);</code>
    /// <summary>
    /// <para>Get device independent resolution and presentation mode for rendering.</para>
    /// <para>This function gets the width and height of the logical rendering output, or
    /// 0 if a logical resolution is not enabled.</para>
    /// <para>Each render target has its own logical presentation state. This function
    /// gets the state for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="w">an int filled with the logical presentation width.</param>
    /// <param name="h">an int filled with the logical presentation height.</param>
    /// <param name="mode">a variable filled with the logical presentation mode being
    /// used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderLogicalPresentation"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderLogicalPresentation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderLogicalPresentation(IntPtr renderer, out int w, out int h, out RendererLogicalPresentation mode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderLogicalPresentationRect(SDL_Renderer *renderer, SDL_FRect *rect);</code>
    /// <summary>
    /// <para>Get the final presentation rectangle for rendering.</para>
    /// <para>This function returns the calculated rectangle used for logical
    /// presentation, based on the presentation mode and output size. If logical
    /// presentation is disabled, it will fill the rectangle with the output size,
    /// in pixels.</para>
    /// <para>Each render target has its own logical presentation state. This function
    /// gets the rectangle for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">a pointer filled in with the final presentation rectangle, may
    /// be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderLogicalPresentation"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderLogicalPresentationRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderLogicalPresentationRect(IntPtr renderer, out FRect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderCoordinatesFromWindow(SDL_Renderer *renderer, float window_x, float window_y, float *x, float *y);</code>
    /// <summary>
    /// <para>Get a point in render coordinates when given a point in window coordinates.</para>
    /// <para>This takes into account several states:</para>
    /// <list type="bullet">
    /// <item>The window dimensions.</item>
    /// <item>The logical presentation settings (<see cref="SetRenderLogicalPresentation"/>)</item>
    /// <item>The scale (<see cref="SetRenderScale"/>)</item>
    /// <item>The viewport (<see cref="SetRenderViewport(nint, nint)"/>)</item>
    /// </list>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="windowx">the x coordinate in window coordinates.</param>
    /// <param name="windowy">the y coordinate in window coordinates.</param>
    /// <param name="x">a pointer filled with the x coordinate in render coordinates.</param>
    /// <param name="y">a pointer filled with the y coordinate in render coordinates.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderLogicalPresentation"/>
    /// <seealso cref="SetRenderScale"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderCoordinatesFromWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderCoordinatesFromWindow(IntPtr renderer, float windowx, float windowy, out float x, out float y);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderCoordinatesToWindow(SDL_Renderer *renderer, float x, float y, float *window_x, float *window_y);</code>
    /// <summary>
    /// <para>Get a point in window coordinates when given a point in render coordinates.</para>
    /// <para>This takes into account several states:</para>
    /// <list type="bullet">
    /// <item>The window dimensions.</item>
    /// <item>The logical presentation settings (<see cref="SetRenderLogicalPresentation"/>)</item>
    /// <item>The scale (<see cref="SetRenderScale"/>)</item>
    /// <item>The viewport (<see cref="SetRenderViewport(nint, nint)"/>)</item>
    /// </list>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="x">the x coordinate in render coordinates.</param>
    /// <param name="y">the y coordinate in render coordinates.</param>
    /// <param name="windowx">a pointer filled with the x coordinate in window
    /// coordinates.</param>
    /// <param name="windowy">a pointer filled with the y coordinate in window
    /// coordinates.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderLogicalPresentation"/>
    /// <seealso cref="SetRenderScale"/>
    /// <seealso cref="SetRenderViewport(nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderCoordinatesToWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderCoordinatesToWindow(IntPtr renderer, float x, float y, out float windowx, out float windowy);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertEventToRenderCoordinates(SDL_Renderer *renderer, SDL_Event *event);</code>
    /// <summary>
    /// <para>Convert the coordinates in an event to render coordinates.</para>
    /// <para>This takes into account several states:</para>
    /// <list type="bullet">
    /// <item>The window dimensions.</item>
    /// <item>The logical presentation settings (<see cref="SetRenderLogicalPresentation"/>)</item>
    /// <item>The scale (<see cref="SetRenderScale"/>)</item>
    /// <item>The viewport (<see cref="SetRenderViewport(nint, nint)"/>)</item>
    /// </list>
    /// <para>Various event types are converted with this function: mouse, touch, pen,
    /// etc.</para>
    /// <para>Touch coordinates are converted from normalized coordinates in the window
    /// to non-normalized rendering coordinates.</para>
    /// <para>Relative mouse coordinates (xrel and yrel event fields) are _also_
    /// converted. Applications that do not want these fields converted should
    /// use <see cref="RenderCoordinatesFromWindow"/> on the specific event fields instead
    /// of converting the entire event structure.</para>
    /// <para>Once converted, coordinates may be outside the rendering area.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="event">the event to modify.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderCoordinatesFromWindow"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_ConvertEventToRenderCoordinates"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool ConvertEventToRenderCoordinates(IntPtr renderer, ref Event @event);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderViewport(SDL_Renderer *renderer, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Set the drawing area for rendering on the current target.</para>
    /// <para>Drawing will clip to this area (separately from any clipping done with
    /// <see cref="SetRenderClipRect(nint, in Rect)"/>), and the top left of the area will become coordinate
    /// (0, 0) for future drawing commands.</para>
    /// <para>The area's width and height must be >= 0.</para>
    /// <para>Each render target has its own viewport. This function sets the viewport
    /// for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the drawing area, or <c>null</c>
    /// to set the viewport to the entire target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderViewport"/>
    /// <seealso cref="RenderViewportSet"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderViewport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderViewport(IntPtr renderer, IntPtr rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderViewport(SDL_Renderer *renderer, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Set the drawing area for rendering on the current target.</para>
    /// <para>Drawing will clip to this area (separately from any clipping done with
    /// <see cref="SetRenderClipRect(nint, in Rect)"/>), and the top left of the area will become coordinate
    /// (0, 0) for future drawing commands.</para>
    /// <para>The area's width and height must be >= 0.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">the <see cref="Rect"/> structure representing the drawing area, or <c>null</c>
    /// to set the viewport to the entire target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderViewport"/>
    /// <seealso cref="RenderViewportSet"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderViewport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderViewport(IntPtr renderer, Rect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderViewport(SDL_Renderer *renderer, SDL_Rect *rect);</code>
    /// <summary>
    /// Get the drawing area for the current target.
    /// <para>Each render target has its own viewport. This function gets the viewport
    /// for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect"/> structure filled in with the current drawing area.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderViewportSet"/>
    /// <seealso cref="SetRenderViewport(nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderViewport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderViewport(IntPtr renderer, out Rect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderViewportSet(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Return whether an explicit rectangle was set as the viewport.</para>
    /// <para>This is useful if you're saving and restoring the viewport and want to know
    /// whether you should restore a specific rectangle or NULL.</para>
    /// <para>Each render target has its own viewport. This function checks the viewport
    /// for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>true if the viewport was set to a specific rectangle, or false if
    /// it was set to <c>null</c> (the entire target).</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderViewport"/>
    /// <seealso cref="SetRenderViewport(nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderViewportSet"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderViewportSet(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderSafeArea(SDL_Renderer *renderer, SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Get the safe area for rendering within the current viewport.</para>
    /// <para>Some devices have portions of the screen which are partially obscured or
    /// not interactive, possibly due to on-screen controls, curved edges, camera
    /// notches, TV overscan, etc. This function provides the area of the current
    /// viewport which is safe to have interactible content. You should continue
    /// rendering into the rest of the render target, but it should not contain
    /// visually important or interactible content.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">a pointer filled in with the area that is safe for interactive
    /// content.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderSafeArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderSafeArea(IntPtr renderer, out Rect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderClipRect(SDL_Renderer *renderer, const SDL_Rect *rect);</code>
    /// <summary>
    /// Set the clip rectangle for rendering on the specified target.
    /// <para>Each render target has its own clip rectangle. This function
    /// sets the cliprect for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the clip area, relative to
    /// the viewport, or <c>null</c> to disable clipping.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderClipRect"/>
    /// <seealso cref="RenderClipEnabled"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderClipRect(IntPtr renderer, IntPtr rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderClipRect(SDL_Renderer *renderer, const SDL_Rect *rect);</code>
    /// <summary>
    /// Set the clip rectangle for rendering on the specified target.
    /// <para>Each render target has its own clip rectangle. This function
    /// sets the cliprect for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the clip area, relative to
    /// the viewport, or <c>null</c> to disable clipping.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderClipRect"/>
    /// <seealso cref="RenderClipEnabled"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderClipRect(IntPtr renderer, in Rect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderClipRect(SDL_Renderer *renderer, SDL_Rect *rect);</code>
    /// <summary>
    /// Get the clip rectangle for the current target.
    /// <para>Each render target has its own clip rectangle. This function
    /// gets the cliprect for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect"/> structure filled in with the current clipping area
    /// or an empty rectangle if clipping is disabled.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderClipEnabled"/>
    /// <seealso cref="SetRenderClipRect(nint, in Rect)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderClipRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderClipRect(IntPtr renderer, out Rect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderClipEnabled(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Get whether clipping is enabled on the given render target.</para>
    /// <para> Each render target has its own clip rectangle. This function
    /// checks the cliprect for the current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns><c>true</c> if clipping is enabled or <c>false</c> if not; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderClipRect"/>
    /// <seealso cref="SetRenderClipRect(nint, in Rect)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderClipEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderClipEnabled(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderScale(SDL_Renderer *renderer, float scaleX, float scaleY);</code>
    /// <summary>
    /// <para>Set the drawing scale for rendering on the current target.</para>
    /// <para>The drawing coordinates are scaled by the x/y scaling factors before they
    /// are used by the renderer. This allows resolution independent drawing with a
    /// single coordinate system.</para>
    /// <para>If this results in scaling or subpixel drawing by the rendering backend, it
    /// will be handled using the appropriate quality hints. For best results use
    /// integer scaling factors.</para>
    /// <para>Each render target has its own scale. This function sets the scale for the
    /// current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="scalex">the horizontal scaling factor.</param>
    /// <param name="scaley">the vertical scaling factor.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderScale"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderScale(IntPtr renderer, float scalex, float scaley);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderScale(SDL_Renderer *renderer, float *scaleX, float *scaleY);</code>
    /// <summary>
    /// Get the drawing scale for the current target.
    /// <para>Each render target has its own scale. This function gets the scale for the
    /// current render target.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="scalex">a pointer filled in with the horizontal scaling factor.</param>
    /// <param name="scaley">a pointer filled in with the vertical scaling factor.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderScale"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderScale(IntPtr renderer, out float scalex, out float scaley);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderDrawColor(SDL_Renderer *renderer, Uint8 r, Uint8 g, Uint8 b, Uint8 a);</code>
    /// <summary>
    /// <para>Set the color used for drawing operations.</para>
    /// <para>Set the color for drawing or filling rectangles, lines, and points, and for
    /// <see cref="RenderClear"/>.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">the alpha value used to draw on the rendering target; usually
    /// <see cref="AlphaOpaque"/> (255). Use <see cref="SetRenderDrawBlendMode"/> to
    /// specify how the alpha channel is used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderDrawColor"/>
    /// <seealso cref="SetRenderDrawColorFloat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderDrawColor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderDrawColorFloat(SDL_Renderer *renderer, float r, float g, float b, float a);</code>
    /// <summary>
    /// <para>Set the color used for drawing operations (Rect, Line and Clear).</para>
    /// <para>Set the color for drawing or filling rectangles, lines, and points, and for
    /// <see cref="RenderClear"/>.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">the alpha value used to draw on the rendering target. Use
    /// <see cref="SetRenderDrawBlendMode"/> to specify how the alpha channel is
    /// used.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderDrawColorFloat"/>
    /// <seealso cref="SetRenderDrawColor"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderDrawColorFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderDrawColorFloat(IntPtr renderer, float r, float g, float b, float a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderDrawColor(SDL_Renderer *renderer, Uint8 *r, Uint8 *g, Uint8 *b, Uint8 *a);</code>
    /// <summary>
    /// Get the color used for drawing operations (Rect, Line and Clear).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">a pointer filled in with the red value used to draw on the
    /// rendering target.</param>
    /// <param name="g">a pointer filled in with the green value used to draw on the
    /// rendering target.</param>
    /// <param name="b">a pointer filled in with the blue value used to draw on the
    /// rendering target.</param>
    /// <param name="a">a pointer filled in with the alpha value used to draw on the
    /// rendering target; usually <see cref="AlphaOpaque"/> (255).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderDrawColorFloat"/>
    /// <seealso cref="SetRenderDrawColor"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderDrawColor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderDrawColorFloat(SDL_Renderer *renderer, float *r, float *g, float *b, float *a);</code>
    /// <summary>
    /// Get the color used for drawing operations (Rect, Line and Clear).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">a pointer filled in with the red value used to draw on the
    /// rendering target.</param>
    /// <param name="g">a pointer filled in with the green value used to draw on the
    /// rendering target.</param>
    /// <param name="b">a pointer filled in with the blue value used to draw on the
    /// rendering target.</param>
    /// <param name="a">a pointer filled in with the alpha value used to draw on the
    /// rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call SDL_GetError() for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderDrawColorFloat"/>
    /// <seealso cref="GetRenderDrawColor"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderDrawColorFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderDrawColorFloat(IntPtr renderer, out float r, out float g, out float b, out float a);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderColorScale(SDL_Renderer *renderer, float scale);</code>
    /// <summary>
    /// <para>Set the color scale used for render operations.</para>
    /// <para>The color scale is an additional scale multiplied into the pixel color
    /// value while rendering. This can be used to adjust the brightness of colors
    /// during HDR rendering, or changing HDR video brightness when playing on an
    /// SDR display.</para>
    /// <para>The color scale does not affect the alpha channel, only the color
    /// brightness.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="scale">the color scale value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderColorScale"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderColorScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderColorScale(IntPtr renderer, float scale);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderColorScale(SDL_Renderer *renderer, float *scale);</code>
    /// <summary>
    /// Get the color scale used for render operations.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="scale">a pointer filled in with the current color scale value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderColorScale"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderColorScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderColorScale(IntPtr renderer, out float scale);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderDrawBlendMode(SDL_Renderer *renderer, SDL_BlendMode blendMode);</code>
    /// <summary>
    /// <para>Set the blend mode used for drawing operations (Fill and Line).</para>
    /// <para>If the blend mode is not supported, the closest supported mode is chosen.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="blendMode">the <see cref="BlendMode"/> to use for blending.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderDrawBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderDrawBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderDrawBlendMode(IntPtr renderer, BlendMode blendMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderDrawBlendMode(SDL_Renderer *renderer, SDL_BlendMode *blendMode);</code>
    /// <summary>
    /// Get the blend mode used for drawing operations.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="blendMode">a pointer filled in with the current <see cref="BlendMode"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderDrawBlendMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderDrawBlendMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderDrawBlendMode(IntPtr renderer, out BlendMode blendMode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderClear(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Clear the current rendering target with the drawing color.</para>
    /// <para>This function clears the entire rendering target, ignoring the viewport and
    /// the clip rectangle. Note, that clearing will also set/fill all pixels of
    /// the rendering target to current renderer draw color, so make sure to invoke
    /// <see cref="SetRenderDrawColor"/> when needed.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderDrawColor"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderClear"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderClear(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderPoint(SDL_Renderer *renderer, float x, float y);</code>
    /// <summary>
    /// Draw a point on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw a point.</param>
    /// <param name="x">the x coordinate of the point.</param>
    /// <param name="y">the y coordinate of the point.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderPoints"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderPoint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderPoint(IntPtr renderer, float x, float y);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderPoints(SDL_Renderer *renderer, const SDL_FPoint *points, int count);</code>
    /// <summary>
    /// Draw multiple points on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw multiple points.</param>
    /// <param name="points">the points to draw.</param>
    /// <param name="count">the number of points to draw.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderPoint"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderPoints"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderPoints(IntPtr renderer, FPoint[] points, int count);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderLine(SDL_Renderer *renderer, float x1, float y1, float x2, float y2);</code>
    /// <summary>
    /// Draw a line on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw a line.</param>
    /// <param name="x1">the x coordinate of the start point.</param>
    /// <param name="y1">the y coordinate of the start point.</param>
    /// <param name="x2">the x coordinate of the end point.</param>
    /// <param name="y2">the y coordinate of the end point.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderLines"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderLine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderLine(IntPtr renderer, float x1, float y1, float x2, float y2);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderLines(SDL_Renderer *renderer, const SDL_FPoint *points, int count);</code>
    /// <summary>
    /// Draw a series of connected lines on the current rendering target at
    /// subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw multiple lines.</param>
    /// <param name="points">the points along the lines.</param>
    /// <param name="count">the number of points, drawing count-1 lines.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderLine"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderLines"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderLines(IntPtr renderer, FPoint[] points, int count);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderRect(SDL_Renderer *renderer, const SDL_FRect *rect);</code>
    /// <summary>
    /// Draw a rectangle on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw a rectangle.</param>
    /// <param name="rect">a pointer to the destination rectangle, or <c>null</c> to outline the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderRect(IntPtr renderer, IntPtr rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderRect(SDL_Renderer *renderer, const SDL_FRect *rect);</code>
    /// <summary>
    /// Draw a rectangle on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw a rectangle.</param>
    /// <param name="rect">a pointer to the destination rectangle, or <c>null</c> to outline the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderRect(IntPtr renderer, in FRect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderRects(SDL_Renderer *renderer, const SDL_FRect *rects, int count);</code>
    /// <summary>
    /// Draw some number of rectangles on the current rendering target at subpixel
    /// precision.
    /// </summary>
    /// <param name="renderer">the renderer which should draw multiple rectangles.</param>
    /// <param name="rects">a pointer to an array of destination rectangles.</param>
    /// <param name="count">the number of rectangles.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <see cref="RenderRect(nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderRects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderRects(IntPtr renderer, FRect[] rects, int count);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderFillRect(SDL_Renderer *renderer, const SDL_FRect *rect);</code>
    /// <summary>
    /// Fill a rectangle on the current rendering target with the drawing color at
    /// subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should fill a rectangle.</param>
    /// <param name="rect">a pointer to the destination rectangle, or <c>null</c> for the entire
    /// rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderFillRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderFillRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderFillRect(IntPtr renderer, IntPtr rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderFillRect(SDL_Renderer *renderer, const SDL_FRect *rect);</code>
    /// <summary>
    /// Fill a rectangle on the current rendering target with the drawing color at
    /// subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should fill a rectangle.</param>
    /// <param name="rect">a pointer to the destination rectangle, or <c>null</c> for the entire
    /// rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderFillRects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderFillRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderFillRect(IntPtr renderer, in FRect rect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderFillRects(SDL_Renderer *renderer, const SDL_FRect *rects, int count);</code>
    /// <summary>
    /// Fill some number of rectangles on the current rendering target with the
    /// drawing color at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should fill multiple rectangles.</param>
    /// <param name="rect">a pointer to an array of destination rectangles.</param>
    /// <param name="count">the number of rectangles.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderFillRect(nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderFillRects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderFillRects(IntPtr renderer, FRect[] rect, int count);

    
    #region RenderTexture
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel
    /// precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTextureRotated(IntPtr, IntPtr, IntPtr, IntPtr, double, IntPtr, FlipMode)"/>
    /// <seealso cref="RenderTextureTiled(IntPtr, IntPtr, IntPtr, float, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel
    /// precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTextureRotated(IntPtr, IntPtr, IntPtr, IntPtr, double, IntPtr, FlipMode)"/>
    /// <seealso cref="RenderTextureTiled(IntPtr, IntPtr, IntPtr, float, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel
    /// precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTextureRotated(IntPtr, IntPtr, IntPtr, IntPtr, double, IntPtr, FlipMode)"/>
    /// <seealso cref="RenderTextureTiled(IntPtr, IntPtr, IntPtr, float, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel
    /// precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTextureRotated(IntPtr, IntPtr, IntPtr, IntPtr, double, IntPtr, FlipMode)"/>
    /// <seealso cref="RenderTextureTiled(IntPtr, IntPtr, IntPtr, float, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect dstrect);
    #endregion
    
    
    #region RenderTextureRotated
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, IntPtr center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr dstrect, double angle, IntPtr center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect dstrect, double angle, IntPtr center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, in FPoint center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect dstrect, double angle, IntPtr center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect dstrect, double angle, in FPoint center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr dstrect, double angle, in FPoint center, FlipMode flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureRotated(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FRect *dstrect, double angle, const SDL_FPoint *center, SDL_FlipMode flip);</code>
    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with
    /// rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect"> a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="angle">an angle in degrees that indicates the rotation that will be
    /// applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">a pointer to a point indicating the point around which
    /// dstrect will be rotated (if <c>null</c>, rotation will be done
    /// around dstrect.w/2, dstrect.h/2).</param>
    /// <param name="flip">an <see cref="FlipMode"/> value stating which flipping actions should be
    /// performed on the texture.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureRotated"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureRotated(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect dstrect, double angle, in FPoint center, FlipMode flip);
    #endregion
    
    
    #region RenderTextureAffine
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, IntPtr right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, IntPtr right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, in FRect right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr origin, in FRect right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect origin, IntPtr right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect origin, IntPtr right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect origin, in FRect right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, IntPtr srcrect, in FRect origin, in FRect right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr origin, IntPtr right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr origin, IntPtr right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr origin, in FRect right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, IntPtr origin, in FRect right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect origin, IntPtr right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect origin, IntPtr right, in FRect down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect origin, in FRect right, IntPtr down);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureAffine(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, const SDL_FPoint *origin, const SDL_FPoint *right, const SDL_FPoint *down);</code>
    /// <summary>
    /// <para>Copy a portion of the source texture to the current rendering target, with
    /// affine transform, at subpixel precision.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="origin">a pointer to a point indicating where the top-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's origin.</param>
    /// <param name="right">a pointer to a point indicating where the top-right corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering
    /// target's top-right corner.</param>
    /// <param name="down">a pointer to a point indicating where the bottom-left corner of
    /// srcrect should be mapped to, or <c>null</c> for the rendering target's
    /// bottom-left corner.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You may only call this function from the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureAffine"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureAffine(IntPtr renderer, IntPtr texture, in FRect srcrect, in FRect origin, in FRect right, in FRect down);
    
    #endregion
    
    
    #region RenderTextureTiled
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float scale, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// <para>Tile a portion of the texture to the current rendering target at subpixel
    /// precision.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureTiled(IntPtr renderer, IntPtr texture, IntPtr srcrect, float scale, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float scale, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// <para>Tile a portion of the texture to the current rendering target at subpixel
    /// precision.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureTiled(IntPtr renderer, IntPtr texture, in FRect srcrect, float scale, IntPtr dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float scale, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// <para>Tile a portion of the texture to the current rendering target at subpixel
    /// precision.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureTiled(IntPtr renderer, IntPtr texture, IntPtr srcrect, float scale, in FRect dstrect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTextureTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float scale, const SDL_FRect *dstrect);</code>
    /// <summary>
    /// <para>Tile a portion of the texture to the current rendering target at subpixel
    /// precision.</para>
    /// <para>The pixels in <c>srcrect</c> will be repeated as many times as needed to
    /// completely fill <c>dstrect</c>.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">a pointer to the source rectangle, or <c>null</c> for the entire
    /// texture.</param>
    /// <param name="scale">the scale used to transform srcrect into the destination
    /// rectangle, e.g. a 32x32 texture with a scale of 2 would fill
    /// 64x64 tiles.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTextureTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTextureTiled(IntPtr renderer, IntPtr texture, in FRect srcrect, float scale, in FRect dstrect);
    #endregion
    
    
    #region RenderTexture9Grid
    //extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9Grid(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect);
    /// <summary>
    /// <para>erform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using `scale` and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="sacel">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9Grid(IntPtr renderer, IntPtr texture, in FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float sacel, IntPtr dstrect);
    
    
    //extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9Grid(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect);
    /// <summary>
    /// <para>erform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using `scale` and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="sacel">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9Grid(IntPtr renderer, IntPtr texture, IntPtr srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float sacel, in FRect dstrect);
    
    
    //extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9Grid(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect);
    /// <summary>
    /// <para>erform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using `scale` and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// stretched into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the <see cref="Rect"/> structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="sacel">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or 0.0f for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderTexture(nint, nint, nint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9Grid"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9Grid(IntPtr renderer, IntPtr texture, in FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float sacel, in FRect dstrect);
    #endregion
    
    
    #region RenderTexture9GridTiled
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9GridTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect, float tileScale);</code>
    /// <summary>
    /// <para>Perform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using <c>scale</c> and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// tiled into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the SDL_Rect structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or <c>0.0f</c> for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="tileScale">the scale used to transform the borders and center of
    /// <c>srcrect</c> into the borders and middle of <c>dstrect</c>, or
    /// <c>1.0f</c> for an unscaled copy.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <seealso cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="RenderTexture(IntPtr, IntPtr, IntPtr, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9GridTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9GridTiled(IntPtr renderer, IntPtr texture, IntPtr srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, IntPtr dstrect, float tileScale);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9GridTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect, float tileScale);</code>
    /// <summary>
    /// <para>Perform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using <c>scale</c> and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// tiled into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the SDL_Rect structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or <c>0.0f</c> for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="tileScale">the scale used to transform the borders and center of
    /// <c>srcrect</c> into the borders and middle of <c>dstrect</c>, or
    /// <c>1.0f</c> for an unscaled copy.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <seealso cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="RenderTexture(IntPtr, IntPtr, IntPtr, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9GridTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9GridTiled(IntPtr renderer, IntPtr texture, in FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, IntPtr dstrect, float tileScale);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9GridTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect, float tileScale);</code>
    /// <summary>
    /// <para>Perform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using <c>scale</c> and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// tiled into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the SDL_Rect structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or <c>0.0f</c> for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="tileScale">the scale used to transform the borders and center of
    /// <c>srcrect</c> into the borders and middle of <c>dstrect</c>, or
    /// <c>1.0f</c> for an unscaled copy.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <seealso cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="RenderTexture(IntPtr, IntPtr, IntPtr, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9GridTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9GridTiled(IntPtr renderer, IntPtr texture, IntPtr srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, in FRect dstrect, float tileScale);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderTexture9GridTiled(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_FRect *srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, const SDL_FRect *dstrect, float tileScale);</code>
    /// <summary>
    /// <para>Perform a scaled copy using the 9-grid algorithm to the current rendering
    /// target at subpixel precision.</para>
    /// <para>The pixels in the texture are split into a 3x3 grid, using the different
    /// corner sizes for each corner, and the sides and center making up the
    /// remaining pixels. The corners are then scaled using <c>scale</c> and fit into
    /// the corners of the destination rectangle. The sides and center are then
    /// tiled into place to cover the remaining destination rectangle.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should copy parts of a texture.</param>
    /// <param name="texture">the source texture.</param>
    /// <param name="srcrect">the SDL_Rect structure representing the rectangle to be used
    /// for the 9-grid, or <c>null</c> to use the entire texture.</param>
    /// <param name="leftWidth">the width, in pixels, of the left corners in <c>srcrect</c>.</param>
    /// <param name="rightWidth">the width, in pixels, of the right corners in <c>srcrect</c>.</param>
    /// <param name="topHeight">the height, in pixels, of the top corners in <c>srcrect</c>.</param>
    /// <param name="bottomHeight">the height, in pixels, of the bottom corners in
    /// <c>srcrect</c>.</param>
    /// <param name="scale">the scale used to transform the corner of <c>srcrect</c> into the
    /// corner of <c>dstrect</c>, or <c>0.0f</c> for an unscaled copy.</param>
    /// <param name="dstrect">a pointer to the destination rectangle, or <c>null</c> for the
    /// entire rendering target.</param>
    /// <param name="tileScale">the scale used to transform the borders and center of
    /// <c>srcrect</c> into the borders and middle of <c>dstrect</c>, or
    /// <c>1.0f</c> for an unscaled copy.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <seealso cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="RenderTexture(IntPtr, IntPtr, IntPtr, IntPtr)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderTexture9GridTiled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderTexture9GridTiled(IntPtr renderer, IntPtr texture, in FRect srcrect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, in FRect dstrect, float tileScale);
    
    #endregion
    
    
    #region RenderGeometry
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderGeometry(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_Vertex *vertices, int num_vertices, const int *indices, int num_indices);</code>
    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the
    /// vertex array Color and alpha modulation is done per vertex
    /// (<see cref="SetTextureColorMod"/> and <see cref="SetTextureAlphaMod"/> are ignored).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="vertices">vertices.</param>
    /// <param name="numVertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of integer indices into the <c>vertices</c>
    /// array, if <c>null</c> all vertices will be rendered in sequential
    /// order.</param>
    /// <param name="numIndices">number of indices.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderGeometryRaw(IntPtr, IntPtr, float[], int, FColor[], int, float[], int, int, IntPtr, int, int)"/>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderGeometry"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderGeometry(IntPtr renderer, IntPtr texture, Vertex[] vertices, int numVertices, IntPtr indices, int numIndices);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderGeometry(SDL_Renderer *renderer, SDL_Texture *texture, const SDL_Vertex *vertices, int num_vertices, const int *indices, int num_indices);</code>
    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the
    /// vertex array Color and alpha modulation is done per vertex
    /// (<see cref="SetTextureColorMod"/> and <see cref="SetTextureAlphaMod"/> are ignored).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="vertices">vertices.</param>
    /// <param name="numVertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of integer indices into the <c>vertices</c>
    /// array, if <c>null</c> all vertices will be rendered in sequential
    /// order.</param>
    /// <param name="numIndices">number of indices.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderGeometryRaw(IntPtr, IntPtr, float[], int, FColor[], int, float[], int, int, IntPtr, int, int)"/>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderGeometry"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderGeometry(IntPtr renderer, IntPtr texture, Vertex[] vertices, int numVertices, int[] indices, int numIndices);
    
    #endregion
    
    
    #region RenderGeometryRaw
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderGeometryRaw(SDL_Renderer *renderer, SDL_Texture *texture, const float *xy, int xy_stride, const SDL_FColor *color, int color_stride, const float *uv, int uv_stride, int num_vertices, const void *indices, int num_indices, int size_indices);</code>
    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the
    /// vertex arrays Color and alpha modulation is done per vertex
    /// (<see cref="SetTextureColorMod"/> and <see cref="SetTextureAlphaMod"/> are ignored).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="xy">vertex positions.</param>
    /// <param name="xyStride">byte size to move from one element to the next element.</param>
    /// <param name="color">vertex colors (as <see cref="FColor"/>).</param>
    /// <param name="colorStride">byte size to move from one element to the next element.</param>
    /// <param name="uv">vertex normalized texture coordinates.</param>
    /// <param name="uvStride">byte size to move from one element to the next element.</param>
    /// <param name="numVertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of indices into the <c>vertices</c> arrays,
    /// if <c>null</c> all vertices will be rendered in sequential order.</param>
    /// <param name="numIndices">number of indices.</param>
    /// <param name="sizeIndices">index size: 1 (byte), 2 (short), 4 (int).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderGeometryRaw"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderGeometryRaw(IntPtr renderer, IntPtr texture, IntPtr xy, int xyStride, IntPtr color, 
        int colorStride, IntPtr uv, int uvStride, int numVertices, IntPtr indices, int numIndices, int sizeIndices);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderGeometryRaw(SDL_Renderer *renderer, SDL_Texture *texture, const float *xy, int xy_stride, const SDL_FColor *color, int color_stride, const float *uv, int uv_stride, int num_vertices, const void *indices, int num_indices, int size_indices);</code>
    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the
    /// vertex arrays Color and alpha modulation is done per vertex
    /// (<see cref="SetTextureColorMod"/> and <see cref="SetTextureAlphaMod"/> are ignored).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="xy">vertex positions.</param>
    /// <param name="xyStride">byte size to move from one element to the next element.</param>
    /// <param name="color">vertex colors (as <see cref="FColor"/>).</param>
    /// <param name="colorStride">byte size to move from one element to the next element.</param>
    /// <param name="uv">vertex normalized texture coordinates.</param>
    /// <param name="uvStride">byte size to move from one element to the next element.</param>
    /// <param name="numVertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of indices into the <c>vertices</c> arrays,
    /// if <c>null</c> all vertices will be rendered in sequential order.</param>
    /// <param name="numIndices">number of indices.</param>
    /// <param name="sizeIndices">index size: 1 (byte), 2 (short), 4 (int).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderGeometryRaw"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderGeometryRaw(IntPtr renderer, IntPtr texture, float[] xy, int xyStride, FColor[] color, int colorStride, float[] uv, int uvStride, int numVertices, IntPtr indices, int numIndices, int sizeIndices);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderGeometryRaw(SDL_Renderer *renderer, SDL_Texture *texture, const float *xy, int xy_stride, const SDL_FColor *color, int color_stride, const float *uv, int uv_stride, int num_vertices, const void *indices, int num_indices, int size_indices);</code>
    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the
    /// vertex arrays Color and alpha modulation is done per vertex
    /// (<see cref="SetTextureColorMod"/> and <see cref="SetTextureAlphaMod"/> are ignored).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="xy">vertex positions.</param>
    /// <param name="xyStride">byte size to move from one element to the next element.</param>
    /// <param name="color">vertex colors (as <see cref="FColor"/>).</param>
    /// <param name="colorStride">byte size to move from one element to the next element.</param>
    /// <param name="uv">vertex normalized texture coordinates.</param>
    /// <param name="uvStride">byte size to move from one element to the next element.</param>
    /// <param name="numVertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of indices into the <c>vertices</c> arrays,
    /// if <c>null</c> all vertices will be rendered in sequential order.</param>
    /// <param name="numIndices">number of indices.</param>
    /// <param name="sizeIndices">index size: 1 (byte), 2 (short), 4 (int).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderGeometryRaw"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderGeometryRaw(IntPtr renderer, IntPtr texture, float[] xy, int xyStride, FColor[] color, int colorStride, float[] uv, int uvStride, int numVertices, byte[] indices, int numIndices, int sizeIndices);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderGeometryRaw(SDL_Renderer *renderer, SDL_Texture *texture, const float *xy, int xy_stride, const SDL_FColor *color, int color_stride, const float *uv, int uv_stride, int num_vertices, const void *indices, int num_indices, int size_indices);</code>
    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the
    /// vertex arrays Color and alpha modulation is done per vertex
    /// (<see cref="SetTextureColorMod"/> and <see cref="SetTextureAlphaMod"/> are ignored).
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="texture">(optional) The SDL texture to use.</param>
    /// <param name="xy">vertex positions.</param>
    /// <param name="xyStride">byte size to move from one element to the next element.</param>
    /// <param name="color">vertex colors (as <see cref="FColor"/>).</param>
    /// <param name="colorStride">byte size to move from one element to the next element.</param>
    /// <param name="uv">vertex normalized texture coordinates.</param>
    /// <param name="uvStride">byte size to move from one element to the next element.</param>
    /// <param name="numVertices">number of vertices.</param>
    /// <param name="indices">(optional) An array of indices into the <c>vertices</c> arrays,
    /// if <c>null</c> all vertices will be rendered in sequential order.</param>
    /// <param name="numIndices">number of indices.</param>
    /// <param name="sizeIndices">index size: 1 (byte), 2 (short), 4 (int).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    public static unsafe bool RenderGeometryRaw<TIndex>(IntPtr renderer, IntPtr texture, Span<float> xy, 
        int xyStride, Span<FColor> color, int colorStride, Span<float> uv, int uvStride,
        int numVertices, Span<TIndex> indices, int numIndices, int sizeIndices) where TIndex : unmanaged
    {
        fixed (float* pXy = xy)
        fixed (FColor* pColor = color)
        fixed (float* pUV = uv)
        {
            if (indices.Length == 0)
            {
                return RenderGeometryRaw(renderer, texture, (IntPtr)pXy, xyStride,
                    (IntPtr)pColor, colorStride, (IntPtr)pUV, uvStride,
                    numVertices, IntPtr.Zero, 0, 0);
            }

            fixed (TIndex* pIndices = indices)
            {
                return RenderGeometryRaw(renderer, texture, (IntPtr)pXy, xyStride,
                    (IntPtr)pColor, colorStride, (IntPtr)pUV, uvStride,
                    numVertices, (IntPtr)pIndices, numIndices, sizeIndices);
            }
        }
    }
    #endregion
    
    
    #region SetRenderTextureAddressMode
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderTextureAddressMode(SDL_Renderer *renderer, SDL_TextureAddressMode u_mode, SDL_TextureAddressMode v_mode);</code>
    /// <summary>
    /// <para>Set the texture addressing mode used in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="umode">the <see cref="TextureAddressMode"/> to use for horizontal texture
    /// coordinates in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>.</param>
    /// <param name="vmode">the <see cref="TextureAddressMode"/> to use for vertical texture
    /// coordinates in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/></param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>
    /// <seealso cref="RenderGeometryRaw(IntPtr, IntPtr, float[], int, FColor[], int, float[], int, int, IntPtr, int, int)"/>
    /// <seealso cref="GetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderTextureAddressMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderTextureAddressMode(IntPtr renderer, TextureAddressMode umode, TextureAddressMode vmode);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderTextureAddressMode(SDL_Renderer *renderer, SDL_TextureAddressMode *u_mode, SDL_TextureAddressMode *v_mode);</code>
    /// <summary>
    /// Get the texture addressing mode used in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="umode">a pointer filled in with the <see cref="TextureAddressMode"/> to use
    /// for horizontal texture coordinates in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>,
    /// may be <c>null</c>.</param>
    /// <param name="vmode">a pointer filled in with the <see cref="TextureAddressMode"/> to use
    /// for vertical texture coordinates in <see cref="RenderGeometry(IntPtr, IntPtr, Vertex[], int, IntPtr, int)"/>, may
    /// be <c>null</c>.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="SetRenderTextureAddressMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderTextureAddressMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderTextureAddressMode(IntPtr renderer, out TextureAddressMode umode, out TextureAddressMode vmode);
    
    #endregion
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderReadPixels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_RenderReadPixels(IntPtr renderer, IntPtr rect);
    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_RenderReadPixels(SDL_Renderer *renderer, const SDL_Rect *rect);</code>
    /// <summary>
    /// <para>Read pixels from the current rendering target.</para>
    /// <para>The returned surface contains pixels inside the desired area clipped to the
    /// current viewport, and should be freed with <see cref="DestroySurface"/>.</para>
    /// <para>Note that this returns the actual pixels on the screen, so if you are using
    /// logical presentation you should use <see cref="GetRenderLogicalPresentationRect"/>
    /// to get the area containing your content.</para>
    /// <para><b>WARNING</b>: This is a very slow operation, and should not be used
    /// frequently. If you're using this on the main rendering target, it should be
    /// called after rendering and before <see cref="RenderPresent"/>.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="rect">an <see cref="Rect"/> structure representing the area to read, which will
    /// be clipped to the current viewport, or <c>null</c> for the entire
    /// viewport.</param>
    /// <returns>a new <see cref="Surface"/> on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr RenderReadPixels(IntPtr renderer, Rect? rect)
    {
        var rectPtr = IntPtr.Zero;
        
        try
        {
            rectPtr = StructureToPointer(rect);
            return SDL_RenderReadPixels(renderer, rectPtr);
        }
        finally
        {
            if(rectPtr != IntPtr.Zero) 
                Marshal.FreeHGlobal(rectPtr);
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderPresent(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Update the screen with any rendering performed since the previous call.</para>
    /// <para>SDL's rendering functions operate on a backbuffer; that is, calling a
    /// rendering function such as <see cref="RenderLine"/> does not directly put a line on
    /// the screen, but rather updates the backbuffer. As such, you compose your
    /// entire scene and <b>present</b> the composed backbuffer to the screen as a
    /// complete picture.</para>
    /// <para>Therefore, when using SDL's rendering API, one does all drawing intended
    /// for the frame, and then calls this function once per frame to present the
    /// final drawing to the user.</para>
    /// <para>The backbuffer should be considered invalidated after each present; do not
    /// assume that previous contents will exist between frames. You are strongly
    /// encouraged to call <see cref="RenderClear"/> to initialize the backbuffer before
    /// starting each new frame's drawing, even if you plan to overwrite every
    /// pixel.</para>
    /// <para>Please note, that in case of rendering to a texture - there is **no need**
    /// to call <see cref="RenderPresent"/> after drawing needed objects to a texture, and
    /// should not be done; you are only required to change back the rendering
    /// target to default via <c>SetRenderTarget(renderer, null)</c> afterwards, as
    /// textures by themselves do not have a concept of backbuffers. Calling
    /// <see cref="RenderPresent"/> while rendering to a texture will still update the screen
    /// with any current drawing that has been done _to the window itself_.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRenderer"/>
    /// <seealso cref="RenderClear"/>
    /// <seealso cref="RenderFillRect(IntPtr, IntPtr)"/>
    /// <seealso cref="RenderFillRects"/>
    /// <seealso cref="RenderLine"/>
    /// <seealso cref="RenderLines"/>
    /// <seealso cref="RenderPoint"/>
    /// <seealso cref="RenderPoints"/>
    /// <seealso cref="RenderRect(IntPtr, IntPtr)"/>
    /// <seealso cref="RenderRects"/>
    /// <seealso cref="SetRenderDrawBlendMode"/>
    /// <seealso cref="SetRenderDrawColor(IntPtr, byte, byte, byte, byte)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderPresent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderPresent(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyTexture(SDL_Texture *texture);</code>
    /// <summary>
    /// <para>Destroy the specified texture.</para>
    /// <para>Passing <c>null</c> or an otherwise invalid texture will set the SDL error message
    /// to "Invalid texture".</para>
    /// </summary>
    /// <param name="texture">the texture to destroy.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateTexture"/>
    /// <seealso cref="CreateTextureFromSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyTexture"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyTexture(IntPtr texture);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyRenderer(SDL_Renderer *renderer);</code>
    /// <summary>
    /// Destroy the rendering context for a window and free all associated
    /// textures.
    /// </summary>
    /// <para>This should be called before destroying the associated window.</para>
    /// <param name="renderer">the rendering context.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRenderer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyRenderer(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FlushRenderer(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Force the rendering context to flush any pending commands and state.</para>
    /// <para>You do not need to (and in fact, shouldn't) call this function unless you
    /// are planning to call into OpenGL/Direct3D/Metal/whatever directly, in
    /// addition to using an SDL_Renderer.</para>
    /// <para>This is for a very-specific case: if you are using SDL's render API, and
    /// you plan to make OpenGL/D3D/whatever calls in addition to SDL render API
    /// calls. If this applies, you should call this function between calls to
    /// SDL's render API and the low-level API you're using in cooperation.</para>
    /// <para>In all other cases, you can ignore this function.</para>
    /// <para>This call makes SDL flush any pending rendering work it was queueing up to
    /// do later in a single batch, and marks any internal cached state as invalid,
    /// so it'll prepare all its state again later, from scratch.</para>
    /// <para>This means you do not need to save state in your rendering code to protect
    /// the SDL renderer. However, there lots of arbitrary pieces of Direct3D and
    /// OpenGL state that can confuse things; you should use your best judgment and
    /// be prepared to make changes if specific state needs to be protected.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlushRenderer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FlushRenderer(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetRenderMetalLayer(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Get the CAMetalLayer associated with the given Metal renderer.</para>
    /// <para>This function returns <c>void *</c>, so SDL doesn't have to include Metal's
    /// headers, but it can be safely cast to a <c>CAMetalLayer *</c>.</para>
    /// </summary>
    /// <param name="renderer">the renderer to query.</param>
    /// <returns>a <c>CAMetalLayer *</c> on success, or <c>null</c> if the renderer isn't a
    /// Metal renderer.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderMetalCommandEncoder"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderMetalLayer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRenderMetalLayer(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetRenderMetalCommandEncoder(SDL_Renderer *renderer);</code>
    /// <summary>
    /// <para>Get the Metal command encoder for the current frame.</para>
    /// <para>This function returns <c>void *</c>, so SDL doesn't have to include Metal's
    /// headers, but it can be safely cast to an <c>id&lt;MTLRenderCommandEncoder&gt;</c>.</para>
    /// <para>This will return <c>null</c> if Metal refuses to give SDL a drawable to render to,
    /// which might happen if the window is hidden/minimized/offscreen. This
    /// doesn't apply to command encoders for render targets, just the window's
    /// backbuffer. Check your return values!</para>
    /// </summary>
    /// <param name="renderer">the renderer to query.</param>
    /// <returns>an <c>id&lt;MTLRenderCommandEncoder&gt;</c> on success, or <c>null</c> if the
    /// renderer isn't a Metal renderer or there was an error.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderMetalLayer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderMetalCommandEncoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetRenderMetalCommandEncoder(IntPtr renderer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AddVulkanRenderSemaphores(SDL_Renderer *renderer, Uint32 wait_stage_mask, Sint64 wait_semaphore, Sint64 signal_semaphore);</code>
    /// <summary>
    /// <para>Add a set of synchronization semaphores for the current frame.</para>
    /// <para>The Vulkan renderer will wait for <c>waitSemaphore</c> before submitting
    /// rendering commands and signal <c>signalSemaphore</c> after rendering commands
    /// are complete for this frame.</para>
    /// <para>This should be called each frame that you want semaphore synchronization.
    /// The Vulkan renderer may have multiple frames in flight on the GPU, so you
    /// should have multiple semaphores that are used for synchronization. Querying
    /// <see cref="Props.RendererVulkanSwapchainImageCountNumber"/> will give you the
    /// maximum number of semaphores you'll need.</para>
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="waitStageMasl">the VkPipelineStageFlags for the wait.</param>
    /// <param name="waitSemaphore">a VkSempahore to wait on before rendering the current
    /// frame, or 0 if not needed.</param>
    /// <param name="signalSemaphore">a VkSempahore that SDL will signal when rendering
    /// for the current frame is complete, or 0 if not
    /// needed.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is <b>NOT</b> safe to call this function from two threads at
    /// once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddVulkanRenderSemaphores"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool AddVulkanRenderSemaphores(IntPtr renderer, uint waitStageMasl, long waitSemaphore, long signalSemaphore);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderVSync(SDL_Renderer *renderer, int vsync);</code>
    /// <summary>
    /// <para>Toggle VSync of the given renderer.</para>
    /// <para>When a renderer is created, vsync defaults to <see cref="RendererVSyncDisabled"/>.</para>
    /// <para>The `vsync` parameter can be 1 to synchronize present with every vertical
    /// refresh, 2 to synchronize present with every second vertical refresh, etc.,
    /// <see cref="RendererVSyncAdaptive"/> for late swap tearing (adaptive vsync), or
    /// <see cref="RendererVSyncDisabled"/> to disable. Not every value is supported by
    /// every driver, so you should check the return value to see whether the
    /// requested setting is supported.</para>
    /// </summary>
    /// <param name="renderer">the renderer to toggle.</param>
    /// <param name="vsync">the vertical refresh sync interval.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRenderVSync"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetRenderVSync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetRenderVSync(IntPtr renderer, int vsync);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRenderVSync(SDL_Renderer *renderer, int *vsync);</code>
    /// <summary>
    /// Get VSync of the given renderer.
    /// </summary>
    /// <param name="renderer">the renderer to toggle.</param>
    /// <param name="vsync">an int filled with the current vertical refresh sync interval.
    /// See <see cref="SetRenderVSync"/> for the meaning of the value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetRenderVSync"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRenderVSync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRenderVSync(IntPtr renderer, out int vsync);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderDebugText(SDL_Renderer *renderer, float x, float y, const char *str);</code>
    /// <summary>
    /// <para>Draw debug text to an SDL_Renderer.</para>
    /// <para>This function will render a string of text to an SDL_Renderer. Note that
    /// this is a convenience function for debugging, with severe limitations, and
    /// not intended to be used for production apps and games.</para>
    /// <para>Among these limitations:</para>
    /// <list type="bullet">
    /// <item>It accepts UTF-8 strings, but will only renders ASCII characters.</item>
    /// <item>It has a single, tiny size (8x8 pixels). You can use logical presentation
    /// or <see cref="SetRenderScale"/> to adjust it.</item>
    /// <item>It uses a simple, hardcoded bitmap font. It does not allow different font
    /// selections and it does not support truetype, for proper scaling.</item>
    /// <item>It does no word-wrapping and does not treat newline characters as a line
    /// break. If the text goes out of the window, it's gone.</item>
    /// </list>
    /// <para>For serious text rendering, there are several good options, such as
    /// SDL_ttf, stb_truetype, or other external libraries.</para>
    /// <para>On first use, this will create an internal texture for rendering glyphs.
    /// This texture will live until the renderer is destroyed.</para>
    /// <para>The text is drawn in the color specified by <see cref="SetRenderDrawColor"/>.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should draw a line of text.</param>
    /// <param name="x">the x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">the y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="str">the string to render.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.6.</since>
    /// <seealso cref="RenderDebugTextFormat"/>
    /// <seealso cref="DebugTextFontCharacterSize"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderDebugText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderDebugText(IntPtr renderer, float x, float y, [MarshalAs(UnmanagedType.LPUTF8Str)] string str);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RenderDebugTextFormat(SDL_Renderer *renderer, float x, float y, SDL_PRINTF_FORMAT_STRING const char *fmt, ...) SDL_PRINTF_VARARG_FUNC(4);</code>
    /// <summary>
    /// <para>Draw debug text to an SDL_Renderer.</para>
    /// <para>This function will render a printf()-style format string to a renderer.
    /// Note that this is a convenience function for debugging, with severe
    /// limitations, and is not intended to be used for production apps and games.</para>
    /// <para>For the full list of limitations and other useful information, see
    /// <see cref="RenderDebugText"/>.</para>
    /// </summary>
    /// <param name="renderer">the renderer which should draw the text.</param>
    /// <param name="x">the x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">the y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="fmt">the format string to draw.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RenderDebugText"/>
    /// <seealso cref="DebugTextFontCharacterSize"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RenderDebugTextFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RenderDebugTextFormat(IntPtr renderer, float x, float y, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt); 
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetDefaultTextureScaleMode(SDL_Renderer *renderer, SDL_ScaleMode scale_mode);</code>
    /// <summary>
    /// <para>Set default scale mode for new textures for given renderer.</para>
    /// <para>When a renderer is created, <c>scaleMode</c> defaults to <see cref="ScaleMode.Linear"/>.</para>
    /// </summary>
    /// <param name="renderer">the renderer to update.</param>
    /// <param name="scaleMode">the scale mode to change to for new textures.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="GetDefaultTextureScaleMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetDefaultTextureScaleMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetDefaultTextureScaleMode(IntPtr renderer, ScaleMode scaleMode); 
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetDefaultTextureScaleMode(SDL_Renderer *renderer, SDL_ScaleMode *scale_mode);</code>
    /// <summary>
    /// Get default texture scale mode of the given renderer.
    /// </summary>
    /// <param name="renderer">the renderer to get data from.</param>
    /// <param name="scaleMode">a <see cref="ScaleMode"/> filled with current default scale mode.
    /// See <see cref="SetDefaultTextureScaleMode"/> for the meaning of
    /// the value.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="SetDefaultTextureScaleMode"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetDefaultTextureScaleMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetDefaultTextureScaleMode(IntPtr renderer, out ScaleMode scaleMode); 
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPURenderState * SDLCALL SDL_CreateGPURenderState(SDL_Renderer *renderer, SDL_GPURenderStateDesc *desc);</code>
    /// <summary>
    /// <para>Create custom GPU render state.</para>
    /// </summary>
    /// <param name="renderer">the renderer to use.</param>
    /// <param name="createinfo">a struct describing the GPU render state to create.</param>
    /// <returns>a custom GPU render state or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// renderer.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="SetGPURenderStateFragmentUniforms"/>
    /// <seealso cref="SetGPURenderState"/>
    /// <seealso cref="DestroyGPURenderState"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPURenderState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGPURenderState(IntPtr renderer, IntPtr createinfo); 
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetGPURenderStateFragmentUniforms(SDL_GPURenderState *state, Uint32 slot_index, const void *data, Uint32 length);</code>
    /// <summary>
    /// <para>Set fragment shader uniform variables in a custom GPU render state.</para>
    /// <para>The data is copied and will be pushed using
    /// <see cref="PushGPUFragmentUniformData(nint, uint, byte[], uint)"/> during draw call execution.</para>
    /// </summary>
    /// <param name="state">the state to modify.</param>
    /// <param name="slotIndex">the fragment uniform slot to push data to.</param>
    /// <param name="data">client data to write.</param>
    /// <param name="length">the length of the data to write.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// renderer.</threadsafety>
    /// <since>This function is available since SDL 3.4.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPURenderStateFragmentUniforms"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetGPURenderStateFragmentUniforms(IntPtr state, uint slotIndex, IntPtr data, uint length); 
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetRenderGPUState(SDL_Renderer *renderer, SDL_GPURenderState *state);</code>
    /// <summary>
    /// <para>Set custom GPU render state.</para>
    /// <para>This function sets custom GPU render state for subsequent draw calls. This
    /// allows using custom shaders with the GPU renderer.</para>
    /// </summary>
    /// <param name="renderer">the renderer to use.</param>
    /// <param name="state">the state to to use, or <c>null</c> to clear custom GPU render state.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// renderer.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetGPURenderState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetGPURenderState(IntPtr renderer, IntPtr state);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyGPURenderState(SDL_GPURenderState *state);</code>
    /// <summary>
    /// <para>Destroy custom GPU render state.</para>
    /// </summary>
    /// <param name="state">state the state to destroy.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// renderer.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyGPURenderState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyGPURenderState(IntPtr state); 
}
