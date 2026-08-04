using System.Reflection;

namespace SDL3.Tests.SDL.Basics.Main;

internal static partial class PInvokeTests
{
    private static IntPtr managedAppstate;

    public static void RunManagedMainCallbackFocusedTests()
    {
        RunMainCallbacks_UsesManagedArgumentsAndForwardsLifecycle();
        RunMainCallbacks_NormalizesNullArgumentsAndSupportsEarlySuccess();
        RunMainCallbacks_RejectsContinueWithoutState();
        RunMainCallbacks_AllowsEarlyTerminationWithoutState();
        RunMainCallbacks_ContainsAndRethrowsManagedCallbackExceptions();
        RunMainCallbacks_PerformsFallbackCleanupAndIgnoresDuplicateQuit();
        RunMainCallbacks_CleansUpBeforeRethrowingNativeFailure();
        RunMainCallbacks_WaitsForActiveEventBeforeQuit();
        RunMainCallbacks_WaitsForConcurrentDuplicateQuit();
        CreateNativeMainArguments_UsesProcessFriendlyNameAndFallback();
        ManagedMainCallbacks_PublicContractHasExpectedShape();
    }

    public static void RunMainCallbacks_UsesManagedArgumentsAndForwardsLifecycle()
    {
        RecordingApp.Reset();
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedLifecycle));

        int result = SDL3.SDL.RunMainCallbacks<RecordingApp>(["--renderer", "software"]);

        TestAssert.Equal(73, result, "SDL.RunMainCallbacks must return the native main result.");
        TestAssert.Equal(2, RecordingApp.Arguments.Length, "Managed AppInit must receive only C# Main arguments.");
        TestAssert.Equal("--renderer", RecordingApp.Arguments[0], "Managed AppInit must preserve the first argument.");
        TestAssert.Equal("software", RecordingApp.Arguments[1], "Managed AppInit must preserve the second argument.");
        TestAssert.Equal(1, RecordingApp.IterateCount, "Managed AppIterate must run once.");
        TestAssert.Equal(1, RecordingApp.EventCount, "Managed AppEvent must run once.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.Quit, RecordingApp.EventType, "Managed AppEvent must receive the native event by reference.");
        TestAssert.Equal(1, RecordingApp.QuitCount, "Managed AppQuit must run once.");
        TestAssert.Equal(SDL3.SDL.AppResult.Success, RecordingApp.QuitResult, "Managed AppQuit must receive the native termination result.");
    }

    public static void RunMainCallbacks_NormalizesNullArgumentsAndSupportsEarlySuccess()
    {
        RecordingApp.Reset();
        RecordingApp.InitResult = SDL3.SDL.AppResult.Success;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedEarlyExit));

        int result = SDL3.SDL.RunMainCallbacks<RecordingApp>(null);

        TestAssert.Equal(17, result, "SDL.RunMainCallbacks must preserve an early native result.");
        TestAssert.Equal(0, RecordingApp.Arguments.Length, "Managed AppInit must normalize null arguments to an empty array.");
        TestAssert.Equal(0, RecordingApp.IterateCount, "Early success must not invoke AppIterate.");
        TestAssert.Equal(1, RecordingApp.QuitCount, "Early success must still invoke AppQuit once.");
        TestAssert.Equal(SDL3.SDL.AppResult.Success, RecordingApp.QuitResult, "Early success must reach AppQuit.");
    }

    public static void RunMainCallbacks_RejectsContinueWithoutState()
    {
        RecordingApp.Reset();
        RecordingApp.ReturnNullState = true;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedInitFailure));

        InvalidOperationException exception = CaptureException<InvalidOperationException>(
            () => SDL3.SDL.RunMainCallbacks<RecordingApp>([]),
            "Continue without managed state must fail after returning from the native boundary.");

        TestAssert.True(exception.Message.Contains("state", StringComparison.OrdinalIgnoreCase), "The null-state error must explain the invalid state contract.");
        TestAssert.Equal(0, RecordingApp.QuitCount, "AppQuit cannot run when AppInit did not create state.");
    }

    public static void RunMainCallbacks_AllowsEarlyTerminationWithoutState()
    {
        RecordingApp.Reset();
        RecordingApp.ReturnNullState = true;
        RecordingApp.InitResult = SDL3.SDL.AppResult.Success;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedEarlyExit));

        int result = SDL3.SDL.RunMainCallbacks<RecordingApp>([]);

        TestAssert.Equal(17, result, "Early termination without state must preserve the native result.");
        TestAssert.Equal(0, RecordingApp.QuitCount, "AppQuit cannot run when early termination did not create state.");
    }

    public static void RunMainCallbacks_ContainsAndRethrowsManagedCallbackExceptions()
    {
        foreach (CallbackStage stage in Enum.GetValues<CallbackStage>())
        {
            RecordingApp.Reset();
            RecordingApp.ThrowAt = stage;
            using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedFailure));

            ApplicationException exception = CaptureException<ApplicationException>(
                () => SDL3.SDL.RunMainCallbacks<RecordingApp>([stage.ToString()]),
                $"An exception from {stage} must be rethrown after the native boundary.");

            TestAssert.Equal(stage.ToString(), exception.Message, $"The original {stage} exception must be preserved.");
            TestAssert.True(RecordingApp.NativeBoundaryReturned, $"The {stage} exception must not escape through the native callback boundary.");
        }
    }

    public static void RunMainCallbacks_PerformsFallbackCleanupAndIgnoresDuplicateQuit()
    {
        RecordingApp.Reset();
        using (NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedWithoutQuit)))
        {
            SDL3.SDL.RunMainCallbacks<RecordingApp>([]);
        }

        TestAssert.Equal(1, RecordingApp.QuitCount, "Runner fallback must invoke AppQuit when the native path omits it.");
        TestAssert.Equal(SDL3.SDL.AppResult.Failure, RecordingApp.QuitResult, "Fallback cleanup must report an incomplete native lifecycle as failure.");

        RecordingApp.Reset();
        using (NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedDuplicateQuit)))
        {
            SDL3.SDL.RunMainCallbacks<RecordingApp>([]);
        }

        TestAssert.Equal(1, RecordingApp.QuitCount, "Duplicate native AppQuit calls must reach managed state only once.");
    }

    public static void RunMainCallbacks_CleansUpBeforeRethrowingNativeFailure()
    {
        RecordingApp.Reset();
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedNativeFailure));

        NotSupportedException exception = CaptureException<NotSupportedException>(
            () => SDL3.SDL.RunMainCallbacks<RecordingApp>([]),
            "A native bridge failure must be rethrown after managed cleanup.");

        TestAssert.Equal("native bridge", exception.Message, "Runner must preserve the native bridge exception.");
        TestAssert.Equal(1, RecordingApp.QuitCount, "Native bridge failure must trigger fallback AppQuit.");
        TestAssert.Equal(SDL3.SDL.AppResult.Failure, RecordingApp.QuitResult, "Native bridge failure cleanup must use AppResult.Failure.");
    }

    public static void RunMainCallbacks_WaitsForActiveEventBeforeQuit()
    {
        RecordingApp.Reset();
        RecordingApp.BlockEvent = true;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedConcurrentQuit));

        SDL3.SDL.RunMainCallbacks<RecordingApp>([]);

        TestAssert.Equal(1, RecordingApp.EventCount, "Concurrent event must complete.");
        TestAssert.Equal(1, RecordingApp.QuitCount, "Quit must run after the active event.");
        TestAssert.True(RecordingApp.EventCompletedBeforeQuit, "AppQuit must wait for an active AppEvent before releasing state.");
    }

    public static void RunMainCallbacks_WaitsForConcurrentDuplicateQuit()
    {
        RecordingApp.Reset();
        RecordingApp.BlockQuit = true;
        using NativeHookScope _ = NativeHookScope.Install("EnterAppMainCallbacksNativeFunction", nameof(RunManagedConcurrentDuplicateQuit));

        SDL3.SDL.RunMainCallbacks<RecordingApp>([]);

        TestAssert.Equal(1, RecordingApp.QuitCount, "Concurrent duplicate AppQuit calls must reach managed state only once.");
        TestAssert.Equal(SDL3.SDL.AppResult.Success, RecordingApp.QuitResult, "The first concurrent AppQuit result must remain terminal.");
    }

    public static void CreateNativeMainArguments_UsesProcessFriendlyNameAndFallback()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(
            "CreateNativeMainArguments",
            BindingFlags.NonPublic | BindingFlags.Static,
            null,
            [typeof(string[]), typeof(string), typeof(string)],
            null);
        TestAssert.NotNull(method, "SDL.CreateNativeMainArguments must expose a pure overload for deterministic fallback verification.");

        AssertNativeArgumentFallback(method!, "C:/apps/game.exe", "friendly.exe", "C:/apps/game.exe");
        AssertNativeArgumentFallback(method!, " ", "friendly.exe", "friendly.exe");
        AssertNativeArgumentFallback(method!, null, "\t", "SDL3-CS");
    }

    public static void ManagedMainCallbacks_PublicContractHasExpectedShape()
    {
        Type contract = typeof(SDL3.SDL.IMainCallbacks<RecordingApp>);
        TestAssert.True(contract.IsInterface, "SDL.IMainCallbacks<TSelf> must be an interface.");
        TestAssert.NotNull(contract.GetMethod("AppInit"), "Managed callback contract must expose AppInit.");
        TestAssert.NotNull(contract.GetMethod("AppIterate"), "Managed callback contract must expose AppIterate.");
        TestAssert.NotNull(contract.GetMethod("AppEvent"), "Managed callback contract must expose AppEvent.");
        TestAssert.NotNull(contract.GetMethod("AppQuit"), "Managed callback contract must expose AppQuit.");

        AttributeUsageAttribute? usage = typeof(SDL3.SDL.GenerateMainAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().SingleOrDefault();
        TestAssert.NotNull(usage, "SDL.GenerateMainAttribute must declare AttributeUsage.");
        TestAssert.Equal(AttributeTargets.Class, usage!.ValidOn, "SDL.GenerateMainAttribute must target classes.");
        TestAssert.Equal(false, usage.AllowMultiple, "SDL.GenerateMainAttribute must not allow duplicates.");
        TestAssert.Equal(false, usage.Inherited, "SDL.GenerateMainAttribute must not be inherited.");
    }

    private static int RunManagedLifecycle(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        AssertNativeArguments(argc, argv, "--renderer", "software");
        managedAppstate = IntPtr.Zero;
        TestAssert.Equal(SDL3.SDL.AppResult.Continue, appinit(ref managedAppstate, argc, argv), "Managed AppInit must continue.");
        TestAssert.True(managedAppstate != IntPtr.Zero, "Managed AppInit must provide an opaque native state handle.");
        TestAssert.Equal(SDL3.SDL.AppResult.Continue, appiter(managedAppstate), "Managed AppIterate must continue.");
        SDL3.SDL.Event @event = new() { Type = (uint)SDL3.SDL.EventType.Quit };
        TestAssert.Equal(SDL3.SDL.AppResult.Continue, appevent(managedAppstate, ref @event), "Managed AppEvent must continue.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.Terminating, @event.Type, "Managed AppEvent must preserve ref mutations.");
        appquit(managedAppstate, SDL3.SDL.AppResult.Success);
        TestAssert.Equal(SDL3.SDL.AppResult.Success, appiter(managedAppstate), "Callbacks after quit begins must not access released state.");
        SDL3.SDL.Event eventAfterQuit = new() { Type = (uint)SDL3.SDL.EventType.Quit };
        TestAssert.Equal(SDL3.SDL.AppResult.Success, appevent(managedAppstate, ref eventAfterQuit), "Events after quit begins must not access released state.");
        TestAssert.Equal((uint)SDL3.SDL.EventType.Quit, eventAfterQuit.Type, "Events after quit must remain untouched.");
        return 73;
    }

    private static int RunManagedEarlyExit(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        AssertNativeArguments(argc, argv);
        managedAppstate = IntPtr.Zero;
        SDL3.SDL.AppResult result = appinit(ref managedAppstate, argc, argv);
        TestAssert.Equal(SDL3.SDL.AppResult.Success, result, "Managed AppInit must return configured early success.");
        appquit(managedAppstate, result);
        return 17;
    }

    private static int RunManagedInitFailure(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        SDL3.SDL.AppResult result = appinit(ref managedAppstate, argc, argv);
        TestAssert.Equal(SDL3.SDL.AppResult.Failure, result, "Invalid managed state must become native failure.");
        appquit(managedAppstate, result);
        return -1;
    }

    private static int RunManagedFailure(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        SDL3.SDL.AppResult result = appinit(ref managedAppstate, argc, argv);
        if (result == SDL3.SDL.AppResult.Continue)
        {
            result = appiter(managedAppstate);
        }

        if (result == SDL3.SDL.AppResult.Continue)
        {
            SDL3.SDL.Event @event = new() { Type = (uint)SDL3.SDL.EventType.Quit };
            result = appevent(managedAppstate, ref @event);
        }

        appquit(managedAppstate, result);
        RecordingApp.NativeBoundaryReturned = true;
        return -1;
    }

    private static int RunManagedWithoutQuit(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        TestAssert.Equal(SDL3.SDL.AppResult.Continue, appinit(ref managedAppstate, argc, argv), "Managed AppInit must continue before fallback cleanup.");
        return 0;
    }

    private static int RunManagedDuplicateQuit(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        appinit(ref managedAppstate, argc, argv);
        appquit(managedAppstate, SDL3.SDL.AppResult.Success);
        appquit(managedAppstate, SDL3.SDL.AppResult.Failure);
        return 0;
    }

    private static int RunManagedNativeFailure(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        appinit(ref managedAppstate, argc, argv);
        throw new NotSupportedException("native bridge");
    }

    private static int RunManagedConcurrentQuit(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        appinit(ref managedAppstate, argc, argv);
        SDL3.SDL.Event @event = new() { Type = (uint)SDL3.SDL.EventType.Quit };
        Task<SDL3.SDL.AppResult> eventTask = Task.Run(() => appevent(managedAppstate, ref @event));
        TestAssert.True(RecordingApp.EventEntered.Wait(TimeSpan.FromSeconds(5)), "Managed AppEvent did not start in time.");
        Task quitTask = Task.Run(() => appquit(managedAppstate, SDL3.SDL.AppResult.Success));
        TestAssert.Equal(false, RecordingApp.QuitEntered.Wait(TimeSpan.FromMilliseconds(100)), "AppQuit must not enter user code while AppEvent is active.");
        RecordingApp.ReleaseEvent.Set();
        TestAssert.True(Task.WaitAll([eventTask, quitTask], TimeSpan.FromSeconds(5)), "Concurrent event and quit did not complete in time.");
        TestAssert.Equal(SDL3.SDL.AppResult.Success, appiter(managedAppstate), "The quit result must remain terminal when an active event returns later.");
        return 0;
    }

    private static int RunManagedConcurrentDuplicateQuit(int argc, string[]? argv, SDL3.SDL.AppInitFunc appinit, SDL3.SDL.AppIterateFunc appiter, SDL3.SDL.AppEventFunc appevent, SDL3.SDL.AppQuitFunc appquit)
    {
        managedAppstate = IntPtr.Zero;
        appinit(ref managedAppstate, argc, argv);
        Task firstQuit = Task.Run(() => appquit(managedAppstate, SDL3.SDL.AppResult.Success));
        TestAssert.True(RecordingApp.QuitEntered.Wait(TimeSpan.FromSeconds(5)), "The first managed AppQuit did not start in time.");
        Task duplicateQuit = Task.Run(() => appquit(managedAppstate, SDL3.SDL.AppResult.Failure));
        TestAssert.Equal(false, duplicateQuit.Wait(TimeSpan.FromMilliseconds(100)), "A duplicate AppQuit must wait while the first AppQuit is still active.");
        RecordingApp.ReleaseQuit.Set();
        TestAssert.True(Task.WaitAll([firstQuit, duplicateQuit], TimeSpan.FromSeconds(5)), "Concurrent AppQuit calls did not complete in time.");
        return 0;
    }

    private static void AssertNativeArgumentFallback(MethodInfo method, string? processPath, string? friendlyName, string expectedExecutable)
    {
        string[] arguments = (string[])method.Invoke(null, [new[] { "--test" }, processPath, friendlyName])!;
        TestAssert.Equal(2, arguments.Length, "Native arguments must prepend exactly one executable identifier.");
        TestAssert.Equal(expectedExecutable, arguments[0], "Native arguments must use the expected executable fallback.");
        TestAssert.Equal("--test", arguments[1], "Native arguments must preserve managed arguments.");
    }

    private static void AssertNativeArguments(int argc, string[]? argv, params string[] expectedArgs)
    {
        TestAssert.NotNull(argv, "Native argv must contain the executable path.");
        TestAssert.Equal(expectedArgs.Length + 1, argc, "Native argc must include the executable path.");
        TestAssert.Equal(argc, argv!.Length, "Native argc must match argv length.");
        TestAssert.True(!string.IsNullOrWhiteSpace(argv[0]), "Native argv[0] must identify the executable.");
        for (int index = 0; index < expectedArgs.Length; index++)
        {
            TestAssert.Equal(expectedArgs[index], argv[index + 1], $"Native argv must preserve managed argument {index}.");
        }
    }

    private static TException CaptureException<TException>(Action action, string message)
        where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException exception)
        {
            return exception;
        }

        throw new InvalidOperationException(message);
    }

    private enum CallbackStage
    {
        Init,
        Iterate,
        Event,
        Quit
    }

    private sealed class RecordingApp : SDL3.SDL.IMainCallbacks<RecordingApp>
    {
        public static string[] Arguments { get; private set; } = [];
        public static SDL3.SDL.AppResult InitResult { get; set; } = SDL3.SDL.AppResult.Continue;
        public static bool ReturnNullState { get; set; }
        public static CallbackStage? ThrowAt { get; set; }
        public static bool NativeBoundaryReturned { get; set; }
        public static int IterateCount { get; private set; }
        public static int EventCount { get; private set; }
        public static uint EventType { get; private set; }
        public static int QuitCount { get; private set; }
        public static SDL3.SDL.AppResult QuitResult { get; private set; }
        public static bool BlockEvent { get; set; }
        public static bool BlockQuit { get; set; }
        public static bool EventCompletedBeforeQuit { get; private set; }
        public static ManualResetEventSlim EventEntered { get; } = new(false);
        public static ManualResetEventSlim ReleaseEvent { get; } = new(false);
        public static ManualResetEventSlim QuitEntered { get; } = new(false);
        public static ManualResetEventSlim ReleaseQuit { get; } = new(false);

        public static SDL3.SDL.AppResult AppInit(out RecordingApp? appState, string[] args)
        {
            appState = null;
            Arguments = args;
            ThrowIfConfigured(CallbackStage.Init);
            if (!ReturnNullState)
            {
                appState = new RecordingApp();
            }

            return InitResult;
        }

        public SDL3.SDL.AppResult AppIterate()
        {
            IterateCount++;
            ThrowIfConfigured(CallbackStage.Iterate);
            return SDL3.SDL.AppResult.Continue;
        }

        public SDL3.SDL.AppResult AppEvent(ref SDL3.SDL.Event @event)
        {
            EventCount++;
            EventType = @event.Type;
            ThrowIfConfigured(CallbackStage.Event);
            if (BlockEvent)
            {
                EventEntered.Set();
                TestAssert.True(ReleaseEvent.Wait(TimeSpan.FromSeconds(5)), "Managed AppEvent was not released in time.");
            }

            @event.Type = (uint)SDL3.SDL.EventType.Terminating;
            EventCompletedBeforeQuit = true;
            return SDL3.SDL.AppResult.Continue;
        }

        public void AppQuit(SDL3.SDL.AppResult result)
        {
            QuitEntered.Set();
            QuitCount++;
            QuitResult = result;
            TestAssert.True(!BlockEvent || EventCompletedBeforeQuit, "AppQuit entered before AppEvent completed.");
            if (BlockQuit)
            {
                TestAssert.True(ReleaseQuit.Wait(TimeSpan.FromSeconds(5)), "Managed AppQuit was not released in time.");
            }

            ThrowIfConfigured(CallbackStage.Quit);
        }

        public static void Reset()
        {
            Arguments = [];
            InitResult = SDL3.SDL.AppResult.Continue;
            ReturnNullState = false;
            ThrowAt = null;
            NativeBoundaryReturned = false;
            IterateCount = 0;
            EventCount = 0;
            EventType = 0;
            QuitCount = 0;
            QuitResult = default;
            BlockEvent = false;
            BlockQuit = false;
            EventCompletedBeforeQuit = false;
            EventEntered.Reset();
            ReleaseEvent.Reset();
            QuitEntered.Reset();
            ReleaseQuit.Reset();
        }

        private static void ThrowIfConfigured(CallbackStage stage)
        {
            if (ThrowAt == stage)
            {
                throw new ApplicationException(stage.ToString());
            }
        }
    }
}
