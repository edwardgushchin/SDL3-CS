using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Sensor;

internal static class PInvokeTests
{
    private static int capturedInstanceId;
    private static IntPtr capturedSensor;
    private static IntPtr capturedFreeMemory;
    private static int capturedNumValues;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;
    private static IntPtr nextPointer;
    private static int nextCount;
    private static SDL3.SDL.SensorType nextSensorType;
    private static int nextInt;
    private static uint nextUInt;
    private static bool nextBool;
    private static float nextData;

    public static void RunAll()
    {
        SDL_GetSensors_UsesExpectedNativeMetadata();
        GetSensors_ReturnsArrayNullAndFreesNativePointer();
        SDL_GetSensorNameForID_UsesExpectedNativeMetadata();
        GetSensorNameForID_ReturnsStringAndNull();
        GetSensorTypeForID_ForwardsInstanceIdAndReturnsNativeValue();
        GetSensorNonPortableTypeForID_ForwardsInstanceIdAndReturnsNativeValue();
        OpenSensor_ForwardsInstanceIdAndReturnsNativePointer();
        GetSensorFromID_ForwardsInstanceIdAndReturnsNativePointer();
        GetSensorProperties_ForwardsSensorAndReturnsNativeValue();
        SDL_GetSensorName_UsesExpectedNativeMetadata();
        GetSensorName_ReturnsStringAndNull();
        GetSensorType_ForwardsSensorAndReturnsNativeValue();
        GetSensorNonPortableType_ForwardsSensorAndReturnsNativeValue();
        GetSensorID_ForwardsSensorAndReturnsNativeValue();
        GetSensorData_ForwardsSensorNumValuesOutputsDataAndReturnsNativeValue();
        CloseSensor_ForwardsSensor();
        UpdateSensors_CallsNativeHook();
    }

