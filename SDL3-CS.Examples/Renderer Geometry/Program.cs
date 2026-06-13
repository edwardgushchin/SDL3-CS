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

namespace Renderer_Geometry;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly int[] Indices = [0, 1, 2, 1, 2, 3];
    private static SampleTexture? _texture;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Geometry",
            "com.example.renderer-geometry",
            "examples/renderer/geometry",
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
        var size = 200.0f + (200.0f * scale);

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        SDL.Vertex[] vertices =
        [
            new()
            {
                Position = new SDL.FPoint { X = WindowWidth / 2.0f, Y = (WindowHeight - size) / 2.0f },
                Color = new SDL.FColor(1.0f, 0.0f, 0.0f, 1.0f)
            },
            new()
            {
                Position = new SDL.FPoint { X = (WindowWidth + size) / 2.0f, Y = (WindowHeight + size) / 2.0f },
                Color = new SDL.FColor(0.0f, 1.0f, 0.0f, 1.0f)
            },
            new()
            {
                Position = new SDL.FPoint { X = (WindowWidth - size) / 2.0f, Y = (WindowHeight + size) / 2.0f },
                Color = new SDL.FColor(0.0f, 0.0f, 1.0f, 1.0f)
            }
        ];
        SDL.RenderGeometry(context.Renderer, IntPtr.Zero, vertices, vertices.Length, IntPtr.Zero, 0);

        vertices =
        [
            TextureVertex(10, 10, 0.0f, 0.0f),
            TextureVertex(150, 10, 1.0f, 0.0f),
            TextureVertex(10, 150, 0.0f, 1.0f),
            TextureVertex(150, 150, 1.0f, 1.0f)
        ];
        SDL.RenderGeometry(context.Renderer, texture.Handle, vertices, 3, IntPtr.Zero, 0);

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i].Position.X += 450.0f;
        }

        SDL.RenderGeometry(context.Renderer, texture.Handle, vertices, vertices.Length, Indices, Indices.Length);
        SDL.RenderPresent(context.Renderer);
    }

    private static SDL.Vertex TextureVertex(float x, float y, float textureX, float textureY)
    {
        return new SDL.Vertex
        {
            Position = new SDL.FPoint { X = x, Y = y },
            Color = new SDL.FColor(1.0f, 1.0f, 1.0f, 1.0f),
            TexCoord = new SDL.FPoint { X = textureX, Y = textureY }
        };
    }

    private static void Cleanup(RendererExampleContext context)
    {
        _texture?.Dispose();
        _texture = null;
    }
}
