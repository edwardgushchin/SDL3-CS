using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Assert;

internal static class PInvokeTests
{
    private static IntPtr capturedData;
    private static string? capturedFunc;
    private static string? capturedFile;
    private static int capturedLine;
    private static SDL3.SDL.AssertState nextAssertState;
    private static SDL3.SDL.AssertionHandler? capturedAssertionHandler;
    private static SDL3.SDL.AssertionHandler? nextAssertionHandler;
    private static IntPtr capturedUserdata;
    private static IntPtr capturedUserdataPointer;
    private static IntPtr nextPointer;
    private static int capturedCallCount;

    public static void ReportAssertion_ForwardsDataStringsAndLine()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ReportAssertion");
        AssertSdlLibraryImport(nativeMethod, "SDL_ReportAssertion");
        AssertStringParameterMarshal(nativeMethod, "func", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "file", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextAssertState = SDL3.SDL.AssertState.Break;
        using NativeHookScope _ = NativeHookScope.Install("ReportAssertionNativeFunction", nameof(CaptureReportAssertion));

        SDL3.SDL.AssertState result = SDL3.SDL.ReportAssertion((IntPtr)101, "AssertFunc", "assert_file.cs", 303);

        TestAssert.Equal(nextAssertState, result, "SDL.ReportAssertion must return native assert state.");
        TestAssert.Equal((IntPtr)101, capturedData, "SDL.ReportAssertion must forward assert data.");
        TestAssert.Equal("AssertFunc", capturedFunc, "SDL.ReportAssertion must forward function name.");
        TestAssert.Equal("assert_file.cs", capturedFile, "SDL.ReportAssertion must forward file name.");
        TestAssert.Equal(303, capturedLine, "SDL.ReportAssertion must forward line number.");
    }

    public static void SetAssertionHandler_ForwardsHandlerAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAssertionHandler");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAssertionHandler");

        ResetCaptureState();
        SDL3.SDL.AssertionHandler handler = HandleAssertion;
        using NativeHookScope _ = NativeHookScope.Install("SetAssertionHandlerNativeFunction", nameof(CaptureSetAssertionHandler));

        SDL3.SDL.SetAssertionHandler(handler, (IntPtr)202);

        TestAssert.Equal(handler, capturedAssertionHandler!, "SDL.SetAssertionHandler must forward assertion handler.");
        TestAssert.Equal((IntPtr)202, capturedUserdata, "SDL.SetAssertionHandler must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetAssertionHandler must call native hook once.");
    }

    public static void GetDefaultAssertionHandler_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetDefaultAssertionHandler");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetDefaultAssertionHandler");

        ResetCaptureState();
        SDL3.SDL.AssertionHandler handler = HandleAssertion;
        nextAssertionHandler = handler;
        using NativeHookScope _ = NativeHookScope.Install("GetDefaultAssertionHandlerNativeFunction", nameof(CaptureGetDefaultAssertionHandler));

        SDL3.SDL.AssertionHandler result = SDL3.SDL.GetDefaultAssertionHandler();

        TestAssert.Equal(handler, result, "SDL.GetDefaultAssertionHandler must return native handler.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetDefaultAssertionHandler must call native hook once.");
    }

    public static void GetAssertionHandler_ForwardsUserdataPointerAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAssertionHandler");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAssertionHandler");

        ResetCaptureState();
        SDL3.SDL.AssertionHandler handler = HandleAssertion;
        nextAssertionHandler = handler;
        using NativeHookScope _ = NativeHookScope.Install("GetAssertionHandlerNativeFunction", nameof(CaptureGetAssertionHandler));

        SDL3.SDL.AssertionHandler result = SDL3.SDL.GetAssertionHandler((IntPtr)404);

        TestAssert.Equal(handler, result, "SDL.GetAssertionHandler must return native handler.");
        TestAssert.Equal((IntPtr)404, capturedUserdataPointer, "SDL.GetAssertionHandler must forward userdata pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetAssertionHandler must call native hook once.");
    }

    public static void GetAssertionReport_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAssertionReport");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAssertionReport");

        ResetCaptureState();
        nextPointer = (IntPtr)505;
        using NativeHookScope _ = NativeHookScope.Install("GetAssertionReportNativeFunction", nameof(CaptureGetAssertionReport));

        IntPtr result = SDL3.SDL.GetAssertionReport();

        TestAssert.Equal(nextPointer, result, "SDL.GetAssertionReport must return native pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetAssertionReport must call native hook once.");
    }

    public static void ResetAssertionReport_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ResetAssertionReport");
        AssertSdlLibraryImport(nativeMethod, "SDL_ResetAssertionReport");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("ResetAssertionReportNativeFunction", nameof(CaptureResetAssertionReport));

        SDL3.SDL.ResetAssertionReport();

        TestAssert.Equal(1, capturedCallCount, "SDL.ResetAssertionReport must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedData = IntPtr.Zero;
        capturedFunc = null;
        capturedFile = null;
        capturedLine = 0;
        nextAssertState = default;
        capturedAssertionHandler = null;
        nextAssertionHandler = null;
        capturedUserdata = IntPtr.Zero;
        capturedUserdataPointer = IntPtr.Zero;
        nextPointer = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static SDL3.SDL.AssertState CaptureReportAssertion(IntPtr data, string func, string file, int line)
    {
        capturedData = data;
        capturedFunc = func;
        capturedFile = file;
        capturedLine = line;
        return nextAssertState;
    }

    private static void CaptureSetAssertionHandler(SDL3.SDL.AssertionHandler handler, IntPtr userdata)
    {
        capturedAssertionHandler = handler;
        capturedUserdata = userdata;
        capturedCallCount++;
    }

    private static SDL3.SDL.AssertionHandler CaptureGetDefaultAssertionHandler()
    {
        capturedCallCount++;
        return nextAssertionHandler ?? throw new InvalidOperationException("Expected assertion handler was not configured.");
    }

    private static SDL3.SDL.AssertionHandler CaptureGetAssertionHandler(IntPtr puserdata)
    {
        capturedUserdataPointer = puserdata;
        capturedCallCount++;
        return nextAssertionHandler ?? throw new InvalidOperationException("Expected assertion handler was not configured.");
    }

    private static IntPtr CaptureGetAssertionReport()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureResetAssertionReport()
    {
        capturedCallCount++;
    }

    private static SDL3.SDL.AssertState HandleAssertion(in SDL3.SDL.AssertData data, IntPtr userdata)
    {
        return SDL3.SDL.AssertState.Ignore;
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

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected string marshalling.");
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
