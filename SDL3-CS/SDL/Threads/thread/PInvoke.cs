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
	/// <code>extern SDL_DECLSPEC SDL_Thread * SDLCALL SDL_CreateThread(SDL_ThreadFunction fn, const char *name, void *data);</code>
	/// <summary>
	/// <para>Create a new thread with a default stack size.</para>
	/// <para>This is a convenience function, equivalent to calling
	/// <see cref="CreateThreadWithProperties"/> with the following properties set:</para>
	/// <list type="bullet">
	///	<item><see cref="Props.ThreadCreateEntryFunctionPointer"/>: <c>fn</c></item>
	/// <item><see cref="Props.ThreadCreateNameString"/>: <c>name</c></item>
	/// <item><see cref="Props.ThreadCreateUserdataPointer"/>: <c>data</c></item>
	/// </list>
	/// <para>Note that this "function" is actually a macro that calls an internal
	/// function with two extra parameters not listed here; they are hidden through
	/// preprocessor macros and are needed to support various C runtimes at the
	/// point of the function call. Language bindings that aren't using the C
	/// headers will need to deal with this.</para>
	/// <para>Usually, apps should just call this function the same way on every platform
	/// and let the macros hide the details.</para>
	/// </summary>
	/// <param name="fn">the <see cref="ThreadFunction"/> function to call in the new thread.</param>
	/// <param name="name">the name of the thread.</param>
	/// <param name="data">a pointer that is passed to <c>fn</c>.</param>
	/// <returns>an opaque pointer to the new thread object on success, <c>null</c> if the
	/// new thread could not be created; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="CreateThreadWithProperties"/>
	/// <seealso cref="WaitThread"/>
	public static IntPtr CreateThread(ThreadFunction fn, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, IntPtr data) => 
		CreateThreadRuntime(fn, name, data, null, null);


	/// <code>extern SDL_DECLSPEC SDL_Thread * SDLCALL SDL_CreateThreadWithProperties(SDL_PropertiesID props);</code>
	/// <summary>
	/// <para>Create a new thread with with the specified properties.</para>
	/// <para>These are the supported properties:</para>
	/// <list type="bullet">
	///	<item><see cref="Props.ThreadCreateEntryFunctionPointer"/>: an SDL_ThreadFunction
	/// value that will be called at the start of the new thread's life.
	/// Required.</item>
	/// <item><see cref="Props.ThreadCreateNameString"/>: the name of the new thread, which
	/// might be available to debuggers. Optional, defaults to <c>null</c>.</item>
	/// <item><see cref="Props.ThreadCreateUserdataPointer"/>: an arbitrary app-defined
	/// pointer, which is passed to the entry function on the new thread, as its
	/// only parameter. Optional, defaults to <c>null</c>.</item>
	/// <item><see cref="Props.ThreadCreateStacksizeNumber"/>: the size, in bytes, of the new
	/// thread's stack. Optional, defaults to 0 (system-defined default).</item>
	/// </list>
	/// <para>SDL makes an attempt to report <see cref="Props.ThreadCreateNameString"/> to the
	/// system, so that debuggers can display it. Not all platforms support this.</para>
	/// <para>Thread naming is a little complicated: Most systems have very small limits
	/// for the string length (Haiku has 32 bytes, Linux currently has 16, Visual
	/// C++ 6.0 has _nine_!), and possibly other arbitrary rules. You'll have to
	/// see what happens with your system's debugger. The name should be UTF-8 (but
	/// using the naming limits of C identifiers is a better bet). There are no
	/// requirements for thread naming conventions, so long as the string is
	/// null-terminated UTF-8, but these guidelines are helpful in choosing a name:</para>
	/// <para>https://stackoverflow.com/questions/149932/naming-conventions-for-threads</para>
	/// <para>If a system imposes requirements, SDL will try to munge the string for it
	/// (truncate, etc), but the original string contents will be available from
	/// <see cref="GetThreadName"/>.</para>
	/// <para>The size (in bytes) of the new stack can be specified with
	/// <see cref="Props.ThreadCreateStacksizeNumber"/>. Zero means "use the system
	/// default" which might be wildly different between platforms. x86 Linux
	/// generally defaults to eight megabytes, an embedded device might be a few
	/// kilobytes instead. You generally need to specify a stack that is a multiple
	/// of the system's page size (in many cases, this is 4 kilobytes, but check
	/// your system documentation).</para>
	/// <para>Note that this "function" is actually a macro that calls an internal
	/// function with two extra parameters not listed here; they are hidden through
	/// preprocessor macros and are needed to support various C runtimes at the
	/// point of the function call. Language bindings that aren't using the C
	/// headers will need to deal with this.</para>
	/// <para>The actual symbol in SDL is <see cref="CreateThreadWithPropertiesRuntime"/>, so
	/// there is no symbol clash, but trying to load an SDL shared library and look
	/// for <see cref="CreateThreadWithProperties"/> will fail.</para>
	/// <para>Usually, apps should just call this function the same way on every platform
	/// and let the macros hide the details.</para>
	/// </summary>
	/// <param name="props">the properties to use.</param>
	/// <returns>an opaque pointer to the new thread object on success, <c>null</c> if the
	/// new thread could not be created; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="CreateThread"/>
	/// <seealso cref="WaitThread"/>
	public static IntPtr CreateThreadWithProperties(uint props) =>
		CreateThreadWithPropertiesRuntime(props, null, null);
	
	
	/// <code>extern SDL_DECLSPEC SDL_Thread * SDLCALL SDL_CreateThreadRuntime(SDL_ThreadFunction fn, const char *name, void *data, SDL_FunctionPointer pfnBeginThread, SDL_FunctionPointer pfnEndThread);</code>
	/// <summary>
	/// The actual entry point for SDL_CreateThread.
	/// </summary>
	/// <param name="fn">the <see cref="ThreadFunction"/> function to call in the new thread</param>
	/// <param name="name">the name of the thread</param>
	/// <param name="data">a pointer that is passed to <c>fn</c></param>
	/// <param name="pfnBeginThread">the C runtime's _beginthreadex (or whatnot). Can be <c>null</c>.</param>
	/// <param name="pfnEndThread">the C runtime's _endthreadex (or whatnot). Can be <c>null</c>.</param>
	/// <returns>an opaque pointer to the new thread object on success, <c>null</c> if the
	/// new thread could not be created; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateThreadRuntime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial IntPtr CreateThreadRuntime(ThreadFunction fn, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, IntPtr data, FunctionPointer? pfnBeginThread, FunctionPointer? pfnEndThread);
	
	
	/// <code>extern SDL_DECLSPEC SDL_Thread * SDLCALL SDL_CreateThreadWithPropertiesRuntime(SDL_PropertiesID props, SDL_FunctionPointer pfnBeginThread, SDL_FunctionPointer pfnEndThread);</code>
	/// <summary>
	/// The actual entry point for <see cref="CreateThreadWithProperties"/>.
	/// </summary>
	/// <param name="props">the properties to use</param>
	/// <param name="pfnBeginThread">the C runtime's _beginthreadex (or whatnot). Can be <c>null</c>.</param>
	/// <param name="pfnEndThread">the C runtime's _endthreadex (or whatnot). Can be <c>null</c>.</param>
	/// <returns>an opaque pointer to the new thread object on success, <c>null</c> if the
	/// new thread could not be created; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateThreadWithPropertiesRuntime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial IntPtr CreateThreadWithPropertiesRuntime(uint props, FunctionPointer? pfnBeginThread, FunctionPointer? pfnEndThread);
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetThreadName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetThreadName(IntPtr thread);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetThreadName(SDL_Thread *thread);</code>
	/// <summary>
	/// Get the thread name as it was specified in <see cref="CreateThread"/>.
	/// </summary>
	/// <param name="thread">the thread to query.</param>
	/// <returns>a pointer to a UTF-8 string that names the specified thread, or
	/// <c>null</c> if it doesn't have a name.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	public static string? GetThreadName(IntPtr thread)
	{
		var value = SDL_GetThreadName(thread); 
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}
	
	
	/// <code>extern SDL_DECLSPEC SDL_ThreadID SDLCALL SDL_GetCurrentThreadID(void);</code>
	/// <summary>
	/// <para>Get the thread identifier for the current thread.</para>
	/// <para>This thread identifier is as reported by the underlying operating system.
	/// If SDL is running on a platform that does not support threads the return
	/// value will always be zero.</para>
	/// <para>This function also returns a valid thread ID when called from the main
	/// thread.</para>
	/// </summary>
	/// <returns>the ID of the current thread.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetThreadID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentThreadID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ulong GetCurrentThreadID();
	
	
	/// <code>extern SDL_DECLSPEC SDL_ThreadID SDLCALL SDL_GetThreadID(SDL_Thread *thread);</code>
	/// <summary>
	/// <para>Get the thread identifier for the specified thread.</para>
	/// <para>This thread identifier is as reported by the underlying operating system.
	/// If SDL is running on a platform that does not support threads the return
	/// value will always be zero.</para>
	/// </summary>
	/// <param name="thread">the thread to query.</param>
	/// <returns>the ID of the specified thread, or the ID of the current thread if
	/// <c>thread</c> is <c>null</c>.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetCurrentThreadID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetThreadID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ulong GetThreadID(IntPtr thread);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetCurrentThreadPriority(SDL_ThreadPriority priority);</code>
	/// <summary>
	/// <para>Set the priority for the current thread.</para>
	/// <para>Note that some platforms will not let you alter the priority (or at least,
	/// promote the thread to a higher priority) at all, and some require you to be
	/// an administrator account. Be prepared for this to fail.</para>
	/// </summary>
	/// <param name="priority">the <see cref="ThreadPriority"/> to set.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetCurrentThreadPriority"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetCurrentThreadPriority(ThreadPriority priority);
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_WaitThread(SDL_Thread *thread, int *status);</code>
	/// <summary>
	/// <para>Wait for a thread to finish.</para>
	/// <para>Threads that haven't been detached will remain until this function cleans
	/// them up. Not doing so is a resource leak.</para>
	/// <para>Once a thread has been cleaned up through this function, the SDL_Thread
	/// that references it becomes invalid and should not be referenced again. As
	/// such, only one thread may call <see cref="WaitThread"/> on another.</para>
	/// <para>The return code from the thread function is placed in the area pointed to
	/// by <c>status</c>, if <c>status</c> is not <c>null</c>.</para>
	/// <para>You may not wait on a thread that has been used in a call to
	/// <see cref="DetachThread"/>. Use either that function or this one, but not both, or
	/// behavior is undefined.</para>
	/// <para>It is safe to pass a <c>null</c> thread to this function; it is a no-op.</para>
	/// <para>Note that the thread pointer is freed by this function and is not valid
	/// afterward.</para>
	/// </summary>
	/// <param name="thread">the SDL_Thread pointer that was returned from the
	/// <see cref="CreateThread"/> call that started this thread.</param>
	/// <param name="status">a pointer filled in with the value returned from the thread
	/// function by its <c>return</c>, or -1 if the thread has been
	/// detached or isn't valid, may be <c>null</c>.</param>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="CreateThread"/>
	/// <seealso cref="DetachThread"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitThread"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void WaitThread(IntPtr thread, out int status);
	
	
	/// <code>extern SDL_DECLSPEC SDL_ThreadState SDLCALL SDL_GetThreadState(SDL_Thread *thread);</code>
	/// <summary>
	/// Get the current state of a thread.
	/// </summary>
	/// <param name="thread">the thread to query.</param>
	/// <returns>the current state of a thread, or <see cref="ThreadState.Unknown"/> if the thread
	/// isn't valid.</returns>
	/// <since>This function is available since SDL 3.1.8.</since>
	/// <seealso cref="ThreadState"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetThreadState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ThreadState GetThreadState(IntPtr thread);
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_DetachThread(SDL_Thread *thread);</code>
	/// <summary>
	/// <para>Let a thread clean up on exit without intervention.</para>
	/// <para>A thread may be "detached" to signify that it should not remain until
	/// another thread has called <see cref="WaitThread"/> on it. Detaching a thread is
	/// useful for long-running threads that nothing needs to synchronize with or
	/// further manage. When a detached thread is done, it simply goes away.</para>
	/// <para>There is no way to recover the return code of a detached thread. If you
	/// need this, don't detach the thread and instead use <see cref="WaitThread"/>.</para>
	/// <para>Once a thread is detached, you should usually assume the SDL_Thread isn't
	/// safe to reference again, as it will become invalid immediately upon the
	/// detached thread's exit, instead of remaining until someone has called
	/// <see cref="WaitThread"/> to finally clean it up. As such, don't detach the same
	/// thread more than once.</para>
	/// <para>If a thread has already exited when passed to <see cref="DetachThread"/>, it will
	/// stop waiting for a call to <see cref="WaitThread"/> and clean up immediately. It is
	/// not safe to detach a thread that might be used with <see cref="WaitThread"/>.</para>
	/// <para>You may not call <see cref="WaitThread"/> on a thread that has been detached. Use
	/// either that function or this one, but not both, or behavior is undefined.</para>
	/// <para>It is safe to pass <c>null</c> to this function; it is a no-op.</para>
	/// </summary>
	/// <param name="thread">the SDL_Thread pointer that was returned from the
	/// <see cref="CreateThread"/> call that started this thread.</param>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="CreateThread"/>
	/// <seealso cref="WaitThread"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_DetachThread"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void DetachThread(IntPtr thread);
	
	
	/// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetTLS(SDL_TLSID *id);</code>
	/// <summary>
	/// <para>Get the current thread's value associated with a thread local storage ID.</para>
	/// </summary>
	/// <param name="id">a pointer to the thread local storage ID, may not be <c>null</c>.</param>
	/// <returns>the value associated with the ID for the current thread or <c>null</c> if
	/// no value has been set; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="SetTLS"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTLS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial IntPtr GetTLS(IntPtr id);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetTLS(SDL_TLSID *id, const void *value, SDL_TLSDestructorCallback destructor);</code>
	/// <summary>
	/// <para>Set the current thread's value associated with a thread local storage ID.</para>
	/// <para>If the thread local storage ID is not initialized (the value is 0), a new
	/// ID will be created in a thread-safe way, so all calls using a pointer to
	/// the same ID will refer to the same local storage.</para>
	/// <para>Note that replacing a value from a previous call to this function on the
	/// same thread does _not_ call the previous value's destructor!</para>
	/// <para><c>destructor</c> can be <c>null</c>; it is assumed that <c>value</c> does not need to be
	/// cleaned up if so.</para>
	/// </summary>
	/// <param name="id">a pointer to the thread local storage ID, may not be <c>null</c>.</param>
	/// <param name="value">the value to associate with the ID for the current thread.</param>
	/// <param name="destructor">a function called when the thread exits, to free the
	/// value, may be <c>null</c>.</param>
	/// <returns><c>null</c> on success or <c>null</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetTLS"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTLS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetTLS(IntPtr id, IntPtr value, TLSDestructorCallback destructor);
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_CleanupTLS(void);</code>
	/// <summary>
	/// <para>Cleanup all TLS data for this thread.</para>
	/// <para>If you are creating your threads outside of SDL and then calling SDL
	/// functions, you should call this function before your thread exits, to
	/// properly clean up SDL memory.</para>
	/// </summary>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_CleanupTLS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void CleanupTLS();
}