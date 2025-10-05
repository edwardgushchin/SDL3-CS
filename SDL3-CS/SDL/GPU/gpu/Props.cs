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

namespace SDL3;

public static partial class SDL
{
    public static partial class Props
    {
        public const string GPUTextureCreateD3D12ClearRFloat = "SDL.gpu.texture.create.d3d12.clear.r";
        public const string GPUTextureCreateD3D12ClearGFloat = "SDL.gpu.texture.create.d3d12.clear.g";
        public const string GPUTextureCreateD3D12ClearBFloat = "SDL.gpu.texture.create.d3d12.clear.b";
        public const string GPUTextureCreateD3D12ClearAFloat = "SDL.gpu.texture.create.d3d12.clear.a";
        public const string GPUTextureCreateD3D12ClearDepthFloat = "SDL.gpu.texture.create.d3d12.clear.depth";
        public const string GPUTextureCreateD3D12ClearStencilUint8 = "SDL.gpu.texture.create.d3d12.clear.stencil";
        public const string GPUTextureCreateD3D12ClearStencilNumber = "SDL.gpu.texture.create.d3d12.clear.stencil";
        public const string GPUTextureCreateNameString = "SDL.gpu.texture.create.name";

        public const string GPUDeviceCreateDebugModeBoolean = "SDL.gpu.device.create.debugmode";
        public const string GPUDeviceCreatePreferLowPowerBoolean = "SDL.gpu.device.create.preferlowpower";
        public const string GPUDeviceCreateVerboseBoolean = "SDL.gpu.device.create.verbose";
        public const string GPUDeviceCreateNameString = "SDL.gpu.device.create.name";
        public const string GPUDeviceCreateFeatureClipDistanceBoolean = "SDL.gpu.device.create.feature.clip_distance";
        public const string GPUDeviceCreateFeatureDepthClampingBoolean = "SDL.gpu.device.create.feature.depth_clamping";
        public const string GPUDeviceCreateFeatureIndirectDrawFirstInstanceBoolean = "SDL.gpu.device.create.feature.indirect_draw_first_instance";
        public const string GPUDeviceCreateFeatureAnisotropyBoolean = "SDL.gpu.device.create.feature.anisotropy";
        public const string GPUDeviceCreateShadersPrivateBoolean = "SDL.gpu.device.create.shaders.private";
        public const string GPUDeviceCreateShadersSPIRVBoolean = "SDL.gpu.device.create.shaders.spirv";
        public const string GPUDeviceCreateShadersDXBCBoolean = "SDL.gpu.device.create.shaders.dxbc";
        public const string GPUDeviceCreateShadersDXILBoolean = "SDL.gpu.device.create.shaders.dxil";
        public const string GPUDeviceCreateShadersMSLBoolean = "SDL.gpu.device.create.shaders.msl";
        public const string GPUDeviceCreateShadersMetalLibBoolean = "SDL.gpu.device.create.shaders.metallib";
        public const string GPUDeviceCreateD3D12AllowFewerResourceSlotsBoolean = "SDL.gpu.device.create.d3d12.allowtier1resourcebinding";
        public const string GPUDeviceCreateD3D12SemanticNameString = "SDL.gpu.device.create.d3d12.semantic";
        public const string GPUDeviceCreateVulkanShaderClipDistanceBoolean = "SDL.gpu.device.create.vulkan.shaderclipdistance";
        public const string GPUDeviceCreateVulkanDepthClampBoolean = "SDL.gpu.device.create.vulkan.depthclamp";
        public const string GPUDeviceCreateVulkanDrawInDirectFirstBoolean =  "SDL.gpu.device.create.vulkan.drawindirectfirstinstance";
        public const string GPUDeviceCreateVulkanSamplerAnisotropyBoolean =  "SDL.gpu.device.create.vulkan.sampleranisotropy";
        
        public const string GPUGraphicsPipelineCreateNameString = "SDL.gpu.graphicspipeline.create.name";
        public const string GPUSamplerCreateNameString = "SDL.gpu.sampler.create.name";
        public const string GPUShaderCreateNameString = "SDL.gpu.shader.create.name";
        public const string GPUBufferCreateNameString = "SDL.gpu.buffer.create.name";
        public const string GPUTransferBufferCreateNameString = "SDL.gpu.transferbuffer.create.name";

        public const string GPUDeviceNameString = "SDL.gpu.device.name";
        public const string GPUDeviceDriverNameString = "SDL.gpu.device.driver_name";
        public const string GPUDeviceDriverVersionString = "SDL.gpu.device.driver_version";
        public const string GPUDeviceDriverInfoString = "SDL.gpu.device.driver_info";
    }
}