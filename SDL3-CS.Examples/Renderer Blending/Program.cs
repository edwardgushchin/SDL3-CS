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

namespace Renderer_Blending;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const int Rows = 2;
    private const int Columns = 3;
    private const float GridSize = (WindowWidth - 1) / 18.0f;
    private const float PanelSize = GridSize * 4.0f;
    private const float RowOffset = (WindowHeight - (Rows * PanelSize)) / 4.0f;
    private const float ColumnOffset = GridSize * Columns;
    private const float RectSize = 50.0f;
    private const float RedOffset = GridSize;
    private const float GreenOffset = (RectSize / 3.0f) + GridSize;
    private const float BlueOffset = (RectSize * 2.0f / 3.0f) + GridSize;
    private static readonly SDL.FRect[] Panels = new SDL.FRect[Rows * Columns];
    private static readonly string[] BlendModeNames = ["NONE", "BLEND", "ADD", "MOD", "MUL", "SCREEN CUSTOM"];
    private static readonly SDL.BlendMode[] BlendModes =
    [
        SDL.BlendMode.None,
        SDL.BlendMode.Blend,
        SDL.BlendMode.Add,
        SDL.BlendMode.Mod,
        SDL.BlendMode.Mul,
        SDL.BlendMode.Invalid
    ];
    private static IntPtr _redTexture;
    private static IntPtr _greenTexture;
    private static IntPtr _blueTexture;
    private static byte _alpha = 255;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Blending",
            "com.example.blending",
            "examples/renderer/blending",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent);
    }

    private static void Configure(RendererExampleContext context)
    {
        for (var row = 0; row < Rows; row++)
        {
            for (var column = 0; column < Columns; column++)
            {
                Panels[column + (row * Columns)] = new SDL.FRect
                {
                    X = (column * PanelSize) + (column * ColumnOffset),
                    Y = (row * PanelSize) + ((row + 1) * RowOffset),
                    W = PanelSize,
                    H = PanelSize
                };
            }
        }

        BlendModes[^1] = SDL.ComposeCustomBlendMode(
            SDL.BlendFactor.OneMinusDstColor,
            SDL.BlendFactor.One,
            SDL.BlendOperation.Add,
            SDL.BlendFactor.Zero,
            SDL.BlendFactor.One,
            SDL.BlendOperation.Add);

        using var surface = new SolidSurface((int)RectSize, (int)RectSize);
        _redTexture = surface.CreateTexture(context.Renderer, 255, 0, 0);
        _greenTexture = surface.CreateTexture(context.Renderer, 0, 255, 0);
        _blueTexture = surface.CreateTexture(context.Renderer, 0, 0, 255);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        if (sdlEvent.Type != (uint)SDL.EventType.KeyDown)
        {
            return true;
        }

        if (sdlEvent.Key.Key == SDL.Keycode.Up && _alpha <= 247)
        {
            _alpha += 8;
        }
        else if (sdlEvent.Key.Key == SDL.Keycode.Down && _alpha >= 8)
        {
            _alpha -= 8;
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        for (var i = 0; i < Panels.Length; i++)
        {
            DrawCheckerboardPanel(context.Renderer, Panels[i]);
            SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
            SDL.RenderDebugText(context.Renderer, Panels[i].X, Panels[i].Y - 15, BlendModeNames[i]);
        }

        SDL.RenderRects(context.Renderer, Panels, Panels.Length);
        SDL.RenderDebugText(context.Renderer, (WindowWidth - 176) / 2.0f, WindowHeight - 30, "UP/DOWN: CHANGE ALPHA");
        SDL.RenderDebugText(context.Renderer, (WindowWidth - 80) / 2.0f, WindowHeight - 20, $"ALPHA: {_alpha}");

        SDL.SetTextureAlphaMod(_redTexture, _alpha);
        SDL.SetTextureAlphaMod(_greenTexture, _alpha);
        SDL.SetTextureAlphaMod(_blueTexture, _alpha);

        for (var i = 0; i < Panels.Length; i++)
        {
            var redDst = RectInPanel(Panels[i], RedOffset);
            var greenDst = RectInPanel(Panels[i], GreenOffset);
            var blueDst = RectInPanel(Panels[i], BlueOffset);

            var supported = SDL.SetTextureBlendMode(_redTexture, BlendModes[i]);
            SDL.SetTextureBlendMode(_greenTexture, BlendModes[i]);
            SDL.SetTextureBlendMode(_blueTexture, BlendModes[i]);

            SDL.RenderTexture(context.Renderer, _redTexture, IntPtr.Zero, in redDst);
            SDL.RenderTexture(context.Renderer, _greenTexture, IntPtr.Zero, in greenDst);
            SDL.RenderTexture(context.Renderer, _blueTexture, IntPtr.Zero, in blueDst);

            if (!supported)
            {
                var dst = new SDL.FRect
                {
                    X = Panels[i].X + ((Panels[i].W - 104.0f) / 2.0f),
                    Y = Panels[i].Y + (Panels[i].H - SDL.DebugTextFontCharacterSize),
                    W = 104.0f,
                    H = SDL.DebugTextFontCharacterSize
                };
                SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
                SDL.RenderFillRect(context.Renderer, in dst);
                SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
                SDL.RenderDebugText(context.Renderer, dst.X, dst.Y, "[UNSUPPORTED]");
            }
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void DrawCheckerboardPanel(IntPtr renderer, SDL.FRect panel)
    {
        for (var y = panel.Y; y < panel.Y + PanelSize; y += GridSize)
        {
            for (var x = panel.X; x < panel.X + PanelSize; x += GridSize)
            {
                var grid = new SDL.FRect { X = x, Y = y, W = GridSize, H = GridSize };
                var dark = (((int)(x / GridSize)) + ((int)(y / GridSize))) % 2 != 0;
                if (dark)
                {
                    SDL.SetRenderDrawColor(renderer, 70, 70, 70, 255);
                }
                else
                {
                    SDL.SetRenderDrawColor(renderer, 110, 110, 110, 255);
                }

                SDL.RenderFillRect(renderer, in grid);
            }
        }
    }

    private static SDL.FRect RectInPanel(SDL.FRect panel, float offset)
    {
        return new SDL.FRect
        {
            X = panel.X + offset,
            Y = panel.Y + offset,
            W = RectSize,
            H = RectSize
        };
    }

    private static void Cleanup(RendererExampleContext context)
    {
        DestroyTexture(ref _redTexture);
        DestroyTexture(ref _greenTexture);
        DestroyTexture(ref _blueTexture);
    }

    private static void DestroyTexture(ref IntPtr texture)
    {
        if (texture == IntPtr.Zero)
        {
            return;
        }

        SDL.DestroyTexture(texture);
        texture = IntPtr.Zero;
    }

    private sealed class SolidSurface : IDisposable
    {
        private readonly IntPtr _surface;

        public SolidSurface(int width, int height)
        {
            _surface = SDL.CreateSurface(width, height, SDL.PixelFormat.RGBA8888);
            if (_surface == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Couldn't create surface: {SDL.GetError()}");
            }
        }

        public IntPtr CreateTexture(IntPtr renderer, byte red, byte green, byte blue)
        {
            var details = SDL.GetPixelFormatDetails(SDL.PixelFormat.RGBA8888);
            var color = SDL.MapRGBA(details, IntPtr.Zero, red, green, blue, 255);
            SDL.FillSurfaceRect(_surface, IntPtr.Zero, color);

            var texture = SDL.CreateTextureFromSurface(renderer, _surface);
            if (texture == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Couldn't create texture: {SDL.GetError()}");
            }

            return texture;
        }

        public void Dispose()
        {
            if (_surface != IntPtr.Zero)
            {
                SDL.DestroySurface(_surface);
            }
        }
    }
}
