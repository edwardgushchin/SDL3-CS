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

namespace Asyncio_Load_Bitmaps;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly BitmapRequest[] Requests =
    [
        new("sample.png", new SDL.FRect { X = 50.0f, Y = 50.0f, W = 128.0f, H = 128.0f }, 96, 96, 0),
        new("gamepad_front.png", new SDL.FRect { X = 256.0f, Y = 42.0f, W = 240.0f, H = 160.0f }, 160, 96, 1),
        new("speaker.png", new SDL.FRect { X = 96.0f, Y = 260.0f, W = 128.0f, H = 128.0f }, 96, 96, 2),
        new("icon2x.png", new SDL.FRect { X = 356.0f, Y = 250.0f, W = 128.0f, H = 128.0f }, 96, 96, 3)
    ];
    private static readonly IntPtr[] Textures = new IntPtr[Requests.Length];
    private static IntPtr _queue;
    private static int _completedRequests;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Asyncio Load Bitmaps",
            "com.example.asyncio-load-bitmaps",
            "examples/asyncio/load-bitmaps",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup);
    }

    private static void Configure(RendererExampleContext context)
    {
        _queue = SDL.CreateAsyncIOQueue();
        if (_queue == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create async I/O queue: {SDL.GetError()}");
        }

        for (var i = 0; i < Requests.Length; i++)
        {
            var request = Requests[i];
            var path = Path.Combine(AppContext.BaseDirectory, request.FileName);
            EnsureGeneratedPng(path, request.Width, request.Height, request.Style);

            if (!SDL.LoadFileAsync(path, _queue, new IntPtr(i + 1)))
            {
                throw new InvalidOperationException($"Couldn't queue async load for {request.FileName}: {SDL.GetError()}");
            }
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        ProcessAsyncResults(context.Renderer);

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        for (var i = 0; i < Requests.Length; i++)
        {
            if (Textures[i] != IntPtr.Zero)
            {
                var destination = Requests[i].Destination;
                SDL.RenderTexture(context.Renderer, Textures[i], IntPtr.Zero, in destination);
            }
        }

        if (_completedRequests < Requests.Length)
        {
            SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
            SDL.RenderDebugText(context.Renderer, 16.0f, 16.0f, $"Loading bitmaps asynchronously... {_completedRequests}/{Requests.Length}");
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void ProcessAsyncResults(IntPtr renderer)
    {
        while (_queue != IntPtr.Zero && SDL.GetAsyncIOResult(_queue, out var outcome))
        {
            try
            {
                var index = outcome.Userdata.ToInt32() - 1;
                if (index < 0 || index >= Requests.Length)
                {
                    continue;
                }

                if (outcome.Result == SDL.AsyncIOResult.Complete)
                {
                    var io = SDL.IOFromConstMem(outcome.Buffer, (UIntPtr)outcome.BytesTransferred);
                    if (io == IntPtr.Zero)
                    {
                        throw new InvalidOperationException($"Couldn't create IO stream: {SDL.GetError()}");
                    }

                    var surface = SDL.LoadPNGIO(io, true);
                    if (surface == IntPtr.Zero)
                    {
                        throw new InvalidOperationException($"Couldn't load PNG from async buffer: {SDL.GetError()}");
                    }

                    try
                    {
                        if (Textures[index] != IntPtr.Zero)
                        {
                            SDL.DestroyTexture(Textures[index]);
                        }

                        Textures[index] = SDL.CreateTextureFromSurface(renderer, surface);
                        if (Textures[index] == IntPtr.Zero)
                        {
                            throw new InvalidOperationException($"Couldn't create texture: {SDL.GetError()}");
                        }

                        _completedRequests++;
                    }
                    finally
                    {
                        SDL.DestroySurface(surface);
                    }
                }
                else
                {
                    SDL.Log($"Async load failed for {Requests[index].FileName}: {outcome.Result}");
                    _completedRequests++;
                }
            }
            finally
            {
                if (outcome.Buffer != IntPtr.Zero)
                {
                    SDL.Free(outcome.Buffer);
                }
            }
        }
    }

    private static void EnsureGeneratedPng(string path, int width, int height, int style)
    {
        if (File.Exists(path))
        {
            return;
        }

        var surface = SDL.CreateSurface(width, height, SDL.PixelFormat.RGBA8888);
        if (surface == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create generated PNG surface: {SDL.GetError()}");
        }

        try
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var edge = x < 3 || y < 3 || x >= width - 3 || y >= height - 3;
                    var checker = ((x / 12) + (y / 12) + style) % 2 == 0;
                    var r = (byte)Math.Clamp((style * 55) + x, 0, 255);
                    var g = (byte)Math.Clamp(80 + (checker ? 110 : 30) + (style * 20), 0, 255);
                    var b = (byte)Math.Clamp(220 - (style * 35) - y, 0, 255);

                    if (style == 1)
                    {
                        var dx = (x - (width / 2.0f)) / (width / 2.0f);
                        var dy = (y - (height / 2.0f)) / (height / 2.0f);
                        var body = (dx * dx) + (dy * dy * 2.0f) < 0.85f;
                        r = (byte)(body ? 230 : 40);
                        g = (byte)(body ? 230 : 60);
                        b = (byte)(body ? 235 : 80);
                    }
                    else if (style == 2)
                    {
                        var ring = Math.Abs(Math.Sqrt(Math.Pow(x - width / 2.0, 2) + Math.Pow(y - height / 2.0, 2)) - 28.0) < 6.0;
                        r = (byte)(ring || edge ? 245 : 80);
                        g = (byte)(ring || edge ? 245 : 90);
                        b = (byte)(ring || edge ? 70 : 120);
                    }
                    else if (style == 3)
                    {
                        var diagonal = Math.Abs(x - y) < 8 || Math.Abs((width - x) - y) < 8;
                        r = (byte)(diagonal || edge ? 255 : 30);
                        g = (byte)(diagonal || edge ? 90 : 170);
                        b = (byte)(diagonal || edge ? 90 : 210);
                    }

                    SDL.WriteSurfacePixel(surface, x, y, r, g, b, 255);
                }
            }

            if (!SDL.SavePNG(surface, path))
            {
                throw new InvalidOperationException($"Couldn't save generated PNG {Path.GetFileName(path)}: {SDL.GetError()}");
            }
        }
        finally
        {
            SDL.DestroySurface(surface);
        }
    }

    private static void Cleanup(RendererExampleContext context)
    {
        for (var i = 0; i < Textures.Length; i++)
        {
            if (Textures[i] != IntPtr.Zero)
            {
                SDL.DestroyTexture(Textures[i]);
                Textures[i] = IntPtr.Zero;
            }
        }

        if (_queue != IntPtr.Zero)
        {
            SDL.DestroyAsyncIOQueue(_queue);
            _queue = IntPtr.Zero;
        }

        _completedRequests = 0;
    }

    private readonly record struct BitmapRequest(string FileName, SDL.FRect Destination, int Width, int Height, int Style);
}
