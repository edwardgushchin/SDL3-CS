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

using System.Buffers;
using SDL3;

namespace VHS_Camera;

internal static class Program
{
    private static IntPtr _window;
    private static IntPtr _renderer;
    
    [STAThread]
    private static void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video | SDL.InitFlags.Camera))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("SDL3 VHS Camera", 800, 600, 0, out _window, out _renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
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
        
        var camera = SDL.OpenCamera(devices[0], 0);
        
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
                if (e.Type == (uint)SDL.EventType.Quit || e is {Type: (uint)SDL.EventType.KeyDown, Key.Key: SDL.Keycode.Escape})
                {
                    loop = false;
                }
                
                if (e is {Type: (uint)SDL.EventType.KeyDown, Key.Key: SDL.Keycode.V})
                {
                    vhs = !vhs;
                    SDL.LogInfo(SDL.LogCategory.Render, $"VHS effect: {vhs}");
                }

                if (e.Type == (uint)SDL.EventType.CameraDeviceApproved)
                {
                    SDL.LogInfo(SDL.LogCategory.Application, "Camera use approved by user!");
                }

                if (e.Type == (uint)SDL.EventType.CameraDeviceDenied)
                {
                    SDL.LogInfo(SDL.LogCategory.Application, "Camera use denied by user!");
                }
            }

            var framePtr = SDL.AcquireCameraFrame(camera, out _);

            if (framePtr != IntPtr.Zero)
            {
                var frame = SDL.PointerToStructure<SDL.Surface>(framePtr) ?? default;
                
                if (texture == IntPtr.Zero)
                {
                    SDL.SetWindowSize(_window, frame.Width, frame.Height);
                    texture = SDL.CreateTexture(_renderer, frame.Format, SDL.TextureAccess.Streaming, frame.Width, frame.Height);
                }
                
                if (vhs)
                {
                    byte[]? pixels = null;
                    
                    if (frame.Format == SDL.PixelFormat.YUY2)
                    {
                        pixels = SDL.PointerToStructureArray<byte>(frame.Pixels, frame.Pitch * frame.Height)!;
                    
                        ApplyVHSEffectYUY22(pixels, frame.Width, frame.Height);
                    }
                    else if (frame.Format == SDL.PixelFormat.NV12)
                    {
                        var bufferSize = (frame.Pitch * frame.Height) + (frame.Pitch * (frame.Height / 2));
                        
                        pixels = SDL.PointerToStructureArray<byte>(frame.Pixels, bufferSize)!;
                    
                        ApplyVHSEffectNV12(pixels, frame.Width, frame.Height, frame.Pitch);
                    }
                    else
                    {
                        Console.WriteLine($"Your camera's format ({frame.Format}) is not supported for VHS mode.");
                        vhs = false;
                    }
                    
                    if(pixels != null) SDL.UpdateTexture(texture, IntPtr.Zero, pixels, frame.Pitch);
                    else SDL.UpdateTexture(texture, IntPtr.Zero, frame.Pixels, frame.Pitch);
                }
                else
                {
                    SDL.UpdateTexture(texture, IntPtr.Zero, frame.Pixels, frame.Pitch);
                }
                
                SDL.ReleaseCameraFrame(camera, framePtr);
            }
            
            fpsCounter.Update();
            
            SDL.RenderClear(_renderer);
            
            SDL.RenderTexture(_renderer, texture, IntPtr.Zero, IntPtr.Zero);
            
            SDL.SetRenderDrawColor(_renderer, 0x00, 0xFF, 0x00, 255);
            SDL.RenderDebugText(_renderer, 10, 10, $"FPS: {fpsCounter.FPS:N0}");
            
            SDL.RenderPresent(_renderer);
        }

        SDL.CloseCamera(camera);
        SDL.DestroyTexture(texture!);
        SDL.DestroyRenderer(_renderer);
        SDL.DestroyWindow(_window);
        SDL.Quit();
    }

    private static void ApplyVHSEffectYUY22(byte[] buffer, int width, int height)
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
            var rowOffset = rng!.Next(-1, 1) * 4;

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
                buffer[index] = (byte)Math.Clamp(blurredY + localRng!.Next(-10, 10), 0, 255); // Y0
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

    private static unsafe void ApplyVHSEffectNV12(byte[] buffer, int width, int height, int pitch)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (width <= 0 || height <= 0) throw new ArgumentException("Invalid width/height");
        if (pitch < width) throw new ArgumentException("Pitch must be >= width");

        var ySize = pitch * height;
        var uvSize = pitch * (height / 2);
        if (buffer.Length < ySize + uvSize)
            throw new ArgumentException("Buffer too small for NV12 (using provided pitch/height)");

        // Tunable params (меняй, если хочешь сильнее/слабее эффект)
        const int blurRadius = 3; // радиус blur (раньше был 5) — 3 даёт хорошее сглаживание при меньшей цене
        const int noiseRangeY = 10; // ± для Y шум
        const int noiseRangeUV = 10; // ± для UV шум
        const double scanlineFactor = 0.8; // затемнение каждой второй строки

        var threadRng = new ThreadLocal<Random>(() =>
            new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));

        // Rented buffers (we'll rent per-row inside loop to avoid huge allocation)

        fixed (byte* basePtr = buffer)
        {
            var yPlane = basePtr; // Y starts at 0
            var uvPlane = basePtr + ySize; // UV starts after Y plane

            var mainRng = threadRng.Value; // only for frame-level decisions

            // Precompute window width for box blur

            // Parallel over rows — keep per-row temp buffers from ArrayPool inside each iteration
            Parallel.For(0, height, y =>
            {
                var rng = threadRng.Value!;

                // Rent working buffers per thread/iteration (small overhead, pooled)
                var rowTemp = ArrayPool<byte>.Shared.Rent(width); // blurred Y -> final row values before writing
                var noise = ArrayPool<byte>.Shared.Rent(width); // random bytes to map to noise offsets
                // UV noise length = pitch (we will use first width bytes, step 2)
                var uvNoise = ArrayPool<byte>.Shared.Rent(pitch);

                try
                {
                    var yRowPtr = yPlane + (long)y * pitch;

                    // --- Horizontal box blur using running sum (O(width)) ---
                    // Compute initial sum for x=0
                    var sum = 0;
                    var left = 0;
                    var right = Math.Min(blurRadius, width - 1);

                    for (var k = left; k <= right; k++)
                    {
                        sum += yRowPtr[k];
                    }

                    for (var x = 0; x < width; x++)
                    {
                        // expand right edge if needed
                        var nextRight = x + blurRadius;
                        if (nextRight <= width - 1 && nextRight > right)
                        {
                            // add newly included sample(s)
                            for (var t = right + 1; t <= nextRight; t++)
                                sum += yRowPtr[t];
                            right = nextRight;
                        }

                        // shrink left edge if needed
                        var nextLeft = x - blurRadius - 1;
                        if (nextLeft >= 0)
                        {
                            sum -= yRowPtr[nextLeft];
                        }

                        var actualWindow = Math.Min(width - 1, x + blurRadius) - Math.Max(0, x - blurRadius) + 1;
                        var blurred = (byte)(sum / actualWindow);
                        rowTemp[x] = blurred;
                    }

                    // --- Generate noise bytes for this row (map to +/- noiseRangeY) ---
                    rng.NextBytes(noise);
                    // Map noise[] (0..255) -> [-noiseRangeY..noiseRangeY]
                    for (var i = 0; i < width; i++)
                    {
                        // Using modulo is cheap; distribution not perfect but fine for visual noise
                        var off = (noise[i] % (noiseRangeY * 2 + 1)) - noiseRangeY;
                        var val = rowTemp[i] + off;

                        // Scanline dimming (every second Y row)
                        if ((y % 2) == 0)
                        {
                            val = (int)(val * scanlineFactor);
                        }

                        val = val switch
                        {
                            // Clamp and write back to temp
                            < 0 => 0,
                            > 255 => 255,
                            _ => val
                        };
                        rowTemp[i] = (byte)val;
                    }

                    // --- Row shift / glitch: perform copy into target region safely using a second temp buffer ---
                    var rowOffset = rng.Next(-2, 3); // small shift in pixels (-2..2)
                    if (rowOffset != 0)
                    {
                        // Create shifted row buffer and then copy valid bytes into actual yRowPtr
                        var shifted = ArrayPool<byte>.Shared.Rent(width);
                        try
                        {
                            // fill with original values to avoid garbage
                            // Note: we want glitchy overwrite; here we do safe bounded copy
                            for (var i = 0; i < width; i++) shifted[i] = yRowPtr[i];

                            var srcStart = 0;
                            var dstStart = rowOffset;
                            if (dstStart < 0)
                            {
                                srcStart = -dstStart;
                                dstStart = 0;
                            }

                            var len = Math.Min(width - srcStart, width - dstStart);
                            if (len > 0)
                            {
                                // copy from rowTemp[srcStart .. srcStart+len) to shifted[dstStart .. dstStart+len)
                                Buffer.BlockCopy(rowTemp, srcStart, shifted, dstStart, len);
                            }

                            // finally write shifted into yRowPtr (only width bytes)
                            for (var i = 0; i < width; i++)
                            {
                                yRowPtr[i] = shifted[i];
                            }
                        }
                        finally
                        {
                            ArrayPool<byte>.Shared.Return(shifted);
                        }
                    }
                    else
                    {
                        // no shift: write rowTemp directly back
                        for (var i = 0; i < width; i++) yRowPtr[i] = rowTemp[i];
                    }

                    // --- UV processing: only once per UV row (y even -> process the UV row shared by y and y+1) ---
                    if ((y & 1) != 0) return;
                    {
                        var uvRow = y / 2;
                        var uvRowPtr = uvPlane + (long)uvRow * pitch;

                        // Generate uvNoise
                        rng.NextBytes(uvNoise);

                        // Step through pairs (U,V) for the valid width range (each pair represents 2 pixels horizontally)
                        var uvPairs = (width + 1) / 2; // number of UV pairs covering the width
                        var uvWriteLimit = uvPairs * 2; // number of bytes to touch (<= pitch)

                        for (var i = 0; i < uvWriteLimit; i += 2)
                        {
                            var mapIdx = i; // using uvNoise[i] to derive offset
                            var offU = (uvNoise[mapIdx] % (noiseRangeUV * 2 + 1)) - noiseRangeUV;
                            // use next noise byte for V (or same distribution)
                            var offV = (uvNoise[mapIdx + 1] % (noiseRangeUV * 2 + 1)) - noiseRangeUV;

                            var u = uvRowPtr[i] + offU;
                            var v = uvRowPtr[i + 1] + offV;
                            
                            u = u switch
                            {
                                < 0 => 0,
                                > 255 => 255,
                                _ => u
                            };
                            
                            v = v switch
                            {
                                < 0 => 0,
                                > 255 => 255,
                                _ => v
                            };
                            
                            uvRowPtr[i] = (byte)u;
                            uvRowPtr[i + 1] = (byte)v;
                        }
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(rowTemp);
                    ArrayPool<byte>.Shared.Return(noise);
                    ArrayPool<byte>.Shared.Return(uvNoise);
                }
            }); // end Parallel.For

            // --- Frame-level glitches (random bands) ---
            if (mainRng == null || !(mainRng.NextDouble() < 0.07)) return; // slightly lower probability than original to keep quality
            {
                var glitchHeight = Math.Max(1, mainRng.Next(1, Math.Max(2, height / 12)));
                var startY = mainRng.Next(0, Math.Max(1, height - glitchHeight + 1));
                Parallel.For(startY, startY + glitchHeight, y =>
                {
                    var rng = threadRng.Value!;
                    var yRowPtr = yPlane + (long)y * pitch;
                    // randomize Y across width quickly using NextBytes
                    var rand = ArrayPool<byte>.Shared.Rent(width);
                    try
                    {
                        rng.NextBytes(rand);
                        for (var x = 0; x < width; x++)
                        {
                            yRowPtr[x] = rand[x];
                        }
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(rand);
                    }

                    // randomize UV row corresponding
                    if ((y & 1) != 0) return;
                    var uvRow = y / 2;
                    var uvRowPtr = uvPlane + (long)uvRow * pitch;
                    var randUv = ArrayPool<byte>.Shared.Rent(pitch);
                    try
                    {
                        rng.NextBytes(randUv);
                        // write first uvPairs*2 bytes
                        var uvPairs = (width + 1) / 2;
                        var uvBytes = uvPairs * 2;
                        for (var i = 0; i < uvBytes; i++)
                            uvRowPtr[i] = randUv[i];
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(randUv);
                    }
                });
            }
        }
    }
}