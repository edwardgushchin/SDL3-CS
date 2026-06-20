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

namespace Renderer_Textures;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static SampleTexture? _texture;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Textures",
            "com.example.renderer-textures",
            "examples/renderer/textures",
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
        var ticks = SDL.GetTicks();
        var direction = (ticks % 2000) >= 1000 ? 1.0f : -1.0f;
        var scale = ((((int)(ticks % 1000)) - 500) / 500.0f) * direction;

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        var dstRect = new SDL.FRect
        {
            X = 100.0f * scale,
            Y = 0.0f,
            W = texture.Width,
            H = texture.Height
        };
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        dstRect.X = (WindowWidth - texture.Width) / 2.0f;
        dstRect.Y = (WindowHeight - texture.Height) / 2.0f;
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        dstRect.X = WindowWidth - texture.Width - (100.0f * scale);
        dstRect.Y = WindowHeight - texture.Height;
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, in dstRect);

        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        _texture?.Dispose();
        _texture = null;
    }
}
