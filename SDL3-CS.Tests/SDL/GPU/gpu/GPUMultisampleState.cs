using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.GPU.Gpu;

internal static class GPUMultisampleStateTests
{
    public static void RunAll()
    {
        EnableAlphaToCoverage_MapsBoolToNativeByteAndPreservesLayout();
    }

    public static void EnableAlphaToCoverage_MapsBoolToNativeByteAndPreservesLayout()
    {
        SDL3.SDL.GPUMultisampleState state = new()
        {
            EnableAlphaToCoverage = true,
        };

        TestAssert.True(state.EnableAlphaToCoverage, "GPUMultisampleState.EnableAlphaToCoverage must read true after setting true.");
        TestAssert.Equal((byte)1, NativeFlag(state), "GPUMultisampleState.EnableAlphaToCoverage must marshal true as byte 1.");

        state.EnableAlphaToCoverage = false;

        TestAssert.Equal(false, state.EnableAlphaToCoverage, "GPUMultisampleState.EnableAlphaToCoverage must read false after setting false.");
        TestAssert.Equal((byte)0, NativeFlag(state), "GPUMultisampleState.EnableAlphaToCoverage must marshal false as byte 0.");
        TestAssert.Equal(9, Marshal.OffsetOf<SDL3.SDL.GPUMultisampleState>("_enableAlphaToCoverage").ToInt32(), "GPUMultisampleState alpha-to-coverage byte must retain its SDL layout offset.");
        TestAssert.Equal(12, Marshal.SizeOf<SDL3.SDL.GPUMultisampleState>(), "GPUMultisampleState size must retain its SDL layout.");
    }

    private static byte NativeFlag(SDL3.SDL.GPUMultisampleState state)
    {
        FieldInfo field = typeof(SDL3.SDL.GPUMultisampleState).GetField("_enableAlphaToCoverage", BindingFlags.Instance | BindingFlags.NonPublic)!;
        return (byte)field.GetValue(state)!;
    }
}
