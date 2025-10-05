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
		public const string RendererCreateNameString = "SDL.renderer.create.name";
		public const string RendererCreateWindowPointer = "SDL.renderer.create.window";
		public const string RendererCreateSurfacePointer = "SDL.renderer.create.surface";
		public const string RendererCreateOutputColorspaceNumber = "SDL.renderer.create.output_colorspace";
		public const string RendererCreatePresentVSyncNumber = "SDL.renderer.create.present_vsync";
		public const string RendererCreateGPUDevicePointer = "SDL.renderer.create.gpu.device";
		public const string RendererCreateGPUShadersSPIRVBoolean = "SDL.renderer.create.gpu.shaders_spirv";
		public const string RendererCreateGPUShadersDXILBoolean = "SDL.renderer.create.gpu.shaders_dxil";
		public const string RendererCreateGPUShadersMSLBoolean = "SDL.renderer.create.gpu.shaders_msl";
		public const string RendererCreateVulkanInstancePointer = "SDL.renderer.create.vulkan.instance";
		public const string RendererCreateVulkanSurfaceNumber = "SDL.renderer.create.vulkan.surface";
		public const string RendererCreateVulkanPhysicalDevicePointer = "SDL.renderer.create.vulkan.physical_device";
		public const string RendererCreateVulkanDevicePointer = "SDL.renderer.create.vulkan.device";
		public const string RendererCreateVulkanGraphicsQueueFamilyIndexNumber = "SDL.renderer.create.vulkan.graphics_queue_family_index";
		public const string RendererCreateVulkanPresentQueueFamilyIndexNumber = "SDL.renderer.create.vulkan.present_queue_family_index";

		public const string RendererNameString = "SDL.renderer.name";
		public const string RendererWindowPointer = "SDL.renderer.window";
		public const string RendererSurfacePointer = "SDL.renderer.surface";
		public const string RendererVSyncNumber = "SDL.renderer.vsync";
		public const string RendererMaxTextureSizeNumber = "SDL.renderer.max_texture_size";
		public const string RendererTextureFormatsPointer = "SDL.renderer.texture_formats";
		public const string RendererOutputColorspaceNumber = "SDL.renderer.output_colorspace";
		public const string RendererHDREnabledBoolean = "SDL.renderer.HDR_enabled";
		public const string RendererSDRWhitePointFloat = "SDL.renderer.SDR_white_point";
		public const string RendererHDRHeadroomFloat = "SDL.renderer.HDR_headroom";
		public const string RendererD3D9DevicePointer = "SDL.renderer.d3d9.device";
		public const string RendererD3D11DevicePointer = "SDL.renderer.d3d11.device";
		public const string RendererD3D11SwapchainPointer = "SDL.renderer.d3d11.swap_chain";
		public const string RendererD3D12DevicePointer = "SDL.renderer.d3d12.device";
		public const string RendererD3D12SwapchainPointer = "SDL.renderer.d3d12.swap_chain";
		public const string RendererD3D12CommandQueuePointer = "SDL.renderer.d3d12.command_queue";
		public const string RendererVulkanInstancePointer = "SDL.renderer.vulkan.instance";
		public const string RendererVulkanSurfaceNumber = "SDL.renderer.vulkan.surface";
		public const string RendererVulkanPhysicalDevicePointer = "SDL.renderer.vulkan.physical_device";
		public const string RendererVulkanDevicePointer = "SDL.renderer.vulkan.device";
		public const string RendererVulkanGraphicsQueueFamilyIndexNumber = "SDL.renderer.vulkan.graphics_queue_family_index";
		public const string RendererVulkanPresentQueueFamilyIndexNumber = "SDL.renderer.vulkan.present_queue_family_index";
		public const string RendererVulkanSwapchainImageCountNumber = "SDL.renderer.vulkan.swapchain_image_count";
		public const string RendererGPUDevicePointer = "SDL.renderer.gpu.device";

		public const string TextureCreateColorspaceNumber = "SDL.texture.create.colorspace";
		public const string TextureCreateFormatNumber = "SDL.texture.create.format";
		public const string TextureCreateAccessNumber = "SDL.texture.create.access";
		public const string TextureCreateWidthNumber = "SDL.texture.create.width";
		public const string TextureCreateHeightNumber = "SDL.texture.create.height";
		public const string TextureCreatePalettePointer = "SDL.texture.create.palette";
		public const string TextureCreateSDRWhitePointFloat = "SDL.texture.create.SDR_white_point";
		public const string TextureCreateHDRHeadroomFloat = "SDL.texture.create.HDR_headroom";
		public const string TextureCreateD3D11TexturePointer = "SDL.texture.create.d3d11.texture";
		public const string TextureCreateD3D11TextureUPointer = "SDL.texture.create.d3d11.texture_u";
		public const string TextureCreateD3D11TextureVPointer = "SDL.texture.create.d3d11.texture_v";
		public const string TextureCreateD3D12TexturePointer = "SDL.texture.create.d3d12.texture";
		public const string TextureCreateD3D12TextureUPointer = "SDL.texture.create.d3d12.texture_u";
		public const string TextureCreateD3D12TextureVPointer = "SDL.texture.create.d3d12.texture_v";
		public const string TextureCreateMetalPixelbufferPointer = "SDL.texture.create.metal.pixelbuffer";
		public const string TextureCreateOpenGLTextureNumber = "SDL.texture.create.opengl.texture";
		public const string TextureCreateOpenGLTextureUVNumber = "SDL.texture.create.opengl.texture_uv";
		public const string TextureCreateOpenGlTextureUNumber = "SDL.texture.create.opengl.texture_u";
		public const string TextureCreateOpenGLTextureVNumber = "SDL.texture.create.opengl.texture_v";
		public const string TextureCreateOpenGLES2TextureNumber = "SDL.texture.create.opengles2.texture";
		public const string TextureCreateOpenGLES2TextureUVNumber = "SDL.texture.create.opengles2.texture_uv";
		public const string TextureCreateOpenGLES2TextureUNumber = "SDL.texture.create.opengles2.texture_u";
		public const string TextureCreateOpenGLES2TextureVNumber = "SDL.texture.create.opengles2.texture_v";
		public const string TextureCreateVulkanTextureNumber = "SDL.texture.create.vulkan.texture";

		public const string TextureColorspaceNumber = "SDL.texture.colorspace";
		public const string TextureFormatNumber = "SDL.texture.format";
		public const string TextureAccessNumber = "SDL.texture.access";
		public const string TextureWidthNumber = "SDL.texture.width";
		public const string TextureHeightNumber = "SDL.texture.height";
		public const string TextureSDRWhitePointFloat = "SDL.texture.SDR_white_point";
		public const string TextureHDRHeadroomFloat = "SDL.texture.HDR_headroom";
		public const string TextureD3D11TexturePointer = "SDL.texture.d3d11.texture";
		public const string TextureD3D11TextureUPointer = "SDL.texture.d3d11.texture_u";
		public const string TextureD3D11TextureVPointer = "SDL.texture.d3d11.texture_v";
		public const string TextureD3D12TexturePointer = "SDL.texture.d3d12.texture";
		public const string TextureD3D12TextureUPointer = "SDL.texture.d3d12.texture_u";
		public const string TextureD3D12TextureVPointer = "SDL.texture.d3d12.texture_v";
		public const string TextureOpenGLTextureNumber = "SDL.texture.opengl.texture";
		public const string TextureOpenGLTextureUVNumber = "SDL.texture.opengl.texture_uv";
		public const string TextureOpenGLTextureUNumber = "SDL.texture.opengl.texture_u";
		public const string TextureOpenGLTextureVNumber = "SDL.texture.opengl.texture_v";
		public const string TextureOpenGLTextureTargetNumber = "SDL.texture.opengl.target";
		public const string TextureOpenGLTexWFloat = "SDL.texture.opengl.tex_w";
		public const string TextureOpenGLTexHFloat = "SDL.texture.opengl.tex_h";
		public const string TextureOpenGLES2TextureNumber = "SDL.texture.opengles2.texture";
		public const string TextureOpenGLES2TextureUVNumber = "SDL.texture.opengles2.texture_uv";
		public const string TextureOpenGLES2TextureUNumber = "SDL.texture.opengles2.texture_u";
		public const string TextureOpenGLES2TextureVNumber = "SDL.texture.opengles2.texture_v";
		public const string TextureOpenGLES2TextureTargetNumber = "SDL.texture.opengles2.target";
		public const string TextureVulkanTextureNumber = "SDL.texture.vulkan.texture";
		public const string TextureGPUTexturePointer = "SDL.texture.gpu.texture";
		public const string TextureGPUTextureUVPointer = "SDL.texture.gpu.texture_uv";
		public const string TextureGPUTextureUPointer = "SDL.texture.gpu.texture_u";
		public const string TextureGPUTextureVPointer = "SDL.texture.gpu.texture_v";
	}
}