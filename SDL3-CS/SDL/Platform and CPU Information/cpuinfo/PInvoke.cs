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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumLogicalCPUCores"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumLogicalCPUCores();
    private delegate int GetNumLogicalCPUCoresNativeDelegate();
    private static GetNumLogicalCPUCoresNativeDelegate GetNumLogicalCPUCoresNativeFunction = SDL_GetNumLogicalCPUCores;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumLogicalCPUCores(void);</code>
    /// <summary>
    /// Get the number of logical CPU cores available.
    /// </summary>
    /// <returns>the total number of logical CPU cores. On CPUs that include
    /// technologies such as hyperthreading, the number of logical cores
    /// may be more than the number of physical cores.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int GetNumLogicalCPUCores()
    {
        return GetNumLogicalCPUCoresNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCPUCacheLineSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetCPUCacheLineSize();
    private delegate int GetCPUCacheLineSizeNativeDelegate();
    private static GetCPUCacheLineSizeNativeDelegate GetCPUCacheLineSizeNativeFunction = SDL_GetCPUCacheLineSize;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetCPUCacheLineSize(void);</code>
    /// <summary>
    /// <para>Determine the L1 cache line size of the CPU.</para>
    /// <para>This is useful for determining multi-threaded structure padding or SIMD
    /// prefetch sizes.</para>
    /// </summary>
    /// <returns>the L1 cache line size of the CPU, in bytes.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int GetCPUCacheLineSize()
    {
        return GetCPUCacheLineSizeNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAltiVec"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasAltiVec();
    private delegate bool HasAltiVecNativeDelegate();
    private static HasAltiVecNativeDelegate HasAltiVecNativeFunction = SDL_HasAltiVec;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAltiVec(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AltiVec features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using PowerPC instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AltiVec features or <c>false</c> if not.</returns>
    ///
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool HasAltiVec()
    {
        return HasAltiVecNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasMMX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasMMX();
    private delegate bool HasMMXNativeDelegate();
    private static HasMMXNativeDelegate HasMMXNativeFunction = SDL_HasMMX;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasMMX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has MMX features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has MMX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool HasMMX()
    {
        return HasMMXNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasSSE();
    private delegate bool HasSSENativeDelegate();
    private static HasSSENativeDelegate HasSSENativeFunction = SDL_HasSSE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE()
    {
        return HasSSENativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE2"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasSSE2();
    private delegate bool HasSSE2NativeDelegate();
    private static HasSSE2NativeDelegate HasSSE2NativeFunction = SDL_HasSSE2;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE2(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE2 features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE2 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE2()
    {
        return HasSSE2NativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE3"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasSSE3();
    private delegate bool HasSSE3NativeDelegate();
    private static HasSSE3NativeDelegate HasSSE3NativeFunction = SDL_HasSSE3;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE3(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE3 features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE3 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE3()
    {
        return HasSSE3NativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE41"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasSSE41();
    private delegate bool HasSSE41NativeDelegate();
    private static HasSSE41NativeDelegate HasSSE41NativeFunction = SDL_HasSSE41;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE41(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE4.1 features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE4.1 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE41()
    {
        return HasSSE41NativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSSE42"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasSSE42();
    private delegate bool HasSSE42NativeDelegate();
    private static HasSSE42NativeDelegate HasSSE42NativeFunction = SDL_HasSSE42;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSSE42(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE4.2 features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE4.2 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    public static bool HasSSE42()
    {
        return HasSSE42NativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAVX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasAVX();
    private delegate bool HasAVXNativeDelegate();
    private static HasAVXNativeDelegate HasAVXNativeFunction = SDL_HasAVX;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAVX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasAVX2"/>
    /// <seealso cref="HasAVX512F"/>
    public static bool HasAVX()
    {
        return HasAVXNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAVX2"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasAVX2();
    private delegate bool HasAVX2NativeDelegate();
    private static HasAVX2NativeDelegate HasAVX2NativeFunction = SDL_HasAVX2;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAVX2(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX2 features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX2 features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX512F"/>
    public static bool HasAVX2()
    {
        return HasAVX2NativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasAVX512F"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasAVX512F();
    private delegate bool HasAVX512FNativeDelegate();
    private static HasAVX512FNativeDelegate HasAVX512FNativeFunction = SDL_HasAVX512F;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasAVX512F(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX-512F (foundation) features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX-512F features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX2"/>
    public static bool HasAVX512F()
    {
        return HasAVX512FNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasARMSIMD"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasARMSIMD();
    private delegate bool HasARMSIMDNativeDelegate();
    private static HasARMSIMDNativeDelegate HasARMSIMDNativeFunction = SDL_HasARMSIMD;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasARMSIMD(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has ARM SIMD (ARMv6) features.</para>
    /// <para>This is different from ARM NEON, which is a different instruction set.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using ARM instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has ARM SIMD features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasNEON"/>
    public static bool HasARMSIMD()
    {
        return HasARMSIMDNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasNEON"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasNEON();
    private delegate bool HasNEONNativeDelegate();
    private static HasNEONNativeDelegate HasNEONNativeFunction = SDL_HasNEON;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasNEON(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has NEON (ARM SIMD) features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using ARM instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has ARM NEON features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool HasNEON()
    {
        return HasNEONNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasLSX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasLSX();
    private delegate bool HasLSXNativeDelegate();
    private static HasLSXNativeDelegate HasLSXNativeFunction = SDL_HasLSX;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasLSX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has LSX (LOONGARCH SIMD) features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using LOONGARCH instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has LOONGARCH LSX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool HasLSX()
    {
        return HasLSXNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasLASX"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HasLASX();
    private delegate bool HasLASXNativeDelegate();
    private static HasLASXNativeDelegate HasLASXNativeFunction = SDL_HasLASX;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasLASX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has LASX (LOONGARCH SIMD) features.</para>
    /// <para>This always returns <c>false</c> on CPUs that aren't using LOONGARCH instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has LOONGARCH LASX features or <c>false</c> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool HasLASX()
    {
        return HasLASXNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSystemRAM"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSystemRAM();
    private delegate int GetSystemRAMNativeDelegate();
    private static GetSystemRAMNativeDelegate GetSystemRAMNativeFunction = SDL_GetSystemRAM;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSystemRAM(void);</code>
    /// <summary>
    /// Get the amount of RAM configured in the system.
    /// </summary>
    /// <returns>the amount of RAM configured in the system in MiB.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int GetSystemRAM()
    {
        return GetSystemRAMNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSIMDAlignment"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial UIntPtr SDL_GetSIMDAlignment();
    private delegate UIntPtr GetSIMDAlignmentNativeDelegate();
    private static GetSIMDAlignmentNativeDelegate GetSIMDAlignmentNativeFunction = SDL_GetSIMDAlignment;

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
    public static UIntPtr GetSIMDAlignment()
    {
        return GetSIMDAlignmentNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSystemPageSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSystemPageSize();
    private delegate int GetSystemPageSizeNativeDelegate();
    private static GetSystemPageSizeNativeDelegate GetSystemPageSizeNativeFunction = SDL_GetSystemPageSize;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSystemPageSize(void);</code>
    /// <summary>
    /// Report the size of a page of memory.
    /// <para>Different platforms might have different memory page sizes. In current
    /// times, 4 kilobytes is not unusual, but newer systems are moving to larger
    /// page sizes, and esoteric platforms might have any unexpected size.</para>
    /// <para>Note that this function can return 0, which means SDL can't determine the
    /// page size on this platform. It will _not_ set an error string to be
    /// retrieved with <see cref="GetError"/> in this case! In this case, defaulting to
    /// 4096 is often a reasonable option.</para>
    /// </summary>
    /// <returns>the size of a single page of memory, in bytes, or 0 if SDL can't
    /// determine this information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    public static int GetSystemPageSize()
    {
        return GetSystemPageSizeNativeFunction();
    }
}
