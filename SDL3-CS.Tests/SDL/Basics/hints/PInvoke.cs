using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Hints;

internal static class PInvokeTests
{
    private static string? capturedName;
    private static string? capturedValue;
    private static SDL3.SDL.HintPriority capturedPriority;
    private static bool capturedDefaultValue;
    private static SDL3.SDL.HintCallback? capturedCallback;
    private static IntPtr capturedUserdata;
    private static bool nextBool;
    private static int nextInt;
    private static IntPtr nextPointer;
    private static int capturedCallCount;

    public static void SetHintWithPriority_ForwardsNameValuePriorityAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetHintWithPriority");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetHintWithPriority");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "value", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetHintWithPriorityNativeFunction", nameof(CaptureSetHintWithPriority));

        bool result = SDL3.SDL.SetHintWithPriority("SDL_HINT_TEST", "enabled", SDL3.SDL.HintPriority.Override);

        TestAssert.Equal(true, result, "SDL.SetHintWithPriority must return native bool value.");
        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.SetHintWithPriority must forward name.");
        TestAssert.Equal("enabled", capturedValue, "SDL.SetHintWithPriority must forward value.");
        TestAssert.Equal(SDL3.SDL.HintPriority.Override, capturedPriority, "SDL.SetHintWithPriority must forward priority.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetHintWithPriority must call native hook once.");
    }

    public static void SetHint_ForwardsNameValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetHint");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetHint");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "value", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("SetHintNativeFunction", nameof(CaptureSetHint));

        bool result = SDL3.SDL.SetHint("SDL_HINT_TEST", "disabled");

        TestAssert.Equal(false, result, "SDL.SetHint must return native bool value.");
        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.SetHint must forward name.");
        TestAssert.Equal("disabled", capturedValue, "SDL.SetHint must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetHint must call native hook once.");
    }

    public static void ResetHint_ForwardsNameAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ResetHint");
        AssertSdlLibraryImport(nativeMethod, "SDL_ResetHint");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("ResetHintNativeFunction", nameof(CaptureResetHint));

        bool result = SDL3.SDL.ResetHint("SDL_HINT_TEST");

        TestAssert.Equal(true, result, "SDL.ResetHint must return native bool value.");
        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.ResetHint must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ResetHint must call native hook once.");
    }

    public static void ResetHints_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ResetHints");
        AssertSdlLibraryImport(nativeMethod, "SDL_ResetHints");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("ResetHintsNativeFunction", nameof(CaptureResetHints));

        SDL3.SDL.ResetHints();

        TestAssert.Equal(1, capturedCallCount, "SDL.ResetHints must call native hook once.");
    }

    public static void SDL_GetHint_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetHint");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetHint");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
    }

    public static void GetHint_ReturnsUtf8StringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetHintNativeFunction", nameof(CaptureGetHint));

        string? value = CaptureUtf8String(() => SDL3.SDL.GetHint("SDL_HINT_TEST"), "enabled");
        TestAssert.Equal("enabled", value, "SDL.GetHint must convert UTF-8 native hint value.");
        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.GetHint must forward name.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetHint("SDL_HINT_MISSING"), "SDL.GetHint must return null for native null.");
        TestAssert.Equal("SDL_HINT_MISSING", capturedName, "SDL.GetHint must forward name for null branch.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetHint must call native hook for both branches.");
    }

    public static void GetHintBoolean_ForwardsNameDefaultAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetHintBoolean");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetHintBoolean");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertBoolParameterMarshal(nativeMethod, "defaultValue", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("GetHintBooleanNativeFunction", nameof(CaptureGetHintBoolean));

        bool result = SDL3.SDL.GetHintBoolean("SDL_HINT_TEST", true);

        TestAssert.Equal(false, result, "SDL.GetHintBoolean must return native bool value.");
        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.GetHintBoolean must forward name.");
        TestAssert.Equal(true, capturedDefaultValue, "SDL.GetHintBoolean must forward default value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetHintBoolean must call native hook once.");
    }

    public static void AddHintCallback_ForwardsNameCallbackUserdataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AddHintCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_AddHintCallback");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextInt = 7;
        SDL3.SDL.HintCallback callback = HandleHint;
        using NativeHookScope _ = NativeHookScope.Install("AddHintCallbackNativeFunction", nameof(CaptureAddHintCallback));

        int result = SDL3.SDL.AddHintCallback("SDL_HINT_TEST", callback, (IntPtr)909);

        TestAssert.Equal(7, result, "SDL.AddHintCallback must return native int value.");
        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.AddHintCallback must forward name.");
        TestAssert.Equal(callback, capturedCallback!, "SDL.AddHintCallback must forward callback.");
        TestAssert.Equal((IntPtr)909, capturedUserdata, "SDL.AddHintCallback must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AddHintCallback must call native hook once.");
    }

    public static void RemoveHintCallback_ForwardsNameCallbackUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RemoveHintCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_RemoveHintCallback");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        SDL3.SDL.HintCallback callback = HandleHint;
        using NativeHookScope _ = NativeHookScope.Install("RemoveHintCallbackNativeFunction", nameof(CaptureRemoveHintCallback));

        SDL3.SDL.RemoveHintCallback("SDL_HINT_TEST", callback, (IntPtr)910);

        TestAssert.Equal("SDL_HINT_TEST", capturedName, "SDL.RemoveHintCallback must forward name.");
        TestAssert.Equal(callback, capturedCallback!, "SDL.RemoveHintCallback must forward callback.");
        TestAssert.Equal((IntPtr)910, capturedUserdata, "SDL.RemoveHintCallback must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RemoveHintCallback must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedName = null;
        capturedValue = null;
        capturedPriority = default;
        capturedDefaultValue = false;
        capturedCallback = null;
        capturedUserdata = IntPtr.Zero;
        nextBool = false;
        nextInt = 0;
        nextPointer = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static bool CaptureSetHintWithPriority(string name, string value, SDL3.SDL.HintPriority priority)
    {
        capturedName = name;
        capturedValue = value;
        capturedPriority = priority;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetHint(string name, string value)
    {
        capturedName = name;
        capturedValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureResetHint(string name)
    {
        capturedName = name;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureResetHints()
    {
        capturedCallCount++;
    }

    private static IntPtr CaptureGetHint(string name)
    {
        capturedName = name;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureGetHintBoolean(string name, bool defaultValue)
    {
        capturedName = name;
        capturedDefaultValue = defaultValue;
        capturedCallCount++;
        return nextBool;
    }

    private static int CaptureAddHintCallback(string name, SDL3.SDL.HintCallback callback, IntPtr userdata)
    {
        capturedName = name;
        capturedCallback = callback;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextInt;
    }

    private static void CaptureRemoveHintCallback(string name, SDL3.SDL.HintCallback callback, IntPtr userdata)
    {
        capturedName = name;
        capturedCallback = callback;
        capturedUserdata = userdata;
        capturedCallCount++;
    }

    private static void HandleHint(IntPtr userdata, string name, string oldValue, string newValue)
    {
    }

    private static string? CaptureUtf8String(Func<string?> action, string value)
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
