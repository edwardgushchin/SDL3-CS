using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Tray;

internal static class PInvokeTests
{
    private static IntPtr capturedTray;
    private static IntPtr capturedIcon;
    private static IntPtr capturedEntry;
    private static IntPtr capturedMenu;
    private static IntPtr capturedUserdata;
    private static IntPtr nextPointer;
    private static IntPtr nextEntriesPointer;
    private static string? capturedTooltip;
    private static string? capturedLabel;
    private static uint capturedProperties;
    private static int capturedPosition;
    private static int nextEntriesCount;
    private static bool capturedBoolean;
    private static int capturedCallCount;
    private static SDL3.SDL.TrayEntryFlags capturedFlags;
    private static SDL3.SDL.TrayCallback? capturedCallback;

    public static void CreateTray_ForwardsIconAndTooltip()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateTray");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateTray");
        AssertStringParameterMarshal(nativeMethod, "tooltip");

        using NativeHookScope _ = NativeHookScope.Install("CreateTrayNativeFunction", nameof(CaptureCreateTray));
        IntPtr result = SDL3.SDL.CreateTray((IntPtr)201, "tip");

        TestAssert.Equal((IntPtr)301, result, "SDL.CreateTray must return the native hook pointer.");
        TestAssert.Equal((IntPtr)201, capturedIcon, "SDL.CreateTray must forward icon.");
        TestAssert.Equal("tip", capturedTooltip, "SDL.CreateTray must forward tooltip.");
    }

    public static void CreateTrayWithProperties_ForwardsProperties()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateTrayWithProperties");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateTrayWithProperties");

        using NativeHookScope _ = NativeHookScope.Install("CreateTrayWithPropertiesNativeFunction", nameof(CaptureCreateTrayWithProperties));
        IntPtr result = SDL3.SDL.CreateTrayWithProperties(202);

        TestAssert.Equal((IntPtr)302, result, "SDL.CreateTrayWithProperties must return the native hook pointer.");
        TestAssert.Equal(202u, capturedProperties, "SDL.CreateTrayWithProperties must forward props.");
    }

    public static void SetTrayIcon_ForwardsTrayAndIcon()
    {
        AssertVoidTwoPointerForwarder("SDL_SetTrayIcon", "SDL_SetTrayIcon", "SetTrayIconNativeFunction", (tray, icon) => SDL3.SDL.SetTrayIcon(tray, icon));
    }

    public static void SetTrayTooltip_ForwardsTrayAndTooltip()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTrayTooltip");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetTrayTooltip");
        AssertStringParameterMarshal(nativeMethod, "tooltip");

        using NativeHookScope _ = NativeHookScope.Install("SetTrayTooltipNativeFunction", nameof(CaptureSetTrayTooltip));
        SDL3.SDL.SetTrayTooltip((IntPtr)203, "tooltip");

        TestAssert.Equal((IntPtr)203, capturedTray, "SDL.SetTrayTooltip must forward tray.");
        TestAssert.Equal("tooltip", capturedTooltip, "SDL.SetTrayTooltip must forward tooltip.");
    }

    public static void CreateTrayMenu_ForwardsTray()
    {
        AssertPointerReturnForwarder("SDL_CreateTrayMenu", "SDL_CreateTrayMenu", "CreateTrayMenuNativeFunction", SDL3.SDL.CreateTrayMenu, (IntPtr)304);
    }

    public static void CreateTraySubmenu_ForwardsEntry()
    {
        AssertEntryPointerReturnForwarder("SDL_CreateTraySubmenu", "SDL_CreateTraySubmenu", "CreateTraySubmenuNativeFunction", SDL3.SDL.CreateTraySubmenu, (IntPtr)305);
    }

    public static void GetTrayMenu_ForwardsTray()
    {
        AssertPointerReturnForwarder("SDL_GetTrayMenu", "SDL_GetTrayMenu", "GetTrayMenuNativeFunction", SDL3.SDL.GetTrayMenu, (IntPtr)306);
    }

    public static void GetTraySubmenu_ForwardsEntry()
    {
        AssertEntryPointerReturnForwarder("SDL_GetTraySubmenu", "SDL_GetTraySubmenu", "GetTraySubmenuNativeFunction", SDL3.SDL.GetTraySubmenu, (IntPtr)307);
    }

    public static void SDL_GetTrayEntries_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTrayEntries");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetTrayEntries");
    }

    public static void GetTrayEntries_ReturnsPointerArrayAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetTrayEntriesNativeFunction", nameof(CaptureGetTrayEntries));

        nextEntriesPointer = CreateNativePointerArray((IntPtr)308, (IntPtr)309);
        nextEntriesCount = 2;
        IntPtr[]? entries = SDL3.SDL.GetTrayEntries((IntPtr)204, out int size);

        TestAssert.NotNull(entries, "SDL.GetTrayEntries must return entries from native pointer array.");
        TestAssert.Equal(2, size, "SDL.GetTrayEntries must forward count.");
        TestAssert.Equal((IntPtr)308, entries![0], "SDL.GetTrayEntries must copy entry 0.");
        TestAssert.Equal((IntPtr)309, entries[1], "SDL.GetTrayEntries must copy entry 1.");
        TestAssert.Equal((IntPtr)204, capturedMenu, "SDL.GetTrayEntries must forward menu.");

        nextEntriesPointer = IntPtr.Zero;
        nextEntriesCount = 0;
        TestAssert.Equal<IntPtr[]?>(null, SDL3.SDL.GetTrayEntries((IntPtr)204, out size), "SDL.GetTrayEntries must return null for native null.");
        TestAssert.Equal(0, size, "SDL.GetTrayEntries must return native count for null pointer.");
    }

    public static void RemoveTrayEntry_ForwardsEntry()
    {
        AssertVoidEntryForwarder("SDL_RemoveTrayEntry", "SDL_RemoveTrayEntry", "RemoveTrayEntryNativeFunction", SDL3.SDL.RemoveTrayEntry);
    }

    public static void InsertTrayEntryAt_ForwardsMenuPositionLabelAndFlags()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_InsertTrayEntryAt");
        AssertSdlLibraryImport(nativeMethod, "SDL_InsertTrayEntryAt");
        AssertStringParameterMarshal(nativeMethod, "label");

        using NativeHookScope _ = NativeHookScope.Install("InsertTrayEntryAtNativeFunction", nameof(CaptureInsertTrayEntryAt));
        IntPtr result = SDL3.SDL.InsertTrayEntryAt((IntPtr)205, 2, "entry", SDL3.SDL.TrayEntryFlags.Button | SDL3.SDL.TrayEntryFlags.Disabled);

        TestAssert.Equal((IntPtr)310, result, "SDL.InsertTrayEntryAt must return the native hook pointer.");
        TestAssert.Equal((IntPtr)205, capturedMenu, "SDL.InsertTrayEntryAt must forward menu.");
        TestAssert.Equal(2, capturedPosition, "SDL.InsertTrayEntryAt must forward position.");
        TestAssert.Equal("entry", capturedLabel, "SDL.InsertTrayEntryAt must forward label.");
        TestAssert.Equal(SDL3.SDL.TrayEntryFlags.Button | SDL3.SDL.TrayEntryFlags.Disabled, capturedFlags, "SDL.InsertTrayEntryAt must forward flags.");
    }

    public static void SetTrayEntryLabel_ForwardsEntryAndLabel()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTrayEntryLabel");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetTrayEntryLabel");
        AssertStringParameterMarshal(nativeMethod, "label");

        using NativeHookScope _ = NativeHookScope.Install("SetTrayEntryLabelNativeFunction", nameof(CaptureSetTrayEntryLabel));
        SDL3.SDL.SetTrayEntryLabel((IntPtr)206, "label");

        TestAssert.Equal((IntPtr)206, capturedEntry, "SDL.SetTrayEntryLabel must forward entry.");
        TestAssert.Equal("label", capturedLabel, "SDL.SetTrayEntryLabel must forward label.");
    }

    public static void SDL_GetTrayEntryLabel_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTrayEntryLabel");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetTrayEntryLabel");
    }

    public static void GetTrayEntryLabel_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetTrayEntryLabelNativeFunction", nameof(CaptureGetTrayEntryLabel));

        string? value = CaptureUtf8String(() => SDL3.SDL.GetTrayEntryLabel((IntPtr)207), "Label");
        TestAssert.Equal("Label", value, "SDL.GetTrayEntryLabel must convert UTF-8 native label.");
        TestAssert.Equal((IntPtr)207, capturedEntry, "SDL.GetTrayEntryLabel must forward entry.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetTrayEntryLabel((IntPtr)207), "SDL.GetTrayEntryLabel must return null for native null.");
    }

    public static void SetTrayEntryChecked_ForwardsEntryAndChecked()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTrayEntryChecked");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetTrayEntryChecked");
        AssertBoolParameterMarshal(nativeMethod, "checked");

        using NativeHookScope _ = NativeHookScope.Install("SetTrayEntryCheckedNativeFunction", nameof(CaptureSetTrayEntryChecked));
        SDL3.SDL.SetTrayEntryChecked((IntPtr)208, true);

        TestAssert.Equal((IntPtr)208, capturedEntry, "SDL.SetTrayEntryChecked must forward entry.");
        TestAssert.Equal(true, capturedBoolean, "SDL.SetTrayEntryChecked must forward checked.");
    }

    public static void GetTrayEntryChecked_ReturnsNativeValue()
    {
        AssertBoolEntryForwarder("SDL_GetTrayEntryChecked", "SDL_GetTrayEntryChecked", "GetTrayEntryCheckedNativeFunction", SDL3.SDL.GetTrayEntryChecked);
    }

    public static void SetTrayEntryEnabled_ForwardsEntryAndEnabled()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTrayEntryEnabled");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetTrayEntryEnabled");
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        using NativeHookScope _ = NativeHookScope.Install("SetTrayEntryEnabledNativeFunction", nameof(CaptureSetTrayEntryEnabled));
        SDL3.SDL.SetTrayEntryEnabled((IntPtr)209, true);

        TestAssert.Equal((IntPtr)209, capturedEntry, "SDL.SetTrayEntryEnabled must forward entry.");
        TestAssert.Equal(true, capturedBoolean, "SDL.SetTrayEntryEnabled must forward enabled.");
    }

    public static void GetTrayEntryEnabled_ReturnsNativeValue()
    {
        AssertBoolEntryForwarder("SDL_GetTrayEntryEnabled", "SDL_GetTrayEntryEnabled", "GetTrayEntryEnabledNativeFunction", SDL3.SDL.GetTrayEntryEnabled);
    }

    public static void SetTrayEntryCallback_ForwardsEntryCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetTrayEntryCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetTrayEntryCallback");

        SDL3.SDL.TrayCallback callback = TestTrayCallback;
        using NativeHookScope _ = NativeHookScope.Install("SetTrayEntryCallbackNativeFunction", nameof(CaptureSetTrayEntryCallback));
        SDL3.SDL.SetTrayEntryCallback((IntPtr)210, callback, (IntPtr)211);

        TestAssert.Equal((IntPtr)210, capturedEntry, "SDL.SetTrayEntryCallback must forward entry.");
        TestAssert.Equal((IntPtr)211, capturedUserdata, "SDL.SetTrayEntryCallback must forward userdata.");
        TestAssert.NotNull(capturedCallback, "SDL.SetTrayEntryCallback must forward callback.");
    }

    public static void ClickTrayEntry_ForwardsEntry()
    {
        AssertVoidEntryForwarder("SDL_ClickTrayEntry", "SDL_ClickTrayEntry", "ClickTrayEntryNativeFunction", SDL3.SDL.ClickTrayEntry);
    }

    public static void DestroyTray_ForwardsTray()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyTray");
        AssertSdlLibraryImport(nativeMethod, "SDL_DestroyTray");

        using NativeHookScope _ = NativeHookScope.Install("DestroyTrayNativeFunction", nameof(CaptureDestroyTray));
        SDL3.SDL.DestroyTray((IntPtr)212);

        TestAssert.Equal((IntPtr)212, capturedTray, "SDL.DestroyTray must forward tray.");
    }

    public static void GetTrayEntryParent_ForwardsEntry()
    {
        AssertEntryPointerReturnForwarder("SDL_GetTrayEntryParent", "SDL_GetTrayEntryParent", "GetTrayEntryParentNativeFunction", SDL3.SDL.GetTrayEntryParent, (IntPtr)311);
    }

    public static void GetTrayMenuParentEntry_ForwardsMenu()
    {
        AssertMenuPointerReturnForwarder("SDL_GetTrayMenuParentEntry", "SDL_GetTrayMenuParentEntry", "GetTrayMenuParentEntryNativeFunction", SDL3.SDL.GetTrayMenuParentEntry, (IntPtr)312);
    }

    public static void GetTrayMenuParentTray_ForwardsMenu()
    {
        AssertMenuPointerReturnForwarder("SDL_GetTrayMenuParentTray", "SDL_GetTrayMenuParentTray", "GetTrayMenuParentTrayNativeFunction", SDL3.SDL.GetTrayMenuParentTray, (IntPtr)313);
    }

    public static void UpdateTrays_ForwardsCall()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UpdateTrays");
        AssertSdlLibraryImport(nativeMethod, "SDL_UpdateTrays");

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("UpdateTraysNativeFunction", nameof(CaptureVoidCall));
        SDL3.SDL.UpdateTrays();

        TestAssert.Equal(1, capturedCallCount, "SDL.UpdateTrays must call the native hook once.");
    }

    private static bool CaptureTrue()
    {
        return true;
    }

    private static IntPtr CaptureCreateTray(IntPtr icon, string? tooltip)
    {
        capturedIcon = icon;
        capturedTooltip = tooltip;
        return (IntPtr)301;
    }

    private static IntPtr CaptureCreateTrayWithProperties(uint props)
    {
        capturedProperties = props;
        return (IntPtr)302;
    }

    private static void CaptureSetTrayIcon(IntPtr tray, IntPtr icon)
    {
        capturedTray = tray;
        capturedIcon = icon;
    }

    private static void CaptureSetTrayTooltip(IntPtr tray, string? tooltip)
    {
        capturedTray = tray;
        capturedTooltip = tooltip;
    }

    private static IntPtr CaptureTrayPointer(IntPtr tray)
    {
        capturedTray = tray;
        return nextPointer;
    }

    private static IntPtr CaptureEntryPointer(IntPtr entry)
    {
        capturedEntry = entry;
        return nextPointer;
    }

    private static IntPtr CaptureMenuPointer(IntPtr menu)
    {
        capturedMenu = menu;
        return nextPointer;
    }

    private static IntPtr CaptureGetTrayEntries(IntPtr menu, out int count)
    {
        capturedMenu = menu;
        count = nextEntriesCount;
        return nextEntriesPointer;
    }

    private static void CaptureEntryCall(IntPtr entry)
    {
        capturedEntry = entry;
        capturedCallCount++;
    }

    private static IntPtr CaptureInsertTrayEntryAt(IntPtr menu, int pos, string? label, SDL3.SDL.TrayEntryFlags flags)
    {
        capturedMenu = menu;
        capturedPosition = pos;
        capturedLabel = label;
        capturedFlags = flags;
        return (IntPtr)310;
    }

    private static void CaptureSetTrayEntryLabel(IntPtr entry, string label)
    {
        capturedEntry = entry;
        capturedLabel = label;
    }

    private static IntPtr CaptureGetTrayEntryLabel(IntPtr entry)
    {
        capturedEntry = entry;
        return nextPointer;
    }

    private static void CaptureSetTrayEntryChecked(IntPtr entry, bool @checked)
    {
        capturedEntry = entry;
        capturedBoolean = @checked;
    }

    private static bool CaptureGetTrayEntryChecked(IntPtr entry)
    {
        capturedEntry = entry;
        return true;
    }

    private static void CaptureSetTrayEntryEnabled(IntPtr entry, bool enabled)
    {
        capturedEntry = entry;
        capturedBoolean = enabled;
    }

    private static bool CaptureGetTrayEntryEnabled(IntPtr entry)
    {
        capturedEntry = entry;
        return true;
    }

    private static void CaptureSetTrayEntryCallback(IntPtr entry, SDL3.SDL.TrayCallback callback, IntPtr userdata)
    {
        capturedEntry = entry;
        capturedCallback = callback;
        capturedUserdata = userdata;
    }

    private static void CaptureDestroyTray(IntPtr tray)
    {
        capturedTray = tray;
    }

    private static void CaptureVoidCall()
    {
        capturedCallCount++;
    }

    private static void TestTrayCallback(IntPtr userdata, IntPtr entry)
    {
    }

    private static IntPtr CreateNativePointerArray(params IntPtr[] values)
    {
        IntPtr pointer = SDL3.SDL.Malloc((UIntPtr)(IntPtr.Size * values.Length));
        TestAssert.True(pointer != IntPtr.Zero, "Test pointer array allocation must succeed.");
        Marshal.Copy(values, 0, pointer, values.Length);
        return pointer;
    }

    private static string? CaptureUtf8String(Func<string?> action, string value)
    {
        nextPointer = Marshal.StringToCoTaskMemUTF8(value);

        try
        {
            return action();
        }
        finally
        {
            Marshal.FreeCoTaskMem(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    private static void AssertBoolNoArgForwarder(string methodName, string entryPoint, string hookFieldName, Func<bool> action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureTrue));
        bool result = action();

        TestAssert.Equal(true, result, $"SDL.{methodName} public wrapper must return the native hook value.");
    }

    private static void AssertPointerReturnForwarder(string methodName, string entryPoint, string hookFieldName, Func<IntPtr, IntPtr> action, IntPtr expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);

        nextPointer = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureTrayPointer));
        IntPtr result = action((IntPtr)213);

        TestAssert.Equal(expected, result, $"SDL.{methodName} public wrapper must return the native hook pointer.");
        TestAssert.Equal((IntPtr)213, capturedTray, $"SDL.{methodName} public wrapper must forward tray.");
    }

    private static void AssertEntryPointerReturnForwarder(string methodName, string entryPoint, string hookFieldName, Func<IntPtr, IntPtr> action, IntPtr expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);

        nextPointer = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureEntryPointer));
        IntPtr result = action((IntPtr)214);

        TestAssert.Equal(expected, result, $"SDL.{methodName} public wrapper must return the native hook pointer.");
        TestAssert.Equal((IntPtr)214, capturedEntry, $"SDL.{methodName} public wrapper must forward entry.");
    }

    private static void AssertMenuPointerReturnForwarder(string methodName, string entryPoint, string hookFieldName, Func<IntPtr, IntPtr> action, IntPtr expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);

        nextPointer = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureMenuPointer));
        IntPtr result = action((IntPtr)215);

        TestAssert.Equal(expected, result, $"SDL.{methodName} public wrapper must return the native hook pointer.");
        TestAssert.Equal((IntPtr)215, capturedMenu, $"SDL.{methodName} public wrapper must forward menu.");
    }

    private static void AssertVoidTwoPointerForwarder(string methodName, string entryPoint, string hookFieldName, Action<IntPtr, IntPtr> action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureSetTrayIcon));
        action((IntPtr)216, (IntPtr)217);

        TestAssert.Equal((IntPtr)216, capturedTray, $"SDL.{methodName} public wrapper must forward tray.");
        TestAssert.Equal((IntPtr)217, capturedIcon, $"SDL.{methodName} public wrapper must forward icon.");
    }

    private static void AssertVoidEntryForwarder(string methodName, string entryPoint, string hookFieldName, Action<IntPtr> action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureEntryCall));
        action((IntPtr)218);

        TestAssert.Equal((IntPtr)218, capturedEntry, $"SDL.{methodName} public wrapper must forward entry.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{methodName} public wrapper must call native hook once.");
    }

    private static void AssertBoolEntryForwarder(string methodName, string entryPoint, string hookFieldName, Func<IntPtr, bool> action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);
        AssertBoolReturnMarshal(nativeMethod);

        string hookMethod = methodName.Contains("Checked", StringComparison.Ordinal) ? nameof(CaptureGetTrayEntryChecked) : nameof(CaptureGetTrayEntryEnabled);
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethod);
        bool result = action((IntPtr)219);

        TestAssert.Equal(true, result, $"SDL.{methodName} public wrapper must return the native hook value.");
        TestAssert.Equal((IntPtr)219, capturedEntry, $"SDL.{methodName} public wrapper must forward entry.");
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
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 string marshalling.");
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
