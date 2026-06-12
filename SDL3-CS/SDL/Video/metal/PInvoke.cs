#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Metal_CreateView"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_Metal_CreateView(IntPtr window);
    private delegate IntPtr MetalCreateViewNativeDelegate(IntPtr window);
    private static MetalCreateViewNativeDelegate MetalCreateViewNativeFunction = SDL_Metal_CreateView;

    /// <code>extern SDL_DECLSPEC SDL_MetalView SDLCALL SDL_Metal_CreateView(SDL_Window *window);</code>
    /// <summary>
    /// <para>Create a CAMetalLayer-backed NSView/UIView and attach it to the specified
    /// window.</para>
    /// <para>On macOS, this does <b>not</b> associate a MTLDevice with the CAMetalLayer on
    /// its own. It is up to user code to do that.</para>
    /// <para>The returned handle can be casted directly to a NSView or UIView. To access
    /// the backing CAMetalLayer, call <see cref="MetalGetLayer"/>.</para>
    /// </summary>
    /// <param name="window">the window.</param>
    /// <returns>handle NSView or UIView.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MetalDestroyView"/>
    /// <seealso cref="MetalGetLayer"/>
    public static IntPtr MetalCreateView(IntPtr window)
    {
        return MetalCreateViewNativeFunction(window);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Metal_DestroyView"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Metal_DestroyView(IntPtr view);
    private delegate void MetalDestroyViewNativeDelegate(IntPtr view);
    private static MetalDestroyViewNativeDelegate MetalDestroyViewNativeFunction = SDL_Metal_DestroyView;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Metal_DestroyView(SDL_MetalView view);</code>
    /// <summary>
    /// <para>Destroy an existing SDL_MetalView object.</para>
    /// <para>This should be called before <see cref="DestroyWindow"/>, if <see cref="MetalCreateView"/> was
    /// called after <see cref="CreateWindow"/>.</para>
    /// </summary>
    /// <param name="view">the SDL_MetalView object.</param>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MetalCreateView"/>
    public static void MetalDestroyView(IntPtr view)
    {
        MetalDestroyViewNativeFunction(view);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Metal_GetLayer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_Metal_GetLayer(IntPtr view);
    private delegate IntPtr MetalGetLayerNativeDelegate(IntPtr view);
    private static MetalGetLayerNativeDelegate MetalGetLayerNativeFunction = SDL_Metal_GetLayer;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_Metal_GetLayer(SDL_MetalView view);</code>
    /// <summary>
    /// <para>Get a pointer to the backing CAMetalLayer for the given view.</para>
    /// </summary>
    /// <param name="view">the SDL_MetalView object.</param>
    /// <returns>a pointer.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr MetalGetLayer(IntPtr view)
    {
        return MetalGetLayerNativeFunction(view);
    }
}
