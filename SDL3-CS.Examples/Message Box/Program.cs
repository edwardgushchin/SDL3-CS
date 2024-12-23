using System.Runtime.InteropServices;
using SDL3;

namespace Message_Box;

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

        if (!SDL.CreateWindowAndRenderer("SDL3 Create Window", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }

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

                if (e is {Type: SDL.EventType.KeyDown, Key.Key: SDL.Keycode.S})
                {
                    SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Information, "Information!", "ShowSimpleMessageBox called!", window);
                }
                
                if (e is {Type: SDL.EventType.KeyDown, Key.Key: SDL.Keycode.E})
                {

                    var buttons = new SDL.MessageBoxButtonData[]
                    {
                        new() {ButtonID = 0, Flags = SDL.MessageBoxButtonFlags.EscapekeyDefault, Text = "Escape"},
                        new() {ButtonID = 1, Flags = SDL.MessageBoxButtonFlags.ReturnkeyDefault, Text = "Return"},
                        new() {ButtonID = 2, Flags = SDL.MessageBoxButtonFlags.ReturnkeyDefault, Text = "Retry"}
                    };

                    var buttonsPtr = SDL.StructArrayToPointer(buttons);

                    try
                    {
                        var messageBoxData = new SDL.MessageBoxData
                        {
                            Buttons = buttonsPtr, 
                            NumButtons = buttons.Length,
                            Flags = SDL.MessageBoxFlags.Error, 
                            Title = "Critical system failure!", 
                            Message = "A very terrible and dangerous mistake! It's the end of everything!"
                        };
                    
                        SDL.ShowMessageBox(messageBoxData, out var resultButton);
                        
                        SDL.LogInfo(SDL.LogCategory.Application, $"MessageBox result button ID: {resultButton}");
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(buttonsPtr);
                    }
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