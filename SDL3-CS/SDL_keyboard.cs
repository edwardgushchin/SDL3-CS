#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasKeyboard();
    public static bool HasKeyboard() => SDL_HasKeyboard();
    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboards(out int count);

    public static uint[] GetKeyboards(out int count)
    {
        var pArray = SDL_GetMice(out count);
        var keyboardArray = new int[count];
        Marshal.Copy(pArray, keyboardArray, 0, count);
        Free(pArray);
        return Array.ConvertAll(keyboardArray, item => (uint)item);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardInstanceName(int instanceId);
    public static string? GetKeyboardInstanceName(int instanceId) =>
        UTF8ToManaged(SDL_GetKeyboardInstanceName(instanceId));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window SDL_GetKeyboardFocus();
    public static Window GetKeyboardFocus() => SDL_GetKeyboardFocus();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyboardState(out int numkeys);
    public static byte[] GetKeyboardState(out int numkeys)
    {
        var pArray = SDL_GetKeyboardState(out numkeys);
        var keyboardState = new byte[numkeys];
        Marshal.Copy(pArray, keyboardState, 0, numkeys);
        return keyboardState;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetKeyboard();
    public static void ResetKeyboard() => SDL_ResetKeyboard();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keymod SDL_GetModState();
    public static Keymod GetModState() => SDL_GetModState();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(Keymod modstate);
    public static void SetModState(Keymod modstate) => SDL_SetModState(modstate);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetDefaultKeyFromScancode(Scancode scancode, Keymod modstate);
    public static Keycode GetDefaultKeyFromScancode(Scancode scancode, Keymod modstate) => 
        SDL_GetDefaultKeyFromScancode(scancode, modstate);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetKeyFromScancode(Scancode scancode, Keymod modstate);
    public static Keycode GetKeyFromScancode(Scancode scancode, Keymod modstate) => 
        SDL_GetKeyFromScancode(scancode, modstate);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetDefaultScancodeFromKey(Keycode key, out Keymod modstate);
    public static Scancode GetDefaultScancodeFromKey(Keycode key, out Keymod modstate) => 
        SDL_GetDefaultScancodeFromKey(key, out modstate);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromKey(Keycode key, out Keymod modstate);
    public static Scancode GetScancodeFromKey(Keycode key, out Keymod modstate) => 
        SDL_GetScancodeFromKey(key, out modstate);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_SetScancodeName(Scancode scancode, byte* name);
    public static unsafe int SetScancodeName(Scancode scancode, string name)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_SetScancodeName(scancode, UTF8Encode(name, utf8Name, utf8NameBufSize));
    }
    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetScancodeName(Scancode scancode);
    public static string? GetScancodeName(Scancode scancode) => UTF8ToManaged(SDL_GetScancodeName(scancode));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Scancode SDL_GetScancodeFromName(byte* name);
    public static unsafe Scancode GetScancodeFromName(string name)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_GetScancodeFromName(UTF8Encode(name, utf8Name, utf8NameBufSize));
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetKeyName(Keycode key);
    public static string? GetKeyName(Keycode key) => UTF8ToManaged(SDL_GetKeyName(key));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Keycode SDL_GetKeyFromName(byte* name);
    public static unsafe Keycode GetKeyFromName(string name)
    {
        var utf8NameBufSize = UTF8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_GetKeyFromName(UTF8Encode(name, utf8Name, utf8NameBufSize));
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_StartTextInput(Window window);
    public static int StartTextInput(Window window) => SDL_StartTextInput(window);

    

    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_TextInputActive(Window window);
    public static bool TextInputActive(Window window) => SDL_TextInputActive(window);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_StopTextInput(Window window);
    public static int StopTextInput(Window window) => SDL_StopTextInput(window);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ClearComposition(Window window);
    public static int ClearComposition(Window window) => SDL_ClearComposition(window);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetTextInputArea(Window window, ref Rect rect, int cursor);
    public static int SetTextInputArea(Window window, ref Rect rect, int cursor) => 
        SDL_SetTextInputArea(window, ref rect, cursor);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetTextInputArea(Window window, out Rect rect, out int cursor);
    public static int GetTextInputArea(Window window, out Rect rect, out int cursor) => 
        SDL_GetTextInputArea(window, out rect, out cursor);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasScreenKeyboardSupport();
    public static bool HasScreenKeyboardSupport() => SDL_HasScreenKeyboardSupport();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ScreenKeyboardShown(Window window);
    public static bool ScreenKeyboardShown(Window window) => SDL_ScreenKeyboardShown(window);
}
