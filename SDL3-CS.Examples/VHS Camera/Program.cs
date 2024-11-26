﻿#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

namespace VHS_Camera;

internal static class Program
{
    private static IntPtr _window;
    private static IntPtr _renderer;
    
    [STAThread]
    private static void Main()
    {
        SDL.SetLogPriorities(SDL.LogPriority.Trace);
        
        if (!SDL.Init(SDL.InitFlags.Camera))
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        _window = SDL.CreateWindow("SDL3 VHS Camera Example", 800, 600, 0);
        
        if (_window == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        _renderer = SDL.CreateRenderer(_window, null);
        
        if (_renderer == IntPtr.Zero)
        {
            Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        SDL.SetRenderVSync(_renderer, 1);
        
        var devices = SDL.GetCameras(out var camerasCount);

        if (devices == null)
        {
            Console.WriteLine($"Couldn't enumerate camera devices: {SDL.GetError()}");
            return;
        }
        if (camerasCount == 0)
        {
            Console.WriteLine("Couldn't find any camera devices! Please connect a camera and try again.");
            return;
        }
        
        var camera = SDL.OpenCamera(devices[0], default);
        
        if (camera == IntPtr.Zero) {
            Console.WriteLine($"Couldn't open camera: {SDL.GetError()}");
            return;
        }

        var texture = IntPtr.Zero;
        
        var loop = true;

        var vhs = false;

        var fpsCounter = new FPSCounter();
        
        while (loop)
        {
            
            while (SDL.PollEvent(out var e))
            {
                if (e.Type == SDL.EventType.Quit || e is {Type: SDL.EventType.KeyDown, Key.Key: SDL.Keycode.Escape})
                {
                    loop = false;
                }
                
                if (e is {Type: SDL.EventType.KeyDown, Key.Key: SDL.Keycode.V})
                {
                    vhs = !vhs;
                    SDL.LogInfo(SDL.LogCategory.Render, $"VHS effect: {vhs}");
                }

                if (e.Type == SDL.EventType.CameraDeviceApproved)
                {
                    SDL.Log("Camera use approved by user!");
                }

                if (e.Type == SDL.EventType.CameraDeviceDenied)
                {
                    SDL.Log("Camera use denied by user!");
                }
            }

            var framePtr = SDL.AcquireCameraFrame(camera, out _);

            if (framePtr != IntPtr.Zero)
            {
                var frame = SDL.PointerToManagedStruct<SDL.Surface>(framePtr) ?? default;
                
                if (vhs)
                {
                    /*
                     * IMPORTANT INFORMATION!
                     *
                     * This code is written as an example. Do not use it in real software.
                     * The point is that here visual effects are applied on the byte array,
                     * which represents the current frame. The calculations are performed on the CPU side.
                     * In a normal situation, such visual effects should be implemented on the
                     * shader side and executed on the GPU side.
                     *
                     * Consider this code as a demonstration of the capabilities, and not a working version.
                     */
                    
                    var frameBuffer = frame.GetManagedPixels();
                    
                    ApplyVHSEffectYUY22(frameBuffer!, frame.Width, frame.Height);
    
                    var bufferHandle = GCHandle.Alloc(frameBuffer, GCHandleType.Pinned);
                    try
                    {
                        var unmanagedFrameBuffer = bufferHandle.AddrOfPinnedObject();
                        UpdateTexture(ref frame, ref texture, ref unmanagedFrameBuffer);
                    }
                    finally
                    {
                        bufferHandle.Free();
                    }
                }
                else
                {
                    UpdateTexture(ref frame, ref texture, ref frame.Pixels);
                }

                SDL.ReleaseCameraFrame(camera, framePtr);
            }

            SDL.SetRenderDrawColor(_renderer, 0x99, 0x99, 0x99, 255);
            SDL.RenderClear(_renderer);
            
            if (texture != IntPtr.Zero) 
            {
                SDL.RenderTexture(_renderer, texture, IntPtr.Zero, IntPtr.Zero);
            }
            
            SDL.RenderPresent(_renderer);
            
            fpsCounter.Update();
        }

        SDL.CloseCamera(camera);
        SDL.DestroyTexture(texture!);
        SDL.DestroyRenderer(_renderer);
        SDL.DestroyWindow(_window);
        SDL.Quit();
    }
    
    private static void UpdateTexture(ref SDL.Surface frame, ref IntPtr texture, ref IntPtr framePixels)
    {
        if (texture == IntPtr.Zero)
        {
            SDL.SetWindowSize(_window, frame.Width, frame.Height);
            texture = SDL.CreateTexture(_renderer, frame.Format, SDL.TextureAccess.Streaming, frame.Width, frame.Height);
        }
        else
        {
            SDL.UpdateTexture(texture, IntPtr.Zero, framePixels, frame.Pitch);
        }
    }
    
    public static void ApplyVHSEffectYUY22(byte[] buffer, int width, int height)
    {
        var rowStride = width * 2; // Number of bytes in one row (YUY2: 2 bytes per pixel)

        // Local random number generator for each thread
        var threadRng = new ThreadLocal<Random>(() => new Random());
        
        var rng = threadRng.Value;

        // Parallel processing of rows
        Parallel.For(0, height, y =>
        {
            var localRng = threadRng.Value; // Local generator for each thread

            // Row shift
            var rowOffset = localRng!.Next(-1, 1) * 4;

            for (var x = 0; x < width * 2; x += 4)
            {
                var index = y * rowStride + x;
                if (index + 3 >= buffer.Length) continue; // Boundary check

                // Apply blur to the Y component (average over neighboring pixels)
                int sumY = 0, count = 0;
                for (var dx = -5; dx <= 5; dx++)
                {
                    var nx = Math.Clamp(x / 2 + dx, 0, width - 1);
                    var sampleIndex = (y * width + nx) * 2;
                    sumY += buffer[sampleIndex];
                    count++;
                }
                var blurredY = (byte)(sumY / count);

                // Apply noise and distortion
                buffer[index] = (byte)Math.Clamp(blurredY + localRng.Next(-10, 10), 0, 255); // Y0
                buffer[index + 2] = (byte)Math.Clamp(buffer[index + 2] + localRng.Next(-10, 10), 0, 255); // Y1
                buffer[index + 1] = (byte)Math.Clamp(buffer[index + 1] + localRng.Next(-10, 10), 0, 255); // Cb
                buffer[index + 3] = (byte)Math.Clamp(buffer[index + 3] + localRng.Next(-10, 10), 0, 255); // Cr

                // Apply row shift
                if (index + rowOffset + 3 < buffer.Length && index + rowOffset >= 0)
                {
                    buffer[index + rowOffset] = buffer[index];
                    buffer[index + rowOffset + 1] = buffer[index + 1];
                    buffer[index + rowOffset + 2] = buffer[index + 2];
                    buffer[index + rowOffset + 3] = buffer[index + 3];
                }

                // Apply scanning lines (every second row)
                if (y % 2 == 0)
                {
                    buffer[index] = (byte)(buffer[index] * 0.8); // Затемнение Y
                }
            }
        });

        // Apply frame glitches
        if (!(rng!.NextDouble() < 0.1)) return;

        var glitchHeight = rng.Next(1, height / 10);
        var startY = rng.Next(0, height - glitchHeight);

        // Parallel processing of frame glitches
        Parallel.For(startY, startY + glitchHeight, y =>
        {
            var localRng = threadRng.Value!; // Local generator for each thread
            for (var x = 0; x < width; x++)
            {
                var index = (y * width + x) * 2;
                buffer[index] = (byte)localRng.Next(0, 256); // Y0
                buffer[index + 2] = (byte)localRng.Next(0, 256); // Y1
            }
        });
    }
}