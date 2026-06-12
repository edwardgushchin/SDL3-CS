using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL;

internal static class SDLTests
{
    public static void RunAll()
    {
        PointerToStructure_ReturnsNullAndStructure();
        StructureToPointer_ReturnsZeroAndAllocatedStructure();
        StructureArrayToPointer_ReturnsZeroAndContiguousArray();
        PointerToPointerArray_ReturnsNullEmptyAndPointers();
        PointerToStringArray_NullTerminated_ReturnsNullAndStrings();
        PointerToStringArray_WithSize_ReturnsNullEmptyAndStrings();
        StringToPointer_ReturnsZeroAndUtf8Pointer();
        PointerToString_ReturnsNullAndString();
        PointerToStructureArray_ReturnsNullEmptyPrimitiveAndStructArrays();
        StringArrayToPointer_ReturnsZeroAndNullTerminatedUtf8PointerArray();
    }

    public static void PointerToStructure_ReturnsNullAndStructure()
    {
        TestAssert.Equal<TestStruct?>(null, SDL3.SDL.PointerToStructure<TestStruct>(IntPtr.Zero), "SDL.PointerToStructure must return null for IntPtr.Zero.");

        TestStruct expected = new() { A = 42, B = 1.5f };
        IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf<TestStruct>());

        try
        {
            Marshal.StructureToPtr(expected, pointer, false);
            TestStruct? actual = SDL3.SDL.PointerToStructure<TestStruct>(pointer);

            TestAssert.True(actual.HasValue, "SDL.PointerToStructure must return a value for a valid pointer.");
            AssertTestStruct(expected, actual!.Value, "SDL.PointerToStructure must read structure fields.");
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    public static void StructureToPointer_ReturnsZeroAndAllocatedStructure()
    {
        TestAssert.Equal(IntPtr.Zero, SDL3.SDL.StructureToPointer<TestStruct>(null), "SDL.StructureToPointer must return zero for null.");

        TestStruct expected = new() { A = 7, B = 2.25f };
        IntPtr pointer = SDL3.SDL.StructureToPointer<TestStruct>(expected);

        try
        {
            TestAssert.True(pointer != IntPtr.Zero, "SDL.StructureToPointer must allocate memory for a value.");
            TestStruct actual = Marshal.PtrToStructure<TestStruct>(pointer);
            AssertTestStruct(expected, actual, "SDL.StructureToPointer must copy structure fields.");
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    public static void StructureArrayToPointer_ReturnsZeroAndContiguousArray()
    {
        TestAssert.Equal(IntPtr.Zero, SDL3.SDL.StructureArrayToPointer(Array.Empty<TestStruct>()), "SDL.StructureArrayToPointer must return zero for an empty array.");

        TestStruct[] expected =
        [
            new() { A = 1, B = 1.25f },
            new() { A = 2, B = 2.5f }
        ];
        IntPtr pointer = SDL3.SDL.StructureArrayToPointer(expected);

        try
        {
            int size = Marshal.SizeOf<TestStruct>();
            TestAssert.True(pointer != IntPtr.Zero, "SDL.StructureArrayToPointer must allocate memory for a non-empty array.");

            for (int i = 0; i < expected.Length; i++)
            {
                TestStruct actual = Marshal.PtrToStructure<TestStruct>(IntPtr.Add(pointer, i * size));
                AssertTestStruct(expected[i], actual, $"SDL.StructureArrayToPointer must copy element {i}.");
            }
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    public static void PointerToPointerArray_ReturnsNullEmptyAndPointers()
    {
        TestAssert.Equal<IntPtr[]?>(null, SDL3.SDL.PointerToPointerArray(IntPtr.Zero, 2), "SDL.PointerToPointerArray must return null for IntPtr.Zero.");

        IntPtr emptyPointer = Marshal.AllocHGlobal(IntPtr.Size);
        try
        {
            IntPtr[]? empty = SDL3.SDL.PointerToPointerArray(emptyPointer, 0);
            TestAssert.NotNull(empty, "SDL.PointerToPointerArray must return an empty array for size 0.");
            TestAssert.Equal(0, empty!.Length, "SDL.PointerToPointerArray must preserve size 0.");
        }
        finally
        {
            Marshal.FreeHGlobal(emptyPointer);
        }

        IntPtr[] expected = [(IntPtr)0x1001, (IntPtr)0x1002, (IntPtr)0x1003];
        IntPtr pointer = Marshal.AllocHGlobal(IntPtr.Size * expected.Length);

        try
        {
            Marshal.Copy(expected, 0, pointer, expected.Length);
            IntPtr[]? actual = SDL3.SDL.PointerToPointerArray(pointer, expected.Length);

            TestAssert.NotNull(actual, "SDL.PointerToPointerArray must return an array for a valid pointer.");
            TestAssert.Equal(expected.Length, actual!.Length, "SDL.PointerToPointerArray must preserve pointer count.");
            for (int i = 0; i < expected.Length; i++)
            {
                TestAssert.Equal(expected[i], actual[i], $"SDL.PointerToPointerArray must copy pointer {i}.");
            }
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    public static void PointerToStringArray_NullTerminated_ReturnsNullAndStrings()
    {
        TestAssert.Equal<string[]?>(null, SDL3.SDL.PointerToStringArray(IntPtr.Zero), "SDL.PointerToStringArray must return null for IntPtr.Zero.");

        IntPtr terminatorOnly = Marshal.AllocHGlobal(IntPtr.Size);
        try
        {
            Marshal.WriteIntPtr(terminatorOnly, IntPtr.Zero);
            TestAssert.Equal<string[]?>(null, SDL3.SDL.PointerToStringArray(terminatorOnly), "SDL.PointerToStringArray must return null for an empty null-terminated array.");
        }
        finally
        {
            Marshal.FreeHGlobal(terminatorOnly);
        }

        IntPtr first = Marshal.StringToCoTaskMemUTF8("alpha");
        IntPtr second = Marshal.StringToCoTaskMemUTF8("бета");
        IntPtr block = Marshal.AllocHGlobal(IntPtr.Size * 3);

        try
        {
            Marshal.WriteIntPtr(block, 0, first);
            Marshal.WriteIntPtr(block, IntPtr.Size, second);
            Marshal.WriteIntPtr(block, IntPtr.Size * 2, IntPtr.Zero);

            string[]? actual = SDL3.SDL.PointerToStringArray(block);

            TestAssert.NotNull(actual, "SDL.PointerToStringArray must return strings for a valid null-terminated array.");
            TestAssert.Equal(2, actual!.Length, "SDL.PointerToStringArray must stop at the null terminator.");
            TestAssert.Equal("alpha", actual[0], "SDL.PointerToStringArray must decode string 0.");
            TestAssert.Equal("бета", actual[1], "SDL.PointerToStringArray must decode string 1.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(first);
            Marshal.FreeCoTaskMem(second);
            Marshal.FreeHGlobal(block);
        }
    }

    public static void PointerToStringArray_WithSize_ReturnsNullEmptyAndStrings()
    {
        TestAssert.Equal<string[]?>(null, SDL3.SDL.PointerToStringArray(IntPtr.Zero, 2), "SDL.PointerToStringArray(size) must return null for IntPtr.Zero.");

        IntPtr emptyPointer = Marshal.AllocHGlobal(IntPtr.Size);
        try
        {
            string[]? empty = SDL3.SDL.PointerToStringArray(emptyPointer, 0);
            TestAssert.NotNull(empty, "SDL.PointerToStringArray(size) must return an empty array for size 0.");
            TestAssert.Equal(0, empty!.Length, "SDL.PointerToStringArray(size) must preserve size 0.");
        }
        finally
        {
            Marshal.FreeHGlobal(emptyPointer);
        }

        IntPtr first = Marshal.StringToCoTaskMemUTF8("one");
        IntPtr second = Marshal.StringToCoTaskMemUTF8("two");
        IntPtr block = Marshal.AllocHGlobal(IntPtr.Size * 2);

        try
        {
            Marshal.WriteIntPtr(block, 0, first);
            Marshal.WriteIntPtr(block, IntPtr.Size, second);

            string[]? actual = SDL3.SDL.PointerToStringArray(block, 2);

            TestAssert.NotNull(actual, "SDL.PointerToStringArray(size) must return strings for a valid pointer array.");
            TestAssert.Equal(2, actual!.Length, "SDL.PointerToStringArray(size) must preserve requested size.");
            TestAssert.Equal("one", actual[0], "SDL.PointerToStringArray(size) must decode string 0.");
            TestAssert.Equal("two", actual[1], "SDL.PointerToStringArray(size) must decode string 1.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(first);
            Marshal.FreeCoTaskMem(second);
            Marshal.FreeHGlobal(block);
        }
    }

    public static void StringToPointer_ReturnsZeroAndUtf8Pointer()
    {
        TestAssert.Equal(IntPtr.Zero, SDL3.SDL.StringToPointer(null), "SDL.StringToPointer must return zero for null.");

        IntPtr pointer = SDL3.SDL.StringToPointer("hello Привет");

        try
        {
            TestAssert.True(pointer != IntPtr.Zero, "SDL.StringToPointer must allocate memory for a string.");
            TestAssert.Equal("hello Привет", Marshal.PtrToStringUTF8(pointer), "SDL.StringToPointer must encode UTF-8 text.");
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    public static void PointerToString_ReturnsNullAndString()
    {
        TestAssert.Equal<string?>(null, SDL3.SDL.PointerToString(IntPtr.Zero), "SDL.PointerToString must return null for IntPtr.Zero.");

        IntPtr pointer = Marshal.StringToCoTaskMemUTF8("plain text");

        try
        {
            TestAssert.Equal("plain text", SDL3.SDL.PointerToString(pointer), "SDL.PointerToString must decode UTF-8 text.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(pointer);
        }
    }

    public static void PointerToStructureArray_ReturnsNullEmptyPrimitiveAndStructArrays()
    {
        TestAssert.Equal<int[]?>(null, SDL3.SDL.PointerToStructureArray<int>(IntPtr.Zero, 2), "SDL.PointerToStructureArray must return null for IntPtr.Zero.");

        IntPtr dummy = Marshal.AllocHGlobal(IntPtr.Size);
        try
        {
            TestAssert.Equal<int[]?>(null, SDL3.SDL.PointerToStructureArray<int>(dummy, -1), "SDL.PointerToStructureArray must return null for negative count.");

            int[]? empty = SDL3.SDL.PointerToStructureArray<int>(dummy, 0);
            TestAssert.NotNull(empty, "SDL.PointerToStructureArray must return an empty array for count 0.");
            TestAssert.Equal(0, empty!.Length, "SDL.PointerToStructureArray must preserve count 0.");
        }
        finally
        {
            Marshal.FreeHGlobal(dummy);
        }

        int[] primitiveExpected = [10, 20, 30];
        IntPtr primitivePointer = Marshal.AllocHGlobal(sizeof(int) * primitiveExpected.Length);

        try
        {
            Marshal.Copy(primitiveExpected, 0, primitivePointer, primitiveExpected.Length);
            int[]? primitiveActual = SDL3.SDL.PointerToStructureArray<int>(primitivePointer, primitiveExpected.Length);

            TestAssert.NotNull(primitiveActual, "SDL.PointerToStructureArray must return primitive values.");
            for (int i = 0; i < primitiveExpected.Length; i++)
            {
                TestAssert.Equal(primitiveExpected[i], primitiveActual![i], $"SDL.PointerToStructureArray must copy primitive {i}.");
            }
        }
        finally
        {
            Marshal.FreeHGlobal(primitivePointer);
        }

        TestStruct[] structExpected =
        [
            new() { A = 5, B = 5.5f },
            new() { A = 6, B = 6.5f }
        ];
        IntPtr[] elementPointers = new IntPtr[structExpected.Length];
        IntPtr pointerArray = Marshal.AllocHGlobal(IntPtr.Size * elementPointers.Length);

        try
        {
            for (int i = 0; i < structExpected.Length; i++)
            {
                elementPointers[i] = Marshal.AllocHGlobal(Marshal.SizeOf<TestStruct>());
                Marshal.StructureToPtr(structExpected[i], elementPointers[i], false);
                Marshal.WriteIntPtr(pointerArray, i * IntPtr.Size, elementPointers[i]);
            }

            TestStruct[]? structActual = SDL3.SDL.PointerToStructureArray<TestStruct>(pointerArray, structExpected.Length);

            TestAssert.NotNull(structActual, "SDL.PointerToStructureArray must return non-primitive structs.");
            for (int i = 0; i < structExpected.Length; i++)
            {
                AssertTestStruct(structExpected[i], structActual![i], $"SDL.PointerToStructureArray must copy struct {i}.");
            }
        }
        finally
        {
            foreach (IntPtr elementPointer in elementPointers)
            {
                if (elementPointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(elementPointer);
                }
            }

            Marshal.FreeHGlobal(pointerArray);
        }
    }

    public static void StringArrayToPointer_ReturnsZeroAndNullTerminatedUtf8PointerArray()
    {
        TestAssert.Equal(IntPtr.Zero, SDL3.SDL.StringArrayToPointer(null), "SDL.StringArrayToPointer must return zero for null.");
        TestAssert.Equal(IntPtr.Zero, SDL3.SDL.StringArrayToPointer([]), "SDL.StringArrayToPointer must return zero for an empty array.");

        IntPtr pointer = SDL3.SDL.StringArrayToPointer(["first", "второй"]);

        try
        {
            TestAssert.True(pointer != IntPtr.Zero, "SDL.StringArrayToPointer must allocate a pointer array for strings.");
            IntPtr first = Marshal.ReadIntPtr(pointer, 0);
            IntPtr second = Marshal.ReadIntPtr(pointer, IntPtr.Size);
            IntPtr terminator = Marshal.ReadIntPtr(pointer, IntPtr.Size * 2);

            TestAssert.Equal("first", Marshal.PtrToStringUTF8(first), "SDL.StringArrayToPointer must encode string 0.");
            TestAssert.Equal("второй", Marshal.PtrToStringUTF8(second), "SDL.StringArrayToPointer must encode string 1.");
            TestAssert.Equal(IntPtr.Zero, terminator, "SDL.StringArrayToPointer must add a null terminator pointer.");
        }
        finally
        {
            FreeStringArrayPointer(pointer, 2);
        }
    }

    private static void FreeStringArrayPointer(IntPtr pointer, int count)
    {
        if (pointer == IntPtr.Zero)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            IntPtr stringPointer = Marshal.ReadIntPtr(pointer, i * IntPtr.Size);
            if (stringPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(stringPointer);
            }
        }

        Marshal.FreeHGlobal(pointer);
    }

    private static void AssertTestStruct(TestStruct expected, TestStruct actual, string message)
    {
        TestAssert.Equal(expected.A, actual.A, $"{message} A mismatch.");
        TestAssert.Equal(expected.B, actual.B, $"{message} B mismatch.");
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct TestStruct
    {
        public int A;
        public float B;
    }
}
