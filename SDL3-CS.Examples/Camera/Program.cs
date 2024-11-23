#region License
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

using SDL3;

namespace Camera;

internal static class Program
{
    private static void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Camera))
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Camera Example", 800, 600, 0);
        
        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var renderer = SDL.CreateRenderer(window, null!);
        
        if (renderer == IntPtr.Zero)
        {
            Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        SDL.SetRenderVSync(renderer, 1);
        
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
        
        while (loop)
        {
            
            while (SDL.PollEvent(out var sdlEvent))
            {
                if (sdlEvent.Type == SDL.EventType.Quit)
                {
                    loop = false;
                }

                if (sdlEvent.Type == SDL.EventType.CameraDeviceApproved)
                {
                    SDL.Log("Camera use approved by user!");
                }

                if (sdlEvent.Type == SDL.EventType.CameraDeviceDenied)
                {
                    SDL.Log("Camera use denied by user!");
                }
            }

            var framePtr = SDL.AcquireCameraFrame(camera, out _);

            if (framePtr != IntPtr.Zero)
            {
                var frame = SDL.PointerToManagedStruct<SDL.Surface>(framePtr) ?? default;
                
                if (texture == IntPtr.Zero)
                {
                    SDL.SetWindowSize(window, frame.Width, frame.Height); 
                    texture = SDL.CreateTexture(renderer, frame.Format, SDL.TextureAccess.Streaming, frame.Width, frame.Height);
                }
                else
                {
                    SDL.UpdateTexture(texture, IntPtr.Zero, frame.Pixels, frame.Pitch);
                }
                SDL.ReleaseCameraFrame(camera, framePtr);
            }

            SDL.SetRenderDrawColor(renderer, 0x99, 0x99, 0x99, 255);
            SDL.RenderClear(renderer);
            if (texture != IntPtr.Zero) 
            {
                SDL.RenderTexture(renderer, texture, IntPtr.Zero, IntPtr.Zero);
            }
            SDL.RenderPresent(renderer);
        }

        SDL.CloseCamera(camera);
        SDL.DestroyTexture(texture!);
        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        SDL.Quit();
    }
}