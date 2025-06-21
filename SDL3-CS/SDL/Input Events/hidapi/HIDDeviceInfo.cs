#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct HIDDeviceInfo
    {
        /// <summary>
        /// Platform-specific device path
        /// </summary>
        [MarshalAs(UnmanagedType.LPUTF8Str)] public string Path;

        /// <summary>
        /// Device Vendor ID
        /// </summary>
        public ushort VendorID;
        
        /// <summary>
        /// Device Product ID
        /// </summary>
        public ushort ProductID;

        /// <summary>
        /// Serial Number
        /// </summary>
        public IntPtr SerialNumber;
        
        /// <summary>
        /// Device Release Number in binary-coded decimal,
        /// also known as Device Version Number
        /// </summary>
        public ushort ReleaseNumber;
        
        /// <summary>
        /// Manufacturer String
        /// </summary>
        public IntPtr ManufacturerString;
        
        /// <summary>
        /// Product string
        /// </summary>
        public IntPtr ProductString;
        
        /// <summary>
        /// Usage Page for this Device/Interface
        /// (Windows/Mac/hidraw only)
        /// </summary>
        public ushort UsagePage;
        
        /// <summary>
        /// Usage for this Device/Interface
        /// (Windows/Mac/hidraw only)
        /// </summary>
        public ushort Usage;

        /// <summary>
        /// The USB interface which this logical device
        /// represents.
        ///
        /// Valid only if the device is a USB HID device.
        /// Set to -1 in all other cases.
        /// </summary>
        public int InterfaceNumber;
        
        /// <summary>
        /// Additional information about the USB interface.
        /// Valid on libusb and Android implementations.
        /// </summary>
        public int InterfaceClass;
        
        public int InterfaceSubclass;
        
        public int InterfaceProtocol;

        /// <summary>
        /// Underlying bus type
        /// </summary>
        public HIDBusType BusType;

        /// <summary>
        /// Pointer to the next device
        /// </summary>
        public IntPtr Next;
    }
}