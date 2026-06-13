#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using SDL3;

namespace SDL3.Examples.Common;

internal static class ExampleColors
{
    public static SDL.Color[] Create(int count, bool whiteFirst = false)
    {
        var random = new Random(19790413);
        var colors = new SDL.Color[count];
        var startIndex = 0;

        if (whiteFirst && colors.Length > 0)
        {
            colors[0] = new SDL.Color { R = 255, G = 255, B = 255, A = 255 };
            startIndex = 1;
        }

        for (var i = startIndex; i < colors.Length; i++)
        {
            colors[i] = new SDL.Color
            {
                R = (byte)random.Next(48, 256),
                G = (byte)random.Next(48, 256),
                B = (byte)random.Next(48, 256),
                A = 255
            };
        }

        return colors;
    }
}
