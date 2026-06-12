using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL;

internal static class WCharStringMarshallerTests
{
    public static void RunAll()
    {
        WChar16_ConvertToUnmanaged_WritesUtf16AndHandlesNull();
        WChar16_ConvertToManaged_ReadsUtf16AndHandlesNull();
        WChar16_Free_ReleasesAllocatedMemory();
        WChar32_ConvertToUnmanaged_WritesUtf32AndHandlesNull();
        WChar32_ConvertToManaged_ReadsUtf32AndHandlesNull();
        WChar32_PtrToStringUTF32_ReadsUtf32AndHandlesNull();
        WChar32_Free_ReleasesAllocatedMemory();
        WCharSize_UsesSelectedPlatformSize();
        DefaultMarshaller_ConvertToUnmanaged_UsesSelectedPlatformImplementation();
        DefaultMarshaller_ConvertToManaged_UsesSelectedPlatformImplementation();
        DefaultMarshaller_Free_ReleasesAllocatedMemory();
    }

    public static void WChar16_ConvertToUnmanaged_WritesUtf16AndHandlesNull()
    {
        TestAssert.Equal(IntPtr.Zero, WCharStringMarshaller.WChar16.ConvertToUnmanaged(null), "WChar16.ConvertToUnmanaged must return zero for null.");

        IntPtr ptr = WCharStringMarshaller.WChar16.ConvertToUnmanaged("Hi");
        try
        {
            TestAssert.True(ptr != IntPtr.Zero, "WChar16.ConvertToUnmanaged must allocate non-null strings.");
            TestAssert.Equal("Hi", Marshal.PtrToStringUni(ptr), "WChar16.ConvertToUnmanaged must write UTF-16 text.");
            TestAssert.Equal(0, Marshal.ReadInt16(ptr, "Hi".Length * sizeof(char)), "WChar16.ConvertToUnmanaged must null-terminate text.");
        }
        finally
        {
            WCharStringMarshaller.WChar16.Free(ptr);
        }
    }

