using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL;

internal static class StreamTests
{
    private static IntPtr capturedMem;
    private static UIntPtr capturedSize;
    private static byte[]? capturedMemoryBytes;
    private static IntPtr capturedContext;
    private static IntPtr capturedReadPointer;
    private static IntPtr capturedWritePointer;
    private static long capturedOffset;
    private static SDL3.SDL.IOWhence capturedWhence;
    private static UIntPtr capturedIOSize;
    private static byte[]? capturedWriteBytes;
    private static IntPtr nextPointer;
    private static IntPtr nextErrorPointer;
    private static long nextLong;
    private static ulong nextULong;
    private static bool nextBool;
    private static byte[]? nextReadBytes;
    private static int capturedCallCount;
    private static int capturedCloseCallCount;

    public static void RunAll()
    {
        IOFromStream_ValidatesArgumentsCreatesOwnerAndReportsFailure();
        AsStream_RejectsNullAndCreatesManagedStream();
        IOStreamOwner_DisposeClosesWhenRequestedAndIsIdempotent();
        ReadAllToBuffer_CoversMemorySeekableAndNonSeekableStreams();
        IOStream_LengthAndPosition_CoverNativeSuccessAndFailure();
        IOStream_Read_CoversZeroReadNativeReadAndValidation();
        IOStream_Write_CoversZeroWriteNativeWriteFailureAndValidation();
        IOStream_Seek_CoversOriginsInvalidOriginAndFailure();
        IOStream_Flush_IsNoOp();
        IOStream_SetLength_ThrowsNotSupported();
        IOStream_Dispose_ClosesOrLeavesOpenAndUpdatesCapabilities();
        IOStream_ThrowIfDisposed_ThrowsForOperationsAfterDispose();
    }

    public static void IOFromStream_ValidatesArgumentsCreatesOwnerAndReportsFailure()
    {
        AssertThrows<ArgumentNullException>(() => SDL3.SDL.IOFromStream(null!), "SDL.IOFromStream must reject null streams.");
        AssertThrows<ArgumentException>(() => SDL3.SDL.IOFromStream(new ReadOnlyTestStream([1], canRead: false, canSeek: true)), "SDL.IOFromStream must reject unreadable streams.");
        AssertThrows<ArgumentOutOfRangeException>(() => SDL3.SDL.IOFromStream(new MemoryStream(), 0), "SDL.IOFromStream must reject non-positive maxBytes.");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7001;
        using MemoryStream source = new([9, 1, 2, 3], 0, 4, writable: false, publiclyVisible: true);
        source.Position = 1;

        using (NativeHookScope.Install("IOFromConstMemNativeFunction", nameof(CaptureIOFromConstMem)))
        using (SDL3.SDL.IOStreamOwner owner = SDL3.SDL.IOFromStream(source, maxBytes: 10, closeIoOnDispose: false))
        {
            TestAssert.Equal((IntPtr)0x7001, owner.Handle, "SDL.IOFromStream must return the native IO handle.");
            TestAssert.Equal(3, owner.Length, "SDL.IOFromStream must preserve the remaining byte count.");
            TestAssert.Equal((UIntPtr)3, capturedSize, "SDL.IOFromStream must pass the remaining byte count to IOFromConstMem.");
            AssertBytes([1, 2, 3], capturedMemoryBytes, "SDL.IOFromStream must pass bytes from the current stream position.");
        }

        ResetCaptureState();
        IntPtr errorPointer = Marshal.StringToCoTaskMemUTF8("create failed");
        nextErrorPointer = errorPointer;

        try
        {
            using NativeHookScope ioHook = NativeHookScope.Install("IOFromConstMemNativeFunction", nameof(CaptureIOFromConstMem));
            using NativeHookScope errorHook = NativeHookScope.Install("GetErrorNativeFunction", nameof(CaptureGetError));

            IOException ex = AssertThrows<IOException>(() => SDL3.SDL.IOFromStream(new MemoryStream([1, 2, 3]), maxBytes: 10), "SDL.IOFromStream must throw when SDL cannot create IO.");
            TestAssert.True(ex.Message.Contains("create failed", StringComparison.Ordinal), "SDL.IOFromStream must include SDL.GetError text.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(errorPointer);
        }
    }

    public static void AsStream_RejectsNullAndCreatesManagedStream()
    {
        AssertThrows<ArgumentException>(() => SDL3.SDL.AsStream(IntPtr.Zero), "SDL.AsStream must reject a null IO handle.");

        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7002, leaveOpen: true);

        TestAssert.Equal(true, stream.CanRead, "SDL.AsStream must create a readable stream.");
        TestAssert.Equal(true, stream.CanSeek, "SDL.AsStream must create a seekable stream.");
        TestAssert.Equal(true, stream.CanWrite, "SDL.AsStream must create a writable stream.");
    }

