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

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_init();
    public static int HIDInit() => SDL_hid_init();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_exit();
    public static int HIDExit() => SDL_hid_exit();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_hid_device_change_count();
    public static uint HIDDeviceChangeCount() => SDL_hid_device_change_count();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_enumerate(ushort venodrId, ushort productId);

    public static HIDDeviceInfo[] HIDEnumerate(ushort vendorId, ushort productId)
    {
        var deviceInfoPtr = SDL_hid_enumerate(vendorId, productId);
        if (deviceInfoPtr == IntPtr.Zero)
        {
            return [];
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
    //public static void HIDFreeEnumeration(HIDDeviceInfo devs) => SDL_hid_free_enumeration(devs);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_open(ushort vendorId, ushort productId, 
        [MarshalAs(UnmanagedType.LPWStr)]string serialNumber);
    public static HIDDevice HIDOpen(ushort vendorId, ushort productId, string serialNumber) => 
        new(SDL_hid_open(vendorId, productId, serialNumber));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_open_path([MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    public static HIDDevice HIDOpenPath(string path) => new(SDL_hid_open_path(path));

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_write(IntPtr dev, byte[] data, IntPtr length);

    public static int HIDWrite(HIDDevice dev, byte[] data, int length) => SDL_hid_write(dev.Handle, data, 
        new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read_timeout(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length, int milliseconds);
    public static int HIDReadTimeout(HIDDevice dev, out byte[] data, int length, int milliseconds) =>
        SDL_hid_read_timeout(dev.Handle, out data, new IntPtr(length), milliseconds);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_read(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length);
    public static int HIDRead(HIDDevice dev, out byte[] data, int length) => SDL_hid_read(dev.Handle, out data, 
        new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_set_nonblocking(IntPtr dev, int nonblock);
    public static int HIDSetNonBlocking(HIDDevice dev, int nonblock) => SDL_hid_set_nonblocking(dev.Handle, nonblock);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_send_feature_report(IntPtr dev, byte[] data, IntPtr length);
    public static int HIDSendFeatureReport(HIDDevice dev, byte[] data, int length) =>
        SDL_hid_send_feature_report(dev.Handle, data, new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_feature_report(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length);
    public static int HIDGetFeatureReport(HIDDevice dev, out byte[] data, int length) =>
        SDL_hid_get_feature_report(dev.Handle, out data, new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_input_report(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] data, IntPtr length);
    public static int HidGetInputReport(HIDDevice dev, out byte[] data, int length) =>
        SDL_hid_get_input_report(dev.Handle, out data, new IntPtr(length));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_close(IntPtr dev);
    public static int HIDClose(HIDDevice dev) => SDL_hid_close(dev.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_manufacturer_string(IntPtr dev, [MarshalAs(UnmanagedType.LPWStr)] out string str,
        IntPtr maxlen);
    public static int HIDGetManufacturerString(HIDDevice dev, out string str, int maxlen) =>
        SDL_hid_get_manufacturer_string(dev.Handle, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_product_string(IntPtr dev, [MarshalAs(UnmanagedType.LPWStr)] out string str,
        IntPtr maxlen);
    public static int HIDGetProductString(HIDDevice dev, out string str, int maxlen) =>
        SDL_hid_get_product_string(dev.Handle, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_serial_number_string(IntPtr dev, [MarshalAs(UnmanagedType.LPWStr)]out string str,
        IntPtr maxlen);
    public static int HIDGetSerialNumberString(HIDDevice dev, out string str, int maxlen) =>
        SDL_hid_get_serial_number_string(dev.Handle, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_indexed_string(IntPtr dev, int stringIndex,
        [MarshalAs(UnmanagedType.LPWStr)] out string str, IntPtr maxlen);
    public static int HIDGetIndexedString(HIDDevice dev, int stringIndex, out string str, int maxlen) =>
        SDL_hid_get_indexed_string(dev.Handle, stringIndex, out str, new IntPtr(maxlen));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_hid_get_device_info(IntPtr dev);

    public static HIDDeviceInfo? HIDGetDeviceInfo(HIDDevice dev) =>
        Marshal.PtrToStructure<HIDDeviceInfo>(SDL_hid_get_device_info(dev.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_hid_get_report_descriptor(IntPtr dev, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, nint bufSize);
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
    private static partial void SDL_hid_ble_scan([MarshalAs(UnmanagedType.I1)] bool active);
    public static void HIDBleScan(bool active) => SDL_hid_ble_scan(active);
}