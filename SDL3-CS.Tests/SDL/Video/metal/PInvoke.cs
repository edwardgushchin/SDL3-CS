using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Metal;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr capturedWindow;
    private static IntPtr capturedView;
    private static int capturedCallCount;

    public static void RunAll()
    {
        MetalCreateView_ForwardsWindowAndReturnsNativeValue();
        MetalDestroyView_ForwardsView();
        MetalGetLayer_ForwardsViewAndReturnsNativeValue();
    }

    public static void MetalCreateView_ForwardsWindowAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1111;

        using NativeHookScope _ = NativeHookScope.Install("MetalCreateViewNativeFunction", nameof(CaptureMetalCreateView));
        IntPtr result = SDL3.SDL.MetalCreateView((IntPtr)0x2222);

        TestAssert.Equal((IntPtr)0x1111, result, "SDL.MetalCreateView must return the native hook value.");
        TestAssert.Equal((IntPtr)0x2222, capturedWindow, "SDL.MetalCreateView must forward window.");
        TestAssert.Equal(1, capturedCallCount, "SDL.MetalCreateView must call the native hook once.");
    }

    public static void MetalDestroyView_ForwardsView()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("MetalDestroyViewNativeFunction", nameof(CaptureMetalDestroyView));
        SDL3.SDL.MetalDestroyView((IntPtr)0x3333);

        TestAssert.Equal((IntPtr)0x3333, capturedView, "SDL.MetalDestroyView must forward view.");
        TestAssert.Equal(1, capturedCallCount, "SDL.MetalDestroyView must call the native hook once.");
    }

    public static void MetalGetLayer_ForwardsViewAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x4444;

        using NativeHookScope _ = NativeHookScope.Install("MetalGetLayerNativeFunction", nameof(CaptureMetalGetLayer));
        IntPtr result = SDL3.SDL.MetalGetLayer((IntPtr)0x5555);

        TestAssert.Equal((IntPtr)0x4444, result, "SDL.MetalGetLayer must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5555, capturedView, "SDL.MetalGetLayer must forward view.");
        TestAssert.Equal(1, capturedCallCount, "SDL.MetalGetLayer must call the native hook once.");
    }

    private static IntPtr CaptureMetalCreateView(IntPtr window)
    {
        capturedWindow = window;
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureMetalDestroyView(IntPtr view)
    {
        capturedView = view;
        capturedCallCount++;
    }

    private static IntPtr CaptureMetalGetLayer(IntPtr view)
    {
        capturedView = view;
        capturedCallCount++;
        return nextPointer;
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedWindow = IntPtr.Zero;
        capturedView = IntPtr.Zero;
        capturedCallCount = 0;
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