    public static void IOStreamOwner_DisposeClosesWhenRequestedAndIsIdempotent()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x7003;
        nextBool = true;

        using NativeHookScope ioHook = NativeHookScope.Install("IOFromConstMemNativeFunction", nameof(CaptureIOFromConstMem));
        using NativeHookScope closeHook = NativeHookScope.Install("CloseIONativeFunction", nameof(CaptureCloseIO));

        SDL3.SDL.IOStreamOwner owner = SDL3.SDL.IOFromStream(new MemoryStream([1, 2]), closeIoOnDispose: true);
        owner.Dispose();
        owner.Dispose();

        TestAssert.Equal((IntPtr)0x7003, capturedContext, "SDL.IOStreamOwner.Dispose must close the owned IO handle.");
        TestAssert.Equal(1, capturedCloseCallCount, "SDL.IOStreamOwner.Dispose must close only once.");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7004;
        using NativeHookScope ioHook2 = NativeHookScope.Install("IOFromConstMemNativeFunction", nameof(CaptureIOFromConstMem));
        using NativeHookScope closeHook2 = NativeHookScope.Install("CloseIONativeFunction", nameof(CaptureCloseIO));

        using SDL3.SDL.IOStreamOwner leaveOpenOwner = SDL3.SDL.IOFromStream(new MemoryStream([3, 4]), closeIoOnDispose: false);

