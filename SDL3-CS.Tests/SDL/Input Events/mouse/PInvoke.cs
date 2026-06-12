using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Mouse;

internal static class PInvokeTests
{
    private delegate SDL3.SDL.MouseButtonFlags MouseStateCaller(out float x, out float y);

    private static uint capturedInstanceId;
    private static IntPtr capturedWindow;
    private static IntPtr capturedCursor;
    private static IntPtr capturedSurface;
    private static IntPtr capturedUserdata;
    private static IntPtr capturedFreeMemory;
    private static SDL3.SDL.MouseMotionTransformCallback? capturedTransformCallback;
    private static byte[]? capturedData;
    private static byte[]? capturedMask;
    private static SDL3.SDL.CursorFrameInfo[]? capturedFrames;
    private static SDL3.SDL.SystemCursor capturedSystemCursor;
    private static bool capturedEnabled;
    private static float capturedX;
    private static float capturedY;
    private static int capturedW;
    private static int capturedH;
    private static int capturedHotX;
    private static int capturedHotY;
    private static int capturedFrameCount;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static int nextCount;
    private static float nextX;
    private static float nextY;
    private static SDL3.SDL.MouseButtonFlags nextMouseButtonFlags;

    public static void RunAll()
    {
        HasMouse_ReturnsNativeValue();
        SDL_GetMice_UsesExpectedNativeMetadata();
        GetMice_ReturnsArrayNullAndFreesNativePointer();
        SDL_GetMouseNameForID_UsesExpectedNativeMetadata();
        GetMouseNameForID_ReturnsStringAndNull();
        GetMouseFocus_ReturnsNativePointer();
        GetMouseState_ReturnsCoordinatesAndButtonFlags();
        GetGlobalMouseState_ReturnsCoordinatesAndButtonFlags();
        GetRelativeMouseState_ReturnsCoordinatesAndButtonFlags();
        WarpMouseInWindow_ForwardsWindowAndCoordinates();
        WarpMouseGlobal_ForwardsCoordinatesAndReturnsNativeValue();
        SetRelativeMouseTransform_ForwardsCallbackUserdataAndReturnsNativeValue();
        SetWindowRelativeMouseMode_ForwardsWindowEnabledAndReturnsNativeValue();
        GetWindowRelativeMouseMode_ForwardsWindowAndReturnsNativeValue();
        CaptureMouse_ForwardsEnabledAndReturnsNativeValue();
        CreateCursor_ForwardsDataMaskDimensionsAndReturnsNativePointer();
        CreateColorCursor_ForwardsSurfaceHotspotAndReturnsNativePointer();
        CreateAnimatedCursor_ForwardsFramesCountHotspotAndReturnsNativePointer();
        CreateSystemCursor_ForwardsIdAndReturnsNativePointer();
        SetCursor_ForwardsCursorAndReturnsNativeValue();
        GetCursor_ReturnsNativePointer();
        GetDefaultCursor_ReturnsNativePointer();
        DestroyCursor_ForwardsCursor();
        ShowCursor_ReturnsNativeValue();
        HideCursor_ReturnsNativeValue();
        CursorVisible_ReturnsNativeValue();
    }

    public static void HasMouse_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasMouse");
        AssertSdlImport(nativeMethod, "SDL_HasMouse");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasMouseNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.HasMouse();

