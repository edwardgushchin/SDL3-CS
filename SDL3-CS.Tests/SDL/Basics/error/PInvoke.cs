using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Error;

internal static class PInvokeTests
{
    private static string? capturedMessage;
    private static string? capturedFormat;
    private static string[]? capturedArguments;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static int capturedCallCount;

    public static void SetError_ForwardsMessageAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetError");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetError");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "message", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("SetErrorNativeFunction", nameof(CaptureSetError));

        bool result = SDL3.SDL.SetError("failure details");

        TestAssert.Equal(false, result, "SDL.SetError must return native bool value.");
        TestAssert.Equal("failure details", capturedMessage, "SDL.SetError must forward message.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetError must call native hook once.");
    }

    public static void SetErrorV_ForwardsFormatAndArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetErrorV");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetErrorV");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.U1);
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);
        AssertStringArrayParameterMarshal(nativeMethod, "ap");

        ResetCaptureState();
        nextBool = true;
        string[] arguments = ["one", "two"];
        using NativeHookScope _ = NativeHookScope.Install("SetErrorVNativeFunction", nameof(CaptureSetErrorV));

        bool result = SDL3.SDL.SetErrorV("fmt %s %s", arguments);

        TestAssert.Equal(true, result, "SDL.SetErrorV must return native bool value.");
        TestAssert.Equal("fmt %s %s", capturedFormat, "SDL.SetErrorV must forward format string.");
        TestAssert.NotNull(capturedArguments, "SDL.SetErrorV must forward argument array.");
        TestAssert.Equal(2, capturedArguments!.Length, "SDL.SetErrorV must preserve argument count.");
        TestAssert.Equal("one", capturedArguments[0], "SDL.SetErrorV must forward argument 0.");
        TestAssert.Equal("two", capturedArguments[1], "SDL.SetErrorV must forward argument 1.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetErrorV must call native hook once.");
    }

    public static void OutOfMemory_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OutOfMemory");
        AssertSdlLibraryImport(nativeMethod, "SDL_OutOfMemory");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.U1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("OutOfMemoryNativeFunction", nameof(CaptureBool));

        bool result = SDL3.SDL.OutOfMemory();

        TestAssert.Equal(false, result, "SDL.OutOfMemory must return native bool value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OutOfMemory must call native hook once.");
    }

    public static void SDL_GetError_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetError");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetError");
    }

    public static void GetError_ReturnsUtf8StringAndEmpty()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetErrorNativeFunction", nameof(CaptureGetError));

        string value = CaptureUtf8String(SDL3.SDL.GetError, "last error");
        TestAssert.Equal("last error", value, "SDL.GetError must convert UTF-8 native error string.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal("", SDL3.SDL.GetError(), "SDL.GetError must return empty string for native null.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetError must call native hook for both branches.");
    }

    public static void ClearError_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ClearError");
        AssertSdlLibraryImport(nativeMethod, "SDL_ClearError");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.U1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("ClearErrorNativeFunction", nameof(CaptureBool));

        bool result = SDL3.SDL.ClearError();

        TestAssert.Equal(true, result, "SDL.ClearError must return native bool value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ClearError must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedMessage = null;
        capturedFormat = null;
        capturedArguments = null;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static bool CaptureSetError(string message)
    {
        capturedMessage = message;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetErrorV(string fmt, string[] ap)
    {
        capturedFormat = fmt;
        capturedArguments = ap;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureGetError()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static string CaptureUtf8String(Func<string> action, string value)
    {
        IntPtr pointer = Marshal.StringToCoTaskMemUTF8(value);
        nextPointer = pointer;

        try
        {
            return action();
        }
        finally
        {
            Marshal.FreeCoTaskMem(pointer);
        }
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

    private static void AssertStringArrayParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use LPArray marshalling.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs.ArraySubType, $"SDL.{method.Name} parameter {parameterName} must keep UTF-8 array subtype.");
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
