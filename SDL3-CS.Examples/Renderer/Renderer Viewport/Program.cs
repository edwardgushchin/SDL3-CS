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

using SDL3;
using SDL3.Examples.Common;

namespace Renderer_Viewport;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static SampleTexture? _texture;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Viewport",
            "com.example.renderer-viewport",
            "examples/renderer/viewport",
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
        var dstRect = new SDL.FRect { X = 0.0f, Y = 0.0f, W = texture.Width, H = texture.Height };

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        SDL.SetRenderViewport(context.Renderer, IntPtr.Zero);
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        var viewport = new SDL.Rect
        {
            X = WindowWidth / 2,
            Y = WindowHeight / 2,
            W = WindowWidth / 2,
            H = WindowHeight / 2
        };
        SDL.SetRenderViewport(context.Renderer, viewport);
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        viewport = new SDL.Rect
        {
            X = 0,
            Y = WindowHeight - (WindowHeight / 5),
            W = WindowWidth / 5,
            H = WindowHeight / 5
        };
        SDL.SetRenderViewport(context.Renderer, viewport);
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        viewport = new SDL.Rect
        {
            X = 100,
            Y = 200,
            W = WindowWidth,
            H = WindowHeight
        };
        dstRect.Y = -50;
        SDL.SetRenderViewport(context.Renderer, viewport);
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        SDL.SetRenderViewport(context.Renderer, IntPtr.Zero);
        _texture?.Dispose();
        _texture = null;
    }
}
