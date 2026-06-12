using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Log;

internal static class PInvokeTests
{
    private static SDL3.SDL.LogCategory capturedCategory;
    private static SDL3.SDL.LogPriority capturedPriority;
    private static string? capturedFormat;
    private static string? capturedPrefix;
    private static string[]? capturedArguments;
    private static SDL3.SDL.LogOutputFunction? capturedCallback;
    private static IntPtr capturedUserdata;
    private static SDL3.SDL.LogPriority nextPriority;
    private static bool nextBool;
    private static SDL3.SDL.LogOutputFunction? nextCallback;
    private static IntPtr nextUserdata;
    private static int capturedCallCount;

    public static void SetLogPriorities_ForwardsPriority()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetLogPriorities");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetLogPriorities");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetLogPrioritiesNativeFunction", nameof(CaptureSetLogPriorities));

        SDL3.SDL.SetLogPriorities(SDL3.SDL.LogPriority.Warn);

        TestAssert.Equal(SDL3.SDL.LogPriority.Warn, capturedPriority, "SDL.SetLogPriorities must forward priority.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetLogPriorities must call native hook once.");
    }

    public static void SetLogPriority_ForwardsCategoryAndPriority()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetLogPriority");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetLogPriority");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetLogPriorityNativeFunction", nameof(CaptureSetLogPriority));

        SDL3.SDL.SetLogPriority(SDL3.SDL.LogCategory.Video, SDL3.SDL.LogPriority.Debug);

        TestAssert.Equal(SDL3.SDL.LogCategory.Video, capturedCategory, "SDL.SetLogPriority must forward category.");
        TestAssert.Equal(SDL3.SDL.LogPriority.Debug, capturedPriority, "SDL.SetLogPriority must forward priority.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetLogPriority must call native hook once.");
    }

    public static void GetLogPriority_ForwardsCategoryAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetLogPriority");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetLogPriority");

        ResetCaptureState();
        nextPriority = SDL3.SDL.LogPriority.Critical;
        using NativeHookScope _ = NativeHookScope.Install("GetLogPriorityNativeFunction", nameof(CaptureGetLogPriority));

        SDL3.SDL.LogPriority result = SDL3.SDL.GetLogPriority(SDL3.SDL.LogCategory.Render);

        TestAssert.Equal(SDL3.SDL.LogPriority.Critical, result, "SDL.GetLogPriority must return native priority.");
        TestAssert.Equal(SDL3.SDL.LogCategory.Render, capturedCategory, "SDL.GetLogPriority must forward category.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetLogPriority must call native hook once.");
    }

    public static void ResetLogPriorities_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ResetLogPriorities");
        AssertSdlLibraryImport(nativeMethod, "SDL_ResetLogPriorities");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("ResetLogPrioritiesNativeFunction", nameof(CaptureResetLogPriorities));

        SDL3.SDL.ResetLogPriorities();

        TestAssert.Equal(1, capturedCallCount, "SDL.ResetLogPriorities must call native hook once.");
    }

    public static void SetLogPriorityPrefix_ForwardsPriorityPrefixAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetLogPriorityPrefix");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetLogPriorityPrefix");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "prefix", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetLogPriorityPrefixNativeFunction", nameof(CaptureSetLogPriorityPrefix));

        bool result = SDL3.SDL.SetLogPriorityPrefix(SDL3.SDL.LogPriority.Error, "ERR: ");

        TestAssert.Equal(true, result, "SDL.SetLogPriorityPrefix must return native bool value.");
        TestAssert.Equal(SDL3.SDL.LogPriority.Error, capturedPriority, "SDL.SetLogPriorityPrefix must forward priority.");
        TestAssert.Equal("ERR: ", capturedPrefix, "SDL.SetLogPriorityPrefix must forward prefix.");

        nextBool = false;
        result = SDL3.SDL.SetLogPriorityPrefix(SDL3.SDL.LogPriority.Info, null);

        TestAssert.Equal(false, result, "SDL.SetLogPriorityPrefix must return native false value.");
        TestAssert.Equal(SDL3.SDL.LogPriority.Info, capturedPriority, "SDL.SetLogPriorityPrefix must forward second priority.");
        TestAssert.Equal<string?>(null, capturedPrefix, "SDL.SetLogPriorityPrefix must forward null prefix.");
        TestAssert.Equal(2, capturedCallCount, "SDL.SetLogPriorityPrefix must call native hook for both branches.");
    }

    public static void Log_ForwardsFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_Log");
        AssertSdlLibraryImport(nativeMethod, "SDL_Log");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogNativeFunction", nameof(CaptureLog));

        SDL3.SDL.Log("application message");

        TestAssert.Equal("application message", capturedFormat, "SDL.Log must forward format string.");
        TestAssert.Equal(1, capturedCallCount, "SDL.Log must call native hook once.");
    }

    public static void LogTrace_ForwardsCategoryAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogTrace");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogTrace");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogTraceNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogTrace(SDL3.SDL.LogCategory.Input, "trace message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.Input, "trace message", "SDL.LogTrace");
    }

    public static void LogVerbose_ForwardsCategoryAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogVerbose");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogVerbose");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogVerboseNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogVerbose(SDL3.SDL.LogCategory.Audio, "verbose message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.Audio, "verbose message", "SDL.LogVerbose");
    }

    public static void LogDebug_ForwardsCategoryAndMessage()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogDebug");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogDebug");
        AssertStringParameterMarshal(nativeMethod, "message", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogDebugNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogDebug(SDL3.SDL.LogCategory.Test, "debug message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.Test, "debug message", "SDL.LogDebug");
    }

    public static void LogInfo_ForwardsCategoryAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogInfo");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogInfo");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogInfoNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogInfo(SDL3.SDL.LogCategory.Application, "info message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.Application, "info message", "SDL.LogInfo");
    }

    public static void LogWarn_ForwardsCategoryAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogWarn");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogWarn");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogWarnNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogWarn(SDL3.SDL.LogCategory.System, "warn message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.System, "warn message", "SDL.LogWarn");
    }

    public static void LogError_ForwardsCategoryAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogError");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogError");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogErrorNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogError(SDL3.SDL.LogCategory.Error, "error message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.Error, "error message", "SDL.LogError");
    }

    public static void LogCritical_ForwardsCategoryAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogCritical");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogCritical");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogCriticalNativeFunction", nameof(CaptureLogCategoryFormat));

        SDL3.SDL.LogCritical(SDL3.SDL.LogCategory.GPU, "critical message");

        AssertCapturedCategoryFormat(SDL3.SDL.LogCategory.GPU, "critical message", "SDL.LogCritical");
    }

    public static void LogMessage_ForwardsCategoryPriorityAndFormat()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogMessage");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogMessage");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("LogMessageNativeFunction", nameof(CaptureLogMessage));

        SDL3.SDL.LogMessage(SDL3.SDL.LogCategory.Render, SDL3.SDL.LogPriority.Verbose, "render message");

        TestAssert.Equal(SDL3.SDL.LogCategory.Render, capturedCategory, "SDL.LogMessage must forward category.");
        TestAssert.Equal(SDL3.SDL.LogPriority.Verbose, capturedPriority, "SDL.LogMessage must forward priority.");
        TestAssert.Equal("render message", capturedFormat, "SDL.LogMessage must forward format.");
        TestAssert.Equal(1, capturedCallCount, "SDL.LogMessage must call native hook once.");
    }

    public static void LogMessageV_ForwardsCategoryPriorityFormatAndArguments()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LogMessageV");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogMessageV");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);
        AssertStringArrayParameterMarshal(nativeMethod, "ap", UnmanagedType.LPArray, UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        string[] arguments = ["left", "right"];
        using NativeHookScope _ = NativeHookScope.Install("LogMessageVNativeFunction", nameof(CaptureLogMessageV));

        SDL3.SDL.LogMessageV(SDL3.SDL.LogCategory.Custom, SDL3.SDL.LogPriority.Trace, "%s %s", arguments);

        TestAssert.Equal(SDL3.SDL.LogCategory.Custom, capturedCategory, "SDL.LogMessageV must forward category.");
        TestAssert.Equal(SDL3.SDL.LogPriority.Trace, capturedPriority, "SDL.LogMessageV must forward priority.");
        TestAssert.Equal("%s %s", capturedFormat, "SDL.LogMessageV must forward format.");
        TestAssert.True(ReferenceEquals(arguments, capturedArguments), "SDL.LogMessageV must forward the argument array.");
        TestAssert.Equal(1, capturedCallCount, "SDL.LogMessageV must call native hook once.");
    }

    public static void GetDefaultLogOutputFunction_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetDefaultLogOutputFunction");
        AssertSdlLibraryImport(nativeMethod, "SDL_LogOutputFunction");
        AssertLogOutputFunctionMetadata();

        ResetCaptureState();
        nextCallback = HandleLog;
        using NativeHookScope _ = NativeHookScope.Install("GetDefaultLogOutputFunctionNativeFunction", nameof(CaptureGetDefaultLogOutputFunction));

        SDL3.SDL.LogOutputFunction result = SDL3.SDL.GetDefaultLogOutputFunction();

        TestAssert.Equal(nextCallback, result, "SDL.GetDefaultLogOutputFunction must return native callback.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetDefaultLogOutputFunction must call native hook once.");
    }

    public static void GetLogOutputFunction_ReturnsCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetLogOutputFunction");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetLogOutputFunction");
        AssertLogOutputFunctionMetadata();

        ResetCaptureState();
        nextCallback = HandleLog;
        nextUserdata = (IntPtr)707;
        using NativeHookScope _ = NativeHookScope.Install("GetLogOutputFunctionNativeFunction", nameof(CaptureGetLogOutputFunction));

        SDL3.SDL.GetLogOutputFunction(out SDL3.SDL.LogOutputFunction callback, out IntPtr userdata);

        TestAssert.Equal(nextCallback, callback, "SDL.GetLogOutputFunction must return native callback.");
        TestAssert.Equal((IntPtr)707, userdata, "SDL.GetLogOutputFunction must return native userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetLogOutputFunction must call native hook once.");
    }

    public static void SetLogOutputFunction_ForwardsCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetLogOutputFunction");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetLogOutputFunction");
        AssertLogOutputFunctionMetadata();

        ResetCaptureState();
        SDL3.SDL.LogOutputFunction callback = HandleLog;
        using NativeHookScope _ = NativeHookScope.Install("SetLogOutputFunctionNativeFunction", nameof(CaptureSetLogOutputFunction));

        SDL3.SDL.SetLogOutputFunction(callback, (IntPtr)808);

        TestAssert.Equal(callback, capturedCallback!, "SDL.SetLogOutputFunction must forward callback.");
        TestAssert.Equal((IntPtr)808, capturedUserdata, "SDL.SetLogOutputFunction must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetLogOutputFunction must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedCategory = default;
        capturedPriority = default;
        capturedFormat = null;
        capturedPrefix = null;
        capturedArguments = null;
        capturedCallback = null;
        capturedUserdata = IntPtr.Zero;
        nextPriority = default;
        nextBool = false;
        nextCallback = null;
        nextUserdata = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static void CaptureSetLogPriorities(SDL3.SDL.LogPriority priority)
    {
        capturedPriority = priority;
        capturedCallCount++;
    }

    private static void CaptureSetLogPriority(SDL3.SDL.LogCategory category, SDL3.SDL.LogPriority priority)
    {
        capturedCategory = category;
        capturedPriority = priority;
        capturedCallCount++;
    }

    private static SDL3.SDL.LogPriority CaptureGetLogPriority(SDL3.SDL.LogCategory category)
    {
        capturedCategory = category;
        capturedCallCount++;
        return nextPriority;
    }

    private static void CaptureResetLogPriorities()
    {
        capturedCallCount++;
    }

    private static bool CaptureSetLogPriorityPrefix(SDL3.SDL.LogPriority priority, string? prefix)
    {
        capturedPriority = priority;
        capturedPrefix = prefix;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureLog(string fmt)
    {
        capturedFormat = fmt;
        capturedCallCount++;
    }

    private static void CaptureLogCategoryFormat(SDL3.SDL.LogCategory category, string fmt)
    {
        capturedCategory = category;
        capturedFormat = fmt;
        capturedCallCount++;
    }

    private static void CaptureLogMessage(SDL3.SDL.LogCategory category, SDL3.SDL.LogPriority priority, string fmt)
    {
        capturedCategory = category;
        capturedPriority = priority;
        capturedFormat = fmt;
        capturedCallCount++;
    }

    private static void CaptureLogMessageV(SDL3.SDL.LogCategory category, SDL3.SDL.LogPriority priority, string fmt, string[] ap)
    {
        capturedCategory = category;
        capturedPriority = priority;
        capturedFormat = fmt;
        capturedArguments = ap;
        capturedCallCount++;
    }

    private static SDL3.SDL.LogOutputFunction CaptureGetDefaultLogOutputFunction()
    {
        capturedCallCount++;
        return nextCallback!;
    }

    private static void CaptureGetLogOutputFunction(out SDL3.SDL.LogOutputFunction callback, out IntPtr userdata)
    {
        callback = nextCallback!;
        userdata = nextUserdata;
        capturedCallCount++;
    }

    private static void CaptureSetLogOutputFunction(SDL3.SDL.LogOutputFunction callback, IntPtr userdata)
    {
        capturedCallback = callback;
        capturedUserdata = userdata;
        capturedCallCount++;
    }

    private static void HandleLog(IntPtr userdata, SDL3.SDL.LogCategory category, SDL3.SDL.LogPriority priority, string message)
    {
    }

    private static void AssertCapturedCategoryFormat(SDL3.SDL.LogCategory expectedCategory, string expectedFormat, string methodName)
    {
        TestAssert.Equal(expectedCategory, capturedCategory, $"{methodName} must forward category.");
        TestAssert.Equal(expectedFormat, capturedFormat, $"{methodName} must forward format.");
        TestAssert.Equal(1, capturedCallCount, $"{methodName} must call native hook once.");
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

    private static void AssertStringArrayParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType, UnmanagedType arraySubType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected array marshalling.");
        TestAssert.Equal(arraySubType, marshalAs.ArraySubType, $"SDL.{method.Name} parameter {parameterName} must keep UTF-8 string array marshalling.");
    }

    private static void AssertLogOutputFunctionMetadata()
    {
        Type callbackType = typeof(SDL3.SDL.LogOutputFunction);
        UnmanagedFunctionPointerAttribute? callbackAttribute = callbackType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(callbackAttribute, "SDL.LogOutputFunction must keep unmanaged callback metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, callbackAttribute!.CallingConvention, "SDL.LogOutputFunction must use cdecl calling convention.");

        MethodInfo? invoke = callbackType.GetMethod("Invoke");
        TestAssert.NotNull(invoke, "SDL.LogOutputFunction.Invoke must exist.");
        ParameterInfo messageParameter = invoke!.GetParameters().Single(param => param.Name == "message");
        MarshalAsAttribute? marshalAs = messageParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, "SDL.LogOutputFunction message parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, "SDL.LogOutputFunction message parameter must use UTF-8 marshalling.");
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
