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
    /// <para>The generic template for any haptic effect.</para>
    /// <para>All values max at 32767 (0x7FFF). Signed values also can be negative. Time
    /// values unless specified otherwise are in milliseconds.</para>
    /// <para>You can also pass <see cref="HAPTIC_INFINITY"/> to length instead of a 0-32767 value.
    /// Neither delay, interval, attack_length nor fade_length support
    /// <see cref="HAPTIC_INFINITY"/>. Fade will also not be used since effect never ends.</para>
    /// <para>Additionally, the <see cref="HAPTIC_RAMP"/> effect does not support a duration of
    /// <see cref="HAPTIC_INFINITY"/>.</para>
    /// <para>Button triggers may not be supported on all devices, it is advised to not
    /// use them if possible. Buttons start at index 1 instead of index 0 like the
    /// joystick.</para>
    /// <para>If both attack_length and fade_level are 0, the envelope is not used,
    /// otherwise both values are used.</para>
    /// <para>Common parts:</para>
    /// <code>
    ///  // Replay - All effects have this
    ///  Uint32 length;        // Duration of effect (ms).
    ///  Uint16 delay;         // Delay before starting effect.
    ///
    ///  // Trigger - All effects have this
    ///  Uint16 button;        // Button that triggers effect.
    ///  Uint16 interval;      // How soon before effect can be triggered again.
    ///
    ///  // Envelope - All effects except condition effects have this
    ///  Uint16 attack_length; // Duration of the attack (ms).
    ///  Uint16 attack_level;  // Level at the start of the attack.
    ///  Uint16 fade_length;   // Duration of the fade out (ms).
    ///  Uint16 fade_level;    // Level at the end of the fade.
    /// </code>
    /// <para>Here we have an example of a constant effect evolution in time:</para>
    /// <code>
    ///   Strength
    ///  ^
    ///  |
    ///  |    effect level -->  _________________
    ///  |                     /                 \
    ///  |                    /                   \
    ///  |                   /                     \
    ///  |                  /                       \
    ///  | attack_level --> |                        \
    ///  |                  |                        |  &lt;---  fade_level
    ///  |
    ///  +--------------------------------------------------> Time
    ///                     [--]                 [---]
    ///                     attack_length        fade_length
    ///
    ///  [------------------][-----------------------]
    ///  delay               length
    /// </code>
    /// <para>Note either the attack_level or the fade_level may be above the actual
    /// effect level.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="HapticConstant"/>
    /// <seealso cref="HapticPeriodic"/>
    /// <seealso cref="HapticCondition"/>
    /// <seealso cref="HapticRamp"/>
    /// <seealso cref="HapticLeftRight"/>
    /// <seealso cref="HapticCustom"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct HapticEffect
    {
        /// <summary>
        /// Effect type.
        /// </summary>
        public UInt16 Type;

        /// <summary>
        /// Constant effect.
        /// </summary>
        public HapticConstant Constant;
        
        /// <summary>
        /// Periodic effect.
        /// </summary>
        public HapticPeriodic Periodic;
        
        /// <summary>
        /// Condition effect.
        /// </summary>
        public HapticCondition Condition;
        
        /// <summary>
        /// Ramp effect.
        /// </summary>
        public HapticRamp Ramp;
        
        /// <summary>
        /// Left/Right effect.
        /// </summary>
        public HapticLeftRight Leftright;
        
        /// <summary>
        /// Custom effect.
        /// </summary>
        public HapticCustom Custom;
    }
}