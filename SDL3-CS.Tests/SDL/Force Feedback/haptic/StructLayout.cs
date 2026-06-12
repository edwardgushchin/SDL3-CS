using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.ForceFeedback.Haptic;

internal static class StructLayoutTests
{
    public static void RunAll()
    {
        HapticDirection_MatchesNativeLayout();
        HapticEffectStructs_MatchNativeSizesAndFieldTypes();
        HapticCondition_UsesSignedNativeFields();
        HapticCustom_UsesNativePointerDataField();
        HapticEffect_OverlapsAllUnionMembers();
    }

    public static void HapticDirection_MatchesNativeLayout()
    {
        TestAssert.Equal(16, Marshal.SizeOf<SDL3.SDL.HapticDirection>(), "SDL.HapticDirection must match SDL_HapticDirection size.");
        AssertOffset<SDL3.SDL.HapticDirection>(nameof(SDL3.SDL.HapticDirection.Type), 0);
        AssertOffset<SDL3.SDL.HapticDirection>(nameof(SDL3.SDL.HapticDirection.Dir), 4);
        AssertFixedBuffer<SDL3.SDL.HapticDirection>(nameof(SDL3.SDL.HapticDirection.Dir), typeof(int), 3);
    }

    public static void HapticCondition_UsesSignedNativeFields()
    {
        AssertFixedBuffer<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.RightSat), typeof(ushort), 3);
        AssertFixedBuffer<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.LeftSat), typeof(ushort), 3);
        AssertFixedBuffer<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.RightCoeff), typeof(short), 3);
        AssertFixedBuffer<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.LeftCoeff), typeof(short), 3);
        AssertFixedBuffer<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.Deadband), typeof(ushort), 3);
        AssertFixedBuffer<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.Center), typeof(short), 3);
    }

    public static void HapticEffectStructs_MatchNativeSizesAndFieldTypes()
    {
        TestAssert.Equal(40, Marshal.SizeOf<SDL3.SDL.HapticConstant>(), "SDL.HapticConstant size must match native layout.");
        TestAssert.Equal(48, Marshal.SizeOf<SDL3.SDL.HapticPeriodic>(), "SDL.HapticPeriodic size must match native layout.");
        TestAssert.Equal(68, Marshal.SizeOf<SDL3.SDL.HapticCondition>(), "SDL.HapticCondition size must match native layout.");
        TestAssert.Equal(44, Marshal.SizeOf<SDL3.SDL.HapticRamp>(), "SDL.HapticRamp size must match native layout.");
        TestAssert.Equal(12, Marshal.SizeOf<SDL3.SDL.HapticLeftRight>(), "SDL.HapticLeftRight size must match native layout.");

        AssertFieldType<SDL3.SDL.HapticPeriodic>(nameof(SDL3.SDL.HapticPeriodic.Length), typeof(uint));
        AssertFieldType<SDL3.SDL.HapticCondition>(nameof(SDL3.SDL.HapticCondition.Length), typeof(uint));
        AssertFieldType<SDL3.SDL.HapticRamp>(nameof(SDL3.SDL.HapticRamp.Length), typeof(uint));
        AssertFieldType<SDL3.SDL.HapticLeftRight>(nameof(SDL3.SDL.HapticLeftRight.Length), typeof(uint));
        AssertFieldType<SDL3.SDL.HapticCustom>(nameof(SDL3.SDL.HapticCustom.Length), typeof(uint));
    }

    public static void HapticCustom_UsesNativePointerDataField()
    {
        FieldInfo? data = typeof(SDL3.SDL.HapticCustom).GetField(nameof(SDL3.SDL.HapticCustom.Data));
        TestAssert.NotNull(data, "SDL.HapticCustom.Data field must exist.");
        TestAssert.Equal(typeof(IntPtr), data!.FieldType, "SDL.HapticCustom.Data must mirror Uint16 *data as a native pointer.");

        int expectedDataOffset = IntPtr.Size == 8 ? 40 : 36;
        int expectedSize = IntPtr.Size == 8 ? 56 : 48;
        AssertOffset<SDL3.SDL.HapticCustom>(nameof(SDL3.SDL.HapticCustom.Data), expectedDataOffset);
        TestAssert.Equal(expectedSize, Marshal.SizeOf<SDL3.SDL.HapticCustom>(), "SDL.HapticCustom size must match native pointer layout.");
    }

    public static void HapticEffect_OverlapsAllUnionMembers()
    {
        int expectedUnionSize = IntPtr.Size == 8 ? 72 : 68;
        TestAssert.Equal(expectedUnionSize, Marshal.SizeOf<SDL3.SDL.HapticEffect>(), "SDL.HapticEffect must match native union size.");
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Type), 0);
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Constant), 0);
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Periodic), 0);
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Condition), 0);
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Ramp), 0);
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Leftright), 0);
        AssertOffset<SDL3.SDL.HapticEffect>(nameof(SDL3.SDL.HapticEffect.Custom), 0);
    }

    private static void AssertOffset<T>(string fieldName, int expected)
    {
        int actual = Marshal.OffsetOf<T>(fieldName).ToInt32();
        TestAssert.Equal(expected, actual, $"{typeof(T).Name}.{fieldName} must have native offset {expected}.");
    }

    private static void AssertFixedBuffer<T>(string fieldName, Type expectedElementType, int expectedLength)
    {
        FieldInfo? field = typeof(T).GetField(fieldName);
        TestAssert.NotNull(field, $"{typeof(T).Name}.{fieldName} field must exist.");

        FixedBufferAttribute? fixedBuffer = field!.GetCustomAttribute<FixedBufferAttribute>();
        TestAssert.NotNull(fixedBuffer, $"{typeof(T).Name}.{fieldName} must be a fixed buffer.");
        TestAssert.Equal(expectedElementType, fixedBuffer!.ElementType, $"{typeof(T).Name}.{fieldName} must use the expected native element type.");
        TestAssert.Equal(expectedLength, fixedBuffer.Length, $"{typeof(T).Name}.{fieldName} must use the expected native element count.");
    }

    private static void AssertFieldType<T>(string fieldName, Type expectedFieldType)
    {
        FieldInfo? field = typeof(T).GetField(fieldName);
        TestAssert.NotNull(field, $"{typeof(T).Name}.{fieldName} field must exist.");
        TestAssert.Equal(expectedFieldType, field!.FieldType, $"{typeof(T).Name}.{fieldName} must use the expected native field type.");
    }
}
