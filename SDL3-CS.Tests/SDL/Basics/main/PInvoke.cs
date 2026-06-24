using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Main;

internal static class PInvokeTests
{
    private static IntPtr capturedAppstate;
    private static IntPtr nextAppstate;
    private static int capturedArgc;
    private static string[]? capturedArgv;
    private static uint capturedEventType;
    private static SDL3.SDL.AppResult capturedResult;
    private static SDL3.SDL.MainFunc? capturedMainFunction;
    private static SDL3.SDL.AppInitFunc? capturedAppInit;
    private static SDL3.SDL.AppIterateFunc? capturedAppIterate;
    private static SDL3.SDL.AppEventFunc? capturedAppEvent;
    private static SDL3.SDL.AppQuitFunc? capturedAppQuit;
    private static string? capturedName;
    private static uint capturedStyle;
    private static IntPtr capturedHInst;
    private static IntPtr capturedReserved;
    private static SDL3.SDL.AppResult nextAppResult;
    private static int nextInt;
    private static bool nextBool;
    private static int capturedCallCount;

    public static void AppInit_ForwardsStateArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AppInit");
        AssertLibraryImport(nativeMethod, "SDL_AppInit");
        AssertByRefIntPtrParameter(nativeMethod, "appstate");
        AssertStringArrayParameterMarshal(nativeMethod, "argv", UnmanagedType.LPArray, UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextAppResult = SDL3.SDL.AppResult.Continue;
        nextAppstate = (IntPtr)909;
        string[] argv = ["app", "--video"];
        using NativeHookScope _ = NativeHookScope.Install("AppInitNativeFunction", nameof(CaptureAppInit));

        IntPtr appstate = (IntPtr)101;
        SDL3.SDL.AppResult result = SDL3.SDL.AppInit(ref appstate, argv.Length, argv);

        TestAssert.Equal(SDL3.SDL.AppResult.Continue, result, "SDL.AppInit must return native AppResult.");
        TestAssert.Equal((IntPtr)101, capturedAppstate, "SDL.AppInit must forward initial appstate.");
        TestAssert.Equal((IntPtr)909, appstate, "SDL.AppInit must preserve appstate mutations from the native callback.");
        TestAssert.Equal(argv.Length, capturedArgc, "SDL.AppInit must forward argc.");
        TestAssert.True(ReferenceEquals(argv, capturedArgv), "SDL.AppInit must forward argv array.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AppInit must call native hook once.");
    }

    public static void AppInit_NullAndEmptyArgumentsForwardNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("AppInitNativeFunction", nameof(CaptureAppInit));

        IntPtr appstate = IntPtr.Zero;
        SDL3.SDL.AppInit(ref appstate, 0, null!);

        TestAssert.Equal(0, capturedArgc, "SDL.AppInit must forward zero argc for null argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.AppInit must forward null argv unchanged.");

        string[] argv = [];
        SDL3.SDL.AppInit(ref appstate, 0, argv);

