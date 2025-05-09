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
    /// <code>typedef void *(SDLCALL *SDL_malloc_func)(size_t size);</code>
    /// <summary>
    /// <para>A callback used to implement <see cref="Malloc"/>.</para>
    /// <para>SDL will always ensure that the passed <c>size</c> is greater than 0.</para>
    /// </summary>
    /// <param name="size">the size to allocate.</param>
    /// <returns>a pointer to the allocated memory, or <c>null</c> if allocation failed.</returns>
    /// <threadsafety>It should be safe to call this callback from any thread.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="Malloc"/>
    /// <seealso cref="GetOriginalMemoryFunctions"/>
    /// <seealso cref="GetMemoryFunctions"/>
    /// <seealso cref="SetMemoryFunctions"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr MallocFunc(UIntPtr size);
    
    
    /// <code>typedef void *(SDLCALL *SDL_calloc_func)(size_t nmemb, size_t size);</code>
    /// <summary>
    /// <para>A callback used to implement <see cref="Calloc"/>.</para>
    /// <para>SDL will always ensure that the passed <c>nmemb</c> and <c>size</c> are both greater
    /// than 0.</para>
    /// </summary>
    /// <param name="nmemb">the number of elements in the array.</param>
    /// <param name="size">the size of each element of the array.</param>
    /// <returns>a pointer to the allocated array, or <c>null</c> if allocation failed.</returns>
    /// <threadsafety>It should be safe to call this callback from any thread.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="Calloc"/>
    /// <seealso cref="GetOriginalMemoryFunctions"/>
    /// <seealso cref="GetMemoryFunctions"/>
    /// <seealso cref="SetMemoryFunctions"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CallocFunc(UIntPtr nmemb, UIntPtr size);
    
    
    /// <code>typedef void *(SDLCALL *SDL_realloc_func)(void *mem, size_t size);</code>
    /// <summary>
    /// <para>A callback used to implement <see cref="Realloc"/>.</para>
    /// <para>SDL will always ensure that the passed <c>size</c> is greater than 0.</para>
    /// </summary>
    /// <param name="mem">a pointer to allocated memory to reallocate, or <c>null</c>.</param>
    /// <param name="size">the new size of the memory.</param>
    /// <para>a pointer to the newly allocated memory, or <c>null</c> if allocation
    /// failed.</para>
    /// <threadsafety>It should be safe to call this callback from any thread.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="Realloc"/>
    /// <seealso cref="GetOriginalMemoryFunctions"/>
    /// <seealso cref="GetMemoryFunctions"/>
    /// <seealso cref="SetMemoryFunctions"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ReallocFunc(IntPtr mem, UIntPtr size);
    
    
    /// <code>typedef void (SDLCALL *SDL_free_func)(void *mem);</code>
    /// <summary>
    /// <para>A callback used to implement <see cref="Free"/>.</para>
    /// <para>SDL will always ensure that the passed <c>mem</c> is a non-NULL pointer.</para>
    /// </summary>
    /// <param name="mem">a pointer to allocated memory.</param>
    /// <threadsafety>It should be safe to call this callback from any thread.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="Free"/>
    /// <seealso cref="GetOriginalMemoryFunctions"/>
    /// <seealso cref="GetMemoryFunctions"/>
    /// <seealso cref="SetMemoryFunctions"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeFunc(IntPtr mem);
    
    
    /// <summary>
    /// <para>A generic function pointer.</para>
    /// <para>In theory, generic function pointers should use this, instead of <c>void *</c>,
    /// since some platforms could treat code addresses differently than data
    /// addresses. Although in current times no popular platforms make this
    /// distinction, it is more correct and portable to use the correct type for a
    /// generic pointer.</para>
    /// <para>If for some reason you need to force this typedef to be an actual <c>void *</c>,
    /// perhaps to work around a compiler or existing code, you can define
    /// <c>SDL_FUNCTION_POINTER_IS_VOID_POINTER</c> before including any SDL headers.</para>
    /// </summary>
    /// <since>This datatype is available since SDL 3.2.0</since>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FunctionPointer();
}