        TestAssert.Equal(true, result, "SDL.HasMouse must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasMouse must call the native hook once.");
    }

    public static void SDL_GetMice_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetMice");
        AssertSdlImport(nativeMethod, "SDL_GetMice");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetMice_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr array = CreateNativeUIntArray(11u, 22u);

        try
        {
            nextPointer = array;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetMiceNativeFunction", nameof(CaptureArrayPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            uint[]? mice = SDL3.SDL.GetMice(out int count);

            TestAssert.NotNull(mice, "SDL.GetMice must convert native mouse IDs.");
            TestAssert.Equal(2, mice!.Length, "SDL.GetMice must preserve native count.");
            TestAssert.Equal(11u, mice[0], "SDL.GetMice must convert mouse ID 0.");
            TestAssert.Equal(22u, mice[1], "SDL.GetMice must convert mouse ID 1.");
            TestAssert.Equal(2, count, "SDL.GetMice must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetMice must free the native array pointer.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            mice = SDL3.SDL.GetMice(out count);

            TestAssert.Equal<uint[]?>(null, mice, "SDL.GetMice must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetMice must return native count for native null.");
            TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.GetMice must pass native null to SDL.Free.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetMice must call the native hook for both branches.");
            TestAssert.Equal(2, capturedFreeCallCount, "SDL.GetMice must call SDL.Free for both branches.");
        }
        finally
        {
            Marshal.FreeHGlobal(array);
        }
    }

    public static void SDL_GetMouseNameForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetMouseNameForID");
        AssertSdlImport(nativeMethod, "SDL_GetMouseNameForID");
    }

    public static void GetMouseNameForID_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("mouse-name");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetMouseNameForIDNativeFunction", nameof(CaptureInstancePointer));
            string? result = SDL3.SDL.GetMouseNameForID(33u);

            TestAssert.Equal("mouse-name", result, "SDL.GetMouseNameForID must convert native UTF-8 strings.");
            TestAssert.Equal(33u, capturedInstanceId, "SDL.GetMouseNameForID must forward instanceId for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetMouseNameForID(44u);

            TestAssert.Equal<string?>(null, result, "SDL.GetMouseNameForID must return null for native null.");
            TestAssert.Equal(44u, capturedInstanceId, "SDL.GetMouseNameForID must forward instanceId for null strings.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetMouseNameForID must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GetMouseFocus_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetMouseFocus");
        AssertSdlImport(nativeMethod, "SDL_GetMouseFocus");

        ResetCaptureState();
        nextPointer = (IntPtr)0x5101;

        using NativeHookScope _ = NativeHookScope.Install("GetMouseFocusNativeFunction", nameof(CaptureNoArgumentPointer));
        IntPtr result = SDL3.SDL.GetMouseFocus();

        TestAssert.Equal((IntPtr)0x5101, result, "SDL.GetMouseFocus must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetMouseFocus must call the native hook once.");
    }

    public static void GetMouseState_ReturnsCoordinatesAndButtonFlags()
    {
        AssertMouseStateMethod("SDL.GetMouseState", "SDL_GetMouseState", "GetMouseStateNativeFunction", SDL3.SDL.GetMouseState);
    }

    public static void GetGlobalMouseState_ReturnsCoordinatesAndButtonFlags()
    {
        AssertMouseStateMethod("SDL.GetGlobalMouseState", "SDL_GetGlobalMouseState", "GetGlobalMouseStateNativeFunction", SDL3.SDL.GetGlobalMouseState);
    }

    public static void GetRelativeMouseState_ReturnsCoordinatesAndButtonFlags()
    {
        AssertMouseStateMethod("SDL.GetRelativeMouseState", "SDL_GetRelativeMouseState", "GetRelativeMouseStateNativeFunction", SDL3.SDL.GetRelativeMouseState);
    }

    public static void WarpMouseInWindow_ForwardsWindowAndCoordinates()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WarpMouseInWindow");
        AssertSdlImport(nativeMethod, "SDL_WarpMouseInWindow");

        ResetCaptureState();
        IntPtr window = (IntPtr)0x5202;

        using NativeHookScope _ = NativeHookScope.Install("WarpMouseInWindowNativeFunction", nameof(CaptureWarpMouseInWindow));
        SDL3.SDL.WarpMouseInWindow(window, 12.5f, 34.75f);

        TestAssert.Equal(window, capturedWindow, "SDL.WarpMouseInWindow must forward window.");
        TestAssert.Equal(12.5f, capturedX, "SDL.WarpMouseInWindow must forward x.");
        TestAssert.Equal(34.75f, capturedY, "SDL.WarpMouseInWindow must forward y.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WarpMouseInWindow must call the native hook once.");
    }

    public static void WarpMouseGlobal_ForwardsCoordinatesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WarpMouseGlobal");
        AssertSdlImport(nativeMethod, "SDL_WarpMouseGlobal");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("WarpMouseGlobalNativeFunction", nameof(CaptureWarpMouseGlobal));
        bool result = SDL3.SDL.WarpMouseGlobal(40.5f, 50.25f);

        TestAssert.Equal(true, result, "SDL.WarpMouseGlobal must return the native hook value.");
        TestAssert.Equal(40.5f, capturedX, "SDL.WarpMouseGlobal must forward x.");
        TestAssert.Equal(50.25f, capturedY, "SDL.WarpMouseGlobal must forward y.");
        TestAssert.Equal(1, capturedCallCount, "SDL.WarpMouseGlobal must call the native hook once.");
    }

    public static void SetRelativeMouseTransform_ForwardsCallbackUserdataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetRelativeMouseTransform");
        AssertSdlImport(nativeMethod, "SDL_SetRelativeMouseTransform");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.MouseMotionTransformCallback callback = CaptureMouseMotionTransform;
        IntPtr userdata = (IntPtr)0x5303;

        using NativeHookScope _ = NativeHookScope.Install("SetRelativeMouseTransformNativeFunction", nameof(CaptureSetRelativeMouseTransform));
        bool result = SDL3.SDL.SetRelativeMouseTransform(callback, userdata);

        TestAssert.Equal(true, result, "SDL.SetRelativeMouseTransform must return the native hook value.");
        TestAssert.True(ReferenceEquals(callback, capturedTransformCallback), "SDL.SetRelativeMouseTransform must forward callback.");
        TestAssert.Equal(userdata, capturedUserdata, "SDL.SetRelativeMouseTransform must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetRelativeMouseTransform must call the native hook once.");
    }

    public static void SetWindowRelativeMouseMode_ForwardsWindowEnabledAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetWindowRelativeMouseMode");
        AssertSdlImport(nativeMethod, "SDL_SetWindowRelativeMouseMode");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        ResetCaptureState();
        nextBool = true;
        IntPtr window = (IntPtr)0x5404;

        using NativeHookScope _ = NativeHookScope.Install("SetWindowRelativeMouseModeNativeFunction", nameof(CaptureWindowEnabledBool));
        bool result = SDL3.SDL.SetWindowRelativeMouseMode(window, true);

        TestAssert.Equal(true, result, "SDL.SetWindowRelativeMouseMode must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, "SDL.SetWindowRelativeMouseMode must forward window.");
        TestAssert.Equal(true, capturedEnabled, "SDL.SetWindowRelativeMouseMode must forward enabled.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetWindowRelativeMouseMode must call the native hook once.");
    }

    public static void GetWindowRelativeMouseMode_ForwardsWindowAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetWindowRelativeMouseMode");
        AssertSdlImport(nativeMethod, "SDL_GetWindowRelativeMouseMode");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        IntPtr window = (IntPtr)0x5505;

        using NativeHookScope _ = NativeHookScope.Install("GetWindowRelativeMouseModeNativeFunction", nameof(CaptureWindowBool));
        bool result = SDL3.SDL.GetWindowRelativeMouseMode(window);

        TestAssert.Equal(true, result, "SDL.GetWindowRelativeMouseMode must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, "SDL.GetWindowRelativeMouseMode must forward window.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetWindowRelativeMouseMode must call the native hook once.");
    }

    public static void CaptureMouse_ForwardsEnabledAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CaptureMouse");
        AssertSdlImport(nativeMethod, "SDL_CaptureMouse");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("CaptureMouseNativeFunction", nameof(CaptureEnabledBool));
        bool result = SDL3.SDL.CaptureMouse(true);

        TestAssert.Equal(true, result, "SDL.CaptureMouse must return the native hook value.");
        TestAssert.Equal(true, capturedEnabled, "SDL.CaptureMouse must forward enabled.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CaptureMouse must call the native hook once.");
    }

    public static void CreateCursor_ForwardsDataMaskDimensionsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateCursor");
        AssertSdlImport(nativeMethod, "SDL_CreateCursor");

        ResetCaptureState();
        nextPointer = (IntPtr)0x5606;
        byte[] data = [0x80, 0x40];
        byte[] mask = [0xff, 0x00];

        using NativeHookScope _ = NativeHookScope.Install("CreateCursorNativeFunction", nameof(CaptureCreateCursor));
        IntPtr result = SDL3.SDL.CreateCursor(data, mask, 16, 8, 3, 4);

        TestAssert.Equal((IntPtr)0x5606, result, "SDL.CreateCursor must return the native hook value.");
        TestAssert.True(ReferenceEquals(data, capturedData), "SDL.CreateCursor must forward data array.");
        TestAssert.True(ReferenceEquals(mask, capturedMask), "SDL.CreateCursor must forward mask array.");
        TestAssert.Equal(16, capturedW, "SDL.CreateCursor must forward width.");
        TestAssert.Equal(8, capturedH, "SDL.CreateCursor must forward height.");
        TestAssert.Equal(3, capturedHotX, "SDL.CreateCursor must forward hotX.");
        TestAssert.Equal(4, capturedHotY, "SDL.CreateCursor must forward hotY.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateCursor must call the native hook once.");
    }

    public static void CreateColorCursor_ForwardsSurfaceHotspotAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateColorCursor");
        AssertSdlImport(nativeMethod, "SDL_CreateColorCursor");

        ResetCaptureState();
        nextPointer = (IntPtr)0x5707;
        IntPtr surface = (IntPtr)0x5808;

        using NativeHookScope _ = NativeHookScope.Install("CreateColorCursorNativeFunction", nameof(CaptureCreateColorCursor));
        IntPtr result = SDL3.SDL.CreateColorCursor(surface, 5, 6);

        TestAssert.Equal((IntPtr)0x5707, result, "SDL.CreateColorCursor must return the native hook value.");
        TestAssert.Equal(surface, capturedSurface, "SDL.CreateColorCursor must forward surface.");
        TestAssert.Equal(5, capturedHotX, "SDL.CreateColorCursor must forward hotX.");
        TestAssert.Equal(6, capturedHotY, "SDL.CreateColorCursor must forward hotY.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateColorCursor must call the native hook once.");
    }

    public static void CreateAnimatedCursor_ForwardsFramesCountHotspotAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateAnimatedCursor");
        AssertSdlImport(nativeMethod, "SDL_CreateAnimatedCursor");
        AssertArrayParameterMarshal(nativeMethod, "frames", 1);

        ResetCaptureState();
        nextPointer = (IntPtr)0x5909;
        SDL3.SDL.CursorFrameInfo[] frames =
        [
            new SDL3.SDL.CursorFrameInfo { Surface = (IntPtr)0x6001, Duration = 10 },
            new SDL3.SDL.CursorFrameInfo { Surface = (IntPtr)0x6002, Duration = 0 }
        ];

        using NativeHookScope _ = NativeHookScope.Install("CreateAnimatedCursorNativeFunction", nameof(CaptureCreateAnimatedCursor));
        IntPtr result = SDL3.SDL.CreateAnimatedCursor(frames, frames.Length, 7, 8);

        TestAssert.Equal((IntPtr)0x5909, result, "SDL.CreateAnimatedCursor must return the native hook value.");
        TestAssert.True(ReferenceEquals(frames, capturedFrames), "SDL.CreateAnimatedCursor must forward frames array.");
        TestAssert.Equal(frames.Length, capturedFrameCount, "SDL.CreateAnimatedCursor must forward frame count.");
        TestAssert.Equal(7, capturedHotX, "SDL.CreateAnimatedCursor must forward hotX.");
        TestAssert.Equal(8, capturedHotY, "SDL.CreateAnimatedCursor must forward hotY.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateAnimatedCursor must call the native hook once.");
    }

    public static void CreateSystemCursor_ForwardsIdAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateSystemCursor");
        AssertSdlImport(nativeMethod, "SDL_CreateSystemCursor");

        ResetCaptureState();
        nextPointer = (IntPtr)0x6101;

        using NativeHookScope _ = NativeHookScope.Install("CreateSystemCursorNativeFunction", nameof(CaptureCreateSystemCursor));
        IntPtr result = SDL3.SDL.CreateSystemCursor(SDL3.SDL.SystemCursor.Pointer);

        TestAssert.Equal((IntPtr)0x6101, result, "SDL.CreateSystemCursor must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.SystemCursor.Pointer, capturedSystemCursor, "SDL.CreateSystemCursor must forward system cursor id.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateSystemCursor must call the native hook once.");
    }

    public static void SetCursor_ForwardsCursorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetCursor");
        AssertSdlImport(nativeMethod, "SDL_SetCursor");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        IntPtr cursor = (IntPtr)0x6202;

        using NativeHookScope _ = NativeHookScope.Install("SetCursorNativeFunction", nameof(CaptureCursorBool));
        bool result = SDL3.SDL.SetCursor(cursor);

        TestAssert.Equal(true, result, "SDL.SetCursor must return the native hook value.");
        TestAssert.Equal(cursor, capturedCursor, "SDL.SetCursor must forward cursor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetCursor must call the native hook once.");
    }

    public static void GetCursor_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetCursor");
        AssertSdlImport(nativeMethod, "SDL_GetCursor");

        ResetCaptureState();
        nextPointer = (IntPtr)0x6303;

        using NativeHookScope _ = NativeHookScope.Install("GetCursorNativeFunction", nameof(CaptureNoArgumentPointer));
        IntPtr result = SDL3.SDL.GetCursor();

        TestAssert.Equal((IntPtr)0x6303, result, "SDL.GetCursor must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetCursor must call the native hook once.");
    }

    public static void GetDefaultCursor_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetDefaultCursor");
        AssertSdlImport(nativeMethod, "SDL_GetDefaultCursor");

        ResetCaptureState();
        nextPointer = (IntPtr)0x6404;

        using NativeHookScope _ = NativeHookScope.Install("GetDefaultCursorNativeFunction", nameof(CaptureNoArgumentPointer));
        IntPtr result = SDL3.SDL.GetDefaultCursor();

        TestAssert.Equal((IntPtr)0x6404, result, "SDL.GetDefaultCursor must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetDefaultCursor must call the native hook once.");
    }

    public static void DestroyCursor_ForwardsCursor()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyCursor");
        AssertSdlImport(nativeMethod, "SDL_DestroyCursor");

        ResetCaptureState();
        IntPtr cursor = (IntPtr)0x6505;

        using NativeHookScope _ = NativeHookScope.Install("DestroyCursorNativeFunction", nameof(CaptureCursorVoid));
        SDL3.SDL.DestroyCursor(cursor);

        TestAssert.Equal(cursor, capturedCursor, "SDL.DestroyCursor must forward cursor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DestroyCursor must call the native hook once.");
    }

    public static void ShowCursor_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ShowCursor");
        AssertSdlImport(nativeMethod, "SDL_ShowCursor");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("ShowCursorNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.ShowCursor();

        TestAssert.Equal(true, result, "SDL.ShowCursor must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ShowCursor must call the native hook once.");
    }

    public static void HideCursor_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HideCursor");
        AssertSdlImport(nativeMethod, "SDL_HideCursor");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HideCursorNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.HideCursor();

        TestAssert.Equal(true, result, "SDL.HideCursor must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HideCursor must call the native hook once.");
    }

    public static void CursorVisible_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CursorVisible");
        AssertSdlImport(nativeMethod, "SDL_CursorVisible");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("CursorVisibleNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.CursorVisible();

        TestAssert.Equal(true, result, "SDL.CursorVisible must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CursorVisible must call the native hook once.");
    }

    private static void AssertMouseStateMethod(string apiName, string nativeMethodName, string hookFieldName, MouseStateCaller call)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeMethodName);
        AssertSdlImport(nativeMethod, nativeMethodName);
        AssertOutParameter(nativeMethod, "x");
        AssertOutParameter(nativeMethod, "y");

        ResetCaptureState();
        nextX = 101.5f;
        nextY = 202.25f;
        nextMouseButtonFlags = SDL3.SDL.MouseButtonFlags.Left | SDL3.SDL.MouseButtonFlags.Right;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureMouseState));
        SDL3.SDL.MouseButtonFlags result = call(out float x, out float y);

        TestAssert.Equal(nextMouseButtonFlags, result, $"{apiName} must return native button flags.");
        TestAssert.Equal(nextX, x, $"{apiName} must output x.");
        TestAssert.Equal(nextY, y, $"{apiName} must output y.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static bool CaptureNoArgumentBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureNoArgumentPointer()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureArrayPointer(out int count)
    {
        capturedCallCount++;
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureInstancePointer(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextPointer;
    }

    private static SDL3.SDL.MouseButtonFlags CaptureMouseState(out float x, out float y)
    {
        capturedCallCount++;
        x = nextX;
        y = nextY;
        return nextMouseButtonFlags;
    }

    private static void CaptureWarpMouseInWindow(IntPtr window, float x, float y)
    {
        capturedCallCount++;
        capturedWindow = window;
        capturedX = x;
        capturedY = y;
    }

    private static bool CaptureWarpMouseGlobal(float x, float y)
    {
        capturedCallCount++;
        capturedX = x;
        capturedY = y;
        return nextBool;
    }

    private static bool CaptureSetRelativeMouseTransform(SDL3.SDL.MouseMotionTransformCallback callback, IntPtr userdata)
    {
        capturedCallCount++;
        capturedTransformCallback = callback;
        capturedUserdata = userdata;
        return nextBool;
    }

    private static bool CaptureWindowEnabledBool(IntPtr window, bool enabled)
    {
        capturedCallCount++;
        capturedWindow = window;
        capturedEnabled = enabled;
        return nextBool;
    }

    private static bool CaptureWindowBool(IntPtr window)
    {
        capturedCallCount++;
        capturedWindow = window;
        return nextBool;
    }

    private static bool CaptureEnabledBool(bool enabled)
    {
        capturedCallCount++;
        capturedEnabled = enabled;
        return nextBool;
    }

    private static IntPtr CaptureCreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY)
    {
        capturedCallCount++;
        capturedData = data;
        capturedMask = mask;
        capturedW = w;
        capturedH = h;
        capturedHotX = hotX;
        capturedHotY = hotY;
        return nextPointer;
    }

    private static IntPtr CaptureCreateColorCursor(IntPtr surface, int hotX, int hotY)
    {
        capturedCallCount++;
        capturedSurface = surface;
        capturedHotX = hotX;
        capturedHotY = hotY;
        return nextPointer;
    }

    private static IntPtr CaptureCreateAnimatedCursor(SDL3.SDL.CursorFrameInfo[] frames, int frameCount, int hotX, int hotY)
    {
        capturedCallCount++;
        capturedFrames = frames;
        capturedFrameCount = frameCount;
        capturedHotX = hotX;
        capturedHotY = hotY;
        return nextPointer;
    }

    private static IntPtr CaptureCreateSystemCursor(SDL3.SDL.SystemCursor id)
    {
        capturedCallCount++;
        capturedSystemCursor = id;
        return nextPointer;
    }

    private static bool CaptureCursorBool(IntPtr cursor)
    {
        capturedCallCount++;
        capturedCursor = cursor;
        return nextBool;
    }

    private static void CaptureCursorVoid(IntPtr cursor)
    {
        capturedCallCount++;
        capturedCursor = cursor;
    }

    private static void CaptureMouseMotionTransform(IntPtr userdata, ulong timestamp, IntPtr window, uint mouseId, ref float x, ref float y)
    {
        capturedUserdata = userdata;
        capturedWindow = window;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeCallCount++;
        capturedFreeMemory = mem;
    }

    private static void ResetCaptureState()
    {
        capturedInstanceId = 0;
        capturedWindow = IntPtr.Zero;
        capturedCursor = IntPtr.Zero;
        capturedSurface = IntPtr.Zero;
        capturedUserdata = IntPtr.Zero;
        capturedFreeMemory = IntPtr.Zero;
        capturedTransformCallback = null;
        capturedData = null;
        capturedMask = null;
        capturedFrames = null;
        capturedSystemCursor = default;
        capturedEnabled = false;
        capturedX = 0;
        capturedY = 0;
        capturedW = 0;
        capturedH = 0;
        capturedHotX = 0;
        capturedHotY = 0;
        capturedFrameCount = 0;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextCount = 0;
        nextX = 0;
        nextY = 0;
        nextMouseButtonFlags = 0;
    }

    private static IntPtr CreateNativeUIntArray(params uint[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(sizeof(uint) * values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(pointer, i * sizeof(uint), unchecked((int)values[i]));
        }

        return pointer;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertSdlImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
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

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, string parameterName, short sizeParamIndex)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use LPArray marshalling.");
        TestAssert.Equal(sizeParamIndex, marshalAs.SizeParamIndex, $"SDL.{method.Name} parameter {parameterName} must use the frame count parameter.");
    }

    private static void AssertOutParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.IsOut, $"SDL.{method.Name} parameter {parameterName} must be an out parameter.");
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
