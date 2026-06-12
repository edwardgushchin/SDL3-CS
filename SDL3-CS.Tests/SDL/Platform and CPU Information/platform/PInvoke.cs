using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.PlatformAndCPUInformation.Platform;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static int capturedCallCount;

    public static void RunAll()
    {
        SDL_GetPlatform_UsesExpectedNativeMetadata();
        GetPlatform_ReturnsUtf8StringAndEmpty();
    }

    public static void SDL_GetPlatform_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPlatform");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetPlatform");
    }

    public static void GetPlatform_ReturnsUtf8StringAndEmpty()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("GetPlatformNativeFunction", nameof(CaptureGetPlatform));

        string value = CaptureUtf8String(SDL3.SDL.GetPlatform, "UnitTestOS");
        TestAssert.Equal("UnitTestOS", value, "SDL.GetPlatform must convert the native UTF-8 string.");

        nextPointer = IntPtr.Zero;
        value = SDL3.SDL.GetPlatform();
        TestAssert.Equal("", value, "SDL.GetPlatform must return an empty string for a null native pointer.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetPlatform must call the native hook for both branches.");
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static IntPtr CaptureGetPlatform()
    {
        capturedCallCount++;
        return nextPointer;
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

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
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
