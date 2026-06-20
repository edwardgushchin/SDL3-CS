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

namespace Renderer_Read_Pixels;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static SampleTexture? _texture;
    private static IntPtr _convertedTexture;
    private static int _convertedTextureWidth;
    private static int _convertedTextureHeight;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Read Pixels",
            "com.example.renderer-read-pixels",
            "examples/renderer/read-pixels",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup);
    }

    private static void Configure(RendererExampleContext context)
    {
        _texture = SampleTexture.Create(context.Renderer);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var texture = _texture ?? throw new InvalidOperationException("Texture was not created.");
        var rotation = (SDL.GetTicks() % 2000) / 2000.0 * 360.0;

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        var dstRect = new SDL.FRect
        {
            X = (WindowWidth - texture.Width) / 2.0f,
            Y = (WindowHeight - texture.Height) / 2.0f,
            W = texture.Width,
            H = texture.Height
        };
        var center = new SDL.FPoint
        {
            X = texture.Width / 2.0f,
            Y = texture.Height / 2.0f
        };
        SDL.RenderTextureRotated(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect, rotation, in center, SDL.FlipMode.None);

        var surface = SDL.RenderReadPixels(context.Renderer, null);
        if (surface != IntPtr.Zero)
        {
            DrawConvertedReadback(context.Renderer, surface);
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawConvertedReadback(IntPtr renderer, IntPtr surface)
    {
        var surfaceInfo = Marshal.PtrToStructure<SDL.Surface>(surface);
        if (surfaceInfo.Format != SDL.PixelFormat.RGBA8888)
        {
            var converted = SDL.ConvertSurface(surface, SDL.PixelFormat.RGBA8888);
            SDL.DestroySurface(surface);
            surface = converted;
            if (surface == IntPtr.Zero)
            {
                return;
            }

            surfaceInfo = Marshal.PtrToStructure<SDL.Surface>(surface);
        }

        try
        {
            RebuildConvertedTexture(renderer, surfaceInfo.Width, surfaceInfo.Height);

            var pixels = new byte[surfaceInfo.Pitch * surfaceInfo.Height];
            Marshal.Copy(surfaceInfo.Pixels, pixels, 0, pixels.Length);
            for (var y = 0; y < surfaceInfo.Height; y++)
            {
                for (var x = 0; x < surfaceInfo.Width; x++)
                {
                    var offset = (y * surfaceInfo.Pitch) + (x * 4);
                    var average = (pixels[offset + 1] + pixels[offset + 2] + pixels[offset + 3]) / 3;
                    if (average == 0)
                    {
                        pixels[offset] = 0xFF;
                        pixels[offset + 1] = 0x00;
                        pixels[offset + 2] = 0x00;
                        pixels[offset + 3] = 0xFF;
                    }
                    else
                    {
                        var value = average > 50 ? (byte)0xFF : (byte)0x00;
                        pixels[offset + 1] = value;
                        pixels[offset + 2] = value;
                        pixels[offset + 3] = value;
                    }
                }
            }

            SDL.UpdateTexture(_convertedTexture, IntPtr.Zero, pixels, surfaceInfo.Pitch);

            var dstRect = new SDL.FRect
            {
                X = 0.0f,
                Y = 0.0f,
                W = WindowWidth / 4.0f,
                H = WindowHeight / 4.0f
            };
            SDL.RenderTexture(renderer, _convertedTexture, IntPtr.Zero, in dstRect);
        }
        finally
        {
            SDL.DestroySurface(surface);
        }
    }

    private static void RebuildConvertedTexture(IntPtr renderer, int width, int height)
    {
        if (_convertedTexture != IntPtr.Zero && width == _convertedTextureWidth && height == _convertedTextureHeight)
        {
            return;
        }

        if (_convertedTexture != IntPtr.Zero)
        {
            SDL.DestroyTexture(_convertedTexture);
        }

        _convertedTexture = SDL.CreateTexture(renderer, SDL.PixelFormat.RGBA8888, SDL.TextureAccess.Streaming, width, height);
        if (_convertedTexture == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create conversion texture: {SDL.GetError()}");
        }

        _convertedTextureWidth = width;
        _convertedTextureHeight = height;
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_convertedTexture != IntPtr.Zero)
        {
            SDL.DestroyTexture(_convertedTexture);
            _convertedTexture = IntPtr.Zero;
        }

        _texture?.Dispose();
        _texture = null;
    }
}
