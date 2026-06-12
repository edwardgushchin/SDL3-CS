using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Messagebox;

internal static class PInvokeTests
{
    private static SDL3.SDL.MessageBoxData capturedMessageBoxData;
    private static SDL3.SDL.MessageBoxFlags capturedSimpleFlags;
    private static string? capturedSimpleTitle;
    private static string? capturedSimpleMessage;
    private static IntPtr capturedSimpleWindow;

    public static void ShowMessageBox_ForwardsDataAndButtonId()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_ShowMessageBox", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_ShowMessageBox method must be private static.");
        AssertSdlDllImport(nativeMethod!, "SDL_ShowMessageBox");
        AssertBoolReturnMarshal(nativeMethod!);

        using NativeHookScope _ = NativeHookScope.Install("ShowMessageBoxNativeFunction", nameof(CaptureShowMessageBox));
        SDL3.SDL.MessageBoxData data = new()
        {
            Flags = SDL3.SDL.MessageBoxFlags.Warning,
            Window = (IntPtr)12,
            Title = "Title",
            Message = "Body",
            NumButtons = 0,
            Buttons = IntPtr.Zero,
            ColorScheme = IntPtr.Zero
        };

        bool result = SDL3.SDL.ShowMessageBox(in data, out int buttonId);
        TestAssert.Equal(true, result, "SDL.ShowMessageBox must return the native hook result.");
        TestAssert.Equal(99, buttonId, "SDL.ShowMessageBox must forward the native button id.");
        TestAssert.Equal(SDL3.SDL.MessageBoxFlags.Warning, capturedMessageBoxData.Flags, "SDL.ShowMessageBox must forward flags.");
        TestAssert.Equal((IntPtr)12, capturedMessageBoxData.Window, "SDL.ShowMessageBox must forward window.");
        TestAssert.Equal("Title", capturedMessageBoxData.Title, "SDL.ShowMessageBox must forward title.");
        TestAssert.Equal("Body", capturedMessageBoxData.Message, "SDL.ShowMessageBox must forward message.");
    }

    public static void ShowSimpleMessageBox_ForwardsTextAndWindow()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_ShowSimpleMessageBox", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_ShowSimpleMessageBox method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_ShowSimpleMessageBox");
        AssertBoolReturnMarshal(nativeMethod!);
        AssertStringParameterMarshal(nativeMethod!, "title");
        AssertStringParameterMarshal(nativeMethod!, "message");

        using NativeHookScope _ = NativeHookScope.Install("ShowSimpleMessageBoxNativeFunction", nameof(CaptureShowSimpleMessageBox));
        bool result = SDL3.SDL.ShowSimpleMessageBox(SDL3.SDL.MessageBoxFlags.Information, "Info", "Message", (IntPtr)34);

        TestAssert.Equal(true, result, "SDL.ShowSimpleMessageBox must return the native hook result.");
        TestAssert.Equal(SDL3.SDL.MessageBoxFlags.Information, capturedSimpleFlags, "SDL.ShowSimpleMessageBox must forward flags.");
        TestAssert.Equal("Info", capturedSimpleTitle, "SDL.ShowSimpleMessageBox must forward title.");
        TestAssert.Equal("Message", capturedSimpleMessage, "SDL.ShowSimpleMessageBox must forward message.");
        TestAssert.Equal((IntPtr)34, capturedSimpleWindow, "SDL.ShowSimpleMessageBox must forward window.");
    }

    private static bool CaptureShowMessageBox(in SDL3.SDL.MessageBoxData messageboxdata, out int buttonid)
    {
        capturedMessageBoxData = messageboxdata;
        buttonid = 99;
        return true;
    }

    private static bool CaptureShowSimpleMessageBox(SDL3.SDL.MessageBoxFlags flags, string title, string message, IntPtr window)
    {
        capturedSimpleFlags = flags;
        capturedSimpleTitle = title;
        capturedSimpleMessage = message;
        capturedSimpleWindow = window;
        return true;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertSdlDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
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
