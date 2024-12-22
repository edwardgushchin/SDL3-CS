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

public partial class SDL
{
    /// <code>extern SDL_DECLSPEC SDL_MALLOC void * SDLCALL SDL_malloc(size_t size);</code>
    /// <summary>
    /// <para>Allocate uninitialized memory.</para>
    /// <para>The allocated memory returned by this function must be freed with
    /// <see cref="Free"/>.</para>
    /// <para>If <c>size</c> is 0, it will be set to 1.</para>
    /// <para>If you want to allocate memory aligned to a specific alignment, consider
    /// using <see cref="AlignedAlloc"/>.</para>
    /// </summary>
    /// <param name="size">the size to allocate.</param>
    /// <returns>a pointer to the allocated memory, or <c>null</c> if allocation failed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    /// <seealso cref="Free"/>
    /// <seealso cref="Calloc"/>
    /// <seealso cref="Realloc"/>
    /// <seealso cref="AlignedAlloc"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_malloc"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Malloc(ulong size);
    
    
    /// <code>extern SDL_DECLSPEC SDL_MALLOC SDL_ALLOC_SIZE2(1, 2) void * SDLCALL SDL_calloc(size_t nmemb, size_t size);</code>
    /// <summary>
    /// <para>Allocate a zero-initialized array.</para>
    /// <para>The memory returned by this function must be freed with <see cref="Free"/>.</para>
    /// <para>If either of <c>nmemb</c> or <c>size</c> is 0, they will both be set to 1.</para>
    /// </summary>
    /// <param name="nmemb">the number of elements in the array.</param>
    /// <param name="size">the size of each element of the array.</param>
    /// <returns>a pointer to the allocated array, or <c>null</c> if allocation failed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_calloc"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Calloc(ulong nmemb, ulong size);
    
    
    /// <code>extern SDL_DECLSPEC SDL_ALLOC_SIZE(2) void * SDLCALL SDL_realloc(void *mem, size_t size);</code>
    /// <summary>
    /// <para>Change the size of allocated memory.</para>
    /// <para>The memory returned by this function must be freed with <see cref="Free"/>.</para>
    /// <para>If <c>size</c> is 0, it will be set to 1. Note that this is unlike some other C
    /// runtime <c>realloc</c> implementations, which may treat <c>realloc(mem, 0)</c> the
    /// same way as <c>free(mem)</c>.</para>
    /// <para>If <c>mem</c> is <c>null</c>, the behavior of this function is equivalent to
    /// <see cref="Malloc"/>. Otherwise, the function can have one of three possible
    /// outcomes:</para>
    /// <list type="bullet">
    /// <item>If it returns the same pointer as <c>mem</c>, it means that <c>mem</c> was resized
    /// in place without freeing.</item>
    /// <item>If it returns a different non-NULL pointer, it means that <c>mem</c> was freed
    /// and cannot be dereferenced anymore.</item>
    /// <item>If it returns <c>null</c> (indicating failure), then <c>mem</c> will remain valid and
    /// must still be freed with <see cref="Free"/>.</item>
    /// </list>
    /// </summary>
    /// <param name="mem">a pointer to allocated memory to reallocate, or <c>null</c>.</param>
    /// <param name="size">the new size of the memory.</param>
    /// <returns>a pointer to the newly allocated memory, or <c>null</c> if allocation
    /// failed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    /// <seealso cref="Free"/>
    /// <seealso cref="Malloc"/>
    /// <seealso cref="Calloc"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_realloc"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Realloc(IntPtr mem, ulong size);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_free(void *mem);</code>
    /// <summary>
    /// <para>Free allocated memory.</para>
    /// <para>The pointer is no longer valid after this call and cannot be dereferenced
    /// anymore.</para>
    /// <para>If <c>mem</c> is <c>null</c>, this function does nothing.</para>
    /// </summary>
    /// <param name="mem">a pointer to allocated memory, or <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_free"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Free(IntPtr mem);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetOriginalMemoryFunctions(SDL_malloc_func *malloc_func, SDL_calloc_func *calloc_func, SDL_realloc_func *realloc_func, SDL_free_func *free_func);</code>
    /// <summary>
    /// <para>Get the original set of SDL memory functions.</para>
    /// <para>This is what <see cref="Malloc"/> and friends will use by default, if there has been
    /// no call to <see cref="SetMemoryFunctions"/>. This is not necessarily using the C
    /// runtime's <c>malloc</c> functions behind the scenes! Different platforms and
    /// build configurations might do any number of unexpected things.</para>
    /// </summary>
    /// <param name="mallocFunc">filled with malloc function.</param>
    /// <param name="callocFunc">filled with calloc function.</param>
    /// <param name="reallocFunc">filled with realloc function.</param>
    /// <param name="freeFunc">filled with free function.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetOriginalMemoryFunctions"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetOriginalMemoryFunctions(out MallocFunc mallocFunc, out CallocFunc callocFunc, out ReallocFunc reallocFunc, out FreeFunc freeFunc);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetMemoryFunctions(SDL_malloc_func *malloc_func, SDL_calloc_func *calloc_func, SDL_realloc_func *realloc_func, SDL_free_func *free_func);</code>
    /// <summary>
    /// <para>Get the current set of SDL memory functions.</para>
    /// </summary>
    /// <param name="mallocFunc">filled with malloc function.</param>
    /// <param name="callocFunc">filled with calloc function.</param>
    /// <param name="reallocFunc">filled with realloc function.</param>
    /// <param name="freeFunc">filled with free function.</param>
    /// <threadsafety>This does not hold a lock, so do not call this in the
    /// unlikely event of a background thread calling
    /// <see cref="SetMemoryFunctions"/> simultaneously.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    /// <seealso cref="SetMemoryFunctions"/>
    /// <seealso cref="GetOriginalMemoryFunctions"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMemoryFunctions"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMemoryFunctions(out MallocFunc mallocFunc, out CallocFunc callocFunc, out ReallocFunc reallocFunc, out FreeFunc freeFunc);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetMemoryFunctions(SDL_malloc_func malloc_func, SDL_calloc_func calloc_func, SDL_realloc_func realloc_func, SDL_free_func free_func);</code>
    /// <summary>
    /// <para>Replace SDL's memory allocation functions with a custom set.</para>
    /// <para>It is not safe to call this function once any allocations have been made,
    /// as future calls to SDL_free will use the new allocator, even if they came
    /// from an SDL_malloc made with the old one!</para>
    /// <para>If used, usually this needs to be the first call made into the SDL library,
    /// if not the very first thing done at program startup time.</para>
    /// </summary>
    /// <param name="mallocFunc">custom malloc function.</param>
    /// <param name="callocFunc">custom calloc function.</param>
    /// <param name="reallocFunc">custom realloc function.</param>
    /// <param name="freeFunc">custom free function.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but one
    /// should not replace the memory functions once any allocations
    /// are made!</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    /// <seealso cref="GetMemoryFunctions"/>
    /// <seealso cref="GetOriginalMemoryFunctions"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetMemoryFunctions"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetMemoryFunctions(MallocFunc mallocFunc, CallocFunc callocFunc, ReallocFunc reallocFunc, FreeFunc freeFunc);
    
    
    /// <code>extern SDL_DECLSPEC SDL_MALLOC void * SDLCALL SDL_aligned_alloc(size_t alignment, size_t size);</code>
    /// <summary>
    /// <para>Allocate memory aligned to a specific alignment.</para>
    /// <para>The memory returned by this function must be freed with <see cref="AlignedFree"/>,
    /// _not_ <see cref="Free"/>.</para>
    /// <para>If <c>alignment</c> is less than the size of <c>void *</c>, it will be increased to
    /// match that.</para>
    /// <para>The returned memory address will be a multiple of the alignment value, and
    /// the size of the memory allocated will be a multiple of the alignment value.</para>
    /// </summary>
    /// <param name="alignment">the alignment of the memory.</param>
    /// <param name="size">the size to allocate.</param>
    /// <returns>a pointer to the aligned memory, or <c>null</c> if allocation failed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    /// <seealso cref="AlignedFree"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_aligned_alloc"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr AlignedAlloc(ulong alignment, ulong size);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_aligned_free(void *mem);</code>
    /// <summary>
    /// <para>Free memory allocated by <see cref="AlignedAlloc"/>.</para>
    /// <para>The pointer is no longer valid after this call and cannot be dereferenced
    /// anymore.</para>
    /// <para>If <c>mem</c> is <c>null</c>, this function does nothing.</para>
    /// </summary>
    /// <param name="mem">a pointer previously returned by <see cref="AlignedAlloc"/>, or <c>null</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    /// <seealso cref="AlignedFree"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_aligned_alloc"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void AlignedFree(IntPtr mem);


    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_memset(SDL_OUT_BYTECAP(len) void *dst, int c, size_t len);</code>
    /// <summary>
    /// <para>Initialize all bytes of buffer of memory to a specific value.</para>
    /// <para>This function will set <c>len</c> bytes, pointed to by <c>dst</c>, to the value
    /// specified in <c>c</c>.</para>
    /// <para>Despite <c>c</c> being an <c>int</c> instead of a <c>char</c>, this only operates on
    /// bytes; <c>c</c> must be a value between 0 and 255, inclusive.</para>
    /// </summary>
    /// <param name="dst">the destination memory region. Must not be <c>null</c>.</param>
    /// <param name="c">the byte value to set.</param>
    /// <param name="len">the length, in bytes, to set in <c>dst</c>.</param>
    /// <returns><c>dst</c></returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.3.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_memset"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Memset(IntPtr dst, int c, uint len);
}