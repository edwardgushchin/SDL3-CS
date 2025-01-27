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

namespace SDL3;

public static partial class SDL
{
    /// <code>#define SDL_AtomicIncRef(a)    SDL_AddAtomicInt(a, 1)</code>
    /// <summary>
    /// <para>Increment an atomic variable used as a reference count.</para>
    /// <para><b>Note: If you don't know what this macro is for, you shouldn't use it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an SDL_AtomicInt to increment.</param>
    /// <returns>the previous value of the atomic variable.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="AtomicDecRef"/>
    [Macro]
    public static int AtomicIncRef(ref AtomicInt a) => AddAtomicInt(ref a, 1);
    
    
    /// <code>#define SDL_AtomicDecRef(a)    (SDL_AddAtomicInt(a, -1) == 1)</code>
    /// <summary>
    /// <para>Decrement an atomic variable used as a reference count.</para>
    /// <para><b>Note: If you don't know what this macro is for, you shouldn't use it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt"/> to increment.</param>
    /// <returns><c>true</c> if the variable reached zero after decrementing, <c>false</c>
    /// otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="AtomicIncRef"/>
    [Macro]
    public static bool AtomicDecRef(ref AtomicInt a) => AddAtomicInt(ref a, -1) == 1;
    
    
    /// <code>#define SDL_MemoryBarrierRelease() SDL_MemoryBarrierReleaseFunction()</code>
    /// <summary>
    /// <para>Insert a memory release barrier (macro version).</para>
    /// <para>Memory barriers are designed to prevent reads and writes from being
    /// reordered by the compiler and being seen out of order on multi-core CPUs.</para>
    /// <para>A typical pattern would be for thread A to write some data and a flag, and
    /// for thread B to read the flag and get the data. In this case you would
    /// insert a release barrier between writing the data and the flag,
    /// guaranteeing that the data write completes no later than the flag is
    /// written, and you would insert an acquire barrier between reading the flag
    /// and reading the data, to ensure that all the reads associated with the flag
    /// have completed.</para>
    /// <para>In this pattern you should always see a release barrier paired with an
    /// acquire barrier and you should gate the data reads/writes with a single
    /// flag variable.</para>
    /// <para>For more information on these semantics, take a look at the blog post:
    /// * http://preshing.com/20120913/acquire-and-release-semantics</para>
    /// <para>This is the macro version of this functionality; if possible, SDL will use
    /// compiler intrinsics or inline assembly, but some platforms might need to
    /// call the function version of this, <see cref="MemoryBarrierReleaseFunction"/> to do
    /// the heavy lifting. Apps that can use the macro should favor it over the
    /// function.</para>
    /// </summary>
    /// <threadsafety>Obviously this macro is safe to use from any thread at any
    /// time, but if you find yourself needing this, you are probably
    /// dealing with some very sensitive code; be careful!</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="MemoryBarrierAcquire"/>
    /// <seealso cref="MemoryBarrierReleaseFunction"/>
    [Macro]
    public static void MemoryBarrierRelease() => MemoryBarrierReleaseFunction();
    
    
    /// <code>#define SDL_MemoryBarrierAcquire() SDL_MemoryBarrierAcquireFunction()</code>
    /// <summary>
    /// <para>Insert a memory acquire barrier (macro version).</para>
    /// <para>Please see <see cref="MemoryBarrierRelease"/> for the details on what memory barriers
    /// are and when to use them.</para>
    /// <para>This is the macro version of this functionality; if possible, SDL will use
    /// compiler intrinsics or inline assembly, but some platforms might need to
    /// call the function version of this, <see cref="MemoryBarrierAcquireFunction"/>, to do
    /// the heavy lifting. Apps that can use the macro should favor it over the
    /// function.</para>
    /// </summary>
    /// <threadsafety>Obviously this macro is safe to use from any thread at any
    /// time, but if you find yourself needing this, you are probably
    /// dealing with some very sensitive code; be careful!</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="MemoryBarrierRelease"/>
    /// <seealso cref="MemoryBarrierAcquireFunction"/>
    [Macro]
    public static void MemoryBarrierAcquire() => MemoryBarrierAcquireFunction();
}