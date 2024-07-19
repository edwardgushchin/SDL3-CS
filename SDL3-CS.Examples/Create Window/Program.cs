using SDL3;

namespace Create_Window;

internal static class Program
{
    private static void Main()
    {
        if (SDL.Init(SDL.InitFlags.Video) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var window = SDL.CreateWindow("SDL3 Create Window", 800, 600, 0);
        
        if (window.Handle == IntPtr.Zero)
        {
            Console.WriteLine($"Window could not be created! SDL Error: {SDL.GetError()}");
            return;
        }
        
        var renderer = SDL.CreateRenderer(window, null);
        
        if (renderer.Handle == IntPtr.Zero)
        {
            Console.WriteLine($"Renderer could not be created! SDL Error: {SDL.GetError()}");
            return;
        }

        SDL.SetRenderDrawColor(renderer, 100,149,237,0);
        
        var loop = true;
        
        while (loop)
        {
            
            while (SDL.PollEvent(out var sdlEvent))
            {
                if (sdlEvent.Type == SDL.EventType.Quit)
                {
                    loop = false;
                }
            }

            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
}