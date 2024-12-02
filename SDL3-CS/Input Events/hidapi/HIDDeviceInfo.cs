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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Information about a connected HID device
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct HIDDeviceInfo
    {
        private IntPtr path;
        private ushort vendor_id;
        private ushort product_id;
        [MarshalAs(UnmanagedType.LPWStr)]
        private string serial_number;
        private ushort release_number;
        [MarshalAs(UnmanagedType.LPWStr)]
        private string manufacturer_string;
        [MarshalAs(UnmanagedType.LPWStr)]
        private string product_string;
        private ushort usage_page;
        private ushort usage;
        private int interface_number;
        private int interface_class;
        private int interface_subclass;
        private int interface_protocol;
        private HIDBusType bus_type;
        private IntPtr next;
        
        
        /// <summary>
        /// Platform-specific device path
        /// </summary>
        public string? Path => Marshal.PtrToStringAnsi(path);
        
        /// <summary>
        /// Device Vendor ID
        /// </summary>
        public ushort VendorId => vendor_id;
        
        /// <summary>
        /// Device Product ID
        /// </summary>
        public ushort ProductId => product_id;
        
        /// <summary>
        /// Serial Number
        /// </summary>
        public string? SerialNumber => serial_number;
        
        /// <summary>
        /// Device Release Number in binary-coded decimal,
        /// also known as Device Version Number
        /// </summary>
        public ushort ReleaseNumber => release_number;
        
        /// <summary>
        /// Manufacturer String
        /// </summary>
        public string? ManufacturerString => manufacturer_string;
        
        /// <summary>
        /// Product string
        /// </summary>
        public string? ProductString => product_string;
        
        /// <summary>
        /// Usage Page for this Device/Interface
        /// (Windows/Mac/hidraw only)
        /// </summary>
        public ushort UsagePage => usage_page;
        
        /// <summary>
        /// Usage for this Device/Interface
        /// (Windows/Mac/hidraw only)
        /// </summary>
        public ushort Usage => usage;
        
        /// <summary>
        /// The USB interface which this logical device
        /// represents.
        ///
        /// Valid only if the device is a USB HID device.
        /// Set to -1 in all other cases.
        /// </summary>
        public int InterfaceNumber => interface_number;
        
        /// <summary>
        /// Additional information about the USB interface.
        /// Valid on libusb and Android implementations.
        /// </summary>
        public int InterfaceClass => interface_class;
        public int InterfaceSubclass => interface_subclass;
        public int InterfaceProtocol => interface_protocol;
        
        /// <summary>
        /// Underlying bus type
        /// </summary>
        public HIDBusType BusType => bus_type;
        
        /// <summary>
        /// Pointer to the next device
        /// </summary>
        public IntPtr Next => next;
    }
}