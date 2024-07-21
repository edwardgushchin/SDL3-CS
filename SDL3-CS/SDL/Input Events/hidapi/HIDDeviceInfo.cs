#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SDL3;

[SuppressMessage("ReSharper", "ConvertToAutoProperty")]
public static partial class SDL
{
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


        public string? Path => Marshal.PtrToStringAnsi(path);
        public ushort VendorId => vendor_id;
        public ushort ProductId => product_id;
        public string? SerialNumber => serial_number;
        public ushort ReleaseNumber => release_number;
        public string? ManufacturerString => manufacturer_string;
        public string? ProductString => product_string;
        public ushort UsagePage => usage_page;
        public ushort Usage => usage;
        public int InterfaceNumber => interface_number;
        public int InterfaceClass => interface_class;
        public int InterfaceSubclass => interface_subclass;
        public int InterfaceProtocol => interface_protocol;
        public HIDBusType BusType => bus_type;
        public IntPtr Next => next;
    }
}