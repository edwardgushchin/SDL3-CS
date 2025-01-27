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
    /// <para>Constant effect supported.</para>
    /// <para>Constant haptic effect.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticCondition"/>
    public const uint HAPTIC_CONSTANT = 1u << 0;

    /// <summary>
    /// <para>Sine wave effect supported.</para>
    /// <para>Periodic haptic effect that simulates sine waves.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticPeriodic"/>
    public const uint HAPTIC_SINE = 1u << 1;

    /// <summary>
    /// <para>Square wave effect supported.</para>
    /// <para>Periodic haptic effect that simulates square waves.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticPeriodic"/>
    public const uint HAPTIC_SQUARE = 1u << 2;
    
    /// <summary>
    /// <para>Triangle wave effect supported.</para>
    /// <para>Periodic haptic effect that simulates triangular waves.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticPeriodic"/>
    public const uint HAPTIC_TRIANGLE = 1u << 3;
    
    /// <summary>
    /// <para>Sawtoothup wave effect supported.</para>
    /// <para>Periodic haptic effect that simulates saw tooth up waves.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticPeriodic"/>
    public const uint HAPTIC_SAWTOOTHUP = 1u << 4;
    
    /// <summary>
    /// <para>Sawtoothdown wave effect supported.</para>
    /// <para>Periodic haptic effect that simulates saw tooth down waves.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticPeriodic"/>
    public const uint HAPTIC_SAWTOOTHDOWN = 1u << 5;
    
    /// <summary>
    /// <para>Ramp effect supported.</para>
    /// <para>Ramp haptic effect.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticRamp"/>
    public const uint HAPTIC_RAMP = 1u << 6;
    
    /// <summary>
    /// <para>Spring effect supported - uses axes position.</para>
    /// <para>Condition haptic effect that simulates a spring. Effect is based on the
    /// axes position.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticCondition"/>
    public const uint HAPTIC_SPRING = 1u << 7;

    /// <summary>
    /// <para>Damper effect supported - uses axes velocity.</para>
    /// <para>Condition haptic effect that simulates dampening. Effect is based on the
    /// axes velocity.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticCondition"/>
    public const uint HAPTIC_DAMPER = 1u << 8;
    
    /// <summary>
    /// <para>Inertia effect supported - uses axes acceleration.</para>
    /// <para>Condition haptic effect that simulates inertia. Effect is based on the axes
    /// acceleration.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticCondition"/>
    public const uint HAPTIC_INERTIA = 1u << 9;
    
    /// <summary>
    /// <para>Friction effect supported - uses axes movement.</para>
    /// <para>Condition haptic effect that simulates friction. Effect is based on the
    /// axes movement.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticCondition"/>
    public const uint HAPTIC_FRICTION = 1u << 10;
    
    /// <summary>
    /// <para>Left/Right effect supported.</para>
    /// <para>Haptic effect for direct control over high/low frequency motors.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticLeftRight"/>
    public const uint HAPTIC_LEFTRIGHT = 1u << 11;
    
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    public const uint HAPTIC_RESERVED1 = 1u << 12;
    
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    public const uint HAPTIC_RESERVED2 = 1u << 13;
    
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    public const uint HAPTIC_RESERVED3 = 1u << 14;

    /// <summary>
    /// <para>Custom effect is supported.</para>
    /// <para>User defined custom haptic effect.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    public const uint HAPTIC_CUSTOM = 1u << 15;

    /// <summary>
    /// <para>Device can set global gain.</para>
    /// <para>Device supports setting the global gain.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetHapticGain"/>
    public const uint HAPTIC_GAIN = 1u << 16;
    
    /// <summary>
    /// <para>Device can set autocenter.</para>
    /// <para>Device supports setting autocenter.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="SetHapticAutocenter"/>
    public const uint HAPTIC_AUTOCENTER = 1u << 17;
    
    /// <summary>
    /// <para>Device can be queried for effect status.</para>
    /// <para>Device supports querying effect status.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticEffectStatus"/>
    public const uint HAPTIC_STATUS = 1u << 18;
    
    /// <summary>
    /// <para>Device can be paused.</para>
    /// <para>Devices supports being paused.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="PauseHaptic"/>
    /// <seealso cref="ResumeHaptic"/>
    public const uint HAPTIC_PAUSE = 1u << 19;

    /// <summary>
    /// <para>Uses polar coordinates for the direction.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticDirection"/>
    public const byte HAPTIC_POLAR = 0;

    /// <summary>
    /// <para>Uses cartesian coordinates for the direction.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticDirection"/>
    public const byte HAPTIC_CARTESIAN = 1;
    
    /// <summary>
    /// <para>Uses spherical coordinates for the direction.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticDirection"/>
    public const byte HAPTIC_SPHERICAL = 2;
    
    /// <summary>
    /// <para>Use this value to play an effect on the steering wheel axis.</para>
    /// <para>This provides better compatibility across platforms and devices as SDL will
    /// guess the correct axis.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="HapticDirection"/>
    public const uint HAPTIC_STEERING_AXIS = 3;

    /// <summary>
    /// <para>Used to play a device an infinite number of times.</para>
    /// </summary>
    /// <since>This macro is available since SDL 3.2.0</since>
    /// <seealso cref="RunHapticEffect"/>
    public const uint HAPTIC_INFINITY = 4294967295U;
}