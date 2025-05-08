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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef SDL_EGLAttrib *(SDLCALL *SDL_EGLAttribArrayCallback)(void *userdata);</code>
    /// <summary>
    /// <para>EGL platform attribute initialization callback.</para>
    /// <para>This is called when SDL is attempting to create an EGL context, to let the
    /// app add extra attributes to its eglGetPlatformDisplay() call.</para>
    /// <para>The callback should return a pointer to an EGL attribute array terminated
    /// with <c>EGL_NONE</c>. If this function returns <c>null</c>, the <see cref="CreateWindow"/>
    /// process will fail gracefully.</para>
    /// <para>The returned pointer should be allocated with SDL_malloc() and will be
    /// passed to <see cref="Free"/>.</para>
    /// <para>The arrays returned by each callback will be appended to the existing
    /// attribute arrays defined by SDL.</para>
    /// </summary>
    /// <param name="userdata">an app-controlled pointer that is passed to the callback.</param>
    /// <returns>a newly-allocated array of attributes, terminated with <c>EGL_NONE</c>.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="EGLSetAttributeCallbacks"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr EGLAttribArrayCallback(IntPtr userdata);
    
    
    /// <code>typedef SDL_EGLint *(SDLCALL *SDL_EGLIntArrayCallback)(void *userdata, SDL_EGLDisplay display, SDL_EGLConfig config);</code>
    /// <summary>
    /// <para>EGL surface/context attribute initialization callback types.</para>
    /// <para>This is called when SDL is attempting to create an EGL surface, to let the
    /// app add extra attributes to its eglCreateWindowSurface() or
    /// eglCreateContext calls.</para>
    /// <para>For convenience, the EGLDisplay and EGLConfig to use are provided to the
    /// callback.</para>
    /// <para>The callback should return a pointer to an EGL attribute array terminated
    /// with <c>EGL_NONE</c>. If this function returns <c>null</c>, the <see cref="CreateWindow"/>
    /// process will fail gracefully.</para>
    /// <para>The returned pointer should be allocated with SDL_malloc() and will be
    /// passed to <see cref="Free"/>.</para>
    /// <para>The arrays returned by each callback will be appended to the existing
    /// attribute arrays defined by SDL.</para>
    /// </summary>
    /// <param name="userdata">an app-controlled pointer that is passed to the callback.</param>
    /// <param name="display">the EGL display to be used.</param>
    /// <param name="config">the EGL config to be used.</param>
    /// <returns>a newly-allocated array of attributes, terminated with <c>EGL_NONE</c>.</returns>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="EGLSetAttributeCallbacks"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr EGLIntArrayCallback(IntPtr userdata, IntPtr display, IntPtr config);
    
    
    /// <code>typedef SDL_HitTestResult (SDLCALL *SDL_HitTest)(SDL_Window *win, const SDL_Point *area, void *data);</code>
    /// <summary>
    /// Callback used for hit-testing.
    /// </summary>
    /// <param name="win">the SDL_Window where hit-testing was set on.</param>
    /// <param name="area">an <see cref="Point"/> which should be hit-tested.</param>
    /// <param name="data">what was passed as <c>callback_data</c> to <see cref="SetWindowHitTest"/>.</param>
    /// <returns>an <see cref="HitTestResult"/> value.</returns>
    /// <seealso cref="SetWindowHitTest"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate HitTestResult HitTest(IntPtr win, in Point area, IntPtr data);
}
