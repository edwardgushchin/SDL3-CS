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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// HID underlying bus types.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum HIDBusType
    {
        /// <summary>
        /// Unknown bus type
        /// </summary>
        Unknown = 0x00,
        
        /// <summary>
        /// USB bus
        /// Specifications:
        /// https://usb.org/hid
        /// </summary>
        USB = 0x01,
        
        /// <summary>
        /// Bluetooth or Bluetooth LE bus
        /// Specifications:
        /// https://www.bluetooth.com/specifications/specs/human-interface-device-profile-1-1-1/
        /// https://www.bluetooth.com/specifications/specs/hid-service-1-0/
        /// https://www.bluetooth.com/specifications/specs/hid-over-gatt-profile-1-0/
        /// </summary>
        Bluetooth = 0x02,
        
        /// <summary>
        /// I2C bus
        /// Specifications:
        /// https://docs.microsoft.com/previous-versions/windows/hardware/design/dn642101(v=vs.85)
        /// </summary>
        I2C = 0x03,
        
        /// <summary>
        /// SPI bus
        /// Specifications:
        /// https://www.microsoft.com/download/details.aspx?id=103325
        /// </summary>
        SPI = 0x04
    }
}