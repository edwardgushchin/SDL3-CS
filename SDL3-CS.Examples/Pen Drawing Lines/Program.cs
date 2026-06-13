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

namespace Pen_Drawing_Lines;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static IntPtr _canvas;
    private static IntPtr _renderer;
    private static float _lastX = -1.0f;
    private static float _lastY = -1.0f;
    private static float _pressure;
    private static float _tiltX;
    private static float _tiltY;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Pen Drawing Lines",
            "com.example.pen-drawing-lines",
            "examples/pen/drawing-lines",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent);
    }

    private static void Configure(RendererExampleContext context)
    {
        _renderer = context.Renderer;

        if (!SDL.GetRenderOutputSize(context.Renderer, out var width, out var height))
        {
            throw new InvalidOperationException($"Couldn't get render output size: {SDL.GetError()}");
        }

        _canvas = SDL.CreateTexture(context.Renderer, SDL.PixelFormat.RGBA8888, SDL.TextureAccess.Target, width, height);
        if (_canvas == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create drawing canvas: {SDL.GetError()}");
        }

        SDL.SetTextureBlendMode(_canvas, SDL.BlendMode.Blend);
        SDL.SetRenderTarget(context.Renderer, _canvas);
        SDL.SetRenderDrawColor(context.Renderer, 32, 32, 32, 255);
        SDL.RenderClear(context.Renderer);
        SDL.SetRenderTarget(context.Renderer, IntPtr.Zero);
        SDL.SetRenderDrawBlendMode(context.Renderer, SDL.BlendMode.Blend);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        if (_renderer != IntPtr.Zero)
        {
            SDL.ConvertEventToRenderCoordinates(_renderer, ref sdlEvent);
        }

        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.PenMotion:
                DrawPenMotion(sdlEvent.PMotion.X, sdlEvent.PMotion.Y);
                break;

            case SDL.EventType.PenAxis:
                UpdateAxis(sdlEvent.PAxis.Axis, sdlEvent.PAxis.Value);
                break;
        }

        return true;
    }

    private static void DrawPenMotion(float x, float y)
    {
        if (_canvas == IntPtr.Zero || _pressure <= 0.0f)
        {
            _lastX = -1.0f;
            _lastY = -1.0f;
            return;
        }

        SDL.SetRenderTarget(_renderer, _canvas);
        SDL.SetRenderDrawColorFloat(_renderer, 1.0f, 1.0f, 1.0f, Math.Clamp(_pressure, 0.05f, 1.0f));

        if (_lastX >= 0.0f && _lastY >= 0.0f)
        {
            SDL.RenderLine(_renderer, _lastX, _lastY, x, y);
        }
        else
        {
            SDL.RenderLine(_renderer, x, y, x + 1.0f, y);
        }

        SDL.SetRenderTarget(_renderer, IntPtr.Zero);
        _lastX = x;
        _lastY = y;
    }

    private static void UpdateAxis(SDL.PenAxis axis, float value)
    {
        switch (axis)
        {
            case SDL.PenAxis.Pressure:
                _pressure = value;
                break;

            case SDL.PenAxis.XTilt:
                _tiltX = value;
                break;

            case SDL.PenAxis.YTilt:
                _tiltY = value;
                break;
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        if (_canvas != IntPtr.Zero)
        {
            SDL.RenderTexture(context.Renderer, _canvas, IntPtr.Zero, IntPtr.Zero);
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 255, 0, 255);
        SDL.RenderDebugText(context.Renderer, 10.0f, 10.0f, $"Pressure: {_pressure:0.00}");
        SDL.RenderDebugText(context.Renderer, 10.0f, 26.0f, $"Tilt: {_tiltX:0.00}, {_tiltY:0.00}");
        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_canvas != IntPtr.Zero)
        {
            SDL.DestroyTexture(_canvas);
            _canvas = IntPtr.Zero;
        }

        _renderer = IntPtr.Zero;
        _lastX = -1.0f;
        _lastY = -1.0f;
        _pressure = 0.0f;
        _tiltX = 0.0f;
        _tiltY = 0.0f;
    }
}
