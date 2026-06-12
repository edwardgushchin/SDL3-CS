using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Threads.Thread;

internal static class PInvokeTests
{
    private static SDL3.SDL.ThreadFunction? capturedThreadFunction;
    private static SDL3.SDL.TLSDestructorCallback? capturedTLSDestructor;
    private static SDL3.SDL.ThreadPriority capturedThreadPriority;
    private static string? capturedName;
    private static IntPtr capturedData;
    private static IntPtr capturedBeginThread;
    private static IntPtr capturedEndThread;
    private static IntPtr capturedThread;
    private static IntPtr capturedTLSId;
    private static IntPtr capturedTLSValue;
    private static IntPtr capturedDestructorValue;
    private static uint capturedProps;
    private static int capturedCallCount;
    private static int nextStatus;
    private static IntPtr nextPointer;
    private static ulong nextThreadID;
    private static bool nextBool;
    private static SDL3.SDL.ThreadState nextThreadState;

    public static void RunAll()
    {
        CreateThread_ForwardsToRuntimeWithNullCrtHooks();
        CreateThreadWithProperties_ForwardsToRuntimeWithNullCrtHooks();
        CreateThreadRuntime_ForwardsArgumentsAndUsesExpectedNativeMetadata();
        CreateThreadWithPropertiesRuntime_ForwardsArgumentsAndUsesExpectedNativeMetadata();
        SDL_GetThreadName_UsesExpectedNativeMetadata();
        GetThreadName_ReturnsUtf8StringAndNull();
        GetCurrentThreadID_ReturnsNativeValue();
        GetThreadID_ForwardsThreadAndReturnsNativeValue();
        SetCurrentThreadPriority_ForwardsPriorityAndReturnsNativeValue();
        WaitThread_ForwardsThreadAndReturnsStatus();
        GetThreadState_ForwardsThreadAndReturnsNativeValue();
        DetachThread_ForwardsThread();
        GetTLS_ForwardsIdAndReturnsNativeValue();
        SetTLS_ForwardsArgumentsAndReturnsNativeValue();
        CleanupTLS_CallsNativeHook();
    }

    public static void CreateThread_ForwardsToRuntimeWithNullCrtHooks()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1001;

        using NativeHookScope _ = NativeHookScope.Install("CreateThreadRuntimeNativeFunction", nameof(CaptureCreateThreadRuntime));
        IntPtr result = SDL3.SDL.CreateThread(TestThreadEntry, "worker", (IntPtr)0x2001);