    public static void SDL_GetSensors_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensors");
        AssertSdlImport(nativeMethod, "SDL_GetSensors");
        AssertOutParameter(nativeMethod, "count");
    }

    public static void GetSensors_ReturnsArrayNullAndFreesNativePointer()
    {
        ResetCaptureState();
        IntPtr array = CreateNativeUIntArray(101u, 202u);

        try
        {
            nextPointer = array;
            nextCount = 2;

            using NativeHookScope nativeHook = NativeHookScope.Install("GetSensorsNativeFunction", nameof(CaptureSensorArrayPointer));
            using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
            uint[]? sensors = SDL3.SDL.GetSensors(out int count);

            TestAssert.NotNull(sensors, "SDL.GetSensors must convert native sensor IDs.");
            TestAssert.Equal(2, sensors!.Length, "SDL.GetSensors must preserve native count.");
            TestAssert.Equal(101u, sensors[0], "SDL.GetSensors must convert sensor ID 0.");
            TestAssert.Equal(202u, sensors[1], "SDL.GetSensors must convert sensor ID 1.");
            TestAssert.Equal(2, count, "SDL.GetSensors must return native count.");
            TestAssert.Equal(array, capturedFreeMemory, "SDL.GetSensors must free the native array pointer.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetSensors must free the non-null native pointer once.");

            nextPointer = IntPtr.Zero;
            nextCount = 0;
            sensors = SDL3.SDL.GetSensors(out count);

            TestAssert.Equal<uint[]?>(null, sensors, "SDL.GetSensors must return null for native null.");
            TestAssert.Equal(0, count, "SDL.GetSensors must return native count for native null.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetSensors must call the native hook for both branches.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetSensors must not free native null.");
        }
        finally
        {
            Marshal.FreeHGlobal(array);
        }
    }

    public static void SDL_GetSensorNameForID_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorNameForID");
        AssertSdlImport(nativeMethod, "SDL_GetSensorNameForID");
    }

    public static void GetSensorNameForID_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("sensor-id-name");

        try
        {
            nextPointer = value;

            using NativeHookScope _ = NativeHookScope.Install("GetSensorNameForIDNativeFunction", nameof(CapturePointerForInstanceId));
            string? result = SDL3.SDL.GetSensorNameForID(12);

            TestAssert.Equal("sensor-id-name", result, "SDL.GetSensorNameForID must convert native UTF-8 strings.");
            TestAssert.Equal(12, capturedInstanceId, "SDL.GetSensorNameForID must forward instanceId for non-null strings.");

            nextPointer = IntPtr.Zero;
            result = SDL3.SDL.GetSensorNameForID(13);

            TestAssert.Equal<string?>(null, result, "SDL.GetSensorNameForID must return null for native null.");
            TestAssert.Equal(13, capturedInstanceId, "SDL.GetSensorNameForID must forward instanceId for null strings.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetSensorNameForID must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GetSensorTypeForID_ForwardsInstanceIdAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorTypeForID");
        AssertSdlImport(nativeMethod, "SDL_GetSensorTypeForID");

        ResetCaptureState();
        nextSensorType = SDL3.SDL.SensorType.Gyro;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorTypeForIDNativeFunction", nameof(CaptureSensorTypeForInstanceId));
        SDL3.SDL.SensorType result = SDL3.SDL.GetSensorTypeForID(14);

        TestAssert.Equal(SDL3.SDL.SensorType.Gyro, result, "SDL.GetSensorTypeForID must return the native hook value.");
        TestAssert.Equal(14, capturedInstanceId, "SDL.GetSensorTypeForID must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorTypeForID must call the native hook once.");
    }

    public static void GetSensorNonPortableTypeForID_ForwardsInstanceIdAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorNonPortableTypeForID");
        AssertSdlImport(nativeMethod, "SDL_GetSensorNonPortableTypeForID");

        ResetCaptureState();
        nextInt = 777;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorNonPortableTypeForIDNativeFunction", nameof(CaptureIntForInstanceId));
        int result = SDL3.SDL.GetSensorNonPortableTypeForID(15);

        TestAssert.Equal(777, result, "SDL.GetSensorNonPortableTypeForID must return the native hook value.");
        TestAssert.Equal(15, capturedInstanceId, "SDL.GetSensorNonPortableTypeForID must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorNonPortableTypeForID must call the native hook once.");
    }

    public static void OpenSensor_ForwardsInstanceIdAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenSensor");
        AssertSdlImport(nativeMethod, "SDL_OpenSensor");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7101;

        using NativeHookScope _ = NativeHookScope.Install("OpenSensorNativeFunction", nameof(CapturePointerForInstanceId));
        IntPtr result = SDL3.SDL.OpenSensor(16);

        TestAssert.Equal((IntPtr)0x7101, result, "SDL.OpenSensor must return the native hook value.");
        TestAssert.Equal(16, capturedInstanceId, "SDL.OpenSensor must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenSensor must call the native hook once.");
    }

    public static void GetSensorFromID_ForwardsInstanceIdAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorFromID");
        AssertSdlImport(nativeMethod, "SDL_GetSensorFromID");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7202;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorFromIDNativeFunction", nameof(CapturePointerForInstanceId));
        IntPtr result = SDL3.SDL.GetSensorFromID(17);

        TestAssert.Equal((IntPtr)0x7202, result, "SDL.GetSensorFromID must return the native hook value.");
        TestAssert.Equal(17, capturedInstanceId, "SDL.GetSensorFromID must forward instanceID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorFromID must call the native hook once.");
    }

    public static void GetSensorProperties_ForwardsSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorProperties");
        AssertSdlImport(nativeMethod, "SDL_GetSensorProperties");

        ResetCaptureState();
        nextUInt = 888u;
        IntPtr sensor = (IntPtr)0x7303;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorPropertiesNativeFunction", nameof(CaptureUIntForSensor));
        uint result = SDL3.SDL.GetSensorProperties(sensor);

        TestAssert.Equal(888u, result, "SDL.GetSensorProperties must return the native hook value.");
        TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorProperties must forward sensor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorProperties must call the native hook once.");
    }

    public static void SDL_GetSensorName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorName");
        AssertSdlImport(nativeMethod, "SDL_GetSensorName");
    }

    public static void GetSensorName_ReturnsStringAndNull()
    {
        ResetCaptureState();
        IntPtr value = Marshal.StringToCoTaskMemUTF8("sensor-name");

        try
        {
            nextPointer = value;
            IntPtr sensor = (IntPtr)0x7404;

            using NativeHookScope _ = NativeHookScope.Install("GetSensorNameNativeFunction", nameof(CapturePointerForSensor));
            string? result = SDL3.SDL.GetSensorName(sensor);

            TestAssert.Equal("sensor-name", result, "SDL.GetSensorName must convert native UTF-8 strings.");
            TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorName must forward sensor for non-null strings.");

            nextPointer = IntPtr.Zero;
            sensor = (IntPtr)0x7505;
            result = SDL3.SDL.GetSensorName(sensor);

            TestAssert.Equal<string?>(null, result, "SDL.GetSensorName must return null for native null.");
            TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorName must forward sensor for null strings.");
            TestAssert.Equal(2, capturedCallCount, "SDL.GetSensorName must call the native hook for both branches.");
        }
        finally
        {
            Marshal.FreeCoTaskMem(value);
        }
    }

    public static void GetSensorType_ForwardsSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorType");
        AssertSdlImport(nativeMethod, "SDL_GetSensorType");

        ResetCaptureState();
        nextSensorType = SDL3.SDL.SensorType.Accel;
        IntPtr sensor = (IntPtr)0x7606;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorTypeNativeFunction", nameof(CaptureSensorTypeForSensor));
        SDL3.SDL.SensorType result = SDL3.SDL.GetSensorType(sensor);

        TestAssert.Equal(SDL3.SDL.SensorType.Accel, result, "SDL.GetSensorType must return the native hook value.");
        TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorType must forward sensor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorType must call the native hook once.");
    }

    public static void GetSensorNonPortableType_ForwardsSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorNonPortableType");
        AssertSdlImport(nativeMethod, "SDL_GetSensorNonPortableType");

        ResetCaptureState();
        nextInt = 999;
        IntPtr sensor = (IntPtr)0x7707;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorNonPortableTypeNativeFunction", nameof(CaptureIntForSensor));
        int result = SDL3.SDL.GetSensorNonPortableType(sensor);

        TestAssert.Equal(999, result, "SDL.GetSensorNonPortableType must return the native hook value.");
        TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorNonPortableType must forward sensor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorNonPortableType must call the native hook once.");
    }

    public static void GetSensorID_ForwardsSensorAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorID");
        AssertSdlImport(nativeMethod, "SDL_GetSensorID");

        ResetCaptureState();
        nextUInt = 1234u;
        IntPtr sensor = (IntPtr)0x7808;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorIDNativeFunction", nameof(CaptureUIntForSensor));
        uint result = SDL3.SDL.GetSensorID(sensor);

        TestAssert.Equal(1234u, result, "SDL.GetSensorID must return the native hook value.");
        TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorID must forward sensor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorID must call the native hook once.");
    }

    public static void GetSensorData_ForwardsSensorNumValuesOutputsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSensorData");
        AssertSdlImport(nativeMethod, "SDL_GetSensorData");
        AssertBoolReturnMarshal(nativeMethod);
        AssertOutParameter(nativeMethod, "data");

        ResetCaptureState();
        nextBool = true;
        nextData = 12.5f;
        IntPtr sensor = (IntPtr)0x7909;

        using NativeHookScope _ = NativeHookScope.Install("GetSensorDataNativeFunction", nameof(CaptureGetSensorData));
        bool result = SDL3.SDL.GetSensorData(sensor, out float data, 3);

        TestAssert.Equal(true, result, "SDL.GetSensorData must return the native hook value.");
        TestAssert.Equal(12.5f, data, "SDL.GetSensorData must output native data.");
        TestAssert.Equal(sensor, capturedSensor, "SDL.GetSensorData must forward sensor.");
        TestAssert.Equal(3, capturedNumValues, "SDL.GetSensorData must forward numValues.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetSensorData must call the native hook once.");
    }

    public static void CloseSensor_ForwardsSensor()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CloseSensor");
        AssertSdlImport(nativeMethod, "SDL_CloseSensor");

        ResetCaptureState();
        IntPtr sensor = (IntPtr)0x8001;

        using NativeHookScope _ = NativeHookScope.Install("CloseSensorNativeFunction", nameof(CaptureSensorVoid));
        SDL3.SDL.CloseSensor(sensor);

        TestAssert.Equal(sensor, capturedSensor, "SDL.CloseSensor must forward sensor.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CloseSensor must call the native hook once.");
    }

    public static void UpdateSensors_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UpdateSensors");
        AssertSdlImport(nativeMethod, "SDL_UpdateSensors");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("UpdateSensorsNativeFunction", nameof(CaptureNoArgumentVoid));
        SDL3.SDL.UpdateSensors();

        TestAssert.Equal(1, capturedCallCount, "SDL.UpdateSensors must call the native hook once.");
    }

    private static IntPtr CaptureSensorArrayPointer(out int count)
    {
        capturedCallCount++;
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CapturePointerForInstanceId(int instanceId)
    {
        capturedCallCount++;
        capturedInstanceId = instanceId;
        return nextPointer;
    }

    private static SDL3.SDL.SensorType CaptureSensorTypeForInstanceId(int instanceID)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        return nextSensorType;
    }

    private static int CaptureIntForInstanceId(int instanceID)
    {
        capturedCallCount++;
        capturedInstanceId = instanceID;
        return nextInt;
    }

    private static uint CaptureUIntForSensor(IntPtr sensor)
    {
        capturedCallCount++;
        capturedSensor = sensor;
        return nextUInt;
    }

    private static IntPtr CapturePointerForSensor(IntPtr sensor)
    {
        capturedCallCount++;
        capturedSensor = sensor;
        return nextPointer;
    }

    private static SDL3.SDL.SensorType CaptureSensorTypeForSensor(IntPtr sensor)
    {
        capturedCallCount++;
        capturedSensor = sensor;
        return nextSensorType;
    }

    private static int CaptureIntForSensor(IntPtr sensor)
    {
        capturedCallCount++;
        capturedSensor = sensor;
        return nextInt;
    }

    private static bool CaptureGetSensorData(IntPtr sensor, out float data, int numValues)
    {
        capturedCallCount++;
        capturedSensor = sensor;
        capturedNumValues = numValues;
        data = nextData;
        return nextBool;
    }

    private static void CaptureSensorVoid(IntPtr sensor)
    {
        capturedCallCount++;
        capturedSensor = sensor;
    }

    private static void CaptureNoArgumentVoid()
    {
        capturedCallCount++;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreeCallCount++;
        capturedFreeMemory = mem;
    }

    private static void ResetCaptureState()
    {
        capturedInstanceId = 0;
        capturedSensor = IntPtr.Zero;
        capturedFreeMemory = IntPtr.Zero;
        capturedNumValues = 0;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
        nextPointer = IntPtr.Zero;
        nextCount = 0;
        nextSensorType = default;
        nextInt = 0;
        nextUInt = 0;
        nextBool = false;
        nextData = 0;
    }

    private static IntPtr CreateNativeUIntArray(params uint[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(sizeof(uint) * values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(pointer, i * sizeof(uint), unchecked((int)values[i]));
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

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
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
