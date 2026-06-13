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

using System.Runtime.InteropServices;
using SDL3;
using SDL3.Examples.Common;

namespace Renderer_Streaming_Textures;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const int TextureSize = 150;
    private static IntPtr _texture;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Streaming Textures",
            "com.example.renderer-streaming-textures",
            "examples/renderer/streaming-textures",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup);
    }

    private static void Configure(RendererExampleContext context)
    {
        _texture = SDL.CreateTexture(
            context.Renderer,
            SDL.PixelFormat.RGBA8888,
            SDL.TextureAccess.Streaming,
            TextureSize,
            TextureSize);

        if (_texture == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create streaming texture: {SDL.GetError()}");
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var ticks = SDL.GetTicks();
        var direction = (ticks % 2000) >= 1000 ? 1.0f : -1.0f;
        var scale = ((((int)(ticks % 1000)) - 500) / 500.0f) * direction;

        if (SDL.LockTextureToSurface(_texture, IntPtr.Zero, out var surface))
        {
            var surfaceInfo = Marshal.PtrToStructure<SDL.Surface>(surface);
            var pixelDetails = SDL.GetPixelFormatDetails(surfaceInfo.Format);
            var black = SDL.MapRGB(pixelDetails, IntPtr.Zero, 0, 0, 0);
            var green = SDL.MapRGB(pixelDetails, IntPtr.Zero, 0, 255, 0);

            SDL.FillSurfaceRect(surface, IntPtr.Zero, black);
            var stripe = new SDL.Rect
            {
                X = 0,
                Y = (int)((TextureSize - (TextureSize / 10)) * ((scale + 1.0f) / 2.0f)),
                W = TextureSize,
                H = TextureSize / 10
            };
            SDL.FillSurfaceRect(surface, in stripe, green);
            SDL.UnlockTexture(_texture);
        }

        SDL.SetRenderDrawColor(context.Renderer, 66, 66, 66, 255);
        SDL.RenderClear(context.Renderer);

        var dstRect = new SDL.FRect
        {
            X = (WindowWidth - TextureSize) / 2.0f,
            Y = (WindowHeight - TextureSize) / 2.0f,
            W = TextureSize,
            H = TextureSize
        };
        SDL.RenderTexture(context.Renderer, _texture, IntPtr.Zero, in dstRect);
        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_texture != IntPtr.Zero)
        {
            SDL.DestroyTexture(_texture);
            _texture = IntPtr.Zero;
        }
    }
}