        TestAssert.Equal((IntPtr)0x1001, result, "SDL.CreateThread must return the runtime thread handle.");
        TestAssert.NotNull(capturedThreadFunction, "SDL.CreateThread must forward the thread callback.");
        TestAssert.Equal("worker", capturedName, "SDL.CreateThread must forward the thread name.");
        TestAssert.Equal((IntPtr)0x2001, capturedData, "SDL.CreateThread must forward userdata.");
        TestAssert.Equal(IntPtr.Zero, capturedBeginThread, "SDL.CreateThread must pass a null begin-thread hook.");
        TestAssert.Equal(IntPtr.Zero, capturedEndThread, "SDL.CreateThread must pass a null end-thread hook.");
        TestAssert.Equal(77, capturedThreadFunction!((IntPtr)0x44), "SDL.CreateThread must preserve the thread callback.");
    }

    public static void CreateThreadWithProperties_ForwardsToRuntimeWithNullCrtHooks()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1002;

        using NativeHookScope _ = NativeHookScope.Install("CreateThreadWithPropertiesRuntimeNativeFunction", nameof(CaptureCreateThreadWithPropertiesRuntime));
        IntPtr result = SDL3.SDL.CreateThreadWithProperties(42);

        TestAssert.Equal((IntPtr)0x1002, result, "SDL.CreateThreadWithProperties must return the runtime thread handle.");
        TestAssert.Equal(42u, capturedProps, "SDL.CreateThreadWithProperties must forward props.");
        TestAssert.Equal(IntPtr.Zero, capturedBeginThread, "SDL.CreateThreadWithProperties must pass a null begin-thread hook.");
        TestAssert.Equal(IntPtr.Zero, capturedEndThread, "SDL.CreateThreadWithProperties must pass a null end-thread hook.");
    }

    public static void CreateThreadRuntime_ForwardsArgumentsAndUsesExpectedNativeMetadata()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1003;

        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateThreadRuntime");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateThreadRuntime");
        AssertParameterMarshalAs(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        using NativeHookScope _ = NativeHookScope.Install("CreateThreadRuntimeNativeFunction", nameof(CaptureCreateThreadRuntime));
        IntPtr result = SDL3.SDL.CreateThreadRuntime(TestThreadEntry, "runtime", (IntPtr)0x2002, (IntPtr)0x3001, (IntPtr)0x3002);

        TestAssert.Equal((IntPtr)0x1003, result, "SDL.CreateThreadRuntime must return the native hook value.");
        TestAssert.NotNull(capturedThreadFunction, "SDL.CreateThreadRuntime must forward the callback.");
        TestAssert.Equal("runtime", capturedName, "SDL.CreateThreadRuntime must forward name.");
        TestAssert.Equal((IntPtr)0x2002, capturedData, "SDL.CreateThreadRuntime must forward userdata.");
        TestAssert.Equal((IntPtr)0x3001, capturedBeginThread, "SDL.CreateThreadRuntime must forward pfnBeginThread.");
        TestAssert.Equal((IntPtr)0x3002, capturedEndThread, "SDL.CreateThreadRuntime must forward pfnEndThread.");
    }

    public static void CreateThreadWithPropertiesRuntime_ForwardsArgumentsAndUsesExpectedNativeMetadata()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1004;

        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateThreadWithPropertiesRuntime");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateThreadWithPropertiesRuntime");

        using NativeHookScope _ = NativeHookScope.Install("CreateThreadWithPropertiesRuntimeNativeFunction", nameof(CaptureCreateThreadWithPropertiesRuntime));
        IntPtr result = SDL3.SDL.CreateThreadWithPropertiesRuntime(43, (IntPtr)0x3003, (IntPtr)0x3004);

        TestAssert.Equal((IntPtr)0x1004, result, "SDL.CreateThreadWithPropertiesRuntime must return the native hook value.");
        TestAssert.Equal(43u, capturedProps, "SDL.CreateThreadWithPropertiesRuntime must forward props.");
        TestAssert.Equal((IntPtr)0x3003, capturedBeginThread, "SDL.CreateThreadWithPropertiesRuntime must forward pfnBeginThread.");
        TestAssert.Equal((IntPtr)0x3004, capturedEndThread, "SDL.CreateThreadWithPropertiesRuntime must forward pfnEndThread.");
    }

    public static void SDL_GetThreadName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetThreadName");

        AssertSdlLibraryImport(nativeMethod, "SDL_GetThreadName");
    }

    public static void GetThreadName_ReturnsUtf8StringAndNull()
    {
        ResetCaptureState();
        IntPtr utf8 = Marshal.StringToCoTaskMemUTF8("thread-name");

        try
        {
            nextPointer = utf8;
            using (NativeHookScope _ = NativeHookScope.Install("GetThreadNameNativeFunction", nameof(CaptureThreadPointerToPointer)))
            {
                string? result = SDL3.SDL.GetThreadName((IntPtr)0x4001);

                TestAssert.Equal("thread-name", result, "SDL.GetThreadName must convert UTF-8 pointers.");
                TestAssert.Equal((IntPtr)0x4001, capturedThread, "SDL.GetThreadName must forward thread.");
            }

            nextPointer = IntPtr.Zero;
            using (NativeHookScope _ = NativeHookScope.Install("GetThreadNameNativeFunction", nameof(CaptureThreadPointerToPointer)))
            {
                string? result = SDL3.SDL.GetThreadName((IntPtr)0x4002);

                TestAssert.Equal<string?>(null, result, "SDL.GetThreadName must return null for a null native pointer.");
            }
        }
        finally
        {
            Marshal.FreeCoTaskMem(utf8);
        }
    }

    public static void GetCurrentThreadID_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextThreadID = 123456789;

        using NativeHookScope _ = NativeHookScope.Install("GetCurrentThreadIDNativeFunction", nameof(CaptureULongNoArgs));
        ulong result = SDL3.SDL.GetCurrentThreadID();

        TestAssert.Equal(123456789ul, result, "SDL.GetCurrentThreadID must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetCurrentThreadID must call the native hook once.");
    }

    public static void GetThreadID_ForwardsThreadAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextThreadID = 987654321;

        using NativeHookScope _ = NativeHookScope.Install("GetThreadIDNativeFunction", nameof(CaptureThreadPointerToULong));
        ulong result = SDL3.SDL.GetThreadID((IntPtr)0x5001);

        TestAssert.Equal(987654321ul, result, "SDL.GetThreadID must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5001, capturedThread, "SDL.GetThreadID must forward thread.");
    }

    public static void SetCurrentThreadPriority_ForwardsPriorityAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetCurrentThreadPriorityNativeFunction", nameof(CaptureThreadPriorityBool));
        bool result = SDL3.SDL.SetCurrentThreadPriority(SDL3.SDL.ThreadPriority.High);

        TestAssert.Equal(true, result, "SDL.SetCurrentThreadPriority must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.ThreadPriority.High, capturedThreadPriority, "SDL.SetCurrentThreadPriority must forward priority.");
    }

    public static void WaitThread_ForwardsThreadAndReturnsStatus()
    {
        ResetCaptureState();
        nextStatus = 123;

        using NativeHookScope _ = NativeHookScope.Install("WaitThreadNativeFunction", nameof(CaptureWaitThread));
        SDL3.SDL.WaitThread((IntPtr)0x6001, out int status);

        TestAssert.Equal((IntPtr)0x6001, capturedThread, "SDL.WaitThread must forward thread.");
        TestAssert.Equal(123, status, "SDL.WaitThread must return native status.");
    }

    public static void GetThreadState_ForwardsThreadAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextThreadState = SDL3.SDL.ThreadState.Complete;

        using NativeHookScope _ = NativeHookScope.Install("GetThreadStateNativeFunction", nameof(CaptureThreadPointerToState));
        SDL3.SDL.ThreadState result = SDL3.SDL.GetThreadState((IntPtr)0x7001);

        TestAssert.Equal(SDL3.SDL.ThreadState.Complete, result, "SDL.GetThreadState must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7001, capturedThread, "SDL.GetThreadState must forward thread.");
    }

    public static void DetachThread_ForwardsThread()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("DetachThreadNativeFunction", nameof(CaptureThreadPointerVoid));
        SDL3.SDL.DetachThread((IntPtr)0x8001);

        TestAssert.Equal((IntPtr)0x8001, capturedThread, "SDL.DetachThread must forward thread.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DetachThread must call the native hook once.");
    }

    public static void GetTLS_ForwardsIdAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x9002;

        using NativeHookScope _ = NativeHookScope.Install("GetTLSNativeFunction", nameof(CaptureTLSToPointer));
        IntPtr result = SDL3.SDL.GetTLS((IntPtr)0x9001);

        TestAssert.Equal((IntPtr)0x9002, result, "SDL.GetTLS must return the native hook value.");
        TestAssert.Equal((IntPtr)0x9001, capturedTLSId, "SDL.GetTLS must forward id.");
    }

    public static void SetTLS_ForwardsArgumentsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetTLSNativeFunction", nameof(CaptureSetTLS));
        bool result = SDL3.SDL.SetTLS((IntPtr)0xA001, (IntPtr)0xA002, TestTLSDestructor);

        TestAssert.Equal(true, result, "SDL.SetTLS must return the native hook value.");
        TestAssert.Equal((IntPtr)0xA001, capturedTLSId, "SDL.SetTLS must forward id.");
        TestAssert.Equal((IntPtr)0xA002, capturedTLSValue, "SDL.SetTLS must forward value.");
        TestAssert.NotNull(capturedTLSDestructor, "SDL.SetTLS must forward destructor.");

        capturedTLSDestructor!((IntPtr)0xA003);

        TestAssert.Equal((IntPtr)0xA003, capturedDestructorValue, "SDL.SetTLS must preserve destructor callback.");
    }

    public static void CleanupTLS_CallsNativeHook()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("CleanupTLSNativeFunction", nameof(CaptureVoidNoArgs));
        SDL3.SDL.CleanupTLS();

        TestAssert.Equal(1, capturedCallCount, "SDL.CleanupTLS must call the native hook once.");
    }

    private static int TestThreadEntry(IntPtr data)
    {
        capturedData = data;
        return 77;
    }

    private static void TestTLSDestructor(IntPtr value)
    {
        capturedDestructorValue = value;
    }

    private static IntPtr CaptureCreateThreadRuntime(SDL3.SDL.ThreadFunction fn, string name, IntPtr data, IntPtr pfnBeginThread, IntPtr pfnEndThread)
    {
        capturedThreadFunction = fn;
        capturedName = name;
        capturedData = data;
        capturedBeginThread = pfnBeginThread;
        capturedEndThread = pfnEndThread;
        return nextPointer;
    }

    private static IntPtr CaptureCreateThreadWithPropertiesRuntime(uint props, IntPtr pfnBeginThread, IntPtr pfnEndThread)
    {
        capturedProps = props;
        capturedBeginThread = pfnBeginThread;
        capturedEndThread = pfnEndThread;
        return nextPointer;
    }

    private static IntPtr CaptureThreadPointerToPointer(IntPtr thread)
    {
        capturedThread = thread;
        return nextPointer;
    }

    private static ulong CaptureULongNoArgs()
    {
        capturedCallCount++;
        return nextThreadID;
    }

    private static ulong CaptureThreadPointerToULong(IntPtr thread)
    {
        capturedThread = thread;
        return nextThreadID;
    }

    private static bool CaptureThreadPriorityBool(SDL3.SDL.ThreadPriority priority)
    {
        capturedThreadPriority = priority;
        return nextBool;
    }

    private static void CaptureWaitThread(IntPtr thread, out int status)
    {
        capturedThread = thread;
        status = nextStatus;
    }

    private static SDL3.SDL.ThreadState CaptureThreadPointerToState(IntPtr thread)
    {
        capturedThread = thread;
        return nextThreadState;
    }

    private static void CaptureThreadPointerVoid(IntPtr thread)
    {
        capturedThread = thread;
        capturedCallCount++;
    }

    private static IntPtr CaptureTLSToPointer(IntPtr id)
    {
        capturedTLSId = id;
        return nextPointer;
    }

    private static bool CaptureSetTLS(IntPtr id, IntPtr value, SDL3.SDL.TLSDestructorCallback destructor)
    {
        capturedTLSId = id;
        capturedTLSValue = value;
        capturedTLSDestructor = destructor;
        return nextBool;
    }

    private static void CaptureVoidNoArgs()
    {
        capturedCallCount++;
    }

    private static MethodInfo GetNativeMethod(string name)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static);

        TestAssert.NotNull(method, $"SDL native method {name} must exist.");

        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? attribute = method.GetCustomAttribute<LibraryImportAttribute>();

        TestAssert.NotNull(attribute, $"{method.Name} must use LibraryImportAttribute.");
        TestAssert.Equal("SDL3", attribute!.LibraryName, $"{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, attribute.EntryPoint, $"{method.Name} must use the expected EntryPoint.");
    }

    private static void AssertParameterMarshalAs(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo? parameter = method.GetParameters().FirstOrDefault(p => p.Name == parameterName);

        TestAssert.NotNull(parameter, $"{method.Name} must expose parameter {parameterName}.");
        MarshalAsAttribute? attribute = parameter!.GetCustomAttribute<MarshalAsAttribute>();

        TestAssert.NotNull(attribute, $"{method.Name}.{parameterName} must use MarshalAsAttribute.");
        TestAssert.Equal(unmanagedType, attribute!.Value, $"{method.Name}.{parameterName} must use the expected unmanaged type.");
    }

    private static void ResetCaptureState()
    {
        capturedThreadFunction = null;
        capturedTLSDestructor = null;
        capturedThreadPriority = default;
        capturedName = null;
        capturedData = IntPtr.Zero;
        capturedBeginThread = IntPtr.Zero;
        capturedEndThread = IntPtr.Zero;
        capturedThread = IntPtr.Zero;
        capturedTLSId = IntPtr.Zero;
        capturedTLSValue = IntPtr.Zero;
        capturedDestructorValue = IntPtr.Zero;
        capturedProps = 0;
        capturedCallCount = 0;
        nextStatus = 0;
        nextPointer = IntPtr.Zero;
        nextThreadID = 0;
        nextBool = false;
        nextThreadState = default;
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private NativeHookScope(FieldInfo field, object? hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL private hook field {fieldName} must exist.");

            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"Test hook method {methodName} must exist.");

            Delegate hook = Delegate.CreateDelegate(field!.FieldType, method!);

            return new NativeHookScope(field, hook);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
