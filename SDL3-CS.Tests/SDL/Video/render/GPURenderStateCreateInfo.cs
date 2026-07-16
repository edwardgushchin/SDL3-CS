using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Render;

internal static class GPURenderStateCreateInfoTests
{
    public static void Layout_MatchesSdl3412Abi()
    {
        Type type = typeof(SDL3.SDL.GPURenderStateCreateInfo);
        StructLayoutAttribute? layout = type.StructLayoutAttribute;

        TestAssert.NotNull(layout, "SDL.GPURenderStateCreateInfo must declare StructLayout.");
        TestAssert.Equal(LayoutKind.Sequential, layout!.Value, "SDL.GPURenderStateCreateInfo must use sequential layout.");

        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        string[] expectedNames =
        [
            nameof(SDL3.SDL.GPURenderStateCreateInfo.FragmentShader),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.NumSamplerBindings),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.SamplerBindings),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.NumStorageTextures),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.StorageTextures),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.NumStorageBuffers),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.StorageBuffers),
            nameof(SDL3.SDL.GPURenderStateCreateInfo.Props)
        ];
        Type[] expectedTypes =
        [
            typeof(IntPtr),
            typeof(int),
            typeof(IntPtr),
            typeof(int),
            typeof(IntPtr),
            typeof(int),
            typeof(IntPtr),
            typeof(uint)
        ];
        int pointerSize = IntPtr.Size;
        int[] expectedOffsets =
        [
            0,
            pointerSize,
            2 * pointerSize,
            3 * pointerSize,
            4 * pointerSize,
            5 * pointerSize,
            6 * pointerSize,
            7 * pointerSize
        ];

        TestAssert.Equal(expectedNames.Length, fields.Length, "SDL.GPURenderStateCreateInfo must expose exactly the native fields.");

        for (int index = 0; index < fields.Length; index++)
        {
            FieldInfo field = fields[index];
            TestAssert.Equal(expectedNames[index], field.Name, $"SDL.GPURenderStateCreateInfo field {index} must keep native order.");
            TestAssert.Equal(expectedTypes[index], field.FieldType, $"SDL.GPURenderStateCreateInfo.{field.Name} must use the native field type.");
            TestAssert.Equal(expectedOffsets[index], Marshal.OffsetOf(type, field.Name).ToInt32(), $"SDL.GPURenderStateCreateInfo.{field.Name} must keep the native offset.");
        }

        int expectedSize = 8 * pointerSize;
        TestAssert.Equal(expectedSize, Marshal.SizeOf<SDL3.SDL.GPURenderStateCreateInfo>(), "SDL.GPURenderStateCreateInfo must keep the native size.");
    }
}
