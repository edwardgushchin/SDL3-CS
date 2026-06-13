#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_init();
    private delegate int HIDInitNativeDelegate();
    private static HIDInitNativeDelegate HIDInitNativeFunction = SDL_hid_init;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_init(void);</code>
    /// <summary>
    /// <para>Initialize the HIDAPI library.</para>
    /// <para>This function initializes the HIDAPI library. Calling it is not strictly
    /// necessary, as it will be called automatically by <see cref="HIDEnumerate"/>,
    /// <see cref="HIDOpen"/>, and <see cref="HIDOpenPath"/> if needed. This function should be
    /// called at the beginning of execution however, if there is a chance of
    /// HIDAPI handles being opened by different threads simultaneously.</para>
    /// <para>Each call to this function should have a matching call to <see cref="HIDExit"/></para>
    /// </summary>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HIDExit"/>
    public static int HIDInit()
    {
        return HIDInitNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_exit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_exit();
    private delegate int HIDExitNativeDelegate();
    private static HIDExitNativeDelegate HIDExitNativeFunction = SDL_hid_exit;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_exit(void);</code>
    /// <summary>
    /// <para>Finalize the HIDAPI library.</para>
    /// <para>This function frees all of the static data associated with HIDAPI. It
    /// should be called at the end of execution to avoid memory leaks.</para>
    /// </summary>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <see cref="HIDInit"/>
    public static int HIDExit()
    {
        return HIDExitNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_device_change_count"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_hid_device_change_count();
    private delegate uint HIDDeviceChangeCountNativeDelegate();
    private static HIDDeviceChangeCountNativeDelegate HIDDeviceChangeCountNativeFunction = SDL_hid_device_change_count;

    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_hid_device_change_count(void);</code>
    /// <summary>
    /// <para>Check to see if devices may have been added or removed.</para>
    /// <para>Enumerating the HID devices is an expensive operation, so you can call this
    /// to see if there have been any system device changes since the last call to
    /// this function. A change in the counter returned doesn't necessarily mean
    /// that anything has changed, but you can call <see cref="HIDEnumerate"/> to get an
    /// updated device list.</para>
    /// <para>Calling this function for the first time may cause a thread or other system
    /// resource to be allocated to track device change notifications.</para>
    /// </summary>
    /// <returns>a change counter that is incremented with each potential device
    /// change, or 0 if device change detection isn't available.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HIDEnumerate"/>
    public static uint HIDDeviceChangeCount()
    {
        return HIDDeviceChangeCountNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_enumerate"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_enumerate(ushort vendorId, ushort productId);
    private delegate IntPtr HIDEnumerateNativeDelegate(ushort vendorId, ushort productId);
    private static HIDEnumerateNativeDelegate HIDEnumerateNativeFunction = SDL_hid_enumerate;

    /// <code>extern SDL_DECLSPEC SDL_hid_device_info * SDLCALL SDL_hid_enumerate(unsigned short vendor_id, unsigned short product_id);</code>
    /// <summary>
    /// <para>Enumerate the HID Devices.</para>
    /// <para>This function returns a linked list of all the HID devices attached to the
    /// system which match <c>vendorId</c> and <c>productId</c>. If <c>vendorId</c> is set to 0
    /// then any vendor matches. If <c>productId</c> is set to 0 then any product
    /// matches. If <c>vendorId</c> and <c>productId</c> are both set to 0, then all HID
    /// devices will be returned.</para>
    /// <para>By default SDL will only enumerate controllers, to reduce risk of hanging
    /// or crashing on bad drivers, but <see cref="Hints.HIDAPIEnumerateOnlyControllers"/>
    /// can be set to "0" to enumerate all HID devices.</para>
    /// </summary>
    /// <param name="vendorId">the Vendor ID (VID) of the types of device to open, or 0
    /// to match any vendor.</param>
    /// <param name="productId">the Product ID (PID) of the types of device to open, or 0
    /// to match any product.</param>
    /// <returns>a pointer to a linked list of type <see cref="HIDDeviceInfo"/>, containing
    /// information about the HID devices attached to the system, or <c>null</c>
    /// in the case of failure. Free this linked list by calling
    /// <see cref="HIDFreeEnumeration"/>.</returns>
    public static IntPtr HIDEnumerate(ushort vendorId, ushort productId)
    {
        return HIDEnumerateNativeFunction(vendorId, productId);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_free_enumeration"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_free_enumeration(IntPtr devs);
    private delegate void HIDFreeEnumerationNativeDelegate(IntPtr devs);
    private static HIDFreeEnumerationNativeDelegate HIDFreeEnumerationNativeFunction = SDL_hid_free_enumeration;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_hid_free_enumeration(SDL_hid_device_info *devs);</code>
    /// <summary>
    /// <para>Free an enumeration linked list.</para>
    /// <para>This function frees a linked list created by <see cref="HIDEnumerate"/>.</para>
    /// </summary>
    /// <param name="devs">pointer to a list of struct_device returned from
    /// <see cref="HIDEnumerate"/>.</param>
    public static void HIDFreeEnumeration(IntPtr devs)
    {
        HIDFreeEnumerationNativeFunction(devs);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_open"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_open(ushort vendorId, ushort productId,  [MarshalUsing(typeof(WCharStringMarshaller))] string? serialNumber);
    private delegate IntPtr HIDOpenNativeDelegate(ushort vendorId, ushort productId, string? serialNumber);
    private static HIDOpenNativeDelegate HIDOpenNativeFunction = SDL_hid_open;

    /// <code>extern SDL_DECLSPEC SDL_hid_device * SDLCALL SDL_hid_open(unsigned short vendor_id, unsigned short product_id, const wchar_t *serial_number);</code>
    /// <summary>
    /// <para>Open a HID device using a Vendor ID (VID), Product ID (PID) and optionally
    /// a serial number.</para>
    /// <para>If <c>serialNumber</c> is <c>null</c>, the first device with the specified VID and PID
    /// is opened.</para>
    /// </summary>
    /// <param name="vendorId">the Vendor ID (VID) of the device to open.</param>
    /// <param name="productId">the Product ID (PID) of the device to open.</param>
    /// <param name="serialNumber">the Serial Number of the device to open (Optionally
    /// <c>null</c>).</param>
    /// <returns>a pointer to a SDL_hid_device object on success or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr HIDOpen(ushort vendorId, ushort productId, string? serialNumber)
    {
        return HIDOpenNativeFunction(vendorId, productId, serialNumber);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_open_path"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_open_path([MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    private delegate IntPtr HIDOpenPathNativeDelegate(string path);
    private static HIDOpenPathNativeDelegate HIDOpenPathNativeFunction = SDL_hid_open_path;

    /// <code>extern SDL_DECLSPEC SDL_hid_device * SDLCALL SDL_hid_open_path(const char *path);</code>
    /// <summary>
    /// <para>Open a HID device by its path name.</para>
    /// <para>The path name be determined by calling <see cref="HIDEnumerate"/>, or a
    /// platform-specific path name can be used (eg: /dev/hidraw0 on Linux).</para>
    /// </summary>
    /// <param name="path">the path name of the device to open.</param>
    /// <returns>a pointer to a SDL_hid_device object on success or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr HIDOpenPath(string path)
    {
        return HIDOpenPathNativeFunction(path);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_properties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_hid_get_properties(IntPtr dev);
    private delegate uint HIDGetPropertiesNativeDelegate(IntPtr dev);
    private static HIDGetPropertiesNativeDelegate HIDGetPropertiesNativeFunction = SDL_hid_get_properties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_hid_get_properties(SDL_hid_device *dev);</code>
    /// <summary>
    /// <para>Get the properties associated with an SDL_hid_device.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.HIDAPILibUSBDeviceHandlePointer"/>: the libusb_device_handle
    /// associated with the device, if it was opened using libus
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="dev">a device handle returned from SDL_hid_open().</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static uint HIDGetProperties(IntPtr dev)
    {
        return HIDGetPropertiesNativeFunction(dev);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_write"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_write(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, UIntPtr length);
    private delegate int HIDWriteNativeDelegate(IntPtr dev, byte[] data, UIntPtr length);
    private static HIDWriteNativeDelegate HIDWriteNativeFunction = SDL_hid_write;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_write(SDL_hid_device *dev, const unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Write an Output report to a HID device.</para>
    /// <para>The first byte of <c>data</c> must contain the Report ID. For devices which only
    /// support a single report, this must be set to 0x0. The remaining bytes
    /// contain the report data. Since the Report ID is mandatory, calls to
    /// <see cref="HIDWrite(nint, byte[], UIntPtr)"/> will always contain one more byte than the report contains.
    /// For example, if a hid report is 16 bytes long, 17 bytes must be passed to
    /// <see cref="HIDWrite(nint, byte[], UIntPtr)"/>, the Report ID (or 0x0, for devices with a single report),
    /// followed by the report data (16 bytes). In this example, the length passed
    /// in would be 17.</para>
    /// <para><see cref="HIDWrite(nint, byte[], UIntPtr)"/> will send the data on the first OUT endpoint, if one
    /// exists. If it does not, it will send the data through the Control Endpoint
    /// (Endpoint 0).</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpenPath"/>.</param>
    /// <param name="data">the data to send, including the report number as the first
    /// byte.</param>
    /// <param name="length">the length in bytes of the data to send.</param>
    /// <returns>the actual number of bytes written and -1 on on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDWrite(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, UIntPtr length)
    {
        return HIDWriteNativeFunction(dev, data, length);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_write"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_write(IntPtr dev, IntPtr data, UIntPtr length);
    private delegate int HIDWritePointerNativeDelegate(IntPtr dev, IntPtr data, UIntPtr length);
    private static HIDWritePointerNativeDelegate HIDWritePointerNativeFunction = SDL_hid_write;

    /// <inheritdoc cref="HIDWrite(nint, byte[], UIntPtr)"/>
    public static unsafe int HIDWrite(IntPtr dev, ReadOnlySpan<byte> data, UIntPtr length)
    {
        fixed (byte* pData = data)
        {
            return HIDWritePointerNativeFunction(dev, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_read_timeout"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read_timeout(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length, int milliseconds);
    private delegate int HIDReadTimeoutNativeDelegate(IntPtr dev, out byte[] data, UIntPtr length, int milliseconds);
    private static HIDReadTimeoutNativeDelegate HIDReadTimeoutNativeFunction = SDL_hid_read_timeout;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_read_timeout(SDL_hid_device *dev, unsigned char *data, size_t length, int milliseconds);</code>
    /// <summary>
    /// <para>Read an Input report from a HID device with timeout.</para>
    /// <para>Input reports are returned to the host through the INTERRUPT IN endpoint.
    /// The first byte will contain the Report number if the device uses numbered
    /// reports.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">a buffer to put the read data into.</param>
    /// <param name="length">the number of bytes to read. For devices with multiple
    /// reports, make sure to read an extra byte for the report
    /// number.</param>
    /// <param name="milliseconds">timeout in milliseconds or -1 for blocking wait.</param>
    /// <returns>the actual number of bytes read and -1 on on failure; call
    /// <see cref="GetError"/> for more information. If no packet was available to
    /// be read within the timeout period, this function returns 0.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDReadTimeout(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length, int milliseconds)
    {
        return HIDReadTimeoutNativeFunction(dev, out data, length, milliseconds);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_read_timeout"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read_timeout(IntPtr dev, IntPtr data, UIntPtr length, int milliseconds);
    private delegate int HIDReadTimeoutPointerNativeDelegate(IntPtr dev, IntPtr data, UIntPtr length, int milliseconds);
    private static HIDReadTimeoutPointerNativeDelegate HIDReadTimeoutPointerNativeFunction = SDL_hid_read_timeout;

    /// <inheritdoc cref="HIDReadTimeout(nint, out byte[], UIntPtr, int)"/>
    public static unsafe int HIDReadTimeout(IntPtr dev, Span<byte> data, UIntPtr length, int milliseconds)
    {
        fixed (byte* pData = data)
        {
            return HIDReadTimeoutPointerNativeFunction(dev, (IntPtr)pData, length, milliseconds);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_read"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length);
    private delegate int HIDReadNativeDelegate(IntPtr dev, out byte[] data, UIntPtr length);
    private static HIDReadNativeDelegate HIDReadNativeFunction = SDL_hid_read;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_read(SDL_hid_device *dev, unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Read an Input report from a HID device.</para>
    /// <para>Input reports are returned to the host through the INTERRUPT IN endpoint.
    /// The first byte will contain the Report number if the device uses numbered
    /// reports.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">a buffer to put the read data into.</param>
    /// <param name="length">the number of bytes to read. For devices with multiple
    /// reports, make sure to read an extra byte for the report
    /// number.</param>
    /// <returns>the actual number of bytes read and -1 on failure; call
    /// <see cref="GetError"/> for more information. If no packet was available to
    /// be read and the handle is in non-blocking mode, this function
    /// returns 0.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDRead(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length)
    {
        return HIDReadNativeFunction(dev, out data, length);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_read"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read(IntPtr dev, IntPtr data, UIntPtr length);
    private delegate int HIDReadPointerNativeDelegate(IntPtr dev, IntPtr data, UIntPtr length);
    private static HIDReadPointerNativeDelegate HIDReadPointerNativeFunction = SDL_hid_read;

    /// <inheritdoc cref="HIDRead(nint, out byte[], UIntPtr)"/>
    public static unsafe int HIDRead(IntPtr dev, Span<byte> data, UIntPtr length)
    {
        fixed (byte* pData = data)
        {
            return HIDReadPointerNativeFunction(dev, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_set_nonblocking"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_set_nonblocking(IntPtr dev, int nonblock);
    private delegate int HIDSetNonBlockingNativeDelegate(IntPtr dev, int nonblock);
    private static HIDSetNonBlockingNativeDelegate HIDSetNonBlockingNativeFunction = SDL_hid_set_nonblocking;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_set_nonblocking(SDL_hid_device *dev, int nonblock);</code>
    /// <summary>
    /// <para>Set the device handle to be non-blocking.</para>
    /// <para>In non-blocking mode calls to <see cref="HIDRead(nint, out byte[], UIntPtr)"/> will return immediately with a
    /// value of 0 if there is no data to be read. In blocking mode, <see cref="HIDRead(nint, out byte[], UIntPtr)"/>
    /// will wait (block) until there is data to read before returning.</para>
    /// <para>Nonblocking can be turned on and off at any time.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="nonblock">enable or not the nonblocking reads - 1 to enable
    /// nonblocking - 0 to disable nonblocking.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int HIDSetNonBlocking(IntPtr dev, int nonblock)
    {
        return HIDSetNonBlockingNativeFunction(dev, nonblock);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_send_feature_report"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_send_feature_report(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, UIntPtr length);
    private delegate int HIDSendFeatureReportNativeDelegate(IntPtr dev, byte[] data, UIntPtr length);
    private static HIDSendFeatureReportNativeDelegate HIDSendFeatureReportNativeFunction = SDL_hid_send_feature_report;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_send_feature_report(SDL_hid_device *dev, const unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Send a Feature report to the device.</para>
    /// <para>Feature reports are sent over the Control endpoint as a Set_Report
    /// transfer. The first byte of <c>data</c> must contain the Report ID. For devices
    /// which only support a single report, this must be set to 0x0. The remaining
    /// bytes contain the report data. Since the Report ID is mandatory, calls to
    /// <see cref="HIDSendFeatureReport(nint, byte[], UIntPtr)"/> will always contain one more byte than the
    /// report contains. For example, if a hid report is 16 bytes long, 17 bytes
    /// must be passed to <see cref="HIDSendFeatureReport(nint, byte[], UIntPtr)"/>: the Report ID (or 0x0, for
    /// devices which do not use numbered reports), followed by the report data (16
    /// bytes). In this example, the length passed in would be 17.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">the data to send, including the report number as the first
    /// byte.</param>
    /// <param name="length">the length in bytes of the data to send, including the report
    /// number.</param>
    /// <returns>the actual number of bytes written and -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDSendFeatureReport(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, UIntPtr length)
    {
        return HIDSendFeatureReportNativeFunction(dev, data, length);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_send_feature_report"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_send_feature_report(IntPtr dev, IntPtr data, UIntPtr length);
    private delegate int HIDSendFeatureReportPointerNativeDelegate(IntPtr dev, IntPtr data, UIntPtr length);
    private static HIDSendFeatureReportPointerNativeDelegate HIDSendFeatureReportPointerNativeFunction = SDL_hid_send_feature_report;

    /// <inheritdoc cref="HIDSendFeatureReport(nint, byte[], UIntPtr)"/>
    public static unsafe int HIDSendFeatureReport(IntPtr dev, ReadOnlySpan<byte> data, UIntPtr length)
    {
        fixed (byte* pData = data)
        {
            return HIDSendFeatureReportPointerNativeFunction(dev, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_feature_report"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_feature_report(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length);
    private delegate int HIDGetFeatureReportNativeDelegate(IntPtr dev, out byte[] data, UIntPtr length);
    private static HIDGetFeatureReportNativeDelegate HIDGetFeatureReportNativeFunction = SDL_hid_get_feature_report;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_feature_report(SDL_hid_device *dev, unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Get a feature report from a HID device.</para>
    /// <para>Set the first byte of <c>data</c> to the Report ID of the report to be read.
    /// Make sure to allow space for this extra byte in <c>data</c>. Upon return, the
    /// first byte will still contain the Report ID, and the report data will start
    /// in data[1].</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">a buffer to put the read data into, including the Report ID.
    /// Set the first byte of <c>data</c> to the Report ID of the report to
    /// be read, or set it to zero if your device does not use numbered
    /// reports.</param>
    /// <param name="length">the number of bytes to read, including an extra byte for the
    /// report ID. The buffer can be longer than the actual report.</param>
    /// <returns>the number of bytes read plus one for the report ID (which is
    /// still in the first byte), or -1 on on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDGetFeatureReport(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length)
    {
        return HIDGetFeatureReportNativeFunction(dev, out data, length);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_feature_report"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_feature_report(IntPtr dev, IntPtr data, UIntPtr length);
    private delegate int HIDGetFeatureReportPointerNativeDelegate(IntPtr dev, IntPtr data, UIntPtr length);
    private static HIDGetFeatureReportPointerNativeDelegate HIDGetFeatureReportPointerNativeFunction = SDL_hid_get_feature_report;

    /// <inheritdoc cref="HIDGetFeatureReport(nint, out byte[], UIntPtr)"/>
    public static unsafe int HIDGetFeatureReport(IntPtr dev, Span<byte> data, UIntPtr length)
    {
        fixed (byte* pData = data)
        {
            return HIDGetFeatureReportPointerNativeFunction(dev, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_input_report"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_input_report(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length);
    private delegate int HidGetInputReportNativeDelegate(IntPtr dev, out byte[] data, UIntPtr length);
    private static HidGetInputReportNativeDelegate HidGetInputReportNativeFunction = SDL_hid_get_input_report;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_input_report(SDL_hid_device *dev, unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Get an input report from a HID device.</para>
    /// <para>Set the first byte of <c>data</c> to the Report ID of the report to be read.
    /// Make sure to allow space for this extra byte in <c>data</c>. Upon return, the
    /// first byte will still contain the Report ID, and the report data will start
    /// in data[1].</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">a buffer to put the read data into, including the Report ID.
    /// Set the first byte of <c>data</c> to the Report ID of the report to
    /// be read, or set it to zero if your device does not use numbered
    /// reports.</param>
    /// <param name="length">the number of bytes to read, including an extra byte for the
    /// report ID. The buffer can be longer than the actual report.</param>
    /// <returns>the number of bytes read plus one for the report ID (which is
    /// still in the first byte), or -1 on on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HidGetInputReport(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, UIntPtr length)
    {
        return HidGetInputReportNativeFunction(dev, out data, length);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_input_report"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_input_report(IntPtr dev, IntPtr data, UIntPtr length);
    private delegate int HidGetInputReportPointerNativeDelegate(IntPtr dev, IntPtr data, UIntPtr length);
    private static HidGetInputReportPointerNativeDelegate HidGetInputReportPointerNativeFunction = SDL_hid_get_input_report;

    /// <inheritdoc cref="HidGetInputReport(nint, out byte[], UIntPtr)"/>
    public static unsafe int HidGetInputReport(IntPtr dev, Span<byte> data, UIntPtr length)
    {
        fixed (byte* pData = data)
        {
            return HidGetInputReportPointerNativeFunction(dev, (IntPtr)pData, length);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_close"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_close(IntPtr dev);
    private delegate int HIDCloseNativeDelegate(IntPtr dev);
    private static HIDCloseNativeDelegate HIDCloseNativeFunction = SDL_hid_close;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_close(SDL_hid_device *dev);</code>
    /// <summary>
    /// Close a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDClose(IntPtr dev)
    {
        return HIDCloseNativeFunction(dev);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_manufacturer_string"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_manufacturer_string(IntPtr dev, IntPtr @string, UIntPtr maxlen);
    private delegate int HIDGetManufacturerStringNativeDelegate(IntPtr dev, IntPtr @string, UIntPtr maxlen);
    private static HIDGetManufacturerStringNativeDelegate HIDGetManufacturerStringNativeFunction = SDL_hid_get_manufacturer_string;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_manufacturer_string(SDL_hid_device *dev, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get The Manufacturer String from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="string">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of wchar_t.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDGetManufacturerString(IntPtr dev, out string @string, UIntPtr maxlen)
    {
        // Allocate a buffer for maxlen characters
        var maxbytes = unchecked(WCharStringMarshaller.WCharSize * maxlen);
        var buf = Marshal.AllocHGlobal((int)maxbytes);
        maxlen = maxbytes / WCharStringMarshaller.WCharSize; // In case the previous multiplication overflowed

        try
        {
            // Call original function to populate the buffer
            var result = HIDGetManufacturerStringNativeFunction(dev, buf, maxlen);
            // Convert contents of buffer into managed string
            @string = WCharStringMarshaller.ConvertToManaged(buf)!;
            return result;
        }
        finally {
            Marshal.FreeHGlobal(buf);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_product_string"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_product_string(IntPtr dev, IntPtr @string, UIntPtr maxlen);
    private delegate int HIDGetProductStringNativeDelegate(IntPtr dev, IntPtr @string, UIntPtr maxlen);
    private static HIDGetProductStringNativeDelegate HIDGetProductStringNativeFunction = SDL_hid_get_product_string;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_product_string(SDL_hid_device *dev, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get The Product String from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="string">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of wchar_t.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    public static int HIDGetProductString(IntPtr dev, out string @string, UIntPtr maxlen)
    {
        // Allocate a buffer for maxlen characters
        var maxbytes = unchecked(WCharStringMarshaller.WCharSize * maxlen);
        var buf = Marshal.AllocHGlobal((int)maxbytes);
        maxlen = maxbytes / WCharStringMarshaller.WCharSize; // In case the previous multiplication overflowed

        try
        {
            // Call original function to populate the buffer
            var result = HIDGetProductStringNativeFunction(dev, buf, maxlen);
            // Convert contents of buffer into managed string
            @string = WCharStringMarshaller.ConvertToManaged(buf)!;
            return result;
        }
        finally {
            Marshal.FreeHGlobal(buf);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_serial_number_string"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_serial_number_string(IntPtr dev, IntPtr @string, UIntPtr maxlen);
    private delegate int HIDGetSerialNumberStringNativeDelegate(IntPtr dev, IntPtr @string, UIntPtr maxlen);
    private static HIDGetSerialNumberStringNativeDelegate HIDGetSerialNumberStringNativeFunction = SDL_hid_get_serial_number_string;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_serial_number_string(SDL_hid_device *dev, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get The Serial Number String from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="string">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of wchar_t.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDGetSerialNumberString(IntPtr dev, out string @string, UIntPtr maxlen)
    {
        // Allocate a buffer for maxlen characters
        var maxbytes = unchecked(WCharStringMarshaller.WCharSize * maxlen);
        var buf = Marshal.AllocHGlobal((int)maxbytes);
        maxlen = maxbytes / WCharStringMarshaller.WCharSize; // In case the previous multiplication overflowed

        try
        {
            // Call original function to populate the buffer
            var result = HIDGetSerialNumberStringNativeFunction(dev, buf, maxlen);
            // Convert contents of buffer into managed string
            @string = WCharStringMarshaller.ConvertToManaged(buf)!;
            return result;
        }
        finally {
            Marshal.FreeHGlobal(buf);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_indexed_string"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_indexed_string(IntPtr dev, int stringIndex, IntPtr @string, UIntPtr maxlen);
    private delegate int HIDGetIndexedStringNativeDelegate(IntPtr dev, int stringIndex, IntPtr @string, UIntPtr maxlen);
    private static HIDGetIndexedStringNativeDelegate HIDGetIndexedStringNativeFunction = SDL_hid_get_indexed_string;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_indexed_string(SDL_hid_device *dev, int string_index, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get a string from a HID device, based on its string index.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="stringIndex">the index of the string to get.</param>
    /// <param name="string">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of wchar_t.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDGetIndexedString(IntPtr dev, int stringIndex, out string @string, UIntPtr maxlen)
    {
        // Allocate a buffer for maxlen characters
        var maxbytes = unchecked(WCharStringMarshaller.WCharSize * maxlen);
        var buf = Marshal.AllocHGlobal((int)maxbytes);
        maxlen = maxbytes / WCharStringMarshaller.WCharSize; // In case the previous multiplication overflowed

        try
        {
            // Call original function to populate the buffer
            var result = HIDGetIndexedStringNativeFunction(dev, stringIndex, buf, maxlen);
            // Convert contents of buffer into managed string
            @string = WCharStringMarshaller.ConvertToManaged(buf)!;
            return result;
        }
        finally {
            Marshal.FreeHGlobal(buf);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_device_info"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_get_device_info(IntPtr dev);
    private delegate IntPtr HIDGetDeviceInfoNativeDelegate(IntPtr dev);
    private static HIDGetDeviceInfoNativeDelegate HIDGetDeviceInfoNativeFunction = SDL_hid_get_device_info;

    /// <code>extern SDL_DECLSPEC SDL_hid_device_info * SDLCALL SDL_hid_get_device_info(SDL_hid_device *dev);</code>
    /// <summary>
    /// Get the device info from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <returns>a pointer to the SDL_hid_device_info for this hid_device or <c>null</c>
    /// on failure; call <see cref="GetError"/> for more information. This struct
    /// is valid until the device is closed with <see cref="HIDClose"/>.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static IntPtr HIDGetDeviceInfo(IntPtr dev)
    {
        return HIDGetDeviceInfoNativeFunction(dev);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_report_descriptor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_report_descriptor(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, UIntPtr bufSize);
    private delegate int HIDGetReportDescriptorNativeDelegate(IntPtr dev, byte[] buf, UIntPtr bufSize);
    private static HIDGetReportDescriptorNativeDelegate HIDGetReportDescriptorNativeFunction = SDL_hid_get_report_descriptor;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_report_descriptor(SDL_hid_device *dev, unsigned char *buf, size_t buf_size);</code>
    /// <summary>
    /// Get a report descriptor from a HID device.
    /// </summary>
    /// <param name="dev">User has to provide a preallocated buffer where descriptor will be copied
    /// to. The recommended size for a preallocated buffer is 4096 bytes.</param>
    /// <param name="buf">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="bufSize">the buffer to copy descriptor into.</param>
    /// <returns>the number of bytes actually copied or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static int HIDGetReportDescriptor(IntPtr dev, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, UIntPtr bufSize)
    {
        return HIDGetReportDescriptorNativeFunction(dev, buf, bufSize);
    }

    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_get_report_descriptor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_report_descriptor(IntPtr dev, IntPtr buf, UIntPtr bufSize);
    private delegate int HIDGetReportDescriptorPointerNativeDelegate(IntPtr dev, IntPtr buf, UIntPtr bufSize);
    private static HIDGetReportDescriptorPointerNativeDelegate HIDGetReportDescriptorPointerNativeFunction = SDL_hid_get_report_descriptor;

    /// <inheritdoc cref="HIDGetReportDescriptor(nint, byte[], UIntPtr)"/>
    public static unsafe int HIDGetReportDescriptor(IntPtr dev, Span<byte> buf, UIntPtr bufSize)
    {
        fixed (byte* pBuf = buf)
        {
            return HIDGetReportDescriptorPointerNativeFunction(dev, (IntPtr)pBuf, bufSize);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_hid_ble_scan"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_ble_scan([MarshalAs(UnmanagedType.I1)] bool active);
    private delegate void HIDBLEScanNativeDelegate(bool active);
    private static HIDBLEScanNativeDelegate HIDBLEScanNativeFunction = SDL_hid_ble_scan;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_hid_ble_scan(bool active);</code>
    /// <summary>
    /// Start or stop a BLE scan on iOS and tvOS to pair Steam Controllers.
    /// </summary>
    /// <param name="active"><c>true</c> to start the scan, <c>false</c> to stop the scan.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void HIDBLEScan(bool active)
    {
        HIDBLEScanNativeFunction(active);
    }
}
