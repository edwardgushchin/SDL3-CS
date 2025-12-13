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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// A structure specifying additional options when using Vulkan.
    /// <para>When no such structure is provided, SDL will use Vulkan API version 1.0 and
    /// a minimal set of features. The requested API version influences how the
    /// feature_list is processed by SDL. When requesting API version 1.0, the
    /// feature_list is ignored. Only the vulkan_10_physical_device_features and
    /// the extension lists are used. When requesting API version 1.1, the
    /// feature_list is scanned for feature structures introduced in Vulkan 1.1.
    /// When requesting Vulkan 1.2 or higher, the feature_list is additionally
    /// scanned for compound feature structs such as
    /// VkPhysicalDeviceVulkan11Features. The device and instance extension lists,
    /// as well as vulkan_10_physical_device_features, are always processed.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.4.0.</since>
    /// <seealso cref="SetGPUViewport"/>
    /// <seealso cref="SDL.PointerToStringArray(System.IntPtr)"/>
    /// <seealso cref="SDL.StringArrayToPointer"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPUVulkanOptions
    {
        /// <summary>
        /// The Vulkan API version to request for the instance. Use Vulkan's VK_MAKE_VERSION or VK_MAKE_API_VERSION.
        /// </summary>
        public uint VulkanApiVersion;
        
        /// <summary>
        /// Pointer to the first element of a chain of Vulkan feature structs. (Requires API version 1.1 or higher.)
        /// </summary>
        public IntPtr FeatureList;
        
        /// <summary>
        /// Pointer to a VkPhysicalDeviceFeatures struct to enable additional Vulkan 1.0 features.
        /// </summary>
        public IntPtr Vulkan10PhysicalDeviceFeatures;
        
        /// <summary>
        /// Number of additional device extensions to require.
        /// </summary>
        public uint DeviceExtensionCount;
        
        /// <summary>
        /// Pointer to a list of additional device extensions to require.
        /// </summary>
        public IntPtr DeviceExtensionNames;
        
        /// <summary>
        /// Number of additional instance extensions to require.
        /// </summary>
        public uint InstanceExtensionCount;
        
        /// <summary>
        /// Pointer to a list of additional instance extensions to require.
        /// </summary>
        public IntPtr InstanceExtensionNames;
    }
}