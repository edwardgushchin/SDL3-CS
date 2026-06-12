using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Events;

internal static class PInvokeTests
{
    private static IntPtr capturedEventsPointer;
    private static SDL3.SDL.Event[]? capturedEventsArray;
    private static int capturedNumEvents;
    private static SDL3.SDL.EventAction capturedAction;
    private static uint capturedMinType;
    private static uint capturedMaxType;
    private static uint capturedType;
    private static SDL3.SDL.Event capturedEvent;
    private static IntPtr capturedEventPointer;
    private static int capturedTimeoutMs;
    private static SDL3.SDL.EventFilter? capturedFilter;
    private static SDL3.SDL.EventFilter? nextFilter;
    private static IntPtr capturedUserdata;
    private static IntPtr nextUserdata;
    private static bool capturedEnabled;
    private static IntPtr capturedBuffer;
    private static int capturedBufferLength;
    private static int nextInt;
    private static uint nextUInt;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static SDL3.SDL.Event nextEvent;
    private static int capturedCallCount;

    public static void RunAll()
    {
        PumpEvents_CallsNativeHook();
        PeepEventsPointer_ForwardsArgumentsAndReturnsNativeValue();
        PeepEventsArray_ForwardsArgumentsAndReturnsNativeValue();
        HasEvent_ForwardsTypeAndReturnsNativeValue();
        HasEvents_ForwardsRangeAndReturnsNativeValue();
        FlushEvent_ForwardsType();
        FlushEvents_ForwardsRange();
        PollEventOut_ForwardsOutEventAndReturnsNativeValue();
        PollEventPointer_ForwardsPointerAndReturnsNativeValue();
        WaitEvent_ForwardsOutEventAndReturnsNativeValue();
        WaitEventTimeout_ForwardsTimeoutOutEventAndReturnsNativeValue();
        PushEvent_ForwardsEventByRefAndReturnsNativeValue();
        SetEventFilter_ForwardsFilterAndUserdata();
        GetEventFilter_ForwardsOutValuesAndReturnsNativeValue();
        AddEventWatch_ForwardsFilterUserdataAndReturnsNativeValue();
        RemoveEventWatch_ForwardsFilterAndUserdata();
        FilterEvents_ForwardsFilterAndUserdata();
        SetEventEnabled_ForwardsTypeAndEnabled();
        EventEnabled_ForwardsTypeAndReturnsNativeValue();
        RegisterEvents_ForwardsCountAndReturnsNativeValue();
        GetWindowFromEvent_ForwardsEventAndReturnsNativePointer();
        SDL_GetEventDescription_UsesExpectedNativeMetadata();
        GetEventDescriptionEvent_ForwardsEventBufferAndReturnsNativeValue();
        GetEventDescriptionPointer_ForwardsPointerBufferAndReturnsNativeValue();
    }

    public static void PumpEvents_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PumpEvents");
        AssertSdlImport(nativeMethod, "SDL_PumpEvents");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("PumpEventsNativeFunction", nameof(CaptureNoArgumentVoid));

        SDL3.SDL.PumpEvents();

