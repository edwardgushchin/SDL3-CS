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
    /// <code>extern SDL_DECLSPEC SDL_AsyncIO * SDLCALL SDL_AsyncIOFromFile(const char *file, const char *mode);</code>
    /// <summary>
    /// <para>Use this function to create a new SDL_AsyncIO object for reading from
    /// and/or writing to a named file.</para>
    /// <para>The <c>mode</c> string understands the following values:</para>
    /// <list type="bullet">
    /// <item>"r": Open a file for reading only. It must exist.</item>
    /// <item>"w": Open a file for writing only. It will create missing files or
    /// truncate existing ones.</item>
    /// <item>"r+": Open a file for update both reading and writing. The file must
    /// exist.</item>
    /// <item>"w+": Create an empty file for both reading and writing. If a file with
    /// the same name already exists its content is erased and the file is
    /// treated as a new empty file.</item>
    /// </list>
    /// <para>There is no "b" mode, as there is only "binary" style I/O, and no "a" mode
    /// for appending, since you specify the position when starting a task.</para>
    /// <para>This function supports Unicode filenames, but they must be encoded in UTF-8
    /// format, regardless of the underlying operating system.</para>
    /// <para>This call is _not_ asynchronous; it will open the file before returning,
    /// under the assumption that doing so is generally a fast operation. Future
    /// reads and writes to the opened file will be async, however.</para>
    /// </summary>
    /// <param name="file">a UTF-8 string representing the filename to open.</param>
    /// <param name="mode">an ASCII string representing the mode to be used for opening
    /// the file.</param>
    /// <returns>a pointer to the SDL_AsyncIO structure that is created or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CloseAsyncIO"/>
    /// <seealso cref="ReadAsyncIO"/>
    /// <seealso cref="WriteAsyncIO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AsyncIOFromFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr AsyncIOFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file, [MarshalAs(UnmanagedType.LPUTF8Str)] string mode);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_GetAsyncIOSize(SDL_AsyncIO *asyncio);</code>
    /// <summary>
    /// <para>Use this function to get the size of the data stream in an SDL_AsyncIO.</para>
    /// <para>This call is _not_ asynchronous; it assumes that obtaining this info is a
    /// non-blocking operation in most reasonable cases.</para>
    /// </summary>
    /// <param name="asyncio">the SDL_AsyncIO to get the size of the data stream from.</param>
    /// <returns>the size of the data stream in the SDL_IOStream on success or a
    /// negative error code on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAsyncIOSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetAsyncIOSize(IntPtr asyncio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadAsyncIO(SDL_AsyncIO *asyncio, void *ptr, Uint64 offset, Uint64 size, SDL_AsyncIOQueue *queue, void *userdata);</code>
    /// <summary>
    /// <para>Start an async read.</para>
    /// <para>This function reads up to <c>size</c> bytes from <c>offset</c> position in the data
    /// source to the area pointed at by <c>ptr</c>. This function may read less bytes
    /// than requested.</para>
    /// <para>This function returns as quickly as possible; it does not wait for the read
    /// to complete. On a successful return, this work will continue in the
    /// background. If the work begins, even failure is asynchronous: a failing
    /// return value from this function only means the work couldn't start at all.</para>
    /// <para><c>ptr</c> must remain available until the work is done, and may be accessed by
    /// the system at any time until then. Do not allocate it on the stack, as this
    /// might take longer than the life of the calling function to complete!</para>
    /// <para>An SDL_AsyncIOQueue must be specified. The newly-created task will be added
    /// to it when it completes its work.</para>
    /// </summary>
    /// <param name="asyncio">a pointer to an SDL_AsyncIO structure.</param>
    /// <param name="ptr">a pointer to a buffer to read data into.</param>
    /// <param name="offset">the position to start reading in the data source.</param>
    /// <param name="size">the number of bytes to read from the data source.</param>
    /// <param name="queue">a queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">an app-defined pointer that will be provided with the task
    /// results.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="WriteAsyncIO"/>
    /// <seealso cref="CreateAsyncIOQueue"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadAsyncIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ReadAsyncIO(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteAsyncIO(SDL_AsyncIO *asyncio, void *ptr, Uint64 offset, Uint64 size, SDL_AsyncIOQueue *queue, void *userdata);</code>
    /// <summary>
    /// <para>Start an async write.</para>
    /// <para>This function writes <c>size</c> bytes from <c>offset</c> position in the data source
    /// to the area pointed at by <c>ptr</c>.</para>
    /// <para>This function returns as quickly as possible; it does not wait for the
    /// write to complete. On a successful return, this work will continue in the
    /// background. If the work begins, even failure is asynchronous: a failing
    /// return value from this function only means the work couldn't start at all.</para>
    /// <para><c>ptr</c> must remain available until the work is done, and may be accessed by
    /// the system at any time until then. Do not allocate it on the stack, as this
    /// might take longer than the life of the calling function to complete!</para>
    /// <para>An SDL_AsyncIOQueue must be specified. The newly-created task will be added
    /// to it when it completes its work.</para>
    /// </summary>
    /// <param name="asyncio">a pointer to an SDL_AsyncIO structure.</param>
    /// <param name="ptr">a pointer to a buffer to write data from.</param>
    /// <param name="offset">the position to start writing to the data source.</param>
    /// <param name="size">the number of bytes to write to the data source.</param>
    /// <param name="queue">a queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">an app-defined pointer that will be provided with the task
    /// results.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="ReadAsyncIO"/>
    /// <seealso cref="CreateAsyncIOQueue"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteAsyncIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WriteAsyncIO(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CloseAsyncIO(SDL_AsyncIO *asyncio, bool flush, SDL_AsyncIOQueue *queue, void *userdata);</code>
    /// <summary>
    /// <para>Close and free any allocated resources for an async I/O object.</para>
    /// <para>Closing a file is _also_ an asynchronous task! If a write failure were to
    /// happen during the closing process, for example, the task results will
    /// report it as usual.</para>
    /// <para>Closing a file that has been written to does not guarantee the data has
    /// made it to physical media; it may remain in the operating system's file
    /// cache, for later writing to disk. This means that a successfully-closed
    /// file can be lost if the system crashes or loses power in this small window.
    /// To prevent this, call this function with the <c>flush</c> parameter set to true.
    /// This will make the operation take longer, and perhaps increase system load
    /// in general, but a successful result guarantees that the data has made it to
    /// physical storage. Don't use this for temporary files, caches, and
    /// unimportant data, and definitely use it for crucial irreplaceable files,
    /// like game saves.</para>
    /// <para>This function guarantees that the close will happen after any other pending
    /// tasks to <c>asyncio</c>, so it's safe to open a file, start several operations,
    /// close the file immediately, then check for all results later. This function
    /// will not block until the tasks have completed.</para>
    /// <para>Once this function returns true, <c>asyncio</c> is no longer valid, regardless
    /// of any future outcomes. Any completed tasks might still contain this
    /// pointer in their SDL_AsyncIOOutcome data, in case the app was using this
    /// value to track information, but it should not be used again.</para>
    /// <para>If this function returns false, the close wasn't started at all, and it's
    /// safe to attempt to close again later.</para>
    /// <para>An SDL_AsyncIOQueue must be specified. The newly-created task will be added
    /// to it when it completes its work.</para>
    /// </summary>
    /// <param name="asyncio">a pointer to an SDL_AsyncIO structure to close.</param>
    /// <param name="flush"><c>true</c> if data should sync to disk before the task completes.</param>
    /// <param name="queue">a queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">an app-defined pointer that will be provided with the task
    /// results.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but two
    /// threads should not attempt to close the same object.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseAsyncIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CloseAsyncIO(IntPtr asyncio, [MarshalAs(UnmanagedType.I1)] bool flush, IntPtr queue, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC SDL_AsyncIOQueue * SDLCALL SDL_CreateAsyncIOQueue(void);</code>
    /// <summary>
    /// <para>Create a task queue for tracking multiple I/O operations.</para>
    /// <para>Async I/O operations are assigned to a queue when started. The queue can be
    /// checked for completed tasks thereafter.</para>
    /// </summary>
    /// <returns>a new task queue object or <c>null</c> if there was an error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="DestroyAsyncIOQueue"/>
    /// <seealso cref="GetAsyncIOResult"/>
    /// <seealso cref="WaitAsyncIOResult"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateAsyncIOQueue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateAsyncIOQueue();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyAsyncIOQueue(SDL_AsyncIOQueue *queue);</code>
    /// <summary>
    /// <para>Destroy a previously-created async I/O task queue.</para>
    /// <para>If there are still tasks pending for this queue, this call will block until
    /// those tasks are finished. All those tasks will be deallocated. Their
    /// results will be lost to the app.</para>
    /// <para>Any pending reads from <see cref="LoadFileAsync"/> that are still in this queue
    /// will have their buffers deallocated by this function, to prevent a memory
    /// leak.</para>
    /// <para>Once this function is called, the queue is no longer valid and should not
    /// be used, including by other threads that might access it while destruction
    /// is blocking on pending tasks.</para>
    /// <para>Do not destroy a queue that still has threads waiting on it through
    /// <see cref="WaitAsyncIOResult"/>. You can call <see cref="SignalAsyncIOQueue"/> first to
    /// unblock those threads, and take measures (such as <see cref="WaitThread"/>) to make
    /// sure they have finished their wait and won't wait on the queue again.</para>
    /// </summary>
    /// <param name="queue">the task queue to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread, so long as
    /// no other thread is waiting on the queue with
    /// <see cref="WaitAsyncIOResult"/>.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyAsyncIOQueue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyAsyncIOQueue(IntPtr queue);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetAsyncIOResult(SDL_AsyncIOQueue *queue, SDL_AsyncIOOutcome *outcome);</code>
    /// <summary>
    /// <para>Query an async I/O task queue for completed tasks.</para>
    /// <para>If a task assigned to this queue has finished, this will return true and
    /// fill in <c>outcome</c> with the details of the task. If no task in the queue has
    /// finished, this function will return false. This function does not block.</para>
    /// <para>If a task has completed, this function will free its resources and the task
    /// pointer will no longer be valid. The task will be removed from the queue.</para>
    /// <para>It is safe for multiple threads to call this function on the same queue at
    /// once; a completed task will only go to one of the threads.</para>
    /// </summary>
    /// <param name="queue">the async I/O task queue to query.</param>
    /// <param name="outcome">details of a finished task will be written here. May not be
    /// <c>null</c>.</param>
    /// <returns><c>true</c> if a task has completed, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="WaitAsyncIOResult"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAsyncIOResult"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetAsyncIOResult(IntPtr queue, out AsyncIOOutcome outcome);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitAsyncIOResult(SDL_AsyncIOQueue *queue, SDL_AsyncIOOutcome *outcome, Sint32 timeoutMS);</code>
    /// <summary>
    /// <para>Block until an async I/O task queue has a completed task.</para>
    /// <para>This function puts the calling thread to sleep until there a task assigned
    /// to the queue that has finished.</para>
    /// <para>If a task assigned to the queue has finished, this will return true and
    /// fill in <c>outcome</c> with the details of the task. If no task in the queue has
    /// finished, this function will return false.</para>
    /// <para>If a task has completed, this function will free its resources and the task
    /// pointer will no longer be valid. The task will be removed from the queue.</para>
    /// <para>It is safe for multiple threads to call this function on the same queue at
    /// once; a completed task will only go to one of the threads.</para>
    /// <para>Note that by the nature of various platforms, more than one waiting thread
    /// may wake to handle a single task, but only one will obtain it, so
    /// <c>timeoutMS</c> is a _maximum_ wait time, and this function may return false
    /// sooner.</para>
    /// <para>This function may return false if there was a system error, the OS
    /// inadvertently awoke multiple threads, or if <see cref="SignalAsyncIOQueue"/> was
    /// called to wake up all waiting threads without a finished task.</para>
    /// <para>A timeout can be used to specify a maximum wait time, but rather than
    /// polling, it is possible to have a timeout of -1 to wait forever, and use
    /// <see cref="SignalAsyncIOQueue"/> to wake up the waiting threads later.</para>
    /// </summary>
    /// <param name="queue">the async I/O task queue to wait on.</param>
    /// <param name="outcome">details of a finished task will be written here. May not be
    /// <c>null</c>.</param>
    /// <param name="timeoutMS">the maximum time to wait, in milliseconds, or -1 to wait
    /// indefinitely.</param>
    /// <returns><c>true</c> if task has completed, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="SignalAsyncIOQueue"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitAsyncIOResult"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitAsyncIOResult(IntPtr queue, out AsyncIOOutcome outcome, int timeoutMS);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SignalAsyncIOQueue(SDL_AsyncIOQueue *queue);</code>
    /// <summary>
    /// <para>Wake up any threads that are blocking in <see cref="WaitAsyncIOResult"/>.</para>
    /// <para>This will unblock any threads that are sleeping in a call to
    /// <see cref="WaitAsyncIOResult"/> for the specified queue, and cause them to return
    /// from that function.</para>
    /// <para>This can be useful when destroying a queue to make sure nothing is touching
    /// it indefinitely. In this case, once this call completes, the caller should
    /// take measures to make sure any previously-blocked threads have returned
    /// from their wait and will not touch the queue again (perhaps by setting a
    /// flag to tell the threads to terminate and then using <see cref="WaitThread"/> to
    /// make sure they've done so).</para>
    /// </summary>
    /// <param name="queue">the async I/O task queue to signal.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="WaitAsyncIOResult"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SignalAsyncIOQueue"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SignalAsyncIOQueue(IntPtr queue);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LoadFileAsync(const char *file, SDL_AsyncIOQueue *queue, void *userdata);</code>
    /// <summary>
    /// <para>Load all the data from a file path, asynchronously.</para>
    /// <para>This function returns as quickly as possible; it does not wait for the read
    /// to complete. On a successful return, this work will continue in the
    /// background. If the work begins, even failure is asynchronous: a failing
    /// return value from this function only means the work couldn't start at all.</para>
    /// <para>The data is allocated with a zero byte at the end (null terminated) for
    /// convenience. This extra byte is not included in SDL_AsyncIOOutcome's
    /// bytes_transferred value.</para>
    /// <para>This function will allocate the buffer to contain the file. It must be
    /// deallocated by calling <see cref="Free"/> on SDL_AsyncIOOutcome's buffer field
    /// after completion.</para>
    /// <para>An SDL_AsyncIOQueue must be specified. The newly-created task will be added
    /// to it when it completes its work.</para>
    /// </summary>
    /// <param name="file">the path to read all available data from.</param>
    /// <param name="queue">a queue to add the new SDL_AsyncIO to.</param>
    /// <param name="userdata">an app-defined pointer that will be provided with the task
    /// results.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <see cref="LoadFileIO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadFileAsync"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LoadFileAsync([MarshalAs(UnmanagedType.LPUTF8Str)] string file, IntPtr queue, IntPtr userdata);
}