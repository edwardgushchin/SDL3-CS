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
    /// <para>The different sensors defined by SDL.</para>
    /// <para>Additional sensors may be available, using platform dependent semantics.</para>
    /// <para>Here are the additional Android sensors:</para>
    /// <para>https://developer.android.com/reference/android/hardware/SensorEvent.html#values</para>
    /// <para>Accelerometer sensor notes:</para>
    /// <para>The accelerometer returns the current acceleration in SI meters per second
    /// squared. This measurement includes the force of gravity, so a device at
    /// rest will have an value of <see cref="SDL.StandardGravity"/> away from the center of the
    /// earth, which is a positive Y value.</para>
    /// <list type="bullet">
    /// <item><c>values[0]</c>: Acceleration on the x axis</item>
    /// <item><c>values[1]</c>: Acceleration on the y axis</item>
    /// <item><c>values[2]</c>: Acceleration on the z axis</item>
    /// </list>
    /// <para>For phones and tablets held in natural orientation and game controllers
    /// held in front of you, the axes are defined as follows:</para>
    /// <list type="bullet">
    /// <item>-X ... +X : left ... right</item>
    /// <item>-Y ... +Y : bottom ... top</item>
    /// <item>-Z ... +Z : farther ... closer</item>
    /// </list>
    /// <para>The accelerometer axis data is not changed when the device is rotated.</para>
    /// <para>Gyroscope sensor notes:</para>
    /// <para>The gyroscope returns the current rate of rotation in radians per second.
    /// The rotation is positive in the counter-clockwise direction. That is, an
    /// observer looking from a positive location on one of the axes would see
    /// positive rotation on that axis when it appeared to be rotating
    /// counter-clockwise.</para>
    /// <list type="bullet">
    /// <item><c>values[0]</c>: Angular speed around the x axis (pitch)</item>
    /// <item><c>values[1]</c>: Angular speed around the y axis (yaw)</item>
    /// <item><c>values[2]</c>: Angular speed around the z axis (roll)</item>
    /// </list>
    /// <para>For phones and tablets held in natural orientation and game controllers
    /// held in front of you, the axes are defined as follows:</para>
    /// <list type="bullet">
    /// <item>-X ... +X : left ... right</item>
    /// <item>-Y ... +Y : bottom ... top</item>
    /// <item>-Z ... +Z : farther ... closer</item>
    /// </list>
    /// <para>The gyroscope axis data is not changed when the device is rotated.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="GetCurrentDisplayOrientation"/>
    public enum SensorType
    {
        /// <summary>
        /// Returned for an invalid sensor
        /// </summary>
        Invalid = -1,
        
        /// <summary>
        /// Unknown sensor type
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Accelerometer
        /// </summary>
        Accel,
        
        /// <summary>
        /// Gyroscope
        /// </summary>
        Gyro,
        
        /// <summary>
        /// Accelerometer for left Joy-Con controller and Wii nunchuk
        /// </summary>
        AccelL,
        
        /// <summary>
        /// Gyroscope for left Joy-Con controller
        /// </summary>
        GyroL,
        
        /// <summary>
        /// Accelerometer for right Joy-Con controller
        /// </summary>
        AccelR,
        
        /// <summary>
        /// Gyroscope for right Joy-Con controller
        /// </summary>
        GyroR,
        
        Count
    }
}