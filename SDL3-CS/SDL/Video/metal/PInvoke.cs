#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MetalDestroyView"/>
    /// <seealso cref="MetalGetLayer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Metal_CreateView"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr MetalCreateView(IntPtr window);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Metal_DestroyView(SDL_MetalView view);</code>
    /// <summary>
    /// <para>Destroy an existing SDL_MetalView object.</para>
    /// <para>This should be called before <see cref="DestroyWindow"/>, if <see cref="MetalCreateView"/> was
    /// called after <see cref="CreateWindow"/>.</para>
    /// </summary>
    /// <param name="view">the SDL_MetalView object.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MetalCreateView"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Metal_DestroyView"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetalDestroyView(IntPtr view);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_Metal_GetLayer(SDL_MetalView view);</code>
    /// <summary>
    /// <para>Get a pointer to the backing CAMetalLayer for the given view.</para>
    /// </summary>
    /// <param name="view">the SDL_MetalView object.</param>
    /// <returns>a pointer.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Metal_GetLayer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr MetalGetLayer(IntPtr view);
}