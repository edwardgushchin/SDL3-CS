using Android.Content.PM;
using Org.Libsdl.App;
using static Android.Content.PM.ConfigChanges;
using SDL = SDL3.SDL;
namespace AndroidCircularColorFade;

[Activity(
    Label = "@string/app_name", 
    MainLauncher = true,
    AlwaysRetainTaskState = true,
    LaunchMode = LaunchMode.SingleInstance,
    Exported = true,
    ConfigurationChanges = 
        LayoutDirection | Locale | GrammaticalGender | FontScale | 
        FontWeightAdjustment | ConfigChanges.Orientation | UiMode |
        ScreenLayout | ScreenSize | SmallestScreenSize |
        Keyboard | KeyboardHidden | Navigation
    )]
public class MainActivity : SDLActivity
{
    protected override string[] GetLibraries() => ["SDL3"];

    protected override void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("SDL3 Circular Color Fade", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
        
        SDL.SetRenderVSync(renderer, 1);

        var loop = true;
        var startCounter = SDL.GetPerformanceCounter();
        var frequency = SDL.GetPerformanceFrequency();

        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
                if (e.Type == (uint)SDL.EventType.Quit || e is {Type: (uint)SDL.EventType.KeyDown, Key.Key: SDL.Keycode.Escape})
                {
                    loop = false;
                }
            }

            // Calculate elapsed time
            var currentCounter = SDL.GetPerformanceCounter();
            var elapsed = (currentCounter - startCounter) / (double)frequency;

            // Calculate color components based on sine wave functions
            var r = (byte)(Math.Sin(elapsed) * 127 + 128);
            var g = (byte)(Math.Sin(elapsed + Math.PI / 2) * 127 + 128);
            var b = (byte)(Math.Sin(elapsed + Math.PI) * 127 + 128);

            SDL.SetRenderDrawColor(renderer, r, g, b, 255);
            
            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }
        
        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        
        SDL.Quit();
    }
}