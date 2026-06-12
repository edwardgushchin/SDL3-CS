using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Clipboard;

internal static class PInvokeTests
{
    private static readonly List<IntPtr> allocatedStringPointers = [];
    private static IntPtr nextPointer;
    private static IntPtr capturedFreePointer;
    private static SDL3.SDL.ClipboardDataCallback? capturedDataCallback;
    private static SDL3.SDL.ClipboardCleanupCallback? capturedCleanupCallback;
    private static string[]? capturedMimeTypes;
    private static string? capturedText;
    private static string? capturedMimeType;
    private static string? callbackMimeType;
    private static IntPtr capturedUserdata;
    private static IntPtr callbackUserdata;
    private static IntPtr cleanupUserdata;
    private static UIntPtr nextUIntPtr;
    private static UIntPtr capturedNumMimeTypes;
    private static UIntPtr callbackSize;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static bool nextBool;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        SetClipboardText_ForwardsUtf8TextAndReturnsNativeValue();
        GetClipboardText_ReturnsUtf8StringAndFreesNativeText();
        GetClipboardText_ReturnsEmptyStringWithoutFreeForNativeNull();
        HasClipboardText_ReturnsNativeValue();
        SetPrimarySelectionText_ForwardsUtf8TextAndReturnsNativeValue();
        GetPrimarySelectionText_ReturnsUtf8StringAndFreesNativeText();
        GetPrimarySelectionText_ReturnsEmptyStringWithoutFreeForNativeNull();
        HasPrimarySelectionText_ReturnsNativeValue();
        SetClipboardData_ForwardsCallbacksUserdataMimeTypesAndReturnsNativeValue();
        ClearClipboardData_ReturnsNativeValue();
        GetClipboardData_ForwardsMimeTypeReturnsSizeAndPointer();
        HasClipboardData_ForwardsMimeTypeAndReturnsNativeValue();
        GetClipboardMimeTypes_ReturnsStringsCountAndFreesNativeArray();
        GetClipboardMimeTypes_ReturnsNullWithoutFreeForNativeNull();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        MethodInfo setClipboardText = GetNativeMethod("SDL_SetClipboardText");
        AssertSdlLibraryImport(setClipboardText, "SDL_SetClipboardText");
        AssertBoolReturnMarshal(setClipboardText);
        AssertStringParameterMarshal(setClipboardText, "text");

        AssertSdlLibraryImport(GetNativeMethod("SDL_GetClipboardText"), "SDL_GetClipboardText");

        MethodInfo hasClipboardText = GetNativeMethod("SDL_HasClipboardText");
        AssertSdlLibraryImport(hasClipboardText, "SDL_HasClipboardText");
        AssertBoolReturnMarshal(hasClipboardText);

        MethodInfo setPrimarySelectionText = GetNativeMethod("SDL_SetPrimarySelectionText");
        AssertSdlLibraryImport(setPrimarySelectionText, "SDL_SetPrimarySelectionText");
        AssertBoolReturnMarshal(setPrimarySelectionText);
        AssertStringParameterMarshal(setPrimarySelectionText, "text");

        AssertSdlLibraryImport(GetNativeMethod("SDL_GetPrimarySelectionText"), "SDL_GetPrimarySelectionText");

        MethodInfo hasPrimarySelectionText = GetNativeMethod("SDL_HasPrimarySelectionText");
        AssertSdlLibraryImport(hasPrimarySelectionText, "SDL_HasPrimarySelectionText");
        AssertBoolReturnMarshal(hasPrimarySelectionText);

        MethodInfo setClipboardData = GetNativeMethod("SDL_SetClipboardData");
        AssertSdlLibraryImport(setClipboardData, "SDL_SetClipboardData");
        AssertBoolReturnMarshal(setClipboardData);

        MethodInfo clearClipboardData = GetNativeMethod("SDL_ClearClipboardData");
        AssertSdlLibraryImport(clearClipboardData, "SDL_ClearClipboardData");
        AssertBoolReturnMarshal(clearClipboardData);

        MethodInfo getClipboardData = GetNativeMethod("SDL_GetClipboardData");
        AssertSdlLibraryImport(getClipboardData, "SDL_GetClipboardData");
        AssertStringParameterMarshal(getClipboardData, "mimeType");

        MethodInfo hasClipboardData = GetNativeMethod("SDL_HasClipboardData");
        AssertSdlLibraryImport(hasClipboardData, "SDL_HasClipboardData");
        AssertBoolReturnMarshal(hasClipboardData);
        AssertStringParameterMarshal(hasClipboardData, "mimeType");

