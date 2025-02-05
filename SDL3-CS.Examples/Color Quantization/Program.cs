using System.Runtime.InteropServices;
using SDL3;

namespace Color_Quantization;

internal class Program
{
    private static IntPtr _window;
    private static IntPtr _renderer;
    
    [STAThread]
    private static void Main()
    {
        var pallete = new List<(byte r, byte g, byte b)>()
        {
            new ValueTuple<byte, byte, byte>(74, 36, 75),
            new ValueTuple<byte, byte, byte>(214, 49, 82),
            new ValueTuple<byte, byte, byte>(239, 142, 93),
            new ValueTuple<byte, byte, byte>(255, 208, 138),
            new ValueTuple<byte, byte, byte>(85, 135, 128),
            new ValueTuple<byte, byte, byte>(87, 76, 117),
        };
        
        
        if (!SDL.Init(SDL.InitFlags.Video | SDL.InitFlags.Camera))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("SDL3 Color Quantization", 640, 640, SDL.WindowFlags.Resizable, out _window, out _renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
        
        SDL.SetRenderVSync(_renderer, 1);
        
        var texture = IntPtr.Zero;
        
        var loop = true;
        
        var newfileName = string.Empty;
        var oldFilename = string.Empty;
        
        Console.WriteLine("Drop image to window");

        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
                if (e.Type == (uint)SDL.EventType.Quit || e is {Type: (uint)SDL.EventType.KeyDown, Key.Key: SDL.Keycode.Escape})
                {
                    loop = false;
                }

                if (e.Type == (uint)SDL.EventType.DropFile)
                {
                    newfileName = Marshal.PtrToStringUTF8(e.Drop.Data);
                    Console.WriteLine($"Dropped file: {newfileName}");
                }
            }

            if (newfileName != string.Empty && (newfileName!.ToLower().EndsWith(".jpg") || newfileName!.ToLower().EndsWith(".png")) && newfileName != oldFilename)
            {
                var surface = Image.Load(newfileName);
                
                var frame = SDL.PointerToStructure<SDL.Surface>(surface) ?? default;

                if (frame.Format == SDL.PixelFormat.RGB24)
                {
                    if(texture != IntPtr.Zero) SDL.DestroyTexture(texture);
                    
                    texture = SDL.CreateTexture(_renderer, frame.Format, SDL.TextureAccess.Static, frame.Width, frame.Height);
                
                    var pixels = SDL.PointerToStructureArray<byte>(frame.Pixels, frame.Width * frame.Height * 3)!;
                    
                    QuantizeToPaletteRGB24(pixels, frame.Width, frame.Height, pallete);
                
                    SDL.UpdateTexture(texture, IntPtr.Zero, pixels, frame.Pitch);
                    
                    var s = GetScreenSize(frame.Width, frame.Height);
                    
                    SDL.SetWindowSize(_window, s.width, s.height);
                }
                else
                {
                    Console.WriteLine("Image format not supported!");
                }
                
                oldFilename = newfileName;
                
                SDL.DestroySurface(surface);
            }
            
            SDL.RenderClear(_renderer);
            if(texture != IntPtr.Zero) SDL.SetRenderDrawColor(_renderer, 0, 0, 0, 255);
            SDL.RenderTexture(_renderer, texture, IntPtr.Zero, IntPtr.Zero);
            SDL.RenderPresent(_renderer);
        }
        
        SDL.DestroyTexture(texture!);
        SDL.DestroyRenderer(_renderer);
        SDL.DestroyWindow(_window);
        SDL.Quit();
    }

    private static (int width, int height) GetScreenSize(int w, int h)
    {
        const int maxWidth = 1360;
        const int maxHeight = 768;
        
        var scaleX = (float)maxWidth / w;
        var scaleY = (float)maxHeight / h;
        
        var scale = Math.Min(scaleX, scaleY);
        
        return ((int)(w * scale), (int)(h * scale));
    }
    
    private static void QuantizeToPaletteRGB24(byte[] buffer, int width, int height, List<(byte r, byte g, byte b)> palette)
    {
        Parallel.For(0, height, y =>
        {
            for (var x = 0; x < width; x++)  // Проходим каждый пиксель
            {
                // Индексы байтов в буфере для каждого пикселя
                var index = (y * width + x) * 3;
            
                // Извлекаем значения R, G, B из буфера
                var r = buffer[index];
                var g = buffer[index + 1];
                var b = buffer[index + 2];
                
                // Находим ближайший цвет из палитры
                var closestColor = FindClosestColor(r, g, b, palette);

                // Сохраняем квантизированные цвета в буфер
                buffer[index] = closestColor.r;
                buffer[index + 1] = closestColor.g;
                buffer[index + 2] = closestColor.b;
                
            }
        });
    }
    
    
    // Функция для нахождения ближайшего цвета в палитре
    private static (byte r, byte g, byte b) FindClosestColor(byte r, byte g, byte b, List<(byte r, byte g, byte b)> pallet)
    {
        var minDistance = int.MaxValue;
        var closestColor = pallet[0];

        foreach (var color in pallet)
        {
            // Евклидово расстояние (квадрат)
            var dr = r - color.r;
            var dg = g - color.g;
            var db = b - color.b;
            var distance = dr * dr + dg * dg + db * db;

            if (distance >= minDistance) continue;
            
            minDistance = distance;
            closestColor = color;
        }

        return closestColor;
    }
}