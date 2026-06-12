using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Hidapi;

internal static class PInvokeTests
{
    private static int capturedCallCount;
    private static IntPtr capturedDevice;
    private static IntPtr capturedDeviceList;
    private static ushort capturedVendorId;
    private static ushort capturedProductId;
    private static string? capturedSerialNumber;
    private static string? capturedPath;
    private static byte[]? capturedData;
    private static UIntPtr capturedLength;
    private static int capturedMilliseconds;
    private static int capturedNonblock;
    private static IntPtr capturedStringBuffer;
    private static UIntPtr capturedMaxlen;
    private static int capturedStringIndex;
    private static bool capturedActive;
    private static int nextInt;
    private static uint nextUInt;
    private static IntPtr nextPointer;
    private static byte[]? nextData;
    private static string? nextWideString;

    public static void RunAll()
    {
        HIDInit_ReturnsNativeValue();
        HIDExit_ReturnsNativeValue();
        HIDDeviceChangeCount_ReturnsNativeValue();
        HIDEnumerate_ForwardsVendorProductAndReturnsNativePointer();
        HIDFreeEnumeration_ForwardsDeviceList();
        HIDOpen_ForwardsVendorProductSerialAndReturnsNativePointer();
        HIDOpenPath_ForwardsPathAndReturnsNativePointer();
        HIDGetProperties_ForwardsDeviceAndReturnsNativeValue();
        HIDWrite_ForwardsDeviceDataLengthAndReturnsNativeValue();
        HIDReadTimeout_ForwardsDeviceLengthTimeoutOutputsDataAndReturnsNativeValue();
        HIDRead_ForwardsDeviceLengthOutputsDataAndReturnsNativeValue();
        HIDSetNonBlocking_ForwardsDeviceNonblockAndReturnsNativeValue();
        HIDSendFeatureReport_ForwardsDeviceDataLengthAndReturnsNativeValue();
        HIDGetFeatureReport_ForwardsDeviceLengthOutputsDataAndReturnsNativeValue();
        HidGetInputReport_ForwardsDeviceLengthOutputsDataAndReturnsNativeValue();
        HIDClose_ForwardsDeviceAndReturnsNativeValue();
        SDL_hid_get_manufacturer_string_UsesExpectedNativeMetadata();
        HIDGetManufacturerString_ReturnsWideStringAndNativeValue();
        SDL_hid_get_product_string_UsesExpectedNativeMetadata();
        HIDGetProductString_ReturnsWideStringAndNativeValue();
        SDL_hid_get_serial_number_string_UsesExpectedNativeMetadata();
        HIDGetSerialNumberString_ReturnsWideStringAndNativeValue();
        SDL_hid_get_indexed_string_UsesExpectedNativeMetadata();
        HIDGetIndexedString_ReturnsWideStringAndNativeValue();
        HIDGetDeviceInfo_ForwardsDeviceAndReturnsNativePointer();
        HIDGetReportDescriptor_ForwardsDeviceBufferSizeAndReturnsNativeValue();
        HIDBLEScan_ForwardsActive();
    }

    public static void HIDInit_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_init");
        AssertSdlImport(nativeMethod, "SDL_hid_init");

        ResetCaptureState();
        nextInt = 0;

        using NativeHookScope _ = NativeHookScope.Install("HIDInitNativeFunction", nameof(CaptureNoArgumentInt));
        int result = SDL3.SDL.HIDInit();

