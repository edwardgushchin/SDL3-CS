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
    /// <para>The list of buttons available on a gamepad</para>
    /// <para>For controllers that use a diamond pattern for the face buttons, the
    /// south/east/west/north buttons below correspond to the locations in the
    /// diamond pattern. For Xbox controllers, this would be A/B/X/Y, for Nintendo
    /// Switch controllers, this would be B/A/Y/X, for GameCube controllers this
    /// would be A/X/B/Y, for PlayStation controllers this would be
    /// Cross/Circle/Square/Triangle.</para>
    /// <para>For controllers that don't use a diamond pattern for the face buttons, the
    /// south/east/west/north buttons indicate the buttons labeled A, B, C, D, or
    /// 1, 2, 3, 4, or for controllers that aren't labeled, they are the primary,
    /// secondary, etc. buttons.</para>
    /// <para>The activate action is often the south button and the cancel action is
    /// often the east button, but in some regions this is reversed, so your game
    /// should allow remapping actions based on user preferences.</para>
    /// <para>You can query the labels for the face buttons using
    /// <see cref="GetGamepadButtonLabel"/></para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum GamepadButton
    {
        Invalid = -1,
        
        /// <summary>
        /// Bottom face button (e.g. Xbox A button)
        /// </summary>
        South,
        
        /// <summary>
        /// Right face button (e.g. Xbox B button)
        /// </summary>
        East,
        
        /// <summary>
        /// Left face button (e.g. Xbox X button)
        /// </summary>
        West,
        
        /// <summary>
        /// Top face button (e.g. Xbox Y button)
        /// </summary>
        North,
        Back,
        Guide,
        Start,
        LeftStick,
        RightStick,
        LeftShoulder,
        RightShoulder,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        
        /// <summary>
        /// Additional button (e.g. Xbox Series X share button, PS5 microphone button, Nintendo Switch Pro capture button, Amazon Luna microphone button, Google Stadia capture button)
        /// </summary>
        Misc1,
        
        /// <summary>
        /// Upper or primary paddle, under your right hand (e.g. Xbox Elite paddle P1, DualSense Edge RB button, Right Joy-Con SR button)
        /// </summary>
        RightPaddle1,
        
        /// <summary>
        /// Upper or primary paddle, under your left hand (e.g. Xbox Elite paddle P3, DualSense Edge LB button, Left Joy-Con SL button)
        /// </summary>
        LeftPaddle1,
        
        /// <summary>
        /// Lower or secondary paddle, under your right hand (e.g. Xbox Elite paddle P2, DualSense Edge right Fn button, Right Joy-Con SL button
        /// </summary>
        RightPaddle2,
        
        /// <summary>
        /// Lower or secondary paddle, under your left hand (e.g. Xbox Elite paddle P4, DualSense Edge left Fn button, Left Joy-Con SR button)
        /// </summary>
        LeftPaddle2,
        
        /// <summary>
        /// PS4/PS5 touchpad button
        /// </summary>
        Touchpad,
        
        /// <summary>
        /// Additional button
        /// </summary>
        Misc2,
        
        /// <summary>
        /// Additional button (e.g. Nintendo GameCube left trigger click)
        /// </summary>
        Misc3,
        
        /// <summary>
        /// Additional button (e.g. Nintendo GameCube right trigger click)
        /// </summary>
        Misc4,
        
        /// <summary>
        /// Additional button
        /// </summary>
        Misc5,
        
        /// <summary>
        /// Additional button
        /// </summary>
        Misc6,
        Count
    }
}