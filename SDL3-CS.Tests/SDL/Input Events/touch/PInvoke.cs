using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Touch;

internal static class PInvokeTests
{
    private static ulong capturedTouchId;
    private static IntPtr capturedFreeMemory;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static IntPtr nextPointer;
    private static int nextCount;
    private static SDL3.SDL.TouchDeviceType nextTouchDeviceType;

    public static void RunAll()
    {
        SDL_GetTouchDevices_UsesExpectedNativeMetadata();
        GetTouchDevices_ReturnsArrayNullAndFreesNativePointer();
        SDL_GetTouchDeviceName_UsesExpectedNativeMetadata();
        GetTouchDeviceName_ReturnsStringAndNull();
        GetTouchDeviceType_ForwardsTouchIdAndReturnsNativeValue();
        SDL_GetTouchFingers_UsesExpectedNativeMetadata();
        GetTouchFingers_ReturnsArrayNullAndFreesNativePointer();
    }

    public static void SDL_GetTouchDevices_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTouchDevices");
        AssertSdlImport(nativeMethod, "SDL_GetTouchDevices");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetTouchDevices_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr array = CreateNativeULongArray(111UL, 222UL);

        try
        {
            nextPointer = array;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetTouchDevicesNativeFunction", nameof(CaptureTouchDevicesPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            ulong[]? devices = SDL3.SDL.GetTouchDevices(out int count);

            TestAssert.NotNull(devices, "SDL.GetTouchDevices must convert native touch IDs.");
            TestAssert.Equal(2, devices!.Length, "SDL.GetTouchDevices must preserve native count.");
            TestAssert.Equal(111UL, devices[0], "SDL.GetTouchDevices must convert touch ID 0.");
            TestAssert.Equal(222UL, devices[1], "SDL.GetTouchDevices must convert touch ID 1.");
            TestAssert.Equal(2, count, "SDL.GetTouchDevices must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetTouchDevices must free the native array pointer.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetTouchDevices must free the non-null native pointer once.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            devices = SDL3.SDL.GetTouchDevices(out count);

            TestAssert.Equal<ulong[]?>(null, devices, "SDL.GetTouchDevices must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetTouchDevices must return native count for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetTouchDevices must call the native hook for both branches.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetTouchDevices must not free native null.");
        }
        finally
        {
            Marshal.FreeHGlobal(array);
        }
    }

