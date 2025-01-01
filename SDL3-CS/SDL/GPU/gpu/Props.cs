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
        public const string GPUCreateTextureD3D12ClearRFloat = "SDL.gpu.createtexture.d3d12.clear.r";
        public const string GPUCreateTextureD3D12ClearGFloat = "SDL.gpu.createtexture.d3d12.clear.g";
        public const string GPUCreateTextureD3D12ClearBFloat = "SDL.gpu.createtexture.d3d12.clear.b";
        public const string GPUCreateTextureD3D12ClearAFloat = "SDL.gpu.createtexture.d3d12.clear.a";
        public const string GPUCreateTextureD3D12ClearDepthFloat = "SDL.gpu.createtexture.d3d12.clear.depth";
        public const string GPUCreateTextureD3D12ClearStencilUint8 = "SDL.gpu.createtexture.d3d12.clear.stencil";

        public const string GPUDeviceCreateDebugModeBoolean = "SDL.gpu.device.create.debugmode";
        public const string GPUDeviceCreatePreferLowPowerBoolean = "SDL.gpu.device.create.preferlowpower";
        public const string GPUDeviceCreateNameString = "SDL.gpu.device.create.name";
        public const string GPUDeviceCreateShadersPrivateBoolean = "SDL.gpu.device.create.shaders.private";
        public const string GPUDeviceCreateShadersSPIRVBoolean = "SDL.gpu.device.create.shaders.spirv";
        public const string GPUDeviceCreateShadersDXBCBoolean = "SDL.gpu.device.create.shaders.dxbc";
        public const string GPUDeviceCreateShadersDXILBoolean = "SDL.gpu.device.create.shaders.dxil";
        public const string GPUDeviceCreateShadersMSLBoolean = "SDL.gpu.device.create.shaders.msl";
        public const string GPUDeviceCreateShadersMetalLibBoolean = "SDL.gpu.device.create.shaders.metallib";
        public const string GPUDeviceCreateD3D12SemanticNameString = "SDL.gpu.device.create.d3d12.semantic";
    }
}