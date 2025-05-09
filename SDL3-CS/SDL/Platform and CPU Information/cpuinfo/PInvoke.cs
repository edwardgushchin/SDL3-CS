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
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumLogicalCPUCores(void);</code>
    /// <summary>
    /// Get the number of logical CPU cores available.
    /// </summary>
    /// <returns>the total number of logical CPU cores. On CPUs that include
    /// technologies such as hyperthreading, the number of logical cores
    /// may be more than the number of physical cores.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumLogicalCPUCores"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumLogicalCPUCores();
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetCPUCacheLineSize(void);</code>
    /// <summary>
    /// <para>Determine the L1 cache line size of the CPU.</para>
    /// <para>This is useful for determining multi-threaded structure padding or SIMD
    /// prefetch sizes.</para>
    /// </summary>
    /// <returns>the L1 cache line size of the CPU, in bytes.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCPUCacheLineSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCPUCacheLineSize();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAltiVec(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AltiVec features.</para>
    /// <para>This always returns false on CPUs that aren't using PowerPC instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AltiVec features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAltiVec"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAltiVec();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasMMX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has MMX features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has MMX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasMMX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasMMX();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSSE();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE2(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE2 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE2 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE2"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSSE2();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE3(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE3 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE3 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE3"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSSE3();


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE41(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE4.1 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE4.1 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE42"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE41"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSSE41();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE42(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE4.2 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE4.2 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE42"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSSE42();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAVX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasAVX2"/>
    /// <seealso cref="HasAVX512F"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAVX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAVX();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAVX2(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX2 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX2 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX512F"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAVX2"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAVX2();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAVX512F(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX-512F (foundation) features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX-512F features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX2"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAVX512F"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAVX512F();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasARMSIMD(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has ARM SIMD (ARMv6) features.</para>
    /// <para>This is different from ARM NEON, which is a different instruction set.</para>
    /// <para>This always returns false on CPUs that aren't using ARM instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has ARM SIMD features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasNEON"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasARMSIMD"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasARMSIMD();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasNEON(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has NEON (ARM SIMD) features.</para>
    /// <para>This always returns false on CPUs that aren't using ARM instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has ARM NEON features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasNEON"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasNEON();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasLSX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has LSX (LOONGARCH SIMD) features.</para>
    /// <para>This always returns false on CPUs that aren't using LOONGARCH instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has LOONGARCH LSX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasLSX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasLSX();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasLASX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has LASX (LOONGARCH SIMD) features.</para>
    /// <para>This always returns false on CPUs that aren't using LOONGARCH instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has LOONGARCH LASX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasLASX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasLASX();
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSystemRAM(void);</code>
    /// <summary>
    /// Get the amount of RAM configured in the system.
    /// </summary>
    /// <returns>the amount of RAM configured in the system in MiB.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSystemRAM"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSystemRAM();
    
    
    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_GetSIMDAlignment(void);</code>
    /// <summary>
    /// <para>Report the alignment this system needs for SIMD allocations.</para>
    /// <para>This will return the minimum number of bytes to which a pointer must be
    /// aligned to be compatible with SIMD instructions on the current machine. For
    /// example, if the machine supports SSE only, it will return 16, but if it
    /// supports AVX-512F, it'll return 64 (etc). This only reports values for
    /// instruction sets SDL knows about, so if your SDL build doesn't have
    /// <see cref="HasAVX512F"/>, then it might return 16 for the SSE support it sees and
    /// not 64 for the AVX-512 instructions that exist but SDL doesn't know about.
    /// Plan accordingly.</para>
    /// </summary>
    /// <returns>the alignment in bytes needed for available, known SIMD
    /// instructions.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AlignedAlloc"/>
    /// <seealso cref="AlignedFree"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSIMDAlignment"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial UIntPtr GetSIMDAlignment();
}