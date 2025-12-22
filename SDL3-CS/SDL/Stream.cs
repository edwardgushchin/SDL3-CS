using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// The default maximum number of bytes allowed to be read from a <see cref="Stream"/> when creating an <c>SDL_IOStream</c>.
    /// This acts as a safety guard against accidentally reading unbounded or very large streams into memory.
    /// </summary>
    public const int DefaultMaxIOFromStreamBytes = 64 * 1024 * 1024; // 64 MB

    /// <summary>
    /// Creates an <c>SDL_IOStream</c> over the contents of a managed <see cref="Stream"/>.
    /// The input stream is read (starting at its current <see cref="Stream.Position"/>) into a managed buffer that is pinned
    /// for the lifetime of the returned owner. This is intended for APIs such as <c>*_Load_IO</c> that consume an <c>SDL_IOStream</c>.
    /// </summary>
    /// <param name="stream">
    /// The source stream. Reading starts from the current <see cref="Stream.Position"/>.
    /// </param>
    /// <param name="maxBytes">
    /// Maximum allowed number of bytes to read from <paramref name="stream"/> (memory safety guard).
    /// </param>
    /// <param name="closeIoOnDispose">
    /// If <c>true</c>, <see cref="CloseIO"/> will be called for the created <c>SDL_IOStream</c> when the returned object is disposed.
    /// This is typically the correct choice when passing <c>closeio:false</c> to SDL APIs that read from the I/O stream.
    /// </param>
    /// <returns>
    /// An owner object that holds the created <c>SDL_IOStream</c> and keeps the backing buffer pinned for its lifetime.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="stream"/> does not support reading.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when more than <paramref name="maxBytes"/> bytes would be read.</exception>
    /// <exception cref="IOException">Thrown when SDL fails to create the <c>SDL_IOStream</c>.</exception>
    public static IOStreamOwner IOFromStream(Stream stream, int maxBytes = DefaultMaxIOFromStreamBytes, bool closeIoOnDispose = true)
    {
        if (stream is null) throw new ArgumentNullException(nameof(stream), "The stream must not be null.");
        if (!stream.CanRead) throw new ArgumentException("The stream must be readable.", nameof(stream));
        if (maxBytes <= 0) throw new ArgumentOutOfRangeException(nameof(maxBytes), "The size limit must be greater than zero.");

        var (buffer, offset, count) = ReadAllToBuffer(stream, maxBytes);

        // Pin the managed buffer so the GC cannot move it while SDL reads from it.
        var pinned = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var basePtr = pinned.AddrOfPinnedObject();
        var dataPtr = offset == 0 ? basePtr : IntPtr.Add(basePtr, offset);

        var io = IOFromConstMem(dataPtr, (nuint)count);
        if (io != IntPtr.Zero) return new IOStreamOwner(io, pinned, count, closeIoOnDispose);

        pinned.Free();
        throw new IOException($"Failed to create an SDL_IOStream from memory: {GetError()}");
    }

    /// <summary>
    /// Wraps an existing <c>SDL_IOStream</c> as a managed <see cref="Stream"/>.
    /// This is useful when SDL returns an <c>SDL_IOStream</c> and you want to consume it using standard .NET stream APIs.
    /// </summary>
    /// <param name="io">A pointer to an existing <c>SDL_IOStream</c>.</param>
    /// <param name="leaveOpen">
    /// If <c>true</c>, the underlying <c>SDL_IOStream</c> will not be closed when the returned managed stream is disposed.
    /// </param>
    /// <returns>A managed stream wrapper over the provided <c>SDL_IOStream</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="io"/> is <see cref="IntPtr.Zero"/>.</exception>
    public static Stream AsStream(IntPtr io, bool leaveOpen = false)
    {
        return io == IntPtr.Zero
            ? throw new ArgumentException("The SDL_IOStream handle must not be null.", nameof(io))
            : new IOStream(io, leaveOpen);
    }

    /// <summary>
    /// Owns an <c>SDL_IOStream</c> created from a managed buffer.
    /// While this object is alive, the buffer remains pinned to ensure SDL can safely read from it.
    /// </summary>
    public sealed class IOStreamOwner : IDisposable
    {
        private GCHandle _pinned;
        private readonly bool _closeIoOnDispose;
        private bool _disposed;

        internal IOStreamOwner(IntPtr io, GCHandle pinned, int length, bool closeIoOnDispose)
        {
            Handle = io;
            Length = length;
            _pinned = pinned;
            _closeIoOnDispose = closeIoOnDispose;
        }

        /// <summary>A pointer to the owned <c>SDL_IOStream</c>.</summary>
        public IntPtr Handle { get; }

        /// <summary>The number of bytes available in the I/O stream.</summary>
        public int Length { get; }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                if (_closeIoOnDispose && Handle != IntPtr.Zero)
                    CloseIO(Handle);
            }
            finally
            {
                if (_pinned.IsAllocated)
                    _pinned.Free();
            }
        }
    }

    private static (byte[] buffer, int offset, int count) ReadAllToBuffer(Stream stream, int maxBytes)
    {
        // Fast path: MemoryStream can expose the backing array without additional copies.
        if (stream is MemoryStream ms && ms.TryGetBuffer(out var seg))
        {
            // Use remaining bytes starting from the current position.
            var pos = (int)ms.Position;
            var remaining = (int)(ms.Length - ms.Position);
            if (remaining < 0) remaining = 0;

            if (remaining > maxBytes)
                throw new ArgumentOutOfRangeException(nameof(stream),
                    $"The data is too large: {remaining} bytes (limit is {maxBytes} bytes).");

            return (seg.Array!, seg.Offset + pos, remaining);
        }

        // For seekable streams, we can pre-check the remaining length.
        if (stream.CanSeek)
        {
            var remaining = stream.Length - stream.Position;
            if (remaining > maxBytes)
                throw new ArgumentOutOfRangeException(nameof(stream),
                    $"The data is too large: {remaining} bytes (limit is {maxBytes} bytes).");
        }

        // Clear and predictable path: copy into a MemoryStream, then try to expose its backing buffer.
        using var tmp = stream.CanSeek
            ? new MemoryStream(capacity: (int)Math.Min(maxBytes, Math.Max(0, stream.Length - stream.Position)))
            : new MemoryStream();

        stream.CopyTo(tmp);

        if (tmp.Length > maxBytes)
            throw new ArgumentOutOfRangeException(nameof(stream),
                $"The data is too large: {tmp.Length} bytes (limit is {maxBytes} bytes).");

        return !tmp.TryGetBuffer(out var seg2) ? (tmp.ToArray(), 0, (int)tmp.Length) : (seg2.Array!, seg2.Offset, (int)tmp.Length);
    }

    /// <summary>
    /// Managed <see cref="Stream"/> wrapper over an <c>SDL_IOStream</c>.
    /// This implementation pins the managed buffer for each read/write call (no unsafe code required).
    /// </summary>
    private sealed class IOStream(IntPtr io, bool leaveOpen) : Stream
    {
        private bool _disposed;

        public override bool CanRead => !_disposed;
        public override bool CanSeek => !_disposed;
        public override bool CanWrite => !_disposed;

        public override long Length
        {
            get
            {
                ThrowIfDisposed();
                var len = GetIOSize(io);
                return len < 0 ? throw new IOException($"Failed to get SDL_IOStream size: {GetError()}") : len;
            }
        }

        public override long Position
        {
            get
            {
                ThrowIfDisposed();
                var pos = TellIO(io);
                return pos < 0 ? throw new IOException($"Failed to get SDL_IOStream position: {GetError()}") : pos;
            }
            set => Seek(value, SeekOrigin.Begin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            ValidateBuffer(buffer, offset, count);

            if (count == 0) return 0;

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
                var n = ReadIO(io, ptr, (nuint)count);
                return checked((int)n);
            }
            finally
            {
                handle.Free();
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            ValidateBuffer(buffer, offset, count);

            if (count == 0) return;

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
                var n = WriteIO(io, ptr, (nuint)count);
                if (n != (nuint)count)
                    throw new IOException($"Failed to write to SDL_IOStream: {GetError()}");
            }
            finally
            {
                handle.Free();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            ThrowIfDisposed();

            var whence = origin switch
            {
                SeekOrigin.Begin => IOWhence.Set,
                SeekOrigin.Current => IOWhence.Cur,
                SeekOrigin.End => IOWhence.End,
                _ => throw new ArgumentOutOfRangeException(nameof(origin), origin, "Unknown seek origin.")
            };

            var pos = SeekIO(io, offset, whence);
            return pos < 0 ? throw new IOException($"Failed to seek in SDL_IOStream: {GetError()}") : pos;
        }

        public override void Flush()
        {
            // SDL_IOStream is typically not buffered from the .NET side.
        }

        public override void SetLength(long value) =>
            throw new NotSupportedException("Changing the length of an SDL_IOStream is not supported.");

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;

            if (!leaveOpen)
                CloseIO(io);

            base.Dispose(disposing);
        }

        private static void ValidateBuffer(byte[] buffer, int offset, int count)
        {
            if (buffer is null) throw new ArgumentNullException(nameof(buffer), "The buffer must not be null.");
            if ((uint)offset > (uint)buffer.Length) throw new ArgumentOutOfRangeException(nameof(offset), "The offset is outside the buffer bounds.");
            if ((uint)count > (uint)(buffer.Length - offset)) throw new ArgumentOutOfRangeException(nameof(count), "The count is outside the buffer bounds.");
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(IOStream));
        }
    }
}
