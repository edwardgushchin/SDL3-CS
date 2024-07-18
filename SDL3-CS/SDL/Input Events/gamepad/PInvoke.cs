using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMapping([MarshalAs(UnmanagedType.LPUTF8Str)] string mapping);
    public static int AddGamepadMapping(string mapping) => SDL_AddGamepadMapping(mapping);
    

    public static int AddGamepadMappingsFromIO(Stream src, bool closeio = true)
    {
        var reader = new StreamReader(src);

        while (!reader.EndOfStream)
        {
            var mapping = reader.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(mapping))
            {
                return AddGamepadMapping(mapping);
            }
        }

        if (closeio) reader.Close();
        return -1;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddGamepadMappingsFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    public static int AddGamepadMappingsFromFile(string file) => SDL_AddGamepadMappingsFromFile(file);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ReloadGamepadMappings();
    public static int ReloadGamepadMappings() => SDL_ReloadGamepadMappings();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetGamepadMappings(out int count);
    public static string?[] GetGamepadMappings(out int count)
    {
        var arrayPtr = SDL_GetGamepadMappings(out count);
        
        if (arrayPtr == IntPtr.Zero || count == 0)
        {
            return [];
        }

        var mappings = new string?[count];
        var ptrArray = new IntPtr[count];
        Marshal.Copy(arrayPtr, ptrArray, 0, count);
        Free(arrayPtr);

        for (var i = 0; i < count; i++)
        {
            mappings[i] = Marshal.PtrToStringAnsi(ptrArray[i]);
        }

        return mappings;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetGamepadMappingForGUID(GUID guid);
    public static string? GetGamepadMappingForGUID(GUID guid) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadMappingForGUID(guid));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetGamepadMapping(IntPtr gamepad);
    public static string? GetGamepadMapping(Gamepad gamepad) => 
        Marshal.PtrToStringUTF8(SDL_GetGamepadMapping(gamepad.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_SetGamepadMapping(uint instance_id, 
        [MarshalAs(UnmanagedType.LPUTF8Str)]string mapping);
    public static int SetGamepadMapping(uint instance_id, string mapping) =>
        SDL_SetGamepadMapping(instance_id, mapping);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenGamepad(uint instance_id);
    public static Gamepad OpenGamepad(uint instance_id) => new(SDL_OpenGamepad(instance_id));
}