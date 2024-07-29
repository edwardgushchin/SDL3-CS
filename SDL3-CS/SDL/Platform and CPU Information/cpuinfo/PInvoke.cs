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
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetCPUCount();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetCPUCount(void);</code>
    /// <summary>
    /// Get the number of CPU cores available.
    /// </summary>
    /// <returns>the total number of logical CPU cores. On CPUs that include
    /// technologies such as hyperthreading, the number of logical cores
    /// may be more than the number of physical cores.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetCPUCount() => SDL_GetCPUCount();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetCPUCacheLineSize();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetCPUCacheLineSize(void);</code>
    /// <summary>
    /// <para>Determine the L1 cache line size of the CPU.</para>
    /// <para>This is useful for determining multi-threaded structure padding or SIMD
    /// prefetch sizes.</para>
    /// </summary>
    /// <returns>the L1 cache line size of the CPU, in bytes.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetCPUCacheLineSize() => SDL_GetCPUCacheLineSize();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasAltiVec();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasAltiVec(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AltiVec features.</para>
    /// <para>This always returns false on CPUs that aren't using PowerPC instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AltiVec features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool HasAltiVec() => SDL_HasAltiVec();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasMMX();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasMMX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has MMX features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has MMX features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool HasMMX() => SDL_HasMMX();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasSSE();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasSSE(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE() => SDL_HasSSE();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasSSE2();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasSSE2(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE2 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE2 features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE2() => SDL_HasSSE2();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasSSE3();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasSSE3(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE3 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE3 features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE41"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE3() => SDL_HasSSE3();


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasSSE41();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasSSE41(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE4.1 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE4.1 features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasSSE"/>
    /// <seealso cref="HasSSE2"/>
    /// <seealso cref="HasSSE3"/>
    /// <seealso cref="HasSSE42"/>
    public static bool HasSSE41() => SDL_HasSSE41();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasSSE42();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasSSE42(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has SSE4.2 features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has SSE4.2 features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasAVX2"/>
    /// <seealso cref="HasAVX512F"/>
    public static bool HasSSE42() => SDL_HasSSE42();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasAVX();
    /// <summary>
    /// <para>Determine whether the CPU has AVX features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasAVX2"/>
    /// <seealso cref="HasAVX512F"/>
    public static bool HasAVX() => SDL_HasAVX();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasAVX2();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasAVX2(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX-512F (foundation) features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX-512F features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX2"/>
    public static bool HasAVX2() => SDL_HasAVX2();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasAVX512F();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasAVX512F(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has AVX-512F (foundation) features.</para>
    /// <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has AVX-512F features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasAVX"/>
    /// <seealso cref="HasAVX2"/>
    public static bool HasAVX512F() => SDL_HasAVX512F();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasARMSIMD();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasARMSIMD(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has ARM SIMD (ARMv6) features.</para>
    /// <para>This is different from ARM NEON, which is a different instruction set.</para>
    /// <para>This always returns false on CPUs that aren't using ARM instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has ARM SIMD features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasNEON"/>
    public static bool HasARMSIMD() => SDL_HasARMSIMD();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasNEON();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasNEON(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has NEON (ARM SIMD) features.</para>
    /// <para>This always returns false on CPUs that aren't using ARM instruction sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has ARM NEON features or <c>false</c> if not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool HasNEON() => SDL_HasNEON();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasLSX();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasLSX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has LSX (LOONGARCH SIMD) features.</para>
    /// <para>This always returns false on CPUs that aren't using LOONGARCH instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has LOONGARCH LSX features or <c>false</c> if
    /// not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool HasLSX() => SDL_HasLSX();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasLASX();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasLASX(void);</code>
    /// <summary>
    /// <para>Determine whether the CPU has LASX (LOONGARCH SIMD) features.</para>
    /// <para>This always returns false on CPUs that aren't using LOONGARCH instruction
    /// sets.</para>
    /// </summary>
    /// <returns><c>true</c> if the CPU has LOONGARCH LASX features or <c>false</c> if
    /// not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool HasLASX() => SDL_HasLASX();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSystemRAM();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSystemRAM(void);</code>
    /// <summary>
    /// Get the amount of RAM configured in the system.
    /// </summary>
    /// <returns>the amount of RAM configured in the system in MiB.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetSystemRAM() => SDL_GetSystemRAM();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_GetSIMDAlignment();
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
    /// <returns></returns>
    public static nuint GetSIMDAlignment() => SDL_GetSIMDAlignment();
}