        TestAssert.Equal(0, result, "SDL.HIDInit must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDInit must call the native hook once.");
    }

    public static void HIDExit_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_exit");
        AssertSdlImport(nativeMethod, "SDL_hid_exit");

        ResetCaptureState();
        nextInt = -2;

        using NativeHookScope _ = NativeHookScope.Install("HIDExitNativeFunction", nameof(CaptureNoArgumentInt));
        int result = SDL3.SDL.HIDExit();

        TestAssert.Equal(-2, result, "SDL.HIDExit must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDExit must call the native hook once.");
    }

    public static void HIDDeviceChangeCount_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_device_change_count");
        AssertSdlImport(nativeMethod, "SDL_hid_device_change_count");

        ResetCaptureState();
        nextUInt = 42u;

        using NativeHookScope _ = NativeHookScope.Install("HIDDeviceChangeCountNativeFunction", nameof(CaptureNoArgumentUInt));
        uint result = SDL3.SDL.HIDDeviceChangeCount();

        TestAssert.Equal(42u, result, "SDL.HIDDeviceChangeCount must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDDeviceChangeCount must call the native hook once.");
    }

    public static void HIDEnumerate_ForwardsVendorProductAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_enumerate");
        AssertSdlImport(nativeMethod, "SDL_hid_enumerate");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7101;

        using NativeHookScope _ = NativeHookScope.Install("HIDEnumerateNativeFunction", nameof(CaptureHIDEnumerate));
        IntPtr result = SDL3.SDL.HIDEnumerate(0x1234, 0x5678);

        TestAssert.Equal((IntPtr)0x7101, result, "SDL.HIDEnumerate must return the native hook value.");
        TestAssert.Equal((ushort)0x1234, capturedVendorId, "SDL.HIDEnumerate must forward vendorId.");
        TestAssert.Equal((ushort)0x5678, capturedProductId, "SDL.HIDEnumerate must forward productId.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDEnumerate must call the native hook once.");
    }

    public static void HIDFreeEnumeration_ForwardsDeviceList()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_free_enumeration");
        AssertSdlImport(nativeMethod, "SDL_hid_free_enumeration");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("HIDFreeEnumerationNativeFunction", nameof(CaptureHIDFreeEnumeration));
        SDL3.SDL.HIDFreeEnumeration((IntPtr)0x7102);

        TestAssert.Equal((IntPtr)0x7102, capturedDeviceList, "SDL.HIDFreeEnumeration must forward device list.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDFreeEnumeration must call the native hook once.");
    }

    public static void HIDOpen_ForwardsVendorProductSerialAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_open");
        AssertSdlImport(nativeMethod, "SDL_hid_open");
        AssertMarshalUsingParameter(nativeMethod, "serialNumber", typeof(WCharStringMarshaller));

        ResetCaptureState();
        nextPointer = (IntPtr)0x7103;

        using NativeHookScope _ = NativeHookScope.Install("HIDOpenNativeFunction", nameof(CaptureHIDOpen));
        IntPtr result = SDL3.SDL.HIDOpen(0x1111, 0x2222, "serial-1");

        TestAssert.Equal((IntPtr)0x7103, result, "SDL.HIDOpen must return the native hook value.");
        TestAssert.Equal((ushort)0x1111, capturedVendorId, "SDL.HIDOpen must forward vendorId.");
        TestAssert.Equal((ushort)0x2222, capturedProductId, "SDL.HIDOpen must forward productId.");
        TestAssert.Equal("serial-1", capturedSerialNumber, "SDL.HIDOpen must forward serialNumber.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDOpen must call the native hook once.");
    }

    public static void HIDOpenPath_ForwardsPathAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_open_path");
        AssertSdlImport(nativeMethod, "SDL_hid_open_path");
        AssertStringParameterMarshal(nativeMethod, "path");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7104;

        using NativeHookScope _ = NativeHookScope.Install("HIDOpenPathNativeFunction", nameof(CaptureHIDOpenPath));
        IntPtr result = SDL3.SDL.HIDOpenPath("/dev/hidraw-test");

        TestAssert.Equal((IntPtr)0x7104, result, "SDL.HIDOpenPath must return the native hook value.");
        TestAssert.Equal("/dev/hidraw-test", capturedPath, "SDL.HIDOpenPath must forward path.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDOpenPath must call the native hook once.");
    }

    public static void HIDGetProperties_ForwardsDeviceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_properties");
        AssertSdlImport(nativeMethod, "SDL_hid_get_properties");

        ResetCaptureState();
        nextUInt = 0xABCDu;

        using NativeHookScope _ = NativeHookScope.Install("HIDGetPropertiesNativeFunction", nameof(CaptureDeviceUInt));
        uint result = SDL3.SDL.HIDGetProperties((IntPtr)0x7201);

        TestAssert.Equal(0xABCDu, result, "SDL.HIDGetProperties must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7201, capturedDevice, "SDL.HIDGetProperties must forward device.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDGetProperties must call the native hook once.");
    }

    public static void HIDWrite_ForwardsDeviceDataLengthAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_write");
        AssertSdlImport(nativeMethod, "SDL_hid_write");
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);

        ResetCaptureState();
        nextInt = 4;
        byte[] data = [0, 1, 2, 3];

        using NativeHookScope _ = NativeHookScope.Install("HIDWriteNativeFunction", nameof(CaptureDeviceDataInt));
        int result = SDL3.SDL.HIDWrite((IntPtr)0x7202, data, (UIntPtr)data.Length);

        TestAssert.Equal(4, result, "SDL.HIDWrite must return the native hook value.");
        AssertDeviceDataLength("SDL.HIDWrite", (IntPtr)0x7202, data, (UIntPtr)4);
    }

    public static void HIDReadTimeout_ForwardsDeviceLengthTimeoutOutputsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_read_timeout");
        AssertSdlImport(nativeMethod, "SDL_hid_read_timeout");
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);
        AssertOutParameter(nativeMethod, "data");

        ResetCaptureState();
        nextInt = 3;
        nextData = [9, 8, 7];

        using NativeHookScope _ = NativeHookScope.Install("HIDReadTimeoutNativeFunction", nameof(CaptureDeviceDataTimeoutOut));
        int result = SDL3.SDL.HIDReadTimeout((IntPtr)0x7203, out byte[] data, (UIntPtr)3, 250);

        TestAssert.Equal(3, result, "SDL.HIDReadTimeout must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7203, capturedDevice, "SDL.HIDReadTimeout must forward device.");
        TestAssert.Equal((UIntPtr)3, capturedLength, "SDL.HIDReadTimeout must forward length.");
        TestAssert.Equal(250, capturedMilliseconds, "SDL.HIDReadTimeout must forward milliseconds.");
        AssertBytes([9, 8, 7], data, "SDL.HIDReadTimeout must output data.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDReadTimeout must call the native hook once.");
    }

    public static void HIDRead_ForwardsDeviceLengthOutputsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_read");
        AssertSdlImport(nativeMethod, "SDL_hid_read");
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);
        AssertOutParameter(nativeMethod, "data");

        ResetCaptureState();
        nextInt = 2;
        nextData = [4, 5];

        using NativeHookScope _ = NativeHookScope.Install("HIDReadNativeFunction", nameof(CaptureDeviceDataOut));
        int result = SDL3.SDL.HIDRead((IntPtr)0x7204, out byte[] data, (UIntPtr)2);

        TestAssert.Equal(2, result, "SDL.HIDRead must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7204, capturedDevice, "SDL.HIDRead must forward device.");
        TestAssert.Equal((UIntPtr)2, capturedLength, "SDL.HIDRead must forward length.");
        AssertBytes([4, 5], data, "SDL.HIDRead must output data.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDRead must call the native hook once.");
    }

    public static void HIDSetNonBlocking_ForwardsDeviceNonblockAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_set_nonblocking");
        AssertSdlImport(nativeMethod, "SDL_hid_set_nonblocking");

        ResetCaptureState();
        nextInt = 0;

        using NativeHookScope _ = NativeHookScope.Install("HIDSetNonBlockingNativeFunction", nameof(CaptureHIDSetNonBlocking));
        int result = SDL3.SDL.HIDSetNonBlocking((IntPtr)0x7205, 1);

        TestAssert.Equal(0, result, "SDL.HIDSetNonBlocking must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7205, capturedDevice, "SDL.HIDSetNonBlocking must forward device.");
        TestAssert.Equal(1, capturedNonblock, "SDL.HIDSetNonBlocking must forward nonblock.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDSetNonBlocking must call the native hook once.");
    }

    public static void HIDSendFeatureReport_ForwardsDeviceDataLengthAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_send_feature_report");
        AssertSdlImport(nativeMethod, "SDL_hid_send_feature_report");
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);

        ResetCaptureState();
        nextInt = 5;
        byte[] data = [1, 2, 3, 4, 5];

        using NativeHookScope _ = NativeHookScope.Install("HIDSendFeatureReportNativeFunction", nameof(CaptureDeviceDataInt));
        int result = SDL3.SDL.HIDSendFeatureReport((IntPtr)0x7206, data, (UIntPtr)data.Length);

        TestAssert.Equal(5, result, "SDL.HIDSendFeatureReport must return the native hook value.");
        AssertDeviceDataLength("SDL.HIDSendFeatureReport", (IntPtr)0x7206, data, (UIntPtr)5);
    }

    public static void HIDGetFeatureReport_ForwardsDeviceLengthOutputsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_feature_report");
        AssertSdlImport(nativeMethod, "SDL_hid_get_feature_report");
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);
        AssertOutParameter(nativeMethod, "data");

        ResetCaptureState();
        nextInt = 4;
        nextData = [7, 6, 5, 4];

        using NativeHookScope _ = NativeHookScope.Install("HIDGetFeatureReportNativeFunction", nameof(CaptureDeviceDataOut));
        int result = SDL3.SDL.HIDGetFeatureReport((IntPtr)0x7207, out byte[] data, (UIntPtr)4);

        TestAssert.Equal(4, result, "SDL.HIDGetFeatureReport must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7207, capturedDevice, "SDL.HIDGetFeatureReport must forward device.");
        TestAssert.Equal((UIntPtr)4, capturedLength, "SDL.HIDGetFeatureReport must forward length.");
        AssertBytes([7, 6, 5, 4], data, "SDL.HIDGetFeatureReport must output data.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDGetFeatureReport must call the native hook once.");
    }

    public static void HidGetInputReport_ForwardsDeviceLengthOutputsDataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_input_report");
        AssertSdlImport(nativeMethod, "SDL_hid_get_input_report");
        AssertArrayParameterMarshal(nativeMethod, "data", UnmanagedType.LPArray, 2);
        AssertOutParameter(nativeMethod, "data");

        ResetCaptureState();
        nextInt = 2;
        nextData = [3, 2];

        using NativeHookScope _ = NativeHookScope.Install("HidGetInputReportNativeFunction", nameof(CaptureDeviceDataOut));
        int result = SDL3.SDL.HidGetInputReport((IntPtr)0x7208, out byte[] data, (UIntPtr)2);

        TestAssert.Equal(2, result, "SDL.HidGetInputReport must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7208, capturedDevice, "SDL.HidGetInputReport must forward device.");
        TestAssert.Equal((UIntPtr)2, capturedLength, "SDL.HidGetInputReport must forward length.");
        AssertBytes([3, 2], data, "SDL.HidGetInputReport must output data.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HidGetInputReport must call the native hook once.");
    }

    public static void HIDClose_ForwardsDeviceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_close");
        AssertSdlImport(nativeMethod, "SDL_hid_close");

        ResetCaptureState();
        nextInt = 0;

        using NativeHookScope _ = NativeHookScope.Install("HIDCloseNativeFunction", nameof(CaptureDeviceInt));
        int result = SDL3.SDL.HIDClose((IntPtr)0x7209);

        TestAssert.Equal(0, result, "SDL.HIDClose must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7209, capturedDevice, "SDL.HIDClose must forward device.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDClose must call the native hook once.");
    }

    public static void SDL_hid_get_manufacturer_string_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_manufacturer_string");
        AssertSdlImport(nativeMethod, "SDL_hid_get_manufacturer_string");
    }

    public static void HIDGetManufacturerString_ReturnsWideStringAndNativeValue()
    {
        AssertWideStringMethod(
            "SDL.HIDGetManufacturerString",
            "HIDGetManufacturerStringNativeFunction",
            nameof(CaptureWideString),
            (IntPtr)0x7301,
            16,
            "Acme",
            static (IntPtr dev, UIntPtr maxlen, out string value) => SDL3.SDL.HIDGetManufacturerString(dev, out value, maxlen));
    }

    public static void SDL_hid_get_product_string_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_product_string");
        AssertSdlImport(nativeMethod, "SDL_hid_get_product_string");
    }

    public static void HIDGetProductString_ReturnsWideStringAndNativeValue()
    {
        AssertWideStringMethod(
            "SDL.HIDGetProductString",
            "HIDGetProductStringNativeFunction",
            nameof(CaptureWideString),
            (IntPtr)0x7302,
            16,
            "Pad",
            static (IntPtr dev, UIntPtr maxlen, out string value) => SDL3.SDL.HIDGetProductString(dev, out value, maxlen));
    }

    public static void SDL_hid_get_serial_number_string_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_serial_number_string");
        AssertSdlImport(nativeMethod, "SDL_hid_get_serial_number_string");
    }

    public static void HIDGetSerialNumberString_ReturnsWideStringAndNativeValue()
    {
        AssertWideStringMethod(
            "SDL.HIDGetSerialNumberString",
            "HIDGetSerialNumberStringNativeFunction",
            nameof(CaptureWideString),
            (IntPtr)0x7303,
            16,
            "SN-001",
            static (IntPtr dev, UIntPtr maxlen, out string value) => SDL3.SDL.HIDGetSerialNumberString(dev, out value, maxlen));
    }

    public static void SDL_hid_get_indexed_string_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_indexed_string");
        AssertSdlImport(nativeMethod, "SDL_hid_get_indexed_string");
    }

    public static void HIDGetIndexedString_ReturnsWideStringAndNativeValue()
    {
        ResetCaptureState();
        nextInt = 0;
        nextWideString = "Indexed";

        using NativeHookScope _ = NativeHookScope.Install("HIDGetIndexedStringNativeFunction", nameof(CaptureIndexedWideString));
        int result = SDL3.SDL.HIDGetIndexedString((IntPtr)0x7304, 4, out string value, (UIntPtr)16);

        TestAssert.Equal(0, result, "SDL.HIDGetIndexedString must return the native hook value.");
        TestAssert.Equal("Indexed", value, "SDL.HIDGetIndexedString must convert the wide string buffer.");
        TestAssert.Equal((IntPtr)0x7304, capturedDevice, "SDL.HIDGetIndexedString must forward device.");
        TestAssert.Equal(4, capturedStringIndex, "SDL.HIDGetIndexedString must forward stringIndex.");
        TestAssert.Equal((UIntPtr)16, capturedMaxlen, "SDL.HIDGetIndexedString must forward maxlen in wchar units.");
        TestAssert.True(capturedStringBuffer != IntPtr.Zero, "SDL.HIDGetIndexedString must allocate a native string buffer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDGetIndexedString must call the native hook once.");
    }

    public static void HIDGetDeviceInfo_ForwardsDeviceAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_device_info");
        AssertSdlImport(nativeMethod, "SDL_hid_get_device_info");

        ResetCaptureState();
        nextPointer = (IntPtr)0x7401;

        using NativeHookScope _ = NativeHookScope.Install("HIDGetDeviceInfoNativeFunction", nameof(CaptureDevicePointer));
        IntPtr result = SDL3.SDL.HIDGetDeviceInfo((IntPtr)0x7402);

        TestAssert.Equal((IntPtr)0x7401, result, "SDL.HIDGetDeviceInfo must return the native hook value.");
        TestAssert.Equal((IntPtr)0x7402, capturedDevice, "SDL.HIDGetDeviceInfo must forward device.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDGetDeviceInfo must call the native hook once.");
    }

    public static void HIDGetReportDescriptor_ForwardsDeviceBufferSizeAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_get_report_descriptor");
        AssertSdlImport(nativeMethod, "SDL_hid_get_report_descriptor");
        AssertArrayParameterMarshal(nativeMethod, "buf", UnmanagedType.LPArray, 2);

        ResetCaptureState();
        nextInt = 3;
        byte[] buffer = [0, 0, 0];

        using NativeHookScope _ = NativeHookScope.Install("HIDGetReportDescriptorNativeFunction", nameof(CaptureDeviceDataInt));
        int result = SDL3.SDL.HIDGetReportDescriptor((IntPtr)0x7403, buffer, (UIntPtr)buffer.Length);

        TestAssert.Equal(3, result, "SDL.HIDGetReportDescriptor must return the native hook value.");
        AssertDeviceDataLength("SDL.HIDGetReportDescriptor", (IntPtr)0x7403, buffer, (UIntPtr)3);
    }

    public static void HIDBLEScan_ForwardsActive()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_hid_ble_scan");
        AssertSdlImport(nativeMethod, "SDL_hid_ble_scan");
        AssertBoolParameterMarshal(nativeMethod, "active");

        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("HIDBLEScanNativeFunction", nameof(CaptureBLEScan));
        SDL3.SDL.HIDBLEScan(active: true);

        TestAssert.Equal(true, capturedActive, "SDL.HIDBLEScan must forward active.");
        TestAssert.Equal(1, capturedCallCount, "SDL.HIDBLEScan must call the native hook once.");
    }

    private static void AssertWideStringMethod(string apiName, string hookFieldName, string hookMethodName, IntPtr device, int maxlen, string nativeValue, WideStringCall call)
    {
        ResetCaptureState();
        nextInt = 0;
        nextWideString = nativeValue;

        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);
        int result = call(device, (UIntPtr)maxlen, out string value);

        TestAssert.Equal(0, result, $"{apiName} must return the native hook value.");
        TestAssert.Equal(nativeValue, value, $"{apiName} must convert the wide string buffer.");
        TestAssert.Equal(device, capturedDevice, $"{apiName} must forward device.");
        TestAssert.Equal((UIntPtr)maxlen, capturedMaxlen, $"{apiName} must forward maxlen in wchar units.");
        TestAssert.True(capturedStringBuffer != IntPtr.Zero, $"{apiName} must allocate a native string buffer.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertDeviceDataLength(string apiName, IntPtr device, byte[] data, UIntPtr length)
    {
        TestAssert.Equal(device, capturedDevice, $"{apiName} must forward device.");
        TestAssert.Equal(data, capturedData, $"{apiName} must forward the same data array.");
        TestAssert.Equal(length, capturedLength, $"{apiName} must forward length.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call the native hook once.");
    }

    private static void AssertBytes(byte[] expected, byte[] actual, string message)
    {
        TestAssert.Equal(expected.Length, actual.Length, message);
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i], actual[i], $"{message} Byte {i} must match.");
        }
    }

    private static int CaptureNoArgumentInt()
    {
        capturedCallCount++;
        return nextInt;
    }

    private static uint CaptureNoArgumentUInt()
    {
        capturedCallCount++;
        return nextUInt;
    }

    private static IntPtr CaptureHIDEnumerate(ushort vendorId, ushort productId)
    {
        capturedCallCount++;
        capturedVendorId = vendorId;
        capturedProductId = productId;
        return nextPointer;
    }

    private static void CaptureHIDFreeEnumeration(IntPtr devs)
    {
        capturedCallCount++;
        capturedDeviceList = devs;
    }

    private static IntPtr CaptureHIDOpen(ushort vendorId, ushort productId, string? serialNumber)
    {
        capturedCallCount++;
        capturedVendorId = vendorId;
        capturedProductId = productId;
        capturedSerialNumber = serialNumber;
        return nextPointer;
    }

    private static IntPtr CaptureHIDOpenPath(string path)
    {
        capturedCallCount++;
        capturedPath = path;
        return nextPointer;
    }

    private static uint CaptureDeviceUInt(IntPtr dev)
    {
        capturedCallCount++;
        capturedDevice = dev;
        return nextUInt;
    }

    private static int CaptureDeviceInt(IntPtr dev)
    {
        capturedCallCount++;
        capturedDevice = dev;
        return nextInt;
    }

    private static IntPtr CaptureDevicePointer(IntPtr dev)
    {
        capturedCallCount++;
        capturedDevice = dev;
        return nextPointer;
    }

    private static int CaptureDeviceDataInt(IntPtr dev, byte[] data, UIntPtr length)
    {
        capturedCallCount++;
        capturedDevice = dev;
        capturedData = data;
        capturedLength = length;
        return nextInt;
    }

    private static int CaptureDeviceDataTimeoutOut(IntPtr dev, out byte[] data, UIntPtr length, int milliseconds)
    {
        capturedCallCount++;
        capturedDevice = dev;
        capturedLength = length;
        capturedMilliseconds = milliseconds;
        data = nextData ?? [];
        return nextInt;
    }

    private static int CaptureDeviceDataOut(IntPtr dev, out byte[] data, UIntPtr length)
    {
        capturedCallCount++;
        capturedDevice = dev;
        capturedLength = length;
        data = nextData ?? [];
        return nextInt;
    }

    private static int CaptureHIDSetNonBlocking(IntPtr dev, int nonblock)
    {
        capturedCallCount++;
        capturedDevice = dev;
        capturedNonblock = nonblock;
        return nextInt;
    }

    private static int CaptureWideString(IntPtr dev, IntPtr @string, UIntPtr maxlen)
    {
        capturedCallCount++;
        capturedDevice = dev;
        capturedStringBuffer = @string;
        capturedMaxlen = maxlen;
        WriteWideString(@string, maxlen, nextWideString ?? string.Empty);
        return nextInt;
    }

    private static int CaptureIndexedWideString(IntPtr dev, int stringIndex, IntPtr @string, UIntPtr maxlen)
    {
        capturedCallCount++;
        capturedDevice = dev;
        capturedStringIndex = stringIndex;
        capturedStringBuffer = @string;
        capturedMaxlen = maxlen;
        WriteWideString(@string, maxlen, nextWideString ?? string.Empty);
        return nextInt;
    }

    private static void CaptureBLEScan(bool active)
    {
        capturedCallCount++;
        capturedActive = active;
    }

    private static void WriteWideString(IntPtr destination, UIntPtr maxlen, string value)
    {
        IntPtr source = WCharStringMarshaller.ConvertToUnmanaged(value);

        try
        {
            int charSize = (int)WCharStringMarshaller.WCharSize;
            int sourceBytes = (value.Length + 1) * charSize;
            int destinationBytes = checked((int)((ulong)maxlen * (ulong)charSize));
            byte[] bytes = new byte[Math.Min(sourceBytes, destinationBytes)];
            Marshal.Copy(source, bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, destination, bytes.Length);
        }
        finally
        {
            WCharStringMarshaller.Free(source);
        }
    }

    private static void ResetCaptureState()
    {
        capturedCallCount = 0;
        capturedDevice = IntPtr.Zero;
        capturedDeviceList = IntPtr.Zero;
        capturedVendorId = 0;
        capturedProductId = 0;
        capturedSerialNumber = null;
        capturedPath = null;
        capturedData = null;
        capturedLength = UIntPtr.Zero;
        capturedMilliseconds = 0;
        capturedNonblock = 0;
        capturedStringBuffer = IntPtr.Zero;
        capturedMaxlen = UIntPtr.Zero;
        capturedStringIndex = 0;
        capturedActive = false;
        nextInt = 0;
        nextUInt = 0;
        nextPointer = IntPtr.Zero;
        nextData = null;
        nextWideString = null;
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

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 marshalling.");
    }

    private static void AssertMarshalUsingParameter(MethodInfo method, string parameterName, Type nativeType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalUsingAttribute? marshalUsing = parameter.GetCustomAttribute<MarshalUsingAttribute>();
        TestAssert.NotNull(marshalUsing, $"SDL.{method.Name} parameter {parameterName} must keep MarshalUsing metadata.");
        TestAssert.Equal(nativeType, marshalUsing!.NativeType, $"SDL.{method.Name} parameter {parameterName} must use the expected custom marshaller.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType, short sizeParamIndex)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected array marshalling.");
        TestAssert.Equal(sizeParamIndex, marshalAs.SizeParamIndex, $"SDL.{method.Name} parameter {parameterName} must keep expected SizeParamIndex.");
    }

    private static void AssertOutParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.IsOut, $"SDL.{method.Name} parameter {parameterName} must be an out parameter.");
    }

    private delegate int WideStringCall(IntPtr dev, UIntPtr maxlen, out string value);

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