    public static void WChar16_ConvertToManaged_ReadsUtf16AndHandlesNull()
    {
        TestAssert.Equal<string?>(null, WCharStringMarshaller.WChar16.ConvertToManaged(IntPtr.Zero), "WChar16.ConvertToManaged must return null for zero.");

        IntPtr ptr = Marshal.StringToHGlobalUni("Wide16");
        try
        {
            TestAssert.Equal("Wide16", WCharStringMarshaller.WChar16.ConvertToManaged(ptr), "WChar16.ConvertToManaged must read UTF-16 text.");
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public static void WChar16_Free_ReleasesAllocatedMemory()
    {
        IntPtr ptr = Marshal.AllocHGlobal(sizeof(char));
        WCharStringMarshaller.WChar16.Free(ptr);
    }

    public static void WChar32_ConvertToUnmanaged_WritesUtf32AndHandlesNull()
    {
        TestAssert.Equal(IntPtr.Zero, WCharStringMarshaller.WChar32.ConvertToUnmanaged(null), "WChar32.ConvertToUnmanaged must return zero for null.");

        IntPtr ptr = WCharStringMarshaller.WChar32.ConvertToUnmanaged("Hi");
        try
        {
            TestAssert.True(ptr != IntPtr.Zero, "WChar32.ConvertToUnmanaged must allocate non-null strings.");
            TestAssert.Equal("Hi", WCharStringMarshaller.WChar32.PtrToStringUTF32(ptr), "WChar32.ConvertToUnmanaged must write UTF-32 text.");
            TestAssert.Equal(0, Marshal.ReadInt32(ptr, "Hi".Length * sizeof(int)), "WChar32.ConvertToUnmanaged must null-terminate text.");
        }
        finally
        {
            WCharStringMarshaller.WChar32.Free(ptr);
        }
    }

    public static void WChar32_ConvertToManaged_ReadsUtf32AndHandlesNull()
    {
        TestAssert.Equal<string?>(null, WCharStringMarshaller.WChar32.ConvertToManaged(IntPtr.Zero), "WChar32.ConvertToManaged must return null for zero.");

        IntPtr ptr = AllocateUtf32('W', '3', '2');
        try
        {
            TestAssert.Equal("W32", WCharStringMarshaller.WChar32.ConvertToManaged(ptr), "WChar32.ConvertToManaged must read UTF-32 text.");
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public static void WChar32_PtrToStringUTF32_ReadsUtf32AndHandlesNull()
    {
        TestAssert.Equal<string?>(null, WCharStringMarshaller.WChar32.PtrToStringUTF32(IntPtr.Zero), "WChar32.PtrToStringUTF32 must return null for zero.");

        IntPtr ptr = AllocateUtf32('A', 'Z');
        try
        {
            TestAssert.Equal("AZ", WCharStringMarshaller.WChar32.PtrToStringUTF32(ptr), "WChar32.PtrToStringUTF32 must read UTF-32 text.");
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    public static void WChar32_Free_ReleasesAllocatedMemory()
    {
        IntPtr ptr = Marshal.AllocHGlobal(sizeof(int));
        WCharStringMarshaller.WChar32.Free(ptr);
    }

    public static void WCharSize_UsesSelectedPlatformSize()
    {
        using (PlatformHookScope _ = PlatformHookScope.Install(true))
        {
            TestAssert.Equal((UIntPtr)2u, WCharStringMarshaller.WCharSize, "WCharSize must be 2 on Windows branch.");
        }

        using (PlatformHookScope _ = PlatformHookScope.Install(false))
        {
            TestAssert.Equal((UIntPtr)4u, WCharStringMarshaller.WCharSize, "WCharSize must be 4 on non-Windows branch.");
        }
    }

    public static void DefaultMarshaller_ConvertToUnmanaged_UsesSelectedPlatformImplementation()
    {
        using (PlatformHookScope _ = PlatformHookScope.Install(true))
        {
            IntPtr ptr = WCharStringMarshaller.ConvertToUnmanaged("Win");
            try
            {
                TestAssert.Equal("Win", WCharStringMarshaller.WChar16.ConvertToManaged(ptr), "Default ConvertToUnmanaged must use WChar16 on Windows branch.");
            }
            finally
            {
                WCharStringMarshaller.Free(ptr);
            }
        }

        using (PlatformHookScope _ = PlatformHookScope.Install(false))
        {
            IntPtr ptr = WCharStringMarshaller.ConvertToUnmanaged("Unix");
            try
            {
                TestAssert.Equal("Unix", WCharStringMarshaller.WChar32.ConvertToManaged(ptr), "Default ConvertToUnmanaged must use WChar32 on non-Windows branch.");
            }
            finally
            {
                WCharStringMarshaller.Free(ptr);
            }
        }
    }

    public static void DefaultMarshaller_ConvertToManaged_UsesSelectedPlatformImplementation()
    {
        IntPtr utf16 = WCharStringMarshaller.WChar16.ConvertToUnmanaged("Managed16");
        try
        {
            using PlatformHookScope _ = PlatformHookScope.Install(true);
            TestAssert.Equal("Managed16", WCharStringMarshaller.ConvertToManaged(utf16), "Default ConvertToManaged must use WChar16 on Windows branch.");
        }
        finally
        {
            WCharStringMarshaller.WChar16.Free(utf16);
        }

        IntPtr utf32 = WCharStringMarshaller.WChar32.ConvertToUnmanaged("Managed32");
        try
        {
            using PlatformHookScope _ = PlatformHookScope.Install(false);
            TestAssert.Equal("Managed32", WCharStringMarshaller.ConvertToManaged(utf32), "Default ConvertToManaged must use WChar32 on non-Windows branch.");
        }
        finally
        {
            WCharStringMarshaller.WChar32.Free(utf32);
        }
    }

    public static void DefaultMarshaller_Free_ReleasesAllocatedMemory()
    {
        IntPtr ptr = Marshal.AllocHGlobal(1);
        WCharStringMarshaller.Free(ptr);
    }

    private static IntPtr AllocateUtf32(params char[] chars)
    {
        IntPtr ptr = Marshal.AllocHGlobal((chars.Length + 1) * sizeof(int));
        for (int i = 0; i < chars.Length; i++)
        {
            Marshal.WriteInt32(ptr, i * sizeof(int), chars[i]);
        }

        Marshal.WriteInt32(ptr, chars.Length * sizeof(int), 0);
        return ptr;
    }

    private sealed class PlatformHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private PlatformHookScope(FieldInfo field, Func<bool> hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static PlatformHookScope Install(bool isWindows)
        {
            FieldInfo? field = typeof(WCharStringMarshaller).GetField("IsWindowsFunction", BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, "WCharStringMarshaller private platform hook field must exist.");

            return new PlatformHookScope(field!, () => isWindows);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
