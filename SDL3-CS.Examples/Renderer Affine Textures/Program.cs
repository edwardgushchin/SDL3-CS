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

namespace Renderer_Affine_Textures;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static SampleTexture? _texture;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Affine Textures",
            "com.example.renderer-affine-textures",
            "examples/renderer/affine-textures",
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
        var x0 = 0.5f * WindowWidth;
        var y0 = 0.5f * WindowHeight;
        var px = Math.Min(WindowWidth, WindowHeight) / MathF.Sqrt(3.0f);
        var radians = ((SDL.GetTicks() % 2000) / 2000.0f) * MathF.PI * 2;
        var cos = MathF.Cos(radians);
        var sin = MathF.Sin(radians);
        var k = new[] { 3.0f / MathF.Sqrt(50.0f), 4.0f / MathF.Sqrt(50.0f), 5.0f / MathF.Sqrt(50.0f) };
        var matrix = new[]
        {
            cos + ((1.0f - cos) * k[0] * k[0]),
            (-sin * k[2]) + ((1.0f - cos) * k[0] * k[1]),
            (sin * k[1]) + ((1.0f - cos) * k[0] * k[2]),
            (sin * k[2]) + ((1.0f - cos) * k[0] * k[1]),
            cos + ((1.0f - cos) * k[1] * k[1]),
            (-sin * k[0]) + ((1.0f - cos) * k[1] * k[2]),
            (-sin * k[1]) + ((1.0f - cos) * k[0] * k[2]),
            (sin * k[0]) + ((1.0f - cos) * k[1] * k[2]),
            cos + ((1.0f - cos) * k[2] * k[2])
        };
        var corners = new float[16];

        for (var i = 0; i < 8; i++)
        {
            var x = (i & 1) != 0 ? -0.5f : 0.5f;
            var y = (i & 2) != 0 ? -0.5f : 0.5f;
            var z = (i & 4) != 0 ? -0.5f : 0.5f;
            corners[0 + (2 * i)] = (matrix[0] * x) + (matrix[1] * y) + (matrix[2] * z);
            corners[1 + (2 * i)] = (matrix[3] * x) + (matrix[4] * y) + (matrix[5] * z);
        }

        SDL.SetRenderDrawColor(context.Renderer, 0x42, 0x87, 0xf5, 255);
        SDL.RenderClear(context.Renderer);

        for (var i = 1; i < 7; i++)
        {
            var dir = 3 & (((i & 4) != 0) ? ~i : i);
            var odd = (i & 1) ^ ((i & 2) >> 1) ^ ((i & 4) >> 2);
            if (0 < ((odd != 0 ? 1.0f : -1.0f) * matrix[5 + dir]))
            {
                continue;
            }

            var originIndex = 1 << ((dir - 1) % 3);
            var rightIndex = (1 << ((dir + odd) % 3)) | originIndex;
            var downIndex = (1 << ((dir + (odd ^ 1)) % 3)) | originIndex;
            if (odd == 0)
            {
                originIndex ^= 7;
                rightIndex ^= 7;
                downIndex ^= 7;
            }

            var origin = CubePoint(x0, y0, px, corners, originIndex);
            var right = CubePoint(x0, y0, px, corners, rightIndex);
            var down = CubePoint(x0, y0, px, corners, downIndex);
            RenderTextureAffine(context.Renderer, texture.Handle, origin, right, down);
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static SDL.FPoint CubePoint(float x0, float y0, float px, float[] corners, int index)
    {
        return new SDL.FPoint
        {
            X = x0 + (px * corners[0 + (2 * index)]),
            Y = y0 + (px * corners[1 + (2 * index)])
        };
    }

    private static void RenderTextureAffine(IntPtr renderer, IntPtr texture, SDL.FPoint origin, SDL.FPoint right, SDL.FPoint down)
    {
        var size = Marshal.SizeOf<SDL.FPoint>();
        var originPtr = Marshal.AllocHGlobal(size);
        var rightPtr = Marshal.AllocHGlobal(size);
        var downPtr = Marshal.AllocHGlobal(size);

        try
        {
            Marshal.StructureToPtr(origin, originPtr, false);
            Marshal.StructureToPtr(right, rightPtr, false);
            Marshal.StructureToPtr(down, downPtr, false);
            SDL.RenderTextureAffine(renderer, texture, IntPtr.Zero, originPtr, rightPtr, downPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(originPtr);
            Marshal.FreeHGlobal(rightPtr);
            Marshal.FreeHGlobal(downPtr);
        }
    }

    private static void Cleanup(RendererExampleContext context)
    {
        _texture?.Dispose();
        _texture = null;
    }
}
