#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

namespace File_Dialog;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("SDL3 File Dialog", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
        
        SDL.SetRenderVSync(renderer, 1);
        
        var callback = new SDL.DialogFileCallback(DialogFileCallback);

        var openFileFilters = new SDL.DialogFileFilter[]
        {
            new("All files", "*"),
            new("Image files", "jpg;jpeg;png;bmp;gif;webp"),
        };

        var saveFileFilters = new SDL.DialogFileFilter[]
        {
            new("SDL File", ".sdl")
        };
        
        SDL.SetRenderDrawColor(renderer, 100, 149, 237, 0);
        
        var loop = true;
        
        while (loop)
        {
            
            while (SDL.PollEvent(out var e))
            {
                if (e.Type == SDL.EventType.Quit)
                {
                    loop = false;
                }

                if (e.Type == SDL.EventType.KeyDown && e.Key.Key == SDL.Keycode.Alpha1)
                {
                    SDL.ShowOpenFileDialog(callback, IntPtr.Zero, window, openFileFilters, openFileFilters.Length, null, true);
                }
                
                if (e.Type == SDL.EventType.KeyDown && e.Key.Key == SDL.Keycode.Alpha2)
                {
                    SDL.ShowSaveFileDialog(callback, IntPtr.Zero, window, saveFileFilters, saveFileFilters.Length, "test");
                }

                if (e.Type == SDL.EventType.KeyDown && e.Key.Key == SDL.Keycode.Alpha3)
                {
                    SDL.ShowOpenFolderDialog(callback, IntPtr.Zero, window, null, false);
                }
            }

            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
    
    private static void DialogFileCallback(IntPtr userdata, IntPtr filelist, int filter)
    {
        if (filelist == IntPtr.Zero && filter == -1)
        {
            SDL.LogError(SDL.LogCategory.Application, $"SDL Error: {SDL.GetError()}");
        }

        var list = SDL.PointerToStringArray(filelist) ?? [];
        
        var type = filter switch
        {
            0 => "file",
            1 => "image",
            _ => "unknow"
        };

        if (list.Length == 0)
        {
            SDL.LogInfo(SDL.LogCategory.Application, "File not selected");
        }
        
        if (list.Length == 1)
        {
            SDL.LogInfo(SDL.LogCategory.Application, $"Selected filter: {filter}, Selected {type}: {list[0]}");
        }

        if (list.Length > 1)
        {
            SDL.LogInfo(SDL.LogCategory.Application, $"Selected filter: {filter}, Selected {type}s:");
            
            for (var i = 0; i < list.Length; i++)
            {
                Console.WriteLine($"[{i}] {list[i]}");
            }
        }
    }
}