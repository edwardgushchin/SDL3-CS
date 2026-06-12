using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Keyboard;

internal static class PInvokeTests
{
    private static uint capturedInstanceId;
    private static SDL3.SDL.Scancode capturedScancode;
    private static SDL3.SDL.Keycode capturedKeycode;
    private static SDL3.SDL.Keymod capturedModState;
    private static bool capturedKeyEvent;
    private static string? capturedName;
    private static IntPtr capturedWindow;
    private static IntPtr capturedRectPointer;
    private static uint capturedProps;
    private static int capturedCursor;
    private static SDL3.SDL.Rect capturedRect;
    private static IntPtr capturedFreeMemory;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static SDL3.SDL.Scancode nextScancode;
    private static SDL3.SDL.Keycode nextKeycode;
    private static SDL3.SDL.Keymod nextModState;
    private static SDL3.SDL.Rect nextRect;
    private static int nextCursor;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static int nextCount;

    public static void RunAll()
    {
        HasKeyboard_ReturnsNativeValue();
        SDL_GetKeyboards_UsesExpectedNativeMetadata();
        GetKeyboards_ReturnsArrayNullAndFreesNativePointer();
        SDL_GetKeyboardNameForID_UsesExpectedNativeMetadata();
        GetKeyboardNameForID_ReturnsStringAndNull();
        GetKeyboardFocus_ReturnsNativePointer();
        SDL_GetKeyboardState_UsesExpectedNativeMetadata();
        GetKeyboardState_ReturnsReadOnlyBoolSpan();
        ResetKeyboard_CallsNativeHook();
        GetModState_ReturnsNativeValue();
        SetModState_ForwardsModState();
        GetKeyFromScancode_ForwardsScancodeModStateKeyEventAndReturnsNativeValue();
        GetScancodeFromKey_ForwardsKeyAndOutputsModState();
        SetScancodeName_ForwardsScancodeNameAndReturnsNativeValue();
        SDL_GetScancodeName_UsesExpectedNativeMetadata();
        GetScancodeName_ReturnsStringAndEmpty();
        GetScancodeFromName_ForwardsNameAndReturnsNativeValue();
        SDL_GetKeyName_UsesExpectedNativeMetadata();
        GetKeyName_ReturnsStringAndEmpty();
        GetKeyFromName_ForwardsNameAndReturnsNativeValue();
        StartTextInput_ForwardsWindowAndReturnsNativeValue();
        StartTextInputWithProperties_ForwardsWindowPropsAndReturnsNativeValue();
        TextInputActive_ForwardsWindowAndReturnsNativeValue();
        StopTextInput_ForwardsWindowAndReturnsNativeValue();
        ClearComposition_ForwardsWindowAndReturnsNativeValue();
        SetTextInputAreaPointer_ForwardsWindowRectCursorAndReturnsNativeValue();
        SetTextInputAreaRef_ForwardsWindowRectCursorAndReturnsNativeValue();
        GetTextInputArea_ForwardsWindowAndOutputsRectCursor();
        HasScreenKeyboardSupport_ReturnsNativeValue();
        ScreenKeyboardShown_ForwardsWindowAndReturnsNativeValue();
    }

    public static void HasKeyboard_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasKeyboard");
        AssertSdlImport(nativeMethod, "SDL_HasKeyboard");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasKeyboardNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.HasKeyboard();

