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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PumpEvents();
    public static void PumpEvents() => SDL_PumpEvents();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PeepEvents(IntPtr events, int numevents, EventAction action, uint minType,
        uint maxType);
    public static int PeepEvents(Event[] events, int numevents, EventAction action, uint minType, uint maxType)
    {
        var eventsPtr = events != null ? Marshal.UnsafeAddrOfPinnedArrayElement(events, 0) : IntPtr.Zero;
        return SDL_PeepEvents(eventsPtr, numevents, action, minType, maxType);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasEvent(uint type);
    public static bool HasEvent(uint type) => SDL_HasEvent(type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasEvents(uint minType, uint maxType);
    public static bool HasEvents(uint minType, uint maxType) => SDL_HasEvents(minType, maxType);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvent(uint type);
    public static void FlushEvent(uint type) => SDL_FlushEvent(type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvents(uint minType, uint maxType);
    public static void FlushEvents(uint minType, uint maxType) => SDL_FlushEvents(minType, maxType);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_PollEvent(out Event e);
    public static bool PollEvent(out Event e) => SDL_PollEvent(out e);


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_WaitEvent(out Event e);
    public static bool WaitEvent(out Event e) => SDL_WaitEvent(out e);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WaitEventTimeout(out Event e, int timeoutMs);
    public static bool WaitEventTimeout(out Event e, int timeoutMs) => SDL_WaitEventTimeout(out e, timeoutMs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PushEvent(ref Event e);
    public static int PushEvent(ref Event e) => SDL_PushEvent(ref e);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventFilter(EventFilter filter, IntPtr userdata);
    public static void SetEventFilter(EventFilter filter, IntPtr userdata) => SDL_SetEventFilter(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_GetEventFilter(out EventFilter filter, out IntPtr userdata);
    public static bool GetEventFilter(out EventFilter filter, out IntPtr userdata) =>
        SDL_GetEventFilter(out filter, out userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_AddEventWatch(EventFilter filter, IntPtr userdata);
    public static int AddEventWatch(EventFilter filter, IntPtr userdata) => SDL_AddEventWatch(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DelEventWatch(EventFilter filter, IntPtr userdata);
    public static void DelEventWatch(EventFilter filter, IntPtr userdata) => SDL_DelEventWatch(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FilterEvents(EventFilter filter, IntPtr userdata);
    public static void FilterEvents(EventFilter filter, IntPtr userdata) => SDL_FilterEvents(filter, userdata);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventEnabled(uint type, [MarshalAs(SDLBool)] bool enabled);
    public static void SetEventEnabled(uint type, bool enabled) => SDL_SetEventEnabled(type, enabled);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_EventEnabled(uint type);
    public static bool EventEnabled(uint type) => SDL_EventEnabled(type);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_RegisterEvents(int numevents);
    public static uint RegisterEvents(int numevents) => SDL_RegisterEvents(numevents);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_AllocateEventMemory(UIntPtr size);
    public static IntPtr AllocateEventMemory(UIntPtr size) => SDL_AllocateEventMemory(size);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FreeEventMemory();
    public static void FreeEventMemory() => SDL_FreeEventMemory();
}