using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Dialog;

internal static class PInvokeTests
{
    private static DialogCapture? openFileDialogCapture;
    private static DialogCapture? saveFileDialogCapture;
    private static DialogCapture? openFolderDialogCapture;
    private static DialogCapture? fileDialogWithPropertiesCapture;
    private static readonly SDL3.SDL.DialogFileCallback NoopDialogFileCallbackDelegate = NoopDialogFileCallback;

    public static void SDL_ShowOpenFileDialog_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_ShowOpenFileDialog", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_ShowOpenFileDialog method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_ShowOpenFileDialog");
        AssertDialogCallbackParameter(method!, "callback");
        AssertBooleanParameterMarshal(method!, "allowMany");
    }

    public static void ShowOpenFileDialog_PinsFiltersAndConvertsDefaultLocation()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.ShowOpenFileDialog), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.ShowOpenFileDialog method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("ShowOpenFileDialogNativeFunction", nameof(CaptureShowOpenFileDialog));
        SDL3.SDL.DialogFileCallback callback = NoopDialogFileCallbackDelegate;
        callback(IntPtr.Zero, IntPtr.Zero, -1);
        SDL3.SDL.DialogFileFilter[] filters = [new("Text files", "txt;md")];

        try
        {
            SDL3.SDL.ShowOpenFileDialog(callback, (IntPtr)123, (IntPtr)456, filters, filters.Length, "C:/Temp", allowMany: true);
            TestAssert.NotNull(openFileDialogCapture, "SDL.ShowOpenFileDialog must call the native hook.");
            TestAssert.Equal((IntPtr)123, openFileDialogCapture!.Userdata, "SDL.ShowOpenFileDialog must forward userdata.");
            TestAssert.Equal((IntPtr)456, openFileDialogCapture.Window, "SDL.ShowOpenFileDialog must forward the window pointer.");
            TestAssert.Equal(1, openFileDialogCapture.NFilters, "SDL.ShowOpenFileDialog must forward the filter count.");
            TestAssert.Equal("Text files", openFileDialogCapture.FilterName, "SDL.ShowOpenFileDialog must pin and forward the filter name.");
            TestAssert.Equal("txt;md", openFileDialogCapture.FilterPattern, "SDL.ShowOpenFileDialog must pin and forward the filter pattern.");
            TestAssert.Equal("C:/Temp", openFileDialogCapture.DefaultLocation, "SDL.ShowOpenFileDialog must convert defaultLocation to UTF-8.");
            TestAssert.Equal(true, openFileDialogCapture.AllowMany, "SDL.ShowOpenFileDialog must forward allowMany.");

            SDL3.SDL.ShowOpenFileDialog(callback, IntPtr.Zero, IntPtr.Zero, null, 0, null, allowMany: false);
            TestAssert.Equal(IntPtr.Zero, openFileDialogCapture.Filters, "SDL.ShowOpenFileDialog must forward null filters as IntPtr.Zero.");
            TestAssert.Equal<string?>(null, openFileDialogCapture.DefaultLocation, "SDL.ShowOpenFileDialog must forward null defaultLocation as IntPtr.Zero.");
            TestAssert.Equal(false, openFileDialogCapture.AllowMany, "SDL.ShowOpenFileDialog must forward allowMany false.");
        }
        finally
        {
            foreach (SDL3.SDL.DialogFileFilter filter in filters)
            {
                filter.Dispose();
            }
        }

        GC.KeepAlive(callback);
    }

    public static void SDL_ShowSaveFileDialog_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_ShowSaveFileDialog", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_ShowSaveFileDialog method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_ShowSaveFileDialog");
        AssertDialogCallbackParameter(method!, "callback");
    }

    public static void ShowSaveFileDialog_PinsFiltersAndConvertsDefaultLocation()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.ShowSaveFileDialog), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.ShowSaveFileDialog method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("ShowSaveFileDialogNativeFunction", nameof(CaptureShowSaveFileDialog));
        SDL3.SDL.DialogFileCallback callback = NoopDialogFileCallbackDelegate;
        callback(IntPtr.Zero, IntPtr.Zero, -1);
        SDL3.SDL.DialogFileFilter[] filters = [new("Images", "png;jpg")];

        try
        {
            SDL3.SDL.ShowSaveFileDialog(callback, (IntPtr)222, (IntPtr)333, filters, filters.Length, "C:/Save/file.txt");
            TestAssert.NotNull(saveFileDialogCapture, "SDL.ShowSaveFileDialog must call the native hook.");
            TestAssert.Equal((IntPtr)222, saveFileDialogCapture!.Userdata, "SDL.ShowSaveFileDialog must forward userdata.");
            TestAssert.Equal((IntPtr)333, saveFileDialogCapture.Window, "SDL.ShowSaveFileDialog must forward the window pointer.");
            TestAssert.Equal(1, saveFileDialogCapture.NFilters, "SDL.ShowSaveFileDialog must forward the filter count.");
            TestAssert.Equal("Images", saveFileDialogCapture.FilterName, "SDL.ShowSaveFileDialog must pin and forward the filter name.");
            TestAssert.Equal("png;jpg", saveFileDialogCapture.FilterPattern, "SDL.ShowSaveFileDialog must pin and forward the filter pattern.");
            TestAssert.Equal("C:/Save/file.txt", saveFileDialogCapture.DefaultLocation, "SDL.ShowSaveFileDialog must convert defaultLocation to UTF-8.");

            SDL3.SDL.ShowSaveFileDialog(callback, IntPtr.Zero, IntPtr.Zero, null, 0, null);
            TestAssert.Equal(IntPtr.Zero, saveFileDialogCapture.Filters, "SDL.ShowSaveFileDialog must forward null filters as IntPtr.Zero.");
            TestAssert.Equal<string?>(null, saveFileDialogCapture.DefaultLocation, "SDL.ShowSaveFileDialog must forward null defaultLocation as IntPtr.Zero.");
        }
        finally
        {
            foreach (SDL3.SDL.DialogFileFilter filter in filters)
            {
                filter.Dispose();
            }
        }

        GC.KeepAlive(callback);
    }

    public static void SDL_ShowOpenFolderDialog_UsesExpectedNativeMetadata()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_ShowOpenFolderDialog", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_ShowOpenFolderDialog method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_ShowOpenFolderDialog");
        AssertDialogCallbackParameter(method!, "callback");
        AssertBooleanParameterMarshal(method!, "allowMany");
    }

    public static void ShowOpenFolderDialog_ConvertsDefaultLocation()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.ShowOpenFolderDialog), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.ShowOpenFolderDialog method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("ShowOpenFolderDialogNativeFunction", nameof(CaptureShowOpenFolderDialog));
        SDL3.SDL.DialogFileCallback callback = NoopDialogFileCallbackDelegate;
        callback(IntPtr.Zero, IntPtr.Zero, -1);

        SDL3.SDL.ShowOpenFolderDialog(callback, (IntPtr)444, (IntPtr)555, "C:/Folder", allowMany: true);
        TestAssert.NotNull(openFolderDialogCapture, "SDL.ShowOpenFolderDialog must call the native hook.");
        TestAssert.Equal((IntPtr)444, openFolderDialogCapture!.Userdata, "SDL.ShowOpenFolderDialog must forward userdata.");
        TestAssert.Equal((IntPtr)555, openFolderDialogCapture.Window, "SDL.ShowOpenFolderDialog must forward the window pointer.");
        TestAssert.Equal("C:/Folder", openFolderDialogCapture.DefaultLocation, "SDL.ShowOpenFolderDialog must convert defaultLocation to UTF-8.");
        TestAssert.Equal(true, openFolderDialogCapture.AllowMany, "SDL.ShowOpenFolderDialog must forward allowMany.");

        SDL3.SDL.ShowOpenFolderDialog(callback, IntPtr.Zero, IntPtr.Zero, null, allowMany: false);
        TestAssert.Equal<string?>(null, openFolderDialogCapture.DefaultLocation, "SDL.ShowOpenFolderDialog must forward null defaultLocation as IntPtr.Zero.");
        TestAssert.Equal(false, openFolderDialogCapture.AllowMany, "SDL.ShowOpenFolderDialog must forward allowMany false.");
        GC.KeepAlive(callback);
    }

    public static void ShowFileDialogWithProperties_ForwardsTypeUserdataAndProperties()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_ShowFileDialogWithProperties", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_ShowFileDialogWithProperties method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_ShowFileDialogWithProperties");
        AssertDialogCallbackParameter(nativeMethod!, "callback");

        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.ShowFileDialogWithProperties), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.ShowFileDialogWithProperties method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("ShowFileDialogWithPropertiesNativeFunction", nameof(CaptureShowFileDialogWithProperties));
        SDL3.SDL.DialogFileCallback callback = NoopDialogFileCallbackDelegate;
        callback(IntPtr.Zero, IntPtr.Zero, -1);

        SDL3.SDL.ShowFileDialogWithProperties(SDL3.SDL.FileDialogType.SaveFile, callback, (IntPtr)777, 888);
        TestAssert.NotNull(fileDialogWithPropertiesCapture, "SDL.ShowFileDialogWithProperties must call the native hook.");
        TestAssert.Equal(SDL3.SDL.FileDialogType.SaveFile, fileDialogWithPropertiesCapture!.DialogType, "SDL.ShowFileDialogWithProperties must forward the dialog type.");
        TestAssert.Equal((IntPtr)777, fileDialogWithPropertiesCapture.Userdata, "SDL.ShowFileDialogWithProperties must forward userdata.");
        TestAssert.Equal(888u, fileDialogWithPropertiesCapture.Properties, "SDL.ShowFileDialogWithProperties must forward properties.");
        GC.KeepAlive(callback);
    }

    private static void CaptureShowOpenFileDialog(SDL3.SDL.DialogFileCallback callback, IntPtr userdata, IntPtr window, IntPtr filters, int nfilters, IntPtr defaultLocation, bool allowMany)
    {
        openFileDialogCapture = CreateDialogCapture(callback, userdata, window, filters, nfilters, defaultLocation, allowMany);
    }

    private static void CaptureShowSaveFileDialog(SDL3.SDL.DialogFileCallback callback, IntPtr userdata, IntPtr window, IntPtr filters, int nfilters, IntPtr defaultLocation)
    {
        saveFileDialogCapture = CreateDialogCapture(callback, userdata, window, filters, nfilters, defaultLocation, allowMany: false);
    }

    private static void CaptureShowOpenFolderDialog(SDL3.SDL.DialogFileCallback callback, IntPtr userdata, IntPtr window, IntPtr defaultLocation, bool allowMany)
    {
        openFolderDialogCapture = CreateDialogCapture(callback, userdata, window, IntPtr.Zero, 0, defaultLocation, allowMany);
    }

    private static void CaptureShowFileDialogWithProperties(SDL3.SDL.FileDialogType type, SDL3.SDL.DialogFileCallback callback, IntPtr userdata, uint props)
    {
        fileDialogWithPropertiesCapture = new DialogCapture
        {
            Callback = callback,
            Userdata = userdata,
            DialogType = type,
            Properties = props
        };
    }

    private static DialogCapture CreateDialogCapture(SDL3.SDL.DialogFileCallback callback, IntPtr userdata, IntPtr window, IntPtr filters, int nfilters, IntPtr defaultLocation, bool allowMany)
    {
        DialogCapture capture = new()
        {
            Callback = callback,
            Userdata = userdata,
            Window = window,
            Filters = filters,
            NFilters = nfilters,
            DefaultLocation = ReadUtf8OrNull(defaultLocation),
            AllowMany = allowMany
        };

        if (filters != IntPtr.Zero)
        {
            SDL3.SDL.DialogFileFilter filter = Marshal.PtrToStructure<SDL3.SDL.DialogFileFilter>(filters);
            capture.FilterName = Marshal.PtrToStringUTF8(filter.Name);
            capture.FilterPattern = Marshal.PtrToStringUTF8(filter.Pattern);
        }

        return capture;
    }

    private static string? ReadUtf8OrNull(IntPtr value)
    {
        if (value == IntPtr.Zero)
        {
            return null;
        }

        return Marshal.PtrToStringUTF8(value);
    }

    private static void NoopDialogFileCallback(IntPtr userdata, IntPtr filelist, int filter)
    {
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBooleanParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertDialogCallbackParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.Equal(typeof(SDL3.SDL.DialogFileCallback), parameter.ParameterType, $"SDL.{method.Name} parameter {parameterName} must use DialogFileCallback.");
        UnmanagedFunctionPointerAttribute? unmanagedFunctionPointer = parameter.ParameterType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(unmanagedFunctionPointer, $"SDL.{method.Name} callback delegate must keep unmanaged function pointer metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, unmanagedFunctionPointer!.CallingConvention, $"SDL.{method.Name} callback delegate must use cdecl calling convention.");
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
            TestAssert.NotNull(method, $"{methodName} capture method must exist.");
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

    private sealed class DialogCapture
    {
        public SDL3.SDL.DialogFileCallback? Callback;
        public IntPtr Userdata;
        public IntPtr Window;
        public IntPtr Filters;
        public int NFilters;
        public string? FilterName;
        public string? FilterPattern;
        public string? DefaultLocation;
        public bool AllowMany;
        public SDL3.SDL.FileDialogType DialogType;
        public uint Properties;
    }
}
