using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Render;

internal static class MainlinePropsTests
{
    public static void RunAll()
    {
        TestAssert.Equal("SDL.renderer.create.metal.device", SDL3.SDL.Props.RendererCreateMetalDevicePointer, "SDL_PROP_RENDERER_CREATE_METAL_DEVICE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.renderer.create.metal.command_queue", SDL3.SDL.Props.RendererCreateMetalCommandQueuePointer, "SDL_PROP_RENDERER_CREATE_METAL_COMMAND_QUEUE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.renderer.metal.device", SDL3.SDL.Props.RendererMetalDevicePointer, "SDL_PROP_RENDERER_METAL_DEVICE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.renderer.metal.command_queue", SDL3.SDL.Props.RendererMetalCommandQueuePointer, "SDL_PROP_RENDERER_METAL_COMMAND_QUEUE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.metal.texture", SDL3.SDL.Props.TextureCreateMetalTexturePointer, "SDL_PROP_TEXTURE_CREATE_METAL_TEXTURE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.metal.texture_uv", SDL3.SDL.Props.TextureCreateMetalTextureUVPointer, "SDL_PROP_TEXTURE_CREATE_METAL_TEXTURE_UV_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.metal.texture_u", SDL3.SDL.Props.TextureCreateMetalTextureUPointer, "SDL_PROP_TEXTURE_CREATE_METAL_TEXTURE_U_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.metal.texture_v", SDL3.SDL.Props.TextureCreateMetalTextureVPointer, "SDL_PROP_TEXTURE_CREATE_METAL_TEXTURE_V_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.metal.texture_usage", SDL3.SDL.Props.TextureCreateMetalTextureUsageNumber, "SDL_PROP_TEXTURE_CREATE_METAL_TEXTURE_USAGE_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.vulkan.texture_u", SDL3.SDL.Props.TextureCreateVulkanTextureUNumber, "SDL_PROP_TEXTURE_CREATE_VULKAN_TEXTURE_U_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.vulkan.texture_v", SDL3.SDL.Props.TextureCreateVulkanTextureVNumber, "SDL_PROP_TEXTURE_CREATE_VULKAN_TEXTURE_V_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.vulkan.usage", SDL3.SDL.Props.TextureCreateVulkanUsageNumber, "SDL_PROP_TEXTURE_CREATE_VULKAN_USAGE_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.create.vulkan.android_hardware_buffer", SDL3.SDL.Props.TextureCreateVulkanAndroidHardwareBufferPointer, "SDL_PROP_TEXTURE_CREATE_VULKAN_ANDROID_HARDWARE_BUFFER_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.metal.texture", SDL3.SDL.Props.TextureMetalTexturePointer, "SDL_PROP_TEXTURE_METAL_TEXTURE_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.metal.texture_uv", SDL3.SDL.Props.TextureMetalTextureUVPointer, "SDL_PROP_TEXTURE_METAL_TEXTURE_UV_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.metal.texture_u", SDL3.SDL.Props.TextureMetalTextureUPointer, "SDL_PROP_TEXTURE_METAL_TEXTURE_U_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.metal.texture_v", SDL3.SDL.Props.TextureMetalTextureVPointer, "SDL_PROP_TEXTURE_METAL_TEXTURE_V_POINTER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.vulkan.texture_u", SDL3.SDL.Props.TextureVulkanTextureUNumber, "SDL_PROP_TEXTURE_VULKAN_TEXTURE_U_NUMBER must retain its native identifier.");
        TestAssert.Equal("SDL.texture.vulkan.texture_v", SDL3.SDL.Props.TextureVulkanTextureVNumber, "SDL_PROP_TEXTURE_VULKAN_TEXTURE_V_NUMBER must retain its native identifier.");
    }
}
