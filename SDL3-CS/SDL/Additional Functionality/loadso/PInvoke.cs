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

/**
 * # CategorySharedObject
 *
 * System-dependent library loading routines.
 *
 * Some things to keep in mind:
 *
 * - These functions only work on C function names. Other languages may have
 *   name mangling and intrinsic language support that varies from compiler to
 *   compiler.
 * - Make sure you declare your function pointers with the same calling
 *   convention as the actual library function. Your code will crash
 *   mysteriously if you do not do this.
 * - Avoid namespace collisions. If you load a symbol from the library, it is
 *   not defined whether or not it goes into the global symbol namespace for
 *   the application. If it does and it conflicts with symbols in your code or
 *   other shared libraries, you will not get the results you expect. :)
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_LoadObject([MarshalAs(UnmanagedType.LPUTF8Str)] string sofile);
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_LoadObject(const char *sofile);</code>
    /// <summary>
    /// Dynamically load a shared object.
    /// </summary>
    /// <param name="sofile">a system-dependent name of the object file.</param>
    /// <returns>an opaque pointer to the object handle or <see cref="IntPtr.Zero"/> if there was an
    /// error; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LoadFunction"/>
    /// <seealso cref="UnloadObject"/>
    public static IntPtr LoadObject(string sofile) => SDL_LoadObject(sofile);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial FunctionPointer? SDL_LoadFunction(IntPtr handle,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC SDL_FunctionPointer SDLCALL SDL_LoadFunction(void *handle, const char *name);</code>
    /// <summary>
    /// <para>Look up the address of the named function in a shared object.</para>
    /// <para>This function pointer is no longer valid after calling <see cref="UnloadObject"/>.</para>
    /// <para>This function can only look up C function names. Other languages may have
    /// name mangling and intrinsic language support that varies from compiler to
    /// compiler.</para>
    /// <para>Make sure you declare your function pointers with the same calling
    /// convention as the actual library function. Your code will crash
    /// mysteriously if you do not do this.</para>
    /// <para>If the requested function doesn't exist, <c>NULL</c> is returned.</para>
    /// </summary>
    /// <param name="handle">a valid shared object handle returned by <see cref="LoadObject"/>.</param>
    /// <param name="name">the name of the function to look up.</param>
    /// <returns>a pointer to the function or <c>NULL</c> if there was an error; call
    /// <see cref="GetError"/> for more information.</returns>
    public static FunctionPointer? LoadFunction(IntPtr handle, string name) => SDL_LoadFunction(handle, name);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnloadObject(IntPtr handle);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnloadObject(void *handle);</code>
    /// <summary>
    /// Unload a shared object from memory.
    /// </summary>
    /// <param name="handle">a valid shared object handle returned by <see cref="LoadObject"/>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LoadObject"/>
    public static void UnloadObject(IntPtr handle) => SDL_UnloadObject(handle);
}