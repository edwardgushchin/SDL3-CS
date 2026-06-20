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

namespace Camera_Read_And_Draw;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static IntPtr _camera;
    private static IntPtr _texture;
    private static int _textureWidth;
    private static int _textureHeight;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Camera Read And Draw",
            "com.example.camera-read-and-draw",
            "examples/camera/read-and-draw",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent,
            SDL.InitFlags.Video | SDL.InitFlags.Camera,
            SDL.RendererLogicalPresentation.Letterbox);
    }

    private static void Configure(RendererExampleContext context)
    {
        var devices = SDL.GetCameras(out var deviceCount);
        if (devices is null || deviceCount == 0)
        {
            throw new InvalidOperationException($"Couldn't find any camera devices: {SDL.GetError()}");
        }

        _camera = SDL.OpenCamera(devices[0], IntPtr.Zero);
        if (_camera == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't open camera: {SDL.GetError()}");
        }
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.CameraDeviceApproved:
                SDL.Log("Camera use approved by user.");
                break;

            case SDL.EventType.CameraDeviceDenied:
                throw new InvalidOperationException("Camera use denied by user.");
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        if (_camera != IntPtr.Zero)
        {
            UpdateCameraTexture(context);
        }

        SDL.SetRenderDrawColor(context.Renderer, 0x33, 0x33, 0x33, 0xFF);
        SDL.RenderClear(context.Renderer);

        if (_texture != IntPtr.Zero)
        {
            SDL.RenderTexture(context.Renderer, _texture, IntPtr.Zero, IntPtr.Zero);
        }
        else
        {
            SDL.SetRenderDrawColor(context.Renderer, 255, 255, 255, 255);
            DrawCenteredText(context.Renderer, "Waiting for camera permission...", WindowHeight / 2.0f);
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void UpdateCameraTexture(RendererExampleContext context)
    {
        var frame = SDL.AcquireCameraFrame(_camera, out _);
        if (frame == IntPtr.Zero)
        {
            return;
        }

        try
        {
            var surface = Marshal.PtrToStructure<SDL.Surface>(frame);
            if (_texture == IntPtr.Zero || _textureWidth != surface.Width || _textureHeight != surface.Height)
            {
                if (_texture != IntPtr.Zero)
                {
                    SDL.DestroyTexture(_texture);
                }

                _texture = SDL.CreateTexture(
                    context.Renderer,
                    surface.Format,
                    SDL.TextureAccess.Streaming,
                    surface.Width,
                    surface.Height);

                if (_texture == IntPtr.Zero)
                {
                    throw new InvalidOperationException($"Couldn't create camera texture: {SDL.GetError()}");
                }

                _textureWidth = surface.Width;
                _textureHeight = surface.Height;
                SDL.SetWindowSize(context.Window, surface.Width, surface.Height);
                SDL.SetRenderLogicalPresentation(
                    context.Renderer,
                    surface.Width,
                    surface.Height,
                    SDL.RendererLogicalPresentation.Letterbox);
            }

            if (!SDL.UpdateTexture(_texture, IntPtr.Zero, surface.Pixels, surface.Pitch))
            {
                throw new InvalidOperationException($"Couldn't update camera texture: {SDL.GetError()}");
            }
        }
        finally
        {
            SDL.ReleaseCameraFrame(_camera, frame);
        }
    }

    private static void DrawCenteredText(IntPtr renderer, string text, float y)
    {
        var x = Math.Max(0.0f, (WindowWidth - (text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_texture != IntPtr.Zero)
        {
            SDL.DestroyTexture(_texture);
            _texture = IntPtr.Zero;
            _textureWidth = 0;
            _textureHeight = 0;
        }

        if (_camera != IntPtr.Zero)
        {
            SDL.CloseCamera(_camera);
            _camera = IntPtr.Zero;
        }
    }
}
