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
    private const uint WindowPosUndefinedMask = 0x1FFF0000u;
    private const uint WindowPosCenteredMask = 0x2FFF0000u;
    
    public static uint WindowPosUndefinedDisplay(int x) => WindowPosUndefinedMask | (uint)x;
    public static readonly uint WindowPosUndefined = WindowPosUndefinedDisplay(0);
    public static bool IsWindowPosUndefined(uint x) => (x & 0xFFFF0000) == WindowPosUndefinedMask;
    
    public static uint WindowPosCenteredDisplay(int x) => WindowPosCenteredMask | (uint)x;
    public static readonly uint WindowPosCentered = WindowPosCenteredDisplay(0);
    public static bool IsWindowPosCentered(uint x) => (x & 0xFFFF0000) == WindowPosCenteredMask;
}