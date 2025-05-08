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

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_Vulkan_LoadLibrary(const char *path);</code>
    /// <summary>
    /// <para>Dynamically load the Vulkan loader library.</para>
    /// <para>This should be called after initializing the video driver, but before
    /// creating any Vulkan windows. If no Vulkan loader library is loaded, the
    /// default library will be loaded upon creation of the first Vulkan window.</para>
    /// <para>SDL keeps a counter of how many times this function has been successfully
    /// called, so it is safe to call this function multiple times, so long as it
    /// is eventually paired with an equivalent number of calls to
    /// <see cref="VulkanUnloadLibrary"/>. The <c>path</c> argument is ignored unless there is no
    /// library currently loaded, and and the library isn't actually unloaded until
    /// there have been an equivalent number of calls to <see cref="VulkanUnloadLibrary"/>.</para>
    /// <para>It is fairly common for Vulkan applications to link with libvulkan instead
    /// of explicitly loading it at run time. This will work with SDL provided the
    /// application links to a dynamic library and both it and SDL use the same
    /// search path.</para>
    /// <para>If you specify a non-NULL <c>path</c>, an application should retrieve all of the
    /// Vulkan functions it uses from the dynamic library using
    /// <see cref="VulkanGetVkGetInstanceProcAddr"/> unless you can guarantee <c>path</c> points
    /// to the same vulkan loader library the application linked to.</para>
    /// <para>On Apple devices, if <c>path</c> is NULL, SDL will attempt to find the
    /// <c>vkGetInstanceProcAddr</c> address within all the Mach-O images of the current
    /// process. This is because it is fairly common for Vulkan applications to
    /// link with libvulkan (and historically MoltenVK was provided as a static
    /// library). If it is not found, on macOS, SDL will attempt to load
    /// <c>vulkan.framework/vulkan</c>, <c>libvulkan.1.dylib</c>,
    /// <c>MoltenVK.framework/MoltenVK</c>, and <c>libMoltenVK.dylib</c>, in that order. On
    /// iOS, SDL will attempt to load <c>libMoltenVK.dylib</c>. Applications using a
    /// dynamic framework or .dylib must ensure it is included in its application
    /// bundle.</para>
    /// <para>On non-Apple devices, application linking with a static libvulkan is not
    /// supported. Either do not link to the Vulkan loader or link to a dynamic
    /// library version.</para>
    /// </summary>
    /// <param name="path">the platform dependent Vulkan loader library name or <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="VulkanGetVkGetInstanceProcAddr"/>
    /// <seealso cref="VulkanUnloadLibrary"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_LoadLibrary"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool VulkanLoadLibrary([MarshalAs(UnmanagedType.LPUTF8Str)] string? path);
    
    
    /// <code>extern SDL_DECLSPEC SDL_FunctionPointer SDLCALL SDL_Vulkan_GetVkGetInstanceProcAddr(void);</code>
    /// <summary>
    /// <para>Get the address of the <c>vkGetInstanceProcAddr</c> function.</para>
    /// <para>This should be called after either calling <see cref="VulkanLoadLibrary"/> or
    /// creating an SDL_Window with the <see cref="WindowFlags.Vulkan"/> flag.</para>
    /// <para>The actual type of the returned function pointer is
    /// PFN_vkGetInstanceProcAddr, but that isn't available because the Vulkan
    /// headers are not included here. You should cast the return value of this
    /// function to that type, e.g.</para>
    /// <para><c>vkGetInstanceProcAddr =
    /// (PFN_vkGetInstanceProcAddr)VulkanGetVkGetInstanceProcAddr();</c></para>
    /// </summary>
    /// <returns>the function pointer for <c>vkGetInstanceProcAddr</c> or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_GetVkGetInstanceProcAddr"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial FunctionPointer? VulkanGetVkGetInstanceProcAddr();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Vulkan_UnloadLibrary(void);</code>
    /// <summary>
    /// Unload the Vulkan library previously loaded by <see cref="VulkanLoadLibrary"/>.
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="VulkanLoadLibrary"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_UnloadLibrary"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void VulkanUnloadLibrary();
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_GetInstanceExtensions"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_Vulkan_GetInstanceExtensions(out uint count);
    /// <code>extern SDL_DECLSPEC char const * const * SDLCALL SDL_Vulkan_GetInstanceExtensions(Uint32 *count);</code>
    /// <summary>
    /// <para>Get the Vulkan instance extensions needed for vkCreateInstance.</para>
    /// <para>his should be called after either calling <see cref="VulkanLoadLibrary"/> or
    /// creating an SDL_Window with the <see cref="WindowFlags.Vulkan"/> flag.</para>
    /// <para>On return, the variable pointed to by <c>count</c> will be set to the number of
    /// elements returned, suitable for using with
    /// VkInstanceCreateInfo::enabledExtensionCount, and the returned array can be
    /// used with VkInstanceCreateInfo::ppEnabledExtensionNames, for calling
    /// Vulkan's vkCreateInstance API.</para>
    /// <para>You should not free the returned array; it is owned by SDL.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of extensions returned.</param>
    /// <returns>an array of extension name strings on success, <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="VulkanCreateSurface"/>
    public static string[]? VulkanGetInstanceExtensions(out uint count)
    {
        var ptr = SDL_Vulkan_GetInstanceExtensions(out count);
        return ptr == IntPtr.Zero ? null : PointerToStringArray(ptr, (int)count);
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_Vulkan_CreateSurface(SDL_Window *window, VkInstance instance, const struct VkAllocationCallbacks *allocator, VkSurfaceKHR* surface);</code>
    /// <summary>
    /// <para>Create a Vulkan rendering surface for a window.</para>
    /// <para>The <c>window</c> must have been created with the <see cref="WindowFlags.Vulkan"/> flag and
    /// <c>instance</c> must have been created with extensions returned by
    /// <see cref="VulkanGetInstanceExtensions"/> enabled.</para>
    /// <para>If <c>allocator</c> is <c>null</c>, Vulkan will use the system default allocator. This
    /// argument is passed directly to Vulkan and isn't used by SDL itself.</para>
    /// </summary>
    /// <param name="window">the window to which to attach the Vulkan surface.</param>
    /// <param name="instance">the Vulkan instance handle.</param>
    /// <param name="allocator">a VkAllocationCallbacks struct, which lets the app set the
    /// allocator that creates the surface. Can be <c>null</c>.</param>
    /// <param name="surface">a pointer to a VkSurfaceKHR handle to output the newly
    /// created surface.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="VulkanGetInstanceExtensions"/>
    /// <seealso cref="VulkanDestroySurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_CreateSurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool VulkanCreateSurface(IntPtr window, IntPtr instance, IntPtr allocator, out IntPtr surface);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Vulkan_DestroySurface(VkInstance instance, VkSurfaceKHR surface, const struct VkAllocationCallbacks *allocator);</code>
    /// <summary>
    /// <para>Destroy the Vulkan rendering surface of a window.</para>
    /// <para>This should be called before <see cref="DestroyWindow"/>, if <see cref="VulkanCreateSurface"/>
    /// was called after <see cref="CreateWindow"/>.</para>
    /// <para>The <c>instance</c> must have been created with extensions returned by
    /// <see cref="VulkanGetInstanceExtensions"/> enabled and <c>surface</c> must have been
    /// created successfully by an <see cref="VulkanCreateSurface"/> call.</para>
    /// <para>If <c>allocator</c> is <c>null</c>, Vulkan will use the system default allocator. This
    /// argument is passed directly to Vulkan and isn't used by SDL itself.</para>
    /// </summary>
    /// <param name="instance">the Vulkan instance handle.</param>
    /// <param name="surface">vkSurfaceKHR handle to destroy.</param>
    /// <param name="allocator">a VkAllocationCallbacks struct, which lets the app set the
    /// allocator that destroys the surface. Can be <c>null</c>.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="VulkanGetInstanceExtensions"/>
    /// <seealso cref="VulkanCreateSurface"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_DestroySurface"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void VulkanDestroySurface(IntPtr instance, IntPtr surface, IntPtr allocator);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_Vulkan_GetPresentationSupport(VkInstance instance, VkPhysicalDevice physicalDevice, Uint32 queueFamilyIndex);</code>
    /// <summary>
    /// <para>Query support for presentation via a given physical device and queue
    /// family.</para>
    /// <para>The <c>instance</c> must have been created with extensions returned by
    /// <see cref="VulkanGetInstanceExtensions"/> enabled.</para>
    /// </summary>
    /// <param name="instance">the Vulkan instance handle.</param>
    /// <param name="physicalDevice">a valid Vulkan physical device handle.</param>
    /// <param name="queueFamilyIndex">a valid queue family index for the given physical
    /// device.</param>
    /// <returns><c>true</c> if supported, <c>false</c> if unsupported or an error occurred.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="VulkanGetInstanceExtensions"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Vulkan_GetPresentationSupport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool VulkanGetPresentationSupport(IntPtr instance, IntPtr physicalDevice, uint queueFamilyIndex);
}