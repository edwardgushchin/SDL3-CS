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

namespace Renderer_Cliprect;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const int ClipRectSize = 250;
    private const int ClipRectSpeed = 200;
    private static SampleTexture? _texture;
    private static SDL.FPoint _clipRectPosition;
    private static SDL.FPoint _clipRectDirection;
    private static ulong _lastTime;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Renderer Clipping Rectangle",
            "com.example.renderer-cliprect",
            "examples/renderer/cliprect",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup);
    }

    private static void Configure(RendererExampleContext context)
    {
        _texture = SampleTexture.Create(context.Renderer);
        _clipRectDirection = new SDL.FPoint { X = 1.0f, Y = 1.0f };
        _lastTime = SDL.GetTicks();
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var texture = _texture ?? throw new InvalidOperationException("Texture was not created.");
        var currentTime = SDL.GetTicks();
        var elapsed = (currentTime - _lastTime) / 1000.0f;
        var distance = elapsed * ClipRectSpeed;

        MoveClipRect(distance);

        var clipRect = new SDL.Rect
        {
            X = (int)MathF.Round(_clipRectPosition.X),
            Y = (int)MathF.Round(_clipRectPosition.Y),
            W = ClipRectSize,
            H = ClipRectSize
        };
        SDL.SetRenderClipRect(context.Renderer, in clipRect);
        _lastTime = currentTime;

        SDL.SetRenderDrawColor(context.Renderer, 33, 33, 33, 255);
        SDL.RenderClear(context.Renderer);
        SDL.RenderTexture(context.Renderer, texture.Handle, IntPtr.Zero, IntPtr.Zero);
        SDL.RenderPresent(context.Renderer);
    }

    private static void MoveClipRect(float distance)
    {
        _clipRectPosition.X += distance * _clipRectDirection.X;
        if (_clipRectPosition.X < -ClipRectSize)
        {
            _clipRectPosition.X = -ClipRectSize;
            _clipRectDirection.X = 1.0f;
        }
        else if (_clipRectPosition.X >= WindowWidth)
        {
            _clipRectPosition.X = WindowWidth - 1;
            _clipRectDirection.X = -1.0f;
        }

        _clipRectPosition.Y += distance * _clipRectDirection.Y;
        if (_clipRectPosition.Y < -ClipRectSize)
        {
            _clipRectPosition.Y = -ClipRectSize;
            _clipRectDirection.Y = 1.0f;
        }
        else if (_clipRectPosition.Y >= WindowHeight)
        {
            _clipRectPosition.Y = WindowHeight - 1;
            _clipRectDirection.Y = -1.0f;
        }
    }

    private static void Cleanup(RendererExampleContext context)
    {
        SDL.SetRenderClipRect(context.Renderer, IntPtr.Zero);
        _texture?.Dispose();
        _texture = null;
    }
}
