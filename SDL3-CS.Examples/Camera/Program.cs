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
        if (SDL.Init(SDL.InitFlags.Video | SDL.InitFlags.Camera) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Camera Example", 800, 600, 0);
        
        if (window == null)
        {
            Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var renderer = SDL.CreateRenderer(window, null);
        
        if (renderer == null)
        {
            Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            return;
        }

        
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
        
        var camera = SDL.OpenCamera(devices[0]);
        
        if (camera == null) {
            Console.WriteLine($"Couldn't open camera: {SDL.GetError()}");
            return;
        }
        
        
        //SDL.SetRenderDrawColor(renderer, 100, 149, 237, 0);

        SDL.Texture? texture = null;
        
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

            var frame = SDL.AcquireCameraFrame(camera, out var timestampNS);

            if (frame != null)
            {
                var frameProps = frame!.GetSurfaceFromPtr();
                if (texture == null)
                {
                    SDL.SetWindowSize(window, frameProps!.Value.Width, frameProps.Value.Height); 
                    texture = SDL.CreateTexture(renderer, frameProps.Value.Format, (int)SDL.TextureAccess.Streaming, frameProps.Value.Width, frameProps.Value.Height);
                }
                else
                {
                    SDL.UpdateTexture(texture, null, frameProps?.Pixels, frameProps!.Value.Pitch);
                }
                
                var i = SDL.ReleaseCameraFrame(camera, frame);
            }

            SDL.SetRenderDrawColor(renderer, 0x99, 0x99, 0x99, 255);
            SDL.RenderClear(renderer);
            if (texture != null) 
            {
                var i = SDL.RenderTexture(renderer, texture, null, null);
            }
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
}