        TestAssert.Equal(true, result, "SDL.HasKeyboard must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasKeyboard must call the native hook once.");
    }

    public static void SDL_GetKeyboards_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyboards");
        AssertSdlImport(nativeMethod, "SDL_GetKeyboards");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetKeyboards_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr array = CreateNativeUIntArray(101u, 202u);

        try
        {
            nextPointer = array;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetKeyboardsNativeFunction", nameof(CaptureArrayPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            uint[]? keyboards = SDL3.SDL.GetKeyboards(out int count);

            TestAssert.NotNull(keyboards, "SDL.GetKeyboards must convert native keyboard IDs.");
            TestAssert.Equal(2, keyboards!.Length, "SDL.GetKeyboards must preserve native count.");
            TestAssert.Equal(101u, keyboards[0], "SDL.GetKeyboards must convert keyboard ID 0.");
            TestAssert.Equal(202u, keyboards[1], "SDL.GetKeyboards must convert keyboard ID 1.");
            TestAssert.Equal(2, count, "SDL.GetKeyboards must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetKeyboards must free the native array pointer.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            keyboards = SDL3.SDL.GetKeyboards(out count);

            TestAssert.Equal<uint[]?>(null, keyboards, "SDL.GetKeyboards must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetKeyboards must return native count for native null.");
            TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.GetKeyboards must pass native null to SDL.Free.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetKeyboards must call the native hook for both branches.");
            TestAssert.Equal(2, capturedFreeCallCount, "SDL.GetKeyboards must call SDL.Free for both branches.");
        }
        finally
        {
            Marshal.FreeHGlobal(array);
        }
    }

    public static void SDL_GetKeyboardNameForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyboardNameForID");
        AssertSdlImport(nativeMethod, "SDL_GetKeyboardNameForID");
    }

    public static void GetKeyboardNameForID_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("keyboard-name");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetKeyboardNameForIDNativeFunction", nameof(CaptureInstancePointer));
            string? result = SDL3.SDL.GetKeyboardNameForID(303u);

            TestAssert.Equal("keyboard-name", result, "SDL.GetKeyboardNameForID must convert native UTF-8 strings.");
            TestAssert.Equal(303u, capturedInstanceId, "SDL.GetKeyboardNameForID must forward instanceId for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetKeyboardNameForID(404u);

            TestAssert.Equal<string?>(null, result, "SDL.GetKeyboardNameForID must return null for native null.");
            TestAssert.Equal(404u, capturedInstanceId, "SDL.GetKeyboardNameForID must forward instanceId for null strings.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetKeyboardNameForID must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GetKeyboardFocus_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyboardFocus");
        AssertSdlImport(nativeMethod, "SDL_GetKeyboardFocus");

        ResetCaptureState();
        nextPointer = (IntPtr)0x4101;

        using NativeHookScope _ = NativeHookScope.Install("GetKeyboardFocusNativeFunction", nameof(CaptureNoArgumentPointer));
        IntPtr result = SDL3.SDL.GetKeyboardFocus();

        TestAssert.Equal((IntPtr)0x4101, result, "SDL.GetKeyboardFocus must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetKeyboardFocus must call the native hook once.");
    }

    public static void SDL_GetKeyboardState_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyboardState");
        AssertSdlImport(nativeMethod, "SDL_GetKeyboardState");
        AssertOutParameter(nativeMethod, "numkeys");
    }

    public static void GetKeyboardState_ReturnsReadOnlyBoolSpan()
    {
        ResetCaptureState();
        IntPtr state = Marshal.AllocHGlobal(3);

        try
        {
            Marshal.WriteByte(state, 0, 1);
            Marshal.WriteByte(state, 1, 0);
            Marshal.WriteByte(state, 2, 1);
            nextPointer = state;
            nextCount = 3;

            using NativeHookScope _ = NativeHookScope.Install("GetKeyboardStateNativeFunction", nameof(CaptureArrayPointer));
            ReadOnlySpan<bool> keyboardState = SDL3.SDL.GetKeyboardState(out int numkeys);

            TestAssert.Equal(3, numkeys, "SDL.GetKeyboardState must return native count.");
            TestAssert.Equal(3, keyboardState.Length, "SDL.GetKeyboardState must expose all native key states.");
            TestAssert.Equal(true, keyboardState[0], "SDL.GetKeyboardState must expose pressed key state.");
            TestAssert.Equal(false, keyboardState[1], "SDL.GetKeyboardState must expose released key state.");
            TestAssert.Equal(true, keyboardState[2], "SDL.GetKeyboardState must expose pressed key state 2.");
            TestAssert.Equal(1, capturedCallCount, "SDL.GetKeyboardState must call the native hook once.");
        }
        finally
        {
            Marshal.FreeHGlobal(state);
        }
    }

    public static void ResetKeyboard_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ResetKeyboard");
        AssertSdlImport(nativeMethod, "SDL_ResetKeyboard");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("ResetKeyboardNativeFunction", nameof(CaptureNoArgumentVoid));
        SDL3.SDL.ResetKeyboard();

        TestAssert.Equal(1, capturedCallCount, "SDL.ResetKeyboard must call the native hook once.");
    }

    public static void GetModState_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetModState");
        AssertSdlImport(nativeMethod, "SDL_GetModState");

        ResetCaptureState();
        nextModState = SDL3.SDL.Keymod.LShift | SDL3.SDL.Keymod.LCtrl;

        using NativeHookScope _ = NativeHookScope.Install("GetModStateNativeFunction", nameof(CaptureNoArgumentKeymod));
        SDL3.SDL.Keymod result = SDL3.SDL.GetModState();

        TestAssert.Equal(SDL3.SDL.Keymod.LShift | SDL3.SDL.Keymod.LCtrl, result, "SDL.GetModState must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetModState must call the native hook once.");
    }

    public static void SetModState_ForwardsModState()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetModState");
        AssertSdlImport(nativeMethod, "SDL_SetModState");

        ResetCaptureState();
        SDL3.SDL.Keymod modState = SDL3.SDL.Keymod.RShift | SDL3.SDL.Keymod.RAlt;

        using NativeHookScope _ = NativeHookScope.Install("SetModStateNativeFunction", nameof(CaptureSetModState));
        SDL3.SDL.SetModState(modState);

        TestAssert.Equal(modState, capturedModState, "SDL.SetModState must forward modstate.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetModState must call the native hook once.");
    }

    public static void GetKeyFromScancode_ForwardsScancodeModStateKeyEventAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyFromScancode");
        AssertSdlImport(nativeMethod, "SDL_GetKeyFromScancode");
        AssertBoolParameterMarshal(nativeMethod, "keyEvent");

        ResetCaptureState();
        nextKeycode = SDL3.SDL.Keycode.A;
        SDL3.SDL.Keymod modState = SDL3.SDL.Keymod.LShift;

        using NativeHookScope _ = NativeHookScope.Install("GetKeyFromScancodeNativeFunction", nameof(CaptureGetKeyFromScancode));
        SDL3.SDL.Keycode result = SDL3.SDL.GetKeyFromScancode(SDL3.SDL.Scancode.A, modState, true);

        TestAssert.Equal(SDL3.SDL.Keycode.A, result, "SDL.GetKeyFromScancode must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.Scancode.A, capturedScancode, "SDL.GetKeyFromScancode must forward scancode.");
        TestAssert.Equal(modState, capturedModState, "SDL.GetKeyFromScancode must forward modstate.");
        TestAssert.Equal(true, capturedKeyEvent, "SDL.GetKeyFromScancode must forward keyEvent.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetKeyFromScancode must call the native hook once.");
    }

    public static void GetScancodeFromKey_ForwardsKeyAndOutputsModState()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetScancodeFromKey");
        AssertSdlImport(nativeMethod, "SDL_GetScancodeFromKey");
        AssertOutParameter(nativeMethod, "modstate");

        ResetCaptureState();
        nextScancode = SDL3.SDL.Scancode.Space;
        nextModState = SDL3.SDL.Keymod.LAlt;

        using NativeHookScope _ = NativeHookScope.Install("GetScancodeFromKeyNativeFunction", nameof(CaptureGetScancodeFromKey));
        SDL3.SDL.Scancode result = SDL3.SDL.GetScancodeFromKey(SDL3.SDL.Keycode.Space, out SDL3.SDL.Keymod modState);

        TestAssert.Equal(SDL3.SDL.Scancode.Space, result, "SDL.GetScancodeFromKey must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.Keycode.Space, capturedKeycode, "SDL.GetScancodeFromKey must forward key.");
        TestAssert.Equal(SDL3.SDL.Keymod.LAlt, modState, "SDL.GetScancodeFromKey must output modstate.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetScancodeFromKey must call the native hook once.");
    }

    public static void SetScancodeName_ForwardsScancodeNameAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetScancodeName");
        AssertSdlImport(nativeMethod, "SDL_SetScancodeName");
        AssertBoolReturnMarshal(nativeMethod);
        AssertStringParameterMarshal(nativeMethod, "name");

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetScancodeNameNativeFunction", nameof(CaptureSetScancodeName));
        bool result = SDL3.SDL.SetScancodeName(SDL3.SDL.Scancode.Return, "Enter");

        TestAssert.Equal(true, result, "SDL.SetScancodeName must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.Scancode.Return, capturedScancode, "SDL.SetScancodeName must forward scancode.");
        TestAssert.Equal("Enter", capturedName, "SDL.SetScancodeName must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetScancodeName must call the native hook once.");
    }

    public static void SDL_GetScancodeName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetScancodeName");
        AssertSdlImport(nativeMethod, "SDL_GetScancodeName");
    }

    public static void GetScancodeName_ReturnsStringAndEmpty()
    {
        AssertScancodeStringMethod("SDL.GetScancodeName", "GetScancodeNameNativeFunction", "Space", SDL3.SDL.Scancode.Space, SDL3.SDL.Scancode.Unknown, SDL3.SDL.GetScancodeName);
    }

    public static void GetScancodeFromName_ForwardsNameAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetScancodeFromName");
        AssertSdlImport(nativeMethod, "SDL_GetScancodeFromName");
        AssertStringParameterMarshal(nativeMethod, "name");

        ResetCaptureState();
        nextScancode = SDL3.SDL.Scancode.Return;

        using NativeHookScope _ = NativeHookScope.Install("GetScancodeFromNameNativeFunction", nameof(CaptureGetScancodeFromName));
        SDL3.SDL.Scancode result = SDL3.SDL.GetScancodeFromName("Return");

        TestAssert.Equal(SDL3.SDL.Scancode.Return, result, "SDL.GetScancodeFromName must return the native hook value.");
        TestAssert.Equal("Return", capturedName, "SDL.GetScancodeFromName must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetScancodeFromName must call the native hook once.");
    }

    public static void SDL_GetKeyName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyName");
        AssertSdlImport(nativeMethod, "SDL_GetKeyName");
    }

    public static void GetKeyName_ReturnsStringAndEmpty()
    {
        AssertKeyStringMethod("SDL.GetKeyName", "GetKeyNameNativeFunction", "A", SDL3.SDL.Keycode.A, SDL3.SDL.Keycode.Unknown, SDL3.SDL.GetKeyName);
    }

    public static void GetKeyFromName_ForwardsNameAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetKeyFromName");
        AssertSdlImport(nativeMethod, "SDL_GetKeyFromName");
        AssertStringParameterMarshal(nativeMethod, "name");

        ResetCaptureState();
        nextKeycode = SDL3.SDL.Keycode.Return;

        using NativeHookScope _ = NativeHookScope.Install("GetKeyFromNameNativeFunction", nameof(CaptureGetKeyFromName));
        SDL3.SDL.Keycode result = SDL3.SDL.GetKeyFromName("Return");

        TestAssert.Equal(SDL3.SDL.Keycode.Return, result, "SDL.GetKeyFromName must return the native hook value.");
        TestAssert.Equal("Return", capturedName, "SDL.GetKeyFromName must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetKeyFromName must call the native hook once.");
    }

    public static void StartTextInput_ForwardsWindowAndReturnsNativeValue()
    {
        AssertWindowBoolMethod("SDL.StartTextInput", "SDL_StartTextInput", "StartTextInputNativeFunction", "SDL_StartTextInput", SDL3.SDL.StartTextInput);
    }

    public static void StartTextInputWithProperties_ForwardsWindowPropsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_StartTextInputWithProperties");
        AssertSdlImport(nativeMethod, "SDL_StartTextInputWithProperties");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        IntPtr window = (IntPtr)0x5102;
        const uint props = 707u;

        using NativeHookScope _ = NativeHookScope.Install("StartTextInputWithPropertiesNativeFunction", nameof(CaptureWindowPropsBool));
        bool result = SDL3.SDL.StartTextInputWithProperties(window, props);

        TestAssert.Equal(true, result, "SDL.StartTextInputWithProperties must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, "SDL.StartTextInputWithProperties must forward window.");
        TestAssert.Equal(props, capturedProps, "SDL.StartTextInputWithProperties must forward props.");
        TestAssert.Equal(1, capturedCallCount, "SDL.StartTextInputWithProperties must call the native hook once.");
    }

    public static void TextInputActive_ForwardsWindowAndReturnsNativeValue()
    {
        AssertWindowBoolMethod("SDL.TextInputActive", "SDL_TextInputActive", "TextInputActiveNativeFunction", "SDL_TextInputActive", SDL3.SDL.TextInputActive);
    }

    public static void StopTextInput_ForwardsWindowAndReturnsNativeValue()
    {
        AssertWindowBoolMethod("SDL.StopTextInput", "SDL_StopTextInput", "StopTextInputNativeFunction", "SDL_StopTextInput", SDL3.SDL.StopTextInput);
    }

    public static void ClearComposition_ForwardsWindowAndReturnsNativeValue()
    {
        AssertWindowBoolMethod("SDL.ClearComposition", "SDL_ClearComposition", "ClearCompositionNativeFunction", "SDL_ClearComposition", SDL3.SDL.ClearComposition);
    }

    public static void SetTextInputAreaPointer_ForwardsWindowRectCursorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTextInputArea", typeof(IntPtr), typeof(IntPtr), typeof(int));
        AssertSdlImport(nativeMethod, "SDL_SetTextInputArea");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        IntPtr window = (IntPtr)0x5203;
        IntPtr rect = (IntPtr)0x5304;
        const int cursor = 17;

        using NativeHookScope _ = NativeHookScope.Install("SetTextInputAreaPointerNativeFunction", nameof(CaptureSetTextInputAreaPointer));
        bool result = SDL3.SDL.SetTextInputArea(window, rect, cursor);

        TestAssert.Equal(true, result, "SDL.SetTextInputArea(IntPtr) must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, "SDL.SetTextInputArea(IntPtr) must forward window.");
        TestAssert.Equal(rect, capturedRectPointer, "SDL.SetTextInputArea(IntPtr) must forward rect pointer.");
        TestAssert.Equal(cursor, capturedCursor, "SDL.SetTextInputArea(IntPtr) must forward cursor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetTextInputArea(IntPtr) must call the native hook once.");
    }

    public static void SetTextInputAreaRef_ForwardsWindowRectCursorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTextInputArea", typeof(IntPtr), typeof(SDL3.SDL.Rect).MakeByRefType(), typeof(int));
        AssertSdlImport(nativeMethod, "SDL_SetTextInputArea");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        IntPtr window = (IntPtr)0x5405;
        SDL3.SDL.Rect rect = new()
        {
            X = 1,
            Y = 2,
            W = 300,
            H = 40
        };
        const int cursor = 9;

        using NativeHookScope _ = NativeHookScope.Install("SetTextInputAreaRefNativeFunction", nameof(CaptureSetTextInputAreaRef));
        bool result = SDL3.SDL.SetTextInputArea(window, in rect, cursor);

        TestAssert.Equal(true, result, "SDL.SetTextInputArea(in Rect) must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, "SDL.SetTextInputArea(in Rect) must forward window.");
        AssertRectEqual(rect, capturedRect, "SDL.SetTextInputArea(in Rect) must forward rect.");
        TestAssert.Equal(cursor, capturedCursor, "SDL.SetTextInputArea(in Rect) must forward cursor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetTextInputArea(in Rect) must call the native hook once.");
    }

    public static void GetTextInputArea_ForwardsWindowAndOutputsRectCursor()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTextInputArea");
        AssertSdlImport(nativeMethod, "SDL_GetTextInputArea");
        AssertBoolReturnMarshal(nativeMethod);
        AssertOutParameter(nativeMethod, "rect");
        AssertOutParameter(nativeMethod, "cursor");

        ResetCaptureState();
        nextBool = true;
        nextRect = new SDL3.SDL.Rect
        {
            X = 10,
            Y = 20,
            W = 400,
            H = 50
        };
        nextCursor = 22;
        IntPtr window = (IntPtr)0x5506;

        using NativeHookScope _ = NativeHookScope.Install("GetTextInputAreaNativeFunction", nameof(CaptureGetTextInputArea));
        bool result = SDL3.SDL.GetTextInputArea(window, out SDL3.SDL.Rect rect, out int cursor);

        TestAssert.Equal(true, result, "SDL.GetTextInputArea must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, "SDL.GetTextInputArea must forward window.");
        AssertRectEqual(nextRect, rect, "SDL.GetTextInputArea must output rect.");
        TestAssert.Equal(nextCursor, cursor, "SDL.GetTextInputArea must output cursor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetTextInputArea must call the native hook once.");
    }

    public static void HasScreenKeyboardSupport_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasScreenKeyboardSupport");
        AssertSdlImport(nativeMethod, "SDL_HasScreenKeyboardSupport");
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasScreenKeyboardSupportNativeFunction", nameof(CaptureNoArgumentBool));
        bool result = SDL3.SDL.HasScreenKeyboardSupport();

        TestAssert.Equal(true, result, "SDL.HasScreenKeyboardSupport must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasScreenKeyboardSupport must call the native hook once.");
    }

    public static void ScreenKeyboardShown_ForwardsWindowAndReturnsNativeValue()
    {
        AssertWindowBoolMethod("SDL.ScreenKeyboardShown", "SDL_ScreenKeyboardShown", "ScreenKeyboardShownNativeFunction", "SDL_ScreenKeyboardShown", SDL3.SDL.ScreenKeyboardShown);
    }

    private static void AssertWindowBoolMethod(string apiName, string nativeMethodName, string hookFieldName, string entryPoint, Func<IntPtr, bool> call)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeMethodName);
        AssertSdlImport(nativeMethod, entryPoint);
        AssertBoolReturnMarshal(nativeMethod);

        ResetCaptureState();
        nextBool = true;
        IntPtr window = (IntPtr)0x5001;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureWindowBool));
        bool result = call(window);

        TestAssert.Equal(true, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(window, capturedWindow, $"{apiName} must forward window.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertScancodeStringMethod(string apiName, string hookFieldName, string nativeValue, SDL3.SDL.Scancode firstScancode, SDL3.SDL.Scancode secondScancode, Func<SDL3.SDL.Scancode, string> call)
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8(nativeValue);

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureScancodePointer));
            string result = call(firstScancode);

            TestAssert.Equal(nativeValue, result, $"{apiName} must convert native UTF-8 strings.");
            TestAssert.Equal(firstScancode, capturedScancode, $"{apiName} must forward scancode for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = call(secondScancode);

            TestAssert.Equal("", result, $"{apiName} must return an empty string for native null.");
            TestAssert.Equal(secondScancode, capturedScancode, $"{apiName} must forward scancode for null strings.");
            TestAssert.Equal(2, capturedCallCount, $"{apiName} must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static void AssertKeyStringMethod(string apiName, string hookFieldName, string nativeValue, SDL3.SDL.Keycode firstKey, SDL3.SDL.Keycode secondKey, Func<SDL3.SDL.Keycode, string> call)
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8(nativeValue);

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureKeyPointer));
            string result = call(firstKey);

            TestAssert.Equal(nativeValue, result, $"{apiName} must convert native UTF-8 strings.");
            TestAssert.Equal(firstKey, capturedKeycode, $"{apiName} must forward key for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = call(secondKey);

            TestAssert.Equal("", result, $"{apiName} must return an empty string for native null.");
            TestAssert.Equal(secondKey, capturedKeycode, $"{apiName} must forward key for null strings.");
            TestAssert.Equal(2, capturedCallCount, $"{apiName} must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    private static void AssertRectEqual(SDL3.SDL.Rect expected, SDL3.SDL.Rect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
        TestAssert.Equal(expected.W, actual.W, $"{message} W.");
        TestAssert.Equal(expected.H, actual.H, $"{message} H.");
    }

    private static void CaptureNoArgumentVoid()
    {
        capturedCallCount++;
    }

    private static bool CaptureNoArgumentBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static SDL3.SDL.Keymod CaptureNoArgumentKeymod()
    {
        capturedCallCount++;
        return nextModState;
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

    private static void CaptureSetModState(SDL3.SDL.Keymod modstate)
    {
        capturedCallCount++;
        capturedModState = modstate;
    }

    private static SDL3.SDL.Keycode CaptureGetKeyFromScancode(SDL3.SDL.Scancode scancode, SDL3.SDL.Keymod modstate, bool keyEvent)
    {
        capturedCallCount++;
        capturedScancode = scancode;
        capturedModState = modstate;
        capturedKeyEvent = keyEvent;
        return nextKeycode;
    }

    private static SDL3.SDL.Scancode CaptureGetScancodeFromKey(SDL3.SDL.Keycode key, out SDL3.SDL.Keymod modstate)
    {
        capturedCallCount++;
        capturedKeycode = key;
        modstate = nextModState;
        return nextScancode;
    }

    private static bool CaptureSetScancodeName(SDL3.SDL.Scancode scancode, string name)
    {
        capturedCallCount++;
        capturedScancode = scancode;
        capturedName = name;
        return nextBool;
    }

    private static IntPtr CaptureScancodePointer(SDL3.SDL.Scancode scancode)
    {
        capturedCallCount++;
        capturedScancode = scancode;
        return nextPointer;
    }

    private static SDL3.SDL.Scancode CaptureGetScancodeFromName(string name)
    {
        capturedCallCount++;
        capturedName = name;
        return nextScancode;
    }

    private static IntPtr CaptureKeyPointer(SDL3.SDL.Keycode key)
    {
        capturedCallCount++;
        capturedKeycode = key;
        return nextPointer;
    }

    private static SDL3.SDL.Keycode CaptureGetKeyFromName(string name)
    {
        capturedCallCount++;
        capturedName = name;
        return nextKeycode;
    }

    private static bool CaptureWindowBool(IntPtr window)
    {
        capturedCallCount++;
        capturedWindow = window;
        return nextBool;
    }

    private static bool CaptureWindowPropsBool(IntPtr window, uint props)
    {
        capturedCallCount++;
        capturedWindow = window;
        capturedProps = props;
        return nextBool;
    }

    private static bool CaptureSetTextInputAreaPointer(IntPtr window, IntPtr rect, int cursor)
    {
        capturedCallCount++;
        capturedWindow = window;
        capturedRectPointer = rect;
        capturedCursor = cursor;
        return nextBool;
    }

    private static bool CaptureSetTextInputAreaRef(IntPtr window, in SDL3.SDL.Rect rect, int cursor)
    {
        capturedCallCount++;
        capturedWindow = window;
        capturedRect = rect;
        capturedCursor = cursor;
        return nextBool;
    }

    private static bool CaptureGetTextInputArea(IntPtr window, out SDL3.SDL.Rect rect, out int cursor)
    {
        capturedCallCount++;
        capturedWindow = window;
        rect = nextRect;
        cursor = nextCursor;
        return nextBool;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeCallCount++;
        capturedFreeMemory = mem;
    }

    private static void ResetCaptureState()
    {
        capturedInstanceId = 0;
        capturedScancode = SDL3.SDL.Scancode.Unknown;
        capturedKeycode = SDL3.SDL.Keycode.Unknown;
        capturedModState = SDL3.SDL.Keymod.None;
        capturedKeyEvent = false;
        capturedName = null;
        capturedWindow = IntPtr.Zero;
        capturedRectPointer = IntPtr.Zero;
        capturedProps = 0;
        capturedCursor = 0;
        capturedRect = default;
        capturedFreeMemory = IntPtr.Zero;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextScancode = SDL3.SDL.Scancode.Unknown;
        nextKeycode = SDL3.SDL.Keycode.Unknown;
        nextModState = SDL3.SDL.Keymod.None;
        nextRect = default;
        nextCursor = 0;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextCount = 0;
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

    private static MethodInfo GetNativeMethod(string methodName, params Type[] parameterTypes)
    {
        MethodInfo? method = parameterTypes.Length == 0
            ? typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)
            : typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static, null, parameterTypes, null);

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

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 marshalling.");
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
