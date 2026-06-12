using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Init;

internal static class PInvokeTests
{
    private static SDL3.SDL.InitFlags capturedFlags;
    private static SDL3.SDL.InitFlags nextFlags;
    private static SDL3.SDL.MainThreadCallback? capturedMainThreadCallback;
    private static IntPtr capturedUserdata;
    private static bool capturedWaitComplete;
    private static string? capturedAppName;
    private static string? capturedAppVersion;
    private static string? capturedAppIdentifier;
    private static string? capturedName;
    private static string? capturedValue;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static int capturedCallCount;

    public static void Init_ForwardsFlagsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_Init");
        AssertSdlLibraryImport(nativeMethod, "SDL_Init");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("InitNativeFunction", nameof(CaptureInit));

        bool result = SDL3.SDL.Init(SDL3.SDL.InitFlags.Video | SDL3.SDL.InitFlags.Events);

        TestAssert.Equal(true, result, "SDL.Init must return native bool value.");
        TestAssert.Equal(SDL3.SDL.InitFlags.Video | SDL3.SDL.InitFlags.Events, capturedFlags, "SDL.Init must forward flags.");
        TestAssert.Equal(1, capturedCallCount, "SDL.Init must call native hook once.");
    }

    public static void InitSubSystem_ForwardsFlagsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_InitSubSystem");
        AssertSdlLibraryImport(nativeMethod, "SDL_InitSubSystem");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("InitSubSystemNativeFunction", nameof(CaptureInitFlagsBool));

        bool result = SDL3.SDL.InitSubSystem(SDL3.SDL.InitFlags.Audio);

        TestAssert.Equal(false, result, "SDL.InitSubSystem must return native bool value.");
        TestAssert.Equal(SDL3.SDL.InitFlags.Audio, capturedFlags, "SDL.InitSubSystem must forward flags.");
        TestAssert.Equal(1, capturedCallCount, "SDL.InitSubSystem must call native hook once.");
    }

    public static void QuitSubSystem_ForwardsFlags()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_QuitSubSystem");
        AssertSdlLibraryImport(nativeMethod, "SDL_QuitSubSystem");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("QuitSubSystemNativeFunction", nameof(CaptureInitFlagsVoid));

        SDL3.SDL.QuitSubSystem(SDL3.SDL.InitFlags.Audio | SDL3.SDL.InitFlags.Events);

        TestAssert.Equal(SDL3.SDL.InitFlags.Audio | SDL3.SDL.InitFlags.Events, capturedFlags, "SDL.QuitSubSystem must forward flags.");
        TestAssert.Equal(1, capturedCallCount, "SDL.QuitSubSystem must call native hook once.");
    }

    public static void WasInit_ForwardsFlagsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WasInit");
        AssertSdlLibraryImport(nativeMethod, "SDL_WasInit");

        ResetCaptureState();
        nextFlags = SDL3.SDL.InitFlags.Video | SDL3.SDL.InitFlags.Events;
        using NativeHookScope _ = NativeHookScope.Install("WasInitNativeFunction", nameof(CaptureWasInit));

        SDL3.SDL.InitFlags result = SDL3.SDL.WasInit(SDL3.SDL.InitFlags.Video);

        TestAssert.Equal(SDL3.SDL.InitFlags.Video | SDL3.SDL.InitFlags.Events, result, "SDL.WasInit must return native flags.");
        TestAssert.Equal(SDL3.SDL.InitFlags.Video, capturedFlags, "SDL.WasInit must forward flags.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WasInit must call native hook once.");
    }

    public static void Quit_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_Quit");
        AssertSdlLibraryImport(nativeMethod, "SDL_Quit");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("QuitNativeFunction", nameof(CaptureQuit));

        SDL3.SDL.Quit();

        TestAssert.Equal(1, capturedCallCount, "SDL.Quit must call native hook once.");
    }

    public static void IsMainThread_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsMainThread");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsMainThread");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("IsMainThreadNativeFunction", nameof(CaptureBool));

        bool result = SDL3.SDL.IsMainThread();

        TestAssert.Equal(true, result, "SDL.IsMainThread must return native bool value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IsMainThread must call native hook once.");
    }

    public static void RunOnMainThread_ForwardsCallbackUserdataWaitAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RunOnMainThread");
        AssertSdlLibraryImport(nativeMethod, "SDL_RunOnMainThread");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertBoolParameterMarshal(nativeMethod, "waitComplete", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.MainThreadCallback callback = HandleMainThread;
        using NativeHookScope _ = NativeHookScope.Install("RunOnMainThreadNativeFunction", nameof(CaptureRunOnMainThread));

        bool result = SDL3.SDL.RunOnMainThread(callback, (IntPtr)606, true);

        TestAssert.Equal(false, result, "SDL.RunOnMainThread must return native bool value.");
        TestAssert.Equal(callback, capturedMainThreadCallback!, "SDL.RunOnMainThread must forward callback.");
        TestAssert.Equal((IntPtr)606, capturedUserdata, "SDL.RunOnMainThread must forward userdata.");
        TestAssert.Equal(true, capturedWaitComplete, "SDL.RunOnMainThread must forward waitComplete.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RunOnMainThread must call native hook once.");
    }

    public static void SetAppMetadata_ForwardsStringsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAppMetadata");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAppMetadata");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "appname", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "appversion", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "appidentifier", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetAppMetadataNativeFunction", nameof(CaptureSetAppMetadata));

        bool result = SDL3.SDL.SetAppMetadata("Game", "1.2.3", "com.example.game");

        TestAssert.Equal(true, result, "SDL.SetAppMetadata must return native bool value.");
        TestAssert.Equal("Game", capturedAppName, "SDL.SetAppMetadata must forward appname.");
        TestAssert.Equal("1.2.3", capturedAppVersion, "SDL.SetAppMetadata must forward appversion.");
        TestAssert.Equal("com.example.game", capturedAppIdentifier, "SDL.SetAppMetadata must forward appidentifier.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetAppMetadata must call native hook once.");
    }

    public static void SetAppMetadataProperty_ForwardsStringsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAppMetadataProperty");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAppMetadataProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "value", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("SetAppMetadataPropertyNativeFunction", nameof(CaptureSetAppMetadataProperty));

        bool result = SDL3.SDL.SetAppMetadataProperty("SDL.app.metadata.name", "Game");

        TestAssert.Equal(false, result, "SDL.SetAppMetadataProperty must return native bool value.");
        TestAssert.Equal("SDL.app.metadata.name", capturedName, "SDL.SetAppMetadataProperty must forward name.");
        TestAssert.Equal("Game", capturedValue, "SDL.SetAppMetadataProperty must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetAppMetadataProperty must call native hook once.");
    }

    public static void SDL_GetAppMetadataProperty_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAppMetadataProperty");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAppMetadataProperty");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
    }

    public static void GetAppMetadataProperty_ReturnsUtf8StringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetAppMetadataPropertyNativeFunction", nameof(CaptureGetAppMetadataProperty));

        string? value = CaptureUtf8String(() => SDL3.SDL.GetAppMetadataProperty("SDL.app.metadata.name"), "Game");
        TestAssert.Equal("Game", value, "SDL.GetAppMetadataProperty must convert UTF-8 native metadata value.");
        TestAssert.Equal("SDL.app.metadata.name", capturedName, "SDL.GetAppMetadataProperty must forward name.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetAppMetadataProperty("SDL.app.metadata.url"), "SDL.GetAppMetadataProperty must return null for native null.");
        TestAssert.Equal("SDL.app.metadata.url", capturedName, "SDL.GetAppMetadataProperty must forward name for null branch.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetAppMetadataProperty must call native hook for both branches.");
    }

    private static void ResetCaptureState()
    {
        capturedFlags = default;
        nextFlags = default;
        capturedMainThreadCallback = null;
        capturedUserdata = IntPtr.Zero;
        capturedWaitComplete = false;
        capturedAppName = null;
        capturedAppVersion = null;
        capturedAppIdentifier = null;
        capturedName = null;
        capturedValue = null;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static bool CaptureInit(SDL3.SDL.InitFlags flags)
    {
        capturedFlags = flags;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureInitFlagsBool(SDL3.SDL.InitFlags flags)
    {
        capturedFlags = flags;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureInitFlagsVoid(SDL3.SDL.InitFlags flags)
    {
        capturedFlags = flags;
        capturedCallCount++;
    }

    private static SDL3.SDL.InitFlags CaptureWasInit(SDL3.SDL.InitFlags flags)
    {
        capturedFlags = flags;
        capturedCallCount++;
        return nextFlags;
    }

    private static void CaptureQuit()
    {
        capturedCallCount++;
    }

    private static bool CaptureBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureRunOnMainThread(SDL3.SDL.MainThreadCallback callback, IntPtr userdata, bool waitComplete)
    {
        capturedMainThreadCallback = callback;
        capturedUserdata = userdata;
        capturedWaitComplete = waitComplete;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetAppMetadata(string appname, string appversion, string appidentifier)
    {
        capturedAppName = appname;
        capturedAppVersion = appversion;
        capturedAppIdentifier = appidentifier;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetAppMetadataProperty(string name, string value)
    {
        capturedName = name;
        capturedValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureGetAppMetadataProperty(string name)
    {
        capturedName = name;
        capturedCallCount++;
        return nextPointer;
    }

    private static void HandleMainThread(IntPtr userdata)
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
