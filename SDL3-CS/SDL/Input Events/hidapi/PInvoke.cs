#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/*
 * # CategoryHIDAPI
 *
 * Header file for SDL HIDAPI functions.
 *
 * This is an adaptation of the original HIDAPI interface by Alan Ott, and
 * includes source code licensed under the following license:
 *
 * ```
 * HIDAPI - Multi-Platform library for
 * communication with HID devices.
 *
 * Copyright 2009, Alan Ott, Signal 11 Software.
 * All Rights Reserved.
 *
 * This software may be used by anyone for any reason so
 * long as the copyright notice in the source files
 * remains intact.
 * ```
 *
 * (Note that this license is the same as item three of SDL's zlib license, so
 * it adds no new requirements on the user.)
 *
 * If you would like a version of SDL without this code, you can build SDL
 * with SDL_HIDAPI_DISABLED defined to 1. You might want to do this for
 * example on iOS or tvOS to avoid a dependency on the CoreBluetooth
 * framework.
 */

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_init();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_init(void);</code>
    /// <summary>
    /// Initialize the HIDAPI library.
    /// </summary>
    /// <para>This function initializes the HIDAPI library. Calling it is not strictly
    /// necessary, as it will be called automatically by <see cref="HIDEnumerate"/> and
    /// any of the <c>HIDOpen*</c> functions if it is needed. This function should
    /// be called at the beginning of execution however, if there is a chance of
    /// HIDAPI handles being opened by different threads simultaneously.</para>
    /// <para>Each call to this function should have a matching call to <see cref="HIDExit"/>.</para>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HIDExit"/>
    public static int HIDInit() => SDL_hid_init();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_exit();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_exit(void);</code>
    /// <summary>
    /// Finalize the HIDAPI library.
    /// </summary>
    /// <para>This function frees all of the static data associated with HIDAPI. It
    /// should be called at the end of execution to avoid memory leaks.</para>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HIDInit"/>
    public static int HIDExit() => SDL_hid_exit();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_hid_device_change_count();
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_hid_device_change_count(void);</code>
    /// <summary>
    /// Check to see if devices may have been added or removed.
    /// </summary>
    /// <para>Enumerating the HID devices is an expensive operation, so you can call this
    /// to see if there have been any system device changes since the last call to
    /// this function. A change in the counter returned doesn't necessarily mean
    /// that anything has changed, but you can call <see cref="HIDEnumerate"/> to get an
    /// updated device list.</para>
    /// <para>Calling this function for the first time may cause a thread or other system
    /// resource to be allocated to track device change notifications.</para>
    /// <returns>a change counter that is incremented with each potential device
    /// change, or <c>0</c> if device change detection isn't available.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HIDEnumerate"/>
    public static uint HIDDeviceChangeCount() => SDL_hid_device_change_count();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_enumerate(ushort venodrId, ushort productId);
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
    /// can be set to <c>"0"</c> to enumerate all HID devices.</para>
    /// </summary>
    /// <param name="vendorId">the Vendor ID (VID) of the types of device to open, or 0
    /// to match any vendor.</param>
    /// <param name="productId">the Product ID (PID) of the types of device to open, or 0
    /// to match any product.</param>
    /// <returns>a pointer to a linked list of type <see cref="HIDGetDeviceInfo"/>, containing
    /// information about the HID devices attached to the system, or <c>null</c>
    /// in the case of failure. Free this linked list by calling <see cref="SDL_hid_free_enumeration"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HIDDeviceChangeCount"/>
    public static HIDDeviceInfo[]? HIDEnumerate(ushort vendorId, ushort productId)
    {
        var deviceInfoPtr = SDL_hid_enumerate(vendorId, productId);
        if (deviceInfoPtr == IntPtr.Zero)
        {
            return null;
        }

        // Create a list to hold the managed devices
        var deviceList = new List<HIDDeviceInfo>();

        // Traverse the linked list
        var currentPtr = deviceInfoPtr;
        while (currentPtr != IntPtr.Zero)
        {
            var deviceInfo = Marshal.PtrToStructure<HIDDeviceInfo>(currentPtr);
            deviceList.Add(deviceInfo);
            
            // Move to the next node
            var nextPtr = deviceInfo.Next;
            currentPtr = nextPtr;
        }

        // Free the enumeration
        SDL_hid_free_enumeration(deviceInfoPtr);

        return deviceList.ToArray();
    }
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_free_enumeration(IntPtr devs);
    /* I'm not sure how long it is needed in the public domain.
     *
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_hid_free_enumeration(SDL_hid_device_info *devs);</code>
    /// <summary>
    /// Free an enumeration linked list.
    /// </summary>
    /// <para>This function frees a linked list created by <see cref="HIDEnumerate"/>.</para>
    /// <param name="devs">pointer to a list of <see cref="HIDDeviceInfo"/> returned from
    /// <see cref="HIDEnumerate"/>.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    //public static void HIDFreeEnumeration(HIDDeviceInfo devs) => SDL_hid_free_enumeration(devs);*/
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_open(ushort vendorId, ushort productId, 
        [MarshalAs(UnmanagedType.LPWStr)]string serialNumber);
    /// <code>extern SDL_DECLSPEC SDL_hid_device * SDLCALL SDL_hid_open(unsigned short vendor_id, unsigned short product_id, const wchar_t *serial_number);</code>
    /// <summary>
    /// Open a HID device using a Vendor ID (VID), Product ID (PID) and optionally
    /// a serial number.
    /// <para>If <c>serialNumber</c> is <c>null</c>, the first device with the specified VID and PID
    /// is opened.</para>
    /// </summary>
    /// <param name="vendorId">the Vendor ID (VID) of the device to open.</param>
    /// <param name="productId">the Product ID (PID) of the device to open.</param>
    /// <param name="serialNumber">the Serial Number of the device to open (Optionally
    /// <c>null</c>).</param>
    /// <returns>a pointer to a <see cref="HIDDevice"/> object on success or <c>null</c> on
    /// failure.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static HIDDevice? HIDOpen(ushort vendorId, ushort productId, string serialNumber)
    { 
        var hidPtr = SDL_hid_open(vendorId, productId, serialNumber);

        if (hidPtr == IntPtr.Zero) return null;

        return new HIDDevice(hidPtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_open_path([MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    /// <code>extern SDL_DECLSPEC SDL_hid_device * SDLCALL SDL_hid_open_path(const char *path);</code>
    /// <summary>
    /// Open a HID device by its path name.
    /// <para>The path name can be determined by calling <see cref="HIDEnumerate"/>, or a
    /// platform-specific path name can be used (e.g., <c>/dev/hidraw0</c> on Linux).</para>
    /// </summary>
    /// <param name="path">the path name of the device to open.</param>
    /// <returns>a pointer to a <see cref="HIDDevice"/> object on success or <c>null</c> on
    /// failure.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static HIDDevice? HIDOpenPath(string path)
    {
        var hidPtr = SDL_hid_open_path(path);

        if (hidPtr == IntPtr.Zero) return null;

        return new HIDDevice(hidPtr);
    }

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_write(IntPtr dev, byte[] data, IntPtr length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_write(SDL_hid_device *dev, const unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Write an Output report to a HID device.</para>
    /// <para>The first byte of <c>data</c> must contain the Report ID. For devices which only
    /// support a single report, this must be set to <c>0x0</c>. The remaining bytes
    /// contain the report data. Since the Report ID is mandatory, calls to
    /// <see cref="HIDWrite"/> will always contain one more byte than the report contains.
    /// For example, if a HID report is 16 bytes long, 17 bytes must be passed to <see cref="HIDWrite"/>,
    /// the Report ID (or <c>0x0</c>, for devices with a single report), followed by the report data (16 bytes).
    /// In this example, the length passed in would be 17.</para>
    /// <para><see cref="HIDWrite"/> will send the data on the first OUT endpoint, if one
    /// exists. If it does not, it will send the data through the Control Endpoint
    /// (Endpoint 0).</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">the data to send, including the report number as the first
    /// byte.</param>
    /// <param name="length">the length in bytes of the data to send.</param>
    /// <returns>the actual number of bytes written and <c>-1</c> on error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDWrite(HIDDevice dev, byte[] data, int length) => SDL_hid_write(dev.Handle, data, 
        new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read_timeout(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length, int milliseconds);
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
    /// reports, make sure to read an extra byte for the report number.</param>
    /// <param name="milliseconds">timeout in milliseconds or <c>-1</c> for blocking wait.</param>
    /// <returns>the actual number of bytes read and <c>-1</c> on error. If no packet was
    /// available to be read within the timeout period, this function
    /// returns <c>0</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDReadTimeout(HIDDevice dev, out byte[] data, int length, int milliseconds) =>
        SDL_hid_read_timeout(dev.Handle, out data, new IntPtr(length), milliseconds);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length);
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
    /// reports, make sure to read an extra byte for the report number.</param>
    /// <returns>the actual number of bytes read and <c>-1</c> on error. If no packet was
    /// available to be read and the handle is in non-blocking mode, this
    /// function returns <c>0</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDRead(HIDDevice dev, out byte[] data, int length) => SDL_hid_read(dev.Handle, out data, 
        new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_set_nonblocking(IntPtr dev, int nonblock);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_set_nonblocking(SDL_hid_device *dev, int nonblock);</code>
    /// <summary>
    /// <para>Set the device handle to be non-blocking.</para>
    /// <para>In non-blocking mode calls to <see cref="HIDRead"/> will return immediately with a
    /// value of <c>0</c> if there is no data to be read. In blocking mode, <see cref="HIDRead"/>
    /// will wait (block) until there is data to read before returning.</para>
    /// <para>Nonblocking can be turned on and off at any time.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="nonblock">enable or not the nonblocking reads - <c>1</c> to enable
    /// nonblocking - <c>0</c> to disable nonblocking.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDSetNonBlocking(HIDDevice dev, int nonblock) => SDL_hid_set_nonblocking(dev.Handle, nonblock);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_send_feature_report(IntPtr dev, byte[] data, IntPtr length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_send_feature_report(SDL_hid_device *dev, const unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Send a Feature report to the device.</para>
    /// <para>Feature reports are sent over the Control endpoint as a Set_Report
    /// transfer. The first byte of <c>data</c> must contain the Report ID. For devices
    /// which only support a single report, this must be set to <c>0x0</c>. The remaining
    /// bytes contain the report data. Since the Report ID is mandatory, calls to
    /// <see cref="HIDGetFeatureReport"/> will always contain one more byte than the
    /// report contains. For example, if a HID report is 16 bytes long, 17 bytes
    /// must be passed to <see cref="HIDGetFeatureReport"/>: the Report ID (or <c>0x0</c>, for
    /// devices which do not use numbered reports), followed by the report data (16
    /// bytes). In this example, the length passed in would be 17.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">the data to send, including the report number as the first
    /// byte.</param>
    /// <param name="length">the length in bytes of the data to send, including the report
    /// number.</param>
    /// <returns>the actual number of bytes written and <c>-1</c> on error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDSendFeatureReport(HIDDevice dev, byte[] data, int length) =>
        SDL_hid_send_feature_report(dev.Handle, data, new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_feature_report(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_feature_report(SDL_hid_device *dev, unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Get a feature report from a HID device.</para>
    /// <para>Set the first byte of <c>data</c> to the Report ID of the report to be read.
    /// Make sure to allow space for this extra byte in <c>data</c>. Upon return, the
    /// first byte will still contain the Report ID, and the report data will start
    /// in <c>data[1]</c>.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">a buffer to put the read data into, including the Report ID.
    /// Set the first byte of <c>data</c> to the Report ID of the report to
    /// be read, or set it to zero if your device does not use numbered
    /// reports.</param>
    /// <param name="length">the number of bytes to read, including an extra byte for the
    /// report ID. The buffer can be longer than the actual report.</param>
    /// <returns>the number of bytes read plus one for the report ID (which is
    /// still in the first byte), or <c>-1</c> on error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDGetFeatureReport(HIDDevice dev, out byte[] data, int length) =>
        SDL_hid_get_feature_report(dev.Handle, out data, new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_input_report(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_input_report(SDL_hid_device *dev, unsigned char *data, size_t length);</code>
    /// <summary>
    /// <para>Get an input report from a HID device.</para>
    /// <para>Set the first byte of <c>data</c> to the Report ID of the report to be read.
    /// Make sure to allow space for this extra byte in <c>data</c>. Upon return, the
    /// first byte will still contain the Report ID, and the report data will start
    /// in <c>data[1]</c>.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="data">a buffer to put the read data into, including the Report ID.
    /// Set the first byte of <c>data</c> to the Report ID of the report to
    /// be read, or set it to zero if your device does not use numbered
    /// reports.</param>
    /// <param name="length">the number of bytes to read, including an extra byte for the
    /// report ID. The buffer can be longer than the actual report.</param>
    /// <returns>the number of bytes read plus one for the report ID (which is
    /// still in the first byte), or <c>-1</c> on error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HidGetInputReport(HIDDevice dev, out byte[] data, int length) =>
        SDL_hid_get_input_report(dev.Handle, out data, new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_close(IntPtr dev);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_close(SDL_hid_device *dev);</code>
    /// <summary>
    /// Close a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <returns>0 on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDClose(HIDDevice dev) => SDL_hid_close(dev.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_manufacturer_string(IntPtr dev, [MarshalAs(UnmanagedType.LPWStr)] out string str,
        IntPtr maxlen);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_manufacturer_string(SDL_hid_device *dev, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get The Manufacturer String from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="str">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of <c>wchar_t</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDGetManufacturerString(HIDDevice dev, out string str, int maxlen) =>
        SDL_hid_get_manufacturer_string(dev.Handle, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_product_string(IntPtr dev, [MarshalAs(UnmanagedType.LPWStr)] out string str,
        IntPtr maxlen);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_product_string(SDL_hid_device *dev, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get The Product String from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="str">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of <c>wchar_t</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDGetProductString(HIDDevice dev, out string str, int maxlen) =>
        SDL_hid_get_product_string(dev.Handle, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_serial_number_string(IntPtr dev, [MarshalAs(UnmanagedType.LPWStr)]out string str,
        IntPtr maxlen);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_serial_number_string(SDL_hid_device *dev, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get The Serial Number String from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="str">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of <c>wchar_t</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDGetSerialNumberString(HIDDevice dev, out string str, int maxlen) =>
        SDL_hid_get_serial_number_string(dev.Handle, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_indexed_string(IntPtr dev, int stringIndex,
        [MarshalAs(UnmanagedType.LPWStr)] out string str, IntPtr maxlen);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_indexed_string(SDL_hid_device *dev, int string_index, wchar_t *string, size_t maxlen);</code>
    /// <summary>
    /// Get a string from a HID device, based on its string index.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="stringIndex">the index of the string to get.</param>
    /// <param name="str">a wide string buffer to put the data into.</param>
    /// <param name="maxlen">the length of the buffer in multiples of <c>wchar_t</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDGetIndexedString(HIDDevice dev, int stringIndex, out string str, int maxlen) =>
        SDL_hid_get_indexed_string(dev.Handle, stringIndex, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_get_device_info(IntPtr dev);
    /// <code>extern SDL_DECLSPEC SDL_hid_device_info * SDLCALL SDL_hid_get_device_info(SDL_hid_device *dev);</code>
    /// <summary>
    /// Get the device info from a HID device.
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <returns>a pointer to the <see cref="HIDDeviceInfo"/> for this HID device, or <c>null</c> in the case
    /// of failure; call <see cref="GetError"/> for more information.
    /// This struct is valid until the device is closed with <see cref="HIDClose"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static HIDDeviceInfo? HIDGetDeviceInfo(HIDDevice dev) =>
        Marshal.PtrToStructure<HIDDeviceInfo>(SDL_hid_get_device_info(dev.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_report_descriptor(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, nint bufSize);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_hid_get_report_descriptor(SDL_hid_device *dev, unsigned char *buf, size_t buf_size);</code>
    /// <summary>
    /// Get a report descriptor from a HID device.
    /// <para>User has to provide a preallocated buffer where descriptor will be copied to.
    /// The recommended size for a preallocated buffer is 4096 bytes.</para>
    /// </summary>
    /// <param name="dev">a device handle returned from <see cref="HIDOpen"/>.</param>
    /// <param name="buf">the buffer to copy descriptor into.</param>
    /// <param name="bufSize">the size of the buffer in bytes.</param>
    /// <returns>the number of bytes actually copied, or <c>-1</c> on error; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int HIDGetReportDescriptor(HIDDevice dev, out byte[] buf, int bufSize = 4096)
    {
        buf = new byte[bufSize];

        var result = SDL_hid_get_report_descriptor(dev.Handle, buf, bufSize);
        if (result < 0)
        {
            buf = [];
        }
        else if (result < buf.Length)
        {
            Array.Resize(ref buf, result);
        }

        return result;
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_hid_ble_scan([MarshalAs(SDLBool)] bool active);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_hid_ble_scan(SDL_bool active);</code>
    /// <summary>
    /// Start or stop a BLE scan on iOS and tvOS to pair Steam Controllers.
    /// </summary>
    /// <param name="active"><c>true</c> to start the scan, <c>false</c> to stop the scan.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void HIDBleScan(bool active) => SDL_hid_ble_scan(active);
}