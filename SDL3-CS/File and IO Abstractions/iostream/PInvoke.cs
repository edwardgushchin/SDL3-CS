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
    private static partial IntPtr SDL_IOFromFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string mode);
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_IOFromFile(const char *file, const char *mode);</code>
    /// <summary>
    /// <para>Use this function to create a new <see cref="IOStream"/> structure for reading from
    /// and/or writing to a named file.</para>
    /// <para>The <c>mode</c> string is treated roughly the same as in a call to the C
    /// library's fopen(), even if SDL doesn't happen to use fopen() behind the
    /// scenes.</para>
    /// <para>Available <c>mode</c> strings:</para>
    /// <list type="bullet">
    /// <item><c>"r"</c>: Open a file for reading. The file must exist</item>
    /// <item><c>"w"</c>: Create an empty file for writing. If a file with the same name
    /// already exists its content is erased and the file is treated as a new
    /// empty file.</item>
    /// <item><c>"a"</c>: Append to a file. Writing operations append data at the end of the
    /// file. The file is created if it does not exist.</item>
    /// <item><c>"r+"</c>: Open a file for update both reading and writing. The file must
    /// exist.</item>
    /// <item><c>"w+"</c>: Create an empty file for both reading and writing. If a file with
    /// the same name already exists its content is erased and the file is
    /// treated as a new empty file.</item>
    /// <item><c>"a+"</c>: Open a file for reading and appending. All writing operations are
    /// performed at the end of the file, protecting the previous content to be
    /// overwritten. You can reposition (fseek, rewind) the internal pointer to
    /// anywhere in the file for reading, but writing operations will move it
    /// back to the end of file. The file is created if it does not exist.</item>
    /// </list>
    /// <para><b>NOTE</b>: In order to open a file as a binary file, a <c>"b"</c> character has to
    /// be included in the <c>mode</c> string. This additional <c>"b"</c> character can either
    /// be appended at the end of the string (thus making the following compound
    /// modes: <c>"rb"</c>, <c>"wb"</c>, <c>"ab"</c>, <c>"r+b"</c>, <c>"w+b"</c>, <c>"a+b"</c>)
    /// or be inserted between the
    /// letter and the <c>"+"</c> sign for the mixed modes (<c>"rb+"</c>, <c>"wb+"</c>, <c>"ab+"</c>).
    /// Additional characters may follow the sequence, although they should have no
    /// effect. For example, <c>"t"</c> is sometimes appended to make explicit the file is
    /// a text file.</para>
    /// <para>This function supports Unicode filenames, but they must be encoded in UTF-8
    /// format, regardless of the underlying operating system.</para>
    /// <para>In Android, <see cref="IOFromFile"/> can be used to open content:// URIs. As a
    /// fallback, <see cref="IOFromFile"/> will transparently open a matching filename in
    /// the app's <c>assets</c>.</para>
    /// <para>Closing the <see cref="IOStream"/> will close SDL's internal file handle.</para>
    /// <para>The following properties may be set at creation time by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamWindowsHandlePointer"/>: a pointer, that can be cast
    /// to a win32 <c>HANDLE</c>, that this <see cref="IOStream"/> is using to access the
    /// filesystem. If the program isn't running on Windows, or SDL used some
    /// other method to access the filesystem, this property will not be set.</item>
    /// <item><see cref="Props.IOStreamSTDIOFilePointer"/>: a pointer, that can be cast to a
    /// stdio <c>FILE *</c>, that this <see cref="IOStream"/> is using to access the filesystem.
    /// If SDL used some other method to access the filesystem, this property
    /// will not be set. <b>PLEASE NOTE</b> that if SDL is using a different C runtime
    /// than your app, trying to use this pointer will almost certainly result in
    /// a crash! This is mostly a problem on Windows; make sure you build SDL and
    /// your app with the same compiler and settings to avoid it.</item>
    /// <item><see cref="Props.IOStreamAndroidAAssetPointer"/>: a pointer,
    /// that can be cast
    /// to an Android NDK <c>AAsset *</c>, that this <see cref="IOStream"/> is using to access
    /// the filesystem. If SDL used some other method to access the filesystem,
    /// this property will not be set.</item>
    /// </list>
    /// </summary>
    /// <param name="file">a UTF-8 string representing the filename to open.</param>
    /// <param name="mode">an ASCII string representing the mode to be used for opening
    /// the file.</param>
    /// <returns>a pointer to the <see cref="IOStream"/> structure that is created, or <c>NULL</c>
    /// on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IOStream? IOFromFile(string file, string mode)
    {
        var ioFile = SDL_IOFromFile(file, mode);
        return ioFile == IntPtr.Zero ? null : new IOStream(ioFile);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromMem(IntPtr mem, ulong size);
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_IOFromMem(void *mem, size_t size);</code>
    /// <summary>
    /// <para>Use this function to prepare a read-write memory buffer for use with
    /// <see cref="IOStream"/>.</para>
    /// <para>This function sets up an <see cref="IOStream"/> struct based on a memory area of a
    /// certain size, for both read and write access.</para>
    /// <para>This memory buffer is not copied by the <see cref="IOStream"/>; the pointer you
    /// provide must remain valid until you close the stream. Closing the stream
    /// will not free the original buffer.</para>
    /// <para>If you need to make sure the <see cref="IOStream"/> never writes to the memory
    /// buffer, you should use <see cref="IOFromConstMem"/> with a read-only buffer of
    /// memory instead.</para>
    /// </summary>
    /// <param name="mem">a pointer to a buffer to feed an <see cref="IOStream"/> stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <returns>a pointer to a new <see cref="IOStream"/> structure, or <c>NULL</c> if it fails;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="IOFromConstMem"/>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IOStream? IOFromMem(IntPtr mem, ulong size)
    {
        var ioFromMem = SDL_IOFromMem(mem, size);
        return ioFromMem == IntPtr.Zero ? null : new IOStream(ioFromMem);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromConstMem(IntPtr mem, ulong size);
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_IOFromConstMem(const void *mem, size_t size);</code>
    /// <summary>
    /// <para>Use this function to prepare a read-only memory buffer for use with
    /// <see cref="IOStream"/>.</para>
    /// <para>This function sets up an <see cref="IOStream"/> struct based on a memory area of a
    /// certain size. It assumes the memory area is not writable.</para>
    /// <para>Attempting to write to this <see cref="IOStream"/> stream will report an error
    /// without writing to the memory buffer.</para>
    /// <para>This memory buffer is not copied by the <see cref="IOStream"/>; the pointer you
    /// provide must remain valid until you close the stream. Closing the stream
    /// will not free the original buffer.</para>
    /// <para>If you need to write to a memory buffer, you should use <see cref="IOFromMem"/>
    /// with a writable buffer of memory instead.</para>
    /// </summary>
    /// <param name="mem">a pointer to a read-only buffer to feed an <see cref="IOStream"/> stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <returns>a pointer to a new <see cref="IOStream"/> structure, or <c>NULL</c> if it fails;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="IOFromMem"/>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    public static IOStream? IOFromConstMem(IntPtr mem, ulong size)
    {
        var ioFromConstMem = SDL_IOFromConstMem(mem, size);
        return ioFromConstMem == IntPtr.Zero ? null : new IOStream(ioFromConstMem);
    }
        
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_IOFromDynamicMem();
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_IOFromDynamicMem(void);</code>
    /// <summary>
    /// <para>Use this function to create an <see cref="IOStream"/> that is backed by dynamically
    /// allocated memory.</para>
    /// <para>This supports the following properties to provide access to the memory and
    /// control over allocations:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.IOStreamDynamicMemoryPointer"/>: a pointer to the internal
    /// memory of the stream. This can be set to <c>NULL</c> to transfer ownership of
    /// the memory to the application, which should free the memory with
    /// <see cref="Free"/>. If this is done, the next operation on the stream must be
    /// <see cref="CloseIO"/>.</item>
    /// <item><see cref="Props.IOStreamDynamicChunkSizeNumber"/>: memory will be allocated in
    /// multiples of this size, defaulting to <c>1024</c>.</item>
    /// </list>
    /// </summary>
    /// <returns>a pointer to a new <see cref="IOStream"/> structure, or <c>NULL</c> if it fails;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="TellIO"/>
    /// <seealso cref="WriteIO"/>
    public static IOStream? IOFromDynamicMem()
    {
        var ioFromDynamicMem = SDL_IOFromDynamicMem();
        return ioFromDynamicMem == IntPtr.Zero ? null : new IOStream(ioFromDynamicMem);
    }
    
    [DllImport(SDLLibrary, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern IntPtr SDL_OpenIO([In] in IOStreamInterface iface, IntPtr userdata);
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_OpenIO(const SDL_IOStreamInterface *iface, void *userdata);</code>
    /// <summary>
    /// <para>Create a custom <see cref="IOStream"/>.</para>
    /// <para>Applications do not need to use this function unless they are providing
    /// their own <see cref="IOStream"/> implementation. If you just need an <see cref="IOStream"/> to
    /// read/write a common data source, you should use the built-in
    /// implementations in SDL, like <see cref="IOFromFile"/> or <see cref="IOFromMem"/>, etc.</para>
    /// <para>You must free the returned pointer with <see cref="CloseIO"/>.</para>
    /// <para>This function makes a copy of <c>iface</c> and the caller does not need to keep
    /// this data around after this call.</para>
    /// </summary>
    /// <param name="iface">the function pointers that implement this <see cref="IOStream"/>.</param>
    /// <param name="userdata">the app-controlled pointer that is passed to iface's
    /// functions when called.</param>
    /// <returns>a pointer to the allocated memory on success, or <c>NULL</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="CloseIO"/>
    /// <seealso cref="IOFromConstMem"/>
    /// <seealso cref="IOFromFile"/>
    /// <seealso cref="IOFromMem"/>
    public static IOStream? OpenIO(IOStreamInterface iface, IntPtr userdata)
    {
        var openIOPtr = SDL_OpenIO(iface, userdata);
        return openIOPtr == IntPtr.Zero ? null : new IOStream(openIOPtr);
    }
        

    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CloseIO(IntPtr context);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CloseIO(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Close and free an allocated <see cref="IOStream"/> structure.</para>
    /// <para><see cref="CloseIO"/> closes and cleans up the <see cref="IOStream"/> stream. It releases any
    /// resources used by the stream and frees the <see cref="IOStream"/> itself. This
    /// returns 0 on success, or -1 if the stream failed to flush to its output
    /// (e.g. to disk).</para>
    /// <para>Note that if this fails to flush the stream to disk, this function reports
    /// an error, but the <see cref="IOStream"/> is still invalid once this function returns.</para>
    /// </summary>
    /// <param name="context"><see cref="IOStream"/> structure to close.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="OpenIO"/>
    public static int CloseIO(IOStream context) => SDL_CloseIO(context.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetIOProperties(IntPtr context);
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetIOProperties(SDL_IOStream *context);</code>
    /// <summary>
    /// Get the properties associated with an <see cref="IOStream"/>.
    /// </summary>
    /// <param name="context">a pointer to an <see cref="IOStream"/> structure.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetIOProperties(IOStream context) => SDL_GetIOProperties(context.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStatus SDL_GetIOStatus(IntPtr context);
    /// <code>extern SDL_DECLSPEC SDL_IOStatus SDLCALL SDL_GetIOStatus(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Query the stream status of an <see cref="IOStream"/>.</para>
    /// <para>This information can be useful to decide if a short read or write was due
    /// to an error, an EOF, or a non-blocking operation that isn't yet ready to
    /// complete.</para>
    /// <para>An <see cref="IOStream"/> status is only expected to change after a <see cref="ReadIO"/> or
    /// <see cref="WriteIO"/> call; don't expect it to change if you just call this query
    /// function in a tight loop.</para>
    /// </summary>
    /// <param name="context">the <see cref="IOStream"/> to query.</param>
    /// <returns>an <see cref="IOStatus"/> enum with the current state.</returns>
    /// <threadsafety>This function should not be called at the same time that
    /// another thread is operating on the same <see cref="IOStream"/>.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static IOStatus GetIOStatus(IOStream context) => SDL_GetIOStatus(context.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetIOSize(IntPtr context);
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_GetIOSize(SDL_IOStream *context);</code>
    /// <summary>
    /// Use this function to get the size of the data stream in an <see cref="IOStream"/>.
    /// </summary>
    /// <param name="context">the <see cref="IOStream"/> to get the size of the data stream from.</param>
    /// <returns>the size of the data stream in the SDL_IOStream on success or a
    /// negative error code on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static long GetIOSize(IOStream context) => SDL_GetIOSize(context.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_SeekIO(IntPtr context, long offset, IOWhence whence);
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_SeekIO(SDL_IOStream *context, Sint64 offset, SDL_IOWhence whence);</code>
    /// <summary>
    /// Seek within an <see cref="IOStream"/> data stream.
    /// <para>This function seeks to byte <c>offset</c>, relative to <c>whence</c>.</para>
    /// <para><c>whence</c> may be any of the following values:</para>
    /// <list type="bullet">
    /// <item><see cref="IOWhence.Set"/>: seek from the beginning of data</item>
    /// <item><see cref="IOWhence.Cur"/>: seek relative to current read point</item>
    /// <item><see cref="IOWhence.End"/>: seek relative to the end of data</item>
    /// </list>
    /// <para>If this stream can not seek, it will return -1.</para>
    /// </summary>
    /// <param name="context">a pointer to an <see cref="IOStream"/> structure.</param>
    /// <param name="offset">an offset in bytes, relative to <c>whence</c> location; can be
    /// negative.</param>
    /// <param name="whence">any of <see cref="IOWhence.Set"/>, <see cref="IOWhence.Cur"/>,
    /// <see cref="IOWhence.End"/>.</param>
    /// <returns>the final offset in the data stream after the seek or a negative
    /// error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="TellIO"/>
    public static long SeekIO(IOStream context, long offset, IOWhence whence) => 
        SDL_SeekIO(context.Handle, offset, whence);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_TellIO(IntPtr context);
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_TellIO(SDL_IOStream *context);</code>
    /// <summary>
    /// <para>Determine the current read/write offset in an <see cref="IOStream"/> data stream.</para>
    /// <para><see cref="TellIO"/> is actually a wrapper function that calls the <see cref="IOStream"/>
    /// <c>seek</c> method, with an offset of 0 bytes from <see cref="IOWhence.Cur"/>, to
    /// simplify application development.</para>
    /// </summary>
    /// <param name="context">an <see cref="IOStream"/> data stream object from which to get the
    /// current offset.</param>
    /// <returns>the current offset in the stream, or -1 if the information can not
    /// be determined.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SeekIO"/>
    public static long TellIO(IOStream context) => SDL_TellIO(context.Handle);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_ReadIO(IntPtr context, IntPtr ptr, ulong size);
    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_ReadIO(SDL_IOStream *context, void *ptr, size_t size);</code>
    /// <summary>
    /// <para>Read from a data source.</para>
    /// <para>This function reads up <c>size</c> bytes from the data source to the area
    /// pointed at by <c>ptr</c>. This function may read less bytes than requested. It
    /// will return zero when the data stream is completely read, or on error. To
    /// determine if there was an error or all data was read, call
    /// <see cref="GetIOStatus"/>.</para>
    /// </summary>
    /// <param name="context">a pointer to an <see cref="IOStream"/> structure.</param>
    /// <param name="ptr">a pointer to a buffer to read data into.</param>
    /// <param name="size">the number of bytes to read from the data source.</param>
    /// <returns>the number of bytes read, or 0 on end of file or other error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="WriteIO"/>
    /// <seealso cref="GetIOStatus"/>
    public static nuint ReadIO(IOStream context, IntPtr ptr, ulong size) =>
        SDL_ReadIO(context.Handle, ptr, size);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint SDL_WriteIO(IntPtr context, IntPtr ptr, ulong size);
    /// <code>extern SDL_DECLSPEC size_t SDLCALL SDL_WriteIO(SDL_IOStream *context, const void *ptr, size_t size);</code>
    /// <summary>
    /// <para>Write to an <see cref="IOStream"/> data stream.</para>
    /// <para>This function writes exactly <c>size</c> bytes from the area pointed at by <c>ptr</c>
    /// to the stream. If this fails for any reason, it'll return less than <c>size</c>
    /// to demonstrate how far the write progressed. On success, it returns <c>size</c>.</para>
    /// <para>On error, this function still attempts to write as much as possible, so it
    /// might return a positive value less than the requested write size.</para>
    /// <para>The caller can use <see cref="GetIOStatus"/> to determine if the problem is
    /// recoverable, such as a non-blocking write that can simply be retried later,
    /// or a fatal error.</para>
    /// </summary>
    /// <param name="context">a pointer to an <see cref="IOStream"/> structure.</param>
    /// <param name="ptr">a pointer to a buffer containing data to write.</param>
    /// <param name="size">the number of bytes to write.</param>
    /// <returns>the number of bytes written, which will be less than <c>size</c> on
    /// error; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ReadIO"/>
    /// <seealso cref="SeekIO"/>
    /// <seealso cref="GetIOStatus"/>
    public static nuint WriteIO(IOStream context, IntPtr ptr, ulong size) =>
        SDL_WriteIO(context.Handle, ptr, size);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_LoadFile_IO(IntPtr src, out ulong datasize, [MarshalAs(UnmanagedType.I1)] bool closeio);
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_LoadFile_IO(SDL_IOStream *src, size_t *datasize, SDL_bool closeio);</code>
    /// <summary>
    /// <para>Load all the data from an SDL data stream.</para>
    /// <para>The data is allocated with a zero byte at the end (null terminated) for
    /// convenience. This extra byte is not included in the value reported via
    /// <c>datasize</c>.</para>
    /// <para>The data should be freed with <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="src">the <see cref="IOStream"/> to read all available data from.</param>
    /// <param name="datasize">if not <c>NULL</c>, will store the number of bytes read.</param>
    /// <param name="closeio">if <c>true</c>, calls <see cref="CloseIO"/> on <c>src</c> before returning,
    /// even in the case of an error.</param>
    /// <returns>the data, or <c>NULL</c> if there was an error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LoadFile"/>
    public static byte[]? LoadFileIO(IOStream src, out ulong datasize, bool closeio)
    {
        var ptr = SDL_LoadFile_IO(src.Handle, out datasize, closeio);
        if (ptr == IntPtr.Zero)
        {
            return null;
        }

        try
        {
            var data = new byte[datasize];
            Marshal.Copy(ptr, data, 0, (int)datasize);
            return data;
        }
        finally
        {
            Free(ptr);
        }
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_LoadFile([MarshalAs(UnmanagedType.LPUTF8Str)] string file, out ulong datasize);
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_LoadFile(const char *file, size_t *datasize);</code>
    /// <summary>
    /// <para>Load all the data from a file path.</para>
    /// <para>The data is allocated with a zero byte at the end (null terminated) for
    /// convenience. This extra byte is not included in the value reported via
    /// <c>datasize</c>.</para>
    /// <para>The data should be freed with <see cref="Free"/></para>
    /// </summary>
    /// <param name="file">the path to read all available data from.</param>
    /// <param name="datasize">if not NULL, will store the number of bytes read.</param>
    /// <returns>the data, or <c>NULL</c> if there was an error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="LoadFileIO"/>
    public static byte[]? LoadFile(string file, out ulong datasize)
    {
        var ptr =  SDL_LoadFile(file, out datasize);
        
        if (ptr == IntPtr.Zero)
        {
            return null;
        }
        
        try
        {
            var data = new byte[datasize];
            Marshal.Copy(ptr, data, 0, (int)datasize);
            return data;
        }
        finally
        {
            Free(ptr);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU8(IntPtr src, out byte value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU8(SDL_IOStream *src, Uint8 *value);</code>
    /// <summary>
    /// <para>Use this function to read a byte from an <see cref="IOStream"/>.</para>
    /// </summary>
    /// <param name="src">the <see cref="IOStream"/> to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU8(IOStream src, out byte value) => SDL_ReadU8(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS8(IntPtr src, out sbyte value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS8(SDL_IOStream *src, Sint8 *value);</code>
    /// <summary>
    /// <para>Use this function to read a signed byte from an <see cref="IOStream"/>.</para>
    /// </summary>
    /// <param name="src">the <see cref="IOStream"/> to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS8(IOStream src, out sbyte value) => SDL_ReadS8(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU16LE(IntPtr src, out ushort value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU16LE(SDL_IOStream *src, Uint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of little-endian data from an
    /// <see cref="IOStream"/> and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU16LE(IOStream src, out ushort value) => SDL_ReadU16LE(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS16LE(IntPtr src, out short value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS16LE(SDL_IOStream *src, Sint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of little-endian data from an
    /// <see cref="IOStream"/> and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS16LE(IOStream src, out short value) => SDL_ReadS16LE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU16BE(IntPtr src, out ushort value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU16BE(SDL_IOStream *src, Uint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of big-endian data from an SDL_IOStream
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU16BE(IOStream src, out ushort value) => SDL_ReadU16BE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS16BE(IntPtr src, out short value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS16BE(SDL_IOStream *src, Sint16 *value);</code>
    /// <summary>
    /// <para>Use this function to read 16 bits of big-endian data from an <see cref="IOStream"/>
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS16BE(IOStream src, out short value) => SDL_ReadS16BE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU32LE(IntPtr src, out uint value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU32LE(SDL_IOStream *src, Uint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of little-endian data from an
    /// <see cref="IOStream"/> and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU32LE(IOStream src, out uint value) => SDL_ReadU32LE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS32LE(IntPtr src, out int value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS32LE(SDL_IOStream *src, Sint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of little-endian data from an
    /// <see cref="IOStream"/> and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS32LE(IOStream src, out int value) => SDL_ReadS32LE(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU32BE(IntPtr src, out uint value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU32BE(SDL_IOStream *src, Uint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of big-endian data from an <see cref="IOStream"/>
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU32BE(IOStream src, out uint value) => SDL_ReadU32BE(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS32BE(IntPtr src, out int value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS32BE(SDL_IOStream *src, Sint32 *value);</code>
    /// <summary>
    /// <para>Use this function to read 32 bits of big-endian data from an <see cref="IOStream"/>
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS32BE(IOStream src, out int value) => SDL_ReadS32BE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU64LE(IntPtr src, out ulong value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU64LE(SDL_IOStream *src, Uint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of little-endian data from an
    /// <see cref="IOStream"/> and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU64LE(IOStream src, out ulong value) => SDL_ReadU64LE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS64LE(IntPtr src, out long value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS64LE(SDL_IOStream *src, Sint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of little-endian data fro
    /// <see cref="IOStream"/> and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS64LE(IOStream src, out long value) => SDL_ReadS64LE(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadU64BE(IntPtr src, out ulong value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadU64BE(SDL_IOStream *src, Uint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of big-endian data from an <see cref="IOStream"/>
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadU64BE(IOStream src, out ulong value) => SDL_ReadU64BE(src.Handle, out value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ReadS64BE(IntPtr src, out long value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_ReadS64BE(SDL_IOStream *src, Sint64 *value);</code>
    /// <summary>
    /// <para>Use this function to read 64 bits of big-endian data from an <see cref="IOStream"/>
    /// and return in native format.</para>
    /// <para>SDL byteswaps the data only if necessary, so the data returned will be in
    /// the native byte order.</para>
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool ReadS64BE(IOStream src, out long value) => SDL_ReadS64BE(src.Handle, out value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU8(IntPtr dst, byte value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU8(SDL_IOStream *dst, Uint8 value);</code>
    /// <summary>
    /// Use this function to write a byte to an <see cref="IOStream"/>.
    /// </summary>
    /// <param name="dst">the <see cref="IOStream"/> to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU8(IOStream dst, byte value) => SDL_WriteU8(dst.Handle, value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS8(IntPtr dst, sbyte value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS8(SDL_IOStream *dst, Sint8 value);</code>
    /// <summary>
    /// Use this function to write a signed byte to an <see cref="IOStream"/>.
    /// </summary>
    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS8(IOStream dst, sbyte value) => SDL_WriteS8(dst.Handle, value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU16LE(IntPtr dst, ushort value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU16LE(SDL_IOStream *dst, Uint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an <see cref="IOStream"/> as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU16LE(IOStream dst, ushort value) => SDL_WriteU16LE(dst.Handle, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS16LE(IntPtr dst, short value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS16LE(SDL_IOStream *dst, Sint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an <see cref="IOStream"/> as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS16LE(IOStream dst, short value) => SDL_WriteS16LE(dst.Handle, value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU16BE(IntPtr dst, ushort value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU16BE(SDL_IOStream *dst, Uint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an <see cref="IOStream"/> as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU16BE(IOStream dst, ushort value) => SDL_WriteU16BE(dst.Handle, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS16BE(IntPtr dst, short value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS16BE(SDL_IOStream *dst, Sint16 value);</code>
    /// <summary>
    /// <para>Use this function to write 16 bits in native format to an <see cref="IOStream"/> as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS16BE(IOStream dst, short value) => SDL_WriteS16BE(dst.Handle, value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU32LE(IntPtr dst, uint value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU32LE(SDL_IOStream *dst, Uint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an <see cref="IOStream"/> as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU32LE(IOStream dst, uint value) => SDL_WriteU32LE(dst.Handle, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS32LE(IntPtr dst, int value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS32LE(SDL_IOStream *dst, Sint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an SDL_IOStream as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS32LE(IOStream dst, int value) => SDL_WriteS32LE(dst.Handle, value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU32BE(IntPtr dst, uint value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU32BE(SDL_IOStream *dst, Uint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an <see cref="IOStream"/> as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU32BE(IOStream dst, uint value) => SDL_WriteU32BE(dst.Handle, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS32BE(IntPtr dst, int value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS32BE(SDL_IOStream *dst, Sint32 value);</code>
    /// <summary>
    /// <para>Use this function to write 32 bits in native format to an <see cref="IOStream"/> as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS32BE(IOStream dst, int value) => SDL_WriteS32BE(dst.Handle, value);


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU64LE(IntPtr dst, ulong value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU64LE(SDL_IOStream *dst, Uint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an <see cref="IOStream"/> as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU64LE(IOStream dst, ulong value) => SDL_WriteU64LE(dst.Handle, value);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS64LE(IntPtr dst, long value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS64LE(SDL_IOStream *dst, Sint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an <see cref="IOStream"/> as
    /// little-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in little-endian
    /// format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS64LE(IOStream dst, long value) => SDL_WriteS64LE(dst.Handle, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteU64BE(IntPtr dst, ulong value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteU64BE(SDL_IOStream *dst, Uint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an <see cref="IOStream"/> as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteU64BE(IOStream dst, ulong value) => SDL_WriteU64BE(dst.Handle, value);


    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_WriteS64BE(IntPtr dst, long value);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_WriteS64BE(SDL_IOStream *dst, Sint64 value);</code>
    /// <summary>
    /// <para>Use this function to write 64 bits in native format to an <see cref="IOStream"/> as
    /// big-endian data.</para>
    /// <para>SDL byteswaps the data only if necessary, so the application always
    /// specifies native format, and the data written will be in big-endian format.</para>
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns><c>true</c> on successful write, <c>false</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool WriteS64BE(IOStream dst, long value) => SDL_WriteS64BE(dst.Handle, value);

}