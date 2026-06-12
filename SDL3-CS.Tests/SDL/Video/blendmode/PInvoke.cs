using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.BlendMode;

internal static class PInvokeTests
{
    private static SDL3.SDL.BlendFactor capturedSrcColorFactor;
    private static SDL3.SDL.BlendFactor capturedDstColorFactor;
    private static SDL3.SDL.BlendOperation capturedColorOperation;
    private static SDL3.SDL.BlendFactor capturedSrcAlphaFactor;
    private static SDL3.SDL.BlendFactor capturedDstAlphaFactor;
    private static SDL3.SDL.BlendOperation capturedAlphaOperation;
    private static SDL3.SDL.BlendMode nextBlendMode;
    private static int capturedCallCount;

    public static void RunAll()
    {
        ComposeCustomBlendMode_ForwardsAllFactorsOperationsAndReturnsNativeValue();
    }

    public static void ComposeCustomBlendMode_ForwardsAllFactorsOperationsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBlendMode = (SDL3.SDL.BlendMode)0x12345678u;

        using NativeHookScope _ = NativeHookScope.Install("ComposeCustomBlendModeNativeFunction", nameof(CaptureComposeCustomBlendMode));
        SDL3.SDL.BlendMode result = SDL3.SDL.ComposeCustomBlendMode(
            SDL3.SDL.BlendFactor.SrcAlpha,
            SDL3.SDL.BlendFactor.OneMinusDstColor,
            SDL3.SDL.BlendOperation.Subtract,
            SDL3.SDL.BlendFactor.One,
            SDL3.SDL.BlendFactor.OneMinusSrcAlpha,
            SDL3.SDL.BlendOperation.Maximum);

        TestAssert.Equal(nextBlendMode, result, "SDL.ComposeCustomBlendMode must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.BlendFactor.SrcAlpha, capturedSrcColorFactor, "SDL.ComposeCustomBlendMode must forward srcColorFactor.");
        TestAssert.Equal(SDL3.SDL.BlendFactor.OneMinusDstColor, capturedDstColorFactor, "SDL.ComposeCustomBlendMode must forward dstColorFactor.");
        TestAssert.Equal(SDL3.SDL.BlendOperation.Subtract, capturedColorOperation, "SDL.ComposeCustomBlendMode must forward colorOperation.");
        TestAssert.Equal(SDL3.SDL.BlendFactor.One, capturedSrcAlphaFactor, "SDL.ComposeCustomBlendMode must forward srcAlphaFactor.");
        TestAssert.Equal(SDL3.SDL.BlendFactor.OneMinusSrcAlpha, capturedDstAlphaFactor, "SDL.ComposeCustomBlendMode must forward dstAlphaFactor.");
        TestAssert.Equal(SDL3.SDL.BlendOperation.Maximum, capturedAlphaOperation, "SDL.ComposeCustomBlendMode must forward alphaOperation.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ComposeCustomBlendMode must call the native hook once.");
    }

    private static SDL3.SDL.BlendMode CaptureComposeCustomBlendMode(
        SDL3.SDL.BlendFactor srcColorFactor,
        SDL3.SDL.BlendFactor dstColorFactor,
        SDL3.SDL.BlendOperation colorOperation,
        SDL3.SDL.BlendFactor srcAlphaFactor,
        SDL3.SDL.BlendFactor dstAlphaFactor,
        SDL3.SDL.BlendOperation alphaOperation)
    {
        capturedSrcColorFactor = srcColorFactor;
        capturedDstColorFactor = dstColorFactor;
        capturedColorOperation = colorOperation;
        capturedSrcAlphaFactor = srcAlphaFactor;
        capturedDstAlphaFactor = dstAlphaFactor;
        capturedAlphaOperation = alphaOperation;
        capturedCallCount++;
        return nextBlendMode;
    }

    private static void ResetCaptureState()
    {
        capturedSrcColorFactor = default;
        capturedDstColorFactor = default;
        capturedColorOperation = default;
        capturedSrcAlphaFactor = default;
        capturedDstAlphaFactor = default;
        capturedAlphaOperation = default;
        nextBlendMode = default;
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
