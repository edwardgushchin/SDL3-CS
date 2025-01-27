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
    /// <code>extern SDL_DECLSPEC SDL_Mutex * SDLCALL SDL_CreateMutex(void);</code>
    /// <summary>
    /// <para>Create a new mutex.</para>
    /// <para>All newly-created mutexes begin in the _unlocked_ state.</para>
    /// <para>Calls to <see cref="LockMutex"/> will not return while the mutex is locked by
    /// another thread. See <see cref="TryLockMutex"/> to attempt to lock without blocking.</para>
    /// <para>SDL mutexes are reentrant.</para>
    /// </summary>
    /// <returns>the initialized and unlocked mutex or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyMutex"/>
    /// <seealso cref="LockMutex"/>
    /// <seealso cref="TryLockMutex"/>
    /// <seealso cref="UnlockMutex"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateMutex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateMutex();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockMutex(SDL_Mutex *mutex) SDL_ACQUIRE(mutex);</code>
    /// <summary>
    /// <para>Lock the mutex.</para>
    /// <para>This will block until the mutex is available, which is to say it is in the
    /// unlocked state and the OS has chosen the caller as the next thread to lock
    /// it. Of all threads waiting to lock the mutex, only one may do so at a time.</para>
    /// <para>It is legal for the owning thread to lock an already-locked mutex. It must
    /// unlock it the same number of times before it is actually made available for
    /// other threads in the system (this is known as a "recursive mutex").</para>
    /// <para>This function does not fail; if mutex is <c>null</c>, it will return immediately
    /// having locked nothing. If the mutex is valid, this function will always
    /// block until it can lock the mutex, and return with it locked.</para>
    /// </summary>
    /// <param name="mutex">the mutex to lock.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="TryLockMutex"/>
    /// <seealso cref="UnlockMutex"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockMutex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LockMutex(IntPtr mutex);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TryLockMutex(SDL_Mutex *mutex) SDL_TRY_ACQUIRE(0, mutex);</code>
    /// <summary>
    /// <para>Try to lock a mutex without blocking.</para>
    /// <para>This works just like <see cref="LockMutex"/>, but if the mutex is not available,
    /// this function returns false immediately.</para>
    /// <para>This technique is useful if you need exclusive access to a resource but
    /// don't want to wait for it, and will return to it to try again later.</para>
    /// <para>This function returns true if passed a <c>null</c> mutex.</para>
    /// </summary>
    /// <param name="mutex">the mutex to try to lock.</param>
    /// <returns><c>null</c> on success, <c>null</c> if the mutex would block.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockMutex"/>
    /// <seealso cref="UnlockMutex"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TryLockMutex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TryLockMutex(IntPtr mutex);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockMutex(SDL_Mutex *mutex) SDL_RELEASE(mutex);</code>
    /// <summary>
    /// <para>Unlock the mutex.</para>
    /// <para>It is legal for the owning thread to lock an already-locked mutex. It must
    /// unlock it the same number of times before it is actually made available for
    /// other threads in the system (this is known as a "recursive mutex").</para>
    /// <para>It is illegal to unlock a mutex that has not been locked by the current
    /// thread, and doing so results in undefined behavior.</para>
    /// </summary>
    /// <param name="mutex">the mutex to unlock.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockMutex"/>
    /// <seealso cref="TryLockMutex"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockMutex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockMutex(IntPtr mutex);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyMutex(SDL_Mutex *mutex);</code>
    /// <summary>
    /// <para>Destroy a mutex created with <see cref="CreateMutex"/>.</para>
    /// <para>This function must be called on any mutex that is no longer needed. Failure
    /// to destroy a mutex will result in a system memory or resource leak. While
    /// it is safe to destroy a mutex that is _unlocked_, it is not safe to attempt
    /// to destroy a locked mutex, and may result in undefined behavior depending
    /// on the platform.</para>
    /// </summary>
    /// <param name="mutex">the mutex to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateMutex"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyMutex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyMutex(IntPtr mutex);
    
    
    /// <code>extern SDL_DECLSPEC SDL_RWLock * SDLCALL SDL_CreateRWLock(void);</code>
    /// <summary>
    /// <para>Create a new read/write lock.</para>
    /// <para>A read/write lock is useful for situations where you have multiple threads
    /// trying to access a resource that is rarely updated. All threads requesting
    /// a read-only lock will be allowed to run in parallel; if a thread requests a
    /// write lock, it will be provided exclusive access. This makes it safe for
    /// multiple threads to use a resource at the same time if they promise not to
    /// change it, and when it has to be changed, the rwlock will serve as a
    /// gateway to make sure those changes can be made safely.</para>
    /// <para>In the right situation, a rwlock can be more efficient than a mutex, which
    /// only lets a single thread proceed at a time, even if it won't be modifying
    /// the data.</para>
    /// <para>All newly-created read/write locks begin in the _unlocked_ state.</para>
    /// <para>Calls to <see cref="LockRWLockForReading"/> and <see cref="LockRWLockForWriting"/> will not
    /// return while the rwlock is locked _for writing_ by another thread. See
    /// <see cref="TryLockRWLockForReading"/> and <see cref="TryLockRWLockForWriting"/> to attempt
    /// to lock without blocking.</para>
    /// <para>SDL read/write locks are only recursive for read-only locks! They are not
    /// guaranteed to be fair, or provide access in a FIFO manner! They are not
    /// guaranteed to favor writers. You may not lock a rwlock for both read-only
    /// and write access at the same time from the same thread (so you can't
    /// promote your read-only lock to a write lock without unlocking first).</para>
    /// </summary>
    /// <returns>the initialized and unlocked read/write lock or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyRWLock"/>
    /// <seealso cref="LockRWLockForReading"/>
    /// <seealso cref="LockRWLockForWriting"/>
    /// <seealso cref="TryLockRWLockForReading"/>
    /// <seealso cref="TryLockRWLockForWriting"/>
    /// <seealso cref="UnlockRWLock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateRWLock"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateRWLock();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockRWLockForReading(SDL_RWLock *rwlock) SDL_ACQUIRE_SHARED(rwlock);</code>
    /// <summary>
    /// <para>Lock the read/write lock for _read only_ operations.</para>
    /// <para>This will block until the rwlock is available, which is to say it is not
    /// locked for writing by any other thread. Of all threads waiting to lock the
    /// rwlock, all may do so at the same time as long as they are requesting
    /// read-only access; if a thread wants to lock for writing, only one may do so
    /// at a time, and no other threads, read-only or not, may hold the lock at the
    /// same time.</para>
    /// <para>It is legal for the owning thread to lock an already-locked rwlock for
    /// reading. It must unlock it the same number of times before it is actually
    /// made available for other threads in the system (this is known as a
    /// "recursive rwlock").</para>
    /// <para>Note that locking for writing is not recursive (this is only available to
    /// read-only locks).</para>
    /// <para>It is illegal to request a read-only lock from a thread that already holds
    /// the write lock. Doing so results in undefined behavior. Unlock the write
    /// lock before requesting a read-only lock. (But, of course, if you have the
    /// write lock, you don't need further locks to read in any case.)</para>
    /// <para>This function does not fail; if rwlock is <c>null</c>, it will return immediately
    /// having locked nothing. If the rwlock is valid, this function will always
    /// block until it can lock the mutex, and return with it locked.</para>
    /// </summary>
    /// <param name="rwlock">the read/write lock to lock.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockRWLockForWriting"/>
    /// <seealso cref="TryLockRWLockForReading"/>
    /// <seealso cref="UnlockRWLock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockRWLockForReading"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LockRWLockForReading(IntPtr rwlock);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockRWLockForWriting(SDL_RWLock *rwlock) SDL_ACQUIRE(rwlock);</code>
    /// <summary>
    /// <para>Lock the read/write lock for _write_ operations.</para>
    /// <para>This will block until the rwlock is available, which is to say it is not
    /// locked for reading or writing by any other thread. Only one thread may hold
    /// the lock when it requests write access; all other threads, whether they
    /// also want to write or only want read-only access, must wait until the
    /// writer thread has released the lock.</para>
    /// <para>It is illegal for the owning thread to lock an already-locked rwlock for
    /// writing (read-only may be locked recursively, writing can not). Doing so
    /// results in undefined behavior.</para>
    /// <para>It is illegal to request a write lock from a thread that already holds a
    /// read-only lock. Doing so results in undefined behavior. Unlock the
    /// read-only lock before requesting a write lock.</para>
    /// <para>This function does not fail; if rwlock is <c>null</c>, it will return immediately
    /// having locked nothing. If the rwlock is valid, this function will always
    /// block until it can lock the mutex, and return with it locked.</para>
    /// </summary>
    /// <param name="rwlock">the read/write lock to lock.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockRWLockForReading"/>
    /// <seealso cref="TryLockRWLockForWriting"/>
    /// <seealso cref="UnlockRWLock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockRWLockForWriting"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LockRWLockForWriting(IntPtr rwlock);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TryLockRWLockForReading(SDL_RWLock *rwlock) SDL_TRY_ACQUIRE_SHARED(0, rwlock);</code>
    /// <summary>
    /// <para>Try to lock a read/write lock _for reading_ without blocking.</para>
    /// <para>This works just like <see cref="LockRWLockForReading"/>, but if the rwlock is not
    /// available, then this function returns false immediately.</para>
    /// <para>This technique is useful if you need access to a resource but don't want to
    /// wait for it, and will return to it to try again later.</para>
    /// <para>Trying to lock for read-only access can succeed if other threads are
    /// holding read-only locks, as this won't prevent access.</para>
    /// <para>This function returns true if passed a <c>null</c> rwlock.</para>
    /// </summary>
    /// <param name="rwlock">the rwlock to try to lock.</param>
    /// <returns><c>true</c> on success, <c>false</c> if the lock would block.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockRWLockForReading"/>
    /// <seealso cref="TryLockRWLockForWriting"/>
    /// <seealso cref="UnlockRWLock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TryLockRWLockForReading"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TryLockRWLockForReading(IntPtr rwlock);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TryLockRWLockForWriting(SDL_RWLock *rwlock) SDL_TRY_ACQUIRE(0, rwlock);</code>
    /// <summary>
    /// <para>Try to lock a read/write lock _for writing_ without blocking.</para>
    /// <para>This works just like <see cref="LockRWLockForWriting"/>, but if the rwlock is not
    /// available, then this function returns false immediately.</para>
    /// <para>This technique is useful if you need exclusive access to a resource but
    /// don't want to wait for it, and will return to it to try again later.</para>
    /// <para>It is illegal for the owning thread to lock an already-locked rwlock for
    /// writing (read-only may be locked recursively, writing can not). Doing so
    /// results in undefined behavior.</para>
    /// <para>It is illegal to request a write lock from a thread that already holds a
    /// read-only lock. Doing so results in undefined behavior. Unlock the
    /// read-only lock before requesting a write lock.</para>
    /// <para>This function returns true if passed a <c>null</c> rwlock.</para>
    /// </summary>
    /// <param name="rwlock">the rwlock to try to lock.</param>
    /// <returns><c>true</c> on success, <c>false</c> if the lock would block.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockRWLockForWriting"/>
    /// <seealso cref="TryLockRWLockForReading"/>
    /// <seealso cref="UnlockRWLock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TryLockRWLockForWriting"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TryLockRWLockForWriting(IntPtr rwlock);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockRWLock(SDL_RWLock *rwlock) SDL_RELEASE_GENERIC(rwlock);</code>
    /// <summary>
    /// <para>Unlock the read/write lock.</para>
    /// <para>Use this function to unlock the rwlock, whether it was locked for read-only
    /// or write operations.</para>
    /// <para>It is legal for the owning thread to lock an already-locked read-only lock.
    /// It must unlock it the same number of times before it is actually made
    /// available for other threads in the system (this is known as a "recursive
    /// rwlock").</para>
    /// <para>It is illegal to unlock a rwlock that has not been locked by the current
    /// thread, and doing so results in undefined behavior.</para>
    /// </summary>
    /// <param name="rwlock">the rwlock to unlock.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockRWLockForReading"/>
    /// <seealso cref="LockRWLockForWriting"/>
    /// <seealso cref="TryLockRWLockForReading"/>
    /// <seealso cref="TryLockRWLockForWriting"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockRWLock"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockRWLock(IntPtr rwlock);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyRWLock(SDL_RWLock *rwlock);</code>
    /// <summary>
    /// <para>Destroy a read/write lock created with <see cref="CreateRWLock"/>.</para>
    /// <para>This function must be called on any read/write lock that is no longer
    /// needed. Failure to destroy a rwlock will result in a system memory or
    /// resource leak. While it is safe to destroy a rwlock that is _unlocked_, it
    /// is not safe to attempt to destroy a locked rwlock, and may result in
    /// undefined behavior depending on the platform.</para>
    /// </summary>
    /// <param name="rwlock">the rwlock to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateRWLock"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyRWLock"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyRWLock(IntPtr rwlock);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Semaphore * SDLCALL SDL_CreateSemaphore(Uint32 initial_value);</code>
    /// <summary>
    /// <para>Create a semaphore.</para>
    /// <para>This function creates a new semaphore and initializes it with the value
    /// <c>initialValue</c>. Each wait operation on the semaphore will atomically
    /// decrement the semaphore value and potentially block if the semaphore value
    /// is 0. Each post operation will atomically increment the semaphore value and
    /// wake waiting threads and allow them to retry the wait operation.</para>
    /// </summary>
    /// <param name="initialValue">the starting value of the semaphore.</param>
    /// <returns>a new semaphore or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroySemaphore"/>
    /// <seealso cref="SignalSemaphore"/>
    /// <seealso cref="TryWaitSemaphore"/>
    /// <seealso cref="GetSemaphoreValue"/>
    /// <seealso cref="WaitSemaphore"/>
    /// <seealso cref="WaitSemaphoreTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateSemaphore"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSemaphore(uint initialValue);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroySemaphore(SDL_Semaphore *sem);</code>
    /// <summary>
    /// <para>Destroy a semaphore.</para>
    /// <para>It is not safe to destroy a semaphore if there are threads currently
    /// waiting on it.</para>
    /// </summary>
    /// <param name="sem">the semaphore to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateSemaphore"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroySemaphore"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroySemaphore(IntPtr sem);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_WaitSemaphore(SDL_Semaphore *sem);</code>
    /// <summary>
    /// <para>Wait until a semaphore has a positive value and then decrements it.</para>
    /// <para>This function suspends the calling thread until the semaphore pointed to by
    /// <c>sem</c> has a positive value, and then atomically decrement the semaphore
    /// value.</para>
    /// <para>This function is the equivalent of calling <see cref="WaitSemaphoreTimeout"/> with
    /// a time length of -1.</para>
    /// </summary>
    /// <param name="sem">the semaphore wait on.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SignalSemaphore"/>
    /// <seealso cref="TryWaitSemaphore"/>
    /// <seealso cref="WaitSemaphoreTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitSemaphore"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitSemaphore(IntPtr sem);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_TryWaitSemaphore(SDL_Semaphore *sem);</code>
    /// <summary>
    /// <para>See if a semaphore has a positive value and decrement it if it does.</para>
    /// <para>This function checks to see if the semaphore pointed to by <c>sem</c> has a
    /// positive value and atomically decrements the semaphore value if it does. If
    /// the semaphore doesn't have a positive value, the function immediately
    /// returns false.</para>
    /// </summary>
    /// <param name="sem">the semaphore to wait on.</param>
    /// <returns><c>true</c> if the wait succeeds, <c>false</c> if the wait would block.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SignalSemaphore"/>
    /// <seealso cref="WaitSemaphore"/>
    /// <seealso cref="WaitSemaphoreTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TryWaitSemaphore"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TryWaitSemaphore(IntPtr sem);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitSemaphoreTimeout(SDL_Semaphore *sem, Sint32 timeoutMS);</code>
    /// <summary>
    /// <para>Wait until a semaphore has a positive value and then decrements it.</para>
    /// <para>This function suspends the calling thread until either the semaphore
    /// pointed to by <c>sem</c> has a positive value or the specified time has elapsed.
    /// If the call is successful it will atomically decrement the semaphore value.</para>
    /// </summary>
    /// <param name="sem">the semaphore to wait on.</param>
    /// <param name="timeoutMS">the length of the timeout, in milliseconds, or -1 to wait
    /// indefinitely.</param>
    /// <returns><c>true</c> if the wait succeeds or <c>false</c> if the wait times out.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SignalSemaphore"/>
    /// <seealso cref="TryWaitSemaphore"/>
    /// <seealso cref="WaitSemaphore"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitSemaphoreTimeout"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitSemaphoreTimeout(IntPtr sem, int timeoutMS);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SignalSemaphore(SDL_Semaphore *sem);</code>
    /// <summary>
    /// Atomically increment a semaphore's value and wake waiting threads.
    /// </summary>
    /// <param name="sem">the semaphore to increment.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="TryWaitSemaphore"/>
    /// <seealso cref="WaitSemaphore"/>
    /// <seealso cref="WaitSemaphoreTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SignalSemaphore"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SignalSemaphore(IntPtr sem);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GetSemaphoreValue(SDL_Semaphore *sem);</code>
    /// <summary>
    /// Get the current value of a semaphore.
    /// </summary>
    /// <param name="sem">the semaphore to query.</param>
    /// <returns>the current value of the semaphore.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSemaphoreValue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetSemaphoreValue(IntPtr sem);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Condition * SDLCALL SDL_CreateCondition(void);</code>
    /// <summary>
    /// Create a condition variable.
    /// </summary>
    /// <returns>a new condition variable or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    /// <seealso cref="DestroyCondition"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateCondition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateCondition();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyCondition(SDL_Condition *cond);</code>
    /// <summary>
    /// <para>Destroy a condition variable.</para>
    /// </summary>
    /// <param name="cond">the condition variable to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateCondition"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyCondition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyCondition(IntPtr cond);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SignalCondition(SDL_Condition *cond);</code>
    /// <summary>
    /// Restart one of the threads that are waiting on the condition variable.
    /// </summary>
    /// <param name="cond">the condition variable to signal.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SignalCondition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SignalCondition(IntPtr cond);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_BroadcastCondition(SDL_Condition *cond);</code>
    /// <summary>
    /// Restart all threads that are waiting on the condition variable.
    /// </summary>
    /// <param name="cond">the condition variable to signal.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BroadcastCondition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void BroadcastCondition(IntPtr cond);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_WaitCondition(SDL_Condition *cond, SDL_Mutex *mutex);</code>
    /// <summary>
    /// <para>Wait until a condition variable is signaled.</para>
    /// <para>This function unlocks the specified <c>mutex</c> and waits for another thread to
    /// call <see cref="SignalCondition"/> or <see cref="BroadcastCondition"/> on the condition
    /// variable <c>cond</c>. Once the condition variable is signaled, the mutex isre-locked and the function returns.</para>
    /// <para>The mutex must be locked before calling this function. Locking the mutex
    /// recursively (more than once) is not supported and leads to undefined
    /// behavior.</para>
    /// <para>This function is the equivalent of calling <see cref="WaitConditionTimeout"/> with
    /// a time length of -1.</para>
    /// </summary>
    /// <param name="cond">the condition variable to wait on.</param>
    /// <param name="mutex">the mutex used to coordinate thread access.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitConditionTimeout"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitCondition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitCondition(IntPtr cond, IntPtr mutex);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitConditionTimeout(SDL_Condition *cond, SDL_Mutex *mutex, Sint32 timeoutMS);</code>
    /// <summary>
    /// <para>Wait until a condition variable is signaled or a certain time has passed.</para>
    /// <para>This function unlocks the specified <c>mutex</c> and waits for another thread to
    /// call <see cref="SignalCondition"/> or <see cref="BroadcastCondition"/> on the condition
    /// variable <c>cond</c>, or for the specified time to elapse. Once the condition
    /// variable is signaled or the time elapsed, the mutex is re-locked and the
    /// function returns.</para>
    /// <para>The mutex must be locked before calling this function. Locking the mutex
    /// recursively (more than once) is not supported and leads to undefined
    /// behavior.</para>
    /// </summary>
    /// <param name="cond">the condition variable to wait on.</param>
    /// <param name="mutex">the mutex used to coordinate thread access.</param>
    /// <param name="timeoutMS">the maximum time to wait, in milliseconds, or -1 to wait
    /// indefinitely.</param>
    /// <returns><c>true</c> if the condition variable is signaled, <c>false</c> if the condition
    /// is not signaled in the allotted time.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BroadcastCondition"/>
    /// <seealso cref="SignalCondition"/>
    /// <seealso cref="WaitCondition"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitConditionTimeout"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitConditionTimeout(IntPtr cond, IntPtr mutex, int timeoutMS);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShouldInit(SDL_InitState *state);</code>
    /// <summary>
    /// <para>Return whether initialization should be done.</para>
    /// <para>This function checks the passed in state and if initialization should be
    /// done, sets the status to <see cref="InitStatus.Initializing"/> and returns true.
    /// If another thread is already modifying this state, it will wait until
    /// that's done before returning.</para>
    /// <para>If this function returns true, the calling code must call
    /// <see cref="SetInitialized"/> to complete the initialization.</para>
    /// </summary>
    /// <param name="state">the initialization state to check.</param>
    /// <returns><c>true</c> if initialization needs to be done, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetInitialized"/>
    /// <seealso cref="ShouldQuit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShouldInit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ShouldInit(ref InitState state);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ShouldQuit(SDL_InitState *state);</code>
    /// <summary>
    /// <para>Return whether cleanup should be done.</para>
    /// <para>This function checks the passed in state and if cleanup should be done,
    /// sets the status to <see cref="InitStatus.UnInitializing"/> and returns true.</para>
    /// <para>If this function returns true, the calling code must call
    /// <see cref="SetInitialized"/> to complete the cleanup.</para>
    /// </summary>
    /// <param name="state">the initialization state to check.</param>
    /// <returns><c>true</c> if cleanup needs to be done, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetInitialized"/>
    /// <seealso cref="ShouldInit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShouldQuit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ShouldQuit(ref InitState state);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetInitialized(SDL_InitState *state, bool initialized);</code>
    /// <summary>
    /// <para>Finish an initialization state transition.</para>
    /// <para>This function sets the status of the passed in state to
    /// <see cref="InitStatus.Initialized"/> or <see cref="InitStatus.UnInitialized"/> and allows
    /// any threads waiting for the status to proceed.</para>
    /// </summary>
    /// <param name="state">the initialization state to check.</param>
    /// <param name="initialized">the new initialization state.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ShouldInit"/>
    /// <seealso cref="ShouldQuit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetInitialized"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(ref InitState state, [MarshalAs(UnmanagedType.I1)] bool initialized);
}