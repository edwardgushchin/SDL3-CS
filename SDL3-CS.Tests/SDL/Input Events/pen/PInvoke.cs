using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Pen;

internal static class PInvokeTests
{
    private static uint capturedInstanceId;
    private static int capturedCallCount;
    private static SDL3.SDL.PenDeviceType nextPenDeviceType;

    public static void RunAll()
    {
        GetPenDeviceType_ForwardsInstanceIdAndReturnsNativeValue();
    }

    public static void GetPenDeviceType_ForwardsInstanceIdAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPenDeviceType");
        AssertSdlImport(nativeMethod, "SDL_GetPenDeviceType");

        ResetCaptureState();
        nextPenDeviceType = SDL3.SDL.PenDeviceType.Direct;

        using NativeHookScope _ = NativeHookScope.Install("GetPenDeviceTypeNativeFunction", nameof(CaptureGetPenDeviceType));
        SDL3.SDL.PenDeviceType result = SDL3.SDL.GetPenDeviceType(42u);

        TestAssert.Equal(SDL3.SDL.PenDeviceType.Direct, result, "SDL.GetPenDeviceType must return the native hook value.");
        TestAssert.Equal(42u, capturedInstanceId, "SDL.GetPenDeviceType must forward instanceId.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPenDeviceType must call the native hook once.");
    }

    private static SDL3.SDL.PenDeviceType CaptureGetPenDeviceType(uint instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextPenDeviceType;
    }

    private static void ResetCaptureState()
    {
        capturedInstanceId = 0;
        capturedCallCount = 0;
        nextPenDeviceType = default;
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
