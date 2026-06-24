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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IOFromFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file, [MarshalAs(UnmanagedType.LPUTF8Str)] string mode);
    private delegate IntPtr IOFromFileNative(string file, string mode);
    private static IOFromFileNative IOFromFileNativeFunction = SDL_IOFromFile;

    /// <code>extern SDL_DECLSPEC SDL_IOStream * SDLCALL SDL_IOFromFile(const char *file, const char *mode);</code>
    /// <summary>
    /// <para>Use this function to create a new SDL_IOStream structure for reading from
    /// and/or writing to a named file.</para>
    /// <para>The <c>mode</c> string is treated roughly the same as in a call to the C
    /// library's fopen(), even if SDL doesn't happen to use fopen() behind the
    /// scenes.</para>
    /// <para>Available <c>mode</c> strings:</para>
    /// <list type="bullet">
    /// <item>"r": Open a file for reading. The file must exist.</item>
    /// <item>"w": Create an empty file for writing. If a file with the same name
    /// already exists its content is erased and the file is treated as a new
    /// empty file.</item>
    /// <item>"wx": Create an empty file for writing. If a file with the same name
    /// already exists, the call fails.</item>
    /// <item>"a": Append to a file. Writing operations append data at the end of the
    /// file. The file is created if it does not exist.</item>
    /// <item>"r+": Open a file for update both reading and writing. The file must
    /// exist.</item>
    /// <item>"w+": Create an empty file for both reading and writing. If a file with
    /// the same name already exists its content is erased and the file is
    /// treated as a new empty file.</item>
    /// <item>"w+x": Create an empty file for both reading and writing. If a file with
    /// the same name already exists, the call fails.</item>
    /// <item>"a+": Open a file for reading and appending. All writing operations are
    /// performed at the end of the file, protecting the previous content to be
    /// overwritten. You can reposition (fseek, rewind) the internal pointer to
    /// anywhere in the file for reading, but writing operations will move it
    /// back to the end of file. The file is created if it does not exist.</item>
    /// </list>
    /// <para><b>NOTE</b>: In order to open a file as a binary file, a "b" character has to
    /// be included in the `mode` string. This additional "b" character can either
    /// be appended at the end of the string (thus making the following compound
    /// modes: "rb", "wb", "ab", "r+b", "w+b", "a+b") or be inserted between the
    /// letter and the "+" sign for the mixed modes ("rb+", "wb+", "ab+").
    /// Additional characters may follow the sequence, although they should have no
    /// effect. For example, "t" is sometimes appended to make explicit the file is
    /// a text file.</para>
    /// <para>This function supports Unicode filenames, but they must be encoded in UTF-8
    /// format, regardless of the underlying operating system.</para>
    /// <para>In Android, <see cref="IOFromFile"/> can be used to open content:// URIs. As a
    /// fallback, <see cref="IOFromFile"/> will transparently open a matching filename in
    /// the app's <c>assets</c>.</para>
    /// <para>Closing the SDL_IOStream will close SDL's internal file handle.</para>
    /// <para>The following properties may be set at creation time by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamWindowsHandlePointer"/>: a pointer, that can be cast
    /// to a win32 <c>HANDLE</c>, that this SDL_IOStream is using to access the
    /// filesystem. If the program isn't running on Windows, or SDL used some
    /// other method to access the filesystem, this property will not be set.</item>
    /// <item><see cref="Props.IOStreamSTDIOFilePointer"/>: a pointer, that can be cast to a
    /// stdio <c>FILE *</c>, that this SDL_IOStream is using to access the filesystem.
    /// If SDL used some other method to access the filesystem, this property
    /// will not be set. PLEASE NOTE that if SDL is using a different C runtime
    /// than your app, trying to use this pointer will almost certainly result in
    /// a crash! This is mostly a problem on Windows; make sure you build SDL and
    /// your app with the same compiler and settings to avoid it.</item>
    /// <item><see cref="Props.IOStreamFileDescriptorNumber"/>: a file descriptor that this
    /// SDL_IOStream is using to access the filesystem.</item>
    /// <item><see cref="Props.IOStreamAndroidAAssetPointer"/>: a pointer, that can be cast
    /// to an Android NDK <c>AAsset *</c>, that this SDL_IOStream is using to access
    /// the filesystem. If SDL used some other method to access the filesystem,
    /// this property will not be set.</item>
    /// </list>
    /// </summary>
    /// <param name="file">a UTF-8 string representing the filename to open.</param>
    /// <param name="mode">an ASCII string representing the mode to be used for opening
    /// the file.</param>
    /// <returns>a pointer to the SDL_IOStream structure that is created or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="FlushIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IntPtr IOFromFile(string file, string mode)
    {
        return IOFromFileNativeFunction(file, mode);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IOFromMem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromMem(IntPtr mem, UIntPtr size);
    private delegate IntPtr IOFromMemNative(IntPtr mem, UIntPtr size);
    private static IOFromMemNative IOFromMemNativeFunction = SDL_IOFromMem;

    /// <code>extern SDL_DECLSPEC SDL_IOStream * SDLCALL SDL_IOFromMem(void *mem, size_t size);</code>
    /// <summary>
    /// <para>Use this function to prepare a read-write memory buffer for use with
    /// SDL_IOStream.</para>
    /// <para>This function sets up an SDL_IOStream struct based on a memory area of a
    /// certain size, for both read and write access.</para>
    /// <para>This memory buffer is not copied by the SDL_IOStream; the pointer you
    /// provide must remain valid until you close the stream.</para>
    /// <para>If you need to make sure the SDL_IOStream never writes to the memory
    /// buffer, you should use <see cref="IOFromConstMem"/> with a read-only buffer of
    /// memory instead.</para>
    /// <para>The following properties will be set at creation time by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamMemoryPointer"/>: this will be the <c>mem</c> parameter that
    /// was passed to this function.</item>
    /// <item><see cref="Props.IOStreamMemorySizeNumber"/>: this will be the <c>size</c> parameter
    /// that was passed to this function.</item>
    /// </list>
    /// <para>Additionally, the following properties are recognized:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamMemoryFreeFuncPointer"/>: if this property is set to
    /// a non-<c>null</c> value it will be interpreted as a function of SDL_free_func
    /// type and called with the passed `mem` pointer when closing the stream. By
    /// default it is unset, i.e., the memory will not be freed.</item>
    /// </list>
    /// </summary>
    /// <param name="mem">a pointer to a buffer to feed an SDL_IOStream stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <returns>a pointer to a new SDL_IOStream structure or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="IOFromConstMem"/>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="FlushIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IntPtr IOFromMem(IntPtr mem, UIntPtr size)
    {
        return IOFromMemNativeFunction(mem, size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IOFromConstMem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromConstMem(IntPtr mem, UIntPtr size);
    private delegate IntPtr IOFromConstMemNative(IntPtr mem, UIntPtr size);
    private static IOFromConstMemNative IOFromConstMemNativeFunction = SDL_IOFromConstMem;

    /// <code>extern SDL_DECLSPEC SDL_IOStream * SDLCALL SDL_IOFromConstMem(const void *mem, size_t size);</code>
    /// <summary>
    /// <para>Use this function to prepare a read-only memory buffer for use with
    /// SDL_IOStream.</para>
    /// <para>This function sets up an SDL_IOStream struct based on a memory area of a
    /// certain size. It assumes the memory area is not writable.</para>
    /// <para>Attempting to write to this SDL_IOStream stream will report an error
    /// without writing to the memory buffer.</para>
    /// <para>This memory buffer is not copied by the SDL_IOStream; the pointer you
    /// provide must remain valid until you close the stream.</para>
    /// <para>If you need to write to a memory buffer, you should use <see cref="IOFromMem"/>
    /// with a writable buffer of memory instead.</para>
    /// <para>The following properties will be set at creation time by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamMemoryPointer"/>: this will be the <c>mem</c> parameter that
    /// was passed to this function.</item>
    /// <item><see cref="Props.IOStreamMemorySizeNumber"/>: this will be the <c>size</c> parameter
    /// that was passed to this function.</item>
    /// </list>
    /// <para>Additionally, the following properties are recognized:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamMemoryFreeFuncPointer"/>: if this property is set to
    /// a non-<c>null</c> value it will be interpreted as a function of SDL_free_func
    /// type and called with the passed <c>mem</c> pointer when closing the stream. By
    /// default it is unset, i.e., the memory will not be freed.</item>
    /// </list>
    /// </summary>
    /// <param name="mem">a pointer to a read-only buffer to feed an SDL_IOStream stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <returns>a pointer to a new SDL_IOStream structure or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="IOFromMem"/>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    public static IntPtr IOFromConstMem(IntPtr mem, UIntPtr size)
    {
        return IOFromConstMemNativeFunction(mem, size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IOFromDynamicMem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromDynamicMem();
    private delegate IntPtr IOFromDynamicMemNative();
    private static IOFromDynamicMemNative IOFromDynamicMemNativeFunction = SDL_IOFromDynamicMem;

    /// <code>extern SDL_DECLSPEC SDL_IOStream * SDLCALL SDL_IOFromDynamicMem(void);</code>
    /// <summary>
    /// <para>Use this function to create an SDL_IOStream that is backed by dynamically
    /// allocated memory.</para>
    /// <para>This supports the following properties to provide access to the memory and
    /// control over allocations:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamDynamicMemoryPointer"/>: a pointer to the internal
    /// memory of the stream. This can be set to <c>null</c> to transfer ownership of
    /// the memory to the application, which should free the memory with
    /// <see cref="Free"/>. If this is done, the next operation on the stream must be
    /// <see cref="CloseIO"/>.</item>
    /// <item><see cref="Props.IOStreamDynamicChunkSizeNumber"/>: memory will be allocated in
    /// multiples of this size, defaulting to 1024.</item>
    /// </list>
    /// </summary>
    /// <returns>a pointer to a new SDL_IOStream structure or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IntPtr IOFromDynamicMem()
    {
        return IOFromDynamicMemNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [DllImport(SDLLibrary, EntryPoint = "SDL_OpenIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static extern IntPtr SDL_OpenIO(in IOStreamInterface iface, IntPtr userdata);
    private delegate IntPtr OpenIONative(in IOStreamInterface iface, IntPtr userdata);
    private static OpenIONative OpenIONativeFunction = SDL_OpenIO;

    /// <code>extern SDL_DECLSPEC SDL_IOStream * SDLCALL SDL_OpenIO(const SDL_IOStreamInterface *iface, void *userdata);</code>
    /// <summary>
    /// <para>Create a custom SDL_IOStream.</para>
    /// <para>Applications do not need to use this function unless they are providing
    /// their own SDL_IOStream implementation. If you just need an SDL_IOStream to
    /// read/write a common data source, you should use the built-in
    /// implementations in SDL, like <see cref="IOFromFile"/> or <see cref="IOFromMem"/>, etc.</para>
    /// <para>This function makes a copy of <c>iface</c> and the caller does not need to keep
    /// it around after this call.</para>
    /// </summary>
    /// <param name="iface">the interface that implements this SDL_IOStream, initialized
    /// using <see cref="InitInterface(ref IOStreamInterface)"/>.</param>
    /// <param name="userdata">the pointer that will be passed to the interface functions.</param>
    /// <returns>a pointer to the allocated memory on success or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="InitInterface(ref IOStreamInterface)"/>
    /// <seealso cref="IOFromConstMem"/>
    /// <seealso cref="IOFromFile"/>
    /// <seealso cref="IOFromMem"/>
    public static IntPtr OpenIO(in IOStreamInterface iface, IntPtr userdata)
    {
        return OpenIONativeFunction(in iface, userdata);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CloseIO(IntPtr context);
    private delegate bool CloseIONative(IntPtr context);
    private static CloseIONative CloseIONativeFunction = SDL_CloseIO;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_CloseIO(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Close and free an allocated SDL_IOStream structure.</para>
    /// <para><see cref="CloseIO"/> closes and cleans up the SDL_IOStream stream. It releases any
    /// resources used by the stream and frees the SDL_IOStream itself. This
    /// returns <c>true</c> on success, or <c>false</c> if the stream failed to flush to its
    /// output (e.g. to disk).</para>
    /// <para>Note that if this fails to flush the stream for any reason, this function
    /// reports an error, but the SDL_IOStream is still invalid once this function
    /// returns.</para>
    /// <para>This call flushes any buffered writes to the operating system, but there
    /// are no guarantees that those writes have gone to physical media; they might
    /// be in the OS's file cache, waiting to go to disk later. If it's absolutely
    /// crucial that writes go to disk immediately, so they are definitely stored
    /// even if the power fails before the file cache would have caught up, one
    /// should call <see cref="FlushIO"/> before closing. Note that flushing takes time and
    /// makes the system and your app operate less efficiently, so do so sparingly.</para>
    /// </summary>
    /// <param name="context">SDL_IOStream structure to close.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenIO"/>
    public static bool CloseIO(IntPtr context)
    {
        return CloseIONativeFunction(context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetIOProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetIOProperties(IntPtr context);
    private delegate uint GetIOPropertiesNative(IntPtr context);
    private static GetIOPropertiesNative GetIOPropertiesNativeFunction = SDL_GetIOProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetIOProperties(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Get the properties associated with an SDL_IOStream.</para>
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GetIOProperties(IntPtr context)
    {
        return GetIOPropertiesNativeFunction(context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetIOStatus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStatus SDL_GetIOStatus(IntPtr context);
    private delegate IOStatus GetIOStatusNative(IntPtr context);
    private static GetIOStatusNative GetIOStatusNativeFunction = SDL_GetIOStatus;

    /// <code>extern SDL_DECLSPEC SDL_IOStatus SDLCALL SDL_GetIOStatus(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Query the stream status of an SDL_IOStream.</para>
    /// <para>This information can be useful to decide if a short read or write was due
    /// to an error, an EOF, or a non-blocking operation that isn't yet ready to
    /// complete.</para>
    /// <para>An SDL_IOStream's status is only expected to change after a <see cref="ReadIO"/> or
    /// <see cref="WriteIO"/> call; don't expect it to change if you just call this query
    /// function in a tight loop.</para>
    /// </summary>
    /// <param name="context">the SDL_IOStream to query.</param>
    /// <returns>an <see cref="IOStatus"/> enum with the current state.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IOStatus GetIOStatus(IntPtr context)
    {
        return GetIOStatusNativeFunction(context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetIOSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetIOSize(IntPtr context);
    private delegate long GetIOSizeNative(IntPtr context);
    private static GetIOSizeNative GetIOSizeNativeFunction = SDL_GetIOSize;

    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_GetIOSize(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Use this function to get the size of the data stream in an SDL_IOStream.</para>
    /// </summary>
    /// <param name="context">the SDL_IOStream to get the size of the data stream from.</param>
    /// <returns>the size of the data stream in the SDL_IOStream on success or a
    /// negative error code on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static long GetIOSize(IntPtr context)
    {
        return GetIOSizeNativeFunction(context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SeekIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_SeekIO(IntPtr context, long offset, IOWhence whence);
    private delegate long SeekIONative(IntPtr context, long offset, IOWhence whence);
    private static SeekIONative SeekIONativeFunction = SDL_SeekIO;

    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_SeekIO(SDL_IOStream *context, Sint64 offset, SDL_IOWhence whence);</code>
    /// <summary>
    /// <para>Seek within an SDL_IOStream data stream.</para>
    /// <para>This function seeks to byte <c>offset</c>, relative to <c>whence</c>.</para>
    /// <para><c>whence</c> may be any of the following values:</para>
    /// <list type="bullet">
    /// <item><see cref="IOWhence.Set"/>: seek from the beginning of data</item>
    /// <item><see cref="IOWhence.Cur"/>: seek relative to current read point</item>
    /// <item><see cref="IOWhence.End"/>: seek relative to the end of data</item>
    /// </list>
    /// <para>If this stream can not seek, it will return -1.</para>
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="offset">an offset in bytes, relative to <c>whence</c> location; can be
    /// negative.</param>
    /// <param name="whence">any of <see cref="IOWhence.Set"/>, <see cref="IOWhence.Cur"/>,
    /// <see cref="IOWhence.End"/>.</param>
    /// <returns>the final offset in the data stream after the seek or -1 on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="TellIO"/>
    public static long SeekIO(IntPtr context, long offset, IOWhence whence)
    {
        return SeekIONativeFunction(context, offset, whence);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_TellIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TellIO(IntPtr context);
    private delegate long TellIONative(IntPtr context);
    private static TellIONative TellIONativeFunction = SDL_TellIO;

    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_TellIO(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Determine the current read/write offset in an SDL_IOStream data stream.</para>
    /// <para><see cref="TellIO"/> is actually a wrapper function that calls the SDL_IOStream's
    /// <c>seek</c> method, with an offset of 0 bytes from <see cref="IOWhence.Cur"/>, to
    /// simplify application development.</para>
    /// </summary>
    /// <param name="context">an SDL_IOStream data stream object from which to get the
    /// current offset.</param>
    /// <returns>the current offset in the stream, or -1 if the information can not
    /// be determined.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SeekIO"/>
    public static long TellIO(IntPtr context)
    {
        return TellIONativeFunction(context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_ReadIO(IntPtr context, IntPtr ptr, UIntPtr size);
    private delegate ulong ReadIONative(IntPtr context, IntPtr ptr, UIntPtr size);
    private static ReadIONative ReadIONativeFunction = SDL_ReadIO;

    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_ReadIO(SDL_IOStream *context, void *ptr, size_t size);</code>
    /// <summary>
    /// <para>Read from a data source.</para>
    /// <para>This function reads up <c>size</c> bytes from the data source to the area
    /// pointed at by <c>ptr</c>. This function may read less bytes than requested.</para>
    /// <para>This function will return zero when the data stream is completely read, and
    /// <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If zero is returned and
    /// the stream is not at EOF, <see cref="GetIOStatus"/> will return a different error
    /// value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// <para>A request for zero bytes on a valid stream will return zero immediately
    /// without accessing the stream, so the stream status (EOF, err, etc) will not
    /// change.</para>
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="ptr">a pointer to a buffer to read data into.</param>
    /// <param name="size">the number of bytes to read from the data source.</param>
    /// <returns>the number of bytes read, or 0 on end of file or other failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="WriteIO"/>
    /// <seealso cref="GetIOStatus"/>
    public static ulong ReadIO(IntPtr context, IntPtr ptr, UIntPtr size)
    {
        return ReadIONativeFunction(context, ptr, size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_WriteIO(IntPtr context, IntPtr ptr, UIntPtr size);
    private delegate ulong WriteIONative(IntPtr context, IntPtr ptr, UIntPtr size);
    private static WriteIONative WriteIONativeFunction = SDL_WriteIO;

    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_WriteIO(SDL_IOStream *context, const void *ptr, size_t size);</code>
    /// <summary>
    /// <para>Write to an SDL_IOStream data stream.</para>
    /// <para>This function writes exactly <c>size</c> bytes from the area pointed at by <c>ptr</c>
    /// to the stream. If this fails for any reason, it'll return less than <c>size</c>
    /// to demonstrate how far the write progressed. On success, it returns <c>size</c>.</para>
    /// <para>On error, this function still attempts to write as much as possible, so it
    /// might return a positive value less than the requested write size.</para>
    /// <para>The caller can use <see cref="GetIOStatus"/> to determine if the problem is
    /// recoverable, such as a non-blocking write that can simply be retried later,
    /// or a fatal error.</para>
    /// <para>A request for zero bytes on a valid stream will return zero immediately
    /// without accessing the stream, so the stream status (EOF, err, etc) will not
    /// change.</para>
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="ptr">a pointer to a buffer containing data to write.</param>
    /// <param name="size">the number of bytes to write.</param>
    /// <returns>the number of bytes written, which will be less than <c>size</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="IOprintf"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="FlushIO"/>
    /// <seealso cref="GetIOStatus"/>
    public static ulong WriteIO(IntPtr context, IntPtr ptr, UIntPtr size)
    {
        return WriteIONativeFunction(context, ptr, size);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IOprintf"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // ReSharper disable once InconsistentNaming
    private static partial UIntPtr SDL_IOprintf(IntPtr context, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt);
    private delegate UIntPtr IOprintfNative(IntPtr context, string fmt);
    private static IOprintfNative IOprintfNativeFunction = SDL_IOprintf;

    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_IOprintf(SDL_IOStream *context, SDL_PRINTF_FORMAT_STRING const char *fmt, ...)  SDL_PRINTF_VARARG_FUNC(2);</code>
    /// <summary>
    /// <para>Print to an SDL_IOStream data stream.</para>
    /// <para>This function does formatted printing to the stream.</para>
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="fmt">a printf() style format string.</param>
    /// <returns>the number of bytes written or 0 on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="IOvprintf"/>
    /// <seealso cref="WriteIO"/>
    public static UIntPtr IOprintf(IntPtr context, string fmt)
    {
        return IOprintfNativeFunction(context, fmt);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IOvprintf"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // ReSharper disable once InconsistentNaming
    private static partial UIntPtr SDL_IOvprintf(IntPtr context, [MarshalAs(UnmanagedType.LPUTF8Str)] string fmt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] ap);
    private delegate UIntPtr IOvprintfNative(IntPtr context, string fmt, string[] ap);
    private static IOvprintfNative IOvprintfNativeFunction = SDL_IOvprintf;

    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_IOvprintf(SDL_IOStream *context, SDL_PRINTF_FORMAT_STRING const char *fmt, va_list ap) SDL_PRINTF_VARARG_FUNCV(2);</code>
    /// <summary>
    /// <para>Print to an SDL_IOStream data stream.</para>
    /// <para>This function does formatted printing to the stream.</para>
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="fmt">a printf() style format string.</param>
    /// <param name="ap">a variable argument list.</param>
    /// <returns>the number of bytes written or 0 on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="IOprintf"/>
    /// <seealso cref="WriteIO"/>
    public static UIntPtr IOvprintf(IntPtr context, string fmt, string[] ap)
    {
        return IOvprintfNativeFunction(context, fmt, ap);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlushIO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_FlushIO(IntPtr context);
    private delegate bool FlushIONative(IntPtr context);
    private static FlushIONative FlushIONativeFunction = SDL_FlushIO;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FlushIO(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Flush any buffered data in the stream.</para>
    /// <para>This function makes sure that any buffered data is written to the stream.
    /// Normally this isn't necessary but if the stream is a pipe or socket it
    /// guarantees that any pending data is sent.</para>
    /// </summary>
    /// <param name="context">SDL_IOStream structure to flush.</param>
    /// <returns><c>true on</c> success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenIO"/>
    /// <seealso cref="WriteIO"/>
    public static bool FlushIO(IntPtr context)
    {
        return FlushIONativeFunction(context);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadFile_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_LoadFile_IO(IntPtr src, out UIntPtr datasize, [MarshalAs(UnmanagedType.I1)] bool closeio);
    private delegate IntPtr LoadFileIONative(IntPtr src, out UIntPtr datasize, bool closeio);
    private static LoadFileIONative LoadFileIONativeFunction = SDL_LoadFile_IO;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_LoadFile_IO(SDL_IOStream *src, size_t *datasize, bool closeio);</code>
    /// <summary>
    /// <para>Load all the data from an SDL data stream.</para>
    /// <para>The data is allocated with a zero byte at the end (<c>null</c> terminated) for
    /// convenience. This extra byte is not included in the value reported via
    /// <c>datasize</c>.</para>
    /// <para>The data should be freed with <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="src">the SDL_IOStream to read all available data from.</param>
    /// <param name="datasize">a pointer filled in with the number of bytes read, may be
    /// <c>null</c>.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>src</c> before returning, even
    /// in the case of an error.</param>
    /// <returns>the data or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LoadFile"/>
    /// <seealso cref="SaveFileIO"/>
    public static IntPtr LoadFileIO(IntPtr src, out UIntPtr datasize, bool closeio)
    {
        return LoadFileIONativeFunction(src, out datasize, closeio);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_LoadFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file, out UIntPtr datasize);
    private delegate IntPtr LoadFileNative(string file, out UIntPtr datasize);
    private static LoadFileNative LoadFileNativeFunction = SDL_LoadFile;

    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_LoadFile(const char *file, size_t *datasize);</code>
    /// <summary>
    /// <para>Load all the data from a file path.</para>
    /// <para>The data is allocated with a zero byte at the end (<c>null</c> terminated) for
    /// convenience. This extra byte is not included in the value reported via
    /// <c>datasize</c>.</para>
    /// <para>The data should be freed with <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="file">the path to read all available data from.</param>
    /// <param name="datasize">if not <c>null</c>, will store the number of bytes read.</param>
    /// <returns>the data or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LoadFileIO"/>
    /// <seealso cref="SaveFile"/>
    public static IntPtr LoadFile(string file, out UIntPtr datasize)
    {
        return LoadFileNativeFunction(file, out datasize);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SaveFile_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SaveFile_IO(IntPtr src, IntPtr data, UIntPtr datasize, [MarshalAs(UnmanagedType.I1)] bool closeio);
    private delegate bool SaveFileIONative(IntPtr src, IntPtr data, UIntPtr datasize, bool closeio);
    private static SaveFileIONative SaveFileIONativeFunction = SDL_SaveFile_IO;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SaveFile_IO(SDL_IOStream *src, const void *data, size_t datasize, bool closeio);</code>
    /// <summary>
    /// Save all the data into an SDL data stream.
    /// </summary>
    /// <param name="src">the SDL_IOStream to write all data to.</param>
    /// <param name="data">the data to be written. If datasize is 0, may be <c>null</c> or a
    /// invalid pointer.</param>
    /// <param name="datasize">the number of bytes to be written.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>src</c> before returning, even
    /// in the case of an error.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="SaveFile"/>
    /// <seealso cref="LoadFileIO"/>
    public static bool SaveFileIO(IntPtr src, IntPtr data, UIntPtr datasize, bool closeio)
    {
        return SaveFileIONativeFunction(src, data, datasize, closeio);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SaveFile"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SaveFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file, IntPtr data, UIntPtr datasize);
    private delegate bool SaveFileNative(string file, IntPtr data, UIntPtr datasize);
    private static SaveFileNative SaveFileNativeFunction = SDL_SaveFile;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SaveFile(const char *file, const void *data, size_t datasize);</code>
    /// <summary>
    /// Save all the data into a file path.
    /// </summary>
    /// <param name="file">the path to write all available data into.</param>
    /// <param name="data">the data to be written. If datasize is 0, may be <c>null</c> or a
    /// invalid pointer.</param>
    /// <param name="datasize">the number of bytes to be written.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="SaveFileIO"/>
    /// <seealso cref="LoadFile"/>
    public static bool SaveFile(string file, IntPtr data, UIntPtr datasize)
    {
        return SaveFileNativeFunction(file, data, datasize);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU8"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU8(IntPtr src, out byte value);
    private delegate bool ReadU8Native(IntPtr src, out byte value);
    private static ReadU8Native ReadU8NativeFunction = SDL_ReadU8;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU8(SDL_IOStream *src, Uint8 *value);</code>
    /// <summary>
    /// <para>Use this function to read a byte from an SDL_IOStream.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure or EOF; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU8(IntPtr src, out byte value)
    {
        return ReadU8NativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS8"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS8(IntPtr src, out sbyte value);
    private delegate bool ReadS8Native(IntPtr src, out sbyte value);
    private static ReadS8Native ReadS8NativeFunction = SDL_ReadS8;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS8(SDL_IOStream *src, Sint8 *value);</code>
    /// <summary>
    /// <para>Use this function to read a signed byte from an SDL_IOStream.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS8(IntPtr src, out sbyte value)
    {
        return ReadS8NativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU16LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU16LE(IntPtr src, out ushort value);
    private delegate bool ReadU16LENative(IntPtr src, out ushort value);
    private static ReadU16LENative ReadU16LENativeFunction = SDL_ReadU16LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU16LE(SDL_IOStream *src, Uint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of little-endian data from an
    /// SDL_IOStream and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU16LE(IntPtr src, out ushort value)
    {
        return ReadU16LENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS16LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS16LE(IntPtr src, out short value);
    private delegate bool ReadS16LENative(IntPtr src, out short value);
    private static ReadS16LENative ReadS16LENativeFunction = SDL_ReadS16LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS16LE(SDL_IOStream *src, Sint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of little-endian data from an
    /// SDL_IOStream and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS16LE(IntPtr src, out short value)
    {
        return ReadS16LENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU16BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU16BE(IntPtr src, out ushort value);
    private delegate bool ReadU16BENative(IntPtr src, out ushort value);
    private static ReadU16BENative ReadU16BENativeFunction = SDL_ReadU16BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU16BE(SDL_IOStream *src, Uint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU16BE(IntPtr src, out ushort value)
    {
        return ReadU16BENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS16BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS16BE(IntPtr src, out short value);
    private delegate bool ReadS16BENative(IntPtr src, out short value);
    private static ReadS16BENative ReadS16BENativeFunction = SDL_ReadS16BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS16BE(SDL_IOStream *src, Sint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS16BE(IntPtr src, out short value)
    {
        return ReadS16BENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU32LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU32LE(IntPtr src, out uint value);
    private delegate bool ReadU32LENative(IntPtr src, out uint value);
    private static ReadU32LENative ReadU32LENativeFunction = SDL_ReadU32LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU32LE(SDL_IOStream *src, Uint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of little-endian data from an
    /// SDL_IOStream and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU32LE(IntPtr src, out uint value)
    {
        return ReadU32LENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS32LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS32LE(IntPtr src, out int value);
    private delegate bool ReadS32LENative(IntPtr src, out int value);
    private static ReadS32LENative ReadS32LENativeFunction = SDL_ReadS32LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS32LE(SDL_IOStream *src, Sint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of little-endian data from an
    /// SDL_IOStream and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS32LE(IntPtr src, out int value)
    {
        return ReadS32LENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU32BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU32BE(IntPtr src, out uint value);
    private delegate bool ReadU32BENative(IntPtr src, out uint value);
    private static ReadU32BENative ReadU32BENativeFunction = SDL_ReadU32BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU32BE(SDL_IOStream *src, Uint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU32BE(IntPtr src, out uint value)
    {
        return ReadU32BENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS32BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS32BE(IntPtr src, out int value);
    private delegate bool ReadS32BENative(IntPtr src, out int value);
    private static ReadS32BENative ReadS32BENativeFunction = SDL_ReadS32BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS32BE(SDL_IOStream *src, Sint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS32BE(IntPtr src, out int value)
    {
        return ReadS32BENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU64LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU64LE(IntPtr src, out ulong value);
    private delegate bool ReadU64LENative(IntPtr src, out ulong value);
    private static ReadU64LENative ReadU64LENativeFunction = SDL_ReadU64LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU64LE(SDL_IOStream *src, Uint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of little-endian data from an
    /// SDL_IOStream and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU64LE(IntPtr src, out ulong value)
    {
        return ReadU64LENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS64LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS64LE(IntPtr src, out long value);
    private delegate bool ReadS64LENative(IntPtr src, out long value);
    private static ReadS64LENative ReadS64LENativeFunction = SDL_ReadS64LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS64LE(SDL_IOStream *src, Sint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of little-endian data from an
    /// SDL_IOStream and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS64LE(IntPtr src, out long value)
    {
        return ReadS64LENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadU64BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU64BE(IntPtr src, out ulong value);
    private delegate bool ReadU64BENative(IntPtr src, out ulong value);
    private static ReadU64BENative ReadU64BENativeFunction = SDL_ReadU64BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadU64BE(SDL_IOStream *src, Uint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadU64BE(IntPtr src, out ulong value)
    {
        return ReadU64BENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadS64BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS64BE(IntPtr src, out long value);
    private delegate bool ReadS64BENative(IntPtr src, out long value);
    private static ReadS64BENative ReadS64BENativeFunction = SDL_ReadS64BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ReadS64BE(SDL_IOStream *src, Sint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// <para>This function will return <c>false</c> when the data stream is completely read,
    /// and <see cref="GetIOStatus"/> will return <see cref="IOStatus.EOF"/>. If <c>false</c> is returned
    /// and the stream is not at EOF, <see cref="GetIOStatus"/> will return a different
    /// error value and <see cref="GetError"/> will offer a human-readable message.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful read or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool ReadS64BE(IntPtr src, out long value)
    {
        return ReadS64BENativeFunction(src, out value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU8"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU8(IntPtr dst, byte value);
    private delegate bool WriteU8Native(IntPtr dst, byte value);
    private static WriteU8Native WriteU8NativeFunction = SDL_WriteU8;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU8(SDL_IOStream *dst, Uint8 value);</code>
    /// <summary>
    /// Use this function to write a byte to an SDL_IOStream.
    /// </summary>
    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU8(IntPtr dst, byte value)
    {
        return WriteU8NativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS8"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS8(IntPtr dst, sbyte value);
    private delegate bool WriteS8Native(IntPtr dst, sbyte value);
    private static WriteS8Native WriteS8NativeFunction = SDL_WriteS8;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS8(SDL_IOStream *dst, Sint8 value);</code>
    /// <summary>
    /// Use this function to write a signed byte to an SDL_IOStream.
    /// </summary>
    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS8(IntPtr dst, sbyte value)
    {
        return WriteS8NativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU16LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU16LE(IntPtr dst, ushort value);
    private delegate bool WriteU16LENative(IntPtr dst, ushort value);
    private static WriteU16LENative WriteU16LENativeFunction = SDL_WriteU16LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU16LE(SDL_IOStream *dst, Uint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful <c>write</c> or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU16LE(IntPtr dst, ushort value)
    {
        return WriteU16LENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS16LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS16LE(IntPtr dst, short value);
    private delegate bool WriteS16LENative(IntPtr dst, short value);
    private static WriteS16LENative WriteS16LENativeFunction = SDL_WriteS16LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS16LE(SDL_IOStream *dst, Sint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS16LE(IntPtr dst, short value)
    {
        return WriteS16LENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU16BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU16BE(IntPtr dst, ushort value);
    private delegate bool WriteU16BENative(IntPtr dst, ushort value);
    private static WriteU16BENative WriteU16BENativeFunction = SDL_WriteU16BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU16BE(SDL_IOStream *dst, Uint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an SDL_IOStream as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU16BE(IntPtr dst, ushort value)
    {
        return WriteU16BENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS16BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS16BE(IntPtr dst, short value);
    private delegate bool WriteS16BENative(IntPtr dst, short value);
    private static WriteS16BENative WriteS16BENativeFunction = SDL_WriteS16BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS16BE(SDL_IOStream *dst, Sint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an SDL_IOStream as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful <c>write</c> or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS16BE(IntPtr dst, short value)
    {
        return WriteS16BENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU32LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU32LE(IntPtr dst, uint value);
    private delegate bool WriteU32LENative(IntPtr dst, uint value);
    private static WriteU32LENative WriteU32LENativeFunction = SDL_WriteU32LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU32LE(SDL_IOStream *dst, Uint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU32LE(IntPtr dst, uint value)
    {
        return WriteU32LENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS32LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS32LE(IntPtr dst, int value);
    private delegate bool WriteS32LENative(IntPtr dst, int value);
    private static WriteS32LENative WriteS32LENativeFunction = SDL_WriteS32LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS32LE(SDL_IOStream *dst, Sint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS32LE(IntPtr dst, int value)
    {
        return WriteS32LENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU32BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU32BE(IntPtr dst, uint value);
    private delegate bool WriteU32BENative(IntPtr dst, uint value);
    private static WriteU32BENative WriteU32BENativeFunction = SDL_WriteU32BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU32BE(SDL_IOStream *dst, Uint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an SDL_IOStream as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU32BE(IntPtr dst, uint value)
    {
        return WriteU32BENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS32BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS32BE(IntPtr dst, int value);
    private delegate bool WriteS32BENative(IntPtr dst, int value);
    private static WriteS32BENative WriteS32BENativeFunction = SDL_WriteS32BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS32BE(SDL_IOStream *dst, Sint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an SDL_IOStream as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS32BE(IntPtr dst, int value)
    {
        return WriteS32BENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU64LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU64LE(IntPtr dst, ulong value);
    private delegate bool WriteU64LENative(IntPtr dst, ulong value);
    private static WriteU64LENative WriteU64LENativeFunction = SDL_WriteU64LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU64LE(SDL_IOStream *dst, Uint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU64LE(IntPtr dst, ulong value)
    {
        return WriteU64LENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS64LE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS64LE(IntPtr dst, long value);
    private delegate bool WriteS64LENative(IntPtr dst, long value);
    private static WriteS64LENative WriteS64LENativeFunction = SDL_WriteS64LE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS64LE(SDL_IOStream *dst, Sint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS64LE(IntPtr dst, long value)
    {
        return WriteS64LENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteU64BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU64BE(IntPtr dst, ulong value);
    private delegate bool WriteU64BENative(IntPtr dst, ulong value);
    private static WriteU64BENative WriteU64BENativeFunction = SDL_WriteU64BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteU64BE(SDL_IOStream *dst, Uint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an SDL_IOStream as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteU64BE(IntPtr dst, ulong value)
    {
        return WriteU64BENativeFunction(dst, value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WriteS64BE"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS64BE(IntPtr dst, long value);
    private delegate bool WriteS64BENative(IntPtr dst, long value);
    private static WriteS64BENative WriteS64BENativeFunction = SDL_WriteS64BE;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WriteS64BE(SDL_IOStream *dst, Sint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an SDL_IOStream as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>Do not use the same SDL_IOStream from two threads at once.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool WriteS64BE(IntPtr dst, long value)
    {
        return WriteS64BENativeFunction(dst, value);
    }
}
