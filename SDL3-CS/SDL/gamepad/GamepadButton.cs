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

namespace SDL3;

public static partial class SDL
{
    public enum GamepadButton
    {
        Invalid = -1,
        South, /* Bottom face button (e.g. Xbox A button) */
        East, /* Right face button (e.g. Xbox B button) */
        West, /* Left face button (e.g. Xbox X button) */
        North, /* Top face button (e.g. Xbox Y button) */
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
        Misc1, /* Additional button (e.g. Xbox Series X share button, PS5 microphone button, Nintendo Switch Pro capture button, Amazon Luna microphone button, Google Stadia capture button) */
        RightPaddle1, /* Upper or primary paddle, under your right hand (e.g. Xbox Elite paddle P1) */
        LeftPaddle1, /* Upper or primary paddle, under your left hand (e.g. Xbox Elite paddle P3) */
        RightPaddle2, /* Lower or secondary paddle, under your right hand (e.g. Xbox Elite paddle P2) */
        LeftPaddle2, /* Lower or secondary paddle, under your left hand (e.g. Xbox Elite paddle P4) */
        Touchpad, /* PS4/PS5 touchpad button */
        Misc2, /* Additional button */
        Misc3, /* Additional button */
        Misc4, /* Additional button */
        Misc5, /* Additional button */
        Misc6, /* Additional button */
        Max
    }
}