        leaveOpenOwner.Dispose();
        TestAssert.Equal(0, capturedCloseCallCount, "SDL.IOStreamOwner.Dispose must not close when closeIoOnDispose is false.");
    }

    public static void ReadAllToBuffer_CoversMemorySeekableAndNonSeekableStreams()
    {
        using MemoryStream memory = new([0, 1, 2, 3], 0, 4, writable: false, publiclyVisible: true);
        memory.Position = 2;
        var memoryResult = InvokeReadAllToBuffer(memory, maxBytes: 4);
        TestAssert.Equal(2, memoryResult.count, "SDL.ReadAllToBuffer must use remaining MemoryStream bytes.");
        TestAssert.Equal(2, memoryResult.offset, "SDL.ReadAllToBuffer must preserve MemoryStream offset and position.");
        AssertBufferSegment([2, 3], memoryResult.buffer, memoryResult.offset, memoryResult.count, "SDL.ReadAllToBuffer must expose MemoryStream bytes.");

        using MemoryStream beyondEnd = new([1, 2], 0, 2, writable: false, publiclyVisible: true);
        beyondEnd.Position = 5;
        var emptyResult = InvokeReadAllToBuffer(beyondEnd, maxBytes: 4);
        TestAssert.Equal(0, emptyResult.count, "SDL.ReadAllToBuffer must clamp negative remaining MemoryStream length to zero.");

        AssertThrows<ArgumentOutOfRangeException>(() => InvokeReadAllToBuffer(new MemoryStream([1, 2, 3], 0, 3, writable: false, publiclyVisible: true), maxBytes: 2), "SDL.ReadAllToBuffer must reject oversized MemoryStream input.");

        using ReadOnlyTestStream seekable = new([5, 6, 7, 8], canRead: true, canSeek: true);
        seekable.Position = 1;
        var seekableResult = InvokeReadAllToBuffer(seekable, maxBytes: 3);
        TestAssert.Equal(3, seekableResult.count, "SDL.ReadAllToBuffer must copy seekable stream remaining bytes.");
        AssertBufferSegment([6, 7, 8], seekableResult.buffer, seekableResult.offset, seekableResult.count, "SDL.ReadAllToBuffer must copy seekable stream bytes.");
        AssertThrows<ArgumentOutOfRangeException>(() => InvokeReadAllToBuffer(new ReadOnlyTestStream([1, 2, 3], canRead: true, canSeek: true), maxBytes: 2), "SDL.ReadAllToBuffer must pre-check oversized seekable streams.");

        using ReadOnlyTestStream nonSeekable = new([9, 10], canRead: true, canSeek: false);
        var nonSeekableResult = InvokeReadAllToBuffer(nonSeekable, maxBytes: 2);
        TestAssert.Equal(2, nonSeekableResult.count, "SDL.ReadAllToBuffer must copy non-seekable streams.");
        AssertBufferSegment([9, 10], nonSeekableResult.buffer, nonSeekableResult.offset, nonSeekableResult.count, "SDL.ReadAllToBuffer must copy non-seekable stream bytes.");
        AssertThrows<ArgumentOutOfRangeException>(() => InvokeReadAllToBuffer(new ReadOnlyTestStream([1, 2, 3], canRead: true, canSeek: false), maxBytes: 2), "SDL.ReadAllToBuffer must reject oversized non-seekable streams after copy.");
    }

    public static void IOStream_LengthAndPosition_CoverNativeSuccessAndFailure()
    {
        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7013, leaveOpen: true);

        ResetCaptureState();
        nextLong = 123;
        using (NativeHookScope.Install("GetIOSizeNativeFunction", nameof(CaptureGetIOSize)))
        {
            long length = stream.Length;

            TestAssert.Equal(123L, length, "SDL.IOStream.Length must return native size.");
            TestAssert.Equal((IntPtr)0x7013, capturedContext, "SDL.IOStream.Length must forward context.");
            TestAssert.Equal(1, capturedCallCount, "SDL.IOStream.Length must call native hook once.");
        }

        ResetCaptureState();
        nextLong = -1;
        IntPtr sizeErrorPointer = Marshal.StringToCoTaskMemUTF8("size failed");
        nextErrorPointer = sizeErrorPointer;

        try
        {
            using NativeHookScope sizeHook = NativeHookScope.Install("GetIOSizeNativeFunction", nameof(CaptureGetIOSize));
            using NativeHookScope errorHook = NativeHookScope.Install("GetErrorNativeFunction", nameof(CaptureGetError));

            IOException ex = AssertThrows<IOException>(() => _ = stream.Length, "SDL.IOStream.Length must throw for negative native sizes.");
            TestAssert.True(ex.Message.Contains("size failed", StringComparison.Ordinal), "SDL.IOStream.Length must include SDL.GetError text.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(sizeErrorPointer);
        }

        ResetCaptureState();
        nextLong = 45;
        using (NativeHookScope.Install("TellIONativeFunction", nameof(CaptureTellIO)))
        {
            long position = stream.Position;

            TestAssert.Equal(45L, position, "SDL.IOStream.Position getter must return native position.");
            TestAssert.Equal((IntPtr)0x7013, capturedContext, "SDL.IOStream.Position getter must forward context.");
            TestAssert.Equal(1, capturedCallCount, "SDL.IOStream.Position getter must call native hook once.");
        }

        ResetCaptureState();
        nextLong = -1;
        IntPtr tellErrorPointer = Marshal.StringToCoTaskMemUTF8("tell failed");
        nextErrorPointer = tellErrorPointer;

        try
        {
            using NativeHookScope tellHook = NativeHookScope.Install("TellIONativeFunction", nameof(CaptureTellIO));
            using NativeHookScope errorHook = NativeHookScope.Install("GetErrorNativeFunction", nameof(CaptureGetError));

            IOException ex = AssertThrows<IOException>(() => _ = stream.Position, "SDL.IOStream.Position getter must throw for negative native positions.");
            TestAssert.True(ex.Message.Contains("tell failed", StringComparison.Ordinal), "SDL.IOStream.Position getter must include SDL.GetError text.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(tellErrorPointer);
        }

        ResetCaptureState();
        nextLong = 77;
        using (NativeHookScope.Install("SeekIONativeFunction", nameof(CaptureSeekIO)))
        {
            stream.Position = 70;

            TestAssert.Equal((IntPtr)0x7013, capturedContext, "SDL.IOStream.Position setter must forward context.");
            TestAssert.Equal(70L, capturedOffset, "SDL.IOStream.Position setter must seek to the requested value.");
            TestAssert.Equal(SDL3.SDL.IOWhence.Set, capturedWhence, "SDL.IOStream.Position setter must use SeekOrigin.Begin.");
            TestAssert.Equal(1, capturedCallCount, "SDL.IOStream.Position setter must call native hook once.");
        }
    }

    public static void IOStream_Read_CoversZeroReadNativeReadAndValidation()
    {
        ResetCaptureState();
        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7005, leaveOpen: true);

        byte[] buffer = [0, 0, 0, 0, 0];
        int zeroRead = stream.Read(buffer, 1, 0);
        TestAssert.Equal(0, zeroRead, "SDL.IOStream.Read must return 0 for zero-count reads.");

        nextReadBytes = [7, 8, 9];
        nextULong = 3;
        using NativeHookScope readHook = NativeHookScope.Install("ReadIONativeFunction", nameof(CaptureReadIO));
        int read = stream.Read(buffer, 1, 3);

        TestAssert.Equal(3, read, "SDL.IOStream.Read must return the native byte count.");
        TestAssert.Equal((IntPtr)0x7005, capturedContext, "SDL.IOStream.Read must forward context.");
        TestAssert.Equal((UIntPtr)3, capturedIOSize, "SDL.IOStream.Read must forward count.");
        TestAssert.Equal(7, buffer[1], "SDL.IOStream.Read must write byte 0 at offset.");
        TestAssert.Equal(8, buffer[2], "SDL.IOStream.Read must write byte 1 at offset.");
        TestAssert.Equal(9, buffer[3], "SDL.IOStream.Read must write byte 2 at offset.");

        AssertThrows<ArgumentNullException>(() => stream.Read(null!, 0, 1), "SDL.IOStream.Read must reject null buffers.");
        AssertThrows<ArgumentOutOfRangeException>(() => stream.Read(new byte[1], 2, 0), "SDL.IOStream.Read must reject offsets outside the buffer.");
        AssertThrows<ArgumentOutOfRangeException>(() => stream.Read(new byte[1], 0, 2), "SDL.IOStream.Read must reject counts outside the buffer.");
    }

    public static void IOStream_Write_CoversZeroWriteNativeWriteFailureAndValidation()
    {
        ResetCaptureState();
        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7006, leaveOpen: true);
        byte[] buffer = [1, 2, 3, 4, 5];

        stream.Write(buffer, 1, 0);
        TestAssert.Equal(0, capturedCallCount, "SDL.IOStream.Write must not call native write for zero-count writes.");

        using NativeHookScope writeHook = NativeHookScope.Install("WriteIONativeFunction", nameof(CaptureWriteIO));
        nextULong = 3;
        stream.Write(buffer, 1, 3);

        TestAssert.Equal((IntPtr)0x7006, capturedContext, "SDL.IOStream.Write must forward context.");
        TestAssert.Equal((UIntPtr)3, capturedIOSize, "SDL.IOStream.Write must forward count.");
        AssertBytes([2, 3, 4], capturedWriteBytes, "SDL.IOStream.Write must forward bytes from the requested offset.");

        IntPtr errorPointer = Marshal.StringToCoTaskMemUTF8("write failed");
        nextErrorPointer = errorPointer;
        nextULong = 1;

        try
        {
            using NativeHookScope errorHook = NativeHookScope.Install("GetErrorNativeFunction", nameof(CaptureGetError));
            IOException ex = AssertThrows<IOException>(() => stream.Write(buffer, 0, 3), "SDL.IOStream.Write must throw when native write is short.");
            TestAssert.True(ex.Message.Contains("write failed", StringComparison.Ordinal), "SDL.IOStream.Write must include SDL.GetError text.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(errorPointer);
        }

        AssertThrows<ArgumentNullException>(() => stream.Write(null!, 0, 1), "SDL.IOStream.Write must reject null buffers.");
        AssertThrows<ArgumentOutOfRangeException>(() => stream.Write(new byte[1], 2, 0), "SDL.IOStream.Write must reject offsets outside the buffer.");
        AssertThrows<ArgumentOutOfRangeException>(() => stream.Write(new byte[1], 0, 2), "SDL.IOStream.Write must reject counts outside the buffer.");
    }

    public static void IOStream_Seek_CoversOriginsInvalidOriginAndFailure()
    {
        ResetCaptureState();
        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7007, leaveOpen: true);
        using NativeHookScope seekHook = NativeHookScope.Install("SeekIONativeFunction", nameof(CaptureSeekIO));

        nextLong = 11;
        TestAssert.Equal(11L, stream.Seek(11, SeekOrigin.Begin), "SDL.IOStream.Seek must return native position for Begin.");
        TestAssert.Equal(SDL3.SDL.IOWhence.Set, capturedWhence, "SDL.IOStream.Seek must map Begin to IOWhence.Set.");

        nextLong = 12;
        TestAssert.Equal(12L, stream.Seek(1, SeekOrigin.Current), "SDL.IOStream.Seek must return native position for Current.");
        TestAssert.Equal(SDL3.SDL.IOWhence.Cur, capturedWhence, "SDL.IOStream.Seek must map Current to IOWhence.Cur.");

        nextLong = 13;
        TestAssert.Equal(13L, stream.Seek(-1, SeekOrigin.End), "SDL.IOStream.Seek must return native position for End.");
        TestAssert.Equal(SDL3.SDL.IOWhence.End, capturedWhence, "SDL.IOStream.Seek must map End to IOWhence.End.");

        AssertThrows<ArgumentOutOfRangeException>(() => stream.Seek(0, (SeekOrigin)99), "SDL.IOStream.Seek must reject unknown seek origins.");

        IntPtr errorPointer = Marshal.StringToCoTaskMemUTF8("seek failed");
        nextErrorPointer = errorPointer;
        nextLong = -1;

        try
        {
            using NativeHookScope errorHook = NativeHookScope.Install("GetErrorNativeFunction", nameof(CaptureGetError));
            IOException ex = AssertThrows<IOException>(() => stream.Seek(0, SeekOrigin.Begin), "SDL.IOStream.Seek must throw for negative native positions.");
            TestAssert.True(ex.Message.Contains("seek failed", StringComparison.Ordinal), "SDL.IOStream.Seek must include SDL.GetError text.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(errorPointer);
        }
    }

    public static void IOStream_Flush_IsNoOp()
    {
        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7008, leaveOpen: true);

        stream.Flush();
    }

    public static void IOStream_SetLength_ThrowsNotSupported()
    {
        using Stream stream = SDL3.SDL.AsStream((IntPtr)0x7009, leaveOpen: true);

        AssertThrows<NotSupportedException>(() => stream.SetLength(12), "SDL.IOStream.SetLength must throw NotSupportedException.");
    }

    public static void IOStream_Dispose_ClosesOrLeavesOpenAndUpdatesCapabilities()
    {
        ResetCaptureState();
        nextBool = true;
        Stream closingStream = SDL3.SDL.AsStream((IntPtr)0x7010, leaveOpen: false);

        using (NativeHookScope.Install("CloseIONativeFunction", nameof(CaptureCloseIO)))
        {
            closingStream.Dispose();
            closingStream.Dispose();
        }

        TestAssert.Equal((IntPtr)0x7010, capturedContext, "SDL.IOStream.Dispose must close the SDL IO handle.");
        TestAssert.Equal(1, capturedCloseCallCount, "SDL.IOStream.Dispose must close only once.");
        TestAssert.Equal(false, closingStream.CanRead, "SDL.IOStream.Dispose must disable reads.");
        TestAssert.Equal(false, closingStream.CanSeek, "SDL.IOStream.Dispose must disable seeks.");
        TestAssert.Equal(false, closingStream.CanWrite, "SDL.IOStream.Dispose must disable writes.");

        ResetCaptureState();
        Stream leaveOpenStream = SDL3.SDL.AsStream((IntPtr)0x7011, leaveOpen: true);
        using (NativeHookScope.Install("CloseIONativeFunction", nameof(CaptureCloseIO)))
        {
            leaveOpenStream.Dispose();
        }

        TestAssert.Equal(0, capturedCloseCallCount, "SDL.IOStream.Dispose must not close when leaveOpen is true.");
    }

    public static void IOStream_ThrowIfDisposed_ThrowsForOperationsAfterDispose()
    {
        Stream stream = SDL3.SDL.AsStream((IntPtr)0x7012, leaveOpen: true);
        stream.Dispose();

        AssertThrows<ObjectDisposedException>(() => _ = stream.Length, "SDL.IOStream must throw after dispose.");
        AssertThrows<ObjectDisposedException>(() => stream.Read(new byte[1], 0, 1), "SDL.IOStream.Read must throw after dispose.");
    }

    private static (byte[] buffer, int offset, int count) InvokeReadAllToBuffer(Stream stream, int maxBytes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("ReadAllToBuffer", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.ReadAllToBuffer private helper must exist.");

        try
        {
            return ((byte[] buffer, int offset, int count))method!.Invoke(null, [stream, maxBytes])!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            throw;
        }
    }

    private static IntPtr CaptureIOFromConstMem(IntPtr mem, UIntPtr size)
    {
        capturedCallCount++;
        capturedMem = mem;
        capturedSize = size;
        int byteCount = checked((int)size.ToUInt64());
        capturedMemoryBytes = new byte[byteCount];
        if (byteCount > 0)
        {
            Marshal.Copy(mem, capturedMemoryBytes, 0, byteCount);
        }

        return nextPointer;
    }

    private static bool CaptureCloseIO(IntPtr context)
    {
        capturedCloseCallCount++;
        capturedContext = context;
        return nextBool;
    }

    private static long CaptureGetIOSize(IntPtr context)
    {
        capturedCallCount++;
        capturedContext = context;
        return nextLong;
    }

    private static long CaptureTellIO(IntPtr context)
    {
        capturedCallCount++;
        capturedContext = context;
        return nextLong;
    }

    private static long CaptureSeekIO(IntPtr context, long offset, SDL3.SDL.IOWhence whence)
    {
        capturedCallCount++;
        capturedContext = context;
        capturedOffset = offset;
        capturedWhence = whence;
        return nextLong;
    }

    private static ulong CaptureReadIO(IntPtr context, IntPtr ptr, UIntPtr size)
    {
        capturedCallCount++;
        capturedContext = context;
        capturedReadPointer = ptr;
        capturedIOSize = size;
        if (nextReadBytes is { Length: > 0 })
        {
            Marshal.Copy(nextReadBytes, 0, ptr, nextReadBytes.Length);
        }

        return nextULong;
    }

    private static ulong CaptureWriteIO(IntPtr context, IntPtr ptr, UIntPtr size)
    {
        capturedCallCount++;
        capturedContext = context;
        capturedWritePointer = ptr;
        capturedIOSize = size;
        int byteCount = checked((int)size.ToUInt64());
        capturedWriteBytes = new byte[byteCount];
        if (byteCount > 0)
        {
            Marshal.Copy(ptr, capturedWriteBytes, 0, byteCount);
        }

        return nextULong;
    }

    private static IntPtr CaptureGetError()
    {
        return nextErrorPointer;
    }

    private static void ResetCaptureState()
    {
        capturedMem = IntPtr.Zero;
        capturedSize = UIntPtr.Zero;
        capturedMemoryBytes = null;
        capturedContext = IntPtr.Zero;
        capturedReadPointer = IntPtr.Zero;
        capturedWritePointer = IntPtr.Zero;
        capturedOffset = 0;
        capturedWhence = default;
        capturedIOSize = UIntPtr.Zero;
        capturedWriteBytes = null;
        nextPointer = IntPtr.Zero;
        nextErrorPointer = IntPtr.Zero;
        nextLong = 0;
        nextULong = 0;
        nextBool = false;
        nextReadBytes = null;
        capturedCallCount = 0;
        capturedCloseCallCount = 0;
    }

    private static TException AssertThrows<TException>(Action action, string message) where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException ex)
        {
            return ex;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{message} Expected {typeof(TException).Name}, got {ex.GetType().Name}.", ex);
        }

        throw new InvalidOperationException($"{message} Expected {typeof(TException).Name}, but no exception was thrown.");
    }

    private static void AssertBytes(byte[] expected, byte[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length mismatch.");

        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], actual[i], $"{message} Byte {i} mismatch.");
        }
    }

    private static void AssertBufferSegment(byte[] expected, byte[] buffer, int offset, int count, string message)
    {
        TestAssert.Equal(expected.Length, count, $"{message} Count mismatch.");

        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], buffer[offset + i], $"{message} Byte {i} mismatch.");
        }
    }

    private sealed class ReadOnlyTestStream(byte[] data, bool canRead, bool canSeek) : Stream
    {
        private long position;

        public override bool CanRead => canRead;
        public override bool CanSeek => canSeek;
        public override bool CanWrite => false;

        public override long Length => canSeek ? data.Length : throw new NotSupportedException();

        public override long Position
        {
            get => canSeek ? position : throw new NotSupportedException();
            set
            {
                if (!canSeek)
                {
                    throw new NotSupportedException();
                }

                position = value;
            }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!canRead)
            {
                throw new NotSupportedException();
            }

            int available = Math.Max(0, data.Length - checked((int)position));
            int read = Math.Min(available, count);
            if (read == 0)
            {
                return 0;
            }

            Array.Copy(data, position, buffer, offset, read);
            position += read;
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (!canSeek)
            {
                throw new NotSupportedException();
            }

            position = origin switch
            {
                SeekOrigin.Begin => offset,
                SeekOrigin.Current => position + offset,
                SeekOrigin.End => data.Length + offset,
                _ => throw new ArgumentOutOfRangeException(nameof(origin), origin, null)
            };
            return position;
        }

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? originalValue;

        private NativeHookScope(FieldInfo field, object? originalValue)
        {
            this.field = field;
            this.originalValue = originalValue;
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL.{fieldName} native hook field must exist.");
            MethodInfo? method = typeof(StreamTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"{methodName} hook method must exist.");
            object? originalValue = field!.GetValue(null);
            Delegate hook = Delegate.CreateDelegate(field.FieldType, method!);
            field.SetValue(null, hook);
            return new NativeHookScope(field, originalValue);
        }

        public void Dispose()
        {
            field.SetValue(null, originalValue);
        }
    }
}
