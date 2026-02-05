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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public partial class SDL
{
    /// <code>extern SDL_DECLSPEC XrResult SDLCALL SDL_CreateGPUXRSession(SDL_GPUDevice *device, const XrSessionCreateInfo *createinfo, XrSession *session);</code>
    /// <summary>
    /// Creates an OpenXR session.
    /// <para>The OpenXR system ID is pulled from the passed GPU context.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="createInfo">the create info for the OpenXR session, sans the system
    /// ID.</param>
    /// <param name="session">a pointer filled in with an OpenXR session created for the
    /// given device.</param>
    /// <returns>the result of the call.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUXRSession"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial XrResult CreateGPUXRSession(IntPtr device, IntPtr createInfo, out IntPtr session);
    
    
    /// <code>extern SDL_DECLSPEC SDL_GPUTextureFormat * SDLCALL SDL_GetGPUXRSwapchainFormats(SDL_GPUDevice *device, XrSession session, int *num_formats);</code>
    /// <summary>
    /// Queries the GPU device for supported XR swapchain image formats.
    /// <para>The returned pointer should be allocated with SDL_malloc() and will be
    /// passed to <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="session">an OpenXR session created for the given device.</param>
    /// <param name="numFormats">a pointer filled with the number of supported XR
    /// swapchain formats.</param>
    /// <returns>a 0 terminated array of supported formats or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. This should be freed with
    /// <see cref="Free"/> when it is no longer needed.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="CreateGPUXRSwapchain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetGPUXRSwapchainFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetGPUXRSwapchainFormats(IntPtr device, IntPtr session, out int numFormats);
    
    
    /// <code>extern SDL_DECLSPEC XrResult SDLCALL SDL_CreateGPUXRSwapchain(SDL_GPUDevice *device, XrSession session, const XrSwapchainCreateInfo *createinfo, SDL_GPUTextureFormat format, XrSwapchain *swapchain, SDL_GPUTexture ***textures);</code>
    /// <summary>
    /// Creates an OpenXR swapchain.
    /// <para>The array returned via <c>textures</c> is sized according to
    /// <c>xrEnumerateSwapchainImages</c>, and thus should only be accessed via index
    /// values returned from <c>xrAcquireSwapchainImage</c>.</para>
    /// <para>Applications are still allowed to call <c>xrEnumerateSwapchainImages</c> on the
    /// returned XrSwapchain if they need to get the exact size of the array.</para>
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="session">an OpenXR session created for the given device.</param>
    /// <param name="createinfo">the create info for the OpenXR swapchain, sans the
    /// format.</param>
    /// <param name="format">a supported format for the OpenXR swapchain.</param>
    /// <param name="swapchain">a pointer filled in with the created OpenXR swapchain.</param>
    /// <param name="textures">a pointer filled in with the array of created swapchain
    /// images.</param>
    /// <returns>the result of the call.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    /// <seealso cref="CreateGPUXRSession"/>
    /// <seealso cref="GetGPUXRSwapchainFormats"/>
    /// <seealso cref="DestroyGPUXRSwapchain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateGPUXRSwapchain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial XrResult CreateGPUXRSwapchain(IntPtr device, IntPtr session, IntPtr createinfo, GPUTextureFormat format, out IntPtr swapchain, out IntPtr textures);
    
    
    /// <code>extern SDL_DECLSPEC XrResult SDLCALL SDL_DestroyGPUXRSwapchain(SDL_GPUDevice *device, XrSwapchain swapchain, SDL_GPUTexture **swapchainImages);</code>
    /// <summary>
    /// Destroys and OpenXR swapchain previously returned by
    /// <see cref="CreateGPUXRSwapchain"/>.
    /// </summary>
    /// <param name="device">a GPU context.</param>
    /// <param name="swapchain">a swapchain previously returned by
    /// <see cref="CreateGPUXRSwapchain"/>.</param>
    /// <param name="swapchainImages">an array of swapchain images returned by the same
    /// call to <see cref="CreateGPUXRSwapchain"/>.</param>
    /// <returns>the result of the call.</returns>
    /// <since>his function is available since SDL 3.6.0.</since>
    /// <seealso cref="CreateGPUDeviceWithProperties"/>
    /// <seealso cref="CreateGPUXRSession"/>
    /// <seealso cref="CreateGPUXRSwapchain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyGPUXRSwapchain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial XrResult DestroyGPUXRSwapchain(IntPtr device, IntPtr swapchain, IntPtr swapchainImages);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_OpenXR_LoadLibrary(void);</code>
    /// <summary>
    /// Dynamically load the OpenXR loader.
    /// <para>SDL keeps a reference count of the OpenXR loader, calling this function
    /// multiple times will increment that count, rather than loading the library
    /// multiple times.</para>
    /// <para>If not called, this will be implicitly called when creating a GPU device
    /// with OpenXR.</para>
    /// <para>This function will use the platform default OpenXR loader name, unless the
    /// <see cref="Hints.OpenXRLibrary"/> hint is set.</para>
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="Hints.OpenXRLibrary"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenXR_LoadLibrary"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool OpenXRLoadLibrary();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_OpenXR_UnloadLibrary(void);</code>
    /// <summary>
    /// Unload the OpenXR loader previously loaded by <see cref="OpenXRLoadLibrary"/>.
    /// <para>SDL keeps a reference count of the OpenXR loader, calling this function
    /// will decrement that count. Once the reference count reaches zero, the
    /// library is unloaded.</para>
    /// </summary>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.6.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenXR_UnloadLibrary"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void OpenXRUnloadLibrary();
    
    
    //extern SDL_DECLSPEC PFN_xrGetInstanceProcAddr SDLCALL SDL_OpenXR_GetXrGetInstanceProcAddr(void);
    /// <summary>
    /// Get the address of the <c>xrGetInstanceProcAddr</c> function.
    /// <para>This should be called after either calling <see cref="OpenXRLoadLibrary"/> or
    /// creating an OpenXR SDL_GPUDevice.</para>
    /// <para>The actual type of the returned function pointer is
    /// PFN_xrGetInstanceProcAddr, but that isn't always available. You should
    /// include the OpenXR headers before this header, or cast the return value of
    /// this function to the correct type.</para>
    /// </summary>
    /// <returns>the function pointer for <c>xrGetInstanceProcAddr</c> or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.6.0.<;since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenXR_GetXrGetInstanceProcAddr"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenXRGetXrGetInstanceProcAddr();
}
