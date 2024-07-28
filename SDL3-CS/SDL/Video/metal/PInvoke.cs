#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_Metal_CreateView(IntPtr window);
    /// <code>extern SDL_DECLSPEC SDL_MetalView SDLCALL SDL_Metal_CreateView(SDL_Window * window);</code>
    /// <summary>
    /// <para>Create a CAMetalLayer-backed NSView/UIView and attach it to the specified
    /// window.</para>
    /// <para>On macOS, this does *not* associate a MTLDevice with the CAMetalLayer on
    /// its own. It is up to user code to do that.</para>
    /// <para>The returned handle can be casted directly to a NSView or UIView. To access
    /// the backing CAMetalLayer, call <see cref="MetalGetLayer"/>.</para>
    /// </summary>
    /// <param name="window">the window.</param>
    /// <returns>handle NSView or UIView.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="MetalDestroyView"/>
    /// <seealso cref="MetalGetLayer"/>
    public static MetalView MetalCreateView(Window window)
    {
        return new MetalView(SDL_Metal_CreateView(window.Handle));
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Metal_DestroyView(IntPtr view);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Metal_DestroyView(SDL_MetalView view);</code>
    /// <summary>
    /// <para>Destroy an existing <see cref="MetalView"/> object.</para>
    /// <para>This should be called before <see cref="DestroyWindow"/>, if <see cref="MetalCreateView"/> was
    /// called after <see cref="CreateWindow"/>.</para>
    /// </summary>
    /// <param name="view">the <see cref="MetalView"/> object.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="MetalCreateView"/>
    public static void MetalDestroyView(MetalView view) => SDL_Metal_DestroyView(view.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_Metal_GetLayer(IntPtr view);
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_Metal_GetLayer(SDL_MetalView view);</code>
    /// <summary>
    /// Get a pointer to the backing CAMetalLayer for the given view.
    /// </summary>
    /// <param name="view">the <see cref="MetalView"/> object.</param>
    /// <returns>a pointer.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static IntPtr MetalGetLayer(MetalView view) => SDL_Metal_GetLayer(view.Handle);
}