    public static void SDL_GetTouchDeviceName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTouchDeviceName");
        AssertSdlImport(nativeMethod, "SDL_GetTouchDeviceName");
    }

    public static void GetTouchDeviceName_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("touch-name");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetTouchDeviceNameNativeFunction", nameof(CapturePointerForTouchId));
            string? result = SDL3.SDL.GetTouchDeviceName(333UL);

            TestAssert.Equal("touch-name", result, "SDL.GetTouchDeviceName must convert native UTF-8 strings.");
            TestAssert.Equal(333UL, capturedTouchId, "SDL.GetTouchDeviceName must forward touchID for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetTouchDeviceName(444UL);

            TestAssert.Equal<string?>(null, result, "SDL.GetTouchDeviceName must return null for native null.");
            TestAssert.Equal(444UL, capturedTouchId, "SDL.GetTouchDeviceName must forward touchID for null strings.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetTouchDeviceName must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GetTouchDeviceType_ForwardsTouchIdAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTouchDeviceType");
        AssertSdlImport(nativeMethod, "SDL_GetTouchDeviceType");

        ResetCaptureState();
        nextTouchDeviceType = SDL3.SDL.TouchDeviceType.IndirectAbsolute;

        using NativeHookScope _ = NativeHookScope.Install("GetTouchDeviceTypeNativeFunction", nameof(CaptureTouchDeviceType));
        SDL3.SDL.TouchDeviceType result = SDL3.SDL.GetTouchDeviceType(555UL);

        TestAssert.Equal(SDL3.SDL.TouchDeviceType.IndirectAbsolute, result, "SDL.GetTouchDeviceType must return the native hook value.");
        TestAssert.Equal(555UL, capturedTouchId, "SDL.GetTouchDeviceType must forward touchID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetTouchDeviceType must call the native hook once.");
    }

    public static void SDL_GetTouchFingers_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetTouchFingers");
        AssertSdlImport(nativeMethod, "SDL_GetTouchFingers");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetTouchFingers_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        SDL3.SDL.Finger first = new() { ID = 701UL, X = 0.25f, Y = 0.5f, Pressure = 0.75f };
        SDL3.SDL.Finger second = new() { ID = 702UL, X = 0.125f, Y = 0.375f, Pressure = 1.0f };
        IntPtr array = CreateNativeFingerPointerArray(out IntPtr[] fingerPointers, first, second);

        try
        {
            nextPointer = array;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetTouchFingersNativeFunction", nameof(CaptureTouchFingersPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            SDL3.SDL.Finger[]? fingers = SDL3.SDL.GetTouchFingers(666UL, out int count);

            TestAssert.NotNull(fingers, "SDL.GetTouchFingers must convert native fingers.");
            TestAssert.Equal(2, fingers!.Length, "SDL.GetTouchFingers must preserve native count.");
            TestAssert.Equal(701UL, fingers[0].ID, "SDL.GetTouchFingers must convert finger 0 ID.");
            TestAssert.Equal(0.25f, fingers[0].X, "SDL.GetTouchFingers must convert finger 0 X.");
            TestAssert.Equal(702UL, fingers[1].ID, "SDL.GetTouchFingers must convert finger 1 ID.");
            TestAssert.Equal(1.0f, fingers[1].Pressure, "SDL.GetTouchFingers must convert finger 1 pressure.");
            TestAssert.Equal(2, count, "SDL.GetTouchFingers must return native count.");
            TestAssert.Equal(666UL, capturedTouchId, "SDL.GetTouchFingers must forward touchID for non-null arrays.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetTouchFingers must free the native array pointer.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetTouchFingers must free the non-null native pointer once.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            fingers = SDL3.SDL.GetTouchFingers(777UL, out count);

            TestAssert.Equal<SDL3.SDL.Finger[]?>(null, fingers, "SDL.GetTouchFingers must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetTouchFingers must return native count for native null.");
            TestAssert.Equal(777UL, capturedTouchId, "SDL.GetTouchFingers must forward touchID for null arrays.");
            TestAssert.Equal(IntPtr.Zero, capturedFreeMemory, "SDL.GetTouchFingers must pass native null to SDL.Free.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetTouchFingers must call the native hook for both branches.");
            TestAssert.Equal(2, capturedFreeCallCount, "SDL.GetTouchFingers must call SDL.Free for both branches.");
        }
        finally
        {
            foreach (IntPtr fingerPointer in fingerPointers)
            {
                Marshal.FreeHGlobal(fingerPointer);
            }

            Marshal.FreeHGlobal(array);
        }
    }

    private static IntPtr CaptureTouchDevicesPointer(out int count)
    {
        capturedCallCount++;
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CapturePointerForTouchId(ulong touchID)
    {
        capturedCallCount++;
        capturedTouchId = touchID;
        return nextPointer;
    }

    private static SDL3.SDL.TouchDeviceType CaptureTouchDeviceType(ulong touchID)
    {
        capturedCallCount++;
        capturedTouchId = touchID;
        return nextTouchDeviceType;
    }

    private static IntPtr CaptureTouchFingersPointer(ulong touchID, out int count)
    {
        capturedCallCount++;
        capturedTouchId = touchID;
        count = nextCount;
        return nextPointer;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeCallCount++;
        capturedFreeMemory = mem;
    }

    private static void ResetCaptureState()
    {
        capturedTouchId = 0;
        capturedFreeMemory = IntPtr.Zero;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextPointer = IntPtr.Zero;
        nextCount = 0;
        nextTouchDeviceType = default;
    }

    private static IntPtr CreateNativeULongArray(params ulong[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(sizeof(ulong) * values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt64(pointer, i * sizeof(ulong), unchecked((long)values[i]));
        }

        return pointer;
    }

    private static IntPtr CreateNativeFingerPointerArray(out IntPtr[] elementPointers, params SDL3.SDL.Finger[] values)
    {
        int fingerSize = Marshal.SizeOf<SDL3.SDL.Finger>();
        elementPointers = new IntPtr[values.Length];
        IntPtr pointer = Marshal.AllocHGlobal(IntPtr.Size * values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            IntPtr elementPointer = Marshal.AllocHGlobal(fingerSize);
            Marshal.StructureToPtr(values[i], elementPointer, false);
            elementPointers[i] = elementPointer;
            Marshal.WriteIntPtr(pointer, i * IntPtr.Size, elementPointer);
        }

        return pointer;
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

    private static void AssertOutParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.IsOut, $"SDL.{method.Name} parameter {parameterName} must be an out parameter.");
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
