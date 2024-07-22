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

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef SDL_EGLAttrib *(SDLCALL *SDL_EGLAttribArrayCallback)(void);</code>
    /// <summary>
    /// EGL attribute initialization callback types.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr EGLAttribArrayCallback();
    
    
    /// <code>typedef SDL_EGLint *(SDLCALL *SDL_EGLIntArrayCallback)(void);</code>
    /// <summary>
    /// EGL attribute initialization callback types.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EGLIntArrayCallback();
    
    
    /// <code>typedef SDL_HitTestResult (SDLCALL *SDL_HitTest)(SDL_Window *win,const SDL_Point *area, void *data);</code>
    /// <summary>
    /// Callback used for hit-testing.
    /// </summary>
    /// <param name="win">the <see cref="Window.Handle"/> where hit-testing was set on.</param>
    /// <param name="area">an <see cref="Point"/> which should be hit-tested.</param>
    /// <param name="data">what was passed as `callback_data` to <see cref="SetWindowHitTest"/>.</param>
    /// <returns>an <see cref="HitTestResult"/> value.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate HitTestResult HitTest(IntPtr win, in Point area, IntPtr data);
}