        TestAssert.Equal(0, capturedArgc, "SDL.AppInit must forward zero argc for empty argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.AppInit must normalize empty argv to null before native marshalling.");
        TestAssert.Equal(2, capturedCallCount, "SDL.AppInit must call native hook for both branches.");
    }

    public static void AppInitFunc_AllowsCallbackToAssignAppstate()
    {
        MethodInfo invoke = typeof(SDL3.SDL.AppInitFunc).GetMethod("Invoke")!;
        AssertByRefIntPtrParameter(invoke, "appstate");

        IntPtr appstate = IntPtr.Zero;
        SDL3.SDL.AppInitFunc appinit = AssignAppstate;

        SDL3.SDL.AppResult result = appinit(ref appstate, 0, null);

        TestAssert.Equal(SDL3.SDL.AppResult.Continue, result, "SDL.AppInitFunc callback must return AppResult.");
        TestAssert.Equal((IntPtr)5150, appstate, "SDL.AppInitFunc must allow managed callbacks to assign appstate.");
    }

    public static void AppIterate_ForwardsStateAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AppIterate");
        AssertLibraryImport(nativeMethod, "SDL_AppIterate");

        ResetCaptureState();
        nextAppResult = SDL3.SDL.AppResult.Success;
        using NativeHookScope _ = NativeHookScope.Install("AppIterateNativeFunction", nameof(CaptureAppIterate));

        SDL3.SDL.AppResult result = SDL3.SDL.AppIterate((IntPtr)202);

        TestAssert.Equal(SDL3.SDL.AppResult.Success, result, "SDL.AppIterate must return native AppResult.");
        TestAssert.Equal((IntPtr)202, capturedAppstate, "SDL.AppIterate must forward appstate.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AppIterate must call native hook once.");
    }

    public static void AppEvent_ForwardsStateEventRefAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AppEvent");
        AssertLibraryImport(nativeMethod, "SDL_AppEvent");
        AssertPointerParameter(nativeMethod, "event", typeof(SDL3.SDL.Event));

        ResetCaptureState();
        nextAppResult = SDL3.SDL.AppResult.Failure;
        SDL3.SDL.Event @event = new()
        {
            Type = (uint)SDL3.SDL.EventType.Quit
        };
        using NativeHookScope _ = NativeHookScope.Install("AppEventNativeFunction", nameof(CaptureAppEvent));

        SDL3.SDL.AppResult result = SDL3.SDL.AppEvent((IntPtr)303, ref @event);

        TestAssert.Equal(SDL3.SDL.AppResult.Failure, result, "SDL.AppEvent must return native AppResult.");
        TestAssert.Equal((IntPtr)303, capturedAppstate, "SDL.AppEvent must forward appstate.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.Quit, capturedEventType, "SDL.AppEvent must pass the event by reference.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.Terminating, @event.Type, "SDL.AppEvent must preserve ref event mutations.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AppEvent must call native hook once.");
    }

    public static void AppQuit_ForwardsStateAndResult()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AppQuit");
        AssertLibraryImport(nativeMethod, "SDL_AppQuit");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("AppQuitNativeFunction", nameof(CaptureAppQuit));

        SDL3.SDL.AppQuit((IntPtr)404, SDL3.SDL.AppResult.Failure);

        TestAssert.Equal((IntPtr)404, capturedAppstate, "SDL.AppQuit must forward appstate.");
        TestAssert.Equal(SDL3.SDL.AppResult.Failure, capturedResult, "SDL.AppQuit must forward result.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AppQuit must call native hook once.");
    }

    public static void Main_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_main");
        AssertLibraryImport(nativeMethod, "SDL_main");
        AssertStringArrayParameterMarshal(nativeMethod, "argv", UnmanagedType.LPArray, UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextInt = 42;
        string[] argv = ["app", "--safe"];
        using NativeHookScope _ = NativeHookScope.Install("MainNativeFunction", nameof(CaptureMain));

        int result = SDL3.SDL.Main(argv.Length, argv);

        TestAssert.Equal(42, result, "SDL.Main must return native int value.");
        TestAssert.Equal(argv.Length, capturedArgc, "SDL.Main must forward argc.");
        TestAssert.True(ReferenceEquals(argv, capturedArgv), "SDL.Main must forward argv array.");
        TestAssert.Equal(1, capturedCallCount, "SDL.Main must call native hook once.");
    }

    public static void Main_NullAndEmptyArgumentsForwardNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("MainNativeFunction", nameof(CaptureMain));

        SDL3.SDL.Main(0, null!);

        TestAssert.Equal(0, capturedArgc, "SDL.Main must forward zero argc for null argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.Main must forward null argv unchanged.");

        string[] argv = [];
        SDL3.SDL.Main(0, argv);

        TestAssert.Equal(0, capturedArgc, "SDL.Main must forward zero argc for empty argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.Main must normalize empty argv to null before native marshalling.");
        TestAssert.Equal(2, capturedCallCount, "SDL.Main must call native hook for both branches.");
    }

    public static void SetMainReady_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetMainReady");
        AssertLibraryImport(nativeMethod, "SDL_SetMainReady");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetMainReadyNativeFunction", nameof(CaptureSetMainReady));

        SDL3.SDL.SetMainReady();

        TestAssert.Equal(1, capturedCallCount, "SDL.SetMainReady must call native hook once.");
    }

    public static void RunApp_ForwardsArgumentsCallbackReservedAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RunApp");
        AssertLibraryImport(nativeMethod, "SDL_RunApp");
        AssertStringArrayParameterMarshal(nativeMethod, "argv", UnmanagedType.LPArray, UnmanagedType.LPUTF8Str);
        AssertCallbackCdecl(typeof(SDL3.SDL.MainFunc), "SDL.MainFunc");

        ResetCaptureState();
        nextInt = 77;
        string[] argv = ["app", "--run"];
        SDL3.SDL.MainFunc mainFunction = HandleMain;
        using NativeHookScope _ = NativeHookScope.Install("RunAppNativeFunction", nameof(CaptureRunApp));

        int result = SDL3.SDL.RunApp(argv.Length, argv, mainFunction, (IntPtr)505);

        TestAssert.Equal(77, result, "SDL.RunApp must return native int value.");
        TestAssert.Equal(argv.Length, capturedArgc, "SDL.RunApp must forward argc.");
        TestAssert.True(ReferenceEquals(argv, capturedArgv), "SDL.RunApp must forward argv array.");
        TestAssert.Equal(mainFunction, capturedMainFunction!, "SDL.RunApp must forward main callback.");
        TestAssert.Equal((IntPtr)505, capturedReserved, "SDL.RunApp must forward reserved pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RunApp must call native hook once.");
    }

    public static void RunApp_NullAndEmptyArgumentsForwardNull()
    {
        ResetCaptureState();
        SDL3.SDL.MainFunc mainFunction = HandleMain;
        using NativeHookScope _ = NativeHookScope.Install("RunAppNativeFunction", nameof(CaptureRunApp));

        SDL3.SDL.RunApp(0, null!, mainFunction, IntPtr.Zero);

        TestAssert.Equal(0, capturedArgc, "SDL.RunApp must forward zero argc for null argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.RunApp must forward null argv unchanged.");

        string[] argv = [];
        SDL3.SDL.RunApp(0, argv, mainFunction, IntPtr.Zero);

        TestAssert.Equal(0, capturedArgc, "SDL.RunApp must forward zero argc for empty argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.RunApp must normalize empty argv to null before native marshalling.");
        TestAssert.Equal(2, capturedCallCount, "SDL.RunApp must call native hook for both branches.");
    }

    public static void EnterAppMainCallbacks_ForwardsArgumentsAndCallbacksAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EnterAppMainCallbacks");
        AssertLibraryImport(nativeMethod, "SDL_EnterAppMainCallbacks");
        AssertStringArrayParameterMarshal(nativeMethod, "argv", UnmanagedType.LPArray, UnmanagedType.LPUTF8Str);
        AssertCallbackCdecl(typeof(SDL3.SDL.AppInitFunc), "SDL.AppInitFunc");
        AssertByRefIntPtrParameter(typeof(SDL3.SDL.AppInitFunc).GetMethod("Invoke")!, "appstate");
        AssertCallbackCdecl(typeof(SDL3.SDL.AppIterateFunc), "SDL.AppIterateFunc");
        AssertCallbackCdecl(typeof(SDL3.SDL.AppEventFunc), "SDL.AppEventFunc");
        AssertCallbackCdecl(typeof(SDL3.SDL.AppQuitFunc), "SDL.AppQuitFunc");

        ResetCaptureState();
        nextInt = 99;
        string[] argv = ["app", "--callbacks"];
        SDL3.SDL.AppInitFunc appinit = HandleAppInit;
        SDL3.SDL.AppIterateFunc appiter = HandleAppIterate;
        SDL3.SDL.AppEventFunc appevent = HandleAppEvent;
        SDL3.SDL.AppQuitFunc appquit = HandleAppQuit;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(CaptureEnterAppMainCallbacks));

        int result = SDL3.SDL.EnterAppMainCallbacks(argv.Length, argv, appinit, appiter, appevent, appquit);

        TestAssert.Equal(99, result, "SDL.EnterAppMainCallbacks must return native int value.");
        TestAssert.Equal(argv.Length, capturedArgc, "SDL.EnterAppMainCallbacks must forward argc.");
        TestAssert.True(ReferenceEquals(argv, capturedArgv), "SDL.EnterAppMainCallbacks must forward argv array.");
        TestAssert.Equal(appinit, capturedAppInit!, "SDL.EnterAppMainCallbacks must forward appinit.");
        TestAssert.Equal(appiter, capturedAppIterate!, "SDL.EnterAppMainCallbacks must forward appiter.");
        TestAssert.Equal(appevent, capturedAppEvent!, "SDL.EnterAppMainCallbacks must forward appevent.");
        TestAssert.Equal(appquit, capturedAppQuit!, "SDL.EnterAppMainCallbacks must forward appquit.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EnterAppMainCallbacks must call native hook once.");
    }

    public static void EnterAppMainCallbacks_NullAndEmptyArgumentsForwardNull()
    {
        ResetCaptureState();
        SDL3.SDL.AppInitFunc appinit = HandleAppInit;
        SDL3.SDL.AppIterateFunc appiter = HandleAppIterate;
        SDL3.SDL.AppEventFunc appevent = HandleAppEvent;
        SDL3.SDL.AppQuitFunc appquit = HandleAppQuit;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(CaptureEnterAppMainCallbacks));

        SDL3.SDL.EnterAppMainCallbacks(0, null!, appinit, appiter, appevent, appquit);

        TestAssert.Equal(0, capturedArgc, "SDL.EnterAppMainCallbacks must forward zero argc for null argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.EnterAppMainCallbacks must forward null argv unchanged.");

        string[] argv = [];
        SDL3.SDL.EnterAppMainCallbacks(0, argv, appinit, appiter, appevent, appquit);

        TestAssert.Equal(0, capturedArgc, "SDL.EnterAppMainCallbacks must forward zero argc for empty argv.");
        TestAssert.Equal<string[]?>(null, capturedArgv, "SDL.EnterAppMainCallbacks must normalize empty argv to null before native marshalling.");
        TestAssert.Equal(2, capturedCallCount, "SDL.EnterAppMainCallbacks must call native hook for both branches.");
    }

    public static void RegisterApp_ForwardsNameStyleHInstAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RegisterApp");
        AssertLibraryImport(nativeMethod, "SDL_RegisterApp");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("RegisterAppNativeFunction", nameof(CaptureRegisterApp));

        bool result = SDL3.SDL.RegisterApp("SDL_app", 123, (IntPtr)606);

        TestAssert.Equal(true, result, "SDL.RegisterApp must return native bool value.");
        TestAssert.Equal("SDL_app", capturedName, "SDL.RegisterApp must forward name.");
        TestAssert.Equal<uint>(123, capturedStyle, "SDL.RegisterApp must forward style.");
        TestAssert.Equal((IntPtr)606, capturedHInst, "SDL.RegisterApp must forward hInst.");

        nextBool = false;
        result = SDL3.SDL.RegisterApp(null, 0, IntPtr.Zero);

        TestAssert.Equal(false, result, "SDL.RegisterApp must return native false value.");
        TestAssert.Equal<string?>(null, capturedName, "SDL.RegisterApp must forward null name.");
        TestAssert.Equal<uint>(0, capturedStyle, "SDL.RegisterApp must forward zero style.");
        TestAssert.Equal(IntPtr.Zero, capturedHInst, "SDL.RegisterApp must forward zero hInst.");
        TestAssert.Equal(2, capturedCallCount, "SDL.RegisterApp must call native hook for both branches.");
    }

    public static void UnregisterApp_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UnregisterApp");
        AssertLibraryImport(nativeMethod, "SDL_UnregisterApp");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("UnregisterAppNativeFunction", nameof(CaptureUnregisterApp));

        SDL3.SDL.UnregisterApp();

        TestAssert.Equal(1, capturedCallCount, "SDL.UnregisterApp must call native hook once.");
    }

    public static void GDKSuspendComplete_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GDKSuspendComplete");
        AssertLibraryImport(nativeMethod, "SDL_GDKSuspendComplete");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GDKSuspendCompleteNativeFunction", nameof(CaptureGDKSuspendComplete));

        SDL3.SDL.GDKSuspendComplete();

        TestAssert.Equal(1, capturedCallCount, "SDL.GDKSuspendComplete must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedAppstate = IntPtr.Zero;
        capturedArgc = 0;
        capturedArgv = null;
        capturedEventType = 0;
        capturedResult = default;
        capturedMainFunction = null;
        capturedAppInit = null;
        capturedAppIterate = null;
        capturedAppEvent = null;
        capturedAppQuit = null;
        capturedName = null;
        capturedStyle = 0;
        capturedHInst = IntPtr.Zero;
        capturedReserved = IntPtr.Zero;
        nextAppstate = IntPtr.Zero;
        nextAppResult = default;
        nextInt = 0;
        nextBool = false;
        capturedCallCount = 0;
    }

    private static SDL3.SDL.AppResult CaptureAppInit(ref IntPtr appstate, int argc, string[]? argv)
    {
        capturedAppstate = appstate;
        appstate = nextAppstate;
        capturedArgc = argc;
        capturedArgv = argv;
        capturedCallCount++;
        return nextAppResult;
    }

    private static SDL3.SDL.AppResult CaptureAppIterate(IntPtr appstate)
    {
        capturedAppstate = appstate;
        capturedCallCount++;
        return nextAppResult;
    }

    private static unsafe SDL3.SDL.AppResult CaptureAppEvent(IntPtr appstate, SDL3.SDL.Event* @event)
    {
        capturedAppstate = appstate;
        capturedEventType = @event->Type;
        @event->Type = (uint)SDL3.SDL.EventType.Terminating;
        capturedCallCount++;
        return nextAppResult;
    }

    private static void CaptureAppQuit(IntPtr appstate, SDL3.SDL.AppResult result)
    {
        capturedAppstate = appstate;
        capturedResult = result;
        capturedCallCount++;
    }

    private static int CaptureMain(int argc, string[]? argv)
    {
        capturedArgc = argc;
        capturedArgv = argv;
        capturedCallCount++;
        return nextInt;
    }

    private static void CaptureSetMainReady()
    {
        capturedCallCount++;
    }

    private static int CaptureRunApp(int argc, string[]? argv, SDL3.SDL.MainFunc mainFunction, IntPtr reserved)
    {
        capturedArgc = argc;
        capturedArgv = argv;
        capturedMainFunction = mainFunction;
        capturedReserved = reserved;
        capturedCallCount++;
        return nextInt;
    }

    private static int CaptureEnterAppMainCallbacks(
        int argc,
        string[]? argv,
        SDL3.SDL.AppInitFunc appinit,
        SDL3.SDL.AppIterateFunc appiter,
        SDL3.SDL.AppEventFunc appevent,
        SDL3.SDL.AppQuitFunc appquit)
    {
        capturedArgc = argc;
        capturedArgv = argv;
        capturedAppInit = appinit;
        capturedAppIterate = appiter;
        capturedAppEvent = appevent;
        capturedAppQuit = appquit;
        capturedCallCount++;
        return nextInt;
    }

    private static bool CaptureRegisterApp(string? name, uint style, IntPtr hInst)
    {
        capturedName = name;
        capturedStyle = style;
        capturedHInst = hInst;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureUnregisterApp()
    {
        capturedCallCount++;
    }

    private static void CaptureGDKSuspendComplete()
    {
        capturedCallCount++;
    }

    private static int HandleMain(int argc, string[]? argv)
    {
        return 0;
    }

    private static SDL3.SDL.AppResult HandleAppInit(ref IntPtr appstate, int argc, string[]? argv)
    {
        return SDL3.SDL.AppResult.Continue;
    }

    private static SDL3.SDL.AppResult AssignAppstate(ref IntPtr appstate, int argc, string[]? argv)
    {
        appstate = (IntPtr)5150;
        return SDL3.SDL.AppResult.Continue;
    }

    private static SDL3.SDL.AppResult HandleAppIterate(IntPtr appstate)
    {
        return SDL3.SDL.AppResult.Continue;
    }

    private static SDL3.SDL.AppResult HandleAppEvent(IntPtr appstate, ref SDL3.SDL.Event @event)
    {
        return SDL3.SDL.AppResult.Continue;
    }

    private static void HandleAppQuit(IntPtr appstate, SDL3.SDL.AppResult result)
    {
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method);
    }

    private static void AssertDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method);
    }

    private static void AssertCdecl(MethodInfo method)
    {
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

    private static void AssertByRefIntPtrParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"{method.DeclaringType?.Name}.{method.Name} parameter {parameterName} must be passed by reference.");
        TestAssert.Equal(typeof(IntPtr), parameter.ParameterType.GetElementType(), $"{method.DeclaringType?.Name}.{method.Name} parameter {parameterName} must be a by-ref IntPtr.");
    }

    private static void AssertPointerParameter(MethodInfo method, string parameterName, Type elementType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsPointer, $"{method.DeclaringType?.Name}.{method.Name} parameter {parameterName} must be passed as a native pointer.");
        TestAssert.Equal(elementType, parameter.ParameterType.GetElementType(), $"{method.DeclaringType?.Name}.{method.Name} parameter {parameterName} must point to the expected element type.");
    }

    private static void AssertCallbackCdecl(Type callbackType, string callbackName)
    {
        UnmanagedFunctionPointerAttribute? callbackAttribute = callbackType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(callbackAttribute, $"{callbackName} must keep unmanaged callback metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, callbackAttribute!.CallingConvention, $"{callbackName} must use cdecl calling convention.");
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
