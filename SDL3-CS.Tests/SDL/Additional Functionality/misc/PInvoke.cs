using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Misc;

internal static class PInvokeTests
{
    private static string? capturedUrl;

    public static void OpenURL_ForwardsUrl()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_OpenURL", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_OpenURL method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_OpenURL");
        AssertBoolReturnMarshal(nativeMethod!);
        AssertStringParameterMarshal(nativeMethod!, "url");

        using NativeHookScope _ = NativeHookScope.Install("OpenURLNativeFunction", nameof(CaptureOpenURL));
        bool result = SDL3.SDL.OpenURL("https://example.invalid/");

        TestAssert.Equal(true, result, "SDL.OpenURL must return the native hook result.");
        TestAssert.Equal("https://example.invalid/", capturedUrl, "SDL.OpenURL must forward the URL string.");
    }

    private static bool CaptureOpenURL(string url)
    {
        capturedUrl = url;
        return true;
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
