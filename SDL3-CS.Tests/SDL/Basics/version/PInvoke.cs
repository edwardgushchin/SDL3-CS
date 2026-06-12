using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Version;

internal static class PInvokeTests
{
    private static int nextVersion;
    private static IntPtr nextRevisionPointer;
    private static int capturedCallCount;

    public static void GetVersion_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetVersion");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetVersion");
        TestAssert.Equal(typeof(int), nativeMethod.ReturnType, "SDL.SDL_GetVersion must return int.");

        ResetCaptureState();
        nextVersion = 3002001;
        using NativeHookScope _ = NativeHookScope.Install("GetVersionNativeFunction", nameof(CaptureGetVersion));

        int result = SDL3.SDL.GetVersion();

        TestAssert.Equal(3002001, result, "SDL.GetVersion must return native version value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetVersion must call native hook once.");
    }

    public static void SDL_GetRevision_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetRevision");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetRevision");
        TestAssert.Equal(typeof(IntPtr), nativeMethod.ReturnType, "SDL.SDL_GetRevision must return IntPtr.");
    }

    public static void GetRevision_ReturnsUtf8StringAndEmpty()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetRevisionNativeFunction", nameof(CaptureGetRevision));

        string revision = CaptureUtf8String(SDL3.SDL.GetRevision, "revision-123");
        TestAssert.Equal("revision-123", revision, "SDL.GetRevision must convert native UTF-8 revision string.");

        nextRevisionPointer = IntPtr.Zero;
        TestAssert.Equal("", SDL3.SDL.GetRevision(), "SDL.GetRevision must return an empty string for native null.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetRevision must call native hook for both branches.");
    }

    private static void ResetCaptureState()
    {
        nextVersion = 0;
        nextRevisionPointer = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static int CaptureGetVersion()
    {
        capturedCallCount++;
        return nextVersion;
    }

    private static IntPtr CaptureGetRevision()
    {
        capturedCallCount++;
        return nextRevisionPointer;
    }

    private static string CaptureUtf8String(Func<string> action, string value)
    {
        IntPtr pointer = Marshal.StringToCoTaskMemUTF8(value);
        nextRevisionPointer = pointer;

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
