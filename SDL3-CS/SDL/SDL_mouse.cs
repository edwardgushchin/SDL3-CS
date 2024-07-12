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
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
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
    public readonly struct Cursor(IntPtr handle)
    {
        public IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            return obj is Cursor other && Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Cursor left, Cursor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cursor left, Cursor right)
        {
            return !(left == right);
        }
    }
    
    
    public enum SystemCursor
    {
        Default,      /**< Default cursor. Usually an arrow. */
        Text,         /**< Text selection. Usually an I-beam. */
        Wait,         /**< Wait. Usually an hourglass or watch or spinning ball. */
        Crosshair,    /**< Crosshair. */
        Progress,     /**< Program is busy but still interactive. Usually it's WAIT with an arrow. */
        NWSEResize,  /**< Double arrow pointing northwest and southeast. */
        NESWResize,  /**< Double arrow pointing northeast and southwest. */
        EWResize,    /**< Double arrow pointing west and east. */
        NSResize,    /**< Double arrow pointing north and south. */
        Move,         /**< Four pointed arrow pointing north, south, east, and west. */
        NotAllowed,  /**< Not permitted. Usually a slashed circle or crossbones. */
        Pointer,      /**< Pointer that indicates a link. Usually a pointing hand. */
        NWResize,    /**< Window resize top-left. This may be a single arrow or a double arrow like NWSE_RESIZE. */
        NResize,     /**< Window resize top. May be NS_RESIZE. */
        NEResize,    /**< Window resize top-right. May be NESW_RESIZE. */
        EResize,     /**< Window resize right. May be EW_RESIZE. */
        SEResize,    /**< Window resize bottom-right. May be NWSE_RESIZE. */
        SResize,     /**< Window resize bottom. May be NS_RESIZE. */
        SWResize,    /**< Window resize bottom-left. May be NESW_RESIZE. */
        WResize,     /**< Window resize left. May be EW_RESIZE. */
        NumSystemCursors
    }
    
    
    public enum MouseWheelDirection
    {
        Normal,
        Flipped
    }
    
    
    public enum MouseButtonFlags : uint
    {
        Left     = 1,
        Middle   = 2,
        Right    = 3,
        X1       = 4,
        X2       = 5,
    }
    
    
    private static uint Button(uint x)
    {
        return 1u << ((int)x - 1);
    }
    
    public static readonly uint ButtonLMask = Button((uint)MouseButtonFlags.Left);
    public static readonly uint ButtonMMask = Button((uint)MouseButtonFlags.Middle);
    public static readonly uint ButtonRMask = Button((uint)MouseButtonFlags.Right);
    public static readonly uint ButtonX1Mask = Button((uint)MouseButtonFlags.X1);
    public static readonly uint ButtonX2Mask = Button((uint)MouseButtonFlags.X2);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasMouse();
    public static bool HasMouse() => SDL_HasMouse();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMice(out int count);

    public static uint[] GetMice(out int cout)
    {
        var pArray = SDL_GetMice(out cout);
        var miceArray = new int[cout];
        Marshal.Copy(pArray, miceArray, 0, cout);
        Free(pArray);
        return Array.ConvertAll(miceArray, item => (uint)item);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMouseInstanceName(uint instanceId);
    public static string? GetMouseInstanceName(uint instanceId) =>
        Marshal.PtrToStringAnsi(SDL_GetMouseInstanceName(instanceId));

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetMouseFocus();
    public static Window GetMouseFocus() => new(SDL_GetMouseFocus());

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetMouseState(out float x, out float y);
    public static MouseButtonFlags GetMouseState(out float x, out float y) => SDL_GetMouseState(out x, out y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);
    public static MouseButtonFlags GetGlobalMouseState(out float x, out float y) => SDL_GetGlobalMouseState(out x, out y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);
    public static MouseButtonFlags GetRelativeMouseState(out float x, out float y) => SDL_GetRelativeMouseState(out x, out y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_WarpMouseInWindow(IntPtr window, float x, float y);
    public static void WarpMouseInWindow(Window window, float x, float y) => SDL_WarpMouseInWindow(window.Handle, x, y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_WarpMouseGlobal(float x, float y);
    public static int WarpMouseGlobal(float x, float y) => SDL_WarpMouseGlobal(x, y);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetRelativeMouseMode([MarshalAs(UnmanagedType.I1)] bool enabled);
    public static int SetRelativeMouseMode(bool enabled) => SDL_SetRelativeMouseMode(enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CaptureMouse([MarshalAs(UnmanagedType.I1)] bool enabled);
    public static int CaptureMouse(bool enabled) => SDL_CaptureMouse(enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_GetRelativeMouseMode();
    public static bool GetRelativeMouseMode() => SDL_GetRelativeMouseMode();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateCursor(
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] byte[] data,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] byte[] mask,
        int w, int h, int hotX, int hotY);
    public static Cursor CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY) =>
        new(SDL_CreateCursor(data, mask, w, h, hotX, hotY));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateColorCursor(Surface surface, int hotX, int hotY);
    public static Cursor CreateColorCursor(Surface surface, int hotX, int hotY) =>
        new(SDL_CreateColorCursor(surface, hotX, hotY));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_CreateSystemCursor(SystemCursor id);
    public static Cursor CreateSystemCursor(SystemCursor id) => new(SDL_CreateSystemCursor(id));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetCursor(IntPtr cursor);
    public static int SetCursor(Cursor cursor) => SDL_SetCursor(cursor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCursor();
    public static Cursor GetCursor() => new(SDL_GetCursor());
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetDefaultCursor();
    public static Cursor GetDefaultCursor() => new(SDL_GetDefaultCursor());
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyCursor(IntPtr cursor);
    public static void DestroyCursor(Cursor cursor) => SDL_DestroyCursor(cursor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ShowCursor();
    public static int ShowCursor() => SDL_ShowCursor();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_HideCursor();
    public static int HideCursor() => SDL_HideCursor();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CursorVisible();
    public static bool CursorVisible() => SDL_CursorVisible();
}