        TestAssert.Equal(1, capturedCallCount, "SDL.PumpEvents must call native hook once.");
    }

    public static void PeepEventsPointer_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PeepEvents", typeof(IntPtr), typeof(int), typeof(SDL3.SDL.EventAction), typeof(uint), typeof(uint));
        AssertSdlImport(nativeMethod, "SDL_PeepEvents");

        ResetCaptureState();
        nextInt = 2;
        using NativeHookScope _ = NativeHookScope.Install("PeepEventsPointerNativeFunction", nameof(CapturePeepEventsPointer));

        int result = SDL3.SDL.PeepEvents((IntPtr)1001, 4, SDL3.SDL.EventAction.PeekEvent, 10, 20);

        TestAssert.Equal(2, result, "SDL.PeepEvents pointer overload must return native count.");
        TestAssert.Equal((IntPtr)1001, capturedEventsPointer, "SDL.PeepEvents pointer overload must forward events.");
        TestAssert.Equal(4, capturedNumEvents, "SDL.PeepEvents pointer overload must forward numevents.");
        TestAssert.Equal(SDL3.SDL.EventAction.PeekEvent, capturedAction, "SDL.PeepEvents pointer overload must forward action.");
        TestAssert.Equal<uint>(10, capturedMinType, "SDL.PeepEvents pointer overload must forward minType.");
        TestAssert.Equal<uint>(20, capturedMaxType, "SDL.PeepEvents pointer overload must forward maxType.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PeepEvents pointer overload must call native hook once.");
    }

    public static void PeepEventsArray_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PeepEvents", typeof(SDL3.SDL.Event[]), typeof(int), typeof(SDL3.SDL.EventAction), typeof(uint), typeof(uint));
        AssertSdlImport(nativeMethod, "SDL_PeepEvents");
        AssertOutParameter(nativeMethod, "events");

        ResetCaptureState();
        nextInt = 1;
        using NativeHookScope _ = NativeHookScope.Install("PeepEventsArrayNativeFunction", nameof(CapturePeepEventsArray));
        SDL3.SDL.Event[] events = [CreateEvent(31), CreateEvent(32)];

        int result = SDL3.SDL.PeepEvents(events, events.Length, SDL3.SDL.EventAction.GetEvent, 30, 40);

        TestAssert.Equal(1, result, "SDL.PeepEvents array overload must return native count.");
        TestAssert.True(ReferenceEquals(events, capturedEventsArray), "SDL.PeepEvents array overload must forward event array.");
        TestAssert.Equal(2, capturedNumEvents, "SDL.PeepEvents array overload must forward numevents.");
        TestAssert.Equal(SDL3.SDL.EventAction.GetEvent, capturedAction, "SDL.PeepEvents array overload must forward action.");
        TestAssert.Equal<uint>(30, capturedMinType, "SDL.PeepEvents array overload must forward minType.");
        TestAssert.Equal<uint>(40, capturedMaxType, "SDL.PeepEvents array overload must forward maxType.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PeepEvents array overload must call native hook once.");
    }

    public static void HasEvent_ForwardsTypeAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasEvent");
        AssertSdlImport(nativeMethod, "SDL_HasEvent");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("HasEventNativeFunction", nameof(CaptureTypeBool));

        bool result = SDL3.SDL.HasEvent(41);

        TestAssert.Equal(true, result, "SDL.HasEvent must return native bool value.");
        TestAssert.Equal<uint>(41, capturedType, "SDL.HasEvent must forward type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasEvent must call native hook once.");
    }

    public static void HasEvents_ForwardsRangeAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasEvents");
        AssertSdlImport(nativeMethod, "SDL_HasEvents");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("HasEventsNativeFunction", nameof(CaptureTypeRangeBool));

        bool result = SDL3.SDL.HasEvents(50, 60);

        TestAssert.Equal(true, result, "SDL.HasEvents must return native bool value.");
        TestAssert.Equal<uint>(50, capturedMinType, "SDL.HasEvents must forward minType.");
        TestAssert.Equal<uint>(60, capturedMaxType, "SDL.HasEvents must forward maxType.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasEvents must call native hook once.");
    }

    public static void FlushEvent_ForwardsType()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_FlushEvent");
        AssertSdlImport(nativeMethod, "SDL_FlushEvent");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("FlushEventNativeFunction", nameof(CaptureTypeVoid));

        SDL3.SDL.FlushEvent(71);

        TestAssert.Equal<uint>(71, capturedType, "SDL.FlushEvent must forward type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.FlushEvent must call native hook once.");
    }

    public static void FlushEvents_ForwardsRange()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_FlushEvents");
        AssertSdlImport(nativeMethod, "SDL_FlushEvents");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("FlushEventsNativeFunction", nameof(CaptureTypeRangeVoid));

        SDL3.SDL.FlushEvents(80, 90);

        TestAssert.Equal<uint>(80, capturedMinType, "SDL.FlushEvents must forward minType.");
        TestAssert.Equal<uint>(90, capturedMaxType, "SDL.FlushEvents must forward maxType.");
        TestAssert.Equal(1, capturedCallCount, "SDL.FlushEvents must call native hook once.");
    }

    public static void PollEventOut_ForwardsOutEventAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PollEvent", typeof(SDL3.SDL.Event).MakeByRefType());
        AssertSdlImport(nativeMethod, "SDL_PollEvent");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "event");

        ResetCaptureState();
        nextBool = true;
        nextEvent = CreateEvent(101);
        using NativeHookScope _ = NativeHookScope.Install("PollEventOutNativeFunction", nameof(CaptureOutEventBool));

        bool result = SDL3.SDL.PollEvent(out SDL3.SDL.Event @event);

        TestAssert.Equal(true, result, "SDL.PollEvent out overload must return native bool value.");
        TestAssert.Equal<uint>(101, @event.Type, "SDL.PollEvent out overload must forward native event out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PollEvent out overload must call native hook once.");
    }

    public static void PollEventPointer_ForwardsPointerAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PollEvent", typeof(IntPtr));
        AssertSdlImport(nativeMethod, "SDL_PollEvent");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("PollEventPointerNativeFunction", nameof(CaptureEventPointerBool));

        bool result = SDL3.SDL.PollEvent((IntPtr)1101);

        TestAssert.Equal(true, result, "SDL.PollEvent pointer overload must return native bool value.");
        TestAssert.Equal((IntPtr)1101, capturedEventPointer, "SDL.PollEvent pointer overload must forward event pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PollEvent pointer overload must call native hook once.");
    }

    public static void WaitEvent_ForwardsOutEventAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitEvent");
        AssertSdlImport(nativeMethod, "SDL_WaitEvent");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "event");

        ResetCaptureState();
        nextBool = true;
        nextEvent = CreateEvent(121);
        using NativeHookScope _ = NativeHookScope.Install("WaitEventNativeFunction", nameof(CaptureOutEventBool));

        bool result = SDL3.SDL.WaitEvent(out SDL3.SDL.Event @event);

        TestAssert.Equal(true, result, "SDL.WaitEvent must return native bool value.");
        TestAssert.Equal<uint>(121, @event.Type, "SDL.WaitEvent must forward native event out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitEvent must call native hook once.");
    }

    public static void WaitEventTimeout_ForwardsTimeoutOutEventAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WaitEventTimeout");
        AssertSdlImport(nativeMethod, "SDL_WaitEventTimeout");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "event");

        ResetCaptureState();
        nextBool = true;
        nextEvent = CreateEvent(131);
        using NativeHookScope _ = NativeHookScope.Install("WaitEventTimeoutNativeFunction", nameof(CaptureOutEventTimeoutBool));

        bool result = SDL3.SDL.WaitEventTimeout(out SDL3.SDL.Event @event, 250);

        TestAssert.Equal(true, result, "SDL.WaitEventTimeout must return native bool value.");
        TestAssert.Equal<uint>(131, @event.Type, "SDL.WaitEventTimeout must forward native event out value.");
        TestAssert.Equal(250, capturedTimeoutMs, "SDL.WaitEventTimeout must forward timeoutMs.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WaitEventTimeout must call native hook once.");
    }

    public static void PushEvent_ForwardsEventByRefAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PushEvent");
        AssertSdlImport(nativeMethod, "SDL_PushEvent");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "event");

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("PushEventNativeFunction", nameof(CaptureRefEventBool));
        SDL3.SDL.Event @event = CreateEvent(141);

        bool result = SDL3.SDL.PushEvent(ref @event);

        TestAssert.Equal(true, result, "SDL.PushEvent must return native bool value.");
        TestAssert.Equal<uint>(141, capturedEvent.Type, "SDL.PushEvent must forward event by ref.");
        TestAssert.Equal(1, capturedCallCount, "SDL.PushEvent must call native hook once.");
    }

    public static void SetEventFilter_ForwardsFilterAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetEventFilter");
        AssertSdlImport(nativeMethod, "SDL_SetEventFilter");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetEventFilterNativeFunction", nameof(CaptureFilterUserdataVoid));
        SDL3.SDL.EventFilter filter = AllowEvent;

        SDL3.SDL.SetEventFilter(filter, (IntPtr)1501);

        TestAssert.Equal(filter, capturedFilter, "SDL.SetEventFilter must forward filter.");
        TestAssert.Equal((IntPtr)1501, capturedUserdata, "SDL.SetEventFilter must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetEventFilter must call native hook once.");
    }

    public static void GetEventFilter_ForwardsOutValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetEventFilter");
        AssertSdlImport(nativeMethod, "SDL_GetEventFilter");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertByRefParameter(nativeMethod, "filter");
        AssertByRefParameter(nativeMethod, "userdata");

        ResetCaptureState();
        nextBool = true;
        nextFilter = AllowEvent;
        nextUserdata = (IntPtr)1601;
        using NativeHookScope _ = NativeHookScope.Install("GetEventFilterNativeFunction", nameof(CaptureGetEventFilter));

        bool result = SDL3.SDL.GetEventFilter(out SDL3.SDL.EventFilter filter, out IntPtr userdata);

        TestAssert.Equal(true, result, "SDL.GetEventFilter must return native bool value.");
        TestAssert.Equal(nextFilter, filter, "SDL.GetEventFilter must forward native filter out value.");
        TestAssert.Equal((IntPtr)1601, userdata, "SDL.GetEventFilter must forward native userdata out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetEventFilter must call native hook once.");
    }

    public static void AddEventWatch_ForwardsFilterUserdataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_AddEventWatch");
        AssertSdlImport(nativeMethod, "SDL_AddEventWatch");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("AddEventWatchNativeFunction", nameof(CaptureFilterUserdataBool));
        SDL3.SDL.EventFilter filter = AllowEvent;

        bool result = SDL3.SDL.AddEventWatch(filter, (IntPtr)1701);

        TestAssert.Equal(true, result, "SDL.AddEventWatch must return native bool value.");
        TestAssert.Equal(filter, capturedFilter, "SDL.AddEventWatch must forward filter.");
        TestAssert.Equal((IntPtr)1701, capturedUserdata, "SDL.AddEventWatch must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.AddEventWatch must call native hook once.");
    }

    public static void RemoveEventWatch_ForwardsFilterAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RemoveEventWatch");
        AssertSdlImport(nativeMethod, "SDL_RemoveEventWatch");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("RemoveEventWatchNativeFunction", nameof(CaptureFilterUserdataVoid));
        SDL3.SDL.EventFilter filter = AllowEvent;

        SDL3.SDL.RemoveEventWatch(filter, (IntPtr)1801);

        TestAssert.Equal(filter, capturedFilter, "SDL.RemoveEventWatch must forward filter.");
        TestAssert.Equal((IntPtr)1801, capturedUserdata, "SDL.RemoveEventWatch must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RemoveEventWatch must call native hook once.");
    }

    public static void FilterEvents_ForwardsFilterAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_FilterEvents");
        AssertSdlImport(nativeMethod, "SDL_FilterEvents");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("FilterEventsNativeFunction", nameof(CaptureFilterUserdataVoid));
        SDL3.SDL.EventFilter filter = AllowEvent;

        SDL3.SDL.FilterEvents(filter, (IntPtr)1901);

        TestAssert.Equal(filter, capturedFilter, "SDL.FilterEvents must forward filter.");
        TestAssert.Equal((IntPtr)1901, capturedUserdata, "SDL.FilterEvents must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.FilterEvents must call native hook once.");
    }

    public static void SetEventEnabled_ForwardsTypeAndEnabled()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetEventEnabled");
        AssertSdlImport(nativeMethod, "SDL_SetEventEnabled");
        AssertBoolParameterMarshal(nativeMethod, "enabled", UnmanagedType.I1);

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("SetEventEnabledNativeFunction", nameof(CaptureSetEventEnabled));

        SDL3.SDL.SetEventEnabled(2001, true);

        TestAssert.Equal<uint>(2001, capturedType, "SDL.SetEventEnabled must forward type.");
        TestAssert.Equal(true, capturedEnabled, "SDL.SetEventEnabled must forward enabled.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetEventEnabled must call native hook once.");
    }

    public static void EventEnabled_ForwardsTypeAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EventEnabled");
        AssertSdlImport(nativeMethod, "SDL_EventEnabled");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("EventEnabledNativeFunction", nameof(CaptureTypeBool));

        bool result = SDL3.SDL.EventEnabled(2101);

        TestAssert.Equal(true, result, "SDL.EventEnabled must return native bool value.");
        TestAssert.Equal<uint>(2101, capturedType, "SDL.EventEnabled must forward type.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EventEnabled must call native hook once.");
    }

    public static void RegisterEvents_ForwardsCountAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RegisterEvents");
        AssertSdlImport(nativeMethod, "SDL_RegisterEvents");

        ResetCaptureState();
        nextUInt = 2201;
        using NativeHookScope _ = NativeHookScope.Install("RegisterEventsNativeFunction", nameof(CaptureRegisterEvents));

        uint result = SDL3.SDL.RegisterEvents(5);

        TestAssert.Equal<uint>(2201, result, "SDL.RegisterEvents must return native event type base.");
        TestAssert.Equal(5, capturedNumEvents, "SDL.RegisterEvents must forward numevents.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RegisterEvents must call native hook once.");
    }

    public static void GetWindowFromEvent_ForwardsEventAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetWindowFromEvent");
        AssertSdlImport(nativeMethod, "SDL_GetWindowFromEvent");
        AssertByRefParameter(nativeMethod, "event");

        ResetCaptureState();
        nextPointer = (IntPtr)2301;
        using NativeHookScope _ = NativeHookScope.Install("GetWindowFromEventNativeFunction", nameof(CaptureGetWindowFromEvent));
        SDL3.SDL.Event @event = CreateEvent(231);

        IntPtr result = SDL3.SDL.GetWindowFromEvent(in @event);

        TestAssert.Equal((IntPtr)2301, result, "SDL.GetWindowFromEvent must return native window pointer.");
        TestAssert.Equal<uint>(231, capturedEvent.Type, "SDL.GetWindowFromEvent must forward event.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetWindowFromEvent must call native hook once.");
    }

    public static void SDL_GetEventDescription_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetEventDescription");
        AssertSdlImport(nativeMethod, "SDL_GetEventDescription");
        AssertByRefParameter(nativeMethod, "event");
    }

    public static void GetEventDescriptionEvent_ForwardsEventBufferAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 12;
        using NativeHookScope _ = NativeHookScope.Install("GetEventDescriptionNativeFunction", nameof(CaptureGetEventDescription));
        SDL3.SDL.Event @event = CreateEvent(241);
        byte[] buffer = new byte[32];

        int result = SDL3.SDL.GetEventDescription(in @event, buffer, buffer.Length);

        TestAssert.Equal(12, result, "SDL.GetEventDescription(Event) must return native byte count.");
        TestAssert.True(capturedEventPointer != IntPtr.Zero, "SDL.GetEventDescription(Event) must allocate and forward an event pointer.");
        TestAssert.Equal<uint>(241, capturedEvent.Type, "SDL.GetEventDescription(Event) must marshal event into native pointer.");
        TestAssert.True(capturedBuffer != IntPtr.Zero, "SDL.GetEventDescription(Event) must pin non-null buffer.");
        TestAssert.Equal(32, capturedBufferLength, "SDL.GetEventDescription(Event) must forward buflen.");

        result = SDL3.SDL.GetEventDescription(in @event, null, 0);

        TestAssert.Equal(12, result, "SDL.GetEventDescription(Event) must return native byte count for null buffer.");
        TestAssert.Equal(IntPtr.Zero, capturedBuffer, "SDL.GetEventDescription(Event) must forward null buffer as zero pointer.");
        TestAssert.Equal(0, capturedBufferLength, "SDL.GetEventDescription(Event) must forward zero buflen.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetEventDescription(Event) must call native hook for both buffer branches.");
    }

    public static void GetEventDescriptionPointer_ForwardsPointerBufferAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 14;
        using NativeHookScope _ = NativeHookScope.Install("GetEventDescriptionNativeFunction", nameof(CaptureGetEventDescription));
        IntPtr eventPtr = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.Event>());
        Marshal.StructureToPtr(CreateEvent(251), eventPtr, false);
        byte[] buffer = new byte[16];

        try
        {
            int result = SDL3.SDL.GetEventDescription(in eventPtr, buffer, buffer.Length);

            TestAssert.Equal(14, result, "SDL.GetEventDescription(IntPtr) must return native byte count.");
            TestAssert.Equal(eventPtr, capturedEventPointer, "SDL.GetEventDescription(IntPtr) must forward event pointer.");
            TestAssert.Equal<uint>(251, capturedEvent.Type, "SDL.GetEventDescription(IntPtr) must pass event pointer content to native hook.");
            TestAssert.True(capturedBuffer != IntPtr.Zero, "SDL.GetEventDescription(IntPtr) must pin non-null buffer.");
            TestAssert.Equal(16, capturedBufferLength, "SDL.GetEventDescription(IntPtr) must forward buflen.");

            IntPtr zeroEvent = IntPtr.Zero;
            result = SDL3.SDL.GetEventDescription(in zeroEvent, null, 0);

            TestAssert.Equal(14, result, "SDL.GetEventDescription(IntPtr) must return native byte count for null pointers.");
            TestAssert.Equal(IntPtr.Zero, capturedEventPointer, "SDL.GetEventDescription(IntPtr) must forward zero event pointer.");
            TestAssert.Equal(IntPtr.Zero, capturedBuffer, "SDL.GetEventDescription(IntPtr) must forward null buffer as zero pointer.");
            TestAssert.Equal(0, capturedBufferLength, "SDL.GetEventDescription(IntPtr) must forward zero buflen.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetEventDescription(IntPtr) must call native hook for both buffer branches.");
        }
        finally
        {
            Marshal.FreeHGlobal(eventPtr);
        }
    }

    private static void ResetCaptureState()
    {
        capturedEventsPointer = IntPtr.Zero;
        capturedEventsArray = null;
        capturedNumEvents = 0;
        capturedAction = default;
        capturedMinType = 0;
        capturedMaxType = 0;
        capturedType = 0;
        capturedEvent = default;
        capturedEventPointer = IntPtr.Zero;
        capturedTimeoutMs = 0;
        capturedFilter = null;
        nextFilter = null;
        capturedUserdata = IntPtr.Zero;
        nextUserdata = IntPtr.Zero;
        capturedEnabled = false;
        capturedBuffer = IntPtr.Zero;
        capturedBufferLength = 0;
        nextInt = 0;
        nextUInt = 0;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextEvent = default;
        capturedCallCount = 0;
    }

    private static void CaptureNoArgumentVoid()
    {
        capturedCallCount++;
    }

    private static int CapturePeepEventsPointer(IntPtr events, int numevents, SDL3.SDL.EventAction action, uint minType, uint maxType)
    {
        capturedEventsPointer = events;
        capturedNumEvents = numevents;
        capturedAction = action;
        capturedMinType = minType;
        capturedMaxType = maxType;
        capturedCallCount++;
        return nextInt;
    }

    private static int CapturePeepEventsArray(SDL3.SDL.Event[] events, int numevents, SDL3.SDL.EventAction action, uint minType, uint maxType)
    {
        capturedEventsArray = events;
        capturedNumEvents = numevents;
        capturedAction = action;
        capturedMinType = minType;
        capturedMaxType = maxType;
        capturedCallCount++;
        return nextInt;
    }

    private static bool CaptureTypeBool(uint type)
    {
        capturedType = type;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTypeRangeBool(uint minType, uint maxType)
    {
        capturedMinType = minType;
        capturedMaxType = maxType;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureTypeVoid(uint type)
    {
        capturedType = type;
        capturedCallCount++;
    }

    private static void CaptureTypeRangeVoid(uint minType, uint maxType)
    {
        capturedMinType = minType;
        capturedMaxType = maxType;
        capturedCallCount++;
    }

    private static bool CaptureOutEventBool(out SDL3.SDL.Event @event)
    {
        @event = nextEvent;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureEventPointerBool(IntPtr @event)
    {
        capturedEventPointer = @event;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureOutEventTimeoutBool(out SDL3.SDL.Event @event, int timeoutMs)
    {
        @event = nextEvent;
        capturedTimeoutMs = timeoutMs;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureRefEventBool(ref SDL3.SDL.Event @event)
    {
        capturedEvent = @event;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureFilterUserdataVoid(SDL3.SDL.EventFilter filter, IntPtr userdata)
    {
        capturedFilter = filter;
        capturedUserdata = userdata;
        capturedCallCount++;
    }

    private static bool CaptureFilterUserdataBool(SDL3.SDL.EventFilter filter, IntPtr userdata)
    {
        capturedFilter = filter;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetEventFilter(out SDL3.SDL.EventFilter filter, out IntPtr userdata)
    {
        filter = nextFilter ?? AllowEvent;
        userdata = nextUserdata;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureSetEventEnabled(uint type, bool enabled)
    {
        capturedType = type;
        capturedEnabled = enabled;
        capturedCallCount++;
    }

    private static uint CaptureRegisterEvents(int numevents)
    {
        capturedNumEvents = numevents;
        capturedCallCount++;
        return nextUInt;
    }

    private static IntPtr CaptureGetWindowFromEvent(in SDL3.SDL.Event @event)
    {
        capturedEvent = @event;
        capturedCallCount++;
        return nextPointer;
    }

    private static int CaptureGetEventDescription(in IntPtr @event, IntPtr buf, int buflen)
    {
        capturedEventPointer = @event;
        capturedBuffer = buf;
        capturedBufferLength = buflen;
        capturedEvent = @event == IntPtr.Zero ? default : Marshal.PtrToStructure<SDL3.SDL.Event>(@event);
        capturedCallCount++;
        return nextInt;
    }

    private static bool AllowEvent(IntPtr userdata, ref SDL3.SDL.Event @event)
    {
        return true;
    }

    private static SDL3.SDL.Event CreateEvent(uint type)
    {
        return new SDL3.SDL.Event { Type = type };
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static MethodInfo GetNativeMethod(string methodName, params Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} overload must be private static.");
        return method!;
    }

    private static void AssertSdlImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.True(libraryImport is not null || dllImport is not null, $"SDL.{method.Name} must keep native import metadata.");

        if (libraryImport is not null)
        {
            TestAssert.Equal("SDL3", libraryImport.LibraryName, $"SDL.{method.Name} must import from SDL3.");
            TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        }
        else
        {
            TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
            TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        }

        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertCdecl(MethodInfo method, string apiName)
    {
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"{apiName} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"{apiName} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"{apiName} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method, UnmanagedType unmanagedType)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} return value must use expected bool marshalling.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected bool marshalling.");
    }

    private static void AssertByRefParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"SDL.{method.Name} parameter {parameterName} must stay by reference.");
    }

    private static void AssertOutParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        OutAttribute? outAttribute = parameter.GetCustomAttribute<OutAttribute>();
        TestAssert.NotNull(outAttribute, $"SDL.{method.Name} parameter {parameterName} must keep Out metadata.");
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