        AssertSdlLibraryImport(GetNativeMethod("SDL_GetClipboardMimeTypes"), "SDL_GetClipboardMimeTypes");
    }

    public static void SetClipboardText_ForwardsUtf8TextAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetClipboardTextNativeFunction", nameof(CaptureTextBool));
        bool result = SDL3.SDL.SetClipboardText("clipboard text");

        TestAssert.Equal(true, result, "SDL.SetClipboardText must return the native hook value.");
        TestAssert.Equal("clipboard text", capturedText, "SDL.SetClipboardText must forward text.");
    }

    public static void GetClipboardText_ReturnsUtf8StringAndFreesNativeText()
    {
        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("copied text");

        using NativeHookScope textHook = NativeHookScope.Install("GetClipboardTextNativeFunction", nameof(CapturePointerNoArgs));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        try
        {
            string result = SDL3.SDL.GetClipboardText();

            TestAssert.Equal("copied text", result, "SDL.GetClipboardText must convert UTF-8 native strings.");
            TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetClipboardText must free native text.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetClipboardText must free native text once.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    public static void GetClipboardText_ReturnsEmptyStringWithoutFreeForNativeNull()
    {
        ResetCaptureState();

        using NativeHookScope textHook = NativeHookScope.Install("GetClipboardTextNativeFunction", nameof(CapturePointerNoArgs));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        string result = SDL3.SDL.GetClipboardText();

        TestAssert.Equal("", result, "SDL.GetClipboardText must return an empty string for native null.");
        TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetClipboardText must not free native null.");
    }

    public static void HasClipboardText_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasClipboardTextNativeFunction", nameof(CaptureBoolNoArgs));
        bool result = SDL3.SDL.HasClipboardText();

        TestAssert.Equal(true, result, "SDL.HasClipboardText must return the native hook value.");
    }

    public static void SetPrimarySelectionText_ForwardsUtf8TextAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("SetPrimarySelectionTextNativeFunction", nameof(CaptureTextBool));
        bool result = SDL3.SDL.SetPrimarySelectionText("primary text");

        TestAssert.Equal(true, result, "SDL.SetPrimarySelectionText must return the native hook value.");
        TestAssert.Equal("primary text", capturedText, "SDL.SetPrimarySelectionText must forward text.");
    }

    public static void GetPrimarySelectionText_ReturnsUtf8StringAndFreesNativeText()
    {
        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("primary copied text");

        using NativeHookScope textHook = NativeHookScope.Install("GetPrimarySelectionTextNativeFunction", nameof(CapturePointerNoArgs));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        try
        {
            string result = SDL3.SDL.GetPrimarySelectionText();

            TestAssert.Equal("primary copied text", result, "SDL.GetPrimarySelectionText must convert UTF-8 native strings.");
            TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetPrimarySelectionText must free native text.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetPrimarySelectionText must free native text once.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    public static void GetPrimarySelectionText_ReturnsEmptyStringWithoutFreeForNativeNull()
    {
        ResetCaptureState();

        using NativeHookScope textHook = NativeHookScope.Install("GetPrimarySelectionTextNativeFunction", nameof(CapturePointerNoArgs));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        string result = SDL3.SDL.GetPrimarySelectionText();

        TestAssert.Equal("", result, "SDL.GetPrimarySelectionText must return an empty string for native null.");
        TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetPrimarySelectionText must not free native null.");
    }

    public static void HasPrimarySelectionText_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasPrimarySelectionTextNativeFunction", nameof(CaptureBoolNoArgs));
        bool result = SDL3.SDL.HasPrimarySelectionText();

        TestAssert.Equal(true, result, "SDL.HasPrimarySelectionText must return the native hook value.");
    }

    public static void SetClipboardData_ForwardsCallbacksUserdataMimeTypesAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        string[] mimeTypes = ["text/plain", "text/html"];

        using NativeHookScope _ = NativeHookScope.Install("SetClipboardDataNativeFunction", nameof(CaptureSetClipboardData));
        bool result = SDL3.SDL.SetClipboardData(TestClipboardDataCallback, TestClipboardCleanupCallback, (IntPtr)0x1111, mimeTypes, (UIntPtr)mimeTypes.Length);

        TestAssert.Equal(true, result, "SDL.SetClipboardData must return the native hook value.");
        TestAssert.NotNull(capturedDataCallback, "SDL.SetClipboardData must forward data callback.");
        TestAssert.NotNull(capturedCleanupCallback, "SDL.SetClipboardData must forward cleanup callback.");
        TestAssert.Equal((IntPtr)0x1111, capturedUserdata, "SDL.SetClipboardData must forward userdata.");
        TestAssert.Equal((UIntPtr)2, capturedNumMimeTypes, "SDL.SetClipboardData must forward mime type count.");
        TestAssert.NotNull(capturedMimeTypes, "SDL.SetClipboardData must forward mime types.");
        TestAssert.Equal("text/plain", capturedMimeTypes![0], "SDL.SetClipboardData must forward first mime type.");
        TestAssert.Equal("text/html", capturedMimeTypes[1], "SDL.SetClipboardData must forward second mime type.");

        IntPtr callbackResult = capturedDataCallback!((IntPtr)0x2222, "text/plain", out UIntPtr size);
        capturedCleanupCallback!((IntPtr)0x3333);

        TestAssert.Equal((IntPtr)0x4444, callbackResult, "SDL.SetClipboardData must preserve data callback.");
        TestAssert.Equal((UIntPtr)5, size, "SDL.SetClipboardData must preserve callback size.");
        TestAssert.Equal((IntPtr)0x2222, callbackUserdata, "SDL.SetClipboardData must preserve data callback userdata.");
        TestAssert.Equal("text/plain", callbackMimeType, "SDL.SetClipboardData must preserve data callback mime type.");
        TestAssert.Equal((IntPtr)0x3333, cleanupUserdata, "SDL.SetClipboardData must preserve cleanup callback.");
    }

    public static void ClearClipboardData_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("ClearClipboardDataNativeFunction", nameof(CaptureBoolNoArgs));
        bool result = SDL3.SDL.ClearClipboardData();

        TestAssert.Equal(true, result, "SDL.ClearClipboardData must return the native hook value.");
    }

    public static void GetClipboardData_ForwardsMimeTypeReturnsSizeAndPointer()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x5555;
        nextUIntPtr = (UIntPtr)64;

        using NativeHookScope _ = NativeHookScope.Install("GetClipboardDataNativeFunction", nameof(CaptureGetClipboardData));
        IntPtr result = SDL3.SDL.GetClipboardData("application/octet-stream", out UIntPtr size);

        TestAssert.Equal((IntPtr)0x5555, result, "SDL.GetClipboardData must return the native hook pointer.");
        TestAssert.Equal((UIntPtr)64, size, "SDL.GetClipboardData must return native size.");
        TestAssert.Equal("application/octet-stream", capturedMimeType, "SDL.GetClipboardData must forward mime type.");
    }

    public static void HasClipboardData_ForwardsMimeTypeAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;

        using NativeHookScope _ = NativeHookScope.Install("HasClipboardDataNativeFunction", nameof(CaptureHasClipboardData));
        bool result = SDL3.SDL.HasClipboardData("image/png");

        TestAssert.Equal(true, result, "SDL.HasClipboardData must return the native hook value.");
        TestAssert.Equal("image/png", capturedMimeType, "SDL.HasClipboardData must forward mime type.");
    }

    public static void GetClipboardMimeTypes_ReturnsStringsCountAndFreesNativeArray()
    {
        ResetCaptureState();
        string[] expected = ["text/plain", "image/png"];
        nextPointer = AllocateStringPointerArray(expected);
        nextUIntPtr = (UIntPtr)expected.Length;

        using NativeHookScope mimeHook = NativeHookScope.Install("GetClipboardMimeTypesNativeFunction", nameof(CaptureClipboardMimeTypes));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        try
        {
            string[]? result = SDL3.SDL.GetClipboardMimeTypes(out UIntPtr numMimeTypes);

            TestAssert.Equal((UIntPtr)expected.Length, numMimeTypes, "SDL.GetClipboardMimeTypes must return native count.");
            TestAssert.NotNull(result, "SDL.GetClipboardMimeTypes must copy native strings.");
            TestAssert.Equal(expected.Length, result!.Length, "SDL.GetClipboardMimeTypes must preserve mime type count.");
            for (int i = 0; i < expected.Length; i++)
            {
                TestAssert.Equal(expected[i], result[i], $"SDL.GetClipboardMimeTypes must copy mime type {i}.");
            }

            TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetClipboardMimeTypes must free native string array.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetClipboardMimeTypes must free native string array once.");
        }
        finally
        {
            FreeAllocatedStringPointerArray(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    public static void GetClipboardMimeTypes_ReturnsNullWithoutFreeForNativeNull()
    {
        ResetCaptureState();
        nextUIntPtr = (UIntPtr)3;

        using NativeHookScope mimeHook = NativeHookScope.Install("GetClipboardMimeTypesNativeFunction", nameof(CaptureClipboardMimeTypes));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        string[]? result = SDL3.SDL.GetClipboardMimeTypes(out UIntPtr numMimeTypes);

        TestAssert.Equal((UIntPtr)3, numMimeTypes, "SDL.GetClipboardMimeTypes must expose native count for null pointers.");
        TestAssert.Equal<string[]?>(null, result, "SDL.GetClipboardMimeTypes must return null for native null.");
        TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetClipboardMimeTypes must not free native null.");
    }

    private static bool CaptureTextBool(string text)
    {
        capturedText = text;
        return nextBool;
    }

    private static IntPtr CapturePointerNoArgs()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureBoolNoArgs()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetClipboardData(
        SDL3.SDL.ClipboardDataCallback callback,
        SDL3.SDL.ClipboardCleanupCallback cleanup,
        IntPtr userdata,
        string[] mimeTypes,
        UIntPtr numMimeTypes)
    {
        capturedDataCallback = callback;
        capturedCleanupCallback = cleanup;
        capturedUserdata = userdata;
        capturedMimeTypes = [.. mimeTypes];
        capturedNumMimeTypes = numMimeTypes;
        return nextBool;
    }

    private static IntPtr CaptureGetClipboardData(string mimeType, out UIntPtr size)
    {
        capturedMimeType = mimeType;
        size = nextUIntPtr;
        return nextPointer;
    }

    private static bool CaptureHasClipboardData(string mimeType)
    {
        capturedMimeType = mimeType;
        return nextBool;
    }

    private static IntPtr CaptureClipboardMimeTypes(out UIntPtr numMimeTypes)
    {
        numMimeTypes = nextUIntPtr;
        return nextPointer;
    }

    private static void CaptureFree(IntPtr pointer)
    {
        capturedFreePointer = pointer;
        capturedFreeCallCount++;
    }

    private static IntPtr TestClipboardDataCallback(IntPtr userdata, string mimeType, out UIntPtr size)
    {
        callbackUserdata = userdata;
        callbackMimeType = mimeType;
        callbackSize = (UIntPtr)5;
        size = callbackSize;
        return (IntPtr)0x4444;
    }

    private static void TestClipboardCleanupCallback(IntPtr userdata)
    {
        cleanupUserdata = userdata;
    }

    private static IntPtr AllocateStringPointerArray(string[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(IntPtr.Size * (values.Length + 1));
        for (int i = 0; i < values.Length; i++)
        {
            IntPtr stringPointer = Marshal.StringToCoTaskMemUTF8(values[i]);
            allocatedStringPointers.Add(stringPointer);
            Marshal.WriteIntPtr(pointer, i * IntPtr.Size, stringPointer);
        }

        Marshal.WriteIntPtr(pointer, values.Length * IntPtr.Size, IntPtr.Zero);
        return pointer;
    }

    private static void FreeAllocatedStringPointerArray(IntPtr pointer)
    {
        foreach (IntPtr stringPointer in allocatedStringPointers)
        {
            Marshal.FreeCoTaskMem(stringPointer);
        }

        allocatedStringPointers.Clear();
        Marshal.FreeHGlobal(pointer);
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedFreePointer = IntPtr.Zero;
        capturedDataCallback = null;
        capturedCleanupCallback = null;
        capturedMimeTypes = null;
        capturedText = null;
        capturedMimeType = null;
        callbackMimeType = null;
        capturedUserdata = IntPtr.Zero;
        callbackUserdata = IntPtr.Zero;
        cleanupUserdata = IntPtr.Zero;
        nextUIntPtr = UIntPtr.Zero;
        capturedNumMimeTypes = UIntPtr.Zero;
        callbackSize = UIntPtr.Zero;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextBool = false;
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
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name}.{parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name}.{parameterName} must use UTF-8 string marshalling.");
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private NativeHookScope(FieldInfo field, object? hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL private hook field {fieldName} must exist.");

            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"Test hook method {methodName} must exist.");

            Delegate hook = Delegate.CreateDelegate(field!.FieldType, method!);

            return new NativeHookScope(field, hook);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
