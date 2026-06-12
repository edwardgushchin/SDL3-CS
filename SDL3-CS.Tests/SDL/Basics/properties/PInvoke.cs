using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Properties;

internal static class PInvokeTests
{
    private static uint capturedProps;
    private static uint capturedSrc;
    private static uint capturedDst;
    private static string? capturedName;
    private static IntPtr capturedValuePointer;
    private static SDL3.SDL.CleanupPropertyCallback? capturedCleanup;
    private static SDL3.SDL.EnumeratePropertiesCallback? capturedEnumerateCallback;
    private static IntPtr capturedUserdata;
    private static string? capturedStringValue;
    private static long capturedNumberValue;
    private static float capturedFloatValue;
    private static bool capturedBoolValue;
    private static IntPtr capturedDefaultPointer;
    private static string? capturedDefaultString;
    private static long capturedDefaultNumber;
    private static float capturedDefaultFloat;
    private static bool capturedDefaultBool;
    private static uint nextUint;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static SDL3.SDL.PropertyType nextPropertyType;
    private static long nextLong;
    private static float nextFloat;
    private static int capturedCallCount;

    public static void GetGlobalProperties_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGlobalProperties");
        AssertLibraryImport(nativeMethod, "SDL_GetGlobalProperties");

        ResetCaptureState();
        nextUint = 111;
        using NativeHookScope _ = NativeHookScope.Install("GetGlobalPropertiesNativeFunction", nameof(CaptureGetGlobalProperties));

        uint result = SDL3.SDL.GetGlobalProperties();

