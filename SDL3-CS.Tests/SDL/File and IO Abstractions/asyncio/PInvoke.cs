using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.FileAndIOAbstractions.Asyncio;

internal static class PInvokeTests
{
    private static string? capturedFile;
    private static string? capturedMode;
    private static IntPtr capturedAsyncio;
    private static IntPtr capturedPtr;
    private static IntPtr capturedQueue;
    private static IntPtr capturedUserdata;
    private static ulong capturedOffset;
    private static ulong capturedSize;
    private static bool capturedFlush;
    private static int capturedTimeoutMS;
    private static long nextLong;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static SDL3.SDL.AsyncIOOutcome nextOutcome;
    private static int capturedCallCount;

    public static void AsyncIOFromFile_ForwardsFileModeAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AsyncIOFromFile");
        AssertSdlLibraryImport(nativeMethod, "SDL_AsyncIOFromFile");
        AssertStringParameterMarshal(nativeMethod, "file", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "mode", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)1001;
        using NativeHookScope _ = NativeHookScope.Install("AsyncIOFromFileNativeFunction", nameof(CaptureAsyncIOFromFile));

        IntPtr result = SDL3.SDL.AsyncIOFromFile("save.dat", "r+");

        TestAssert.Equal((IntPtr)1001, result, "SDL.AsyncIOFromFile must return the native async I/O pointer.");
        TestAssert.Equal("save.dat", capturedFile, "SDL.AsyncIOFromFile must forward the file path.");
        TestAssert.Equal("r+", capturedMode, "SDL.AsyncIOFromFile must forward the mode string.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AsyncIOFromFile must call native hook once.");
    }

    public static void GetAsyncIOSize_ForwardsAsyncioAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAsyncIOSize");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAsyncIOSize");

        ResetCaptureState();
        nextLong = 4096;
        using NativeHookScope _ = NativeHookScope.Install("GetAsyncIOSizeNativeFunction", nameof(CaptureGetAsyncIOSize));

        long result = SDL3.SDL.GetAsyncIOSize((IntPtr)2002);

        TestAssert.Equal(4096L, result, "SDL.GetAsyncIOSize must return native size.");
        TestAssert.Equal((IntPtr)2002, capturedAsyncio, "SDL.GetAsyncIOSize must forward the async I/O pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetAsyncIOSize must call native hook once.");
    }

    public static void ReadAsyncIO_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ReadAsyncIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_ReadAsyncIO");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("ReadAsyncIONativeFunction", nameof(CaptureReadAsyncIO));

        bool result = SDL3.SDL.ReadAsyncIO((IntPtr)1, (IntPtr)2, 3, 4, (IntPtr)5, (IntPtr)6);

        TestAssert.Equal(true, result, "SDL.ReadAsyncIO must return native bool value.");
        AssertTaskArguments((IntPtr)1, (IntPtr)2, 3, 4, (IntPtr)5, (IntPtr)6, "SDL.ReadAsyncIO");
    }

    public static void WriteAsyncIO_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WriteAsyncIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_WriteAsyncIO");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("WriteAsyncIONativeFunction", nameof(CaptureWriteAsyncIO));

        bool result = SDL3.SDL.WriteAsyncIO((IntPtr)7, (IntPtr)8, 9, 10, (IntPtr)11, (IntPtr)12);

        TestAssert.Equal(false, result, "SDL.WriteAsyncIO must return native bool value.");
        AssertTaskArguments((IntPtr)7, (IntPtr)8, 9, 10, (IntPtr)11, (IntPtr)12, "SDL.WriteAsyncIO");
    }

    public static void CloseAsyncIO_ForwardsFlushAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CloseAsyncIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_CloseAsyncIO");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertBoolParameterMarshal(nativeMethod, "flush", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("CloseAsyncIONativeFunction", nameof(CaptureCloseAsyncIO));

        bool result = SDL3.SDL.CloseAsyncIO((IntPtr)13, true, (IntPtr)14, (IntPtr)15);

        TestAssert.Equal(true, result, "SDL.CloseAsyncIO must return native bool value.");
        TestAssert.Equal((IntPtr)13, capturedAsyncio, "SDL.CloseAsyncIO must forward the async I/O pointer.");
        TestAssert.Equal(true, capturedFlush, "SDL.CloseAsyncIO must forward the flush flag.");
        TestAssert.Equal((IntPtr)14, capturedQueue, "SDL.CloseAsyncIO must forward the queue pointer.");
        TestAssert.Equal((IntPtr)15, capturedUserdata, "SDL.CloseAsyncIO must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CloseAsyncIO must call native hook once.");
    }

    public static void CreateAsyncIOQueue_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateAsyncIOQueue");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateAsyncIOQueue");

        ResetCaptureState();
        nextPointer = (IntPtr)3003;
        using NativeHookScope _ = NativeHookScope.Install("CreateAsyncIOQueueNativeFunction", nameof(CaptureCreateAsyncIOQueue));

        IntPtr result = SDL3.SDL.CreateAsyncIOQueue();

        TestAssert.Equal((IntPtr)3003, result, "SDL.CreateAsyncIOQueue must return the native queue pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateAsyncIOQueue must call native hook once.");
    }

    public static void DestroyAsyncIOQueue_ForwardsQueue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyAsyncIOQueue");
        AssertSdlLibraryImport(nativeMethod, "SDL_DestroyAsyncIOQueue");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DestroyAsyncIOQueueNativeFunction", nameof(CaptureDestroyAsyncIOQueue));

        SDL3.SDL.DestroyAsyncIOQueue((IntPtr)4004);

        TestAssert.Equal((IntPtr)4004, capturedQueue, "SDL.DestroyAsyncIOQueue must forward the queue pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DestroyAsyncIOQueue must call native hook once.");
    }

    public static void GetAsyncIOResult_ForwardsQueueAndReturnsOutcome()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAsyncIOResult");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAsyncIOResult");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        nextOutcome = CreateOutcome(SDL3.SDL.AsyncIOTaskType.Read, SDL3.SDL.AsyncIOResult.Complete);
        using NativeHookScope _ = NativeHookScope.Install("GetAsyncIOResultNativeFunction", nameof(CaptureGetAsyncIOResult));

        bool result = SDL3.SDL.GetAsyncIOResult((IntPtr)5005, out SDL3.SDL.AsyncIOOutcome outcome);

        TestAssert.Equal(true, result, "SDL.GetAsyncIOResult must return native bool value.");
        TestAssert.Equal((IntPtr)5005, capturedQueue, "SDL.GetAsyncIOResult must forward the queue pointer.");
        AssertOutcome(nextOutcome, outcome, "SDL.GetAsyncIOResult");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetAsyncIOResult must call native hook once.");
    }

    public static void WaitAsyncIOResult_ForwardsQueueTimeoutAndReturnsOutcome()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitAsyncIOResult");
        AssertSdlLibraryImport(nativeMethod, "SDL_WaitAsyncIOResult");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        nextOutcome = CreateOutcome(SDL3.SDL.AsyncIOTaskType.Close, SDL3.SDL.AsyncIOResult.Canceled);
        using NativeHookScope _ = NativeHookScope.Install("WaitAsyncIOResultNativeFunction", nameof(CaptureWaitAsyncIOResult));

        bool result = SDL3.SDL.WaitAsyncIOResult((IntPtr)6006, out SDL3.SDL.AsyncIOOutcome outcome, -1);

        TestAssert.Equal(false, result, "SDL.WaitAsyncIOResult must return native bool value.");
        TestAssert.Equal((IntPtr)6006, capturedQueue, "SDL.WaitAsyncIOResult must forward the queue pointer.");
        TestAssert.Equal(-1, capturedTimeoutMS, "SDL.WaitAsyncIOResult must forward the timeout value.");
        AssertOutcome(nextOutcome, outcome, "SDL.WaitAsyncIOResult");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitAsyncIOResult must call native hook once.");
    }

    public static void SignalAsyncIOQueue_ForwardsQueue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SignalAsyncIOQueue");
        AssertSdlLibraryImport(nativeMethod, "SDL_SignalAsyncIOQueue");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SignalAsyncIOQueueNativeFunction", nameof(CaptureSignalAsyncIOQueue));

        SDL3.SDL.SignalAsyncIOQueue((IntPtr)7007);

        TestAssert.Equal((IntPtr)7007, capturedQueue, "SDL.SignalAsyncIOQueue must forward the queue pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SignalAsyncIOQueue must call native hook once.");
    }

    public static void LoadFileAsync_ForwardsFileQueueUserdataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LoadFileAsync");
        AssertSdlLibraryImport(nativeMethod, "SDL_LoadFileAsync");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "file", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("LoadFileAsyncNativeFunction", nameof(CaptureLoadFileAsync));

        bool result = SDL3.SDL.LoadFileAsync("async.bin", (IntPtr)8008, (IntPtr)8009);

        TestAssert.Equal(true, result, "SDL.LoadFileAsync must return native bool value.");
        TestAssert.Equal("async.bin", capturedFile, "SDL.LoadFileAsync must forward the file path.");
        TestAssert.Equal((IntPtr)8008, capturedQueue, "SDL.LoadFileAsync must forward the queue pointer.");
        TestAssert.Equal((IntPtr)8009, capturedUserdata, "SDL.LoadFileAsync must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.LoadFileAsync must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedFile = null;
        capturedMode = null;
        capturedAsyncio = IntPtr.Zero;
        capturedPtr = IntPtr.Zero;
        capturedQueue = IntPtr.Zero;
        capturedUserdata = IntPtr.Zero;
        capturedOffset = 0;
        capturedSize = 0;
        capturedFlush = false;
        capturedTimeoutMS = 0;
        nextLong = 0;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextOutcome = default;
        capturedCallCount = 0;
    }

    private static IntPtr CaptureAsyncIOFromFile(string file, string mode)
    {
        capturedFile = file;
        capturedMode = mode;
        capturedCallCount++;
        return nextPointer;
    }

    private static long CaptureGetAsyncIOSize(IntPtr asyncio)
    {
        capturedAsyncio = asyncio;
        capturedCallCount++;
        return nextLong;
    }

    private static bool CaptureReadAsyncIO(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata)
    {
        CaptureTaskArguments(asyncio, ptr, offset, size, queue, userdata);
        return nextBool;
    }

    private static bool CaptureWriteAsyncIO(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata)
    {
        CaptureTaskArguments(asyncio, ptr, offset, size, queue, userdata);
        return nextBool;
    }

    private static bool CaptureCloseAsyncIO(IntPtr asyncio, bool flush, IntPtr queue, IntPtr userdata)
    {
        capturedAsyncio = asyncio;
        capturedFlush = flush;
        capturedQueue = queue;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureCreateAsyncIOQueue()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureDestroyAsyncIOQueue(IntPtr queue)
    {
        capturedQueue = queue;
        capturedCallCount++;
    }

    private static bool CaptureGetAsyncIOResult(IntPtr queue, out SDL3.SDL.AsyncIOOutcome outcome)
    {
        capturedQueue = queue;
        outcome = nextOutcome;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWaitAsyncIOResult(IntPtr queue, out SDL3.SDL.AsyncIOOutcome outcome, int timeoutMS)
    {
        capturedQueue = queue;
        capturedTimeoutMS = timeoutMS;
        outcome = nextOutcome;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureSignalAsyncIOQueue(IntPtr queue)
    {
        capturedQueue = queue;
        capturedCallCount++;
    }

    private static bool CaptureLoadFileAsync(string file, IntPtr queue, IntPtr userdata)
    {
        capturedFile = file;
        capturedQueue = queue;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureTaskArguments(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata)
    {
        capturedAsyncio = asyncio;
        capturedPtr = ptr;
        capturedOffset = offset;
        capturedSize = size;
        capturedQueue = queue;
        capturedUserdata = userdata;
        capturedCallCount++;
    }

    private static SDL3.SDL.AsyncIOOutcome CreateOutcome(SDL3.SDL.AsyncIOTaskType type, SDL3.SDL.AsyncIOResult result)
    {
        return new SDL3.SDL.AsyncIOOutcome
        {
            ASyncIO = (IntPtr)901,
            Type = type,
            Result = result,
            Buffer = (IntPtr)902,
            Offset = 903,
            BytesRequested = 904,
            BytesTransferred = 905,
            Userdata = (IntPtr)906
        };
    }

    private static void AssertTaskArguments(
        IntPtr expectedAsyncio,
        IntPtr expectedPtr,
        ulong expectedOffset,
        ulong expectedSize,
        IntPtr expectedQueue,
        IntPtr expectedUserdata,
        string apiName)
    {
        TestAssert.Equal(expectedAsyncio, capturedAsyncio, $"{apiName} must forward the async I/O pointer.");
        TestAssert.Equal(expectedPtr, capturedPtr, $"{apiName} must forward the buffer pointer.");
        TestAssert.Equal(expectedOffset, capturedOffset, $"{apiName} must forward the offset.");
        TestAssert.Equal(expectedSize, capturedSize, $"{apiName} must forward the size.");
        TestAssert.Equal(expectedQueue, capturedQueue, $"{apiName} must forward the queue pointer.");
        TestAssert.Equal(expectedUserdata, capturedUserdata, $"{apiName} must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static void AssertOutcome(SDL3.SDL.AsyncIOOutcome expected, SDL3.SDL.AsyncIOOutcome actual, string apiName)
    {
        TestAssert.Equal(expected.ASyncIO, actual.ASyncIO, $"{apiName} must write outcome.ASyncIO.");
        TestAssert.Equal(expected.Type, actual.Type, $"{apiName} must write outcome.Type.");
        TestAssert.Equal(expected.Result, actual.Result, $"{apiName} must write outcome.Result.");
        TestAssert.Equal(expected.Buffer, actual.Buffer, $"{apiName} must write outcome.Buffer.");
        TestAssert.Equal(expected.Offset, actual.Offset, $"{apiName} must write outcome.Offset.");
        TestAssert.Equal(expected.BytesRequested, actual.BytesRequested, $"{apiName} must write outcome.BytesRequested.");
        TestAssert.Equal(expected.BytesTransferred, actual.BytesTransferred, $"{apiName} must write outcome.BytesTransferred.");
        TestAssert.Equal(expected.Userdata, actual.Userdata, $"{apiName} must write outcome.Userdata.");
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");

        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"SDL.{method.Name} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"SDL.{method.Name} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"SDL.{method.Name} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method, UnmanagedType unmanagedType)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} return value must use expected bool marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected string marshalling.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected bool marshalling.");
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
            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
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
