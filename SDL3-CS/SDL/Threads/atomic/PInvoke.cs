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
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TryLockSpinlock(SDL_SpinLock *lock);</code>
    /// <summary>
    /// <para>Try to lock a spin lock by setting it to a non-zero value.</para>
    /// <para><b>Please note that spinlocks are dangerous if you don't know what you're
    /// doing. Please be careful using any sort of spinlock!</b></para>
    /// </summary>
    /// <param name="lock">a pointer to a lock variable.</param>
    /// <returns><c>true</c> if the lock succeeded, <c>false</c> if the lock is already held.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockSpinlock"/>
    /// <seealso cref="UnlockSpinlock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TryLockSpinlock"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TryLockSpinlock(ref int @lock);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockSpinlock(SDL_SpinLock *lock);</code>
    /// <summary>
    /// <para>Lock a spin lock by setting it to a non-zero value.</para>
    /// <para><b>Please note that spinlocks are dangerous if you don't know what you're
    /// doing. Please be careful using any sort of spinlock!</b></para>
    /// </summary>
    /// <param name="lock">a pointer to a lock variable.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="TryLockSpinlock"/>
    /// <seealso cref="UnlockSpinlock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockSpinlock"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LockSpinlock(ref int @lock);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockSpinlock(SDL_SpinLock *lock);</code>
    /// <summary>
    /// <para>Unlock a spin lock by setting it to 0.</para>
    /// <para>Always returns immediately.</para>
    /// <para><b>Please note that spinlocks are dangerous if you don't know what you're
    /// doing. Please be careful using any sort of spinlock!</b></para>
    /// </summary>
    /// <param name="lock">a pointer to a lock variable.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockSpinlock"/>
    /// <seealso cref="TryLockSpinlock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockSpinlock"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockSpinlock(ref int @lock);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_MemoryBarrierReleaseFunction(void);</code>
    /// <summary>
    /// <para>Insert a memory release barrier (function version).</para>
    /// <para>Please refer to SDL_MemoryBarrierRelease for details. This is a function
    /// version, which might be useful if you need to use this functionality from a
    /// scripting language, etc. Also, some of the macro versions call this
    /// function behind the scenes, where more heavy lifting can happen inside of
    /// SDL. Generally, though, an app written in C/C++/etc should use the macro
    /// version, as it will be more efficient.</para>
    /// </summary>
    /// <threadsafety>Obviously this function is safe to use from any thread at any
    /// time, but if you find yourself needing this, you are probably
    /// dealing with some very sensitive code; be careful!</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MemoryBarrierRelease"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MemoryBarrierReleaseFunction"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MemoryBarrierReleaseFunction();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_MemoryBarrierAcquireFunction(void);</code>
    /// <summary>
    /// <para>Insert a memory acquire barrier (function version).</para>
    /// <para>Please refer to SDL_MemoryBarrierRelease for details. This is a function
    /// version, which might be useful if you need to use this functionality from a
    /// scripting language, etc. Also, some of the macro versions call this
    /// function behind the scenes, where more heavy lifting can happen inside of
    /// SDL. Generally, though, an app written in C/C++/etc should use the macro
    /// version, as it will be more efficient.</para>
    /// </summary>
    /// <threadsafety>Obviously this function is safe to use from any thread at any
    /// time, but if you find yourself needing this, you are probably
    /// dealing with some very sensitive code; be careful!</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="MemoryBarrierAcquire"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MemoryBarrierAcquireFunction"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MemoryBarrierAcquireFunction();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CompareAndSwapAtomicInt(SDL_AtomicInt *a, int oldval, int newval);</code>
    /// <summary>
    /// <para>Set an atomic variable to a new value if it is currently an old value.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt"/> variable to be modified.</param>
    /// <param name="oldval">the old value.</param>
    /// <param name="newval">the new value.</param>
    /// <returns><c>true</c> if the atomic variable was set, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAtomicInt"/>
    /// <seealso cref="SetAtomicInt"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CompareAndSwapAtomicInt"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CompareAndSwapAtomicInt(ref AtomicInt a, int oldval, int newval);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetAtomicInt(SDL_AtomicInt *a, int v);</code>
    /// <summary>
    /// <para>Get the value of an atomic variable.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt"/> variable.</param>
    /// <param name="v">the current value of an atomic variable.</param>
    /// <returns></returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAtomicInt"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAtomicInt"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetAtomicInt(ref AtomicInt a, int v);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetAtomicInt(SDL_AtomicInt *a);</code>
    /// <summary>
    /// <para>Get the value of an atomic variable.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt"/> variable.</param>
    /// <returns>the current value of an atomic variable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAtomicInt"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAtomicInt"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAtomicInt(ref AtomicInt a);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_AddAtomicInt(SDL_AtomicInt *a, int v);</code>
    /// <summary>
    /// <para>Add to an atomic variable.</para>
    /// <para>This function also acts as a full memory barrier.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an SDL_AtomicInt variable to be modified.</param>
    /// <param name="v">the desired value to add.</param>
    /// <returns>the previous value of the atomic variable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AtomicDecRef"/>
    /// <seealso cref="AtomicIncRef"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddAtomicInt"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int AddAtomicInt(ref AtomicInt a, int v);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CompareAndSwapAtomicU32(SDL_AtomicU32 *a, Uint32 oldval, Uint32 newval);</code>
    /// <summary>
    /// <para>Set an atomic variable to a new value if it is currently an old value.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicU32"/> variable to be modified.</param>
    /// <param name="oldval">the old value.</param>
    /// <param name="newval">the new value.</param>
    /// <returns><c>true</c> if the atomic variable was set, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAtomicU32"/>
    /// <seealso cref="SetAtomicU32"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CompareAndSwapAtomicU32"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldval, uint newval);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_SetAtomicU32(SDL_AtomicU32 *a, Uint32 v);</code>
    /// <summary>
    /// <para>Set an atomic variable to a value.</para>
    /// <para>This function also acts as a full memory barrier.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an SDL_AtomicU32 variable to be modified.</param>
    /// <param name="v">the desired value.</param>
    /// <returns>the previous value of the atomic variable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAtomicU32"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAtomicU32"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SetAtomicU32(ref AtomicU32 a, uint v);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GetAtomicU32(SDL_AtomicU32 *a);</code>
    /// <summary>
    /// <para>Get the value of an atomic variable.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicU32"/> variable.</param>
    /// <returns>the current value of an atomic variable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAtomicU32"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAtomicU32"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetAtomicU32(ref AtomicU32 a);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_AddAtomicU32(SDL_AtomicU32 *a, int v);</code>
    /// <summary>
    /// <para>Add to an atomic variable.</para>
    /// <para>This function also acts as a full memory barrier.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicU32"/> variable to be modified.</param>
    /// <param name="v">the desired value to add or subtract.</param>
    /// <returns>the previous value of the atomic variable.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AddAtomicU32"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint AddAtomicU32(ref AtomicU32 a, int v);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CompareAndSwapAtomicPointer(void **a, void *oldval, void *newval);</code>
    /// <summary>
    /// <para>Set a pointer to a new value if it is currently an old value.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to a pointer.</param>
    /// <param name="oldval">the old pointer value.</param>
    /// <param name="newval">the new pointer value.</param>
    /// <returns><c>true</c> if the pointer was set, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CompareAndSwapAtomicInt"/>
    /// <seealso cref="GetAtomicPointer"/>
    /// <seealso cref="SetAtomicPointer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CompareAndSwapAtomicPointer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CompareAndSwapAtomicPointer(ref IntPtr a, IntPtr oldval, IntPtr newval);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_SetAtomicPointer(void **a, void *v);</code>
    /// <summary>
    /// <para>Set a pointer to a value atomically.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to a pointer.</param>
    /// <param name="v">the desired pointer value.</param>
    /// <returns>the previous value of the pointer.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CompareAndSwapAtomicPointer"/>
    /// <seealso cref="GetAtomicPointer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAtomicPointer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr SetAtomicPointer(ref IntPtr a, IntPtr v);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetAtomicPointer(void **a);</code>
    /// <summary>
    /// <para>Get the value of a pointer atomically.</para>
    /// <para><b>Note: If you don't know what this function is for, you shouldn't use
    /// it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to a pointer.</param>
    /// <returns>the current value of a pointer.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CompareAndSwapAtomicPointer"/>
    /// <seealso cref="SetAtomicPointer"/>
    [LibraryImport(SDLLibrary, EntryPoint = "GetAtomicPointer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetAtomicPointer(ref IntPtr a);
}