        TestAssert.Equal<uint>(111, result, "SDL.GetGlobalProperties must return native property id.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGlobalProperties must call native hook once.");
    }

    public static void CreateProperties_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateProperties");
        AssertLibraryImport(nativeMethod, "SDL_CreateProperties");

        ResetCaptureState();
        nextUint = 222;
        using NativeHookScope _ = NativeHookScope.Install("CreatePropertiesNativeFunction", nameof(CaptureCreateProperties));

        uint result = SDL3.SDL.CreateProperties();

        TestAssert.Equal<uint>(222, result, "SDL.CreateProperties must return native property id.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateProperties must call native hook once.");
    }

    public static void CopyProperties_ForwardsSourceDestinationAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CopyProperties");
        AssertLibraryImport(nativeMethod, "SDL_CopyProperties");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("CopyPropertiesNativeFunction", nameof(CaptureCopyProperties));

        bool result = SDL3.SDL.CopyProperties(10, 20);

        TestAssert.Equal(true, result, "SDL.CopyProperties must return native bool value.");
        TestAssert.Equal<uint>(10, capturedSrc, "SDL.CopyProperties must forward source properties.");
        TestAssert.Equal<uint>(20, capturedDst, "SDL.CopyProperties must forward destination properties.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CopyProperties must call native hook once.");
    }

    public static void LockProperties_ForwardsPropsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LockProperties");
        AssertLibraryImport(nativeMethod, "SDL_LockProperties");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("LockPropertiesNativeFunction", nameof(CapturePropsBool));

        bool result = SDL3.SDL.LockProperties(30);

        TestAssert.Equal(false, result, "SDL.LockProperties must return native bool value.");
        TestAssert.Equal<uint>(30, capturedProps, "SDL.LockProperties must forward properties id.");
        TestAssert.Equal(1, capturedCallCount, "SDL.LockProperties must call native hook once.");
    }

    public static void UnlockProperties_ForwardsProps()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UnlockProperties");
        AssertLibraryImport(nativeMethod, "SDL_UnlockProperties");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("UnlockPropertiesNativeFunction", nameof(CapturePropsVoid));

        SDL3.SDL.UnlockProperties(40);

        TestAssert.Equal<uint>(40, capturedProps, "SDL.UnlockProperties must forward properties id.");
        TestAssert.Equal(1, capturedCallCount, "SDL.UnlockProperties must call native hook once.");
    }

    public static void SetPointerPropertyWithCleanup_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetPointerPropertyWithCleanup");
        AssertLibraryImport(nativeMethod, "SDL_SetPointerPropertyWithCleanup");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertCallbackCdecl(typeof(SDL3.SDL.CleanupPropertyCallback), "SDL.CleanupPropertyCallback");

        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.CleanupPropertyCallback cleanup = HandleCleanup;
        using NativeHookScope _ = NativeHookScope.Install("SetPointerPropertyWithCleanupNativeFunction", nameof(CaptureSetPointerPropertyWithCleanup));

        bool result = SDL3.SDL.SetPointerPropertyWithCleanup(50, "ptr", (IntPtr)51, cleanup, (IntPtr)52);

        TestAssert.Equal(true, result, "SDL.SetPointerPropertyWithCleanup must return native bool value.");
        TestAssert.Equal<uint>(50, capturedProps, "SDL.SetPointerPropertyWithCleanup must forward properties id.");
        TestAssert.Equal("ptr", capturedName, "SDL.SetPointerPropertyWithCleanup must forward name.");
        TestAssert.Equal((IntPtr)51, capturedValuePointer, "SDL.SetPointerPropertyWithCleanup must forward value.");
        TestAssert.Equal(cleanup, capturedCleanup!, "SDL.SetPointerPropertyWithCleanup must forward cleanup callback.");
        TestAssert.Equal((IntPtr)52, capturedUserdata, "SDL.SetPointerPropertyWithCleanup must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetPointerPropertyWithCleanup must call native hook once.");
    }

    public static void SetPointerProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetPointerProperty");
        AssertLibraryImport(nativeMethod, "SDL_SetPointerProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("SetPointerPropertyNativeFunction", nameof(CaptureSetPointerProperty));

        bool result = SDL3.SDL.SetPointerProperty(60, "ptr", (IntPtr)61);

        TestAssert.Equal(false, result, "SDL.SetPointerProperty must return native bool value.");
        TestAssert.Equal<uint>(60, capturedProps, "SDL.SetPointerProperty must forward properties id.");
        TestAssert.Equal("ptr", capturedName, "SDL.SetPointerProperty must forward name.");
        TestAssert.Equal((IntPtr)61, capturedValuePointer, "SDL.SetPointerProperty must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetPointerProperty must call native hook once.");
    }

    public static void SetStringProperty_ForwardsNameValueAndNullAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetStringProperty");
        AssertLibraryImport(nativeMethod, "SDL_SetStringProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "value", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetStringPropertyNativeFunction", nameof(CaptureSetStringProperty));

        bool result = SDL3.SDL.SetStringProperty(70, "title", "game");

        TestAssert.Equal(true, result, "SDL.SetStringProperty must return native bool value.");
        TestAssert.Equal<uint>(70, capturedProps, "SDL.SetStringProperty must forward properties id.");
        TestAssert.Equal("title", capturedName, "SDL.SetStringProperty must forward name.");
        TestAssert.Equal("game", capturedStringValue, "SDL.SetStringProperty must forward string value.");

        nextBool = false;
        result = SDL3.SDL.SetStringProperty(71, "title", null);

        TestAssert.Equal(false, result, "SDL.SetStringProperty must return native false value.");
        TestAssert.Equal<uint>(71, capturedProps, "SDL.SetStringProperty must forward second properties id.");
        TestAssert.Equal<string?>(null, capturedStringValue, "SDL.SetStringProperty must forward null value.");
        TestAssert.Equal(2, capturedCallCount, "SDL.SetStringProperty must call native hook for both branches.");
    }

    public static void SetNumberProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetNumberProperty");
        AssertLibraryImport(nativeMethod, "SDL_SetNumberProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetNumberPropertyNativeFunction", nameof(CaptureSetNumberProperty));

        bool result = SDL3.SDL.SetNumberProperty(80, "score", 1234);

        TestAssert.Equal(true, result, "SDL.SetNumberProperty must return native bool value.");
        TestAssert.Equal<uint>(80, capturedProps, "SDL.SetNumberProperty must forward properties id.");
        TestAssert.Equal("score", capturedName, "SDL.SetNumberProperty must forward name.");
        TestAssert.Equal<long>(1234, capturedNumberValue, "SDL.SetNumberProperty must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetNumberProperty must call native hook once.");
    }

    public static void SetFloatProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetFloatProperty");
        AssertLibraryImport(nativeMethod, "SDL_SetFloatProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("SetFloatPropertyNativeFunction", nameof(CaptureSetFloatProperty));

        bool result = SDL3.SDL.SetFloatProperty(90, "gain", 1.5f);

        TestAssert.Equal(false, result, "SDL.SetFloatProperty must return native bool value.");
        TestAssert.Equal<uint>(90, capturedProps, "SDL.SetFloatProperty must forward properties id.");
        TestAssert.Equal("gain", capturedName, "SDL.SetFloatProperty must forward name.");
        TestAssert.Equal(1.5f, capturedFloatValue, "SDL.SetFloatProperty must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetFloatProperty must call native hook once.");
    }

    public static void SetBooleanProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetBooleanProperty");
        AssertLibraryImport(nativeMethod, "SDL_SetBooleanProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertBoolParameterMarshal(nativeMethod, "value", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetBooleanPropertyNativeFunction", nameof(CaptureSetBooleanProperty));

        bool result = SDL3.SDL.SetBooleanProperty(100, "enabled", true);

        TestAssert.Equal(true, result, "SDL.SetBooleanProperty must return native bool value.");
        TestAssert.Equal<uint>(100, capturedProps, "SDL.SetBooleanProperty must forward properties id.");
        TestAssert.Equal("enabled", capturedName, "SDL.SetBooleanProperty must forward name.");
        TestAssert.Equal(true, capturedBoolValue, "SDL.SetBooleanProperty must forward value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SetBooleanProperty must call native hook once.");
    }

    public static void HasProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_HasProperty");
        AssertLibraryImport(nativeMethod, "SDL_HasProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("HasPropertyNativeFunction", nameof(CaptureHasProperty));

        bool result = SDL3.SDL.HasProperty(110, "exists");

        TestAssert.Equal(false, result, "SDL.HasProperty must return native bool value.");
        TestAssert.Equal<uint>(110, capturedProps, "SDL.HasProperty must forward properties id.");
        TestAssert.Equal("exists", capturedName, "SDL.HasProperty must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HasProperty must call native hook once.");
    }

    public static void GetPropertyType_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPropertyType");
        AssertLibraryImport(nativeMethod, "SDL_GetPropertyType");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPropertyType = SDL3.SDL.PropertyType.String;
        using NativeHookScope _ = NativeHookScope.Install("GetPropertyTypeNativeFunction", nameof(CaptureGetPropertyType));

        SDL3.SDL.PropertyType result = SDL3.SDL.GetPropertyType(120, "kind");

        TestAssert.Equal(SDL3.SDL.PropertyType.String, result, "SDL.GetPropertyType must return native property type.");
        TestAssert.Equal<uint>(120, capturedProps, "SDL.GetPropertyType must forward properties id.");
        TestAssert.Equal("kind", capturedName, "SDL.GetPropertyType must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPropertyType must call native hook once.");
    }

    public static void GetPointerProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPointerProperty");
        AssertLibraryImport(nativeMethod, "SDL_GetPointerProperty");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)131;
        using NativeHookScope _ = NativeHookScope.Install("GetPointerPropertyNativeFunction", nameof(CaptureGetPointerProperty));

        IntPtr result = SDL3.SDL.GetPointerProperty(130, "ptr", (IntPtr)132);

        TestAssert.Equal((IntPtr)131, result, "SDL.GetPointerProperty must return native pointer.");
        TestAssert.Equal<uint>(130, capturedProps, "SDL.GetPointerProperty must forward properties id.");
        TestAssert.Equal("ptr", capturedName, "SDL.GetPointerProperty must forward name.");
        TestAssert.Equal((IntPtr)132, capturedDefaultPointer, "SDL.GetPointerProperty must forward default value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPointerProperty must call native hook once.");
    }

    public static void SDL_GetStringProperty_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetStringProperty");
        AssertLibraryImport(nativeMethod, "SDL_GetStringProperty");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "defaultValue", UnmanagedType.LPUTF8Str);
    }

    public static void GetStringProperty_ReturnsUtf8StringAndEmpty()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetStringPropertyNativeFunction", nameof(CaptureGetStringProperty));

        string value = CaptureUtf8String(() => SDL3.SDL.GetStringProperty(140, "title", "fallback"), "game");
        TestAssert.Equal("game", value, "SDL.GetStringProperty must convert UTF-8 native value.");
        TestAssert.Equal<uint>(140, capturedProps, "SDL.GetStringProperty must forward properties id.");
        TestAssert.Equal("title", capturedName, "SDL.GetStringProperty must forward name.");
        TestAssert.Equal("fallback", capturedDefaultString, "SDL.GetStringProperty must forward default value.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal("", SDL3.SDL.GetStringProperty(141, "missing", "fallback"), "SDL.GetStringProperty must return empty string for native null.");
        TestAssert.Equal<uint>(141, capturedProps, "SDL.GetStringProperty must forward second properties id.");
        TestAssert.Equal("missing", capturedName, "SDL.GetStringProperty must forward name for null branch.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetStringProperty must call native hook for both branches.");
    }

    public static void GetNumberProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetNumberProperty");
        AssertLibraryImport(nativeMethod, "SDL_GetNumberProperty");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextLong = 9876;
        using NativeHookScope _ = NativeHookScope.Install("GetNumberPropertyNativeFunction", nameof(CaptureGetNumberProperty));

        long result = SDL3.SDL.GetNumberProperty(150, "score", -1);

        TestAssert.Equal<long>(9876, result, "SDL.GetNumberProperty must return native number.");
        TestAssert.Equal<uint>(150, capturedProps, "SDL.GetNumberProperty must forward properties id.");
        TestAssert.Equal("score", capturedName, "SDL.GetNumberProperty must forward name.");
        TestAssert.Equal<long>(-1, capturedDefaultNumber, "SDL.GetNumberProperty must forward default value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetNumberProperty must call native hook once.");
    }

    public static void GetFloatProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetFloatProperty");
        AssertLibraryImport(nativeMethod, "SDL_GetFloatProperty");
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextFloat = 2.5f;
        using NativeHookScope _ = NativeHookScope.Install("GetFloatPropertyNativeFunction", nameof(CaptureGetFloatProperty));

        float result = SDL3.SDL.GetFloatProperty(160, "gain", 1.0f);

        TestAssert.Equal(2.5f, result, "SDL.GetFloatProperty must return native float.");
        TestAssert.Equal<uint>(160, capturedProps, "SDL.GetFloatProperty must forward properties id.");
        TestAssert.Equal("gain", capturedName, "SDL.GetFloatProperty must forward name.");
        TestAssert.Equal(1.0f, capturedDefaultFloat, "SDL.GetFloatProperty must forward default value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetFloatProperty must call native hook once.");
    }

    public static void GetBooleanProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetBooleanProperty");
        AssertLibraryImport(nativeMethod, "SDL_GetBooleanProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);
        AssertBoolParameterMarshal(nativeMethod, "defaultValue", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("GetBooleanPropertyNativeFunction", nameof(CaptureGetBooleanProperty));

        bool result = SDL3.SDL.GetBooleanProperty(170, "enabled", true);

        TestAssert.Equal(false, result, "SDL.GetBooleanProperty must return native bool value.");
        TestAssert.Equal<uint>(170, capturedProps, "SDL.GetBooleanProperty must forward properties id.");
        TestAssert.Equal("enabled", capturedName, "SDL.GetBooleanProperty must forward name.");
        TestAssert.Equal(true, capturedDefaultBool, "SDL.GetBooleanProperty must forward default value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetBooleanProperty must call native hook once.");
    }

    public static void ClearProperty_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ClearProperty");
        AssertLibraryImport(nativeMethod, "SDL_ClearProperty");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("ClearPropertyNativeFunction", nameof(CaptureClearProperty));

        bool result = SDL3.SDL.ClearProperty(180, "clear");

        TestAssert.Equal(true, result, "SDL.ClearProperty must return native bool value.");
        TestAssert.Equal<uint>(180, capturedProps, "SDL.ClearProperty must forward properties id.");
        TestAssert.Equal("clear", capturedName, "SDL.ClearProperty must forward name.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ClearProperty must call native hook once.");
    }

    public static void EnumerateProperties_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EnumerateProperties");
        AssertLibraryImport(nativeMethod, "SDL_EnumerateProperties");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertCallbackCdecl(typeof(SDL3.SDL.EnumeratePropertiesCallback), "SDL.EnumeratePropertiesCallback");
        AssertCallbackStringParameterMarshal(typeof(SDL3.SDL.EnumeratePropertiesCallback), "name", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.EnumeratePropertiesCallback callback = HandleEnumerate;
        using NativeHookScope _ = NativeHookScope.Install("EnumeratePropertiesNativeFunction", nameof(CaptureEnumerateProperties));

        bool result = SDL3.SDL.EnumerateProperties(190, callback, (IntPtr)191);

        TestAssert.Equal(false, result, "SDL.EnumerateProperties must return native bool value.");
        TestAssert.Equal<uint>(190, capturedProps, "SDL.EnumerateProperties must forward properties id.");
        TestAssert.Equal(callback, capturedEnumerateCallback!, "SDL.EnumerateProperties must forward callback.");
        TestAssert.Equal((IntPtr)191, capturedUserdata, "SDL.EnumerateProperties must forward userdata.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EnumerateProperties must call native hook once.");
    }

    public static void DestroyProperties_ForwardsProps()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyProperties");
        AssertLibraryImport(nativeMethod, "SDL_DestroyProperties");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("DestroyPropertiesNativeFunction", nameof(CaptureDestroyProperties));

        SDL3.SDL.DestroyProperties(200);

        TestAssert.Equal<uint>(200, capturedProps, "SDL.DestroyProperties must forward properties id.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DestroyProperties must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedProps = 0;
        capturedSrc = 0;
        capturedDst = 0;
        capturedName = null;
        capturedValuePointer = IntPtr.Zero;
        capturedCleanup = null;
        capturedEnumerateCallback = null;
        capturedUserdata = IntPtr.Zero;
        capturedStringValue = null;
        capturedNumberValue = 0;
        capturedFloatValue = 0;
        capturedBoolValue = false;
        capturedDefaultPointer = IntPtr.Zero;
        capturedDefaultString = null;
        capturedDefaultNumber = 0;
        capturedDefaultFloat = 0;
        capturedDefaultBool = false;
        nextUint = 0;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextPropertyType = default;
        nextLong = 0;
        nextFloat = 0;
        capturedCallCount = 0;
    }

    private static uint CaptureGetGlobalProperties()
    {
        capturedCallCount++;
        return nextUint;
    }

    private static uint CaptureCreateProperties()
    {
        capturedCallCount++;
        return nextUint;
    }

    private static bool CaptureCopyProperties(uint src, uint dst)
    {
        capturedSrc = src;
        capturedDst = dst;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CapturePropsBool(uint props)
    {
        capturedProps = props;
        capturedCallCount++;
        return nextBool;
    }

    private static void CapturePropsVoid(uint props)
    {
        capturedProps = props;
        capturedCallCount++;
    }

    private static bool CaptureSetPointerPropertyWithCleanup(uint props, string name, IntPtr value, SDL3.SDL.CleanupPropertyCallback cleanup, IntPtr userdata)
    {
        capturedProps = props;
        capturedName = name;
        capturedValuePointer = value;
        capturedCleanup = cleanup;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetPointerProperty(uint props, string name, IntPtr value)
    {
        capturedProps = props;
        capturedName = name;
        capturedValuePointer = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetStringProperty(uint props, string name, string? value)
    {
        capturedProps = props;
        capturedName = name;
        capturedStringValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetNumberProperty(uint props, string name, long value)
    {
        capturedProps = props;
        capturedName = name;
        capturedNumberValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetFloatProperty(uint props, string name, float value)
    {
        capturedProps = props;
        capturedName = name;
        capturedFloatValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSetBooleanProperty(uint props, string name, bool value)
    {
        capturedProps = props;
        capturedName = name;
        capturedBoolValue = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureHasProperty(uint props, string name)
    {
        capturedProps = props;
        capturedName = name;
        capturedCallCount++;
        return nextBool;
    }

    private static SDL3.SDL.PropertyType CaptureGetPropertyType(uint props, string name)
    {
        capturedProps = props;
        capturedName = name;
        capturedCallCount++;
        return nextPropertyType;
    }

    private static IntPtr CaptureGetPointerProperty(uint props, string name, IntPtr defaultValue)
    {
        capturedProps = props;
        capturedName = name;
        capturedDefaultPointer = defaultValue;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGetStringProperty(uint props, string name, string defaultValue)
    {
        capturedProps = props;
        capturedName = name;
        capturedDefaultString = defaultValue;
        capturedCallCount++;
        return nextPointer;
    }

    private static long CaptureGetNumberProperty(uint props, string name, long defaultValue)
    {
        capturedProps = props;
        capturedName = name;
        capturedDefaultNumber = defaultValue;
        capturedCallCount++;
        return nextLong;
    }

    private static float CaptureGetFloatProperty(uint props, string name, float defaultValue)
    {
        capturedProps = props;
        capturedName = name;
        capturedDefaultFloat = defaultValue;
        capturedCallCount++;
        return nextFloat;
    }

    private static bool CaptureGetBooleanProperty(uint props, string name, bool defaultValue)
    {
        capturedProps = props;
        capturedName = name;
        capturedDefaultBool = defaultValue;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureClearProperty(uint props, string name)
    {
        capturedProps = props;
        capturedName = name;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureEnumerateProperties(uint props, SDL3.SDL.EnumeratePropertiesCallback callback, IntPtr userdata)
    {
        capturedProps = props;
        capturedEnumerateCallback = callback;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureDestroyProperties(uint props)
    {
        capturedProps = props;
        capturedCallCount++;
    }

    private static void HandleCleanup(IntPtr userdata, IntPtr value)
    {
    }

    private static void HandleEnumerate(IntPtr userdata, uint props, string name)
    {
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

    private static void AssertLibraryImport(MethodInfo method, string entryPoint)
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

    private static void AssertCallbackCdecl(Type callbackType, string callbackName)
    {
        UnmanagedFunctionPointerAttribute? callbackAttribute = callbackType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(callbackAttribute, $"{callbackName} must keep unmanaged callback metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, callbackAttribute!.CallingConvention, $"{callbackName} must use cdecl calling convention.");
    }

    private static void AssertCallbackStringParameterMarshal(Type callbackType, string parameterName, UnmanagedType unmanagedType)
    {
        MethodInfo? invoke = callbackType.GetMethod("Invoke");
        TestAssert.NotNull(invoke, $"{callbackType.Name}.Invoke must exist.");
        ParameterInfo parameter = invoke!.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"{callbackType.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"{callbackType.Name} parameter {parameterName} must use expected string marshalling.");
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
